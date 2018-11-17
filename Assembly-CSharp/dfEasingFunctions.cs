using System;
using UnityEngine;

// Token: 0x02000817 RID: 2071
public class dfEasingFunctions
{
	// Token: 0x060047C2 RID: 18370 RVA: 0x0010F428 File Offset: 0x0010D628
	public static global::dfEasingFunctions.EasingFunction GetFunction(global::dfEasingType easeType)
	{
		switch (easeType)
		{
		case global::dfEasingType.Linear:
			return new global::dfEasingFunctions.EasingFunction(global::dfEasingFunctions.linear);
		case global::dfEasingType.Bounce:
			return new global::dfEasingFunctions.EasingFunction(global::dfEasingFunctions.bounce);
		case global::dfEasingType.BackEaseIn:
			return new global::dfEasingFunctions.EasingFunction(global::dfEasingFunctions.easeInBack);
		case global::dfEasingType.BackEaseOut:
			return new global::dfEasingFunctions.EasingFunction(global::dfEasingFunctions.easeOutBack);
		case global::dfEasingType.BackEaseInOut:
			return new global::dfEasingFunctions.EasingFunction(global::dfEasingFunctions.easeInOutBack);
		case global::dfEasingType.CircEaseIn:
			return new global::dfEasingFunctions.EasingFunction(global::dfEasingFunctions.easeInCirc);
		case global::dfEasingType.CircEaseOut:
			return new global::dfEasingFunctions.EasingFunction(global::dfEasingFunctions.easeOutCirc);
		case global::dfEasingType.CircEaseInOut:
			return new global::dfEasingFunctions.EasingFunction(global::dfEasingFunctions.easeInOutCirc);
		case global::dfEasingType.CubicEaseIn:
			return new global::dfEasingFunctions.EasingFunction(global::dfEasingFunctions.easeInCubic);
		case global::dfEasingType.CubicEaseOut:
			return new global::dfEasingFunctions.EasingFunction(global::dfEasingFunctions.easeOutCubic);
		case global::dfEasingType.CubicEaseInOut:
			return new global::dfEasingFunctions.EasingFunction(global::dfEasingFunctions.easeInOutCubic);
		case global::dfEasingType.ExpoEaseIn:
			return new global::dfEasingFunctions.EasingFunction(global::dfEasingFunctions.easeInExpo);
		case global::dfEasingType.ExpoEaseOut:
			return new global::dfEasingFunctions.EasingFunction(global::dfEasingFunctions.easeOutExpo);
		case global::dfEasingType.ExpoEaseInOut:
			return new global::dfEasingFunctions.EasingFunction(global::dfEasingFunctions.easeInOutExpo);
		case global::dfEasingType.QuadEaseIn:
			return new global::dfEasingFunctions.EasingFunction(global::dfEasingFunctions.easeInQuad);
		case global::dfEasingType.QuadEaseOut:
			return new global::dfEasingFunctions.EasingFunction(global::dfEasingFunctions.easeOutQuad);
		case global::dfEasingType.QuadEaseInOut:
			return new global::dfEasingFunctions.EasingFunction(global::dfEasingFunctions.easeInOutQuad);
		case global::dfEasingType.QuartEaseIn:
			return new global::dfEasingFunctions.EasingFunction(global::dfEasingFunctions.easeInQuart);
		case global::dfEasingType.QuartEaseOut:
			return new global::dfEasingFunctions.EasingFunction(global::dfEasingFunctions.easeOutQuart);
		case global::dfEasingType.QuartEaseInOut:
			return new global::dfEasingFunctions.EasingFunction(global::dfEasingFunctions.easeInOutQuart);
		case global::dfEasingType.QuintEaseIn:
			return new global::dfEasingFunctions.EasingFunction(global::dfEasingFunctions.easeInQuint);
		case global::dfEasingType.QuintEaseOut:
			return new global::dfEasingFunctions.EasingFunction(global::dfEasingFunctions.easeOutQuint);
		case global::dfEasingType.QuintEaseInOut:
			return new global::dfEasingFunctions.EasingFunction(global::dfEasingFunctions.easeInOutQuint);
		case global::dfEasingType.SineEaseIn:
			return new global::dfEasingFunctions.EasingFunction(global::dfEasingFunctions.easeInSine);
		case global::dfEasingType.SineEaseOut:
			return new global::dfEasingFunctions.EasingFunction(global::dfEasingFunctions.easeOutSine);
		case global::dfEasingType.SineEaseInOut:
			return new global::dfEasingFunctions.EasingFunction(global::dfEasingFunctions.easeInOutSine);
		case global::dfEasingType.Spring:
			return new global::dfEasingFunctions.EasingFunction(global::dfEasingFunctions.spring);
		default:
			throw new NotImplementedException();
		}
	}

	// Token: 0x060047C3 RID: 18371 RVA: 0x0010F614 File Offset: 0x0010D814
	private static float linear(float start, float end, float time)
	{
		return Mathf.Lerp(start, end, time);
	}

