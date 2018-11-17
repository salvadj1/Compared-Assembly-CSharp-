using System;
using UnityEngine;

// Token: 0x02000418 RID: 1048
public class RPOSItemRightClickMenu : MonoBehaviour
{
	// Token: 0x060026BF RID: 9919 RVA: 0x000971A8 File Offset: 0x000953A8
	public void Awake()
	{
		if (this.uiCamera == null)
		{
			this.uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		this.planeTest = new Plane(this.uiCamera.transform.forward * 1f, new Vector3(0f, 0f, 2f));
		base.GetComponent<UIPanel>().enabled = false;
	}

	// Token: 0x060026C0 RID: 9920 RVA: 0x00097224 File Offset: 0x00095424
	public void AddRightClickEntry(string entry)
	{
		GameObject gameObject = NGUITools.AddChild(base.gameObject, this._buttonPrefab);
		gameObject.GetComponentInChildren<UILabel>().text = entry;
		UIEventListener uieventListener = UIEventListener.Get(gameObject);
		UIEventListener uieventListener2 = uieventListener;
		uieventListener2.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uieventListener2.onClick, new UIEventListener.VoidDelegate(this.EntryClicked));
		gameObject.name = entry;
		Vector3 localPosition = gameObject.transform.localPosition;
		localPosition.y = this.lastHeight;
		gameObject.transform.localPosition = localPosition;
		this.lastHeight -= gameObject.GetComponentInChildren<UISlicedSprite>().transform.localScale.y;
	}

	// Token: 0x060026C1 RID: 9921 RVA: 0x000972CC File Offset: 0x000954CC
	public virtual void SetItem(IInventoryItem item)
	{
		this.ClearChildren();
		this._observedItem = item;
		int num = item.datablock.RetreiveMenuOptions(item, RPOSItemRightClickMenu.menuItemBuffer, 0);
		for (int i = 0; i < num; i++)
		{
			this.AddRightClickEntry(RPOSItemRightClickMenu.menuItemBuffer[i].ToString());
		}
		UICamera.PopupPanel(base.GetComponent<UIPanel>());
	}

	// Token: 0x060026C2 RID: 9922 RVA: 0x00097330 File Offset: 0x00095530
	private void PopupStart()
	{
		this.RepositionAtCursor();
		base.GetComponent<UIPanel>().enabled = true;
	}

	// Token: 0x060026C3 RID: 9923 RVA: 0x00097344 File Offset: 0x00095544
	private void PopupEnd()
	{
		base.GetComponent<UIPanel>().enabled = false;
	}

	// Token: 0x060026C4 RID: 9924 RVA: 0x00097354 File Offset: 0x00095554
	public void ClearChildren()
	{
		UIButton[] componentsInChildren = base.GetComponentsInChildren<UIButton>();
		foreach (UIButton uibutton in componentsInChildren)
		{
			Object.Destroy(uibutton.gameObject);
		}
		this.lastHeight = 0f;
	}

	// Token: 0x060026C5 RID: 9925 RVA: 0x00097398 File Offset: 0x00095598
	public void EntryClicked(GameObject go)
	{
		try
		{
			if (this._observedItem != null)
			{
				InventoryItem.MenuItem? menuItem;
				try
				{
					menuItem = new InventoryItem.MenuItem?((InventoryItem.MenuItem)((byte)Enum.Parse(typeof(InventoryItem.MenuItem), go.name, true)));
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
			UICamera.UnPopupPanel(base.GetComponent<UIPanel>());
		}
	}

	// Token: 0x060026C6 RID: 9926 RVA: 0x00097478 File Offset: 0x00095678
	public void RepositionAtCursor()
	{
		Vector3 vector = UICamera.lastMousePosition;
		Ray ray = this.uiCamera.ScreenPointToRay(vector);
		float num = 0f;
		if (this.planeTest.Raycast(ray, ref num))
		{
			base.transform.position = ray.GetPoint(num);
			AABBox aabbox = NGUIMath.CalculateRelativeWidgetBounds(base.transform);
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

	// Token: 0x04001306 RID: 4870
	private IInventoryItem _observedItem;

	// Token: 0x04001307 RID: 4871
	public GameObject _buttonPrefab;

	// Token: 0x04001308 RID: 4872
	public Camera uiCamera;

	// Token: 0x04001309 RID: 4873
	private Plane planeTest;

	// Token: 0x0400130A RID: 4874
	public float lastHeight;

	// Token: 0x0400130B RID: 4875
	private static readonly InventoryItem.MenuItem[] menuItemBuffer = new InventoryItem.MenuItem[30];
}
