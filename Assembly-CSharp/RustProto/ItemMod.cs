using System;
using System.Diagnostics;
using System.IO;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;
using RustProto.Helpers;
using RustProto.Proto;

namespace RustProto
{
	// Token: 0x02000203 RID: 515
	[DebuggerNonUserCode]
	public sealed class ItemMod : GeneratedMessage<ItemMod, ItemMod.Builder>
	{
		// Token: 0x06000F4E RID: 3918 RVA: 0x00039BD4 File Offset: 0x00037DD4
		private ItemMod()
		{
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x00039BF0 File Offset: 0x00037DF0
		static ItemMod()
		{
			object.ReferenceEquals(ItemMod.Descriptor, null);
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x00039C48 File Offset: 0x00037E48
		public static Recycler<ItemMod, ItemMod.Builder> Recycler()
		{
			return Recycler<ItemMod, ItemMod.Builder>.Manufacture();
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000F51 RID: 3921 RVA: 0x00039C50 File Offset: 0x00037E50
		public static ItemMod DefaultInstance
		{
			get
			{
				return ItemMod.defaultInstance;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000F52 RID: 3922 RVA: 0x00039C58 File Offset: 0x00037E58
		public override ItemMod DefaultInstanceForType
		{
			get
			{
				return ItemMod.DefaultInstance;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000F53 RID: 3923 RVA: 0x00039C60 File Offset: 0x00037E60
		protected override ItemMod ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000F54 RID: 3924 RVA: 0x00039C64 File Offset: 0x00037E64
		public static MessageDescriptor Descriptor
		{
			get
			{
				return ItemMod.internal__static_RustProto_ItemMod__Descriptor;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000F55 RID: 3925 RVA: 0x00039C6C File Offset: 0x00037E6C
		protected override FieldAccessorTable<ItemMod, ItemMod.Builder> InternalFieldAccessors
		{
			get
			{
				return ItemMod.internal__static_RustProto_ItemMod__FieldAccessorTable;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000F56 RID: 3926 RVA: 0x00039C74 File Offset: 0x00037E74
		public bool HasId
		{
			get
			{
				return this.hasId;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000F57 RID: 3927 RVA: 0x00039C7C File Offset: 0x00037E7C
		public int Id
		{
			get
			{
				return this.id_;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000F58 RID: 3928 RVA: 0x00039C84 File Offset: 0x00037E84
		public bool HasName
		{
			get
			{
				return this.hasName;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000F59 RID: 3929 RVA: 0x00039C8C File Offset: 0x00037E8C
		public string Name
		{
			get
			{
				return this.name_;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000F5A RID: 3930 RVA: 0x00039C94 File Offset: 0x00037E94
		public override bool IsInitialized
		{
			get
			{
				return this.hasId;
			}
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x00039CA4 File Offset: 0x00037EA4
		public override void WriteTo(ICodedOutputStream output)
		{
			int serializedSize = this.SerializedSize;
			string[] itemModFieldNames = ItemMod._itemModFieldNames;
			if (this.hasId)
			{
				output.WriteInt32(1, itemModFieldNames[0], this.Id);
			}
			if (this.hasName)
			{
				output.WriteString(2, itemModFieldNames[1], this.Name);
			}
			this.UnknownFields.WriteTo(output);
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000F5C RID: 3932 RVA: 0x00039D00 File Offset: 0x00037F00
		public override int SerializedSize
		{
			get
			{
				int num = this.memoizedSerializedSize;
				if (num != -1)
				{
					return num;
				}
				num = 0;
				if (this.hasId)
				{
					num += CodedOutputStream.ComputeInt32Size(1, this.Id);
				}
				if (this.hasName)
				{
					num += CodedOutputStream.ComputeStringSize(2, this.Name);
				}
				num += this.UnknownFields.SerializedSize;
				this.memoizedSerializedSize = num;
				return num;
			}
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x00039D6C File Offset: 0x00037F6C
		public static ItemMod ParseFrom(ByteString data)
		{
			return ItemMod.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x00039D80 File Offset: 0x00037F80
		public static ItemMod ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return ItemMod.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x00039D94 File Offset: 0x00037F94
		public static ItemMod ParseFrom(byte[] data)
		{
			return ItemMod.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x00039DA8 File Offset: 0x00037FA8
		public static ItemMod ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return ItemMod.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x00039DBC File Offset: 0x00037FBC
		public static ItemMod ParseFrom(Stream input)
		{
			return ItemMod.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x00039DD0 File Offset: 0x00037FD0
		public static ItemMod ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return ItemMod.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x00039DE4 File Offset: 0x00037FE4
		public static ItemMod ParseDelimitedFrom(Stream input)
		{
			return ItemMod.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x00039DF8 File Offset: 0x00037FF8
		public static ItemMod ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return ItemMod.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x00039E0C File Offset: 0x0003800C
		public static ItemMod ParseFrom(ICodedInputStream input)
		{
			return ItemMod.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x00039E20 File Offset: 0x00038020
		public static ItemMod ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return ItemMod.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x00039E34 File Offset: 0x00038034
		private ItemMod MakeReadOnly()
		{
			return this;
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x00039E38 File Offset: 0x00038038
		public static ItemMod.Builder CreateBuilder()
		{
			return new ItemMod.Builder();
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x00039E40 File Offset: 0x00038040
		public override ItemMod.Builder ToBuilder()
		{
			return ItemMod.CreateBuilder(this);
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x00039E48 File Offset: 0x00038048
		public override ItemMod.Builder CreateBuilderForType()
		{
			return new ItemMod.Builder();
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x00039E50 File Offset: 0x00038050
		public static ItemMod.Builder CreateBuilder(ItemMod prototype)
		{
			return new ItemMod.Builder(prototype);
		}

		// Token: 0x040008E6 RID: 2278
		public const int IdFieldNumber = 1;

		// Token: 0x040008E7 RID: 2279
		public const int NameFieldNumber = 2;

		// Token: 0x040008E8 RID: 2280
		private static readonly ItemMod defaultInstance = new ItemMod().MakeReadOnly();

		// Token: 0x040008E9 RID: 2281
		private static readonly string[] _itemModFieldNames = new string[]
		{
			"id",
			"name"
		};

		// Token: 0x040008EA RID: 2282
		private static readonly uint[] _itemModFieldTags = new uint[]
		{
			8u,
			18u
		};

		// Token: 0x040008EB RID: 2283
		private bool hasId;

		// Token: 0x040008EC RID: 2284
		private int id_;

		// Token: 0x040008ED RID: 2285
		private bool hasName;

		// Token: 0x040008EE RID: 2286
		private string name_ = string.Empty;

		// Token: 0x040008EF RID: 2287
		private int memoizedSerializedSize = -1;

		// Token: 0x02000204 RID: 516
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<ItemMod, ItemMod.Builder>
		{
			// Token: 0x06000F6C RID: 3948 RVA: 0x00039E58 File Offset: 0x00038058
			public Builder()
			{
				this.result = ItemMod.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x06000F6D RID: 3949 RVA: 0x00039E74 File Offset: 0x00038074
			internal Builder(ItemMod cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x170003F9 RID: 1017
			// (get) Token: 0x06000F6E RID: 3950 RVA: 0x00039E8C File Offset: 0x0003808C
			protected override ItemMod.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x06000F6F RID: 3951 RVA: 0x00039E90 File Offset: 0x00038090
			private ItemMod PrepareBuilder()
			{
				if (this.resultIsReadOnly)
				{
					ItemMod other = this.result;
					this.result = new ItemMod();
					this.resultIsReadOnly = false;
					this.MergeFrom(other);
				}
				return this.result;
			}

			// Token: 0x170003FA RID: 1018
			// (get) Token: 0x06000F70 RID: 3952 RVA: 0x00039ED0 File Offset: 0x000380D0
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x170003FB RID: 1019
			// (get) Token: 0x06000F71 RID: 3953 RVA: 0x00039EE0 File Offset: 0x000380E0
			protected override ItemMod MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x06000F72 RID: 3954 RVA: 0x00039EE8 File Offset: 0x000380E8
			public override ItemMod.Builder Clear()
			{
				this.result = ItemMod.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x06000F73 RID: 3955 RVA: 0x00039F00 File Offset: 0x00038100
			public override ItemMod.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new ItemMod.Builder(this.result);
				}
				return new ItemMod.Builder().MergeFrom(this.result);
			}

			// Token: 0x170003FC RID: 1020
			// (get) Token: 0x06000F74 RID: 3956 RVA: 0x00039F2C File Offset: 0x0003812C
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return ItemMod.Descriptor;
				}
			}

			// Token: 0x170003FD RID: 1021
			// (get) Token: 0x06000F75 RID: 3957 RVA: 0x00039F34 File Offset: 0x00038134
			public override ItemMod DefaultInstanceForType
			{
				get
				{
					return ItemMod.DefaultInstance;
				}
			}

			// Token: 0x06000F76 RID: 3958 RVA: 0x00039F3C File Offset: 0x0003813C
			public override ItemMod BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x06000F77 RID: 3959 RVA: 0x00039F70 File Offset: 0x00038170
			public override ItemMod.Builder MergeFrom(IMessage other)
			{
				if (other is ItemMod)
				{
					return this.MergeFrom((ItemMod)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x06000F78 RID: 3960 RVA: 0x00039F94 File Offset: 0x00038194
			public override ItemMod.Builder MergeFrom(ItemMod other)
			{
				if (other == ItemMod.DefaultInstance)
				{
					return this;
				}
				this.PrepareBuilder();
				if (other.HasId)
				{
					this.Id = other.Id;
				}
				if (other.HasName)
				{
					this.Name = other.Name;
				}
				this.MergeUnknownFields(other.UnknownFields);
				return this;
			}

			// Token: 0x06000F79 RID: 3961 RVA: 0x00039FF4 File Offset: 0x000381F4
			public override ItemMod.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x06000F7A RID: 3962 RVA: 0x0003A004 File Offset: 0x00038204
			public override ItemMod.Builder MergeFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
			{
				this.PrepareBuilder();
				UnknownFieldSet.Builder builder = null;
				uint num;
				string text;
				while (input.ReadTag(ref num, ref text))
				{
					if (num == 0u && text != null)
					{
						int num2 = Array.BinarySearch<string>(ItemMod._itemModFieldNames, text, StringComparer.Ordinal);
						if (num2 < 0)
						{
							if (builder == null)
							{
								builder = UnknownFieldSet.CreateBuilder(this.UnknownFields);
							}
							this.ParseUnknownField(input, builder, extensionRegistry, num, text);
							continue;
						}
						num = ItemMod._itemModFieldTags[num2];
					}
					uint num3 = num;
					if (num3 == 0u)
					{
						throw InvalidProtocolBufferException.InvalidTag();
					}
					if (num3 != 8u)
					{
						if (num3 != 18u)
						{
							if (WireFormat.IsEndGroupTag(num))
							{
								if (builder != null)
								{
									this.UnknownFields = builder.Build();
								}
								return this;
							}
							if (builder == null)
							{
								builder = UnknownFieldSet.CreateBuilder(this.UnknownFields);
							}
							this.ParseUnknownField(input, builder, extensionRegistry, num, text);
						}
						else
						{
							this.result.hasName = input.ReadString(ref this.result.name_);
						}
					}
					else
					{
						this.result.hasId = input.ReadInt32(ref this.result.id_);
					}
				}
				if (builder != null)
				{
					this.UnknownFields = builder.Build();
				}
				return this;
			}

			// Token: 0x170003FE RID: 1022
			// (get) Token: 0x06000F7B RID: 3963 RVA: 0x0003A140 File Offset: 0x00038340
			public bool HasId
			{
				get
				{
					return this.result.hasId;
				}
			}

			// Token: 0x170003FF RID: 1023
			// (get) Token: 0x06000F7C RID: 3964 RVA: 0x0003A150 File Offset: 0x00038350
			// (set) Token: 0x06000F7D RID: 3965 RVA: 0x0003A160 File Offset: 0x00038360
			public int Id
			{
				get
				{
					return this.result.Id;
				}
				set
				{
					this.SetId(value);
				}
			}

			// Token: 0x06000F7E RID: 3966 RVA: 0x0003A16C File Offset: 0x0003836C
			public ItemMod.Builder SetId(int value)
			{
				this.PrepareBuilder();
				this.result.hasId = true;
				this.result.id_ = value;
				return this;
			}

			// Token: 0x06000F7F RID: 3967 RVA: 0x0003A19C File Offset: 0x0003839C
			public ItemMod.Builder ClearId()
			{
				this.PrepareBuilder();
				this.result.hasId = false;
				this.result.id_ = 0;
				return this;
			}

			// Token: 0x17000400 RID: 1024
			// (get) Token: 0x06000F80 RID: 3968 RVA: 0x0003A1CC File Offset: 0x000383CC
			public bool HasName
			{
				get
				{
					return this.result.hasName;
				}
			}

			// Token: 0x17000401 RID: 1025
			// (get) Token: 0x06000F81 RID: 3969 RVA: 0x0003A1DC File Offset: 0x000383DC
			// (set) Token: 0x06000F82 RID: 3970 RVA: 0x0003A1EC File Offset: 0x000383EC
			public string Name
			{
				get
				{
					return this.result.Name;
				}
				set
				{
					this.SetName(value);
				}
			}

			// Token: 0x06000F83 RID: 3971 RVA: 0x0003A1F8 File Offset: 0x000383F8
			public ItemMod.Builder SetName(string value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasName = true;
				this.result.name_ = value;
				return this;
			}

			// Token: 0x06000F84 RID: 3972 RVA: 0x0003A228 File Offset: 0x00038428
			public ItemMod.Builder ClearName()
			{
				this.PrepareBuilder();
				this.result.hasName = false;
				this.result.name_ = string.Empty;
				return this;
			}

			// Token: 0x040008F0 RID: 2288
			private bool resultIsReadOnly;

			// Token: 0x040008F1 RID: 2289
			private ItemMod result;
		}
	}
}
