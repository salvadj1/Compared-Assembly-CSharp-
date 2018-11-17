using System;
using UnityEngine;

// Token: 0x02000414 RID: 1044
public class RPOSInfoWindow : RPOSWindowScrollable
{
	// Token: 0x06002694 RID: 9876 RVA: 0x0009634C File Offset: 0x0009454C
	public RPOSInfoWindow()
	{
		this.neverAutoShow = true;
	}

	// Token: 0x06002695 RID: 9877 RVA: 0x0009635C File Offset: 0x0009455C
	public GameObject AddItemTitle(ItemDataBlock item)
	{
		return this.AddItemTitle(item, 0f);
	}

	// Token: 0x06002696 RID: 9878 RVA: 0x0009636C File Offset: 0x0009456C
	public GameObject AddItemTitle(ItemDataBlock item, float aboveSpace)
	{
		float contentHeight = this.GetContentHeight();
		GameObject gameObject = NGUITools.AddChild(this.addParent, this.itemTitlePrefab);
		gameObject.GetComponentInChildren<UILabel>().text = item.name;
		UITexture componentInChildren = gameObject.GetComponentInChildren<UITexture>();
		componentInChildren.material = componentInChildren.material.Clone();
		componentInChildren.material.Set("_MainTex", item.GetIconTexture());
		gameObject.transform.SetLocalPositionY(-(contentHeight + aboveSpace));
		return gameObject;
	}

	// Token: 0x06002697 RID: 9879 RVA: 0x000963E4 File Offset: 0x000945E4
	public GameObject AddSectionTitle(string text, float aboveSpace)
	{
		float contentHeight = this.GetContentHeight();
		GameObject gameObject = NGUITools.AddChild(this.addParent, this.sectionTitlePrefab);
		gameObject.GetComponentInChildren<UILabel>().text = text;
		gameObject.transform.SetLocalPositionY(-(contentHeight + aboveSpace));
		return gameObject;
	}

	// Token: 0x06002698 RID: 9880 RVA: 0x00096428 File Offset: 0x00094628
	public GameObject AddItemDescription(ItemDataBlock item, float aboveSpace)
	{
		float contentHeight = this.GetContentHeight();
		GameObject gameObject = NGUITools.AddChild(this.addParent, this.itemDescriptionPrefab);
		gameObject.transform.FindChild("DescText").GetComponent<UILabel>().text = item.GetItemDescription();
		gameObject.transform.SetLocalPositionY(-(contentHeight + aboveSpace));
		return null;
	}

	// Token: 0x06002699 RID: 9881 RVA: 0x00096480 File Offset: 0x00094680
	public GameObject AddBasicLabel(string text, float aboveSpace)
	{
		float contentHeight = this.GetContentHeight();
		GameObject gameObject = NGUITools.AddChild(this.addParent, this.basicLabelPrefab);
		gameObject.GetComponentInChildren<UILabel>().text = text;
		gameObject.transform.SetLocalPositionY(-(contentHeight + aboveSpace));
		return gameObject;
	}

	// Token: 0x0600269A RID: 9882 RVA: 0x000964C4 File Offset: 0x000946C4
	public GameObject AddProgressStat(string text, float currentAmount, float maxAmount, float aboveSpace)
	{
		float contentHeight = this.GetContentHeight();
		GameObject gameObject = NGUITools.AddChild(this.addParent, this.progressStatPrefab);
		UISlider componentInChildren = gameObject.GetComponentInChildren<UISlider>();
		UILabel component = FindChildHelper.FindChildByName("ProgressStatTitle", gameObject.gameObject).GetComponent<UILabel>();
		UILabel component2 = FindChildHelper.FindChildByName("ProgressAmountLabel", gameObject.gameObject).GetComponent<UILabel>();
		component.text = text;
		component2.text = ((currentAmount >= 1f) ? currentAmount.ToString("N0") : currentAmount.ToString("N2"));
		componentInChildren.sliderValue = currentAmount / maxAmount;
		gameObject.transform.SetLocalPositionY(-(contentHeight + aboveSpace));
		return gameObject;
	}

	// Token: 0x0600269B RID: 9883 RVA: 0x00096570 File Offset: 0x00094770
	public float GetContentHeight()
	{
		return NGUIMath.CalculateRelativeWidgetBounds(this.addParent.transform, this.addParent.transform).size.y;
	}

	// Token: 0x0600269C RID: 9884 RVA: 0x000965A8 File Offset: 0x000947A8
	public void FinishPopulating()
	{
		base.ResetScrolling();
		base.showWithRPOS = this.autoShowWithRPOS;
		base.showWithoutRPOS = this.autoShowWithoutRPOS;
	}

	// Token: 0x0600269D RID: 9885 RVA: 0x000965C8 File Offset: 0x000947C8
	private void SetVisible(bool enable)
	{
		Debug.Log("Info RPOS opened");
		base.mainPanel.enabled = enable;
		UIPanel[] componentsInChildren = base.GetComponentsInChildren<UIPanel>();
		foreach (UIPanel uipanel in componentsInChildren)
		{
			uipanel.enabled = enable;
		}
	}

	// Token: 0x0600269E RID: 9886 RVA: 0x00096614 File Offset: 0x00094814
	protected override void OnWindowShow()
	{
		base.OnWindowShow();
		this.SetVisible(true);
	}

	// Token: 0x0600269F RID: 9887 RVA: 0x00096624 File Offset: 0x00094824
	protected override void OnWindowHide()
	{
		base.OnWindowHide();
		this.SetVisible(false);
	}

	// Token: 0x040012DF RID: 4831
	public GameObject addParent;

	// Token: 0x040012E0 RID: 4832
	public GameObject itemTitlePrefab;

	// Token: 0x040012E1 RID: 4833
	public GameObject sectionTitlePrefab;

	// Token: 0x040012E2 RID: 4834
	public GameObject itemDescriptionPrefab;

	// Token: 0x040012E3 RID: 4835
	public GameObject basicLabelPrefab;

	// Token: 0x040012E4 RID: 4836
	public GameObject progressStatPrefab;
}
