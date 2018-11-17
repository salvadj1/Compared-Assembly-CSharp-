using System;
using UnityEngine;

// Token: 0x02000449 RID: 1097
public static class VisGizmosUtility
{
	// Token: 0x06002661 RID: 9825 RVA: 0x0008BF5C File Offset: 0x0008A15C
	static VisGizmosUtility()
	{
		global::VisGizmosUtility.circleVerts = new Vector3[31];
		for (int i = 0; i < 31; i++)
		{
			float num = 0.196349546f * (float)i;
			global::VisGizmosUtility.circleVerts[i].x = Mathf.Cos(num);
			global::VisGizmosUtility.circleVerts[i].y = Mathf.Sin(num);
		}
	}

	// Token: 0x06002662 RID: 9826 RVA: 0x0008C048 File Offset: 0x0008A248
	public static void PushMatrix()
	{
		if (global::VisGizmosUtility.stackPos == global::VisGizmosUtility.matStack.Length)
		{
			Array.Resize<Matrix4x4>(ref global::VisGizmosUtility.matStack, global::VisGizmosUtility.stackPos + 8);
		}
		global::VisGizmosUtility.matStack[global::VisGizmosUtility.stackPos++] = Gizmos.matrix;
	}

	// Token: 0x06002663 RID: 9827 RVA: 0x0008C098 File Offset: 0x0008A298
	public static void PushMatrix(Matrix4x4 mat)
	{
		global::VisGizmosUtility.PushMatrix();
		Gizmos.matrix = mat;
	}

	// Token: 0x06002664 RID: 9828 RVA: 0x0008C0A8 File Offset: 0x0008A2A8
	public static void PushMatrixMul(Matrix4x4 mat)
	{
		global::VisGizmosUtility.PushMatrix();
		Gizmos.matrix = mat * global::VisGizmosUtility.matStack[global::VisGizmosUtility.stackPos - 1];
	}

	// Token: 0x06002665 RID: 9829 RVA: 0x0008C0DC File Offset: 0x0008A2DC
	public static void PushMatrixMul(Matrix4x4 mat, out Matrix4x4 res)
	{
		global::VisGizmosUtility.PushMatrix();
		Gizmos.matrix = (res = mat * global::VisGizmosUtility.matStack[global::VisGizmosUtility.stackPos - 1]);
	}

	// Token: 0x06002666 RID: 9830 RVA: 0x0008C118 File Offset: 0x0008A318
	public static void PopMatrix()
	{
		Gizmos.matrix = global::VisGizmosUtility.matStack[--global::VisGizmosUtility.stackPos];
	}

	// Token: 0x06002667 RID: 9831 RVA: 0x0008C13C File Offset: 0x0008A33C
	public static void PopMatrix(out Matrix4x4 mat)
	{
		mat = global::VisGizmosUtility.matStack[--global::VisGizmosUtility.stackPos];
		Gizmos.matrix = mat;
	}

	// Token: 0x06002668 RID: 9832 RVA: 0x0008C16C File Offset: 0x0008A36C
	public static void ResetMatrixStack()
	{
		global::VisGizmosUtility.stackPos = 0;
	}

	// Token: 0x06002669 RID: 9833 RVA: 0x0008C174 File Offset: 0x0008A374
	public static void DrawFlatCircle()
	{
		int num = 30;
		int num2 = 0;
		do
		{
			Gizmos.DrawLine(global::VisGizmosUtility.circleVerts[num], global::VisGizmosUtility.circleVerts[num2]);
			num = num2++;
		}
		while (num2 < global::VisGizmosUtility.circleVerts.Length);
	}

	// Token: 0x0600266A RID: 9834 RVA: 0x0008C1C0 File Offset: 0x0008A3C0
	public static void DrawFlatCircle(float radius)
	{
		global::VisGizmosUtility.PushMatrixMul(Matrix4x4.Scale(Vector3.one * radius));
		global::VisGizmosUtility.DrawFlatCircle();
		global::VisGizmosUtility.PopMatrix();
	}

	// Token: 0x0600266B RID: 9835 RVA: 0x0008C1E4 File Offset: 0x0008A3E4
	public static void DrawFlatCapEnd()
	{
		int num = 30;
		int num2 = 0;
		do
		{
			Gizmos.DrawLine(global::VisGizmosUtility.circleVerts[num], global::VisGizmosUtility.circleVerts[num2]);
			num = num2++;
		}
		while (num2 < 16);
	}

