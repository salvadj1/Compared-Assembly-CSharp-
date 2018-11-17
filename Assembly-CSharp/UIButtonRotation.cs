using System;
using UnityEngine;

// Token: 0x02000844 RID: 2116
[AddComponentMenu("NGUI/Interaction/Button Rotation")]
public class UIButtonRotation : MonoBehaviour
{
	// Token: 0x0600493F RID: 18751 RVA: 0x00117730 File Offset: 0x00115930
	private void Start()
	{
		this.mStarted = true;
	}

	// Token: 0x06004940 RID: 18752 RVA: 0x0011773C File Offset: 0x0011593C
	private void OnEnable()
	{
		if (this.mStarted && this.mHighlighted)
		{
			this.OnHover(global::UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x06004941 RID: 18753 RVA: 0x00117768 File Offset: 0x00115968
	private void OnDisable()
	{
		if (this.tweenTarget != null)
		{
			global::TweenRotation component = this.tweenTarget.GetComponent<global::TweenRotation>();
			if (component != null)
			{
				component.rotation = this.mRot;
				component.enabled = false;
			}
		}
	}

	// Token: 0x06004942 RID: 18754 RVA: 0x001177B4 File Offset: 0x001159B4
	private void Init()
	{
		this.mInitDone = true;
		if (this.tweenTarget == null)
		{
			this.tweenTarget = base.transform;
		}
		this.mRot = this.tweenTarget.localRotation;
	}

	// Token: 0x06004943 RID: 18755 RVA: 0x001177EC File Offset: 0x001159EC
	private void OnPress(bool isPressed)
	{
		if (base.enabled)
		{
			if (!this.mInitDone)
			{
				this.Init();
			}
			global::TweenRotation.Begin(this.tweenTarget.gameObject, this.duration, (!isPressed) ? ((!global::UICamera.IsHighlighted(base.gameObject)) ? this.mRot : (this.mRot * Quaternion.Euler(this.hover))) : (this.mRot * Quaternion.Euler(this.pressed))).method = global::UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x06004944 RID: 18756 RVA: 0x00117884 File Offset: 0x00115A84
	private void OnHover(bool isOver)
	{
		if (base.enabled)
		{
			if (!this.mInitDone)
			{
				this.Init();
			}
			global::TweenRotation.Begin(this.tweenTarget.gameObject, this.duration, (!isOver) ? this.mRot : (this.mRot * Quaternion.Euler(this.hover))).method = global::UITweener.Method.EaseInOut;
			this.mHighlighted = isOver;
		}
	}

	// Token: 0x0400274E RID: 10062
	public Transform tweenTarget;

	// Token: 0x0400274F RID: 10063
	public Vector3 hover = Vector3.zero;

	// Token: 0x04002750 RID: 10064
	public Vector3 pressed = Vector3.zero;

	// Token: 0x04002751 RID: 10065
	public float duration = 0.2f;

	// Token: 0x04002752 RID: 10066
	private Quaternion mRot;

	// Token: 0x04002753 RID: 10067
	private bool mInitDone;

	// Token: 0x04002754 RID: 10068
	private bool mStarted;

	// Token: 0x04002755 RID: 10069
	private bool mHighlighted;
}
