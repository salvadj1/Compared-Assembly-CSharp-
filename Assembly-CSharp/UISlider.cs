using System;
using UnityEngine;

// Token: 0x02000861 RID: 2145
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Slider")]
public class UISlider : global::IgnoreTimeScale
{
	// Token: 0x17000E17 RID: 3607
	// (get) Token: 0x060049F4 RID: 18932 RVA: 0x0011C6C8 File Offset: 0x0011A8C8
	// (set) Token: 0x060049F5 RID: 18933 RVA: 0x0011C6D0 File Offset: 0x0011A8D0
	public float sliderValue
	{
		get
		{
			return this.mStepValue;
		}
		set
		{
			this.Set(value, false);
		}
	}

	// Token: 0x060049F6 RID: 18934 RVA: 0x0011C6DC File Offset: 0x0011A8DC
	private void Init()
	{
		this.mInitDone = true;
		if (this.foreground != null)
		{
			this.mFGWidget = this.foreground.GetComponent<global::UIWidget>();
			this.mFGFilled = ((!(this.mFGWidget != null)) ? null : (this.mFGWidget as global::UIFilledSprite));
			this.mFGTrans = this.foreground.transform;
			if (this.fullSize == Vector2.zero)
			{
				this.fullSize = this.foreground.localScale;
			}
		}
		else if (this.mCol != null)
		{
			if (this.fullSize == Vector2.zero)
			{
				this.fullSize = this.mCol.size;
			}
		}
		else
		{
			Debug.LogWarning("UISlider expected to find a foreground object or a box collider to work with", this);
		}
	}

	// Token: 0x060049F7 RID: 18935 RVA: 0x0011C7C8 File Offset: 0x0011A9C8
	private void Awake()
	{
		this.mTrans = base.transform;
		this.mCol = (base.collider as BoxCollider);
	}

	// Token: 0x060049F8 RID: 18936 RVA: 0x0011C7E8 File Offset: 0x0011A9E8
	private void Start()
	{
		this.Init();
		if (Application.isPlaying && this.thumb != null && global::NGUITools.HasMeansOfClicking(this.thumb))
		{
			global::UIEventListener uieventListener = global::UIEventListener.Get(this.thumb.gameObject);
			global::UIEventListener uieventListener2 = uieventListener;
			uieventListener2.onPress = (global::UIEventListener.BoolDelegate)Delegate.Combine(uieventListener2.onPress, new global::UIEventListener.BoolDelegate(this.OnPressThumb));
			global::UIEventListener uieventListener3 = uieventListener;
			uieventListener3.onDrag = (global::UIEventListener.VectorDelegate)Delegate.Combine(uieventListener3.onDrag, new global::UIEventListener.VectorDelegate(this.OnDragThumb));
		}
		this.Set(this.rawValue, true);
	}

	// Token: 0x060049F9 RID: 18937 RVA: 0x0011C888 File Offset: 0x0011AA88
	private void OnPress(bool pressed)
	{
		if (pressed)
		{
			this.UpdateDrag();
		}
	}

	// Token: 0x060049FA RID: 18938 RVA: 0x0011C898 File Offset: 0x0011AA98
	private void OnDrag(Vector2 delta)
	{
		this.UpdateDrag();
	}

	// Token: 0x060049FB RID: 18939 RVA: 0x0011C8A0 File Offset: 0x0011AAA0
	private void OnPressThumb(GameObject go, bool pressed)
	{
		if (pressed)
		{
			this.UpdateDrag();
		}
	}

	// Token: 0x060049FC RID: 18940 RVA: 0x0011C8B0 File Offset: 0x0011AAB0
	private void OnDragThumb(GameObject go, Vector2 delta)
	{
		this.UpdateDrag();
	}

	// Token: 0x060049FD RID: 18941 RVA: 0x0011C8B8 File Offset: 0x0011AAB8
	private void OnKey(KeyCode key)
	{
		float num = ((float)this.numberOfSteps <= 1f) ? 0.125f : (1f / (float)(this.numberOfSteps - 1));
		if (this.direction == global::UISlider.Direction.Horizontal)
		{
			if (key == 276)
			{
				this.Set(this.rawValue - num, false);
			}
			else if (key == 275)
			{
				this.Set(this.rawValue + num, false);
			}
		}
		else if (key == 274)
		{
			this.Set(this.rawValue - num, false);
		}
		else if (key == 273)
		{
			this.Set(this.rawValue + num, false);
		}
	}

