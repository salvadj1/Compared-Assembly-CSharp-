using System;
using UnityEngine;

// Token: 0x02000407 RID: 1031
public class ServerCategory : MonoBehaviour
{
	// Token: 0x060025D2 RID: 9682 RVA: 0x00091CC4 File Offset: 0x0008FEC4
	public void UpdateServerCount(int iCount)
	{
		if (iCount == 0)
		{
			this.serverCount.Hide();
		}
		else
		{
			this.serverCount.Show();
		}
		this.serverCount.Text = iCount.ToString("#,##0");
	}

	// Token: 0x060025D3 RID: 9683 RVA: 0x00091D0C File Offset: 0x0008FF0C
	public void OnSelected()
	{
		ServerBrowser serverBrowser = Object.FindObjectOfType<ServerBrowser>();
		serverBrowser.SwitchCategory(this.categoryId);
	}

	// Token: 0x060025D4 RID: 9684 RVA: 0x00091D2C File Offset: 0x0008FF2C
	public void CategoryChanged(int iCategory)
	{
		if (iCategory == this.categoryId)
		{
			base.GetComponent<dfControl>().Opacity = 1f;
		}
		else
		{
			base.GetComponent<dfControl>().Opacity = 0.5f;
		}
	}

	// Token: 0x0400126C RID: 4716
	public dfLabel serverCount;

	// Token: 0x0400126D RID: 4717
	public int categoryId;

	// Token: 0x0400126E RID: 4718
	public bool activeCategory;
}
