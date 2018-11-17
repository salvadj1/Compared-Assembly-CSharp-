using System;
using UnityEngine;

// Token: 0x020006D8 RID: 1752
[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider))]
[Serializable]
public class dfInteractiveBase : dfControl
{
	// Token: 0x17000C14 RID: 3092
	// (get) Token: 0x06003DE5 RID: 15845 RVA: 0x000EA590 File Offset: 0x000E8790
	// (set) Token: 0x06003DE6 RID: 15846 RVA: 0x000EA5D8 File Offset: 0x000E87D8
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

	// Token: 0x17000C15 RID: 3093
	// (get) Token: 0x06003DE7 RID: 15847 RVA: 0x000EA5F8 File Offset: 0x000E87F8
	// (set) Token: 0x06003DE8 RID: 15848 RVA: 0x000EA600 File Offset: 0x000E8800
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
				this.setDefaultSize(value);
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000C16 RID: 3094
	// (get) Token: 0x06003DE9 RID: 15849 RVA: 0x000EA628 File Offset: 0x000E8828
	// (set) Token: 0x06003DEA RID: 15850 RVA: 0x000EA630 File Offset: 0x000E8830
	public string DisabledSprite
	{
		get
		{
			return this.disabledSprite;
		}
		set
		{
			if (value != this.disabledSprite)
			{
				this.disabledSprite = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000C17 RID: 3095
	// (get) Token: 0x06003DEB RID: 15851 RVA: 0x000EA650 File Offset: 0x000E8850
	// (set) Token: 0x06003DEC RID: 15852 RVA: 0x000EA658 File Offset: 0x000E8858
	public string FocusSprite
	{
		get
		{
			return this.focusSprite;
		}
		set
		{
			if (value != this.focusSprite)
			{
				this.focusSprite = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000C18 RID: 3096
	// (get) Token: 0x06003DED RID: 15853 RVA: 0x000EA678 File Offset: 0x000E8878
	// (set) Token: 0x06003DEE RID: 15854 RVA: 0x000EA680 File Offset: 0x000E8880
	public string HoverSprite
	{
		get
		{
			return this.hoverSprite;
		}
		set
		{
			if (value != this.hoverSprite)
			{
				this.hoverSprite = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000C19 RID: 3097
	// (get) Token: 0x06003DEF RID: 15855 RVA: 0x000EA6A0 File Offset: 0x000E88A0
	public override bool CanFocus
	{
		get
		{
			return (base.IsEnabled && base.IsVisible) || base.CanFocus;
		}
	}

	// Token: 0x06003DF0 RID: 15856 RVA: 0x000EA6C0 File Offset: 0x000E88C0
	protected internal override void OnGotFocus(dfFocusEventArgs args)
	{
		base.OnGotFocus(args);
		this.Invalidate();
	}

	// Token: 0x06003DF1 RID: 15857 RVA: 0x000EA6D0 File Offset: 0x000E88D0
	protected internal override void OnLostFocus(dfFocusEventArgs args)
	{
		base.OnLostFocus(args);
		this.Invalidate();
	}

	// Token: 0x06003DF2 RID: 15858 RVA: 0x000EA6E0 File Offset: 0x000E88E0
	protected internal override void OnMouseEnter(dfMouseEventArgs args)
	{
		base.OnMouseEnter(args);
		this.Invalidate();
	}

	// Token: 0x06003DF3 RID: 15859 RVA: 0x000EA6F0 File Offset: 0x000E88F0
	protected internal override void OnMouseLeave(dfMouseEventArgs args)
	{
		base.OnMouseLeave(args);
		this.Invalidate();
	}

	// Token: 0x06003DF4 RID: 15860 RVA: 0x000EA700 File Offset: 0x000E8900
	public override Vector2 CalculateMinimumSize()
	{
		dfAtlas.ItemInfo itemInfo = this.getBackgroundSprite();
		if (itemInfo == null)
		{
			return base.CalculateMinimumSize();
		}
		RectOffset border = itemInfo.border;
		if (border.horizontal > 0 || border.vertical > 0)
		{
			return Vector2.Max(base.CalculateMinimumSize(), new Vector2((float)border.horizontal, (float)border.vertical));
		}
		return base.CalculateMinimumSize();
	}

	// Token: 0x06003DF5 RID: 15861 RVA: 0x000EA76C File Offset: 0x000E896C
	protected internal virtual void renderBackground()
	{
		if (this.Atlas == null)
		{
			return;
		}
		dfAtlas.ItemInfo itemInfo = this.getBackgroundSprite();
		if (itemInfo == null)
		{
			return;
		}
		Color32 color = base.ApplyOpacity(this.getActiveColor());
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

	// Token: 0x06003DF6 RID: 15862 RVA: 0x000EA850 File Offset: 0x000E8A50
	protected virtual Color32 getActiveColor()
	{
		if (base.IsEnabled)
		{
			return this.color;
		}
		if (!string.IsNullOrEmpty(this.disabledSprite) && this.Atlas != null && this.Atlas[this.DisabledSprite] != null)
		{
			return this.color;
		}
		return this.disabledColor;
	}

	// Token: 0x06003DF7 RID: 15863 RVA: 0x000EA8BC File Offset: 0x000E8ABC
	protected internal virtual dfAtlas.ItemInfo getBackgroundSprite()
	{
		if (this.Atlas == null)
		{
			return null;
		}
		if (!base.IsEnabled)
		{
			dfAtlas.ItemInfo itemInfo = this.atlas[this.DisabledSprite];
			if (itemInfo != null)
			{
				return itemInfo;
			}
			return this.atlas[this.BackgroundSprite];
		}
		else
		{
			if (!this.HasFocus)
			{
				if (this.isMouseHovering)
				{
					dfAtlas.ItemInfo itemInfo2 = this.atlas[this.HoverSprite];
					if (itemInfo2 != null)
					{
						return itemInfo2;
					}
				}
				return this.Atlas[this.BackgroundSprite];
			}
			dfAtlas.ItemInfo itemInfo3 = this.atlas[this.FocusSprite];
			if (itemInfo3 != null)
			{
				return itemInfo3;
			}
			return this.atlas[this.BackgroundSprite];
		}
	}

	// Token: 0x06003DF8 RID: 15864 RVA: 0x000EA994 File Offset: 0x000E8B94
	private void setDefaultSize(string spriteName)
	{
		if (this.Atlas == null)
		{
			return;
		}
		dfAtlas.ItemInfo itemInfo = this.Atlas[spriteName];
		if (this.size == Vector2.zero && itemInfo != null)
		{
			base.Size = itemInfo.sizeInPixels;
		}
	}

	// Token: 0x0400209F RID: 8351
	[SerializeField]
	protected dfAtlas atlas;

	// Token: 0x040020A0 RID: 8352
	[SerializeField]
	protected string backgroundSprite;

	// Token: 0x040020A1 RID: 8353
	[SerializeField]
	protected string hoverSprite;

	// Token: 0x040020A2 RID: 8354
	[SerializeField]
	protected string disabledSprite;

	// Token: 0x040020A3 RID: 8355
	[SerializeField]
	protected string focusSprite;
}
