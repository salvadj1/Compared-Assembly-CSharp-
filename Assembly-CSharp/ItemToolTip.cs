using System;
using UnityEngine;

// Token: 0x020004AC RID: 1196
public class ItemToolTip : MonoBehaviour
{
	// Token: 0x060028E0 RID: 10464 RVA: 0x00095910 File Offset: 0x00093B10
	public static global::ItemToolTip Get()
	{
		return global::ItemToolTip._globalToolTip;
	}

	// Token: 0x060028E1 RID: 10465 RVA: 0x00095918 File Offset: 0x00093B18
	private void Awake()
	{
		global::ItemToolTip._globalToolTip = this;
		if (this.uiCamera == null)
		{
			this.uiCamera = global::NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		this.planeTest = new Plane(this.uiCamera.transform.forward * 1f, new Vector3(0f, 0f, 2f));
	}

	// Token: 0x060028E2 RID: 10466 RVA: 0x0009598C File Offset: 0x00093B8C
	private void Update()
	{
	}

	// Token: 0x060028E3 RID: 10467 RVA: 0x00095990 File Offset: 0x00093B90
	public void RepositionAtCursor()
	{
		Vector3 vector = global::UICamera.lastMousePosition;
		Ray ray = this.uiCamera.ScreenPointToRay(vector);
		float num = 0f;
		if (this.planeTest.Raycast(ray, ref num))
		{
			base.transform.position = ray.GetPoint(num);
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y, -180f);
			global::AABBox aabbox = global::NGUIMath.CalculateRelativeWidgetBounds(base.transform);
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

	// Token: 0x060028E4 RID: 10468 RVA: 0x00095AE8 File Offset: 0x00093CE8
	public static void SetToolTip(global::ItemDataBlock itemdb, global::IInventoryItem item = null)
	{
		global::ItemToolTip.Get().Internal_SetToolTip(itemdb, item);
		global::ItemToolTip.Get().RepositionAtCursor();
	}

	// Token: 0x060028E5 RID: 10469 RVA: 0x00095B00 File Offset: 0x00093D00
	public void Internal_SetToolTip(global::ItemDataBlock itemdb, global::IInventoryItem item)
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

	// Token: 0x060028E6 RID: 10470 RVA: 0x00095B88 File Offset: 0x00093D88
	public void ClearContents()
	{
		foreach (object obj in this.addParent.transform)
		{
			Transform transform = (Transform)obj;
			Object.Destroy(transform.gameObject);
		}
	}

	// Token: 0x060028E7 RID: 10471 RVA: 0x00095C00 File Offset: 0x00093E00
	public void SetVisible(bool vis)
	{
		base.GetComponent<global::UIPanel>().enabled = vis;
	}

	// Token: 0x060028E8 RID: 10472 RVA: 0x00095C10 File Offset: 0x00093E10
	public GameObject AddItemTitle(global::ItemDataBlock itemdb, global::IInventoryItem itemInstance = null, float aboveSpace = 0f)
	{
		float contentHeight = this.GetContentHeight();
		GameObject gameObject = global::NGUITools.AddChild(this.addParent, this.itemTitlePrefab);
		gameObject.GetComponentInChildren<global::UILabel>().text = ((itemInstance == null) ? itemdb.name : itemInstance.toolTip);
		global::UITexture componentInChildren = gameObject.GetComponentInChildren<global::UITexture>();
		componentInChildren.material = componentInChildren.material.Clone();
		componentInChildren.material.Set("_MainTex", itemdb.GetIconTexture());
		componentInChildren.color = ((itemInstance == null || !itemInstance.IsBroken()) ? Color.white : Color.red);
		gameObject.transform.SetLocalPositionY(-(contentHeight + aboveSpace));
		return gameObject;
	}

	// Token: 0x060028E9 RID: 10473 RVA: 0x00095CBC File Offset: 0x00093EBC
	public GameObject AddSectionTitle(string text, float aboveSpace)
	{
		float contentHeight = this.GetContentHeight();
		GameObject gameObject = global::NGUITools.AddChild(this.addParent, this.sectionTitlePrefab);
		gameObject.GetComponentInChildren<global::UILabel>().text = text;
		gameObject.transform.SetLocalPositionY(-(contentHeight + aboveSpace));
		return gameObject;
	}

	// Token: 0x060028EA RID: 10474 RVA: 0x00095D00 File Offset: 0x00093F00
	public GameObject AddItemDescription(global::ItemDataBlock item, float aboveSpace)
	{
		float contentHeight = this.GetContentHeight();
		GameObject gameObject = global::NGUITools.AddChild(this.addParent, this.itemDescriptionPrefab);
		gameObject.transform.FindChild("DescText").GetComponent<global::UILabel>().text = item.GetItemDescription();
		gameObject.transform.SetLocalPositionY(-(contentHeight + aboveSpace));
		return null;
	}

	// Token: 0x060028EB RID: 10475 RVA: 0x00095D58 File Offset: 0x00093F58
	public GameObject AddBasicLabel(string text, float aboveSpace)
	{
		return this.AddBasicLabel(text, aboveSpace, Color.white);
	}

	// Token: 0x060028EC RID: 10476 RVA: 0x00095D68 File Offset: 0x00093F68
	public GameObject AddBasicLabel(string text, float aboveSpace, Color col)
	{
		float contentHeight = this.GetContentHeight();
		GameObject gameObject = global::NGUITools.AddChild(this.addParent, this.basicLabelPrefab);
		global::UILabel componentInChildren = gameObject.GetComponentInChildren<global::UILabel>();
		componentInChildren.text = text;
		componentInChildren.color = col;
		gameObject.transform.SetLocalPositionY(-(contentHeight + aboveSpace));
		return gameObject;
	}

	// Token: 0x060028ED RID: 10477 RVA: 0x00095DB4 File Offset: 0x00093FB4
	public GameObject AddConditionInfo(global::IInventoryItem item)
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

	// Token: 0x060028EE RID: 10478 RVA: 0x00095E5C File Offset: 0x0009405C
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

	// Token: 0x060028EF RID: 10479 RVA: 0x00095F08 File Offset: 0x00094108
	public float GetContentHeight()
	{
		return global::NGUIMath.CalculateRelativeWidgetBounds(this.addParent.transform, this.addParent.transform).size.y;
	}

	// Token: 0x060028F0 RID: 10480 RVA: 0x00095F40 File Offset: 0x00094140
	public void FinishPopulating()
	{
	}

	// Token: 0x0400139F RID: 5023
	public global::UISlicedSprite _background;

	// Token: 0x040013A0 RID: 5024
	public static global::ItemToolTip _globalToolTip;

	// Token: 0x040013A1 RID: 5025
	public GameObject addParent;

	// Token: 0x040013A2 RID: 5026
	public GameObject itemTitlePrefab;

	// Token: 0x040013A3 RID: 5027
	public GameObject sectionTitlePrefab;

	// Token: 0x040013A4 RID: 5028
	public GameObject itemDescriptionPrefab;

	// Token: 0x040013A5 RID: 5029
	public GameObject basicLabelPrefab;

	// Token: 0x040013A6 RID: 5030
	public GameObject progressStatPrefab;

	// Token: 0x040013A7 RID: 5031
	public Camera uiCamera;

	// Token: 0x040013A8 RID: 5032
	private Plane planeTest;
}
