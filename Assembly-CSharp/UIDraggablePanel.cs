using System;
using UnityEngine;

// Token: 0x0200076F RID: 1903
[RequireComponent(typeof(UIPanel))]
[AddComponentMenu("NGUI/Interaction/Draggable Panel")]
[ExecuteInEditMode]
public class UIDraggablePanel : IgnoreTimeScale
{
	// Token: 0x1400006F RID: 111
	// (add) Token: 0x06004523 RID: 17699 RVA: 0x0010FACC File Offset: 0x0010DCCC
	// (remove) Token: 0x06004524 RID: 17700 RVA: 0x0010FAE8 File Offset: 0x0010DCE8
	public event UIDraggablePanel.CalculatedNextChangeCallback onNextChangeCallback
	{
		add
		{
			this.calculatedNextChangeCallback = (UIDraggablePanel.CalculatedNextChangeCallback)Delegate.Combine(this.calculatedNextChangeCallback, value);
		}
		remove
		{
			this.calculatedNextChangeCallback = (UIDraggablePanel.CalculatedNextChangeCallback)Delegate.Remove(this.calculatedNextChangeCallback, value);
		}
	}

	// Token: 0x17000D72 RID: 3442
	// (get) Token: 0x06004525 RID: 17701 RVA: 0x0010FB04 File Offset: 0x0010DD04
	// (set) Token: 0x06004526 RID: 17702 RVA: 0x0010FB0C File Offset: 0x0010DD0C
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

	// Token: 0x17000D73 RID: 3443
	// (get) Token: 0x06004527 RID: 17703 RVA: 0x0010FB3C File Offset: 0x0010DD3C
	public bool panelMayNeedBoundsCalculated
	{
		get
		{
			return this._panelMayNeedBoundCalculation;
		}
	}

	// Token: 0x17000D74 RID: 3444
	// (set) Token: 0x06004528 RID: 17704 RVA: 0x0010FB44 File Offset: 0x0010DD44
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

	// Token: 0x17000D75 RID: 3445
	// (get) Token: 0x06004529 RID: 17705 RVA: 0x0010FB54 File Offset: 0x0010DD54
	public AABBox bounds
	{
		get
		{
			if (!this.mCalculatedBounds)
			{
				this.mCalculatedBounds = true;
				this.mBounds = NGUIMath.CalculateRelativeWidgetBounds(this.mTrans, this.mTrans);
			}
			return this.mBounds;
		}
	}

	// Token: 0x17000D76 RID: 3446
	// (get) Token: 0x0600452A RID: 17706 RVA: 0x0010FB88 File Offset: 0x0010DD88
	public bool shouldMoveHorizontally
	{
		get
		{
			float num = this.bounds.size.x;
			if (this.mPanel.clipping == UIDrawCall.Clipping.SoftClip)
			{
				num += this.mPanel.clipSoftness.x * 2f;
			}
			return num > this.mPanel.clipRange.z;
		}
	}

	// Token: 0x17000D77 RID: 3447
	// (get) Token: 0x0600452B RID: 17707 RVA: 0x0010FBF0 File Offset: 0x0010DDF0
	public bool shouldMoveVertically
	{
		get
		{
			float num = this.bounds.size.y;
			if (this.mPanel.clipping == UIDrawCall.Clipping.SoftClip)
			{
				num += this.mPanel.clipSoftness.y * 2f;
			}
			return num > this.mPanel.clipRange.w;
		}
	}

