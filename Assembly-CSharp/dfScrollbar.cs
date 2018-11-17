using System;
using UnityEngine;

// Token: 0x020006F0 RID: 1776
[RequireComponent(typeof(BoxCollider))]
[AddComponentMenu("Daikon Forge/User Interface/Scrollbar")]
[ExecuteInEditMode]
[Serializable]
public class dfScrollbar : dfControl
{
	// Token: 0x14000050 RID: 80
	// (add) Token: 0x06003FE4 RID: 16356 RVA: 0x000F3E5C File Offset: 0x000F205C
	// (remove) Token: 0x06003FE5 RID: 16357 RVA: 0x000F3E78 File Offset: 0x000F2078
	public event PropertyChangedEventHandler<float> ValueChanged;

	// Token: 0x17000C8D RID: 3213
	// (get) Token: 0x06003FE6 RID: 16358 RVA: 0x000F3E94 File Offset: 0x000F2094
	// (set) Token: 0x06003FE7 RID: 16359 RVA: 0x000F3EDC File Offset: 0x000F20DC
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

	// Token: 0x17000C8E RID: 3214
	// (get) Token: 0x06003FE8 RID: 16360 RVA: 0x000F3EFC File Offset: 0x000F20FC
	// (set) Token: 0x06003FE9 RID: 16361 RVA: 0x000F3F04 File Offset: 0x000F2104
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

	// Token: 0x17000C8F RID: 3215
	// (get) Token: 0x06003FEA RID: 16362 RVA: 0x000F3F34 File Offset: 0x000F2134
	// (set) Token: 0x06003FEB RID: 16363 RVA: 0x000F3F3C File Offset: 0x000F213C
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

	// Token: 0x17000C90 RID: 3216
	// (get) Token: 0x06003FEC RID: 16364 RVA: 0x000F3F6C File Offset: 0x000F216C
	// (set) Token: 0x06003FED RID: 16365 RVA: 0x000F3F74 File Offset: 0x000F2174
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

	// Token: 0x17000C91 RID: 3217
	// (get) Token: 0x06003FEE RID: 16366 RVA: 0x000F3FB4 File Offset: 0x000F21B4
	// (set) Token: 0x06003FEF RID: 16367 RVA: 0x000F3FBC File Offset: 0x000F21BC
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

	// Token: 0x17000C92 RID: 3218
	// (get) Token: 0x06003FF0 RID: 16368 RVA: 0x000F4004 File Offset: 0x000F2204
	// (set) Token: 0x06003FF1 RID: 16369 RVA: 0x000F400C File Offset: 0x000F220C
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

