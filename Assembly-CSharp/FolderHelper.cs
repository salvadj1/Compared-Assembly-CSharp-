using System;
using UnityEngine;

// Token: 0x02000595 RID: 1429
public class FolderHelper : MonoBehaviour
{
	// Token: 0x06002E73 RID: 11891 RVA: 0x000B29D8 File Offset: 0x000B0BD8
	private void Awake()
	{
		base.transform.DetachChildren();
		Object.Destroy(base.gameObject);
	}
}
