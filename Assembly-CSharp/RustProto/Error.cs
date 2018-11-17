using System;
using System.Diagnostics;
using System.IO;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;
using RustProto.Proto;

namespace RustProto
{
	// Token: 0x02000260 RID: 608
	[DebuggerNonUserCode]
	public sealed class Error : GeneratedMessage<RustProto.Error, RustProto.Error.Builder>
	{
		// Token: 0x0600156D RID: 5485 RVA: 0x000486F8 File Offset: 0x000468F8
		private Error()
		{
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x00048720 File Offset: 0x00046920
		static Error()
		{
			object.ReferenceEquals(RustProto.Proto.Error.Descriptor, null);
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x0600156F RID: 5487 RVA: 0x00048778 File Offset: 0x00046978
		public static RustProto.Error DefaultInstance
		{
			get
			{
				return RustProto.Error.defaultInstance;
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06001570 RID: 5488 RVA: 0x00048780 File Offset: 0x00046980
		public override RustProto.Error DefaultInstanceForType
		{
			get
			{
				return RustProto.Error.DefaultInstance;
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001571 RID: 5489 RVA: 0x00048788 File Offset: 0x00046988
		protected override RustProto.Error ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001572 RID: 5490 RVA: 0x0004878C File Offset: 0x0004698C
		public static MessageDescriptor Descriptor
		{
			get
			{
				return RustProto.Proto.Error.internal__static_RustProto_Error__Descriptor;
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001573 RID: 5491 RVA: 0x00048794 File Offset: 0x00046994
		protected override FieldAccessorTable<RustProto.Error, RustProto.Error.Builder> InternalFieldAccessors
		{
			get
			{
				return RustProto.Proto.Error.internal__static_RustProto_Error__FieldAccessorTable;
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001574 RID: 5492 RVA: 0x0004879C File Offset: 0x0004699C
		public bool HasStatus
		{
			get
			{
				return this.hasStatus;
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001575 RID: 5493 RVA: 0x000487A4 File Offset: 0x000469A4
		public string Status
		{
			get
			{
				return this.status_;
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06001576 RID: 5494 RVA: 0x000487AC File Offset: 0x000469AC
		public bool HasMessage
		{
			get
			{
				return this.hasMessage;
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001577 RID: 5495 RVA: 0x000487B4 File Offset: 0x000469B4
		public string Message
		{
			get
			{
				return this.message_;
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001578 RID: 5496 RVA: 0x000487BC File Offset: 0x000469BC
		public override bool IsInitialized
		{
			get
			{
				return this.hasStatus && this.hasMessage;
			}
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x000487DC File Offset: 0x000469DC
		public override void WriteTo(ICodedOutputStream output)
		{
			int serializedSize = this.SerializedSize;
			string[] errorFieldNames = RustProto.Error._errorFieldNames;
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

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x0600157A RID: 5498 RVA: 0x00048838 File Offset: 0x00046A38
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

		// Token: 0x0600157B RID: 5499 RVA: 0x000488A4 File Offset: 0x00046AA4
		public static RustProto.Error ParseFrom(ByteString data)
		{
			return RustProto.Error.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x000488B8 File Offset: 0x00046AB8
		public static RustProto.Error ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return RustProto.Error.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x000488CC File Offset: 0x00046ACC
		public static RustProto.Error ParseFrom(byte[] data)
		{
			return RustProto.Error.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x000488E0 File Offset: 0x00046AE0
		public static RustProto.Error ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return RustProto.Error.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x000488F4 File Offset: 0x00046AF4
		public static RustProto.Error ParseFrom(Stream input)
		{
			return RustProto.Error.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x00048908 File Offset: 0x00046B08
		public static RustProto.Error ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return RustProto.Error.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x0004891C File Offset: 0x00046B1C
		public static RustProto.Error ParseDelimitedFrom(Stream input)
		{
			return RustProto.Error.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x00048930 File Offset: 0x00046B30
		public static RustProto.Error ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return RustProto.Error.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x00048944 File Offset: 0x00046B44
		public static RustProto.Error ParseFrom(ICodedInputStream input)
		{
			return RustProto.Error.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x00048958 File Offset: 0x00046B58
		public static RustProto.Error ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return RustProto.Error.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x0004896C File Offset: 0x00046B6C
		private RustProto.Error MakeReadOnly()
		{
			return this;
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x00048970 File Offset: 0x00046B70
		public static RustProto.Error.Builder CreateBuilder()
		{
			return new RustProto.Error.Builder();
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x00048978 File Offset: 0x00046B78
		public override RustProto.Error.Builder ToBuilder()
		{
			return RustProto.Error.CreateBuilder(this);
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x00048980 File Offset: 0x00046B80
		public override RustProto.Error.Builder CreateBuilderForType()
		{
			return new RustProto.Error.Builder();
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x00048988 File Offset: 0x00046B88
		public static RustProto.Error.Builder CreateBuilder(RustProto.Error prototype)
		{
			return new RustProto.Error.Builder(prototype);
		}

		// Token: 0x04000B44 RID: 2884
		public const int StatusFieldNumber = 1;

		// Token: 0x04000B45 RID: 2885
		public const int MessageFieldNumber = 2;

		// Token: 0x04000B46 RID: 2886
		private static readonly RustProto.Error defaultInstance = new RustProto.Error().MakeReadOnly();

		// Token: 0x04000B47 RID: 2887
		private static readonly string[] _errorFieldNames = new string[]
		{
			"message",
			"status"
		};

		// Token: 0x04000B48 RID: 2888
		private static readonly uint[] _errorFieldTags = new uint[]
		{
			18u,
			10u
		};

		// Token: 0x04000B49 RID: 2889
		private bool hasStatus;

		// Token: 0x04000B4A RID: 2890
		private string status_ = string.Empty;

		// Token: 0x04000B4B RID: 2891
		private bool hasMessage;

		// Token: 0x04000B4C RID: 2892
		private string message_ = string.Empty;

		// Token: 0x04000B4D RID: 2893
		private int memoizedSerializedSize = -1;

		// Token: 0x02000261 RID: 609
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<RustProto.Error, RustProto.Error.Builder>
		{
			// Token: 0x0600158A RID: 5514 RVA: 0x00048990 File Offset: 0x00046B90
			public Builder()
			{
				this.result = RustProto.Error.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x0600158B RID: 5515 RVA: 0x000489AC File Offset: 0x00046BAC
			internal Builder(RustProto.Error cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x17000611 RID: 1553
			// (get) Token: 0x0600158C RID: 5516 RVA: 0x000489C4 File Offset: 0x00046BC4
			protected override RustProto.Error.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x0600158D RID: 5517 RVA: 0x000489C8 File Offset: 0x00046BC8
			private RustProto.Error PrepareBuilder()
			{
				if (this.resultIsReadOnly)
				{
					RustProto.Error other = this.result;
					this.result = new RustProto.Error();
					this.resultIsReadOnly = false;
					this.MergeFrom(other);
				}
				return this.result;
			}

			// Token: 0x17000612 RID: 1554
			// (get) Token: 0x0600158E RID: 5518 RVA: 0x00048A08 File Offset: 0x00046C08
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x17000613 RID: 1555
			// (get) Token: 0x0600158F RID: 5519 RVA: 0x00048A18 File Offset: 0x00046C18
			protected override RustProto.Error MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x06001590 RID: 5520 RVA: 0x00048A20 File Offset: 0x00046C20
			public override RustProto.Error.Builder Clear()
			{
				this.result = RustProto.Error.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x06001591 RID: 5521 RVA: 0x00048A38 File Offset: 0x00046C38
			public override RustProto.Error.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new RustProto.Error.Builder(this.result);
				}
				return new RustProto.Error.Builder().MergeFrom(this.result);
			}

			// Token: 0x17000614 RID: 1556
			// (get) Token: 0x06001592 RID: 5522 RVA: 0x00048A64 File Offset: 0x00046C64
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return RustProto.Error.Descriptor;
				}
			}

			// Token: 0x17000615 RID: 1557
			// (get) Token: 0x06001593 RID: 5523 RVA: 0x00048A6C File Offset: 0x00046C6C
			public override RustProto.Error DefaultInstanceForType
			{
				get
				{
					return RustProto.Error.DefaultInstance;
				}
			}

			// Token: 0x06001594 RID: 5524 RVA: 0x00048A74 File Offset: 0x00046C74
			public override RustProto.Error BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x06001595 RID: 5525 RVA: 0x00048AA8 File Offset: 0x00046CA8
			public override RustProto.Error.Builder MergeFrom(IMessage other)
			{
				if (other is RustProto.Error)
				{
					return this.MergeFrom((RustProto.Error)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x06001596 RID: 5526 RVA: 0x00048ACC File Offset: 0x00046CCC
			public override RustProto.Error.Builder MergeFrom(RustProto.Error other)
			{
				if (other == RustProto.Error.DefaultInstance)
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

			// Token: 0x06001597 RID: 5527 RVA: 0x00048B2C File Offset: 0x00046D2C
			public override RustProto.Error.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x06001598 RID: 5528 RVA: 0x00048B3C File Offset: 0x00046D3C
			public override RustProto.Error.Builder MergeFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
			{
				this.PrepareBuilder();
				UnknownFieldSet.Builder builder = null;
				uint num;
				string text;
				while (input.ReadTag(ref num, ref text))
				{
					if (num == 0u && text != null)
					{
						int num2 = Array.BinarySearch<string>(RustProto.Error._errorFieldNames, text, StringComparer.Ordinal);
						if (num2 < 0)
						{
							if (builder == null)
							{
								builder = UnknownFieldSet.CreateBuilder(this.UnknownFields);
							}
							this.ParseUnknownField(input, builder, extensionRegistry, num, text);
							continue;
						}
						num = RustProto.Error._errorFieldTags[num2];
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

			// Token: 0x17000616 RID: 1558
			// (get) Token: 0x06001599 RID: 5529 RVA: 0x00048C7C File Offset: 0x00046E7C
			public bool HasStatus
			{
				get
				{
					return this.result.hasStatus;
				}
			}

			// Token: 0x17000617 RID: 1559
			// (get) Token: 0x0600159A RID: 5530 RVA: 0x00048C8C File Offset: 0x00046E8C
			// (set) Token: 0x0600159B RID: 5531 RVA: 0x00048C9C File Offset: 0x00046E9C
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

			// Token: 0x0600159C RID: 5532 RVA: 0x00048CA8 File Offset: 0x00046EA8
			public RustProto.Error.Builder SetStatus(string value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasStatus = true;
				this.result.status_ = value;
				return this;
			}

			// Token: 0x0600159D RID: 5533 RVA: 0x00048CD8 File Offset: 0x00046ED8
			public RustProto.Error.Builder ClearStatus()
			{
				this.PrepareBuilder();
				this.result.hasStatus = false;
				this.result.status_ = string.Empty;
				return this;
			}

			// Token: 0x17000618 RID: 1560
			// (get) Token: 0x0600159E RID: 5534 RVA: 0x00048D0C File Offset: 0x00046F0C
			public bool HasMessage
			{
				get
				{
					return this.result.hasMessage;
				}
			}

			// Token: 0x17000619 RID: 1561
			// (get) Token: 0x0600159F RID: 5535 RVA: 0x00048D1C File Offset: 0x00046F1C
			// (set) Token: 0x060015A0 RID: 5536 RVA: 0x00048D2C File Offset: 0x00046F2C
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

			// Token: 0x060015A1 RID: 5537 RVA: 0x00048D38 File Offset: 0x00046F38
			public RustProto.Error.Builder SetMessage(string value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasMessage = true;
				this.result.message_ = value;
				return this;
			}

			// Token: 0x060015A2 RID: 5538 RVA: 0x00048D68 File Offset: 0x00046F68
			public RustProto.Error.Builder ClearMessage()
			{
				this.PrepareBuilder();
				this.result.hasMessage = false;
				this.result.message_ = string.Empty;
				return this;
			}

			// Token: 0x04000B4E RID: 2894
			private bool resultIsReadOnly;

			// Token: 0x04000B4F RID: 2895
			private RustProto.Error result;
		}
	}
}
