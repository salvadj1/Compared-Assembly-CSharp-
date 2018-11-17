using System;

// Token: 0x02000131 RID: 305
public struct CharacterStateFlags : IFormattable, IEquatable<global::CharacterStateFlags>
{
	// Token: 0x060007F0 RID: 2032 RVA: 0x0002284C File Offset: 0x00020A4C
	public CharacterStateFlags(bool crouching, bool sprinting, bool aiming, bool attacking, bool airborne, bool slipping, bool moving, bool lostFocus, bool lamp, bool laser)
	{
		ushort num = 0;
		if (crouching)
		{
			num |= 1;
		}
		if (sprinting)
		{
			num |= 2;
		}
		if (aiming)
		{
			num |= 4;
		}
		if (attacking)
		{
			num |= 8;
		}
		if (airborne)
		{
			num |= 16;
		}
		if (slipping)
		{
			num |= 32;
		}
		if (moving)
		{
			num |= 64;
		}
		if (lostFocus)
		{
			num |= 128;
		}
		if (lamp)
		{
			num |= 2048;
		}
		if (laser)
		{
			num |= 4096;
		}
		this.flags = num;
	}

	// Token: 0x170001DA RID: 474
	// (get) Token: 0x060007F1 RID: 2033 RVA: 0x000228E8 File Offset: 0x00020AE8
	// (set) Token: 0x060007F2 RID: 2034 RVA: 0x000228F8 File Offset: 0x00020AF8
	public bool crouch
	{
		get
		{
			return (this.flags & 1) == 1;
		}
		set
		{
			if (value)
			{
				this.flags |= 1;
			}
			else
			{
				this.flags &= 65534;
			}
		}
	}

	// Token: 0x170001DB RID: 475
	// (get) Token: 0x060007F3 RID: 2035 RVA: 0x00022928 File Offset: 0x00020B28
	// (set) Token: 0x060007F4 RID: 2036 RVA: 0x00022938 File Offset: 0x00020B38
	public bool sprint
	{
		get
		{
			return (this.flags & 2) == 2;
		}
		set
		{
			if (value)
			{
				this.flags |= 2;
			}
			else
			{
				this.flags &= 65533;
			}
		}
	}

	// Token: 0x170001DC RID: 476
	// (get) Token: 0x060007F5 RID: 2037 RVA: 0x00022968 File Offset: 0x00020B68
	// (set) Token: 0x060007F6 RID: 2038 RVA: 0x00022980 File Offset: 0x00020B80
	public bool crouchBlocked
	{
		get
		{
			return (this.flags & 1024) == 1024;
		}
		set
		{
			if (value)
			{
				this.flags |= 1024;
			}
			else
			{
				this.flags &= 64511;
			}
		}
	}

	// Token: 0x170001DD RID: 477
	// (get) Token: 0x060007F7 RID: 2039 RVA: 0x000229B4 File Offset: 0x00020BB4
	// (set) Token: 0x060007F8 RID: 2040 RVA: 0x000229C4 File Offset: 0x00020BC4
	public bool aim
	{
		get
		{
			return (this.flags & 4) == 4;
		}
		set
		{
			if (value)
			{
				this.flags |= 4;
			}
			else
			{
				this.flags &= 65531;
			}
		}
	}

	// Token: 0x170001DE RID: 478
	// (get) Token: 0x060007F9 RID: 2041 RVA: 0x000229F4 File Offset: 0x00020BF4
	// (set) Token: 0x060007FA RID: 2042 RVA: 0x00022A04 File Offset: 0x00020C04
	public bool attack
	{
		get
		{
			return (this.flags & 8) == 8;
		}
		set
		{
			if (value)
			{
				this.flags |= 8;
			}
			else
			{
				this.flags &= 65527;
			}
		}
	}

	// Token: 0x170001DF RID: 479
	// (get) Token: 0x060007FB RID: 2043 RVA: 0x00022A34 File Offset: 0x00020C34
	// (set) Token: 0x060007FC RID: 2044 RVA: 0x00022A48 File Offset: 0x00020C48
	public bool attack2
	{
		get
		{
			return (this.flags & 8) == 256;
		}
		set
		{
			if (value)
			{
				this.flags |= 256;
			}
			else
			{
				this.flags &= 65279;
			}
		}
	}

