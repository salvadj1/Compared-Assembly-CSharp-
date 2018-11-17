using System;
using System.Text;

namespace Facepunch.Hash
{
	// Token: 0x020001D2 RID: 466
	public static class MurmurHash2
	{
		// Token: 0x06000D4D RID: 3405 RVA: 0x00034218 File Offset: 0x00032418
		public static uint UINT(byte[] key, int len, uint seed)
		{
			uint num = (uint)((ulong)seed ^ (ulong)((long)(len * 1)));
			int num2 = 0;
			while (len >= 4)
			{
				uint num3 = (uint)((int)key[num2++] | (int)key[num2++] << 8 | (int)key[num2++] << 16 | (int)key[num2++] << 24);
				num3 *= 1540483477u;
				num3 ^= num3 >> 24;
				num3 *= 1540483477u;
				num *= 1540483477u;
				num ^= num3;
				len -= 4;
			}
			switch (len)
			{
			case 1:
				num ^= (uint)key[num2];
				num *= 1540483477u;
				break;
			case 2:
				num ^= (uint)((uint)key[num2 + 1] << 8);
				num ^= (uint)key[num2];
				num *= 1540483477u;
				break;
			case 3:
				num ^= (uint)((uint)key[num2 + 2] << 16);
				num ^= (uint)((uint)key[num2 + 1] << 8);
				num ^= (uint)key[num2];
				num *= 1540483477u;
				break;
			}
			num ^= num >> 13;
			num *= 1540483477u;
			return num ^ num >> 15;
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x00034318 File Offset: 0x00032518
		public static uint UINT(byte[] key, uint seed)
		{
			return MurmurHash2.UINT(key, key.Length, seed);
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x00034324 File Offset: 0x00032524
		public static uint UINT(sbyte[] key, int len, uint seed)
		{
			uint num = (uint)((ulong)seed ^ (ulong)((long)(len * 1)));
			int num2 = 0;
			while (len >= 4)
			{
				uint num3 = (uint)((int)((byte)key[num2++]) | (int)((byte)key[num2++]) << 8 | (int)((byte)key[num2++]) << 16 | (int)((byte)key[num2++]) << 24);
				num3 *= 1540483477u;
				num3 ^= num3 >> 24;
				num3 *= 1540483477u;
				num *= 1540483477u;
				num ^= num3;
				len -= 4;
			}
			switch (len)
			{
			case 1:
				num ^= (uint)key[num2];
				num *= 1540483477u;
				break;
			case 2:
				num ^= (uint)key[num2 + 1] << 8;
				num ^= (uint)key[num2];
				num *= 1540483477u;
				break;
			case 3:
				num ^= (uint)key[num2 + 2] << 16;
				num ^= (uint)key[num2 + 1] << 8;
				num ^= (uint)key[num2];
				num *= 1540483477u;
				break;
			}
			num ^= num >> 13;
			num *= 1540483477u;
			return num ^ num >> 15;
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x0003442C File Offset: 0x0003262C
		public static uint UINT(sbyte[] key, uint seed)
		{
			return MurmurHash2.UINT(key, key.Length, seed);
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x00034438 File Offset: 0x00032638
		public static uint UINT(ushort[] key, int len, uint seed)
		{
			uint num = (uint)((ulong)seed ^ (ulong)((long)(len * 2)));
			int num2 = 0;
			while (len >= 2)
			{
				uint num3 = (uint)((int)key[num2++] | (int)key[num2++] << 16);
				num3 *= 1540483477u;
				num3 ^= num3 >> 24;
				num3 *= 1540483477u;
				num *= 1540483477u;
				num ^= num3;
				len -= 2;
			}
			int num4 = len;
			if (num4 == 1)
			{
				num ^= (uint)(key[num2] & 65280);
				num ^= (uint)(key[num2] & 255);
				num *= 1540483477u;
			}
			num ^= num >> 13;
			num *= 1540483477u;
			return num ^ num >> 15;
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x000344E4 File Offset: 0x000326E4
		public static uint UINT(ushort[] key, uint seed)
		{
			return MurmurHash2.UINT(key, key.Length, seed);
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x000344F0 File Offset: 0x000326F0
		public static uint UINT(short[] key, int len, uint seed)
		{
			uint num = (uint)((ulong)seed ^ (ulong)((long)(len * 2)));
			int num2 = 0;
			while (len >= 2)
			{
				uint num3 = (uint)((int)((ushort)key[num2++]) | (int)((ushort)key[num2++]) << 16);
				num3 *= 1540483477u;
				num3 ^= num3 >> 24;
				num3 *= 1540483477u;
				num *= 1540483477u;
				num ^= num3;
				len -= 2;
			}
			int num4 = len;
			if (num4 == 1)
			{
				num ^= ((uint)key[num2] & 65280u);
				num ^= (uint)(key[num2] & 255);
				num *= 1540483477u;
			}
			num ^= num >> 13;
			num *= 1540483477u;
			return num ^ num >> 15;
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x0003459C File Offset: 0x0003279C
		public static uint UINT(short[] key, uint seed)
		{
			return MurmurHash2.UINT(key, key.Length, seed);
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x000345A8 File Offset: 0x000327A8
		public static uint UINT(char[] key, int len, uint seed)
		{
			uint num = (uint)((ulong)seed ^ (ulong)((long)(len * 2)));
			int num2 = 0;
			while (len >= 2)
			{
				uint num3 = (uint)(key[num2++] | (uint)key[num2++] << 16);
				num3 *= 1540483477u;
				num3 ^= num3 >> 24;
				num3 *= 1540483477u;
				num *= 1540483477u;
				num ^= num3;
				len -= 2;
			}
			int num4 = len;
			if (num4 == 1)
			{
				num ^= (uint)(key[num2] & '＀');
				num ^= (uint)(key[num2] & 'ÿ');
				num *= 1540483477u;
			}
			num ^= num >> 13;
			num *= 1540483477u;
			return num ^ num >> 15;
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x00034654 File Offset: 0x00032854
		public static uint UINT(char[] key, uint seed)
		{
			return MurmurHash2.UINT(key, key.Length, seed);
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x00034660 File Offset: 0x00032860
		public static uint UINT(string key, int len, uint seed)
		{
			uint num = (uint)((ulong)seed ^ (ulong)((long)(len * 2)));
			int index = 0;
			while (len >= 2)
			{
				uint num2 = (uint)(key[index++] | (uint)key[index++] << 16);
				num2 *= 1540483477u;
				num2 ^= num2 >> 24;
				num2 *= 1540483477u;
				num *= 1540483477u;
				num ^= num2;
				len -= 2;
			}
			int num3 = len;
			if (num3 == 1)
			{
				num ^= (uint)(key[index] & '＀');
				num ^= (uint)(key[index] & 'ÿ');
				num *= 1540483477u;
			}
			num ^= num >> 13;
			num *= 1540483477u;
			return num ^ num >> 15;
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x0003471C File Offset: 0x0003291C
		public static uint UINT(string key, uint seed)
		{
			return MurmurHash2.UINT(key, key.Length, seed);
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x0003472C File Offset: 0x0003292C
		public static uint UINT(uint[] key, int len, uint seed)
		{
			uint num = (uint)((ulong)seed ^ (ulong)((long)(len * 4)));
			int num2 = 0;
			while (len > 0)
			{
				uint num3 = key[num2++];
				num3 *= 1540483477u;
				num3 ^= num3 >> 24;
				num3 *= 1540483477u;
				num *= 1540483477u;
				num ^= num3;
				len--;
			}
			num ^= num >> 13;
			num *= 1540483477u;
			return num ^ num >> 15;
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x00034798 File Offset: 0x00032998
		public static uint UINT(uint[] key, uint seed)
		{
			return MurmurHash2.UINT(key, key.Length, seed);
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x000347A4 File Offset: 0x000329A4
		public static uint UINT(int[] key, int len, uint seed)
		{
			uint num = (uint)((ulong)seed ^ (ulong)((long)(len * 4)));
			int num2 = 0;
			while (len > 0)
			{
				uint num3 = (uint)key[num2++];
				num3 *= 1540483477u;
				num3 ^= num3 >> 24;
				num3 *= 1540483477u;
				num *= 1540483477u;
				num ^= num3;
				len--;
			}
			num ^= num >> 13;
			num *= 1540483477u;
			return num ^ num >> 15;
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x00034810 File Offset: 0x00032A10
		public static uint UINT(int[] key, uint seed)
		{
			return MurmurHash2.UINT(key, key.Length, seed);
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x0003481C File Offset: 0x00032A1C
		public static uint UINT(ulong[] key, int len, uint seed)
		{
			uint num = (uint)((ulong)seed ^ (ulong)((long)(len * 8)));
			int num2 = 0;
			while (len > 0)
			{
				uint num3 = (uint)(key[num2] & (ulong)-1);
				num3 *= 1540483477u;
				num3 ^= num3 >> 24;
				num3 *= 1540483477u;
				num *= 1540483477u;
				num ^= num3;
				uint num4 = (uint)(key[num2] >> 32 & (ulong)-1);
				num4 *= 1540483477u;
				num4 ^= num4 >> 24;
				num4 *= 1540483477u;
				num *= 1540483477u;
				num ^= num4;
				len--;
			}
			num ^= num >> 13;
			num *= 1540483477u;
			return num ^ num >> 15;
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x000348B8 File Offset: 0x00032AB8
		public static uint UINT(ulong[] key, uint seed)
		{
			return MurmurHash2.UINT(key, key.Length, seed);
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x000348C4 File Offset: 0x00032AC4
		public static uint UINT(long[] key, int len, uint seed)
		{
			uint num = (uint)((ulong)seed ^ (ulong)((long)(len * 8)));
			int num2 = 0;
			while (len > 0)
			{
				uint num3 = (uint)(key[num2] & (long)((ulong)-1));
				num3 *= 1540483477u;
				num3 ^= num3 >> 24;
				num3 *= 1540483477u;
				num *= 1540483477u;
				num ^= num3;
				uint num4 = (uint)(key[num2] >> 32 & (long)((ulong)-1));
				num4 *= 1540483477u;
				num4 ^= num4 >> 24;
				num4 *= 1540483477u;
				num *= 1540483477u;
				num ^= num4;
				len--;
			}
			num ^= num >> 13;
			num *= 1540483477u;
			return num ^ num >> 15;
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x00034960 File Offset: 0x00032B60
		public static uint UINT(long[] key, uint seed)
		{
			return MurmurHash2.UINT(key, key.Length, seed);
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x0003496C File Offset: 0x00032B6C
		public static uint UINT_BLOCK(Array key, int len, uint seed)
		{
			uint num = (uint)((ulong)seed ^ (ulong)((long)(len * 1)));
			int num2 = 0;
			while (len >= 4)
			{
				uint num3 = (uint)((int)Buffer.GetByte(key, num2++) | (int)Buffer.GetByte(key, num2++) << 8 | (int)Buffer.GetByte(key, num2++) << 16 | (int)Buffer.GetByte(key, num2++) << 24);
				num3 *= 1540483477u;
				num3 ^= num3 >> 24;
				num3 *= 1540483477u;
				num *= 1540483477u;
				num ^= num3;
				len -= 4;
			}
			switch (len)
			{
			case 1:
				num ^= (uint)Buffer.GetByte(key, num2);
				num *= 1540483477u;
				break;
			case 2:
				num ^= (uint)((uint)Buffer.GetByte(key, num2 + 1) << 8);
				num ^= (uint)Buffer.GetByte(key, num2);
				num *= 1540483477u;
				break;
			case 3:
				num ^= (uint)((uint)Buffer.GetByte(key, num2 + 2) << 16);
				num ^= (uint)((uint)Buffer.GetByte(key, num2 + 1) << 8);
				num ^= (uint)Buffer.GetByte(key, num2);
				num *= 1540483477u;
				break;
			}
			num ^= num >> 13;
			num *= 1540483477u;
			return num ^ num >> 15;
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x00034A94 File Offset: 0x00032C94
		public static uint UINT_BLOCK(Array key, uint seed)
		{
			return MurmurHash2.UINT_BLOCK(key, Buffer.ByteLength(key), seed);
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x00034AA4 File Offset: 0x00032CA4
		public static uint UINT(string key, Encoding encoding, uint seed)
		{
			return MurmurHash2.UINT(encoding.GetBytes(key), seed);
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x00034AB4 File Offset: 0x00032CB4
		public static int SINT(byte[] key, int len, uint seed)
		{
			return (int)MurmurHash2.UINT(key, len, seed);
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x00034AC0 File Offset: 0x00032CC0
		public static int SINT(sbyte[] key, int len, uint seed)
		{
			return (int)MurmurHash2.UINT(key, len, seed);
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x00034ACC File Offset: 0x00032CCC
		public static int SINT(ushort[] key, int len, uint seed)
		{
			return (int)MurmurHash2.UINT(key, len, seed);
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x00034AD8 File Offset: 0x00032CD8
		public static int SINT(short[] key, int len, uint seed)
		{
			return (int)MurmurHash2.UINT(key, len, seed);
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x00034AE4 File Offset: 0x00032CE4
		public static int SINT(char[] key, int len, uint seed)
		{
			return (int)MurmurHash2.UINT(key, len, seed);
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x00034AF0 File Offset: 0x00032CF0
		public static int SINT(string key, int len, uint seed)
		{
			return (int)MurmurHash2.UINT(key, len, seed);
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x00034AFC File Offset: 0x00032CFC
		public static int SINT(uint[] key, int len, uint seed)
		{
			return (int)MurmurHash2.UINT(key, len, seed);
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x00034B08 File Offset: 0x00032D08
		public static int SINT(int[] key, int len, uint seed)
		{
			return (int)MurmurHash2.UINT(key, len, seed);
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x00034B14 File Offset: 0x00032D14
		public static int SINT(ulong[] key, int len, uint seed)
		{
			return (int)MurmurHash2.UINT(key, len, seed);
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x00034B20 File Offset: 0x00032D20
		public static int SINT(long[] key, int len, uint seed)
		{
			return (int)MurmurHash2.UINT(key, len, seed);
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x00034B2C File Offset: 0x00032D2C
		public static int SINT(byte[] key, uint seed)
		{
			return (int)MurmurHash2.UINT(key, key.Length, seed);
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x00034B38 File Offset: 0x00032D38
		public static int SINT(sbyte[] key, uint seed)
		{
			return (int)MurmurHash2.UINT(key, key.Length, seed);
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x00034B44 File Offset: 0x00032D44
		public static int SINT(ushort[] key, uint seed)
		{
			return (int)MurmurHash2.UINT(key, key.Length, seed);
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x00034B50 File Offset: 0x00032D50
		public static int SINT(short[] key, uint seed)
		{
			return (int)MurmurHash2.UINT(key, key.Length, seed);
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x00034B5C File Offset: 0x00032D5C
		public static int SINT(char[] key, uint seed)
		{
			return (int)MurmurHash2.UINT(key, key.Length, seed);
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x00034B68 File Offset: 0x00032D68
		public static int SINT(string key, uint seed)
		{
			return (int)MurmurHash2.UINT(key, key.Length, seed);
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x00034B78 File Offset: 0x00032D78
		public static int SINT(uint[] key, uint seed)
		{
			return (int)MurmurHash2.UINT(key, key.Length, seed);
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x00034B84 File Offset: 0x00032D84
		public static int SINT(int[] key, uint seed)
		{
			return (int)MurmurHash2.UINT(key, key.Length, seed);
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x00034B90 File Offset: 0x00032D90
		public static int SINT(ulong[] key, uint seed)
		{
			return (int)MurmurHash2.UINT(key, key.Length, seed);
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x00034B9C File Offset: 0x00032D9C
		public static int SINT(long[] key, uint seed)
		{
			return (int)MurmurHash2.UINT(key, key.Length, seed);
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x00034BA8 File Offset: 0x00032DA8
		public static int SINT_BLOCK(Array key, int len, uint seed)
		{
			return (int)MurmurHash2.UINT_BLOCK(key, len, seed);
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x00034BB4 File Offset: 0x00032DB4
		public static int SINT_BLOCK(Array key, uint seed)
		{
			return (int)MurmurHash2.UINT_BLOCK(key, Buffer.ByteLength(key), seed);
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x00034BC4 File Offset: 0x00032DC4
		public static int SINT(string key, Encoding encoding, uint seed)
		{
			return (int)MurmurHash2.UINT(key, encoding, seed);
		}

		// Token: 0x04000880 RID: 2176
		public const uint m = 1540483477u;

		// Token: 0x04000881 RID: 2177
		public const int r = 24;
	}
}
