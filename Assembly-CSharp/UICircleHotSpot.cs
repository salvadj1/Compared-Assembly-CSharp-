using System;
using UnityEngine;

// Token: 0x02000751 RID: 1873
public class UICircleHotSpot : UIHotSpot
{
	// Token: 0x06004455 RID: 17493 RVA: 0x0010A96C File Offset: 0x00108B6C
	public UICircleHotSpot() : base(UIHotSpot.Kind.Circle, true)
	{
	}

	// Token: 0x17000D4E RID: 3406
	// (get) Token: 0x06004456 RID: 17494 RVA: 0x0010A984 File Offset: 0x00108B84
	// (set) Token: 0x06004457 RID: 17495 RVA: 0x0010A994 File Offset: 0x00108B94
	public new float size
	{
		get
		{
			return this.radius * 2f;
		}
		set
		{
			this.radius = value / 2f;
		}
	}

	// Token: 0x06004458 RID: 17496 RVA: 0x0010A9A4 File Offset: 0x00108BA4
	internal Bounds? Internal_CalculateBounds(bool moved)
	{
		float num = this.radius * 2f;
		return new Bounds?(new Bounds(this.center, new Vector3(num, num, 0f)));
	}

	// Token: 0x06004459 RID: 17497 RVA: 0x0010A9DC File Offset: 0x00108BDC
	internal bool Internal_RaycastRef(Ray ray, ref UIHotSpot.Hit hit)
	{
		if (this.radius == 0f)
		{
			return false;
		}
		Plane plane;
		plane..ctor(UIHotSpot.forward, this.center);
		float num;
		if (!plane.Raycast(ray, ref num))
		{
			hit = default(UIHotSpot.Hit);
			return false;
		}
		hit.point = ray.GetPoint(num);
		hit.normal = ((!plane.GetSide(ray.origin)) ? UIHotSpot.backward : UIHotSpot.forward);
		Vector2 vector;
		vector.x = hit.point.x - this.center.x;
		vector.y = hit.point.y - this.center.y;
		float num2 = vector.x * vector.x + vector.y * vector.y;
		if (num2 < this.radius * this.radius)
		{
			hit.distance = Mathf.Sqrt(num2);
			return true;
		}
		return false;
	}

	// Token: 0x0600445A RID: 17498 RVA: 0x0010AAE4 File Offset: 0x00108CE4
	private void OnDrawGizmos()
	{
		Gizmos.matrix = base.gizmoMatrix * Matrix4x4.TRS(this.center, Quaternion.identity, new Vector3(1f, 1f, 0.0001f));
		Gizmos.color = base.gizmoColor;
		Gizmos.DrawWireSphere(Vector3.zero, this.radius);
	}

	// Token: 0x040024A6 RID: 9382
	private const UIHotSpot.Kind kKind = UIHotSpot.Kind.Circle;

	// Token: 0x040024A7 RID: 9383
	public new Vector3 center;

	// Token: 0x040024A8 RID: 9384
	public float radius = 0.5f;
}
