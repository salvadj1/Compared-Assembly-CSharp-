using System;
using UnityEngine;

// Token: 0x02000845 RID: 2117
[AddComponentMenu("NGUI/Interaction/Button Scale")]
public class UIButtonScale : MonoBehaviour
{
	// Token: 0x06004946 RID: 18758 RVA: 0x0011794C File Offset: 0x00115B4C
	private void Start()
	{
		this.mStarted = true;
	}

	// Token: 0x06004947 RID: 18759 RVA: 0x00117958 File Offset: 0x00115B58
	private void OnEnable()
	{
		if (this.mStarted && this.mHighlighted)
		{
			this.OnHover(global::UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x06004948 RID: 18760 RVA: 0x00117984 File Offset: 0x00115B84
	private void OnDisable()
	{
		if (this.tweenTarget != null)
		{
			global::TweenScale component = this.tweenTarget.GetComponent<global::TweenScale>();
			if (component != null)
			{
				component.scale = this.mScale;
				component.enabled = false;
			}
		}
	}

	// Token: 0x06004949 RID: 18761 RVA: 0x001179D0 File Offset: 0x00115BD0
	private void Init()
	{
		this.mInitDone = true;
		if (this.tweenTarget == null)
		{
			this.tweenTarget = base.transform;
		}
		this.mScale = this.tweenTarget.localScale;
	}

	// Token: 0x0600494A RID: 18762 RVA: 0x00117A08 File Offset: 0x00115C08
	private void OnPress(bool isPressed)
	{
		if (base.enabled)
		{
			if (!this.mInitDone)
			{
				this.Init();
			}
			global::TweenScale.Begin(this.tweenTarget.gameObject, this.duration, (!isPressed) ? ((!global::UICamera.IsHighlighted(base.gameObject)) ? this.mScale : Vector3.Scale(this.mScale, this.hover)) : Vector3.Scale(this.mScale, this.pressed)).method = global::UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x0600494B RID: 18763 RVA: 0x00117A98 File Offset: 0x00115C98
	private void OnHover(bool isOver)
	{
		if (base.enabled)
		{
			if (!this.mInitDone)
			{
				this.Init();
			}
			global::TweenScale.Begin(this.tweenTarget.gameObject, this.duration, (!isOver) ? this.mScale : Vector3.Scale(this.mScale, this.hover)).method = global::UITweener.Method.EaseInOut;
			this.mHighlighted = isOver;
		}
	}

	// Token: 0x04002756 RID: 10070
	public Transform tweenTarget;

	// Token: 0x04002757 RID: 10071
	public Vector3 hover = new Vector3(1.1f, 1.1f, 1.1f);

	// Token: 0x04002758 RID: 10072
	public Vector3 pressed = new Vector3(1.05f, 1.05f, 1.05f);

	// Token: 0x04002759 RID: 10073
	public float duration = 0.2f;

	// Token: 0x0400275A RID: 10074
	private Vector3 mScale;

	// Token: 0x0400275B RID: 10075
	private bool mInitDone;

	// Token: 0x0400275C RID: 10076
	private bool mStarted;

	// Token: 0x0400275D RID: 10077
	private bool mHighlighted;
}
