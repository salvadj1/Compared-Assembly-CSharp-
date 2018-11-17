using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

// Token: 0x020006C3 RID: 1731
[AddComponentMenu("Daikon Forge/User Interface/Font Definition")]
[Serializable]
public class dfFont : dfFontBase
{
	// Token: 0x17000BC5 RID: 3013
	// (get) Token: 0x06003CA2 RID: 15522 RVA: 0x000E4214 File Offset: 0x000E2414
	public List<dfFont.GlyphDefinition> Glyphs
	{
		get
		{
			return this.glyphs;
		}
	}

	// Token: 0x17000BC6 RID: 3014
	// (get) Token: 0x06003CA3 RID: 15523 RVA: 0x000E421C File Offset: 0x000E241C
	public List<dfFont.GlyphKerning> KerningInfo
	{
		get
		{
			return this.kerning;
		}
	}

	// Token: 0x17000BC7 RID: 3015
	// (get) Token: 0x06003CA4 RID: 15524 RVA: 0x000E4224 File Offset: 0x000E2424
	// (set) Token: 0x06003CA5 RID: 15525 RVA: 0x000E422C File Offset: 0x000E242C
	public dfAtlas Atlas
	{
		get
		{
			return this.atlas;
		}
		set
		{
			if (value != this.atlas)
			{
				this.atlas = value;
				this.glyphMap = null;
			}
		}
	}

	// Token: 0x17000BC8 RID: 3016
	// (get) Token: 0x06003CA6 RID: 15526 RVA: 0x000E4250 File Offset: 0x000E2450
	// (set) Token: 0x06003CA7 RID: 15527 RVA: 0x000E4260 File Offset: 0x000E2460
	public override Material Material
	{
		get
		{
			return this.Atlas.Material;
		}
		set
		{
			throw new InvalidOperationException();
		}
	}

	// Token: 0x17000BC9 RID: 3017
	// (get) Token: 0x06003CA8 RID: 15528 RVA: 0x000E4268 File Offset: 0x000E2468
	public override Texture Texture
	{
		get
		{
			return this.Atlas.Texture;
		}
	}

	// Token: 0x17000BCA RID: 3018
	// (get) Token: 0x06003CA9 RID: 15529 RVA: 0x000E4278 File Offset: 0x000E2478
	// (set) Token: 0x06003CAA RID: 15530 RVA: 0x000E4280 File Offset: 0x000E2480
	public string Sprite
	{
		get
		{
			return this.sprite;
		}
		set
		{
			if (value != this.sprite)
			{
				this.sprite = value;
				this.glyphMap = null;
			}
		}
	}

	// Token: 0x17000BCB RID: 3019
	// (get) Token: 0x06003CAB RID: 15531 RVA: 0x000E42A4 File Offset: 0x000E24A4
	public override bool IsValid
	{
		get
		{
			return !(this.Atlas == null) && !(this.Atlas[this.Sprite] == null);
		}
	}

	// Token: 0x17000BCC RID: 3020
	// (get) Token: 0x06003CAC RID: 15532 RVA: 0x000E42E4 File Offset: 0x000E24E4
	public string FontFace
	{
		get
		{
			return this.face;
		}
	}

	// Token: 0x17000BCD RID: 3021
	// (get) Token: 0x06003CAD RID: 15533 RVA: 0x000E42EC File Offset: 0x000E24EC
	// (set) Token: 0x06003CAE RID: 15534 RVA: 0x000E42F4 File Offset: 0x000E24F4
	public override int FontSize
	{
		get
		{
			return this.size;
		}
		set
		{
			throw new InvalidOperationException();
		}
	}

	// Token: 0x17000BCE RID: 3022
	// (get) Token: 0x06003CAF RID: 15535 RVA: 0x000E42FC File Offset: 0x000E24FC
	// (set) Token: 0x06003CB0 RID: 15536 RVA: 0x000E4304 File Offset: 0x000E2504
	public override int LineHeight
	{
		get
		{
			return this.lineHeight;
		}
		set
		{
			throw new InvalidOperationException();
		}
	}

	// Token: 0x17000BCF RID: 3023
	// (get) Token: 0x06003CB1 RID: 15537 RVA: 0x000E430C File Offset: 0x000E250C
	public bool Bold
	{
		get
		{
			return this.bold;
		}
	}

	// Token: 0x17000BD0 RID: 3024
	// (get) Token: 0x06003CB2 RID: 15538 RVA: 0x000E4314 File Offset: 0x000E2514
	public bool Italic
	{
		get
		{
			return this.italic;
		}
	}

	// Token: 0x17000BD1 RID: 3025
	// (get) Token: 0x06003CB3 RID: 15539 RVA: 0x000E431C File Offset: 0x000E251C
	public int[] Padding
	{
		get
		{
			return this.padding;
		}
	}

	// Token: 0x17000BD2 RID: 3026
	// (get) Token: 0x06003CB4 RID: 15540 RVA: 0x000E4324 File Offset: 0x000E2524
	public int[] Spacing
	{
		get
		{
			return this.spacing;
		}
	}

	// Token: 0x17000BD3 RID: 3027
	// (get) Token: 0x06003CB5 RID: 15541 RVA: 0x000E432C File Offset: 0x000E252C
	public int Outline
	{
		get
		{
			return this.outline;
		}
	}

