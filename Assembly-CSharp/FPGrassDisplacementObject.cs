using System;
using UnityEngine;

// Token: 0x02000035 RID: 53
public class FPGrassDisplacementObject : MonoBehaviour
{
	// Token: 0x06000204 RID: 516 RVA: 0x0000BBC0 File Offset: 0x00009DC0
	public void Awake()
	{
		this.myTransform = base.transform;
		this.Initialize();
	}

	// Token: 0x06000205 RID: 517 RVA: 0x0000BBD4 File Offset: 0x00009DD4
	public virtual void Initialize()
	{
	}

	// Token: 0x06000206 RID: 518 RVA: 0x0000BBD8 File Offset: 0x00009DD8
	public void SetDepressionAmount(float percent)
	{
		this.targetDepressionPercent = percent;
	}

	// Token: 0x06000207 RID: 519 RVA: 0x0000BBE4 File Offset: 0x00009DE4
	public void SetOn(bool on)
	{
		this.targetDepressionPercent = ((!on) ? 0f : 1f);
	}

	// Token: 0x06000208 RID: 520 RVA: 0x0000BC04 File Offset: 0x00009E04
	public void Update()
	{
		this.UpdateDepression();
	}

	// Token: 0x06000209 RID: 521 RVA: 0x0000BC0C File Offset: 0x00009E0C
	public virtual void UpdateDepression()
	{
	}

	// Token: 0x0600020A RID: 522 RVA: 0x0000BC10 File Offset: 0x00009E10
	public virtual void DetachAndDestroy()
	{
	}

	// Token: 0x0400013D RID: 317
	protected Transform myTransform;

	// Token: 0x0400013E RID: 318
	protected float currentDepressionPercent;

	// Token: 0x0400013F RID: 319
	protected float targetDepressionPercent = 1f;
}
