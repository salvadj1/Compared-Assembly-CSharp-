using System;
using UnityEngine;

// Token: 0x020007F0 RID: 2032
public class dfMarkupBoxSprite : global::dfMarkupBox
{
	// Token: 0x060046B4 RID: 18100 RVA: 0x0010AD8C File Offset: 0x00108F8C
	public dfMarkupBoxSprite(global::dfMarkupElement element, global::dfMarkupDisplayType display, global::dfMarkupStyle style) : base(element, display, style)
	{
	}

	// Token: 0x17000D98 RID: 3480
	// (get) Token: 0x060046B6 RID: 18102 RVA: 0x0010ADBC File Offset: 0x00108FBC
	// (set) Token: 0x060046B7 RID: 18103 RVA: 0x0010ADC4 File Offset: 0x00108FC4
	public global::dfAtlas Atlas { get; set; }

	// Token: 0x17000D99 RID: 3481
	// (get) Token: 0x060046B8 RID: 18104 RVA: 0x0010ADD0 File Offset: 0x00108FD0
	// (set) Token: 0x060046B9 RID: 18105 RVA: 0x0010ADD8 File Offset: 0x00108FD8
	public string Source { get; set; }

	// Token: 0x060046BA RID: 18106 RVA: 0x0010ADE4 File Offset: 0x00108FE4
	internal void LoadImage(global::dfAtlas atlas, string source)
	{
		global::dfAtlas.ItemInfo itemInfo = atlas[source];
		if (itemInfo == null)
		{
			throw new InvalidOperationException("Sprite does not exist in atlas: " + source);
		}
		this.Atlas = atlas;
		this.Source = source;
		this.Size = itemInfo.sizeInPixels;
		this.Baseline = (int)this.Size.y;
	}

	// Token: 0x060046BB RID: 18107 RVA: 0x0010AE44 File Offset: 0x00109044
	protected override global::dfRenderData OnRebuildRenderData()
	{
		this.renderData.Clear();
		if (this.Atlas != null && this.Atlas[this.Source] != null)
		{
			global::dfSprite.RenderOptions options = new global::dfSprite.RenderOptions
			{
				atlas = this.Atlas,
				spriteInfo = this.Atlas[this.Source],
				pixelsToUnits = 1f,
				size = this.Size,
				color = this.Style.Color,
				fillAmount = 1f
			};
			global::dfSlicedSprite.renderSprite(this.renderData, options);
			this.renderData.Material = this.Atlas.Material;
			this.renderData.Transform = Matrix4x4.identity;
		}
		return this.renderData;
	}

	// Token: 0x060046BC RID: 18108 RVA: 0x0010AF30 File Offset: 0x00109130
	private static void addTriangleIndices(global::dfList<Vector3> verts, global::dfList<int> triangles)
	{
		int count = verts.Count;
		int[] triangle_INDICES = global::dfMarkupBoxSprite.TRIANGLE_INDICES;
		for (int i = 0; i < triangle_INDICES.Length; i++)
		{
			triangles.Add(count + triangle_INDICES[i]);
		}
	}

	// Token: 0x04002526 RID: 9510
	private static int[] TRIANGLE_INDICES = new int[]
	{
		0,
		1,
		2,
		0,
		2,
		3
	};

	// Token: 0x04002527 RID: 9511
	private global::dfRenderData renderData = new global::dfRenderData(32);
}
