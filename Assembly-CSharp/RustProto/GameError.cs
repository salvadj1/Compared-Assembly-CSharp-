using System;
using System.Diagnostics;
using System.IO;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;
using RustProto.Proto;

namespace RustProto
{
	// Token: 0x02000262 RID: 610
	[DebuggerNonUserCode]
	public sealed class GameError : GeneratedMessage<GameError, GameError.Builder>
	{
		// Token: 0x060015A3 RID: 5539 RVA: 0x00048D9C File Offset: 0x00046F9C
		private GameError()
		{
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x00048DC4 File Offset: 0x00046FC4
		static GameError()
		{
			object.ReferenceEquals(RustProto.Proto.Error.Descriptor, null);
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x060015A5 RID: 5541 RVA: 0x00048E1C File Offset: 0x0004701C
		public static GameError DefaultInstance
		{
			get
			{
				return GameError.defaultInstance;
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x060015A6 RID: 5542 RVA: 0x00048E24 File Offset: 0x00047024
		public override GameError DefaultInstanceForType
		{
			get
			{
				return GameError.DefaultInstance;
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x060015A7 RID: 5543 RVA: 0x00048E2C File Offset: 0x0004702C
		protected override GameError ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x060015A8 RID: 5544 RVA: 0x00048E30 File Offset: 0x00047030
		public static MessageDescriptor Descriptor
		{
			get
			{
				return RustProto.Proto.Error.internal__static_RustProto_GameError__Descriptor;
			}
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x060015A9 RID: 5545 RVA: 0x00048E38 File Offset: 0x00047038
		protected override FieldAccessorTable<GameError, GameError.Builder> InternalFieldAccessors
		{
			get
			{
				return RustProto.Proto.Error.internal__static_RustProto_GameError__FieldAccessorTable;
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x060015AA RID: 5546 RVA: 0x00048E40 File Offset: 0x00047040
		public bool HasError
		{
			get
			{
				return this.hasError;
			}
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x060015AB RID: 5547 RVA: 0x00048E48 File Offset: 0x00047048
		public string Error
		{
			get
			{
				return this.error_;
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x060015AC RID: 5548 RVA: 0x00048E50 File Offset: 0x00047050
		public bool HasTrace
		{
			get
			{
				return this.hasTrace;
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x060015AD RID: 5549 RVA: 0x00048E58 File Offset: 0x00047058
		public string Trace
		{
			get
			{
				return this.trace_;
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x060015AE RID: 5550 RVA: 0x00048E60 File Offset: 0x00047060
		public override bool IsInitialized
		{
			get
			{
				return this.hasError && this.hasTrace;
			}
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x00048E80 File Offset: 0x00047080
		public override void WriteTo(ICodedOutputStream output)
		{
			int serializedSize = this.SerializedSize;
			string[] gameErrorFieldNames = GameError._gameErrorFieldNames;
			if (this.hasError)
			{
				output.WriteString(1, gameErrorFieldNames[0], this.Error);
			}
			if (this.hasTrace)
			{
				output.WriteString(2, gameErrorFieldNames[1], this.Trace);
			}
			this.UnknownFields.WriteTo(output);
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x060015B0 RID: 5552 RVA: 0x00048EDC File Offset: 0x000470DC
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
				if (this.hasError)
				{
					num += CodedOutputStream.ComputeStringSize(1, this.Error);
				}
				if (this.hasTrace)
				{
					num += CodedOutputStream.ComputeStringSize(2, this.Trace);
				}
				num += this.UnknownFields.SerializedSize;
				this.memoizedSerializedSize = num;
				return num;
			}
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x00048F48 File Offset: 0x00047148
		public static GameError ParseFrom(ByteString data)
		{
			return GameError.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x00048F5C File Offset: 0x0004715C
		public static GameError ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return GameError.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x00048F70 File Offset: 0x00047170
		public static GameError ParseFrom(byte[] data)
		{
			return GameError.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x00048F84 File Offset: 0x00047184
		public static GameError ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return GameError.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x00048F98 File Offset: 0x00047198
		public static GameError ParseFrom(Stream input)
		{
			return GameError.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x00048FAC File Offset: 0x000471AC
		public static GameError ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return GameError.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x00048FC0 File Offset: 0x000471C0
		public static GameError ParseDelimitedFrom(Stream input)
		{
			return GameError.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x00048FD4 File Offset: 0x000471D4
		public static GameError ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return GameError.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x00048FE8 File Offset: 0x000471E8
		public static GameError ParseFrom(ICodedInputStream input)
		{
			return GameError.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x00048FFC File Offset: 0x000471FC
		public static GameError ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return GameError.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x00049010 File Offset: 0x00047210
		private GameError MakeReadOnly()
		{
			return this;
		}

		// Token: 0x060015BC RID: 5564 RVA: 0x00049014 File Offset: 0x00047214
		public static GameError.Builder CreateBuilder()
		{
			return new GameError.Builder();
		}

		// Token: 0x060015BD RID: 5565 RVA: 0x0004901C File Offset: 0x0004721C
		public override GameError.Builder ToBuilder()
		{
			return GameError.CreateBuilder(this);
		}

		// Token: 0x060015BE RID: 5566 RVA: 0x00049024 File Offset: 0x00047224
		public override GameError.Builder CreateBuilderForType()
		{
			return new GameError.Builder();
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x0004902C File Offset: 0x0004722C
		public static GameError.Builder CreateBuilder(GameError prototype)
		{
			return new GameError.Builder(prototype);
		}

		// Token: 0x04000B50 RID: 2896
		public const int ErrorFieldNumber = 1;

		// Token: 0x04000B51 RID: 2897
		public const int TraceFieldNumber = 2;

		// Token: 0x04000B52 RID: 2898
		private static readonly GameError defaultInstance = new GameError().MakeReadOnly();

		// Token: 0x04000B53 RID: 2899
		private static readonly string[] _gameErrorFieldNames = new string[]
		{
			"error",
			"trace"
		};

		// Token: 0x04000B54 RID: 2900
		private static readonly uint[] _gameErrorFieldTags = new uint[]
		{
			10u,
			18u
		};

		// Token: 0x04000B55 RID: 2901
		private bool hasError;

		// Token: 0x04000B56 RID: 2902
		private string error_ = string.Empty;

		// Token: 0x04000B57 RID: 2903
		private bool hasTrace;

		// Token: 0x04000B58 RID: 2904
		private string trace_ = string.Empty;

		// Token: 0x04000B59 RID: 2905
		private int memoizedSerializedSize = -1;

		// Token: 0x02000263 RID: 611
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<GameError, GameError.Builder>
		{
			// Token: 0x060015C0 RID: 5568 RVA: 0x00049034 File Offset: 0x00047234
			public Builder()
			{
				this.result = GameError.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x060015C1 RID: 5569 RVA: 0x00049050 File Offset: 0x00047250
			internal Builder(GameError cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x17000625 RID: 1573
			// (get) Token: 0x060015C2 RID: 5570 RVA: 0x00049068 File Offset: 0x00047268
			protected override GameError.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x060015C3 RID: 5571 RVA: 0x0004906C File Offset: 0x0004726C
			private GameError PrepareBuilder()
			{
				if (this.resultIsReadOnly)
				{
					GameError other = this.result;
					this.result = new GameError();
					this.resultIsReadOnly = false;
					this.MergeFrom(other);
				}
				return this.result;
			}

			// Token: 0x17000626 RID: 1574
			// (get) Token: 0x060015C4 RID: 5572 RVA: 0x000490AC File Offset: 0x000472AC
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x17000627 RID: 1575
			// (get) Token: 0x060015C5 RID: 5573 RVA: 0x000490BC File Offset: 0x000472BC
			protected override GameError MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x060015C6 RID: 5574 RVA: 0x000490C4 File Offset: 0x000472C4
			public override GameError.Builder Clear()
			{
				this.result = GameError.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x060015C7 RID: 5575 RVA: 0x000490DC File Offset: 0x000472DC
			public override GameError.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new GameError.Builder(this.result);
				}
				return new GameError.Builder().MergeFrom(this.result);
			}

			// Token: 0x17000628 RID: 1576
			// (get) Token: 0x060015C8 RID: 5576 RVA: 0x00049108 File Offset: 0x00047308
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return GameError.Descriptor;
				}
			}

			// Token: 0x17000629 RID: 1577
			// (get) Token: 0x060015C9 RID: 5577 RVA: 0x00049110 File Offset: 0x00047310
			public override GameError DefaultInstanceForType
			{
				get
				{
					return GameError.DefaultInstance;
				}
			}

			// Token: 0x060015CA RID: 5578 RVA: 0x00049118 File Offset: 0x00047318
			public override GameError BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x060015CB RID: 5579 RVA: 0x0004914C File Offset: 0x0004734C
			public override GameError.Builder MergeFrom(IMessage other)
			{
				if (other is GameError)
				{
					return this.MergeFrom((GameError)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x060015CC RID: 5580 RVA: 0x00049170 File Offset: 0x00047370
			public override GameError.Builder MergeFrom(GameError other)
			{
				if (other == GameError.DefaultInstance)
				{
					return this;
				}
				this.PrepareBuilder();
				if (other.HasError)
				{
					this.Error = other.Error;
				}
				if (other.HasTrace)
				{
					this.Trace = other.Trace;
				}
				this.MergeUnknownFields(other.UnknownFields);
				return this;
			}

			// Token: 0x060015CD RID: 5581 RVA: 0x000491D0 File Offset: 0x000473D0
			public override GameError.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x060015CE RID: 5582 RVA: 0x000491E0 File Offset: 0x000473E0
			public override GameError.Builder MergeFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
			{
				this.PrepareBuilder();
				UnknownFieldSet.Builder builder = null;
				uint num;
				string text;
				while (input.ReadTag(ref num, ref text))
				{
					if (num == 0u && text != null)
					{
						int num2 = Array.BinarySearch<string>(GameError._gameErrorFieldNames, text, StringComparer.Ordinal);
						if (num2 < 0)
						{
							if (builder == null)
							{
								builder = UnknownFieldSet.CreateBuilder(this.UnknownFields);
							}
							this.ParseUnknownField(input, builder, extensionRegistry, num, text);
							continue;
						}
						num = GameError._gameErrorFieldTags[num2];
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
							this.result.hasTrace = input.ReadString(ref this.result.trace_);
						}
					}
					else
					{
						this.result.hasError = input.ReadString(ref this.result.error_);
					}
				}
				if (builder != null)
				{
					this.UnknownFields = builder.Build();
				}
				return this;
			}

			// Token: 0x1700062A RID: 1578
			// (get) Token: 0x060015CF RID: 5583 RVA: 0x00049320 File Offset: 0x00047520
			public bool HasError
			{
				get
				{
					return this.result.hasError;
				}
			}

			// Token: 0x1700062B RID: 1579
			// (get) Token: 0x060015D0 RID: 5584 RVA: 0x00049330 File Offset: 0x00047530
			// (set) Token: 0x060015D1 RID: 5585 RVA: 0x00049340 File Offset: 0x00047540
			public string Error
			{
				get
				{
					return this.result.Error;
				}
				set
				{
					this.SetError(value);
				}
			}

			// Token: 0x060015D2 RID: 5586 RVA: 0x0004934C File Offset: 0x0004754C
			public GameError.Builder SetError(string value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasError = true;
				this.result.error_ = value;
				return this;
			}

			// Token: 0x060015D3 RID: 5587 RVA: 0x0004937C File Offset: 0x0004757C
			public GameError.Builder ClearError()
			{
				this.PrepareBuilder();
				this.result.hasError = false;
				this.result.error_ = string.Empty;
				return this;
			}

			// Token: 0x1700062C RID: 1580
			// (get) Token: 0x060015D4 RID: 5588 RVA: 0x000493B0 File Offset: 0x000475B0
			public bool HasTrace
			{
				get
				{
					return this.result.hasTrace;
				}
			}

			// Token: 0x1700062D RID: 1581
			// (get) Token: 0x060015D5 RID: 5589 RVA: 0x000493C0 File Offset: 0x000475C0
			// (set) Token: 0x060015D6 RID: 5590 RVA: 0x000493D0 File Offset: 0x000475D0
			public string Trace
			{
				get
				{
					return this.result.Trace;
				}
				set
				{
					this.SetTrace(value);
				}
			}

			// Token: 0x060015D7 RID: 5591 RVA: 0x000493DC File Offset: 0x000475DC
			public GameError.Builder SetTrace(string value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasTrace = true;
				this.result.trace_ = value;
				return this;
			}

			// Token: 0x060015D8 RID: 5592 RVA: 0x0004940C File Offset: 0x0004760C
			public GameError.Builder ClearTrace()
			{
				this.PrepareBuilder();
				this.result.hasTrace = false;
				this.result.trace_ = string.Empty;
				return this;
			}

			// Token: 0x04000B5A RID: 2906
			private bool resultIsReadOnly;

			// Token: 0x04000B5B RID: 2907
			private GameError result;
		}
	}
}
