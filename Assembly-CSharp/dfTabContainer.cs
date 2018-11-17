using System;
using UnityEngine;

// Token: 0x020007CE RID: 1998
[AddComponentMenu("Daikon Forge/User Interface/Containers/Tab Control/Tab Page Container")]
[ExecuteInEditMode]
[Serializable]
public class dfTabContainer : global::dfControl
{
	// Token: 0x14000053 RID: 83
	// (add) Token: 0x060044AA RID: 17578 RVA: 0x00100A74 File Offset: 0x000FEC74
	// (remove) Token: 0x060044AB RID: 17579 RVA: 0x00100A90 File Offset: 0x000FEC90
	public event global::PropertyChangedEventHandler<int> SelectedIndexChanged;

	// Token: 0x17000D37 RID: 3383
	// (get) Token: 0x060044AC RID: 17580 RVA: 0x00100AAC File Offset: 0x000FECAC
	// (set) Token: 0x060044AD RID: 17581 RVA: 0x00100AF4 File Offset: 0x000FECF4
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

	// Token: 0x17000D38 RID: 3384
	// (get) Token: 0x060044AE RID: 17582 RVA: 0x00100B14 File Offset: 0x000FED14
	// (set) Token: 0x060044AF RID: 17583 RVA: 0x00100B1C File Offset: 0x000FED1C
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

