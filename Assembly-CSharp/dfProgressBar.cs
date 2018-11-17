using System;
using UnityEngine;

// Token: 0x020007BA RID: 1978
[ExecuteInEditMode]
[AddComponentMenu("Daikon Forge/User Interface/Progress Bar")]
[RequireComponent(typeof(BoxCollider))]
[Serializable]
public class dfProgressBar : global::dfControl
{
	// Token: 0x1400004E RID: 78
	// (add) Token: 0x06004356 RID: 17238 RVA: 0x000F9154 File Offset: 0x000F7354
	// (remove) Token: 0x06004357 RID: 17239 RVA: 0x000F9170 File Offset: 0x000F7370
	public event global::PropertyChangedEventHandler<float> ValueChanged;

	// Token: 0x17000CE7 RID: 3303
	// (get) Token: 0x06004358 RID: 17240 RVA: 0x000F918C File Offset: 0x000F738C
	// (set) Token: 0x06004359 RID: 17241 RVA: 0x000F91D4 File Offset: 0x000F73D4
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

	// Token: 0x17000CE8 RID: 3304
	// (get) Token: 0x0600435A RID: 17242 RVA: 0x000F91F4 File Offset: 0x000F73F4
	// (set) Token: 0x0600435B RID: 17243 RVA: 0x000F91FC File Offset: 0x000F73FC
	public string BackgroundSprite
	{
		get
		{
			return this.backgroundSprite;
		}
		set
		{
			if (value != this.backgroundSprite)
			{
				this.backgroundSprite = value;
				this.setDefaultSize(value);
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CE9 RID: 3305
	// (get) Token: 0x0600435C RID: 17244 RVA: 0x000F9224 File Offset: 0x000F7424
	// (set) Token: 0x0600435D RID: 17245 RVA: 0x000F922C File Offset: 0x000F742C
	public string ProgressSprite
	{
		get
		{
			return this.progressSprite;
		}
		set
		{
			if (value != this.progressSprite)
			{
				this.progressSprite = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CEA RID: 3306
	// (get) Token: 0x0600435E RID: 17246 RVA: 0x000F924C File Offset: 0x000F744C
	// (set) Token: 0x0600435F RID: 17247 RVA: 0x000F9254 File Offset: 0x000F7454
	public Color32 ProgressColor
	{
		get
		{
			return this.progressColor;
		}
		set
		{
			if (!object.Equals(value, this.progressColor))
			{
				this.progressColor = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CEB RID: 3307
	// (get) Token: 0x06004360 RID: 17248 RVA: 0x000F928C File Offset: 0x000F748C
	// (set) Token: 0x06004361 RID: 17249 RVA: 0x000F9294 File Offset: 0x000F7494
	public float MinValue
	{
		get
		{
			return this.minValue;
		}
		set
		{
			if (value != this.minValue)
			{
				this.minValue = value;
				if (this.rawValue < value)
				{
					this.Value = value;
				}
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CEC RID: 3308
	// (get) Token: 0x06004362 RID: 17250 RVA: 0x000F92D0 File Offset: 0x000F74D0
	// (set) Token: 0x06004363 RID: 17251 RVA: 0x000F92D8 File Offset: 0x000F74D8
	public float MaxValue
	{
		get
		{
			return this.maxValue;
		}
		set
		{
			if (value != this.maxValue)
			{
				this.maxValue = value;
				if (this.rawValue > value)
				{
					this.Value = value;
				}
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CED RID: 3309
	// (get) Token: 0x06004364 RID: 17252 RVA: 0x000F9314 File Offset: 0x000F7514
	// (set) Token: 0x06004365 RID: 17253 RVA: 0x000F931C File Offset: 0x000F751C
	public float Value
	{
		get
		{
			return this.rawValue;
		}
		set
		{
			value = Mathf.Max(this.minValue, Mathf.Min(this.maxValue, value));
			if (!Mathf.Approximately(value, this.rawValue))
			{
				this.rawValue = value;
				this.OnValueChanged();
			}
		}
	}

	// Token: 0x17000CEE RID: 3310
	// (get) Token: 0x06004366 RID: 17254 RVA: 0x000F9358 File Offset: 0x000F7558
	// (set) Token: 0x06004367 RID: 17255 RVA: 0x000F9360 File Offset: 0x000F7560
	public global::dfProgressFillMode FillMode
	{
		get
		{
			return this.fillMode;
		}
		set
		{
			if (value != this.fillMode)
			{
				this.fillMode = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CEF RID: 3311
	// (get) Token: 0x06004368 RID: 17256 RVA: 0x000F937C File Offset: 0x000F757C
	// (set) Token: 0x06004369 RID: 17257 RVA: 0x000F939C File Offset: 0x000F759C
	public RectOffset Padding
	{
		get
		{
			if (this.padding == null)
			{
				this.padding = new RectOffset();
			}
			return this.padding;
		}
		set
		{
			if (!object.Equals(value, this.padding))
			{
				this.padding = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CF0 RID: 3312
	// (get) Token: 0x0600436A RID: 17258 RVA: 0x000F93BC File Offset: 0x000F75BC
	// (set) Token: 0x0600436B RID: 17259 RVA: 0x000F93C4 File Offset: 0x000F75C4
	public bool ActAsSlider
	{
		get
		{
			return this.actAsSlider;
		}
		set
		{
			this.actAsSlider = value;
		}
	}

	// Token: 0x0600436C RID: 17260 RVA: 0x000F93D0 File Offset: 0x000F75D0
	protected internal override void OnMouseWheel(global::dfMouseEventArgs args)
	{
		try
		{
			if (this.actAsSlider)
			{
				float num = (this.maxValue - this.minValue) * 0.1f;
				this.Value += num * (float)Mathf.RoundToInt(-args.WheelDelta);
				args.Use();
			}
		}
		finally
		{
			base.OnMouseWheel(args);
		}
	}

	// Token: 0x0600436D RID: 17261 RVA: 0x000F944C File Offset: 0x000F764C
	protected internal override void OnMouseMove(global::dfMouseEventArgs args)
	{
		try
		{
			if (this.actAsSlider)
			{
				if (args.Buttons.IsSet(global::dfMouseButtons.Left))
				{
					this.Value = this.getValueFromMouseEvent(args);
					args.Use();
				}
			}
		}
		finally
		{
			base.OnMouseMove(args);
		}
	}

	// Token: 0x0600436E RID: 17262 RVA: 0x000F94BC File Offset: 0x000F76BC
	protected internal override void OnMouseDown(global::dfMouseEventArgs args)
	{
		try
		{
			if (this.actAsSlider)
			{
				if (args.Buttons.IsSet(global::dfMouseButtons.Left))
				{
					base.Focus();
					this.Value = this.getValueFromMouseEvent(args);
					args.Use();
				}
			}
		}
		finally
		{
			base.OnMouseDown(args);
		}
	}

	// Token: 0x0600436F RID: 17263 RVA: 0x000F9534 File Offset: 0x000F7734
	protected internal override void OnKeyDown(global::dfKeyEventArgs args)
	{
		try
		{
			if (this.actAsSlider)
			{
				float num = (this.maxValue - this.minValue) * 0.1f;
				if (args.KeyCode == 276)
				{
					this.Value -= num;
					args.Use();
				}
				else if (args.KeyCode == 275)
				{
					this.Value += num;
					args.Use();
				}
			}
		}
		finally
		{
			base.OnKeyDown(args);
		}
	}

	// Token: 0x06004370 RID: 17264 RVA: 0x000F95E0 File Offset: 0x000F77E0
	protected internal virtual void OnValueChanged()
	{
		this.Invalidate();
		base.SignalHierarchy("OnValueChanged", new object[]
		{
			this.Value
		});
		if (this.ValueChanged != null)
		{
			this.ValueChanged(this, this.Value);
		}
	}

	// Token: 0x06004371 RID: 17265 RVA: 0x000F9630 File Offset: 0x000F7830
	protected override void OnRebuildRenderData()
	{
		if (this.Atlas == null)
		{
			return;
		}
		this.renderData.Material = this.Atlas.Material;
		this.renderBackground();
		this.renderProgressFill();
	}

	// Token: 0x06004372 RID: 17266 RVA: 0x000F9674 File Offset: 0x000F7874
	private void renderProgressFill()
	{
		if (this.Atlas == null)
		{
			return;
		}
		global::dfAtlas.ItemInfo itemInfo = this.Atlas[this.progressSprite];
		if (itemInfo == null)
		{
			return;
		}
		Vector3 vector;
		vector..ctor((float)this.padding.left, (float)(-(float)this.padding.top));
		Vector2 size;
		size..ctor(this.size.x - (float)this.padding.horizontal, this.size.y - (float)this.padding.vertical);
		float fillAmount = 1f;
		float num = this.maxValue - this.minValue;
		float num2 = (this.rawValue - this.minValue) / num;
		global::dfProgressFillMode dfProgressFillMode = this.fillMode;
		if (dfProgressFillMode != global::dfProgressFillMode.Stretch || size.x * num2 < (float)itemInfo.border.horizontal)
		{
		}
		if (dfProgressFillMode == global::dfProgressFillMode.Fill)
		{
			fillAmount = num2;
		}
		else
		{
			size.x = Mathf.Max((float)itemInfo.border.horizontal, size.x * num2);
		}
		Color32 color = base.ApplyOpacity((!base.IsEnabled) ? base.DisabledColor : this.ProgressColor);
		global::dfSprite.RenderOptions options = new global::dfSprite.RenderOptions
		{
			atlas = this.atlas,
			color = color,
			fillAmount = fillAmount,
			offset = this.pivot.TransformToUpperLeft(base.Size) + vector,
			pixelsToUnits = base.PixelsToUnits(),
			size = size,
			spriteInfo = itemInfo
		};
		if (itemInfo.border.horizontal == 0 && itemInfo.border.vertical == 0)
		{
			global::dfSprite.renderSprite(this.renderData, options);
		}
		else
		{
			global::dfSlicedSprite.renderSprite(this.renderData, options);
		}
	}

	// Token: 0x06004373 RID: 17267 RVA: 0x000F9858 File Offset: 0x000F7A58
	private void renderBackground()
	{
		if (this.Atlas == null)
		{
			return;
		}
		global::dfAtlas.ItemInfo itemInfo = this.Atlas[this.backgroundSprite];
		if (itemInfo == null)
		{
			return;
		}
		Color32 color = base.ApplyOpacity((!base.IsEnabled) ? base.DisabledColor : base.Color);
		global::dfSprite.RenderOptions options = new global::dfSprite.RenderOptions
		{
			atlas = this.atlas,
			color = color,
			fillAmount = 1f,
			offset = this.pivot.TransformToUpperLeft(base.Size),
			pixelsToUnits = base.PixelsToUnits(),
			size = base.Size,
			spriteInfo = itemInfo
		};
		if (itemInfo.border.horizontal == 0 && itemInfo.border.vertical == 0)
		{
			global::dfSprite.renderSprite(this.renderData, options);
		}
		else
		{
			global::dfSlicedSprite.renderSprite(this.renderData, options);
		}
	}

	// Token: 0x06004374 RID: 17268 RVA: 0x000F9960 File Offset: 0x000F7B60
	private float getValueFromMouseEvent(global::dfMouseEventArgs args)
	{
		Vector3[] endPoints = this.getEndPoints(true);
		Vector3 vector = endPoints[0];
		Vector3 vector2 = endPoints[1];
		Plane plane;
		plane..ctor(base.transform.TransformDirection(Vector3.back), vector);
		Ray ray = args.Ray;
		float num = 0f;
		if (!plane.Raycast(ray, ref num))
		{
			return this.rawValue;
		}
		Vector3 test = ray.origin + ray.direction * num;
		Vector3 vector3 = global::dfProgressBar.closestPoint(vector, vector2, test, true);
		float num2 = (vector3 - vector).magnitude / (vector2 - vector).magnitude;
		return this.minValue + (this.maxValue - this.minValue) * num2;
	}

	// Token: 0x06004375 RID: 17269 RVA: 0x000F9A34 File Offset: 0x000F7C34
	private Vector3[] getEndPoints(bool convertToWorld = false)
	{
		Vector3 vector = this.pivot.TransformToUpperLeft(base.Size);
		Vector3 vector2;
		vector2..ctor(vector.x + (float)this.padding.left, vector.y - this.size.y * 0.5f);
		Vector3 vector3 = vector2 + new Vector3(this.size.x - (float)this.padding.right, 0f);
		if (convertToWorld)
		{
			float num = base.PixelsToUnits();
			Matrix4x4 localToWorldMatrix = base.transform.localToWorldMatrix;
			vector2 = localToWorldMatrix.MultiplyPoint(vector2 * num);
			vector3 = localToWorldMatrix.MultiplyPoint(vector3 * num);
		}
		return new Vector3[]
		{
			vector2,
			vector3
		};
	}

	// Token: 0x06004376 RID: 17270 RVA: 0x000F9B08 File Offset: 0x000F7D08
	private static Vector3 closestPoint(Vector3 start, Vector3 end, Vector3 test, bool clamp)
	{
		Vector3 vector = test - start;
		Vector3 vector2 = (end - start).normalized;
		float magnitude = (end - start).magnitude;
		float num = Vector3.Dot(vector2, vector);
		if (clamp)
		{
			if (num < 0f)
			{
				return start;
			}
			if (num > magnitude)
			{
				return end;
			}
		}
		vector2 *= num;
		return start + vector2;
	}

	// Token: 0x06004377 RID: 17271 RVA: 0x000F9B74 File Offset: 0x000F7D74
	private void setDefaultSize(string spriteName)
	{
		if (this.Atlas == null)
		{
			return;
		}
		global::dfAtlas.ItemInfo itemInfo = this.Atlas[spriteName];
		if (this.size == Vector2.zero && itemInfo != null)
		{
			base.Size = itemInfo.sizeInPixels;
		}
	}

	// Token: 0x040023CF RID: 9167
	[SerializeField]
	protected global::dfAtlas atlas;

	// Token: 0x040023D0 RID: 9168
	[SerializeField]
	protected string backgroundSprite;

	// Token: 0x040023D1 RID: 9169
	[SerializeField]
	protected string progressSprite;

	// Token: 0x040023D2 RID: 9170
	[SerializeField]
	protected Color32 progressColor = UnityEngine.Color.white;

	// Token: 0x040023D3 RID: 9171
	[SerializeField]
	protected float rawValue = 0.25f;

	// Token: 0x040023D4 RID: 9172
	[SerializeField]
	protected float minValue;

	// Token: 0x040023D5 RID: 9173
	[SerializeField]
	protected float maxValue = 1f;

	// Token: 0x040023D6 RID: 9174
	[SerializeField]
	protected global::dfProgressFillMode fillMode;

	// Token: 0x040023D7 RID: 9175
	[SerializeField]
	protected RectOffset padding = new RectOffset();

	// Token: 0x040023D8 RID: 9176
	[SerializeField]
	protected bool actAsSlider;
}
