using System;
using UnityEngine;

// Token: 0x02000835 RID: 2101
public class UIHotSpot : MonoBehaviour
{
	// Token: 0x060048BF RID: 18623 RVA: 0x001144DC File Offset: 0x001126DC
	protected UIHotSpot(global::UIHotSpot.Kind kind, bool configuredInLocalSpace)
	{
		this.kind = kind;
		this.configuredInLocalSpace = configuredInLocalSpace;
	}

	// Token: 0x17000DDF RID: 3551
	// (get) Token: 0x060048C1 RID: 18625 RVA: 0x00114514 File Offset: 0x00112714
	protected static Vector3 forward
	{
		get
		{
			Vector3 result;
			result.x = 0f;
			result.y = 0f;
			result.z = -1f;
			return result;
		}
	}

	// Token: 0x17000DE0 RID: 3552
	// (get) Token: 0x060048C2 RID: 18626 RVA: 0x00114548 File Offset: 0x00112748
	protected static Vector3 backward
	{
		get
		{
			Vector3 result;
			result.x = 0f;
			result.y = 0f;
			result.z = 1f;
			return result;
		}
	}

	// Token: 0x060048C3 RID: 18627 RVA: 0x0011457C File Offset: 0x0011277C
	public static void ConvertRaycastHit(ref Ray ray, ref RaycastHit raycastHit, out global::UIHotSpot.Hit hit)
	{
		hit.collider = raycastHit.collider;
		hit.hotSpot = hit.collider.GetComponent<global::UIHotSpot>();
		hit.isCollider = !hit.hotSpot;
		if (hit.isCollider)
		{
			hit.panel = global::UIPanel.Find(hit.collider.transform);
		}
		else
		{
			hit.panel = ((!hit.hotSpot.panel) ? global::UIPanel.Find(hit.collider.transform) : hit.hotSpot.panel);
		}
		hit.ray = ray;
		hit.distance = raycastHit.distance;
		hit.point = raycastHit.point;
		hit.normal = raycastHit.normal;
	}

	// Token: 0x17000DE1 RID: 3553
	// (get) Token: 0x060048C4 RID: 18628 RVA: 0x0011464C File Offset: 0x0011284C
	public global::UIPanel uipanel
	{
		get
		{
			return this.panel;
		}
	}

	// Token: 0x17000DE2 RID: 3554
	// (get) Token: 0x060048C5 RID: 18629 RVA: 0x00114654 File Offset: 0x00112854
	private global::UICircleHotSpot circleUS
	{
		get
		{
			return (global::UICircleHotSpot)this;
		}
	}

	// Token: 0x17000DE3 RID: 3555
	// (get) Token: 0x060048C6 RID: 18630 RVA: 0x0011465C File Offset: 0x0011285C
	private global::UIRectHotSpot rectUS
	{
		get
		{
			return (global::UIRectHotSpot)this;
		}
	}

	// Token: 0x17000DE4 RID: 3556
	// (get) Token: 0x060048C7 RID: 18631 RVA: 0x00114664 File Offset: 0x00112864
	private global::UIConvexHotSpot convexUS
	{
		get
		{
			return (global::UIConvexHotSpot)this;
		}
	}

	// Token: 0x17000DE5 RID: 3557
	// (get) Token: 0x060048C8 RID: 18632 RVA: 0x0011466C File Offset: 0x0011286C
	private global::UISphereHotSpot sphereUS
	{
		get
		{
			return (global::UISphereHotSpot)this;
		}
	}

	// Token: 0x17000DE6 RID: 3558
	// (get) Token: 0x060048C9 RID: 18633 RVA: 0x00114674 File Offset: 0x00112874
	private global::UIBoxHotSpot boxUS
	{
		get
		{
			return (global::UIBoxHotSpot)this;
		}
	}

	// Token: 0x17000DE7 RID: 3559
	// (get) Token: 0x060048CA RID: 18634 RVA: 0x0011467C File Offset: 0x0011287C
	private global::UIBrushHotSpot brushUS
	{
		get
		{
			return (global::UIBrushHotSpot)this;
		}
	}

	// Token: 0x17000DE8 RID: 3560
	// (get) Token: 0x060048CB RID: 18635 RVA: 0x00114684 File Offset: 0x00112884
	public global::UICircleHotSpot asCircle
	{
		get
		{
			return (this.kind != global::UIHotSpot.Kind.Circle) ? null : ((global::UICircleHotSpot)this);
		}
	}

	// Token: 0x17000DE9 RID: 3561
	// (get) Token: 0x060048CC RID: 18636 RVA: 0x001146A0 File Offset: 0x001128A0
	public global::UIRectHotSpot asRect
	{
		get
		{
			return (this.kind != global::UIHotSpot.Kind.Rect) ? null : ((global::UIRectHotSpot)this);
		}
	}

	// Token: 0x17000DEA RID: 3562
	// (get) Token: 0x060048CD RID: 18637 RVA: 0x001146BC File Offset: 0x001128BC
	public global::UIConvexHotSpot asConvex
	{
		get
		{
			return (this.kind != global::UIHotSpot.Kind.Convex) ? null : ((global::UIConvexHotSpot)this);
		}
	}

	// Token: 0x17000DEB RID: 3563
	// (get) Token: 0x060048CE RID: 18638 RVA: 0x001146D8 File Offset: 0x001128D8
	public global::UISphereHotSpot asSphere
	{
		get
		{
			return (this.kind != global::UIHotSpot.Kind.Sphere) ? null : ((global::UISphereHotSpot)this);
		}
	}

	// Token: 0x17000DEC RID: 3564
	// (get) Token: 0x060048CF RID: 18639 RVA: 0x001146F8 File Offset: 0x001128F8
	public global::UIBoxHotSpot asBox
	{
		get
		{
			return (this.kind != global::UIHotSpot.Kind.Box) ? null : ((global::UIBoxHotSpot)this);
		}
	}

	// Token: 0x17000DED RID: 3565
	// (get) Token: 0x060048D0 RID: 18640 RVA: 0x00114718 File Offset: 0x00112918
	public global::UIBrushHotSpot asBrush
	{
		get
		{
			return (this.kind != global::UIHotSpot.Kind.Brush) ? null : ((global::UIBrushHotSpot)this);
		}
	}

	// Token: 0x17000DEE RID: 3566
	// (get) Token: 0x060048D1 RID: 18641 RVA: 0x00114738 File Offset: 0x00112938
	public bool isCircle
	{
		get
		{
			return this.kind == global::UIHotSpot.Kind.Circle;
		}
	}

	// Token: 0x17000DEF RID: 3567
	// (get) Token: 0x060048D2 RID: 18642 RVA: 0x00114744 File Offset: 0x00112944
	public bool isRect
	{
		get
		{
			return this.kind == global::UIHotSpot.Kind.Rect;
		}
	}

