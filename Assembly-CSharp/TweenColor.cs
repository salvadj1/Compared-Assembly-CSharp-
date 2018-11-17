using System;
using UnityEngine;

// Token: 0x020007B3 RID: 1971
[AddComponentMenu("NGUI/Tween/Color")]
public class TweenColor : UITweener
{
	// Token: 0x17000DBB RID: 3515
	// (get) Token: 0x06004732 RID: 18226 RVA: 0x0011E534 File Offset: 0x0011C734
	// (set) Token: 0x06004733 RID: 18227 RVA: 0x0011E5A0 File Offset: 0x0011C7A0
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

	// Token: 0x06004734 RID: 18228 RVA: 0x0011E630 File Offset: 0x0011C830
	private void Awake()
	{
		this.mWidget = base.GetComponentInChildren<UIWidget>();
		Renderer renderer = base.renderer;
		if (renderer != null)
		{
			this.mMat = renderer.material;
		}
		this.mLight = base.light;
	}

	// Token: 0x06004735 RID: 18229 RVA: 0x0011E674 File Offset: 0x0011C874
	protected override void OnUpdate(float factor)
	{
		Color color = this.from * (1f - factor) + this.to * factor;
		if (this.isFullscreen)
		{
			GameFullscreen instance = ImageEffectManager.GetInstance<GameFullscreen>();
			if (instance)
			{
				instance.autoFadeColor = color;
			}
			color.a = 0f;
		}
		this.color = color;
	}

	// Token: 0x06004736 RID: 18230 RVA: 0x0011E6DC File Offset: 0x0011C8DC
	public static TweenColor Begin(GameObject go, float duration, Color color)
	{
		TweenColor tweenColor = UITweener.Begin<TweenColor>(go, duration);
		tweenColor.from = tweenColor.color;
		tweenColor.to = color;
		return tweenColor;
	}

	// Token: 0x04002738 RID: 10040
	public Color from = Color.white;

	// Token: 0x04002739 RID: 10041
	public Color to = Color.white;

	// Token: 0x0400273A RID: 10042
	[NonSerialized]
	public bool isFullscreen;

	// Token: 0x0400273B RID: 10043
	private Transform mTrans;

	// Token: 0x0400273C RID: 10044
	private UIWidget mWidget;

	// Token: 0x0400273D RID: 10045
	private Material mMat;

	// Token: 0x0400273E RID: 10046
	private Light mLight;
}
