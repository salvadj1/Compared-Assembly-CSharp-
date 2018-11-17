using System;
using Facepunch.Precision;
using UnityEngine;

// Token: 0x02000751 RID: 1873
public class TransformTest : MonoBehaviour
{
	// Token: 0x06003DFB RID: 15867 RVA: 0x000E059C File Offset: 0x000DE79C
	private void OnDrawGizmos()
	{
		Matrix4x4G matrix4x4G;
		Precise.ExtractLocalToWorld(base.transform, ref matrix4x4G);
		Matrix4x4 localToWorldMatrix = base.transform.localToWorldMatrix;
		Vector3 vector = localToWorldMatrix.MultiplyPoint(Vector3.zero);
		Vector3 vector2 = localToWorldMatrix.MultiplyPoint(Vector3.forward);
		Vector3 vector3 = localToWorldMatrix.MultiplyPoint(Vector3.up);
		Vector3 vector4 = localToWorldMatrix.MultiplyPoint(Vector3.right);
		Vector3G vector3G;
		vector3G.x = 1.0;
		vector3G.y = 0.0;
		vector3G.z = 0.0;
		Vector3G vector3G2;
		Matrix4x4G.Mult(ref vector3G, ref matrix4x4G, ref vector3G2);
		vector3G.x = 0.0;
		vector3G.y = 1.0;
		vector3G.z = 0.0;
		Vector3G vector3G3;
		Matrix4x4G.Mult(ref vector3G, ref matrix4x4G, ref vector3G3);
		vector3G.x = 0.0;
		vector3G.y = 0.0;
		vector3G.z = 1.0;
		Vector3G vector3G4;
		Matrix4x4G.Mult(ref vector3G, ref matrix4x4G, ref vector3G4);
		vector3G.x = 0.0;
		vector3G.y = 0.0;
		vector3G.z = 0.0;
		Vector3G vector3G5;
		Matrix4x4G.Mult(ref vector3G, ref matrix4x4G, ref vector3G5);
		Gizmos.color = Color.red * new Color(1f, 1f, 1f, 0.5f);
		Gizmos.DrawLine(vector, vector4);
		Gizmos.color = Color.green * new Color(1f, 1f, 1f, 0.5f);
		Gizmos.DrawLine(vector, vector3);
		Gizmos.color = Color.blue * new Color(1f, 1f, 1f, 0.5f);
		Gizmos.DrawLine(vector, vector2);
		Gizmos.color = Color.red * new Color(1f, 1f, 1f, 1f);
		Gizmos.DrawLine(vector3G5.f, vector3G2.f);
		Gizmos.color = Color.green * new Color(1f, 1f, 1f, 1f);
		Gizmos.DrawLine(vector3G5.f, vector3G3.f);
		Gizmos.color = Color.blue * new Color(1f, 1f, 1f, 1f);
		Gizmos.DrawLine(vector3G5.f, vector3G4.f);
	}
}
