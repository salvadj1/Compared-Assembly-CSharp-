using System;
using UnityEngine;

// Token: 0x0200077B RID: 1915
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Scroll Bar")]
public class UIScrollBar : MonoBehaviour
{
	// Token: 0x17000D7E RID: 3454
	// (get) Token: 0x06004571 RID: 17777 RVA: 0x00112310 File Offset: 0x00110510
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

	// Token: 0x17000D7F RID: 3455
	// (get) Token: 0x06004572 RID: 17778 RVA: 0x00112338 File Offset: 0x00110538
	public Camera cachedCamera
	{
		get
		{
			if (this.mCam == null)
			{
				this.mCam = NGUITools.FindCameraForLayer(base.gameObject.layer);
			}
			return this.mCam;
		}
	}

	// Token: 0x17000D80 RID: 3456
	// (get) Token: 0x06004573 RID: 17779 RVA: 0x00112368 File Offset: 0x00110568
	// (set) Token: 0x06004574 RID: 17780 RVA: 0x00112370 File Offset: 0x00110570
	public UISprite background
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

	// Token: 0x17000D81 RID: 3457
	// (get) Token: 0x06004575 RID: 17781 RVA: 0x00112394 File Offset: 0x00110594
	// (set) Token: 0x06004576 RID: 17782 RVA: 0x0011239C File Offset: 0x0011059C
	public UISprite foreground
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

