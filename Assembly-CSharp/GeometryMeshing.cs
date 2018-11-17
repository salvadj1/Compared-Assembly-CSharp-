using System;
using UnityEngine;

// Token: 0x02000743 RID: 1859
public class GeometryMeshing
{
	// Token: 0x06003DCD RID: 15821 RVA: 0x000DE66C File Offset: 0x000DC86C
	public static global::GeometryMeshing.Mesh Sphere(global::GeometryMeshing.SphereInfo sphere)
	{
		Debug.Log("TODO");
		return default(global::GeometryMeshing.Mesh);
	}

	// Token: 0x06003DCE RID: 15822 RVA: 0x000DE68C File Offset: 0x000DC88C
	public static global::GeometryMeshing.Mesh Capsule(global::GeometryMeshing.CapsuleInfo capsule)
	{
		if (capsule.height <= capsule.radius * 2f)
		{
			global::GeometryMeshing.SphereInfo sphere;
			sphere.offset = capsule.offset;
			sphere.radius = capsule.radius;
			sphere.capSplit = capsule.capSplit;
			sphere.sides = capsule.sides;
			return global::GeometryMeshing.Sphere(sphere);
		}
		bool flag = capsule.capSplit == 0;
		int num = (!flag) ? (capsule.capSplit - 1) : 0;
		int num2 = (!flag) ? (num * capsule.sides + 1) : 0;
		Vector3[] array = new Vector3[capsule.sides * 2 + ((!flag) ? (2 + num * capsule.sides * 2) : 0)];
		float num3 = capsule.offset.y - capsule.height / 2f;
		float y = num3 + capsule.radius;
		float num4 = capsule.offset.y + capsule.height / 2f;
		float y2 = num4 - capsule.radius;
		for (int i = 0; i < capsule.sides; i++)
		{
			float num5 = (float)i / ((float)capsule.sides / 2f) * 3.14159274f;
			int num6 = i + num2;
			int num7 = num6 + capsule.sides;
			array[num6].x = (array[num7].x = capsule.offset.x + Mathf.Cos(num5) * capsule.radius);
			array[num6].z = (array[num7].z = capsule.offset.z + Mathf.Sin(num5) * capsule.radius);
			array[num6].y = y;
			array[num7].y = y2;
		}
		if (!flag)
		{
			array[0] = new Vector3(capsule.offset.x, num3, capsule.offset.z);
			array[array.Length - 1] = new Vector3(capsule.offset.x, num4, capsule.offset.z);
		}
		int[] array2 = new int[3 * (((!flag) ? capsule.sides : (capsule.sides - 1)) * 2 + capsule.sides * 2)];
		int num8 = 0;
		if (flag)
		{
			for (int j = 1; j < capsule.sides; j++)
			{
				array2[num8++] = j + num2;
				array2[num8++] = (j + 1) % capsule.sides + num2;
				array2[num8++] = 0;
			}
			for (int k = 0; k < capsule.sides - 1; k++)
			{
				array2[num8++] = k + (num2 + capsule.sides);
				array2[num8++] = (k + 1) % capsule.sides + num2 + capsule.sides;
				array2[num8++] = array.Length - 1;
			}
		}
		else
		{
			for (int l = 0; l < capsule.sides; l++)
			{
				array2[num8++] = l + num2;
				array2[num8++] = (l + 1) % capsule.sides + num2;
				array2[num8++] = 0;
			}
			for (int m = 0; m < capsule.sides; m++)
			{
				array2[num8++] = m + (num2 + capsule.sides);
				array2[num8++] = array.Length - 1;
				array2[num8++] = (m + 1) % capsule.sides + (num2 + capsule.sides);
			}
		}
		for (int n = 0; n < capsule.sides; n++)
		{
			array2[num8++] = n + num2;
			array2[num8++] = n + num2 + capsule.sides;
			array2[num8++] = (n + 1) % capsule.sides + num2;
			array2[num8++] = n + num2 + capsule.sides;
			array2[num8++] = (n + 1) % capsule.sides + num2 + capsule.sides;
			array2[num8++] = (n + 1) % capsule.sides + num2;
		}
		return new global::GeometryMeshing.Mesh(array, array2, global::GeometryMeshing.IndexKind.Triangles);
	}

	// Token: 0x02000744 RID: 1860
	public enum IndexKind : sbyte
	{
		// Token: 0x04001FBA RID: 8122
		Invalid,
		// Token: 0x04001FBB RID: 8123
		Triangles,
		// Token: 0x04001FBC RID: 8124
		TriangleStrip
	}

	// Token: 0x02000745 RID: 1861
	public struct Mesh
	{
		// Token: 0x06003DCF RID: 15823 RVA: 0x000DEB38 File Offset: 0x000DCD38
		internal Mesh(Vector3[] vertices, int[] indices, global::GeometryMeshing.IndexKind kind)
		{
			this.vertices = vertices;
			this.indices = indices;
			this.vertexCount = (ushort)this.vertices.Length;
			this.indexCount = (uint)this.indices.Length;
			this.indexKind = kind;
		}

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x06003DD0 RID: 15824 RVA: 0x000DEB78 File Offset: 0x000DCD78
		public bool valid
		{
			get
			{
				return (int)this.indexKind != 0;
			}
		}

		// Token: 0x04001FBD RID: 8125
		public readonly Vector3[] vertices;

		// Token: 0x04001FBE RID: 8126
		public readonly int[] indices;

		// Token: 0x04001FBF RID: 8127
		public readonly uint indexCount;

		// Token: 0x04001FC0 RID: 8128
		public readonly ushort vertexCount;

		// Token: 0x04001FC1 RID: 8129
		public readonly global::GeometryMeshing.IndexKind indexKind;
	}

	// Token: 0x02000746 RID: 1862
	public struct CapsuleInfo
	{
		// Token: 0x04001FC2 RID: 8130
		public Vector3 offset;

		// Token: 0x04001FC3 RID: 8131
		public float height;

		// Token: 0x04001FC4 RID: 8132
		public float radius;

		// Token: 0x04001FC5 RID: 8133
		public int sides;

		// Token: 0x04001FC6 RID: 8134
		public int capSplit;
	}

	// Token: 0x02000747 RID: 1863
	public struct SphereInfo
	{
		// Token: 0x04001FC7 RID: 8135
		public Vector3 offset;

		// Token: 0x04001FC8 RID: 8136
		public float radius;

		// Token: 0x04001FC9 RID: 8137
		public int sides;

		// Token: 0x04001FCA RID: 8138
		public int capSplit;
	}
}
