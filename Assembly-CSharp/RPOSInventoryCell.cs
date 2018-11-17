using System;
using Facepunch;
using UnityEngine;

// Token: 0x020004CC RID: 1228
public class RPOSInventoryCell : MonoBehaviour
{
	// Token: 0x17000955 RID: 2389
	// (get) Token: 0x06002A38 RID: 10808 RVA: 0x0009C8C0 File Offset: 0x0009AAC0
	public global::IInventoryItem slotItem
	{
		get
		{
			global::IInventoryItem result;
			if (this._displayInventory && this._displayInventory.GetItem((int)this._mySlot, out result))
			{
				return result;
			}
			return null;
		}
	}

	// Token: 0x06002A39 RID: 10809 RVA: 0x0009C8F8 File Offset: 0x0009AAF8
	private void Start()
	{
		if (!global::RPOSInventoryCell._myMaterial)
		{
			Facepunch.Bundling.Load<Material>("content/item/mat/ItemIconShader", out global::RPOSInventoryCell._myMaterial);
		}
		this._icon.enabled = false;
		if (this.modSprites.Length > 0)
		{
			this.mod_empty = this.modSprites[0].atlas.GetSprite("slot_empty");
			this.mod_full = this.modSprites[0].atlas.GetSprite("slot_full");
		}
	}

	// Token: 0x06002A3A RID: 10810 RVA: 0x0009C978 File Offset: 0x0009AB78
	private void Update()
	{
		if (this._displayInventory)
		{
			if (global::RPOS.Item_IsClickedCell(this))
			{
				this.MakeEmpty();
			}
			else
			{
				global::IInventoryItem inventoryItem;
				this._displayInventory.GetItem((int)this._mySlot, out inventoryItem);
				if (this._displayInventory.MarkSlotClean((int)this._mySlot) || !object.ReferenceEquals(this._myDisplayItem, inventoryItem))
				{
					this.SetItem(inventoryItem);
				}
			}
			if (!global::RPOS.IsOpen && this._darkener)
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

	// Token: 0x06002A3B RID: 10811 RVA: 0x0009CA74 File Offset: 0x0009AC74
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

	// Token: 0x06002A3C RID: 10812 RVA: 0x0009CACC File Offset: 0x0009ACCC
	public bool IsItemLocked()
	{
		return this._locked;
	}

	// Token: 0x06002A3D RID: 10813 RVA: 0x0009CAD4 File Offset: 0x0009ACD4
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

	// Token: 0x06002A3E RID: 10814 RVA: 0x0009CB68 File Offset: 0x0009AD68
	private void SetItem(global::IInventoryItem item)
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
				Vector2 vector = this._stackLabel.font.CalculatePrintedSize(this._stackLabel.text, true, global::UIFont.SymbolStyle.None);
				this._amountBackground.enabled = true;
				this._amountBackground.transform.localScale = new Vector3(vector.x * this._stackLabel.transform.localScale.x + 12f, 16f, 1f);
			}
		}
		if (global::ItemDataBlock.LoadIconOrUnknown<Texture>(item.datablock.icon, ref item.datablock.iconTex))
		{
			Material material = global::TextureMaterial.GetMaterial(global::RPOSInventoryCell._myMaterial, item.datablock.iconTex);
			this._icon.material = (global::UIMaterial)material;
			this._icon.enabled = true;
		}
		global::IHeldItem heldItem;
		int num = ((heldItem = (item as global::IHeldItem)) != null) ? heldItem.totalModSlots : 0;
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

	// Token: 0x06002A3F RID: 10815 RVA: 0x0009CE5C File Offset: 0x0009B05C
	private void OnClick()
	{
	}

	// Token: 0x06002A40 RID: 10816 RVA: 0x0009CE60 File Offset: 0x0009B060
	private void OnPress(bool start)
	{
		if (start)
		{
			this.startedNoItem = (this.slotItem == null || this.IsItemLocked());
			if (this.startedNoItem)
			{
				global::UICamera.Cursor.CurrentButton.ClickNotification = global::UICamera.ClickNotification.None;
				global::UICamera.Cursor.DropNotification = (global::DropNotificationFlags)0;
			}
		}
	}

