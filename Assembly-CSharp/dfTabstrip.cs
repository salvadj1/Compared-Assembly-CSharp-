using System;
using UnityEngine;

// Token: 0x020006FD RID: 1789
[AddComponentMenu("Daikon Forge/User Interface/Containers/Tab Control/Tab Strip")]
[ExecuteInEditMode]
[Serializable]
public class dfTabstrip : dfControl
{
	// Token: 0x14000054 RID: 84
	// (add) Token: 0x060040A7 RID: 16551 RVA: 0x000F847C File Offset: 0x000F667C
	// (remove) Token: 0x060040A8 RID: 16552 RVA: 0x000F8498 File Offset: 0x000F6698
	public event PropertyChangedEventHandler<int> SelectedIndexChanged;

	// Token: 0x17000CB7 RID: 3255
	// (get) Token: 0x060040A9 RID: 16553 RVA: 0x000F84B4 File Offset: 0x000F66B4
	// (set) Token: 0x060040AA RID: 16554 RVA: 0x000F84BC File Offset: 0x000F66BC
	public dfTabContainer TabPages
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

	// Token: 0x17000CB8 RID: 3256
	// (get) Token: 0x060040AB RID: 16555 RVA: 0x000F852C File Offset: 0x000F672C
	// (set) Token: 0x060040AC RID: 16556 RVA: 0x000F8534 File Offset: 0x000F6734
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

	// Token: 0x17000CB9 RID: 3257
	// (get) Token: 0x060040AD RID: 16557 RVA: 0x000F854C File Offset: 0x000F674C
	// (set) Token: 0x060040AE RID: 16558 RVA: 0x000F8594 File Offset: 0x000F6794
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

	// Token: 0x17000CBA RID: 3258
	// (get) Token: 0x060040AF RID: 16559 RVA: 0x000F85B4 File Offset: 0x000F67B4
	// (set) Token: 0x060040B0 RID: 16560 RVA: 0x000F85BC File Offset: 0x000F67BC
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

	// Token: 0x17000CBB RID: 3259
	// (get) Token: 0x060040B1 RID: 16561 RVA: 0x000F85DC File Offset: 0x000F67DC
	// (set) Token: 0x060040B2 RID: 16562 RVA: 0x000F85FC File Offset: 0x000F67FC
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

	// Token: 0x17000CBC RID: 3260
	// (get) Token: 0x060040B3 RID: 16563 RVA: 0x000F8630 File Offset: 0x000F6830
	// (set) Token: 0x060040B4 RID: 16564 RVA: 0x000F8638 File Offset: 0x000F6838
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

	// Token: 0x060040B5 RID: 16565 RVA: 0x000F8644 File Offset: 0x000F6844
	public void EnableTab(int index)
	{
		if (this.selectedIndex >= 0 && this.selectedIndex <= this.controls.Count - 1)
		{
			this.controls[index].Enable();
		}
	}

	// Token: 0x060040B6 RID: 16566 RVA: 0x000F867C File Offset: 0x000F687C
	public void DisableTab(int index)
	{
		if (this.selectedIndex >= 0 && this.selectedIndex <= this.controls.Count - 1)
		{
			this.controls[index].Disable();
		}
	}

