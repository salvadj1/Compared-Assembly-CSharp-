using System;
using UnityEngine;

// Token: 0x0200080C RID: 2060
[RequireComponent(typeof(BoxCollider))]
[AddComponentMenu("Daikon Forge/User Interface/Rich Text Label")]
[ExecuteInEditMode]
[Serializable]
public class dfRichTextLabel : global::dfControl, global::IDFMultiRender
{
	// Token: 0x14000062 RID: 98
	// (add) Token: 0x06004753 RID: 18259 RVA: 0x0010DFF0 File Offset: 0x0010C1F0
	// (remove) Token: 0x06004754 RID: 18260 RVA: 0x0010E00C File Offset: 0x0010C20C
	public event global::PropertyChangedEventHandler<string> TextChanged;

	// Token: 0x14000063 RID: 99
	// (add) Token: 0x06004755 RID: 18261 RVA: 0x0010E028 File Offset: 0x0010C228
	// (remove) Token: 0x06004756 RID: 18262 RVA: 0x0010E044 File Offset: 0x0010C244
	public event global::PropertyChangedEventHandler<Vector2> ScrollPositionChanged;

	// Token: 0x14000064 RID: 100
	// (add) Token: 0x06004757 RID: 18263 RVA: 0x0010E060 File Offset: 0x0010C260
	// (remove) Token: 0x06004758 RID: 18264 RVA: 0x0010E07C File Offset: 0x0010C27C
	public event global::dfRichTextLabel.LinkClickEventHandler LinkClicked;

	// Token: 0x17000DAF RID: 3503
	// (get) Token: 0x06004759 RID: 18265 RVA: 0x0010E098 File Offset: 0x0010C298
	// (set) Token: 0x0600475A RID: 18266 RVA: 0x0010E0E0 File Offset: 0x0010C2E0
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

