using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006A4 RID: 1700
public class dfClippingUtil
{
	// Token: 0x06003AFD RID: 15101 RVA: 0x000DCDA8 File Offset: 0x000DAFA8
	public static void Clip(IList<Plane> planes, dfRenderData source, dfRenderData dest)
	{
		dest.EnsureCapacity(dest.Vertices.Count + source.Vertices.Count);
		for (int i = 0; i < source.Triangles.Count; i += 3)
		{
			for (int j = 0; j < 3; j++)
			{
				int index = source.Triangles[i + j];
				dfClippingUtil.clipSource[0].corner[j] = source.Transform.MultiplyPoint(source.Vertices[index]);
				dfClippingUtil.clipSource[0].uv[j] = source.UV[index];
				dfClippingUtil.clipSource[0].color[j] = source.Colors[index];
			}
			int num = 1;
			for (int k = 0; k < planes.Count; k++)
			{
				num = dfClippingUtil.clipToPlane(planes[k], dfClippingUtil.clipSource, dfClippingUtil.clipDest, num);
				dfClippingUtil.ClipTriangle[] array = dfClippingUtil.clipSource;
				dfClippingUtil.clipSource = dfClippingUtil.clipDest;
				dfClippingUtil.clipDest = array;
			}
			for (int l = 0; l < num; l++)
			{
				dfClippingUtil.clipSource[l].CopyTo(dest);
			}
		}
	}

	// Token: 0x06003AFE RID: 15102 RVA: 0x000DCF0C File Offset: 0x000DB10C
	private static int clipToPlane(Plane plane, dfClippingUtil.ClipTriangle[] source, dfClippingUtil.ClipTriangle[] dest, int count)
	{
		int num = 0;
		for (int i = 0; i < count; i++)
		{
			num += dfClippingUtil.clipToPlane(plane, source[i], dest, num);
		}
		return num;
	}

	// Token: 0x06003AFF RID: 15103 RVA: 0x000DCF48 File Offset: 0x000DB148
	private static int clipToPlane(Plane plane, dfClippingUtil.ClipTriangle triangle, dfClippingUtil.ClipTriangle[] dest, int destIndex)
	{
		Vector3[] corner = triangle.corner;
		int num = 0;
		int num2 = 0;
		Vector3 normal = plane.normal;
		float distance = plane.distance;
		for (int i = 0; i < 3; i++)
		{
			if (Vector3.Dot(normal, corner[i]) + distance > 0f)
			{
				dfClippingUtil.inside[num++] = i;
			}
			else
			{
				num2 = i;
			}
		}
		if (num == 3)
		{
			triangle.CopyTo(dest[destIndex]);
			return 1;
		}
		if (num == 0)
		{
			return 0;
		}
		if (num == 1)
		{
			int num3 = dfClippingUtil.inside[0];
			int num4 = (num3 + 1) % 3;
			int num5 = (num3 + 2) % 3;
			Vector3 vector = corner[num3];
			Vector3 vector2 = corner[num4];
			Vector3 vector3 = corner[num5];
			Vector2 vector4 = triangle.uv[num3];
			Vector2 vector5 = triangle.uv[num4];
			Vector2 vector6 = triangle.uv[num5];
			Color32 color = triangle.color[num3];
			Color32 color2 = triangle.color[num4];
			Color32 color3 = triangle.color[num5];
			float num6 = 0f;
			Vector3 vector7 = vector2 - vector;
			Ray ray;
			ray..ctor(vector, vector7.normalized);
			plane.Raycast(ray, ref num6);
			float num7 = num6 / vector7.magnitude;
			Vector3 vector8 = ray.origin + ray.direction * num6;
			Vector2 vector9 = Vector2.Lerp(vector4, vector5, num7);
			Color color4 = Color.Lerp(color, color2, num7);
			vector7 = vector3 - vector;
			ray..ctor(vector, vector7.normalized);
			plane.Raycast(ray, ref num6);
			num7 = num6 / vector7.magnitude;
			Vector3 vector10 = ray.origin + ray.direction * num6;
			Vector2 vector11 = Vector2.Lerp(vector4, vector6, num7);
			Color color5 = Color.Lerp(color, color3, num7);
			dest[destIndex].corner[0] = vector;
			dest[destIndex].corner[1] = vector8;
			dest[destIndex].corner[2] = vector10;
			dest[destIndex].uv[0] = vector4;
			dest[destIndex].uv[1] = vector9;
			dest[destIndex].uv[2] = vector11;
			dest[destIndex].color[0] = color;
			dest[destIndex].color[1] = color4;
			dest[destIndex].color[2] = color5;
			return 1;
		}
		int num8 = num2;
		int num9 = (num8 + 1) % 3;
		int num10 = (num8 + 2) % 3;
		Vector3 vector12 = corner[num8];
		Vector3 vector13 = corner[num9];
		Vector3 vector14 = corner[num10];
		Vector2 vector15 = triangle.uv[num8];
		Vector2 vector16 = triangle.uv[num9];
		Vector2 vector17 = triangle.uv[num10];
		Color32 color6 = triangle.color[num8];
		Color32 color7 = triangle.color[num9];
		Color32 color8 = triangle.color[num10];
		Vector3 vector18 = vector13 - vector12;
		Ray ray2;
		ray2..ctor(vector12, vector18.normalized);
		float num11 = 0f;
		plane.Raycast(ray2, ref num11);
		float num12 = num11 / vector18.magnitude;
		Vector3 vector19 = ray2.origin + ray2.direction * num11;
		Vector2 vector20 = Vector2.Lerp(vector15, vector16, num12);
		Color color9 = Color.Lerp(color6, color7, num12);
		vector18 = vector14 - vector12;
		ray2..ctor(vector12, vector18.normalized);
		plane.Raycast(ray2, ref num11);
		num12 = num11 / vector18.magnitude;
		Vector3 vector21 = ray2.origin + ray2.direction * num11;
		Vector2 vector22 = Vector2.Lerp(vector15, vector17, num12);
		Color color10 = Color.Lerp(color6, color8, num12);
		dest[destIndex].corner[0] = vector19;
		dest[destIndex].corner[1] = vector13;
		dest[destIndex].corner[2] = vector21;
		dest[destIndex].uv[0] = vector20;
		dest[destIndex].uv[1] = vector16;
		dest[destIndex].uv[2] = vector22;
		dest[destIndex].color[0] = color9;
		dest[destIndex].color[1] = color7;
		dest[destIndex].color[2] = color10;
		destIndex++;
		dest[destIndex].corner[0] = vector21;
		dest[destIndex].corner[1] = vector13;
		dest[destIndex].corner[2] = vector14;
		dest[destIndex].uv[0] = vector22;
		dest[destIndex].uv[1] = vector16;
		dest[destIndex].uv[2] = vector17;
		dest[destIndex].color[0] = color10;
		dest[destIndex].color[1] = color7;
		dest[destIndex].color[2] = color8;
		return 2;
	}

