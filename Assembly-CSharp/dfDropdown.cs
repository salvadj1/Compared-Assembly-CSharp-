using System;
using UnityEngine;

// Token: 0x020006A9 RID: 1705
[AddComponentMenu("Daikon Forge/User Interface/Dropdown List")]
[RequireComponent(typeof(BoxCollider))]
[ExecuteInEditMode]
[Serializable]
public class dfDropdown : dfInteractiveBase, IDFMultiRender
{
	// Token: 0x14000046 RID: 70
	// (add) Token: 0x06003C10 RID: 15376 RVA: 0x000E2518 File Offset: 0x000E0718
	// (remove) Token: 0x06003C11 RID: 15377 RVA: 0x000E2534 File Offset: 0x000E0734
	public event dfDropdown.PopupEventHandler DropdownOpen;

	// Token: 0x14000047 RID: 71
	// (add) Token: 0x06003C12 RID: 15378 RVA: 0x000E2550 File Offset: 0x000E0750
	// (remove) Token: 0x06003C13 RID: 15379 RVA: 0x000E256C File Offset: 0x000E076C
	public event dfDropdown.PopupEventHandler DropdownClose;

	// Token: 0x14000048 RID: 72
	// (add) Token: 0x06003C14 RID: 15380 RVA: 0x000E2588 File Offset: 0x000E0788
	// (remove) Token: 0x06003C15 RID: 15381 RVA: 0x000E25A4 File Offset: 0x000E07A4
	public event PropertyChangedEventHandler<int> SelectedIndexChanged;

