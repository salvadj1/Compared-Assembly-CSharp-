using System;
using UnityEngine;

// Token: 0x02000753 RID: 1875
public class UIHotSpot : MonoBehaviour
{
	// Token: 0x0600445E RID: 17502 RVA: 0x0010AB5C File Offset: 0x00108D5C
	protected UIHotSpot(UIHotSpot.Kind kind, bool configuredInLocalSpace)
	{
		this.kind = kind;
		this.configuredInLocalSpace = configuredInLocalSpace;
	}

	// Token: 0x17000D4F RID: 3407
	// (get) Token: 0x06004460 RID: 17504 RVA: 0x0010AB94 File Offset: 0x00108D94
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

	// Token: 0x17000D50 RID: 3408
	// (get) Token: 0x06004461 RID: 17505 RVA: 0x0010ABC8 File Offset: 0x00108DC8
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

	// Token: 0x06004462 RID: 17506 RVA: 0x0010ABFC File Offset: 0x00108DFC
	public static void ConvertRaycastHit(ref Ray ray, ref RaycastHit raycastHit, out UIHotSpot.Hit hit)
	{
		hit.collider = raycastHit.collider;
		hit.hotSpot = hit.collider.GetComponent<UIHotSpot>();
		hit.isCollider = !hit.hotSpot;
		if (hit.isCollider)
		{
			hit.panel = UIPanel.Find(hit.collider.transform);
		}
		else
		{
			hit.panel = ((!hit.hotSpot.panel) ? UIPanel.Find(hit.collider.transform) : hit.hotSpot.panel);
		}
		hit.ray = ray;
		hit.distance = raycastHit.distance;
		hit.point = raycastHit.point;
		hit.normal = raycastHit.normal;
	}

	// Token: 0x17000D51 RID: 3409
	// (get) Token: 0x06004463 RID: 17507 RVA: 0x0010ACCC File Offset: 0x00108ECC
	public UIPanel uipanel
	{
		get
		{
			return this.panel;
		}
	}

	// Token: 0x17000D52 RID: 3410
	// (get) Token: 0x06004464 RID: 17508 RVA: 0x0010ACD4 File Offset: 0x00108ED4
	private UICircleHotSpot circleUS
	{
		get
		{
			return (UICircleHotSpot)this;
		}
	}

	// Token: 0x17000D53 RID: 3411
	// (get) Token: 0x06004465 RID: 17509 RVA: 0x0010ACDC File Offset: 0x00108EDC
	private UIRectHotSpot rectUS
	{
		get
		{
			return (UIRectHotSpot)this;
		}
	}

	// Token: 0x17000D54 RID: 3412
	// (get) Token: 0x06004466 RID: 17510 RVA: 0x0010ACE4 File Offset: 0x00108EE4
	private UIConvexHotSpot convexUS
	{
		get
		{
			return (UIConvexHotSpot)this;
		}
	}

	// Token: 0x17000D55 RID: 3413
	// (get) Token: 0x06004467 RID: 17511 RVA: 0x0010ACEC File Offset: 0x00108EEC
	private UISphereHotSpot sphereUS
	{
		get
		{
			return (UISphereHotSpot)this;
		}
	}

	// Token: 0x17000D56 RID: 3414
	// (get) Token: 0x06004468 RID: 17512 RVA: 0x0010ACF4 File Offset: 0x00108EF4
	private UIBoxHotSpot boxUS
	{
		get
		{
			return (UIBoxHotSpot)this;
		}
	}

	// Token: 0x17000D57 RID: 3415
	// (get) Token: 0x06004469 RID: 17513 RVA: 0x0010ACFC File Offset: 0x00108EFC
	private UIBrushHotSpot brushUS
	{
		get
		{
			return (UIBrushHotSpot)this;
		}
	}

	// Token: 0x17000D58 RID: 3416
	// (get) Token: 0x0600446A RID: 17514 RVA: 0x0010AD04 File Offset: 0x00108F04
	public UICircleHotSpot asCircle
	{
		get
		{
			return (this.kind != UIHotSpot.Kind.Circle) ? null : ((UICircleHotSpot)this);
		}
	}

	// Token: 0x17000D59 RID: 3417
	// (get) Token: 0x0600446B RID: 17515 RVA: 0x0010AD20 File Offset: 0x00108F20
	public UIRectHotSpot asRect
	{
		get
		{
			return (this.kind != UIHotSpot.Kind.Rect) ? null : ((UIRectHotSpot)this);
		}
	}

	// Token: 0x17000D5A RID: 3418
	// (get) Token: 0x0600446C RID: 17516 RVA: 0x0010AD3C File Offset: 0x00108F3C
	public UIConvexHotSpot asConvex
	{
		get
		{
			return (this.kind != UIHotSpot.Kind.Convex) ? null : ((UIConvexHotSpot)this);
		}
	}

	// Token: 0x17000D5B RID: 3419
	// (get) Token: 0x0600446D RID: 17517 RVA: 0x0010AD58 File Offset: 0x00108F58
	public UISphereHotSpot asSphere
	{
		get
		{
			return (this.kind != UIHotSpot.Kind.Sphere) ? null : ((UISphereHotSpot)this);
		}
	}

	// Token: 0x17000D5C RID: 3420
	// (get) Token: 0x0600446E RID: 17518 RVA: 0x0010AD78 File Offset: 0x00108F78
	public UIBoxHotSpot asBox
	{
		get
		{
			return (this.kind != UIHotSpot.Kind.Box) ? null : ((UIBoxHotSpot)this);
		}
	}

