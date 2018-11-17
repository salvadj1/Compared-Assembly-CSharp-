using System;
using UnityEngine;

// Token: 0x020006F8 RID: 1784
public static class dfFloatExtensions
{
	// Token: 0x06004075 RID: 16501 RVA: 0x000F7764 File Offset: 0x000F5964
	public static float Quantize(this float value, float stepSize)
	{
		if (stepSize <= 0f)
		{
			return value;
		}
		return Mathf.Floor(value / stepSize) * stepSize;
	}

	// Token: 0x06004076 RID: 16502 RVA: 0x000F7780 File Offset: 0x000F5980
	public static float RoundToNearest(this float value, float stepSize)
	{
		if (stepSize <= 0f)
		{
			return value;
		}
		return (float)Mathf.RoundToInt(value / stepSize) * stepSize;
	}
}
