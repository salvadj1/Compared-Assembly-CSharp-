using System;
using Facepunch.Precision;
using UnityEngine;

// Token: 0x020003FF RID: 1023
public static class TransitionFunctions
{
	// Token: 0x06002381 RID: 9089 RVA: 0x00083720 File Offset: 0x00081920
	public static Color Linear(float t, Color a, Color b)
	{
		return global::TransitionFunctions.Sum(global::TransitionFunctions.Mul(a, 1f - t), global::TransitionFunctions.Mul(b, t));
	}

	// Token: 0x06002382 RID: 9090 RVA: 0x0008373C File Offset: 0x0008193C
	public static Color Round(float t, Color a, Color b)
	{
		return (t >= 0.5f) ? b : a;
	}

	// Token: 0x06002383 RID: 9091 RVA: 0x00083750 File Offset: 0x00081950
	public static Color Ceil(float t, Color a, Color b)
	{
		return (t <= 0f) ? a : b;
	}

	// Token: 0x06002384 RID: 9092 RVA: 0x00083764 File Offset: 0x00081964
	public static Color Floor(float t, Color a, Color b)
	{
		return (t >= 1f) ? b : a;
	}

	// Token: 0x06002385 RID: 9093 RVA: 0x00083778 File Offset: 0x00081978
	public static Color Spline(float t, Color a, Color b)
	{
		return (t > 0f) ? ((t < 1f) ? global::TransitionFunctions.Linear(global::TransitionFunctions.SimpleSpline(t), a, b) : b) : a;
	}

	// Token: 0x06002386 RID: 9094 RVA: 0x000837AC File Offset: 0x000819AC
	public static Color Evaluate(this global::TransitionFunction f, float t, Color a, Color b)
	{
		switch (f)
		{
		case global::TransitionFunction.Linear:
			return global::TransitionFunctions.Linear(t, a, b);
		case global::TransitionFunction.Round:
			return global::TransitionFunctions.Round(t, a, b);
		case global::TransitionFunction.Floor:
			return global::TransitionFunctions.Floor(t, a, b);
		case global::TransitionFunction.Ceil:
			return global::TransitionFunctions.Ceil(t, a, b);
		case global::TransitionFunction.Spline:
			return global::TransitionFunctions.Spline(t, a, b);
		default:
			throw new ArgumentOutOfRangeException("v", "Attempted use of unrecognized TransitionFunction enum value");
		}
	}

	// Token: 0x06002387 RID: 9095 RVA: 0x00083818 File Offset: 0x00081A18
	public static Color Evaluate(this global::TransitionFunction<Color> v, float t)
	{
		return v.f.Evaluate(t, v.a, v.b);
	}

	// Token: 0x06002388 RID: 9096 RVA: 0x00083838 File Offset: 0x00081A38
	public static Color Mul(Color a, float b)
	{
		Color result;
		result.r = a.r * b;
		result.g = a.g * b;
		result.b = a.b * b;
		result.a = a.a * b;
		return result;
	}

	// Token: 0x06002389 RID: 9097 RVA: 0x00083888 File Offset: 0x00081A88
	public static Color Sum(Color a, Color b)
	{
		Color result;
		result.r = a.r + b.r;
		result.g = a.g + b.g;
		result.b = a.b + b.b;
		result.a = a.a * b.a;
		return result;
	}

	// Token: 0x0600238A RID: 9098 RVA: 0x000838F0 File Offset: 0x00081AF0
	public static Matrix4x4 Linear(float t, Matrix4x4 a, Matrix4x4 b)
	{
		return global::TransitionFunctions.Sum(global::TransitionFunctions.Mul(a, 1f - t), global::TransitionFunctions.Mul(b, t));
	}

	// Token: 0x0600238B RID: 9099 RVA: 0x0008390C File Offset: 0x00081B0C
	public static Matrix4x4 Round(float t, Matrix4x4 a, Matrix4x4 b)
	{
		return (t >= 0.5f) ? b : a;
	}

	// Token: 0x0600238C RID: 9100 RVA: 0x00083920 File Offset: 0x00081B20
	public static Matrix4x4 Ceil(float t, Matrix4x4 a, Matrix4x4 b)
	{
		return (t <= 0f) ? a : b;
	}

	// Token: 0x0600238D RID: 9101 RVA: 0x00083934 File Offset: 0x00081B34
	public static Matrix4x4 Floor(float t, Matrix4x4 a, Matrix4x4 b)
	{
		return (t >= 1f) ? b : a;
	}

	// Token: 0x0600238E RID: 9102 RVA: 0x00083948 File Offset: 0x00081B48
	public static Matrix4x4 Spline(float t, Matrix4x4 a, Matrix4x4 b)
	{
		return (t > 0f) ? ((t < 1f) ? global::TransitionFunctions.Linear(global::TransitionFunctions.SimpleSpline(t), a, b) : b) : a;
	}

	// Token: 0x0600238F RID: 9103 RVA: 0x0008397C File Offset: 0x00081B7C
	public static Matrix4x4 Evaluate(this global::TransitionFunction f, float t, Matrix4x4 a, Matrix4x4 b)
	{
		switch (f)
		{
		case global::TransitionFunction.Linear:
			return global::TransitionFunctions.Linear(t, a, b);
		case global::TransitionFunction.Round:
			return global::TransitionFunctions.Round(t, a, b);
		case global::TransitionFunction.Floor:
			return global::TransitionFunctions.Floor(t, a, b);
		case global::TransitionFunction.Ceil:
			return global::TransitionFunctions.Ceil(t, a, b);
		case global::TransitionFunction.Spline:
			return global::TransitionFunctions.Spline(t, a, b);
		default:
			throw new ArgumentOutOfRangeException("v", "Attempted use of unrecognized TransitionFunction enum value");
		}
	}

	// Token: 0x06002390 RID: 9104 RVA: 0x000839E8 File Offset: 0x00081BE8
	public static Matrix4x4 Evaluate(this global::TransitionFunction<Matrix4x4> v, float t)
	{
		return v.f.Evaluate(t, v.a, v.b);
	}

	// Token: 0x06002391 RID: 9105 RVA: 0x00083A08 File Offset: 0x00081C08
	public static Matrix4x4 Mul(Matrix4x4 a, float b)
	{
		Matrix4x4 result;
		result.m00 = a.m00 * b;
		result.m10 = a.m10 * b;
		result.m20 = a.m20 * b;
		result.m30 = a.m30 * b;
		result.m01 = a.m01 * b;
		result.m11 = a.m11 * b;
		result.m21 = a.m21 * b;
		result.m31 = a.m31 * b;
		result.m02 = a.m02 * b;
		result.m12 = a.m12 * b;
		result.m22 = a.m22 * b;
		result.m32 = a.m32 * b;
		result.m03 = a.m03 * b;
		result.m13 = a.m13 * b;
		result.m23 = a.m23 * b;
		result.m33 = a.m33 * b;
		return result;
	}

	// Token: 0x06002392 RID: 9106 RVA: 0x00083B18 File Offset: 0x00081D18
	public static Matrix4x4 Sum(Matrix4x4 a, Matrix4x4 b)
	{
		Matrix4x4 result;
		result.m00 = a.m00 + b.m00;
		result.m10 = a.m10 + b.m10;
		result.m20 = a.m20 + b.m20;
		result.m30 = a.m30 + b.m30;
		result.m01 = a.m01 + b.m01;
		result.m11 = a.m11 + b.m11;
		result.m21 = a.m21 + b.m21;
		result.m31 = a.m31 + b.m31;
		result.m02 = a.m02 + b.m02;
		result.m12 = a.m12 + b.m12;
		result.m22 = a.m22 + b.m22;
		result.m32 = a.m32 + b.m32;
		result.m03 = a.m03 + b.m03;
		result.m13 = a.m13 + b.m13;
		result.m23 = a.m23 + b.m23;
		result.m33 = a.m33 + b.m33;
		return result;
	}

	// Token: 0x06002393 RID: 9107 RVA: 0x00083C88 File Offset: 0x00081E88
	public static Matrix4x4 Inverse(Matrix4x4 v)
	{
		return Matrix4x4.Inverse(v);
	}

	// Token: 0x06002394 RID: 9108 RVA: 0x00083C90 File Offset: 0x00081E90
	public static Matrix4x4 Transpose(Matrix4x4 v)
	{
		return Matrix4x4.Transpose(v);
	}

	// Token: 0x06002395 RID: 9109 RVA: 0x00083C98 File Offset: 0x00081E98
	private static Vector3 GET_0X(Matrix4x4 a)
	{
		return global::TransitionFunctions.Vect(a.m00, a.m01, a.m02);
	}

	// Token: 0x06002396 RID: 9110 RVA: 0x00083CB4 File Offset: 0x00081EB4
	private static Vector3 GET_X0(Matrix4x4 a)
	{
		return global::TransitionFunctions.Vect(a.m00, a.m10, a.m20);
	}

	// Token: 0x06002397 RID: 9111 RVA: 0x00083CD0 File Offset: 0x00081ED0
	private static void SET_0X(ref Matrix4x4 m, Vector3 v)
	{
		m.m00 = v.x;
		m.m01 = v.y;
		m.m02 = v.z;
	}

	// Token: 0x06002398 RID: 9112 RVA: 0x00083CFC File Offset: 0x00081EFC
	private static void SET_X0(ref Matrix4x4 m, Vector3 v)
	{
		m.m00 = v.x;
		m.m10 = v.y;
		m.m20 = v.z;
	}

	// Token: 0x06002399 RID: 9113 RVA: 0x00083D28 File Offset: 0x00081F28
	private static Vector3 GET_1X(Matrix4x4 a)
	{
		return global::TransitionFunctions.Vect(a.m10, a.m11, a.m12);
	}

	// Token: 0x0600239A RID: 9114 RVA: 0x00083D44 File Offset: 0x00081F44
	private static Vector3 GET_X1(Matrix4x4 a)
	{
		return global::TransitionFunctions.Vect(a.m01, a.m11, a.m21);
	}

	// Token: 0x0600239B RID: 9115 RVA: 0x00083D60 File Offset: 0x00081F60
	private static void SET_1X(ref Matrix4x4 m, Vector3 v)
	{
		m.m10 = v.x;
		m.m11 = v.y;
		m.m12 = v.z;
	}

