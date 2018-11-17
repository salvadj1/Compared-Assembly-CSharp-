using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Collections;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;
using RustProto.Helpers;
using RustProto.Proto;

namespace RustProto
{
	// Token: 0x02000234 RID: 564
	[DebuggerNonUserCode]
	public sealed class Item : GeneratedMessage<RustProto.Item, RustProto.Item.Builder>
	{
		// Token: 0x0600103C RID: 4156 RVA: 0x0003D0C0 File Offset: 0x0003B2C0
		private Item()
		{
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x0003D0E8 File Offset: 0x0003B2E8
		static Item()
		{
			object.ReferenceEquals(RustProto.Proto.Item.Descriptor, null);
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x0003D174 File Offset: 0x0003B374
		public static RustProto.Helpers.Recycler<RustProto.Item, RustProto.Item.Builder> Recycler()
		{
			return RustProto.Helpers.Recycler<RustProto.Item, RustProto.Item.Builder>.Manufacture();
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x0600103F RID: 4159 RVA: 0x0003D17C File Offset: 0x0003B37C
		public static RustProto.Item DefaultInstance
		{
			get
			{
				return RustProto.Item.defaultInstance;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06001040 RID: 4160 RVA: 0x0003D184 File Offset: 0x0003B384
		public override RustProto.Item DefaultInstanceForType
		{
			get
			{
				return RustProto.Item.DefaultInstance;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06001041 RID: 4161 RVA: 0x0003D18C File Offset: 0x0003B38C
		protected override RustProto.Item ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06001042 RID: 4162 RVA: 0x0003D190 File Offset: 0x0003B390
		public static MessageDescriptor Descriptor
		{
			get
			{
				return RustProto.Proto.Item.internal__static_RustProto_Item__Descriptor;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06001043 RID: 4163 RVA: 0x0003D198 File Offset: 0x0003B398
		protected override FieldAccessorTable<RustProto.Item, RustProto.Item.Builder> InternalFieldAccessors
		{
			get
			{
				return RustProto.Proto.Item.internal__static_RustProto_Item__FieldAccessorTable;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06001044 RID: 4164 RVA: 0x0003D1A0 File Offset: 0x0003B3A0
		public bool HasId
		{
			get
			{
				return this.hasId;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06001045 RID: 4165 RVA: 0x0003D1A8 File Offset: 0x0003B3A8
		public int Id
		{
			get
			{
				return this.id_;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06001046 RID: 4166 RVA: 0x0003D1B0 File Offset: 0x0003B3B0
		public bool HasName
		{
			get
			{
				return this.hasName;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001047 RID: 4167 RVA: 0x0003D1B8 File Offset: 0x0003B3B8
		public string Name
		{
			get
			{
				return this.name_;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06001048 RID: 4168 RVA: 0x0003D1C0 File Offset: 0x0003B3C0
		public bool HasSlot
		{
			get
			{
				return this.hasSlot;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06001049 RID: 4169 RVA: 0x0003D1C8 File Offset: 0x0003B3C8
		public int Slot
		{
			get
			{
				return this.slot_;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x0600104A RID: 4170 RVA: 0x0003D1D0 File Offset: 0x0003B3D0
		public bool HasCount
		{
			get
			{
				return this.hasCount;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x0600104B RID: 4171 RVA: 0x0003D1D8 File Offset: 0x0003B3D8
		public int Count
		{
			get
			{
				return this.count_;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x0600104C RID: 4172 RVA: 0x0003D1E0 File Offset: 0x0003B3E0
		public bool HasSubslots
		{
			get
			{
				return this.hasSubslots;
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x0600104D RID: 4173 RVA: 0x0003D1E8 File Offset: 0x0003B3E8
		public int Subslots
		{
			get
			{
				return this.subslots_;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x0600104E RID: 4174 RVA: 0x0003D1F0 File Offset: 0x0003B3F0
		public bool HasCondition
		{
			get
			{
				return this.hasCondition;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x0600104F RID: 4175 RVA: 0x0003D1F8 File Offset: 0x0003B3F8
		public float Condition
		{
			get
			{
				return this.condition_;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06001050 RID: 4176 RVA: 0x0003D200 File Offset: 0x0003B400
		public bool HasMaxcondition
		{
			get
			{
				return this.hasMaxcondition;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06001051 RID: 4177 RVA: 0x0003D208 File Offset: 0x0003B408
		public float Maxcondition
		{
			get
			{
				return this.maxcondition_;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06001052 RID: 4178 RVA: 0x0003D210 File Offset: 0x0003B410
		public IList<RustProto.Item> SubitemList
		{
			get
			{
				return this.subitem_;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06001053 RID: 4179 RVA: 0x0003D218 File Offset: 0x0003B418
		public int SubitemCount
		{
			get
			{
				return this.subitem_.Count;
			}
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x0003D228 File Offset: 0x0003B428
		public RustProto.Item GetSubitem(int index)
		{
			return this.subitem_[index];
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06001055 RID: 4181 RVA: 0x0003D238 File Offset: 0x0003B438
		public override bool IsInitialized
		{
			get
			{
				if (!this.hasId)
				{
					return false;
				}
				foreach (RustProto.Item item in this.SubitemList)
				{
					if (!item.IsInitialized)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x0003D2B8 File Offset: 0x0003B4B8
		public override void WriteTo(ICodedOutputStream output)
		{
			int serializedSize = this.SerializedSize;
			string[] itemFieldNames = RustProto.Item._itemFieldNames;
			if (this.hasId)
			{
				output.WriteInt32(1, itemFieldNames[2], this.Id);
			}
			if (this.hasName)
			{
				output.WriteString(2, itemFieldNames[4], this.Name);
			}
			if (this.hasSlot)
			{
				output.WriteInt32(3, itemFieldNames[5], this.Slot);
			}
			if (this.hasCount)
			{
				output.WriteInt32(4, itemFieldNames[1], this.Count);
			}
			if (this.subitem_.Count > 0)
			{
				output.WriteMessageArray<RustProto.Item>(5, itemFieldNames[6], this.subitem_);
			}
			if (this.hasSubslots)
			{
				output.WriteInt32(6, itemFieldNames[7], this.Subslots);
			}
			if (this.hasCondition)
			{
				output.WriteFloat(7, itemFieldNames[0], this.Condition);
			}
			if (this.hasMaxcondition)
			{
				output.WriteFloat(8, itemFieldNames[3], this.Maxcondition);
			}
			this.UnknownFields.WriteTo(output);
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06001057 RID: 4183 RVA: 0x0003D3BC File Offset: 0x0003B5BC
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
				if (this.hasSlot)
				{
					num += CodedOutputStream.ComputeInt32Size(3, this.Slot);
				}
				if (this.hasCount)
				{
					num += CodedOutputStream.ComputeInt32Size(4, this.Count);
				}
				if (this.hasSubslots)
				{
					num += CodedOutputStream.ComputeInt32Size(6, this.Subslots);
				}
				if (this.hasCondition)
				{
					num += CodedOutputStream.ComputeFloatSize(7, this.Condition);
				}
				if (this.hasMaxcondition)
				{
					num += CodedOutputStream.ComputeFloatSize(8, this.Maxcondition);
				}
				foreach (RustProto.Item item in this.SubitemList)
				{
					num += CodedOutputStream.ComputeMessageSize(5, item);
				}
				num += this.UnknownFields.SerializedSize;
				this.memoizedSerializedSize = num;
				return num;
			}
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x0003D500 File Offset: 0x0003B700
		public static RustProto.Item ParseFrom(ByteString data)
		{
			return RustProto.Item.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x0003D514 File Offset: 0x0003B714
		public static RustProto.Item ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return RustProto.Item.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x0003D528 File Offset: 0x0003B728
		public static RustProto.Item ParseFrom(byte[] data)
		{
			return RustProto.Item.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x0003D53C File Offset: 0x0003B73C
		public static RustProto.Item ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return RustProto.Item.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x0003D550 File Offset: 0x0003B750
		public static RustProto.Item ParseFrom(Stream input)
		{
			return RustProto.Item.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x0003D564 File Offset: 0x0003B764
		public static RustProto.Item ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return RustProto.Item.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x0003D578 File Offset: 0x0003B778
		public static RustProto.Item ParseDelimitedFrom(Stream input)
		{
			return RustProto.Item.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x0003D58C File Offset: 0x0003B78C
		public static RustProto.Item ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return RustProto.Item.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x0003D5A0 File Offset: 0x0003B7A0
		public static RustProto.Item ParseFrom(ICodedInputStream input)
		{
			return RustProto.Item.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x0003D5B4 File Offset: 0x0003B7B4
		public static RustProto.Item ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return RustProto.Item.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x0003D5C8 File Offset: 0x0003B7C8
		private RustProto.Item MakeReadOnly()
		{
			this.subitem_.MakeReadOnly();
			return this;
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x0003D5D8 File Offset: 0x0003B7D8
		public static RustProto.Item.Builder CreateBuilder()
		{
			return new RustProto.Item.Builder();
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x0003D5E0 File Offset: 0x0003B7E0
		public override RustProto.Item.Builder ToBuilder()
		{
			return RustProto.Item.CreateBuilder(this);
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x0003D5E8 File Offset: 0x0003B7E8
		public override RustProto.Item.Builder CreateBuilderForType()
		{
			return new RustProto.Item.Builder();
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x0003D5F0 File Offset: 0x0003B7F0
		public static RustProto.Item.Builder CreateBuilder(RustProto.Item prototype)
		{
			return new RustProto.Item.Builder(prototype);
		}

		// Token: 0x040009EC RID: 2540
		public const int IdFieldNumber = 1;

		// Token: 0x040009ED RID: 2541
		public const int NameFieldNumber = 2;

		// Token: 0x040009EE RID: 2542
		public const int SlotFieldNumber = 3;

		// Token: 0x040009EF RID: 2543
		public const int CountFieldNumber = 4;

		// Token: 0x040009F0 RID: 2544
		public const int SubslotsFieldNumber = 6;

		// Token: 0x040009F1 RID: 2545
		public const int ConditionFieldNumber = 7;

		// Token: 0x040009F2 RID: 2546
		public const int MaxconditionFieldNumber = 8;

		// Token: 0x040009F3 RID: 2547
		public const int SubitemFieldNumber = 5;

		// Token: 0x040009F4 RID: 2548
		private static readonly RustProto.Item defaultInstance = new RustProto.Item().MakeReadOnly();

		// Token: 0x040009F5 RID: 2549
		private static readonly string[] _itemFieldNames = new string[]
		{
			"condition",
			"count",
			"id",
			"maxcondition",
			"name",
			"slot",
			"subitem",
			"subslots"
		};

		// Token: 0x040009F6 RID: 2550
		private static readonly uint[] _itemFieldTags = new uint[]
		{
			61u,
			32u,
			8u,
			69u,
			18u,
			24u,
			42u,
			48u
		};

		// Token: 0x040009F7 RID: 2551
		private bool hasId;

		// Token: 0x040009F8 RID: 2552
		private int id_;

		// Token: 0x040009F9 RID: 2553
		private bool hasName;

		// Token: 0x040009FA RID: 2554
		private string name_ = string.Empty;

		// Token: 0x040009FB RID: 2555
		private bool hasSlot;

		// Token: 0x040009FC RID: 2556
		private int slot_;

		// Token: 0x040009FD RID: 2557
		private bool hasCount;

		// Token: 0x040009FE RID: 2558
		private int count_;

		// Token: 0x040009FF RID: 2559
		private bool hasSubslots;

		// Token: 0x04000A00 RID: 2560
		private int subslots_;

		// Token: 0x04000A01 RID: 2561
		private bool hasCondition;

		// Token: 0x04000A02 RID: 2562
		private float condition_;

		// Token: 0x04000A03 RID: 2563
		private bool hasMaxcondition;

		// Token: 0x04000A04 RID: 2564
		private float maxcondition_;

		// Token: 0x04000A05 RID: 2565
		private PopsicleList<RustProto.Item> subitem_ = new PopsicleList<RustProto.Item>();

		// Token: 0x04000A06 RID: 2566
		private int memoizedSerializedSize = -1;

		// Token: 0x02000235 RID: 565
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<RustProto.Item, RustProto.Item.Builder>
		{
			// Token: 0x06001067 RID: 4199 RVA: 0x0003D5F8 File Offset: 0x0003B7F8
			public Builder()
			{
				this.result = RustProto.Item.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x06001068 RID: 4200 RVA: 0x0003D614 File Offset: 0x0003B814
			internal Builder(RustProto.Item cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x17000421 RID: 1057
			// (get) Token: 0x06001069 RID: 4201 RVA: 0x0003D62C File Offset: 0x0003B82C
			protected override RustProto.Item.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x0600106A RID: 4202 RVA: 0x0003D630 File Offset: 0x0003B830
			private RustProto.Item PrepareBuilder()
			{
				if (this.resultIsReadOnly)
				{
					RustProto.Item other = this.result;
					this.result = new RustProto.Item();
					this.resultIsReadOnly = false;
					this.MergeFrom(other);
				}
				return this.result;
			}

			// Token: 0x17000422 RID: 1058
			// (get) Token: 0x0600106B RID: 4203 RVA: 0x0003D670 File Offset: 0x0003B870
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x17000423 RID: 1059
			// (get) Token: 0x0600106C RID: 4204 RVA: 0x0003D680 File Offset: 0x0003B880
			protected override RustProto.Item MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x0600106D RID: 4205 RVA: 0x0003D688 File Offset: 0x0003B888
			public override RustProto.Item.Builder Clear()
			{
				this.result = RustProto.Item.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x0600106E RID: 4206 RVA: 0x0003D6A0 File Offset: 0x0003B8A0
			public override RustProto.Item.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new RustProto.Item.Builder(this.result);
				}
				return new RustProto.Item.Builder().MergeFrom(this.result);
			}

			// Token: 0x17000424 RID: 1060
			// (get) Token: 0x0600106F RID: 4207 RVA: 0x0003D6CC File Offset: 0x0003B8CC
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return RustProto.Item.Descriptor;
				}
			}

			// Token: 0x17000425 RID: 1061
			// (get) Token: 0x06001070 RID: 4208 RVA: 0x0003D6D4 File Offset: 0x0003B8D4
			public override RustProto.Item DefaultInstanceForType
			{
				get
				{
					return RustProto.Item.DefaultInstance;
				}
			}

			// Token: 0x06001071 RID: 4209 RVA: 0x0003D6DC File Offset: 0x0003B8DC
			public override RustProto.Item BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x06001072 RID: 4210 RVA: 0x0003D710 File Offset: 0x0003B910
			public override RustProto.Item.Builder MergeFrom(IMessage other)
			{
				if (other is RustProto.Item)
				{
					return this.MergeFrom((RustProto.Item)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x06001073 RID: 4211 RVA: 0x0003D734 File Offset: 0x0003B934
			public override RustProto.Item.Builder MergeFrom(RustProto.Item other)
			{
				if (other == RustProto.Item.DefaultInstance)
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
				if (other.HasSlot)
				{
					this.Slot = other.Slot;
				}
				if (other.HasCount)
				{
					this.Count = other.Count;
				}
				if (other.HasSubslots)
				{
					this.Subslots = other.Subslots;
				}
				if (other.HasCondition)
				{
					this.Condition = other.Condition;
				}
				if (other.HasMaxcondition)
				{
					this.Maxcondition = other.Maxcondition;
				}
				if (other.subitem_.Count != 0)
				{
					this.result.subitem_.Add(other.subitem_);
				}
				this.MergeUnknownFields(other.UnknownFields);
				return this;
			}

			// Token: 0x06001074 RID: 4212 RVA: 0x0003D82C File Offset: 0x0003BA2C
			public override RustProto.Item.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x06001075 RID: 4213 RVA: 0x0003D83C File Offset: 0x0003BA3C
			public override RustProto.Item.Builder MergeFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
			{
				this.PrepareBuilder();
				UnknownFieldSet.Builder builder = null;
				uint num;
				string text;
				while (input.ReadTag(ref num, ref text))
				{
					if (num == 0u && text != null)
					{
						int num2 = Array.BinarySearch<string>(RustProto.Item._itemFieldNames, text, StringComparer.Ordinal);
						if (num2 < 0)
						{
							if (builder == null)
							{
								builder = UnknownFieldSet.CreateBuilder(this.UnknownFields);
							}
							this.ParseUnknownField(input, builder, extensionRegistry, num, text);
							continue;
						}
						num = RustProto.Item._itemFieldTags[num2];
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
							if (num3 != 24u)
							{
								if (num3 != 32u)
								{
									if (num3 != 42u)
									{
										if (num3 != 48u)
										{
											if (num3 != 61u)
											{
												if (num3 != 69u)
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
													this.result.hasMaxcondition = input.ReadFloat(ref this.result.maxcondition_);
												}
											}
											else
											{
												this.result.hasCondition = input.ReadFloat(ref this.result.condition_);
											}
										}
										else
										{
											this.result.hasSubslots = input.ReadInt32(ref this.result.subslots_);
										}
									}
									else
									{
										input.ReadMessageArray<RustProto.Item>(num, text, this.result.subitem_, RustProto.Item.DefaultInstance, extensionRegistry);
									}
								}
								else
								{
									this.result.hasCount = input.ReadInt32(ref this.result.count_);
								}
							}
							else
							{
								this.result.hasSlot = input.ReadInt32(ref this.result.slot_);
							}
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

			// Token: 0x17000426 RID: 1062
			// (get) Token: 0x06001076 RID: 4214 RVA: 0x0003DA74 File Offset: 0x0003BC74
			public bool HasId
			{
				get
				{
					return this.result.hasId;
				}
			}

			// Token: 0x17000427 RID: 1063
			// (get) Token: 0x06001077 RID: 4215 RVA: 0x0003DA84 File Offset: 0x0003BC84
			// (set) Token: 0x06001078 RID: 4216 RVA: 0x0003DA94 File Offset: 0x0003BC94
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

			// Token: 0x06001079 RID: 4217 RVA: 0x0003DAA0 File Offset: 0x0003BCA0
			public RustProto.Item.Builder SetId(int value)
			{
				this.PrepareBuilder();
				this.result.hasId = true;
				this.result.id_ = value;
				return this;
			}

			// Token: 0x0600107A RID: 4218 RVA: 0x0003DAD0 File Offset: 0x0003BCD0
			public RustProto.Item.Builder ClearId()
			{
				this.PrepareBuilder();
				this.result.hasId = false;
				this.result.id_ = 0;
				return this;
			}

			// Token: 0x17000428 RID: 1064
			// (get) Token: 0x0600107B RID: 4219 RVA: 0x0003DB00 File Offset: 0x0003BD00
			public bool HasName
			{
				get
				{
					return this.result.hasName;
				}
			}

			// Token: 0x17000429 RID: 1065
			// (get) Token: 0x0600107C RID: 4220 RVA: 0x0003DB10 File Offset: 0x0003BD10
			// (set) Token: 0x0600107D RID: 4221 RVA: 0x0003DB20 File Offset: 0x0003BD20
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

			// Token: 0x0600107E RID: 4222 RVA: 0x0003DB2C File Offset: 0x0003BD2C
			public RustProto.Item.Builder SetName(string value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasName = true;
				this.result.name_ = value;
				return this;
			}

			// Token: 0x0600107F RID: 4223 RVA: 0x0003DB5C File Offset: 0x0003BD5C
			public RustProto.Item.Builder ClearName()
			{
				this.PrepareBuilder();
				this.result.hasName = false;
				this.result.name_ = string.Empty;
				return this;
			}

			// Token: 0x1700042A RID: 1066
			// (get) Token: 0x06001080 RID: 4224 RVA: 0x0003DB90 File Offset: 0x0003BD90
			public bool HasSlot
			{
				get
				{
					return this.result.hasSlot;
				}
			}

			// Token: 0x1700042B RID: 1067
			// (get) Token: 0x06001081 RID: 4225 RVA: 0x0003DBA0 File Offset: 0x0003BDA0
			// (set) Token: 0x06001082 RID: 4226 RVA: 0x0003DBB0 File Offset: 0x0003BDB0
			public int Slot
			{
				get
				{
					return this.result.Slot;
				}
				set
				{
					this.SetSlot(value);
				}
			}

			// Token: 0x06001083 RID: 4227 RVA: 0x0003DBBC File Offset: 0x0003BDBC
			public RustProto.Item.Builder SetSlot(int value)
			{
				this.PrepareBuilder();
				this.result.hasSlot = true;
				this.result.slot_ = value;
				return this;
			}

			// Token: 0x06001084 RID: 4228 RVA: 0x0003DBEC File Offset: 0x0003BDEC
			public RustProto.Item.Builder ClearSlot()
			{
				this.PrepareBuilder();
				this.result.hasSlot = false;
				this.result.slot_ = 0;
				return this;
			}

			// Token: 0x1700042C RID: 1068
			// (get) Token: 0x06001085 RID: 4229 RVA: 0x0003DC1C File Offset: 0x0003BE1C
			public bool HasCount
			{
				get
				{
					return this.result.hasCount;
				}
			}

			// Token: 0x1700042D RID: 1069
			// (get) Token: 0x06001086 RID: 4230 RVA: 0x0003DC2C File Offset: 0x0003BE2C
			// (set) Token: 0x06001087 RID: 4231 RVA: 0x0003DC3C File Offset: 0x0003BE3C
			public int Count
			{
				get
				{
					return this.result.Count;
				}
				set
				{
					this.SetCount(value);
				}
			}

			// Token: 0x06001088 RID: 4232 RVA: 0x0003DC48 File Offset: 0x0003BE48
			public RustProto.Item.Builder SetCount(int value)
			{
				this.PrepareBuilder();
				this.result.hasCount = true;
				this.result.count_ = value;
				return this;
			}

			// Token: 0x06001089 RID: 4233 RVA: 0x0003DC78 File Offset: 0x0003BE78
			public RustProto.Item.Builder ClearCount()
			{
				this.PrepareBuilder();
				this.result.hasCount = false;
				this.result.count_ = 0;
				return this;
			}

			// Token: 0x1700042E RID: 1070
			// (get) Token: 0x0600108A RID: 4234 RVA: 0x0003DCA8 File Offset: 0x0003BEA8
			public bool HasSubslots
			{
				get
				{
					return this.result.hasSubslots;
				}
			}

			// Token: 0x1700042F RID: 1071
			// (get) Token: 0x0600108B RID: 4235 RVA: 0x0003DCB8 File Offset: 0x0003BEB8
			// (set) Token: 0x0600108C RID: 4236 RVA: 0x0003DCC8 File Offset: 0x0003BEC8
			public int Subslots
			{
				get
				{
					return this.result.Subslots;
				}
				set
				{
					this.SetSubslots(value);
				}
			}

			// Token: 0x0600108D RID: 4237 RVA: 0x0003DCD4 File Offset: 0x0003BED4
			public RustProto.Item.Builder SetSubslots(int value)
			{
				this.PrepareBuilder();
				this.result.hasSubslots = true;
				this.result.subslots_ = value;
				return this;
			}

			// Token: 0x0600108E RID: 4238 RVA: 0x0003DD04 File Offset: 0x0003BF04
			public RustProto.Item.Builder ClearSubslots()
			{
				this.PrepareBuilder();
				this.result.hasSubslots = false;
				this.result.subslots_ = 0;
				return this;
			}

			// Token: 0x17000430 RID: 1072
			// (get) Token: 0x0600108F RID: 4239 RVA: 0x0003DD34 File Offset: 0x0003BF34
			public bool HasCondition
			{
				get
				{
					return this.result.hasCondition;
				}
			}

			// Token: 0x17000431 RID: 1073
			// (get) Token: 0x06001090 RID: 4240 RVA: 0x0003DD44 File Offset: 0x0003BF44
			// (set) Token: 0x06001091 RID: 4241 RVA: 0x0003DD54 File Offset: 0x0003BF54
			public float Condition
			{
				get
				{
					return this.result.Condition;
				}
				set
				{
					this.SetCondition(value);
				}
			}

			// Token: 0x06001092 RID: 4242 RVA: 0x0003DD60 File Offset: 0x0003BF60
			public RustProto.Item.Builder SetCondition(float value)
			{
				this.PrepareBuilder();
				this.result.hasCondition = true;
				this.result.condition_ = value;
				return this;
			}

			// Token: 0x06001093 RID: 4243 RVA: 0x0003DD90 File Offset: 0x0003BF90
			public RustProto.Item.Builder ClearCondition()
			{
				this.PrepareBuilder();
				this.result.hasCondition = false;
				this.result.condition_ = 0f;
				return this;
			}

			// Token: 0x17000432 RID: 1074
			// (get) Token: 0x06001094 RID: 4244 RVA: 0x0003DDC4 File Offset: 0x0003BFC4
			public bool HasMaxcondition
			{
				get
				{
					return this.result.hasMaxcondition;
				}
			}

			// Token: 0x17000433 RID: 1075
			// (get) Token: 0x06001095 RID: 4245 RVA: 0x0003DDD4 File Offset: 0x0003BFD4
			// (set) Token: 0x06001096 RID: 4246 RVA: 0x0003DDE4 File Offset: 0x0003BFE4
			public float Maxcondition
			{
				get
				{
					return this.result.Maxcondition;
				}
				set
				{
					this.SetMaxcondition(value);
				}
			}

			// Token: 0x06001097 RID: 4247 RVA: 0x0003DDF0 File Offset: 0x0003BFF0
			public RustProto.Item.Builder SetMaxcondition(float value)
			{
				this.PrepareBuilder();
				this.result.hasMaxcondition = true;
				this.result.maxcondition_ = value;
				return this;
			}

			// Token: 0x06001098 RID: 4248 RVA: 0x0003DE20 File Offset: 0x0003C020
			public RustProto.Item.Builder ClearMaxcondition()
			{
				this.PrepareBuilder();
				this.result.hasMaxcondition = false;
				this.result.maxcondition_ = 0f;
				return this;
			}

			// Token: 0x17000434 RID: 1076
			// (get) Token: 0x06001099 RID: 4249 RVA: 0x0003DE54 File Offset: 0x0003C054
			public IPopsicleList<RustProto.Item> SubitemList
			{
				get
				{
					return this.PrepareBuilder().subitem_;
				}
			}

			// Token: 0x17000435 RID: 1077
			// (get) Token: 0x0600109A RID: 4250 RVA: 0x0003DE64 File Offset: 0x0003C064
			public int SubitemCount
			{
				get
				{
					return this.result.SubitemCount;
				}
			}

			// Token: 0x0600109B RID: 4251 RVA: 0x0003DE74 File Offset: 0x0003C074
			public RustProto.Item GetSubitem(int index)
			{
				return this.result.GetSubitem(index);
			}

			// Token: 0x0600109C RID: 4252 RVA: 0x0003DE84 File Offset: 0x0003C084
			public RustProto.Item.Builder SetSubitem(int index, RustProto.Item value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.subitem_[index] = value;
				return this;
			}

			// Token: 0x0600109D RID: 4253 RVA: 0x0003DEAC File Offset: 0x0003C0AC
			public RustProto.Item.Builder SetSubitem(int index, RustProto.Item.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.subitem_[index] = builderForValue.Build();
				return this;
			}

			// Token: 0x0600109E RID: 4254 RVA: 0x0003DEE4 File Offset: 0x0003C0E4
			public RustProto.Item.Builder AddSubitem(RustProto.Item value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.subitem_.Add(value);
				return this;
			}

			// Token: 0x0600109F RID: 4255 RVA: 0x0003DF18 File Offset: 0x0003C118
			public RustProto.Item.Builder AddSubitem(RustProto.Item.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.subitem_.Add(builderForValue.Build());
				return this;
			}

			// Token: 0x060010A0 RID: 4256 RVA: 0x0003DF44 File Offset: 0x0003C144
			public RustProto.Item.Builder AddRangeSubitem(IEnumerable<RustProto.Item> values)
			{
				this.PrepareBuilder();
				this.result.subitem_.Add(values);
				return this;
			}

			// Token: 0x060010A1 RID: 4257 RVA: 0x0003DF60 File Offset: 0x0003C160
			public RustProto.Item.Builder ClearSubitem()
			{
				this.PrepareBuilder();
				this.result.subitem_.Clear();
				return this;
			}

			// Token: 0x04000A07 RID: 2567
			private bool resultIsReadOnly;

			// Token: 0x04000A08 RID: 2568
			private RustProto.Item result;
		}
	}
}
