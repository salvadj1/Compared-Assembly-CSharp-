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
	// Token: 0x02000258 RID: 600
	[DebuggerNonUserCode]
	public sealed class AwayEvent : GeneratedMessage<AwayEvent, AwayEvent.Builder>
	{
		// Token: 0x0600151F RID: 5407 RVA: 0x00047B88 File Offset: 0x00045D88
		private AwayEvent()
		{
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x00047B98 File Offset: 0x00045D98
		static AwayEvent()
		{
			object.ReferenceEquals(RustProto.Proto.Avatar.Descriptor, null);
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x00047BFC File Offset: 0x00045DFC
		public static RustProto.Helpers.Recycler<AwayEvent, AwayEvent.Builder> Recycler()
		{
			return RustProto.Helpers.Recycler<AwayEvent, AwayEvent.Builder>.Manufacture();
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06001522 RID: 5410 RVA: 0x00047C04 File Offset: 0x00045E04
		public static AwayEvent DefaultInstance
		{
			get
			{
				return AwayEvent.defaultInstance;
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06001523 RID: 5411 RVA: 0x00047C0C File Offset: 0x00045E0C
		public override AwayEvent DefaultInstanceForType
		{
			get
			{
				return AwayEvent.DefaultInstance;
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001524 RID: 5412 RVA: 0x00047C14 File Offset: 0x00045E14
		protected override AwayEvent ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06001525 RID: 5413 RVA: 0x00047C18 File Offset: 0x00045E18
		public static MessageDescriptor Descriptor
		{
			get
			{
				return RustProto.Proto.Avatar.internal__static_RustProto_AwayEvent__Descriptor;
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06001526 RID: 5414 RVA: 0x00047C20 File Offset: 0x00045E20
		protected override FieldAccessorTable<AwayEvent, AwayEvent.Builder> InternalFieldAccessors
		{
			get
			{
				return RustProto.Proto.Avatar.internal__static_RustProto_AwayEvent__FieldAccessorTable;
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06001527 RID: 5415 RVA: 0x00047C28 File Offset: 0x00045E28
		public bool HasType
		{
			get
			{
				return this.hasType;
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06001528 RID: 5416 RVA: 0x00047C30 File Offset: 0x00045E30
		public AwayEvent.Types.AwayEventType Type
		{
			get
			{
				return this.type_;
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06001529 RID: 5417 RVA: 0x00047C38 File Offset: 0x00045E38
		public bool HasTimestamp
		{
			get
			{
				return this.hasTimestamp;
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x0600152A RID: 5418 RVA: 0x00047C40 File Offset: 0x00045E40
		public int Timestamp
		{
			get
			{
				return this.timestamp_;
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x0600152B RID: 5419 RVA: 0x00047C48 File Offset: 0x00045E48
		public bool HasInstigator
		{
			get
			{
				return this.hasInstigator;
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x0600152C RID: 5420 RVA: 0x00047C50 File Offset: 0x00045E50
		[CLSCompliant(false)]
		public ulong Instigator
		{
			get
			{
				return this.instigator_;
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x0600152D RID: 5421 RVA: 0x00047C58 File Offset: 0x00045E58
		public override bool IsInitialized
		{
			get
			{
				return this.hasType && this.hasTimestamp;
			}
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x00047C78 File Offset: 0x00045E78
		public override void WriteTo(ICodedOutputStream output)
		{
			int serializedSize = this.SerializedSize;
			string[] awayEventFieldNames = AwayEvent._awayEventFieldNames;
			if (this.hasType)
			{
				output.WriteEnum(1, awayEventFieldNames[2], (int)this.Type, this.Type);
			}
			if (this.hasTimestamp)
			{
				output.WriteInt32(2, awayEventFieldNames[1], this.Timestamp);
			}
			if (this.hasInstigator)
			{
				output.WriteUInt64(3, awayEventFieldNames[0], this.Instigator);
			}
			this.UnknownFields.WriteTo(output);
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x0600152F RID: 5423 RVA: 0x00047CFC File Offset: 0x00045EFC
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
				if (this.hasType)
				{
					num += CodedOutputStream.ComputeEnumSize(1, (int)this.Type);
				}
				if (this.hasTimestamp)
				{
					num += CodedOutputStream.ComputeInt32Size(2, this.Timestamp);
				}
				if (this.hasInstigator)
				{
					num += CodedOutputStream.ComputeUInt64Size(3, this.Instigator);
				}
				num += this.UnknownFields.SerializedSize;
				this.memoizedSerializedSize = num;
				return num;
			}
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x00047D80 File Offset: 0x00045F80
		public static AwayEvent ParseFrom(ByteString data)
		{
			return AwayEvent.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x00047D94 File Offset: 0x00045F94
		public static AwayEvent ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return AwayEvent.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x00047DA8 File Offset: 0x00045FA8
		public static AwayEvent ParseFrom(byte[] data)
		{
			return AwayEvent.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x00047DBC File Offset: 0x00045FBC
		public static AwayEvent ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return AwayEvent.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x00047DD0 File Offset: 0x00045FD0
		public static AwayEvent ParseFrom(Stream input)
		{
			return AwayEvent.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x00047DE4 File Offset: 0x00045FE4
		public static AwayEvent ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return AwayEvent.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x00047DF8 File Offset: 0x00045FF8
		public static AwayEvent ParseDelimitedFrom(Stream input)
		{
			return AwayEvent.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x00047E0C File Offset: 0x0004600C
		public static AwayEvent ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return AwayEvent.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001538 RID: 5432 RVA: 0x00047E20 File Offset: 0x00046020
		public static AwayEvent ParseFrom(ICodedInputStream input)
		{
			return AwayEvent.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x00047E34 File Offset: 0x00046034
		public static AwayEvent ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return AwayEvent.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x00047E48 File Offset: 0x00046048
		private AwayEvent MakeReadOnly()
		{
			return this;
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x00047E4C File Offset: 0x0004604C
		public static AwayEvent.Builder CreateBuilder()
		{
			return new AwayEvent.Builder();
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x00047E54 File Offset: 0x00046054
		public override AwayEvent.Builder ToBuilder()
		{
			return AwayEvent.CreateBuilder(this);
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x00047E5C File Offset: 0x0004605C
		public override AwayEvent.Builder CreateBuilderForType()
		{
			return new AwayEvent.Builder();
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x00047E64 File Offset: 0x00046064
		public static AwayEvent.Builder CreateBuilder(AwayEvent prototype)
		{
			return new AwayEvent.Builder(prototype);
		}

		// Token: 0x04000B1B RID: 2843
		public const int TypeFieldNumber = 1;

		// Token: 0x04000B1C RID: 2844
		public const int TimestampFieldNumber = 2;

		// Token: 0x04000B1D RID: 2845
		public const int InstigatorFieldNumber = 3;

		// Token: 0x04000B1E RID: 2846
		private static readonly AwayEvent defaultInstance = new AwayEvent().MakeReadOnly();

		// Token: 0x04000B1F RID: 2847
		private static readonly string[] _awayEventFieldNames = new string[]
		{
			"instigator",
			"timestamp",
			"type"
		};

		// Token: 0x04000B20 RID: 2848
		private static readonly uint[] _awayEventFieldTags = new uint[]
		{
			24u,
			16u,
			8u
		};

		// Token: 0x04000B21 RID: 2849
		private bool hasType;

		// Token: 0x04000B22 RID: 2850
		private AwayEvent.Types.AwayEventType type_;

		// Token: 0x04000B23 RID: 2851
		private bool hasTimestamp;

		// Token: 0x04000B24 RID: 2852
		private int timestamp_;

		// Token: 0x04000B25 RID: 2853
		private bool hasInstigator;

		// Token: 0x04000B26 RID: 2854
		private ulong instigator_;

		// Token: 0x04000B27 RID: 2855
		private int memoizedSerializedSize = -1;

		// Token: 0x02000259 RID: 601
		[DebuggerNonUserCode]
		public static class Types
		{
			// Token: 0x0200025A RID: 602
			public enum AwayEventType
			{
				// Token: 0x04000B29 RID: 2857
				UNKNOWN,
				// Token: 0x04000B2A RID: 2858
				SLUMBER,
				// Token: 0x04000B2B RID: 2859
				DIED
			}
		}

		// Token: 0x0200025B RID: 603
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<AwayEvent, AwayEvent.Builder>
		{
			// Token: 0x0600153F RID: 5439 RVA: 0x00047E6C File Offset: 0x0004606C
			public Builder()
			{
				this.result = AwayEvent.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x06001540 RID: 5440 RVA: 0x00047E88 File Offset: 0x00046088
			internal Builder(AwayEvent cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x170005F7 RID: 1527
			// (get) Token: 0x06001541 RID: 5441 RVA: 0x00047EA0 File Offset: 0x000460A0
			protected override AwayEvent.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x06001542 RID: 5442 RVA: 0x00047EA4 File Offset: 0x000460A4
			private AwayEvent PrepareBuilder()
			{
				if (this.resultIsReadOnly)
				{
					AwayEvent other = this.result;
					this.result = new AwayEvent();
					this.resultIsReadOnly = false;
					this.MergeFrom(other);
				}
				return this.result;
			}

			// Token: 0x170005F8 RID: 1528
			// (get) Token: 0x06001543 RID: 5443 RVA: 0x00047EE4 File Offset: 0x000460E4
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x170005F9 RID: 1529
			// (get) Token: 0x06001544 RID: 5444 RVA: 0x00047EF4 File Offset: 0x000460F4
			protected override AwayEvent MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x06001545 RID: 5445 RVA: 0x00047EFC File Offset: 0x000460FC
			public override AwayEvent.Builder Clear()
			{
				this.result = AwayEvent.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x06001546 RID: 5446 RVA: 0x00047F14 File Offset: 0x00046114
			public override AwayEvent.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new AwayEvent.Builder(this.result);
				}
				return new AwayEvent.Builder().MergeFrom(this.result);
			}

			// Token: 0x170005FA RID: 1530
			// (get) Token: 0x06001547 RID: 5447 RVA: 0x00047F40 File Offset: 0x00046140
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return AwayEvent.Descriptor;
				}
			}

			// Token: 0x170005FB RID: 1531
			// (get) Token: 0x06001548 RID: 5448 RVA: 0x00047F48 File Offset: 0x00046148
			public override AwayEvent DefaultInstanceForType
			{
				get
				{
					return AwayEvent.DefaultInstance;
				}
			}

			// Token: 0x06001549 RID: 5449 RVA: 0x00047F50 File Offset: 0x00046150
			public override AwayEvent BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x0600154A RID: 5450 RVA: 0x00047F84 File Offset: 0x00046184
			public override AwayEvent.Builder MergeFrom(IMessage other)
			{
				if (other is AwayEvent)
				{
					return this.MergeFrom((AwayEvent)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x0600154B RID: 5451 RVA: 0x00047FA8 File Offset: 0x000461A8
			public override AwayEvent.Builder MergeFrom(AwayEvent other)
			{
				if (other == AwayEvent.DefaultInstance)
				{
					return this;
				}
				this.PrepareBuilder();
				if (other.HasType)
				{
					this.Type = other.Type;
				}
				if (other.HasTimestamp)
				{
					this.Timestamp = other.Timestamp;
				}
				if (other.HasInstigator)
				{
					this.Instigator = other.Instigator;
				}
				this.MergeUnknownFields(other.UnknownFields);
				return this;
			}

			// Token: 0x0600154C RID: 5452 RVA: 0x0004801C File Offset: 0x0004621C
			public override AwayEvent.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x0600154D RID: 5453 RVA: 0x0004802C File Offset: 0x0004622C
			public override AwayEvent.Builder MergeFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
			{
				this.PrepareBuilder();
				UnknownFieldSet.Builder builder = null;
				uint num;
				string text;
				while (input.ReadTag(ref num, ref text))
				{
					if (num == 0u && text != null)
					{
						int num2 = Array.BinarySearch<string>(AwayEvent._awayEventFieldNames, text, StringComparer.Ordinal);
						if (num2 < 0)
						{
							if (builder == null)
							{
								builder = UnknownFieldSet.CreateBuilder(this.UnknownFields);
							}
							this.ParseUnknownField(input, builder, extensionRegistry, num, text);
							continue;
						}
						num = AwayEvent._awayEventFieldTags[num2];
					}
					uint num3 = num;
					if (num3 == 0u)
					{
						throw InvalidProtocolBufferException.InvalidTag();
					}
					object obj;
					if (num3 != 8u)
					{
						if (num3 != 16u)
						{
							if (num3 != 24u)
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
								this.result.hasInstigator = input.ReadUInt64(ref this.result.instigator_);
							}
						}
						else
						{
							this.result.hasTimestamp = input.ReadInt32(ref this.result.timestamp_);
						}
					}
					else if (input.ReadEnum<AwayEvent.Types.AwayEventType>(ref this.result.type_, ref obj))
					{
						this.result.hasType = true;
					}
					else if (obj is int)
					{
						if (builder == null)
						{
							builder = UnknownFieldSet.CreateBuilder(this.UnknownFields);
						}
						builder.MergeVarintField(1, (ulong)((long)((int)obj)));
					}
				}
				if (builder != null)
				{
					this.UnknownFields = builder.Build();
				}
				return this;
			}

			// Token: 0x170005FC RID: 1532
			// (get) Token: 0x0600154E RID: 5454 RVA: 0x000481D0 File Offset: 0x000463D0
			public bool HasType
			{
				get
				{
					return this.result.hasType;
				}
			}

			// Token: 0x170005FD RID: 1533
			// (get) Token: 0x0600154F RID: 5455 RVA: 0x000481E0 File Offset: 0x000463E0
			// (set) Token: 0x06001550 RID: 5456 RVA: 0x000481F0 File Offset: 0x000463F0
			public AwayEvent.Types.AwayEventType Type
			{
				get
				{
					return this.result.Type;
				}
				set
				{
					this.SetType(value);
				}
			}

			// Token: 0x06001551 RID: 5457 RVA: 0x000481FC File Offset: 0x000463FC
			public AwayEvent.Builder SetType(AwayEvent.Types.AwayEventType value)
			{
				this.PrepareBuilder();
				this.result.hasType = true;
				this.result.type_ = value;
				return this;
			}

			// Token: 0x06001552 RID: 5458 RVA: 0x0004822C File Offset: 0x0004642C
			public AwayEvent.Builder ClearType()
			{
				this.PrepareBuilder();
				this.result.hasType = false;
				this.result.type_ = AwayEvent.Types.AwayEventType.UNKNOWN;
				return this;
			}

			// Token: 0x170005FE RID: 1534
			// (get) Token: 0x06001553 RID: 5459 RVA: 0x0004825C File Offset: 0x0004645C
			public bool HasTimestamp
			{
				get
				{
					return this.result.hasTimestamp;
				}
			}

			// Token: 0x170005FF RID: 1535
			// (get) Token: 0x06001554 RID: 5460 RVA: 0x0004826C File Offset: 0x0004646C
			// (set) Token: 0x06001555 RID: 5461 RVA: 0x0004827C File Offset: 0x0004647C
			public int Timestamp
			{
				get
				{
					return this.result.Timestamp;
				}
				set
				{
					this.SetTimestamp(value);
				}
			}

			// Token: 0x06001556 RID: 5462 RVA: 0x00048288 File Offset: 0x00046488
			public AwayEvent.Builder SetTimestamp(int value)
			{
				this.PrepareBuilder();
				this.result.hasTimestamp = true;
				this.result.timestamp_ = value;
				return this;
			}

			// Token: 0x06001557 RID: 5463 RVA: 0x000482B8 File Offset: 0x000464B8
			public AwayEvent.Builder ClearTimestamp()
			{
				this.PrepareBuilder();
				this.result.hasTimestamp = false;
				this.result.timestamp_ = 0;
				return this;
			}

			// Token: 0x17000600 RID: 1536
			// (get) Token: 0x06001558 RID: 5464 RVA: 0x000482E8 File Offset: 0x000464E8
			public bool HasInstigator
			{
				get
				{
					return this.result.hasInstigator;
				}
			}

			// Token: 0x17000601 RID: 1537
			// (get) Token: 0x06001559 RID: 5465 RVA: 0x000482F8 File Offset: 0x000464F8
			// (set) Token: 0x0600155A RID: 5466 RVA: 0x00048308 File Offset: 0x00046508
			[CLSCompliant(false)]
			public ulong Instigator
			{
				get
				{
					return this.result.Instigator;
				}
				set
				{
					this.SetInstigator(value);
				}
			}

			// Token: 0x0600155B RID: 5467 RVA: 0x00048314 File Offset: 0x00046514
			[CLSCompliant(false)]
			public AwayEvent.Builder SetInstigator(ulong value)
			{
				this.PrepareBuilder();
				this.result.hasInstigator = true;
				this.result.instigator_ = value;
				return this;
			}

			// Token: 0x0600155C RID: 5468 RVA: 0x00048344 File Offset: 0x00046544
			public AwayEvent.Builder ClearInstigator()
			{
				this.PrepareBuilder();
				this.result.hasInstigator = false;
				this.result.instigator_ = 0UL;
				return this;
			}

			// Token: 0x04000B2C RID: 2860
			private bool resultIsReadOnly;

			// Token: 0x04000B2D RID: 2861
			private AwayEvent result;
		}
	}
}
