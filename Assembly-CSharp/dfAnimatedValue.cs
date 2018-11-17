using System;
using UnityEngine;

// Token: 0x02000816 RID: 2070
public abstract class dfAnimatedValue<T> where T : struct
{
	// Token: 0x060047B3 RID: 18355 RVA: 0x0010F2CC File Offset: 0x0010D4CC
	protected internal dfAnimatedValue(T StartValue, T EndValue, float Time) : this()
	{
		this.startValue = StartValue;
		this.endValue = EndValue;
		this.animLength = Time;
	}

	// Token: 0x060047B4 RID: 18356 RVA: 0x0010F2EC File Offset: 0x0010D4EC
	protected internal dfAnimatedValue()
	{
		this.startTime = Time.realtimeSinceStartup;
		this.easingFunction = global::dfEasingFunctions.GetFunction(this.easingType);
	}

	// Token: 0x17000DBF RID: 3519
	// (get) Token: 0x060047B5 RID: 18357 RVA: 0x0010F31C File Offset: 0x0010D51C
	public bool IsDone
	{
		get
		{
			return Time.realtimeSinceStartup - this.startTime >= this.Length;
		}
	}

	// Token: 0x17000DC0 RID: 3520
	// (get) Token: 0x060047B6 RID: 18358 RVA: 0x0010F338 File Offset: 0x0010D538
	// (set) Token: 0x060047B7 RID: 18359 RVA: 0x0010F340 File Offset: 0x0010D540
	public float Length
	{
		get
		{
			return this.animLength;
		}
		set
		{
			this.animLength = value;
			this.startTime = Time.realtimeSinceStartup;
		}
	}

	// Token: 0x17000DC1 RID: 3521
	// (get) Token: 0x060047B8 RID: 18360 RVA: 0x0010F354 File Offset: 0x0010D554
	// (set) Token: 0x060047B9 RID: 18361 RVA: 0x0010F35C File Offset: 0x0010D55C
	public T StartValue
	{
		get
		{
			return this.startValue;
		}
		set
		{
			this.startValue = value;
			this.startTime = Time.realtimeSinceStartup;
		}
	}

	// Token: 0x17000DC2 RID: 3522
	// (get) Token: 0x060047BA RID: 18362 RVA: 0x0010F370 File Offset: 0x0010D570
	// (set) Token: 0x060047BB RID: 18363 RVA: 0x0010F378 File Offset: 0x0010D578
	public T EndValue
	{
		get
		{
			return this.endValue;
		}
		set
		{
			this.endValue = value;
			this.startTime = Time.realtimeSinceStartup;
		}
	}

	// Token: 0x17000DC3 RID: 3523
	// (get) Token: 0x060047BC RID: 18364 RVA: 0x0010F38C File Offset: 0x0010D58C
	public T Value
	{
		get
		{
			float num = Time.realtimeSinceStartup - this.startTime;
			if (num >= this.animLength)
			{
				return this.endValue;
			}
			float time = Mathf.Clamp01(num / this.animLength);
			time = this.easingFunction(0f, 1f, time);
			return this.Lerp(this.startValue, this.endValue, time);
		}
	}

	// Token: 0x17000DC4 RID: 3524
	// (get) Token: 0x060047BD RID: 18365 RVA: 0x0010F3F4 File Offset: 0x0010D5F4
	// (set) Token: 0x060047BE RID: 18366 RVA: 0x0010F3FC File Offset: 0x0010D5FC
	public global::dfEasingType EasingType
	{
		get
		{
			return this.easingType;
		}
		set
		{
			this.easingType = value;
			this.easingFunction = global::dfEasingFunctions.GetFunction(this.easingType);
		}
	}

	// Token: 0x060047BF RID: 18367
	protected abstract T Lerp(T startValue, T endValue, float time);

	// Token: 0x060047C0 RID: 18368 RVA: 0x0010F418 File Offset: 0x0010D618
	public static implicit operator T(global::dfAnimatedValue<T> animated)
	{
		return animated.Value;
	}

	// Token: 0x04002597 RID: 9623
	private T startValue;

	// Token: 0x04002598 RID: 9624
	private T endValue;

	// Token: 0x04002599 RID: 9625
	private float animLength = 1f;

	// Token: 0x0400259A RID: 9626
	private float startTime;

	// Token: 0x0400259B RID: 9627
	private global::dfEasingType easingType;

	// Token: 0x0400259C RID: 9628
	private global::dfEasingFunctions.EasingFunction easingFunction;
}
