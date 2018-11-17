using System;
using UnityEngine;

// Token: 0x0200067E RID: 1662
public class GeometryMeshing
{
	// Token: 0x060039D5 RID: 14805 RVA: 0x000D5C8C File Offset: 0x000D3E8C
	public static GeometryMeshing.Mesh Sphere(GeometryMeshing.SphereInfo sphere)
	{
		Debug.Log("TODO");
		return default(GeometryMeshing.Mesh);
	}

	// Token: 0x060039D6 RID: 14806 RVA: 0x000D5CAC File Offset: 0x000D3EAC
	public static GeometryMeshing.Mesh Capsule(GeometryMeshing.CapsuleInfo capsule)
	{
		if (capsule.height <= capsule.radius * 2f)
		{
			GeometryMeshing.SphereInfo sphere;
			sphere.offset = capsule.offset;
			sphere.radius = capsule.radius;
			sphere.capSplit = capsule.capSplit;
			sphere.sides = capsule.sides;
			return GeometryMeshing.Sphere(sphere);
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
		return new GeometryMeshing.Mesh(array, array2, GeometryMeshing.IndexKind.Triangles);
	}

	// Token: 0x0200067F RID: 1663
	public enum IndexKind : sbyte
	{
		// Token: 0x04001DC2 RID: 7618
		Invalid,
		// Token: 0x04001DC3 RID: 7619
		Triangles,
		// Token: 0x04001DC4 RID: 7620
		TriangleStrip
	}

	// Token: 0x02000680 RID: 1664
	public struct Mesh
	{
		// Token: 0x060039D7 RID: 14807 RVA: 0x000D6158 File Offset: 0x000D4358
		internal Mesh(Vector3[] vertices, int[] indices, GeometryMeshing.IndexKind kind)
		{
			this.vertices = vertices;
			this.indices = indices;
			this.vertexCount = (ushort)this.vertices.Length;
			this.indexCount = (uint)this.indices.Length;
			this.indexKind = kind;
		}

		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x060039D8 RID: 14808 RVA: 0x000D6198 File Offset: 0x000D4398
		public bool valid
		{
			get
			{
				return (int)this.indexKind != 0;
			}
		}

		// Token: 0x04001DC5 RID: 7621
		public readonly Vector3[] vertices;

		// Token: 0x04001DC6 RID: 7622
		public readonly int[] indices;

		// Token: 0x04001DC7 RID: 7623
		public readonly uint indexCount;

		// Token: 0x04001DC8 RID: 7624
		public readonly ushort vertexCount;

		// Token: 0x04001DC9 RID: 7625
		public readonly GeometryMeshing.IndexKind indexKind;
	}

	// Token: 0x02000681 RID: 1665
	public struct CapsuleInfo
	{
		// Token: 0x04001DCA RID: 7626
		public Vector3 offset;

		// Token: 0x04001DCB RID: 7627
		public float height;

		// Token: 0x04001DCC RID: 7628
		public float radius;

		// Token: 0x04001DCD RID: 7629
		public int sides;

		// Token: 0x04001DCE RID: 7630
		public int capSplit;
	}

	// Token: 0x02000682 RID: 1666
	public struct SphereInfo
	{
		// Token: 0x04001DCF RID: 7631
		public Vector3 offset;

		// Token: 0x04001DD0 RID: 7632
		public float radius;

		// Token: 0x04001DD1 RID: 7633
		public int sides;

		// Token: 0x04001DD2 RID: 7634
		public int capSplit;
	}
}
