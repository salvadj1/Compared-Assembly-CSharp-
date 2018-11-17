using System;
using UnityEngine;

// Token: 0x020005B8 RID: 1464
public sealed class GameFullscreen : PostEffectsBase
{
	// Token: 0x170009F9 RID: 2553
	// (get) Token: 0x06002EF5 RID: 12021 RVA: 0x000B5040 File Offset: 0x000B3240
	// (set) Token: 0x06002EF6 RID: 12022 RVA: 0x000B5048 File Offset: 0x000B3248
	public Color autoFadeColor
	{
		get
		{
			return this.fadeColor;
		}
		set
		{
			this.fadeColor.r = value.r;
			this.fadeColor.g = value.g;
			this.fadeColor.b = value.b;
			if (value.r == value.g && value.r == value.b)
			{
				this.tintColor.r = (this.tintColor.g = (this.tintColor.b = 1f));
			}
			else
			{
				float num = Mathf.Atan2(1.73205078f * (value.g - value.b), 2f * value.r - value.g - value.b) * 57.29578f;
				if (float.IsNaN(num) || float.IsInfinity(num))
				{
					this.tintColor.r = (this.tintColor.g = (this.tintColor.b = 1f));
				}
				else
				{
					float num2 = ((num >= 0f) ? num : (num + 360f)) / 60f;
					float num3 = 1f * (1f - Mathf.Abs(num2 % 2f - 1f));
					switch (Mathf.FloorToInt(num2) % 6)
					{
					default:
						this.tintColor.r = 1f;
						this.tintColor.g = num3;
						this.tintColor.b = 0f;
						break;
					case 1:
						this.tintColor.r = num3;
						this.tintColor.g = 1f;
						this.tintColor.b = 0f;
						break;
					case 2:
						this.tintColor.r = 0f;
						this.tintColor.g = 1f;
						this.tintColor.b = num3;
						break;
					case 3:
						this.tintColor.r = 0f;
						this.tintColor.g = num3;
						this.tintColor.b = 1f;
						break;
					case 4:
						this.tintColor.r = num3;
						this.tintColor.g = 0f;
						this.tintColor.b = 1f;
						break;
					case 5:
						this.tintColor.r = 1f;
						this.tintColor.g = 0f;
						this.tintColor.b = num3;
						break;
					}
				}
			}
			this.tintColor.a = Mathf.Clamp01(Mathf.SmoothStep(0f, 1f, Mathf.InverseLerp(0f, 0.5f, value.a)));
			this.fadeColor.a = Mathf.Clamp01(Mathf.SmoothStep(0f, 1f, Mathf.InverseLerp(0f, 1f, value.a)));
		}
	}

	// Token: 0x170009FA RID: 2554
	// (get) Token: 0x06002EF7 RID: 12023 RVA: 0x000B5370 File Offset: 0x000B3570
	private bool run
	{
		get
		{
			if (this.fadeColor.a > 0f || this.tintColor.a > 0f)
			{
				return true;
			}
			for (int i = 0; i < this.overlays.Length; i++)
			{
				if (this.overlays[i].willRender)
				{
					return true;
				}
			}
			return false;
		}
	}

	// Token: 0x06002EF8 RID: 12024 RVA: 0x000B53DC File Offset: 0x000B35DC
	protected void OnDisable()
	{
		if (this.material)
		{
			Object.DestroyImmediate(this.material);
		}
	}

