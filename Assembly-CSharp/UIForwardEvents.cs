using System;
using UnityEngine;

// Token: 0x02000772 RID: 1906
[AddComponentMenu("NGUI/Interaction/Forward Events")]
public class UIForwardEvents : MonoBehaviour
{
	// Token: 0x06004543 RID: 17731 RVA: 0x00110C3C File Offset: 0x0010EE3C
	private void OnHover(bool isOver)
	{
		if (this.onHover && this.target != null)
		{
			this.target.SendMessage("OnHover", isOver, 1);
		}
	}

	// Token: 0x06004544 RID: 17732 RVA: 0x00110C74 File Offset: 0x0010EE74
	private void OnPress(bool pressed)
	{
		if (this.onPress && this.target != null)
		{
			this.target.SendMessage("OnPress", pressed, 1);
		}
	}

	// Token: 0x06004545 RID: 17733 RVA: 0x00110CAC File Offset: 0x0010EEAC
	private void OnClick()
	{
		if (this.onClick && this.target != null)
		{
			this.target.SendMessage("OnClick", 1);
		}
	}

	// Token: 0x06004546 RID: 17734 RVA: 0x00110CDC File Offset: 0x0010EEDC
	private void OnDoubleClick()
	{
		if (this.onDoubleClick && this.target != null)
		{
			this.target.SendMessage("OnDoubleClick", 1);
		}
	}

	// Token: 0x06004547 RID: 17735 RVA: 0x00110D0C File Offset: 0x0010EF0C
	private void OnSelect(bool selected)
	{
		if (this.onSelect && this.target != null)
		{
			this.target.SendMessage("OnSelect", selected, 1);
		}
	}

	// Token: 0x06004548 RID: 17736 RVA: 0x00110D44 File Offset: 0x0010EF44
	private void OnDrag(Vector2 delta)
	{
		if (this.onDrag && this.target != null)
		{
			this.target.SendMessage("OnDrag", delta, 1);
		}
	}

	// Token: 0x06004549 RID: 17737 RVA: 0x00110D7C File Offset: 0x0010EF7C
	private void OnDrop(GameObject go)
	{
		if (this.onDrop && this.target != null)
		{
			this.target.SendMessage("OnDrop", go, 1);
		}
	}

	// Token: 0x0600454A RID: 17738 RVA: 0x00110DB8 File Offset: 0x0010EFB8
	private void OnInput(string text)
	{
		if (this.onInput && this.target != null)
		{
			this.target.SendMessage("OnInput", text, 1);
		}
	}

	// Token: 0x0600454B RID: 17739 RVA: 0x00110DF4 File Offset: 0x0010EFF4
	private void OnSubmit()
	{
		if (this.onSubmit && this.target != null)
		{
			this.target.SendMessage("OnSubmit", 1);
		}
	}

	// Token: 0x04002598 RID: 9624
	public GameObject target;

	// Token: 0x04002599 RID: 9625
	public bool onHover;

	// Token: 0x0400259A RID: 9626
	public bool onPress;

	// Token: 0x0400259B RID: 9627
	public bool onClick;

	// Token: 0x0400259C RID: 9628
	public bool onDoubleClick;

	// Token: 0x0400259D RID: 9629
	public bool onSelect;

	// Token: 0x0400259E RID: 9630
	public bool onDrag;

	// Token: 0x0400259F RID: 9631
	public bool onDrop;

	// Token: 0x040025A0 RID: 9632
	public bool onInput;

	// Token: 0x040025A1 RID: 9633
	public bool onSubmit;
}
