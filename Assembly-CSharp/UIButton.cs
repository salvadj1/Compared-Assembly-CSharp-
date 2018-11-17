using System;
using UnityEngine;

// Token: 0x0200075B RID: 1883
[AddComponentMenu("NGUI/Interaction/Button")]
public class UIButton : UIButtonColor
{
	// Token: 0x060044B1 RID: 17585 RVA: 0x0010D0F0 File Offset: 0x0010B2F0
	protected override void OnEnable()
	{
		if (this.isEnabled)
		{
			base.OnEnable();
		}
		else
		{
			this.UpdateColor(false, true);
		}
	}

	// Token: 0x060044B2 RID: 17586 RVA: 0x0010D110 File Offset: 0x0010B310
	protected override void OnHover(bool isOver)
	{
		if (this.isEnabled)
		{
			base.OnHover(isOver);
		}
	}

	// Token: 0x060044B3 RID: 17587 RVA: 0x0010D124 File Offset: 0x0010B324
	protected override void OnPress(bool isPressed)
	{
		if (this.isEnabled)
		{
			base.OnPress(isPressed);
		}
	}

	// Token: 0x17000D6D RID: 3437
	// (get) Token: 0x060044B4 RID: 17588 RVA: 0x0010D138 File Offset: 0x0010B338
	// (set) Token: 0x060044B5 RID: 17589 RVA: 0x0010D140 File Offset: 0x0010B340
	public bool isEnabled
	{
		get
		{
			return NGUITools.GetAllowClick(this);
		}
		set
		{
			bool flag;
			bool allowClick = NGUITools.GetAllowClick(this, out flag);
			if (!flag)
			{
				return;
			}
			if (allowClick != value)
			{
				NGUITools.SetAllowClick(this, value);
				this.UpdateColor(value, false);
			}
		}
	}

	// Token: 0x060044B6 RID: 17590 RVA: 0x0010D174 File Offset: 0x0010B374
	public void UpdateColor(bool shouldBeEnabled, bool immediate)
	{
		if (this.tweenTarget)
		{
			if (!this.mInitDone)
			{
				base.Init();
			}
			Color color = (!shouldBeEnabled) ? this.disabledColor : base.defaultColor;
			TweenColor tweenColor = TweenColor.Begin(this.tweenTarget, 0.15f, color);
			if (immediate)
			{
				tweenColor.color = color;
				tweenColor.enabled = false;
			}
		}
	}

	// Token: 0x040024E7 RID: 9447
	public Color disabledColor = Color.grey;
}
