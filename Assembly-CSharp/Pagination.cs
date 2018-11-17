using System;
using UnityEngine;

// Token: 0x020004BE RID: 1214
public class Pagination : MonoBehaviour
{
	// Token: 0x14000017 RID: 23
	// (add) Token: 0x06002965 RID: 10597 RVA: 0x00097F6C File Offset: 0x0009616C
	// (remove) Token: 0x06002966 RID: 10598 RVA: 0x00097F88 File Offset: 0x00096188
	public event global::Pagination.SwitchToPage OnPageSwitch;

	// Token: 0x06002967 RID: 10599 RVA: 0x00097FA4 File Offset: 0x000961A4
	public void Setup(int iPages, int iCurrentPage)
	{
		if (this.pageCount == iPages && this.pageCurrent == iCurrentPage)
		{
			return;
		}
		this.pageCount = iPages;
		this.pageCurrent = iCurrentPage;
		global::dfControl[] componentsInChildren = base.gameObject.GetComponentsInChildren<global::dfControl>();
		foreach (global::dfControl dfControl in componentsInChildren)
		{
			if (!(dfControl.gameObject == base.gameObject))
			{
				Object.Destroy(dfControl.gameObject);
			}
		}
		if (this.pageCount <= 1)
		{
			return;
		}
		global::dfControl component = base.GetComponent<global::dfControl>();
		bool flag = true;
		Vector3 position;
		position..ctor(0f, 0f, 0f);
		for (int j = 0; j < this.pageCount; j++)
		{
			if (this.buttonGroups - j <= 0 && j < this.pageCount - this.buttonGroups && Math.Abs(j - this.pageCurrent) > this.buttonGroups / 2)
			{
				if (flag)
				{
					this.DropSpacer(ref position);
				}
				flag = false;
			}
			else
			{
				GameObject gameObject = (GameObject)Object.Instantiate(this.clickableButton);
				global::dfButton component2 = gameObject.GetComponent<global::dfButton>();
				component.AddControl(component2);
				component2.Tooltip = j.ToString();
				component2.MouseDown += this.OnButtonClicked;
				component2.Text = (j + 1).ToString();
				component2.Invalidate();
				if (j == this.pageCurrent)
				{
					component2.Disable();
				}
				component2.Position = position;
				position.x += component2.Width + 5f;
				flag = true;
			}
		}
		component.Width = position.x;
	}

	// Token: 0x06002968 RID: 10600 RVA: 0x0009816C File Offset: 0x0009636C
	public void DropSpacer(ref Vector3 vPos)
	{
		if (!this.spacerLabel)
		{
			return;
		}
		global::dfControl component = base.GetComponent<global::dfControl>();
		GameObject gameObject = (GameObject)Object.Instantiate(this.spacerLabel);
		global::dfControl component2 = gameObject.GetComponent<global::dfControl>();
		component.AddControl(component2);
		component2.Position = vPos;
		vPos.x += component2.Width + 5f;
	}

	// Token: 0x06002969 RID: 10601 RVA: 0x000981D8 File Offset: 0x000963D8
	public void OnButtonClicked(global::dfControl control, global::dfMouseEventArgs mouseEvent)
	{
		int num = int.Parse(control.Tooltip);
		this.Setup(this.pageCount, num);
		if (this.OnPageSwitch != null)
		{
			this.OnPageSwitch(num);
		}
	}

	// Token: 0x040013F6 RID: 5110
	public GameObject clickableButton;

	// Token: 0x040013F7 RID: 5111
	public GameObject spacerLabel;

	// Token: 0x040013F8 RID: 5112
	public int buttonGroups = 2;

	// Token: 0x040013F9 RID: 5113
	protected int pageCount;

	// Token: 0x040013FA RID: 5114
	protected int pageCurrent;

	// Token: 0x020004BF RID: 1215
	// (Invoke) Token: 0x0600296B RID: 10603
	public delegate void SwitchToPage(int iPage);
}
