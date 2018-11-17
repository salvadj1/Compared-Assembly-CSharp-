using System;
using System.Diagnostics;
using System.IO;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;
using RustProto.Helpers;

namespace RustProto
{
	// Token: 0x02000242 RID: 578
	[DebuggerNonUserCode]
	public sealed class objectDeployable : GeneratedMessage<objectDeployable, objectDeployable.Builder>
	{
		// Token: 0x06001281 RID: 4737 RVA: 0x000428C8 File Offset: 0x00040AC8
		private objectDeployable()
		{
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x000428D8 File Offset: 0x00040AD8
		static objectDeployable()
		{
			object.ReferenceEquals(Worldsave.Descriptor, null);
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x00042930 File Offset: 0x00040B30
		public static RustProto.Helpers.Recycler<objectDeployable, objectDeployable.Builder> Recycler()
		{
			return RustProto.Helpers.Recycler<objectDeployable, objectDeployable.Builder>.Manufacture();
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06001284 RID: 4740 RVA: 0x00042938 File Offset: 0x00040B38
		public static objectDeployable DefaultInstance
		{
			get
			{
				return objectDeployable.defaultInstance;
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06001285 RID: 4741 RVA: 0x00042940 File Offset: 0x00040B40
		public override objectDeployable DefaultInstanceForType
		{
			get
			{
				return objectDeployable.DefaultInstance;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06001286 RID: 4742 RVA: 0x00042948 File Offset: 0x00040B48
		protected override objectDeployable ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06001287 RID: 4743 RVA: 0x0004294C File Offset: 0x00040B4C
		public static MessageDescriptor Descriptor
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectDeployable__Descriptor;
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06001288 RID: 4744 RVA: 0x00042954 File Offset: 0x00040B54
		protected override FieldAccessorTable<objectDeployable, objectDeployable.Builder> InternalFieldAccessors
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectDeployable__FieldAccessorTable;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06001289 RID: 4745 RVA: 0x0004295C File Offset: 0x00040B5C
		public bool HasCreatorID
		{
			get
			{
				return this.hasCreatorID;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x0600128A RID: 4746 RVA: 0x00042964 File Offset: 0x00040B64
		[CLSCompliant(false)]
		public ulong CreatorID
		{
			get
			{
				return this.creatorID_;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x0600128B RID: 4747 RVA: 0x0004296C File Offset: 0x00040B6C
		public bool HasOwnerID
		{
			get
			{
				return this.hasOwnerID;
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x0600128C RID: 4748 RVA: 0x00042974 File Offset: 0x00040B74
		[CLSCompliant(false)]
		public ulong OwnerID
		{
			get
			{
				return this.ownerID_;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x0600128D RID: 4749 RVA: 0x0004297C File Offset: 0x00040B7C
		public override bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x00042980 File Offset: 0x00040B80
		public override void WriteTo(ICodedOutputStream output)
		{
			int serializedSize = this.SerializedSize;
			string[] objectDeployableFieldNames = objectDeployable._objectDeployableFieldNames;
			if (this.hasCreatorID)
			{
				output.WriteUInt64(1, objectDeployableFieldNames[0], this.CreatorID);
			}
			if (this.hasOwnerID)
			{
				output.WriteUInt64(2, objectDeployableFieldNames[1], this.OwnerID);
			}
			this.UnknownFields.WriteTo(output);
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x0600128F RID: 4751 RVA: 0x000429DC File Offset: 0x00040BDC
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
				if (this.hasCreatorID)
				{
					num += CodedOutputStream.ComputeUInt64Size(1, this.CreatorID);
				}
				if (this.hasOwnerID)
				{
					num += CodedOutputStream.ComputeUInt64Size(2, this.OwnerID);
				}
				num += this.UnknownFields.SerializedSize;
				this.memoizedSerializedSize = num;
				return num;
			}
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x00042A48 File Offset: 0x00040C48
		public static objectDeployable ParseFrom(ByteString data)
		{
			return objectDeployable.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x00042A5C File Offset: 0x00040C5C
		public static objectDeployable ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return objectDeployable.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x00042A70 File Offset: 0x00040C70
		public static objectDeployable ParseFrom(byte[] data)
		{
			return objectDeployable.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x00042A84 File Offset: 0x00040C84
		public static objectDeployable ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return objectDeployable.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x00042A98 File Offset: 0x00040C98
		public static objectDeployable ParseFrom(Stream input)
		{
			return objectDeployable.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x00042AAC File Offset: 0x00040CAC
		public static objectDeployable ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectDeployable.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x00042AC0 File Offset: 0x00040CC0
		public static objectDeployable ParseDelimitedFrom(Stream input)
		{
			return objectDeployable.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x00042AD4 File Offset: 0x00040CD4
		public static objectDeployable ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectDeployable.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x00042AE8 File Offset: 0x00040CE8
		public static objectDeployable ParseFrom(ICodedInputStream input)
		{
			return objectDeployable.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x00042AFC File Offset: 0x00040CFC
		public static objectDeployable ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return objectDeployable.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x00042B10 File Offset: 0x00040D10
		private objectDeployable MakeReadOnly()
		{
			return this;
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x00042B14 File Offset: 0x00040D14
		public static objectDeployable.Builder CreateBuilder()
		{
			return new objectDeployable.Builder();
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x00042B1C File Offset: 0x00040D1C
		public override objectDeployable.Builder ToBuilder()
		{
			return objectDeployable.CreateBuilder(this);
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x00042B24 File Offset: 0x00040D24
		public override objectDeployable.Builder CreateBuilderForType()
		{
			return new objectDeployable.Builder();
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x00042B2C File Offset: 0x00040D2C
		public static objectDeployable.Builder CreateBuilder(objectDeployable prototype)
		{
			return new objectDeployable.Builder(prototype);
		}

		// Token: 0x04000A80 RID: 2688
		public const int CreatorIDFieldNumber = 1;

		// Token: 0x04000A81 RID: 2689
		public const int OwnerIDFieldNumber = 2;

		// Token: 0x04000A82 RID: 2690
		private static readonly objectDeployable defaultInstance = new objectDeployable().MakeReadOnly();

		// Token: 0x04000A83 RID: 2691
		private static readonly string[] _objectDeployableFieldNames = new string[]
		{
			"CreatorID",
			"OwnerID"
		};

		// Token: 0x04000A84 RID: 2692
		private static readonly uint[] _objectDeployableFieldTags = new uint[]
		{
			8u,
			16u
		};

		// Token: 0x04000A85 RID: 2693
		private bool hasCreatorID;

		// Token: 0x04000A86 RID: 2694
		private ulong creatorID_;

		// Token: 0x04000A87 RID: 2695
		private bool hasOwnerID;

		// Token: 0x04000A88 RID: 2696
		private ulong ownerID_;

		// Token: 0x04000A89 RID: 2697
		private int memoizedSerializedSize = -1;

		// Token: 0x02000243 RID: 579
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<objectDeployable, objectDeployable.Builder>
		{
			// Token: 0x0600129F RID: 4767 RVA: 0x00042B34 File Offset: 0x00040D34
			public Builder()
			{
				this.result = objectDeployable.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x060012A0 RID: 4768 RVA: 0x00042B50 File Offset: 0x00040D50
			internal Builder(objectDeployable cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x170004F9 RID: 1273
			// (get) Token: 0x060012A1 RID: 4769 RVA: 0x00042B68 File Offset: 0x00040D68
			protected override objectDeployable.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x060012A2 RID: 4770 RVA: 0x00042B6C File Offset: 0x00040D6C
			private objectDeployable PrepareBuilder()
			{
				if (this.resultIsReadOnly)
				{
					objectDeployable other = this.result;
					this.result = new objectDeployable();
					this.resultIsReadOnly = false;
					this.MergeFrom(other);
				}
				return this.result;
			}

			// Token: 0x170004FA RID: 1274
			// (get) Token: 0x060012A3 RID: 4771 RVA: 0x00042BAC File Offset: 0x00040DAC
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x170004FB RID: 1275
			// (get) Token: 0x060012A4 RID: 4772 RVA: 0x00042BBC File Offset: 0x00040DBC
			protected override objectDeployable MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x060012A5 RID: 4773 RVA: 0x00042BC4 File Offset: 0x00040DC4
			public override objectDeployable.Builder Clear()
			{
				this.result = objectDeployable.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x060012A6 RID: 4774 RVA: 0x00042BDC File Offset: 0x00040DDC
			public override objectDeployable.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new objectDeployable.Builder(this.result);
				}
				return new objectDeployable.Builder().MergeFrom(this.result);
			}

			// Token: 0x170004FC RID: 1276
			// (get) Token: 0x060012A7 RID: 4775 RVA: 0x00042C08 File Offset: 0x00040E08
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return objectDeployable.Descriptor;
				}
			}

			// Token: 0x170004FD RID: 1277
			// (get) Token: 0x060012A8 RID: 4776 RVA: 0x00042C10 File Offset: 0x00040E10
			public override objectDeployable DefaultInstanceForType
			{
				get
				{
					return objectDeployable.DefaultInstance;
				}
			}

			// Token: 0x060012A9 RID: 4777 RVA: 0x00042C18 File Offset: 0x00040E18
			public override objectDeployable BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x060012AA RID: 4778 RVA: 0x00042C4C File Offset: 0x00040E4C
			public override objectDeployable.Builder MergeFrom(IMessage other)
			{
				if (other is objectDeployable)
				{
					return this.MergeFrom((objectDeployable)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x060012AB RID: 4779 RVA: 0x00042C70 File Offset: 0x00040E70
			public override objectDeployable.Builder MergeFrom(objectDeployable other)
			{
				if (other == objectDeployable.DefaultInstance)
				{
					return this;
				}
				this.PrepareBuilder();
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

			// Token: 0x060012AC RID: 4780 RVA: 0x00042CD0 File Offset: 0x00040ED0
			public override objectDeployable.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x060012AD RID: 4781 RVA: 0x00042CE0 File Offset: 0x00040EE0
			public override objectDeployable.Builder MergeFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
			{
				this.PrepareBuilder();
				UnknownFieldSet.Builder builder = null;
				uint num;
				string text;
				while (input.ReadTag(ref num, ref text))
				{
					if (num == 0u && text != null)
					{
						int num2 = Array.BinarySearch<string>(objectDeployable._objectDeployableFieldNames, text, StringComparer.Ordinal);
						if (num2 < 0)
						{
							if (builder == null)
							{
								builder = UnknownFieldSet.CreateBuilder(this.UnknownFields);
							}
							this.ParseUnknownField(input, builder, extensionRegistry, num, text);
							continue;
						}
						num = objectDeployable._objectDeployableFieldTags[num2];
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
						this.result.hasCreatorID = input.ReadUInt64(ref this.result.creatorID_);
					}
				}
				if (builder != null)
				{
					this.UnknownFields = builder.Build();
				}
				return this;
			}

			// Token: 0x170004FE RID: 1278
			// (get) Token: 0x060012AE RID: 4782 RVA: 0x00042E1C File Offset: 0x0004101C
			public bool HasCreatorID
			{
				get
				{
					return this.result.hasCreatorID;
				}
			}

			// Token: 0x170004FF RID: 1279
			// (get) Token: 0x060012AF RID: 4783 RVA: 0x00042E2C File Offset: 0x0004102C
			// (set) Token: 0x060012B0 RID: 4784 RVA: 0x00042E3C File Offset: 0x0004103C
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

			// Token: 0x060012B1 RID: 4785 RVA: 0x00042E48 File Offset: 0x00041048
			[CLSCompliant(false)]
			public objectDeployable.Builder SetCreatorID(ulong value)
			{
				this.PrepareBuilder();
				this.result.hasCreatorID = true;
				this.result.creatorID_ = value;
				return this;
			}

			// Token: 0x060012B2 RID: 4786 RVA: 0x00042E78 File Offset: 0x00041078
			public objectDeployable.Builder ClearCreatorID()
			{
				this.PrepareBuilder();
				this.result.hasCreatorID = false;
				this.result.creatorID_ = 0UL;
				return this;
			}

			// Token: 0x17000500 RID: 1280
			// (get) Token: 0x060012B3 RID: 4787 RVA: 0x00042E9C File Offset: 0x0004109C
			public bool HasOwnerID
			{
				get
				{
					return this.result.hasOwnerID;
				}
			}

			// Token: 0x17000501 RID: 1281
			// (get) Token: 0x060012B4 RID: 4788 RVA: 0x00042EAC File Offset: 0x000410AC
			// (set) Token: 0x060012B5 RID: 4789 RVA: 0x00042EBC File Offset: 0x000410BC
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

			// Token: 0x060012B6 RID: 4790 RVA: 0x00042EC8 File Offset: 0x000410C8
			[CLSCompliant(false)]
			public objectDeployable.Builder SetOwnerID(ulong value)
			{
				this.PrepareBuilder();
				this.result.hasOwnerID = true;
				this.result.ownerID_ = value;
				return this;
			}

			// Token: 0x060012B7 RID: 4791 RVA: 0x00042EF8 File Offset: 0x000410F8
			public objectDeployable.Builder ClearOwnerID()
			{
				this.PrepareBuilder();
				this.result.hasOwnerID = false;
				this.result.ownerID_ = 0UL;
				return this;
			}

			// Token: 0x04000A8A RID: 2698
			private bool resultIsReadOnly;

			// Token: 0x04000A8B RID: 2699
			private objectDeployable result;
		}
	}
}
