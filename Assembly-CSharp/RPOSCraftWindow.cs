using System;
using UnityEngine;

// Token: 0x02000412 RID: 1042
public class RPOSCraftWindow : RPOSWindowScrollable
{
	// Token: 0x0600267D RID: 9853 RVA: 0x00095AE4 File Offset: 0x00093CE4
	public new void Awake()
	{
		this.ShowCraftingOptions(false);
		UIEventListener uieventListener = UIEventListener.Get(this.craftButton.gameObject);
		UIEventListener uieventListener2 = uieventListener;
		uieventListener2.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uieventListener2.onClick, new UIEventListener.VoidDelegate(this.CraftButtonClicked));
		UIEventListener uieventListener3 = UIEventListener.Get(this.plusButton.gameObject);
		UIEventListener uieventListener4 = uieventListener3;
		uieventListener4.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uieventListener4.onClick, new UIEventListener.VoidDelegate(this.PlusButtonClicked));
		UIEventListener uieventListener5 = UIEventListener.Get(this.minusButton.gameObject);
		UIEventListener uieventListener6 = uieventListener5;
		uieventListener6.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uieventListener6.onClick, new UIEventListener.VoidDelegate(this.MinusButtonClicked));
		this.amountInput.text = "1";
	}

	// Token: 0x0600267E RID: 9854 RVA: 0x00095BA4 File Offset: 0x00093DA4
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

	// Token: 0x0600267F RID: 9855 RVA: 0x00095BE4 File Offset: 0x00093DE4
	public void ItemHovered(GameObject go, bool what)
	{
	}

	// Token: 0x170008EC RID: 2284
	// (get) Token: 0x06002680 RID: 9856 RVA: 0x00095BE8 File Offset: 0x00093DE8
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

	// Token: 0x06002681 RID: 9857 RVA: 0x00095C50 File Offset: 0x00093E50
	public void MinusButtonClicked(GameObject go)
	{
		this.PlusMinusClick(-RPOSCraftWindow.amountModifier);
	}

	// Token: 0x06002682 RID: 9858 RVA: 0x00095C60 File Offset: 0x00093E60
	public void PlusButtonClicked(GameObject go)
	{
		this.PlusMinusClick(RPOSCraftWindow.amountModifier);
	}

	// Token: 0x06002683 RID: 9859 RVA: 0x00095C70 File Offset: 0x00093E70
	public void SetRequestedAmount(int amount)
	{
		if (!this.selectedItem)
		{
			this.desiredAmount = amount;
		}
		else
		{
			int num = this.selectedItem.MaxAmount(RPOS.ObservedPlayer.GetComponent<Inventory>());
			this.desiredAmount = Mathf.Clamp(amount, 1, (num > 0) ? num : 1);
		}
		this.amountInput.text = this.desiredAmount.ToString();
	}

	// Token: 0x06002684 RID: 9860 RVA: 0x00095CE0 File Offset: 0x00093EE0
	public void PlusMinusClick(int amount)
	{
		if (amount == 0)
		{
			return;
		}
		CraftingInventory component = RPOS.ObservedPlayer.GetComponent<CraftingInventory>();
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

	// Token: 0x06002685 RID: 9861 RVA: 0x00095D2C File Offset: 0x00093F2C
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

	// Token: 0x06002686 RID: 9862 RVA: 0x00095DB0 File Offset: 0x00093FB0
	public void LocalInventoryModified()
	{
		this.bpLister.UpdateItems();
		this.UpdateIngredients();
	}

	// Token: 0x06002687 RID: 9863 RVA: 0x00095DC4 File Offset: 0x00093FC4
	protected override void OnWindowShow()
	{
		this.bpLister.UpdateItems();
		this.SetRequestedAmount(1);
		base.OnWindowShow();
	}

	// Token: 0x06002688 RID: 9864 RVA: 0x00095DE0 File Offset: 0x00093FE0
	protected override void OnWindowHide()
	{
		base.OnWindowHide();
	}

	// Token: 0x06002689 RID: 9865 RVA: 0x00095DE8 File Offset: 0x00093FE8
	public void Update()
	{
		if (RPOS.ObservedPlayer == null)
		{
			return;
		}
		CraftingInventory component = RPOS.ObservedPlayer.GetComponent<CraftingInventory>();
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
			this.craftButton.GetComponentInChildren<UILabel>().text = ((!component.isCrafting) ? "Craft" : "Cancel");
		}
		if (this.craftProgressBar && this.craftProgressBar.gameObject && this.craftProgressBar.gameObject.activeSelf)
		{
			UISlider uislider = this.craftProgressBar;
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

	// Token: 0x0600268A RID: 9866 RVA: 0x00096008 File Offset: 0x00094208
	public void CraftButtonClicked(GameObject go)
	{
		if (this.selectedItem == null)
		{
			return;
		}
		Debug.Log("Crafting clicked");
		CraftingInventory component = RPOS.ObservedPlayer.GetComponent<CraftingInventory>();
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

	// Token: 0x0600268B RID: 9867 RVA: 0x00096090 File Offset: 0x00094290
	public bool AtWorkbench()
	{
		CraftingInventory component = RPOS.ObservedPlayer.GetComponent<CraftingInventory>();
		return component.AtWorkBench();
	}

	// Token: 0x0600268C RID: 9868 RVA: 0x000960B0 File Offset: 0x000942B0
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

	// Token: 0x0600268D RID: 9869 RVA: 0x00096128 File Offset: 0x00094328
	public void SetSelectedItem(BlueprintDataBlock newSel)
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

	// Token: 0x0600268E RID: 9870 RVA: 0x0009617C File Offset: 0x0009437C
	public int RequestedAmount()
	{
		return this.desiredAmount;
	}

	// Token: 0x0600268F RID: 9871 RVA: 0x00096184 File Offset: 0x00094384
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
			foreach (BlueprintDataBlock.IngredientEntry ingredientEntry in this.selectedItem.ingredients)
			{
				int haveAmount = 0;
				PlayerClient.GetLocalPlayer().controllable.GetComponent<CraftingInventory>().FindItem(ingredientEntry.Ingredient, out haveAmount);
				int needAmount = ingredientEntry.amount * num;
				GameObject gameObject = NGUITools.AddChild(this.ingredientAnchor, this.ingredientPlaquePrefab);
				gameObject.GetComponent<RPOS_Craft_IngredientPlaque>().Bind(ingredientEntry, needAmount, haveAmount);
				gameObject.transform.SetLocalPositionY((float)num2);
				num2 -= 12;
			}
		}
	}

	// Token: 0x06002690 RID: 9872 RVA: 0x000962AC File Offset: 0x000944AC
	public void ItemClicked(GameObject go)
	{
		if (RPOS.ObservedPlayer.GetComponent<CraftingInventory>().isCrafting)
		{
			return;
		}
		RPOSCraftItemEntry component = go.GetComponent<RPOSCraftItemEntry>();
		if (component == null)
		{
			return;
		}
		BlueprintDataBlock blueprint = component.blueprint;
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

	// Token: 0x040012CD RID: 4813
	public GameObject ingredientAnchor;

	// Token: 0x040012CE RID: 4814
	public GameObject ingredientPlaquePrefab;

	// Token: 0x040012CF RID: 4815
	public BlueprintDataBlock selectedItem;

	// Token: 0x040012D0 RID: 4816
	public UIButton craftButton;

	// Token: 0x040012D1 RID: 4817
	public RPOS_Craft_BlueprintList bpLister;

	// Token: 0x040012D2 RID: 4818
	public UISlider craftProgressBar;

	// Token: 0x040012D3 RID: 4819
	public UILabel amountInput;

	// Token: 0x040012D4 RID: 4820
	public UISprite amountInputBackground;

	// Token: 0x040012D5 RID: 4821
	public UIButton plusButton;

	// Token: 0x040012D6 RID: 4822
	public UIButton minusButton;

	// Token: 0x040012D7 RID: 4823
	public UILabel progressLabel;

	// Token: 0x040012D8 RID: 4824
	public UILabel requirementLabel;

	// Token: 0x040012D9 RID: 4825
	public int desiredAmount = 1;

	// Token: 0x040012DA RID: 4826
	private bool wasCrafting;

	// Token: 0x040012DB RID: 4827
	public AudioClip craftSound;

	// Token: 0x040012DC RID: 4828
	[NonSerialized]
	private float _lastTimeStringValue = float.PositiveInfinity;

	// Token: 0x040012DD RID: 4829
	[NonSerialized]
	private string _lastTimeStringString;
}
