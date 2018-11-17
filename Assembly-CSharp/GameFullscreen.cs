using System;
using UnityEngine;

// Token: 0x020004FB RID: 1275
public sealed class GameFullscreen : PostEffectsBase
{
	// Token: 0x17000985 RID: 2437
	// (get) Token: 0x06002B35 RID: 11061 RVA: 0x000ACFA4 File Offset: 0x000AB1A4
	// (set) Token: 0x06002B36 RID: 11062 RVA: 0x000ACFAC File Offset: 0x000AB1AC
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

	// Token: 0x17000986 RID: 2438
	// (get) Token: 0x06002B37 RID: 11063 RVA: 0x000AD2D4 File Offset: 0x000AB4D4
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

	// Token: 0x06002B38 RID: 11064 RVA: 0x000AD340 File Offset: 0x000AB540
	protected void OnDisable()
	{
		if (this.material)
		{
			Object.DestroyImmediate(this.material);
		}
	}

	// Token: 0x06002B39 RID: 11065 RVA: 0x000AD360 File Offset: 0x000AB560
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

	// Token: 0x06002B3A RID: 11066 RVA: 0x000AD39C File Offset: 0x000AB59C
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

	// Token: 0x040017AF RID: 6063
	private const ScaleMode kDefaultScaleMode = 0;

	// Token: 0x040017B0 RID: 6064
	private const int kDefaultOverlayPass = 1;

	// Token: 0x040017B1 RID: 6065
	private const float sqrtOf3 = 1.73205078f;

	// Token: 0x040017B2 RID: 6066
	public Color tintColor = Color.white;

	// Token: 0x040017B3 RID: 6067
	public Color fadeColor = new Color(0f, 0f, 0f, 1f);

	// Token: 0x040017B4 RID: 6068
	public readonly GameFullscreen.Overlay[] overlays = new GameFullscreen.Overlay[]
	{
		new GameFullscreen.Overlay
		{
			pass = 1
		},
		new GameFullscreen.Overlay
		{
			pass = 1
		},
		new GameFullscreen.Overlay
		{
			pass = 1
		},
		new GameFullscreen.Overlay
		{
			pass = 1
		}
	};

	// Token: 0x040017B5 RID: 6069
	public Shader shader;

	// Token: 0x040017B6 RID: 6070
	private Material material;

	// Token: 0x020004FC RID: 1276
	public struct Overlay
	{
		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x06002B3B RID: 11067 RVA: 0x000AD804 File Offset: 0x000ABA04
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

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x06002B3C RID: 11068 RVA: 0x000AD848 File Offset: 0x000ABA48
		// (set) Token: 0x06002B3D RID: 11069 RVA: 0x000AD850 File Offset: 0x000ABA50
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

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x06002B3E RID: 11070 RVA: 0x000AD8A0 File Offset: 0x000ABAA0
		// (set) Token: 0x06002B3F RID: 11071 RVA: 0x000AD8A8 File Offset: 0x000ABAA8
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

		// Token: 0x040017B7 RID: 6071
		public ScaleMode scaleMode;

		// Token: 0x040017B8 RID: 6072
		public int pass;

		// Token: 0x040017B9 RID: 6073
		private Texture2D _texture;

		// Token: 0x040017BA RID: 6074
		private float _alpha;

		// Token: 0x040017BB RID: 6075
		private bool hasTex;

		// Token: 0x040017BC RID: 6076
		private bool hasAlpha;

		// Token: 0x040017BD RID: 6077
		private bool shouldDraw;
	}
}
