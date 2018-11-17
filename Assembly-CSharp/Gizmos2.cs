using System;
using UnityEngine;

// Token: 0x02000483 RID: 1155
public static class Gizmos2
{
	// Token: 0x17000932 RID: 2354
	// (get) Token: 0x060027FD RID: 10237 RVA: 0x000911C4 File Offset: 0x0008F3C4
	// (set) Token: 0x060027FE RID: 10238 RVA: 0x000911CC File Offset: 0x0008F3CC
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

	// Token: 0x17000933 RID: 2355
	// (get) Token: 0x060027FF RID: 10239 RVA: 0x000911D4 File Offset: 0x0008F3D4
	// (set) Token: 0x06002800 RID: 10240 RVA: 0x000911DC File Offset: 0x0008F3DC
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

	// Token: 0x06002801 RID: 10241 RVA: 0x000911E4 File Offset: 0x0008F3E4
	public static void DrawRay(Ray r)
	{
		Gizmos.DrawRay(r);
	}

	// Token: 0x06002802 RID: 10242 RVA: 0x000911EC File Offset: 0x0008F3EC
	public static void DrawRay(Vector3 from, Vector3 direction)
	{
		Gizmos.DrawRay(from, direction);
	}

	// Token: 0x06002803 RID: 10243 RVA: 0x000911F8 File Offset: 0x0008F3F8
	public static void DrawLine(Vector3 from, Vector3 to)
	{
		Gizmos.DrawLine(from, to);
	}

	// Token: 0x06002804 RID: 10244 RVA: 0x00091204 File Offset: 0x0008F404
	public static void DrawWireSphere(Vector3 center, float radius)
	{
		Gizmos.DrawWireSphere(center, radius);
	}

	// Token: 0x06002805 RID: 10245 RVA: 0x00091210 File Offset: 0x0008F410
	public static void DrawSphere(Vector3 center, float radius)
	{
		Gizmos.DrawSphere(center, radius);
	}

	// Token: 0x06002806 RID: 10246 RVA: 0x0009121C File Offset: 0x0008F41C
	public static void DrawWireCube(Vector3 center, Vector3 size)
	{
		Gizmos.DrawWireCube(center, size);
	}

	// Token: 0x06002807 RID: 10247 RVA: 0x00091228 File Offset: 0x0008F428
	public static void DrawCube(Vector3 center, Vector3 size)
	{
		Gizmos.DrawCube(center, size);
	}

	// Token: 0x06002808 RID: 10248 RVA: 0x00091234 File Offset: 0x0008F434
	public static void DrawIcon(Vector3 center, string name, bool allowScaling)
	{
		Gizmos.DrawIcon(center, name, allowScaling);
	}

	// Token: 0x06002809 RID: 10249 RVA: 0x00091240 File Offset: 0x0008F440
	public static void DrawIcon(Vector3 center, string name)
	{
		Gizmos.DrawIcon(center, name);
	}

	// Token: 0x0600280A RID: 10250 RVA: 0x0009124C File Offset: 0x0008F44C
	public static void DrawGUITexture(Rect screenRect, Texture texture)
	{
		Gizmos.DrawGUITexture(screenRect, texture);
	}

	// Token: 0x0600280B RID: 10251 RVA: 0x00091258 File Offset: 0x0008F458
	public static void DrawGUITexture(Rect screenRect, Texture texture, Material mat)
	{
		Gizmos.DrawGUITexture(screenRect, texture, mat);
	}

	// Token: 0x0600280C RID: 10252 RVA: 0x00091264 File Offset: 0x0008F464
	public static void DrawGUITexture(Rect screenRect, Texture texture, int leftBorder, int rightBorder, int topBorder, int bottomBorder, Material mat)
	{
		global::Gizmos2.DrawGUITexture(screenRect, texture, leftBorder, rightBorder, topBorder, bottomBorder, mat);
	}

	// Token: 0x0600280D RID: 10253 RVA: 0x00091278 File Offset: 0x0008F478
	public static void DrawGUITexture(Rect screenRect, Texture texture, int leftBorder, int rightBorder, int topBorder, int bottomBorder)
	{
		global::Gizmos2.DrawGUITexture(screenRect, texture, leftBorder, rightBorder, topBorder, bottomBorder);
	}

	// Token: 0x0600280E RID: 10254 RVA: 0x00091288 File Offset: 0x0008F488
	public static void DrawFrustum(Vector3 center, float fov, float maxRange, float minRange, float aspect)
	{
		Gizmos.DrawFrustum(center, fov, maxRange, minRange, aspect);
	}

	// Token: 0x0600280F RID: 10255 RVA: 0x00091298 File Offset: 0x0008F498
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
