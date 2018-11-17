using System;
using UnityEngine;

// Token: 0x020006F3 RID: 1779
[RequireComponent(typeof(BoxCollider))]
[AddComponentMenu("Daikon Forge/User Interface/Sprite/Basic")]
[ExecuteInEditMode]
[Serializable]
public class dfSprite : dfControl
{
	// Token: 0x14000052 RID: 82
	// (add) Token: 0x06004053 RID: 16467 RVA: 0x000F6CAC File Offset: 0x000F4EAC
	// (remove) Token: 0x06004054 RID: 16468 RVA: 0x000F6CC8 File Offset: 0x000F4EC8
	public event PropertyChangedEventHandler<string> SpriteNameChanged;

	// Token: 0x17000CAB RID: 3243
	// (get) Token: 0x06004055 RID: 16469 RVA: 0x000F6CE4 File Offset: 0x000F4EE4
	// (set) Token: 0x06004056 RID: 16470 RVA: 0x000F6D2C File Offset: 0x000F4F2C
	public dfAtlas Atlas
	{
		get
		{
			if (this.atlas == null)
			{
				dfGUIManager manager = base.GetManager();
				if (manager != null)
				{
					return this.atlas = manager.DefaultAtlas;
				}
			}
			return this.atlas;
		}
		set
		{
			if (!dfAtlas.Equals(value, this.atlas))
			{
				this.atlas = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CAC RID: 3244
	// (get) Token: 0x06004057 RID: 16471 RVA: 0x000F6D4C File Offset: 0x000F4F4C
	// (set) Token: 0x06004058 RID: 16472 RVA: 0x000F6D54 File Offset: 0x000F4F54
	public string SpriteName
	{
		get
		{
			return this.spriteName;
		}
		set
		{
			value = base.getLocalizedValue(value);
			if (value != this.spriteName)
			{
				this.spriteName = value;
				if (!Application.isPlaying)
				{
					dfAtlas.ItemInfo spriteInfo = this.SpriteInfo;
					if (this.size == Vector2.zero && spriteInfo != null)
					{
						this.size = spriteInfo.sizeInPixels;
						this.updateCollider();
					}
				}
				this.Invalidate();
				this.OnSpriteNameChanged(value);
			}
		}
	}

	// Token: 0x17000CAD RID: 3245
	// (get) Token: 0x06004059 RID: 16473 RVA: 0x000F6DD4 File Offset: 0x000F4FD4
	public dfAtlas.ItemInfo SpriteInfo
	{
		get
		{
			if (this.Atlas == null)
			{
				return null;
			}
			return this.Atlas[this.spriteName];
		}
	}

	// Token: 0x17000CAE RID: 3246
	// (get) Token: 0x0600405A RID: 16474 RVA: 0x000F6E0C File Offset: 0x000F500C
	// (set) Token: 0x0600405B RID: 16475 RVA: 0x000F6E14 File Offset: 0x000F5014
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

	// Token: 0x17000CAF RID: 3247
	// (get) Token: 0x0600405C RID: 16476 RVA: 0x000F6E30 File Offset: 0x000F5030
	// (set) Token: 0x0600405D RID: 16477 RVA: 0x000F6E38 File Offset: 0x000F5038
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

	// Token: 0x17000CB0 RID: 3248
	// (get) Token: 0x0600405E RID: 16478 RVA: 0x000F6E54 File Offset: 0x000F5054
	// (set) Token: 0x0600405F RID: 16479 RVA: 0x000F6E5C File Offset: 0x000F505C
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

	// Token: 0x17000CB1 RID: 3249
	// (get) Token: 0x06004060 RID: 16480 RVA: 0x000F6E9C File Offset: 0x000F509C
	// (set) Token: 0x06004061 RID: 16481 RVA: 0x000F6EA4 File Offset: 0x000F50A4
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

	// Token: 0x06004062 RID: 16482 RVA: 0x000F6EC0 File Offset: 0x000F50C0
	protected internal override void OnLocalize()
	{
		base.OnLocalize();
		this.SpriteName = base.getLocalizedValue(this.spriteName);
	}

	// Token: 0x06004063 RID: 16483 RVA: 0x000F6EDC File Offset: 0x000F50DC
	protected internal virtual void OnSpriteNameChanged(string value)
	{
		base.Signal("OnSpriteNameChanged", new object[]
		{
			value
		});
		if (this.SpriteNameChanged != null)
		{
			this.SpriteNameChanged(this, value);
		}
	}

	// Token: 0x06004064 RID: 16484 RVA: 0x000F6F18 File Offset: 0x000F5118
	public override Vector2 CalculateMinimumSize()
	{
		dfAtlas.ItemInfo spriteInfo = this.SpriteInfo;
		if (spriteInfo == null)
		{
			return Vector2.zero;
		}
		RectOffset border = spriteInfo.border;
		if (border != null && border.horizontal > 0 && border.vertical > 0)
		{
			return Vector2.Max(base.CalculateMinimumSize(), new Vector2((float)border.horizontal, (float)border.vertical));
		}
		return base.CalculateMinimumSize();
	}

	// Token: 0x06004065 RID: 16485 RVA: 0x000F6F88 File Offset: 0x000F5188
	protected override void OnRebuildRenderData()
	{
		if (!(this.Atlas != null) || !(this.Atlas.Material != null) || !base.IsVisible)
		{
			return;
		}
		if (this.SpriteInfo == null)
		{
			return;
		}
		this.renderData.Material = this.Atlas.Material;
		Color32 color = base.ApplyOpacity((!base.IsEnabled) ? this.disabledColor : this.color);
		dfSprite.RenderOptions options = new dfSprite.RenderOptions
		{
			atlas = this.Atlas,
			color = color,
			fillAmount = this.fillAmount,
			fillDirection = this.fillDirection,
			flip = this.flip,
			invertFill = this.invertFill,
			offset = this.pivot.TransformToUpperLeft(base.Size),
			pixelsToUnits = base.PixelsToUnits(),
			size = base.Size,
			spriteInfo = this.SpriteInfo
		};
		dfSprite.renderSprite(this.renderData, options);
	}

	// Token: 0x06004066 RID: 16486 RVA: 0x000F70B8 File Offset: 0x000F52B8
	internal static void renderSprite(dfRenderData data, dfSprite.RenderOptions options)
	{
		options.baseIndex = data.Vertices.Count;
		dfSprite.rebuildTriangles(data, options);
		dfSprite.rebuildVertices(data, options);
		dfSprite.rebuildUV(data, options);
		dfSprite.rebuildColors(data, options);
		if (options.fillAmount > -1f && options.fillAmount < 1f)
		{
			dfSprite.doFill(data, options);
		}
	}

	// Token: 0x06004067 RID: 16487 RVA: 0x000F711C File Offset: 0x000F531C
	private static void rebuildTriangles(dfRenderData renderData, dfSprite.RenderOptions options)
	{
		int baseIndex = options.baseIndex;
		dfList<int> triangles = renderData.Triangles;
		triangles.EnsureCapacity(triangles.Count + dfSprite.TRIANGLE_INDICES.Length);
		for (int i = 0; i < dfSprite.TRIANGLE_INDICES.Length; i++)
		{
			triangles.Add(baseIndex + dfSprite.TRIANGLE_INDICES[i]);
		}
	}

	// Token: 0x06004068 RID: 16488 RVA: 0x000F7174 File Offset: 0x000F5374
	private static void rebuildVertices(dfRenderData renderData, dfSprite.RenderOptions options)
	{
		dfList<Vector3> vertices = renderData.Vertices;
		int baseIndex = options.baseIndex;
		float num = 0f;
		float num2 = 0f;
		float num3 = Mathf.Ceil(options.size.x);
		float num4 = Mathf.Ceil(-options.size.y);
		vertices.Add(new Vector3(num, num2, 0f) * options.pixelsToUnits);
		vertices.Add(new Vector3(num3, num2, 0f) * options.pixelsToUnits);
		vertices.Add(new Vector3(num3, num4, 0f) * options.pixelsToUnits);
		vertices.Add(new Vector3(num, num4, 0f) * options.pixelsToUnits);
		Vector3 vector = options.offset.RoundToInt() * options.pixelsToUnits;
		for (int i = 0; i < 4; i++)
		{
			vertices[baseIndex + i] = (vertices[baseIndex + i] + vector).Quantize(options.pixelsToUnits);
		}
	}

	// Token: 0x06004069 RID: 16489 RVA: 0x000F7298 File Offset: 0x000F5498
	private static void rebuildColors(dfRenderData renderData, dfSprite.RenderOptions options)
	{
		dfList<Color32> colors = renderData.Colors;
		colors.Add(options.color);
		colors.Add(options.color);
		colors.Add(options.color);
		colors.Add(options.color);
	}

	// Token: 0x0600406A RID: 16490 RVA: 0x000F72E0 File Offset: 0x000F54E0
	private static void rebuildUV(dfRenderData renderData, dfSprite.RenderOptions options)
	{
		Rect region = options.spriteInfo.region;
		dfList<Vector2> uv = renderData.UV;
		uv.Add(new Vector2(region.x, region.yMax));
		uv.Add(new Vector2(region.xMax, region.yMax));
		uv.Add(new Vector2(region.xMax, region.y));
		uv.Add(new Vector2(region.x, region.y));
		Vector2 value = Vector2.zero;
		if (options.flip.IsSet(dfSpriteFlip.FlipHorizontal))
		{
			value = uv[1];
			uv[1] = uv[0];
			uv[0] = value;
			value = uv[3];
			uv[3] = uv[2];
			uv[2] = value;
		}
		if (options.flip.IsSet(dfSpriteFlip.FlipVertical))
		{
			value = uv[0];
			uv[0] = uv[3];
			uv[3] = value;
			value = uv[1];
			uv[1] = uv[2];
			uv[2] = value;
		}
	}

	// Token: 0x0600406B RID: 16491 RVA: 0x000F7408 File Offset: 0x000F5608
	private static void doFill(dfRenderData renderData, dfSprite.RenderOptions options)
	{
		int baseIndex = options.baseIndex;
		dfList<Vector3> vertices = renderData.Vertices;
		dfList<Vector2> uv = renderData.UV;
		int index = baseIndex;
		int index2 = baseIndex + 1;
		int index3 = baseIndex + 3;
		int index4 = baseIndex + 2;
		if (options.invertFill)
		{
			if (options.fillDirection == dfFillDirection.Horizontal)
			{
				index = baseIndex + 1;
				index2 = baseIndex;
				index3 = baseIndex + 2;
				index4 = baseIndex + 3;
			}
			else
			{
				index = baseIndex + 3;
				index2 = baseIndex + 2;
				index3 = baseIndex;
				index4 = baseIndex + 1;
			}
		}
		if (options.fillDirection == dfFillDirection.Horizontal)
		{
			vertices[index2] = Vector3.Lerp(vertices[index2], vertices[index], 1f - options.fillAmount);
			vertices[index4] = Vector3.Lerp(vertices[index4], vertices[index3], 1f - options.fillAmount);
			uv[index2] = Vector2.Lerp(uv[index2], uv[index], 1f - options.fillAmount);
			uv[index4] = Vector2.Lerp(uv[index4], uv[index3], 1f - options.fillAmount);
		}
		else
		{
			vertices[index3] = Vector3.Lerp(vertices[index3], vertices[index], 1f - options.fillAmount);
			vertices[index4] = Vector3.Lerp(vertices[index4], vertices[index2], 1f - options.fillAmount);
			uv[index3] = Vector2.Lerp(uv[index3], uv[index], 1f - options.fillAmount);
			uv[index4] = Vector2.Lerp(uv[index4], uv[index2], 1f - options.fillAmount);
		}
	}

	// Token: 0x0600406C RID: 16492 RVA: 0x000F75D8 File Offset: 0x000F57D8
	public override string ToString()
	{
		if (!string.IsNullOrEmpty(this.spriteName))
		{
			return string.Format("{0} ({1})", base.name, this.spriteName);
		}
		return base.ToString();
	}

	// Token: 0x0400222C RID: 8748
	private static int[] TRIANGLE_INDICES = new int[]
	{
		0,
		1,
		3,
		3,
		1,
		2
	};

	// Token: 0x0400222D RID: 8749
	[SerializeField]
	protected dfAtlas atlas;

	// Token: 0x0400222E RID: 8750
	[SerializeField]
	protected string spriteName;

	// Token: 0x0400222F RID: 8751
	[SerializeField]
	protected dfSpriteFlip flip;

	// Token: 0x04002230 RID: 8752
	[SerializeField]
	protected dfFillDirection fillDirection;

	// Token: 0x04002231 RID: 8753
	[SerializeField]
	protected float fillAmount = 1f;

	// Token: 0x04002232 RID: 8754
	[SerializeField]
	protected bool invertFill;

	// Token: 0x020006F4 RID: 1780
	internal struct RenderOptions
	{
		// Token: 0x04002234 RID: 8756
		public dfAtlas atlas;

		// Token: 0x04002235 RID: 8757
		public dfAtlas.ItemInfo spriteInfo;

		// Token: 0x04002236 RID: 8758
		public Color32 color;

		// Token: 0x04002237 RID: 8759
		public float pixelsToUnits;

		// Token: 0x04002238 RID: 8760
		public Vector2 size;

		// Token: 0x04002239 RID: 8761
		public dfSpriteFlip flip;

		// Token: 0x0400223A RID: 8762
		public bool invertFill;

		// Token: 0x0400223B RID: 8763
		public dfFillDirection fillDirection;

		// Token: 0x0400223C RID: 8764
		public float fillAmount;

		// Token: 0x0400223D RID: 8765
		public Vector3 offset;

		// Token: 0x0400223E RID: 8766
		public int baseIndex;
	}
}