	// Token: 0x0600239C RID: 9116 RVA: 0x00083D8C File Offset: 0x00081F8C
	private static void SET_X1(ref Matrix4x4 m, Vector3 v)
	{
		m.m01 = v.x;
		m.m11 = v.y;
		m.m21 = v.z;
	}

	// Token: 0x0600239D RID: 9117 RVA: 0x00083DB8 File Offset: 0x00081FB8
	private static Vector3 GET_2X(Matrix4x4 a)
	{
		return global::TransitionFunctions.Vect(a.m20, a.m21, a.m22);
	}

	// Token: 0x0600239E RID: 9118 RVA: 0x00083DD4 File Offset: 0x00081FD4
	private static Vector3 GET_X2(Matrix4x4 a)
	{
		return global::TransitionFunctions.Vect(a.m02, a.m12, a.m22);
	}

	// Token: 0x0600239F RID: 9119 RVA: 0x00083DF0 File Offset: 0x00081FF0
	private static void SET_2X(ref Matrix4x4 m, Vector3 v)
	{
		m.m20 = v.x;
		m.m21 = v.y;
		m.m22 = v.z;
	}

	// Token: 0x060023A0 RID: 9120 RVA: 0x00083E1C File Offset: 0x0008201C
	private static void SET_X2(ref Matrix4x4 m, Vector3 v)
	{
		m.m02 = v.x;
		m.m12 = v.y;
		m.m22 = v.z;
	}

	// Token: 0x060023A1 RID: 9121 RVA: 0x00083E48 File Offset: 0x00082048
	private static Vector3 GET_3X(Matrix4x4 a)
	{
		return global::TransitionFunctions.Vect(a.m30, a.m31, a.m32);
	}

	// Token: 0x060023A2 RID: 9122 RVA: 0x00083E64 File Offset: 0x00082064
	private static Vector3 GET_X3(Matrix4x4 a)
	{
		return global::TransitionFunctions.Vect(a.m03, a.m13, a.m23);
	}

	// Token: 0x060023A3 RID: 9123 RVA: 0x00083E80 File Offset: 0x00082080
	private static void SET_3X(ref Matrix4x4 m, Vector3 v)
	{
		m.m30 = v.x;
		m.m31 = v.y;
		m.m32 = v.z;
	}

	// Token: 0x060023A4 RID: 9124 RVA: 0x00083EAC File Offset: 0x000820AC
	private static void SET_X3(ref Matrix4x4 m, Vector3 v)
	{
		m.m03 = v.x;
		m.m13 = v.y;
		m.m23 = v.z;
	}

	// Token: 0x060023A5 RID: 9125 RVA: 0x00083ED8 File Offset: 0x000820D8
	private static Vector3 DIR_X(Matrix4x4 a)
	{
		return global::TransitionFunctions.GET_X0(a);
	}

	// Token: 0x060023A6 RID: 9126 RVA: 0x00083EE0 File Offset: 0x000820E0
	private static void DIR_X(ref Matrix4x4 a, Vector3 v)
	{
		global::TransitionFunctions.SET_X0(ref a, v);
	}

	// Token: 0x060023A7 RID: 9127 RVA: 0x00083EEC File Offset: 0x000820EC
	private static Vector3 DIR_Y(Matrix4x4 a)
	{
		return global::TransitionFunctions.GET_X1(a);
	}

	// Token: 0x060023A8 RID: 9128 RVA: 0x00083EF4 File Offset: 0x000820F4
	private static void DIR_Y(ref Matrix4x4 a, Vector3 v)
	{
		global::TransitionFunctions.SET_X1(ref a, v);
	}

	// Token: 0x060023A9 RID: 9129 RVA: 0x00083F00 File Offset: 0x00082100
	private static Vector3 DIR_Z(Matrix4x4 a)
	{
		return global::TransitionFunctions.GET_X2(a);
	}

	// Token: 0x060023AA RID: 9130 RVA: 0x00083F08 File Offset: 0x00082108
	private static void DIR_Z(ref Matrix4x4 a, Vector3 v)
	{
		global::TransitionFunctions.SET_X2(ref a, v);
	}

	// Token: 0x060023AB RID: 9131 RVA: 0x00083F14 File Offset: 0x00082114
	private static Vector3 TRANS(Matrix4x4 a)
	{
		return global::TransitionFunctions.GET_X3(a);
	}

	// Token: 0x060023AC RID: 9132 RVA: 0x00083F1C File Offset: 0x0008211C
	private static void TRANS(ref Matrix4x4 a, Vector3 v)
	{
		global::TransitionFunctions.SET_X3(ref a, v);
	}

	// Token: 0x060023AD RID: 9133 RVA: 0x00083F28 File Offset: 0x00082128
	private static Vector3 SCALE(Matrix4x4 a)
	{
		return global::TransitionFunctions.GET_3X(a);
	}

	// Token: 0x060023AE RID: 9134 RVA: 0x00083F30 File Offset: 0x00082130
	private static void SCALE(ref Matrix4x4 a, Vector3 v)
	{
		global::TransitionFunctions.SET_3X(ref a, v);
	}

	// Token: 0x060023AF RID: 9135 RVA: 0x00083F3C File Offset: 0x0008213C
	private static Vector3 SLERP(float t, Vector3 a, Vector3 b)
	{
		return global::TransitionFunctions.Slerp(t, a, b);
	}

	// Token: 0x060023B0 RID: 9136 RVA: 0x00083F48 File Offset: 0x00082148
	private static Vector3 LLERP(float t, Vector3 a, Vector3 b)
	{
		return global::TransitionFunctions.Linear(t, a, b);
	}

	// Token: 0x060023B1 RID: 9137 RVA: 0x00083F54 File Offset: 0x00082154
	public static Matrix4x4 Slerp(float t, Matrix4x4 a, Matrix4x4 b)
	{
		Matrix4x4 identity = Matrix4x4.identity;
		Vector3 vector = global::TransitionFunctions.Slerp(t, global::TransitionFunctions.DIR_X(a), global::TransitionFunctions.DIR_X(b));
		Vector3 vector2 = global::TransitionFunctions.Slerp(t, global::TransitionFunctions.DIR_Y(a), global::TransitionFunctions.DIR_Y(b));
		Vector3 vector3 = global::TransitionFunctions.Slerp(t, global::TransitionFunctions.DIR_Z(a), global::TransitionFunctions.DIR_Z(b));
		Quaternion rotation = global::TransitionFunctions.LookRotation(vector3, vector2);
		vector2 = global::TransitionFunctions.Rotate(rotation, global::TransitionFunctions.Y3(global::TransitionFunctions.Length(vector2)));
		if (global::TransitionFunctions.CrossDot(vector3, vector2, vector) > 0f)
		{
			vector = global::TransitionFunctions.Rotate(rotation, global::TransitionFunctions.X3(-global::TransitionFunctions.Length(vector)));
		}
		else
		{
			vector = global::TransitionFunctions.Rotate(rotation, global::TransitionFunctions.X3(global::TransitionFunctions.Length(vector)));
		}
		global::TransitionFunctions.DIR_X(ref identity, vector);
		global::TransitionFunctions.DIR_Y(ref identity, vector2);
		global::TransitionFunctions.DIR_Z(ref identity, vector3);
		global::TransitionFunctions.SCALE(ref identity, global::TransitionFunctions.Linear(t, global::TransitionFunctions.SCALE(a), global::TransitionFunctions.SCALE(b)));
		global::TransitionFunctions.TRANS(ref identity, global::TransitionFunctions.Linear(t, global::TransitionFunctions.TRANS(a), global::TransitionFunctions.TRANS(b)));
		identity.m33 = global::TransitionFunctions.Linear(t, a.m33, b.m33);
		return identity;
	}

	// Token: 0x060023B2 RID: 9138 RVA: 0x00084060 File Offset: 0x00082260
	public static Matrix4x4 SlerpWorldToCamera(float t, Matrix4x4 a, Matrix4x4 b)
	{
		return global::TransitionFunctions.Slerp(t, a.inverse, b.inverse).inverse;
	}

	// Token: 0x060023B3 RID: 9139 RVA: 0x0008408C File Offset: 0x0008228C
	public static Matrix4x4G Linear(double t, Matrix4x4G a, Matrix4x4G b)
	{
		return global::TransitionFunctions.Sum(global::TransitionFunctions.Mul(a, 1.0 - t), global::TransitionFunctions.Mul(b, t));
	}

	// Token: 0x060023B4 RID: 9140 RVA: 0x000840AC File Offset: 0x000822AC
	public static Matrix4x4G Round(double t, Matrix4x4G a, Matrix4x4G b)
	{
		return (t >= 0.5) ? b : a;
	}

	// Token: 0x060023B5 RID: 9141 RVA: 0x000840C4 File Offset: 0x000822C4
	public static Matrix4x4G Ceil(double t, Matrix4x4G a, Matrix4x4G b)
	{
		return (t <= 0.0) ? a : b;
	}

	// Token: 0x060023B6 RID: 9142 RVA: 0x000840DC File Offset: 0x000822DC
	public static Matrix4x4G Floor(double t, Matrix4x4G a, Matrix4x4G b)
	{
		return (t >= 1.0) ? b : a;
	}

	// Token: 0x060023B7 RID: 9143 RVA: 0x000840F4 File Offset: 0x000822F4
	public static Matrix4x4G Spline(double t, Matrix4x4G a, Matrix4x4G b)
	{
		return (t > 0.0) ? ((t < 1.0) ? global::TransitionFunctions.Linear(global::TransitionFunctions.SimpleSpline(t), a, b) : b) : a;
	}

	// Token: 0x060023B8 RID: 9144 RVA: 0x00084130 File Offset: 0x00082330
	public static Matrix4x4G Evaluate(this global::TransitionFunction f, double t, Matrix4x4G a, Matrix4x4G b)
	{
		switch (f)
		{
		case global::TransitionFunction.Linear:
			return global::TransitionFunctions.Linear(t, a, b);
		case global::TransitionFunction.Round:
			return global::TransitionFunctions.Round(t, a, b);
		case global::TransitionFunction.Floor:
			return global::TransitionFunctions.Floor(t, a, b);
		case global::TransitionFunction.Ceil:
			return global::TransitionFunctions.Ceil(t, a, b);
		case global::TransitionFunction.Spline:
			return global::TransitionFunctions.Spline(t, a, b);
		default:
			throw new ArgumentOutOfRangeException("v", "Attempted use of unrecognized TransitionFunction enum value");
		}
	}

