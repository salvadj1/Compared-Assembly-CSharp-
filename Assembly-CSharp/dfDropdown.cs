using System;
using UnityEngine;

// Token: 0x02000772 RID: 1906
[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider))]
[AddComponentMenu("Daikon Forge/User Interface/Dropdown List")]
[Serializable]
public class dfDropdown : global::dfInteractiveBase, global::IDFMultiRender
{
	// Token: 0x14000046 RID: 70
	// (add) Token: 0x06004014 RID: 16404 RVA: 0x000EB040 File Offset: 0x000E9240
	// (remove) Token: 0x06004015 RID: 16405 RVA: 0x000EB05C File Offset: 0x000E925C
	public event global::dfDropdown.PopupEventHandler DropdownOpen;

	// Token: 0x14000047 RID: 71
	// (add) Token: 0x06004016 RID: 16406 RVA: 0x000EB078 File Offset: 0x000E9278
	// (remove) Token: 0x06004017 RID: 16407 RVA: 0x000EB094 File Offset: 0x000E9294
	public event global::dfDropdown.PopupEventHandler DropdownClose;

	// Token: 0x14000048 RID: 72
	// (add) Token: 0x06004018 RID: 16408 RVA: 0x000EB0B0 File Offset: 0x000E92B0
	// (remove) Token: 0x06004019 RID: 16409 RVA: 0x000EB0CC File Offset: 0x000E92CC
	public event global::PropertyChangedEventHandler<int> SelectedIndexChanged;

	// Token: 0x17000C1B RID: 3099
	// (get) Token: 0x0600401A RID: 16410 RVA: 0x000EB0E8 File Offset: 0x000E92E8
	// (set) Token: 0x0600401B RID: 16411 RVA: 0x000EB12C File Offset: 0x000E932C
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
				this.closePopup(true);
				this.font = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000C1C RID: 3100
	// (get) Token: 0x0600401C RID: 16412 RVA: 0x000EB154 File Offset: 0x000E9354
	// (set) Token: 0x0600401D RID: 16413 RVA: 0x000EB15C File Offset: 0x000E935C
	public global::dfScrollbar ListScrollbar
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

	// Token: 0x17000C1D RID: 3101
	// (get) Token: 0x0600401E RID: 16414 RVA: 0x000EB17C File Offset: 0x000E937C
	// (set) Token: 0x0600401F RID: 16415 RVA: 0x000EB184 File Offset: 0x000E9384
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

	// Token: 0x17000C1E RID: 3102
	// (get) Token: 0x06004020 RID: 16416 RVA: 0x000EB1AC File Offset: 0x000E93AC
	// (set) Token: 0x06004021 RID: 16417 RVA: 0x000EB1B4 File Offset: 0x000E93B4
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

	// Token: 0x17000C1F RID: 3103
	// (get) Token: 0x06004022 RID: 16418 RVA: 0x000EB1DC File Offset: 0x000E93DC
	// (set) Token: 0x06004023 RID: 16419 RVA: 0x000EB1E4 File Offset: 0x000E93E4
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

	// Token: 0x17000C20 RID: 3104
	// (get) Token: 0x06004024 RID: 16420 RVA: 0x000EB204 File Offset: 0x000E9404
	// (set) Token: 0x06004025 RID: 16421 RVA: 0x000EB20C File Offset: 0x000E940C
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

	// Token: 0x17000C21 RID: 3105
	// (get) Token: 0x06004026 RID: 16422 RVA: 0x000EB234 File Offset: 0x000E9434
	// (set) Token: 0x06004027 RID: 16423 RVA: 0x000EB244 File Offset: 0x000E9444
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

	// Token: 0x17000C22 RID: 3106
	// (get) Token: 0x06004028 RID: 16424 RVA: 0x000EB290 File Offset: 0x000E9490
	// (set) Token: 0x06004029 RID: 16425 RVA: 0x000EB298 File Offset: 0x000E9498
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

