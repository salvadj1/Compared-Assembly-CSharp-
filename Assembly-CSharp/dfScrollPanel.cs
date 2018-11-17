using System;
using UnityEngine;

// Token: 0x020007C0 RID: 1984
[AddComponentMenu("Daikon Forge/User Interface/Containers/Scrollable Panel")]
[ExecuteInEditMode]
[Serializable]
public class dfScrollPanel : global::dfControl
{
	// Token: 0x1400004F RID: 79
	// (add) Token: 0x060043B2 RID: 17330 RVA: 0x000FAF08 File Offset: 0x000F9108
	// (remove) Token: 0x060043B3 RID: 17331 RVA: 0x000FAF24 File Offset: 0x000F9124
	public event global::PropertyChangedEventHandler<Vector2> ScrollPositionChanged;

	// Token: 0x17000D00 RID: 3328
	// (get) Token: 0x060043B4 RID: 17332 RVA: 0x000FAF40 File Offset: 0x000F9140
	// (set) Token: 0x060043B5 RID: 17333 RVA: 0x000FAF48 File Offset: 0x000F9148
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

	// Token: 0x17000D01 RID: 3329
	// (get) Token: 0x060043B6 RID: 17334 RVA: 0x000FAF5C File Offset: 0x000F915C
	// (set) Token: 0x060043B7 RID: 17335 RVA: 0x000FAF64 File Offset: 0x000F9164
	public bool ScrollWithArrowKeys
	{
		get
		{
			return this.scrollWithArrowKeys;
		}
		set
		{
			this.scrollWithArrowKeys = value;
		}
	}

	// Token: 0x17000D02 RID: 3330
	// (get) Token: 0x060043B8 RID: 17336 RVA: 0x000FAF70 File Offset: 0x000F9170
	// (set) Token: 0x060043B9 RID: 17337 RVA: 0x000FAFB8 File Offset: 0x000F91B8
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

	// Token: 0x17000D03 RID: 3331
	// (get) Token: 0x060043BA RID: 17338 RVA: 0x000FAFD8 File Offset: 0x000F91D8
	// (set) Token: 0x060043BB RID: 17339 RVA: 0x000FAFE0 File Offset: 0x000F91E0
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

	// Token: 0x17000D04 RID: 3332
	// (get) Token: 0x060043BC RID: 17340 RVA: 0x000FB000 File Offset: 0x000F9200
	// (set) Token: 0x060043BD RID: 17341 RVA: 0x000FB008 File Offset: 0x000F9208
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

	// Token: 0x17000D05 RID: 3333
	// (get) Token: 0x060043BE RID: 17342 RVA: 0x000FB040 File Offset: 0x000F9240
	// (set) Token: 0x060043BF RID: 17343 RVA: 0x000FB048 File Offset: 0x000F9248
	public bool AutoReset
	{
		get
		{
			return this.autoReset;
		}
		set
		{
			if (value != this.autoReset)
			{
				this.autoReset = value;
				if (value)
				{
					this.Reset();
				}
			}
		}
	}

	// Token: 0x17000D06 RID: 3334
	// (get) Token: 0x060043C0 RID: 17344 RVA: 0x000FB06C File Offset: 0x000F926C
	// (set) Token: 0x060043C1 RID: 17345 RVA: 0x000FB08C File Offset: 0x000F928C
	public RectOffset ScrollPadding
	{
		get
		{
			if (this.scrollPadding == null)
			{
				this.scrollPadding = new RectOffset();
			}
			return this.scrollPadding;
		}
		set
		{
			value = value.ConstrainPadding();
			if (!object.Equals(value, this.scrollPadding))
			{
				this.scrollPadding = value;
				this.Reset();
			}
		}
	}

	// Token: 0x17000D07 RID: 3335
	// (get) Token: 0x060043C2 RID: 17346 RVA: 0x000FB0C0 File Offset: 0x000F92C0
	// (set) Token: 0x060043C3 RID: 17347 RVA: 0x000FB0C8 File Offset: 0x000F92C8
	public bool AutoLayout
	{
		get
		{
			return this.autoLayout;
		}
		set
		{
			if (value != this.autoLayout)
			{
				this.autoLayout = value;
				this.Reset();
			}
		}
	}

	// Token: 0x17000D08 RID: 3336
	// (get) Token: 0x060043C4 RID: 17348 RVA: 0x000FB0E4 File Offset: 0x000F92E4
	// (set) Token: 0x060043C5 RID: 17349 RVA: 0x000FB0EC File Offset: 0x000F92EC
	public bool WrapLayout
	{
		get
		{
			return this.wrapLayout;
		}
		set
		{
			if (value != this.wrapLayout)
			{
				this.wrapLayout = value;
				this.Reset();
			}
		}
	}