	// Token: 0x060023B9 RID: 9145 RVA: 0x0008419C File Offset: 0x0008239C
	public static Matrix4x4G Evaluate(this global::TransitionFunction<Matrix4x4G> v, double t)
	{
		return v.f.Evaluate(t, v.a, v.b);
	}

	// Token: 0x060023BA RID: 9146 RVA: 0x000841BC File Offset: 0x000823BC
	public static Matrix4x4G Mul(Matrix4x4G a, double b)
	{
		Matrix4x4G result;
		result.m00 = a.m00 * b;
		result.m10 = a.m10 * b;
		result.m20 = a.m20 * b;
		result.m30 = a.m30 * b;
		result.m01 = a.m01 * b;
		result.m11 = a.m11 * b;
		result.m21 = a.m21 * b;
		result.m31 = a.m31 * b;
		result.m02 = a.m02 * b;
		result.m12 = a.m12 * b;
		result.m22 = a.m22 * b;
		result.m32 = a.m32 * b;
		result.m03 = a.m03 * b;
		result.m13 = a.m13 * b;
		result.m23 = a.m23 * b;
		result.m33 = a.m33 * b;
		return result;
	}

	// Token: 0x060023BB RID: 9147 RVA: 0x000842CC File Offset: 0x000824CC
	public static Matrix4x4G Sum(Matrix4x4G a, Matrix4x4G b)
	{
		Matrix4x4G result;
		result.m00 = a.m00 + b.m00;
		result.m10 = a.m10 + b.m10;
		result.m20 = a.m20 + b.m20;
		result.m30 = a.m30 + b.m30;
		result.m01 = a.m01 + b.m01;
		result.m11 = a.m11 + b.m11;
		result.m21 = a.m21 + b.m21;
		result.m31 = a.m31 + b.m31;
		result.m02 = a.m02 + b.m02;
		result.m12 = a.m12 + b.m12;
		result.m22 = a.m22 + b.m22;
		result.m32 = a.m32 + b.m32;
		result.m03 = a.m03 + b.m03;
		result.m13 = a.m13 + b.m13;
		result.m23 = a.m23 + b.m23;
		result.m33 = a.m33 + b.m33;
		return result;
	}

	// Token: 0x060023BC RID: 9148 RVA: 0x0008443C File Offset: 0x0008263C
	public static Matrix4x4G Inverse(Matrix4x4G v)
	{
		Matrix4x4G result;
		Matrix4x4G.Inverse(ref v, ref result);
		return result;
	}

	// Token: 0x060023BD RID: 9149 RVA: 0x00084454 File Offset: 0x00082654
	public static Matrix4x4G Transpose(Matrix4x4G v)
	{
		Matrix4x4G result;
		Matrix4x4G.Transpose(ref v, ref result);
		return result;
	}

	// Token: 0x060023BE RID: 9150 RVA: 0x0008446C File Offset: 0x0008266C
	private static Vector3G VECT3F(double x, double y, double z)
	{
		Vector3G result;
		result.x = x;
		result.y = y;
		result.z = z;
		return result;
	}

	// Token: 0x060023BF RID: 9151 RVA: 0x00084494 File Offset: 0x00082694
	private static Vector3G GET_0X(Matrix4x4G a)
	{
		return global::TransitionFunctions.VECT3F(a.m00, a.m01, a.m02);
	}

	// Token: 0x060023C0 RID: 9152 RVA: 0x000844B0 File Offset: 0x000826B0
	private static Vector3G GET_X0(Matrix4x4G a)
	{
		return global::TransitionFunctions.VECT3F(a.m00, a.m10, a.m20);
	}

	// Token: 0x060023C1 RID: 9153 RVA: 0x000844CC File Offset: 0x000826CC
	private static void SET_0X(ref Matrix4x4G m, Vector3G v)
	{
		m.m00 = v.x;
		m.m01 = v.y;
		m.m02 = v.z;
	}

	// Token: 0x060023C2 RID: 9154 RVA: 0x000844F8 File Offset: 0x000826F8
	private static void SET_X0(ref Matrix4x4G m, Vector3G v)
	{
		m.m00 = v.x;
		m.m10 = v.y;
		m.m20 = v.z;
	}

	// Token: 0x060023C3 RID: 9155 RVA: 0x00084524 File Offset: 0x00082724
	private static Vector3G GET_1X(Matrix4x4G a)
	{
		return global::TransitionFunctions.VECT3F(a.m10, a.m11, a.m12);
	}

	// Token: 0x060023C4 RID: 9156 RVA: 0x00084540 File Offset: 0x00082740
	private static Vector3G GET_X1(Matrix4x4G a)
	{
		return global::TransitionFunctions.VECT3F(a.m01, a.m11, a.m21);
	}

	// Token: 0x060023C5 RID: 9157 RVA: 0x0008455C File Offset: 0x0008275C
	private static void SET_1X(ref Matrix4x4G m, Vector3G v)
	{
		m.m10 = v.x;
		m.m11 = v.y;
		m.m12 = v.z;
	}

	// Token: 0x060023C6 RID: 9158 RVA: 0x00084588 File Offset: 0x00082788
	private static void SET_X1(ref Matrix4x4G m, Vector3G v)
	{
		m.m01 = v.x;
		m.m11 = v.y;
		m.m21 = v.z;
	}

	// Token: 0x060023C7 RID: 9159 RVA: 0x000845B4 File Offset: 0x000827B4
	private static Vector3G GET_2X(Matrix4x4G a)
	{
		return global::TransitionFunctions.VECT3F(a.m20, a.m21, a.m22);
	}

	// Token: 0x060023C8 RID: 9160 RVA: 0x000845D0 File Offset: 0x000827D0
	private static Vector3G GET_X2(Matrix4x4G a)
	{
		return global::TransitionFunctions.VECT3F(a.m02, a.m12, a.m22);
	}

	// Token: 0x060023C9 RID: 9161 RVA: 0x000845EC File Offset: 0x000827EC
	private static void SET_2X(ref Matrix4x4G m, Vector3G v)
	{
		m.m20 = v.x;
		m.m21 = v.y;
		m.m22 = v.z;
	}

	// Token: 0x060023CA RID: 9162 RVA: 0x00084618 File Offset: 0x00082818
	private static void SET_X2(ref Matrix4x4G m, Vector3G v)
	{
		m.m02 = v.x;
		m.m12 = v.y;
		m.m22 = v.z;
	}

	// Token: 0x060023CB RID: 9163 RVA: 0x00084644 File Offset: 0x00082844
	private static Vector3G GET_3X(Matrix4x4G a)
	{
		return global::TransitionFunctions.VECT3F(a.m30, a.m31, a.m32);
	}

	// Token: 0x060023CC RID: 9164 RVA: 0x00084660 File Offset: 0x00082860
	private static Vector3G GET_X3(Matrix4x4G a)
	{
		return global::TransitionFunctions.VECT3F(a.m03, a.m13, a.m23);
	}

	// Token: 0x060023CD RID: 9165 RVA: 0x0008467C File Offset: 0x0008287C
	private static void SET_3X(ref Matrix4x4G m, Vector3G v)
	{
		m.m30 = v.x;
		m.m31 = v.y;
		m.m32 = v.z;
	}

	// Token: 0x060023CE RID: 9166 RVA: 0x000846A8 File Offset: 0x000828A8
	private static void SET_X3(ref Matrix4x4G m, Vector3G v)
	{
		m.m03 = v.x;
		m.m13 = v.y;
		m.m23 = v.z;
	}

	// Token: 0x060023CF RID: 9167 RVA: 0x000846D4 File Offset: 0x000828D4
	private static Vector3G DIR_X(Matrix4x4G a)
	{
		return global::TransitionFunctions.GET_X0(a);
	}

	// Token: 0x060023D0 RID: 9168 RVA: 0x000846DC File Offset: 0x000828DC
	private static void DIR_X(ref Matrix4x4G a, Vector3G v)
	{
		global::TransitionFunctions.SET_X0(ref a, v);
	}

	// Token: 0x060023D1 RID: 9169 RVA: 0x000846E8 File Offset: 0x000828E8
	private static Vector3G DIR_Y(Matrix4x4G a)
	{
		return global::TransitionFunctions.GET_X1(a);
	}

	// Token: 0x060023D2 RID: 9170 RVA: 0x000846F0 File Offset: 0x000828F0
	private static void DIR_Y(ref Matrix4x4G a, Vector3G v)
	{
		global::TransitionFunctions.SET_X1(ref a, v);
	}

	// Token: 0x060023D3 RID: 9171 RVA: 0x000846FC File Offset: 0x000828FC
	private static Vector3G DIR_Z(Matrix4x4G a)
	{
		return global::TransitionFunctions.GET_X2(a);
	}

	// Token: 0x060023D4 RID: 9172 RVA: 0x00084704 File Offset: 0x00082904
	private static void DIR_Z(ref Matrix4x4G a, Vector3G v)
	{
		global::TransitionFunctions.SET_X2(ref a, v);
	}

	// Token: 0x060023D5 RID: 9173 RVA: 0x00084710 File Offset: 0x00082910
	private static Vector3G TRANS(Matrix4x4G a)
	{
		return global::TransitionFunctions.GET_X3(a);
	}

	// Token: 0x060023D6 RID: 9174 RVA: 0x00084718 File Offset: 0x00082918
	private static void TRANS(ref Matrix4x4G a, Vector3G v)
	{
		global::TransitionFunctions.SET_X3(ref a, v);
	}

	// Token: 0x060023D7 RID: 9175 RVA: 0x00084724 File Offset: 0x00082924
	private static Vector3G SCALE(Matrix4x4G a)
	{
		return global::TransitionFunctions.GET_3X(a);
	}

	// Token: 0x060023D8 RID: 9176 RVA: 0x0008472C File Offset: 0x0008292C
	private static void SCALE(ref Matrix4x4G a, Vector3G v)
	{
		global::TransitionFunctions.SET_3X(ref a, v);
	}

	// Token: 0x060023D9 RID: 9177 RVA: 0x00084738 File Offset: 0x00082938
	private static Vector3G SLERP(double t, Vector3G a, Vector3G b)
	{
		return global::TransitionFunctions.Slerp(t, a, b);
	}

