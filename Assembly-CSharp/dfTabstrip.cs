using System;
using UnityEngine;

// Token: 0x020007CF RID: 1999
[AddComponentMenu("Daikon Forge/User Interface/Containers/Tab Control/Tab Strip")]
[ExecuteInEditMode]
[Serializable]
public class dfTabstrip : global::dfControl
{
	// Token: 0x14000054 RID: 84
	// (add) Token: 0x060044C3 RID: 17603 RVA: 0x00101080 File Offset: 0x000FF280
	// (remove) Token: 0x060044C4 RID: 17604 RVA: 0x0010109C File Offset: 0x000FF29C
	public event global::PropertyChangedEventHandler<int> SelectedIndexChanged;

	// Token: 0x17000D3B RID: 3387
	// (get) Token: 0x060044C5 RID: 17605 RVA: 0x001010B8 File Offset: 0x000FF2B8
	// (set) Token: 0x060044C6 RID: 17606 RVA: 0x001010C0 File Offset: 0x000FF2C0
	public global::dfTabContainer TabPages
	{
		get
		{
			return this.pageContainer;
		}
		set
		{
			if (this.pageContainer != value)
			{
				this.pageContainer = value;
				if (value != null)
				{
					while (value.Controls.Count < this.controls.Count)
					{
						value.AddTabPage();
					}
				}
				this.pageContainer.SelectedIndex = this.SelectedIndex;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000D3C RID: 3388
	// (get) Token: 0x060044C7 RID: 17607 RVA: 0x00101130 File Offset: 0x000FF330
	// (set) Token: 0x060044C8 RID: 17608 RVA: 0x00101138 File Offset: 0x000FF338
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
				this.selectTabByIndex(value);
			}
		}
	}

	// Token: 0x17000D3D RID: 3389
	// (get) Token: 0x060044C9 RID: 17609 RVA: 0x00101150 File Offset: 0x000FF350
	// (set) Token: 0x060044CA RID: 17610 RVA: 0x00101198 File Offset: 0x000FF398
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

	// Token: 0x17000D3E RID: 3390
	// (get) Token: 0x060044CB RID: 17611 RVA: 0x001011B8 File Offset: 0x000FF3B8
	// (set) Token: 0x060044CC RID: 17612 RVA: 0x001011C0 File Offset: 0x000FF3C0
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

	// Token: 0x17000D3F RID: 3391
	// (get) Token: 0x060044CD RID: 17613 RVA: 0x001011E0 File Offset: 0x000FF3E0
	// (set) Token: 0x060044CE RID: 17614 RVA: 0x00101200 File Offset: 0x000FF400
	public RectOffset LayoutPadding
	{
		get
		{
			if (this.layoutPadding == null)
			{
				this.layoutPadding = new RectOffset();
			}
			return this.layoutPadding;
		}
		set
		{
			value = value.ConstrainPadding();
			if (!object.Equals(value, this.layoutPadding))
			{
				this.layoutPadding = value;
				this.arrangeTabs();
			}
		}
	}

	// Token: 0x17000D40 RID: 3392
	// (get) Token: 0x060044CF RID: 17615 RVA: 0x00101234 File Offset: 0x000FF434
	// (set) Token: 0x060044D0 RID: 17616 RVA: 0x0010123C File Offset: 0x000FF43C
	public bool AllowKeyboardNavigation
	{
		get
		{
			return this.allowKeyboardNavigation;
		}
		set
		{
			this.allowKeyboardNavigation = value;
		}
	}

	// Token: 0x060044D1 RID: 17617 RVA: 0x00101248 File Offset: 0x000FF448
	public void EnableTab(int index)
	{
		if (this.selectedIndex >= 0 && this.selectedIndex <= this.controls.Count - 1)
		{
			this.controls[index].Enable();
		}
	}

	// Token: 0x060044D2 RID: 17618 RVA: 0x00101280 File Offset: 0x000FF480
	public void DisableTab(int index)
	{
		if (this.selectedIndex >= 0 && this.selectedIndex <= this.controls.Count - 1)
		{
			this.controls[index].Disable();
		}
	}

	// Token: 0x060044D3 RID: 17619 RVA: 0x001012B8 File Offset: 0x000FF4B8
	public global::dfControl AddTab(string Text = "")
	{
		global::dfButton dfButton = (from i in this.controls
		where i is global::dfButton
		select i).FirstOrDefault() as global::dfButton;
		string text = "Tab " + (this.controls.Count + 1);
		if (string.IsNullOrEmpty(Text))
		{
			Text = text;
		}
		global::dfButton dfButton2 = base.AddControl<global::dfButton>();
		dfButton2.name = text;
		dfButton2.Atlas = this.Atlas;
		dfButton2.Text = Text;
		dfButton2.ButtonGroup = this;
		if (dfButton != null)
		{
			dfButton2.Atlas = dfButton.Atlas;
			dfButton2.Font = dfButton.Font;
			dfButton2.AutoSize = dfButton.AutoSize;
			dfButton2.Size = dfButton.Size;
			dfButton2.BackgroundSprite = dfButton.BackgroundSprite;
			dfButton2.DisabledSprite = dfButton.DisabledSprite;
			dfButton2.FocusSprite = dfButton.FocusSprite;
			dfButton2.HoverSprite = dfButton.HoverSprite;
			dfButton2.PressedSprite = dfButton.PressedSprite;
			dfButton2.Shadow = dfButton.Shadow;
			dfButton2.ShadowColor = dfButton.ShadowColor;
			dfButton2.ShadowOffset = dfButton.ShadowOffset;
			dfButton2.TextColor = dfButton.TextColor;
			dfButton2.TextAlignment = dfButton.TextAlignment;
			RectOffset padding = dfButton.Padding;
			dfButton2.Padding = new RectOffset(padding.left, padding.right, padding.top, padding.bottom);
		}
		if (this.pageContainer != null)
		{
			this.pageContainer.AddTabPage();
		}
		this.arrangeTabs();
		this.Invalidate();
		return dfButton2;
	}

