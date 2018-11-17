using System;

// Token: 0x020006BC RID: 1724
public class SightModRep : global::WeaponModRep
{
	// Token: 0x06003AA6 RID: 15014 RVA: 0x000CDE30 File Offset: 0x000CC030
	protected SightModRep(global::ItemModRepresentation.Caps caps, bool defaultOn) : base(caps, defaultOn)
	{
	}

	// Token: 0x06003AA7 RID: 15015 RVA: 0x000CDE3C File Offset: 0x000CC03C
	public SightModRep() : this((global::ItemModRepresentation.Caps)0, true)
	{
	}

	// Token: 0x06003AA8 RID: 15016 RVA: 0x000CDE48 File Offset: 0x000CC048
	protected SightModRep(global::ItemModRepresentation.Caps caps) : this(caps, true)
	{
	}

	// Token: 0x06003AA9 RID: 15017 RVA: 0x000CDE54 File Offset: 0x000CC054
	protected override void EnableMod(global::ItemModRepresentation.Reason reason)
	{
	}

	// Token: 0x06003AAA RID: 15018 RVA: 0x000CDE58 File Offset: 0x000CC058
	protected override void DisableMod(global::ItemModRepresentation.Reason reason)
	{
	}
}
