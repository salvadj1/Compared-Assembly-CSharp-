using System;

// Token: 0x02000112 RID: 274
public struct CharacterStateFlags : IFormattable, IEquatable<CharacterStateFlags>
{
	// Token: 0x0600071E RID: 1822 RVA: 0x0001FC78 File Offset: 0x0001DE78
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

	// Token: 0x170001AC RID: 428
	// (get) Token: 0x0600071F RID: 1823 RVA: 0x0001FD14 File Offset: 0x0001DF14
	// (set) Token: 0x06000720 RID: 1824 RVA: 0x0001FD24 File Offset: 0x0001DF24
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

	// Token: 0x170001AD RID: 429
	// (get) Token: 0x06000721 RID: 1825 RVA: 0x0001FD54 File Offset: 0x0001DF54
	// (set) Token: 0x06000722 RID: 1826 RVA: 0x0001FD64 File Offset: 0x0001DF64
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

	// Token: 0x170001AE RID: 430
	// (get) Token: 0x06000723 RID: 1827 RVA: 0x0001FD94 File Offset: 0x0001DF94
	// (set) Token: 0x06000724 RID: 1828 RVA: 0x0001FDAC File Offset: 0x0001DFAC
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

	// Token: 0x170001AF RID: 431
	// (get) Token: 0x06000725 RID: 1829 RVA: 0x0001FDE0 File Offset: 0x0001DFE0
	// (set) Token: 0x06000726 RID: 1830 RVA: 0x0001FDF0 File Offset: 0x0001DFF0
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

	// Token: 0x170001B0 RID: 432
	// (get) Token: 0x06000727 RID: 1831 RVA: 0x0001FE20 File Offset: 0x0001E020
	// (set) Token: 0x06000728 RID: 1832 RVA: 0x0001FE30 File Offset: 0x0001E030
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

	// Token: 0x170001B1 RID: 433
	// (get) Token: 0x06000729 RID: 1833 RVA: 0x0001FE60 File Offset: 0x0001E060
	// (set) Token: 0x0600072A RID: 1834 RVA: 0x0001FE74 File Offset: 0x0001E074
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

	// Token: 0x170001B2 RID: 434
	// (get) Token: 0x0600072B RID: 1835 RVA: 0x0001FEA8 File Offset: 0x0001E0A8
	// (set) Token: 0x0600072C RID: 1836 RVA: 0x0001FEBC File Offset: 0x0001E0BC
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

	// Token: 0x170001B3 RID: 435
	// (get) Token: 0x0600072D RID: 1837 RVA: 0x0001FEF8 File Offset: 0x0001E0F8
	// (set) Token: 0x0600072E RID: 1838 RVA: 0x0001FF10 File Offset: 0x0001E110
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

	// Token: 0x170001B4 RID: 436
	// (get) Token: 0x0600072F RID: 1839 RVA: 0x0001FF44 File Offset: 0x0001E144
	// (set) Token: 0x06000730 RID: 1840 RVA: 0x0001FF54 File Offset: 0x0001E154
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

	// Token: 0x170001B5 RID: 437
	// (get) Token: 0x06000731 RID: 1841 RVA: 0x0001FF90 File Offset: 0x0001E190
	// (set) Token: 0x06000732 RID: 1842 RVA: 0x0001FFA0 File Offset: 0x0001E1A0
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

	// Token: 0x170001B6 RID: 438
	// (get) Token: 0x06000733 RID: 1843 RVA: 0x0001FFDC File Offset: 0x0001E1DC
	// (set) Token: 0x06000734 RID: 1844 RVA: 0x0001FFEC File Offset: 0x0001E1EC
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

	// Token: 0x170001B7 RID: 439
	// (get) Token: 0x06000735 RID: 1845 RVA: 0x00020028 File Offset: 0x0001E228
	// (set) Token: 0x06000736 RID: 1846 RVA: 0x00020040 File Offset: 0x0001E240
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

	// Token: 0x170001B8 RID: 440
	// (get) Token: 0x06000737 RID: 1847 RVA: 0x00020074 File Offset: 0x0001E274
	// (set) Token: 0x06000738 RID: 1848 RVA: 0x0002008C File Offset: 0x0001E28C
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

