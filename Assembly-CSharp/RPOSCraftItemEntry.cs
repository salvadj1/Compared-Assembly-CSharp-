using System;
using UnityEngine;

// Token: 0x02000411 RID: 1041
public class RPOSCraftItemEntry : MonoBehaviour
{
	// Token: 0x06002679 RID: 9849 RVA: 0x00095A14 File Offset: 0x00093C14
	public void SetSelected(bool selected)
	{
		Color color = (!selected) ? Color.white : Color.yellow;
		base.GetComponentInChildren<UILabel>().color = color;
	}

	// Token: 0x0600267A RID: 9850 RVA: 0x00095A44 File Offset: 0x00093C44
	public void Update()
	{
		if (!RPOS.IsOpen)
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

	// Token: 0x0600267B RID: 9851 RVA: 0x00095A9C File Offset: 0x00093C9C
	public void OnTooltip(bool show)
	{
		ItemToolTip.SetToolTip((!show || !(this.actualItemDataBlock != null)) ? null : this.actualItemDataBlock, null);
	}

	// Token: 0x040012CA RID: 4810
	public ItemDataBlock actualItemDataBlock;

	// Token: 0x040012CB RID: 4811
	public BlueprintDataBlock blueprint;

	// Token: 0x040012CC RID: 4812
	public RPOSCraftWindow craftWindow;
}
