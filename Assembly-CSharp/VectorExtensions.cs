using System;
using UnityEngine;

// Token: 0x020007CB RID: 1995
public static class VectorExtensions
{
	// Token: 0x06004493 RID: 17555 RVA: 0x001003A0 File Offset: 0x000FE5A0
	public static Vector2 Scale(this Vector2 vector, float x, float y)
	{
		return new Vector2(vector.x * x, vector.y * y);
	}

	// Token: 0x06004494 RID: 17556 RVA: 0x001003BC File Offset: 0x000FE5BC
	public static Vector3 Scale(this Vector3 vector, float x, float y, float z)
	{
		return new Vector3(vector.x * x, vector.y * y, vector.z * z);
	}

	// Token: 0x06004495 RID: 17557 RVA: 0x001003EC File Offset: 0x000FE5EC
	public static Vector3 FloorToInt(this Vector3 vector)
	{
		return new Vector3((float)Mathf.FloorToInt(vector.x), (float)Mathf.FloorToInt(vector.y), (float)Mathf.FloorToInt(vector.z));
	}

	// Token: 0x06004496 RID: 17558 RVA: 0x00100428 File Offset: 0x000FE628
	public static Vector3 CeilToInt(this Vector3 vector)
	{
		return new Vector3((float)Mathf.CeilToInt(vector.x), (float)Mathf.CeilToInt(vector.y), (float)Mathf.CeilToInt(vector.z));
	}

	// Token: 0x06004497 RID: 17559 RVA: 0x00100464 File Offset: 0x000FE664
	public static Vector2 FloorToInt(this Vector2 vector)
	{
		return new Vector2((float)Mathf.FloorToInt(vector.x), (float)Mathf.FloorToInt(vector.y));
	}

	// Token: 0x06004498 RID: 17560 RVA: 0x00100488 File Offset: 0x000FE688
	public static Vector2 CeilToInt(this Vector2 vector)
	{
		return new Vector2((float)Mathf.CeilToInt(vector.x), (float)Mathf.CeilToInt(vector.y));
	}

	// Token: 0x06004499 RID: 17561 RVA: 0x001004AC File Offset: 0x000FE6AC
	public static Vector3 RoundToInt(this Vector3 vector)
	{
		return new Vector3((float)Mathf.RoundToInt(vector.x), (float)Mathf.RoundToInt(vector.y), (float)Mathf.RoundToInt(vector.z));
	}

	// Token: 0x0600449A RID: 17562 RVA: 0x001004E8 File Offset: 0x000FE6E8
	public static Vector2 RoundToInt(this Vector2 vector)
	{
		return new Vector2((float)Mathf.RoundToInt(vector.x), (float)Mathf.RoundToInt(vector.y));
	}

	// Token: 0x0600449B RID: 17563 RVA: 0x0010050C File Offset: 0x000FE70C
	public static Vector2 Quantize(this Vector2 vector, float discreteValue)
	{
		vector.x = (float)Mathf.RoundToInt(vector.x / discreteValue) * discreteValue;
		vector.y = (float)Mathf.RoundToInt(vector.y / discreteValue) * discreteValue;
		return vector;
	}

	// Token: 0x0600449C RID: 17564 RVA: 0x00100540 File Offset: 0x000FE740
	public static Vector3 Quantize(this Vector3 vector, float discreteValue)
	{
		vector.x = (float)Mathf.RoundToInt(vector.x / discreteValue) * discreteValue;
		vector.y = (float)Mathf.RoundToInt(vector.y / discreteValue) * discreteValue;
		vector.z = (float)Mathf.RoundToInt(vector.z / discreteValue) * discreteValue;
		return vector;
	}
}