	// Token: 0x170001B9 RID: 441
	// (get) Token: 0x06000739 RID: 1849 RVA: 0x000200C0 File Offset: 0x0001E2C0
	// (set) Token: 0x0600073A RID: 1850 RVA: 0x000200D8 File Offset: 0x0001E2D8
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

	// Token: 0x170001BA RID: 442
	// (get) Token: 0x0600073B RID: 1851 RVA: 0x0002010C File Offset: 0x0001E30C
	// (set) Token: 0x0600073C RID: 1852 RVA: 0x00020124 File Offset: 0x0001E324
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

	// Token: 0x170001BB RID: 443
	// (get) Token: 0x0600073D RID: 1853 RVA: 0x00020158 File Offset: 0x0001E358
	// (set) Token: 0x0600073E RID: 1854 RVA: 0x0002017C File Offset: 0x0001E37C
	public CharacterStateFlags off
	{
		get
		{
			CharacterStateFlags result;
			result.flags = (~this.flags & ushort.MaxValue);
			return result;
		}
		set
		{
			this.flags = (~value.flags & ushort.MaxValue);
		}
	}

	// Token: 0x0600073F RID: 1855 RVA: 0x00020194 File Offset: 0x0001E394
	public override bool Equals(object obj)
	{
		return obj is CharacterStateFlags && ((CharacterStateFlags)obj).flags == this.flags;
	}

	// Token: 0x06000740 RID: 1856 RVA: 0x000201C8 File Offset: 0x0001E3C8
	public bool Equals(CharacterStateFlags other)
	{
		return this.flags == other.flags;
	}

	// Token: 0x06000741 RID: 1857 RVA: 0x000201DC File Offset: 0x0001E3DC
	public override int GetHashCode()
	{
		return this.flags.GetHashCode();
	}

	// Token: 0x06000742 RID: 1858 RVA: 0x000201EC File Offset: 0x0001E3EC
	public override string ToString()
	{
		return this.flags.ToString();
	}

	// Token: 0x06000743 RID: 1859 RVA: 0x000201FC File Offset: 0x0001E3FC
	public string ToString(string format, IFormatProvider formatProvider)
	{
		return this.flags.ToString(format, formatProvider);
	}

	// Token: 0x06000744 RID: 1860 RVA: 0x0002020C File Offset: 0x0001E40C
	public string ToString(string format)
	{
		return this.flags.ToString(format, null);
	}

	// Token: 0x06000745 RID: 1861 RVA: 0x0002021C File Offset: 0x0001E41C
	public static CharacterStateFlags operator &(CharacterStateFlags lhs, CharacterStateFlags rhs)
	{
		lhs.flags &= rhs.flags;
		return lhs;
	}

	// Token: 0x06000746 RID: 1862 RVA: 0x00020238 File Offset: 0x0001E438
	public static CharacterStateFlags operator |(CharacterStateFlags lhs, CharacterStateFlags rhs)
	{
		lhs.flags |= rhs.flags;
		return lhs;
	}

	// Token: 0x06000747 RID: 1863 RVA: 0x00020254 File Offset: 0x0001E454
	public static CharacterStateFlags operator ^(CharacterStateFlags lhs, CharacterStateFlags rhs)
	{
		lhs.flags ^= rhs.flags;
		return lhs;
	}

	// Token: 0x06000748 RID: 1864 RVA: 0x00020270 File Offset: 0x0001E470
	public static CharacterStateFlags operator &(CharacterStateFlags lhs, ushort rhs)
	{
		lhs.flags &= rhs;
		return lhs;
	}

	// Token: 0x06000749 RID: 1865 RVA: 0x00020284 File Offset: 0x0001E484
	public static CharacterStateFlags operator ^(CharacterStateFlags lhs, ushort rhs)
	{
		lhs.flags |= rhs;
		return lhs;
	}

	// Token: 0x0600074A RID: 1866 RVA: 0x00020298 File Offset: 0x0001E498
	public static CharacterStateFlags operator |(CharacterStateFlags lhs, ushort rhs)
	{
		lhs.flags ^= rhs;
		return lhs;
	}