	// Token: 0x060023DA RID: 9178 RVA: 0x00084744 File Offset: 0x00082944
	private static Vector3G LLERP(double t, Vector3G a, Vector3G b)
	{
		return global::TransitionFunctions.Linear(t, a, b);
	}

	// Token: 0x060023DB RID: 9179 RVA: 0x00084750 File Offset: 0x00082950
	public static Matrix4x4G Slerp(double t, Matrix4x4G a, Matrix4x4G b)
	{
		Matrix4x4G identity = Matrix4x4G.identity;
		Vector3G vector3G = global::TransitionFunctions.Slerp(t, global::TransitionFunctions.DIR_X(a), global::TransitionFunctions.DIR_X(b));
		Vector3G vector3G2 = global::TransitionFunctions.Slerp(t, global::TransitionFunctions.DIR_Y(a), global::TransitionFunctions.DIR_Y(b));
		Vector3G vector3G3 = global::TransitionFunctions.Slerp(t, global::TransitionFunctions.DIR_Z(a), global::TransitionFunctions.DIR_Z(b));
		QuaternionG rotation = global::TransitionFunctions.LookRotation(vector3G3, vector3G2);
		vector3G2 = global::TransitionFunctions.Rotate(rotation, global::TransitionFunctions.Y3(global::TransitionFunctions.Length(vector3G2)));
		if (global::TransitionFunctions.CrossDot(vector3G3, vector3G2, vector3G) > 0.0)
		{
			vector3G = global::TransitionFunctions.Rotate(rotation, global::TransitionFunctions.X3(-global::TransitionFunctions.Length(vector3G)));
		}
		else
		{
			vector3G = global::TransitionFunctions.Rotate(rotation, global::TransitionFunctions.X3(global::TransitionFunctions.Length(vector3G)));
		}
		global::TransitionFunctions.DIR_X(ref identity, vector3G);
		global::TransitionFunctions.DIR_Y(ref identity, vector3G2);
		global::TransitionFunctions.DIR_Z(ref identity, vector3G3);
		global::TransitionFunctions.SCALE(ref identity, global::TransitionFunctions.Linear(t, global::TransitionFunctions.SCALE(a), global::TransitionFunctions.SCALE(b)));
		global::TransitionFunctions.TRANS(ref identity, global::TransitionFunctions.Linear(t, global::TransitionFunctions.TRANS(a), global::TransitionFunctions.TRANS(b)));
		identity.m33 = global::TransitionFunctions.Linear(t, a.m33, b.m33);
		return identity;
	}

	// Token: 0x060023DC RID: 9180 RVA: 0x00084860 File Offset: 0x00082A60
	public static Matrix4x4G SlerpWorldToCamera(double t, Matrix4x4G a, Matrix4x4G b)
	{
		return global::TransitionFunctions.Inverse(global::TransitionFunctions.Slerp(t, global::TransitionFunctions.Inverse(a), global::TransitionFunctions.Inverse(b)));
	}

	// Token: 0x060023DD RID: 9181 RVA: 0x0008487C File Offset: 0x00082A7C
	public static Quaternion Round(float t, Quaternion a, Quaternion b)
	{
		return (t >= 0.5f) ? b : a;
	}

	// Token: 0x060023DE RID: 9182 RVA: 0x00084890 File Offset: 0x00082A90
	public static Quaternion Ceil(float t, Quaternion a, Quaternion b)
	{
		return (t <= 0f) ? a : b;
	}

	// Token: 0x060023DF RID: 9183 RVA: 0x000848A4 File Offset: 0x00082AA4
	public static Quaternion Floor(float t, Quaternion a, Quaternion b)
	{
		return (t >= 1f) ? b : a;
	}

	// Token: 0x060023E0 RID: 9184 RVA: 0x000848B8 File Offset: 0x00082AB8
	public static Quaternion Spline(float t, Quaternion a, Quaternion b)
	{
		return (t > 0f) ? ((t < 1f) ? global::TransitionFunctions.Linear(global::TransitionFunctions.SimpleSpline(t), a, b) : b) : a;
	}

	// Token: 0x060023E1 RID: 9185 RVA: 0x000848EC File Offset: 0x00082AEC
	public static Quaternion Evaluate(this global::TransitionFunction f, float t, Quaternion a, Quaternion b)
	{
		switch (f)
		{
		case global::TransitionFunction.Linear:
			return global::TransitionFunctions.Linear(t, a, b);
		case global::TransitionFunction.Round:
			return global::TransitionFunctions.Round(t, a, b);
		case global::TransitionFunction.Floor:
			return global::TransitionFunctions.Floor(t, a, b);
		case global::TransitionFunction.Ceil:
			return global::TransitionFunctions.Ceil(t, a, b);
		case global::TransitionFunction.Spline:
			return global::TransitionFunctions.Spline(t, a, b);
		default:
			throw new ArgumentOutOfRangeException("v", "Attempted use of unrecognized TransitionFunction enum value");
		}
	}

	// Token: 0x060023E2 RID: 9186 RVA: 0x00084958 File Offset: 0x00082B58
	public static Quaternion Evaluate(this global::TransitionFunction<Quaternion> v, float t)
	{
		return v.f.Evaluate(t, v.a, v.b);
	}

	// Token: 0x060023E3 RID: 9187 RVA: 0x00084978 File Offset: 0x00082B78
	public static Quaternion Mul(Quaternion a, float b)
	{
		Quaternion result;
		result.x = a.x * b;
		result.y = a.y * b;
		result.z = a.z * b;
		result.w = a.w * b;
		return result;
	}

	// Token: 0x060023E4 RID: 9188 RVA: 0x000849C8 File Offset: 0x00082BC8
	public static Quaternion Sum(Quaternion a, Quaternion b)
	{
		Quaternion result;
		result.x = a.x + b.x;
		result.y = a.y + b.y;
		result.z = a.z + b.z;
		result.w = a.w * b.w;
		return result;
	}

	// Token: 0x060023E5 RID: 9189 RVA: 0x00084A30 File Offset: 0x00082C30
	public static Quaternion Linear(float t, Quaternion a, Quaternion b)
	{
		return global::TransitionFunctions.Slerp(t, a, b);
	}

	// Token: 0x060023E6 RID: 9190 RVA: 0x00084A3C File Offset: 0x00082C3C
	public static Quaternion Slerp(float t, Quaternion a, Quaternion b)
	{
		Quaternion result;
		if (t == 0f)
		{
			result = a;
		}
		else if (t == 1f)
		{
			result = b;
		}
		else
		{
			float num = a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
			if (num == 1f)
			{
				result = a;
			}
			else if (num < 0f)
			{
				num = global::TransitionFunctions.Acos(-num);
				float num2 = global::TransitionFunctions.Sin(num);
				if (num2 == 0f)
				{
					float num3 = 1f - t;
					result.x = a.x * num3 + b.x * t;
					result.y = a.y * num3 + b.y * t;
					result.z = a.z * num3 + b.z * t;
					result.w = a.w * num3 + b.w * t;
				}
				else
				{
					float num4 = global::TransitionFunctions.Sin(num * t);
					float num3 = global::TransitionFunctions.Sin(num * (1f - t));
					result.x = (a.x * num3 - b.x * num4) / num2;
					result.y = (a.y * num3 - b.y * num4) / num2;
					result.z = (a.z * num3 - b.z * num4) / num2;
					result.w = (a.w * num3 - b.w * num4) / num2;
				}
			}
			else
			{
				num = global::TransitionFunctions.Acos(num);
				float num2 = global::TransitionFunctions.Sin(num);
				if (num2 == 0f)
				{
					float num3 = 1f - t;
					result.x = a.x * num3 + b.x * t;
					result.y = a.y * num3 + b.y * t;
					result.z = a.z * num3 + b.z * t;
					result.w = a.w * num3 + b.w * t;
				}
				else
				{
					float num4 = global::TransitionFunctions.Sin(num * t);
					float num3 = global::TransitionFunctions.Sin(num * (1f - t));
					result.x = (a.x * num3 + b.x * num4) / num2;
					result.y = (a.y * num3 + b.y * num4) / num2;
					result.z = (a.z * num3 + b.z * num4) / num2;
					result.w = (a.w * num3 + b.w * num4) / num2;
				}
			}
		}
		return result;
	}

	// Token: 0x060023E7 RID: 9191 RVA: 0x00084D20 File Offset: 0x00082F20
	public static Quaternion LookRotation(Vector3 forward, Vector3 up)
	{
		return Quaternion.LookRotation(forward, up);
	}

	// Token: 0x060023E8 RID: 9192 RVA: 0x00084D2C File Offset: 0x00082F2C
	public static Vector3 Rotate(Quaternion rotation, Vector3 vector)
	{
		return rotation * vector;
	}

	// Token: 0x060023E9 RID: 9193 RVA: 0x00084D38 File Offset: 0x00082F38
	public static QuaternionG Round(double t, QuaternionG a, QuaternionG b)
	{
		return (t >= 0.5) ? b : a;
	}

	// Token: 0x060023EA RID: 9194 RVA: 0x00084D50 File Offset: 0x00082F50
	public static QuaternionG Ceil(double t, QuaternionG a, QuaternionG b)
	{
		return (t <= 0.0) ? a : b;
	}

	// Token: 0x060023EB RID: 9195 RVA: 0x00084D68 File Offset: 0x00082F68
	public static QuaternionG Floor(double t, QuaternionG a, QuaternionG b)
	{
		return (t >= 1.0) ? b : a;
	}

	// Token: 0x060023EC RID: 9196 RVA: 0x00084D80 File Offset: 0x00082F80
	public static QuaternionG Spline(double t, QuaternionG a, QuaternionG b)
	{
		return (t > 0.0) ? ((t < 1.0) ? global::TransitionFunctions.Linear(global::TransitionFunctions.SimpleSpline(t), a, b) : b) : a;
	}