	// Token: 0x170001E0 RID: 480
	// (get) Token: 0x060007FD RID: 2045 RVA: 0x00022A7C File Offset: 0x00020C7C
	// (set) Token: 0x060007FE RID: 2046 RVA: 0x00022A90 File Offset: 0x00020C90
	public bool grounded
	{
		get
		{
			return (this.flags & 16) != 16;
		}
		set
		{
			if (value)
			{
				this.flags &= 65519;
			}
			else
			{
				this.flags |= 16;
			}
		}
	}

	// Token: 0x170001E1 RID: 481
	// (get) Token: 0x060007FF RID: 2047 RVA: 0x00022ACC File Offset: 0x00020CCC
	// (set) Token: 0x06000800 RID: 2048 RVA: 0x00022AE4 File Offset: 0x00020CE4
	public bool bleeding
	{
		get
		{
			return (this.flags & 512) != 512;
		}
		set
		{
			if (value)
			{
				this.flags &= 65023;
			}
			else
			{
				this.flags |= 512;
			}
		}
	}

	// Token: 0x170001E2 RID: 482
	// (get) Token: 0x06000801 RID: 2049 RVA: 0x00022B18 File Offset: 0x00020D18
	// (set) Token: 0x06000802 RID: 2050 RVA: 0x00022B28 File Offset: 0x00020D28
	public bool airborne
	{
		get
		{
			return (this.flags & 16) == 16;
		}
		set
		{
			if (value)
			{
				this.flags |= 16;
			}
			else
			{
				this.flags &= 65519;
			}
		}
	}

	// Token: 0x170001E3 RID: 483
	// (get) Token: 0x06000803 RID: 2051 RVA: 0x00022B64 File Offset: 0x00020D64
	// (set) Token: 0x06000804 RID: 2052 RVA: 0x00022B74 File Offset: 0x00020D74
	public bool slipping
	{
		get
		{
			return (this.flags & 32) == 32;
		}
		set
		{
			if (value)
			{
				this.flags |= 32;
			}
			else
			{
				this.flags &= 65503;
			}
		}
	}

	// Token: 0x170001E4 RID: 484
	// (get) Token: 0x06000805 RID: 2053 RVA: 0x00022BB0 File Offset: 0x00020DB0
	// (set) Token: 0x06000806 RID: 2054 RVA: 0x00022BC0 File Offset: 0x00020DC0
	public bool movement
	{
		get
		{
			return (this.flags & 64) == 64;
		}
		set
		{
			if (value)
			{
				this.flags |= 64;
			}
			else
			{
				this.flags &= 65471;
			}
		}
	}

	// Token: 0x170001E5 RID: 485
	// (get) Token: 0x06000807 RID: 2055 RVA: 0x00022BFC File Offset: 0x00020DFC
	// (set) Token: 0x06000808 RID: 2056 RVA: 0x00022C14 File Offset: 0x00020E14
	public bool lostFocus
	{
		get
		{
			return (this.flags & 128) == 128;
		}
		set
		{
			if (value)
			{
				this.flags |= 128;
			}
			else
			{
				this.flags &= 65407;
			}
		}
	}

	// Token: 0x170001E6 RID: 486
	// (get) Token: 0x06000809 RID: 2057 RVA: 0x00022C48 File Offset: 0x00020E48
	// (set) Token: 0x0600080A RID: 2058 RVA: 0x00022C60 File Offset: 0x00020E60
	public bool focus
	{
		get
		{
			return (this.flags & 128) != 128;
		}
		set
		{
			if (value)
			{
				this.flags &= 65407;
			}
			else
			{
				this.flags |= 128;
			}
		}
	}

	// Token: 0x170001E7 RID: 487
	// (get) Token: 0x0600080B RID: 2059 RVA: 0x00022C94 File Offset: 0x00020E94
	// (set) Token: 0x0600080C RID: 2060 RVA: 0x00022CAC File Offset: 0x00020EAC
	public bool lamp
	{
		get
		{
			return (this.flags & 2048) == 2048;
		}
		set
		{
			if (value)
			{
				this.flags |= 2048;
			}
			else
			{
				this.flags &= 63487;
			}
		}
	}

