using System;
using UnityEngine;

// Token: 0x02000700 RID: 1792
[AddComponentMenu("Daikon Forge/User Interface/Sprite/Tiled")]
[ExecuteInEditMode]
[Serializable]
public class dfTiledSprite : dfSprite
{
	// Token: 0x17000CE0 RID: 3296
	// (get) Token: 0x0600415D RID: 16733 RVA: 0x000FBE68 File Offset: 0x000FA068
	// (set) Token: 0x0600415E RID: 16734 RVA: 0x000FBE70 File Offset: 0x000FA070
	public Vector2 TileScale
	{
		get
		{
			return this.tileScale;
		}
		set
		{
			if (Vector2.Distance(value, this.tileScale) > 1.401298E-45f)
			{
				this.tileScale = Vector2.Max(Vector2.one * 0.1f, value);
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CE1 RID: 3297
	// (get) Token: 0x0600415F RID: 16735 RVA: 0x000FBEAC File Offset: 0x000FA0AC
	// (set) Token: 0x06004160 RID: 16736 RVA: 0x000FBEB4 File Offset: 0x000FA0B4
	public Vector2 TileScroll
	{
		get
		{
			return this.tileScroll;
		}
		set
		{
			if (Vector2.Distance(value, this.tileScroll) > 1.401298E-45f)
			{
				this.tileScroll = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x06004161 RID: 16737 RVA: 0x000FBEDC File Offset: 0x000FA0DC
	protected override void OnRebuildRenderData()
	{
		if (base.Atlas == null)
		{
			return;
		}
		dfAtlas.ItemInfo spriteInfo = base.SpriteInfo;
		if (spriteInfo == null)
		{
			return;
		}
		this.renderData.Material = base.Atlas.Material;
		dfList<Vector3> vertices = this.renderData.Vertices;
		dfList<Vector2> uv = this.renderData.UV;
		dfList<Color32> colors = this.renderData.Colors;
		dfList<int> triangles = this.renderData.Triangles;
		Vector2[] spriteUV = this.buildQuadUV();
		Vector2 vector = Vector2.Scale(spriteInfo.sizeInPixels, this.tileScale);
		Vector2 vector2;
		vector2..ctor(this.tileScroll.x % 1f, this.tileScroll.y % 1f);
		for (float num = -Mathf.Abs(vector2.y * vector.y); num < this.size.y; num += vector.y)
		{
			for (float num2 = -Mathf.Abs(vector2.x * vector.x); num2 < this.size.x; num2 += vector.x)
			{
				int count = vertices.Count;
				vertices.Add(new Vector3(num2, -num));
				vertices.Add(new Vector3(num2 + vector.x, -num));
				vertices.Add(new Vector3(num2 + vector.x, -num + -vector.y));
				vertices.Add(new Vector3(num2, -num + -vector.y));
				this.addQuadTriangles(triangles, count);
				this.addQuadUV(uv, spriteUV);
				this.addQuadColors(colors);
			}
		}
		this.clipQuads(vertices, uv);
		float num3 = base.PixelsToUnits();
		Vector3 vector3 = this.pivot.TransformToUpperLeft(this.size);
		for (int i = 0; i < vertices.Count; i++)
		{
			vertices[i] = (vertices[i] + vector3) * num3;
		}
	}

	// Token: 0x06004162 RID: 16738 RVA: 0x000FC0F0 File Offset: 0x000FA2F0
	private void clipQuads(dfList<Vector3> verts, dfList<Vector2> uv)
	{
		float num = 0f;
		float num2 = this.size.x;
		float num3 = -this.size.y;
		float num4 = 0f;
		if (this.fillAmount < 1f)
		{
			if (this.fillDirection == dfFillDirection.Horizontal)
			{
				if (!this.invertFill)
				{
					num2 = this.size.x * this.fillAmount;
				}
				else
				{
					num = this.size.x - this.size.x * this.fillAmount;
				}
			}
			else if (!this.invertFill)
			{
				num3 = -this.size.y * this.fillAmount;
			}
			else
			{
				num4 = -this.size.y * (1f - this.fillAmount);
			}
		}
		for (int i = 0; i < verts.Count; i += 4)
		{
			Vector3 value = verts[i];
			Vector3 value2 = verts[i + 1];
			Vector3 value3 = verts[i + 2];
			Vector3 value4 = verts[i + 3];
			float num5 = value2.x - value.x;
			float num6 = value.y - value4.y;
			if (value.x < num)
			{
				float num7 = (num - value.x) / num5;
				int index = i;
				value..ctor(Mathf.Max(num, value.x), value.y, value.z);
				verts[index] = value;
				int index2 = i + 1;
				value2..ctor(Mathf.Max(num, value2.x), value2.y, value2.z);
				verts[index2] = value2;
				int index3 = i + 2;
				value3..ctor(Mathf.Max(num, value3.x), value3.y, value3.z);
				verts[index3] = value3;
				int index4 = i + 3;
				value4..ctor(Mathf.Max(num, value4.x), value4.y, value4.z);
				verts[index4] = value4;
				float num8 = Mathf.Lerp(uv[i].x, uv[i + 1].x, num7);
				uv[i] = new Vector2(num8, uv[i].y);
				uv[i + 3] = new Vector2(num8, uv[i + 3].y);
				num5 = value2.x - value.x;
			}
			if (value2.x > num2)
			{
				float num9 = 1f - (num2 - value2.x + num5) / num5;
				int index5 = i;
				value..ctor(Mathf.Min(value.x, num2), value.y, value.z);
				verts[index5] = value;
				int index6 = i + 1;
				value2..ctor(Mathf.Min(value2.x, num2), value2.y, value2.z);
				verts[index6] = value2;
				int index7 = i + 2;
				value3..ctor(Mathf.Min(value3.x, num2), value3.y, value3.z);
				verts[index7] = value3;
				int index8 = i + 3;
				value4..ctor(Mathf.Min(value4.x, num2), value4.y, value4.z);
				verts[index8] = value4;
				float num10 = Mathf.Lerp(uv[i + 1].x, uv[i].x, num9);
				uv[i + 1] = new Vector2(num10, uv[i + 1].y);
				uv[i + 2] = new Vector2(num10, uv[i + 2].y);
				num5 = value2.x - value.x;
			}
			if (value4.y < num3)
			{
				float num11 = 1f - Mathf.Abs(-num3 + value.y) / num6;
				int index9 = i;
				value..ctor(value.x, Mathf.Max(value.y, num3), value2.z);
				verts[index9] = value;
				int index10 = i + 1;
				value2..ctor(value2.x, Mathf.Max(value2.y, num3), value2.z);
				verts[index10] = value2;
				int index11 = i + 2;
				value3..ctor(value3.x, Mathf.Max(value3.y, num3), value3.z);
				verts[index11] = value3;
				int index12 = i + 3;
				value4..ctor(value4.x, Mathf.Max(value4.y, num3), value4.z);
				verts[index12] = value4;
				float num12 = Mathf.Lerp(uv[i + 3].y, uv[i].y, num11);
				uv[i + 3] = new Vector2(uv[i + 3].x, num12);
				uv[i + 2] = new Vector2(uv[i + 2].x, num12);
				num6 = Mathf.Abs(value4.y - value.y);
			}
			if (value.y > num4)
			{
				float num13 = Mathf.Abs(num4 - value.y) / num6;
				int index13 = i;
				value..ctor(value.x, Mathf.Min(num4, value.y), value.z);
				verts[index13] = value;
				int index14 = i + 1;
				value2..ctor(value2.x, Mathf.Min(num4, value2.y), value2.z);
				verts[index14] = value2;
				int index15 = i + 2;
				value3..ctor(value3.x, Mathf.Min(num4, value3.y), value3.z);
				verts[index15] = value3;
				int index16 = i + 3;
				value4..ctor(value4.x, Mathf.Min(num4, value4.y), value4.z);
				verts[index16] = value4;
				float num14 = Mathf.Lerp(uv[i].y, uv[i + 3].y, num13);
				uv[i] = new Vector2(uv[i].x, num14);
				uv[i + 1] = new Vector2(uv[i + 1].x, num14);
			}
		}
	}

	// Token: 0x06004163 RID: 16739 RVA: 0x000FC790 File Offset: 0x000FA990
	private void addQuadTriangles(dfList<int> triangles, int baseIndex)
	{
		for (int i = 0; i < dfTiledSprite.quadTriangles.Length; i++)
		{
			triangles.Add(dfTiledSprite.quadTriangles[i] + baseIndex);
		}
	}

	// Token: 0x06004164 RID: 16740 RVA: 0x000FC7C4 File Offset: 0x000FA9C4
	private void addQuadColors(dfList<Color32> colors)
	{
		colors.EnsureCapacity(colors.Count + 4);
		Color32 item = base.ApplyOpacity((!base.IsEnabled) ? this.disabledColor : this.color);
		for (int i = 0; i < 4; i++)
		{
			colors.Add(item);
		}
	}

	// Token: 0x06004165 RID: 16741 RVA: 0x000FC81C File Offset: 0x000FAA1C
	private void addQuadUV(dfList<Vector2> uv, Vector2[] spriteUV)
	{
		uv.AddRange(spriteUV);
	}

	// Token: 0x06004166 RID: 16742 RVA: 0x000FC828 File Offset: 0x000FAA28
	private Vector2[] buildQuadUV()
	{
		dfAtlas.ItemInfo spriteInfo = base.SpriteInfo;
		Rect region = spriteInfo.region;
		dfTiledSprite.quadUV[0] = new Vector2(region.x, region.yMax);
		dfTiledSprite.quadUV[1] = new Vector2(region.xMax, region.yMax);
		dfTiledSprite.quadUV[2] = new Vector2(region.xMax, region.y);
		dfTiledSprite.quadUV[3] = new Vector2(region.x, region.y);
		Vector2 vector = Vector2.zero;
		if (this.flip.IsSet(dfSpriteFlip.FlipHorizontal))
		{
			vector = dfTiledSprite.quadUV[1];
			dfTiledSprite.quadUV[1] = dfTiledSprite.quadUV[0];
			dfTiledSprite.quadUV[0] = vector;
			vector = dfTiledSprite.quadUV[3];
			dfTiledSprite.quadUV[3] = dfTiledSprite.quadUV[2];
			dfTiledSprite.quadUV[2] = vector;
		}
		if (this.flip.IsSet(dfSpriteFlip.FlipVertical))
		{
			vector = dfTiledSprite.quadUV[0];
			dfTiledSprite.quadUV[0] = dfTiledSprite.quadUV[3];
			dfTiledSprite.quadUV[3] = vector;
			vector = dfTiledSprite.quadUV[1];
			dfTiledSprite.quadUV[1] = dfTiledSprite.quadUV[2];
			dfTiledSprite.quadUV[2] = vector;
		}
		return dfTiledSprite.quadUV;
	}

	// Token: 0x04002285 RID: 8837
	private static int[] quadTriangles = new int[]
	{
		0,
		1,
		3,
		3,
		1,
		2
	};

	// Token: 0x04002286 RID: 8838
	private static Vector2[] quadUV = new Vector2[4];

	// Token: 0x04002287 RID: 8839
	[SerializeField]
	protected Vector2 tileScale = Vector2.one;

	// Token: 0x04002288 RID: 8840
	[SerializeField]
	protected Vector2 tileScroll = Vector2.zero;
}