	// Token: 0x17000BD4 RID: 3028
	// (get) Token: 0x06003CB6 RID: 15542 RVA: 0x000E4334 File Offset: 0x000E2534
	public int Count
	{
		get
		{
			return this.glyphs.Count;
		}
	}

	// Token: 0x06003CB7 RID: 15543 RVA: 0x000E4344 File Offset: 0x000E2544
	public void OnEnable()
	{
		this.glyphMap = null;
	}

	// Token: 0x06003CB8 RID: 15544 RVA: 0x000E4350 File Offset: 0x000E2550
	public override dfFontRendererBase ObtainRenderer()
	{
		return dfFont.BitmappedFontRenderer.Obtain(this);
	}

	// Token: 0x06003CB9 RID: 15545 RVA: 0x000E4358 File Offset: 0x000E2558
	public void AddKerning(int first, int second, int amount)
	{
		this.kerning.Add(new dfFont.GlyphKerning
		{
			first = first,
			second = second,
			amount = amount
		});
	}

	// Token: 0x06003CBA RID: 15546 RVA: 0x000E438C File Offset: 0x000E258C
	public int GetKerning(char previousChar, char currentChar)
	{
		int result;
		try
		{
			if (this.kerningMap == null)
			{
				this.buildKerningMap();
			}
			dfFont.GlyphKerningList glyphKerningList = null;
			if (!this.kerningMap.TryGetValue((int)previousChar, out glyphKerningList))
			{
				result = 0;
			}
			else
			{
				result = glyphKerningList.GetKerning((int)previousChar, (int)currentChar);
			}
		}
		finally
		{
		}
		return result;
	}

	// Token: 0x06003CBB RID: 15547 RVA: 0x000E43F8 File Offset: 0x000E25F8
	private void buildKerningMap()
	{
		Dictionary<int, dfFont.GlyphKerningList> dictionary = this.kerningMap = new Dictionary<int, dfFont.GlyphKerningList>();
		for (int i = 0; i < this.kerning.Count; i++)
		{
			dfFont.GlyphKerning glyphKerning = this.kerning[i];
			if (!dictionary.ContainsKey(glyphKerning.first))
			{
				dictionary[glyphKerning.first] = new dfFont.GlyphKerningList();
			}
			dfFont.GlyphKerningList glyphKerningList = dictionary[glyphKerning.first];
			glyphKerningList.Add(glyphKerning);
		}
	}

	// Token: 0x06003CBC RID: 15548 RVA: 0x000E4478 File Offset: 0x000E2678
	public dfFont.GlyphDefinition GetGlyph(char id)
	{
		if (this.glyphMap == null)
		{
			this.glyphMap = new Dictionary<int, dfFont.GlyphDefinition>();
			for (int i = 0; i < this.glyphs.Count; i++)
			{
				dfFont.GlyphDefinition glyphDefinition = this.glyphs[i];
				this.glyphMap[glyphDefinition.id] = glyphDefinition;
			}
		}
		dfFont.GlyphDefinition result = null;
		this.glyphMap.TryGetValue((int)id, out result);
		return result;
	}

	// Token: 0x04001FFA RID: 8186
	[SerializeField]
	protected dfAtlas atlas;

	// Token: 0x04001FFB RID: 8187
	[SerializeField]
	protected string sprite;

	// Token: 0x04001FFC RID: 8188
	[SerializeField]
	protected string face = string.Empty;

	// Token: 0x04001FFD RID: 8189
	[SerializeField]
	protected int size;

	// Token: 0x04001FFE RID: 8190
	[SerializeField]
	protected bool bold;

	// Token: 0x04001FFF RID: 8191
	[SerializeField]
	protected bool italic;

	// Token: 0x04002000 RID: 8192
	[SerializeField]
	protected string charset;

	// Token: 0x04002001 RID: 8193
	[SerializeField]
	protected int stretchH;

	// Token: 0x04002002 RID: 8194
	[SerializeField]
	protected bool smooth;

	// Token: 0x04002003 RID: 8195
	[SerializeField]
	protected int aa;

	// Token: 0x04002004 RID: 8196
	[SerializeField]
	protected int[] padding;

	// Token: 0x04002005 RID: 8197
	[SerializeField]
	protected int[] spacing;

	// Token: 0x04002006 RID: 8198
	[SerializeField]
	protected int outline;

	// Token: 0x04002007 RID: 8199
	[SerializeField]
	protected int lineHeight;

	// Token: 0x04002008 RID: 8200
	[SerializeField]
	private List<dfFont.GlyphDefinition> glyphs = new List<dfFont.GlyphDefinition>();

	// Token: 0x04002009 RID: 8201
	[SerializeField]
	protected List<dfFont.GlyphKerning> kerning = new List<dfFont.GlyphKerning>();

	// Token: 0x0400200A RID: 8202
	private Dictionary<int, dfFont.GlyphDefinition> glyphMap;

	// Token: 0x0400200B RID: 8203
	private Dictionary<int, dfFont.GlyphKerningList> kerningMap;