	// Token: 0x060047C4 RID: 18372 RVA: 0x0010F620 File Offset: 0x0010D820
	private static float clerp(float start, float end, float time)
	{
		float num = 0f;
		float num2 = 360f;
		float num3 = Mathf.Abs((num2 - num) / 2f);
		float result;
		if (end - start < -num3)
		{
			float num4 = (num2 - start + end) * time;
			result = start + num4;
		}
		else if (end - start > num3)
		{
			float num4 = -(num2 - end + start) * time;
			result = start + num4;
		}
		else
		{
			result = start + (end - start) * time;
		}
		return result;
	}

	// Token: 0x060047C5 RID: 18373 RVA: 0x0010F698 File Offset: 0x0010D898
	private static float spring(float start, float end, float time)
	{
		time = Mathf.Clamp01(time);
		time = (Mathf.Sin(time * 3.14159274f * (0.2f + 2.5f * time * time * time)) * Mathf.Pow(1f - time, 2.2f) + time) * (1f + 1.2f * (1f - time));
		return start + (end - start) * time;
	}

	// Token: 0x060047C6 RID: 18374 RVA: 0x0010F6FC File Offset: 0x0010D8FC
	private static float easeInQuad(float start, float end, float time)
	{
		end -= start;
		return end * time * time + start;
	}

	// Token: 0x060047C7 RID: 18375 RVA: 0x0010F70C File Offset: 0x0010D90C
	private static float easeOutQuad(float start, float end, float time)
	{
		end -= start;
		return -end * time * (time - 2f) + start;
	}

	// Token: 0x060047C8 RID: 18376 RVA: 0x0010F724 File Offset: 0x0010D924
	private static float easeInOutQuad(float start, float end, float time)
	{
		time /= 0.5f;
		end -= start;
		if (time < 1f)
		{
			return end / 2f * time * time + start;
		}
		time -= 1f;
		return -end / 2f * (time * (time - 2f) - 1f) + start;
	}

	// Token: 0x060047C9 RID: 18377 RVA: 0x0010F77C File Offset: 0x0010D97C
	private static float easeInCubic(float start, float end, float time)
	{
		end -= start;
		return end * time * time * time + start;
	}

	// Token: 0x060047CA RID: 18378 RVA: 0x0010F78C File Offset: 0x0010D98C
	private static float easeOutCubic(float start, float end, float time)
	{
		time -= 1f;
		end -= start;
		return end * (time * time * time + 1f) + start;
	}

	// Token: 0x060047CB RID: 18379 RVA: 0x0010F7AC File Offset: 0x0010D9AC
	private static float easeInOutCubic(float start, float end, float time)
	{
		time /= 0.5f;
		end -= start;
		if (time < 1f)
		{
			return end / 2f * time * time * time + start;
		}
		time -= 2f;
		return end / 2f * (time * time * time + 2f) + start;
	}

	// Token: 0x060047CC RID: 18380 RVA: 0x0010F800 File Offset: 0x0010DA00
	private static float easeInQuart(float start, float end, float time)
	{
		end -= start;
		return end * time * time * time * time + start;
	}

	// Token: 0x060047CD RID: 18381 RVA: 0x0010F814 File Offset: 0x0010DA14
	private static float easeOutQuart(float start, float end, float time)
	{
		time -= 1f;
		end -= start;
		return -end * (time * time * time * time - 1f) + start;
	}

	// Token: 0x060047CE RID: 18382 RVA: 0x0010F844 File Offset: 0x0010DA44
	private static float easeInOutQuart(float start, float end, float time)
	{
		time /= 0.5f;
		end -= start;
		if (time < 1f)
		{
			return end / 2f * time * time * time * time + start;
		}
		time -= 2f;
		return -end / 2f * (time * time * time * time - 2f) + start;
	}

	// Token: 0x060047CF RID: 18383 RVA: 0x0010F8A0 File Offset: 0x0010DAA0
	private static float easeInQuint(float start, float end, float time)
	{
		end -= start;
		return end * time * time * time * time * time + start;
	}

	// Token: 0x060047D0 RID: 18384 RVA: 0x0010F8B4 File Offset: 0x0010DAB4
	private static float easeOutQuint(float start, float end, float time)
	{
		time -= 1f;
		end -= start;
		return end * (time * time * time * time * time + 1f) + start;
	}

	// Token: 0x060047D1 RID: 18385 RVA: 0x0010F8D8 File Offset: 0x0010DAD8
	private static float easeInOutQuint(float start, float end, float time)
	{
		time /= 0.5f;
		end -= start;
		if (time < 1f)
		{
			return end / 2f * time * time * time * time * time + start;
		}
		time -= 2f;
		return end / 2f * (time * time * time * time * time + 2f) + start;
	}

	// Token: 0x060047D2 RID: 18386 RVA: 0x0010F934 File Offset: 0x0010DB34
	private static float easeInSine(float start, float end, float time)
	{
		end -= start;
		return -end * Mathf.Cos(time / 1f * 1.57079637f) + end + start;
	}

	// Token: 0x060047D3 RID: 18387 RVA: 0x0010F954 File Offset: 0x0010DB54
	private static float easeOutSine(float start, float end, float time)
	{
		end -= start;
		return end * Mathf.Sin(time / 1f * 1.57079637f) + start;
	}

