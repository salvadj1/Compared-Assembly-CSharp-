using System;
using UnityEngine;

// Token: 0x020006BB RID: 1723
public static class dfPivotExtensions
{
	// Token: 0x06003C63 RID: 15459 RVA: 0x000E3ABC File Offset: 0x000E1CBC
	public static Vector3 TransformToCenter(this dfPivotPoint pivot, Vector2 size)
	{
		switch (pivot)
		{
		case dfPivotPoint.TopLeft:
			return new Vector2(0.5f * size.x, 0.5f * -size.y);
		case dfPivotPoint.TopCenter:
			return new Vector2(0f, 0.5f * -size.y);
		case dfPivotPoint.TopRight:
			return new Vector2(0.5f * -size.x, 0.5f * -size.y);
		case dfPivotPoint.MiddleLeft:
			return new Vector2(0.5f * size.x, 0f);
		case dfPivotPoint.MiddleCenter:
			return new Vector2(0f, 0f);
		case dfPivotPoint.MiddleRight:
			return new Vector2(0.5f * -size.x, 0f);
		case dfPivotPoint.BottomLeft:
			return new Vector2(0.5f * size.x, 0.5f * size.y);
		case dfPivotPoint.BottomCenter:
			return new Vector2(0f, 0.5f * size.y);
		case dfPivotPoint.BottomRight:
			return new Vector2(0.5f * -size.x, 0.5f * size.y);
		default:
			throw new Exception(string.Concat(new object[]
			{
				"Unhandled ",
				pivot.GetType().Name,
				" value: ",
				pivot
			}));
		}
	}

	// Token: 0x06003C64 RID: 15460 RVA: 0x000E3C5C File Offset: 0x000E1E5C
	public static Vector3 UpperLeftToTransform(this dfPivotPoint pivot, Vector2 size)
	{
		return pivot.TransformToUpperLeft(size).Scale(-1f, -1f, 1f);
	}

	// Token: 0x06003C65 RID: 15461 RVA: 0x000E3C7C File Offset: 0x000E1E7C
	public static Vector3 TransformToUpperLeft(this dfPivotPoint pivot, Vector2 size)
	{
		switch (pivot)
		{
		case dfPivotPoint.TopLeft:
			return new Vector2(0f, 0f);
		case dfPivotPoint.TopCenter:
			return new Vector2(0.5f * -size.x, 0f);
		case dfPivotPoint.TopRight:
			return new Vector2(-size.x, 0f);
		case dfPivotPoint.MiddleLeft:
			return new Vector2(0f, 0.5f * size.y);
		case dfPivotPoint.MiddleCenter:
			return new Vector2(0.5f * -size.x, 0.5f * size.y);
		case dfPivotPoint.MiddleRight:
			return new Vector2(-size.x, 0.5f * size.y);
		case dfPivotPoint.BottomLeft:
			return new Vector2(0f, size.y);
		case dfPivotPoint.BottomCenter:
			return new Vector2(0.5f * -size.x, size.y);
		case dfPivotPoint.BottomRight:
			return new Vector2(-size.x, size.y);
		default:
			throw new Exception(string.Concat(new object[]
			{
				"Unhandled ",
				pivot.GetType().Name,
				" value: ",
				pivot
			}));
		}
	}
}
