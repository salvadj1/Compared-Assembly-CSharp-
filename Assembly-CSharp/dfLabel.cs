using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006D9 RID: 1753
[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider))]
[AddComponentMenu("Daikon Forge/User Interface/Label")]
[Serializable]
public class dfLabel : dfControl, IDFMultiRender
{
	// Token: 0x1400004B RID: 75
	// (add) Token: 0x06003DFA RID: 15866 RVA: 0x000EAAB8 File Offset: 0x000E8CB8
	// (remove) Token: 0x06003DFB RID: 15867 RVA: 0x000EAAD4 File Offset: 0x000E8CD4
	public event PropertyChangedEventHandler<string> TextChanged;

	// Token: 0x17000C1A RID: 3098
	// (get) Token: 0x06003DFC RID: 15868 RVA: 0x000EAAF0 File Offset: 0x000E8CF0
	// (set) Token: 0x06003DFD RID: 15869 RVA: 0x000EAB38 File Offset: 0x000E8D38
	public dfAtlas Atlas
	{
		get
		{
			if (this.atlas == null)
			{
				dfGUIManager manager = base.GetManager();
				if (manager != null)
				{
					return this.atlas = manager.DefaultAtlas;
				}
			}
			return this.atlas;
		}
		set
		{
			if (!dfAtlas.Equals(value, this.atlas))
			{
				this.atlas = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000C1B RID: 3099
	// (get) Token: 0x06003DFE RID: 15870 RVA: 0x000EAB58 File Offset: 0x000E8D58
	// (set) Token: 0x06003DFF RID: 15871 RVA: 0x000EAB9C File Offset: 0x000E8D9C
	public dfFontBase Font
	{
		get
		{
			if (this.font == null)
			{
				dfGUIManager manager = base.GetManager();
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

	// Token: 0x17000C1C RID: 3100
	// (get) Token: 0x06003E00 RID: 15872 RVA: 0x000EABBC File Offset: 0x000E8DBC
	// (set) Token: 0x06003E01 RID: 15873 RVA: 0x000EABC4 File Offset: 0x000E8DC4
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

	// Token: 0x17000C1D RID: 3101
	// (get) Token: 0x06003E02 RID: 15874 RVA: 0x000EABE4 File Offset: 0x000E8DE4
	// (set) Token: 0x06003E03 RID: 15875 RVA: 0x000EABEC File Offset: 0x000E8DEC
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

	// Token: 0x17000C1E RID: 3102
	// (get) Token: 0x06003E04 RID: 15876 RVA: 0x000EAC24 File Offset: 0x000E8E24
	// (set) Token: 0x06003E05 RID: 15877 RVA: 0x000EAC2C File Offset: 0x000E8E2C
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

	// Token: 0x17000C1F RID: 3103
	// (get) Token: 0x06003E06 RID: 15878 RVA: 0x000EAC5C File Offset: 0x000E8E5C
	// (set) Token: 0x06003E07 RID: 15879 RVA: 0x000EAC64 File Offset: 0x000E8E64
	public dfTextScaleMode TextScaleMode
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

	// Token: 0x17000C20 RID: 3104
	// (get) Token: 0x06003E08 RID: 15880 RVA: 0x000EAC74 File Offset: 0x000E8E74
	// (set) Token: 0x06003E09 RID: 15881 RVA: 0x000EAC7C File Offset: 0x000E8E7C
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

	// Token: 0x17000C21 RID: 3105
	// (get) Token: 0x06003E0A RID: 15882 RVA: 0x000EACAC File Offset: 0x000E8EAC
	// (set) Token: 0x06003E0B RID: 15883 RVA: 0x000EACB4 File Offset: 0x000E8EB4
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

	// Token: 0x17000C22 RID: 3106
	// (get) Token: 0x06003E0C RID: 15884 RVA: 0x000EACD0 File Offset: 0x000E8ED0
	// (set) Token: 0x06003E0D RID: 15885 RVA: 0x000EACD8 File Offset: 0x000E8ED8
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

	// Token: 0x17000C23 RID: 3107
	// (get) Token: 0x06003E0E RID: 15886 RVA: 0x000EACF4 File Offset: 0x000E8EF4
	// (set) Token: 0x06003E0F RID: 15887 RVA: 0x000EACFC File Offset: 0x000E8EFC
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

	// Token: 0x17000C24 RID: 3108
	// (get) Token: 0x06003E10 RID: 15888 RVA: 0x000EAD18 File Offset: 0x000E8F18
	// (set) Token: 0x06003E11 RID: 15889 RVA: 0x000EAD20 File Offset: 0x000E8F20
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

	// Token: 0x17000C25 RID: 3109
	// (get) Token: 0x06003E12 RID: 15890 RVA: 0x000EAD58 File Offset: 0x000E8F58
	// (set) Token: 0x06003E13 RID: 15891 RVA: 0x000EAD60 File Offset: 0x000E8F60
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

	// Token: 0x17000C26 RID: 3110
	// (get) Token: 0x06003E14 RID: 15892 RVA: 0x000EADB4 File Offset: 0x000E8FB4
	// (set) Token: 0x06003E15 RID: 15893 RVA: 0x000EADBC File Offset: 0x000E8FBC
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

	// Token: 0x17000C27 RID: 3111
	// (get) Token: 0x06003E16 RID: 15894 RVA: 0x000EADF0 File Offset: 0x000E8FF0
	// (set) Token: 0x06003E17 RID: 15895 RVA: 0x000EAE0C File Offset: 0x000E900C
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

	// Token: 0x17000C28 RID: 3112
	// (get) Token: 0x06003E18 RID: 15896 RVA: 0x000EAE40 File Offset: 0x000E9040
	// (set) Token: 0x06003E19 RID: 15897 RVA: 0x000EAE48 File Offset: 0x000E9048
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

	// Token: 0x17000C29 RID: 3113
	// (get) Token: 0x06003E1A RID: 15898 RVA: 0x000EAE64 File Offset: 0x000E9064
	// (set) Token: 0x06003E1B RID: 15899 RVA: 0x000EAE6C File Offset: 0x000E906C
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

	// Token: 0x17000C2A RID: 3114
	// (get) Token: 0x06003E1C RID: 15900 RVA: 0x000EAE88 File Offset: 0x000E9088
	// (set) Token: 0x06003E1D RID: 15901 RVA: 0x000EAE90 File Offset: 0x000E9090
	public dfVerticalAlignment VerticalAlignment
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

	// Token: 0x17000C2B RID: 3115
	// (get) Token: 0x06003E1E RID: 15902 RVA: 0x000EAEAC File Offset: 0x000E90AC
	// (set) Token: 0x06003E1F RID: 15903 RVA: 0x000EAEB4 File Offset: 0x000E90B4
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

	// Token: 0x17000C2C RID: 3116
	// (get) Token: 0x06003E20 RID: 15904 RVA: 0x000EAED0 File Offset: 0x000E90D0
	// (set) Token: 0x06003E21 RID: 15905 RVA: 0x000EAED8 File Offset: 0x000E90D8
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

	// Token: 0x17000C2D RID: 3117
	// (get) Token: 0x06003E22 RID: 15906 RVA: 0x000EAF08 File Offset: 0x000E9108
	// (set) Token: 0x06003E23 RID: 15907 RVA: 0x000EAF10 File Offset: 0x000E9110
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

	// Token: 0x17000C2E RID: 3118
	// (get) Token: 0x06003E24 RID: 15908 RVA: 0x000EAF48 File Offset: 0x000E9148
	// (set) Token: 0x06003E25 RID: 15909 RVA: 0x000EAF50 File Offset: 0x000E9150
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

	// Token: 0x17000C2F RID: 3119
	// (get) Token: 0x06003E26 RID: 15910 RVA: 0x000EAF6C File Offset: 0x000E916C
	// (set) Token: 0x06003E27 RID: 15911 RVA: 0x000EAF74 File Offset: 0x000E9174
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

	// Token: 0x17000C30 RID: 3120
	// (get) Token: 0x06003E28 RID: 15912 RVA: 0x000EAFAC File Offset: 0x000E91AC
	// (set) Token: 0x06003E29 RID: 15913 RVA: 0x000EAFB4 File Offset: 0x000E91B4
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

	// Token: 0x17000C31 RID: 3121
	// (get) Token: 0x06003E2A RID: 15914 RVA: 0x000EAFD4 File Offset: 0x000E91D4
	// (set) Token: 0x06003E2B RID: 15915 RVA: 0x000EAFF4 File Offset: 0x000E91F4
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

	// Token: 0x17000C32 RID: 3122
	// (get) Token: 0x06003E2C RID: 15916 RVA: 0x000EB028 File Offset: 0x000E9228
	// (set) Token: 0x06003E2D RID: 15917 RVA: 0x000EB030 File Offset: 0x000E9230
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

	// Token: 0x17000C33 RID: 3123
	// (get) Token: 0x06003E2E RID: 15918 RVA: 0x000EB060 File Offset: 0x000E9260
	public List<int> TabStops
	{
		get
		{
			return this.tabStops;
		}
	}

	// Token: 0x06003E2F RID: 15919 RVA: 0x000EB068 File Offset: 0x000E9268
	protected internal override void OnLocalize()
	{
		base.OnLocalize();
		this.Text = base.getLocalizedValue(this.text);
	}

	// Token: 0x06003E30 RID: 15920 RVA: 0x000EB084 File Offset: 0x000E9284
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

	// Token: 0x06003E31 RID: 15921 RVA: 0x000EB0D0 File Offset: 0x000E92D0
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

	// Token: 0x06003E32 RID: 15922 RVA: 0x000EB150 File Offset: 0x000E9350
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

	// Token: 0x06003E33 RID: 15923 RVA: 0x000EB198 File Offset: 0x000E9398
	public override void Awake()
	{
		base.Awake();
		this.startSize = ((!Application.isPlaying) ? Vector2.zero : base.Size);
	}

	// Token: 0x06003E34 RID: 15924 RVA: 0x000EB1CC File Offset: 0x000E93CC
	public override Vector2 CalculateMinimumSize()
	{
		if (this.Font != null)
		{
			float num = (float)this.Font.FontSize * this.TextScale * 0.75f;
			return Vector2.Max(base.CalculateMinimumSize(), new Vector2(num, num));
		}
		return base.CalculateMinimumSize();
	}

	// Token: 0x06003E35 RID: 15925 RVA: 0x000EB220 File Offset: 0x000E9420
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
		using (dfFontRendererBase dfFontRendererBase = this.obtainRenderer())
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

	// Token: 0x06003E36 RID: 15926 RVA: 0x000EB3A4 File Offset: 0x000E95A4
	private dfFontRendererBase obtainRenderer()
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
		dfFontRendererBase dfFontRendererBase = this.Font.ObtainRenderer();
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
		dfDynamicFont.DynamicFontRenderer dynamicFontRenderer = dfFontRendererBase as dfDynamicFont.DynamicFontRenderer;
		if (dynamicFontRenderer != null)
		{
			dynamicFontRenderer.SpriteAtlas = this.Atlas;
			dynamicFontRenderer.SpriteBuffer = this.renderData;
		}
		if (this.vertAlign != dfVerticalAlignment.Top)
		{
			dfFontRendererBase.VectorOffset = this.getVertAlignOffset(dfFontRendererBase);
		}
		return dfFontRendererBase;
	}

	// Token: 0x06003E37 RID: 15927 RVA: 0x000EB62C File Offset: 0x000E982C
	private float getTextScaleMultiplier()
	{
		if (this.textScaleMode == dfTextScaleMode.None || !Application.isPlaying)
		{
			return 1f;
		}
		if (this.textScaleMode == dfTextScaleMode.ScreenResolution)
		{
			return (float)Screen.height / (float)this.manager.FixedHeight;
		}
		if (this.autoSize)
		{
			return 1f;
		}
		return base.Size.y / this.startSize.y;
	}

	// Token: 0x06003E38 RID: 15928 RVA: 0x000EB6A0 File Offset: 0x000E98A0
	private Vector2 getAutoSizeDefault()
	{
		float num = (this.maxSize.x <= float.Epsilon) ? 2.14748365E+09f : this.maxSize.x;
		float num2 = (this.maxSize.y <= float.Epsilon) ? 2.14748365E+09f : this.maxSize.y;
		Vector2 result;
		result..ctor(num, num2);
		return result;
	}

	// Token: 0x06003E39 RID: 15929 RVA: 0x000EB710 File Offset: 0x000E9910
	private Vector3 getVertAlignOffset(dfFontRendererBase textRenderer)
	{
		float num = base.PixelsToUnits();
		Vector2 vector = textRenderer.MeasureString(this.text) * num;
		Vector3 vectorOffset = textRenderer.VectorOffset;
		float num2 = (base.Height - (float)this.padding.vertical) * num;
		if (vector.y >= num2)
		{
			return vectorOffset;
		}
		dfVerticalAlignment dfVerticalAlignment = this.vertAlign;
		if (dfVerticalAlignment != dfVerticalAlignment.Middle)
		{
			if (dfVerticalAlignment == dfVerticalAlignment.Bottom)
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

	// Token: 0x06003E3A RID: 15930 RVA: 0x000EB7C0 File Offset: 0x000E99C0
	protected internal virtual void renderBackground()
	{
		if (this.Atlas == null)
		{
			return;
		}
		dfAtlas.ItemInfo itemInfo = this.Atlas[this.backgroundSprite];
		if (itemInfo == null)
		{
			return;
		}
		Color32 color = base.ApplyOpacity(this.BackgroundColor);
		dfSprite.RenderOptions options = new dfSprite.RenderOptions
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
			dfSprite.renderSprite(this.renderData, options);
		}
		else
		{
			dfSlicedSprite.renderSprite(this.renderData, options);
		}
	}

	// Token: 0x06003E3B RID: 15931 RVA: 0x000EB8B0 File Offset: 0x000E9AB0
	public dfList<dfRenderData> RenderMultiple()
	{
		dfList<dfRenderData> result;
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
					this.renderData = dfRenderData.Obtain();
					this.textRenderData = dfRenderData.Obtain();
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
						using (dfFontRendererBase dfFontRendererBase = this.obtainRenderer())
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

	// Token: 0x040020A4 RID: 8356
	[SerializeField]
	protected dfAtlas atlas;

	// Token: 0x040020A5 RID: 8357
	[SerializeField]
	protected dfFontBase font;

	// Token: 0x040020A6 RID: 8358
	[SerializeField]
	protected string backgroundSprite;

	// Token: 0x040020A7 RID: 8359
	[SerializeField]
	protected Color32 backgroundColor = UnityEngine.Color.white;

	// Token: 0x040020A8 RID: 8360
	[SerializeField]
	protected bool autoSize;

	// Token: 0x040020A9 RID: 8361
	[SerializeField]
	protected bool autoHeight;

	// Token: 0x040020AA RID: 8362
	[SerializeField]
	protected bool wordWrap;

	// Token: 0x040020AB RID: 8363
	[SerializeField]
	protected string text = "Label";

	// Token: 0x040020AC RID: 8364
	[SerializeField]
	protected Color32 bottomColor = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

	// Token: 0x040020AD RID: 8365
	[SerializeField]
	protected TextAlignment align;

	// Token: 0x040020AE RID: 8366
	[SerializeField]
	protected dfVerticalAlignment vertAlign;

	// Token: 0x040020AF RID: 8367
	[SerializeField]
	protected float textScale = 1f;

	// Token: 0x040020B0 RID: 8368
	[SerializeField]
	protected dfTextScaleMode textScaleMode;

	// Token: 0x040020B1 RID: 8369
	[SerializeField]
	protected int charSpacing;

	// Token: 0x040020B2 RID: 8370
	[SerializeField]
	protected bool colorizeSymbols;

	// Token: 0x040020B3 RID: 8371
	[SerializeField]
	protected bool processMarkup;

	// Token: 0x040020B4 RID: 8372
	[SerializeField]
	protected bool outline;

	// Token: 0x040020B5 RID: 8373
	[SerializeField]
	protected int outlineWidth = 1;

	// Token: 0x040020B6 RID: 8374
	[SerializeField]
	protected bool enableGradient;

	// Token: 0x040020B7 RID: 8375
	[SerializeField]
	protected Color32 outlineColor = UnityEngine.Color.black;

	// Token: 0x040020B8 RID: 8376
	[SerializeField]
	protected bool shadow;

	// Token: 0x040020B9 RID: 8377
	[SerializeField]
	protected Color32 shadowColor = UnityEngine.Color.black;

	// Token: 0x040020BA RID: 8378
	[SerializeField]
	protected Vector2 shadowOffset = new Vector2(1f, -1f);

	// Token: 0x040020BB RID: 8379
	[SerializeField]
	protected RectOffset padding = new RectOffset();

	// Token: 0x040020BC RID: 8380
	[SerializeField]
	protected int tabSize = 48;

	// Token: 0x040020BD RID: 8381
	[SerializeField]
	protected List<int> tabStops = new List<int>();

	// Token: 0x040020BE RID: 8382
	private Vector2 startSize = Vector2.zero;

	// Token: 0x040020BF RID: 8383
	private dfRenderData textRenderData;

	// Token: 0x040020C0 RID: 8384
	private dfList<dfRenderData> buffers = dfList<dfRenderData>.Obtain();
}
