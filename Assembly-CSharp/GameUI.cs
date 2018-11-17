using System;
using UnityEngine;

// Token: 0x020003EB RID: 1003
public class GameUI : MonoBehaviour
{
	// Token: 0x06002519 RID: 9497 RVA: 0x0008EB18 File Offset: 0x0008CD18
	private void Awake()
	{
		Object.DontDestroyOnLoad(this);
		Debug.Log("GameUI Loaded");
	}
}