	// Token: 0x170001E8 RID: 488
	// (get) Token: 0x0600080D RID: 2061 RVA: 0x00022CE0 File Offset: 0x00020EE0
	// (set) Token: 0x0600080E RID: 2062 RVA: 0x00022CF8 File Offset: 0x00020EF8
	public bool laser
	{
		get
		{
			return (this.flags & 4096) == 4096;
		}
		set
		{
			if (value)
			{
				this.flags |= 4096;
			}
			else
			{
				this.flags &= 61439;
			}
		}
	}

	// Token: 0x170001E9 RID: 489
	// (get) Token: 0x0600080F RID: 2063 RVA: 0x00022D2C File Offset: 0x00020F2C
	// (set) Token: 0x06000810 RID: 2064 RVA: 0x00022D50 File Offset: 0x00020F50
	public global::CharacterStateFlags off
	{
		get
		{
			global::CharacterStateFlags result;
			result.flags = (~this.flags & ushort.MaxValue);
			return result;
		}
		set
		{
			this.flags = (~value.flags & ushort.MaxValue);
		}
	}

	// Token: 0x06000811 RID: 2065 RVA: 0x00022D68 File Offset: 0x00020F68
	public override bool Equals(object obj)
	{
		return obj is global::CharacterStateFlags && ((global::CharacterStateFlags)obj).flags == this.flags;
	}

	// Token: 0x06000812 RID: 2066 RVA: 0x00022D9C File Offset: 0x00020F9C
	public bool Equals(global::CharacterStateFlags other)
	{
		return this.flags == other.flags;
	}

	// Token: 0x06000813 RID: 2067 RVA: 0x00022DB0 File Offset: 0x00020FB0
	public override int GetHashCode()
	{
		return this.flags.GetHashCode();
	}

	// Token: 0x06000814 RID: 2068 RVA: 0x00022DC0 File Offset: 0x00020FC0
	public override string ToString()
	{
		return this.flags.ToString();
	}

	// Token: 0x06000815 RID: 2069 RVA: 0x00022DD0 File Offset: 0x00020FD0
	public string ToString(string format, IFormatProvider formatProvider)
	{
		return this.flags.ToString(format, formatProvider);
	}

	// Token: 0x06000816 RID: 2070 RVA: 0x00022DE0 File Offset: 0x00020FE0
	public string ToString(string format)
	{
		return this.flags.ToString(format, null);
	}

	// Token: 0x06000817 RID: 2071 RVA: 0x00022DF0 File Offset: 0x00020FF0
	public static global::CharacterStateFlags operator &(global::CharacterStateFlags lhs, global::CharacterStateFlags rhs)
	{
		lhs.flags &= rhs.flags;
		return lhs;
	}

	// Token: 0x06000818 RID: 2072 RVA: 0x00022E0C File Offset: 0x0002100C
	public static global::CharacterStateFlags operator |(global::CharacterStateFlags lhs, global::CharacterStateFlags rhs)
	{
		lhs.flags |= rhs.flags;
		return lhs;
	}

	// Token: 0x06000819 RID: 2073 RVA: 0x00022E28 File Offset: 0x00021028
	public static global::CharacterStateFlags operator ^(global::CharacterStateFlags lhs, global::CharacterStateFlags rhs)
	{
		lhs.flags ^= rhs.flags;
		return lhs;
	}

	// Token: 0x0600081A RID: 2074 RVA: 0x00022E44 File Offset: 0x00021044
	public static global::CharacterStateFlags operator &(global::CharacterStateFlags lhs, ushort rhs)
	{
		lhs.flags &= rhs;
		return lhs;
	}

	// Token: 0x0600081B RID: 2075 RVA: 0x00022E58 File Offset: 0x00021058
	public static global::CharacterStateFlags operator ^(global::CharacterStateFlags lhs, ushort rhs)
	{
		lhs.flags |= rhs;
		return lhs;
	}

	// Token: 0x0600081C RID: 2076 RVA: 0x00022E6C File Offset: 0x0002106C
	public static global::CharacterStateFlags operator |(global::CharacterStateFlags lhs, ushort rhs)
	{
		lhs.flags ^= rhs;
		return lhs;
	}

