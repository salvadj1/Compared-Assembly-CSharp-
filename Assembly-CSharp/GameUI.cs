using System;
using UnityEngine;

// Token: 0x0200049B RID: 1179
public class GameUI : MonoBehaviour
{
	// Token: 0x0600288B RID: 10379 RVA: 0x00094504 File Offset: 0x00092704
	private void Awake()
	{
		Object.DontDestroyOnLoad(this);
		Debug.Log("GameUI Loaded");
	}
}
