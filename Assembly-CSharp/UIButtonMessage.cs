using System;
using UnityEngine;

// Token: 0x0200075E RID: 1886
[AddComponentMenu("NGUI/Interaction/Button Message")]
public class UIButtonMessage : MonoBehaviour
{
	// Token: 0x060044C5 RID: 17605 RVA: 0x0010D734 File Offset: 0x0010B934
	private void Start()
	{
		this.mStarted = true;
	}

	// Token: 0x060044C6 RID: 17606 RVA: 0x0010D740 File Offset: 0x0010B940
	private void OnEnable()
	{
		if (this.mStarted && this.mHighlighted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x060044C7 RID: 17607 RVA: 0x0010D76C File Offset: 0x0010B96C
	private void OnHover(bool isOver)
	{
		if (base.enabled)
		{
			if ((isOver && this.trigger == UIButtonMessage.Trigger.OnMouseOver) || (!isOver && this.trigger == UIButtonMessage.Trigger.OnMouseOut))
			{
				this.Send();
			}
			this.mHighlighted = isOver;
		}
	}

	// Token: 0x060044C8 RID: 17608 RVA: 0x0010D7B8 File Offset: 0x0010B9B8
	private void OnPress(bool isPressed)
	{
		if (base.enabled && ((isPressed && this.trigger == UIButtonMessage.Trigger.OnPress) || (!isPressed && this.trigger == UIButtonMessage.Trigger.OnRelease)))
		{
			this.Send();
		}
	}

	// Token: 0x060044C9 RID: 17609 RVA: 0x0010D7F0 File Offset: 0x0010B9F0
	private void OnClick()
	{
		if (base.enabled && this.trigger == UIButtonMessage.Trigger.OnClick)
		{
			this.Send();
		}
	}

	// Token: 0x060044CA RID: 17610 RVA: 0x0010D810 File Offset: 0x0010BA10
	private void OnDoubleClick()
	{
		if (base.enabled && this.trigger == UIButtonMessage.Trigger.OnDoubleClick)
		{
			this.Send();
		}
	}

	// Token: 0x060044CB RID: 17611 RVA: 0x0010D830 File Offset: 0x0010BA30
	private void Send()
	{
		if (string.IsNullOrEmpty(this.functionName))
		{
			return;
		}
		if (this.target == null)
		{
			this.target = base.gameObject;
		}
		if (this.includeChildren)
		{
			Transform[] componentsInChildren = this.target.GetComponentsInChildren<Transform>();
			int i = 0;
			int num = componentsInChildren.Length;
			while (i < num)
			{
				Transform transform = componentsInChildren[i];
				transform.gameObject.SendMessage(this.functionName, base.gameObject, 1);
				i++;
			}
		}
		else
		{
			this.target.SendMessage(this.functionName, base.gameObject, 1);
		}
	}

	// Token: 0x040024F6 RID: 9462
	public GameObject target;

	// Token: 0x040024F7 RID: 9463
	public string functionName;

	// Token: 0x040024F8 RID: 9464
	public UIButtonMessage.Trigger trigger;

	// Token: 0x040024F9 RID: 9465
	public bool includeChildren;

	// Token: 0x040024FA RID: 9466
	private bool mStarted;

	// Token: 0x040024FB RID: 9467
	private bool mHighlighted;

	// Token: 0x0200075F RID: 1887
	public enum Trigger
	{
		// Token: 0x040024FD RID: 9469
		OnClick,
		// Token: 0x040024FE RID: 9470
		OnMouseOver,
		// Token: 0x040024FF RID: 9471
		OnMouseOut,
		// Token: 0x04002500 RID: 9472
		OnPress,
		// Token: 0x04002501 RID: 9473
		OnRelease,
		// Token: 0x04002502 RID: 9474
		OnDoubleClick
	}
}
