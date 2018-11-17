using System;
using UnityEngine;

// Token: 0x020004C7 RID: 1223
public class RPOSCraftWindow : global::RPOSWindowScrollable
{
	// Token: 0x06002A07 RID: 10759 RVA: 0x0009B9A8 File Offset: 0x00099BA8
	public new void Awake()
	{
		this.ShowCraftingOptions(false);
		global::UIEventListener uieventListener = global::UIEventListener.Get(this.craftButton.gameObject);
		global::UIEventListener uieventListener2 = uieventListener;
		uieventListener2.onClick = (global::UIEventListener.VoidDelegate)Delegate.Combine(uieventListener2.onClick, new global::UIEventListener.VoidDelegate(this.CraftButtonClicked));
		global::UIEventListener uieventListener3 = global::UIEventListener.Get(this.plusButton.gameObject);
		global::UIEventListener uieventListener4 = uieventListener3;
		uieventListener4.onClick = (global::UIEventListener.VoidDelegate)Delegate.Combine(uieventListener4.onClick, new global::UIEventListener.VoidDelegate(this.PlusButtonClicked));
		global::UIEventListener uieventListener5 = global::UIEventListener.Get(this.minusButton.gameObject);
		global::UIEventListener uieventListener6 = uieventListener5;
		uieventListener6.onClick = (global::UIEventListener.VoidDelegate)Delegate.Combine(uieventListener6.onClick, new global::UIEventListener.VoidDelegate(this.MinusButtonClicked));
		this.amountInput.text = "1";
	}

	// Token: 0x06002A08 RID: 10760 RVA: 0x0009BA68 File Offset: 0x00099C68
	public char ValidateAmountInput(string text, char ch)
	{
		Debug.Log("validating input");
		if (text.Length == 0 && ch == '0')
		{
			return '\0';
		}
		if (ch >= '0' && ch <= '9')
		{
			return ch;
		}
		return '\0';
	}

	// Token: 0x06002A09 RID: 10761 RVA: 0x0009BAA8 File Offset: 0x00099CA8
	public void ItemHovered(GameObject go, bool what)
	{
	}

	// Token: 0x17000952 RID: 2386
	// (get) Token: 0x06002A0A RID: 10762 RVA: 0x0009BAAC File Offset: 0x00099CAC
	private static int amountModifier
	{
		get
		{
			try
			{
				Event current = Event.current;
				if (current.control)
				{
					return 32767;
				}
				if (current.shift)
				{
					return 10;
				}
			}
			catch
			{
			}
			return 1;
		}
	}

	// Token: 0x06002A0B RID: 10763 RVA: 0x0009BB14 File Offset: 0x00099D14
	public void MinusButtonClicked(GameObject go)
	{
		this.PlusMinusClick(-global::RPOSCraftWindow.amountModifier);
	}

	// Token: 0x06002A0C RID: 10764 RVA: 0x0009BB24 File Offset: 0x00099D24
	public void PlusButtonClicked(GameObject go)
	{
		this.PlusMinusClick(global::RPOSCraftWindow.amountModifier);
	}

	// Token: 0x06002A0D RID: 10765 RVA: 0x0009BB34 File Offset: 0x00099D34
	public void SetRequestedAmount(int amount)
	{
		if (!this.selectedItem)
		{
			this.desiredAmount = amount;
		}
		else
		{
			int num = this.selectedItem.MaxAmount(global::RPOS.ObservedPlayer.GetComponent<global::Inventory>());
			this.desiredAmount = Mathf.Clamp(amount, 1, (num > 0) ? num : 1);
		}
		this.amountInput.text = this.desiredAmount.ToString();
	}

	// Token: 0x06002A0E RID: 10766 RVA: 0x0009BBA4 File Offset: 0x00099DA4
	public void PlusMinusClick(int amount)
	{
		if (amount == 0)
		{
			return;
		}
		global::CraftingInventory component = global::RPOS.ObservedPlayer.GetComponent<global::CraftingInventory>();
		if (component == null)
		{
			return;
		}
		if (component.isCrafting)
		{
			return;
		}
		this.SetRequestedAmount(this.desiredAmount + amount);
		this.UpdateIngredients();
	}

	// Token: 0x06002A0F RID: 10767 RVA: 0x0009BBF0 File Offset: 0x00099DF0
	public void ShowCraftingOptions(bool show)
	{
		this.plusButton.gameObject.SetActive(show);
		this.minusButton.gameObject.SetActive(show);
		this.amountInput.gameObject.SetActive(show);
		this.amountInputBackground.gameObject.SetActive(show);
		this.craftProgressBar.gameObject.SetActive(show);
		this.craftButton.gameObject.SetActive(show);
		this.requirementLabel.gameObject.SetActive(show);
	}

