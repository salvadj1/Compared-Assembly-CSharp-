using System;
using UnityEngine;

// Token: 0x0200076B RID: 1899
[AddComponentMenu("NGUI/Interaction/Drag Object")]
public class UIDragObject : IgnoreTimeScale
{
	// Token: 0x17000D70 RID: 3440
	// (get) Token: 0x0600450B RID: 17675 RVA: 0x0010EBC8 File Offset: 0x0010CDC8
	public static RectOffset screenBorder
	{
		get
		{
			return new RectOffset(0, -64, 0, 0);
		}
	}

	// Token: 0x0600450C RID: 17676 RVA: 0x0010EBD4 File Offset: 0x0010CDD4
	private void FindPanel()
	{
		this.mPanel = ((!(this.target != null)) ? null : UIPanel.Find(this.target.transform, false));
		if (this.mPanel == null)
		{
			this.restrictWithinPanel = false;
		}
	}

	// Token: 0x0600450D RID: 17677 RVA: 0x0010EC28 File Offset: 0x0010CE28
	private void OnPress(bool pressed)
	{
		if (base.enabled && base.gameObject.activeInHierarchy && this.target != null)
		{
			this.mPressed = pressed;
			if (pressed)
			{
				if ((this.restrictWithinPanel || this.restrictToScreen) && this.mPanel == null)
				{
					this.FindPanel();
				}
				if (this.restrictWithinPanel)
				{
					this.mBounds = NGUIMath.CalculateRelativeWidgetBounds(this.mPanel.cachedTransform, this.target);
				}
				if (this.restrictToScreen)
				{
					UICamera uicamera = UICamera.FindCameraForLayer(base.gameObject.layer);
					Rect rect = UIDragObject.screenBorder.Add(uicamera.camera.pixelRect);
					this.mBounds = AABBox.CenterAndSize(rect.center, new Vector3(rect.width, rect.height, 0f));
				}
				this.mMomentum = Vector3.zero;
				this.mScroll = 0f;
				SpringPosition component = this.target.GetComponent<SpringPosition>();
				if (component != null)
				{
					component.enabled = false;
				}
				this.mLastPos = UICamera.lastHit.point;
				Transform transform = UICamera.currentCamera.transform;
				this.mPlane = new Plane(((!(this.mPanel != null)) ? transform.rotation : this.mPanel.cachedTransform.rotation) * Vector3.back, this.mLastPos);
			}
			else if (this.restrictWithinPanel && this.mPanel.clipping != UIDrawCall.Clipping.None && this.dragEffect == UIDragObject.DragEffect.MomentumAndSpring)
			{
				this.mPanel.ConstrainTargetToBounds(this.target, ref this.mBounds, false);
			}
		}
	}