	// Token: 0x020006C4 RID: 1732
	private class GlyphKerningList
	{
		// Token: 0x06003CBE RID: 15550 RVA: 0x000E44FC File Offset: 0x000E26FC
		public void Add(dfFont.GlyphKerning kerning)
		{
			this.list[kerning.second] = kerning.amount;
		}

		// Token: 0x06003CBF RID: 15551 RVA: 0x000E4518 File Offset: 0x000E2718
		public int GetKerning(int firstCharacter, int secondCharacter)
		{
			int result = 0;
			this.list.TryGetValue(secondCharacter, out result);
			return result;
		}

		// Token: 0x0400200C RID: 8204
		private Dictionary<int, int> list = new Dictionary<int, int>();
	}

	// Token: 0x020006C5 RID: 1733
	[Serializable]
	public class GlyphKerning : IComparable<dfFont.GlyphKerning>
	{
		// Token: 0x06003CC1 RID: 15553 RVA: 0x000E4540 File Offset: 0x000E2740
		public int CompareTo(dfFont.GlyphKerning other)
		{
			if (this.first == other.first)
			{
				return this.second.CompareTo(other.second);
			}
			return this.first.CompareTo(other.first);
		}

		// Token: 0x0400200D RID: 8205
		public int first;

		// Token: 0x0400200E RID: 8206
		public int second;

		// Token: 0x0400200F RID: 8207
		public int amount;
	}

	// Token: 0x020006C6 RID: 1734
	[Serializable]
	public class GlyphDefinition : IComparable<dfFont.GlyphDefinition>
	{
		// Token: 0x06003CC3 RID: 15555 RVA: 0x000E458C File Offset: 0x000E278C
		public int CompareTo(dfFont.GlyphDefinition other)
		{
			return this.id.CompareTo(other.id);
		}

		// Token: 0x04002010 RID: 8208
		[SerializeField]
		public int id;

		// Token: 0x04002011 RID: 8209
		[SerializeField]
		public int x;

		// Token: 0x04002012 RID: 8210
		[SerializeField]
		public int y;

		// Token: 0x04002013 RID: 8211
		[SerializeField]
		public int width;

		// Token: 0x04002014 RID: 8212
		[SerializeField]
		public int height;

		// Token: 0x04002015 RID: 8213
		[SerializeField]
		public int xoffset;

		// Token: 0x04002016 RID: 8214
		[SerializeField]
		public int yoffset;

		// Token: 0x04002017 RID: 8215
		[SerializeField]
		public int xadvance;

		// Token: 0x04002018 RID: 8216
		[SerializeField]
		public bool rotated;
	}

	// Token: 0x020006C7 RID: 1735
	public class BitmappedFontRenderer : dfFontRendererBase
	{
		// Token: 0x06003CC4 RID: 15556 RVA: 0x000E45A0 File Offset: 0x000E27A0
		internal BitmappedFontRenderer()
		{
		}

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x06003CC6 RID: 15558 RVA: 0x000E4658 File Offset: 0x000E2858
		public int LineCount
		{
			get
			{
				return this.lines.Count;
			}
		}

		// Token: 0x06003CC7 RID: 15559 RVA: 0x000E4668 File Offset: 0x000E2868
		public static dfFontRendererBase Obtain(dfFont font)
		{
			dfFont.BitmappedFontRenderer bitmappedFontRenderer = (dfFont.BitmappedFontRenderer.objectPool.Count <= 0) ? new dfFont.BitmappedFontRenderer() : dfFont.BitmappedFontRenderer.objectPool.Dequeue();
			bitmappedFontRenderer.Reset();
			bitmappedFontRenderer.Font = font;
			return bitmappedFontRenderer;
		}

		// Token: 0x06003CC8 RID: 15560 RVA: 0x000E46A8 File Offset: 0x000E28A8
		public override void Release()
		{
			this.Reset();
			this.tokens = null;
			if (this.lines != null)
			{
				this.lines.Release();
				this.lines = null;
			}
			dfFont.LineRenderInfo.ResetPool();
			base.BottomColor = null;
			dfFont.BitmappedFontRenderer.objectPool.Enqueue(this);
		}

		// Token: 0x06003CC9 RID: 15561 RVA: 0x000E4700 File Offset: 0x000E2900
		public override float[] GetCharacterWidths(string text)
		{
			float num = 0f;
			return this.GetCharacterWidths(text, 0, text.Length - 1, out num);
		}

		// Token: 0x06003CCA RID: 15562 RVA: 0x000E4728 File Offset: 0x000E2928
		public float[] GetCharacterWidths(string text, int startIndex, int endIndex, out float totalWidth)
		{
			totalWidth = 0f;
			dfFont dfFont = (dfFont)base.Font;
			float[] array = new float[text.Length];
			float num = base.TextScale * base.PixelRatio;
			float num2 = (float)base.CharacterSpacing * num;
			for (int i = startIndex; i <= endIndex; i++)
			{
				dfFont.GlyphDefinition glyph = dfFont.GetGlyph(text[i]);
				if (glyph != null)
				{
					if (i > 0)
					{
						array[i - 1] += num2;
						totalWidth += num2;
					}
					float num3 = (float)glyph.xadvance * num;
					array[i] = num3;
					totalWidth += num3;
				}
			}
			return array;
		}

