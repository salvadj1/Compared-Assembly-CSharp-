using System;
using UnityEngine;

// Token: 0x020006FA RID: 1786
public static class RectExtensions
{
	// Token: 0x06004081 RID: 16513 RVA: 0x000F7994 File Offset: 0x000F5B94
	public static RectOffset ConstrainPadding(this RectOffset borders)
	{
		if (borders == null)
		{
			return new RectOffset();
		}
		borders.left = Mathf.Max(0, borders.left);
		borders.right = Mathf.Max(0, borders.right);
		borders.top = Mathf.Max(0, borders.top);
		borders.bottom = Mathf.Max(0, borders.bottom);
		return borders;
	}

	// Token: 0x06004082 RID: 16514 RVA: 0x000F79F8 File Offset: 0x000F5BF8
	public static bool IsEmpty(this Rect rect)
	{
		return rect.xMin == rect.xMax || rect.yMin == rect.yMax;
	}

	// Token: 0x06004083 RID: 16515 RVA: 0x000F7A2C File Offset: 0x000F5C2C
	public static Rect Intersection(this Rect a, Rect b)
	{
		if (!a.Intersects(b))
		{
			return default(Rect);
		}
		float num = Mathf.Max(a.xMin, b.xMin);
		float num2 = Mathf.Min(a.xMax, b.xMax);
		float num3 = Mathf.Max(a.yMin, b.yMin);
		float num4 = Mathf.Min(a.yMax, b.yMax);
		return Rect.MinMaxRect(num, num4, num2, num3);
	}

	// Token: 0x06004084 RID: 16516 RVA: 0x000F7AAC File Offset: 0x000F5CAC
	public static Rect Union(this Rect a, Rect b)
	{
		float num = Mathf.Min(a.xMin, b.xMin);
		float num2 = Mathf.Max(a.xMax, b.xMax);
		float num3 = Mathf.Min(a.yMin, b.yMin);
		float num4 = Mathf.Max(a.yMax, b.yMax);
		return Rect.MinMaxRect(num, num3, num2, num4);
	}

	// Token: 0x06004085 RID: 16517 RVA: 0x000F7B14 File Offset: 0x000F5D14
	public static bool Contains(this Rect rect, Rect other)
	{
		bool flag = rect.x <= other.x;
		bool flag2 = rect.x + rect.width >= other.x + other.width;
		bool flag3 = rect.yMin <= other.yMin;
		bool flag4 = rect.y + rect.height >= other.y + other.height;
		return flag && flag2 && flag3 && flag4;
	}

	// Token: 0x06004086 RID: 16518 RVA: 0x000F7BA8 File Offset: 0x000F5DA8
	public static bool Intersects(this Rect rect, Rect other)
	{
		bool flag = rect.xMax < other.xMin || rect.yMax < other.xMin || rect.xMin > other.xMax || rect.yMin > other.yMax;
		return !flag;
	}

	// Token: 0x06004087 RID: 16519 RVA: 0x000F7C08 File Offset: 0x000F5E08
	public static Rect RoundToInt(this Rect rect)
	{
		return new Rect((float)Mathf.RoundToInt(rect.x), (float)Mathf.RoundToInt(rect.y), (float)Mathf.RoundToInt(rect.width), (float)Mathf.RoundToInt(rect.height));
	}

	// Token: 0x06004088 RID: 16520 RVA: 0x000F7C50 File Offset: 0x000F5E50
	public static string Debug(this Rect rect)
	{
		return string.Format("[{0},{1},{2},{3}]", new object[]
		{
			rect.xMin,
			rect.yMin,
			rect.xMax,
			rect.yMax
		});
	}
}