	// Token: 0x17000DF0 RID: 3568
	// (get) Token: 0x060048D3 RID: 18643 RVA: 0x00114750 File Offset: 0x00112950
	public bool isConvex
	{
		get
		{
			return this.kind == global::UIHotSpot.Kind.Convex;
		}
	}

	// Token: 0x17000DF1 RID: 3569
	// (get) Token: 0x060048D4 RID: 18644 RVA: 0x0011475C File Offset: 0x0011295C
	public bool isSphere
	{
		get
		{
			return this.kind == global::UIHotSpot.Kind.Sphere;
		}
	}

	// Token: 0x17000DF2 RID: 3570
	// (get) Token: 0x060048D5 RID: 18645 RVA: 0x0011476C File Offset: 0x0011296C
	public bool isBox
	{
		get
		{
			return this.kind == global::UIHotSpot.Kind.Box;
		}
	}

	// Token: 0x17000DF3 RID: 3571
	// (get) Token: 0x060048D6 RID: 18646 RVA: 0x0011477C File Offset: 0x0011297C
	public bool isBrush
	{
		get
		{
			return this.kind == global::UIHotSpot.Kind.Brush;
		}
	}

	// Token: 0x060048D7 RID: 18647 RVA: 0x0011478C File Offset: 0x0011298C
	public bool As(out global::UICircleHotSpot cast)
	{
		if (this.kind == global::UIHotSpot.Kind.Circle)
		{
			cast = (global::UICircleHotSpot)this;
			return true;
		}
		cast = null;
		return false;
	}

	// Token: 0x060048D8 RID: 18648 RVA: 0x001147A8 File Offset: 0x001129A8
	public bool As(out global::UIRectHotSpot cast)
	{
		if (this.kind == global::UIHotSpot.Kind.Rect)
		{
			cast = (global::UIRectHotSpot)this;
			return true;
		}
		cast = null;
		return false;
	}

	// Token: 0x060048D9 RID: 18649 RVA: 0x001147C4 File Offset: 0x001129C4
	public bool As(out global::UIConvexHotSpot cast)
	{
		if (this.kind == global::UIHotSpot.Kind.Convex)
		{
			cast = (global::UIConvexHotSpot)this;
			return true;
		}
		cast = null;
		return false;
	}

	// Token: 0x060048DA RID: 18650 RVA: 0x001147E0 File Offset: 0x001129E0
	public bool As(out global::UISphereHotSpot cast)
	{
		if (this.kind == global::UIHotSpot.Kind.Sphere)
		{
			cast = (global::UISphereHotSpot)this;
			return true;
		}
		cast = null;
		return false;
	}

	// Token: 0x060048DB RID: 18651 RVA: 0x00114800 File Offset: 0x00112A00
	public bool As(out global::UIBoxHotSpot cast)
	{
		if (this.kind == global::UIHotSpot.Kind.Box)
		{
			cast = (global::UIBoxHotSpot)this;
			return true;
		}
		cast = null;
		return false;
	}

	// Token: 0x060048DC RID: 18652 RVA: 0x00114820 File Offset: 0x00112A20
	public bool As(out global::UIBrushHotSpot cast)
	{
		if (this.kind == global::UIHotSpot.Kind.Brush)
		{
			cast = (global::UIBrushHotSpot)this;
			return true;
		}
		cast = null;
		return false;
	}

	// Token: 0x060048DD RID: 18653 RVA: 0x00114840 File Offset: 0x00112A40
	private bool EnableHotSpot()
	{
		return global::UIHotSpot.Global.Add(this);
	}

	// Token: 0x060048DE RID: 18654 RVA: 0x00114848 File Offset: 0x00112A48
	private bool DisableHotSpot()
	{
		return global::UIHotSpot.Global.Remove(this);
	}

	// Token: 0x060048DF RID: 18655 RVA: 0x00114850 File Offset: 0x00112A50
	private void Start()
	{
		this.panel = global::UIPanel.Find(base.transform);
		if (this.panel)
		{
			global::UIPanel.RegisterHotSpot(this.panel, this);
		}
		else
		{
			Debug.LogWarning("Did not find panel!", this);
		}
	}

	// Token: 0x060048E0 RID: 18656 RVA: 0x00114890 File Offset: 0x00112A90
	private void OnDestroy()
	{
		if (this.panel)
		{
			global::UIPanel uipanel = this.panel;
			this.panel = null;
			global::UIPanel.UnregisterHotSpot(uipanel, this);
		}
	}

	// Token: 0x060048E1 RID: 18657 RVA: 0x001148C4 File Offset: 0x00112AC4
	protected void OnEnable()
	{
		if (this.panel && this.panel.enabled)
		{
			this.EnableHotSpot();
		}
	}

	// Token: 0x060048E2 RID: 18658 RVA: 0x001148F0 File Offset: 0x00112AF0
	protected void OnDisable()
	{
		if (this.once)
		{
			this.DisableHotSpot();
		}
	}

	// Token: 0x060048E3 RID: 18659 RVA: 0x00114904 File Offset: 0x00112B04
	internal void OnPanelEnable()
	{
		if (base.enabled)
		{
			this.EnableHotSpot();
		}
	}

	// Token: 0x060048E4 RID: 18660 RVA: 0x00114918 File Offset: 0x00112B18
	internal void OnPanelDisable()
	{
		if (base.enabled)
		{
			this.DisableHotSpot();
		}
	}

	// Token: 0x060048E5 RID: 18661 RVA: 0x0011492C File Offset: 0x00112B2C
	internal void OnPanelDestroy()
	{
		global::UIPanel uipanel = this.panel;
		this.panel = null;
		if (base.enabled && uipanel && uipanel.enabled)
		{
			this.OnPanelDisable();
		}
	}

	// Token: 0x060048E6 RID: 18662 RVA: 0x00114970 File Offset: 0x00112B70
	private void SetBounds(bool moved, Bounds bounds, bool worldEquals)
	{
		if (this.configuredInLocalSpace)
		{
			if (this._lastBoundsEntered == bounds && worldEquals)
			{
				return;
			}
			this._lastBoundsEntered = bounds;
			global::AABBox.Transform3x4(ref bounds, ref this.toWorld, out this._bounds);
		}
		else
		{
			this._lastBoundsEntered = bounds;
			this._bounds = bounds;
		}
	}

	// Token: 0x060048E7 RID: 18663 RVA: 0x001149D0 File Offset: 0x00112BD0
	protected virtual void HotSpotInit()
	{
	}

	// Token: 0x060048E8 RID: 18664 RVA: 0x001149D4 File Offset: 0x00112BD4
	public bool ClosestRaycast(Ray ray, ref global::UIHotSpot.Hit hit)
	{
		global::UIHotSpot.Hit hit2;
		if (this.Raycast(ray, out hit2) && hit2.distance < hit.distance)
		{
			hit = hit2;
			return true;
		}
		return false;
	}