	// Token: 0x06002A41 RID: 10817 RVA: 0x0009CEB4 File Offset: 0x0009B0B4
	private void OnDragState(bool start)
	{
		if (start)
		{
			if (!this.dragging && !this.startedNoItem)
			{
				global::UICamera.Cursor.DropNotification = (global::DropNotificationFlags.DragLand | global::DropNotificationFlags.AltLand | global::DropNotificationFlags.RegularHover | global::DropNotificationFlags.DragLandOutside);
				this.lastLanding = null;
				this.dragging = true;
				global::RPOS.Item_CellDragBegin(this);
				global::UICamera.Cursor.CurrentButton.ClickNotification = global::UICamera.ClickNotification.BasedOnDelta;
			}
		}
		else if (this.dragging)
		{
			this.dragging = false;
			if (this.lastLanding)
			{
				this.dragging = false;
				global::RPOS.Item_CellDragEnd(this, this.lastLanding);
				global::UICamera.Cursor.Buttons.LeftValue.ClickNotification = global::UICamera.ClickNotification.None;
			}
			else
			{
				global::RPOS.Item_CellReset();
			}
		}
	}

	// Token: 0x06002A42 RID: 10818 RVA: 0x0009CF6C File Offset: 0x0009B16C
	private void OnLand(GameObject landing)
	{
		this.lastLanding = landing.GetComponent<global::RPOSInventoryCell>();
	}

	// Token: 0x06002A43 RID: 10819 RVA: 0x0009CF7C File Offset: 0x0009B17C
	private void OnTooltip(bool show)
	{
		global::IInventoryItem inventoryItem;
		if (show && this._myDisplayItem != null)
		{
			global::IInventoryItem myDisplayItem = this._myDisplayItem;
			inventoryItem = myDisplayItem;
		}
		else
		{
			inventoryItem = null;
		}
		global::IInventoryItem item = inventoryItem;
		global::ItemDataBlock itemdb = (!show || this._myDisplayItem == null) ? null : this._myDisplayItem.datablock;
		global::ItemToolTip.SetToolTip(itemdb, item);
	}

	// Token: 0x06002A44 RID: 10820 RVA: 0x0009CFD4 File Offset: 0x0009B1D4
	private void OnLandOutside()
	{
		if (this._displayInventory.gameObject == global::RPOS.ObservedPlayer.gameObject)
		{
			global::RPOS.TossItem(this._mySlot);
		}
	}

	// Token: 0x06002A45 RID: 10821 RVA: 0x0009D00C File Offset: 0x0009B20C
	private void OnAltLand(GameObject landing)
	{
		global::RPOSInventoryCell component = landing.GetComponent<global::RPOSInventoryCell>();
		if (!component)
		{
			return;
		}
		global::RPOS.ItemCellAltClicked(component);
	}

	// Token: 0x06002A46 RID: 10822 RVA: 0x0009D034 File Offset: 0x0009B234
	private void OnAltClick()
	{
		if (this.slotItem != null)
		{
			global::RPOS.GetRightClickMenu().SetItem(this.slotItem);
		}
	}

	// Token: 0x04001473 RID: 5235
	public global::UISprite _amountBackground;

	// Token: 0x04001474 RID: 5236
	public global::UILabel _stackLabel;

	// Token: 0x04001475 RID: 5237
	public global::UILabel _usesLabel;

	// Token: 0x04001476 RID: 5238
	public global::UILabel _numberLabel;

	// Token: 0x04001477 RID: 5239
	public global::UITexture _icon;

	// Token: 0x04001478 RID: 5240
	public global::UISlicedSprite _background;

	// Token: 0x04001479 RID: 5241
	public global::UISprite _darkener;

	// Token: 0x0400147A RID: 5242
	private Color backupColor = Color.cyan;

	// Token: 0x0400147B RID: 5243
	public global::Inventory _displayInventory;

	// Token: 0x0400147C RID: 5244
	public byte _mySlot;

	// Token: 0x0400147D RID: 5245
	public global::IInventoryItem _myDisplayItem;

	// Token: 0x0400147E RID: 5246
	public static Material _myMaterial;

	// Token: 0x0400147F RID: 5247
	private bool _locked;

	// Token: 0x04001480 RID: 5248
	public global::UISprite[] modSprites;

	// Token: 0x04001481 RID: 5249
	private global::UIAtlas.Sprite mod_empty;

	// Token: 0x04001482 RID: 5250
	private global::UIAtlas.Sprite mod_full;

	// Token: 0x04001483 RID: 5251
	private bool dragging;

	// Token: 0x04001484 RID: 5252
	private global::RPOSInventoryCell lastLanding;

	// Token: 0x04001485 RID: 5253
	private bool startedNoItem;
}