	// Token: 0x17000DB0 RID: 3504
	// (get) Token: 0x0600475B RID: 18267 RVA: 0x0010E100 File Offset: 0x0010C300
	// (set) Token: 0x0600475C RID: 18268 RVA: 0x0010E108 File Offset: 0x0010C308
	public global::dfDynamicFont Font
	{
		get
		{
			return this.font;
		}
		set
		{
			if (value != this.font)
			{
				this.font = value;
				this.LineHeight = value.FontSize;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000DB1 RID: 3505
	// (get) Token: 0x0600475D RID: 18269 RVA: 0x0010E140 File Offset: 0x0010C340
	// (set) Token: 0x0600475E RID: 18270 RVA: 0x0010E148 File Offset: 0x0010C348
	public string BlankTextureSprite
	{
		get
		{
			return this.blankTextureSprite;
		}
		set
		{
			if (value != this.blankTextureSprite)
			{
				this.blankTextureSprite = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000DB2 RID: 3506
	// (get) Token: 0x0600475F RID: 18271 RVA: 0x0010E168 File Offset: 0x0010C368
	// (set) Token: 0x06004760 RID: 18272 RVA: 0x0010E170 File Offset: 0x0010C370
	public string Text
	{
		get
		{
			return this.text;
		}
		set
		{
			value = base.getLocalizedValue(value);
			if (!string.Equals(this.text, value))
			{
				this.text = value;
				this.scrollPosition = Vector2.zero;
				this.Invalidate();
				this.OnTextChanged();
			}
		}
	}

	// Token: 0x17000DB3 RID: 3507
	// (get) Token: 0x06004761 RID: 18273 RVA: 0x0010E1B8 File Offset: 0x0010C3B8
	// (set) Token: 0x06004762 RID: 18274 RVA: 0x0010E1C0 File Offset: 0x0010C3C0
	public int FontSize
	{
		get
		{
			return this.fontSize;
		}
		set
		{
			value = Mathf.Max(6, value);
			if (value != this.fontSize)
			{
				this.fontSize = value;
				this.Invalidate();
			}
			this.LineHeight = value;
		}
	}

	// Token: 0x17000DB4 RID: 3508
	// (get) Token: 0x06004763 RID: 18275 RVA: 0x0010E1EC File Offset: 0x0010C3EC
	// (set) Token: 0x06004764 RID: 18276 RVA: 0x0010E1F4 File Offset: 0x0010C3F4
	public int LineHeight
	{
		get
		{
			return this.lineheight;
		}
		set
		{
			value = Mathf.Max(this.FontSize, value);
			if (value != this.lineheight)
			{
				this.lineheight = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000DB5 RID: 3509
	// (get) Token: 0x06004765 RID: 18277 RVA: 0x0010E220 File Offset: 0x0010C420
	// (set) Token: 0x06004766 RID: 18278 RVA: 0x0010E228 File Offset: 0x0010C428
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

	// Token: 0x17000DB6 RID: 3510
	// (get) Token: 0x06004767 RID: 18279 RVA: 0x0010E238 File Offset: 0x0010C438
	// (set) Token: 0x06004768 RID: 18280 RVA: 0x0010E240 File Offset: 0x0010C440
	public bool PreserveWhitespace
	{
		get
		{
			return this.preserveWhitespace;
		}
		set
		{
			if (value != this.preserveWhitespace)
			{
				this.preserveWhitespace = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000DB7 RID: 3511
	// (get) Token: 0x06004769 RID: 18281 RVA: 0x0010E25C File Offset: 0x0010C45C
	// (set) Token: 0x0600476A RID: 18282 RVA: 0x0010E264 File Offset: 0x0010C464
	public FontStyle FontStyle
	{
		get
		{
			return this.fontStyle;
		}
		set
		{
			if (value != this.fontStyle)
			{
				this.fontStyle = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000DB8 RID: 3512
	// (get) Token: 0x0600476B RID: 18283 RVA: 0x0010E280 File Offset: 0x0010C480
	// (set) Token: 0x0600476C RID: 18284 RVA: 0x0010E288 File Offset: 0x0010C488
	public global::dfMarkupTextAlign TextAlignment
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

	// Token: 0x17000DB9 RID: 3513
	// (get) Token: 0x0600476D RID: 18285 RVA: 0x0010E2A4 File Offset: 0x0010C4A4
	// (set) Token: 0x0600476E RID: 18286 RVA: 0x0010E2AC File Offset: 0x0010C4AC
	public bool AllowScrolling
	{
		get
		{
			return this.allowScrolling;
		}
		set
		{
			this.allowScrolling = value;
			if (!value)
			{
				this.ScrollPosition = Vector2.zero;
			}
		}
	}

	// Token: 0x17000DBA RID: 3514
	// (get) Token: 0x0600476F RID: 18287 RVA: 0x0010E2C8 File Offset: 0x0010C4C8
	// (set) Token: 0x06004770 RID: 18288 RVA: 0x0010E2D0 File Offset: 0x0010C4D0
	public Vector2 ScrollPosition
	{
		get
		{
			return this.scrollPosition;
		}
		set
		{
			if (!this.allowScrolling)
			{
				value = Vector2.zero;
			}
			Vector2 vector = this.ContentSize - base.Size;
			value = Vector2.Min(vector, value);
			value = Vector2.Max(Vector2.zero, value);
			value = value.RoundToInt();
			if ((value - this.scrollPosition).sqrMagnitude > 1.401298E-45f)
			{
				this.scrollPosition = value;
				this.updateScrollbars();
				this.OnScrollPositionChanged();
			}
		}
	}

	// Token: 0x17000DBB RID: 3515
	// (get) Token: 0x06004771 RID: 18289 RVA: 0x0010E350 File Offset: 0x0010C550
	// (set) Token: 0x06004772 RID: 18290 RVA: 0x0010E358 File Offset: 0x0010C558
	public global::dfScrollbar HorizontalScrollbar
	{
		get
		{
			return this.horzScrollbar;
		}
		set
		{
			this.horzScrollbar = value;
			this.updateScrollbars();
		}
	}

	// Token: 0x17000DBC RID: 3516
	// (get) Token: 0x06004773 RID: 18291 RVA: 0x0010E368 File Offset: 0x0010C568
	// (set) Token: 0x06004774 RID: 18292 RVA: 0x0010E370 File Offset: 0x0010C570
	public global::dfScrollbar VerticalScrollbar
	{
		get
		{
			return this.vertScrollbar;
		}
		set
		{
			this.vertScrollbar = value;
			this.updateScrollbars();
		}
	}

	// Token: 0x17000DBD RID: 3517
	// (get) Token: 0x06004775 RID: 18293 RVA: 0x0010E380 File Offset: 0x0010C580
	public Vector2 ContentSize
	{
		get
		{
			if (this.viewportBox != null)
			{
				return this.viewportBox.Size;
			}
			return base.Size;
		}
	}

	// Token: 0x17000DBE RID: 3518
	// (get) Token: 0x06004776 RID: 18294 RVA: 0x0010E3A0 File Offset: 0x0010C5A0
	// (set) Token: 0x06004777 RID: 18295 RVA: 0x0010E3A8 File Offset: 0x0010C5A8
	public bool UseScrollMomentum
	{
		get
		{
			return this.useScrollMomentum;
		}
		set
		{
			this.useScrollMomentum = value;
			this.scrollMomentum = Vector2.zero;
		}
	}

	// Token: 0x06004778 RID: 18296 RVA: 0x0010E3BC File Offset: 0x0010C5BC
	protected internal override void OnLocalize()
	{
		base.OnLocalize();
		this.Text = base.getLocalizedValue(this.text);
	}

	// Token: 0x06004779 RID: 18297 RVA: 0x0010E3D8 File Offset: 0x0010C5D8
	public override void Invalidate()
	{
		base.Invalidate();
		this.isMarkupInvalidated = true;
	}

	// Token: 0x0600477A RID: 18298 RVA: 0x0010E3E8 File Offset: 0x0010C5E8
	public override void Awake()
	{
		base.Awake();
		this.startSize = base.Size;
	}

	// Token: 0x0600477B RID: 18299 RVA: 0x0010E3FC File Offset: 0x0010C5FC
	public override void OnEnable()
	{
		base.OnEnable();
		if (this.size.sqrMagnitude <= 1.401298E-45f)
		{
			base.Size = new Vector2(320f, 200f);
			int lineHeight = 16;
			this.LineHeight = lineHeight;
			this.FontSize = lineHeight;
		}
	}

	// Token: 0x0600477C RID: 18300 RVA: 0x0010E44C File Offset: 0x0010C64C
	public override void Update()
	{
		base.Update();
		if (this.useScrollMomentum && !this.isMouseDown && this.scrollMomentum.magnitude > 0.1f)
		{
			this.ScrollPosition += this.scrollMomentum;
			this.scrollMomentum *= 0.95f - Time.deltaTime;
		}
	}

	// Token: 0x0600477D RID: 18301 RVA: 0x0010E4C0 File Offset: 0x0010C6C0
	public override void LateUpdate()
	{
		base.LateUpdate();
		this.initialize();
	}

	// Token: 0x0600477E RID: 18302 RVA: 0x0010E4D0 File Offset: 0x0010C6D0
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

	// Token: 0x0600477F RID: 18303 RVA: 0x0010E51C File Offset: 0x0010C71C
	protected internal void OnScrollPositionChanged()
	{
		base.Invalidate();
		base.SignalHierarchy("OnScrollPositionChanged", new object[]
		{
			this.ScrollPosition
		});
		if (this.ScrollPositionChanged != null)
		{
			this.ScrollPositionChanged(this, this.ScrollPosition);
		}
	}

	// Token: 0x06004780 RID: 18304 RVA: 0x0010E56C File Offset: 0x0010C76C
	protected internal override void OnKeyDown(global::dfKeyEventArgs args)
	{
		if (args.Used)
		{
			base.OnKeyDown(args);
			return;
		}
		int num = this.FontSize;
		int num2 = this.FontSize;
		if (args.KeyCode == 276)
		{
			this.ScrollPosition += new Vector2((float)(-(float)num), 0f);
			args.Use();
		}
		else if (args.KeyCode == 275)
		{
			this.ScrollPosition += new Vector2((float)num, 0f);
			args.Use();
		}
		else if (args.KeyCode == 273)
		{
			this.ScrollPosition += new Vector2(0f, (float)(-(float)num2));
			args.Use();
		}
		else if (args.KeyCode == 274)
		{
			this.ScrollPosition += new Vector2(0f, (float)num2);
			args.Use();
		}
		base.OnKeyDown(args);
	}

	// Token: 0x06004781 RID: 18305 RVA: 0x0010E680 File Offset: 0x0010C880
	internal override void OnDragEnd(global::dfDragEventArgs args)
	{
		base.OnDragEnd(args);
		this.isMouseDown = false;
	}

	// Token: 0x06004782 RID: 18306 RVA: 0x0010E690 File Offset: 0x0010C890
	protected internal override void OnMouseEnter(global::dfMouseEventArgs args)
	{
		base.OnMouseEnter(args);
		this.touchStartPosition = args.Position;
	}

	// Token: 0x06004783 RID: 18307 RVA: 0x0010E6A8 File Offset: 0x0010C8A8
	protected internal override void OnMouseDown(global::dfMouseEventArgs args)
	{
		base.OnMouseDown(args);
		this.mouseDownTag = this.hitTestTag(args);
		this.mouseDownScrollPosition = this.scrollPosition;
		this.scrollMomentum = Vector2.zero;
		this.touchStartPosition = args.Position;
		this.isMouseDown = true;
	}

	// Token: 0x06004784 RID: 18308 RVA: 0x0010E6F4 File Offset: 0x0010C8F4
	protected internal override void OnMouseUp(global::dfMouseEventArgs args)
	{
		base.OnMouseUp(args);
		this.isMouseDown = false;
		if (Vector2.Distance(this.scrollPosition, this.mouseDownScrollPosition) <= 2f && this.hitTestTag(args) == this.mouseDownTag)
		{
			global::dfMarkupTag dfMarkupTag = this.mouseDownTag;
			while (dfMarkupTag != null && !(dfMarkupTag is global::dfMarkupTagAnchor))
			{
				dfMarkupTag = (dfMarkupTag.Parent as global::dfMarkupTag);
			}
			if (dfMarkupTag is global::dfMarkupTagAnchor)
			{
				base.Signal("OnLinkClicked", new object[]
				{
					dfMarkupTag
				});
				if (this.LinkClicked != null)
				{
					this.LinkClicked(this, dfMarkupTag as global::dfMarkupTagAnchor);
				}
			}
		}
		this.mouseDownTag = null;
		this.mouseDownScrollPosition = this.scrollPosition;
	}

	// Token: 0x06004785 RID: 18309 RVA: 0x0010E7B8 File Offset: 0x0010C9B8
	protected internal override void OnMouseMove(global::dfMouseEventArgs args)
	{
		base.OnMouseMove(args);
		if (!this.allowScrolling)
		{
			return;
		}
		bool flag = args is global::dfTouchEventArgs || this.isMouseDown;
		if (flag && (args.Position - this.touchStartPosition).magnitude > 5f)
		{
			Vector2 vector = args.MoveDelta.Scale(-1f, 1f);
			this.ScrollPosition += vector;
			this.scrollMomentum = (this.scrollMomentum + vector) * 0.5f;
		}
	}

	// Token: 0x06004786 RID: 18310 RVA: 0x0010E85C File Offset: 0x0010CA5C
	protected internal override void OnMouseWheel(global::dfMouseEventArgs args)
	{
		try
		{
			if (!args.Used && this.allowScrolling)
			{
				int num = (!this.UseScrollMomentum) ? 3 : 1;
				float num2 = (!(this.vertScrollbar != null)) ? ((float)(this.FontSize * num)) : this.vertScrollbar.IncrementAmount;
				this.ScrollPosition = new Vector2(this.scrollPosition.x, this.scrollPosition.y - num2 * args.WheelDelta);
				this.scrollMomentum = new Vector2(0f, -num2 * args.WheelDelta);
				args.Use();
				base.Signal("OnMouseWheel", new object[]
				{
					args
				});
			}
		}
		finally
		{
			base.OnMouseWheel(args);
		}
	}

	// Token: 0x06004787 RID: 18311 RVA: 0x0010E94C File Offset: 0x0010CB4C
	public void ScrollToTop()
	{
		this.ScrollPosition = new Vector2(this.scrollPosition.x, 0f);
	}

	// Token: 0x06004788 RID: 18312 RVA: 0x0010E96C File Offset: 0x0010CB6C
	public void ScrollToBottom()
	{
		this.ScrollPosition = new Vector2(this.scrollPosition.x, 2.14748365E+09f);
	}

	// Token: 0x06004789 RID: 18313 RVA: 0x0010E98C File Offset: 0x0010CB8C
	public void ScrollToLeft()
	{
		this.ScrollPosition = new Vector2(0f, this.scrollPosition.y);
	}

	// Token: 0x0600478A RID: 18314 RVA: 0x0010E9AC File Offset: 0x0010CBAC
	public void ScrollToRight()
	{
		this.ScrollPosition = new Vector2(2.14748365E+09f, this.scrollPosition.y);
	}

	// Token: 0x0600478B RID: 18315 RVA: 0x0010E9CC File Offset: 0x0010CBCC
	public global::dfList<global::dfRenderData> RenderMultiple()
	{
		if (!this.isVisible || this.Font == null)
		{
			return null;
		}
		if (!this.isControlInvalidated && this.viewportBox != null)
		{
			this.buffers.Clear();
			this.gatherRenderBuffers(this.viewportBox, this.buffers);
			return this.buffers;
		}
		global::dfList<global::dfRenderData> result;
		try
		{
			if (this.isMarkupInvalidated)
			{
				this.isMarkupInvalidated = false;
				this.processMarkup();
			}
			this.viewportBox.FitToContents(false);
			this.updateScrollbars();
			this.buffers.Clear();
			this.gatherRenderBuffers(this.viewportBox, this.buffers);
			result = this.buffers;
		}
		finally
		{
			this.isControlInvalidated = false;
		}
		return result;
	}

	// Token: 0x0600478C RID: 18316 RVA: 0x0010EAB0 File Offset: 0x0010CCB0
	private global::dfMarkupTag hitTestTag(global::dfMouseEventArgs args)
	{
		Vector2 point = base.GetHitPosition(args) + this.scrollPosition;
		global::dfMarkupBox dfMarkupBox = this.viewportBox.HitTest(point);
		if (dfMarkupBox != null)
		{
			global::dfMarkupElement dfMarkupElement = dfMarkupBox.Element;
			while (dfMarkupElement != null && !(dfMarkupElement is global::dfMarkupTag))
			{
				dfMarkupElement = dfMarkupElement.Parent;
			}
			return dfMarkupElement as global::dfMarkupTag;
		}
		return null;
	}

	// Token: 0x0600478D RID: 18317 RVA: 0x0010EB10 File Offset: 0x0010CD10
	private void processMarkup()
	{
		this.releaseMarkupReferences();
		this.elements = global::dfMarkupParser.Parse(this, this.text);
		float textScaleMultiplier = this.getTextScaleMultiplier();
		int num = Mathf.CeilToInt((float)this.FontSize * textScaleMultiplier);
		int lineHeight = Mathf.CeilToInt((float)this.LineHeight * textScaleMultiplier);
		global::dfMarkupStyle style = new global::dfMarkupStyle
		{
			Host = this,
			Atlas = this.Atlas,
			Font = this.Font,
			FontSize = num,
			FontStyle = this.FontStyle,
			LineHeight = lineHeight,
			Color = base.ApplyOpacity(base.Color),
			Opacity = base.CalculateOpacity(),
			Align = this.TextAlignment,
			PreserveWhitespace = this.preserveWhitespace
		};
		this.viewportBox = new global::dfMarkupBox(null, global::dfMarkupDisplayType.block, style)
		{
			Size = base.Size
		};
		for (int i = 0; i < this.elements.Count; i++)
		{
			global::dfMarkupElement dfMarkupElement = this.elements[i];
			if (dfMarkupElement != null)
			{
				dfMarkupElement.PerformLayout(this.viewportBox, style);
			}
		}
	}

	// Token: 0x0600478E RID: 18318 RVA: 0x0010EC4C File Offset: 0x0010CE4C
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
		return base.Size.y / this.startSize.y;
	}

	// Token: 0x0600478F RID: 18319 RVA: 0x0010ECB0 File Offset: 0x0010CEB0
	private void releaseMarkupReferences()
	{
		this.mouseDownTag = null;
		if (this.viewportBox != null)
		{
			this.viewportBox.Release();
		}
		if (this.elements != null)
		{
			for (int i = 0; i < this.elements.Count; i++)
			{
				this.elements[i].Release();
			}
			this.elements.Release();
		}
	}

	// Token: 0x06004790 RID: 18320 RVA: 0x0010ED20 File Offset: 0x0010CF20
	[HideInInspector]
	private void initialize()
	{
		if (this.initialized)
		{
			return;
		}
		this.initialized = true;
		if (Application.isPlaying)
		{
			if (this.horzScrollbar != null)
			{
				this.horzScrollbar.ValueChanged += this.horzScroll_ValueChanged;
			}
			if (this.vertScrollbar != null)
			{
				this.vertScrollbar.ValueChanged += this.vertScroll_ValueChanged;
			}
		}
		this.Invalidate();
		this.ScrollPosition = Vector2.zero;
		this.updateScrollbars();
	}

	// Token: 0x06004791 RID: 18321 RVA: 0x0010EDB4 File Offset: 0x0010CFB4
	private void vertScroll_ValueChanged(global::dfControl control, float value)
	{
		this.ScrollPosition = new Vector2(this.scrollPosition.x, value);
	}

	// Token: 0x06004792 RID: 18322 RVA: 0x0010EDD0 File Offset: 0x0010CFD0
	private void horzScroll_ValueChanged(global::dfControl control, float value)
	{
		this.ScrollPosition = new Vector2(value, this.ScrollPosition.y);
	}

	// Token: 0x06004793 RID: 18323 RVA: 0x0010EDF8 File Offset: 0x0010CFF8
	private void updateScrollbars()
	{
		if (this.horzScrollbar != null)
		{
			this.horzScrollbar.MinValue = 0f;
			this.horzScrollbar.MaxValue = this.ContentSize.x;
			this.horzScrollbar.ScrollSize = base.Size.x;
			this.horzScrollbar.Value = this.ScrollPosition.x;
		}
		if (this.vertScrollbar != null)
		{
			this.vertScrollbar.MinValue = 0f;
			this.vertScrollbar.MaxValue = this.ContentSize.y;
			this.vertScrollbar.ScrollSize = base.Size.y;
			this.vertScrollbar.Value = this.ScrollPosition.y;
		}
	}

	// Token: 0x06004794 RID: 18324 RVA: 0x0010EEE0 File Offset: 0x0010D0E0
	private void gatherRenderBuffers(global::dfMarkupBox box, global::dfList<global::dfRenderData> buffers)
	{
		global::dfIntersectionType viewportIntersection = this.getViewportIntersection(box);
		if (viewportIntersection == global::dfIntersectionType.None)
		{
			return;
		}
		global::dfRenderData dfRenderData = box.Render();
		if (dfRenderData != null)
		{
			if (dfRenderData.Material == null && this.atlas != null)
			{
				dfRenderData.Material = this.atlas.Material;
			}
			float num = base.PixelsToUnits();
			Vector2 vector = -this.scrollPosition.Scale(1f, -1f).RoundToInt();
			Vector3 vector2 = vector + box.GetOffset().Scale(1f, -1f) + this.pivot.TransformToUpperLeft(base.Size);
			global::dfList<Vector3> vertices = dfRenderData.Vertices;
			Matrix4x4 localToWorldMatrix = base.transform.localToWorldMatrix;
			for (int i = 0; i < dfRenderData.Vertices.Count; i++)
			{
				vertices[i] = localToWorldMatrix.MultiplyPoint((vector2 + vertices[i]) * num);
			}
			if (viewportIntersection == global::dfIntersectionType.Intersecting)
			{
				this.clipToViewport(dfRenderData);
			}
			buffers.Add(dfRenderData);
		}
		for (int j = 0; j < box.Children.Count; j++)
		{
			this.gatherRenderBuffers(box.Children[j], buffers);
		}
	}

	// Token: 0x06004795 RID: 18325 RVA: 0x0010F040 File Offset: 0x0010D240
	private global::dfIntersectionType getViewportIntersection(global::dfMarkupBox box)
	{
		if (box.Display == global::dfMarkupDisplayType.none)
		{
			return global::dfIntersectionType.None;
		}
		Vector2 size = base.Size;
		Vector2 vector = box.GetOffset() - this.scrollPosition;
		Vector2 vector2 = vector + box.Size;
		if (vector2.x <= 0f || vector2.y <= 0f)
		{
			return global::dfIntersectionType.None;
		}
		if (vector.x >= size.x || vector.y >= size.y)
		{
			return global::dfIntersectionType.None;
		}
		if (vector.x < 0f || vector.y < 0f || vector2.x > size.x || vector2.y > size.y)
		{
			return global::dfIntersectionType.Intersecting;
		}
		return global::dfIntersectionType.Inside;
	}

	// Token: 0x06004796 RID: 18326 RVA: 0x0010F11C File Offset: 0x0010D31C
	private void clipToViewport(global::dfRenderData renderData)
	{
		Plane[] clippingPlanes = this.GetClippingPlanes();
		Material material = renderData.Material;
		Matrix4x4 transform = renderData.Transform;
		global::dfRichTextLabel.clipBuffer.Clear();
		global::dfClippingUtil.Clip(clippingPlanes, renderData, global::dfRichTextLabel.clipBuffer);
		renderData.Clear();
		renderData.Merge(global::dfRichTextLabel.clipBuffer, false);
		renderData.Material = material;
		renderData.Transform = transform;
	}

	// Token: 0x04002579 RID: 9593
	[SerializeField]
	protected global::dfAtlas atlas;

	// Token: 0x0400257A RID: 9594
	[SerializeField]
	protected global::dfDynamicFont font;

	// Token: 0x0400257B RID: 9595
	[SerializeField]
	protected string text = "Rich Text Label";

	// Token: 0x0400257C RID: 9596
	[SerializeField]
	protected int fontSize = 16;

	// Token: 0x0400257D RID: 9597
	[SerializeField]
	protected int lineheight = 16;

	// Token: 0x0400257E RID: 9598
	[SerializeField]
	protected global::dfTextScaleMode textScaleMode;

	// Token: 0x0400257F RID: 9599
	[SerializeField]
	protected FontStyle fontStyle;

	// Token: 0x04002580 RID: 9600
	[SerializeField]
	protected bool preserveWhitespace;

	// Token: 0x04002581 RID: 9601
	[SerializeField]
	protected string blankTextureSprite;

	// Token: 0x04002582 RID: 9602
	[SerializeField]
	protected global::dfMarkupTextAlign align;

	// Token: 0x04002583 RID: 9603
	[SerializeField]
	protected bool allowScrolling;

	// Token: 0x04002584 RID: 9604
	[SerializeField]
	protected global::dfScrollbar horzScrollbar;

	// Token: 0x04002585 RID: 9605
	[SerializeField]
	protected global::dfScrollbar vertScrollbar;

	// Token: 0x04002586 RID: 9606
	[SerializeField]
	protected bool useScrollMomentum;

	// Token: 0x04002587 RID: 9607
	private static global::dfRenderData clipBuffer = new global::dfRenderData(32);

	// Token: 0x04002588 RID: 9608
	private global::dfList<global::dfRenderData> buffers = new global::dfList<global::dfRenderData>();

	// Token: 0x04002589 RID: 9609
	private global::dfList<global::dfMarkupElement> elements;

	// Token: 0x0400258A RID: 9610
	private global::dfMarkupBox viewportBox;

	// Token: 0x0400258B RID: 9611
	private global::dfMarkupTag mouseDownTag;

	// Token: 0x0400258C RID: 9612
	private Vector2 mouseDownScrollPosition = Vector2.zero;

	// Token: 0x0400258D RID: 9613
	private Vector2 scrollPosition = Vector2.zero;

	// Token: 0x0400258E RID: 9614
	private bool initialized;

	// Token: 0x0400258F RID: 9615
	private bool isMouseDown;

	// Token: 0x04002590 RID: 9616
	private Vector2 touchStartPosition = Vector2.zero;

	// Token: 0x04002591 RID: 9617
	private Vector2 scrollMomentum = Vector2.zero;

	// Token: 0x04002592 RID: 9618
	private bool isMarkupInvalidated = true;

	// Token: 0x04002593 RID: 9619
	private Vector2 startSize = Vector2.zero;

	// Token: 0x0200080D RID: 2061
	// (Invoke) Token: 0x06004798 RID: 18328
	[global::dfEventCategory("Markup")]
	public delegate void LinkClickEventHandler(global::dfRichTextLabel sender, global::dfMarkupTagAnchor tag);
}
