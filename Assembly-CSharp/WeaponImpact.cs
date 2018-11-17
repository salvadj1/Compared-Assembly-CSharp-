using System;

// Token: 0x02000621 RID: 1569
public class WeaponImpact
{
	// Token: 0x06003367 RID: 13159 RVA: 0x000C1660 File Offset: 0x000BF860
	public WeaponImpact(global::WeaponDataBlock dataBlock, global::IWeaponItem item, global::ItemRepresentation itemRep)
	{
		this.dataBlock = dataBlock;
		this.item = item;
		this.itemRep = itemRep;
	}

	// Token: 0x04001B1F RID: 6943
	public readonly global::WeaponDataBlock dataBlock;

	// Token: 0x04001B20 RID: 6944
	public readonly global::ItemRepresentation itemRep;

	// Token: 0x04001B21 RID: 6945
	public readonly global::IWeaponItem item;
}
