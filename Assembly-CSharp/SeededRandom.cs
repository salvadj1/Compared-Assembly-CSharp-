using System;

// Token: 0x02000736 RID: 1846
public class SeededRandom
{
	// Token: 0x06003D40 RID: 15680 RVA: 0x000DB2BC File Offset: 0x000D94BC
	public SeededRandom() : this(Environment.TickCount)
	{
	}

	// Token: 0x06003D41 RID: 15681 RVA: 0x000DB2CC File Offset: 0x000D94CC
	public SeededRandom(int seed)
	{
		this.byteBuffer = new byte[16];
		this.Seed = seed;
		this.rand = new Random(seed);
	}

	// Token: 0x06003D42 RID: 15682 RVA: 0x000DB300 File Offset: 0x000D9500
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

	// Token: 0x17000BA9 RID: 2985
	// (get) Token: 0x06003D43 RID: 15683 RVA: 0x000DB358 File Offset: 0x000D9558
	// (set) Token: 0x06003D44 RID: 15684 RVA: 0x000DB3B8 File Offset: 0x000D95B8
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

	// Token: 0x06003D45 RID: 15685 RVA: 0x000DB450 File Offset: 0x000D9650
	private void Fill()
	{
		if ((this.allocCount += 1u) == 33554432u)
		{
			this.rand = new Random(this.Seed);
			this.allocCount = 1u;
		}
		this.rand.NextBytes(this.byteBuffer);
	}

	// Token: 0x06003D46 RID: 15686 RVA: 0x000DB4A4 File Offset: 0x000D96A4
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

	// Token: 0x06003D47 RID: 15687 RVA: 0x000DB554 File Offset: 0x000D9754
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

	// Token: 0x06003D48 RID: 15688 RVA: 0x000DB5D8 File Offset: 0x000D97D8
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

	// Token: 0x06003D49 RID: 15689 RVA: 0x000DB620 File Offset: 0x000D9820
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

	// Token: 0x06003D4A RID: 15690 RVA: 0x000DB668 File Offset: 0x000D9868
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

	// Token: 0x06003D4B RID: 15691 RVA: 0x000DB6B0 File Offset: 0x000D98B0
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

	// Token: 0x06003D4C RID: 15692 RVA: 0x000DB720 File Offset: 0x000D9920
	private static double LT1(double v)
	{
		return (v <= 9.8813129168249309E-324) ? v : (v - double.Epsilon);
	}

	// Token: 0x06003D4D RID: 15693 RVA: 0x000DB750 File Offset: 0x000D9950
	private double RandomFractionBitDepthLT1(int bitDepth, int bitMask)
	{
		return global::SeededRandom.LT1(this.RandomFractionBitDepth(bitDepth, bitMask));
	}

	// Token: 0x06003D4E RID: 15694 RVA: 0x000DB760 File Offset: 0x000D9960
	public double RandomFractionBitDepthLT1(int bitDepth)
	{
		return global::SeededRandom.LT1(this.RandomFractionBitDepth(bitDepth));
	}

	// Token: 0x06003D4F RID: 15695 RVA: 0x000DB770 File Offset: 0x000D9970
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

	// Token: 0x06003D50 RID: 15696 RVA: 0x000DB7FC File Offset: 0x000D99FC
	public double Range(double minInclusive, double maxInclusive, int bitDepth)
	{
		return (minInclusive != maxInclusive) ? (this.RandomFractionBitDepth(bitDepth) * (maxInclusive - minInclusive) + minInclusive) : minInclusive;
	}

	// Token: 0x06003D51 RID: 15697 RVA: 0x000DB818 File Offset: 0x000D9A18
	public double Range(double minInclusive, double maxInclusive)
	{
		return this.Range(minInclusive, maxInclusive, 16);
	}

	// Token: 0x06003D52 RID: 15698 RVA: 0x000DB824 File Offset: 0x000D9A24
	public float Range(float minInclusive, float maxInclusive, int bitDepth)
	{
		return (float)this.Range((double)minInclusive, (double)maxInclusive, bitDepth);
	}

	// Token: 0x06003D53 RID: 15699 RVA: 0x000DB834 File Offset: 0x000D9A34
	public float Range(float minInclusive, float maxInclusive)
	{
		return (float)this.Range((double)minInclusive, (double)maxInclusive);
	}

	// Token: 0x06003D54 RID: 15700 RVA: 0x000DB844 File Offset: 0x000D9A44
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

	// Token: 0x06003D55 RID: 15701 RVA: 0x000DB8D0 File Offset: 0x000D9AD0
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

	// Token: 0x06003D56 RID: 15702 RVA: 0x000DB8FC File Offset: 0x000D9AFC
	public T Pick<T>(T[] array)
	{
		if (array == null)
		{
			throw new ArgumentNullException("array");
		}
		return array[this.RandomIndex(array.Length)];
	}

	// Token: 0x06003D57 RID: 15703 RVA: 0x000DB92C File Offset: 0x000D9B2C
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

	// Token: 0x04001F4C RID: 8012
	private const int kBufferSize = 16;

	// Token: 0x04001F4D RID: 8013
	private const int kBufferBitSize = 128;

	// Token: 0x04001F4E RID: 8014
	private const int kBitsInByte = 8;

	// Token: 0x04001F4F RID: 8015
	private const byte kMaskBitPos = 7;

	// Token: 0x04001F50 RID: 8016
	private const int kShiftBitPos = 3;

	// Token: 0x04001F51 RID: 8017
	private const byte kMaskBytePos = 15;

	// Token: 0x04001F52 RID: 8018
	private const int kShiftBytePos = 4;

	// Token: 0x04001F53 RID: 8019
	private const int kMaxAllocPos = 33554431;

	// Token: 0x04001F54 RID: 8020
	private const int kMaxAllocCount = 33554432;

	// Token: 0x04001F55 RID: 8021
	private Random rand;

	// Token: 0x04001F56 RID: 8022
	private readonly byte[] byteBuffer;

	// Token: 0x04001F57 RID: 8023
	public readonly int Seed;

	// Token: 0x04001F58 RID: 8024
	private uint allocCount;

	// Token: 0x04001F59 RID: 8025
	private byte bytePos;

	// Token: 0x04001F5A RID: 8026
	private byte bitPos;
}
