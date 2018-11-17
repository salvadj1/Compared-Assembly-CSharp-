using System;
using UnityEngine;

// Token: 0x020007D2 RID: 2002
[ExecuteInEditMode]
[AddComponentMenu("Daikon Forge/User Interface/Sprite/Texture")]
[Serializable]
public class dfTextureSprite : global::dfControl
{
	// Token: 0x1400005A RID: 90
	// (add) Token: 0x06004565 RID: 17765 RVA: 0x0010432C File Offset: 0x0010252C
	// (remove) Token: 0x06004566 RID: 17766 RVA: 0x00104348 File Offset: 0x00102548
	public event global::PropertyChangedEventHandler<Texture2D> TextureChanged;

	// Token: 0x17000D5F RID: 3423
	// (get) Token: 0x06004567 RID: 17767 RVA: 0x00104364 File Offset: 0x00102564
	// (set) Token: 0x06004568 RID: 17768 RVA: 0x0010436C File Offset: 0x0010256C
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

	// Token: 0x17000D60 RID: 3424
	// (get) Token: 0x06004569 RID: 17769 RVA: 0x001043D8 File Offset: 0x001025D8
	// (set) Token: 0x0600456A RID: 17770 RVA: 0x001043E0 File Offset: 0x001025E0
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

	// Token: 0x17000D61 RID: 3425
	// (get) Token: 0x0600456B RID: 17771 RVA: 0x00104410 File Offset: 0x00102610
	// (set) Token: 0x0600456C RID: 17772 RVA: 0x00104418 File Offset: 0x00102618
	public global::dfSpriteFlip Flip
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

	// Token: 0x17000D62 RID: 3426
	// (get) Token: 0x0600456D RID: 17773 RVA: 0x00104434 File Offset: 0x00102634
	// (set) Token: 0x0600456E RID: 17774 RVA: 0x0010443C File Offset: 0x0010263C
	public global::dfFillDirection FillDirection
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

	// Token: 0x17000D63 RID: 3427
	// (get) Token: 0x0600456F RID: 17775 RVA: 0x00104458 File Offset: 0x00102658
	// (set) Token: 0x06004570 RID: 17776 RVA: 0x00104460 File Offset: 0x00102660
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

	// Token: 0x17000D64 RID: 3428
	// (get) Token: 0x06004571 RID: 17777 RVA: 0x001044A0 File Offset: 0x001026A0
	// (set) Token: 0x06004572 RID: 17778 RVA: 0x001044A8 File Offset: 0x001026A8
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

	// Token: 0x17000D65 RID: 3429
	// (get) Token: 0x06004573 RID: 17779 RVA: 0x001044C4 File Offset: 0x001026C4
	public Material RenderMaterial
	{
		get
		{
			return this.renderMaterial;
		}
	}

	// Token: 0x06004574 RID: 17780 RVA: 0x001044CC File Offset: 0x001026CC
	public override void OnEnable()
	{
		base.OnEnable();
		this.renderMaterial = null;
	}

	// Token: 0x06004575 RID: 17781 RVA: 0x001044DC File Offset: 0x001026DC
	public override void OnDestroy()
	{
		base.OnDestroy();
		if (this.renderMaterial != null)
		{
			Object.DestroyImmediate(this.renderMaterial);
			this.renderMaterial = null;
		}
	}

	// Token: 0x06004576 RID: 17782 RVA: 0x00104508 File Offset: 0x00102708
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

	// Token: 0x06004577 RID: 17783 RVA: 0x00104550 File Offset: 0x00102750
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
		this.renderData.Triangles.AddRange(global::dfTextureSprite.TRIANGLE_INDICES);
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

	// Token: 0x06004578 RID: 17784 RVA: 0x0010476C File Offset: 0x0010296C
	private void disposeCreatedMaterial()
	{
		if (this.createdRuntimeMaterial)
		{
			Object.DestroyImmediate(this.material);
			this.material = null;
			this.createdRuntimeMaterial = false;
		}
	}

	// Token: 0x06004579 RID: 17785 RVA: 0x001047A0 File Offset: 0x001029A0
	private void rebuildUV(global::dfRenderData renderData)
	{
		global::dfList<Vector2> uv = renderData.UV;
		uv.Add(new Vector2(0f, 1f));
		uv.Add(new Vector2(1f, 1f));
		uv.Add(new Vector2(1f, 0f));
		uv.Add(new Vector2(0f, 0f));
		Vector2 value = Vector2.zero;
		if (this.flip.IsSet(global::dfSpriteFlip.FlipHorizontal))
		{
			value = uv[1];
			uv[1] = uv[0];
			uv[0] = value;
			value = uv[3];
			uv[3] = uv[2];
			uv[2] = value;
		}
		if (this.flip.IsSet(global::dfSpriteFlip.FlipVertical))
		{
			value = uv[0];
			uv[0] = uv[3];
			uv[3] = value;
			value = uv[1];
			uv[1] = uv[2];
			uv[2] = value;
		}
	}

	// Token: 0x0600457A RID: 17786 RVA: 0x001048A8 File Offset: 0x00102AA8
	private void doFill(global::dfRenderData renderData)
	{
		global::dfList<Vector3> vertices = renderData.Vertices;
		global::dfList<Vector2> uv = renderData.UV;
		int index = 0;
		int index2 = 1;
		int index3 = 3;
		int index4 = 2;
		if (this.invertFill)
		{
			if (this.fillDirection == global::dfFillDirection.Horizontal)
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
		if (this.fillDirection == global::dfFillDirection.Horizontal)
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

	// Token: 0x0600457B RID: 17787 RVA: 0x00104A4C File Offset: 0x00102C4C
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

	// Token: 0x0600457C RID: 17788 RVA: 0x00104A88 File Offset: 0x00102C88
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

	// Token: 0x04002487 RID: 9351
	private static int[] TRIANGLE_INDICES = new int[]
	{
		0,
		1,
		3,
		3,
		1,
		2
	};

	// Token: 0x04002488 RID: 9352
	[SerializeField]
	protected Texture2D texture;

	// Token: 0x04002489 RID: 9353
	[SerializeField]
	protected Material material;

	// Token: 0x0400248A RID: 9354
	[SerializeField]
	protected global::dfSpriteFlip flip;

	// Token: 0x0400248B RID: 9355
	[SerializeField]
	protected global::dfFillDirection fillDirection;

	// Token: 0x0400248C RID: 9356
	[SerializeField]
	protected float fillAmount = 1f;

	// Token: 0x0400248D RID: 9357
	[SerializeField]
	protected bool invertFill;

	// Token: 0x0400248E RID: 9358
	private bool createdRuntimeMaterial;

	// Token: 0x0400248F RID: 9359
	private Material renderMaterial;
}
