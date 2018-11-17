using System;
using UnityEngine;

// Token: 0x0200077D RID: 1917
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Slider")]
public class UISlider : IgnoreTimeScale
{
	// Token: 0x17000D87 RID: 3463
	// (get) Token: 0x0600458B RID: 17803 RVA: 0x00112D48 File Offset: 0x00110F48
	// (set) Token: 0x0600458C RID: 17804 RVA: 0x00112D50 File Offset: 0x00110F50
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

	// Token: 0x0600458D RID: 17805 RVA: 0x00112D5C File Offset: 0x00110F5C
	private void Init()
	{
		this.mInitDone = true;
		if (this.foreground != null)
		{
			this.mFGWidget = this.foreground.GetComponent<UIWidget>();
			this.mFGFilled = ((!(this.mFGWidget != null)) ? null : (this.mFGWidget as UIFilledSprite));
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

	// Token: 0x0600458E RID: 17806 RVA: 0x00112E48 File Offset: 0x00111048
	private void Awake()
	{
		this.mTrans = base.transform;
		this.mCol = (base.collider as BoxCollider);
	}

	// Token: 0x0600458F RID: 17807 RVA: 0x00112E68 File Offset: 0x00111068
	private void Start()
	{
		this.Init();
		if (Application.isPlaying && this.thumb != null && NGUITools.HasMeansOfClicking(this.thumb))
		{
			UIEventListener uieventListener = UIEventListener.Get(this.thumb.gameObject);
			UIEventListener uieventListener2 = uieventListener;
			uieventListener2.onPress = (UIEventListener.BoolDelegate)Delegate.Combine(uieventListener2.onPress, new UIEventListener.BoolDelegate(this.OnPressThumb));
			UIEventListener uieventListener3 = uieventListener;
			uieventListener3.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(uieventListener3.onDrag, new UIEventListener.VectorDelegate(this.OnDragThumb));
		}
		this.Set(this.rawValue, true);
	}

	// Token: 0x06004590 RID: 17808 RVA: 0x00112F08 File Offset: 0x00111108
	private void OnPress(bool pressed)
	{
		if (pressed)
		{
			this.UpdateDrag();
		}
	}

	// Token: 0x06004591 RID: 17809 RVA: 0x00112F18 File Offset: 0x00111118
	private void OnDrag(Vector2 delta)
	{
		this.UpdateDrag();
	}

	// Token: 0x06004592 RID: 17810 RVA: 0x00112F20 File Offset: 0x00111120
	private void OnPressThumb(GameObject go, bool pressed)
	{
		if (pressed)
		{
			this.UpdateDrag();
		}
	}

	// Token: 0x06004593 RID: 17811 RVA: 0x00112F30 File Offset: 0x00111130
	private void OnDragThumb(GameObject go, Vector2 delta)
	{
		this.UpdateDrag();
	}

	// Token: 0x06004594 RID: 17812 RVA: 0x00112F38 File Offset: 0x00111138
	private void OnKey(KeyCode key)
	{
		float num = ((float)this.numberOfSteps <= 1f) ? 0.125f : (1f / (float)(this.numberOfSteps - 1));
		if (this.direction == UISlider.Direction.Horizontal)
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

	// Token: 0x06004595 RID: 17813 RVA: 0x00112FF4 File Offset: 0x001111F4
	private void UpdateDrag()
	{
		if (this.mCol == null || UICamera.currentCamera == null || !UICamera.IsPressing)
		{
			return;
		}
		UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
		Ray ray = UICamera.currentCamera.ScreenPointToRay(UICamera.currentTouch.pos);
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
		this.Set((this.direction != UISlider.Direction.Horizontal) ? (vector4.y / this.mCol.size.y) : (vector4.x / this.mCol.size.x), false);
	}

	// Token: 0x06004596 RID: 17814 RVA: 0x00113144 File Offset: 0x00111344
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
			if (this.direction == UISlider.Direction.Horizontal)
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
					if (this.mFGFilled.fillDirection == UIFilledSprite.FillDirection.Horizontal)
					{
						localPosition.x = ((!this.mFGFilled.invert) ? localScale.x : (this.fullSize.x - localScale.x));
					}
					else if (this.mFGFilled.fillDirection == UIFilledSprite.FillDirection.Vertical)
					{
						localPosition.y = ((!this.mFGFilled.invert) ? localScale.y : (this.fullSize.y - localScale.y));
					}
				}
				else if (this.direction == UISlider.Direction.Horizontal)
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
				UISlider.current = this;
				this.eventReceiver.SendMessage(this.functionName, this.mStepValue, 1);
				UISlider.current = null;
			}
		}
	}

	// Token: 0x06004597 RID: 17815 RVA: 0x001133E0 File Offset: 0x001115E0
	public void ForceUpdate()
	{
		this.Set(this.rawValue, true);
	}

	// Token: 0x040025E6 RID: 9702
	public static UISlider current;

	// Token: 0x040025E7 RID: 9703
	public Transform foreground;

	// Token: 0x040025E8 RID: 9704
	public Transform thumb;

	// Token: 0x040025E9 RID: 9705
	public UISlider.Direction direction;

	// Token: 0x040025EA RID: 9706
	public Vector2 fullSize = Vector2.zero;

	// Token: 0x040025EB RID: 9707
	public GameObject eventReceiver;

	// Token: 0x040025EC RID: 9708
	public string functionName = "OnSliderChange";

	// Token: 0x040025ED RID: 9709
	public int numberOfSteps;

	// Token: 0x040025EE RID: 9710
	[SerializeField]
	[HideInInspector]
	private float rawValue = 1f;

	// Token: 0x040025EF RID: 9711
	private float mStepValue = 1f;

	// Token: 0x040025F0 RID: 9712
	private BoxCollider mCol;

	// Token: 0x040025F1 RID: 9713
	private Transform mTrans;

	// Token: 0x040025F2 RID: 9714
	private Transform mFGTrans;

	// Token: 0x040025F3 RID: 9715
	private UIWidget mFGWidget;

	// Token: 0x040025F4 RID: 9716
	private UIFilledSprite mFGFilled;

	// Token: 0x040025F5 RID: 9717
	private bool mInitDone;

	// Token: 0x0200077E RID: 1918
	public enum Direction
	{
		// Token: 0x040025F7 RID: 9719
		Horizontal,
		// Token: 0x040025F8 RID: 9720
		Vertical
	}
}
