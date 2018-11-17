using System;
using System.Diagnostics;
using System.IO;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;
using RustProto.Helpers;

namespace RustProto
{
	// Token: 0x0200020F RID: 527
	[DebuggerNonUserCode]
	public sealed class objectDeployable : GeneratedMessage<objectDeployable, objectDeployable.Builder>
	{
		// Token: 0x0600112D RID: 4397 RVA: 0x0003E520 File Offset: 0x0003C720
		private objectDeployable()
		{
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x0003E530 File Offset: 0x0003C730
		static objectDeployable()
		{
			object.ReferenceEquals(Worldsave.Descriptor, null);
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x0003E588 File Offset: 0x0003C788
		public static Recycler<objectDeployable, objectDeployable.Builder> Recycler()
		{
			return Recycler<objectDeployable, objectDeployable.Builder>.Manufacture();
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001130 RID: 4400 RVA: 0x0003E590 File Offset: 0x0003C790
		public static objectDeployable DefaultInstance
		{
			get
			{
				return objectDeployable.defaultInstance;
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001131 RID: 4401 RVA: 0x0003E598 File Offset: 0x0003C798
		public override objectDeployable DefaultInstanceForType
		{
			get
			{
				return objectDeployable.DefaultInstance;
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001132 RID: 4402 RVA: 0x0003E5A0 File Offset: 0x0003C7A0
		protected override objectDeployable ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001133 RID: 4403 RVA: 0x0003E5A4 File Offset: 0x0003C7A4
		public static MessageDescriptor Descriptor
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectDeployable__Descriptor;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001134 RID: 4404 RVA: 0x0003E5AC File Offset: 0x0003C7AC
		protected override FieldAccessorTable<objectDeployable, objectDeployable.Builder> InternalFieldAccessors
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectDeployable__FieldAccessorTable;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001135 RID: 4405 RVA: 0x0003E5B4 File Offset: 0x0003C7B4
		public bool HasCreatorID
		{
			get
			{
				return this.hasCreatorID;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06001136 RID: 4406 RVA: 0x0003E5BC File Offset: 0x0003C7BC
		[CLSCompliant(false)]
		public ulong CreatorID
		{
			get
			{
				return this.creatorID_;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06001137 RID: 4407 RVA: 0x0003E5C4 File Offset: 0x0003C7C4
		public bool HasOwnerID
		{
			get
			{
				return this.hasOwnerID;
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06001138 RID: 4408 RVA: 0x0003E5CC File Offset: 0x0003C7CC
		[CLSCompliant(false)]
		public ulong OwnerID
		{
			get
			{
				return this.ownerID_;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001139 RID: 4409 RVA: 0x0003E5D4 File Offset: 0x0003C7D4
		public override bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x0003E5D8 File Offset: 0x0003C7D8
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

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x0600113B RID: 4411 RVA: 0x0003E634 File Offset: 0x0003C834
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

		// Token: 0x0600113C RID: 4412 RVA: 0x0003E6A0 File Offset: 0x0003C8A0
		public static objectDeployable ParseFrom(ByteString data)
		{
			return objectDeployable.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x0003E6B4 File Offset: 0x0003C8B4
		public static objectDeployable ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return objectDeployable.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x0003E6C8 File Offset: 0x0003C8C8
		public static objectDeployable ParseFrom(byte[] data)
		{
			return objectDeployable.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x0003E6DC File Offset: 0x0003C8DC
		public static objectDeployable ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return objectDeployable.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x0003E6F0 File Offset: 0x0003C8F0
		public static objectDeployable ParseFrom(Stream input)
		{
			return objectDeployable.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x0003E704 File Offset: 0x0003C904
		public static objectDeployable ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectDeployable.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x0003E718 File Offset: 0x0003C918
		public static objectDeployable ParseDelimitedFrom(Stream input)
		{
			return objectDeployable.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x0003E72C File Offset: 0x0003C92C
		public static objectDeployable ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectDeployable.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x0003E740 File Offset: 0x0003C940
		public static objectDeployable ParseFrom(ICodedInputStream input)
		{
			return objectDeployable.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x0003E754 File Offset: 0x0003C954
		public static objectDeployable ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return objectDeployable.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x0003E768 File Offset: 0x0003C968
		private objectDeployable MakeReadOnly()
		{
			return this;
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x0003E76C File Offset: 0x0003C96C
		public static objectDeployable.Builder CreateBuilder()
		{
			return new objectDeployable.Builder();
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x0003E774 File Offset: 0x0003C974
		public override objectDeployable.Builder ToBuilder()
		{
			return objectDeployable.CreateBuilder(this);
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x0003E77C File Offset: 0x0003C97C
		public override objectDeployable.Builder CreateBuilderForType()
		{
			return new objectDeployable.Builder();
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x0003E784 File Offset: 0x0003C984
		public static objectDeployable.Builder CreateBuilder(objectDeployable prototype)
		{
			return new objectDeployable.Builder(prototype);
		}

		// Token: 0x0400095D RID: 2397
		public const int CreatorIDFieldNumber = 1;

		// Token: 0x0400095E RID: 2398
		public const int OwnerIDFieldNumber = 2;

		// Token: 0x0400095F RID: 2399
		private static readonly objectDeployable defaultInstance = new objectDeployable().MakeReadOnly();

		// Token: 0x04000960 RID: 2400
		private static readonly string[] _objectDeployableFieldNames = new string[]
		{
			"CreatorID",
			"OwnerID"
		};

		// Token: 0x04000961 RID: 2401
		private static readonly uint[] _objectDeployableFieldTags = new uint[]
		{
			8u,
			16u
		};

		// Token: 0x04000962 RID: 2402
		private bool hasCreatorID;

		// Token: 0x04000963 RID: 2403
		private ulong creatorID_;

		// Token: 0x04000964 RID: 2404
		private bool hasOwnerID;

		// Token: 0x04000965 RID: 2405
		private ulong ownerID_;

		// Token: 0x04000966 RID: 2406
		private int memoizedSerializedSize = -1;

		// Token: 0x02000210 RID: 528
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<objectDeployable, objectDeployable.Builder>
		{
			// Token: 0x0600114B RID: 4427 RVA: 0x0003E78C File Offset: 0x0003C98C
			public Builder()
			{
				this.result = objectDeployable.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x0600114C RID: 4428 RVA: 0x0003E7A8 File Offset: 0x0003C9A8
			internal Builder(objectDeployable cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x170004B1 RID: 1201
			// (get) Token: 0x0600114D RID: 4429 RVA: 0x0003E7C0 File Offset: 0x0003C9C0
			protected override objectDeployable.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x0600114E RID: 4430 RVA: 0x0003E7C4 File Offset: 0x0003C9C4
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

			// Token: 0x170004B2 RID: 1202
			// (get) Token: 0x0600114F RID: 4431 RVA: 0x0003E804 File Offset: 0x0003CA04
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x170004B3 RID: 1203
			// (get) Token: 0x06001150 RID: 4432 RVA: 0x0003E814 File Offset: 0x0003CA14
			protected override objectDeployable MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x06001151 RID: 4433 RVA: 0x0003E81C File Offset: 0x0003CA1C
			public override objectDeployable.Builder Clear()
			{
				this.result = objectDeployable.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x06001152 RID: 4434 RVA: 0x0003E834 File Offset: 0x0003CA34
			public override objectDeployable.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new objectDeployable.Builder(this.result);
				}
				return new objectDeployable.Builder().MergeFrom(this.result);
			}

			// Token: 0x170004B4 RID: 1204
			// (get) Token: 0x06001153 RID: 4435 RVA: 0x0003E860 File Offset: 0x0003CA60
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return objectDeployable.Descriptor;
				}
			}

			// Token: 0x170004B5 RID: 1205
			// (get) Token: 0x06001154 RID: 4436 RVA: 0x0003E868 File Offset: 0x0003CA68
			public override objectDeployable DefaultInstanceForType
			{
				get
				{
					return objectDeployable.DefaultInstance;
				}
			}

			// Token: 0x06001155 RID: 4437 RVA: 0x0003E870 File Offset: 0x0003CA70
			public override objectDeployable BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x06001156 RID: 4438 RVA: 0x0003E8A4 File Offset: 0x0003CAA4
			public override objectDeployable.Builder MergeFrom(IMessage other)
			{
				if (other is objectDeployable)
				{
					return this.MergeFrom((objectDeployable)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x06001157 RID: 4439 RVA: 0x0003E8C8 File Offset: 0x0003CAC8
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

			// Token: 0x06001158 RID: 4440 RVA: 0x0003E928 File Offset: 0x0003CB28
			public override objectDeployable.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x06001159 RID: 4441 RVA: 0x0003E938 File Offset: 0x0003CB38
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

			// Token: 0x170004B6 RID: 1206
			// (get) Token: 0x0600115A RID: 4442 RVA: 0x0003EA74 File Offset: 0x0003CC74
			public bool HasCreatorID
			{
				get
				{
					return this.result.hasCreatorID;
				}
			}

			// Token: 0x170004B7 RID: 1207
			// (get) Token: 0x0600115B RID: 4443 RVA: 0x0003EA84 File Offset: 0x0003CC84
			// (set) Token: 0x0600115C RID: 4444 RVA: 0x0003EA94 File Offset: 0x0003CC94
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

			// Token: 0x0600115D RID: 4445 RVA: 0x0003EAA0 File Offset: 0x0003CCA0
			[CLSCompliant(false)]
			public objectDeployable.Builder SetCreatorID(ulong value)
			{
				this.PrepareBuilder();
				this.result.hasCreatorID = true;
				this.result.creatorID_ = value;
				return this;
			}

			// Token: 0x0600115E RID: 4446 RVA: 0x0003EAD0 File Offset: 0x0003CCD0
			public objectDeployable.Builder ClearCreatorID()
			{
				this.PrepareBuilder();
				this.result.hasCreatorID = false;
				this.result.creatorID_ = 0UL;
				return this;
			}

			// Token: 0x170004B8 RID: 1208
			// (get) Token: 0x0600115F RID: 4447 RVA: 0x0003EAF4 File Offset: 0x0003CCF4
			public bool HasOwnerID
			{
				get
				{
					return this.result.hasOwnerID;
				}
			}

			// Token: 0x170004B9 RID: 1209
			// (get) Token: 0x06001160 RID: 4448 RVA: 0x0003EB04 File Offset: 0x0003CD04
			// (set) Token: 0x06001161 RID: 4449 RVA: 0x0003EB14 File Offset: 0x0003CD14
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

			// Token: 0x06001162 RID: 4450 RVA: 0x0003EB20 File Offset: 0x0003CD20
			[CLSCompliant(false)]
			public objectDeployable.Builder SetOwnerID(ulong value)
			{
				this.PrepareBuilder();
				this.result.hasOwnerID = true;
				this.result.ownerID_ = value;
				return this;
			}

			// Token: 0x06001163 RID: 4451 RVA: 0x0003EB50 File Offset: 0x0003CD50
			public objectDeployable.Builder ClearOwnerID()
			{
				this.PrepareBuilder();
				this.result.hasOwnerID = false;
				this.result.ownerID_ = 0UL;
				return this;
			}

			// Token: 0x04000967 RID: 2407
			private bool resultIsReadOnly;

			// Token: 0x04000968 RID: 2408
			private objectDeployable result;
		}
	}
}