	// Token: 0x06002A10 RID: 10768 RVA: 0x0009BC74 File Offset: 0x00099E74
	public void LocalInventoryModified()
	{
		this.bpLister.UpdateItems();
		this.UpdateIngredients();
	}

	// Token: 0x06002A11 RID: 10769 RVA: 0x0009BC88 File Offset: 0x00099E88
	protected override void OnWindowShow()
	{
		this.bpLister.UpdateItems();
		this.SetRequestedAmount(1);
		base.OnWindowShow();
	}

	// Token: 0x06002A12 RID: 10770 RVA: 0x0009BCA4 File Offset: 0x00099EA4
	protected override void OnWindowHide()
	{
		base.OnWindowHide();
	}

	// Token: 0x06002A13 RID: 10771 RVA: 0x0009BCAC File Offset: 0x00099EAC
	public void Update()
	{
		if (global::RPOS.ObservedPlayer == null)
		{
			return;
		}
		global::CraftingInventory component = global::RPOS.ObservedPlayer.GetComponent<global::CraftingInventory>();
		if (component == null)
		{
			return;
		}
		bool isCrafting = component.isCrafting;
		if (isCrafting)
		{
			component.CraftThink();
		}
		if (!isCrafting && this.wasCrafting)
		{
			this.UpdateIngredients();
		}
		else if (!this.wasCrafting && isCrafting)
		{
			this.craftSound.Play();
		}
		if (this.craftButton.gameObject.activeSelf)
		{
			this.craftButton.GetComponentInChildren<global::UILabel>().text = ((!component.isCrafting) ? "Craft" : "Cancel");
		}
		if (this.craftProgressBar && this.craftProgressBar.gameObject && this.craftProgressBar.gameObject.activeSelf)
		{
			global::UISlider uislider = this.craftProgressBar;
			float? craftingCompletePercent = component.craftingCompletePercent;
			uislider.sliderValue = ((craftingCompletePercent == null) ? 0f : craftingCompletePercent.Value);
			float? craftingSecondsRemaining = component.craftingSecondsRemaining;
			float num = (craftingSecondsRemaining == null) ? 0f : craftingSecondsRemaining.Value;
			if (num != this._lastTimeStringValue)
			{
				this._lastTimeStringString = num.ToString("0.0");
				this._lastTimeStringValue = num;
			}
			this.progressLabel.text = this._lastTimeStringString;
			Color color = Color.white;
			float craftingSpeedPerSec = component.craftingSpeedPerSec;
			if (craftingSpeedPerSec > 1f)
			{
				color = Color.green;
			}
			else if (craftingSpeedPerSec < 1f)
			{
				color = Color.yellow;
			}
			else if (craftingSpeedPerSec < 0.5f)
			{
				color = Color.red;
			}
			this.progressLabel.color = color;
		}
		if (this.selectedItem != null)
		{
			this.UpdateWorkbenchRequirements();
		}
		if (this.progressLabel)
		{
			this.progressLabel.enabled = isCrafting;
		}
		this.wasCrafting = component.isCrafting;
	}

	// Token: 0x06002A14 RID: 10772 RVA: 0x0009BECC File Offset: 0x0009A0CC
	public void CraftButtonClicked(GameObject go)
	{
		if (this.selectedItem == null)
		{
			return;
		}
		Debug.Log("Crafting clicked");
		global::CraftingInventory component = global::RPOS.ObservedPlayer.GetComponent<global::CraftingInventory>();
		if (component == null)
		{
			Debug.Log("No local player inventory.. weird");
			return;
		}
		if (component.isCrafting)
		{
			component.CancelCrafting();
		}
		else if (component.ValidateCraftRequirements(this.selectedItem))
		{
			component.StartCrafting(this.selectedItem, this.RequestedAmount());
		}
	}

	// Token: 0x06002A15 RID: 10773 RVA: 0x0009BF54 File Offset: 0x0009A154
	public bool AtWorkbench()
	{
		global::CraftingInventory component = global::RPOS.ObservedPlayer.GetComponent<global::CraftingInventory>();
		return component.AtWorkBench();
	}