	// Token: 0x0600074B RID: 1867 RVA: 0x000202AC File Offset: 0x0001E4AC
	public static CharacterStateFlags operator &(CharacterStateFlags lhs, int rhs)
	{
		lhs.flags &= (ushort)(rhs & 65535);
		return lhs;
	}

	// Token: 0x0600074C RID: 1868 RVA: 0x000202C8 File Offset: 0x0001E4C8
	public static CharacterStateFlags operator ^(CharacterStateFlags lhs, int rhs)
	{
		lhs.flags |= (ushort)(rhs & 65535);
		return lhs;
	}

	// Token: 0x0600074D RID: 1869 RVA: 0x000202E4 File Offset: 0x0001E4E4
	public static CharacterStateFlags operator |(CharacterStateFlags lhs, int rhs)
	{
		lhs.flags ^= (ushort)(rhs & 65535);
		return lhs;
	}

	// Token: 0x0600074E RID: 1870 RVA: 0x00020300 File Offset: 0x0001E500
	public static CharacterStateFlags operator &(CharacterStateFlags lhs, long rhs)
	{
		lhs.flags &= (ushort)(rhs & 65535L);
		return lhs;
	}

	// Token: 0x0600074F RID: 1871 RVA: 0x0002031C File Offset: 0x0001E51C
	public static CharacterStateFlags operator ^(CharacterStateFlags lhs, long rhs)
	{
		lhs.flags |= (ushort)(rhs & 65535L);
		return lhs;
	}

	// Token: 0x06000750 RID: 1872 RVA: 0x00020338 File Offset: 0x0001E538
	public static CharacterStateFlags operator |(CharacterStateFlags lhs, long rhs)
	{
		lhs.flags ^= (ushort)(rhs & 65535L);
		return lhs;
	}

	// Token: 0x06000751 RID: 1873 RVA: 0x00020354 File Offset: 0x0001E554
	public static CharacterStateFlags operator &(CharacterStateFlags lhs, uint rhs)
	{
		lhs.flags &= (ushort)(rhs & 65535u);
		return lhs;
	}

	// Token: 0x06000752 RID: 1874 RVA: 0x00020370 File Offset: 0x0001E570
	public static CharacterStateFlags operator ^(CharacterStateFlags lhs, uint rhs)
	{
		lhs.flags |= (ushort)(rhs & 65535u);
		return lhs;
	}

	// Token: 0x06000753 RID: 1875 RVA: 0x0002038C File Offset: 0x0001E58C
	public static CharacterStateFlags operator |(CharacterStateFlags lhs, uint rhs)
	{
		lhs.flags ^= (ushort)(rhs & 65535u);
		return lhs;
	}

	// Token: 0x06000754 RID: 1876 RVA: 0x000203A8 File Offset: 0x0001E5A8
	public static CharacterStateFlags operator &(CharacterStateFlags lhs, ulong rhs)
	{
		lhs.flags &= (ushort)(rhs & 65535UL);
		return lhs;
	}

	// Token: 0x06000755 RID: 1877 RVA: 0x000203C4 File Offset: 0x0001E5C4
	public static CharacterStateFlags operator ^(CharacterStateFlags lhs, ulong rhs)
	{
		lhs.flags |= (ushort)(rhs & 65535UL);
		return lhs;
	}

	// Token: 0x06000756 RID: 1878 RVA: 0x000203E0 File Offset: 0x0001E5E0
	public static CharacterStateFlags operator |(CharacterStateFlags lhs, ulong rhs)
	{
		lhs.flags ^= (ushort)(rhs & 65535UL);
		return lhs;
	}

	// Token: 0x06000757 RID: 1879 RVA: 0x000203FC File Offset: 0x0001E5FC
	public static int operator &(int lhs, CharacterStateFlags rhs)
	{
		return lhs & (int)rhs.flags;
	}

	// Token: 0x06000758 RID: 1880 RVA: 0x00020408 File Offset: 0x0001E608
	public static int operator |(int lhs, CharacterStateFlags rhs)
	{
		return lhs | (int)rhs.flags;
	}

