using System;
using UnityEngine;

// Token: 0x02000048 RID: 72
public class FPGrassDisplacementRadius : global::FPGrassDisplacementObject
{
	// Token: 0x0600027E RID: 638 RVA: 0x0000D1C4 File Offset: 0x0000B3C4
	public override void Initialize()
	{
		this.startScale = this.myTransform.localScale;
		this.myTransform.localScale = Vector3.zero;
	}

	// Token: 0x0600027F RID: 639 RVA: 0x0000D1E8 File Offset: 0x0000B3E8
	public override void UpdateDepression()
	{
		if (Mathf.Approximately(this.currentDepressionPercent, this.targetDepressionPercent))
		{
			return;
		}
		float currentDepressionPercent = Mathf.Lerp(this.currentDepressionPercent, this.targetDepressionPercent, Time.deltaTime * 5f);
		this.currentDepressionPercent = currentDepressionPercent;
		this.myTransform.localScale = this.startScale * this.currentDepressionPercent;
	}

	// Token: 0x06000280 RID: 640 RVA: 0x0000D24C File Offset: 0x0000B44C
	public override void DetachAndDestroy()
	{
		base.transform.parent = null;
		base.SetOn(false);
		Object.Destroy(base.gameObject, 1f);
	}

	// Token: 0x040001A2 RID: 418
	private Vector3 startScale;
}
