using System;
using UnityEngine;

// Token: 0x020007B1 RID: 1969
[RequireComponent(typeof(BoxCollider))]
[AddComponentMenu("Daikon Forge/User Interface/Listbox")]
[ExecuteInEditMode]
[Serializable]
public class dfListbox : global::dfInteractiveBase, global::IDFMultiRender
{
	// Token: 0x1400004C RID: 76
	// (add) Token: 0x060042AF RID: 17071 RVA: 0x000F5AFC File Offset: 0x000F3CFC
	// (remove) Token: 0x060042B0 RID: 17072 RVA: 0x000F5B18 File Offset: 0x000F3D18
	public event global::PropertyChangedEventHandler<int> SelectedIndexChanged;

	// Token: 0x1400004D RID: 77
	// (add) Token: 0x060042B1 RID: 17073 RVA: 0x000F5B34 File Offset: 0x000F3D34
	// (remove) Token: 0x060042B2 RID: 17074 RVA: 0x000F5B50 File Offset: 0x000F3D50
	public event global::PropertyChangedEventHandler<int> ItemClicked;

	// Token: 0x17000CC1 RID: 3265
	// (get) Token: 0x060042B3 RID: 17075 RVA: 0x000F5B6C File Offset: 0x000F3D6C
	// (set) Token: 0x060042B4 RID: 17076 RVA: 0x000F5BB0 File Offset: 0x000F3DB0
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
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CC2 RID: 3266
	// (get) Token: 0x060042B5 RID: 17077 RVA: 0x000F5BD0 File Offset: 0x000F3DD0
	// (set) Token: 0x060042B6 RID: 17078 RVA: 0x000F5BD8 File Offset: 0x000F3DD8
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

	// Token: 0x17000CC3 RID: 3267
	// (get) Token: 0x060042B7 RID: 17079 RVA: 0x000F5C0C File Offset: 0x000F3E0C
	// (set) Token: 0x060042B8 RID: 17080 RVA: 0x000F5C14 File Offset: 0x000F3E14
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

	// Token: 0x17000CC4 RID: 3268
	// (get) Token: 0x060042B9 RID: 17081 RVA: 0x000F5C30 File Offset: 0x000F3E30
	// (set) Token: 0x060042BA RID: 17082 RVA: 0x000F5C38 File Offset: 0x000F3E38
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

	// Token: 0x17000CC5 RID: 3269
	// (get) Token: 0x060042BB RID: 17083 RVA: 0x000F5C58 File Offset: 0x000F3E58
	// (set) Token: 0x060042BC RID: 17084 RVA: 0x000F5C60 File Offset: 0x000F3E60
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

	// Token: 0x17000CC6 RID: 3270
	// (get) Token: 0x060042BD RID: 17085 RVA: 0x000F5C80 File Offset: 0x000F3E80
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

	// Token: 0x17000CC7 RID: 3271
	// (get) Token: 0x060042BE RID: 17086 RVA: 0x000F5CA0 File Offset: 0x000F3EA0
	// (set) Token: 0x060042BF RID: 17087 RVA: 0x000F5CB0 File Offset: 0x000F3EB0
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

	// Token: 0x17000CC8 RID: 3272
	// (get) Token: 0x060042C0 RID: 17088 RVA: 0x000F5CFC File Offset: 0x000F3EFC
	// (set) Token: 0x060042C1 RID: 17089 RVA: 0x000F5D04 File Offset: 0x000F3F04
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

	// Token: 0x17000CC9 RID: 3273
	// (get) Token: 0x060042C2 RID: 17090 RVA: 0x000F5D54 File Offset: 0x000F3F54
	// (set) Token: 0x060042C3 RID: 17091 RVA: 0x000F5D74 File Offset: 0x000F3F74
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

	// Token: 0x17000CCA RID: 3274
	// (get) Token: 0x060042C4 RID: 17092 RVA: 0x000F5DA8 File Offset: 0x000F3FA8
	// (set) Token: 0x060042C5 RID: 17093 RVA: 0x000F5DB0 File Offset: 0x000F3FB0
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

	// Token: 0x17000CCB RID: 3275
	// (get) Token: 0x060042C6 RID: 17094 RVA: 0x000F5DE8 File Offset: 0x000F3FE8
	// (set) Token: 0x060042C7 RID: 17095 RVA: 0x000F5DF0 File Offset: 0x000F3FF0
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