	// Token: 0x0600081D RID: 2077 RVA: 0x00022E80 File Offset: 0x00021080
	public static global::CharacterStateFlags operator &(global::CharacterStateFlags lhs, int rhs)
	{
		lhs.flags &= (ushort)(rhs & 65535);
		return lhs;
	}

	// Token: 0x0600081E RID: 2078 RVA: 0x00022E9C File Offset: 0x0002109C
	public static global::CharacterStateFlags operator ^(global::CharacterStateFlags lhs, int rhs)
	{
		lhs.flags |= (ushort)(rhs & 65535);
		return lhs;
	}

	// Token: 0x0600081F RID: 2079 RVA: 0x00022EB8 File Offset: 0x000210B8
	public static global::CharacterStateFlags operator |(global::CharacterStateFlags lhs, int rhs)
	{
		lhs.flags ^= (ushort)(rhs & 65535);
		return lhs;
	}

	// Token: 0x06000820 RID: 2080 RVA: 0x00022ED4 File Offset: 0x000210D4
	public static global::CharacterStateFlags operator &(global::CharacterStateFlags lhs, long rhs)
	{
		lhs.flags &= (ushort)(rhs & 65535L);
		return lhs;
	}

	// Token: 0x06000821 RID: 2081 RVA: 0x00022EF0 File Offset: 0x000210F0
	public static global::CharacterStateFlags operator ^(global::CharacterStateFlags lhs, long rhs)
	{
		lhs.flags |= (ushort)(rhs & 65535L);
		return lhs;
	}

	// Token: 0x06000822 RID: 2082 RVA: 0x00022F0C File Offset: 0x0002110C
	public static global::CharacterStateFlags operator |(global::CharacterStateFlags lhs, long rhs)
	{
		lhs.flags ^= (ushort)(rhs & 65535L);
		return lhs;
	}

	// Token: 0x06000823 RID: 2083 RVA: 0x00022F28 File Offset: 0x00021128
	public static global::CharacterStateFlags operator &(global::CharacterStateFlags lhs, uint rhs)
	{
		lhs.flags &= (ushort)(rhs & 65535u);
		return lhs;
	}

	// Token: 0x06000824 RID: 2084 RVA: 0x00022F44 File Offset: 0x00021144
	public static global::CharacterStateFlags operator ^(global::CharacterStateFlags lhs, uint rhs)
	{
		lhs.flags |= (ushort)(rhs & 65535u);
		return lhs;
	}

	// Token: 0x06000825 RID: 2085 RVA: 0x00022F60 File Offset: 0x00021160
	public static global::CharacterStateFlags operator |(global::CharacterStateFlags lhs, uint rhs)
	{
		lhs.flags ^= (ushort)(rhs & 65535u);
		return lhs;
	}

	// Token: 0x06000826 RID: 2086 RVA: 0x00022F7C File Offset: 0x0002117C
	public static global::CharacterStateFlags operator &(global::CharacterStateFlags lhs, ulong rhs)
	{
		lhs.flags &= (ushort)(rhs & 65535UL);
		return lhs;
	}

	// Token: 0x06000827 RID: 2087 RVA: 0x00022F98 File Offset: 0x00021198
	public static global::CharacterStateFlags operator ^(global::CharacterStateFlags lhs, ulong rhs)
	{
		lhs.flags |= (ushort)(rhs & 65535UL);
		return lhs;
	}

	// Token: 0x06000828 RID: 2088 RVA: 0x00022FB4 File Offset: 0x000211B4
	public static global::CharacterStateFlags operator |(global::CharacterStateFlags lhs, ulong rhs)
	{
		lhs.flags ^= (ushort)(rhs & 65535UL);
		return lhs;
	}

	// Token: 0x06000829 RID: 2089 RVA: 0x00022FD0 File Offset: 0x000211D0
	public static int operator &(int lhs, global::CharacterStateFlags rhs)
	{
		return lhs & (int)rhs.flags;
	}

	// Token: 0x0600082A RID: 2090 RVA: 0x00022FDC File Offset: 0x000211DC
	public static int operator |(int lhs, global::CharacterStateFlags rhs)
	{
		return lhs | (int)rhs.flags;
	}

