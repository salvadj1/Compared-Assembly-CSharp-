using System;
using Facepunch.Precision;
using UnityEngine;

// Token: 0x02000352 RID: 850
public static class TransitionFunctions
{
	// Token: 0x0600201F RID: 8223 RVA: 0x0007E324 File Offset: 0x0007C524
	public static Color Linear(float t, Color a, Color b)
	{
		return TransitionFunctions.Sum(TransitionFunctions.Mul(a, 1f - t), TransitionFunctions.Mul(b, t));
	}

	// Token: 0x06002020 RID: 8224 RVA: 0x0007E340 File Offset: 0x0007C540
	public static Color Round(float t, Color a, Color b)
	{
		return (t >= 0.5f) ? b : a;
	}

	// Token: 0x06002021 RID: 8225 RVA: 0x0007E354 File Offset: 0x0007C554
	public static Color Ceil(float t, Color a, Color b)
	{
		return (t <= 0f) ? a : b;
	}

	// Token: 0x06002022 RID: 8226 RVA: 0x0007E368 File Offset: 0x0007C568
	public static Color Floor(float t, Color a, Color b)
	{
		return (t >= 1f) ? b : a;
	}

	// Token: 0x06002023 RID: 8227 RVA: 0x0007E37C File Offset: 0x0007C57C
	public static Color Spline(float t, Color a, Color b)
	{
		return (t > 0f) ? ((t < 1f) ? TransitionFunctions.Linear(TransitionFunctions.SimpleSpline(t), a, b) : b) : a;
	}

	// Token: 0x06002024 RID: 8228 RVA: 0x0007E3B0 File Offset: 0x0007C5B0
	public static Color Evaluate(this TransitionFunction f, float t, Color a, Color b)
	{
		switch (f)
		{
		case TransitionFunction.Linear:
			return TransitionFunctions.Linear(t, a, b);
		case TransitionFunction.Round:
			return TransitionFunctions.Round(t, a, b);
		case TransitionFunction.Floor:
			return TransitionFunctions.Floor(t, a, b);
		case TransitionFunction.Ceil:
			return TransitionFunctions.Ceil(t, a, b);
		case TransitionFunction.Spline:
			return TransitionFunctions.Spline(t, a, b);
		default:
			throw new ArgumentOutOfRangeException("v", "Attempted use of unrecognized TransitionFunction enum value");
		}
	}

	// Token: 0x06002025 RID: 8229 RVA: 0x0007E41C File Offset: 0x0007C61C
	public static Color Evaluate(this TransitionFunction<Color> v, float t)
	{
		return v.f.Evaluate(t, v.a, v.b);
	}

	// Token: 0x06002026 RID: 8230 RVA: 0x0007E43C File Offset: 0x0007C63C
	public static Color Mul(Color a, float b)
	{
		Color result;
		result.r = a.r * b;
		result.g = a.g * b;
		result.b = a.b * b;
		result.a = a.a * b;
		return result;
	}

	// Token: 0x06002027 RID: 8231 RVA: 0x0007E48C File Offset: 0x0007C68C
	public static Color Sum(Color a, Color b)
	{
		Color result;
		result.r = a.r + b.r;
		result.g = a.g + b.g;
		result.b = a.b + b.b;
		result.a = a.a * b.a;
		return result;
	}

	// Token: 0x06002028 RID: 8232 RVA: 0x0007E4F4 File Offset: 0x0007C6F4
	public static Matrix4x4 Linear(float t, Matrix4x4 a, Matrix4x4 b)
	{
		return TransitionFunctions.Sum(TransitionFunctions.Mul(a, 1f - t), TransitionFunctions.Mul(b, t));
	}

	// Token: 0x06002029 RID: 8233 RVA: 0x0007E510 File Offset: 0x0007C710
	public static Matrix4x4 Round(float t, Matrix4x4 a, Matrix4x4 b)
	{
		return (t >= 0.5f) ? b : a;
	}

	// Token: 0x0600202A RID: 8234 RVA: 0x0007E524 File Offset: 0x0007C724
	public static Matrix4x4 Ceil(float t, Matrix4x4 a, Matrix4x4 b)
	{
		return (t <= 0f) ? a : b;
	}

	// Token: 0x0600202B RID: 8235 RVA: 0x0007E538 File Offset: 0x0007C738
	public static Matrix4x4 Floor(float t, Matrix4x4 a, Matrix4x4 b)
	{
		return (t >= 1f) ? b : a;
	}

	// Token: 0x0600202C RID: 8236 RVA: 0x0007E54C File Offset: 0x0007C74C
	public static Matrix4x4 Spline(float t, Matrix4x4 a, Matrix4x4 b)
	{
		return (t > 0f) ? ((t < 1f) ? TransitionFunctions.Linear(TransitionFunctions.SimpleSpline(t), a, b) : b) : a;
	}

	// Token: 0x0600202D RID: 8237 RVA: 0x0007E580 File Offset: 0x0007C780
	public static Matrix4x4 Evaluate(this TransitionFunction f, float t, Matrix4x4 a, Matrix4x4 b)
	{
		switch (f)
		{
		case TransitionFunction.Linear:
			return TransitionFunctions.Linear(t, a, b);
		case TransitionFunction.Round:
			return TransitionFunctions.Round(t, a, b);
		case TransitionFunction.Floor:
			return TransitionFunctions.Floor(t, a, b);
		case TransitionFunction.Ceil:
			return TransitionFunctions.Ceil(t, a, b);
		case TransitionFunction.Spline:
			return TransitionFunctions.Spline(t, a, b);
		default:
			throw new ArgumentOutOfRangeException("v", "Attempted use of unrecognized TransitionFunction enum value");
		}
	}

	// Token: 0x0600202E RID: 8238 RVA: 0x0007E5EC File Offset: 0x0007C7EC
	public static Matrix4x4 Evaluate(this TransitionFunction<Matrix4x4> v, float t)
	{
		return v.f.Evaluate(t, v.a, v.b);
	}

	// Token: 0x0600202F RID: 8239 RVA: 0x0007E60C File Offset: 0x0007C80C
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

	// Token: 0x06002030 RID: 8240 RVA: 0x0007E71C File Offset: 0x0007C91C
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

	// Token: 0x06002031 RID: 8241 RVA: 0x0007E88C File Offset: 0x0007CA8C
	public static Matrix4x4 Inverse(Matrix4x4 v)
	{
		return Matrix4x4.Inverse(v);
	}

	// Token: 0x06002032 RID: 8242 RVA: 0x0007E894 File Offset: 0x0007CA94
	public static Matrix4x4 Transpose(Matrix4x4 v)
	{
		return Matrix4x4.Transpose(v);
	}

	// Token: 0x06002033 RID: 8243 RVA: 0x0007E89C File Offset: 0x0007CA9C
	private static Vector3 GET_0X(Matrix4x4 a)
	{
		return TransitionFunctions.Vect(a.m00, a.m01, a.m02);
	}

	// Token: 0x06002034 RID: 8244 RVA: 0x0007E8B8 File Offset: 0x0007CAB8
	private static Vector3 GET_X0(Matrix4x4 a)
	{
		return TransitionFunctions.Vect(a.m00, a.m10, a.m20);
	}

	// Token: 0x06002035 RID: 8245 RVA: 0x0007E8D4 File Offset: 0x0007CAD4
	private static void SET_0X(ref Matrix4x4 m, Vector3 v)
	{
		m.m00 = v.x;
		m.m01 = v.y;
		m.m02 = v.z;
	}

	// Token: 0x06002036 RID: 8246 RVA: 0x0007E900 File Offset: 0x0007CB00
	private static void SET_X0(ref Matrix4x4 m, Vector3 v)
	{
		m.m00 = v.x;
		m.m10 = v.y;
		m.m20 = v.z;
	}

	// Token: 0x06002037 RID: 8247 RVA: 0x0007E92C File Offset: 0x0007CB2C
	private static Vector3 GET_1X(Matrix4x4 a)
	{
		return TransitionFunctions.Vect(a.m10, a.m11, a.m12);
	}

	// Token: 0x06002038 RID: 8248 RVA: 0x0007E948 File Offset: 0x0007CB48
	private static Vector3 GET_X1(Matrix4x4 a)
	{
		return TransitionFunctions.Vect(a.m01, a.m11, a.m21);
	}

	// Token: 0x06002039 RID: 8249 RVA: 0x0007E964 File Offset: 0x0007CB64
	private static void SET_1X(ref Matrix4x4 m, Vector3 v)
	{
		m.m10 = v.x;
		m.m11 = v.y;
		m.m12 = v.z;
	}

	// Token: 0x0600203A RID: 8250 RVA: 0x0007E990 File Offset: 0x0007CB90
	private static void SET_X1(ref Matrix4x4 m, Vector3 v)
	{
		m.m01 = v.x;
		m.m11 = v.y;
		m.m21 = v.z;
	}

	// Token: 0x0600203B RID: 8251 RVA: 0x0007E9BC File Offset: 0x0007CBBC
	private static Vector3 GET_2X(Matrix4x4 a)
	{
		return TransitionFunctions.Vect(a.m20, a.m21, a.m22);
	}

	// Token: 0x0600203C RID: 8252 RVA: 0x0007E9D8 File Offset: 0x0007CBD8
	private static Vector3 GET_X2(Matrix4x4 a)
	{
		return TransitionFunctions.Vect(a.m02, a.m12, a.m22);
	}

