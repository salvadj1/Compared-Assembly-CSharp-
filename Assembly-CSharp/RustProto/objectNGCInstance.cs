using System;
using System.Diagnostics;
using System.IO;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;
using RustProto.Helpers;

namespace RustProto
{
	// Token: 0x0200020D RID: 525
	[DebuggerNonUserCode]
	public sealed class objectNGCInstance : GeneratedMessage<objectNGCInstance, objectNGCInstance.Builder>
	{
		// Token: 0x060010F6 RID: 4342 RVA: 0x0003DEA4 File Offset: 0x0003C0A4
		private objectNGCInstance()
		{
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x0003DEC0 File Offset: 0x0003C0C0
		static objectNGCInstance()
		{
			object.ReferenceEquals(Worldsave.Descriptor, null);
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x0003DF18 File Offset: 0x0003C118
		public static Recycler<objectNGCInstance, objectNGCInstance.Builder> Recycler()
		{
			return Recycler<objectNGCInstance, objectNGCInstance.Builder>.Manufacture();
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x060010F9 RID: 4345 RVA: 0x0003DF20 File Offset: 0x0003C120
		public static objectNGCInstance DefaultInstance
		{
			get
			{
				return objectNGCInstance.defaultInstance;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x060010FA RID: 4346 RVA: 0x0003DF28 File Offset: 0x0003C128
		public override objectNGCInstance DefaultInstanceForType
		{
			get
			{
				return objectNGCInstance.DefaultInstance;
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x060010FB RID: 4347 RVA: 0x0003DF30 File Offset: 0x0003C130
		protected override objectNGCInstance ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x060010FC RID: 4348 RVA: 0x0003DF34 File Offset: 0x0003C134
		public static MessageDescriptor Descriptor
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectNGCInstance__Descriptor;
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x060010FD RID: 4349 RVA: 0x0003DF3C File Offset: 0x0003C13C
		protected override FieldAccessorTable<objectNGCInstance, objectNGCInstance.Builder> InternalFieldAccessors
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectNGCInstance__FieldAccessorTable;
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x060010FE RID: 4350 RVA: 0x0003DF44 File Offset: 0x0003C144
		public bool HasID
		{
			get
			{
				return this.hasID;
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x060010FF RID: 4351 RVA: 0x0003DF4C File Offset: 0x0003C14C
		public int ID
		{
			get
			{
				return this.iD_;
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06001100 RID: 4352 RVA: 0x0003DF54 File Offset: 0x0003C154
		public bool HasData
		{
			get
			{
				return this.hasData;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06001101 RID: 4353 RVA: 0x0003DF5C File Offset: 0x0003C15C
		public ByteString Data
		{
			get
			{
				return this.data_;
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001102 RID: 4354 RVA: 0x0003DF64 File Offset: 0x0003C164
		public override bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x0003DF68 File Offset: 0x0003C168
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

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06001104 RID: 4356 RVA: 0x0003DFC4 File Offset: 0x0003C1C4
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

		// Token: 0x06001105 RID: 4357 RVA: 0x0003E030 File Offset: 0x0003C230
		public static objectNGCInstance ParseFrom(ByteString data)
		{
			return objectNGCInstance.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x0003E044 File Offset: 0x0003C244
		public static objectNGCInstance ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return objectNGCInstance.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x0003E058 File Offset: 0x0003C258
		public static objectNGCInstance ParseFrom(byte[] data)
		{
			return objectNGCInstance.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x0003E06C File Offset: 0x0003C26C
		public static objectNGCInstance ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return objectNGCInstance.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x0003E080 File Offset: 0x0003C280
		public static objectNGCInstance ParseFrom(Stream input)
		{
			return objectNGCInstance.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x0003E094 File Offset: 0x0003C294
		public static objectNGCInstance ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectNGCInstance.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x0003E0A8 File Offset: 0x0003C2A8
		public static objectNGCInstance ParseDelimitedFrom(Stream input)
		{
			return objectNGCInstance.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x0003E0BC File Offset: 0x0003C2BC
		public static objectNGCInstance ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectNGCInstance.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x0003E0D0 File Offset: 0x0003C2D0
		public static objectNGCInstance ParseFrom(ICodedInputStream input)
		{
			return objectNGCInstance.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x0003E0E4 File Offset: 0x0003C2E4
		public static objectNGCInstance ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return objectNGCInstance.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x0003E0F8 File Offset: 0x0003C2F8
		private objectNGCInstance MakeReadOnly()
		{
			return this;
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x0003E0FC File Offset: 0x0003C2FC
		public static objectNGCInstance.Builder CreateBuilder()
		{
			return new objectNGCInstance.Builder();
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x0003E104 File Offset: 0x0003C304
		public override objectNGCInstance.Builder ToBuilder()
		{
			return objectNGCInstance.CreateBuilder(this);
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x0003E10C File Offset: 0x0003C30C
		public override objectNGCInstance.Builder CreateBuilderForType()
		{
			return new objectNGCInstance.Builder();
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x0003E114 File Offset: 0x0003C314
		public static objectNGCInstance.Builder CreateBuilder(objectNGCInstance prototype)
		{
			return new objectNGCInstance.Builder(prototype);
		}

		// Token: 0x04000951 RID: 2385
		public const int IDFieldNumber = 1;

		// Token: 0x04000952 RID: 2386
		public const int DataFieldNumber = 2;

		// Token: 0x04000953 RID: 2387
		private static readonly objectNGCInstance defaultInstance = new objectNGCInstance().MakeReadOnly();

		// Token: 0x04000954 RID: 2388
		private static readonly string[] _objectNGCInstanceFieldNames = new string[]
		{
			"ID",
			"data"
		};

		// Token: 0x04000955 RID: 2389
		private static readonly uint[] _objectNGCInstanceFieldTags = new uint[]
		{
			8u,
			18u
		};

		// Token: 0x04000956 RID: 2390
		private bool hasID;

		// Token: 0x04000957 RID: 2391
		private int iD_;

		// Token: 0x04000958 RID: 2392
		private bool hasData;

		// Token: 0x04000959 RID: 2393
		private ByteString data_ = ByteString.Empty;

		// Token: 0x0400095A RID: 2394
		private int memoizedSerializedSize = -1;

		// Token: 0x0200020E RID: 526
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<objectNGCInstance, objectNGCInstance.Builder>
		{
			// Token: 0x06001114 RID: 4372 RVA: 0x0003E11C File Offset: 0x0003C31C
			public Builder()
			{
				this.result = objectNGCInstance.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x06001115 RID: 4373 RVA: 0x0003E138 File Offset: 0x0003C338
			internal Builder(objectNGCInstance cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x1700049D RID: 1181
			// (get) Token: 0x06001116 RID: 4374 RVA: 0x0003E150 File Offset: 0x0003C350
			protected override objectNGCInstance.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x06001117 RID: 4375 RVA: 0x0003E154 File Offset: 0x0003C354
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

			// Token: 0x1700049E RID: 1182
			// (get) Token: 0x06001118 RID: 4376 RVA: 0x0003E194 File Offset: 0x0003C394
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x1700049F RID: 1183
			// (get) Token: 0x06001119 RID: 4377 RVA: 0x0003E1A4 File Offset: 0x0003C3A4
			protected override objectNGCInstance MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x0600111A RID: 4378 RVA: 0x0003E1AC File Offset: 0x0003C3AC
			public override objectNGCInstance.Builder Clear()
			{
				this.result = objectNGCInstance.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x0600111B RID: 4379 RVA: 0x0003E1C4 File Offset: 0x0003C3C4
			public override objectNGCInstance.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new objectNGCInstance.Builder(this.result);
				}
				return new objectNGCInstance.Builder().MergeFrom(this.result);
			}

			// Token: 0x170004A0 RID: 1184
			// (get) Token: 0x0600111C RID: 4380 RVA: 0x0003E1F0 File Offset: 0x0003C3F0
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return objectNGCInstance.Descriptor;
				}
			}

			// Token: 0x170004A1 RID: 1185
			// (get) Token: 0x0600111D RID: 4381 RVA: 0x0003E1F8 File Offset: 0x0003C3F8
			public override objectNGCInstance DefaultInstanceForType
			{
				get
				{
					return objectNGCInstance.DefaultInstance;
				}
			}

			// Token: 0x0600111E RID: 4382 RVA: 0x0003E200 File Offset: 0x0003C400
			public override objectNGCInstance BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x0600111F RID: 4383 RVA: 0x0003E234 File Offset: 0x0003C434
			public override objectNGCInstance.Builder MergeFrom(IMessage other)
			{
				if (other is objectNGCInstance)
				{
					return this.MergeFrom((objectNGCInstance)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x06001120 RID: 4384 RVA: 0x0003E258 File Offset: 0x0003C458
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

			// Token: 0x06001121 RID: 4385 RVA: 0x0003E2B8 File Offset: 0x0003C4B8
			public override objectNGCInstance.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x06001122 RID: 4386 RVA: 0x0003E2C8 File Offset: 0x0003C4C8
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

			// Token: 0x170004A2 RID: 1186
			// (get) Token: 0x06001123 RID: 4387 RVA: 0x0003E404 File Offset: 0x0003C604
			public bool HasID
			{
				get
				{
					return this.result.hasID;
				}
			}

			// Token: 0x170004A3 RID: 1187
			// (get) Token: 0x06001124 RID: 4388 RVA: 0x0003E414 File Offset: 0x0003C614
			// (set) Token: 0x06001125 RID: 4389 RVA: 0x0003E424 File Offset: 0x0003C624
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

			// Token: 0x06001126 RID: 4390 RVA: 0x0003E430 File Offset: 0x0003C630
			public objectNGCInstance.Builder SetID(int value)
			{
				this.PrepareBuilder();
				this.result.hasID = true;
				this.result.iD_ = value;
				return this;
			}

			// Token: 0x06001127 RID: 4391 RVA: 0x0003E460 File Offset: 0x0003C660
			public objectNGCInstance.Builder ClearID()
			{
				this.PrepareBuilder();
				this.result.hasID = false;
				this.result.iD_ = 0;
				return this;
			}

			// Token: 0x170004A4 RID: 1188
			// (get) Token: 0x06001128 RID: 4392 RVA: 0x0003E490 File Offset: 0x0003C690
			public bool HasData
			{
				get
				{
					return this.result.hasData;
				}
			}

			// Token: 0x170004A5 RID: 1189
			// (get) Token: 0x06001129 RID: 4393 RVA: 0x0003E4A0 File Offset: 0x0003C6A0
			// (set) Token: 0x0600112A RID: 4394 RVA: 0x0003E4B0 File Offset: 0x0003C6B0
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

			// Token: 0x0600112B RID: 4395 RVA: 0x0003E4BC File Offset: 0x0003C6BC
			public objectNGCInstance.Builder SetData(ByteString value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasData = true;
				this.result.data_ = value;
				return this;
			}

			// Token: 0x0600112C RID: 4396 RVA: 0x0003E4EC File Offset: 0x0003C6EC
			public objectNGCInstance.Builder ClearData()
			{
				this.PrepareBuilder();
				this.result.hasData = false;
				this.result.data_ = ByteString.Empty;
				return this;
			}

			// Token: 0x0400095B RID: 2395
			private bool resultIsReadOnly;

			// Token: 0x0400095C RID: 2396
			private objectNGCInstance result;
		}
	}
}
