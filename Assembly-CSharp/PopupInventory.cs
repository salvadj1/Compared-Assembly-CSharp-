using System;
using UnityEngine;

// Token: 0x020004A7 RID: 1191
public class PopupInventory : MonoBehaviour
{
	// Token: 0x060028D1 RID: 10449 RVA: 0x000953FC File Offset: 0x000935FC
	public void Setup(float fSeconds, string strText)
	{
		Vector2 size = base.transform.parent.GetComponent<global::dfPanel>().Size;
		global::dfPanel component = base.GetComponent<global::dfPanel>();
		Vector2 vector = this.labelText.Font.MeasureText(strText, this.labelText.FontSize, this.labelText.FontStyle);
		this.labelText.Width = vector.x + 16f;
		component.Width = this.labelText.RelativePosition.x + this.labelText.Width + 8f;
		Vector2 vector2 = default(Vector2);
		vector2.x = size.x + Random.Range(-16f, 16f);
		vector2.y = size.y * 0.7f + Random.Range(-16f, 16f);
		vector2.y += ((float)global::PopupInventory.iYPos / 6f - 0.5f) * size.y * 0.2f;
		component.RelativePosition = vector2;
		global::PopupInventory.iYPos++;
		if (global::PopupInventory.iYPos > 5)
		{
			global::PopupInventory.iYPos = 0;
		}
		Vector3 endValue = this.tweenOut.EndValue;
		endValue.y = Random.Range(-100f, 100f);
		this.tweenOut.EndValue = endValue;
		component.BringToFront();
		this.labelText.Text = strText;
		base.Invoke("PlayOut", fSeconds);
	}

	// Token: 0x060028D2 RID: 10450 RVA: 0x00095584 File Offset: 0x00093784
	public void PlayOut()
	{
		this.tweenOut.Play();
		Object.Destroy(base.gameObject, this.tweenOut.Length);
	}

	// Token: 0x04001397 RID: 5015
	public global::dfRichTextLabel labelText;

	// Token: 0x04001398 RID: 5016
	public global::dfTweenVector3 tweenOut;

	// Token: 0x04001399 RID: 5017
	private static int iYPos;
}
