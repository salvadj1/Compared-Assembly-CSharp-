using System;

// Token: 0x020006BD RID: 1725
public class SilencerModRep : global::WeaponModRep
{
	// Token: 0x06003AAB RID: 15019 RVA: 0x000CDE5C File Offset: 0x000CC05C
	protected SilencerModRep(global::ItemModRepresentation.Caps caps, bool defaultOn) : base(caps, defaultOn)
	{
	}

	// Token: 0x06003AAC RID: 15020 RVA: 0x000CDE68 File Offset: 0x000CC068
	public SilencerModRep() : this((global::ItemModRepresentation.Caps)0, true)
	{
	}

	// Token: 0x06003AAD RID: 15021 RVA: 0x000CDE74 File Offset: 0x000CC074
	protected SilencerModRep(global::ItemModRepresentation.Caps caps) : this(caps, true)
	{
	}

	// Token: 0x06003AAE RID: 15022 RVA: 0x000CDE80 File Offset: 0x000CC080
	protected override void EnableMod(global::ItemModRepresentation.Reason reason)
	{
	}

	// Token: 0x06003AAF RID: 15023 RVA: 0x000CDE84 File Offset: 0x000CC084
	protected override void DisableMod(global::ItemModRepresentation.Reason reason)
	{
	}
}
