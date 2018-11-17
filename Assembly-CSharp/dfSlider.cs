using System;
using UnityEngine;

// Token: 0x020006F2 RID: 1778
[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider))]
[AddComponentMenu("Daikon Forge/User Interface/Slider")]
[Serializable]
public class dfSlider : dfControl
{
	// Token: 0x14000051 RID: 81
	// (add) Token: 0x06004024 RID: 16420 RVA: 0x000F6004 File Offset: 0x000F4204
	// (remove) Token: 0x06004025 RID: 16421 RVA: 0x000F6020 File Offset: 0x000F4220
	public event PropertyChangedEventHandler<float> ValueChanged;

	// Token: 0x17000C9C RID: 3228
	// (get) Token: 0x06004026 RID: 16422 RVA: 0x000F603C File Offset: 0x000F423C
	// (set) Token: 0x06004027 RID: 16423 RVA: 0x000F6084 File Offset: 0x000F4284
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

	// Token: 0x17000C9D RID: 3229
	// (get) Token: 0x06004028 RID: 16424 RVA: 0x000F60A4 File Offset: 0x000F42A4
	// (set) Token: 0x06004029 RID: 16425 RVA: 0x000F60AC File Offset: 0x000F42AC
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
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000C9E RID: 3230
	// (get) Token: 0x0600402A RID: 16426 RVA: 0x000F60CC File Offset: 0x000F42CC
	// (set) Token: 0x0600402B RID: 16427 RVA: 0x000F60D4 File Offset: 0x000F42D4
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

	// Token: 0x17000C9F RID: 3231
	// (get) Token: 0x0600402C RID: 16428 RVA: 0x000F6110 File Offset: 0x000F4310
	// (set) Token: 0x0600402D RID: 16429 RVA: 0x000F6118 File Offset: 0x000F4318
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

