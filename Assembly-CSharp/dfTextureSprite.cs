using System;
using UnityEngine;

// Token: 0x020006FF RID: 1791
[AddComponentMenu("Daikon Forge/User Interface/Sprite/Texture")]
[ExecuteInEditMode]
[Serializable]
public class dfTextureSprite : dfControl
{
	// Token: 0x1400005A RID: 90
	// (add) Token: 0x06004143 RID: 16707 RVA: 0x000FB640 File Offset: 0x000F9840
	// (remove) Token: 0x06004144 RID: 16708 RVA: 0x000FB65C File Offset: 0x000F985C
	public event PropertyChangedEventHandler<Texture2D> TextureChanged;

	// Token: 0x17000CD9 RID: 3289
	// (get) Token: 0x06004145 RID: 16709 RVA: 0x000FB678 File Offset: 0x000F9878
	// (set) Token: 0x06004146 RID: 16710 RVA: 0x000FB680 File Offset: 0x000F9880
	public Texture2D Texture
	{
		get
		{
			return this.texture;
		}
		set
		{
			if (value != this.texture)
			{
				this.texture = value;
				this.Invalidate();
				if (value != null && this.size.sqrMagnitude <= 1.401298E-45f)
				{
					this.size = new Vector2((float)value.width, (float)value.height);
				}
				this.OnTextureChanged(value);
			}
		}
	}

