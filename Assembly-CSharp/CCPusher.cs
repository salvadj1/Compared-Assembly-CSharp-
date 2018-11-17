using System;
using Facepunch.Geometry;
using UnityEngine;

// Token: 0x0200028A RID: 650
public class CCPusher : MonoBehaviour
{
	// Token: 0x0600177F RID: 6015 RVA: 0x0005B07C File Offset: 0x0005927C
	private void OnDrawGizmosSelected()
	{
		Collider collider = base.collider;
		if (collider)
		{
			Gizmos.color = new Color(0.5f, 0.5f, 1f, 0.8f);
			this.shape.Transform(ColliderUtility.ColliderToWorld(collider)).Gizmo();
			Matrix4x4 localToWorldMatrix = base.transform.localToWorldMatrix;
			Gizmos.color = new Color(0.9f, 0.5f, 1f, 0.8f);
			CCPusher.DrawPushPlane(localToWorldMatrix, this.pushPoint0, this.pushDir0);
			Gizmos.color = new Color(1f, 0.5f, 0.5f, 0.8f);
			CCPusher.DrawPushPlane(localToWorldMatrix, this.pushPoint1, this.pushDir1);
		}
	}

	// Token: 0x06001780 RID: 6016 RVA: 0x0005B148 File Offset: 0x00059348
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

	// Token: 0x06001781 RID: 6017 RVA: 0x0005B1D0 File Offset: 0x000593D0
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

	// Token: 0x06001782 RID: 6018 RVA: 0x0005B224 File Offset: 0x00059424
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

	// Token: 0x04000C47 RID: 3143
	[SerializeField]
	private ShapeDefinition shape;

	// Token: 0x04000C48 RID: 3144
	[SerializeField]
	private Vector3 pushPoint0 = Vector3.forward;

	// Token: 0x04000C49 RID: 3145
	[SerializeField]
	private Vector3 pushDir0 = Vector3.back;

	// Token: 0x04000C4A RID: 3146
	[SerializeField]
	private Vector3 pushPoint1 = Vector3.back;

	// Token: 0x04000C4B RID: 3147
	[SerializeField]
	private Vector3 pushDir1 = Vector3.forward;

	// Token: 0x04000C4C RID: 3148
	[SerializeField]
	private float pushSpeed = 3f;
}
