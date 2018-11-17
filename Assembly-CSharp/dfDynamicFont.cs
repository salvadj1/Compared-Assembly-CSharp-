using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

// Token: 0x0200070F RID: 1807
[AddComponentMenu("Daikon Forge/User Interface/Dynamic Font")]
[ExecuteInEditMode]
[Serializable]
public class dfDynamicFont : dfFontBase
{
	// Token: 0x17000CFD RID: 3325
	// (get) Token: 0x0600421D RID: 16925 RVA: 0x000FED3C File Offset: 0x000FCF3C
	// (set) Token: 0x0600421E RID: 16926 RVA: 0x000FED60 File Offset: 0x000FCF60
	public override Material Material
	{
		get
		{
			this.material.mainTexture = this.baseFont.material.mainTexture;
			return this.material;
		}
		set
		{
			if (value != this.material)
			{
				this.material = value;
				dfGUIManager.RefreshAll(false);
			}
		}
	}

	// Token: 0x17000CFE RID: 3326
	// (get) Token: 0x0600421F RID: 16927 RVA: 0x000FED80 File Offset: 0x000FCF80
	public override Texture Texture
	{
		get
		{
			return this.baseFont.material.mainTexture;
		}
	}

	// Token: 0x17000CFF RID: 3327
	// (get) Token: 0x06004220 RID: 16928 RVA: 0x000FED94 File Offset: 0x000FCF94
	public override bool IsValid
	{
		get
		{
			return this.baseFont != null && this.Material != null && this.Texture != null;
		}
	}

	// Token: 0x17000D00 RID: 3328
	// (get) Token: 0x06004221 RID: 16929 RVA: 0x000FEDD4 File Offset: 0x000FCFD4
	// (set) Token: 0x06004222 RID: 16930 RVA: 0x000FEDDC File Offset: 0x000FCFDC
	public override int FontSize
	{
		get
		{
			return this.baseFontSize;
		}
		set
		{
			if (value != this.baseFontSize)
			{
				this.baseFontSize = value;
				dfGUIManager.RefreshAll(false);
			}
		}
	}

	// Token: 0x17000D01 RID: 3329
	// (get) Token: 0x06004223 RID: 16931 RVA: 0x000FEDF8 File Offset: 0x000FCFF8
	// (set) Token: 0x06004224 RID: 16932 RVA: 0x000FEE00 File Offset: 0x000FD000
	public override int LineHeight
	{
		get
		{
			return this.lineHeight;
		}
		set
		{
			if (value != this.lineHeight)
			{
				this.lineHeight = value;
				dfGUIManager.RefreshAll(false);
			}
		}
	}

	// Token: 0x06004225 RID: 16933 RVA: 0x000FEE1C File Offset: 0x000FD01C
	public override dfFontRendererBase ObtainRenderer()
	{
		return dfDynamicFont.DynamicFontRenderer.Obtain(this);
	}

	// Token: 0x17000D02 RID: 3330
	// (get) Token: 0x06004226 RID: 16934 RVA: 0x000FEE24 File Offset: 0x000FD024
	// (set) Token: 0x06004227 RID: 16935 RVA: 0x000FEE2C File Offset: 0x000FD02C
	public Font BaseFont
	{
		get
		{
			return this.baseFont;
		}
		set
		{
			if (value != this.baseFont)
			{
				this.baseFont = value;
				dfGUIManager.RefreshAll(false);
			}
		}
	}

	// Token: 0x17000D03 RID: 3331
	// (get) Token: 0x06004228 RID: 16936 RVA: 0x000FEE4C File Offset: 0x000FD04C
	// (set) Token: 0x06004229 RID: 16937 RVA: 0x000FEE54 File Offset: 0x000FD054
	public int Baseline
	{
		get
		{
			return this.baseline;
		}
		set
		{
			if (value != this.baseline)
			{
				this.baseline = value;
				dfGUIManager.RefreshAll(false);
			}
		}
	}

	// Token: 0x17000D04 RID: 3332
	// (get) Token: 0x0600422A RID: 16938 RVA: 0x000FEE70 File Offset: 0x000FD070
	public int Descent
	{
		get
		{
			return this.LineHeight - this.baseline;
		}
	}

