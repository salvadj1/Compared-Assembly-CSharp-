using System;
using UnityEngine;

// Token: 0x02000851 RID: 2129
[AddComponentMenu("NGUI/Interaction/Draggable Panel")]
[RequireComponent(typeof(global::UIPanel))]
[ExecuteInEditMode]
public class UIDraggablePanel : global::IgnoreTimeScale
{
	// Token: 0x1400006F RID: 111
	// (add) Token: 0x06004984 RID: 18820 RVA: 0x0011944C File Offset: 0x0011764C
	// (remove) Token: 0x06004985 RID: 18821 RVA: 0x00119468 File Offset: 0x00117668
	public event global::UIDraggablePanel.CalculatedNextChangeCallback onNextChangeCallback
	{
		add
		{
			this.calculatedNextChangeCallback = (global::UIDraggablePanel.CalculatedNextChangeCallback)Delegate.Combine(this.calculatedNextChangeCallback, value);
		}
		remove
		{
			this.calculatedNextChangeCallback = (global::UIDraggablePanel.CalculatedNextChangeCallback)Delegate.Remove(this.calculatedNextChangeCallback, value);
		}
	}

	// Token: 0x17000E02 RID: 3586
	// (get) Token: 0x06004986 RID: 18822 RVA: 0x00119484 File Offset: 0x00117684
	// (set) Token: 0x06004987 RID: 18823 RVA: 0x0011948C File Offset: 0x0011768C
	public bool calculateBoundsEveryChange
	{
		get
		{
			return this._calculateBoundsEveryChange;
		}
		set
		{
			if (value)
			{
				if (!this._calculateBoundsEveryChange)
				{
					this.CalculateBoundsIfNeeded();
					this._calculateBoundsEveryChange = true;
				}
			}
			else
			{
				this._calculateBoundsEveryChange = false;
			}
		}
	}

	// Token: 0x17000E03 RID: 3587
	// (get) Token: 0x06004988 RID: 18824 RVA: 0x001194BC File Offset: 0x001176BC
	public bool panelMayNeedBoundsCalculated
	{
		get
		{
			return this._panelMayNeedBoundCalculation;
		}
	}

	// Token: 0x17000E04 RID: 3588
	// (set) Token: 0x06004989 RID: 18825 RVA: 0x001194C4 File Offset: 0x001176C4
	public bool calculateNextChange
	{
		set
		{
			if (value)
			{
				this._calculateNextChange = true;
			}
		}
	}

	// Token: 0x17000E05 RID: 3589
	// (get) Token: 0x0600498A RID: 18826 RVA: 0x001194D4 File Offset: 0x001176D4
	public global::AABBox bounds
	{
		get
		{
			if (!this.mCalculatedBounds)
			{
				this.mCalculatedBounds = true;
				this.mBounds = global::NGUIMath.CalculateRelativeWidgetBounds(this.mTrans, this.mTrans);
			}
			return this.mBounds;
		}
	}

	// Token: 0x17000E06 RID: 3590
	// (get) Token: 0x0600498B RID: 18827 RVA: 0x00119508 File Offset: 0x00117708
	public bool shouldMoveHorizontally
	{
		get
		{
			float num = this.bounds.size.x;
			if (this.mPanel.clipping == global::UIDrawCall.Clipping.SoftClip)
			{
				num += this.mPanel.clipSoftness.x * 2f;
			}
			return num > this.mPanel.clipRange.z;
		}
	}

	// Token: 0x17000E07 RID: 3591
	// (get) Token: 0x0600498C RID: 18828 RVA: 0x00119570 File Offset: 0x00117770
	public bool shouldMoveVertically
	{
		get
		{
			float num = this.bounds.size.y;
			if (this.mPanel.clipping == global::UIDrawCall.Clipping.SoftClip)
			{
				num += this.mPanel.clipSoftness.y * 2f;
			}
			return num > this.mPanel.clipRange.w;
		}
	}