	// Token: 0x060044D4 RID: 17620 RVA: 0x00101454 File Offset: 0x000FF654
	protected internal override void OnGotFocus(global::dfFocusEventArgs args)
	{
		if (this.controls.Contains(args.GotFocus))
		{
			this.SelectedIndex = args.GotFocus.ZOrder;
		}
		base.OnGotFocus(args);
	}

	// Token: 0x060044D5 RID: 17621 RVA: 0x00101490 File Offset: 0x000FF690
	protected internal override void OnLostFocus(global::dfFocusEventArgs args)
	{
		base.OnLostFocus(args);
		if (this.controls.Contains(args.LostFocus))
		{
			this.showSelectedTab();
		}
	}

	// Token: 0x060044D6 RID: 17622 RVA: 0x001014B8 File Offset: 0x000FF6B8
	protected internal override void OnClick(global::dfMouseEventArgs args)
	{
		if (this.controls.Contains(args.Source))
		{
			this.SelectedIndex = args.Source.ZOrder;
		}
		base.OnClick(args);
	}

	// Token: 0x060044D7 RID: 17623 RVA: 0x001014F4 File Offset: 0x000FF6F4
	private void OnClick(global::dfControl sender, global::dfMouseEventArgs args)
	{
		if (!this.controls.Contains(args.Source))
		{
			return;
		}
		this.SelectedIndex = args.Source.ZOrder;
	}

	// Token: 0x060044D8 RID: 17624 RVA: 0x0010152C File Offset: 0x000FF72C
	protected internal override void OnKeyDown(global::dfKeyEventArgs args)
	{
		if (args.Used)
		{
			return;
		}
		if (this.allowKeyboardNavigation)
		{
			if (args.KeyCode == 276 || (args.KeyCode == 9 && args.Shift))
			{
				this.SelectedIndex = Mathf.Max(0, this.SelectedIndex - 1);
				args.Use();
				return;
			}
			if (args.KeyCode == 275 || args.KeyCode == 9)
			{
				this.SelectedIndex++;
				args.Use();
				return;
			}
		}
		base.OnKeyDown(args);
	}

	// Token: 0x060044D9 RID: 17625 RVA: 0x001015CC File Offset: 0x000FF7CC
	protected internal override void OnControlAdded(global::dfControl child)
	{
		base.OnControlAdded(child);
		this.attachEvents(child);
		this.arrangeTabs();
	}

	// Token: 0x060044DA RID: 17626 RVA: 0x001015E4 File Offset: 0x000FF7E4
	protected internal override void OnControlRemoved(global::dfControl child)
	{
		base.OnControlRemoved(child);
		this.detachEvents(child);
		this.arrangeTabs();
	}

	// Token: 0x060044DB RID: 17627 RVA: 0x001015FC File Offset: 0x000FF7FC
	public override void OnEnable()
	{
		base.OnEnable();
		if (this.size.sqrMagnitude < 1.401298E-45f)
		{
			base.Size = new Vector2(256f, 26f);
		}
		if (Application.isPlaying)
		{
			this.selectTabByIndex(Mathf.Max(this.selectedIndex, 0));
		}
	}

	// Token: 0x060044DC RID: 17628 RVA: 0x00101658 File Offset: 0x000FF858
	public override void Update()
	{
		base.Update();
		if (this.isControlInvalidated)
		{
			this.arrangeTabs();
		}
		this.showSelectedTab();
	}

	// Token: 0x060044DD RID: 17629 RVA: 0x00101678 File Offset: 0x000FF878
	protected internal virtual void OnSelectedIndexChanged()
	{
		base.SignalHierarchy("OnSelectedIndexChanged", new object[]
		{
			this.SelectedIndex
		});
		if (this.SelectedIndexChanged != null)
		{
			this.SelectedIndexChanged(this, this.SelectedIndex);
		}
	}

	// Token: 0x060044DE RID: 17630 RVA: 0x001016C4 File Offset: 0x000FF8C4
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

