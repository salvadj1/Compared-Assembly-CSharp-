using System;
using UnityEngine;

// Token: 0x020003F7 RID: 1015
public class PopupNotice : MonoBehaviour
{
	// Token: 0x0600255C RID: 9564 RVA: 0x0008F784 File Offset: 0x0008D984
	public void Setup(float fSeconds, string strIcon, string strText)
	{
		Vector2 size = base.transform.parent.GetComponent<dfPanel>().Size;
		dfPanel component = base.GetComponent<dfPanel>();
		Vector2 vector = this.labelText.Font.MeasureText(strText, this.labelText.FontSize, this.labelText.FontStyle);
		this.labelText.Width = vector.x + 16f;
		component.Width = this.labelText.RelativePosition.x + this.labelText.Width + 8f;
		Vector2 vector2 = default(Vector2);
		vector2.x = (size.x - component.Width) / 2f + Random.Range(-32f, 32f);
		vector2.y = component.Height * -1f + Random.Range(-32f, 32f);
		component.RelativePosition = vector2;
		this.labelIcon.Text = strIcon;
		this.labelText.Text = strText;
		component.BringToFront();
		base.Invoke("PlayOut", fSeconds);
	}

	// Token: 0x0600255D RID: 9565 RVA: 0x0008F8A8 File Offset: 0x0008DAA8
	public void PlayOut()
	{
		this.tweenOut.Play();
		Object.Destroy(base.gameObject, this.tweenOut.Length);
	}

	// Token: 0x0400121D RID: 4637
	public dfRichTextLabel labelIcon;

	// Token: 0x0400121E RID: 4638
	public dfRichTextLabel labelText;

	// Token: 0x0400121F RID: 4639
	public dfTweenVector3 tweenOut;
}
