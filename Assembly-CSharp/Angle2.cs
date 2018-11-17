using System;
using System.Runtime.InteropServices;
using UnityEngine;

// Token: 0x02000270 RID: 624
[StructLayout(LayoutKind.Explicit, Size = 8)]
public struct Angle2
{
	// Token: 0x06001697 RID: 5783 RVA: 0x00053CC0 File Offset: 0x00051EC0
	public Angle2(float pitch, float yaw)
	{
		this = default(global::Angle2);
		global::Angle2 angle = this;
		angle.pitch = pitch;
		angle.yaw = yaw;
		this = angle;
	}

	// Token: 0x06001698 RID: 5784 RVA: 0x00053CF4 File Offset: 0x00051EF4
	public Angle2(global::Angle2 angle)
	{
		this = angle;
	}

	// Token: 0x06001699 RID: 5785 RVA: 0x00053D00 File Offset: 0x00051F00
	public Angle2(Vector2 pitchYaw)
	{
		this = default(global::Angle2);
		global::Angle2 angle = this;
		angle.m = pitchYaw;
		this = angle;
	}

	// Token: 0x0600169A RID: 5786 RVA: 0x00053D2C File Offset: 0x00051F2C
	static Angle2()
	{
		for (long num = 0L; num < 8192L; num += 1L)
		{
			global::Angle2.eights360[(int)(checked((IntPtr)num))] = (float)((double)num / 65536.0 * 360.0);
		}
		global::uLinkAngle2Extensions.Register();
	}

	// Token: 0x17000674 RID: 1652
	public float this[int index]
	{
		get
		{
			return this.m[index];
		}
		set
		{
			this.m[index] = value;
		}
	}

	// Token: 0x17000675 RID: 1653
	// (get) Token: 0x0600169D RID: 5789 RVA: 0x00053DB4 File Offset: 0x00051FB4
	// (set) Token: 0x0600169E RID: 5790 RVA: 0x00053DD0 File Offset: 0x00051FD0
	public Quaternion quat
	{
		get
		{
			return Quaternion.Euler(-this.pitch, this.yaw, 0f);
		}
		set
		{
			this.eulerAngles = value.eulerAngles;
		}
	}

	// Token: 0x17000676 RID: 1654
	// (get) Token: 0x0600169F RID: 5791 RVA: 0x00053DE0 File Offset: 0x00051FE0
	// (set) Token: 0x060016A0 RID: 5792 RVA: 0x00053DF4 File Offset: 0x00051FF4
	public Vector3 eulerAngles
	{
		get
		{
			return new Vector3(-this.pitch, this.yaw);
		}
		set
		{
			this.pitch = -value.x;
			this.yaw = value.y;
		}
	}

	// Token: 0x17000677 RID: 1655
	// (get) Token: 0x060016A1 RID: 5793 RVA: 0x00053E14 File Offset: 0x00052014
	// (set) Token: 0x060016A2 RID: 5794 RVA: 0x00053E28 File Offset: 0x00052028
	public Vector3 yawEulerAngles
	{
		get
		{
			return new Vector3(0f, this.yaw);
		}
		set
		{
			this.yaw = value.y;
		}
	}

	// Token: 0x17000678 RID: 1656
	// (get) Token: 0x060016A3 RID: 5795 RVA: 0x00053E38 File Offset: 0x00052038
	// (set) Token: 0x060016A4 RID: 5796 RVA: 0x00053E4C File Offset: 0x0005204C
	public Vector3 pitchEulerAngles
	{
		get
		{
			return new Vector3(-this.pitch, 0f);
		}
		set
		{
			this.pitch = -value.x;
		}
	}

	// Token: 0x17000679 RID: 1657
	// (get) Token: 0x060016A5 RID: 5797 RVA: 0x00053E5C File Offset: 0x0005205C
	// (set) Token: 0x060016A6 RID: 5798 RVA: 0x00053E70 File Offset: 0x00052070
	public Vector3 forward
	{
		get
		{
			return this.quat * Vector3.forward;
		}
		set
		{
			this.quat = Quaternion.LookRotation(value);
		}
	}

	// Token: 0x1700067A RID: 1658
	// (get) Token: 0x060016A7 RID: 5799 RVA: 0x00053E80 File Offset: 0x00052080
	public Vector3 right
	{
		get
		{
			return this.quat * Vector3.right;
		}
	}

