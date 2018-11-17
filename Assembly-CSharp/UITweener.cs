using System;
using AnimationOrTween;
using UnityEngine;

// Token: 0x020007B9 RID: 1977
public abstract class UITweener : IgnoreTimeScale
{
	// Token: 0x17000DC4 RID: 3524
	// (get) Token: 0x06004753 RID: 18259 RVA: 0x0011EBE8 File Offset: 0x0011CDE8
	public float amountPerDelta
	{
		get
		{
			if (this.mDuration != this.duration)
			{
				this.mDuration = this.duration;
				this.mAmountPerDelta = Mathf.Abs((this.duration <= 0f) ? 1000f : (1f / this.duration));
			}
			return this.mAmountPerDelta;
		}
	}

	// Token: 0x17000DC5 RID: 3525
	// (get) Token: 0x06004754 RID: 18260 RVA: 0x0011EC4C File Offset: 0x0011CE4C
	public float factor
	{
		get
		{
			return this.mFactor;
		}
	}

	// Token: 0x17000DC6 RID: 3526
	// (get) Token: 0x06004755 RID: 18261 RVA: 0x0011EC54 File Offset: 0x0011CE54
	public Direction direction
	{
		get
		{
			return (this.mAmountPerDelta >= 0f) ? Direction.Forward : Direction.Reverse;
		}
	}

	// Token: 0x06004756 RID: 18262 RVA: 0x0011EC70 File Offset: 0x0011CE70
	private void Start()
	{
		this.mStartTime = Time.time + this.delay;
		this.Update();
	}

	// Token: 0x06004757 RID: 18263 RVA: 0x0011EC8C File Offset: 0x0011CE8C
	private void Update()
	{
		if (Time.time < this.mStartTime)
		{
			return;
		}
		float num = base.UpdateRealTimeDelta();
		this.mFactor += this.amountPerDelta * num;
		if (this.style == UITweener.Style.Loop)
		{
			if (this.mFactor > 1f)
			{
				this.mFactor -= Mathf.Floor(this.mFactor);
			}
		}
		else if (this.style == UITweener.Style.PingPong)
		{
			if (this.mFactor > 1f)
			{
				this.mFactor = 1f - (this.mFactor - Mathf.Floor(this.mFactor));
				this.mAmountPerDelta = -this.mAmountPerDelta;
			}
			else if (this.mFactor < 0f)
			{
				this.mFactor = -this.mFactor;
				this.mFactor -= Mathf.Floor(this.mFactor);
				this.mAmountPerDelta = -this.mAmountPerDelta;
			}
		}
		float num2 = Mathf.Clamp01(this.mFactor);
		if (this.method == UITweener.Method.EaseIn)
		{
			num2 = 1f - Mathf.Sin(1.57079637f * (1f - num2));
			if (this.steeperCurves)
			{
				num2 *= num2;
			}
		}
		else if (this.method == UITweener.Method.EaseOut)
		{
			num2 = Mathf.Sin(1.57079637f * num2);
			if (this.steeperCurves)
			{
				num2 = 1f - num2;
				num2 = 1f - num2 * num2;
			}
		}
		else if (this.method == UITweener.Method.EaseInOut)
		{
			num2 -= Mathf.Sin(num2 * 6.28318548f) / 6.28318548f;
			if (this.steeperCurves)
			{
				num2 = num2 * 2f - 1f;
				float num3 = Mathf.Sign(num2);
				num2 = 1f - Mathf.Abs(num2);
				num2 = 1f - num2 * num2;
				num2 = num3 * num2 * 0.5f + 0.5f;
			}
		}
		this.OnUpdate(num2);
		if (this.style == UITweener.Style.Once && (this.mFactor > 1f || this.mFactor < 0f))
		{
			this.mFactor = Mathf.Clamp01(this.mFactor);
			if (string.IsNullOrEmpty(this.callWhenFinished))
			{
				base.enabled = false;
			}
			else
			{
				if (this.eventReceiver != null && !string.IsNullOrEmpty(this.callWhenFinished))
				{
					this.eventReceiver.SendMessage(this.callWhenFinished, this, 1);
				}
				if ((this.mFactor == 1f && this.mAmountPerDelta > 0f) || (this.mFactor == 0f && this.mAmountPerDelta < 0f))
				{
					base.enabled = false;
				}
			}
		}
	}

	// Token: 0x06004758 RID: 18264 RVA: 0x0011EF50 File Offset: 0x0011D150
	public void Play(bool forward)
	{
		this.mAmountPerDelta = Mathf.Abs(this.amountPerDelta);
		if (!forward)
		{
			this.mAmountPerDelta = -this.mAmountPerDelta;
		}
		base.enabled = true;
	}

	// Token: 0x06004759 RID: 18265 RVA: 0x0011EF80 File Offset: 0x0011D180
	[Obsolete("Use Tweener.Play instead")]
	public void Animate(bool forward)
	{
		this.Play(forward);
	}

	// Token: 0x0600475A RID: 18266 RVA: 0x0011EF8C File Offset: 0x0011D18C
	public void Reset()
	{
		this.mFactor = ((this.mAmountPerDelta >= 0f) ? 0f : 1f);
	}

	// Token: 0x0600475B RID: 18267 RVA: 0x0011EFB4 File Offset: 0x0011D1B4
	public void Toggle()
	{
		if (this.mFactor > 0f)
		{
			this.mAmountPerDelta = -this.amountPerDelta;
		}
		else
		{
			this.mAmountPerDelta = Mathf.Abs(this.amountPerDelta);
		}
		base.enabled = true;
	}

	// Token: 0x0600475C RID: 18268
	protected abstract void OnUpdate(float factor);

	// Token: 0x0600475D RID: 18269 RVA: 0x0011EFFC File Offset: 0x0011D1FC
	public static T Begin<T>(GameObject go, float duration) where T : UITweener
	{
		T t = go.GetComponent<T>();
		if (t == null)
		{
			t = go.AddComponent<T>();
		}
		t.duration = duration;
		t.mFactor = 0f;
		t.style = UITweener.Style.Once;
		t.enabled = true;
		return t;
	}

	// Token: 0x04002750 RID: 10064
	public UITweener.Method method;

	// Token: 0x04002751 RID: 10065
	public UITweener.Style style;

	// Token: 0x04002752 RID: 10066
	public float delay;

	// Token: 0x04002753 RID: 10067
	public float duration = 1f;

	// Token: 0x04002754 RID: 10068
	public bool steeperCurves;

	// Token: 0x04002755 RID: 10069
	public int tweenGroup;

	// Token: 0x04002756 RID: 10070
	public GameObject eventReceiver;

	// Token: 0x04002757 RID: 10071
	public string callWhenFinished;

	// Token: 0x04002758 RID: 10072
	private float mStartTime;

	// Token: 0x04002759 RID: 10073
	private float mDuration;

	// Token: 0x0400275A RID: 10074
	private float mAmountPerDelta = 1f;

	// Token: 0x0400275B RID: 10075
	private float mFactor;

	// Token: 0x020007BA RID: 1978
	public enum Method
	{
		// Token: 0x0400275D RID: 10077
		Linear,
		// Token: 0x0400275E RID: 10078
		EaseIn,
		// Token: 0x0400275F RID: 10079
		EaseOut,
		// Token: 0x04002760 RID: 10080
		EaseInOut
	}

	// Token: 0x020007BB RID: 1979
	public enum Style
	{
		// Token: 0x04002762 RID: 10082
		Once,
		// Token: 0x04002763 RID: 10083
		Loop,
		// Token: 0x04002764 RID: 10084
		PingPong
	}
}
