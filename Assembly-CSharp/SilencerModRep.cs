using System;

// Token: 0x020005FD RID: 1533
public class SilencerModRep : WeaponModRep
{
	// Token: 0x060036D3 RID: 14035 RVA: 0x000C592C File Offset: 0x000C3B2C
	protected SilencerModRep(ItemModRepresentation.Caps caps, bool defaultOn) : base(caps, defaultOn)
	{
	}

	// Token: 0x060036D4 RID: 14036 RVA: 0x000C5938 File Offset: 0x000C3B38
	public SilencerModRep() : this((ItemModRepresentation.Caps)0, true)
	{
	}

	// Token: 0x060036D5 RID: 14037 RVA: 0x000C5944 File Offset: 0x000C3B44
	protected SilencerModRep(ItemModRepresentation.Caps caps) : this(caps, true)
	{
	}

	// Token: 0x060036D6 RID: 14038 RVA: 0x000C5950 File Offset: 0x000C3B50
	protected override void EnableMod(ItemModRepresentation.Reason reason)
	{
	}

	// Token: 0x060036D7 RID: 14039 RVA: 0x000C5954 File Offset: 0x000C3B54
	protected override void DisableMod(ItemModRepresentation.Reason reason)
	{
	}
}
