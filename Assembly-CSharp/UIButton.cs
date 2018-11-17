using System;
using UnityEngine;

// Token: 0x0200083D RID: 2109
[AddComponentMenu("NGUI/Interaction/Button")]
public class UIButton : global::UIButtonColor
{
	// Token: 0x06004912 RID: 18706 RVA: 0x00116A70 File Offset: 0x00114C70
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

	// Token: 0x06004913 RID: 18707 RVA: 0x00116A90 File Offset: 0x00114C90
	protected override void OnHover(bool isOver)
	{
		if (this.isEnabled)
		{
			base.OnHover(isOver);
		}
	}

	// Token: 0x06004914 RID: 18708 RVA: 0x00116AA4 File Offset: 0x00114CA4
	protected override void OnPress(bool isPressed)
	{
		if (this.isEnabled)
		{
			base.OnPress(isPressed);
		}
	}

	// Token: 0x17000DFD RID: 3581
	// (get) Token: 0x06004915 RID: 18709 RVA: 0x00116AB8 File Offset: 0x00114CB8
	// (set) Token: 0x06004916 RID: 18710 RVA: 0x00116AC0 File Offset: 0x00114CC0
	public bool isEnabled
	{
		get
		{
			return global::NGUITools.GetAllowClick(this);
		}
		set
		{
			bool flag;
			bool allowClick = global::NGUITools.GetAllowClick(this, out flag);
			if (!flag)
			{
				return;
			}
			if (allowClick != value)
			{
				global::NGUITools.SetAllowClick(this, value);
				this.UpdateColor(value, false);
			}
		}
	}

	// Token: 0x06004917 RID: 18711 RVA: 0x00116AF4 File Offset: 0x00114CF4
	public void UpdateColor(bool shouldBeEnabled, bool immediate)
	{
		if (this.tweenTarget)
		{
			if (!this.mInitDone)
			{
				base.Init();
			}
			Color color = (!shouldBeEnabled) ? this.disabledColor : base.defaultColor;
			global::TweenColor tweenColor = global::TweenColor.Begin(this.tweenTarget, 0.15f, color);
			if (immediate)
			{
				tweenColor.color = color;
				tweenColor.enabled = false;
			}
		}
	}

	// Token: 0x0400271E RID: 10014
	public Color disabledColor = Color.grey;
}
