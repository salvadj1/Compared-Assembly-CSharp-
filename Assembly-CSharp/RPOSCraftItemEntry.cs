using System;
using UnityEngine;

// Token: 0x020004C6 RID: 1222
public class RPOSCraftItemEntry : MonoBehaviour
{
	// Token: 0x06002A03 RID: 10755 RVA: 0x0009B8D8 File Offset: 0x00099AD8
	public void SetSelected(bool selected)
	{
		Color color = (!selected) ? Color.white : Color.yellow;
		base.GetComponentInChildren<global::UILabel>().color = color;
	}

	// Token: 0x06002A04 RID: 10756 RVA: 0x0009B908 File Offset: 0x00099B08
	public void Update()
	{
		if (!global::RPOS.IsOpen)
		{
			return;
		}
		if (this.blueprint && this.blueprint == this.craftWindow.selectedItem)
		{
			this.SetSelected(true);
		}
		else
		{
			this.SetSelected(false);
		}
	}

	// Token: 0x06002A05 RID: 10757 RVA: 0x0009B960 File Offset: 0x00099B60
	public void OnTooltip(bool show)
	{
		global::ItemToolTip.SetToolTip((!show || !(this.actualItemDataBlock != null)) ? null : this.actualItemDataBlock, null);
	}

	// Token: 0x0400144A RID: 5194
	public global::ItemDataBlock actualItemDataBlock;

	// Token: 0x0400144B RID: 5195
	public global::BlueprintDataBlock blueprint;

	// Token: 0x0400144C RID: 5196
	public global::RPOSCraftWindow craftWindow;
}
