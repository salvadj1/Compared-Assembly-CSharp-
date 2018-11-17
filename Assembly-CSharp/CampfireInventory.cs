using System;

// Token: 0x02000518 RID: 1304
public class CampfireInventory : Inventory, IServerSaveable, FixedSizeInventory
{
	// Token: 0x170009AC RID: 2476
	// (get) Token: 0x06002BE3 RID: 11235 RVA: 0x000AF72C File Offset: 0x000AD92C
	public int fixedSlotCount
	{
		get
		{
			return 8;
		}
	}

	// Token: 0x06002BE4 RID: 11236 RVA: 0x000AF730 File Offset: 0x000AD930
	protected override void ConfigureSlots(int totalCount, ref Inventory.Slot.KindDictionary<Inventory.Slot.Range> ranges, ref Inventory.SlotFlags[] flags)
	{
		Inventory.Slot.KindDictionary<Inventory.Slot.Range> kindDictionary = default(Inventory.Slot.KindDictionary<Inventory.Slot.Range>);
		kindDictionary[Inventory.Slot.Kind.Belt] = new Inventory.Slot.Range(0, 3);
		kindDictionary[Inventory.Slot.Kind.Default] = new Inventory.Slot.Range(3, totalCount - 3);
		ranges = kindDictionary;
		Inventory.SlotFlags[] array = new Inventory.SlotFlags[totalCount];
		for (int i = 0; i < 3; i++)
		{
			array[i] |= Inventory.SlotFlags.Cooked;
		}
		for (int j = 3; j < 6; j++)
		{
			array[j] |= Inventory.SlotFlags.Raw;
		}
		array[6] |= Inventory.SlotFlags.FuelBasic;
		array[7] |= Inventory.SlotFlags.Debris;
		flags = array;
	}

	// Token: 0x06002BE5 RID: 11237 RVA: 0x000AF7DC File Offset: 0x000AD9DC
	protected override bool CheckSlotFlags(Inventory.SlotFlags itemSlotFlags, Inventory.SlotFlags slotFlags)
	{
		return (itemSlotFlags & slotFlags) != (Inventory.SlotFlags)0;
	}
}