	// Token: 0x17000C23 RID: 3107
	// (get) Token: 0x0600402A RID: 16426 RVA: 0x000EB2FC File Offset: 0x000E94FC
	// (set) Token: 0x0600402B RID: 16427 RVA: 0x000EB31C File Offset: 0x000E951C
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

	// Token: 0x17000C24 RID: 3108
	// (get) Token: 0x0600402C RID: 16428 RVA: 0x000EB350 File Offset: 0x000E9550
	// (set) Token: 0x0600402D RID: 16429 RVA: 0x000EB358 File Offset: 0x000E9558
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

	// Token: 0x17000C25 RID: 3109
	// (get) Token: 0x0600402E RID: 16430 RVA: 0x000EB370 File Offset: 0x000E9570
	// (set) Token: 0x0600402F RID: 16431 RVA: 0x000EB378 File Offset: 0x000E9578
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

	// Token: 0x17000C26 RID: 3110
	// (get) Token: 0x06004030 RID: 16432 RVA: 0x000EB3B8 File Offset: 0x000E95B8
	// (set) Token: 0x06004031 RID: 16433 RVA: 0x000EB3C0 File Offset: 0x000E95C0
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

	// Token: 0x17000C27 RID: 3111
	// (get) Token: 0x06004032 RID: 16434 RVA: 0x000EB3EC File Offset: 0x000E95EC
	// (set) Token: 0x06004033 RID: 16435 RVA: 0x000EB40C File Offset: 0x000E960C
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

	// Token: 0x17000C28 RID: 3112
	// (get) Token: 0x06004034 RID: 16436 RVA: 0x000EB43C File Offset: 0x000E963C
	// (set) Token: 0x06004035 RID: 16437 RVA: 0x000EB45C File Offset: 0x000E965C
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

	// Token: 0x17000C29 RID: 3113
	// (get) Token: 0x06004036 RID: 16438 RVA: 0x000EB490 File Offset: 0x000E9690
	// (set) Token: 0x06004037 RID: 16439 RVA: 0x000EB498 File Offset: 0x000E9698
	public global::dfDropdown.PopupListPosition ListPosition
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

	// Token: 0x17000C2A RID: 3114
	// (get) Token: 0x06004038 RID: 16440 RVA: 0x000EB4C8 File Offset: 0x000E96C8
	// (set) Token: 0x06004039 RID: 16441 RVA: 0x000EB4D0 File Offset: 0x000E96D0
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

	// Token: 0x17000C2B RID: 3115
	// (get) Token: 0x0600403A RID: 16442 RVA: 0x000EB4DC File Offset: 0x000E96DC
	// (set) Token: 0x0600403B RID: 16443 RVA: 0x000EB4E4 File Offset: 0x000E96E4
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

	// Token: 0x17000C2C RID: 3116
	// (get) Token: 0x0600403C RID: 16444 RVA: 0x000EB4F4 File Offset: 0x000E96F4
	// (set) Token: 0x0600403D RID: 16445 RVA: 0x000EB4FC File Offset: 0x000E96FC
	public global::dfControl TriggerButton
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

	// Token: 0x17000C2D RID: 3117
	// (get) Token: 0x0600403E RID: 16446 RVA: 0x000EB534 File Offset: 0x000E9734
	// (set) Token: 0x0600403F RID: 16447 RVA: 0x000EB53C File Offset: 0x000E973C
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

	// Token: 0x17000C2E RID: 3118
	// (get) Token: 0x06004040 RID: 16448 RVA: 0x000EB548 File Offset: 0x000E9748
	// (set) Token: 0x06004041 RID: 16449 RVA: 0x000EB550 File Offset: 0x000E9750
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
	// (get) Token: 0x06004042 RID: 16450 RVA: 0x000EB56C File Offset: 0x000E976C
	// (set) Token: 0x06004043 RID: 16451 RVA: 0x000EB574 File Offset: 0x000E9774
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
	// (get) Token: 0x06004044 RID: 16452 RVA: 0x000EB5AC File Offset: 0x000E97AC
	// (set) Token: 0x06004045 RID: 16453 RVA: 0x000EB5B4 File Offset: 0x000E97B4
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