	// Token: 0x17000B97 RID: 2967
	// (get) Token: 0x06003C16 RID: 15382 RVA: 0x000E25C0 File Offset: 0x000E07C0
	// (set) Token: 0x06003C17 RID: 15383 RVA: 0x000E2604 File Offset: 0x000E0804
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
				this.closePopup(true);
				this.font = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000B98 RID: 2968
	// (get) Token: 0x06003C18 RID: 15384 RVA: 0x000E262C File Offset: 0x000E082C
	// (set) Token: 0x06003C19 RID: 15385 RVA: 0x000E2634 File Offset: 0x000E0834
	public dfScrollbar ListScrollbar
	{
		get
		{
			return this.listScrollbar;
		}
		set
		{
			if (value != this.listScrollbar)
			{
				this.listScrollbar = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000B99 RID: 2969
	// (get) Token: 0x06003C1A RID: 15386 RVA: 0x000E2654 File Offset: 0x000E0854
	// (set) Token: 0x06003C1B RID: 15387 RVA: 0x000E265C File Offset: 0x000E085C
	public Vector2 ListOffset
	{
		get
		{
			return this.listOffset;
		}
		set
		{
			if (Vector2.Distance(this.listOffset, value) > 1f)
			{
				this.listOffset = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000B9A RID: 2970
	// (get) Token: 0x06003C1C RID: 15388 RVA: 0x000E2684 File Offset: 0x000E0884
	// (set) Token: 0x06003C1D RID: 15389 RVA: 0x000E268C File Offset: 0x000E088C
	public string ListBackground
	{
		get
		{
			return this.listBackground;
		}
		set
		{
			if (value != this.listBackground)
			{
				this.closePopup(true);
				this.listBackground = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000B9B RID: 2971
	// (get) Token: 0x06003C1E RID: 15390 RVA: 0x000E26B4 File Offset: 0x000E08B4
	// (set) Token: 0x06003C1F RID: 15391 RVA: 0x000E26BC File Offset: 0x000E08BC
	public string ItemHover
	{
		get
		{
			return this.itemHover;
		}
		set
		{
			if (value != this.itemHover)
			{
				this.itemHover = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000B9C RID: 2972
	// (get) Token: 0x06003C20 RID: 15392 RVA: 0x000E26DC File Offset: 0x000E08DC
	// (set) Token: 0x06003C21 RID: 15393 RVA: 0x000E26E4 File Offset: 0x000E08E4
	public string ItemHighlight
	{
		get
		{
			return this.itemHighlight;
		}
		set
		{
			if (value != this.itemHighlight)
			{
				this.closePopup(true);
				this.itemHighlight = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000B9D RID: 2973
	// (get) Token: 0x06003C22 RID: 15394 RVA: 0x000E270C File Offset: 0x000E090C
	// (set) Token: 0x06003C23 RID: 15395 RVA: 0x000E271C File Offset: 0x000E091C
	public string SelectedValue
	{
		get
		{
			return this.items[this.selectedIndex];
		}
		set
		{
			this.selectedIndex = -1;
			for (int i = 0; i < this.items.Length; i++)
			{
				if (this.items[i] == value)
				{
					this.selectedIndex = i;
					break;
				}
			}
		}
	}

	// Token: 0x17000B9E RID: 2974
	// (get) Token: 0x06003C24 RID: 15396 RVA: 0x000E2768 File Offset: 0x000E0968
	// (set) Token: 0x06003C25 RID: 15397 RVA: 0x000E2770 File Offset: 0x000E0970
	public int SelectedIndex
	{
		get
		{
			return this.selectedIndex;
		}
		set
		{
			value = Mathf.Max(-1, value);
			value = Mathf.Min(this.items.Length - 1, value);
			if (value != this.selectedIndex)
			{
				if (this.popup != null)
				{
					this.popup.SelectedIndex = value;
				}
				this.selectedIndex = value;
				this.OnSelectedIndexChanged();
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000B9F RID: 2975
	// (get) Token: 0x06003C26 RID: 15398 RVA: 0x000E27D4 File Offset: 0x000E09D4
	// (set) Token: 0x06003C27 RID: 15399 RVA: 0x000E27F4 File Offset: 0x000E09F4
	public RectOffset TextFieldPadding
	{
		get
		{
			if (this.textFieldPadding == null)
			{
				this.textFieldPadding = new RectOffset();
			}
			return this.textFieldPadding;
		}
		set
		{
			value = value.ConstrainPadding();
			if (!object.Equals(value, this.textFieldPadding))
			{
				this.textFieldPadding = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000BA0 RID: 2976
	// (get) Token: 0x06003C28 RID: 15400 RVA: 0x000E2828 File Offset: 0x000E0A28
	// (set) Token: 0x06003C29 RID: 15401 RVA: 0x000E2830 File Offset: 0x000E0A30
	public Color32 TextColor
	{
		get
		{
			return this.textColor;
		}
		set
		{
			this.closePopup(true);
			this.textColor = value;
			this.Invalidate();
		}
	}

	// Token: 0x17000BA1 RID: 2977
	// (get) Token: 0x06003C2A RID: 15402 RVA: 0x000E2848 File Offset: 0x000E0A48
	// (set) Token: 0x06003C2B RID: 15403 RVA: 0x000E2850 File Offset: 0x000E0A50
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
				this.closePopup(true);
				this.textScale = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000BA2 RID: 2978
	// (get) Token: 0x06003C2C RID: 15404 RVA: 0x000E2890 File Offset: 0x000E0A90
	// (set) Token: 0x06003C2D RID: 15405 RVA: 0x000E2898 File Offset: 0x000E0A98
	public int ItemHeight
	{
		get
		{
			return this.itemHeight;
		}
		set
		{
			value = Mathf.Max(1, value);
			if (value != this.itemHeight)
			{
				this.closePopup(true);
				this.itemHeight = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000BA3 RID: 2979
	// (get) Token: 0x06003C2E RID: 15406 RVA: 0x000E28C4 File Offset: 0x000E0AC4
	// (set) Token: 0x06003C2F RID: 15407 RVA: 0x000E28E4 File Offset: 0x000E0AE4
	public string[] Items
	{
		get
		{
			if (this.items == null)
			{
				this.items = new string[0];
			}
			return this.items;
		}
		set
		{
			this.closePopup(true);
			if (value == null)
			{
				value = new string[0];
			}
			this.items = value;
			this.Invalidate();
		}
	}

	// Token: 0x17000BA4 RID: 2980
	// (get) Token: 0x06003C30 RID: 15408 RVA: 0x000E2914 File Offset: 0x000E0B14
	// (set) Token: 0x06003C31 RID: 15409 RVA: 0x000E2934 File Offset: 0x000E0B34
	public RectOffset ListPadding
	{
		get
		{
			if (this.listPadding == null)
			{
				this.listPadding = new RectOffset();
			}
			return this.listPadding;
		}
		set
		{
			value = value.ConstrainPadding();
			if (!object.Equals(value, this.listPadding))
			{
				this.listPadding = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000BA5 RID: 2981
	// (get) Token: 0x06003C32 RID: 15410 RVA: 0x000E2968 File Offset: 0x000E0B68
	// (set) Token: 0x06003C33 RID: 15411 RVA: 0x000E2970 File Offset: 0x000E0B70
	public dfDropdown.PopupListPosition ListPosition
	{
		get
		{
			return this.listPosition;
		}
		set
		{
			if (value != this.ListPosition)
			{
				this.closePopup(true);
				this.listPosition = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000BA6 RID: 2982
	// (get) Token: 0x06003C34 RID: 15412 RVA: 0x000E29A0 File Offset: 0x000E0BA0
	// (set) Token: 0x06003C35 RID: 15413 RVA: 0x000E29A8 File Offset: 0x000E0BA8
	public int MaxListWidth
	{
		get
		{
			return this.listWidth;
		}
		set
		{
			this.listWidth = value;
		}
	}

	// Token: 0x17000BA7 RID: 2983
	// (get) Token: 0x06003C36 RID: 15414 RVA: 0x000E29B4 File Offset: 0x000E0BB4
	// (set) Token: 0x06003C37 RID: 15415 RVA: 0x000E29BC File Offset: 0x000E0BBC
	public int MaxListHeight
	{
		get
		{
			return this.listHeight;
		}
		set
		{
			this.listHeight = value;
			this.Invalidate();
		}
	}

	// Token: 0x17000BA8 RID: 2984
	// (get) Token: 0x06003C38 RID: 15416 RVA: 0x000E29CC File Offset: 0x000E0BCC
	// (set) Token: 0x06003C39 RID: 15417 RVA: 0x000E29D4 File Offset: 0x000E0BD4
	public dfControl TriggerButton
	{
		get
		{
			return this.triggerButton;
		}
		set
		{
			if (value != this.triggerButton)
			{
				this.detachChildEvents();
				this.triggerButton = value;
				this.attachChildEvents();
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000BA9 RID: 2985
	// (get) Token: 0x06003C3A RID: 15418 RVA: 0x000E2A0C File Offset: 0x000E0C0C
	// (set) Token: 0x06003C3B RID: 15419 RVA: 0x000E2A14 File Offset: 0x000E0C14
	public bool OpenOnMouseDown
	{
		get
		{
			return this.openOnMouseDown;
		}
		set
		{
			this.openOnMouseDown = value;
		}
	}

	// Token: 0x17000BAA RID: 2986
	// (get) Token: 0x06003C3C RID: 15420 RVA: 0x000E2A20 File Offset: 0x000E0C20
	// (set) Token: 0x06003C3D RID: 15421 RVA: 0x000E2A28 File Offset: 0x000E0C28
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

	// Token: 0x17000BAB RID: 2987
	// (get) Token: 0x06003C3E RID: 15422 RVA: 0x000E2A44 File Offset: 0x000E0C44
	// (set) Token: 0x06003C3F RID: 15423 RVA: 0x000E2A4C File Offset: 0x000E0C4C
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

	// Token: 0x17000BAC RID: 2988
	// (get) Token: 0x06003C40 RID: 15424 RVA: 0x000E2A84 File Offset: 0x000E0C84
	// (set) Token: 0x06003C41 RID: 15425 RVA: 0x000E2A8C File Offset: 0x000E0C8C
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

	// Token: 0x06003C42 RID: 15426 RVA: 0x000E2AAC File Offset: 0x000E0CAC
	protected internal override void OnMouseWheel(dfMouseEventArgs args)
	{
		this.SelectedIndex = Mathf.Max(0, this.SelectedIndex - Mathf.RoundToInt(args.WheelDelta));
		args.Use();
		base.OnMouseWheel(args);
	}

	// Token: 0x06003C43 RID: 15427 RVA: 0x000E2AE4 File Offset: 0x000E0CE4
	protected internal override void OnMouseDown(dfMouseEventArgs args)
	{
		if (this.openOnMouseDown && !args.Used && args.Buttons == dfMouseButtons.Left && args.Source == this)
		{
			args.Use();
			base.OnMouseDown(args);
			if (this.popup != null)
			{
				this.closePopup(true);
			}
			else
			{
				this.openPopup();
			}
		}
		else
		{
			base.OnMouseDown(args);
		}
	}

	// Token: 0x06003C44 RID: 15428 RVA: 0x000E2B60 File Offset: 0x000E0D60
	protected internal override void OnKeyDown(dfKeyEventArgs args)
	{
		KeyCode keyCode = args.KeyCode;
		switch (keyCode)
		{
		case 273:
			this.SelectedIndex = Mathf.Max(0, this.selectedIndex - 1);
			break;
		case 274:
			this.SelectedIndex = Mathf.Min(this.items.Length - 1, this.selectedIndex + 1);
			break;
		default:
			if (keyCode == 13 || keyCode == 32)
			{
				this.openPopup();
			}
			break;
		case 278:
			this.SelectedIndex = 0;
			break;
		case 279:
			this.SelectedIndex = this.items.Length - 1;
			break;
		}
		base.OnKeyDown(args);
	}

	// Token: 0x06003C45 RID: 15429 RVA: 0x000E2C20 File Offset: 0x000E0E20
	public override void OnEnable()
	{
		base.OnEnable();
		bool flag = this.Font != null && this.Font.IsValid;
		if (Application.isPlaying && !flag)
		{
			this.Font = base.GetManager().DefaultFont;
		}
	}

	// Token: 0x06003C46 RID: 15430 RVA: 0x000E2C74 File Offset: 0x000E0E74
	public override void OnDisable()
	{
		base.OnDisable();
		this.closePopup(false);
	}

	// Token: 0x06003C47 RID: 15431 RVA: 0x000E2C84 File Offset: 0x000E0E84
	public override void OnDestroy()
	{
		base.OnDestroy();
		this.closePopup(false);
		this.detachChildEvents();
	}

	// Token: 0x06003C48 RID: 15432 RVA: 0x000E2C9C File Offset: 0x000E0E9C
	public override void Update()
	{
		base.Update();
		this.checkForPopupClose();
	}

	// Token: 0x06003C49 RID: 15433 RVA: 0x000E2CAC File Offset: 0x000E0EAC
	private void checkForPopupClose()
	{
		if (this.popup == null || !Input.GetMouseButtonDown(0))
		{
			return;
		}
		Camera camera = base.GetCamera();
		Ray ray = camera.ScreenPointToRay(Input.mousePosition);
		RaycastHit raycastHit;
		if (this.popup.collider.Raycast(ray, ref raycastHit, camera.farClipPlane))
		{
			return;
		}
		if (this.popup.Scrollbar != null && this.popup.Scrollbar.collider.Raycast(ray, ref raycastHit, camera.farClipPlane))
		{
			return;
		}
		this.closePopup(true);
	}

	// Token: 0x06003C4A RID: 15434 RVA: 0x000E2D4C File Offset: 0x000E0F4C
	public override void LateUpdate()
	{
		base.LateUpdate();
		if (!Application.isPlaying)
		{
			return;
		}
		if (!this.eventsAttached)
		{
			this.attachChildEvents();
		}
		if (this.popup != null && !this.popup.ContainsFocus)
		{
			this.closePopup(true);
		}
	}

	// Token: 0x06003C4B RID: 15435 RVA: 0x000E2DA4 File Offset: 0x000E0FA4
	private void attachChildEvents()
	{
		if (this.triggerButton != null && !this.eventsAttached)
		{
			this.eventsAttached = true;
			this.triggerButton.Click += this.trigger_Click;
		}
	}

	// Token: 0x06003C4C RID: 15436 RVA: 0x000E2DEC File Offset: 0x000E0FEC
	private void detachChildEvents()
	{
		if (this.triggerButton != null && this.eventsAttached)
		{
			this.triggerButton.Click -= this.trigger_Click;
			this.eventsAttached = false;
		}
	}

	// Token: 0x06003C4D RID: 15437 RVA: 0x000E2E34 File Offset: 0x000E1034
	private void trigger_Click(dfControl control, dfMouseEventArgs mouseEvent)
	{
		if (mouseEvent.Source == this.triggerButton && !mouseEvent.Used)
		{
			mouseEvent.Use();
			if (this.popup == null)
			{
				this.openPopup();
			}
			else
			{
				Debug.Log("Close popup");
				this.closePopup(true);
			}
		}
	}

	// Token: 0x06003C4E RID: 15438 RVA: 0x000E2E98 File Offset: 0x000E1098
	protected internal virtual void OnSelectedIndexChanged()
	{
		base.SignalHierarchy("OnSelectedIndexChanged", new object[]
		{
			this.selectedIndex
		});
		if (this.SelectedIndexChanged != null)
		{
			this.SelectedIndexChanged(this, this.selectedIndex);
		}
	}

	// Token: 0x06003C4F RID: 15439 RVA: 0x000E2ED8 File Offset: 0x000E10D8
	private void renderText(dfRenderData buffer)
	{
		if (this.selectedIndex < 0 || this.selectedIndex >= this.items.Length)
		{
			return;
		}
		string text = this.items[this.selectedIndex];
		float num = base.PixelsToUnits();
		Vector2 maxSize;
		maxSize..ctor(this.size.x - (float)this.textFieldPadding.horizontal, this.size.y - (float)this.textFieldPadding.vertical);
		Vector3 vector = this.pivot.TransformToUpperLeft(base.Size);
		Vector3 vectorOffset = new Vector3(vector.x + (float)this.textFieldPadding.left, vector.y - (float)this.textFieldPadding.top, 0f) * num;
		Color32 defaultColor = (!base.IsEnabled) ? base.DisabledColor : this.TextColor;
		using (dfFontRendererBase dfFontRendererBase = this.font.ObtainRenderer())
		{
			dfFontRendererBase.WordWrap = false;
			dfFontRendererBase.MaxSize = maxSize;
			dfFontRendererBase.PixelRatio = num;
			dfFontRendererBase.TextScale = this.TextScale;
			dfFontRendererBase.VectorOffset = vectorOffset;
			dfFontRendererBase.MultiLine = false;
			dfFontRendererBase.TextAlign = 0;
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
				dynamicFontRenderer.SpriteBuffer = buffer;
			}
			dfFontRendererBase.Render(text, buffer);
		}
	}

	// Token: 0x06003C50 RID: 15440 RVA: 0x000E30B4 File Offset: 0x000E12B4
	public void AddItem(string item)
	{
		string[] array = new string[this.items.Length + 1];
		Array.Copy(this.items, array, this.items.Length);
		array[this.items.Length] = item;
		this.items = array;
	}

	// Token: 0x06003C51 RID: 15441 RVA: 0x000E30F8 File Offset: 0x000E12F8
	private Vector3 calculatePopupPosition(int height)
	{
		float num = base.PixelsToUnits();
		Vector3 vector = this.pivot.TransformToUpperLeft(base.Size);
		Vector3 vector2 = base.transform.position + vector * num;
		Vector3 scaledDirection = base.getScaledDirection(Vector3.down);
		Vector3 vector3 = base.transformOffset(this.listOffset) * num;
		Vector3 vector4 = vector2 + vector3 + scaledDirection * base.Size.y * num;
		Vector3 result = vector2 + vector3 - scaledDirection * this.popup.Size.y * num;
		if (this.listPosition == dfDropdown.PopupListPosition.Above)
		{
			return result;
		}
		if (this.listPosition == dfDropdown.PopupListPosition.Below)
		{
			return vector4;
		}
		Vector3 vector5 = this.popup.transform.parent.position / num + this.popup.Parent.Pivot.TransformToUpperLeft(base.Size);
		Vector3 vector6 = vector5 + scaledDirection * this.parent.Size.y;
		Vector3 vector7 = vector4 / num + scaledDirection * this.popup.Size.y;
		if (vector7.y < vector6.y)
		{
			return result;
		}
		if (base.GetCamera().WorldToScreenPoint(vector7 * num).y <= 0f)
		{
			return result;
		}
		return vector4;
	}

	// Token: 0x06003C52 RID: 15442 RVA: 0x000E329C File Offset: 0x000E149C
	private Vector2 calculatePopupSize()
	{
		float num = (this.MaxListWidth <= 0) ? this.size.x : ((float)this.MaxListWidth);
		int num2 = this.items.Length * this.itemHeight + this.listPadding.vertical;
		if (this.items.Length == 0)
		{
			num2 = this.itemHeight / 2 + this.listPadding.vertical;
		}
		return new Vector2(num, (float)Mathf.Min(this.MaxListHeight, num2));
	}

	// Token: 0x06003C53 RID: 15443 RVA: 0x000E3320 File Offset: 0x000E1520
	private void openPopup()
	{
		if (this.popup != null || this.items.Length == 0)
		{
			return;
		}
		Vector2 size2 = this.calculatePopupSize();
		this.popup = base.GetManager().AddControl<dfListbox>();
		this.popup.name = base.name + " - Dropdown List";
		this.popup.gameObject.hideFlags = 4;
		this.popup.Atlas = base.Atlas;
		this.popup.Anchor = (dfAnchorStyle.Top | dfAnchorStyle.Left);
		this.popup.Font = this.Font;
		this.popup.Pivot = dfPivotPoint.TopLeft;
		this.popup.Size = size2;
		this.popup.Font = this.Font;
		this.popup.ItemHeight = this.ItemHeight;
		this.popup.ItemHighlight = this.ItemHighlight;
		this.popup.ItemHover = this.ItemHover;
		this.popup.ItemPadding = this.TextFieldPadding;
		this.popup.ItemTextColor = this.TextColor;
		this.popup.ItemTextScale = this.TextScale;
		this.popup.Items = this.Items;
		this.popup.ListPadding = this.ListPadding;
		this.popup.BackgroundSprite = this.ListBackground;
		this.popup.Shadow = this.Shadow;
		this.popup.ShadowColor = this.ShadowColor;
		this.popup.ShadowOffset = this.ShadowOffset;
		this.popup.ZOrder = int.MaxValue;
		if (size2.y >= (float)this.MaxListHeight && this.listScrollbar != null)
		{
			GameObject gameObject = Object.Instantiate(this.listScrollbar.gameObject) as GameObject;
			dfScrollbar activeScrollbar = gameObject.GetComponent<dfScrollbar>();
			float num = base.PixelsToUnits();
			Vector3 vector = this.popup.transform.TransformDirection(Vector3.right);
			Vector3 position = this.popup.transform.position + vector * (size2.x - activeScrollbar.Width) * num;
			activeScrollbar.transform.parent = this.popup.transform;
			activeScrollbar.transform.position = position;
			activeScrollbar.Anchor = (dfAnchorStyle.Top | dfAnchorStyle.Bottom);
			activeScrollbar.Height = this.popup.Height;
			this.popup.Width -= activeScrollbar.Width;
			this.popup.Scrollbar = activeScrollbar;
			this.popup.SizeChanged += delegate(dfControl control, Vector2 size)
			{
				activeScrollbar.Height = control.Height;
			};
		}
		Vector3 position2 = this.calculatePopupPosition((int)this.popup.Size.y);
		this.popup.transform.position = position2;
		this.popup.transform.rotation = base.transform.rotation;
		this.popup.SelectedIndexChanged += this.popup_SelectedIndexChanged;
		this.popup.LostFocus += this.popup_LostFocus;
		this.popup.ItemClicked += this.popup_ItemClicked;
		this.popup.KeyDown += this.popup_KeyDown;
		this.popup.SelectedIndex = Mathf.Max(0, this.SelectedIndex);
		this.popup.EnsureVisible(this.popup.SelectedIndex);
		this.popup.Focus();
		if (this.DropdownOpen != null)
		{
			bool flag = false;
			this.DropdownOpen(this, this.popup, ref flag);
		}
		base.Signal("OnDropdownOpen", new object[]
		{
			this,
			this.popup
		});
	}

	// Token: 0x06003C54 RID: 15444 RVA: 0x000E3720 File Offset: 0x000E1920
	private void closePopup(bool allowOverride = true)
	{
		if (this.popup == null)
		{
			return;
		}
		this.popup.LostFocus -= this.popup_LostFocus;
		this.popup.SelectedIndexChanged -= this.popup_SelectedIndexChanged;
		this.popup.ItemClicked -= this.popup_ItemClicked;
		this.popup.KeyDown -= this.popup_KeyDown;
		if (!allowOverride)
		{
			Object.Destroy(this.popup.gameObject);
			this.popup = null;
			return;
		}
		bool flag = false;
		if (this.DropdownClose != null)
		{
			this.DropdownClose(this, this.popup, ref flag);
		}
		if (!flag)
		{
			flag = base.Signal("OnDropdownClose", new object[]
			{
				this,
				this.popup
			});
		}
		if (!flag)
		{
			Object.Destroy(this.popup.gameObject);
		}
		this.popup = null;
	}

	// Token: 0x06003C55 RID: 15445 RVA: 0x000E381C File Offset: 0x000E1A1C
	private void popup_KeyDown(dfControl control, dfKeyEventArgs args)
	{
		if (args.KeyCode == 27 || args.KeyCode == 13)
		{
			this.closePopup(true);
			base.Focus();
		}
	}

	// Token: 0x06003C56 RID: 15446 RVA: 0x000E3848 File Offset: 0x000E1A48
	private void popup_ItemClicked(dfControl control, int selectedIndex)
	{
		this.closePopup(true);
		base.Focus();
	}

	// Token: 0x06003C57 RID: 15447 RVA: 0x000E3858 File Offset: 0x000E1A58
	private void popup_LostFocus(dfControl control, dfFocusEventArgs args)
	{
		if (this.popup != null && !this.popup.ContainsFocus)
		{
			this.closePopup(true);
		}
	}

	// Token: 0x06003C58 RID: 15448 RVA: 0x000E3890 File Offset: 0x000E1A90
	private void popup_SelectedIndexChanged(dfControl control, int selectedIndex)
	{
		this.SelectedIndex = selectedIndex;
		this.Invalidate();
	}

	// Token: 0x06003C59 RID: 15449 RVA: 0x000E38A0 File Offset: 0x000E1AA0
	public dfList<dfRenderData> RenderMultiple()
	{
		if (base.Atlas == null || this.Font == null)
		{
			return null;
		}
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
		this.buffers.Clear();
		this.renderData.Clear();
		this.renderData.Material = base.Atlas.Material;
		this.renderData.Transform = base.transform.localToWorldMatrix;
		this.buffers.Add(this.renderData);
		this.textRenderData.Clear();
		this.textRenderData.Material = base.Atlas.Material;
		this.textRenderData.Transform = base.transform.localToWorldMatrix;
		this.buffers.Add(this.textRenderData);
		this.renderBackground();
		this.renderText(this.textRenderData);
		this.isControlInvalidated = false;
		this.updateCollider();
		return this.buffers;
	}

	// Token: 0x04001F83 RID: 8067
	[SerializeField]
	protected dfFontBase font;

	// Token: 0x04001F84 RID: 8068
	[SerializeField]
	protected int selectedIndex = -1;

	// Token: 0x04001F85 RID: 8069
	[SerializeField]
	protected dfControl triggerButton;

	// Token: 0x04001F86 RID: 8070
	[SerializeField]
	protected Color32 textColor = UnityEngine.Color.white;

	// Token: 0x04001F87 RID: 8071
	[SerializeField]
	protected float textScale = 1f;

	// Token: 0x04001F88 RID: 8072
	[SerializeField]
	protected RectOffset textFieldPadding = new RectOffset();

	// Token: 0x04001F89 RID: 8073
	[SerializeField]
	protected dfDropdown.PopupListPosition listPosition;

	// Token: 0x04001F8A RID: 8074
	[SerializeField]
	protected int listWidth;

	// Token: 0x04001F8B RID: 8075
	[SerializeField]
	protected int listHeight = 200;

	// Token: 0x04001F8C RID: 8076
	[SerializeField]
	protected RectOffset listPadding = new RectOffset();

	// Token: 0x04001F8D RID: 8077
	[SerializeField]
	protected dfScrollbar listScrollbar;

	// Token: 0x04001F8E RID: 8078
	[SerializeField]
	protected int itemHeight = 25;

	// Token: 0x04001F8F RID: 8079
	[SerializeField]
	protected string itemHighlight = string.Empty;

	// Token: 0x04001F90 RID: 8080
	[SerializeField]
	protected string itemHover = string.Empty;

	// Token: 0x04001F91 RID: 8081
	[SerializeField]
	protected string listBackground = string.Empty;

	// Token: 0x04001F92 RID: 8082
	[SerializeField]
	protected Vector2 listOffset = Vector2.zero;

	// Token: 0x04001F93 RID: 8083
	[SerializeField]
	protected string[] items = new string[0];

	// Token: 0x04001F94 RID: 8084
	[SerializeField]
	protected bool shadow;

	// Token: 0x04001F95 RID: 8085
	[SerializeField]
	protected Color32 shadowColor = UnityEngine.Color.black;

	// Token: 0x04001F96 RID: 8086
	[SerializeField]
	protected Vector2 shadowOffset = new Vector2(1f, -1f);

	// Token: 0x04001F97 RID: 8087
	[SerializeField]
	protected bool openOnMouseDown;

	// Token: 0x04001F98 RID: 8088
	private bool eventsAttached;

	// Token: 0x04001F99 RID: 8089
	private dfListbox popup;

	// Token: 0x04001F9A RID: 8090
	private dfRenderData textRenderData;

	// Token: 0x04001F9B RID: 8091
	private dfList<dfRenderData> buffers = dfList<dfRenderData>.Obtain();

	// Token: 0x020006AA RID: 1706
	public enum PopupListPosition
	{
		// Token: 0x04001FA0 RID: 8096
		Below,
		// Token: 0x04001FA1 RID: 8097
		Above,
		// Token: 0x04001FA2 RID: 8098
		Automatic
	}

	// Token: 0x020008DC RID: 2268
	// (Invoke) Token: 0x06004D48 RID: 19784
	[dfEventCategory("Popup")]
	public delegate void PopupEventHandler(dfDropdown dropdown, dfListbox popup, ref bool overridden);
}
