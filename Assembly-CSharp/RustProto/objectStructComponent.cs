using System;
using System.Diagnostics;
using System.IO;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;
using RustProto.Helpers;

namespace RustProto
{
	// Token: 0x0200021B RID: 539
	[DebuggerNonUserCode]
	public sealed class objectStructComponent : GeneratedMessage<objectStructComponent, objectStructComponent.Builder>
	{
		// Token: 0x06001291 RID: 4753 RVA: 0x0004109C File Offset: 0x0003F29C
		private objectStructComponent()
		{
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x000410AC File Offset: 0x0003F2AC
		static objectStructComponent()
		{
			object.ReferenceEquals(Worldsave.Descriptor, null);
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x00041110 File Offset: 0x0003F310
		public static Recycler<objectStructComponent, objectStructComponent.Builder> Recycler()
		{
			return Recycler<objectStructComponent, objectStructComponent.Builder>.Manufacture();
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06001294 RID: 4756 RVA: 0x00041118 File Offset: 0x0003F318
		public static objectStructComponent DefaultInstance
		{
			get
			{
				return objectStructComponent.defaultInstance;
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06001295 RID: 4757 RVA: 0x00041120 File Offset: 0x0003F320
		public override objectStructComponent DefaultInstanceForType
		{
			get
			{
				return objectStructComponent.DefaultInstance;
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06001296 RID: 4758 RVA: 0x00041128 File Offset: 0x0003F328
		protected override objectStructComponent ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06001297 RID: 4759 RVA: 0x0004112C File Offset: 0x0003F32C
		public static MessageDescriptor Descriptor
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectStructComponent__Descriptor;
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06001298 RID: 4760 RVA: 0x00041134 File Offset: 0x0003F334
		protected override FieldAccessorTable<objectStructComponent, objectStructComponent.Builder> InternalFieldAccessors
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectStructComponent__FieldAccessorTable;
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06001299 RID: 4761 RVA: 0x0004113C File Offset: 0x0003F33C
		public bool HasID
		{
			get
			{
				return this.hasID;
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x0600129A RID: 4762 RVA: 0x00041144 File Offset: 0x0003F344
		public int ID
		{
			get
			{
				return this.iD_;
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x0600129B RID: 4763 RVA: 0x0004114C File Offset: 0x0003F34C
		public bool HasMasterID
		{
			get
			{
				return this.hasMasterID;
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x0600129C RID: 4764 RVA: 0x00041154 File Offset: 0x0003F354
		public int MasterID
		{
			get
			{
				return this.masterID_;
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x0600129D RID: 4765 RVA: 0x0004115C File Offset: 0x0003F35C
		public bool HasMasterViewID
		{
			get
			{
				return this.hasMasterViewID;
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x0600129E RID: 4766 RVA: 0x00041164 File Offset: 0x0003F364
		public int MasterViewID
		{
			get
			{
				return this.masterViewID_;
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x0600129F RID: 4767 RVA: 0x0004116C File Offset: 0x0003F36C
		public override bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x00041170 File Offset: 0x0003F370
		public override void WriteTo(ICodedOutputStream output)
		{
			int serializedSize = this.SerializedSize;
			string[] objectStructComponentFieldNames = objectStructComponent._objectStructComponentFieldNames;
			if (this.hasID)
			{
				output.WriteInt32(1, objectStructComponentFieldNames[0], this.ID);
			}
			if (this.hasMasterID)
			{
				output.WriteInt32(2, objectStructComponentFieldNames[1], this.MasterID);
			}
			if (this.hasMasterViewID)
			{
				output.WriteInt32(3, objectStructComponentFieldNames[2], this.MasterViewID);
			}
			this.UnknownFields.WriteTo(output);
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x060012A1 RID: 4769 RVA: 0x000411E8 File Offset: 0x0003F3E8
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
				if (this.hasMasterID)
				{
					num += CodedOutputStream.ComputeInt32Size(2, this.MasterID);
				}
				if (this.hasMasterViewID)
				{
					num += CodedOutputStream.ComputeInt32Size(3, this.MasterViewID);
				}
				num += this.UnknownFields.SerializedSize;
				this.memoizedSerializedSize = num;
				return num;
			}
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x0004126C File Offset: 0x0003F46C
		public static objectStructComponent ParseFrom(ByteString data)
		{
			return objectStructComponent.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x00041280 File Offset: 0x0003F480
		public static objectStructComponent ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return objectStructComponent.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x00041294 File Offset: 0x0003F494
		public static objectStructComponent ParseFrom(byte[] data)
		{
			return objectStructComponent.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x000412A8 File Offset: 0x0003F4A8
		public static objectStructComponent ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return objectStructComponent.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x000412BC File Offset: 0x0003F4BC
		public static objectStructComponent ParseFrom(Stream input)
		{
			return objectStructComponent.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x000412D0 File Offset: 0x0003F4D0
		public static objectStructComponent ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectStructComponent.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x000412E4 File Offset: 0x0003F4E4
		public static objectStructComponent ParseDelimitedFrom(Stream input)
		{
			return objectStructComponent.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x000412F8 File Offset: 0x0003F4F8
		public static objectStructComponent ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectStructComponent.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x0004130C File Offset: 0x0003F50C
		public static objectStructComponent ParseFrom(ICodedInputStream input)
		{
			return objectStructComponent.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x00041320 File Offset: 0x0003F520
		public static objectStructComponent ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return objectStructComponent.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x00041334 File Offset: 0x0003F534
		private objectStructComponent MakeReadOnly()
		{
			return this;
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x00041338 File Offset: 0x0003F538
		public static objectStructComponent.Builder CreateBuilder()
		{
			return new objectStructComponent.Builder();
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x00041340 File Offset: 0x0003F540
		public override objectStructComponent.Builder ToBuilder()
		{
			return objectStructComponent.CreateBuilder(this);
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x00041348 File Offset: 0x0003F548
		public override objectStructComponent.Builder CreateBuilderForType()
		{
			return new objectStructComponent.Builder();
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x00041350 File Offset: 0x0003F550
		public static objectStructComponent.Builder CreateBuilder(objectStructComponent prototype)
		{
			return new objectStructComponent.Builder(prototype);
		}

		// Token: 0x040009AD RID: 2477
		public const int IDFieldNumber = 1;

		// Token: 0x040009AE RID: 2478
		public const int MasterIDFieldNumber = 2;

		// Token: 0x040009AF RID: 2479
		public const int MasterViewIDFieldNumber = 3;

		// Token: 0x040009B0 RID: 2480
		private static readonly objectStructComponent defaultInstance = new objectStructComponent().MakeReadOnly();

		// Token: 0x040009B1 RID: 2481
		private static readonly string[] _objectStructComponentFieldNames = new string[]
		{
			"ID",
			"MasterID",
			"MasterViewID"
		};

		// Token: 0x040009B2 RID: 2482
		private static readonly uint[] _objectStructComponentFieldTags = new uint[]
		{
			8u,
			16u,
			24u
		};

		// Token: 0x040009B3 RID: 2483
		private bool hasID;

		// Token: 0x040009B4 RID: 2484
		private int iD_;

		// Token: 0x040009B5 RID: 2485
		private bool hasMasterID;

		// Token: 0x040009B6 RID: 2486
		private int masterID_;

		// Token: 0x040009B7 RID: 2487
		private bool hasMasterViewID;

		// Token: 0x040009B8 RID: 2488
		private int masterViewID_;

		// Token: 0x040009B9 RID: 2489
		private int memoizedSerializedSize = -1;

		// Token: 0x0200021C RID: 540
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<objectStructComponent, objectStructComponent.Builder>
		{
			// Token: 0x060012B1 RID: 4785 RVA: 0x00041358 File Offset: 0x0003F558
			public Builder()
			{
				this.result = objectStructComponent.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x060012B2 RID: 4786 RVA: 0x00041374 File Offset: 0x0003F574
			internal Builder(objectStructComponent cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x17000537 RID: 1335
			// (get) Token: 0x060012B3 RID: 4787 RVA: 0x0004138C File Offset: 0x0003F58C
			protected override objectStructComponent.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x060012B4 RID: 4788 RVA: 0x00041390 File Offset: 0x0003F590
			private objectStructComponent PrepareBuilder()
			{
				if (this.resultIsReadOnly)
				{
					objectStructComponent other = this.result;
					this.result = new objectStructComponent();
					this.resultIsReadOnly = false;
					this.MergeFrom(other);
				}
				return this.result;
			}

			// Token: 0x17000538 RID: 1336
			// (get) Token: 0x060012B5 RID: 4789 RVA: 0x000413D0 File Offset: 0x0003F5D0
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x17000539 RID: 1337
			// (get) Token: 0x060012B6 RID: 4790 RVA: 0x000413E0 File Offset: 0x0003F5E0
			protected override objectStructComponent MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x060012B7 RID: 4791 RVA: 0x000413E8 File Offset: 0x0003F5E8
			public override objectStructComponent.Builder Clear()
			{
				this.result = objectStructComponent.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x060012B8 RID: 4792 RVA: 0x00041400 File Offset: 0x0003F600
			public override objectStructComponent.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new objectStructComponent.Builder(this.result);
				}
				return new objectStructComponent.Builder().MergeFrom(this.result);
			}

			// Token: 0x1700053A RID: 1338
			// (get) Token: 0x060012B9 RID: 4793 RVA: 0x0004142C File Offset: 0x0003F62C
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return objectStructComponent.Descriptor;
				}
			}

			// Token: 0x1700053B RID: 1339
			// (get) Token: 0x060012BA RID: 4794 RVA: 0x00041434 File Offset: 0x0003F634
			public override objectStructComponent DefaultInstanceForType
			{
				get
				{
					return objectStructComponent.DefaultInstance;
				}
			}

			// Token: 0x060012BB RID: 4795 RVA: 0x0004143C File Offset: 0x0003F63C
			public override objectStructComponent BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x060012BC RID: 4796 RVA: 0x00041470 File Offset: 0x0003F670
			public override objectStructComponent.Builder MergeFrom(IMessage other)
			{
				if (other is objectStructComponent)
				{
					return this.MergeFrom((objectStructComponent)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x060012BD RID: 4797 RVA: 0x00041494 File Offset: 0x0003F694
			public override objectStructComponent.Builder MergeFrom(objectStructComponent other)
			{
				if (other == objectStructComponent.DefaultInstance)
				{
					return this;
				}
				this.PrepareBuilder();
				if (other.HasID)
				{
					this.ID = other.ID;
				}
				if (other.HasMasterID)
				{
					this.MasterID = other.MasterID;
				}
				if (other.HasMasterViewID)
				{
					this.MasterViewID = other.MasterViewID;
				}
				this.MergeUnknownFields(other.UnknownFields);
				return this;
			}

			// Token: 0x060012BE RID: 4798 RVA: 0x00041508 File Offset: 0x0003F708
			public override objectStructComponent.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x060012BF RID: 4799 RVA: 0x00041518 File Offset: 0x0003F718
			public override objectStructComponent.Builder MergeFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
			{
				this.PrepareBuilder();
				UnknownFieldSet.Builder builder = null;
				uint num;
				string text;
				while (input.ReadTag(ref num, ref text))
				{
					if (num == 0u && text != null)
					{
						int num2 = Array.BinarySearch<string>(objectStructComponent._objectStructComponentFieldNames, text, StringComparer.Ordinal);
						if (num2 < 0)
						{
							if (builder == null)
							{
								builder = UnknownFieldSet.CreateBuilder(this.UnknownFields);
							}
							this.ParseUnknownField(input, builder, extensionRegistry, num, text);
							continue;
						}
						num = objectStructComponent._objectStructComponentFieldTags[num2];
					}
					uint num3 = num;
					if (num3 == 0u)
					{
						throw InvalidProtocolBufferException.InvalidTag();
					}
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
								this.result.hasMasterViewID = input.ReadInt32(ref this.result.masterViewID_);
							}
						}
						else
						{
							this.result.hasMasterID = input.ReadInt32(ref this.result.masterID_);
						}
					}
					else
					{
						this.result.hasID = input.ReadInt32(ref this.result.iD_);
					}
				}
				if (builder != null)
				{
					this.UnknownFields = builder.Build();
				}
				return this;
			}

			// Token: 0x1700053C RID: 1340
			// (get) Token: 0x060012C0 RID: 4800 RVA: 0x00041680 File Offset: 0x0003F880
			public bool HasID
			{
				get
				{
					return this.result.hasID;
				}
			}

			// Token: 0x1700053D RID: 1341
			// (get) Token: 0x060012C1 RID: 4801 RVA: 0x00041690 File Offset: 0x0003F890
			// (set) Token: 0x060012C2 RID: 4802 RVA: 0x000416A0 File Offset: 0x0003F8A0
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

			// Token: 0x060012C3 RID: 4803 RVA: 0x000416AC File Offset: 0x0003F8AC
			public objectStructComponent.Builder SetID(int value)
			{
				this.PrepareBuilder();
				this.result.hasID = true;
				this.result.iD_ = value;
				return this;
			}

			// Token: 0x060012C4 RID: 4804 RVA: 0x000416DC File Offset: 0x0003F8DC
			public objectStructComponent.Builder ClearID()
			{
				this.PrepareBuilder();
				this.result.hasID = false;
				this.result.iD_ = 0;
				return this;
			}

			// Token: 0x1700053E RID: 1342
			// (get) Token: 0x060012C5 RID: 4805 RVA: 0x0004170C File Offset: 0x0003F90C
			public bool HasMasterID
			{
				get
				{
					return this.result.hasMasterID;
				}
			}

			// Token: 0x1700053F RID: 1343
			// (get) Token: 0x060012C6 RID: 4806 RVA: 0x0004171C File Offset: 0x0003F91C
			// (set) Token: 0x060012C7 RID: 4807 RVA: 0x0004172C File Offset: 0x0003F92C
			public int MasterID
			{
				get
				{
					return this.result.MasterID;
				}
				set
				{
					this.SetMasterID(value);
				}
			}

			// Token: 0x060012C8 RID: 4808 RVA: 0x00041738 File Offset: 0x0003F938
			public objectStructComponent.Builder SetMasterID(int value)
			{
				this.PrepareBuilder();
				this.result.hasMasterID = true;
				this.result.masterID_ = value;
				return this;
			}

			// Token: 0x060012C9 RID: 4809 RVA: 0x00041768 File Offset: 0x0003F968
			public objectStructComponent.Builder ClearMasterID()
			{
				this.PrepareBuilder();
				this.result.hasMasterID = false;
				this.result.masterID_ = 0;
				return this;
			}

			// Token: 0x17000540 RID: 1344
			// (get) Token: 0x060012CA RID: 4810 RVA: 0x00041798 File Offset: 0x0003F998
			public bool HasMasterViewID
			{
				get
				{
					return this.result.hasMasterViewID;
				}
			}

			// Token: 0x17000541 RID: 1345
			// (get) Token: 0x060012CB RID: 4811 RVA: 0x000417A8 File Offset: 0x0003F9A8
			// (set) Token: 0x060012CC RID: 4812 RVA: 0x000417B8 File Offset: 0x0003F9B8
			public int MasterViewID
			{
				get
				{
					return this.result.MasterViewID;
				}
				set
				{
					this.SetMasterViewID(value);
				}
			}

			// Token: 0x060012CD RID: 4813 RVA: 0x000417C4 File Offset: 0x0003F9C4
			public objectStructComponent.Builder SetMasterViewID(int value)
			{
				this.PrepareBuilder();
				this.result.hasMasterViewID = true;
				this.result.masterViewID_ = value;
				return this;
			}

			// Token: 0x060012CE RID: 4814 RVA: 0x000417F4 File Offset: 0x0003F9F4
			public objectStructComponent.Builder ClearMasterViewID()
			{
				this.PrepareBuilder();
				this.result.hasMasterViewID = false;
				this.result.masterViewID_ = 0;
				return this;
			}

			// Token: 0x040009BA RID: 2490
			private bool resultIsReadOnly;

			// Token: 0x040009BB RID: 2491
			private objectStructComponent result;
		}
	}
}
