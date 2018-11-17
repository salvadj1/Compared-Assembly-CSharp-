using System;
using uLink;

// Token: 0x020003D6 RID: 982
public static class uLinkExtentions
{
	// Token: 0x06002249 RID: 8777 RVA: 0x0007EE1C File Offset: 0x0007D01C
	public static void Write7BitEncodedSize(this BitStream stream, ulong u)
	{
		while (u >= 128UL)
		{
			stream.WriteByte((byte)((u & 127UL) | 128UL));
			u >>= 7;
		}
		stream.WriteByte((byte)u);
	}

	// Token: 0x0600224A RID: 8778 RVA: 0x0007EE50 File Offset: 0x0007D050
	public static void Write7BitEncodedSize(this BitStream stream, uint u)
	{
		while (u >= 128u)
		{
			stream.WriteByte((byte)((u & 127u) | 128u));
			u >>= 7;
		}
		stream.WriteByte((byte)u);
	}

	// Token: 0x0600224B RID: 8779 RVA: 0x0007EE8C File Offset: 0x0007D08C
	public static void Write7BitEncodedSize(this BitStream stream, ushort u)
	{
		while (u >= 128)
		{
			stream.WriteByte((byte)((u & 127) | 128));
			u = (ushort)(u >> 7);
		}
		stream.WriteByte((byte)u);
	}

	// Token: 0x0600224C RID: 8780 RVA: 0x0007EEC0 File Offset: 0x0007D0C0
	public static void Write7BitEncodedSize(this BitStream stream, long u)
	{
		if (u < 0L)
		{
			throw new ArgumentOutOfRangeException("u", "u<0");
		}
		stream.Write7BitEncodedSize((ulong)u);
	}

	// Token: 0x0600224D RID: 8781 RVA: 0x0007EEE4 File Offset: 0x0007D0E4
	public static void Write7BitEncodedSize(this BitStream stream, int u)
	{
		if (u < 0)
		{
			throw new ArgumentOutOfRangeException("u", "u<0");
		}
		stream.Write7BitEncodedSize((uint)u);
	}

	// Token: 0x0600224E RID: 8782 RVA: 0x0007EF04 File Offset: 0x0007D104
	public static void Write7BitEncodedSize(this BitStream stream, short u)
	{
		if (u < 0)
		{
			throw new ArgumentOutOfRangeException("u", "u<0");
		}
		stream.Write7BitEncodedSize((ushort)u);
	}

	// Token: 0x0600224F RID: 8783 RVA: 0x0007EF28 File Offset: 0x0007D128
	public static void Read7BitEncodedSize(this BitStream stream, out ulong u)
	{
		byte b = stream.ReadByte();
		int num = 0;
		u = (ulong)(b & 127);
		while ((b & 128) == 128 && num <= 9)
		{
			b = stream.ReadByte();
			u |= (ulong)((ulong)(b & 127) << (++num * 7 & 31 & 31));
		}
	}

	// Token: 0x06002250 RID: 8784 RVA: 0x0007EF84 File Offset: 0x0007D184
	public static void Read7BitEncodedSize(this BitStream stream, out uint u)
	{
		byte b = stream.ReadByte();
		int num = 0;
		u = (uint)(b & 127);
		while ((b & 128) == 128 && num <= 4)
		{
			b = stream.ReadByte();
			u |= (uint)((uint)(b & 127) << ++num * 7);
		}
	}

	// Token: 0x06002251 RID: 8785 RVA: 0x0007EFDC File Offset: 0x0007D1DC
	public static void Read7BitEncodedSize(this BitStream stream, out ushort u)
	{
		byte b = stream.ReadByte();
		int num = 0;
		u = (ushort)(b & 127);
		while ((b & 128) == 128 && num <= 2)
		{
			b = stream.ReadByte();
			u |= (ushort)((b & 127) << (++num * 7 & 31));
		}
	}

	// Token: 0x06002252 RID: 8786 RVA: 0x0007F034 File Offset: 0x0007D234
	public static void Read7BitEncodedSize(this BitStream stream, out long u)
	{
		ulong num;
		stream.Read7BitEncodedSize(out num);
		if (num > 9223372036854775807UL)
		{
			throw new InvalidOperationException("Wrong");
		}
		u = (long)num;
	}

	// Token: 0x06002253 RID: 8787 RVA: 0x0007F068 File Offset: 0x0007D268
	public static void Read7BitEncodedSize(this BitStream stream, out int u)
	{
		uint num;
		stream.Read7BitEncodedSize(out num);
		if (num > 2147483647u)
		{
			throw new InvalidOperationException("Wrong");
		}
		u = (int)num;
	}

	// Token: 0x06002254 RID: 8788 RVA: 0x0007F098 File Offset: 0x0007D298
	public static void Read7BitEncodedSize(this BitStream stream, out short u)
	{
		ushort num;
		stream.Read7BitEncodedSize(out num);
		if (num > 32767)
		{
			throw new InvalidOperationException("Wrong");
		}
		u = (short)num;
	}

	// Token: 0x06002255 RID: 8789 RVA: 0x0007F0C8 File Offset: 0x0007D2C8
	public static byte[] GetDataByteArray(this BitStream stream)
	{
		int bitCount = stream._bitCount;
		int num = bitCount / 8;
		if (bitCount % 8 != 0)
		{
			num++;
		}
		byte[] data = stream._data;
		if (data.Length > num)
		{
			Array.Resize<byte>(ref data, num);
		}
		return data;
	}