	// Token: 0x060048E9 RID: 18665 RVA: 0x00114A0C File Offset: 0x00112C0C
	private bool LocalRaycastRef(Ray worldRay, ref global::UIHotSpot.Hit hit)
	{
		Matrix4x4 matrix4x = base.transform.worldToLocalMatrix;
		Ray ray;
		ray..ctor(matrix4x.MultiplyPoint(worldRay.origin), matrix4x.MultiplyVector(worldRay.direction));
		global::UIHotSpot.Hit invalid = global::UIHotSpot.Hit.invalid;
		if (this.DoRaycastRef(ray, ref invalid))
		{
			matrix4x = base.transform.localToWorldMatrix;
			hit.point = matrix4x.MultiplyPoint(invalid.point);
			hit.normal = matrix4x.MultiplyVector(invalid.normal);
			hit.ray = worldRay;
			hit.distance = Vector3.Dot(worldRay.direction, hit.point - worldRay.origin);
			hit.hotSpot = this;
			hit.panel = this.panel;
			return true;
		}
		return false;
	}

	// Token: 0x060048EA RID: 18666 RVA: 0x00114AD4 File Offset: 0x00112CD4
	private bool DoRaycastRef(Ray ray, ref global::UIHotSpot.Hit hit)
	{
		global::UIHotSpot.Kind kind = this.kind;
		switch (kind)
		{
		case global::UIHotSpot.Kind.Circle:
			return ((global::UICircleHotSpot)this).Internal_RaycastRef(ray, ref hit);
		case global::UIHotSpot.Kind.Rect:
			return ((global::UIRectHotSpot)this).Internal_RaycastRef(ray, ref hit);
		case global::UIHotSpot.Kind.Convex:
			return ((global::UIConvexHotSpot)this).Internal_RaycastRef(ray, ref hit);
		default:
			switch (kind)
			{
			case global::UIHotSpot.Kind.Sphere:
				return ((global::UISphereHotSpot)this).Internal_RaycastRef(ray, ref hit);
			case global::UIHotSpot.Kind.Box:
				return ((global::UIBoxHotSpot)this).Internal_RaycastRef(ray, ref hit);
			case global::UIHotSpot.Kind.Brush:
				return ((global::UIBrushHotSpot)this).Internal_RaycastRef(ray, ref hit);
			default:
				throw new NotImplementedException();
			}
			break;
		}
	}

	// Token: 0x060048EB RID: 18667 RVA: 0x00114B70 File Offset: 0x00112D70
	public bool Raycast(Ray ray, out global::UIHotSpot.Hit hit)
	{
		hit = global::UIHotSpot.Hit.invalid;
		return this.RaycastRef(ray, ref hit);
	}

	// Token: 0x060048EC RID: 18668 RVA: 0x00114B88 File Offset: 0x00112D88
	public bool RaycastRef(Ray ray, ref global::UIHotSpot.Hit hit)
	{
		if (this.configuredInLocalSpace)
		{
			return this.LocalRaycastRef(ray, ref hit);
		}
		return this.DoRaycastRef(ray, ref hit);
	}

	// Token: 0x060048ED RID: 18669 RVA: 0x00114BA8 File Offset: 0x00112DA8
	public static bool Raycast(Ray ray, out global::UIHotSpot.Hit hit, float distance)
	{
		return global::UIHotSpot.Global.Raycast(ray, out hit, distance);
	}

	// Token: 0x17000DF4 RID: 3572
	// (get) Token: 0x060048EE RID: 18670 RVA: 0x00114BB4 File Offset: 0x00112DB4
	protected Color gizmoColor
	{
		get
		{
			return Color.green;
		}
	}

	// Token: 0x17000DF5 RID: 3573
	// (get) Token: 0x060048EF RID: 18671 RVA: 0x00114BBC File Offset: 0x00112DBC
	protected Matrix4x4 gizmoMatrix
	{
		get
		{
			if (this.index == -1)
			{
				return (!this.configuredInLocalSpace) ? Matrix4x4.identity : base.transform.localToWorldMatrix;
			}
			return (!this.configuredInLocalSpace) ? Matrix4x4.identity : this.toWorld;
		}
	}

	// Token: 0x17000DF6 RID: 3574
	// (get) Token: 0x060048F0 RID: 18672 RVA: 0x00114C14 File Offset: 0x00112E14
	public Vector3 worldCenter
	{
		get
		{
			global::UIHotSpot.Kind kind = this.kind;
			Vector3 center;
			if (kind != global::UIHotSpot.Kind.Circle)
			{
				if (kind != global::UIHotSpot.Kind.Rect)
				{
					if (kind != global::UIHotSpot.Kind.Sphere)
					{
						if (kind != global::UIHotSpot.Kind.Box)
						{
							throw new NotImplementedException();
						}
						center = ((global::UIBoxHotSpot)this).center;
					}
					else
					{
						center = ((global::UISphereHotSpot)this).center;
					}
				}
				else
				{
					center = ((global::UIRectHotSpot)this).center;
				}
			}
			else
			{
				center = ((global::UICircleHotSpot)this).center;
			}
			return base.transform.TransformPoint(center);
		}
	}

	// Token: 0x17000DF7 RID: 3575
	// (get) Token: 0x060048F1 RID: 18673 RVA: 0x00114CA8 File Offset: 0x00112EA8
	// (set) Token: 0x060048F2 RID: 18674 RVA: 0x00114D20 File Offset: 0x00112F20
	public Vector3 center
	{
		get
		{
			global::UIHotSpot.Kind kind = this.kind;
			if (kind == global::UIHotSpot.Kind.Circle)
			{
				return ((global::UICircleHotSpot)this).center;
			}
			if (kind == global::UIHotSpot.Kind.Rect)
			{
				return ((global::UIRectHotSpot)this).center;
			}
			if (kind == global::UIHotSpot.Kind.Sphere)
			{
				return ((global::UISphereHotSpot)this).center;
			}
			if (kind != global::UIHotSpot.Kind.Box)
			{
				return default(Vector3);
			}
			return ((global::UIBoxHotSpot)this).center;
		}
		set
		{
			global::UIHotSpot.Kind kind = this.kind;
			if (kind != global::UIHotSpot.Kind.Circle)
			{
				if (kind != global::UIHotSpot.Kind.Rect)
				{
					if (kind != global::UIHotSpot.Kind.Sphere)
					{
						if (kind == global::UIHotSpot.Kind.Box)
						{
							((global::UIBoxHotSpot)this).center = value;
						}
					}
					else
					{
						((global::UISphereHotSpot)this).center = value;
					}
				}
				else
				{
					((global::UIRectHotSpot)this).center = value;
				}
			}
			else
			{
				((global::UICircleHotSpot)this).center = value;
			}
		}
	}

