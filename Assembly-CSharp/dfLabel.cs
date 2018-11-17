using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007AB RID: 1963
[RequireComponent(typeof(BoxCollider))]
[AddComponentMenu("Daikon Forge/User Interface/Label")]
[ExecuteInEditMode]
[Serializable]
public class dfLabel : global::dfControl, global::IDFMultiRender
{
	// Token: 0x1400004B RID: 75
	// (add) Token: 0x06004216 RID: 16918 RVA: 0x000F36BC File Offset: 0x000F18BC
	// (remove) Token: 0x06004217 RID: 16919 RVA: 0x000F36D8 File Offset: 0x000F18D8
	public event global::PropertyChangedEventHandler<string> TextChanged;

	// Token: 0x17000C9E RID: 3230
	// (get) Token: 0x06004218 RID: 16920 RVA: 0x000F36F4 File Offset: 0x000F18F4
	// (set) Token: 0x06004219 RID: 16921 RVA: 0x000F373C File Offset: 0x000F193C
	public global::dfAtlas Atlas
	{
		get
		{
			if (this.atlas == null)
			{
				global::dfGUIManager manager = base.GetManager();
				if (manager != null)
				{
					return this.atlas = manager.DefaultAtlas;
				}
			}
			return this.atlas;
		}
		set
		{
			if (!global::dfAtlas.Equals(value, this.atlas))
			{
				this.atlas = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000C9F RID: 3231
	// (get) Token: 0x0600421A RID: 16922 RVA: 0x000F375C File Offset: 0x000F195C
	// (set) Token: 0x0600421B RID: 16923 RVA: 0x000F37A0 File Offset: 0x000F19A0
	public global::dfFontBase Font
	{
		get
		{
			if (this.font == null)
			{
				global::dfGUIManager manager = base.GetManager();
				if (manager != null)
				{
					this.font = manager.DefaultFont;
				}
			}
			return this.font;
		}
		set
		{
			if (value != this.font)
			{
				this.font = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CA0 RID: 3232
	// (get) Token: 0x0600421C RID: 16924 RVA: 0x000F37C0 File Offset: 0x000F19C0
	// (set) Token: 0x0600421D RID: 16925 RVA: 0x000F37C8 File Offset: 0x000F19C8
	public string BackgroundSprite
	{
		get
		{
			return this.backgroundSprite;
		}
		set
		{
			if (value != this.backgroundSprite)
			{
				this.backgroundSprite = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CA1 RID: 3233
	// (get) Token: 0x0600421E RID: 16926 RVA: 0x000F37E8 File Offset: 0x000F19E8
	// (set) Token: 0x0600421F RID: 16927 RVA: 0x000F37F0 File Offset: 0x000F19F0
	public Color32 BackgroundColor
	{
		get
		{
			return this.backgroundColor;
		}
		set
		{
			if (!object.Equals(value, this.backgroundColor))
			{
				this.backgroundColor = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CA2 RID: 3234
	// (get) Token: 0x06004220 RID: 16928 RVA: 0x000F3828 File Offset: 0x000F1A28
	// (set) Token: 0x06004221 RID: 16929 RVA: 0x000F3830 File Offset: 0x000F1A30
	public float TextScale
	{
		get
		{
			return this.textScale;
		}
		set
		{
			value = Mathf.Max(0.1f, value);
			if (!Mathf.Approximately(this.textScale, value))
			{
				this.textScale = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CA3 RID: 3235
	// (get) Token: 0x06004222 RID: 16930 RVA: 0x000F3860 File Offset: 0x000F1A60
	// (set) Token: 0x06004223 RID: 16931 RVA: 0x000F3868 File Offset: 0x000F1A68
	public global::dfTextScaleMode TextScaleMode
	{
		get
		{
			return this.textScaleMode;
		}
		set
		{
			this.textScaleMode = value;
			this.Invalidate();
		}
	}

	// Token: 0x17000CA4 RID: 3236
	// (get) Token: 0x06004224 RID: 16932 RVA: 0x000F3878 File Offset: 0x000F1A78
	// (set) Token: 0x06004225 RID: 16933 RVA: 0x000F3880 File Offset: 0x000F1A80
	public int CharacterSpacing
	{
		get
		{
			return this.charSpacing;
		}
		set
		{
			value = Mathf.Max(0, value);
			if (value != this.charSpacing)
			{
				this.charSpacing = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CA5 RID: 3237
	// (get) Token: 0x06004226 RID: 16934 RVA: 0x000F38B0 File Offset: 0x000F1AB0
	// (set) Token: 0x06004227 RID: 16935 RVA: 0x000F38B8 File Offset: 0x000F1AB8
	public bool ColorizeSymbols
	{
		get
		{
			return this.colorizeSymbols;
		}
		set
		{
			if (value != this.colorizeSymbols)
			{
				this.colorizeSymbols = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CA6 RID: 3238
	// (get) Token: 0x06004228 RID: 16936 RVA: 0x000F38D4 File Offset: 0x000F1AD4
	// (set) Token: 0x06004229 RID: 16937 RVA: 0x000F38DC File Offset: 0x000F1ADC
	public bool ProcessMarkup
	{
		get
		{
			return this.processMarkup;
		}
		set
		{
			if (value != this.processMarkup)
			{
				this.processMarkup = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CA7 RID: 3239
	// (get) Token: 0x0600422A RID: 16938 RVA: 0x000F38F8 File Offset: 0x000F1AF8
	// (set) Token: 0x0600422B RID: 16939 RVA: 0x000F3900 File Offset: 0x000F1B00
	public bool ShowGradient
	{
		get
		{
			return this.enableGradient;
		}
		set
		{
			if (value != this.enableGradient)
			{
				this.enableGradient = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CA8 RID: 3240
	// (get) Token: 0x0600422C RID: 16940 RVA: 0x000F391C File Offset: 0x000F1B1C
	// (set) Token: 0x0600422D RID: 16941 RVA: 0x000F3924 File Offset: 0x000F1B24
	public Color32 BottomColor
	{
		get
		{
			return this.bottomColor;
		}
		set
		{
			if (!this.bottomColor.Equals(value))
			{
				this.bottomColor = value;
				this.OnColorChanged();
			}
		}
	}

	// Token: 0x17000CA9 RID: 3241
	// (get) Token: 0x0600422E RID: 16942 RVA: 0x000F395C File Offset: 0x000F1B5C
	// (set) Token: 0x0600422F RID: 16943 RVA: 0x000F3964 File Offset: 0x000F1B64
	public string Text
	{
		get
		{
			return this.text;
		}
		set
		{
			value = value.Replace("\\t", "\t").Replace("\\n", "\n");
			if (!string.Equals(value, this.text))
			{
				this.text = base.getLocalizedValue(value);
				this.OnTextChanged();
			}
		}
	}

	// Token: 0x17000CAA RID: 3242
	// (get) Token: 0x06004230 RID: 16944 RVA: 0x000F39B8 File Offset: 0x000F1BB8
	// (set) Token: 0x06004231 RID: 16945 RVA: 0x000F39C0 File Offset: 0x000F1BC0
	public bool AutoSize
	{
		get
		{
			return this.autoSize;
		}
		set
		{
			if (value != this.autoSize)
			{
				if (value)
				{
					this.autoHeight = false;
				}
				this.autoSize = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CAB RID: 3243
	// (get) Token: 0x06004232 RID: 16946 RVA: 0x000F39F4 File Offset: 0x000F1BF4
	// (set) Token: 0x06004233 RID: 16947 RVA: 0x000F3A10 File Offset: 0x000F1C10
	public bool AutoHeight
	{
		get
		{
			return this.autoHeight && !this.autoSize;
		}
		set
		{
			if (value != this.autoHeight)
			{
				if (value)
				{
					this.autoSize = false;
				}
				this.autoHeight = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CAC RID: 3244
	// (get) Token: 0x06004234 RID: 16948 RVA: 0x000F3A44 File Offset: 0x000F1C44
	// (set) Token: 0x06004235 RID: 16949 RVA: 0x000F3A4C File Offset: 0x000F1C4C
	public bool WordWrap
	{
		get
		{
			return this.wordWrap;
		}
		set
		{
			if (value != this.wordWrap)
			{
				this.wordWrap = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CAD RID: 3245
	// (get) Token: 0x06004236 RID: 16950 RVA: 0x000F3A68 File Offset: 0x000F1C68
	// (set) Token: 0x06004237 RID: 16951 RVA: 0x000F3A70 File Offset: 0x000F1C70
	public TextAlignment TextAlignment
	{
		get
		{
			return this.align;
		}
		set
		{
			if (value != this.align)
			{
				this.align = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CAE RID: 3246
	// (get) Token: 0x06004238 RID: 16952 RVA: 0x000F3A8C File Offset: 0x000F1C8C
	// (set) Token: 0x06004239 RID: 16953 RVA: 0x000F3A94 File Offset: 0x000F1C94
	public global::dfVerticalAlignment VerticalAlignment
	{
		get
		{
			return this.vertAlign;
		}
		set
		{
			if (value != this.vertAlign)
			{
				this.vertAlign = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CAF RID: 3247
	// (get) Token: 0x0600423A RID: 16954 RVA: 0x000F3AB0 File Offset: 0x000F1CB0
	// (set) Token: 0x0600423B RID: 16955 RVA: 0x000F3AB8 File Offset: 0x000F1CB8
	public bool Outline
	{
		get
		{
			return this.outline;
		}
		set
		{
			if (value != this.outline)
			{
				this.outline = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CB0 RID: 3248
	// (get) Token: 0x0600423C RID: 16956 RVA: 0x000F3AD4 File Offset: 0x000F1CD4
	// (set) Token: 0x0600423D RID: 16957 RVA: 0x000F3ADC File Offset: 0x000F1CDC
	public int OutlineSize
	{
		get
		{
			return this.outlineWidth;
		}
		set
		{
			value = Mathf.Max(0, value);
			if (value != this.outlineWidth)
			{
				this.outlineWidth = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CB1 RID: 3249
	// (get) Token: 0x0600423E RID: 16958 RVA: 0x000F3B0C File Offset: 0x000F1D0C
	// (set) Token: 0x0600423F RID: 16959 RVA: 0x000F3B14 File Offset: 0x000F1D14
	public Color32 OutlineColor
	{
		get
		{
			return this.outlineColor;
		}
		set
		{
			if (!value.Equals(this.outlineColor))
			{
				this.outlineColor = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CB2 RID: 3250
	// (get) Token: 0x06004240 RID: 16960 RVA: 0x000F3B4C File Offset: 0x000F1D4C
	// (set) Token: 0x06004241 RID: 16961 RVA: 0x000F3B54 File Offset: 0x000F1D54
	public bool Shadow
	{
		get
		{
			return this.shadow;
		}
		set
		{
			if (value != this.shadow)
			{
				this.shadow = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CB3 RID: 3251
	// (get) Token: 0x06004242 RID: 16962 RVA: 0x000F3B70 File Offset: 0x000F1D70
	// (set) Token: 0x06004243 RID: 16963 RVA: 0x000F3B78 File Offset: 0x000F1D78
	public Color32 ShadowColor
	{
		get
		{
			return this.shadowColor;
		}
		set
		{
			if (!value.Equals(this.shadowColor))
			{
				this.shadowColor = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CB4 RID: 3252
	// (get) Token: 0x06004244 RID: 16964 RVA: 0x000F3BB0 File Offset: 0x000F1DB0
	// (set) Token: 0x06004245 RID: 16965 RVA: 0x000F3BB8 File Offset: 0x000F1DB8
	public Vector2 ShadowOffset
	{
		get
		{
			return this.shadowOffset;
		}
		set
		{
			if (value != this.shadowOffset)
			{
				this.shadowOffset = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CB5 RID: 3253
	// (get) Token: 0x06004246 RID: 16966 RVA: 0x000F3BD8 File Offset: 0x000F1DD8
	// (set) Token: 0x06004247 RID: 16967 RVA: 0x000F3BF8 File Offset: 0x000F1DF8
	public RectOffset Padding
	{
		get
		{
			if (this.padding == null)
			{
				this.padding = new RectOffset();
			}
			return this.padding;
		}
		set
		{
			value = value.ConstrainPadding();
			if (!object.Equals(value, this.padding))
			{
				this.padding = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CB6 RID: 3254
	// (get) Token: 0x06004248 RID: 16968 RVA: 0x000F3C2C File Offset: 0x000F1E2C
	// (set) Token: 0x06004249 RID: 16969 RVA: 0x000F3C34 File Offset: 0x000F1E34
	public int TabSize
	{
		get
		{
			return this.tabSize;
		}
		set
		{
			value = Mathf.Max(0, value);
			if (value != this.tabSize)
			{
				this.tabSize = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CB7 RID: 3255
	// (get) Token: 0x0600424A RID: 16970 RVA: 0x000F3C64 File Offset: 0x000F1E64
	public List<int> TabStops
	{
		get
		{
			return this.tabStops;
		}
	}

	// Token: 0x0600424B RID: 16971 RVA: 0x000F3C6C File Offset: 0x000F1E6C
	protected internal override void OnLocalize()
	{
		base.OnLocalize();
		this.Text = base.getLocalizedValue(this.text);
	}

	// Token: 0x0600424C RID: 16972 RVA: 0x000F3C88 File Offset: 0x000F1E88
	protected internal void OnTextChanged()
	{
		this.Invalidate();
		base.Signal("OnTextChanged", new object[]
		{
			this.text
		});
		if (this.TextChanged != null)
		{
			this.TextChanged(this, this.text);
		}
	}

	// Token: 0x0600424D RID: 16973 RVA: 0x000F3CD4 File Offset: 0x000F1ED4
	public override void OnEnable()
	{
		base.OnEnable();
		bool flag = this.Font != null && this.Font.IsValid;
		if (Application.isPlaying && !flag)
		{
			this.Font = base.GetManager().DefaultFont;
		}
		if (this.size.sqrMagnitude <= 1.401298E-45f)
		{
			base.Size = new Vector2(150f, 25f);
		}
	}

	// Token: 0x0600424E RID: 16974 RVA: 0x000F3D54 File Offset: 0x000F1F54
	public override void Update()
	{
		if (this.autoSize)
		{
			this.autoHeight = false;
		}
		if (this.Font == null)
		{
			this.Font = base.GetManager().DefaultFont;
		}
		base.Update();
	}

	// Token: 0x0600424F RID: 16975 RVA: 0x000F3D9C File Offset: 0x000F1F9C
	public override void Awake()
	{
		base.Awake();
		this.startSize = ((!Application.isPlaying) ? Vector2.zero : base.Size);
	}

	// Token: 0x06004250 RID: 16976 RVA: 0x000F3DD0 File Offset: 0x000F1FD0
	public override Vector2 CalculateMinimumSize()
	{
		if (this.Font != null)
		{
			float num = (float)this.Font.FontSize * this.TextScale * 0.75f;
			return Vector2.Max(base.CalculateMinimumSize(), new Vector2(num, num));
		}
		return base.CalculateMinimumSize();
	}

	// Token: 0x06004251 RID: 16977 RVA: 0x000F3E24 File Offset: 0x000F2024
	public override void Invalidate()
	{
		base.Invalidate();
		if (this.Font == null || !this.Font.IsValid)
		{
			return;
		}
		bool flag = this.size.sqrMagnitude <= float.Epsilon;
		if (!this.autoSize && !this.autoHeight && !flag)
		{
			return;
		}
		if (string.IsNullOrEmpty(this.Text))
		{
			if (flag)
			{
				base.Size = new Vector2(150f, 24f);
			}
			if (this.AutoSize || this.AutoHeight)
			{
				base.Height = (float)Mathf.CeilToInt((float)this.Font.LineHeight * this.TextScale);
			}
			return;
		}
		using (global::dfFontRendererBase dfFontRendererBase = this.obtainRenderer())
		{
			Vector2 vector = dfFontRendererBase.MeasureString(this.text).RoundToInt();
			if (this.AutoSize || flag)
			{
				this.size = vector + new Vector2((float)this.padding.horizontal, (float)this.padding.vertical);
			}
			else if (this.AutoHeight)
			{
				this.size = new Vector2(this.size.x, vector.y + (float)this.padding.vertical);
			}
		}
	}

	// Token: 0x06004252 RID: 16978 RVA: 0x000F3FA8 File Offset: 0x000F21A8
	private global::dfFontRendererBase obtainRenderer()
	{
		bool flag = base.Size.sqrMagnitude <= float.Epsilon;
		Vector2 vector = base.Size - new Vector2((float)this.padding.horizontal, (float)this.padding.vertical);
		Vector2 maxSize = (!this.autoSize && !flag) ? vector : this.getAutoSizeDefault();
		if (this.autoHeight)
		{
			maxSize..ctor(vector.x, 2.14748365E+09f);
		}
		float num = base.PixelsToUnits();
		Vector3 vector2 = (this.pivot.TransformToUpperLeft(base.Size) + new Vector3((float)this.padding.left, (float)(-(float)this.padding.top))) * num;
		float num2 = this.TextScale * this.getTextScaleMultiplier();
		global::dfFontRendererBase dfFontRendererBase = this.Font.ObtainRenderer();
		dfFontRendererBase.WordWrap = this.WordWrap;
		dfFontRendererBase.MaxSize = maxSize;
		dfFontRendererBase.PixelRatio = num;
		dfFontRendererBase.TextScale = num2;
		dfFontRendererBase.CharacterSpacing = this.CharacterSpacing;
		dfFontRendererBase.VectorOffset = vector2.Quantize(num);
		dfFontRendererBase.MultiLine = true;
		dfFontRendererBase.TabSize = this.TabSize;
		dfFontRendererBase.TabStops = this.TabStops;
		dfFontRendererBase.TextAlign = ((!this.autoSize) ? this.TextAlignment : 0);
		dfFontRendererBase.ColorizeSymbols = this.ColorizeSymbols;
		dfFontRendererBase.ProcessMarkup = this.ProcessMarkup;
		dfFontRendererBase.DefaultColor = ((!base.IsEnabled) ? base.DisabledColor : base.Color);
		dfFontRendererBase.BottomColor = ((!this.enableGradient) ? null : new Color32?(this.BottomColor));
		dfFontRendererBase.OverrideMarkupColors = !base.IsEnabled;
		dfFontRendererBase.Opacity = base.CalculateOpacity();
		dfFontRendererBase.Outline = this.Outline;
		dfFontRendererBase.OutlineSize = this.OutlineSize;
		dfFontRendererBase.OutlineColor = this.OutlineColor;
		dfFontRendererBase.Shadow = this.Shadow;
		dfFontRendererBase.ShadowColor = this.ShadowColor;
		dfFontRendererBase.ShadowOffset = this.ShadowOffset;
		global::dfDynamicFont.DynamicFontRenderer dynamicFontRenderer = dfFontRendererBase as global::dfDynamicFont.DynamicFontRenderer;
		if (dynamicFontRenderer != null)
		{
			dynamicFontRenderer.SpriteAtlas = this.Atlas;
			dynamicFontRenderer.SpriteBuffer = this.renderData;
		}
		if (this.vertAlign != global::dfVerticalAlignment.Top)
		{
			dfFontRendererBase.VectorOffset = this.getVertAlignOffset(dfFontRendererBase);
		}
		return dfFontRendererBase;
	}

	// Token: 0x06004253 RID: 16979 RVA: 0x000F4230 File Offset: 0x000F2430
	private float getTextScaleMultiplier()
	{
		if (this.textScaleMode == global::dfTextScaleMode.None || !Application.isPlaying)
		{
			return 1f;
		}
		if (this.textScaleMode == global::dfTextScaleMode.ScreenResolution)
		{
			return (float)Screen.height / (float)this.manager.FixedHeight;
		}
		if (this.autoSize)
		{
			return 1f;
		}
		return base.Size.y / this.startSize.y;
	}

	// Token: 0x06004254 RID: 16980 RVA: 0x000F42A4 File Offset: 0x000F24A4
	private Vector2 getAutoSizeDefault()
	{
		float num = (this.maxSize.x <= float.Epsilon) ? 2.14748365E+09f : this.maxSize.x;
		float num2 = (this.maxSize.y <= float.Epsilon) ? 2.14748365E+09f : this.maxSize.y;
		Vector2 result;
		result..ctor(num, num2);
		return result;
	}

	// Token: 0x06004255 RID: 16981 RVA: 0x000F4314 File Offset: 0x000F2514
	private Vector3 getVertAlignOffset(global::dfFontRendererBase textRenderer)
	{
		float num = base.PixelsToUnits();
		Vector2 vector = textRenderer.MeasureString(this.text) * num;
		Vector3 vectorOffset = textRenderer.VectorOffset;
		float num2 = (base.Height - (float)this.padding.vertical) * num;
		if (vector.y >= num2)
		{
			return vectorOffset;
		}
		global::dfVerticalAlignment dfVerticalAlignment = this.vertAlign;
		if (dfVerticalAlignment != global::dfVerticalAlignment.Middle)
		{
			if (dfVerticalAlignment == global::dfVerticalAlignment.Bottom)
			{
				vectorOffset.y -= num2 - vector.y;
			}
		}
		else
		{
			vectorOffset.y -= (num2 - vector.y) * 0.5f;
		}
		return vectorOffset;
	}

	// Token: 0x06004256 RID: 16982 RVA: 0x000F43C4 File Offset: 0x000F25C4
	protected internal virtual void renderBackground()
	{
		if (this.Atlas == null)
		{
			return;
		}
		global::dfAtlas.ItemInfo itemInfo = this.Atlas[this.backgroundSprite];
		if (itemInfo == null)
		{
			return;
		}
		Color32 color = base.ApplyOpacity(this.BackgroundColor);
		global::dfSprite.RenderOptions options = new global::dfSprite.RenderOptions
		{
			atlas = this.atlas,
			color = color,
			fillAmount = 1f,
			offset = this.pivot.TransformToUpperLeft(base.Size),
			pixelsToUnits = base.PixelsToUnits(),
			size = base.Size,
			spriteInfo = itemInfo
		};
		if (itemInfo.border.horizontal == 0 && itemInfo.border.vertical == 0)
		{
			global::dfSprite.renderSprite(this.renderData, options);
		}
		else
		{
			global::dfSlicedSprite.renderSprite(this.renderData, options);
		}
	}

	// Token: 0x06004257 RID: 16983 RVA: 0x000F44B4 File Offset: 0x000F26B4
	public global::dfList<global::dfRenderData> RenderMultiple()
	{
		global::dfList<global::dfRenderData> result;
		try
		{
			if (this.Atlas == null || this.Font == null || !this.isVisible || !this.Font.IsValid)
			{
				result = null;
			}
			else
			{
				if (this.renderData == null)
				{
					this.renderData = global::dfRenderData.Obtain();
					this.textRenderData = global::dfRenderData.Obtain();
					this.isControlInvalidated = true;
				}
				if (!this.isControlInvalidated)
				{
					for (int i = 0; i < this.buffers.Count; i++)
					{
						this.buffers[i].Transform = base.transform.localToWorldMatrix;
					}
					result = this.buffers;
				}
				else
				{
					this.buffers.Clear();
					this.renderData.Clear();
					this.renderData.Material = this.Atlas.Material;
					this.renderData.Transform = base.transform.localToWorldMatrix;
					this.buffers.Add(this.renderData);
					this.textRenderData.Clear();
					this.textRenderData.Material = this.Atlas.Material;
					this.textRenderData.Transform = base.transform.localToWorldMatrix;
					this.buffers.Add(this.textRenderData);
					this.renderBackground();
					if (string.IsNullOrEmpty(this.Text))
					{
						if (this.AutoSize || this.AutoHeight)
						{
							base.Height = (float)Mathf.CeilToInt((float)this.Font.LineHeight * this.TextScale);
						}
						result = this.buffers;
					}
					else
					{
						bool flag = this.size.sqrMagnitude <= float.Epsilon;
						using (global::dfFontRendererBase dfFontRendererBase = this.obtainRenderer())
						{
							dfFontRendererBase.Render(this.text, this.textRenderData);
							if (this.AutoSize || flag)
							{
								base.Size = (dfFontRendererBase.RenderedSize + new Vector2((float)this.padding.horizontal, (float)this.padding.vertical)).CeilToInt();
							}
							else if (this.AutoHeight)
							{
								base.Size = new Vector2(this.size.x, dfFontRendererBase.RenderedSize.y + (float)this.padding.vertical).CeilToInt();
							}
						}
						this.updateCollider();
						result = this.buffers;
					}
				}
			}
		}
		finally
		{
			this.isControlInvalidated = false;
		}
		return result;
	}

	// Token: 0x040022AD RID: 8877
	[SerializeField]
	protected global::dfAtlas atlas;

	// Token: 0x040022AE RID: 8878
	[SerializeField]
	protected global::dfFontBase font;

	// Token: 0x040022AF RID: 8879
	[SerializeField]
	protected string backgroundSprite;

	// Token: 0x040022B0 RID: 8880
	[SerializeField]
	protected Color32 backgroundColor = UnityEngine.Color.white;

	// Token: 0x040022B1 RID: 8881
	[SerializeField]
	protected bool autoSize;

	// Token: 0x040022B2 RID: 8882
	[SerializeField]
	protected bool autoHeight;

	// Token: 0x040022B3 RID: 8883
	[SerializeField]
	protected bool wordWrap;

	// Token: 0x040022B4 RID: 8884
	[SerializeField]
	protected string text = "Label";

	// Token: 0x040022B5 RID: 8885
	[SerializeField]
	protected Color32 bottomColor = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

	// Token: 0x040022B6 RID: 8886
	[SerializeField]
	protected TextAlignment align;

	// Token: 0x040022B7 RID: 8887
	[SerializeField]
	protected global::dfVerticalAlignment vertAlign;

	// Token: 0x040022B8 RID: 8888
	[SerializeField]
	protected float textScale = 1f;

	// Token: 0x040022B9 RID: 8889
	[SerializeField]
	protected global::dfTextScaleMode textScaleMode;

	// Token: 0x040022BA RID: 8890
	[SerializeField]
	protected int charSpacing;

	// Token: 0x040022BB RID: 8891
	[SerializeField]
	protected bool colorizeSymbols;

	// Token: 0x040022BC RID: 8892
	[SerializeField]
	protected bool processMarkup;

	// Token: 0x040022BD RID: 8893
	[SerializeField]
	protected bool outline;

	// Token: 0x040022BE RID: 8894
	[SerializeField]
	protected int outlineWidth = 1;

	// Token: 0x040022BF RID: 8895
	[SerializeField]
	protected bool enableGradient;

	// Token: 0x040022C0 RID: 8896
	[SerializeField]
	protected Color32 outlineColor = UnityEngine.Color.black;

	// Token: 0x040022C1 RID: 8897
	[SerializeField]
	protected bool shadow;

	// Token: 0x040022C2 RID: 8898
	[SerializeField]
	protected Color32 shadowColor = UnityEngine.Color.black;

	// Token: 0x040022C3 RID: 8899
	[SerializeField]
	protected Vector2 shadowOffset = new Vector2(1f, -1f);

	// Token: 0x040022C4 RID: 8900
	[SerializeField]
	protected RectOffset padding = new RectOffset();

	// Token: 0x040022C5 RID: 8901
	[SerializeField]
	protected int tabSize = 48;

	// Token: 0x040022C6 RID: 8902
	[SerializeField]
	protected List<int> tabStops = new List<int>();

	// Token: 0x040022C7 RID: 8903
	private Vector2 startSize = Vector2.zero;

	// Token: 0x040022C8 RID: 8904
	private global::dfRenderData textRenderData;

	// Token: 0x040022C9 RID: 8905
	private global::dfList<global::dfRenderData> buffers = global::dfList<global::dfRenderData>.Obtain();
}