	// Token: 0x17000E08 RID: 3592
	// (get) Token: 0x0600498D RID: 18829 RVA: 0x001195D8 File Offset: 0x001177D8
	private bool shouldMove
	{
		get
		{
			if (!this.disableDragIfFits)
			{
				return true;
			}
			if (this.mPanel == null)
			{
				this.mPanel = base.GetComponent<global::UIPanel>();
			}
			Vector4 clipRange = this.mPanel.clipRange;
			global::AABBox bounds = this.bounds;
			float num = clipRange.z * 0.5f;
			float num2 = clipRange.w * 0.5f;
			if (!Mathf.Approximately(this.scale.x, 0f))
			{
				if (bounds.min.x < clipRange.x - num)
				{
					return true;
				}
				if (bounds.max.x > clipRange.x + num)
				{
					return true;
				}
			}
			if (!Mathf.Approximately(this.scale.y, 0f))
			{
				if (bounds.min.y < clipRange.y - num2)
				{
					return true;
				}
				if (bounds.max.y > clipRange.y + num2)
				{
					return true;
				}
			}
			return false;
		}
	}

	// Token: 0x17000E09 RID: 3593
	// (get) Token: 0x0600498E RID: 18830 RVA: 0x001196F4 File Offset: 0x001178F4
	// (set) Token: 0x0600498F RID: 18831 RVA: 0x001196FC File Offset: 0x001178FC
	public Vector3 currentMomentum
	{
		get
		{
			return this.mMomentum;
		}
		set
		{
			this.mMomentum = value;
		}
	}

	// Token: 0x06004990 RID: 18832 RVA: 0x00119708 File Offset: 0x00117908
	private void Awake()
	{
		this.mTrans = base.transform;
		this.mPanel = base.GetComponent<global::UIPanel>();
	}

	// Token: 0x06004991 RID: 18833 RVA: 0x00119724 File Offset: 0x00117924
	private void Start()
	{
		if (this.mStartedManually)
		{
			return;
		}
		this.UpdateScrollbars(true);
		if (this.horizontalScrollBar != null)
		{
			global::UIScrollBar uiscrollBar = this.horizontalScrollBar;
			uiscrollBar.onChange = (global::UIScrollBar.OnScrollBarChange)Delegate.Combine(uiscrollBar.onChange, new global::UIScrollBar.OnScrollBarChange(this.OnHorizontalBar));
			this.horizontalScrollBar.alpha = ((this.showScrollBars != global::UIDraggablePanel.ShowCondition.Always && !this.shouldMoveHorizontally) ? 0f : 1f);
		}
		if (this.verticalScrollBar != null)
		{
			global::UIScrollBar uiscrollBar2 = this.verticalScrollBar;
			uiscrollBar2.onChange = (global::UIScrollBar.OnScrollBarChange)Delegate.Combine(uiscrollBar2.onChange, new global::UIScrollBar.OnScrollBarChange(this.OnVerticalBar));
			this.verticalScrollBar.alpha = ((this.showScrollBars != global::UIDraggablePanel.ShowCondition.Always && !this.shouldMoveVertically) ? 0f : 1f);
		}
		this.mStartedAutomatically = true;
	}

	// Token: 0x06004992 RID: 18834 RVA: 0x0011981C File Offset: 0x00117A1C
	public bool ManualStart()
	{
		if (!this.mStartedManually)
		{
			if (!this.mStartedAutomatically)
			{
				this.Start();
				this.mStartedManually = true;
				return true;
			}
		}
		return false;
	}

	// Token: 0x06004993 RID: 18835 RVA: 0x0011984C File Offset: 0x00117A4C
	public void RestrictWithinBounds(bool instant)
	{
		Vector3 vector = this.mPanel.CalculateConstrainOffset(this.bounds.min, this.bounds.max);
		if (vector.magnitude > 0.001f)
		{
			if (!instant && this.dragEffect == global::UIDraggablePanel.DragEffect.MomentumAndSpring)
			{
				global::SpringPanel.Begin(this.mPanel.gameObject, this.mTrans.localPosition + vector, 13f);
			}
			else
			{
				this.MoveRelative(vector);
				this.mMomentum = Vector3.zero;
				this.mScroll = 0f;
			}
		}
		else
		{
			this.DisableSpring();
		}
	}

	// Token: 0x06004994 RID: 18836 RVA: 0x00119904 File Offset: 0x00117B04
	public void DisableSpring()
	{
		global::SpringPanel component = base.GetComponent<global::SpringPanel>();
		if (component != null)
		{
			component.enabled = false;
		}
	}

