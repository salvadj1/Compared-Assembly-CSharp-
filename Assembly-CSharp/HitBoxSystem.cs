using System;
using Facepunch.Intersect;

// Token: 0x02000197 RID: 407
public class HitBoxSystem : HitBoxSystem
{
	// Token: 0x06000BFA RID: 3066 RVA: 0x0002FFF4 File Offset: 0x0002E1F4
	private void CheckLayer()
	{
		if (base.gameObject.layer != 17)
		{
			base.gameObject.layer = 17;
		}
	}

	// Token: 0x06000BFB RID: 3067 RVA: 0x00030020 File Offset: 0x0002E220
	protected void Awake()
	{
		base.Awake();
		this.CheckLayer();
	}

	// Token: 0x06000BFC RID: 3068 RVA: 0x00030030 File Offset: 0x0002E230
	protected void Start()
	{
		this.CheckLayer();
	}
}