	// Token: 0x0600422B RID: 16939 RVA: 0x000FEE80 File Offset: 0x000FD080
	public static dfDynamicFont FindByName(string name)
	{
		for (int i = 0; i < dfDynamicFont.loadedFonts.Count; i++)
		{
			if (string.Equals(dfDynamicFont.loadedFonts[i].name, name, StringComparison.InvariantCultureIgnoreCase))
			{
				return dfDynamicFont.loadedFonts[i];
			}
		}
		GameObject gameObject = Resources.Load(name) as GameObject;
		if (gameObject == null)
		{
			return null;
		}
		dfDynamicFont component = gameObject.GetComponent<dfDynamicFont>();
		if (component == null)
		{
			return null;
		}
		dfDynamicFont.loadedFonts.Add(component);
		return component;
	}

	// Token: 0x0600422C RID: 16940 RVA: 0x000FEF0C File Offset: 0x000FD10C
	public Vector2 MeasureText(string text, int size, FontStyle style)
	{
		CharacterInfo[] array = this.RequestCharacters(text, size, style);
		float num = (float)size / (float)this.FontSize;
		int num2 = Mathf.CeilToInt((float)this.Baseline * num);
		Vector2 result;
		result..ctor(0f, (float)num2);
		for (int i = 0; i < text.Length; i++)
		{
			CharacterInfo characterInfo = array[i];
			float num3 = Mathf.Ceil(characterInfo.vert.x + characterInfo.vert.width);
			if (text[i] == ' ')
			{
				num3 = Mathf.Ceil(characterInfo.width * 1.25f);
			}
			else if (text[i] == '\t')
			{
				num3 += (float)(size * 4);
			}
			result.x += num3;
		}
		return result;
	}

	// Token: 0x0600422D RID: 16941 RVA: 0x000FEFE8 File Offset: 0x000FD1E8
	public CharacterInfo[] RequestCharacters(string text, int size, FontStyle style)
	{
		if (this.baseFont == null)
		{
			throw new NullReferenceException("Base Font not assigned: " + base.name);
		}
		this.ensureGlyphBufferCapacity(size);
		if (!dfDynamicFont.loadedFonts.Contains(this))
		{
			Font font = this.baseFont;
			font.textureRebuildCallback = (Font.FontTextureRebuildCallback)Delegate.Combine(font.textureRebuildCallback, new Font.FontTextureRebuildCallback(this.onFontAtlasRebuilt));
			dfDynamicFont.loadedFonts.Add(this);
		}
		this.baseFont.RequestCharactersInTexture(text, size, style);
		this.getGlyphData(dfDynamicFont.glyphBuffer, text, size, style);
		return dfDynamicFont.glyphBuffer;
	}

	// Token: 0x0600422E RID: 16942 RVA: 0x000FF088 File Offset: 0x000FD288
	private void onFontAtlasRebuilt()
	{
		this.wasFontAtlasRebuilt = true;
		this.OnFontChanged();
	}

	// Token: 0x0600422F RID: 16943 RVA: 0x000FF098 File Offset: 0x000FD298
	private void OnFontChanged()
	{
		try
		{
			if (!this.invalidatingDependentControls)
			{
				dfGUIManager.RenderCallback callback = null;
				callback = delegate(dfGUIManager manager)
				{
					dfGUIManager.AfterRender -= callback;
					this.invalidatingDependentControls = true;
					try
					{
						if (this.wasFontAtlasRebuilt)
						{
						}
						List<dfControl> list = (from dfControl x in 
							from x in Object.FindObjectsOfType(typeof(dfControl))
							where x is IDFMultiRender
							select x
						orderby x.RenderOrder
						select x).ToList<dfControl>();
						for (int i = 0; i < list.Count; i++)
						{
							list[i].Invalidate();
						}
						if (this.wasFontAtlasRebuilt)
						{
							manager.Render();
						}
					}
					finally
					{
						this.wasFontAtlasRebuilt = false;
						this.invalidatingDependentControls = false;
					}
				};
				dfGUIManager.AfterRender += callback;
			}
		}
		finally
		{
		}
	}

