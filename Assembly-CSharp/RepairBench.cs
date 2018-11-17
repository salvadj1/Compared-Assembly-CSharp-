using System;
using uLink;
using UnityEngine;

// Token: 0x0200072B RID: 1835
[global::NGCAutoAddScript]
public class RepairBench : IDLocal
{
	// Token: 0x06003CEC RID: 15596 RVA: 0x000DA044 File Offset: 0x000D8244
	public bool CanRepair(global::Inventory ingredientInv)
	{
		global::IInventoryItem repairItem = this.GetRepairItem();
		if (repairItem == null || !repairItem.datablock.isRepairable)
		{
			return false;
		}
		if (!repairItem.IsDamaged())
		{
			return false;
		}
		global::BlueprintDataBlock blueprintDataBlock;
		if (global::BlueprintDataBlock.FindBlueprintForItem<global::BlueprintDataBlock>(repairItem.datablock, out blueprintDataBlock))
		{
			for (int i = 0; i < blueprintDataBlock.ingredients.Length; i++)
			{
				global::BlueprintDataBlock.IngredientEntry ingredientEntry = blueprintDataBlock.ingredients[i];
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

	// Token: 0x06003CED RID: 15597 RVA: 0x000DA0FC File Offset: 0x000D82FC
	public bool CompleteRepair(global::Inventory ingredientInv)
	{
		if (!this.CanRepair(ingredientInv))
		{
			return false;
		}
		global::IInventoryItem repairItem = this.GetRepairItem();
		global::BlueprintDataBlock blueprintDataBlock;
		if (!global::BlueprintDataBlock.FindBlueprintForItem<global::BlueprintDataBlock>(repairItem.datablock, out blueprintDataBlock))
		{
			return false;
		}
		for (int i = 0; i < blueprintDataBlock.ingredients.Length; i++)
		{
			global::BlueprintDataBlock.IngredientEntry ingredientEntry = blueprintDataBlock.ingredients[i];
			int j = Mathf.RoundToInt((float)blueprintDataBlock.ingredients[i].amount * this.GetResourceScalar());
			if (j > 0)
			{
				while (j > 0)
				{
					int num = 0;
					global::IInventoryItem inventoryItem = ingredientInv.FindItem(ingredientEntry.Ingredient, out num);
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

	// Token: 0x06003CEE RID: 15598 RVA: 0x000DA200 File Offset: 0x000D8400
	public global::IInventoryItem GetRepairItem()
	{
		global::IInventoryItem result;
		base.GetComponent<global::Inventory>().GetItem(0, out result);
		return result;
	}

	// Token: 0x06003CEF RID: 15599 RVA: 0x000DA220 File Offset: 0x000D8420
	public bool HasRepairItem()
	{
		return this.GetRepairItem() != null;
	}

	// Token: 0x06003CF0 RID: 15600 RVA: 0x000DA230 File Offset: 0x000D8430
	public float GetResourceScalar()
	{
		global::IInventoryItem repairItem = this.GetRepairItem();
		if (repairItem == null)
		{
			return 0f;
		}
		return (repairItem.maxcondition - repairItem.condition) * 0.5f;
	}

	// Token: 0x06003CF1 RID: 15601 RVA: 0x000DA264 File Offset: 0x000D8464
	[RPC]
	protected void DoRepair(uLink.NetworkMessageInfo info)
	{
	}
}