	// Token: 0x17000D78 RID: 3448
	// (get) Token: 0x0600452C RID: 17708 RVA: 0x0010FC58 File Offset: 0x0010DE58
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
				this.mPanel = base.GetComponent<UIPanel>();
			}
			Vector4 clipRange = this.mPanel.clipRange;
			AABBox bounds = this.bounds;
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

	// Token: 0x17000D79 RID: 3449
	// (get) Token: 0x0600452D RID: 17709 RVA: 0x0010FD74 File Offset: 0x0010DF74
	// (set) Token: 0x0600452E RID: 17710 RVA: 0x0010FD7C File Offset: 0x0010DF7C
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

	// Token: 0x0600452F RID: 17711 RVA: 0x0010FD88 File Offset: 0x0010DF88
	private void Awake()
	{
		this.mTrans = base.transform;
		this.mPanel = base.GetComponent<UIPanel>();
	}

	// Token: 0x06004530 RID: 17712 RVA: 0x0010FDA4 File Offset: 0x0010DFA4
	private void Start()
	{
		if (this.mStartedManually)
		{
			return;
		}
		this.UpdateScrollbars(true);
		if (this.horizontalScrollBar != null)
		{
			UIScrollBar uiscrollBar = this.horizontalScrollBar;
			uiscrollBar.onChange = (UIScrollBar.OnScrollBarChange)Delegate.Combine(uiscrollBar.onChange, new UIScrollBar.OnScrollBarChange(this.OnHorizontalBar));
			this.horizontalScrollBar.alpha = ((this.showScrollBars != UIDraggablePanel.ShowCondition.Always && !this.shouldMoveHorizontally) ? 0f : 1f);
		}
		if (this.verticalScrollBar != null)
		{
			UIScrollBar uiscrollBar2 = this.verticalScrollBar;
			uiscrollBar2.onChange = (UIScrollBar.OnScrollBarChange)Delegate.Combine(uiscrollBar2.onChange, new UIScrollBar.OnScrollBarChange(this.OnVerticalBar));
			this.verticalScrollBar.alpha = ((this.showScrollBars != UIDraggablePanel.ShowCondition.Always && !this.shouldMoveVertically) ? 0f : 1f);
		}
		this.mStartedAutomatically = true;
	}

	// Token: 0x06004531 RID: 17713 RVA: 0x0010FE9C File Offset: 0x0010E09C
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

	// Token: 0x06004532 RID: 17714 RVA: 0x0010FECC File Offset: 0x0010E0CC
	public void RestrictWithinBounds(bool instant)
	{
		Vector3 vector = this.mPanel.CalculateConstrainOffset(this.bounds.min, this.bounds.max);
		if (vector.magnitude > 0.001f)
		{
			if (!instant && this.dragEffect == UIDraggablePanel.DragEffect.MomentumAndSpring)
			{
				SpringPanel.Begin(this.mPanel.gameObject, this.mTrans.localPosition + vector, 13f);
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

	// Token: 0x06004533 RID: 17715 RVA: 0x0010FF84 File Offset: 0x0010E184
	public void DisableSpring()
	{
		SpringPanel component = base.GetComponent<SpringPanel>();
		if (component != null)
		{
			component.enabled = false;
		}
	}

	// Token: 0x06004534 RID: 17716 RVA: 0x0010FFAC File Offset: 0x0010E1AC
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
				AABBox bounds = this.bounds;
				Vector3 size = bounds.size;
				if (size.x > 0f)
				{
					Vector4 clipRange = this.mPanel.clipRange;
					float num = clipRange.z * 0.5f;
					float num2 = clipRange.x - num - bounds.min.x;
					float num3 = bounds.max.x - num - clipRange.x;
					if (this.mPanel.clipping == UIDrawCall.Clipping.SoftClip)
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
				AABBox bounds2 = this.bounds;
				Vector3 size2 = bounds2.size;
				if (size2.y > 0f)
				{
					Vector4 clipRange2 = this.mPanel.clipRange;
					float num5 = clipRange2.w * 0.5f;
					float num6 = clipRange2.y - num5 - bounds2.min.y;
					float num7 = bounds2.max.y - num5 - clipRange2.y;
					if (this.mPanel.clipping == UIDrawCall.Clipping.SoftClip)
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

	// Token: 0x06004535 RID: 17717 RVA: 0x001102A0 File Offset: 0x0010E4A0
	public void SetDragAmount(float x, float y, bool updateScrollbars)
	{
		this.DisableSpring();
		AABBox bounds = this.bounds;
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
		if (this.mPanel.clipping == UIDrawCall.Clipping.SoftClip)
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

	// Token: 0x06004536 RID: 17718 RVA: 0x001104B0 File Offset: 0x0010E6B0
	public void ResetPosition()
	{
		this.mCalculatedBounds = false;
		this.SetDragAmount(this.relativePositionOnReset.x, this.relativePositionOnReset.y, false);
		this.SetDragAmount(this.relativePositionOnReset.x, this.relativePositionOnReset.y, true);
	}

	// Token: 0x06004537 RID: 17719 RVA: 0x00110500 File Offset: 0x0010E700
	private void OnHorizontalBar(UIScrollBar sb)
	{
		if (!this.mIgnoreCallbacks)
		{
			float x = (!(this.horizontalScrollBar != null)) ? 0f : this.horizontalScrollBar.scrollValue;
			float y = (!(this.verticalScrollBar != null)) ? 0f : this.verticalScrollBar.scrollValue;
			this.SetDragAmount(x, y, false);
		}
	}

	// Token: 0x06004538 RID: 17720 RVA: 0x00110570 File Offset: 0x0010E770
	private void OnVerticalBar(UIScrollBar sb)
	{
		if (!this.mIgnoreCallbacks)
		{
			float x = (!(this.horizontalScrollBar != null)) ? 0f : this.horizontalScrollBar.scrollValue;
			float y = (!(this.verticalScrollBar != null)) ? 0f : this.verticalScrollBar.scrollValue;
			this.SetDragAmount(x, y, false);
		}
	}

	// Token: 0x06004539 RID: 17721 RVA: 0x001105E0 File Offset: 0x0010E7E0
	private void MoveRelative(Vector3 relative)
	{
		this.mTrans.localPosition += relative;
		Vector4 clipRange = this.mPanel.clipRange;
		clipRange.x -= relative.x;
		clipRange.y -= relative.y;
		this.mPanel.clipRange = clipRange;
		this.UpdateScrollbars(false);
	}

	// Token: 0x0600453A RID: 17722 RVA: 0x00110650 File Offset: 0x0010E850
	private void MoveAbsolute(Vector3 absolute)
	{
		Vector3 vector = this.mTrans.InverseTransformPoint(absolute);
		Vector3 vector2 = this.mTrans.InverseTransformPoint(Vector3.zero);
		this.MoveRelative(vector - vector2);
	}

	// Token: 0x0600453B RID: 17723 RVA: 0x00110688 File Offset: 0x0010E888
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
				this.mLastPos = UICamera.lastHit.point;
				this.mPlane = new Plane(this.mTrans.rotation * Vector3.back, this.mLastPos);
			}
			else if (this.restrictWithinPanel && this.mPanel.clipping != UIDrawCall.Clipping.None && this.dragEffect == UIDraggablePanel.DragEffect.MomentumAndSpring)
			{
				this.RestrictWithinBounds(false);
			}
		}
	}

	// Token: 0x0600453C RID: 17724 RVA: 0x0011077C File Offset: 0x0010E97C
	public void Drag(Vector2 delta)
	{
		if (base.enabled && base.gameObject.activeInHierarchy && this.mShouldMove)
		{
			UICamera.currentTouch.clickNotification = UICamera.ClickNotification.BasedOnDelta;
			Ray ray = UICamera.currentCamera.ScreenPointToRay(UICamera.currentTouch.pos);
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
				if (this.restrictWithinPanel && this.mPanel.clipping != UIDrawCall.Clipping.None && this.dragEffect != UIDraggablePanel.DragEffect.MomentumAndSpring)
				{
					this.RestrictWithinBounds(false);
				}
			}
		}
	}

	// Token: 0x0600453D RID: 17725 RVA: 0x001108BC File Offset: 0x0010EABC
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

	// Token: 0x0600453E RID: 17726 RVA: 0x00110928 File Offset: 0x0010EB28
	private void OnPanelChanged()
	{
		if (this._calculateNextChange)
		{
			this._calculateNextChange = false;
			this.UpdateScrollbars(true);
			if (this.calculatedNextChangeCallback != null)
			{
				UIDraggablePanel.CalculatedNextChangeCallback calculatedNextChangeCallback = this.calculatedNextChangeCallback;
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

	// Token: 0x0600453F RID: 17727 RVA: 0x0011099C File Offset: 0x0010EB9C
	public bool CalculateBoundsIfNeeded()
	{
		if (this._panelMayNeedBoundCalculation)
		{
			this.UpdateScrollbars(true);
			return !this._panelMayNeedBoundCalculation;
		}
		return false;
	}

	// Token: 0x06004540 RID: 17728 RVA: 0x001109BC File Offset: 0x0010EBBC
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
		if (this.showScrollBars != UIDraggablePanel.ShowCondition.Always)
		{
			bool flag = false;
			bool flag2 = false;
			if (this.showScrollBars != UIDraggablePanel.ShowCondition.WhenDragging || this.mTouches > 0)
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
				this.mScroll = NGUIMath.SpringLerp(this.mScroll, 0f, 20f, num);
				Vector3 absolute = NGUIMath.SpringDampen(ref this.mMomentum, 9f, num);
				this.MoveAbsolute(absolute);
				if ((this.restrictWithinPanel || this.restrictWithinPanelWithScroll) && this.mPanel.clipping != UIDrawCall.Clipping.None)
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
		NGUIMath.SpringDampen(ref this.mMomentum, 9f, num);
	}

	// Token: 0x06004541 RID: 17729 RVA: 0x00110C20 File Offset: 0x0010EE20
	private void OnHoverScroll(float y)
	{
		if (this.respondHoverScroll)
		{
			this.Scroll(y);
		}
	}

	// Token: 0x04002571 RID: 9585
	public bool restrictWithinPanel = true;

	// Token: 0x04002572 RID: 9586
	public bool restrictWithinPanelWithScroll = true;

	// Token: 0x04002573 RID: 9587
	public bool disableDragIfFits;

	// Token: 0x04002574 RID: 9588
	public UIDraggablePanel.DragEffect dragEffect = UIDraggablePanel.DragEffect.MomentumAndSpring;

	// Token: 0x04002575 RID: 9589
	public Vector3 scale = Vector3.one;

	// Token: 0x04002576 RID: 9590
	public float scrollWheelFactor;

	// Token: 0x04002577 RID: 9591
	public float momentumAmount = 35f;

	// Token: 0x04002578 RID: 9592
	public Vector2 relativePositionOnReset = Vector2.zero;

	// Token: 0x04002579 RID: 9593
	public bool repositionClipping;

	// Token: 0x0400257A RID: 9594
	public UIScrollBar horizontalScrollBar;

	// Token: 0x0400257B RID: 9595
	public UIScrollBar verticalScrollBar;

	// Token: 0x0400257C RID: 9596
	public UIDraggablePanel.ShowCondition showScrollBars = UIDraggablePanel.ShowCondition.OnlyIfNeeded;

	// Token: 0x0400257D RID: 9597
	[SerializeField]
	private bool _calculateBoundsEveryChange = true;

	// Token: 0x0400257E RID: 9598
	private bool _panelMayNeedBoundCalculation;

	// Token: 0x0400257F RID: 9599
	private Transform mTrans;

	// Token: 0x04002580 RID: 9600
	private UIPanel mPanel;

	// Token: 0x04002581 RID: 9601
	private Plane mPlane;

	// Token: 0x04002582 RID: 9602
	private Vector3 mLastPos;

	// Token: 0x04002583 RID: 9603
	private bool mPressed;

	// Token: 0x04002584 RID: 9604
	private Vector3 mMomentum = Vector3.zero;

	// Token: 0x04002585 RID: 9605
	private float mScroll;

	// Token: 0x04002586 RID: 9606
	private AABBox mBounds;

	// Token: 0x04002587 RID: 9607
	private bool mCalculatedBounds;

	// Token: 0x04002588 RID: 9608
	private bool mShouldMove;

	// Token: 0x04002589 RID: 9609
	private bool mIgnoreCallbacks;

	// Token: 0x0400258A RID: 9610
	private bool mStartedManually;

	// Token: 0x0400258B RID: 9611
	private bool mStartedAutomatically;

	// Token: 0x0400258C RID: 9612
	private int mTouches;

	// Token: 0x0400258D RID: 9613
	private bool _calculateNextChange;

	// Token: 0x0400258E RID: 9614
	public bool respondHoverScroll = true;

	// Token: 0x0400258F RID: 9615
	private UIDraggablePanel.CalculatedNextChangeCallback calculatedNextChangeCallback;

	// Token: 0x02000770 RID: 1904
	public enum DragEffect
	{
		// Token: 0x04002591 RID: 9617
		None,
		// Token: 0x04002592 RID: 9618
		Momentum,
		// Token: 0x04002593 RID: 9619
		MomentumAndSpring
	}

	// Token: 0x02000771 RID: 1905
	public enum ShowCondition
	{
		// Token: 0x04002595 RID: 9621
		Always,
		// Token: 0x04002596 RID: 9622
		OnlyIfNeeded,
		// Token: 0x04002597 RID: 9623
		WhenDragging
	}

	// Token: 0x020008E4 RID: 2276
	// (Invoke) Token: 0x06004D68 RID: 19816
	public delegate void CalculatedNextChangeCallback();
}
