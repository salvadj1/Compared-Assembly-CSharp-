using System;
using uLink;

// Token: 0x02000113 RID: 275
public static class CharacterStateFlagsExtenders
{
	// Token: 0x06000790 RID: 1936 RVA: 0x00020780 File Offset: 0x0001E980
	public static void WriteCharacterStateFlags(this BitStream stream, CharacterStateFlags v)
	{
		stream.WriteUInt16(v.flags);
	}

	// Token: 0x06000791 RID: 1937 RVA: 0x00020790 File Offset: 0x0001E990
	public static CharacterStateFlags ReadCharacterStateFlags(this BitStream stream)
	{
		CharacterStateFlags result;
		result.flags = stream.ReadUInt16();
		return result;
	}

	// Token: 0x06000792 RID: 1938 RVA: 0x000207AC File Offset: 0x0001E9AC
	public static void Serialize(this BitStream stream, ref CharacterStateFlags v)
	{
		stream.Serialize<ushort>(ref v.flags, new object[0]);
	}
}
