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
	// Token: 0x0200024C RID: 588
	[DebuggerNonUserCode]
	public sealed class objectLockable : GeneratedMessage<objectLockable, objectLockable.Builder>
	{
		// Token: 0x060013A4 RID: 5028 RVA: 0x00044BEC File Offset: 0x00042DEC
		private objectLockable()
		{
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x00044C14 File Offset: 0x00042E14
		static objectLockable()
		{
			object.ReferenceEquals(Worldsave.Descriptor, null);
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x00044C7C File Offset: 0x00042E7C
		public static RustProto.Helpers.Recycler<objectLockable, objectLockable.Builder> Recycler()
		{
			return RustProto.Helpers.Recycler<objectLockable, objectLockable.Builder>.Manufacture();
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x060013A7 RID: 5031 RVA: 0x00044C84 File Offset: 0x00042E84
		public static objectLockable DefaultInstance
		{
			get
			{
				return objectLockable.defaultInstance;
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x060013A8 RID: 5032 RVA: 0x00044C8C File Offset: 0x00042E8C
		public override objectLockable DefaultInstanceForType
		{
			get
			{
				return objectLockable.DefaultInstance;
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x060013A9 RID: 5033 RVA: 0x00044C94 File Offset: 0x00042E94
		protected override objectLockable ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x060013AA RID: 5034 RVA: 0x00044C98 File Offset: 0x00042E98
		public static MessageDescriptor Descriptor
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectLockable__Descriptor;
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x060013AB RID: 5035 RVA: 0x00044CA0 File Offset: 0x00042EA0
		protected override FieldAccessorTable<objectLockable, objectLockable.Builder> InternalFieldAccessors
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectLockable__FieldAccessorTable;
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x060013AC RID: 5036 RVA: 0x00044CA8 File Offset: 0x00042EA8
		public bool HasPassword
		{
			get
			{
				return this.hasPassword;
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x060013AD RID: 5037 RVA: 0x00044CB0 File Offset: 0x00042EB0
		public string Password
		{
			get
			{
				return this.password_;
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x060013AE RID: 5038 RVA: 0x00044CB8 File Offset: 0x00042EB8
		public bool HasLocked
		{
			get
			{
				return this.hasLocked;
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x060013AF RID: 5039 RVA: 0x00044CC0 File Offset: 0x00042EC0
		public bool Locked
		{
			get
			{
				return this.locked_;
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x060013B0 RID: 5040 RVA: 0x00044CC8 File Offset: 0x00042EC8
		[CLSCompliant(false)]
		public IList<ulong> UsersList
		{
			get
			{
				return Lists.AsReadOnly<ulong>(this.users_);
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x060013B1 RID: 5041 RVA: 0x00044CD8 File Offset: 0x00042ED8
		public int UsersCount
		{
			get
			{
				return this.users_.Count;
			}
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x00044CE8 File Offset: 0x00042EE8
		[CLSCompliant(false)]
		public ulong GetUsers(int index)
		{
			return this.users_[index];
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x060013B3 RID: 5043 RVA: 0x00044CF8 File Offset: 0x00042EF8
		public override bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x00044CFC File Offset: 0x00042EFC
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

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x060013B5 RID: 5045 RVA: 0x00044D7C File Offset: 0x00042F7C
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

		// Token: 0x060013B6 RID: 5046 RVA: 0x00044E54 File Offset: 0x00043054
		public static objectLockable ParseFrom(ByteString data)
		{
			return objectLockable.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x00044E68 File Offset: 0x00043068
		public static objectLockable ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return objectLockable.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x00044E7C File Offset: 0x0004307C
		public static objectLockable ParseFrom(byte[] data)
		{
			return objectLockable.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x00044E90 File Offset: 0x00043090
		public static objectLockable ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return objectLockable.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x00044EA4 File Offset: 0x000430A4
		public static objectLockable ParseFrom(Stream input)
		{
			return objectLockable.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x00044EB8 File Offset: 0x000430B8
		public static objectLockable ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectLockable.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x00044ECC File Offset: 0x000430CC
		public static objectLockable ParseDelimitedFrom(Stream input)
		{
			return objectLockable.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x00044EE0 File Offset: 0x000430E0
		public static objectLockable ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectLockable.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x00044EF4 File Offset: 0x000430F4
		public static objectLockable ParseFrom(ICodedInputStream input)
		{
			return objectLockable.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x00044F08 File Offset: 0x00043108
		public static objectLockable ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return objectLockable.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x00044F1C File Offset: 0x0004311C
		private objectLockable MakeReadOnly()
		{
			this.users_.MakeReadOnly();
			return this;
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x00044F2C File Offset: 0x0004312C
		public static objectLockable.Builder CreateBuilder()
		{
			return new objectLockable.Builder();
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x00044F34 File Offset: 0x00043134
		public override objectLockable.Builder ToBuilder()
		{
			return objectLockable.CreateBuilder(this);
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x00044F3C File Offset: 0x0004313C
		public override objectLockable.Builder CreateBuilderForType()
		{
			return new objectLockable.Builder();
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x00044F44 File Offset: 0x00043144
		public static objectLockable.Builder CreateBuilder(objectLockable prototype)
		{
			return new objectLockable.Builder(prototype);
		}

		// Token: 0x04000AC2 RID: 2754
		public const int PasswordFieldNumber = 1;

		// Token: 0x04000AC3 RID: 2755
		public const int LockedFieldNumber = 2;

		// Token: 0x04000AC4 RID: 2756
		public const int UsersFieldNumber = 3;

		// Token: 0x04000AC5 RID: 2757
		private static readonly objectLockable defaultInstance = new objectLockable().MakeReadOnly();

		// Token: 0x04000AC6 RID: 2758
		private static readonly string[] _objectLockableFieldNames = new string[]
		{
			"locked",
			"password",
			"users"
		};

		// Token: 0x04000AC7 RID: 2759
		private static readonly uint[] _objectLockableFieldTags = new uint[]
		{
			16u,
			10u,
			24u
		};

		// Token: 0x04000AC8 RID: 2760
		private bool hasPassword;

		// Token: 0x04000AC9 RID: 2761
		private string password_ = string.Empty;

		// Token: 0x04000ACA RID: 2762
		private bool hasLocked;

		// Token: 0x04000ACB RID: 2763
		private bool locked_;

		// Token: 0x04000ACC RID: 2764
		private PopsicleList<ulong> users_ = new PopsicleList<ulong>();

		// Token: 0x04000ACD RID: 2765
		private int memoizedSerializedSize = -1;

		// Token: 0x0200024D RID: 589
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<objectLockable, objectLockable.Builder>
		{
			// Token: 0x060013C5 RID: 5061 RVA: 0x00044F4C File Offset: 0x0004314C
			public Builder()
			{
				this.result = objectLockable.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x060013C6 RID: 5062 RVA: 0x00044F68 File Offset: 0x00043168
			internal Builder(objectLockable cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x17000567 RID: 1383
			// (get) Token: 0x060013C7 RID: 5063 RVA: 0x00044F80 File Offset: 0x00043180
			protected override objectLockable.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x060013C8 RID: 5064 RVA: 0x00044F84 File Offset: 0x00043184
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

			// Token: 0x17000568 RID: 1384
			// (get) Token: 0x060013C9 RID: 5065 RVA: 0x00044FC4 File Offset: 0x000431C4
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x17000569 RID: 1385
			// (get) Token: 0x060013CA RID: 5066 RVA: 0x00044FD4 File Offset: 0x000431D4
			protected override objectLockable MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x060013CB RID: 5067 RVA: 0x00044FDC File Offset: 0x000431DC
			public override objectLockable.Builder Clear()
			{
				this.result = objectLockable.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x060013CC RID: 5068 RVA: 0x00044FF4 File Offset: 0x000431F4
			public override objectLockable.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new objectLockable.Builder(this.result);
				}
				return new objectLockable.Builder().MergeFrom(this.result);
			}

			// Token: 0x1700056A RID: 1386
			// (get) Token: 0x060013CD RID: 5069 RVA: 0x00045020 File Offset: 0x00043220
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return objectLockable.Descriptor;
				}
			}

			// Token: 0x1700056B RID: 1387
			// (get) Token: 0x060013CE RID: 5070 RVA: 0x00045028 File Offset: 0x00043228
			public override objectLockable DefaultInstanceForType
			{
				get
				{
					return objectLockable.DefaultInstance;
				}
			}

			// Token: 0x060013CF RID: 5071 RVA: 0x00045030 File Offset: 0x00043230
			public override objectLockable BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x060013D0 RID: 5072 RVA: 0x00045064 File Offset: 0x00043264
			public override objectLockable.Builder MergeFrom(IMessage other)
			{
				if (other is objectLockable)
				{
					return this.MergeFrom((objectLockable)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x060013D1 RID: 5073 RVA: 0x00045088 File Offset: 0x00043288
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

			// Token: 0x060013D2 RID: 5074 RVA: 0x0004510C File Offset: 0x0004330C
			public override objectLockable.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x060013D3 RID: 5075 RVA: 0x0004511C File Offset: 0x0004331C
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

			// Token: 0x1700056C RID: 1388
			// (get) Token: 0x060013D4 RID: 5076 RVA: 0x00045288 File Offset: 0x00043488
			public bool HasPassword
			{
				get
				{
					return this.result.hasPassword;
				}
			}

			// Token: 0x1700056D RID: 1389
			// (get) Token: 0x060013D5 RID: 5077 RVA: 0x00045298 File Offset: 0x00043498
			// (set) Token: 0x060013D6 RID: 5078 RVA: 0x000452A8 File Offset: 0x000434A8
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

			// Token: 0x060013D7 RID: 5079 RVA: 0x000452B4 File Offset: 0x000434B4
			public objectLockable.Builder SetPassword(string value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasPassword = true;
				this.result.password_ = value;
				return this;
			}

			// Token: 0x060013D8 RID: 5080 RVA: 0x000452E4 File Offset: 0x000434E4
			public objectLockable.Builder ClearPassword()
			{
				this.PrepareBuilder();
				this.result.hasPassword = false;
				this.result.password_ = string.Empty;
				return this;
			}

			// Token: 0x1700056E RID: 1390
			// (get) Token: 0x060013D9 RID: 5081 RVA: 0x00045318 File Offset: 0x00043518
			public bool HasLocked
			{
				get
				{
					return this.result.hasLocked;
				}
			}

			// Token: 0x1700056F RID: 1391
			// (get) Token: 0x060013DA RID: 5082 RVA: 0x00045328 File Offset: 0x00043528
			// (set) Token: 0x060013DB RID: 5083 RVA: 0x00045338 File Offset: 0x00043538
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

			// Token: 0x060013DC RID: 5084 RVA: 0x00045344 File Offset: 0x00043544
			public objectLockable.Builder SetLocked(bool value)
			{
				this.PrepareBuilder();
				this.result.hasLocked = true;
				this.result.locked_ = value;
				return this;
			}

			// Token: 0x060013DD RID: 5085 RVA: 0x00045374 File Offset: 0x00043574
			public objectLockable.Builder ClearLocked()
			{
				this.PrepareBuilder();
				this.result.hasLocked = false;
				this.result.locked_ = false;
				return this;
			}

			// Token: 0x17000570 RID: 1392
			// (get) Token: 0x060013DE RID: 5086 RVA: 0x000453A4 File Offset: 0x000435A4
			[CLSCompliant(false)]
			public IPopsicleList<ulong> UsersList
			{
				get
				{
					return this.PrepareBuilder().users_;
				}
			}

			// Token: 0x17000571 RID: 1393
			// (get) Token: 0x060013DF RID: 5087 RVA: 0x000453B4 File Offset: 0x000435B4
			public int UsersCount
			{
				get
				{
					return this.result.UsersCount;
				}
			}

			// Token: 0x060013E0 RID: 5088 RVA: 0x000453C4 File Offset: 0x000435C4
			[CLSCompliant(false)]
			public ulong GetUsers(int index)
			{
				return this.result.GetUsers(index);
			}

			// Token: 0x060013E1 RID: 5089 RVA: 0x000453D4 File Offset: 0x000435D4
			[CLSCompliant(false)]
			public objectLockable.Builder SetUsers(int index, ulong value)
			{
				this.PrepareBuilder();
				this.result.users_[index] = value;
				return this;
			}

			// Token: 0x060013E2 RID: 5090 RVA: 0x000453F0 File Offset: 0x000435F0
			[CLSCompliant(false)]
			public objectLockable.Builder AddUsers(ulong value)
			{
				this.PrepareBuilder();
				this.result.users_.Add(value);
				return this;
			}

			// Token: 0x060013E3 RID: 5091 RVA: 0x0004540C File Offset: 0x0004360C
			[CLSCompliant(false)]
			public objectLockable.Builder AddRangeUsers(IEnumerable<ulong> values)
			{
				this.PrepareBuilder();
				this.result.users_.Add(values);
				return this;
			}

			// Token: 0x060013E4 RID: 5092 RVA: 0x00045428 File Offset: 0x00043628
			public objectLockable.Builder ClearUsers()
			{
				this.PrepareBuilder();
				this.result.users_.Clear();
				return this;
			}

			// Token: 0x04000ACE RID: 2766
			private bool resultIsReadOnly;

			// Token: 0x04000ACF RID: 2767
			private objectLockable result;
		}
	}
}
