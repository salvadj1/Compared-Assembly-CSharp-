using System;
using UnityEngine;

// Token: 0x020004E1 RID: 1249
public class LevelSharedPrefab : MonoBehaviour
{
	// Token: 0x06002AD6 RID: 10966 RVA: 0x000AB1B4 File Offset: 0x000A93B4
	private void Awake()
	{
		base.transform.DetachChildren();
		Object.Destroy(this);
	}
}
