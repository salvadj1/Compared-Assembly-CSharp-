using System;
using UnityEngine;

// Token: 0x02000047 RID: 71
public class FPGrassDisplacementObject : MonoBehaviour
{
	// Token: 0x06000276 RID: 630 RVA: 0x0000D168 File Offset: 0x0000B368
	public void Awake()
	{
		this.myTransform = base.transform;
		this.Initialize();
	}

	// Token: 0x06000277 RID: 631 RVA: 0x0000D17C File Offset: 0x0000B37C
	public virtual void Initialize()
	{
	}

	// Token: 0x06000278 RID: 632 RVA: 0x0000D180 File Offset: 0x0000B380
	public void SetDepressionAmount(float percent)
	{
		this.targetDepressionPercent = percent;
	}

	// Token: 0x06000279 RID: 633 RVA: 0x0000D18C File Offset: 0x0000B38C
	public void SetOn(bool on)
	{
		this.targetDepressionPercent = ((!on) ? 0f : 1f);
	}

	// Token: 0x0600027A RID: 634 RVA: 0x0000D1AC File Offset: 0x0000B3AC
	public void Update()
	{
		this.UpdateDepression();
	}

	// Token: 0x0600027B RID: 635 RVA: 0x0000D1B4 File Offset: 0x0000B3B4
	public virtual void UpdateDepression()
	{
	}

	// Token: 0x0600027C RID: 636 RVA: 0x0000D1B8 File Offset: 0x0000B3B8
	public virtual void DetachAndDestroy()
	{
	}

	// Token: 0x0400019F RID: 415
	protected Transform myTransform;

	// Token: 0x040001A0 RID: 416
	protected float currentDepressionPercent;

	// Token: 0x040001A1 RID: 417
	protected float targetDepressionPercent = 1f;
}
