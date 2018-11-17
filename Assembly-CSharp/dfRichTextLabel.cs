using System;
using UnityEngine;

// Token: 0x02000730 RID: 1840
[RequireComponent(typeof(BoxCollider))]
[AddComponentMenu("Daikon Forge/User Interface/Rich Text Label")]
[ExecuteInEditMode]
[Serializable]
public class dfRichTextLabel : dfControl, IDFMultiRender
{
	// Token: 0x14000062 RID: 98
	// (add) Token: 0x0600430F RID: 17167 RVA: 0x00104CE0 File Offset: 0x00102EE0
	// (remove) Token: 0x06004310 RID: 17168 RVA: 0x00104CFC File Offset: 0x00102EFC
	public event PropertyChangedEventHandler<string> TextChanged;

	// Token: 0x14000063 RID: 99
	// (add) Token: 0x06004311 RID: 17169 RVA: 0x00104D18 File Offset: 0x00102F18
	// (remove) Token: 0x06004312 RID: 17170 RVA: 0x00104D34 File Offset: 0x00102F34
	public event PropertyChangedEventHandler<Vector2> ScrollPositionChanged;

	// Token: 0x14000064 RID: 100
	// (add) Token: 0x06004313 RID: 17171 RVA: 0x00104D50 File Offset: 0x00102F50
	// (remove) Token: 0x06004314 RID: 17172 RVA: 0x00104D6C File Offset: 0x00102F6C
	public event dfRichTextLabel.LinkClickEventHandler LinkClicked;

	// Token: 0x17000D25 RID: 3365
	// (get) Token: 0x06004315 RID: 17173 RVA: 0x00104D88 File Offset: 0x00102F88
	// (set) Token: 0x06004316 RID: 17174 RVA: 0x00104DD0 File Offset: 0x00102FD0
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

	// Token: 0x17000D26 RID: 3366
	// (get) Token: 0x06004317 RID: 17175 RVA: 0x00104DF0 File Offset: 0x00102FF0
	// (set) Token: 0x06004318 RID: 17176 RVA: 0x00104DF8 File Offset: 0x00102FF8
	public dfDynamicFont Font
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

	// Token: 0x17000D27 RID: 3367
	// (get) Token: 0x06004319 RID: 17177 RVA: 0x00104E30 File Offset: 0x00103030
	// (set) Token: 0x0600431A RID: 17178 RVA: 0x00104E38 File Offset: 0x00103038
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

	// Token: 0x17000D28 RID: 3368
	// (get) Token: 0x0600431B RID: 17179 RVA: 0x00104E58 File Offset: 0x00103058
	// (set) Token: 0x0600431C RID: 17180 RVA: 0x00104E60 File Offset: 0x00103060
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

	// Token: 0x17000D29 RID: 3369
	// (get) Token: 0x0600431D RID: 17181 RVA: 0x00104EA8 File Offset: 0x001030A8
	// (set) Token: 0x0600431E RID: 17182 RVA: 0x00104EB0 File Offset: 0x001030B0
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

	// Token: 0x17000D2A RID: 3370
	// (get) Token: 0x0600431F RID: 17183 RVA: 0x00104EDC File Offset: 0x001030DC
	// (set) Token: 0x06004320 RID: 17184 RVA: 0x00104EE4 File Offset: 0x001030E4
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

	// Token: 0x17000D2B RID: 3371
	// (get) Token: 0x06004321 RID: 17185 RVA: 0x00104F10 File Offset: 0x00103110
	// (set) Token: 0x06004322 RID: 17186 RVA: 0x00104F18 File Offset: 0x00103118
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

	// Token: 0x17000D2C RID: 3372
	// (get) Token: 0x06004323 RID: 17187 RVA: 0x00104F28 File Offset: 0x00103128
	// (set) Token: 0x06004324 RID: 17188 RVA: 0x00104F30 File Offset: 0x00103130
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

	// Token: 0x17000D2D RID: 3373
	// (get) Token: 0x06004325 RID: 17189 RVA: 0x00104F4C File Offset: 0x0010314C
	// (set) Token: 0x06004326 RID: 17190 RVA: 0x00104F54 File Offset: 0x00103154
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

