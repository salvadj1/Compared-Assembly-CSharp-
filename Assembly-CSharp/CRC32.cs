using System;
using System.Security.Cryptography;
using System.Text;

// Token: 0x020001A2 RID: 418
public sealed class CRC32 : HashAlgorithm
{
	// Token: 0x06000C07 RID: 3079 RVA: 0x00030088 File Offset: 0x0002E288
	public CRC32()
	{
		this.table = CRC32.Default.Table;
		this.seed = uint.MaxValue;
		this.Initialize();
	}

	// Token: 0x06000C08 RID: 3080 RVA: 0x000300A8 File Offset: 0x0002E2A8
	public CRC32(uint polynomial, uint seed)
	{
		this.table = ((polynomial != 3988292384u) ? CRC32.ProcessHashTable(polynomial) : CRC32.Default.Table);
		this.seed = seed;
		this.Initialize();
	}

	// Token: 0x06000C09 RID: 3081 RVA: 0x000300EC File Offset: 0x0002E2EC
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

	// Token: 0x06000C0A RID: 3082 RVA: 0x00030154 File Offset: 0x0002E354
	public static uint Quick(byte[] buffer)
	{
		return ~CRC32.BufferHash(CRC32.Default.Table, uint.MaxValue, buffer, 0, buffer.Length);
	}

	// Token: 0x06000C0B RID: 3083 RVA: 0x00030168 File Offset: 0x0002E368
	public static uint String(string str)
	{
		byte[] bytes = Encoding.ASCII.GetBytes(str);
		return CRC32.Quick(bytes);
	}

	// Token: 0x06000C0C RID: 3084 RVA: 0x00030188 File Offset: 0x0002E388
	public static uint Quick(uint seed, byte[] buffer)
	{
		return ~CRC32.BufferHash(CRC32.Default.Table, seed, buffer, 0, buffer.Length);
	}

	// Token: 0x06000C0D RID: 3085 RVA: 0x0003019C File Offset: 0x0002E39C
	public static uint Quick(uint polynomial, uint seed, byte[] buffer)
	{
		return ~CRC32.BufferHash(CRC32.Default.Table, seed, buffer, 0, buffer.Length);
	}

	// Token: 0x06000C0E RID: 3086 RVA: 0x000301B0 File Offset: 0x0002E3B0
	private static uint BufferHash(uint[] table, uint seed, byte[] buffer, int start, int size)
	{
		while (size-- > 0)
		{
			seed = (seed >> 8 ^ table[(int)((UIntPtr)((uint)buffer[start++] ^ (seed & 255u)))]);
		}
		return seed;
	}

	// Token: 0x06000C0F RID: 3087 RVA: 0x000301EC File Offset: 0x0002E3EC
	private void BufferHash(byte[] buffer, int start, int size)
	{
		while (size-- > 0)
		{
			this.hash = (this.hash >> 8 ^ this.table[(int)((UIntPtr)((uint)buffer[start++] ^ (this.hash & 255u)))]);
		}
	}

	// Token: 0x06000C10 RID: 3088 RVA: 0x0003022C File Offset: 0x0002E42C
	public override void Initialize()
	{
		this.hash = this.seed;
	}

	// Token: 0x06000C11 RID: 3089 RVA: 0x0003023C File Offset: 0x0002E43C
	protected sealed override void HashCore(byte[] buffer, int start, int length)
	{
		this.BufferHash(buffer, start, length);
	}

	// Token: 0x06000C12 RID: 3090 RVA: 0x00030248 File Offset: 0x0002E448
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

	// Token: 0x17000318 RID: 792
	// (get) Token: 0x06000C13 RID: 3091 RVA: 0x000302A0 File Offset: 0x0002E4A0
	public sealed override int HashSize
	{
		get
		{
			return 32;
		}
	}

	// Token: 0x04000763 RID: 1891
	public const uint kDefaultPolynomial = 3988292384u;

	// Token: 0x04000764 RID: 1892
	public const uint kDefaultSeed = 4294967295u;

	// Token: 0x04000765 RID: 1893
	public const uint kTableSize = 256u;

	// Token: 0x04000766 RID: 1894
	private const uint I = 256u;

	// Token: 0x04000767 RID: 1895
	private const uint J = 8u;

	// Token: 0x04000768 RID: 1896
	private uint hash;

	// Token: 0x04000769 RID: 1897
	private readonly uint seed;

	// Token: 0x0400076A RID: 1898
	private readonly uint[] table;

	// Token: 0x020001A3 RID: 419
	private static class Default
	{
		// Token: 0x06000C14 RID: 3092 RVA: 0x000302A4 File Offset: 0x0002E4A4
		static Default()
		{
			for (uint num = 0u; num < 256u; num += 1u)
			{
				CRC32.Default.Table[(int)((UIntPtr)num)] = num;
				for (uint num2 = 0u; num2 < 8u; num2 += 1u)
				{
					CRC32.Default.Table[(int)((UIntPtr)num)] = (((CRC32.Default.Table[(int)((UIntPtr)num)] & 1u) != 1u) ? (CRC32.Default.Table[(int)((UIntPtr)num)] >> 1) : (CRC32.Default.Table[(int)((UIntPtr)num)] >> 1 ^ 3988292384u));
				}
			}
		}

		// Token: 0x0400076B RID: 1899
		public static readonly uint[] Table = new uint[256];
	}
}