	// Token: 0x17000D5D RID: 3421
	// (get) Token: 0x0600446F RID: 17519 RVA: 0x0010AD98 File Offset: 0x00108F98
	public UIBrushHotSpot asBrush
	{
		get
		{
			return (this.kind != UIHotSpot.Kind.Brush) ? null : ((UIBrushHotSpot)this);
		}
	}

	// Token: 0x17000D5E RID: 3422
	// (get) Token: 0x06004470 RID: 17520 RVA: 0x0010ADB8 File Offset: 0x00108FB8
	public bool isCircle
	{
		get
		{
			return this.kind == UIHotSpot.Kind.Circle;
		}
	}

	// Token: 0x17000D5F RID: 3423
	// (get) Token: 0x06004471 RID: 17521 RVA: 0x0010ADC4 File Offset: 0x00108FC4
	public bool isRect
	{
		get
		{
			return this.kind == UIHotSpot.Kind.Rect;
		}
	}

	// Token: 0x17000D60 RID: 3424
	// (get) Token: 0x06004472 RID: 17522 RVA: 0x0010ADD0 File Offset: 0x00108FD0
	public bool isConvex
	{
		get
		{
			return this.kind == UIHotSpot.Kind.Convex;
		}
	}

	// Token: 0x17000D61 RID: 3425
	// (get) Token: 0x06004473 RID: 17523 RVA: 0x0010ADDC File Offset: 0x00108FDC
	public bool isSphere
	{
		get
		{
			return this.kind == UIHotSpot.Kind.Sphere;
		}
	}

	// Token: 0x17000D62 RID: 3426
	// (get) Token: 0x06004474 RID: 17524 RVA: 0x0010ADEC File Offset: 0x00108FEC
	public bool isBox
	{
		get
		{
			return this.kind == UIHotSpot.Kind.Box;
		}
	}

	// Token: 0x17000D63 RID: 3427
	// (get) Token: 0x06004475 RID: 17525 RVA: 0x0010ADFC File Offset: 0x00108FFC
	public bool isBrush
	{
		get
		{
			return this.kind == UIHotSpot.Kind.Brush;
		}
	}

	// Token: 0x06004476 RID: 17526 RVA: 0x0010AE0C File Offset: 0x0010900C
	public bool As(out UICircleHotSpot cast)
	{
		if (this.kind == UIHotSpot.Kind.Circle)
		{
			cast = (UICircleHotSpot)this;
			return true;
		}
		cast = null;
		return false;
	}

	// Token: 0x06004477 RID: 17527 RVA: 0x0010AE28 File Offset: 0x00109028
	public bool As(out UIRectHotSpot cast)
	{
		if (this.kind == UIHotSpot.Kind.Rect)
		{
			cast = (UIRectHotSpot)this;
			return true;
		}
		cast = null;
		return false;
	}

	// Token: 0x06004478 RID: 17528 RVA: 0x0010AE44 File Offset: 0x00109044
	public bool As(out UIConvexHotSpot cast)
	{
		if (this.kind == UIHotSpot.Kind.Convex)
		{
			cast = (UIConvexHotSpot)this;
			return true;
		}
		cast = null;
		return false;
	}

	// Token: 0x06004479 RID: 17529 RVA: 0x0010AE60 File Offset: 0x00109060
	public bool As(out UISphereHotSpot cast)
	{
		if (this.kind == UIHotSpot.Kind.Sphere)
		{
			cast = (UISphereHotSpot)this;
			return true;
		}
		cast = null;
		return false;
	}

	// Token: 0x0600447A RID: 17530 RVA: 0x0010AE80 File Offset: 0x00109080
	public bool As(out UIBoxHotSpot cast)
	{
		if (this.kind == UIHotSpot.Kind.Box)
		{
			cast = (UIBoxHotSpot)this;
			return true;
		}
		cast = null;
		return false;
	}

	// Token: 0x0600447B RID: 17531 RVA: 0x0010AEA0 File Offset: 0x001090A0
	public bool As(out UIBrushHotSpot cast)
	{
		if (this.kind == UIHotSpot.Kind.Brush)
		{
			cast = (UIBrushHotSpot)this;
			return true;
		}
		cast = null;
		return false;
	}

	// Token: 0x0600447C RID: 17532 RVA: 0x0010AEC0 File Offset: 0x001090C0
	private bool EnableHotSpot()
	{
		return UIHotSpot.Global.Add(this);
	}

	// Token: 0x0600447D RID: 17533 RVA: 0x0010AEC8 File Offset: 0x001090C8
	private bool DisableHotSpot()
	{
		return UIHotSpot.Global.Remove(this);
	}

	// Token: 0x0600447E RID: 17534 RVA: 0x0010AED0 File Offset: 0x001090D0
	private void Start()
	{
		this.panel = UIPanel.Find(base.transform);
		if (this.panel)
		{
			UIPanel.RegisterHotSpot(this.panel, this);
		}
		else
		{
			Debug.LogWarning("Did not find panel!", this);
		}
	}

	// Token: 0x0600447F RID: 17535 RVA: 0x0010AF10 File Offset: 0x00109110
	private void OnDestroy()
	{
		if (this.panel)
		{
			UIPanel uipanel = this.panel;
			this.panel = null;
			UIPanel.UnregisterHotSpot(uipanel, this);
		}
	}

	// Token: 0x06004480 RID: 17536 RVA: 0x0010AF44 File Offset: 0x00109144
	protected void OnEnable()
	{
		if (this.panel && this.panel.enabled)
		{
			this.EnableHotSpot();
		}
	}

	// Token: 0x06004481 RID: 17537 RVA: 0x0010AF70 File Offset: 0x00109170
	protected void OnDisable()
	{
		if (this.once)
		{
			this.DisableHotSpot();
		}
	}