	// Token: 0x06004995 RID: 18837 RVA: 0x0011992C File Offset: 0x00117B2C
	public void UpdateScrollbars(bool recalculateBounds)
	{
		if (this.mPanel == null)
		{
			return;
		}
		if (this.horizontalScrollBar != null || this.verticalScrollBar != null)
		{
			if (recalculateBounds)
			{
				this.mCalculatedBounds = false;
				this._panelMayNeedBoundCalculation = false;
				this.mShouldMove = this.shouldMove;
			}
			if (this.horizontalScrollBar != null)
			{
				global::AABBox bounds = this.bounds;
				Vector3 size = bounds.size;
				if (size.x > 0f)
				{
					Vector4 clipRange = this.mPanel.clipRange;
					float num = clipRange.z * 0.5f;
					float num2 = clipRange.x - num - bounds.min.x;
					float num3 = bounds.max.x - num - clipRange.x;
					if (this.mPanel.clipping == global::UIDrawCall.Clipping.SoftClip)
					{
						num2 += this.mPanel.clipSoftness.x;
						num3 -= this.mPanel.clipSoftness.x;
					}
					num2 = Mathf.Clamp01(num2 / size.x);
					num3 = Mathf.Clamp01(num3 / size.x);
					float num4 = num2 + num3;
					this.mIgnoreCallbacks = true;
					this.horizontalScrollBar.barSize = 1f - num4;
					this.horizontalScrollBar.scrollValue = ((num4 <= 0.001f) ? 0f : (num2 / num4));
					this.mIgnoreCallbacks = false;
				}
			}
			if (this.verticalScrollBar != null)
			{
				global::AABBox bounds2 = this.bounds;
				Vector3 size2 = bounds2.size;
				if (size2.y > 0f)
				{
					Vector4 clipRange2 = this.mPanel.clipRange;
					float num5 = clipRange2.w * 0.5f;
					float num6 = clipRange2.y - num5 - bounds2.min.y;
					float num7 = bounds2.max.y - num5 - clipRange2.y;
					if (this.mPanel.clipping == global::UIDrawCall.Clipping.SoftClip)
					{
						num6 += this.mPanel.clipSoftness.y;
						num7 -= this.mPanel.clipSoftness.y;
					}
					num6 = Mathf.Clamp01(num6 / size2.y);
					num7 = Mathf.Clamp01(num7 / size2.y);
					float num8 = num6 + num7;
					this.mIgnoreCallbacks = true;
					this.verticalScrollBar.barSize = 1f - num8;
					this.verticalScrollBar.scrollValue = ((num8 <= 0.001f) ? 0f : (1f - num6 / num8));
					this.mIgnoreCallbacks = false;
				}
			}
		}
		else if (recalculateBounds)
		{
			this.mCalculatedBounds = false;
			this._panelMayNeedBoundCalculation = false;
		}
	}

	// Token: 0x06004996 RID: 18838 RVA: 0x00119C20 File Offset: 0x00117E20
	public void SetDragAmount(float x, float y, bool updateScrollbars)
	{
		this.DisableSpring();
		global::AABBox bounds = this.bounds;
		if (bounds.min.x == bounds.max.x || bounds.min.y == bounds.max.x)
		{
			return;
		}
		Vector4 clipRange = this.mPanel.clipRange;
		float num = clipRange.z * 0.5f;
		float num2 = clipRange.w * 0.5f;
		float num3 = bounds.min.x + num;
		float num4 = bounds.max.x - num;
		float num5 = bounds.min.y + num2;
		float num6 = bounds.max.y - num2;
		if (this.mPanel.clipping == global::UIDrawCall.Clipping.SoftClip)
		{
			num3 -= this.mPanel.clipSoftness.x;
			num4 += this.mPanel.clipSoftness.x;
			num5 -= this.mPanel.clipSoftness.y;
			num6 += this.mPanel.clipSoftness.y;
		}
		float num7 = Mathf.Lerp(num3, num4, x);
		float num8 = Mathf.Lerp(num6, num5, y);
		if (!updateScrollbars)
		{
			Vector3 localPosition = this.mTrans.localPosition;
			if (this.scale.x != 0f)
			{
				localPosition.x += clipRange.x - num7;
			}
			if (this.scale.y != 0f)
			{
				localPosition.y += clipRange.y - num8;
			}
			this.mTrans.localPosition = localPosition;
		}
		clipRange.x = num7;
		clipRange.y = num8;
		this.mPanel.clipRange = clipRange;
		if (updateScrollbars)
		{
			this.UpdateScrollbars(false);
		}
	}

