using System;
using UnityEngine;

// Token: 0x0200076E RID: 1902
[RequireComponent(typeof(Camera))]
[AddComponentMenu("NGUI/Interaction/Draggable Camera")]
public class UIDraggableCamera : IgnoreTimeScale
{
	// Token: 0x17000D71 RID: 3441
	// (get) Token: 0x06004518 RID: 17688 RVA: 0x0010F504 File Offset: 0x0010D704
	// (set) Token: 0x06004519 RID: 17689 RVA: 0x0010F50C File Offset: 0x0010D70C
	public Vector2 currentMomentum
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

	// Token: 0x0600451A RID: 17690 RVA: 0x0010F518 File Offset: 0x0010D718
	private void Awake()
	{
		this.mCam = base.camera;
		this.mTrans = base.transform;
		if (this.rootForBounds == null)
		{
			Debug.LogError(NGUITools.GetHierarchy(base.gameObject) + " needs the 'Root For Bounds' parameter to be set", this);
			base.enabled = false;
		}
	}

	// Token: 0x0600451B RID: 17691 RVA: 0x0010F570 File Offset: 0x0010D770
	private void Start()
	{
		this.mRoot = NGUITools.FindInParents<UIRoot>(base.gameObject);
	}

	// Token: 0x0600451C RID: 17692 RVA: 0x0010F584 File Offset: 0x0010D784
	private Vector3 CalculateConstrainOffset()
	{
		if (this.rootForBounds == null || this.rootForBounds.childCount == 0)
		{
			return Vector3.zero;
		}
		Vector3 vector;
		vector..ctor(this.mCam.rect.xMin * (float)Screen.width, this.mCam.rect.yMin * (float)Screen.height, 0f);
		Vector3 vector2;
		vector2..ctor(this.mCam.rect.xMax * (float)Screen.width, this.mCam.rect.yMax * (float)Screen.height, 0f);
		vector = this.mCam.ScreenToWorldPoint(vector);
		vector2 = this.mCam.ScreenToWorldPoint(vector2);
		Vector2 minRect;
		minRect..ctor(this.mBounds.min.x, this.mBounds.min.y);
		Vector2 maxRect;
		maxRect..ctor(this.mBounds.max.x, this.mBounds.max.y);
		return NGUIMath.ConstrainRect(minRect, maxRect, vector, vector2);
	}

	// Token: 0x0600451D RID: 17693 RVA: 0x0010F6CC File Offset: 0x0010D8CC
	public bool ConstrainToBounds(bool immediate)
	{
		if (this.mTrans != null && this.rootForBounds != null)
		{
			Vector3 vector = this.CalculateConstrainOffset();
			if (vector.magnitude > 0f)
			{
				if (immediate)
				{
					this.mTrans.position -= vector;
				}
				else
				{
					SpringPosition springPosition = SpringPosition.Begin(base.gameObject, this.mTrans.position - vector, 13f);
					springPosition.ignoreTimeScale = true;
					springPosition.worldSpace = true;
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600451E RID: 17694 RVA: 0x0010F768 File Offset: 0x0010D968
	public void Press(bool isPressed)
	{
		if (this.rootForBounds != null)
		{
			this.mPressed = isPressed;
			if (isPressed)
			{
				this.mBounds = NGUIMath.CalculateAbsoluteWidgetBounds(this.rootForBounds);
				this.mMomentum = Vector2.zero;
				this.mScroll = 0f;
				SpringPosition component = base.GetComponent<SpringPosition>();
				if (component != null)
				{
					component.enabled = false;
				}
			}
			else if (this.dragEffect == UIDragObject.DragEffect.MomentumAndSpring)
			{
				this.ConstrainToBounds(false);
			}
		}
	}

	// Token: 0x0600451F RID: 17695 RVA: 0x0010F7F0 File Offset: 0x0010D9F0
	public void Drag(Vector2 delta)
	{
		UICamera.currentTouch.clickNotification = UICamera.ClickNotification.BasedOnDelta;
		if (this.mRoot != null && !this.mRoot.automatic)
		{
			delta *= (float)this.mRoot.manualHeight / (float)Screen.height;
		}
		Vector2 vector = Vector2.Scale(delta, -this.scale);
		this.mTrans.localPosition += vector;
		this.mMomentum = Vector2.Lerp(this.mMomentum, this.mMomentum + vector * (0.01f * this.momentumAmount), 0.67f);
		if (this.dragEffect != UIDragObject.DragEffect.MomentumAndSpring && this.ConstrainToBounds(true))
		{
			this.mMomentum = Vector2.zero;
			this.mScroll = 0f;
		}
	}

	// Token: 0x06004520 RID: 17696 RVA: 0x0010F8D4 File Offset: 0x0010DAD4
	public void Scroll(float delta)
	{
		if (base.enabled && base.gameObject.activeInHierarchy)
		{
			if (Mathf.Sign(this.mScroll) != Mathf.Sign(delta))
			{
				this.mScroll = 0f;
			}
			this.mScroll += delta * this.scrollWheelFactor;
		}
	}

	// Token: 0x06004521 RID: 17697 RVA: 0x0010F934 File Offset: 0x0010DB34
	private void Update()
	{
		float deltaTime = base.UpdateRealTimeDelta();
		if (this.mPressed)
		{
			SpringPosition component = base.GetComponent<SpringPosition>();
			if (component != null)
			{
				component.enabled = false;
			}
			this.mScroll = 0f;
		}
		else
		{
			this.mMomentum += this.scale * (this.mScroll * 20f);
			this.mScroll = NGUIMath.SpringLerp(this.mScroll, 0f, 20f, deltaTime);
			if (this.mMomentum.magnitude > 0.01f)
			{
				this.mTrans.localPosition += NGUIMath.SpringDampen(ref this.mMomentum, 9f, deltaTime);
				this.mBounds = NGUIMath.CalculateAbsoluteWidgetBounds(this.rootForBounds);
				if (!this.ConstrainToBounds(this.dragEffect == UIDragObject.DragEffect.None))
				{
					SpringPosition component2 = base.GetComponent<SpringPosition>();
					if (component2 != null)
					{
						component2.enabled = false;
					}
				}
				return;
			}
			this.mScroll = 0f;
		}
		NGUIMath.SpringDampen(ref this.mMomentum, 9f, deltaTime);
	}

	// Token: 0x04002565 RID: 9573
	public Transform rootForBounds;

	// Token: 0x04002566 RID: 9574
	public Vector2 scale = Vector2.one;

	// Token: 0x04002567 RID: 9575
	public float scrollWheelFactor;

	// Token: 0x04002568 RID: 9576
	public UIDragObject.DragEffect dragEffect = UIDragObject.DragEffect.MomentumAndSpring;

	// Token: 0x04002569 RID: 9577
	public float momentumAmount = 35f;

	// Token: 0x0400256A RID: 9578
	private Camera mCam;

	// Token: 0x0400256B RID: 9579
	private Transform mTrans;

	// Token: 0x0400256C RID: 9580
	private bool mPressed;

	// Token: 0x0400256D RID: 9581
	private Vector2 mMomentum = Vector2.zero;

	// Token: 0x0400256E RID: 9582
	private AABBox mBounds;

	// Token: 0x0400256F RID: 9583
	private float mScroll;

	// Token: 0x04002570 RID: 9584
	private UIRoot mRoot;
}
