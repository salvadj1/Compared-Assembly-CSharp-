using System;
using UnityEngine;

// Token: 0x020003F6 RID: 1014
public class PopupInventory : MonoBehaviour
{
	// Token: 0x06002559 RID: 9561 RVA: 0x0008F5C4 File Offset: 0x0008D7C4
	public void Setup(float fSeconds, string strText)
	{
		Vector2 size = base.transform.parent.GetComponent<dfPanel>().Size;
		dfPanel component = base.GetComponent<dfPanel>();
		Vector2 vector = this.labelText.Font.MeasureText(strText, this.labelText.FontSize, this.labelText.FontStyle);
		this.labelText.Width = vector.x + 16f;
		component.Width = this.labelText.RelativePosition.x + this.labelText.Width + 8f;
		Vector2 vector2 = default(Vector2);
		vector2.x = size.x + Random.Range(-16f, 16f);
		vector2.y = size.y * 0.7f + Random.Range(-16f, 16f);
		vector2.y += ((float)PopupInventory.iYPos / 6f - 0.5f) * size.y * 0.2f;
		component.RelativePosition = vector2;
		PopupInventory.iYPos++;
		if (PopupInventory.iYPos > 5)
		{
			PopupInventory.iYPos = 0;
		}
		Vector3 endValue = this.tweenOut.EndValue;
		endValue.y = Random.Range(-100f, 100f);
		this.tweenOut.EndValue = endValue;
		component.BringToFront();
		this.labelText.Text = strText;
		base.Invoke("PlayOut", fSeconds);
	}

	// Token: 0x0600255A RID: 9562 RVA: 0x0008F74C File Offset: 0x0008D94C
	public void PlayOut()
	{
		this.tweenOut.Play();
		Object.Destroy(base.gameObject, this.tweenOut.Length);
	}

	// Token: 0x0400121A RID: 4634
	public dfRichTextLabel labelText;

	// Token: 0x0400121B RID: 4635
	public dfTweenVector3 tweenOut;

	// Token: 0x0400121C RID: 4636
	private static int iYPos;
}
