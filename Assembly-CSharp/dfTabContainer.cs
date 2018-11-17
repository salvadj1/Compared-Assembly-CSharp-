using System;
using UnityEngine;

// Token: 0x020006FC RID: 1788
[AddComponentMenu("Daikon Forge/User Interface/Containers/Tab Control/Tab Page Container")]
[ExecuteInEditMode]
[Serializable]
public class dfTabContainer : dfControl
{
	// Token: 0x14000053 RID: 83
	// (add) Token: 0x0600408E RID: 16526 RVA: 0x000F7E70 File Offset: 0x000F6070
	// (remove) Token: 0x0600408F RID: 16527 RVA: 0x000F7E8C File Offset: 0x000F608C
	public event PropertyChangedEventHandler<int> SelectedIndexChanged;

	// Token: 0x17000CB3 RID: 3251
	// (get) Token: 0x06004090 RID: 16528 RVA: 0x000F7EA8 File Offset: 0x000F60A8
	// (set) Token: 0x06004091 RID: 16529 RVA: 0x000F7EF0 File Offset: 0x000F60F0
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

	// Token: 0x17000CB4 RID: 3252
	// (get) Token: 0x06004092 RID: 16530 RVA: 0x000F7F10 File Offset: 0x000F6110
	// (set) Token: 0x06004093 RID: 16531 RVA: 0x000F7F18 File Offset: 0x000F6118
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

	// Token: 0x17000CB5 RID: 3253
	// (get) Token: 0x06004094 RID: 16532 RVA: 0x000F7F38 File Offset: 0x000F6138
	// (set) Token: 0x06004095 RID: 16533 RVA: 0x000F7F58 File Offset: 0x000F6158
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

	// Token: 0x17000CB6 RID: 3254
	// (get) Token: 0x06004096 RID: 16534 RVA: 0x000F7F8C File Offset: 0x000F618C
	// (set) Token: 0x06004097 RID: 16535 RVA: 0x000F7F94 File Offset: 0x000F6194
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

	// Token: 0x06004098 RID: 16536 RVA: 0x000F7FAC File Offset: 0x000F61AC
	public dfControl AddTabPage()
	{
		dfPanel dfPanel = (from i in this.controls
		where i is dfPanel
		select i).FirstOrDefault() as dfPanel;
		string name = "Tab Page " + (this.controls.Count + 1);
		dfPanel dfPanel2 = base.AddControl<dfPanel>();
		dfPanel2.name = name;
		dfPanel2.Atlas = this.Atlas;
		dfPanel2.Anchor = dfAnchorStyle.All;
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

	// Token: 0x06004099 RID: 16537 RVA: 0x000F8064 File Offset: 0x000F6264
	public override void OnEnable()
	{
		base.OnEnable();
		if (this.size.sqrMagnitude < 1.401298E-45f)
		{
			base.Size = new Vector2(256f, 256f);
		}
	}

	// Token: 0x0600409A RID: 16538 RVA: 0x000F80A4 File Offset: 0x000F62A4
	protected internal override void OnControlAdded(dfControl child)
	{
		base.OnControlAdded(child);
		this.attachEvents(child);
		this.arrangeTabPages();
	}

	// Token: 0x0600409B RID: 16539 RVA: 0x000F80BC File Offset: 0x000F62BC
	protected internal override void OnControlRemoved(dfControl child)
	{
		base.OnControlRemoved(child);
		this.detachEvents(child);
		this.arrangeTabPages();
	}

	// Token: 0x0600409C RID: 16540 RVA: 0x000F80D4 File Offset: 0x000F62D4
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

	// Token: 0x0600409D RID: 16541 RVA: 0x000F810C File Offset: 0x000F630C
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

	// Token: 0x0600409E RID: 16542 RVA: 0x000F8238 File Offset: 0x000F6438
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
			dfControl dfControl = this.controls[i];
			if (!(dfControl == null))
			{
				dfControl.IsVisible = (i == value);
			}
		}
		this.arrangeTabPages();
		this.Invalidate();
		this.OnSelectedIndexChanged(value);
	}

	// Token: 0x0600409F RID: 16543 RVA: 0x000F82CC File Offset: 0x000F64CC
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
			dfPanel dfPanel = this.controls[i] as dfPanel;
			if (dfPanel != null)
			{
				dfPanel.Size = size;
				dfPanel.RelativePosition = relativePosition;
			}
		}
	}

	// Token: 0x060040A0 RID: 16544 RVA: 0x000F8394 File Offset: 0x000F6594
	private void attachEvents(dfControl control)
	{
		control.IsVisibleChanged += this.control_IsVisibleChanged;
		control.PositionChanged += this.childControlInvalidated;
		control.SizeChanged += this.childControlInvalidated;
	}

	// Token: 0x060040A1 RID: 16545 RVA: 0x000F83D8 File Offset: 0x000F65D8
	private void detachEvents(dfControl control)
	{
		control.IsVisibleChanged -= this.control_IsVisibleChanged;
		control.PositionChanged -= this.childControlInvalidated;
		control.SizeChanged -= this.childControlInvalidated;
	}

	// Token: 0x060040A2 RID: 16546 RVA: 0x000F841C File Offset: 0x000F661C
	private void control_IsVisibleChanged(dfControl control, bool value)
	{
		this.onChildControlInvalidatedLayout();
	}

	// Token: 0x060040A3 RID: 16547 RVA: 0x000F8424 File Offset: 0x000F6624
	private void childControlInvalidated(dfControl control, Vector2 value)
	{
		this.onChildControlInvalidatedLayout();
	}

	// Token: 0x060040A4 RID: 16548 RVA: 0x000F842C File Offset: 0x000F662C
	private void onChildControlInvalidatedLayout()
	{
		if (base.IsLayoutSuspended)
		{
			return;
		}
		this.arrangeTabPages();
		this.Invalidate();
	}

	// Token: 0x04002241 RID: 8769
	[SerializeField]
	protected dfAtlas atlas;

	// Token: 0x04002242 RID: 8770
	[SerializeField]
	protected string backgroundSprite;

	// Token: 0x04002243 RID: 8771
	[SerializeField]
	protected RectOffset padding = new RectOffset();

	// Token: 0x04002244 RID: 8772
	[SerializeField]
	protected int selectedIndex;
}
