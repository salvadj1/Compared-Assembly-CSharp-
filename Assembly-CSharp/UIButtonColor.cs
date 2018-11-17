using System;
using UnityEngine;

// Token: 0x0200083E RID: 2110
[AddComponentMenu("NGUI/Interaction/Button Color")]
public class UIButtonColor : MonoBehaviour
{
	// Token: 0x17000DFE RID: 3582
	// (get) Token: 0x06004919 RID: 18713 RVA: 0x00116BA0 File Offset: 0x00114DA0
	// (set) Token: 0x0600491A RID: 18714 RVA: 0x00116BA8 File Offset: 0x00114DA8
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

	// Token: 0x0600491B RID: 18715 RVA: 0x00116BB4 File Offset: 0x00114DB4
	private void Start()
	{
		this.mStarted = true;
		if (!this.mInitDone)
		{
			this.Init();
		}
	}

	// Token: 0x0600491C RID: 18716 RVA: 0x00116BD0 File Offset: 0x00114DD0
	protected virtual void OnEnable()
	{
		if (this.mStarted && this.mHighlighted)
		{
			this.OnHover(global::UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x0600491D RID: 18717 RVA: 0x00116BFC File Offset: 0x00114DFC
	private void OnDisable()
	{
		if (this.tweenTarget != null)
		{
			global::TweenColor component = this.tweenTarget.GetComponent<global::TweenColor>();
			if (component != null)
			{
				component.color = this.mColor;
				component.enabled = false;
			}
		}
	}

	// Token: 0x0600491E RID: 18718 RVA: 0x00116C48 File Offset: 0x00114E48
	protected void Init()
	{
		this.mInitDone = true;
		if (this.tweenTarget == null)
		{
			this.tweenTarget = base.gameObject;
		}
		global::UIWidget component = this.tweenTarget.GetComponent<global::UIWidget>();
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
					Debug.LogWarning(global::NGUITools.GetHierarchy(base.gameObject) + " has nothing for UIButtonColor to color", this);
					base.enabled = false;
				}
			}
		}
	}

	// Token: 0x0600491F RID: 18719 RVA: 0x00116D1C File Offset: 0x00114F1C
	protected virtual void OnPress(bool isPressed)
	{
		if (!this.mInitDone)
		{
			this.Init();
		}
		if (base.enabled)
		{
			global::TweenColor.Begin(this.tweenTarget, this.duration, (!isPressed) ? ((!global::UICamera.IsHighlighted(base.gameObject)) ? this.mColor : this.hover) : this.pressed);
		}
	}

	// Token: 0x06004920 RID: 18720 RVA: 0x00116D8C File Offset: 0x00114F8C
	protected virtual void OnHover(bool isOver)
	{
		if (base.enabled)
		{
			if (!this.mInitDone)
			{
				this.Init();
			}
			global::TweenColor.Begin(this.tweenTarget, this.duration, (!isOver) ? this.mColor : this.hover);
			this.mHighlighted = isOver;
		}
	}

	// Token: 0x0400271F RID: 10015
	public GameObject tweenTarget;

	// Token: 0x04002720 RID: 10016
	public Color hover = new Color(0.6f, 1f, 0.2f, 1f);

	// Token: 0x04002721 RID: 10017
	public Color pressed = Color.grey;

	// Token: 0x04002722 RID: 10018
	public float duration = 0.2f;

	// Token: 0x04002723 RID: 10019
	protected Color mColor;

	// Token: 0x04002724 RID: 10020
	protected bool mInitDone;

	// Token: 0x04002725 RID: 10021
	protected bool mStarted;

	// Token: 0x04002726 RID: 10022
	protected bool mHighlighted;
}
