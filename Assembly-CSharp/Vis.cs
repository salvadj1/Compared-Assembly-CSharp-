using System;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using UnityEngine;

// Token: 0x0200037A RID: 890
public static class Vis
{
	// Token: 0x060021CF RID: 8655 RVA: 0x00083410 File Offset: 0x00081610
	public static Vis.Op Negate(Vis.Op op)
	{
		return Vis.Op.Any - (op - Vis.Op.Any);
	}

	// Token: 0x060021D0 RID: 8656 RVA: 0x00083418 File Offset: 0x00081618
	public static Vis.Op<TFlags> Negate<TFlags>(Vis.Op<TFlags> op) where TFlags : struct, IConvertible, IFormattable, IComparable
	{
		op.op = Vis.Negate(op.op);
		return op;
	}

	// Token: 0x060021D1 RID: 8657 RVA: 0x00083430 File Offset: 0x00081630
	internal static bool Evaluate(Vis.Op op, int f, int m)
	{
		switch (op)
		{
		case Vis.Op.Always:
			return true;
		case Vis.Op.Equals:
			return m == f;
		case Vis.Op.All:
			return (m & f) == f;
		case Vis.Op.Any:
			return (m & f) != 0;
		case Vis.Op.None:
			return (m & f) == 0;
		case Vis.Op.NotEquals:
			return m != f;
		default:
			return false;
		}
	}

	// Token: 0x060021D2 RID: 8658 RVA: 0x00083490 File Offset: 0x00081690
	private static int SetTrue(Vis.Op op, int f, ref BitVector32 bits, BitVector32.Section sect)
	{
		int num = bits[sect];
		int result;
		if (num != (result = Vis.SetTrue(op, f, num)))
		{
			bits[sect] = num;
		}
		return result;
	}

	// Token: 0x060021D3 RID: 8659 RVA: 0x000834C0 File Offset: 0x000816C0
	private static int SetTrue(Vis.Op op, int f, int m)
	{
		switch (op)
		{
		default:
			return m;
		case Vis.Op.Equals:
			return f;
		case Vis.Op.All:
			return m | f;
		case Vis.Op.Any:
			if ((m & f) == 0)
			{
				return m | f;
			}
			return m;
		case Vis.Op.None:
			return m & ~f;
		case Vis.Op.NotEquals:
			return ~f;
		}
	}

	// Token: 0x060021D4 RID: 8660 RVA: 0x00083514 File Offset: 0x00081714
	public static bool IsZeroWay(this Vis.Comparison comparison)
	{
		return (comparison & Vis.Comparison.Contact) == Vis.Comparison.Oblivious;
	}

	// Token: 0x060021D5 RID: 8661 RVA: 0x00083520 File Offset: 0x00081720
	public static bool IsOneWay(this Vis.Comparison comparison)
	{
		return (comparison & Vis.Comparison.IsSelf) != Vis.Comparison.IsSelf;
	}

	// Token: 0x060021D6 RID: 8662 RVA: 0x00083530 File Offset: 0x00081730
	public static bool DoesSeeTarget(this Vis.Comparison comparison)
	{
		return (comparison & (Vis.Comparison)4) == (Vis.Comparison)4;
	}

	// Token: 0x060021D7 RID: 8663 RVA: 0x00083538 File Offset: 0x00081738
	public static bool IsSeenByTarget(this Vis.Comparison comparison)
	{
		return (comparison & (Vis.Comparison)8) == (Vis.Comparison)8;
	}

	// Token: 0x060021D8 RID: 8664 RVA: 0x00083540 File Offset: 0x00081740
	public static bool IsTwoWay(this Vis.Comparison comparison)
	{
		return (comparison & Vis.Comparison.Contact) != Vis.Comparison.IsSelf;
	}

	// Token: 0x060021D9 RID: 8665 RVA: 0x00083550 File Offset: 0x00081750
	public static int CountSeen(this Vis.Comparison comparison)
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

