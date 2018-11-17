using System;
using System.Diagnostics;
using System.IO;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;
using RustProto.Proto;

namespace RustProto
{
	// Token: 0x02000267 RID: 615
	[DebuggerNonUserCode]
	public sealed class User : GeneratedMessage<RustProto.User, RustProto.User.Builder>
	{
		// Token: 0x060015E5 RID: 5605 RVA: 0x00049660 File Offset: 0x00047860
		private User()
		{
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x0004967C File Offset: 0x0004787C
		static User()
		{
			object.ReferenceEquals(RustProto.Proto.User.Descriptor, null);
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x060015E7 RID: 5607 RVA: 0x000496E0 File Offset: 0x000478E0
		public static RustProto.User DefaultInstance
		{
			get
			{
				return RustProto.User.defaultInstance;
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x060015E8 RID: 5608 RVA: 0x000496E8 File Offset: 0x000478E8
		public override RustProto.User DefaultInstanceForType
		{
			get
			{
				return RustProto.User.DefaultInstance;
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x060015E9 RID: 5609 RVA: 0x000496F0 File Offset: 0x000478F0
		protected override RustProto.User ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x060015EA RID: 5610 RVA: 0x000496F4 File Offset: 0x000478F4
		public static MessageDescriptor Descriptor
		{
			get
			{
				return RustProto.Proto.User.internal__static_RustProto_User__Descriptor;
			}
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x060015EB RID: 5611 RVA: 0x000496FC File Offset: 0x000478FC
		protected override FieldAccessorTable<RustProto.User, RustProto.User.Builder> InternalFieldAccessors
		{
			get
			{
				return RustProto.Proto.User.internal__static_RustProto_User__FieldAccessorTable;
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x060015EC RID: 5612 RVA: 0x00049704 File Offset: 0x00047904
		public bool HasUserid
		{
			get
			{
				return this.hasUserid;
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x060015ED RID: 5613 RVA: 0x0004970C File Offset: 0x0004790C
		[CLSCompliant(false)]
		public ulong Userid
		{
			get
			{
				return this.userid_;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x060015EE RID: 5614 RVA: 0x00049714 File Offset: 0x00047914
		public bool HasDisplayname
		{
			get
			{
				return this.hasDisplayname;
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x060015EF RID: 5615 RVA: 0x0004971C File Offset: 0x0004791C
		public string Displayname
		{
			get
			{
				return this.displayname_;
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x060015F0 RID: 5616 RVA: 0x00049724 File Offset: 0x00047924
		public bool HasUsergroup
		{
			get
			{
				return this.hasUsergroup;
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x060015F1 RID: 5617 RVA: 0x0004972C File Offset: 0x0004792C
		public RustProto.User.Types.UserGroup Usergroup
		{
			get
			{
				return this.usergroup_;
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x060015F2 RID: 5618 RVA: 0x00049734 File Offset: 0x00047934
		public override bool IsInitialized
		{
			get
			{
				return this.hasUserid && this.hasDisplayname && this.hasUsergroup;
			}
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x0004976C File Offset: 0x0004796C
		public override void WriteTo(ICodedOutputStream output)
		{
			int serializedSize = this.SerializedSize;
			string[] userFieldNames = RustProto.User._userFieldNames;
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

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x060015F4 RID: 5620 RVA: 0x000497F0 File Offset: 0x000479F0
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

		// Token: 0x060015F5 RID: 5621 RVA: 0x00049874 File Offset: 0x00047A74
		public static RustProto.User ParseFrom(ByteString data)
		{
			return RustProto.User.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x00049888 File Offset: 0x00047A88
		public static RustProto.User ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return RustProto.User.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x0004989C File Offset: 0x00047A9C
		public static RustProto.User ParseFrom(byte[] data)
		{
			return RustProto.User.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x000498B0 File Offset: 0x00047AB0
		public static RustProto.User ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return RustProto.User.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x000498C4 File Offset: 0x00047AC4
		public static RustProto.User ParseFrom(Stream input)
		{
			return RustProto.User.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x000498D8 File Offset: 0x00047AD8
		public static RustProto.User ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return RustProto.User.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x000498EC File Offset: 0x00047AEC
		public static RustProto.User ParseDelimitedFrom(Stream input)
		{
			return RustProto.User.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x00049900 File Offset: 0x00047B00
		public static RustProto.User ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return RustProto.User.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x00049914 File Offset: 0x00047B14
		public static RustProto.User ParseFrom(ICodedInputStream input)
		{
			return RustProto.User.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x00049928 File Offset: 0x00047B28
		public static RustProto.User ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return RustProto.User.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x0004993C File Offset: 0x00047B3C
		private RustProto.User MakeReadOnly()
		{
			return this;
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x00049940 File Offset: 0x00047B40
		public static RustProto.User.Builder CreateBuilder()
		{
			return new RustProto.User.Builder();
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x00049948 File Offset: 0x00047B48
		public override RustProto.User.Builder ToBuilder()
		{
			return RustProto.User.CreateBuilder(this);
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x00049950 File Offset: 0x00047B50
		public override RustProto.User.Builder CreateBuilderForType()
		{
			return new RustProto.User.Builder();
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x00049958 File Offset: 0x00047B58
		public static RustProto.User.Builder CreateBuilder(RustProto.User prototype)
		{
			return new RustProto.User.Builder(prototype);
		}

		// Token: 0x04000B68 RID: 2920
		public const int UseridFieldNumber = 1;

		// Token: 0x04000B69 RID: 2921
		public const int DisplaynameFieldNumber = 2;

		// Token: 0x04000B6A RID: 2922
		public const int UsergroupFieldNumber = 3;

		// Token: 0x04000B6B RID: 2923
		private static readonly RustProto.User defaultInstance = new RustProto.User().MakeReadOnly();

		// Token: 0x04000B6C RID: 2924
		private static readonly string[] _userFieldNames = new string[]
		{
			"displayname",
			"usergroup",
			"userid"
		};

		// Token: 0x04000B6D RID: 2925
		private static readonly uint[] _userFieldTags = new uint[]
		{
			18u,
			24u,
			8u
		};

		// Token: 0x04000B6E RID: 2926
		private bool hasUserid;

		// Token: 0x04000B6F RID: 2927
		private ulong userid_;

		// Token: 0x04000B70 RID: 2928
		private bool hasDisplayname;

		// Token: 0x04000B71 RID: 2929
		private string displayname_ = string.Empty;

		// Token: 0x04000B72 RID: 2930
		private bool hasUsergroup;

		// Token: 0x04000B73 RID: 2931
		private RustProto.User.Types.UserGroup usergroup_;

		// Token: 0x04000B74 RID: 2932
		private int memoizedSerializedSize = -1;

		// Token: 0x02000268 RID: 616
		[DebuggerNonUserCode]
		public static class Types
		{
			// Token: 0x02000269 RID: 617
			public enum UserGroup
			{
				// Token: 0x04000B76 RID: 2934
				REGULAR,
				// Token: 0x04000B77 RID: 2935
				BANNED,
				// Token: 0x04000B78 RID: 2936
				ADMIN
			}
		}

		// Token: 0x0200026A RID: 618
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<RustProto.User, RustProto.User.Builder>
		{
			// Token: 0x06001604 RID: 5636 RVA: 0x00049960 File Offset: 0x00047B60
			public Builder()
			{
				this.result = RustProto.User.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x06001605 RID: 5637 RVA: 0x0004997C File Offset: 0x00047B7C
			internal Builder(RustProto.User cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x1700063E RID: 1598
			// (get) Token: 0x06001606 RID: 5638 RVA: 0x00049994 File Offset: 0x00047B94
			protected override RustProto.User.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x06001607 RID: 5639 RVA: 0x00049998 File Offset: 0x00047B98
			private RustProto.User PrepareBuilder()
			{
				if (this.resultIsReadOnly)
				{
					RustProto.User other = this.result;
					this.result = new RustProto.User();
					this.resultIsReadOnly = false;
					this.MergeFrom(other);
				}
				return this.result;
			}

			// Token: 0x1700063F RID: 1599
			// (get) Token: 0x06001608 RID: 5640 RVA: 0x000499D8 File Offset: 0x00047BD8
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x17000640 RID: 1600
			// (get) Token: 0x06001609 RID: 5641 RVA: 0x000499E8 File Offset: 0x00047BE8
			protected override RustProto.User MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x0600160A RID: 5642 RVA: 0x000499F0 File Offset: 0x00047BF0
			public override RustProto.User.Builder Clear()
			{
				this.result = RustProto.User.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x0600160B RID: 5643 RVA: 0x00049A08 File Offset: 0x00047C08
			public override RustProto.User.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new RustProto.User.Builder(this.result);
				}
				return new RustProto.User.Builder().MergeFrom(this.result);
			}

			// Token: 0x17000641 RID: 1601
			// (get) Token: 0x0600160C RID: 5644 RVA: 0x00049A34 File Offset: 0x00047C34
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return RustProto.User.Descriptor;
				}
			}

			// Token: 0x17000642 RID: 1602
			// (get) Token: 0x0600160D RID: 5645 RVA: 0x00049A3C File Offset: 0x00047C3C
			public override RustProto.User DefaultInstanceForType
			{
				get
				{
					return RustProto.User.DefaultInstance;
				}
			}

			// Token: 0x0600160E RID: 5646 RVA: 0x00049A44 File Offset: 0x00047C44
			public override RustProto.User BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x0600160F RID: 5647 RVA: 0x00049A78 File Offset: 0x00047C78
			public override RustProto.User.Builder MergeFrom(IMessage other)
			{
				if (other is RustProto.User)
				{
					return this.MergeFrom((RustProto.User)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x06001610 RID: 5648 RVA: 0x00049A9C File Offset: 0x00047C9C
			public override RustProto.User.Builder MergeFrom(RustProto.User other)
			{
				if (other == RustProto.User.DefaultInstance)
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

			// Token: 0x06001611 RID: 5649 RVA: 0x00049B10 File Offset: 0x00047D10
			public override RustProto.User.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x06001612 RID: 5650 RVA: 0x00049B20 File Offset: 0x00047D20
			public override RustProto.User.Builder MergeFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
			{
				this.PrepareBuilder();
				UnknownFieldSet.Builder builder = null;
				uint num;
				string text;
				while (input.ReadTag(ref num, ref text))
				{
					if (num == 0u && text != null)
					{
						int num2 = Array.BinarySearch<string>(RustProto.User._userFieldNames, text, StringComparer.Ordinal);
						if (num2 < 0)
						{
							if (builder == null)
							{
								builder = UnknownFieldSet.CreateBuilder(this.UnknownFields);
							}
							this.ParseUnknownField(input, builder, extensionRegistry, num, text);
							continue;
						}
						num = RustProto.User._userFieldTags[num2];
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
							else if (input.ReadEnum<RustProto.User.Types.UserGroup>(ref this.result.usergroup_, ref obj))
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

			// Token: 0x17000643 RID: 1603
			// (get) Token: 0x06001613 RID: 5651 RVA: 0x00049CC4 File Offset: 0x00047EC4
			public bool HasUserid
			{
				get
				{
					return this.result.hasUserid;
				}
			}

			// Token: 0x17000644 RID: 1604
			// (get) Token: 0x06001614 RID: 5652 RVA: 0x00049CD4 File Offset: 0x00047ED4
			// (set) Token: 0x06001615 RID: 5653 RVA: 0x00049CE4 File Offset: 0x00047EE4
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

			// Token: 0x06001616 RID: 5654 RVA: 0x00049CF0 File Offset: 0x00047EF0
			[CLSCompliant(false)]
			public RustProto.User.Builder SetUserid(ulong value)
			{
				this.PrepareBuilder();
				this.result.hasUserid = true;
				this.result.userid_ = value;
				return this;
			}

			// Token: 0x06001617 RID: 5655 RVA: 0x00049D20 File Offset: 0x00047F20
			public RustProto.User.Builder ClearUserid()
			{
				this.PrepareBuilder();
				this.result.hasUserid = false;
				this.result.userid_ = 0UL;
				return this;
			}

			// Token: 0x17000645 RID: 1605
			// (get) Token: 0x06001618 RID: 5656 RVA: 0x00049D44 File Offset: 0x00047F44
			public bool HasDisplayname
			{
				get
				{
					return this.result.hasDisplayname;
				}
			}

			// Token: 0x17000646 RID: 1606
			// (get) Token: 0x06001619 RID: 5657 RVA: 0x00049D54 File Offset: 0x00047F54
			// (set) Token: 0x0600161A RID: 5658 RVA: 0x00049D64 File Offset: 0x00047F64
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

			// Token: 0x0600161B RID: 5659 RVA: 0x00049D70 File Offset: 0x00047F70
			public RustProto.User.Builder SetDisplayname(string value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasDisplayname = true;
				this.result.displayname_ = value;
				return this;
			}

			// Token: 0x0600161C RID: 5660 RVA: 0x00049DA0 File Offset: 0x00047FA0
			public RustProto.User.Builder ClearDisplayname()
			{
				this.PrepareBuilder();
				this.result.hasDisplayname = false;
				this.result.displayname_ = string.Empty;
				return this;
			}

			// Token: 0x17000647 RID: 1607
			// (get) Token: 0x0600161D RID: 5661 RVA: 0x00049DD4 File Offset: 0x00047FD4
			public bool HasUsergroup
			{
				get
				{
					return this.result.hasUsergroup;
				}
			}

			// Token: 0x17000648 RID: 1608
			// (get) Token: 0x0600161E RID: 5662 RVA: 0x00049DE4 File Offset: 0x00047FE4
			// (set) Token: 0x0600161F RID: 5663 RVA: 0x00049DF4 File Offset: 0x00047FF4
			public RustProto.User.Types.UserGroup Usergroup
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

			// Token: 0x06001620 RID: 5664 RVA: 0x00049E00 File Offset: 0x00048000
			public RustProto.User.Builder SetUsergroup(RustProto.User.Types.UserGroup value)
			{
				this.PrepareBuilder();
				this.result.hasUsergroup = true;
				this.result.usergroup_ = value;
				return this;
			}

			// Token: 0x06001621 RID: 5665 RVA: 0x00049E30 File Offset: 0x00048030
			public RustProto.User.Builder ClearUsergroup()
			{
				this.PrepareBuilder();
				this.result.hasUsergroup = false;
				this.result.usergroup_ = RustProto.User.Types.UserGroup.REGULAR;
				return this;
			}

			// Token: 0x04000B79 RID: 2937
			private bool resultIsReadOnly;

			// Token: 0x04000B7A RID: 2938
			private RustProto.User result;
		}
	}
}
