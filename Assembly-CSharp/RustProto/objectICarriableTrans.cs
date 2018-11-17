using System;
using System.Diagnostics;
using System.IO;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;
using RustProto.Helpers;

namespace RustProto
{
	// Token: 0x0200021F RID: 543
	[DebuggerNonUserCode]
	public sealed class objectICarriableTrans : GeneratedMessage<objectICarriableTrans, objectICarriableTrans.Builder>
	{
		// Token: 0x06001314 RID: 4884 RVA: 0x000420BC File Offset: 0x000402BC
		private objectICarriableTrans()
		{
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x000420CC File Offset: 0x000402CC
		static objectICarriableTrans()
		{
			object.ReferenceEquals(Worldsave.Descriptor, null);
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x0004210C File Offset: 0x0004030C
		public static Recycler<objectICarriableTrans, objectICarriableTrans.Builder> Recycler()
		{
			return Recycler<objectICarriableTrans, objectICarriableTrans.Builder>.Manufacture();
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001317 RID: 4887 RVA: 0x00042114 File Offset: 0x00040314
		public static objectICarriableTrans DefaultInstance
		{
			get
			{
				return objectICarriableTrans.defaultInstance;
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001318 RID: 4888 RVA: 0x0004211C File Offset: 0x0004031C
		public override objectICarriableTrans DefaultInstanceForType
		{
			get
			{
				return objectICarriableTrans.DefaultInstance;
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06001319 RID: 4889 RVA: 0x00042124 File Offset: 0x00040324
		protected override objectICarriableTrans ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x0600131A RID: 4890 RVA: 0x00042128 File Offset: 0x00040328
		public static MessageDescriptor Descriptor
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectICarriableTrans__Descriptor;
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x0600131B RID: 4891 RVA: 0x00042130 File Offset: 0x00040330
		protected override FieldAccessorTable<objectICarriableTrans, objectICarriableTrans.Builder> InternalFieldAccessors
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectICarriableTrans__FieldAccessorTable;
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x0600131C RID: 4892 RVA: 0x00042138 File Offset: 0x00040338
		public bool HasTransCarrierID
		{
			get
			{
				return this.hasTransCarrierID;
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x0600131D RID: 4893 RVA: 0x00042140 File Offset: 0x00040340
		public int TransCarrierID
		{
			get
			{
				return this.transCarrierID_;
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x0600131E RID: 4894 RVA: 0x00042148 File Offset: 0x00040348
		public override bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x0004214C File Offset: 0x0004034C
		public override void WriteTo(ICodedOutputStream output)
		{
			int serializedSize = this.SerializedSize;
			string[] objectICarriableTransFieldNames = objectICarriableTrans._objectICarriableTransFieldNames;
			if (this.hasTransCarrierID)
			{
				output.WriteInt32(1, objectICarriableTransFieldNames[0], this.TransCarrierID);
			}
			this.UnknownFields.WriteTo(output);
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06001320 RID: 4896 RVA: 0x00042190 File Offset: 0x00040390
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
				if (this.hasTransCarrierID)
				{
					num += CodedOutputStream.ComputeInt32Size(1, this.TransCarrierID);
				}
				num += this.UnknownFields.SerializedSize;
				this.memoizedSerializedSize = num;
				return num;
			}
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x000421E0 File Offset: 0x000403E0
		public static objectICarriableTrans ParseFrom(ByteString data)
		{
			return objectICarriableTrans.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x000421F4 File Offset: 0x000403F4
		public static objectICarriableTrans ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return objectICarriableTrans.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x00042208 File Offset: 0x00040408
		public static objectICarriableTrans ParseFrom(byte[] data)
		{
			return objectICarriableTrans.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x0004221C File Offset: 0x0004041C
		public static objectICarriableTrans ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return objectICarriableTrans.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x00042230 File Offset: 0x00040430
		public static objectICarriableTrans ParseFrom(Stream input)
		{
			return objectICarriableTrans.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x00042244 File Offset: 0x00040444
		public static objectICarriableTrans ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectICarriableTrans.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x00042258 File Offset: 0x00040458
		public static objectICarriableTrans ParseDelimitedFrom(Stream input)
		{
			return objectICarriableTrans.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x0004226C File Offset: 0x0004046C
		public static objectICarriableTrans ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectICarriableTrans.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x00042280 File Offset: 0x00040480
		public static objectICarriableTrans ParseFrom(ICodedInputStream input)
		{
			return objectICarriableTrans.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x00042294 File Offset: 0x00040494
		public static objectICarriableTrans ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return objectICarriableTrans.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x000422A8 File Offset: 0x000404A8
		private objectICarriableTrans MakeReadOnly()
		{
			return this;
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x000422AC File Offset: 0x000404AC
		public static objectICarriableTrans.Builder CreateBuilder()
		{
			return new objectICarriableTrans.Builder();
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x000422B4 File Offset: 0x000404B4
		public override objectICarriableTrans.Builder ToBuilder()
		{
			return objectICarriableTrans.CreateBuilder(this);
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x000422BC File Offset: 0x000404BC
		public override objectICarriableTrans.Builder CreateBuilderForType()
		{
			return new objectICarriableTrans.Builder();
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x000422C4 File Offset: 0x000404C4
		public static objectICarriableTrans.Builder CreateBuilder(objectICarriableTrans prototype)
		{
			return new objectICarriableTrans.Builder(prototype);
		}

		// Token: 0x040009CE RID: 2510
		public const int TransCarrierIDFieldNumber = 1;

		// Token: 0x040009CF RID: 2511
		private static readonly objectICarriableTrans defaultInstance = new objectICarriableTrans().MakeReadOnly();

		// Token: 0x040009D0 RID: 2512
		private static readonly string[] _objectICarriableTransFieldNames = new string[]
		{
			"transCarrierID"
		};

		// Token: 0x040009D1 RID: 2513
		private static readonly uint[] _objectICarriableTransFieldTags = new uint[]
		{
			8u
		};

		// Token: 0x040009D2 RID: 2514
		private bool hasTransCarrierID;

		// Token: 0x040009D3 RID: 2515
		private int transCarrierID_;

		// Token: 0x040009D4 RID: 2516
		private int memoizedSerializedSize = -1;

		// Token: 0x02000220 RID: 544
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<objectICarriableTrans, objectICarriableTrans.Builder>
		{
			// Token: 0x06001330 RID: 4912 RVA: 0x000422CC File Offset: 0x000404CC
			public Builder()
			{
				this.result = objectICarriableTrans.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x06001331 RID: 4913 RVA: 0x000422E8 File Offset: 0x000404E8
			internal Builder(objectICarriableTrans cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x17000567 RID: 1383
			// (get) Token: 0x06001332 RID: 4914 RVA: 0x00042300 File Offset: 0x00040500
			protected override objectICarriableTrans.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x06001333 RID: 4915 RVA: 0x00042304 File Offset: 0x00040504
			private objectICarriableTrans PrepareBuilder()
			{
				if (this.resultIsReadOnly)
				{
					objectICarriableTrans other = this.result;
					this.result = new objectICarriableTrans();
					this.resultIsReadOnly = false;
					this.MergeFrom(other);
				}
				return this.result;
			}

			// Token: 0x17000568 RID: 1384
			// (get) Token: 0x06001334 RID: 4916 RVA: 0x00042344 File Offset: 0x00040544
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x17000569 RID: 1385
			// (get) Token: 0x06001335 RID: 4917 RVA: 0x00042354 File Offset: 0x00040554
			protected override objectICarriableTrans MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x06001336 RID: 4918 RVA: 0x0004235C File Offset: 0x0004055C
			public override objectICarriableTrans.Builder Clear()
			{
				this.result = objectICarriableTrans.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x06001337 RID: 4919 RVA: 0x00042374 File Offset: 0x00040574
			public override objectICarriableTrans.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new objectICarriableTrans.Builder(this.result);
				}
				return new objectICarriableTrans.Builder().MergeFrom(this.result);
			}

			// Token: 0x1700056A RID: 1386
			// (get) Token: 0x06001338 RID: 4920 RVA: 0x000423A0 File Offset: 0x000405A0
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return objectICarriableTrans.Descriptor;
				}
			}

			// Token: 0x1700056B RID: 1387
			// (get) Token: 0x06001339 RID: 4921 RVA: 0x000423A8 File Offset: 0x000405A8
			public override objectICarriableTrans DefaultInstanceForType
			{
				get
				{
					return objectICarriableTrans.DefaultInstance;
				}
			}

			// Token: 0x0600133A RID: 4922 RVA: 0x000423B0 File Offset: 0x000405B0
			public override objectICarriableTrans BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x0600133B RID: 4923 RVA: 0x000423E4 File Offset: 0x000405E4
			public override objectICarriableTrans.Builder MergeFrom(IMessage other)
			{
				if (other is objectICarriableTrans)
				{
					return this.MergeFrom((objectICarriableTrans)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x0600133C RID: 4924 RVA: 0x00042408 File Offset: 0x00040608
			public override objectICarriableTrans.Builder MergeFrom(objectICarriableTrans other)
			{
				if (other == objectICarriableTrans.DefaultInstance)
				{
					return this;
				}
				this.PrepareBuilder();
				if (other.HasTransCarrierID)
				{
					this.TransCarrierID = other.TransCarrierID;
				}
				this.MergeUnknownFields(other.UnknownFields);
				return this;
			}

			// Token: 0x0600133D RID: 4925 RVA: 0x00042450 File Offset: 0x00040650
			public override objectICarriableTrans.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x0600133E RID: 4926 RVA: 0x00042460 File Offset: 0x00040660
			public override objectICarriableTrans.Builder MergeFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
			{
				this.PrepareBuilder();
				UnknownFieldSet.Builder builder = null;
				uint num;
				string text;
				while (input.ReadTag(ref num, ref text))
				{
					if (num == 0u && text != null)
					{
						int num2 = Array.BinarySearch<string>(objectICarriableTrans._objectICarriableTransFieldNames, text, StringComparer.Ordinal);
						if (num2 < 0)
						{
							if (builder == null)
							{
								builder = UnknownFieldSet.CreateBuilder(this.UnknownFields);
							}
							this.ParseUnknownField(input, builder, extensionRegistry, num, text);
							continue;
						}
						num = objectICarriableTrans._objectICarriableTransFieldTags[num2];
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
						this.result.hasTransCarrierID = input.ReadInt32(ref this.result.transCarrierID_);
					}
				}
				if (builder != null)
				{
					this.UnknownFields = builder.Build();
				}
				return this;
			}

			// Token: 0x1700056C RID: 1388
			// (get) Token: 0x0600133F RID: 4927 RVA: 0x00042574 File Offset: 0x00040774
			public bool HasTransCarrierID
			{
				get
				{
					return this.result.hasTransCarrierID;
				}
			}

			// Token: 0x1700056D RID: 1389
			// (get) Token: 0x06001340 RID: 4928 RVA: 0x00042584 File Offset: 0x00040784
			// (set) Token: 0x06001341 RID: 4929 RVA: 0x00042594 File Offset: 0x00040794
			public int TransCarrierID
			{
				get
				{
					return this.result.TransCarrierID;
				}
				set
				{
					this.SetTransCarrierID(value);
				}
			}

			// Token: 0x06001342 RID: 4930 RVA: 0x000425A0 File Offset: 0x000407A0
			public objectICarriableTrans.Builder SetTransCarrierID(int value)
			{
				this.PrepareBuilder();
				this.result.hasTransCarrierID = true;
				this.result.transCarrierID_ = value;
				return this;
			}

			// Token: 0x06001343 RID: 4931 RVA: 0x000425D0 File Offset: 0x000407D0
			public objectICarriableTrans.Builder ClearTransCarrierID()
			{
				this.PrepareBuilder();
				this.result.hasTransCarrierID = false;
				this.result.transCarrierID_ = 0;
				return this;
			}

			// Token: 0x040009D5 RID: 2517
			private bool resultIsReadOnly;

			// Token: 0x040009D6 RID: 2518
			private objectICarriableTrans result;
		}
	}
}
