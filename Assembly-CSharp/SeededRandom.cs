using System;

// Token: 0x02000672 RID: 1650
public class SeededRandom
{
	// Token: 0x0600394C RID: 14668 RVA: 0x000D28DC File Offset: 0x000D0ADC
	public SeededRandom() : this(Environment.TickCount)
	{
	}

	// Token: 0x0600394D RID: 14669 RVA: 0x000D28EC File Offset: 0x000D0AEC
	public SeededRandom(int seed)
	{
		this.byteBuffer = new byte[16];
		this.Seed = seed;
		this.rand = new Random(seed);
	}

	// Token: 0x0600394E RID: 14670 RVA: 0x000D2920 File Offset: 0x000D0B20
	public int RandomBits(int bitCount)
	{
		if (bitCount < 0 || bitCount > 32)
		{
			throw new ArgumentOutOfRangeException("bitCount");
		}
		int num = 0;
		int num2 = 0;
		while (bitCount-- > 0)
		{
			if (this.Boolean())
			{
				num |= 1 << num2;
			}
			num2++;
		}
		return num;
	}

	// Token: 0x17000B27 RID: 2855
	// (get) Token: 0x0600394F RID: 14671 RVA: 0x000D2978 File Offset: 0x000D0B78
	// (set) Token: 0x06003950 RID: 14672 RVA: 0x000D29D8 File Offset: 0x000D0BD8
	public uint PositionData
	{
		get
		{
			return ((((this.bytePos <= 0 && this.bitPos <= 0) || this.allocCount <= 0u) ? this.allocCount : (this.allocCount - 1u)) << 4 | (uint)(this.bytePos & 15)) << 7 | (uint)(this.bitPos & 7);
		}
		set
		{
			byte b = (byte)(value & 7u);
			byte b2 = (byte)((value >>= 3) & 15u);
			uint num;
			value = (num = value >> 4);
			if (b > 0 || b2 > 0)
			{
				num += 1u;
			}
			if (num < this.allocCount)
			{
				this.allocCount = 0u;
				this.rand = new Random(this.Seed);
			}
			while (this.allocCount < num)
			{
				this.allocCount += 1u;
				this.rand.NextBytes(this.byteBuffer);
			}
			this.bitPos = b;
			this.bytePos = b2;
		}
	}

	// Token: 0x06003951 RID: 14673 RVA: 0x000D2A70 File Offset: 0x000D0C70
	private void Fill()
	{
		if ((this.allocCount += 1u) == 33554432u)
		{
			this.rand = new Random(this.Seed);
			this.allocCount = 1u;
		}
		this.rand.NextBytes(this.byteBuffer);
	}

	// Token: 0x06003952 RID: 14674 RVA: 0x000D2AC4 File Offset: 0x000D0CC4
	public bool Boolean()
	{
		if (this.bytePos == 0 && this.bitPos == 0)
		{
			this.Fill();
			this.bitPos += 1;
			return (this.byteBuffer[0] & 1) == 1;
		}
		bool result = ((int)this.byteBuffer[(int)this.bytePos] & 1 << (int)this.bitPos) == 1 << (int)this.bitPos;
		if ((this.bitPos += 1) == 8)
		{
			this.bitPos = 0;
			if ((this.bytePos += 1) == 16)
			{
				this.bytePos = 0;
			}
		}
		return result;
	}

	// Token: 0x06003953 RID: 14675 RVA: 0x000D2B74 File Offset: 0x000D0D74
	private double RandomFractionBitDepth(int bitDepth, int bitMask)
	{
		if (bitDepth < 1 || bitDepth > 32)
		{
			throw new ArgumentOutOfRangeException("bitDepth", "!( bitDepth > 0 && bitDepth <= 32 )");
		}
		if (bitDepth == 32)
		{
			return this.RandomFraction32();
		}
		if (bitMask <= 0)
		{
			throw new ArgumentException("bitMask", "!(bitMask > 0)");
		}
		int num = 0;
		for (int i = 0; i < bitDepth; i++)
		{
			if (this.Boolean())
			{
				num |= 1 << i;
			}
		}
		return (double)num / (double)bitMask;
	}

