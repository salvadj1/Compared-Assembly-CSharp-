using System;
using Facepunch;
using UnityEngine;

// Token: 0x020000AA RID: 170
public class SupplyParachute : MonoBehaviour
{
	// Token: 0x060003A5 RID: 933 RVA: 0x000115E0 File Offset: 0x0000F7E0
	private void Start()
	{
		this.targetScale = base.transform.localScale;
		base.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
	}

	// Token: 0x060003A6 RID: 934 RVA: 0x00011620 File Offset: 0x0000F820
	private void Update()
	{
		base.transform.localScale = Vector3.MoveTowards(base.transform.localScale, this.targetScale, Time.deltaTime * 2f);
		if (base.transform.localScale == this.targetScale)
		{
			base.enabled = false;
		}
	}

	// Token: 0x060003A7 RID: 935 RVA: 0x0001167C File Offset: 0x0000F87C
	public void Landed()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x0400031D RID: 797
	[NonSerialized]
	private Vector3 targetScale;
}