	// Token: 0x0600203D RID: 8253 RVA: 0x0007E9F4 File Offset: 0x0007CBF4
	private static void SET_2X(ref Matrix4x4 m, Vector3 v)
	{
		m.m20 = v.x;
		m.m21 = v.y;
		m.m22 = v.z;
	}

	// Token: 0x0600203E RID: 8254 RVA: 0x0007EA20 File Offset: 0x0007CC20
	private static void SET_X2(ref Matrix4x4 m, Vector3 v)
	{
		m.m02 = v.x;
		m.m12 = v.y;
		m.m22 = v.z;
	}

	// Token: 0x0600203F RID: 8255 RVA: 0x0007EA4C File Offset: 0x0007CC4C
	private static Vector3 GET_3X(Matrix4x4 a)
	{
		return TransitionFunctions.Vect(a.m30, a.m31, a.m32);
	}

	// Token: 0x06002040 RID: 8256 RVA: 0x0007EA68 File Offset: 0x0007CC68
	private static Vector3 GET_X3(Matrix4x4 a)
	{
		return TransitionFunctions.Vect(a.m03, a.m13, a.m23);
	}

	// Token: 0x06002041 RID: 8257 RVA: 0x0007EA84 File Offset: 0x0007CC84
	private static void SET_3X(ref Matrix4x4 m, Vector3 v)
	{
		m.m30 = v.x;
		m.m31 = v.y;
		m.m32 = v.z;
	}

	// Token: 0x06002042 RID: 8258 RVA: 0x0007EAB0 File Offset: 0x0007CCB0
	private static void SET_X3(ref Matrix4x4 m, Vector3 v)
	{
		m.m03 = v.x;
		m.m13 = v.y;
		m.m23 = v.z;
	}

	// Token: 0x06002043 RID: 8259 RVA: 0x0007EADC File Offset: 0x0007CCDC
	private static Vector3 DIR_X(Matrix4x4 a)
	{
		return TransitionFunctions.GET_X0(a);
	}

	// Token: 0x06002044 RID: 8260 RVA: 0x0007EAE4 File Offset: 0x0007CCE4
	private static void DIR_X(ref Matrix4x4 a, Vector3 v)
	{
		TransitionFunctions.SET_X0(ref a, v);
	}

	// Token: 0x06002045 RID: 8261 RVA: 0x0007EAF0 File Offset: 0x0007CCF0
	private static Vector3 DIR_Y(Matrix4x4 a)
	{
		return TransitionFunctions.GET_X1(a);
	}

	// Token: 0x06002046 RID: 8262 RVA: 0x0007EAF8 File Offset: 0x0007CCF8
	private static void DIR_Y(ref Matrix4x4 a, Vector3 v)
	{
		TransitionFunctions.SET_X1(ref a, v);
	}

	// Token: 0x06002047 RID: 8263 RVA: 0x0007EB04 File Offset: 0x0007CD04
	private static Vector3 DIR_Z(Matrix4x4 a)
	{
		return TransitionFunctions.GET_X2(a);
	}

	// Token: 0x06002048 RID: 8264 RVA: 0x0007EB0C File Offset: 0x0007CD0C
	private static void DIR_Z(ref Matrix4x4 a, Vector3 v)
	{
		TransitionFunctions.SET_X2(ref a, v);
	}

	// Token: 0x06002049 RID: 8265 RVA: 0x0007EB18 File Offset: 0x0007CD18
	private static Vector3 TRANS(Matrix4x4 a)
	{
		return TransitionFunctions.GET_X3(a);
	}

	// Token: 0x0600204A RID: 8266 RVA: 0x0007EB20 File Offset: 0x0007CD20
	private static void TRANS(ref Matrix4x4 a, Vector3 v)
	{
		TransitionFunctions.SET_X3(ref a, v);
	}

	// Token: 0x0600204B RID: 8267 RVA: 0x0007EB2C File Offset: 0x0007CD2C
	private static Vector3 SCALE(Matrix4x4 a)
	{
		return TransitionFunctions.GET_3X(a);
	}

	// Token: 0x0600204C RID: 8268 RVA: 0x0007EB34 File Offset: 0x0007CD34
	private static void SCALE(ref Matrix4x4 a, Vector3 v)
	{
		TransitionFunctions.SET_3X(ref a, v);
	}

	// Token: 0x0600204D RID: 8269 RVA: 0x0007EB40 File Offset: 0x0007CD40
	private static Vector3 SLERP(float t, Vector3 a, Vector3 b)
	{
		return TransitionFunctions.Slerp(t, a, b);
	}

	// Token: 0x0600204E RID: 8270 RVA: 0x0007EB4C File Offset: 0x0007CD4C
	private static Vector3 LLERP(float t, Vector3 a, Vector3 b)
	{
		return TransitionFunctions.Linear(t, a, b);
	}

	// Token: 0x0600204F RID: 8271 RVA: 0x0007EB58 File Offset: 0x0007CD58
	public static Matrix4x4 Slerp(float t, Matrix4x4 a, Matrix4x4 b)
	{
		Matrix4x4 identity = Matrix4x4.identity;
		Vector3 vector = TransitionFunctions.Slerp(t, TransitionFunctions.DIR_X(a), TransitionFunctions.DIR_X(b));
		Vector3 vector2 = TransitionFunctions.Slerp(t, TransitionFunctions.DIR_Y(a), TransitionFunctions.DIR_Y(b));
		Vector3 vector3 = TransitionFunctions.Slerp(t, TransitionFunctions.DIR_Z(a), TransitionFunctions.DIR_Z(b));
		Quaternion rotation = TransitionFunctions.LookRotation(vector3, vector2);
		vector2 = TransitionFunctions.Rotate(rotation, TransitionFunctions.Y3(TransitionFunctions.Length(vector2)));
		if (TransitionFunctions.CrossDot(vector3, vector2, vector) > 0f)
		{
			vector = TransitionFunctions.Rotate(rotation, TransitionFunctions.X3(-TransitionFunctions.Length(vector)));
		}
		else
		{
			vector = TransitionFunctions.Rotate(rotation, TransitionFunctions.X3(TransitionFunctions.Length(vector)));
		}
		TransitionFunctions.DIR_X(ref identity, vector);
		TransitionFunctions.DIR_Y(ref identity, vector2);
		TransitionFunctions.DIR_Z(ref identity, vector3);
		TransitionFunctions.SCALE(ref identity, TransitionFunctions.Linear(t, TransitionFunctions.SCALE(a), TransitionFunctions.SCALE(b)));
		TransitionFunctions.TRANS(ref identity, TransitionFunctions.Linear(t, TransitionFunctions.TRANS(a), TransitionFunctions.TRANS(b)));
		identity.m33 = TransitionFunctions.Linear(t, a.m33, b.m33);
		return identity;
	}

	// Token: 0x06002050 RID: 8272 RVA: 0x0007EC64 File Offset: 0x0007CE64
	public static Matrix4x4 SlerpWorldToCamera(float t, Matrix4x4 a, Matrix4x4 b)
	{
		return TransitionFunctions.Slerp(t, a.inverse, b.inverse).inverse;
	}

	// Token: 0x06002051 RID: 8273 RVA: 0x0007EC90 File Offset: 0x0007CE90
	public static Matrix4x4G Linear(double t, Matrix4x4G a, Matrix4x4G b)
	{
		return TransitionFunctions.Sum(TransitionFunctions.Mul(a, 1.0 - t), TransitionFunctions.Mul(b, t));
	}

	// Token: 0x06002052 RID: 8274 RVA: 0x0007ECB0 File Offset: 0x0007CEB0
	public static Matrix4x4G Round(double t, Matrix4x4G a, Matrix4x4G b)
	{
		return (t >= 0.5) ? b : a;
	}

	// Token: 0x06002053 RID: 8275 RVA: 0x0007ECC8 File Offset: 0x0007CEC8
	public static Matrix4x4G Ceil(double t, Matrix4x4G a, Matrix4x4G b)
	{
		return (t <= 0.0) ? a : b;
	}

	// Token: 0x06002054 RID: 8276 RVA: 0x0007ECE0 File Offset: 0x0007CEE0
	public static Matrix4x4G Floor(double t, Matrix4x4G a, Matrix4x4G b)
	{
		return (t >= 1.0) ? b : a;
	}

	// Token: 0x06002055 RID: 8277 RVA: 0x0007ECF8 File Offset: 0x0007CEF8
	public static Matrix4x4G Spline(double t, Matrix4x4G a, Matrix4x4G b)
	{
		return (t > 0.0) ? ((t < 1.0) ? TransitionFunctions.Linear(TransitionFunctions.SimpleSpline(t), a, b) : b) : a;
	}

	// Token: 0x06002056 RID: 8278 RVA: 0x0007ED34 File Offset: 0x0007CF34
	public static Matrix4x4G Evaluate(this TransitionFunction f, double t, Matrix4x4G a, Matrix4x4G b)
	{
		switch (f)
		{
		case TransitionFunction.Linear:
			return TransitionFunctions.Linear(t, a, b);
		case TransitionFunction.Round:
			return TransitionFunctions.Round(t, a, b);
		case TransitionFunction.Floor:
			return TransitionFunctions.Floor(t, a, b);
		case TransitionFunction.Ceil:
			return TransitionFunctions.Ceil(t, a, b);
		case TransitionFunction.Spline:
			return TransitionFunctions.Spline(t, a, b);
		default:
			throw new ArgumentOutOfRangeException("v", "Attempted use of unrecognized TransitionFunction enum value");
		}
	}