	// Token: 0x06004482 RID: 17538 RVA: 0x0010AF84 File Offset: 0x00109184
	internal void OnPanelEnable()
	{
		if (base.enabled)
		{
			this.EnableHotSpot();
		}
	}

	// Token: 0x06004483 RID: 17539 RVA: 0x0010AF98 File Offset: 0x00109198
	internal void OnPanelDisable()
	{
		if (base.enabled)
		{
			this.DisableHotSpot();
		}
	}

	// Token: 0x06004484 RID: 17540 RVA: 0x0010AFAC File Offset: 0x001091AC
	internal void OnPanelDestroy()
	{
		UIPanel uipanel = this.panel;
		this.panel = null;
		if (base.enabled && uipanel && uipanel.enabled)
		{
			this.OnPanelDisable();
		}
	}

	// Token: 0x06004485 RID: 17541 RVA: 0x0010AFF0 File Offset: 0x001091F0
	private void SetBounds(bool moved, Bounds bounds, bool worldEquals)
	{
		if (this.configuredInLocalSpace)
		{
			if (this._lastBoundsEntered == bounds && worldEquals)
			{
				return;
			}
			this._lastBoundsEntered = bounds;
			AABBox.Transform3x4(ref bounds, ref this.toWorld, out this._bounds);
		}
		else
		{
			this._lastBoundsEntered = bounds;
			this._bounds = bounds;
		}
	}

	// Token: 0x06004486 RID: 17542 RVA: 0x0010B050 File Offset: 0x00109250
	protected virtual void HotSpotInit()
	{
	}

	// Token: 0x06004487 RID: 17543 RVA: 0x0010B054 File Offset: 0x00109254
	public bool ClosestRaycast(Ray ray, ref UIHotSpot.Hit hit)
	{
		UIHotSpot.Hit hit2;
		if (this.Raycast(ray, out hit2) && hit2.distance < hit.distance)
		{
			hit = hit2;
			return true;
		}
		return false;
	}

