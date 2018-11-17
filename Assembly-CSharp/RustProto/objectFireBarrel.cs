using System;
using System.Diagnostics;
using System.IO;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;
using RustProto.Helpers;

namespace RustProto
{
	// Token: 0x02000217 RID: 535
	[DebuggerNonUserCode]
	public sealed class objectFireBarrel : GeneratedMessage<objectFireBarrel, objectFireBarrel.Builder>
	{
		// Token: 0x06001220 RID: 4640 RVA: 0x00040300 File Offset: 0x0003E500
		private objectFireBarrel()
		{
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x00040310 File Offset: 0x0003E510
		static objectFireBarrel()
		{
			object.ReferenceEquals(Worldsave.Descriptor, null);
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x00040350 File Offset: 0x0003E550
		public static Recycler<objectFireBarrel, objectFireBarrel.Builder> Recycler()
		{
			return Recycler<objectFireBarrel, objectFireBarrel.Builder>.Manufacture();
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06001223 RID: 4643 RVA: 0x00040358 File Offset: 0x0003E558
		public static objectFireBarrel DefaultInstance
		{
			get
			{
				return objectFireBarrel.defaultInstance;
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06001224 RID: 4644 RVA: 0x00040360 File Offset: 0x0003E560
		public override objectFireBarrel DefaultInstanceForType
		{
			get
			{
				return objectFireBarrel.DefaultInstance;
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06001225 RID: 4645 RVA: 0x00040368 File Offset: 0x0003E568
		protected override objectFireBarrel ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001226 RID: 4646 RVA: 0x0004036C File Offset: 0x0003E56C
		public static MessageDescriptor Descriptor
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectFireBarrel__Descriptor;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001227 RID: 4647 RVA: 0x00040374 File Offset: 0x0003E574
		protected override FieldAccessorTable<objectFireBarrel, objectFireBarrel.Builder> InternalFieldAccessors
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectFireBarrel__FieldAccessorTable;
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001228 RID: 4648 RVA: 0x0004037C File Offset: 0x0003E57C
		public bool HasOnFire
		{
			get
			{
				return this.hasOnFire;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001229 RID: 4649 RVA: 0x00040384 File Offset: 0x0003E584
		public bool OnFire
		{
			get
			{
				return this.onFire_;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x0600122A RID: 4650 RVA: 0x0004038C File Offset: 0x0003E58C
		public override bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x00040390 File Offset: 0x0003E590
		public override void WriteTo(ICodedOutputStream output)
		{
			int serializedSize = this.SerializedSize;
			string[] objectFireBarrelFieldNames = objectFireBarrel._objectFireBarrelFieldNames;
			if (this.hasOnFire)
			{
				output.WriteBool(1, objectFireBarrelFieldNames[0], this.OnFire);
			}
			this.UnknownFields.WriteTo(output);
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x0600122C RID: 4652 RVA: 0x000403D4 File Offset: 0x0003E5D4
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
				if (this.hasOnFire)
				{
					num += CodedOutputStream.ComputeBoolSize(1, this.OnFire);
				}
				num += this.UnknownFields.SerializedSize;
				this.memoizedSerializedSize = num;
				return num;
			}
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x00040424 File Offset: 0x0003E624
		public static objectFireBarrel ParseFrom(ByteString data)
		{
			return objectFireBarrel.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x00040438 File Offset: 0x0003E638
		public static objectFireBarrel ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return objectFireBarrel.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x0004044C File Offset: 0x0003E64C
		public static objectFireBarrel ParseFrom(byte[] data)
		{
			return objectFireBarrel.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x00040460 File Offset: 0x0003E660
		public static objectFireBarrel ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return objectFireBarrel.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x00040474 File Offset: 0x0003E674
		public static objectFireBarrel ParseFrom(Stream input)
		{
			return objectFireBarrel.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x00040488 File Offset: 0x0003E688
		public static objectFireBarrel ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectFireBarrel.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x0004049C File Offset: 0x0003E69C
		public static objectFireBarrel ParseDelimitedFrom(Stream input)
		{
			return objectFireBarrel.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x000404B0 File Offset: 0x0003E6B0
		public static objectFireBarrel ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectFireBarrel.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x000404C4 File Offset: 0x0003E6C4
		public static objectFireBarrel ParseFrom(ICodedInputStream input)
		{
			return objectFireBarrel.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x000404D8 File Offset: 0x0003E6D8
		public static objectFireBarrel ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return objectFireBarrel.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x000404EC File Offset: 0x0003E6EC
		private objectFireBarrel MakeReadOnly()
		{
			return this;
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x000404F0 File Offset: 0x0003E6F0
		public static objectFireBarrel.Builder CreateBuilder()
		{
			return new objectFireBarrel.Builder();
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x000404F8 File Offset: 0x0003E6F8
		public override objectFireBarrel.Builder ToBuilder()
		{
			return objectFireBarrel.CreateBuilder(this);
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x00040500 File Offset: 0x0003E700
		public override objectFireBarrel.Builder CreateBuilderForType()
		{
			return new objectFireBarrel.Builder();
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x00040508 File Offset: 0x0003E708
		public static objectFireBarrel.Builder CreateBuilder(objectFireBarrel prototype)
		{
			return new objectFireBarrel.Builder(prototype);
		}

		// Token: 0x04000996 RID: 2454
		public const int OnFireFieldNumber = 1;

		// Token: 0x04000997 RID: 2455
		private static readonly objectFireBarrel defaultInstance = new objectFireBarrel().MakeReadOnly();

		// Token: 0x04000998 RID: 2456
		private static readonly string[] _objectFireBarrelFieldNames = new string[]
		{
			"OnFire"
		};

		// Token: 0x04000999 RID: 2457
		private static readonly uint[] _objectFireBarrelFieldTags = new uint[]
		{
			8u
		};

		// Token: 0x0400099A RID: 2458
		private bool hasOnFire;

		// Token: 0x0400099B RID: 2459
		private bool onFire_;

		// Token: 0x0400099C RID: 2460
		private int memoizedSerializedSize = -1;

		// Token: 0x02000218 RID: 536
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<objectFireBarrel, objectFireBarrel.Builder>
		{
			// Token: 0x0600123C RID: 4668 RVA: 0x00040510 File Offset: 0x0003E710
			public Builder()
			{
				this.result = objectFireBarrel.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x0600123D RID: 4669 RVA: 0x0004052C File Offset: 0x0003E72C
			internal Builder(objectFireBarrel cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x1700050B RID: 1291
			// (get) Token: 0x0600123E RID: 4670 RVA: 0x00040544 File Offset: 0x0003E744
			protected override objectFireBarrel.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x0600123F RID: 4671 RVA: 0x00040548 File Offset: 0x0003E748
			private objectFireBarrel PrepareBuilder()
			{
				if (this.resultIsReadOnly)
				{
					objectFireBarrel other = this.result;
					this.result = new objectFireBarrel();
					this.resultIsReadOnly = false;
					this.MergeFrom(other);
				}
				return this.result;
			}

			// Token: 0x1700050C RID: 1292
			// (get) Token: 0x06001240 RID: 4672 RVA: 0x00040588 File Offset: 0x0003E788
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x1700050D RID: 1293
			// (get) Token: 0x06001241 RID: 4673 RVA: 0x00040598 File Offset: 0x0003E798
			protected override objectFireBarrel MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x06001242 RID: 4674 RVA: 0x000405A0 File Offset: 0x0003E7A0
			public override objectFireBarrel.Builder Clear()
			{
				this.result = objectFireBarrel.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x06001243 RID: 4675 RVA: 0x000405B8 File Offset: 0x0003E7B8
			public override objectFireBarrel.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new objectFireBarrel.Builder(this.result);
				}
				return new objectFireBarrel.Builder().MergeFrom(this.result);
			}

			// Token: 0x1700050E RID: 1294
			// (get) Token: 0x06001244 RID: 4676 RVA: 0x000405E4 File Offset: 0x0003E7E4
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return objectFireBarrel.Descriptor;
				}
			}

			// Token: 0x1700050F RID: 1295
			// (get) Token: 0x06001245 RID: 4677 RVA: 0x000405EC File Offset: 0x0003E7EC
			public override objectFireBarrel DefaultInstanceForType
			{
				get
				{
					return objectFireBarrel.DefaultInstance;
				}
			}

			// Token: 0x06001246 RID: 4678 RVA: 0x000405F4 File Offset: 0x0003E7F4
			public override objectFireBarrel BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x06001247 RID: 4679 RVA: 0x00040628 File Offset: 0x0003E828
			public override objectFireBarrel.Builder MergeFrom(IMessage other)
			{
				if (other is objectFireBarrel)
				{
					return this.MergeFrom((objectFireBarrel)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x06001248 RID: 4680 RVA: 0x0004064C File Offset: 0x0003E84C
			public override objectFireBarrel.Builder MergeFrom(objectFireBarrel other)
			{
				if (other == objectFireBarrel.DefaultInstance)
				{
					return this;
				}
				this.PrepareBuilder();
				if (other.HasOnFire)
				{
					this.OnFire = other.OnFire;
				}
				this.MergeUnknownFields(other.UnknownFields);
				return this;
			}

			// Token: 0x06001249 RID: 4681 RVA: 0x00040694 File Offset: 0x0003E894
			public override objectFireBarrel.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x0600124A RID: 4682 RVA: 0x000406A4 File Offset: 0x0003E8A4
			public override objectFireBarrel.Builder MergeFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
			{
				this.PrepareBuilder();
				UnknownFieldSet.Builder builder = null;
				uint num;
				string text;
				while (input.ReadTag(ref num, ref text))
				{
					if (num == 0u && text != null)
					{
						int num2 = Array.BinarySearch<string>(objectFireBarrel._objectFireBarrelFieldNames, text, StringComparer.Ordinal);
						if (num2 < 0)
						{
							if (builder == null)
							{
								builder = UnknownFieldSet.CreateBuilder(this.UnknownFields);
							}
							this.ParseUnknownField(input, builder, extensionRegistry, num, text);
							continue;
						}
						num = objectFireBarrel._objectFireBarrelFieldTags[num2];
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
						this.result.hasOnFire = input.ReadBool(ref this.result.onFire_);
					}
				}
				if (builder != null)
				{
					this.UnknownFields = builder.Build();
				}
				return this;
			}

			// Token: 0x17000510 RID: 1296
			// (get) Token: 0x0600124B RID: 4683 RVA: 0x000407B8 File Offset: 0x0003E9B8
			public bool HasOnFire
			{
				get
				{
					return this.result.hasOnFire;
				}
			}

			// Token: 0x17000511 RID: 1297
			// (get) Token: 0x0600124C RID: 4684 RVA: 0x000407C8 File Offset: 0x0003E9C8
			// (set) Token: 0x0600124D RID: 4685 RVA: 0x000407D8 File Offset: 0x0003E9D8
			public bool OnFire
			{
				get
				{
					return this.result.OnFire;
				}
				set
				{
					this.SetOnFire(value);
				}
			}

			// Token: 0x0600124E RID: 4686 RVA: 0x000407E4 File Offset: 0x0003E9E4
			public objectFireBarrel.Builder SetOnFire(bool value)
			{
				this.PrepareBuilder();
				this.result.hasOnFire = true;
				this.result.onFire_ = value;
				return this;
			}

			// Token: 0x0600124F RID: 4687 RVA: 0x00040814 File Offset: 0x0003EA14
			public objectFireBarrel.Builder ClearOnFire()
			{
				this.PrepareBuilder();
				this.result.hasOnFire = false;
				this.result.onFire_ = false;
				return this;
			}

			// Token: 0x0400099D RID: 2461
			private bool resultIsReadOnly;

			// Token: 0x0400099E RID: 2462
			private objectFireBarrel result;
		}
	}
}
