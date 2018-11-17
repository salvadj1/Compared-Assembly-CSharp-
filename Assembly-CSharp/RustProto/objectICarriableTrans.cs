using System;
using System.Diagnostics;
using System.IO;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;
using RustProto.Helpers;

namespace RustProto
{
	// Token: 0x02000252 RID: 594
	[DebuggerNonUserCode]
	public sealed class objectICarriableTrans : GeneratedMessage<objectICarriableTrans, objectICarriableTrans.Builder>
	{
		// Token: 0x06001468 RID: 5224 RVA: 0x00046464 File Offset: 0x00044664
		private objectICarriableTrans()
		{
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x00046474 File Offset: 0x00044674
		static objectICarriableTrans()
		{
			object.ReferenceEquals(Worldsave.Descriptor, null);
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x000464B4 File Offset: 0x000446B4
		public static RustProto.Helpers.Recycler<objectICarriableTrans, objectICarriableTrans.Builder> Recycler()
		{
			return RustProto.Helpers.Recycler<objectICarriableTrans, objectICarriableTrans.Builder>.Manufacture();
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x0600146B RID: 5227 RVA: 0x000464BC File Offset: 0x000446BC
		public static objectICarriableTrans DefaultInstance
		{
			get
			{
				return objectICarriableTrans.defaultInstance;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x0600146C RID: 5228 RVA: 0x000464C4 File Offset: 0x000446C4
		public override objectICarriableTrans DefaultInstanceForType
		{
			get
			{
				return objectICarriableTrans.DefaultInstance;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x0600146D RID: 5229 RVA: 0x000464CC File Offset: 0x000446CC
		protected override objectICarriableTrans ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x0600146E RID: 5230 RVA: 0x000464D0 File Offset: 0x000446D0
		public static MessageDescriptor Descriptor
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectICarriableTrans__Descriptor;
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x0600146F RID: 5231 RVA: 0x000464D8 File Offset: 0x000446D8
		protected override FieldAccessorTable<objectICarriableTrans, objectICarriableTrans.Builder> InternalFieldAccessors
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectICarriableTrans__FieldAccessorTable;
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001470 RID: 5232 RVA: 0x000464E0 File Offset: 0x000446E0
		public bool HasTransCarrierID
		{
			get
			{
				return this.hasTransCarrierID;
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001471 RID: 5233 RVA: 0x000464E8 File Offset: 0x000446E8
		public int TransCarrierID
		{
			get
			{
				return this.transCarrierID_;
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001472 RID: 5234 RVA: 0x000464F0 File Offset: 0x000446F0
		public override bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x000464F4 File Offset: 0x000446F4
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

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001474 RID: 5236 RVA: 0x00046538 File Offset: 0x00044738
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

		// Token: 0x06001475 RID: 5237 RVA: 0x00046588 File Offset: 0x00044788
		public static objectICarriableTrans ParseFrom(ByteString data)
		{
			return objectICarriableTrans.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x0004659C File Offset: 0x0004479C
		public static objectICarriableTrans ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return objectICarriableTrans.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x000465B0 File Offset: 0x000447B0
		public static objectICarriableTrans ParseFrom(byte[] data)
		{
			return objectICarriableTrans.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06001478 RID: 5240 RVA: 0x000465C4 File Offset: 0x000447C4
		public static objectICarriableTrans ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return objectICarriableTrans.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x000465D8 File Offset: 0x000447D8
		public static objectICarriableTrans ParseFrom(Stream input)
		{
			return objectICarriableTrans.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x000465EC File Offset: 0x000447EC
		public static objectICarriableTrans ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectICarriableTrans.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x00046600 File Offset: 0x00044800
		public static objectICarriableTrans ParseDelimitedFrom(Stream input)
		{
			return objectICarriableTrans.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x00046614 File Offset: 0x00044814
		public static objectICarriableTrans ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectICarriableTrans.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x00046628 File Offset: 0x00044828
		public static objectICarriableTrans ParseFrom(ICodedInputStream input)
		{
			return objectICarriableTrans.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x0004663C File Offset: 0x0004483C
		public static objectICarriableTrans ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return objectICarriableTrans.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x00046650 File Offset: 0x00044850
		private objectICarriableTrans MakeReadOnly()
		{
			return this;
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x00046654 File Offset: 0x00044854
		public static objectICarriableTrans.Builder CreateBuilder()
		{
			return new objectICarriableTrans.Builder();
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x0004665C File Offset: 0x0004485C
		public override objectICarriableTrans.Builder ToBuilder()
		{
			return objectICarriableTrans.CreateBuilder(this);
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x00046664 File Offset: 0x00044864
		public override objectICarriableTrans.Builder CreateBuilderForType()
		{
			return new objectICarriableTrans.Builder();
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x0004666C File Offset: 0x0004486C
		public static objectICarriableTrans.Builder CreateBuilder(objectICarriableTrans prototype)
		{
			return new objectICarriableTrans.Builder(prototype);
		}

		// Token: 0x04000AF1 RID: 2801
		public const int TransCarrierIDFieldNumber = 1;

		// Token: 0x04000AF2 RID: 2802
		private static readonly objectICarriableTrans defaultInstance = new objectICarriableTrans().MakeReadOnly();

		// Token: 0x04000AF3 RID: 2803
		private static readonly string[] _objectICarriableTransFieldNames = new string[]
		{
			"transCarrierID"
		};

		// Token: 0x04000AF4 RID: 2804
		private static readonly uint[] _objectICarriableTransFieldTags = new uint[]
		{
			8u
		};

		// Token: 0x04000AF5 RID: 2805
		private bool hasTransCarrierID;

		// Token: 0x04000AF6 RID: 2806
		private int transCarrierID_;

		// Token: 0x04000AF7 RID: 2807
		private int memoizedSerializedSize = -1;

		// Token: 0x02000253 RID: 595
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<objectICarriableTrans, objectICarriableTrans.Builder>
		{
			// Token: 0x06001484 RID: 5252 RVA: 0x00046674 File Offset: 0x00044874
			public Builder()
			{
				this.result = objectICarriableTrans.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x06001485 RID: 5253 RVA: 0x00046690 File Offset: 0x00044890
			internal Builder(objectICarriableTrans cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x170005AF RID: 1455
			// (get) Token: 0x06001486 RID: 5254 RVA: 0x000466A8 File Offset: 0x000448A8
			protected override objectICarriableTrans.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x06001487 RID: 5255 RVA: 0x000466AC File Offset: 0x000448AC
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

			// Token: 0x170005B0 RID: 1456
			// (get) Token: 0x06001488 RID: 5256 RVA: 0x000466EC File Offset: 0x000448EC
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x170005B1 RID: 1457
			// (get) Token: 0x06001489 RID: 5257 RVA: 0x000466FC File Offset: 0x000448FC
			protected override objectICarriableTrans MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x0600148A RID: 5258 RVA: 0x00046704 File Offset: 0x00044904
			public override objectICarriableTrans.Builder Clear()
			{
				this.result = objectICarriableTrans.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x0600148B RID: 5259 RVA: 0x0004671C File Offset: 0x0004491C
			public override objectICarriableTrans.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new objectICarriableTrans.Builder(this.result);
				}
				return new objectICarriableTrans.Builder().MergeFrom(this.result);
			}

			// Token: 0x170005B2 RID: 1458
			// (get) Token: 0x0600148C RID: 5260 RVA: 0x00046748 File Offset: 0x00044948
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return objectICarriableTrans.Descriptor;
				}
			}

			// Token: 0x170005B3 RID: 1459
			// (get) Token: 0x0600148D RID: 5261 RVA: 0x00046750 File Offset: 0x00044950
			public override objectICarriableTrans DefaultInstanceForType
			{
				get
				{
					return objectICarriableTrans.DefaultInstance;
				}
			}

			// Token: 0x0600148E RID: 5262 RVA: 0x00046758 File Offset: 0x00044958
			public override objectICarriableTrans BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x0600148F RID: 5263 RVA: 0x0004678C File Offset: 0x0004498C
			public override objectICarriableTrans.Builder MergeFrom(IMessage other)
			{
				if (other is objectICarriableTrans)
				{
					return this.MergeFrom((objectICarriableTrans)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x06001490 RID: 5264 RVA: 0x000467B0 File Offset: 0x000449B0
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

			// Token: 0x06001491 RID: 5265 RVA: 0x000467F8 File Offset: 0x000449F8
			public override objectICarriableTrans.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x06001492 RID: 5266 RVA: 0x00046808 File Offset: 0x00044A08
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

			// Token: 0x170005B4 RID: 1460
			// (get) Token: 0x06001493 RID: 5267 RVA: 0x0004691C File Offset: 0x00044B1C
			public bool HasTransCarrierID
			{
				get
				{
					return this.result.hasTransCarrierID;
				}
			}

			// Token: 0x170005B5 RID: 1461
			// (get) Token: 0x06001494 RID: 5268 RVA: 0x0004692C File Offset: 0x00044B2C
			// (set) Token: 0x06001495 RID: 5269 RVA: 0x0004693C File Offset: 0x00044B3C
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

			// Token: 0x06001496 RID: 5270 RVA: 0x00046948 File Offset: 0x00044B48
			public objectICarriableTrans.Builder SetTransCarrierID(int value)
			{
				this.PrepareBuilder();
				this.result.hasTransCarrierID = true;
				this.result.transCarrierID_ = value;
				return this;
			}

			// Token: 0x06001497 RID: 5271 RVA: 0x00046978 File Offset: 0x00044B78
			public objectICarriableTrans.Builder ClearTransCarrierID()
			{
				this.PrepareBuilder();
				this.result.hasTransCarrierID = false;
				this.result.transCarrierID_ = 0;
				return this;
			}

			// Token: 0x04000AF8 RID: 2808
			private bool resultIsReadOnly;

			// Token: 0x04000AF9 RID: 2809
			private objectICarriableTrans result;
		}
	}
}