	// Token: 0x060023ED RID: 9197 RVA: 0x00084DBC File Offset: 0x00082FBC
	public static QuaternionG Evaluate(this global::TransitionFunction f, double t, QuaternionG a, QuaternionG b)
	{
		switch (f)
		{
		case global::TransitionFunction.Linear:
			return global::TransitionFunctions.Linear(t, a, b);
		case global::TransitionFunction.Round:
			return global::TransitionFunctions.Round(t, a, b);
		case global::TransitionFunction.Floor:
			return global::TransitionFunctions.Floor(t, a, b);
		case global::TransitionFunction.Ceil:
			return global::TransitionFunctions.Ceil(t, a, b);
		case global::TransitionFunction.Spline:
			return global::TransitionFunctions.Spline(t, a, b);
		default:
			throw new ArgumentOutOfRangeException("v", "Attempted use of unrecognized TransitionFunction enum value");
		}
	}

	// Token: 0x060023EE RID: 9198 RVA: 0x00084E28 File Offset: 0x00083028
	public static QuaternionG Evaluate(this global::TransitionFunction<QuaternionG> v, double t)
	{
		return v.f.Evaluate(t, v.a, v.b);
	}

	// Token: 0x060023EF RID: 9199 RVA: 0x00084E48 File Offset: 0x00083048
	public static QuaternionG Mul(QuaternionG a, double b)
	{
		QuaternionG result;
		result.x = a.x * b;
		result.y = a.y * b;
		result.z = a.z * b;
		result.w = a.w * b;
		return result;
	}

	// Token: 0x060023F0 RID: 9200 RVA: 0x00084E98 File Offset: 0x00083098
	public static QuaternionG Sum(QuaternionG a, QuaternionG b)
	{
		QuaternionG result;
		result.x = a.x + b.x;
		result.y = a.y + b.y;
		result.z = a.z + b.z;
		result.w = a.w * b.w;
		return result;
	}

	// Token: 0x060023F1 RID: 9201 RVA: 0x00084F00 File Offset: 0x00083100
	public static QuaternionG Linear(double t, QuaternionG a, QuaternionG b)
	{
		return global::TransitionFunctions.Slerp(t, a, b);
	}

	// Token: 0x060023F2 RID: 9202 RVA: 0x00084F0C File Offset: 0x0008310C
	public static QuaternionG Slerp(double t, QuaternionG a, QuaternionG b)
	{
		QuaternionG result;
		if (t == 0.0)
		{
			result = a;
		}
		else if (t == 1.0)
		{
			result = b;
		}
		else
		{
			double num = a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
			if (num == 1.0)
			{
				result = a;
			}
			else if (num < 0.0)
			{
				num = global::TransitionFunctions.Acos(-num);
				double num2 = global::TransitionFunctions.Sin(num);
				if (num2 == 0.0)
				{
					double num3 = 1.0 - t;
					result.x = a.x * num3 + b.x * t;
					result.y = a.y * num3 + b.y * t;
					result.z = a.z * num3 + b.z * t;
					result.w = a.w * num3 + b.w * t;
				}
				else
				{
					double num4 = global::TransitionFunctions.Sin(num * t);
					double num3 = global::TransitionFunctions.Sin(num * (1.0 - t));
					result.x = (a.x * num3 - b.x * num4) / num2;
					result.y = (a.y * num3 - b.y * num4) / num2;
					result.z = (a.z * num3 - b.z * num4) / num2;
					result.w = (a.w * num3 - b.w * num4) / num2;
				}
			}
			else
			{
				num = global::TransitionFunctions.Acos(num);
				double num2 = global::TransitionFunctions.Sin(num);
				if (num2 == 0.0)
				{
					double num3 = 1.0 - t;
					result.x = a.x * num3 + b.x * t;
					result.y = a.y * num3 + b.y * t;
					result.z = a.z * num3 + b.z * t;
					result.w = a.w * num3 + b.w * t;
				}
				else
				{
					double num4 = global::TransitionFunctions.Sin(num * t);
					double num3 = global::TransitionFunctions.Sin(num * (1.0 - t));
					result.x = (a.x * num3 + b.x * num4) / num2;
					result.y = (a.y * num3 + b.y * num4) / num2;
					result.z = (a.z * num3 + b.z * num4) / num2;
					result.w = (a.w * num3 + b.w * num4) / num2;
				}
			}
		}
		return result;
	}

	// Token: 0x060023F3 RID: 9203 RVA: 0x00085218 File Offset: 0x00083418
	public static QuaternionG LookRotation(Vector3G forward, Vector3G up)
	{
		QuaternionG result;
		QuaternionG.LookRotation(ref forward, ref up, ref result);
		return result;
	}

	// Token: 0x060023F4 RID: 9204 RVA: 0x00085234 File Offset: 0x00083434
	public static Vector3G Rotate(QuaternionG rotation, Vector3G vector)
	{
		Vector3G result;
		QuaternionG.Mult(ref rotation, ref vector, ref result);
		return result;
	}

	// Token: 0x060023F5 RID: 9205 RVA: 0x00085250 File Offset: 0x00083450
	public static Vector2 Linear(float t, Vector2 a, Vector2 b)
	{
		return global::TransitionFunctions.Sum(global::TransitionFunctions.Mul(a, 1f - t), global::TransitionFunctions.Mul(b, t));
	}

	// Token: 0x060023F6 RID: 9206 RVA: 0x0008526C File Offset: 0x0008346C
	public static Vector2 Round(float t, Vector2 a, Vector2 b)
	{
		return (t >= 0.5f) ? b : a;
	}

	// Token: 0x060023F7 RID: 9207 RVA: 0x00085280 File Offset: 0x00083480
	public static Vector2 Ceil(float t, Vector2 a, Vector2 b)
	{
		return (t <= 0f) ? a : b;
	}

	// Token: 0x060023F8 RID: 9208 RVA: 0x00085294 File Offset: 0x00083494
	public static Vector2 Floor(float t, Vector2 a, Vector2 b)
	{
		return (t >= 1f) ? b : a;
	}

	// Token: 0x060023F9 RID: 9209 RVA: 0x000852A8 File Offset: 0x000834A8
	public static Vector2 Spline(float t, Vector2 a, Vector2 b)
	{
		return (t > 0f) ? ((t < 1f) ? global::TransitionFunctions.Linear(global::TransitionFunctions.SimpleSpline(t), a, b) : b) : a;
	}

	// Token: 0x060023FA RID: 9210 RVA: 0x000852DC File Offset: 0x000834DC
	public static Vector2 Evaluate(this global::TransitionFunction f, float t, Vector2 a, Vector2 b)
	{
		switch (f)
		{
		case global::TransitionFunction.Linear:
			return global::TransitionFunctions.Linear(t, a, b);
		case global::TransitionFunction.Round:
			return global::TransitionFunctions.Round(t, a, b);
		case global::TransitionFunction.Floor:
			return global::TransitionFunctions.Floor(t, a, b);
		case global::TransitionFunction.Ceil:
			return global::TransitionFunctions.Ceil(t, a, b);
		case global::TransitionFunction.Spline:
			return global::TransitionFunctions.Spline(t, a, b);
		default:
			throw new ArgumentOutOfRangeException("v", "Attempted use of unrecognized TransitionFunction enum value");
		}
	}

	// Token: 0x060023FB RID: 9211 RVA: 0x00085348 File Offset: 0x00083548
	public static Vector2 Evaluate(this global::TransitionFunction<Vector2> v, float t)
	{
		return v.f.Evaluate(t, v.a, v.b);
	}

	// Token: 0x060023FC RID: 9212 RVA: 0x00085368 File Offset: 0x00083568
	public static Vector2 Mul(Vector2 a, float b)
	{
		Vector2 result;
		result.x = a.x * b;
		result.y = a.y * b;
		return result;
	}

	// Token: 0x060023FD RID: 9213 RVA: 0x00085398 File Offset: 0x00083598
	public static Vector2 Sum(Vector2 a, Vector2 b)
	{
		Vector2 result;
		result.x = a.x + b.x;
		result.y = a.y + b.y;
		return result;
	}

	// Token: 0x060023FE RID: 9214 RVA: 0x000853D4 File Offset: 0x000835D4
	public static Vector2 Slerp(float t, Vector2 a, Vector2 b)
	{
		float num = global::TransitionFunctions.DegreesToRadians(Vector2.Angle(a, b));
		float num2;
		if (num == 0f || (num2 = global::TransitionFunctions.Sin(num)) == 0f)
		{
			return global::TransitionFunctions.Linear(t, a, b);
		}
		float b2 = global::TransitionFunctions.Sin((1f - t) * num) / num2;
		float b3 = global::TransitionFunctions.Sin(t * num) / num2;
		return global::TransitionFunctions.Sum(global::TransitionFunctions.Mul(a, b2), global::TransitionFunctions.Mul(b, b3));
	}

	// Token: 0x060023FF RID: 9215 RVA: 0x00085448 File Offset: 0x00083648
	public static Vector3 Linear(float t, Vector3 a, Vector3 b)
	{
		return global::TransitionFunctions.Sum(global::TransitionFunctions.Mul(a, 1f - t), global::TransitionFunctions.Mul(b, t));
	}

	// Token: 0x06002400 RID: 9216 RVA: 0x00085464 File Offset: 0x00083664
	public static Vector3 Round(float t, Vector3 a, Vector3 b)
	{
		return (t >= 0.5f) ? b : a;
	}

	// Token: 0x06002401 RID: 9217 RVA: 0x00085478 File Offset: 0x00083678
	public static Vector3 Ceil(float t, Vector3 a, Vector3 b)
	{
		return (t <= 0f) ? a : b;
	}

	// Token: 0x06002402 RID: 9218 RVA: 0x0008548C File Offset: 0x0008368C
	public static Vector3 Floor(float t, Vector3 a, Vector3 b)
	{
		return (t >= 1f) ? b : a;
	}

	// Token: 0x06002403 RID: 9219 RVA: 0x000854A0 File Offset: 0x000836A0
	public static Vector3 Spline(float t, Vector3 a, Vector3 b)
	{
		return (t > 0f) ? ((t < 1f) ? global::TransitionFunctions.Linear(global::TransitionFunctions.SimpleSpline(t), a, b) : b) : a;
	}

	// Token: 0x06002404 RID: 9220 RVA: 0x000854D4 File Offset: 0x000836D4
	public static Vector3 Evaluate(this global::TransitionFunction f, float t, Vector3 a, Vector3 b)
	{
		switch (f)
		{
		case global::TransitionFunction.Linear:
			return global::TransitionFunctions.Linear(t, a, b);
		case global::TransitionFunction.Round:
			return global::TransitionFunctions.Round(t, a, b);
		case global::TransitionFunction.Floor:
			return global::TransitionFunctions.Floor(t, a, b);
		case global::TransitionFunction.Ceil:
			return global::TransitionFunctions.Ceil(t, a, b);
		case global::TransitionFunction.Spline:
			return global::TransitionFunctions.Spline(t, a, b);
		default:
			throw new ArgumentOutOfRangeException("v", "Attempted use of unrecognized TransitionFunction enum value");
		}
	}

