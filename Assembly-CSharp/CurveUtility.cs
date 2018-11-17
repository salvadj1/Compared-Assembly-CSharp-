using System;
using UnityEngine;

// Token: 0x02000543 RID: 1347
public static class CurveUtility
{
	// Token: 0x06002D08 RID: 11528 RVA: 0x000A8774 File Offset: 0x000A6974
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

	// Token: 0x06002D09 RID: 11529 RVA: 0x000A882C File Offset: 0x000A6A2C
	public static float GetEndTime(this AnimationCurve curve)
	{
		if (curve.length == 0)
		{
			return 0f;
		}
		return curve[curve.length - 1].time;
	}

	// Token: 0x06002D0A RID: 11530 RVA: 0x000A8860 File Offset: 0x000A6A60
	public static float GetStartTime(this AnimationCurve curve)
	{
		if (curve.length == 0)
		{
			return 0f;
		}
		return curve[0].time;
	}
}
