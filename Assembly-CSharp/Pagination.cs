using System;
using UnityEngine;

// Token: 0x0200040A RID: 1034
public class Pagination : MonoBehaviour
{
	// Token: 0x14000017 RID: 23
	// (add) Token: 0x060025DF RID: 9695 RVA: 0x000920A8 File Offset: 0x000902A8
	// (remove) Token: 0x060025E0 RID: 9696 RVA: 0x000920C4 File Offset: 0x000902C4
	public event Pagination.SwitchToPage OnPageSwitch;

	// Token: 0x060025E1 RID: 9697 RVA: 0x000920E0 File Offset: 0x000902E0
	public void Setup(int iPages, int iCurrentPage)
	{
		if (this.pageCount == iPages && this.pageCurrent == iCurrentPage)
		{
			return;
		}
		this.pageCount = iPages;
		this.pageCurrent = iCurrentPage;
		dfControl[] componentsInChildren = base.gameObject.GetComponentsInChildren<dfControl>();
		foreach (dfControl dfControl in componentsInChildren)
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
		dfControl component = base.GetComponent<dfControl>();
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
				dfButton component2 = gameObject.GetComponent<dfButton>();
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

	// Token: 0x060025E2 RID: 9698 RVA: 0x000922A8 File Offset: 0x000904A8
	public void DropSpacer(ref Vector3 vPos)
	{
		if (!this.spacerLabel)
		{
			return;
		}
		dfControl component = base.GetComponent<dfControl>();
		GameObject gameObject = (GameObject)Object.Instantiate(this.spacerLabel);
		dfControl component2 = gameObject.GetComponent<dfControl>();
		component.AddControl(component2);
		component2.Position = vPos;
		vPos.x += component2.Width + 5f;
	}

	// Token: 0x060025E3 RID: 9699 RVA: 0x00092314 File Offset: 0x00090514
	public void OnButtonClicked(dfControl control, dfMouseEventArgs mouseEvent)
	{
		int num = int.Parse(control.Tooltip);
		this.Setup(this.pageCount, num);
		if (this.OnPageSwitch != null)
		{
			this.OnPageSwitch(num);
		}
	}

	// Token: 0x04001276 RID: 4726
	public GameObject clickableButton;

	// Token: 0x04001277 RID: 4727
	public GameObject spacerLabel;

	// Token: 0x04001278 RID: 4728
	public int buttonGroups = 2;

	// Token: 0x04001279 RID: 4729
	protected int pageCount;

	// Token: 0x0400127A RID: 4730
	protected int pageCurrent;

	// Token: 0x020008D9 RID: 2265
	// (Invoke) Token: 0x06004D3C RID: 19772
	public delegate void SwitchToPage(int iPage);
}