	// Token: 0x17000DF8 RID: 3576
	// (get) Token: 0x060048F3 RID: 18675 RVA: 0x00114DA8 File Offset: 0x00112FA8
	// (set) Token: 0x060048F4 RID: 18676 RVA: 0x00114E70 File Offset: 0x00113070
	public Vector3 size
	{
		get
		{
			global::UIHotSpot.Kind kind = this.kind;
			Vector3 result;
			if (kind == global::UIHotSpot.Kind.Circle)
			{
				result.x = ((global::UICircleHotSpot)this).radius * 2f;
				result.y = result.x;
				result.z = 0f;
				return result;
			}
			if (kind == global::UIHotSpot.Kind.Rect)
			{
				return ((global::UIRectHotSpot)this).size;
			}
			if (kind == global::UIHotSpot.Kind.Sphere)
			{
				result.x = ((global::UICircleHotSpot)this).radius * 1.41421354f;
				result.y = (result.z = result.x);
				return result;
			}
			if (kind != global::UIHotSpot.Kind.Box)
			{
				return default(Vector3);
			}
			return ((global::UIBoxHotSpot)this).size;
		}
		set
		{
			global::UIHotSpot.Kind kind = this.kind;
			if (kind != global::UIHotSpot.Kind.Circle)
			{
				if (kind != global::UIHotSpot.Kind.Rect)
				{
					if (kind != global::UIHotSpot.Kind.Sphere)
					{
						if (kind == global::UIHotSpot.Kind.Box)
						{
							((global::UIBoxHotSpot)this).size = value;
						}
					}
					else
					{
						value.z *= 0.707106769f;
						value.y *= 0.707106769f;
						value.x *= 0.707106769f;
						((global::UISphereHotSpot)this).radius = Mathf.Sqrt(value.x * value.x + value.y * value.y + value.z * value.z) / 2f;
					}
				}
				else
				{
					((global::UIRectHotSpot)this).size = new Vector2(value.x, value.y);
				}
			}
			else
			{
				value.y *= 0.707106769f;
				value.x *= 0.707106769f;
				((global::UICircleHotSpot)this).radius = Mathf.Sqrt(value.x * value.x + value.y * value.y) / 2f;
			}
		}
	}

	// Token: 0x040026E1 RID: 9953
	private const global::UIHotSpot.Kind kKindFlag_2D = global::UIHotSpot.Kind.Circle;

	// Token: 0x040026E2 RID: 9954
	private const global::UIHotSpot.Kind kKindFlag_3D = global::UIHotSpot.Kind.Sphere;

	// Token: 0x040026E3 RID: 9955
	private const global::UIHotSpot.Kind kKindFlag_Radial = global::UIHotSpot.Kind.Circle;

	// Token: 0x040026E4 RID: 9956
	private const global::UIHotSpot.Kind kKindFlag_Axial = global::UIHotSpot.Kind.Rect;

	// Token: 0x040026E5 RID: 9957
	private const global::UIHotSpot.Kind kKindFlag_Convex = global::UIHotSpot.Kind.Convex;

	// Token: 0x040026E6 RID: 9958
	private const float kCos45 = 0.707106769f;

	// Token: 0x040026E7 RID: 9959
	private const float k2Cos45 = 1.41421354f;

	// Token: 0x040026E8 RID: 9960
	public readonly global::UIHotSpot.Kind kind;

	// Token: 0x040026E9 RID: 9961
	private global::UIPanel panel;

	// Token: 0x040026EA RID: 9962
	private Matrix4x4 toWorld;

	// Token: 0x040026EB RID: 9963
	private Matrix4x4 toLocal;

	// Token: 0x040026EC RID: 9964
	private Matrix4x4 lastWorld;

	// Token: 0x040026ED RID: 9965
	private Matrix4x4 lastLocal;

	// Token: 0x040026EE RID: 9966
	private Bounds _bounds;

	// Token: 0x040026EF RID: 9967
	private Bounds _lastBoundsEntered;

	// Token: 0x040026F0 RID: 9968
	private bool once;

	// Token: 0x040026F1 RID: 9969
	private bool justAdded;

	// Token: 0x040026F2 RID: 9970
	private int index = -1;

	// Token: 0x040026F3 RID: 9971
	private readonly bool configuredInLocalSpace;

	// Token: 0x040026F4 RID: 9972
	protected static readonly Plane localPlane = new Plane(Vector3.back, Vector3.zero);

	// Token: 0x02000836 RID: 2102
	public enum Kind
	{
		// Token: 0x040026F6 RID: 9974
		Circle,
		// Token: 0x040026F7 RID: 9975
		Rect,
		// Token: 0x040026F8 RID: 9976
		Convex,
		// Token: 0x040026F9 RID: 9977
		Sphere = 128,
		// Token: 0x040026FA RID: 9978
		Box,
		// Token: 0x040026FB RID: 9979
		Brush
	}

	// Token: 0x02000837 RID: 2103
	public struct Hit
	{
		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x060048F6 RID: 18678 RVA: 0x0011501C File Offset: 0x0011321C
		public GameObject gameObject
		{
			get
			{
				return (!this.isCollider) ? ((!this.hotSpot) ? null : this.hotSpot.gameObject) : this.collider.gameObject;
			}
		}

		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x060048F7 RID: 18679 RVA: 0x00115068 File Offset: 0x00113268
		public Transform transform
		{
			get
			{
				return (!this.isCollider) ? ((!this.hotSpot) ? null : this.hotSpot.transform) : this.collider.transform;
			}
		}

		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x060048F8 RID: 18680 RVA: 0x001150B4 File Offset: 0x001132B4
		public Component component
		{
			get
			{
				return (!this.isCollider) ? this.hotSpot : this.collider;
			}
		}

		// Token: 0x040026FC RID: 9980
		public global::UIHotSpot hotSpot;

		// Token: 0x040026FD RID: 9981
		public Collider collider;

		// Token: 0x040026FE RID: 9982
		public global::UIPanel panel;

		// Token: 0x040026FF RID: 9983
		public Vector3 point;

		// Token: 0x04002700 RID: 9984
		public Vector3 normal;

		// Token: 0x04002701 RID: 9985
		public Ray ray;

		// Token: 0x04002702 RID: 9986
		public float distance;

		// Token: 0x04002703 RID: 9987
		public bool isCollider;

		// Token: 0x04002704 RID: 9988
		public static readonly global::UIHotSpot.Hit invalid = new global::UIHotSpot.Hit
		{
			distance = float.PositiveInfinity,
			ray = default(Ray),
			point = default(Vector3),
			normal = default(Vector3)
		};
	}

