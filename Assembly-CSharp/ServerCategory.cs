using System;
using UnityEngine;

// Token: 0x020004BB RID: 1211
public class ServerCategory : MonoBehaviour
{
	// Token: 0x06002958 RID: 10584 RVA: 0x00097B88 File Offset: 0x00095D88
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

	// Token: 0x06002959 RID: 10585 RVA: 0x00097BD0 File Offset: 0x00095DD0
	public void OnSelected()
	{
		global::ServerBrowser serverBrowser = Object.FindObjectOfType<global::ServerBrowser>();
		serverBrowser.SwitchCategory(this.categoryId);
	}

	// Token: 0x0600295A RID: 10586 RVA: 0x00097BF0 File Offset: 0x00095DF0
	public void CategoryChanged(int iCategory)
	{
		if (iCategory == this.categoryId)
		{
			base.GetComponent<global::dfControl>().Opacity = 1f;
		}
		else
		{
			base.GetComponent<global::dfControl>().Opacity = 0.5f;
		}
	}

	// Token: 0x040013EC RID: 5100
	public global::dfLabel serverCount;

	// Token: 0x040013ED RID: 5101
	public int categoryId;

	// Token: 0x040013EE RID: 5102
	public bool activeCategory;
}
