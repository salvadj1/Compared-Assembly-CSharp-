using System;
using System.Collections;
using Facepunch.Build;
using uLink;
using UnityEngine;

// Token: 0x02000624 RID: 1572
public class ClientInit : MonoBehaviour
{
	// Token: 0x060037B7 RID: 14263 RVA: 0x000CCB28 File Offset: 0x000CAD28
	private void Awake()
	{
		Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x060037B8 RID: 14264 RVA: 0x000CCB38 File Offset: 0x000CAD38
	private void Start()
	{
		SteamClient.Create();
		ConsoleSystem.Run("config.load", false);
		ConsoleSystem.Run("serverfavourite.load", false);
		HudEnabled.Disable();
		DatablockDictionary.Initialize();
		Application.LoadLevelAdditive("GameUI");
		Connection.GameLoaded();
	}

	// Token: 0x060037B9 RID: 14265 RVA: 0x000CCB74 File Offset: 0x000CAD74
	private IEnumerator uLink_OnDisconnectedFromServer(NetworkDisconnection netDisconnect)
	{
		yield return null;
		yield return null;
		try
		{
			SoundPool.Drain();
		}
		catch (Exception ex)
		{
			Exception e = ex;
			Debug.LogException(e);
		}
		try
		{
			DestroysOnDisconnect.OnDisconnectedFromServer();
		}
		catch (Exception ex2)
		{
			Exception e2 = ex2;
			Debug.LogException(e2);
		}
		yield break;
	}
}