	// Token: 0x02000838 RID: 2104
	private static class Global
	{
		// Token: 0x060048FA RID: 18682 RVA: 0x001150E0 File Offset: 0x001132E0
		public static bool Add(global::UIHotSpot hotSpot)
		{
			if (hotSpot.index != -1)
			{
				return false;
			}
			global::UIHotSpot.Kind kind = hotSpot.kind;
			switch (kind)
			{
			case global::UIHotSpot.Kind.Circle:
				global::UIHotSpot.Global.Circle.Add((global::UICircleHotSpot)hotSpot);
				break;
			case global::UIHotSpot.Kind.Rect:
				global::UIHotSpot.Global.Rect.Add((global::UIRectHotSpot)hotSpot);
				break;
			case global::UIHotSpot.Kind.Convex:
				global::UIHotSpot.Global.Convex.Add((global::UIConvexHotSpot)hotSpot);
				break;
			default:
				switch (kind)
				{
				case global::UIHotSpot.Kind.Sphere:
					global::UIHotSpot.Global.Sphere.Add((global::UISphereHotSpot)hotSpot);
					break;
				case global::UIHotSpot.Kind.Box:
					global::UIHotSpot.Global.Box.Add((global::UIBoxHotSpot)hotSpot);
					break;
				case global::UIHotSpot.Kind.Brush:
					global::UIHotSpot.Global.Brush.Add((global::UIBrushHotSpot)hotSpot);
					break;
				default:
					throw new NotImplementedException();
				}
				break;
			}
			hotSpot.justAdded = true;
			if (!hotSpot.once)
			{
				hotSpot.HotSpotInit();
				hotSpot.once = true;
			}
			return true;
		}

		// Token: 0x060048FB RID: 18683 RVA: 0x001151D8 File Offset: 0x001133D8
		public static bool Remove(global::UIHotSpot hotSpot)
		{
			if (hotSpot.index == -1)
			{
				return false;
			}
			global::UIHotSpot.Kind kind = hotSpot.kind;
			switch (kind)
			{
			case global::UIHotSpot.Kind.Circle:
				global::UIHotSpot.Global.Circle.Erase((global::UICircleHotSpot)hotSpot);
				break;
			case global::UIHotSpot.Kind.Rect:
				global::UIHotSpot.Global.Rect.Erase((global::UIRectHotSpot)hotSpot);
				break;
			case global::UIHotSpot.Kind.Convex:
				global::UIHotSpot.Global.Convex.Erase((global::UIConvexHotSpot)hotSpot);
				break;
			default:
				switch (kind)
				{
				case global::UIHotSpot.Kind.Sphere:
					global::UIHotSpot.Global.Sphere.Erase((global::UISphereHotSpot)hotSpot);
					break;
				case global::UIHotSpot.Kind.Box:
					global::UIHotSpot.Global.Box.Erase((global::UIBoxHotSpot)hotSpot);
					break;
				case global::UIHotSpot.Kind.Brush:
					global::UIHotSpot.Global.Brush.Erase((global::UIBrushHotSpot)hotSpot);
					break;
				default:
					throw new NotImplementedException();
				}
				break;
			}
			return true;
		}

		// Token: 0x060048FC RID: 18684 RVA: 0x001152B0 File Offset: 0x001134B0
		private static bool MatrixEquals(ref Matrix4x4 a, ref Matrix4x4 b)
		{
			return a.m03 == b.m03 && a.m12 == b.m13 && a.m23 == b.m23 && a.m00 == b.m00 && a.m11 == b.m11 && a.m22 == b.m22 && a.m01 == b.m01 && a.m12 == b.m12 && a.m20 == b.m20 && a.m02 == b.m02 && a.m10 == b.m10 && a.m21 == b.m21 && a.m30 == b.m30 && a.m31 == b.m31 && a.m32 == b.m32 && a.m33 == b.m33;
		}