	// Token: 0x06002057 RID: 8279 RVA: 0x0007EDA0 File Offset: 0x0007CFA0
	public static Matrix4x4G Evaluate(this TransitionFunction<Matrix4x4G> v, double t)
	{
		return v.f.Evaluate(t, v.a, v.b);
	}

	// Token: 0x06002058 RID: 8280 RVA: 0x0007EDC0 File Offset: 0x0007CFC0
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

	// Token: 0x06002059 RID: 8281 RVA: 0x0007EED0 File Offset: 0x0007D0D0
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

	// Token: 0x0600205A RID: 8282 RVA: 0x0007F040 File Offset: 0x0007D240
	public static Matrix4x4G Inverse(Matrix4x4G v)
	{
		Matrix4x4G result;
		Matrix4x4G.Inverse(ref v, ref result);
		return result;
	}

	// Token: 0x0600205B RID: 8283 RVA: 0x0007F058 File Offset: 0x0007D258
	public static Matrix4x4G Transpose(Matrix4x4G v)
	{
		Matrix4x4G result;
		Matrix4x4G.Transpose(ref v, ref result);
		return result;
	}

	// Token: 0x0600205C RID: 8284 RVA: 0x0007F070 File Offset: 0x0007D270
	private static Vector3G VECT3F(double x, double y, double z)
	{
		Vector3G result;
		result.x = x;
		result.y = y;
		result.z = z;
		return result;
	}

	// Token: 0x0600205D RID: 8285 RVA: 0x0007F098 File Offset: 0x0007D298
	private static Vector3G GET_0X(Matrix4x4G a)
	{
		return TransitionFunctions.VECT3F(a.m00, a.m01, a.m02);
	}

	// Token: 0x0600205E RID: 8286 RVA: 0x0007F0B4 File Offset: 0x0007D2B4
	private static Vector3G GET_X0(Matrix4x4G a)
	{
		return TransitionFunctions.VECT3F(a.m00, a.m10, a.m20);
	}

	// Token: 0x0600205F RID: 8287 RVA: 0x0007F0D0 File Offset: 0x0007D2D0
	private static void SET_0X(ref Matrix4x4G m, Vector3G v)
	{
		m.m00 = v.x;
		m.m01 = v.y;
		m.m02 = v.z;
	}

	// Token: 0x06002060 RID: 8288 RVA: 0x0007F0FC File Offset: 0x0007D2FC
	private static void SET_X0(ref Matrix4x4G m, Vector3G v)
	{
		m.m00 = v.x;
		m.m10 = v.y;
		m.m20 = v.z;
	}

	// Token: 0x06002061 RID: 8289 RVA: 0x0007F128 File Offset: 0x0007D328
	private static Vector3G GET_1X(Matrix4x4G a)
	{
		return TransitionFunctions.VECT3F(a.m10, a.m11, a.m12);
	}

	// Token: 0x06002062 RID: 8290 RVA: 0x0007F144 File Offset: 0x0007D344
	private static Vector3G GET_X1(Matrix4x4G a)
	{
		return TransitionFunctions.VECT3F(a.m01, a.m11, a.m21);
	}

	// Token: 0x06002063 RID: 8291 RVA: 0x0007F160 File Offset: 0x0007D360
	private static void SET_1X(ref Matrix4x4G m, Vector3G v)
	{
		m.m10 = v.x;
		m.m11 = v.y;
		m.m12 = v.z;
	}

	// Token: 0x06002064 RID: 8292 RVA: 0x0007F18C File Offset: 0x0007D38C
	private static void SET_X1(ref Matrix4x4G m, Vector3G v)
	{
		m.m01 = v.x;
		m.m11 = v.y;
		m.m21 = v.z;
	}

	// Token: 0x06002065 RID: 8293 RVA: 0x0007F1B8 File Offset: 0x0007D3B8
	private static Vector3G GET_2X(Matrix4x4G a)
	{
		return TransitionFunctions.VECT3F(a.m20, a.m21, a.m22);
	}

	// Token: 0x06002066 RID: 8294 RVA: 0x0007F1D4 File Offset: 0x0007D3D4
	private static Vector3G GET_X2(Matrix4x4G a)
	{
		return TransitionFunctions.VECT3F(a.m02, a.m12, a.m22);
	}

	// Token: 0x06002067 RID: 8295 RVA: 0x0007F1F0 File Offset: 0x0007D3F0
	private static void SET_2X(ref Matrix4x4G m, Vector3G v)
	{
		m.m20 = v.x;
		m.m21 = v.y;
		m.m22 = v.z;
	}

	// Token: 0x06002068 RID: 8296 RVA: 0x0007F21C File Offset: 0x0007D41C
	private static void SET_X2(ref Matrix4x4G m, Vector3G v)
	{
		m.m02 = v.x;
		m.m12 = v.y;
		m.m22 = v.z;
	}

	// Token: 0x06002069 RID: 8297 RVA: 0x0007F248 File Offset: 0x0007D448
	private static Vector3G GET_3X(Matrix4x4G a)
	{
		return TransitionFunctions.VECT3F(a.m30, a.m31, a.m32);
	}

	// Token: 0x0600206A RID: 8298 RVA: 0x0007F264 File Offset: 0x0007D464
	private static Vector3G GET_X3(Matrix4x4G a)
	{
		return TransitionFunctions.VECT3F(a.m03, a.m13, a.m23);
	}

	// Token: 0x0600206B RID: 8299 RVA: 0x0007F280 File Offset: 0x0007D480
	private static void SET_3X(ref Matrix4x4G m, Vector3G v)
	{
		m.m30 = v.x;
		m.m31 = v.y;
		m.m32 = v.z;
	}

	// Token: 0x0600206C RID: 8300 RVA: 0x0007F2AC File Offset: 0x0007D4AC
	private static void SET_X3(ref Matrix4x4G m, Vector3G v)
	{
		m.m03 = v.x;
		m.m13 = v.y;
		m.m23 = v.z;
	}

	// Token: 0x0600206D RID: 8301 RVA: 0x0007F2D8 File Offset: 0x0007D4D8
	private static Vector3G DIR_X(Matrix4x4G a)
	{
		return TransitionFunctions.GET_X0(a);
	}

	// Token: 0x0600206E RID: 8302 RVA: 0x0007F2E0 File Offset: 0x0007D4E0
	private static void DIR_X(ref Matrix4x4G a, Vector3G v)
	{
		TransitionFunctions.SET_X0(ref a, v);
	}

	// Token: 0x0600206F RID: 8303 RVA: 0x0007F2EC File Offset: 0x0007D4EC
	private static Vector3G DIR_Y(Matrix4x4G a)
	{
		return TransitionFunctions.GET_X1(a);
	}

	// Token: 0x06002070 RID: 8304 RVA: 0x0007F2F4 File Offset: 0x0007D4F4
	private static void DIR_Y(ref Matrix4x4G a, Vector3G v)
	{
		TransitionFunctions.SET_X1(ref a, v);
	}

	// Token: 0x06002071 RID: 8305 RVA: 0x0007F300 File Offset: 0x0007D500
	private static Vector3G DIR_Z(Matrix4x4G a)
	{
		return TransitionFunctions.GET_X2(a);
	}

	// Token: 0x06002072 RID: 8306 RVA: 0x0007F308 File Offset: 0x0007D508
	private static void DIR_Z(ref Matrix4x4G a, Vector3G v)
	{
		TransitionFunctions.SET_X2(ref a, v);
	}

	// Token: 0x06002073 RID: 8307 RVA: 0x0007F314 File Offset: 0x0007D514
	private static Vector3G TRANS(Matrix4x4G a)
	{
		return TransitionFunctions.GET_X3(a);
	}

	// Token: 0x06002074 RID: 8308 RVA: 0x0007F31C File Offset: 0x0007D51C
	private static void TRANS(ref Matrix4x4G a, Vector3G v)
	{
		TransitionFunctions.SET_X3(ref a, v);
	}

	// Token: 0x06002075 RID: 8309 RVA: 0x0007F328 File Offset: 0x0007D528
	private static Vector3G SCALE(Matrix4x4G a)
	{
		return TransitionFunctions.GET_3X(a);
	}

	// Token: 0x06002076 RID: 8310 RVA: 0x0007F330 File Offset: 0x0007D530
	private static void SCALE(ref Matrix4x4G a, Vector3G v)
	{
		TransitionFunctions.SET_3X(ref a, v);
	}

	// Token: 0x06002077 RID: 8311 RVA: 0x0007F33C File Offset: 0x0007D53C
	private static Vector3G SLERP(double t, Vector3G a, Vector3G b)
	{
		return TransitionFunctions.Slerp(t, a, b);
	}

	// Token: 0x06002078 RID: 8312 RVA: 0x0007F348 File Offset: 0x0007D548
	private static Vector3G LLERP(double t, Vector3G a, Vector3G b)
	{
		return TransitionFunctions.Linear(t, a, b);
	}