	// Token: 0x06002405 RID: 9221 RVA: 0x00085540 File Offset: 0x00083740
	public static Vector3 Evaluate(this global::TransitionFunction<Vector3> v, float t)
	{
		return v.f.Evaluate(t, v.a, v.b);
	}

	// Token: 0x06002406 RID: 9222 RVA: 0x00085560 File Offset: 0x00083760
	public static Vector3 Mul(Vector3 a, float b)
	{
		Vector3 result;
		result.x = a.x * b;
		result.y = a.y * b;
		result.z = a.z * b;
		return result;
	}

	// Token: 0x06002407 RID: 9223 RVA: 0x000855A0 File Offset: 0x000837A0
	public static Vector3 Sum(Vector3 a, Vector3 b)
	{
		Vector3 result;
		result.x = a.x + b.x;
		result.y = a.y + b.y;
		result.z = a.z + b.z;
		return result;
	}

	// Token: 0x06002408 RID: 9224 RVA: 0x000855F0 File Offset: 0x000837F0
	public static Vector3 Slerp(float t, Vector3 a, Vector3 b)
	{
		float num = global::TransitionFunctions.AngleRadians(a, b);
		float num2;
		if (num == 0f || (num2 = global::TransitionFunctions.Sin(num)) == 0f)
		{
			return global::TransitionFunctions.Linear(t, a, b);
		}
		float b2 = global::TransitionFunctions.Sin((1f - t) * num) / num2;
		float b3 = global::TransitionFunctions.Sin(t * num) / num2;
		return global::TransitionFunctions.Sum(global::TransitionFunctions.Mul(a, b2), global::TransitionFunctions.Mul(b, b3));
	}

	// Token: 0x06002409 RID: 9225 RVA: 0x0008565C File Offset: 0x0008385C
	public static Vector3 Normalize(Vector3 v)
	{
		float num = v.x * v.x + v.y * v.y + v.z * v.z;
		if (num == 0f || num == 1f)
		{
			return v;
		}
		num = global::TransitionFunctions.Sqrt(num);
		Vector3 result;
		result.x = v.x / num;
		result.y = v.y / num;
		result.z = v.z / num;
		return result;
	}

	// Token: 0x0600240A RID: 9226 RVA: 0x000856EC File Offset: 0x000838EC
	public static float AngleRadians(Vector3 a, Vector3 b)
	{
		float num = global::TransitionFunctions.DotNormal(a, b);
		if (num >= 1f)
		{
			return 0f;
		}
		if (num <= -1f)
		{
			return 3.14159274f;
		}
		if (num == 0f)
		{
			return 1.57079637f;
		}
		return global::TransitionFunctions.Acos(num);
	}

	// Token: 0x0600240B RID: 9227 RVA: 0x0008573C File Offset: 0x0008393C
	public static float AngleDegrees(Vector3 a, Vector3 b)
	{
		float num = global::TransitionFunctions.DotNormal(a, b);
		if (num >= 1f)
		{
			return 0f;
		}
		if (num <= -1f)
		{
			return 180f;
		}
		if (num == 0f)
		{
			return 90f;
		}
		return global::TransitionFunctions.RadiansToDegrees(global::TransitionFunctions.Acos(num));
	}

	// Token: 0x0600240C RID: 9228 RVA: 0x00085790 File Offset: 0x00083990
	public static Vector3 Vect(float x, float y, float z)
	{
		Vector3 result;
		result.x = x;
		result.y = y;
		result.z = z;
		return result;
	}

	// Token: 0x0600240D RID: 9229 RVA: 0x000857B8 File Offset: 0x000839B8
	public static float Length(Vector3 a)
	{
		return global::TransitionFunctions.Sqrt(a.x * a.x + a.y * a.y + a.z * a.z);
	}

	// Token: 0x0600240E RID: 9230 RVA: 0x000857FC File Offset: 0x000839FC
	public static Vector3 Cross(Vector3 a, Vector3 b)
	{
		Vector3 result;
		result.x = a.y * b.z - a.z * b.y;
		result.y = a.z * b.x - a.x * b.z;
		result.z = a.x * b.y - a.y * b.x;
		return result;
	}

	// Token: 0x0600240F RID: 9231 RVA: 0x0008587C File Offset: 0x00083A7C
	public static float CrossDot(Vector3 a, Vector3 b, Vector3 dotB)
	{
		return (a.y * b.z - a.z * b.y) * dotB.x + (a.z * b.x - a.x * b.z) * dotB.y + (a.x * b.y - a.y * b.x) * dotB.z;
	}

	// Token: 0x06002410 RID: 9232 RVA: 0x00085900 File Offset: 0x00083B00
	public static float Dot(Vector3 a, Vector3 b)
	{
		return a.x * b.x + a.y * b.y + a.z * b.z;
	}

	// Token: 0x06002411 RID: 9233 RVA: 0x00085934 File Offset: 0x00083B34
	public static float DotNormal(Vector3 a, Vector3 b)
	{
		return global::TransitionFunctions.Dot(global::TransitionFunctions.Normalize(a), global::TransitionFunctions.Normalize(b));
	}

	// Token: 0x06002412 RID: 9234 RVA: 0x00085948 File Offset: 0x00083B48
	public static Vector3 X3(float x)
	{
		Vector3 result;
		result.y = (result.z = 0f);
		result.x = x;
		return result;
	}

	// Token: 0x06002413 RID: 9235 RVA: 0x00085974 File Offset: 0x00083B74
	public static Vector3 Y3(float y)
	{
		Vector3 result;
		result.x = (result.z = 0f);
		result.y = y;
		return result;
	}

	// Token: 0x06002414 RID: 9236 RVA: 0x000859A0 File Offset: 0x00083BA0
	public static Vector3 Z3(float z)
	{
		Vector3 result;
		result.x = (result.y = 0f);
		result.z = z;
		return result;
	}

	// Token: 0x06002415 RID: 9237 RVA: 0x000859CC File Offset: 0x00083BCC
	public static Vector3 Scale3(float xyz)
	{
		Vector3 result;
		result.z = xyz;
		result.y = xyz;
		result.x = xyz;
		return result;
	}

	// Token: 0x06002416 RID: 9238 RVA: 0x000859F8 File Offset: 0x00083BF8
	public static Vector3G Linear(double t, Vector3G a, Vector3G b)
	{
		return global::TransitionFunctions.Sum(global::TransitionFunctions.Mul(a, 1.0 - t), global::TransitionFunctions.Mul(b, t));
	}

	// Token: 0x06002417 RID: 9239 RVA: 0x00085A18 File Offset: 0x00083C18
	public static Vector3G Round(double t, Vector3G a, Vector3G b)
	{
		return (t >= 0.5) ? b : a;
	}

	// Token: 0x06002418 RID: 9240 RVA: 0x00085A30 File Offset: 0x00083C30
	public static Vector3G Ceil(double t, Vector3G a, Vector3G b)
	{
		return (t <= 0.0) ? a : b;
	}

	// Token: 0x06002419 RID: 9241 RVA: 0x00085A48 File Offset: 0x00083C48
	public static Vector3G Floor(double t, Vector3G a, Vector3G b)
	{
		return (t >= 1.0) ? b : a;
	}

	// Token: 0x0600241A RID: 9242 RVA: 0x00085A60 File Offset: 0x00083C60
	public static Vector3G Spline(double t, Vector3G a, Vector3G b)
	{
		return (t > 0.0) ? ((t < 1.0) ? global::TransitionFunctions.Linear(global::TransitionFunctions.SimpleSpline(t), a, b) : b) : a;
	}

	// Token: 0x0600241B RID: 9243 RVA: 0x00085A9C File Offset: 0x00083C9C
	public static Vector3G Evaluate(this global::TransitionFunction f, double t, Vector3G a, Vector3G b)
	{
		switch (f)
		{
		case global::TransitionFunction.Linear:
			return global::TransitionFunctions.Linear(t, a, b);
		case global::TransitionFunction.Round:
			return global::TransitionFunctions.Round(t, a, b);
		case global::TransitionFunction.Floor:
			return global::TransitionFunctions.Floor(t, a, b);
		case global::TransitionFunction.Ceil:
			return global::TransitionFunctions.Ceil(t, a, b);
		case global::TransitionFunction.Spline:
			return global::TransitionFunctions.Spline(t, a, b);
		default:
			throw new ArgumentOutOfRangeException("v", "Attempted use of unrecognized TransitionFunction enum value");
		}
	}

	// Token: 0x0600241C RID: 9244 RVA: 0x00085B08 File Offset: 0x00083D08
	public static Vector3G Evaluate(this global::TransitionFunction<Vector3G> v, double t)
	{
		return v.f.Evaluate(t, v.a, v.b);
	}

	// Token: 0x0600241D RID: 9245 RVA: 0x00085B28 File Offset: 0x00083D28
	public static Vector3G Mul(Vector3G a, double b)
	{
		Vector3G result;
		result.x = a.x * b;
		result.y = a.y * b;
		result.z = a.z * b;
		return result;
	}

	// Token: 0x0600241E RID: 9246 RVA: 0x00085B68 File Offset: 0x00083D68
	public static Vector3G Sum(Vector3G a, Vector3G b)
	{
		Vector3G result;
		result.x = a.x + b.x;
		result.y = a.y + b.y;
		result.z = a.z + b.z;
		return result;
	}

	// Token: 0x0600241F RID: 9247 RVA: 0x00085BB8 File Offset: 0x00083DB8
	public static Vector3G Slerp(double t, Vector3G a, Vector3G b)
	{
		double num = global::TransitionFunctions.AngleRadians(a, b);
		double num2;
		if (num == 0.0 || (num2 = global::TransitionFunctions.Sin(num)) == 0.0)
		{
			return global::TransitionFunctions.Linear(t, a, b);
		}
		double b2 = global::TransitionFunctions.Sin((1.0 - t) * num) / num2;
		double b3 = global::TransitionFunctions.Sin(t * num) / num2;
		return global::TransitionFunctions.Sum(global::TransitionFunctions.Mul(a, b2), global::TransitionFunctions.Mul(b, b3));
	}