	// Token: 0x17000D2E RID: 3374
	// (get) Token: 0x06004327 RID: 17191 RVA: 0x00104F70 File Offset: 0x00103170
	// (set) Token: 0x06004328 RID: 17192 RVA: 0x00104F78 File Offset: 0x00103178
	public dfMarkupTextAlign TextAlignment
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

	// Token: 0x17000D2F RID: 3375
	// (get) Token: 0x06004329 RID: 17193 RVA: 0x00104F94 File Offset: 0x00103194
	// (set) Token: 0x0600432A RID: 17194 RVA: 0x00104F9C File Offset: 0x0010319C
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

	// Token: 0x17000D30 RID: 3376
	// (get) Token: 0x0600432B RID: 17195 RVA: 0x00104FB8 File Offset: 0x001031B8
	// (set) Token: 0x0600432C RID: 17196 RVA: 0x00104FC0 File Offset: 0x001031C0
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

	// Token: 0x17000D31 RID: 3377
	// (get) Token: 0x0600432D RID: 17197 RVA: 0x00105040 File Offset: 0x00103240
	// (set) Token: 0x0600432E RID: 17198 RVA: 0x00105048 File Offset: 0x00103248
	public dfScrollbar HorizontalScrollbar
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

	// Token: 0x17000D32 RID: 3378
	// (get) Token: 0x0600432F RID: 17199 RVA: 0x00105058 File Offset: 0x00103258
	// (set) Token: 0x06004330 RID: 17200 RVA: 0x00105060 File Offset: 0x00103260
	public dfScrollbar VerticalScrollbar
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

	// Token: 0x17000D33 RID: 3379
	// (get) Token: 0x06004331 RID: 17201 RVA: 0x00105070 File Offset: 0x00103270
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

	// Token: 0x17000D34 RID: 3380
	// (get) Token: 0x06004332 RID: 17202 RVA: 0x00105090 File Offset: 0x00103290
	// (set) Token: 0x06004333 RID: 17203 RVA: 0x00105098 File Offset: 0x00103298
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

	// Token: 0x06004334 RID: 17204 RVA: 0x001050AC File Offset: 0x001032AC
	protected internal override void OnLocalize()
	{
		base.OnLocalize();
		this.Text = base.getLocalizedValue(this.text);
	}

	// Token: 0x06004335 RID: 17205 RVA: 0x001050C8 File Offset: 0x001032C8
	public override void Invalidate()
	{
		base.Invalidate();
		this.isMarkupInvalidated = true;
	}

	// Token: 0x06004336 RID: 17206 RVA: 0x001050D8 File Offset: 0x001032D8
	public override void Awake()
	{
		base.Awake();
		this.startSize = base.Size;
	}

	// Token: 0x06004337 RID: 17207 RVA: 0x001050EC File Offset: 0x001032EC
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

	// Token: 0x06004338 RID: 17208 RVA: 0x0010513C File Offset: 0x0010333C
	public override void Update()
	{
		base.Update();
		if (this.useScrollMomentum && !this.isMouseDown && this.scrollMomentum.magnitude > 0.1f)
		{
			this.ScrollPosition += this.scrollMomentum;
			this.scrollMomentum *= 0.95f - Time.deltaTime;
		}
	}

	// Token: 0x06004339 RID: 17209 RVA: 0x001051B0 File Offset: 0x001033B0
	public override void LateUpdate()
	{
		base.LateUpdate();
		this.initialize();
	}

	// Token: 0x0600433A RID: 17210 RVA: 0x001051C0 File Offset: 0x001033C0
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

	// Token: 0x0600433B RID: 17211 RVA: 0x0010520C File Offset: 0x0010340C
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

	// Token: 0x0600433C RID: 17212 RVA: 0x0010525C File Offset: 0x0010345C
	protected internal override void OnKeyDown(dfKeyEventArgs args)
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

	// Token: 0x0600433D RID: 17213 RVA: 0x00105370 File Offset: 0x00103570
	internal override void OnDragEnd(dfDragEventArgs args)
	{
		base.OnDragEnd(args);
		this.isMouseDown = false;
	}

