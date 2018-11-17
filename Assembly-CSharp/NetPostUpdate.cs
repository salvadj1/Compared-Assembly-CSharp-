using System;
using UnityEngine;

// Token: 0x02000312 RID: 786
internal class NetPostUpdate : MonoBehaviour
{
	// Token: 0x06001E4B RID: 7755 RVA: 0x00076F58 File Offset: 0x00075158
	private void Awake()
	{
		NetCull.Callbacks.BindUpdater(this);
	}

	// Token: 0x06001E4C RID: 7756 RVA: 0x00076F60 File Offset: 0x00075160
	private void OnDestroy()
	{
		NetCull.Callbacks.ResignUpdater(this);
	}

	// Token: 0x06001E4D RID: 7757 RVA: 0x00076F68 File Offset: 0x00075168
	protected void LateUpdate()
	{
		if (Application.isPlaying)
		{
			NetCull.Callbacks.FirePostUpdate(this);
		}
	}
}