	// Token: 0x17000D82 RID: 3458
	// (get) Token: 0x06004577 RID: 17783 RVA: 0x001123C0 File Offset: 0x001105C0
	// (set) Token: 0x06004578 RID: 17784 RVA: 0x001123C8 File Offset: 0x001105C8
	public UIScrollBar.Direction direction
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
					if ((this.mDir == UIScrollBar.Direction.Vertical && localScale.x > localScale.y) || (this.mDir == UIScrollBar.Direction.Horizontal && localScale.x < localScale.y))
					{
						float x = localScale.x;
						localScale.x = localScale.y;
						localScale.y = x;
						cachedTransform.localScale = localScale;
						this.ForceUpdate();
						if (this.mBG.collider != null)
						{
							NGUITools.AddWidgetHotSpot(this.mBG.gameObject);
						}
						if (this.mFG.collider != null)
						{
							NGUITools.AddWidgetHotSpot(this.mFG.gameObject);
						}
					}
				}
			}
		}
	}

	// Token: 0x17000D83 RID: 3459
	// (get) Token: 0x06004579 RID: 17785 RVA: 0x001124CC File Offset: 0x001106CC
	// (set) Token: 0x0600457A RID: 17786 RVA: 0x001124D4 File Offset: 0x001106D4
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

	// Token: 0x17000D84 RID: 3460
	// (get) Token: 0x0600457B RID: 17787 RVA: 0x001124F0 File Offset: 0x001106F0
	// (set) Token: 0x0600457C RID: 17788 RVA: 0x001124F8 File Offset: 0x001106F8
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

	// Token: 0x17000D85 RID: 3461
	// (get) Token: 0x0600457D RID: 17789 RVA: 0x00112540 File Offset: 0x00110740
	// (set) Token: 0x0600457E RID: 17790 RVA: 0x00112548 File Offset: 0x00110748
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

	// Token: 0x17000D86 RID: 3462
	// (get) Token: 0x0600457F RID: 17791 RVA: 0x00112590 File Offset: 0x00110790
	// (set) Token: 0x06004580 RID: 17792 RVA: 0x001125DC File Offset: 0x001107DC
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
				this.mFG.gameObject.SetActive(!NGUITools.ZeroAlpha(this.mFG.alpha));
			}
			if (this.mBG != null)
			{
				this.mBG.alpha = value;
				this.mBG.gameObject.SetActive(!NGUITools.ZeroAlpha(this.mFG.alpha));
			}
		}
	}

	// Token: 0x06004581 RID: 17793 RVA: 0x0011266C File Offset: 0x0011086C
	private void CenterOnPos(Vector2 localPos)
	{
		if (this.mBG == null || this.mFG == null)
		{
			return;
		}
		AABBox aabbox = NGUIMath.CalculateRelativeInnerBounds(this.cachedTransform, this.mBG);
		AABBox aabbox2 = NGUIMath.CalculateRelativeInnerBounds(this.cachedTransform, this.mFG);
		if (this.mDir == UIScrollBar.Direction.Horizontal)
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

	// Token: 0x06004582 RID: 17794 RVA: 0x001127E0 File Offset: 0x001109E0
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

	// Token: 0x06004583 RID: 17795 RVA: 0x0011284C File Offset: 0x00110A4C
	private void OnPressBackground(GameObject go, bool isPressed)
	{
		this.mCam = UICamera.currentCamera;
		this.Reposition(UICamera.lastTouchPosition);
	}

	// Token: 0x06004584 RID: 17796 RVA: 0x00112864 File Offset: 0x00110A64
	private void OnDragBackground(GameObject go, Vector2 delta)
	{
		this.mCam = UICamera.currentCamera;
		this.Reposition(UICamera.lastTouchPosition);
	}

	// Token: 0x06004585 RID: 17797 RVA: 0x0011287C File Offset: 0x00110A7C
	private void OnPressForeground(GameObject go, bool isPressed)
	{
		if (isPressed)
		{
			this.mCam = UICamera.currentCamera;
			AABBox aabbox = NGUIMath.CalculateAbsoluteWidgetBounds(this.mFG.cachedTransform);
			this.mScreenPos = this.mCam.WorldToScreenPoint(aabbox.center);
		}
	}

	// Token: 0x06004586 RID: 17798 RVA: 0x001128C8 File Offset: 0x00110AC8
	private void OnDragForeground(GameObject go, Vector2 delta)
	{
		this.mCam = UICamera.currentCamera;
		this.Reposition(this.mScreenPos + UICamera.currentTouch.totalDelta);
	}

	// Token: 0x06004587 RID: 17799 RVA: 0x001128FC File Offset: 0x00110AFC
	private void Start()
	{
		if (this.background != null && NGUITools.HasMeansOfClicking(this.background))
		{
			UIEventListener uieventListener = UIEventListener.Get(this.background.gameObject);
			UIEventListener uieventListener2 = uieventListener;
			uieventListener2.onPress = (UIEventListener.BoolDelegate)Delegate.Combine(uieventListener2.onPress, new UIEventListener.BoolDelegate(this.OnPressBackground));
			UIEventListener uieventListener3 = uieventListener;
			uieventListener3.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(uieventListener3.onDrag, new UIEventListener.VectorDelegate(this.OnDragBackground));
		}
		if (this.foreground != null && NGUITools.HasMeansOfClicking(this.foreground))
		{
			UIEventListener uieventListener4 = UIEventListener.Get(this.foreground.gameObject);
			UIEventListener uieventListener5 = uieventListener4;
			uieventListener5.onPress = (UIEventListener.BoolDelegate)Delegate.Combine(uieventListener5.onPress, new UIEventListener.BoolDelegate(this.OnPressForeground));
			UIEventListener uieventListener6 = uieventListener4;
			uieventListener6.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(uieventListener6.onDrag, new UIEventListener.VectorDelegate(this.OnDragForeground));
		}
		this.ForceUpdate();
	}

	// Token: 0x06004588 RID: 17800 RVA: 0x001129FC File Offset: 0x00110BFC
	private void Update()
	{
		if (this.mIsDirty)
		{
			this.ForceUpdate();
		}
	}

	// Token: 0x06004589 RID: 17801 RVA: 0x00112A10 File Offset: 0x00110C10
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
			if (this.mDir == UIScrollBar.Direction.Horizontal)
			{
				Vector2 vector2;
				vector2..ctor(vector.x * this.mSize, vector.y);
				this.mFG.pivot = UIWidget.Pivot.Left;
				this.mBG.pivot = UIWidget.Pivot.Left;
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
				this.mFG.pivot = UIWidget.Pivot.Top;
				this.mBG.pivot = UIWidget.Pivot.Top;
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

	// Token: 0x040025D8 RID: 9688
	[HideInInspector]
	[SerializeField]
	private UISprite mBG;

	// Token: 0x040025D9 RID: 9689
	[SerializeField]
	[HideInInspector]
	private UISprite mFG;

	// Token: 0x040025DA RID: 9690
	[HideInInspector]
	[SerializeField]
	private UIScrollBar.Direction mDir;

	// Token: 0x040025DB RID: 9691
	[SerializeField]
	[HideInInspector]
	private bool mInverted;

	// Token: 0x040025DC RID: 9692
	[HideInInspector]
	[SerializeField]
	private float mScroll;

	// Token: 0x040025DD RID: 9693
	[HideInInspector]
	[SerializeField]
	private float mSize = 1f;

	// Token: 0x040025DE RID: 9694
	private Transform mTrans;

	// Token: 0x040025DF RID: 9695
	private bool mIsDirty;

	// Token: 0x040025E0 RID: 9696
	private Camera mCam;

	// Token: 0x040025E1 RID: 9697
	private Vector2 mScreenPos = Vector2.zero;

	// Token: 0x040025E2 RID: 9698
	public UIScrollBar.OnScrollBarChange onChange;

	// Token: 0x0200077C RID: 1916
	public enum Direction
	{
		// Token: 0x040025E4 RID: 9700
		Horizontal,
		// Token: 0x040025E5 RID: 9701
		Vertical
	}

	// Token: 0x020008E5 RID: 2277
	// (Invoke) Token: 0x06004D6C RID: 19820
	public delegate void OnScrollBarChange(UIScrollBar sb);
}
