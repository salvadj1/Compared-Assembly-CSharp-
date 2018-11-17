using System;
using UnityEngine;

// Token: 0x0200074F RID: 1871
public class TransformLerpTest : MonoBehaviour
{
	// Token: 0x06003DF5 RID: 15861 RVA: 0x000E00B0 File Offset: 0x000DE2B0
	private Matrix4x4 Interp(float t, Matrix4x4 a, Matrix4x4 b)
	{
		Matrix4x4 result;
		switch (this.mode)
		{
		default:
			result = global::TransitionFunctions.Slerp(t, a, b);
			break;
		case global::TransformLerpTest.SlerpMode.TransformLerp:
		case global::TransformLerpTest.SlerpMode.WorldToCameraLerp:
		case global::TransformLerpTest.SlerpMode.CameraToWorldLerp:
			result = global::TransitionFunctions.Linear(t, a, b);
			break;
		case global::TransformLerpTest.SlerpMode.WorldToCameraSlerp2:
			result = global::TransitionFunctions.SlerpWorldToCamera(t, a, b);
			break;
		}
		if (this.inverse0)
		{
			if (this.transpose)
			{
				if (!this.inverse1)
				{
					return result.inverse.transpose;
				}
				return result.inverse.transpose.inverse;
			}
			else
			{
				if (this.inverse1)
				{
					return result.inverse.inverse;
				}
				return result.inverse;
			}
		}
		else if (this.transpose)
		{
			if (this.inverse1)
			{
				return result.transpose.inverse;
			}
			return result.transpose;
		}
		else
		{
			if (this.inverse1)
			{
				return result.inverse;
			}
			return result;
		}
	}

	// Token: 0x17000BBD RID: 3005
	// (get) Token: 0x06003DF6 RID: 15862 RVA: 0x000E01C8 File Offset: 0x000DE3C8
	private bool ready
	{
		get
		{
			switch (this.mode)
			{
			default:
				return this.a && this.b;
			case global::TransformLerpTest.SlerpMode.WorldToCameraSlerp:
			case global::TransformLerpTest.SlerpMode.WorldToCameraLerp:
			case global::TransformLerpTest.SlerpMode.CameraToWorldSlerp:
			case global::TransformLerpTest.SlerpMode.CameraToWorldLerp:
				return this.a && this.b && this.a.camera && this.b.camera;
			}
		}
	}

	// Token: 0x06003DF7 RID: 15863 RVA: 0x000E0268 File Offset: 0x000DE468
	private Matrix4x4 GetMatrix(Transform a)
	{
		switch (this.mode)
		{
		default:
			if (a.camera)
			{
				return a.camera.worldToCameraMatrix * a.localToWorldMatrix;
			}
			return a.localToWorldMatrix;
		case global::TransformLerpTest.SlerpMode.WorldToCameraSlerp:
		case global::TransformLerpTest.SlerpMode.WorldToCameraLerp:
		case global::TransformLerpTest.SlerpMode.WorldToCameraSlerp2:
			return a.camera.worldToCameraMatrix;
		case global::TransformLerpTest.SlerpMode.CameraToWorldSlerp:
		case global::TransformLerpTest.SlerpMode.CameraToWorldLerp:
			return a.camera.cameraToWorldMatrix;
		}
	}

	// Token: 0x06003DF8 RID: 15864 RVA: 0x000E02E8 File Offset: 0x000DE4E8
	private static void DrawAxes(Matrix4x4 m, float alpha)
	{
		Vector3 vector = m.MultiplyPoint(Vector3.zero);
		Gizmos.color = new Color(1f, 1f, 1f, alpha);
		Gizmos.DrawSphere(vector, 0.01f);
		Gizmos.color = new Color(1f, 0f, 0f, alpha);
		Gizmos.DrawLine(vector, m.MultiplyPoint(Vector3.right));
		Gizmos.color = new Color(0f, 1f, 0f, alpha);
		Gizmos.DrawLine(vector, m.MultiplyPoint(Vector3.up));
		Gizmos.color = new Color(0f, 0f, 1f, alpha);
		Gizmos.DrawLine(vector, m.MultiplyPoint(Vector3.forward));
	}

