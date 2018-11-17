using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Collections;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;
using RustProto.Helpers;

namespace RustProto
{
	// Token: 0x02000219 RID: 537
	[DebuggerNonUserCode]
	public sealed class objectLockable : GeneratedMessage<objectLockable, objectLockable.Builder>
	{
		// Token: 0x06001250 RID: 4688 RVA: 0x00040844 File Offset: 0x0003EA44
		private objectLockable()
		{
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x0004086C File Offset: 0x0003EA6C
		static objectLockable()
		{
			object.ReferenceEquals(Worldsave.Descriptor, null);
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x000408D4 File Offset: 0x0003EAD4
		public static Recycler<objectLockable, objectLockable.Builder> Recycler()
		{
			return Recycler<objectLockable, objectLockable.Builder>.Manufacture();
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06001253 RID: 4691 RVA: 0x000408DC File Offset: 0x0003EADC
		public static objectLockable DefaultInstance
		{
			get
			{
				return objectLockable.defaultInstance;
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06001254 RID: 4692 RVA: 0x000408E4 File Offset: 0x0003EAE4
		public override objectLockable DefaultInstanceForType
		{
			get
			{
				return objectLockable.DefaultInstance;
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06001255 RID: 4693 RVA: 0x000408EC File Offset: 0x0003EAEC
		protected override objectLockable ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06001256 RID: 4694 RVA: 0x000408F0 File Offset: 0x0003EAF0
		public static MessageDescriptor Descriptor
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectLockable__Descriptor;
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001257 RID: 4695 RVA: 0x000408F8 File Offset: 0x0003EAF8
		protected override FieldAccessorTable<objectLockable, objectLockable.Builder> InternalFieldAccessors
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectLockable__FieldAccessorTable;
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06001258 RID: 4696 RVA: 0x00040900 File Offset: 0x0003EB00
		public bool HasPassword
		{
			get
			{
				return this.hasPassword;
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06001259 RID: 4697 RVA: 0x00040908 File Offset: 0x0003EB08
		public string Password
		{
			get
			{
				return this.password_;
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x0600125A RID: 4698 RVA: 0x00040910 File Offset: 0x0003EB10
		public bool HasLocked
		{
			get
			{
				return this.hasLocked;
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x0600125B RID: 4699 RVA: 0x00040918 File Offset: 0x0003EB18
		public bool Locked
		{
			get
			{
				return this.locked_;
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x0600125C RID: 4700 RVA: 0x00040920 File Offset: 0x0003EB20
		[CLSCompliant(false)]
		public IList<ulong> UsersList
		{
			get
			{
				return Lists.AsReadOnly<ulong>(this.users_);
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x0600125D RID: 4701 RVA: 0x00040930 File Offset: 0x0003EB30
		public int UsersCount
		{
			get
			{
				return this.users_.Count;
			}
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x00040940 File Offset: 0x0003EB40
		[CLSCompliant(false)]
		public ulong GetUsers(int index)
		{
			return this.users_[index];
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x0600125F RID: 4703 RVA: 0x00040950 File Offset: 0x0003EB50
		public override bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x00040954 File Offset: 0x0003EB54
		public override void WriteTo(ICodedOutputStream output)
		{
			int serializedSize = this.SerializedSize;
			string[] objectLockableFieldNames = objectLockable._objectLockableFieldNames;
			if (this.hasPassword)
			{
				output.WriteString(1, objectLockableFieldNames[1], this.Password);
			}
			if (this.hasLocked)
			{
				output.WriteBool(2, objectLockableFieldNames[0], this.Locked);
			}
			if (this.users_.Count > 0)
			{
				output.WriteUInt64Array(3, objectLockableFieldNames[2], this.users_);
			}
			this.UnknownFields.WriteTo(output);
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06001261 RID: 4705 RVA: 0x000409D4 File Offset: 0x0003EBD4
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
				if (this.hasPassword)
				{
					num += CodedOutputStream.ComputeStringSize(1, this.Password);
				}
				if (this.hasLocked)
				{
					num += CodedOutputStream.ComputeBoolSize(2, this.Locked);
				}
				int num2 = 0;
				foreach (ulong num3 in this.UsersList)
				{
					num2 += CodedOutputStream.ComputeUInt64SizeNoTag(num3);
				}
				num += num2;
				num += 1 * this.users_.Count;
				num += this.UnknownFields.SerializedSize;
				this.memoizedSerializedSize = num;
				return num;
			}
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x00040AAC File Offset: 0x0003ECAC
		public static objectLockable ParseFrom(ByteString data)
		{
			return objectLockable.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x00040AC0 File Offset: 0x0003ECC0
		public static objectLockable ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return objectLockable.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x00040AD4 File Offset: 0x0003ECD4
		public static objectLockable ParseFrom(byte[] data)
		{
			return objectLockable.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x00040AE8 File Offset: 0x0003ECE8
		public static objectLockable ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return objectLockable.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x00040AFC File Offset: 0x0003ECFC
		public static objectLockable ParseFrom(Stream input)
		{
			return objectLockable.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x00040B10 File Offset: 0x0003ED10
		public static objectLockable ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectLockable.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x00040B24 File Offset: 0x0003ED24
		public static objectLockable ParseDelimitedFrom(Stream input)
		{
			return objectLockable.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x00040B38 File Offset: 0x0003ED38
		public static objectLockable ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectLockable.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x00040B4C File Offset: 0x0003ED4C
		public static objectLockable ParseFrom(ICodedInputStream input)
		{
			return objectLockable.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x00040B60 File Offset: 0x0003ED60
		public static objectLockable ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return objectLockable.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x00040B74 File Offset: 0x0003ED74
		private objectLockable MakeReadOnly()
		{
			this.users_.MakeReadOnly();
			return this;
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x00040B84 File Offset: 0x0003ED84
		public static objectLockable.Builder CreateBuilder()
		{
			return new objectLockable.Builder();
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x00040B8C File Offset: 0x0003ED8C
		public override objectLockable.Builder ToBuilder()
		{
			return objectLockable.CreateBuilder(this);
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x00040B94 File Offset: 0x0003ED94
		public override objectLockable.Builder CreateBuilderForType()
		{
			return new objectLockable.Builder();
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x00040B9C File Offset: 0x0003ED9C
		public static objectLockable.Builder CreateBuilder(objectLockable prototype)
		{
			return new objectLockable.Builder(prototype);
		}

		// Token: 0x0400099F RID: 2463
		public const int PasswordFieldNumber = 1;

		// Token: 0x040009A0 RID: 2464
		public const int LockedFieldNumber = 2;

		// Token: 0x040009A1 RID: 2465
		public const int UsersFieldNumber = 3;

		// Token: 0x040009A2 RID: 2466
		private static readonly objectLockable defaultInstance = new objectLockable().MakeReadOnly();

		// Token: 0x040009A3 RID: 2467
		private static readonly string[] _objectLockableFieldNames = new string[]
		{
			"locked",
			"password",
			"users"
		};

		// Token: 0x040009A4 RID: 2468
		private static readonly uint[] _objectLockableFieldTags = new uint[]
		{
			16u,
			10u,
			24u
		};

		// Token: 0x040009A5 RID: 2469
		private bool hasPassword;

		// Token: 0x040009A6 RID: 2470
		private string password_ = string.Empty;

		// Token: 0x040009A7 RID: 2471
		private bool hasLocked;

		// Token: 0x040009A8 RID: 2472
		private bool locked_;

		// Token: 0x040009A9 RID: 2473
		private PopsicleList<ulong> users_ = new PopsicleList<ulong>();

		// Token: 0x040009AA RID: 2474
		private int memoizedSerializedSize = -1;

		// Token: 0x0200021A RID: 538
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<objectLockable, objectLockable.Builder>
		{
			// Token: 0x06001271 RID: 4721 RVA: 0x00040BA4 File Offset: 0x0003EDA4
			public Builder()
			{
				this.result = objectLockable.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x06001272 RID: 4722 RVA: 0x00040BC0 File Offset: 0x0003EDC0
			internal Builder(objectLockable cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x1700051F RID: 1311
			// (get) Token: 0x06001273 RID: 4723 RVA: 0x00040BD8 File Offset: 0x0003EDD8
			protected override objectLockable.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x06001274 RID: 4724 RVA: 0x00040BDC File Offset: 0x0003EDDC
			private objectLockable PrepareBuilder()
			{
				if (this.resultIsReadOnly)
				{
					objectLockable other = this.result;
					this.result = new objectLockable();
					this.resultIsReadOnly = false;
					this.MergeFrom(other);
				}
				return this.result;
			}

			// Token: 0x17000520 RID: 1312
			// (get) Token: 0x06001275 RID: 4725 RVA: 0x00040C1C File Offset: 0x0003EE1C
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x17000521 RID: 1313
			// (get) Token: 0x06001276 RID: 4726 RVA: 0x00040C2C File Offset: 0x0003EE2C
			protected override objectLockable MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x06001277 RID: 4727 RVA: 0x00040C34 File Offset: 0x0003EE34
			public override objectLockable.Builder Clear()
			{
				this.result = objectLockable.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x06001278 RID: 4728 RVA: 0x00040C4C File Offset: 0x0003EE4C
			public override objectLockable.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new objectLockable.Builder(this.result);
				}
				return new objectLockable.Builder().MergeFrom(this.result);
			}

			// Token: 0x17000522 RID: 1314
			// (get) Token: 0x06001279 RID: 4729 RVA: 0x00040C78 File Offset: 0x0003EE78
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return objectLockable.Descriptor;
				}
			}

			// Token: 0x17000523 RID: 1315
			// (get) Token: 0x0600127A RID: 4730 RVA: 0x00040C80 File Offset: 0x0003EE80
			public override objectLockable DefaultInstanceForType
			{
				get
				{
					return objectLockable.DefaultInstance;
				}
			}

			// Token: 0x0600127B RID: 4731 RVA: 0x00040C88 File Offset: 0x0003EE88
			public override objectLockable BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x0600127C RID: 4732 RVA: 0x00040CBC File Offset: 0x0003EEBC
			public override objectLockable.Builder MergeFrom(IMessage other)
			{
				if (other is objectLockable)
				{
					return this.MergeFrom((objectLockable)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x0600127D RID: 4733 RVA: 0x00040CE0 File Offset: 0x0003EEE0
			public override objectLockable.Builder MergeFrom(objectLockable other)
			{
				if (other == objectLockable.DefaultInstance)
				{
					return this;
				}
				this.PrepareBuilder();
				if (other.HasPassword)
				{
					this.Password = other.Password;
				}
				if (other.HasLocked)
				{
					this.Locked = other.Locked;
				}
				if (other.users_.Count != 0)
				{
					this.result.users_.Add(other.users_);
				}
				this.MergeUnknownFields(other.UnknownFields);
				return this;
			}

			// Token: 0x0600127E RID: 4734 RVA: 0x00040D64 File Offset: 0x0003EF64
			public override objectLockable.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x0600127F RID: 4735 RVA: 0x00040D74 File Offset: 0x0003EF74
			public override objectLockable.Builder MergeFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
			{
				this.PrepareBuilder();
				UnknownFieldSet.Builder builder = null;
				uint num;
				string text;
				while (input.ReadTag(ref num, ref text))
				{
					if (num == 0u && text != null)
					{
						int num2 = Array.BinarySearch<string>(objectLockable._objectLockableFieldNames, text, StringComparer.Ordinal);
						if (num2 < 0)
						{
							if (builder == null)
							{
								builder = UnknownFieldSet.CreateBuilder(this.UnknownFields);
							}
							this.ParseUnknownField(input, builder, extensionRegistry, num, text);
							continue;
						}
						num = objectLockable._objectLockableFieldTags[num2];
					}
					uint num3 = num;
					switch (num3)
					{
					case 24u:
					case 26u:
						input.ReadUInt64Array(num, text, this.result.users_);
						break;
					default:
						if (num3 == 0u)
						{
							throw InvalidProtocolBufferException.InvalidTag();
						}
						if (num3 != 10u)
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
								this.result.hasLocked = input.ReadBool(ref this.result.locked_);
							}
						}
						else
						{
							this.result.hasPassword = input.ReadString(ref this.result.password_);
						}
						break;
					}
				}
				if (builder != null)
				{
					this.UnknownFields = builder.Build();
				}
				return this;
			}

			// Token: 0x17000524 RID: 1316
			// (get) Token: 0x06001280 RID: 4736 RVA: 0x00040EE0 File Offset: 0x0003F0E0
			public bool HasPassword
			{
				get
				{
					return this.result.hasPassword;
				}
			}

			// Token: 0x17000525 RID: 1317
			// (get) Token: 0x06001281 RID: 4737 RVA: 0x00040EF0 File Offset: 0x0003F0F0
			// (set) Token: 0x06001282 RID: 4738 RVA: 0x00040F00 File Offset: 0x0003F100
			public string Password
			{
				get
				{
					return this.result.Password;
				}
				set
				{
					this.SetPassword(value);
				}
			}

			// Token: 0x06001283 RID: 4739 RVA: 0x00040F0C File Offset: 0x0003F10C
			public objectLockable.Builder SetPassword(string value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasPassword = true;
				this.result.password_ = value;
				return this;
			}

			// Token: 0x06001284 RID: 4740 RVA: 0x00040F3C File Offset: 0x0003F13C
			public objectLockable.Builder ClearPassword()
			{
				this.PrepareBuilder();
				this.result.hasPassword = false;
				this.result.password_ = string.Empty;
				return this;
			}

			// Token: 0x17000526 RID: 1318
			// (get) Token: 0x06001285 RID: 4741 RVA: 0x00040F70 File Offset: 0x0003F170
			public bool HasLocked
			{
				get
				{
					return this.result.hasLocked;
				}
			}

			// Token: 0x17000527 RID: 1319
			// (get) Token: 0x06001286 RID: 4742 RVA: 0x00040F80 File Offset: 0x0003F180
			// (set) Token: 0x06001287 RID: 4743 RVA: 0x00040F90 File Offset: 0x0003F190
			public bool Locked
			{
				get
				{
					return this.result.Locked;
				}
				set
				{
					this.SetLocked(value);
				}
			}

			// Token: 0x06001288 RID: 4744 RVA: 0x00040F9C File Offset: 0x0003F19C
			public objectLockable.Builder SetLocked(bool value)
			{
				this.PrepareBuilder();
				this.result.hasLocked = true;
				this.result.locked_ = value;
				return this;
			}

			// Token: 0x06001289 RID: 4745 RVA: 0x00040FCC File Offset: 0x0003F1CC
			public objectLockable.Builder ClearLocked()
			{
				this.PrepareBuilder();
				this.result.hasLocked = false;
				this.result.locked_ = false;
				return this;
			}

			// Token: 0x17000528 RID: 1320
			// (get) Token: 0x0600128A RID: 4746 RVA: 0x00040FFC File Offset: 0x0003F1FC
			[CLSCompliant(false)]
			public IPopsicleList<ulong> UsersList
			{
				get
				{
					return this.PrepareBuilder().users_;
				}
			}

			// Token: 0x17000529 RID: 1321
			// (get) Token: 0x0600128B RID: 4747 RVA: 0x0004100C File Offset: 0x0003F20C
			public int UsersCount
			{
				get
				{
					return this.result.UsersCount;
				}
			}

			// Token: 0x0600128C RID: 4748 RVA: 0x0004101C File Offset: 0x0003F21C
			[CLSCompliant(false)]
			public ulong GetUsers(int index)
			{
				return this.result.GetUsers(index);
			}

			// Token: 0x0600128D RID: 4749 RVA: 0x0004102C File Offset: 0x0003F22C
			[CLSCompliant(false)]
			public objectLockable.Builder SetUsers(int index, ulong value)
			{
				this.PrepareBuilder();
				this.result.users_[index] = value;
				return this;
			}

			// Token: 0x0600128E RID: 4750 RVA: 0x00041048 File Offset: 0x0003F248
			[CLSCompliant(false)]
			public objectLockable.Builder AddUsers(ulong value)
			{
				this.PrepareBuilder();
				this.result.users_.Add(value);
				return this;
			}

			// Token: 0x0600128F RID: 4751 RVA: 0x00041064 File Offset: 0x0003F264
			[CLSCompliant(false)]
			public objectLockable.Builder AddRangeUsers(IEnumerable<ulong> values)
			{
				this.PrepareBuilder();
				this.result.users_.Add(values);
				return this;
			}

			// Token: 0x06001290 RID: 4752 RVA: 0x00041080 File Offset: 0x0003F280
			public objectLockable.Builder ClearUsers()
			{
				this.PrepareBuilder();
				this.result.users_.Clear();
				return this;
			}

			// Token: 0x040009AB RID: 2475
			private bool resultIsReadOnly;

			// Token: 0x040009AC RID: 2476
			private objectLockable result;
		}
	}
}