	// Token: 0x06002079 RID: 8313 RVA: 0x0007F354 File Offset: 0x0007D554
	public static Matrix4x4G Slerp(double t, Matrix4x4G a, Matrix4x4G b)
	{
		Matrix4x4G identity = Matrix4x4G.identity;
		Vector3G vector3G = TransitionFunctions.Slerp(t, TransitionFunctions.DIR_X(a), TransitionFunctions.DIR_X(b));
		Vector3G vector3G2 = TransitionFunctions.Slerp(t, TransitionFunctions.DIR_Y(a), TransitionFunctions.DIR_Y(b));
		Vector3G vector3G3 = TransitionFunctions.Slerp(t, TransitionFunctions.DIR_Z(a), TransitionFunctions.DIR_Z(b));
		QuaternionG rotation = TransitionFunctions.LookRotation(vector3G3, vector3G2);
		vector3G2 = TransitionFunctions.Rotate(rotation, TransitionFunctions.Y3(TransitionFunctions.Length(vector3G2)));
		if (TransitionFunctions.CrossDot(vector3G3, vector3G2, vector3G) > 0.0)
		{
			vector3G = TransitionFunctions.Rotate(rotation, TransitionFunctions.X3(-TransitionFunctions.Length(vector3G)));
		}
		else
		{
			vector3G = TransitionFunctions.Rotate(rotation, TransitionFunctions.X3(TransitionFunctions.Length(vector3G)));
		}
		TransitionFunctions.DIR_X(ref identity, vector3G);
		TransitionFunctions.DIR_Y(ref identity, vector3G2);
		TransitionFunctions.DIR_Z(ref identity, vector3G3);
		TransitionFunctions.SCALE(ref identity, TransitionFunctions.Linear(t, TransitionFunctions.SCALE(a), TransitionFunctions.SCALE(b)));
		TransitionFunctions.TRANS(ref identity, TransitionFunctions.Linear(t, TransitionFunctions.TRANS(a), TransitionFunctions.TRANS(b)));
		identity.m33 = TransitionFunctions.Linear(t, a.m33, b.m33);
		return identity;
	}

	// Token: 0x0600207A RID: 8314 RVA: 0x0007F464 File Offset: 0x0007D664
	public static Matrix4x4G SlerpWorldToCamera(double t, Matrix4x4G a, Matrix4x4G b)
	{
		return TransitionFunctions.Inverse(TransitionFunctions.Slerp(t, TransitionFunctions.Inverse(a), TransitionFunctions.Inverse(b)));
	}

	// Token: 0x0600207B RID: 8315 RVA: 0x0007F480 File Offset: 0x0007D680
	public static Quaternion Round(float t, Quaternion a, Quaternion b)
	{
		return (t >= 0.5f) ? b : a;
	}

	// Token: 0x0600207C RID: 8316 RVA: 0x0007F494 File Offset: 0x0007D694
	public static Quaternion Ceil(float t, Quaternion a, Quaternion b)
	{
		return (t <= 0f) ? a : b;
	}

	// Token: 0x0600207D RID: 8317 RVA: 0x0007F4A8 File Offset: 0x0007D6A8
	public static Quaternion Floor(float t, Quaternion a, Quaternion b)
	{
		return (t >= 1f) ? b : a;
	}

	// Token: 0x0600207E RID: 8318 RVA: 0x0007F4BC File Offset: 0x0007D6BC
	public static Quaternion Spline(float t, Quaternion a, Quaternion b)
	{
		return (t > 0f) ? ((t < 1f) ? TransitionFunctions.Linear(TransitionFunctions.SimpleSpline(t), a, b) : b) : a;
	}

	// Token: 0x0600207F RID: 8319 RVA: 0x0007F4F0 File Offset: 0x0007D6F0
	public static Quaternion Evaluate(this TransitionFunction f, float t, Quaternion a, Quaternion b)
	{
		switch (f)
		{
		case TransitionFunction.Linear:
			return TransitionFunctions.Linear(t, a, b);
		case TransitionFunction.Round:
			return TransitionFunctions.Round(t, a, b);
		case TransitionFunction.Floor:
			return TransitionFunctions.Floor(t, a, b);
		case TransitionFunction.Ceil:
			return TransitionFunctions.Ceil(t, a, b);
		case TransitionFunction.Spline:
			return TransitionFunctions.Spline(t, a, b);
		default:
			throw new ArgumentOutOfRangeException("v", "Attempted use of unrecognized TransitionFunction enum value");
		}
	}

	// Token: 0x06002080 RID: 8320 RVA: 0x0007F55C File Offset: 0x0007D75C
	public static Quaternion Evaluate(this TransitionFunction<Quaternion> v, float t)
	{
		return v.f.Evaluate(t, v.a, v.b);
	}

	// Token: 0x06002081 RID: 8321 RVA: 0x0007F57C File Offset: 0x0007D77C
	public static Quaternion Mul(Quaternion a, float b)
	{
		Quaternion result;
		result.x = a.x * b;
		result.y = a.y * b;
		result.z = a.z * b;
		result.w = a.w * b;
		return result;
	}

	// Token: 0x06002082 RID: 8322 RVA: 0x0007F5CC File Offset: 0x0007D7CC
	public static Quaternion Sum(Quaternion a, Quaternion b)
	{
		Quaternion result;
		result.x = a.x + b.x;
		result.y = a.y + b.y;
		result.z = a.z + b.z;
		result.w = a.w * b.w;
		return result;
	}

	// Token: 0x06002083 RID: 8323 RVA: 0x0007F634 File Offset: 0x0007D834
	public static Quaternion Linear(float t, Quaternion a, Quaternion b)
	{
		return TransitionFunctions.Slerp(t, a, b);
	}