	// Token: 0x06002420 RID: 9248 RVA: 0x00085C30 File Offset: 0x00083E30
	public static Vector3G Normalize(Vector3G v)
	{
		double num = v.x * v.x + v.y * v.y + v.z * v.z;
		if (num == 0.0 || num == 1.0)
		{
			return v;
		}
		num = global::TransitionFunctions.Sqrt(num);
		Vector3G result;
		result.x = v.x / num;
		result.y = v.y / num;
		result.z = v.z / num;
		return result;
	}

	// Token: 0x06002421 RID: 9249 RVA: 0x00085CC8 File Offset: 0x00083EC8
	public static double AngleRadians(Vector3G a, Vector3G b)
	{
		double num = global::TransitionFunctions.DotNormal(a, b);
		if (num >= 1.0)
		{
			return 0.0;
		}
		if (num <= -1.0)
		{
			return 3.1415926535897931;
		}
		if (num == 0.0)
		{
			return 1.5707963267948966;
		}
		return global::TransitionFunctions.Acos(num);
	}

	// Token: 0x06002422 RID: 9250 RVA: 0x00085D30 File Offset: 0x00083F30
	public static double AngleDegrees(Vector3G a, Vector3G b)
	{
		double num = global::TransitionFunctions.DotNormal(a, b);
		if (num >= 1.0)
		{
			return 0.0;
		}
		if (num <= -1.0)
		{
			return 180.0;
		}
		if (num == 0.0)
		{
			return 90.0;
		}
		return global::TransitionFunctions.RadiansToDegrees(global::TransitionFunctions.Acos(num));
	}

	// Token: 0x06002423 RID: 9251 RVA: 0x00085D9C File Offset: 0x00083F9C
	public static Vector3G Vect(double x, double y, double z)
	{
		Vector3G result;
		result.x = x;
		result.y = y;
		result.z = z;
		return result;
	}

	// Token: 0x06002424 RID: 9252 RVA: 0x00085DC4 File Offset: 0x00083FC4
	public static double Length(Vector3G a)
	{
		return global::TransitionFunctions.Sqrt(a.x * a.x + a.y * a.y + a.z * a.z);
	}

	// Token: 0x06002425 RID: 9253 RVA: 0x00085E08 File Offset: 0x00084008
	public static Vector3G Cross(Vector3G a, Vector3G b)
	{
		Vector3G result;
		result.x = a.y * b.z - a.z * b.y;
		result.y = a.z * b.x - a.x * b.z;
		result.z = a.x * b.y - a.y * b.x;
		return result;
	}

	// Token: 0x06002426 RID: 9254 RVA: 0x00085E88 File Offset: 0x00084088
	public static double CrossDot(Vector3G a, Vector3G b, Vector3G dotB)
	{
		return (a.y * b.z - a.z * b.y) * dotB.x + (a.z * b.x - a.x * b.z) * dotB.y + (a.x * b.y - a.y * b.x) * dotB.z;
	}

	// Token: 0x06002427 RID: 9255 RVA: 0x00085F0C File Offset: 0x0008410C
	public static double Dot(Vector3G a, Vector3G b)
	{
		return a.x * b.x + a.y * b.y + a.z * b.z;
	}

	// Token: 0x06002428 RID: 9256 RVA: 0x00085F40 File Offset: 0x00084140
	public static double DotNormal(Vector3G a, Vector3G b)
	{
		return global::TransitionFunctions.Dot(global::TransitionFunctions.Normalize(a), global::TransitionFunctions.Normalize(b));
	}

	// Token: 0x06002429 RID: 9257 RVA: 0x00085F54 File Offset: 0x00084154
	public static Vector3G X3(double x)
	{
		Vector3G result;
		result.y = (result.z = 0.0);
		result.x = x;
		return result;
	}

	// Token: 0x0600242A RID: 9258 RVA: 0x00085F84 File Offset: 0x00084184
	public static Vector3G Y3(double y)
	{
		Vector3G result;
		result.x = (result.z = 0.0);
		result.y = y;
		return result;
	}

	// Token: 0x0600242B RID: 9259 RVA: 0x00085FB4 File Offset: 0x000841B4
	public static Vector3G Z3(double z)
	{
		Vector3G result;
		result.x = (result.y = 0.0);
		result.z = z;
		return result;
	}

	// Token: 0x0600242C RID: 9260 RVA: 0x00085FE4 File Offset: 0x000841E4
	public static Vector3G Scale3(double xyz)
	{
		Vector3G result;
		result.z = xyz;
		result.y = xyz;
		result.x = xyz;
		return result;
	}

	// Token: 0x0600242D RID: 9261 RVA: 0x00086010 File Offset: 0x00084210
	public static Vector4 Linear(float t, Vector4 a, Vector4 b)
	{
		return global::TransitionFunctions.Sum(global::TransitionFunctions.Mul(a, 1f - t), global::TransitionFunctions.Mul(b, t));
	}

	// Token: 0x0600242E RID: 9262 RVA: 0x0008602C File Offset: 0x0008422C
	public static Vector4 Round(float t, Vector4 a, Vector4 b)
	{
		return (t >= 0.5f) ? b : a;
	}

	// Token: 0x0600242F RID: 9263 RVA: 0x00086040 File Offset: 0x00084240
	public static Vector4 Ceil(float t, Vector4 a, Vector4 b)
	{
		return (t <= 0f) ? a : b;
	}

	// Token: 0x06002430 RID: 9264 RVA: 0x00086054 File Offset: 0x00084254
	public static Vector4 Floor(float t, Vector4 a, Vector4 b)
	{
		return (t >= 1f) ? b : a;
	}

	// Token: 0x06002431 RID: 9265 RVA: 0x00086068 File Offset: 0x00084268
	public static Vector4 Spline(float t, Vector4 a, Vector4 b)
	{
		return (t > 0f) ? ((t < 1f) ? global::TransitionFunctions.Linear(global::TransitionFunctions.SimpleSpline(t), a, b) : b) : a;
	}

	// Token: 0x06002432 RID: 9266 RVA: 0x0008609C File Offset: 0x0008429C
	public static Vector4 Evaluate(this global::TransitionFunction f, float t, Vector4 a, Vector4 b)
	{
		switch (f)
		{
		case global::TransitionFunction.Linear:
			return global::TransitionFunctions.Linear(t, a, b);
		case global::TransitionFunction.Round:
			return global::TransitionFunctions.Round(t, a, b);
		case global::TransitionFunction.Floor:
			return global::TransitionFunctions.Floor(t, a, b);
		case global::TransitionFunction.Ceil:
			return global::TransitionFunctions.Ceil(t, a, b);
		case global::TransitionFunction.Spline:
			return global::TransitionFunctions.Spline(t, a, b);
		default:
			throw new ArgumentOutOfRangeException("v", "Attempted use of unrecognized TransitionFunction enum value");
		}
	}

	// Token: 0x06002433 RID: 9267 RVA: 0x00086108 File Offset: 0x00084308
	public static Vector4 Evaluate(this global::TransitionFunction<Vector4> v, float t)
	{
		return v.f.Evaluate(t, v.a, v.b);
	}

	// Token: 0x06002434 RID: 9268 RVA: 0x00086128 File Offset: 0x00084328
	public static Vector4 Mul(Vector4 a, float b)
	{
		Vector4 result;
		result.x = a.x * b;
		result.y = a.y * b;
		result.z = a.z * b;
		result.w = a.w * b;
		return result;
	}

	// Token: 0x06002435 RID: 9269 RVA: 0x00086178 File Offset: 0x00084378
	public static Vector4 Sum(Vector4 a, Vector4 b)
	{
		Vector4 result;
		result.x = a.x + b.x;
		result.y = a.y + b.y;
		result.z = a.z + b.z;
		result.w = a.w * b.w;
		return result;
	}

	// Token: 0x06002436 RID: 9270 RVA: 0x000861E0 File Offset: 0x000843E0
	public static double Min(double a, double b)
	{
		return (b >= a) ? a : b;
	}

	// Token: 0x06002437 RID: 9271 RVA: 0x000861F0 File Offset: 0x000843F0
	public static double Max(double a, double b)
	{
		return (b <= a) ? a : b;
	}

	// Token: 0x06002438 RID: 9272 RVA: 0x00086200 File Offset: 0x00084400
	public static double Distance(double a, double b)
	{
		return (b <= a) ? (a - b) : (b - a);
	}

	// Token: 0x06002439 RID: 9273 RVA: 0x00086214 File Offset: 0x00084414
	public static double Evaluate(this global::TransitionFunction f, double t)
	{
		return f.Evaluate(t, 0.0, 1.0);
	}

	// Token: 0x0600243A RID: 9274 RVA: 0x00086230 File Offset: 0x00084430
	public static double Mul(double a, double b)
	{
		return a * b;
	}

	// Token: 0x0600243B RID: 9275 RVA: 0x00086238 File Offset: 0x00084438
	public static double Sum(double a, double b)
	{
		return a + b;
	}

	// Token: 0x0600243C RID: 9276 RVA: 0x00086240 File Offset: 0x00084440
	public static double Linear(double t, double a, double b)
	{
		return global::TransitionFunctions.Sum(global::TransitionFunctions.Mul(a, 1.0 - t), global::TransitionFunctions.Mul(b, t));
	}

	// Token: 0x0600243D RID: 9277 RVA: 0x00086260 File Offset: 0x00084460
	public static double Round(double t, double a, double b)
	{
		return (t >= 0.5) ? b : a;
	}

	// Token: 0x0600243E RID: 9278 RVA: 0x00086278 File Offset: 0x00084478
	public static double Ceil(double t, double a, double b)
	{
		return (t <= 0.0) ? a : b;
	}

	// Token: 0x0600243F RID: 9279 RVA: 0x00086290 File Offset: 0x00084490
	public static double Floor(double t, double a, double b)
	{
		return (t >= 1.0) ? b : a;
	}

	// Token: 0x06002440 RID: 9280 RVA: 0x000862A8 File Offset: 0x000844A8
	public static double Spline(double t, double a, double b)
	{
		return (t > 0.0) ? ((t < 1.0) ? global::TransitionFunctions.Linear(global::TransitionFunctions.SimpleSpline(t), a, b) : b) : a;
	}

