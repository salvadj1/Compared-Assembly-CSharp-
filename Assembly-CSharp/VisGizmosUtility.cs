using System;
using UnityEngine;

// Token: 0x0200039C RID: 924
public static class VisGizmosUtility
{
	// Token: 0x060022FF RID: 8959 RVA: 0x00086B60 File Offset: 0x00084D60
	static VisGizmosUtility()
	{
		VisGizmosUtility.circleVerts = new Vector3[31];
		for (int i = 0; i < 31; i++)
		{
			float num = 0.196349546f * (float)i;
			VisGizmosUtility.circleVerts[i].x = Mathf.Cos(num);
			VisGizmosUtility.circleVerts[i].y = Mathf.Sin(num);
		}
	}

	// Token: 0x06002300 RID: 8960 RVA: 0x00086C4C File Offset: 0x00084E4C
	public static void PushMatrix()
	{
		if (VisGizmosUtility.stackPos == VisGizmosUtility.matStack.Length)
		{
			Array.Resize<Matrix4x4>(ref VisGizmosUtility.matStack, VisGizmosUtility.stackPos + 8);
		}
		VisGizmosUtility.matStack[VisGizmosUtility.stackPos++] = Gizmos.matrix;
	}

	// Token: 0x06002301 RID: 8961 RVA: 0x00086C9C File Offset: 0x00084E9C
	public static void PushMatrix(Matrix4x4 mat)
	{
		VisGizmosUtility.PushMatrix();
		Gizmos.matrix = mat;
	}

	// Token: 0x06002302 RID: 8962 RVA: 0x00086CAC File Offset: 0x00084EAC
	public static void PushMatrixMul(Matrix4x4 mat)
	{
		VisGizmosUtility.PushMatrix();
		Gizmos.matrix = mat * VisGizmosUtility.matStack[VisGizmosUtility.stackPos - 1];
	}

	// Token: 0x06002303 RID: 8963 RVA: 0x00086CE0 File Offset: 0x00084EE0
	public static void PushMatrixMul(Matrix4x4 mat, out Matrix4x4 res)
	{
		VisGizmosUtility.PushMatrix();
		Gizmos.matrix = (res = mat * VisGizmosUtility.matStack[VisGizmosUtility.stackPos - 1]);
	}

	// Token: 0x06002304 RID: 8964 RVA: 0x00086D1C File Offset: 0x00084F1C
	public static void PopMatrix()
	{
		Gizmos.matrix = VisGizmosUtility.matStack[--VisGizmosUtility.stackPos];
	}

	// Token: 0x06002305 RID: 8965 RVA: 0x00086D40 File Offset: 0x00084F40
	public static void PopMatrix(out Matrix4x4 mat)
	{
		mat = VisGizmosUtility.matStack[--VisGizmosUtility.stackPos];
		Gizmos.matrix = mat;
	}

	// Token: 0x06002306 RID: 8966 RVA: 0x00086D70 File Offset: 0x00084F70
	public static void ResetMatrixStack()
	{
		VisGizmosUtility.stackPos = 0;
	}

	// Token: 0x06002307 RID: 8967 RVA: 0x00086D78 File Offset: 0x00084F78
	public static void DrawFlatCircle()
	{
		int num = 30;
		int num2 = 0;
		do
		{
			Gizmos.DrawLine(VisGizmosUtility.circleVerts[num], VisGizmosUtility.circleVerts[num2]);
			num = num2++;
		}
		while (num2 < VisGizmosUtility.circleVerts.Length);
	}

	// Token: 0x06002308 RID: 8968 RVA: 0x00086DC4 File Offset: 0x00084FC4
	public static void DrawFlatCircle(float radius)
	{
		VisGizmosUtility.PushMatrixMul(Matrix4x4.Scale(Vector3.one * radius));
		VisGizmosUtility.DrawFlatCircle();
		VisGizmosUtility.PopMatrix();
	}

	// Token: 0x06002309 RID: 8969 RVA: 0x00086DE8 File Offset: 0x00084FE8
	public static void DrawFlatCapEnd()
	{
		int num = 30;
		int num2 = 0;
		do
		{
			Gizmos.DrawLine(VisGizmosUtility.circleVerts[num], VisGizmosUtility.circleVerts[num2]);
			num = num2++;
		}
		while (num2 < 16);
	}