	// Token: 0x06004230 RID: 16944 RVA: 0x000FF108 File Offset: 0x000FD308
	private void ensureGlyphBufferCapacity(int size)
	{
		int i = dfDynamicFont.glyphBuffer.Length;
		if (size < i)
		{
			return;
		}
		while (i < size)
		{
			i += 1024;
		}
		dfDynamicFont.glyphBuffer = new CharacterInfo[i];
	}

	// Token: 0x06004231 RID: 16945 RVA: 0x000FF144 File Offset: 0x000FD344
	private void getGlyphData(CharacterInfo[] result, string text, int size, FontStyle style)
	{
		if (text.Length > dfDynamicFont.glyphBuffer.Length)
		{
			dfDynamicFont.glyphBuffer = new CharacterInfo[text.Length + 512];
		}
		for (int i = 0; i < text.Length; i++)
		{
			if (!this.baseFont.GetCharacterInfo(text[i], ref result[i], size, style))
			{
				int num = i;
				CharacterInfo characterInfo = default(CharacterInfo);
				characterInfo.index = -1;
				characterInfo.size = size;
				characterInfo.style = style;
				characterInfo.width = (float)size * 0.25f;
				result[num] = characterInfo;
			}
		}
	}

	// Token: 0x040022CE RID: 8910
	private static List<dfDynamicFont> loadedFonts = new List<dfDynamicFont>();

	// Token: 0x040022CF RID: 8911
	private static CharacterInfo[] glyphBuffer = new CharacterInfo[1024];

	// Token: 0x040022D0 RID: 8912
	[SerializeField]
	private Font baseFont;

	// Token: 0x040022D1 RID: 8913
	[SerializeField]
	private Material material;

	// Token: 0x040022D2 RID: 8914
	[SerializeField]
	private int baseFontSize = -1;

	// Token: 0x040022D3 RID: 8915
	[SerializeField]
	private int baseline = -1;

	// Token: 0x040022D4 RID: 8916
	[SerializeField]
	private int lineHeight;

	// Token: 0x040022D5 RID: 8917
	private bool invalidatingDependentControls;

	// Token: 0x040022D6 RID: 8918
	private bool wasFontAtlasRebuilt;

	// Token: 0x02000710 RID: 1808
	public class DynamicFontRenderer : dfFontRendererBase
	{
		// Token: 0x06004232 RID: 16946 RVA: 0x000FF1F0 File Offset: 0x000FD3F0
		internal DynamicFontRenderer()
		{
		}

		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x06004234 RID: 16948 RVA: 0x000FF2A8 File Offset: 0x000FD4A8
		public int LineCount
		{
			get
			{
				return this.lines.Count;
			}
		}

		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x06004235 RID: 16949 RVA: 0x000FF2B8 File Offset: 0x000FD4B8
		// (set) Token: 0x06004236 RID: 16950 RVA: 0x000FF2C0 File Offset: 0x000FD4C0
		public dfAtlas SpriteAtlas { get; set; }

		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x06004237 RID: 16951 RVA: 0x000FF2CC File Offset: 0x000FD4CC
		// (set) Token: 0x06004238 RID: 16952 RVA: 0x000FF2D4 File Offset: 0x000FD4D4
		public dfRenderData SpriteBuffer { get; set; }

		// Token: 0x06004239 RID: 16953 RVA: 0x000FF2E0 File Offset: 0x000FD4E0
		public static dfFontRendererBase Obtain(dfDynamicFont font)
		{
			dfDynamicFont.DynamicFontRenderer dynamicFontRenderer = (dfDynamicFont.DynamicFontRenderer.objectPool.Count <= 0) ? new dfDynamicFont.DynamicFontRenderer() : dfDynamicFont.DynamicFontRenderer.objectPool.Dequeue();
			dynamicFontRenderer.Reset();
			dynamicFontRenderer.Font = font;
			return dynamicFontRenderer;
		}

		// Token: 0x0600423A RID: 16954 RVA: 0x000FF320 File Offset: 0x000FD520
		public override void Release()
		{
			this.Reset();
			this.tokens = null;
			if (this.lines != null)
			{
				this.lines.Release();
				this.lines = null;
			}
			dfDynamicFont.LineRenderInfo.ResetPool();
			base.BottomColor = null;
			dfDynamicFont.DynamicFontRenderer.objectPool.Enqueue(this);
		}

