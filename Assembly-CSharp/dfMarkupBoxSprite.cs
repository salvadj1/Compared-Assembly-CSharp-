using System;
using UnityEngine;

// Token: 0x02000714 RID: 1812
public class dfMarkupBoxSprite : dfMarkupBox
{
	// Token: 0x06004270 RID: 17008 RVA: 0x00101A7C File Offset: 0x000FFC7C
	public dfMarkupBoxSprite(dfMarkupElement element, dfMarkupDisplayType display, dfMarkupStyle style) : base(element, display, style)
	{
	}

	// Token: 0x17000D0E RID: 3342
	// (get) Token: 0x06004272 RID: 17010 RVA: 0x00101AAC File Offset: 0x000FFCAC
	// (set) Token: 0x06004273 RID: 17011 RVA: 0x00101AB4 File Offset: 0x000FFCB4
	public dfAtlas Atlas { get; set; }

	// Token: 0x17000D0F RID: 3343
	// (get) Token: 0x06004274 RID: 17012 RVA: 0x00101AC0 File Offset: 0x000FFCC0
	// (set) Token: 0x06004275 RID: 17013 RVA: 0x00101AC8 File Offset: 0x000FFCC8
	public string Source { get; set; }

	// Token: 0x06004276 RID: 17014 RVA: 0x00101AD4 File Offset: 0x000FFCD4
	internal void LoadImage(dfAtlas atlas, string source)
	{
		dfAtlas.ItemInfo itemInfo = atlas[source];
		if (itemInfo == null)
		{
			throw new InvalidOperationException("Sprite does not exist in atlas: " + source);
		}
		this.Atlas = atlas;
		this.Source = source;
		this.Size = itemInfo.sizeInPixels;
		this.Baseline = (int)this.Size.y;
	}

	// Token: 0x06004277 RID: 17015 RVA: 0x00101B34 File Offset: 0x000FFD34
	protected override dfRenderData OnRebuildRenderData()
	{
		this.renderData.Clear();
		if (this.Atlas != null && this.Atlas[this.Source] != null)
		{
			dfSprite.RenderOptions options = new dfSprite.RenderOptions
			{
				atlas = this.Atlas,
				spriteInfo = this.Atlas[this.Source],
				pixelsToUnits = 1f,
				size = this.Size,
				color = this.Style.Color,
				fillAmount = 1f
			};
			dfSlicedSprite.renderSprite(this.renderData, options);
			this.renderData.Material = this.Atlas.Material;
			this.renderData.Transform = Matrix4x4.identity;
		}
		return this.renderData;
	}

	// Token: 0x06004278 RID: 17016 RVA: 0x00101C20 File Offset: 0x000FFE20
	private static void addTriangleIndices(dfList<Vector3> verts, dfList<int> triangles)
	{
		int count = verts.Count;
		int[] triangle_INDICES = dfMarkupBoxSprite.TRIANGLE_INDICES;
		for (int i = 0; i < triangle_INDICES.Length; i++)
		{
			triangles.Add(count + triangle_INDICES[i]);
		}
	}

	// Token: 0x04002303 RID: 8963
	private static int[] TRIANGLE_INDICES = new int[]
	{
		0,
		1,
		2,
		0,
		2,
		3
	};

	// Token: 0x04002304 RID: 8964
	private dfRenderData renderData = new dfRenderData(32);
}
