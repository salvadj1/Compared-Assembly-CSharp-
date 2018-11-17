using System;
using UnityEngine;

// Token: 0x0200075C RID: 1884
[AddComponentMenu("NGUI/Interaction/Button Color")]
public class UIButtonColor : MonoBehaviour
{
	// Token: 0x17000D6E RID: 3438
	// (get) Token: 0x060044B8 RID: 17592 RVA: 0x0010D220 File Offset: 0x0010B420
	// (set) Token: 0x060044B9 RID: 17593 RVA: 0x0010D228 File Offset: 0x0010B428
	public Color defaultColor
	{
		get
		{
			return this.mColor;
		}
		set
		{
			this.mColor = value;
		}
	}

	// Token: 0x060044BA RID: 17594 RVA: 0x0010D234 File Offset: 0x0010B434
	private void Start()
	{
		this.mStarted = true;
		if (!this.mInitDone)
		{
			this.Init();
		}
	}

	// Token: 0x060044BB RID: 17595 RVA: 0x0010D250 File Offset: 0x0010B450
	protected virtual void OnEnable()
	{
		if (this.mStarted && this.mHighlighted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x060044BC RID: 17596 RVA: 0x0010D27C File Offset: 0x0010B47C
	private void OnDisable()
	{
		if (this.tweenTarget != null)
		{
			TweenColor component = this.tweenTarget.GetComponent<TweenColor>();
			if (component != null)
			{
				component.color = this.mColor;
				component.enabled = false;
			}
		}
	}

	// Token: 0x060044BD RID: 17597 RVA: 0x0010D2C8 File Offset: 0x0010B4C8
	protected void Init()
	{
		this.mInitDone = true;
		if (this.tweenTarget == null)
		{
			this.tweenTarget = base.gameObject;
		}
		UIWidget component = this.tweenTarget.GetComponent<UIWidget>();
		if (component != null)
		{
			this.mColor = component.color;
		}
		else
		{
			Renderer renderer = this.tweenTarget.renderer;
			if (renderer != null)
			{
				this.mColor = renderer.material.color;
			}
			else
			{
				Light light = this.tweenTarget.light;
				if (light != null)
				{
					this.mColor = light.color;
				}
				else
				{
					Debug.LogWarning(NGUITools.GetHierarchy(base.gameObject) + " has nothing for UIButtonColor to color", this);
					base.enabled = false;
				}
			}
		}
	}

	// Token: 0x060044BE RID: 17598 RVA: 0x0010D39C File Offset: 0x0010B59C
	protected virtual void OnPress(bool isPressed)
	{
		if (!this.mInitDone)
		{
			this.Init();
		}
		if (base.enabled)
		{
			TweenColor.Begin(this.tweenTarget, this.duration, (!isPressed) ? ((!UICamera.IsHighlighted(base.gameObject)) ? this.mColor : this.hover) : this.pressed);
		}
	}

	// Token: 0x060044BF RID: 17599 RVA: 0x0010D40C File Offset: 0x0010B60C
	protected virtual void OnHover(bool isOver)
	{
		if (base.enabled)
		{
			if (!this.mInitDone)
			{
				this.Init();
			}
			TweenColor.Begin(this.tweenTarget, this.duration, (!isOver) ? this.mColor : this.hover);
			this.mHighlighted = isOver;
		}
	}

	// Token: 0x040024E8 RID: 9448
	public GameObject tweenTarget;

	// Token: 0x040024E9 RID: 9449
	public Color hover = new Color(0.6f, 1f, 0.2f, 1f);

	// Token: 0x040024EA RID: 9450
	public Color pressed = Color.grey;

	// Token: 0x040024EB RID: 9451
	public float duration = 0.2f;

	// Token: 0x040024EC RID: 9452
	protected Color mColor;

	// Token: 0x040024ED RID: 9453
	protected bool mInitDone;

	// Token: 0x040024EE RID: 9454
	protected bool mStarted;

	// Token: 0x040024EF RID: 9455
	protected bool mHighlighted;
}
