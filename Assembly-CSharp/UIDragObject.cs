using System;
using UnityEngine;

// Token: 0x0200084D RID: 2125
[AddComponentMenu("NGUI/Interaction/Drag Object")]
public class UIDragObject : global::IgnoreTimeScale
{
	// Token: 0x17000E00 RID: 3584
	// (get) Token: 0x0600496C RID: 18796 RVA: 0x00118548 File Offset: 0x00116748
	public static RectOffset screenBorder
	{
		get
		{
			return new RectOffset(0, -64, 0, 0);
		}
	}

	// Token: 0x0600496D RID: 18797 RVA: 0x00118554 File Offset: 0x00116754
	private void FindPanel()
	{
		this.mPanel = ((!(this.target != null)) ? null : global::UIPanel.Find(this.target.transform, false));
		if (this.mPanel == null)
		{
			this.restrictWithinPanel = false;
		}
	}

	// Token: 0x0600496E RID: 18798 RVA: 0x001185A8 File Offset: 0x001167A8
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
					this.mBounds = global::NGUIMath.CalculateRelativeWidgetBounds(this.mPanel.cachedTransform, this.target);
				}
				if (this.restrictToScreen)
				{
					global::UICamera uicamera = global::UICamera.FindCameraForLayer(base.gameObject.layer);
					Rect rect = global::UIDragObject.screenBorder.Add(uicamera.camera.pixelRect);
					this.mBounds = global::AABBox.CenterAndSize(rect.center, new Vector3(rect.width, rect.height, 0f));
				}
				this.mMomentum = Vector3.zero;
				this.mScroll = 0f;
				global::SpringPosition component = this.target.GetComponent<global::SpringPosition>();
				if (component != null)
				{
					component.enabled = false;
				}
				this.mLastPos = global::UICamera.lastHit.point;
				Transform transform = global::UICamera.currentCamera.transform;
				this.mPlane = new Plane(((!(this.mPanel != null)) ? transform.rotation : this.mPanel.cachedTransform.rotation) * Vector3.back, this.mLastPos);
			}
			else if (this.restrictWithinPanel && this.mPanel.clipping != global::UIDrawCall.Clipping.None && this.dragEffect == global::UIDragObject.DragEffect.MomentumAndSpring)
			{
				this.mPanel.ConstrainTargetToBounds(this.target, ref this.mBounds, false);
			}
		}
	}

	// Token: 0x0600496F RID: 18799 RVA: 0x00118780 File Offset: 0x00116980
	private void OnDrag(Vector2 delta)
	{
		if (base.enabled && base.gameObject.activeInHierarchy && this.target != null)
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
					if (this.dragEffect != global::UIDragObject.DragEffect.MomentumAndSpring && this.mPanel.clipping != global::UIDrawCall.Clipping.None && this.mPanel.ConstrainTargetToBounds(this.target, ref this.mBounds, true))
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
						vector2 = global::NGUIMath.CalculateRelativeWidgetBounds(this.target).size;
					}
					Rect rect = global::UIDragObject.screenBorder.Add(new Rect(0f, (float)(-(float)Screen.height), (float)Screen.width, (float)Screen.height));
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

	// Token: 0x06004970 RID: 18800 RVA: 0x00118AC0 File Offset: 0x00116CC0
	private void LateUpdate()
	{
		float deltaTime = base.UpdateRealTimeDelta();
		if (this.target == null)
		{
			return;
		}
		if (this.mPressed)
		{
			global::SpringPosition component = this.target.GetComponent<global::SpringPosition>();
			if (component != null)
			{
				component.enabled = false;
			}
			this.mScroll = 0f;
		}
		else
		{
			this.mMomentum += this.scale * (-this.mScroll * 0.05f);
			this.mScroll = global::NGUIMath.SpringLerp(this.mScroll, 0f, 20f, deltaTime);
			if (this.mMomentum.magnitude > 0.0001f)
			{
				if (this.mPanel == null)
				{
					this.FindPanel();
				}
				if (this.mPanel != null)
				{
					this.target.position += global::NGUIMath.SpringDampen(ref this.mMomentum, 9f, deltaTime);
					if (this.restrictWithinPanel && this.mPanel.clipping != global::UIDrawCall.Clipping.None)
					{
						this.mBounds = global::NGUIMath.CalculateRelativeWidgetBounds(this.mPanel.cachedTransform, this.target);
						if (!this.mPanel.ConstrainTargetToBounds(this.target, ref this.mBounds, this.dragEffect == global::UIDragObject.DragEffect.None))
						{
							global::SpringPosition component2 = this.target.GetComponent<global::SpringPosition>();
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
		global::NGUIMath.SpringDampen(ref this.mMomentum, 9f, deltaTime);
	}

	// Token: 0x06004971 RID: 18801 RVA: 0x00118C68 File Offset: 0x00116E68
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

	// Token: 0x04002787 RID: 10119
	public Transform target;

	// Token: 0x04002788 RID: 10120
	public Transform sizeParent;

	// Token: 0x04002789 RID: 10121
	public Vector3 scale = Vector3.one;

	// Token: 0x0400278A RID: 10122
	public float scrollWheelFactor;

	// Token: 0x0400278B RID: 10123
	public bool restrictWithinPanel;

	// Token: 0x0400278C RID: 10124
	public bool restrictToScreen;

	// Token: 0x0400278D RID: 10125
	public global::UIDragObject.DragEffect dragEffect = global::UIDragObject.DragEffect.MomentumAndSpring;

	// Token: 0x0400278E RID: 10126
	public float momentumAmount = 35f;

	// Token: 0x0400278F RID: 10127
	private Plane mPlane;

	// Token: 0x04002790 RID: 10128
	private Vector3 mLastPos;

	// Token: 0x04002791 RID: 10129
	private global::UIPanel mPanel;

	// Token: 0x04002792 RID: 10130
	private bool mPressed;

	// Token: 0x04002793 RID: 10131
	private Vector3 mMomentum = Vector3.zero;

	// Token: 0x04002794 RID: 10132
	private float mScroll;

	// Token: 0x04002795 RID: 10133
	private global::AABBox mBounds;

	// Token: 0x0200084E RID: 2126
	public enum DragEffect
	{
		// Token: 0x04002797 RID: 10135
		None,
		// Token: 0x04002798 RID: 10136
		Momentum,
		// Token: 0x04002799 RID: 10137
		MomentumAndSpring
	}
}
