using System;
using Facepunch;
using UnityEngine;

// Token: 0x02000417 RID: 1047
public class RPOSInventoryCell : MonoBehaviour
{
	// Token: 0x170008EF RID: 2287
	// (get) Token: 0x060026AE RID: 9902 RVA: 0x000969FC File Offset: 0x00094BFC
	public IInventoryItem slotItem
	{
		get
		{
			IInventoryItem result;
			if (this._displayInventory && this._displayInventory.GetItem((int)this._mySlot, out result))
			{
				return result;
			}
			return null;
		}
	}

	// Token: 0x060026AF RID: 9903 RVA: 0x00096A34 File Offset: 0x00094C34
	private void Start()
	{
		if (!RPOSInventoryCell._myMaterial)
		{
			Bundling.Load<Material>("content/item/mat/ItemIconShader", out RPOSInventoryCell._myMaterial);
		}
		this._icon.enabled = false;
		if (this.modSprites.Length > 0)
		{
			this.mod_empty = this.modSprites[0].atlas.GetSprite("slot_empty");
			this.mod_full = this.modSprites[0].atlas.GetSprite("slot_full");
		}
	}

	// Token: 0x060026B0 RID: 9904 RVA: 0x00096AB4 File Offset: 0x00094CB4
	private void Update()
	{
		if (this._displayInventory)
		{
			if (RPOS.Item_IsClickedCell(this))
			{
				this.MakeEmpty();
			}
			else
			{
				IInventoryItem inventoryItem;
				this._displayInventory.GetItem((int)this._mySlot, out inventoryItem);
				if (this._displayInventory.MarkSlotClean((int)this._mySlot) || !object.ReferenceEquals(this._myDisplayItem, inventoryItem))
				{
					this.SetItem(inventoryItem);
				}
			}
			if (!RPOS.IsOpen && this._darkener)
			{
				if (this.backupColor == Color.cyan)
				{
					this.backupColor = this._darkener.color;
				}
				if (this._myDisplayItem != null && this._displayInventory._activeItem == this._myDisplayItem)
				{
					this._darkener.color = Color.grey;
				}
				else
				{
					this._darkener.color = this.backupColor;
				}
			}
		}
	}

	// Token: 0x060026B1 RID: 9905 RVA: 0x00096BB0 File Offset: 0x00094DB0
	public void SetItemLocked(bool locked)
	{
		this._locked = locked;
		if (this._locked)
		{
			this._icon.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
		}
		else
		{
			this._icon.color = Color.white;
		}
	}

	// Token: 0x060026B2 RID: 9906 RVA: 0x00096C08 File Offset: 0x00094E08
	public bool IsItemLocked()
	{
		return this._locked;
	}

	// Token: 0x060026B3 RID: 9907 RVA: 0x00096C10 File Offset: 0x00094E10
	private void MakeEmpty()
	{
		this._myDisplayItem = null;
		this._icon.enabled = false;
		this._stackLabel.text = string.Empty;
		this._usesLabel.text = string.Empty;
		if (this._amountBackground)
		{
			this._amountBackground.enabled = false;
		}
		if (this.modSprites.Length > 0)
		{
			for (int i = 0; i < this.modSprites.Length; i++)
			{
				this.modSprites[i].enabled = false;
			}
		}
	}

	// Token: 0x060026B4 RID: 9908 RVA: 0x00096CA4 File Offset: 0x00094EA4
	private void SetItem(IInventoryItem item)
	{
		if (item == null)
		{
			this.MakeEmpty();
			return;
		}
		this._myDisplayItem = item;
		if (item.datablock.IsSplittable())
		{
			this._stackLabel.color = Color.white;
			if (item.uses > 1)
			{
				this._stackLabel.text = "x" + item.uses.ToString();
			}
			else
			{
				this._stackLabel.text = string.Empty;
			}
		}
		else
		{
			this._stackLabel.color = Color.yellow;
			this._stackLabel.text = ((item.datablock._maxUses <= item.datablock.GetMinUsesForDisplay()) ? string.Empty : item.uses.ToString());
		}
		if (this._amountBackground)
		{
			if (this._stackLabel.text == string.Empty)
			{
				this._amountBackground.enabled = false;
			}
			else
			{
				Vector2 vector = this._stackLabel.font.CalculatePrintedSize(this._stackLabel.text, true, UIFont.SymbolStyle.None);
				this._amountBackground.enabled = true;
				this._amountBackground.transform.localScale = new Vector3(vector.x * this._stackLabel.transform.localScale.x + 12f, 16f, 1f);
			}
		}
		if (ItemDataBlock.LoadIconOrUnknown<Texture>(item.datablock.icon, ref item.datablock.iconTex))
		{
			Material material = TextureMaterial.GetMaterial(RPOSInventoryCell._myMaterial, item.datablock.iconTex);
			this._icon.material = (UIMaterial)material;
			this._icon.enabled = true;
		}
		IHeldItem heldItem;
		int num = ((heldItem = (item as IHeldItem)) != null) ? heldItem.totalModSlots : 0;
		int num2 = (num != 0) ? heldItem.usedModSlots : 0;
		for (int i = 0; i < this.modSprites.Length; i++)
		{
			if (i < num)
			{
				this.modSprites[i].enabled = true;
				this.modSprites[i].sprite = ((i >= num2) ? this.mod_empty : this.mod_full);
				this.modSprites[i].spriteName = this.modSprites[i].sprite.name;
			}
			else
			{
				this.modSprites[i].enabled = false;
			}
		}
		if (item.IsBroken())
		{
			this._icon.color = Color.red;
		}
		else if (item.condition / item.maxcondition <= 0.4f)
		{
			this._icon.color = Color.yellow;
		}
		else
		{
			this._icon.color = Color.white;
		}
	}

