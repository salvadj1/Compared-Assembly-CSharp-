using System;
using UnityEngine;

// Token: 0x02000763 RID: 1891
[AddComponentMenu("NGUI/Interaction/Button Scale")]
public class UIButtonScale : MonoBehaviour
{
	// Token: 0x060044E5 RID: 17637 RVA: 0x0010DFCC File Offset: 0x0010C1CC
	private void Start()
	{
		this.mStarted = true;
	}

	// Token: 0x060044E6 RID: 17638 RVA: 0x0010DFD8 File Offset: 0x0010C1D8
	private void OnEnable()
	{
		if (this.mStarted && this.mHighlighted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x060044E7 RID: 17639 RVA: 0x0010E004 File Offset: 0x0010C204
	private void OnDisable()
	{
		if (this.tweenTarget != null)
		{
			TweenScale component = this.tweenTarget.GetComponent<TweenScale>();
			if (component != null)
			{
				component.scale = this.mScale;
				component.enabled = false;
			}
		}
	}

	// Token: 0x060044E8 RID: 17640 RVA: 0x0010E050 File Offset: 0x0010C250
	private void Init()
	{
		this.mInitDone = true;
		if (this.tweenTarget == null)
		{
			this.tweenTarget = base.transform;
		}
		this.mScale = this.tweenTarget.localScale;
	}

	// Token: 0x060044E9 RID: 17641 RVA: 0x0010E088 File Offset: 0x0010C288
	private void OnPress(bool isPressed)
	{
		if (base.enabled)
		{
			if (!this.mInitDone)
			{
				this.Init();
			}
			TweenScale.Begin(this.tweenTarget.gameObject, this.duration, (!isPressed) ? ((!UICamera.IsHighlighted(base.gameObject)) ? this.mScale : Vector3.Scale(this.mScale, this.hover)) : Vector3.Scale(this.mScale, this.pressed)).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x060044EA RID: 17642 RVA: 0x0010E118 File Offset: 0x0010C318
	private void OnHover(bool isOver)
	{
		if (base.enabled)
		{
			if (!this.mInitDone)
			{
				this.Init();
			}
			TweenScale.Begin(this.tweenTarget.gameObject, this.duration, (!isOver) ? this.mScale : Vector3.Scale(this.mScale, this.hover)).method = UITweener.Method.EaseInOut;
			this.mHighlighted = isOver;
		}
	}

	// Token: 0x0400251F RID: 9503
	public Transform tweenTarget;

	// Token: 0x04002520 RID: 9504
	public Vector3 hover = new Vector3(1.1f, 1.1f, 1.1f);

	// Token: 0x04002521 RID: 9505
	public Vector3 pressed = new Vector3(1.05f, 1.05f, 1.05f);

	// Token: 0x04002522 RID: 9506
	public float duration = 0.2f;

	// Token: 0x04002523 RID: 9507
	private Vector3 mScale;

	// Token: 0x04002524 RID: 9508
	private bool mInitDone;

	// Token: 0x04002525 RID: 9509
	private bool mStarted;

	// Token: 0x04002526 RID: 9510
	private bool mHighlighted;
}
