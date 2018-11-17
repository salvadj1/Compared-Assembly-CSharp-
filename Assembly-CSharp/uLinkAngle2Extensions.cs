using System;
using uLink;

// Token: 0x0200023C RID: 572
public static class uLinkAngle2Extensions
{
	// Token: 0x0600153C RID: 5436 RVA: 0x0004F7D0 File Offset: 0x0004D9D0
	static uLinkAngle2Extensions()
	{
		uLinkAngle2Extensions.deserializer = new BitStreamCodec.Deserializer(uLinkAngle2Extensions.Deserializer);
		BitStreamCodec.Add<Angle2>(uLinkAngle2Extensions.deserializer, uLinkAngle2Extensions.serializer, 13, false);
	}

	// Token: 0x0600153D RID: 5437 RVA: 0x0004F82C File Offset: 0x0004DA2C
	public static void Serialize(this BitStream stream, ref Angle2 value, params object[] codecOptions)
	{
		int encoded = value.encoded;
		int num = encoded;
		stream.Serialize(ref num, codecOptions);
		if (num != encoded)
		{
			value.encoded = num;
		}
	}

	// Token: 0x0600153E RID: 5438 RVA: 0x0004F85C File Offset: 0x0004DA5C
	public static void WriteAngle2(this BitStream stream, Angle2 value)
	{
		stream.WriteInt32(value.encoded);
	}

	// Token: 0x0600153F RID: 5439 RVA: 0x0004F86C File Offset: 0x0004DA6C
	public static Angle2 ReadAngle2(this BitStream stream)
	{
		return new Angle2
		{
			encoded = stream.ReadInt32()
		};
	}

	// Token: 0x06001540 RID: 5440 RVA: 0x0004F894 File Offset: 0x0004DA94
	private static object Deserializer(BitStream stream, params object[] codecOptions)
	{
		object obj = uLinkAngle2Extensions.int32Codec.deserializer.Invoke(stream, codecOptions);
		if (obj is int)
		{
			return new Angle2
			{
				encoded = (int)obj
			};
		}
		return obj;
	}

	// Token: 0x06001541 RID: 5441 RVA: 0x0004F8E0 File Offset: 0x0004DAE0
	private static void Serializer(BitStream stream, object value, params object[] codecOptions)
	{
		uLinkAngle2Extensions.int32Codec.serializer.Invoke(stream, ((Angle2)value).encoded, codecOptions);
	}

	// Token: 0x06001542 RID: 5442 RVA: 0x0004F914 File Offset: 0x0004DB14
	public static void Register()
	{
	}

	// Token: 0x04000A87 RID: 2695
	private const BitStreamTypeCode bitStreamTypeCode = 13;

	// Token: 0x04000A88 RID: 2696
	private static readonly BitStreamCodec int32Codec = BitStreamCodec.Find(typeof(int).TypeHandle);

	// Token: 0x04000A89 RID: 2697
	private static readonly BitStreamCodec.Deserializer deserializer;

	// Token: 0x04000A8A RID: 2698
	private static readonly BitStreamCodec.Serializer serializer = new BitStreamCodec.Serializer(uLinkAngle2Extensions.Serializer);
}
