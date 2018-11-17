using System;
using System.Runtime.InteropServices;
using UnityEngine;

// Token: 0x0200023D RID: 573
[StructLayout(LayoutKind.Explicit, Size = 8)]
public struct Angle2
{
	// Token: 0x06001543 RID: 5443 RVA: 0x0004F918 File Offset: 0x0004DB18
	public Angle2(float pitch, float yaw)
	{
		this = default(Angle2);
		Angle2 angle = this;
		angle.pitch = pitch;
		angle.yaw = yaw;
		this = angle;
	}

	// Token: 0x06001544 RID: 5444 RVA: 0x0004F94C File Offset: 0x0004DB4C
	public Angle2(Angle2 angle)
	{
		this = angle;
	}

	// Token: 0x06001545 RID: 5445 RVA: 0x0004F958 File Offset: 0x0004DB58
	public Angle2(Vector2 pitchYaw)
	{
		this = default(Angle2);
		Angle2 angle = this;
		angle.m = pitchYaw;
		this = angle;
	}

	// Token: 0x06001546 RID: 5446 RVA: 0x0004F984 File Offset: 0x0004DB84
	static Angle2()
	{
		for (long num = 0L; num < 8192L; num += 1L)
		{
			Angle2.eights360[(int)(checked((IntPtr)num))] = (float)((double)num / 65536.0 * 360.0);
		}
		uLinkAngle2Extensions.Register();
	}

	// Token: 0x1700062C RID: 1580
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

	// Token: 0x1700062D RID: 1581
	// (get) Token: 0x06001549 RID: 5449 RVA: 0x0004FA0C File Offset: 0x0004DC0C
	// (set) Token: 0x0600154A RID: 5450 RVA: 0x0004FA28 File Offset: 0x0004DC28
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

	// Token: 0x1700062E RID: 1582
	// (get) Token: 0x0600154B RID: 5451 RVA: 0x0004FA38 File Offset: 0x0004DC38
	// (set) Token: 0x0600154C RID: 5452 RVA: 0x0004FA4C File Offset: 0x0004DC4C
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

	// Token: 0x1700062F RID: 1583
	// (get) Token: 0x0600154D RID: 5453 RVA: 0x0004FA6C File Offset: 0x0004DC6C
	// (set) Token: 0x0600154E RID: 5454 RVA: 0x0004FA80 File Offset: 0x0004DC80
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

	// Token: 0x17000630 RID: 1584
	// (get) Token: 0x0600154F RID: 5455 RVA: 0x0004FA90 File Offset: 0x0004DC90
	// (set) Token: 0x06001550 RID: 5456 RVA: 0x0004FAA4 File Offset: 0x0004DCA4
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

	// Token: 0x17000631 RID: 1585
	// (get) Token: 0x06001551 RID: 5457 RVA: 0x0004FAB4 File Offset: 0x0004DCB4
	// (set) Token: 0x06001552 RID: 5458 RVA: 0x0004FAC8 File Offset: 0x0004DCC8
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

	// Token: 0x17000632 RID: 1586
	// (get) Token: 0x06001553 RID: 5459 RVA: 0x0004FAD8 File Offset: 0x0004DCD8
	public Vector3 right
	{
		get
		{
			return this.quat * Vector3.right;
		}
	}

	// Token: 0x17000633 RID: 1587
	// (get) Token: 0x06001554 RID: 5460 RVA: 0x0004FAEC File Offset: 0x0004DCEC
	public Vector3 up
	{
		get
		{
			return this.quat * Vector3.up;
		}
	}

	// Token: 0x17000634 RID: 1588
	// (get) Token: 0x06001555 RID: 5461 RVA: 0x0004FB00 File Offset: 0x0004DD00
	// (set) Token: 0x06001556 RID: 5462 RVA: 0x0004FB14 File Offset: 0x0004DD14
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

	// Token: 0x17000635 RID: 1589
	// (get) Token: 0x06001557 RID: 5463 RVA: 0x0004FB24 File Offset: 0x0004DD24
	public Vector3 left
	{
		get
		{
			return this.quat * Vector3.left;
		}
	}

	// Token: 0x17000636 RID: 1590
	// (get) Token: 0x06001558 RID: 5464 RVA: 0x0004FB38 File Offset: 0x0004DD38
	public Vector3 down
	{
		get
		{
			return this.quat * Vector3.down;
		}
	}

