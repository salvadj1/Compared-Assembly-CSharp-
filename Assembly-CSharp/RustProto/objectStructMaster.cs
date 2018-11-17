using System;
using System.Diagnostics;
using System.IO;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;
using RustProto.Helpers;

namespace RustProto
{
	// Token: 0x02000250 RID: 592
	[DebuggerNonUserCode]
	public sealed class objectStructMaster : GeneratedMessage<objectStructMaster, objectStructMaster.Builder>
	{
		// Token: 0x06001423 RID: 5155 RVA: 0x00045BCC File Offset: 0x00043DCC
		private objectStructMaster()
		{
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x00045BDC File Offset: 0x00043DDC
		static objectStructMaster()
		{
			object.ReferenceEquals(Worldsave.Descriptor, null);
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x00045C48 File Offset: 0x00043E48
		public static RustProto.Helpers.Recycler<objectStructMaster, objectStructMaster.Builder> Recycler()
		{
			return RustProto.Helpers.Recycler<objectStructMaster, objectStructMaster.Builder>.Manufacture();
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06001426 RID: 5158 RVA: 0x00045C50 File Offset: 0x00043E50
		public static objectStructMaster DefaultInstance
		{
			get
			{
				return objectStructMaster.defaultInstance;
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001427 RID: 5159 RVA: 0x00045C58 File Offset: 0x00043E58
		public override objectStructMaster DefaultInstanceForType
		{
			get
			{
				return objectStructMaster.DefaultInstance;
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001428 RID: 5160 RVA: 0x00045C60 File Offset: 0x00043E60
		protected override objectStructMaster ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001429 RID: 5161 RVA: 0x00045C64 File Offset: 0x00043E64
		public static MessageDescriptor Descriptor
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectStructMaster__Descriptor;
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x0600142A RID: 5162 RVA: 0x00045C6C File Offset: 0x00043E6C
		protected override FieldAccessorTable<objectStructMaster, objectStructMaster.Builder> InternalFieldAccessors
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectStructMaster__FieldAccessorTable;
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x0600142B RID: 5163 RVA: 0x00045C74 File Offset: 0x00043E74
		public bool HasID
		{
			get
			{
				return this.hasID;
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x0600142C RID: 5164 RVA: 0x00045C7C File Offset: 0x00043E7C
		public int ID
		{
			get
			{
				return this.iD_;
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x0600142D RID: 5165 RVA: 0x00045C84 File Offset: 0x00043E84
		public bool HasDecayDelay
		{
			get
			{
				return this.hasDecayDelay;
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x0600142E RID: 5166 RVA: 0x00045C8C File Offset: 0x00043E8C
		public float DecayDelay
		{
			get
			{
				return this.decayDelay_;
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x0600142F RID: 5167 RVA: 0x00045C94 File Offset: 0x00043E94
		public bool HasCreatorID
		{
			get
			{
				return this.hasCreatorID;
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001430 RID: 5168 RVA: 0x00045C9C File Offset: 0x00043E9C
		[CLSCompliant(false)]
		public ulong CreatorID
		{
			get
			{
				return this.creatorID_;
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06001431 RID: 5169 RVA: 0x00045CA4 File Offset: 0x00043EA4
		public bool HasOwnerID
		{
			get
			{
				return this.hasOwnerID;
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001432 RID: 5170 RVA: 0x00045CAC File Offset: 0x00043EAC
		[CLSCompliant(false)]
		public ulong OwnerID
		{
			get
			{
				return this.ownerID_;
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06001433 RID: 5171 RVA: 0x00045CB4 File Offset: 0x00043EB4
		public override bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x00045CB8 File Offset: 0x00043EB8
		public override void WriteTo(ICodedOutputStream output)
		{
			int serializedSize = this.SerializedSize;
			string[] objectStructMasterFieldNames = objectStructMaster._objectStructMasterFieldNames;
			if (this.hasID)
			{
				output.WriteInt32(1, objectStructMasterFieldNames[2], this.ID);
			}
			if (this.hasDecayDelay)
			{
				output.WriteFloat(2, objectStructMasterFieldNames[1], this.DecayDelay);
			}
			if (this.hasCreatorID)
			{
				output.WriteUInt64(3, objectStructMasterFieldNames[0], this.CreatorID);
			}
			if (this.hasOwnerID)
			{
				output.WriteUInt64(4, objectStructMasterFieldNames[3], this.OwnerID);
			}
			this.UnknownFields.WriteTo(output);
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06001435 RID: 5173 RVA: 0x00045D4C File Offset: 0x00043F4C
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
				if (this.hasID)
				{
					num += CodedOutputStream.ComputeInt32Size(1, this.ID);
				}
				if (this.hasDecayDelay)
				{
					num += CodedOutputStream.ComputeFloatSize(2, this.DecayDelay);
				}
				if (this.hasCreatorID)
				{
					num += CodedOutputStream.ComputeUInt64Size(3, this.CreatorID);
				}
				if (this.hasOwnerID)
				{
					num += CodedOutputStream.ComputeUInt64Size(4, this.OwnerID);
				}
				num += this.UnknownFields.SerializedSize;
				this.memoizedSerializedSize = num;
				return num;
			}
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x00045DEC File Offset: 0x00043FEC
		public static objectStructMaster ParseFrom(ByteString data)
		{
			return objectStructMaster.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x00045E00 File Offset: 0x00044000
		public static objectStructMaster ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return objectStructMaster.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x00045E14 File Offset: 0x00044014
		public static objectStructMaster ParseFrom(byte[] data)
		{
			return objectStructMaster.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x00045E28 File Offset: 0x00044028
		public static objectStructMaster ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return objectStructMaster.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x00045E3C File Offset: 0x0004403C
		public static objectStructMaster ParseFrom(Stream input)
		{
			return objectStructMaster.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x00045E50 File Offset: 0x00044050
		public static objectStructMaster ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectStructMaster.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x00045E64 File Offset: 0x00044064
		public static objectStructMaster ParseDelimitedFrom(Stream input)
		{
			return objectStructMaster.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x00045E78 File Offset: 0x00044078
		public static objectStructMaster ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectStructMaster.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x00045E8C File Offset: 0x0004408C
		public static objectStructMaster ParseFrom(ICodedInputStream input)
		{
			return objectStructMaster.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x00045EA0 File Offset: 0x000440A0
		public static objectStructMaster ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return objectStructMaster.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001440 RID: 5184 RVA: 0x00045EB4 File Offset: 0x000440B4
		private objectStructMaster MakeReadOnly()
		{
			return this;
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x00045EB8 File Offset: 0x000440B8
		public static objectStructMaster.Builder CreateBuilder()
		{
			return new objectStructMaster.Builder();
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x00045EC0 File Offset: 0x000440C0
		public override objectStructMaster.Builder ToBuilder()
		{
			return objectStructMaster.CreateBuilder(this);
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x00045EC8 File Offset: 0x000440C8
		public override objectStructMaster.Builder CreateBuilderForType()
		{
			return new objectStructMaster.Builder();
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x00045ED0 File Offset: 0x000440D0
		public static objectStructMaster.Builder CreateBuilder(objectStructMaster prototype)
		{
			return new objectStructMaster.Builder(prototype);
		}

		// Token: 0x04000ADF RID: 2783
		public const int IDFieldNumber = 1;

		// Token: 0x04000AE0 RID: 2784
		public const int DecayDelayFieldNumber = 2;

		// Token: 0x04000AE1 RID: 2785
		public const int CreatorIDFieldNumber = 3;

		// Token: 0x04000AE2 RID: 2786
		public const int OwnerIDFieldNumber = 4;

		// Token: 0x04000AE3 RID: 2787
		private static readonly objectStructMaster defaultInstance = new objectStructMaster().MakeReadOnly();

		// Token: 0x04000AE4 RID: 2788
		private static readonly string[] _objectStructMasterFieldNames = new string[]
		{
			"CreatorID",
			"DecayDelay",
			"ID",
			"OwnerID"
		};

		// Token: 0x04000AE5 RID: 2789
		private static readonly uint[] _objectStructMasterFieldTags = new uint[]
		{
			24u,
			21u,
			8u,
			32u
		};

		// Token: 0x04000AE6 RID: 2790
		private bool hasID;

		// Token: 0x04000AE7 RID: 2791
		private int iD_;

		// Token: 0x04000AE8 RID: 2792
		private bool hasDecayDelay;

		// Token: 0x04000AE9 RID: 2793
		private float decayDelay_;

		// Token: 0x04000AEA RID: 2794
		private bool hasCreatorID;

		// Token: 0x04000AEB RID: 2795
		private ulong creatorID_;

		// Token: 0x04000AEC RID: 2796
		private bool hasOwnerID;

		// Token: 0x04000AED RID: 2797
		private ulong ownerID_;

		// Token: 0x04000AEE RID: 2798
		private int memoizedSerializedSize = -1;

		// Token: 0x02000251 RID: 593
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<objectStructMaster, objectStructMaster.Builder>
		{
			// Token: 0x06001445 RID: 5189 RVA: 0x00045ED8 File Offset: 0x000440D8
			public Builder()
			{
				this.result = objectStructMaster.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x06001446 RID: 5190 RVA: 0x00045EF4 File Offset: 0x000440F4
			internal Builder(objectStructMaster cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x17000599 RID: 1433
			// (get) Token: 0x06001447 RID: 5191 RVA: 0x00045F0C File Offset: 0x0004410C
			protected override objectStructMaster.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x06001448 RID: 5192 RVA: 0x00045F10 File Offset: 0x00044110
			private objectStructMaster PrepareBuilder()
			{
				if (this.resultIsReadOnly)
				{
					objectStructMaster other = this.result;
					this.result = new objectStructMaster();
					this.resultIsReadOnly = false;
					this.MergeFrom(other);
				}
				return this.result;
			}

			// Token: 0x1700059A RID: 1434
			// (get) Token: 0x06001449 RID: 5193 RVA: 0x00045F50 File Offset: 0x00044150
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x1700059B RID: 1435
			// (get) Token: 0x0600144A RID: 5194 RVA: 0x00045F60 File Offset: 0x00044160
			protected override objectStructMaster MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x0600144B RID: 5195 RVA: 0x00045F68 File Offset: 0x00044168
			public override objectStructMaster.Builder Clear()
			{
				this.result = objectStructMaster.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x0600144C RID: 5196 RVA: 0x00045F80 File Offset: 0x00044180
			public override objectStructMaster.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new objectStructMaster.Builder(this.result);
				}
				return new objectStructMaster.Builder().MergeFrom(this.result);
			}

			// Token: 0x1700059C RID: 1436
			// (get) Token: 0x0600144D RID: 5197 RVA: 0x00045FAC File Offset: 0x000441AC
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return objectStructMaster.Descriptor;
				}
			}

			// Token: 0x1700059D RID: 1437
			// (get) Token: 0x0600144E RID: 5198 RVA: 0x00045FB4 File Offset: 0x000441B4
			public override objectStructMaster DefaultInstanceForType
			{
				get
				{
					return objectStructMaster.DefaultInstance;
				}
			}

			// Token: 0x0600144F RID: 5199 RVA: 0x00045FBC File Offset: 0x000441BC
			public override objectStructMaster BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x06001450 RID: 5200 RVA: 0x00045FF0 File Offset: 0x000441F0
			public override objectStructMaster.Builder MergeFrom(IMessage other)
			{
				if (other is objectStructMaster)
				{
					return this.MergeFrom((objectStructMaster)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x06001451 RID: 5201 RVA: 0x00046014 File Offset: 0x00044214
			public override objectStructMaster.Builder MergeFrom(objectStructMaster other)
			{
				if (other == objectStructMaster.DefaultInstance)
				{
					return this;
				}
				this.PrepareBuilder();
				if (other.HasID)
				{
					this.ID = other.ID;
				}
				if (other.HasDecayDelay)
				{
					this.DecayDelay = other.DecayDelay;
				}
				if (other.HasCreatorID)
				{
					this.CreatorID = other.CreatorID;
				}
				if (other.HasOwnerID)
				{
					this.OwnerID = other.OwnerID;
				}
				this.MergeUnknownFields(other.UnknownFields);
				return this;
			}

			// Token: 0x06001452 RID: 5202 RVA: 0x000460A0 File Offset: 0x000442A0
			public override objectStructMaster.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x06001453 RID: 5203 RVA: 0x000460B0 File Offset: 0x000442B0
			public override objectStructMaster.Builder MergeFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
			{
				this.PrepareBuilder();
				UnknownFieldSet.Builder builder = null;
				uint num;
				string text;
				while (input.ReadTag(ref num, ref text))
				{
					if (num == 0u && text != null)
					{
						int num2 = Array.BinarySearch<string>(objectStructMaster._objectStructMasterFieldNames, text, StringComparer.Ordinal);
						if (num2 < 0)
						{
							if (builder == null)
							{
								builder = UnknownFieldSet.CreateBuilder(this.UnknownFields);
							}
							this.ParseUnknownField(input, builder, extensionRegistry, num, text);
							continue;
						}
						num = objectStructMaster._objectStructMasterFieldTags[num2];
					}
					uint num3 = num;
					switch (num3)
					{
					case 21u:
						this.result.hasDecayDelay = input.ReadFloat(ref this.result.decayDelay_);
						break;
					default:
						if (num3 == 0u)
						{
							throw InvalidProtocolBufferException.InvalidTag();
						}
						if (num3 != 8u)
						{
							if (num3 != 32u)
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
								this.result.hasOwnerID = input.ReadUInt64(ref this.result.ownerID_);
							}
						}
						else
						{
							this.result.hasID = input.ReadInt32(ref this.result.iD_);
						}
						break;
					case 24u:
						this.result.hasCreatorID = input.ReadUInt64(ref this.result.creatorID_);
						break;
					}
				}
				if (builder != null)
				{
					this.UnknownFields = builder.Build();
				}
				return this;
			}

			// Token: 0x1700059E RID: 1438
			// (get) Token: 0x06001454 RID: 5204 RVA: 0x00046248 File Offset: 0x00044448
			public bool HasID
			{
				get
				{
					return this.result.hasID;
				}
			}

			// Token: 0x1700059F RID: 1439
			// (get) Token: 0x06001455 RID: 5205 RVA: 0x00046258 File Offset: 0x00044458
			// (set) Token: 0x06001456 RID: 5206 RVA: 0x00046268 File Offset: 0x00044468
			public int ID
			{
				get
				{
					return this.result.ID;
				}
				set
				{
					this.SetID(value);
				}
			}

			// Token: 0x06001457 RID: 5207 RVA: 0x00046274 File Offset: 0x00044474
			public objectStructMaster.Builder SetID(int value)
			{
				this.PrepareBuilder();
				this.result.hasID = true;
				this.result.iD_ = value;
				return this;
			}

			// Token: 0x06001458 RID: 5208 RVA: 0x000462A4 File Offset: 0x000444A4
			public objectStructMaster.Builder ClearID()
			{
				this.PrepareBuilder();
				this.result.hasID = false;
				this.result.iD_ = 0;
				return this;
			}

			// Token: 0x170005A0 RID: 1440
			// (get) Token: 0x06001459 RID: 5209 RVA: 0x000462D4 File Offset: 0x000444D4
			public bool HasDecayDelay
			{
				get
				{
					return this.result.hasDecayDelay;
				}
			}

			// Token: 0x170005A1 RID: 1441
			// (get) Token: 0x0600145A RID: 5210 RVA: 0x000462E4 File Offset: 0x000444E4
			// (set) Token: 0x0600145B RID: 5211 RVA: 0x000462F4 File Offset: 0x000444F4
			public float DecayDelay
			{
				get
				{
					return this.result.DecayDelay;
				}
				set
				{
					this.SetDecayDelay(value);
				}
			}

			// Token: 0x0600145C RID: 5212 RVA: 0x00046300 File Offset: 0x00044500
			public objectStructMaster.Builder SetDecayDelay(float value)
			{
				this.PrepareBuilder();
				this.result.hasDecayDelay = true;
				this.result.decayDelay_ = value;
				return this;
			}

			// Token: 0x0600145D RID: 5213 RVA: 0x00046330 File Offset: 0x00044530
			public objectStructMaster.Builder ClearDecayDelay()
			{
				this.PrepareBuilder();
				this.result.hasDecayDelay = false;
				this.result.decayDelay_ = 0f;
				return this;
			}

			// Token: 0x170005A2 RID: 1442
			// (get) Token: 0x0600145E RID: 5214 RVA: 0x00046364 File Offset: 0x00044564
			public bool HasCreatorID
			{
				get
				{
					return this.result.hasCreatorID;
				}
			}

			// Token: 0x170005A3 RID: 1443
			// (get) Token: 0x0600145F RID: 5215 RVA: 0x00046374 File Offset: 0x00044574
			// (set) Token: 0x06001460 RID: 5216 RVA: 0x00046384 File Offset: 0x00044584
			[CLSCompliant(false)]
			public ulong CreatorID
			{
				get
				{
					return this.result.CreatorID;
				}
				set
				{
					this.SetCreatorID(value);
				}
			}

			// Token: 0x06001461 RID: 5217 RVA: 0x00046390 File Offset: 0x00044590
			[CLSCompliant(false)]
			public objectStructMaster.Builder SetCreatorID(ulong value)
			{
				this.PrepareBuilder();
				this.result.hasCreatorID = true;
				this.result.creatorID_ = value;
				return this;
			}

			// Token: 0x06001462 RID: 5218 RVA: 0x000463C0 File Offset: 0x000445C0
			public objectStructMaster.Builder ClearCreatorID()
			{
				this.PrepareBuilder();
				this.result.hasCreatorID = false;
				this.result.creatorID_ = 0UL;
				return this;
			}

			// Token: 0x170005A4 RID: 1444
			// (get) Token: 0x06001463 RID: 5219 RVA: 0x000463E4 File Offset: 0x000445E4
			public bool HasOwnerID
			{
				get
				{
					return this.result.hasOwnerID;
				}
			}

			// Token: 0x170005A5 RID: 1445
			// (get) Token: 0x06001464 RID: 5220 RVA: 0x000463F4 File Offset: 0x000445F4
			// (set) Token: 0x06001465 RID: 5221 RVA: 0x00046404 File Offset: 0x00044604
			[CLSCompliant(false)]
			public ulong OwnerID
			{
				get
				{
					return this.result.OwnerID;
				}
				set
				{
					this.SetOwnerID(value);
				}
			}

			// Token: 0x06001466 RID: 5222 RVA: 0x00046410 File Offset: 0x00044610
			[CLSCompliant(false)]
			public objectStructMaster.Builder SetOwnerID(ulong value)
			{
				this.PrepareBuilder();
				this.result.hasOwnerID = true;
				this.result.ownerID_ = value;
				return this;
			}

			// Token: 0x06001467 RID: 5223 RVA: 0x00046440 File Offset: 0x00044640
			public objectStructMaster.Builder ClearOwnerID()
			{
				this.PrepareBuilder();
				this.result.hasOwnerID = false;
				this.result.ownerID_ = 0UL;
				return this;
			}

			// Token: 0x04000AEF RID: 2799
			private bool resultIsReadOnly;

			// Token: 0x04000AF0 RID: 2800
			private objectStructMaster result;
		}
	}
}
