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
	// Token: 0x02000236 RID: 566
	[DebuggerNonUserCode]
	public sealed class ItemMod : GeneratedMessage<RustProto.ItemMod, RustProto.ItemMod.Builder>
	{
		// Token: 0x060010A2 RID: 4258 RVA: 0x0003DF7C File Offset: 0x0003C17C
		private ItemMod()
		{
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x0003DF98 File Offset: 0x0003C198
		static ItemMod()
		{
			object.ReferenceEquals(RustProto.Proto.ItemMod.Descriptor, null);
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x0003DFF0 File Offset: 0x0003C1F0
		public static RustProto.Helpers.Recycler<RustProto.ItemMod, RustProto.ItemMod.Builder> Recycler()
		{
			return RustProto.Helpers.Recycler<RustProto.ItemMod, RustProto.ItemMod.Builder>.Manufacture();
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x060010A5 RID: 4261 RVA: 0x0003DFF8 File Offset: 0x0003C1F8
		public static RustProto.ItemMod DefaultInstance
		{
			get
			{
				return RustProto.ItemMod.defaultInstance;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x060010A6 RID: 4262 RVA: 0x0003E000 File Offset: 0x0003C200
		public override RustProto.ItemMod DefaultInstanceForType
		{
			get
			{
				return RustProto.ItemMod.DefaultInstance;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x060010A7 RID: 4263 RVA: 0x0003E008 File Offset: 0x0003C208
		protected override RustProto.ItemMod ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x060010A8 RID: 4264 RVA: 0x0003E00C File Offset: 0x0003C20C
		public static MessageDescriptor Descriptor
		{
			get
			{
				return RustProto.Proto.ItemMod.internal__static_RustProto_ItemMod__Descriptor;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x060010A9 RID: 4265 RVA: 0x0003E014 File Offset: 0x0003C214
		protected override FieldAccessorTable<RustProto.ItemMod, RustProto.ItemMod.Builder> InternalFieldAccessors
		{
			get
			{
				return RustProto.Proto.ItemMod.internal__static_RustProto_ItemMod__FieldAccessorTable;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x060010AA RID: 4266 RVA: 0x0003E01C File Offset: 0x0003C21C
		public bool HasId
		{
			get
			{
				return this.hasId;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x060010AB RID: 4267 RVA: 0x0003E024 File Offset: 0x0003C224
		public int Id
		{
			get
			{
				return this.id_;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x060010AC RID: 4268 RVA: 0x0003E02C File Offset: 0x0003C22C
		public bool HasName
		{
			get
			{
				return this.hasName;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x060010AD RID: 4269 RVA: 0x0003E034 File Offset: 0x0003C234
		public string Name
		{
			get
			{
				return this.name_;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x060010AE RID: 4270 RVA: 0x0003E03C File Offset: 0x0003C23C
		public override bool IsInitialized
		{
			get
			{
				return this.hasId;
			}
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x0003E04C File Offset: 0x0003C24C
		public override void WriteTo(ICodedOutputStream output)
		{
			int serializedSize = this.SerializedSize;
			string[] itemModFieldNames = RustProto.ItemMod._itemModFieldNames;
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

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x060010B0 RID: 4272 RVA: 0x0003E0A8 File Offset: 0x0003C2A8
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

		// Token: 0x060010B1 RID: 4273 RVA: 0x0003E114 File Offset: 0x0003C314
		public static RustProto.ItemMod ParseFrom(ByteString data)
		{
			return RustProto.ItemMod.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x0003E128 File Offset: 0x0003C328
		public static RustProto.ItemMod ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return RustProto.ItemMod.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x0003E13C File Offset: 0x0003C33C
		public static RustProto.ItemMod ParseFrom(byte[] data)
		{
			return RustProto.ItemMod.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x0003E150 File Offset: 0x0003C350
		public static RustProto.ItemMod ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return RustProto.ItemMod.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x0003E164 File Offset: 0x0003C364
		public static RustProto.ItemMod ParseFrom(Stream input)
		{
			return RustProto.ItemMod.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x0003E178 File Offset: 0x0003C378
		public static RustProto.ItemMod ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return RustProto.ItemMod.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x0003E18C File Offset: 0x0003C38C
		public static RustProto.ItemMod ParseDelimitedFrom(Stream input)
		{
			return RustProto.ItemMod.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x0003E1A0 File Offset: 0x0003C3A0
		public static RustProto.ItemMod ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return RustProto.ItemMod.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x0003E1B4 File Offset: 0x0003C3B4
		public static RustProto.ItemMod ParseFrom(ICodedInputStream input)
		{
			return RustProto.ItemMod.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x0003E1C8 File Offset: 0x0003C3C8
		public static RustProto.ItemMod ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return RustProto.ItemMod.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x0003E1DC File Offset: 0x0003C3DC
		private RustProto.ItemMod MakeReadOnly()
		{
			return this;
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x0003E1E0 File Offset: 0x0003C3E0
		public static RustProto.ItemMod.Builder CreateBuilder()
		{
			return new RustProto.ItemMod.Builder();
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x0003E1E8 File Offset: 0x0003C3E8
		public override RustProto.ItemMod.Builder ToBuilder()
		{
			return RustProto.ItemMod.CreateBuilder(this);
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x0003E1F0 File Offset: 0x0003C3F0
		public override RustProto.ItemMod.Builder CreateBuilderForType()
		{
			return new RustProto.ItemMod.Builder();
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x0003E1F8 File Offset: 0x0003C3F8
		public static RustProto.ItemMod.Builder CreateBuilder(RustProto.ItemMod prototype)
		{
			return new RustProto.ItemMod.Builder(prototype);
		}

		// Token: 0x04000A09 RID: 2569
		public const int IdFieldNumber = 1;

		// Token: 0x04000A0A RID: 2570
		public const int NameFieldNumber = 2;

		// Token: 0x04000A0B RID: 2571
		private static readonly RustProto.ItemMod defaultInstance = new RustProto.ItemMod().MakeReadOnly();

		// Token: 0x04000A0C RID: 2572
		private static readonly string[] _itemModFieldNames = new string[]
		{
			"id",
			"name"
		};

		// Token: 0x04000A0D RID: 2573
		private static readonly uint[] _itemModFieldTags = new uint[]
		{
			8u,
			18u
		};

		// Token: 0x04000A0E RID: 2574
		private bool hasId;

		// Token: 0x04000A0F RID: 2575
		private int id_;

		// Token: 0x04000A10 RID: 2576
		private bool hasName;

		// Token: 0x04000A11 RID: 2577
		private string name_ = string.Empty;

		// Token: 0x04000A12 RID: 2578
		private int memoizedSerializedSize = -1;

		// Token: 0x02000237 RID: 567
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<RustProto.ItemMod, RustProto.ItemMod.Builder>
		{
			// Token: 0x060010C0 RID: 4288 RVA: 0x0003E200 File Offset: 0x0003C400
			public Builder()
			{
				this.result = RustProto.ItemMod.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x060010C1 RID: 4289 RVA: 0x0003E21C File Offset: 0x0003C41C
			internal Builder(RustProto.ItemMod cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x17000441 RID: 1089
			// (get) Token: 0x060010C2 RID: 4290 RVA: 0x0003E234 File Offset: 0x0003C434
			protected override RustProto.ItemMod.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x060010C3 RID: 4291 RVA: 0x0003E238 File Offset: 0x0003C438
			private RustProto.ItemMod PrepareBuilder()
			{
				if (this.resultIsReadOnly)
				{
					RustProto.ItemMod other = this.result;
					this.result = new RustProto.ItemMod();
					this.resultIsReadOnly = false;
					this.MergeFrom(other);
				}
				return this.result;
			}

			// Token: 0x17000442 RID: 1090
			// (get) Token: 0x060010C4 RID: 4292 RVA: 0x0003E278 File Offset: 0x0003C478
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x17000443 RID: 1091
			// (get) Token: 0x060010C5 RID: 4293 RVA: 0x0003E288 File Offset: 0x0003C488
			protected override RustProto.ItemMod MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x060010C6 RID: 4294 RVA: 0x0003E290 File Offset: 0x0003C490
			public override RustProto.ItemMod.Builder Clear()
			{
				this.result = RustProto.ItemMod.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x060010C7 RID: 4295 RVA: 0x0003E2A8 File Offset: 0x0003C4A8
			public override RustProto.ItemMod.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new RustProto.ItemMod.Builder(this.result);
				}
				return new RustProto.ItemMod.Builder().MergeFrom(this.result);
			}

			// Token: 0x17000444 RID: 1092
			// (get) Token: 0x060010C8 RID: 4296 RVA: 0x0003E2D4 File Offset: 0x0003C4D4
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return RustProto.ItemMod.Descriptor;
				}
			}

			// Token: 0x17000445 RID: 1093
			// (get) Token: 0x060010C9 RID: 4297 RVA: 0x0003E2DC File Offset: 0x0003C4DC
			public override RustProto.ItemMod DefaultInstanceForType
			{
				get
				{
					return RustProto.ItemMod.DefaultInstance;
				}
			}

			// Token: 0x060010CA RID: 4298 RVA: 0x0003E2E4 File Offset: 0x0003C4E4
			public override RustProto.ItemMod BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x060010CB RID: 4299 RVA: 0x0003E318 File Offset: 0x0003C518
			public override RustProto.ItemMod.Builder MergeFrom(IMessage other)
			{
				if (other is RustProto.ItemMod)
				{
					return this.MergeFrom((RustProto.ItemMod)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x060010CC RID: 4300 RVA: 0x0003E33C File Offset: 0x0003C53C
			public override RustProto.ItemMod.Builder MergeFrom(RustProto.ItemMod other)
			{
				if (other == RustProto.ItemMod.DefaultInstance)
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

			// Token: 0x060010CD RID: 4301 RVA: 0x0003E39C File Offset: 0x0003C59C
			public override RustProto.ItemMod.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x060010CE RID: 4302 RVA: 0x0003E3AC File Offset: 0x0003C5AC
			public override RustProto.ItemMod.Builder MergeFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
			{
				this.PrepareBuilder();
				UnknownFieldSet.Builder builder = null;
				uint num;
				string text;
				while (input.ReadTag(ref num, ref text))
				{
					if (num == 0u && text != null)
					{
						int num2 = Array.BinarySearch<string>(RustProto.ItemMod._itemModFieldNames, text, StringComparer.Ordinal);
						if (num2 < 0)
						{
							if (builder == null)
							{
								builder = UnknownFieldSet.CreateBuilder(this.UnknownFields);
							}
							this.ParseUnknownField(input, builder, extensionRegistry, num, text);
							continue;
						}
						num = RustProto.ItemMod._itemModFieldTags[num2];
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

			// Token: 0x17000446 RID: 1094
			// (get) Token: 0x060010CF RID: 4303 RVA: 0x0003E4E8 File Offset: 0x0003C6E8
			public bool HasId
			{
				get
				{
					return this.result.hasId;
				}
			}

			// Token: 0x17000447 RID: 1095
			// (get) Token: 0x060010D0 RID: 4304 RVA: 0x0003E4F8 File Offset: 0x0003C6F8
			// (set) Token: 0x060010D1 RID: 4305 RVA: 0x0003E508 File Offset: 0x0003C708
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

			// Token: 0x060010D2 RID: 4306 RVA: 0x0003E514 File Offset: 0x0003C714
			public RustProto.ItemMod.Builder SetId(int value)
			{
				this.PrepareBuilder();
				this.result.hasId = true;
				this.result.id_ = value;
				return this;
			}

			// Token: 0x060010D3 RID: 4307 RVA: 0x0003E544 File Offset: 0x0003C744
			public RustProto.ItemMod.Builder ClearId()
			{
				this.PrepareBuilder();
				this.result.hasId = false;
				this.result.id_ = 0;
				return this;
			}

			// Token: 0x17000448 RID: 1096
			// (get) Token: 0x060010D4 RID: 4308 RVA: 0x0003E574 File Offset: 0x0003C774
			public bool HasName
			{
				get
				{
					return this.result.hasName;
				}
			}

			// Token: 0x17000449 RID: 1097
			// (get) Token: 0x060010D5 RID: 4309 RVA: 0x0003E584 File Offset: 0x0003C784
			// (set) Token: 0x060010D6 RID: 4310 RVA: 0x0003E594 File Offset: 0x0003C794
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

			// Token: 0x060010D7 RID: 4311 RVA: 0x0003E5A0 File Offset: 0x0003C7A0
			public RustProto.ItemMod.Builder SetName(string value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasName = true;
				this.result.name_ = value;
				return this;
			}

			// Token: 0x060010D8 RID: 4312 RVA: 0x0003E5D0 File Offset: 0x0003C7D0
			public RustProto.ItemMod.Builder ClearName()
			{
				this.PrepareBuilder();
				this.result.hasName = false;
				this.result.name_ = string.Empty;
				return this;
			}

			// Token: 0x04000A13 RID: 2579
			private bool resultIsReadOnly;

			// Token: 0x04000A14 RID: 2580
			private RustProto.ItemMod result;
		}
	}
}