	// Token: 0x06002084 RID: 8324 RVA: 0x0007F640 File Offset: 0x0007D840
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
				num = TransitionFunctions.Acos(-num);
				float num2 = TransitionFunctions.Sin(num);
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
					float num4 = TransitionFunctions.Sin(num * t);
					float num3 = TransitionFunctions.Sin(num * (1f - t));
					result.x = (a.x * num3 - b.x * num4) / num2;
					result.y = (a.y * num3 - b.y * num4) / num2;
					result.z = (a.z * num3 - b.z * num4) / num2;
					result.w = (a.w * num3 - b.w * num4) / num2;
				}
			}
			else
			{
				num = TransitionFunctions.Acos(num);
				float num2 = TransitionFunctions.Sin(num);
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
					float num4 = TransitionFunctions.Sin(num * t);
					float num3 = TransitionFunctions.Sin(num * (1f - t));
					result.x = (a.x * num3 + b.x * num4) / num2;
					result.y = (a.y * num3 + b.y * num4) / num2;
					result.z = (a.z * num3 + b.z * num4) / num2;
					result.w = (a.w * num3 + b.w * num4) / num2;
				}
			}
		}
		return result;
	}

	// Token: 0x06002085 RID: 8325 RVA: 0x0007F924 File Offset: 0x0007DB24
	public static Quaternion LookRotation(Vector3 forward, Vector3 up)
	{
		return Quaternion.LookRotation(forward, up);
	}

	// Token: 0x06002086 RID: 8326 RVA: 0x0007F930 File Offset: 0x0007DB30
	public static Vector3 Rotate(Quaternion rotation, Vector3 vector)
	{
		return rotation * vector;
	}

	// Token: 0x06002087 RID: 8327 RVA: 0x0007F93C File Offset: 0x0007DB3C
	public static QuaternionG Round(double t, QuaternionG a, QuaternionG b)
	{
		return (t >= 0.5) ? b : a;
	}

	// Token: 0x06002088 RID: 8328 RVA: 0x0007F954 File Offset: 0x0007DB54
	public static QuaternionG Ceil(double t, QuaternionG a, QuaternionG b)
	{
		return (t <= 0.0) ? a : b;
	}

	// Token: 0x06002089 RID: 8329 RVA: 0x0007F96C File Offset: 0x0007DB6C
	public static QuaternionG Floor(double t, QuaternionG a, QuaternionG b)
	{
		return (t >= 1.0) ? b : a;
	}

	// Token: 0x0600208A RID: 8330 RVA: 0x0007F984 File Offset: 0x0007DB84
	public static QuaternionG Spline(double t, QuaternionG a, QuaternionG b)
	{
		return (t > 0.0) ? ((t < 1.0) ? TransitionFunctions.Linear(TransitionFunctions.SimpleSpline(t), a, b) : b) : a;
	}

	// Token: 0x0600208B RID: 8331 RVA: 0x0007F9C0 File Offset: 0x0007DBC0
	public static QuaternionG Evaluate(this TransitionFunction f, double t, QuaternionG a, QuaternionG b)
	{
		switch (f)
		{
		case TransitionFunction.Linear:
			return TransitionFunctions.Linear(t, a, b);
		case TransitionFunction.Round:
			return TransitionFunctions.Round(t, a, b);
		case TransitionFunction.Floor:
			return TransitionFunctions.Floor(t, a, b);
		case TransitionFunction.Ceil:
			return TransitionFunctions.Ceil(t, a, b);
		case TransitionFunction.Spline:
			return TransitionFunctions.Spline(t, a, b);
		default:
			throw new ArgumentOutOfRangeException("v", "Attempted use of unrecognized TransitionFunction enum value");
		}
	}

	// Token: 0x0600208C RID: 8332 RVA: 0x0007FA2C File Offset: 0x0007DC2C
	public static QuaternionG Evaluate(this TransitionFunction<QuaternionG> v, double t)
	{
		return v.f.Evaluate(t, v.a, v.b);
	}

	// Token: 0x0600208D RID: 8333 RVA: 0x0007FA4C File Offset: 0x0007DC4C
	public static QuaternionG Mul(QuaternionG a, double b)
	{
		QuaternionG result;
		result.x = a.x * b;
		result.y = a.y * b;
		result.z = a.z * b;
		result.w = a.w * b;
		return result;
	}

	// Token: 0x0600208E RID: 8334 RVA: 0x0007FA9C File Offset: 0x0007DC9C
	public static QuaternionG Sum(QuaternionG a, QuaternionG b)
	{
		QuaternionG result;
		result.x = a.x + b.x;
		result.y = a.y + b.y;
		result.z = a.z + b.z;
		result.w = a.w * b.w;
		return result;
	}

	// Token: 0x0600208F RID: 8335 RVA: 0x0007FB04 File Offset: 0x0007DD04
	public static QuaternionG Linear(double t, QuaternionG a, QuaternionG b)
	{
		return TransitionFunctions.Slerp(t, a, b);
	}

	// Token: 0x06002090 RID: 8336 RVA: 0x0007FB10 File Offset: 0x0007DD10
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
				num = TransitionFunctions.Acos(-num);
				double num2 = TransitionFunctions.Sin(num);
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
					double num4 = TransitionFunctions.Sin(num * t);
					double num3 = TransitionFunctions.Sin(num * (1.0 - t));
					result.x = (a.x * num3 - b.x * num4) / num2;
					result.y = (a.y * num3 - b.y * num4) / num2;
					result.z = (a.z * num3 - b.z * num4) / num2;
					result.w = (a.w * num3 - b.w * num4) / num2;
				}
			}
			else
			{
				num = TransitionFunctions.Acos(num);
				double num2 = TransitionFunctions.Sin(num);
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
					double num4 = TransitionFunctions.Sin(num * t);
					double num3 = TransitionFunctions.Sin(num * (1.0 - t));
					result.x = (a.x * num3 + b.x * num4) / num2;
					result.y = (a.y * num3 + b.y * num4) / num2;
					result.z = (a.z * num3 + b.z * num4) / num2;
					result.w = (a.w * num3 + b.w * num4) / num2;
				}
			}
		}
		return result;
	}

	// Token: 0x06002091 RID: 8337 RVA: 0x0007FE1C File Offset: 0x0007E01C
	public static QuaternionG LookRotation(Vector3G forward, Vector3G up)
	{
		QuaternionG result;
		QuaternionG.LookRotation(ref forward, ref up, ref result);
		return result;
	}

	// Token: 0x06002092 RID: 8338 RVA: 0x0007FE38 File Offset: 0x0007E038
	public static Vector3G Rotate(QuaternionG rotation, Vector3G vector)
	{
		Vector3G result;
		QuaternionG.Mult(ref rotation, ref vector, ref result);
		return result;
	}

	// Token: 0x06002093 RID: 8339 RVA: 0x0007FE54 File Offset: 0x0007E054
	public static Vector2 Linear(float t, Vector2 a, Vector2 b)
	{
		return TransitionFunctions.Sum(TransitionFunctions.Mul(a, 1f - t), TransitionFunctions.Mul(b, t));
	}

	// Token: 0x06002094 RID: 8340 RVA: 0x0007FE70 File Offset: 0x0007E070
	public static Vector2 Round(float t, Vector2 a, Vector2 b)
	{
		return (t >= 0.5f) ? b : a;
	}

	// Token: 0x06002095 RID: 8341 RVA: 0x0007FE84 File Offset: 0x0007E084
	public static Vector2 Ceil(float t, Vector2 a, Vector2 b)
	{
		return (t <= 0f) ? a : b;
	}

	// Token: 0x06002096 RID: 8342 RVA: 0x0007FE98 File Offset: 0x0007E098
	public static Vector2 Floor(float t, Vector2 a, Vector2 b)
	{
		return (t >= 1f) ? b : a;
	}

	// Token: 0x06002097 RID: 8343 RVA: 0x0007FEAC File Offset: 0x0007E0AC
	public static Vector2 Spline(float t, Vector2 a, Vector2 b)
	{
		return (t > 0f) ? ((t < 1f) ? TransitionFunctions.Linear(TransitionFunctions.SimpleSpline(t), a, b) : b) : a;
	}

	// Token: 0x06002098 RID: 8344 RVA: 0x0007FEE0 File Offset: 0x0007E0E0
	public static Vector2 Evaluate(this TransitionFunction f, float t, Vector2 a, Vector2 b)
	{
		switch (f)
		{
		case TransitionFunction.Linear:
			return TransitionFunctions.Linear(t, a, b);
		case TransitionFunction.Round:
			return TransitionFunctions.Round(t, a, b);
		case TransitionFunction.Floor:
			return TransitionFunctions.Floor(t, a, b);
		case TransitionFunction.Ceil:
			return TransitionFunctions.Ceil(t, a, b);
		case TransitionFunction.Spline:
			return TransitionFunctions.Spline(t, a, b);
		default:
			throw new ArgumentOutOfRangeException("v", "Attempted use of unrecognized TransitionFunction enum value");
		}
	}

	// Token: 0x06002099 RID: 8345 RVA: 0x0007FF4C File Offset: 0x0007E14C
	public static Vector2 Evaluate(this TransitionFunction<Vector2> v, float t)
	{
		return v.f.Evaluate(t, v.a, v.b);
	}

	// Token: 0x0600209A RID: 8346 RVA: 0x0007FF6C File Offset: 0x0007E16C
	public static Vector2 Mul(Vector2 a, float b)
	{
		Vector2 result;
		result.x = a.x * b;
		result.y = a.y * b;
		return result;
	}

	// Token: 0x0600209B RID: 8347 RVA: 0x0007FF9C File Offset: 0x0007E19C
	public static Vector2 Sum(Vector2 a, Vector2 b)
	{
		Vector2 result;
		result.x = a.x + b.x;
		result.y = a.y + b.y;
		return result;
	}

	// Token: 0x0600209C RID: 8348 RVA: 0x0007FFD8 File Offset: 0x0007E1D8
	public static Vector2 Slerp(float t, Vector2 a, Vector2 b)
	{
		float num = TransitionFunctions.DegreesToRadians(Vector2.Angle(a, b));
		float num2;
		if (num == 0f || (num2 = TransitionFunctions.Sin(num)) == 0f)
		{
			return TransitionFunctions.Linear(t, a, b);
		}
		float b2 = TransitionFunctions.Sin((1f - t) * num) / num2;
		float b3 = TransitionFunctions.Sin(t * num) / num2;
		return TransitionFunctions.Sum(TransitionFunctions.Mul(a, b2), TransitionFunctions.Mul(b, b3));
	}

	// Token: 0x0600209D RID: 8349 RVA: 0x0008004C File Offset: 0x0007E24C
	public static Vector3 Linear(float t, Vector3 a, Vector3 b)
	{
		return TransitionFunctions.Sum(TransitionFunctions.Mul(a, 1f - t), TransitionFunctions.Mul(b, t));
	}

	// Token: 0x0600209E RID: 8350 RVA: 0x00080068 File Offset: 0x0007E268
	public static Vector3 Round(float t, Vector3 a, Vector3 b)
	{
		return (t >= 0.5f) ? b : a;
	}

	// Token: 0x0600209F RID: 8351 RVA: 0x0008007C File Offset: 0x0007E27C
	public static Vector3 Ceil(float t, Vector3 a, Vector3 b)
	{
		return (t <= 0f) ? a : b;
	}

	// Token: 0x060020A0 RID: 8352 RVA: 0x00080090 File Offset: 0x0007E290
	public static Vector3 Floor(float t, Vector3 a, Vector3 b)
	{
		return (t >= 1f) ? b : a;
	}

	// Token: 0x060020A1 RID: 8353 RVA: 0x000800A4 File Offset: 0x0007E2A4
	public static Vector3 Spline(float t, Vector3 a, Vector3 b)
	{
		return (t > 0f) ? ((t < 1f) ? TransitionFunctions.Linear(TransitionFunctions.SimpleSpline(t), a, b) : b) : a;
	}

	// Token: 0x060020A2 RID: 8354 RVA: 0x000800D8 File Offset: 0x0007E2D8
	public static Vector3 Evaluate(this TransitionFunction f, float t, Vector3 a, Vector3 b)
	{
		switch (f)
		{
		case TransitionFunction.Linear:
			return TransitionFunctions.Linear(t, a, b);
		case TransitionFunction.Round:
			return TransitionFunctions.Round(t, a, b);
		case TransitionFunction.Floor:
			return TransitionFunctions.Floor(t, a, b);
		case TransitionFunction.Ceil:
			return TransitionFunctions.Ceil(t, a, b);
		case TransitionFunction.Spline:
			return TransitionFunctions.Spline(t, a, b);
		default:
			throw new ArgumentOutOfRangeException("v", "Attempted use of unrecognized TransitionFunction enum value");
		}
	}

	// Token: 0x060020A3 RID: 8355 RVA: 0x00080144 File Offset: 0x0007E344
	public static Vector3 Evaluate(this TransitionFunction<Vector3> v, float t)
	{
		return v.f.Evaluate(t, v.a, v.b);
	}

	// Token: 0x060020A4 RID: 8356 RVA: 0x00080164 File Offset: 0x0007E364
	public static Vector3 Mul(Vector3 a, float b)
	{
		Vector3 result;
		result.x = a.x * b;
		result.y = a.y * b;
		result.z = a.z * b;
		return result;
	}

	// Token: 0x060020A5 RID: 8357 RVA: 0x000801A4 File Offset: 0x0007E3A4
	public static Vector3 Sum(Vector3 a, Vector3 b)
	{
		Vector3 result;
		result.x = a.x + b.x;
		result.y = a.y + b.y;
		result.z = a.z + b.z;
		return result;
	}

	// Token: 0x060020A6 RID: 8358 RVA: 0x000801F4 File Offset: 0x0007E3F4
	public static Vector3 Slerp(float t, Vector3 a, Vector3 b)
	{
		float num = TransitionFunctions.AngleRadians(a, b);
		float num2;
		if (num == 0f || (num2 = TransitionFunctions.Sin(num)) == 0f)
		{
			return TransitionFunctions.Linear(t, a, b);
		}
		float b2 = TransitionFunctions.Sin((1f - t) * num) / num2;
		float b3 = TransitionFunctions.Sin(t * num) / num2;
		return TransitionFunctions.Sum(TransitionFunctions.Mul(a, b2), TransitionFunctions.Mul(b, b3));
	}

	// Token: 0x060020A7 RID: 8359 RVA: 0x00080260 File Offset: 0x0007E460
	public static Vector3 Normalize(Vector3 v)
	{
		float num = v.x * v.x + v.y * v.y + v.z * v.z;
		if (num == 0f || num == 1f)
		{
			return v;
		}
		num = TransitionFunctions.Sqrt(num);
		Vector3 result;
		result.x = v.x / num;
		result.y = v.y / num;
		result.z = v.z / num;
		return result;
	}

	// Token: 0x060020A8 RID: 8360 RVA: 0x000802F0 File Offset: 0x0007E4F0
	public static float AngleRadians(Vector3 a, Vector3 b)
	{
		float num = TransitionFunctions.DotNormal(a, b);
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
		return TransitionFunctions.Acos(num);
	}

	// Token: 0x060020A9 RID: 8361 RVA: 0x00080340 File Offset: 0x0007E540
	public static float AngleDegrees(Vector3 a, Vector3 b)
	{
		float num = TransitionFunctions.DotNormal(a, b);
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
		return TransitionFunctions.RadiansToDegrees(TransitionFunctions.Acos(num));
	}

	// Token: 0x060020AA RID: 8362 RVA: 0x00080394 File Offset: 0x0007E594
	public static Vector3 Vect(float x, float y, float z)
	{
		Vector3 result;
		result.x = x;
		result.y = y;
		result.z = z;
		return result;
	}

	// Token: 0x060020AB RID: 8363 RVA: 0x000803BC File Offset: 0x0007E5BC
	public static float Length(Vector3 a)
	{
		return TransitionFunctions.Sqrt(a.x * a.x + a.y * a.y + a.z * a.z);
	}

	// Token: 0x060020AC RID: 8364 RVA: 0x00080400 File Offset: 0x0007E600
	public static Vector3 Cross(Vector3 a, Vector3 b)
	{
		Vector3 result;
		result.x = a.y * b.z - a.z * b.y;
		result.y = a.z * b.x - a.x * b.z;
		result.z = a.x * b.y - a.y * b.x;
		return result;
	}

	// Token: 0x060020AD RID: 8365 RVA: 0x00080480 File Offset: 0x0007E680
	public static float CrossDot(Vector3 a, Vector3 b, Vector3 dotB)
	{
		return (a.y * b.z - a.z * b.y) * dotB.x + (a.z * b.x - a.x * b.z) * dotB.y + (a.x * b.y - a.y * b.x) * dotB.z;
	}

	// Token: 0x060020AE RID: 8366 RVA: 0x00080504 File Offset: 0x0007E704
	public static float Dot(Vector3 a, Vector3 b)
	{
		return a.x * b.x + a.y * b.y + a.z * b.z;
	}

	// Token: 0x060020AF RID: 8367 RVA: 0x00080538 File Offset: 0x0007E738
	public static float DotNormal(Vector3 a, Vector3 b)
	{
		return TransitionFunctions.Dot(TransitionFunctions.Normalize(a), TransitionFunctions.Normalize(b));
	}

	// Token: 0x060020B0 RID: 8368 RVA: 0x0008054C File Offset: 0x0007E74C
	public static Vector3 X3(float x)
	{
		Vector3 result;
		result.y = (result.z = 0f);
		result.x = x;
		return result;
	}

	// Token: 0x060020B1 RID: 8369 RVA: 0x00080578 File Offset: 0x0007E778
	public static Vector3 Y3(float y)
	{
		Vector3 result;
		result.x = (result.z = 0f);
		result.y = y;
		return result;
	}

	// Token: 0x060020B2 RID: 8370 RVA: 0x000805A4 File Offset: 0x0007E7A4
	public static Vector3 Z3(float z)
	{
		Vector3 result;
		result.x = (result.y = 0f);
		result.z = z;
		return result;
	}

	// Token: 0x060020B3 RID: 8371 RVA: 0x000805D0 File Offset: 0x0007E7D0
	public static Vector3 Scale3(float xyz)
	{
		Vector3 result;
		result.z = xyz;
		result.y = xyz;
		result.x = xyz;
		return result;
	}

	// Token: 0x060020B4 RID: 8372 RVA: 0x000805FC File Offset: 0x0007E7FC
	public static Vector3G Linear(double t, Vector3G a, Vector3G b)
	{
		return TransitionFunctions.Sum(TransitionFunctions.Mul(a, 1.0 - t), TransitionFunctions.Mul(b, t));
	}

	// Token: 0x060020B5 RID: 8373 RVA: 0x0008061C File Offset: 0x0007E81C
	public static Vector3G Round(double t, Vector3G a, Vector3G b)
	{
		return (t >= 0.5) ? b : a;
	}

	// Token: 0x060020B6 RID: 8374 RVA: 0x00080634 File Offset: 0x0007E834
	public static Vector3G Ceil(double t, Vector3G a, Vector3G b)
	{
		return (t <= 0.0) ? a : b;
	}

	// Token: 0x060020B7 RID: 8375 RVA: 0x0008064C File Offset: 0x0007E84C
	public static Vector3G Floor(double t, Vector3G a, Vector3G b)
	{
		return (t >= 1.0) ? b : a;
	}

	// Token: 0x060020B8 RID: 8376 RVA: 0x00080664 File Offset: 0x0007E864
	public static Vector3G Spline(double t, Vector3G a, Vector3G b)
	{
		return (t > 0.0) ? ((t < 1.0) ? TransitionFunctions.Linear(TransitionFunctions.SimpleSpline(t), a, b) : b) : a;
	}

	// Token: 0x060020B9 RID: 8377 RVA: 0x000806A0 File Offset: 0x0007E8A0
	public static Vector3G Evaluate(this TransitionFunction f, double t, Vector3G a, Vector3G b)
	{
		switch (f)
		{
		case TransitionFunction.Linear:
			return TransitionFunctions.Linear(t, a, b);
		case TransitionFunction.Round:
			return TransitionFunctions.Round(t, a, b);
		case TransitionFunction.Floor:
			return TransitionFunctions.Floor(t, a, b);
		case TransitionFunction.Ceil:
			return TransitionFunctions.Ceil(t, a, b);
		case TransitionFunction.Spline:
			return TransitionFunctions.Spline(t, a, b);
		default:
			throw new ArgumentOutOfRangeException("v", "Attempted use of unrecognized TransitionFunction enum value");
		}
	}

	// Token: 0x060020BA RID: 8378 RVA: 0x0008070C File Offset: 0x0007E90C
	public static Vector3G Evaluate(this TransitionFunction<Vector3G> v, double t)
	{
		return v.f.Evaluate(t, v.a, v.b);
	}

	// Token: 0x060020BB RID: 8379 RVA: 0x0008072C File Offset: 0x0007E92C
	public static Vector3G Mul(Vector3G a, double b)
	{
		Vector3G result;
		result.x = a.x * b;
		result.y = a.y * b;
		result.z = a.z * b;
		return result;
	}

	// Token: 0x060020BC RID: 8380 RVA: 0x0008076C File Offset: 0x0007E96C
	public static Vector3G Sum(Vector3G a, Vector3G b)
	{
		Vector3G result;
		result.x = a.x + b.x;
		result.y = a.y + b.y;
		result.z = a.z + b.z;
		return result;
	}

	// Token: 0x060020BD RID: 8381 RVA: 0x000807BC File Offset: 0x0007E9BC
	public static Vector3G Slerp(double t, Vector3G a, Vector3G b)
	{
		double num = TransitionFunctions.AngleRadians(a, b);
		double num2;
		if (num == 0.0 || (num2 = TransitionFunctions.Sin(num)) == 0.0)
		{
			return TransitionFunctions.Linear(t, a, b);
		}
		double b2 = TransitionFunctions.Sin((1.0 - t) * num) / num2;
		double b3 = TransitionFunctions.Sin(t * num) / num2;
		return TransitionFunctions.Sum(TransitionFunctions.Mul(a, b2), TransitionFunctions.Mul(b, b3));
	}

	// Token: 0x060020BE RID: 8382 RVA: 0x00080834 File Offset: 0x0007EA34
	public static Vector3G Normalize(Vector3G v)
	{
		double num = v.x * v.x + v.y * v.y + v.z * v.z;
		if (num == 0.0 || num == 1.0)
		{
			return v;
		}
		num = TransitionFunctions.Sqrt(num);
		Vector3G result;
		result.x = v.x / num;
		result.y = v.y / num;
		result.z = v.z / num;
		return result;
	}

	// Token: 0x060020BF RID: 8383 RVA: 0x000808CC File Offset: 0x0007EACC
	public static double AngleRadians(Vector3G a, Vector3G b)
	{
		double num = TransitionFunctions.DotNormal(a, b);
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
		return TransitionFunctions.Acos(num);
	}

	// Token: 0x060020C0 RID: 8384 RVA: 0x00080934 File Offset: 0x0007EB34
	public static double AngleDegrees(Vector3G a, Vector3G b)
	{
		double num = TransitionFunctions.DotNormal(a, b);
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
		return TransitionFunctions.RadiansToDegrees(TransitionFunctions.Acos(num));
	}

	// Token: 0x060020C1 RID: 8385 RVA: 0x000809A0 File Offset: 0x0007EBA0
	public static Vector3G Vect(double x, double y, double z)
	{
		Vector3G result;
		result.x = x;
		result.y = y;
		result.z = z;
		return result;
	}

	// Token: 0x060020C2 RID: 8386 RVA: 0x000809C8 File Offset: 0x0007EBC8
	public static double Length(Vector3G a)
	{
		return TransitionFunctions.Sqrt(a.x * a.x + a.y * a.y + a.z * a.z);
	}

	// Token: 0x060020C3 RID: 8387 RVA: 0x00080A0C File Offset: 0x0007EC0C
	public static Vector3G Cross(Vector3G a, Vector3G b)
	{
		Vector3G result;
		result.x = a.y * b.z - a.z * b.y;
		result.y = a.z * b.x - a.x * b.z;
		result.z = a.x * b.y - a.y * b.x;
		return result;
	}

	// Token: 0x060020C4 RID: 8388 RVA: 0x00080A8C File Offset: 0x0007EC8C
	public static double CrossDot(Vector3G a, Vector3G b, Vector3G dotB)
	{
		return (a.y * b.z - a.z * b.y) * dotB.x + (a.z * b.x - a.x * b.z) * dotB.y + (a.x * b.y - a.y * b.x) * dotB.z;
	}

	// Token: 0x060020C5 RID: 8389 RVA: 0x00080B10 File Offset: 0x0007ED10
	public static double Dot(Vector3G a, Vector3G b)
	{
		return a.x * b.x + a.y * b.y + a.z * b.z;
	}

	// Token: 0x060020C6 RID: 8390 RVA: 0x00080B44 File Offset: 0x0007ED44
	public static double DotNormal(Vector3G a, Vector3G b)
	{
		return TransitionFunctions.Dot(TransitionFunctions.Normalize(a), TransitionFunctions.Normalize(b));
	}

	// Token: 0x060020C7 RID: 8391 RVA: 0x00080B58 File Offset: 0x0007ED58
	public static Vector3G X3(double x)
	{
		Vector3G result;
		result.y = (result.z = 0.0);
		result.x = x;
		return result;
	}

	// Token: 0x060020C8 RID: 8392 RVA: 0x00080B88 File Offset: 0x0007ED88
	public static Vector3G Y3(double y)
	{
		Vector3G result;
		result.x = (result.z = 0.0);
		result.y = y;
		return result;
	}

	// Token: 0x060020C9 RID: 8393 RVA: 0x00080BB8 File Offset: 0x0007EDB8
	public static Vector3G Z3(double z)
	{
		Vector3G result;
		result.x = (result.y = 0.0);
		result.z = z;
		return result;
	}

	// Token: 0x060020CA RID: 8394 RVA: 0x00080BE8 File Offset: 0x0007EDE8
	public static Vector3G Scale3(double xyz)
	{
		Vector3G result;
		result.z = xyz;
		result.y = xyz;
		result.x = xyz;
		return result;
	}

	// Token: 0x060020CB RID: 8395 RVA: 0x00080C14 File Offset: 0x0007EE14
	public static Vector4 Linear(float t, Vector4 a, Vector4 b)
	{
		return TransitionFunctions.Sum(TransitionFunctions.Mul(a, 1f - t), TransitionFunctions.Mul(b, t));
	}

	// Token: 0x060020CC RID: 8396 RVA: 0x00080C30 File Offset: 0x0007EE30
	public static Vector4 Round(float t, Vector4 a, Vector4 b)
	{
		return (t >= 0.5f) ? b : a;
	}

	// Token: 0x060020CD RID: 8397 RVA: 0x00080C44 File Offset: 0x0007EE44
	public static Vector4 Ceil(float t, Vector4 a, Vector4 b)
	{
		return (t <= 0f) ? a : b;
	}

	// Token: 0x060020CE RID: 8398 RVA: 0x00080C58 File Offset: 0x0007EE58
	public static Vector4 Floor(float t, Vector4 a, Vector4 b)
	{
		return (t >= 1f) ? b : a;
	}

	// Token: 0x060020CF RID: 8399 RVA: 0x00080C6C File Offset: 0x0007EE6C
	public static Vector4 Spline(float t, Vector4 a, Vector4 b)
	{
		return (t > 0f) ? ((t < 1f) ? TransitionFunctions.Linear(TransitionFunctions.SimpleSpline(t), a, b) : b) : a;
	}

	// Token: 0x060020D0 RID: 8400 RVA: 0x00080CA0 File Offset: 0x0007EEA0
	public static Vector4 Evaluate(this TransitionFunction f, float t, Vector4 a, Vector4 b)
	{
		switch (f)
		{
		case TransitionFunction.Linear:
			return TransitionFunctions.Linear(t, a, b);
		case TransitionFunction.Round:
			return TransitionFunctions.Round(t, a, b);
		case TransitionFunction.Floor:
			return TransitionFunctions.Floor(t, a, b);
		case TransitionFunction.Ceil:
			return TransitionFunctions.Ceil(t, a, b);
		case TransitionFunction.Spline:
			return TransitionFunctions.Spline(t, a, b);
		default:
			throw new ArgumentOutOfRangeException("v", "Attempted use of unrecognized TransitionFunction enum value");
		}
	}

	// Token: 0x060020D1 RID: 8401 RVA: 0x00080D0C File Offset: 0x0007EF0C
	public static Vector4 Evaluate(this TransitionFunction<Vector4> v, float t)
	{
		return v.f.Evaluate(t, v.a, v.b);
	}

	// Token: 0x060020D2 RID: 8402 RVA: 0x00080D2C File Offset: 0x0007EF2C
	public static Vector4 Mul(Vector4 a, float b)
	{
		Vector4 result;
		result.x = a.x * b;
		result.y = a.y * b;
		result.z = a.z * b;
		result.w = a.w * b;
		return result;
	}

	// Token: 0x060020D3 RID: 8403 RVA: 0x00080D7C File Offset: 0x0007EF7C
	public static Vector4 Sum(Vector4 a, Vector4 b)
	{
		Vector4 result;
		result.x = a.x + b.x;
		result.y = a.y + b.y;
		result.z = a.z + b.z;
		result.w = a.w * b.w;
		return result;
	}

	// Token: 0x060020D4 RID: 8404 RVA: 0x00080DE4 File Offset: 0x0007EFE4
	public static double Min(double a, double b)
	{
		return (b >= a) ? a : b;
	}

	// Token: 0x060020D5 RID: 8405 RVA: 0x00080DF4 File Offset: 0x0007EFF4
	public static double Max(double a, double b)
	{
		return (b <= a) ? a : b;
	}

	// Token: 0x060020D6 RID: 8406 RVA: 0x00080E04 File Offset: 0x0007F004
	public static double Distance(double a, double b)
	{
		return (b <= a) ? (a - b) : (b - a);
	}

	// Token: 0x060020D7 RID: 8407 RVA: 0x00080E18 File Offset: 0x0007F018
	public static double Evaluate(this TransitionFunction f, double t)
	{
		return f.Evaluate(t, 0.0, 1.0);
	}

	// Token: 0x060020D8 RID: 8408 RVA: 0x00080E34 File Offset: 0x0007F034
	public static double Mul(double a, double b)
	{
		return a * b;
	}

	// Token: 0x060020D9 RID: 8409 RVA: 0x00080E3C File Offset: 0x0007F03C
	public static double Sum(double a, double b)
	{
		return a + b;
	}

	// Token: 0x060020DA RID: 8410 RVA: 0x00080E44 File Offset: 0x0007F044
	public static double Linear(double t, double a, double b)
	{
		return TransitionFunctions.Sum(TransitionFunctions.Mul(a, 1.0 - t), TransitionFunctions.Mul(b, t));
	}

	// Token: 0x060020DB RID: 8411 RVA: 0x00080E64 File Offset: 0x0007F064
	public static double Round(double t, double a, double b)
	{
		return (t >= 0.5) ? b : a;
	}

	// Token: 0x060020DC RID: 8412 RVA: 0x00080E7C File Offset: 0x0007F07C
	public static double Ceil(double t, double a, double b)
	{
		return (t <= 0.0) ? a : b;
	}

	// Token: 0x060020DD RID: 8413 RVA: 0x00080E94 File Offset: 0x0007F094
	public static double Floor(double t, double a, double b)
	{
		return (t >= 1.0) ? b : a;
	}

	// Token: 0x060020DE RID: 8414 RVA: 0x00080EAC File Offset: 0x0007F0AC
	public static double Spline(double t, double a, double b)
	{
		return (t > 0.0) ? ((t < 1.0) ? TransitionFunctions.Linear(TransitionFunctions.SimpleSpline(t), a, b) : b) : a;
	}

	// Token: 0x060020DF RID: 8415 RVA: 0x00080EE8 File Offset: 0x0007F0E8
	public static double Evaluate(this TransitionFunction f, double t, double a, double b)
	{
		switch (f)
		{
		case TransitionFunction.Linear:
			return TransitionFunctions.Linear(t, a, b);
		case TransitionFunction.Round:
			return TransitionFunctions.Round(t, a, b);
		case TransitionFunction.Floor:
			return TransitionFunctions.Floor(t, a, b);
		case TransitionFunction.Ceil:
			return TransitionFunctions.Ceil(t, a, b);
		case TransitionFunction.Spline:
			return TransitionFunctions.Spline(t, a, b);
		default:
			throw new ArgumentOutOfRangeException("v", "Attempted use of unrecognized TransitionFunction enum value");
		}
	}

	// Token: 0x060020E0 RID: 8416 RVA: 0x00080F54 File Offset: 0x0007F154
	public static double Evaluate(this TransitionFunction<double> v, double t)
	{
		return v.f.Evaluate(t, v.a, v.b);
	}

	// Token: 0x060020E1 RID: 8417 RVA: 0x00080F74 File Offset: 0x0007F174
	public static double Sin(double v)
	{
		return Math.Sin(v);
	}

	// Token: 0x060020E2 RID: 8418 RVA: 0x00080F7C File Offset: 0x0007F17C
	public static double Cos(double v)
	{
		return Math.Cos(v);
	}

	// Token: 0x060020E3 RID: 8419 RVA: 0x00080F84 File Offset: 0x0007F184
	public static double Atan2(double y, double x)
	{
		return Math.Atan2(y, x);
	}

	// Token: 0x060020E4 RID: 8420 RVA: 0x00080F90 File Offset: 0x0007F190
	public static double Acos(double v)
	{
		return Math.Acos(v);
	}

	// Token: 0x060020E5 RID: 8421 RVA: 0x00080F98 File Offset: 0x0007F198
	public static double Sqrt(double v)
	{
		return Math.Sqrt(v);
	}

	// Token: 0x060020E6 RID: 8422 RVA: 0x00080FA0 File Offset: 0x0007F1A0
	public static double DegreesToRadians(double rads)
	{
		return 0.017453292519943295 * rads;
	}

	// Token: 0x060020E7 RID: 8423 RVA: 0x00080FB0 File Offset: 0x0007F1B0
	public static double RadiansToDegrees(double degs)
	{
		return 57.295779513082323 * degs;
	}

	// Token: 0x060020E8 RID: 8424 RVA: 0x00080FC0 File Offset: 0x0007F1C0
	private static double SimpleSpline(double v01)
	{
		return 3.0 * (v01 * v01) - 2.0 * (v01 * v01) * v01;
	}

	// Token: 0x060020E9 RID: 8425 RVA: 0x00080FE0 File Offset: 0x0007F1E0
	public static double Linear(float t, double a, double b)
	{
		return TransitionFunctions.Sum(TransitionFunctions.Mul(a, (double)(1f - t)), TransitionFunctions.Mul(b, (double)t));
	}

	// Token: 0x060020EA RID: 8426 RVA: 0x00081000 File Offset: 0x0007F200
	public static double Round(float t, double a, double b)
	{
		return (t >= 0.5f) ? b : a;
	}

	// Token: 0x060020EB RID: 8427 RVA: 0x00081014 File Offset: 0x0007F214
	public static double Ceil(float t, double a, double b)
	{
		return (t <= 0f) ? a : b;
	}

	// Token: 0x060020EC RID: 8428 RVA: 0x00081028 File Offset: 0x0007F228
	public static double Floor(float t, double a, double b)
	{
		return (t >= 1f) ? b : a;
	}

	// Token: 0x060020ED RID: 8429 RVA: 0x0008103C File Offset: 0x0007F23C
	public static double Spline(float t, double a, double b)
	{
		return (t > 0f) ? ((t < 1f) ? TransitionFunctions.Linear(TransitionFunctions.SimpleSpline(t), a, b) : b) : a;
	}

	// Token: 0x060020EE RID: 8430 RVA: 0x00081070 File Offset: 0x0007F270
	public static double Evaluate(this TransitionFunction f, float t, double a, double b)
	{
		switch (f)
		{
		case TransitionFunction.Linear:
			return TransitionFunctions.Linear(t, a, b);
		case TransitionFunction.Round:
			return TransitionFunctions.Round(t, a, b);
		case TransitionFunction.Floor:
			return TransitionFunctions.Floor(t, a, b);
		case TransitionFunction.Ceil:
			return TransitionFunctions.Ceil(t, a, b);
		case TransitionFunction.Spline:
			return TransitionFunctions.Spline(t, a, b);
		default:
			throw new ArgumentOutOfRangeException("v", "Attempted use of unrecognized TransitionFunction enum value");
		}
	}

	// Token: 0x060020EF RID: 8431 RVA: 0x000810DC File Offset: 0x0007F2DC
	public static double Evaluate(this TransitionFunction<double> v, float t)
	{
		return v.f.Evaluate(t, v.a, v.b);
	}

	// Token: 0x060020F0 RID: 8432 RVA: 0x000810FC File Offset: 0x0007F2FC
	public static float Min(float a, float b)
	{
		return (b >= a) ? a : b;
	}

	// Token: 0x060020F1 RID: 8433 RVA: 0x0008110C File Offset: 0x0007F30C
	public static float Max(float a, float b)
	{
		return (b <= a) ? a : b;
	}

	// Token: 0x060020F2 RID: 8434 RVA: 0x0008111C File Offset: 0x0007F31C
	public static float Distance(float a, float b)
	{
		return (b <= a) ? (a - b) : (b - a);
	}

	// Token: 0x060020F3 RID: 8435 RVA: 0x00081130 File Offset: 0x0007F330
	public static float Evaluate(this TransitionFunction f, float t)
	{
		return f.Evaluate(t, 0f, 1f);
	}

	// Token: 0x060020F4 RID: 8436 RVA: 0x00081144 File Offset: 0x0007F344
	public static float Mul(float a, float b)
	{
		return a * b;
	}

	// Token: 0x060020F5 RID: 8437 RVA: 0x0008114C File Offset: 0x0007F34C
	public static float Sum(float a, float b)
	{
		return a + b;
	}

	// Token: 0x060020F6 RID: 8438 RVA: 0x00081154 File Offset: 0x0007F354
	public static float Linear(float t, float a, float b)
	{
		return TransitionFunctions.Sum(TransitionFunctions.Mul(a, 1f - t), TransitionFunctions.Mul(b, t));
	}

	// Token: 0x060020F7 RID: 8439 RVA: 0x00081170 File Offset: 0x0007F370
	public static float Round(float t, float a, float b)
	{
		return (t >= 0.5f) ? b : a;
	}

	// Token: 0x060020F8 RID: 8440 RVA: 0x00081184 File Offset: 0x0007F384
	public static float Ceil(float t, float a, float b)
	{
		return (t <= 0f) ? a : b;
	}

	// Token: 0x060020F9 RID: 8441 RVA: 0x00081198 File Offset: 0x0007F398
	public static float Floor(float t, float a, float b)
	{
		return (t >= 1f) ? b : a;
	}

	// Token: 0x060020FA RID: 8442 RVA: 0x000811AC File Offset: 0x0007F3AC
	public static float Spline(float t, float a, float b)
	{
		return (t > 0f) ? ((t < 1f) ? TransitionFunctions.Linear(TransitionFunctions.SimpleSpline(t), a, b) : b) : a;
	}

	// Token: 0x060020FB RID: 8443 RVA: 0x000811E0 File Offset: 0x0007F3E0
	public static float Evaluate(this TransitionFunction f, float t, float a, float b)
	{
		switch (f)
		{
		case TransitionFunction.Linear:
			return TransitionFunctions.Linear(t, a, b);
		case TransitionFunction.Round:
			return TransitionFunctions.Round(t, a, b);
		case TransitionFunction.Floor:
			return TransitionFunctions.Floor(t, a, b);
		case TransitionFunction.Ceil:
			return TransitionFunctions.Ceil(t, a, b);
		case TransitionFunction.Spline:
			return TransitionFunctions.Spline(t, a, b);
		default:
			throw new ArgumentOutOfRangeException("v", "Attempted use of unrecognized TransitionFunction enum value");
		}
	}

	// Token: 0x060020FC RID: 8444 RVA: 0x0008124C File Offset: 0x0007F44C
	public static float Evaluate(this TransitionFunction<float> v, float t)
	{
		return v.f.Evaluate(t, v.a, v.b);
	}

	// Token: 0x060020FD RID: 8445 RVA: 0x0008126C File Offset: 0x0007F46C
	public static float Sin(float v)
	{
		return Mathf.Sin(v);
	}

	// Token: 0x060020FE RID: 8446 RVA: 0x00081274 File Offset: 0x0007F474
	public static float Cos(float v)
	{
		return Mathf.Cos(v);
	}

	// Token: 0x060020FF RID: 8447 RVA: 0x0008127C File Offset: 0x0007F47C
	public static float Atan2(float y, float x)
	{
		return Mathf.Atan2(y, x);
	}

	// Token: 0x06002100 RID: 8448 RVA: 0x00081288 File Offset: 0x0007F488
	public static float Acos(float v)
	{
		return Mathf.Acos(v);
	}

	// Token: 0x06002101 RID: 8449 RVA: 0x00081290 File Offset: 0x0007F490
	public static float Sqrt(float v)
	{
		return Mathf.Sqrt(v);
	}

	// Token: 0x06002102 RID: 8450 RVA: 0x00081298 File Offset: 0x0007F498
	public static float DegreesToRadians(float rads)
	{
		return 0.0174532924f * rads;
	}

	// Token: 0x06002103 RID: 8451 RVA: 0x000812A4 File Offset: 0x0007F4A4
	public static float RadiansToDegrees(float degs)
	{
		return 57.29578f * degs;
	}

	// Token: 0x06002104 RID: 8452 RVA: 0x000812B0 File Offset: 0x0007F4B0
	private static float SimpleSpline(float v01)
	{
		return 3f * (v01 * v01) - 2f * (v01 * v01) * v01;
	}
}