	// Token: 0x06003DF9 RID: 15865 RVA: 0x000E03AC File Offset: 0x000DE5AC
	private void OnDrawGizmos()
	{
		if (this.ready)
		{
			Matrix4x4 matrix = this.GetMatrix(this.a);
			Matrix4x4 matrix2 = this.GetMatrix(this.b);
			float num = (!this.cap) ? this.t : Mathf.Clamp01(this.t);
			Matrix4x4 m = this.Interp(0f, matrix, matrix2);
			global::TransformLerpTest.DrawAxes(m, 0.5f);
			for (int i = 1; i <= 32; i++)
			{
				Matrix4x4 matrix4x = this.Interp((float)i / 32f, matrix, matrix2);
				Gizmos.color = Color.white * 0.5f;
				Gizmos.DrawLine(m.MultiplyPoint(Vector3.zero), matrix4x.MultiplyPoint(Vector3.zero));
				Gizmos.color = Color.red * 0.5f;
				Gizmos.DrawLine(m.MultiplyPoint(Vector3.right), matrix4x.MultiplyPoint(Vector3.right));
				Gizmos.color = Color.green * 0.5f;
				Gizmos.DrawLine(m.MultiplyPoint(Vector3.up), matrix4x.MultiplyPoint(Vector3.up));
				Gizmos.color = Color.blue * 0.5f;
				Gizmos.DrawLine(m.MultiplyPoint(Vector3.forward), matrix4x.MultiplyPoint(Vector3.forward));
				m = matrix4x;
			}
			global::TransformLerpTest.DrawAxes(m, 0.5f);
			m = this.Interp(num, matrix, matrix2);
			global::TransformLerpTest.DrawAxes(m, 1f);
			this.angleXY = Vector3.Angle(m.MultiplyVector(Vector3.right), m.MultiplyVector(Vector3.up));
			this.angleYZ = Vector3.Angle(m.MultiplyVector(Vector3.up), m.MultiplyVector(Vector3.forward));
			this.angleZX = Vector3.Angle(m.MultiplyVector(Vector3.forward), m.MultiplyVector(Vector3.right));
		}
	}

	// Token: 0x04002007 RID: 8199
	public Transform a;

	// Token: 0x04002008 RID: 8200
	public Transform b;

	// Token: 0x04002009 RID: 8201
	public float t;

	// Token: 0x0400200A RID: 8202
	public float angleXY;

	// Token: 0x0400200B RID: 8203
	public float angleYZ;

	// Token: 0x0400200C RID: 8204
	public float angleZX;

	// Token: 0x0400200D RID: 8205
	public bool cap;

	// Token: 0x0400200E RID: 8206
	public bool inverse0;

	// Token: 0x0400200F RID: 8207
	public bool transpose;

	// Token: 0x04002010 RID: 8208
	public bool inverse1;

	// Token: 0x04002011 RID: 8209
	[SerializeField]
	private global::TransformLerpTest.SlerpMode mode;

	// Token: 0x02000750 RID: 1872
	private enum SlerpMode
	{
		// Token: 0x04002013 RID: 8211
		TransformSlerp,
		// Token: 0x04002014 RID: 8212
		TransformLerp,
		// Token: 0x04002015 RID: 8213
		WorldToCameraSlerp,
		// Token: 0x04002016 RID: 8214
		WorldToCameraLerp,
		// Token: 0x04002017 RID: 8215
		CameraToWorldSlerp,
		// Token: 0x04002018 RID: 8216
		CameraToWorldLerp,
		// Token: 0x04002019 RID: 8217
		WorldToCameraSlerp2
	}
}
