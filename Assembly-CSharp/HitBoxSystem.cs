using System;
using Facepunch.Intersect;

// Token: 0x020001C5 RID: 453
public class HitBoxSystem : Facepunch.Intersect.HitBoxSystem
{
	// Token: 0x06000D32 RID: 3378 RVA: 0x00033EE0 File Offset: 0x000320E0
	private void CheckLayer()
	{
		if (base.gameObject.layer != 17)
		{
			base.gameObject.layer = 17;
		}
	}

	// Token: 0x06000D33 RID: 3379 RVA: 0x00033F0C File Offset: 0x0003210C
	protected void Awake()
	{
		base.Awake();
		this.CheckLayer();
	}

	// Token: 0x06000D34 RID: 3380 RVA: 0x00033F1C File Offset: 0x0003211C
	protected void Start()
	{
		this.CheckLayer();
	}
}