	// Token: 0x06000759 RID: 1881 RVA: 0x00020414 File Offset: 0x0001E614
	public static int operator ^(int lhs, CharacterStateFlags rhs)
	{
		return lhs ^ (int)rhs.flags;
	}

	// Token: 0x0600075A RID: 1882 RVA: 0x00020420 File Offset: 0x0001E620
	public static uint operator &(uint lhs, CharacterStateFlags rhs)
	{
		return lhs & (uint)rhs.flags;
	}

	// Token: 0x0600075B RID: 1883 RVA: 0x0002042C File Offset: 0x0001E62C
	public static uint operator |(uint lhs, CharacterStateFlags rhs)
	{
		return lhs | (uint)rhs.flags;
	}

	// Token: 0x0600075C RID: 1884 RVA: 0x00020438 File Offset: 0x0001E638
	public static uint operator ^(uint lhs, CharacterStateFlags rhs)
	{
		return lhs ^ (uint)rhs.flags;
	}

	// Token: 0x0600075D RID: 1885 RVA: 0x00020444 File Offset: 0x0001E644
	public static long operator &(long lhs, CharacterStateFlags rhs)
	{
		return lhs & (long)rhs.flags;
	}

	// Token: 0x0600075E RID: 1886 RVA: 0x00020450 File Offset: 0x0001E650
	public static long operator |(long lhs, CharacterStateFlags rhs)
	{
		return lhs | (long)rhs.flags;
	}

	// Token: 0x0600075F RID: 1887 RVA: 0x0002045C File Offset: 0x0001E65C
	public static long operator ^(long lhs, CharacterStateFlags rhs)
	{
		return lhs ^ (long)rhs.flags;
	}

	// Token: 0x06000760 RID: 1888 RVA: 0x00020468 File Offset: 0x0001E668
	public static ulong operator &(ulong lhs, CharacterStateFlags rhs)
	{
		return lhs & (ulong)rhs.flags;
	}

	// Token: 0x06000761 RID: 1889 RVA: 0x00020474 File Offset: 0x0001E674
	public static ulong operator |(ulong lhs, CharacterStateFlags rhs)
	{
		return lhs | (ulong)rhs.flags;
	}

	// Token: 0x06000762 RID: 1890 RVA: 0x00020480 File Offset: 0x0001E680
	public static ulong operator ^(ulong lhs, CharacterStateFlags rhs)
	{
		return lhs ^ (ulong)rhs.flags;
	}

	// Token: 0x06000763 RID: 1891 RVA: 0x0002048C File Offset: 0x0001E68C
	public static int operator &(byte lhs, CharacterStateFlags rhs)
	{
		return (int)((ushort)lhs & rhs.flags);
	}

	// Token: 0x06000764 RID: 1892 RVA: 0x00020498 File Offset: 0x0001E698
	public static int operator |(byte lhs, CharacterStateFlags rhs)
	{
		return (int)((ushort)lhs | rhs.flags);
	}

	// Token: 0x06000765 RID: 1893 RVA: 0x000204A4 File Offset: 0x0001E6A4
	public static int operator ^(byte lhs, CharacterStateFlags rhs)
	{
		return (int)((ushort)lhs ^ rhs.flags);
	}

	// Token: 0x06000766 RID: 1894 RVA: 0x000204B0 File Offset: 0x0001E6B0
	public static int operator &(sbyte lhs, CharacterStateFlags rhs)
	{
		return (int)lhs & (int)rhs.flags;
	}

	// Token: 0x06000767 RID: 1895 RVA: 0x000204BC File Offset: 0x0001E6BC
	public static int operator |(sbyte lhs, CharacterStateFlags rhs)
	{
		return (int)lhs | (int)rhs.flags;
	}

	// Token: 0x06000768 RID: 1896 RVA: 0x000204C8 File Offset: 0x0001E6C8
	public static int operator ^(sbyte lhs, CharacterStateFlags rhs)
	{
		return (int)lhs ^ (int)rhs.flags;
	}

	// Token: 0x06000769 RID: 1897 RVA: 0x000204D4 File Offset: 0x0001E6D4
	public static int operator &(short lhs, CharacterStateFlags rhs)
	{
		return (int)(lhs & (short)rhs.flags);
	}

