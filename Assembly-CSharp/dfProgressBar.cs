using System;
using UnityEngine;

// Token: 0x020006E8 RID: 1768
[AddComponentMenu("Daikon Forge/User Interface/Progress Bar")]
[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider))]
[Serializable]
public class dfProgressBar : dfControl
{
	// Token: 0x1400004E RID: 78
	// (add) Token: 0x06003F3A RID: 16186 RVA: 0x000F0550 File Offset: 0x000EE750
	// (remove) Token: 0x06003F3B RID: 16187 RVA: 0x000F056C File Offset: 0x000EE76C
	public event PropertyChangedEventHandler<float> ValueChanged;

	// Token: 0x17000C63 RID: 3171
	// (get) Token: 0x06003F3C RID: 16188 RVA: 0x000F0588 File Offset: 0x000EE788
	// (set) Token: 0x06003F3D RID: 16189 RVA: 0x000F05D0 File Offset: 0x000EE7D0
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

	// Token: 0x17000C64 RID: 3172
	// (get) Token: 0x06003F3E RID: 16190 RVA: 0x000F05F0 File Offset: 0x000EE7F0
	// (set) Token: 0x06003F3F RID: 16191 RVA: 0x000F05F8 File Offset: 0x000EE7F8
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

	// Token: 0x17000C65 RID: 3173
	// (get) Token: 0x06003F40 RID: 16192 RVA: 0x000F0620 File Offset: 0x000EE820
	// (set) Token: 0x06003F41 RID: 16193 RVA: 0x000F0628 File Offset: 0x000EE828
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

	// Token: 0x17000C66 RID: 3174
	// (get) Token: 0x06003F42 RID: 16194 RVA: 0x000F0648 File Offset: 0x000EE848
	// (set) Token: 0x06003F43 RID: 16195 RVA: 0x000F0650 File Offset: 0x000EE850
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

	// Token: 0x17000C67 RID: 3175
	// (get) Token: 0x06003F44 RID: 16196 RVA: 0x000F0688 File Offset: 0x000EE888
	// (set) Token: 0x06003F45 RID: 16197 RVA: 0x000F0690 File Offset: 0x000EE890
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

	// Token: 0x17000C68 RID: 3176
	// (get) Token: 0x06003F46 RID: 16198 RVA: 0x000F06CC File Offset: 0x000EE8CC
	// (set) Token: 0x06003F47 RID: 16199 RVA: 0x000F06D4 File Offset: 0x000EE8D4
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

	// Token: 0x17000C69 RID: 3177
	// (get) Token: 0x06003F48 RID: 16200 RVA: 0x000F0710 File Offset: 0x000EE910
	// (set) Token: 0x06003F49 RID: 16201 RVA: 0x000F0718 File Offset: 0x000EE918
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

	// Token: 0x17000C6A RID: 3178
	// (get) Token: 0x06003F4A RID: 16202 RVA: 0x000F0754 File Offset: 0x000EE954
	// (set) Token: 0x06003F4B RID: 16203 RVA: 0x000F075C File Offset: 0x000EE95C
	public dfProgressFillMode FillMode
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

	// Token: 0x17000C6B RID: 3179
	// (get) Token: 0x06003F4C RID: 16204 RVA: 0x000F0778 File Offset: 0x000EE978
	// (set) Token: 0x06003F4D RID: 16205 RVA: 0x000F0798 File Offset: 0x000EE998
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

	// Token: 0x17000C6C RID: 3180
	// (get) Token: 0x06003F4E RID: 16206 RVA: 0x000F07B8 File Offset: 0x000EE9B8
	// (set) Token: 0x06003F4F RID: 16207 RVA: 0x000F07C0 File Offset: 0x000EE9C0
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

	// Token: 0x06003F50 RID: 16208 RVA: 0x000F07CC File Offset: 0x000EE9CC
	protected internal override void OnMouseWheel(dfMouseEventArgs args)
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

