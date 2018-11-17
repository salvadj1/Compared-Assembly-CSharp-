using System;
using System.Diagnostics;
using System.IO;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;
using RustProto.Helpers;

namespace RustProto
{
	// Token: 0x02000211 RID: 529
	[DebuggerNonUserCode]
	public sealed class objectSleepingAvatar : GeneratedMessage<objectSleepingAvatar, objectSleepingAvatar.Builder>
	{
		// Token: 0x06001164 RID: 4452 RVA: 0x0003EB74 File Offset: 0x0003CD74
		private objectSleepingAvatar()
		{
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x0003EB84 File Offset: 0x0003CD84
		static objectSleepingAvatar()
		{
			object.ReferenceEquals(Worldsave.Descriptor, null);
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x0003EC00 File Offset: 0x0003CE00
		public static Recycler<objectSleepingAvatar, objectSleepingAvatar.Builder> Recycler()
		{
			return Recycler<objectSleepingAvatar, objectSleepingAvatar.Builder>.Manufacture();
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001167 RID: 4455 RVA: 0x0003EC08 File Offset: 0x0003CE08
		public static objectSleepingAvatar DefaultInstance
		{
			get
			{
				return objectSleepingAvatar.defaultInstance;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001168 RID: 4456 RVA: 0x0003EC10 File Offset: 0x0003CE10
		public override objectSleepingAvatar DefaultInstanceForType
		{
			get
			{
				return objectSleepingAvatar.DefaultInstance;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001169 RID: 4457 RVA: 0x0003EC18 File Offset: 0x0003CE18
		protected override objectSleepingAvatar ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x0600116A RID: 4458 RVA: 0x0003EC1C File Offset: 0x0003CE1C
		public static MessageDescriptor Descriptor
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectSleepingAvatar__Descriptor;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x0600116B RID: 4459 RVA: 0x0003EC24 File Offset: 0x0003CE24
		protected override FieldAccessorTable<objectSleepingAvatar, objectSleepingAvatar.Builder> InternalFieldAccessors
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectSleepingAvatar__FieldAccessorTable;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x0600116C RID: 4460 RVA: 0x0003EC2C File Offset: 0x0003CE2C
		public bool HasFootArmor
		{
			get
			{
				return this.hasFootArmor;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x0600116D RID: 4461 RVA: 0x0003EC34 File Offset: 0x0003CE34
		public int FootArmor
		{
			get
			{
				return this.footArmor_;
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x0600116E RID: 4462 RVA: 0x0003EC3C File Offset: 0x0003CE3C
		public bool HasLegArmor
		{
			get
			{
				return this.hasLegArmor;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x0600116F RID: 4463 RVA: 0x0003EC44 File Offset: 0x0003CE44
		public int LegArmor
		{
			get
			{
				return this.legArmor_;
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001170 RID: 4464 RVA: 0x0003EC4C File Offset: 0x0003CE4C
		public bool HasTorsoArmor
		{
			get
			{
				return this.hasTorsoArmor;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001171 RID: 4465 RVA: 0x0003EC54 File Offset: 0x0003CE54
		public int TorsoArmor
		{
			get
			{
				return this.torsoArmor_;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001172 RID: 4466 RVA: 0x0003EC5C File Offset: 0x0003CE5C
		public bool HasHeadArmor
		{
			get
			{
				return this.hasHeadArmor;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001173 RID: 4467 RVA: 0x0003EC64 File Offset: 0x0003CE64
		public int HeadArmor
		{
			get
			{
				return this.headArmor_;
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001174 RID: 4468 RVA: 0x0003EC6C File Offset: 0x0003CE6C
		public bool HasTimestamp
		{
			get
			{
				return this.hasTimestamp;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001175 RID: 4469 RVA: 0x0003EC74 File Offset: 0x0003CE74
		public int Timestamp
		{
			get
			{
				return this.timestamp_;
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001176 RID: 4470 RVA: 0x0003EC7C File Offset: 0x0003CE7C
		public bool HasVitals
		{
			get
			{
				return this.hasVitals;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06001177 RID: 4471 RVA: 0x0003EC84 File Offset: 0x0003CE84
		public Vitals Vitals
		{
			get
			{
				return this.vitals_ ?? Vitals.DefaultInstance;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001178 RID: 4472 RVA: 0x0003EC98 File Offset: 0x0003CE98
		public override bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x0003EC9C File Offset: 0x0003CE9C
		public override void WriteTo(ICodedOutputStream output)
		{
			int serializedSize = this.SerializedSize;
			string[] objectSleepingAvatarFieldNames = objectSleepingAvatar._objectSleepingAvatarFieldNames;
			if (this.hasFootArmor)
			{
				output.WriteInt32(1, objectSleepingAvatarFieldNames[0], this.FootArmor);
			}
			if (this.hasLegArmor)
			{
				output.WriteInt32(2, objectSleepingAvatarFieldNames[2], this.LegArmor);
			}
			if (this.hasTorsoArmor)
			{
				output.WriteInt32(3, objectSleepingAvatarFieldNames[4], this.TorsoArmor);
			}
			if (this.hasHeadArmor)
			{
				output.WriteInt32(4, objectSleepingAvatarFieldNames[1], this.HeadArmor);
			}
			if (this.hasTimestamp)
			{
				output.WriteInt32(5, objectSleepingAvatarFieldNames[3], this.Timestamp);
			}
			if (this.hasVitals)
			{
				output.WriteMessage(6, objectSleepingAvatarFieldNames[5], this.Vitals);
			}
			this.UnknownFields.WriteTo(output);
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x0600117A RID: 4474 RVA: 0x0003ED64 File Offset: 0x0003CF64
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
				if (this.hasFootArmor)
				{
					num += CodedOutputStream.ComputeInt32Size(1, this.FootArmor);
				}
				if (this.hasLegArmor)
				{
					num += CodedOutputStream.ComputeInt32Size(2, this.LegArmor);
				}
				if (this.hasTorsoArmor)
				{
					num += CodedOutputStream.ComputeInt32Size(3, this.TorsoArmor);
				}
				if (this.hasHeadArmor)
				{
					num += CodedOutputStream.ComputeInt32Size(4, this.HeadArmor);
				}
				if (this.hasTimestamp)
				{
					num += CodedOutputStream.ComputeInt32Size(5, this.Timestamp);
				}
				if (this.hasVitals)
				{
					num += CodedOutputStream.ComputeMessageSize(6, this.Vitals);
				}
				num += this.UnknownFields.SerializedSize;
				this.memoizedSerializedSize = num;
				return num;
			}
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x0003EE38 File Offset: 0x0003D038
		public static objectSleepingAvatar ParseFrom(ByteString data)
		{
			return objectSleepingAvatar.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x0003EE4C File Offset: 0x0003D04C
		public static objectSleepingAvatar ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return objectSleepingAvatar.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x0003EE60 File Offset: 0x0003D060
		public static objectSleepingAvatar ParseFrom(byte[] data)
		{
			return objectSleepingAvatar.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x0003EE74 File Offset: 0x0003D074
		public static objectSleepingAvatar ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return objectSleepingAvatar.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x0003EE88 File Offset: 0x0003D088
		public static objectSleepingAvatar ParseFrom(Stream input)
		{
			return objectSleepingAvatar.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x0003EE9C File Offset: 0x0003D09C
		public static objectSleepingAvatar ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectSleepingAvatar.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x0003EEB0 File Offset: 0x0003D0B0
		public static objectSleepingAvatar ParseDelimitedFrom(Stream input)
		{
			return objectSleepingAvatar.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x0003EEC4 File Offset: 0x0003D0C4
		public static objectSleepingAvatar ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectSleepingAvatar.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x0003EED8 File Offset: 0x0003D0D8
		public static objectSleepingAvatar ParseFrom(ICodedInputStream input)
		{
			return objectSleepingAvatar.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x0003EEEC File Offset: 0x0003D0EC
		public static objectSleepingAvatar ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return objectSleepingAvatar.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x0003EF00 File Offset: 0x0003D100
		private objectSleepingAvatar MakeReadOnly()
		{
			return this;
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x0003EF04 File Offset: 0x0003D104
		public static objectSleepingAvatar.Builder CreateBuilder()
		{
			return new objectSleepingAvatar.Builder();
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x0003EF0C File Offset: 0x0003D10C
		public override objectSleepingAvatar.Builder ToBuilder()
		{
			return objectSleepingAvatar.CreateBuilder(this);
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x0003EF14 File Offset: 0x0003D114
		public override objectSleepingAvatar.Builder CreateBuilderForType()
		{
			return new objectSleepingAvatar.Builder();
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x0003EF1C File Offset: 0x0003D11C
		public static objectSleepingAvatar.Builder CreateBuilder(objectSleepingAvatar prototype)
		{
			return new objectSleepingAvatar.Builder(prototype);
		}

		// Token: 0x04000969 RID: 2409
		public const int FootArmorFieldNumber = 1;

		// Token: 0x0400096A RID: 2410
		public const int LegArmorFieldNumber = 2;

		// Token: 0x0400096B RID: 2411
		public const int TorsoArmorFieldNumber = 3;

		// Token: 0x0400096C RID: 2412
		public const int HeadArmorFieldNumber = 4;

		// Token: 0x0400096D RID: 2413
		public const int TimestampFieldNumber = 5;

		// Token: 0x0400096E RID: 2414
		public const int VitalsFieldNumber = 6;

		// Token: 0x0400096F RID: 2415
		private static readonly objectSleepingAvatar defaultInstance = new objectSleepingAvatar().MakeReadOnly();

		// Token: 0x04000970 RID: 2416
		private static readonly string[] _objectSleepingAvatarFieldNames = new string[]
		{
			"footArmor",
			"headArmor",
			"legArmor",
			"timestamp",
			"torsoArmor",
			"vitals"
		};

		// Token: 0x04000971 RID: 2417
		private static readonly uint[] _objectSleepingAvatarFieldTags = new uint[]
		{
			8u,
			32u,
			16u,
			40u,
			24u,
			50u
		};

		// Token: 0x04000972 RID: 2418
		private bool hasFootArmor;

		// Token: 0x04000973 RID: 2419
		private int footArmor_;

		// Token: 0x04000974 RID: 2420
		private bool hasLegArmor;

		// Token: 0x04000975 RID: 2421
		private int legArmor_;

		// Token: 0x04000976 RID: 2422
		private bool hasTorsoArmor;

		// Token: 0x04000977 RID: 2423
		private int torsoArmor_;

		// Token: 0x04000978 RID: 2424
		private bool hasHeadArmor;

		// Token: 0x04000979 RID: 2425
		private int headArmor_;

		// Token: 0x0400097A RID: 2426
		private bool hasTimestamp;

		// Token: 0x0400097B RID: 2427
		private int timestamp_;

		// Token: 0x0400097C RID: 2428
		private bool hasVitals;

		// Token: 0x0400097D RID: 2429
		private Vitals vitals_;

		// Token: 0x0400097E RID: 2430
		private int memoizedSerializedSize = -1;

		// Token: 0x02000212 RID: 530
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<objectSleepingAvatar, objectSleepingAvatar.Builder>
		{
			// Token: 0x0600118A RID: 4490 RVA: 0x0003EF24 File Offset: 0x0003D124
			public Builder()
			{
				this.result = objectSleepingAvatar.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x0600118B RID: 4491 RVA: 0x0003EF40 File Offset: 0x0003D140
			internal Builder(objectSleepingAvatar cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x170004CD RID: 1229
			// (get) Token: 0x0600118C RID: 4492 RVA: 0x0003EF58 File Offset: 0x0003D158
			protected override objectSleepingAvatar.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x0600118D RID: 4493 RVA: 0x0003EF5C File Offset: 0x0003D15C
			private objectSleepingAvatar PrepareBuilder()
			{
				if (this.resultIsReadOnly)
				{
					objectSleepingAvatar other = this.result;
					this.result = new objectSleepingAvatar();
					this.resultIsReadOnly = false;
					this.MergeFrom(other);
				}
				return this.result;
			}

			// Token: 0x170004CE RID: 1230
			// (get) Token: 0x0600118E RID: 4494 RVA: 0x0003EF9C File Offset: 0x0003D19C
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x170004CF RID: 1231
			// (get) Token: 0x0600118F RID: 4495 RVA: 0x0003EFAC File Offset: 0x0003D1AC
			protected override objectSleepingAvatar MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x06001190 RID: 4496 RVA: 0x0003EFB4 File Offset: 0x0003D1B4
			public override objectSleepingAvatar.Builder Clear()
			{
				this.result = objectSleepingAvatar.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x06001191 RID: 4497 RVA: 0x0003EFCC File Offset: 0x0003D1CC
			public override objectSleepingAvatar.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new objectSleepingAvatar.Builder(this.result);
				}
				return new objectSleepingAvatar.Builder().MergeFrom(this.result);
			}

			// Token: 0x170004D0 RID: 1232
			// (get) Token: 0x06001192 RID: 4498 RVA: 0x0003EFF8 File Offset: 0x0003D1F8
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return objectSleepingAvatar.Descriptor;
				}
			}

			// Token: 0x170004D1 RID: 1233
			// (get) Token: 0x06001193 RID: 4499 RVA: 0x0003F000 File Offset: 0x0003D200
			public override objectSleepingAvatar DefaultInstanceForType
			{
				get
				{
					return objectSleepingAvatar.DefaultInstance;
				}
			}

			// Token: 0x06001194 RID: 4500 RVA: 0x0003F008 File Offset: 0x0003D208
			public override objectSleepingAvatar BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x06001195 RID: 4501 RVA: 0x0003F03C File Offset: 0x0003D23C
			public override objectSleepingAvatar.Builder MergeFrom(IMessage other)
			{
				if (other is objectSleepingAvatar)
				{
					return this.MergeFrom((objectSleepingAvatar)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x06001196 RID: 4502 RVA: 0x0003F060 File Offset: 0x0003D260
			public override objectSleepingAvatar.Builder MergeFrom(objectSleepingAvatar other)
			{
				if (other == objectSleepingAvatar.DefaultInstance)
				{
					return this;
				}
				this.PrepareBuilder();
				if (other.HasFootArmor)
				{
					this.FootArmor = other.FootArmor;
				}
				if (other.HasLegArmor)
				{
					this.LegArmor = other.LegArmor;
				}
				if (other.HasTorsoArmor)
				{
					this.TorsoArmor = other.TorsoArmor;
				}
				if (other.HasHeadArmor)
				{
					this.HeadArmor = other.HeadArmor;
				}
				if (other.HasTimestamp)
				{
					this.Timestamp = other.Timestamp;
				}
				if (other.HasVitals)
				{
					this.MergeVitals(other.Vitals);
				}
				this.MergeUnknownFields(other.UnknownFields);
				return this;
			}

			// Token: 0x06001197 RID: 4503 RVA: 0x0003F11C File Offset: 0x0003D31C
			public override objectSleepingAvatar.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x06001198 RID: 4504 RVA: 0x0003F12C File Offset: 0x0003D32C
			public override objectSleepingAvatar.Builder MergeFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
			{
				this.PrepareBuilder();
				UnknownFieldSet.Builder builder = null;
				uint num;
				string text;
				while (input.ReadTag(ref num, ref text))
				{
					if (num == 0u && text != null)
					{
						int num2 = Array.BinarySearch<string>(objectSleepingAvatar._objectSleepingAvatarFieldNames, text, StringComparer.Ordinal);
						if (num2 < 0)
						{
							if (builder == null)
							{
								builder = UnknownFieldSet.CreateBuilder(this.UnknownFields);
							}
							this.ParseUnknownField(input, builder, extensionRegistry, num, text);
							continue;
						}
						num = objectSleepingAvatar._objectSleepingAvatarFieldTags[num2];
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
								if (num3 != 32u)
								{
									if (num3 != 40u)
									{
										if (num3 != 50u)
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
											Vitals.Builder builder2 = Vitals.CreateBuilder();
											if (this.result.hasVitals)
											{
												builder2.MergeFrom(this.Vitals);
											}
											input.ReadMessage(builder2, extensionRegistry);
											this.Vitals = builder2.BuildPartial();
										}
									}
									else
									{
										this.result.hasTimestamp = input.ReadInt32(ref this.result.timestamp_);
									}
								}
								else
								{
									this.result.hasHeadArmor = input.ReadInt32(ref this.result.headArmor_);
								}
							}
							else
							{
								this.result.hasTorsoArmor = input.ReadInt32(ref this.result.torsoArmor_);
							}
						}
						else
						{
							this.result.hasLegArmor = input.ReadInt32(ref this.result.legArmor_);
						}
					}
					else
					{
						this.result.hasFootArmor = input.ReadInt32(ref this.result.footArmor_);
					}
				}
				if (builder != null)
				{
					this.UnknownFields = builder.Build();
				}
				return this;
			}

			// Token: 0x170004D2 RID: 1234
			// (get) Token: 0x06001199 RID: 4505 RVA: 0x0003F330 File Offset: 0x0003D530
			public bool HasFootArmor
			{
				get
				{
					return this.result.hasFootArmor;
				}
			}

			// Token: 0x170004D3 RID: 1235
			// (get) Token: 0x0600119A RID: 4506 RVA: 0x0003F340 File Offset: 0x0003D540
			// (set) Token: 0x0600119B RID: 4507 RVA: 0x0003F350 File Offset: 0x0003D550
			public int FootArmor
			{
				get
				{
					return this.result.FootArmor;
				}
				set
				{
					this.SetFootArmor(value);
				}
			}

			// Token: 0x0600119C RID: 4508 RVA: 0x0003F35C File Offset: 0x0003D55C
			public objectSleepingAvatar.Builder SetFootArmor(int value)
			{
				this.PrepareBuilder();
				this.result.hasFootArmor = true;
				this.result.footArmor_ = value;
				return this;
			}

			// Token: 0x0600119D RID: 4509 RVA: 0x0003F38C File Offset: 0x0003D58C
			public objectSleepingAvatar.Builder ClearFootArmor()
			{
				this.PrepareBuilder();
				this.result.hasFootArmor = false;
				this.result.footArmor_ = 0;
				return this;
			}

			// Token: 0x170004D4 RID: 1236
			// (get) Token: 0x0600119E RID: 4510 RVA: 0x0003F3BC File Offset: 0x0003D5BC
			public bool HasLegArmor
			{
				get
				{
					return this.result.hasLegArmor;
				}
			}

			// Token: 0x170004D5 RID: 1237
			// (get) Token: 0x0600119F RID: 4511 RVA: 0x0003F3CC File Offset: 0x0003D5CC
			// (set) Token: 0x060011A0 RID: 4512 RVA: 0x0003F3DC File Offset: 0x0003D5DC
			public int LegArmor
			{
				get
				{
					return this.result.LegArmor;
				}
				set
				{
					this.SetLegArmor(value);
				}
			}

			// Token: 0x060011A1 RID: 4513 RVA: 0x0003F3E8 File Offset: 0x0003D5E8
			public objectSleepingAvatar.Builder SetLegArmor(int value)
			{
				this.PrepareBuilder();
				this.result.hasLegArmor = true;
				this.result.legArmor_ = value;
				return this;
			}

			// Token: 0x060011A2 RID: 4514 RVA: 0x0003F418 File Offset: 0x0003D618
			public objectSleepingAvatar.Builder ClearLegArmor()
			{
				this.PrepareBuilder();
				this.result.hasLegArmor = false;
				this.result.legArmor_ = 0;
				return this;
			}

			// Token: 0x170004D6 RID: 1238
			// (get) Token: 0x060011A3 RID: 4515 RVA: 0x0003F448 File Offset: 0x0003D648
			public bool HasTorsoArmor
			{
				get
				{
					return this.result.hasTorsoArmor;
				}
			}

			// Token: 0x170004D7 RID: 1239
			// (get) Token: 0x060011A4 RID: 4516 RVA: 0x0003F458 File Offset: 0x0003D658
			// (set) Token: 0x060011A5 RID: 4517 RVA: 0x0003F468 File Offset: 0x0003D668
			public int TorsoArmor
			{
				get
				{
					return this.result.TorsoArmor;
				}
				set
				{
					this.SetTorsoArmor(value);
				}
			}

			// Token: 0x060011A6 RID: 4518 RVA: 0x0003F474 File Offset: 0x0003D674
			public objectSleepingAvatar.Builder SetTorsoArmor(int value)
			{
				this.PrepareBuilder();
				this.result.hasTorsoArmor = true;
				this.result.torsoArmor_ = value;
				return this;
			}

			// Token: 0x060011A7 RID: 4519 RVA: 0x0003F4A4 File Offset: 0x0003D6A4
			public objectSleepingAvatar.Builder ClearTorsoArmor()
			{
				this.PrepareBuilder();
				this.result.hasTorsoArmor = false;
				this.result.torsoArmor_ = 0;
				return this;
			}

			// Token: 0x170004D8 RID: 1240
			// (get) Token: 0x060011A8 RID: 4520 RVA: 0x0003F4D4 File Offset: 0x0003D6D4
			public bool HasHeadArmor
			{
				get
				{
					return this.result.hasHeadArmor;
				}
			}

			// Token: 0x170004D9 RID: 1241
			// (get) Token: 0x060011A9 RID: 4521 RVA: 0x0003F4E4 File Offset: 0x0003D6E4
			// (set) Token: 0x060011AA RID: 4522 RVA: 0x0003F4F4 File Offset: 0x0003D6F4
			public int HeadArmor
			{
				get
				{
					return this.result.HeadArmor;
				}
				set
				{
					this.SetHeadArmor(value);
				}
			}

			// Token: 0x060011AB RID: 4523 RVA: 0x0003F500 File Offset: 0x0003D700
			public objectSleepingAvatar.Builder SetHeadArmor(int value)
			{
				this.PrepareBuilder();
				this.result.hasHeadArmor = true;
				this.result.headArmor_ = value;
				return this;
			}

			// Token: 0x060011AC RID: 4524 RVA: 0x0003F530 File Offset: 0x0003D730
			public objectSleepingAvatar.Builder ClearHeadArmor()
			{
				this.PrepareBuilder();
				this.result.hasHeadArmor = false;
				this.result.headArmor_ = 0;
				return this;
			}

			// Token: 0x170004DA RID: 1242
			// (get) Token: 0x060011AD RID: 4525 RVA: 0x0003F560 File Offset: 0x0003D760
			public bool HasTimestamp
			{
				get
				{
					return this.result.hasTimestamp;
				}
			}

			// Token: 0x170004DB RID: 1243
			// (get) Token: 0x060011AE RID: 4526 RVA: 0x0003F570 File Offset: 0x0003D770
			// (set) Token: 0x060011AF RID: 4527 RVA: 0x0003F580 File Offset: 0x0003D780
			public int Timestamp
			{
				get
				{
					return this.result.Timestamp;
				}
				set
				{
					this.SetTimestamp(value);
				}
			}

			// Token: 0x060011B0 RID: 4528 RVA: 0x0003F58C File Offset: 0x0003D78C
			public objectSleepingAvatar.Builder SetTimestamp(int value)
			{
				this.PrepareBuilder();
				this.result.hasTimestamp = true;
				this.result.timestamp_ = value;
				return this;
			}

			// Token: 0x060011B1 RID: 4529 RVA: 0x0003F5BC File Offset: 0x0003D7BC
			public objectSleepingAvatar.Builder ClearTimestamp()
			{
				this.PrepareBuilder();
				this.result.hasTimestamp = false;
				this.result.timestamp_ = 0;
				return this;
			}

			// Token: 0x170004DC RID: 1244
			// (get) Token: 0x060011B2 RID: 4530 RVA: 0x0003F5EC File Offset: 0x0003D7EC
			public bool HasVitals
			{
				get
				{
					return this.result.hasVitals;
				}
			}

			// Token: 0x170004DD RID: 1245
			// (get) Token: 0x060011B3 RID: 4531 RVA: 0x0003F5FC File Offset: 0x0003D7FC
			// (set) Token: 0x060011B4 RID: 4532 RVA: 0x0003F60C File Offset: 0x0003D80C
			public Vitals Vitals
			{
				get
				{
					return this.result.Vitals;
				}
				set
				{
					this.SetVitals(value);
				}
			}

			// Token: 0x060011B5 RID: 4533 RVA: 0x0003F618 File Offset: 0x0003D818
			public objectSleepingAvatar.Builder SetVitals(Vitals value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasVitals = true;
				this.result.vitals_ = value;
				return this;
			}

			// Token: 0x060011B6 RID: 4534 RVA: 0x0003F648 File Offset: 0x0003D848
			public objectSleepingAvatar.Builder SetVitals(Vitals.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.hasVitals = true;
				this.result.vitals_ = builderForValue.Build();
				return this;
			}

			// Token: 0x060011B7 RID: 4535 RVA: 0x0003F688 File Offset: 0x0003D888
			public objectSleepingAvatar.Builder MergeVitals(Vitals value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				if (this.result.hasVitals && this.result.vitals_ != Vitals.DefaultInstance)
				{
					this.result.vitals_ = Vitals.CreateBuilder(this.result.vitals_).MergeFrom(value).BuildPartial();
				}
				else
				{
					this.result.vitals_ = value;
				}
				this.result.hasVitals = true;
				return this;
			}

			// Token: 0x060011B8 RID: 4536 RVA: 0x0003F710 File Offset: 0x0003D910
			public objectSleepingAvatar.Builder ClearVitals()
			{
				this.PrepareBuilder();
				this.result.hasVitals = false;
				this.result.vitals_ = null;
				return this;
			}

			// Token: 0x0400097F RID: 2431
			private bool resultIsReadOnly;

			// Token: 0x04000980 RID: 2432
			private objectSleepingAvatar result;
		}
	}
}