	// Token: 0x1700067B RID: 1659
	// (get) Token: 0x060016A8 RID: 5800 RVA: 0x00053E94 File Offset: 0x00052094
	public Vector3 up
	{
		get
		{
			return this.quat * Vector3.up;
		}
	}

	// Token: 0x1700067C RID: 1660
	// (get) Token: 0x060016A9 RID: 5801 RVA: 0x00053EA8 File Offset: 0x000520A8
	// (set) Token: 0x060016AA RID: 5802 RVA: 0x00053EBC File Offset: 0x000520BC
	public Vector3 back
	{
		get
		{
			return this.quat * Vector3.back;
		}
		set
		{
			this.forward = -value;
		}
	}

	// Token: 0x1700067D RID: 1661
	// (get) Token: 0x060016AB RID: 5803 RVA: 0x00053ECC File Offset: 0x000520CC
	public Vector3 left
	{
		get
		{
			return this.quat * Vector3.left;
		}
	}

	// Token: 0x1700067E RID: 1662
	// (get) Token: 0x060016AC RID: 5804 RVA: 0x00053EE0 File Offset: 0x000520E0
	public Vector3 down
	{
		get
		{
			return this.quat * Vector3.down;
		}
	}

	// Token: 0x060016AD RID: 5805 RVA: 0x00053EF4 File Offset: 0x000520F4
	public override int GetHashCode()
	{
		return this.normalized.m.GetHashCode();
	}

