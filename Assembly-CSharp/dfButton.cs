using System;
using UnityEngine;

// Token: 0x020006A0 RID: 1696
[RequireComponent(typeof(BoxCollider))]
[AddComponentMenu("Daikon Forge/User Interface/Button")]
[ExecuteInEditMode]
[Serializable]
public class dfButton : dfInteractiveBase, IDFMultiRender
{
	// Token: 0x14000021 RID: 33
	// (add) Token: 0x06003A9E RID: 15006 RVA: 0x000DBAF8 File Offset: 0x000D9CF8
	// (remove) Token: 0x06003A9F RID: 15007 RVA: 0x000DBB14 File Offset: 0x000D9D14
	public event PropertyChangedEventHandler<dfButton.ButtonState> ButtonStateChanged;

	// Token: 0x17000B55 RID: 2901
	// (get) Token: 0x06003AA0 RID: 15008 RVA: 0x000DBB30 File Offset: 0x000D9D30
	// (set) Token: 0x06003AA1 RID: 15009 RVA: 0x000DBB38 File Offset: 0x000D9D38
	public dfButton.ButtonState State
	{
		get
		{
			return this.state;
		}
		set
		{
			if (value != this.state)
			{
				this.OnButtonStateChanged(value);
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000B56 RID: 2902
	// (get) Token: 0x06003AA2 RID: 15010 RVA: 0x000DBB54 File Offset: 0x000D9D54
	// (set) Token: 0x06003AA3 RID: 15011 RVA: 0x000DBB5C File Offset: 0x000D9D5C
	public string PressedSprite
	{
		get
		{
			return this.pressedSprite;
		}
		set
		{
			if (value != this.pressedSprite)
			{
				this.pressedSprite = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000B57 RID: 2903
	// (get) Token: 0x06003AA4 RID: 15012 RVA: 0x000DBB7C File Offset: 0x000D9D7C
	// (set) Token: 0x06003AA5 RID: 15013 RVA: 0x000DBB84 File Offset: 0x000D9D84
	public dfControl ButtonGroup
	{
		get
		{
			return this.group;
		}
		set
		{
			if (value != this.group)
			{
				this.group = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000B58 RID: 2904
	// (get) Token: 0x06003AA6 RID: 15014 RVA: 0x000DBBA4 File Offset: 0x000D9DA4
	// (set) Token: 0x06003AA7 RID: 15015 RVA: 0x000DBBAC File Offset: 0x000D9DAC
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
				this.autoSize = value;
				if (value)
				{
					this.textAlign = 0;
				}
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000B59 RID: 2905
	// (get) Token: 0x06003AA8 RID: 15016 RVA: 0x000DBBE0 File Offset: 0x000D9DE0
	// (set) Token: 0x06003AA9 RID: 15017 RVA: 0x000DBBF8 File Offset: 0x000D9DF8
	public TextAlignment TextAlignment
	{
		get
		{
			if (this.autoSize)
			{
				return 0;
			}
			return this.textAlign;
		}
		set
		{
			if (value != this.textAlign)
			{
				this.textAlign = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000B5A RID: 2906
	// (get) Token: 0x06003AAA RID: 15018 RVA: 0x000DBC14 File Offset: 0x000D9E14
	// (set) Token: 0x06003AAB RID: 15019 RVA: 0x000DBC1C File Offset: 0x000D9E1C
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

	// Token: 0x17000B5B RID: 2907
	// (get) Token: 0x06003AAC RID: 15020 RVA: 0x000DBC38 File Offset: 0x000D9E38
	// (set) Token: 0x06003AAD RID: 15021 RVA: 0x000DBC58 File Offset: 0x000D9E58
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

	// Token: 0x17000B5C RID: 2908
	// (get) Token: 0x06003AAE RID: 15022 RVA: 0x000DBC8C File Offset: 0x000D9E8C
	// (set) Token: 0x06003AAF RID: 15023 RVA: 0x000DBCD0 File Offset: 0x000D9ED0
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
			}
			this.Invalidate();
		}
	}

	// Token: 0x17000B5D RID: 2909
	// (get) Token: 0x06003AB0 RID: 15024 RVA: 0x000DBCF0 File Offset: 0x000D9EF0
	// (set) Token: 0x06003AB1 RID: 15025 RVA: 0x000DBCF8 File Offset: 0x000D9EF8
	public string Text
	{
		get
		{
			return this.text;
		}
		set
		{
			if (value != this.text)
			{
				this.text = base.getLocalizedValue(value);
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000B5E RID: 2910
	// (get) Token: 0x06003AB2 RID: 15026 RVA: 0x000DBD2C File Offset: 0x000D9F2C
	// (set) Token: 0x06003AB3 RID: 15027 RVA: 0x000DBD34 File Offset: 0x000D9F34
	public Color32 TextColor
	{
		get
		{
			return this.textColor;
		}
		set
		{
			this.textColor = value;
			this.Invalidate();
		}
	}

	// Token: 0x17000B5F RID: 2911
	// (get) Token: 0x06003AB4 RID: 15028 RVA: 0x000DBD44 File Offset: 0x000D9F44
	// (set) Token: 0x06003AB5 RID: 15029 RVA: 0x000DBD4C File Offset: 0x000D9F4C
	public Color32 HoverTextColor
	{
		get
		{
			return this.hoverText;
		}
		set
		{
			this.hoverText = value;
			this.Invalidate();
		}
	}

	// Token: 0x17000B60 RID: 2912
	// (get) Token: 0x06003AB6 RID: 15030 RVA: 0x000DBD5C File Offset: 0x000D9F5C
	// (set) Token: 0x06003AB7 RID: 15031 RVA: 0x000DBD64 File Offset: 0x000D9F64
	public Color32 HoverBackgroundColor
	{
		get
		{
			return this.hoverColor;
		}
		set
		{
			this.hoverColor = value;
			this.Invalidate();
		}
	}

	// Token: 0x17000B61 RID: 2913
	// (get) Token: 0x06003AB8 RID: 15032 RVA: 0x000DBD74 File Offset: 0x000D9F74
	// (set) Token: 0x06003AB9 RID: 15033 RVA: 0x000DBD7C File Offset: 0x000D9F7C
	public Color32 PressedTextColor
	{
		get
		{
			return this.pressedText;
		}
		set
		{
			this.pressedText = value;
			this.Invalidate();
		}
	}

	// Token: 0x17000B62 RID: 2914
	// (get) Token: 0x06003ABA RID: 15034 RVA: 0x000DBD8C File Offset: 0x000D9F8C
	// (set) Token: 0x06003ABB RID: 15035 RVA: 0x000DBD94 File Offset: 0x000D9F94
	public Color32 PressedBackgroundColor
	{
		get
		{
			return this.pressedColor;
		}
		set
		{
			this.pressedColor = value;
			this.Invalidate();
		}
	}

	// Token: 0x17000B63 RID: 2915
	// (get) Token: 0x06003ABC RID: 15036 RVA: 0x000DBDA4 File Offset: 0x000D9FA4
	// (set) Token: 0x06003ABD RID: 15037 RVA: 0x000DBDAC File Offset: 0x000D9FAC
	public Color32 FocusTextColor
	{
		get
		{
			return this.focusText;
		}
		set
		{
			this.focusText = value;
			this.Invalidate();
		}
	}

	// Token: 0x17000B64 RID: 2916
	// (get) Token: 0x06003ABE RID: 15038 RVA: 0x000DBDBC File Offset: 0x000D9FBC
	// (set) Token: 0x06003ABF RID: 15039 RVA: 0x000DBDC4 File Offset: 0x000D9FC4
	public Color32 FocusBackgroundColor
	{
		get
		{
			return this.focusColor;
		}
		set
		{
			this.focusColor = value;
			this.Invalidate();
		}
	}

	// Token: 0x17000B65 RID: 2917
	// (get) Token: 0x06003AC0 RID: 15040 RVA: 0x000DBDD4 File Offset: 0x000D9FD4
	// (set) Token: 0x06003AC1 RID: 15041 RVA: 0x000DBDDC File Offset: 0x000D9FDC
	public Color32 DisabledTextColor
	{
		get
		{
			return this.disabledText;
		}
		set
		{
			this.disabledText = value;
			this.Invalidate();
		}
	}

	// Token: 0x17000B66 RID: 2918
	// (get) Token: 0x06003AC2 RID: 15042 RVA: 0x000DBDEC File Offset: 0x000D9FEC
	// (set) Token: 0x06003AC3 RID: 15043 RVA: 0x000DBDF4 File Offset: 0x000D9FF4
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

	// Token: 0x17000B67 RID: 2919
	// (get) Token: 0x06003AC4 RID: 15044 RVA: 0x000DBE24 File Offset: 0x000DA024
	// (set) Token: 0x06003AC5 RID: 15045 RVA: 0x000DBE2C File Offset: 0x000DA02C
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

	// Token: 0x17000B68 RID: 2920
	// (get) Token: 0x06003AC6 RID: 15046 RVA: 0x000DBE3C File Offset: 0x000DA03C
	// (set) Token: 0x06003AC7 RID: 15047 RVA: 0x000DBE44 File Offset: 0x000DA044
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

	// Token: 0x17000B69 RID: 2921
	// (get) Token: 0x06003AC8 RID: 15048 RVA: 0x000DBE60 File Offset: 0x000DA060
	// (set) Token: 0x06003AC9 RID: 15049 RVA: 0x000DBE68 File Offset: 0x000DA068
	public bool Shadow
	{
		get
		{
			return this.textShadow;
		}
		set
		{
			if (value != this.textShadow)
			{
				this.textShadow = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000B6A RID: 2922
	// (get) Token: 0x06003ACA RID: 15050 RVA: 0x000DBE84 File Offset: 0x000DA084
	// (set) Token: 0x06003ACB RID: 15051 RVA: 0x000DBE8C File Offset: 0x000DA08C
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

	// Token: 0x17000B6B RID: 2923
	// (get) Token: 0x06003ACC RID: 15052 RVA: 0x000DBEC4 File Offset: 0x000DA0C4
	// (set) Token: 0x06003ACD RID: 15053 RVA: 0x000DBECC File Offset: 0x000DA0CC
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

	// Token: 0x06003ACE RID: 15054 RVA: 0x000DBEEC File Offset: 0x000DA0EC
	protected internal override void OnLocalize()
	{
		base.OnLocalize();
		this.Text = base.getLocalizedValue(this.text);
	}

	// Token: 0x06003ACF RID: 15055 RVA: 0x000DBF08 File Offset: 0x000DA108
	public override void Invalidate()
	{
		base.Invalidate();
		if (this.AutoSize)
		{
			this.autoSizeToText();
		}
	}

	// Token: 0x06003AD0 RID: 15056 RVA: 0x000DBF24 File Offset: 0x000DA124
	public override void OnEnable()
	{
		base.OnEnable();
		bool flag = this.Font != null && this.Font.IsValid;
		if (Application.isPlaying && !flag)
		{
			this.Font = base.GetManager().DefaultFont;
		}
	}

	// Token: 0x06003AD1 RID: 15057 RVA: 0x000DBF78 File Offset: 0x000DA178
	public override void Awake()
	{
		base.Awake();
		this.startSize = base.Size;
	}

	// Token: 0x06003AD2 RID: 15058 RVA: 0x000DBF8C File Offset: 0x000DA18C
	public override void Update()
	{
		base.Update();
	}

	// Token: 0x06003AD3 RID: 15059 RVA: 0x000DBF94 File Offset: 0x000DA194
	protected internal override void OnEnterFocus(dfFocusEventArgs args)
	{
		if (this.State != dfButton.ButtonState.Pressed)
		{
			this.State = dfButton.ButtonState.Focus;
		}
		base.OnEnterFocus(args);
	}

	// Token: 0x06003AD4 RID: 15060 RVA: 0x000DBFB0 File Offset: 0x000DA1B0
	protected internal override void OnLeaveFocus(dfFocusEventArgs args)
	{
		this.State = dfButton.ButtonState.Default;
		base.OnLeaveFocus(args);
	}

	// Token: 0x06003AD5 RID: 15061 RVA: 0x000DBFC0 File Offset: 0x000DA1C0
	protected internal override void OnKeyPress(dfKeyEventArgs args)
	{
		if (this.IsInteractive && args.KeyCode == 32)
		{
			this.OnClick(new dfMouseEventArgs(this, dfMouseButtons.Left, 1, default(Ray), Vector2.zero, 0f));
			return;
		}
		base.OnKeyPress(args);
	}

	// Token: 0x06003AD6 RID: 15062 RVA: 0x000DC010 File Offset: 0x000DA210
	protected internal override void OnClick(dfMouseEventArgs args)
	{
		if (this.group != null)
		{
			foreach (dfButton dfButton in base.transform.parent.GetComponentsInChildren<dfButton>())
			{
				if (dfButton != this && dfButton.ButtonGroup == this.ButtonGroup && dfButton != this)
				{
					dfButton.State = dfButton.ButtonState.Default;
				}
			}
			if (!base.transform.IsChildOf(this.group.transform))
			{
				base.Signal(this.group.gameObject, "OnClick", new object[]
				{
					args
				});
			}
		}
		base.OnClick(args);
	}

	// Token: 0x06003AD7 RID: 15063 RVA: 0x000DC0D0 File Offset: 0x000DA2D0
	protected internal override void OnMouseDown(dfMouseEventArgs args)
	{
		if (!(this.parent is dfTabstrip) || this.State != dfButton.ButtonState.Focus)
		{
			this.State = dfButton.ButtonState.Pressed;
		}
		base.OnMouseDown(args);
	}

	// Token: 0x06003AD8 RID: 15064 RVA: 0x000DC108 File Offset: 0x000DA308
	protected internal override void OnMouseUp(dfMouseEventArgs args)
	{
		if (this.isMouseHovering)
		{
			if (this.parent is dfTabstrip && this.ContainsFocus)
			{
				this.State = dfButton.ButtonState.Focus;
			}
			else
			{
				this.State = dfButton.ButtonState.Hover;
			}
		}
		else if (this.HasFocus)
		{
			this.State = dfButton.ButtonState.Focus;
		}
		else
		{
			this.State = dfButton.ButtonState.Default;
		}
		base.OnMouseUp(args);
	}

	// Token: 0x06003AD9 RID: 15065 RVA: 0x000DC178 File Offset: 0x000DA378
	protected internal override void OnMouseEnter(dfMouseEventArgs args)
	{
		if (!(this.parent is dfTabstrip) || this.State != dfButton.ButtonState.Focus)
		{
			this.State = dfButton.ButtonState.Hover;
		}
		base.OnMouseEnter(args);
	}

	// Token: 0x06003ADA RID: 15066 RVA: 0x000DC1B0 File Offset: 0x000DA3B0
	protected internal override void OnMouseLeave(dfMouseEventArgs args)
	{
		if (this.ContainsFocus)
		{
			this.State = dfButton.ButtonState.Focus;
		}
		else
		{
			this.State = dfButton.ButtonState.Default;
		}
		base.OnMouseLeave(args);
	}

	// Token: 0x06003ADB RID: 15067 RVA: 0x000DC1E4 File Offset: 0x000DA3E4
	protected internal override void OnIsEnabledChanged()
	{
		if (!base.IsEnabled)
		{
			this.State = dfButton.ButtonState.Disabled;
		}
		else
		{
			this.State = dfButton.ButtonState.Default;
		}
		base.OnIsEnabledChanged();
	}

	// Token: 0x06003ADC RID: 15068 RVA: 0x000DC218 File Offset: 0x000DA418
	protected virtual void OnButtonStateChanged(dfButton.ButtonState value)
	{
		if (!this.isEnabled && value != dfButton.ButtonState.Disabled)
		{
			return;
		}
		this.state = value;
		base.Signal("OnButtonStateChanged", new object[]
		{
			value
		});
		if (this.ButtonStateChanged != null)
		{
			this.ButtonStateChanged(this, value);
		}
		this.Invalidate();
	}

	// Token: 0x06003ADD RID: 15069 RVA: 0x000DC278 File Offset: 0x000DA478
	protected override Color32 getActiveColor()
	{
		switch (this.State)
		{
		case dfButton.ButtonState.Focus:
			return this.FocusBackgroundColor;
		case dfButton.ButtonState.Hover:
			return this.HoverBackgroundColor;
		case dfButton.ButtonState.Pressed:
			return this.PressedBackgroundColor;
		case dfButton.ButtonState.Disabled:
			return base.DisabledColor;
		default:
			return base.Color;
		}
	}

	// Token: 0x06003ADE RID: 15070 RVA: 0x000DC2CC File Offset: 0x000DA4CC
	private void autoSizeToText()
	{
		if (this.Font == null || !this.Font.IsValid || string.IsNullOrEmpty(this.Text))
		{
			return;
		}
		using (dfFontRendererBase dfFontRendererBase = this.obtainTextRenderer())
		{
			Vector2 vector = dfFontRendererBase.MeasureString(this.Text);
			Vector2 size;
			size..ctor(vector.x + (float)this.padding.horizontal, vector.y + (float)this.padding.vertical);
			base.Size = size;
		}
	}

	// Token: 0x06003ADF RID: 15071 RVA: 0x000DC384 File Offset: 0x000DA584
	private dfRenderData renderText()
	{
		if (this.Font == null || !this.Font.IsValid || string.IsNullOrEmpty(this.Text))
		{
			return null;
		}
		dfRenderData renderData = this.renderData;
		if (this.font is dfDynamicFont)
		{
			dfDynamicFont dfDynamicFont = (dfDynamicFont)this.font;
			renderData = this.textRenderData;
			renderData.Clear();
			renderData.Material = dfDynamicFont.Material;
		}
		using (dfFontRendererBase dfFontRendererBase = this.obtainTextRenderer())
		{
			dfFontRendererBase.Render(this.text, renderData);
		}
		return renderData;
	}

	// Token: 0x06003AE0 RID: 15072 RVA: 0x000DC444 File Offset: 0x000DA644
	private dfFontRendererBase obtainTextRenderer()
	{
		Vector2 vector = base.Size - new Vector2((float)this.padding.horizontal, (float)this.padding.vertical);
		Vector2 maxSize = (!this.autoSize) ? vector : (Vector2.one * 2.14748365E+09f);
		float num = base.PixelsToUnits();
		Vector3 vector2 = (this.pivot.TransformToUpperLeft(base.Size) + new Vector3((float)this.padding.left, (float)(-(float)this.padding.top))) * num;
		float num2 = this.TextScale * this.getTextScaleMultiplier();
		Color32 defaultColor = base.ApplyOpacity(this.getTextColorForState());
		dfFontRendererBase dfFontRendererBase = this.Font.ObtainRenderer();
		dfFontRendererBase.WordWrap = this.WordWrap;
		dfFontRendererBase.MultiLine = this.WordWrap;
		dfFontRendererBase.MaxSize = maxSize;
		dfFontRendererBase.PixelRatio = num;
		dfFontRendererBase.TextScale = num2;
		dfFontRendererBase.CharacterSpacing = 0;
		dfFontRendererBase.VectorOffset = vector2.Quantize(num);
		dfFontRendererBase.TabSize = 0;
		dfFontRendererBase.TextAlign = ((!this.autoSize) ? this.TextAlignment : 0);
		dfFontRendererBase.ProcessMarkup = true;
		dfFontRendererBase.DefaultColor = defaultColor;
		dfFontRendererBase.OverrideMarkupColors = false;
		dfFontRendererBase.Opacity = base.CalculateOpacity();
		dfFontRendererBase.Shadow = this.Shadow;
		dfFontRendererBase.ShadowColor = this.ShadowColor;
		dfFontRendererBase.ShadowOffset = this.ShadowOffset;
		dfDynamicFont.DynamicFontRenderer dynamicFontRenderer = dfFontRendererBase as dfDynamicFont.DynamicFontRenderer;
		if (dynamicFontRenderer != null)
		{
			dynamicFontRenderer.SpriteAtlas = base.Atlas;
			dynamicFontRenderer.SpriteBuffer = this.renderData;
		}
		if (this.vertAlign != dfVerticalAlignment.Top)
		{
			dfFontRendererBase.VectorOffset = this.getVertAlignOffset(dfFontRendererBase);
		}
		return dfFontRendererBase;
	}

	// Token: 0x06003AE1 RID: 15073 RVA: 0x000DC60C File Offset: 0x000DA80C
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

	// Token: 0x06003AE2 RID: 15074 RVA: 0x000DC680 File Offset: 0x000DA880
	private Color32 getTextColorForState()
	{
		if (!base.IsEnabled)
		{
			return this.DisabledTextColor;
		}
		switch (this.state)
		{
		case dfButton.ButtonState.Default:
			return this.TextColor;
		case dfButton.ButtonState.Focus:
			return this.FocusTextColor;
		case dfButton.ButtonState.Hover:
			return this.HoverTextColor;
		case dfButton.ButtonState.Pressed:
			return this.PressedTextColor;
		case dfButton.ButtonState.Disabled:
			return this.DisabledTextColor;
		default:
			return UnityEngine.Color.white;
		}
	}

	// Token: 0x06003AE3 RID: 15075 RVA: 0x000DC6F4 File Offset: 0x000DA8F4
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

	// Token: 0x06003AE4 RID: 15076 RVA: 0x000DC7A4 File Offset: 0x000DA9A4
	protected internal override dfAtlas.ItemInfo getBackgroundSprite()
	{
		if (base.Atlas == null)
		{
			return null;
		}
		dfAtlas.ItemInfo itemInfo = null;
		switch (this.state)
		{
		case dfButton.ButtonState.Default:
			itemInfo = this.atlas[this.backgroundSprite];
			break;
		case dfButton.ButtonState.Focus:
			itemInfo = this.atlas[this.focusSprite];
			break;
		case dfButton.ButtonState.Hover:
			itemInfo = this.atlas[this.hoverSprite];
			break;
		case dfButton.ButtonState.Pressed:
			itemInfo = this.atlas[this.pressedSprite];
			break;
		case dfButton.ButtonState.Disabled:
			itemInfo = this.atlas[this.disabledSprite];
			break;
		}
		if (itemInfo == null)
		{
			itemInfo = this.atlas[this.backgroundSprite];
		}
		return itemInfo;
	}

	// Token: 0x06003AE5 RID: 15077 RVA: 0x000DC880 File Offset: 0x000DAA80
	public dfList<dfRenderData> RenderMultiple()
	{
		if (!this.isVisible)
		{
			return null;
		}
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
			return this.buffers;
		}
		this.isControlInvalidated = false;
		this.buffers.Clear();
		this.renderData.Clear();
		if (base.Atlas != null)
		{
			this.renderData.Material = base.Atlas.Material;
			this.renderData.Transform = base.transform.localToWorldMatrix;
			this.renderBackground();
			this.buffers.Add(this.renderData);
		}
		dfRenderData dfRenderData = this.renderText();
		if (dfRenderData != null && dfRenderData != this.renderData)
		{
			dfRenderData.Transform = base.transform.localToWorldMatrix;
			this.buffers.Add(dfRenderData);
		}
		this.updateCollider();
		return this.buffers;
	}

	// Token: 0x04001F00 RID: 7936
	[SerializeField]
	protected dfFontBase font;

	// Token: 0x04001F01 RID: 7937
	[SerializeField]
	protected string pressedSprite;

	// Token: 0x04001F02 RID: 7938
	[SerializeField]
	protected dfButton.ButtonState state;

	// Token: 0x04001F03 RID: 7939
	[SerializeField]
	protected dfControl group;

	// Token: 0x04001F04 RID: 7940
	[SerializeField]
	protected string text = string.Empty;

	// Token: 0x04001F05 RID: 7941
	[SerializeField]
	protected TextAlignment textAlign = 1;

	// Token: 0x04001F06 RID: 7942
	[SerializeField]
	protected dfVerticalAlignment vertAlign = dfVerticalAlignment.Middle;

	// Token: 0x04001F07 RID: 7943
	[SerializeField]
	protected Color32 textColor = UnityEngine.Color.white;

	// Token: 0x04001F08 RID: 7944
	[SerializeField]
	protected Color32 hoverText = UnityEngine.Color.white;

	// Token: 0x04001F09 RID: 7945
	[SerializeField]
	protected Color32 pressedText = UnityEngine.Color.white;

	// Token: 0x04001F0A RID: 7946
	[SerializeField]
	protected Color32 focusText = UnityEngine.Color.white;

	// Token: 0x04001F0B RID: 7947
	[SerializeField]
	protected Color32 disabledText = UnityEngine.Color.white;

	// Token: 0x04001F0C RID: 7948
	[SerializeField]
	protected Color32 hoverColor = UnityEngine.Color.white;

	// Token: 0x04001F0D RID: 7949
	[SerializeField]
	protected Color32 pressedColor = UnityEngine.Color.white;

	// Token: 0x04001F0E RID: 7950
	[SerializeField]
	protected Color32 focusColor = UnityEngine.Color.white;

	// Token: 0x04001F0F RID: 7951
	[SerializeField]
	protected float textScale = 1f;

	// Token: 0x04001F10 RID: 7952
	[SerializeField]
	protected dfTextScaleMode textScaleMode;

	// Token: 0x04001F11 RID: 7953
	[SerializeField]
	protected bool wordWrap;

	// Token: 0x04001F12 RID: 7954
	[SerializeField]
	protected RectOffset padding = new RectOffset();

	// Token: 0x04001F13 RID: 7955
	[SerializeField]
	protected bool textShadow;

	// Token: 0x04001F14 RID: 7956
	[SerializeField]
	protected Color32 shadowColor = UnityEngine.Color.black;

	// Token: 0x04001F15 RID: 7957
	[SerializeField]
	protected Vector2 shadowOffset = new Vector2(1f, -1f);

	// Token: 0x04001F16 RID: 7958
	[SerializeField]
	protected bool autoSize;

	// Token: 0x04001F17 RID: 7959
	private Vector2 startSize = Vector2.zero;

	// Token: 0x04001F18 RID: 7960
	private dfRenderData textRenderData;

	// Token: 0x04001F19 RID: 7961
	private dfList<dfRenderData> buffers = dfList<dfRenderData>.Obtain();

	// Token: 0x020006A1 RID: 1697
	public enum ButtonState
	{
		// Token: 0x04001F1C RID: 7964
		Default,
		// Token: 0x04001F1D RID: 7965
		Focus,
		// Token: 0x04001F1E RID: 7966
		Hover,
		// Token: 0x04001F1F RID: 7967
		Pressed,
		// Token: 0x04001F20 RID: 7968
		Disabled
	}
}