	// Token: 0x0600076A RID: 1898 RVA: 0x000204E0 File Offset: 0x0001E6E0
	public static int operator |(short lhs, CharacterStateFlags rhs)
	{
		return (int)(lhs | (short)rhs.flags);
	}

	// Token: 0x0600076B RID: 1899 RVA: 0x000204EC File Offset: 0x0001E6EC
	public static int operator ^(short lhs, CharacterStateFlags rhs)
	{
		return (int)(lhs ^ (short)rhs.flags);
	}

	// Token: 0x0600076C RID: 1900 RVA: 0x000204F8 File Offset: 0x0001E6F8
	public static int operator &(ushort lhs, CharacterStateFlags rhs)
	{
		return (int)(lhs & rhs.flags);
	}

	// Token: 0x0600076D RID: 1901 RVA: 0x00020504 File Offset: 0x0001E704
	public static int operator |(ushort lhs, CharacterStateFlags rhs)
	{
		return (int)(lhs | rhs.flags);
	}

	// Token: 0x0600076E RID: 1902 RVA: 0x00020510 File Offset: 0x0001E710
	public static int operator ^(ushort lhs, CharacterStateFlags rhs)
	{
		return (int)(lhs ^ rhs.flags);
	}

	// Token: 0x0600076F RID: 1903 RVA: 0x0002051C File Offset: 0x0001E71C
	public static bool operator ==(CharacterStateFlags lhs, CharacterStateFlags rhs)
	{
		return lhs.flags == rhs.flags;
	}

	// Token: 0x06000770 RID: 1904 RVA: 0x00020530 File Offset: 0x0001E730
	public static bool operator ==(CharacterStateFlags lhs, byte rhs)
	{
		return lhs.flags == (ushort)rhs;
	}

	// Token: 0x06000771 RID: 1905 RVA: 0x0002053C File Offset: 0x0001E73C
	public static bool operator ==(CharacterStateFlags lhs, sbyte rhs)
	{
		return (int)lhs.flags == (int)rhs;
	}

	// Token: 0x06000772 RID: 1906 RVA: 0x0002054C File Offset: 0x0001E74C
	public static bool operator ==(CharacterStateFlags lhs, short rhs)
	{
		return lhs.flags == (ushort)rhs;
	}

	// Token: 0x06000773 RID: 1907 RVA: 0x00020558 File Offset: 0x0001E758
	public static bool operator ==(CharacterStateFlags lhs, ushort rhs)
	{
		return lhs.flags == rhs;
	}

	// Token: 0x06000774 RID: 1908 RVA: 0x00020564 File Offset: 0x0001E764
	public static bool operator ==(CharacterStateFlags lhs, int rhs)
	{
		return (int)lhs.flags == rhs;
	}

	// Token: 0x06000775 RID: 1909 RVA: 0x00020570 File Offset: 0x0001E770
	public static bool operator ==(CharacterStateFlags lhs, uint rhs)
	{
		return (uint)lhs.flags == rhs;
	}

	// Token: 0x06000776 RID: 1910 RVA: 0x0002057C File Offset: 0x0001E77C
	public static bool operator ==(CharacterStateFlags lhs, long rhs)
	{
		return (long)lhs.flags == rhs;
	}

	// Token: 0x06000777 RID: 1911 RVA: 0x0002058C File Offset: 0x0001E78C
	public static bool operator ==(CharacterStateFlags lhs, ulong rhs)
	{
		return (ulong)lhs.flags == rhs;
	}

	// Token: 0x06000778 RID: 1912 RVA: 0x0002059C File Offset: 0x0001E79C
	public static bool operator ==(CharacterStateFlags lhs, bool rhs)
	{
		return lhs.flags != 0 == rhs;
	}

	// Token: 0x06000779 RID: 1913 RVA: 0x000205B0 File Offset: 0x0001E7B0
	public static bool operator !=(CharacterStateFlags lhs, CharacterStateFlags rhs)
	{
		return lhs.flags != rhs.flags;
	}

