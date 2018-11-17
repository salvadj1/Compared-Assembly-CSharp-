using System;
using System.Collections;
using System.Runtime.InteropServices;
using uLink;
using UnityEngine;

// Token: 0x02000622 RID: 1570
public class ClientConnect : MonoBehaviour
{
	// Token: 0x060037A9 RID: 14249 RVA: 0x000CC64C File Offset: 0x000CA84C
	public static ClientConnect Instance()
	{
		GameObject gameObject = new GameObject();
		Object.DontDestroyOnLoad(gameObject);
		return gameObject.AddComponent<ClientConnect>();
	}

	// Token: 0x060037AA RID: 14250
	[DllImport("librust")]
	public static extern uint SteamClient_GetAuth(IntPtr pData, int iMaxLength);

	// Token: 0x060037AB RID: 14251
	[DllImport("librust")]
	public static extern IntPtr Steam_GetDisplayname();

	// Token: 0x060037AC RID: 14252
	[DllImport("librust")]
	public static extern ulong Steam_GetSteamID();

	// Token: 0x060037AD RID: 14253 RVA: 0x000CC670 File Offset: 0x000CA870
	public bool DoConnect(string strURL, int iPort)
	{
		SteamClient.Needed();
		NetCull.config.timeoutDelay = 60f;
		if (ClientConnect.Steam_GetSteamID() == 0UL)
		{
			LoadingScreen.Update("connection failed (no steam detected)");
			Object.Destroy(base.gameObject);
			return false;
		}
		byte[] array = new byte[1024];
		IntPtr intPtr = Marshal.AllocHGlobal(1024);
		uint num = ClientConnect.SteamClient_GetAuth(intPtr, 1024);
		byte[] array2 = new byte[num];
		Marshal.Copy(intPtr, array2, 0, (int)num);
		Marshal.FreeHGlobal(intPtr);
		BitStream bitStream = new BitStream(false);
		bitStream.WriteInt32(1069);
		bitStream.WriteByte(2);
		bitStream.WriteUInt64(ClientConnect.Steam_GetSteamID());
		bitStream.WriteString(Marshal.PtrToStringAnsi(ClientConnect.Steam_GetDisplayname()));
		bitStream.WriteBytes(array2);
		try
		{
			NetError netError = NetCull.Connect(strURL, iPort, string.Empty, new object[]
			{
				bitStream
			});
			if (netError != NetError.NoError)
			{
				LoadingScreen.Update("connection failed (" + netError + ")");
				Object.Destroy(base.gameObject);
				return false;
			}
		}
		catch (Exception ex)
		{
			Debug.LogException(ex);
			Object.Destroy(base.gameObject);
			return false;
		}
		SteamClient.SteamClient_OnJoinServer(strURL, iPort);
		return true;
	}

	// Token: 0x060037AE RID: 14254 RVA: 0x000CC7CC File Offset: 0x000CA9CC
	private void uLink_OnConnectedToServer()
	{
		LoadingScreen.Update("connected!");
		BitStream bitStream = new BitStream((byte[])NetCull.approvalData.ReadObject(typeof(byte[]).TypeHandle, new object[0]), false);
		string text = bitStream.ReadString();
		NetCull.sendRate = bitStream.ReadSingle();
		string str = bitStream.ReadString();
		bool flag = bitStream.ReadBoolean();
		bool flag2 = bitStream.ReadBoolean();
		if (bitStream.bytesRemaining > 8)
		{
			ulong serverid = bitStream.ReadUInt64();
			uint serverip = bitStream.ReadUInt32();
			int serverport = bitStream.ReadInt32();
			SteamClient.SteamUser_AdvertiseGame(serverid, serverip, serverport);
		}
		Debug.Log("Server Name: \"" + str + "\"");
		Debug.Log("Level Name: \"" + text + "\"");
		Debug.Log("Send Rate: " + NetCull.sendRate);
		NetCull.isMessageQueueRunning = false;
		base.StartCoroutine(this.LoadLevel(text));
		DisableOnConnectedState.OnConnected();
	}

	// Token: 0x060037AF RID: 14255 RVA: 0x000CC8C4 File Offset: 0x000CAAC4
	private IEnumerator LoadLevel(string levelName)
	{
		AudioSource audioSource = base.GetComponentInChildren<AudioSource>();
		if (audioSource)
		{
			audioSource.enabled = false;
		}
		yield return RustLevel.Load(levelName, out this.levelLoader);
		GameEvent.DoQualitySettingsRefresh();
		Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x060037B0 RID: 14256 RVA: 0x000CC8F0 File Offset: 0x000CAAF0
	private void uLink_OnFailedToConnect(NetworkConnectionError ulink_error)
	{
		if (this.levelLoader)
		{
			Object.Destroy(this.levelLoader);
		}
		if (!MainMenu.singleton)
		{
			NetError netError = ulink_error.ToNetError();
			if (netError != NetError.NoError)
			{
				Debug.LogError(netError.NiceString());
			}
		}
		try
		{
			DisableOnConnectedState.OnDisconnected();
		}
		finally
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x060037B1 RID: 14257 RVA: 0x000CC974 File Offset: 0x000CAB74
	private void uLink_OnDisconnectedFromServer(NetworkDisconnection netDisconnect)
	{
		if (this.levelLoader)
		{
			Object.Destroy(this.levelLoader);
		}
		try
		{
			DisableOnConnectedState.OnDisconnected();
		}
		finally
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04001BD1 RID: 7121
	[NonSerialized]
	private GameObject levelLoader;
}