	// Token: 0x06002256 RID: 8790 RVA: 0x0007F108 File Offset: 0x0007D308
	public static byte[] GetDataByteArrayShiftedRight(this BitStream stream, int right)
	{
		if (right == 0)
		{
			return stream.GetDataByteArray();
		}
		int bitCount = stream._bitCount;
		int num = bitCount / 8;
		if (bitCount % 8 != 0)
		{
			num++;
		}
		byte[] array = new byte[right + num];
		byte[] data = stream._data;
		for (int i = 0; i < num; i++)
		{
			array[right++] = data[i];
		}
		return array;
	}

	// Token: 0x06002257 RID: 8791 RVA: 0x0007F16C File Offset: 0x0007D36C
	public static void WriteByteArray_MinimumCalls(this BitStream stream, byte[] array, int offset, int length, params object[] codecOptions)
	{
		stream.Write<int>(length, codecOptions);
		int num = offset + length;
		int num2 = length / 8;
		int num3 = length / 4;
		int num4 = length / 2;
		int num5 = length - num4 * 2;
		while (num5-- > 0)
		{
			stream.Write<byte>(array[--num], codecOptions);
		}
		num4 -= num3 * 2;
		while (num4-- > 0)
		{
			ushort num6 = (ushort)(array[--num] << 8);
			num6 |= (ushort)array[--num];
			stream.Write<ushort>(num6, codecOptions);
		}
		num3 -= num2 * 2;
		while (num3-- > 0)
		{
			uint num7 = (uint)((uint)array[--num] << 24);
			num7 |= (uint)((uint)array[--num] << 16);
			num7 |= (uint)((uint)array[--num] << 8);
			num7 |= (uint)array[--num];
			stream.Write<uint>(num7, codecOptions);
		}
		while (num2-- > 0)
		{
			ulong num8 = (ulong)array[--num] << 56;
			num8 |= (ulong)array[--num] << 48;
			num8 |= (ulong)array[--num] << 40;
			num8 |= (ulong)array[--num] << 32;
			num8 |= (ulong)array[--num] << 24;
			num8 |= (ulong)array[--num] << 16;
			num8 |= (ulong)array[--num] << 8;
			num8 |= (ulong)array[--num];
			stream.Write<ulong>(num8, codecOptions);
		}
	}

	// Token: 0x06002258 RID: 8792 RVA: 0x0007F2E0 File Offset: 0x0007D4E0
	public static void ReadByteArray_MinimalCalls(this BitStream stream, out byte[] array, out int length, params object[] codecOptions)
	{
		length = stream.Read<int>(codecOptions);
		if (length == 0)
		{
			array = null;
		}
		else
		{
			array = new byte[length];
			int num = length;
			int num2 = length / 8;
			int num3 = length / 4;
			int num4 = length / 2;
			int num5 = length;
			num5 -= num4 * 2;
			while (num5-- > 0)
			{
				array[--num] = stream.Read<byte>(codecOptions);
			}
			num4 -= num3 * 2;
			while (num4-- > 0)
			{
				ushort num6 = stream.Read<ushort>(codecOptions);
				array[--num] = (byte)(num6 >> 8 & 255);
				array[--num] = (byte)(num6 & 255);
			}
			num3 -= num2 * 2;
			while (num3-- > 0)
			{
				uint num7 = stream.Read<uint>(codecOptions);
				array[--num] = (byte)(num7 >> 24 & 255u);
				array[--num] = (byte)(num7 >> 16 & 255u);
				array[--num] = (byte)(num7 >> 8 & 255u);
				array[--num] = (byte)(num7 & 255u);
			}
			while (num2-- > 0)
			{
				ulong num8 = stream.Read<ulong>(codecOptions);
				array[--num] = (byte)(num8 >> 56 & 255UL);
				array[--num] = (byte)(num8 >> 48 & 255UL);
				array[--num] = (byte)(num8 >> 40 & 255UL);
				array[--num] = (byte)(num8 >> 32 & 255UL);
				array[--num] = (byte)(num8 >> 24 & 255UL);
				array[--num] = (byte)(num8 >> 16 & 255UL);
				array[--num] = (byte)(num8 >> 8 & 255UL);
				array[--num] = (byte)(num8 & 255UL);
			}
		}
	}

	// Token: 0x0400104A RID: 4170
	private const ushort bit8 = 128;

	// Token: 0x0400104B RID: 4171
	private const ushort bit1234567 = 127;

	// Token: 0x0400104C RID: 4172
	private const int kByte0 = 0;

	// Token: 0x0400104D RID: 4173
	private const int kByte1 = 8;

	// Token: 0x0400104E RID: 4174
	private const int kByte2 = 16;

	// Token: 0x0400104F RID: 4175
	private const int kByte3 = 24;

	// Token: 0x04001050 RID: 4176
	private const int kByte4 = 32;

	// Token: 0x04001051 RID: 4177
	private const int kByte5 = 40;

	// Token: 0x04001052 RID: 4178
	private const int kByte6 = 48;

	// Token: 0x04001053 RID: 4179
	private const int kByte7 = 56;
}
