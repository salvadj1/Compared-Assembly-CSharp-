using System;
using UnityEngine;

// Token: 0x020004C8 RID: 1224
[RequireComponent(typeof(global::UICamera))]
public class RPOSFilter : MonoBehaviour
{
	// Token: 0x06002A1C RID: 10780 RVA: 0x0009C1F0 File Offset: 0x0009A3F0
	private void Awake()
	{
		this.uicamera = base.GetComponent<global::UICamera>();
	}

	// Token: 0x06002A1D RID: 10781 RVA: 0x0009C200 File Offset: 0x0009A400
	private void OnPreCull()
	{
		global::RPOS.BeforeRPOSRender_Internal(this.uicamera);
	}

	// Token: 0x0400145E RID: 5214
	private global::UICamera uicamera;
}