		// Token: 0x06003CCB RID: 15563 RVA: 0x000E47DC File Offset: 0x000E29DC
		public override Vector2 MeasureString(string text)
		{
			this.tokenize(text);
			dfList<dfFont.LineRenderInfo> dfList = this.calculateLinebreaks();
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < dfList.Count; i++)
			{
				num = Mathf.Max((int)dfList[i].lineWidth, num);
				num2 += (int)dfList[i].lineHeight;
			}
			return new Vector2((float)num, (float)num2) * base.TextScale;
		}

		// Token: 0x06003CCC RID: 15564 RVA: 0x000E484C File Offset: 0x000E2A4C
		public override void Render(string text, dfRenderData destination)
		{
			dfFont.BitmappedFontRenderer.textColors.Clear();
			dfFont.BitmappedFontRenderer.textColors.Push(Color.white);
			this.tokenize(text);
			dfList<dfFont.LineRenderInfo> dfList = this.calculateLinebreaks();
			int num = 0;
			int num2 = 0;
			Vector3 vectorOffset = base.VectorOffset;
			float num3 = base.TextScale * base.PixelRatio;
			for (int i = 0; i < dfList.Count; i++)
			{
				dfFont.LineRenderInfo lineRenderInfo = dfList[i];
				int count = destination.Vertices.Count;
				this.renderLine(dfList[i], dfFont.BitmappedFontRenderer.textColors, vectorOffset, destination);
				vectorOffset.y -= (float)base.Font.LineHeight * num3;
				num = Mathf.Max((int)lineRenderInfo.lineWidth, num);
				num2 += (int)lineRenderInfo.lineHeight;
				if (lineRenderInfo.lineWidth * base.TextScale > base.MaxSize.x)
				{
					this.clipRight(destination, count);
				}
				if ((float)num2 * base.TextScale > base.MaxSize.y)
				{
					this.clipBottom(destination, count);
				}
			}
			base.RenderedSize = new Vector2(Mathf.Min(base.MaxSize.x, (float)num), Mathf.Min(base.MaxSize.y, (float)num2)) * base.TextScale;
		}

		// Token: 0x06003CCD RID: 15565 RVA: 0x000E49B4 File Offset: 0x000E2BB4
		private void renderLine(dfFont.LineRenderInfo line, Stack<Color32> colors, Vector3 position, dfRenderData destination)
		{
			float num = base.TextScale * base.PixelRatio;
			position.x += (float)this.calculateLineAlignment(line) * num;
			for (int i = line.startOffset; i <= line.endOffset; i++)
			{
				dfMarkupToken dfMarkupToken = this.tokens[i];
				dfMarkupTokenType tokenType = dfMarkupToken.TokenType;
				if (tokenType == dfMarkupTokenType.Text)
				{
					this.renderText(dfMarkupToken, colors.Peek(), position, destination);
				}
				else if (tokenType == dfMarkupTokenType.StartTag)
				{
					if (dfMarkupToken.Matches("sprite"))
					{
						this.renderSprite(dfMarkupToken, colors.Peek(), position, destination);
					}
					else if (dfMarkupToken.Matches("color"))
					{
						colors.Push(this.parseColor(dfMarkupToken));
					}
				}
				else if (tokenType == dfMarkupTokenType.EndTag && dfMarkupToken.Matches("color") && colors.Count > 1)
				{
					colors.Pop();
				}
				position.x += (float)dfMarkupToken.Width * num;
			}
		}

