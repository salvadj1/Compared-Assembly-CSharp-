using System;
using UnityEngine;

// Token: 0x020006EE RID: 1774
[AddComponentMenu("Daikon Forge/User Interface/Containers/Scrollable Panel")]
[ExecuteInEditMode]
[Serializable]
public class dfScrollPanel : dfControl
{
	// Token: 0x1400004F RID: 79
	// (add) Token: 0x06003F96 RID: 16278 RVA: 0x000F2304 File Offset: 0x000F0504
	// (remove) Token: 0x06003F97 RID: 16279 RVA: 0x000F2320 File Offset: 0x000F0520
	public event PropertyChangedEventHandler<Vector2> ScrollPositionChanged;

	// Token: 0x17000C7C RID: 3196
	// (get) Token: 0x06003F98 RID: 16280 RVA: 0x000F233C File Offset: 0x000F053C
	// (set) Token: 0x06003F99 RID: 16281 RVA: 0x000F2344 File Offset: 0x000F0544
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

	// Token: 0x17000C7D RID: 3197
	// (get) Token: 0x06003F9A RID: 16282 RVA: 0x000F2358 File Offset: 0x000F0558
	// (set) Token: 0x06003F9B RID: 16283 RVA: 0x000F2360 File Offset: 0x000F0560
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

	// Token: 0x17000C7E RID: 3198
	// (get) Token: 0x06003F9C RID: 16284 RVA: 0x000F236C File Offset: 0x000F056C
	// (set) Token: 0x06003F9D RID: 16285 RVA: 0x000F23B4 File Offset: 0x000F05B4
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

	// Token: 0x17000C7F RID: 3199
	// (get) Token: 0x06003F9E RID: 16286 RVA: 0x000F23D4 File Offset: 0x000F05D4
	// (set) Token: 0x06003F9F RID: 16287 RVA: 0x000F23DC File Offset: 0x000F05DC
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

	// Token: 0x17000C80 RID: 3200
	// (get) Token: 0x06003FA0 RID: 16288 RVA: 0x000F23FC File Offset: 0x000F05FC
	// (set) Token: 0x06003FA1 RID: 16289 RVA: 0x000F2404 File Offset: 0x000F0604
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

	// Token: 0x17000C81 RID: 3201
	// (get) Token: 0x06003FA2 RID: 16290 RVA: 0x000F243C File Offset: 0x000F063C
	// (set) Token: 0x06003FA3 RID: 16291 RVA: 0x000F2444 File Offset: 0x000F0644
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

	// Token: 0x17000C82 RID: 3202
	// (get) Token: 0x06003FA4 RID: 16292 RVA: 0x000F2468 File Offset: 0x000F0668
	// (set) Token: 0x06003FA5 RID: 16293 RVA: 0x000F2488 File Offset: 0x000F0688
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

	// Token: 0x17000C83 RID: 3203
	// (get) Token: 0x06003FA6 RID: 16294 RVA: 0x000F24BC File Offset: 0x000F06BC
	// (set) Token: 0x06003FA7 RID: 16295 RVA: 0x000F24C4 File Offset: 0x000F06C4
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

	// Token: 0x17000C84 RID: 3204
	// (get) Token: 0x06003FA8 RID: 16296 RVA: 0x000F24E0 File Offset: 0x000F06E0
	// (set) Token: 0x06003FA9 RID: 16297 RVA: 0x000F24E8 File Offset: 0x000F06E8
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

	// Token: 0x17000C85 RID: 3205
	// (get) Token: 0x06003FAA RID: 16298 RVA: 0x000F2504 File Offset: 0x000F0704
	// (set) Token: 0x06003FAB RID: 16299 RVA: 0x000F250C File Offset: 0x000F070C
	public dfScrollPanel.LayoutDirection FlowDirection
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

	// Token: 0x17000C86 RID: 3206
	// (get) Token: 0x06003FAC RID: 16300 RVA: 0x000F2528 File Offset: 0x000F0728
	// (set) Token: 0x06003FAD RID: 16301 RVA: 0x000F2548 File Offset: 0x000F0748
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

	// Token: 0x17000C87 RID: 3207
	// (get) Token: 0x06003FAE RID: 16302 RVA: 0x000F257C File Offset: 0x000F077C
	// (set) Token: 0x06003FAF RID: 16303 RVA: 0x000F2584 File Offset: 0x000F0784
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

