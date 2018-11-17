using System;
using UnityEngine;

// Token: 0x020003D6 RID: 982
public static class Gizmos2
{
	// Token: 0x170008D4 RID: 2260
	// (get) Token: 0x0600249B RID: 9371 RVA: 0x0008BDC8 File Offset: 0x00089FC8
	// (set) Token: 0x0600249C RID: 9372 RVA: 0x0008BDD0 File Offset: 0x00089FD0
	public static Color color
	{
		get
		{
			return Gizmos.color;
		}
		set
		{
			Gizmos.color = value;
		}
	}

	// Token: 0x170008D5 RID: 2261
	// (get) Token: 0x0600249D RID: 9373 RVA: 0x0008BDD8 File Offset: 0x00089FD8
	// (set) Token: 0x0600249E RID: 9374 RVA: 0x0008BDE0 File Offset: 0x00089FE0
	public static Matrix4x4 matrix
	{
		get
		{
			return Gizmos.matrix;
		}
		set
		{
			Gizmos.matrix = value;
		}
	}

	// Token: 0x0600249F RID: 9375 RVA: 0x0008BDE8 File Offset: 0x00089FE8
	public static void DrawRay(Ray r)
	{
		Gizmos.DrawRay(r);
	}

	// Token: 0x060024A0 RID: 9376 RVA: 0x0008BDF0 File Offset: 0x00089FF0
	public static void DrawRay(Vector3 from, Vector3 direction)
	{
		Gizmos.DrawRay(from, direction);
	}

	// Token: 0x060024A1 RID: 9377 RVA: 0x0008BDFC File Offset: 0x00089FFC
	public static void DrawLine(Vector3 from, Vector3 to)
	{
		Gizmos.DrawLine(from, to);
	}

	// Token: 0x060024A2 RID: 9378 RVA: 0x0008BE08 File Offset: 0x0008A008
	public static void DrawWireSphere(Vector3 center, float radius)
	{
		Gizmos.DrawWireSphere(center, radius);
	}

	// Token: 0x060024A3 RID: 9379 RVA: 0x0008BE14 File Offset: 0x0008A014
	public static void DrawSphere(Vector3 center, float radius)
	{
		Gizmos.DrawSphere(center, radius);
	}

	// Token: 0x060024A4 RID: 9380 RVA: 0x0008BE20 File Offset: 0x0008A020
	public static void DrawWireCube(Vector3 center, Vector3 size)
	{
		Gizmos.DrawWireCube(center, size);
	}

	// Token: 0x060024A5 RID: 9381 RVA: 0x0008BE2C File Offset: 0x0008A02C
	public static void DrawCube(Vector3 center, Vector3 size)
	{
		Gizmos.DrawCube(center, size);
	}

	// Token: 0x060024A6 RID: 9382 RVA: 0x0008BE38 File Offset: 0x0008A038
	public static void DrawIcon(Vector3 center, string name, bool allowScaling)
	{
		Gizmos.DrawIcon(center, name, allowScaling);
	}

	// Token: 0x060024A7 RID: 9383 RVA: 0x0008BE44 File Offset: 0x0008A044
	public static void DrawIcon(Vector3 center, string name)
	{
		Gizmos.DrawIcon(center, name);
	}

	// Token: 0x060024A8 RID: 9384 RVA: 0x0008BE50 File Offset: 0x0008A050
	public static void DrawGUITexture(Rect screenRect, Texture texture)
	{
		Gizmos.DrawGUITexture(screenRect, texture);
	}

	// Token: 0x060024A9 RID: 9385 RVA: 0x0008BE5C File Offset: 0x0008A05C
	public static void DrawGUITexture(Rect screenRect, Texture texture, Material mat)
	{
		Gizmos.DrawGUITexture(screenRect, texture, mat);
	}

	// Token: 0x060024AA RID: 9386 RVA: 0x0008BE68 File Offset: 0x0008A068
	public static void DrawGUITexture(Rect screenRect, Texture texture, int leftBorder, int rightBorder, int topBorder, int bottomBorder, Material mat)
	{
		Gizmos2.DrawGUITexture(screenRect, texture, leftBorder, rightBorder, topBorder, bottomBorder, mat);
	}

	// Token: 0x060024AB RID: 9387 RVA: 0x0008BE7C File Offset: 0x0008A07C
	public static void DrawGUITexture(Rect screenRect, Texture texture, int leftBorder, int rightBorder, int topBorder, int bottomBorder)
	{
		Gizmos2.DrawGUITexture(screenRect, texture, leftBorder, rightBorder, topBorder, bottomBorder);
	}