	// Token: 0x060040B7 RID: 16567 RVA: 0x000F86B4 File Offset: 0x000F68B4
	public dfControl AddTab(string Text = "")
	{
		dfButton dfButton = (from i in this.controls
		where i is dfButton
		select i).FirstOrDefault() as dfButton;
		string text = "Tab " + (this.controls.Count + 1);
		if (string.IsNullOrEmpty(Text))
		{
			Text = text;
		}
		dfButton dfButton2 = base.AddControl<dfButton>();
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

	// Token: 0x060040B8 RID: 16568 RVA: 0x000F8850 File Offset: 0x000F6A50
	protected internal override void OnGotFocus(dfFocusEventArgs args)
	{
		if (this.controls.Contains(args.GotFocus))
		{
			this.SelectedIndex = args.GotFocus.ZOrder;
		}
		base.OnGotFocus(args);
	}

	// Token: 0x060040B9 RID: 16569 RVA: 0x000F888C File Offset: 0x000F6A8C
	protected internal override void OnLostFocus(dfFocusEventArgs args)
	{
		base.OnLostFocus(args);
		if (this.controls.Contains(args.LostFocus))
		{
			this.showSelectedTab();
		}
	}

	// Token: 0x060040BA RID: 16570 RVA: 0x000F88B4 File Offset: 0x000F6AB4
	protected internal override void OnClick(dfMouseEventArgs args)
	{
		if (this.controls.Contains(args.Source))
		{
			this.SelectedIndex = args.Source.ZOrder;
		}
		base.OnClick(args);
	}

	// Token: 0x060040BB RID: 16571 RVA: 0x000F88F0 File Offset: 0x000F6AF0
	private void OnClick(dfControl sender, dfMouseEventArgs args)
	{
		if (!this.controls.Contains(args.Source))
		{
			return;
		}
		this.SelectedIndex = args.Source.ZOrder;
	}

	// Token: 0x060040BC RID: 16572 RVA: 0x000F8928 File Offset: 0x000F6B28
	protected internal override void OnKeyDown(dfKeyEventArgs args)
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

	// Token: 0x060040BD RID: 16573 RVA: 0x000F89C8 File Offset: 0x000F6BC8
	protected internal override void OnControlAdded(dfControl child)
	{
		base.OnControlAdded(child);
		this.attachEvents(child);
		this.arrangeTabs();
	}

	// Token: 0x060040BE RID: 16574 RVA: 0x000F89E0 File Offset: 0x000F6BE0
	protected internal override void OnControlRemoved(dfControl child)
	{
		base.OnControlRemoved(child);
		this.detachEvents(child);
		this.arrangeTabs();
	}

	// Token: 0x060040BF RID: 16575 RVA: 0x000F89F8 File Offset: 0x000F6BF8
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

	// Token: 0x060040C0 RID: 16576 RVA: 0x000F8A54 File Offset: 0x000F6C54
	public override void Update()
	{
		base.Update();
		if (this.isControlInvalidated)
		{
			this.arrangeTabs();
		}
		this.showSelectedTab();
	}

	// Token: 0x060040C1 RID: 16577 RVA: 0x000F8A74 File Offset: 0x000F6C74
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

	// Token: 0x060040C2 RID: 16578 RVA: 0x000F8AC0 File Offset: 0x000F6CC0
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
		Color32 color = base.ApplyOpacity((!base.IsEnabled) ? this.disabledColor : this.color);
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

	// Token: 0x060040C3 RID: 16579 RVA: 0x000F8BEC File Offset: 0x000F6DEC
	private void showSelectedTab()
	{
		if (this.selectedIndex >= 0 && this.selectedIndex <= this.controls.Count - 1)
		{
			dfButton dfButton = this.controls[this.selectedIndex] as dfButton;
			if (dfButton != null && !dfButton.ContainsMouse)
			{
				dfButton.State = dfButton.ButtonState.Focus;
			}
		}
	}

	// Token: 0x060040C4 RID: 16580 RVA: 0x000F8C54 File Offset: 0x000F6E54
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
			dfButton dfButton = this.controls[i] as dfButton;
			if (!(dfButton == null))
			{
				if (i == value)
				{
					dfButton.State = dfButton.ButtonState.Focus;
				}
				else
				{
					dfButton.State = dfButton.ButtonState.Default;
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

	// Token: 0x060040C5 RID: 16581 RVA: 0x000F8D14 File Offset: 0x000F6F14
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
				dfControl dfControl = this.controls[i];
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

	// Token: 0x060040C6 RID: 16582 RVA: 0x000F8E58 File Offset: 0x000F7058
	private void attachEvents(dfControl control)
	{
		control.IsVisibleChanged += this.control_IsVisibleChanged;
		control.PositionChanged += this.childControlInvalidated;
		control.SizeChanged += this.childControlInvalidated;
		control.ZOrderChanged += this.childControlZOrderChanged;
	}

	// Token: 0x060040C7 RID: 16583 RVA: 0x000F8EB0 File Offset: 0x000F70B0
	private void detachEvents(dfControl control)
	{
		control.IsVisibleChanged -= this.control_IsVisibleChanged;
		control.PositionChanged -= this.childControlInvalidated;
		control.SizeChanged -= this.childControlInvalidated;
	}

	// Token: 0x060040C8 RID: 16584 RVA: 0x000F8EF4 File Offset: 0x000F70F4
	private void childControlZOrderChanged(dfControl control, int value)
	{
		this.onChildControlInvalidatedLayout();
	}

	// Token: 0x060040C9 RID: 16585 RVA: 0x000F8EFC File Offset: 0x000F70FC
	private void control_IsVisibleChanged(dfControl control, bool value)
	{
		this.onChildControlInvalidatedLayout();
	}

	// Token: 0x060040CA RID: 16586 RVA: 0x000F8F04 File Offset: 0x000F7104
	private void childControlInvalidated(dfControl control, Vector2 value)
	{
		this.onChildControlInvalidatedLayout();
	}

	// Token: 0x060040CB RID: 16587 RVA: 0x000F8F0C File Offset: 0x000F710C
	private void onChildControlInvalidatedLayout()
	{
		if (base.IsLayoutSuspended)
		{
			return;
		}
		this.arrangeTabs();
		this.Invalidate();
	}

	// Token: 0x04002247 RID: 8775
	[SerializeField]
	protected dfAtlas atlas;

	// Token: 0x04002248 RID: 8776
	[SerializeField]
	protected string backgroundSprite;

	// Token: 0x04002249 RID: 8777
	[SerializeField]
	protected RectOffset layoutPadding = new RectOffset();

	// Token: 0x0400224A RID: 8778
	[SerializeField]
	protected Vector2 scrollPosition = Vector2.zero;

	// Token: 0x0400224B RID: 8779
	[SerializeField]
	protected int selectedIndex;

	// Token: 0x0400224C RID: 8780
	[SerializeField]
	protected dfTabContainer pageContainer;

	// Token: 0x0400224D RID: 8781
	[SerializeField]
	protected bool allowKeyboardNavigation = true;
}