	// Token: 0x0600433E RID: 17214 RVA: 0x00105380 File Offset: 0x00103580
	protected internal override void OnMouseEnter(dfMouseEventArgs args)
	{
		base.OnMouseEnter(args);
		this.touchStartPosition = args.Position;
	}

	// Token: 0x0600433F RID: 17215 RVA: 0x00105398 File Offset: 0x00103598
	protected internal override void OnMouseDown(dfMouseEventArgs args)
	{
		base.OnMouseDown(args);
		this.mouseDownTag = this.hitTestTag(args);
		this.mouseDownScrollPosition = this.scrollPosition;
		this.scrollMomentum = Vector2.zero;
		this.touchStartPosition = args.Position;
		this.isMouseDown = true;
	}

	// Token: 0x06004340 RID: 17216 RVA: 0x001053E4 File Offset: 0x001035E4
	protected internal override void OnMouseUp(dfMouseEventArgs args)
	{
		base.OnMouseUp(args);
		this.isMouseDown = false;
		if (Vector2.Distance(this.scrollPosition, this.mouseDownScrollPosition) <= 2f && this.hitTestTag(args) == this.mouseDownTag)
		{
			dfMarkupTag dfMarkupTag = this.mouseDownTag;
			while (dfMarkupTag != null && !(dfMarkupTag is dfMarkupTagAnchor))
			{
				dfMarkupTag = (dfMarkupTag.Parent as dfMarkupTag);
			}
			if (dfMarkupTag is dfMarkupTagAnchor)
			{
				base.Signal("OnLinkClicked", new object[]
				{
					dfMarkupTag
				});
				if (this.LinkClicked != null)
				{
					this.LinkClicked(this, dfMarkupTag as dfMarkupTagAnchor);
				}
			}
		}
		this.mouseDownTag = null;
		this.mouseDownScrollPosition = this.scrollPosition;
	}

	// Token: 0x06004341 RID: 17217 RVA: 0x001054A8 File Offset: 0x001036A8
	protected internal override void OnMouseMove(dfMouseEventArgs args)
	{
		base.OnMouseMove(args);
		if (!this.allowScrolling)
		{
			return;
		}
		bool flag = args is dfTouchEventArgs || this.isMouseDown;
		if (flag && (args.Position - this.touchStartPosition).magnitude > 5f)
		{
			Vector2 vector = args.MoveDelta.Scale(-1f, 1f);
			this.ScrollPosition += vector;
			this.scrollMomentum = (this.scrollMomentum + vector) * 0.5f;
		}
	}

	// Token: 0x06004342 RID: 17218 RVA: 0x0010554C File Offset: 0x0010374C
	protected internal override void OnMouseWheel(dfMouseEventArgs args)
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

	// Token: 0x06004343 RID: 17219 RVA: 0x0010563C File Offset: 0x0010383C
	public void ScrollToTop()
	{
		this.ScrollPosition = new Vector2(this.scrollPosition.x, 0f);
	}

	// Token: 0x06004344 RID: 17220 RVA: 0x0010565C File Offset: 0x0010385C
	public void ScrollToBottom()
	{
		this.ScrollPosition = new Vector2(this.scrollPosition.x, 2.14748365E+09f);
	}

	// Token: 0x06004345 RID: 17221 RVA: 0x0010567C File Offset: 0x0010387C
	public void ScrollToLeft()
	{
		this.ScrollPosition = new Vector2(0f, this.scrollPosition.y);
	}

	// Token: 0x06004346 RID: 17222 RVA: 0x0010569C File Offset: 0x0010389C
	public void ScrollToRight()
	{
		this.ScrollPosition = new Vector2(2.14748365E+09f, this.scrollPosition.y);
	}

	// Token: 0x06004347 RID: 17223 RVA: 0x001056BC File Offset: 0x001038BC
	public dfList<dfRenderData> RenderMultiple()
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
		dfList<dfRenderData> result;
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

	// Token: 0x06004348 RID: 17224 RVA: 0x001057A0 File Offset: 0x001039A0
	private dfMarkupTag hitTestTag(dfMouseEventArgs args)
	{
		Vector2 point = base.GetHitPosition(args) + this.scrollPosition;
		dfMarkupBox dfMarkupBox = this.viewportBox.HitTest(point);
		if (dfMarkupBox != null)
		{
			dfMarkupElement dfMarkupElement = dfMarkupBox.Element;
			while (dfMarkupElement != null && !(dfMarkupElement is dfMarkupTag))
			{
				dfMarkupElement = dfMarkupElement.Parent;
			}
			return dfMarkupElement as dfMarkupTag;
		}
		return null;
	}

