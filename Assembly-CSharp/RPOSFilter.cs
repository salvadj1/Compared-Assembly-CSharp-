using System;
using UnityEngine;

// Token: 0x02000413 RID: 1043
[RequireComponent(typeof(UICamera))]
public class RPOSFilter : MonoBehaviour
{
	// Token: 0x06002692 RID: 9874 RVA: 0x0009632C File Offset: 0x0009452C
	private void Awake()
	{
		this.uicamera = base.GetComponent<UICamera>();
	}

	// Token: 0x06002693 RID: 9875 RVA: 0x0009633C File Offset: 0x0009453C
	private void OnPreCull()
	{
		RPOS.BeforeRPOSRender_Internal(this.uicamera);
	}

	// Token: 0x040012DE RID: 4830
	private UICamera uicamera;
}
