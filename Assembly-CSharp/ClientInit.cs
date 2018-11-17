using System;
using System.Collections;
using Facepunch.Build;
using uLink;
using UnityEngine;

// Token: 0x020006E6 RID: 1766
public class ClientInit : MonoBehaviour
{
	// Token: 0x06003B9D RID: 15261 RVA: 0x000D52D0 File Offset: 0x000D34D0
	private void Awake()
	{
		Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x06003B9E RID: 15262 RVA: 0x000D52E0 File Offset: 0x000D34E0
	private void Start()
	{
		global::SteamClient.Create();
		global::ConsoleSystem.Run("config.load", false);
		global::ConsoleSystem.Run("serverfavourite.load", false);
		global::HudEnabled.Disable();
		global::DatablockDictionary.Initialize();
		Application.LoadLevelAdditive("GameUI");
		Facepunch.Build.Connection.GameLoaded();
	}

	// Token: 0x06003B9F RID: 15263 RVA: 0x000D531C File Offset: 0x000D351C
	private IEnumerator uLink_OnDisconnectedFromServer(uLink.NetworkDisconnection netDisconnect)
	{
		yield return null;
		yield return null;
		try
		{
			global::SoundPool.Drain();
		}
		catch (Exception ex)
		{
			Exception e = ex;
			Debug.LogException(e);
		}
		try
		{
			global::DestroysOnDisconnect.OnDisconnectedFromServer();
		}
		catch (Exception ex2)
		{
			Exception e2 = ex2;
			Debug.LogException(e2);
		}
		yield break;
	}
}
