using System;
using UnityEngine;

// Token: 0x0200041B RID: 1051
public class RPOSRepairBenchWindow : RPOSLootWindow
{
	// Token: 0x060026D4 RID: 9940 RVA: 0x000978DC File Offset: 0x00095ADC
	protected override void WindowAwake()
	{
		base.WindowAwake();
		UIEventListener uieventListener = UIEventListener.Get(this.repairButton.gameObject);
		UIEventListener uieventListener2 = uieventListener;
		uieventListener2.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uieventListener2.onClick, new UIEventListener.VoidDelegate(this.RepairButtonClicked));
		this.ClearRepairItem();
	}

	// Token: 0x060026D5 RID: 9941 RVA: 0x00097928 File Offset: 0x00095B28
	private void RepairButtonClicked(GameObject go)
	{
		if (this._benchItem != null)
		{
			NetCull.RPC(this._bench, "DoRepair", 0);
		}
	}

	// Token: 0x060026D6 RID: 9942 RVA: 0x00097948 File Offset: 0x00095B48
	public void Update()
	{
		IInventoryItem repairItem = null;
		if (this._bench)
		{
			this._bench.GetComponent<Inventory>().GetItem(0, out repairItem);
		}
		this.SetRepairItem(repairItem);
	}

	// Token: 0x060026D7 RID: 9943 RVA: 0x00097984 File Offset: 0x00095B84
	public void SetRepairItem(IInventoryItem item)
	{
		if (item == null || !item.datablock.isRepairable)
		{
			this.ClearRepairItem();
			return;
		}
		this._benchItem = item;
		this.UpdateGUIAmounts();
	}

	// Token: 0x060026D8 RID: 9944 RVA: 0x000979BC File Offset: 0x00095BBC
	public void ClearRepairItem()
	{
		this._benchItem = null;
		this.UpdateGUIAmounts();
	}

	// Token: 0x060026D9 RID: 9945 RVA: 0x000979CC File Offset: 0x00095BCC
	public void UpdateGUIAmounts()
	{
		if (this._benchItem == null)
		{
			foreach (UILabel uilabel in this._amountLabels)
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
			Controllable controllable = PlayerClient.GetLocalPlayer().controllable;
			if (controllable == null)
			{
				return;
			}
			Inventory component = controllable.GetComponent<Inventory>();
			int num = 0;
			if (this._benchItem.IsDamaged())
			{
				BlueprintDataBlock blueprintDataBlock;
				if (BlueprintDataBlock.FindBlueprintForItem<BlueprintDataBlock>(this._benchItem.datablock, out blueprintDataBlock))
				{
					for (int j = 0; j < blueprintDataBlock.ingredients.Length; j++)
					{
						if (num >= this._amountLabels.Length)
						{
							break;
						}
						BlueprintDataBlock.IngredientEntry ingredientEntry = blueprintDataBlock.ingredients[j];
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
				foreach (UILabel uilabel2 in this._amountLabels)
				{
					uilabel2.text = string.Empty;
					uilabel2.color = Color.white;
				}
			}
		}
	}

	// Token: 0x060026DA RID: 9946 RVA: 0x00097D5C File Offset: 0x00095F5C
	public override void SetLootable(LootableObject lootable, bool doInit)
	{
		base.SetLootable(lootable, doInit);
		this._bench = lootable.GetComponent<RepairBench>();
	}

	// Token: 0x04001311 RID: 4881
	private RepairBench _bench;

	// Token: 0x04001312 RID: 4882
	private IInventoryItem _benchItem;

	// Token: 0x04001313 RID: 4883
	public UILabel[] _amountLabels;

	// Token: 0x04001314 RID: 4884
	public UIButton repairButton;

	// Token: 0x04001315 RID: 4885
	public UILabel conditionLabel;

	// Token: 0x04001316 RID: 4886
	public UILabel needsLabel;
}
