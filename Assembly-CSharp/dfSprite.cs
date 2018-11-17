using System;
using UnityEngine;

// Token: 0x020007C5 RID: 1989
[AddComponentMenu("Daikon Forge/User Interface/Sprite/Basic")]
[RequireComponent(typeof(BoxCollider))]
[ExecuteInEditMode]
[Serializable]
public class dfSprite : global::dfControl
{
	// Token: 0x14000052 RID: 82
	// (add) Token: 0x0600446F RID: 17519 RVA: 0x000FF8B0 File Offset: 0x000FDAB0
	// (remove) Token: 0x06004470 RID: 17520 RVA: 0x000FF8CC File Offset: 0x000FDACC
	public event global::PropertyChangedEventHandler<string> SpriteNameChanged;

	// Token: 0x17000D2F RID: 3375
	// (get) Token: 0x06004471 RID: 17521 RVA: 0x000FF8E8 File Offset: 0x000FDAE8
	// (set) Token: 0x06004472 RID: 17522 RVA: 0x000FF930 File Offset: 0x000FDB30
	public global::dfAtlas Atlas
	{
		get
		{
			if (this.atlas == null)
			{
				global::dfGUIManager manager = base.GetManager();
				if (manager != null)
				{
					return this.atlas = manager.DefaultAtlas;
				}
			}
			return this.atlas;
		}
		set
		{
			if (!global::dfAtlas.Equals(value, this.atlas))
			{
				this.atlas = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000D30 RID: 3376
	// (get) Token: 0x06004473 RID: 17523 RVA: 0x000FF950 File Offset: 0x000FDB50
	// (set) Token: 0x06004474 RID: 17524 RVA: 0x000FF958 File Offset: 0x000FDB58
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
					global::dfAtlas.ItemInfo spriteInfo = this.SpriteInfo;
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

	// Token: 0x17000D31 RID: 3377
	// (get) Token: 0x06004475 RID: 17525 RVA: 0x000FF9D8 File Offset: 0x000FDBD8
	public global::dfAtlas.ItemInfo SpriteInfo
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

	// Token: 0x17000D32 RID: 3378
	// (get) Token: 0x06004476 RID: 17526 RVA: 0x000FFA10 File Offset: 0x000FDC10
	// (set) Token: 0x06004477 RID: 17527 RVA: 0x000FFA18 File Offset: 0x000FDC18
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

	// Token: 0x17000D33 RID: 3379
	// (get) Token: 0x06004478 RID: 17528 RVA: 0x000FFA34 File Offset: 0x000FDC34
	// (set) Token: 0x06004479 RID: 17529 RVA: 0x000FFA3C File Offset: 0x000FDC3C
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

	// Token: 0x17000D34 RID: 3380
	// (get) Token: 0x0600447A RID: 17530 RVA: 0x000FFA58 File Offset: 0x000FDC58
	// (set) Token: 0x0600447B RID: 17531 RVA: 0x000FFA60 File Offset: 0x000FDC60
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

	// Token: 0x17000D35 RID: 3381
	// (get) Token: 0x0600447C RID: 17532 RVA: 0x000FFAA0 File Offset: 0x000FDCA0
	// (set) Token: 0x0600447D RID: 17533 RVA: 0x000FFAA8 File Offset: 0x000FDCA8
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

	// Token: 0x0600447E RID: 17534 RVA: 0x000FFAC4 File Offset: 0x000FDCC4
	protected internal override void OnLocalize()
	{
		base.OnLocalize();
		this.SpriteName = base.getLocalizedValue(this.spriteName);
	}

	// Token: 0x0600447F RID: 17535 RVA: 0x000FFAE0 File Offset: 0x000FDCE0
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

	// Token: 0x06004480 RID: 17536 RVA: 0x000FFB1C File Offset: 0x000FDD1C
	public override Vector2 CalculateMinimumSize()
	{
		global::dfAtlas.ItemInfo spriteInfo = this.SpriteInfo;
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

	// Token: 0x06004481 RID: 17537 RVA: 0x000FFB8C File Offset: 0x000FDD8C
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
		global::dfSprite.RenderOptions options = new global::dfSprite.RenderOptions
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
		global::dfSprite.renderSprite(this.renderData, options);
	}

	// Token: 0x06004482 RID: 17538 RVA: 0x000FFCBC File Offset: 0x000FDEBC
	internal static void renderSprite(global::dfRenderData data, global::dfSprite.RenderOptions options)
	{
		options.baseIndex = data.Vertices.Count;
		global::dfSprite.rebuildTriangles(data, options);
		global::dfSprite.rebuildVertices(data, options);
		global::dfSprite.rebuildUV(data, options);
		global::dfSprite.rebuildColors(data, options);
		if (options.fillAmount > -1f && options.fillAmount < 1f)
		{
			global::dfSprite.doFill(data, options);
		}
	}

	// Token: 0x06004483 RID: 17539 RVA: 0x000FFD20 File Offset: 0x000FDF20
	private static void rebuildTriangles(global::dfRenderData renderData, global::dfSprite.RenderOptions options)
	{
		int baseIndex = options.baseIndex;
		global::dfList<int> triangles = renderData.Triangles;
		triangles.EnsureCapacity(triangles.Count + global::dfSprite.TRIANGLE_INDICES.Length);
		for (int i = 0; i < global::dfSprite.TRIANGLE_INDICES.Length; i++)
		{
			triangles.Add(baseIndex + global::dfSprite.TRIANGLE_INDICES[i]);
		}
	}

	// Token: 0x06004484 RID: 17540 RVA: 0x000FFD78 File Offset: 0x000FDF78
	private static void rebuildVertices(global::dfRenderData renderData, global::dfSprite.RenderOptions options)
	{
		global::dfList<Vector3> vertices = renderData.Vertices;
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

	// Token: 0x06004485 RID: 17541 RVA: 0x000FFE9C File Offset: 0x000FE09C
	private static void rebuildColors(global::dfRenderData renderData, global::dfSprite.RenderOptions options)
	{
		global::dfList<Color32> colors = renderData.Colors;
		colors.Add(options.color);
		colors.Add(options.color);
		colors.Add(options.color);
		colors.Add(options.color);
	}

	// Token: 0x06004486 RID: 17542 RVA: 0x000FFEE4 File Offset: 0x000FE0E4
	private static void rebuildUV(global::dfRenderData renderData, global::dfSprite.RenderOptions options)
	{
		Rect region = options.spriteInfo.region;
		global::dfList<Vector2> uv = renderData.UV;
		uv.Add(new Vector2(region.x, region.yMax));
		uv.Add(new Vector2(region.xMax, region.yMax));
		uv.Add(new Vector2(region.xMax, region.y));
		uv.Add(new Vector2(region.x, region.y));
		Vector2 value = Vector2.zero;
		if (options.flip.IsSet(global::dfSpriteFlip.FlipHorizontal))
		{
			value = uv[1];
			uv[1] = uv[0];
			uv[0] = value;
			value = uv[3];
			uv[3] = uv[2];
			uv[2] = value;
		}
		if (options.flip.IsSet(global::dfSpriteFlip.FlipVertical))
		{
			value = uv[0];
			uv[0] = uv[3];
			uv[3] = value;
			value = uv[1];
			uv[1] = uv[2];
			uv[2] = value;
		}
	}

	// Token: 0x06004487 RID: 17543 RVA: 0x0010000C File Offset: 0x000FE20C
	private static void doFill(global::dfRenderData renderData, global::dfSprite.RenderOptions options)
	{
		int baseIndex = options.baseIndex;
		global::dfList<Vector3> vertices = renderData.Vertices;
		global::dfList<Vector2> uv = renderData.UV;
		int index = baseIndex;
		int index2 = baseIndex + 1;
		int index3 = baseIndex + 3;
		int index4 = baseIndex + 2;
		if (options.invertFill)
		{
			if (options.fillDirection == global::dfFillDirection.Horizontal)
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
		if (options.fillDirection == global::dfFillDirection.Horizontal)
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

	// Token: 0x06004488 RID: 17544 RVA: 0x001001DC File Offset: 0x000FE3DC
	public override string ToString()
	{
		if (!string.IsNullOrEmpty(this.spriteName))
		{
			return string.Format("{0} ({1})", base.name, this.spriteName);
		}
		return base.ToString();
	}

	// Token: 0x04002435 RID: 9269
	private static int[] TRIANGLE_INDICES = new int[]
	{
		0,
		1,
		3,
		3,
		1,
		2
	};

	// Token: 0x04002436 RID: 9270
	[SerializeField]
	protected global::dfAtlas atlas;

	// Token: 0x04002437 RID: 9271
	[SerializeField]
	protected string spriteName;

	// Token: 0x04002438 RID: 9272
	[SerializeField]
	protected global::dfSpriteFlip flip;

	// Token: 0x04002439 RID: 9273
	[SerializeField]
	protected global::dfFillDirection fillDirection;

	// Token: 0x0400243A RID: 9274
	[SerializeField]
	protected float fillAmount = 1f;

	// Token: 0x0400243B RID: 9275
	[SerializeField]
	protected bool invertFill;

	// Token: 0x020007C6 RID: 1990
	internal struct RenderOptions
	{
		// Token: 0x0400243D RID: 9277
		public global::dfAtlas atlas;

		// Token: 0x0400243E RID: 9278
		public global::dfAtlas.ItemInfo spriteInfo;

		// Token: 0x0400243F RID: 9279
		public Color32 color;

		// Token: 0x04002440 RID: 9280
		public float pixelsToUnits;

		// Token: 0x04002441 RID: 9281
		public Vector2 size;

		// Token: 0x04002442 RID: 9282
		public global::dfSpriteFlip flip;

		// Token: 0x04002443 RID: 9283
		public bool invertFill;

		// Token: 0x04002444 RID: 9284
		public global::dfFillDirection fillDirection;

		// Token: 0x04002445 RID: 9285
		public float fillAmount;

		// Token: 0x04002446 RID: 9286
		public Vector3 offset;

		// Token: 0x04002447 RID: 9287
		public int baseIndex;
	}
}
