using System;
using UnityEngine;

// Token: 0x020007CC RID: 1996
public static class RectExtensions
{
	// Token: 0x0600449D RID: 17565 RVA: 0x00100598 File Offset: 0x000FE798
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

	// Token: 0x0600449E RID: 17566 RVA: 0x001005FC File Offset: 0x000FE7FC
	public static bool IsEmpty(this Rect rect)
	{
		return rect.xMin == rect.xMax || rect.yMin == rect.yMax;
	}

	// Token: 0x0600449F RID: 17567 RVA: 0x00100630 File Offset: 0x000FE830
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

	// Token: 0x060044A0 RID: 17568 RVA: 0x001006B0 File Offset: 0x000FE8B0
	public static Rect Union(this Rect a, Rect b)
	{
		float num = Mathf.Min(a.xMin, b.xMin);
		float num2 = Mathf.Max(a.xMax, b.xMax);
		float num3 = Mathf.Min(a.yMin, b.yMin);
		float num4 = Mathf.Max(a.yMax, b.yMax);
		return Rect.MinMaxRect(num, num3, num2, num4);
	}

	// Token: 0x060044A1 RID: 17569 RVA: 0x00100718 File Offset: 0x000FE918
	public static bool Contains(this Rect rect, Rect other)
	{
		bool flag = rect.x <= other.x;
		bool flag2 = rect.x + rect.width >= other.x + other.width;
		bool flag3 = rect.yMin <= other.yMin;
		bool flag4 = rect.y + rect.height >= other.y + other.height;
		return flag && flag2 && flag3 && flag4;
	}

	// Token: 0x060044A2 RID: 17570 RVA: 0x001007AC File Offset: 0x000FE9AC
	public static bool Intersects(this Rect rect, Rect other)
	{
		bool flag = rect.xMax < other.xMin || rect.yMax < other.xMin || rect.xMin > other.xMax || rect.yMin > other.yMax;
		return !flag;
	}

	// Token: 0x060044A3 RID: 17571 RVA: 0x0010080C File Offset: 0x000FEA0C
	public static Rect RoundToInt(this Rect rect)
	{
		return new Rect((float)Mathf.RoundToInt(rect.x), (float)Mathf.RoundToInt(rect.y), (float)Mathf.RoundToInt(rect.width), (float)Mathf.RoundToInt(rect.height));
	}

	// Token: 0x060044A4 RID: 17572 RVA: 0x00100854 File Offset: 0x000FEA54
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