	// Token: 0x06004997 RID: 18839 RVA: 0x00119E30 File Offset: 0x00118030
	public void ResetPosition()
	{
		this.mCalculatedBounds = false;
		this.SetDragAmount(this.relativePositionOnReset.x, this.relativePositionOnReset.y, false);
		this.SetDragAmount(this.relativePositionOnReset.x, this.relativePositionOnReset.y, true);
	}

	// Token: 0x06004998 RID: 18840 RVA: 0x00119E80 File Offset: 0x00118080
	private void OnHorizontalBar(global::UIScrollBar sb)
	{
		if (!this.mIgnoreCallbacks)
		{
			float x = (!(this.horizontalScrollBar != null)) ? 0f : this.horizontalScrollBar.scrollValue;
			float y = (!(this.verticalScrollBar != null)) ? 0f : this.verticalScrollBar.scrollValue;
			this.SetDragAmount(x, y, false);
		}
	}

	// Token: 0x06004999 RID: 18841 RVA: 0x00119EF0 File Offset: 0x001180F0
	private void OnVerticalBar(global::UIScrollBar sb)
	{
		if (!this.mIgnoreCallbacks)
		{
			float x = (!(this.horizontalScrollBar != null)) ? 0f : this.horizontalScrollBar.scrollValue;
			float y = (!(this.verticalScrollBar != null)) ? 0f : this.verticalScrollBar.scrollValue;
			this.SetDragAmount(x, y, false);
		}
	}

	// Token: 0x0600499A RID: 18842 RVA: 0x00119F60 File Offset: 0x00118160
	private void MoveRelative(Vector3 relative)
	{
		this.mTrans.localPosition += relative;
		Vector4 clipRange = this.mPanel.clipRange;
		clipRange.x -= relative.x;
		clipRange.y -= relative.y;
		this.mPanel.clipRange = clipRange;
		this.UpdateScrollbars(false);
	}

	// Token: 0x0600499B RID: 18843 RVA: 0x00119FD0 File Offset: 0x001181D0
	private void MoveAbsolute(Vector3 absolute)
	{
		Vector3 vector = this.mTrans.InverseTransformPoint(absolute);
		Vector3 vector2 = this.mTrans.InverseTransformPoint(Vector3.zero);
		this.MoveRelative(vector - vector2);
	}

	// Token: 0x0600499C RID: 18844 RVA: 0x0011A008 File Offset: 0x00118208
	public void Press(bool pressed)
	{
		if (base.enabled && base.gameObject.activeInHierarchy)
		{
			this.mTouches += ((!pressed) ? -1 : 1);
			this.mCalculatedBounds = false;
			this.mShouldMove = this.shouldMove;
			if (!this.mShouldMove)
			{
				return;
			}
			this.mPressed = pressed;
			if (pressed)
			{
				this.mMomentum = Vector3.zero;
				this.mScroll = 0f;
				this.DisableSpring();
				this.mLastPos = global::UICamera.lastHit.point;
				this.mPlane = new Plane(this.mTrans.rotation * Vector3.back, this.mLastPos);
			}
			else if (this.restrictWithinPanel && this.mPanel.clipping != global::UIDrawCall.Clipping.None && this.dragEffect == global::UIDraggablePanel.DragEffect.MomentumAndSpring)
			{
				this.RestrictWithinBounds(false);
			}
		}
	}

	// Token: 0x0600499D RID: 18845 RVA: 0x0011A0FC File Offset: 0x001182FC
	public void Drag(Vector2 delta)
	{
		if (base.enabled && base.gameObject.activeInHierarchy && this.mShouldMove)
		{
			global::UICamera.currentTouch.clickNotification = global::UICamera.ClickNotification.BasedOnDelta;
			Ray ray = global::UICamera.currentCamera.ScreenPointToRay(global::UICamera.currentTouch.pos);
			float num = 0f;
			if (this.mPlane.Raycast(ray, ref num))
			{
				Vector3 point = ray.GetPoint(num);
				Vector3 vector = point - this.mLastPos;
				this.mLastPos = point;
				if (vector.x != 0f || vector.y != 0f)
				{
					vector = this.mTrans.InverseTransformDirection(vector);
					vector.Scale(this.scale);
					vector = this.mTrans.TransformDirection(vector);
				}
				this.mMomentum = Vector3.Lerp(this.mMomentum, this.mMomentum + vector * (0.01f * this.momentumAmount), 0.67f);
				this.MoveAbsolute(vector);
				if (this.restrictWithinPanel && this.mPanel.clipping != global::UIDrawCall.Clipping.None && this.dragEffect != global::UIDraggablePanel.DragEffect.MomentumAndSpring)
				{
					this.RestrictWithinBounds(false);
				}
			}
		}
	}

