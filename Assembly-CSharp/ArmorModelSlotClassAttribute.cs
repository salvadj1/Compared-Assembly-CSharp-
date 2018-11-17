using System;

// Token: 0x0200050E RID: 1294
[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class ArmorModelSlotClassAttribute : Attribute
{
	// Token: 0x06002BC9 RID: 11209 RVA: 0x000AF290 File Offset: 0x000AD490
	public ArmorModelSlotClassAttribute(ArmorModelSlot slot)
	{
		this.ArmorModelSlot = slot;
	}

	// Token: 0x040017E8 RID: 6120
	public readonly ArmorModelSlot ArmorModelSlot;
}