	// Token: 0x060047D4 RID: 18388 RVA: 0x0010F974 File Offset: 0x0010DB74
	private static float easeInOutSine(float start, float end, float time)
	{
		end -= start;
		return -end / 2f * (Mathf.Cos(3.14159274f * time / 1f) - 1f) + start;
	}

	// Token: 0x060047D5 RID: 18389 RVA: 0x0010F9AC File Offset: 0x0010DBAC
	private static float easeInExpo(float start, float end, float time)
	{
		end -= start;
		return end * Mathf.Pow(2f, 10f * (time / 1f - 1f)) + start;
	}

	// Token: 0x060047D6 RID: 18390 RVA: 0x0010F9E0 File Offset: 0x0010DBE0
	private static float easeOutExpo(float start, float end, float time)
	{
		end -= start;
		return end * (-Mathf.Pow(2f, -10f * time / 1f) + 1f) + start;
	}

	// Token: 0x060047D7 RID: 18391 RVA: 0x0010FA0C File Offset: 0x0010DC0C
	private static float easeInOutExpo(float start, float end, float time)
	{
		time /= 0.5f;
		end -= start;
		if (time < 1f)
		{
			return end / 2f * Mathf.Pow(2f, 10f * (time - 1f)) + start;
		}
		time -= 1f;
		return end / 2f * (-Mathf.Pow(2f, -10f * time) + 2f) + start;
	}

	// Token: 0x060047D8 RID: 18392 RVA: 0x0010FA80 File Offset: 0x0010DC80
	private static float easeInCirc(float start, float end, float time)
	{
		end -= start;
		return -end * (Mathf.Sqrt(1f - time * time) - 1f) + start;
	}

	// Token: 0x060047D9 RID: 18393 RVA: 0x0010FAA0 File Offset: 0x0010DCA0
	private static float easeOutCirc(float start, float end, float time)
	{
		time -= 1f;
		end -= start;
		return end * Mathf.Sqrt(1f - time * time) + start;
	}

	// Token: 0x060047DA RID: 18394 RVA: 0x0010FAD0 File Offset: 0x0010DCD0
	private static float easeInOutCirc(float start, float end, float time)
	{
		time /= 0.5f;
		end -= start;
		if (time < 1f)
		{
			return -end / 2f * (Mathf.Sqrt(1f - time * time) - 1f) + start;
		}
		time -= 2f;
		return end / 2f * (Mathf.Sqrt(1f - time * time) + 1f) + start;
	}

	// Token: 0x060047DB RID: 18395 RVA: 0x0010FB40 File Offset: 0x0010DD40
	private static float bounce(float start, float end, float time)
	{
		time /= 1f;
		end -= start;
		if (time < 0.363636374f)
		{
			return end * (7.5625f * time * time) + start;
		}
		if (time < 0.727272749f)
		{
			time -= 0.545454562f;
			return end * (7.5625f * time * time + 0.75f) + start;
		}
		if ((double)time < 0.90909090909090906)
		{
			time -= 0.8181818f;
			return end * (7.5625f * time * time + 0.9375f) + start;
		}
		time -= 0.954545438f;
		return end * (7.5625f * time * time + 0.984375f) + start;
	}

	// Token: 0x060047DC RID: 18396 RVA: 0x0010FBE8 File Offset: 0x0010DDE8
	private static float easeInBack(float start, float end, float time)
	{
		end -= start;
		time /= 1f;
		float num = 1.70158f;
		return end * time * time * ((num + 1f) * time - num) + start;
	}

	// Token: 0x060047DD RID: 18397 RVA: 0x0010FC1C File Offset: 0x0010DE1C
	private static float easeOutBack(float start, float end, float time)
	{
		float num = 1.70158f;
		end -= start;
		time = time / 1f - 1f;
		return end * (time * time * ((num + 1f) * time + num) + 1f) + start;
	}

	// Token: 0x060047DE RID: 18398 RVA: 0x0010FC5C File Offset: 0x0010DE5C
	private static float easeInOutBack(float start, float end, float time)
	{
		float num = 1.70158f;
		end -= start;
		time /= 0.5f;
		if (time < 1f)
		{
			num *= 1.525f;
			return end / 2f * (time * time * ((num + 1f) * time - num)) + start;
		}
		time -= 2f;
		num *= 1.525f;
		return end / 2f * (time * time * ((num + 1f) * time + num) + 2f) + start;
	}

	// Token: 0x060047DF RID: 18399 RVA: 0x0010FCDC File Offset: 0x0010DEDC
	private static float punch(float amplitude, float time)
	{
		if (time == 0f)
		{
			return 0f;
		}
		if (time == 1f)
		{
			return 0f;
		}
		float num = 0.3f;
		float num2 = num / 6.28318548f * Mathf.Asin(0f);
		return amplitude * Mathf.Pow(2f, -10f * time) * Mathf.Sin((time * 1f - num2) * 6.28318548f / num);
	}

	// Token: 0x02000818 RID: 2072
	// (Invoke) Token: 0x060047E1 RID: 18401
	public delegate float EasingFunction(float start, float end, float time);
}
