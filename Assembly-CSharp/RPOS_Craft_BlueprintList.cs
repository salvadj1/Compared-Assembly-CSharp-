using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000424 RID: 1060
public class RPOS_Craft_BlueprintList : MonoBehaviour
{
	// Token: 0x06002763 RID: 10083 RVA: 0x0009977C File Offset: 0x0009797C
	private void Awake()
	{
	}

	// Token: 0x06002764 RID: 10084 RVA: 0x00099780 File Offset: 0x00097980
	public bool AnyOfCategoryInList(ItemDataBlock.ItemCategory category, List<BlueprintDataBlock> checkList)
	{
		foreach (BlueprintDataBlock blueprintDataBlock in checkList)
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

	// Token: 0x06002765 RID: 10085 RVA: 0x00099814 File Offset: 0x00097A14
	public void AddItemCategoryHeader(ItemDataBlock.ItemCategory category)
	{
	}

	// Token: 0x06002766 RID: 10086 RVA: 0x00099818 File Offset: 0x00097A18
	public RPOSCraftItemEntry GetEntryByBP(BlueprintDataBlock bp)
	{
		foreach (object obj in base.transform)
		{
			RPOSCraftItemEntry component = (obj as Transform).GetComponent<RPOSCraftItemEntry>();
			if (component && component.blueprint == bp)
			{
				return component;
			}
		}
		return null;
	}

	// Token: 0x06002767 RID: 10087 RVA: 0x000998B0 File Offset: 0x00097AB0
	public int AddItemsOfCategory(ItemDataBlock.ItemCategory category, List<BlueprintDataBlock> checkList, int yPos)
	{
		if (!this.AnyOfCategoryInList(category, checkList))
		{
			return yPos;
		}
		GameObject gameObject = NGUITools.AddChild(base.gameObject, this.CategoryHeaderPrefab);
		gameObject.transform.localPosition = new Vector3(0f, (float)yPos, -1f);
		gameObject.GetComponentInChildren<UILabel>().text = category.ToString();
		yPos -= 16;
		foreach (BlueprintDataBlock blueprintDataBlock in checkList)
		{
			if (blueprintDataBlock.resultItem.category == category)
			{
				GameObject gameObject2 = NGUITools.AddChild(base.gameObject, this.ItemPlaquePrefab);
				gameObject2.GetComponentInChildren<UILabel>().text = blueprintDataBlock.resultItem.name;
				gameObject2.transform.localPosition = new Vector3(10f, (float)yPos, -1f);
				UIEventListener uieventListener = UIEventListener.Get(gameObject2);
				UIEventListener uieventListener2 = uieventListener;
				uieventListener2.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uieventListener2.onClick, new UIEventListener.VoidDelegate(this.craftWindow.ItemClicked));
				gameObject2.GetComponent<RPOSCraftItemEntry>().actualItemDataBlock = blueprintDataBlock.resultItem;
				gameObject2.GetComponent<RPOSCraftItemEntry>().blueprint = blueprintDataBlock;
				gameObject2.GetComponent<RPOSCraftItemEntry>().craftWindow = this.craftWindow;
				gameObject2.GetComponent<RPOSCraftItemEntry>().SetSelected(false);
				yPos -= 16;
			}
		}
		return yPos;
	}

	// Token: 0x06002768 RID: 10088 RVA: 0x00099A30 File Offset: 0x00097C30
	public void UpdateItems()
	{
		PlayerInventory component = RPOS.ObservedPlayer.GetComponent<PlayerInventory>();
		List<BlueprintDataBlock> boundBPs = component.GetBoundBPs();
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
		int yPos = this.AddItemsOfCategory(ItemDataBlock.ItemCategory.Survival, boundBPs, 0);
		yPos = this.AddItemsOfCategory(ItemDataBlock.ItemCategory.Resource, boundBPs, yPos);
		yPos = this.AddItemsOfCategory(ItemDataBlock.ItemCategory.Medical, boundBPs, yPos);
		yPos = this.AddItemsOfCategory(ItemDataBlock.ItemCategory.Ammo, boundBPs, yPos);
		yPos = this.AddItemsOfCategory(ItemDataBlock.ItemCategory.Weapons, boundBPs, yPos);
		yPos = this.AddItemsOfCategory(ItemDataBlock.ItemCategory.Armor, boundBPs, yPos);
		yPos = this.AddItemsOfCategory(ItemDataBlock.ItemCategory.Tools, boundBPs, yPos);
		yPos = this.AddItemsOfCategory(ItemDataBlock.ItemCategory.Mods, boundBPs, yPos);
		yPos = this.AddItemsOfCategory(ItemDataBlock.ItemCategory.Parts, boundBPs, yPos);
		yPos = this.AddItemsOfCategory(ItemDataBlock.ItemCategory.Food, boundBPs, yPos);
		yPos = this.AddItemsOfCategory(ItemDataBlock.ItemCategory.Blueprint, boundBPs, yPos);
		yPos = this.AddItemsOfCategory(ItemDataBlock.ItemCategory.Misc, boundBPs, yPos);
		base.GetComponent<UIDraggablePanel>().calculateNextChange = true;
	}

	// Token: 0x04001364 RID: 4964
	public GameObject CategoryHeaderPrefab;

	// Token: 0x04001365 RID: 4965
	public GameObject ItemPlaquePrefab;

	// Token: 0x04001366 RID: 4966
	public RPOSCraftWindow craftWindow;

	// Token: 0x04001367 RID: 4967
	private int lastNumBoundBPs;
}