	// Token: 0x17000D09 RID: 3337
	// (get) Token: 0x060043C6 RID: 17350 RVA: 0x000FB108 File Offset: 0x000F9308
	// (set) Token: 0x060043C7 RID: 17351 RVA: 0x000FB110 File Offset: 0x000F9310
	public global::dfScrollPanel.LayoutDirection FlowDirection
	{
		get
		{
			return this.flowDirection;
		}
		set
		{
			if (value != this.flowDirection)
			{
				this.flowDirection = value;
				this.Reset();
			}
		}
	}

	// Token: 0x17000D0A RID: 3338
	// (get) Token: 0x060043C8 RID: 17352 RVA: 0x000FB12C File Offset: 0x000F932C
	// (set) Token: 0x060043C9 RID: 17353 RVA: 0x000FB14C File Offset: 0x000F934C
	public RectOffset FlowPadding
	{
		get
		{
			if (this.flowPadding == null)
			{
				this.flowPadding = new RectOffset();
			}
			return this.flowPadding;
		}
		set
		{
			value = value.ConstrainPadding();
			if (!object.Equals(value, this.flowPadding))
			{
				this.flowPadding = value;
				this.Reset();
			}
		}
	}

	// Token: 0x17000D0B RID: 3339
	// (get) Token: 0x060043CA RID: 17354 RVA: 0x000FB180 File Offset: 0x000F9380
	// (set) Token: 0x060043CB RID: 17355 RVA: 0x000FB188 File Offset: 0x000F9388
	public Vector2 ScrollPosition
	{
		get
		{
			return this.scrollPosition;
		}
		set
		{
			Vector2 vector = this.calculateViewSize();
			Vector2 vector2;
			vector2..ctor(this.size.x - (float)this.scrollPadding.horizontal, this.size.y - (float)this.scrollPadding.vertical);
			value = Vector2.Min(vector - vector2, value);
			value = Vector2.Max(Vector2.zero, value);
			value = value.RoundToInt();
			if ((value - this.scrollPosition).sqrMagnitude > 1.401298E-45f)
			{
				Vector2 vector3 = value - this.scrollPosition;
				this.scrollPosition = value;
				this.scrollChildControls(vector3);
				this.updateScrollbars();
			}
			this.OnScrollPositionChanged();
		}
	}

	// Token: 0x17000D0C RID: 3340
	// (get) Token: 0x060043CC RID: 17356 RVA: 0x000FB244 File Offset: 0x000F9444
	// (set) Token: 0x060043CD RID: 17357 RVA: 0x000FB24C File Offset: 0x000F944C
	public int ScrollWheelAmount
	{
		get
		{
			return this.scrollWheelAmount;
		}
		set
		{
			this.scrollWheelAmount = value;
		}
	}

	// Token: 0x17000D0D RID: 3341
	// (get) Token: 0x060043CE RID: 17358 RVA: 0x000FB258 File Offset: 0x000F9458
	// (set) Token: 0x060043CF RID: 17359 RVA: 0x000FB260 File Offset: 0x000F9460
	public global::dfScrollbar HorzScrollbar
	{
		get
		{
			return this.horzScroll;
		}
		set
		{
			this.horzScroll = value;
			this.updateScrollbars();
		}
	}

	// Token: 0x17000D0E RID: 3342
	// (get) Token: 0x060043D0 RID: 17360 RVA: 0x000FB270 File Offset: 0x000F9470
	// (set) Token: 0x060043D1 RID: 17361 RVA: 0x000FB278 File Offset: 0x000F9478
	public global::dfScrollbar VertScrollbar
	{
		get
		{
			return this.vertScroll;
		}
		set
		{
			this.vertScroll = value;
			this.updateScrollbars();
		}
	}

	// Token: 0x17000D0F RID: 3343
	// (get) Token: 0x060043D2 RID: 17362 RVA: 0x000FB288 File Offset: 0x000F9488
	// (set) Token: 0x060043D3 RID: 17363 RVA: 0x000FB290 File Offset: 0x000F9490
	public global::dfControlOrientation WheelScrollDirection
	{
		get
		{
			return this.wheelDirection;
		}
		set
		{
			this.wheelDirection = value;
		}
	}

