using System;
using System.Diagnostics;
using System.IO;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;
using RustProto.Helpers;

namespace RustProto
{
	// Token: 0x02000209 RID: 521
	[DebuggerNonUserCode]
	public sealed class objectCoords : GeneratedMessage<objectCoords, objectCoords.Builder>
	{
		// Token: 0x06001064 RID: 4196 RVA: 0x0003C988 File Offset: 0x0003AB88
		private objectCoords()
		{
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x0003C998 File Offset: 0x0003AB98
		static objectCoords()
		{
			object.ReferenceEquals(Worldsave.Descriptor, null);
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x0003CA04 File Offset: 0x0003AC04
		public static Recycler<objectCoords, objectCoords.Builder> Recycler()
		{
			return Recycler<objectCoords, objectCoords.Builder>.Manufacture();
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06001067 RID: 4199 RVA: 0x0003CA0C File Offset: 0x0003AC0C
		public static objectCoords DefaultInstance
		{
			get
			{
				return objectCoords.defaultInstance;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06001068 RID: 4200 RVA: 0x0003CA14 File Offset: 0x0003AC14
		public override objectCoords DefaultInstanceForType
		{
			get
			{
				return objectCoords.DefaultInstance;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06001069 RID: 4201 RVA: 0x0003CA1C File Offset: 0x0003AC1C
		protected override objectCoords ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x0600106A RID: 4202 RVA: 0x0003CA20 File Offset: 0x0003AC20
		public static MessageDescriptor Descriptor
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectCoords__Descriptor;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x0600106B RID: 4203 RVA: 0x0003CA28 File Offset: 0x0003AC28
		protected override FieldAccessorTable<objectCoords, objectCoords.Builder> InternalFieldAccessors
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectCoords__FieldAccessorTable;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x0600106C RID: 4204 RVA: 0x0003CA30 File Offset: 0x0003AC30
		public bool HasPos
		{
			get
			{
				return this.hasPos;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x0600106D RID: 4205 RVA: 0x0003CA38 File Offset: 0x0003AC38
		public Vector Pos
		{
			get
			{
				return this.pos_ ?? Vector.DefaultInstance;
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x0600106E RID: 4206 RVA: 0x0003CA4C File Offset: 0x0003AC4C
		public bool HasOldPos
		{
			get
			{
				return this.hasOldPos;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x0600106F RID: 4207 RVA: 0x0003CA54 File Offset: 0x0003AC54
		public Vector OldPos
		{
			get
			{
				return this.oldPos_ ?? Vector.DefaultInstance;
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06001070 RID: 4208 RVA: 0x0003CA68 File Offset: 0x0003AC68
		public bool HasRot
		{
			get
			{
				return this.hasRot;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06001071 RID: 4209 RVA: 0x0003CA70 File Offset: 0x0003AC70
		public Quaternion Rot
		{
			get
			{
				return this.rot_ ?? Quaternion.DefaultInstance;
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06001072 RID: 4210 RVA: 0x0003CA84 File Offset: 0x0003AC84
		public bool HasOldRot
		{
			get
			{
				return this.hasOldRot;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06001073 RID: 4211 RVA: 0x0003CA8C File Offset: 0x0003AC8C
		public Quaternion OldRot
		{
			get
			{
				return this.oldRot_ ?? Quaternion.DefaultInstance;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06001074 RID: 4212 RVA: 0x0003CAA0 File Offset: 0x0003ACA0
		public override bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x0003CAA4 File Offset: 0x0003ACA4
		public override void WriteTo(ICodedOutputStream output)
		{
			int serializedSize = this.SerializedSize;
			string[] objectCoordsFieldNames = objectCoords._objectCoordsFieldNames;
			if (this.hasPos)
			{
				output.WriteMessage(1, objectCoordsFieldNames[2], this.Pos);
			}
			if (this.hasOldPos)
			{
				output.WriteMessage(2, objectCoordsFieldNames[0], this.OldPos);
			}
			if (this.hasRot)
			{
				output.WriteMessage(3, objectCoordsFieldNames[3], this.Rot);
			}
			if (this.hasOldRot)
			{
				output.WriteMessage(4, objectCoordsFieldNames[1], this.OldRot);
			}
			this.UnknownFields.WriteTo(output);
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x0003CB38 File Offset: 0x0003AD38
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
				if (this.hasPos)
				{
					num += CodedOutputStream.ComputeMessageSize(1, this.Pos);
				}
				if (this.hasOldPos)
				{
					num += CodedOutputStream.ComputeMessageSize(2, this.OldPos);
				}
				if (this.hasRot)
				{
					num += CodedOutputStream.ComputeMessageSize(3, this.Rot);
				}
				if (this.hasOldRot)
				{
					num += CodedOutputStream.ComputeMessageSize(4, this.OldRot);
				}
				num += this.UnknownFields.SerializedSize;
				this.memoizedSerializedSize = num;
				return num;
			}
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x0003CBD8 File Offset: 0x0003ADD8
		public static objectCoords ParseFrom(ByteString data)
		{
			return objectCoords.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x0003CBEC File Offset: 0x0003ADEC
		public static objectCoords ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return objectCoords.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x0003CC00 File Offset: 0x0003AE00
		public static objectCoords ParseFrom(byte[] data)
		{
			return objectCoords.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x0003CC14 File Offset: 0x0003AE14
		public static objectCoords ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return objectCoords.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x0003CC28 File Offset: 0x0003AE28
		public static objectCoords ParseFrom(Stream input)
		{
			return objectCoords.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x0003CC3C File Offset: 0x0003AE3C
		public static objectCoords ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectCoords.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x0003CC50 File Offset: 0x0003AE50
		public static objectCoords ParseDelimitedFrom(Stream input)
		{
			return objectCoords.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x0003CC64 File Offset: 0x0003AE64
		public static objectCoords ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectCoords.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x0003CC78 File Offset: 0x0003AE78
		public static objectCoords ParseFrom(ICodedInputStream input)
		{
			return objectCoords.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x0003CC8C File Offset: 0x0003AE8C
		public static objectCoords ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return objectCoords.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x0003CCA0 File Offset: 0x0003AEA0
		private objectCoords MakeReadOnly()
		{
			return this;
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x0003CCA4 File Offset: 0x0003AEA4
		public static objectCoords.Builder CreateBuilder()
		{
			return new objectCoords.Builder();
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x0003CCAC File Offset: 0x0003AEAC
		public override objectCoords.Builder ToBuilder()
		{
			return objectCoords.CreateBuilder(this);
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x0003CCB4 File Offset: 0x0003AEB4
		public override objectCoords.Builder CreateBuilderForType()
		{
			return new objectCoords.Builder();
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x0003CCBC File Offset: 0x0003AEBC
		public static objectCoords.Builder CreateBuilder(objectCoords prototype)
		{
			return new objectCoords.Builder(prototype);
		}

		// Token: 0x0400092D RID: 2349
		public const int PosFieldNumber = 1;

		// Token: 0x0400092E RID: 2350
		public const int OldPosFieldNumber = 2;

		// Token: 0x0400092F RID: 2351
		public const int RotFieldNumber = 3;

		// Token: 0x04000930 RID: 2352
		public const int OldRotFieldNumber = 4;

		// Token: 0x04000931 RID: 2353
		private static readonly objectCoords defaultInstance = new objectCoords().MakeReadOnly();

		// Token: 0x04000932 RID: 2354
		private static readonly string[] _objectCoordsFieldNames = new string[]
		{
			"oldPos",
			"oldRot",
			"pos",
			"rot"
		};

		// Token: 0x04000933 RID: 2355
		private static readonly uint[] _objectCoordsFieldTags = new uint[]
		{
			18u,
			34u,
			10u,
			26u
		};

		// Token: 0x04000934 RID: 2356
		private bool hasPos;

		// Token: 0x04000935 RID: 2357
		private Vector pos_;

		// Token: 0x04000936 RID: 2358
		private bool hasOldPos;

		// Token: 0x04000937 RID: 2359
		private Vector oldPos_;

		// Token: 0x04000938 RID: 2360
		private bool hasRot;

		// Token: 0x04000939 RID: 2361
		private Quaternion rot_;

		// Token: 0x0400093A RID: 2362
		private bool hasOldRot;

		// Token: 0x0400093B RID: 2363
		private Quaternion oldRot_;

		// Token: 0x0400093C RID: 2364
		private int memoizedSerializedSize = -1;

		// Token: 0x0200020A RID: 522
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<objectCoords, objectCoords.Builder>
		{
			// Token: 0x06001086 RID: 4230 RVA: 0x0003CCC4 File Offset: 0x0003AEC4
			public Builder()
			{
				this.result = objectCoords.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x06001087 RID: 4231 RVA: 0x0003CCE0 File Offset: 0x0003AEE0
			internal Builder(objectCoords cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x17000469 RID: 1129
			// (get) Token: 0x06001088 RID: 4232 RVA: 0x0003CCF8 File Offset: 0x0003AEF8
			protected override objectCoords.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x06001089 RID: 4233 RVA: 0x0003CCFC File Offset: 0x0003AEFC
			private objectCoords PrepareBuilder()
			{
				if (this.resultIsReadOnly)
				{
					objectCoords other = this.result;
					this.result = new objectCoords();
					this.resultIsReadOnly = false;
					this.MergeFrom(other);
				}
				return this.result;
			}

			// Token: 0x1700046A RID: 1130
			// (get) Token: 0x0600108A RID: 4234 RVA: 0x0003CD3C File Offset: 0x0003AF3C
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x1700046B RID: 1131
			// (get) Token: 0x0600108B RID: 4235 RVA: 0x0003CD4C File Offset: 0x0003AF4C
			protected override objectCoords MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x0600108C RID: 4236 RVA: 0x0003CD54 File Offset: 0x0003AF54
			public override objectCoords.Builder Clear()
			{
				this.result = objectCoords.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x0600108D RID: 4237 RVA: 0x0003CD6C File Offset: 0x0003AF6C
			public override objectCoords.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new objectCoords.Builder(this.result);
				}
				return new objectCoords.Builder().MergeFrom(this.result);
			}

			// Token: 0x1700046C RID: 1132
			// (get) Token: 0x0600108E RID: 4238 RVA: 0x0003CD98 File Offset: 0x0003AF98
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return objectCoords.Descriptor;
				}
			}

			// Token: 0x1700046D RID: 1133
			// (get) Token: 0x0600108F RID: 4239 RVA: 0x0003CDA0 File Offset: 0x0003AFA0
			public override objectCoords DefaultInstanceForType
			{
				get
				{
					return objectCoords.DefaultInstance;
				}
			}

			// Token: 0x06001090 RID: 4240 RVA: 0x0003CDA8 File Offset: 0x0003AFA8
			public override objectCoords BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x06001091 RID: 4241 RVA: 0x0003CDDC File Offset: 0x0003AFDC
			public override objectCoords.Builder MergeFrom(IMessage other)
			{
				if (other is objectCoords)
				{
					return this.MergeFrom((objectCoords)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x06001092 RID: 4242 RVA: 0x0003CE00 File Offset: 0x0003B000
			public override objectCoords.Builder MergeFrom(objectCoords other)
			{
				if (other == objectCoords.DefaultInstance)
				{
					return this;
				}
				this.PrepareBuilder();
				if (other.HasPos)
				{
					this.MergePos(other.Pos);
				}
				if (other.HasOldPos)
				{
					this.MergeOldPos(other.OldPos);
				}
				if (other.HasRot)
				{
					this.MergeRot(other.Rot);
				}
				if (other.HasOldRot)
				{
					this.MergeOldRot(other.OldRot);
				}
				this.MergeUnknownFields(other.UnknownFields);
				return this;
			}

			// Token: 0x06001093 RID: 4243 RVA: 0x0003CE90 File Offset: 0x0003B090
			public override objectCoords.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x06001094 RID: 4244 RVA: 0x0003CEA0 File Offset: 0x0003B0A0
			public override objectCoords.Builder MergeFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
			{
				this.PrepareBuilder();
				UnknownFieldSet.Builder builder = null;
				uint num;
				string text;
				while (input.ReadTag(ref num, ref text))
				{
					if (num == 0u && text != null)
					{
						int num2 = Array.BinarySearch<string>(objectCoords._objectCoordsFieldNames, text, StringComparer.Ordinal);
						if (num2 < 0)
						{
							if (builder == null)
							{
								builder = UnknownFieldSet.CreateBuilder(this.UnknownFields);
							}
							this.ParseUnknownField(input, builder, extensionRegistry, num, text);
							continue;
						}
						num = objectCoords._objectCoordsFieldTags[num2];
					}
					uint num3 = num;
					if (num3 == 0u)
					{
						throw InvalidProtocolBufferException.InvalidTag();
					}
					if (num3 != 10u)
					{
						if (num3 != 18u)
						{
							if (num3 != 26u)
							{
								if (num3 != 34u)
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
									Quaternion.Builder builder2 = Quaternion.CreateBuilder();
									if (this.result.hasOldRot)
									{
										builder2.MergeFrom(this.OldRot);
									}
									input.ReadMessage(builder2, extensionRegistry);
									this.OldRot = builder2.BuildPartial();
								}
							}
							else
							{
								Quaternion.Builder builder3 = Quaternion.CreateBuilder();
								if (this.result.hasRot)
								{
									builder3.MergeFrom(this.Rot);
								}
								input.ReadMessage(builder3, extensionRegistry);
								this.Rot = builder3.BuildPartial();
							}
						}
						else
						{
							Vector.Builder builder4 = Vector.CreateBuilder();
							if (this.result.hasOldPos)
							{
								builder4.MergeFrom(this.OldPos);
							}
							input.ReadMessage(builder4, extensionRegistry);
							this.OldPos = builder4.BuildPartial();
						}
					}
					else
					{
						Vector.Builder builder5 = Vector.CreateBuilder();
						if (this.result.hasPos)
						{
							builder5.MergeFrom(this.Pos);
						}
						input.ReadMessage(builder5, extensionRegistry);
						this.Pos = builder5.BuildPartial();
					}
				}
				if (builder != null)
				{
					this.UnknownFields = builder.Build();
				}
				return this;
			}

			// Token: 0x1700046E RID: 1134
			// (get) Token: 0x06001095 RID: 4245 RVA: 0x0003D0B0 File Offset: 0x0003B2B0
			public bool HasPos
			{
				get
				{
					return this.result.hasPos;
				}
			}

			// Token: 0x1700046F RID: 1135
			// (get) Token: 0x06001096 RID: 4246 RVA: 0x0003D0C0 File Offset: 0x0003B2C0
			// (set) Token: 0x06001097 RID: 4247 RVA: 0x0003D0D0 File Offset: 0x0003B2D0
			public Vector Pos
			{
				get
				{
					return this.result.Pos;
				}
				set
				{
					this.SetPos(value);
				}
			}

			// Token: 0x06001098 RID: 4248 RVA: 0x0003D0DC File Offset: 0x0003B2DC
			public objectCoords.Builder SetPos(Vector value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasPos = true;
				this.result.pos_ = value;
				return this;
			}

			// Token: 0x06001099 RID: 4249 RVA: 0x0003D10C File Offset: 0x0003B30C
			public objectCoords.Builder SetPos(Vector.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.hasPos = true;
				this.result.pos_ = builderForValue.Build();
				return this;
			}

			// Token: 0x0600109A RID: 4250 RVA: 0x0003D14C File Offset: 0x0003B34C
			public objectCoords.Builder MergePos(Vector value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				if (this.result.hasPos && this.result.pos_ != Vector.DefaultInstance)
				{
					this.result.pos_ = Vector.CreateBuilder(this.result.pos_).MergeFrom(value).BuildPartial();
				}
				else
				{
					this.result.pos_ = value;
				}
				this.result.hasPos = true;
				return this;
			}

			// Token: 0x0600109B RID: 4251 RVA: 0x0003D1D4 File Offset: 0x0003B3D4
			public objectCoords.Builder ClearPos()
			{
				this.PrepareBuilder();
				this.result.hasPos = false;
				this.result.pos_ = null;
				return this;
			}

			// Token: 0x17000470 RID: 1136
			// (get) Token: 0x0600109C RID: 4252 RVA: 0x0003D204 File Offset: 0x0003B404
			public bool HasOldPos
			{
				get
				{
					return this.result.hasOldPos;
				}
			}

			// Token: 0x17000471 RID: 1137
			// (get) Token: 0x0600109D RID: 4253 RVA: 0x0003D214 File Offset: 0x0003B414
			// (set) Token: 0x0600109E RID: 4254 RVA: 0x0003D224 File Offset: 0x0003B424
			public Vector OldPos
			{
				get
				{
					return this.result.OldPos;
				}
				set
				{
					this.SetOldPos(value);
				}
			}

			// Token: 0x0600109F RID: 4255 RVA: 0x0003D230 File Offset: 0x0003B430
			public objectCoords.Builder SetOldPos(Vector value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasOldPos = true;
				this.result.oldPos_ = value;
				return this;
			}

			// Token: 0x060010A0 RID: 4256 RVA: 0x0003D260 File Offset: 0x0003B460
			public objectCoords.Builder SetOldPos(Vector.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.hasOldPos = true;
				this.result.oldPos_ = builderForValue.Build();
				return this;
			}

			// Token: 0x060010A1 RID: 4257 RVA: 0x0003D2A0 File Offset: 0x0003B4A0
			public objectCoords.Builder MergeOldPos(Vector value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				if (this.result.hasOldPos && this.result.oldPos_ != Vector.DefaultInstance)
				{
					this.result.oldPos_ = Vector.CreateBuilder(this.result.oldPos_).MergeFrom(value).BuildPartial();
				}
				else
				{
					this.result.oldPos_ = value;
				}
				this.result.hasOldPos = true;
				return this;
			}

			// Token: 0x060010A2 RID: 4258 RVA: 0x0003D328 File Offset: 0x0003B528
			public objectCoords.Builder ClearOldPos()
			{
				this.PrepareBuilder();
				this.result.hasOldPos = false;
				this.result.oldPos_ = null;
				return this;
			}

			// Token: 0x17000472 RID: 1138
			// (get) Token: 0x060010A3 RID: 4259 RVA: 0x0003D358 File Offset: 0x0003B558
			public bool HasRot
			{
				get
				{
					return this.result.hasRot;
				}
			}

			// Token: 0x17000473 RID: 1139
			// (get) Token: 0x060010A4 RID: 4260 RVA: 0x0003D368 File Offset: 0x0003B568
			// (set) Token: 0x060010A5 RID: 4261 RVA: 0x0003D378 File Offset: 0x0003B578
			public Quaternion Rot
			{
				get
				{
					return this.result.Rot;
				}
				set
				{
					this.SetRot(value);
				}
			}

			// Token: 0x060010A6 RID: 4262 RVA: 0x0003D384 File Offset: 0x0003B584
			public objectCoords.Builder SetRot(Quaternion value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasRot = true;
				this.result.rot_ = value;
				return this;
			}

			// Token: 0x060010A7 RID: 4263 RVA: 0x0003D3B4 File Offset: 0x0003B5B4
			public objectCoords.Builder SetRot(Quaternion.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.hasRot = true;
				this.result.rot_ = builderForValue.Build();
				return this;
			}

			// Token: 0x060010A8 RID: 4264 RVA: 0x0003D3F4 File Offset: 0x0003B5F4
			public objectCoords.Builder MergeRot(Quaternion value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				if (this.result.hasRot && this.result.rot_ != Quaternion.DefaultInstance)
				{
					this.result.rot_ = Quaternion.CreateBuilder(this.result.rot_).MergeFrom(value).BuildPartial();
				}
				else
				{
					this.result.rot_ = value;
				}
				this.result.hasRot = true;
				return this;
			}

			// Token: 0x060010A9 RID: 4265 RVA: 0x0003D47C File Offset: 0x0003B67C
			public objectCoords.Builder ClearRot()
			{
				this.PrepareBuilder();
				this.result.hasRot = false;
				this.result.rot_ = null;
				return this;
			}

			// Token: 0x17000474 RID: 1140
			// (get) Token: 0x060010AA RID: 4266 RVA: 0x0003D4AC File Offset: 0x0003B6AC
			public bool HasOldRot
			{
				get
				{
					return this.result.hasOldRot;
				}
			}

			// Token: 0x17000475 RID: 1141
			// (get) Token: 0x060010AB RID: 4267 RVA: 0x0003D4BC File Offset: 0x0003B6BC
			// (set) Token: 0x060010AC RID: 4268 RVA: 0x0003D4CC File Offset: 0x0003B6CC
			public Quaternion OldRot
			{
				get
				{
					return this.result.OldRot;
				}
				set
				{
					this.SetOldRot(value);
				}
			}

			// Token: 0x060010AD RID: 4269 RVA: 0x0003D4D8 File Offset: 0x0003B6D8
			public objectCoords.Builder SetOldRot(Quaternion value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasOldRot = true;
				this.result.oldRot_ = value;
				return this;
			}

			// Token: 0x060010AE RID: 4270 RVA: 0x0003D508 File Offset: 0x0003B708
			public objectCoords.Builder SetOldRot(Quaternion.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.hasOldRot = true;
				this.result.oldRot_ = builderForValue.Build();
				return this;
			}

			// Token: 0x060010AF RID: 4271 RVA: 0x0003D548 File Offset: 0x0003B748
			public objectCoords.Builder MergeOldRot(Quaternion value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				if (this.result.hasOldRot && this.result.oldRot_ != Quaternion.DefaultInstance)
				{
					this.result.oldRot_ = Quaternion.CreateBuilder(this.result.oldRot_).MergeFrom(value).BuildPartial();
				}
				else
				{
					this.result.oldRot_ = value;
				}
				this.result.hasOldRot = true;
				return this;
			}

			// Token: 0x060010B0 RID: 4272 RVA: 0x0003D5D0 File Offset: 0x0003B7D0
			public objectCoords.Builder ClearOldRot()
			{
				this.PrepareBuilder();
				this.result.hasOldRot = false;
				this.result.oldRot_ = null;
				return this;
			}

			// Token: 0x0400093D RID: 2365
			private bool resultIsReadOnly;

			// Token: 0x0400093E RID: 2366
			private objectCoords result;
		}
	}
}
