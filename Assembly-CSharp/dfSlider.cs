using System;
using UnityEngine;

// Token: 0x020007C4 RID: 1988
[AddComponentMenu("Daikon Forge/User Interface/Slider")]
[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider))]
[Serializable]
public class dfSlider : global::dfControl
{
	// Token: 0x14000051 RID: 81
	// (add) Token: 0x06004440 RID: 17472 RVA: 0x000FEC08 File Offset: 0x000FCE08
	// (remove) Token: 0x06004441 RID: 17473 RVA: 0x000FEC24 File Offset: 0x000FCE24
	public event global::PropertyChangedEventHandler<float> ValueChanged;

	// Token: 0x17000D20 RID: 3360
	// (get) Token: 0x06004442 RID: 17474 RVA: 0x000FEC40 File Offset: 0x000FCE40
	// (set) Token: 0x06004443 RID: 17475 RVA: 0x000FEC88 File Offset: 0x000FCE88
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

	// Token: 0x17000D21 RID: 3361
	// (get) Token: 0x06004444 RID: 17476 RVA: 0x000FECA8 File Offset: 0x000FCEA8
	// (set) Token: 0x06004445 RID: 17477 RVA: 0x000FECB0 File Offset: 0x000FCEB0
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

	// Token: 0x17000D22 RID: 3362
	// (get) Token: 0x06004446 RID: 17478 RVA: 0x000FECD0 File Offset: 0x000FCED0
	// (set) Token: 0x06004447 RID: 17479 RVA: 0x000FECD8 File Offset: 0x000FCED8
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

	// Token: 0x17000D23 RID: 3363
	// (get) Token: 0x06004448 RID: 17480 RVA: 0x000FED14 File Offset: 0x000FCF14
	// (set) Token: 0x06004449 RID: 17481 RVA: 0x000FED1C File Offset: 0x000FCF1C
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

	// Token: 0x17000D24 RID: 3364
	// (get) Token: 0x0600444A RID: 17482 RVA: 0x000FED58 File Offset: 0x000FCF58
	// (set) Token: 0x0600444B RID: 17483 RVA: 0x000FED60 File Offset: 0x000FCF60
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

	// Token: 0x17000D25 RID: 3365
	// (get) Token: 0x0600444C RID: 17484 RVA: 0x000FEDA8 File Offset: 0x000FCFA8
	// (set) Token: 0x0600444D RID: 17485 RVA: 0x000FEDB0 File Offset: 0x000FCFB0
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

	// Token: 0x17000D26 RID: 3366
	// (get) Token: 0x0600444E RID: 17486 RVA: 0x000FEDE4 File Offset: 0x000FCFE4
	// (set) Token: 0x0600444F RID: 17487 RVA: 0x000FEDEC File Offset: 0x000FCFEC
	public global::dfControlOrientation Orientation
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

	// Token: 0x17000D27 RID: 3367
	// (get) Token: 0x06004450 RID: 17488 RVA: 0x000FEE14 File Offset: 0x000FD014
	// (set) Token: 0x06004451 RID: 17489 RVA: 0x000FEE1C File Offset: 0x000FD01C
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

	// Token: 0x17000D28 RID: 3368
	// (get) Token: 0x06004452 RID: 17490 RVA: 0x000FEE6C File Offset: 0x000FD06C
	// (set) Token: 0x06004453 RID: 17491 RVA: 0x000FEE74 File Offset: 0x000FD074
	public global::dfControl Thumb
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

	// Token: 0x17000D29 RID: 3369
	// (get) Token: 0x06004454 RID: 17492 RVA: 0x000FEEAC File Offset: 0x000FD0AC
	// (set) Token: 0x06004455 RID: 17493 RVA: 0x000FEEB4 File Offset: 0x000FD0B4
	public global::dfControl Progress
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

	// Token: 0x17000D2A RID: 3370
	// (get) Token: 0x06004456 RID: 17494 RVA: 0x000FEEEC File Offset: 0x000FD0EC
	// (set) Token: 0x06004457 RID: 17495 RVA: 0x000FEEF4 File Offset: 0x000FD0F4
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

	// Token: 0x17000D2B RID: 3371
	// (get) Token: 0x06004458 RID: 17496 RVA: 0x000FEF10 File Offset: 0x000FD110
	// (set) Token: 0x06004459 RID: 17497 RVA: 0x000FEF30 File Offset: 0x000FD130
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

	// Token: 0x17000D2C RID: 3372
	// (get) Token: 0x0600445A RID: 17498 RVA: 0x000FEF68 File Offset: 0x000FD168
	// (set) Token: 0x0600445B RID: 17499 RVA: 0x000FEF70 File Offset: 0x000FD170
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

	// Token: 0x17000D2D RID: 3373
	// (get) Token: 0x0600445C RID: 17500 RVA: 0x000FEF9C File Offset: 0x000FD19C
	// (set) Token: 0x0600445D RID: 17501 RVA: 0x000FEFA4 File Offset: 0x000FD1A4
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

