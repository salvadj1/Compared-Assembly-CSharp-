using System;
using UnityEngine;

// Token: 0x020004C9 RID: 1225
public class RPOSInfoWindow : global::RPOSWindowScrollable
{
	// Token: 0x06002A1E RID: 10782 RVA: 0x0009C210 File Offset: 0x0009A410
	public RPOSInfoWindow()
	{
		this.neverAutoShow = true;
	}

	// Token: 0x06002A1F RID: 10783 RVA: 0x0009C220 File Offset: 0x0009A420
	public GameObject AddItemTitle(global::ItemDataBlock item)
	{
		return this.AddItemTitle(item, 0f);
	}

	// Token: 0x06002A20 RID: 10784 RVA: 0x0009C230 File Offset: 0x0009A430
	public GameObject AddItemTitle(global::ItemDataBlock item, float aboveSpace)
	{
		float contentHeight = this.GetContentHeight();
		GameObject gameObject = global::NGUITools.AddChild(this.addParent, this.itemTitlePrefab);
		gameObject.GetComponentInChildren<global::UILabel>().text = item.name;
		global::UITexture componentInChildren = gameObject.GetComponentInChildren<global::UITexture>();
		componentInChildren.material = componentInChildren.material.Clone();
		componentInChildren.material.Set("_MainTex", item.GetIconTexture());
		gameObject.transform.SetLocalPositionY(-(contentHeight + aboveSpace));
		return gameObject;
	}

	// Token: 0x06002A21 RID: 10785 RVA: 0x0009C2A8 File Offset: 0x0009A4A8
	public GameObject AddSectionTitle(string text, float aboveSpace)
	{
		float contentHeight = this.GetContentHeight();
		GameObject gameObject = global::NGUITools.AddChild(this.addParent, this.sectionTitlePrefab);
		gameObject.GetComponentInChildren<global::UILabel>().text = text;
		gameObject.transform.SetLocalPositionY(-(contentHeight + aboveSpace));
		return gameObject;
	}

	// Token: 0x06002A22 RID: 10786 RVA: 0x0009C2EC File Offset: 0x0009A4EC
	public GameObject AddItemDescription(global::ItemDataBlock item, float aboveSpace)
	{
		float contentHeight = this.GetContentHeight();
		GameObject gameObject = global::NGUITools.AddChild(this.addParent, this.itemDescriptionPrefab);
		gameObject.transform.FindChild("DescText").GetComponent<global::UILabel>().text = item.GetItemDescription();
		gameObject.transform.SetLocalPositionY(-(contentHeight + aboveSpace));
		return null;
	}

	// Token: 0x06002A23 RID: 10787 RVA: 0x0009C344 File Offset: 0x0009A544
	public GameObject AddBasicLabel(string text, float aboveSpace)
	{
		float contentHeight = this.GetContentHeight();
		GameObject gameObject = global::NGUITools.AddChild(this.addParent, this.basicLabelPrefab);
		gameObject.GetComponentInChildren<global::UILabel>().text = text;
		gameObject.transform.SetLocalPositionY(-(contentHeight + aboveSpace));
		return gameObject;
	}

	// Token: 0x06002A24 RID: 10788 RVA: 0x0009C388 File Offset: 0x0009A588
	public GameObject AddProgressStat(string text, float currentAmount, float maxAmount, float aboveSpace)
	{
		float contentHeight = this.GetContentHeight();
		GameObject gameObject = global::NGUITools.AddChild(this.addParent, this.progressStatPrefab);
		global::UISlider componentInChildren = gameObject.GetComponentInChildren<global::UISlider>();
		global::UILabel component = global::FindChildHelper.FindChildByName("ProgressStatTitle", gameObject.gameObject).GetComponent<global::UILabel>();
		global::UILabel component2 = global::FindChildHelper.FindChildByName("ProgressAmountLabel", gameObject.gameObject).GetComponent<global::UILabel>();
		component.text = text;
		component2.text = ((currentAmount >= 1f) ? currentAmount.ToString("N0") : currentAmount.ToString("N2"));
		componentInChildren.sliderValue = currentAmount / maxAmount;
		gameObject.transform.SetLocalPositionY(-(contentHeight + aboveSpace));
		return gameObject;
	}

	// Token: 0x06002A25 RID: 10789 RVA: 0x0009C434 File Offset: 0x0009A634
	public float GetContentHeight()
	{
		return global::NGUIMath.CalculateRelativeWidgetBounds(this.addParent.transform, this.addParent.transform).size.y;
	}

	// Token: 0x06002A26 RID: 10790 RVA: 0x0009C46C File Offset: 0x0009A66C
	public void FinishPopulating()
	{
		base.ResetScrolling();
		base.showWithRPOS = this.autoShowWithRPOS;
		base.showWithoutRPOS = this.autoShowWithoutRPOS;
	}

	// Token: 0x06002A27 RID: 10791 RVA: 0x0009C48C File Offset: 0x0009A68C
	private void SetVisible(bool enable)
	{
		Debug.Log("Info RPOS opened");
		base.mainPanel.enabled = enable;
		global::UIPanel[] componentsInChildren = base.GetComponentsInChildren<global::UIPanel>();
		foreach (global::UIPanel uipanel in componentsInChildren)
		{
			uipanel.enabled = enable;
		}
	}

	// Token: 0x06002A28 RID: 10792 RVA: 0x0009C4D8 File Offset: 0x0009A6D8
	protected override void OnWindowShow()
	{
		base.OnWindowShow();
		this.SetVisible(true);
	}

	// Token: 0x06002A29 RID: 10793 RVA: 0x0009C4E8 File Offset: 0x0009A6E8
	protected override void OnWindowHide()
	{
		base.OnWindowHide();
		this.SetVisible(false);
	}

	// Token: 0x0400145F RID: 5215
	public GameObject addParent;

	// Token: 0x04001460 RID: 5216
	public GameObject itemTitlePrefab;

	// Token: 0x04001461 RID: 5217
	public GameObject sectionTitlePrefab;

	// Token: 0x04001462 RID: 5218
	public GameObject itemDescriptionPrefab;

	// Token: 0x04001463 RID: 5219
	public GameObject basicLabelPrefab;

	// Token: 0x04001464 RID: 5220
	public GameObject progressStatPrefab;
}