	// Token: 0x06003954 RID: 14676 RVA: 0x000D2BF8 File Offset: 0x000D0DF8
	public double RandomFraction32()
	{
		uint num = 0u;
		for (int i = 0; i < 32; i++)
		{
			if (this.Boolean())
			{
				num |= 1u << i;
			}
		}
		return num / 4294967295.0;
	}

	// Token: 0x06003955 RID: 14677 RVA: 0x000D2C40 File Offset: 0x000D0E40
	public double RandomFraction16()
	{
		uint num = 0u;
		for (int i = 0; i < 16; i++)
		{
			if (this.Boolean())
			{
				num |= 1u << i;
			}
		}
		return num / 65535.0;
	}

	// Token: 0x06003956 RID: 14678 RVA: 0x000D2C88 File Offset: 0x000D0E88
	public double RandomFraction8()
	{
		uint num = 0u;
		for (int i = 0; i < 8; i++)
		{
			if (this.Boolean())
			{
				num |= 1u << i;
			}
		}
		return num / 255.0;
	}

	// Token: 0x06003957 RID: 14679 RVA: 0x000D2CD0 File Offset: 0x000D0ED0
	public double RandomFractionBitDepth(int bitDepth)
	{
		if (bitDepth < 1 || bitDepth > 32)
		{
			throw new ArgumentOutOfRangeException("bitDepth", "!( bitDepth > 0 && bitDepth <= 32 )");
		}
		if (bitDepth == 32)
		{
			return this.RandomFraction32();
		}
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < bitDepth; i++)
		{
			int num3 = 1 << i;
			if (this.Boolean())
			{
				num |= num3;
			}
			num2 |= num3;
		}
		return (double)num / (double)num2;
	}

	// Token: 0x06003958 RID: 14680 RVA: 0x000D2D40 File Offset: 0x000D0F40
	private static double LT1(double v)
	{
		return (v <= 9.8813129168249309E-324) ? v : (v - double.Epsilon);
	}

	// Token: 0x06003959 RID: 14681 RVA: 0x000D2D70 File Offset: 0x000D0F70
	private double RandomFractionBitDepthLT1(int bitDepth, int bitMask)
	{
		return SeededRandom.LT1(this.RandomFractionBitDepth(bitDepth, bitMask));
	}

	// Token: 0x0600395A RID: 14682 RVA: 0x000D2D80 File Offset: 0x000D0F80
	public double RandomFractionBitDepthLT1(int bitDepth)
	{
		return SeededRandom.LT1(this.RandomFractionBitDepth(bitDepth));
	}

	// Token: 0x0600395B RID: 14683 RVA: 0x000D2D90 File Offset: 0x000D0F90
	public int RandomIndex(int length)
	{
		if (length == 0 || (length & -2147483648) == -2147483648)
		{
			throw new ArgumentOutOfRangeException("length", "!(length <= 0)");
		}
		uint num;
		if ((num = (uint)length >> 1) == 0u)
		{
			return 0;
		}
		int num2 = 1;
		byte b = 1;
		if ((num >>= 1) != 0u)
		{
			do
			{
				b += 1;
				num2 = (num2 << 1 | 1);
			}
			while ((num >>= 1) != 0u);
			return (int)Math.Floor(this.RandomFractionBitDepthLT1((int)b, num2) * (double)length);
		}
		return (!this.Boolean()) ? 0 : 1;
	}

	// Token: 0x0600395C RID: 14684 RVA: 0x000D2E1C File Offset: 0x000D101C
	public double Range(double minInclusive, double maxInclusive, int bitDepth)
	{
		return (minInclusive != maxInclusive) ? (this.RandomFractionBitDepth(bitDepth) * (maxInclusive - minInclusive) + minInclusive) : minInclusive;
	}

	// Token: 0x0600395D RID: 14685 RVA: 0x000D2E38 File Offset: 0x000D1038
	public double Range(double minInclusive, double maxInclusive)
	{
		return this.Range(minInclusive, maxInclusive, 16);
	}

	// Token: 0x0600395E RID: 14686 RVA: 0x000D2E44 File Offset: 0x000D1044
	public float Range(float minInclusive, float maxInclusive, int bitDepth)
	{
		return (float)this.Range((double)minInclusive, (double)maxInclusive, bitDepth);
	}

	// Token: 0x0600395F RID: 14687 RVA: 0x000D2E54 File Offset: 0x000D1054
	public float Range(float minInclusive, float maxInclusive)
	{
		return (float)this.Range((double)minInclusive, (double)maxInclusive);
	}

	// Token: 0x06003960 RID: 14688 RVA: 0x000D2E64 File Offset: 0x000D1064
	public int Range(int minInclusive, int maxInclusive)
	{
		if (minInclusive > maxInclusive)
		{
			int num = maxInclusive;
			maxInclusive = minInclusive;
			minInclusive = num;
		}
		else if (maxInclusive == minInclusive)
		{
			return minInclusive;
		}
		ulong num2 = (ulong)((long)maxInclusive - (long)minInclusive);
		if (num2 > 2147483647UL)
		{
			return (int)((long)minInclusive + (long)Math.Round(num2 * this.RandomFraction32()));
		}
		int num3 = 0;
		int num4 = 0;
		uint num5 = (uint)num2;
		while ((num5 >>= 1) != 0u)
		{
			num3++;
			num4 = (num4 << 1 | 1);
		}
		return minInclusive + (int)Math.Round((double)this.RandomBits(num3) / (double)num4 * num2);
	}

	// Token: 0x06003961 RID: 14689 RVA: 0x000D2EF0 File Offset: 0x000D10F0
	public bool Reset()
	{
		if (this.allocCount > 0u)
		{
			this.rand = new Random(this.Seed);
			this.allocCount = 0u;
			return true;
		}
		return false;
	}

	// Token: 0x06003962 RID: 14690 RVA: 0x000D2F1C File Offset: 0x000D111C
	public T Pick<T>(T[] array)
	{
		if (array == null)
		{
			throw new ArgumentNullException("array");
		}
		return array[this.RandomIndex(array.Length)];
	}

	// Token: 0x06003963 RID: 14691 RVA: 0x000D2F4C File Offset: 0x000D114C
	public bool Pick<T>(T[] array, out T value)
	{
		if (array == null || array.Length == 0)
		{
			value = default(T);
			return false;
		}
		value = array[this.RandomIndex(array.Length)];
		return true;
	}

	// Token: 0x04001D54 RID: 7508
	private const int kBufferSize = 16;

	// Token: 0x04001D55 RID: 7509
	private const int kBufferBitSize = 128;

	// Token: 0x04001D56 RID: 7510
	private const int kBitsInByte = 8;

	// Token: 0x04001D57 RID: 7511
	private const byte kMaskBitPos = 7;

	// Token: 0x04001D58 RID: 7512
	private const int kShiftBitPos = 3;

	// Token: 0x04001D59 RID: 7513
	private const byte kMaskBytePos = 15;

	// Token: 0x04001D5A RID: 7514
	private const int kShiftBytePos = 4;

	// Token: 0x04001D5B RID: 7515
	private const int kMaxAllocPos = 33554431;

	// Token: 0x04001D5C RID: 7516
	private const int kMaxAllocCount = 33554432;

	// Token: 0x04001D5D RID: 7517
	private Random rand;

	// Token: 0x04001D5E RID: 7518
	private readonly byte[] byteBuffer;

	// Token: 0x04001D5F RID: 7519
	public readonly int Seed;

	// Token: 0x04001D60 RID: 7520
	private uint allocCount;

	// Token: 0x04001D61 RID: 7521
	private byte bytePos;

	// Token: 0x04001D62 RID: 7522
	private byte bitPos;
}
