using System;
using UnityEngine;

// Token: 0x020003FB RID: 1019
public class ItemToolTip : MonoBehaviour
{
	// Token: 0x06002568 RID: 9576 RVA: 0x0008FAD8 File Offset: 0x0008DCD8
	public static ItemToolTip Get()
	{
		return ItemToolTip._globalToolTip;
	}

	// Token: 0x06002569 RID: 9577 RVA: 0x0008FAE0 File Offset: 0x0008DCE0
	private void Awake()
	{
		ItemToolTip._globalToolTip = this;
		if (this.uiCamera == null)
		{
			this.uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		this.planeTest = new Plane(this.uiCamera.transform.forward * 1f, new Vector3(0f, 0f, 2f));
	}

	// Token: 0x0600256A RID: 9578 RVA: 0x0008FB54 File Offset: 0x0008DD54
	private void Update()
	{
	}

	// Token: 0x0600256B RID: 9579 RVA: 0x0008FB58 File Offset: 0x0008DD58
	public void RepositionAtCursor()
	{
		Vector3 vector = UICamera.lastMousePosition;
		Ray ray = this.uiCamera.ScreenPointToRay(vector);
		float num = 0f;
		if (this.planeTest.Raycast(ray, ref num))
		{
			base.transform.position = ray.GetPoint(num);
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y, -180f);
			AABBox aabbox = NGUIMath.CalculateRelativeWidgetBounds(base.transform);
			float num2 = base.transform.localPosition.x + aabbox.size.x - (float)Screen.width;
			if (num2 > 0f)
			{
				base.transform.SetLocalPositionX(base.transform.localPosition.x - num2);
			}
			float num3 = Mathf.Abs(base.transform.localPosition.y - aabbox.size.y) - (float)Screen.height;
			if (num3 > 0f)
			{
				base.transform.SetLocalPositionY(base.transform.localPosition.y + num3);
			}
		}
	}

	// Token: 0x0600256C RID: 9580 RVA: 0x0008FCB0 File Offset: 0x0008DEB0
	public static void SetToolTip(ItemDataBlock itemdb, IInventoryItem item = null)
	{
		ItemToolTip.Get().Internal_SetToolTip(itemdb, item);
		ItemToolTip.Get().RepositionAtCursor();
	}

	// Token: 0x0600256D RID: 9581 RVA: 0x0008FCC8 File Offset: 0x0008DEC8
	public void Internal_SetToolTip(ItemDataBlock itemdb, IInventoryItem item)
	{
		this.ClearContents();
		if (itemdb == null)
		{
			this.SetVisible(false);
			return;
		}
		this.RepositionAtCursor();
		itemdb.PopulateInfoWindow(this, item);
		this._background.transform.localScale = new Vector3(250f, this.GetContentHeight() + Mathf.Abs(this.addParent.transform.localPosition.y * 2f), 1f);
		this.SetVisible(true);
	}

	// Token: 0x0600256E RID: 9582 RVA: 0x0008FD50 File Offset: 0x0008DF50
	public void ClearContents()
	{
		foreach (object obj in this.addParent.transform)
		{
			Transform transform = (Transform)obj;
			Object.Destroy(transform.gameObject);
		}
	}

	// Token: 0x0600256F RID: 9583 RVA: 0x0008FDC8 File Offset: 0x0008DFC8
	public void SetVisible(bool vis)
	{
		base.GetComponent<UIPanel>().enabled = vis;
	}

	// Token: 0x06002570 RID: 9584 RVA: 0x0008FDD8 File Offset: 0x0008DFD8
	public GameObject AddItemTitle(ItemDataBlock itemdb, IInventoryItem itemInstance = null, float aboveSpace = 0f)
	{
		float contentHeight = this.GetContentHeight();
		GameObject gameObject = NGUITools.AddChild(this.addParent, this.itemTitlePrefab);
		gameObject.GetComponentInChildren<UILabel>().text = ((itemInstance == null) ? itemdb.name : itemInstance.toolTip);
		UITexture componentInChildren = gameObject.GetComponentInChildren<UITexture>();
		componentInChildren.material = componentInChildren.material.Clone();
		componentInChildren.material.Set("_MainTex", itemdb.GetIconTexture());
		componentInChildren.color = ((itemInstance == null || !itemInstance.IsBroken()) ? Color.white : Color.red);
		gameObject.transform.SetLocalPositionY(-(contentHeight + aboveSpace));
		return gameObject;
	}