		// Token: 0x0600423B RID: 16955 RVA: 0x000FF378 File Offset: 0x000FD578
		public override float[] GetCharacterWidths(string text)
		{
			float num = 0f;
			return this.GetCharacterWidths(text, 0, text.Length - 1, out num);
		}

		// Token: 0x0600423C RID: 16956 RVA: 0x000FF3A0 File Offset: 0x000FD5A0
		public float[] GetCharacterWidths(string text, int startIndex, int endIndex, out float totalWidth)
		{
			totalWidth = 0f;
			dfDynamicFont dfDynamicFont = (dfDynamicFont)base.Font;
			int size = Mathf.CeilToInt((float)dfDynamicFont.FontSize * base.TextScale);
			CharacterInfo[] array = dfDynamicFont.RequestCharacters(text, size, 0);
			float[] array2 = new float[text.Length];
			float num = 0f;
			float num2 = 0f;
			int i = startIndex;
			while (i <= endIndex)
			{
				CharacterInfo characterInfo = array[i];
				if (text[i] == '\t')
				{
					num2 += (float)base.TabSize;
				}
				else if (text[i] == ' ')
				{
					num2 += characterInfo.width;
				}
				else
				{
					num2 += characterInfo.vert.x + characterInfo.vert.width;
				}
				array2[i] = (num2 - num) * base.PixelRatio;
				i++;
				num = num2;
			}
			return array2;
		}

		// Token: 0x0600423D RID: 16957 RVA: 0x000FF494 File Offset: 0x000FD694
		public override Vector2 MeasureString(string text)
		{
			this.tokenize(text);
			dfList<dfDynamicFont.LineRenderInfo> dfList = this.calculateLinebreaks();
			float num = 0f;
			float num2 = 0f;
			for (int i = 0; i < dfList.Count; i++)
			{
				num = Mathf.Max(dfList[i].lineWidth, num);
				num2 += dfList[i].lineHeight;
			}
			Vector2 result;
			result..ctor(num, num2);
			return result;
		}

		// Token: 0x0600423E RID: 16958 RVA: 0x000FF500 File Offset: 0x000FD700
		public override void Render(string text, dfRenderData destination)
		{
			dfDynamicFont.DynamicFontRenderer.textColors.Clear();
			dfDynamicFont.DynamicFontRenderer.textColors.Push(Color.white);
			this.tokenize(text);
			dfList<dfDynamicFont.LineRenderInfo> dfList = this.calculateLinebreaks();
			int num = 0;
			int num2 = 0;
			Vector3 position = (base.VectorOffset / base.PixelRatio).CeilToInt();
			for (int i = 0; i < dfList.Count; i++)
			{
				dfDynamicFont.LineRenderInfo lineRenderInfo = dfList[i];
				int count = destination.Vertices.Count;
				int startIndex = (this.SpriteBuffer == null) ? 0 : this.SpriteBuffer.Vertices.Count;
				this.renderLine(dfList[i], dfDynamicFont.DynamicFontRenderer.textColors, position, destination);
				position.y -= lineRenderInfo.lineHeight;
				num = Mathf.Max((int)lineRenderInfo.lineWidth, num);
				num2 += Mathf.CeilToInt(lineRenderInfo.lineHeight);
				if (lineRenderInfo.lineWidth > base.MaxSize.x)
				{
					this.clipRight(destination, count);
					this.clipRight(this.SpriteBuffer, startIndex);
				}
				this.clipBottom(destination, count);
				this.clipBottom(this.SpriteBuffer, startIndex);
			}
			base.RenderedSize = new Vector2(Mathf.Min(base.MaxSize.x, (float)num), Mathf.Min(base.MaxSize.y, (float)num2)) * base.TextScale;
		}

		// Token: 0x0600423F RID: 16959 RVA: 0x000FF680 File Offset: 0x000FD880
		private void renderLine(dfDynamicFont.LineRenderInfo line, Stack<Color32> colors, Vector3 position, dfRenderData destination)
		{
			position.x += (float)this.calculateLineAlignment(line);
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
					if (dfMarkupToken.Matches("sprite") && this.SpriteAtlas != null && this.SpriteBuffer != null)
					{
						this.renderSprite(dfMarkupToken, colors.Peek(), position, this.SpriteBuffer);
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
				position.x += (float)dfMarkupToken.Width;
			}
		}