	// Token: 0x060021DA RID: 8666 RVA: 0x00083588 File Offset: 0x00081788
	public static int GetStealth(this Vis.Comparison comparison)
	{
		int num = (int)(comparison & Vis.Comparison.IsSelf);
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

	// Token: 0x060021DB RID: 8667 RVA: 0x000835B4 File Offset: 0x000817B4
	public static VisNode.Search.Radial.Enumerator GetNodesInRadius(Vector3 point, float radius)
	{
		return new VisNode.Search.Radial.Enumerator(new VisNode.Search.PointRadiusData(point, radius));
	}

	// Token: 0x060021DC RID: 8668 RVA: 0x000835C4 File Offset: 0x000817C4
	public static void RadialMessage(Vector3 point, float radius, string message, object arg)
	{
		VisNode.Search.Radial.Enumerator nodesInRadius = Vis.GetNodesInRadius(point, radius);
		while (nodesInRadius.MoveNext())
		{
			nodesInRadius.Current.SendMessage(message, arg, 1);
		}
	}

	// Token: 0x060021DD RID: 8669 RVA: 0x000835FC File Offset: 0x000817FC
	public static void RadialMessage(Vector3 point, float radius, string message)
	{
		VisNode.Search.Radial.Enumerator nodesInRadius = Vis.GetNodesInRadius(point, radius);
		while (nodesInRadius.MoveNext())
		{
			nodesInRadius.Current.SendMessage(message, 1);
		}
	}

	// Token: 0x060021DE RID: 8670 RVA: 0x00083630 File Offset: 0x00081830
	public static VisNode.Search.Point.Visual.Enumerator GetNodesWhoCanSee(Vector3 point)
	{
		return new VisNode.Search.Point.Visual.Enumerator(new VisNode.Search.PointVisibilityData(point));
	}

	// Token: 0x060021DF RID: 8671 RVA: 0x00083640 File Offset: 0x00081840
	public static void VisibleMessage(Vector3 point, string message, object arg)
	{
		VisNode.Search.Point.Visual.Enumerator nodesWhoCanSee = Vis.GetNodesWhoCanSee(point);
		while (nodesWhoCanSee.MoveNext())
		{
			nodesWhoCanSee.Current.SendMessage(message, arg, 1);
		}
	}

	// Token: 0x060021E0 RID: 8672 RVA: 0x00083674 File Offset: 0x00081874
	public static void VisibleMessage(Vector3 point, string message)
	{
		VisNode.Search.Point.Visual.Enumerator nodesWhoCanSee = Vis.GetNodesWhoCanSee(point);
		while (nodesWhoCanSee.MoveNext())
		{
			nodesWhoCanSee.Current.SendMessage(message, 1);
		}
	}

	// Token: 0x04000FBE RID: 4030
	public const Vis.Trait kTraitBegin = Vis.Trait.Alive;

	// Token: 0x04000FBF RID: 4031
	public const Vis.Trait kTraitEnd = (Vis.Trait)32;

	// Token: 0x04000FC0 RID: 4032
	public const Vis.Trait kLifeFirst = Vis.Trait.Alive;

	// Token: 0x04000FC1 RID: 4033
	public const Vis.Trait kLifeLast = Vis.Trait.Dead;

	// Token: 0x04000FC2 RID: 4034
	public const Vis.Trait kStatusFirst = Vis.Trait.Casual;

	// Token: 0x04000FC3 RID: 4035
	public const Vis.Trait kStatusLast = Vis.Trait.Attacking;

	// Token: 0x04000FC4 RID: 4036
	public const Vis.Trait kRoleFirst = Vis.Trait.Citizen;

	// Token: 0x04000FC5 RID: 4037
	public const Vis.Trait kRoleLast = Vis.Trait.Animal;

	// Token: 0x04000FC6 RID: 4038
	public const Vis.Trait kLifeBegin = Vis.Trait.Alive;

	// Token: 0x04000FC7 RID: 4039
	public const Vis.Trait kLifeEnd = (Vis.Trait)3;

	// Token: 0x04000FC8 RID: 4040
	public const Vis.Trait kStatusBegin = Vis.Trait.Casual;

	// Token: 0x04000FC9 RID: 4041
	public const Vis.Trait kStatusEnd = (Vis.Trait)15;

	// Token: 0x04000FCA RID: 4042
	public const Vis.Trait kRoleBegin = Vis.Trait.Citizen;

	// Token: 0x04000FCB RID: 4043
	public const Vis.Trait kRoleEnd = (Vis.Trait)32;

	// Token: 0x04000FCC RID: 4044
	public const int kLifeCount = 3;

	// Token: 0x04000FCD RID: 4045
	public const int kStatusCount = 7;

	// Token: 0x04000FCE RID: 4046
	public const int kRoleCount = 8;

	// Token: 0x04000FCF RID: 4047
	private const uint one = 1u;

	// Token: 0x04000FD0 RID: 4048
	public const int kLifeMask = 7;

	// Token: 0x04000FD1 RID: 4049
	public const int kStatusMask = 32512;

	// Token: 0x04000FD2 RID: 4050
	public const int kRoleMask = -16777216;

	// Token: 0x04000FD3 RID: 4051
	private const int OpZero = 3;

	// Token: 0x04000FD4 RID: 4052
	private const int mask24b = 16777215;

	// Token: 0x04000FD5 RID: 4053
	private const int mask31b = 2147483647;

	// Token: 0x04000FD6 RID: 4054
	private const int mask24o7b = 16777216;

	// Token: 0x04000FD7 RID: 4055
	private const int mask31o1b = -2147483648;

	// Token: 0x04000FD8 RID: 4056
	private const byte mask7b = 127;

	// Token: 0x04000FD9 RID: 4057
	private const byte mask7o1b = 128;

	// Token: 0x04000FDA RID: 4058
	public const Vis.Life kLifeNone = (Vis.Life)0;

	// Token: 0x04000FDB RID: 4059
	public const Vis.Life kLifeAll = Vis.Life.Alive | Vis.Life.Unconcious | Vis.Life.Dead;

	// Token: 0x04000FDC RID: 4060
	public const Vis.Status kStatusNone = (Vis.Status)0;

	// Token: 0x04000FDD RID: 4061
	public const Vis.Status kStatusAll = Vis.Status.Casual | Vis.Status.Hurt | Vis.Status.Curious | Vis.Status.Alert | Vis.Status.Search | Vis.Status.Armed | Vis.Status.Attacking;

	// Token: 0x04000FDE RID: 4062
	public const Vis.Role kRoleNone = (Vis.Role)0;

	// Token: 0x04000FDF RID: 4063
	public const Vis.Role kRoleAll = (Vis.Role)(-1);

	// Token: 0x04000FE0 RID: 4064
	public const int kFlagRelative = 1;

	// Token: 0x04000FE1 RID: 4065
	public const int kFlagTarget = 4;

	// Token: 0x04000FE2 RID: 4066
	public const int kFlagSelf = 8;

	// Token: 0x04000FE3 RID: 4067
	public const int kComparisonStealthy = 5;

	// Token: 0x04000FE4 RID: 4068
	public const int kComparisonPrey = 9;

	// Token: 0x04000FE5 RID: 4069
	public const int kComparisonIsSelf = 12;

	// Token: 0x04000FE6 RID: 4070
	public const int kComparisonOblivious = 1;

	// Token: 0x04000FE7 RID: 4071
	public const int kComparisonContact = 13;

	// Token: 0x0200037B RID: 891
	public enum Trait
	{
		// Token: 0x04000FE9 RID: 4073
		Alive,
		// Token: 0x04000FEA RID: 4074
		Unconcious,
		// Token: 0x04000FEB RID: 4075
		Dead,
		// Token: 0x04000FEC RID: 4076
		Casual = 8,
		// Token: 0x04000FED RID: 4077
		Hurt,
		// Token: 0x04000FEE RID: 4078
		Curious,
		// Token: 0x04000FEF RID: 4079
		Alert,
		// Token: 0x04000FF0 RID: 4080
		Search,
		// Token: 0x04000FF1 RID: 4081
		Armed,
		// Token: 0x04000FF2 RID: 4082
		Attacking,
		// Token: 0x04000FF3 RID: 4083
		Citizen = 24,
		// Token: 0x04000FF4 RID: 4084
		Criminal,
		// Token: 0x04000FF5 RID: 4085
		Authority,
		// Token: 0x04000FF6 RID: 4086
		Target,
		// Token: 0x04000FF7 RID: 4087
		Entourage,
		// Token: 0x04000FF8 RID: 4088
		Player,
		// Token: 0x04000FF9 RID: 4089
		Vehicle,
		// Token: 0x04000FFA RID: 4090
		Animal
	}

	// Token: 0x0200037C RID: 892
	public enum Op
	{
		// Token: 0x04000FFC RID: 4092
		Always,
		// Token: 0x04000FFD RID: 4093
		Equals,
		// Token: 0x04000FFE RID: 4094
		All,
		// Token: 0x04000FFF RID: 4095
		Any,
		// Token: 0x04001000 RID: 4096
		None,
		// Token: 0x04001001 RID: 4097
		NotEquals,
		// Token: 0x04001002 RID: 4098
		Never
	}

	// Token: 0x0200037D RID: 893
	private static class EnumUtil<TEnum> where TEnum : struct, IConvertible, IFormattable, IComparable
	{
		// Token: 0x060021E1 RID: 8673 RVA: 0x000836A8 File Offset: 0x000818A8
		public static int ToInt(TEnum val)
		{
			return Convert.ToInt32(val);
		}
	}

	// Token: 0x0200037E RID: 894
	[StructLayout(LayoutKind.Explicit, Size = 4)]
	private struct OpBase
	{
		// Token: 0x060021E2 RID: 8674 RVA: 0x000836B8 File Offset: 0x000818B8
		public OpBase(byte _op, int _val)
		{
			this._val = _val;
			this._op = _op;
		}

		// Token: 0x04001003 RID: 4099
		[FieldOffset(0)]
		public int _val;

		// Token: 0x04001004 RID: 4100
		[FieldOffset(3)]
		public byte _op;
	}

	// Token: 0x0200037F RID: 895
	public struct Op<TFlags> : IEquatable<Vis.Op>, IEquatable<Vis.Op<TFlags>> where TFlags : struct, IConvertible, IFormattable, IComparable
	{
		// Token: 0x060021E3 RID: 8675 RVA: 0x000836C8 File Offset: 0x000818C8
		internal Op(byte op, int val)
		{
			this._ = new Vis.OpBase(op, val);
		}

		// Token: 0x060021E4 RID: 8676 RVA: 0x000836D8 File Offset: 0x000818D8
		public Op(Vis.Op op, TFlags flags)
		{
			this = new Vis.Op<TFlags>((byte)op, Convert.ToInt32(flags));
		}

		// Token: 0x060021E5 RID: 8677 RVA: 0x000836F0 File Offset: 0x000818F0
		internal Op(int op, int flags)
		{
			this = new Vis.Op<TFlags>((byte)op, flags);
		}

		// Token: 0x060021E6 RID: 8678 RVA: 0x000836FC File Offset: 0x000818FC
		private static int ToInt(TFlags f)
		{
			return Vis.EnumUtil<TFlags>.ToInt(f);
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x060021E7 RID: 8679 RVA: 0x00083704 File Offset: 0x00081904
		// (set) Token: 0x060021E8 RID: 8680 RVA: 0x00083714 File Offset: 0x00081914
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

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x060021E9 RID: 8681 RVA: 0x00083724 File Offset: 0x00081924
		// (set) Token: 0x060021EA RID: 8682 RVA: 0x00083734 File Offset: 0x00081934
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

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x060021EB RID: 8683 RVA: 0x00083744 File Offset: 0x00081944
		// (set) Token: 0x060021EC RID: 8684 RVA: 0x00083774 File Offset: 0x00081974
		public TFlags value
		{
			get
			{
				return (TFlags)((object)Enum.ToObject(typeof(TFlags), this._val & 16777215));
			}
			set
			{
				this._val = ((this._val & 16777216) | (Vis.Op<TFlags>.ToInt(value) & 16777215));
			}
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x060021ED RID: 8685 RVA: 0x00083798 File Offset: 0x00081998
		// (set) Token: 0x060021EE RID: 8686 RVA: 0x000837A8 File Offset: 0x000819A8
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

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x060021EF RID: 8687 RVA: 0x000837C4 File Offset: 0x000819C4
		// (set) Token: 0x060021F0 RID: 8688 RVA: 0x000837D0 File Offset: 0x000819D0
		public Vis.Op op
		{
			get
			{
				return (Vis.Op)(this._op & 127);
			}
			set
			{
				this._op = ((this._op & 128) | ((byte)value & 127));
			}
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x060021F1 RID: 8689 RVA: 0x000837EC File Offset: 0x000819EC
		// (set) Token: 0x060021F2 RID: 8690 RVA: 0x000837F4 File Offset: 0x000819F4
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

		// Token: 0x060021F3 RID: 8691 RVA: 0x00083800 File Offset: 0x00081A00
		public override int GetHashCode()
		{
			return this._val & int.MaxValue;
		}

		// Token: 0x060021F4 RID: 8692 RVA: 0x00083810 File Offset: 0x00081A10
		public override string ToString()
		{
			return this.op + ':' + this.value;
		}

		// Token: 0x060021F5 RID: 8693 RVA: 0x00083840 File Offset: 0x00081A40
		public override bool Equals(object obj)
		{
			if (obj is Vis.Op<TFlags>)
			{
				return this.Equals((Vis.Op<TFlags>)obj);
			}
			if (obj is Vis.Op)
			{
				return this.Equals((Vis.Op)((int)obj));
			}
			return obj.Equals(this);
		}

		// Token: 0x060021F6 RID: 8694 RVA: 0x00083890 File Offset: 0x00081A90
		public bool Equals(Vis.Op<TFlags> other)
		{
			return other._val == this;
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x000838A4 File Offset: 0x00081AA4
		public bool Equals(Vis.Op other)
		{
			return other == this.op;
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x000838B0 File Offset: 0x00081AB0
		public Vis.Op<TFlags>.Res Eval(int flags)
		{
			return new Vis.Op<TFlags>.Res(this, (TFlags)((object)Enum.ToObject(typeof(TFlags), flags)), flags);
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x000838D4 File Offset: 0x00081AD4
		public Vis.Op<TFlags>.Res Eval(TFlags flags)
		{
			return new Vis.Op<TFlags>.Res(this, flags, Vis.Op<TFlags>.ToInt(flags));
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x000838E8 File Offset: 0x00081AE8
		public static bool operator ==(Vis.Op<TFlags> op, TFlags flags)
		{
			return Vis.Evaluate(op.op, op._val & 16777215, Vis.Op<TFlags>.ToInt(flags));
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x00083914 File Offset: 0x00081B14
		public static bool operator ==(TFlags flags, Vis.Op<TFlags> op)
		{
			return Vis.Evaluate(op.op, op._val & 16777215, Vis.Op<TFlags>.ToInt(flags));
		}

		// Token: 0x060021FC RID: 8700 RVA: 0x00083940 File Offset: 0x00081B40
		public static bool operator !=(Vis.Op<TFlags> op, TFlags flags)
		{
			return !Vis.Evaluate(op.op, op._val & 16777215, Vis.Op<TFlags>.ToInt(flags));
		}

		// Token: 0x060021FD RID: 8701 RVA: 0x00083970 File Offset: 0x00081B70
		public static bool operator !=(TFlags flags, Vis.Op<TFlags> op)
		{
			return !Vis.Evaluate(op.op, op._val & 16777215, Vis.Op<TFlags>.ToInt(flags));
		}

		// Token: 0x060021FE RID: 8702 RVA: 0x000839A0 File Offset: 0x00081BA0
		public static Vis.Op<TFlags>.Res operator +(Vis.Op<TFlags> op, TFlags flags)
		{
			return op.Eval(flags);
		}

		// Token: 0x060021FF RID: 8703 RVA: 0x000839AC File Offset: 0x00081BAC
		public static Vis.Op<TFlags>.Res operator +(Vis.Op<TFlags> op, int flags)
		{
			return op.Eval(flags);
		}

		// Token: 0x06002200 RID: 8704 RVA: 0x000839B8 File Offset: 0x00081BB8
		public static Vis.Op<TFlags>.Res operator -(Vis.Op<TFlags> op, TFlags flags)
		{
			return op.Eval(flags);
		}

		// Token: 0x06002201 RID: 8705 RVA: 0x000839C4 File Offset: 0x00081BC4
		public static Vis.Op<TFlags>.Res operator -(Vis.Op<TFlags> op, int flags)
		{
			return op.Eval(flags);
		}

		// Token: 0x06002202 RID: 8706 RVA: 0x000839D0 File Offset: 0x00081BD0
		public static Vis.Op<TFlags>operator -(Vis.Op<TFlags> op)
		{
			op.op = Vis.Negate(op.op);
			return op;
		}

		// Token: 0x06002203 RID: 8707 RVA: 0x000839E8 File Offset: 0x00081BE8
		public static bool operator ==(Vis.Op<TFlags> L, Vis.Op R)
		{
			return (int)L._op == (int)((sbyte)R);
		}

		// Token: 0x06002204 RID: 8708 RVA: 0x000839F8 File Offset: 0x00081BF8
		public static bool operator ==(Vis.Op L, Vis.Op<TFlags> R)
		{
			return (int)R._op == (int)((sbyte)L);
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x00083A08 File Offset: 0x00081C08
		public static bool operator !=(Vis.Op<TFlags> L, Vis.Op R)
		{
			return (int)L._op != (int)((sbyte)R);
		}

		// Token: 0x06002206 RID: 8710 RVA: 0x00083A1C File Offset: 0x00081C1C
		public static bool operator !=(Vis.Op L, Vis.Op<TFlags> R)
		{
			return (int)R._op != (int)((sbyte)L);
		}

		// Token: 0x06002207 RID: 8711 RVA: 0x00083A30 File Offset: 0x00081C30
		public static bool operator ==(Vis.Op<TFlags> L, int R)
		{
			return L._val == R;
		}

		// Token: 0x06002208 RID: 8712 RVA: 0x00083A3C File Offset: 0x00081C3C
		public static bool operator ==(int R, Vis.Op<TFlags> L)
		{
			return L._val == R;
		}

		// Token: 0x06002209 RID: 8713 RVA: 0x00083A48 File Offset: 0x00081C48
		public static bool operator !=(Vis.Op<TFlags> L, int R)
		{
			return L._val != R;
		}

		// Token: 0x0600220A RID: 8714 RVA: 0x00083A58 File Offset: 0x00081C58
		public static bool operator !=(int R, Vis.Op<TFlags> L)
		{
			return L._val != R;
		}

		// Token: 0x0600220B RID: 8715 RVA: 0x00083A68 File Offset: 0x00081C68
		public static bool operator ==(Vis.Op<TFlags> L, Vis.Op<TFlags> R)
		{
			return L._val == R._val;
		}

		// Token: 0x0600220C RID: 8716 RVA: 0x00083A7C File Offset: 0x00081C7C
		public static bool operator !=(Vis.Op<TFlags> L, Vis.Op<TFlags> R)
		{
			return L._val != R._val;
		}

		// Token: 0x0600220D RID: 8717 RVA: 0x00083A94 File Offset: 0x00081C94
		public static implicit operator Vis.Op<TFlags>(int data)
		{
			return new Vis.Op<TFlags>
			{
				_val = data
			};
		}

		// Token: 0x0600220E RID: 8718 RVA: 0x00083AB4 File Offset: 0x00081CB4
		public static implicit operator int(Vis.Op<TFlags> op)
		{
			return op._val;
		}

		// Token: 0x0600220F RID: 8719 RVA: 0x00083AC0 File Offset: 0x00081CC0
		public static implicit operator Vis.Op(Vis.Op<TFlags> op)
		{
			return op.op;
		}

		// Token: 0x04001005 RID: 4101
		private Vis.OpBase _;

		// Token: 0x02000380 RID: 896
		public struct Res
		{
			// Token: 0x06002210 RID: 8720 RVA: 0x00083ACC File Offset: 0x00081CCC
			internal Res(Vis.Op<TFlags> op, TFlags flags, int flagsint)
			{
				this._op = op;
				this.query = flags;
				if (Vis.Evaluate(op.op, op.intvalue, flagsint))
				{
					this._op._val = (this._op._val | int.MinValue);
				}
				else
				{
					this._op._val = (this._op._val & int.MaxValue);
				}
			}

			// Token: 0x17000841 RID: 2113
			// (get) Token: 0x06002211 RID: 8721 RVA: 0x00083B34 File Offset: 0x00081D34
			public Vis.Op<TFlags> operation
			{
				get
				{
					return this._op;
				}
			}

			// Token: 0x17000842 RID: 2114
			// (get) Token: 0x06002212 RID: 8722 RVA: 0x00083B3C File Offset: 0x00081D3C
			public bool passed
			{
				get
				{
					return (this._op._val & int.MinValue) == int.MinValue;
				}
			}

			// Token: 0x17000843 RID: 2115
			// (get) Token: 0x06002213 RID: 8723 RVA: 0x00083B64 File Offset: 0x00081D64
			public bool failed
			{
				get
				{
					return (this._op._val & int.MinValue) != int.MinValue;
				}
			}

			// Token: 0x17000844 RID: 2116
			// (get) Token: 0x06002214 RID: 8724 RVA: 0x00083B90 File Offset: 0x00081D90
			public TFlags value
			{
				get
				{
					return this._op.value;
				}
			}

			// Token: 0x17000845 RID: 2117
			// (get) Token: 0x06002215 RID: 8725 RVA: 0x00083BAC File Offset: 0x00081DAC
			public int intvalue
			{
				get
				{
					return this._op.intvalue;
				}
			}

			// Token: 0x17000846 RID: 2118
			// (get) Token: 0x06002216 RID: 8726 RVA: 0x00083BC8 File Offset: 0x00081DC8
			public int data
			{
				get
				{
					return this._op._val;
				}
			}

			// Token: 0x06002217 RID: 8727 RVA: 0x00083BE4 File Offset: 0x00081DE4
			public override int GetHashCode()
			{
				return (int.MinValue | this._op._val) ^ Vis.Op<TFlags>.ToInt(this.query);
			}

			// Token: 0x06002218 RID: 8728 RVA: 0x00083C14 File Offset: 0x00081E14
			public override string ToString()
			{
				return string.Format("{0}({1}) == {2}", this.operation, this.query, this.passed);
			}

			// Token: 0x06002219 RID: 8729 RVA: 0x00083C4C File Offset: 0x00081E4C
			public static implicit operator bool(Vis.Op<TFlags>.Res r)
			{
				return r.passed;
			}

			// Token: 0x0600221A RID: 8730 RVA: 0x00083C58 File Offset: 0x00081E58
			public static bool operator !(Vis.Op<TFlags>.Res r)
			{
				return r.failed;
			}

			// Token: 0x04001006 RID: 4102
			public readonly TFlags query;

			// Token: 0x04001007 RID: 4103
			private readonly Vis.Op<TFlags> _op;
		}
	}

	// Token: 0x02000381 RID: 897
	[Flags]
	public enum Life
	{
		// Token: 0x04001009 RID: 4105
		Alive = 1,
		// Token: 0x0400100A RID: 4106
		Unconcious = 2,
		// Token: 0x0400100B RID: 4107
		Dead = 4
	}

	// Token: 0x02000382 RID: 898
	[Flags]
	public enum Status
	{
		// Token: 0x0400100D RID: 4109
		Casual = 1,
		// Token: 0x0400100E RID: 4110
		Hurt = 2,
		// Token: 0x0400100F RID: 4111
		Curious = 4,
		// Token: 0x04001010 RID: 4112
		Alert = 8,
		// Token: 0x04001011 RID: 4113
		Search = 16,
		// Token: 0x04001012 RID: 4114
		Armed = 32,
		// Token: 0x04001013 RID: 4115
		Attacking = 64
	}

	// Token: 0x02000383 RID: 899
	[Flags]
	public enum Role
	{
		// Token: 0x04001015 RID: 4117
		Citizen = 1,
		// Token: 0x04001016 RID: 4118
		Criminal = 2,
		// Token: 0x04001017 RID: 4119
		Authority = 4,
		// Token: 0x04001018 RID: 4120
		Target = 8,
		// Token: 0x04001019 RID: 4121
		Entourage = 16,
		// Token: 0x0400101A RID: 4122
		Player = 32,
		// Token: 0x0400101B RID: 4123
		Vehicle = 64,
		// Token: 0x0400101C RID: 4124
		Animal = 128
	}

	// Token: 0x02000384 RID: 900
	[StructLayout(LayoutKind.Explicit, Size = 4)]
	public struct Mask
	{
		// Token: 0x0600221B RID: 8731 RVA: 0x00083C64 File Offset: 0x00081E64
		static Mask()
		{
			int num = 0;
			Vis.Mask.sect(0, ref num);
			BitVector32.Section? section = new BitVector32.Section?(Vis.Mask.sect(3, ref num));
			Vis.Mask.sect(5, ref num);
			BitVector32.Section? section2 = new BitVector32.Section?(Vis.Mask.sect(7, ref num));
			Vis.Mask.sect(9, ref num);
			BitVector32.Section? section3 = new BitVector32.Section?(Vis.Mask.sect(8, ref num));
			Vis.Mask.s_life = section.GetValueOrDefault();
			Vis.Mask.s_stat = section2.GetValueOrDefault();
			Vis.Mask.s_role = section3.GetValueOrDefault();
		}

		// Token: 0x0600221C RID: 8732 RVA: 0x00083CEC File Offset: 0x00081EEC
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

		// Token: 0x0600221D RID: 8733 RVA: 0x00083DA0 File Offset: 0x00081FA0
		private static BitVector32.Section sect(int count, ref int i)
		{
			return Vis.Mask.sect_(count, ref i);
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x0600221E RID: 8734 RVA: 0x00083DAC File Offset: 0x00081FAC
		// (set) Token: 0x0600221F RID: 8735 RVA: 0x00083DC0 File Offset: 0x00081FC0
		public Vis.Life life
		{
			get
			{
				return (Vis.Life)this.bits[Vis.Mask.s_life];
			}
			set
			{
				this.bits[Vis.Mask.s_life] = (int)(value & (Vis.Life)Vis.Mask.s_life.Mask);
			}
		}

		// Token: 0x17000848 RID: 2120
		public bool this[Vis.Life mask]
		{
			get
			{
				return (this.life & mask) != (Vis.Life)0;
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

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06002222 RID: 8738 RVA: 0x00083E28 File Offset: 0x00082028
		// (set) Token: 0x06002223 RID: 8739 RVA: 0x00083E3C File Offset: 0x0008203C
		public Vis.Status stat
		{
			get
			{
				return (Vis.Status)this.bits[Vis.Mask.s_stat];
			}
			set
			{
				this.bits[Vis.Mask.s_stat] = (int)(value & (Vis.Status)Vis.Mask.s_stat.Mask);
			}
		}

		// Token: 0x1700084A RID: 2122
		public bool this[Vis.Status mask]
		{
			get
			{
				return (this.stat & mask) != (Vis.Status)0;
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

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06002226 RID: 8742 RVA: 0x00083EA4 File Offset: 0x000820A4
		// (set) Token: 0x06002227 RID: 8743 RVA: 0x00083EB8 File Offset: 0x000820B8
		public Vis.Role role
		{
			get
			{
				return (Vis.Role)this.bits[Vis.Mask.s_role];
			}
			set
			{
				this.bits[Vis.Mask.s_role] = (int)(value & (Vis.Role)Vis.Mask.s_role.Mask);
			}
		}

		// Token: 0x1700084C RID: 2124
		public bool this[Vis.Role mask]
		{
			get
			{
				return (this.role & mask) != (Vis.Role)0;
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

		// Token: 0x1700084D RID: 2125
		public bool this[Vis.Op op, Vis.Life val]
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

		// Token: 0x1700084E RID: 2126
		public bool this[Vis.Op op, Vis.Status val]
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

		// Token: 0x1700084F RID: 2127
		public bool this[Vis.Op op, Vis.Role val]
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

		// Token: 0x17000850 RID: 2128
		public Vis.Op<Vis.Life>.Res this[Vis.Op<Vis.Life> op]
		{
			get
			{
				return op.Eval(this.bits[Vis.Mask.s_life]);
			}
		}

		// Token: 0x17000851 RID: 2129
		public Vis.Op<Vis.Status>.Res this[Vis.Op<Vis.Status> op]
		{
			get
			{
				return op.Eval(this.bits[Vis.Mask.s_stat]);
			}
		}

		// Token: 0x17000852 RID: 2130
		public Vis.Op<Vis.Role>.Res this[Vis.Op<Vis.Role> op]
		{
			get
			{
				return op.Eval(this.bits[Vis.Mask.s_role]);
			}
		}

		// Token: 0x06002233 RID: 8755 RVA: 0x00083FBC File Offset: 0x000821BC
		public bool All(Vis.Life f)
		{
			return (this.life & f) == f;
		}

		// Token: 0x06002234 RID: 8756 RVA: 0x00083FCC File Offset: 0x000821CC
		public bool All(Vis.Role f)
		{
			return (this.role & f) == f;
		}

		// Token: 0x06002235 RID: 8757 RVA: 0x00083FDC File Offset: 0x000821DC
		public bool All(Vis.Status f)
		{
			return (this.stat & f) == f;
		}

		// Token: 0x06002236 RID: 8758 RVA: 0x00083FEC File Offset: 0x000821EC
		public bool Any(Vis.Life f)
		{
			return (this.life & f) > (Vis.Life)0;
		}

		// Token: 0x06002237 RID: 8759 RVA: 0x00083FFC File Offset: 0x000821FC
		public bool Any(Vis.Role f)
		{
			return (this.role & f) > (Vis.Role)0;
		}

		// Token: 0x06002238 RID: 8760 RVA: 0x0008400C File Offset: 0x0008220C
		public bool Any(Vis.Status f)
		{
			return (this.stat & f) > (Vis.Status)0;
		}

		// Token: 0x06002239 RID: 8761 RVA: 0x0008401C File Offset: 0x0008221C
		public bool AllMore(Vis.Life f)
		{
			Vis.Life life = this.life;
			return life > f && (life & f) == f;
		}

		// Token: 0x0600223A RID: 8762 RVA: 0x00084040 File Offset: 0x00082240
		public bool AllMore(Vis.Role f)
		{
			Vis.Role role = this.role;
			return role > f && (role & f) == f;
		}

		// Token: 0x0600223B RID: 8763 RVA: 0x00084064 File Offset: 0x00082264
		public bool AllMore(Vis.Status f)
		{
			Vis.Status stat = this.stat;
			return stat > f && (stat & f) == f;
		}

		// Token: 0x0600223C RID: 8764 RVA: 0x00084088 File Offset: 0x00082288
		public bool AnyLess(Vis.Life f)
		{
			Vis.Life life = this.life;
			return (life & f) < f;
		}

		// Token: 0x0600223D RID: 8765 RVA: 0x000840A4 File Offset: 0x000822A4
		public bool AnyLess(Vis.Role f)
		{
			Vis.Role role = this.role;
			return (role & f) < f;
		}

		// Token: 0x0600223E RID: 8766 RVA: 0x000840C0 File Offset: 0x000822C0
		public bool AnyLess(Vis.Status f)
		{
			Vis.Status stat = this.stat;
			return (stat & f) < f;
		}

		// Token: 0x0600223F RID: 8767 RVA: 0x000840DC File Offset: 0x000822DC
		public bool Equals(Vis.Life f)
		{
			return this.life == f;
		}

		// Token: 0x06002240 RID: 8768 RVA: 0x000840E8 File Offset: 0x000822E8
		public bool Equals(Vis.Role f)
		{
			return this.role == f;
		}

		// Token: 0x06002241 RID: 8769 RVA: 0x000840F4 File Offset: 0x000822F4
		public bool Equals(Vis.Status f)
		{
			return this.stat == f;
		}

		// Token: 0x06002242 RID: 8770 RVA: 0x00084100 File Offset: 0x00082300
		public void Append(Vis.Life f)
		{
			this.life |= f;
		}

		// Token: 0x06002243 RID: 8771 RVA: 0x00084110 File Offset: 0x00082310
		public void Append(Vis.Role f)
		{
			this.role |= f;
		}

		// Token: 0x06002244 RID: 8772 RVA: 0x00084120 File Offset: 0x00082320
		public void Append(Vis.Status f)
		{
			this.stat |= f;
		}

		// Token: 0x06002245 RID: 8773 RVA: 0x00084130 File Offset: 0x00082330
		public Vis.Life Not(Vis.Life f)
		{
			return (this.life ^ f) & f;
		}

		// Token: 0x06002246 RID: 8774 RVA: 0x0008413C File Offset: 0x0008233C
		public Vis.Role Not(Vis.Role f)
		{
			return (this.role ^ f) & f;
		}

		// Token: 0x06002247 RID: 8775 RVA: 0x00084148 File Offset: 0x00082348
		public Vis.Status Not(Vis.Status f)
		{
			return (this.stat ^ f) & f;
		}

		// Token: 0x06002248 RID: 8776 RVA: 0x00084154 File Offset: 0x00082354
		public Vis.Life AppendNot(Vis.Life f)
		{
			Vis.Life life = (this.life ^ f) & f;
			this.life |= life;
			return life;
		}

		// Token: 0x06002249 RID: 8777 RVA: 0x0008417C File Offset: 0x0008237C
		public Vis.Role AppendNot(Vis.Role f)
		{
			Vis.Role role = (this.role ^ f) & f;
			this.role |= role;
			return role;
		}

		// Token: 0x0600224A RID: 8778 RVA: 0x000841A4 File Offset: 0x000823A4
		public Vis.Status AppendNot(Vis.Status f)
		{
			Vis.Status status = (this.stat ^ f) & f;
			this.stat |= status;
			return status;
		}

		// Token: 0x0600224B RID: 8779 RVA: 0x000841CC File Offset: 0x000823CC
		public bool Eval(Vis.Op op, Vis.Life f)
		{
			return Vis.Evaluate(op, (int)f, this.bits[Vis.Mask.s_life]);
		}

		// Token: 0x0600224C RID: 8780 RVA: 0x000841E8 File Offset: 0x000823E8
		public bool Eval(Vis.Op<Vis.Life> op)
		{
			return op == this.life;
		}

		// Token: 0x0600224D RID: 8781 RVA: 0x000841F8 File Offset: 0x000823F8
		public Vis.Life Apply(Vis.Op op, Vis.Life f)
		{
			return (Vis.Life)Vis.SetTrue(op, (int)f, ref this.bits, Vis.Mask.s_life);
		}

		// Token: 0x0600224E RID: 8782 RVA: 0x0008420C File Offset: 0x0008240C
		public Vis.Life Apply(Vis.Op<Vis.Life> op)
		{
			return (Vis.Life)Vis.SetTrue(op, op.intvalue, ref this.bits, Vis.Mask.s_life);
		}

		// Token: 0x0600224F RID: 8783 RVA: 0x0008422C File Offset: 0x0008242C
		public bool Eval(Vis.Op op, Vis.Status f)
		{
			return Vis.Evaluate(op, (int)f, this.bits[Vis.Mask.s_stat]);
		}

		// Token: 0x06002250 RID: 8784 RVA: 0x00084248 File Offset: 0x00082448
		public bool Eval(Vis.Op<Vis.Status> op)
		{
			return op == this.stat;
		}

		// Token: 0x06002251 RID: 8785 RVA: 0x00084258 File Offset: 0x00082458
		public Vis.Status Apply(Vis.Op op, Vis.Status f)
		{
			return (Vis.Status)Vis.SetTrue(op, (int)f, ref this.bits, Vis.Mask.s_stat);
		}

		// Token: 0x06002252 RID: 8786 RVA: 0x0008426C File Offset: 0x0008246C
		public Vis.Status Apply(Vis.Op<Vis.Status> op)
		{
			return (Vis.Status)Vis.SetTrue(op, op.intvalue, ref this.bits, Vis.Mask.s_stat);
		}

		// Token: 0x06002253 RID: 8787 RVA: 0x0008428C File Offset: 0x0008248C
		public bool Eval(Vis.Op op, Vis.Role f)
		{
			return Vis.Evaluate(op, (int)f, this.bits[Vis.Mask.s_role]);
		}

		// Token: 0x06002254 RID: 8788 RVA: 0x000842A8 File Offset: 0x000824A8
		public bool Eval(Vis.Op<Vis.Role> op)
		{
			return op == this.role;
		}

		// Token: 0x06002255 RID: 8789 RVA: 0x000842B8 File Offset: 0x000824B8
		public Vis.Role Apply(Vis.Op op, Vis.Role f)
		{
			return (Vis.Role)Vis.SetTrue(op, (int)f, ref this.bits, Vis.Mask.s_role);
		}

		// Token: 0x06002256 RID: 8790 RVA: 0x000842CC File Offset: 0x000824CC
		public Vis.Role Apply(Vis.Op<Vis.Role> op)
		{
			return (Vis.Role)Vis.SetTrue(op, op.intvalue, ref this.bits, Vis.Mask.s_role);
		}

		// Token: 0x06002257 RID: 8791 RVA: 0x000842EC File Offset: 0x000824EC
		public void Remove(Vis.Life f)
		{
			this.life &= ~f;
		}

		// Token: 0x06002258 RID: 8792 RVA: 0x00084300 File Offset: 0x00082500
		public void Remove(Vis.Role f)
		{
			this.role &= ~f;
		}

		// Token: 0x06002259 RID: 8793 RVA: 0x00084314 File Offset: 0x00082514
		public void Remove(Vis.Status f)
		{
			this.stat &= ~f;
		}

		// Token: 0x17000853 RID: 2131
		public bool this[Vis.Trait trait]
		{
			get
			{
				return this.bits[1 << (int)trait];
			}
		}

		// Token: 0x17000854 RID: 2132
		public bool this[int mask]
		{
			get
			{
				return this.bits[mask];
			}
		}

		// Token: 0x0400101D RID: 4125
		public const int kAlive = 1;

		// Token: 0x0400101E RID: 4126
		public const int kUnconcious = 2;

		// Token: 0x0400101F RID: 4127
		public const int kDead = 4;

		// Token: 0x04001020 RID: 4128
		public const int kCasual = 256;

		// Token: 0x04001021 RID: 4129
		public const int kHurt = 512;

		// Token: 0x04001022 RID: 4130
		public const int kCurious = 1024;

		// Token: 0x04001023 RID: 4131
		public const int kAlert = 2048;

		// Token: 0x04001024 RID: 4132
		public const int kSearch = 4096;

		// Token: 0x04001025 RID: 4133
		public const int kArmed = 8192;

		// Token: 0x04001026 RID: 4134
		public const int kAttacking = 16384;

		// Token: 0x04001027 RID: 4135
		public const int kCriminal = 33554432;

		// Token: 0x04001028 RID: 4136
		public const int kAuthority = 67108864;

		// Token: 0x04001029 RID: 4137
		private static BitVector32.Section s_life;

		// Token: 0x0400102A RID: 4138
		private static BitVector32.Section s_stat;

		// Token: 0x0400102B RID: 4139
		private static BitVector32.Section s_role;

		// Token: 0x0400102C RID: 4140
		[FieldOffset(0)]
		public BitVector32 bits;

		// Token: 0x0400102D RID: 4141
		[FieldOffset(0)]
		public int data;

		// Token: 0x0400102E RID: 4142
		[FieldOffset(0)]
		public uint udata;

		// Token: 0x0400102F RID: 4143
		public static readonly Vis.Mask zero = default(Vis.Mask);
	}

	// Token: 0x02000385 RID: 901
	[Flags]
	public enum Flag
	{
		// Token: 0x04001031 RID: 4145
		Zero = 0,
		// Token: 0x04001032 RID: 4146
		Relative = 1,
		// Token: 0x04001033 RID: 4147
		Target = 4,
		// Token: 0x04001034 RID: 4148
		Self = 8
	}

	// Token: 0x02000386 RID: 902
	public enum Comparison
	{
		// Token: 0x04001036 RID: 4150
		Stealthy = 5,
		// Token: 0x04001037 RID: 4151
		Prey = 9,
		// Token: 0x04001038 RID: 4152
		IsSelf = 12,
		// Token: 0x04001039 RID: 4153
		Oblivious = 1,
		// Token: 0x0400103A RID: 4154
		Contact = 13
	}

	// Token: 0x02000387 RID: 903
	public enum Region
	{
		// Token: 0x0400103C RID: 4156
		Life,
		// Token: 0x0400103D RID: 4157
		Status,
		// Token: 0x0400103E RID: 4158
		Role
	}

	// Token: 0x02000388 RID: 904
	public struct Rule
	{
		// Token: 0x17000855 RID: 2133
		public Vis.Mask this[Vis.Rule.Step step]
		{
			get
			{
				switch (step)
				{
				case Vis.Rule.Step.Accept:
					return this.accept;
				case Vis.Rule.Step.Conditional:
					return this.conditional;
				case Vis.Rule.Step.Reject:
					return this.reject;
				default:
					throw new ArgumentOutOfRangeException("step");
				}
			}
			set
			{
				switch (step)
				{
				case Vis.Rule.Step.Accept:
					this.accept = value;
					break;
				case Vis.Rule.Step.Conditional:
					this.conditional = value;
					break;
				case Vis.Rule.Step.Reject:
					this.reject = value;
					break;
				default:
					throw new ArgumentOutOfRangeException("step");
				}
			}
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x0600225E RID: 8798 RVA: 0x000843EC File Offset: 0x000825EC
		// (set) Token: 0x0600225F RID: 8799 RVA: 0x00084420 File Offset: 0x00082620
		public Vis.Op<Vis.Life> rejectLife
		{
			get
			{
				return new Vis.Op<Vis.Life>((byte)this.setup.life.reject, (int)this.reject.life);
			}
			set
			{
				Vis.Rule.RegionSetup life = this.setup.life;
				life.reject = value.op;
				this.setup.life = life;
				this.reject.life = value.value;
			}
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06002260 RID: 8800 RVA: 0x00084468 File Offset: 0x00082668
		// (set) Token: 0x06002261 RID: 8801 RVA: 0x0008449C File Offset: 0x0008269C
		public Vis.Op<Vis.Status> rejectStatus
		{
			get
			{
				return new Vis.Op<Vis.Status>((byte)this.setup.stat.reject, (int)this.reject.stat);
			}
			set
			{
				Vis.Rule.RegionSetup life = this.setup.life;
				life.reject = value.op;
				this.setup.life = life;
				this.reject.stat = value.value;
			}
		}

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06002262 RID: 8802 RVA: 0x000844E4 File Offset: 0x000826E4
		// (set) Token: 0x06002263 RID: 8803 RVA: 0x00084518 File Offset: 0x00082718
		public Vis.Op<Vis.Role> rejectRole
		{
			get
			{
				return new Vis.Op<Vis.Role>((byte)this.setup.role.reject, (int)this.reject.role);
			}
			set
			{
				Vis.Rule.RegionSetup life = this.setup.life;
				life.reject = value.op;
				this.setup.life = life;
				this.reject.role = value.value;
			}
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06002264 RID: 8804 RVA: 0x00084560 File Offset: 0x00082760
		// (set) Token: 0x06002265 RID: 8805 RVA: 0x00084594 File Offset: 0x00082794
		public Vis.Op<Vis.Life> acceptLife
		{
			get
			{
				return new Vis.Op<Vis.Life>((byte)this.setup.life.accept, (int)this.accept.life);
			}
			set
			{
				Vis.Rule.RegionSetup life = this.setup.life;
				life.accept = value.op;
				this.setup.life = life;
				this.accept.life = value.value;
			}
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06002266 RID: 8806 RVA: 0x000845DC File Offset: 0x000827DC
		// (set) Token: 0x06002267 RID: 8807 RVA: 0x00084610 File Offset: 0x00082810
		public Vis.Op<Vis.Status> acceptStatus
		{
			get
			{
				return new Vis.Op<Vis.Status>((byte)this.setup.stat.accept, (int)this.accept.stat);
			}
			set
			{
				Vis.Rule.RegionSetup stat = this.setup.stat;
				stat.accept = value.op;
				this.setup.stat = stat;
				this.accept.stat = value.value;
			}
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06002268 RID: 8808 RVA: 0x00084658 File Offset: 0x00082858
		// (set) Token: 0x06002269 RID: 8809 RVA: 0x0008468C File Offset: 0x0008288C
		public Vis.Op<Vis.Role> acceptRole
		{
			get
			{
				return new Vis.Op<Vis.Role>((byte)this.setup.role.accept, (int)this.accept.role);
			}
			set
			{
				Vis.Rule.RegionSetup role = this.setup.role;
				role.accept = value.op;
				this.setup.role = role;
				this.accept.role = value.value;
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x0600226A RID: 8810 RVA: 0x000846D4 File Offset: 0x000828D4
		// (set) Token: 0x0600226B RID: 8811 RVA: 0x00084708 File Offset: 0x00082908
		public Vis.Op<Vis.Life> conditionalLife
		{
			get
			{
				return new Vis.Op<Vis.Life>((byte)this.setup.life.conditional, (int)this.conditional.life);
			}
			set
			{
				Vis.Rule.RegionSetup life = this.setup.life;
				life.conditional = value.op;
				this.setup.life = life;
				this.conditional.life = value.value;
			}
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x0600226C RID: 8812 RVA: 0x00084750 File Offset: 0x00082950
		// (set) Token: 0x0600226D RID: 8813 RVA: 0x00084784 File Offset: 0x00082984
		public Vis.Op<Vis.Status> conditionalStatus
		{
			get
			{
				return new Vis.Op<Vis.Status>((byte)this.setup.stat.conditional, (int)this.conditional.stat);
			}
			set
			{
				Vis.Rule.RegionSetup stat = this.setup.stat;
				stat.conditional = value.op;
				this.setup.stat = stat;
				this.conditional.stat = value.value;
			}
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x0600226E RID: 8814 RVA: 0x000847CC File Offset: 0x000829CC
		// (set) Token: 0x0600226F RID: 8815 RVA: 0x00084800 File Offset: 0x00082A00
		public Vis.Op<Vis.Role> conditionalRole
		{
			get
			{
				return new Vis.Op<Vis.Role>((byte)this.setup.role.conditional, (int)this.conditional.role);
			}
			set
			{
				Vis.Rule.RegionSetup role = this.setup.role;
				role.conditional = value.op;
				this.setup.role = role;
				this.conditional.role = value.value;
			}
		}

		// Token: 0x06002270 RID: 8816 RVA: 0x00084848 File Offset: 0x00082A48
		private Vis.Rule.Failure Accept(Vis.Mask mask)
		{
			if (!this.setup.checkAccept)
			{
				return Vis.Rule.Failure.None;
			}
			Vis.Rule.Failure failure = Vis.Rule.Failure.None;
			if (!mask.Eval(this.acceptLife))
			{
				failure |= (Vis.Rule.Failure.Conditional | Vis.Rule.Failure.Life);
			}
			if (!mask.Eval(this.acceptRole))
			{
				failure |= (Vis.Rule.Failure.Conditional | Vis.Rule.Failure.Role);
			}
			if (!mask.Eval(this.acceptStatus))
			{
				failure |= (Vis.Rule.Failure.Conditional | Vis.Rule.Failure.Status);
			}
			return failure;
		}

		// Token: 0x06002271 RID: 8817 RVA: 0x000848B0 File Offset: 0x00082AB0
		private Vis.Rule.Failure Conditional(Vis.Mask mask)
		{
			if (!this.setup.checkConditional)
			{
				return Vis.Rule.Failure.None;
			}
			Vis.Rule.Failure failure = Vis.Rule.Failure.None;
			if (!mask.Eval(this.conditionalLife))
			{
				failure |= (Vis.Rule.Failure.Conditional | Vis.Rule.Failure.Life);
			}
			if (!mask.Eval(this.conditionalRole))
			{
				failure |= (Vis.Rule.Failure.Conditional | Vis.Rule.Failure.Role);
			}
			if (!mask.Eval(this.conditionalStatus))
			{
				failure |= (Vis.Rule.Failure.Conditional | Vis.Rule.Failure.Status);
			}
			return failure;
		}

		// Token: 0x06002272 RID: 8818 RVA: 0x00084918 File Offset: 0x00082B18
		private Vis.Rule.Failure Reject(Vis.Mask mask)
		{
			if (!this.setup.checkReject)
			{
				return Vis.Rule.Failure.None;
			}
			Vis.Rule.Failure failure = Vis.Rule.Failure.None;
			if (mask.Eval(this.rejectLife))
			{
				failure |= (Vis.Rule.Failure.Reject | Vis.Rule.Failure.Life);
			}
			if (mask.Eval(this.rejectRole))
			{
				failure |= (Vis.Rule.Failure.Reject | Vis.Rule.Failure.Role);
			}
			if (mask.Eval(this.rejectStatus))
			{
				failure |= (Vis.Rule.Failure.Reject | Vis.Rule.Failure.Status);
			}
			return failure;
		}

		// Token: 0x06002273 RID: 8819 RVA: 0x00084980 File Offset: 0x00082B80
		private Vis.Rule.Failure Check(Vis.Mask a, Vis.Mask c, Vis.Mask r)
		{
			Vis.Rule.Failure failure = this.Accept(a);
			if (failure != Vis.Rule.Failure.None)
			{
				return failure;
			}
			failure = this.Conditional(c);
			if (failure != Vis.Rule.Failure.None)
			{
				return failure;
			}
			return this.Reject(r);
		}

		// Token: 0x06002274 RID: 8820 RVA: 0x000849B8 File Offset: 0x00082BB8
		public Vis.Rule.Failure Pass(Vis.Mask self, Vis.Mask other)
		{
			switch (this.setup.option)
			{
			default:
				return this.Check(other, self, other);
			case Vis.Rule.Setup.Option.Inverse:
				return this.Check(self, other, self);
			case Vis.Rule.Setup.Option.NoConditional:
				return this.Check(other, other, other);
			case Vis.Rule.Setup.Option.AllConditional:
				return this.Check(self, self, self);
			}
		}

		// Token: 0x06002275 RID: 8821 RVA: 0x00084A14 File Offset: 0x00082C14
		public static Vis.Rule Decode(int[] data, int index)
		{
			Vis.Rule result = default(Vis.Rule);
			result.setup.data = data[index++];
			result.accept.data = data[index++];
			result.conditional.data = data[index++];
			result.reject.data = data[index];
			return result;
		}

		// Token: 0x06002276 RID: 8822 RVA: 0x00084A78 File Offset: 0x00082C78
		public static void Encode(ref Vis.Rule rule, int[] data, int index)
		{
			data[index++] = rule.setup.data;
			data[index++] = rule.accept.data;
			data[index++] = rule.conditional.data;
			data[index++] = rule.reject.data;
		}

		// Token: 0x0400103F RID: 4159
		public Vis.Rule.Setup setup;

		// Token: 0x04001040 RID: 4160
		public Vis.Mask reject;

		// Token: 0x04001041 RID: 4161
		public Vis.Mask accept;

		// Token: 0x04001042 RID: 4162
		public Vis.Mask conditional;

		// Token: 0x02000389 RID: 905
		public enum Clearance
		{
			// Token: 0x04001044 RID: 4164
			Outside,
			// Token: 0x04001045 RID: 4165
			Enter,
			// Token: 0x04001046 RID: 4166
			Stay,
			// Token: 0x04001047 RID: 4167
			Exit
		}

		// Token: 0x0200038A RID: 906
		public enum Step
		{
			// Token: 0x04001049 RID: 4169
			Accept,
			// Token: 0x0400104A RID: 4170
			Conditional,
			// Token: 0x0400104B RID: 4171
			Reject
		}

		// Token: 0x0200038B RID: 907
		public struct RegionSetup
		{
			// Token: 0x06002277 RID: 8823 RVA: 0x00084AD4 File Offset: 0x00082CD4
			internal RegionSetup(int value, Vis.Region reg)
			{
				this._ = new BitVector32(value | (int)((int)reg << (int)(Vis.Rule.RegionSetup.s_region.Offset & 31)));
			}

			// Token: 0x06002278 RID: 8824 RVA: 0x00084B00 File Offset: 0x00082D00
			static RegionSetup()
			{
				BitVector32.Section previous = Vis.Rule.RegionSetup.s_apt = BitVector32.CreateSection(7);
				previous = (Vis.Rule.RegionSetup.s_cnd = BitVector32.CreateSection(7, previous));
				previous = (Vis.Rule.RegionSetup.s_rej = BitVector32.CreateSection(7, previous));
				Vis.Rule.RegionSetup.s_whole = BitVector32.CreateSection(511);
				previous = BitVector32.CreateSection(7, previous);
				Vis.Rule.RegionSetup.s_region = BitVector32.CreateSection(3, previous);
			}

			// Token: 0x1700085F RID: 2143
			// (get) Token: 0x06002279 RID: 8825 RVA: 0x00084B5C File Offset: 0x00082D5C
			// (set) Token: 0x0600227A RID: 8826 RVA: 0x00084B70 File Offset: 0x00082D70
			public Vis.Op accept
			{
				get
				{
					return (Vis.Op)this._[Vis.Rule.RegionSetup.s_apt];
				}
				set
				{
					this._[Vis.Rule.RegionSetup.s_apt] = (int)value;
				}
			}

			// Token: 0x17000860 RID: 2144
			// (get) Token: 0x0600227B RID: 8827 RVA: 0x00084B84 File Offset: 0x00082D84
			// (set) Token: 0x0600227C RID: 8828 RVA: 0x00084B98 File Offset: 0x00082D98
			public Vis.Op conditional
			{
				get
				{
					return (Vis.Op)this._[Vis.Rule.RegionSetup.s_cnd];
				}
				set
				{
					this._[Vis.Rule.RegionSetup.s_cnd] = (int)value;
				}
			}

			// Token: 0x17000861 RID: 2145
			// (get) Token: 0x0600227D RID: 8829 RVA: 0x00084BAC File Offset: 0x00082DAC
			// (set) Token: 0x0600227E RID: 8830 RVA: 0x00084BC0 File Offset: 0x00082DC0
			public Vis.Op reject
			{
				get
				{
					return (Vis.Op)this._[Vis.Rule.RegionSetup.s_rej];
				}
				set
				{
					this._[Vis.Rule.RegionSetup.s_rej] = (int)value;
				}
			}

			// Token: 0x17000862 RID: 2146
			// (get) Token: 0x0600227F RID: 8831 RVA: 0x00084BD4 File Offset: 0x00082DD4
			// (set) Token: 0x06002280 RID: 8832 RVA: 0x00084BE8 File Offset: 0x00082DE8
			public Vis.Region region
			{
				get
				{
					return (Vis.Region)this._[Vis.Rule.RegionSetup.s_region];
				}
				set
				{
					this._[Vis.Rule.RegionSetup.s_region] = (int)value;
				}
			}

			// Token: 0x17000863 RID: 2147
			public Vis.Op this[Vis.Rule.Step step]
			{
				get
				{
					switch (step)
					{
					case Vis.Rule.Step.Accept:
						return this.accept;
					case Vis.Rule.Step.Conditional:
						return this.conditional;
					case Vis.Rule.Step.Reject:
						return this.reject;
					default:
						throw new ArgumentOutOfRangeException("step");
					}
				}
				set
				{
					switch (step)
					{
					case Vis.Rule.Step.Accept:
						this.accept = value;
						break;
					case Vis.Rule.Step.Conditional:
						this.conditional = value;
						break;
					case Vis.Rule.Step.Reject:
						this.reject = value;
						break;
					default:
						throw new ArgumentOutOfRangeException("step");
					}
				}
			}

			// Token: 0x17000864 RID: 2148
			// (get) Token: 0x06002283 RID: 8835 RVA: 0x00084C9C File Offset: 0x00082E9C
			internal int dat
			{
				get
				{
					return this._[Vis.Rule.RegionSetup.s_whole];
				}
			}

			// Token: 0x0400104C RID: 4172
			private static readonly BitVector32.Section s_apt;

			// Token: 0x0400104D RID: 4173
			private static readonly BitVector32.Section s_cnd;

			// Token: 0x0400104E RID: 4174
			private static readonly BitVector32.Section s_rej;

			// Token: 0x0400104F RID: 4175
			private static readonly BitVector32.Section s_whole;

			// Token: 0x04001050 RID: 4176
			private static readonly BitVector32.Section s_region;

			// Token: 0x04001051 RID: 4177
			private BitVector32 _;
		}

		// Token: 0x0200038C RID: 908
		public struct StepSetup
		{
			// Token: 0x06002284 RID: 8836 RVA: 0x00084CB0 File Offset: 0x00082EB0
			internal StepSetup(int life, int stat, int role, int step, int enable)
			{
				this = default(Vis.Rule.StepSetup);
				this._[Vis.Rule.StepSetup.s_life] = life;
				this._[Vis.Rule.StepSetup.s_stat] = stat;
				this._[Vis.Rule.StepSetup.s_role] = role;
				this._[Vis.Rule.StepSetup.s_step] = step;
				this._[Vis.Rule.StepSetup.s_enable] = enable;
			}

			// Token: 0x06002285 RID: 8837 RVA: 0x00084D24 File Offset: 0x00082F24
			static StepSetup()
			{
				BitVector32.Section previous = Vis.Rule.StepSetup.s_life = BitVector32.CreateSection(7);
				previous = (Vis.Rule.StepSetup.s_step = BitVector32.CreateSection(255, previous));
				previous = (Vis.Rule.StepSetup.s_enable = BitVector32.CreateSection(1, previous));
				previous = (Vis.Rule.StepSetup.s_stat = BitVector32.CreateSection(7, previous));
				previous = BitVector32.CreateSection(511, previous);
				Vis.Rule.StepSetup.s_role = BitVector32.CreateSection(7, previous);
			}

			// Token: 0x17000865 RID: 2149
			// (get) Token: 0x06002286 RID: 8838 RVA: 0x00084D88 File Offset: 0x00082F88
			// (set) Token: 0x06002287 RID: 8839 RVA: 0x00084D9C File Offset: 0x00082F9C
			public Vis.Op life
			{
				get
				{
					return (Vis.Op)this._[Vis.Rule.StepSetup.s_life];
				}
				set
				{
					this._[Vis.Rule.StepSetup.s_life] = (int)value;
				}
			}

			// Token: 0x17000866 RID: 2150
			// (get) Token: 0x06002288 RID: 8840 RVA: 0x00084DB0 File Offset: 0x00082FB0
			// (set) Token: 0x06002289 RID: 8841 RVA: 0x00084DC4 File Offset: 0x00082FC4
			public Vis.Op stat
			{
				get
				{
					return (Vis.Op)this._[Vis.Rule.StepSetup.s_stat];
				}
				set
				{
					this._[Vis.Rule.StepSetup.s_stat] = (int)value;
				}
			}

			// Token: 0x17000867 RID: 2151
			// (get) Token: 0x0600228A RID: 8842 RVA: 0x00084DD8 File Offset: 0x00082FD8
			// (set) Token: 0x0600228B RID: 8843 RVA: 0x00084DEC File Offset: 0x00082FEC
			public Vis.Op role
			{
				get
				{
					return (Vis.Op)this._[Vis.Rule.StepSetup.s_role];
				}
				set
				{
					this._[Vis.Rule.StepSetup.s_role] = (int)value;
				}
			}

			// Token: 0x17000868 RID: 2152
			// (get) Token: 0x0600228C RID: 8844 RVA: 0x00084E00 File Offset: 0x00083000
			// (set) Token: 0x0600228D RID: 8845 RVA: 0x00084E14 File Offset: 0x00083014
			public Vis.Rule.Step step
			{
				get
				{
					return (Vis.Rule.Step)this._[Vis.Rule.StepSetup.s_step];
				}
				set
				{
					this._[Vis.Rule.StepSetup.s_step] = (int)value;
				}
			}

			// Token: 0x17000869 RID: 2153
			// (get) Token: 0x0600228E RID: 8846 RVA: 0x00084E28 File Offset: 0x00083028
			// (set) Token: 0x0600228F RID: 8847 RVA: 0x00084E40 File Offset: 0x00083040
			public bool enabled
			{
				get
				{
					return this._[Vis.Rule.StepSetup.s_enable] != 0;
				}
				set
				{
					this._[Vis.Rule.StepSetup.s_enable] = ((!value) ? 0 : 1);
				}
			}

			// Token: 0x1700086A RID: 2154
			public Vis.Op this[Vis.Region region]
			{
				get
				{
					switch (region)
					{
					case Vis.Region.Life:
						return this.life;
					case Vis.Region.Status:
						return this.stat;
					case Vis.Region.Role:
						return this.role;
					default:
						throw new ArgumentOutOfRangeException("region");
					}
				}
				set
				{
					switch (region)
					{
					case Vis.Region.Life:
						this.life = value;
						break;
					case Vis.Region.Status:
						this.stat = value;
						break;
					case Vis.Region.Role:
						this.role = value;
						break;
					default:
						throw new ArgumentOutOfRangeException("region");
					}
				}
			}

			// Token: 0x04001052 RID: 4178
			private static readonly BitVector32.Section s_life;

			// Token: 0x04001053 RID: 4179
			private static readonly BitVector32.Section s_stat;

			// Token: 0x04001054 RID: 4180
			private static readonly BitVector32.Section s_role;

			// Token: 0x04001055 RID: 4181
			private static readonly BitVector32.Section s_step;

			// Token: 0x04001056 RID: 4182
			private static readonly BitVector32.Section s_enable;

			// Token: 0x04001057 RID: 4183
			private BitVector32 _;
		}

		// Token: 0x0200038D RID: 909
		public struct StepCheck
		{
			// Token: 0x06002292 RID: 8850 RVA: 0x00084F00 File Offset: 0x00083100
			internal StepCheck(int i)
			{
				this.bits = (byte)i;
			}

			// Token: 0x1700086B RID: 2155
			// (get) Token: 0x06002293 RID: 8851 RVA: 0x00084F0C File Offset: 0x0008310C
			// (set) Token: 0x06002294 RID: 8852 RVA: 0x00084F1C File Offset: 0x0008311C
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

			// Token: 0x1700086C RID: 2156
			// (get) Token: 0x06002295 RID: 8853 RVA: 0x00084F4C File Offset: 0x0008314C
			// (set) Token: 0x06002296 RID: 8854 RVA: 0x00084F5C File Offset: 0x0008315C
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

			// Token: 0x1700086D RID: 2157
			// (get) Token: 0x06002297 RID: 8855 RVA: 0x00084F8C File Offset: 0x0008318C
			// (set) Token: 0x06002298 RID: 8856 RVA: 0x00084F9C File Offset: 0x0008319C
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

			// Token: 0x1700086E RID: 2158
			// (get) Token: 0x06002299 RID: 8857 RVA: 0x00084FCC File Offset: 0x000831CC
			internal int value
			{
				get
				{
					return (int)this.bits;
				}
			}

			// Token: 0x1700086F RID: 2159
			public bool this[Vis.Rule.Step step]
			{
				get
				{
					switch (step)
					{
					case Vis.Rule.Step.Accept:
						return this.accept;
					case Vis.Rule.Step.Conditional:
						return this.conditional;
					case Vis.Rule.Step.Reject:
						return this.reject;
					default:
						throw new ArgumentOutOfRangeException("step");
					}
				}
				set
				{
					switch (step)
					{
					case Vis.Rule.Step.Accept:
						this.accept = value;
						break;
					case Vis.Rule.Step.Conditional:
						this.conditional = value;
						break;
					case Vis.Rule.Step.Reject:
						this.reject = value;
						break;
					default:
						throw new ArgumentOutOfRangeException("step");
					}
				}
			}

			// Token: 0x04001058 RID: 4184
			private byte bits;
		}

		// Token: 0x0200038E RID: 910
		public struct Setup
		{
			// Token: 0x0600229C RID: 8860 RVA: 0x00085074 File Offset: 0x00083274
			static Setup()
			{
				BitVector32.Section previous = Vis.Rule.Setup.s_life = BitVector32.CreateSection(511);
				previous = (Vis.Rule.Setup.s_stat = BitVector32.CreateSection(511, previous));
				previous = (Vis.Rule.Setup.s_role = BitVector32.CreateSection(511, previous));
				previous = (Vis.Rule.Setup.s_options = BitVector32.CreateSection(3, previous));
				Vis.Rule.Setup.s_check = BitVector32.CreateSection(7, previous);
				Vis.Rule.Setup.s_life_ = new BitVector32.Section[3];
				Vis.Rule.Setup.s_stat_ = new BitVector32.Section[3];
				Vis.Rule.Setup.s_role_ = new BitVector32.Section[3];
				Vis.Rule.Setup.s_check_ = new BitVector32.Section[3];
				int i = 0;
				Vis.Rule.Setup.s_life_[i] = BitVector32.CreateSection(7);
				Vis.Rule.Setup.s_stat_[i] = BitVector32.CreateSection(7, Vis.Rule.Setup.s_life);
				Vis.Rule.Setup.s_role_[i] = BitVector32.CreateSection(7, Vis.Rule.Setup.s_stat);
				Vis.Rule.Setup.s_check_[i] = BitVector32.CreateSection(1, Vis.Rule.Setup.s_options);
				for (i++; i < 3; i++)
				{
					Vis.Rule.Setup.s_life_[i] = BitVector32.CreateSection(7, Vis.Rule.Setup.s_life_[i - 1]);
					Vis.Rule.Setup.s_stat_[i] = BitVector32.CreateSection(7, Vis.Rule.Setup.s_stat_[i - 1]);
					Vis.Rule.Setup.s_role_[i] = BitVector32.CreateSection(7, Vis.Rule.Setup.s_role_[i - 1]);
					Vis.Rule.Setup.s_check_[i] = BitVector32.CreateSection(1, Vis.Rule.Setup.s_check_[i - 1]);
				}
			}

			// Token: 0x17000870 RID: 2160
			// (get) Token: 0x0600229D RID: 8861 RVA: 0x0008521C File Offset: 0x0008341C
			// (set) Token: 0x0600229E RID: 8862 RVA: 0x00085234 File Offset: 0x00083434
			public Vis.Rule.RegionSetup life
			{
				get
				{
					return new Vis.Rule.RegionSetup(this._[Vis.Rule.Setup.s_life], Vis.Region.Life);
				}
				set
				{
					this._[Vis.Rule.Setup.s_life] = value.dat;
				}
			}

			// Token: 0x17000871 RID: 2161
			// (get) Token: 0x0600229F RID: 8863 RVA: 0x00085250 File Offset: 0x00083450
			// (set) Token: 0x060022A0 RID: 8864 RVA: 0x00085268 File Offset: 0x00083468
			public Vis.Rule.RegionSetup stat
			{
				get
				{
					return new Vis.Rule.RegionSetup(this._[Vis.Rule.Setup.s_stat], Vis.Region.Status);
				}
				set
				{
					this._[Vis.Rule.Setup.s_stat] = value.dat;
				}
			}

			// Token: 0x17000872 RID: 2162
			// (get) Token: 0x060022A1 RID: 8865 RVA: 0x00085284 File Offset: 0x00083484
			// (set) Token: 0x060022A2 RID: 8866 RVA: 0x0008529C File Offset: 0x0008349C
			public Vis.Rule.RegionSetup role
			{
				get
				{
					return new Vis.Rule.RegionSetup(this._[Vis.Rule.Setup.s_role], Vis.Region.Role);
				}
				set
				{
					this._[Vis.Rule.Setup.s_role] = value.dat;
				}
			}

			// Token: 0x060022A3 RID: 8867 RVA: 0x000852B8 File Offset: 0x000834B8
			private Vis.Rule.StepSetup Get(int i)
			{
				return new Vis.Rule.StepSetup(this._[Vis.Rule.Setup.s_life_[i]], this._[Vis.Rule.Setup.s_stat_[i]], this._[Vis.Rule.Setup.s_role_[i]], i, this._[Vis.Rule.Setup.s_check_[i]]);
			}

			// Token: 0x060022A4 RID: 8868 RVA: 0x00085338 File Offset: 0x00083538
			private void Set(int i, Vis.Rule.StepSetup step)
			{
				this._[Vis.Rule.Setup.s_life_[i]] = (int)(step.life & (Vis.Op)Vis.Rule.Setup.s_life_[i].Mask);
				this._[Vis.Rule.Setup.s_stat_[i]] = (int)(step.stat & (Vis.Op)Vis.Rule.Setup.s_stat_[i].Mask);
				this._[Vis.Rule.Setup.s_role_[i]] = (int)(step.role & (Vis.Op)Vis.Rule.Setup.s_role_[i].Mask);
				this._[Vis.Rule.Setup.s_check_[i]] = ((!step.enabled) ? 0 : 1);
			}

			// Token: 0x17000873 RID: 2163
			// (get) Token: 0x060022A5 RID: 8869 RVA: 0x0008540C File Offset: 0x0008360C
			// (set) Token: 0x060022A6 RID: 8870 RVA: 0x00085418 File Offset: 0x00083618
			public Vis.Rule.StepSetup accept
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

			// Token: 0x17000874 RID: 2164
			// (get) Token: 0x060022A7 RID: 8871 RVA: 0x00085424 File Offset: 0x00083624
			// (set) Token: 0x060022A8 RID: 8872 RVA: 0x00085430 File Offset: 0x00083630
			public Vis.Rule.StepSetup conditional
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

			// Token: 0x17000875 RID: 2165
			// (get) Token: 0x060022A9 RID: 8873 RVA: 0x0008543C File Offset: 0x0008363C
			// (set) Token: 0x060022AA RID: 8874 RVA: 0x00085448 File Offset: 0x00083648
			public Vis.Rule.StepSetup reject
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

			// Token: 0x17000876 RID: 2166
			public Vis.Rule.RegionSetup this[Vis.Region region]
			{
				get
				{
					switch (region)
					{
					case Vis.Region.Life:
						return this.life;
					case Vis.Region.Status:
						return this.stat;
					case Vis.Region.Role:
						return this.role;
					default:
						throw new ArgumentOutOfRangeException("region");
					}
				}
				set
				{
					switch (region)
					{
					case Vis.Region.Life:
						this.life = value;
						break;
					case Vis.Region.Status:
						this.stat = value;
						break;
					case Vis.Region.Role:
						this.role = value;
						break;
					default:
						throw new ArgumentOutOfRangeException("region");
					}
				}
			}

			// Token: 0x17000877 RID: 2167
			public Vis.Rule.StepSetup this[Vis.Rule.Step step]
			{
				get
				{
					switch (step)
					{
					case Vis.Rule.Step.Accept:
						return this.accept;
					case Vis.Rule.Step.Conditional:
						return this.conditional;
					case Vis.Rule.Step.Reject:
						return this.reject;
					default:
						throw new ArgumentOutOfRangeException("step");
					}
				}
				set
				{
					switch (step)
					{
					case Vis.Rule.Step.Accept:
						this.accept = value;
						break;
					case Vis.Rule.Step.Conditional:
						this.conditional = value;
						break;
					case Vis.Rule.Step.Reject:
						this.reject = value;
						break;
					default:
						throw new ArgumentOutOfRangeException("step");
					}
				}
			}

			// Token: 0x17000878 RID: 2168
			// (get) Token: 0x060022AF RID: 8879 RVA: 0x00085594 File Offset: 0x00083794
			// (set) Token: 0x060022B0 RID: 8880 RVA: 0x000855A8 File Offset: 0x000837A8
			public Vis.Rule.Setup.Option option
			{
				get
				{
					return (Vis.Rule.Setup.Option)this._[Vis.Rule.Setup.s_options];
				}
				set
				{
					this._[Vis.Rule.Setup.s_options] = (int)value;
				}
			}

			// Token: 0x17000879 RID: 2169
			// (get) Token: 0x060022B1 RID: 8881 RVA: 0x000855BC File Offset: 0x000837BC
			// (set) Token: 0x060022B2 RID: 8882 RVA: 0x000855D4 File Offset: 0x000837D4
			public Vis.Rule.StepCheck check
			{
				get
				{
					return new Vis.Rule.StepCheck(this._[Vis.Rule.Setup.s_check]);
				}
				set
				{
					this._[Vis.Rule.Setup.s_check] = value.value;
				}
			}

			// Token: 0x1700087A RID: 2170
			// (get) Token: 0x060022B3 RID: 8883 RVA: 0x000855F0 File Offset: 0x000837F0
			// (set) Token: 0x060022B4 RID: 8884 RVA: 0x00085614 File Offset: 0x00083814
			public bool checkAccept
			{
				get
				{
					return this._[Vis.Rule.Setup.s_check_[0]] != 0;
				}
				set
				{
					this._[Vis.Rule.Setup.s_check_[0]] = ((!value) ? 0 : 1);
				}
			}

			// Token: 0x1700087B RID: 2171
			// (get) Token: 0x060022B5 RID: 8885 RVA: 0x0008564C File Offset: 0x0008384C
			// (set) Token: 0x060022B6 RID: 8886 RVA: 0x00085670 File Offset: 0x00083870
			public bool checkConditional
			{
				get
				{
					return this._[Vis.Rule.Setup.s_check_[1]] != 0;
				}
				set
				{
					this._[Vis.Rule.Setup.s_check_[1]] = ((!value) ? 0 : 1);
				}
			}

			// Token: 0x1700087C RID: 2172
			// (get) Token: 0x060022B7 RID: 8887 RVA: 0x000856A8 File Offset: 0x000838A8
			// (set) Token: 0x060022B8 RID: 8888 RVA: 0x000856CC File Offset: 0x000838CC
			public bool checkReject
			{
				get
				{
					return this._[Vis.Rule.Setup.s_check_[2]] != 0;
				}
				set
				{
					this._[Vis.Rule.Setup.s_check_[2]] = ((!value) ? 0 : 1);
				}
			}

			// Token: 0x060022B9 RID: 8889 RVA: 0x00085704 File Offset: 0x00083904
			public void SetSetup(Vis.Rule.RegionSetup operations)
			{
				this[operations.region] = operations;
			}

			// Token: 0x060022BA RID: 8890 RVA: 0x00085714 File Offset: 0x00083914
			public void SetSetup(Vis.Rule.StepSetup operations)
			{
				this[operations.step] = operations;
			}

			// Token: 0x1700087D RID: 2173
			// (get) Token: 0x060022BB RID: 8891 RVA: 0x00085724 File Offset: 0x00083924
			// (set) Token: 0x060022BC RID: 8892 RVA: 0x00085734 File Offset: 0x00083934
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

			// Token: 0x04001059 RID: 4185
			private static readonly BitVector32.Section s_life;

			// Token: 0x0400105A RID: 4186
			private static readonly BitVector32.Section[] s_life_;

			// Token: 0x0400105B RID: 4187
			private static readonly BitVector32.Section s_stat;

			// Token: 0x0400105C RID: 4188
			private static readonly BitVector32.Section[] s_stat_;

			// Token: 0x0400105D RID: 4189
			private static readonly BitVector32.Section s_role;

			// Token: 0x0400105E RID: 4190
			private static readonly BitVector32.Section[] s_role_;

			// Token: 0x0400105F RID: 4191
			private static readonly BitVector32.Section s_options;

			// Token: 0x04001060 RID: 4192
			private static readonly BitVector32.Section s_check;

			// Token: 0x04001061 RID: 4193
			private static readonly BitVector32.Section[] s_check_;

			// Token: 0x04001062 RID: 4194
			private BitVector32 _;

			// Token: 0x0200038F RID: 911
			public enum Option
			{
				// Token: 0x04001064 RID: 4196
				Default,
				// Token: 0x04001065 RID: 4197
				Inverse,
				// Token: 0x04001066 RID: 4198
				NoConditional,
				// Token: 0x04001067 RID: 4199
				AllConditional
			}
		}

		// Token: 0x02000390 RID: 912
		[Flags]
		public enum Failure
		{
			// Token: 0x04001069 RID: 4201
			None = 0,
			// Token: 0x0400106A RID: 4202
			Accept = 1,
			// Token: 0x0400106B RID: 4203
			Conditional = 2,
			// Token: 0x0400106C RID: 4204
			Reject = 4,
			// Token: 0x0400106D RID: 4205
			Life = 8,
			// Token: 0x0400106E RID: 4206
			Role = 16,
			// Token: 0x0400106F RID: 4207
			Status = 32
		}
	}

	// Token: 0x02000391 RID: 913
	public struct Stamp
	{
		// Token: 0x060022BD RID: 8893 RVA: 0x00085744 File Offset: 0x00083944
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

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x060022BE RID: 8894 RVA: 0x000857F8 File Offset: 0x000839F8
		public Vector3 forward
		{
			get
			{
				return new Vector3(this.plane.x, this.plane.y, this.plane.z);
			}
		}

		// Token: 0x060022BF RID: 8895 RVA: 0x0008582C File Offset: 0x00083A2C
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

		// Token: 0x04001070 RID: 4208
		public Vector3 position;

		// Token: 0x04001071 RID: 4209
		public Vector4 plane;

		// Token: 0x04001072 RID: 4210
		public Quaternion rotation;
	}
}
