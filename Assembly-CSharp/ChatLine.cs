using System;
using UnityEngine;

// Token: 0x0200049C RID: 1180
public class ChatLine : MonoBehaviour
{
	// Token: 0x0600288D RID: 10381 RVA: 0x00094520 File Offset: 0x00092720
	public void Setup(string name, string text)
	{
		this.lblName.Text = name;
		this.lblText.Text = text;
	}

	// Token: 0x0400137F RID: 4991
	public global::dfLabel lblName;

	// Token: 0x04001380 RID: 4992
	public global::dfLabel lblText;
}