	// Token: 0x06002A16 RID: 10774 RVA: 0x0009BF74 File Offset: 0x0009A174
	public void UpdateWorkbenchRequirements()
	{
		if (this.selectedItem != null && this.selectedItem.RequireWorkbench)
		{
			this.requirementLabel.color = ((!this.AtWorkbench()) ? Color.red : Color.green);
			this.requirementLabel.text = "REQUIRES WORKBENCH";
		}
		else
		{
			this.requirementLabel.text = string.Empty;
		}
	}

	// Token: 0x06002A17 RID: 10775 RVA: 0x0009BFEC File Offset: 0x0009A1EC
	public void SetSelectedItem(global::BlueprintDataBlock newSel)
	{
		if (this.selectedItem)
		{
		}
		this.selectedItem = newSel;
		this.SetRequestedAmount(1);
		if (this.selectedItem)
		{
		}
		this.ShowCraftingOptions(this.selectedItem != null);
		this.UpdateWorkbenchRequirements();
	}

	// Token: 0x06002A18 RID: 10776 RVA: 0x0009C040 File Offset: 0x0009A240
	public int RequestedAmount()
	{
		return this.desiredAmount;
	}

	// Token: 0x06002A19 RID: 10777 RVA: 0x0009C048 File Offset: 0x0009A248
	public void UpdateIngredients()
	{
		if (this.selectedItem)
		{
			foreach (object obj in this.ingredientAnchor.transform)
			{
				Object.Destroy((obj as Transform).gameObject);
			}
			int num = this.RequestedAmount();
			int num2 = 0;
			foreach (global::BlueprintDataBlock.IngredientEntry ingredientEntry in this.selectedItem.ingredients)
			{
				int haveAmount = 0;
				global::PlayerClient.GetLocalPlayer().controllable.GetComponent<global::CraftingInventory>().FindItem(ingredientEntry.Ingredient, out haveAmount);
				int needAmount = ingredientEntry.amount * num;
				GameObject gameObject = global::NGUITools.AddChild(this.ingredientAnchor, this.ingredientPlaquePrefab);
				gameObject.GetComponent<global::RPOS_Craft_IngredientPlaque>().Bind(ingredientEntry, needAmount, haveAmount);
				gameObject.transform.SetLocalPositionY((float)num2);
				num2 -= 12;
			}
		}
	}

	// Token: 0x06002A1A RID: 10778 RVA: 0x0009C170 File Offset: 0x0009A370
	public void ItemClicked(GameObject go)
	{
		if (global::RPOS.ObservedPlayer.GetComponent<global::CraftingInventory>().isCrafting)
		{
			return;
		}
		global::RPOSCraftItemEntry component = go.GetComponent<global::RPOSCraftItemEntry>();
		if (component == null)
		{
			return;
		}
		global::BlueprintDataBlock blueprint = component.blueprint;
		if (!blueprint)
		{
			Debug.Log("no bp by that name");
			return;
		}
		if (blueprint != this.selectedItem)
		{
			this.SetSelectedItem(component.blueprint);
			this.UpdateIngredients();
		}
	}

	// Token: 0x0400144D RID: 5197
	public GameObject ingredientAnchor;

	// Token: 0x0400144E RID: 5198
	public GameObject ingredientPlaquePrefab;

	// Token: 0x0400144F RID: 5199
	public global::BlueprintDataBlock selectedItem;

	// Token: 0x04001450 RID: 5200
	public global::UIButton craftButton;

	// Token: 0x04001451 RID: 5201
	public global::RPOS_Craft_BlueprintList bpLister;

	// Token: 0x04001452 RID: 5202
	public global::UISlider craftProgressBar;

	// Token: 0x04001453 RID: 5203
	public global::UILabel amountInput;

	// Token: 0x04001454 RID: 5204
	public global::UISprite amountInputBackground;

	// Token: 0x04001455 RID: 5205
	public global::UIButton plusButton;

	// Token: 0x04001456 RID: 5206
	public global::UIButton minusButton;

	// Token: 0x04001457 RID: 5207
	public global::UILabel progressLabel;

	// Token: 0x04001458 RID: 5208
	public global::UILabel requirementLabel;

	// Token: 0x04001459 RID: 5209
	public int desiredAmount = 1;

	// Token: 0x0400145A RID: 5210
	private bool wasCrafting;

	// Token: 0x0400145B RID: 5211
	public AudioClip craftSound;

	// Token: 0x0400145C RID: 5212
	[NonSerialized]
	private float _lastTimeStringValue = float.PositiveInfinity;

	// Token: 0x0400145D RID: 5213
	[NonSerialized]
	private string _lastTimeStringString;
}
