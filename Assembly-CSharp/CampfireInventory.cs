using System;

// Token: 0x020005D5 RID: 1493
public class CampfireInventory : global::Inventory, global::IServerSaveable, global::FixedSizeInventory
{
	// Token: 0x17000A20 RID: 2592
	// (get) Token: 0x06002FA3 RID: 12195 RVA: 0x000B77C8 File Offset: 0x000B59C8
	public int fixedSlotCount
	{
		get
		{
			return 8;
		}
	}

	// Token: 0x06002FA4 RID: 12196 RVA: 0x000B77CC File Offset: 0x000B59CC
	protected override void ConfigureSlots(int totalCount, ref global::Inventory.Slot.KindDictionary<global::Inventory.Slot.Range> ranges, ref global::Inventory.SlotFlags[] flags)
	{
		global::Inventory.Slot.KindDictionary<global::Inventory.Slot.Range> kindDictionary = default(global::Inventory.Slot.KindDictionary<global::Inventory.Slot.Range>);
		kindDictionary[global::Inventory.Slot.Kind.Belt] = new global::Inventory.Slot.Range(0, 3);
		kindDictionary[global::Inventory.Slot.Kind.Default] = new global::Inventory.Slot.Range(3, totalCount - 3);
		ranges = kindDictionary;
		global::Inventory.SlotFlags[] array = new global::Inventory.SlotFlags[totalCount];
		for (int i = 0; i < 3; i++)
		{
			array[i] |= global::Inventory.SlotFlags.Cooked;
		}
		for (int j = 3; j < 6; j++)
		{
			array[j] |= global::Inventory.SlotFlags.Raw;
		}
		array[6] |= global::Inventory.SlotFlags.FuelBasic;
		array[7] |= global::Inventory.SlotFlags.Debris;
		flags = array;
	}

	// Token: 0x06002FA5 RID: 12197 RVA: 0x000B7878 File Offset: 0x000B5A78
	protected override bool CheckSlotFlags(global::Inventory.SlotFlags itemSlotFlags, global::Inventory.SlotFlags slotFlags)
	{
		return (itemSlotFlags & slotFlags) != (global::Inventory.SlotFlags)0;
	}
}
