using System;

// Token: 0x02000563 RID: 1379
public class WeaponImpact
{
	// Token: 0x06002F9F RID: 12191 RVA: 0x000B9404 File Offset: 0x000B7604
	public WeaponImpact(WeaponDataBlock dataBlock, IWeaponItem item, ItemRepresentation itemRep)
	{
		this.dataBlock = dataBlock;
		this.item = item;
		this.itemRep = itemRep;
	}

	// Token: 0x0400194E RID: 6478
	public readonly WeaponDataBlock dataBlock;

	// Token: 0x0400194F RID: 6479
	public readonly ItemRepresentation itemRep;

	// Token: 0x04001950 RID: 6480
	public readonly IWeaponItem item;
}