	// Token: 0x0600230A RID: 8970 RVA: 0x00086E30 File Offset: 0x00085030
	public static void DrawFlatCapStart()
	{
		int num = 0;
		int num2 = 30;
		do
		{
			Gizmos.DrawLine(VisGizmosUtility.circleVerts[num], VisGizmosUtility.circleVerts[num2]);
			num = num2--;
		}
		while (num2 >= 16);
	}

	// Token: 0x0600230B RID: 8971 RVA: 0x00086E78 File Offset: 0x00085078
	public static void DrawFlatCapsule(float lengthOverRadius)
	{
		VisGizmosUtility.DrawFlatCapStart();
		Gizmos.DrawLine(VisGizmosUtility.circleVerts[16], VisGizmosUtility.circleVerts[16] + new Vector3(lengthOverRadius, 0f));
		VisGizmosUtility.PushMatrix();
		Gizmos.matrix *= Matrix4x4.TRS(new Vector3(lengthOverRadius, 0f, 0f), Quaternion.identity, Vector3.one);
		VisGizmosUtility.DrawFlatCapEnd();
		Gizmos.DrawLine(VisGizmosUtility.circleVerts[0], VisGizmosUtility.circleVerts[0] - new Vector3(lengthOverRadius, 0f));
		VisGizmosUtility.PopMatrix();
	}

	// Token: 0x0600230C RID: 8972 RVA: 0x00086F34 File Offset: 0x00085134
	public static void DrawCircle(Vector3 origin, Vector3 axis, float radius)
	{
		VisGizmosUtility.PushMatrix();
		Gizmos.matrix = Matrix4x4.TRS(origin, Quaternion.LookRotation(axis), new Vector3(radius, radius, 1f)) * Gizmos.matrix;
		VisGizmosUtility.DrawFlatCircle();
		VisGizmosUtility.PopMatrix();
	}

	// Token: 0x0600230D RID: 8973 RVA: 0x00086F78 File Offset: 0x00085178
	private static void MagicForward(Vector3 a, Vector3 b, out Vector3 up, out Vector3 forward)
	{
		Vector3 vector = a - b;
		vector.Normalize();
		if (vector.y * vector.y > 0.8f)
		{
			up = Vector3.Cross(vector, Vector3.forward);
			forward = Vector3.Cross(vector, up);
		}
		else
		{
			forward = Vector3.Cross(vector, Vector3.up);
			up = Vector3.Cross(vector, forward);
		}
		up.Normalize();
		forward.Normalize();
	}

	// Token: 0x0600230E RID: 8974 RVA: 0x00087004 File Offset: 0x00085204
	private static Quaternion MagicFlat(Vector3 a, Vector3 b)
	{
		Vector3 vector;
		Vector3 vector2;
		VisGizmosUtility.MagicForward(a, b, out vector, out vector2);
		return Quaternion.LookRotation(vector2, vector);
	}

	// Token: 0x0600230F RID: 8975 RVA: 0x00087024 File Offset: 0x00085224
	public static void DrawCapsule(Vector3 capA, Vector3 capB, float radius)
	{
		if (radius == 0f)
		{
			Gizmos.DrawLine(capA, capB);
		}
		else
		{
			float num = Vector3.Distance(capA, capB);
			if (num == 0f)
			{
				VisGizmosUtility.DrawSphere(capA, radius);
			}
			else
			{
				VisGizmosUtility.PushMatrix();
				Matrix4x4 matrix4x = Matrix4x4.TRS(capA, VisGizmosUtility.MagicFlat(capA, capB), new Vector3(radius, radius, radius)) * Gizmos.matrix;
				Gizmos.matrix = matrix4x;
				float lengthOverRadius = num / radius;
				VisGizmosUtility.DrawFlatCapsule(lengthOverRadius);
				Gizmos.matrix = VisGizmosUtility.ninetyZ * matrix4x;
				VisGizmosUtility.DrawFlatCapsule(lengthOverRadius);
				Gizmos.matrix = VisGizmosUtility.ninetyY * Gizmos.matrix;
				VisGizmosUtility.DrawFlatCircle();
				Gizmos.matrix = VisGizmosUtility.ninetyY * matrix4x;
				VisGizmosUtility.DrawFlatCircle();
				VisGizmosUtility.PopMatrix();
			}
		}
	}

