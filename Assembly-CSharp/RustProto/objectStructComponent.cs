using System;
using System.Diagnostics;
using System.IO;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;
using RustProto.Helpers;

namespace RustProto
{
	// Token: 0x0200024E RID: 590
	[DebuggerNonUserCode]
	public sealed class objectStructComponent : GeneratedMessage<objectStructComponent, objectStructComponent.Builder>
	{
		// Token: 0x060013E5 RID: 5093 RVA: 0x00045444 File Offset: 0x00043644
		private objectStructComponent()
		{
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x00045454 File Offset: 0x00043654
		static objectStructComponent()
		{
			object.ReferenceEquals(Worldsave.Descriptor, null);
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x000454B8 File Offset: 0x000436B8
		public static RustProto.Helpers.Recycler<objectStructComponent, objectStructComponent.Builder> Recycler()
		{
			return RustProto.Helpers.Recycler<objectStructComponent, objectStructComponent.Builder>.Manufacture();
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x060013E8 RID: 5096 RVA: 0x000454C0 File Offset: 0x000436C0
		public static objectStructComponent DefaultInstance
		{
			get
			{
				return objectStructComponent.defaultInstance;
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x060013E9 RID: 5097 RVA: 0x000454C8 File Offset: 0x000436C8
		public override objectStructComponent DefaultInstanceForType
		{
			get
			{
				return objectStructComponent.DefaultInstance;
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x060013EA RID: 5098 RVA: 0x000454D0 File Offset: 0x000436D0
		protected override objectStructComponent ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x060013EB RID: 5099 RVA: 0x000454D4 File Offset: 0x000436D4
		public static MessageDescriptor Descriptor
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectStructComponent__Descriptor;
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x060013EC RID: 5100 RVA: 0x000454DC File Offset: 0x000436DC
		protected override FieldAccessorTable<objectStructComponent, objectStructComponent.Builder> InternalFieldAccessors
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectStructComponent__FieldAccessorTable;
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x060013ED RID: 5101 RVA: 0x000454E4 File Offset: 0x000436E4
		public bool HasID
		{
			get
			{
				return this.hasID;
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x060013EE RID: 5102 RVA: 0x000454EC File Offset: 0x000436EC
		public int ID
		{
			get
			{
				return this.iD_;
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x060013EF RID: 5103 RVA: 0x000454F4 File Offset: 0x000436F4
		public bool HasMasterID
		{
			get
			{
				return this.hasMasterID;
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x060013F0 RID: 5104 RVA: 0x000454FC File Offset: 0x000436FC
		public int MasterID
		{
			get
			{
				return this.masterID_;
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x060013F1 RID: 5105 RVA: 0x00045504 File Offset: 0x00043704
		public bool HasMasterViewID
		{
			get
			{
				return this.hasMasterViewID;
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x060013F2 RID: 5106 RVA: 0x0004550C File Offset: 0x0004370C
		public int MasterViewID
		{
			get
			{
				return this.masterViewID_;
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x060013F3 RID: 5107 RVA: 0x00045514 File Offset: 0x00043714
		public override bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x00045518 File Offset: 0x00043718
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

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x060013F5 RID: 5109 RVA: 0x00045590 File Offset: 0x00043790
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

		// Token: 0x060013F6 RID: 5110 RVA: 0x00045614 File Offset: 0x00043814
		public static objectStructComponent ParseFrom(ByteString data)
		{
			return objectStructComponent.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x00045628 File Offset: 0x00043828
		public static objectStructComponent ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return objectStructComponent.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x0004563C File Offset: 0x0004383C
		public static objectStructComponent ParseFrom(byte[] data)
		{
			return objectStructComponent.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x00045650 File Offset: 0x00043850
		public static objectStructComponent ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return objectStructComponent.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x00045664 File Offset: 0x00043864
		public static objectStructComponent ParseFrom(Stream input)
		{
			return objectStructComponent.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x00045678 File Offset: 0x00043878
		public static objectStructComponent ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectStructComponent.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x0004568C File Offset: 0x0004388C
		public static objectStructComponent ParseDelimitedFrom(Stream input)
		{
			return objectStructComponent.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x000456A0 File Offset: 0x000438A0
		public static objectStructComponent ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectStructComponent.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x000456B4 File Offset: 0x000438B4
		public static objectStructComponent ParseFrom(ICodedInputStream input)
		{
			return objectStructComponent.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x000456C8 File Offset: 0x000438C8
		public static objectStructComponent ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return objectStructComponent.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x000456DC File Offset: 0x000438DC
		private objectStructComponent MakeReadOnly()
		{
			return this;
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x000456E0 File Offset: 0x000438E0
		public static objectStructComponent.Builder CreateBuilder()
		{
			return new objectStructComponent.Builder();
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x000456E8 File Offset: 0x000438E8
		public override objectStructComponent.Builder ToBuilder()
		{
			return objectStructComponent.CreateBuilder(this);
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x000456F0 File Offset: 0x000438F0
		public override objectStructComponent.Builder CreateBuilderForType()
		{
			return new objectStructComponent.Builder();
		}

		// Token: 0x06001404 RID: 5124 RVA: 0x000456F8 File Offset: 0x000438F8
		public static objectStructComponent.Builder CreateBuilder(objectStructComponent prototype)
		{
			return new objectStructComponent.Builder(prototype);
		}

		// Token: 0x04000AD0 RID: 2768
		public const int IDFieldNumber = 1;

		// Token: 0x04000AD1 RID: 2769
		public const int MasterIDFieldNumber = 2;

		// Token: 0x04000AD2 RID: 2770
		public const int MasterViewIDFieldNumber = 3;

		// Token: 0x04000AD3 RID: 2771
		private static readonly objectStructComponent defaultInstance = new objectStructComponent().MakeReadOnly();

		// Token: 0x04000AD4 RID: 2772
		private static readonly string[] _objectStructComponentFieldNames = new string[]
		{
			"ID",
			"MasterID",
			"MasterViewID"
		};

		// Token: 0x04000AD5 RID: 2773
		private static readonly uint[] _objectStructComponentFieldTags = new uint[]
		{
			8u,
			16u,
			24u
		};

		// Token: 0x04000AD6 RID: 2774
		private bool hasID;

		// Token: 0x04000AD7 RID: 2775
		private int iD_;

		// Token: 0x04000AD8 RID: 2776
		private bool hasMasterID;

		// Token: 0x04000AD9 RID: 2777
		private int masterID_;

		// Token: 0x04000ADA RID: 2778
		private bool hasMasterViewID;

		// Token: 0x04000ADB RID: 2779
		private int masterViewID_;

		// Token: 0x04000ADC RID: 2780
		private int memoizedSerializedSize = -1;

		// Token: 0x0200024F RID: 591
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<objectStructComponent, objectStructComponent.Builder>
		{
			// Token: 0x06001405 RID: 5125 RVA: 0x00045700 File Offset: 0x00043900
			public Builder()
			{
				this.result = objectStructComponent.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x06001406 RID: 5126 RVA: 0x0004571C File Offset: 0x0004391C
			internal Builder(objectStructComponent cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x1700057F RID: 1407
			// (get) Token: 0x06001407 RID: 5127 RVA: 0x00045734 File Offset: 0x00043934
			protected override objectStructComponent.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x06001408 RID: 5128 RVA: 0x00045738 File Offset: 0x00043938
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

			// Token: 0x17000580 RID: 1408
			// (get) Token: 0x06001409 RID: 5129 RVA: 0x00045778 File Offset: 0x00043978
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x17000581 RID: 1409
			// (get) Token: 0x0600140A RID: 5130 RVA: 0x00045788 File Offset: 0x00043988
			protected override objectStructComponent MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x0600140B RID: 5131 RVA: 0x00045790 File Offset: 0x00043990
			public override objectStructComponent.Builder Clear()
			{
				this.result = objectStructComponent.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x0600140C RID: 5132 RVA: 0x000457A8 File Offset: 0x000439A8
			public override objectStructComponent.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new objectStructComponent.Builder(this.result);
				}
				return new objectStructComponent.Builder().MergeFrom(this.result);
			}

			// Token: 0x17000582 RID: 1410
			// (get) Token: 0x0600140D RID: 5133 RVA: 0x000457D4 File Offset: 0x000439D4
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return objectStructComponent.Descriptor;
				}
			}

			// Token: 0x17000583 RID: 1411
			// (get) Token: 0x0600140E RID: 5134 RVA: 0x000457DC File Offset: 0x000439DC
			public override objectStructComponent DefaultInstanceForType
			{
				get
				{
					return objectStructComponent.DefaultInstance;
				}
			}

			// Token: 0x0600140F RID: 5135 RVA: 0x000457E4 File Offset: 0x000439E4
			public override objectStructComponent BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x06001410 RID: 5136 RVA: 0x00045818 File Offset: 0x00043A18
			public override objectStructComponent.Builder MergeFrom(IMessage other)
			{
				if (other is objectStructComponent)
				{
					return this.MergeFrom((objectStructComponent)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x06001411 RID: 5137 RVA: 0x0004583C File Offset: 0x00043A3C
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

			// Token: 0x06001412 RID: 5138 RVA: 0x000458B0 File Offset: 0x00043AB0
			public override objectStructComponent.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x06001413 RID: 5139 RVA: 0x000458C0 File Offset: 0x00043AC0
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

			// Token: 0x17000584 RID: 1412
			// (get) Token: 0x06001414 RID: 5140 RVA: 0x00045A28 File Offset: 0x00043C28
			public bool HasID
			{
				get
				{
					return this.result.hasID;
				}
			}

			// Token: 0x17000585 RID: 1413
			// (get) Token: 0x06001415 RID: 5141 RVA: 0x00045A38 File Offset: 0x00043C38
			// (set) Token: 0x06001416 RID: 5142 RVA: 0x00045A48 File Offset: 0x00043C48
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

			// Token: 0x06001417 RID: 5143 RVA: 0x00045A54 File Offset: 0x00043C54
			public objectStructComponent.Builder SetID(int value)
			{
				this.PrepareBuilder();
				this.result.hasID = true;
				this.result.iD_ = value;
				return this;
			}

			// Token: 0x06001418 RID: 5144 RVA: 0x00045A84 File Offset: 0x00043C84
			public objectStructComponent.Builder ClearID()
			{
				this.PrepareBuilder();
				this.result.hasID = false;
				this.result.iD_ = 0;
				return this;
			}

			// Token: 0x17000586 RID: 1414
			// (get) Token: 0x06001419 RID: 5145 RVA: 0x00045AB4 File Offset: 0x00043CB4
			public bool HasMasterID
			{
				get
				{
					return this.result.hasMasterID;
				}
			}

			// Token: 0x17000587 RID: 1415
			// (get) Token: 0x0600141A RID: 5146 RVA: 0x00045AC4 File Offset: 0x00043CC4
			// (set) Token: 0x0600141B RID: 5147 RVA: 0x00045AD4 File Offset: 0x00043CD4
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

			// Token: 0x0600141C RID: 5148 RVA: 0x00045AE0 File Offset: 0x00043CE0
			public objectStructComponent.Builder SetMasterID(int value)
			{
				this.PrepareBuilder();
				this.result.hasMasterID = true;
				this.result.masterID_ = value;
				return this;
			}

			// Token: 0x0600141D RID: 5149 RVA: 0x00045B10 File Offset: 0x00043D10
			public objectStructComponent.Builder ClearMasterID()
			{
				this.PrepareBuilder();
				this.result.hasMasterID = false;
				this.result.masterID_ = 0;
				return this;
			}

			// Token: 0x17000588 RID: 1416
			// (get) Token: 0x0600141E RID: 5150 RVA: 0x00045B40 File Offset: 0x00043D40
			public bool HasMasterViewID
			{
				get
				{
					return this.result.hasMasterViewID;
				}
			}

			// Token: 0x17000589 RID: 1417
			// (get) Token: 0x0600141F RID: 5151 RVA: 0x00045B50 File Offset: 0x00043D50
			// (set) Token: 0x06001420 RID: 5152 RVA: 0x00045B60 File Offset: 0x00043D60
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

			// Token: 0x06001421 RID: 5153 RVA: 0x00045B6C File Offset: 0x00043D6C
			public objectStructComponent.Builder SetMasterViewID(int value)
			{
				this.PrepareBuilder();
				this.result.hasMasterViewID = true;
				this.result.masterViewID_ = value;
				return this;
			}

			// Token: 0x06001422 RID: 5154 RVA: 0x00045B9C File Offset: 0x00043D9C
			public objectStructComponent.Builder ClearMasterViewID()
			{
				this.PrepareBuilder();
				this.result.hasMasterViewID = false;
				this.result.masterViewID_ = 0;
				return this;
			}

			// Token: 0x04000ADD RID: 2781
			private bool resultIsReadOnly;

			// Token: 0x04000ADE RID: 2782
			private objectStructComponent result;
		}
	}
}