		// Token: 0x06003CCE RID: 15566 RVA: 0x000E4AC4 File Offset: 0x000E2CC4
		private void renderText(dfMarkupToken token, Color32 color, Vector3 position, dfRenderData destination)
		{
			try
			{
				dfList<Vector3> vertices = destination.Vertices;
				dfList<int> triangles = destination.Triangles;
				dfList<Color32> colors = destination.Colors;
				dfList<Vector2> uv = destination.UV;
				dfFont dfFont = (dfFont)base.Font;
				dfAtlas.ItemInfo itemInfo = dfFont.Atlas[dfFont.sprite];
				Texture texture = dfFont.Texture;
				float num = 1f / (float)texture.width;
				float num2 = 1f / (float)texture.height;
				float num3 = num * 0.125f;
				float num4 = num2 * 0.125f;
				float num5 = base.TextScale * base.PixelRatio;
				char previousChar = '\0';
				Color32 color2 = this.applyOpacity(this.multiplyColors(color, base.DefaultColor));
				Color32 item = color2;
				if (base.BottomColor != null)
				{
					item = this.applyOpacity(this.multiplyColors(color, base.BottomColor.Value));
				}
				int i = 0;
				while (i < token.Length)
				{
					char c = token[i];
					if (c != '\0')
					{
						dfFont.GlyphDefinition glyph = dfFont.GetGlyph(c);
						if (glyph != null)
						{
							int kerning = dfFont.GetKerning(previousChar, c);
							float num6 = position.x + (float)(glyph.xoffset + kerning) * num5;
							float num7 = position.y - (float)glyph.yoffset * num5;
							float num8 = (float)glyph.width * num5;
							float num9 = (float)glyph.height * num5;
							float num10 = num6 + num8;
							float num11 = num7 - num9;
							Vector3 vector;
							vector..ctor(num6, num7);
							Vector3 vector2;
							vector2..ctor(num10, num7);
							Vector3 vector3;
							vector3..ctor(num10, num11);
							Vector3 vector4;
							vector4..ctor(num6, num11);
							float num12 = itemInfo.region.x + (float)glyph.x * num - num3;
							float num13 = itemInfo.region.yMax - (float)glyph.y * num2 - num4;
							float num14 = num12 + (float)glyph.width * num - num3;
							float num15 = num13 - (float)glyph.height * num2 + num4;
							if (base.Shadow)
							{
								dfFont.BitmappedFontRenderer.addTriangleIndices(vertices, triangles);
								Vector3 vector5 = base.ShadowOffset * num5;
								vertices.Add(vector + vector5);
								vertices.Add(vector2 + vector5);
								vertices.Add(vector3 + vector5);
								vertices.Add(vector4 + vector5);
								Color32 item2 = this.applyOpacity(base.ShadowColor);
								colors.Add(item2);
								colors.Add(item2);
								colors.Add(item2);
								colors.Add(item2);
								uv.Add(new Vector2(num12, num13));
								uv.Add(new Vector2(num14, num13));
								uv.Add(new Vector2(num14, num15));
								uv.Add(new Vector2(num12, num15));
							}
							if (base.Outline)
							{
								for (int j = 0; j < dfFont.BitmappedFontRenderer.OUTLINE_OFFSETS.Length; j++)
								{
									dfFont.BitmappedFontRenderer.addTriangleIndices(vertices, triangles);
									Vector3 vector6 = dfFont.BitmappedFontRenderer.OUTLINE_OFFSETS[j] * (float)base.OutlineSize * num5;
									vertices.Add(vector + vector6);
									vertices.Add(vector2 + vector6);
									vertices.Add(vector3 + vector6);
									vertices.Add(vector4 + vector6);
									Color32 item3 = this.applyOpacity(base.OutlineColor);
									colors.Add(item3);
									colors.Add(item3);
									colors.Add(item3);
									colors.Add(item3);
									uv.Add(new Vector2(num12, num13));
									uv.Add(new Vector2(num14, num13));
									uv.Add(new Vector2(num14, num15));
									uv.Add(new Vector2(num12, num15));
								}
							}
							dfFont.BitmappedFontRenderer.addTriangleIndices(vertices, triangles);
							vertices.Add(vector);
							vertices.Add(vector2);
							vertices.Add(vector3);
							vertices.Add(vector4);
							colors.Add(color2);
							colors.Add(color2);
							colors.Add(item);
							colors.Add(item);
							uv.Add(new Vector2(num12, num13));
							uv.Add(new Vector2(num14, num13));
							uv.Add(new Vector2(num14, num15));
							uv.Add(new Vector2(num12, num15));
							position.x += (float)(glyph.xadvance + kerning + base.CharacterSpacing) * num5;
						}
					}
					i++;
					previousChar = c;
				}
			}
			finally
			{
			}
		}

		// Token: 0x06003CCF RID: 15567 RVA: 0x000E4F9C File Offset: 0x000E319C
		private void renderSprite(dfMarkupToken token, Color32 color, Vector3 position, dfRenderData destination)
		{
			try
			{
				dfList<Vector3> vertices = destination.Vertices;
				dfList<int> triangles = destination.Triangles;
				dfList<Color32> colors = destination.Colors;
				dfList<Vector2> uv = destination.UV;
				dfFont dfFont = (dfFont)base.Font;
				string value = token.GetAttribute(0).Value.Value;
				dfAtlas.ItemInfo itemInfo = dfFont.Atlas[value];
				if (!(itemInfo == null))
				{
					float num = (float)token.Height * base.TextScale * base.PixelRatio;
					float num2 = (float)token.Width * base.TextScale * base.PixelRatio;
					float x = position.x;
					float y = position.y;
					int count = vertices.Count;
					vertices.Add(new Vector3(x, y));
					vertices.Add(new Vector3(x + num2, y));
					vertices.Add(new Vector3(x + num2, y - num));
					vertices.Add(new Vector3(x, y - num));
					triangles.Add(count);
					triangles.Add(count + 1);
					triangles.Add(count + 3);
					triangles.Add(count + 3);
					triangles.Add(count + 1);
					triangles.Add(count + 2);
					Color32 item = (!base.ColorizeSymbols) ? this.applyOpacity(base.DefaultColor) : this.applyOpacity(color);
					colors.Add(item);
					colors.Add(item);
					colors.Add(item);
					colors.Add(item);
					Rect region = itemInfo.region;
					uv.Add(new Vector2(region.x, region.yMax));
					uv.Add(new Vector2(region.xMax, region.yMax));
					uv.Add(new Vector2(region.xMax, region.y));
					uv.Add(new Vector2(region.x, region.y));
				}
			}
			finally
			{
			}
		}

		// Token: 0x06003CD0 RID: 15568 RVA: 0x000E51AC File Offset: 0x000E33AC
		private Color32 parseColor(dfMarkupToken token)
		{
			Color color = Color.white;
			if (token.AttributeCount == 1)
			{
				string value = token.GetAttribute(0).Value.Value;
				if (value.Length == 7 && value[0] == '#')
				{
					uint num = 0u;
					uint.TryParse(value.Substring(1), NumberStyles.HexNumber, null, out num);
					color = this.UIntToColor(num | 4278190080u);
				}
				else
				{
					color = dfMarkupStyle.ParseColor(value, base.DefaultColor);
				}
			}
			return this.applyOpacity(color);
		}

