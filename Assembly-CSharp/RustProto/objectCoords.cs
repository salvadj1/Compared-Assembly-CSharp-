using System;
using System.Diagnostics;
using System.IO;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;
using RustProto.Helpers;

namespace RustProto
{
	// Token: 0x0200023C RID: 572
	[DebuggerNonUserCode]
	public sealed class objectCoords : GeneratedMessage<objectCoords, objectCoords.Builder>
	{
		// Token: 0x060011B8 RID: 4536 RVA: 0x00040D30 File Offset: 0x0003EF30
		private objectCoords()
		{
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x00040D40 File Offset: 0x0003EF40
		static objectCoords()
		{
			object.ReferenceEquals(Worldsave.Descriptor, null);
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x00040DAC File Offset: 0x0003EFAC
		public static RustProto.Helpers.Recycler<objectCoords, objectCoords.Builder> Recycler()
		{
			return RustProto.Helpers.Recycler<objectCoords, objectCoords.Builder>.Manufacture();
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x060011BB RID: 4539 RVA: 0x00040DB4 File Offset: 0x0003EFB4
		public static objectCoords DefaultInstance
		{
			get
			{
				return objectCoords.defaultInstance;
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x060011BC RID: 4540 RVA: 0x00040DBC File Offset: 0x0003EFBC
		public override objectCoords DefaultInstanceForType
		{
			get
			{
				return objectCoords.DefaultInstance;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x060011BD RID: 4541 RVA: 0x00040DC4 File Offset: 0x0003EFC4
		protected override objectCoords ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060011BE RID: 4542 RVA: 0x00040DC8 File Offset: 0x0003EFC8
		public static MessageDescriptor Descriptor
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectCoords__Descriptor;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060011BF RID: 4543 RVA: 0x00040DD0 File Offset: 0x0003EFD0
		protected override FieldAccessorTable<objectCoords, objectCoords.Builder> InternalFieldAccessors
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectCoords__FieldAccessorTable;
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060011C0 RID: 4544 RVA: 0x00040DD8 File Offset: 0x0003EFD8
		public bool HasPos
		{
			get
			{
				return this.hasPos;
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x060011C1 RID: 4545 RVA: 0x00040DE0 File Offset: 0x0003EFE0
		public Vector Pos
		{
			get
			{
				return this.pos_ ?? Vector.DefaultInstance;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060011C2 RID: 4546 RVA: 0x00040DF4 File Offset: 0x0003EFF4
		public bool HasOldPos
		{
			get
			{
				return this.hasOldPos;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x060011C3 RID: 4547 RVA: 0x00040DFC File Offset: 0x0003EFFC
		public Vector OldPos
		{
			get
			{
				return this.oldPos_ ?? Vector.DefaultInstance;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x060011C4 RID: 4548 RVA: 0x00040E10 File Offset: 0x0003F010
		public bool HasRot
		{
			get
			{
				return this.hasRot;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x060011C5 RID: 4549 RVA: 0x00040E18 File Offset: 0x0003F018
		public Quaternion Rot
		{
			get
			{
				return this.rot_ ?? Quaternion.DefaultInstance;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x060011C6 RID: 4550 RVA: 0x00040E2C File Offset: 0x0003F02C
		public bool HasOldRot
		{
			get
			{
				return this.hasOldRot;
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x060011C7 RID: 4551 RVA: 0x00040E34 File Offset: 0x0003F034
		public Quaternion OldRot
		{
			get
			{
				return this.oldRot_ ?? Quaternion.DefaultInstance;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x060011C8 RID: 4552 RVA: 0x00040E48 File Offset: 0x0003F048
		public override bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x00040E4C File Offset: 0x0003F04C
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

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x060011CA RID: 4554 RVA: 0x00040EE0 File Offset: 0x0003F0E0
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

		// Token: 0x060011CB RID: 4555 RVA: 0x00040F80 File Offset: 0x0003F180
		public static objectCoords ParseFrom(ByteString data)
		{
			return objectCoords.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x00040F94 File Offset: 0x0003F194
		public static objectCoords ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return objectCoords.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x00040FA8 File Offset: 0x0003F1A8
		public static objectCoords ParseFrom(byte[] data)
		{
			return objectCoords.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x00040FBC File Offset: 0x0003F1BC
		public static objectCoords ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return objectCoords.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x00040FD0 File Offset: 0x0003F1D0
		public static objectCoords ParseFrom(Stream input)
		{
			return objectCoords.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x00040FE4 File Offset: 0x0003F1E4
		public static objectCoords ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectCoords.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x00040FF8 File Offset: 0x0003F1F8
		public static objectCoords ParseDelimitedFrom(Stream input)
		{
			return objectCoords.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x0004100C File Offset: 0x0003F20C
		public static objectCoords ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectCoords.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x00041020 File Offset: 0x0003F220
		public static objectCoords ParseFrom(ICodedInputStream input)
		{
			return objectCoords.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x00041034 File Offset: 0x0003F234
		public static objectCoords ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return objectCoords.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x00041048 File Offset: 0x0003F248
		private objectCoords MakeReadOnly()
		{
			return this;
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x0004104C File Offset: 0x0003F24C
		public static objectCoords.Builder CreateBuilder()
		{
			return new objectCoords.Builder();
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x00041054 File Offset: 0x0003F254
		public override objectCoords.Builder ToBuilder()
		{
			return objectCoords.CreateBuilder(this);
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x0004105C File Offset: 0x0003F25C
		public override objectCoords.Builder CreateBuilderForType()
		{
			return new objectCoords.Builder();
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x00041064 File Offset: 0x0003F264
		public static objectCoords.Builder CreateBuilder(objectCoords prototype)
		{
			return new objectCoords.Builder(prototype);
		}

		// Token: 0x04000A50 RID: 2640
		public const int PosFieldNumber = 1;

		// Token: 0x04000A51 RID: 2641
		public const int OldPosFieldNumber = 2;

		// Token: 0x04000A52 RID: 2642
		public const int RotFieldNumber = 3;

		// Token: 0x04000A53 RID: 2643
		public const int OldRotFieldNumber = 4;

		// Token: 0x04000A54 RID: 2644
		private static readonly objectCoords defaultInstance = new objectCoords().MakeReadOnly();

		// Token: 0x04000A55 RID: 2645
		private static readonly string[] _objectCoordsFieldNames = new string[]
		{
			"oldPos",
			"oldRot",
			"pos",
			"rot"
		};

		// Token: 0x04000A56 RID: 2646
		private static readonly uint[] _objectCoordsFieldTags = new uint[]
		{
			18u,
			34u,
			10u,
			26u
		};

		// Token: 0x04000A57 RID: 2647
		private bool hasPos;

		// Token: 0x04000A58 RID: 2648
		private Vector pos_;

		// Token: 0x04000A59 RID: 2649
		private bool hasOldPos;

		// Token: 0x04000A5A RID: 2650
		private Vector oldPos_;

		// Token: 0x04000A5B RID: 2651
		private bool hasRot;

		// Token: 0x04000A5C RID: 2652
		private Quaternion rot_;

		// Token: 0x04000A5D RID: 2653
		private bool hasOldRot;

		// Token: 0x04000A5E RID: 2654
		private Quaternion oldRot_;

		// Token: 0x04000A5F RID: 2655
		private int memoizedSerializedSize = -1;

		// Token: 0x0200023D RID: 573
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<objectCoords, objectCoords.Builder>
		{
			// Token: 0x060011DA RID: 4570 RVA: 0x0004106C File Offset: 0x0003F26C
			public Builder()
			{
				this.result = objectCoords.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x060011DB RID: 4571 RVA: 0x00041088 File Offset: 0x0003F288
			internal Builder(objectCoords cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x170004B1 RID: 1201
			// (get) Token: 0x060011DC RID: 4572 RVA: 0x000410A0 File Offset: 0x0003F2A0
			protected override objectCoords.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x060011DD RID: 4573 RVA: 0x000410A4 File Offset: 0x0003F2A4
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

			// Token: 0x170004B2 RID: 1202
			// (get) Token: 0x060011DE RID: 4574 RVA: 0x000410E4 File Offset: 0x0003F2E4
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x170004B3 RID: 1203
			// (get) Token: 0x060011DF RID: 4575 RVA: 0x000410F4 File Offset: 0x0003F2F4
			protected override objectCoords MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x060011E0 RID: 4576 RVA: 0x000410FC File Offset: 0x0003F2FC
			public override objectCoords.Builder Clear()
			{
				this.result = objectCoords.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x060011E1 RID: 4577 RVA: 0x00041114 File Offset: 0x0003F314
			public override objectCoords.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new objectCoords.Builder(this.result);
				}
				return new objectCoords.Builder().MergeFrom(this.result);
			}

			// Token: 0x170004B4 RID: 1204
			// (get) Token: 0x060011E2 RID: 4578 RVA: 0x00041140 File Offset: 0x0003F340
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return objectCoords.Descriptor;
				}
			}

			// Token: 0x170004B5 RID: 1205
			// (get) Token: 0x060011E3 RID: 4579 RVA: 0x00041148 File Offset: 0x0003F348
			public override objectCoords DefaultInstanceForType
			{
				get
				{
					return objectCoords.DefaultInstance;
				}
			}

			// Token: 0x060011E4 RID: 4580 RVA: 0x00041150 File Offset: 0x0003F350
			public override objectCoords BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x060011E5 RID: 4581 RVA: 0x00041184 File Offset: 0x0003F384
			public override objectCoords.Builder MergeFrom(IMessage other)
			{
				if (other is objectCoords)
				{
					return this.MergeFrom((objectCoords)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x060011E6 RID: 4582 RVA: 0x000411A8 File Offset: 0x0003F3A8
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

			// Token: 0x060011E7 RID: 4583 RVA: 0x00041238 File Offset: 0x0003F438
			public override objectCoords.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x060011E8 RID: 4584 RVA: 0x00041248 File Offset: 0x0003F448
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

			// Token: 0x170004B6 RID: 1206
			// (get) Token: 0x060011E9 RID: 4585 RVA: 0x00041458 File Offset: 0x0003F658
			public bool HasPos
			{
				get
				{
					return this.result.hasPos;
				}
			}

			// Token: 0x170004B7 RID: 1207
			// (get) Token: 0x060011EA RID: 4586 RVA: 0x00041468 File Offset: 0x0003F668
			// (set) Token: 0x060011EB RID: 4587 RVA: 0x00041478 File Offset: 0x0003F678
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

			// Token: 0x060011EC RID: 4588 RVA: 0x00041484 File Offset: 0x0003F684
			public objectCoords.Builder SetPos(Vector value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasPos = true;
				this.result.pos_ = value;
				return this;
			}

			// Token: 0x060011ED RID: 4589 RVA: 0x000414B4 File Offset: 0x0003F6B4
			public objectCoords.Builder SetPos(Vector.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.hasPos = true;
				this.result.pos_ = builderForValue.Build();
				return this;
			}

			// Token: 0x060011EE RID: 4590 RVA: 0x000414F4 File Offset: 0x0003F6F4
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

			// Token: 0x060011EF RID: 4591 RVA: 0x0004157C File Offset: 0x0003F77C
			public objectCoords.Builder ClearPos()
			{
				this.PrepareBuilder();
				this.result.hasPos = false;
				this.result.pos_ = null;
				return this;
			}

			// Token: 0x170004B8 RID: 1208
			// (get) Token: 0x060011F0 RID: 4592 RVA: 0x000415AC File Offset: 0x0003F7AC
			public bool HasOldPos
			{
				get
				{
					return this.result.hasOldPos;
				}
			}

			// Token: 0x170004B9 RID: 1209
			// (get) Token: 0x060011F1 RID: 4593 RVA: 0x000415BC File Offset: 0x0003F7BC
			// (set) Token: 0x060011F2 RID: 4594 RVA: 0x000415CC File Offset: 0x0003F7CC
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

			// Token: 0x060011F3 RID: 4595 RVA: 0x000415D8 File Offset: 0x0003F7D8
			public objectCoords.Builder SetOldPos(Vector value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasOldPos = true;
				this.result.oldPos_ = value;
				return this;
			}

			// Token: 0x060011F4 RID: 4596 RVA: 0x00041608 File Offset: 0x0003F808
			public objectCoords.Builder SetOldPos(Vector.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.hasOldPos = true;
				this.result.oldPos_ = builderForValue.Build();
				return this;
			}

			// Token: 0x060011F5 RID: 4597 RVA: 0x00041648 File Offset: 0x0003F848
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

			// Token: 0x060011F6 RID: 4598 RVA: 0x000416D0 File Offset: 0x0003F8D0
			public objectCoords.Builder ClearOldPos()
			{
				this.PrepareBuilder();
				this.result.hasOldPos = false;
				this.result.oldPos_ = null;
				return this;
			}

			// Token: 0x170004BA RID: 1210
			// (get) Token: 0x060011F7 RID: 4599 RVA: 0x00041700 File Offset: 0x0003F900
			public bool HasRot
			{
				get
				{
					return this.result.hasRot;
				}
			}

			// Token: 0x170004BB RID: 1211
			// (get) Token: 0x060011F8 RID: 4600 RVA: 0x00041710 File Offset: 0x0003F910
			// (set) Token: 0x060011F9 RID: 4601 RVA: 0x00041720 File Offset: 0x0003F920
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

			// Token: 0x060011FA RID: 4602 RVA: 0x0004172C File Offset: 0x0003F92C
			public objectCoords.Builder SetRot(Quaternion value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasRot = true;
				this.result.rot_ = value;
				return this;
			}

			// Token: 0x060011FB RID: 4603 RVA: 0x0004175C File Offset: 0x0003F95C
			public objectCoords.Builder SetRot(Quaternion.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.hasRot = true;
				this.result.rot_ = builderForValue.Build();
				return this;
			}

			// Token: 0x060011FC RID: 4604 RVA: 0x0004179C File Offset: 0x0003F99C
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

			// Token: 0x060011FD RID: 4605 RVA: 0x00041824 File Offset: 0x0003FA24
			public objectCoords.Builder ClearRot()
			{
				this.PrepareBuilder();
				this.result.hasRot = false;
				this.result.rot_ = null;
				return this;
			}

			// Token: 0x170004BC RID: 1212
			// (get) Token: 0x060011FE RID: 4606 RVA: 0x00041854 File Offset: 0x0003FA54
			public bool HasOldRot
			{
				get
				{
					return this.result.hasOldRot;
				}
			}

			// Token: 0x170004BD RID: 1213
			// (get) Token: 0x060011FF RID: 4607 RVA: 0x00041864 File Offset: 0x0003FA64
			// (set) Token: 0x06001200 RID: 4608 RVA: 0x00041874 File Offset: 0x0003FA74
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

			// Token: 0x06001201 RID: 4609 RVA: 0x00041880 File Offset: 0x0003FA80
			public objectCoords.Builder SetOldRot(Quaternion value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasOldRot = true;
				this.result.oldRot_ = value;
				return this;
			}

			// Token: 0x06001202 RID: 4610 RVA: 0x000418B0 File Offset: 0x0003FAB0
			public objectCoords.Builder SetOldRot(Quaternion.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.hasOldRot = true;
				this.result.oldRot_ = builderForValue.Build();
				return this;
			}

			// Token: 0x06001203 RID: 4611 RVA: 0x000418F0 File Offset: 0x0003FAF0
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

			// Token: 0x06001204 RID: 4612 RVA: 0x00041978 File Offset: 0x0003FB78
			public objectCoords.Builder ClearOldRot()
			{
				this.PrepareBuilder();
				this.result.hasOldRot = false;
				this.result.oldRot_ = null;
				return this;
			}

			// Token: 0x04000A60 RID: 2656
			private bool resultIsReadOnly;

			// Token: 0x04000A61 RID: 2657
			private objectCoords result;
		}
	}
}