	// Token: 0x17000CCC RID: 3276
	// (get) Token: 0x060042C8 RID: 17096 RVA: 0x000F5E20 File Offset: 0x000F4020
	// (set) Token: 0x060042C9 RID: 17097 RVA: 0x000F5E28 File Offset: 0x000F4028
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

	// Token: 0x17000CCD RID: 3277
	// (get) Token: 0x060042CA RID: 17098 RVA: 0x000F5E58 File Offset: 0x000F4058
	// (set) Token: 0x060042CB RID: 17099 RVA: 0x000F5E78 File Offset: 0x000F4078
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

	// Token: 0x17000CCE RID: 3278
	// (get) Token: 0x060042CC RID: 17100 RVA: 0x000F5EB8 File Offset: 0x000F40B8
	// (set) Token: 0x060042CD RID: 17101 RVA: 0x000F5EC0 File Offset: 0x000F40C0
	public global::dfScrollbar Scrollbar
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

	// Token: 0x17000CCF RID: 3279
	// (get) Token: 0x060042CE RID: 17102 RVA: 0x000F5EF8 File Offset: 0x000F40F8
	// (set) Token: 0x060042CF RID: 17103 RVA: 0x000F5F18 File Offset: 0x000F4118
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

	// Token: 0x17000CD0 RID: 3280
	// (get) Token: 0x060042D0 RID: 17104 RVA: 0x000F5F4C File Offset: 0x000F414C
	// (set) Token: 0x060042D1 RID: 17105 RVA: 0x000F5F54 File Offset: 0x000F4154
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

	// Token: 0x17000CD1 RID: 3281
	// (get) Token: 0x060042D2 RID: 17106 RVA: 0x000F5F70 File Offset: 0x000F4170
	// (set) Token: 0x060042D3 RID: 17107 RVA: 0x000F5F78 File Offset: 0x000F4178
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

	// Token: 0x17000CD2 RID: 3282
	// (get) Token: 0x060042D4 RID: 17108 RVA: 0x000F5FB0 File Offset: 0x000F41B0
	// (set) Token: 0x060042D5 RID: 17109 RVA: 0x000F5FB8 File Offset: 0x000F41B8
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

	// Token: 0x17000CD3 RID: 3283
	// (get) Token: 0x060042D6 RID: 17110 RVA: 0x000F5FD8 File Offset: 0x000F41D8
	// (set) Token: 0x060042D7 RID: 17111 RVA: 0x000F5FE0 File Offset: 0x000F41E0
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

	// Token: 0x17000CD4 RID: 3284
	// (get) Token: 0x060042D8 RID: 17112 RVA: 0x000F5FEC File Offset: 0x000F41EC
	// (set) Token: 0x060042D9 RID: 17113 RVA: 0x000F5FF4 File Offset: 0x000F41F4
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

	// Token: 0x060042DA RID: 17114 RVA: 0x000F6004 File Offset: 0x000F4204
	public override void Awake()
	{
		base.Awake();
		this.startSize = base.Size;
	}

	// Token: 0x060042DB RID: 17115 RVA: 0x000F6018 File Offset: 0x000F4218
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

	// Token: 0x060042DC RID: 17116 RVA: 0x000F60B0 File Offset: 0x000F42B0
	public override void LateUpdate()
	{
		base.LateUpdate();
		if (!Application.isPlaying)
		{
			return;
		}
		this.attachScrollbarEvents();
	}

	// Token: 0x060042DD RID: 17117 RVA: 0x000F60CC File Offset: 0x000F42CC
	public override void OnDestroy()
	{
		base.OnDestroy();
		this.detachScrollbarEvents();
	}

	// Token: 0x060042DE RID: 17118 RVA: 0x000F60DC File Offset: 0x000F42DC
	public override void OnDisable()
	{
		base.OnDisable();
		this.detachScrollbarEvents();
	}

	// Token: 0x060042DF RID: 17119 RVA: 0x000F60EC File Offset: 0x000F42EC
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

	// Token: 0x060042E0 RID: 17120 RVA: 0x000F612C File Offset: 0x000F432C
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