		// Token: 0x06004240 RID: 16960 RVA: 0x000FF79C File Offset: 0x000FD99C
		private void renderText(dfMarkupToken token, Color32 color, Vector3 position, dfRenderData renderData)
		{
			try
			{
				dfDynamicFont dfDynamicFont = (dfDynamicFont)base.Font;
				int num = Mathf.CeilToInt((float)dfDynamicFont.FontSize * base.TextScale);
				FontStyle style = 0;
				int descent = dfDynamicFont.Descent;
				dfList<Vector3> vertices = renderData.Vertices;
				dfList<int> triangles = renderData.Triangles;
				dfList<Vector2> uv = renderData.UV;
				dfList<Color32> colors = renderData.Colors;
				string value = token.Value;
				float num2 = position.x;
				float y = position.y;
				CharacterInfo[] array = dfDynamicFont.RequestCharacters(value, num, style);
				renderData.Material = dfDynamicFont.Material;
				Color32 color2 = this.applyOpacity(this.multiplyColors(color, base.DefaultColor));
				Color32 item = color2;
				if (base.BottomColor != null)
				{
					item = this.applyOpacity(this.multiplyColors(color, base.BottomColor.Value));
				}
				for (int i = 0; i < value.Length; i++)
				{
					if (i > 0)
					{
						num2 += (float)base.CharacterSpacing * base.TextScale;
					}
					CharacterInfo glyph = array[i];
					float num3 = (float)dfDynamicFont.FontSize + glyph.vert.y - (float)num + (float)descent;
					float num4 = num2 + glyph.vert.x;
					float num5 = y + num3;
					float num6 = num4 + glyph.vert.width;
					float num7 = num5 + glyph.vert.height;
					Vector3 vector = new Vector3(num4, num5) * base.PixelRatio;
					Vector3 vector2 = new Vector3(num6, num5) * base.PixelRatio;
					Vector3 vector3 = new Vector3(num6, num7) * base.PixelRatio;
					Vector3 vector4 = new Vector3(num4, num7) * base.PixelRatio;
					if (base.Shadow)
					{
						dfDynamicFont.DynamicFontRenderer.addTriangleIndices(vertices, triangles);
						Vector3 vector5 = base.ShadowOffset * base.PixelRatio;
						vertices.Add(vector + vector5);
						vertices.Add(vector2 + vector5);
						vertices.Add(vector3 + vector5);
						vertices.Add(vector4 + vector5);
						Color32 item2 = this.applyOpacity(base.ShadowColor);
						colors.Add(item2);
						colors.Add(item2);
						colors.Add(item2);
						colors.Add(item2);
						dfDynamicFont.DynamicFontRenderer.addUVCoords(uv, glyph);
					}
					if (base.Outline)
					{
						for (int j = 0; j < dfDynamicFont.DynamicFontRenderer.OUTLINE_OFFSETS.Length; j++)
						{
							dfDynamicFont.DynamicFontRenderer.addTriangleIndices(vertices, triangles);
							Vector3 vector6 = dfDynamicFont.DynamicFontRenderer.OUTLINE_OFFSETS[j] * (float)base.OutlineSize * base.PixelRatio;
							vertices.Add(vector + vector6);
							vertices.Add(vector2 + vector6);
							vertices.Add(vector3 + vector6);
							vertices.Add(vector4 + vector6);
							Color32 item3 = this.applyOpacity(base.OutlineColor);
							colors.Add(item3);
							colors.Add(item3);
							colors.Add(item3);
							colors.Add(item3);
							dfDynamicFont.DynamicFontRenderer.addUVCoords(uv, glyph);
						}
					}
					dfDynamicFont.DynamicFontRenderer.addTriangleIndices(vertices, triangles);
					vertices.Add(vector);
					vertices.Add(vector2);
					vertices.Add(vector3);
					vertices.Add(vector4);
					colors.Add(color2);
					colors.Add(color2);
					colors.Add(item);
					colors.Add(item);
					dfDynamicFont.DynamicFontRenderer.addUVCoords(uv, glyph);
					num2 += (float)Mathf.CeilToInt(glyph.vert.x + glyph.vert.width);
				}
			}
			finally
			{
			}
		}