	// Token: 0x06001559 RID: 5465 RVA: 0x0004FB4C File Offset: 0x0004DD4C
	public override int GetHashCode()
	{
		return this.normalized.m.GetHashCode();
	}

	// Token: 0x0600155A RID: 5466 RVA: 0x0004FB6C File Offset: 0x0004DD6C
	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return false;
		}
		if (obj is Angle2)
		{
			return this == (Angle2)obj;
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

	// Token: 0x0600155B RID: 5467 RVA: 0x0004FC08 File Offset: 0x0004DE08
	public override string ToString()
	{
		return this.m.ToString();
	}

	// Token: 0x17000637 RID: 1591
	// (get) Token: 0x0600155C RID: 5468 RVA: 0x0004FC18 File Offset: 0x0004DE18
	public Angle2 normalized
	{
		get
		{
			return Angle2.Normalize(this);
		}
	}

	// Token: 0x17000638 RID: 1592
	// (get) Token: 0x0600155D RID: 5469 RVA: 0x0004FC28 File Offset: 0x0004DE28
	public float angleMagnitude
	{
		get
		{
			return this.m.magnitude;
		}
	}

	// Token: 0x17000639 RID: 1593
	// (get) Token: 0x0600155E RID: 5470 RVA: 0x0004FC38 File Offset: 0x0004DE38
	public float sqrAngleMagnitude
	{
		get
		{
			return this.m.sqrMagnitude;
		}
	}

	// Token: 0x1700063A RID: 1594
	// (get) Token: 0x0600155F RID: 5471 RVA: 0x0004FC48 File Offset: 0x0004DE48
	public float normalizedAngleMagnitude
	{
		get
		{
			return Angle2.Normalize(this).m.magnitude;
		}
	}

	// Token: 0x1700063B RID: 1595
	// (get) Token: 0x06001560 RID: 5472 RVA: 0x0004FC70 File Offset: 0x0004DE70
	public float normalizedSqrAngleMagnitude
	{
		get
		{
			return Angle2.Normalize(this).m.sqrMagnitude;
		}
	}

	// Token: 0x06001561 RID: 5473 RVA: 0x0004FC98 File Offset: 0x0004DE98
	private static Vector2 NormMags(Angle2 a, Angle2 b)
	{
		Vector2 result;
		result..ctor(Angle2.DistAngle(a.x, b.x), Angle2.DistAngle(a.y, b.y));
		result.Normalize();
		return result;
	}

	// Token: 0x06001562 RID: 5474 RVA: 0x0004FCDC File Offset: 0x0004DEDC
	public static Angle2 SmoothDamp(Angle2 current, Angle2 target, ref Vector2 velocity, float damping, float maxAngleMove, float deltaTime)
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
			Vector2 vector = Angle2.NormMags(current, target) * maxAngleMove;
			current.x = Mathf.SmoothDampAngle(current.x, target.x, ref velocity.x, damping, vector.x, deltaTime);
			current.y = Mathf.SmoothDampAngle(current.y, target.y, ref velocity.y, damping, vector.y, deltaTime);
		}
		return current;
	}

	// Token: 0x06001563 RID: 5475 RVA: 0x0004FDE0 File Offset: 0x0004DFE0
	public static Angle2 SmoothDamp(Angle2 current, Angle2 target, ref Vector2 velocity, Vector2 damping, Vector2 maxAngleMove, float deltaTime)
	{
		current.x = Mathf.SmoothDampAngle(current.x, target.x, ref velocity.x, damping.x, maxAngleMove.x, deltaTime);
		current.y = Mathf.SmoothDampAngle(current.y, target.y, ref velocity.y, damping.y, maxAngleMove.y, deltaTime);
		return current;
	}

	// Token: 0x06001564 RID: 5476 RVA: 0x0004FE50 File Offset: 0x0004E050
	public static Angle2 SmoothDamp(Angle2 current, Angle2 target, ref Vector2 velocity, float damping, Vector2 maxAngleMove, float deltaTime)
	{
		current.x = Mathf.SmoothDampAngle(current.x, target.x, ref velocity.x, damping, maxAngleMove.x, deltaTime);
		current.y = Mathf.SmoothDampAngle(current.y, target.y, ref velocity.y, damping, maxAngleMove.y, deltaTime);
		return current;
	}

	// Token: 0x06001565 RID: 5477 RVA: 0x0004FEB4 File Offset: 0x0004E0B4
	public static Angle2 SmoothDamp(Angle2 current, Angle2 target, ref Vector2 velocity, float damping, Vector2 maxAngleMove)
	{
		return Angle2.SmoothDamp(current, target, ref velocity, damping, maxAngleMove, Time.deltaTime);
	}

	// Token: 0x06001566 RID: 5478 RVA: 0x0004FEC8 File Offset: 0x0004E0C8
	public static Angle2 SmoothDamp(Angle2 current, Angle2 target, ref Vector2 velocity, Vector2 damping, Vector2 maxAngleMove)
	{
		return Angle2.SmoothDamp(current, target, ref velocity, damping, maxAngleMove, Time.deltaTime);
	}

	// Token: 0x06001567 RID: 5479 RVA: 0x0004FEDC File Offset: 0x0004E0DC
	public static Angle2 SmoothDamp(Angle2 current, Angle2 target, ref Vector2 velocity, float damping, float maxAngleMove)
	{
		return Angle2.SmoothDamp(current, target, ref velocity, damping, maxAngleMove, Time.deltaTime);
	}

	// Token: 0x06001568 RID: 5480 RVA: 0x0004FEF0 File Offset: 0x0004E0F0
	public static Angle2 SmoothDamp(Angle2 current, Angle2 target, ref Vector2 velocity, float damping)
	{
		return Angle2.SmoothDamp(current, target, ref velocity, damping, float.PositiveInfinity, Time.deltaTime);
	}

	// Token: 0x06001569 RID: 5481 RVA: 0x0004FF08 File Offset: 0x0004E108
	public static Angle2 SmoothDamp(Angle2 current, Angle2 target, ref Vector2 velocity, Vector2 damping)
	{
		return Angle2.SmoothDamp(current, target, ref velocity, damping, new Vector2(float.PositiveInfinity, float.PositiveInfinity), Time.deltaTime);
	}

	// Token: 0x0600156A RID: 5482 RVA: 0x0004FF28 File Offset: 0x0004E128
	public static Angle2 MoveTowards(Angle2 current, Angle2 target, float maxAngleMove)
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
			Vector2 vector = Angle2.NormMags(current, target) * maxAngleMove;
			current.x = Mathf.MoveTowardsAngle(current.x, target.x, vector.x);
			current.y = Mathf.MoveTowardsAngle(current.y, target.y, vector.y);
		}
		return current;
	}

	// Token: 0x0600156B RID: 5483 RVA: 0x0004FFEC File Offset: 0x0004E1EC
	public static Angle2 MoveTowards(Angle2 current, Angle2 target, Vector2 maxAngleMove)
	{
		current.x = Mathf.MoveTowardsAngle(current.x, target.y, maxAngleMove.x);
		current.y = Mathf.MoveTowardsAngle(current.x, target.y, maxAngleMove.y);
		return current;
	}

	// Token: 0x0600156C RID: 5484 RVA: 0x0005003C File Offset: 0x0004E23C
	public static Angle2 Lerp(Angle2 a, Angle2 b, float t)
	{
		return new Angle2(Mathf.LerpAngle(a.x, b.x, t), Mathf.LerpAngle(a.y, b.y, t));
	}

	// Token: 0x0600156D RID: 5485 RVA: 0x0005006C File Offset: 0x0004E26C
	public static Angle2 Lerp(Angle2 a, Angle2 b, Vector2 t)
	{
		return new Angle2(Mathf.LerpAngle(a.x, b.x, t.x), Mathf.LerpAngle(a.y, b.y, t.y));
	}

	// Token: 0x0600156E RID: 5486 RVA: 0x000500A8 File Offset: 0x0004E2A8
	public static Angle2 Delta(Angle2 a, Angle2 b)
	{
		return new Angle2(Mathf.DeltaAngle(a.x, b.x), Mathf.DeltaAngle(b.x, b.y));
	}

	// Token: 0x0600156F RID: 5487 RVA: 0x000500D8 File Offset: 0x0004E2D8
	public static float SquareAngleDistance(Angle2 a, Angle2 b)
	{
		float num = Mathf.DeltaAngle(a.x, b.x);
		float num2 = Mathf.DeltaAngle(a.y, b.y);
		return num * num + num2 * num2;
	}

	// Token: 0x06001570 RID: 5488 RVA: 0x00050114 File Offset: 0x0004E314
	public static float AngleDistance(Angle2 a, Angle2 b)
	{
		return Mathf.Sqrt(Angle2.SquareAngleDistance(a, b));
	}

	// Token: 0x06001571 RID: 5489 RVA: 0x00050124 File Offset: 0x0004E324
	private static float DistAngle(float a, float b)
	{
		return Mathf.Abs(Mathf.DeltaAngle(a, b));
	}

	// Token: 0x06001572 RID: 5490 RVA: 0x00050134 File Offset: 0x0004E334
	private static float NormAngle(float a)
	{
		a = Mathf.DeltaAngle(0f, a);
		return (a <= 180f) ? a : (a - 360f);
	}

	// Token: 0x06001573 RID: 5491 RVA: 0x0005015C File Offset: 0x0004E35C
	public static Angle2 Normalize(Angle2 a)
	{
		return new Angle2
		{
			x = Angle2.NormAngle(a.x),
			y = Angle2.NormAngle(a.y)
		};
	}

	// Token: 0x06001574 RID: 5492 RVA: 0x00050198 File Offset: 0x0004E398
	public static Angle2 NormalizeAdd(Angle2 a, Angle2 b)
	{
		return new Angle2
		{
			x = Angle2.NormAngle(a.x + b.x),
			y = Angle2.NormAngle(a.y + b.y)
		};
	}

	// Token: 0x06001575 RID: 5493 RVA: 0x000501E4 File Offset: 0x0004E3E4
	public static Angle2 NormalizeSubtract(Angle2 a, Angle2 b)
	{
		return new Angle2
		{
			x = Angle2.NormAngle(a.x - b.x),
			y = Angle2.NormAngle(a.y - b.y)
		};
	}

	// Token: 0x06001576 RID: 5494 RVA: 0x00050230 File Offset: 0x0004E430
	public static Angle2 LookDirection(Vector3 v)
	{
		return (Angle2)v;
	}

	// Token: 0x06001577 RID: 5495 RVA: 0x00050238 File Offset: 0x0004E438
	public static Vector3 Direction(float pitch, float yaw)
	{
		return Quaternion.Euler(-pitch, yaw, 0f) * Vector3.forward;
	}

	// Token: 0x06001578 RID: 5496 RVA: 0x00050254 File Offset: 0x0004E454
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

	// Token: 0x06001579 RID: 5497 RVA: 0x000503AC File Offset: 0x0004E5AC
	public static float Decode360(int x)
	{
		int num = x / 8192;
		float num2 = (float)num * 45f + Angle2.eights360[x - num * 8192];
		return (num2 >= 180f) ? (num2 - 360f) : num2;
	}

	// Token: 0x1700063C RID: 1596
	// (get) Token: 0x0600157A RID: 5498 RVA: 0x000503F4 File Offset: 0x0004E5F4
	// (set) Token: 0x0600157B RID: 5499 RVA: 0x00050410 File Offset: 0x0004E610
	public int encoded
	{
		get
		{
			return Angle2.Encode360(this.y) << 16 | Angle2.Encode360(this.x);
		}
		set
		{
			this.x = Angle2.Decode360(value & 65535);
			this.y = Angle2.Decode360(value >> 16 & 65535);
		}
	}

	// Token: 0x1700063D RID: 1597
	// (get) Token: 0x0600157C RID: 5500 RVA: 0x0005043C File Offset: 0x0004E63C
	public Angle2 decoded
	{
		get
		{
			Angle2 result = this;
			result.encoded = this.encoded;
			return result;
		}
	}

	// Token: 0x0600157D RID: 5501 RVA: 0x00050460 File Offset: 0x0004E660
	public static Angle2 operator -(Angle2 L, Angle2 R)
	{
		L.m -= R.m;
		return L;
	}

	// Token: 0x0600157E RID: 5502 RVA: 0x0005047C File Offset: 0x0004E67C
	public static Angle2 operator +(Angle2 L, Angle2 R)
	{
		L.m += R.m;
		return L;
	}

	// Token: 0x0600157F RID: 5503 RVA: 0x00050498 File Offset: 0x0004E698
	public static Angle2 operator *(Angle2 L, Angle2 R)
	{
		L.m += Angle2.Delta(L, R).m;
		return L;
	}

	// Token: 0x06001580 RID: 5504 RVA: 0x000504C8 File Offset: 0x0004E6C8
	public static Angle2 operator /(Angle2 L, Angle2 R)
	{
		L.m -= Angle2.Delta(L, R).m;
		return L;
	}

	// Token: 0x06001581 RID: 5505 RVA: 0x000504F8 File Offset: 0x0004E6F8
	public static Angle2 operator +(Angle2 L, Vector2 R)
	{
		L.m += R;
		return L;
	}

	// Token: 0x06001582 RID: 5506 RVA: 0x00050510 File Offset: 0x0004E710
	public static Angle2 operator -(Angle2 L, Vector2 R)
	{
		L.m -= R;
		return L;
	}

	// Token: 0x06001583 RID: 5507 RVA: 0x00050528 File Offset: 0x0004E728
	public static Angle2 operator *(Angle2 L, Vector2 R)
	{
		L.m = Vector2.Scale(L.m, R);
		return L;
	}

	// Token: 0x06001584 RID: 5508 RVA: 0x00050540 File Offset: 0x0004E740
	public static Angle2 operator /(Angle2 L, Vector2 R)
	{
		L.x /= R.x;
		L.y /= R.y;
		return L;
	}

	// Token: 0x06001585 RID: 5509 RVA: 0x00050570 File Offset: 0x0004E770
	public static Vector2 operator +(Vector2 L, Angle2 R)
	{
		L += R.m;
		return L;
	}

	// Token: 0x06001586 RID: 5510 RVA: 0x00050584 File Offset: 0x0004E784
	public static Vector2 operator -(Vector2 L, Angle2 R)
	{
		L -= R.m;
		return L;
	}

	// Token: 0x06001587 RID: 5511 RVA: 0x00050598 File Offset: 0x0004E798
	public static Angle2 operator *(Angle2 L, float R)
	{
		L.m *= R;
		return L;
	}

	// Token: 0x06001588 RID: 5512 RVA: 0x000505B0 File Offset: 0x0004E7B0
	public static Angle2 operator *(float L, Angle2 R)
	{
		R.m *= L;
		return R;
	}

	// Token: 0x06001589 RID: 5513 RVA: 0x000505C8 File Offset: 0x0004E7C8
	public static Angle2 operator /(Angle2 L, float R)
	{
		L.m /= R;
		return L;
	}

	// Token: 0x0600158A RID: 5514 RVA: 0x000505E0 File Offset: 0x0004E7E0
	public static Angle2 operator /(float L, Angle2 R)
	{
		R.m /= L;
		return R;
	}

	// Token: 0x0600158B RID: 5515 RVA: 0x000505F8 File Offset: 0x0004E7F8
	public static Vector3 operator *(Angle2 L, Vector3 R)
	{
		return L.quat * R;
	}

	// Token: 0x0600158C RID: 5516 RVA: 0x00050608 File Offset: 0x0004E808
	public static Angle2 operator *(Angle2 L, Quaternion R)
	{
		L.quat *= R;
		return L;
	}

	// Token: 0x0600158D RID: 5517 RVA: 0x00050620 File Offset: 0x0004E820
	public static Quaternion operator *(Quaternion L, Angle2 R)
	{
		return L * R.quat;
	}

	// Token: 0x0600158E RID: 5518 RVA: 0x00050630 File Offset: 0x0004E830
	public static Angle2 operator -(Angle2 negate)
	{
		negate.m = -negate.m;
		return negate;
	}

	// Token: 0x0600158F RID: 5519 RVA: 0x00050648 File Offset: 0x0004E848
	public static bool operator ==(Angle2 L, Angle2 R)
	{
		return Angle2.Normalize(L - R).sqrAngleMagnitude == 0f;
	}

	// Token: 0x06001590 RID: 5520 RVA: 0x00050670 File Offset: 0x0004E870
	public static bool operator !=(Angle2 L, Angle2 R)
	{
		return Angle2.Normalize(L - R).sqrAngleMagnitude != 0f;
	}

	// Token: 0x06001591 RID: 5521 RVA: 0x0005069C File Offset: 0x0004E89C
	public static bool operator ==(Vector2 L, Angle2 R)
	{
		return L == R.m;
	}

	// Token: 0x06001592 RID: 5522 RVA: 0x000506AC File Offset: 0x0004E8AC
	public static bool operator !=(Vector2 L, Angle2 R)
	{
		return L != R.m;
	}

	// Token: 0x06001593 RID: 5523 RVA: 0x000506BC File Offset: 0x0004E8BC
	public static bool operator ==(Angle2 L, Vector2 R)
	{
		return L.m == R;
	}

	// Token: 0x06001594 RID: 5524 RVA: 0x000506CC File Offset: 0x0004E8CC
	public static bool operator !=(Angle2 L, Vector2 R)
	{
		return L.m != R;
	}

	// Token: 0x06001595 RID: 5525 RVA: 0x000506DC File Offset: 0x0004E8DC
	public static bool operator ==(Vector3 L, Angle2 R)
	{
		return L == R.forward;
	}

	// Token: 0x06001596 RID: 5526 RVA: 0x000506EC File Offset: 0x0004E8EC
	public static bool operator !=(Vector3 L, Angle2 R)
	{
		return L != R.forward;
	}

	// Token: 0x06001597 RID: 5527 RVA: 0x000506FC File Offset: 0x0004E8FC
	public static bool operator ==(Angle2 L, Vector3 R)
	{
		return L.forward == R;
	}

	// Token: 0x06001598 RID: 5528 RVA: 0x0005070C File Offset: 0x0004E90C
	public static bool operator !=(Angle2 L, Vector3 R)
	{
		return L.forward != R;
	}

	// Token: 0x06001599 RID: 5529 RVA: 0x0005071C File Offset: 0x0004E91C
	public static bool operator ==(Quaternion L, Angle2 R)
	{
		return L == R.quat;
	}

	// Token: 0x0600159A RID: 5530 RVA: 0x0005072C File Offset: 0x0004E92C
	public static bool operator !=(Quaternion L, Angle2 R)
	{
		return L != R.quat;
	}

	// Token: 0x0600159B RID: 5531 RVA: 0x0005073C File Offset: 0x0004E93C
	public static bool operator ==(Angle2 L, Quaternion R)
	{
		return L.quat == R;
	}

	// Token: 0x0600159C RID: 5532 RVA: 0x0005074C File Offset: 0x0004E94C
	public static bool operator !=(Angle2 L, Quaternion R)
	{
		return L.quat != R;
	}

	// Token: 0x0600159D RID: 5533 RVA: 0x0005075C File Offset: 0x0004E95C
	public static implicit operator Angle2(Vector2 yawPitch)
	{
		return new Angle2
		{
			m = yawPitch
		};
	}

	// Token: 0x0600159E RID: 5534 RVA: 0x0005077C File Offset: 0x0004E97C
	public static implicit operator Vector2(Angle2 a)
	{
		return a.m;
	}

	// Token: 0x0600159F RID: 5535 RVA: 0x00050788 File Offset: 0x0004E988
	public static explicit operator Angle2(Vector3 forward)
	{
		return new Angle2
		{
			forward = forward
		};
	}

	// Token: 0x060015A0 RID: 5536 RVA: 0x000507A8 File Offset: 0x0004E9A8
	public static explicit operator Vector3(Angle2 a)
	{
		return a.forward;
	}

	// Token: 0x060015A1 RID: 5537 RVA: 0x000507B4 File Offset: 0x0004E9B4
	public static explicit operator Angle2(Quaternion quat)
	{
		return new Angle2
		{
			quat = quat
		};
	}

	// Token: 0x060015A2 RID: 5538 RVA: 0x000507D4 File Offset: 0x0004E9D4
	public static explicit operator Quaternion(Angle2 a)
	{
		return a.quat;
	}

	// Token: 0x04000A8B RID: 2699
	private const float kEigth = 45f;

	// Token: 0x04000A8C RID: 2700
	private const double kF2I = 182.04444444444445;

	// Token: 0x04000A8D RID: 2701
	[FieldOffset(0)]
	public float pitch;

	// Token: 0x04000A8E RID: 2702
	[FieldOffset(4)]
	public float yaw;

	// Token: 0x04000A8F RID: 2703
	[FieldOffset(0)]
	public float x;

	// Token: 0x04000A90 RID: 2704
	[FieldOffset(4)]
	public float y;

	// Token: 0x04000A91 RID: 2705
	[FieldOffset(0)]
	public Vector2 m;

	// Token: 0x04000A92 RID: 2706
	public static readonly Angle2 zero = default(Angle2);

	// Token: 0x04000A93 RID: 2707
	private static readonly float[] eights360 = new float[8192];
}
