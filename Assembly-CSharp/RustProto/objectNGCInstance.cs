using System;
using System.Diagnostics;
using System.IO;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;
using RustProto.Helpers;

namespace RustProto
{
	// Token: 0x02000240 RID: 576
	[DebuggerNonUserCode]
	public sealed class objectNGCInstance : GeneratedMessage<objectNGCInstance, objectNGCInstance.Builder>
	{
		// Token: 0x0600124A RID: 4682 RVA: 0x0004224C File Offset: 0x0004044C
		private objectNGCInstance()
		{
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x00042268 File Offset: 0x00040468
		static objectNGCInstance()
		{
			object.ReferenceEquals(Worldsave.Descriptor, null);
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x000422C0 File Offset: 0x000404C0
		public static RustProto.Helpers.Recycler<objectNGCInstance, objectNGCInstance.Builder> Recycler()
		{
			return RustProto.Helpers.Recycler<objectNGCInstance, objectNGCInstance.Builder>.Manufacture();
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x0600124D RID: 4685 RVA: 0x000422C8 File Offset: 0x000404C8
		public static objectNGCInstance DefaultInstance
		{
			get
			{
				return objectNGCInstance.defaultInstance;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x0600124E RID: 4686 RVA: 0x000422D0 File Offset: 0x000404D0
		public override objectNGCInstance DefaultInstanceForType
		{
			get
			{
				return objectNGCInstance.DefaultInstance;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x0600124F RID: 4687 RVA: 0x000422D8 File Offset: 0x000404D8
		protected override objectNGCInstance ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06001250 RID: 4688 RVA: 0x000422DC File Offset: 0x000404DC
		public static MessageDescriptor Descriptor
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectNGCInstance__Descriptor;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06001251 RID: 4689 RVA: 0x000422E4 File Offset: 0x000404E4
		protected override FieldAccessorTable<objectNGCInstance, objectNGCInstance.Builder> InternalFieldAccessors
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectNGCInstance__FieldAccessorTable;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06001252 RID: 4690 RVA: 0x000422EC File Offset: 0x000404EC
		public bool HasID
		{
			get
			{
				return this.hasID;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06001253 RID: 4691 RVA: 0x000422F4 File Offset: 0x000404F4
		public int ID
		{
			get
			{
				return this.iD_;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06001254 RID: 4692 RVA: 0x000422FC File Offset: 0x000404FC
		public bool HasData
		{
			get
			{
				return this.hasData;
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06001255 RID: 4693 RVA: 0x00042304 File Offset: 0x00040504
		public ByteString Data
		{
			get
			{
				return this.data_;
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06001256 RID: 4694 RVA: 0x0004230C File Offset: 0x0004050C
		public override bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x00042310 File Offset: 0x00040510
		public override void WriteTo(ICodedOutputStream output)
		{
			int serializedSize = this.SerializedSize;
			string[] objectNGCInstanceFieldNames = objectNGCInstance._objectNGCInstanceFieldNames;
			if (this.hasID)
			{
				output.WriteInt32(1, objectNGCInstanceFieldNames[0], this.ID);
			}
			if (this.hasData)
			{
				output.WriteBytes(2, objectNGCInstanceFieldNames[1], this.Data);
			}
			this.UnknownFields.WriteTo(output);
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06001258 RID: 4696 RVA: 0x0004236C File Offset: 0x0004056C
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
				if (this.hasData)
				{
					num += CodedOutputStream.ComputeBytesSize(2, this.Data);
				}
				num += this.UnknownFields.SerializedSize;
				this.memoizedSerializedSize = num;
				return num;
			}
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x000423D8 File Offset: 0x000405D8
		public static objectNGCInstance ParseFrom(ByteString data)
		{
			return objectNGCInstance.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x000423EC File Offset: 0x000405EC
		public static objectNGCInstance ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return objectNGCInstance.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x00042400 File Offset: 0x00040600
		public static objectNGCInstance ParseFrom(byte[] data)
		{
			return objectNGCInstance.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x00042414 File Offset: 0x00040614
		public static objectNGCInstance ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return objectNGCInstance.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x00042428 File Offset: 0x00040628
		public static objectNGCInstance ParseFrom(Stream input)
		{
			return objectNGCInstance.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x0004243C File Offset: 0x0004063C
		public static objectNGCInstance ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectNGCInstance.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x00042450 File Offset: 0x00040650
		public static objectNGCInstance ParseDelimitedFrom(Stream input)
		{
			return objectNGCInstance.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x00042464 File Offset: 0x00040664
		public static objectNGCInstance ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectNGCInstance.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x00042478 File Offset: 0x00040678
		public static objectNGCInstance ParseFrom(ICodedInputStream input)
		{
			return objectNGCInstance.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x0004248C File Offset: 0x0004068C
		public static objectNGCInstance ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return objectNGCInstance.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x000424A0 File Offset: 0x000406A0
		private objectNGCInstance MakeReadOnly()
		{
			return this;
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x000424A4 File Offset: 0x000406A4
		public static objectNGCInstance.Builder CreateBuilder()
		{
			return new objectNGCInstance.Builder();
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x000424AC File Offset: 0x000406AC
		public override objectNGCInstance.Builder ToBuilder()
		{
			return objectNGCInstance.CreateBuilder(this);
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x000424B4 File Offset: 0x000406B4
		public override objectNGCInstance.Builder CreateBuilderForType()
		{
			return new objectNGCInstance.Builder();
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x000424BC File Offset: 0x000406BC
		public static objectNGCInstance.Builder CreateBuilder(objectNGCInstance prototype)
		{
			return new objectNGCInstance.Builder(prototype);
		}

		// Token: 0x04000A74 RID: 2676
		public const int IDFieldNumber = 1;

		// Token: 0x04000A75 RID: 2677
		public const int DataFieldNumber = 2;

		// Token: 0x04000A76 RID: 2678
		private static readonly objectNGCInstance defaultInstance = new objectNGCInstance().MakeReadOnly();

		// Token: 0x04000A77 RID: 2679
		private static readonly string[] _objectNGCInstanceFieldNames = new string[]
		{
			"ID",
			"data"
		};

		// Token: 0x04000A78 RID: 2680
		private static readonly uint[] _objectNGCInstanceFieldTags = new uint[]
		{
			8u,
			18u
		};

		// Token: 0x04000A79 RID: 2681
		private bool hasID;

		// Token: 0x04000A7A RID: 2682
		private int iD_;

		// Token: 0x04000A7B RID: 2683
		private bool hasData;

		// Token: 0x04000A7C RID: 2684
		private ByteString data_ = ByteString.Empty;

		// Token: 0x04000A7D RID: 2685
		private int memoizedSerializedSize = -1;

		// Token: 0x02000241 RID: 577
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<objectNGCInstance, objectNGCInstance.Builder>
		{
			// Token: 0x06001268 RID: 4712 RVA: 0x000424C4 File Offset: 0x000406C4
			public Builder()
			{
				this.result = objectNGCInstance.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x06001269 RID: 4713 RVA: 0x000424E0 File Offset: 0x000406E0
			internal Builder(objectNGCInstance cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x170004E5 RID: 1253
			// (get) Token: 0x0600126A RID: 4714 RVA: 0x000424F8 File Offset: 0x000406F8
			protected override objectNGCInstance.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x0600126B RID: 4715 RVA: 0x000424FC File Offset: 0x000406FC
			private objectNGCInstance PrepareBuilder()
			{
				if (this.resultIsReadOnly)
				{
					objectNGCInstance other = this.result;
					this.result = new objectNGCInstance();
					this.resultIsReadOnly = false;
					this.MergeFrom(other);
				}
				return this.result;
			}

			// Token: 0x170004E6 RID: 1254
			// (get) Token: 0x0600126C RID: 4716 RVA: 0x0004253C File Offset: 0x0004073C
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x170004E7 RID: 1255
			// (get) Token: 0x0600126D RID: 4717 RVA: 0x0004254C File Offset: 0x0004074C
			protected override objectNGCInstance MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x0600126E RID: 4718 RVA: 0x00042554 File Offset: 0x00040754
			public override objectNGCInstance.Builder Clear()
			{
				this.result = objectNGCInstance.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x0600126F RID: 4719 RVA: 0x0004256C File Offset: 0x0004076C
			public override objectNGCInstance.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new objectNGCInstance.Builder(this.result);
				}
				return new objectNGCInstance.Builder().MergeFrom(this.result);
			}

			// Token: 0x170004E8 RID: 1256
			// (get) Token: 0x06001270 RID: 4720 RVA: 0x00042598 File Offset: 0x00040798
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return objectNGCInstance.Descriptor;
				}
			}

			// Token: 0x170004E9 RID: 1257
			// (get) Token: 0x06001271 RID: 4721 RVA: 0x000425A0 File Offset: 0x000407A0
			public override objectNGCInstance DefaultInstanceForType
			{
				get
				{
					return objectNGCInstance.DefaultInstance;
				}
			}

			// Token: 0x06001272 RID: 4722 RVA: 0x000425A8 File Offset: 0x000407A8
			public override objectNGCInstance BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x06001273 RID: 4723 RVA: 0x000425DC File Offset: 0x000407DC
			public override objectNGCInstance.Builder MergeFrom(IMessage other)
			{
				if (other is objectNGCInstance)
				{
					return this.MergeFrom((objectNGCInstance)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x06001274 RID: 4724 RVA: 0x00042600 File Offset: 0x00040800
			public override objectNGCInstance.Builder MergeFrom(objectNGCInstance other)
			{
				if (other == objectNGCInstance.DefaultInstance)
				{
					return this;
				}
				this.PrepareBuilder();
				if (other.HasID)
				{
					this.ID = other.ID;
				}
				if (other.HasData)
				{
					this.Data = other.Data;
				}
				this.MergeUnknownFields(other.UnknownFields);
				return this;
			}

			// Token: 0x06001275 RID: 4725 RVA: 0x00042660 File Offset: 0x00040860
			public override objectNGCInstance.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x06001276 RID: 4726 RVA: 0x00042670 File Offset: 0x00040870
			public override objectNGCInstance.Builder MergeFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
			{
				this.PrepareBuilder();
				UnknownFieldSet.Builder builder = null;
				uint num;
				string text;
				while (input.ReadTag(ref num, ref text))
				{
					if (num == 0u && text != null)
					{
						int num2 = Array.BinarySearch<string>(objectNGCInstance._objectNGCInstanceFieldNames, text, StringComparer.Ordinal);
						if (num2 < 0)
						{
							if (builder == null)
							{
								builder = UnknownFieldSet.CreateBuilder(this.UnknownFields);
							}
							this.ParseUnknownField(input, builder, extensionRegistry, num, text);
							continue;
						}
						num = objectNGCInstance._objectNGCInstanceFieldTags[num2];
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
							this.result.hasData = input.ReadBytes(ref this.result.data_);
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

			// Token: 0x170004EA RID: 1258
			// (get) Token: 0x06001277 RID: 4727 RVA: 0x000427AC File Offset: 0x000409AC
			public bool HasID
			{
				get
				{
					return this.result.hasID;
				}
			}

			// Token: 0x170004EB RID: 1259
			// (get) Token: 0x06001278 RID: 4728 RVA: 0x000427BC File Offset: 0x000409BC
			// (set) Token: 0x06001279 RID: 4729 RVA: 0x000427CC File Offset: 0x000409CC
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

			// Token: 0x0600127A RID: 4730 RVA: 0x000427D8 File Offset: 0x000409D8
			public objectNGCInstance.Builder SetID(int value)
			{
				this.PrepareBuilder();
				this.result.hasID = true;
				this.result.iD_ = value;
				return this;
			}

			// Token: 0x0600127B RID: 4731 RVA: 0x00042808 File Offset: 0x00040A08
			public objectNGCInstance.Builder ClearID()
			{
				this.PrepareBuilder();
				this.result.hasID = false;
				this.result.iD_ = 0;
				return this;
			}

			// Token: 0x170004EC RID: 1260
			// (get) Token: 0x0600127C RID: 4732 RVA: 0x00042838 File Offset: 0x00040A38
			public bool HasData
			{
				get
				{
					return this.result.hasData;
				}
			}

			// Token: 0x170004ED RID: 1261
			// (get) Token: 0x0600127D RID: 4733 RVA: 0x00042848 File Offset: 0x00040A48
			// (set) Token: 0x0600127E RID: 4734 RVA: 0x00042858 File Offset: 0x00040A58
			public ByteString Data
			{
				get
				{
					return this.result.Data;
				}
				set
				{
					this.SetData(value);
				}
			}

			// Token: 0x0600127F RID: 4735 RVA: 0x00042864 File Offset: 0x00040A64
			public objectNGCInstance.Builder SetData(ByteString value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasData = true;
				this.result.data_ = value;
				return this;
			}

			// Token: 0x06001280 RID: 4736 RVA: 0x00042894 File Offset: 0x00040A94
			public objectNGCInstance.Builder ClearData()
			{
				this.PrepareBuilder();
				this.result.hasData = false;
				this.result.data_ = ByteString.Empty;
				return this;
			}

			// Token: 0x04000A7E RID: 2686
			private bool resultIsReadOnly;

			// Token: 0x04000A7F RID: 2687
			private objectNGCInstance result;
		}
	}
}
