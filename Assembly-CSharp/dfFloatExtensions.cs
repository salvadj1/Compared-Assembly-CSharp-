using System;
using UnityEngine;

// Token: 0x020007CA RID: 1994
public static class dfFloatExtensions
{
	// Token: 0x06004491 RID: 17553 RVA: 0x00100368 File Offset: 0x000FE568
	public static float Quantize(this float value, float stepSize)
	{
		if (stepSize <= 0f)
		{
			return value;
		}
		return Mathf.Floor(value / stepSize) * stepSize;
	}

	// Token: 0x06004492 RID: 17554 RVA: 0x00100384 File Offset: 0x000FE584
	public static float RoundToNearest(this float value, float stepSize)
	{
		if (stepSize <= 0f)
		{
			return value;
		}
		return (float)Mathf.RoundToInt(value / stepSize) * stepSize;
	}
}