	// Token: 0x06004488 RID: 17544 RVA: 0x0010B08C File Offset: 0x0010928C
	private bool LocalRaycastRef(Ray worldRay, ref UIHotSpot.Hit hit)
	{
		Matrix4x4 matrix4x = base.transform.worldToLocalMatrix;
		Ray ray;
		ray..ctor(matrix4x.MultiplyPoint(worldRay.origin), matrix4x.MultiplyVector(worldRay.direction));
		UIHotSpot.Hit invalid = UIHotSpot.Hit.invalid;
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

	// Token: 0x06004489 RID: 17545 RVA: 0x0010B154 File Offset: 0x00109354
	private bool DoRaycastRef(Ray ray, ref UIHotSpot.Hit hit)
	{
		UIHotSpot.Kind kind = this.kind;
		switch (kind)
		{
		case UIHotSpot.Kind.Circle:
			return ((UICircleHotSpot)this).Internal_RaycastRef(ray, ref hit);
		case UIHotSpot.Kind.Rect:
			return ((UIRectHotSpot)this).Internal_RaycastRef(ray, ref hit);
		case UIHotSpot.Kind.Convex:
			return ((UIConvexHotSpot)this).Internal_RaycastRef(ray, ref hit);
		default:
			switch (kind)
			{
			case UIHotSpot.Kind.Sphere:
				return ((UISphereHotSpot)this).Internal_RaycastRef(ray, ref hit);
			case UIHotSpot.Kind.Box:
				return ((UIBoxHotSpot)this).Internal_RaycastRef(ray, ref hit);
			case UIHotSpot.Kind.Brush:
				return ((UIBrushHotSpot)this).Internal_RaycastRef(ray, ref hit);
			default:
				throw new NotImplementedException();
			}
			break;
		}
	}

	// Token: 0x0600448A RID: 17546 RVA: 0x0010B1F0 File Offset: 0x001093F0
	public bool Raycast(Ray ray, out UIHotSpot.Hit hit)
	{
		hit = UIHotSpot.Hit.invalid;
		return this.RaycastRef(ray, ref hit);
	}

	// Token: 0x0600448B RID: 17547 RVA: 0x0010B208 File Offset: 0x00109408
	public bool RaycastRef(Ray ray, ref UIHotSpot.Hit hit)
	{
		if (this.configuredInLocalSpace)
		{
			return this.LocalRaycastRef(ray, ref hit);
		}
		return this.DoRaycastRef(ray, ref hit);
	}

	// Token: 0x0600448C RID: 17548 RVA: 0x0010B228 File Offset: 0x00109428
	public static bool Raycast(Ray ray, out UIHotSpot.Hit hit, float distance)
	{
		return UIHotSpot.Global.Raycast(ray, out hit, distance);
	}

	// Token: 0x17000D64 RID: 3428
	// (get) Token: 0x0600448D RID: 17549 RVA: 0x0010B234 File Offset: 0x00109434
	protected Color gizmoColor
	{
		get
		{
			return Color.green;
		}
	}

	// Token: 0x17000D65 RID: 3429
	// (get) Token: 0x0600448E RID: 17550 RVA: 0x0010B23C File Offset: 0x0010943C
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

	// Token: 0x17000D66 RID: 3430
	// (get) Token: 0x0600448F RID: 17551 RVA: 0x0010B294 File Offset: 0x00109494
	public Vector3 worldCenter
	{
		get
		{
			UIHotSpot.Kind kind = this.kind;
			Vector3 center;
			if (kind != UIHotSpot.Kind.Circle)
			{
				if (kind != UIHotSpot.Kind.Rect)
				{
					if (kind != UIHotSpot.Kind.Sphere)
					{
						if (kind != UIHotSpot.Kind.Box)
						{
							throw new NotImplementedException();
						}
						center = ((UIBoxHotSpot)this).center;
					}
					else
					{
						center = ((UISphereHotSpot)this).center;
					}
				}
				else
				{
					center = ((UIRectHotSpot)this).center;
				}
			}
			else
			{
				center = ((UICircleHotSpot)this).center;
			}
			return base.transform.TransformPoint(center);
		}
	}

	// Token: 0x17000D67 RID: 3431
	// (get) Token: 0x06004490 RID: 17552 RVA: 0x0010B328 File Offset: 0x00109528
	// (set) Token: 0x06004491 RID: 17553 RVA: 0x0010B3A0 File Offset: 0x001095A0
	public Vector3 center
	{
		get
		{
			UIHotSpot.Kind kind = this.kind;
			if (kind == UIHotSpot.Kind.Circle)
			{
				return ((UICircleHotSpot)this).center;
			}
			if (kind == UIHotSpot.Kind.Rect)
			{
				return ((UIRectHotSpot)this).center;
			}
			if (kind == UIHotSpot.Kind.Sphere)
			{
				return ((UISphereHotSpot)this).center;
			}
			if (kind != UIHotSpot.Kind.Box)
			{
				return default(Vector3);
			}
			return ((UIBoxHotSpot)this).center;
		}
		set
		{
			UIHotSpot.Kind kind = this.kind;
			if (kind != UIHotSpot.Kind.Circle)
			{
				if (kind != UIHotSpot.Kind.Rect)
				{
					if (kind != UIHotSpot.Kind.Sphere)
					{
						if (kind == UIHotSpot.Kind.Box)
						{
							((UIBoxHotSpot)this).center = value;
						}
					}
					else
					{
						((UISphereHotSpot)this).center = value;
					}
				}
				else
				{
					((UIRectHotSpot)this).center = value;
				}
			}
			else
			{
				((UICircleHotSpot)this).center = value;
			}
		}
	}

	// Token: 0x17000D68 RID: 3432
	// (get) Token: 0x06004492 RID: 17554 RVA: 0x0010B428 File Offset: 0x00109628
	// (set) Token: 0x06004493 RID: 17555 RVA: 0x0010B4F0 File Offset: 0x001096F0
	public Vector3 size
	{
		get
		{
			UIHotSpot.Kind kind = this.kind;
			Vector3 result;
			if (kind == UIHotSpot.Kind.Circle)
			{
				result.x = ((UICircleHotSpot)this).radius * 2f;
				result.y = result.x;
				result.z = 0f;
				return result;
			}
			if (kind == UIHotSpot.Kind.Rect)
			{
				return ((UIRectHotSpot)this).size;
			}
			if (kind == UIHotSpot.Kind.Sphere)
			{
				result.x = ((UICircleHotSpot)this).radius * 1.41421354f;
				result.y = (result.z = result.x);
				return result;
			}
			if (kind != UIHotSpot.Kind.Box)
			{
				return default(Vector3);
			}
			return ((UIBoxHotSpot)this).size;
		}
		set
		{
			UIHotSpot.Kind kind = this.kind;
			if (kind != UIHotSpot.Kind.Circle)
			{
				if (kind != UIHotSpot.Kind.Rect)
				{
					if (kind != UIHotSpot.Kind.Sphere)
					{
						if (kind == UIHotSpot.Kind.Box)
						{
							((UIBoxHotSpot)this).size = value;
						}
					}
					else
					{
						value.z *= 0.707106769f;
						value.y *= 0.707106769f;
						value.x *= 0.707106769f;
						((UISphereHotSpot)this).radius = Mathf.Sqrt(value.x * value.x + value.y * value.y + value.z * value.z) / 2f;
					}
				}
				else
				{
					((UIRectHotSpot)this).size = new Vector2(value.x, value.y);
				}
			}
			else
			{
				value.y *= 0.707106769f;
				value.x *= 0.707106769f;
				((UICircleHotSpot)this).radius = Mathf.Sqrt(value.x * value.x + value.y * value.y) / 2f;
			}
		}
	}

	// Token: 0x040024AA RID: 9386
	private const UIHotSpot.Kind kKindFlag_2D = UIHotSpot.Kind.Circle;

	// Token: 0x040024AB RID: 9387
	private const UIHotSpot.Kind kKindFlag_3D = UIHotSpot.Kind.Sphere;

	// Token: 0x040024AC RID: 9388
	private const UIHotSpot.Kind kKindFlag_Radial = UIHotSpot.Kind.Circle;

	// Token: 0x040024AD RID: 9389
	private const UIHotSpot.Kind kKindFlag_Axial = UIHotSpot.Kind.Rect;

	// Token: 0x040024AE RID: 9390
	private const UIHotSpot.Kind kKindFlag_Convex = UIHotSpot.Kind.Convex;

	// Token: 0x040024AF RID: 9391
	private const float kCos45 = 0.707106769f;

	// Token: 0x040024B0 RID: 9392
	private const float k2Cos45 = 1.41421354f;

	// Token: 0x040024B1 RID: 9393
	public readonly UIHotSpot.Kind kind;

	// Token: 0x040024B2 RID: 9394
	private UIPanel panel;

	// Token: 0x040024B3 RID: 9395
	private Matrix4x4 toWorld;

	// Token: 0x040024B4 RID: 9396
	private Matrix4x4 toLocal;

	// Token: 0x040024B5 RID: 9397
	private Matrix4x4 lastWorld;

	// Token: 0x040024B6 RID: 9398
	private Matrix4x4 lastLocal;

	// Token: 0x040024B7 RID: 9399
	private Bounds _bounds;

	// Token: 0x040024B8 RID: 9400
	private Bounds _lastBoundsEntered;

	// Token: 0x040024B9 RID: 9401
	private bool once;

	// Token: 0x040024BA RID: 9402
	private bool justAdded;

	// Token: 0x040024BB RID: 9403
	private int index = -1;

	// Token: 0x040024BC RID: 9404
	private readonly bool configuredInLocalSpace;

	// Token: 0x040024BD RID: 9405
	protected static readonly Plane localPlane = new Plane(Vector3.back, Vector3.zero);

	// Token: 0x02000754 RID: 1876
	public enum Kind
	{
		// Token: 0x040024BF RID: 9407
		Circle,
		// Token: 0x040024C0 RID: 9408
		Rect,
		// Token: 0x040024C1 RID: 9409
		Convex,
		// Token: 0x040024C2 RID: 9410
		Sphere = 128,
		// Token: 0x040024C3 RID: 9411
		Box,
		// Token: 0x040024C4 RID: 9412
		Brush
	}

	// Token: 0x02000755 RID: 1877
	public struct Hit
	{
		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x06004495 RID: 17557 RVA: 0x0010B69C File Offset: 0x0010989C
		public GameObject gameObject
		{
			get
			{
				return (!this.isCollider) ? ((!this.hotSpot) ? null : this.hotSpot.gameObject) : this.collider.gameObject;
			}
		}

		// Token: 0x17000D6A RID: 3434
		// (get) Token: 0x06004496 RID: 17558 RVA: 0x0010B6E8 File Offset: 0x001098E8
		public Transform transform
		{
			get
			{
				return (!this.isCollider) ? ((!this.hotSpot) ? null : this.hotSpot.transform) : this.collider.transform;
			}
		}

		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x06004497 RID: 17559 RVA: 0x0010B734 File Offset: 0x00109934
		public Component component
		{
			get
			{
				return (!this.isCollider) ? this.hotSpot : this.collider;
			}
		}

		// Token: 0x040024C5 RID: 9413
		public UIHotSpot hotSpot;

		// Token: 0x040024C6 RID: 9414
		public Collider collider;

		// Token: 0x040024C7 RID: 9415
		public UIPanel panel;

		// Token: 0x040024C8 RID: 9416
		public Vector3 point;

		// Token: 0x040024C9 RID: 9417
		public Vector3 normal;

		// Token: 0x040024CA RID: 9418
		public Ray ray;

		// Token: 0x040024CB RID: 9419
		public float distance;

		// Token: 0x040024CC RID: 9420
		public bool isCollider;

		// Token: 0x040024CD RID: 9421
		public static readonly UIHotSpot.Hit invalid = new UIHotSpot.Hit
		{
			distance = float.PositiveInfinity,
			ray = default(Ray),
			point = default(Vector3),
			normal = default(Vector3)
		};
	}

	// Token: 0x02000756 RID: 1878
	private static class Global
	{
		// Token: 0x06004499 RID: 17561 RVA: 0x0010B760 File Offset: 0x00109960
		public static bool Add(UIHotSpot hotSpot)
		{
			if (hotSpot.index != -1)
			{
				return false;
			}
			UIHotSpot.Kind kind = hotSpot.kind;
			switch (kind)
			{
			case UIHotSpot.Kind.Circle:
				UIHotSpot.Global.Circle.Add((UICircleHotSpot)hotSpot);
				break;
			case UIHotSpot.Kind.Rect:
				UIHotSpot.Global.Rect.Add((UIRectHotSpot)hotSpot);
				break;
			case UIHotSpot.Kind.Convex:
				UIHotSpot.Global.Convex.Add((UIConvexHotSpot)hotSpot);
				break;
			default:
				switch (kind)
				{
				case UIHotSpot.Kind.Sphere:
					UIHotSpot.Global.Sphere.Add((UISphereHotSpot)hotSpot);
					break;
				case UIHotSpot.Kind.Box:
					UIHotSpot.Global.Box.Add((UIBoxHotSpot)hotSpot);
					break;
				case UIHotSpot.Kind.Brush:
					UIHotSpot.Global.Brush.Add((UIBrushHotSpot)hotSpot);
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

		// Token: 0x0600449A RID: 17562 RVA: 0x0010B858 File Offset: 0x00109A58
		public static bool Remove(UIHotSpot hotSpot)
		{
			if (hotSpot.index == -1)
			{
				return false;
			}
			UIHotSpot.Kind kind = hotSpot.kind;
			switch (kind)
			{
			case UIHotSpot.Kind.Circle:
				UIHotSpot.Global.Circle.Erase((UICircleHotSpot)hotSpot);
				break;
			case UIHotSpot.Kind.Rect:
				UIHotSpot.Global.Rect.Erase((UIRectHotSpot)hotSpot);
				break;
			case UIHotSpot.Kind.Convex:
				UIHotSpot.Global.Convex.Erase((UIConvexHotSpot)hotSpot);
				break;
			default:
				switch (kind)
				{
				case UIHotSpot.Kind.Sphere:
					UIHotSpot.Global.Sphere.Erase((UISphereHotSpot)hotSpot);
					break;
				case UIHotSpot.Kind.Box:
					UIHotSpot.Global.Box.Erase((UIBoxHotSpot)hotSpot);
					break;
				case UIHotSpot.Kind.Brush:
					UIHotSpot.Global.Brush.Erase((UIBrushHotSpot)hotSpot);
					break;
				default:
					throw new NotImplementedException();
				}
				break;
			}
			return true;
		}

		// Token: 0x0600449B RID: 17563 RVA: 0x0010B930 File Offset: 0x00109B30
		private static bool MatrixEquals(ref Matrix4x4 a, ref Matrix4x4 b)
		{
			return a.m03 == b.m03 && a.m12 == b.m13 && a.m23 == b.m23 && a.m00 == b.m00 && a.m11 == b.m11 && a.m22 == b.m22 && a.m01 == b.m01 && a.m12 == b.m12 && a.m20 == b.m20 && a.m02 == b.m02 && a.m10 == b.m10 && a.m21 == b.m21 && a.m30 == b.m30 && a.m31 == b.m31 && a.m32 == b.m32 && a.m33 == b.m33;
		}

		// Token: 0x0600449C RID: 17564 RVA: 0x0010BA50 File Offset: 0x00109C50
		private static Bounds? DoStep()
		{
			Bounds value = default(Bounds);
			bool flag = true;
			int num = UIHotSpot.Global.allCount;
			if (UIHotSpot.Global.Circle.any)
			{
				for (int i = 0; i < UIHotSpot.Global.Circle.count; i++)
				{
					UICircleHotSpot uicircleHotSpot = UIHotSpot.Global.Circle.array[i];
					Transform transform = uicircleHotSpot.transform;
					uicircleHotSpot.lastWorld = uicircleHotSpot.toWorld;
					uicircleHotSpot.toWorld = transform.localToWorldMatrix;
					uicircleHotSpot.lastLocal = uicircleHotSpot.toLocal;
					uicircleHotSpot.toLocal = transform.worldToLocalMatrix;
					bool worldEquals;
					bool flag2 = !(worldEquals = !uicircleHotSpot.justAdded) || !(worldEquals = UIHotSpot.Global.MatrixEquals(ref uicircleHotSpot.toWorld, ref uicircleHotSpot.lastWorld));
					bool moved = uicircleHotSpot.justAdded || ((!uicircleHotSpot.configuredInLocalSpace) ? flag2 : (!UIHotSpot.Global.MatrixEquals(ref uicircleHotSpot.toLocal, ref uicircleHotSpot.lastLocal)));
					UIHotSpot uihotSpot = uicircleHotSpot;
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
			if (UIHotSpot.Global.Rect.any)
			{
				for (int j = 0; j < UIHotSpot.Global.Rect.count; j++)
				{
					UIRectHotSpot uirectHotSpot = UIHotSpot.Global.Rect.array[j];
					Transform transform = uirectHotSpot.transform;
					uirectHotSpot.lastWorld = uirectHotSpot.toWorld;
					uirectHotSpot.toWorld = transform.localToWorldMatrix;
					uirectHotSpot.lastLocal = uirectHotSpot.toLocal;
					uirectHotSpot.toLocal = transform.worldToLocalMatrix;
					bool worldEquals;
					bool flag2 = !(worldEquals = !uirectHotSpot.justAdded) || !(worldEquals = UIHotSpot.Global.MatrixEquals(ref uirectHotSpot.toWorld, ref uirectHotSpot.lastWorld));
					bool moved = uirectHotSpot.justAdded || ((!uirectHotSpot.configuredInLocalSpace) ? flag2 : (!UIHotSpot.Global.MatrixEquals(ref uirectHotSpot.toLocal, ref uirectHotSpot.lastLocal)));
					UIHotSpot uihotSpot2 = uirectHotSpot;
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
			if (UIHotSpot.Global.Convex.any)
			{
				for (int k = 0; k < UIHotSpot.Global.Convex.count; k++)
				{
					UIConvexHotSpot uiconvexHotSpot = UIHotSpot.Global.Convex.array[k];
					Transform transform = uiconvexHotSpot.transform;
					uiconvexHotSpot.lastWorld = uiconvexHotSpot.toWorld;
					uiconvexHotSpot.toWorld = transform.localToWorldMatrix;
					uiconvexHotSpot.lastLocal = uiconvexHotSpot.toLocal;
					uiconvexHotSpot.toLocal = transform.worldToLocalMatrix;
					bool worldEquals;
					bool flag2 = !(worldEquals = !uiconvexHotSpot.justAdded) || !(worldEquals = UIHotSpot.Global.MatrixEquals(ref uiconvexHotSpot.toWorld, ref uiconvexHotSpot.lastWorld));
					bool moved = uiconvexHotSpot.justAdded || ((!uiconvexHotSpot.configuredInLocalSpace) ? flag2 : (!UIHotSpot.Global.MatrixEquals(ref uiconvexHotSpot.toLocal, ref uiconvexHotSpot.lastLocal)));
					UIHotSpot uihotSpot3 = uiconvexHotSpot;
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
			if (UIHotSpot.Global.Sphere.any)
			{
				for (int l = 0; l < UIHotSpot.Global.Sphere.count; l++)
				{
					UISphereHotSpot uisphereHotSpot = UIHotSpot.Global.Sphere.array[l];
					Transform transform = uisphereHotSpot.transform;
					uisphereHotSpot.lastWorld = uisphereHotSpot.toWorld;
					uisphereHotSpot.toWorld = transform.localToWorldMatrix;
					uisphereHotSpot.lastLocal = uisphereHotSpot.toLocal;
					uisphereHotSpot.toLocal = transform.worldToLocalMatrix;
					bool worldEquals;
					bool flag2 = !(worldEquals = !uisphereHotSpot.justAdded) || !(worldEquals = UIHotSpot.Global.MatrixEquals(ref uisphereHotSpot.toWorld, ref uisphereHotSpot.lastWorld));
					bool moved = uisphereHotSpot.justAdded || ((!uisphereHotSpot.configuredInLocalSpace) ? flag2 : (!UIHotSpot.Global.MatrixEquals(ref uisphereHotSpot.toLocal, ref uisphereHotSpot.lastLocal)));
					UIHotSpot uihotSpot4 = uisphereHotSpot;
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
			if (UIHotSpot.Global.Box.any)
			{
				for (int m = 0; m < UIHotSpot.Global.Box.count; m++)
				{
					UIBoxHotSpot uiboxHotSpot = UIHotSpot.Global.Box.array[m];
					Transform transform = uiboxHotSpot.transform;
					uiboxHotSpot.lastWorld = uiboxHotSpot.toWorld;
					uiboxHotSpot.toWorld = transform.localToWorldMatrix;
					uiboxHotSpot.lastLocal = uiboxHotSpot.toLocal;
					uiboxHotSpot.toLocal = transform.worldToLocalMatrix;
					bool worldEquals;
					bool flag2 = !(worldEquals = !uiboxHotSpot.justAdded) || !(worldEquals = UIHotSpot.Global.MatrixEquals(ref uiboxHotSpot.toWorld, ref uiboxHotSpot.lastWorld));
					bool moved = uiboxHotSpot.justAdded || ((!uiboxHotSpot.configuredInLocalSpace) ? flag2 : (!UIHotSpot.Global.MatrixEquals(ref uiboxHotSpot.toLocal, ref uiboxHotSpot.lastLocal)));
					UIHotSpot uihotSpot5 = uiboxHotSpot;
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
			if (UIHotSpot.Global.Brush.any)
			{
				for (int n = 0; n < UIHotSpot.Global.Brush.count; n++)
				{
					UIBrushHotSpot uibrushHotSpot = UIHotSpot.Global.Brush.array[n];
					Transform transform = uibrushHotSpot.transform;
					uibrushHotSpot.lastWorld = uibrushHotSpot.toWorld;
					uibrushHotSpot.toWorld = transform.localToWorldMatrix;
					uibrushHotSpot.lastLocal = uibrushHotSpot.toLocal;
					uibrushHotSpot.toLocal = transform.worldToLocalMatrix;
					bool worldEquals;
					bool flag2 = !(worldEquals = !uibrushHotSpot.justAdded) || !(worldEquals = UIHotSpot.Global.MatrixEquals(ref uibrushHotSpot.toWorld, ref uibrushHotSpot.lastWorld));
					bool moved = uibrushHotSpot.justAdded || ((!uibrushHotSpot.configuredInLocalSpace) ? flag2 : (!UIHotSpot.Global.MatrixEquals(ref uibrushHotSpot.toLocal, ref uibrushHotSpot.lastLocal)));
					UIHotSpot uihotSpot6 = uibrushHotSpot;
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

		// Token: 0x0600449D RID: 17565 RVA: 0x0010C45C File Offset: 0x0010A65C
		private static bool DoRaycast(Ray ray, out UIHotSpot.Hit hit, float dist)
		{
			hit = UIHotSpot.Hit.invalid;
			UIHotSpot.Hit invalid = UIHotSpot.Hit.invalid;
			bool flag = true;
			Vector3 origin = ray.origin;
			int num = UIHotSpot.Global.allCount;
			float num2;
			if (UIHotSpot.Global.Circle.any)
			{
				for (int i = 0; i < UIHotSpot.Global.Circle.count; i++)
				{
					UICircleHotSpot uicircleHotSpot = UIHotSpot.Global.Circle.array[i];
					if ((uicircleHotSpot._bounds.Contains(origin) || (uicircleHotSpot._bounds.IntersectRay(ray, ref num2) && num2 < dist)) && uicircleHotSpot.panel.InsideClippingRect(ray, UIHotSpot.Global.lastStepFrame) && uicircleHotSpot.RaycastRef(ray, ref invalid) && invalid.distance < dist)
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
			if (UIHotSpot.Global.Rect.any)
			{
				for (int j = 0; j < UIHotSpot.Global.Rect.count; j++)
				{
					UIRectHotSpot uirectHotSpot = UIHotSpot.Global.Rect.array[j];
					if ((uirectHotSpot._bounds.Contains(origin) || (uirectHotSpot._bounds.IntersectRay(ray, ref num2) && num2 < dist)) && uirectHotSpot.panel.InsideClippingRect(ray, UIHotSpot.Global.lastStepFrame) && uirectHotSpot.RaycastRef(ray, ref invalid) && invalid.distance < dist)
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
			if (UIHotSpot.Global.Convex.any)
			{
				for (int k = 0; k < UIHotSpot.Global.Convex.count; k++)
				{
					UIConvexHotSpot uiconvexHotSpot = UIHotSpot.Global.Convex.array[k];
					if ((uiconvexHotSpot._bounds.Contains(origin) || (uiconvexHotSpot._bounds.IntersectRay(ray, ref num2) && num2 < dist)) && uiconvexHotSpot.panel.InsideClippingRect(ray, UIHotSpot.Global.lastStepFrame) && uiconvexHotSpot.RaycastRef(ray, ref invalid) && invalid.distance < dist)
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
			if (UIHotSpot.Global.Sphere.any)
			{
				for (int l = 0; l < UIHotSpot.Global.Sphere.count; l++)
				{
					UISphereHotSpot uisphereHotSpot = UIHotSpot.Global.Sphere.array[l];
					if ((uisphereHotSpot._bounds.Contains(origin) || (uisphereHotSpot._bounds.IntersectRay(ray, ref num2) && num2 < dist)) && uisphereHotSpot.panel.InsideClippingRect(ray, UIHotSpot.Global.lastStepFrame) && uisphereHotSpot.RaycastRef(ray, ref invalid) && invalid.distance < dist)
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
			if (UIHotSpot.Global.Box.any)
			{
				for (int m = 0; m < UIHotSpot.Global.Box.count; m++)
				{
					UIBoxHotSpot uiboxHotSpot = UIHotSpot.Global.Box.array[m];
					if ((uiboxHotSpot._bounds.Contains(origin) || (uiboxHotSpot._bounds.IntersectRay(ray, ref num2) && num2 < dist)) && uiboxHotSpot.panel.InsideClippingRect(ray, UIHotSpot.Global.lastStepFrame) && uiboxHotSpot.RaycastRef(ray, ref invalid) && invalid.distance < dist)
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
			if (UIHotSpot.Global.Brush.any)
			{
				for (int n = 0; n < UIHotSpot.Global.Brush.count; n++)
				{
					UIBrushHotSpot uibrushHotSpot = UIHotSpot.Global.Brush.array[n];
					if ((uibrushHotSpot._bounds.Contains(origin) || (uibrushHotSpot._bounds.IntersectRay(ray, ref num2) && num2 < dist)) && uibrushHotSpot.panel.InsideClippingRect(ray, UIHotSpot.Global.lastStepFrame) && uibrushHotSpot.RaycastRef(ray, ref invalid) && invalid.distance < dist)
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

		// Token: 0x0600449E RID: 17566 RVA: 0x0010C9AC File Offset: 0x0010ABAC
		public static void Step()
		{
			if (UIHotSpot.Global.allAny)
			{
				Bounds? bounds = UIHotSpot.Global.DoStep();
				UIHotSpot.Global.validBounds = (bounds != null);
				if (UIHotSpot.Global.validBounds)
				{
					UIHotSpot.Global.allBounds = bounds.Value;
				}
			}
			else
			{
				UIHotSpot.Global.validBounds = false;
			}
		}

		// Token: 0x0600449F RID: 17567 RVA: 0x0010C9F8 File Offset: 0x0010ABF8
		public static bool Raycast(Ray ray, out UIHotSpot.Hit hit, float distance)
		{
			if (!UIHotSpot.Global.allAny)
			{
				hit = UIHotSpot.Hit.invalid;
				return false;
			}
			int frameCount = Time.frameCount;
			if (UIHotSpot.Global.lastStepFrame != frameCount || UIHotSpot.Global.anyRemovedRecently || UIHotSpot.Global.anyAddedRecently)
			{
				UIHotSpot.Global.Step();
				UIHotSpot.Global.anyRemovedRecently = (UIHotSpot.Global.anyAddedRecently = false);
			}
			UIHotSpot.Global.lastStepFrame = frameCount;
			if (!UIHotSpot.Global.validBounds)
			{
				hit = UIHotSpot.Hit.invalid;
				return false;
			}
			if (UIHotSpot.Global.allBounds.Contains(ray.origin))
			{
				float num = 0f;
			}
			else
			{
				float num;
				if (!UIHotSpot.Global.allBounds.IntersectRay(ray, ref num) || num > distance)
				{
					hit = UIHotSpot.Hit.invalid;
					return false;
				}
				if (num != 0f)
				{
					ray.origin = ray.GetPoint(num - 0.001f);
					num = 0f;
				}
			}
			return UIHotSpot.Global.DoRaycast(ray, out hit, distance);
		}

		// Token: 0x040024CE RID: 9422
		private static int allCount;

		// Token: 0x040024CF RID: 9423
		private static bool allAny;

		// Token: 0x040024D0 RID: 9424
		private static Bounds allBounds;

		// Token: 0x040024D1 RID: 9425
		private static bool validBounds;

		// Token: 0x040024D2 RID: 9426
		private static bool anyAddedRecently;

		// Token: 0x040024D3 RID: 9427
		private static bool anyRemovedRecently;

		// Token: 0x040024D4 RID: 9428
		private static UIHotSpot.Global.List<UICircleHotSpot> Circle;

		// Token: 0x040024D5 RID: 9429
		private static UIHotSpot.Global.List<UIRectHotSpot> Rect;

		// Token: 0x040024D6 RID: 9430
		private static UIHotSpot.Global.List<UIConvexHotSpot> Convex;

		// Token: 0x040024D7 RID: 9431
		private static UIHotSpot.Global.List<UISphereHotSpot> Sphere;

		// Token: 0x040024D8 RID: 9432
		private static UIHotSpot.Global.List<UIBoxHotSpot> Box;

		// Token: 0x040024D9 RID: 9433
		private static UIHotSpot.Global.List<UIBrushHotSpot> Brush;

		// Token: 0x040024DA RID: 9434
		private static int lastStepFrame = int.MinValue;

		// Token: 0x02000757 RID: 1879
		private struct List<THotSpot> where THotSpot : UIHotSpot
		{
			// Token: 0x060044A0 RID: 17568 RVA: 0x0010CAE4 File Offset: 0x0010ACE4
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
				if (UIHotSpot.Global.allCount++ == 0)
				{
					UIHotSpot.Global.allAny = true;
				}
				UIHotSpot.Global.anyAddedRecently = true;
			}

			// Token: 0x060044A1 RID: 17569 RVA: 0x0010CB80 File Offset: 0x0010AD80
			public void Erase(THotSpot hotSpot)
			{
				UIHotSpot.Global.allCount--;
				if (--this.count == hotSpot.index)
				{
					this.array[hotSpot.index] = (THotSpot)((object)null);
					this.any = (this.count > 0);
					if (!this.any)
					{
						UIHotSpot.Global.allAny = (UIHotSpot.Global.allCount > 0);
					}
				}
				else
				{
					(this.array[hotSpot.index] = this.array[this.count]).index = hotSpot.index;
					this.array[this.count] = (THotSpot)((object)null);
				}
				hotSpot.index = -1;
				UIHotSpot.Global.anyRemovedRecently = true;
			}

			// Token: 0x040024DB RID: 9435
			public THotSpot[] array;

			// Token: 0x040024DC RID: 9436
			public int count;

			// Token: 0x040024DD RID: 9437
			public int capacity;

			// Token: 0x040024DE RID: 9438
			public bool any;
		}
	}
}
