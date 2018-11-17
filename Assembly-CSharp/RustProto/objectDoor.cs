using System;
using System.Diagnostics;
using System.IO;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;
using RustProto.Helpers;

namespace RustProto
{
	// Token: 0x02000246 RID: 582
	[DebuggerNonUserCode]
	public sealed class objectDoor : GeneratedMessage<objectDoor, objectDoor.Builder>
	{
		// Token: 0x0600130D RID: 4877 RVA: 0x00043AE8 File Offset: 0x00041CE8
		private objectDoor()
		{
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x00043AF8 File Offset: 0x00041CF8
		static objectDoor()
		{
			object.ReferenceEquals(Worldsave.Descriptor, null);
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x00043B50 File Offset: 0x00041D50
		public static RustProto.Helpers.Recycler<objectDoor, objectDoor.Builder> Recycler()
		{
			return RustProto.Helpers.Recycler<objectDoor, objectDoor.Builder>.Manufacture();
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06001310 RID: 4880 RVA: 0x00043B58 File Offset: 0x00041D58
		public static objectDoor DefaultInstance
		{
			get
			{
				return objectDoor.defaultInstance;
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06001311 RID: 4881 RVA: 0x00043B60 File Offset: 0x00041D60
		public override objectDoor DefaultInstanceForType
		{
			get
			{
				return objectDoor.DefaultInstance;
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06001312 RID: 4882 RVA: 0x00043B68 File Offset: 0x00041D68
		protected override objectDoor ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06001313 RID: 4883 RVA: 0x00043B6C File Offset: 0x00041D6C
		public static MessageDescriptor Descriptor
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectDoor__Descriptor;
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06001314 RID: 4884 RVA: 0x00043B74 File Offset: 0x00041D74
		protected override FieldAccessorTable<objectDoor, objectDoor.Builder> InternalFieldAccessors
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectDoor__FieldAccessorTable;
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06001315 RID: 4885 RVA: 0x00043B7C File Offset: 0x00041D7C
		public bool HasState
		{
			get
			{
				return this.hasState;
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06001316 RID: 4886 RVA: 0x00043B84 File Offset: 0x00041D84
		public int State
		{
			get
			{
				return this.state_;
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06001317 RID: 4887 RVA: 0x00043B8C File Offset: 0x00041D8C
		public bool HasOpen
		{
			get
			{
				return this.hasOpen;
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06001318 RID: 4888 RVA: 0x00043B94 File Offset: 0x00041D94
		public bool Open
		{
			get
			{
				return this.open_;
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06001319 RID: 4889 RVA: 0x00043B9C File Offset: 0x00041D9C
		public override bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x00043BA0 File Offset: 0x00041DA0
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

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x0600131B RID: 4891 RVA: 0x00043BFC File Offset: 0x00041DFC
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

		// Token: 0x0600131C RID: 4892 RVA: 0x00043C68 File Offset: 0x00041E68
		public static objectDoor ParseFrom(ByteString data)
		{
			return objectDoor.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x00043C7C File Offset: 0x00041E7C
		public static objectDoor ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return objectDoor.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x00043C90 File Offset: 0x00041E90
		public static objectDoor ParseFrom(byte[] data)
		{
			return objectDoor.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x00043CA4 File Offset: 0x00041EA4
		public static objectDoor ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return objectDoor.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x00043CB8 File Offset: 0x00041EB8
		public static objectDoor ParseFrom(Stream input)
		{
			return objectDoor.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x00043CCC File Offset: 0x00041ECC
		public static objectDoor ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectDoor.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x00043CE0 File Offset: 0x00041EE0
		public static objectDoor ParseDelimitedFrom(Stream input)
		{
			return objectDoor.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x00043CF4 File Offset: 0x00041EF4
		public static objectDoor ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectDoor.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x00043D08 File Offset: 0x00041F08
		public static objectDoor ParseFrom(ICodedInputStream input)
		{
			return objectDoor.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x00043D1C File Offset: 0x00041F1C
		public static objectDoor ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return objectDoor.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x00043D30 File Offset: 0x00041F30
		private objectDoor MakeReadOnly()
		{
			return this;
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x00043D34 File Offset: 0x00041F34
		public static objectDoor.Builder CreateBuilder()
		{
			return new objectDoor.Builder();
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x00043D3C File Offset: 0x00041F3C
		public override objectDoor.Builder ToBuilder()
		{
			return objectDoor.CreateBuilder(this);
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x00043D44 File Offset: 0x00041F44
		public override objectDoor.Builder CreateBuilderForType()
		{
			return new objectDoor.Builder();
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x00043D4C File Offset: 0x00041F4C
		public static objectDoor.Builder CreateBuilder(objectDoor prototype)
		{
			return new objectDoor.Builder(prototype);
		}

		// Token: 0x04000AA4 RID: 2724
		public const int StateFieldNumber = 1;

		// Token: 0x04000AA5 RID: 2725
		public const int OpenFieldNumber = 2;

		// Token: 0x04000AA6 RID: 2726
		private static readonly objectDoor defaultInstance = new objectDoor().MakeReadOnly();

		// Token: 0x04000AA7 RID: 2727
		private static readonly string[] _objectDoorFieldNames = new string[]
		{
			"Open",
			"State"
		};

		// Token: 0x04000AA8 RID: 2728
		private static readonly uint[] _objectDoorFieldTags = new uint[]
		{
			16u,
			8u
		};

		// Token: 0x04000AA9 RID: 2729
		private bool hasState;

		// Token: 0x04000AAA RID: 2730
		private int state_;

		// Token: 0x04000AAB RID: 2731
		private bool hasOpen;

		// Token: 0x04000AAC RID: 2732
		private bool open_;

		// Token: 0x04000AAD RID: 2733
		private int memoizedSerializedSize = -1;

		// Token: 0x02000247 RID: 583
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<objectDoor, objectDoor.Builder>
		{
			// Token: 0x0600132B RID: 4907 RVA: 0x00043D54 File Offset: 0x00041F54
			public Builder()
			{
				this.result = objectDoor.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x0600132C RID: 4908 RVA: 0x00043D70 File Offset: 0x00041F70
			internal Builder(objectDoor cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x17000531 RID: 1329
			// (get) Token: 0x0600132D RID: 4909 RVA: 0x00043D88 File Offset: 0x00041F88
			protected override objectDoor.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x0600132E RID: 4910 RVA: 0x00043D8C File Offset: 0x00041F8C
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

			// Token: 0x17000532 RID: 1330
			// (get) Token: 0x0600132F RID: 4911 RVA: 0x00043DCC File Offset: 0x00041FCC
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x17000533 RID: 1331
			// (get) Token: 0x06001330 RID: 4912 RVA: 0x00043DDC File Offset: 0x00041FDC
			protected override objectDoor MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x06001331 RID: 4913 RVA: 0x00043DE4 File Offset: 0x00041FE4
			public override objectDoor.Builder Clear()
			{
				this.result = objectDoor.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x06001332 RID: 4914 RVA: 0x00043DFC File Offset: 0x00041FFC
			public override objectDoor.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new objectDoor.Builder(this.result);
				}
				return new objectDoor.Builder().MergeFrom(this.result);
			}

			// Token: 0x17000534 RID: 1332
			// (get) Token: 0x06001333 RID: 4915 RVA: 0x00043E28 File Offset: 0x00042028
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return objectDoor.Descriptor;
				}
			}

			// Token: 0x17000535 RID: 1333
			// (get) Token: 0x06001334 RID: 4916 RVA: 0x00043E30 File Offset: 0x00042030
			public override objectDoor DefaultInstanceForType
			{
				get
				{
					return objectDoor.DefaultInstance;
				}
			}

			// Token: 0x06001335 RID: 4917 RVA: 0x00043E38 File Offset: 0x00042038
			public override objectDoor BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x06001336 RID: 4918 RVA: 0x00043E6C File Offset: 0x0004206C
			public override objectDoor.Builder MergeFrom(IMessage other)
			{
				if (other is objectDoor)
				{
					return this.MergeFrom((objectDoor)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x06001337 RID: 4919 RVA: 0x00043E90 File Offset: 0x00042090
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

			// Token: 0x06001338 RID: 4920 RVA: 0x00043EF0 File Offset: 0x000420F0
			public override objectDoor.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x06001339 RID: 4921 RVA: 0x00043F00 File Offset: 0x00042100
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

			// Token: 0x17000536 RID: 1334
			// (get) Token: 0x0600133A RID: 4922 RVA: 0x0004403C File Offset: 0x0004223C
			public bool HasState
			{
				get
				{
					return this.result.hasState;
				}
			}

			// Token: 0x17000537 RID: 1335
			// (get) Token: 0x0600133B RID: 4923 RVA: 0x0004404C File Offset: 0x0004224C
			// (set) Token: 0x0600133C RID: 4924 RVA: 0x0004405C File Offset: 0x0004225C
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

			// Token: 0x0600133D RID: 4925 RVA: 0x00044068 File Offset: 0x00042268
			public objectDoor.Builder SetState(int value)
			{
				this.PrepareBuilder();
				this.result.hasState = true;
				this.result.state_ = value;
				return this;
			}

			// Token: 0x0600133E RID: 4926 RVA: 0x00044098 File Offset: 0x00042298
			public objectDoor.Builder ClearState()
			{
				this.PrepareBuilder();
				this.result.hasState = false;
				this.result.state_ = 0;
				return this;
			}

			// Token: 0x17000538 RID: 1336
			// (get) Token: 0x0600133F RID: 4927 RVA: 0x000440C8 File Offset: 0x000422C8
			public bool HasOpen
			{
				get
				{
					return this.result.hasOpen;
				}
			}

			// Token: 0x17000539 RID: 1337
			// (get) Token: 0x06001340 RID: 4928 RVA: 0x000440D8 File Offset: 0x000422D8
			// (set) Token: 0x06001341 RID: 4929 RVA: 0x000440E8 File Offset: 0x000422E8
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

			// Token: 0x06001342 RID: 4930 RVA: 0x000440F4 File Offset: 0x000422F4
			public objectDoor.Builder SetOpen(bool value)
			{
				this.PrepareBuilder();
				this.result.hasOpen = true;
				this.result.open_ = value;
				return this;
			}

			// Token: 0x06001343 RID: 4931 RVA: 0x00044124 File Offset: 0x00042324
			public objectDoor.Builder ClearOpen()
			{
				this.PrepareBuilder();
				this.result.hasOpen = false;
				this.result.open_ = false;
				return this;
			}

			// Token: 0x04000AAE RID: 2734
			private bool resultIsReadOnly;

			// Token: 0x04000AAF RID: 2735
			private objectDoor result;
		}
	}
}