	// Token: 0x060044DF RID: 17631 RVA: 0x001017F0 File Offset: 0x000FF9F0
	private void showSelectedTab()
	{
		if (this.selectedIndex >= 0 && this.selectedIndex <= this.controls.Count - 1)
		{
			global::dfButton dfButton = this.controls[this.selectedIndex] as global::dfButton;
			if (dfButton != null && !dfButton.ContainsMouse)
			{
				dfButton.State = global::dfButton.ButtonState.Focus;
			}
		}
	}

	// Token: 0x060044E0 RID: 17632 RVA: 0x00101858 File Offset: 0x000FFA58
	private void selectTabByIndex(int value)
	{
		value = Mathf.Max(Mathf.Min(value, this.controls.Count - 1), -1);
		if (value == this.selectedIndex)
		{
			return;
		}
		this.selectedIndex = value;
		for (int i = 0; i < this.controls.Count; i++)
		{
			global::dfButton dfButton = this.controls[i] as global::dfButton;
			if (!(dfButton == null))
			{
				if (i == value)
				{
					dfButton.State = global::dfButton.ButtonState.Focus;
				}
				else
				{
					dfButton.State = global::dfButton.ButtonState.Default;
				}
			}
		}
		this.Invalidate();
		this.OnSelectedIndexChanged();
		if (this.pageContainer != null)
		{
			this.pageContainer.SelectedIndex = value;
		}
	}

	// Token: 0x060044E1 RID: 17633 RVA: 0x00101918 File Offset: 0x000FFB18
	private void arrangeTabs()
	{
		this.SuspendLayout();
		try
		{
			this.layoutPadding = this.layoutPadding.ConstrainPadding();
			float num = (float)this.layoutPadding.left - this.scrollPosition.x;
			float num2 = (float)this.layoutPadding.top - this.scrollPosition.y;
			float num3 = 0f;
			float num4 = 0f;
			for (int i = 0; i < base.Controls.Count; i++)
			{
				global::dfControl dfControl = this.controls[i];
				if (dfControl.IsVisible && dfControl.enabled && dfControl.gameObject.activeSelf)
				{
					Vector2 vector;
					vector..ctor(num, num2);
					dfControl.RelativePosition = vector;
					float num5 = dfControl.Width + (float)this.layoutPadding.horizontal;
					float num6 = dfControl.Height + (float)this.layoutPadding.vertical;
					num3 = Mathf.Max(num5, num3);
					num4 = Mathf.Max(num6, num4);
					num += num5;
				}
			}
		}
		finally
		{
			this.ResumeLayout();
		}
	}

	// Token: 0x060044E2 RID: 17634 RVA: 0x00101A5C File Offset: 0x000FFC5C
	private void attachEvents(global::dfControl control)
	{
		control.IsVisibleChanged += this.control_IsVisibleChanged;
		control.PositionChanged += this.childControlInvalidated;
		control.SizeChanged += this.childControlInvalidated;
		control.ZOrderChanged += this.childControlZOrderChanged;
	}

	// Token: 0x060044E3 RID: 17635 RVA: 0x00101AB4 File Offset: 0x000FFCB4
	private void detachEvents(global::dfControl control)
	{
		control.IsVisibleChanged -= this.control_IsVisibleChanged;
		control.PositionChanged -= this.childControlInvalidated;
		control.SizeChanged -= this.childControlInvalidated;
	}

	// Token: 0x060044E4 RID: 17636 RVA: 0x00101AF8 File Offset: 0x000FFCF8
	private void childControlZOrderChanged(global::dfControl control, int value)
	{
		this.onChildControlInvalidatedLayout();
	}

	// Token: 0x060044E5 RID: 17637 RVA: 0x00101B00 File Offset: 0x000FFD00
	private void control_IsVisibleChanged(global::dfControl control, bool value)
	{
		this.onChildControlInvalidatedLayout();
	}

	// Token: 0x060044E6 RID: 17638 RVA: 0x00101B08 File Offset: 0x000FFD08
	private void childControlInvalidated(global::dfControl control, Vector2 value)
	{
		this.onChildControlInvalidatedLayout();
	}

	// Token: 0x060044E7 RID: 17639 RVA: 0x00101B10 File Offset: 0x000FFD10
	private void onChildControlInvalidatedLayout()
	{
		if (base.IsLayoutSuspended)
		{
			return;
		}
		this.arrangeTabs();
		this.Invalidate();
	}

	// Token: 0x04002450 RID: 9296
	[SerializeField]
	protected global::dfAtlas atlas;

	// Token: 0x04002451 RID: 9297
	[SerializeField]
	protected string backgroundSprite;

	// Token: 0x04002452 RID: 9298
	[SerializeField]
	protected RectOffset layoutPadding = new RectOffset();

	// Token: 0x04002453 RID: 9299
	[SerializeField]
	protected Vector2 scrollPosition = Vector2.zero;

	// Token: 0x04002454 RID: 9300
	[SerializeField]
	protected int selectedIndex;

	// Token: 0x04002455 RID: 9301
	[SerializeField]
	protected global::dfTabContainer pageContainer;

	// Token: 0x04002456 RID: 9302
	[SerializeField]
	protected bool allowKeyboardNavigation = true;
}
