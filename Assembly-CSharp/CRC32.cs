using System;
using System.Security.Cryptography;
using System.Text;

// Token: 0x020001D0 RID: 464
public sealed class CRC32 : HashAlgorithm
{
	// Token: 0x06000D3F RID: 3391 RVA: 0x00033F74 File Offset: 0x00032174
	public CRC32()
	{
		this.table = global::CRC32.Default.Table;
		this.seed = uint.MaxValue;
		this.Initialize();
	}

	// Token: 0x06000D40 RID: 3392 RVA: 0x00033F94 File Offset: 0x00032194
	public CRC32(uint polynomial, uint seed)
	{
		this.table = ((polynomial != 3988292384u) ? global::CRC32.ProcessHashTable(polynomial) : global::CRC32.Default.Table);
		this.seed = seed;
		this.Initialize();
	}

	// Token: 0x06000D41 RID: 3393 RVA: 0x00033FD8 File Offset: 0x000321D8
	private static uint[] ProcessHashTable(uint p)
	{
		uint[] array = new uint[256];
		for (ushort num = 0; num < 256; num += 1)
		{
			array[(int)num] = (uint)num;
			for (uint num2 = 0u; num2 < 8u; num2 += 1u)
			{
				array[(int)num] = (((array[(int)num] & 1u) != 1u) ? (array[(int)num] >> 1) : (array[(int)num] >> 1 ^ p));
			}
		}
		return array;
	}

	// Token: 0x06000D42 RID: 3394 RVA: 0x00034040 File Offset: 0x00032240
	public static uint Quick(byte[] buffer)
	{
		return ~global::CRC32.BufferHash(global::CRC32.Default.Table, uint.MaxValue, buffer, 0, buffer.Length);
	}

	// Token: 0x06000D43 RID: 3395 RVA: 0x00034054 File Offset: 0x00032254
	public static uint String(string str)
	{
		byte[] bytes = Encoding.ASCII.GetBytes(str);
		return global::CRC32.Quick(bytes);
	}

	// Token: 0x06000D44 RID: 3396 RVA: 0x00034074 File Offset: 0x00032274
	public static uint Quick(uint seed, byte[] buffer)
	{
		return ~global::CRC32.BufferHash(global::CRC32.Default.Table, seed, buffer, 0, buffer.Length);
	}

	// Token: 0x06000D45 RID: 3397 RVA: 0x00034088 File Offset: 0x00032288
	public static uint Quick(uint polynomial, uint seed, byte[] buffer)
	{
		return ~global::CRC32.BufferHash(global::CRC32.Default.Table, seed, buffer, 0, buffer.Length);
	}

	// Token: 0x06000D46 RID: 3398 RVA: 0x0003409C File Offset: 0x0003229C
	private static uint BufferHash(uint[] table, uint seed, byte[] buffer, int start, int size)
	{
		while (size-- > 0)
		{
			seed = (seed >> 8 ^ table[(int)((UIntPtr)((uint)buffer[start++] ^ (seed & 255u)))]);
		}
		return seed;
	}

	// Token: 0x06000D47 RID: 3399 RVA: 0x000340D8 File Offset: 0x000322D8
	private void BufferHash(byte[] buffer, int start, int size)
	{
		while (size-- > 0)
		{
			this.hash = (this.hash >> 8 ^ this.table[(int)((UIntPtr)((uint)buffer[start++] ^ (this.hash & 255u)))]);
		}
	}

	// Token: 0x06000D48 RID: 3400 RVA: 0x00034118 File Offset: 0x00032318
	public override void Initialize()
	{
		this.hash = this.seed;
	}

	// Token: 0x06000D49 RID: 3401 RVA: 0x00034128 File Offset: 0x00032328
	protected sealed override void HashCore(byte[] buffer, int start, int length)
	{
		this.BufferHash(buffer, start, length);
	}

	// Token: 0x06000D4A RID: 3402 RVA: 0x00034134 File Offset: 0x00032334
	protected sealed override byte[] HashFinal()
	{
		uint num = ~this.hash;
		byte[] array = new byte[]
		{
			(byte)(num >> 24 & 255u),
			(byte)(num >> 16 & 255u),
			(byte)(num >> 8 & 255u),
			(byte)(num & 255u)
		};
		this.HashValue = array;
		return array;
	}

	// Token: 0x1700035C RID: 860
	// (get) Token: 0x06000D4B RID: 3403 RVA: 0x0003418C File Offset: 0x0003238C
	public sealed override int HashSize
	{
		get
		{
			return 32;
		}
	}

	// Token: 0x04000877 RID: 2167
	public const uint kDefaultPolynomial = 3988292384u;

	// Token: 0x04000878 RID: 2168
	public const uint kDefaultSeed = 4294967295u;

	// Token: 0x04000879 RID: 2169
	public const uint kTableSize = 256u;

	// Token: 0x0400087A RID: 2170
	private const uint I = 256u;

	// Token: 0x0400087B RID: 2171
	private const uint J = 8u;

	// Token: 0x0400087C RID: 2172
	private uint hash;

	// Token: 0x0400087D RID: 2173
	private readonly uint seed;

	// Token: 0x0400087E RID: 2174
	private readonly uint[] table;

	// Token: 0x020001D1 RID: 465
	private static class Default
	{
		// Token: 0x06000D4C RID: 3404 RVA: 0x00034190 File Offset: 0x00032390
		static Default()
		{
			for (uint num = 0u; num < 256u; num += 1u)
			{
				global::CRC32.Default.Table[(int)((UIntPtr)num)] = num;
				for (uint num2 = 0u; num2 < 8u; num2 += 1u)
				{
					global::CRC32.Default.Table[(int)((UIntPtr)num)] = (((global::CRC32.Default.Table[(int)((UIntPtr)num)] & 1u) != 1u) ? (global::CRC32.Default.Table[(int)((UIntPtr)num)] >> 1) : (global::CRC32.Default.Table[(int)((UIntPtr)num)] >> 1 ^ 3988292384u));
				}
			}
		}

		// Token: 0x0400087F RID: 2175
		public static readonly uint[] Table = new uint[256];
	}
}