	// Token: 0x060024AC RID: 9388 RVA: 0x0008BE8C File Offset: 0x0008A08C
	public static void DrawFrustum(Vector3 center, float fov, float maxRange, float minRange, float aspect)
	{
		Gizmos.DrawFrustum(center, fov, maxRange, minRange, aspect);
	}

	// Token: 0x060024AD RID: 9389 RVA: 0x0008BE9C File Offset: 0x0008A09C
	public static void DrawWireCapsule(Vector3 center, float radius, float height, int axis)
	{
		int num = axis % 3;
		Vector3 vector;
		Vector3 vector2;
		Vector3 vector3;
		switch (num + 2)
		{
		case 0:
		case 3:
			vector = Vector3.up;
			vector2 = Vector3.forward;
			vector3 = Vector3.right;
			break;
		case 1:
		case 4:
			vector = Vector3.forward;
			vector2 = Vector3.right;
			vector3 = Vector3.up;
			break;
		case 2:
			vector = Vector3.right;
			vector2 = Vector3.up;
			vector3 = Vector3.forward;
			break;
		default:
			return;
		}
		Vector3 vector4 = Vector3.one - vector2 * 2f;
		Vector3 vector5 = Vector3.one - vector3 * 2f;
		if (radius * 2f >= height)
		{
			Gizmos.DrawWireSphere(center, radius);
		}
		else
		{
			Vector3 vector6 = center + vector * ((height - radius * 2f) / 2f);
			Vector3 vector7 = center - vector * ((height - radius * 2f) / 2f);
			Gizmos.DrawLine(vector6 + vector2 * radius, vector7 + vector2 * radius);
			Gizmos.DrawLine(vector6 + vector3 * radius, vector7 + vector3 * radius);
			Gizmos.DrawLine(vector6 - vector2 * radius, vector7 - vector2 * radius);
			Gizmos.DrawLine(vector6 - vector3 * radius, vector7 - vector3 * radius);
			for (int i = 0; i < 6; i++)
			{
				float num2 = (float)i / 12f * 3.14159274f;
				float num3 = ((float)i + 1f) / 12f * 3.14159274f;
				float num4 = Mathf.Cos(num2) * radius;
				float num5 = Mathf.Sin(num2) * radius;
				float num6 = Mathf.Cos(num3) * radius;
				float num7 = Mathf.Sin(num3) * radius;
				Vector3 vector8 = vector * num5 + vector2 * num4;
				Vector3 vector9 = vector * num7 + vector2 * num6;
				Vector3 vector10 = vector * num5 + vector3 * num4;
				Vector3 vector11 = vector * num7 + vector3 * num6;
				Vector3 vector12 = vector2 * num5 + vector3 * num4;
				Vector3 vector13 = vector2 * num7 + vector3 * num6;
				Gizmos.DrawLine(vector6 + vector8, vector6 + vector9);
				Gizmos.DrawLine(vector6 + vector10, vector6 + vector11);
				Gizmos.DrawLine(vector7 - vector8, vector7 - vector9);
				Gizmos.DrawLine(vector7 - vector10, vector7 - vector11);
				Gizmos.DrawLine(vector6 + vector12, vector6 + vector13);
				Gizmos.DrawLine(vector6 - vector12, vector6 - vector13);
				Gizmos.DrawLine(vector7 + vector12, vector7 + vector13);
				Gizmos.DrawLine(vector7 - vector12, vector7 - vector13);
				vector8 = Vector3.Scale(vector8, vector4);
				vector9 = Vector3.Scale(vector9, vector4);
				vector10 = Vector3.Scale(vector10, vector5);
				vector11 = Vector3.Scale(vector11, vector5);
				vector12 = Vector3.Scale(vector12, vector4);
				vector13 = Vector3.Scale(vector13, vector4);
				Gizmos.DrawLine(vector6 + vector8, vector6 + vector9);
				Gizmos.DrawLine(vector6 + vector10, vector6 + vector11);
				Gizmos.DrawLine(vector7 - vector8, vector7 - vector9);
				Gizmos.DrawLine(vector7 - vector10, vector7 - vector11);
				Gizmos.DrawLine(vector6 + vector12, vector6 + vector13);
				Gizmos.DrawLine(vector6 - vector12, vector6 - vector13);
				Gizmos.DrawLine(vector7 + vector12, vector7 + vector13);
				Gizmos.DrawLine(vector7 - vector12, vector7 - vector13);
			}
		}
	}
}
