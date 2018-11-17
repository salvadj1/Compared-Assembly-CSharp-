using System;

// Token: 0x02000683 RID: 1667
public abstract class EquipmentItem<T> : global::InventoryItem<T> where T : global::EquipmentDataBlock
{
	// Token: 0x0600391C RID: 14620 RVA: 0x000CA814 File Offset: 0x000C8A14
	protected EquipmentItem(T db) : base(db)
	{
	}

	// Token: 0x0600391D RID: 14621 RVA: 0x000CA820 File Offset: 0x000C8A20
	public void OnEquipped()
	{
		T datablock = this.datablock;
		datablock.OnEquipped(this.iface as global::IEquipmentItem);
	}

	// Token: 0x0600391E RID: 14622 RVA: 0x000CA84C File Offset: 0x000C8A4C
	public void OnUnEquipped()
	{
		T datablock = this.datablock;
		datablock.OnUnEquipped(this.iface as global::IEquipmentItem);
	}
}