	// Token: 0x06003B00 RID: 15104 RVA: 0x000DD608 File Offset: 0x000DB808
	private static dfClippingUtil.ClipTriangle[] initClipBuffer(int size)
	{
		dfClippingUtil.ClipTriangle[] array = new dfClippingUtil.ClipTriangle[size];
		for (int i = 0; i < size; i++)
		{
			array[i].corner = new Vector3[3];
			array[i].uv = new Vector2[3];
			array[i].color = new Color32[3];
		}
		return array;
	}

	// Token: 0x04001F28 RID: 7976
	private static int[] inside = new int[3];

	// Token: 0x04001F29 RID: 7977
	private static dfClippingUtil.ClipTriangle[] clipSource = dfClippingUtil.initClipBuffer(1024);

	// Token: 0x04001F2A RID: 7978
	private static dfClippingUtil.ClipTriangle[] clipDest = dfClippingUtil.initClipBuffer(1024);

	// Token: 0x020006A5 RID: 1701
	protected struct ClipTriangle
	{
		// Token: 0x06003B01 RID: 15105 RVA: 0x000DD668 File Offset: 0x000DB868
		public void CopyTo(dfClippingUtil.ClipTriangle target)
		{
			Array.Copy(this.corner, target.corner, 3);
			Array.Copy(this.uv, target.uv, 3);
			Array.Copy(this.color, target.color, 3);
		}

		// Token: 0x06003B02 RID: 15106 RVA: 0x000DD6A4 File Offset: 0x000DB8A4
		public void CopyTo(dfRenderData buffer)
		{
			int count = buffer.Vertices.Count;
			buffer.Vertices.AddRange(this.corner);
			buffer.UV.AddRange(this.uv);
			buffer.Colors.AddRange(this.color);
			buffer.Triangles.Add(count);
			buffer.Triangles.Add(count + 1);
			buffer.Triangles.Add(count + 2);
		}

		// Token: 0x04001F2B RID: 7979
		public Vector3[] corner;

		// Token: 0x04001F2C RID: 7980
		public Vector2[] uv;

		// Token: 0x04001F2D RID: 7981
		public Color32[] color;
	}
}
