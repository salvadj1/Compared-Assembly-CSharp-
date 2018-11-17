using System;
using System.Collections.Generic;
using uLink;

// Token: 0x0200052F RID: 1327
internal class ContextMenuData
{
	// Token: 0x06002C78 RID: 11384 RVA: 0x000A64C4 File Offset: 0x000A46C4
	public ContextMenuData(int optionCount, ContextMenuItemData[] data)
	{
		this.options_length = optionCount;
		this.options = data;
	}

	// Token: 0x06002C79 RID: 11385 RVA: 0x000A64DC File Offset: 0x000A46DC
	public ContextMenuData(IEnumerable<global::ContextActionPrototype> prototypeEnumerable)
	{
		if (prototypeEnumerable is ICollection<global::ContextActionPrototype>)
		{
			this.options = new ContextMenuItemData[((ICollection<global::ContextActionPrototype>)prototypeEnumerable).Count];
			int num = 0;
			foreach (global::ContextActionPrototype prototype in prototypeEnumerable)
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

	// Token: 0x06002C7A RID: 11386 RVA: 0x000A65C0 File Offset: 0x000A47C0
	static ContextMenuData()
	{
		BitStreamCodec.Add<ContextMenuData>(ContextMenuData.deserializer, ContextMenuData.serializer);
	}

	// Token: 0x06002C7B RID: 11387 RVA: 0x000A65F4 File Offset: 0x000A47F4
	private static ContextMenuItemData[] ToArray(IEnumerable<global::ContextActionPrototype> enumerable, out int length)
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

	// Token: 0x06002C7C RID: 11388 RVA: 0x000A6670 File Offset: 0x000A4870
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

	// Token: 0x06002C7D RID: 11389 RVA: 0x000A66EC File Offset: 0x000A48EC
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

	// Token: 0x04001680 RID: 5760
	[NonSerialized]
	public readonly int options_length;

	// Token: 0x04001681 RID: 5761
	[NonSerialized]
	public readonly ContextMenuItemData[] options;

	// Token: 0x04001682 RID: 5762
	private static readonly BitStreamCodec.Serializer serializer = new BitStreamCodec.Serializer(ContextMenuData.Serialize);

	// Token: 0x04001683 RID: 5763
	private static readonly BitStreamCodec.Deserializer deserializer = new BitStreamCodec.Deserializer(ContextMenuData.Deserialize);

	// Token: 0x02000530 RID: 1328
	private struct EnumerableConverter
	{
		// Token: 0x06002C7E RID: 11390 RVA: 0x000A6758 File Offset: 0x000A4958
		public void R()
		{
			if (this.enumerator.MoveNext())
			{
				this.length++;
				global::ContextActionPrototype prototype = this.enumerator.Current;
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

		// Token: 0x04001684 RID: 5764
		public IEnumerator<global::ContextActionPrototype> enumerator;

		// Token: 0x04001685 RID: 5765
		public int length;

		// Token: 0x04001686 RID: 5766
		public int spot;

		// Token: 0x04001687 RID: 5767
		public ContextMenuItemData[] array;
	}
}
