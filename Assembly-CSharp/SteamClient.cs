using System;
using System.Runtime.InteropServices;
using Rust.Steam;
using UnityEngine;

// Token: 0x0200048A RID: 1162
public class SteamClient : MonoBehaviour
{
	// Token: 0x0600282F RID: 10287 RVA: 0x00092A10 File Offset: 0x00090C10
	public static void Create()
	{
		if (!global::SteamClient.SteamClient_Init())
		{
			Application.Quit();
			return;
		}
		global::SteamClient.steamClientObject = new GameObject();
		Object.DontDestroyOnLoad(global::SteamClient.steamClientObject);
		global::SteamClient.steamClientObject.AddComponent<global::SteamClient>();
		global::SteamClient.steamClientObject.name = "SteamClient";
	}

	// Token: 0x06002830 RID: 10288 RVA: 0x00092A5C File Offset: 0x00090C5C
	public void Start()
	{
		Rust.Steam.SteamGroups.Init();
	}

	// Token: 0x06002831 RID: 10289 RVA: 0x00092A64 File Offset: 0x00090C64
	public static void Needed()
	{
		if (global::SteamClient.steamClientObject != null)
		{
			return;
		}
		global::SteamClient.Create();
	}

	// Token: 0x06002832 RID: 10290 RVA: 0x00092A7C File Offset: 0x00090C7C
	public void Update()
	{
		global::SteamClient.SteamClient_Cycle();
	}

	// Token: 0x06002833 RID: 10291 RVA: 0x00092A84 File Offset: 0x00090C84
	public void OnDestroy()
	{
		global::SteamClient.SteamClient_Shutdown();
	}

	// Token: 0x06002834 RID: 10292
	[DllImport("librust")]
	public static extern bool SteamClient_Init();

	// Token: 0x06002835 RID: 10293
	[DllImport("librust")]
	public static extern void SteamClient_Shutdown();

	// Token: 0x06002836 RID: 10294
	[DllImport("librust")]
	public static extern void SteamClient_Cycle();

	// Token: 0x06002837 RID: 10295
	[DllImport("librust")]
	public static extern void SteamClient_OnJoinServer(string strHost, int iIP);

	// Token: 0x06002838 RID: 10296
	[DllImport("librust")]
	public static extern void SteamUser_AdvertiseGame(ulong serverid, uint serverip, int serverport);

	// Token: 0x04001337 RID: 4919
	public static GameObject steamClientObject;

	// Token: 0x04001338 RID: 4920
	protected static Vector3 vOldPosition = Vector3.zero;
}
