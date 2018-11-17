using System;

// Token: 0x020005C5 RID: 1477
public abstract class EquipmentItem<T> : InventoryItem<T> where T : EquipmentDataBlock
{
	// Token: 0x06003554 RID: 13652 RVA: 0x000C25B8 File Offset: 0x000C07B8
	protected EquipmentItem(T db) : base(db)
	{
	}

	// Token: 0x06003555 RID: 13653 RVA: 0x000C25C4 File Offset: 0x000C07C4
	public void OnEquipped()
	{
		T datablock = this.datablock;
		datablock.OnEquipped(this.iface as IEquipmentItem);
	}

	// Token: 0x06003556 RID: 13654 RVA: 0x000C25F0 File Offset: 0x000C07F0
	public void OnUnEquipped()
	{
		T datablock = this.datablock;
		datablock.OnUnEquipped(this.iface as IEquipmentItem);
	}
}
