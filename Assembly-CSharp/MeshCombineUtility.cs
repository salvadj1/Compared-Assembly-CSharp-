﻿using System;
using UnityEngine;

// Token: 0x02000945 RID: 2373
public class MeshCombineUtility
{
	// Token: 0x06004FAB RID: 20395 RVA: 0x00151D80 File Offset: 0x0014FF80
	public static Mesh Combine(global::MeshCombineUtility.MeshInstance[] combines, bool generateStrips)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		foreach (global::MeshCombineUtility.MeshInstance meshInstance in combines)
		{
			if (meshInstance.mesh)
			{
				num += meshInstance.mesh.vertexCount;
				if (generateStrips)
				{
					int num4 = meshInstance.mesh.GetTriangleStrip(meshInstance.subMeshIndex).Length;
					if (num4 != 0)
					{
						if (num3 != 0)
						{
							if ((num3 & 1) == 1)
							{
								num3 += 3;
							}
							else
							{
								num3 += 2;
							}
						}
						num3 += num4;
					}
					else
					{
						generateStrips = false;
					}
				}
			}
		}
		if (!generateStrips)
		{
			foreach (global::MeshCombineUtility.MeshInstance meshInstance2 in combines)
			{
				if (meshInstance2.mesh)
				{
					num2 += meshInstance2.mesh.GetTriangles(meshInstance2.subMeshIndex).Length;
				}
			}
		}
		Vector3[] array = new Vector3[num];
		Vector3[] array2 = new Vector3[num];
		Vector4[] array3 = new Vector4[num];
		Vector2[] array4 = new Vector2[num];
		Vector2[] array5 = new Vector2[num];
		Color[] array6 = new Color[num];
		int[] array7 = new int[num2];
		int[] array8 = new int[num3];
		int num5 = 0;
		foreach (global::MeshCombineUtility.MeshInstance meshInstance3 in combines)
		{
			if (meshInstance3.mesh)
			{
				global::MeshCombineUtility.Copy(meshInstance3.mesh.vertexCount, meshInstance3.mesh.vertices, array, ref num5, meshInstance3.transform);
			}
		}
		num5 = 0;
		foreach (global::MeshCombineUtility.MeshInstance meshInstance4 in combines)
		{
			if (meshInstance4.mesh)
			{
				Matrix4x4 transform = meshInstance4.transform;
				transform = transform.inverse.transpose;
				global::MeshCombineUtility.CopyNormal(meshInstance4.mesh.vertexCount, meshInstance4.mesh.normals, array2, ref num5, transform);
			}
		}
		num5 = 0;
		foreach (global::MeshCombineUtility.MeshInstance meshInstance5 in combines)
		{
			if (meshInstance5.mesh)
			{
				Matrix4x4 transform2 = meshInstance5.transform;
				transform2 = transform2.inverse.transpose;
				global::MeshCombineUtility.CopyTangents(meshInstance5.mesh.vertexCount, meshInstance5.mesh.tangents, array3, ref num5, transform2);
			}
		}
		num5 = 0;
		foreach (global::MeshCombineUtility.MeshInstance meshInstance6 in combines)
		{
			if (meshInstance6.mesh)
			{
				global::MeshCombineUtility.Copy(meshInstance6.mesh.vertexCount, meshInstance6.mesh.uv, array4, ref num5);
			}
		}
		num5 = 0;
		foreach (global::MeshCombineUtility.MeshInstance meshInstance7 in combines)
		{
			if (meshInstance7.mesh)
			{
				global::MeshCombineUtility.Copy(meshInstance7.mesh.vertexCount, meshInstance7.mesh.uv1, array5, ref num5);
			}
		}
		num5 = 0;
		foreach (global::MeshCombineUtility.MeshInstance meshInstance8 in combines)
		{
			if (meshInstance8.mesh)
			{
				global::MeshCombineUtility.CopyColors(meshInstance8.mesh.vertexCount, meshInstance8.mesh.colors, array6, ref num5);
			}
		}
		int num8 = 0;
		int num9 = 0;
		int num10 = 0;
		foreach (global::MeshCombineUtility.MeshInstance meshInstance9 in combines)
		{
			if (meshInstance9.mesh)
			{
				if (generateStrips)
				{
					int[] triangleStrip = meshInstance9.mesh.GetTriangleStrip(meshInstance9.subMeshIndex);
					if (num9 != 0)
					{
						if ((num9 & 1) == 1)
						{
							array8[num9] = array8[num9 - 1];
							array8[num9 + 1] = triangleStrip[0] + num10;
							array8[num9 + 2] = triangleStrip[0] + num10;
							num9 += 3;
						}
						else
						{
							array8[num9] = array8[num9 - 1];
							array8[num9 + 1] = triangleStrip[0] + num10;
							num9 += 2;
						}
					}
					for (int num12 = 0; num12 < triangleStrip.Length; num12++)
					{
						array8[num12 + num9] = triangleStrip[num12] + num10;
					}
					num9 += triangleStrip.Length;
				}
				else
				{
					int[] triangles = meshInstance9.mesh.GetTriangles(meshInstance9.subMeshIndex);
					for (int num13 = 0; num13 < triangles.Length; num13++)
					{
						array7[num13 + num8] = triangles[num13] + num10;
					}
					num8 += triangles.Length;
				}
				num10 += meshInstance9.mesh.vertexCount;
			}
		}
		Mesh mesh = new Mesh();
		mesh.name = "Combined Mesh";
		mesh.vertices = array;
		mesh.normals = array2;
		mesh.colors = array6;
		mesh.uv = array4;
		mesh.uv1 = array5;
		mesh.tangents = array3;
		if (generateStrips)
		{
			mesh.SetTriangleStrip(array8, 0);
		}
		else
		{
			mesh.triangles = array7;
		}
		return mesh;
	}

	// Token: 0x06004FAC RID: 20396 RVA: 0x0015230C File Offset: 0x0015050C
	private static void Copy(int vertexcount, Vector3[] src, Vector3[] dst, ref int offset, Matrix4x4 transform)
	{
		for (int i = 0; i < src.Length; i++)
		{
			dst[i + offset] = transform.MultiplyPoint(src[i]);
		}
		offset += vertexcount;
	}

	// Token: 0x06004FAD RID: 20397 RVA: 0x00152358 File Offset: 0x00150558
	private static void CopyNormal(int vertexcount, Vector3[] src, Vector3[] dst, ref int offset, Matrix4x4 transform)
	{
		for (int i = 0; i < src.Length; i++)
		{
			dst[i + offset] = transform.MultiplyVector(src[i]).normalized;
		}
		offset += vertexcount;
	}

	// Token: 0x06004FAE RID: 20398 RVA: 0x001523AC File Offset: 0x001505AC
	private static void Copy(int vertexcount, Vector2[] src, Vector2[] dst, ref int offset)
	{
		for (int i = 0; i < src.Length; i++)
		{
			dst[i + offset] = src[i];
		}
		offset += vertexcount;
	}

	// Token: 0x06004FAF RID: 20399 RVA: 0x001523F0 File Offset: 0x001505F0
	private static void CopyColors(int vertexcount, Color[] src, Color[] dst, ref int offset)
	{
		for (int i = 0; i < src.Length; i++)
		{
			dst[i + offset] = src[i];
		}
		offset += vertexcount;
	}

	// Token: 0x06004FB0 RID: 20400 RVA: 0x00152434 File Offset: 0x00150634
	private static void CopyTangents(int vertexcount, Vector4[] src, Vector4[] dst, ref int offset, Matrix4x4 transform)
	{
		for (int i = 0; i < src.Length; i++)
		{
			Vector4 vector = src[i];
			Vector3 normalized;
			normalized..ctor(vector.x, vector.y, vector.z);
			normalized = transform.MultiplyVector(normalized).normalized;
			dst[i + offset] = new Vector4(normalized.x, normalized.y, normalized.z, vector.w);
		}
		offset += vertexcount;
	}

	// Token: 0x02000946 RID: 2374
	public struct MeshInstance
	{
		// Token: 0x04002E70 RID: 11888
		public Mesh mesh;

		// Token: 0x04002E71 RID: 11889
		public int subMeshIndex;

		// Token: 0x04002E72 RID: 11890
		public Matrix4x4 transform;
	}
}
