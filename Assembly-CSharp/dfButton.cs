using System;
using UnityEngine;

// Token: 0x02000766 RID: 1894
[RequireComponent(typeof(BoxCollider))]
[ExecuteInEditMode]
[AddComponentMenu("Daikon Forge/User Interface/Button")]
[Serializable]
public class dfButton : global::dfInteractiveBase, global::IDFMultiRender
{
	// Token: 0x14000021 RID: 33
	// (add) Token: 0x06003E9C RID: 16028 RVA: 0x000E4588 File Offset: 0x000E2788
	// (remove) Token: 0x06003E9D RID: 16029 RVA: 0x000E45A4 File Offset: 0x000E27A4
	public event global::PropertyChangedEventHandler<global::dfButton.ButtonState> ButtonStateChanged;

	// Token: 0x17000BD9 RID: 3033
	// (get) Token: 0x06003E9E RID: 16030 RVA: 0x000E45C0 File Offset: 0x000E27C0
	// (set) Token: 0x06003E9F RID: 16031 RVA: 0x000E45C8 File Offset: 0x000E27C8
	public global::dfButton.ButtonState State
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

	// Token: 0x17000BDA RID: 3034
	// (get) Token: 0x06003EA0 RID: 16032 RVA: 0x000E45E4 File Offset: 0x000E27E4
	// (set) Token: 0x06003EA1 RID: 16033 RVA: 0x000E45EC File Offset: 0x000E27EC
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

	// Token: 0x17000BDB RID: 3035
	// (get) Token: 0x06003EA2 RID: 16034 RVA: 0x000E460C File Offset: 0x000E280C
	// (set) Token: 0x06003EA3 RID: 16035 RVA: 0x000E4614 File Offset: 0x000E2814
	public global::dfControl ButtonGroup
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

	// Token: 0x17000BDC RID: 3036
	// (get) Token: 0x06003EA4 RID: 16036 RVA: 0x000E4634 File Offset: 0x000E2834
	// (set) Token: 0x06003EA5 RID: 16037 RVA: 0x000E463C File Offset: 0x000E283C
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

	// Token: 0x17000BDD RID: 3037
	// (get) Token: 0x06003EA6 RID: 16038 RVA: 0x000E4670 File Offset: 0x000E2870
	// (set) Token: 0x06003EA7 RID: 16039 RVA: 0x000E4688 File Offset: 0x000E2888
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

	// Token: 0x17000BDE RID: 3038
	// (get) Token: 0x06003EA8 RID: 16040 RVA: 0x000E46A4 File Offset: 0x000E28A4
	// (set) Token: 0x06003EA9 RID: 16041 RVA: 0x000E46AC File Offset: 0x000E28AC
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

	// Token: 0x17000BDF RID: 3039
	// (get) Token: 0x06003EAA RID: 16042 RVA: 0x000E46C8 File Offset: 0x000E28C8
	// (set) Token: 0x06003EAB RID: 16043 RVA: 0x000E46E8 File Offset: 0x000E28E8
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

