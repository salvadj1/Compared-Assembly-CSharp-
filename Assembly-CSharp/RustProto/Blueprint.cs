using System;
using System.Diagnostics;
using System.IO;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;
using RustProto.Helpers;
using RustProto.Proto;

namespace RustProto
{
	// Token: 0x02000205 RID: 517
	[DebuggerNonUserCode]
	public sealed class Blueprint : GeneratedMessage<Blueprint, Blueprint.Builder>
	{
		// Token: 0x06000F85 RID: 3973 RVA: 0x0003A25C File Offset: 0x0003845C
		private Blueprint()
		{
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x0003A26C File Offset: 0x0003846C
		static Blueprint()
		{
			object.ReferenceEquals(Blueprint.Descriptor, null);
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x0003A2AC File Offset: 0x000384AC
		public static Recycler<Blueprint, Blueprint.Builder> Recycler()
		{
			return Recycler<Blueprint, Blueprint.Builder>.Manufacture();
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000F88 RID: 3976 RVA: 0x0003A2B4 File Offset: 0x000384B4
		public static Blueprint DefaultInstance
		{
			get
			{
				return Blueprint.defaultInstance;
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000F89 RID: 3977 RVA: 0x0003A2BC File Offset: 0x000384BC
		public override Blueprint DefaultInstanceForType
		{
			get
			{
				return Blueprint.DefaultInstance;
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000F8A RID: 3978 RVA: 0x0003A2C4 File Offset: 0x000384C4
		protected override Blueprint ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000F8B RID: 3979 RVA: 0x0003A2C8 File Offset: 0x000384C8
		public static MessageDescriptor Descriptor
		{
			get
			{
				return Blueprint.internal__static_RustProto_Blueprint__Descriptor;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000F8C RID: 3980 RVA: 0x0003A2D0 File Offset: 0x000384D0
		protected override FieldAccessorTable<Blueprint, Blueprint.Builder> InternalFieldAccessors
		{
			get
			{
				return Blueprint.internal__static_RustProto_Blueprint__FieldAccessorTable;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000F8D RID: 3981 RVA: 0x0003A2D8 File Offset: 0x000384D8
		public bool HasId
		{
			get
			{
				return this.hasId;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000F8E RID: 3982 RVA: 0x0003A2E0 File Offset: 0x000384E0
		public int Id
		{
			get
			{
				return this.id_;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000F8F RID: 3983 RVA: 0x0003A2E8 File Offset: 0x000384E8
		public override bool IsInitialized
		{
			get
			{
				return this.hasId;
			}
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x0003A2F8 File Offset: 0x000384F8
		public override void WriteTo(ICodedOutputStream output)
		{
			int serializedSize = this.SerializedSize;
			string[] blueprintFieldNames = Blueprint._blueprintFieldNames;
			if (this.hasId)
			{
				output.WriteInt32(1, blueprintFieldNames[0], this.Id);
			}
			this.UnknownFields.WriteTo(output);
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000F91 RID: 3985 RVA: 0x0003A33C File Offset: 0x0003853C
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
				if (this.hasId)
				{
					num += CodedOutputStream.ComputeInt32Size(1, this.Id);
				}
				num += this.UnknownFields.SerializedSize;
				this.memoizedSerializedSize = num;
				return num;
			}
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x0003A38C File Offset: 0x0003858C
		public static Blueprint ParseFrom(ByteString data)
		{
			return Blueprint.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x0003A3A0 File Offset: 0x000385A0
		public static Blueprint ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return Blueprint.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x0003A3B4 File Offset: 0x000385B4
		public static Blueprint ParseFrom(byte[] data)
		{
			return Blueprint.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x0003A3C8 File Offset: 0x000385C8
		public static Blueprint ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return Blueprint.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x0003A3DC File Offset: 0x000385DC
		public static Blueprint ParseFrom(Stream input)
		{
			return Blueprint.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x0003A3F0 File Offset: 0x000385F0
		public static Blueprint ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return Blueprint.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x0003A404 File Offset: 0x00038604
		public static Blueprint ParseDelimitedFrom(Stream input)
		{
			return Blueprint.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x0003A418 File Offset: 0x00038618
		public static Blueprint ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return Blueprint.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x0003A42C File Offset: 0x0003862C
		public static Blueprint ParseFrom(ICodedInputStream input)
		{
			return Blueprint.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x0003A440 File Offset: 0x00038640
		public static Blueprint ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return Blueprint.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x0003A454 File Offset: 0x00038654
		private Blueprint MakeReadOnly()
		{
			return this;
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x0003A458 File Offset: 0x00038658
		public static Blueprint.Builder CreateBuilder()
		{
			return new Blueprint.Builder();
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x0003A460 File Offset: 0x00038660
		public override Blueprint.Builder ToBuilder()
		{
			return Blueprint.CreateBuilder(this);
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x0003A468 File Offset: 0x00038668
		public override Blueprint.Builder CreateBuilderForType()
		{
			return new Blueprint.Builder();
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x0003A470 File Offset: 0x00038670
		public static Blueprint.Builder CreateBuilder(Blueprint prototype)
		{
			return new Blueprint.Builder(prototype);
		}

		// Token: 0x040008F2 RID: 2290
		public const int IdFieldNumber = 1;

		// Token: 0x040008F3 RID: 2291
		private static readonly Blueprint defaultInstance = new Blueprint().MakeReadOnly();

		// Token: 0x040008F4 RID: 2292
		private static readonly string[] _blueprintFieldNames = new string[]
		{
			"id"
		};

		// Token: 0x040008F5 RID: 2293
		private static readonly uint[] _blueprintFieldTags = new uint[]
		{
			8u
		};

		// Token: 0x040008F6 RID: 2294
		private bool hasId;

		// Token: 0x040008F7 RID: 2295
		private int id_;

		// Token: 0x040008F8 RID: 2296
		private int memoizedSerializedSize = -1;

		// Token: 0x02000206 RID: 518
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<Blueprint, Blueprint.Builder>
		{
			// Token: 0x06000FA1 RID: 4001 RVA: 0x0003A478 File Offset: 0x00038678
			public Builder()
			{
				this.result = Blueprint.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x06000FA2 RID: 4002 RVA: 0x0003A494 File Offset: 0x00038694
			internal Builder(Blueprint cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x1700040B RID: 1035
			// (get) Token: 0x06000FA3 RID: 4003 RVA: 0x0003A4AC File Offset: 0x000386AC
			protected override Blueprint.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x06000FA4 RID: 4004 RVA: 0x0003A4B0 File Offset: 0x000386B0
			private Blueprint PrepareBuilder()
			{
				if (this.resultIsReadOnly)
				{
					Blueprint other = this.result;
					this.result = new Blueprint();
					this.resultIsReadOnly = false;
					this.MergeFrom(other);
				}
				return this.result;
			}

			// Token: 0x1700040C RID: 1036
			// (get) Token: 0x06000FA5 RID: 4005 RVA: 0x0003A4F0 File Offset: 0x000386F0
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x1700040D RID: 1037
			// (get) Token: 0x06000FA6 RID: 4006 RVA: 0x0003A500 File Offset: 0x00038700
			protected override Blueprint MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x06000FA7 RID: 4007 RVA: 0x0003A508 File Offset: 0x00038708
			public override Blueprint.Builder Clear()
			{
				this.result = Blueprint.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x06000FA8 RID: 4008 RVA: 0x0003A520 File Offset: 0x00038720
			public override Blueprint.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new Blueprint.Builder(this.result);
				}
				return new Blueprint.Builder().MergeFrom(this.result);
			}

			// Token: 0x1700040E RID: 1038
			// (get) Token: 0x06000FA9 RID: 4009 RVA: 0x0003A54C File Offset: 0x0003874C
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return Blueprint.Descriptor;
				}
			}

			// Token: 0x1700040F RID: 1039
			// (get) Token: 0x06000FAA RID: 4010 RVA: 0x0003A554 File Offset: 0x00038754
			public override Blueprint DefaultInstanceForType
			{
				get
				{
					return Blueprint.DefaultInstance;
				}
			}

			// Token: 0x06000FAB RID: 4011 RVA: 0x0003A55C File Offset: 0x0003875C
			public override Blueprint BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x06000FAC RID: 4012 RVA: 0x0003A590 File Offset: 0x00038790
			public override Blueprint.Builder MergeFrom(IMessage other)
			{
				if (other is Blueprint)
				{
					return this.MergeFrom((Blueprint)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x06000FAD RID: 4013 RVA: 0x0003A5B4 File Offset: 0x000387B4
			public override Blueprint.Builder MergeFrom(Blueprint other)
			{
				if (other == Blueprint.DefaultInstance)
				{
					return this;
				}
				this.PrepareBuilder();
				if (other.HasId)
				{
					this.Id = other.Id;
				}
				this.MergeUnknownFields(other.UnknownFields);
				return this;
			}

			// Token: 0x06000FAE RID: 4014 RVA: 0x0003A5FC File Offset: 0x000387FC
			public override Blueprint.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x06000FAF RID: 4015 RVA: 0x0003A60C File Offset: 0x0003880C
			public override Blueprint.Builder MergeFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
			{
				this.PrepareBuilder();
				UnknownFieldSet.Builder builder = null;
				uint num;
				string text;
				while (input.ReadTag(ref num, ref text))
				{
					if (num == 0u && text != null)
					{
						int num2 = Array.BinarySearch<string>(Blueprint._blueprintFieldNames, text, StringComparer.Ordinal);
						if (num2 < 0)
						{
							if (builder == null)
							{
								builder = UnknownFieldSet.CreateBuilder(this.UnknownFields);
							}
							this.ParseUnknownField(input, builder, extensionRegistry, num, text);
							continue;
						}
						num = Blueprint._blueprintFieldTags[num2];
					}
					uint num3 = num;
					if (num3 == 0u)
					{
						throw InvalidProtocolBufferException.InvalidTag();
					}
					if (num3 != 8u)
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
						this.result.hasId = input.ReadInt32(ref this.result.id_);
					}
				}
				if (builder != null)
				{
					this.UnknownFields = builder.Build();
				}
				return this;
			}

			// Token: 0x17000410 RID: 1040
			// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x0003A720 File Offset: 0x00038920
			public bool HasId
			{
				get
				{
					return this.result.hasId;
				}
			}

			// Token: 0x17000411 RID: 1041
			// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x0003A730 File Offset: 0x00038930
			// (set) Token: 0x06000FB2 RID: 4018 RVA: 0x0003A740 File Offset: 0x00038940
			public int Id
			{
				get
				{
					return this.result.Id;
				}
				set
				{
					this.SetId(value);
				}
			}

			// Token: 0x06000FB3 RID: 4019 RVA: 0x0003A74C File Offset: 0x0003894C
			public Blueprint.Builder SetId(int value)
			{
				this.PrepareBuilder();
				this.result.hasId = true;
				this.result.id_ = value;
				return this;
			}

			// Token: 0x06000FB4 RID: 4020 RVA: 0x0003A77C File Offset: 0x0003897C
			public Blueprint.Builder ClearId()
			{
				this.PrepareBuilder();
				this.result.hasId = false;
				this.result.id_ = 0;
				return this;
			}

			// Token: 0x040008F9 RID: 2297
			private bool resultIsReadOnly;

			// Token: 0x040008FA RID: 2298
			private Blueprint result;
		}
	}
}