	// Token: 0x0600266C RID: 9836 RVA: 0x0008C22C File Offset: 0x0008A42C
	public static void DrawFlatCapStart()
	{
		int num = 0;
		int num2 = 30;
		do
		{
			Gizmos.DrawLine(global::VisGizmosUtility.circleVerts[num], global::VisGizmosUtility.circleVerts[num2]);
			num = num2--;
		}
		while (num2 >= 16);
	}

	// Token: 0x0600266D RID: 9837 RVA: 0x0008C274 File Offset: 0x0008A474
	public static void DrawFlatCapsule(float lengthOverRadius)
	{
		global::VisGizmosUtility.DrawFlatCapStart();
		Gizmos.DrawLine(global::VisGizmosUtility.circleVerts[16], global::VisGizmosUtility.circleVerts[16] + new Vector3(lengthOverRadius, 0f));
		global::VisGizmosUtility.PushMatrix();
		Gizmos.matrix *= Matrix4x4.TRS(new Vector3(lengthOverRadius, 0f, 0f), Quaternion.identity, Vector3.one);
		global::VisGizmosUtility.DrawFlatCapEnd();
		Gizmos.DrawLine(global::VisGizmosUtility.circleVerts[0], global::VisGizmosUtility.circleVerts[0] - new Vector3(lengthOverRadius, 0f));
		global::VisGizmosUtility.PopMatrix();
	}

	// Token: 0x0600266E RID: 9838 RVA: 0x0008C330 File Offset: 0x0008A530
	public static void DrawCircle(Vector3 origin, Vector3 axis, float radius)
	{
		global::VisGizmosUtility.PushMatrix();
		Gizmos.matrix = Matrix4x4.TRS(origin, Quaternion.LookRotation(axis), new Vector3(radius, radius, 1f)) * Gizmos.matrix;
		global::VisGizmosUtility.DrawFlatCircle();
		global::VisGizmosUtility.PopMatrix();
	}

	// Token: 0x0600266F RID: 9839 RVA: 0x0008C374 File Offset: 0x0008A574
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

	// Token: 0x06002670 RID: 9840 RVA: 0x0008C400 File Offset: 0x0008A600
	private static Quaternion MagicFlat(Vector3 a, Vector3 b)
	{
		Vector3 vector;
		Vector3 vector2;
		global::VisGizmosUtility.MagicForward(a, b, out vector, out vector2);
		return Quaternion.LookRotation(vector2, vector);
	}