		// Token: 0x060048FD RID: 18685 RVA: 0x001153D0 File Offset: 0x001135D0
		private static Bounds? DoStep()
		{
			Bounds value = default(Bounds);
			bool flag = true;
			int num = global::UIHotSpot.Global.allCount;
			if (global::UIHotSpot.Global.Circle.any)
			{
				for (int i = 0; i < global::UIHotSpot.Global.Circle.count; i++)
				{
					global::UICircleHotSpot uicircleHotSpot = global::UIHotSpot.Global.Circle.array[i];
					Transform transform = uicircleHotSpot.transform;
					uicircleHotSpot.lastWorld = uicircleHotSpot.toWorld;
					uicircleHotSpot.toWorld = transform.localToWorldMatrix;
					uicircleHotSpot.lastLocal = uicircleHotSpot.toLocal;
					uicircleHotSpot.toLocal = transform.worldToLocalMatrix;
					bool worldEquals;
					bool flag2 = !(worldEquals = !uicircleHotSpot.justAdded) || !(worldEquals = global::UIHotSpot.Global.MatrixEquals(ref uicircleHotSpot.toWorld, ref uicircleHotSpot.lastWorld));
					bool moved = uicircleHotSpot.justAdded || ((!uicircleHotSpot.configuredInLocalSpace) ? flag2 : (!global::UIHotSpot.Global.MatrixEquals(ref uicircleHotSpot.toLocal, ref uicircleHotSpot.lastLocal)));
					global::UIHotSpot uihotSpot = uicircleHotSpot;
					bool moved2 = flag2;
					Bounds? bounds = uicircleHotSpot.Internal_CalculateBounds(moved);
					uihotSpot.SetBounds(moved2, (bounds == null) ? uicircleHotSpot._bounds : bounds.Value, worldEquals);
					uicircleHotSpot.justAdded = false;
					if (uicircleHotSpot._bounds.size != Vector3.zero)
					{
						if (flag)
						{
							if (--num == 0)
							{
								return new Bounds?(uicircleHotSpot._bounds);
							}
							flag = false;
							value = uicircleHotSpot._bounds;
						}
						else
						{
							value.Encapsulate(uicircleHotSpot._bounds);
							if (--num == 0)
							{
								return new Bounds?(value);
							}
						}
					}
					else if (--num == 0)
					{
						return null;
					}
				}
			}
			if (global::UIHotSpot.Global.Rect.any)
			{
				for (int j = 0; j < global::UIHotSpot.Global.Rect.count; j++)
				{
					global::UIRectHotSpot uirectHotSpot = global::UIHotSpot.Global.Rect.array[j];
					Transform transform = uirectHotSpot.transform;
					uirectHotSpot.lastWorld = uirectHotSpot.toWorld;
					uirectHotSpot.toWorld = transform.localToWorldMatrix;
					uirectHotSpot.lastLocal = uirectHotSpot.toLocal;
					uirectHotSpot.toLocal = transform.worldToLocalMatrix;
					bool worldEquals;
					bool flag2 = !(worldEquals = !uirectHotSpot.justAdded) || !(worldEquals = global::UIHotSpot.Global.MatrixEquals(ref uirectHotSpot.toWorld, ref uirectHotSpot.lastWorld));
					bool moved = uirectHotSpot.justAdded || ((!uirectHotSpot.configuredInLocalSpace) ? flag2 : (!global::UIHotSpot.Global.MatrixEquals(ref uirectHotSpot.toLocal, ref uirectHotSpot.lastLocal)));
					global::UIHotSpot uihotSpot2 = uirectHotSpot;
					bool moved3 = flag2;
					Bounds? bounds2 = uirectHotSpot.Internal_CalculateBounds(moved);
					uihotSpot2.SetBounds(moved3, (bounds2 == null) ? uirectHotSpot._bounds : bounds2.Value, worldEquals);
					uirectHotSpot.justAdded = false;
					if (uirectHotSpot._bounds.size != Vector3.zero)
					{
						if (flag)
						{
							if (--num == 0)
							{
								return new Bounds?(uirectHotSpot._bounds);
							}
							flag = false;
							value = uirectHotSpot._bounds;
						}
						else
						{
							value.Encapsulate(uirectHotSpot._bounds);
							if (--num == 0)
							{
								return new Bounds?(value);
							}
						}
					}
					else if (--num == 0)
					{
						return null;
					}
				}
			}
			if (global::UIHotSpot.Global.Convex.any)
			{
				for (int k = 0; k < global::UIHotSpot.Global.Convex.count; k++)
				{
					global::UIConvexHotSpot uiconvexHotSpot = global::UIHotSpot.Global.Convex.array[k];
					Transform transform = uiconvexHotSpot.transform;
					uiconvexHotSpot.lastWorld = uiconvexHotSpot.toWorld;
					uiconvexHotSpot.toWorld = transform.localToWorldMatrix;
					uiconvexHotSpot.lastLocal = uiconvexHotSpot.toLocal;
					uiconvexHotSpot.toLocal = transform.worldToLocalMatrix;
					bool worldEquals;
					bool flag2 = !(worldEquals = !uiconvexHotSpot.justAdded) || !(worldEquals = global::UIHotSpot.Global.MatrixEquals(ref uiconvexHotSpot.toWorld, ref uiconvexHotSpot.lastWorld));
					bool moved = uiconvexHotSpot.justAdded || ((!uiconvexHotSpot.configuredInLocalSpace) ? flag2 : (!global::UIHotSpot.Global.MatrixEquals(ref uiconvexHotSpot.toLocal, ref uiconvexHotSpot.lastLocal)));
					global::UIHotSpot uihotSpot3 = uiconvexHotSpot;
					bool moved4 = flag2;
					Bounds? bounds3 = uiconvexHotSpot.Internal_CalculateBounds(moved);
					uihotSpot3.SetBounds(moved4, (bounds3 == null) ? uiconvexHotSpot._bounds : bounds3.Value, worldEquals);
					uiconvexHotSpot.justAdded = false;
					if (uiconvexHotSpot._bounds.size != Vector3.zero)
					{
						if (flag)
						{
							if (--num == 0)
							{
								return new Bounds?(uiconvexHotSpot._bounds);
							}
							flag = false;
							value = uiconvexHotSpot._bounds;
						}
						else
						{
							value.Encapsulate(uiconvexHotSpot._bounds);
							if (--num == 0)
							{
								return new Bounds?(value);
							}
						}
					}
					else if (--num == 0)
					{
						return null;
					}
				}
			}
			if (global::UIHotSpot.Global.Sphere.any)
			{
				for (int l = 0; l < global::UIHotSpot.Global.Sphere.count; l++)
				{
					global::UISphereHotSpot uisphereHotSpot = global::UIHotSpot.Global.Sphere.array[l];
					Transform transform = uisphereHotSpot.transform;
					uisphereHotSpot.lastWorld = uisphereHotSpot.toWorld;
					uisphereHotSpot.toWorld = transform.localToWorldMatrix;
					uisphereHotSpot.lastLocal = uisphereHotSpot.toLocal;
					uisphereHotSpot.toLocal = transform.worldToLocalMatrix;
					bool worldEquals;
					bool flag2 = !(worldEquals = !uisphereHotSpot.justAdded) || !(worldEquals = global::UIHotSpot.Global.MatrixEquals(ref uisphereHotSpot.toWorld, ref uisphereHotSpot.lastWorld));
					bool moved = uisphereHotSpot.justAdded || ((!uisphereHotSpot.configuredInLocalSpace) ? flag2 : (!global::UIHotSpot.Global.MatrixEquals(ref uisphereHotSpot.toLocal, ref uisphereHotSpot.lastLocal)));
					global::UIHotSpot uihotSpot4 = uisphereHotSpot;
					bool moved5 = flag2;
					Bounds? bounds4 = uisphereHotSpot.Internal_CalculateBounds(moved);
					uihotSpot4.SetBounds(moved5, (bounds4 == null) ? uisphereHotSpot._bounds : bounds4.Value, worldEquals);
					uisphereHotSpot.justAdded = false;
					if (uisphereHotSpot._bounds.size != Vector3.zero)
					{
						if (flag)
						{
							if (--num == 0)
							{
								return new Bounds?(uisphereHotSpot._bounds);
							}
							flag = false;
							value = uisphereHotSpot._bounds;
						}
						else
						{
							value.Encapsulate(uisphereHotSpot._bounds);
							if (--num == 0)
							{
								return new Bounds?(value);
							}
						}
					}
					else if (--num == 0)
					{
						return null;
					}
				}
			}
			if (global::UIHotSpot.Global.Box.any)
			{
				for (int m = 0; m < global::UIHotSpot.Global.Box.count; m++)
				{
					global::UIBoxHotSpot uiboxHotSpot = global::UIHotSpot.Global.Box.array[m];
					Transform transform = uiboxHotSpot.transform;
					uiboxHotSpot.lastWorld = uiboxHotSpot.toWorld;
					uiboxHotSpot.toWorld = transform.localToWorldMatrix;
					uiboxHotSpot.lastLocal = uiboxHotSpot.toLocal;
					uiboxHotSpot.toLocal = transform.worldToLocalMatrix;
					bool worldEquals;
					bool flag2 = !(worldEquals = !uiboxHotSpot.justAdded) || !(worldEquals = global::UIHotSpot.Global.MatrixEquals(ref uiboxHotSpot.toWorld, ref uiboxHotSpot.lastWorld));
					bool moved = uiboxHotSpot.justAdded || ((!uiboxHotSpot.configuredInLocalSpace) ? flag2 : (!global::UIHotSpot.Global.MatrixEquals(ref uiboxHotSpot.toLocal, ref uiboxHotSpot.lastLocal)));
					global::UIHotSpot uihotSpot5 = uiboxHotSpot;
					bool moved6 = flag2;
					Bounds? bounds5 = uiboxHotSpot.Internal_CalculateBounds(moved);
					uihotSpot5.SetBounds(moved6, (bounds5 == null) ? uiboxHotSpot._bounds : bounds5.Value, worldEquals);
					uiboxHotSpot.justAdded = false;
					if (uiboxHotSpot._bounds.size != Vector3.zero)
					{
						if (flag)
						{
							if (--num == 0)
							{
								return new Bounds?(uiboxHotSpot._bounds);
							}
							flag = false;
							value = uiboxHotSpot._bounds;
						}
						else
						{
							value.Encapsulate(uiboxHotSpot._bounds);
							if (--num == 0)
							{
								return new Bounds?(value);
							}
						}
					}
					else if (--num == 0)
					{
						return null;
					}
				}
			}
			if (global::UIHotSpot.Global.Brush.any)
			{
				for (int n = 0; n < global::UIHotSpot.Global.Brush.count; n++)
				{
					global::UIBrushHotSpot uibrushHotSpot = global::UIHotSpot.Global.Brush.array[n];
					Transform transform = uibrushHotSpot.transform;
					uibrushHotSpot.lastWorld = uibrushHotSpot.toWorld;
					uibrushHotSpot.toWorld = transform.localToWorldMatrix;
					uibrushHotSpot.lastLocal = uibrushHotSpot.toLocal;
					uibrushHotSpot.toLocal = transform.worldToLocalMatrix;
					bool worldEquals;
					bool flag2 = !(worldEquals = !uibrushHotSpot.justAdded) || !(worldEquals = global::UIHotSpot.Global.MatrixEquals(ref uibrushHotSpot.toWorld, ref uibrushHotSpot.lastWorld));
					bool moved = uibrushHotSpot.justAdded || ((!uibrushHotSpot.configuredInLocalSpace) ? flag2 : (!global::UIHotSpot.Global.MatrixEquals(ref uibrushHotSpot.toLocal, ref uibrushHotSpot.lastLocal)));
					global::UIHotSpot uihotSpot6 = uibrushHotSpot;
					bool moved7 = flag2;
					Bounds? bounds6 = uibrushHotSpot.Internal_CalculateBounds(moved);
					uihotSpot6.SetBounds(moved7, (bounds6 == null) ? uibrushHotSpot._bounds : bounds6.Value, worldEquals);
					uibrushHotSpot.justAdded = false;
					if (uibrushHotSpot._bounds.size != Vector3.zero)
					{
						if (flag)
						{
							if (--num == 0)
							{
								return new Bounds?(uibrushHotSpot._bounds);
							}
							flag = false;
							value = uibrushHotSpot._bounds;
						}
						else
						{
							value.Encapsulate(uibrushHotSpot._bounds);
							if (--num == 0)
							{
								return new Bounds?(value);
							}
						}
					}
					else if (--num == 0)
					{
						return null;
					}
				}
			}
			throw new InvalidOperationException("Something is messed up. this line should never execute.");
		}

