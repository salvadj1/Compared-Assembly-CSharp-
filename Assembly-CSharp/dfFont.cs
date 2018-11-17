using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

// Token: 0x0200078E RID: 1934
[AddComponentMenu("Daikon Forge/User Interface/Font Definition")]
[Serializable]
public class dfFont : global::dfFontBase
{
	// Token: 0x17000C49 RID: 3145
	// (get) Token: 0x060040AC RID: 16556 RVA: 0x000ECD58 File Offset: 0x000EAF58
	public List<global::dfFont.GlyphDefinition> Glyphs
	{
		get
		{
			return this.glyphs;
		}
	}

	// Token: 0x17000C4A RID: 3146
	// (get) Token: 0x060040AD RID: 16557 RVA: 0x000ECD60 File Offset: 0x000EAF60
	public List<global::dfFont.GlyphKerning> KerningInfo
	{
		get
		{
			return this.kerning;
		}
	}

	// Token: 0x17000C4B RID: 3147
	// (get) Token: 0x060040AE RID: 16558 RVA: 0x000ECD68 File Offset: 0x000EAF68
	// (set) Token: 0x060040AF RID: 16559 RVA: 0x000ECD70 File Offset: 0x000EAF70
	public global::dfAtlas Atlas
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

	// Token: 0x17000C4C RID: 3148
	// (get) Token: 0x060040B0 RID: 16560 RVA: 0x000ECD94 File Offset: 0x000EAF94
	// (set) Token: 0x060040B1 RID: 16561 RVA: 0x000ECDA4 File Offset: 0x000EAFA4
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

	// Token: 0x17000C4D RID: 3149
	// (get) Token: 0x060040B2 RID: 16562 RVA: 0x000ECDAC File Offset: 0x000EAFAC
	public override Texture Texture
	{
		get
		{
			return this.Atlas.Texture;
		}
	}

	// Token: 0x17000C4E RID: 3150
	// (get) Token: 0x060040B3 RID: 16563 RVA: 0x000ECDBC File Offset: 0x000EAFBC
	// (set) Token: 0x060040B4 RID: 16564 RVA: 0x000ECDC4 File Offset: 0x000EAFC4
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

	// Token: 0x17000C4F RID: 3151
	// (get) Token: 0x060040B5 RID: 16565 RVA: 0x000ECDE8 File Offset: 0x000EAFE8
	public override bool IsValid
	{
		get
		{
			return !(this.Atlas == null) && !(this.Atlas[this.Sprite] == null);
		}
	}

	// Token: 0x17000C50 RID: 3152
	// (get) Token: 0x060040B6 RID: 16566 RVA: 0x000ECE28 File Offset: 0x000EB028
	public string FontFace
	{
		get
		{
			return this.face;
		}
	}

	// Token: 0x17000C51 RID: 3153
	// (get) Token: 0x060040B7 RID: 16567 RVA: 0x000ECE30 File Offset: 0x000EB030
	// (set) Token: 0x060040B8 RID: 16568 RVA: 0x000ECE38 File Offset: 0x000EB038
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

	// Token: 0x17000C52 RID: 3154
	// (get) Token: 0x060040B9 RID: 16569 RVA: 0x000ECE40 File Offset: 0x000EB040
	// (set) Token: 0x060040BA RID: 16570 RVA: 0x000ECE48 File Offset: 0x000EB048
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

	// Token: 0x17000C53 RID: 3155
	// (get) Token: 0x060040BB RID: 16571 RVA: 0x000ECE50 File Offset: 0x000EB050
	public bool Bold
	{
		get
		{
			return this.bold;
		}
	}

	// Token: 0x17000C54 RID: 3156
	// (get) Token: 0x060040BC RID: 16572 RVA: 0x000ECE58 File Offset: 0x000EB058
	public bool Italic
	{
		get
		{
			return this.italic;
		}
	}

	// Token: 0x17000C55 RID: 3157
	// (get) Token: 0x060040BD RID: 16573 RVA: 0x000ECE60 File Offset: 0x000EB060
	public int[] Padding
	{
		get
		{
			return this.padding;
		}
	}

	// Token: 0x17000C56 RID: 3158
	// (get) Token: 0x060040BE RID: 16574 RVA: 0x000ECE68 File Offset: 0x000EB068
	public int[] Spacing
	{
		get
		{
			return this.spacing;
		}
	}