	// Token: 0x17000C88 RID: 3208
	// (get) Token: 0x06003FB0 RID: 16304 RVA: 0x000F2640 File Offset: 0x000F0840
	// (set) Token: 0x06003FB1 RID: 16305 RVA: 0x000F2648 File Offset: 0x000F0848
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

	// Token: 0x17000C89 RID: 3209
	// (get) Token: 0x06003FB2 RID: 16306 RVA: 0x000F2654 File Offset: 0x000F0854
	// (set) Token: 0x06003FB3 RID: 16307 RVA: 0x000F265C File Offset: 0x000F085C
	public dfScrollbar HorzScrollbar
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

	// Token: 0x17000C8A RID: 3210
	// (get) Token: 0x06003FB4 RID: 16308 RVA: 0x000F266C File Offset: 0x000F086C
	// (set) Token: 0x06003FB5 RID: 16309 RVA: 0x000F2674 File Offset: 0x000F0874
	public dfScrollbar VertScrollbar
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

	// Token: 0x17000C8B RID: 3211
	// (get) Token: 0x06003FB6 RID: 16310 RVA: 0x000F2684 File Offset: 0x000F0884
	// (set) Token: 0x06003FB7 RID: 16311 RVA: 0x000F268C File Offset: 0x000F088C
	public dfControlOrientation WheelScrollDirection
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

	// Token: 0x06003FB8 RID: 16312 RVA: 0x000F2698 File Offset: 0x000F0898
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

	// Token: 0x17000C8C RID: 3212
	// (get) Token: 0x06003FB9 RID: 16313 RVA: 0x000F2864 File Offset: 0x000F0A64
	public override bool CanFocus
	{
		get
		{
			return (base.IsEnabled && base.IsVisible) || base.CanFocus;
		}
	}

	// Token: 0x06003FBA RID: 16314 RVA: 0x000F2884 File Offset: 0x000F0A84
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

	// Token: 0x06003FBB RID: 16315 RVA: 0x000F28F0 File Offset: 0x000F0AF0
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

	// Token: 0x06003FBC RID: 16316 RVA: 0x000F2990 File Offset: 0x000F0B90
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

	// Token: 0x06003FBD RID: 16317 RVA: 0x000F29E4 File Offset: 0x000F0BE4
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

	// Token: 0x06003FBE RID: 16318 RVA: 0x000F2A60 File Offset: 0x000F0C60
	protected internal override void OnIsVisibleChanged()
	{
		base.OnIsVisibleChanged();
		if (base.IsVisible && (this.autoReset || this.autoLayout))
		{
			this.Reset();
			this.updateScrollbars();
		}
	}

	// Token: 0x06003FBF RID: 16319 RVA: 0x000F2A98 File Offset: 0x000F0C98
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

	// Token: 0x06003FC0 RID: 16320 RVA: 0x000F2B44 File Offset: 0x000F0D44
	protected internal override void OnResolutionChanged(Vector2 previousResolution, Vector2 currentResolution)
	{
		base.OnResolutionChanged(previousResolution, currentResolution);
		this.resetNeeded = true;
	}

	// Token: 0x06003FC1 RID: 16321 RVA: 0x000F2B58 File Offset: 0x000F0D58
	protected internal override void OnGotFocus(dfFocusEventArgs args)
	{
		if (args.Source != this)
		{
			this.ScrollIntoView(args.Source);
		}
		base.OnGotFocus(args);
	}

	// Token: 0x06003FC2 RID: 16322 RVA: 0x000F2B8C File Offset: 0x000F0D8C
	protected internal override void OnKeyDown(dfKeyEventArgs args)
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

	// Token: 0x06003FC3 RID: 16323 RVA: 0x000F2CE8 File Offset: 0x000F0EE8
	protected internal override void OnMouseEnter(dfMouseEventArgs args)
	{
		base.OnMouseEnter(args);
		this.touchStartPosition = args.Position;
	}

	// Token: 0x06003FC4 RID: 16324 RVA: 0x000F2D00 File Offset: 0x000F0F00
	protected internal override void OnMouseDown(dfMouseEventArgs args)
	{
		base.OnMouseDown(args);
		this.touchStartPosition = args.Position;
		this.isMouseDown = true;
	}