	// Token: 0x17000C93 RID: 3219
	// (get) Token: 0x06003FF2 RID: 16370 RVA: 0x000F4034 File Offset: 0x000F2234
	// (set) Token: 0x06003FF3 RID: 16371 RVA: 0x000F403C File Offset: 0x000F223C
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
			}
		}
	}

	// Token: 0x17000C94 RID: 3220
	// (get) Token: 0x06003FF4 RID: 16372 RVA: 0x000F4058 File Offset: 0x000F2258
	// (set) Token: 0x06003FF5 RID: 16373 RVA: 0x000F4060 File Offset: 0x000F2260
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

	// Token: 0x17000C95 RID: 3221
	// (get) Token: 0x06003FF6 RID: 16374 RVA: 0x000F4098 File Offset: 0x000F2298
	// (set) Token: 0x06003FF7 RID: 16375 RVA: 0x000F40A0 File Offset: 0x000F22A0
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
			}
		}
	}

	// Token: 0x17000C96 RID: 3222
	// (get) Token: 0x06003FF8 RID: 16376 RVA: 0x000F40C0 File Offset: 0x000F22C0
	// (set) Token: 0x06003FF9 RID: 16377 RVA: 0x000F40C8 File Offset: 0x000F22C8
	public dfControl Track
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

	// Token: 0x17000C97 RID: 3223
	// (get) Token: 0x06003FFA RID: 16378 RVA: 0x000F40E8 File Offset: 0x000F22E8
	// (set) Token: 0x06003FFB RID: 16379 RVA: 0x000F40F0 File Offset: 0x000F22F0
	public dfControl IncButton
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

	// Token: 0x17000C98 RID: 3224
	// (get) Token: 0x06003FFC RID: 16380 RVA: 0x000F4110 File Offset: 0x000F2310
	// (set) Token: 0x06003FFD RID: 16381 RVA: 0x000F4118 File Offset: 0x000F2318
	public dfControl DecButton
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

	// Token: 0x17000C99 RID: 3225
	// (get) Token: 0x06003FFE RID: 16382 RVA: 0x000F4138 File Offset: 0x000F2338
	// (set) Token: 0x06003FFF RID: 16383 RVA: 0x000F4158 File Offset: 0x000F2358
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
			if (this.orientation == dfControlOrientation.Horizontal)
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

	// Token: 0x17000C9A RID: 3226
	// (get) Token: 0x06004000 RID: 16384 RVA: 0x000F41BC File Offset: 0x000F23BC
	// (set) Token: 0x06004001 RID: 16385 RVA: 0x000F41C4 File Offset: 0x000F23C4
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

	// Token: 0x06004002 RID: 16386 RVA: 0x000F41E8 File Offset: 0x000F23E8
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
		if (this.orientation == dfControlOrientation.Horizontal)
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

	// Token: 0x17000C9B RID: 3227
	// (get) Token: 0x06004003 RID: 16387 RVA: 0x000F4370 File Offset: 0x000F2570
	public override bool CanFocus
	{
		get
		{
			return (base.IsEnabled && base.IsVisible) || base.CanFocus;
		}
	}

	// Token: 0x06004004 RID: 16388 RVA: 0x000F4390 File Offset: 0x000F2590
	protected override void OnRebuildRenderData()
	{
		this.updateThumb(this.rawValue);
		base.OnRebuildRenderData();
	}

	// Token: 0x06004005 RID: 16389 RVA: 0x000F43A4 File Offset: 0x000F25A4
	public override void Start()
	{
		base.Start();
		this.attachEvents();
	}

	// Token: 0x06004006 RID: 16390 RVA: 0x000F43B4 File Offset: 0x000F25B4
	public override void OnDisable()
	{
		base.OnDisable();
		this.detachEvents();
	}

	// Token: 0x06004007 RID: 16391 RVA: 0x000F43C4 File Offset: 0x000F25C4
	public override void OnDestroy()
	{
		base.OnDestroy();
		this.detachEvents();
	}

	// Token: 0x06004008 RID: 16392 RVA: 0x000F43D4 File Offset: 0x000F25D4
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

	// Token: 0x06004009 RID: 16393 RVA: 0x000F446C File Offset: 0x000F266C
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

	// Token: 0x0600400A RID: 16394 RVA: 0x000F4504 File Offset: 0x000F2704
	protected internal override void OnKeyDown(dfKeyEventArgs args)
	{
		if (this.Orientation == dfControlOrientation.Horizontal)
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

	// Token: 0x0600400B RID: 16395 RVA: 0x000F45D0 File Offset: 0x000F27D0
	protected internal override void OnMouseWheel(dfMouseEventArgs args)
	{
		this.Value += this.IncrementAmount * -args.WheelDelta;
		args.Use();
		base.Signal("OnMouseWheel", new object[]
		{
			args
		});
	}

	// Token: 0x0600400C RID: 16396 RVA: 0x000F4614 File Offset: 0x000F2814
	protected internal override void OnMouseHover(dfMouseEventArgs args)
	{
		bool flag = args.Source == this.incButton || args.Source == this.decButton || args.Source == this.thumb;
		if (flag)
		{
			return;
		}
		if (args.Source != this.track || !args.Buttons.IsSet(dfMouseButtons.Left))
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

	// Token: 0x0600400D RID: 16397 RVA: 0x000F46BC File Offset: 0x000F28BC
	protected internal override void OnMouseMove(dfMouseEventArgs args)
	{
		if (args.Source == this.incButton || args.Source == this.decButton)
		{
			return;
		}
		if ((args.Source != this.track && args.Source != this.thumb) || !args.Buttons.IsSet(dfMouseButtons.Left))
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

	// Token: 0x0600400E RID: 16398 RVA: 0x000F477C File Offset: 0x000F297C
	protected internal override void OnMouseDown(dfMouseEventArgs args)
	{
		if (args.Buttons.IsSet(dfMouseButtons.Left))
		{
			base.Focus();
		}
		if (args.Source == this.incButton || args.Source == this.decButton)
		{
			return;
		}
		if ((args.Source != this.track && args.Source != this.thumb) || !args.Buttons.IsSet(dfMouseButtons.Left))
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

	// Token: 0x0600400F RID: 16399 RVA: 0x000F48C0 File Offset: 0x000F2AC0
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

	// Token: 0x06004010 RID: 16400 RVA: 0x000F4910 File Offset: 0x000F2B10
	protected internal override void OnSizeChanged()
	{
		base.OnSizeChanged();
		this.updateThumb(this.rawValue);
	}

	// Token: 0x06004011 RID: 16401 RVA: 0x000F4924 File Offset: 0x000F2B24
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

	// Token: 0x06004012 RID: 16402 RVA: 0x000F497C File Offset: 0x000F2B7C
	private void incrementPressed(dfControl sender, dfMouseEventArgs args)
	{
		if (args.Buttons.IsSet(dfMouseButtons.Left))
		{
			this.Value += this.IncrementAmount;
			args.Use();
		}
	}

	// Token: 0x06004013 RID: 16403 RVA: 0x000F49B4 File Offset: 0x000F2BB4
	private void decrementPressed(dfControl sender, dfMouseEventArgs args)
	{
		if (args.Buttons.IsSet(dfMouseButtons.Left))
		{
			this.Value -= this.IncrementAmount;
			args.Use();
		}
	}

	// Token: 0x06004014 RID: 16404 RVA: 0x000F49EC File Offset: 0x000F2BEC
	private void updateFromTrackClick(dfMouseEventArgs args)
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

	// Token: 0x06004015 RID: 16405 RVA: 0x000F4A4C File Offset: 0x000F2C4C
	private float adjustValue(float value)
	{
		float num = Mathf.Max(this.maxValue - this.minValue, 0f);
		float num2 = Mathf.Max(num - this.scrollSize, 0f) + this.minValue;
		float value2 = Mathf.Max(Mathf.Min(num2, value), this.minValue);
		return value2.Quantize(this.stepSize);
	}

	// Token: 0x06004016 RID: 16406 RVA: 0x000F4AAC File Offset: 0x000F2CAC
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
		float num2 = (this.orientation != dfControlOrientation.Horizontal) ? this.track.Height : this.track.Width;
		float num3 = (this.orientation != dfControlOrientation.Horizontal) ? Mathf.Max(this.scrollSize / num * num2, this.thumb.MinimumSize.y) : Mathf.Max(this.scrollSize / num * num2, this.thumb.MinimumSize.x);
		Vector2 size = (this.orientation != dfControlOrientation.Horizontal) ? new Vector2(this.thumb.Width, num3) : new Vector2(num3, this.thumb.Height);
		if (this.Orientation == dfControlOrientation.Horizontal)
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
		Vector3 vector = (this.orientation != dfControlOrientation.Horizontal) ? Vector3.up : Vector3.right;
		Vector3 vector2 = (this.Orientation != dfControlOrientation.Horizontal) ? new Vector3((this.track.Width - this.thumb.Width) * 0.5f, 0f) : new Vector3(0f, (this.track.Height - this.thumb.Height) * 0.5f);
		if (this.Orientation == dfControlOrientation.Horizontal)
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

	// Token: 0x06004017 RID: 16407 RVA: 0x000F4D64 File Offset: 0x000F2F64
	private float getValueFromMouseEvent(dfMouseEventArgs args)
	{
		Vector3[] corners = this.track.GetCorners();
		Vector3 vector = corners[0];
		Vector3 vector2 = corners[(this.orientation != dfControlOrientation.Horizontal) ? 2 : 1];
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
		Vector3 vector4 = dfScrollbar.closestPoint(vector, vector2, vector3, true);
		float num2 = (vector4 - vector).magnitude / (vector2 - vector).magnitude;
		return this.minValue + (this.maxValue - this.minValue) * num2;
	}

	// Token: 0x06004018 RID: 16408 RVA: 0x000F4E74 File Offset: 0x000F3074
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

	// Token: 0x04002207 RID: 8711
	[SerializeField]
	protected dfAtlas atlas;

	// Token: 0x04002208 RID: 8712
	[SerializeField]
	protected dfControlOrientation orientation;

	// Token: 0x04002209 RID: 8713
	[SerializeField]
	protected float rawValue = 1f;

	// Token: 0x0400220A RID: 8714
	[SerializeField]
	protected float minValue;

	// Token: 0x0400220B RID: 8715
	[SerializeField]
	protected float maxValue = 100f;

	// Token: 0x0400220C RID: 8716
	[SerializeField]
	protected float stepSize = 1f;

	// Token: 0x0400220D RID: 8717
	[SerializeField]
	protected float scrollSize = 1f;

	// Token: 0x0400220E RID: 8718
	[SerializeField]
	protected float increment = 1f;

	// Token: 0x0400220F RID: 8719
	[SerializeField]
	protected dfControl thumb;

	// Token: 0x04002210 RID: 8720
	[SerializeField]
	protected dfControl track;

	// Token: 0x04002211 RID: 8721
	[SerializeField]
	protected dfControl incButton;

	// Token: 0x04002212 RID: 8722
	[SerializeField]
	protected dfControl decButton;

	// Token: 0x04002213 RID: 8723
	[SerializeField]
	protected RectOffset thumbPadding = new RectOffset();

	// Token: 0x04002214 RID: 8724
	[SerializeField]
	protected bool autoHide;

	// Token: 0x04002215 RID: 8725
	private Vector3 thumbMouseOffset = Vector3.zero;
}
