using System;
using UnityEngine;

// Token: 0x02000036 RID: 54
public class FPGrassDisplacementRadius : FPGrassDisplacementObject
{
	// Token: 0x0600020C RID: 524 RVA: 0x0000BC1C File Offset: 0x00009E1C
	public override void Initialize()
	{
		this.startScale = this.myTransform.localScale;
		this.myTransform.localScale = Vector3.zero;
	}

	// Token: 0x0600020D RID: 525 RVA: 0x0000BC40 File Offset: 0x00009E40
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

	// Token: 0x0600020E RID: 526 RVA: 0x0000BCA4 File Offset: 0x00009EA4
	public override void DetachAndDestroy()
	{
		base.transform.parent = null;
		base.SetOn(false);
		Object.Destroy(base.gameObject, 1f);
	}

	// Token: 0x04000140 RID: 320
	private Vector3 startScale;
}
