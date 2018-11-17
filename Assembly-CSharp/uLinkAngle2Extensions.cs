using System;
using uLink;

// Token: 0x0200026F RID: 623
public static class uLinkAngle2Extensions
{
	// Token: 0x06001690 RID: 5776 RVA: 0x00053B78 File Offset: 0x00051D78
	static uLinkAngle2Extensions()
	{
		global::uLinkAngle2Extensions.deserializer = new BitStreamCodec.Deserializer(global::uLinkAngle2Extensions.Deserializer);
		BitStreamCodec.Add<global::Angle2>(global::uLinkAngle2Extensions.deserializer, global::uLinkAngle2Extensions.serializer, 13, false);
	}

	// Token: 0x06001691 RID: 5777 RVA: 0x00053BD4 File Offset: 0x00051DD4
	public static void Serialize(this BitStream stream, ref global::Angle2 value, params object[] codecOptions)
	{
		int encoded = value.encoded;
		int num = encoded;
		stream.Serialize(ref num, codecOptions);
		if (num != encoded)
		{
			value.encoded = num;
		}
	}

	// Token: 0x06001692 RID: 5778 RVA: 0x00053C04 File Offset: 0x00051E04
	public static void WriteAngle2(this BitStream stream, global::Angle2 value)
	{
		stream.WriteInt32(value.encoded);
	}

	// Token: 0x06001693 RID: 5779 RVA: 0x00053C14 File Offset: 0x00051E14
	public static global::Angle2 ReadAngle2(this BitStream stream)
	{
		return new global::Angle2
		{
			encoded = stream.ReadInt32()
		};
	}

	// Token: 0x06001694 RID: 5780 RVA: 0x00053C3C File Offset: 0x00051E3C
	private static object Deserializer(BitStream stream, params object[] codecOptions)
	{
		object obj = global::uLinkAngle2Extensions.int32Codec.deserializer.Invoke(stream, codecOptions);
		if (obj is int)
		{
			return new global::Angle2
			{
				encoded = (int)obj
			};
		}
		return obj;
	}

	// Token: 0x06001695 RID: 5781 RVA: 0x00053C88 File Offset: 0x00051E88
	private static void Serializer(BitStream stream, object value, params object[] codecOptions)
	{
		global::uLinkAngle2Extensions.int32Codec.serializer.Invoke(stream, ((global::Angle2)value).encoded, codecOptions);
	}

	// Token: 0x06001696 RID: 5782 RVA: 0x00053CBC File Offset: 0x00051EBC
	public static void Register()
	{
	}

	// Token: 0x04000BAA RID: 2986
	private const BitStreamTypeCode bitStreamTypeCode = 13;

	// Token: 0x04000BAB RID: 2987
	private static readonly BitStreamCodec int32Codec = BitStreamCodec.Find(typeof(int).TypeHandle);

	// Token: 0x04000BAC RID: 2988
	private static readonly BitStreamCodec.Deserializer deserializer;

	// Token: 0x04000BAD RID: 2989
	private static readonly BitStreamCodec.Serializer serializer = new BitStreamCodec.Serializer(global::uLinkAngle2Extensions.Serializer);
}
