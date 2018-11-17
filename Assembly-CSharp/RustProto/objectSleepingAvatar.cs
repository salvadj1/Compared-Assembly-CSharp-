using System;
using System.Diagnostics;
using System.IO;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;
using RustProto.Helpers;

namespace RustProto
{
	// Token: 0x02000244 RID: 580
	[DebuggerNonUserCode]
	public sealed class objectSleepingAvatar : GeneratedMessage<objectSleepingAvatar, objectSleepingAvatar.Builder>
	{
		// Token: 0x060012B8 RID: 4792 RVA: 0x00042F1C File Offset: 0x0004111C
		private objectSleepingAvatar()
		{
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x00042F2C File Offset: 0x0004112C
		static objectSleepingAvatar()
		{
			object.ReferenceEquals(Worldsave.Descriptor, null);
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x00042FA8 File Offset: 0x000411A8
		public static RustProto.Helpers.Recycler<objectSleepingAvatar, objectSleepingAvatar.Builder> Recycler()
		{
			return RustProto.Helpers.Recycler<objectSleepingAvatar, objectSleepingAvatar.Builder>.Manufacture();
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x060012BB RID: 4795 RVA: 0x00042FB0 File Offset: 0x000411B0
		public static objectSleepingAvatar DefaultInstance
		{
			get
			{
				return objectSleepingAvatar.defaultInstance;
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x060012BC RID: 4796 RVA: 0x00042FB8 File Offset: 0x000411B8
		public override objectSleepingAvatar DefaultInstanceForType
		{
			get
			{
				return objectSleepingAvatar.DefaultInstance;
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x060012BD RID: 4797 RVA: 0x00042FC0 File Offset: 0x000411C0
		protected override objectSleepingAvatar ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x060012BE RID: 4798 RVA: 0x00042FC4 File Offset: 0x000411C4
		public static MessageDescriptor Descriptor
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectSleepingAvatar__Descriptor;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x060012BF RID: 4799 RVA: 0x00042FCC File Offset: 0x000411CC
		protected override FieldAccessorTable<objectSleepingAvatar, objectSleepingAvatar.Builder> InternalFieldAccessors
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectSleepingAvatar__FieldAccessorTable;
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x060012C0 RID: 4800 RVA: 0x00042FD4 File Offset: 0x000411D4
		public bool HasFootArmor
		{
			get
			{
				return this.hasFootArmor;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x060012C1 RID: 4801 RVA: 0x00042FDC File Offset: 0x000411DC
		public int FootArmor
		{
			get
			{
				return this.footArmor_;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x060012C2 RID: 4802 RVA: 0x00042FE4 File Offset: 0x000411E4
		public bool HasLegArmor
		{
			get
			{
				return this.hasLegArmor;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x060012C3 RID: 4803 RVA: 0x00042FEC File Offset: 0x000411EC
		public int LegArmor
		{
			get
			{
				return this.legArmor_;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x060012C4 RID: 4804 RVA: 0x00042FF4 File Offset: 0x000411F4
		public bool HasTorsoArmor
		{
			get
			{
				return this.hasTorsoArmor;
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x060012C5 RID: 4805 RVA: 0x00042FFC File Offset: 0x000411FC
		public int TorsoArmor
		{
			get
			{
				return this.torsoArmor_;
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x060012C6 RID: 4806 RVA: 0x00043004 File Offset: 0x00041204
		public bool HasHeadArmor
		{
			get
			{
				return this.hasHeadArmor;
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x060012C7 RID: 4807 RVA: 0x0004300C File Offset: 0x0004120C
		public int HeadArmor
		{
			get
			{
				return this.headArmor_;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x060012C8 RID: 4808 RVA: 0x00043014 File Offset: 0x00041214
		public bool HasTimestamp
		{
			get
			{
				return this.hasTimestamp;
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x060012C9 RID: 4809 RVA: 0x0004301C File Offset: 0x0004121C
		public int Timestamp
		{
			get
			{
				return this.timestamp_;
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x060012CA RID: 4810 RVA: 0x00043024 File Offset: 0x00041224
		public bool HasVitals
		{
			get
			{
				return this.hasVitals;
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x060012CB RID: 4811 RVA: 0x0004302C File Offset: 0x0004122C
		public Vitals Vitals
		{
			get
			{
				return this.vitals_ ?? Vitals.DefaultInstance;
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x060012CC RID: 4812 RVA: 0x00043040 File Offset: 0x00041240
		public override bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x00043044 File Offset: 0x00041244
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

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x060012CE RID: 4814 RVA: 0x0004310C File Offset: 0x0004130C
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

		// Token: 0x060012CF RID: 4815 RVA: 0x000431E0 File Offset: 0x000413E0
		public static objectSleepingAvatar ParseFrom(ByteString data)
		{
			return objectSleepingAvatar.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x000431F4 File Offset: 0x000413F4
		public static objectSleepingAvatar ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return objectSleepingAvatar.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x00043208 File Offset: 0x00041408
		public static objectSleepingAvatar ParseFrom(byte[] data)
		{
			return objectSleepingAvatar.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x0004321C File Offset: 0x0004141C
		public static objectSleepingAvatar ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return objectSleepingAvatar.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x00043230 File Offset: 0x00041430
		public static objectSleepingAvatar ParseFrom(Stream input)
		{
			return objectSleepingAvatar.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x00043244 File Offset: 0x00041444
		public static objectSleepingAvatar ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectSleepingAvatar.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x00043258 File Offset: 0x00041458
		public static objectSleepingAvatar ParseDelimitedFrom(Stream input)
		{
			return objectSleepingAvatar.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x0004326C File Offset: 0x0004146C
		public static objectSleepingAvatar ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectSleepingAvatar.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x00043280 File Offset: 0x00041480
		public static objectSleepingAvatar ParseFrom(ICodedInputStream input)
		{
			return objectSleepingAvatar.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x00043294 File Offset: 0x00041494
		public static objectSleepingAvatar ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return objectSleepingAvatar.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x000432A8 File Offset: 0x000414A8
		private objectSleepingAvatar MakeReadOnly()
		{
			return this;
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x000432AC File Offset: 0x000414AC
		public static objectSleepingAvatar.Builder CreateBuilder()
		{
			return new objectSleepingAvatar.Builder();
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x000432B4 File Offset: 0x000414B4
		public override objectSleepingAvatar.Builder ToBuilder()
		{
			return objectSleepingAvatar.CreateBuilder(this);
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x000432BC File Offset: 0x000414BC
		public override objectSleepingAvatar.Builder CreateBuilderForType()
		{
			return new objectSleepingAvatar.Builder();
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x000432C4 File Offset: 0x000414C4
		public static objectSleepingAvatar.Builder CreateBuilder(objectSleepingAvatar prototype)
		{
			return new objectSleepingAvatar.Builder(prototype);
		}

		// Token: 0x04000A8C RID: 2700
		public const int FootArmorFieldNumber = 1;

		// Token: 0x04000A8D RID: 2701
		public const int LegArmorFieldNumber = 2;

		// Token: 0x04000A8E RID: 2702
		public const int TorsoArmorFieldNumber = 3;

		// Token: 0x04000A8F RID: 2703
		public const int HeadArmorFieldNumber = 4;

		// Token: 0x04000A90 RID: 2704
		public const int TimestampFieldNumber = 5;

		// Token: 0x04000A91 RID: 2705
		public const int VitalsFieldNumber = 6;

		// Token: 0x04000A92 RID: 2706
		private static readonly objectSleepingAvatar defaultInstance = new objectSleepingAvatar().MakeReadOnly();

		// Token: 0x04000A93 RID: 2707
		private static readonly string[] _objectSleepingAvatarFieldNames = new string[]
		{
			"footArmor",
			"headArmor",
			"legArmor",
			"timestamp",
			"torsoArmor",
			"vitals"
		};

		// Token: 0x04000A94 RID: 2708
		private static readonly uint[] _objectSleepingAvatarFieldTags = new uint[]
		{
			8u,
			32u,
			16u,
			40u,
			24u,
			50u
		};

		// Token: 0x04000A95 RID: 2709
		private bool hasFootArmor;

		// Token: 0x04000A96 RID: 2710
		private int footArmor_;

		// Token: 0x04000A97 RID: 2711
		private bool hasLegArmor;

		// Token: 0x04000A98 RID: 2712
		private int legArmor_;

		// Token: 0x04000A99 RID: 2713
		private bool hasTorsoArmor;

		// Token: 0x04000A9A RID: 2714
		private int torsoArmor_;

		// Token: 0x04000A9B RID: 2715
		private bool hasHeadArmor;

		// Token: 0x04000A9C RID: 2716
		private int headArmor_;

		// Token: 0x04000A9D RID: 2717
		private bool hasTimestamp;

		// Token: 0x04000A9E RID: 2718
		private int timestamp_;

		// Token: 0x04000A9F RID: 2719
		private bool hasVitals;

		// Token: 0x04000AA0 RID: 2720
		private Vitals vitals_;

		// Token: 0x04000AA1 RID: 2721
		private int memoizedSerializedSize = -1;

		// Token: 0x02000245 RID: 581
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<objectSleepingAvatar, objectSleepingAvatar.Builder>
		{
			// Token: 0x060012DE RID: 4830 RVA: 0x000432CC File Offset: 0x000414CC
			public Builder()
			{
				this.result = objectSleepingAvatar.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x060012DF RID: 4831 RVA: 0x000432E8 File Offset: 0x000414E8
			internal Builder(objectSleepingAvatar cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x17000515 RID: 1301
			// (get) Token: 0x060012E0 RID: 4832 RVA: 0x00043300 File Offset: 0x00041500
			protected override objectSleepingAvatar.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x060012E1 RID: 4833 RVA: 0x00043304 File Offset: 0x00041504
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

			// Token: 0x17000516 RID: 1302
			// (get) Token: 0x060012E2 RID: 4834 RVA: 0x00043344 File Offset: 0x00041544
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x17000517 RID: 1303
			// (get) Token: 0x060012E3 RID: 4835 RVA: 0x00043354 File Offset: 0x00041554
			protected override objectSleepingAvatar MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x060012E4 RID: 4836 RVA: 0x0004335C File Offset: 0x0004155C
			public override objectSleepingAvatar.Builder Clear()
			{
				this.result = objectSleepingAvatar.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x060012E5 RID: 4837 RVA: 0x00043374 File Offset: 0x00041574
			public override objectSleepingAvatar.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new objectSleepingAvatar.Builder(this.result);
				}
				return new objectSleepingAvatar.Builder().MergeFrom(this.result);
			}

			// Token: 0x17000518 RID: 1304
			// (get) Token: 0x060012E6 RID: 4838 RVA: 0x000433A0 File Offset: 0x000415A0
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return objectSleepingAvatar.Descriptor;
				}
			}

			// Token: 0x17000519 RID: 1305
			// (get) Token: 0x060012E7 RID: 4839 RVA: 0x000433A8 File Offset: 0x000415A8
			public override objectSleepingAvatar DefaultInstanceForType
			{
				get
				{
					return objectSleepingAvatar.DefaultInstance;
				}
			}

			// Token: 0x060012E8 RID: 4840 RVA: 0x000433B0 File Offset: 0x000415B0
			public override objectSleepingAvatar BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x060012E9 RID: 4841 RVA: 0x000433E4 File Offset: 0x000415E4
			public override objectSleepingAvatar.Builder MergeFrom(IMessage other)
			{
				if (other is objectSleepingAvatar)
				{
					return this.MergeFrom((objectSleepingAvatar)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x060012EA RID: 4842 RVA: 0x00043408 File Offset: 0x00041608
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

			// Token: 0x060012EB RID: 4843 RVA: 0x000434C4 File Offset: 0x000416C4
			public override objectSleepingAvatar.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x060012EC RID: 4844 RVA: 0x000434D4 File Offset: 0x000416D4
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

			// Token: 0x1700051A RID: 1306
			// (get) Token: 0x060012ED RID: 4845 RVA: 0x000436D8 File Offset: 0x000418D8
			public bool HasFootArmor
			{
				get
				{
					return this.result.hasFootArmor;
				}
			}

			// Token: 0x1700051B RID: 1307
			// (get) Token: 0x060012EE RID: 4846 RVA: 0x000436E8 File Offset: 0x000418E8
			// (set) Token: 0x060012EF RID: 4847 RVA: 0x000436F8 File Offset: 0x000418F8
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

			// Token: 0x060012F0 RID: 4848 RVA: 0x00043704 File Offset: 0x00041904
			public objectSleepingAvatar.Builder SetFootArmor(int value)
			{
				this.PrepareBuilder();
				this.result.hasFootArmor = true;
				this.result.footArmor_ = value;
				return this;
			}

			// Token: 0x060012F1 RID: 4849 RVA: 0x00043734 File Offset: 0x00041934
			public objectSleepingAvatar.Builder ClearFootArmor()
			{
				this.PrepareBuilder();
				this.result.hasFootArmor = false;
				this.result.footArmor_ = 0;
				return this;
			}

			// Token: 0x1700051C RID: 1308
			// (get) Token: 0x060012F2 RID: 4850 RVA: 0x00043764 File Offset: 0x00041964
			public bool HasLegArmor
			{
				get
				{
					return this.result.hasLegArmor;
				}
			}

			// Token: 0x1700051D RID: 1309
			// (get) Token: 0x060012F3 RID: 4851 RVA: 0x00043774 File Offset: 0x00041974
			// (set) Token: 0x060012F4 RID: 4852 RVA: 0x00043784 File Offset: 0x00041984
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

			// Token: 0x060012F5 RID: 4853 RVA: 0x00043790 File Offset: 0x00041990
			public objectSleepingAvatar.Builder SetLegArmor(int value)
			{
				this.PrepareBuilder();
				this.result.hasLegArmor = true;
				this.result.legArmor_ = value;
				return this;
			}

			// Token: 0x060012F6 RID: 4854 RVA: 0x000437C0 File Offset: 0x000419C0
			public objectSleepingAvatar.Builder ClearLegArmor()
			{
				this.PrepareBuilder();
				this.result.hasLegArmor = false;
				this.result.legArmor_ = 0;
				return this;
			}

			// Token: 0x1700051E RID: 1310
			// (get) Token: 0x060012F7 RID: 4855 RVA: 0x000437F0 File Offset: 0x000419F0
			public bool HasTorsoArmor
			{
				get
				{
					return this.result.hasTorsoArmor;
				}
			}

			// Token: 0x1700051F RID: 1311
			// (get) Token: 0x060012F8 RID: 4856 RVA: 0x00043800 File Offset: 0x00041A00
			// (set) Token: 0x060012F9 RID: 4857 RVA: 0x00043810 File Offset: 0x00041A10
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

			// Token: 0x060012FA RID: 4858 RVA: 0x0004381C File Offset: 0x00041A1C
			public objectSleepingAvatar.Builder SetTorsoArmor(int value)
			{
				this.PrepareBuilder();
				this.result.hasTorsoArmor = true;
				this.result.torsoArmor_ = value;
				return this;
			}

			// Token: 0x060012FB RID: 4859 RVA: 0x0004384C File Offset: 0x00041A4C
			public objectSleepingAvatar.Builder ClearTorsoArmor()
			{
				this.PrepareBuilder();
				this.result.hasTorsoArmor = false;
				this.result.torsoArmor_ = 0;
				return this;
			}

			// Token: 0x17000520 RID: 1312
			// (get) Token: 0x060012FC RID: 4860 RVA: 0x0004387C File Offset: 0x00041A7C
			public bool HasHeadArmor
			{
				get
				{
					return this.result.hasHeadArmor;
				}
			}

			// Token: 0x17000521 RID: 1313
			// (get) Token: 0x060012FD RID: 4861 RVA: 0x0004388C File Offset: 0x00041A8C
			// (set) Token: 0x060012FE RID: 4862 RVA: 0x0004389C File Offset: 0x00041A9C
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

			// Token: 0x060012FF RID: 4863 RVA: 0x000438A8 File Offset: 0x00041AA8
			public objectSleepingAvatar.Builder SetHeadArmor(int value)
			{
				this.PrepareBuilder();
				this.result.hasHeadArmor = true;
				this.result.headArmor_ = value;
				return this;
			}

			// Token: 0x06001300 RID: 4864 RVA: 0x000438D8 File Offset: 0x00041AD8
			public objectSleepingAvatar.Builder ClearHeadArmor()
			{
				this.PrepareBuilder();
				this.result.hasHeadArmor = false;
				this.result.headArmor_ = 0;
				return this;
			}

			// Token: 0x17000522 RID: 1314
			// (get) Token: 0x06001301 RID: 4865 RVA: 0x00043908 File Offset: 0x00041B08
			public bool HasTimestamp
			{
				get
				{
					return this.result.hasTimestamp;
				}
			}

			// Token: 0x17000523 RID: 1315
			// (get) Token: 0x06001302 RID: 4866 RVA: 0x00043918 File Offset: 0x00041B18
			// (set) Token: 0x06001303 RID: 4867 RVA: 0x00043928 File Offset: 0x00041B28
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

			// Token: 0x06001304 RID: 4868 RVA: 0x00043934 File Offset: 0x00041B34
			public objectSleepingAvatar.Builder SetTimestamp(int value)
			{
				this.PrepareBuilder();
				this.result.hasTimestamp = true;
				this.result.timestamp_ = value;
				return this;
			}

			// Token: 0x06001305 RID: 4869 RVA: 0x00043964 File Offset: 0x00041B64
			public objectSleepingAvatar.Builder ClearTimestamp()
			{
				this.PrepareBuilder();
				this.result.hasTimestamp = false;
				this.result.timestamp_ = 0;
				return this;
			}

			// Token: 0x17000524 RID: 1316
			// (get) Token: 0x06001306 RID: 4870 RVA: 0x00043994 File Offset: 0x00041B94
			public bool HasVitals
			{
				get
				{
					return this.result.hasVitals;
				}
			}

			// Token: 0x17000525 RID: 1317
			// (get) Token: 0x06001307 RID: 4871 RVA: 0x000439A4 File Offset: 0x00041BA4
			// (set) Token: 0x06001308 RID: 4872 RVA: 0x000439B4 File Offset: 0x00041BB4
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

			// Token: 0x06001309 RID: 4873 RVA: 0x000439C0 File Offset: 0x00041BC0
			public objectSleepingAvatar.Builder SetVitals(Vitals value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasVitals = true;
				this.result.vitals_ = value;
				return this;
			}

			// Token: 0x0600130A RID: 4874 RVA: 0x000439F0 File Offset: 0x00041BF0
			public objectSleepingAvatar.Builder SetVitals(Vitals.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.hasVitals = true;
				this.result.vitals_ = builderForValue.Build();
				return this;
			}

			// Token: 0x0600130B RID: 4875 RVA: 0x00043A30 File Offset: 0x00041C30
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

			// Token: 0x0600130C RID: 4876 RVA: 0x00043AB8 File Offset: 0x00041CB8
			public objectSleepingAvatar.Builder ClearVitals()
			{
				this.PrepareBuilder();
				this.result.hasVitals = false;
				this.result.vitals_ = null;
				return this;
			}

			// Token: 0x04000AA2 RID: 2722
			private bool resultIsReadOnly;

			// Token: 0x04000AA3 RID: 2723
			private objectSleepingAvatar result;
		}
	}
}
