using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

// Token: 0x020007EA RID: 2026
[ExecuteInEditMode]
[AddComponentMenu("Daikon Forge/User Interface/Dynamic Font")]
[Serializable]
public class dfDynamicFont : global::dfFontBase
{
	// Token: 0x17000D87 RID: 3463
	// (get) Token: 0x0600465D RID: 18013 RVA: 0x00107F28 File Offset: 0x00106128
	// (set) Token: 0x0600465E RID: 18014 RVA: 0x00107F4C File Offset: 0x0010614C
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
				global::dfGUIManager.RefreshAll(false);
			}
		}
	}

	// Token: 0x17000D88 RID: 3464
	// (get) Token: 0x0600465F RID: 18015 RVA: 0x00107F6C File Offset: 0x0010616C
	public override Texture Texture
	{
		get
		{
			return this.baseFont.material.mainTexture;
		}
	}

	// Token: 0x17000D89 RID: 3465
	// (get) Token: 0x06004660 RID: 18016 RVA: 0x00107F80 File Offset: 0x00106180
	public override bool IsValid
	{
		get
		{
			return this.baseFont != null && this.Material != null && this.Texture != null;
		}
	}

	// Token: 0x17000D8A RID: 3466
	// (get) Token: 0x06004661 RID: 18017 RVA: 0x00107FC0 File Offset: 0x001061C0
	// (set) Token: 0x06004662 RID: 18018 RVA: 0x00107FC8 File Offset: 0x001061C8
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
				global::dfGUIManager.RefreshAll(false);
			}
		}
	}

	// Token: 0x17000D8B RID: 3467
	// (get) Token: 0x06004663 RID: 18019 RVA: 0x00107FE4 File Offset: 0x001061E4
	// (set) Token: 0x06004664 RID: 18020 RVA: 0x00107FEC File Offset: 0x001061EC
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
				global::dfGUIManager.RefreshAll(false);
			}
		}
	}

	// Token: 0x06004665 RID: 18021 RVA: 0x00108008 File Offset: 0x00106208
	public override global::dfFontRendererBase ObtainRenderer()
	{
		return global::dfDynamicFont.DynamicFontRenderer.Obtain(this);
	}

	// Token: 0x17000D8C RID: 3468
	// (get) Token: 0x06004666 RID: 18022 RVA: 0x00108010 File Offset: 0x00106210
	// (set) Token: 0x06004667 RID: 18023 RVA: 0x00108018 File Offset: 0x00106218
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
				global::dfGUIManager.RefreshAll(false);
			}
		}
	}

	// Token: 0x17000D8D RID: 3469
	// (get) Token: 0x06004668 RID: 18024 RVA: 0x00108038 File Offset: 0x00106238
	// (set) Token: 0x06004669 RID: 18025 RVA: 0x00108040 File Offset: 0x00106240
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
				global::dfGUIManager.RefreshAll(false);
			}
		}
	}

	// Token: 0x17000D8E RID: 3470
	// (get) Token: 0x0600466A RID: 18026 RVA: 0x0010805C File Offset: 0x0010625C
	public int Descent
	{
		get
		{
			return this.LineHeight - this.baseline;
		}
	}

	// Token: 0x0600466B RID: 18027 RVA: 0x0010806C File Offset: 0x0010626C
	public static global::dfDynamicFont FindByName(string name)
	{
		for (int i = 0; i < global::dfDynamicFont.loadedFonts.Count; i++)
		{
			if (string.Equals(global::dfDynamicFont.loadedFonts[i].name, name, StringComparison.InvariantCultureIgnoreCase))
			{
				return global::dfDynamicFont.loadedFonts[i];
			}
		}
		GameObject gameObject = global::Resources.Load(name) as GameObject;
		if (gameObject == null)
		{
			return null;
		}
		global::dfDynamicFont component = gameObject.GetComponent<global::dfDynamicFont>();
		if (component == null)
		{
			return null;
		}
		global::dfDynamicFont.loadedFonts.Add(component);
		return component;
	}

	// Token: 0x0600466C RID: 18028 RVA: 0x001080F8 File Offset: 0x001062F8
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

	// Token: 0x0600466D RID: 18029 RVA: 0x001081D4 File Offset: 0x001063D4
	public CharacterInfo[] RequestCharacters(string text, int size, FontStyle style)
	{
		if (this.baseFont == null)
		{
			throw new NullReferenceException("Base Font not assigned: " + base.name);
		}
		this.ensureGlyphBufferCapacity(size);
		if (!global::dfDynamicFont.loadedFonts.Contains(this))
		{
			Font font = this.baseFont;
			font.textureRebuildCallback = (Font.FontTextureRebuildCallback)Delegate.Combine(font.textureRebuildCallback, new Font.FontTextureRebuildCallback(this.onFontAtlasRebuilt));
			global::dfDynamicFont.loadedFonts.Add(this);
		}
		this.baseFont.RequestCharactersInTexture(text, size, style);
		this.getGlyphData(global::dfDynamicFont.glyphBuffer, text, size, style);
		return global::dfDynamicFont.glyphBuffer;
	}

	// Token: 0x0600466E RID: 18030 RVA: 0x00108274 File Offset: 0x00106474
	private void onFontAtlasRebuilt()
	{
		this.wasFontAtlasRebuilt = true;
		this.OnFontChanged();
	}

	// Token: 0x0600466F RID: 18031 RVA: 0x00108284 File Offset: 0x00106484
	private void OnFontChanged()
	{
		try
		{
			if (!this.invalidatingDependentControls)
			{
				global::dfGUIManager.RenderCallback callback = null;
				callback = delegate(global::dfGUIManager manager)
				{
					global::dfGUIManager.AfterRender -= callback;
					this.invalidatingDependentControls = true;
					try
					{
						if (this.wasFontAtlasRebuilt)
						{
						}
						List<global::dfControl> list = (from global::dfControl x in 
							from x in Object.FindObjectsOfType(typeof(global::dfControl))
							where x is global::IDFMultiRender
							select x
						orderby x.RenderOrder
						select x).ToList<global::dfControl>();
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
				global::dfGUIManager.AfterRender += callback;
			}
		}
		finally
		{
		}
	}

	// Token: 0x06004670 RID: 18032 RVA: 0x001082F4 File Offset: 0x001064F4
	private void ensureGlyphBufferCapacity(int size)
	{
		int i = global::dfDynamicFont.glyphBuffer.Length;
		if (size < i)
		{
			return;
		}
		while (i < size)
		{
			i += 1024;
		}
		global::dfDynamicFont.glyphBuffer = new CharacterInfo[i];
	}

	// Token: 0x06004671 RID: 18033 RVA: 0x00108330 File Offset: 0x00106530
	private void getGlyphData(CharacterInfo[] result, string text, int size, FontStyle style)
	{
		if (text.Length > global::dfDynamicFont.glyphBuffer.Length)
		{
			global::dfDynamicFont.glyphBuffer = new CharacterInfo[text.Length + 512];
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

	// Token: 0x040024ED RID: 9453
	private static List<global::dfDynamicFont> loadedFonts = new List<global::dfDynamicFont>();

	// Token: 0x040024EE RID: 9454
	private static CharacterInfo[] glyphBuffer = new CharacterInfo[1024];

	// Token: 0x040024EF RID: 9455
	[SerializeField]
	private Font baseFont;

	// Token: 0x040024F0 RID: 9456
	[SerializeField]
	private Material material;

	// Token: 0x040024F1 RID: 9457
	[SerializeField]
	private int baseFontSize = -1;

	// Token: 0x040024F2 RID: 9458
	[SerializeField]
	private int baseline = -1;

	// Token: 0x040024F3 RID: 9459
	[SerializeField]
	private int lineHeight;

	// Token: 0x040024F4 RID: 9460
	private bool invalidatingDependentControls;

	// Token: 0x040024F5 RID: 9461
	private bool wasFontAtlasRebuilt;

	// Token: 0x020007EB RID: 2027
	public class DynamicFontRenderer : global::dfFontRendererBase
	{
		// Token: 0x06004672 RID: 18034 RVA: 0x001083DC File Offset: 0x001065DC
		internal DynamicFontRenderer()
		{
		}

		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x06004674 RID: 18036 RVA: 0x00108494 File Offset: 0x00106694
		public int LineCount
		{
			get
			{
				return this.lines.Count;
			}
		}

		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x06004675 RID: 18037 RVA: 0x001084A4 File Offset: 0x001066A4
		// (set) Token: 0x06004676 RID: 18038 RVA: 0x001084AC File Offset: 0x001066AC
		public global::dfAtlas SpriteAtlas { get; set; }

		// Token: 0x17000D91 RID: 3473
		// (get) Token: 0x06004677 RID: 18039 RVA: 0x001084B8 File Offset: 0x001066B8
		// (set) Token: 0x06004678 RID: 18040 RVA: 0x001084C0 File Offset: 0x001066C0
		public global::dfRenderData SpriteBuffer { get; set; }

		// Token: 0x06004679 RID: 18041 RVA: 0x001084CC File Offset: 0x001066CC
		public static global::dfFontRendererBase Obtain(global::dfDynamicFont font)
		{
			global::dfDynamicFont.DynamicFontRenderer dynamicFontRenderer = (global::dfDynamicFont.DynamicFontRenderer.objectPool.Count <= 0) ? new global::dfDynamicFont.DynamicFontRenderer() : global::dfDynamicFont.DynamicFontRenderer.objectPool.Dequeue();
			dynamicFontRenderer.Reset();
			dynamicFontRenderer.Font = font;
			return dynamicFontRenderer;
		}

		// Token: 0x0600467A RID: 18042 RVA: 0x0010850C File Offset: 0x0010670C
		public override void Release()
		{
			this.Reset();
			this.tokens = null;
			if (this.lines != null)
			{
				this.lines.Release();
				this.lines = null;
			}
			global::dfDynamicFont.LineRenderInfo.ResetPool();
			base.BottomColor = null;
			global::dfDynamicFont.DynamicFontRenderer.objectPool.Enqueue(this);
		}

		// Token: 0x0600467B RID: 18043 RVA: 0x00108564 File Offset: 0x00106764
		public override float[] GetCharacterWidths(string text)
		{
			float num = 0f;
			return this.GetCharacterWidths(text, 0, text.Length - 1, out num);
		}

		// Token: 0x0600467C RID: 18044 RVA: 0x0010858C File Offset: 0x0010678C
		public float[] GetCharacterWidths(string text, int startIndex, int endIndex, out float totalWidth)
		{
			totalWidth = 0f;
			global::dfDynamicFont dfDynamicFont = (global::dfDynamicFont)base.Font;
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

		// Token: 0x0600467D RID: 18045 RVA: 0x00108680 File Offset: 0x00106880
		public override Vector2 MeasureString(string text)
		{
			this.tokenize(text);
			global::dfList<global::dfDynamicFont.LineRenderInfo> dfList = this.calculateLinebreaks();
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

		// Token: 0x0600467E RID: 18046 RVA: 0x001086EC File Offset: 0x001068EC
		public override void Render(string text, global::dfRenderData destination)
		{
			global::dfDynamicFont.DynamicFontRenderer.textColors.Clear();
			global::dfDynamicFont.DynamicFontRenderer.textColors.Push(Color.white);
			this.tokenize(text);
			global::dfList<global::dfDynamicFont.LineRenderInfo> dfList = this.calculateLinebreaks();
			int num = 0;
			int num2 = 0;
			Vector3 position = (base.VectorOffset / base.PixelRatio).CeilToInt();
			for (int i = 0; i < dfList.Count; i++)
			{
				global::dfDynamicFont.LineRenderInfo lineRenderInfo = dfList[i];
				int count = destination.Vertices.Count;
				int startIndex = (this.SpriteBuffer == null) ? 0 : this.SpriteBuffer.Vertices.Count;
				this.renderLine(dfList[i], global::dfDynamicFont.DynamicFontRenderer.textColors, position, destination);
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

		// Token: 0x0600467F RID: 18047 RVA: 0x0010886C File Offset: 0x00106A6C
		private void renderLine(global::dfDynamicFont.LineRenderInfo line, Stack<Color32> colors, Vector3 position, global::dfRenderData destination)
		{
			position.x += (float)this.calculateLineAlignment(line);
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
					if (dfMarkupToken.Matches("sprite") && this.SpriteAtlas != null && this.SpriteBuffer != null)
					{
						this.renderSprite(dfMarkupToken, colors.Peek(), position, this.SpriteBuffer);
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
				position.x += (float)dfMarkupToken.Width;
			}
		}

		// Token: 0x06004680 RID: 18048 RVA: 0x00108988 File Offset: 0x00106B88
		private void renderText(global::dfMarkupToken token, Color32 color, Vector3 position, global::dfRenderData renderData)
		{
			try
			{
				global::dfDynamicFont dfDynamicFont = (global::dfDynamicFont)base.Font;
				int num = Mathf.CeilToInt((float)dfDynamicFont.FontSize * base.TextScale);
				FontStyle style = 0;
				int descent = dfDynamicFont.Descent;
				global::dfList<Vector3> vertices = renderData.Vertices;
				global::dfList<int> triangles = renderData.Triangles;
				global::dfList<Vector2> uv = renderData.UV;
				global::dfList<Color32> colors = renderData.Colors;
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
						global::dfDynamicFont.DynamicFontRenderer.addTriangleIndices(vertices, triangles);
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
						global::dfDynamicFont.DynamicFontRenderer.addUVCoords(uv, glyph);
					}
					if (base.Outline)
					{
						for (int j = 0; j < global::dfDynamicFont.DynamicFontRenderer.OUTLINE_OFFSETS.Length; j++)
						{
							global::dfDynamicFont.DynamicFontRenderer.addTriangleIndices(vertices, triangles);
							Vector3 vector6 = global::dfDynamicFont.DynamicFontRenderer.OUTLINE_OFFSETS[j] * (float)base.OutlineSize * base.PixelRatio;
							vertices.Add(vector + vector6);
							vertices.Add(vector2 + vector6);
							vertices.Add(vector3 + vector6);
							vertices.Add(vector4 + vector6);
							Color32 item3 = this.applyOpacity(base.OutlineColor);
							colors.Add(item3);
							colors.Add(item3);
							colors.Add(item3);
							colors.Add(item3);
							global::dfDynamicFont.DynamicFontRenderer.addUVCoords(uv, glyph);
						}
					}
					global::dfDynamicFont.DynamicFontRenderer.addTriangleIndices(vertices, triangles);
					vertices.Add(vector);
					vertices.Add(vector2);
					vertices.Add(vector3);
					vertices.Add(vector4);
					colors.Add(color2);
					colors.Add(color2);
					colors.Add(item);
					colors.Add(item);
					global::dfDynamicFont.DynamicFontRenderer.addUVCoords(uv, glyph);
					num2 += (float)Mathf.CeilToInt(glyph.vert.x + glyph.vert.width);
				}
			}
			finally
			{
			}
		}

		// Token: 0x06004681 RID: 18049 RVA: 0x00108D94 File Offset: 0x00106F94
		private static void addUVCoords(global::dfList<Vector2> uvs, CharacterInfo glyph)
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

		// Token: 0x06004682 RID: 18050 RVA: 0x00108E54 File Offset: 0x00107054
		private void renderSprite(global::dfMarkupToken token, Color32 color, Vector3 position, global::dfRenderData destination)
		{
			try
			{
				string value = token.GetAttribute(0).Value.Value;
				global::dfAtlas.ItemInfo itemInfo = this.SpriteAtlas[value];
				if (!(itemInfo == null))
				{
					global::dfSprite.RenderOptions options = new global::dfSprite.RenderOptions
					{
						atlas = this.SpriteAtlas,
						color = color,
						fillAmount = 1f,
						offset = position,
						pixelsToUnits = base.PixelRatio,
						size = new Vector2((float)token.Width, (float)token.Height),
						spriteInfo = itemInfo
					};
					global::dfSprite.renderSprite(this.SpriteBuffer, options);
				}
			}
			finally
			{
			}
		}

		// Token: 0x06004683 RID: 18051 RVA: 0x00108F24 File Offset: 0x00107124
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

		// Token: 0x06004684 RID: 18052 RVA: 0x00108FBC File Offset: 0x001071BC
		private Color32 UIntToColor(uint color)
		{
			byte b = (byte)(color >> 24);
			byte b2 = (byte)(color >> 16);
			byte b3 = (byte)(color >> 8);
			byte b4 = (byte)color;
			return new Color32(b2, b3, b4, b);
		}

		// Token: 0x06004685 RID: 18053 RVA: 0x00108FE8 File Offset: 0x001071E8
		private global::dfList<global::dfDynamicFont.LineRenderInfo> calculateLinebreaks()
		{
			global::dfList<global::dfDynamicFont.LineRenderInfo> result;
			try
			{
				if (this.lines != null)
				{
					result = this.lines;
				}
				else
				{
					this.lines = global::dfList<global::dfDynamicFont.LineRenderInfo>.Obtain();
					global::dfDynamicFont dfDynamicFont = (global::dfDynamicFont)base.Font;
					int num = 0;
					int num2 = 0;
					int num3 = 0;
					int num4 = 0;
					float num5 = (float)dfDynamicFont.Baseline * base.TextScale;
					while (num3 < this.tokens.Count && (float)this.lines.Count * num5 <= base.MaxSize.y + num5)
					{
						global::dfMarkupToken dfMarkupToken = this.tokens[num3];
						global::dfMarkupTokenType tokenType = dfMarkupToken.TokenType;
						if (tokenType == global::dfMarkupTokenType.Newline)
						{
							this.lines.Add(global::dfDynamicFont.LineRenderInfo.Obtain(num2, num3));
							num = (num2 = ++num3);
							num4 = 0;
						}
						else
						{
							int num6 = Mathf.CeilToInt((float)dfMarkupToken.Width);
							bool flag = base.WordWrap && num > num2 && (tokenType == global::dfMarkupTokenType.Text || (tokenType == global::dfMarkupTokenType.StartTag && dfMarkupToken.Matches("sprite")));
							if (flag && (float)(num4 + num6) >= base.MaxSize.x)
							{
								if (num > num2)
								{
									this.lines.Add(global::dfDynamicFont.LineRenderInfo.Obtain(num2, num - 1));
									num3 = (num2 = ++num);
									num4 = 0;
								}
								else
								{
									this.lines.Add(global::dfDynamicFont.LineRenderInfo.Obtain(num2, num - 1));
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
						this.lines.Add(global::dfDynamicFont.LineRenderInfo.Obtain(num2, this.tokens.Count - 1));
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

		// Token: 0x06004686 RID: 18054 RVA: 0x0010921C File Offset: 0x0010741C
		private int calculateLineAlignment(global::dfDynamicFont.LineRenderInfo line)
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

		// Token: 0x06004687 RID: 18055 RVA: 0x00109298 File Offset: 0x00107498
		private void calculateLineSize(global::dfDynamicFont.LineRenderInfo line)
		{
			global::dfDynamicFont dfDynamicFont = (global::dfDynamicFont)base.Font;
			line.lineHeight = (float)dfDynamicFont.Baseline * base.TextScale;
			int num = 0;
			for (int i = line.startOffset; i <= line.endOffset; i++)
			{
				num += this.tokens[i].Width;
			}
			line.lineWidth = (float)num;
		}

		// Token: 0x06004688 RID: 18056 RVA: 0x00109300 File Offset: 0x00107500
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

		// Token: 0x06004689 RID: 18057 RVA: 0x001093D8 File Offset: 0x001075D8
		private void calculateTokenRenderSize(global::dfMarkupToken token)
		{
			try
			{
				int num = 0;
				bool flag = token.TokenType == global::dfMarkupTokenType.Whitespace || token.TokenType == global::dfMarkupTokenType.Text;
				global::dfDynamicFont dfDynamicFont = (global::dfDynamicFont)base.Font;
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
				else if (token.TokenType == global::dfMarkupTokenType.StartTag && token.Matches("sprite") && this.SpriteAtlas != null && token.AttributeCount == 1)
				{
					Texture2D texture = this.SpriteAtlas.Texture;
					float num2 = (float)dfDynamicFont.Baseline * base.TextScale;
					string value = token.GetAttribute(0).Value.Value;
					global::dfAtlas.ItemInfo itemInfo = this.SpriteAtlas[value];
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

		// Token: 0x0600468A RID: 18058 RVA: 0x001095EC File Offset: 0x001077EC
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

		// Token: 0x0600468B RID: 18059 RVA: 0x00109690 File Offset: 0x00107890
		private void clipRight(global::dfRenderData destination, int startIndex)
		{
			if (destination == null)
			{
				return;
			}
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

		// Token: 0x0600468C RID: 18060 RVA: 0x00109884 File Offset: 0x00107A84
		private void clipBottom(global::dfRenderData destination, int startIndex)
		{
			if (destination == null)
			{
				return;
			}
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
					uv[i + 3] = Vector2.Lerp(uv[i + 3], uv[i], num3);
					uv[i + 2] = Vector2.Lerp(uv[i + 2], uv[i + 1], num3);
					Color color = Color.Lerp(colors[i + 3], colors[i], num3);
					colors[i + 3] = color;
					colors[i + 2] = color;
				}
			}
		}

		// Token: 0x0600468D RID: 18061 RVA: 0x00109A9C File Offset: 0x00107C9C
		private Color32 applyOpacity(Color32 color)
		{
			color.a = (byte)(base.Opacity * 255f);
			return color;
		}

		// Token: 0x0600468E RID: 18062 RVA: 0x00109AB4 File Offset: 0x00107CB4
		private static void addTriangleIndices(global::dfList<Vector3> verts, global::dfList<int> triangles)
		{
			int count = verts.Count;
			int[] triangle_INDICES = global::dfDynamicFont.DynamicFontRenderer.TRIANGLE_INDICES;
			for (int i = 0; i < triangle_INDICES.Length; i++)
			{
				triangles.Add(count + triangle_INDICES[i]);
			}
		}

		// Token: 0x0600468F RID: 18063 RVA: 0x00109AF0 File Offset: 0x00107CF0
		private Color multiplyColors(Color lhs, Color rhs)
		{
			return new Color(lhs.r * rhs.r, lhs.g * rhs.g, lhs.b * rhs.b, lhs.a * rhs.a);
		}

		// Token: 0x040024F6 RID: 9462
		private static Queue<global::dfDynamicFont.DynamicFontRenderer> objectPool = new Queue<global::dfDynamicFont.DynamicFontRenderer>();

		// Token: 0x040024F7 RID: 9463
		private static Vector2[] OUTLINE_OFFSETS = new Vector2[]
		{
			new Vector2(-1f, -1f),
			new Vector2(-1f, 1f),
			new Vector2(1f, -1f),
			new Vector2(1f, 1f)
		};

		// Token: 0x040024F8 RID: 9464
		private static int[] TRIANGLE_INDICES = new int[]
		{
			0,
			1,
			3,
			3,
			1,
			2
		};

		// Token: 0x040024F9 RID: 9465
		private static Stack<Color32> textColors = new Stack<Color32>();

		// Token: 0x040024FA RID: 9466
		private global::dfList<global::dfDynamicFont.LineRenderInfo> lines;

		// Token: 0x040024FB RID: 9467
		private List<global::dfMarkupToken> tokens;
	}

	// Token: 0x020007EC RID: 2028
	private class LineRenderInfo
	{
		// Token: 0x06004690 RID: 18064 RVA: 0x00109B40 File Offset: 0x00107D40
		private LineRenderInfo()
		{
		}

		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x06004692 RID: 18066 RVA: 0x00109B5C File Offset: 0x00107D5C
		public int length
		{
			get
			{
				return this.endOffset - this.startOffset + 1;
			}
		}

		// Token: 0x06004693 RID: 18067 RVA: 0x00109B70 File Offset: 0x00107D70
		public static void ResetPool()
		{
			global::dfDynamicFont.LineRenderInfo.poolIndex = 0;
		}

		// Token: 0x06004694 RID: 18068 RVA: 0x00109B78 File Offset: 0x00107D78
		public static global::dfDynamicFont.LineRenderInfo Obtain(int start, int end)
		{
			if (global::dfDynamicFont.LineRenderInfo.poolIndex >= global::dfDynamicFont.LineRenderInfo.pool.Count - 1)
			{
				global::dfDynamicFont.LineRenderInfo.pool.Add(new global::dfDynamicFont.LineRenderInfo());
			}
			global::dfDynamicFont.LineRenderInfo lineRenderInfo = global::dfDynamicFont.LineRenderInfo.pool[global::dfDynamicFont.LineRenderInfo.poolIndex++];
			lineRenderInfo.startOffset = start;
			lineRenderInfo.endOffset = end;
			lineRenderInfo.lineHeight = 0f;
			return lineRenderInfo;
		}

		// Token: 0x040024FE RID: 9470
		public int startOffset;

		// Token: 0x040024FF RID: 9471
		public int endOffset;

		// Token: 0x04002500 RID: 9472
		public float lineWidth;

		// Token: 0x04002501 RID: 9473
		public float lineHeight;

		// Token: 0x04002502 RID: 9474
		private static global::dfList<global::dfDynamicFont.LineRenderInfo> pool = new global::dfList<global::dfDynamicFont.LineRenderInfo>();

		// Token: 0x04002503 RID: 9475
		private static int poolIndex = 0;
	}
}