	// Token: 0x06002571 RID: 9585 RVA: 0x0008FE84 File Offset: 0x0008E084
	public GameObject AddSectionTitle(string text, float aboveSpace)
	{
		float contentHeight = this.GetContentHeight();
		GameObject gameObject = NGUITools.AddChild(this.addParent, this.sectionTitlePrefab);
		gameObject.GetComponentInChildren<UILabel>().text = text;
		gameObject.transform.SetLocalPositionY(-(contentHeight + aboveSpace));
		return gameObject;
	}

	// Token: 0x06002572 RID: 9586 RVA: 0x0008FEC8 File Offset: 0x0008E0C8
	public GameObject AddItemDescription(ItemDataBlock item, float aboveSpace)
	{
		float contentHeight = this.GetContentHeight();
		GameObject gameObject = NGUITools.AddChild(this.addParent, this.itemDescriptionPrefab);
		gameObject.transform.FindChild("DescText").GetComponent<UILabel>().text = item.GetItemDescription();
		gameObject.transform.SetLocalPositionY(-(contentHeight + aboveSpace));
		return null;
	}

	// Token: 0x06002573 RID: 9587 RVA: 0x0008FF20 File Offset: 0x0008E120
	public GameObject AddBasicLabel(string text, float aboveSpace)
	{
		return this.AddBasicLabel(text, aboveSpace, Color.white);
	}

	// Token: 0x06002574 RID: 9588 RVA: 0x0008FF30 File Offset: 0x0008E130
	public GameObject AddBasicLabel(string text, float aboveSpace, Color col)
	{
		float contentHeight = this.GetContentHeight();
		GameObject gameObject = NGUITools.AddChild(this.addParent, this.basicLabelPrefab);
		UILabel componentInChildren = gameObject.GetComponentInChildren<UILabel>();
		componentInChildren.text = text;
		componentInChildren.color = col;
		gameObject.transform.SetLocalPositionY(-(contentHeight + aboveSpace));
		return gameObject;
	}

	// Token: 0x06002575 RID: 9589 RVA: 0x0008FF7C File Offset: 0x0008E17C
	public GameObject AddConditionInfo(IInventoryItem item)
	{
		if (item != null && item.datablock.DoesLoseCondition())
		{
			Color col = Color.green;
			if (item.condition <= 0.6f)
			{
				col = Color.yellow;
			}
			else if (item.IsBroken())
			{
				col = Color.red;
			}
			float num = 100f * item.condition;
			float num2 = 100f * item.maxcondition;
			return this.AddBasicLabel("Condition : " + num.ToString("0") + "/" + num2.ToString("0"), 15f, col);
		}
		return null;
	}

	// Token: 0x06002576 RID: 9590 RVA: 0x00090024 File Offset: 0x0008E224
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

	// Token: 0x06002577 RID: 9591 RVA: 0x000900D0 File Offset: 0x0008E2D0
	public float GetContentHeight()
	{
		return NGUIMath.CalculateRelativeWidgetBounds(this.addParent.transform, this.addParent.transform).size.y;
	}

	// Token: 0x06002578 RID: 9592 RVA: 0x00090108 File Offset: 0x0008E308
	public void FinishPopulating()
	{
	}

	// Token: 0x04001222 RID: 4642
	public UISlicedSprite _background;

	// Token: 0x04001223 RID: 4643
	public static ItemToolTip _globalToolTip;

	// Token: 0x04001224 RID: 4644
	public GameObject addParent;

	// Token: 0x04001225 RID: 4645
	public GameObject itemTitlePrefab;

	// Token: 0x04001226 RID: 4646
	public GameObject sectionTitlePrefab;

	// Token: 0x04001227 RID: 4647
	public GameObject itemDescriptionPrefab;

	// Token: 0x04001228 RID: 4648
	public GameObject basicLabelPrefab;

	// Token: 0x04001229 RID: 4649
	public GameObject progressStatPrefab;

	// Token: 0x0400122A RID: 4650
	public Camera uiCamera;

	// Token: 0x0400122B RID: 4651
	private Plane planeTest;
}
