using System;

// Token: 0x020005C4 RID: 1476
public interface IEquipmentItem : IInventoryItem
{
	// Token: 0x06003552 RID: 13650
	void OnUnEquipped();

	// Token: 0x06003553 RID: 13651
	void OnEquipped();
}