	// Token: 0x06002441 RID: 9281 RVA: 0x000862E4 File Offset: 0x000844E4
	public static double Evaluate(this global::TransitionFunction f, double t, double a, double b)
	{
		switch (f)
		{
		case global::TransitionFunction.Linear:
			return global::TransitionFunctions.Linear(t, a, b);
		case global::TransitionFunction.Round:
			return global::TransitionFunctions.Round(t, a, b);
		case global::TransitionFunction.Floor:
			return global::TransitionFunctions.Floor(t, a, b);
		case global::TransitionFunction.Ceil:
			return global::TransitionFunctions.Ceil(t, a, b);
		case global::TransitionFunction.Spline:
			return global::TransitionFunctions.Spline(t, a, b);
		default:
			throw new ArgumentOutOfRangeException("v", "Attempted use of unrecognized TransitionFunction enum value");
		}
	}

	// Token: 0x06002442 RID: 9282 RVA: 0x00086350 File Offset: 0x00084550
	public static double Evaluate(this global::TransitionFunction<double> v, double t)
	{
		return v.f.Evaluate(t, v.a, v.b);
	}

	// Token: 0x06002443 RID: 9283 RVA: 0x00086370 File Offset: 0x00084570
	public static double Sin(double v)
	{
		return Math.Sin(v);
	}

	// Token: 0x06002444 RID: 9284 RVA: 0x00086378 File Offset: 0x00084578
	public static double Cos(double v)
	{
		return Math.Cos(v);
	}

	// Token: 0x06002445 RID: 9285 RVA: 0x00086380 File Offset: 0x00084580
	public static double Atan2(double y, double x)
	{
		return Math.Atan2(y, x);
	}

	// Token: 0x06002446 RID: 9286 RVA: 0x0008638C File Offset: 0x0008458C
	public static double Acos(double v)
	{
		return Math.Acos(v);
	}

	// Token: 0x06002447 RID: 9287 RVA: 0x00086394 File Offset: 0x00084594
	public static double Sqrt(double v)
	{
		return Math.Sqrt(v);
	}

	// Token: 0x06002448 RID: 9288 RVA: 0x0008639C File Offset: 0x0008459C
	public static double DegreesToRadians(double rads)
	{
		return 0.017453292519943295 * rads;
	}

	// Token: 0x06002449 RID: 9289 RVA: 0x000863AC File Offset: 0x000845AC
	public static double RadiansToDegrees(double degs)
	{
		return 57.295779513082323 * degs;
	}

	// Token: 0x0600244A RID: 9290 RVA: 0x000863BC File Offset: 0x000845BC
	private static double SimpleSpline(double v01)
	{
		return 3.0 * (v01 * v01) - 2.0 * (v01 * v01) * v01;
	}

	// Token: 0x0600244B RID: 9291 RVA: 0x000863DC File Offset: 0x000845DC
	public static double Linear(float t, double a, double b)
	{
		return global::TransitionFunctions.Sum(global::TransitionFunctions.Mul(a, (double)(1f - t)), global::TransitionFunctions.Mul(b, (double)t));
	}

	// Token: 0x0600244C RID: 9292 RVA: 0x000863FC File Offset: 0x000845FC
	public static double Round(float t, double a, double b)
	{
		return (t >= 0.5f) ? b : a;
	}

	// Token: 0x0600244D RID: 9293 RVA: 0x00086410 File Offset: 0x00084610
	public static double Ceil(float t, double a, double b)
	{
		return (t <= 0f) ? a : b;
	}

	// Token: 0x0600244E RID: 9294 RVA: 0x00086424 File Offset: 0x00084624
	public static double Floor(float t, double a, double b)
	{
		return (t >= 1f) ? b : a;
	}

	// Token: 0x0600244F RID: 9295 RVA: 0x00086438 File Offset: 0x00084638
	public static double Spline(float t, double a, double b)
	{
		return (t > 0f) ? ((t < 1f) ? global::TransitionFunctions.Linear(global::TransitionFunctions.SimpleSpline(t), a, b) : b) : a;
	}

	// Token: 0x06002450 RID: 9296 RVA: 0x0008646C File Offset: 0x0008466C
	public static double Evaluate(this global::TransitionFunction f, float t, double a, double b)
	{
		switch (f)
		{
		case global::TransitionFunction.Linear:
			return global::TransitionFunctions.Linear(t, a, b);
		case global::TransitionFunction.Round:
			return global::TransitionFunctions.Round(t, a, b);
		case global::TransitionFunction.Floor:
			return global::TransitionFunctions.Floor(t, a, b);
		case global::TransitionFunction.Ceil:
			return global::TransitionFunctions.Ceil(t, a, b);
		case global::TransitionFunction.Spline:
			return global::TransitionFunctions.Spline(t, a, b);
		default:
			throw new ArgumentOutOfRangeException("v", "Attempted use of unrecognized TransitionFunction enum value");
		}
	}

	// Token: 0x06002451 RID: 9297 RVA: 0x000864D8 File Offset: 0x000846D8
	public static double Evaluate(this global::TransitionFunction<double> v, float t)
	{
		return v.f.Evaluate(t, v.a, v.b);
	}

	// Token: 0x06002452 RID: 9298 RVA: 0x000864F8 File Offset: 0x000846F8
	public static float Min(float a, float b)
	{
		return (b >= a) ? a : b;
	}

	// Token: 0x06002453 RID: 9299 RVA: 0x00086508 File Offset: 0x00084708
	public static float Max(float a, float b)
	{
		return (b <= a) ? a : b;
	}

	// Token: 0x06002454 RID: 9300 RVA: 0x00086518 File Offset: 0x00084718
	public static float Distance(float a, float b)
	{
		return (b <= a) ? (a - b) : (b - a);
	}

	// Token: 0x06002455 RID: 9301 RVA: 0x0008652C File Offset: 0x0008472C
	public static float Evaluate(this global::TransitionFunction f, float t)
	{
		return f.Evaluate(t, 0f, 1f);
	}

	// Token: 0x06002456 RID: 9302 RVA: 0x00086540 File Offset: 0x00084740
	public static float Mul(float a, float b)
	{
		return a * b;
	}

	// Token: 0x06002457 RID: 9303 RVA: 0x00086548 File Offset: 0x00084748
	public static float Sum(float a, float b)
	{
		return a + b;
	}

	// Token: 0x06002458 RID: 9304 RVA: 0x00086550 File Offset: 0x00084750
	public static float Linear(float t, float a, float b)
	{
		return global::TransitionFunctions.Sum(global::TransitionFunctions.Mul(a, 1f - t), global::TransitionFunctions.Mul(b, t));
	}

	// Token: 0x06002459 RID: 9305 RVA: 0x0008656C File Offset: 0x0008476C
	public static float Round(float t, float a, float b)
	{
		return (t >= 0.5f) ? b : a;
	}

	// Token: 0x0600245A RID: 9306 RVA: 0x00086580 File Offset: 0x00084780
	public static float Ceil(float t, float a, float b)
	{
		return (t <= 0f) ? a : b;
	}

	// Token: 0x0600245B RID: 9307 RVA: 0x00086594 File Offset: 0x00084794
	public static float Floor(float t, float a, float b)
	{
		return (t >= 1f) ? b : a;
	}

	// Token: 0x0600245C RID: 9308 RVA: 0x000865A8 File Offset: 0x000847A8
	public static float Spline(float t, float a, float b)
	{
		return (t > 0f) ? ((t < 1f) ? global::TransitionFunctions.Linear(global::TransitionFunctions.SimpleSpline(t), a, b) : b) : a;
	}

	// Token: 0x0600245D RID: 9309 RVA: 0x000865DC File Offset: 0x000847DC
	public static float Evaluate(this global::TransitionFunction f, float t, float a, float b)
	{
		switch (f)
		{
		case global::TransitionFunction.Linear:
			return global::TransitionFunctions.Linear(t, a, b);
		case global::TransitionFunction.Round:
			return global::TransitionFunctions.Round(t, a, b);
		case global::TransitionFunction.Floor:
			return global::TransitionFunctions.Floor(t, a, b);
		case global::TransitionFunction.Ceil:
			return global::TransitionFunctions.Ceil(t, a, b);
		case global::TransitionFunction.Spline:
			return global::TransitionFunctions.Spline(t, a, b);
		default:
			throw new ArgumentOutOfRangeException("v", "Attempted use of unrecognized TransitionFunction enum value");
		}
	}

	// Token: 0x0600245E RID: 9310 RVA: 0x00086648 File Offset: 0x00084848
	public static float Evaluate(this global::TransitionFunction<float> v, float t)
	{
		return v.f.Evaluate(t, v.a, v.b);
	}

	// Token: 0x0600245F RID: 9311 RVA: 0x00086668 File Offset: 0x00084868
	public static float Sin(float v)
	{
		return Mathf.Sin(v);
	}

	// Token: 0x06002460 RID: 9312 RVA: 0x00086670 File Offset: 0x00084870
	public static float Cos(float v)
	{
		return Mathf.Cos(v);
	}

	// Token: 0x06002461 RID: 9313 RVA: 0x00086678 File Offset: 0x00084878
	public static float Atan2(float y, float x)
	{
		return Mathf.Atan2(y, x);
	}

	// Token: 0x06002462 RID: 9314 RVA: 0x00086684 File Offset: 0x00084884
	public static float Acos(float v)
	{
		return Mathf.Acos(v);
	}

	// Token: 0x06002463 RID: 9315 RVA: 0x0008668C File Offset: 0x0008488C
	public static float Sqrt(float v)
	{
		return Mathf.Sqrt(v);
	}

	// Token: 0x06002464 RID: 9316 RVA: 0x00086694 File Offset: 0x00084894
	public static float DegreesToRadians(float rads)
	{
		return 0.0174532924f * rads;
	}

	// Token: 0x06002465 RID: 9317 RVA: 0x000866A0 File Offset: 0x000848A0
	public static float RadiansToDegrees(float degs)
	{
		return 57.29578f * degs;
	}

	// Token: 0x06002466 RID: 9318 RVA: 0x000866AC File Offset: 0x000848AC
	private static float SimpleSpline(float v01)
	{
		return 3f * (v01 * v01) - 2f * (v01 * v01) * v01;
	}
}