	// Token: 0x17000C57 RID: 3159
	// (get) Token: 0x060040BF RID: 16575 RVA: 0x000ECE70 File Offset: 0x000EB070
	public int Outline
	{
		get
		{
			return this.outline;
		}
	}

	// Token: 0x17000C58 RID: 3160
	// (get) Token: 0x060040C0 RID: 16576 RVA: 0x000ECE78 File Offset: 0x000EB078
	public int Count
	{
		get
		{
			return this.glyphs.Count;
		}
	}

	// Token: 0x060040C1 RID: 16577 RVA: 0x000ECE88 File Offset: 0x000EB088
	public void OnEnable()
	{
		this.glyphMap = null;
	}

	// Token: 0x060040C2 RID: 16578 RVA: 0x000ECE94 File Offset: 0x000EB094
	public override global::dfFontRendererBase ObtainRenderer()
	{
		return global::dfFont.BitmappedFontRenderer.Obtain(this);
	}

	// Token: 0x060040C3 RID: 16579 RVA: 0x000ECE9C File Offset: 0x000EB09C
	public void AddKerning(int first, int second, int amount)
	{
		this.kerning.Add(new global::dfFont.GlyphKerning
		{
			first = first,
			second = second,
			amount = amount
		});
	}

	// Token: 0x060040C4 RID: 16580 RVA: 0x000ECED0 File Offset: 0x000EB0D0
	public int GetKerning(char previousChar, char currentChar)
	{
		int result;
		try
		{
			if (this.kerningMap == null)
			{
				this.buildKerningMap();
			}
			global::dfFont.GlyphKerningList glyphKerningList = null;
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

	// Token: 0x060040C5 RID: 16581 RVA: 0x000ECF3C File Offset: 0x000EB13C
	private void buildKerningMap()
	{
		Dictionary<int, global::dfFont.GlyphKerningList> dictionary = this.kerningMap = new Dictionary<int, global::dfFont.GlyphKerningList>();
		for (int i = 0; i < this.kerning.Count; i++)
		{
			global::dfFont.GlyphKerning glyphKerning = this.kerning[i];
			if (!dictionary.ContainsKey(glyphKerning.first))
			{
				dictionary[glyphKerning.first] = new global::dfFont.GlyphKerningList();
			}
			global::dfFont.GlyphKerningList glyphKerningList = dictionary[glyphKerning.first];
			glyphKerningList.Add(glyphKerning);
		}
	}

	// Token: 0x060040C6 RID: 16582 RVA: 0x000ECFBC File Offset: 0x000EB1BC
	public global::dfFont.GlyphDefinition GetGlyph(char id)
	{
		if (this.glyphMap == null)
		{
			this.glyphMap = new Dictionary<int, global::dfFont.GlyphDefinition>();
			for (int i = 0; i < this.glyphs.Count; i++)
			{
				global::dfFont.GlyphDefinition glyphDefinition = this.glyphs[i];
				this.glyphMap[glyphDefinition.id] = glyphDefinition;
			}
		}
		global::dfFont.GlyphDefinition result = null;
		this.glyphMap.TryGetValue((int)id, out result);
		return result;
	}

	// Token: 0x040021FB RID: 8699
	[SerializeField]
	protected global::dfAtlas atlas;

	// Token: 0x040021FC RID: 8700
	[SerializeField]
	protected string sprite;

	// Token: 0x040021FD RID: 8701
	[SerializeField]
	protected string face = string.Empty;

	// Token: 0x040021FE RID: 8702
	[SerializeField]
	protected int size;

	// Token: 0x040021FF RID: 8703
	[SerializeField]
	protected bool bold;

	// Token: 0x04002200 RID: 8704
	[SerializeField]
	protected bool italic;

	// Token: 0x04002201 RID: 8705
	[SerializeField]
	protected string charset;

	// Token: 0x04002202 RID: 8706
	[SerializeField]
	protected int stretchH;

	// Token: 0x04002203 RID: 8707
	[SerializeField]
	protected bool smooth;

	// Token: 0x04002204 RID: 8708
	[SerializeField]
	protected int aa;

	// Token: 0x04002205 RID: 8709
	[SerializeField]
	protected int[] padding;

	// Token: 0x04002206 RID: 8710
	[SerializeField]
	protected int[] spacing;

	// Token: 0x04002207 RID: 8711
	[SerializeField]
	protected int outline;

	// Token: 0x04002208 RID: 8712
	[SerializeField]
	protected int lineHeight;

	// Token: 0x04002209 RID: 8713
	[SerializeField]
	private List<global::dfFont.GlyphDefinition> glyphs = new List<global::dfFont.GlyphDefinition>();

	// Token: 0x0400220A RID: 8714
	[SerializeField]
	protected List<global::dfFont.GlyphKerning> kerning = new List<global::dfFont.GlyphKerning>();

	// Token: 0x0400220B RID: 8715
	private Dictionary<int, global::dfFont.GlyphDefinition> glyphMap;

	// Token: 0x0400220C RID: 8716
	private Dictionary<int, global::dfFont.GlyphKerningList> kerningMap;

	// Token: 0x0200078F RID: 1935
	private class GlyphKerningList
	{
		// Token: 0x060040C8 RID: 16584 RVA: 0x000ED040 File Offset: 0x000EB240
		public void Add(global::dfFont.GlyphKerning kerning)
		{
			this.list[kerning.second] = kerning.amount;
		}

		// Token: 0x060040C9 RID: 16585 RVA: 0x000ED05C File Offset: 0x000EB25C
		public int GetKerning(int firstCharacter, int secondCharacter)
		{
			int result = 0;
			this.list.TryGetValue(secondCharacter, out result);
			return result;
		}

		// Token: 0x0400220D RID: 8717
		private Dictionary<int, int> list = new Dictionary<int, int>();
	}

	// Token: 0x02000790 RID: 1936
	[Serializable]
	public class GlyphKerning : IComparable<global::dfFont.GlyphKerning>
	{
		// Token: 0x060040CB RID: 16587 RVA: 0x000ED084 File Offset: 0x000EB284
		public int CompareTo(global::dfFont.GlyphKerning other)
		{
			if (this.first == other.first)
			{
				return this.second.CompareTo(other.second);
			}
			return this.first.CompareTo(other.first);
		}

		// Token: 0x0400220E RID: 8718
		public int first;

		// Token: 0x0400220F RID: 8719
		public int second;

		// Token: 0x04002210 RID: 8720
		public int amount;
	}

	// Token: 0x02000791 RID: 1937
	[Serializable]
	public class GlyphDefinition : IComparable<global::dfFont.GlyphDefinition>
	{
		// Token: 0x060040CD RID: 16589 RVA: 0x000ED0D0 File Offset: 0x000EB2D0
		public int CompareTo(global::dfFont.GlyphDefinition other)
		{
			return this.id.CompareTo(other.id);
		}

		// Token: 0x04002211 RID: 8721
		[SerializeField]
		public int id;

		// Token: 0x04002212 RID: 8722
		[SerializeField]
		public int x;

		// Token: 0x04002213 RID: 8723
		[SerializeField]
		public int y;

		// Token: 0x04002214 RID: 8724
		[SerializeField]
		public int width;

		// Token: 0x04002215 RID: 8725
		[SerializeField]
		public int height;

		// Token: 0x04002216 RID: 8726
		[SerializeField]
		public int xoffset;

		// Token: 0x04002217 RID: 8727
		[SerializeField]
		public int yoffset;

		// Token: 0x04002218 RID: 8728
		[SerializeField]
		public int xadvance;

		// Token: 0x04002219 RID: 8729
		[SerializeField]
		public bool rotated;
	}

	// Token: 0x02000792 RID: 1938
	public class BitmappedFontRenderer : global::dfFontRendererBase
	{
		// Token: 0x060040CE RID: 16590 RVA: 0x000ED0E4 File Offset: 0x000EB2E4
		internal BitmappedFontRenderer()
		{
		}

		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x060040D0 RID: 16592 RVA: 0x000ED19C File Offset: 0x000EB39C
		public int LineCount
		{
			get
			{
				return this.lines.Count;
			}
		}

		// Token: 0x060040D1 RID: 16593 RVA: 0x000ED1AC File Offset: 0x000EB3AC
		public static global::dfFontRendererBase Obtain(global::dfFont font)
		{
			global::dfFont.BitmappedFontRenderer bitmappedFontRenderer = (global::dfFont.BitmappedFontRenderer.objectPool.Count <= 0) ? new global::dfFont.BitmappedFontRenderer() : global::dfFont.BitmappedFontRenderer.objectPool.Dequeue();
			bitmappedFontRenderer.Reset();
			bitmappedFontRenderer.Font = font;
			return bitmappedFontRenderer;
		}

		// Token: 0x060040D2 RID: 16594 RVA: 0x000ED1EC File Offset: 0x000EB3EC
		public override void Release()
		{
			this.Reset();
			this.tokens = null;
			if (this.lines != null)
			{
				this.lines.Release();
				this.lines = null;
			}
			global::dfFont.LineRenderInfo.ResetPool();
			base.BottomColor = null;
			global::dfFont.BitmappedFontRenderer.objectPool.Enqueue(this);
		}

		// Token: 0x060040D3 RID: 16595 RVA: 0x000ED244 File Offset: 0x000EB444
		public override float[] GetCharacterWidths(string text)
		{
			float num = 0f;
			return this.GetCharacterWidths(text, 0, text.Length - 1, out num);
		}

		// Token: 0x060040D4 RID: 16596 RVA: 0x000ED26C File Offset: 0x000EB46C
		public float[] GetCharacterWidths(string text, int startIndex, int endIndex, out float totalWidth)
		{
			totalWidth = 0f;
			global::dfFont dfFont = (global::dfFont)base.Font;
			float[] array = new float[text.Length];
			float num = base.TextScale * base.PixelRatio;
			float num2 = (float)base.CharacterSpacing * num;
			for (int i = startIndex; i <= endIndex; i++)
			{
				global::dfFont.GlyphDefinition glyph = dfFont.GetGlyph(text[i]);
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

		// Token: 0x060040D5 RID: 16597 RVA: 0x000ED320 File Offset: 0x000EB520
		public override Vector2 MeasureString(string text)
		{
			this.tokenize(text);
			global::dfList<global::dfFont.LineRenderInfo> dfList = this.calculateLinebreaks();
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < dfList.Count; i++)
			{
				num = Mathf.Max((int)dfList[i].lineWidth, num);
				num2 += (int)dfList[i].lineHeight;
			}
			return new Vector2((float)num, (float)num2) * base.TextScale;
		}

		// Token: 0x060040D6 RID: 16598 RVA: 0x000ED390 File Offset: 0x000EB590
		public override void Render(string text, global::dfRenderData destination)
		{
			global::dfFont.BitmappedFontRenderer.textColors.Clear();
			global::dfFont.BitmappedFontRenderer.textColors.Push(Color.white);
			this.tokenize(text);
			global::dfList<global::dfFont.LineRenderInfo> dfList = this.calculateLinebreaks();
			int num = 0;
			int num2 = 0;
			Vector3 vectorOffset = base.VectorOffset;
			float num3 = base.TextScale * base.PixelRatio;
			for (int i = 0; i < dfList.Count; i++)
			{
				global::dfFont.LineRenderInfo lineRenderInfo = dfList[i];
				int count = destination.Vertices.Count;
				this.renderLine(dfList[i], global::dfFont.BitmappedFontRenderer.textColors, vectorOffset, destination);
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

		// Token: 0x060040D7 RID: 16599 RVA: 0x000ED4F8 File Offset: 0x000EB6F8
		private void renderLine(global::dfFont.LineRenderInfo line, Stack<Color32> colors, Vector3 position, global::dfRenderData destination)
		{
			float num = base.TextScale * base.PixelRatio;
			position.x += (float)this.calculateLineAlignment(line) * num;
			for (int i = line.startOffset; i <= line.endOffset; i++)
			{
				global::dfMarkupToken dfMarkupToken = this.tokens[i];
				global::dfMarkupTokenType tokenType = dfMarkupToken.TokenType;
				if (tokenType == global::dfMarkupTokenType.Text)
				{
					this.renderText(dfMarkupToken, colors.Peek(), position, destination);
				}
				else if (tokenType == global::dfMarkupTokenType.StartTag)
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
				else if (tokenType == global::dfMarkupTokenType.EndTag && dfMarkupToken.Matches("color") && colors.Count > 1)
				{
					colors.Pop();
				}
				position.x += (float)dfMarkupToken.Width * num;
			}
		}

		// Token: 0x060040D8 RID: 16600 RVA: 0x000ED608 File Offset: 0x000EB808
		private void renderText(global::dfMarkupToken token, Color32 color, Vector3 position, global::dfRenderData destination)
		{
			try
			{
				global::dfList<Vector3> vertices = destination.Vertices;
				global::dfList<int> triangles = destination.Triangles;
				global::dfList<Color32> colors = destination.Colors;
				global::dfList<Vector2> uv = destination.UV;
				global::dfFont dfFont = (global::dfFont)base.Font;
				global::dfAtlas.ItemInfo itemInfo = dfFont.Atlas[dfFont.sprite];
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
						global::dfFont.GlyphDefinition glyph = dfFont.GetGlyph(c);
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
								global::dfFont.BitmappedFontRenderer.addTriangleIndices(vertices, triangles);
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
								for (int j = 0; j < global::dfFont.BitmappedFontRenderer.OUTLINE_OFFSETS.Length; j++)
								{
									global::dfFont.BitmappedFontRenderer.addTriangleIndices(vertices, triangles);
									Vector3 vector6 = global::dfFont.BitmappedFontRenderer.OUTLINE_OFFSETS[j] * (float)base.OutlineSize * num5;
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
							global::dfFont.BitmappedFontRenderer.addTriangleIndices(vertices, triangles);
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

		// Token: 0x060040D9 RID: 16601 RVA: 0x000EDAE0 File Offset: 0x000EBCE0
		private void renderSprite(global::dfMarkupToken token, Color32 color, Vector3 position, global::dfRenderData destination)
		{
			try
			{
				global::dfList<Vector3> vertices = destination.Vertices;
				global::dfList<int> triangles = destination.Triangles;
				global::dfList<Color32> colors = destination.Colors;
				global::dfList<Vector2> uv = destination.UV;
				global::dfFont dfFont = (global::dfFont)base.Font;
				string value = token.GetAttribute(0).Value.Value;
				global::dfAtlas.ItemInfo itemInfo = dfFont.Atlas[value];
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

		// Token: 0x060040DA RID: 16602 RVA: 0x000EDCF0 File Offset: 0x000EBEF0
		private Color32 parseColor(global::dfMarkupToken token)
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
					color = global::dfMarkupStyle.ParseColor(value, base.DefaultColor);
				}
			}
			return this.applyOpacity(color);
		}

		// Token: 0x060040DB RID: 16603 RVA: 0x000EDD88 File Offset: 0x000EBF88
		private Color32 UIntToColor(uint color)
		{
			byte b = (byte)(color >> 24);
			byte b2 = (byte)(color >> 16);
			byte b3 = (byte)(color >> 8);
			byte b4 = (byte)color;
			return new Color32(b2, b3, b4, b);
		}

		// Token: 0x060040DC RID: 16604 RVA: 0x000EDDB4 File Offset: 0x000EBFB4
		private global::dfList<global::dfFont.LineRenderInfo> calculateLinebreaks()
		{
			global::dfList<global::dfFont.LineRenderInfo> result;
			try
			{
				if (this.lines != null)
				{
					result = this.lines;
				}
				else
				{
					this.lines = global::dfList<global::dfFont.LineRenderInfo>.Obtain();
					int num = 0;
					int num2 = 0;
					int num3 = 0;
					int num4 = 0;
					float num5 = (float)base.Font.LineHeight * base.TextScale;
					while (num3 < this.tokens.Count && (float)this.lines.Count * num5 < base.MaxSize.y)
					{
						global::dfMarkupToken dfMarkupToken = this.tokens[num3];
						global::dfMarkupTokenType tokenType = dfMarkupToken.TokenType;
						if (tokenType == global::dfMarkupTokenType.Newline)
						{
							this.lines.Add(global::dfFont.LineRenderInfo.Obtain(num2, num3));
							num = (num2 = ++num3);
							num4 = 0;
						}
						else
						{
							int num6 = Mathf.CeilToInt((float)dfMarkupToken.Width * base.TextScale);
							bool flag = base.WordWrap && num > num2 && (tokenType == global::dfMarkupTokenType.Text || (tokenType == global::dfMarkupTokenType.StartTag && dfMarkupToken.Matches("sprite")));
							if (flag && (float)(num4 + num6) >= base.MaxSize.x)
							{
								if (num > num2)
								{
									this.lines.Add(global::dfFont.LineRenderInfo.Obtain(num2, num - 1));
									num3 = (num2 = ++num);
									num4 = 0;
								}
								else
								{
									this.lines.Add(global::dfFont.LineRenderInfo.Obtain(num2, num - 1));
									num = (num2 = ++num3);
									num4 = 0;
								}
							}
							else
							{
								if (tokenType == global::dfMarkupTokenType.Whitespace)
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
						this.lines.Add(global::dfFont.LineRenderInfo.Obtain(num2, this.tokens.Count - 1));
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

		// Token: 0x060040DD RID: 16605 RVA: 0x000EDFDC File Offset: 0x000EC1DC
		private int calculateLineAlignment(global::dfFont.LineRenderInfo line)
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

		// Token: 0x060040DE RID: 16606 RVA: 0x000EE064 File Offset: 0x000EC264
		private void calculateLineSize(global::dfFont.LineRenderInfo line)
		{
			line.lineHeight = (float)base.Font.LineHeight;
			int num = 0;
			for (int i = line.startOffset; i <= line.endOffset; i++)
			{
				num += this.tokens[i].Width;
			}
			line.lineWidth = (float)num;
		}

		// Token: 0x060040DF RID: 16607 RVA: 0x000EE0C0 File Offset: 0x000EC2C0
		private List<global::dfMarkupToken> tokenize(string text)
		{
			List<global::dfMarkupToken> result;
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
						this.tokens = global::dfMarkupTokenizer.Tokenize(text);
					}
					else
					{
						this.tokens = global::dfPlainTextTokenizer.Tokenize(text);
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

		// Token: 0x060040E0 RID: 16608 RVA: 0x000EE198 File Offset: 0x000EC398
		private void calculateTokenRenderSize(global::dfMarkupToken token)
		{
			try
			{
				global::dfFont dfFont = (global::dfFont)base.Font;
				int num = 0;
				char previousChar = '\0';
				bool flag = token.TokenType == global::dfMarkupTokenType.Whitespace || token.TokenType == global::dfMarkupTokenType.Text;
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
							global::dfFont.GlyphDefinition glyph = dfFont.GetGlyph(c);
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
				else if (token.TokenType == global::dfMarkupTokenType.StartTag && token.Matches("sprite"))
				{
					if (token.AttributeCount < 1)
					{
						throw new Exception("Missing sprite name in markup");
					}
					Texture texture = dfFont.Texture;
					int lineHeight = dfFont.LineHeight;
					string value = token.GetAttribute(0).Value.Value;
					global::dfAtlas.ItemInfo itemInfo = dfFont.atlas[value];
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

		// Token: 0x060040E1 RID: 16609 RVA: 0x000EE33C File Offset: 0x000EC53C
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

		// Token: 0x060040E2 RID: 16610 RVA: 0x000EE3E0 File Offset: 0x000EC5E0
		private void clipRight(global::dfRenderData destination, int startIndex)
		{
			float num = base.VectorOffset.x + base.MaxSize.x * base.PixelRatio;
			global::dfList<Vector3> vertices = destination.Vertices;
			global::dfList<Vector2> uv = destination.UV;
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
					global::dfList<Vector3> dfList = vertices;
					int index = i;
					value..ctor(Mathf.Min(value.x, num), value.y, value.z);
					dfList[index] = value;
					global::dfList<Vector3> dfList2 = vertices;
					int index2 = i + 1;
					value2..ctor(Mathf.Min(value2.x, num), value2.y, value2.z);
					dfList2[index2] = value2;
					global::dfList<Vector3> dfList3 = vertices;
					int index3 = i + 2;
					value3..ctor(Mathf.Min(value3.x, num), value3.y, value3.z);
					dfList3[index3] = value3;
					global::dfList<Vector3> dfList4 = vertices;
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

		// Token: 0x060040E3 RID: 16611 RVA: 0x000EE5CC File Offset: 0x000EC7CC
		private void clipBottom(global::dfRenderData destination, int startIndex)
		{
			float num = base.VectorOffset.y - base.MaxSize.y * base.PixelRatio;
			global::dfList<Vector3> vertices = destination.Vertices;
			global::dfList<Vector2> uv = destination.UV;
			global::dfList<Color32> colors = destination.Colors;
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
					global::dfList<Vector3> dfList = vertices;
					int index = i;
					value..ctor(value.x, Mathf.Max(value.y, num), value2.z);
					dfList[index] = value;
					global::dfList<Vector3> dfList2 = vertices;
					int index2 = i + 1;
					value2..ctor(value2.x, Mathf.Max(value2.y, num), value2.z);
					dfList2[index2] = value2;
					global::dfList<Vector3> dfList3 = vertices;
					int index3 = i + 2;
					value3..ctor(value3.x, Mathf.Max(value3.y, num), value3.z);
					dfList3[index3] = value3;
					global::dfList<Vector3> dfList4 = vertices;
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

		// Token: 0x060040E4 RID: 16612 RVA: 0x000EE808 File Offset: 0x000ECA08
		private Color32 applyOpacity(Color32 color)
		{
			color.a = (byte)(base.Opacity * 255f);
			return color;
		}

		// Token: 0x060040E5 RID: 16613 RVA: 0x000EE820 File Offset: 0x000ECA20
		private static void addTriangleIndices(global::dfList<Vector3> verts, global::dfList<int> triangles)
		{
			int count = verts.Count;
			int[] triangle_INDICES = global::dfFont.BitmappedFontRenderer.TRIANGLE_INDICES;
			for (int i = 0; i < triangle_INDICES.Length; i++)
			{
				triangles.Add(count + triangle_INDICES[i]);
			}
		}

		// Token: 0x060040E6 RID: 16614 RVA: 0x000EE85C File Offset: 0x000ECA5C
		private Color multiplyColors(Color lhs, Color rhs)
		{
			return new Color(lhs.r * rhs.r, lhs.g * rhs.g, lhs.b * rhs.b, lhs.a * rhs.a);
		}

		// Token: 0x060040E7 RID: 16615 RVA: 0x000EE8AC File Offset: 0x000ECAAC
		private global::dfFont.LineRenderInfo fitSingleLine()
		{
			return global::dfFont.LineRenderInfo.Obtain(0, 0);
		}

		// Token: 0x0400221A RID: 8730
		private static Queue<global::dfFont.BitmappedFontRenderer> objectPool = new Queue<global::dfFont.BitmappedFontRenderer>();

		// Token: 0x0400221B RID: 8731
		private static Vector2[] OUTLINE_OFFSETS = new Vector2[]
		{
			new Vector2(-1f, -1f),
			new Vector2(-1f, 1f),
			new Vector2(1f, -1f),
			new Vector2(1f, 1f)
		};

		// Token: 0x0400221C RID: 8732
		private static int[] TRIANGLE_INDICES = new int[]
		{
			0,
			1,
			3,
			3,
			1,
			2
		};

		// Token: 0x0400221D RID: 8733
		private static Stack<Color32> textColors = new Stack<Color32>();

		// Token: 0x0400221E RID: 8734
		private global::dfList<global::dfFont.LineRenderInfo> lines;

		// Token: 0x0400221F RID: 8735
		private List<global::dfMarkupToken> tokens;
	}

	// Token: 0x02000793 RID: 1939
	private class LineRenderInfo
	{
		// Token: 0x060040E8 RID: 16616 RVA: 0x000EE8C4 File Offset: 0x000ECAC4
		private LineRenderInfo()
		{
		}

		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x060040EA RID: 16618 RVA: 0x000EE8E0 File Offset: 0x000ECAE0
		public int length
		{
			get
			{
				return this.endOffset - this.startOffset + 1;
			}
		}

		// Token: 0x060040EB RID: 16619 RVA: 0x000EE8F4 File Offset: 0x000ECAF4
		public static void ResetPool()
		{
			global::dfFont.LineRenderInfo.poolIndex = 0;
		}

		// Token: 0x060040EC RID: 16620 RVA: 0x000EE8FC File Offset: 0x000ECAFC
		public static global::dfFont.LineRenderInfo Obtain(int start, int end)
		{
			if (global::dfFont.LineRenderInfo.poolIndex >= global::dfFont.LineRenderInfo.pool.Count - 1)
			{
				global::dfFont.LineRenderInfo.pool.Add(new global::dfFont.LineRenderInfo());
			}
			global::dfFont.LineRenderInfo lineRenderInfo = global::dfFont.LineRenderInfo.pool[global::dfFont.LineRenderInfo.poolIndex++];
			lineRenderInfo.startOffset = start;
			lineRenderInfo.endOffset = end;
			lineRenderInfo.lineHeight = 0f;
			return lineRenderInfo;
		}

		// Token: 0x04002220 RID: 8736
		public int startOffset;

		// Token: 0x04002221 RID: 8737
		public int endOffset;

		// Token: 0x04002222 RID: 8738
		public float lineWidth;

		// Token: 0x04002223 RID: 8739
		public float lineHeight;

		// Token: 0x04002224 RID: 8740
		private static global::dfList<global::dfFont.LineRenderInfo> pool = new global::dfList<global::dfFont.LineRenderInfo>();

		// Token: 0x04002225 RID: 8741
		private static int poolIndex = 0;
	}
}
