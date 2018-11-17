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
	// Token: 0x02000225 RID: 549
	[DebuggerNonUserCode]
	public sealed class AwayEvent : GeneratedMessage<AwayEvent, AwayEvent.Builder>
	{
		// Token: 0x060013CB RID: 5067 RVA: 0x000437E0 File Offset: 0x000419E0
		private AwayEvent()
		{
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x000437F0 File Offset: 0x000419F0
		static AwayEvent()
		{
			object.ReferenceEquals(Avatar.Descriptor, null);
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x00043854 File Offset: 0x00041A54
		public static Recycler<AwayEvent, AwayEvent.Builder> Recycler()
		{
			return Recycler<AwayEvent, AwayEvent.Builder>.Manufacture();
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x060013CE RID: 5070 RVA: 0x0004385C File Offset: 0x00041A5C
		public static AwayEvent DefaultInstance
		{
			get
			{
				return AwayEvent.defaultInstance;
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x060013CF RID: 5071 RVA: 0x00043864 File Offset: 0x00041A64
		public override AwayEvent DefaultInstanceForType
		{
			get
			{
				return AwayEvent.DefaultInstance;
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x060013D0 RID: 5072 RVA: 0x0004386C File Offset: 0x00041A6C
		protected override AwayEvent ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x060013D1 RID: 5073 RVA: 0x00043870 File Offset: 0x00041A70
		public static MessageDescriptor Descriptor
		{
			get
			{
				return Avatar.internal__static_RustProto_AwayEvent__Descriptor;
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x060013D2 RID: 5074 RVA: 0x00043878 File Offset: 0x00041A78
		protected override FieldAccessorTable<AwayEvent, AwayEvent.Builder> InternalFieldAccessors
		{
			get
			{
				return Avatar.internal__static_RustProto_AwayEvent__FieldAccessorTable;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x060013D3 RID: 5075 RVA: 0x00043880 File Offset: 0x00041A80
		public bool HasType
		{
			get
			{
				return this.hasType;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x060013D4 RID: 5076 RVA: 0x00043888 File Offset: 0x00041A88
		public AwayEvent.Types.AwayEventType Type
		{
			get
			{
				return this.type_;
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x060013D5 RID: 5077 RVA: 0x00043890 File Offset: 0x00041A90
		public bool HasTimestamp
		{
			get
			{
				return this.hasTimestamp;
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x060013D6 RID: 5078 RVA: 0x00043898 File Offset: 0x00041A98
		public int Timestamp
		{
			get
			{
				return this.timestamp_;
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x060013D7 RID: 5079 RVA: 0x000438A0 File Offset: 0x00041AA0
		public bool HasInstigator
		{
			get
			{
				return this.hasInstigator;
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x060013D8 RID: 5080 RVA: 0x000438A8 File Offset: 0x00041AA8
		[CLSCompliant(false)]
		public ulong Instigator
		{
			get
			{
				return this.instigator_;
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x060013D9 RID: 5081 RVA: 0x000438B0 File Offset: 0x00041AB0
		public override bool IsInitialized
		{
			get
			{
				return this.hasType && this.hasTimestamp;
			}
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x000438D0 File Offset: 0x00041AD0
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

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x060013DB RID: 5083 RVA: 0x00043954 File Offset: 0x00041B54
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

		// Token: 0x060013DC RID: 5084 RVA: 0x000439D8 File Offset: 0x00041BD8
		public static AwayEvent ParseFrom(ByteString data)
		{
			return AwayEvent.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x000439EC File Offset: 0x00041BEC
		public static AwayEvent ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return AwayEvent.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x00043A00 File Offset: 0x00041C00
		public static AwayEvent ParseFrom(byte[] data)
		{
			return AwayEvent.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x00043A14 File Offset: 0x00041C14
		public static AwayEvent ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return AwayEvent.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x00043A28 File Offset: 0x00041C28
		public static AwayEvent ParseFrom(Stream input)
		{
			return AwayEvent.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x00043A3C File Offset: 0x00041C3C
		public static AwayEvent ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return AwayEvent.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x00043A50 File Offset: 0x00041C50
		public static AwayEvent ParseDelimitedFrom(Stream input)
		{
			return AwayEvent.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x00043A64 File Offset: 0x00041C64
		public static AwayEvent ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return AwayEvent.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x00043A78 File Offset: 0x00041C78
		public static AwayEvent ParseFrom(ICodedInputStream input)
		{
			return AwayEvent.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x00043A8C File Offset: 0x00041C8C
		public static AwayEvent ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return AwayEvent.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x00043AA0 File Offset: 0x00041CA0
		private AwayEvent MakeReadOnly()
		{
			return this;
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x00043AA4 File Offset: 0x00041CA4
		public static AwayEvent.Builder CreateBuilder()
		{
			return new AwayEvent.Builder();
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x00043AAC File Offset: 0x00041CAC
		public override AwayEvent.Builder ToBuilder()
		{
			return AwayEvent.CreateBuilder(this);
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x00043AB4 File Offset: 0x00041CB4
		public override AwayEvent.Builder CreateBuilderForType()
		{
			return new AwayEvent.Builder();
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x00043ABC File Offset: 0x00041CBC
		public static AwayEvent.Builder CreateBuilder(AwayEvent prototype)
		{
			return new AwayEvent.Builder(prototype);
		}

		// Token: 0x040009F8 RID: 2552
		public const int TypeFieldNumber = 1;

		// Token: 0x040009F9 RID: 2553
		public const int TimestampFieldNumber = 2;

		// Token: 0x040009FA RID: 2554
		public const int InstigatorFieldNumber = 3;

		// Token: 0x040009FB RID: 2555
		private static readonly AwayEvent defaultInstance = new AwayEvent().MakeReadOnly();

		// Token: 0x040009FC RID: 2556
		private static readonly string[] _awayEventFieldNames = new string[]
		{
			"instigator",
			"timestamp",
			"type"
		};

		// Token: 0x040009FD RID: 2557
		private static readonly uint[] _awayEventFieldTags = new uint[]
		{
			24u,
			16u,
			8u
		};

		// Token: 0x040009FE RID: 2558
		private bool hasType;

		// Token: 0x040009FF RID: 2559
		private AwayEvent.Types.AwayEventType type_;

		// Token: 0x04000A00 RID: 2560
		private bool hasTimestamp;

		// Token: 0x04000A01 RID: 2561
		private int timestamp_;

		// Token: 0x04000A02 RID: 2562
		private bool hasInstigator;

		// Token: 0x04000A03 RID: 2563
		private ulong instigator_;

		// Token: 0x04000A04 RID: 2564
		private int memoizedSerializedSize = -1;

		// Token: 0x02000226 RID: 550
		[DebuggerNonUserCode]
		public static class Types
		{
			// Token: 0x02000227 RID: 551
			public enum AwayEventType
			{
				// Token: 0x04000A06 RID: 2566
				UNKNOWN,
				// Token: 0x04000A07 RID: 2567
				SLUMBER,
				// Token: 0x04000A08 RID: 2568
				DIED
			}
		}

		// Token: 0x02000228 RID: 552
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<AwayEvent, AwayEvent.Builder>
		{
			// Token: 0x060013EB RID: 5099 RVA: 0x00043AC4 File Offset: 0x00041CC4
			public Builder()
			{
				this.result = AwayEvent.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x060013EC RID: 5100 RVA: 0x00043AE0 File Offset: 0x00041CE0
			internal Builder(AwayEvent cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x170005AF RID: 1455
			// (get) Token: 0x060013ED RID: 5101 RVA: 0x00043AF8 File Offset: 0x00041CF8
			protected override AwayEvent.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x060013EE RID: 5102 RVA: 0x00043AFC File Offset: 0x00041CFC
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

			// Token: 0x170005B0 RID: 1456
			// (get) Token: 0x060013EF RID: 5103 RVA: 0x00043B3C File Offset: 0x00041D3C
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x170005B1 RID: 1457
			// (get) Token: 0x060013F0 RID: 5104 RVA: 0x00043B4C File Offset: 0x00041D4C
			protected override AwayEvent MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x060013F1 RID: 5105 RVA: 0x00043B54 File Offset: 0x00041D54
			public override AwayEvent.Builder Clear()
			{
				this.result = AwayEvent.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x060013F2 RID: 5106 RVA: 0x00043B6C File Offset: 0x00041D6C
			public override AwayEvent.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new AwayEvent.Builder(this.result);
				}
				return new AwayEvent.Builder().MergeFrom(this.result);
			}

			// Token: 0x170005B2 RID: 1458
			// (get) Token: 0x060013F3 RID: 5107 RVA: 0x00043B98 File Offset: 0x00041D98
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return AwayEvent.Descriptor;
				}
			}

			// Token: 0x170005B3 RID: 1459
			// (get) Token: 0x060013F4 RID: 5108 RVA: 0x00043BA0 File Offset: 0x00041DA0
			public override AwayEvent DefaultInstanceForType
			{
				get
				{
					return AwayEvent.DefaultInstance;
				}
			}

			// Token: 0x060013F5 RID: 5109 RVA: 0x00043BA8 File Offset: 0x00041DA8
			public override AwayEvent BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x060013F6 RID: 5110 RVA: 0x00043BDC File Offset: 0x00041DDC
			public override AwayEvent.Builder MergeFrom(IMessage other)
			{
				if (other is AwayEvent)
				{
					return this.MergeFrom((AwayEvent)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x060013F7 RID: 5111 RVA: 0x00043C00 File Offset: 0x00041E00
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

			// Token: 0x060013F8 RID: 5112 RVA: 0x00043C74 File Offset: 0x00041E74
			public override AwayEvent.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x060013F9 RID: 5113 RVA: 0x00043C84 File Offset: 0x00041E84
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

			// Token: 0x170005B4 RID: 1460
			// (get) Token: 0x060013FA RID: 5114 RVA: 0x00043E28 File Offset: 0x00042028
			public bool HasType
			{
				get
				{
					return this.result.hasType;
				}
			}

			// Token: 0x170005B5 RID: 1461
			// (get) Token: 0x060013FB RID: 5115 RVA: 0x00043E38 File Offset: 0x00042038
			// (set) Token: 0x060013FC RID: 5116 RVA: 0x00043E48 File Offset: 0x00042048
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

			// Token: 0x060013FD RID: 5117 RVA: 0x00043E54 File Offset: 0x00042054
			public AwayEvent.Builder SetType(AwayEvent.Types.AwayEventType value)
			{
				this.PrepareBuilder();
				this.result.hasType = true;
				this.result.type_ = value;
				return this;
			}

			// Token: 0x060013FE RID: 5118 RVA: 0x00043E84 File Offset: 0x00042084
			public AwayEvent.Builder ClearType()
			{
				this.PrepareBuilder();
				this.result.hasType = false;
				this.result.type_ = AwayEvent.Types.AwayEventType.UNKNOWN;
				return this;
			}

			// Token: 0x170005B6 RID: 1462
			// (get) Token: 0x060013FF RID: 5119 RVA: 0x00043EB4 File Offset: 0x000420B4
			public bool HasTimestamp
			{
				get
				{
					return this.result.hasTimestamp;
				}
			}

			// Token: 0x170005B7 RID: 1463
			// (get) Token: 0x06001400 RID: 5120 RVA: 0x00043EC4 File Offset: 0x000420C4
			// (set) Token: 0x06001401 RID: 5121 RVA: 0x00043ED4 File Offset: 0x000420D4
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

			// Token: 0x06001402 RID: 5122 RVA: 0x00043EE0 File Offset: 0x000420E0
			public AwayEvent.Builder SetTimestamp(int value)
			{
				this.PrepareBuilder();
				this.result.hasTimestamp = true;
				this.result.timestamp_ = value;
				return this;
			}

			// Token: 0x06001403 RID: 5123 RVA: 0x00043F10 File Offset: 0x00042110
			public AwayEvent.Builder ClearTimestamp()
			{
				this.PrepareBuilder();
				this.result.hasTimestamp = false;
				this.result.timestamp_ = 0;
				return this;
			}

			// Token: 0x170005B8 RID: 1464
			// (get) Token: 0x06001404 RID: 5124 RVA: 0x00043F40 File Offset: 0x00042140
			public bool HasInstigator
			{
				get
				{
					return this.result.hasInstigator;
				}
			}

			// Token: 0x170005B9 RID: 1465
			// (get) Token: 0x06001405 RID: 5125 RVA: 0x00043F50 File Offset: 0x00042150
			// (set) Token: 0x06001406 RID: 5126 RVA: 0x00043F60 File Offset: 0x00042160
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

			// Token: 0x06001407 RID: 5127 RVA: 0x00043F6C File Offset: 0x0004216C
			[CLSCompliant(false)]
			public AwayEvent.Builder SetInstigator(ulong value)
			{
				this.PrepareBuilder();
				this.result.hasInstigator = true;
				this.result.instigator_ = value;
				return this;
			}

			// Token: 0x06001408 RID: 5128 RVA: 0x00043F9C File Offset: 0x0004219C
			public AwayEvent.Builder ClearInstigator()
			{
				this.PrepareBuilder();
				this.result.hasInstigator = false;
				this.result.instigator_ = 0UL;
				return this;
			}

			// Token: 0x04000A09 RID: 2569
			private bool resultIsReadOnly;

			// Token: 0x04000A0A RID: 2570
			private AwayEvent result;
		}
	}
}