	// Token: 0x0600082B RID: 2091 RVA: 0x00022FE8 File Offset: 0x000211E8
	public static int operator ^(int lhs, global::CharacterStateFlags rhs)
	{
		return lhs ^ (int)rhs.flags;
	}

	// Token: 0x0600082C RID: 2092 RVA: 0x00022FF4 File Offset: 0x000211F4
	public static uint operator &(uint lhs, global::CharacterStateFlags rhs)
	{
		return lhs & (uint)rhs.flags;
	}

	// Token: 0x0600082D RID: 2093 RVA: 0x00023000 File Offset: 0x00021200
	public static uint operator |(uint lhs, global::CharacterStateFlags rhs)
	{
		return lhs | (uint)rhs.flags;
	}

	// Token: 0x0600082E RID: 2094 RVA: 0x0002300C File Offset: 0x0002120C
	public static uint operator ^(uint lhs, global::CharacterStateFlags rhs)
	{
		return lhs ^ (uint)rhs.flags;
	}

	// Token: 0x0600082F RID: 2095 RVA: 0x00023018 File Offset: 0x00021218
	public static long operator &(long lhs, global::CharacterStateFlags rhs)
	{
		return lhs & (long)rhs.flags;
	}

	// Token: 0x06000830 RID: 2096 RVA: 0x00023024 File Offset: 0x00021224
	public static long operator |(long lhs, global::CharacterStateFlags rhs)
	{
		return lhs | (long)rhs.flags;
	}

	// Token: 0x06000831 RID: 2097 RVA: 0x00023030 File Offset: 0x00021230
	public static long operator ^(long lhs, global::CharacterStateFlags rhs)
	{
		return lhs ^ (long)rhs.flags;
	}

	// Token: 0x06000832 RID: 2098 RVA: 0x0002303C File Offset: 0x0002123C
	public static ulong operator &(ulong lhs, global::CharacterStateFlags rhs)
	{
		return lhs & (ulong)rhs.flags;
	}

	// Token: 0x06000833 RID: 2099 RVA: 0x00023048 File Offset: 0x00021248
	public static ulong operator |(ulong lhs, global::CharacterStateFlags rhs)
	{
		return lhs | (ulong)rhs.flags;
	}

	// Token: 0x06000834 RID: 2100 RVA: 0x00023054 File Offset: 0x00021254
	public static ulong operator ^(ulong lhs, global::CharacterStateFlags rhs)
	{
		return lhs ^ (ulong)rhs.flags;
	}

	// Token: 0x06000835 RID: 2101 RVA: 0x00023060 File Offset: 0x00021260
	public static int operator &(byte lhs, global::CharacterStateFlags rhs)
	{
		return (int)((ushort)lhs & rhs.flags);
	}

	// Token: 0x06000836 RID: 2102 RVA: 0x0002306C File Offset: 0x0002126C
	public static int operator |(byte lhs, global::CharacterStateFlags rhs)
	{
		return (int)((ushort)lhs | rhs.flags);
	}

	// Token: 0x06000837 RID: 2103 RVA: 0x00023078 File Offset: 0x00021278
	public static int operator ^(byte lhs, global::CharacterStateFlags rhs)
	{
		return (int)((ushort)lhs ^ rhs.flags);
	}

	// Token: 0x06000838 RID: 2104 RVA: 0x00023084 File Offset: 0x00021284
	public static int operator &(sbyte lhs, global::CharacterStateFlags rhs)
	{
		return (int)lhs & (int)rhs.flags;
	}

	// Token: 0x06000839 RID: 2105 RVA: 0x00023090 File Offset: 0x00021290
	public static int operator |(sbyte lhs, global::CharacterStateFlags rhs)
	{
		return (int)lhs | (int)rhs.flags;
	}

	// Token: 0x0600083A RID: 2106 RVA: 0x0002309C File Offset: 0x0002129C
	public static int operator ^(sbyte lhs, global::CharacterStateFlags rhs)
	{
		return (int)lhs ^ (int)rhs.flags;
	}

	// Token: 0x0600083B RID: 2107 RVA: 0x000230A8 File Offset: 0x000212A8
	public static int operator &(short lhs, global::CharacterStateFlags rhs)
	{
		return (int)(lhs & (short)rhs.flags);
	}

