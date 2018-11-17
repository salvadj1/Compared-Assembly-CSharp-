using System;

// Token: 0x0200066E RID: 1646
public interface IArmorItem : global::IEquipmentItem, global::IInventoryItem
{
	// Token: 0x060038A2 RID: 14498
	void ArmorUpdate(global::Inventory belongInv, int belongSlot);
}