	// Token: 0x17000CDA RID: 3290
	// (get) Token: 0x06004147 RID: 16711 RVA: 0x000FB6EC File Offset: 0x000F98EC
	// (set) Token: 0x06004148 RID: 16712 RVA: 0x000FB6F4 File Offset: 0x000F98F4
	public Material Material
	{
		get
		{
			return this.material;
		}
		set
		{
			if (value != this.material)
			{
				this.disposeCreatedMaterial();
				this.renderMaterial = null;
				this.material = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CDB RID: 3291
	// (get) Token: 0x06004149 RID: 16713 RVA: 0x000FB724 File Offset: 0x000F9924
	// (set) Token: 0x0600414A RID: 16714 RVA: 0x000FB72C File Offset: 0x000F992C
	public dfSpriteFlip Flip
	{
		get
		{
			return this.flip;
		}
		set
		{
			if (value != this.flip)
			{
				this.flip = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CDC RID: 3292
	// (get) Token: 0x0600414B RID: 16715 RVA: 0x000FB748 File Offset: 0x000F9948
	// (set) Token: 0x0600414C RID: 16716 RVA: 0x000FB750 File Offset: 0x000F9950
	public dfFillDirection FillDirection
	{
		get
		{
			return this.fillDirection;
		}
		set
		{
			if (value != this.fillDirection)
			{
				this.fillDirection = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CDD RID: 3293
	// (get) Token: 0x0600414D RID: 16717 RVA: 0x000FB76C File Offset: 0x000F996C
	// (set) Token: 0x0600414E RID: 16718 RVA: 0x000FB774 File Offset: 0x000F9974
	public float FillAmount
	{
		get
		{
			return this.fillAmount;
		}
		set
		{
			if (!Mathf.Approximately(value, this.fillAmount))
			{
				this.fillAmount = Mathf.Max(0f, Mathf.Min(1f, value));
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CDE RID: 3294
	// (get) Token: 0x0600414F RID: 16719 RVA: 0x000FB7B4 File Offset: 0x000F99B4
	// (set) Token: 0x06004150 RID: 16720 RVA: 0x000FB7BC File Offset: 0x000F99BC
	public bool InvertFill
	{
		get
		{
			return this.invertFill;
		}
		set
		{
			if (value != this.invertFill)
			{
				this.invertFill = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CDF RID: 3295
	// (get) Token: 0x06004151 RID: 16721 RVA: 0x000FB7D8 File Offset: 0x000F99D8
	public Material RenderMaterial
	{
		get
		{
			return this.renderMaterial;
		}
	}

	// Token: 0x06004152 RID: 16722 RVA: 0x000FB7E0 File Offset: 0x000F99E0
	public override void OnEnable()
	{
		base.OnEnable();
		this.renderMaterial = null;
	}

	// Token: 0x06004153 RID: 16723 RVA: 0x000FB7F0 File Offset: 0x000F99F0
	public override void OnDestroy()
	{
		base.OnDestroy();
		if (this.renderMaterial != null)
		{
			Object.DestroyImmediate(this.renderMaterial);
			this.renderMaterial = null;
		}
	}

	// Token: 0x06004154 RID: 16724 RVA: 0x000FB81C File Offset: 0x000F9A1C
	public override void OnDisable()
	{
		base.OnDisable();
		this.disposeCreatedMaterial();
		if (Application.isPlaying && this.renderMaterial != null)
		{
			Object.DestroyImmediate(this.renderMaterial);
			this.renderMaterial = null;
		}
	}

	// Token: 0x06004155 RID: 16725 RVA: 0x000FB864 File Offset: 0x000F9A64
	protected override void OnRebuildRenderData()
	{
		base.OnRebuildRenderData();
		if (this.texture == null)
		{
			return;
		}
		this.ensureMaterial();
		if (this.material == null)
		{
			return;
		}
		if (this.renderMaterial == null)
		{
			this.renderMaterial = new Material(this.material)
			{
				hideFlags = 4,
				name = this.material.name + " (copy)"
			};
		}
		this.renderMaterial.mainTexture = this.texture;
		this.renderData.Material = this.renderMaterial;
		float num = base.PixelsToUnits();
		float num2 = 0f;
		float num3 = 0f;
		float num4 = this.size.x * num;
		float num5 = -this.size.y * num;
		Vector3 vector = this.pivot.TransformToUpperLeft(this.size).RoundToInt() * num;
		this.renderData.Vertices.Add(new Vector3(num2, num3, 0f) + vector);
		this.renderData.Vertices.Add(new Vector3(num4, num3, 0f) + vector);
		this.renderData.Vertices.Add(new Vector3(num4, num5, 0f) + vector);
		this.renderData.Vertices.Add(new Vector3(num2, num5, 0f) + vector);
		this.renderData.Triangles.AddRange(dfTextureSprite.TRIANGLE_INDICES);
		this.rebuildUV(this.renderData);
		Color32 item = base.ApplyOpacity(this.color);
		this.renderData.Colors.Add(item);
		this.renderData.Colors.Add(item);
		this.renderData.Colors.Add(item);
		this.renderData.Colors.Add(item);
		if (this.fillAmount < 1f)
		{
			this.doFill(this.renderData);
		}
	}

	// Token: 0x06004156 RID: 16726 RVA: 0x000FBA80 File Offset: 0x000F9C80
	private void disposeCreatedMaterial()
	{
		if (this.createdRuntimeMaterial)
		{
			Object.DestroyImmediate(this.material);
			this.material = null;
			this.createdRuntimeMaterial = false;
		}
	}

	// Token: 0x06004157 RID: 16727 RVA: 0x000FBAB4 File Offset: 0x000F9CB4
	private void rebuildUV(dfRenderData renderData)
	{
		dfList<Vector2> uv = renderData.UV;
		uv.Add(new Vector2(0f, 1f));
		uv.Add(new Vector2(1f, 1f));
		uv.Add(new Vector2(1f, 0f));
		uv.Add(new Vector2(0f, 0f));
		Vector2 value = Vector2.zero;
		if (this.flip.IsSet(dfSpriteFlip.FlipHorizontal))
		{
			value = uv[1];
			uv[1] = uv[0];
			uv[0] = value;
			value = uv[3];
			uv[3] = uv[2];
			uv[2] = value;
		}
		if (this.flip.IsSet(dfSpriteFlip.FlipVertical))
		{
			value = uv[0];
			uv[0] = uv[3];
			uv[3] = value;
			value = uv[1];
			uv[1] = uv[2];
			uv[2] = value;
		}
	}

	// Token: 0x06004158 RID: 16728 RVA: 0x000FBBBC File Offset: 0x000F9DBC
	private void doFill(dfRenderData renderData)
	{
		dfList<Vector3> vertices = renderData.Vertices;
		dfList<Vector2> uv = renderData.UV;
		int index = 0;
		int index2 = 1;
		int index3 = 3;
		int index4 = 2;
		if (this.invertFill)
		{
			if (this.fillDirection == dfFillDirection.Horizontal)
			{
				index = 1;
				index2 = 0;
				index3 = 2;
				index4 = 3;
			}
			else
			{
				index = 3;
				index2 = 2;
				index3 = 0;
				index4 = 1;
			}
		}
		if (this.fillDirection == dfFillDirection.Horizontal)
		{
			vertices[index2] = Vector3.Lerp(vertices[index2], vertices[index], 1f - this.fillAmount);
			vertices[index4] = Vector3.Lerp(vertices[index4], vertices[index3], 1f - this.fillAmount);
			uv[index2] = Vector2.Lerp(uv[index2], uv[index], 1f - this.fillAmount);
			uv[index4] = Vector2.Lerp(uv[index4], uv[index3], 1f - this.fillAmount);
		}
		else
		{
			vertices[index3] = Vector3.Lerp(vertices[index3], vertices[index], 1f - this.fillAmount);
			vertices[index4] = Vector3.Lerp(vertices[index4], vertices[index2], 1f - this.fillAmount);
			uv[index3] = Vector2.Lerp(uv[index3], uv[index], 1f - this.fillAmount);
			uv[index4] = Vector2.Lerp(uv[index4], uv[index2], 1f - this.fillAmount);
		}
	}

	// Token: 0x06004159 RID: 16729 RVA: 0x000FBD60 File Offset: 0x000F9F60
	protected internal virtual void OnTextureChanged(Texture2D value)
	{
		base.SignalHierarchy("OnTextureChanged", new object[]
		{
			value
		});
		if (this.TextureChanged != null)
		{
			this.TextureChanged(this, value);
		}
	}

	// Token: 0x0600415A RID: 16730 RVA: 0x000FBD9C File Offset: 0x000F9F9C
	private void ensureMaterial()
	{
		if (this.material != null || this.texture == null)
		{
			return;
		}
		Shader shader = Shader.Find("Daikon Forge/Default UI Shader");
		if (shader == null)
		{
			Debug.LogError("Failed to find default shader");
			return;
		}
		this.material = new Material(shader)
		{
			name = "Default Texture Shader",
			hideFlags = 4,
			mainTexture = this.texture
		};
		this.createdRuntimeMaterial = true;
	}

	// Token: 0x0400227B RID: 8827
	private static int[] TRIANGLE_INDICES = new int[]
	{
		0,
		1,
		3,
		3,
		1,
		2
	};

	// Token: 0x0400227C RID: 8828
	[SerializeField]
	protected Texture2D texture;

	// Token: 0x0400227D RID: 8829
	[SerializeField]
	protected Material material;

	// Token: 0x0400227E RID: 8830
	[SerializeField]
	protected dfSpriteFlip flip;

	// Token: 0x0400227F RID: 8831
	[SerializeField]
	protected dfFillDirection fillDirection;

	// Token: 0x04002280 RID: 8832
	[SerializeField]
	protected float fillAmount = 1f;

	// Token: 0x04002281 RID: 8833
	[SerializeField]
	protected bool invertFill;

	// Token: 0x04002282 RID: 8834
	private bool createdRuntimeMaterial;

	// Token: 0x04002283 RID: 8835
	private Material renderMaterial;
}
