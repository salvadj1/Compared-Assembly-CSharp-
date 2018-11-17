using System;
using UnityEngine;

// Token: 0x02000850 RID: 2128
[AddComponentMenu("NGUI/Interaction/Draggable Camera")]
[RequireComponent(typeof(Camera))]
public class UIDraggableCamera : global::IgnoreTimeScale
{
	// Token: 0x17000E01 RID: 3585
	// (get) Token: 0x06004979 RID: 18809 RVA: 0x00118E84 File Offset: 0x00117084
	// (set) Token: 0x0600497A RID: 18810 RVA: 0x00118E8C File Offset: 0x0011708C
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

	// Token: 0x0600497B RID: 18811 RVA: 0x00118E98 File Offset: 0x00117098
	private void Awake()
	{
		this.mCam = base.camera;
		this.mTrans = base.transform;
		if (this.rootForBounds == null)
		{
			Debug.LogError(global::NGUITools.GetHierarchy(base.gameObject) + " needs the 'Root For Bounds' parameter to be set", this);
			base.enabled = false;
		}
	}

	// Token: 0x0600497C RID: 18812 RVA: 0x00118EF0 File Offset: 0x001170F0
	private void Start()
	{
		this.mRoot = global::NGUITools.FindInParents<global::UIRoot>(base.gameObject);
	}

	// Token: 0x0600497D RID: 18813 RVA: 0x00118F04 File Offset: 0x00117104
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
		return global::NGUIMath.ConstrainRect(minRect, maxRect, vector, vector2);
	}

	// Token: 0x0600497E RID: 18814 RVA: 0x0011904C File Offset: 0x0011724C
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
					global::SpringPosition springPosition = global::SpringPosition.Begin(base.gameObject, this.mTrans.position - vector, 13f);
					springPosition.ignoreTimeScale = true;
					springPosition.worldSpace = true;
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600497F RID: 18815 RVA: 0x001190E8 File Offset: 0x001172E8
	public void Press(bool isPressed)
	{
		if (this.rootForBounds != null)
		{
			this.mPressed = isPressed;
			if (isPressed)
			{
				this.mBounds = global::NGUIMath.CalculateAbsoluteWidgetBounds(this.rootForBounds);
				this.mMomentum = Vector2.zero;
				this.mScroll = 0f;
				global::SpringPosition component = base.GetComponent<global::SpringPosition>();
				if (component != null)
				{
					component.enabled = false;
				}
			}
			else if (this.dragEffect == global::UIDragObject.DragEffect.MomentumAndSpring)
			{
				this.ConstrainToBounds(false);
			}
		}
	}

	// Token: 0x06004980 RID: 18816 RVA: 0x00119170 File Offset: 0x00117370
	public void Drag(Vector2 delta)
	{
		global::UICamera.currentTouch.clickNotification = global::UICamera.ClickNotification.BasedOnDelta;
		if (this.mRoot != null && !this.mRoot.automatic)
		{
			delta *= (float)this.mRoot.manualHeight / (float)Screen.height;
		}
		Vector2 vector = Vector2.Scale(delta, -this.scale);
		this.mTrans.localPosition += vector;
		this.mMomentum = Vector2.Lerp(this.mMomentum, this.mMomentum + vector * (0.01f * this.momentumAmount), 0.67f);
		if (this.dragEffect != global::UIDragObject.DragEffect.MomentumAndSpring && this.ConstrainToBounds(true))
		{
			this.mMomentum = Vector2.zero;
			this.mScroll = 0f;
		}
	}

	// Token: 0x06004981 RID: 18817 RVA: 0x00119254 File Offset: 0x00117454
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

	// Token: 0x06004982 RID: 18818 RVA: 0x001192B4 File Offset: 0x001174B4
	private void Update()
	{
		float deltaTime = base.UpdateRealTimeDelta();
		if (this.mPressed)
		{
			global::SpringPosition component = base.GetComponent<global::SpringPosition>();
			if (component != null)
			{
				component.enabled = false;
			}
			this.mScroll = 0f;
		}
		else
		{
			this.mMomentum += this.scale * (this.mScroll * 20f);
			this.mScroll = global::NGUIMath.SpringLerp(this.mScroll, 0f, 20f, deltaTime);
			if (this.mMomentum.magnitude > 0.01f)
			{
				this.mTrans.localPosition += global::NGUIMath.SpringDampen(ref this.mMomentum, 9f, deltaTime);
				this.mBounds = global::NGUIMath.CalculateAbsoluteWidgetBounds(this.rootForBounds);
				if (!this.ConstrainToBounds(this.dragEffect == global::UIDragObject.DragEffect.None))
				{
					global::SpringPosition component2 = base.GetComponent<global::SpringPosition>();
					if (component2 != null)
					{
						component2.enabled = false;
					}
				}
				return;
			}
			this.mScroll = 0f;
		}
		global::NGUIMath.SpringDampen(ref this.mMomentum, 9f, deltaTime);
	}

	// Token: 0x0400279C RID: 10140
	public Transform rootForBounds;

	// Token: 0x0400279D RID: 10141
	public Vector2 scale = Vector2.one;

	// Token: 0x0400279E RID: 10142
	public float scrollWheelFactor;

	// Token: 0x0400279F RID: 10143
	public global::UIDragObject.DragEffect dragEffect = global::UIDragObject.DragEffect.MomentumAndSpring;

	// Token: 0x040027A0 RID: 10144
	public float momentumAmount = 35f;

	// Token: 0x040027A1 RID: 10145
	private Camera mCam;

	// Token: 0x040027A2 RID: 10146
	private Transform mTrans;

	// Token: 0x040027A3 RID: 10147
	private bool mPressed;

	// Token: 0x040027A4 RID: 10148
	private Vector2 mMomentum = Vector2.zero;

	// Token: 0x040027A5 RID: 10149
	private global::AABBox mBounds;

	// Token: 0x040027A6 RID: 10150
	private float mScroll;

	// Token: 0x040027A7 RID: 10151
	private global::UIRoot mRoot;
}