	// Token: 0x060049FE RID: 18942 RVA: 0x0011C974 File Offset: 0x0011AB74
	private void UpdateDrag()
	{
		if (this.mCol == null || global::UICamera.currentCamera == null || !global::UICamera.IsPressing)
		{
			return;
		}
		global::UICamera.currentTouch.clickNotification = global::UICamera.ClickNotification.None;
		Ray ray = global::UICamera.currentCamera.ScreenPointToRay(global::UICamera.currentTouch.pos);
		Plane plane;
		plane..ctor(this.mTrans.rotation * Vector3.back, this.mTrans.position);
		float num;
		if (!plane.Raycast(ray, ref num))
		{
			return;
		}
		Vector3 vector = this.mTrans.localPosition + this.mCol.center - this.mCol.size * 0.5f;
		Vector3 vector2 = this.mTrans.localPosition - vector;
		Vector3 vector3 = this.mTrans.InverseTransformPoint(ray.GetPoint(num));
		Vector3 vector4 = vector3 + vector2;
		this.Set((this.direction != global::UISlider.Direction.Horizontal) ? (vector4.y / this.mCol.size.y) : (vector4.x / this.mCol.size.x), false);
	}

	// Token: 0x060049FF RID: 18943 RVA: 0x0011CAC4 File Offset: 0x0011ACC4
	private void Set(float input, bool force)
	{
		if (!this.mInitDone)
		{
			this.Init();
		}
		float num = Mathf.Clamp01(input);
		if (num < 0.001f)
		{
			num = 0f;
		}
		this.rawValue = num;
		if (this.numberOfSteps > 1)
		{
			num = Mathf.Round(num * (float)(this.numberOfSteps - 1)) / (float)(this.numberOfSteps - 1);
		}
		if (force || this.mStepValue != num)
		{
			this.mStepValue = num;
			Vector3 localScale = this.fullSize;
			if (this.direction == global::UISlider.Direction.Horizontal)
			{
				localScale.x *= this.mStepValue;
			}
			else
			{
				localScale.y *= this.mStepValue;
			}
			if (this.mFGFilled != null)
			{
				this.mFGFilled.fillAmount = this.mStepValue;
			}
			else if (this.foreground != null)
			{
				this.mFGTrans.localScale = localScale;
				if (this.mFGWidget != null)
				{
					if (num > 0.001f)
					{
						this.mFGWidget.enabled = true;
						this.mFGWidget.MarkAsChanged();
					}
					else
					{
						this.mFGWidget.enabled = false;
					}
				}
			}
			if (this.thumb != null)
			{
				Vector3 localPosition = this.thumb.localPosition;
				if (this.mFGFilled != null)
				{
					if (this.mFGFilled.fillDirection == global::UIFilledSprite.FillDirection.Horizontal)
					{
						localPosition.x = ((!this.mFGFilled.invert) ? localScale.x : (this.fullSize.x - localScale.x));
					}
					else if (this.mFGFilled.fillDirection == global::UIFilledSprite.FillDirection.Vertical)
					{
						localPosition.y = ((!this.mFGFilled.invert) ? localScale.y : (this.fullSize.y - localScale.y));
					}
				}
				else if (this.direction == global::UISlider.Direction.Horizontal)
				{
					localPosition.x = localScale.x;
				}
				else
				{
					localPosition.y = localScale.y;
				}
				this.thumb.localPosition = localPosition;
			}
			if (this.eventReceiver != null && !string.IsNullOrEmpty(this.functionName) && Application.isPlaying)
			{
				global::UISlider.current = this;
				this.eventReceiver.SendMessage(this.functionName, this.mStepValue, 1);
				global::UISlider.current = null;
			}
		}
	}

	// Token: 0x06004A00 RID: 18944 RVA: 0x0011CD60 File Offset: 0x0011AF60
	public void ForceUpdate()
	{
		this.Set(this.rawValue, true);
	}

	// Token: 0x0400281D RID: 10269
	public static global::UISlider current;

	// Token: 0x0400281E RID: 10270
	public Transform foreground;

	// Token: 0x0400281F RID: 10271
	public Transform thumb;

	// Token: 0x04002820 RID: 10272
	public global::UISlider.Direction direction;

	// Token: 0x04002821 RID: 10273
	public Vector2 fullSize = Vector2.zero;

	// Token: 0x04002822 RID: 10274
	public GameObject eventReceiver;

	// Token: 0x04002823 RID: 10275
	public string functionName = "OnSliderChange";

	// Token: 0x04002824 RID: 10276
	public int numberOfSteps;

	// Token: 0x04002825 RID: 10277
	[SerializeField]
	[HideInInspector]
	private float rawValue = 1f;

	// Token: 0x04002826 RID: 10278
	private float mStepValue = 1f;

	// Token: 0x04002827 RID: 10279
	private BoxCollider mCol;

	// Token: 0x04002828 RID: 10280
	private Transform mTrans;

	// Token: 0x04002829 RID: 10281
	private Transform mFGTrans;

	// Token: 0x0400282A RID: 10282
	private global::UIWidget mFGWidget;

	// Token: 0x0400282B RID: 10283
	private global::UIFilledSprite mFGFilled;

	// Token: 0x0400282C RID: 10284
	private bool mInitDone;

	// Token: 0x02000862 RID: 2146
	public enum Direction
	{
		// Token: 0x0400282E RID: 10286
		Horizontal,
		// Token: 0x0400282F RID: 10287
		Vertical
	}
}
