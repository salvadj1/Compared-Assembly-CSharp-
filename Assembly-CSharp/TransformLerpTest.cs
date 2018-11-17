using System;
using UnityEngine;

// Token: 0x0200068A RID: 1674
public class TransformLerpTest : MonoBehaviour
{
	// Token: 0x060039FD RID: 14845 RVA: 0x000D76D0 File Offset: 0x000D58D0
	private Matrix4x4 Interp(float t, Matrix4x4 a, Matrix4x4 b)
	{
		Matrix4x4 result;
		switch (this.mode)
		{
		default:
			result = TransitionFunctions.Slerp(t, a, b);
			break;
		case TransformLerpTest.SlerpMode.TransformLerp:
		case TransformLerpTest.SlerpMode.WorldToCameraLerp:
		case TransformLerpTest.SlerpMode.CameraToWorldLerp:
			result = TransitionFunctions.Linear(t, a, b);
			break;
		case TransformLerpTest.SlerpMode.WorldToCameraSlerp2:
			result = TransitionFunctions.SlerpWorldToCamera(t, a, b);
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

	// Token: 0x17000B3B RID: 2875
	// (get) Token: 0x060039FE RID: 14846 RVA: 0x000D77E8 File Offset: 0x000D59E8
	private bool ready
	{
		get
		{
			switch (this.mode)
			{
			default:
				return this.a && this.b;
			case TransformLerpTest.SlerpMode.WorldToCameraSlerp:
			case TransformLerpTest.SlerpMode.WorldToCameraLerp:
			case TransformLerpTest.SlerpMode.CameraToWorldSlerp:
			case TransformLerpTest.SlerpMode.CameraToWorldLerp:
				return this.a && this.b && this.a.camera && this.b.camera;
			}
		}
	}

	// Token: 0x060039FF RID: 14847 RVA: 0x000D7888 File Offset: 0x000D5A88
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
		case TransformLerpTest.SlerpMode.WorldToCameraSlerp:
		case TransformLerpTest.SlerpMode.WorldToCameraLerp:
		case TransformLerpTest.SlerpMode.WorldToCameraSlerp2:
			return a.camera.worldToCameraMatrix;
		case TransformLerpTest.SlerpMode.CameraToWorldSlerp:
		case TransformLerpTest.SlerpMode.CameraToWorldLerp:
			return a.camera.cameraToWorldMatrix;
		}
	}

	// Token: 0x06003A00 RID: 14848 RVA: 0x000D7908 File Offset: 0x000D5B08
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

	// Token: 0x06003A01 RID: 14849 RVA: 0x000D79CC File Offset: 0x000D5BCC
	private void OnDrawGizmos()
	{
		if (this.ready)
		{
			Matrix4x4 matrix = this.GetMatrix(this.a);
			Matrix4x4 matrix2 = this.GetMatrix(this.b);
			float num = (!this.cap) ? this.t : Mathf.Clamp01(this.t);
			Matrix4x4 m = this.Interp(0f, matrix, matrix2);
			TransformLerpTest.DrawAxes(m, 0.5f);
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
			TransformLerpTest.DrawAxes(m, 0.5f);
			m = this.Interp(num, matrix, matrix2);
			TransformLerpTest.DrawAxes(m, 1f);
			this.angleXY = Vector3.Angle(m.MultiplyVector(Vector3.right), m.MultiplyVector(Vector3.up));
			this.angleYZ = Vector3.Angle(m.MultiplyVector(Vector3.up), m.MultiplyVector(Vector3.forward));
			this.angleZX = Vector3.Angle(m.MultiplyVector(Vector3.forward), m.MultiplyVector(Vector3.right));
		}
	}

	// Token: 0x04001E0F RID: 7695
	public Transform a;

	// Token: 0x04001E10 RID: 7696
	public Transform b;

	// Token: 0x04001E11 RID: 7697
	public float t;

	// Token: 0x04001E12 RID: 7698
	public float angleXY;

	// Token: 0x04001E13 RID: 7699
	public float angleYZ;

	// Token: 0x04001E14 RID: 7700
	public float angleZX;

	// Token: 0x04001E15 RID: 7701
	public bool cap;

	// Token: 0x04001E16 RID: 7702
	public bool inverse0;

	// Token: 0x04001E17 RID: 7703
	public bool transpose;

	// Token: 0x04001E18 RID: 7704
	public bool inverse1;

	// Token: 0x04001E19 RID: 7705
	[SerializeField]
	private TransformLerpTest.SlerpMode mode;

	// Token: 0x0200068B RID: 1675
	private enum SlerpMode
	{
		// Token: 0x04001E1B RID: 7707
		TransformSlerp,
		// Token: 0x04001E1C RID: 7708
		TransformLerp,
		// Token: 0x04001E1D RID: 7709
		WorldToCameraSlerp,
		// Token: 0x04001E1E RID: 7710
		WorldToCameraLerp,
		// Token: 0x04001E1F RID: 7711
		CameraToWorldSlerp,
		// Token: 0x04001E20 RID: 7712
		CameraToWorldLerp,
		// Token: 0x04001E21 RID: 7713
		WorldToCameraSlerp2
	}
}
