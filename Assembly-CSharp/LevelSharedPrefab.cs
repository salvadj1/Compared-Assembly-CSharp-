using System;
using UnityEngine;

// Token: 0x0200059C RID: 1436
public class LevelSharedPrefab : MonoBehaviour
{
	// Token: 0x06002E88 RID: 11912 RVA: 0x000B2F4C File Offset: 0x000B114C
	private void Awake()
	{
		base.transform.DetachChildren();
		Object.Destroy(this);
	}
}
