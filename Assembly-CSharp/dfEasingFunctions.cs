using System;
using UnityEngine;

// Token: 0x0200073A RID: 1850
public class dfEasingFunctions
{
	// Token: 0x0600437A RID: 17274 RVA: 0x00106118 File Offset: 0x00104318
	public static dfEasingFunctions.EasingFunction GetFunction(dfEasingType easeType)
	{
		switch (easeType)
		{
		case dfEasingType.Linear:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.linear);
		case dfEasingType.Bounce:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.bounce);
		case dfEasingType.BackEaseIn:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeInBack);
		case dfEasingType.BackEaseOut:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeOutBack);
		case dfEasingType.BackEaseInOut:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeInOutBack);
		case dfEasingType.CircEaseIn:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeInCirc);
		case dfEasingType.CircEaseOut:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeOutCirc);
		case dfEasingType.CircEaseInOut:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeInOutCirc);
		case dfEasingType.CubicEaseIn:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeInCubic);
		case dfEasingType.CubicEaseOut:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeOutCubic);
		case dfEasingType.CubicEaseInOut:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeInOutCubic);
		case dfEasingType.ExpoEaseIn:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeInExpo);
		case dfEasingType.ExpoEaseOut:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeOutExpo);
		case dfEasingType.ExpoEaseInOut:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeInOutExpo);
		case dfEasingType.QuadEaseIn:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeInQuad);
		case dfEasingType.QuadEaseOut:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeOutQuad);
		case dfEasingType.QuadEaseInOut:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeInOutQuad);
		case dfEasingType.QuartEaseIn:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeInQuart);
		case dfEasingType.QuartEaseOut:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeOutQuart);
		case dfEasingType.QuartEaseInOut:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeInOutQuart);
		case dfEasingType.QuintEaseIn:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeInQuint);
		case dfEasingType.QuintEaseOut:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeOutQuint);
		case dfEasingType.QuintEaseInOut:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeInOutQuint);
		case dfEasingType.SineEaseIn:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeInSine);
		case dfEasingType.SineEaseOut:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeOutSine);
		case dfEasingType.SineEaseInOut:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeInOutSine);
		case dfEasingType.Spring:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.spring);
		default:
			throw new NotImplementedException();
		}
	}

	// Token: 0x0600437B RID: 17275 RVA: 0x00106304 File Offset: 0x00104504
	private static float linear(float start, float end, float time)
	{
		return Mathf.Lerp(start, end, time);
	}

	// Token: 0x0600437C RID: 17276 RVA: 0x00106310 File Offset: 0x00104510
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

	// Token: 0x0600437D RID: 17277 RVA: 0x00106388 File Offset: 0x00104588
	private static float spring(float start, float end, float time)
	{
		time = Mathf.Clamp01(time);
		time = (Mathf.Sin(time * 3.14159274f * (0.2f + 2.5f * time * time * time)) * Mathf.Pow(1f - time, 2.2f) + time) * (1f + 1.2f * (1f - time));
		return start + (end - start) * time;
	}

	// Token: 0x0600437E RID: 17278 RVA: 0x001063EC File Offset: 0x001045EC
	private static float easeInQuad(float start, float end, float time)
	{
		end -= start;
		return end * time * time + start;
	}

	// Token: 0x0600437F RID: 17279 RVA: 0x001063FC File Offset: 0x001045FC
	private static float easeOutQuad(float start, float end, float time)
	{
		end -= start;
		return -end * time * (time - 2f) + start;
	}

	// Token: 0x06004380 RID: 17280 RVA: 0x00106414 File Offset: 0x00104614
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

	// Token: 0x06004381 RID: 17281 RVA: 0x0010646C File Offset: 0x0010466C
	private static float easeInCubic(float start, float end, float time)
	{
		end -= start;
		return end * time * time * time + start;
	}

	// Token: 0x06004382 RID: 17282 RVA: 0x0010647C File Offset: 0x0010467C
	private static float easeOutCubic(float start, float end, float time)
	{
		time -= 1f;
		end -= start;
		return end * (time * time * time + 1f) + start;
	}

	// Token: 0x06004383 RID: 17283 RVA: 0x0010649C File Offset: 0x0010469C
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

	// Token: 0x06004384 RID: 17284 RVA: 0x001064F0 File Offset: 0x001046F0
	private static float easeInQuart(float start, float end, float time)
	{
		end -= start;
		return end * time * time * time * time + start;
	}

	// Token: 0x06004385 RID: 17285 RVA: 0x00106504 File Offset: 0x00104704
	private static float easeOutQuart(float start, float end, float time)
	{
		time -= 1f;
		end -= start;
		return -end * (time * time * time * time - 1f) + start;
	}

	// Token: 0x06004386 RID: 17286 RVA: 0x00106534 File Offset: 0x00104734
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

	// Token: 0x06004387 RID: 17287 RVA: 0x00106590 File Offset: 0x00104790
	private static float easeInQuint(float start, float end, float time)
	{
		end -= start;
		return end * time * time * time * time * time + start;
	}

	// Token: 0x06004388 RID: 17288 RVA: 0x001065A4 File Offset: 0x001047A4
	private static float easeOutQuint(float start, float end, float time)
	{
		time -= 1f;
		end -= start;
		return end * (time * time * time * time * time + 1f) + start;
	}

	// Token: 0x06004389 RID: 17289 RVA: 0x001065C8 File Offset: 0x001047C8
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

	// Token: 0x0600438A RID: 17290 RVA: 0x00106624 File Offset: 0x00104824
	private static float easeInSine(float start, float end, float time)
	{
		end -= start;
		return -end * Mathf.Cos(time / 1f * 1.57079637f) + end + start;
	}

	// Token: 0x0600438B RID: 17291 RVA: 0x00106644 File Offset: 0x00104844
	private static float easeOutSine(float start, float end, float time)
	{
		end -= start;
		return end * Mathf.Sin(time / 1f * 1.57079637f) + start;
	}

	// Token: 0x0600438C RID: 17292 RVA: 0x00106664 File Offset: 0x00104864
	private static float easeInOutSine(float start, float end, float time)
	{
		end -= start;
		return -end / 2f * (Mathf.Cos(3.14159274f * time / 1f) - 1f) + start;
	}

	// Token: 0x0600438D RID: 17293 RVA: 0x0010669C File Offset: 0x0010489C
	private static float easeInExpo(float start, float end, float time)
	{
		end -= start;
		return end * Mathf.Pow(2f, 10f * (time / 1f - 1f)) + start;
	}

	// Token: 0x0600438E RID: 17294 RVA: 0x001066D0 File Offset: 0x001048D0
	private static float easeOutExpo(float start, float end, float time)
	{
		end -= start;
		return end * (-Mathf.Pow(2f, -10f * time / 1f) + 1f) + start;
	}

	// Token: 0x0600438F RID: 17295 RVA: 0x001066FC File Offset: 0x001048FC
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

	// Token: 0x06004390 RID: 17296 RVA: 0x00106770 File Offset: 0x00104970
	private static float easeInCirc(float start, float end, float time)
	{
		end -= start;
		return -end * (Mathf.Sqrt(1f - time * time) - 1f) + start;
	}

	// Token: 0x06004391 RID: 17297 RVA: 0x00106790 File Offset: 0x00104990
	private static float easeOutCirc(float start, float end, float time)
	{
		time -= 1f;
		end -= start;
		return end * Mathf.Sqrt(1f - time * time) + start;
	}

	// Token: 0x06004392 RID: 17298 RVA: 0x001067C0 File Offset: 0x001049C0
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

	// Token: 0x06004393 RID: 17299 RVA: 0x00106830 File Offset: 0x00104A30
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

	// Token: 0x06004394 RID: 17300 RVA: 0x001068D8 File Offset: 0x00104AD8
	private static float easeInBack(float start, float end, float time)
	{
		end -= start;
		time /= 1f;
		float num = 1.70158f;
		return end * time * time * ((num + 1f) * time - num) + start;
	}

	// Token: 0x06004395 RID: 17301 RVA: 0x0010690C File Offset: 0x00104B0C
	private static float easeOutBack(float start, float end, float time)
	{
		float num = 1.70158f;
		end -= start;
		time = time / 1f - 1f;
		return end * (time * time * ((num + 1f) * time + num) + 1f) + start;
	}

	// Token: 0x06004396 RID: 17302 RVA: 0x0010694C File Offset: 0x00104B4C
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

	// Token: 0x06004397 RID: 17303 RVA: 0x001069CC File Offset: 0x00104BCC
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

	// Token: 0x020008E3 RID: 2275
	// (Invoke) Token: 0x06004D64 RID: 19812
	public delegate float EasingFunction(float start, float end, float time);
}
