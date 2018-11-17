using System;
using UnityEngine;

// Token: 0x02000739 RID: 1849
public abstract class dfAnimatedValue<T> where T : struct
{
	// Token: 0x0600436B RID: 17259 RVA: 0x00105FBC File Offset: 0x001041BC
	protected internal dfAnimatedValue(T StartValue, T EndValue, float Time) : this()
	{
		this.startValue = StartValue;
		this.endValue = EndValue;
		this.animLength = Time;
	}

	// Token: 0x0600436C RID: 17260 RVA: 0x00105FDC File Offset: 0x001041DC
	protected internal dfAnimatedValue()
	{
		this.startTime = Time.realtimeSinceStartup;
		this.easingFunction = dfEasingFunctions.GetFunction(this.easingType);
	}

	// Token: 0x17000D35 RID: 3381
	// (get) Token: 0x0600436D RID: 17261 RVA: 0x0010600C File Offset: 0x0010420C
	public bool IsDone
	{
		get
		{
			return Time.realtimeSinceStartup - this.startTime >= this.Length;
		}
	}

	// Token: 0x17000D36 RID: 3382
	// (get) Token: 0x0600436E RID: 17262 RVA: 0x00106028 File Offset: 0x00104228
	// (set) Token: 0x0600436F RID: 17263 RVA: 0x00106030 File Offset: 0x00104230
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

	// Token: 0x17000D37 RID: 3383
	// (get) Token: 0x06004370 RID: 17264 RVA: 0x00106044 File Offset: 0x00104244
	// (set) Token: 0x06004371 RID: 17265 RVA: 0x0010604C File Offset: 0x0010424C
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

	// Token: 0x17000D38 RID: 3384
	// (get) Token: 0x06004372 RID: 17266 RVA: 0x00106060 File Offset: 0x00104260
	// (set) Token: 0x06004373 RID: 17267 RVA: 0x00106068 File Offset: 0x00104268
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

	// Token: 0x17000D39 RID: 3385
	// (get) Token: 0x06004374 RID: 17268 RVA: 0x0010607C File Offset: 0x0010427C
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

	// Token: 0x17000D3A RID: 3386
	// (get) Token: 0x06004375 RID: 17269 RVA: 0x001060E4 File Offset: 0x001042E4
	// (set) Token: 0x06004376 RID: 17270 RVA: 0x001060EC File Offset: 0x001042EC
	public dfEasingType EasingType
	{
		get
		{
			return this.easingType;
		}
		set
		{
			this.easingType = value;
			this.easingFunction = dfEasingFunctions.GetFunction(this.easingType);
		}
	}

	// Token: 0x06004377 RID: 17271
	protected abstract T Lerp(T startValue, T endValue, float time);

	// Token: 0x06004378 RID: 17272 RVA: 0x00106108 File Offset: 0x00104308
	public static implicit operator T(dfAnimatedValue<T> animated)
	{
		return animated.Value;
	}

	// Token: 0x04002374 RID: 9076
	private T startValue;

	// Token: 0x04002375 RID: 9077
	private T endValue;

	// Token: 0x04002376 RID: 9078
	private float animLength = 1f;

	// Token: 0x04002377 RID: 9079
	private float startTime;

	// Token: 0x04002378 RID: 9080
	private dfEasingType easingType;

	// Token: 0x04002379 RID: 9081
	private dfEasingFunctions.EasingFunction easingFunction;
}