	// Token: 0x06002671 RID: 9841 RVA: 0x0008C420 File Offset: 0x0008A620
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
				global::VisGizmosUtility.DrawSphere(capA, radius);
			}
			else
			{
				global::VisGizmosUtility.PushMatrix();
				Matrix4x4 matrix4x = Matrix4x4.TRS(capA, global::VisGizmosUtility.MagicFlat(capA, capB), new Vector3(radius, radius, radius)) * Gizmos.matrix;
				Gizmos.matrix = matrix4x;
				float lengthOverRadius = num / radius;
				global::VisGizmosUtility.DrawFlatCapsule(lengthOverRadius);
				Gizmos.matrix = global::VisGizmosUtility.ninetyZ * matrix4x;
				global::VisGizmosUtility.DrawFlatCapsule(lengthOverRadius);
				Gizmos.matrix = global::VisGizmosUtility.ninetyY * Gizmos.matrix;
				global::VisGizmosUtility.DrawFlatCircle();
				Gizmos.matrix = global::VisGizmosUtility.ninetyY * matrix4x;
				global::VisGizmosUtility.DrawFlatCircle();
				global::VisGizmosUtility.PopMatrix();
			}
		}
	}

	// Token: 0x06002672 RID: 9842 RVA: 0x0008C4E4 File Offset: 0x0008A6E4
	public static void DrawSphere(Vector3 center, float radius, Quaternion rotation)
	{
		global::VisGizmosUtility.PushMatrix();
		Matrix4x4 matrix4x = Matrix4x4.TRS(center, rotation, new Vector3(radius, radius, radius)) * Gizmos.matrix;
		Gizmos.matrix = matrix4x;
		global::VisGizmosUtility.DrawFlatCircle();
		Gizmos.matrix = global::VisGizmosUtility.ninetyX * matrix4x;
		global::VisGizmosUtility.DrawFlatCircle();
		Gizmos.matrix = global::VisGizmosUtility.ninetyY * matrix4x;
		global::VisGizmosUtility.DrawFlatCircle();
		global::VisGizmosUtility.PopMatrix();
	}

	// Token: 0x06002673 RID: 9843 RVA: 0x0008C54C File Offset: 0x0008A74C
	public static void DrawSphere(Vector3 center, float radius, Vector3 forward)
	{
		global::VisGizmosUtility.DrawSphere(center, radius, Quaternion.LookRotation(forward));
	}

	// Token: 0x06002674 RID: 9844 RVA: 0x0008C55C File Offset: 0x0008A75C
	public static void DrawSphere(Vector3 center, float radius)
	{
		global::VisGizmosUtility.DrawSphere(center, radius, Quaternion.identity);
	}

	// Token: 0x06002675 RID: 9845 RVA: 0x0008C56C File Offset: 0x0008A76C
	public static void DrawCapsule(Vector3 center, float length, float radius, Vector3 heading)
	{
		length = Mathf.Max(length - radius * 2f, 0f);
		if (length == 0f)
		{
			global::VisGizmosUtility.DrawSphere(center, radius, heading);
		}
		heading.Normalize();
		length /= 2f;
		global::VisGizmosUtility.DrawCapsule(center - heading * length, center + heading * length, radius);
	}

	// Token: 0x06002676 RID: 9846 RVA: 0x0008C5D4 File Offset: 0x0008A7D4
	public static void DrawAngle(Vector3 origin, Vector3 heading, Vector3 axis, float angle, float radius)
	{
		global::VisGizmosUtility.PushMatrix();
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
				vector3 = global::VisGizmosUtility.circleVerts[num++];
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
		global::VisGizmosUtility.PopMatrix();
	}

	// Token: 0x06002677 RID: 9847 RVA: 0x0008C6E8 File Offset: 0x0008A8E8
	public static void DrawDotCone(Vector3 position, Vector3 forward, float length, float arc)
	{
		global::VisGizmosUtility.DrawDotCone(position, forward, length, arc, 0f);
	}

	// Token: 0x06002678 RID: 9848 RVA: 0x0008C6F8 File Offset: 0x0008A8F8
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
				global::VisGizmosUtility.PushMatrixMul(Matrix4x4.TRS(position, Quaternion.LookRotation(forward), Vector3.one), out matrix4x);
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
					global::VisGizmosUtility.DrawFlatCircle();
					i++;
					num += num4;
					num6 += num5;
				}
				global::VisGizmosUtility.PopMatrix();
			}
		}
	}

	// Token: 0x06002679 RID: 9849 RVA: 0x0008C8B4 File Offset: 0x0008AAB4
	public static void DrawDotArc(Vector3 position, Transform transform, float length, float arc, float back)
	{
		Vector3 forward = transform.forward;
		global::VisGizmosUtility.DrawDotCone(position, forward, arc * length, arc, back);
		float num = Mathf.Acos(arc) * 57.29578f;
		Vector3 up = transform.up;
		Vector3 right = transform.right;
		global::VisGizmosUtility.DrawAngle(position, forward, up, num, length);
		global::VisGizmosUtility.DrawAngle(position, forward, up, -num, length);
		global::VisGizmosUtility.DrawAngle(position, forward, right, num, length);
		global::VisGizmosUtility.DrawAngle(position, forward, right, -num, length);
	}

	// Token: 0x04001206 RID: 4614
	private const int numCircleVerts = 32;

	// Token: 0x04001207 RID: 4615
	private const int lengthCircleVerts = 31;

	// Token: 0x04001208 RID: 4616
	private const float degreePerCircleVert = 11.25f;

	// Token: 0x04001209 RID: 4617
	private const float radPerCircleVert = 0.196349546f;

	// Token: 0x0400120A RID: 4618
	private const int halveCircleIndex = 16;

	// Token: 0x0400120B RID: 4619
	private static Matrix4x4[] matStack = new Matrix4x4[8];

	// Token: 0x0400120C RID: 4620
	private static int stackPos = 0;

	// Token: 0x0400120D RID: 4621
	private static Vector3[] circleVerts;

	// Token: 0x0400120E RID: 4622
	private static readonly Matrix4x4 ninetyX = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(90f, 0f, 0f), Vector3.one);

	// Token: 0x0400120F RID: 4623
	private static readonly Matrix4x4 ninetyY = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 90f, 0f), Vector3.one);

	// Token: 0x04001210 RID: 4624
	private static readonly Matrix4x4 ninetyZ = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, 90f), Vector3.one);
}