	// Token: 0x0600445E RID: 17502 RVA: 0x000FEFC8 File Offset: 0x000FD1C8
	protected internal override void OnKeyDown(global::dfKeyEventArgs args)
	{
		if (this.Orientation == global::dfControlOrientation.Horizontal)
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

	// Token: 0x0600445F RID: 17503 RVA: 0x000FF094 File Offset: 0x000FD294
	public override void Start()
	{
		base.Start();
		this.updateValueIndicators(this.rawValue);
	}

	// Token: 0x06004460 RID: 17504 RVA: 0x000FF0A8 File Offset: 0x000FD2A8
	public override void OnEnable()
	{
		if (this.size.magnitude < 1.401298E-45f)
		{
			this.size = new Vector2(100f, 25f);
		}
		base.OnEnable();
		this.updateValueIndicators(this.rawValue);
	}

	// Token: 0x06004461 RID: 17505 RVA: 0x000FF0F4 File Offset: 0x000FD2F4
	protected internal override void OnMouseWheel(global::dfMouseEventArgs args)
	{
		int num = (this.orientation != global::dfControlOrientation.Horizontal) ? 1 : -1;
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

	// Token: 0x06004462 RID: 17506 RVA: 0x000FF168 File Offset: 0x000FD368
	protected internal override void OnMouseMove(global::dfMouseEventArgs args)
	{
		if (!args.Buttons.IsSet(global::dfMouseButtons.Left))
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

	// Token: 0x06004463 RID: 17507 RVA: 0x000FF1D0 File Offset: 0x000FD3D0
	protected internal override void OnMouseDown(global::dfMouseEventArgs args)
	{
		if (!args.Buttons.IsSet(global::dfMouseButtons.Left))
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

	// Token: 0x06004464 RID: 17508 RVA: 0x000FF240 File Offset: 0x000FD440
	protected internal override void OnSizeChanged()
	{
		base.OnSizeChanged();
		this.updateValueIndicators(this.rawValue);
	}

	// Token: 0x06004465 RID: 17509 RVA: 0x000FF254 File Offset: 0x000FD454
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

	// Token: 0x17000D2E RID: 3374
	// (get) Token: 0x06004466 RID: 17510 RVA: 0x000FF2B0 File Offset: 0x000FD4B0
	public override bool CanFocus
	{
		get
		{
			return (base.IsEnabled && base.IsVisible) || base.CanFocus;
		}
	}

	// Token: 0x06004467 RID: 17511 RVA: 0x000FF2D0 File Offset: 0x000FD4D0
	protected override void OnRebuildRenderData()
	{
		if (this.Atlas == null)
		{
			return;
		}
		this.renderData.Material = this.Atlas.Material;
		this.renderBackground();
	}

	// Token: 0x06004468 RID: 17512 RVA: 0x000FF30C File Offset: 0x000FD50C
	protected internal virtual void renderBackground()
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
		Color32 color = base.ApplyOpacity((!base.IsEnabled) ? this.disabledColor : this.color);
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

	// Token: 0x06004469 RID: 17513 RVA: 0x000FF414 File Offset: 0x000FD614
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
			if (this.orientation == global::dfControlOrientation.Vertical || this.rightToLeft)
			{
				position = endPoints[1] + -vector.normalized * num2 + vector2;
			}
			this.thumb.Pivot = global::dfPivotPoint.MiddleCenter;
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
		global::dfSprite dfSprite = this.fillIndicator as global::dfSprite;
		if (dfSprite != null && this.fillMode == global::dfProgressFillMode.Fill)
		{
			dfSprite.FillAmount = num3;
		}
		else if (this.orientation == global::dfControlOrientation.Horizontal)
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

	// Token: 0x0600446A RID: 17514 RVA: 0x000FF614 File Offset: 0x000FD814
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
		Vector3 vector3 = global::dfSlider.closestPoint(vector, vector2, test, true);
		float num2 = (vector3 - vector).magnitude / (vector2 - vector).magnitude;
		float num3 = this.minValue + (this.maxValue - this.minValue) * num2;
		if (this.orientation == global::dfControlOrientation.Vertical || this.rightToLeft)
		{
			num3 = this.maxValue - num3;
		}
		return num3;
	}

	// Token: 0x0600446B RID: 17515 RVA: 0x000FF70C File Offset: 0x000FD90C
	private Vector3[] getEndPoints(bool convertToWorld = false)
	{
		Vector3 vector = this.pivot.TransformToUpperLeft(base.Size);
		Vector3 vector2;
		vector2..ctor(vector.x, vector.y - this.size.y * 0.5f);
		Vector3 vector3 = vector2 + new Vector3(this.size.x, 0f);
		if (this.orientation == global::dfControlOrientation.Vertical)
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

	// Token: 0x0600446C RID: 17516 RVA: 0x000FF818 File Offset: 0x000FDA18
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

	// Token: 0x04002426 RID: 9254
	[SerializeField]
	protected global::dfAtlas atlas;

	// Token: 0x04002427 RID: 9255
	[SerializeField]
	protected string backgroundSprite;

	// Token: 0x04002428 RID: 9256
	[SerializeField]
	protected global::dfControlOrientation orientation;

	// Token: 0x04002429 RID: 9257
	[SerializeField]
	protected float rawValue = 10f;

	// Token: 0x0400242A RID: 9258
	[SerializeField]
	protected float minValue;

	// Token: 0x0400242B RID: 9259
	[SerializeField]
	protected float maxValue = 100f;

	// Token: 0x0400242C RID: 9260
	[SerializeField]
	protected float stepSize = 1f;

	// Token: 0x0400242D RID: 9261
	[SerializeField]
	protected float scrollSize = 1f;

	// Token: 0x0400242E RID: 9262
	[SerializeField]
	protected global::dfControl thumb;

	// Token: 0x0400242F RID: 9263
	[SerializeField]
	protected global::dfControl fillIndicator;

	// Token: 0x04002430 RID: 9264
	[SerializeField]
	protected global::dfProgressFillMode fillMode = global::dfProgressFillMode.Fill;

	// Token: 0x04002431 RID: 9265
	[SerializeField]
	protected RectOffset fillPadding = new RectOffset();

	// Token: 0x04002432 RID: 9266
	[SerializeField]
	protected Vector2 thumbOffset = Vector2.zero;

	// Token: 0x04002433 RID: 9267
	[SerializeField]
	protected bool rightToLeft;
}
