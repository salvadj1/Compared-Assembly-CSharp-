using System;
using System.Text;

namespace Facepunch.Hash
{
	// Token: 0x020001A4 RID: 420
	public static class MurmurHash2
	{
		// Token: 0x06000C15 RID: 3093 RVA: 0x0003032C File Offset: 0x0002E52C
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

		// Token: 0x06000C16 RID: 3094 RVA: 0x0003042C File Offset: 0x0002E62C
		public static uint UINT(byte[] key, uint seed)
		{
			return MurmurHash2.UINT(key, key.Length, seed);
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x00030438 File Offset: 0x0002E638
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

		// Token: 0x06000C18 RID: 3096 RVA: 0x00030540 File Offset: 0x0002E740
		public static uint UINT(sbyte[] key, uint seed)
		{
			return MurmurHash2.UINT(key, key.Length, seed);
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x0003054C File Offset: 0x0002E74C
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

		// Token: 0x06000C1A RID: 3098 RVA: 0x000305F8 File Offset: 0x0002E7F8
		public static uint UINT(ushort[] key, uint seed)
		{
			return MurmurHash2.UINT(key, key.Length, seed);
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x00030604 File Offset: 0x0002E804
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

		// Token: 0x06000C1C RID: 3100 RVA: 0x000306B0 File Offset: 0x0002E8B0
		public static uint UINT(short[] key, uint seed)
		{
			return MurmurHash2.UINT(key, key.Length, seed);
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x000306BC File Offset: 0x0002E8BC
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

		// Token: 0x06000C1E RID: 3102 RVA: 0x00030768 File Offset: 0x0002E968
		public static uint UINT(char[] key, uint seed)
		{
			return MurmurHash2.UINT(key, key.Length, seed);
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x00030774 File Offset: 0x0002E974
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

		// Token: 0x06000C20 RID: 3104 RVA: 0x00030830 File Offset: 0x0002EA30
		public static uint UINT(string key, uint seed)
		{
			return MurmurHash2.UINT(key, key.Length, seed);
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x00030840 File Offset: 0x0002EA40
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

		// Token: 0x06000C22 RID: 3106 RVA: 0x000308AC File Offset: 0x0002EAAC
		public static uint UINT(uint[] key, uint seed)
		{
			return MurmurHash2.UINT(key, key.Length, seed);
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x000308B8 File Offset: 0x0002EAB8
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

		// Token: 0x06000C24 RID: 3108 RVA: 0x00030924 File Offset: 0x0002EB24
		public static uint UINT(int[] key, uint seed)
		{
			return MurmurHash2.UINT(key, key.Length, seed);
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x00030930 File Offset: 0x0002EB30
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

		// Token: 0x06000C26 RID: 3110 RVA: 0x000309CC File Offset: 0x0002EBCC
		public static uint UINT(ulong[] key, uint seed)
		{
			return MurmurHash2.UINT(key, key.Length, seed);
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x000309D8 File Offset: 0x0002EBD8
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

		// Token: 0x06000C28 RID: 3112 RVA: 0x00030A74 File Offset: 0x0002EC74
		public static uint UINT(long[] key, uint seed)
		{
			return MurmurHash2.UINT(key, key.Length, seed);
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x00030A80 File Offset: 0x0002EC80
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

		// Token: 0x06000C2A RID: 3114 RVA: 0x00030BA8 File Offset: 0x0002EDA8
		public static uint UINT_BLOCK(Array key, uint seed)
		{
			return MurmurHash2.UINT_BLOCK(key, Buffer.ByteLength(key), seed);
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x00030BB8 File Offset: 0x0002EDB8
		public static uint UINT(string key, Encoding encoding, uint seed)
		{
			return MurmurHash2.UINT(encoding.GetBytes(key), seed);
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x00030BC8 File Offset: 0x0002EDC8
		public static int SINT(byte[] key, int len, uint seed)
		{
			return (int)MurmurHash2.UINT(key, len, seed);
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x00030BD4 File Offset: 0x0002EDD4
		public static int SINT(sbyte[] key, int len, uint seed)
		{
			return (int)MurmurHash2.UINT(key, len, seed);
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x00030BE0 File Offset: 0x0002EDE0
		public static int SINT(ushort[] key, int len, uint seed)
		{
			return (int)MurmurHash2.UINT(key, len, seed);
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x00030BEC File Offset: 0x0002EDEC
		public static int SINT(short[] key, int len, uint seed)
		{
			return (int)MurmurHash2.UINT(key, len, seed);
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x00030BF8 File Offset: 0x0002EDF8
		public static int SINT(char[] key, int len, uint seed)
		{
			return (int)MurmurHash2.UINT(key, len, seed);
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x00030C04 File Offset: 0x0002EE04
		public static int SINT(string key, int len, uint seed)
		{
			return (int)MurmurHash2.UINT(key, len, seed);
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x00030C10 File Offset: 0x0002EE10
		public static int SINT(uint[] key, int len, uint seed)
		{
			return (int)MurmurHash2.UINT(key, len, seed);
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x00030C1C File Offset: 0x0002EE1C
		public static int SINT(int[] key, int len, uint seed)
		{
			return (int)MurmurHash2.UINT(key, len, seed);
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x00030C28 File Offset: 0x0002EE28
		public static int SINT(ulong[] key, int len, uint seed)
		{
			return (int)MurmurHash2.UINT(key, len, seed);
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x00030C34 File Offset: 0x0002EE34
		public static int SINT(long[] key, int len, uint seed)
		{
			return (int)MurmurHash2.UINT(key, len, seed);
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x00030C40 File Offset: 0x0002EE40
		public static int SINT(byte[] key, uint seed)
		{
			return (int)MurmurHash2.UINT(key, key.Length, seed);
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x00030C4C File Offset: 0x0002EE4C
		public static int SINT(sbyte[] key, uint seed)
		{
			return (int)MurmurHash2.UINT(key, key.Length, seed);
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x00030C58 File Offset: 0x0002EE58
		public static int SINT(ushort[] key, uint seed)
		{
			return (int)MurmurHash2.UINT(key, key.Length, seed);
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x00030C64 File Offset: 0x0002EE64
		public static int SINT(short[] key, uint seed)
		{
			return (int)MurmurHash2.UINT(key, key.Length, seed);
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x00030C70 File Offset: 0x0002EE70
		public static int SINT(char[] key, uint seed)
		{
			return (int)MurmurHash2.UINT(key, key.Length, seed);
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x00030C7C File Offset: 0x0002EE7C
		public static int SINT(string key, uint seed)
		{
			return (int)MurmurHash2.UINT(key, key.Length, seed);
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x00030C8C File Offset: 0x0002EE8C
		public static int SINT(uint[] key, uint seed)
		{
			return (int)MurmurHash2.UINT(key, key.Length, seed);
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x00030C98 File Offset: 0x0002EE98
		public static int SINT(int[] key, uint seed)
		{
			return (int)MurmurHash2.UINT(key, key.Length, seed);
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x00030CA4 File Offset: 0x0002EEA4
		public static int SINT(ulong[] key, uint seed)
		{
			return (int)MurmurHash2.UINT(key, key.Length, seed);
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x00030CB0 File Offset: 0x0002EEB0
		public static int SINT(long[] key, uint seed)
		{
			return (int)MurmurHash2.UINT(key, key.Length, seed);
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x00030CBC File Offset: 0x0002EEBC
		public static int SINT_BLOCK(Array key, int len, uint seed)
		{
			return (int)MurmurHash2.UINT_BLOCK(key, len, seed);
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x00030CC8 File Offset: 0x0002EEC8
		public static int SINT_BLOCK(Array key, uint seed)
		{
			return (int)MurmurHash2.UINT_BLOCK(key, Buffer.ByteLength(key), seed);
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x00030CD8 File Offset: 0x0002EED8
		public static int SINT(string key, Encoding encoding, uint seed)
		{
			return (int)MurmurHash2.UINT(key, encoding, seed);
		}

		// Token: 0x0400076C RID: 1900
		public const uint m = 1540483477u;

		// Token: 0x0400076D RID: 1901
		public const int r = 24;
	}
}