	// Token: 0x060026B5 RID: 9909 RVA: 0x00096F98 File Offset: 0x00095198
	private void OnClick()
	{
	}

	// Token: 0x060026B6 RID: 9910 RVA: 0x00096F9C File Offset: 0x0009519C
	private void OnPress(bool start)
	{
		if (start)
		{
			this.startedNoItem = (this.slotItem == null || this.IsItemLocked());
			if (this.startedNoItem)
			{
				UICamera.Cursor.CurrentButton.ClickNotification = UICamera.ClickNotification.None;
				UICamera.Cursor.DropNotification = (DropNotificationFlags)0;
			}
		}
	}

	// Token: 0x060026B7 RID: 9911 RVA: 0x00096FF0 File Offset: 0x000951F0
	private void OnDragState(bool start)
	{
		if (start)
		{
			if (!this.dragging && !this.startedNoItem)
			{
				UICamera.Cursor.DropNotification = (DropNotificationFlags.DragLand | DropNotificationFlags.AltLand | DropNotificationFlags.RegularHover | DropNotificationFlags.DragLandOutside);
				this.lastLanding = null;
				this.dragging = true;
				RPOS.Item_CellDragBegin(this);
				UICamera.Cursor.CurrentButton.ClickNotification = UICamera.ClickNotification.BasedOnDelta;
			}
		}
		else if (this.dragging)
		{
			this.dragging = false;
			if (this.lastLanding)
			{
				this.dragging = false;
				RPOS.Item_CellDragEnd(this, this.lastLanding);
				UICamera.Cursor.Buttons.LeftValue.ClickNotification = UICamera.ClickNotification.None;
			}
			else
			{
				RPOS.Item_CellReset();
			}
		}
	}

	// Token: 0x060026B8 RID: 9912 RVA: 0x000970A8 File Offset: 0x000952A8
	private void OnLand(GameObject landing)
	{
		this.lastLanding = landing.GetComponent<RPOSInventoryCell>();
	}

	// Token: 0x060026B9 RID: 9913 RVA: 0x000970B8 File Offset: 0x000952B8
	private void OnTooltip(bool show)
	{
		IInventoryItem inventoryItem;
		if (show && this._myDisplayItem != null)
		{
			IInventoryItem myDisplayItem = this._myDisplayItem;
			inventoryItem = myDisplayItem;
		}
		else
		{
			inventoryItem = null;
		}
		IInventoryItem item = inventoryItem;
		ItemDataBlock itemdb = (!show || this._myDisplayItem == null) ? null : this._myDisplayItem.datablock;
		ItemToolTip.SetToolTip(itemdb, item);
	}

	// Token: 0x060026BA RID: 9914 RVA: 0x00097110 File Offset: 0x00095310
	private void OnLandOutside()
	{
		if (this._displayInventory.gameObject == RPOS.ObservedPlayer.gameObject)
		{
			RPOS.TossItem(this._mySlot);
		}
	}

	// Token: 0x060026BB RID: 9915 RVA: 0x00097148 File Offset: 0x00095348
	private void OnAltLand(GameObject landing)
	{
		RPOSInventoryCell component = landing.GetComponent<RPOSInventoryCell>();
		if (!component)
		{
			return;
		}
		RPOS.ItemCellAltClicked(component);
	}

	// Token: 0x060026BC RID: 9916 RVA: 0x00097170 File Offset: 0x00095370
	private void OnAltClick()
	{
		if (this.slotItem != null)
		{
			RPOS.GetRightClickMenu().SetItem(this.slotItem);
		}
	}

	// Token: 0x040012F3 RID: 4851
	public UISprite _amountBackground;

	// Token: 0x040012F4 RID: 4852
	public UILabel _stackLabel;

	// Token: 0x040012F5 RID: 4853
	public UILabel _usesLabel;

	// Token: 0x040012F6 RID: 4854
	public UILabel _numberLabel;

	// Token: 0x040012F7 RID: 4855
	public UITexture _icon;

	// Token: 0x040012F8 RID: 4856
	public UISlicedSprite _background;

	// Token: 0x040012F9 RID: 4857
	public UISprite _darkener;

	// Token: 0x040012FA RID: 4858
	private Color backupColor = Color.cyan;

	// Token: 0x040012FB RID: 4859
	public Inventory _displayInventory;

	// Token: 0x040012FC RID: 4860
	public byte _mySlot;

	// Token: 0x040012FD RID: 4861
	public IInventoryItem _myDisplayItem;

	// Token: 0x040012FE RID: 4862
	public static Material _myMaterial;

	// Token: 0x040012FF RID: 4863
	private bool _locked;

	// Token: 0x04001300 RID: 4864
	public UISprite[] modSprites;

	// Token: 0x04001301 RID: 4865
	private UIAtlas.Sprite mod_empty;

	// Token: 0x04001302 RID: 4866
	private UIAtlas.Sprite mod_full;

	// Token: 0x04001303 RID: 4867
	private bool dragging;

	// Token: 0x04001304 RID: 4868
	private RPOSInventoryCell lastLanding;

	// Token: 0x04001305 RID: 4869
	private bool startedNoItem;
}