	// Token: 0x060042E1 RID: 17121 RVA: 0x000F616C File Offset: 0x000F436C
	protected internal override void OnMouseMove(global::dfMouseEventArgs args)
	{
		base.OnMouseMove(args);
		if (!(args is global::dfTouchEventArgs))
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

	// Token: 0x060042E2 RID: 17122 RVA: 0x000F61F4 File Offset: 0x000F43F4
	protected internal override void OnMouseEnter(global::dfMouseEventArgs args)
	{
		base.OnMouseEnter(args);
		this.touchStartPosition = args.Position;
	}

	// Token: 0x060042E3 RID: 17123 RVA: 0x000F620C File Offset: 0x000F440C
	protected internal override void OnMouseLeave(global::dfMouseEventArgs args)
	{
		base.OnMouseLeave(args);
		this.hoverIndex = -1;
	}

	// Token: 0x060042E4 RID: 17124 RVA: 0x000F621C File Offset: 0x000F441C
	protected internal override void OnMouseWheel(global::dfMouseEventArgs args)
	{
		base.OnMouseWheel(args);
		this.ScrollPosition = Mathf.Max(0f, this.ScrollPosition - (float)((int)args.WheelDelta * this.ItemHeight));
		this.synchronizeScrollbar();
		this.updateItemHover(args);
	}

	// Token: 0x060042E5 RID: 17125 RVA: 0x000F6264 File Offset: 0x000F4464
	protected internal override void OnMouseUp(global::dfMouseEventArgs args)
	{
		this.hoverIndex = -1;
		base.OnMouseUp(args);
		if (args is global::dfTouchEventArgs && Mathf.Abs(args.Position.y - this.touchStartPosition.y) < (float)this.itemHeight)
		{
			this.selectItemUnderMouse(args);
		}
	}

	// Token: 0x060042E6 RID: 17126 RVA: 0x000F62BC File Offset: 0x000F44BC
	protected internal override void OnMouseDown(global::dfMouseEventArgs args)
	{
		base.OnMouseDown(args);
		if (args is global::dfTouchEventArgs)
		{
			this.touchStartPosition = args.Position;
			return;
		}
		this.selectItemUnderMouse(args);
	}

	// Token: 0x060042E7 RID: 17127 RVA: 0x000F62F0 File Offset: 0x000F44F0
	protected internal override void OnKeyDown(global::dfKeyEventArgs args)
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

	// Token: 0x060042E8 RID: 17128 RVA: 0x000F6404 File Offset: 0x000F4604
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

	// Token: 0x060042E9 RID: 17129 RVA: 0x000F6470 File Offset: 0x000F4670
	private void selectItemUnderMouse(global::dfMouseEventArgs args)
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

	// Token: 0x060042EA RID: 17130 RVA: 0x000F656C File Offset: 0x000F476C
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
		global::dfAtlas.ItemInfo itemInfo = base.Atlas[this.ItemHover];
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
		global::dfSprite.RenderOptions options = new global::dfSprite.RenderOptions
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
			global::dfSlicedSprite.renderSprite(this.renderData, options);
		}
		else
		{
			global::dfSprite.renderSprite(this.renderData, options);
		}
		if ((float)num2 != this.hoverTweenLocation)
		{
			this.Invalidate();
		}
	}

	// Token: 0x060042EB RID: 17131 RVA: 0x000F67F0 File Offset: 0x000F49F0
	private void renderSelection()
	{
		if (base.Atlas == null || this.selectedIndex < 0)
		{
			return;
		}
		global::dfAtlas.ItemInfo itemInfo = base.Atlas[this.ItemHighlight];
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
		global::dfSprite.RenderOptions options = new global::dfSprite.RenderOptions
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
			global::dfSlicedSprite.renderSprite(this.renderData, options);
		}
		else
		{
			global::dfSprite.renderSprite(this.renderData, options);
		}
	}

	// Token: 0x060042EC RID: 17132 RVA: 0x000F6974 File Offset: 0x000F4B74
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

	// Token: 0x060042ED RID: 17133 RVA: 0x000F69D8 File Offset: 0x000F4BD8
	private void renderItems(global::dfRenderData buffer)
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
			using (global::dfFontRendererBase dfFontRendererBase = this.font.ObtainRenderer())
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
				global::dfDynamicFont.DynamicFontRenderer dynamicFontRenderer = dfFontRendererBase as global::dfDynamicFont.DynamicFontRenderer;
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

	// Token: 0x060042EE RID: 17134 RVA: 0x000F6C80 File Offset: 0x000F4E80
	private void clipQuads(global::dfRenderData buffer, int startIndex)
	{
		global::dfList<Vector3> vertices = buffer.Vertices;
		global::dfList<Vector2> uv = buffer.UV;
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
				global::dfList<Vector3> dfList = vertices;
				int index = i;
				value..ctor(value.x, Mathf.Max(value.y, num3), value2.z);
				dfList[index] = value;
				global::dfList<Vector3> dfList2 = vertices;
				int index2 = i + 1;
				value2..ctor(value2.x, Mathf.Max(value2.y, num3), value2.z);
				dfList2[index2] = value2;
				global::dfList<Vector3> dfList3 = vertices;
				int index3 = i + 2;
				value3..ctor(value3.x, Mathf.Max(value3.y, num3), value3.z);
				dfList3[index3] = value3;
				global::dfList<Vector3> dfList4 = vertices;
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

	// Token: 0x060042EF RID: 17135 RVA: 0x000F6FEC File Offset: 0x000F51EC
	private void updateItemHover(global::dfMouseEventArgs args)
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

	// Token: 0x060042F0 RID: 17136 RVA: 0x000F7114 File Offset: 0x000F5314
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

	// Token: 0x060042F1 RID: 17137 RVA: 0x000F7170 File Offset: 0x000F5370
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

	// Token: 0x060042F2 RID: 17138 RVA: 0x000F71D0 File Offset: 0x000F53D0
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

	// Token: 0x060042F3 RID: 17139 RVA: 0x000F7230 File Offset: 0x000F5430
	private void scrollbar_GotFocus(global::dfControl control, global::dfFocusEventArgs args)
	{
		base.Focus();
	}

	// Token: 0x060042F4 RID: 17140 RVA: 0x000F7238 File Offset: 0x000F5438
	private void scrollbar_ValueChanged(global::dfControl control, float value)
	{
		this.ScrollPosition = value;
	}

	// Token: 0x060042F5 RID: 17141 RVA: 0x000F7244 File Offset: 0x000F5444
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

	// Token: 0x060042F6 RID: 17142 RVA: 0x000F72D8 File Offset: 0x000F54D8
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

	// Token: 0x04002389 RID: 9097
	[SerializeField]
	protected global::dfFontBase font;

	// Token: 0x0400238A RID: 9098
	[SerializeField]
	protected RectOffset listPadding = new RectOffset();

	// Token: 0x0400238B RID: 9099
	[SerializeField]
	protected int selectedIndex = -1;

	// Token: 0x0400238C RID: 9100
	[SerializeField]
	protected Color32 itemTextColor = UnityEngine.Color.white;

	// Token: 0x0400238D RID: 9101
	[SerializeField]
	protected float itemTextScale = 1f;

	// Token: 0x0400238E RID: 9102
	[SerializeField]
	protected int itemHeight = 25;

	// Token: 0x0400238F RID: 9103
	[SerializeField]
	protected RectOffset itemPadding = new RectOffset();

	// Token: 0x04002390 RID: 9104
	[SerializeField]
	protected string[] items = new string[0];

	// Token: 0x04002391 RID: 9105
	[SerializeField]
	protected string itemHighlight = string.Empty;

	// Token: 0x04002392 RID: 9106
	[SerializeField]
	protected string itemHover = string.Empty;

	// Token: 0x04002393 RID: 9107
	[SerializeField]
	protected global::dfScrollbar scrollbar;

	// Token: 0x04002394 RID: 9108
	[SerializeField]
	protected bool animateHover;

	// Token: 0x04002395 RID: 9109
	[SerializeField]
	protected bool shadow;

	// Token: 0x04002396 RID: 9110
	[SerializeField]
	protected global::dfTextScaleMode textScaleMode;

	// Token: 0x04002397 RID: 9111
	[SerializeField]
	protected Color32 shadowColor = UnityEngine.Color.black;

	// Token: 0x04002398 RID: 9112
	[SerializeField]
	protected Vector2 shadowOffset = new Vector2(1f, -1f);

	// Token: 0x04002399 RID: 9113
	[SerializeField]
	protected TextAlignment itemAlignment;

	// Token: 0x0400239A RID: 9114
	private bool eventsAttached;

	// Token: 0x0400239B RID: 9115
	private float scrollPosition;

	// Token: 0x0400239C RID: 9116
	private int hoverIndex = -1;

	// Token: 0x0400239D RID: 9117
	private float hoverTweenLocation;

	// Token: 0x0400239E RID: 9118
	private Vector2 touchStartPosition = Vector2.zero;

	// Token: 0x0400239F RID: 9119
	private Vector2 startSize = Vector2.zero;

	// Token: 0x040023A0 RID: 9120
	private global::dfRenderData textRenderData;

	// Token: 0x040023A1 RID: 9121
	private global::dfList<global::dfRenderData> buffers = global::dfList<global::dfRenderData>.Obtain();
}