		// Token: 0x06004241 RID: 16961 RVA: 0x000FFBA8 File Offset: 0x000FDDA8
		private static void addUVCoords(dfList<Vector2> uvs, CharacterInfo glyph)
		{
			Rect uv = glyph.uv;
			float x = uv.x;
			float num = uv.y + uv.height;
			float num2 = x + uv.width;
			float y = uv.y;
			if (glyph.flipped)
			{
				uvs.Add(new Vector2(num2, y));
				uvs.Add(new Vector2(num2, num));
				uvs.Add(new Vector2(x, num));
				uvs.Add(new Vector2(x, y));
			}
			else
			{
				uvs.Add(new Vector2(x, num));
				uvs.Add(new Vector2(num2, num));
				uvs.Add(new Vector2(num2, y));
				uvs.Add(new Vector2(x, y));
			}
		}

		// Token: 0x06004242 RID: 16962 RVA: 0x000FFC68 File Offset: 0x000FDE68
		private void renderSprite(dfMarkupToken token, Color32 color, Vector3 position, dfRenderData destination)
		{
			try
			{
				string value = token.GetAttribute(0).Value.Value;
				dfAtlas.ItemInfo itemInfo = this.SpriteAtlas[value];
				if (!(itemInfo == null))
				{
					dfSprite.RenderOptions options = new dfSprite.RenderOptions
					{
						atlas = this.SpriteAtlas,
						color = color,
						fillAmount = 1f,
						offset = position,
						pixelsToUnits = base.PixelRatio,
						size = new Vector2((float)token.Width, (float)token.Height),
						spriteInfo = itemInfo
					};
					dfSprite.renderSprite(this.SpriteBuffer, options);
				}
			}
			finally
			{
			}
		}

		// Token: 0x06004243 RID: 16963 RVA: 0x000FFD38 File Offset: 0x000FDF38
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

		// Token: 0x06004244 RID: 16964 RVA: 0x000FFDD0 File Offset: 0x000FDFD0
		private Color32 UIntToColor(uint color)
		{
			byte b = (byte)(color >> 24);
			byte b2 = (byte)(color >> 16);
			byte b3 = (byte)(color >> 8);
			byte b4 = (byte)color;
			return new Color32(b2, b3, b4, b);
		}