	// Token: 0x0600499E RID: 18846 RVA: 0x0011A23C File Offset: 0x0011843C
	public void Scroll(float delta)
	{
		if (base.enabled && base.gameObject.activeInHierarchy)
		{
			this.mShouldMove = this.shouldMove;
			if (Mathf.Sign(this.mScroll) != Mathf.Sign(delta))
			{
				this.mScroll = 0f;
			}
			this.mScroll += delta * this.scrollWheelFactor;
		}
	}

	// Token: 0x0600499F RID: 18847 RVA: 0x0011A2A8 File Offset: 0x001184A8
	private void OnPanelChanged()
	{
		if (this._calculateNextChange)
		{
			this._calculateNextChange = false;
			this.UpdateScrollbars(true);
			if (this.calculatedNextChangeCallback != null)
			{
				global::UIDraggablePanel.CalculatedNextChangeCallback calculatedNextChangeCallback = this.calculatedNextChangeCallback;
				this.calculatedNextChangeCallback = null;
				calculatedNextChangeCallback();
			}
		}
		else if (!Application.isPlaying || this._calculateBoundsEveryChange)
		{
			this.UpdateScrollbars(true);
		}
		else
		{
			this._panelMayNeedBoundCalculation = true;
		}
	}

	// Token: 0x060049A0 RID: 18848 RVA: 0x0011A31C File Offset: 0x0011851C
	public bool CalculateBoundsIfNeeded()
	{
		if (this._panelMayNeedBoundCalculation)
		{
			this.UpdateScrollbars(true);
			return !this._panelMayNeedBoundCalculation;
		}
		return false;
	}

	// Token: 0x060049A1 RID: 18849 RVA: 0x0011A33C File Offset: 0x0011853C
	private void LateUpdate()
	{
		if (!this.mPanel.enabled)
		{
			this.mMomentum = Vector3.zero;
			return;
		}
		if (this.mPanel.changedLastFrame)
		{
			this.OnPanelChanged();
		}
		if (this.repositionClipping)
		{
			this.repositionClipping = false;
			this.mCalculatedBounds = false;
			this.SetDragAmount(this.relativePositionOnReset.x, this.relativePositionOnReset.y, true);
		}
		if (!Application.isPlaying)
		{
			return;
		}
		float num = base.UpdateRealTimeDelta();
		if (this.showScrollBars != global::UIDraggablePanel.ShowCondition.Always)
		{
			bool flag = false;
			bool flag2 = false;
			if (this.showScrollBars != global::UIDraggablePanel.ShowCondition.WhenDragging || this.mTouches > 0)
			{
				flag = this.shouldMoveVertically;
				flag2 = this.shouldMoveHorizontally;
			}
			if (this.verticalScrollBar)
			{
				float num2 = this.verticalScrollBar.alpha;
				num2 += ((!flag) ? (-num * 3f) : (num * 6f));
				num2 = Mathf.Clamp01(num2);
				if (this.verticalScrollBar.alpha != num2)
				{
					this.verticalScrollBar.alpha = num2;
				}
			}
			if (this.horizontalScrollBar)
			{
				float num3 = this.horizontalScrollBar.alpha;
				num3 += ((!flag2) ? (-num * 3f) : (num * 6f));
				num3 = Mathf.Clamp01(num3);
				if (this.horizontalScrollBar.alpha != num3)
				{
					this.horizontalScrollBar.alpha = num3;
				}
			}
		}
		if (this.mShouldMove && !this.mPressed)
		{
			this.mMomentum += this.scale * (-this.mScroll * 0.05f);
			if (this.mMomentum.magnitude > 0.0001f)
			{
				this.mScroll = global::NGUIMath.SpringLerp(this.mScroll, 0f, 20f, num);
				Vector3 absolute = global::NGUIMath.SpringDampen(ref this.mMomentum, 9f, num);
				this.MoveAbsolute(absolute);
				if ((this.restrictWithinPanel || this.restrictWithinPanelWithScroll) && this.mPanel.clipping != global::UIDrawCall.Clipping.None)
				{
					this.RestrictWithinBounds(false);
				}
				return;
			}
			this.mScroll = 0f;
		}
		else
		{
			this.mScroll = 0f;
		}
		global::NGUIMath.SpringDampen(ref this.mMomentum, 9f, num);
	}

