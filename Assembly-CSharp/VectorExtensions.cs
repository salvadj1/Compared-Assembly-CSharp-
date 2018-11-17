using System;
using UnityEngine;

// Token: 0x020006F9 RID: 1785
public static class VectorExtensions
{
	// Token: 0x06004077 RID: 16503 RVA: 0x000F779C File Offset: 0x000F599C
	public static Vector2 Scale(this Vector2 vector, float x, float y)
	{
		return new Vector2(vector.x * x, vector.y * y);
	}

	// Token: 0x06004078 RID: 16504 RVA: 0x000F77B8 File Offset: 0x000F59B8
	public static Vector3 Scale(this Vector3 vector, float x, float y, float z)
	{
		return new Vector3(vector.x * x, vector.y * y, vector.z * z);
	}

	// Token: 0x06004079 RID: 16505 RVA: 0x000F77E8 File Offset: 0x000F59E8
	public static Vector3 FloorToInt(this Vector3 vector)
	{
		return new Vector3((float)Mathf.FloorToInt(vector.x), (float)Mathf.FloorToInt(vector.y), (float)Mathf.FloorToInt(vector.z));
	}

	// Token: 0x0600407A RID: 16506 RVA: 0x000F7824 File Offset: 0x000F5A24
	public static Vector3 CeilToInt(this Vector3 vector)
	{
		return new Vector3((float)Mathf.CeilToInt(vector.x), (float)Mathf.CeilToInt(vector.y), (float)Mathf.CeilToInt(vector.z));
	}

	// Token: 0x0600407B RID: 16507 RVA: 0x000F7860 File Offset: 0x000F5A60
	public static Vector2 FloorToInt(this Vector2 vector)
	{
		return new Vector2((float)Mathf.FloorToInt(vector.x), (float)Mathf.FloorToInt(vector.y));
	}

	// Token: 0x0600407C RID: 16508 RVA: 0x000F7884 File Offset: 0x000F5A84
	public static Vector2 CeilToInt(this Vector2 vector)
	{
		return new Vector2((float)Mathf.CeilToInt(vector.x), (float)Mathf.CeilToInt(vector.y));
	}

	// Token: 0x0600407D RID: 16509 RVA: 0x000F78A8 File Offset: 0x000F5AA8
	public static Vector3 RoundToInt(this Vector3 vector)
	{
		return new Vector3((float)Mathf.RoundToInt(vector.x), (float)Mathf.RoundToInt(vector.y), (float)Mathf.RoundToInt(vector.z));
	}

	// Token: 0x0600407E RID: 16510 RVA: 0x000F78E4 File Offset: 0x000F5AE4
	public static Vector2 RoundToInt(this Vector2 vector)
	{
		return new Vector2((float)Mathf.RoundToInt(vector.x), (float)Mathf.RoundToInt(vector.y));
	}

	// Token: 0x0600407F RID: 16511 RVA: 0x000F7908 File Offset: 0x000F5B08
	public static Vector2 Quantize(this Vector2 vector, float discreteValue)
	{
		vector.x = (float)Mathf.RoundToInt(vector.x / discreteValue) * discreteValue;
		vector.y = (float)Mathf.RoundToInt(vector.y / discreteValue) * discreteValue;
		return vector;
	}

	// Token: 0x06004080 RID: 16512 RVA: 0x000F793C File Offset: 0x000F5B3C
	public static Vector3 Quantize(this Vector3 vector, float discreteValue)
	{
		vector.x = (float)Mathf.RoundToInt(vector.x / discreteValue) * discreteValue;
		vector.y = (float)Mathf.RoundToInt(vector.y / discreteValue) * discreteValue;
		vector.z = (float)Mathf.RoundToInt(vector.z / discreteValue) * discreteValue;
		return vector;
	}
}
