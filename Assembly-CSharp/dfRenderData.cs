using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006EA RID: 1770
public class dfRenderData : IDisposable
{
	// Token: 0x06003F66 RID: 16230 RVA: 0x000F1658 File Offset: 0x000EF858
	internal dfRenderData(int capacity = 32)
	{
		this.Vertices = new dfList<Vector3>(capacity);
		this.Triangles = new dfList<int>(capacity);
		this.Normals = new dfList<Vector3>(capacity);
		this.Tangents = new dfList<Vector4>(capacity);
		this.UV = new dfList<Vector2>(capacity);
		this.Colors = new dfList<Color32>(capacity);
		this.Transform = Matrix4x4.identity;
	}

	// Token: 0x17000C6E RID: 3182
	// (get) Token: 0x06003F68 RID: 16232 RVA: 0x000F16CC File Offset: 0x000EF8CC
	// (set) Token: 0x06003F69 RID: 16233 RVA: 0x000F16D4 File Offset: 0x000EF8D4
	public Material Material { get; set; }

	// Token: 0x17000C6F RID: 3183
	// (get) Token: 0x06003F6A RID: 16234 RVA: 0x000F16E0 File Offset: 0x000EF8E0
	// (set) Token: 0x06003F6B RID: 16235 RVA: 0x000F16E8 File Offset: 0x000EF8E8
	public Shader Shader { get; set; }

	// Token: 0x17000C70 RID: 3184
	// (get) Token: 0x06003F6C RID: 16236 RVA: 0x000F16F4 File Offset: 0x000EF8F4
	// (set) Token: 0x06003F6D RID: 16237 RVA: 0x000F16FC File Offset: 0x000EF8FC
	public Matrix4x4 Transform { get; set; }

	// Token: 0x17000C71 RID: 3185
	// (get) Token: 0x06003F6E RID: 16238 RVA: 0x000F1708 File Offset: 0x000EF908
	// (set) Token: 0x06003F6F RID: 16239 RVA: 0x000F1710 File Offset: 0x000EF910
	public dfList<Vector3> Vertices { get; set; }

	// Token: 0x17000C72 RID: 3186
	// (get) Token: 0x06003F70 RID: 16240 RVA: 0x000F171C File Offset: 0x000EF91C
	// (set) Token: 0x06003F71 RID: 16241 RVA: 0x000F1724 File Offset: 0x000EF924
	public dfList<Vector2> UV { get; set; }

	// Token: 0x17000C73 RID: 3187
	// (get) Token: 0x06003F72 RID: 16242 RVA: 0x000F1730 File Offset: 0x000EF930
	// (set) Token: 0x06003F73 RID: 16243 RVA: 0x000F1738 File Offset: 0x000EF938
	public dfList<Vector3> Normals { get; set; }

	// Token: 0x17000C74 RID: 3188
	// (get) Token: 0x06003F74 RID: 16244 RVA: 0x000F1744 File Offset: 0x000EF944
	// (set) Token: 0x06003F75 RID: 16245 RVA: 0x000F174C File Offset: 0x000EF94C
	public dfList<Vector4> Tangents { get; set; }

	// Token: 0x17000C75 RID: 3189
	// (get) Token: 0x06003F76 RID: 16246 RVA: 0x000F1758 File Offset: 0x000EF958
	// (set) Token: 0x06003F77 RID: 16247 RVA: 0x000F1760 File Offset: 0x000EF960
	public dfList<int> Triangles { get; set; }

	// Token: 0x17000C76 RID: 3190
	// (get) Token: 0x06003F78 RID: 16248 RVA: 0x000F176C File Offset: 0x000EF96C
	// (set) Token: 0x06003F79 RID: 16249 RVA: 0x000F1774 File Offset: 0x000EF974
	public dfList<Color32> Colors { get; set; }

	// Token: 0x17000C77 RID: 3191
	// (get) Token: 0x06003F7A RID: 16250 RVA: 0x000F1780 File Offset: 0x000EF980
	// (set) Token: 0x06003F7B RID: 16251 RVA: 0x000F1788 File Offset: 0x000EF988
	public uint Checksum { get; set; }

	// Token: 0x17000C78 RID: 3192
	// (get) Token: 0x06003F7C RID: 16252 RVA: 0x000F1794 File Offset: 0x000EF994
	// (set) Token: 0x06003F7D RID: 16253 RVA: 0x000F179C File Offset: 0x000EF99C
	public dfIntersectionType Intersection { get; set; }

