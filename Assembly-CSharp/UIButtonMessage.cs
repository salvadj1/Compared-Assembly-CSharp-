using System;
using UnityEngine;

// Token: 0x02000840 RID: 2112
[AddComponentMenu("NGUI/Interaction/Button Message")]
public class UIButtonMessage : MonoBehaviour
{
	// Token: 0x06004926 RID: 18726 RVA: 0x001170B4 File Offset: 0x001152B4
	private void Start()
	{
		this.mStarted = true;
	}

	// Token: 0x06004927 RID: 18727 RVA: 0x001170C0 File Offset: 0x001152C0
	private void OnEnable()
	{
		if (this.mStarted && this.mHighlighted)
		{
			this.OnHover(global::UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x06004928 RID: 18728 RVA: 0x001170EC File Offset: 0x001152EC
	private void OnHover(bool isOver)
	{
		if (base.enabled)
		{
			if ((isOver && this.trigger == global::UIButtonMessage.Trigger.OnMouseOver) || (!isOver && this.trigger == global::UIButtonMessage.Trigger.OnMouseOut))
			{
				this.Send();
			}
			this.mHighlighted = isOver;
		}
	}

	// Token: 0x06004929 RID: 18729 RVA: 0x00117138 File Offset: 0x00115338
	private void OnPress(bool isPressed)
	{
		if (base.enabled && ((isPressed && this.trigger == global::UIButtonMessage.Trigger.OnPress) || (!isPressed && this.trigger == global::UIButtonMessage.Trigger.OnRelease)))
		{
			this.Send();
		}
	}

	// Token: 0x0600492A RID: 18730 RVA: 0x00117170 File Offset: 0x00115370
	private void OnClick()
	{
		if (base.enabled && this.trigger == global::UIButtonMessage.Trigger.OnClick)
		{
			this.Send();
		}
	}

	// Token: 0x0600492B RID: 18731 RVA: 0x00117190 File Offset: 0x00115390
	private void OnDoubleClick()
	{
		if (base.enabled && this.trigger == global::UIButtonMessage.Trigger.OnDoubleClick)
		{
			this.Send();
		}
	}

	// Token: 0x0600492C RID: 18732 RVA: 0x001171B0 File Offset: 0x001153B0
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

	// Token: 0x0400272D RID: 10029
	public GameObject target;

	// Token: 0x0400272E RID: 10030
	public string functionName;

	// Token: 0x0400272F RID: 10031
	public global::UIButtonMessage.Trigger trigger;

	// Token: 0x04002730 RID: 10032
	public bool includeChildren;

	// Token: 0x04002731 RID: 10033
	private bool mStarted;

	// Token: 0x04002732 RID: 10034
	private bool mHighlighted;

	// Token: 0x02000841 RID: 2113
	public enum Trigger
	{
		// Token: 0x04002734 RID: 10036
		OnClick,
		// Token: 0x04002735 RID: 10037
		OnMouseOver,
		// Token: 0x04002736 RID: 10038
		OnMouseOut,
		// Token: 0x04002737 RID: 10039
		OnPress,
		// Token: 0x04002738 RID: 10040
		OnRelease,
		// Token: 0x04002739 RID: 10041
		OnDoubleClick
	}
}
