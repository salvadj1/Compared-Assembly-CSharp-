using System;

// Token: 0x020005B0 RID: 1456
public interface IArmorItem : IEquipmentItem, IInventoryItem
{
	// Token: 0x060034DA RID: 13530
	void ArmorUpdate(Inventory belongInv, int belongSlot);
}