		// Token: 0x06004245 RID: 16965 RVA: 0x000FFDFC File Offset: 0x000FDFFC
		private dfList<dfDynamicFont.LineRenderInfo> calculateLinebreaks()
		{
			dfList<dfDynamicFont.LineRenderInfo> result;
			try
			{
				if (this.lines != null)
				{
					result = this.lines;
				}
				else
				{
					this.lines = dfList<dfDynamicFont.LineRenderInfo>.Obtain();
					dfDynamicFont dfDynamicFont = (dfDynamicFont)base.Font;
					int num = 0;
					int num2 = 0;
					int num3 = 0;
					int num4 = 0;
					float num5 = (float)dfDynamicFont.Baseline * base.TextScale;
					while (num3 < this.tokens.Count && (float)this.lines.Count * num5 <= base.MaxSize.y + num5)
					{
						dfMarkupToken dfMarkupToken = this.tokens[num3];
						dfMarkupTokenType tokenType = dfMarkupToken.TokenType;
						if (tokenType == dfMarkupTokenType.Newline)
						{
							this.lines.Add(dfDynamicFont.LineRenderInfo.Obtain(num2, num3));
							num = (num2 = ++num3);
							num4 = 0;
						}
						else
						{
							int num6 = Mathf.CeilToInt((float)dfMarkupToken.Width);
							bool flag = base.WordWrap && num > num2 && (tokenType == dfMarkupTokenType.Text || (tokenType == dfMarkupTokenType.StartTag && dfMarkupToken.Matches("sprite")));
							if (flag && (float)(num4 + num6) >= base.MaxSize.x)
							{
								if (num > num2)
								{
									this.lines.Add(dfDynamicFont.LineRenderInfo.Obtain(num2, num - 1));
									num3 = (num2 = ++num);
									num4 = 0;
								}
								else
								{
									this.lines.Add(dfDynamicFont.LineRenderInfo.Obtain(num2, num - 1));
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
						this.lines.Add(dfDynamicFont.LineRenderInfo.Obtain(num2, this.tokens.Count - 1));
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

		// Token: 0x06004246 RID: 16966 RVA: 0x00100030 File Offset: 0x000FE230
		private int calculateLineAlignment(dfDynamicFont.LineRenderInfo line)
		{
			float lineWidth = line.lineWidth;
			if (base.TextAlign == null || lineWidth < 1f)
			{
				return 0;
			}
			float num;
			if (base.TextAlign == 2)
			{
				num = base.MaxSize.x - lineWidth;
			}
			else
			{
				num = (base.MaxSize.x - lineWidth) * 0.5f;
			}
			return Mathf.CeilToInt(Mathf.Max(0f, num));
		}

		// Token: 0x06004247 RID: 16967 RVA: 0x001000AC File Offset: 0x000FE2AC
		private void calculateLineSize(dfDynamicFont.LineRenderInfo line)
		{
			dfDynamicFont dfDynamicFont = (dfDynamicFont)base.Font;
			line.lineHeight = (float)dfDynamicFont.Baseline * base.TextScale;
			int num = 0;
			for (int i = line.startOffset; i <= line.endOffset; i++)
			{
				num += this.tokens[i].Width;
			}
			line.lineWidth = (float)num;
		}

		// Token: 0x06004248 RID: 16968 RVA: 0x00100114 File Offset: 0x000FE314
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

		// Token: 0x06004249 RID: 16969 RVA: 0x001001EC File Offset: 0x000FE3EC
		private void calculateTokenRenderSize(dfMarkupToken token)
		{
			try
			{
				int num = 0;
				bool flag = token.TokenType == dfMarkupTokenType.Whitespace || token.TokenType == dfMarkupTokenType.Text;
				dfDynamicFont dfDynamicFont = (dfDynamicFont)base.Font;
				if (flag)
				{
					int size = Mathf.CeilToInt((float)dfDynamicFont.FontSize * base.TextScale);
					CharacterInfo[] array = dfDynamicFont.RequestCharacters(token.Value, size, 0);
					for (int i = 0; i < token.Length; i++)
					{
						char c = token[i];
						if (c == '\t')
						{
							num += base.TabSize;
						}
						else
						{
							CharacterInfo characterInfo = array[i];
							num += ((c == ' ') ? Mathf.CeilToInt(characterInfo.width) : Mathf.CeilToInt(characterInfo.vert.x + characterInfo.vert.width));
							if (i > 0)
							{
								num += Mathf.CeilToInt((float)base.CharacterSpacing * base.TextScale);
							}
						}
					}
					token.Height = base.Font.LineHeight;
					token.Width = num;
				}
				else if (token.TokenType == dfMarkupTokenType.StartTag && token.Matches("sprite") && this.SpriteAtlas != null && token.AttributeCount == 1)
				{
					Texture2D texture = this.SpriteAtlas.Texture;
					float num2 = (float)dfDynamicFont.Baseline * base.TextScale;
					string value = token.GetAttribute(0).Value.Value;
					dfAtlas.ItemInfo itemInfo = this.SpriteAtlas[value];
					if (itemInfo != null)
					{
						float num3 = itemInfo.region.width * (float)texture.width / (itemInfo.region.height * (float)texture.height);
						num = Mathf.CeilToInt(num2 * num3);
					}
					token.Height = Mathf.CeilToInt(num2);
					token.Width = num;
				}
			}
			finally
			{
			}
		}

		// Token: 0x0600424A RID: 16970 RVA: 0x00100400 File Offset: 0x000FE600
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

		// Token: 0x0600424B RID: 16971 RVA: 0x001004A4 File Offset: 0x000FE6A4
		private void clipRight(dfRenderData destination, int startIndex)
		{
			if (destination == null)
			{
				return;
			}
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

		// Token: 0x0600424C RID: 16972 RVA: 0x00100698 File Offset: 0x000FE898
		private void clipBottom(dfRenderData destination, int startIndex)
		{
			if (destination == null)
			{
				return;
			}
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
					uv[i + 3] = Vector2.Lerp(uv[i + 3], uv[i], num3);
					uv[i + 2] = Vector2.Lerp(uv[i + 2], uv[i + 1], num3);
					Color color = Color.Lerp(colors[i + 3], colors[i], num3);
					colors[i + 3] = color;
					colors[i + 2] = color;
				}
			}
		}

		// Token: 0x0600424D RID: 16973 RVA: 0x001008B0 File Offset: 0x000FEAB0
		private Color32 applyOpacity(Color32 color)
		{
			color.a = (byte)(base.Opacity * 255f);
			return color;
		}

		// Token: 0x0600424E RID: 16974 RVA: 0x001008C8 File Offset: 0x000FEAC8
		private static void addTriangleIndices(dfList<Vector3> verts, dfList<int> triangles)
		{
			int count = verts.Count;
			int[] triangle_INDICES = dfDynamicFont.DynamicFontRenderer.TRIANGLE_INDICES;
			for (int i = 0; i < triangle_INDICES.Length; i++)
			{
				triangles.Add(count + triangle_INDICES[i]);
			}
		}

		// Token: 0x0600424F RID: 16975 RVA: 0x00100904 File Offset: 0x000FEB04
		private Color multiplyColors(Color lhs, Color rhs)
		{
			return new Color(lhs.r * rhs.r, lhs.g * rhs.g, lhs.b * rhs.b, lhs.a * rhs.a);
		}

		// Token: 0x040022D7 RID: 8919
		private static Queue<dfDynamicFont.DynamicFontRenderer> objectPool = new Queue<dfDynamicFont.DynamicFontRenderer>();

		// Token: 0x040022D8 RID: 8920
		private static Vector2[] OUTLINE_OFFSETS = new Vector2[]
		{
			new Vector2(-1f, -1f),
			new Vector2(-1f, 1f),
			new Vector2(1f, -1f),
			new Vector2(1f, 1f)
		};

		// Token: 0x040022D9 RID: 8921
		private static int[] TRIANGLE_INDICES = new int[]
		{
			0,
			1,
			3,
			3,
			1,
			2
		};

		// Token: 0x040022DA RID: 8922
		private static Stack<Color32> textColors = new Stack<Color32>();

		// Token: 0x040022DB RID: 8923
		private dfList<dfDynamicFont.LineRenderInfo> lines;

		// Token: 0x040022DC RID: 8924
		private List<dfMarkupToken> tokens;
	}

	// Token: 0x02000711 RID: 1809
	private class LineRenderInfo
	{
		// Token: 0x06004250 RID: 16976 RVA: 0x00100954 File Offset: 0x000FEB54
		private LineRenderInfo()
		{
		}

		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x06004252 RID: 16978 RVA: 0x00100970 File Offset: 0x000FEB70
		public int length
		{
			get
			{
				return this.endOffset - this.startOffset + 1;
			}
		}

		// Token: 0x06004253 RID: 16979 RVA: 0x00100984 File Offset: 0x000FEB84
		public static void ResetPool()
		{
			dfDynamicFont.LineRenderInfo.poolIndex = 0;
		}

		// Token: 0x06004254 RID: 16980 RVA: 0x0010098C File Offset: 0x000FEB8C
		public static dfDynamicFont.LineRenderInfo Obtain(int start, int end)
		{
			if (dfDynamicFont.LineRenderInfo.poolIndex >= dfDynamicFont.LineRenderInfo.pool.Count - 1)
			{
				dfDynamicFont.LineRenderInfo.pool.Add(new dfDynamicFont.LineRenderInfo());
			}
			dfDynamicFont.LineRenderInfo lineRenderInfo = dfDynamicFont.LineRenderInfo.pool[dfDynamicFont.LineRenderInfo.poolIndex++];
			lineRenderInfo.startOffset = start;
			lineRenderInfo.endOffset = end;
			lineRenderInfo.lineHeight = 0f;
			return lineRenderInfo;
		}

		// Token: 0x040022DF RID: 8927
		public int startOffset;

		// Token: 0x040022E0 RID: 8928
		public int endOffset;

		// Token: 0x040022E1 RID: 8929
		public float lineWidth;

		// Token: 0x040022E2 RID: 8930
		public float lineHeight;

		// Token: 0x040022E3 RID: 8931
		private static dfList<dfDynamicFont.LineRenderInfo> pool = new dfList<dfDynamicFont.LineRenderInfo>();

		// Token: 0x040022E4 RID: 8932
		private static int poolIndex = 0;
	}
}