	// Token: 0x06004046 RID: 16454 RVA: 0x000EB5D4 File Offset: 0x000E97D4
	protected internal override void OnMouseWheel(global::dfMouseEventArgs args)
	{
		this.SelectedIndex = Mathf.Max(0, this.SelectedIndex - Mathf.RoundToInt(args.WheelDelta));
		args.Use();
		base.OnMouseWheel(args);
	}

	// Token: 0x06004047 RID: 16455 RVA: 0x000EB60C File Offset: 0x000E980C
	protected internal override void OnMouseDown(global::dfMouseEventArgs args)
	{
		if (this.openOnMouseDown && !args.Used && args.Buttons == global::dfMouseButtons.Left && args.Source == this)
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

	// Token: 0x06004048 RID: 16456 RVA: 0x000EB688 File Offset: 0x000E9888
	protected internal override void OnKeyDown(global::dfKeyEventArgs args)
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

	// Token: 0x06004049 RID: 16457 RVA: 0x000EB748 File Offset: 0x000E9948
	public override void OnEnable()
	{
		base.OnEnable();
		bool flag = this.Font != null && this.Font.IsValid;
		if (Application.isPlaying && !flag)
		{
			this.Font = base.GetManager().DefaultFont;
		}
	}

	// Token: 0x0600404A RID: 16458 RVA: 0x000EB79C File Offset: 0x000E999C
	public override void OnDisable()
	{
		base.OnDisable();
		this.closePopup(false);
	}

	// Token: 0x0600404B RID: 16459 RVA: 0x000EB7AC File Offset: 0x000E99AC
	public override void OnDestroy()
	{
		base.OnDestroy();
		this.closePopup(false);
		this.detachChildEvents();
	}

	// Token: 0x0600404C RID: 16460 RVA: 0x000EB7C4 File Offset: 0x000E99C4
	public override void Update()
	{
		base.Update();
		this.checkForPopupClose();
	}

	// Token: 0x0600404D RID: 16461 RVA: 0x000EB7D4 File Offset: 0x000E99D4
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

	// Token: 0x0600404E RID: 16462 RVA: 0x000EB874 File Offset: 0x000E9A74
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

	// Token: 0x0600404F RID: 16463 RVA: 0x000EB8CC File Offset: 0x000E9ACC
	private void attachChildEvents()
	{
		if (this.triggerButton != null && !this.eventsAttached)
		{
			this.eventsAttached = true;
			this.triggerButton.Click += this.trigger_Click;
		}
	}

	// Token: 0x06004050 RID: 16464 RVA: 0x000EB914 File Offset: 0x000E9B14
	private void detachChildEvents()
	{
		if (this.triggerButton != null && this.eventsAttached)
		{
			this.triggerButton.Click -= this.trigger_Click;
			this.eventsAttached = false;
		}
	}

	// Token: 0x06004051 RID: 16465 RVA: 0x000EB95C File Offset: 0x000E9B5C
	private void trigger_Click(global::dfControl control, global::dfMouseEventArgs mouseEvent)
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

	// Token: 0x06004052 RID: 16466 RVA: 0x000EB9C0 File Offset: 0x000E9BC0
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

	// Token: 0x06004053 RID: 16467 RVA: 0x000EBA00 File Offset: 0x000E9C00
	private void renderText(global::dfRenderData buffer)
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
		using (global::dfFontRendererBase dfFontRendererBase = this.font.ObtainRenderer())
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
			global::dfDynamicFont.DynamicFontRenderer dynamicFontRenderer = dfFontRendererBase as global::dfDynamicFont.DynamicFontRenderer;
			if (dynamicFontRenderer != null)
			{
				dynamicFontRenderer.SpriteAtlas = base.Atlas;
				dynamicFontRenderer.SpriteBuffer = buffer;
			}
			dfFontRendererBase.Render(text, buffer);
		}
	}

	// Token: 0x06004054 RID: 16468 RVA: 0x000EBBDC File Offset: 0x000E9DDC
	public void AddItem(string item)
	{
		string[] array = new string[this.items.Length + 1];
		Array.Copy(this.items, array, this.items.Length);
		array[this.items.Length] = item;
		this.items = array;
	}

	// Token: 0x06004055 RID: 16469 RVA: 0x000EBC20 File Offset: 0x000E9E20
	private Vector3 calculatePopupPosition(int height)
	{
		float num = base.PixelsToUnits();
		Vector3 vector = this.pivot.TransformToUpperLeft(base.Size);
		Vector3 vector2 = base.transform.position + vector * num;
		Vector3 scaledDirection = base.getScaledDirection(Vector3.down);
		Vector3 vector3 = base.transformOffset(this.listOffset) * num;
		Vector3 vector4 = vector2 + vector3 + scaledDirection * base.Size.y * num;
		Vector3 result = vector2 + vector3 - scaledDirection * this.popup.Size.y * num;
		if (this.listPosition == global::dfDropdown.PopupListPosition.Above)
		{
			return result;
		}
		if (this.listPosition == global::dfDropdown.PopupListPosition.Below)
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

	// Token: 0x06004056 RID: 16470 RVA: 0x000EBDC4 File Offset: 0x000E9FC4
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

	// Token: 0x06004057 RID: 16471 RVA: 0x000EBE48 File Offset: 0x000EA048
	private void openPopup()
	{
		if (this.popup != null || this.items.Length == 0)
		{
			return;
		}
		Vector2 size2 = this.calculatePopupSize();
		this.popup = base.GetManager().AddControl<global::dfListbox>();
		this.popup.name = base.name + " - Dropdown List";
		this.popup.gameObject.hideFlags = 4;
		this.popup.Atlas = base.Atlas;
		this.popup.Anchor = (global::dfAnchorStyle.Top | global::dfAnchorStyle.Left);
		this.popup.Font = this.Font;
		this.popup.Pivot = global::dfPivotPoint.TopLeft;
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
			global::dfScrollbar activeScrollbar = gameObject.GetComponent<global::dfScrollbar>();
			float num = base.PixelsToUnits();
			Vector3 vector = this.popup.transform.TransformDirection(Vector3.right);
			Vector3 position = this.popup.transform.position + vector * (size2.x - activeScrollbar.Width) * num;
			activeScrollbar.transform.parent = this.popup.transform;
			activeScrollbar.transform.position = position;
			activeScrollbar.Anchor = (global::dfAnchorStyle.Top | global::dfAnchorStyle.Bottom);
			activeScrollbar.Height = this.popup.Height;
			this.popup.Width -= activeScrollbar.Width;
			this.popup.Scrollbar = activeScrollbar;
			this.popup.SizeChanged += delegate(global::dfControl control, Vector2 size)
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

	// Token: 0x06004058 RID: 16472 RVA: 0x000EC248 File Offset: 0x000EA448
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

	// Token: 0x06004059 RID: 16473 RVA: 0x000EC344 File Offset: 0x000EA544
	private void popup_KeyDown(global::dfControl control, global::dfKeyEventArgs args)
	{
		if (args.KeyCode == 27 || args.KeyCode == 13)
		{
			this.closePopup(true);
			base.Focus();
		}
	}

	// Token: 0x0600405A RID: 16474 RVA: 0x000EC370 File Offset: 0x000EA570
	private void popup_ItemClicked(global::dfControl control, int selectedIndex)
	{
		this.closePopup(true);
		base.Focus();
	}

	// Token: 0x0600405B RID: 16475 RVA: 0x000EC380 File Offset: 0x000EA580
	private void popup_LostFocus(global::dfControl control, global::dfFocusEventArgs args)
	{
		if (this.popup != null && !this.popup.ContainsFocus)
		{
			this.closePopup(true);
		}
	}

	// Token: 0x0600405C RID: 16476 RVA: 0x000EC3B8 File Offset: 0x000EA5B8
	private void popup_SelectedIndexChanged(global::dfControl control, int selectedIndex)
	{
		this.SelectedIndex = selectedIndex;
		this.Invalidate();
	}

	// Token: 0x0600405D RID: 16477 RVA: 0x000EC3C8 File Offset: 0x000EA5C8
	public global::dfList<global::dfRenderData> RenderMultiple()
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

	// Token: 0x04002183 RID: 8579
	[SerializeField]
	protected global::dfFontBase font;

	// Token: 0x04002184 RID: 8580
	[SerializeField]
	protected int selectedIndex = -1;

	// Token: 0x04002185 RID: 8581
	[SerializeField]
	protected global::dfControl triggerButton;

	// Token: 0x04002186 RID: 8582
	[SerializeField]
	protected Color32 textColor = UnityEngine.Color.white;

	// Token: 0x04002187 RID: 8583
	[SerializeField]
	protected float textScale = 1f;

	// Token: 0x04002188 RID: 8584
	[SerializeField]
	protected RectOffset textFieldPadding = new RectOffset();

	// Token: 0x04002189 RID: 8585
	[SerializeField]
	protected global::dfDropdown.PopupListPosition listPosition;

	// Token: 0x0400218A RID: 8586
	[SerializeField]
	protected int listWidth;

	// Token: 0x0400218B RID: 8587
	[SerializeField]
	protected int listHeight = 200;

	// Token: 0x0400218C RID: 8588
	[SerializeField]
	protected RectOffset listPadding = new RectOffset();

	// Token: 0x0400218D RID: 8589
	[SerializeField]
	protected global::dfScrollbar listScrollbar;

	// Token: 0x0400218E RID: 8590
	[SerializeField]
	protected int itemHeight = 25;

	// Token: 0x0400218F RID: 8591
	[SerializeField]
	protected string itemHighlight = string.Empty;

	// Token: 0x04002190 RID: 8592
	[SerializeField]
	protected string itemHover = string.Empty;

	// Token: 0x04002191 RID: 8593
	[SerializeField]
	protected string listBackground = string.Empty;

	// Token: 0x04002192 RID: 8594
	[SerializeField]
	protected Vector2 listOffset = Vector2.zero;

	// Token: 0x04002193 RID: 8595
	[SerializeField]
	protected string[] items = new string[0];

	// Token: 0x04002194 RID: 8596
	[SerializeField]
	protected bool shadow;

	// Token: 0x04002195 RID: 8597
	[SerializeField]
	protected Color32 shadowColor = UnityEngine.Color.black;

	// Token: 0x04002196 RID: 8598
	[SerializeField]
	protected Vector2 shadowOffset = new Vector2(1f, -1f);

	// Token: 0x04002197 RID: 8599
	[SerializeField]
	protected bool openOnMouseDown;

	// Token: 0x04002198 RID: 8600
	private bool eventsAttached;

	// Token: 0x04002199 RID: 8601
	private global::dfListbox popup;

	// Token: 0x0400219A RID: 8602
	private global::dfRenderData textRenderData;

	// Token: 0x0400219B RID: 8603
	private global::dfList<global::dfRenderData> buffers = global::dfList<global::dfRenderData>.Obtain();

	// Token: 0x02000773 RID: 1907
	public enum PopupListPosition
	{
		// Token: 0x040021A0 RID: 8608
		Below,
		// Token: 0x040021A1 RID: 8609
		Above,
		// Token: 0x040021A2 RID: 8610
		Automatic
	}

	// Token: 0x02000774 RID: 1908
	// (Invoke) Token: 0x0600405F RID: 16479
	[global::dfEventCategory("Popup")]
	public delegate void PopupEventHandler(global::dfDropdown dropdown, global::dfListbox popup, ref bool overridden);
}
