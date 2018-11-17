using System;
using UnityEngine;

// Token: 0x020003EC RID: 1004
public class ChatLine : MonoBehaviour
{
	// Token: 0x0600251B RID: 9499 RVA: 0x0008EB34 File Offset: 0x0008CD34
	public void Setup(string name, string text)
	{
		this.lblName.Text = name;
		this.lblText.Text = text;
	}

	// Token: 0x04001205 RID: 4613
	public dfLabel lblName;

	// Token: 0x04001206 RID: 4614
	public dfLabel lblText;
}
