using System;
using System.Runtime.InteropServices;
using Rust.Steam;
using UnityEngine;
using UnityEngine.Cloud.Analytics;

// Token: 0x020003DD RID: 989
public class SteamClient : MonoBehaviour
{
	// Token: 0x060024CD RID: 9421 RVA: 0x0008D614 File Offset: 0x0008B814
	public static void Create()
	{
		if (!SteamClient.SteamClient_Init())
		{
			Application.Quit();
			return;
		}
		SteamClient.steamClientObject = new GameObject();
		Object.DontDestroyOnLoad(SteamClient.steamClientObject);
		SteamClient.steamClientObject.AddComponent<SteamClient>();
		SteamClient.steamClientObject.name = "SteamClient";
	}

	// Token: 0x060024CE RID: 9422 RVA: 0x0008D660 File Offset: 0x0008B860
	public void Start()
	{
		SteamGroups.Init();
		UnityAnalytics.SetUserId(ClientConnect.Steam_GetSteamID().ToString());
		UnityAnalytics.StartSDK("9fb52793-aff8-4ef9-9381-1c26affde21e");
	}

	// Token: 0x060024CF RID: 9423 RVA: 0x0008D690 File Offset: 0x0008B890
	public static void Needed()
	{
		if (SteamClient.steamClientObject != null)
		{
			return;
		}
		SteamClient.Create();
	}

	// Token: 0x060024D0 RID: 9424 RVA: 0x0008D6A8 File Offset: 0x0008B8A8
	public void Update()
	{
		SteamClient.SteamClient_Cycle();
	}

	// Token: 0x060024D1 RID: 9425 RVA: 0x0008D6B0 File Offset: 0x0008B8B0
	public void OnDestroy()
	{
		SteamClient.SteamClient_Shutdown();
	}

	// Token: 0x060024D2 RID: 9426
	[DllImport("librust")]
	public static extern bool SteamClient_Init();

	// Token: 0x060024D3 RID: 9427
	[DllImport("librust")]
	public static extern void SteamClient_Shutdown();

	// Token: 0x060024D4 RID: 9428
	[DllImport("librust")]
	public static extern void SteamClient_Cycle();

	// Token: 0x060024D5 RID: 9429
	[DllImport("librust")]
	public static extern void SteamClient_OnJoinServer(string strHost, int iIP);

	// Token: 0x060024D6 RID: 9430
	[DllImport("librust")]
	public static extern void SteamUser_AdvertiseGame(ulong serverid, uint serverip, int serverport);

	// Token: 0x040011D1 RID: 4561
	public static GameObject steamClientObject;

	// Token: 0x040011D2 RID: 4562
	protected static Vector3 vOldPosition = Vector3.zero;
}