	// Token: 0x0600077A RID: 1914 RVA: 0x000205C8 File Offset: 0x0001E7C8
	public static bool operator !=(CharacterStateFlags lhs, byte rhs)
	{
		return lhs.flags != (ushort)rhs;
	}

	// Token: 0x0600077B RID: 1915 RVA: 0x000205D8 File Offset: 0x0001E7D8
	public static bool operator !=(CharacterStateFlags lhs, sbyte rhs)
	{
		return (int)lhs.flags != (int)rhs;
	}

	// Token: 0x0600077C RID: 1916 RVA: 0x000205E8 File Offset: 0x0001E7E8
	public static bool operator !=(CharacterStateFlags lhs, short rhs)
	{
		return lhs.flags != (ushort)rhs;
	}

	// Token: 0x0600077D RID: 1917 RVA: 0x000205F8 File Offset: 0x0001E7F8
	public static bool operator !=(CharacterStateFlags lhs, ushort rhs)
	{
		return lhs.flags != rhs;
	}

	// Token: 0x0600077E RID: 1918 RVA: 0x00020608 File Offset: 0x0001E808
	public static bool operator !=(CharacterStateFlags lhs, int rhs)
	{
		return (int)lhs.flags != rhs;
	}

	// Token: 0x0600077F RID: 1919 RVA: 0x00020618 File Offset: 0x0001E818
	public static bool operator !=(CharacterStateFlags lhs, uint rhs)
	{
		return (uint)lhs.flags != rhs;
	}

	// Token: 0x06000780 RID: 1920 RVA: 0x00020628 File Offset: 0x0001E828
	public static bool operator !=(CharacterStateFlags lhs, long rhs)
	{
		return (long)lhs.flags != rhs;
	}

	// Token: 0x06000781 RID: 1921 RVA: 0x00020638 File Offset: 0x0001E838
	public static bool operator !=(CharacterStateFlags lhs, ulong rhs)
	{
		return (ulong)lhs.flags != rhs;
	}

	// Token: 0x06000782 RID: 1922 RVA: 0x00020648 File Offset: 0x0001E848
	public static bool operator !=(CharacterStateFlags lhs, bool rhs)
	{
		return lhs.flags == 0 == rhs;
	}

	// Token: 0x06000783 RID: 1923 RVA: 0x00020658 File Offset: 0x0001E858
	public static CharacterStateFlags operator >>(CharacterStateFlags lhs, int shift)
	{
		lhs.flags = (ushort)(lhs.flags >> shift);
		return lhs;
	}

	// Token: 0x06000784 RID: 1924 RVA: 0x00020670 File Offset: 0x0001E870
	public static CharacterStateFlags operator <<(CharacterStateFlags lhs, int shift)
	{
		lhs.flags = (ushort)(lhs.flags >> shift);
		return lhs;
	}

	// Token: 0x06000785 RID: 1925 RVA: 0x00020688 File Offset: 0x0001E888
	public static CharacterStateFlags operator ~(CharacterStateFlags lhs)
	{
		lhs.flags = (~lhs.flags & ushort.MaxValue);
		return lhs;
	}

	// Token: 0x06000786 RID: 1926 RVA: 0x000206A4 File Offset: 0x0001E8A4
	public static bool operator true(CharacterStateFlags lhs)
	{
		return lhs.flags != 0;
	}

	// Token: 0x06000787 RID: 1927 RVA: 0x000206B4 File Offset: 0x0001E8B4
	public static bool operator false(CharacterStateFlags lhs)
	{
		return lhs.flags == 0;
	}

	// Token: 0x06000788 RID: 1928 RVA: 0x000206C0 File Offset: 0x0001E8C0
	public static explicit operator bool(CharacterStateFlags lhs)
	{
		return lhs.flags != 0;
	}

	// Token: 0x06000789 RID: 1929 RVA: 0x000206D0 File Offset: 0x0001E8D0
	public static bool operator !(CharacterStateFlags lhs)
	{
		return lhs.flags == 0;
	}

	// Token: 0x0600078A RID: 1930 RVA: 0x000206DC File Offset: 0x0001E8DC
	public static implicit operator ushort(CharacterStateFlags lhs)
	{
		return lhs.flags;
	}

