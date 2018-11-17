using System;
using UnityEngine;

// Token: 0x020006DF RID: 1759
[ExecuteInEditMode]
[AddComponentMenu("Daikon Forge/User Interface/Listbox")]
[RequireComponent(typeof(BoxCollider))]
[Serializable]
public class dfListbox : dfInteractiveBase, IDFMultiRender
{
	// Token: 0x1400004C RID: 76
	// (add) Token: 0x06003E93 RID: 16019 RVA: 0x000ECEF8 File Offset: 0x000EB0F8
	// (remove) Token: 0x06003E94 RID: 16020 RVA: 0x000ECF14 File Offset: 0x000EB114
	public event PropertyChangedEventHandler<int> SelectedIndexChanged;

	// Token: 0x1400004D RID: 77
	// (add) Token: 0x06003E95 RID: 16021 RVA: 0x000ECF30 File Offset: 0x000EB130
	// (remove) Token: 0x06003E96 RID: 16022 RVA: 0x000ECF4C File Offset: 0x000EB14C
	public event PropertyChangedEventHandler<int> ItemClicked;

	// Token: 0x17000C3D RID: 3133
	// (get) Token: 0x06003E97 RID: 16023 RVA: 0x000ECF68 File Offset: 0x000EB168
	// (set) Token: 0x06003E98 RID: 16024 RVA: 0x000ECFAC File Offset: 0x000EB1AC
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

