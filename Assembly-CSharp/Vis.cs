using System;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using UnityEngine;

// Token: 0x02000427 RID: 1063
public static class Vis
{
	// Token: 0x06002531 RID: 9521 RVA: 0x0008880C File Offset: 0x00086A0C
	public static global::Vis.Op Negate(global::Vis.Op op)
	{
		return global::Vis.Op.Any - (op - global::Vis.Op.Any);
	}

	// Token: 0x06002532 RID: 9522 RVA: 0x00088814 File Offset: 0x00086A14
	public static global::Vis.Op<TFlags> Negate<TFlags>(global::Vis.Op<TFlags> op) where TFlags : struct, IConvertible, IFormattable, IComparable
	{
		op.op = global::Vis.Negate(op.op);
		return op;
	}

	// Token: 0x06002533 RID: 9523 RVA: 0x0008882C File Offset: 0x00086A2C
	internal static bool Evaluate(global::Vis.Op op, int f, int m)
	{
		switch (op)
		{
		case global::Vis.Op.Always:
			return true;
		case global::Vis.Op.Equals:
			return m == f;
		case global::Vis.Op.All:
			return (m & f) == f;
		case global::Vis.Op.Any:
			return (m & f) != 0;
		case global::Vis.Op.None:
			return (m & f) == 0;
		case global::Vis.Op.NotEquals:
			return m != f;
		default:
			return false;
		}
	}

	// Token: 0x06002534 RID: 9524 RVA: 0x0008888C File Offset: 0x00086A8C
	private static int SetTrue(global::Vis.Op op, int f, ref BitVector32 bits, BitVector32.Section sect)
	{
		int num = bits[sect];
		int result;
		if (num != (result = global::Vis.SetTrue(op, f, num)))
		{
			bits[sect] = num;
		}
		return result;
	}

	// Token: 0x06002535 RID: 9525 RVA: 0x000888BC File Offset: 0x00086ABC
	private static int SetTrue(global::Vis.Op op, int f, int m)
	{
		switch (op)
		{
		default:
			return m;
		case global::Vis.Op.Equals:
			return f;
		case global::Vis.Op.All:
			return m | f;
		case global::Vis.Op.Any:
			if ((m & f) == 0)
			{
				return m | f;
			}
			return m;
		case global::Vis.Op.None:
			return m & ~f;
		case global::Vis.Op.NotEquals:
			return ~f;
		}
	}

	// Token: 0x06002536 RID: 9526 RVA: 0x00088910 File Offset: 0x00086B10
	public static bool IsZeroWay(this global::Vis.Comparison comparison)
	{
		return (comparison & global::Vis.Comparison.Contact) == global::Vis.Comparison.Oblivious;
	}

	// Token: 0x06002537 RID: 9527 RVA: 0x0008891C File Offset: 0x00086B1C
	public static bool IsOneWay(this global::Vis.Comparison comparison)
	{
		return (comparison & global::Vis.Comparison.IsSelf) != global::Vis.Comparison.IsSelf;
	}

	// Token: 0x06002538 RID: 9528 RVA: 0x0008892C File Offset: 0x00086B2C
	public static bool DoesSeeTarget(this global::Vis.Comparison comparison)
	{
		return (comparison & (global::Vis.Comparison)4) == (global::Vis.Comparison)4;
	}

	// Token: 0x06002539 RID: 9529 RVA: 0x00088934 File Offset: 0x00086B34
	public static bool IsSeenByTarget(this global::Vis.Comparison comparison)
	{
		return (comparison & (global::Vis.Comparison)8) == (global::Vis.Comparison)8;
	}

	// Token: 0x0600253A RID: 9530 RVA: 0x0008893C File Offset: 0x00086B3C
	public static bool IsTwoWay(this global::Vis.Comparison comparison)
	{
		return (comparison & global::Vis.Comparison.Contact) != global::Vis.Comparison.IsSelf;
	}

	// Token: 0x0600253B RID: 9531 RVA: 0x0008894C File Offset: 0x00086B4C
	public static int CountSeen(this global::Vis.Comparison comparison)
	{
		int result = 0;
		int num = (int)comparison;
		if ((num & 1) == 1)
		{
			if ((num & 4) == 4)
			{
				num++;
			}
			if ((num & 8) == 8)
			{
				num++;
			}
		}
		return result;
	}

	// Token: 0x0600253C RID: 9532 RVA: 0x00088984 File Offset: 0x00086B84
	public static int GetStealth(this global::Vis.Comparison comparison)
	{
		int num = (int)(comparison & global::Vis.Comparison.IsSelf);
		if (num == 4)
		{
			return 1;
		}
		if (num != 8)
		{
			return 0;
		}
		return -1;
	}

	// Token: 0x0600253D RID: 9533 RVA: 0x000889B0 File Offset: 0x00086BB0
	public static global::VisNode.Search.Radial.Enumerator GetNodesInRadius(Vector3 point, float radius)
	{
		return new global::VisNode.Search.Radial.Enumerator(new global::VisNode.Search.PointRadiusData(point, radius));
	}

	// Token: 0x0600253E RID: 9534 RVA: 0x000889C0 File Offset: 0x00086BC0
	public static void RadialMessage(Vector3 point, float radius, string message, object arg)
	{
		global::VisNode.Search.Radial.Enumerator nodesInRadius = global::Vis.GetNodesInRadius(point, radius);
		while (nodesInRadius.MoveNext())
		{
			nodesInRadius.Current.SendMessage(message, arg, 1);
		}
	}

	// Token: 0x0600253F RID: 9535 RVA: 0x000889F8 File Offset: 0x00086BF8
	public static void RadialMessage(Vector3 point, float radius, string message)
	{
		global::VisNode.Search.Radial.Enumerator nodesInRadius = global::Vis.GetNodesInRadius(point, radius);
		while (nodesInRadius.MoveNext())
		{
			nodesInRadius.Current.SendMessage(message, 1);
		}
	}

	// Token: 0x06002540 RID: 9536 RVA: 0x00088A2C File Offset: 0x00086C2C
	public static global::VisNode.Search.Point.Visual.Enumerator GetNodesWhoCanSee(Vector3 point)
	{
		return new global::VisNode.Search.Point.Visual.Enumerator(new global::VisNode.Search.PointVisibilityData(point));
	}

	// Token: 0x06002541 RID: 9537 RVA: 0x00088A3C File Offset: 0x00086C3C
	public static void VisibleMessage(Vector3 point, string message, object arg)
	{
		global::VisNode.Search.Point.Visual.Enumerator nodesWhoCanSee = global::Vis.GetNodesWhoCanSee(point);
		while (nodesWhoCanSee.MoveNext())
		{
			nodesWhoCanSee.Current.SendMessage(message, arg, 1);
		}
	}

	// Token: 0x06002542 RID: 9538 RVA: 0x00088A70 File Offset: 0x00086C70
	public static void VisibleMessage(Vector3 point, string message)
	{
		global::VisNode.Search.Point.Visual.Enumerator nodesWhoCanSee = global::Vis.GetNodesWhoCanSee(point);
		while (nodesWhoCanSee.MoveNext())
		{
			nodesWhoCanSee.Current.SendMessage(message, 1);
		}
	}

	// Token: 0x04001124 RID: 4388
	public const global::Vis.Trait kTraitBegin = global::Vis.Trait.Alive;

	// Token: 0x04001125 RID: 4389
	public const global::Vis.Trait kTraitEnd = (global::Vis.Trait)32;

	// Token: 0x04001126 RID: 4390
	public const global::Vis.Trait kLifeFirst = global::Vis.Trait.Alive;

	// Token: 0x04001127 RID: 4391
	public const global::Vis.Trait kLifeLast = global::Vis.Trait.Dead;

	// Token: 0x04001128 RID: 4392
	public const global::Vis.Trait kStatusFirst = global::Vis.Trait.Casual;

	// Token: 0x04001129 RID: 4393
	public const global::Vis.Trait kStatusLast = global::Vis.Trait.Attacking;

	// Token: 0x0400112A RID: 4394
	public const global::Vis.Trait kRoleFirst = global::Vis.Trait.Citizen;

	// Token: 0x0400112B RID: 4395
	public const global::Vis.Trait kRoleLast = global::Vis.Trait.Animal;

	// Token: 0x0400112C RID: 4396
	public const global::Vis.Trait kLifeBegin = global::Vis.Trait.Alive;

	// Token: 0x0400112D RID: 4397
	public const global::Vis.Trait kLifeEnd = (global::Vis.Trait)3;

	// Token: 0x0400112E RID: 4398
	public const global::Vis.Trait kStatusBegin = global::Vis.Trait.Casual;

	// Token: 0x0400112F RID: 4399
	public const global::Vis.Trait kStatusEnd = (global::Vis.Trait)15;

	// Token: 0x04001130 RID: 4400
	public const global::Vis.Trait kRoleBegin = global::Vis.Trait.Citizen;

	// Token: 0x04001131 RID: 4401
	public const global::Vis.Trait kRoleEnd = (global::Vis.Trait)32;

	// Token: 0x04001132 RID: 4402
	public const int kLifeCount = 3;

	// Token: 0x04001133 RID: 4403
	public const int kStatusCount = 7;

	// Token: 0x04001134 RID: 4404
	public const int kRoleCount = 8;

	// Token: 0x04001135 RID: 4405
	private const uint one = 1u;

	// Token: 0x04001136 RID: 4406
	public const int kLifeMask = 7;

	// Token: 0x04001137 RID: 4407
	public const int kStatusMask = 32512;

	// Token: 0x04001138 RID: 4408
	public const int kRoleMask = -16777216;

	// Token: 0x04001139 RID: 4409
	private const int OpZero = 3;

	// Token: 0x0400113A RID: 4410
	private const int mask24b = 16777215;

	// Token: 0x0400113B RID: 4411
	private const int mask31b = 2147483647;

	// Token: 0x0400113C RID: 4412
	private const int mask24o7b = 16777216;

	// Token: 0x0400113D RID: 4413
	private const int mask31o1b = -2147483648;

	// Token: 0x0400113E RID: 4414
	private const byte mask7b = 127;

	// Token: 0x0400113F RID: 4415
	private const byte mask7o1b = 128;

	// Token: 0x04001140 RID: 4416
	public const global::Vis.Life kLifeNone = (global::Vis.Life)0;

	// Token: 0x04001141 RID: 4417
	public const global::Vis.Life kLifeAll = global::Vis.Life.Alive | global::Vis.Life.Unconcious | global::Vis.Life.Dead;

	// Token: 0x04001142 RID: 4418
	public const global::Vis.Status kStatusNone = (global::Vis.Status)0;

	// Token: 0x04001143 RID: 4419
	public const global::Vis.Status kStatusAll = global::Vis.Status.Casual | global::Vis.Status.Hurt | global::Vis.Status.Curious | global::Vis.Status.Alert | global::Vis.Status.Search | global::Vis.Status.Armed | global::Vis.Status.Attacking;

	// Token: 0x04001144 RID: 4420
	public const global::Vis.Role kRoleNone = (global::Vis.Role)0;

	// Token: 0x04001145 RID: 4421
	public const global::Vis.Role kRoleAll = (global::Vis.Role)(-1);

	// Token: 0x04001146 RID: 4422
	public const int kFlagRelative = 1;