	// Token: 0x17000CA0 RID: 3232
	// (get) Token: 0x0600402E RID: 16430 RVA: 0x000F6154 File Offset: 0x000F4354
	// (set) Token: 0x0600402F RID: 16431 RVA: 0x000F615C File Offset: 0x000F435C
	public float StepSize
	{
		get
		{
			return this.stepSize;
		}
		set
		{
			value = Mathf.Max(0f, value);
			if (value != this.stepSize)
			{
				this.stepSize = value;
				this.Value = this.rawValue.Quantize(value);
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CA1 RID: 3233
	// (get) Token: 0x06004030 RID: 16432 RVA: 0x000F61A4 File Offset: 0x000F43A4
	// (set) Token: 0x06004031 RID: 16433 RVA: 0x000F61AC File Offset: 0x000F43AC
	public float ScrollSize
	{
		get
		{
			return this.scrollSize;
		}
		set
		{
			value = Mathf.Max(0f, value);
			if (value != this.scrollSize)
			{
				this.scrollSize = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CA2 RID: 3234
	// (get) Token: 0x06004032 RID: 16434 RVA: 0x000F61E0 File Offset: 0x000F43E0
	// (set) Token: 0x06004033 RID: 16435 RVA: 0x000F61E8 File Offset: 0x000F43E8
	public dfControlOrientation Orientation
	{
		get
		{
			return this.orientation;
		}
		set
		{
			if (value != this.orientation)
			{
				this.orientation = value;
				this.Invalidate();
				this.updateValueIndicators(this.rawValue);
			}
		}
	}

	// Token: 0x17000CA3 RID: 3235
	// (get) Token: 0x06004034 RID: 16436 RVA: 0x000F6210 File Offset: 0x000F4410
	// (set) Token: 0x06004035 RID: 16437 RVA: 0x000F6218 File Offset: 0x000F4418
	public float Value
	{
		get
		{
			return this.rawValue;
		}
		set
		{
			value = Mathf.Max(this.minValue, Mathf.Min(this.maxValue, value)).Quantize(this.stepSize);
			if (!Mathf.Approximately(value, this.rawValue))
			{
				this.rawValue = value;
				this.OnValueChanged();
			}
		}
	}

	// Token: 0x17000CA4 RID: 3236
	// (get) Token: 0x06004036 RID: 16438 RVA: 0x000F6268 File Offset: 0x000F4468
	// (set) Token: 0x06004037 RID: 16439 RVA: 0x000F6270 File Offset: 0x000F4470
	public dfControl Thumb
	{
		get
		{
			return this.thumb;
		}
		set
		{
			if (value != this.thumb)
			{
				this.thumb = value;
				this.Invalidate();
				this.updateValueIndicators(this.rawValue);
			}
		}
	}

	// Token: 0x17000CA5 RID: 3237
	// (get) Token: 0x06004038 RID: 16440 RVA: 0x000F62A8 File Offset: 0x000F44A8
	// (set) Token: 0x06004039 RID: 16441 RVA: 0x000F62B0 File Offset: 0x000F44B0
	public dfControl Progress
	{
		get
		{
			return this.fillIndicator;
		}
		set
		{
			if (value != this.fillIndicator)
			{
				this.fillIndicator = value;
				this.Invalidate();
				this.updateValueIndicators(this.rawValue);
			}
		}
	}

	// Token: 0x17000CA6 RID: 3238
	// (get) Token: 0x0600403A RID: 16442 RVA: 0x000F62E8 File Offset: 0x000F44E8
	// (set) Token: 0x0600403B RID: 16443 RVA: 0x000F62F0 File Offset: 0x000F44F0
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

	// Token: 0x17000CA7 RID: 3239
	// (get) Token: 0x0600403C RID: 16444 RVA: 0x000F630C File Offset: 0x000F450C
	// (set) Token: 0x0600403D RID: 16445 RVA: 0x000F632C File Offset: 0x000F452C
	public RectOffset FillPadding
	{
		get
		{
			if (this.fillPadding == null)
			{
				this.fillPadding = new RectOffset();
			}
			return this.fillPadding;
		}
		set
		{
			if (!object.Equals(value, this.fillPadding))
			{
				this.fillPadding = value;
				this.updateValueIndicators(this.rawValue);
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CA8 RID: 3240
	// (get) Token: 0x0600403E RID: 16446 RVA: 0x000F6364 File Offset: 0x000F4564
	// (set) Token: 0x0600403F RID: 16447 RVA: 0x000F636C File Offset: 0x000F456C
	public Vector2 ThumbOffset
	{
		get
		{
			return this.thumbOffset;
		}
		set
		{
			if (Vector2.Distance(value, this.thumbOffset) > 1.401298E-45f)
			{
				this.thumbOffset = value;
				this.updateValueIndicators(this.rawValue);
			}
		}
	}

	// Token: 0x17000CA9 RID: 3241
	// (get) Token: 0x06004040 RID: 16448 RVA: 0x000F6398 File Offset: 0x000F4598
	// (set) Token: 0x06004041 RID: 16449 RVA: 0x000F63A0 File Offset: 0x000F45A0
	public bool RightToLeft
	{
		get
		{
			return this.rightToLeft;
		}
		set
		{
			if (value != this.rightToLeft)
			{
				this.rightToLeft = value;
				this.updateValueIndicators(this.rawValue);
			}
		}
	}

	// Token: 0x06004042 RID: 16450 RVA: 0x000F63C4 File Offset: 0x000F45C4
	protected internal override void OnKeyDown(dfKeyEventArgs args)
	{
		if (this.Orientation == dfControlOrientation.Horizontal)
		{
			if (args.KeyCode == 276)
			{
				this.Value -= this.ScrollSize;
				args.Use();
				return;
			}
			if (args.KeyCode == 275)
			{
				this.Value += this.ScrollSize;
				args.Use();
				return;
			}
		}
		else
		{
			if (args.KeyCode == 273)
			{
				this.Value -= this.ScrollSize;
				args.Use();
				return;
			}
			if (args.KeyCode == 274)
			{
				this.Value += this.ScrollSize;
				args.Use();
				return;
			}
		}
		base.OnKeyDown(args);
	}

	// Token: 0x06004043 RID: 16451 RVA: 0x000F6490 File Offset: 0x000F4690
	public override void Start()
	{
		base.Start();
		this.updateValueIndicators(this.rawValue);
	}

	// Token: 0x06004044 RID: 16452 RVA: 0x000F64A4 File Offset: 0x000F46A4
	public override void OnEnable()
	{
		if (this.size.magnitude < 1.401298E-45f)
		{
			this.size = new Vector2(100f, 25f);
		}
		base.OnEnable();
		this.updateValueIndicators(this.rawValue);
	}

	// Token: 0x06004045 RID: 16453 RVA: 0x000F64F0 File Offset: 0x000F46F0
	protected internal override void OnMouseWheel(dfMouseEventArgs args)
	{
		int num = (this.orientation != dfControlOrientation.Horizontal) ? 1 : -1;
		this.Value += this.scrollSize * args.WheelDelta * (float)num;
		args.Use();
		base.Signal("OnMouseWheel", new object[]
		{
			args
		});
		base.RaiseEvent("MouseWheel", new object[]
		{
			this,
			args
		});
	}

	// Token: 0x06004046 RID: 16454 RVA: 0x000F6564 File Offset: 0x000F4764
	protected internal override void OnMouseMove(dfMouseEventArgs args)
	{
		if (!args.Buttons.IsSet(dfMouseButtons.Left))
		{
			base.OnMouseMove(args);
			return;
		}
		this.Value = this.getValueFromMouseEvent(args);
		args.Use();
		base.Signal("OnMouseMove", new object[]
		{
			args
		});
		base.RaiseEvent("MouseMove", new object[]
		{
			this,
			args
		});
	}

	// Token: 0x06004047 RID: 16455 RVA: 0x000F65CC File Offset: 0x000F47CC
	protected internal override void OnMouseDown(dfMouseEventArgs args)
	{
		if (!args.Buttons.IsSet(dfMouseButtons.Left))
		{
			base.OnMouseMove(args);
			return;
		}
		base.Focus();
		this.Value = this.getValueFromMouseEvent(args);
		args.Use();
		base.Signal("OnMouseDown", new object[]
		{
			args
		});
		base.RaiseEvent("MouseDown", new object[]
		{
			this,
			args
		});
	}

	// Token: 0x06004048 RID: 16456 RVA: 0x000F663C File Offset: 0x000F483C
	protected internal override void OnSizeChanged()
	{
		base.OnSizeChanged();
		this.updateValueIndicators(this.rawValue);
	}

	// Token: 0x06004049 RID: 16457 RVA: 0x000F6650 File Offset: 0x000F4850
	protected internal virtual void OnValueChanged()
	{
		this.Invalidate();
		this.updateValueIndicators(this.rawValue);
		base.SignalHierarchy("OnValueChanged", new object[]
		{
			this.Value
		});
		if (this.ValueChanged != null)
		{
			this.ValueChanged(this, this.Value);
		}
	}

	// Token: 0x17000CAA RID: 3242
	// (get) Token: 0x0600404A RID: 16458 RVA: 0x000F66AC File Offset: 0x000F48AC
	public override bool CanFocus
	{
		get
		{
			return (base.IsEnabled && base.IsVisible) || base.CanFocus;
		}
	}

	// Token: 0x0600404B RID: 16459 RVA: 0x000F66CC File Offset: 0x000F48CC
	protected override void OnRebuildRenderData()
	{
		if (this.Atlas == null)
		{
			return;
		}
		this.renderData.Material = this.Atlas.Material;
		this.renderBackground();
	}

	// Token: 0x0600404C RID: 16460 RVA: 0x000F6708 File Offset: 0x000F4908
	protected internal virtual void renderBackground()
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
		Color32 color = base.ApplyOpacity((!base.IsEnabled) ? this.disabledColor : this.color);
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

	// Token: 0x0600404D RID: 16461 RVA: 0x000F6810 File Offset: 0x000F4A10
	private void updateValueIndicators(float rawValue)
	{
		if (this.thumb != null)
		{
			Vector3[] endPoints = this.getEndPoints(true);
			Vector3 vector = endPoints[1] - endPoints[0];
			float num = this.maxValue - this.minValue;
			float num2 = (rawValue - this.minValue) / num * vector.magnitude;
			Vector3 vector2 = this.thumbOffset * base.PixelsToUnits();
			Vector3 position = endPoints[0] + vector.normalized * num2 + vector2;
			if (this.orientation == dfControlOrientation.Vertical || this.rightToLeft)
			{
				position = endPoints[1] + -vector.normalized * num2 + vector2;
			}
			this.thumb.Pivot = dfPivotPoint.MiddleCenter;
			this.thumb.transform.position = position;
		}
		if (this.fillIndicator == null)
		{
			return;
		}
		RectOffset rectOffset = this.FillPadding;
		float num3 = (rawValue - this.minValue) / (this.maxValue - this.minValue);
		Vector3 relativePosition;
		relativePosition..ctor((float)rectOffset.left, (float)rectOffset.top);
		Vector2 size = this.size - new Vector2((float)rectOffset.horizontal, (float)rectOffset.vertical);
		dfSprite dfSprite = this.fillIndicator as dfSprite;
		if (dfSprite != null && this.fillMode == dfProgressFillMode.Fill)
		{
			dfSprite.FillAmount = num3;
		}
		else if (this.orientation == dfControlOrientation.Horizontal)
		{
			size.x = base.Width * num3 - (float)rectOffset.horizontal;
		}
		else
		{
			size.y = base.Height * num3 - (float)rectOffset.vertical;
		}
		this.fillIndicator.Size = size;
		this.fillIndicator.RelativePosition = relativePosition;
	}

	// Token: 0x0600404E RID: 16462 RVA: 0x000F6A10 File Offset: 0x000F4C10
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
		Vector3 vector3 = dfSlider.closestPoint(vector, vector2, test, true);
		float num2 = (vector3 - vector).magnitude / (vector2 - vector).magnitude;
		float num3 = this.minValue + (this.maxValue - this.minValue) * num2;
		if (this.orientation == dfControlOrientation.Vertical || this.rightToLeft)
		{
			num3 = this.maxValue - num3;
		}
		return num3;
	}

	// Token: 0x0600404F RID: 16463 RVA: 0x000F6B08 File Offset: 0x000F4D08
	private Vector3[] getEndPoints(bool convertToWorld = false)
	{
		Vector3 vector = this.pivot.TransformToUpperLeft(base.Size);
		Vector3 vector2;
		vector2..ctor(vector.x, vector.y - this.size.y * 0.5f);
		Vector3 vector3 = vector2 + new Vector3(this.size.x, 0f);
		if (this.orientation == dfControlOrientation.Vertical)
		{
			vector2..ctor(vector.x + this.size.x * 0.5f, vector.y);
			vector3 = vector2 - new Vector3(0f, this.size.y);
		}
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

	// Token: 0x06004050 RID: 16464 RVA: 0x000F6C14 File Offset: 0x000F4E14
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

	// Token: 0x0400221D RID: 8733
	[SerializeField]
	protected dfAtlas atlas;

	// Token: 0x0400221E RID: 8734
	[SerializeField]
	protected string backgroundSprite;

	// Token: 0x0400221F RID: 8735
	[SerializeField]
	protected dfControlOrientation orientation;

	// Token: 0x04002220 RID: 8736
	[SerializeField]
	protected float rawValue = 10f;

	// Token: 0x04002221 RID: 8737
	[SerializeField]
	protected float minValue;

	// Token: 0x04002222 RID: 8738
	[SerializeField]
	protected float maxValue = 100f;

	// Token: 0x04002223 RID: 8739
	[SerializeField]
	protected float stepSize = 1f;

	// Token: 0x04002224 RID: 8740
	[SerializeField]
	protected float scrollSize = 1f;

	// Token: 0x04002225 RID: 8741
	[SerializeField]
	protected dfControl thumb;

	// Token: 0x04002226 RID: 8742
	[SerializeField]
	protected dfControl fillIndicator;

	// Token: 0x04002227 RID: 8743
	[SerializeField]
	protected dfProgressFillMode fillMode = dfProgressFillMode.Fill;

	// Token: 0x04002228 RID: 8744
	[SerializeField]
	protected RectOffset fillPadding = new RectOffset();

	// Token: 0x04002229 RID: 8745
	[SerializeField]
	protected Vector2 thumbOffset = Vector2.zero;

	// Token: 0x0400222A RID: 8746
	[SerializeField]
	protected bool rightToLeft;
}