	// Token: 0x0600083C RID: 2108 RVA: 0x000230B4 File Offset: 0x000212B4
	public static int operator |(short lhs, global::CharacterStateFlags rhs)
	{
		return (int)(lhs | (short)rhs.flags);
	}

	// Token: 0x0600083D RID: 2109 RVA: 0x000230C0 File Offset: 0x000212C0
	public static int operator ^(short lhs, global::CharacterStateFlags rhs)
	{
		return (int)(lhs ^ (short)rhs.flags);
	}

	// Token: 0x0600083E RID: 2110 RVA: 0x000230CC File Offset: 0x000212CC
	public static int operator &(ushort lhs, global::CharacterStateFlags rhs)
	{
		return (int)(lhs & rhs.flags);
	}

	// Token: 0x0600083F RID: 2111 RVA: 0x000230D8 File Offset: 0x000212D8
	public static int operator |(ushort lhs, global::CharacterStateFlags rhs)
	{
		return (int)(lhs | rhs.flags);
	}

	// Token: 0x06000840 RID: 2112 RVA: 0x000230E4 File Offset: 0x000212E4
	public static int operator ^(ushort lhs, global::CharacterStateFlags rhs)
	{
		return (int)(lhs ^ rhs.flags);
	}

	// Token: 0x06000841 RID: 2113 RVA: 0x000230F0 File Offset: 0x000212F0
	public static bool operator ==(global::CharacterStateFlags lhs, global::CharacterStateFlags rhs)
	{
		return lhs.flags == rhs.flags;
	}

	// Token: 0x06000842 RID: 2114 RVA: 0x00023104 File Offset: 0x00021304
	public static bool operator ==(global::CharacterStateFlags lhs, byte rhs)
	{
		return lhs.flags == (ushort)rhs;
	}

	// Token: 0x06000843 RID: 2115 RVA: 0x00023110 File Offset: 0x00021310
	public static bool operator ==(global::CharacterStateFlags lhs, sbyte rhs)
	{
		return (int)lhs.flags == (int)rhs;
	}

	// Token: 0x06000844 RID: 2116 RVA: 0x00023120 File Offset: 0x00021320
	public static bool operator ==(global::CharacterStateFlags lhs, short rhs)
	{
		return lhs.flags == (ushort)rhs;
	}

	// Token: 0x06000845 RID: 2117 RVA: 0x0002312C File Offset: 0x0002132C
	public static bool operator ==(global::CharacterStateFlags lhs, ushort rhs)
	{
		return lhs.flags == rhs;
	}

	// Token: 0x06000846 RID: 2118 RVA: 0x00023138 File Offset: 0x00021338
	public static bool operator ==(global::CharacterStateFlags lhs, int rhs)
	{
		return (int)lhs.flags == rhs;
	}

	// Token: 0x06000847 RID: 2119 RVA: 0x00023144 File Offset: 0x00021344
	public static bool operator ==(global::CharacterStateFlags lhs, uint rhs)
	{
		return (uint)lhs.flags == rhs;
	}

	// Token: 0x06000848 RID: 2120 RVA: 0x00023150 File Offset: 0x00021350
	public static bool operator ==(global::CharacterStateFlags lhs, long rhs)
	{
		return (long)lhs.flags == rhs;
	}

	// Token: 0x06000849 RID: 2121 RVA: 0x00023160 File Offset: 0x00021360
	public static bool operator ==(global::CharacterStateFlags lhs, ulong rhs)
	{
		return (ulong)lhs.flags == rhs;
	}

	// Token: 0x0600084A RID: 2122 RVA: 0x00023170 File Offset: 0x00021370
	public static bool operator ==(global::CharacterStateFlags lhs, bool rhs)
	{
		return lhs.flags != 0 == rhs;
	}

	// Token: 0x0600084B RID: 2123 RVA: 0x00023184 File Offset: 0x00021384
	public static bool operator !=(global::CharacterStateFlags lhs, global::CharacterStateFlags rhs)
	{
		return lhs.flags != rhs.flags;
	}