	// Token: 0x17000D39 RID: 3385
	// (get) Token: 0x060044B0 RID: 17584 RVA: 0x00100B3C File Offset: 0x000FED3C
	// (set) Token: 0x060044B1 RID: 17585 RVA: 0x00100B5C File Offset: 0x000FED5C
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
				this.arrangeTabPages();
			}
		}
	}

	// Token: 0x17000D3A RID: 3386
	// (get) Token: 0x060044B2 RID: 17586 RVA: 0x00100B90 File Offset: 0x000FED90
	// (set) Token: 0x060044B3 RID: 17587 RVA: 0x00100B98 File Offset: 0x000FED98
	public int SelectedIndex
	{
		get
		{
			return this.selectedIndex;
		}
		set
		{
			if (value != this.selectedIndex)
			{
				this.selectPageByIndex(value);
			}
		}
	}

	// Token: 0x060044B4 RID: 17588 RVA: 0x00100BB0 File Offset: 0x000FEDB0
	public global::dfControl AddTabPage()
	{
		global::dfPanel dfPanel = (from i in this.controls
		where i is global::dfPanel
		select i).FirstOrDefault() as global::dfPanel;
		string name = "Tab Page " + (this.controls.Count + 1);
		global::dfPanel dfPanel2 = base.AddControl<global::dfPanel>();
		dfPanel2.name = name;
		dfPanel2.Atlas = this.Atlas;
		dfPanel2.Anchor = global::dfAnchorStyle.All;
		dfPanel2.ClipChildren = true;
		if (dfPanel != null)
		{
			dfPanel2.Atlas = dfPanel.Atlas;
			dfPanel2.BackgroundSprite = dfPanel.BackgroundSprite;
		}
		this.arrangeTabPages();
		this.Invalidate();
		return dfPanel2;
	}

	// Token: 0x060044B5 RID: 17589 RVA: 0x00100C68 File Offset: 0x000FEE68
	public override void OnEnable()
	{
		base.OnEnable();
		if (this.size.sqrMagnitude < 1.401298E-45f)
		{
			base.Size = new Vector2(256f, 256f);
		}
	}

	// Token: 0x060044B6 RID: 17590 RVA: 0x00100CA8 File Offset: 0x000FEEA8
	protected internal override void OnControlAdded(global::dfControl child)
	{
		base.OnControlAdded(child);
		this.attachEvents(child);
		this.arrangeTabPages();
	}

	// Token: 0x060044B7 RID: 17591 RVA: 0x00100CC0 File Offset: 0x000FEEC0
	protected internal override void OnControlRemoved(global::dfControl child)
	{
		base.OnControlRemoved(child);
		this.detachEvents(child);
		this.arrangeTabPages();
	}

	// Token: 0x060044B8 RID: 17592 RVA: 0x00100CD8 File Offset: 0x000FEED8
	protected internal virtual void OnSelectedIndexChanged(int Index)
	{
		base.SignalHierarchy("OnSelectedIndexChanged", new object[]
		{
			Index
		});
		if (this.SelectedIndexChanged != null)
		{
			this.SelectedIndexChanged(this, Index);
		}
	}

	// Token: 0x060044B9 RID: 17593 RVA: 0x00100D10 File Offset: 0x000FEF10
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
		Color32 color = base.ApplyOpacity((!base.IsEnabled) ? this.disabledColor : this.color);
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

	// Token: 0x060044BA RID: 17594 RVA: 0x00100E3C File Offset: 0x000FF03C
	private void selectPageByIndex(int value)
	{
		value = Mathf.Max(Mathf.Min(value, this.controls.Count - 1), -1);
		if (value == this.selectedIndex)
		{
			return;
		}
		this.selectedIndex = value;
		for (int i = 0; i < this.controls.Count; i++)
		{
			global::dfControl dfControl = this.controls[i];
			if (!(dfControl == null))
			{
				dfControl.IsVisible = (i == value);
			}
		}
		this.arrangeTabPages();
		this.Invalidate();
		this.OnSelectedIndexChanged(value);
	}

	// Token: 0x060044BB RID: 17595 RVA: 0x00100ED0 File Offset: 0x000FF0D0
	private void arrangeTabPages()
	{
		if (this.padding == null)
		{
			this.padding = new RectOffset(0, 0, 0, 0);
		}
		Vector3 relativePosition;
		relativePosition..ctor((float)this.padding.left, (float)this.padding.top);
		Vector2 size;
		size..ctor(this.size.x - (float)this.padding.horizontal, this.size.y - (float)this.padding.vertical);
		for (int i = 0; i < this.controls.Count; i++)
		{
			global::dfPanel dfPanel = this.controls[i] as global::dfPanel;
			if (dfPanel != null)
			{
				dfPanel.Size = size;
				dfPanel.RelativePosition = relativePosition;
			}
		}
	}

	// Token: 0x060044BC RID: 17596 RVA: 0x00100F98 File Offset: 0x000FF198
	private void attachEvents(global::dfControl control)
	{
		control.IsVisibleChanged += this.control_IsVisibleChanged;
		control.PositionChanged += this.childControlInvalidated;
		control.SizeChanged += this.childControlInvalidated;
	}

	// Token: 0x060044BD RID: 17597 RVA: 0x00100FDC File Offset: 0x000FF1DC
	private void detachEvents(global::dfControl control)
	{
		control.IsVisibleChanged -= this.control_IsVisibleChanged;
		control.PositionChanged -= this.childControlInvalidated;
		control.SizeChanged -= this.childControlInvalidated;
	}

	// Token: 0x060044BE RID: 17598 RVA: 0x00101020 File Offset: 0x000FF220
	private void control_IsVisibleChanged(global::dfControl control, bool value)
	{
		this.onChildControlInvalidatedLayout();
	}

	// Token: 0x060044BF RID: 17599 RVA: 0x00101028 File Offset: 0x000FF228
	private void childControlInvalidated(global::dfControl control, Vector2 value)
	{
		this.onChildControlInvalidatedLayout();
	}

	// Token: 0x060044C0 RID: 17600 RVA: 0x00101030 File Offset: 0x000FF230
	private void onChildControlInvalidatedLayout()
	{
		if (base.IsLayoutSuspended)
		{
			return;
		}
		this.arrangeTabPages();
		this.Invalidate();
	}

	// Token: 0x0400244A RID: 9290
	[SerializeField]
	protected global::dfAtlas atlas;

	// Token: 0x0400244B RID: 9291
	[SerializeField]
	protected string backgroundSprite;

	// Token: 0x0400244C RID: 9292
	[SerializeField]
	protected RectOffset padding = new RectOffset();

	// Token: 0x0400244D RID: 9293
	[SerializeField]
	protected int selectedIndex;
}
