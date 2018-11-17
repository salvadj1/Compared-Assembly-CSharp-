using System;
using System.Collections;
using System.Runtime.InteropServices;
using uLink;
using UnityEngine;

// Token: 0x020006E3 RID: 1763
public class ClientConnect : MonoBehaviour
{
	// Token: 0x06003B89 RID: 15241 RVA: 0x000D4D24 File Offset: 0x000D2F24
	public static global::ClientConnect Instance()
	{
		GameObject gameObject = new GameObject();
		Object.DontDestroyOnLoad(gameObject);
		return gameObject.AddComponent<global::ClientConnect>();
	}

	// Token: 0x06003B8A RID: 15242
	[DllImport("librust")]
	public static extern uint SteamClient_GetAuth(IntPtr pData, int iMaxLength);

	// Token: 0x06003B8B RID: 15243
	[DllImport("librust")]
	public static extern IntPtr Steam_GetDisplayname();

	// Token: 0x06003B8C RID: 15244
	[DllImport("librust")]
	public static extern ulong Steam_GetSteamID();

	// Token: 0x06003B8D RID: 15245 RVA: 0x000D4D48 File Offset: 0x000D2F48
	public bool DoConnect(string strURL, int iPort)
	{
		global::SteamClient.Needed();
		global::NetCull.config.timeoutDelay = 60f;
		if (global::ClientConnect.Steam_GetSteamID() == 0UL)
		{
			global::LoadingScreen.Update("connection failed (no steam detected)");
			Object.Destroy(base.gameObject);
			return false;
		}
		byte[] array = new byte[1024];
		IntPtr intPtr = Marshal.AllocHGlobal(1024);
		uint num = global::ClientConnect.SteamClient_GetAuth(intPtr, 1024);
		byte[] array2 = new byte[num];
		Marshal.Copy(intPtr, array2, 0, (int)num);
		Marshal.FreeHGlobal(intPtr);
		BitStream bitStream = new BitStream(false);
		bitStream.WriteInt32(1069);
		bitStream.WriteByte(2);
		bitStream.WriteUInt64(global::ClientConnect.Steam_GetSteamID());
		bitStream.WriteString(Marshal.PtrToStringAnsi(global::ClientConnect.Steam_GetDisplayname()));
		bitStream.WriteBytes(array2);
		try
		{
			global::NetError netError = global::NetCull.Connect(strURL, iPort, string.Empty, new object[]
			{
				bitStream
			});
			if (netError != global::NetError.NoError)
			{
				global::LoadingScreen.Update("connection failed (" + netError + ")");
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
		global::SteamClient.SteamClient_OnJoinServer(strURL, iPort);
		return true;
	}

	// Token: 0x06003B8E RID: 15246 RVA: 0x000D4EA4 File Offset: 0x000D30A4
	private void uLink_OnConnectedToServer()
	{
		global::LoadingScreen.Update("connected!");
		BitStream bitStream = new BitStream((byte[])global::NetCull.approvalData.ReadObject(typeof(byte[]).TypeHandle, new object[0]), false);
		string text = bitStream.ReadString();
		global::NetCull.sendRate = bitStream.ReadSingle();
		string str = bitStream.ReadString();
		bool flag = bitStream.ReadBoolean();
		bool flag2 = bitStream.ReadBoolean();
		if (bitStream.bytesRemaining > 8)
		{
			ulong serverid = bitStream.ReadUInt64();
			uint serverip = bitStream.ReadUInt32();
			int serverport = bitStream.ReadInt32();
			global::SteamClient.SteamUser_AdvertiseGame(serverid, serverip, serverport);
		}
		Debug.Log("Server Name: \"" + str + "\"");
		Debug.Log("Level Name: \"" + text + "\"");
		Debug.Log("Send Rate: " + global::NetCull.sendRate);
		global::NetCull.isMessageQueueRunning = false;
		base.StartCoroutine(this.LoadLevel(text));
		global::DisableOnConnectedState.OnConnected();
	}

	// Token: 0x06003B8F RID: 15247 RVA: 0x000D4F9C File Offset: 0x000D319C
	private IEnumerator LoadLevel(string levelName)
	{
		AudioSource audioSource = base.GetComponentInChildren<AudioSource>();
		if (audioSource)
		{
			audioSource.enabled = false;
		}
		yield return global::RustLevel.Load(levelName, out this.levelLoader);
		global::GameEvent.DoQualitySettingsRefresh();
		Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x06003B90 RID: 15248 RVA: 0x000D4FC8 File Offset: 0x000D31C8
	private void uLink_OnFailedToConnect(uLink.NetworkConnectionError ulink_error)
	{
		if (this.levelLoader)
		{
			Object.Destroy(this.levelLoader);
		}
		if (!global::MainMenu.singleton)
		{
			global::NetError netError = ulink_error.ToNetError();
			if (netError != global::NetError.NoError)
			{
				Debug.LogError(netError.NiceString());
			}
		}
		try
		{
			global::DisableOnConnectedState.OnDisconnected();
		}
		finally
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06003B91 RID: 15249 RVA: 0x000D504C File Offset: 0x000D324C
	private void uLink_OnDisconnectedFromServer(uLink.NetworkDisconnection netDisconnect)
	{
		if (this.levelLoader)
		{
			Object.Destroy(this.levelLoader);
		}
		try
		{
			global::DisableOnConnectedState.OnDisconnected();
		}
		finally
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04001DBC RID: 7612
	[NonSerialized]
	private GameObject levelLoader;
}
