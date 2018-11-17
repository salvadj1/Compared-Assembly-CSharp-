using System;
using UnityEngine;

// Token: 0x020007AA RID: 1962
[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider))]
[Serializable]
public class dfInteractiveBase : global::dfControl
{
	// Token: 0x17000C98 RID: 3224
	// (get) Token: 0x06004201 RID: 16897 RVA: 0x000F3194 File Offset: 0x000F1394
	// (set) Token: 0x06004202 RID: 16898 RVA: 0x000F31DC File Offset: 0x000F13DC
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

	// Token: 0x17000C99 RID: 3225
	// (get) Token: 0x06004203 RID: 16899 RVA: 0x000F31FC File Offset: 0x000F13FC
	// (set) Token: 0x06004204 RID: 16900 RVA: 0x000F3204 File Offset: 0x000F1404
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

	// Token: 0x17000C9A RID: 3226
	// (get) Token: 0x06004205 RID: 16901 RVA: 0x000F322C File Offset: 0x000F142C
	// (set) Token: 0x06004206 RID: 16902 RVA: 0x000F3234 File Offset: 0x000F1434
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

	// Token: 0x17000C9B RID: 3227
	// (get) Token: 0x06004207 RID: 16903 RVA: 0x000F3254 File Offset: 0x000F1454
	// (set) Token: 0x06004208 RID: 16904 RVA: 0x000F325C File Offset: 0x000F145C
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

	// Token: 0x17000C9C RID: 3228
	// (get) Token: 0x06004209 RID: 16905 RVA: 0x000F327C File Offset: 0x000F147C
	// (set) Token: 0x0600420A RID: 16906 RVA: 0x000F3284 File Offset: 0x000F1484
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

	// Token: 0x17000C9D RID: 3229
	// (get) Token: 0x0600420B RID: 16907 RVA: 0x000F32A4 File Offset: 0x000F14A4
	public override bool CanFocus
	{
		get
		{
			return (base.IsEnabled && base.IsVisible) || base.CanFocus;
		}
	}

	// Token: 0x0600420C RID: 16908 RVA: 0x000F32C4 File Offset: 0x000F14C4
	protected internal override void OnGotFocus(global::dfFocusEventArgs args)
	{
		base.OnGotFocus(args);
		this.Invalidate();
	}

	// Token: 0x0600420D RID: 16909 RVA: 0x000F32D4 File Offset: 0x000F14D4
	protected internal override void OnLostFocus(global::dfFocusEventArgs args)
	{
		base.OnLostFocus(args);
		this.Invalidate();
	}

	// Token: 0x0600420E RID: 16910 RVA: 0x000F32E4 File Offset: 0x000F14E4
	protected internal override void OnMouseEnter(global::dfMouseEventArgs args)
	{
		base.OnMouseEnter(args);
		this.Invalidate();
	}

	// Token: 0x0600420F RID: 16911 RVA: 0x000F32F4 File Offset: 0x000F14F4
	protected internal override void OnMouseLeave(global::dfMouseEventArgs args)
	{
		base.OnMouseLeave(args);
		this.Invalidate();
	}

	// Token: 0x06004210 RID: 16912 RVA: 0x000F3304 File Offset: 0x000F1504
	public override Vector2 CalculateMinimumSize()
	{
		global::dfAtlas.ItemInfo itemInfo = this.getBackgroundSprite();
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

	// Token: 0x06004211 RID: 16913 RVA: 0x000F3370 File Offset: 0x000F1570
	protected internal virtual void renderBackground()
	{
		if (this.Atlas == null)
		{
			return;
		}
		global::dfAtlas.ItemInfo itemInfo = this.getBackgroundSprite();
		if (itemInfo == null)
		{
			return;
		}
		Color32 color = base.ApplyOpacity(this.getActiveColor());
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

	// Token: 0x06004212 RID: 16914 RVA: 0x000F3454 File Offset: 0x000F1654
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

	// Token: 0x06004213 RID: 16915 RVA: 0x000F34C0 File Offset: 0x000F16C0
	protected internal virtual global::dfAtlas.ItemInfo getBackgroundSprite()
	{
		if (this.Atlas == null)
		{
			return null;
		}
		if (!base.IsEnabled)
		{
			global::dfAtlas.ItemInfo itemInfo = this.atlas[this.DisabledSprite];
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
					global::dfAtlas.ItemInfo itemInfo2 = this.atlas[this.HoverSprite];
					if (itemInfo2 != null)
					{
						return itemInfo2;
					}
				}
				return this.Atlas[this.BackgroundSprite];
			}
			global::dfAtlas.ItemInfo itemInfo3 = this.atlas[this.FocusSprite];
			if (itemInfo3 != null)
			{
				return itemInfo3;
			}
			return this.atlas[this.BackgroundSprite];
		}
	}

	// Token: 0x06004214 RID: 16916 RVA: 0x000F3598 File Offset: 0x000F1798
	private void setDefaultSize(string spriteName)
	{
		if (this.Atlas == null)
		{
			return;
		}
		global::dfAtlas.ItemInfo itemInfo = this.Atlas[spriteName];
		if (this.size == Vector2.zero && itemInfo != null)
		{
			base.Size = itemInfo.sizeInPixels;
		}
	}

	// Token: 0x040022A8 RID: 8872
	[SerializeField]
	protected global::dfAtlas atlas;

	// Token: 0x040022A9 RID: 8873
	[SerializeField]
	protected string backgroundSprite;

	// Token: 0x040022AA RID: 8874
	[SerializeField]
	protected string hoverSprite;

	// Token: 0x040022AB RID: 8875
	[SerializeField]
	protected string disabledSprite;

	// Token: 0x040022AC RID: 8876
	[SerializeField]
	protected string focusSprite;
}
