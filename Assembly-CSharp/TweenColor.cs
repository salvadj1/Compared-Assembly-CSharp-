using System;
using UnityEngine;

// Token: 0x020008A0 RID: 2208
[AddComponentMenu("NGUI/Tween/Color")]
public class TweenColor : global::UITweener
{
	// Token: 0x17000E4D RID: 3661
	// (get) Token: 0x06004BC1 RID: 19393 RVA: 0x00127F58 File Offset: 0x00126158
	// (set) Token: 0x06004BC2 RID: 19394 RVA: 0x00127FC4 File Offset: 0x001261C4
	public Color color
	{
		get
		{
			if (this.mWidget != null)
			{
				return this.mWidget.color;
			}
			if (this.mLight != null)
			{
				return this.mLight.color;
			}
			if (this.mMat != null)
			{
				return this.mMat.color;
			}
			return Color.black;
		}
		set
		{
			if (this.mWidget != null)
			{
				this.mWidget.color = value;
			}
			if (this.mMat != null)
			{
				this.mMat.color = value;
			}
			if (this.mLight != null)
			{
				this.mLight.color = value;
				this.mLight.enabled = (value.r + value.g + value.b > 0.01f);
			}
		}
	}

	// Token: 0x06004BC3 RID: 19395 RVA: 0x00128054 File Offset: 0x00126254
	private void Awake()
	{
		this.mWidget = base.GetComponentInChildren<global::UIWidget>();
		Renderer renderer = base.renderer;
		if (renderer != null)
		{
			this.mMat = renderer.material;
		}
		this.mLight = base.light;
	}

	// Token: 0x06004BC4 RID: 19396 RVA: 0x00128098 File Offset: 0x00126298
	protected override void OnUpdate(float factor)
	{
		Color color = this.from * (1f - factor) + this.to * factor;
		if (this.isFullscreen)
		{
			global::GameFullscreen instance = global::ImageEffectManager.GetInstance<global::GameFullscreen>();
			if (instance)
			{
				instance.autoFadeColor = color;
			}
			color.a = 0f;
		}
		this.color = color;
	}

	// Token: 0x06004BC5 RID: 19397 RVA: 0x00128100 File Offset: 0x00126300
	public static global::TweenColor Begin(GameObject go, float duration, Color color)
	{
		global::TweenColor tweenColor = global::UITweener.Begin<global::TweenColor>(go, duration);
		tweenColor.from = tweenColor.color;
		tweenColor.to = color;
		return tweenColor;
	}

	// Token: 0x04002972 RID: 10610
	public Color from = Color.white;

	// Token: 0x04002973 RID: 10611
	public Color to = Color.white;

	// Token: 0x04002974 RID: 10612
	[NonSerialized]
	public bool isFullscreen;

	// Token: 0x04002975 RID: 10613
	private Transform mTrans;

	// Token: 0x04002976 RID: 10614
	private global::UIWidget mWidget;

	// Token: 0x04002977 RID: 10615
	private Material mMat;

	// Token: 0x04002978 RID: 10616
	private Light mLight;
}