		// Token: 0x06003CD1 RID: 15569 RVA: 0x000E5244 File Offset: 0x000E3444
		private Color32 UIntToColor(uint color)
		{
			byte b = (byte)(color >> 24);
			byte b2 = (byte)(color >> 16);
			byte b3 = (byte)(color >> 8);
			byte b4 = (byte)color;
			return new Color32(b2, b3, b4, b);
		}

		// Token: 0x06003CD2 RID: 15570 RVA: 0x000E5270 File Offset: 0x000E3470
		private dfList<dfFont.LineRenderInfo> calculateLinebreaks()
		{
			dfList<dfFont.LineRenderInfo> result;
			try
			{
				if (this.lines != null)
				{
					result = this.lines;
				}
				else
				{
					this.lines = dfList<dfFont.LineRenderInfo>.Obtain();
					int num = 0;
					int num2 = 0;
					int num3 = 0;
					int num4 = 0;
					float num5 = (float)base.Font.LineHeight * base.TextScale;
					while (num3 < this.tokens.Count && (float)this.lines.Count * num5 < base.MaxSize.y)
					{
						dfMarkupToken dfMarkupToken = this.tokens[num3];
						dfMarkupTokenType tokenType = dfMarkupToken.TokenType;
						if (tokenType == dfMarkupTokenType.Newline)
						{
							this.lines.Add(dfFont.LineRenderInfo.Obtain(num2, num3));
							num = (num2 = ++num3);
							num4 = 0;
						}
						else
						{
							int num6 = Mathf.CeilToInt((float)dfMarkupToken.Width * base.TextScale);
							bool flag = base.WordWrap && num > num2 && (tokenType == dfMarkupTokenType.Text || (tokenType == dfMarkupTokenType.StartTag && dfMarkupToken.Matches("sprite")));
							if (flag && (float)(num4 + num6) >= base.MaxSize.x)
							{
								if (num > num2)
								{
									this.lines.Add(dfFont.LineRenderInfo.Obtain(num2, num - 1));
									num3 = (num2 = ++num);
									num4 = 0;
								}
								else
								{
									this.lines.Add(dfFont.LineRenderInfo.Obtain(num2, num - 1));
									num = (num2 = ++num3);
									num4 = 0;
								}
							}
							else
							{
								if (tokenType == dfMarkupTokenType.Whitespace)
								{
									num = num3;
								}
								num4 += num6;
								num3++;
							}
						}
					}
					if (num2 < this.tokens.Count)
					{
						this.lines.Add(dfFont.LineRenderInfo.Obtain(num2, this.tokens.Count - 1));
					}
					for (int i = 0; i < this.lines.Count; i++)
					{
						this.calculateLineSize(this.lines[i]);
					}
					result = this.lines;
				}
			}
			finally
			{
			}
			return result;
		}

		// Token: 0x06003CD3 RID: 15571 RVA: 0x000E5498 File Offset: 0x000E3698
		private int calculateLineAlignment(dfFont.LineRenderInfo line)
		{
			float lineWidth = line.lineWidth;
			if (base.TextAlign == null || lineWidth == 0f)
			{
				return 0;
			}
			int num;
			if (base.TextAlign == 2)
			{
				num = Mathf.FloorToInt(base.MaxSize.x / base.TextScale - lineWidth);
			}
			else
			{
				num = Mathf.FloorToInt((base.MaxSize.x / base.TextScale - lineWidth) * 0.5f);
			}
			return Mathf.Max(0, num);
		}

		// Token: 0x06003CD4 RID: 15572 RVA: 0x000E5520 File Offset: 0x000E3720
		private void calculateLineSize(dfFont.LineRenderInfo line)
		{
			line.lineHeight = (float)base.Font.LineHeight;
			int num = 0;
			for (int i = line.startOffset; i <= line.endOffset; i++)
			{
				num += this.tokens[i].Width;
			}
			line.lineWidth = (float)num;
		}

		// Token: 0x06003CD5 RID: 15573 RVA: 0x000E557C File Offset: 0x000E377C
		private List<dfMarkupToken> tokenize(string text)
		{
			List<dfMarkupToken> result;
			try
			{
				if (this.tokens != null && this.tokens.Count > 0 && this.tokens[0].Source == text)
				{
					result = this.tokens;
				}
				else
				{
					if (base.ProcessMarkup)
					{
						this.tokens = dfMarkupTokenizer.Tokenize(text);
					}
					else
					{
						this.tokens = dfPlainTextTokenizer.Tokenize(text);
					}
					for (int i = 0; i < this.tokens.Count; i++)
					{
						this.calculateTokenRenderSize(this.tokens[i]);
					}
					result = this.tokens;
				}
			}
			finally
			{
			}
			return result;
		}