	// Token: 0x17000C3E RID: 3134
	// (get) Token: 0x06003E99 RID: 16025 RVA: 0x000ECFCC File Offset: 0x000EB1CC
	// (set) Token: 0x06003E9A RID: 16026 RVA: 0x000ECFD4 File Offset: 0x000EB1D4
	public float ScrollPosition
	{
		get
		{
			return this.scrollPosition;
		}
		set
		{
			if (!Mathf.Approximately(value, this.scrollPosition))
			{
				this.scrollPosition = this.constrainScrollPosition(value);
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000C3F RID: 3135
	// (get) Token: 0x06003E9B RID: 16027 RVA: 0x000ED008 File Offset: 0x000EB208
	// (set) Token: 0x06003E9C RID: 16028 RVA: 0x000ED010 File Offset: 0x000EB210
	public TextAlignment ItemAlignment
	{
		get
		{
			return this.itemAlignment;
		}
		set
		{
			if (value != this.itemAlignment)
			{
				this.itemAlignment = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000C40 RID: 3136
	// (get) Token: 0x06003E9D RID: 16029 RVA: 0x000ED02C File Offset: 0x000EB22C
	// (set) Token: 0x06003E9E RID: 16030 RVA: 0x000ED034 File Offset: 0x000EB234
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
				this.itemHighlight = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000C41 RID: 3137
	// (get) Token: 0x06003E9F RID: 16031 RVA: 0x000ED054 File Offset: 0x000EB254
	// (set) Token: 0x06003EA0 RID: 16032 RVA: 0x000ED05C File Offset: 0x000EB25C
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

	// Token: 0x17000C42 RID: 3138
	// (get) Token: 0x06003EA1 RID: 16033 RVA: 0x000ED07C File Offset: 0x000EB27C
	public string SelectedItem
	{
		get
		{
			if (this.selectedIndex == -1)
			{
				return null;
			}
			return this.items[this.selectedIndex];
		}
	}

	// Token: 0x17000C43 RID: 3139
	// (get) Token: 0x06003EA2 RID: 16034 RVA: 0x000ED09C File Offset: 0x000EB29C
	// (set) Token: 0x06003EA3 RID: 16035 RVA: 0x000ED0AC File Offset: 0x000EB2AC
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

	// Token: 0x17000C44 RID: 3140
	// (get) Token: 0x06003EA4 RID: 16036 RVA: 0x000ED0F8 File Offset: 0x000EB2F8
	// (set) Token: 0x06003EA5 RID: 16037 RVA: 0x000ED100 File Offset: 0x000EB300
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
				this.selectedIndex = value;
				this.EnsureVisible(value);
				this.OnSelectedIndexChanged();
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000C45 RID: 3141
	// (get) Token: 0x06003EA6 RID: 16038 RVA: 0x000ED150 File Offset: 0x000EB350
	// (set) Token: 0x06003EA7 RID: 16039 RVA: 0x000ED170 File Offset: 0x000EB370
	public RectOffset ItemPadding
	{
		get
		{
			if (this.itemPadding == null)
			{
				this.itemPadding = new RectOffset();
			}
			return this.itemPadding;
		}
		set
		{
			value = value.ConstrainPadding();
			if (!value.Equals(this.itemPadding))
			{
				this.itemPadding = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000C46 RID: 3142
	// (get) Token: 0x06003EA8 RID: 16040 RVA: 0x000ED1A4 File Offset: 0x000EB3A4
	// (set) Token: 0x06003EA9 RID: 16041 RVA: 0x000ED1AC File Offset: 0x000EB3AC
	public Color32 ItemTextColor
	{
		get
		{
			return this.itemTextColor;
		}
		set
		{
			if (!value.Equals(this.itemTextColor))
			{
				this.itemTextColor = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000C47 RID: 3143
	// (get) Token: 0x06003EAA RID: 16042 RVA: 0x000ED1E4 File Offset: 0x000EB3E4
	// (set) Token: 0x06003EAB RID: 16043 RVA: 0x000ED1EC File Offset: 0x000EB3EC
	public float ItemTextScale
	{
		get
		{
			return this.itemTextScale;
		}
		set
		{
			value = Mathf.Max(0.1f, value);
			if (!Mathf.Approximately(this.itemTextScale, value))
			{
				this.itemTextScale = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000C48 RID: 3144
	// (get) Token: 0x06003EAC RID: 16044 RVA: 0x000ED21C File Offset: 0x000EB41C
	// (set) Token: 0x06003EAD RID: 16045 RVA: 0x000ED224 File Offset: 0x000EB424
	public int ItemHeight
	{
		get
		{
			return this.itemHeight;
		}
		set
		{
			this.scrollPosition = 0f;
			value = Mathf.Max(1, value);
			if (value != this.itemHeight)
			{
				this.itemHeight = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000C49 RID: 3145
	// (get) Token: 0x06003EAE RID: 16046 RVA: 0x000ED254 File Offset: 0x000EB454
	// (set) Token: 0x06003EAF RID: 16047 RVA: 0x000ED274 File Offset: 0x000EB474
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
			if (value != this.items)
			{
				this.scrollPosition = 0f;
				if (value == null)
				{
					value = new string[0];
				}
				this.items = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000C4A RID: 3146
	// (get) Token: 0x06003EB0 RID: 16048 RVA: 0x000ED2B4 File Offset: 0x000EB4B4
	// (set) Token: 0x06003EB1 RID: 16049 RVA: 0x000ED2BC File Offset: 0x000EB4BC
	public dfScrollbar Scrollbar
	{
		get
		{
			return this.scrollbar;
		}
		set
		{
			this.scrollPosition = 0f;
			if (value != this.scrollbar)
			{
				this.detachScrollbarEvents();
				this.scrollbar = value;
				this.attachScrollbarEvents();
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000C4B RID: 3147
	// (get) Token: 0x06003EB2 RID: 16050 RVA: 0x000ED2F4 File Offset: 0x000EB4F4
	// (set) Token: 0x06003EB3 RID: 16051 RVA: 0x000ED314 File Offset: 0x000EB514
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

	// Token: 0x17000C4C RID: 3148
	// (get) Token: 0x06003EB4 RID: 16052 RVA: 0x000ED348 File Offset: 0x000EB548
	// (set) Token: 0x06003EB5 RID: 16053 RVA: 0x000ED350 File Offset: 0x000EB550
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

	// Token: 0x17000C4D RID: 3149
	// (get) Token: 0x06003EB6 RID: 16054 RVA: 0x000ED36C File Offset: 0x000EB56C
	// (set) Token: 0x06003EB7 RID: 16055 RVA: 0x000ED374 File Offset: 0x000EB574
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

	// Token: 0x17000C4E RID: 3150
	// (get) Token: 0x06003EB8 RID: 16056 RVA: 0x000ED3AC File Offset: 0x000EB5AC
	// (set) Token: 0x06003EB9 RID: 16057 RVA: 0x000ED3B4 File Offset: 0x000EB5B4
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

	// Token: 0x17000C4F RID: 3151
	// (get) Token: 0x06003EBA RID: 16058 RVA: 0x000ED3D4 File Offset: 0x000EB5D4
	// (set) Token: 0x06003EBB RID: 16059 RVA: 0x000ED3DC File Offset: 0x000EB5DC
	public bool AnimateHover
	{
		get
		{
			return this.animateHover;
		}
		set
		{
			this.animateHover = value;
		}
	}

	// Token: 0x17000C50 RID: 3152
	// (get) Token: 0x06003EBC RID: 16060 RVA: 0x000ED3E8 File Offset: 0x000EB5E8
	// (set) Token: 0x06003EBD RID: 16061 RVA: 0x000ED3F0 File Offset: 0x000EB5F0
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

	// Token: 0x06003EBE RID: 16062 RVA: 0x000ED400 File Offset: 0x000EB600
	public override void Awake()
	{
		base.Awake();
		this.startSize = base.Size;
	}

	// Token: 0x06003EBF RID: 16063 RVA: 0x000ED414 File Offset: 0x000EB614
	public override void Update()
	{
		base.Update();
		if (this.size.magnitude == 0f)
		{
			this.size = new Vector2(200f, 150f);
		}
		if (this.animateHover && this.hoverIndex != -1)
		{
			float num = (float)(this.hoverIndex * this.itemHeight) * base.PixelsToUnits();
			if (Mathf.Abs(this.hoverTweenLocation - num) < 1f)
			{
				this.Invalidate();
			}
		}
		if (this.isControlInvalidated)
		{
			this.synchronizeScrollbar();
		}
	}

	// Token: 0x06003EC0 RID: 16064 RVA: 0x000ED4AC File Offset: 0x000EB6AC
	public override void LateUpdate()
	{
		base.LateUpdate();
		if (!Application.isPlaying)
		{
			return;
		}
		this.attachScrollbarEvents();
	}

	// Token: 0x06003EC1 RID: 16065 RVA: 0x000ED4C8 File Offset: 0x000EB6C8
	public override void OnDestroy()
	{
		base.OnDestroy();
		this.detachScrollbarEvents();
	}

	// Token: 0x06003EC2 RID: 16066 RVA: 0x000ED4D8 File Offset: 0x000EB6D8
	public override void OnDisable()
	{
		base.OnDisable();
		this.detachScrollbarEvents();
	}

	// Token: 0x06003EC3 RID: 16067 RVA: 0x000ED4E8 File Offset: 0x000EB6E8
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

	// Token: 0x06003EC4 RID: 16068 RVA: 0x000ED528 File Offset: 0x000EB728
	protected internal virtual void OnItemClicked()
	{
		base.Signal("OnItemClicked", new object[]
		{
			this.selectedIndex
		});
		if (this.ItemClicked != null)
		{
			this.ItemClicked(this, this.selectedIndex);
		}
	}

	// Token: 0x06003EC5 RID: 16069 RVA: 0x000ED568 File Offset: 0x000EB768
	protected internal override void OnMouseMove(dfMouseEventArgs args)
	{
		base.OnMouseMove(args);
		if (!(args is dfTouchEventArgs))
		{
			this.updateItemHover(args);
			return;
		}
		if (Mathf.Abs(args.Position.y - this.touchStartPosition.y) < (float)(this.itemHeight / 2))
		{
			return;
		}
		this.ScrollPosition = Mathf.Max(0f, this.ScrollPosition + args.MoveDelta.y);
		this.synchronizeScrollbar();
		this.hoverIndex = -1;
	}

	// Token: 0x06003EC6 RID: 16070 RVA: 0x000ED5F0 File Offset: 0x000EB7F0
	protected internal override void OnMouseEnter(dfMouseEventArgs args)
	{
		base.OnMouseEnter(args);
		this.touchStartPosition = args.Position;
	}

	// Token: 0x06003EC7 RID: 16071 RVA: 0x000ED608 File Offset: 0x000EB808
	protected internal override void OnMouseLeave(dfMouseEventArgs args)
	{
		base.OnMouseLeave(args);
		this.hoverIndex = -1;
	}

	// Token: 0x06003EC8 RID: 16072 RVA: 0x000ED618 File Offset: 0x000EB818
	protected internal override void OnMouseWheel(dfMouseEventArgs args)
	{
		base.OnMouseWheel(args);
		this.ScrollPosition = Mathf.Max(0f, this.ScrollPosition - (float)((int)args.WheelDelta * this.ItemHeight));
		this.synchronizeScrollbar();
		this.updateItemHover(args);
	}

	// Token: 0x06003EC9 RID: 16073 RVA: 0x000ED660 File Offset: 0x000EB860
	protected internal override void OnMouseUp(dfMouseEventArgs args)
	{
		this.hoverIndex = -1;
		base.OnMouseUp(args);
		if (args is dfTouchEventArgs && Mathf.Abs(args.Position.y - this.touchStartPosition.y) < (float)this.itemHeight)
		{
			this.selectItemUnderMouse(args);
		}
	}

	// Token: 0x06003ECA RID: 16074 RVA: 0x000ED6B8 File Offset: 0x000EB8B8
	protected internal override void OnMouseDown(dfMouseEventArgs args)
	{
		base.OnMouseDown(args);
		if (args is dfTouchEventArgs)
		{
			this.touchStartPosition = args.Position;
			return;
		}
		this.selectItemUnderMouse(args);
	}

	// Token: 0x06003ECB RID: 16075 RVA: 0x000ED6EC File Offset: 0x000EB8EC
	protected internal override void OnKeyDown(dfKeyEventArgs args)
	{
		switch (args.KeyCode)
		{
		case 273:
			this.SelectedIndex = Mathf.Max(0, this.selectedIndex - 1);
			break;
		case 274:
			this.SelectedIndex++;
			break;
		case 278:
			this.SelectedIndex = 0;
			break;
		case 279:
			this.SelectedIndex = this.items.Length;
			break;
		case 280:
		{
			int num = this.SelectedIndex - Mathf.FloorToInt((this.size.y - (float)this.listPadding.vertical) / (float)this.itemHeight);
			this.SelectedIndex = Mathf.Max(0, num);
			break;
		}
		case 281:
			this.SelectedIndex += Mathf.FloorToInt((this.size.y - (float)this.listPadding.vertical) / (float)this.itemHeight);
			break;
		}
		base.OnKeyDown(args);
	}

	// Token: 0x06003ECC RID: 16076 RVA: 0x000ED800 File Offset: 0x000EBA00
	public void EnsureVisible(int index)
	{
		int num = index * this.ItemHeight;
		if (this.scrollPosition > (float)num)
		{
			this.ScrollPosition = (float)num;
		}
		float num2 = this.size.y - (float)this.listPadding.vertical;
		if (this.scrollPosition + num2 < (float)(num + this.itemHeight))
		{
			this.ScrollPosition = (float)num - num2 + (float)this.itemHeight;
		}
	}

	// Token: 0x06003ECD RID: 16077 RVA: 0x000ED86C File Offset: 0x000EBA6C
	private void selectItemUnderMouse(dfMouseEventArgs args)
	{
		float num = this.pivot.TransformToUpperLeft(base.Size).y + ((float)(-(float)this.itemHeight) * ((float)this.selectedIndex - this.scrollPosition) - (float)this.listPadding.top);
		float num2 = ((float)this.selectedIndex - this.scrollPosition + 1f) * (float)this.itemHeight + (float)this.listPadding.vertical;
		float num3 = num2 - this.size.y;
		if (num3 > 0f)
		{
			num += num3;
		}
		float num4 = base.GetHitPosition(args).y - (float)this.listPadding.top;
		if (num4 < 0f || num4 > this.size.y - (float)this.listPadding.bottom)
		{
			return;
		}
		this.SelectedIndex = (int)((this.scrollPosition + num4) / (float)this.itemHeight);
		this.OnItemClicked();
	}

	// Token: 0x06003ECE RID: 16078 RVA: 0x000ED968 File Offset: 0x000EBB68
	private void renderHover()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		bool flag = base.Atlas == null || !base.IsEnabled || this.hoverIndex < 0 || this.hoverIndex > this.items.Length - 1 || string.IsNullOrEmpty(this.ItemHover);
		if (flag)
		{
			return;
		}
		dfAtlas.ItemInfo itemInfo = base.Atlas[this.ItemHover];
		if (itemInfo == null)
		{
			return;
		}
		Vector3 vector = this.pivot.TransformToUpperLeft(base.Size);
		Vector3 offset;
		offset..ctor(vector.x + (float)this.listPadding.left, vector.y - (float)this.listPadding.top + this.scrollPosition, 0f);
		float num = base.PixelsToUnits();
		int num2 = this.hoverIndex * this.itemHeight;
		if (this.animateHover)
		{
			float num3 = Mathf.Abs(this.hoverTweenLocation - (float)num2);
			float num4 = (this.size.y - (float)this.listPadding.vertical) * 0.5f;
			if (num3 > num4)
			{
				this.hoverTweenLocation = (float)num2 + Mathf.Sign(this.hoverTweenLocation - (float)num2) * num4;
			}
			float num5 = Time.deltaTime / num * 2f;
			this.hoverTweenLocation = Mathf.MoveTowards(this.hoverTweenLocation, (float)num2, num5);
		}
		else
		{
			this.hoverTweenLocation = (float)num2;
		}
		offset.y -= this.hoverTweenLocation.Quantize(num);
		Color32 color = base.ApplyOpacity(this.color);
		dfSprite.RenderOptions options = new dfSprite.RenderOptions
		{
			atlas = this.atlas,
			color = color,
			fillAmount = 1f,
			pixelsToUnits = base.PixelsToUnits(),
			size = new Vector3(this.size.x - (float)this.listPadding.horizontal, (float)this.itemHeight),
			spriteInfo = itemInfo,
			offset = offset
		};
		if (itemInfo.border.horizontal > 0 || itemInfo.border.vertical > 0)
		{
			dfSlicedSprite.renderSprite(this.renderData, options);
		}
		else
		{
			dfSprite.renderSprite(this.renderData, options);
		}
		if ((float)num2 != this.hoverTweenLocation)
		{
			this.Invalidate();
		}
	}

	// Token: 0x06003ECF RID: 16079 RVA: 0x000EDBEC File Offset: 0x000EBDEC
	private void renderSelection()
	{
		if (base.Atlas == null || this.selectedIndex < 0)
		{
			return;
		}
		dfAtlas.ItemInfo itemInfo = base.Atlas[this.ItemHighlight];
		if (itemInfo == null)
		{
			return;
		}
		float pixelsToUnits = base.PixelsToUnits();
		Vector3 vector = this.pivot.TransformToUpperLeft(base.Size);
		Vector3 offset;
		offset..ctor(vector.x + (float)this.listPadding.left, vector.y - (float)this.listPadding.top + this.scrollPosition, 0f);
		offset.y -= (float)(this.selectedIndex * this.itemHeight);
		Color32 color = base.ApplyOpacity(this.color);
		dfSprite.RenderOptions options = new dfSprite.RenderOptions
		{
			atlas = this.atlas,
			color = color,
			fillAmount = 1f,
			pixelsToUnits = pixelsToUnits,
			size = new Vector3(this.size.x - (float)this.listPadding.horizontal, (float)this.itemHeight),
			spriteInfo = itemInfo,
			offset = offset
		};
		if (itemInfo.border.horizontal > 0 || itemInfo.border.vertical > 0)
		{
			dfSlicedSprite.renderSprite(this.renderData, options);
		}
		else
		{
			dfSprite.renderSprite(this.renderData, options);
		}
	}

	// Token: 0x06003ED0 RID: 16080 RVA: 0x000EDD70 File Offset: 0x000EBF70
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

	// Token: 0x06003ED1 RID: 16081 RVA: 0x000EDDD4 File Offset: 0x000EBFD4
	private void renderItems(dfRenderData buffer)
	{
		if (this.font == null || this.items == null || this.items.Length == 0)
		{
			return;
		}
		float num = base.PixelsToUnits();
		Vector2 maxSize;
		maxSize..ctor(this.size.x - (float)this.itemPadding.horizontal - (float)this.listPadding.horizontal, (float)(this.itemHeight - this.itemPadding.vertical));
		Vector3 vector = this.pivot.TransformToUpperLeft(base.Size);
		Vector3 vectorOffset = new Vector3(vector.x + (float)this.itemPadding.left + (float)this.listPadding.left, vector.y - (float)this.itemPadding.top - (float)this.listPadding.top, 0f) * num;
		vectorOffset.y += this.scrollPosition * num;
		Color32 defaultColor = (!base.IsEnabled) ? base.DisabledColor : this.ItemTextColor;
		float num2 = vector.y * num;
		float num3 = num2 - this.size.y * num;
		for (int i = 0; i < this.items.Length; i++)
		{
			using (dfFontRendererBase dfFontRendererBase = this.font.ObtainRenderer())
			{
				dfFontRendererBase.WordWrap = false;
				dfFontRendererBase.MaxSize = maxSize;
				dfFontRendererBase.PixelRatio = num;
				dfFontRendererBase.TextScale = this.ItemTextScale * this.getTextScaleMultiplier();
				dfFontRendererBase.VectorOffset = vectorOffset;
				dfFontRendererBase.MultiLine = false;
				dfFontRendererBase.TextAlign = this.ItemAlignment;
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
				if (vectorOffset.y - (float)this.itemHeight * num <= num2)
				{
					dfFontRendererBase.Render(this.items[i], buffer);
				}
				vectorOffset.y -= (float)this.itemHeight * num;
				dfFontRendererBase.VectorOffset = vectorOffset;
				if (vectorOffset.y < num3)
				{
					break;
				}
			}
		}
	}

	// Token: 0x06003ED2 RID: 16082 RVA: 0x000EE07C File Offset: 0x000EC27C
	private void clipQuads(dfRenderData buffer, int startIndex)
	{
		dfList<Vector3> vertices = buffer.Vertices;
		dfList<Vector2> uv = buffer.UV;
		float num = base.PixelsToUnits();
		float num2 = (base.Pivot.TransformToUpperLeft(base.Size).y - (float)this.listPadding.top) * num;
		float num3 = num2 - (this.size.y - (float)this.listPadding.vertical) * num;
		for (int i = startIndex; i < vertices.Count; i += 4)
		{
			Vector3 value = vertices[i];
			Vector3 value2 = vertices[i + 1];
			Vector3 value3 = vertices[i + 2];
			Vector3 value4 = vertices[i + 3];
			float num4 = value.y - value4.y;
			if (value4.y < num3)
			{
				float num5 = 1f - Mathf.Abs(-num3 + value.y) / num4;
				dfList<Vector3> dfList = vertices;
				int index = i;
				value..ctor(value.x, Mathf.Max(value.y, num3), value2.z);
				dfList[index] = value;
				dfList<Vector3> dfList2 = vertices;
				int index2 = i + 1;
				value2..ctor(value2.x, Mathf.Max(value2.y, num3), value2.z);
				dfList2[index2] = value2;
				dfList<Vector3> dfList3 = vertices;
				int index3 = i + 2;
				value3..ctor(value3.x, Mathf.Max(value3.y, num3), value3.z);
				dfList3[index3] = value3;
				dfList<Vector3> dfList4 = vertices;
				int index4 = i + 3;
				value4..ctor(value4.x, Mathf.Max(value4.y, num3), value4.z);
				dfList4[index4] = value4;
				float num6 = Mathf.Lerp(uv[i + 3].y, uv[i].y, num5);
				uv[i + 3] = new Vector2(uv[i + 3].x, num6);
				uv[i + 2] = new Vector2(uv[i + 2].x, num6);
				num4 = Mathf.Abs(value4.y - value.y);
			}
			if (value.y > num2)
			{
				float num7 = Mathf.Abs(num2 - value.y) / num4;
				vertices[i] = new Vector3(value.x, Mathf.Min(num2, value.y), value.z);
				vertices[i + 1] = new Vector3(value2.x, Mathf.Min(num2, value2.y), value2.z);
				vertices[i + 2] = new Vector3(value3.x, Mathf.Min(num2, value3.y), value3.z);
				vertices[i + 3] = new Vector3(value4.x, Mathf.Min(num2, value4.y), value4.z);
				float num8 = Mathf.Lerp(uv[i].y, uv[i + 3].y, num7);
				uv[i] = new Vector2(uv[i].x, num8);
				uv[i + 1] = new Vector2(uv[i + 1].x, num8);
			}
		}
	}

	// Token: 0x06003ED3 RID: 16083 RVA: 0x000EE3E8 File Offset: 0x000EC5E8
	private void updateItemHover(dfMouseEventArgs args)
	{
		if (!Application.isPlaying)
		{
			return;
		}
		Ray ray = args.Ray;
		RaycastHit raycastHit;
		if (!base.collider.Raycast(ray, ref raycastHit, 1000f))
		{
			this.hoverIndex = -1;
			this.hoverTweenLocation = 0f;
			return;
		}
		Vector2 vector;
		base.GetHitPosition(ray, out vector);
		float num = base.Pivot.TransformToUpperLeft(base.Size).y + ((float)(-(float)this.itemHeight) * ((float)this.selectedIndex - this.scrollPosition) - (float)this.listPadding.top);
		float num2 = ((float)this.selectedIndex - this.scrollPosition + 1f) * (float)this.itemHeight + (float)this.listPadding.vertical;
		float num3 = num2 - this.size.y;
		if (num3 > 0f)
		{
			num += num3;
		}
		float num4 = vector.y - (float)this.listPadding.top;
		int num5 = (int)(this.scrollPosition + num4) / this.itemHeight;
		if (num5 != this.hoverIndex)
		{
			this.hoverIndex = num5;
			this.Invalidate();
		}
	}

	// Token: 0x06003ED4 RID: 16084 RVA: 0x000EE510 File Offset: 0x000EC710
	private float constrainScrollPosition(float value)
	{
		value = Mathf.Max(0f, value);
		int num = this.items.Length * this.itemHeight;
		float num2 = this.size.y - (float)this.listPadding.vertical;
		if ((float)num < num2)
		{
			return 0f;
		}
		return Mathf.Min(value, (float)num - num2);
	}

	// Token: 0x06003ED5 RID: 16085 RVA: 0x000EE56C File Offset: 0x000EC76C
	private void attachScrollbarEvents()
	{
		if (this.scrollbar == null || this.eventsAttached)
		{
			return;
		}
		this.eventsAttached = true;
		this.scrollbar.ValueChanged += this.scrollbar_ValueChanged;
		this.scrollbar.GotFocus += this.scrollbar_GotFocus;
	}

	// Token: 0x06003ED6 RID: 16086 RVA: 0x000EE5CC File Offset: 0x000EC7CC
	private void detachScrollbarEvents()
	{
		if (this.scrollbar == null || !this.eventsAttached)
		{
			return;
		}
		this.eventsAttached = false;
		this.scrollbar.ValueChanged -= this.scrollbar_ValueChanged;
		this.scrollbar.GotFocus -= this.scrollbar_GotFocus;
	}

	// Token: 0x06003ED7 RID: 16087 RVA: 0x000EE62C File Offset: 0x000EC82C
	private void scrollbar_GotFocus(dfControl control, dfFocusEventArgs args)
	{
		base.Focus();
	}

	// Token: 0x06003ED8 RID: 16088 RVA: 0x000EE634 File Offset: 0x000EC834
	private void scrollbar_ValueChanged(dfControl control, float value)
	{
		this.ScrollPosition = value;
	}

	// Token: 0x06003ED9 RID: 16089 RVA: 0x000EE640 File Offset: 0x000EC840
	private void synchronizeScrollbar()
	{
		if (this.scrollbar == null)
		{
			return;
		}
		int num = this.items.Length * this.itemHeight;
		float scrollSize = this.size.y - (float)this.listPadding.vertical;
		this.scrollbar.IncrementAmount = (float)this.itemHeight;
		this.scrollbar.MinValue = 0f;
		this.scrollbar.MaxValue = (float)num;
		this.scrollbar.ScrollSize = scrollSize;
		this.scrollbar.Value = this.scrollPosition;
	}

	// Token: 0x06003EDA RID: 16090 RVA: 0x000EE6D4 File Offset: 0x000EC8D4
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
		int count = this.renderData.Vertices.Count;
		this.renderHover();
		this.renderSelection();
		this.renderItems(this.textRenderData);
		this.clipQuads(this.renderData, count);
		this.clipQuads(this.textRenderData, 0);
		this.isControlInvalidated = false;
		this.updateCollider();
		return this.buffers;
	}

	// Token: 0x04002180 RID: 8576
	[SerializeField]
	protected dfFontBase font;

	// Token: 0x04002181 RID: 8577
	[SerializeField]
	protected RectOffset listPadding = new RectOffset();

	// Token: 0x04002182 RID: 8578
	[SerializeField]
	protected int selectedIndex = -1;

	// Token: 0x04002183 RID: 8579
	[SerializeField]
	protected Color32 itemTextColor = UnityEngine.Color.white;

	// Token: 0x04002184 RID: 8580
	[SerializeField]
	protected float itemTextScale = 1f;

	// Token: 0x04002185 RID: 8581
	[SerializeField]
	protected int itemHeight = 25;

	// Token: 0x04002186 RID: 8582
	[SerializeField]
	protected RectOffset itemPadding = new RectOffset();

	// Token: 0x04002187 RID: 8583
	[SerializeField]
	protected string[] items = new string[0];

	// Token: 0x04002188 RID: 8584
	[SerializeField]
	protected string itemHighlight = string.Empty;

	// Token: 0x04002189 RID: 8585
	[SerializeField]
	protected string itemHover = string.Empty;

	// Token: 0x0400218A RID: 8586
	[SerializeField]
	protected dfScrollbar scrollbar;

	// Token: 0x0400218B RID: 8587
	[SerializeField]
	protected bool animateHover;

	// Token: 0x0400218C RID: 8588
	[SerializeField]
	protected bool shadow;

	// Token: 0x0400218D RID: 8589
	[SerializeField]
	protected dfTextScaleMode textScaleMode;

	// Token: 0x0400218E RID: 8590
	[SerializeField]
	protected Color32 shadowColor = UnityEngine.Color.black;

	// Token: 0x0400218F RID: 8591
	[SerializeField]
	protected Vector2 shadowOffset = new Vector2(1f, -1f);

	// Token: 0x04002190 RID: 8592
	[SerializeField]
	protected TextAlignment itemAlignment;

	// Token: 0x04002191 RID: 8593
	private bool eventsAttached;

	// Token: 0x04002192 RID: 8594
	private float scrollPosition;

	// Token: 0x04002193 RID: 8595
	private int hoverIndex = -1;

	// Token: 0x04002194 RID: 8596
	private float hoverTweenLocation;

	// Token: 0x04002195 RID: 8597
	private Vector2 touchStartPosition = Vector2.zero;

	// Token: 0x04002196 RID: 8598
	private Vector2 startSize = Vector2.zero;

	// Token: 0x04002197 RID: 8599
	private dfRenderData textRenderData;

	// Token: 0x04002198 RID: 8600
	private dfList<dfRenderData> buffers = dfList<dfRenderData>.Obtain();
}
