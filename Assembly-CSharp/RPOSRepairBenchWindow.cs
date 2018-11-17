using System;
using UnityEngine;

// Token: 0x020004D0 RID: 1232
public class RPOSRepairBenchWindow : global::RPOSLootWindow
{
	// Token: 0x06002A5E RID: 10846 RVA: 0x0009D7A0 File Offset: 0x0009B9A0
	protected override void WindowAwake()
	{
		base.WindowAwake();
		global::UIEventListener uieventListener = global::UIEventListener.Get(this.repairButton.gameObject);
		global::UIEventListener uieventListener2 = uieventListener;
		uieventListener2.onClick = (global::UIEventListener.VoidDelegate)Delegate.Combine(uieventListener2.onClick, new global::UIEventListener.VoidDelegate(this.RepairButtonClicked));
		this.ClearRepairItem();
	}

	// Token: 0x06002A5F RID: 10847 RVA: 0x0009D7EC File Offset: 0x0009B9EC
	private void RepairButtonClicked(GameObject go)
	{
		if (this._benchItem != null)
		{
			global::NetCull.RPC(this._bench, "DoRepair", 0);
		}
	}

	// Token: 0x06002A60 RID: 10848 RVA: 0x0009D80C File Offset: 0x0009BA0C
	public void Update()
	{
		global::IInventoryItem repairItem = null;
		if (this._bench)
		{
			this._bench.GetComponent<global::Inventory>().GetItem(0, out repairItem);
		}
		this.SetRepairItem(repairItem);
	}

	// Token: 0x06002A61 RID: 10849 RVA: 0x0009D848 File Offset: 0x0009BA48
	public void SetRepairItem(global::IInventoryItem item)
	{
		if (item == null || !item.datablock.isRepairable)
		{
			this.ClearRepairItem();
			return;
		}
		this._benchItem = item;
		this.UpdateGUIAmounts();
	}

	// Token: 0x06002A62 RID: 10850 RVA: 0x0009D880 File Offset: 0x0009BA80
	public void ClearRepairItem()
	{
		this._benchItem = null;
		this.UpdateGUIAmounts();
	}

	// Token: 0x06002A63 RID: 10851 RVA: 0x0009D890 File Offset: 0x0009BA90
	public void UpdateGUIAmounts()
	{
		if (this._benchItem == null)
		{
			foreach (global::UILabel uilabel in this._amountLabels)
			{
				uilabel.text = string.Empty;
				uilabel.color = Color.white;
			}
			this.needsLabel.enabled = false;
			this.conditionLabel.enabled = false;
			this.repairButton.gameObject.SetActive(false);
		}
		else
		{
			global::Controllable controllable = global::PlayerClient.GetLocalPlayer().controllable;
			if (controllable == null)
			{
				return;
			}
			global::Inventory component = controllable.GetComponent<global::Inventory>();
			int num = 0;
			if (this._benchItem.IsDamaged())
			{
				global::BlueprintDataBlock blueprintDataBlock;
				if (global::BlueprintDataBlock.FindBlueprintForItem<global::BlueprintDataBlock>(this._benchItem.datablock, out blueprintDataBlock))
				{
					for (int j = 0; j < blueprintDataBlock.ingredients.Length; j++)
					{
						if (num >= this._amountLabels.Length)
						{
							break;
						}
						global::BlueprintDataBlock.IngredientEntry ingredientEntry = blueprintDataBlock.ingredients[j];
						int num2 = Mathf.CeilToInt((float)blueprintDataBlock.ingredients[j].amount * this._bench.GetResourceScalar());
						if (num2 > 0)
						{
							bool flag = component.CanConsume(blueprintDataBlock.ingredients[j].Ingredient, num2) > 0;
							this._amountLabels[num].text = num2 + " " + blueprintDataBlock.ingredients[j].Ingredient.name;
							this._amountLabels[num].color = ((!flag) ? Color.red : Color.green);
							num++;
						}
					}
				}
				this.needsLabel.color = Color.white;
				this.needsLabel.enabled = true;
				this.conditionLabel.enabled = true;
				this.repairButton.gameObject.SetActive(true);
				string str = (this._benchItem.condition * 100f).ToString("0");
				string str2 = (this._benchItem.maxcondition * 100f).ToString("0");
				this.conditionLabel.text = "Condition : " + str + "/" + str2;
				this.conditionLabel.color = ((this._benchItem.condition >= 0.6f) ? Color.green : Color.yellow);
				if (this._benchItem.IsBroken())
				{
					this.conditionLabel.color = Color.red;
				}
			}
			else
			{
				this.needsLabel.text = "Does not need repairs";
				this.needsLabel.color = Color.green;
				this.needsLabel.enabled = true;
				string str3 = (this._benchItem.condition * 100f).ToString("0");
				string str4 = (this._benchItem.maxcondition * 100f).ToString("0");
				this.conditionLabel.text = "Condition : " + str3 + "/" + str4;
				this.conditionLabel.color = Color.green;
				this.conditionLabel.enabled = true;
				this.repairButton.gameObject.SetActive(false);
				foreach (global::UILabel uilabel2 in this._amountLabels)
				{
					uilabel2.text = string.Empty;
					uilabel2.color = Color.white;
				}
			}
		}
	}

	// Token: 0x06002A64 RID: 10852 RVA: 0x0009DC20 File Offset: 0x0009BE20
	public override void SetLootable(global::LootableObject lootable, bool doInit)
	{
		base.SetLootable(lootable, doInit);
		this._bench = lootable.GetComponent<global::RepairBench>();
	}

	// Token: 0x04001491 RID: 5265
	private global::RepairBench _bench;

	// Token: 0x04001492 RID: 5266
	private global::IInventoryItem _benchItem;

	// Token: 0x04001493 RID: 5267
	public global::UILabel[] _amountLabels;

	// Token: 0x04001494 RID: 5268
	public global::UIButton repairButton;

	// Token: 0x04001495 RID: 5269
	public global::UILabel conditionLabel;

	// Token: 0x04001496 RID: 5270
	public global::UILabel needsLabel;
}
