using System;
using UnityEngine;

// Token: 0x02000786 RID: 1926
public static class dfPivotExtensions
{
	// Token: 0x0600406D RID: 16493 RVA: 0x000EC600 File Offset: 0x000EA800
	public static Vector3 TransformToCenter(this global::dfPivotPoint pivot, Vector2 size)
	{
		switch (pivot)
		{
		case global::dfPivotPoint.TopLeft:
			return new Vector2(0.5f * size.x, 0.5f * -size.y);
		case global::dfPivotPoint.TopCenter:
			return new Vector2(0f, 0.5f * -size.y);
		case global::dfPivotPoint.TopRight:
			return new Vector2(0.5f * -size.x, 0.5f * -size.y);
		case global::dfPivotPoint.MiddleLeft:
			return new Vector2(0.5f * size.x, 0f);
		case global::dfPivotPoint.MiddleCenter:
			return new Vector2(0f, 0f);
		case global::dfPivotPoint.MiddleRight:
			return new Vector2(0.5f * -size.x, 0f);
		case global::dfPivotPoint.BottomLeft:
			return new Vector2(0.5f * size.x, 0.5f * size.y);
		case global::dfPivotPoint.BottomCenter:
			return new Vector2(0f, 0.5f * size.y);
		case global::dfPivotPoint.BottomRight:
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

	// Token: 0x0600406E RID: 16494 RVA: 0x000EC7A0 File Offset: 0x000EA9A0
	public static Vector3 UpperLeftToTransform(this global::dfPivotPoint pivot, Vector2 size)
	{
		return pivot.TransformToUpperLeft(size).Scale(-1f, -1f, 1f);
	}

	// Token: 0x0600406F RID: 16495 RVA: 0x000EC7C0 File Offset: 0x000EA9C0
	public static Vector3 TransformToUpperLeft(this global::dfPivotPoint pivot, Vector2 size)
	{
		switch (pivot)
		{
		case global::dfPivotPoint.TopLeft:
			return new Vector2(0f, 0f);
		case global::dfPivotPoint.TopCenter:
			return new Vector2(0.5f * -size.x, 0f);
		case global::dfPivotPoint.TopRight:
			return new Vector2(-size.x, 0f);
		case global::dfPivotPoint.MiddleLeft:
			return new Vector2(0f, 0.5f * size.y);
		case global::dfPivotPoint.MiddleCenter:
			return new Vector2(0.5f * -size.x, 0.5f * size.y);
		case global::dfPivotPoint.MiddleRight:
			return new Vector2(-size.x, 0.5f * size.y);
		case global::dfPivotPoint.BottomLeft:
			return new Vector2(0f, size.y);
		case global::dfPivotPoint.BottomCenter:
			return new Vector2(0.5f * -size.x, size.y);
		case global::dfPivotPoint.BottomRight:
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
