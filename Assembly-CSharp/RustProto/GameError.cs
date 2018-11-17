using System;
using System.Diagnostics;
using System.IO;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;
using RustProto.Proto;

namespace RustProto
{
	// Token: 0x0200022F RID: 559
	[DebuggerNonUserCode]
	public sealed class GameError : GeneratedMessage<GameError, GameError.Builder>
	{
		// Token: 0x0600144F RID: 5199 RVA: 0x000449F4 File Offset: 0x00042BF4
		private GameError()
		{
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x00044A1C File Offset: 0x00042C1C
		static GameError()
		{
			object.ReferenceEquals(RustProto.Proto.Error.Descriptor, null);
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001451 RID: 5201 RVA: 0x00044A74 File Offset: 0x00042C74
		public static GameError DefaultInstance
		{
			get
			{
				return GameError.defaultInstance;
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06001452 RID: 5202 RVA: 0x00044A7C File Offset: 0x00042C7C
		public override GameError DefaultInstanceForType
		{
			get
			{
				return GameError.DefaultInstance;
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06001453 RID: 5203 RVA: 0x00044A84 File Offset: 0x00042C84
		protected override GameError ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06001454 RID: 5204 RVA: 0x00044A88 File Offset: 0x00042C88
		public static MessageDescriptor Descriptor
		{
			get
			{
				return RustProto.Proto.Error.internal__static_RustProto_GameError__Descriptor;
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06001455 RID: 5205 RVA: 0x00044A90 File Offset: 0x00042C90
		protected override FieldAccessorTable<GameError, GameError.Builder> InternalFieldAccessors
		{
			get
			{
				return RustProto.Proto.Error.internal__static_RustProto_GameError__FieldAccessorTable;
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001456 RID: 5206 RVA: 0x00044A98 File Offset: 0x00042C98
		public bool HasError
		{
			get
			{
				return this.hasError;
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06001457 RID: 5207 RVA: 0x00044AA0 File Offset: 0x00042CA0
		public string Error
		{
			get
			{
				return this.error_;
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06001458 RID: 5208 RVA: 0x00044AA8 File Offset: 0x00042CA8
		public bool HasTrace
		{
			get
			{
				return this.hasTrace;
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06001459 RID: 5209 RVA: 0x00044AB0 File Offset: 0x00042CB0
		public string Trace
		{
			get
			{
				return this.trace_;
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x0600145A RID: 5210 RVA: 0x00044AB8 File Offset: 0x00042CB8
		public override bool IsInitialized
		{
			get
			{
				return this.hasError && this.hasTrace;
			}
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x00044AD8 File Offset: 0x00042CD8
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

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x0600145C RID: 5212 RVA: 0x00044B34 File Offset: 0x00042D34
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

		// Token: 0x0600145D RID: 5213 RVA: 0x00044BA0 File Offset: 0x00042DA0
		public static GameError ParseFrom(ByteString data)
		{
			return GameError.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x00044BB4 File Offset: 0x00042DB4
		public static GameError ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return GameError.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x00044BC8 File Offset: 0x00042DC8
		public static GameError ParseFrom(byte[] data)
		{
			return GameError.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x00044BDC File Offset: 0x00042DDC
		public static GameError ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return GameError.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x00044BF0 File Offset: 0x00042DF0
		public static GameError ParseFrom(Stream input)
		{
			return GameError.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x00044C04 File Offset: 0x00042E04
		public static GameError ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return GameError.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x00044C18 File Offset: 0x00042E18
		public static GameError ParseDelimitedFrom(Stream input)
		{
			return GameError.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x00044C2C File Offset: 0x00042E2C
		public static GameError ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return GameError.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x00044C40 File Offset: 0x00042E40
		public static GameError ParseFrom(ICodedInputStream input)
		{
			return GameError.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x00044C54 File Offset: 0x00042E54
		public static GameError ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return GameError.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x00044C68 File Offset: 0x00042E68
		private GameError MakeReadOnly()
		{
			return this;
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x00044C6C File Offset: 0x00042E6C
		public static GameError.Builder CreateBuilder()
		{
			return new GameError.Builder();
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x00044C74 File Offset: 0x00042E74
		public override GameError.Builder ToBuilder()
		{
			return GameError.CreateBuilder(this);
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x00044C7C File Offset: 0x00042E7C
		public override GameError.Builder CreateBuilderForType()
		{
			return new GameError.Builder();
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x00044C84 File Offset: 0x00042E84
		public static GameError.Builder CreateBuilder(GameError prototype)
		{
			return new GameError.Builder(prototype);
		}

		// Token: 0x04000A2D RID: 2605
		public const int ErrorFieldNumber = 1;

		// Token: 0x04000A2E RID: 2606
		public const int TraceFieldNumber = 2;

		// Token: 0x04000A2F RID: 2607
		private static readonly GameError defaultInstance = new GameError().MakeReadOnly();

		// Token: 0x04000A30 RID: 2608
		private static readonly string[] _gameErrorFieldNames = new string[]
		{
			"error",
			"trace"
		};

		// Token: 0x04000A31 RID: 2609
		private static readonly uint[] _gameErrorFieldTags = new uint[]
		{
			10u,
			18u
		};

		// Token: 0x04000A32 RID: 2610
		private bool hasError;

		// Token: 0x04000A33 RID: 2611
		private string error_ = string.Empty;

		// Token: 0x04000A34 RID: 2612
		private bool hasTrace;

		// Token: 0x04000A35 RID: 2613
		private string trace_ = string.Empty;

		// Token: 0x04000A36 RID: 2614
		private int memoizedSerializedSize = -1;

		// Token: 0x02000230 RID: 560
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<GameError, GameError.Builder>
		{
			// Token: 0x0600146C RID: 5228 RVA: 0x00044C8C File Offset: 0x00042E8C
			public Builder()
			{
				this.result = GameError.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x0600146D RID: 5229 RVA: 0x00044CA8 File Offset: 0x00042EA8
			internal Builder(GameError cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x170005DD RID: 1501
			// (get) Token: 0x0600146E RID: 5230 RVA: 0x00044CC0 File Offset: 0x00042EC0
			protected override GameError.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x0600146F RID: 5231 RVA: 0x00044CC4 File Offset: 0x00042EC4
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

			// Token: 0x170005DE RID: 1502
			// (get) Token: 0x06001470 RID: 5232 RVA: 0x00044D04 File Offset: 0x00042F04
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x170005DF RID: 1503
			// (get) Token: 0x06001471 RID: 5233 RVA: 0x00044D14 File Offset: 0x00042F14
			protected override GameError MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x06001472 RID: 5234 RVA: 0x00044D1C File Offset: 0x00042F1C
			public override GameError.Builder Clear()
			{
				this.result = GameError.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x06001473 RID: 5235 RVA: 0x00044D34 File Offset: 0x00042F34
			public override GameError.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new GameError.Builder(this.result);
				}
				return new GameError.Builder().MergeFrom(this.result);
			}

			// Token: 0x170005E0 RID: 1504
			// (get) Token: 0x06001474 RID: 5236 RVA: 0x00044D60 File Offset: 0x00042F60
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return GameError.Descriptor;
				}
			}

			// Token: 0x170005E1 RID: 1505
			// (get) Token: 0x06001475 RID: 5237 RVA: 0x00044D68 File Offset: 0x00042F68
			public override GameError DefaultInstanceForType
			{
				get
				{
					return GameError.DefaultInstance;
				}
			}

			// Token: 0x06001476 RID: 5238 RVA: 0x00044D70 File Offset: 0x00042F70
			public override GameError BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x06001477 RID: 5239 RVA: 0x00044DA4 File Offset: 0x00042FA4
			public override GameError.Builder MergeFrom(IMessage other)
			{
				if (other is GameError)
				{
					return this.MergeFrom((GameError)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x06001478 RID: 5240 RVA: 0x00044DC8 File Offset: 0x00042FC8
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

			// Token: 0x06001479 RID: 5241 RVA: 0x00044E28 File Offset: 0x00043028
			public override GameError.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x0600147A RID: 5242 RVA: 0x00044E38 File Offset: 0x00043038
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

			// Token: 0x170005E2 RID: 1506
			// (get) Token: 0x0600147B RID: 5243 RVA: 0x00044F78 File Offset: 0x00043178
			public bool HasError
			{
				get
				{
					return this.result.hasError;
				}
			}

			// Token: 0x170005E3 RID: 1507
			// (get) Token: 0x0600147C RID: 5244 RVA: 0x00044F88 File Offset: 0x00043188
			// (set) Token: 0x0600147D RID: 5245 RVA: 0x00044F98 File Offset: 0x00043198
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

			// Token: 0x0600147E RID: 5246 RVA: 0x00044FA4 File Offset: 0x000431A4
			public GameError.Builder SetError(string value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasError = true;
				this.result.error_ = value;
				return this;
			}

			// Token: 0x0600147F RID: 5247 RVA: 0x00044FD4 File Offset: 0x000431D4
			public GameError.Builder ClearError()
			{
				this.PrepareBuilder();
				this.result.hasError = false;
				this.result.error_ = string.Empty;
				return this;
			}

			// Token: 0x170005E4 RID: 1508
			// (get) Token: 0x06001480 RID: 5248 RVA: 0x00045008 File Offset: 0x00043208
			public bool HasTrace
			{
				get
				{
					return this.result.hasTrace;
				}
			}

			// Token: 0x170005E5 RID: 1509
			// (get) Token: 0x06001481 RID: 5249 RVA: 0x00045018 File Offset: 0x00043218
			// (set) Token: 0x06001482 RID: 5250 RVA: 0x00045028 File Offset: 0x00043228
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

			// Token: 0x06001483 RID: 5251 RVA: 0x00045034 File Offset: 0x00043234
			public GameError.Builder SetTrace(string value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasTrace = true;
				this.result.trace_ = value;
				return this;
			}

			// Token: 0x06001484 RID: 5252 RVA: 0x00045064 File Offset: 0x00043264
			public GameError.Builder ClearTrace()
			{
				this.PrepareBuilder();
				this.result.hasTrace = false;
				this.result.trace_ = string.Empty;
				return this;
			}

			// Token: 0x04000A37 RID: 2615
			private bool resultIsReadOnly;

			// Token: 0x04000A38 RID: 2616
			private GameError result;
		}
	}
}
