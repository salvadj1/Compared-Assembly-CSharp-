using System;
using System.Collections.Generic;
using uLink;

// Token: 0x02000479 RID: 1145
internal class ContextMenuData
{
	// Token: 0x060028E8 RID: 10472 RVA: 0x000A0544 File Offset: 0x0009E744
	public ContextMenuData(int optionCount, ContextMenuItemData[] data)
	{
		this.options_length = optionCount;
		this.options = data;
	}

	// Token: 0x060028E9 RID: 10473 RVA: 0x000A055C File Offset: 0x0009E75C
	public ContextMenuData(IEnumerable<ContextActionPrototype> prototypeEnumerable)
	{
		if (prototypeEnumerable is ICollection<ContextActionPrototype>)
		{
			this.options = new ContextMenuItemData[((ICollection<ContextActionPrototype>)prototypeEnumerable).Count];
			int num = 0;
			foreach (ContextActionPrototype prototype in prototypeEnumerable)
			{
				this.options[num++] = new ContextMenuItemData(prototype);
			}
			if (num < this.options.Length)
			{
				Array.Resize<ContextMenuItemData>(ref this.options, this.options.Length);
			}
			this.options_length = this.options.Length;
		}
		else
		{
			this.options = ContextMenuData.ToArray(prototypeEnumerable, out this.options_length);
		}
	}

	// Token: 0x060028EA RID: 10474 RVA: 0x000A0640 File Offset: 0x0009E840
	static ContextMenuData()
	{
		BitStreamCodec.Add<ContextMenuData>(ContextMenuData.deserializer, ContextMenuData.serializer);
	}

	// Token: 0x060028EB RID: 10475 RVA: 0x000A0674 File Offset: 0x0009E874
	private static ContextMenuItemData[] ToArray(IEnumerable<ContextActionPrototype> enumerable, out int length)
	{
		ContextMenuItemData[] array;
		using (enumerable.GetEnumerator())
		{
			ContextMenuData.EnumerableConverter enumerableConverter = new ContextMenuData.EnumerableConverter
			{
				enumerator = enumerable.GetEnumerator()
			};
			enumerableConverter.R();
			length = enumerableConverter.length;
			array = enumerableConverter.array;
		}
		return array;
	}

	// Token: 0x060028EC RID: 10476 RVA: 0x000A06F0 File Offset: 0x0009E8F0
	private static void Serialize(BitStream stream, object value, params object[] codecOptions)
	{
		ContextMenuData contextMenuData = (ContextMenuData)value;
		stream.Write<int>(contextMenuData.options_length, codecOptions);
		for (int i = 0; i < contextMenuData.options_length; i++)
		{
			stream.Write<int>(contextMenuData.options[i].name, codecOptions);
			stream.WriteByteArray_MinimumCalls(contextMenuData.options[i].utf8_text, 0, contextMenuData.options[i].utf8_length, codecOptions);
		}
	}

	// Token: 0x060028ED RID: 10477 RVA: 0x000A076C File Offset: 0x0009E96C
	private static object Deserialize(BitStream stream, params object[] codecOptions)
	{
		int num = stream.Read<int>(codecOptions);
		ContextMenuItemData[] array = (num != 0) ? new ContextMenuItemData[num] : null;
		for (int i = 0; i < num; i++)
		{
			int name = stream.Read<int>(codecOptions);
			byte[] utf8_text;
			int utf8_length;
			stream.ReadByteArray_MinimalCalls(out utf8_text, out utf8_length, codecOptions);
			array[i] = new ContextMenuItemData(name, utf8_length, utf8_text);
		}
		return new ContextMenuData(num, array);
	}

	// Token: 0x040014FD RID: 5373
	[NonSerialized]
	public readonly int options_length;

	// Token: 0x040014FE RID: 5374
	[NonSerialized]
	public readonly ContextMenuItemData[] options;

	// Token: 0x040014FF RID: 5375
	private static readonly BitStreamCodec.Serializer serializer = new BitStreamCodec.Serializer(ContextMenuData.Serialize);

	// Token: 0x04001500 RID: 5376
	private static readonly BitStreamCodec.Deserializer deserializer = new BitStreamCodec.Deserializer(ContextMenuData.Deserialize);

	// Token: 0x0200047A RID: 1146
	private struct EnumerableConverter
	{
		// Token: 0x060028EE RID: 10478 RVA: 0x000A07D8 File Offset: 0x0009E9D8
		public void R()
		{
			if (this.enumerator.MoveNext())
			{
				this.length++;
				ContextActionPrototype prototype = this.enumerator.Current;
				this.R();
				this.array[--this.spot] = new ContextMenuItemData(prototype);
			}
			else if (this.length == 0)
			{
				this.array = null;
			}
			else
			{
				this.array = new ContextMenuItemData[this.length];
				this.spot = this.length;
			}
		}

		// Token: 0x04001501 RID: 5377
		public IEnumerator<ContextActionPrototype> enumerator;

		// Token: 0x04001502 RID: 5378
		public int length;

		// Token: 0x04001503 RID: 5379
		public int spot;

		// Token: 0x04001504 RID: 5380
		public ContextMenuItemData[] array;
	}
}