	// Token: 0x060016AE RID: 5806 RVA: 0x00053F14 File Offset: 0x00052114
	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return false;
		}
		if (obj is global::Angle2)
		{
			return this == (global::Angle2)obj;
		}
		if (obj is Vector2)
		{
			return this == (Vector2)obj;
		}
		if (obj is Quaternion)
		{
			return this == (Quaternion)obj;
		}
		if (obj is Vector3)
		{
			return this == (Vector3)obj;
		}
		return obj.Equals(this);
	}

	// Token: 0x060016AF RID: 5807 RVA: 0x00053FB0 File Offset: 0x000521B0
	public override string ToString()
	{
		return this.m.ToString();
	}

	// Token: 0x1700067F RID: 1663
	// (get) Token: 0x060016B0 RID: 5808 RVA: 0x00053FC0 File Offset: 0x000521C0
	public global::Angle2 normalized
	{
		get
		{
			return global::Angle2.Normalize(this);
		}
	}

	// Token: 0x17000680 RID: 1664
	// (get) Token: 0x060016B1 RID: 5809 RVA: 0x00053FD0 File Offset: 0x000521D0
	public float angleMagnitude
	{
		get
		{
			return this.m.magnitude;
		}
	}

	// Token: 0x17000681 RID: 1665
	// (get) Token: 0x060016B2 RID: 5810 RVA: 0x00053FE0 File Offset: 0x000521E0
	public float sqrAngleMagnitude
	{
		get
		{
			return this.m.sqrMagnitude;
		}
	}

	// Token: 0x17000682 RID: 1666
	// (get) Token: 0x060016B3 RID: 5811 RVA: 0x00053FF0 File Offset: 0x000521F0
	public float normalizedAngleMagnitude
	{
		get
		{
			return global::Angle2.Normalize(this).m.magnitude;
		}
	}

	// Token: 0x17000683 RID: 1667
	// (get) Token: 0x060016B4 RID: 5812 RVA: 0x00054018 File Offset: 0x00052218
	public float normalizedSqrAngleMagnitude
	{
		get
		{
			return global::Angle2.Normalize(this).m.sqrMagnitude;
		}
	}

	// Token: 0x060016B5 RID: 5813 RVA: 0x00054040 File Offset: 0x00052240
	private static Vector2 NormMags(global::Angle2 a, global::Angle2 b)
	{
		Vector2 result;
		result..ctor(global::Angle2.DistAngle(a.x, b.x), global::Angle2.DistAngle(a.y, b.y));
		result.Normalize();
		return result;
	}

	// Token: 0x060016B6 RID: 5814 RVA: 0x00054084 File Offset: 0x00052284
	public static global::Angle2 SmoothDamp(global::Angle2 current, global::Angle2 target, ref Vector2 velocity, float damping, float maxAngleMove, float deltaTime)
	{
		if (current.x == target.x)
		{
			velocity.x = 0f;
			current.y = Mathf.SmoothDampAngle(current.y, target.y, ref velocity.y, damping, maxAngleMove, deltaTime);
		}
		else if (current.y == target.y)
		{
			velocity.y = 0f;
			current.x = Mathf.SmoothDampAngle(current.x, target.x, ref velocity.x, damping, maxAngleMove, deltaTime);
		}
		else
		{
			Vector2 vector = global::Angle2.NormMags(current, target) * maxAngleMove;
			current.x = Mathf.SmoothDampAngle(current.x, target.x, ref velocity.x, damping, vector.x, deltaTime);
			current.y = Mathf.SmoothDampAngle(current.y, target.y, ref velocity.y, damping, vector.y, deltaTime);
		}
		return current;
	}

	// Token: 0x060016B7 RID: 5815 RVA: 0x00054188 File Offset: 0x00052388
	public static global::Angle2 SmoothDamp(global::Angle2 current, global::Angle2 target, ref Vector2 velocity, Vector2 damping, Vector2 maxAngleMove, float deltaTime)
	{
		current.x = Mathf.SmoothDampAngle(current.x, target.x, ref velocity.x, damping.x, maxAngleMove.x, deltaTime);
		current.y = Mathf.SmoothDampAngle(current.y, target.y, ref velocity.y, damping.y, maxAngleMove.y, deltaTime);
		return current;
	}

	// Token: 0x060016B8 RID: 5816 RVA: 0x000541F8 File Offset: 0x000523F8
	public static global::Angle2 SmoothDamp(global::Angle2 current, global::Angle2 target, ref Vector2 velocity, float damping, Vector2 maxAngleMove, float deltaTime)
	{
		current.x = Mathf.SmoothDampAngle(current.x, target.x, ref velocity.x, damping, maxAngleMove.x, deltaTime);
		current.y = Mathf.SmoothDampAngle(current.y, target.y, ref velocity.y, damping, maxAngleMove.y, deltaTime);
		return current;
	}

	// Token: 0x060016B9 RID: 5817 RVA: 0x0005425C File Offset: 0x0005245C
	public static global::Angle2 SmoothDamp(global::Angle2 current, global::Angle2 target, ref Vector2 velocity, float damping, Vector2 maxAngleMove)
	{
		return global::Angle2.SmoothDamp(current, target, ref velocity, damping, maxAngleMove, Time.deltaTime);
	}

	// Token: 0x060016BA RID: 5818 RVA: 0x00054270 File Offset: 0x00052470
	public static global::Angle2 SmoothDamp(global::Angle2 current, global::Angle2 target, ref Vector2 velocity, Vector2 damping, Vector2 maxAngleMove)
	{
		return global::Angle2.SmoothDamp(current, target, ref velocity, damping, maxAngleMove, Time.deltaTime);
	}

	// Token: 0x060016BB RID: 5819 RVA: 0x00054284 File Offset: 0x00052484
	public static global::Angle2 SmoothDamp(global::Angle2 current, global::Angle2 target, ref Vector2 velocity, float damping, float maxAngleMove)
	{
		return global::Angle2.SmoothDamp(current, target, ref velocity, damping, maxAngleMove, Time.deltaTime);
	}

	// Token: 0x060016BC RID: 5820 RVA: 0x00054298 File Offset: 0x00052498
	public static global::Angle2 SmoothDamp(global::Angle2 current, global::Angle2 target, ref Vector2 velocity, float damping)
	{
		return global::Angle2.SmoothDamp(current, target, ref velocity, damping, float.PositiveInfinity, Time.deltaTime);
	}

	// Token: 0x060016BD RID: 5821 RVA: 0x000542B0 File Offset: 0x000524B0
	public static global::Angle2 SmoothDamp(global::Angle2 current, global::Angle2 target, ref Vector2 velocity, Vector2 damping)
	{
		return global::Angle2.SmoothDamp(current, target, ref velocity, damping, new Vector2(float.PositiveInfinity, float.PositiveInfinity), Time.deltaTime);
	}

	// Token: 0x060016BE RID: 5822 RVA: 0x000542D0 File Offset: 0x000524D0
	public static global::Angle2 MoveTowards(global::Angle2 current, global::Angle2 target, float maxAngleMove)
	{
		if (current.x == target.x)
		{
			current.y = Mathf.MoveTowardsAngle(current.y, target.y, maxAngleMove);
		}
		else if (current.y == target.y)
		{
			current.x = Mathf.MoveTowardsAngle(current.x, target.x, maxAngleMove);
		}
		else
		{
			Vector2 vector = global::Angle2.NormMags(current, target) * maxAngleMove;
			current.x = Mathf.MoveTowardsAngle(current.x, target.x, vector.x);
			current.y = Mathf.MoveTowardsAngle(current.y, target.y, vector.y);
		}
		return current;
	}

	// Token: 0x060016BF RID: 5823 RVA: 0x00054394 File Offset: 0x00052594
	public static global::Angle2 MoveTowards(global::Angle2 current, global::Angle2 target, Vector2 maxAngleMove)
	{
		current.x = Mathf.MoveTowardsAngle(current.x, target.y, maxAngleMove.x);
		current.y = Mathf.MoveTowardsAngle(current.x, target.y, maxAngleMove.y);
		return current;
	}

	// Token: 0x060016C0 RID: 5824 RVA: 0x000543E4 File Offset: 0x000525E4
	public static global::Angle2 Lerp(global::Angle2 a, global::Angle2 b, float t)
	{
		return new global::Angle2(Mathf.LerpAngle(a.x, b.x, t), Mathf.LerpAngle(a.y, b.y, t));
	}

	// Token: 0x060016C1 RID: 5825 RVA: 0x00054414 File Offset: 0x00052614
	public static global::Angle2 Lerp(global::Angle2 a, global::Angle2 b, Vector2 t)
	{
		return new global::Angle2(Mathf.LerpAngle(a.x, b.x, t.x), Mathf.LerpAngle(a.y, b.y, t.y));
	}

	// Token: 0x060016C2 RID: 5826 RVA: 0x00054450 File Offset: 0x00052650
	public static global::Angle2 Delta(global::Angle2 a, global::Angle2 b)
	{
		return new global::Angle2(Mathf.DeltaAngle(a.x, b.x), Mathf.DeltaAngle(b.x, b.y));
	}

	// Token: 0x060016C3 RID: 5827 RVA: 0x00054480 File Offset: 0x00052680
	public static float SquareAngleDistance(global::Angle2 a, global::Angle2 b)
	{
		float num = Mathf.DeltaAngle(a.x, b.x);
		float num2 = Mathf.DeltaAngle(a.y, b.y);
		return num * num + num2 * num2;
	}

	// Token: 0x060016C4 RID: 5828 RVA: 0x000544BC File Offset: 0x000526BC
	public static float AngleDistance(global::Angle2 a, global::Angle2 b)
	{
		return Mathf.Sqrt(global::Angle2.SquareAngleDistance(a, b));
	}

	// Token: 0x060016C5 RID: 5829 RVA: 0x000544CC File Offset: 0x000526CC
	private static float DistAngle(float a, float b)
	{
		return Mathf.Abs(Mathf.DeltaAngle(a, b));
	}

	// Token: 0x060016C6 RID: 5830 RVA: 0x000544DC File Offset: 0x000526DC
	private static float NormAngle(float a)
	{
		a = Mathf.DeltaAngle(0f, a);
		return (a <= 180f) ? a : (a - 360f);
	}

	// Token: 0x060016C7 RID: 5831 RVA: 0x00054504 File Offset: 0x00052704
	public static global::Angle2 Normalize(global::Angle2 a)
	{
		return new global::Angle2
		{
			x = global::Angle2.NormAngle(a.x),
			y = global::Angle2.NormAngle(a.y)
		};
	}

	// Token: 0x060016C8 RID: 5832 RVA: 0x00054540 File Offset: 0x00052740
	public static global::Angle2 NormalizeAdd(global::Angle2 a, global::Angle2 b)
	{
		return new global::Angle2
		{
			x = global::Angle2.NormAngle(a.x + b.x),
			y = global::Angle2.NormAngle(a.y + b.y)
		};
	}

	// Token: 0x060016C9 RID: 5833 RVA: 0x0005458C File Offset: 0x0005278C
	public static global::Angle2 NormalizeSubtract(global::Angle2 a, global::Angle2 b)
	{
		return new global::Angle2
		{
			x = global::Angle2.NormAngle(a.x - b.x),
			y = global::Angle2.NormAngle(a.y - b.y)
		};
	}

	// Token: 0x060016CA RID: 5834 RVA: 0x000545D8 File Offset: 0x000527D8
	public static global::Angle2 LookDirection(Vector3 v)
	{
		return (global::Angle2)v;
	}

	// Token: 0x060016CB RID: 5835 RVA: 0x000545E0 File Offset: 0x000527E0
	public static Vector3 Direction(float pitch, float yaw)
	{
		return Quaternion.Euler(-pitch, yaw, 0f) * Vector3.forward;
	}

	// Token: 0x060016CC RID: 5836 RVA: 0x000545FC File Offset: 0x000527FC
	public static int Encode360(float x)
	{
		x = Mathf.DeltaAngle(0f, x);
		if (x < 0f)
		{
			x += 360f;
		}
		switch (Mathf.FloorToInt(x) / 45)
		{
		case 0:
			return Mathf.RoundToInt((float)((double)x * 182.04444444444445));
		case 1:
			return Mathf.RoundToInt((float)((double)(x - 45f) * 182.04444444444445)) + 8192;
		case 2:
			return Mathf.RoundToInt((float)((double)(x - 90f) * 182.04444444444445)) + 16384;
		case 3:
			return Mathf.RoundToInt((float)((double)(x - 135f) * 182.04444444444445)) + 24576;
		case 4:
			return Mathf.RoundToInt((float)((double)(x - 180f) * 182.04444444444445)) + 32768;
		case 5:
			return Mathf.RoundToInt((float)((double)(x - 225f) * 182.04444444444445)) + 40960;
		case 6:
			return Mathf.RoundToInt((float)((double)(x - 270f) * 182.04444444444445)) + 49152;
		case 7:
			return Mathf.RoundToInt((float)((double)(x - 315f) * 182.04444444444445)) + 57344;
		case 8:
			return 0;
		default:
			return -1;
		}
	}

	// Token: 0x060016CD RID: 5837 RVA: 0x00054754 File Offset: 0x00052954
	public static float Decode360(int x)
	{
		int num = x / 8192;
		float num2 = (float)num * 45f + global::Angle2.eights360[x - num * 8192];
		return (num2 >= 180f) ? (num2 - 360f) : num2;
	}

	// Token: 0x17000684 RID: 1668
	// (get) Token: 0x060016CE RID: 5838 RVA: 0x0005479C File Offset: 0x0005299C
	// (set) Token: 0x060016CF RID: 5839 RVA: 0x000547B8 File Offset: 0x000529B8
	public int encoded
	{
		get
		{
			return global::Angle2.Encode360(this.y) << 16 | global::Angle2.Encode360(this.x);
		}
		set
		{
			this.x = global::Angle2.Decode360(value & 65535);
			this.y = global::Angle2.Decode360(value >> 16 & 65535);
		}
	}

	// Token: 0x17000685 RID: 1669
	// (get) Token: 0x060016D0 RID: 5840 RVA: 0x000547E4 File Offset: 0x000529E4
	public global::Angle2 decoded
	{
		get
		{
			global::Angle2 result = this;
			result.encoded = this.encoded;
			return result;
		}
	}

	// Token: 0x060016D1 RID: 5841 RVA: 0x00054808 File Offset: 0x00052A08
	public static global::Angle2 operator -(global::Angle2 L, global::Angle2 R)
	{
		L.m -= R.m;
		return L;
	}

	// Token: 0x060016D2 RID: 5842 RVA: 0x00054824 File Offset: 0x00052A24
	public static global::Angle2 operator +(global::Angle2 L, global::Angle2 R)
	{
		L.m += R.m;
		return L;
	}

	// Token: 0x060016D3 RID: 5843 RVA: 0x00054840 File Offset: 0x00052A40
	public static global::Angle2 operator *(global::Angle2 L, global::Angle2 R)
	{
		L.m += global::Angle2.Delta(L, R).m;
		return L;
	}

	// Token: 0x060016D4 RID: 5844 RVA: 0x00054870 File Offset: 0x00052A70
	public static global::Angle2 operator /(global::Angle2 L, global::Angle2 R)
	{
		L.m -= global::Angle2.Delta(L, R).m;
		return L;
	}

	// Token: 0x060016D5 RID: 5845 RVA: 0x000548A0 File Offset: 0x00052AA0
	public static global::Angle2 operator +(global::Angle2 L, Vector2 R)
	{
		L.m += R;
		return L;
	}

	// Token: 0x060016D6 RID: 5846 RVA: 0x000548B8 File Offset: 0x00052AB8
	public static global::Angle2 operator -(global::Angle2 L, Vector2 R)
	{
		L.m -= R;
		return L;
	}

	// Token: 0x060016D7 RID: 5847 RVA: 0x000548D0 File Offset: 0x00052AD0
	public static global::Angle2 operator *(global::Angle2 L, Vector2 R)
	{
		L.m = Vector2.Scale(L.m, R);
		return L;
	}

	// Token: 0x060016D8 RID: 5848 RVA: 0x000548E8 File Offset: 0x00052AE8
	public static global::Angle2 operator /(global::Angle2 L, Vector2 R)
	{
		L.x /= R.x;
		L.y /= R.y;
		return L;
	}

	// Token: 0x060016D9 RID: 5849 RVA: 0x00054918 File Offset: 0x00052B18
	public static Vector2 operator +(Vector2 L, global::Angle2 R)
	{
		L += R.m;
		return L;
	}

	// Token: 0x060016DA RID: 5850 RVA: 0x0005492C File Offset: 0x00052B2C
	public static Vector2 operator -(Vector2 L, global::Angle2 R)
	{
		L -= R.m;
		return L;
	}

	// Token: 0x060016DB RID: 5851 RVA: 0x00054940 File Offset: 0x00052B40
	public static global::Angle2 operator *(global::Angle2 L, float R)
	{
		L.m *= R;
		return L;
	}

	// Token: 0x060016DC RID: 5852 RVA: 0x00054958 File Offset: 0x00052B58
	public static global::Angle2 operator *(float L, global::Angle2 R)
	{
		R.m *= L;
		return R;
	}

	// Token: 0x060016DD RID: 5853 RVA: 0x00054970 File Offset: 0x00052B70
	public static global::Angle2 operator /(global::Angle2 L, float R)
	{
		L.m /= R;
		return L;
	}

	// Token: 0x060016DE RID: 5854 RVA: 0x00054988 File Offset: 0x00052B88
	public static global::Angle2 operator /(float L, global::Angle2 R)
	{
		R.m /= L;
		return R;
	}

	// Token: 0x060016DF RID: 5855 RVA: 0x000549A0 File Offset: 0x00052BA0
	public static Vector3 operator *(global::Angle2 L, Vector3 R)
	{
		return L.quat * R;
	}

	// Token: 0x060016E0 RID: 5856 RVA: 0x000549B0 File Offset: 0x00052BB0
	public static global::Angle2 operator *(global::Angle2 L, Quaternion R)
	{
		L.quat *= R;
		return L;
	}

	// Token: 0x060016E1 RID: 5857 RVA: 0x000549C8 File Offset: 0x00052BC8
	public static Quaternion operator *(Quaternion L, global::Angle2 R)
	{
		return L * R.quat;
	}

	// Token: 0x060016E2 RID: 5858 RVA: 0x000549D8 File Offset: 0x00052BD8
	public static global::Angle2 operator -(global::Angle2 negate)
	{
		negate.m = -negate.m;
		return negate;
	}

	// Token: 0x060016E3 RID: 5859 RVA: 0x000549F0 File Offset: 0x00052BF0
	public static bool operator ==(global::Angle2 L, global::Angle2 R)
	{
		return global::Angle2.Normalize(L - R).sqrAngleMagnitude == 0f;
	}

	// Token: 0x060016E4 RID: 5860 RVA: 0x00054A18 File Offset: 0x00052C18
	public static bool operator !=(global::Angle2 L, global::Angle2 R)
	{
		return global::Angle2.Normalize(L - R).sqrAngleMagnitude != 0f;
	}

	// Token: 0x060016E5 RID: 5861 RVA: 0x00054A44 File Offset: 0x00052C44
	public static bool operator ==(Vector2 L, global::Angle2 R)
	{
		return L == R.m;
	}

	// Token: 0x060016E6 RID: 5862 RVA: 0x00054A54 File Offset: 0x00052C54
	public static bool operator !=(Vector2 L, global::Angle2 R)
	{
		return L != R.m;
	}

	// Token: 0x060016E7 RID: 5863 RVA: 0x00054A64 File Offset: 0x00052C64
	public static bool operator ==(global::Angle2 L, Vector2 R)
	{
		return L.m == R;
	}

	// Token: 0x060016E8 RID: 5864 RVA: 0x00054A74 File Offset: 0x00052C74
	public static bool operator !=(global::Angle2 L, Vector2 R)
	{
		return L.m != R;
	}

	// Token: 0x060016E9 RID: 5865 RVA: 0x00054A84 File Offset: 0x00052C84
	public static bool operator ==(Vector3 L, global::Angle2 R)
	{
		return L == R.forward;
	}

	// Token: 0x060016EA RID: 5866 RVA: 0x00054A94 File Offset: 0x00052C94
	public static bool operator !=(Vector3 L, global::Angle2 R)
	{
		return L != R.forward;
	}

	// Token: 0x060016EB RID: 5867 RVA: 0x00054AA4 File Offset: 0x00052CA4
	public static bool operator ==(global::Angle2 L, Vector3 R)
	{
		return L.forward == R;
	}

	// Token: 0x060016EC RID: 5868 RVA: 0x00054AB4 File Offset: 0x00052CB4
	public static bool operator !=(global::Angle2 L, Vector3 R)
	{
		return L.forward != R;
	}

	// Token: 0x060016ED RID: 5869 RVA: 0x00054AC4 File Offset: 0x00052CC4
	public static bool operator ==(Quaternion L, global::Angle2 R)
	{
		return L == R.quat;
	}

	// Token: 0x060016EE RID: 5870 RVA: 0x00054AD4 File Offset: 0x00052CD4
	public static bool operator !=(Quaternion L, global::Angle2 R)
	{
		return L != R.quat;
	}

	// Token: 0x060016EF RID: 5871 RVA: 0x00054AE4 File Offset: 0x00052CE4
	public static bool operator ==(global::Angle2 L, Quaternion R)
	{
		return L.quat == R;
	}

	// Token: 0x060016F0 RID: 5872 RVA: 0x00054AF4 File Offset: 0x00052CF4
	public static bool operator !=(global::Angle2 L, Quaternion R)
	{
		return L.quat != R;
	}

	// Token: 0x060016F1 RID: 5873 RVA: 0x00054B04 File Offset: 0x00052D04
	public static implicit operator global::Angle2(Vector2 yawPitch)
	{
		return new global::Angle2
		{
			m = yawPitch
		};
	}

	// Token: 0x060016F2 RID: 5874 RVA: 0x00054B24 File Offset: 0x00052D24
	public static implicit operator Vector2(global::Angle2 a)
	{
		return a.m;
	}

	// Token: 0x060016F3 RID: 5875 RVA: 0x00054B30 File Offset: 0x00052D30
	public static explicit operator global::Angle2(Vector3 forward)
	{
		return new global::Angle2
		{
			forward = forward
		};
	}

	// Token: 0x060016F4 RID: 5876 RVA: 0x00054B50 File Offset: 0x00052D50
	public static explicit operator Vector3(global::Angle2 a)
	{
		return a.forward;
	}

	// Token: 0x060016F5 RID: 5877 RVA: 0x00054B5C File Offset: 0x00052D5C
	public static explicit operator global::Angle2(Quaternion quat)
	{
		return new global::Angle2
		{
			quat = quat
		};
	}

	// Token: 0x060016F6 RID: 5878 RVA: 0x00054B7C File Offset: 0x00052D7C
	public static explicit operator Quaternion(global::Angle2 a)
	{
		return a.quat;
	}

	// Token: 0x04000BAE RID: 2990
	private const float kEigth = 45f;

	// Token: 0x04000BAF RID: 2991
	private const double kF2I = 182.04444444444445;

	// Token: 0x04000BB0 RID: 2992
	[FieldOffset(0)]
	public float pitch;

	// Token: 0x04000BB1 RID: 2993
	[FieldOffset(4)]
	public float yaw;

	// Token: 0x04000BB2 RID: 2994
	[FieldOffset(0)]
	public float x;

	// Token: 0x04000BB3 RID: 2995
	[FieldOffset(4)]
	public float y;

	// Token: 0x04000BB4 RID: 2996
	[FieldOffset(0)]
	public Vector2 m;

	// Token: 0x04000BB5 RID: 2997
	public static readonly global::Angle2 zero = default(global::Angle2);

	// Token: 0x04000BB6 RID: 2998
	private static readonly float[] eights360 = new float[8192];
}
