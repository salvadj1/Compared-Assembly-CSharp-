using System;
using System.Diagnostics;
using System.IO;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;
using RustProto.Proto;

namespace RustProto
{
	// Token: 0x02000234 RID: 564
	[DebuggerNonUserCode]
	public sealed class User : GeneratedMessage<User, User.Builder>
	{
		// Token: 0x06001491 RID: 5265 RVA: 0x000452B8 File Offset: 0x000434B8
		private User()
		{
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x000452D4 File Offset: 0x000434D4
		static User()
		{
			object.ReferenceEquals(User.Descriptor, null);
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06001493 RID: 5267 RVA: 0x00045338 File Offset: 0x00043538
		public static User DefaultInstance
		{
			get
			{
				return User.defaultInstance;
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06001494 RID: 5268 RVA: 0x00045340 File Offset: 0x00043540
		public override User DefaultInstanceForType
		{
			get
			{
				return User.DefaultInstance;
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06001495 RID: 5269 RVA: 0x00045348 File Offset: 0x00043548
		protected override User ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001496 RID: 5270 RVA: 0x0004534C File Offset: 0x0004354C
		public static MessageDescriptor Descriptor
		{
			get
			{
				return User.internal__static_RustProto_User__Descriptor;
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06001497 RID: 5271 RVA: 0x00045354 File Offset: 0x00043554
		protected override FieldAccessorTable<User, User.Builder> InternalFieldAccessors
		{
			get
			{
				return User.internal__static_RustProto_User__FieldAccessorTable;
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06001498 RID: 5272 RVA: 0x0004535C File Offset: 0x0004355C
		public bool HasUserid
		{
			get
			{
				return this.hasUserid;
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06001499 RID: 5273 RVA: 0x00045364 File Offset: 0x00043564
		[CLSCompliant(false)]
		public ulong Userid
		{
			get
			{
				return this.userid_;
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x0600149A RID: 5274 RVA: 0x0004536C File Offset: 0x0004356C
		public bool HasDisplayname
		{
			get
			{
				return this.hasDisplayname;
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x0600149B RID: 5275 RVA: 0x00045374 File Offset: 0x00043574
		public string Displayname
		{
			get
			{
				return this.displayname_;
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x0600149C RID: 5276 RVA: 0x0004537C File Offset: 0x0004357C
		public bool HasUsergroup
		{
			get
			{
				return this.hasUsergroup;
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x0600149D RID: 5277 RVA: 0x00045384 File Offset: 0x00043584
		public User.Types.UserGroup Usergroup
		{
			get
			{
				return this.usergroup_;
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x0600149E RID: 5278 RVA: 0x0004538C File Offset: 0x0004358C
		public override bool IsInitialized
		{
			get
			{
				return this.hasUserid && this.hasDisplayname && this.hasUsergroup;
			}
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x000453C4 File Offset: 0x000435C4
		public override void WriteTo(ICodedOutputStream output)
		{
			int serializedSize = this.SerializedSize;
			string[] userFieldNames = User._userFieldNames;
			if (this.hasUserid)
			{
				output.WriteUInt64(1, userFieldNames[2], this.Userid);
			}
			if (this.hasDisplayname)
			{
				output.WriteString(2, userFieldNames[0], this.Displayname);
			}
			if (this.hasUsergroup)
			{
				output.WriteEnum(3, userFieldNames[1], (int)this.Usergroup, this.Usergroup);
			}
			this.UnknownFields.WriteTo(output);
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x060014A0 RID: 5280 RVA: 0x00045448 File Offset: 0x00043648
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
				if (this.hasUserid)
				{
					num += CodedOutputStream.ComputeUInt64Size(1, this.Userid);
				}
				if (this.hasDisplayname)
				{
					num += CodedOutputStream.ComputeStringSize(2, this.Displayname);
				}
				if (this.hasUsergroup)
				{
					num += CodedOutputStream.ComputeEnumSize(3, (int)this.Usergroup);
				}
				num += this.UnknownFields.SerializedSize;
				this.memoizedSerializedSize = num;
				return num;
			}
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x000454CC File Offset: 0x000436CC
		public static User ParseFrom(ByteString data)
		{
			return User.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x000454E0 File Offset: 0x000436E0
		public static User ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return User.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x000454F4 File Offset: 0x000436F4
		public static User ParseFrom(byte[] data)
		{
			return User.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x00045508 File Offset: 0x00043708
		public static User ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return User.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x0004551C File Offset: 0x0004371C
		public static User ParseFrom(Stream input)
		{
			return User.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x00045530 File Offset: 0x00043730
		public static User ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return User.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x00045544 File Offset: 0x00043744
		public static User ParseDelimitedFrom(Stream input)
		{
			return User.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x00045558 File Offset: 0x00043758
		public static User ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return User.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x0004556C File Offset: 0x0004376C
		public static User ParseFrom(ICodedInputStream input)
		{
			return User.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x00045580 File Offset: 0x00043780
		public static User ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return User.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x00045594 File Offset: 0x00043794
		private User MakeReadOnly()
		{
			return this;
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x00045598 File Offset: 0x00043798
		public static User.Builder CreateBuilder()
		{
			return new User.Builder();
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x000455A0 File Offset: 0x000437A0
		public override User.Builder ToBuilder()
		{
			return User.CreateBuilder(this);
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x000455A8 File Offset: 0x000437A8
		public override User.Builder CreateBuilderForType()
		{
			return new User.Builder();
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x000455B0 File Offset: 0x000437B0
		public static User.Builder CreateBuilder(User prototype)
		{
			return new User.Builder(prototype);
		}

		// Token: 0x04000A45 RID: 2629
		public const int UseridFieldNumber = 1;

		// Token: 0x04000A46 RID: 2630
		public const int DisplaynameFieldNumber = 2;

		// Token: 0x04000A47 RID: 2631
		public const int UsergroupFieldNumber = 3;

		// Token: 0x04000A48 RID: 2632
		private static readonly User defaultInstance = new User().MakeReadOnly();

		// Token: 0x04000A49 RID: 2633
		private static readonly string[] _userFieldNames = new string[]
		{
			"displayname",
			"usergroup",
			"userid"
		};

		// Token: 0x04000A4A RID: 2634
		private static readonly uint[] _userFieldTags = new uint[]
		{
			18u,
			24u,
			8u
		};

		// Token: 0x04000A4B RID: 2635
		private bool hasUserid;

		// Token: 0x04000A4C RID: 2636
		private ulong userid_;

		// Token: 0x04000A4D RID: 2637
		private bool hasDisplayname;

		// Token: 0x04000A4E RID: 2638
		private string displayname_ = string.Empty;

		// Token: 0x04000A4F RID: 2639
		private bool hasUsergroup;

		// Token: 0x04000A50 RID: 2640
		private User.Types.UserGroup usergroup_;

		// Token: 0x04000A51 RID: 2641
		private int memoizedSerializedSize = -1;

		// Token: 0x02000235 RID: 565
		[DebuggerNonUserCode]
		public static class Types
		{
			// Token: 0x02000236 RID: 566
			public enum UserGroup
			{
				// Token: 0x04000A53 RID: 2643
				REGULAR,
				// Token: 0x04000A54 RID: 2644
				BANNED,
				// Token: 0x04000A55 RID: 2645
				ADMIN
			}
		}

		// Token: 0x02000237 RID: 567
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<User, User.Builder>
		{
			// Token: 0x060014B0 RID: 5296 RVA: 0x000455B8 File Offset: 0x000437B8
			public Builder()
			{
				this.result = User.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x060014B1 RID: 5297 RVA: 0x000455D4 File Offset: 0x000437D4
			internal Builder(User cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x170005F6 RID: 1526
			// (get) Token: 0x060014B2 RID: 5298 RVA: 0x000455EC File Offset: 0x000437EC
			protected override User.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x060014B3 RID: 5299 RVA: 0x000455F0 File Offset: 0x000437F0
			private User PrepareBuilder()
			{
				if (this.resultIsReadOnly)
				{
					User other = this.result;
					this.result = new User();
					this.resultIsReadOnly = false;
					this.MergeFrom(other);
				}
				return this.result;
			}

			// Token: 0x170005F7 RID: 1527
			// (get) Token: 0x060014B4 RID: 5300 RVA: 0x00045630 File Offset: 0x00043830
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x170005F8 RID: 1528
			// (get) Token: 0x060014B5 RID: 5301 RVA: 0x00045640 File Offset: 0x00043840
			protected override User MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x060014B6 RID: 5302 RVA: 0x00045648 File Offset: 0x00043848
			public override User.Builder Clear()
			{
				this.result = User.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x060014B7 RID: 5303 RVA: 0x00045660 File Offset: 0x00043860
			public override User.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new User.Builder(this.result);
				}
				return new User.Builder().MergeFrom(this.result);
			}

			// Token: 0x170005F9 RID: 1529
			// (get) Token: 0x060014B8 RID: 5304 RVA: 0x0004568C File Offset: 0x0004388C
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return User.Descriptor;
				}
			}

			// Token: 0x170005FA RID: 1530
			// (get) Token: 0x060014B9 RID: 5305 RVA: 0x00045694 File Offset: 0x00043894
			public override User DefaultInstanceForType
			{
				get
				{
					return User.DefaultInstance;
				}
			}

			// Token: 0x060014BA RID: 5306 RVA: 0x0004569C File Offset: 0x0004389C
			public override User BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x060014BB RID: 5307 RVA: 0x000456D0 File Offset: 0x000438D0
			public override User.Builder MergeFrom(IMessage other)
			{
				if (other is User)
				{
					return this.MergeFrom((User)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x060014BC RID: 5308 RVA: 0x000456F4 File Offset: 0x000438F4
			public override User.Builder MergeFrom(User other)
			{
				if (other == User.DefaultInstance)
				{
					return this;
				}
				this.PrepareBuilder();
				if (other.HasUserid)
				{
					this.Userid = other.Userid;
				}
				if (other.HasDisplayname)
				{
					this.Displayname = other.Displayname;
				}
				if (other.HasUsergroup)
				{
					this.Usergroup = other.Usergroup;
				}
				this.MergeUnknownFields(other.UnknownFields);
				return this;
			}

			// Token: 0x060014BD RID: 5309 RVA: 0x00045768 File Offset: 0x00043968
			public override User.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x060014BE RID: 5310 RVA: 0x00045778 File Offset: 0x00043978
			public override User.Builder MergeFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
			{
				this.PrepareBuilder();
				UnknownFieldSet.Builder builder = null;
				uint num;
				string text;
				while (input.ReadTag(ref num, ref text))
				{
					if (num == 0u && text != null)
					{
						int num2 = Array.BinarySearch<string>(User._userFieldNames, text, StringComparer.Ordinal);
						if (num2 < 0)
						{
							if (builder == null)
							{
								builder = UnknownFieldSet.CreateBuilder(this.UnknownFields);
							}
							this.ParseUnknownField(input, builder, extensionRegistry, num, text);
							continue;
						}
						num = User._userFieldTags[num2];
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
							object obj;
							if (num3 != 24u)
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
							else if (input.ReadEnum<User.Types.UserGroup>(ref this.result.usergroup_, ref obj))
							{
								this.result.hasUsergroup = true;
							}
							else if (obj is int)
							{
								if (builder == null)
								{
									builder = UnknownFieldSet.CreateBuilder(this.UnknownFields);
								}
								builder.MergeVarintField(3, (ulong)((long)((int)obj)));
							}
						}
						else
						{
							this.result.hasDisplayname = input.ReadString(ref this.result.displayname_);
						}
					}
					else
					{
						this.result.hasUserid = input.ReadUInt64(ref this.result.userid_);
					}
				}
				if (builder != null)
				{
					this.UnknownFields = builder.Build();
				}
				return this;
			}

			// Token: 0x170005FB RID: 1531
			// (get) Token: 0x060014BF RID: 5311 RVA: 0x0004591C File Offset: 0x00043B1C
			public bool HasUserid
			{
				get
				{
					return this.result.hasUserid;
				}
			}

			// Token: 0x170005FC RID: 1532
			// (get) Token: 0x060014C0 RID: 5312 RVA: 0x0004592C File Offset: 0x00043B2C
			// (set) Token: 0x060014C1 RID: 5313 RVA: 0x0004593C File Offset: 0x00043B3C
			[CLSCompliant(false)]
			public ulong Userid
			{
				get
				{
					return this.result.Userid;
				}
				set
				{
					this.SetUserid(value);
				}
			}

			// Token: 0x060014C2 RID: 5314 RVA: 0x00045948 File Offset: 0x00043B48
			[CLSCompliant(false)]
			public User.Builder SetUserid(ulong value)
			{
				this.PrepareBuilder();
				this.result.hasUserid = true;
				this.result.userid_ = value;
				return this;
			}

			// Token: 0x060014C3 RID: 5315 RVA: 0x00045978 File Offset: 0x00043B78
			public User.Builder ClearUserid()
			{
				this.PrepareBuilder();
				this.result.hasUserid = false;
				this.result.userid_ = 0UL;
				return this;
			}

			// Token: 0x170005FD RID: 1533
			// (get) Token: 0x060014C4 RID: 5316 RVA: 0x0004599C File Offset: 0x00043B9C
			public bool HasDisplayname
			{
				get
				{
					return this.result.hasDisplayname;
				}
			}

			// Token: 0x170005FE RID: 1534
			// (get) Token: 0x060014C5 RID: 5317 RVA: 0x000459AC File Offset: 0x00043BAC
			// (set) Token: 0x060014C6 RID: 5318 RVA: 0x000459BC File Offset: 0x00043BBC
			public string Displayname
			{
				get
				{
					return this.result.Displayname;
				}
				set
				{
					this.SetDisplayname(value);
				}
			}

			// Token: 0x060014C7 RID: 5319 RVA: 0x000459C8 File Offset: 0x00043BC8
			public User.Builder SetDisplayname(string value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasDisplayname = true;
				this.result.displayname_ = value;
				return this;
			}

			// Token: 0x060014C8 RID: 5320 RVA: 0x000459F8 File Offset: 0x00043BF8
			public User.Builder ClearDisplayname()
			{
				this.PrepareBuilder();
				this.result.hasDisplayname = false;
				this.result.displayname_ = string.Empty;
				return this;
			}

			// Token: 0x170005FF RID: 1535
			// (get) Token: 0x060014C9 RID: 5321 RVA: 0x00045A2C File Offset: 0x00043C2C
			public bool HasUsergroup
			{
				get
				{
					return this.result.hasUsergroup;
				}
			}

			// Token: 0x17000600 RID: 1536
			// (get) Token: 0x060014CA RID: 5322 RVA: 0x00045A3C File Offset: 0x00043C3C
			// (set) Token: 0x060014CB RID: 5323 RVA: 0x00045A4C File Offset: 0x00043C4C
			public User.Types.UserGroup Usergroup
			{
				get
				{
					return this.result.Usergroup;
				}
				set
				{
					this.SetUsergroup(value);
				}
			}

			// Token: 0x060014CC RID: 5324 RVA: 0x00045A58 File Offset: 0x00043C58
			public User.Builder SetUsergroup(User.Types.UserGroup value)
			{
				this.PrepareBuilder();
				this.result.hasUsergroup = true;
				this.result.usergroup_ = value;
				return this;
			}

			// Token: 0x060014CD RID: 5325 RVA: 0x00045A88 File Offset: 0x00043C88
			public User.Builder ClearUsergroup()
			{
				this.PrepareBuilder();
				this.result.hasUsergroup = false;
				this.result.usergroup_ = User.Types.UserGroup.REGULAR;
				return this;
			}

			// Token: 0x04000A56 RID: 2646
			private bool resultIsReadOnly;

			// Token: 0x04000A57 RID: 2647
			private User result;
		}
	}
}
