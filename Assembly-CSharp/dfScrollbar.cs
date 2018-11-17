using System;
using UnityEngine;

// Token: 0x020007C2 RID: 1986
[ExecuteInEditMode]
[AddComponentMenu("Daikon Forge/User Interface/Scrollbar")]
[RequireComponent(typeof(BoxCollider))]
[Serializable]
public class dfScrollbar : global::dfControl
{
	// Token: 0x14000050 RID: 80
	// (add) Token: 0x06004400 RID: 17408 RVA: 0x000FCA60 File Offset: 0x000FAC60
	// (remove) Token: 0x06004401 RID: 17409 RVA: 0x000FCA7C File Offset: 0x000FAC7C
	public event global::PropertyChangedEventHandler<float> ValueChanged;

	// Token: 0x17000D11 RID: 3345
	// (get) Token: 0x06004402 RID: 17410 RVA: 0x000FCA98 File Offset: 0x000FAC98
	// (set) Token: 0x06004403 RID: 17411 RVA: 0x000FCAE0 File Offset: 0x000FACE0
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

	// Token: 0x17000D12 RID: 3346
	// (get) Token: 0x06004404 RID: 17412 RVA: 0x000FCB00 File Offset: 0x000FAD00
	// (set) Token: 0x06004405 RID: 17413 RVA: 0x000FCB08 File Offset: 0x000FAD08
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
				this.Value = this.Value;
				this.Invalidate();
				this.doAutoHide();
			}
		}
	}

	// Token: 0x17000D13 RID: 3347
	// (get) Token: 0x06004406 RID: 17414 RVA: 0x000FCB38 File Offset: 0x000FAD38
	// (set) Token: 0x06004407 RID: 17415 RVA: 0x000FCB40 File Offset: 0x000FAD40
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
				this.Value = this.Value;
				this.Invalidate();
				this.doAutoHide();
			}
		}
	}

	// Token: 0x17000D14 RID: 3348
	// (get) Token: 0x06004408 RID: 17416 RVA: 0x000FCB70 File Offset: 0x000FAD70
	// (set) Token: 0x06004409 RID: 17417 RVA: 0x000FCB78 File Offset: 0x000FAD78
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
				this.Value = this.Value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000D15 RID: 3349
	// (get) Token: 0x0600440A RID: 17418 RVA: 0x000FCBB8 File Offset: 0x000FADB8
	// (set) Token: 0x0600440B RID: 17419 RVA: 0x000FCBC0 File Offset: 0x000FADC0
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
				this.Value = this.Value;
				this.Invalidate();
				this.doAutoHide();
			}
		}
	}

	// Token: 0x17000D16 RID: 3350
	// (get) Token: 0x0600440C RID: 17420 RVA: 0x000FCC08 File Offset: 0x000FAE08
	// (set) Token: 0x0600440D RID: 17421 RVA: 0x000FCC10 File Offset: 0x000FAE10
	public float IncrementAmount
	{
		get
		{
			return this.increment;
		}
		set
		{
			value = Mathf.Max(0f, value);
			if (!Mathf.Approximately(value, this.increment))
			{
				this.increment = value;
			}
		}
	}

	// Token: 0x17000D17 RID: 3351
	// (get) Token: 0x0600440E RID: 17422 RVA: 0x000FCC38 File Offset: 0x000FAE38
	// (set) Token: 0x0600440F RID: 17423 RVA: 0x000FCC40 File Offset: 0x000FAE40
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
			}
		}
	}

	// Token: 0x17000D18 RID: 3352
	// (get) Token: 0x06004410 RID: 17424 RVA: 0x000FCC5C File Offset: 0x000FAE5C
	// (set) Token: 0x06004411 RID: 17425 RVA: 0x000FCC64 File Offset: 0x000FAE64
	public float Value
	{
		get
		{
			return this.rawValue;
		}
		set
		{
			value = this.adjustValue(value);
			if (!Mathf.Approximately(value, this.rawValue))
			{
				this.rawValue = value;
				this.OnValueChanged();
			}
			this.updateThumb(this.rawValue);
		}
	}

	// Token: 0x17000D19 RID: 3353
	// (get) Token: 0x06004412 RID: 17426 RVA: 0x000FCC9C File Offset: 0x000FAE9C
	// (set) Token: 0x06004413 RID: 17427 RVA: 0x000FCCA4 File Offset: 0x000FAEA4
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
			}
		}
	}

	// Token: 0x17000D1A RID: 3354
	// (get) Token: 0x06004414 RID: 17428 RVA: 0x000FCCC4 File Offset: 0x000FAEC4
	// (set) Token: 0x06004415 RID: 17429 RVA: 0x000FCCCC File Offset: 0x000FAECC
	public global::dfControl Track
	{
		get
		{
			return this.track;
		}
		set
		{
			if (value != this.track)
			{
				this.track = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000D1B RID: 3355
	// (get) Token: 0x06004416 RID: 17430 RVA: 0x000FCCEC File Offset: 0x000FAEEC
	// (set) Token: 0x06004417 RID: 17431 RVA: 0x000FCCF4 File Offset: 0x000FAEF4
	public global::dfControl IncButton
	{
		get
		{
			return this.incButton;
		}
		set
		{
			if (value != this.incButton)
			{
				this.incButton = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000D1C RID: 3356
	// (get) Token: 0x06004418 RID: 17432 RVA: 0x000FCD14 File Offset: 0x000FAF14
	// (set) Token: 0x06004419 RID: 17433 RVA: 0x000FCD1C File Offset: 0x000FAF1C
	public global::dfControl DecButton
	{
		get
		{
			return this.decButton;
		}
		set
		{
			if (value != this.decButton)
			{
				this.decButton = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000D1D RID: 3357
	// (get) Token: 0x0600441A RID: 17434 RVA: 0x000FCD3C File Offset: 0x000FAF3C
	// (set) Token: 0x0600441B RID: 17435 RVA: 0x000FCD5C File Offset: 0x000FAF5C
	public RectOffset ThumbPadding
	{
		get
		{
			if (this.thumbPadding == null)
			{
				this.thumbPadding = new RectOffset();
			}
			return this.thumbPadding;
		}
		set
		{
			if (this.orientation == global::dfControlOrientation.Horizontal)
			{
				int num = 0;
				value.bottom = num;
				value.top = num;
			}
			else
			{
				int num = 0;
				value.right = num;
				value.left = num;
			}
			if (!object.Equals(value, this.thumbPadding))
			{
				this.thumbPadding = value;
				this.updateThumb(this.rawValue);
			}
		}
	}

	// Token: 0x17000D1E RID: 3358
	// (get) Token: 0x0600441C RID: 17436 RVA: 0x000FCDC0 File Offset: 0x000FAFC0
	// (set) Token: 0x0600441D RID: 17437 RVA: 0x000FCDC8 File Offset: 0x000FAFC8
	public bool AutoHide
	{
		get
		{
			return this.autoHide;
		}
		set
		{
			if (value != this.autoHide)
			{
				this.autoHide = value;
				this.Invalidate();
				this.doAutoHide();
			}
		}
	}

	// Token: 0x0600441E RID: 17438 RVA: 0x000FCDEC File Offset: 0x000FAFEC
	public override Vector2 CalculateMinimumSize()
	{
		Vector2[] array = new Vector2[3];
		if (this.decButton != null)
		{
			array[0] = this.decButton.CalculateMinimumSize();
		}
		if (this.incButton != null)
		{
			array[1] = this.incButton.CalculateMinimumSize();
		}
		if (this.thumb != null)
		{
			array[2] = this.thumb.CalculateMinimumSize();
		}
		Vector2 zero = Vector2.zero;
		if (this.orientation == global::dfControlOrientation.Horizontal)
		{
			zero.x = array[0].x + array[1].x + array[2].x;
			zero.y = Mathf.Max(new float[]
			{
				array[0].y,
				array[1].y,
				array[2].y
			});
		}
		else
		{
			zero.x = Mathf.Max(new float[]
			{
				array[0].x,
				array[1].x,
				array[2].x
			});
			zero.y = array[0].y + array[1].y + array[2].y;
		}
		return Vector2.Max(zero, base.CalculateMinimumSize());
	}

	// Token: 0x17000D1F RID: 3359
	// (get) Token: 0x0600441F RID: 17439 RVA: 0x000FCF74 File Offset: 0x000FB174
	public override bool CanFocus
	{
		get
		{
			return (base.IsEnabled && base.IsVisible) || base.CanFocus;
		}
	}

	// Token: 0x06004420 RID: 17440 RVA: 0x000FCF94 File Offset: 0x000FB194
	protected override void OnRebuildRenderData()
	{
		this.updateThumb(this.rawValue);
		base.OnRebuildRenderData();
	}

	// Token: 0x06004421 RID: 17441 RVA: 0x000FCFA8 File Offset: 0x000FB1A8
	public override void Start()
	{
		base.Start();
		this.attachEvents();
	}

	// Token: 0x06004422 RID: 17442 RVA: 0x000FCFB8 File Offset: 0x000FB1B8
	public override void OnDisable()
	{
		base.OnDisable();
		this.detachEvents();
	}

	// Token: 0x06004423 RID: 17443 RVA: 0x000FCFC8 File Offset: 0x000FB1C8
	public override void OnDestroy()
	{
		base.OnDestroy();
		this.detachEvents();
	}

	// Token: 0x06004424 RID: 17444 RVA: 0x000FCFD8 File Offset: 0x000FB1D8
	private void attachEvents()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		if (this.IncButton != null)
		{
			this.IncButton.MouseDown += this.incrementPressed;
			this.IncButton.MouseHover += this.incrementPressed;
		}
		if (this.DecButton != null)
		{
			this.DecButton.MouseDown += this.decrementPressed;
			this.DecButton.MouseHover += this.decrementPressed;
		}
	}

	// Token: 0x06004425 RID: 17445 RVA: 0x000FD070 File Offset: 0x000FB270
	private void detachEvents()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		if (this.IncButton != null)
		{
			this.IncButton.MouseDown -= this.incrementPressed;
			this.IncButton.MouseHover -= this.incrementPressed;
		}
		if (this.DecButton != null)
		{
			this.DecButton.MouseDown -= this.decrementPressed;
			this.DecButton.MouseHover -= this.decrementPressed;
		}
	}

	// Token: 0x06004426 RID: 17446 RVA: 0x000FD108 File Offset: 0x000FB308
	protected internal override void OnKeyDown(global::dfKeyEventArgs args)
	{
		if (this.Orientation == global::dfControlOrientation.Horizontal)
		{
			if (args.KeyCode == 276)
			{
				this.Value -= this.IncrementAmount;
				args.Use();
				return;
			}
			if (args.KeyCode == 275)
			{
				this.Value += this.IncrementAmount;
				args.Use();
				return;
			}
		}
		else
		{
			if (args.KeyCode == 273)
			{
				this.Value -= this.IncrementAmount;
				args.Use();
				return;
			}
			if (args.KeyCode == 274)
			{
				this.Value += this.IncrementAmount;
				args.Use();
				return;
			}
		}
		base.OnKeyDown(args);
	}

	// Token: 0x06004427 RID: 17447 RVA: 0x000FD1D4 File Offset: 0x000FB3D4
	protected internal override void OnMouseWheel(global::dfMouseEventArgs args)
	{
		this.Value += this.IncrementAmount * -args.WheelDelta;
		args.Use();
		base.Signal("OnMouseWheel", new object[]
		{
			args
		});
	}

	// Token: 0x06004428 RID: 17448 RVA: 0x000FD218 File Offset: 0x000FB418
	protected internal override void OnMouseHover(global::dfMouseEventArgs args)
	{
		bool flag = args.Source == this.incButton || args.Source == this.decButton || args.Source == this.thumb;
		if (flag)
		{
			return;
		}
		if (args.Source != this.track || !args.Buttons.IsSet(global::dfMouseButtons.Left))
		{
			base.OnMouseHover(args);
			return;
		}
		this.updateFromTrackClick(args);
		args.Use();
		base.Signal("OnMouseHover", new object[]
		{
			args
		});
	}

	// Token: 0x06004429 RID: 17449 RVA: 0x000FD2C0 File Offset: 0x000FB4C0
	protected internal override void OnMouseMove(global::dfMouseEventArgs args)
	{
		if (args.Source == this.incButton || args.Source == this.decButton)
		{
			return;
		}
		if ((args.Source != this.track && args.Source != this.thumb) || !args.Buttons.IsSet(global::dfMouseButtons.Left))
		{
			base.OnMouseMove(args);
			return;
		}
		this.Value = Mathf.Max(this.minValue, this.getValueFromMouseEvent(args) - this.scrollSize * 0.5f);
		args.Use();
		base.Signal("OnMouseMove", new object[]
		{
			args
		});
	}

	// Token: 0x0600442A RID: 17450 RVA: 0x000FD380 File Offset: 0x000FB580
	protected internal override void OnMouseDown(global::dfMouseEventArgs args)
	{
		if (args.Buttons.IsSet(global::dfMouseButtons.Left))
		{
			base.Focus();
		}
		if (args.Source == this.incButton || args.Source == this.decButton)
		{
			return;
		}
		if ((args.Source != this.track && args.Source != this.thumb) || !args.Buttons.IsSet(global::dfMouseButtons.Left))
		{
			base.OnMouseDown(args);
			return;
		}
		if (args.Source == this.thumb)
		{
			RaycastHit raycastHit;
			this.thumb.collider.Raycast(args.Ray, ref raycastHit, 1000f);
			Vector3 vector = this.thumb.transform.position + this.thumb.Pivot.TransformToCenter(this.thumb.Size * base.PixelsToUnits());
			this.thumbMouseOffset = vector - raycastHit.point;
		}
		else
		{
			this.updateFromTrackClick(args);
		}
		args.Use();
		base.Signal("OnMouseDown", new object[]
		{
			args
		});
	}

	// Token: 0x0600442B RID: 17451 RVA: 0x000FD4C4 File Offset: 0x000FB6C4
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

	// Token: 0x0600442C RID: 17452 RVA: 0x000FD514 File Offset: 0x000FB714
	protected internal override void OnSizeChanged()
	{
		base.OnSizeChanged();
		this.updateThumb(this.rawValue);
	}

	// Token: 0x0600442D RID: 17453 RVA: 0x000FD528 File Offset: 0x000FB728
	private void doAutoHide()
	{
		if (!this.autoHide || !Application.isPlaying)
		{
			return;
		}
		if (Mathf.CeilToInt(this.ScrollSize) >= Mathf.CeilToInt(this.maxValue - this.minValue))
		{
			base.Hide();
		}
		else
		{
			base.Show();
		}
	}

	// Token: 0x0600442E RID: 17454 RVA: 0x000FD580 File Offset: 0x000FB780
	private void incrementPressed(global::dfControl sender, global::dfMouseEventArgs args)
	{
		if (args.Buttons.IsSet(global::dfMouseButtons.Left))
		{
			this.Value += this.IncrementAmount;
			args.Use();
		}
	}

	// Token: 0x0600442F RID: 17455 RVA: 0x000FD5B8 File Offset: 0x000FB7B8
	private void decrementPressed(global::dfControl sender, global::dfMouseEventArgs args)
	{
		if (args.Buttons.IsSet(global::dfMouseButtons.Left))
		{
			this.Value -= this.IncrementAmount;
			args.Use();
		}
	}

	// Token: 0x06004430 RID: 17456 RVA: 0x000FD5F0 File Offset: 0x000FB7F0
	private void updateFromTrackClick(global::dfMouseEventArgs args)
	{
		float valueFromMouseEvent = this.getValueFromMouseEvent(args);
		if (valueFromMouseEvent > this.rawValue + this.scrollSize)
		{
			this.Value += this.scrollSize;
		}
		else if (valueFromMouseEvent < this.rawValue)
		{
			this.Value -= this.scrollSize;
		}
	}

	// Token: 0x06004431 RID: 17457 RVA: 0x000FD650 File Offset: 0x000FB850
	private float adjustValue(float value)
	{
		float num = Mathf.Max(this.maxValue - this.minValue, 0f);
		float num2 = Mathf.Max(num - this.scrollSize, 0f) + this.minValue;
		float value2 = Mathf.Max(Mathf.Min(num2, value), this.minValue);
		return value2.Quantize(this.stepSize);
	}

	// Token: 0x06004432 RID: 17458 RVA: 0x000FD6B0 File Offset: 0x000FB8B0
	private void updateThumb(float rawValue)
	{
		if (this.controls.Count == 0 || this.thumb == null || this.track == null || !base.IsVisible)
		{
			return;
		}
		float num = this.maxValue - this.minValue;
		if (num <= 0f || num <= this.scrollSize)
		{
			this.thumb.IsVisible = false;
			return;
		}
		this.thumb.IsVisible = true;
		float num2 = (this.orientation != global::dfControlOrientation.Horizontal) ? this.track.Height : this.track.Width;
		float num3 = (this.orientation != global::dfControlOrientation.Horizontal) ? Mathf.Max(this.scrollSize / num * num2, this.thumb.MinimumSize.y) : Mathf.Max(this.scrollSize / num * num2, this.thumb.MinimumSize.x);
		Vector2 size = (this.orientation != global::dfControlOrientation.Horizontal) ? new Vector2(this.thumb.Width, num3) : new Vector2(num3, this.thumb.Height);
		if (this.Orientation == global::dfControlOrientation.Horizontal)
		{
			size.x -= (float)this.thumbPadding.horizontal;
		}
		else
		{
			size.y -= (float)this.thumbPadding.vertical;
		}
		this.thumb.Size = size;
		float num4 = (rawValue - this.minValue) / (num - this.scrollSize);
		float num5 = num4 * (num2 - num3);
		Vector3 vector = (this.orientation != global::dfControlOrientation.Horizontal) ? Vector3.up : Vector3.right;
		Vector3 vector2 = (this.Orientation != global::dfControlOrientation.Horizontal) ? new Vector3((this.track.Width - this.thumb.Width) * 0.5f, 0f) : new Vector3(0f, (this.track.Height - this.thumb.Height) * 0.5f);
		if (this.Orientation == global::dfControlOrientation.Horizontal)
		{
			vector2.x = (float)this.thumbPadding.left;
		}
		else
		{
			vector2.y = (float)this.thumbPadding.top;
		}
		if (this.thumb.Parent == this)
		{
			this.thumb.RelativePosition = this.track.RelativePosition + vector2 + vector * num5;
		}
		else
		{
			this.thumb.RelativePosition = vector * num5 + vector2;
		}
	}

	// Token: 0x06004433 RID: 17459 RVA: 0x000FD968 File Offset: 0x000FBB68
	private float getValueFromMouseEvent(global::dfMouseEventArgs args)
	{
		Vector3[] corners = this.track.GetCorners();
		Vector3 vector = corners[0];
		Vector3 vector2 = corners[(this.orientation != global::dfControlOrientation.Horizontal) ? 2 : 1];
		Plane plane;
		plane..ctor(base.transform.TransformDirection(Vector3.back), vector);
		Ray ray = args.Ray;
		float num = 0f;
		if (!plane.Raycast(ray, ref num))
		{
			return this.rawValue;
		}
		Vector3 vector3 = ray.origin + ray.direction * num;
		if (args.Source == this.thumb)
		{
			vector3 += this.thumbMouseOffset;
		}
		Vector3 vector4 = global::dfScrollbar.closestPoint(vector, vector2, vector3, true);
		float num2 = (vector4 - vector).magnitude / (vector2 - vector).magnitude;
		return this.minValue + (this.maxValue - this.minValue) * num2;
	}

	// Token: 0x06004434 RID: 17460 RVA: 0x000FDA78 File Offset: 0x000FBC78
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

	// Token: 0x04002410 RID: 9232
	[SerializeField]
	protected global::dfAtlas atlas;

	// Token: 0x04002411 RID: 9233
	[SerializeField]
	protected global::dfControlOrientation orientation;

	// Token: 0x04002412 RID: 9234
	[SerializeField]
	protected float rawValue = 1f;

	// Token: 0x04002413 RID: 9235
	[SerializeField]
	protected float minValue;

	// Token: 0x04002414 RID: 9236
	[SerializeField]
	protected float maxValue = 100f;

	// Token: 0x04002415 RID: 9237
	[SerializeField]
	protected float stepSize = 1f;

	// Token: 0x04002416 RID: 9238
	[SerializeField]
	protected float scrollSize = 1f;

	// Token: 0x04002417 RID: 9239
	[SerializeField]
	protected float increment = 1f;

	// Token: 0x04002418 RID: 9240
	[SerializeField]
	protected global::dfControl thumb;

	// Token: 0x04002419 RID: 9241
	[SerializeField]
	protected global::dfControl track;

	// Token: 0x0400241A RID: 9242
	[SerializeField]
	protected global::dfControl incButton;

	// Token: 0x0400241B RID: 9243
	[SerializeField]
	protected global::dfControl decButton;

	// Token: 0x0400241C RID: 9244
	[SerializeField]
	protected RectOffset thumbPadding = new RectOffset();

	// Token: 0x0400241D RID: 9245
	[SerializeField]
	protected bool autoHide;

	// Token: 0x0400241E RID: 9246
	private Vector3 thumbMouseOffset = Vector3.zero;
}
