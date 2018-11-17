using System;
using System.Diagnostics;
using System.IO;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;
using RustProto.Helpers;

namespace RustProto
{
	// Token: 0x02000213 RID: 531
	[DebuggerNonUserCode]
	public sealed class objectDoor : GeneratedMessage<objectDoor, objectDoor.Builder>
	{
		// Token: 0x060011B9 RID: 4537 RVA: 0x0003F740 File Offset: 0x0003D940
		private objectDoor()
		{
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x0003F750 File Offset: 0x0003D950
		static objectDoor()
		{
			object.ReferenceEquals(Worldsave.Descriptor, null);
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x0003F7A8 File Offset: 0x0003D9A8
		public static Recycler<objectDoor, objectDoor.Builder> Recycler()
		{
			return Recycler<objectDoor, objectDoor.Builder>.Manufacture();
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x060011BC RID: 4540 RVA: 0x0003F7B0 File Offset: 0x0003D9B0
		public static objectDoor DefaultInstance
		{
			get
			{
				return objectDoor.defaultInstance;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x060011BD RID: 4541 RVA: 0x0003F7B8 File Offset: 0x0003D9B8
		public override objectDoor DefaultInstanceForType
		{
			get
			{
				return objectDoor.DefaultInstance;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x060011BE RID: 4542 RVA: 0x0003F7C0 File Offset: 0x0003D9C0
		protected override objectDoor ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x060011BF RID: 4543 RVA: 0x0003F7C4 File Offset: 0x0003D9C4
		public static MessageDescriptor Descriptor
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectDoor__Descriptor;
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x060011C0 RID: 4544 RVA: 0x0003F7CC File Offset: 0x0003D9CC
		protected override FieldAccessorTable<objectDoor, objectDoor.Builder> InternalFieldAccessors
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectDoor__FieldAccessorTable;
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x060011C1 RID: 4545 RVA: 0x0003F7D4 File Offset: 0x0003D9D4
		public bool HasState
		{
			get
			{
				return this.hasState;
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x060011C2 RID: 4546 RVA: 0x0003F7DC File Offset: 0x0003D9DC
		public int State
		{
			get
			{
				return this.state_;
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x060011C3 RID: 4547 RVA: 0x0003F7E4 File Offset: 0x0003D9E4
		public bool HasOpen
		{
			get
			{
				return this.hasOpen;
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x060011C4 RID: 4548 RVA: 0x0003F7EC File Offset: 0x0003D9EC
		public bool Open
		{
			get
			{
				return this.open_;
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x060011C5 RID: 4549 RVA: 0x0003F7F4 File Offset: 0x0003D9F4
		public override bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x0003F7F8 File Offset: 0x0003D9F8
		public override void WriteTo(ICodedOutputStream output)
		{
			int serializedSize = this.SerializedSize;
			string[] objectDoorFieldNames = objectDoor._objectDoorFieldNames;
			if (this.hasState)
			{
				output.WriteInt32(1, objectDoorFieldNames[1], this.State);
			}
			if (this.hasOpen)
			{
				output.WriteBool(2, objectDoorFieldNames[0], this.Open);
			}
			this.UnknownFields.WriteTo(output);
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x060011C7 RID: 4551 RVA: 0x0003F854 File Offset: 0x0003DA54
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
				if (this.hasState)
				{
					num += CodedOutputStream.ComputeInt32Size(1, this.State);
				}
				if (this.hasOpen)
				{
					num += CodedOutputStream.ComputeBoolSize(2, this.Open);
				}
				num += this.UnknownFields.SerializedSize;
				this.memoizedSerializedSize = num;
				return num;
			}
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x0003F8C0 File Offset: 0x0003DAC0
		public static objectDoor ParseFrom(ByteString data)
		{
			return objectDoor.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x0003F8D4 File Offset: 0x0003DAD4
		public static objectDoor ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return objectDoor.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x0003F8E8 File Offset: 0x0003DAE8
		public static objectDoor ParseFrom(byte[] data)
		{
			return objectDoor.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x0003F8FC File Offset: 0x0003DAFC
		public static objectDoor ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return objectDoor.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x0003F910 File Offset: 0x0003DB10
		public static objectDoor ParseFrom(Stream input)
		{
			return objectDoor.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x0003F924 File Offset: 0x0003DB24
		public static objectDoor ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectDoor.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x0003F938 File Offset: 0x0003DB38
		public static objectDoor ParseDelimitedFrom(Stream input)
		{
			return objectDoor.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x0003F94C File Offset: 0x0003DB4C
		public static objectDoor ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectDoor.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x0003F960 File Offset: 0x0003DB60
		public static objectDoor ParseFrom(ICodedInputStream input)
		{
			return objectDoor.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x0003F974 File Offset: 0x0003DB74
		public static objectDoor ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return objectDoor.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x0003F988 File Offset: 0x0003DB88
		private objectDoor MakeReadOnly()
		{
			return this;
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x0003F98C File Offset: 0x0003DB8C
		public static objectDoor.Builder CreateBuilder()
		{
			return new objectDoor.Builder();
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x0003F994 File Offset: 0x0003DB94
		public override objectDoor.Builder ToBuilder()
		{
			return objectDoor.CreateBuilder(this);
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x0003F99C File Offset: 0x0003DB9C
		public override objectDoor.Builder CreateBuilderForType()
		{
			return new objectDoor.Builder();
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x0003F9A4 File Offset: 0x0003DBA4
		public static objectDoor.Builder CreateBuilder(objectDoor prototype)
		{
			return new objectDoor.Builder(prototype);
		}

		// Token: 0x04000981 RID: 2433
		public const int StateFieldNumber = 1;

		// Token: 0x04000982 RID: 2434
		public const int OpenFieldNumber = 2;

		// Token: 0x04000983 RID: 2435
		private static readonly objectDoor defaultInstance = new objectDoor().MakeReadOnly();

		// Token: 0x04000984 RID: 2436
		private static readonly string[] _objectDoorFieldNames = new string[]
		{
			"Open",
			"State"
		};

		// Token: 0x04000985 RID: 2437
		private static readonly uint[] _objectDoorFieldTags = new uint[]
		{
			16u,
			8u
		};

		// Token: 0x04000986 RID: 2438
		private bool hasState;

		// Token: 0x04000987 RID: 2439
		private int state_;

		// Token: 0x04000988 RID: 2440
		private bool hasOpen;

		// Token: 0x04000989 RID: 2441
		private bool open_;

		// Token: 0x0400098A RID: 2442
		private int memoizedSerializedSize = -1;

		// Token: 0x02000214 RID: 532
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<objectDoor, objectDoor.Builder>
		{
			// Token: 0x060011D7 RID: 4567 RVA: 0x0003F9AC File Offset: 0x0003DBAC
			public Builder()
			{
				this.result = objectDoor.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x060011D8 RID: 4568 RVA: 0x0003F9C8 File Offset: 0x0003DBC8
			internal Builder(objectDoor cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x170004E9 RID: 1257
			// (get) Token: 0x060011D9 RID: 4569 RVA: 0x0003F9E0 File Offset: 0x0003DBE0
			protected override objectDoor.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x060011DA RID: 4570 RVA: 0x0003F9E4 File Offset: 0x0003DBE4
			private objectDoor PrepareBuilder()
			{
				if (this.resultIsReadOnly)
				{
					objectDoor other = this.result;
					this.result = new objectDoor();
					this.resultIsReadOnly = false;
					this.MergeFrom(other);
				}
				return this.result;
			}

			// Token: 0x170004EA RID: 1258
			// (get) Token: 0x060011DB RID: 4571 RVA: 0x0003FA24 File Offset: 0x0003DC24
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x170004EB RID: 1259
			// (get) Token: 0x060011DC RID: 4572 RVA: 0x0003FA34 File Offset: 0x0003DC34
			protected override objectDoor MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x060011DD RID: 4573 RVA: 0x0003FA3C File Offset: 0x0003DC3C
			public override objectDoor.Builder Clear()
			{
				this.result = objectDoor.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x060011DE RID: 4574 RVA: 0x0003FA54 File Offset: 0x0003DC54
			public override objectDoor.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new objectDoor.Builder(this.result);
				}
				return new objectDoor.Builder().MergeFrom(this.result);
			}

			// Token: 0x170004EC RID: 1260
			// (get) Token: 0x060011DF RID: 4575 RVA: 0x0003FA80 File Offset: 0x0003DC80
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return objectDoor.Descriptor;
				}
			}

			// Token: 0x170004ED RID: 1261
			// (get) Token: 0x060011E0 RID: 4576 RVA: 0x0003FA88 File Offset: 0x0003DC88
			public override objectDoor DefaultInstanceForType
			{
				get
				{
					return objectDoor.DefaultInstance;
				}
			}

			// Token: 0x060011E1 RID: 4577 RVA: 0x0003FA90 File Offset: 0x0003DC90
			public override objectDoor BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x060011E2 RID: 4578 RVA: 0x0003FAC4 File Offset: 0x0003DCC4
			public override objectDoor.Builder MergeFrom(IMessage other)
			{
				if (other is objectDoor)
				{
					return this.MergeFrom((objectDoor)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x060011E3 RID: 4579 RVA: 0x0003FAE8 File Offset: 0x0003DCE8
			public override objectDoor.Builder MergeFrom(objectDoor other)
			{
				if (other == objectDoor.DefaultInstance)
				{
					return this;
				}
				this.PrepareBuilder();
				if (other.HasState)
				{
					this.State = other.State;
				}
				if (other.HasOpen)
				{
					this.Open = other.Open;
				}
				this.MergeUnknownFields(other.UnknownFields);
				return this;
			}

			// Token: 0x060011E4 RID: 4580 RVA: 0x0003FB48 File Offset: 0x0003DD48
			public override objectDoor.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x060011E5 RID: 4581 RVA: 0x0003FB58 File Offset: 0x0003DD58
			public override objectDoor.Builder MergeFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
			{
				this.PrepareBuilder();
				UnknownFieldSet.Builder builder = null;
				uint num;
				string text;
				while (input.ReadTag(ref num, ref text))
				{
					if (num == 0u && text != null)
					{
						int num2 = Array.BinarySearch<string>(objectDoor._objectDoorFieldNames, text, StringComparer.Ordinal);
						if (num2 < 0)
						{
							if (builder == null)
							{
								builder = UnknownFieldSet.CreateBuilder(this.UnknownFields);
							}
							this.ParseUnknownField(input, builder, extensionRegistry, num, text);
							continue;
						}
						num = objectDoor._objectDoorFieldTags[num2];
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
							this.result.hasOpen = input.ReadBool(ref this.result.open_);
						}
					}
					else
					{
						this.result.hasState = input.ReadInt32(ref this.result.state_);
					}
				}
				if (builder != null)
				{
					this.UnknownFields = builder.Build();
				}
				return this;
			}

			// Token: 0x170004EE RID: 1262
			// (get) Token: 0x060011E6 RID: 4582 RVA: 0x0003FC94 File Offset: 0x0003DE94
			public bool HasState
			{
				get
				{
					return this.result.hasState;
				}
			}

			// Token: 0x170004EF RID: 1263
			// (get) Token: 0x060011E7 RID: 4583 RVA: 0x0003FCA4 File Offset: 0x0003DEA4
			// (set) Token: 0x060011E8 RID: 4584 RVA: 0x0003FCB4 File Offset: 0x0003DEB4
			public int State
			{
				get
				{
					return this.result.State;
				}
				set
				{
					this.SetState(value);
				}
			}

			// Token: 0x060011E9 RID: 4585 RVA: 0x0003FCC0 File Offset: 0x0003DEC0
			public objectDoor.Builder SetState(int value)
			{
				this.PrepareBuilder();
				this.result.hasState = true;
				this.result.state_ = value;
				return this;
			}

			// Token: 0x060011EA RID: 4586 RVA: 0x0003FCF0 File Offset: 0x0003DEF0
			public objectDoor.Builder ClearState()
			{
				this.PrepareBuilder();
				this.result.hasState = false;
				this.result.state_ = 0;
				return this;
			}

			// Token: 0x170004F0 RID: 1264
			// (get) Token: 0x060011EB RID: 4587 RVA: 0x0003FD20 File Offset: 0x0003DF20
			public bool HasOpen
			{
				get
				{
					return this.result.hasOpen;
				}
			}

			// Token: 0x170004F1 RID: 1265
			// (get) Token: 0x060011EC RID: 4588 RVA: 0x0003FD30 File Offset: 0x0003DF30
			// (set) Token: 0x060011ED RID: 4589 RVA: 0x0003FD40 File Offset: 0x0003DF40
			public bool Open
			{
				get
				{
					return this.result.Open;
				}
				set
				{
					this.SetOpen(value);
				}
			}

			// Token: 0x060011EE RID: 4590 RVA: 0x0003FD4C File Offset: 0x0003DF4C
			public objectDoor.Builder SetOpen(bool value)
			{
				this.PrepareBuilder();
				this.result.hasOpen = true;
				this.result.open_ = value;
				return this;
			}

			// Token: 0x060011EF RID: 4591 RVA: 0x0003FD7C File Offset: 0x0003DF7C
			public objectDoor.Builder ClearOpen()
			{
				this.PrepareBuilder();
				this.result.hasOpen = false;
				this.result.open_ = false;
				return this;
			}

			// Token: 0x0400098B RID: 2443
			private bool resultIsReadOnly;

			// Token: 0x0400098C RID: 2444
			private objectDoor result;
		}
	}
}
