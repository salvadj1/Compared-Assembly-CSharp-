using System;
using System.Diagnostics;
using System.IO;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;
using RustProto.Helpers;

namespace RustProto
{
	// Token: 0x0200021D RID: 541
	[DebuggerNonUserCode]
	public sealed class objectStructMaster : GeneratedMessage<objectStructMaster, objectStructMaster.Builder>
	{
		// Token: 0x060012CF RID: 4815 RVA: 0x00041824 File Offset: 0x0003FA24
		private objectStructMaster()
		{
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x00041834 File Offset: 0x0003FA34
		static objectStructMaster()
		{
			object.ReferenceEquals(Worldsave.Descriptor, null);
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x000418A0 File Offset: 0x0003FAA0
		public static Recycler<objectStructMaster, objectStructMaster.Builder> Recycler()
		{
			return Recycler<objectStructMaster, objectStructMaster.Builder>.Manufacture();
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x060012D2 RID: 4818 RVA: 0x000418A8 File Offset: 0x0003FAA8
		public static objectStructMaster DefaultInstance
		{
			get
			{
				return objectStructMaster.defaultInstance;
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x060012D3 RID: 4819 RVA: 0x000418B0 File Offset: 0x0003FAB0
		public override objectStructMaster DefaultInstanceForType
		{
			get
			{
				return objectStructMaster.DefaultInstance;
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x060012D4 RID: 4820 RVA: 0x000418B8 File Offset: 0x0003FAB8
		protected override objectStructMaster ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x060012D5 RID: 4821 RVA: 0x000418BC File Offset: 0x0003FABC
		public static MessageDescriptor Descriptor
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectStructMaster__Descriptor;
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x060012D6 RID: 4822 RVA: 0x000418C4 File Offset: 0x0003FAC4
		protected override FieldAccessorTable<objectStructMaster, objectStructMaster.Builder> InternalFieldAccessors
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectStructMaster__FieldAccessorTable;
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x060012D7 RID: 4823 RVA: 0x000418CC File Offset: 0x0003FACC
		public bool HasID
		{
			get
			{
				return this.hasID;
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x060012D8 RID: 4824 RVA: 0x000418D4 File Offset: 0x0003FAD4
		public int ID
		{
			get
			{
				return this.iD_;
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x060012D9 RID: 4825 RVA: 0x000418DC File Offset: 0x0003FADC
		public bool HasDecayDelay
		{
			get
			{
				return this.hasDecayDelay;
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x060012DA RID: 4826 RVA: 0x000418E4 File Offset: 0x0003FAE4
		public float DecayDelay
		{
			get
			{
				return this.decayDelay_;
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x060012DB RID: 4827 RVA: 0x000418EC File Offset: 0x0003FAEC
		public bool HasCreatorID
		{
			get
			{
				return this.hasCreatorID;
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x060012DC RID: 4828 RVA: 0x000418F4 File Offset: 0x0003FAF4
		[CLSCompliant(false)]
		public ulong CreatorID
		{
			get
			{
				return this.creatorID_;
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x060012DD RID: 4829 RVA: 0x000418FC File Offset: 0x0003FAFC
		public bool HasOwnerID
		{
			get
			{
				return this.hasOwnerID;
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x060012DE RID: 4830 RVA: 0x00041904 File Offset: 0x0003FB04
		[CLSCompliant(false)]
		public ulong OwnerID
		{
			get
			{
				return this.ownerID_;
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x060012DF RID: 4831 RVA: 0x0004190C File Offset: 0x0003FB0C
		public override bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x00041910 File Offset: 0x0003FB10
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

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x060012E1 RID: 4833 RVA: 0x000419A4 File Offset: 0x0003FBA4
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

		// Token: 0x060012E2 RID: 4834 RVA: 0x00041A44 File Offset: 0x0003FC44
		public static objectStructMaster ParseFrom(ByteString data)
		{
			return objectStructMaster.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x00041A58 File Offset: 0x0003FC58
		public static objectStructMaster ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return objectStructMaster.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x00041A6C File Offset: 0x0003FC6C
		public static objectStructMaster ParseFrom(byte[] data)
		{
			return objectStructMaster.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x00041A80 File Offset: 0x0003FC80
		public static objectStructMaster ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return objectStructMaster.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x00041A94 File Offset: 0x0003FC94
		public static objectStructMaster ParseFrom(Stream input)
		{
			return objectStructMaster.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x00041AA8 File Offset: 0x0003FCA8
		public static objectStructMaster ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectStructMaster.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x00041ABC File Offset: 0x0003FCBC
		public static objectStructMaster ParseDelimitedFrom(Stream input)
		{
			return objectStructMaster.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x00041AD0 File Offset: 0x0003FCD0
		public static objectStructMaster ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectStructMaster.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x00041AE4 File Offset: 0x0003FCE4
		public static objectStructMaster ParseFrom(ICodedInputStream input)
		{
			return objectStructMaster.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x00041AF8 File Offset: 0x0003FCF8
		public static objectStructMaster ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return objectStructMaster.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x00041B0C File Offset: 0x0003FD0C
		private objectStructMaster MakeReadOnly()
		{
			return this;
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x00041B10 File Offset: 0x0003FD10
		public static objectStructMaster.Builder CreateBuilder()
		{
			return new objectStructMaster.Builder();
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x00041B18 File Offset: 0x0003FD18
		public override objectStructMaster.Builder ToBuilder()
		{
			return objectStructMaster.CreateBuilder(this);
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x00041B20 File Offset: 0x0003FD20
		public override objectStructMaster.Builder CreateBuilderForType()
		{
			return new objectStructMaster.Builder();
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x00041B28 File Offset: 0x0003FD28
		public static objectStructMaster.Builder CreateBuilder(objectStructMaster prototype)
		{
			return new objectStructMaster.Builder(prototype);
		}

		// Token: 0x040009BC RID: 2492
		public const int IDFieldNumber = 1;

		// Token: 0x040009BD RID: 2493
		public const int DecayDelayFieldNumber = 2;

		// Token: 0x040009BE RID: 2494
		public const int CreatorIDFieldNumber = 3;

		// Token: 0x040009BF RID: 2495
		public const int OwnerIDFieldNumber = 4;

		// Token: 0x040009C0 RID: 2496
		private static readonly objectStructMaster defaultInstance = new objectStructMaster().MakeReadOnly();

		// Token: 0x040009C1 RID: 2497
		private static readonly string[] _objectStructMasterFieldNames = new string[]
		{
			"CreatorID",
			"DecayDelay",
			"ID",
			"OwnerID"
		};

		// Token: 0x040009C2 RID: 2498
		private static readonly uint[] _objectStructMasterFieldTags = new uint[]
		{
			24u,
			21u,
			8u,
			32u
		};

		// Token: 0x040009C3 RID: 2499
		private bool hasID;

		// Token: 0x040009C4 RID: 2500
		private int iD_;

		// Token: 0x040009C5 RID: 2501
		private bool hasDecayDelay;

		// Token: 0x040009C6 RID: 2502
		private float decayDelay_;

		// Token: 0x040009C7 RID: 2503
		private bool hasCreatorID;

		// Token: 0x040009C8 RID: 2504
		private ulong creatorID_;

		// Token: 0x040009C9 RID: 2505
		private bool hasOwnerID;

		// Token: 0x040009CA RID: 2506
		private ulong ownerID_;

		// Token: 0x040009CB RID: 2507
		private int memoizedSerializedSize = -1;

		// Token: 0x0200021E RID: 542
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<objectStructMaster, objectStructMaster.Builder>
		{
			// Token: 0x060012F1 RID: 4849 RVA: 0x00041B30 File Offset: 0x0003FD30
			public Builder()
			{
				this.result = objectStructMaster.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x060012F2 RID: 4850 RVA: 0x00041B4C File Offset: 0x0003FD4C
			internal Builder(objectStructMaster cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x17000551 RID: 1361
			// (get) Token: 0x060012F3 RID: 4851 RVA: 0x00041B64 File Offset: 0x0003FD64
			protected override objectStructMaster.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x060012F4 RID: 4852 RVA: 0x00041B68 File Offset: 0x0003FD68
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

			// Token: 0x17000552 RID: 1362
			// (get) Token: 0x060012F5 RID: 4853 RVA: 0x00041BA8 File Offset: 0x0003FDA8
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x17000553 RID: 1363
			// (get) Token: 0x060012F6 RID: 4854 RVA: 0x00041BB8 File Offset: 0x0003FDB8
			protected override objectStructMaster MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x060012F7 RID: 4855 RVA: 0x00041BC0 File Offset: 0x0003FDC0
			public override objectStructMaster.Builder Clear()
			{
				this.result = objectStructMaster.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x060012F8 RID: 4856 RVA: 0x00041BD8 File Offset: 0x0003FDD8
			public override objectStructMaster.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new objectStructMaster.Builder(this.result);
				}
				return new objectStructMaster.Builder().MergeFrom(this.result);
			}

			// Token: 0x17000554 RID: 1364
			// (get) Token: 0x060012F9 RID: 4857 RVA: 0x00041C04 File Offset: 0x0003FE04
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return objectStructMaster.Descriptor;
				}
			}

			// Token: 0x17000555 RID: 1365
			// (get) Token: 0x060012FA RID: 4858 RVA: 0x00041C0C File Offset: 0x0003FE0C
			public override objectStructMaster DefaultInstanceForType
			{
				get
				{
					return objectStructMaster.DefaultInstance;
				}
			}

			// Token: 0x060012FB RID: 4859 RVA: 0x00041C14 File Offset: 0x0003FE14
			public override objectStructMaster BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x060012FC RID: 4860 RVA: 0x00041C48 File Offset: 0x0003FE48
			public override objectStructMaster.Builder MergeFrom(IMessage other)
			{
				if (other is objectStructMaster)
				{
					return this.MergeFrom((objectStructMaster)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x060012FD RID: 4861 RVA: 0x00041C6C File Offset: 0x0003FE6C
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

			// Token: 0x060012FE RID: 4862 RVA: 0x00041CF8 File Offset: 0x0003FEF8
			public override objectStructMaster.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x060012FF RID: 4863 RVA: 0x00041D08 File Offset: 0x0003FF08
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

			// Token: 0x17000556 RID: 1366
			// (get) Token: 0x06001300 RID: 4864 RVA: 0x00041EA0 File Offset: 0x000400A0
			public bool HasID
			{
				get
				{
					return this.result.hasID;
				}
			}

			// Token: 0x17000557 RID: 1367
			// (get) Token: 0x06001301 RID: 4865 RVA: 0x00041EB0 File Offset: 0x000400B0
			// (set) Token: 0x06001302 RID: 4866 RVA: 0x00041EC0 File Offset: 0x000400C0
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

			// Token: 0x06001303 RID: 4867 RVA: 0x00041ECC File Offset: 0x000400CC
			public objectStructMaster.Builder SetID(int value)
			{
				this.PrepareBuilder();
				this.result.hasID = true;
				this.result.iD_ = value;
				return this;
			}

			// Token: 0x06001304 RID: 4868 RVA: 0x00041EFC File Offset: 0x000400FC
			public objectStructMaster.Builder ClearID()
			{
				this.PrepareBuilder();
				this.result.hasID = false;
				this.result.iD_ = 0;
				return this;
			}

			// Token: 0x17000558 RID: 1368
			// (get) Token: 0x06001305 RID: 4869 RVA: 0x00041F2C File Offset: 0x0004012C
			public bool HasDecayDelay
			{
				get
				{
					return this.result.hasDecayDelay;
				}
			}

			// Token: 0x17000559 RID: 1369
			// (get) Token: 0x06001306 RID: 4870 RVA: 0x00041F3C File Offset: 0x0004013C
			// (set) Token: 0x06001307 RID: 4871 RVA: 0x00041F4C File Offset: 0x0004014C
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

			// Token: 0x06001308 RID: 4872 RVA: 0x00041F58 File Offset: 0x00040158
			public objectStructMaster.Builder SetDecayDelay(float value)
			{
				this.PrepareBuilder();
				this.result.hasDecayDelay = true;
				this.result.decayDelay_ = value;
				return this;
			}

			// Token: 0x06001309 RID: 4873 RVA: 0x00041F88 File Offset: 0x00040188
			public objectStructMaster.Builder ClearDecayDelay()
			{
				this.PrepareBuilder();
				this.result.hasDecayDelay = false;
				this.result.decayDelay_ = 0f;
				return this;
			}

			// Token: 0x1700055A RID: 1370
			// (get) Token: 0x0600130A RID: 4874 RVA: 0x00041FBC File Offset: 0x000401BC
			public bool HasCreatorID
			{
				get
				{
					return this.result.hasCreatorID;
				}
			}

			// Token: 0x1700055B RID: 1371
			// (get) Token: 0x0600130B RID: 4875 RVA: 0x00041FCC File Offset: 0x000401CC
			// (set) Token: 0x0600130C RID: 4876 RVA: 0x00041FDC File Offset: 0x000401DC
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

			// Token: 0x0600130D RID: 4877 RVA: 0x00041FE8 File Offset: 0x000401E8
			[CLSCompliant(false)]
			public objectStructMaster.Builder SetCreatorID(ulong value)
			{
				this.PrepareBuilder();
				this.result.hasCreatorID = true;
				this.result.creatorID_ = value;
				return this;
			}

			// Token: 0x0600130E RID: 4878 RVA: 0x00042018 File Offset: 0x00040218
			public objectStructMaster.Builder ClearCreatorID()
			{
				this.PrepareBuilder();
				this.result.hasCreatorID = false;
				this.result.creatorID_ = 0UL;
				return this;
			}

			// Token: 0x1700055C RID: 1372
			// (get) Token: 0x0600130F RID: 4879 RVA: 0x0004203C File Offset: 0x0004023C
			public bool HasOwnerID
			{
				get
				{
					return this.result.hasOwnerID;
				}
			}

			// Token: 0x1700055D RID: 1373
			// (get) Token: 0x06001310 RID: 4880 RVA: 0x0004204C File Offset: 0x0004024C
			// (set) Token: 0x06001311 RID: 4881 RVA: 0x0004205C File Offset: 0x0004025C
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

			// Token: 0x06001312 RID: 4882 RVA: 0x00042068 File Offset: 0x00040268
			[CLSCompliant(false)]
			public objectStructMaster.Builder SetOwnerID(ulong value)
			{
				this.PrepareBuilder();
				this.result.hasOwnerID = true;
				this.result.ownerID_ = value;
				return this;
			}

			// Token: 0x06001313 RID: 4883 RVA: 0x00042098 File Offset: 0x00040298
			public objectStructMaster.Builder ClearOwnerID()
			{
				this.PrepareBuilder();
				this.result.hasOwnerID = false;
				this.result.ownerID_ = 0UL;
				return this;
			}

			// Token: 0x040009CC RID: 2508
			private bool resultIsReadOnly;

			// Token: 0x040009CD RID: 2509
			private objectStructMaster result;
		}
	}
}
