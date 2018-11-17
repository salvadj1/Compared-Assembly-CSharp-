using System;
using UnityEngine;

// Token: 0x02000842 RID: 2114
[AddComponentMenu("NGUI/Interaction/Button Offset")]
public class UIButtonOffset : MonoBehaviour
{
	// Token: 0x0600492E RID: 18734 RVA: 0x00117288 File Offset: 0x00115488
	private void Start()
	{
		this.mStarted = true;
	}

	// Token: 0x0600492F RID: 18735 RVA: 0x00117294 File Offset: 0x00115494
	private void OnEnable()
	{
		if (this.mStarted && this.mHighlighted)
		{
			this.OnHover(global::UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x06004930 RID: 18736 RVA: 0x001172C0 File Offset: 0x001154C0
	private void OnDisable()
	{
		if (this.tweenTarget != null)
		{
			global::TweenPosition component = this.tweenTarget.GetComponent<global::TweenPosition>();
			if (component != null)
			{
				component.position = this.mPos;
				component.enabled = false;
			}
		}
	}

	// Token: 0x06004931 RID: 18737 RVA: 0x0011730C File Offset: 0x0011550C
	private void Init()
	{
		this.mInitDone = true;
		if (this.tweenTarget == null)
		{
			this.tweenTarget = base.transform;
		}
		this.mPos = this.tweenTarget.localPosition;
	}

	// Token: 0x06004932 RID: 18738 RVA: 0x00117344 File Offset: 0x00115544
	private void OnPress(bool isPressed)
	{
		if (base.enabled)
		{
			if (!this.mInitDone)
			{
				this.Init();
			}
			global::TweenPosition.Begin(this.tweenTarget.gameObject, this.duration, (!isPressed) ? ((!global::UICamera.IsHighlighted(base.gameObject)) ? this.mPos : (this.mPos + this.hover)) : (this.mPos + this.pressed)).method = global::UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x06004933 RID: 18739 RVA: 0x001173D4 File Offset: 0x001155D4
	private void OnHover(bool isOver)
	{
		if (base.enabled)
		{
			if (!this.mInitDone)
			{
				this.Init();
			}
			global::TweenPosition.Begin(this.tweenTarget.gameObject, this.duration, (!isOver) ? this.mPos : (this.mPos + this.hover)).method = global::UITweener.Method.EaseInOut;
			this.mHighlighted = isOver;
		}
	}

	// Token: 0x0400273A RID: 10042
	public Transform tweenTarget;

	// Token: 0x0400273B RID: 10043
	public Vector3 hover = Vector3.zero;

	// Token: 0x0400273C RID: 10044
	public Vector3 pressed = new Vector3(2f, -2f);

	// Token: 0x0400273D RID: 10045
	public float duration = 0.2f;

	// Token: 0x0400273E RID: 10046
	private Vector3 mPos;

	// Token: 0x0400273F RID: 10047
	private bool mInitDone;

	// Token: 0x04002740 RID: 10048
	private bool mStarted;

	// Token: 0x04002741 RID: 10049
	private bool mHighlighted;
}