	// Token: 0x06002310 RID: 8976 RVA: 0x000870E8 File Offset: 0x000852E8
	public static void DrawSphere(Vector3 center, float radius, Quaternion rotation)
	{
		VisGizmosUtility.PushMatrix();
		Matrix4x4 matrix4x = Matrix4x4.TRS(center, rotation, new Vector3(radius, radius, radius)) * Gizmos.matrix;
		Gizmos.matrix = matrix4x;
		VisGizmosUtility.DrawFlatCircle();
		Gizmos.matrix = VisGizmosUtility.ninetyX * matrix4x;
		VisGizmosUtility.DrawFlatCircle();
		Gizmos.matrix = VisGizmosUtility.ninetyY * matrix4x;
		VisGizmosUtility.DrawFlatCircle();
		VisGizmosUtility.PopMatrix();
	}

	// Token: 0x06002311 RID: 8977 RVA: 0x00087150 File Offset: 0x00085350
	public static void DrawSphere(Vector3 center, float radius, Vector3 forward)
	{
		VisGizmosUtility.DrawSphere(center, radius, Quaternion.LookRotation(forward));
	}

	// Token: 0x06002312 RID: 8978 RVA: 0x00087160 File Offset: 0x00085360
	public static void DrawSphere(Vector3 center, float radius)
	{
		VisGizmosUtility.DrawSphere(center, radius, Quaternion.identity);
	}

	// Token: 0x06002313 RID: 8979 RVA: 0x00087170 File Offset: 0x00085370
	public static void DrawCapsule(Vector3 center, float length, float radius, Vector3 heading)
	{
		length = Mathf.Max(length - radius * 2f, 0f);
		if (length == 0f)
		{
			VisGizmosUtility.DrawSphere(center, radius, heading);
		}
		heading.Normalize();
		length /= 2f;
		VisGizmosUtility.DrawCapsule(center - heading * length, center + heading * length, radius);
	}

	// Token: 0x06002314 RID: 8980 RVA: 0x000871D8 File Offset: 0x000853D8
	public static void DrawAngle(Vector3 origin, Vector3 heading, Vector3 axis, float angle, float radius)
	{
		VisGizmosUtility.PushMatrix();
		if (angle < 0f)
		{
			axis = -axis;
			angle = -angle;
		}
		Vector3 vector = Vector3.Cross(axis, heading);
		Gizmos.matrix = Matrix4x4.TRS(origin, Quaternion.LookRotation(axis, vector), new Vector3(radius, radius, 1f)) * Gizmos.matrix;
		Vector3 vector2 = Vector3.zero;
		if (angle == 0f)
		{
			Gizmos.DrawLine(Vector3.zero, new Vector3(0f, 1f, 0f));
		}
		else if (angle < 360f)
		{
			int num = 0;
			float num2 = 0f;
			Vector3 vector3;
			do
			{
				vector3 = VisGizmosUtility.circleVerts[num++];
				Gizmos.DrawLine(vector2, vector3);
				vector2 = vector3;
				num2 += 11.25f;
			}
			while (num2 < angle);
			if (num2 != angle)
			{
				Vector3 vector4 = vector2;
				vector3..ctor(Mathf.Cos(angle * 0.0174532924f), Mathf.Sin(angle * 0.0174532924f));
				Gizmos.DrawLine(vector4, vector3);
				vector2 = vector3;
			}
			Gizmos.DrawLine(vector2, Vector3.zero);
		}
		VisGizmosUtility.PopMatrix();
	}

	// Token: 0x06002315 RID: 8981 RVA: 0x000872EC File Offset: 0x000854EC
	public static void DrawDotCone(Vector3 position, Vector3 forward, float length, float arc)
	{
		VisGizmosUtility.DrawDotCone(position, forward, length, arc, 0f);
	}