	// Token: 0x060043D4 RID: 17364 RVA: 0x000FB29C File Offset: 0x000F949C
	protected internal override Plane[] GetClippingPlanes()
	{
		if (!base.ClipChildren)
		{
			return null;
		}
		Vector3[] corners = base.GetCorners();
		Vector3 vector = base.transform.TransformDirection(Vector3.right);
		Vector3 vector2 = base.transform.TransformDirection(Vector3.left);
		Vector3 vector3 = base.transform.TransformDirection(Vector3.up);
		Vector3 vector4 = base.transform.TransformDirection(Vector3.down);
		float num = base.PixelsToUnits();
		RectOffset rectOffset = this.ScrollPadding;
		corners[0] += vector * (float)rectOffset.left * num + vector4 * (float)rectOffset.top * num;
		corners[1] += vector2 * (float)rectOffset.right * num + vector4 * (float)rectOffset.top * num;
		corners[2] += vector * (float)rectOffset.left * num + vector3 * (float)rectOffset.bottom * num;
		return new Plane[]
		{
			new Plane(vector, corners[0]),
			new Plane(vector2, corners[1]),
			new Plane(vector3, corners[2]),
			new Plane(vector4, corners[0])
		};
	}

	// Token: 0x17000D10 RID: 3344
	// (get) Token: 0x060043D5 RID: 17365 RVA: 0x000FB468 File Offset: 0x000F9668
	public override bool CanFocus
	{
		get
		{
			return (base.IsEnabled && base.IsVisible) || base.CanFocus;
		}
	}

	// Token: 0x060043D6 RID: 17366 RVA: 0x000FB488 File Offset: 0x000F9688
	public override void OnDestroy()
	{
		if (this.horzScroll != null)
		{
			this.horzScroll.ValueChanged -= this.horzScroll_ValueChanged;
		}
		if (this.vertScroll != null)
		{
			this.vertScroll.ValueChanged -= this.vertScroll_ValueChanged;
		}
		this.horzScroll = null;
		this.vertScroll = null;
	}

	// Token: 0x060043D7 RID: 17367 RVA: 0x000FB4F4 File Offset: 0x000F96F4
	public override void Update()
	{
		base.Update();
		if (this.useScrollMomentum && !this.isMouseDown && this.scrollMomentum.sqrMagnitude > 1.401298E-45f)
		{
			this.ScrollPosition += this.scrollMomentum;
		}
		if (this.isControlInvalidated && this.autoLayout && base.IsVisible)
		{
			this.AutoArrange();
			this.updateScrollbars();
		}
		this.scrollMomentum *= 0.95f - Time.deltaTime;
	}

	// Token: 0x060043D8 RID: 17368 RVA: 0x000FB594 File Offset: 0x000F9794
	public override void LateUpdate()
	{
		base.LateUpdate();
		this.initialize();
		if (this.resetNeeded && base.IsVisible)
		{
			this.resetNeeded = false;
			if (this.autoReset || this.autoLayout)
			{
				this.Reset();
			}
		}
	}

	// Token: 0x060043D9 RID: 17369 RVA: 0x000FB5E8 File Offset: 0x000F97E8
	public override void OnEnable()
	{
		base.OnEnable();
		if (this.size == Vector2.zero)
		{
			this.SuspendLayout();
			Camera camera = base.GetCamera();
			base.Size = new Vector3(camera.pixelWidth / 2f, camera.pixelHeight / 2f);
			this.ResumeLayout();
		}
		if (this.autoLayout)
		{
			this.AutoArrange();
		}
		this.updateScrollbars();
	}

	// Token: 0x060043DA RID: 17370 RVA: 0x000FB664 File Offset: 0x000F9864
	protected internal override void OnIsVisibleChanged()
	{
		base.OnIsVisibleChanged();
		if (base.IsVisible && (this.autoReset || this.autoLayout))
		{
			this.Reset();
			this.updateScrollbars();
		}
	}

	// Token: 0x060043DB RID: 17371 RVA: 0x000FB69C File Offset: 0x000F989C
	protected internal override void OnSizeChanged()
	{
		base.OnSizeChanged();
		if (this.autoReset || this.autoLayout)
		{
			this.Reset();
			return;
		}
		Vector2 vector = this.calculateMinChildPosition();
		if (vector.x > (float)this.scrollPadding.left || vector.y > (float)this.scrollPadding.top)
		{
			vector -= new Vector2((float)this.scrollPadding.left, (float)this.scrollPadding.top);
			vector = Vector2.Max(vector, Vector2.zero);
			this.scrollChildControls(vector);
		}
		this.updateScrollbars();
	}

	// Token: 0x060043DC RID: 17372 RVA: 0x000FB748 File Offset: 0x000F9948
	protected internal override void OnResolutionChanged(Vector2 previousResolution, Vector2 currentResolution)
	{
		base.OnResolutionChanged(previousResolution, currentResolution);
		this.resetNeeded = true;
	}