		// Token: 0x06003CD6 RID: 15574 RVA: 0x000E5654 File Offset: 0x000E3854
		private void calculateTokenRenderSize(dfMarkupToken token)
		{
			try
			{
				dfFont dfFont = (dfFont)base.Font;
				int num = 0;
				char previousChar = '\0';
				bool flag = token.TokenType == dfMarkupTokenType.Whitespace || token.TokenType == dfMarkupTokenType.Text;
				if (flag)
				{
					int i = 0;
					while (i < token.Length)
					{
						char c = token[i];
						if (c == '\t')
						{
							num += base.TabSize;
						}
						else
						{
							dfFont.GlyphDefinition glyph = dfFont.GetGlyph(c);
							if (glyph != null)
							{
								if (i > 0)
								{
									num += dfFont.GetKerning(previousChar, c);
									num += base.CharacterSpacing;
								}
								num += glyph.xadvance;
							}
						}
						i++;
						previousChar = c;
					}
				}
				else if (token.TokenType == dfMarkupTokenType.StartTag && token.Matches("sprite"))
				{
					if (token.AttributeCount < 1)
					{
						throw new Exception("Missing sprite name in markup");
					}
					Texture texture = dfFont.Texture;
					int lineHeight = dfFont.LineHeight;
					string value = token.GetAttribute(0).Value.Value;
					dfAtlas.ItemInfo itemInfo = dfFont.atlas[value];
					if (itemInfo != null)
					{
						float num2 = itemInfo.region.width * (float)texture.width / (itemInfo.region.height * (float)texture.height);
						num = Mathf.CeilToInt((float)lineHeight * num2);
					}
				}
				token.Height = base.Font.LineHeight;
				token.Width = num;
			}
			finally
			{
			}
		}

		// Token: 0x06003CD7 RID: 15575 RVA: 0x000E57F8 File Offset: 0x000E39F8
		private float getTabStop(float position)
		{
			float num = base.PixelRatio * base.TextScale;
			if (base.TabStops != null && base.TabStops.Count > 0)
			{
				for (int i = 0; i < base.TabStops.Count; i++)
				{
					if ((float)base.TabStops[i] * num > position)
					{
						return (float)base.TabStops[i] * num;
					}
				}
			}
			if (base.TabSize > 0)
			{
				return position + (float)base.TabSize * num;
			}
			return position + (float)(base.Font.FontSize * 4) * num;
		}

		// Token: 0x06003CD8 RID: 15576 RVA: 0x000E589C File Offset: 0x000E3A9C
		private void clipRight(dfRenderData destination, int startIndex)
		{
			float num = base.VectorOffset.x + base.MaxSize.x * base.PixelRatio;
			dfList<Vector3> vertices = destination.Vertices;
			dfList<Vector2> uv = destination.UV;
			for (int i = startIndex; i < vertices.Count; i += 4)
			{
				Vector3 value = vertices[i];
				Vector3 value2 = vertices[i + 1];
				Vector3 value3 = vertices[i + 2];
				Vector3 value4 = vertices[i + 3];
				float num2 = value2.x - value.x;
				if (value2.x > num)
				{
					float num3 = 1f - (num - value2.x + num2) / num2;
					dfList<Vector3> dfList = vertices;
					int index = i;
					value..ctor(Mathf.Min(value.x, num), value.y, value.z);
					dfList[index] = value;
					dfList<Vector3> dfList2 = vertices;
					int index2 = i + 1;
					value2..ctor(Mathf.Min(value2.x, num), value2.y, value2.z);
					dfList2[index2] = value2;
					dfList<Vector3> dfList3 = vertices;
					int index3 = i + 2;
					value3..ctor(Mathf.Min(value3.x, num), value3.y, value3.z);
					dfList3[index3] = value3;
					dfList<Vector3> dfList4 = vertices;
					int index4 = i + 3;
					value4..ctor(Mathf.Min(value4.x, num), value4.y, value4.z);
					dfList4[index4] = value4;
					float num4 = Mathf.Lerp(uv[i + 1].x, uv[i].x, num3);
					uv[i + 1] = new Vector2(num4, uv[i + 1].y);
					uv[i + 2] = new Vector2(num4, uv[i + 2].y);
					num2 = value2.x - value.x;
				}
			}
		}

		// Token: 0x06003CD9 RID: 15577 RVA: 0x000E5A88 File Offset: 0x000E3C88
		private void clipBottom(dfRenderData destination, int startIndex)
		{
			float num = base.VectorOffset.y - base.MaxSize.y * base.PixelRatio;
			dfList<Vector3> vertices = destination.Vertices;
			dfList<Vector2> uv = destination.UV;
			dfList<Color32> colors = destination.Colors;
			for (int i = startIndex; i < vertices.Count; i += 4)
			{
				Vector3 value = vertices[i];
				Vector3 value2 = vertices[i + 1];
				Vector3 value3 = vertices[i + 2];
				Vector3 value4 = vertices[i + 3];
				float num2 = value.y - value4.y;
				if (value4.y <= num)
				{
					float num3 = 1f - Mathf.Abs(-num + value.y) / num2;
					dfList<Vector3> dfList = vertices;
					int index = i;
					value..ctor(value.x, Mathf.Max(value.y, num), value2.z);
					dfList[index] = value;
					dfList<Vector3> dfList2 = vertices;
					int index2 = i + 1;
					value2..ctor(value2.x, Mathf.Max(value2.y, num), value2.z);
					dfList2[index2] = value2;
					dfList<Vector3> dfList3 = vertices;
					int index3 = i + 2;
					value3..ctor(value3.x, Mathf.Max(value3.y, num), value3.z);
					dfList3[index3] = value3;
					dfList<Vector3> dfList4 = vertices;
					int index4 = i + 3;
					value4..ctor(value4.x, Mathf.Max(value4.y, num), value4.z);
					dfList4[index4] = value4;
					float num4 = Mathf.Lerp(uv[i + 3].y, uv[i].y, num3);
					uv[i + 3] = new Vector2(uv[i + 3].x, num4);
					uv[i + 2] = new Vector2(uv[i + 2].x, num4);
					Color color = Color.Lerp(colors[i + 3], colors[i], num3);
					colors[i + 3] = color;
					colors[i + 2] = color;
				}
			}
		}