		// Token: 0x060048FE RID: 18686 RVA: 0x00115DDC File Offset: 0x00113FDC
		private static bool DoRaycast(Ray ray, out global::UIHotSpot.Hit hit, float dist)
		{
			hit = global::UIHotSpot.Hit.invalid;
			global::UIHotSpot.Hit invalid = global::UIHotSpot.Hit.invalid;
			bool flag = true;
			Vector3 origin = ray.origin;
			int num = global::UIHotSpot.Global.allCount;
			float num2;
			if (global::UIHotSpot.Global.Circle.any)
			{
				for (int i = 0; i < global::UIHotSpot.Global.Circle.count; i++)
				{
					global::UICircleHotSpot uicircleHotSpot = global::UIHotSpot.Global.Circle.array[i];
					if ((uicircleHotSpot._bounds.Contains(origin) || (uicircleHotSpot._bounds.IntersectRay(ray, ref num2) && num2 < dist)) && uicircleHotSpot.panel.InsideClippingRect(ray, global::UIHotSpot.Global.lastStepFrame) && uicircleHotSpot.RaycastRef(ray, ref invalid) && invalid.distance < dist)
					{
						if (flag)
						{
							flag = false;
						}
						dist = invalid.distance;
						hit = invalid;
						if (--num == 0)
						{
							return true;
						}
					}
					else if (--num == 0)
					{
						return !flag;
					}
				}
			}
			if (global::UIHotSpot.Global.Rect.any)
			{
				for (int j = 0; j < global::UIHotSpot.Global.Rect.count; j++)
				{
					global::UIRectHotSpot uirectHotSpot = global::UIHotSpot.Global.Rect.array[j];
					if ((uirectHotSpot._bounds.Contains(origin) || (uirectHotSpot._bounds.IntersectRay(ray, ref num2) && num2 < dist)) && uirectHotSpot.panel.InsideClippingRect(ray, global::UIHotSpot.Global.lastStepFrame) && uirectHotSpot.RaycastRef(ray, ref invalid) && invalid.distance < dist)
					{
						if (flag)
						{
							flag = false;
						}
						dist = invalid.distance;
						hit = invalid;
						if (--num == 0)
						{
							return true;
						}
					}
					else if (--num == 0)
					{
						return !flag;
					}
				}
			}
			if (global::UIHotSpot.Global.Convex.any)
			{
				for (int k = 0; k < global::UIHotSpot.Global.Convex.count; k++)
				{
					global::UIConvexHotSpot uiconvexHotSpot = global::UIHotSpot.Global.Convex.array[k];
					if ((uiconvexHotSpot._bounds.Contains(origin) || (uiconvexHotSpot._bounds.IntersectRay(ray, ref num2) && num2 < dist)) && uiconvexHotSpot.panel.InsideClippingRect(ray, global::UIHotSpot.Global.lastStepFrame) && uiconvexHotSpot.RaycastRef(ray, ref invalid) && invalid.distance < dist)
					{
						if (flag)
						{
							flag = false;
						}
						dist = invalid.distance;
						hit = invalid;
						if (--num == 0)
						{
							return true;
						}
					}
					else if (--num == 0)
					{
						return !flag;
					}
				}
			}
			if (global::UIHotSpot.Global.Sphere.any)
			{
				for (int l = 0; l < global::UIHotSpot.Global.Sphere.count; l++)
				{
					global::UISphereHotSpot uisphereHotSpot = global::UIHotSpot.Global.Sphere.array[l];
					if ((uisphereHotSpot._bounds.Contains(origin) || (uisphereHotSpot._bounds.IntersectRay(ray, ref num2) && num2 < dist)) && uisphereHotSpot.panel.InsideClippingRect(ray, global::UIHotSpot.Global.lastStepFrame) && uisphereHotSpot.RaycastRef(ray, ref invalid) && invalid.distance < dist)
					{
						if (flag)
						{
							flag = false;
						}
						dist = invalid.distance;
						hit = invalid;
						if (--num == 0)
						{
							return true;
						}
					}
					else if (--num == 0)
					{
						return !flag;
					}
				}
			}
			if (global::UIHotSpot.Global.Box.any)
			{
				for (int m = 0; m < global::UIHotSpot.Global.Box.count; m++)
				{
					global::UIBoxHotSpot uiboxHotSpot = global::UIHotSpot.Global.Box.array[m];
					if ((uiboxHotSpot._bounds.Contains(origin) || (uiboxHotSpot._bounds.IntersectRay(ray, ref num2) && num2 < dist)) && uiboxHotSpot.panel.InsideClippingRect(ray, global::UIHotSpot.Global.lastStepFrame) && uiboxHotSpot.RaycastRef(ray, ref invalid) && invalid.distance < dist)
					{
						if (flag)
						{
							flag = false;
						}
						dist = invalid.distance;
						hit = invalid;
						if (--num == 0)
						{
							return true;
						}
					}
					else if (--num == 0)
					{
						return !flag;
					}
				}
			}
			if (global::UIHotSpot.Global.Brush.any)
			{
				for (int n = 0; n < global::UIHotSpot.Global.Brush.count; n++)
				{
					global::UIBrushHotSpot uibrushHotSpot = global::UIHotSpot.Global.Brush.array[n];
					if ((uibrushHotSpot._bounds.Contains(origin) || (uibrushHotSpot._bounds.IntersectRay(ray, ref num2) && num2 < dist)) && uibrushHotSpot.panel.InsideClippingRect(ray, global::UIHotSpot.Global.lastStepFrame) && uibrushHotSpot.RaycastRef(ray, ref invalid) && invalid.distance < dist)
					{
						if (flag)
						{
							flag = false;
						}
						dist = invalid.distance;
						hit = invalid;
						if (--num == 0)
						{
							return true;
						}
					}
					else if (--num == 0)
					{
						return !flag;
					}
				}
			}
			throw new InvalidOperationException("Something is messed up. this line should never execute.");
		}