	// Token: 0x06003F51 RID: 16209 RVA: 0x000F0848 File Offset: 0x000EEA48
	protected internal override void OnMouseMove(dfMouseEventArgs args)
	{
		try
		{
			if (this.actAsSlider)
			{
				if (args.Buttons.IsSet(dfMouseButtons.Left))
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

	// Token: 0x06003F52 RID: 16210 RVA: 0x000F08B8 File Offset: 0x000EEAB8
	protected internal override void OnMouseDown(dfMouseEventArgs args)
	{
		try
		{
			if (this.actAsSlider)
			{
				if (args.Buttons.IsSet(dfMouseButtons.Left))
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

	// Token: 0x06003F53 RID: 16211 RVA: 0x000F0930 File Offset: 0x000EEB30
	protected internal override void OnKeyDown(dfKeyEventArgs args)
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

	// Token: 0x06003F54 RID: 16212 RVA: 0x000F09DC File Offset: 0x000EEBDC
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

	// Token: 0x06003F55 RID: 16213 RVA: 0x000F0A2C File Offset: 0x000EEC2C
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

	// Token: 0x06003F56 RID: 16214 RVA: 0x000F0A70 File Offset: 0x000EEC70
	private void renderProgressFill()
	{
		if (this.Atlas == null)
		{
			return;
		}
		dfAtlas.ItemInfo itemInfo = this.Atlas[this.progressSprite];
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
		dfProgressFillMode dfProgressFillMode = this.fillMode;
		if (dfProgressFillMode != dfProgressFillMode.Stretch || size.x * num2 < (float)itemInfo.border.horizontal)
		{
		}
		if (dfProgressFillMode == dfProgressFillMode.Fill)
		{
			fillAmount = num2;
		}
		else
		{
			size.x = Mathf.Max((float)itemInfo.border.horizontal, size.x * num2);
		}
		Color32 color = base.ApplyOpacity((!base.IsEnabled) ? base.DisabledColor : this.ProgressColor);
		dfSprite.RenderOptions options = new dfSprite.RenderOptions
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
			dfSprite.renderSprite(this.renderData, options);
		}
		else
		{
			dfSlicedSprite.renderSprite(this.renderData, options);
		}
	}

	// Token: 0x06003F57 RID: 16215 RVA: 0x000F0C54 File Offset: 0x000EEE54
	private void renderBackground()
	{
		if (this.Atlas == null)
		{
			return;
		}
		dfAtlas.ItemInfo itemInfo = this.Atlas[this.backgroundSprite];
		if (itemInfo == null)
		{
			return;
		}
		Color32 color = base.ApplyOpacity((!base.IsEnabled) ? base.DisabledColor : base.Color);
		dfSprite.RenderOptions options = new dfSprite.RenderOptions
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
			dfSprite.renderSprite(this.renderData, options);
		}
		else
		{
			dfSlicedSprite.renderSprite(this.renderData, options);
		}
	}

	// Token: 0x06003F58 RID: 16216 RVA: 0x000F0D5C File Offset: 0x000EEF5C
	private float getValueFromMouseEvent(dfMouseEventArgs args)
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
		Vector3 vector3 = dfProgressBar.closestPoint(vector, vector2, test, true);
		float num2 = (vector3 - vector).magnitude / (vector2 - vector).magnitude;
		return this.minValue + (this.maxValue - this.minValue) * num2;
	}

	// Token: 0x06003F59 RID: 16217 RVA: 0x000F0E30 File Offset: 0x000EF030
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

	// Token: 0x06003F5A RID: 16218 RVA: 0x000F0F04 File Offset: 0x000EF104
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

	// Token: 0x06003F5B RID: 16219 RVA: 0x000F0F70 File Offset: 0x000EF170
	private void setDefaultSize(string spriteName)
	{
		if (this.Atlas == null)
		{
			return;
		}
		dfAtlas.ItemInfo itemInfo = this.Atlas[spriteName];
		if (this.size == Vector2.zero && itemInfo != null)
		{
			base.Size = itemInfo.sizeInPixels;
		}
	}

	// Token: 0x040021C6 RID: 8646
	[SerializeField]
	protected dfAtlas atlas;

	// Token: 0x040021C7 RID: 8647
	[SerializeField]
	protected string backgroundSprite;

	// Token: 0x040021C8 RID: 8648
	[SerializeField]
	protected string progressSprite;

	// Token: 0x040021C9 RID: 8649
	[SerializeField]
	protected Color32 progressColor = UnityEngine.Color.white;

	// Token: 0x040021CA RID: 8650
	[SerializeField]
	protected float rawValue = 0.25f;

	// Token: 0x040021CB RID: 8651
	[SerializeField]
	protected float minValue;

	// Token: 0x040021CC RID: 8652
	[SerializeField]
	protected float maxValue = 1f;

	// Token: 0x040021CD RID: 8653
	[SerializeField]
	protected dfProgressFillMode fillMode;

	// Token: 0x040021CE RID: 8654
	[SerializeField]
	protected RectOffset padding = new RectOffset();

	// Token: 0x040021CF RID: 8655
	[SerializeField]
	protected bool actAsSlider;
}