		// Token: 0x06003CDA RID: 15578 RVA: 0x000E5CC4 File Offset: 0x000E3EC4
		private Color32 applyOpacity(Color32 color)
		{
			color.a = (byte)(base.Opacity * 255f);
			return color;
		}

		// Token: 0x06003CDB RID: 15579 RVA: 0x000E5CDC File Offset: 0x000E3EDC
		private static void addTriangleIndices(dfList<Vector3> verts, dfList<int> triangles)
		{
			int count = verts.Count;
			int[] triangle_INDICES = dfFont.BitmappedFontRenderer.TRIANGLE_INDICES;
			for (int i = 0; i < triangle_INDICES.Length; i++)
			{
				triangles.Add(count + triangle_INDICES[i]);
			}
		}

		// Token: 0x06003CDC RID: 15580 RVA: 0x000E5D18 File Offset: 0x000E3F18
		private Color multiplyColors(Color lhs, Color rhs)
		{
			return new Color(lhs.r * rhs.r, lhs.g * rhs.g, lhs.b * rhs.b, lhs.a * rhs.a);
		}

		// Token: 0x06003CDD RID: 15581 RVA: 0x000E5D68 File Offset: 0x000E3F68
		private dfFont.LineRenderInfo fitSingleLine()
		{
			return dfFont.LineRenderInfo.Obtain(0, 0);
		}

		// Token: 0x04002019 RID: 8217
		private static Queue<dfFont.BitmappedFontRenderer> objectPool = new Queue<dfFont.BitmappedFontRenderer>();

		// Token: 0x0400201A RID: 8218
		private static Vector2[] OUTLINE_OFFSETS = new Vector2[]
		{
			new Vector2(-1f, -1f),
			new Vector2(-1f, 1f),
			new Vector2(1f, -1f),
			new Vector2(1f, 1f)
		};

		// Token: 0x0400201B RID: 8219
		private static int[] TRIANGLE_INDICES = new int[]
		{
			0,
			1,
			3,
			3,
			1,
			2
		};

		// Token: 0x0400201C RID: 8220
		private static Stack<Color32> textColors = new Stack<Color32>();

		// Token: 0x0400201D RID: 8221
		private dfList<dfFont.LineRenderInfo> lines;

		// Token: 0x0400201E RID: 8222
		private List<dfMarkupToken> tokens;
	}

	// Token: 0x020006C8 RID: 1736
	private class LineRenderInfo
	{
		// Token: 0x06003CDE RID: 15582 RVA: 0x000E5D80 File Offset: 0x000E3F80
		private LineRenderInfo()
		{
		}

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x06003CE0 RID: 15584 RVA: 0x000E5D9C File Offset: 0x000E3F9C
		public int length
		{
			get
			{
				return this.endOffset - this.startOffset + 1;
			}
		}

		// Token: 0x06003CE1 RID: 15585 RVA: 0x000E5DB0 File Offset: 0x000E3FB0
		public static void ResetPool()
		{
			dfFont.LineRenderInfo.poolIndex = 0;
		}

		// Token: 0x06003CE2 RID: 15586 RVA: 0x000E5DB8 File Offset: 0x000E3FB8
		public static dfFont.LineRenderInfo Obtain(int start, int end)
		{
			if (dfFont.LineRenderInfo.poolIndex >= dfFont.LineRenderInfo.pool.Count - 1)
			{
				dfFont.LineRenderInfo.pool.Add(new dfFont.LineRenderInfo());
			}
			dfFont.LineRenderInfo lineRenderInfo = dfFont.LineRenderInfo.pool[dfFont.LineRenderInfo.poolIndex++];
			lineRenderInfo.startOffset = start;
			lineRenderInfo.endOffset = end;
			lineRenderInfo.lineHeight = 0f;
			return lineRenderInfo;
		}

		// Token: 0x0400201F RID: 8223
		public int startOffset;

		// Token: 0x04002020 RID: 8224
		public int endOffset;

		// Token: 0x04002021 RID: 8225
		public float lineWidth;

		// Token: 0x04002022 RID: 8226
		public float lineHeight;

		// Token: 0x04002023 RID: 8227
		private static dfList<dfFont.LineRenderInfo> pool = new dfList<dfFont.LineRenderInfo>();

		// Token: 0x04002024 RID: 8228
		private static int poolIndex = 0;
	}
}