	// Token: 0x060043DD RID: 17373 RVA: 0x000FB75C File Offset: 0x000F995C
	protected internal override void OnGotFocus(global::dfFocusEventArgs args)
	{
		if (args.Source != this)
		{
			this.ScrollIntoView(args.Source);
		}
		base.OnGotFocus(args);
	}

	// Token: 0x060043DE RID: 17374 RVA: 0x000FB790 File Offset: 0x000F9990
	protected internal override void OnKeyDown(global::dfKeyEventArgs args)
	{
		if (!this.scrollWithArrowKeys || args.Used)
		{
			base.OnKeyDown(args);
			return;
		}
		float num = (!(this.horzScroll != null)) ? 1f : this.horzScroll.IncrementAmount;
		float num2 = (!(this.vertScroll != null)) ? 1f : this.vertScroll.IncrementAmount;
		if (args.KeyCode == 276)
		{
			this.ScrollPosition += new Vector2(-num, 0f);
			args.Use();
		}
		else if (args.KeyCode == 275)
		{
			this.ScrollPosition += new Vector2(num, 0f);
			args.Use();
		}
		else if (args.KeyCode == 273)
		{
			this.ScrollPosition += new Vector2(0f, -num2);
			args.Use();
		}
		else if (args.KeyCode == 274)
		{
			this.ScrollPosition += new Vector2(0f, num2);
			args.Use();
		}
		base.OnKeyDown(args);
	}

	// Token: 0x060043DF RID: 17375 RVA: 0x000FB8EC File Offset: 0x000F9AEC
	protected internal override void OnMouseEnter(global::dfMouseEventArgs args)
	{
		base.OnMouseEnter(args);
		this.touchStartPosition = args.Position;
	}

	// Token: 0x060043E0 RID: 17376 RVA: 0x000FB904 File Offset: 0x000F9B04
	protected internal override void OnMouseDown(global::dfMouseEventArgs args)
	{
		base.OnMouseDown(args);
		this.touchStartPosition = args.Position;
		this.isMouseDown = true;
	}

	// Token: 0x060043E1 RID: 17377 RVA: 0x000FB920 File Offset: 0x000F9B20
	internal override void OnDragStart(global::dfDragEventArgs args)
	{
		base.OnDragStart(args);
		if (args.Used)
		{
			this.isMouseDown = false;
		}
	}

	// Token: 0x060043E2 RID: 17378 RVA: 0x000FB93C File Offset: 0x000F9B3C
	protected internal override void OnMouseUp(global::dfMouseEventArgs args)
	{
		base.OnMouseUp(args);
		this.isMouseDown = false;
	}

	// Token: 0x060043E3 RID: 17379 RVA: 0x000FB94C File Offset: 0x000F9B4C
	protected internal override void OnMouseMove(global::dfMouseEventArgs args)
	{
		if ((args is global::dfTouchEventArgs || this.isMouseDown) && !args.Used && (args.Position - this.touchStartPosition).magnitude > 5f)
		{
			Vector2 vector = args.MoveDelta.Scale(-1f, 1f);
			this.ScrollPosition += vector;
			this.scrollMomentum = (this.scrollMomentum + vector) * 0.5f;
			args.Use();
		}
		base.OnMouseMove(args);
	}