	// Token: 0x0600084C RID: 2124 RVA: 0x0002319C File Offset: 0x0002139C
	public static bool operator !=(global::CharacterStateFlags lhs, byte rhs)
	{
		return lhs.flags != (ushort)rhs;
	}

	// Token: 0x0600084D RID: 2125 RVA: 0x000231AC File Offset: 0x000213AC
	public static bool operator !=(global::CharacterStateFlags lhs, sbyte rhs)
	{
		return (int)lhs.flags != (int)rhs;
	}

	// Token: 0x0600084E RID: 2126 RVA: 0x000231BC File Offset: 0x000213BC
	public static bool operator !=(global::CharacterStateFlags lhs, short rhs)
	{
		return lhs.flags != (ushort)rhs;
	}

	// Token: 0x0600084F RID: 2127 RVA: 0x000231CC File Offset: 0x000213CC
	public static bool operator !=(global::CharacterStateFlags lhs, ushort rhs)
	{
		return lhs.flags != rhs;
	}

	// Token: 0x06000850 RID: 2128 RVA: 0x000231DC File Offset: 0x000213DC
	public static bool operator !=(global::CharacterStateFlags lhs, int rhs)
	{
		return (int)lhs.flags != rhs;
	}

	// Token: 0x06000851 RID: 2129 RVA: 0x000231EC File Offset: 0x000213EC
	public static bool operator !=(global::CharacterStateFlags lhs, uint rhs)
	{
		return (uint)lhs.flags != rhs;
	}

	// Token: 0x06000852 RID: 2130 RVA: 0x000231FC File Offset: 0x000213FC
	public static bool operator !=(global::CharacterStateFlags lhs, long rhs)
	{
		return (long)lhs.flags != rhs;
	}

	// Token: 0x06000853 RID: 2131 RVA: 0x0002320C File Offset: 0x0002140C
	public static bool operator !=(global::CharacterStateFlags lhs, ulong rhs)
	{
		return (ulong)lhs.flags != rhs;
	}

	// Token: 0x06000854 RID: 2132 RVA: 0x0002321C File Offset: 0x0002141C
	public static bool operator !=(global::CharacterStateFlags lhs, bool rhs)
	{
		return lhs.flags == 0 == rhs;
	}

	// Token: 0x06000855 RID: 2133 RVA: 0x0002322C File Offset: 0x0002142C
	public static global::CharacterStateFlags operator >>(global::CharacterStateFlags lhs, int shift)
	{
		lhs.flags = (ushort)(lhs.flags >> shift);
		return lhs;
	}

	// Token: 0x06000856 RID: 2134 RVA: 0x00023244 File Offset: 0x00021444
	public static global::CharacterStateFlags operator <<(global::CharacterStateFlags lhs, int shift)
	{
		lhs.flags = (ushort)(lhs.flags >> shift);
		return lhs;
	}

	// Token: 0x06000857 RID: 2135 RVA: 0x0002325C File Offset: 0x0002145C
	public static global::CharacterStateFlags operator ~(global::CharacterStateFlags lhs)
	{
		lhs.flags = (~lhs.flags & ushort.MaxValue);
		return lhs;
	}

	// Token: 0x06000858 RID: 2136 RVA: 0x00023278 File Offset: 0x00021478
	public static bool operator true(global::CharacterStateFlags lhs)
	{
		return lhs.flags != 0;
	}

	// Token: 0x06000859 RID: 2137 RVA: 0x00023288 File Offset: 0x00021488
	public static bool operator false(global::CharacterStateFlags lhs)
	{
		return lhs.flags == 0;
	}

	// Token: 0x0600085A RID: 2138 RVA: 0x00023294 File Offset: 0x00021494
	public static explicit operator bool(global::CharacterStateFlags lhs)
	{
		return lhs.flags != 0;
	}

	// Token: 0x0600085B RID: 2139 RVA: 0x000232A4 File Offset: 0x000214A4
	public static bool operator !(global::CharacterStateFlags lhs)
	{
		return lhs.flags == 0;
	}

	// Token: 0x0600085C RID: 2140 RVA: 0x000232B0 File Offset: 0x000214B0
	public static implicit operator ushort(global::CharacterStateFlags lhs)
	{
		return lhs.flags;
	}