	// Token: 0x06003F7E RID: 16254 RVA: 0x000F17A8 File Offset: 0x000EF9A8
	public static dfRenderData Obtain()
	{
		return (dfRenderData.pool.Count <= 0) ? new dfRenderData(32) : dfRenderData.pool.Dequeue();
	}

	// Token: 0x06003F7F RID: 16255 RVA: 0x000F17DC File Offset: 0x000EF9DC
	public static void FlushObjectPool()
	{
		while (dfRenderData.pool.Count > 0)
		{
			dfRenderData dfRenderData = dfRenderData.pool.Dequeue();
			dfRenderData.Vertices.TrimExcess();
			dfRenderData.Triangles.TrimExcess();
			dfRenderData.UV.TrimExcess();
			dfRenderData.Colors.TrimExcess();
		}
	}

	// Token: 0x06003F80 RID: 16256 RVA: 0x000F1838 File Offset: 0x000EFA38
	public void Release()
	{
		this.Clear();
		dfRenderData.pool.Enqueue(this);
	}

	// Token: 0x06003F81 RID: 16257 RVA: 0x000F184C File Offset: 0x000EFA4C
	public void Clear()
	{
		this.Material = null;
		this.Shader = null;
		this.Transform = Matrix4x4.identity;
		this.Checksum = 0u;
		this.Intersection = dfIntersectionType.None;
		this.Vertices.Clear();
		this.UV.Clear();
		this.Triangles.Clear();
		this.Colors.Clear();
		this.Normals.Clear();
		this.Tangents.Clear();
	}

	// Token: 0x06003F82 RID: 16258 RVA: 0x000F18C4 File Offset: 0x000EFAC4
	public bool IsValid()
	{
		int count = this.Vertices.Count;
		return count > 0 && count <= 65000 && this.UV.Count == count && this.Colors.Count == count;
	}

	// Token: 0x06003F83 RID: 16259 RVA: 0x000F1914 File Offset: 0x000EFB14
	public void EnsureCapacity(int capacity)
	{
		this.Vertices.EnsureCapacity(capacity);
		this.Triangles.EnsureCapacity(capacity);
		this.UV.EnsureCapacity(capacity);
		this.Colors.EnsureCapacity(capacity);
	}

	// Token: 0x06003F84 RID: 16260 RVA: 0x000F1954 File Offset: 0x000EFB54
	public void Merge(dfRenderData buffer, bool transformVertices = true)
	{
		int count = this.Vertices.Count;
		this.Vertices.EnsureCapacity(this.Vertices.Count + buffer.Vertices.Count);
		if (transformVertices)
		{
			for (int i = 0; i < buffer.Vertices.Count; i++)
			{
				this.Vertices.Add(buffer.Transform.MultiplyPoint(buffer.Vertices[i]));
			}
		}
		else
		{
			this.Vertices.AddRange(buffer.Vertices);
		}
		this.UV.AddRange(buffer.UV);
		this.Colors.AddRange(buffer.Colors);
		this.Normals.AddRange(buffer.Normals);
		this.Tangents.AddRange(buffer.Tangents);
		this.Triangles.EnsureCapacity(this.Triangles.Count + buffer.Triangles.Count);
		for (int j = 0; j < buffer.Triangles.Count; j++)
		{
			this.Triangles.Add(buffer.Triangles[j] + count);
		}
	}

	// Token: 0x06003F85 RID: 16261 RVA: 0x000F1A88 File Offset: 0x000EFC88
	internal void ApplyTransform(Matrix4x4 transform)
	{
		for (int i = 0; i < this.Vertices.Count; i++)
		{
			this.Vertices[i] = transform.MultiplyPoint(this.Vertices[i]);
		}
		if (this.Normals.Count > 0)
		{
			for (int j = 0; j < this.Vertices.Count; j++)
			{
				this.Normals[j] = transform.MultiplyVector(this.Normals[j]);
			}
		}
	}

	// Token: 0x06003F86 RID: 16262 RVA: 0x000F1B1C File Offset: 0x000EFD1C
	public override string ToString()
	{
		return string.Format("V:{0} T:{1} U:{2} C:{3}", new object[]
		{
			this.Vertices.Count,
			this.Triangles.Count,
			this.UV.Count,
			this.Colors.Count
		});
	}

	// Token: 0x06003F87 RID: 16263 RVA: 0x000F1B88 File Offset: 0x000EFD88
	public void Dispose()
	{
		this.Release();
	}

	// Token: 0x040021D3 RID: 8659
	private static Queue<dfRenderData> pool = new Queue<dfRenderData>();
}
