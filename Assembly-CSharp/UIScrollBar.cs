using System;
using UnityEngine;

// Token: 0x0200085E RID: 2142
[AddComponentMenu("NGUI/Interaction/Scroll Bar")]
[ExecuteInEditMode]
public class UIScrollBar : MonoBehaviour
{
	// Token: 0x17000E0E RID: 3598
	// (get) Token: 0x060049D6 RID: 18902 RVA: 0x0011BC90 File Offset: 0x00119E90
	public Transform cachedTransform
	{
		get
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
			}
			return this.mTrans;
		}
	}

	// Token: 0x17000E0F RID: 3599
	// (get) Token: 0x060049D7 RID: 18903 RVA: 0x0011BCB8 File Offset: 0x00119EB8
	public Camera cachedCamera
	{
		get
		{
			if (this.mCam == null)
			{
				this.mCam = global::NGUITools.FindCameraForLayer(base.gameObject.layer);
			}
			return this.mCam;
		}
	}

	// Token: 0x17000E10 RID: 3600
	// (get) Token: 0x060049D8 RID: 18904 RVA: 0x0011BCE8 File Offset: 0x00119EE8
	// (set) Token: 0x060049D9 RID: 18905 RVA: 0x0011BCF0 File Offset: 0x00119EF0
	public global::UISprite background
	{
		get
		{
			return this.mBG;
		}
		set
		{
			if (this.mBG != value)
			{
				this.mBG = value;
				this.mIsDirty = true;
			}
		}
	}

	// Token: 0x17000E11 RID: 3601
	// (get) Token: 0x060049DA RID: 18906 RVA: 0x0011BD14 File Offset: 0x00119F14
	// (set) Token: 0x060049DB RID: 18907 RVA: 0x0011BD1C File Offset: 0x00119F1C
	public global::UISprite foreground
	{
		get
		{
			return this.mFG;
		}
		set
		{
			if (this.mFG != value)
			{
				this.mFG = value;
				this.mIsDirty = true;
			}
		}
	}

	// Token: 0x17000E12 RID: 3602
	// (get) Token: 0x060049DC RID: 18908 RVA: 0x0011BD40 File Offset: 0x00119F40
	// (set) Token: 0x060049DD RID: 18909 RVA: 0x0011BD48 File Offset: 0x00119F48
	public global::UIScrollBar.Direction direction
	{
		get
		{
			return this.mDir;
		}
		set
		{
			if (this.mDir != value)
			{
				this.mDir = value;
				this.mIsDirty = true;
				if (this.mBG != null)
				{
					Transform cachedTransform = this.mBG.cachedTransform;
					Vector3 localScale = cachedTransform.localScale;
					if ((this.mDir == global::UIScrollBar.Direction.Vertical && localScale.x > localScale.y) || (this.mDir == global::UIScrollBar.Direction.Horizontal && localScale.x < localScale.y))
					{
						float x = localScale.x;
						localScale.x = localScale.y;
						localScale.y = x;
						cachedTransform.localScale = localScale;
						this.ForceUpdate();
						if (this.mBG.collider != null)
						{
							global::NGUITools.AddWidgetHotSpot(this.mBG.gameObject);
						}
						if (this.mFG.collider != null)
						{
							global::NGUITools.AddWidgetHotSpot(this.mFG.gameObject);
						}
					}
				}
			}
		}
	}

	// Token: 0x17000E13 RID: 3603
	// (get) Token: 0x060049DE RID: 18910 RVA: 0x0011BE4C File Offset: 0x0011A04C
	// (set) Token: 0x060049DF RID: 18911 RVA: 0x0011BE54 File Offset: 0x0011A054
	public bool inverted
	{
		get
		{
			return this.mInverted;
		}
		set
		{
			if (this.mInverted != value)
			{
				this.mInverted = value;
				this.mIsDirty = true;
			}
		}
	}

	// Token: 0x17000E14 RID: 3604
	// (get) Token: 0x060049E0 RID: 18912 RVA: 0x0011BE70 File Offset: 0x0011A070
	// (set) Token: 0x060049E1 RID: 18913 RVA: 0x0011BE78 File Offset: 0x0011A078
	public float scrollValue
	{
		get
		{
			return this.mScroll;
		}
		set
		{
			float num = Mathf.Clamp01(value);
			if (this.mScroll != num)
			{
				this.mScroll = num;
				this.mIsDirty = true;
				if (this.onChange != null)
				{
					this.onChange(this);
				}
			}
		}
	}

	// Token: 0x17000E15 RID: 3605
	// (get) Token: 0x060049E2 RID: 18914 RVA: 0x0011BEC0 File Offset: 0x0011A0C0
	// (set) Token: 0x060049E3 RID: 18915 RVA: 0x0011BEC8 File Offset: 0x0011A0C8
	public float barSize
	{
		get
		{
			return this.mSize;
		}
		set
		{
			float num = Mathf.Clamp01(value);
			if (this.mSize != num)
			{
				this.mSize = num;
				this.mIsDirty = true;
				if (this.onChange != null)
				{
					this.onChange(this);
				}
			}
		}
	}

	// Token: 0x17000E16 RID: 3606
	// (get) Token: 0x060049E4 RID: 18916 RVA: 0x0011BF10 File Offset: 0x0011A110
	// (set) Token: 0x060049E5 RID: 18917 RVA: 0x0011BF5C File Offset: 0x0011A15C
	public float alpha
	{
		get
		{
			if (this.mFG != null)
			{
				return this.mFG.alpha;
			}
			if (this.mBG != null)
			{
				return this.mBG.alpha;
			}
			return 0f;
		}
		set
		{
			if (this.mFG != null)
			{
				this.mFG.alpha = value;
				this.mFG.gameObject.SetActive(!global::NGUITools.ZeroAlpha(this.mFG.alpha));
			}
			if (this.mBG != null)
			{
				this.mBG.alpha = value;
				this.mBG.gameObject.SetActive(!global::NGUITools.ZeroAlpha(this.mFG.alpha));
			}
		}
	}

	// Token: 0x060049E6 RID: 18918 RVA: 0x0011BFEC File Offset: 0x0011A1EC
	private void CenterOnPos(Vector2 localPos)
	{
		if (this.mBG == null || this.mFG == null)
		{
			return;
		}
		global::AABBox aabbox = global::NGUIMath.CalculateRelativeInnerBounds(this.cachedTransform, this.mBG);
		global::AABBox aabbox2 = global::NGUIMath.CalculateRelativeInnerBounds(this.cachedTransform, this.mFG);
		if (this.mDir == global::UIScrollBar.Direction.Horizontal)
		{
			float num = aabbox.size.x - aabbox2.size.x;
			float num2 = num * 0.5f;
			float num3 = aabbox.center.x - num2;
			float num4 = (num <= 0f) ? 0f : ((localPos.x - num3) / num);
			this.scrollValue = ((!this.mInverted) ? num4 : (1f - num4));
		}
		else
		{
			float num5 = aabbox.size.y - aabbox2.size.y;
			float num6 = num5 * 0.5f;
			float num7 = aabbox.center.y - num6;
			float num8 = (num5 <= 0f) ? 0f : (1f - (localPos.y - num7) / num5);
			this.scrollValue = ((!this.mInverted) ? num8 : (1f - num8));
		}
	}

	// Token: 0x060049E7 RID: 18919 RVA: 0x0011C160 File Offset: 0x0011A360
	private void Reposition(Vector2 screenPos)
	{
		Transform cachedTransform = this.cachedTransform;
		Plane plane;
		plane..ctor(cachedTransform.rotation * Vector3.back, cachedTransform.position);
		Ray ray = this.cachedCamera.ScreenPointToRay(screenPos);
		float num;
		if (!plane.Raycast(ray, ref num))
		{
			return;
		}
		this.CenterOnPos(cachedTransform.InverseTransformPoint(ray.GetPoint(num)));
	}

	// Token: 0x060049E8 RID: 18920 RVA: 0x0011C1CC File Offset: 0x0011A3CC
	private void OnPressBackground(GameObject go, bool isPressed)
	{
		this.mCam = global::UICamera.currentCamera;
		this.Reposition(global::UICamera.lastTouchPosition);
	}

	// Token: 0x060049E9 RID: 18921 RVA: 0x0011C1E4 File Offset: 0x0011A3E4
	private void OnDragBackground(GameObject go, Vector2 delta)
	{
		this.mCam = global::UICamera.currentCamera;
		this.Reposition(global::UICamera.lastTouchPosition);
	}

	// Token: 0x060049EA RID: 18922 RVA: 0x0011C1FC File Offset: 0x0011A3FC
	private void OnPressForeground(GameObject go, bool isPressed)
	{
		if (isPressed)
		{
			this.mCam = global::UICamera.currentCamera;
			global::AABBox aabbox = global::NGUIMath.CalculateAbsoluteWidgetBounds(this.mFG.cachedTransform);
			this.mScreenPos = this.mCam.WorldToScreenPoint(aabbox.center);
		}
	}

	// Token: 0x060049EB RID: 18923 RVA: 0x0011C248 File Offset: 0x0011A448
	private void OnDragForeground(GameObject go, Vector2 delta)
	{
		this.mCam = global::UICamera.currentCamera;
		this.Reposition(this.mScreenPos + global::UICamera.currentTouch.totalDelta);
	}

	// Token: 0x060049EC RID: 18924 RVA: 0x0011C27C File Offset: 0x0011A47C
	private void Start()
	{
		if (this.background != null && global::NGUITools.HasMeansOfClicking(this.background))
		{
			global::UIEventListener uieventListener = global::UIEventListener.Get(this.background.gameObject);
			global::UIEventListener uieventListener2 = uieventListener;
			uieventListener2.onPress = (global::UIEventListener.BoolDelegate)Delegate.Combine(uieventListener2.onPress, new global::UIEventListener.BoolDelegate(this.OnPressBackground));
			global::UIEventListener uieventListener3 = uieventListener;
			uieventListener3.onDrag = (global::UIEventListener.VectorDelegate)Delegate.Combine(uieventListener3.onDrag, new global::UIEventListener.VectorDelegate(this.OnDragBackground));
		}
		if (this.foreground != null && global::NGUITools.HasMeansOfClicking(this.foreground))
		{
			global::UIEventListener uieventListener4 = global::UIEventListener.Get(this.foreground.gameObject);
			global::UIEventListener uieventListener5 = uieventListener4;
			uieventListener5.onPress = (global::UIEventListener.BoolDelegate)Delegate.Combine(uieventListener5.onPress, new global::UIEventListener.BoolDelegate(this.OnPressForeground));
			global::UIEventListener uieventListener6 = uieventListener4;
			uieventListener6.onDrag = (global::UIEventListener.VectorDelegate)Delegate.Combine(uieventListener6.onDrag, new global::UIEventListener.VectorDelegate(this.OnDragForeground));
		}
		this.ForceUpdate();
	}

	// Token: 0x060049ED RID: 18925 RVA: 0x0011C37C File Offset: 0x0011A57C
	private void Update()
	{
		if (this.mIsDirty)
		{
			this.ForceUpdate();
		}
	}

	// Token: 0x060049EE RID: 18926 RVA: 0x0011C390 File Offset: 0x0011A590
	public void ForceUpdate()
	{
		this.mIsDirty = false;
		if (this.mBG != null && this.mFG != null)
		{
			this.mSize = Mathf.Clamp01(this.mSize);
			this.mScroll = Mathf.Clamp01(this.mScroll);
			Vector4 border = this.mBG.border;
			Vector4 border2 = this.mFG.border;
			Vector2 vector;
			vector..ctor(Mathf.Max(0f, this.mBG.cachedTransform.localScale.x - border.x - border.z), Mathf.Max(0f, this.mBG.cachedTransform.localScale.y - border.y - border.w));
			float num = (!this.mInverted) ? this.mScroll : (1f - this.mScroll);
			if (this.mDir == global::UIScrollBar.Direction.Horizontal)
			{
				Vector2 vector2;
				vector2..ctor(vector.x * this.mSize, vector.y);
				this.mFG.pivot = global::UIWidget.Pivot.Left;
				this.mBG.pivot = global::UIWidget.Pivot.Left;
				this.mBG.cachedTransform.localPosition = Vector3.zero;
				this.mFG.cachedTransform.localPosition = new Vector3(border.x - border2.x + (vector.x - vector2.x) * num, 0f, 0f);
				this.mFG.cachedTransform.localScale = new Vector3(vector2.x + border2.x + border2.z, vector2.y + border2.y + border2.w, 1f);
				if (num < 0.999f && num > 0.001f)
				{
					this.mFG.MakePixelPerfect();
				}
			}
			else
			{
				Vector2 vector3;
				vector3..ctor(vector.x, vector.y * this.mSize);
				this.mFG.pivot = global::UIWidget.Pivot.Top;
				this.mBG.pivot = global::UIWidget.Pivot.Top;
				this.mBG.cachedTransform.localPosition = Vector3.zero;
				this.mFG.cachedTransform.localPosition = new Vector3(0f, -border.y + border2.y - (vector.y - vector3.y) * num, 0f);
				this.mFG.cachedTransform.localScale = new Vector3(vector3.x + border2.x + border2.z, vector3.y + border2.y + border2.w, 1f);
				if (num < 0.999f && num > 0.001f)
				{
					this.mFG.MakePixelPerfect();
				}
			}
		}
	}

	// Token: 0x0400280F RID: 10255
	[SerializeField]
	[HideInInspector]
	private global::UISprite mBG;

	// Token: 0x04002810 RID: 10256
	[HideInInspector]
	[SerializeField]
	private global::UISprite mFG;

	// Token: 0x04002811 RID: 10257
	[HideInInspector]
	[SerializeField]
	private global::UIScrollBar.Direction mDir;

	// Token: 0x04002812 RID: 10258
	[SerializeField]
	[HideInInspector]
	private bool mInverted;

	// Token: 0x04002813 RID: 10259
	[SerializeField]
	[HideInInspector]
	private float mScroll;

	// Token: 0x04002814 RID: 10260
	[SerializeField]
	[HideInInspector]
	private float mSize = 1f;

	// Token: 0x04002815 RID: 10261
	private Transform mTrans;

	// Token: 0x04002816 RID: 10262
	private bool mIsDirty;

	// Token: 0x04002817 RID: 10263
	private Camera mCam;

	// Token: 0x04002818 RID: 10264
	private Vector2 mScreenPos = Vector2.zero;

	// Token: 0x04002819 RID: 10265
	public global::UIScrollBar.OnScrollBarChange onChange;

	// Token: 0x0200085F RID: 2143
	public enum Direction
	{
		// Token: 0x0400281B RID: 10267
		Horizontal,
		// Token: 0x0400281C RID: 10268
		Vertical
	}

	// Token: 0x02000860 RID: 2144
	// (Invoke) Token: 0x060049F0 RID: 18928
	public delegate void OnScrollBarChange(global::UIScrollBar sb);
}