	// Token: 0x04001147 RID: 4423
	public const int kFlagTarget = 4;

	// Token: 0x04001148 RID: 4424
	public const int kFlagSelf = 8;

	// Token: 0x04001149 RID: 4425
	public const int kComparisonStealthy = 5;

	// Token: 0x0400114A RID: 4426
	public const int kComparisonPrey = 9;

	// Token: 0x0400114B RID: 4427
	public const int kComparisonIsSelf = 12;

	// Token: 0x0400114C RID: 4428
	public const int kComparisonOblivious = 1;

	// Token: 0x0400114D RID: 4429
	public const int kComparisonContact = 13;

	// Token: 0x02000428 RID: 1064
	public enum Trait
	{
		// Token: 0x0400114F RID: 4431
		Alive,
		// Token: 0x04001150 RID: 4432
		Unconcious,
		// Token: 0x04001151 RID: 4433
		Dead,
		// Token: 0x04001152 RID: 4434
		Casual = 8,
		// Token: 0x04001153 RID: 4435
		Hurt,
		// Token: 0x04001154 RID: 4436
		Curious,
		// Token: 0x04001155 RID: 4437
		Alert,
		// Token: 0x04001156 RID: 4438
		Search,
		// Token: 0x04001157 RID: 4439
		Armed,
		// Token: 0x04001158 RID: 4440
		Attacking,
		// Token: 0x04001159 RID: 4441
		Citizen = 24,
		// Token: 0x0400115A RID: 4442
		Criminal,
		// Token: 0x0400115B RID: 4443
		Authority,
		// Token: 0x0400115C RID: 4444
		Target,
		// Token: 0x0400115D RID: 4445
		Entourage,
		// Token: 0x0400115E RID: 4446
		Player,
		// Token: 0x0400115F RID: 4447
		Vehicle,
		// Token: 0x04001160 RID: 4448
		Animal
	}

	// Token: 0x02000429 RID: 1065
	public enum Op
	{
		// Token: 0x04001162 RID: 4450
		Always,
		// Token: 0x04001163 RID: 4451
		Equals,
		// Token: 0x04001164 RID: 4452
		All,
		// Token: 0x04001165 RID: 4453
		Any,
		// Token: 0x04001166 RID: 4454
		None,
		// Token: 0x04001167 RID: 4455
		NotEquals,
		// Token: 0x04001168 RID: 4456
		Never
	}

	// Token: 0x0200042A RID: 1066
	private static class EnumUtil<TEnum> where TEnum : struct, IConvertible, IFormattable, IComparable
	{
		// Token: 0x06002543 RID: 9539 RVA: 0x00088AA4 File Offset: 0x00086CA4
		public static int ToInt(TEnum val)
		{
			return Convert.ToInt32(val);
		}
	}

	// Token: 0x0200042B RID: 1067
	[StructLayout(LayoutKind.Explicit, Size = 4)]
	private struct OpBase
	{
		// Token: 0x06002544 RID: 9540 RVA: 0x00088AB4 File Offset: 0x00086CB4
		public OpBase(byte _op, int _val)
		{
			this._val = _val;
			this._op = _op;
		}

		// Token: 0x04001169 RID: 4457
		[FieldOffset(0)]
		public int _val;

		// Token: 0x0400116A RID: 4458
		[FieldOffset(3)]
		public byte _op;
	}

	// Token: 0x0200042C RID: 1068
	public struct Op<TFlags> : IEquatable<global::Vis.Op>, IEquatable<global::Vis.Op<TFlags>> where TFlags : struct, IConvertible, IFormattable, IComparable
	{
		// Token: 0x06002545 RID: 9541 RVA: 0x00088AC4 File Offset: 0x00086CC4
		internal Op(byte op, int val)
		{
			this._ = new global::Vis.OpBase(op, val);
		}

		// Token: 0x06002546 RID: 9542 RVA: 0x00088AD4 File Offset: 0x00086CD4
		public Op(global::Vis.Op op, TFlags flags)
		{
			this = new global::Vis.Op<TFlags>((byte)op, Convert.ToInt32(flags));
		}

		// Token: 0x06002547 RID: 9543 RVA: 0x00088AEC File Offset: 0x00086CEC
		internal Op(int op, int flags)
		{
			this = new global::Vis.Op<TFlags>((byte)op, flags);
		}