	// Token: 0x17000BE0 RID: 3040
	// (get) Token: 0x06003EAC RID: 16044 RVA: 0x000E471C File Offset: 0x000E291C
	// (set) Token: 0x06003EAD RID: 16045 RVA: 0x000E4760 File Offset: 0x000E2960
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
			}
			this.Invalidate();
		}
	}

	// Token: 0x17000BE1 RID: 3041
	// (get) Token: 0x06003EAE RID: 16046 RVA: 0x000E4780 File Offset: 0x000E2980
	// (set) Token: 0x06003EAF RID: 16047 RVA: 0x000E4788 File Offset: 0x000E2988
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

	// Token: 0x17000BE2 RID: 3042
	// (get) Token: 0x06003EB0 RID: 16048 RVA: 0x000E47BC File Offset: 0x000E29BC
	// (set) Token: 0x06003EB1 RID: 16049 RVA: 0x000E47C4 File Offset: 0x000E29C4
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

	// Token: 0x17000BE3 RID: 3043
	// (get) Token: 0x06003EB2 RID: 16050 RVA: 0x000E47D4 File Offset: 0x000E29D4
	// (set) Token: 0x06003EB3 RID: 16051 RVA: 0x000E47DC File Offset: 0x000E29DC
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

	// Token: 0x17000BE4 RID: 3044
	// (get) Token: 0x06003EB4 RID: 16052 RVA: 0x000E47EC File Offset: 0x000E29EC
	// (set) Token: 0x06003EB5 RID: 16053 RVA: 0x000E47F4 File Offset: 0x000E29F4
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

	// Token: 0x17000BE5 RID: 3045
	// (get) Token: 0x06003EB6 RID: 16054 RVA: 0x000E4804 File Offset: 0x000E2A04
	// (set) Token: 0x06003EB7 RID: 16055 RVA: 0x000E480C File Offset: 0x000E2A0C
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

	// Token: 0x17000BE6 RID: 3046
	// (get) Token: 0x06003EB8 RID: 16056 RVA: 0x000E481C File Offset: 0x000E2A1C
	// (set) Token: 0x06003EB9 RID: 16057 RVA: 0x000E4824 File Offset: 0x000E2A24
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

	// Token: 0x17000BE7 RID: 3047
	// (get) Token: 0x06003EBA RID: 16058 RVA: 0x000E4834 File Offset: 0x000E2A34
	// (set) Token: 0x06003EBB RID: 16059 RVA: 0x000E483C File Offset: 0x000E2A3C
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

	// Token: 0x17000BE8 RID: 3048
	// (get) Token: 0x06003EBC RID: 16060 RVA: 0x000E484C File Offset: 0x000E2A4C
	// (set) Token: 0x06003EBD RID: 16061 RVA: 0x000E4854 File Offset: 0x000E2A54
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

	// Token: 0x17000BE9 RID: 3049
	// (get) Token: 0x06003EBE RID: 16062 RVA: 0x000E4864 File Offset: 0x000E2A64
	// (set) Token: 0x06003EBF RID: 16063 RVA: 0x000E486C File Offset: 0x000E2A6C
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

	// Token: 0x17000BEA RID: 3050
	// (get) Token: 0x06003EC0 RID: 16064 RVA: 0x000E487C File Offset: 0x000E2A7C
	// (set) Token: 0x06003EC1 RID: 16065 RVA: 0x000E4884 File Offset: 0x000E2A84
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

	// Token: 0x17000BEB RID: 3051
	// (get) Token: 0x06003EC2 RID: 16066 RVA: 0x000E48B4 File Offset: 0x000E2AB4
	// (set) Token: 0x06003EC3 RID: 16067 RVA: 0x000E48BC File Offset: 0x000E2ABC
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

	// Token: 0x17000BEC RID: 3052
	// (get) Token: 0x06003EC4 RID: 16068 RVA: 0x000E48CC File Offset: 0x000E2ACC
	// (set) Token: 0x06003EC5 RID: 16069 RVA: 0x000E48D4 File Offset: 0x000E2AD4
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

	// Token: 0x17000BED RID: 3053
	// (get) Token: 0x06003EC6 RID: 16070 RVA: 0x000E48F0 File Offset: 0x000E2AF0
	// (set) Token: 0x06003EC7 RID: 16071 RVA: 0x000E48F8 File Offset: 0x000E2AF8
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

	// Token: 0x17000BEE RID: 3054
	// (get) Token: 0x06003EC8 RID: 16072 RVA: 0x000E4914 File Offset: 0x000E2B14
	// (set) Token: 0x06003EC9 RID: 16073 RVA: 0x000E491C File Offset: 0x000E2B1C
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

	// Token: 0x17000BEF RID: 3055
	// (get) Token: 0x06003ECA RID: 16074 RVA: 0x000E4954 File Offset: 0x000E2B54
	// (set) Token: 0x06003ECB RID: 16075 RVA: 0x000E495C File Offset: 0x000E2B5C
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

	// Token: 0x06003ECC RID: 16076 RVA: 0x000E497C File Offset: 0x000E2B7C
	protected internal override void OnLocalize()
	{
		base.OnLocalize();
		this.Text = base.getLocalizedValue(this.text);
	}

	// Token: 0x06003ECD RID: 16077 RVA: 0x000E4998 File Offset: 0x000E2B98
	public override void Invalidate()
	{
		base.Invalidate();
		if (this.AutoSize)
		{
			this.autoSizeToText();
		}
	}

	// Token: 0x06003ECE RID: 16078 RVA: 0x000E49B4 File Offset: 0x000E2BB4
	public override void OnEnable()
	{
		base.OnEnable();
		bool flag = this.Font != null && this.Font.IsValid;
		if (Application.isPlaying && !flag)
		{
			this.Font = base.GetManager().DefaultFont;
		}
	}

	// Token: 0x06003ECF RID: 16079 RVA: 0x000E4A08 File Offset: 0x000E2C08
	public override void Awake()
	{
		base.Awake();
		this.startSize = base.Size;
	}

	// Token: 0x06003ED0 RID: 16080 RVA: 0x000E4A1C File Offset: 0x000E2C1C
	public override void Update()
	{
		base.Update();
	}

	// Token: 0x06003ED1 RID: 16081 RVA: 0x000E4A24 File Offset: 0x000E2C24
	protected internal override void OnEnterFocus(global::dfFocusEventArgs args)
	{
		if (this.State != global::dfButton.ButtonState.Pressed)
		{
			this.State = global::dfButton.ButtonState.Focus;
		}
		base.OnEnterFocus(args);
	}

	// Token: 0x06003ED2 RID: 16082 RVA: 0x000E4A40 File Offset: 0x000E2C40
	protected internal override void OnLeaveFocus(global::dfFocusEventArgs args)
	{
		this.State = global::dfButton.ButtonState.Default;
		base.OnLeaveFocus(args);
	}

	// Token: 0x06003ED3 RID: 16083 RVA: 0x000E4A50 File Offset: 0x000E2C50
	protected internal override void OnKeyPress(global::dfKeyEventArgs args)
	{
		if (this.IsInteractive && args.KeyCode == 32)
		{
			this.OnClick(new global::dfMouseEventArgs(this, global::dfMouseButtons.Left, 1, default(Ray), Vector2.zero, 0f));
			return;
		}
		base.OnKeyPress(args);
	}

	// Token: 0x06003ED4 RID: 16084 RVA: 0x000E4AA0 File Offset: 0x000E2CA0
	protected internal override void OnClick(global::dfMouseEventArgs args)
	{
		if (this.group != null)
		{
			foreach (global::dfButton dfButton in base.transform.parent.GetComponentsInChildren<global::dfButton>())
			{
				if (dfButton != this && dfButton.ButtonGroup == this.ButtonGroup && dfButton != this)
				{
					dfButton.State = global::dfButton.ButtonState.Default;
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

	// Token: 0x06003ED5 RID: 16085 RVA: 0x000E4B60 File Offset: 0x000E2D60
	protected internal override void OnMouseDown(global::dfMouseEventArgs args)
	{
		if (!(this.parent is global::dfTabstrip) || this.State != global::dfButton.ButtonState.Focus)
		{
			this.State = global::dfButton.ButtonState.Pressed;
		}
		base.OnMouseDown(args);
	}

	// Token: 0x06003ED6 RID: 16086 RVA: 0x000E4B98 File Offset: 0x000E2D98
	protected internal override void OnMouseUp(global::dfMouseEventArgs args)
	{
		if (this.isMouseHovering)
		{
			if (this.parent is global::dfTabstrip && this.ContainsFocus)
			{
				this.State = global::dfButton.ButtonState.Focus;
			}
			else
			{
				this.State = global::dfButton.ButtonState.Hover;
			}
		}
		else if (this.HasFocus)
		{
			this.State = global::dfButton.ButtonState.Focus;
		}
		else
		{
			this.State = global::dfButton.ButtonState.Default;
		}
		base.OnMouseUp(args);
	}

	// Token: 0x06003ED7 RID: 16087 RVA: 0x000E4C08 File Offset: 0x000E2E08
	protected internal override void OnMouseEnter(global::dfMouseEventArgs args)
	{
		if (!(this.parent is global::dfTabstrip) || this.State != global::dfButton.ButtonState.Focus)
		{
			this.State = global::dfButton.ButtonState.Hover;
		}
		base.OnMouseEnter(args);
	}

	// Token: 0x06003ED8 RID: 16088 RVA: 0x000E4C40 File Offset: 0x000E2E40
	protected internal override void OnMouseLeave(global::dfMouseEventArgs args)
	{
		if (this.ContainsFocus)
		{
			this.State = global::dfButton.ButtonState.Focus;
		}
		else
		{
			this.State = global::dfButton.ButtonState.Default;
		}
		base.OnMouseLeave(args);
	}

	// Token: 0x06003ED9 RID: 16089 RVA: 0x000E4C74 File Offset: 0x000E2E74
	protected internal override void OnIsEnabledChanged()
	{
		if (!base.IsEnabled)
		{
			this.State = global::dfButton.ButtonState.Disabled;
		}
		else
		{
			this.State = global::dfButton.ButtonState.Default;
		}
		base.OnIsEnabledChanged();
	}

	// Token: 0x06003EDA RID: 16090 RVA: 0x000E4CA8 File Offset: 0x000E2EA8
	protected virtual void OnButtonStateChanged(global::dfButton.ButtonState value)
	{
		if (!this.isEnabled && value != global::dfButton.ButtonState.Disabled)
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

	// Token: 0x06003EDB RID: 16091 RVA: 0x000E4D08 File Offset: 0x000E2F08
	protected override Color32 getActiveColor()
	{
		switch (this.State)
		{
		case global::dfButton.ButtonState.Focus:
			return this.FocusBackgroundColor;
		case global::dfButton.ButtonState.Hover:
			return this.HoverBackgroundColor;
		case global::dfButton.ButtonState.Pressed:
			return this.PressedBackgroundColor;
		case global::dfButton.ButtonState.Disabled:
			return base.DisabledColor;
		default:
			return base.Color;
		}
	}

	// Token: 0x06003EDC RID: 16092 RVA: 0x000E4D5C File Offset: 0x000E2F5C
	private void autoSizeToText()
	{
		if (this.Font == null || !this.Font.IsValid || string.IsNullOrEmpty(this.Text))
		{
			return;
		}
		using (global::dfFontRendererBase dfFontRendererBase = this.obtainTextRenderer())
		{
			Vector2 vector = dfFontRendererBase.MeasureString(this.Text);
			Vector2 size;
			size..ctor(vector.x + (float)this.padding.horizontal, vector.y + (float)this.padding.vertical);
			base.Size = size;
		}
	}

	// Token: 0x06003EDD RID: 16093 RVA: 0x000E4E14 File Offset: 0x000E3014
	private global::dfRenderData renderText()
	{
		if (this.Font == null || !this.Font.IsValid || string.IsNullOrEmpty(this.Text))
		{
			return null;
		}
		global::dfRenderData renderData = this.renderData;
		if (this.font is global::dfDynamicFont)
		{
			global::dfDynamicFont dfDynamicFont = (global::dfDynamicFont)this.font;
			renderData = this.textRenderData;
			renderData.Clear();
			renderData.Material = dfDynamicFont.Material;
		}
		using (global::dfFontRendererBase dfFontRendererBase = this.obtainTextRenderer())
		{
			dfFontRendererBase.Render(this.text, renderData);
		}
		return renderData;
	}

	// Token: 0x06003EDE RID: 16094 RVA: 0x000E4ED4 File Offset: 0x000E30D4
	private global::dfFontRendererBase obtainTextRenderer()
	{
		Vector2 vector = base.Size - new Vector2((float)this.padding.horizontal, (float)this.padding.vertical);
		Vector2 maxSize = (!this.autoSize) ? vector : (Vector2.one * 2.14748365E+09f);
		float num = base.PixelsToUnits();
		Vector3 vector2 = (this.pivot.TransformToUpperLeft(base.Size) + new Vector3((float)this.padding.left, (float)(-(float)this.padding.top))) * num;
		float num2 = this.TextScale * this.getTextScaleMultiplier();
		Color32 defaultColor = base.ApplyOpacity(this.getTextColorForState());
		global::dfFontRendererBase dfFontRendererBase = this.Font.ObtainRenderer();
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
		global::dfDynamicFont.DynamicFontRenderer dynamicFontRenderer = dfFontRendererBase as global::dfDynamicFont.DynamicFontRenderer;
		if (dynamicFontRenderer != null)
		{
			dynamicFontRenderer.SpriteAtlas = base.Atlas;
			dynamicFontRenderer.SpriteBuffer = this.renderData;
		}
		if (this.vertAlign != global::dfVerticalAlignment.Top)
		{
			dfFontRendererBase.VectorOffset = this.getVertAlignOffset(dfFontRendererBase);
		}
		return dfFontRendererBase;
	}

	// Token: 0x06003EDF RID: 16095 RVA: 0x000E509C File Offset: 0x000E329C
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

	// Token: 0x06003EE0 RID: 16096 RVA: 0x000E5110 File Offset: 0x000E3310
	private Color32 getTextColorForState()
	{
		if (!base.IsEnabled)
		{
			return this.DisabledTextColor;
		}
		switch (this.state)
		{
		case global::dfButton.ButtonState.Default:
			return this.TextColor;
		case global::dfButton.ButtonState.Focus:
			return this.FocusTextColor;
		case global::dfButton.ButtonState.Hover:
			return this.HoverTextColor;
		case global::dfButton.ButtonState.Pressed:
			return this.PressedTextColor;
		case global::dfButton.ButtonState.Disabled:
			return this.DisabledTextColor;
		default:
			return UnityEngine.Color.white;
		}
	}

	// Token: 0x06003EE1 RID: 16097 RVA: 0x000E5184 File Offset: 0x000E3384
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

	// Token: 0x06003EE2 RID: 16098 RVA: 0x000E5234 File Offset: 0x000E3434
	protected internal override global::dfAtlas.ItemInfo getBackgroundSprite()
	{
		if (base.Atlas == null)
		{
			return null;
		}
		global::dfAtlas.ItemInfo itemInfo = null;
		switch (this.state)
		{
		case global::dfButton.ButtonState.Default:
			itemInfo = this.atlas[this.backgroundSprite];
			break;
		case global::dfButton.ButtonState.Focus:
			itemInfo = this.atlas[this.focusSprite];
			break;
		case global::dfButton.ButtonState.Hover:
			itemInfo = this.atlas[this.hoverSprite];
			break;
		case global::dfButton.ButtonState.Pressed:
			itemInfo = this.atlas[this.pressedSprite];
			break;
		case global::dfButton.ButtonState.Disabled:
			itemInfo = this.atlas[this.disabledSprite];
			break;
		}
		if (itemInfo == null)
		{
			itemInfo = this.atlas[this.backgroundSprite];
		}
		return itemInfo;
	}

	// Token: 0x06003EE3 RID: 16099 RVA: 0x000E5310 File Offset: 0x000E3510
	public global::dfList<global::dfRenderData> RenderMultiple()
	{
		if (!this.isVisible)
		{
			return null;
		}
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
		global::dfRenderData dfRenderData = this.renderText();
		if (dfRenderData != null && dfRenderData != this.renderData)
		{
			dfRenderData.Transform = base.transform.localToWorldMatrix;
			this.buffers.Add(dfRenderData);
		}
		this.updateCollider();
		return this.buffers;
	}

	// Token: 0x040020FC RID: 8444
	[SerializeField]
	protected global::dfFontBase font;

	// Token: 0x040020FD RID: 8445
	[SerializeField]
	protected string pressedSprite;

	// Token: 0x040020FE RID: 8446
	[SerializeField]
	protected global::dfButton.ButtonState state;

	// Token: 0x040020FF RID: 8447
	[SerializeField]
	protected global::dfControl group;

	// Token: 0x04002100 RID: 8448
	[SerializeField]
	protected string text = string.Empty;

	// Token: 0x04002101 RID: 8449
	[SerializeField]
	protected TextAlignment textAlign = 1;

	// Token: 0x04002102 RID: 8450
	[SerializeField]
	protected global::dfVerticalAlignment vertAlign = global::dfVerticalAlignment.Middle;

	// Token: 0x04002103 RID: 8451
	[SerializeField]
	protected Color32 textColor = UnityEngine.Color.white;

	// Token: 0x04002104 RID: 8452
	[SerializeField]
	protected Color32 hoverText = UnityEngine.Color.white;

	// Token: 0x04002105 RID: 8453
	[SerializeField]
	protected Color32 pressedText = UnityEngine.Color.white;

	// Token: 0x04002106 RID: 8454
	[SerializeField]
	protected Color32 focusText = UnityEngine.Color.white;

	// Token: 0x04002107 RID: 8455
	[SerializeField]
	protected Color32 disabledText = UnityEngine.Color.white;

	// Token: 0x04002108 RID: 8456
	[SerializeField]
	protected Color32 hoverColor = UnityEngine.Color.white;

	// Token: 0x04002109 RID: 8457
	[SerializeField]
	protected Color32 pressedColor = UnityEngine.Color.white;

	// Token: 0x0400210A RID: 8458
	[SerializeField]
	protected Color32 focusColor = UnityEngine.Color.white;

	// Token: 0x0400210B RID: 8459
	[SerializeField]
	protected float textScale = 1f;

	// Token: 0x0400210C RID: 8460
	[SerializeField]
	protected global::dfTextScaleMode textScaleMode;

	// Token: 0x0400210D RID: 8461
	[SerializeField]
	protected bool wordWrap;

	// Token: 0x0400210E RID: 8462
	[SerializeField]
	protected RectOffset padding = new RectOffset();

	// Token: 0x0400210F RID: 8463
	[SerializeField]
	protected bool textShadow;

	// Token: 0x04002110 RID: 8464
	[SerializeField]
	protected Color32 shadowColor = UnityEngine.Color.black;

	// Token: 0x04002111 RID: 8465
	[SerializeField]
	protected Vector2 shadowOffset = new Vector2(1f, -1f);

	// Token: 0x04002112 RID: 8466
	[SerializeField]
	protected bool autoSize;

	// Token: 0x04002113 RID: 8467
	private Vector2 startSize = Vector2.zero;

	// Token: 0x04002114 RID: 8468
	private global::dfRenderData textRenderData;

	// Token: 0x04002115 RID: 8469
	private global::dfList<global::dfRenderData> buffers = global::dfList<global::dfRenderData>.Obtain();

	// Token: 0x02000767 RID: 1895
	public enum ButtonState
	{
		// Token: 0x04002118 RID: 8472
		Default,
		// Token: 0x04002119 RID: 8473
		Focus,
		// Token: 0x0400211A RID: 8474
		Hover,
		// Token: 0x0400211B RID: 8475
		Pressed,
		// Token: 0x0400211C RID: 8476
		Disabled
	}
}
