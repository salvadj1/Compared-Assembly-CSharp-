using System;
using System.Diagnostics;
using System.IO;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;
using RustProto.Proto;

namespace RustProto
{
	// Token: 0x0200022D RID: 557
	[DebuggerNonUserCode]
	public sealed class Error : GeneratedMessage<Error, Error.Builder>
	{
		// Token: 0x06001419 RID: 5145 RVA: 0x00044350 File Offset: 0x00042550
		private Error()
		{
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x00044378 File Offset: 0x00042578
		static Error()
		{
			object.ReferenceEquals(Error.Descriptor, null);
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x0600141B RID: 5147 RVA: 0x000443D0 File Offset: 0x000425D0
		public static Error DefaultInstance
		{
			get
			{
				return Error.defaultInstance;
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x0600141C RID: 5148 RVA: 0x000443D8 File Offset: 0x000425D8
		public override Error DefaultInstanceForType
		{
			get
			{
				return Error.DefaultInstance;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x0600141D RID: 5149 RVA: 0x000443E0 File Offset: 0x000425E0
		protected override Error ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x0600141E RID: 5150 RVA: 0x000443E4 File Offset: 0x000425E4
		public static MessageDescriptor Descriptor
		{
			get
			{
				return Error.internal__static_RustProto_Error__Descriptor;
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x0600141F RID: 5151 RVA: 0x000443EC File Offset: 0x000425EC
		protected override FieldAccessorTable<Error, Error.Builder> InternalFieldAccessors
		{
			get
			{
				return Error.internal__static_RustProto_Error__FieldAccessorTable;
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06001420 RID: 5152 RVA: 0x000443F4 File Offset: 0x000425F4
		public bool HasStatus
		{
			get
			{
				return this.hasStatus;
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06001421 RID: 5153 RVA: 0x000443FC File Offset: 0x000425FC
		public string Status
		{
			get
			{
				return this.status_;
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06001422 RID: 5154 RVA: 0x00044404 File Offset: 0x00042604
		public bool HasMessage
		{
			get
			{
				return this.hasMessage;
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06001423 RID: 5155 RVA: 0x0004440C File Offset: 0x0004260C
		public string Message
		{
			get
			{
				return this.message_;
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06001424 RID: 5156 RVA: 0x00044414 File Offset: 0x00042614
		public override bool IsInitialized
		{
			get
			{
				return this.hasStatus && this.hasMessage;
			}
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x00044434 File Offset: 0x00042634
		public override void WriteTo(ICodedOutputStream output)
		{
			int serializedSize = this.SerializedSize;
			string[] errorFieldNames = Error._errorFieldNames;
			if (this.hasStatus)
			{
				output.WriteString(1, errorFieldNames[1], this.Status);
			}
			if (this.hasMessage)
			{
				output.WriteString(2, errorFieldNames[0], this.Message);
			}
			this.UnknownFields.WriteTo(output);
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06001426 RID: 5158 RVA: 0x00044490 File Offset: 0x00042690
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
				if (this.hasStatus)
				{
					num += CodedOutputStream.ComputeStringSize(1, this.Status);
				}
				if (this.hasMessage)
				{
					num += CodedOutputStream.ComputeStringSize(2, this.Message);
				}
				num += this.UnknownFields.SerializedSize;
				this.memoizedSerializedSize = num;
				return num;
			}
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x000444FC File Offset: 0x000426FC
		public static Error ParseFrom(ByteString data)
		{
			return Error.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x00044510 File Offset: 0x00042710
		public static Error ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return Error.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x00044524 File Offset: 0x00042724
		public static Error ParseFrom(byte[] data)
		{
			return Error.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x00044538 File Offset: 0x00042738
		public static Error ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return Error.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x0004454C File Offset: 0x0004274C
		public static Error ParseFrom(Stream input)
		{
			return Error.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x00044560 File Offset: 0x00042760
		public static Error ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return Error.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x00044574 File Offset: 0x00042774
		public static Error ParseDelimitedFrom(Stream input)
		{
			return Error.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x00044588 File Offset: 0x00042788
		public static Error ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return Error.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x0004459C File Offset: 0x0004279C
		public static Error ParseFrom(ICodedInputStream input)
		{
			return Error.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x000445B0 File Offset: 0x000427B0
		public static Error ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return Error.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x000445C4 File Offset: 0x000427C4
		private Error MakeReadOnly()
		{
			return this;
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x000445C8 File Offset: 0x000427C8
		public static Error.Builder CreateBuilder()
		{
			return new Error.Builder();
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x000445D0 File Offset: 0x000427D0
		public override Error.Builder ToBuilder()
		{
			return Error.CreateBuilder(this);
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x000445D8 File Offset: 0x000427D8
		public override Error.Builder CreateBuilderForType()
		{
			return new Error.Builder();
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x000445E0 File Offset: 0x000427E0
		public static Error.Builder CreateBuilder(Error prototype)
		{
			return new Error.Builder(prototype);
		}

		// Token: 0x04000A21 RID: 2593
		public const int StatusFieldNumber = 1;

		// Token: 0x04000A22 RID: 2594
		public const int MessageFieldNumber = 2;

		// Token: 0x04000A23 RID: 2595
		private static readonly Error defaultInstance = new Error().MakeReadOnly();

		// Token: 0x04000A24 RID: 2596
		private static readonly string[] _errorFieldNames = new string[]
		{
			"message",
			"status"
		};

		// Token: 0x04000A25 RID: 2597
		private static readonly uint[] _errorFieldTags = new uint[]
		{
			18u,
			10u
		};

		// Token: 0x04000A26 RID: 2598
		private bool hasStatus;

		// Token: 0x04000A27 RID: 2599
		private string status_ = string.Empty;

		// Token: 0x04000A28 RID: 2600
		private bool hasMessage;

		// Token: 0x04000A29 RID: 2601
		private string message_ = string.Empty;

		// Token: 0x04000A2A RID: 2602
		private int memoizedSerializedSize = -1;

		// Token: 0x0200022E RID: 558
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<Error, Error.Builder>
		{
			// Token: 0x06001436 RID: 5174 RVA: 0x000445E8 File Offset: 0x000427E8
			public Builder()
			{
				this.result = Error.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x06001437 RID: 5175 RVA: 0x00044604 File Offset: 0x00042804
			internal Builder(Error cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x170005C9 RID: 1481
			// (get) Token: 0x06001438 RID: 5176 RVA: 0x0004461C File Offset: 0x0004281C
			protected override Error.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x06001439 RID: 5177 RVA: 0x00044620 File Offset: 0x00042820
			private Error PrepareBuilder()
			{
				if (this.resultIsReadOnly)
				{
					Error other = this.result;
					this.result = new Error();
					this.resultIsReadOnly = false;
					this.MergeFrom(other);
				}
				return this.result;
			}

			// Token: 0x170005CA RID: 1482
			// (get) Token: 0x0600143A RID: 5178 RVA: 0x00044660 File Offset: 0x00042860
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x170005CB RID: 1483
			// (get) Token: 0x0600143B RID: 5179 RVA: 0x00044670 File Offset: 0x00042870
			protected override Error MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x0600143C RID: 5180 RVA: 0x00044678 File Offset: 0x00042878
			public override Error.Builder Clear()
			{
				this.result = Error.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x0600143D RID: 5181 RVA: 0x00044690 File Offset: 0x00042890
			public override Error.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new Error.Builder(this.result);
				}
				return new Error.Builder().MergeFrom(this.result);
			}

			// Token: 0x170005CC RID: 1484
			// (get) Token: 0x0600143E RID: 5182 RVA: 0x000446BC File Offset: 0x000428BC
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return Error.Descriptor;
				}
			}

			// Token: 0x170005CD RID: 1485
			// (get) Token: 0x0600143F RID: 5183 RVA: 0x000446C4 File Offset: 0x000428C4
			public override Error DefaultInstanceForType
			{
				get
				{
					return Error.DefaultInstance;
				}
			}

			// Token: 0x06001440 RID: 5184 RVA: 0x000446CC File Offset: 0x000428CC
			public override Error BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x06001441 RID: 5185 RVA: 0x00044700 File Offset: 0x00042900
			public override Error.Builder MergeFrom(IMessage other)
			{
				if (other is Error)
				{
					return this.MergeFrom((Error)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x06001442 RID: 5186 RVA: 0x00044724 File Offset: 0x00042924
			public override Error.Builder MergeFrom(Error other)
			{
				if (other == Error.DefaultInstance)
				{
					return this;
				}
				this.PrepareBuilder();
				if (other.HasStatus)
				{
					this.Status = other.Status;
				}
				if (other.HasMessage)
				{
					this.Message = other.Message;
				}
				this.MergeUnknownFields(other.UnknownFields);
				return this;
			}

			// Token: 0x06001443 RID: 5187 RVA: 0x00044784 File Offset: 0x00042984
			public override Error.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x06001444 RID: 5188 RVA: 0x00044794 File Offset: 0x00042994
			public override Error.Builder MergeFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
			{
				this.PrepareBuilder();
				UnknownFieldSet.Builder builder = null;
				uint num;
				string text;
				while (input.ReadTag(ref num, ref text))
				{
					if (num == 0u && text != null)
					{
						int num2 = Array.BinarySearch<string>(Error._errorFieldNames, text, StringComparer.Ordinal);
						if (num2 < 0)
						{
							if (builder == null)
							{
								builder = UnknownFieldSet.CreateBuilder(this.UnknownFields);
							}
							this.ParseUnknownField(input, builder, extensionRegistry, num, text);
							continue;
						}
						num = Error._errorFieldTags[num2];
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
							this.result.hasMessage = input.ReadString(ref this.result.message_);
						}
					}
					else
					{
						this.result.hasStatus = input.ReadString(ref this.result.status_);
					}
				}
				if (builder != null)
				{
					this.UnknownFields = builder.Build();
				}
				return this;
			}

			// Token: 0x170005CE RID: 1486
			// (get) Token: 0x06001445 RID: 5189 RVA: 0x000448D4 File Offset: 0x00042AD4
			public bool HasStatus
			{
				get
				{
					return this.result.hasStatus;
				}
			}

			// Token: 0x170005CF RID: 1487
			// (get) Token: 0x06001446 RID: 5190 RVA: 0x000448E4 File Offset: 0x00042AE4
			// (set) Token: 0x06001447 RID: 5191 RVA: 0x000448F4 File Offset: 0x00042AF4
			public string Status
			{
				get
				{
					return this.result.Status;
				}
				set
				{
					this.SetStatus(value);
				}
			}

			// Token: 0x06001448 RID: 5192 RVA: 0x00044900 File Offset: 0x00042B00
			public Error.Builder SetStatus(string value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasStatus = true;
				this.result.status_ = value;
				return this;
			}

			// Token: 0x06001449 RID: 5193 RVA: 0x00044930 File Offset: 0x00042B30
			public Error.Builder ClearStatus()
			{
				this.PrepareBuilder();
				this.result.hasStatus = false;
				this.result.status_ = string.Empty;
				return this;
			}

			// Token: 0x170005D0 RID: 1488
			// (get) Token: 0x0600144A RID: 5194 RVA: 0x00044964 File Offset: 0x00042B64
			public bool HasMessage
			{
				get
				{
					return this.result.hasMessage;
				}
			}

			// Token: 0x170005D1 RID: 1489
			// (get) Token: 0x0600144B RID: 5195 RVA: 0x00044974 File Offset: 0x00042B74
			// (set) Token: 0x0600144C RID: 5196 RVA: 0x00044984 File Offset: 0x00042B84
			public string Message
			{
				get
				{
					return this.result.Message;
				}
				set
				{
					this.SetMessage(value);
				}
			}

			// Token: 0x0600144D RID: 5197 RVA: 0x00044990 File Offset: 0x00042B90
			public Error.Builder SetMessage(string value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasMessage = true;
				this.result.message_ = value;
				return this;
			}

			// Token: 0x0600144E RID: 5198 RVA: 0x000449C0 File Offset: 0x00042BC0
			public Error.Builder ClearMessage()
			{
				this.PrepareBuilder();
				this.result.hasMessage = false;
				this.result.message_ = string.Empty;
				return this;
			}

			// Token: 0x04000A2B RID: 2603
			private bool resultIsReadOnly;

			// Token: 0x04000A2C RID: 2604
			private Error result;
		}
	}
}
