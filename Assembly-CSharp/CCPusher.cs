using System;
using Facepunch.Geometry;
using UnityEngine;

// Token: 0x020002C1 RID: 705
public class CCPusher : MonoBehaviour
{
	// Token: 0x060018E7 RID: 6375 RVA: 0x0005F5D0 File Offset: 0x0005D7D0
	private void OnDrawGizmosSelected()
	{
		Collider collider = base.collider;
		if (collider)
		{
			Gizmos.color = new Color(0.5f, 0.5f, 1f, 0.8f);
			this.shape.Transform(ColliderUtility.ColliderToWorld(collider)).Gizmo();
			Matrix4x4 localToWorldMatrix = base.transform.localToWorldMatrix;
			Gizmos.color = new Color(0.9f, 0.5f, 1f, 0.8f);
			global::CCPusher.DrawPushPlane(localToWorldMatrix, this.pushPoint0, this.pushDir0);
			Gizmos.color = new Color(1f, 0.5f, 0.5f, 0.8f);
			global::CCPusher.DrawPushPlane(localToWorldMatrix, this.pushPoint1, this.pushDir1);
		}
	}

	// Token: 0x060018E8 RID: 6376 RVA: 0x0005F69C File Offset: 0x0005D89C
	private static void DrawPushPlane(Matrix4x4 trs, Vector3 point, Vector3 dir)
	{
		point = trs.MultiplyPoint3x4(point);
		dir = trs.MultiplyVector(dir);
		Vector3 vector = point + dir.normalized * 0.1f;
		Gizmos.DrawLine(point, vector);
		Matrix4x4 matrix = Gizmos.matrix;
		Gizmos.matrix = matrix * Matrix4x4.TRS(point, Quaternion.LookRotation(dir), Vector3.one);
		Gizmos.DrawWireCube(Vector3.zero, new Vector3(1f, 1f, 0.0001f));
		Gizmos.matrix = matrix;
	}

	// Token: 0x060018E9 RID: 6377 RVA: 0x0005F724 File Offset: 0x0005D924
	private void Reset()
	{
		if (this.shape == null)
		{
			this.shape = new ShapeDefinition();
		}
		Shape shape;
		if (base.collider && ColliderUtility.GetGeometricShapeLocal(base.collider, ref shape))
		{
			this.shape.Shape = shape;
		}
	}

	// Token: 0x060018EA RID: 6378 RVA: 0x0005F778 File Offset: 0x0005D978
	public bool Push(Sphere Sphere, ref Vector3 Velocity)
	{
		if (this.shape.Shape.Intersects(Sphere))
		{
			Plane plane = Plane.DirectionPoint(this.pushDir0, this.pushPoint0);
			Plane plane2 = Plane.DirectionPoint(this.pushDir1, this.pushPoint1);
			float num = plane.DistanceTo(Sphere.Center + (Normal)plane.Direction * Sphere.Radius);
			float num2 = plane2.DistanceTo(Sphere.Center + (Normal)plane2.Direction * Sphere.Radius);
			Vector vector;
			if (num > num2)
			{
				vector = (Normal)plane.Direction * (this.pushSpeed * Time.deltaTime);
			}
			else
			{
				vector = (Normal)plane2.Direction * (this.pushSpeed * Time.deltaTime);
			}
			Velocity.x += vector.x;
			Velocity.y += vector.y;
			Velocity.z += vector.z;
			return true;
		}
		return false;
	}

	// Token: 0x04000D72 RID: 3442
	[SerializeField]
	private ShapeDefinition shape;

	// Token: 0x04000D73 RID: 3443
	[SerializeField]
	private Vector3 pushPoint0 = Vector3.forward;

	// Token: 0x04000D74 RID: 3444
	[SerializeField]
	private Vector3 pushDir0 = Vector3.back;

	// Token: 0x04000D75 RID: 3445
	[SerializeField]
	private Vector3 pushPoint1 = Vector3.back;

	// Token: 0x04000D76 RID: 3446
	[SerializeField]
	private Vector3 pushDir1 = Vector3.forward;

	// Token: 0x04000D77 RID: 3447
	[SerializeField]
	private float pushSpeed = 3f;
}
