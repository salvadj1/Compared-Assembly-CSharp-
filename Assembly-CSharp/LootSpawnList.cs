using System;
using UnityEngine;

// Token: 0x020005F5 RID: 1525
public class LootSpawnList : ScriptableObject
{
	// Token: 0x060036B0 RID: 14000 RVA: 0x000C51DC File Offset: 0x000C33DC
	public void PopulateInventory(Inventory inven)
	{
		LootSpawnList.RecursiveInventoryPopulateArgs recursiveInventoryPopulateArgs;
		recursiveInventoryPopulateArgs.inventory = inven;
		recursiveInventoryPopulateArgs.spawnCount = 0;
		recursiveInventoryPopulateArgs.inventoryExausted = inven.noVacantSlots;
		if (!recursiveInventoryPopulateArgs.inventoryExausted)
		{
			this.PopulateInventory_Recurse(ref recursiveInventoryPopulateArgs);
		}
	}

	// Token: 0x060036B1 RID: 14001 RVA: 0x000C521C File Offset: 0x000C341C
	private void PopulateInventory_Recurse(ref LootSpawnList.RecursiveInventoryPopulateArgs args)
	{
		if (this.maxPackagesToSpawn > this.LootPackages.Length)
		{
			this.maxPackagesToSpawn = this.LootPackages.Length;
		}
		int num;
		if (this.spawnOneOfEach)
		{
			num = this.LootPackages.Length;
		}
		else
		{
			num = Random.Range(this.minPackagesToSpawn, this.maxPackagesToSpawn);
		}
		int num2 = 0;
		while (!args.inventoryExausted && num2 < num)
		{
			LootSpawnList.LootWeightedEntry lootWeightedEntry;
			if (this.spawnOneOfEach)
			{
				lootWeightedEntry = this.LootPackages[num2];
			}
			else
			{
				lootWeightedEntry = (WeightSelection.RandomPickEntry(this.LootPackages) as LootSpawnList.LootWeightedEntry);
			}
			if (lootWeightedEntry == null)
			{
				Debug.Log("Massive fuckup...");
				return;
			}
			Object obj = lootWeightedEntry.obj;
			if (obj)
			{
				if (obj is ItemDataBlock)
				{
					ItemDataBlock datablock = obj as ItemDataBlock;
					if (!object.ReferenceEquals(args.inventory.AddItem(datablock, Inventory.Slot.Preference.Define(args.spawnCount, false, Inventory.Slot.KindFlags.Default | Inventory.Slot.KindFlags.Belt), Random.Range(lootWeightedEntry.amountMin, lootWeightedEntry.amountMax + 1)), null))
					{
						args.spawnCount++;
						if (args.inventory.noVacantSlots)
						{
							args.inventoryExausted = true;
						}
					}
				}
				else if (obj is LootSpawnList)
				{
					((LootSpawnList)obj).PopulateInventory_Recurse(ref args);
				}
			}
			num2++;
		}
	}

	// Token: 0x04001AD6 RID: 6870
	public LootSpawnList.LootWeightedEntry[] LootPackages;

	// Token: 0x04001AD7 RID: 6871
	public int minPackagesToSpawn = 1;

	// Token: 0x04001AD8 RID: 6872
	public int maxPackagesToSpawn = 1;

	// Token: 0x04001AD9 RID: 6873
	public bool noDuplicates;

	// Token: 0x04001ADA RID: 6874
	public bool spawnOneOfEach;

	// Token: 0x020005F6 RID: 1526
	[Serializable]
	public class LootWeightedEntry : WeightSelection.WeightedEntry
	{
		// Token: 0x04001ADB RID: 6875
		public int amountMin;

		// Token: 0x04001ADC RID: 6876
		public int amountMax = 1;
	}

	// Token: 0x020005F7 RID: 1527
	private struct RecursiveInventoryPopulateArgs
	{
		// Token: 0x04001ADD RID: 6877
		public Inventory inventory;

		// Token: 0x04001ADE RID: 6878
		public int spawnCount;

		// Token: 0x04001ADF RID: 6879
		public bool inventoryExausted;
	}
}
