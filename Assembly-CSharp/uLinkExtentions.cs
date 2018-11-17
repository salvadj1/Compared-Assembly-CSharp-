using System;
using uLink;

// Token: 0x02000329 RID: 809
public static class uLinkExtentions
{
	// Token: 0x06001EE7 RID: 7911 RVA: 0x00079A20 File Offset: 0x00077C20
	public static void Write7BitEncodedSize(this BitStream stream, ulong u)
	{
		while (u >= 128UL)
		{
			stream.WriteByte((byte)((u & 127UL) | 128UL));
			u >>= 7;
		}
		stream.WriteByte((byte)u);
	}

	// Token: 0x06001EE8 RID: 7912 RVA: 0x00079A54 File Offset: 0x00077C54
	public static void Write7BitEncodedSize(this BitStream stream, uint u)
	{
		while (u >= 128u)
		{
			stream.WriteByte((byte)((u & 127u) | 128u));
			u >>= 7;
		}
		stream.WriteByte((byte)u);
	}

	// Token: 0x06001EE9 RID: 7913 RVA: 0x00079A90 File Offset: 0x00077C90
	public static void Write7BitEncodedSize(this BitStream stream, ushort u)
	{
		while (u >= 128)
		{
			stream.WriteByte((byte)((u & 127) | 128));
			u = (ushort)(u >> 7);
		}
		stream.WriteByte((byte)u);
	}

	// Token: 0x06001EEA RID: 7914 RVA: 0x00079AC4 File Offset: 0x00077CC4
	public static void Write7BitEncodedSize(this BitStream stream, long u)
	{
		if (u < 0L)
		{
			throw new ArgumentOutOfRangeException("u", "u<0");
		}
		stream.Write7BitEncodedSize((ulong)u);
	}

	// Token: 0x06001EEB RID: 7915 RVA: 0x00079AE8 File Offset: 0x00077CE8
	public static void Write7BitEncodedSize(this BitStream stream, int u)
	{
		if (u < 0)
		{
			throw new ArgumentOutOfRangeException("u", "u<0");
		}
		stream.Write7BitEncodedSize((uint)u);
	}

	// Token: 0x06001EEC RID: 7916 RVA: 0x00079B08 File Offset: 0x00077D08
	public static void Write7BitEncodedSize(this BitStream stream, short u)
	{
		if (u < 0)
		{
			throw new ArgumentOutOfRangeException("u", "u<0");
		}
		stream.Write7BitEncodedSize((ushort)u);
	}

	// Token: 0x06001EED RID: 7917 RVA: 0x00079B2C File Offset: 0x00077D2C
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

	// Token: 0x06001EEE RID: 7918 RVA: 0x00079B88 File Offset: 0x00077D88
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

	// Token: 0x06001EEF RID: 7919 RVA: 0x00079BE0 File Offset: 0x00077DE0
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

	// Token: 0x06001EF0 RID: 7920 RVA: 0x00079C38 File Offset: 0x00077E38
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

	// Token: 0x06001EF1 RID: 7921 RVA: 0x00079C6C File Offset: 0x00077E6C
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

	// Token: 0x06001EF2 RID: 7922 RVA: 0x00079C9C File Offset: 0x00077E9C
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

	// Token: 0x06001EF3 RID: 7923 RVA: 0x00079CCC File Offset: 0x00077ECC
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

	// Token: 0x06001EF4 RID: 7924 RVA: 0x00079D0C File Offset: 0x00077F0C
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

	// Token: 0x06001EF5 RID: 7925 RVA: 0x00079D70 File Offset: 0x00077F70
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

	// Token: 0x06001EF6 RID: 7926 RVA: 0x00079EE4 File Offset: 0x000780E4
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

	// Token: 0x04000EE4 RID: 3812
	private const ushort bit8 = 128;

	// Token: 0x04000EE5 RID: 3813
	private const ushort bit1234567 = 127;

	// Token: 0x04000EE6 RID: 3814
	private const int kByte0 = 0;

	// Token: 0x04000EE7 RID: 3815
	private const int kByte1 = 8;

	// Token: 0x04000EE8 RID: 3816
	private const int kByte2 = 16;

	// Token: 0x04000EE9 RID: 3817
	private const int kByte3 = 24;

	// Token: 0x04000EEA RID: 3818
	private const int kByte4 = 32;

	// Token: 0x04000EEB RID: 3819
	private const int kByte5 = 40;

	// Token: 0x04000EEC RID: 3820
	private const int kByte6 = 48;

	// Token: 0x04000EED RID: 3821
	private const int kByte7 = 56;
}