	// Token: 0x0600085D RID: 2141 RVA: 0x000232BC File Offset: 0x000214BC
	public static implicit operator global::CharacterStateFlags(ushort lhs)
	{
		global::CharacterStateFlags result;
		result.flags = lhs;
		return result;
	}

	// Token: 0x0600085E RID: 2142 RVA: 0x000232D4 File Offset: 0x000214D4
	public static implicit operator global::CharacterStateFlags(int lhs)
	{
		global::CharacterStateFlags result;
		result.flags = (ushort)(lhs & 65535);
		return result;
	}

	// Token: 0x0600085F RID: 2143 RVA: 0x000232F4 File Offset: 0x000214F4
	public static implicit operator global::CharacterStateFlags(long lhs)
	{
		global::CharacterStateFlags result;
		result.flags = (ushort)(lhs & 65535L);
		return result;
	}

	// Token: 0x06000860 RID: 2144 RVA: 0x00023314 File Offset: 0x00021514
	public static implicit operator global::CharacterStateFlags(uint lhs)
	{
		global::CharacterStateFlags result;
		result.flags = (ushort)(lhs & 65535u);
		return result;
	}

	// Token: 0x06000861 RID: 2145 RVA: 0x00023334 File Offset: 0x00021534
	public static implicit operator global::CharacterStateFlags(ulong lhs)
	{
		global::CharacterStateFlags result;
		result.flags = (ushort)(lhs & 65535UL);
		return result;
	}

	// Token: 0x0400060E RID: 1550
	public const ushort kCrouch = 1;

	// Token: 0x0400060F RID: 1551
	public const ushort kSprint = 2;

	// Token: 0x04000610 RID: 1552
	public const ushort kAim = 4;

	// Token: 0x04000611 RID: 1553
	public const ushort kAttack = 8;

	// Token: 0x04000612 RID: 1554
	public const ushort kAirborne = 16;

	// Token: 0x04000613 RID: 1555
	public const ushort kSlipping = 32;

	// Token: 0x04000614 RID: 1556
	public const ushort kMovement = 64;

	// Token: 0x04000615 RID: 1557
	public const ushort kLostFocus = 128;

	// Token: 0x04000616 RID: 1558
	public const ushort kAttack2 = 256;

	// Token: 0x04000617 RID: 1559
	public const ushort kBleeding = 512;

	// Token: 0x04000618 RID: 1560
	public const ushort kCrouchBlocked = 1024;

	// Token: 0x04000619 RID: 1561
	public const ushort kLamp = 2048;

	// Token: 0x0400061A RID: 1562
	public const ushort kLaser = 4096;

	// Token: 0x0400061B RID: 1563
	public const ushort kNone = 0;

	// Token: 0x0400061C RID: 1564
	public const ushort kMask = 8191;

	// Token: 0x0400061D RID: 1565
	private const ushort kAllMask = 65535;

	// Token: 0x0400061E RID: 1566
	private const ushort kNotCrouch = 65534;

	// Token: 0x0400061F RID: 1567
	private const ushort kNotSprint = 65533;

	// Token: 0x04000620 RID: 1568
	private const ushort kNotAim = 65531;

	// Token: 0x04000621 RID: 1569
	private const ushort kNotAttack = 65527;

	// Token: 0x04000622 RID: 1570
	private const ushort kNotAirborne = 65519;

	// Token: 0x04000623 RID: 1571
	private const ushort kNotSlipping = 65503;

	// Token: 0x04000624 RID: 1572
	private const ushort kNotMovement = 65471;

	// Token: 0x04000625 RID: 1573
	private const ushort kNotLostFocus = 65407;

	// Token: 0x04000626 RID: 1574
	private const ushort kNotAttack2 = 65279;

	// Token: 0x04000627 RID: 1575
	private const ushort kNotBleeding = 65023;

	// Token: 0x04000628 RID: 1576
	private const ushort kNotCrouchBlocked = 64511;

	// Token: 0x04000629 RID: 1577
	private const ushort kNotLamp = 63487;

	// Token: 0x0400062A RID: 1578
	private const ushort kNotLaser = 61439;

	// Token: 0x0400062B RID: 1579
	public ushort flags;
}
