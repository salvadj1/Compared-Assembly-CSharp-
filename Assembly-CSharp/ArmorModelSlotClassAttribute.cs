using System;

// Token: 0x020005CB RID: 1483
[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class ArmorModelSlotClassAttribute : Attribute
{
	// Token: 0x06002F89 RID: 12169 RVA: 0x000B732C File Offset: 0x000B552C
	public ArmorModelSlotClassAttribute(global::ArmorModelSlot slot)
	{
		this.ArmorModelSlot = slot;
	}

	// Token: 0x040019B4 RID: 6580
	public readonly global::ArmorModelSlot ArmorModelSlot;
}
