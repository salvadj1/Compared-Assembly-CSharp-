using System;
using UnityEngine;

// Token: 0x020004A8 RID: 1192
public class PopupNotice : MonoBehaviour
{
	// Token: 0x060028D4 RID: 10452 RVA: 0x000955BC File Offset: 0x000937BC
	public void Setup(float fSeconds, string strIcon, string strText)
	{
		Vector2 size = base.transform.parent.GetComponent<global::dfPanel>().Size;
		global::dfPanel component = base.GetComponent<global::dfPanel>();
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

	// Token: 0x060028D5 RID: 10453 RVA: 0x000956E0 File Offset: 0x000938E0
	public void PlayOut()
	{
		this.tweenOut.Play();
		Object.Destroy(base.gameObject, this.tweenOut.Length);
	}

	// Token: 0x0400139A RID: 5018
	public global::dfRichTextLabel labelIcon;

	// Token: 0x0400139B RID: 5019
	public global::dfRichTextLabel labelText;

	// Token: 0x0400139C RID: 5020
	public global::dfTweenVector3 tweenOut;
}
