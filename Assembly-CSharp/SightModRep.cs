using System;

// Token: 0x020005FC RID: 1532
public class SightModRep : WeaponModRep
{
	// Token: 0x060036CE RID: 14030 RVA: 0x000C5900 File Offset: 0x000C3B00
	protected SightModRep(ItemModRepresentation.Caps caps, bool defaultOn) : base(caps, defaultOn)
	{
	}

	// Token: 0x060036CF RID: 14031 RVA: 0x000C590C File Offset: 0x000C3B0C
	public SightModRep() : this((ItemModRepresentation.Caps)0, true)
	{
	}

	// Token: 0x060036D0 RID: 14032 RVA: 0x000C5918 File Offset: 0x000C3B18
	protected SightModRep(ItemModRepresentation.Caps caps) : this(caps, true)
	{
	}

	// Token: 0x060036D1 RID: 14033 RVA: 0x000C5924 File Offset: 0x000C3B24
	protected override void EnableMod(ItemModRepresentation.Reason reason)
	{
	}

	// Token: 0x060036D2 RID: 14034 RVA: 0x000C5928 File Offset: 0x000C3B28
	protected override void DisableMod(ItemModRepresentation.Reason reason)
	{
	}
}