	// Token: 0x06004349 RID: 17225 RVA: 0x00105800 File Offset: 0x00103A00
	private void processMarkup()
	{
		this.releaseMarkupReferences();
		this.elements = dfMarkupParser.Parse(this, this.text);
		float textScaleMultiplier = this.getTextScaleMultiplier();
		int num = Mathf.CeilToInt((float)this.FontSize * textScaleMultiplier);
		int lineHeight = Mathf.CeilToInt((float)this.LineHeight * textScaleMultiplier);
		dfMarkupStyle style = new dfMarkupStyle
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
		this.viewportBox = new dfMarkupBox(null, dfMarkupDisplayType.block, style)
		{
			Size = base.Size
		};
		for (int i = 0; i < this.elements.Count; i++)
		{
			dfMarkupElement dfMarkupElement = this.elements[i];
			if (dfMarkupElement != null)
			{
				dfMarkupElement.PerformLayout(this.viewportBox, style);
			}
		}
	}

	// Token: 0x0600434A RID: 17226 RVA: 0x0010593C File Offset: 0x00103B3C
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
		return base.Size.y / this.startSize.y;
	}

	// Token: 0x0600434B RID: 17227 RVA: 0x001059A0 File Offset: 0x00103BA0
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

	// Token: 0x0600434C RID: 17228 RVA: 0x00105A10 File Offset: 0x00103C10
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

	// Token: 0x0600434D RID: 17229 RVA: 0x00105AA4 File Offset: 0x00103CA4
	private void vertScroll_ValueChanged(dfControl control, float value)
	{
		this.ScrollPosition = new Vector2(this.scrollPosition.x, value);
	}

	// Token: 0x0600434E RID: 17230 RVA: 0x00105AC0 File Offset: 0x00103CC0
	private void horzScroll_ValueChanged(dfControl control, float value)
	{
		this.ScrollPosition = new Vector2(value, this.ScrollPosition.y);
	}

	// Token: 0x0600434F RID: 17231 RVA: 0x00105AE8 File Offset: 0x00103CE8
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

	// Token: 0x06004350 RID: 17232 RVA: 0x00105BD0 File Offset: 0x00103DD0
	private void gatherRenderBuffers(dfMarkupBox box, dfList<dfRenderData> buffers)
	{
		dfIntersectionType viewportIntersection = this.getViewportIntersection(box);
		if (viewportIntersection == dfIntersectionType.None)
		{
			return;
		}
		dfRenderData dfRenderData = box.Render();
		if (dfRenderData != null)
		{
			if (dfRenderData.Material == null && this.atlas != null)
			{
				dfRenderData.Material = this.atlas.Material;
			}
			float num = base.PixelsToUnits();
			Vector2 vector = -this.scrollPosition.Scale(1f, -1f).RoundToInt();
			Vector3 vector2 = vector + box.GetOffset().Scale(1f, -1f) + this.pivot.TransformToUpperLeft(base.Size);
			dfList<Vector3> vertices = dfRenderData.Vertices;
			Matrix4x4 localToWorldMatrix = base.transform.localToWorldMatrix;
			for (int i = 0; i < dfRenderData.Vertices.Count; i++)
			{
				vertices[i] = localToWorldMatrix.MultiplyPoint((vector2 + vertices[i]) * num);
			}
			if (viewportIntersection == dfIntersectionType.Intersecting)
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

	// Token: 0x06004351 RID: 17233 RVA: 0x00105D30 File Offset: 0x00103F30
	private dfIntersectionType getViewportIntersection(dfMarkupBox box)
	{
		if (box.Display == dfMarkupDisplayType.none)
		{
			return dfIntersectionType.None;
		}
		Vector2 size = base.Size;
		Vector2 vector = box.GetOffset() - this.scrollPosition;
		Vector2 vector2 = vector + box.Size;
		if (vector2.x <= 0f || vector2.y <= 0f)
		{
			return dfIntersectionType.None;
		}
		if (vector.x >= size.x || vector.y >= size.y)
		{
			return dfIntersectionType.None;
		}
		if (vector.x < 0f || vector.y < 0f || vector2.x > size.x || vector2.y > size.y)
		{
			return dfIntersectionType.Intersecting;
		}
		return dfIntersectionType.Inside;
	}

	// Token: 0x06004352 RID: 17234 RVA: 0x00105E0C File Offset: 0x0010400C
	private void clipToViewport(dfRenderData renderData)
	{
		Plane[] clippingPlanes = this.GetClippingPlanes();
		Material material = renderData.Material;
		Matrix4x4 transform = renderData.Transform;
		dfRichTextLabel.clipBuffer.Clear();
		dfClippingUtil.Clip(clippingPlanes, renderData, dfRichTextLabel.clipBuffer);
		renderData.Clear();
		renderData.Merge(dfRichTextLabel.clipBuffer, false);
		renderData.Material = material;
		renderData.Transform = transform;
	}

	// Token: 0x04002356 RID: 9046
	[SerializeField]
	protected dfAtlas atlas;

	// Token: 0x04002357 RID: 9047
	[SerializeField]
	protected dfDynamicFont font;

	// Token: 0x04002358 RID: 9048
	[SerializeField]
	protected string text = "Rich Text Label";

	// Token: 0x04002359 RID: 9049
	[SerializeField]
	protected int fontSize = 16;

	// Token: 0x0400235A RID: 9050
	[SerializeField]
	protected int lineheight = 16;

	// Token: 0x0400235B RID: 9051
	[SerializeField]
	protected dfTextScaleMode textScaleMode;

	// Token: 0x0400235C RID: 9052
	[SerializeField]
	protected FontStyle fontStyle;

	// Token: 0x0400235D RID: 9053
	[SerializeField]
	protected bool preserveWhitespace;

	// Token: 0x0400235E RID: 9054
	[SerializeField]
	protected string blankTextureSprite;

	// Token: 0x0400235F RID: 9055
	[SerializeField]
	protected dfMarkupTextAlign align;

	// Token: 0x04002360 RID: 9056
	[SerializeField]
	protected bool allowScrolling;

	// Token: 0x04002361 RID: 9057
	[SerializeField]
	protected dfScrollbar horzScrollbar;

	// Token: 0x04002362 RID: 9058
	[SerializeField]
	protected dfScrollbar vertScrollbar;

	// Token: 0x04002363 RID: 9059
	[SerializeField]
	protected bool useScrollMomentum;

	// Token: 0x04002364 RID: 9060
	private static dfRenderData clipBuffer = new dfRenderData(32);

	// Token: 0x04002365 RID: 9061
	private dfList<dfRenderData> buffers = new dfList<dfRenderData>();

	// Token: 0x04002366 RID: 9062
	private dfList<dfMarkupElement> elements;

	// Token: 0x04002367 RID: 9063
	private dfMarkupBox viewportBox;

	// Token: 0x04002368 RID: 9064
	private dfMarkupTag mouseDownTag;

	// Token: 0x04002369 RID: 9065
	private Vector2 mouseDownScrollPosition = Vector2.zero;

	// Token: 0x0400236A RID: 9066
	private Vector2 scrollPosition = Vector2.zero;

	// Token: 0x0400236B RID: 9067
	private bool initialized;

	// Token: 0x0400236C RID: 9068
	private bool isMouseDown;

	// Token: 0x0400236D RID: 9069
	private Vector2 touchStartPosition = Vector2.zero;

	// Token: 0x0400236E RID: 9070
	private Vector2 scrollMomentum = Vector2.zero;

	// Token: 0x0400236F RID: 9071
	private bool isMarkupInvalidated = true;

	// Token: 0x04002370 RID: 9072
	private Vector2 startSize = Vector2.zero;

	// Token: 0x020008E2 RID: 2274
	// (Invoke) Token: 0x06004D60 RID: 19808
	[dfEventCategory("Markup")]
	public delegate void LinkClickEventHandler(dfRichTextLabel sender, dfMarkupTagAnchor tag);
}
