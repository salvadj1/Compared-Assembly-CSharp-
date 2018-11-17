using System;
using UnityEngine;

// Token: 0x02000762 RID: 1890
[AddComponentMenu("NGUI/Interaction/Button Rotation")]
public class UIButtonRotation : MonoBehaviour
{
	// Token: 0x060044DE RID: 17630 RVA: 0x0010DDB0 File Offset: 0x0010BFB0
	private void Start()
	{
		this.mStarted = true;
	}

	// Token: 0x060044DF RID: 17631 RVA: 0x0010DDBC File Offset: 0x0010BFBC
	private void OnEnable()
	{
		if (this.mStarted && this.mHighlighted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x060044E0 RID: 17632 RVA: 0x0010DDE8 File Offset: 0x0010BFE8
	private void OnDisable()
	{
		if (this.tweenTarget != null)
		{
			TweenRotation component = this.tweenTarget.GetComponent<TweenRotation>();
			if (component != null)
			{
				component.rotation = this.mRot;
				component.enabled = false;
			}
		}
	}

	// Token: 0x060044E1 RID: 17633 RVA: 0x0010DE34 File Offset: 0x0010C034
	private void Init()
	{
		this.mInitDone = true;
		if (this.tweenTarget == null)
		{
			this.tweenTarget = base.transform;
		}
		this.mRot = this.tweenTarget.localRotation;
	}

	// Token: 0x060044E2 RID: 17634 RVA: 0x0010DE6C File Offset: 0x0010C06C
	private void OnPress(bool isPressed)
	{
		if (base.enabled)
		{
			if (!this.mInitDone)
			{
				this.Init();
			}
			TweenRotation.Begin(this.tweenTarget.gameObject, this.duration, (!isPressed) ? ((!UICamera.IsHighlighted(base.gameObject)) ? this.mRot : (this.mRot * Quaternion.Euler(this.hover))) : (this.mRot * Quaternion.Euler(this.pressed))).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x060044E3 RID: 17635 RVA: 0x0010DF04 File Offset: 0x0010C104
	private void OnHover(bool isOver)
	{
		if (base.enabled)
		{
			if (!this.mInitDone)
			{
				this.Init();
			}
			TweenRotation.Begin(this.tweenTarget.gameObject, this.duration, (!isOver) ? this.mRot : (this.mRot * Quaternion.Euler(this.hover))).method = UITweener.Method.EaseInOut;
			this.mHighlighted = isOver;
		}
	}

	// Token: 0x04002517 RID: 9495
	public Transform tweenTarget;

	// Token: 0x04002518 RID: 9496
	public Vector3 hover = Vector3.zero;

	// Token: 0x04002519 RID: 9497
	public Vector3 pressed = Vector3.zero;

	// Token: 0x0400251A RID: 9498
	public float duration = 0.2f;

	// Token: 0x0400251B RID: 9499
	private Quaternion mRot;

	// Token: 0x0400251C RID: 9500
	private bool mInitDone;

	// Token: 0x0400251D RID: 9501
	private bool mStarted;

	// Token: 0x0400251E RID: 9502
	private bool mHighlighted;
}
