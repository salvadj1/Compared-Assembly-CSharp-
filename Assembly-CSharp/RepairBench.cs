using System;
using uLink;
using UnityEngine;

// Token: 0x02000667 RID: 1639
[NGCAutoAddScript]
public class RepairBench : IDLocal
{
	// Token: 0x060038F8 RID: 14584 RVA: 0x000D1664 File Offset: 0x000CF864
	public bool CanRepair(Inventory ingredientInv)
	{
		IInventoryItem repairItem = this.GetRepairItem();
		if (repairItem == null || !repairItem.datablock.isRepairable)
		{
			return false;
		}
		if (!repairItem.IsDamaged())
		{
			return false;
		}
		BlueprintDataBlock blueprintDataBlock;
		if (BlueprintDataBlock.FindBlueprintForItem<BlueprintDataBlock>(repairItem.datablock, out blueprintDataBlock))
		{
			for (int i = 0; i < blueprintDataBlock.ingredients.Length; i++)
			{
				BlueprintDataBlock.IngredientEntry ingredientEntry = blueprintDataBlock.ingredients[i];
				int num = Mathf.CeilToInt((float)blueprintDataBlock.ingredients[i].amount * this.GetResourceScalar());
				if (num > 0 && ingredientInv.CanConsume(blueprintDataBlock.ingredients[i].Ingredient, num) <= 0)
				{
					return false;
				}
			}
			return true;
		}
		return false;
	}

	// Token: 0x060038F9 RID: 14585 RVA: 0x000D171C File Offset: 0x000CF91C
	public bool CompleteRepair(Inventory ingredientInv)
	{
		if (!this.CanRepair(ingredientInv))
		{
			return false;
		}
		IInventoryItem repairItem = this.GetRepairItem();
		BlueprintDataBlock blueprintDataBlock;
		if (!BlueprintDataBlock.FindBlueprintForItem<BlueprintDataBlock>(repairItem.datablock, out blueprintDataBlock))
		{
			return false;
		}
		for (int i = 0; i < blueprintDataBlock.ingredients.Length; i++)
		{
			BlueprintDataBlock.IngredientEntry ingredientEntry = blueprintDataBlock.ingredients[i];
			int j = Mathf.RoundToInt((float)blueprintDataBlock.ingredients[i].amount * this.GetResourceScalar());
			if (j > 0)
			{
				while (j > 0)
				{
					int num = 0;
					IInventoryItem inventoryItem = ingredientInv.FindItem(ingredientEntry.Ingredient, out num);
					if (inventoryItem == null)
					{
						return false;
					}
					if (inventoryItem.Consume(ref j))
					{
						ingredientInv.RemoveItem(inventoryItem.slot);
					}
				}
			}
		}
		float num2 = repairItem.maxcondition - repairItem.condition;
		float num3 = num2 * 0.2f + 0.05f;
		repairItem.SetMaxCondition(repairItem.maxcondition - num3);
		repairItem.SetCondition(repairItem.maxcondition);
		return true;
	}

	// Token: 0x060038FA RID: 14586 RVA: 0x000D1820 File Offset: 0x000CFA20
	public IInventoryItem GetRepairItem()
	{
		IInventoryItem result;
		base.GetComponent<Inventory>().GetItem(0, out result);
		return result;
	}

	// Token: 0x060038FB RID: 14587 RVA: 0x000D1840 File Offset: 0x000CFA40
	public bool HasRepairItem()
	{
		return this.GetRepairItem() != null;
	}

	// Token: 0x060038FC RID: 14588 RVA: 0x000D1850 File Offset: 0x000CFA50
	public float GetResourceScalar()
	{
		IInventoryItem repairItem = this.GetRepairItem();
		if (repairItem == null)
		{
			return 0f;
		}
		return (repairItem.maxcondition - repairItem.condition) * 0.5f;
	}

	// Token: 0x060038FD RID: 14589 RVA: 0x000D1884 File Offset: 0x000CFA84
	[RPC]
	protected void DoRepair(NetworkMessageInfo info)
	{
	}
}