	// Token: 0x060049A2 RID: 18850 RVA: 0x0011A5A0 File Offset: 0x001187A0
	private void OnHoverScroll(float y)
	{
		if (this.respondHoverScroll)
		{
			this.Scroll(y);
		}
	}

	// Token: 0x040027A8 RID: 10152
	public bool restrictWithinPanel = true;

	// Token: 0x040027A9 RID: 10153
	public bool restrictWithinPanelWithScroll = true;

	// Token: 0x040027AA RID: 10154
	public bool disableDragIfFits;

	// Token: 0x040027AB RID: 10155
	public global::UIDraggablePanel.DragEffect dragEffect = global::UIDraggablePanel.DragEffect.MomentumAndSpring;

	// Token: 0x040027AC RID: 10156
	public Vector3 scale = Vector3.one;

	// Token: 0x040027AD RID: 10157
	public float scrollWheelFactor;

	// Token: 0x040027AE RID: 10158
	public float momentumAmount = 35f;

	// Token: 0x040027AF RID: 10159
	public Vector2 relativePositionOnReset = Vector2.zero;

	// Token: 0x040027B0 RID: 10160
	public bool repositionClipping;

	// Token: 0x040027B1 RID: 10161
	public global::UIScrollBar horizontalScrollBar;

	// Token: 0x040027B2 RID: 10162
	public global::UIScrollBar verticalScrollBar;

	// Token: 0x040027B3 RID: 10163
	public global::UIDraggablePanel.ShowCondition showScrollBars = global::UIDraggablePanel.ShowCondition.OnlyIfNeeded;

	// Token: 0x040027B4 RID: 10164
	[SerializeField]
	private bool _calculateBoundsEveryChange = true;

	// Token: 0x040027B5 RID: 10165
	private bool _panelMayNeedBoundCalculation;

	// Token: 0x040027B6 RID: 10166
	private Transform mTrans;

	// Token: 0x040027B7 RID: 10167
	private global::UIPanel mPanel;

	// Token: 0x040027B8 RID: 10168
	private Plane mPlane;

	// Token: 0x040027B9 RID: 10169
	private Vector3 mLastPos;

	// Token: 0x040027BA RID: 10170
	private bool mPressed;

	// Token: 0x040027BB RID: 10171
	private Vector3 mMomentum = Vector3.zero;

	// Token: 0x040027BC RID: 10172
	private float mScroll;

	// Token: 0x040027BD RID: 10173
	private global::AABBox mBounds;

	// Token: 0x040027BE RID: 10174
	private bool mCalculatedBounds;

	// Token: 0x040027BF RID: 10175
	private bool mShouldMove;

	// Token: 0x040027C0 RID: 10176
	private bool mIgnoreCallbacks;

	// Token: 0x040027C1 RID: 10177
	private bool mStartedManually;

	// Token: 0x040027C2 RID: 10178
	private bool mStartedAutomatically;

	// Token: 0x040027C3 RID: 10179
	private int mTouches;

	// Token: 0x040027C4 RID: 10180
	private bool _calculateNextChange;

	// Token: 0x040027C5 RID: 10181
	public bool respondHoverScroll = true;

	// Token: 0x040027C6 RID: 10182
	private global::UIDraggablePanel.CalculatedNextChangeCallback calculatedNextChangeCallback;

	// Token: 0x02000852 RID: 2130
	public enum DragEffect
	{
		// Token: 0x040027C8 RID: 10184
		None,
		// Token: 0x040027C9 RID: 10185
		Momentum,
		// Token: 0x040027CA RID: 10186
		MomentumAndSpring
	}

	// Token: 0x02000853 RID: 2131
	public enum ShowCondition
	{
		// Token: 0x040027CC RID: 10188
		Always,
		// Token: 0x040027CD RID: 10189
		OnlyIfNeeded,
		// Token: 0x040027CE RID: 10190
		WhenDragging
	}

	// Token: 0x02000854 RID: 2132
	// (Invoke) Token: 0x060049A4 RID: 18852
	public delegate void CalculatedNextChangeCallback();
}
