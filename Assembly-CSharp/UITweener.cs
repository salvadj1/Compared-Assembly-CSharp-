using System;
using AnimationOrTween;
using UnityEngine;

// Token: 0x020008A6 RID: 2214
public abstract class UITweener : global::IgnoreTimeScale
{
	// Token: 0x17000E56 RID: 3670
	// (get) Token: 0x06004BE2 RID: 19426 RVA: 0x0012860C File Offset: 0x0012680C
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

	// Token: 0x17000E57 RID: 3671
	// (get) Token: 0x06004BE3 RID: 19427 RVA: 0x00128670 File Offset: 0x00126870
	public float factor
	{
		get
		{
			return this.mFactor;
		}
	}

	// Token: 0x17000E58 RID: 3672
	// (get) Token: 0x06004BE4 RID: 19428 RVA: 0x00128678 File Offset: 0x00126878
	public AnimationOrTween.Direction direction
	{
		get
		{
			return (this.mAmountPerDelta >= 0f) ? AnimationOrTween.Direction.Forward : AnimationOrTween.Direction.Reverse;
		}
	}

	// Token: 0x06004BE5 RID: 19429 RVA: 0x00128694 File Offset: 0x00126894
	private void Start()
	{
		this.mStartTime = Time.time + this.delay;
		this.Update();
	}

	// Token: 0x06004BE6 RID: 19430 RVA: 0x001286B0 File Offset: 0x001268B0
	private void Update()
	{
		if (Time.time < this.mStartTime)
		{
			return;
		}
		float num = base.UpdateRealTimeDelta();
		this.mFactor += this.amountPerDelta * num;
		if (this.style == global::UITweener.Style.Loop)
		{
			if (this.mFactor > 1f)
			{
				this.mFactor -= Mathf.Floor(this.mFactor);
			}
		}
		else if (this.style == global::UITweener.Style.PingPong)
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
		if (this.method == global::UITweener.Method.EaseIn)
		{
			num2 = 1f - Mathf.Sin(1.57079637f * (1f - num2));
			if (this.steeperCurves)
			{
				num2 *= num2;
			}
		}
		else if (this.method == global::UITweener.Method.EaseOut)
		{
			num2 = Mathf.Sin(1.57079637f * num2);
			if (this.steeperCurves)
			{
				num2 = 1f - num2;
				num2 = 1f - num2 * num2;
			}
		}
		else if (this.method == global::UITweener.Method.EaseInOut)
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
		if (this.style == global::UITweener.Style.Once && (this.mFactor > 1f || this.mFactor < 0f))
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

	// Token: 0x06004BE7 RID: 19431 RVA: 0x00128974 File Offset: 0x00126B74
	public void Play(bool forward)
	{
		this.mAmountPerDelta = Mathf.Abs(this.amountPerDelta);
		if (!forward)
		{
			this.mAmountPerDelta = -this.mAmountPerDelta;
		}
		base.enabled = true;
	}

	// Token: 0x06004BE8 RID: 19432 RVA: 0x001289A4 File Offset: 0x00126BA4
	[Obsolete("Use Tweener.Play instead")]
	public void Animate(bool forward)
	{
		this.Play(forward);
	}

	// Token: 0x06004BE9 RID: 19433 RVA: 0x001289B0 File Offset: 0x00126BB0
	public void Reset()
	{
		this.mFactor = ((this.mAmountPerDelta >= 0f) ? 0f : 1f);
	}

	// Token: 0x06004BEA RID: 19434 RVA: 0x001289D8 File Offset: 0x00126BD8
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

	// Token: 0x06004BEB RID: 19435
	protected abstract void OnUpdate(float factor);

	// Token: 0x06004BEC RID: 19436 RVA: 0x00128A20 File Offset: 0x00126C20
	public static T Begin<T>(GameObject go, float duration) where T : global::UITweener
	{
		T t = go.GetComponent<T>();
		if (t == null)
		{
			t = go.AddComponent<T>();
		}
		t.duration = duration;
		t.mFactor = 0f;
		t.style = global::UITweener.Style.Once;
		t.enabled = true;
		return t;
	}

	// Token: 0x0400298A RID: 10634
	public global::UITweener.Method method;

	// Token: 0x0400298B RID: 10635
	public global::UITweener.Style style;

	// Token: 0x0400298C RID: 10636
	public float delay;

	// Token: 0x0400298D RID: 10637
	public float duration = 1f;

	// Token: 0x0400298E RID: 10638
	public bool steeperCurves;

	// Token: 0x0400298F RID: 10639
	public int tweenGroup;

	// Token: 0x04002990 RID: 10640
	public GameObject eventReceiver;

	// Token: 0x04002991 RID: 10641
	public string callWhenFinished;

	// Token: 0x04002992 RID: 10642
	private float mStartTime;

	// Token: 0x04002993 RID: 10643
	private float mDuration;

	// Token: 0x04002994 RID: 10644
	private float mAmountPerDelta = 1f;

	// Token: 0x04002995 RID: 10645
	private float mFactor;

	// Token: 0x020008A7 RID: 2215
	public enum Method
	{
		// Token: 0x04002997 RID: 10647
		Linear,
		// Token: 0x04002998 RID: 10648
		EaseIn,
		// Token: 0x04002999 RID: 10649
		EaseOut,
		// Token: 0x0400299A RID: 10650
		EaseInOut
	}

	// Token: 0x020008A8 RID: 2216
	public enum Style
	{
		// Token: 0x0400299C RID: 10652
		Once,
		// Token: 0x0400299D RID: 10653
		Loop,
		// Token: 0x0400299E RID: 10654
		PingPong
	}
}