	// Token: 0x0600078B RID: 1931 RVA: 0x000206E8 File Offset: 0x0001E8E8
	public static implicit operator CharacterStateFlags(ushort lhs)
	{
		CharacterStateFlags result;
		result.flags = lhs;
		return result;
	}

	// Token: 0x0600078C RID: 1932 RVA: 0x00020700 File Offset: 0x0001E900
	public static implicit operator CharacterStateFlags(int lhs)
	{
		CharacterStateFlags result;
		result.flags = (ushort)(lhs & 65535);
		return result;
	}

	// Token: 0x0600078D RID: 1933 RVA: 0x00020720 File Offset: 0x0001E920
	public static implicit operator CharacterStateFlags(long lhs)
	{
		CharacterStateFlags result;
		result.flags = (ushort)(lhs & 65535L);
		return result;
	}

	// Token: 0x0600078E RID: 1934 RVA: 0x00020740 File Offset: 0x0001E940
	public static implicit operator CharacterStateFlags(uint lhs)
	{
		CharacterStateFlags result;
		result.flags = (ushort)(lhs & 65535u);
		return result;
	}

	// Token: 0x0600078F RID: 1935 RVA: 0x00020760 File Offset: 0x0001E960
	public static implicit operator CharacterStateFlags(ulong lhs)
	{
		CharacterStateFlags result;
		result.flags = (ushort)(lhs & 65535UL);
		return result;
	}

	// Token: 0x04000543 RID: 1347
	public const ushort kCrouch = 1;

	// Token: 0x04000544 RID: 1348
	public const ushort kSprint = 2;

	// Token: 0x04000545 RID: 1349
	public const ushort kAim = 4;

	// Token: 0x04000546 RID: 1350
	public const ushort kAttack = 8;

	// Token: 0x04000547 RID: 1351
	public const ushort kAirborne = 16;

	// Token: 0x04000548 RID: 1352
	public const ushort kSlipping = 32;

	// Token: 0x04000549 RID: 1353
	public const ushort kMovement = 64;

	// Token: 0x0400054A RID: 1354
	public const ushort kLostFocus = 128;

	// Token: 0x0400054B RID: 1355
	public const ushort kAttack2 = 256;

	// Token: 0x0400054C RID: 1356
	public const ushort kBleeding = 512;

	// Token: 0x0400054D RID: 1357
	public const ushort kCrouchBlocked = 1024;

	// Token: 0x0400054E RID: 1358
	public const ushort kLamp = 2048;

	// Token: 0x0400054F RID: 1359
	public const ushort kLaser = 4096;

	// Token: 0x04000550 RID: 1360
	public const ushort kNone = 0;

	// Token: 0x04000551 RID: 1361
	public const ushort kMask = 8191;

	// Token: 0x04000552 RID: 1362
	private const ushort kAllMask = 65535;

	// Token: 0x04000553 RID: 1363
	private const ushort kNotCrouch = 65534;

	// Token: 0x04000554 RID: 1364
	private const ushort kNotSprint = 65533;

	// Token: 0x04000555 RID: 1365
	private const ushort kNotAim = 65531;

	// Token: 0x04000556 RID: 1366
	private const ushort kNotAttack = 65527;

	// Token: 0x04000557 RID: 1367
	private const ushort kNotAirborne = 65519;

	// Token: 0x04000558 RID: 1368
	private const ushort kNotSlipping = 65503;

	// Token: 0x04000559 RID: 1369
	private const ushort kNotMovement = 65471;

	// Token: 0x0400055A RID: 1370
	private const ushort kNotLostFocus = 65407;

	// Token: 0x0400055B RID: 1371
	private const ushort kNotAttack2 = 65279;

	// Token: 0x0400055C RID: 1372
	private const ushort kNotBleeding = 65023;

	// Token: 0x0400055D RID: 1373
	private const ushort kNotCrouchBlocked = 64511;

	// Token: 0x0400055E RID: 1374
	private const ushort kNotLamp = 63487;

	// Token: 0x0400055F RID: 1375
	private const ushort kNotLaser = 61439;

	// Token: 0x04000560 RID: 1376
	public ushort flags;
}