	// Token: 0x06002EF9 RID: 12025 RVA: 0x000B53FC File Offset: 0x000B35FC
	public override bool CheckResources()
	{
		this.CheckSupport(false);
		this.material = this.CheckShaderAndCreateMaterial(this.shader, this.material);
		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}
		return this.isSupported;
	}

	// Token: 0x06002EFA RID: 12026 RVA: 0x000B5438 File Offset: 0x000B3638
	protected void OnRenderImage(RenderTexture src, RenderTexture dst)
	{
		if (!this.CheckResources() || !this.run)
		{
			Graphics.Blit(src, dst);
			return;
		}
		if (this.tintColor.a > 0f || this.fadeColor.a > 0f)
		{
			this.material.SetColor("_FadeColor", this.tintColor);
			this.material.SetColor("_SolidColor", this.fadeColor);
			for (int i = 0; i < this.overlays.Length; i++)
			{
				if (this.overlays[i].willRender)
				{
					this.material.SetFloat("_Blend", this.overlays[i].alpha);
					this.material.SetTexture("_OverlayTex", this.overlays[i].texture);
					RenderTexture renderTexture = RenderTexture.GetTemporary(src.width, src.height, 0);
					RenderTexture renderTexture2 = RenderTexture.GetTemporary(src.width, src.height, 0);
					try
					{
						Graphics.Blit(src, renderTexture, this.material, this.overlays[i].pass);
						while (++i < this.overlays.Length)
						{
							if (this.overlays[i].willRender)
							{
								this.material.SetFloat("_Blend", this.overlays[i].alpha);
								this.material.SetTexture("_OverlayTex", this.overlays[i].texture);
								Graphics.Blit(renderTexture, renderTexture2, this.material, this.overlays[i].pass);
								RenderTexture renderTexture3 = renderTexture;
								renderTexture = renderTexture2;
								renderTexture2 = renderTexture3;
							}
						}
						Graphics.Blit(renderTexture, dst, this.material, 0);
					}
					finally
					{
						RenderTexture.ReleaseTemporary(renderTexture);
						RenderTexture.ReleaseTemporary(renderTexture2);
					}
					return;
				}
			}
			Graphics.Blit(src, dst, this.material, 0);
			return;
		}
		for (int j = 0; j < this.overlays.Length; j++)
		{
			if (this.overlays[j].willRender)
			{
				this.material.SetFloat("_Blend", this.overlays[j].alpha);
				this.material.SetTexture("_OverlayTex", this.overlays[j].texture);
				int pass = this.overlays[j].pass;
				while (++j < this.overlays.Length)
				{
					if (this.overlays[j].willRender)
					{
						RenderTexture renderTexture4 = RenderTexture.GetTemporary(src.width, src.height, 0);
						RenderTexture renderTexture5 = RenderTexture.GetTemporary(src.width, src.height, 0);
						try
						{
							Graphics.Blit(src, renderTexture4, this.material, pass);
							this.material.SetFloat("_Blend", this.overlays[j].alpha);
							this.material.SetTexture("_OverlayTex", this.overlays[j].texture);
							pass = this.overlays[j].pass;
							while (++j < this.overlays.Length)
							{
								if (this.overlays[j].willRender)
								{
									Graphics.Blit(renderTexture4, renderTexture5, this.material, pass);
									RenderTexture renderTexture6 = renderTexture4;
									renderTexture4 = renderTexture5;
									renderTexture5 = renderTexture6;
									this.material.SetFloat("_Blend", this.overlays[j].alpha);
									this.material.SetTexture("_OverlayTex", this.overlays[j].texture);
									pass = this.overlays[j].pass;
								}
							}
							Graphics.Blit(renderTexture4, dst, this.material, pass);
						}
						finally
						{
							RenderTexture.ReleaseTemporary(renderTexture4);
							RenderTexture.ReleaseTemporary(renderTexture5);
						}
						return;
					}
				}
				Graphics.Blit(src, dst, this.material, pass);
				return;
			}
		}
	}

	// Token: 0x0400197B RID: 6523
	private const ScaleMode kDefaultScaleMode = 0;

	// Token: 0x0400197C RID: 6524
	private const int kDefaultOverlayPass = 1;

	// Token: 0x0400197D RID: 6525
	private const float sqrtOf3 = 1.73205078f;

	// Token: 0x0400197E RID: 6526
	public Color tintColor = Color.white;

	// Token: 0x0400197F RID: 6527
	public Color fadeColor = new Color(0f, 0f, 0f, 1f);

	// Token: 0x04001980 RID: 6528
	public readonly global::GameFullscreen.Overlay[] overlays = new global::GameFullscreen.Overlay[]
	{
		new global::GameFullscreen.Overlay
		{
			pass = 1
		},
		new global::GameFullscreen.Overlay
		{
			pass = 1
		},
		new global::GameFullscreen.Overlay
		{
			pass = 1
		},
		new global::GameFullscreen.Overlay
		{
			pass = 1
		}
	};

	// Token: 0x04001981 RID: 6529
	public Shader shader;

	// Token: 0x04001982 RID: 6530
	private Material material;

	// Token: 0x020005B9 RID: 1465
	public struct Overlay
	{
		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x06002EFB RID: 12027 RVA: 0x000B58A0 File Offset: 0x000B3AA0
		public bool willRender
		{
			get
			{
				if (this.shouldDraw && !this._texture)
				{
					this.hasTex = false;
					this._texture = null;
					this.shouldDraw = false;
				}
				return this.shouldDraw;
			}
		}

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x06002EFC RID: 12028 RVA: 0x000B58E4 File Offset: 0x000B3AE4
		// (set) Token: 0x06002EFD RID: 12029 RVA: 0x000B58EC File Offset: 0x000B3AEC
		public float alpha
		{
			get
			{
				return this._alpha;
			}
			set
			{
				this._alpha = value;
				bool flag = this.hasAlpha;
				this.hasAlpha = (value > 0f);
				if (flag != this.hasAlpha)
				{
					this.shouldDraw = (this.hasAlpha && this.hasTex);
				}
			}
		}

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x06002EFE RID: 12030 RVA: 0x000B593C File Offset: 0x000B3B3C
		// (set) Token: 0x06002EFF RID: 12031 RVA: 0x000B5944 File Offset: 0x000B3B44
		public Texture2D texture
		{
			get
			{
				return this._texture;
			}
			set
			{
				this._texture = value;
				bool flag = this.hasTex;
				this.hasTex = this._texture;
				if (flag != this.hasTex)
				{
					this.shouldDraw = (this.hasTex && this.hasAlpha);
				}
			}
		}

		// Token: 0x04001983 RID: 6531
		public ScaleMode scaleMode;

		// Token: 0x04001984 RID: 6532
		public int pass;

		// Token: 0x04001985 RID: 6533
		private Texture2D _texture;

		// Token: 0x04001986 RID: 6534
		private float _alpha;

		// Token: 0x04001987 RID: 6535
		private bool hasTex;

		// Token: 0x04001988 RID: 6536
		private bool hasAlpha;

		// Token: 0x04001989 RID: 6537
		private bool shouldDraw;
	}
}