	// Token: 0x06003FC5 RID: 16325 RVA: 0x000F2D1C File Offset: 0x000F0F1C
	internal override void OnDragStart(dfDragEventArgs args)
	{
		base.OnDragStart(args);
		if (args.Used)
		{
			this.isMouseDown = false;
		}
	}

	// Token: 0x06003FC6 RID: 16326 RVA: 0x000F2D38 File Offset: 0x000F0F38
	protected internal override void OnMouseUp(dfMouseEventArgs args)
	{
		base.OnMouseUp(args);
		this.isMouseDown = false;
	}

	// Token: 0x06003FC7 RID: 16327 RVA: 0x000F2D48 File Offset: 0x000F0F48
	protected internal override void OnMouseMove(dfMouseEventArgs args)
	{
		if ((args is dfTouchEventArgs || this.isMouseDown) && !args.Used && (args.Position - this.touchStartPosition).magnitude > 5f)
		{
			Vector2 vector = args.MoveDelta.Scale(-1f, 1f);
			this.ScrollPosition += vector;
			this.scrollMomentum = (this.scrollMomentum + vector) * 0.5f;
			args.Use();
		}
		base.OnMouseMove(args);
	}

	// Token: 0x06003FC8 RID: 16328 RVA: 0x000F2DEC File Offset: 0x000F0FEC
	protected internal override void OnMouseWheel(dfMouseEventArgs args)
	{
		try
		{
			if (!args.Used)
			{
				float num = (this.wheelDirection != dfControlOrientation.Horizontal) ? ((!(this.vertScroll != null)) ? ((float)this.scrollWheelAmount) : this.vertScroll.IncrementAmount) : ((!(this.horzScroll != null)) ? ((float)this.scrollWheelAmount) : this.horzScroll.IncrementAmount);
				if (this.wheelDirection == dfControlOrientation.Horizontal)
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

	// Token: 0x06003FC9 RID: 16329 RVA: 0x000F2F48 File Offset: 0x000F1148
	protected internal override void OnControlAdded(dfControl child)
	{
		base.OnControlAdded(child);
		this.attachEvents(child);
		if (this.autoLayout)
		{
			this.AutoArrange();
		}
	}

	// Token: 0x06003FCA RID: 16330 RVA: 0x000F2F6C File Offset: 0x000F116C
	protected internal override void OnControlRemoved(dfControl child)
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

	// Token: 0x06003FCB RID: 16331 RVA: 0x000F2FB0 File Offset: 0x000F11B0
	protected override void OnRebuildRenderData()
	{
		if (this.Atlas == null || string.IsNullOrEmpty(this.backgroundSprite))
		{
			return;
		}
		dfAtlas.ItemInfo itemInfo = this.Atlas[this.backgroundSprite];
		if (itemInfo == null)
		{
			return;
		}
		this.renderData.Material = this.Atlas.Material;
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

	// Token: 0x06003FCC RID: 16332 RVA: 0x000F30C8 File Offset: 0x000F12C8
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

	// Token: 0x06003FCD RID: 16333 RVA: 0x000F3118 File Offset: 0x000F1318
	public void FitToContents()
	{
		if (this.controls.Count == 0)
		{
			return;
		}
		Vector2 vector = Vector2.zero;
		for (int i = 0; i < this.controls.Count; i++)
		{
			dfControl dfControl = this.controls[i];
			Vector2 vector2 = dfControl.RelativePosition + dfControl.Size;
			vector = Vector2.Max(vector, vector2);
		}
		base.Size = vector + new Vector2((float)this.scrollPadding.right, (float)this.scrollPadding.bottom);
	}

	// Token: 0x06003FCE RID: 16334 RVA: 0x000F31B0 File Offset: 0x000F13B0
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
			dfControl dfControl = this.controls[i];
			Vector2 vector3 = dfControl.RelativePosition;
			Vector2 vector4 = vector3 + dfControl.Size;
			vector = Vector2.Min(vector, vector3);
			vector2 = Vector2.Max(vector2, vector4);
		}
		Vector2 vector5 = vector2 - vector;
		Vector2 vector6 = (base.Size - vector5) * 0.5f;
		for (int j = 0; j < this.controls.Count; j++)
		{
			dfControl dfControl2 = this.controls[j];
			dfControl2.RelativePosition = dfControl2.RelativePosition - vector + vector6;
		}
	}

	// Token: 0x06003FCF RID: 16335 RVA: 0x000F32BC File Offset: 0x000F14BC
	public void ScrollToTop()
	{
		this.ScrollPosition = new Vector2(this.scrollPosition.x, 0f);
	}

	// Token: 0x06003FD0 RID: 16336 RVA: 0x000F32DC File Offset: 0x000F14DC
	public void ScrollToBottom()
	{
		this.ScrollPosition = new Vector2(this.scrollPosition.x, 2.14748365E+09f);
	}

	// Token: 0x06003FD1 RID: 16337 RVA: 0x000F32FC File Offset: 0x000F14FC
	public void ScrollToLeft()
	{
		this.ScrollPosition = new Vector2(0f, this.scrollPosition.y);
	}

	// Token: 0x06003FD2 RID: 16338 RVA: 0x000F331C File Offset: 0x000F151C
	public void ScrollToRight()
	{
		this.ScrollPosition = new Vector2(2.14748365E+09f, this.scrollPosition.y);
	}

	// Token: 0x06003FD3 RID: 16339 RVA: 0x000F333C File Offset: 0x000F153C
	public void ScrollIntoView(dfControl control)
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

	// Token: 0x06003FD4 RID: 16340 RVA: 0x000F3548 File Offset: 0x000F1748
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

	// Token: 0x06003FD5 RID: 16341 RVA: 0x000F3644 File Offset: 0x000F1844
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
				dfControl dfControl = this.controls[i];
				if (dfControl.IsVisible && dfControl.enabled && dfControl.gameObject.activeSelf)
				{
					if (!(dfControl == this.horzScroll) && !(dfControl == this.vertScroll))
					{
						if (this.wrapLayout)
						{
							if (this.flowDirection == dfScrollPanel.LayoutDirection.Horizontal)
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
						if (this.flowDirection == dfScrollPanel.LayoutDirection.Horizontal)
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

	// Token: 0x06003FD6 RID: 16342 RVA: 0x000F38B0 File Offset: 0x000F1AB0
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

	// Token: 0x06003FD7 RID: 16343 RVA: 0x000F3968 File Offset: 0x000F1B68
	private void vertScroll_ValueChanged(dfControl control, float value)
	{
		this.ScrollPosition = new Vector2(this.scrollPosition.x, value);
	}

	// Token: 0x06003FD8 RID: 16344 RVA: 0x000F3984 File Offset: 0x000F1B84
	private void horzScroll_ValueChanged(dfControl control, float value)
	{
		this.ScrollPosition = new Vector2(value, this.ScrollPosition.y);
	}

	// Token: 0x06003FD9 RID: 16345 RVA: 0x000F39AC File Offset: 0x000F1BAC
	private void scrollChildControls(Vector3 delta)
	{
		try
		{
			this.scrolling = true;
			delta = delta.Scale(1f, -1f, 1f);
			for (int i = 0; i < this.controls.Count; i++)
			{
				dfControl dfControl = this.controls[i];
				dfControl.Position = (dfControl.Position - delta).RoundToInt();
			}
		}
		finally
		{
			this.scrolling = false;
		}
	}

	// Token: 0x06003FDA RID: 16346 RVA: 0x000F3A40 File Offset: 0x000F1C40
	private Vector2 calculateMinChildPosition()
	{
		float num = float.MaxValue;
		float num2 = float.MaxValue;
		for (int i = 0; i < this.controls.Count; i++)
		{
			dfControl dfControl = this.controls[i];
			if (dfControl.enabled && dfControl.gameObject.activeSelf)
			{
				Vector3 vector = dfControl.RelativePosition.FloorToInt();
				num = Mathf.Min(num, vector.x);
				num2 = Mathf.Min(num2, vector.y);
			}
		}
		return new Vector2(num, num2);
	}

	// Token: 0x06003FDB RID: 16347 RVA: 0x000F3AD4 File Offset: 0x000F1CD4
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
			dfControl dfControl = this.controls[i];
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

	// Token: 0x06003FDC RID: 16348 RVA: 0x000F3BE8 File Offset: 0x000F1DE8
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

	// Token: 0x06003FDD RID: 16349 RVA: 0x000F3CF0 File Offset: 0x000F1EF0
	private void attachEvents(dfControl control)
	{
		control.IsVisibleChanged += this.childIsVisibleChanged;
		control.PositionChanged += this.childControlInvalidated;
		control.SizeChanged += this.childControlInvalidated;
		control.ZOrderChanged += this.childOrderChanged;
	}

	// Token: 0x06003FDE RID: 16350 RVA: 0x000F3D48 File Offset: 0x000F1F48
	private void detachEvents(dfControl control)
	{
		control.IsVisibleChanged -= this.childIsVisibleChanged;
		control.PositionChanged -= this.childControlInvalidated;
		control.SizeChanged -= this.childControlInvalidated;
		control.ZOrderChanged -= this.childOrderChanged;
	}

	// Token: 0x06003FDF RID: 16351 RVA: 0x000F3DA0 File Offset: 0x000F1FA0
	private void childOrderChanged(dfControl control, int value)
	{
		this.onChildControlInvalidatedLayout();
	}

	// Token: 0x06003FE0 RID: 16352 RVA: 0x000F3DA8 File Offset: 0x000F1FA8
	private void childIsVisibleChanged(dfControl control, bool value)
	{
		this.onChildControlInvalidatedLayout();
	}

	// Token: 0x06003FE1 RID: 16353 RVA: 0x000F3DB0 File Offset: 0x000F1FB0
	private void childControlInvalidated(dfControl control, Vector2 value)
	{
		this.onChildControlInvalidatedLayout();
	}

	// Token: 0x06003FE2 RID: 16354 RVA: 0x000F3DB8 File Offset: 0x000F1FB8
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

	// Token: 0x040021ED RID: 8685
	[SerializeField]
	protected dfAtlas atlas;

	// Token: 0x040021EE RID: 8686
	[SerializeField]
	protected string backgroundSprite;

	// Token: 0x040021EF RID: 8687
	[SerializeField]
	protected Color32 backgroundColor = UnityEngine.Color.white;

	// Token: 0x040021F0 RID: 8688
	[SerializeField]
	protected bool autoReset = true;

	// Token: 0x040021F1 RID: 8689
	[SerializeField]
	protected bool autoLayout;

	// Token: 0x040021F2 RID: 8690
	[SerializeField]
	protected RectOffset scrollPadding = new RectOffset();

	// Token: 0x040021F3 RID: 8691
	[SerializeField]
	protected RectOffset flowPadding = new RectOffset();

	// Token: 0x040021F4 RID: 8692
	[SerializeField]
	protected dfScrollPanel.LayoutDirection flowDirection;

	// Token: 0x040021F5 RID: 8693
	[SerializeField]
	protected bool wrapLayout;

	// Token: 0x040021F6 RID: 8694
	[SerializeField]
	protected Vector2 scrollPosition = Vector2.zero;

	// Token: 0x040021F7 RID: 8695
	[SerializeField]
	protected int scrollWheelAmount = 10;

	// Token: 0x040021F8 RID: 8696
	[SerializeField]
	protected dfScrollbar horzScroll;

	// Token: 0x040021F9 RID: 8697
	[SerializeField]
	protected dfScrollbar vertScroll;

	// Token: 0x040021FA RID: 8698
	[SerializeField]
	protected dfControlOrientation wheelDirection;

	// Token: 0x040021FB RID: 8699
	[SerializeField]
	protected bool scrollWithArrowKeys;

	// Token: 0x040021FC RID: 8700
	[SerializeField]
	protected bool useScrollMomentum;

	// Token: 0x040021FD RID: 8701
	private bool initialized;

	// Token: 0x040021FE RID: 8702
	private bool resetNeeded;

	// Token: 0x040021FF RID: 8703
	private bool scrolling;

	// Token: 0x04002200 RID: 8704
	private bool isMouseDown;

	// Token: 0x04002201 RID: 8705
	private Vector2 touchStartPosition = Vector2.zero;

	// Token: 0x04002202 RID: 8706
	private Vector2 scrollMomentum = Vector2.zero;

	// Token: 0x020006EF RID: 1775
	public enum LayoutDirection
	{
		// Token: 0x04002205 RID: 8709
		Horizontal,
		// Token: 0x04002206 RID: 8710
		Vertical
	}
}