		// Token: 0x06002548 RID: 9544 RVA: 0x00088AF8 File Offset: 0x00086CF8
		private static int ToInt(TFlags f)
		{
			return global::Vis.EnumUtil<TFlags>.ToInt(f);
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06002549 RID: 9545 RVA: 0x00088B00 File Offset: 0x00086D00
		// (set) Token: 0x0600254A RID: 9546 RVA: 0x00088B10 File Offset: 0x00086D10
		private int _val
		{
			get
			{
				return this._._val;
			}
			set
			{
				this._._val = value;
			}
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x0600254B RID: 9547 RVA: 0x00088B20 File Offset: 0x00086D20
		// (set) Token: 0x0600254C RID: 9548 RVA: 0x00088B30 File Offset: 0x00086D30
		private byte _op
		{
			get
			{
				return this._._op;
			}
			set
			{
				this._._op = value;
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x0600254D RID: 9549 RVA: 0x00088B40 File Offset: 0x00086D40
		// (set) Token: 0x0600254E RID: 9550 RVA: 0x00088B70 File Offset: 0x00086D70
		public TFlags value
		{
			get
			{
				return (TFlags)((object)Enum.ToObject(typeof(TFlags), this._val & 16777215));
			}
			set
			{
				this._val = ((this._val & 16777216) | (global::Vis.Op<TFlags>.ToInt(value) & 16777215));
			}
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x0600254F RID: 9551 RVA: 0x00088B94 File Offset: 0x00086D94
		// (set) Token: 0x06002550 RID: 9552 RVA: 0x00088BA4 File Offset: 0x00086DA4
		public int intvalue
		{
			get
			{
				return this._val & 16777216;
			}
			set
			{
				this._val = ((this._val & 16777216) | (value & 16777215));
			}
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06002551 RID: 9553 RVA: 0x00088BC0 File Offset: 0x00086DC0
		// (set) Token: 0x06002552 RID: 9554 RVA: 0x00088BCC File Offset: 0x00086DCC
		public global::Vis.Op op
		{
			get
			{
				return (global::Vis.Op)(this._op & 127);
			}
			set
			{
				this._op = ((this._op & 128) | ((byte)value & 127));
			}
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06002553 RID: 9555 RVA: 0x00088BE8 File Offset: 0x00086DE8
		// (set) Token: 0x06002554 RID: 9556 RVA: 0x00088BF0 File Offset: 0x00086DF0
		public int data
		{
			get
			{
				return this._val;
			}
			set
			{
				this._val = value;
			}
		}

		// Token: 0x06002555 RID: 9557 RVA: 0x00088BFC File Offset: 0x00086DFC
		public override int GetHashCode()
		{
			return this._val & int.MaxValue;
		}

		// Token: 0x06002556 RID: 9558 RVA: 0x00088C0C File Offset: 0x00086E0C
		public override string ToString()
		{
			return this.op + ':' + this.value;
		}

		// Token: 0x06002557 RID: 9559 RVA: 0x00088C3C File Offset: 0x00086E3C
		public override bool Equals(object obj)
		{
			if (obj is global::Vis.Op<TFlags>)
			{
				return this.Equals((global::Vis.Op<TFlags>)obj);
			}
			if (obj is global::Vis.Op)
			{
				return this.Equals((global::Vis.Op)((int)obj));
			}
			return obj.Equals(this);
		}

		// Token: 0x06002558 RID: 9560 RVA: 0x00088C8C File Offset: 0x00086E8C
		public bool Equals(global::Vis.Op<TFlags> other)
		{
			return other._val == this;
		}

		// Token: 0x06002559 RID: 9561 RVA: 0x00088CA0 File Offset: 0x00086EA0
		public bool Equals(global::Vis.Op other)
		{
			return other == this.op;
		}

		// Token: 0x0600255A RID: 9562 RVA: 0x00088CAC File Offset: 0x00086EAC
		public global::Vis.Op<TFlags>.Res Eval(int flags)
		{
			return new global::Vis.Op<TFlags>.Res(this, (TFlags)((object)Enum.ToObject(typeof(TFlags), flags)), flags);
		}

		// Token: 0x0600255B RID: 9563 RVA: 0x00088CD0 File Offset: 0x00086ED0
		public global::Vis.Op<TFlags>.Res Eval(TFlags flags)
		{
			return new global::Vis.Op<TFlags>.Res(this, flags, global::Vis.Op<TFlags>.ToInt(flags));
		}

		// Token: 0x0600255C RID: 9564 RVA: 0x00088CE4 File Offset: 0x00086EE4
		public static bool operator ==(global::Vis.Op<TFlags> op, TFlags flags)
		{
			return global::Vis.Evaluate(op.op, op._val & 16777215, global::Vis.Op<TFlags>.ToInt(flags));
		}

		// Token: 0x0600255D RID: 9565 RVA: 0x00088D10 File Offset: 0x00086F10
		public static bool operator ==(TFlags flags, global::Vis.Op<TFlags> op)
		{
			return global::Vis.Evaluate(op.op, op._val & 16777215, global::Vis.Op<TFlags>.ToInt(flags));
		}

		// Token: 0x0600255E RID: 9566 RVA: 0x00088D3C File Offset: 0x00086F3C
		public static bool operator !=(global::Vis.Op<TFlags> op, TFlags flags)
		{
			return !global::Vis.Evaluate(op.op, op._val & 16777215, global::Vis.Op<TFlags>.ToInt(flags));
		}

		// Token: 0x0600255F RID: 9567 RVA: 0x00088D6C File Offset: 0x00086F6C
		public static bool operator !=(TFlags flags, global::Vis.Op<TFlags> op)
		{
			return !global::Vis.Evaluate(op.op, op._val & 16777215, global::Vis.Op<TFlags>.ToInt(flags));
		}

		// Token: 0x06002560 RID: 9568 RVA: 0x00088D9C File Offset: 0x00086F9C
		public static global::Vis.Op<TFlags>.Res operator +(global::Vis.Op<TFlags> op, TFlags flags)
		{
			return op.Eval(flags);
		}

		// Token: 0x06002561 RID: 9569 RVA: 0x00088DA8 File Offset: 0x00086FA8
		public static global::Vis.Op<TFlags>.Res operator +(global::Vis.Op<TFlags> op, int flags)
		{
			return op.Eval(flags);
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x00088DB4 File Offset: 0x00086FB4
		public static global::Vis.Op<TFlags>.Res operator -(global::Vis.Op<TFlags> op, TFlags flags)
		{
			return op.Eval(flags);
		}

		// Token: 0x06002563 RID: 9571 RVA: 0x00088DC0 File Offset: 0x00086FC0
		public static global::Vis.Op<TFlags>.Res operator -(global::Vis.Op<TFlags> op, int flags)
		{
			return op.Eval(flags);
		}

		// Token: 0x06002564 RID: 9572 RVA: 0x00088DCC File Offset: 0x00086FCC
		public static global::Vis.Op<TFlags>operator -(global::Vis.Op<TFlags> op)
		{
			op.op = global::Vis.Negate(op.op);
			return op;
		}

		// Token: 0x06002565 RID: 9573 RVA: 0x00088DE4 File Offset: 0x00086FE4
		public static bool operator ==(global::Vis.Op<TFlags> L, global::Vis.Op R)
		{
			return (int)L._op == (int)((sbyte)R);
		}

		// Token: 0x06002566 RID: 9574 RVA: 0x00088DF4 File Offset: 0x00086FF4
		public static bool operator ==(global::Vis.Op L, global::Vis.Op<TFlags> R)
		{
			return (int)R._op == (int)((sbyte)L);
		}

		// Token: 0x06002567 RID: 9575 RVA: 0x00088E04 File Offset: 0x00087004
		public static bool operator !=(global::Vis.Op<TFlags> L, global::Vis.Op R)
		{
			return (int)L._op != (int)((sbyte)R);
		}

		// Token: 0x06002568 RID: 9576 RVA: 0x00088E18 File Offset: 0x00087018
		public static bool operator !=(global::Vis.Op L, global::Vis.Op<TFlags> R)
		{
			return (int)R._op != (int)((sbyte)L);
		}

		// Token: 0x06002569 RID: 9577 RVA: 0x00088E2C File Offset: 0x0008702C
		public static bool operator ==(global::Vis.Op<TFlags> L, int R)
		{
			return L._val == R;
		}

		// Token: 0x0600256A RID: 9578 RVA: 0x00088E38 File Offset: 0x00087038
		public static bool operator ==(int R, global::Vis.Op<TFlags> L)
		{
			return L._val == R;
		}

		// Token: 0x0600256B RID: 9579 RVA: 0x00088E44 File Offset: 0x00087044
		public static bool operator !=(global::Vis.Op<TFlags> L, int R)
		{
			return L._val != R;
		}

		// Token: 0x0600256C RID: 9580 RVA: 0x00088E54 File Offset: 0x00087054
		public static bool operator !=(int R, global::Vis.Op<TFlags> L)
		{
			return L._val != R;
		}

		// Token: 0x0600256D RID: 9581 RVA: 0x00088E64 File Offset: 0x00087064
		public static bool operator ==(global::Vis.Op<TFlags> L, global::Vis.Op<TFlags> R)
		{
			return L._val == R._val;
		}

		// Token: 0x0600256E RID: 9582 RVA: 0x00088E78 File Offset: 0x00087078
		public static bool operator !=(global::Vis.Op<TFlags> L, global::Vis.Op<TFlags> R)
		{
			return L._val != R._val;
		}

		// Token: 0x0600256F RID: 9583 RVA: 0x00088E90 File Offset: 0x00087090
		public static implicit operator global::Vis.Op<TFlags>(int data)
		{
			return new global::Vis.Op<TFlags>
			{
				_val = data
			};
		}

		// Token: 0x06002570 RID: 9584 RVA: 0x00088EB0 File Offset: 0x000870B0
		public static implicit operator int(global::Vis.Op<TFlags> op)
		{
			return op._val;
		}

		// Token: 0x06002571 RID: 9585 RVA: 0x00088EBC File Offset: 0x000870BC
		public static implicit operator global::Vis.Op(global::Vis.Op<TFlags> op)
		{
			return op.op;
		}

		// Token: 0x0400116B RID: 4459
		private global::Vis.OpBase _;

		// Token: 0x0200042D RID: 1069
		public struct Res
		{
			// Token: 0x06002572 RID: 9586 RVA: 0x00088EC8 File Offset: 0x000870C8
			internal Res(global::Vis.Op<TFlags> op, TFlags flags, int flagsint)
			{
				this._op = op;
				this.query = flags;
				if (global::Vis.Evaluate(op.op, op.intvalue, flagsint))
				{
					this._op._val = (this._op._val | int.MinValue);
				}
				else
				{
					this._op._val = (this._op._val & int.MaxValue);
				}
			}

			// Token: 0x1700089F RID: 2207
			// (get) Token: 0x06002573 RID: 9587 RVA: 0x00088F30 File Offset: 0x00087130
			public global::Vis.Op<TFlags> operation
			{
				get
				{
					return this._op;
				}
			}

			// Token: 0x170008A0 RID: 2208
			// (get) Token: 0x06002574 RID: 9588 RVA: 0x00088F38 File Offset: 0x00087138
			public bool passed
			{
				get
				{
					return (this._op._val & int.MinValue) == int.MinValue;
				}
			}

			// Token: 0x170008A1 RID: 2209
			// (get) Token: 0x06002575 RID: 9589 RVA: 0x00088F60 File Offset: 0x00087160
			public bool failed
			{
				get
				{
					return (this._op._val & int.MinValue) != int.MinValue;
				}
			}

			// Token: 0x170008A2 RID: 2210
			// (get) Token: 0x06002576 RID: 9590 RVA: 0x00088F8C File Offset: 0x0008718C
			public TFlags value
			{
				get
				{
					return this._op.value;
				}
			}

			// Token: 0x170008A3 RID: 2211
			// (get) Token: 0x06002577 RID: 9591 RVA: 0x00088FA8 File Offset: 0x000871A8
			public int intvalue
			{
				get
				{
					return this._op.intvalue;
				}
			}

			// Token: 0x170008A4 RID: 2212
			// (get) Token: 0x06002578 RID: 9592 RVA: 0x00088FC4 File Offset: 0x000871C4
			public int data
			{
				get
				{
					return this._op._val;
				}
			}

			// Token: 0x06002579 RID: 9593 RVA: 0x00088FE0 File Offset: 0x000871E0
			public override int GetHashCode()
			{
				return (int.MinValue | this._op._val) ^ global::Vis.Op<TFlags>.ToInt(this.query);
			}

			// Token: 0x0600257A RID: 9594 RVA: 0x00089010 File Offset: 0x00087210
			public override string ToString()
			{
				return string.Format("{0}({1}) == {2}", this.operation, this.query, this.passed);
			}

			// Token: 0x0600257B RID: 9595 RVA: 0x00089048 File Offset: 0x00087248
			public static implicit operator bool(global::Vis.Op<TFlags>.Res r)
			{
				return r.passed;
			}

			// Token: 0x0600257C RID: 9596 RVA: 0x00089054 File Offset: 0x00087254
			public static bool operator !(global::Vis.Op<TFlags>.Res r)
			{
				return r.failed;
			}

			// Token: 0x0400116C RID: 4460
			public readonly TFlags query;

			// Token: 0x0400116D RID: 4461
			private readonly global::Vis.Op<TFlags> _op;
		}
	}

	// Token: 0x0200042E RID: 1070
	[Flags]
	public enum Life
	{
		// Token: 0x0400116F RID: 4463
		Alive = 1,
		// Token: 0x04001170 RID: 4464
		Unconcious = 2,
		// Token: 0x04001171 RID: 4465
		Dead = 4
	}

	// Token: 0x0200042F RID: 1071
	[Flags]
	public enum Status
	{
		// Token: 0x04001173 RID: 4467
		Casual = 1,
		// Token: 0x04001174 RID: 4468
		Hurt = 2,
		// Token: 0x04001175 RID: 4469
		Curious = 4,
		// Token: 0x04001176 RID: 4470
		Alert = 8,
		// Token: 0x04001177 RID: 4471
		Search = 16,
		// Token: 0x04001178 RID: 4472
		Armed = 32,
		// Token: 0x04001179 RID: 4473
		Attacking = 64
	}

	// Token: 0x02000430 RID: 1072
	[Flags]
	public enum Role
	{
		// Token: 0x0400117B RID: 4475
		Citizen = 1,
		// Token: 0x0400117C RID: 4476
		Criminal = 2,
		// Token: 0x0400117D RID: 4477
		Authority = 4,
		// Token: 0x0400117E RID: 4478
		Target = 8,
		// Token: 0x0400117F RID: 4479
		Entourage = 16,
		// Token: 0x04001180 RID: 4480
		Player = 32,
		// Token: 0x04001181 RID: 4481
		Vehicle = 64,
		// Token: 0x04001182 RID: 4482
		Animal = 128
	}

	// Token: 0x02000431 RID: 1073
	[StructLayout(LayoutKind.Explicit, Size = 4)]
	public struct Mask
	{
		// Token: 0x0600257D RID: 9597 RVA: 0x00089060 File Offset: 0x00087260
		static Mask()
		{
			int num = 0;
			global::Vis.Mask.sect(0, ref num);
			BitVector32.Section? section = new BitVector32.Section?(global::Vis.Mask.sect(3, ref num));
			global::Vis.Mask.sect(5, ref num);
			BitVector32.Section? section2 = new BitVector32.Section?(global::Vis.Mask.sect(7, ref num));
			global::Vis.Mask.sect(9, ref num);
			BitVector32.Section? section3 = new BitVector32.Section?(global::Vis.Mask.sect(8, ref num));
			global::Vis.Mask.s_life = section.GetValueOrDefault();
			global::Vis.Mask.s_stat = section2.GetValueOrDefault();
			global::Vis.Mask.s_role = section3.GetValueOrDefault();
		}

		// Token: 0x0600257E RID: 9598 RVA: 0x000890E8 File Offset: 0x000872E8
		private static BitVector32.Section sect_(int count, ref int i)
		{
			if (count == 0)
			{
				return default(BitVector32.Section);
			}
			if (i == 0)
			{
				BitVector32.Section result = BitVector32.CreateSection((short)((1 << count) - 1));
				i += count;
				return result;
			}
			int j = i;
			BitVector32.Section previous;
			if (j >= 8)
			{
				previous = BitVector32.CreateSection(255);
				for (j -= 8; j >= 8; j -= 8)
				{
					previous = BitVector32.CreateSection(255, previous);
				}
				if (j > 0)
				{
					previous = BitVector32.CreateSection((short)((1 << j) - 1), previous);
				}
			}
			else
			{
				previous = BitVector32.CreateSection((short)((1 << j) - 1));
			}
			BitVector32.Section result2 = BitVector32.CreateSection((short)((1 << count) - 1), previous);
			i += count;
			return result2;
		}

		// Token: 0x0600257F RID: 9599 RVA: 0x0008919C File Offset: 0x0008739C
		private static BitVector32.Section sect(int count, ref int i)
		{
			return global::Vis.Mask.sect_(count, ref i);
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06002580 RID: 9600 RVA: 0x000891A8 File Offset: 0x000873A8
		// (set) Token: 0x06002581 RID: 9601 RVA: 0x000891BC File Offset: 0x000873BC
		public global::Vis.Life life
		{
			get
			{
				return (global::Vis.Life)this.bits[global::Vis.Mask.s_life];
			}
			set
			{
				this.bits[global::Vis.Mask.s_life] = (int)(value & (global::Vis.Life)global::Vis.Mask.s_life.Mask);
			}
		}

		// Token: 0x170008A6 RID: 2214
		public bool this[global::Vis.Life mask]
		{
			get
			{
				return (this.life & mask) != (global::Vis.Life)0;
			}
			set
			{
				if (value)
				{
					this.life |= mask;
				}
				else
				{
					this.life &= ~mask;
				}
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06002584 RID: 9604 RVA: 0x00089224 File Offset: 0x00087424
		// (set) Token: 0x06002585 RID: 9605 RVA: 0x00089238 File Offset: 0x00087438
		public global::Vis.Status stat
		{
			get
			{
				return (global::Vis.Status)this.bits[global::Vis.Mask.s_stat];
			}
			set
			{
				this.bits[global::Vis.Mask.s_stat] = (int)(value & (global::Vis.Status)global::Vis.Mask.s_stat.Mask);
			}
		}

		// Token: 0x170008A8 RID: 2216
		public bool this[global::Vis.Status mask]
		{
			get
			{
				return (this.stat & mask) != (global::Vis.Status)0;
			}
			set
			{
				if (value)
				{
					this.stat |= mask;
				}
				else
				{
					this.stat &= ~mask;
				}
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06002588 RID: 9608 RVA: 0x000892A0 File Offset: 0x000874A0
		// (set) Token: 0x06002589 RID: 9609 RVA: 0x000892B4 File Offset: 0x000874B4
		public global::Vis.Role role
		{
			get
			{
				return (global::Vis.Role)this.bits[global::Vis.Mask.s_role];
			}
			set
			{
				this.bits[global::Vis.Mask.s_role] = (int)(value & (global::Vis.Role)global::Vis.Mask.s_role.Mask);
			}
		}

		// Token: 0x170008AA RID: 2218
		public bool this[global::Vis.Role mask]
		{
			get
			{
				return (this.role & mask) != (global::Vis.Role)0;
			}
			set
			{
				if (value)
				{
					this.role |= mask;
				}
				else
				{
					this.role &= ~mask;
				}
			}
		}

		// Token: 0x170008AB RID: 2219
		public bool this[global::Vis.Op op, global::Vis.Life val]
		{
			get
			{
				return this.Eval(op, val);
			}
			set
			{
				this.Apply(op, val);
			}
		}

		// Token: 0x170008AC RID: 2220
		public bool this[global::Vis.Op op, global::Vis.Status val]
		{
			get
			{
				return this.Eval(op, val);
			}
			set
			{
				this.Apply(op, val);
			}
		}

		// Token: 0x170008AD RID: 2221
		public bool this[global::Vis.Op op, global::Vis.Role val]
		{
			get
			{
				return this.Eval(op, val);
			}
			set
			{
				this.Apply(op, val);
			}
		}

		// Token: 0x170008AE RID: 2222
		public global::Vis.Op<global::Vis.Life>.Res this[global::Vis.Op<global::Vis.Life> op]
		{
			get
			{
				return op.Eval(this.bits[global::Vis.Mask.s_life]);
			}
		}

		// Token: 0x170008AF RID: 2223
		public global::Vis.Op<global::Vis.Status>.Res this[global::Vis.Op<global::Vis.Status> op]
		{
			get
			{
				return op.Eval(this.bits[global::Vis.Mask.s_stat]);
			}
		}

		// Token: 0x170008B0 RID: 2224
		public global::Vis.Op<global::Vis.Role>.Res this[global::Vis.Op<global::Vis.Role> op]
		{
			get
			{
				return op.Eval(this.bits[global::Vis.Mask.s_role]);
			}
		}

		// Token: 0x06002595 RID: 9621 RVA: 0x000893B8 File Offset: 0x000875B8
		public bool All(global::Vis.Life f)
		{
			return (this.life & f) == f;
		}

		// Token: 0x06002596 RID: 9622 RVA: 0x000893C8 File Offset: 0x000875C8
		public bool All(global::Vis.Role f)
		{
			return (this.role & f) == f;
		}

		// Token: 0x06002597 RID: 9623 RVA: 0x000893D8 File Offset: 0x000875D8
		public bool All(global::Vis.Status f)
		{
			return (this.stat & f) == f;
		}

		// Token: 0x06002598 RID: 9624 RVA: 0x000893E8 File Offset: 0x000875E8
		public bool Any(global::Vis.Life f)
		{
			return (this.life & f) > (global::Vis.Life)0;
		}

		// Token: 0x06002599 RID: 9625 RVA: 0x000893F8 File Offset: 0x000875F8
		public bool Any(global::Vis.Role f)
		{
			return (this.role & f) > (global::Vis.Role)0;
		}

		// Token: 0x0600259A RID: 9626 RVA: 0x00089408 File Offset: 0x00087608
		public bool Any(global::Vis.Status f)
		{
			return (this.stat & f) > (global::Vis.Status)0;
		}

		// Token: 0x0600259B RID: 9627 RVA: 0x00089418 File Offset: 0x00087618
		public bool AllMore(global::Vis.Life f)
		{
			global::Vis.Life life = this.life;
			return life > f && (life & f) == f;
		}

		// Token: 0x0600259C RID: 9628 RVA: 0x0008943C File Offset: 0x0008763C
		public bool AllMore(global::Vis.Role f)
		{
			global::Vis.Role role = this.role;
			return role > f && (role & f) == f;
		}

		// Token: 0x0600259D RID: 9629 RVA: 0x00089460 File Offset: 0x00087660
		public bool AllMore(global::Vis.Status f)
		{
			global::Vis.Status stat = this.stat;
			return stat > f && (stat & f) == f;
		}

		// Token: 0x0600259E RID: 9630 RVA: 0x00089484 File Offset: 0x00087684
		public bool AnyLess(global::Vis.Life f)
		{
			global::Vis.Life life = this.life;
			return (life & f) < f;
		}

		// Token: 0x0600259F RID: 9631 RVA: 0x000894A0 File Offset: 0x000876A0
		public bool AnyLess(global::Vis.Role f)
		{
			global::Vis.Role role = this.role;
			return (role & f) < f;
		}

		// Token: 0x060025A0 RID: 9632 RVA: 0x000894BC File Offset: 0x000876BC
		public bool AnyLess(global::Vis.Status f)
		{
			global::Vis.Status stat = this.stat;
			return (stat & f) < f;
		}

		// Token: 0x060025A1 RID: 9633 RVA: 0x000894D8 File Offset: 0x000876D8
		public bool Equals(global::Vis.Life f)
		{
			return this.life == f;
		}

		// Token: 0x060025A2 RID: 9634 RVA: 0x000894E4 File Offset: 0x000876E4
		public bool Equals(global::Vis.Role f)
		{
			return this.role == f;
		}

		// Token: 0x060025A3 RID: 9635 RVA: 0x000894F0 File Offset: 0x000876F0
		public bool Equals(global::Vis.Status f)
		{
			return this.stat == f;
		}

		// Token: 0x060025A4 RID: 9636 RVA: 0x000894FC File Offset: 0x000876FC
		public void Append(global::Vis.Life f)
		{
			this.life |= f;
		}

		// Token: 0x060025A5 RID: 9637 RVA: 0x0008950C File Offset: 0x0008770C
		public void Append(global::Vis.Role f)
		{
			this.role |= f;
		}

		// Token: 0x060025A6 RID: 9638 RVA: 0x0008951C File Offset: 0x0008771C
		public void Append(global::Vis.Status f)
		{
			this.stat |= f;
		}

		// Token: 0x060025A7 RID: 9639 RVA: 0x0008952C File Offset: 0x0008772C
		public global::Vis.Life Not(global::Vis.Life f)
		{
			return (this.life ^ f) & f;
		}

		// Token: 0x060025A8 RID: 9640 RVA: 0x00089538 File Offset: 0x00087738
		public global::Vis.Role Not(global::Vis.Role f)
		{
			return (this.role ^ f) & f;
		}

		// Token: 0x060025A9 RID: 9641 RVA: 0x00089544 File Offset: 0x00087744
		public global::Vis.Status Not(global::Vis.Status f)
		{
			return (this.stat ^ f) & f;
		}

		// Token: 0x060025AA RID: 9642 RVA: 0x00089550 File Offset: 0x00087750
		public global::Vis.Life AppendNot(global::Vis.Life f)
		{
			global::Vis.Life life = (this.life ^ f) & f;
			this.life |= life;
			return life;
		}

		// Token: 0x060025AB RID: 9643 RVA: 0x00089578 File Offset: 0x00087778
		public global::Vis.Role AppendNot(global::Vis.Role f)
		{
			global::Vis.Role role = (this.role ^ f) & f;
			this.role |= role;
			return role;
		}

		// Token: 0x060025AC RID: 9644 RVA: 0x000895A0 File Offset: 0x000877A0
		public global::Vis.Status AppendNot(global::Vis.Status f)
		{
			global::Vis.Status status = (this.stat ^ f) & f;
			this.stat |= status;
			return status;
		}

		// Token: 0x060025AD RID: 9645 RVA: 0x000895C8 File Offset: 0x000877C8
		public bool Eval(global::Vis.Op op, global::Vis.Life f)
		{
			return global::Vis.Evaluate(op, (int)f, this.bits[global::Vis.Mask.s_life]);
		}

		// Token: 0x060025AE RID: 9646 RVA: 0x000895E4 File Offset: 0x000877E4
		public bool Eval(global::Vis.Op<global::Vis.Life> op)
		{
			return op == this.life;
		}

		// Token: 0x060025AF RID: 9647 RVA: 0x000895F4 File Offset: 0x000877F4
		public global::Vis.Life Apply(global::Vis.Op op, global::Vis.Life f)
		{
			return (global::Vis.Life)global::Vis.SetTrue(op, (int)f, ref this.bits, global::Vis.Mask.s_life);
		}

		// Token: 0x060025B0 RID: 9648 RVA: 0x00089608 File Offset: 0x00087808
		public global::Vis.Life Apply(global::Vis.Op<global::Vis.Life> op)
		{
			return (global::Vis.Life)global::Vis.SetTrue(op, op.intvalue, ref this.bits, global::Vis.Mask.s_life);
		}

		// Token: 0x060025B1 RID: 9649 RVA: 0x00089628 File Offset: 0x00087828
		public bool Eval(global::Vis.Op op, global::Vis.Status f)
		{
			return global::Vis.Evaluate(op, (int)f, this.bits[global::Vis.Mask.s_stat]);
		}

		// Token: 0x060025B2 RID: 9650 RVA: 0x00089644 File Offset: 0x00087844
		public bool Eval(global::Vis.Op<global::Vis.Status> op)
		{
			return op == this.stat;
		}

		// Token: 0x060025B3 RID: 9651 RVA: 0x00089654 File Offset: 0x00087854
		public global::Vis.Status Apply(global::Vis.Op op, global::Vis.Status f)
		{
			return (global::Vis.Status)global::Vis.SetTrue(op, (int)f, ref this.bits, global::Vis.Mask.s_stat);
		}

		// Token: 0x060025B4 RID: 9652 RVA: 0x00089668 File Offset: 0x00087868
		public global::Vis.Status Apply(global::Vis.Op<global::Vis.Status> op)
		{
			return (global::Vis.Status)global::Vis.SetTrue(op, op.intvalue, ref this.bits, global::Vis.Mask.s_stat);
		}

		// Token: 0x060025B5 RID: 9653 RVA: 0x00089688 File Offset: 0x00087888
		public bool Eval(global::Vis.Op op, global::Vis.Role f)
		{
			return global::Vis.Evaluate(op, (int)f, this.bits[global::Vis.Mask.s_role]);
		}

		// Token: 0x060025B6 RID: 9654 RVA: 0x000896A4 File Offset: 0x000878A4
		public bool Eval(global::Vis.Op<global::Vis.Role> op)
		{
			return op == this.role;
		}

		// Token: 0x060025B7 RID: 9655 RVA: 0x000896B4 File Offset: 0x000878B4
		public global::Vis.Role Apply(global::Vis.Op op, global::Vis.Role f)
		{
			return (global::Vis.Role)global::Vis.SetTrue(op, (int)f, ref this.bits, global::Vis.Mask.s_role);
		}

		// Token: 0x060025B8 RID: 9656 RVA: 0x000896C8 File Offset: 0x000878C8
		public global::Vis.Role Apply(global::Vis.Op<global::Vis.Role> op)
		{
			return (global::Vis.Role)global::Vis.SetTrue(op, op.intvalue, ref this.bits, global::Vis.Mask.s_role);
		}

		// Token: 0x060025B9 RID: 9657 RVA: 0x000896E8 File Offset: 0x000878E8
		public void Remove(global::Vis.Life f)
		{
			this.life &= ~f;
		}

		// Token: 0x060025BA RID: 9658 RVA: 0x000896FC File Offset: 0x000878FC
		public void Remove(global::Vis.Role f)
		{
			this.role &= ~f;
		}

		// Token: 0x060025BB RID: 9659 RVA: 0x00089710 File Offset: 0x00087910
		public void Remove(global::Vis.Status f)
		{
			this.stat &= ~f;
		}

		// Token: 0x170008B1 RID: 2225
		public bool this[global::Vis.Trait trait]
		{
			get
			{
				return this.bits[1 << (int)trait];
			}
		}

		// Token: 0x170008B2 RID: 2226
		public bool this[int mask]
		{
			get
			{
				return this.bits[mask];
			}
		}

		// Token: 0x04001183 RID: 4483
		public const int kAlive = 1;

		// Token: 0x04001184 RID: 4484
		public const int kUnconcious = 2;

		// Token: 0x04001185 RID: 4485
		public const int kDead = 4;

		// Token: 0x04001186 RID: 4486
		public const int kCasual = 256;

		// Token: 0x04001187 RID: 4487
		public const int kHurt = 512;

		// Token: 0x04001188 RID: 4488
		public const int kCurious = 1024;

		// Token: 0x04001189 RID: 4489
		public const int kAlert = 2048;

		// Token: 0x0400118A RID: 4490
		public const int kSearch = 4096;

		// Token: 0x0400118B RID: 4491
		public const int kArmed = 8192;

		// Token: 0x0400118C RID: 4492
		public const int kAttacking = 16384;

		// Token: 0x0400118D RID: 4493
		public const int kCriminal = 33554432;

		// Token: 0x0400118E RID: 4494
		public const int kAuthority = 67108864;

		// Token: 0x0400118F RID: 4495
		private static BitVector32.Section s_life;

		// Token: 0x04001190 RID: 4496
		private static BitVector32.Section s_stat;

		// Token: 0x04001191 RID: 4497
		private static BitVector32.Section s_role;

		// Token: 0x04001192 RID: 4498
		[FieldOffset(0)]
		public BitVector32 bits;

		// Token: 0x04001193 RID: 4499
		[FieldOffset(0)]
		public int data;

		// Token: 0x04001194 RID: 4500
		[FieldOffset(0)]
		public uint udata;

		// Token: 0x04001195 RID: 4501
		public static readonly global::Vis.Mask zero = default(global::Vis.Mask);
	}

	// Token: 0x02000432 RID: 1074
	[Flags]
	public enum Flag
	{
		// Token: 0x04001197 RID: 4503
		Zero = 0,
		// Token: 0x04001198 RID: 4504
		Relative = 1,
		// Token: 0x04001199 RID: 4505
		Target = 4,
		// Token: 0x0400119A RID: 4506
		Self = 8
	}

	// Token: 0x02000433 RID: 1075
	public enum Comparison
	{
		// Token: 0x0400119C RID: 4508
		Stealthy = 5,
		// Token: 0x0400119D RID: 4509
		Prey = 9,
		// Token: 0x0400119E RID: 4510
		IsSelf = 12,
		// Token: 0x0400119F RID: 4511
		Oblivious = 1,
		// Token: 0x040011A0 RID: 4512
		Contact = 13
	}

	// Token: 0x02000434 RID: 1076
	public enum Region
	{
		// Token: 0x040011A2 RID: 4514
		Life,
		// Token: 0x040011A3 RID: 4515
		Status,
		// Token: 0x040011A4 RID: 4516
		Role
	}

	// Token: 0x02000435 RID: 1077
	public struct Rule
	{
		// Token: 0x170008B3 RID: 2227
		public global::Vis.Mask this[global::Vis.Rule.Step step]
		{
			get
			{
				switch (step)
				{
				case global::Vis.Rule.Step.Accept:
					return this.accept;
				case global::Vis.Rule.Step.Conditional:
					return this.conditional;
				case global::Vis.Rule.Step.Reject:
					return this.reject;
				default:
					throw new ArgumentOutOfRangeException("step");
				}
			}
			set
			{
				switch (step)
				{
				case global::Vis.Rule.Step.Accept:
					this.accept = value;
					break;
				case global::Vis.Rule.Step.Conditional:
					this.conditional = value;
					break;
				case global::Vis.Rule.Step.Reject:
					this.reject = value;
					break;
				default:
					throw new ArgumentOutOfRangeException("step");
				}
			}
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x060025C0 RID: 9664 RVA: 0x000897E8 File Offset: 0x000879E8
		// (set) Token: 0x060025C1 RID: 9665 RVA: 0x0008981C File Offset: 0x00087A1C
		public global::Vis.Op<global::Vis.Life> rejectLife
		{
			get
			{
				return new global::Vis.Op<global::Vis.Life>((byte)this.setup.life.reject, (int)this.reject.life);
			}
			set
			{
				global::Vis.Rule.RegionSetup life = this.setup.life;
				life.reject = value.op;
				this.setup.life = life;
				this.reject.life = value.value;
			}
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x060025C2 RID: 9666 RVA: 0x00089864 File Offset: 0x00087A64
		// (set) Token: 0x060025C3 RID: 9667 RVA: 0x00089898 File Offset: 0x00087A98
		public global::Vis.Op<global::Vis.Status> rejectStatus
		{
			get
			{
				return new global::Vis.Op<global::Vis.Status>((byte)this.setup.stat.reject, (int)this.reject.stat);
			}
			set
			{
				global::Vis.Rule.RegionSetup life = this.setup.life;
				life.reject = value.op;
				this.setup.life = life;
				this.reject.stat = value.value;
			}
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x060025C4 RID: 9668 RVA: 0x000898E0 File Offset: 0x00087AE0
		// (set) Token: 0x060025C5 RID: 9669 RVA: 0x00089914 File Offset: 0x00087B14
		public global::Vis.Op<global::Vis.Role> rejectRole
		{
			get
			{
				return new global::Vis.Op<global::Vis.Role>((byte)this.setup.role.reject, (int)this.reject.role);
			}
			set
			{
				global::Vis.Rule.RegionSetup life = this.setup.life;
				life.reject = value.op;
				this.setup.life = life;
				this.reject.role = value.value;
			}
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x060025C6 RID: 9670 RVA: 0x0008995C File Offset: 0x00087B5C
		// (set) Token: 0x060025C7 RID: 9671 RVA: 0x00089990 File Offset: 0x00087B90
		public global::Vis.Op<global::Vis.Life> acceptLife
		{
			get
			{
				return new global::Vis.Op<global::Vis.Life>((byte)this.setup.life.accept, (int)this.accept.life);
			}
			set
			{
				global::Vis.Rule.RegionSetup life = this.setup.life;
				life.accept = value.op;
				this.setup.life = life;
				this.accept.life = value.value;
			}
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x060025C8 RID: 9672 RVA: 0x000899D8 File Offset: 0x00087BD8
		// (set) Token: 0x060025C9 RID: 9673 RVA: 0x00089A0C File Offset: 0x00087C0C
		public global::Vis.Op<global::Vis.Status> acceptStatus
		{
			get
			{
				return new global::Vis.Op<global::Vis.Status>((byte)this.setup.stat.accept, (int)this.accept.stat);
			}
			set
			{
				global::Vis.Rule.RegionSetup stat = this.setup.stat;
				stat.accept = value.op;
				this.setup.stat = stat;
				this.accept.stat = value.value;
			}
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x060025CA RID: 9674 RVA: 0x00089A54 File Offset: 0x00087C54
		// (set) Token: 0x060025CB RID: 9675 RVA: 0x00089A88 File Offset: 0x00087C88
		public global::Vis.Op<global::Vis.Role> acceptRole
		{
			get
			{
				return new global::Vis.Op<global::Vis.Role>((byte)this.setup.role.accept, (int)this.accept.role);
			}
			set
			{
				global::Vis.Rule.RegionSetup role = this.setup.role;
				role.accept = value.op;
				this.setup.role = role;
				this.accept.role = value.value;
			}
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x060025CC RID: 9676 RVA: 0x00089AD0 File Offset: 0x00087CD0
		// (set) Token: 0x060025CD RID: 9677 RVA: 0x00089B04 File Offset: 0x00087D04
		public global::Vis.Op<global::Vis.Life> conditionalLife
		{
			get
			{
				return new global::Vis.Op<global::Vis.Life>((byte)this.setup.life.conditional, (int)this.conditional.life);
			}
			set
			{
				global::Vis.Rule.RegionSetup life = this.setup.life;
				life.conditional = value.op;
				this.setup.life = life;
				this.conditional.life = value.value;
			}
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x060025CE RID: 9678 RVA: 0x00089B4C File Offset: 0x00087D4C
		// (set) Token: 0x060025CF RID: 9679 RVA: 0x00089B80 File Offset: 0x00087D80
		public global::Vis.Op<global::Vis.Status> conditionalStatus
		{
			get
			{
				return new global::Vis.Op<global::Vis.Status>((byte)this.setup.stat.conditional, (int)this.conditional.stat);
			}
			set
			{
				global::Vis.Rule.RegionSetup stat = this.setup.stat;
				stat.conditional = value.op;
				this.setup.stat = stat;
				this.conditional.stat = value.value;
			}
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x060025D0 RID: 9680 RVA: 0x00089BC8 File Offset: 0x00087DC8
		// (set) Token: 0x060025D1 RID: 9681 RVA: 0x00089BFC File Offset: 0x00087DFC
		public global::Vis.Op<global::Vis.Role> conditionalRole
		{
			get
			{
				return new global::Vis.Op<global::Vis.Role>((byte)this.setup.role.conditional, (int)this.conditional.role);
			}
			set
			{
				global::Vis.Rule.RegionSetup role = this.setup.role;
				role.conditional = value.op;
				this.setup.role = role;
				this.conditional.role = value.value;
			}
		}

		// Token: 0x060025D2 RID: 9682 RVA: 0x00089C44 File Offset: 0x00087E44
		private global::Vis.Rule.Failure Accept(global::Vis.Mask mask)
		{
			if (!this.setup.checkAccept)
			{
				return global::Vis.Rule.Failure.None;
			}
			global::Vis.Rule.Failure failure = global::Vis.Rule.Failure.None;
			if (!mask.Eval(this.acceptLife))
			{
				failure |= (global::Vis.Rule.Failure.Conditional | global::Vis.Rule.Failure.Life);
			}
			if (!mask.Eval(this.acceptRole))
			{
				failure |= (global::Vis.Rule.Failure.Conditional | global::Vis.Rule.Failure.Role);
			}
			if (!mask.Eval(this.acceptStatus))
			{
				failure |= (global::Vis.Rule.Failure.Conditional | global::Vis.Rule.Failure.Status);
			}
			return failure;
		}

		// Token: 0x060025D3 RID: 9683 RVA: 0x00089CAC File Offset: 0x00087EAC
		private global::Vis.Rule.Failure Conditional(global::Vis.Mask mask)
		{
			if (!this.setup.checkConditional)
			{
				return global::Vis.Rule.Failure.None;
			}
			global::Vis.Rule.Failure failure = global::Vis.Rule.Failure.None;
			if (!mask.Eval(this.conditionalLife))
			{
				failure |= (global::Vis.Rule.Failure.Conditional | global::Vis.Rule.Failure.Life);
			}
			if (!mask.Eval(this.conditionalRole))
			{
				failure |= (global::Vis.Rule.Failure.Conditional | global::Vis.Rule.Failure.Role);
			}
			if (!mask.Eval(this.conditionalStatus))
			{
				failure |= (global::Vis.Rule.Failure.Conditional | global::Vis.Rule.Failure.Status);
			}
			return failure;
		}

		// Token: 0x060025D4 RID: 9684 RVA: 0x00089D14 File Offset: 0x00087F14
		private global::Vis.Rule.Failure Reject(global::Vis.Mask mask)
		{
			if (!this.setup.checkReject)
			{
				return global::Vis.Rule.Failure.None;
			}
			global::Vis.Rule.Failure failure = global::Vis.Rule.Failure.None;
			if (mask.Eval(this.rejectLife))
			{
				failure |= (global::Vis.Rule.Failure.Reject | global::Vis.Rule.Failure.Life);
			}
			if (mask.Eval(this.rejectRole))
			{
				failure |= (global::Vis.Rule.Failure.Reject | global::Vis.Rule.Failure.Role);
			}
			if (mask.Eval(this.rejectStatus))
			{
				failure |= (global::Vis.Rule.Failure.Reject | global::Vis.Rule.Failure.Status);
			}
			return failure;
		}

		// Token: 0x060025D5 RID: 9685 RVA: 0x00089D7C File Offset: 0x00087F7C
		private global::Vis.Rule.Failure Check(global::Vis.Mask a, global::Vis.Mask c, global::Vis.Mask r)
		{
			global::Vis.Rule.Failure failure = this.Accept(a);
			if (failure != global::Vis.Rule.Failure.None)
			{
				return failure;
			}
			failure = this.Conditional(c);
			if (failure != global::Vis.Rule.Failure.None)
			{
				return failure;
			}
			return this.Reject(r);
		}

		// Token: 0x060025D6 RID: 9686 RVA: 0x00089DB4 File Offset: 0x00087FB4
		public global::Vis.Rule.Failure Pass(global::Vis.Mask self, global::Vis.Mask other)
		{
			switch (this.setup.option)
			{
			default:
				return this.Check(other, self, other);
			case global::Vis.Rule.Setup.Option.Inverse:
				return this.Check(self, other, self);
			case global::Vis.Rule.Setup.Option.NoConditional:
				return this.Check(other, other, other);
			case global::Vis.Rule.Setup.Option.AllConditional:
				return this.Check(self, self, self);
			}
		}

		// Token: 0x060025D7 RID: 9687 RVA: 0x00089E10 File Offset: 0x00088010
		public static global::Vis.Rule Decode(int[] data, int index)
		{
			global::Vis.Rule result = default(global::Vis.Rule);
			result.setup.data = data[index++];
			result.accept.data = data[index++];
			result.conditional.data = data[index++];
			result.reject.data = data[index];
			return result;
		}

		// Token: 0x060025D8 RID: 9688 RVA: 0x00089E74 File Offset: 0x00088074
		public static void Encode(ref global::Vis.Rule rule, int[] data, int index)
		{
			data[index++] = rule.setup.data;
			data[index++] = rule.accept.data;
			data[index++] = rule.conditional.data;
			data[index++] = rule.reject.data;
		}

		// Token: 0x040011A5 RID: 4517
		public global::Vis.Rule.Setup setup;

		// Token: 0x040011A6 RID: 4518
		public global::Vis.Mask reject;

		// Token: 0x040011A7 RID: 4519
		public global::Vis.Mask accept;

		// Token: 0x040011A8 RID: 4520
		public global::Vis.Mask conditional;

		// Token: 0x02000436 RID: 1078
		public enum Clearance
		{
			// Token: 0x040011AA RID: 4522
			Outside,
			// Token: 0x040011AB RID: 4523
			Enter,
			// Token: 0x040011AC RID: 4524
			Stay,
			// Token: 0x040011AD RID: 4525
			Exit
		}

		// Token: 0x02000437 RID: 1079
		public enum Step
		{
			// Token: 0x040011AF RID: 4527
			Accept,
			// Token: 0x040011B0 RID: 4528
			Conditional,
			// Token: 0x040011B1 RID: 4529
			Reject
		}

		// Token: 0x02000438 RID: 1080
		public struct RegionSetup
		{
			// Token: 0x060025D9 RID: 9689 RVA: 0x00089ED0 File Offset: 0x000880D0
			internal RegionSetup(int value, global::Vis.Region reg)
			{
				this._ = new BitVector32(value | (int)((int)reg << (int)(global::Vis.Rule.RegionSetup.s_region.Offset & 31)));
			}

			// Token: 0x060025DA RID: 9690 RVA: 0x00089EFC File Offset: 0x000880FC
			static RegionSetup()
			{
				BitVector32.Section previous = global::Vis.Rule.RegionSetup.s_apt = BitVector32.CreateSection(7);
				previous = (global::Vis.Rule.RegionSetup.s_cnd = BitVector32.CreateSection(7, previous));
				previous = (global::Vis.Rule.RegionSetup.s_rej = BitVector32.CreateSection(7, previous));
				global::Vis.Rule.RegionSetup.s_whole = BitVector32.CreateSection(511);
				previous = BitVector32.CreateSection(7, previous);
				global::Vis.Rule.RegionSetup.s_region = BitVector32.CreateSection(3, previous);
			}

			// Token: 0x170008BD RID: 2237
			// (get) Token: 0x060025DB RID: 9691 RVA: 0x00089F58 File Offset: 0x00088158
			// (set) Token: 0x060025DC RID: 9692 RVA: 0x00089F6C File Offset: 0x0008816C
			public global::Vis.Op accept
			{
				get
				{
					return (global::Vis.Op)this._[global::Vis.Rule.RegionSetup.s_apt];
				}
				set
				{
					this._[global::Vis.Rule.RegionSetup.s_apt] = (int)value;
				}
			}

			// Token: 0x170008BE RID: 2238
			// (get) Token: 0x060025DD RID: 9693 RVA: 0x00089F80 File Offset: 0x00088180
			// (set) Token: 0x060025DE RID: 9694 RVA: 0x00089F94 File Offset: 0x00088194
			public global::Vis.Op conditional
			{
				get
				{
					return (global::Vis.Op)this._[global::Vis.Rule.RegionSetup.s_cnd];
				}
				set
				{
					this._[global::Vis.Rule.RegionSetup.s_cnd] = (int)value;
				}
			}

			// Token: 0x170008BF RID: 2239
			// (get) Token: 0x060025DF RID: 9695 RVA: 0x00089FA8 File Offset: 0x000881A8
			// (set) Token: 0x060025E0 RID: 9696 RVA: 0x00089FBC File Offset: 0x000881BC
			public global::Vis.Op reject
			{
				get
				{
					return (global::Vis.Op)this._[global::Vis.Rule.RegionSetup.s_rej];
				}
				set
				{
					this._[global::Vis.Rule.RegionSetup.s_rej] = (int)value;
				}
			}

			// Token: 0x170008C0 RID: 2240
			// (get) Token: 0x060025E1 RID: 9697 RVA: 0x00089FD0 File Offset: 0x000881D0
			// (set) Token: 0x060025E2 RID: 9698 RVA: 0x00089FE4 File Offset: 0x000881E4
			public global::Vis.Region region
			{
				get
				{
					return (global::Vis.Region)this._[global::Vis.Rule.RegionSetup.s_region];
				}
				set
				{
					this._[global::Vis.Rule.RegionSetup.s_region] = (int)value;
				}
			}

			// Token: 0x170008C1 RID: 2241
			public global::Vis.Op this[global::Vis.Rule.Step step]
			{
				get
				{
					switch (step)
					{
					case global::Vis.Rule.Step.Accept:
						return this.accept;
					case global::Vis.Rule.Step.Conditional:
						return this.conditional;
					case global::Vis.Rule.Step.Reject:
						return this.reject;
					default:
						throw new ArgumentOutOfRangeException("step");
					}
				}
				set
				{
					switch (step)
					{
					case global::Vis.Rule.Step.Accept:
						this.accept = value;
						break;
					case global::Vis.Rule.Step.Conditional:
						this.conditional = value;
						break;
					case global::Vis.Rule.Step.Reject:
						this.reject = value;
						break;
					default:
						throw new ArgumentOutOfRangeException("step");
					}
				}
			}

			// Token: 0x170008C2 RID: 2242
			// (get) Token: 0x060025E5 RID: 9701 RVA: 0x0008A098 File Offset: 0x00088298
			internal int dat
			{
				get
				{
					return this._[global::Vis.Rule.RegionSetup.s_whole];
				}
			}

			// Token: 0x040011B2 RID: 4530
			private static readonly BitVector32.Section s_apt;

			// Token: 0x040011B3 RID: 4531
			private static readonly BitVector32.Section s_cnd;

			// Token: 0x040011B4 RID: 4532
			private static readonly BitVector32.Section s_rej;

			// Token: 0x040011B5 RID: 4533
			private static readonly BitVector32.Section s_whole;

			// Token: 0x040011B6 RID: 4534
			private static readonly BitVector32.Section s_region;

			// Token: 0x040011B7 RID: 4535
			private BitVector32 _;
		}

		// Token: 0x02000439 RID: 1081
		public struct StepSetup
		{
			// Token: 0x060025E6 RID: 9702 RVA: 0x0008A0AC File Offset: 0x000882AC
			internal StepSetup(int life, int stat, int role, int step, int enable)
			{
				this = default(global::Vis.Rule.StepSetup);
				this._[global::Vis.Rule.StepSetup.s_life] = life;
				this._[global::Vis.Rule.StepSetup.s_stat] = stat;
				this._[global::Vis.Rule.StepSetup.s_role] = role;
				this._[global::Vis.Rule.StepSetup.s_step] = step;
				this._[global::Vis.Rule.StepSetup.s_enable] = enable;
			}

			// Token: 0x060025E7 RID: 9703 RVA: 0x0008A120 File Offset: 0x00088320
			static StepSetup()
			{
				BitVector32.Section previous = global::Vis.Rule.StepSetup.s_life = BitVector32.CreateSection(7);
				previous = (global::Vis.Rule.StepSetup.s_step = BitVector32.CreateSection(255, previous));
				previous = (global::Vis.Rule.StepSetup.s_enable = BitVector32.CreateSection(1, previous));
				previous = (global::Vis.Rule.StepSetup.s_stat = BitVector32.CreateSection(7, previous));
				previous = BitVector32.CreateSection(511, previous);
				global::Vis.Rule.StepSetup.s_role = BitVector32.CreateSection(7, previous);
			}

			// Token: 0x170008C3 RID: 2243
			// (get) Token: 0x060025E8 RID: 9704 RVA: 0x0008A184 File Offset: 0x00088384
			// (set) Token: 0x060025E9 RID: 9705 RVA: 0x0008A198 File Offset: 0x00088398
			public global::Vis.Op life
			{
				get
				{
					return (global::Vis.Op)this._[global::Vis.Rule.StepSetup.s_life];
				}
				set
				{
					this._[global::Vis.Rule.StepSetup.s_life] = (int)value;
				}
			}

			// Token: 0x170008C4 RID: 2244
			// (get) Token: 0x060025EA RID: 9706 RVA: 0x0008A1AC File Offset: 0x000883AC
			// (set) Token: 0x060025EB RID: 9707 RVA: 0x0008A1C0 File Offset: 0x000883C0
			public global::Vis.Op stat
			{
				get
				{
					return (global::Vis.Op)this._[global::Vis.Rule.StepSetup.s_stat];
				}
				set
				{
					this._[global::Vis.Rule.StepSetup.s_stat] = (int)value;
				}
			}

			// Token: 0x170008C5 RID: 2245
			// (get) Token: 0x060025EC RID: 9708 RVA: 0x0008A1D4 File Offset: 0x000883D4
			// (set) Token: 0x060025ED RID: 9709 RVA: 0x0008A1E8 File Offset: 0x000883E8
			public global::Vis.Op role
			{
				get
				{
					return (global::Vis.Op)this._[global::Vis.Rule.StepSetup.s_role];
				}
				set
				{
					this._[global::Vis.Rule.StepSetup.s_role] = (int)value;
				}
			}

			// Token: 0x170008C6 RID: 2246
			// (get) Token: 0x060025EE RID: 9710 RVA: 0x0008A1FC File Offset: 0x000883FC
			// (set) Token: 0x060025EF RID: 9711 RVA: 0x0008A210 File Offset: 0x00088410
			public global::Vis.Rule.Step step
			{
				get
				{
					return (global::Vis.Rule.Step)this._[global::Vis.Rule.StepSetup.s_step];
				}
				set
				{
					this._[global::Vis.Rule.StepSetup.s_step] = (int)value;
				}
			}

			// Token: 0x170008C7 RID: 2247
			// (get) Token: 0x060025F0 RID: 9712 RVA: 0x0008A224 File Offset: 0x00088424
			// (set) Token: 0x060025F1 RID: 9713 RVA: 0x0008A23C File Offset: 0x0008843C
			public bool enabled
			{
				get
				{
					return this._[global::Vis.Rule.StepSetup.s_enable] != 0;
				}
				set
				{
					this._[global::Vis.Rule.StepSetup.s_enable] = ((!value) ? 0 : 1);
				}
			}

			// Token: 0x170008C8 RID: 2248
			public global::Vis.Op this[global::Vis.Region region]
			{
				get
				{
					switch (region)
					{
					case global::Vis.Region.Life:
						return this.life;
					case global::Vis.Region.Status:
						return this.stat;
					case global::Vis.Region.Role:
						return this.role;
					default:
						throw new ArgumentOutOfRangeException("region");
					}
				}
				set
				{
					switch (region)
					{
					case global::Vis.Region.Life:
						this.life = value;
						break;
					case global::Vis.Region.Status:
						this.stat = value;
						break;
					case global::Vis.Region.Role:
						this.role = value;
						break;
					default:
						throw new ArgumentOutOfRangeException("region");
					}
				}
			}

			// Token: 0x040011B8 RID: 4536
			private static readonly BitVector32.Section s_life;

			// Token: 0x040011B9 RID: 4537
			private static readonly BitVector32.Section s_stat;

			// Token: 0x040011BA RID: 4538
			private static readonly BitVector32.Section s_role;

			// Token: 0x040011BB RID: 4539
			private static readonly BitVector32.Section s_step;

			// Token: 0x040011BC RID: 4540
			private static readonly BitVector32.Section s_enable;

			// Token: 0x040011BD RID: 4541
			private BitVector32 _;
		}

		// Token: 0x0200043A RID: 1082
		public struct StepCheck
		{
			// Token: 0x060025F4 RID: 9716 RVA: 0x0008A2FC File Offset: 0x000884FC
			internal StepCheck(int i)
			{
				this.bits = (byte)i;
			}

			// Token: 0x170008C9 RID: 2249
			// (get) Token: 0x060025F5 RID: 9717 RVA: 0x0008A308 File Offset: 0x00088508
			// (set) Token: 0x060025F6 RID: 9718 RVA: 0x0008A318 File Offset: 0x00088518
			public bool accept
			{
				get
				{
					return (this.bits & 1) == 1;
				}
				set
				{
					this.bits = ((!value) ? (this.bits & 6) : (this.bits | 1));
				}
			}

			// Token: 0x170008CA RID: 2250
			// (get) Token: 0x060025F7 RID: 9719 RVA: 0x0008A348 File Offset: 0x00088548
			// (set) Token: 0x060025F8 RID: 9720 RVA: 0x0008A358 File Offset: 0x00088558
			public bool conditional
			{
				get
				{
					return (this.bits & 2) == 2;
				}
				set
				{
					this.bits = ((!value) ? (this.bits & 5) : (this.bits | 2));
				}
			}

			// Token: 0x170008CB RID: 2251
			// (get) Token: 0x060025F9 RID: 9721 RVA: 0x0008A388 File Offset: 0x00088588
			// (set) Token: 0x060025FA RID: 9722 RVA: 0x0008A398 File Offset: 0x00088598
			public bool reject
			{
				get
				{
					return (this.bits & 4) == 4;
				}
				set
				{
					this.bits = ((!value) ? (this.bits & 3) : (this.bits | 4));
				}
			}

			// Token: 0x170008CC RID: 2252
			// (get) Token: 0x060025FB RID: 9723 RVA: 0x0008A3C8 File Offset: 0x000885C8
			internal int value
			{
				get
				{
					return (int)this.bits;
				}
			}

			// Token: 0x170008CD RID: 2253
			public bool this[global::Vis.Rule.Step step]
			{
				get
				{
					switch (step)
					{
					case global::Vis.Rule.Step.Accept:
						return this.accept;
					case global::Vis.Rule.Step.Conditional:
						return this.conditional;
					case global::Vis.Rule.Step.Reject:
						return this.reject;
					default:
						throw new ArgumentOutOfRangeException("step");
					}
				}
				set
				{
					switch (step)
					{
					case global::Vis.Rule.Step.Accept:
						this.accept = value;
						break;
					case global::Vis.Rule.Step.Conditional:
						this.conditional = value;
						break;
					case global::Vis.Rule.Step.Reject:
						this.reject = value;
						break;
					default:
						throw new ArgumentOutOfRangeException("step");
					}
				}
			}

			// Token: 0x040011BE RID: 4542
			private byte bits;
		}

		// Token: 0x0200043B RID: 1083
		public struct Setup
		{
			// Token: 0x060025FE RID: 9726 RVA: 0x0008A470 File Offset: 0x00088670
			static Setup()
			{
				BitVector32.Section previous = global::Vis.Rule.Setup.s_life = BitVector32.CreateSection(511);
				previous = (global::Vis.Rule.Setup.s_stat = BitVector32.CreateSection(511, previous));
				previous = (global::Vis.Rule.Setup.s_role = BitVector32.CreateSection(511, previous));
				previous = (global::Vis.Rule.Setup.s_options = BitVector32.CreateSection(3, previous));
				global::Vis.Rule.Setup.s_check = BitVector32.CreateSection(7, previous);
				global::Vis.Rule.Setup.s_life_ = new BitVector32.Section[3];
				global::Vis.Rule.Setup.s_stat_ = new BitVector32.Section[3];
				global::Vis.Rule.Setup.s_role_ = new BitVector32.Section[3];
				global::Vis.Rule.Setup.s_check_ = new BitVector32.Section[3];
				int i = 0;
				global::Vis.Rule.Setup.s_life_[i] = BitVector32.CreateSection(7);
				global::Vis.Rule.Setup.s_stat_[i] = BitVector32.CreateSection(7, global::Vis.Rule.Setup.s_life);
				global::Vis.Rule.Setup.s_role_[i] = BitVector32.CreateSection(7, global::Vis.Rule.Setup.s_stat);
				global::Vis.Rule.Setup.s_check_[i] = BitVector32.CreateSection(1, global::Vis.Rule.Setup.s_options);
				for (i++; i < 3; i++)
				{
					global::Vis.Rule.Setup.s_life_[i] = BitVector32.CreateSection(7, global::Vis.Rule.Setup.s_life_[i - 1]);
					global::Vis.Rule.Setup.s_stat_[i] = BitVector32.CreateSection(7, global::Vis.Rule.Setup.s_stat_[i - 1]);
					global::Vis.Rule.Setup.s_role_[i] = BitVector32.CreateSection(7, global::Vis.Rule.Setup.s_role_[i - 1]);
					global::Vis.Rule.Setup.s_check_[i] = BitVector32.CreateSection(1, global::Vis.Rule.Setup.s_check_[i - 1]);
				}
			}

			// Token: 0x170008CE RID: 2254
			// (get) Token: 0x060025FF RID: 9727 RVA: 0x0008A618 File Offset: 0x00088818
			// (set) Token: 0x06002600 RID: 9728 RVA: 0x0008A630 File Offset: 0x00088830
			public global::Vis.Rule.RegionSetup life
			{
				get
				{
					return new global::Vis.Rule.RegionSetup(this._[global::Vis.Rule.Setup.s_life], global::Vis.Region.Life);
				}
				set
				{
					this._[global::Vis.Rule.Setup.s_life] = value.dat;
				}
			}

			// Token: 0x170008CF RID: 2255
			// (get) Token: 0x06002601 RID: 9729 RVA: 0x0008A64C File Offset: 0x0008884C
			// (set) Token: 0x06002602 RID: 9730 RVA: 0x0008A664 File Offset: 0x00088864
			public global::Vis.Rule.RegionSetup stat
			{
				get
				{
					return new global::Vis.Rule.RegionSetup(this._[global::Vis.Rule.Setup.s_stat], global::Vis.Region.Status);
				}
				set
				{
					this._[global::Vis.Rule.Setup.s_stat] = value.dat;
				}
			}

			// Token: 0x170008D0 RID: 2256
			// (get) Token: 0x06002603 RID: 9731 RVA: 0x0008A680 File Offset: 0x00088880
			// (set) Token: 0x06002604 RID: 9732 RVA: 0x0008A698 File Offset: 0x00088898
			public global::Vis.Rule.RegionSetup role
			{
				get
				{
					return new global::Vis.Rule.RegionSetup(this._[global::Vis.Rule.Setup.s_role], global::Vis.Region.Role);
				}
				set
				{
					this._[global::Vis.Rule.Setup.s_role] = value.dat;
				}
			}

			// Token: 0x06002605 RID: 9733 RVA: 0x0008A6B4 File Offset: 0x000888B4
			private global::Vis.Rule.StepSetup Get(int i)
			{
				return new global::Vis.Rule.StepSetup(this._[global::Vis.Rule.Setup.s_life_[i]], this._[global::Vis.Rule.Setup.s_stat_[i]], this._[global::Vis.Rule.Setup.s_role_[i]], i, this._[global::Vis.Rule.Setup.s_check_[i]]);
			}

			// Token: 0x06002606 RID: 9734 RVA: 0x0008A734 File Offset: 0x00088934
			private void Set(int i, global::Vis.Rule.StepSetup step)
			{
				this._[global::Vis.Rule.Setup.s_life_[i]] = (int)(step.life & (global::Vis.Op)global::Vis.Rule.Setup.s_life_[i].Mask);
				this._[global::Vis.Rule.Setup.s_stat_[i]] = (int)(step.stat & (global::Vis.Op)global::Vis.Rule.Setup.s_stat_[i].Mask);
				this._[global::Vis.Rule.Setup.s_role_[i]] = (int)(step.role & (global::Vis.Op)global::Vis.Rule.Setup.s_role_[i].Mask);
				this._[global::Vis.Rule.Setup.s_check_[i]] = ((!step.enabled) ? 0 : 1);
			}

			// Token: 0x170008D1 RID: 2257
			// (get) Token: 0x06002607 RID: 9735 RVA: 0x0008A808 File Offset: 0x00088A08
			// (set) Token: 0x06002608 RID: 9736 RVA: 0x0008A814 File Offset: 0x00088A14
			public global::Vis.Rule.StepSetup accept
			{
				get
				{
					return this.Get(0);
				}
				set
				{
					this.Set(0, value);
				}
			}

			// Token: 0x170008D2 RID: 2258
			// (get) Token: 0x06002609 RID: 9737 RVA: 0x0008A820 File Offset: 0x00088A20
			// (set) Token: 0x0600260A RID: 9738 RVA: 0x0008A82C File Offset: 0x00088A2C
			public global::Vis.Rule.StepSetup conditional
			{
				get
				{
					return this.Get(1);
				}
				set
				{
					this.Set(1, value);
				}
			}

			// Token: 0x170008D3 RID: 2259
			// (get) Token: 0x0600260B RID: 9739 RVA: 0x0008A838 File Offset: 0x00088A38
			// (set) Token: 0x0600260C RID: 9740 RVA: 0x0008A844 File Offset: 0x00088A44
			public global::Vis.Rule.StepSetup reject
			{
				get
				{
					return this.Get(2);
				}
				set
				{
					this.Set(2, value);
				}
			}

			// Token: 0x170008D4 RID: 2260
			public global::Vis.Rule.RegionSetup this[global::Vis.Region region]
			{
				get
				{
					switch (region)
					{
					case global::Vis.Region.Life:
						return this.life;
					case global::Vis.Region.Status:
						return this.stat;
					case global::Vis.Region.Role:
						return this.role;
					default:
						throw new ArgumentOutOfRangeException("region");
					}
				}
				set
				{
					switch (region)
					{
					case global::Vis.Region.Life:
						this.life = value;
						break;
					case global::Vis.Region.Status:
						this.stat = value;
						break;
					case global::Vis.Region.Role:
						this.role = value;
						break;
					default:
						throw new ArgumentOutOfRangeException("region");
					}
				}
			}

			// Token: 0x170008D5 RID: 2261
			public global::Vis.Rule.StepSetup this[global::Vis.Rule.Step step]
			{
				get
				{
					switch (step)
					{
					case global::Vis.Rule.Step.Accept:
						return this.accept;
					case global::Vis.Rule.Step.Conditional:
						return this.conditional;
					case global::Vis.Rule.Step.Reject:
						return this.reject;
					default:
						throw new ArgumentOutOfRangeException("step");
					}
				}
				set
				{
					switch (step)
					{
					case global::Vis.Rule.Step.Accept:
						this.accept = value;
						break;
					case global::Vis.Rule.Step.Conditional:
						this.conditional = value;
						break;
					case global::Vis.Rule.Step.Reject:
						this.reject = value;
						break;
					default:
						throw new ArgumentOutOfRangeException("step");
					}
				}
			}

			// Token: 0x170008D6 RID: 2262
			// (get) Token: 0x06002611 RID: 9745 RVA: 0x0008A990 File Offset: 0x00088B90
			// (set) Token: 0x06002612 RID: 9746 RVA: 0x0008A9A4 File Offset: 0x00088BA4
			public global::Vis.Rule.Setup.Option option
			{
				get
				{
					return (global::Vis.Rule.Setup.Option)this._[global::Vis.Rule.Setup.s_options];
				}
				set
				{
					this._[global::Vis.Rule.Setup.s_options] = (int)value;
				}
			}

			// Token: 0x170008D7 RID: 2263
			// (get) Token: 0x06002613 RID: 9747 RVA: 0x0008A9B8 File Offset: 0x00088BB8
			// (set) Token: 0x06002614 RID: 9748 RVA: 0x0008A9D0 File Offset: 0x00088BD0
			public global::Vis.Rule.StepCheck check
			{
				get
				{
					return new global::Vis.Rule.StepCheck(this._[global::Vis.Rule.Setup.s_check]);
				}
				set
				{
					this._[global::Vis.Rule.Setup.s_check] = value.value;
				}
			}

			// Token: 0x170008D8 RID: 2264
			// (get) Token: 0x06002615 RID: 9749 RVA: 0x0008A9EC File Offset: 0x00088BEC
			// (set) Token: 0x06002616 RID: 9750 RVA: 0x0008AA10 File Offset: 0x00088C10
			public bool checkAccept
			{
				get
				{
					return this._[global::Vis.Rule.Setup.s_check_[0]] != 0;
				}
				set
				{
					this._[global::Vis.Rule.Setup.s_check_[0]] = ((!value) ? 0 : 1);
				}
			}

			// Token: 0x170008D9 RID: 2265
			// (get) Token: 0x06002617 RID: 9751 RVA: 0x0008AA48 File Offset: 0x00088C48
			// (set) Token: 0x06002618 RID: 9752 RVA: 0x0008AA6C File Offset: 0x00088C6C
			public bool checkConditional
			{
				get
				{
					return this._[global::Vis.Rule.Setup.s_check_[1]] != 0;
				}
				set
				{
					this._[global::Vis.Rule.Setup.s_check_[1]] = ((!value) ? 0 : 1);
				}
			}

			// Token: 0x170008DA RID: 2266
			// (get) Token: 0x06002619 RID: 9753 RVA: 0x0008AAA4 File Offset: 0x00088CA4
			// (set) Token: 0x0600261A RID: 9754 RVA: 0x0008AAC8 File Offset: 0x00088CC8
			public bool checkReject
			{
				get
				{
					return this._[global::Vis.Rule.Setup.s_check_[2]] != 0;
				}
				set
				{
					this._[global::Vis.Rule.Setup.s_check_[2]] = ((!value) ? 0 : 1);
				}
			}

			// Token: 0x0600261B RID: 9755 RVA: 0x0008AB00 File Offset: 0x00088D00
			public void SetSetup(global::Vis.Rule.RegionSetup operations)
			{
				this[operations.region] = operations;
			}

			// Token: 0x0600261C RID: 9756 RVA: 0x0008AB10 File Offset: 0x00088D10
			public void SetSetup(global::Vis.Rule.StepSetup operations)
			{
				this[operations.step] = operations;
			}

			// Token: 0x170008DB RID: 2267
			// (get) Token: 0x0600261D RID: 9757 RVA: 0x0008AB20 File Offset: 0x00088D20
			// (set) Token: 0x0600261E RID: 9758 RVA: 0x0008AB30 File Offset: 0x00088D30
			internal int data
			{
				get
				{
					return this._.Data;
				}
				set
				{
					this._ = new BitVector32(value);
				}
			}

			// Token: 0x040011BF RID: 4543
			private static readonly BitVector32.Section s_life;

			// Token: 0x040011C0 RID: 4544
			private static readonly BitVector32.Section[] s_life_;

			// Token: 0x040011C1 RID: 4545
			private static readonly BitVector32.Section s_stat;

			// Token: 0x040011C2 RID: 4546
			private static readonly BitVector32.Section[] s_stat_;

			// Token: 0x040011C3 RID: 4547
			private static readonly BitVector32.Section s_role;

			// Token: 0x040011C4 RID: 4548
			private static readonly BitVector32.Section[] s_role_;

			// Token: 0x040011C5 RID: 4549
			private static readonly BitVector32.Section s_options;

			// Token: 0x040011C6 RID: 4550
			private static readonly BitVector32.Section s_check;

			// Token: 0x040011C7 RID: 4551
			private static readonly BitVector32.Section[] s_check_;

			// Token: 0x040011C8 RID: 4552
			private BitVector32 _;

			// Token: 0x0200043C RID: 1084
			public enum Option
			{
				// Token: 0x040011CA RID: 4554
				Default,
				// Token: 0x040011CB RID: 4555
				Inverse,
				// Token: 0x040011CC RID: 4556
				NoConditional,
				// Token: 0x040011CD RID: 4557
				AllConditional
			}
		}

		// Token: 0x0200043D RID: 1085
		[Flags]
		public enum Failure
		{
			// Token: 0x040011CF RID: 4559
			None = 0,
			// Token: 0x040011D0 RID: 4560
			Accept = 1,
			// Token: 0x040011D1 RID: 4561
			Conditional = 2,
			// Token: 0x040011D2 RID: 4562
			Reject = 4,
			// Token: 0x040011D3 RID: 4563
			Life = 8,
			// Token: 0x040011D4 RID: 4564
			Role = 16,
			// Token: 0x040011D5 RID: 4565
			Status = 32
		}
	}

	// Token: 0x0200043E RID: 1086
	public struct Stamp
	{
		// Token: 0x0600261F RID: 9759 RVA: 0x0008AB40 File Offset: 0x00088D40
		public Stamp(Transform transform)
		{
			this.position = transform.position;
			this.rotation = transform.rotation;
			Vector3 forward = transform.forward;
			this.plane.x = forward.x;
			this.plane.y = forward.y;
			this.plane.z = forward.z;
			this.plane.w = this.position.x * this.plane.x + this.position.y * this.plane.y + this.position.z * this.plane.z;
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06002620 RID: 9760 RVA: 0x0008ABF4 File Offset: 0x00088DF4
		public Vector3 forward
		{
			get
			{
				return new Vector3(this.plane.x, this.plane.y, this.plane.z);
			}
		}

		// Token: 0x06002621 RID: 9761 RVA: 0x0008AC28 File Offset: 0x00088E28
		public void Collect(Transform transform)
		{
			this.position = transform.position;
			this.rotation = transform.rotation;
			Vector3 forward = transform.forward;
			this.plane.x = forward.x;
			this.plane.y = forward.y;
			this.plane.z = forward.z;
			this.plane.w = this.position.x * this.forward.x + this.position.y * this.forward.y + this.position.z * this.forward.z;
		}

		// Token: 0x040011D6 RID: 4566
		public Vector3 position;

		// Token: 0x040011D7 RID: 4567
		public Vector4 plane;

		// Token: 0x040011D8 RID: 4568
		public Quaternion rotation;
	}
}
