using System;
using UnityEngine;

// Token: 0x02000760 RID: 1888
[AddComponentMenu("NGUI/Interaction/Button Offset")]
public class UIButtonOffset : MonoBehaviour
{
	// Token: 0x060044CD RID: 17613 RVA: 0x0010D908 File Offset: 0x0010BB08
	private void Start()
	{
		this.mStarted = true;
	}

	// Token: 0x060044CE RID: 17614 RVA: 0x0010D914 File Offset: 0x0010BB14
	private void OnEnable()
	{
		if (this.mStarted && this.mHighlighted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x060044CF RID: 17615 RVA: 0x0010D940 File Offset: 0x0010BB40
	private void OnDisable()
	{
		if (this.tweenTarget != null)
		{
			TweenPosition component = this.tweenTarget.GetComponent<TweenPosition>();
			if (component != null)
			{
				component.position = this.mPos;
				component.enabled = false;
			}
		}
	}

	// Token: 0x060044D0 RID: 17616 RVA: 0x0010D98C File Offset: 0x0010BB8C
	private void Init()
	{
		this.mInitDone = true;
		if (this.tweenTarget == null)
		{
			this.tweenTarget = base.transform;
		}
		this.mPos = this.tweenTarget.localPosition;
	}

	// Token: 0x060044D1 RID: 17617 RVA: 0x0010D9C4 File Offset: 0x0010BBC4
	private void OnPress(bool isPressed)
	{
		if (base.enabled)
		{
			if (!this.mInitDone)
			{
				this.Init();
			}
			TweenPosition.Begin(this.tweenTarget.gameObject, this.duration, (!isPressed) ? ((!UICamera.IsHighlighted(base.gameObject)) ? this.mPos : (this.mPos + this.hover)) : (this.mPos + this.pressed)).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x060044D2 RID: 17618 RVA: 0x0010DA54 File Offset: 0x0010BC54
	private void OnHover(bool isOver)
	{
		if (base.enabled)
		{
			if (!this.mInitDone)
			{
				this.Init();
			}
			TweenPosition.Begin(this.tweenTarget.gameObject, this.duration, (!isOver) ? this.mPos : (this.mPos + this.hover)).method = UITweener.Method.EaseInOut;
			this.mHighlighted = isOver;
		}
	}

	// Token: 0x04002503 RID: 9475
	public Transform tweenTarget;

	// Token: 0x04002504 RID: 9476
	public Vector3 hover = Vector3.zero;

	// Token: 0x04002505 RID: 9477
	public Vector3 pressed = new Vector3(2f, -2f);

	// Token: 0x04002506 RID: 9478
	public float duration = 0.2f;

	// Token: 0x04002507 RID: 9479
	private Vector3 mPos;

	// Token: 0x04002508 RID: 9480
	private bool mInitDone;

	// Token: 0x04002509 RID: 9481
	private bool mStarted;

	// Token: 0x0400250A RID: 9482
	private bool mHighlighted;
}
