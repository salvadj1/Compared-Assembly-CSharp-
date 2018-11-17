using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007BC RID: 1980
public class dfRenderData : IDisposable
{
	// Token: 0x06004382 RID: 17282 RVA: 0x000FA25C File Offset: 0x000F845C
	internal dfRenderData(int capacity = 32)
	{
		this.Vertices = new global::dfList<Vector3>(capacity);
		this.Triangles = new global::dfList<int>(capacity);
		this.Normals = new global::dfList<Vector3>(capacity);
		this.Tangents = new global::dfList<Vector4>(capacity);
		this.UV = new global::dfList<Vector2>(capacity);
		this.Colors = new global::dfList<Color32>(capacity);
		this.Transform = Matrix4x4.identity;
	}

	// Token: 0x17000CF2 RID: 3314
	// (get) Token: 0x06004384 RID: 17284 RVA: 0x000FA2D0 File Offset: 0x000F84D0
	// (set) Token: 0x06004385 RID: 17285 RVA: 0x000FA2D8 File Offset: 0x000F84D8
	public Material Material { get; set; }

	// Token: 0x17000CF3 RID: 3315
	// (get) Token: 0x06004386 RID: 17286 RVA: 0x000FA2E4 File Offset: 0x000F84E4
	// (set) Token: 0x06004387 RID: 17287 RVA: 0x000FA2EC File Offset: 0x000F84EC
	public Shader Shader { get; set; }

	// Token: 0x17000CF4 RID: 3316
	// (get) Token: 0x06004388 RID: 17288 RVA: 0x000FA2F8 File Offset: 0x000F84F8
	// (set) Token: 0x06004389 RID: 17289 RVA: 0x000FA300 File Offset: 0x000F8500
	public Matrix4x4 Transform { get; set; }

	// Token: 0x17000CF5 RID: 3317
	// (get) Token: 0x0600438A RID: 17290 RVA: 0x000FA30C File Offset: 0x000F850C
	// (set) Token: 0x0600438B RID: 17291 RVA: 0x000FA314 File Offset: 0x000F8514
	public global::dfList<Vector3> Vertices { get; set; }

	// Token: 0x17000CF6 RID: 3318
	// (get) Token: 0x0600438C RID: 17292 RVA: 0x000FA320 File Offset: 0x000F8520
	// (set) Token: 0x0600438D RID: 17293 RVA: 0x000FA328 File Offset: 0x000F8528
	public global::dfList<Vector2> UV { get; set; }

	// Token: 0x17000CF7 RID: 3319
	// (get) Token: 0x0600438E RID: 17294 RVA: 0x000FA334 File Offset: 0x000F8534
	// (set) Token: 0x0600438F RID: 17295 RVA: 0x000FA33C File Offset: 0x000F853C
	public global::dfList<Vector3> Normals { get; set; }

	// Token: 0x17000CF8 RID: 3320
	// (get) Token: 0x06004390 RID: 17296 RVA: 0x000FA348 File Offset: 0x000F8548
	// (set) Token: 0x06004391 RID: 17297 RVA: 0x000FA350 File Offset: 0x000F8550
	public global::dfList<Vector4> Tangents { get; set; }

	// Token: 0x17000CF9 RID: 3321
	// (get) Token: 0x06004392 RID: 17298 RVA: 0x000FA35C File Offset: 0x000F855C
	// (set) Token: 0x06004393 RID: 17299 RVA: 0x000FA364 File Offset: 0x000F8564
	public global::dfList<int> Triangles { get; set; }

	// Token: 0x17000CFA RID: 3322
	// (get) Token: 0x06004394 RID: 17300 RVA: 0x000FA370 File Offset: 0x000F8570
	// (set) Token: 0x06004395 RID: 17301 RVA: 0x000FA378 File Offset: 0x000F8578
	public global::dfList<Color32> Colors { get; set; }

	// Token: 0x17000CFB RID: 3323
	// (get) Token: 0x06004396 RID: 17302 RVA: 0x000FA384 File Offset: 0x000F8584
	// (set) Token: 0x06004397 RID: 17303 RVA: 0x000FA38C File Offset: 0x000F858C
	public uint Checksum { get; set; }

	// Token: 0x17000CFC RID: 3324
	// (get) Token: 0x06004398 RID: 17304 RVA: 0x000FA398 File Offset: 0x000F8598
	// (set) Token: 0x06004399 RID: 17305 RVA: 0x000FA3A0 File Offset: 0x000F85A0
	public global::dfIntersectionType Intersection { get; set; }

	// Token: 0x0600439A RID: 17306 RVA: 0x000FA3AC File Offset: 0x000F85AC
	public static global::dfRenderData Obtain()
	{
		return (global::dfRenderData.pool.Count <= 0) ? new global::dfRenderData(32) : global::dfRenderData.pool.Dequeue();
	}

	// Token: 0x0600439B RID: 17307 RVA: 0x000FA3E0 File Offset: 0x000F85E0
	public static void FlushObjectPool()
	{
		while (global::dfRenderData.pool.Count > 0)
		{
			global::dfRenderData dfRenderData = global::dfRenderData.pool.Dequeue();
			dfRenderData.Vertices.TrimExcess();
			dfRenderData.Triangles.TrimExcess();
			dfRenderData.UV.TrimExcess();
			dfRenderData.Colors.TrimExcess();
		}
	}

	// Token: 0x0600439C RID: 17308 RVA: 0x000FA43C File Offset: 0x000F863C
	public void Release()
	{
		this.Clear();
		global::dfRenderData.pool.Enqueue(this);
	}

	// Token: 0x0600439D RID: 17309 RVA: 0x000FA450 File Offset: 0x000F8650
	public void Clear()
	{
		this.Material = null;
		this.Shader = null;
		this.Transform = Matrix4x4.identity;
		this.Checksum = 0u;
		this.Intersection = global::dfIntersectionType.None;
		this.Vertices.Clear();
		this.UV.Clear();
		this.Triangles.Clear();
		this.Colors.Clear();
		this.Normals.Clear();
		this.Tangents.Clear();
	}

	// Token: 0x0600439E RID: 17310 RVA: 0x000FA4C8 File Offset: 0x000F86C8
	public bool IsValid()
	{
		int count = this.Vertices.Count;
		return count > 0 && count <= 65000 && this.UV.Count == count && this.Colors.Count == count;
	}

	// Token: 0x0600439F RID: 17311 RVA: 0x000FA518 File Offset: 0x000F8718
	public void EnsureCapacity(int capacity)
	{
		this.Vertices.EnsureCapacity(capacity);
		this.Triangles.EnsureCapacity(capacity);
		this.UV.EnsureCapacity(capacity);
		this.Colors.EnsureCapacity(capacity);
	}

	// Token: 0x060043A0 RID: 17312 RVA: 0x000FA558 File Offset: 0x000F8758
	public void Merge(global::dfRenderData buffer, bool transformVertices = true)
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

	// Token: 0x060043A1 RID: 17313 RVA: 0x000FA68C File Offset: 0x000F888C
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

	// Token: 0x060043A2 RID: 17314 RVA: 0x000FA720 File Offset: 0x000F8920
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

	// Token: 0x060043A3 RID: 17315 RVA: 0x000FA78C File Offset: 0x000F898C
	public void Dispose()
	{
		this.Release();
	}

	// Token: 0x040023DC RID: 9180
	private static Queue<global::dfRenderData> pool = new Queue<global::dfRenderData>();
}
