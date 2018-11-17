using System;
using uLink;

// Token: 0x02000132 RID: 306
public static class CharacterStateFlagsExtenders
{
	// Token: 0x06000862 RID: 2146 RVA: 0x00023354 File Offset: 0x00021554
	public static void WriteCharacterStateFlags(this BitStream stream, global::CharacterStateFlags v)
	{
		stream.WriteUInt16(v.flags);
	}

	// Token: 0x06000863 RID: 2147 RVA: 0x00023364 File Offset: 0x00021564
	public static global::CharacterStateFlags ReadCharacterStateFlags(this BitStream stream)
	{
		global::CharacterStateFlags result;
		result.flags = stream.ReadUInt16();
		return result;
	}

	// Token: 0x06000864 RID: 2148 RVA: 0x00023380 File Offset: 0x00021580
	public static void Serialize(this BitStream stream, ref global::CharacterStateFlags v)
	{
		stream.Serialize<ushort>(ref v.flags, new object[0]);
	}
}
