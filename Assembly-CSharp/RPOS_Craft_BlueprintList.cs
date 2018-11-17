using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004DA RID: 1242
public class RPOS_Craft_BlueprintList : MonoBehaviour
{
	// Token: 0x06002AF3 RID: 10995 RVA: 0x0009F6FC File Offset: 0x0009D8FC
	private void Awake()
	{
	}

	// Token: 0x06002AF4 RID: 10996 RVA: 0x0009F700 File Offset: 0x0009D900
	public bool AnyOfCategoryInList(global::ItemDataBlock.ItemCategory category, List<global::BlueprintDataBlock> checkList)
	{
		foreach (global::BlueprintDataBlock blueprintDataBlock in checkList)
		{
			if (blueprintDataBlock == null)
			{
				Debug.Log("WTFFFF");
				return false;
			}
			if (blueprintDataBlock.resultItem.category == category)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06002AF5 RID: 10997 RVA: 0x0009F794 File Offset: 0x0009D994
	public void AddItemCategoryHeader(global::ItemDataBlock.ItemCategory category)
	{
	}

	// Token: 0x06002AF6 RID: 10998 RVA: 0x0009F798 File Offset: 0x0009D998
	public global::RPOSCraftItemEntry GetEntryByBP(global::BlueprintDataBlock bp)
	{
		foreach (object obj in base.transform)
		{
			global::RPOSCraftItemEntry component = (obj as Transform).GetComponent<global::RPOSCraftItemEntry>();
			if (component && component.blueprint == bp)
			{
				return component;
			}
		}
		return null;
	}

	// Token: 0x06002AF7 RID: 10999 RVA: 0x0009F830 File Offset: 0x0009DA30
	public int AddItemsOfCategory(global::ItemDataBlock.ItemCategory category, List<global::BlueprintDataBlock> checkList, int yPos)
	{
		if (!this.AnyOfCategoryInList(category, checkList))
		{
			return yPos;
		}
		GameObject gameObject = global::NGUITools.AddChild(base.gameObject, this.CategoryHeaderPrefab);
		gameObject.transform.localPosition = new Vector3(0f, (float)yPos, -1f);
		gameObject.GetComponentInChildren<global::UILabel>().text = category.ToString();
		yPos -= 16;
		foreach (global::BlueprintDataBlock blueprintDataBlock in checkList)
		{
			if (blueprintDataBlock.resultItem.category == category)
			{
				GameObject gameObject2 = global::NGUITools.AddChild(base.gameObject, this.ItemPlaquePrefab);
				gameObject2.GetComponentInChildren<global::UILabel>().text = blueprintDataBlock.resultItem.name;
				gameObject2.transform.localPosition = new Vector3(10f, (float)yPos, -1f);
				global::UIEventListener uieventListener = global::UIEventListener.Get(gameObject2);
				global::UIEventListener uieventListener2 = uieventListener;
				uieventListener2.onClick = (global::UIEventListener.VoidDelegate)Delegate.Combine(uieventListener2.onClick, new global::UIEventListener.VoidDelegate(this.craftWindow.ItemClicked));
				gameObject2.GetComponent<global::RPOSCraftItemEntry>().actualItemDataBlock = blueprintDataBlock.resultItem;
				gameObject2.GetComponent<global::RPOSCraftItemEntry>().blueprint = blueprintDataBlock;
				gameObject2.GetComponent<global::RPOSCraftItemEntry>().craftWindow = this.craftWindow;
				gameObject2.GetComponent<global::RPOSCraftItemEntry>().SetSelected(false);
				yPos -= 16;
			}
		}
		return yPos;
	}

	// Token: 0x06002AF8 RID: 11000 RVA: 0x0009F9B0 File Offset: 0x0009DBB0
	public void UpdateItems()
	{
		global::PlayerInventory component = global::RPOS.ObservedPlayer.GetComponent<global::PlayerInventory>();
		List<global::BlueprintDataBlock> boundBPs = component.GetBoundBPs();
		int count = boundBPs.Count;
		if (boundBPs == null)
		{
			Debug.Log("BOUND BP LIST EMPTY!!!!!");
			return;
		}
		if (this.lastNumBoundBPs == count)
		{
			return;
		}
		this.lastNumBoundBPs = count;
		foreach (object obj in base.transform)
		{
			Object.Destroy((obj as Transform).gameObject);
		}
		int yPos = this.AddItemsOfCategory(global::ItemDataBlock.ItemCategory.Survival, boundBPs, 0);
		yPos = this.AddItemsOfCategory(global::ItemDataBlock.ItemCategory.Resource, boundBPs, yPos);
		yPos = this.AddItemsOfCategory(global::ItemDataBlock.ItemCategory.Medical, boundBPs, yPos);
		yPos = this.AddItemsOfCategory(global::ItemDataBlock.ItemCategory.Ammo, boundBPs, yPos);
		yPos = this.AddItemsOfCategory(global::ItemDataBlock.ItemCategory.Weapons, boundBPs, yPos);
		yPos = this.AddItemsOfCategory(global::ItemDataBlock.ItemCategory.Armor, boundBPs, yPos);
		yPos = this.AddItemsOfCategory(global::ItemDataBlock.ItemCategory.Tools, boundBPs, yPos);
		yPos = this.AddItemsOfCategory(global::ItemDataBlock.ItemCategory.Mods, boundBPs, yPos);
		yPos = this.AddItemsOfCategory(global::ItemDataBlock.ItemCategory.Parts, boundBPs, yPos);
		yPos = this.AddItemsOfCategory(global::ItemDataBlock.ItemCategory.Food, boundBPs, yPos);
		yPos = this.AddItemsOfCategory(global::ItemDataBlock.ItemCategory.Blueprint, boundBPs, yPos);
		yPos = this.AddItemsOfCategory(global::ItemDataBlock.ItemCategory.Misc, boundBPs, yPos);
		base.GetComponent<global::UIDraggablePanel>().calculateNextChange = true;
	}

	// Token: 0x040014E7 RID: 5351
	public GameObject CategoryHeaderPrefab;

	// Token: 0x040014E8 RID: 5352
	public GameObject ItemPlaquePrefab;

	// Token: 0x040014E9 RID: 5353
	public global::RPOSCraftWindow craftWindow;

	// Token: 0x040014EA RID: 5354
	private int lastNumBoundBPs;
}