	// Token: 0x06002316 RID: 8982 RVA: 0x000872FC File Offset: 0x000854FC
	public static void DrawDotCone(Vector3 position, Vector3 forward, float length, float arc, float back)
	{
		if (arc == 1f)
		{
			Gizmos.DrawLine(position, position + forward * length);
		}
		else
		{
			float num = Mathf.Ceil(length);
			if (num != 0f)
			{
				float num2 = Mathf.Acos(arc);
				int num3 = Mathf.Abs((int)num);
				float num4 = length / num;
				float num5 = num4 * num2;
				int i;
				float num6;
				float num7;
				if (back == 0f)
				{
					num = num4;
					i = 1;
					num6 = num5;
					num7 = 0f;
				}
				else
				{
					num = 0f;
					i = 0;
					num6 = num2 * back;
					num7 = num6;
				}
				Matrix4x4 matrix4x;
				VisGizmosUtility.PushMatrixMul(Matrix4x4.TRS(position, Quaternion.LookRotation(forward), Vector3.one), out matrix4x);
				Vector3 vector;
				vector..ctor(num7, 0f, 0f);
				Vector3 vector2;
				vector2..ctor(num7 + num2 * length, 0f, length);
				Gizmos.DrawLine(vector, vector2);
				vector.x = -vector.x;
				vector2.x = -vector2.x;
				Gizmos.DrawLine(vector, vector2);
				vector.y = vector.x;
				vector.x = 0f;
				vector2.y = vector2.x;
				vector2.x = 0f;
				Gizmos.DrawLine(vector, vector2);
				vector.y = -vector.y;
				vector2.y = -vector2.y;
				Gizmos.DrawLine(vector, vector2);
				while (i <= num3)
				{
					Gizmos.matrix = matrix4x * Matrix4x4.TRS(new Vector3(0f, 0f, num), Quaternion.identity, new Vector3(num6, num6, 1f));
					VisGizmosUtility.DrawFlatCircle();
					i++;
					num += num4;
					num6 += num5;
				}
				VisGizmosUtility.PopMatrix();
			}
		}
	}

	// Token: 0x06002317 RID: 8983 RVA: 0x000874B8 File Offset: 0x000856B8
	public static void DrawDotArc(Vector3 position, Transform transform, float length, float arc, float back)
	{
		Vector3 forward = transform.forward;
		VisGizmosUtility.DrawDotCone(position, forward, arc * length, arc, back);
		float num = Mathf.Acos(arc) * 57.29578f;
		Vector3 up = transform.up;
		Vector3 right = transform.right;
		VisGizmosUtility.DrawAngle(position, forward, up, num, length);
		VisGizmosUtility.DrawAngle(position, forward, up, -num, length);
		VisGizmosUtility.DrawAngle(position, forward, right, num, length);
		VisGizmosUtility.DrawAngle(position, forward, right, -num, length);
	}

	// Token: 0x040010A0 RID: 4256
	private const int numCircleVerts = 32;

	// Token: 0x040010A1 RID: 4257
	private const int lengthCircleVerts = 31;

	// Token: 0x040010A2 RID: 4258
	private const float degreePerCircleVert = 11.25f;

	// Token: 0x040010A3 RID: 4259
	private const float radPerCircleVert = 0.196349546f;

	// Token: 0x040010A4 RID: 4260
	private const int halveCircleIndex = 16;

	// Token: 0x040010A5 RID: 4261
	private static Matrix4x4[] matStack = new Matrix4x4[8];

	// Token: 0x040010A6 RID: 4262
	private static int stackPos = 0;

	// Token: 0x040010A7 RID: 4263
	private static Vector3[] circleVerts;

	// Token: 0x040010A8 RID: 4264
	private static readonly Matrix4x4 ninetyX = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(90f, 0f, 0f), Vector3.one);

	// Token: 0x040010A9 RID: 4265
	private static readonly Matrix4x4 ninetyY = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 90f, 0f), Vector3.one);

	// Token: 0x040010AA RID: 4266
	private static readonly Matrix4x4 ninetyZ = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, 90f), Vector3.one);
}
