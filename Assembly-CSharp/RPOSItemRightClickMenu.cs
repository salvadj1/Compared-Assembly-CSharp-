using System;
using UnityEngine;

// Token: 0x020004CD RID: 1229
public class RPOSItemRightClickMenu : MonoBehaviour
{
	// Token: 0x06002A49 RID: 10825 RVA: 0x0009D06C File Offset: 0x0009B26C
	public void Awake()
	{
		if (this.uiCamera == null)
		{
			this.uiCamera = global::NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		this.planeTest = new Plane(this.uiCamera.transform.forward * 1f, new Vector3(0f, 0f, 2f));
		base.GetComponent<global::UIPanel>().enabled = false;
	}

	// Token: 0x06002A4A RID: 10826 RVA: 0x0009D0E8 File Offset: 0x0009B2E8
	public void AddRightClickEntry(string entry)
	{
		GameObject gameObject = global::NGUITools.AddChild(base.gameObject, this._buttonPrefab);
		gameObject.GetComponentInChildren<global::UILabel>().text = entry;
		global::UIEventListener uieventListener = global::UIEventListener.Get(gameObject);
		global::UIEventListener uieventListener2 = uieventListener;
		uieventListener2.onClick = (global::UIEventListener.VoidDelegate)Delegate.Combine(uieventListener2.onClick, new global::UIEventListener.VoidDelegate(this.EntryClicked));
		gameObject.name = entry;
		Vector3 localPosition = gameObject.transform.localPosition;
		localPosition.y = this.lastHeight;
		gameObject.transform.localPosition = localPosition;
		this.lastHeight -= gameObject.GetComponentInChildren<global::UISlicedSprite>().transform.localScale.y;
	}

	// Token: 0x06002A4B RID: 10827 RVA: 0x0009D190 File Offset: 0x0009B390
	public virtual void SetItem(global::IInventoryItem item)
	{
		this.ClearChildren();
		this._observedItem = item;
		int num = item.datablock.RetreiveMenuOptions(item, global::RPOSItemRightClickMenu.menuItemBuffer, 0);
		for (int i = 0; i < num; i++)
		{
			this.AddRightClickEntry(global::RPOSItemRightClickMenu.menuItemBuffer[i].ToString());
		}
		global::UICamera.PopupPanel(base.GetComponent<global::UIPanel>());
	}

	// Token: 0x06002A4C RID: 10828 RVA: 0x0009D1F4 File Offset: 0x0009B3F4
	private void PopupStart()
	{
		this.RepositionAtCursor();
		base.GetComponent<global::UIPanel>().enabled = true;
	}

	// Token: 0x06002A4D RID: 10829 RVA: 0x0009D208 File Offset: 0x0009B408
	private void PopupEnd()
	{
		base.GetComponent<global::UIPanel>().enabled = false;
	}

	// Token: 0x06002A4E RID: 10830 RVA: 0x0009D218 File Offset: 0x0009B418
	public void ClearChildren()
	{
		global::UIButton[] componentsInChildren = base.GetComponentsInChildren<global::UIButton>();
		foreach (global::UIButton uibutton in componentsInChildren)
		{
			Object.Destroy(uibutton.gameObject);
		}
		this.lastHeight = 0f;
	}

	// Token: 0x06002A4F RID: 10831 RVA: 0x0009D25C File Offset: 0x0009B45C
	public void EntryClicked(GameObject go)
	{
		try
		{
			if (this._observedItem != null)
			{
				global::InventoryItem.MenuItem? menuItem;
				try
				{
					menuItem = new global::InventoryItem.MenuItem?((global::InventoryItem.MenuItem)((byte)Enum.Parse(typeof(global::InventoryItem.MenuItem), go.name, true)));
				}
				catch (Exception ex)
				{
					menuItem = null;
					Debug.LogException(ex);
				}
				if (menuItem != null)
				{
					this._observedItem.OnMenuOption(menuItem.Value);
				}
			}
		}
		catch (Exception ex2)
		{
			Debug.LogException(ex2);
		}
		finally
		{
			global::UICamera.UnPopupPanel(base.GetComponent<global::UIPanel>());
		}
	}

	// Token: 0x06002A50 RID: 10832 RVA: 0x0009D33C File Offset: 0x0009B53C
	public void RepositionAtCursor()
	{
		Vector3 vector = global::UICamera.lastMousePosition;
		Ray ray = this.uiCamera.ScreenPointToRay(vector);
		float num = 0f;
		if (this.planeTest.Raycast(ray, ref num))
		{
			base.transform.position = ray.GetPoint(num);
			global::AABBox aabbox = global::NGUIMath.CalculateRelativeWidgetBounds(base.transform);
			float num2 = base.transform.localPosition.x + aabbox.size.x - (float)Screen.width;
			if (num2 > 0f)
			{
				base.transform.SetLocalPositionX(base.transform.localPosition.x - num2);
			}
			float num3 = base.transform.localPosition.y + aabbox.size.y - (float)Screen.height;
			if (num3 > 0f)
			{
				base.transform.SetLocalPositionY(base.transform.localPosition.y - num3);
			}
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y, -180f);
		}
	}

	// Token: 0x04001486 RID: 5254
	private global::IInventoryItem _observedItem;

	// Token: 0x04001487 RID: 5255
	public GameObject _buttonPrefab;

	// Token: 0x04001488 RID: 5256
	public Camera uiCamera;

	// Token: 0x04001489 RID: 5257
	private Plane planeTest;

	// Token: 0x0400148A RID: 5258
	public float lastHeight;

	// Token: 0x0400148B RID: 5259
	private static readonly global::InventoryItem.MenuItem[] menuItemBuffer = new global::InventoryItem.MenuItem[30];
}