	// Token: 0x0600450E RID: 17678 RVA: 0x0010EE00 File Offset: 0x0010D000
	private void OnDrag(Vector2 delta)
	{
		if (base.enabled && base.gameObject.activeInHierarchy && this.target != null)
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
					vector = this.target.InverseTransformDirection(vector);
					vector.Scale(this.scale);
					vector = this.target.TransformDirection(vector);
				}
				this.mMomentum = Vector3.Lerp(this.mMomentum, this.mMomentum + vector * (0.01f * this.momentumAmount), 0.67f);
				if (this.restrictWithinPanel)
				{
					Vector3 localPosition = this.target.localPosition;
					this.target.position += vector;
					this.mBounds.center = this.mBounds.center + (this.target.localPosition - localPosition);
					if (this.dragEffect != UIDragObject.DragEffect.MomentumAndSpring && this.mPanel.clipping != UIDrawCall.Clipping.None && this.mPanel.ConstrainTargetToBounds(this.target, ref this.mBounds, true))
					{
						this.mMomentum = Vector3.zero;
						this.mScroll = 0f;
					}
				}
				else if (this.restrictToScreen)
				{
					this.target.position += vector;
					Vector2 vector2;
					if (this.sizeParent)
					{
						vector2 = this.sizeParent.transform.localScale;
					}
					else
					{
						vector2 = NGUIMath.CalculateRelativeWidgetBounds(this.target).size;
					}
					Rect rect = UIDragObject.screenBorder.Add(new Rect(0f, (float)(-(float)Screen.height), (float)Screen.width, (float)Screen.height));
					Vector3 localPosition2 = this.target.localPosition;
					bool flag = true;
					if (localPosition2.x + vector2.x > rect.xMax)
					{
						localPosition2.x = rect.xMax - vector2.x;
					}
					else if (localPosition2.x < rect.xMin)
					{
						localPosition2.x = rect.xMin;
					}
					else
					{
						flag = false;
					}
					bool flag2 = true;
					if (localPosition2.y > rect.yMax)
					{
						localPosition2.y = rect.yMax;
					}
					else if (localPosition2.y - vector2.y < rect.yMin)
					{
						localPosition2.y = rect.yMin + vector2.y;
					}
					else
					{
						flag2 = false;
					}
					if (flag || flag2)
					{
						this.target.localPosition = localPosition2;
					}
				}
			}
		}
	}

	// Token: 0x0600450F RID: 17679 RVA: 0x0010F140 File Offset: 0x0010D340
	private void LateUpdate()
	{
		float deltaTime = base.UpdateRealTimeDelta();
		if (this.target == null)
		{
			return;
		}
		if (this.mPressed)
		{
			SpringPosition component = this.target.GetComponent<SpringPosition>();
			if (component != null)
			{
				component.enabled = false;
			}
			this.mScroll = 0f;
		}
		else
		{
			this.mMomentum += this.scale * (-this.mScroll * 0.05f);
			this.mScroll = NGUIMath.SpringLerp(this.mScroll, 0f, 20f, deltaTime);
			if (this.mMomentum.magnitude > 0.0001f)
			{
				if (this.mPanel == null)
				{
					this.FindPanel();
				}
				if (this.mPanel != null)
				{
					this.target.position += NGUIMath.SpringDampen(ref this.mMomentum, 9f, deltaTime);
					if (this.restrictWithinPanel && this.mPanel.clipping != UIDrawCall.Clipping.None)
					{
						this.mBounds = NGUIMath.CalculateRelativeWidgetBounds(this.mPanel.cachedTransform, this.target);
						if (!this.mPanel.ConstrainTargetToBounds(this.target, ref this.mBounds, this.dragEffect == UIDragObject.DragEffect.None))
						{
							SpringPosition component2 = this.target.GetComponent<SpringPosition>();
							if (component2 != null)
							{
								component2.enabled = false;
							}
						}
					}
					return;
				}
			}
			else
			{
				this.mScroll = 0f;
			}
		}
		NGUIMath.SpringDampen(ref this.mMomentum, 9f, deltaTime);
	}

	// Token: 0x06004510 RID: 17680 RVA: 0x0010F2E8 File Offset: 0x0010D4E8
	private void OnScroll(float delta)
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

	// Token: 0x04002550 RID: 9552
	public Transform target;

	// Token: 0x04002551 RID: 9553
	public Transform sizeParent;

	// Token: 0x04002552 RID: 9554
	public Vector3 scale = Vector3.one;

	// Token: 0x04002553 RID: 9555
	public float scrollWheelFactor;

	// Token: 0x04002554 RID: 9556
	public bool restrictWithinPanel;

	// Token: 0x04002555 RID: 9557
	public bool restrictToScreen;

	// Token: 0x04002556 RID: 9558
	public UIDragObject.DragEffect dragEffect = UIDragObject.DragEffect.MomentumAndSpring;

	// Token: 0x04002557 RID: 9559
	public float momentumAmount = 35f;

	// Token: 0x04002558 RID: 9560
	private Plane mPlane;

	// Token: 0x04002559 RID: 9561
	private Vector3 mLastPos;

	// Token: 0x0400255A RID: 9562
	private UIPanel mPanel;

	// Token: 0x0400255B RID: 9563
	private bool mPressed;

	// Token: 0x0400255C RID: 9564
	private Vector3 mMomentum = Vector3.zero;

	// Token: 0x0400255D RID: 9565
	private float mScroll;

	// Token: 0x0400255E RID: 9566
	private AABBox mBounds;

	// Token: 0x0200076C RID: 1900
	public enum DragEffect
	{
		// Token: 0x04002560 RID: 9568
		None,
		// Token: 0x04002561 RID: 9569
		Momentum,
		// Token: 0x04002562 RID: 9570
		MomentumAndSpring
	}
}