	// Token: 0x060043E4 RID: 17380 RVA: 0x000FB9F0 File Offset: 0x000F9BF0
	protected internal override void OnMouseWheel(global::dfMouseEventArgs args)
	{
		try
		{
			if (!args.Used)
			{
				float num = (this.wheelDirection != global::dfControlOrientation.Horizontal) ? ((!(this.vertScroll != null)) ? ((float)this.scrollWheelAmount) : this.vertScroll.IncrementAmount) : ((!(this.horzScroll != null)) ? ((float)this.scrollWheelAmount) : this.horzScroll.IncrementAmount);
				if (this.wheelDirection == global::dfControlOrientation.Horizontal)
				{
					this.ScrollPosition = new Vector2(this.scrollPosition.x - num * args.WheelDelta, this.scrollPosition.y);
					this.scrollMomentum = new Vector2(-num * args.WheelDelta, 0f);
				}
				else
				{
					this.ScrollPosition = new Vector2(this.scrollPosition.x, this.scrollPosition.y - num * args.WheelDelta);
					this.scrollMomentum = new Vector2(0f, -num * args.WheelDelta);
				}
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

	// Token: 0x060043E5 RID: 17381 RVA: 0x000FBB4C File Offset: 0x000F9D4C
	protected internal override void OnControlAdded(global::dfControl child)
	{
		base.OnControlAdded(child);
		this.attachEvents(child);
		if (this.autoLayout)
		{
			this.AutoArrange();
		}
	}

	// Token: 0x060043E6 RID: 17382 RVA: 0x000FBB70 File Offset: 0x000F9D70
	protected internal override void OnControlRemoved(global::dfControl child)
	{
		base.OnControlRemoved(child);
		if (child != null)
		{
			this.detachEvents(child);
		}
		if (this.autoLayout)
		{
			this.AutoArrange();
		}
		else
		{
			this.updateScrollbars();
		}
	}

	// Token: 0x060043E7 RID: 17383 RVA: 0x000FBBB4 File Offset: 0x000F9DB4
	protected override void OnRebuildRenderData()
	{
		if (this.Atlas == null || string.IsNullOrEmpty(this.backgroundSprite))
		{
			return;
		}
		global::dfAtlas.ItemInfo itemInfo = this.Atlas[this.backgroundSprite];
		if (itemInfo == null)
		{
			return;
		}
		this.renderData.Material = this.Atlas.Material;
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

	// Token: 0x060043E8 RID: 17384 RVA: 0x000FBCCC File Offset: 0x000F9ECC
	protected internal void OnScrollPositionChanged()
	{
		this.Invalidate();
		base.SignalHierarchy("OnScrollPositionChanged", new object[]
		{
			this.ScrollPosition
		});
		if (this.ScrollPositionChanged != null)
		{
			this.ScrollPositionChanged(this, this.ScrollPosition);
		}
	}

	// Token: 0x060043E9 RID: 17385 RVA: 0x000FBD1C File Offset: 0x000F9F1C
	public void FitToContents()
	{
		if (this.controls.Count == 0)
		{
			return;
		}
		Vector2 vector = Vector2.zero;
		for (int i = 0; i < this.controls.Count; i++)
		{
			global::dfControl dfControl = this.controls[i];
			Vector2 vector2 = dfControl.RelativePosition + dfControl.Size;
			vector = Vector2.Max(vector, vector2);
		}
		base.Size = vector + new Vector2((float)this.scrollPadding.right, (float)this.scrollPadding.bottom);
	}

	// Token: 0x060043EA RID: 17386 RVA: 0x000FBDB4 File Offset: 0x000F9FB4
	public void CenterChildControls()
	{
		if (this.controls.Count == 0)
		{
			return;
		}
		Vector2 vector = Vector2.one * float.MaxValue;
		Vector2 vector2 = Vector2.one * float.MinValue;
		for (int i = 0; i < this.controls.Count; i++)
		{
			global::dfControl dfControl = this.controls[i];
			Vector2 vector3 = dfControl.RelativePosition;
			Vector2 vector4 = vector3 + dfControl.Size;
			vector = Vector2.Min(vector, vector3);
			vector2 = Vector2.Max(vector2, vector4);
		}
		Vector2 vector5 = vector2 - vector;
		Vector2 vector6 = (base.Size - vector5) * 0.5f;
		for (int j = 0; j < this.controls.Count; j++)
		{
			global::dfControl dfControl2 = this.controls[j];
			dfControl2.RelativePosition = dfControl2.RelativePosition - vector + vector6;
		}
	}

	// Token: 0x060043EB RID: 17387 RVA: 0x000FBEC0 File Offset: 0x000FA0C0
	public void ScrollToTop()
	{
		this.ScrollPosition = new Vector2(this.scrollPosition.x, 0f);
	}

	// Token: 0x060043EC RID: 17388 RVA: 0x000FBEE0 File Offset: 0x000FA0E0
	public void ScrollToBottom()
	{
		this.ScrollPosition = new Vector2(this.scrollPosition.x, 2.14748365E+09f);
	}

	// Token: 0x060043ED RID: 17389 RVA: 0x000FBF00 File Offset: 0x000FA100
	public void ScrollToLeft()
	{
		this.ScrollPosition = new Vector2(0f, this.scrollPosition.y);
	}

	// Token: 0x060043EE RID: 17390 RVA: 0x000FBF20 File Offset: 0x000FA120
	public void ScrollToRight()
	{
		this.ScrollPosition = new Vector2(2.14748365E+09f, this.scrollPosition.y);
	}

	// Token: 0x060043EF RID: 17391 RVA: 0x000FBF40 File Offset: 0x000FA140
	public void ScrollIntoView(global::dfControl control)
	{
		Rect rect = new Rect(this.scrollPosition.x + (float)this.scrollPadding.left, this.scrollPosition.y + (float)this.scrollPadding.top, this.size.x - (float)this.scrollPadding.horizontal, this.size.y - (float)this.scrollPadding.vertical).RoundToInt();
		Vector3 vector = control.RelativePosition;
		Vector2 size = control.Size;
		while (!this.controls.Contains(control))
		{
			control = control.Parent;
			vector += control.RelativePosition;
		}
		Rect other = new Rect(this.scrollPosition.x + vector.x, this.scrollPosition.y + vector.y, size.x, size.y).RoundToInt();
		if (rect.Contains(other))
		{
			return;
		}
		Vector2 vector2 = this.scrollPosition;
		if (other.xMin < rect.xMin)
		{
			vector2.x = other.xMin - (float)this.scrollPadding.left;
		}
		else if (other.xMax > rect.xMax)
		{
			vector2.x = other.xMax - Mathf.Max(this.size.x, size.x) + (float)this.scrollPadding.horizontal;
		}
		if (other.y < rect.y)
		{
			vector2.y = other.yMin - (float)this.scrollPadding.top;
		}
		else if (other.yMax > rect.yMax)
		{
			vector2.y = other.yMax - Mathf.Max(this.size.y, size.y) + (float)this.scrollPadding.vertical;
		}
		this.ScrollPosition = vector2;
		this.scrollMomentum = Vector2.zero;
	}

	// Token: 0x060043F0 RID: 17392 RVA: 0x000FC14C File Offset: 0x000FA34C
	public void Reset()
	{
		try
		{
			this.SuspendLayout();
			if (this.autoLayout)
			{
				Vector2 vector = this.ScrollPosition;
				this.ScrollPosition = Vector2.zero;
				this.AutoArrange();
				this.ScrollPosition = vector;
			}
			else
			{
				this.scrollPadding = this.ScrollPadding.ConstrainPadding();
				Vector3 vector2 = this.calculateMinChildPosition();
				vector2 -= new Vector3((float)this.scrollPadding.left, (float)this.scrollPadding.top);
				for (int i = 0; i < this.controls.Count; i++)
				{
					this.controls[i].RelativePosition -= vector2;
				}
				this.scrollPosition = Vector2.zero;
			}
			this.Invalidate();
			this.updateScrollbars();
		}
		finally
		{
			this.ResumeLayout();
		}
	}

	// Token: 0x060043F1 RID: 17393 RVA: 0x000FC248 File Offset: 0x000FA448
	[HideInInspector]
	private void AutoArrange()
	{
		this.SuspendLayout();
		try
		{
			this.scrollPadding = this.ScrollPadding.ConstrainPadding();
			this.flowPadding = this.FlowPadding.ConstrainPadding();
			float num = (float)this.scrollPadding.left + (float)this.flowPadding.left - this.scrollPosition.x;
			float num2 = (float)this.scrollPadding.top + (float)this.flowPadding.top - this.scrollPosition.y;
			float num3 = 0f;
			float num4 = 0f;
			for (int i = 0; i < this.controls.Count; i++)
			{
				global::dfControl dfControl = this.controls[i];
				if (dfControl.IsVisible && dfControl.enabled && dfControl.gameObject.activeSelf)
				{
					if (!(dfControl == this.horzScroll) && !(dfControl == this.vertScroll))
					{
						if (this.wrapLayout)
						{
							if (this.flowDirection == global::dfScrollPanel.LayoutDirection.Horizontal)
							{
								if (num + dfControl.Width >= this.size.x - (float)this.scrollPadding.right)
								{
									num = (float)this.scrollPadding.left + (float)this.flowPadding.left;
									num2 += num4;
									num4 = 0f;
								}
							}
							else if (num2 + dfControl.Height + (float)this.flowPadding.vertical >= this.size.y - (float)this.scrollPadding.bottom)
							{
								num2 = (float)this.scrollPadding.top + (float)this.flowPadding.top;
								num += num3;
								num3 = 0f;
							}
						}
						Vector2 vector;
						vector..ctor(num, num2);
						dfControl.RelativePosition = vector;
						float num5 = dfControl.Width + (float)this.flowPadding.horizontal;
						float num6 = dfControl.Height + (float)this.flowPadding.vertical;
						num3 = Mathf.Max(num5, num3);
						num4 = Mathf.Max(num6, num4);
						if (this.flowDirection == global::dfScrollPanel.LayoutDirection.Horizontal)
						{
							num += num5;
						}
						else
						{
							num2 += num6;
						}
					}
				}
			}
			this.updateScrollbars();
		}
		finally
		{
			this.ResumeLayout();
		}
	}

	// Token: 0x060043F2 RID: 17394 RVA: 0x000FC4B4 File Offset: 0x000FA6B4
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
			if (this.horzScroll != null)
			{
				this.horzScroll.ValueChanged += this.horzScroll_ValueChanged;
			}
			if (this.vertScroll != null)
			{
				this.vertScroll.ValueChanged += this.vertScroll_ValueChanged;
			}
		}
		if (this.resetNeeded || this.autoLayout || this.autoReset)
		{
			this.Reset();
		}
		this.Invalidate();
		this.ScrollPosition = Vector2.zero;
		this.updateScrollbars();
	}

	// Token: 0x060043F3 RID: 17395 RVA: 0x000FC56C File Offset: 0x000FA76C
	private void vertScroll_ValueChanged(global::dfControl control, float value)
	{
		this.ScrollPosition = new Vector2(this.scrollPosition.x, value);
	}

	// Token: 0x060043F4 RID: 17396 RVA: 0x000FC588 File Offset: 0x000FA788
	private void horzScroll_ValueChanged(global::dfControl control, float value)
	{
		this.ScrollPosition = new Vector2(value, this.ScrollPosition.y);
	}

	// Token: 0x060043F5 RID: 17397 RVA: 0x000FC5B0 File Offset: 0x000FA7B0
	private void scrollChildControls(Vector3 delta)
	{
		try
		{
			this.scrolling = true;
			delta = delta.Scale(1f, -1f, 1f);
			for (int i = 0; i < this.controls.Count; i++)
			{
				global::dfControl dfControl = this.controls[i];
				dfControl.Position = (dfControl.Position - delta).RoundToInt();
			}
		}
		finally
		{
			this.scrolling = false;
		}
	}

	// Token: 0x060043F6 RID: 17398 RVA: 0x000FC644 File Offset: 0x000FA844
	private Vector2 calculateMinChildPosition()
	{
		float num = float.MaxValue;
		float num2 = float.MaxValue;
		for (int i = 0; i < this.controls.Count; i++)
		{
			global::dfControl dfControl = this.controls[i];
			if (dfControl.enabled && dfControl.gameObject.activeSelf)
			{
				Vector3 vector = dfControl.RelativePosition.FloorToInt();
				num = Mathf.Min(num, vector.x);
				num2 = Mathf.Min(num2, vector.y);
			}
		}
		return new Vector2(num, num2);
	}

	// Token: 0x060043F7 RID: 17399 RVA: 0x000FC6D8 File Offset: 0x000FA8D8
	private Vector2 calculateViewSize()
	{
		Vector2 vector = new Vector2((float)this.scrollPadding.horizontal, (float)this.scrollPadding.vertical).RoundToInt();
		Vector2 vector2 = base.Size.RoundToInt() - vector;
		if (!base.IsVisible || this.controls.Count == 0)
		{
			return vector2;
		}
		Vector2 vector3 = Vector2.one * float.MaxValue;
		Vector2 vector4 = Vector2.one * float.MinValue;
		for (int i = 0; i < this.controls.Count; i++)
		{
			global::dfControl dfControl = this.controls[i];
			if (!Application.isPlaying || dfControl.IsVisible)
			{
				Vector2 vector5 = dfControl.RelativePosition.RoundToInt();
				Vector2 vector6 = vector5 + dfControl.Size.RoundToInt();
				vector3 = Vector2.Min(vector5, vector3);
				vector4 = Vector2.Max(vector6, vector4);
			}
		}
		vector4 = Vector2.Max(vector4, vector2);
		return vector4 - vector3;
	}

	// Token: 0x060043F8 RID: 17400 RVA: 0x000FC7EC File Offset: 0x000FA9EC
	[HideInInspector]
	private void updateScrollbars()
	{
		Vector2 vector = this.calculateViewSize();
		Vector2 vector2 = base.Size - new Vector2((float)this.scrollPadding.horizontal, (float)this.scrollPadding.vertical);
		if (this.horzScroll != null)
		{
			this.horzScroll.MinValue = 0f;
			this.horzScroll.MaxValue = vector.x;
			this.horzScroll.ScrollSize = vector2.x;
			this.horzScroll.Value = Mathf.Max(0f, this.scrollPosition.x);
		}
		if (this.vertScroll != null)
		{
			this.vertScroll.MinValue = 0f;
			this.vertScroll.MaxValue = vector.y;
			this.vertScroll.ScrollSize = vector2.y;
			this.vertScroll.Value = Mathf.Max(0f, this.scrollPosition.y);
		}
	}

	// Token: 0x060043F9 RID: 17401 RVA: 0x000FC8F4 File Offset: 0x000FAAF4
	private void attachEvents(global::dfControl control)
	{
		control.IsVisibleChanged += this.childIsVisibleChanged;
		control.PositionChanged += this.childControlInvalidated;
		control.SizeChanged += this.childControlInvalidated;
		control.ZOrderChanged += this.childOrderChanged;
	}

	// Token: 0x060043FA RID: 17402 RVA: 0x000FC94C File Offset: 0x000FAB4C
	private void detachEvents(global::dfControl control)
	{
		control.IsVisibleChanged -= this.childIsVisibleChanged;
		control.PositionChanged -= this.childControlInvalidated;
		control.SizeChanged -= this.childControlInvalidated;
		control.ZOrderChanged -= this.childOrderChanged;
	}

	// Token: 0x060043FB RID: 17403 RVA: 0x000FC9A4 File Offset: 0x000FABA4
	private void childOrderChanged(global::dfControl control, int value)
	{
		this.onChildControlInvalidatedLayout();
	}

	// Token: 0x060043FC RID: 17404 RVA: 0x000FC9AC File Offset: 0x000FABAC
	private void childIsVisibleChanged(global::dfControl control, bool value)
	{
		this.onChildControlInvalidatedLayout();
	}

	// Token: 0x060043FD RID: 17405 RVA: 0x000FC9B4 File Offset: 0x000FABB4
	private void childControlInvalidated(global::dfControl control, Vector2 value)
	{
		this.onChildControlInvalidatedLayout();
	}

	// Token: 0x060043FE RID: 17406 RVA: 0x000FC9BC File Offset: 0x000FABBC
	[HideInInspector]
	private void onChildControlInvalidatedLayout()
	{
		if (this.scrolling || base.IsLayoutSuspended)
		{
			return;
		}
		if (this.autoLayout)
		{
			this.AutoArrange();
		}
		this.updateScrollbars();
		this.Invalidate();
	}

	// Token: 0x040023F6 RID: 9206
	[SerializeField]
	protected global::dfAtlas atlas;

	// Token: 0x040023F7 RID: 9207
	[SerializeField]
	protected string backgroundSprite;

	// Token: 0x040023F8 RID: 9208
	[SerializeField]
	protected Color32 backgroundColor = UnityEngine.Color.white;

	// Token: 0x040023F9 RID: 9209
	[SerializeField]
	protected bool autoReset = true;

	// Token: 0x040023FA RID: 9210
	[SerializeField]
	protected bool autoLayout;

	// Token: 0x040023FB RID: 9211
	[SerializeField]
	protected RectOffset scrollPadding = new RectOffset();

	// Token: 0x040023FC RID: 9212
	[SerializeField]
	protected RectOffset flowPadding = new RectOffset();

	// Token: 0x040023FD RID: 9213
	[SerializeField]
	protected global::dfScrollPanel.LayoutDirection flowDirection;

	// Token: 0x040023FE RID: 9214
	[SerializeField]
	protected bool wrapLayout;

	// Token: 0x040023FF RID: 9215
	[SerializeField]
	protected Vector2 scrollPosition = Vector2.zero;

	// Token: 0x04002400 RID: 9216
	[SerializeField]
	protected int scrollWheelAmount = 10;

	// Token: 0x04002401 RID: 9217
	[SerializeField]
	protected global::dfScrollbar horzScroll;

	// Token: 0x04002402 RID: 9218
	[SerializeField]
	protected global::dfScrollbar vertScroll;

	// Token: 0x04002403 RID: 9219
	[SerializeField]
	protected global::dfControlOrientation wheelDirection;

	// Token: 0x04002404 RID: 9220
	[SerializeField]
	protected bool scrollWithArrowKeys;

	// Token: 0x04002405 RID: 9221
	[SerializeField]
	protected bool useScrollMomentum;

	// Token: 0x04002406 RID: 9222
	private bool initialized;

	// Token: 0x04002407 RID: 9223
	private bool resetNeeded;

	// Token: 0x04002408 RID: 9224
	private bool scrolling;

	// Token: 0x04002409 RID: 9225
	private bool isMouseDown;

	// Token: 0x0400240A RID: 9226
	private Vector2 touchStartPosition = Vector2.zero;

	// Token: 0x0400240B RID: 9227
	private Vector2 scrollMomentum = Vector2.zero;

	// Token: 0x020007C1 RID: 1985
	public enum LayoutDirection
	{
		// Token: 0x0400240E RID: 9230
		Horizontal,
		// Token: 0x0400240F RID: 9231
		Vertical
	}
}
