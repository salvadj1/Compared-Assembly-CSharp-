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
	// Token: 0x02000238 RID: 568
	[DebuggerNonUserCode]
	public sealed class Blueprint : GeneratedMessage<RustProto.Blueprint, RustProto.Blueprint.Builder>
	{
		// Token: 0x060010D9 RID: 4313 RVA: 0x0003E604 File Offset: 0x0003C804
		private Blueprint()
		{
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x0003E614 File Offset: 0x0003C814
		static Blueprint()
		{
			object.ReferenceEquals(RustProto.Proto.Blueprint.Descriptor, null);
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x0003E654 File Offset: 0x0003C854
		public static RustProto.Helpers.Recycler<RustProto.Blueprint, RustProto.Blueprint.Builder> Recycler()
		{
			return RustProto.Helpers.Recycler<RustProto.Blueprint, RustProto.Blueprint.Builder>.Manufacture();
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x060010DC RID: 4316 RVA: 0x0003E65C File Offset: 0x0003C85C
		public static RustProto.Blueprint DefaultInstance
		{
			get
			{
				return RustProto.Blueprint.defaultInstance;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x060010DD RID: 4317 RVA: 0x0003E664 File Offset: 0x0003C864
		public override RustProto.Blueprint DefaultInstanceForType
		{
			get
			{
				return RustProto.Blueprint.DefaultInstance;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x060010DE RID: 4318 RVA: 0x0003E66C File Offset: 0x0003C86C
		protected override RustProto.Blueprint ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x060010DF RID: 4319 RVA: 0x0003E670 File Offset: 0x0003C870
		public static MessageDescriptor Descriptor
		{
			get
			{
				return RustProto.Proto.Blueprint.internal__static_RustProto_Blueprint__Descriptor;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x060010E0 RID: 4320 RVA: 0x0003E678 File Offset: 0x0003C878
		protected override FieldAccessorTable<RustProto.Blueprint, RustProto.Blueprint.Builder> InternalFieldAccessors
		{
			get
			{
				return RustProto.Proto.Blueprint.internal__static_RustProto_Blueprint__FieldAccessorTable;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x060010E1 RID: 4321 RVA: 0x0003E680 File Offset: 0x0003C880
		public bool HasId
		{
			get
			{
				return this.hasId;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x060010E2 RID: 4322 RVA: 0x0003E688 File Offset: 0x0003C888
		public int Id
		{
			get
			{
				return this.id_;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x060010E3 RID: 4323 RVA: 0x0003E690 File Offset: 0x0003C890
		public override bool IsInitialized
		{
			get
			{
				return this.hasId;
			}
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x0003E6A0 File Offset: 0x0003C8A0
		public override void WriteTo(ICodedOutputStream output)
		{
			int serializedSize = this.SerializedSize;
			string[] blueprintFieldNames = RustProto.Blueprint._blueprintFieldNames;
			if (this.hasId)
			{
				output.WriteInt32(1, blueprintFieldNames[0], this.Id);
			}
			this.UnknownFields.WriteTo(output);
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x060010E5 RID: 4325 RVA: 0x0003E6E4 File Offset: 0x0003C8E4
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
				num += this.UnknownFields.SerializedSize;
				this.memoizedSerializedSize = num;
				return num;
			}
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x0003E734 File Offset: 0x0003C934
		public static RustProto.Blueprint ParseFrom(ByteString data)
		{
			return RustProto.Blueprint.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x0003E748 File Offset: 0x0003C948
		public static RustProto.Blueprint ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return RustProto.Blueprint.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x0003E75C File Offset: 0x0003C95C
		public static RustProto.Blueprint ParseFrom(byte[] data)
		{
			return RustProto.Blueprint.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x0003E770 File Offset: 0x0003C970
		public static RustProto.Blueprint ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return RustProto.Blueprint.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x0003E784 File Offset: 0x0003C984
		public static RustProto.Blueprint ParseFrom(Stream input)
		{
			return RustProto.Blueprint.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x0003E798 File Offset: 0x0003C998
		public static RustProto.Blueprint ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return RustProto.Blueprint.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x0003E7AC File Offset: 0x0003C9AC
		public static RustProto.Blueprint ParseDelimitedFrom(Stream input)
		{
			return RustProto.Blueprint.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x0003E7C0 File Offset: 0x0003C9C0
		public static RustProto.Blueprint ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return RustProto.Blueprint.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x0003E7D4 File Offset: 0x0003C9D4
		public static RustProto.Blueprint ParseFrom(ICodedInputStream input)
		{
			return RustProto.Blueprint.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x0003E7E8 File Offset: 0x0003C9E8
		public static RustProto.Blueprint ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return RustProto.Blueprint.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x0003E7FC File Offset: 0x0003C9FC
		private RustProto.Blueprint MakeReadOnly()
		{
			return this;
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x0003E800 File Offset: 0x0003CA00
		public static RustProto.Blueprint.Builder CreateBuilder()
		{
			return new RustProto.Blueprint.Builder();
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x0003E808 File Offset: 0x0003CA08
		public override RustProto.Blueprint.Builder ToBuilder()
		{
			return RustProto.Blueprint.CreateBuilder(this);
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x0003E810 File Offset: 0x0003CA10
		public override RustProto.Blueprint.Builder CreateBuilderForType()
		{
			return new RustProto.Blueprint.Builder();
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x0003E818 File Offset: 0x0003CA18
		public static RustProto.Blueprint.Builder CreateBuilder(RustProto.Blueprint prototype)
		{
			return new RustProto.Blueprint.Builder(prototype);
		}

		// Token: 0x04000A15 RID: 2581
		public const int IdFieldNumber = 1;

		// Token: 0x04000A16 RID: 2582
		private static readonly RustProto.Blueprint defaultInstance = new RustProto.Blueprint().MakeReadOnly();

		// Token: 0x04000A17 RID: 2583
		private static readonly string[] _blueprintFieldNames = new string[]
		{
			"id"
		};

		// Token: 0x04000A18 RID: 2584
		private static readonly uint[] _blueprintFieldTags = new uint[]
		{
			8u
		};

		// Token: 0x04000A19 RID: 2585
		private bool hasId;

		// Token: 0x04000A1A RID: 2586
		private int id_;

		// Token: 0x04000A1B RID: 2587
		private int memoizedSerializedSize = -1;

		// Token: 0x02000239 RID: 569
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<RustProto.Blueprint, RustProto.Blueprint.Builder>
		{
			// Token: 0x060010F5 RID: 4341 RVA: 0x0003E820 File Offset: 0x0003CA20
			public Builder()
			{
				this.result = RustProto.Blueprint.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x060010F6 RID: 4342 RVA: 0x0003E83C File Offset: 0x0003CA3C
			internal Builder(RustProto.Blueprint cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x17000453 RID: 1107
			// (get) Token: 0x060010F7 RID: 4343 RVA: 0x0003E854 File Offset: 0x0003CA54
			protected override RustProto.Blueprint.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x060010F8 RID: 4344 RVA: 0x0003E858 File Offset: 0x0003CA58
			private RustProto.Blueprint PrepareBuilder()
			{
				if (this.resultIsReadOnly)
				{
					RustProto.Blueprint other = this.result;
					this.result = new RustProto.Blueprint();
					this.resultIsReadOnly = false;
					this.MergeFrom(other);
				}
				return this.result;
			}

			// Token: 0x17000454 RID: 1108
			// (get) Token: 0x060010F9 RID: 4345 RVA: 0x0003E898 File Offset: 0x0003CA98
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x17000455 RID: 1109
			// (get) Token: 0x060010FA RID: 4346 RVA: 0x0003E8A8 File Offset: 0x0003CAA8
			protected override RustProto.Blueprint MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x060010FB RID: 4347 RVA: 0x0003E8B0 File Offset: 0x0003CAB0
			public override RustProto.Blueprint.Builder Clear()
			{
				this.result = RustProto.Blueprint.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x060010FC RID: 4348 RVA: 0x0003E8C8 File Offset: 0x0003CAC8
			public override RustProto.Blueprint.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new RustProto.Blueprint.Builder(this.result);
				}
				return new RustProto.Blueprint.Builder().MergeFrom(this.result);
			}

			// Token: 0x17000456 RID: 1110
			// (get) Token: 0x060010FD RID: 4349 RVA: 0x0003E8F4 File Offset: 0x0003CAF4
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return RustProto.Blueprint.Descriptor;
				}
			}

			// Token: 0x17000457 RID: 1111
			// (get) Token: 0x060010FE RID: 4350 RVA: 0x0003E8FC File Offset: 0x0003CAFC
			public override RustProto.Blueprint DefaultInstanceForType
			{
				get
				{
					return RustProto.Blueprint.DefaultInstance;
				}
			}

			// Token: 0x060010FF RID: 4351 RVA: 0x0003E904 File Offset: 0x0003CB04
			public override RustProto.Blueprint BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x06001100 RID: 4352 RVA: 0x0003E938 File Offset: 0x0003CB38
			public override RustProto.Blueprint.Builder MergeFrom(IMessage other)
			{
				if (other is RustProto.Blueprint)
				{
					return this.MergeFrom((RustProto.Blueprint)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x06001101 RID: 4353 RVA: 0x0003E95C File Offset: 0x0003CB5C
			public override RustProto.Blueprint.Builder MergeFrom(RustProto.Blueprint other)
			{
				if (other == RustProto.Blueprint.DefaultInstance)
				{
					return this;
				}
				this.PrepareBuilder();
				if (other.HasId)
				{
					this.Id = other.Id;
				}
				this.MergeUnknownFields(other.UnknownFields);
				return this;
			}

			// Token: 0x06001102 RID: 4354 RVA: 0x0003E9A4 File Offset: 0x0003CBA4
			public override RustProto.Blueprint.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x06001103 RID: 4355 RVA: 0x0003E9B4 File Offset: 0x0003CBB4
			public override RustProto.Blueprint.Builder MergeFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
			{
				this.PrepareBuilder();
				UnknownFieldSet.Builder builder = null;
				uint num;
				string text;
				while (input.ReadTag(ref num, ref text))
				{
					if (num == 0u && text != null)
					{
						int num2 = Array.BinarySearch<string>(RustProto.Blueprint._blueprintFieldNames, text, StringComparer.Ordinal);
						if (num2 < 0)
						{
							if (builder == null)
							{
								builder = UnknownFieldSet.CreateBuilder(this.UnknownFields);
							}
							this.ParseUnknownField(input, builder, extensionRegistry, num, text);
							continue;
						}
						num = RustProto.Blueprint._blueprintFieldTags[num2];
					}
					uint num3 = num;
					if (num3 == 0u)
					{
						throw InvalidProtocolBufferException.InvalidTag();
					}
					if (num3 != 8u)
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
						this.result.hasId = input.ReadInt32(ref this.result.id_);
					}
				}
				if (builder != null)
				{
					this.UnknownFields = builder.Build();
				}
				return this;
			}

			// Token: 0x17000458 RID: 1112
			// (get) Token: 0x06001104 RID: 4356 RVA: 0x0003EAC8 File Offset: 0x0003CCC8
			public bool HasId
			{
				get
				{
					return this.result.hasId;
				}
			}

			// Token: 0x17000459 RID: 1113
			// (get) Token: 0x06001105 RID: 4357 RVA: 0x0003EAD8 File Offset: 0x0003CCD8
			// (set) Token: 0x06001106 RID: 4358 RVA: 0x0003EAE8 File Offset: 0x0003CCE8
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

			// Token: 0x06001107 RID: 4359 RVA: 0x0003EAF4 File Offset: 0x0003CCF4
			public RustProto.Blueprint.Builder SetId(int value)
			{
				this.PrepareBuilder();
				this.result.hasId = true;
				this.result.id_ = value;
				return this;
			}

			// Token: 0x06001108 RID: 4360 RVA: 0x0003EB24 File Offset: 0x0003CD24
			public RustProto.Blueprint.Builder ClearId()
			{
				this.PrepareBuilder();
				this.result.hasId = false;
				this.result.id_ = 0;
				return this;
			}

			// Token: 0x04000A1C RID: 2588
			private bool resultIsReadOnly;

			// Token: 0x04000A1D RID: 2589
			private RustProto.Blueprint result;
		}
	}
}
