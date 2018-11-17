using System;
using UnityEngine;

// Token: 0x02000488 RID: 1160
public static class CurveUtility
{
	// Token: 0x06002956 RID: 10582 RVA: 0x000A2378 File Offset: 0x000A0578
	public static float EvaluateClampedTime(this AnimationCurve curve, ref float time, float advance)
	{
		int length = curve.length;
		if (curve.length == 0)
		{
			return 1f;
		}
		if (curve.length == 1)
		{
			return curve.Evaluate(0f);
		}
		if (advance > 0f)
		{
			float time2 = curve[length - 1].time;
			if (time < time2)
			{
				time += advance;
				if (time > time2)
				{
					time = time2;
				}
			}
		}
		else if (advance < 0f)
		{
			float time3 = curve[0].time;
			if (time > time3)
			{
				time += advance;
				if (time < time3)
				{
					time = time3;
				}
			}
		}
		return curve.Evaluate(time);
	}

	// Token: 0x06002957 RID: 10583 RVA: 0x000A2430 File Offset: 0x000A0630
	public static float GetEndTime(this AnimationCurve curve)
	{
		if (curve.length == 0)
		{
			return 0f;
		}
		return curve[curve.length - 1].time;
	}

	// Token: 0x06002958 RID: 10584 RVA: 0x000A2464 File Offset: 0x000A0664
	public static float GetStartTime(this AnimationCurve curve)
	{
		if (curve.length == 0)
		{
			return 0f;
		}
		return curve[0].time;
	}
}
