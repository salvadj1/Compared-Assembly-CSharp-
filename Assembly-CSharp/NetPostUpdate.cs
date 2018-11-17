using System;
using UnityEngine;

// Token: 0x020003BB RID: 955
internal class NetPostUpdate : MonoBehaviour
{
	// Token: 0x0600218D RID: 8589 RVA: 0x0007B9D8 File Offset: 0x00079BD8
	private void Awake()
	{
		global::NetCull.Callbacks.BindUpdater(this);
	}

	// Token: 0x0600218E RID: 8590 RVA: 0x0007B9E0 File Offset: 0x00079BE0
	private void OnDestroy()
	{
		global::NetCull.Callbacks.ResignUpdater(this);
	}

	// Token: 0x0600218F RID: 8591 RVA: 0x0007B9E8 File Offset: 0x00079BE8
	protected void LateUpdate()
	{
		if (Application.isPlaying)
		{
			global::NetCull.Callbacks.FirePostUpdate(this);
		}
	}
}