		// Token: 0x060048FF RID: 18687 RVA: 0x0011632C File Offset: 0x0011452C
		public static void Step()
		{
			if (global::UIHotSpot.Global.allAny)
			{
				Bounds? bounds = global::UIHotSpot.Global.DoStep();
				global::UIHotSpot.Global.validBounds = (bounds != null);
				if (global::UIHotSpot.Global.validBounds)
				{
					global::UIHotSpot.Global.allBounds = bounds.Value;
				}
			}
			else
			{
				global::UIHotSpot.Global.validBounds = false;
			}
		}

		// Token: 0x06004900 RID: 18688 RVA: 0x00116378 File Offset: 0x00114578
		public static bool Raycast(Ray ray, out global::UIHotSpot.Hit hit, float distance)
		{
			if (!global::UIHotSpot.Global.allAny)
			{
				hit = global::UIHotSpot.Hit.invalid;
				return false;
			}
			int frameCount = Time.frameCount;
			if (global::UIHotSpot.Global.lastStepFrame != frameCount || global::UIHotSpot.Global.anyRemovedRecently || global::UIHotSpot.Global.anyAddedRecently)
			{
				global::UIHotSpot.Global.Step();
				global::UIHotSpot.Global.anyRemovedRecently = (global::UIHotSpot.Global.anyAddedRecently = false);
			}
			global::UIHotSpot.Global.lastStepFrame = frameCount;
			if (!global::UIHotSpot.Global.validBounds)
			{
				hit = global::UIHotSpot.Hit.invalid;
				return false;
			}
			if (global::UIHotSpot.Global.allBounds.Contains(ray.origin))
			{
				float num = 0f;
			}
			else
			{
				float num;
				if (!global::UIHotSpot.Global.allBounds.IntersectRay(ray, ref num) || num > distance)
				{
					hit = global::UIHotSpot.Hit.invalid;
					return false;
				}
				if (num != 0f)
				{
					ray.origin = ray.GetPoint(num - 0.001f);
					num = 0f;
				}
			}
			return global::UIHotSpot.Global.DoRaycast(ray, out hit, distance);
		}

		// Token: 0x04002705 RID: 9989
		private static int allCount;

		// Token: 0x04002706 RID: 9990
		private static bool allAny;

		// Token: 0x04002707 RID: 9991
		private static Bounds allBounds;

		// Token: 0x04002708 RID: 9992
		private static bool validBounds;

		// Token: 0x04002709 RID: 9993
		private static bool anyAddedRecently;

		// Token: 0x0400270A RID: 9994
		private static bool anyRemovedRecently;

		// Token: 0x0400270B RID: 9995
		private static global::UIHotSpot.Global.List<global::UICircleHotSpot> Circle;

		// Token: 0x0400270C RID: 9996
		private static global::UIHotSpot.Global.List<global::UIRectHotSpot> Rect;

		// Token: 0x0400270D RID: 9997
		private static global::UIHotSpot.Global.List<global::UIConvexHotSpot> Convex;

		// Token: 0x0400270E RID: 9998
		private static global::UIHotSpot.Global.List<global::UISphereHotSpot> Sphere;

		// Token: 0x0400270F RID: 9999
		private static global::UIHotSpot.Global.List<global::UIBoxHotSpot> Box;

		// Token: 0x04002710 RID: 10000
		private static global::UIHotSpot.Global.List<global::UIBrushHotSpot> Brush;

		// Token: 0x04002711 RID: 10001
		private static int lastStepFrame = int.MinValue;

		// Token: 0x02000839 RID: 2105
		private struct List<THotSpot> where THotSpot : global::UIHotSpot
		{
			// Token: 0x06004901 RID: 18689 RVA: 0x00116464 File Offset: 0x00114664
			public void Add(THotSpot hotSpot)
			{
				hotSpot.index = this.count++;
				if (hotSpot.index == this.capacity)
				{
					this.capacity += 8;
					Array.Resize<THotSpot>(ref this.array, this.capacity);
				}
				this.array[hotSpot.index] = hotSpot;
				this.any = true;
				if (global::UIHotSpot.Global.allCount++ == 0)
				{
					global::UIHotSpot.Global.allAny = true;
				}
				global::UIHotSpot.Global.anyAddedRecently = true;
			}

			// Token: 0x06004902 RID: 18690 RVA: 0x00116500 File Offset: 0x00114700
			public void Erase(THotSpot hotSpot)
			{
				global::UIHotSpot.Global.allCount--;
				if (--this.count == hotSpot.index)
				{
					this.array[hotSpot.index] = (THotSpot)((object)null);
					this.any = (this.count > 0);
					if (!this.any)
					{
						global::UIHotSpot.Global.allAny = (global::UIHotSpot.Global.allCount > 0);
					}
				}
				else
				{
					(this.array[hotSpot.index] = this.array[this.count]).index = hotSpot.index;
					this.array[this.count] = (THotSpot)((object)null);
				}
				hotSpot.index = -1;
				global::UIHotSpot.Global.anyRemovedRecently = true;
			}

			// Token: 0x04002712 RID: 10002
			public THotSpot[] array;

			// Token: 0x04002713 RID: 10003
			public int count;

			// Token: 0x04002714 RID: 10004
			public int capacity;

			// Token: 0x04002715 RID: 10005
			public bool any;
		}
	}
}
