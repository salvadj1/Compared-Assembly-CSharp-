using System;
using System.Diagnostics;
using System.IO;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;
using RustProto.Helpers;

namespace RustProto
{
	// Token: 0x02000215 RID: 533
	[DebuggerNonUserCode]
	public sealed class objectTakeDamage : GeneratedMessage<objectTakeDamage, objectTakeDamage.Builder>
	{
		// Token: 0x060011F0 RID: 4592 RVA: 0x0003FDAC File Offset: 0x0003DFAC
		private objectTakeDamage()
		{
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x0003FDBC File Offset: 0x0003DFBC
		static objectTakeDamage()
		{
			object.ReferenceEquals(Worldsave.Descriptor, null);
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x0003FE08 File Offset: 0x0003E008
		public static Recycler<objectTakeDamage, objectTakeDamage.Builder> Recycler()
		{
			return Recycler<objectTakeDamage, objectTakeDamage.Builder>.Manufacture();
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x060011F3 RID: 4595 RVA: 0x0003FE10 File Offset: 0x0003E010
		public static objectTakeDamage DefaultInstance
		{
			get
			{
				return objectTakeDamage.defaultInstance;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x060011F4 RID: 4596 RVA: 0x0003FE18 File Offset: 0x0003E018
		public override objectTakeDamage DefaultInstanceForType
		{
			get
			{
				return objectTakeDamage.DefaultInstance;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x060011F5 RID: 4597 RVA: 0x0003FE20 File Offset: 0x0003E020
		protected override objectTakeDamage ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x060011F6 RID: 4598 RVA: 0x0003FE24 File Offset: 0x0003E024
		public static MessageDescriptor Descriptor
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectTakeDamage__Descriptor;
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x060011F7 RID: 4599 RVA: 0x0003FE2C File Offset: 0x0003E02C
		protected override FieldAccessorTable<objectTakeDamage, objectTakeDamage.Builder> InternalFieldAccessors
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectTakeDamage__FieldAccessorTable;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x060011F8 RID: 4600 RVA: 0x0003FE34 File Offset: 0x0003E034
		public bool HasHealth
		{
			get
			{
				return this.hasHealth;
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x060011F9 RID: 4601 RVA: 0x0003FE3C File Offset: 0x0003E03C
		public float Health
		{
			get
			{
				return this.health_;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x060011FA RID: 4602 RVA: 0x0003FE44 File Offset: 0x0003E044
		public override bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x0003FE48 File Offset: 0x0003E048
		public override void WriteTo(ICodedOutputStream output)
		{
			int serializedSize = this.SerializedSize;
			string[] objectTakeDamageFieldNames = objectTakeDamage._objectTakeDamageFieldNames;
			if (this.hasHealth)
			{
				output.WriteFloat(1, objectTakeDamageFieldNames[0], this.Health);
			}
			this.UnknownFields.WriteTo(output);
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x060011FC RID: 4604 RVA: 0x0003FE8C File Offset: 0x0003E08C
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
				if (this.hasHealth)
				{
					num += CodedOutputStream.ComputeFloatSize(1, this.Health);
				}
				num += this.UnknownFields.SerializedSize;
				this.memoizedSerializedSize = num;
				return num;
			}
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x0003FEDC File Offset: 0x0003E0DC
		public static objectTakeDamage ParseFrom(ByteString data)
		{
			return objectTakeDamage.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x0003FEF0 File Offset: 0x0003E0F0
		public static objectTakeDamage ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return objectTakeDamage.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x0003FF04 File Offset: 0x0003E104
		public static objectTakeDamage ParseFrom(byte[] data)
		{
			return objectTakeDamage.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x0003FF18 File Offset: 0x0003E118
		public static objectTakeDamage ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return objectTakeDamage.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x0003FF2C File Offset: 0x0003E12C
		public static objectTakeDamage ParseFrom(Stream input)
		{
			return objectTakeDamage.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x0003FF40 File Offset: 0x0003E140
		public static objectTakeDamage ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectTakeDamage.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x0003FF54 File Offset: 0x0003E154
		public static objectTakeDamage ParseDelimitedFrom(Stream input)
		{
			return objectTakeDamage.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x0003FF68 File Offset: 0x0003E168
		public static objectTakeDamage ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectTakeDamage.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x0003FF7C File Offset: 0x0003E17C
		public static objectTakeDamage ParseFrom(ICodedInputStream input)
		{
			return objectTakeDamage.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x0003FF90 File Offset: 0x0003E190
		public static objectTakeDamage ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return objectTakeDamage.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x0003FFA4 File Offset: 0x0003E1A4
		private objectTakeDamage MakeReadOnly()
		{
			return this;
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x0003FFA8 File Offset: 0x0003E1A8
		public static objectTakeDamage.Builder CreateBuilder()
		{
			return new objectTakeDamage.Builder();
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x0003FFB0 File Offset: 0x0003E1B0
		public override objectTakeDamage.Builder ToBuilder()
		{
			return objectTakeDamage.CreateBuilder(this);
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x0003FFB8 File Offset: 0x0003E1B8
		public override objectTakeDamage.Builder CreateBuilderForType()
		{
			return new objectTakeDamage.Builder();
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x0003FFC0 File Offset: 0x0003E1C0
		public static objectTakeDamage.Builder CreateBuilder(objectTakeDamage prototype)
		{
			return new objectTakeDamage.Builder(prototype);
		}

		// Token: 0x0400098D RID: 2445
		public const int HealthFieldNumber = 1;

		// Token: 0x0400098E RID: 2446
		private static readonly objectTakeDamage defaultInstance = new objectTakeDamage().MakeReadOnly();

		// Token: 0x0400098F RID: 2447
		private static readonly string[] _objectTakeDamageFieldNames = new string[]
		{
			"health"
		};

		// Token: 0x04000990 RID: 2448
		private static readonly uint[] _objectTakeDamageFieldTags = new uint[]
		{
			13u
		};

		// Token: 0x04000991 RID: 2449
		private bool hasHealth;

		// Token: 0x04000992 RID: 2450
		private float health_;

		// Token: 0x04000993 RID: 2451
		private int memoizedSerializedSize = -1;

		// Token: 0x02000216 RID: 534
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<objectTakeDamage, objectTakeDamage.Builder>
		{
			// Token: 0x0600120C RID: 4620 RVA: 0x0003FFC8 File Offset: 0x0003E1C8
			public Builder()
			{
				this.result = objectTakeDamage.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x0600120D RID: 4621 RVA: 0x0003FFE4 File Offset: 0x0003E1E4
			internal Builder(objectTakeDamage cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x170004FB RID: 1275
			// (get) Token: 0x0600120E RID: 4622 RVA: 0x0003FFFC File Offset: 0x0003E1FC
			protected override objectTakeDamage.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x0600120F RID: 4623 RVA: 0x00040000 File Offset: 0x0003E200
			private objectTakeDamage PrepareBuilder()
			{
				if (this.resultIsReadOnly)
				{
					objectTakeDamage other = this.result;
					this.result = new objectTakeDamage();
					this.resultIsReadOnly = false;
					this.MergeFrom(other);
				}
				return this.result;
			}

			// Token: 0x170004FC RID: 1276
			// (get) Token: 0x06001210 RID: 4624 RVA: 0x00040040 File Offset: 0x0003E240
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x170004FD RID: 1277
			// (get) Token: 0x06001211 RID: 4625 RVA: 0x00040050 File Offset: 0x0003E250
			protected override objectTakeDamage MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x06001212 RID: 4626 RVA: 0x00040058 File Offset: 0x0003E258
			public override objectTakeDamage.Builder Clear()
			{
				this.result = objectTakeDamage.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x06001213 RID: 4627 RVA: 0x00040070 File Offset: 0x0003E270
			public override objectTakeDamage.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new objectTakeDamage.Builder(this.result);
				}
				return new objectTakeDamage.Builder().MergeFrom(this.result);
			}

			// Token: 0x170004FE RID: 1278
			// (get) Token: 0x06001214 RID: 4628 RVA: 0x0004009C File Offset: 0x0003E29C
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return objectTakeDamage.Descriptor;
				}
			}

			// Token: 0x170004FF RID: 1279
			// (get) Token: 0x06001215 RID: 4629 RVA: 0x000400A4 File Offset: 0x0003E2A4
			public override objectTakeDamage DefaultInstanceForType
			{
				get
				{
					return objectTakeDamage.DefaultInstance;
				}
			}

			// Token: 0x06001216 RID: 4630 RVA: 0x000400AC File Offset: 0x0003E2AC
			public override objectTakeDamage BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x06001217 RID: 4631 RVA: 0x000400E0 File Offset: 0x0003E2E0
			public override objectTakeDamage.Builder MergeFrom(IMessage other)
			{
				if (other is objectTakeDamage)
				{
					return this.MergeFrom((objectTakeDamage)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x06001218 RID: 4632 RVA: 0x00040104 File Offset: 0x0003E304
			public override objectTakeDamage.Builder MergeFrom(objectTakeDamage other)
			{
				if (other == objectTakeDamage.DefaultInstance)
				{
					return this;
				}
				this.PrepareBuilder();
				if (other.HasHealth)
				{
					this.Health = other.Health;
				}
				this.MergeUnknownFields(other.UnknownFields);
				return this;
			}

			// Token: 0x06001219 RID: 4633 RVA: 0x0004014C File Offset: 0x0003E34C
			public override objectTakeDamage.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x0600121A RID: 4634 RVA: 0x0004015C File Offset: 0x0003E35C
			public override objectTakeDamage.Builder MergeFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
			{
				this.PrepareBuilder();
				UnknownFieldSet.Builder builder = null;
				uint num;
				string text;
				while (input.ReadTag(ref num, ref text))
				{
					if (num == 0u && text != null)
					{
						int num2 = Array.BinarySearch<string>(objectTakeDamage._objectTakeDamageFieldNames, text, StringComparer.Ordinal);
						if (num2 < 0)
						{
							if (builder == null)
							{
								builder = UnknownFieldSet.CreateBuilder(this.UnknownFields);
							}
							this.ParseUnknownField(input, builder, extensionRegistry, num, text);
							continue;
						}
						num = objectTakeDamage._objectTakeDamageFieldTags[num2];
					}
					uint num3 = num;
					if (num3 == 0u)
					{
						throw InvalidProtocolBufferException.InvalidTag();
					}
					if (num3 != 13u)
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
						this.result.hasHealth = input.ReadFloat(ref this.result.health_);
					}
				}
				if (builder != null)
				{
					this.UnknownFields = builder.Build();
				}
				return this;
			}

			// Token: 0x17000500 RID: 1280
			// (get) Token: 0x0600121B RID: 4635 RVA: 0x00040270 File Offset: 0x0003E470
			public bool HasHealth
			{
				get
				{
					return this.result.hasHealth;
				}
			}

			// Token: 0x17000501 RID: 1281
			// (get) Token: 0x0600121C RID: 4636 RVA: 0x00040280 File Offset: 0x0003E480
			// (set) Token: 0x0600121D RID: 4637 RVA: 0x00040290 File Offset: 0x0003E490
			public float Health
			{
				get
				{
					return this.result.Health;
				}
				set
				{
					this.SetHealth(value);
				}
			}

			// Token: 0x0600121E RID: 4638 RVA: 0x0004029C File Offset: 0x0003E49C
			public objectTakeDamage.Builder SetHealth(float value)
			{
				this.PrepareBuilder();
				this.result.hasHealth = true;
				this.result.health_ = value;
				return this;
			}

			// Token: 0x0600121F RID: 4639 RVA: 0x000402CC File Offset: 0x0003E4CC
			public objectTakeDamage.Builder ClearHealth()
			{
				this.PrepareBuilder();
				this.result.hasHealth = false;
				this.result.health_ = 0f;
				return this;
			}

			// Token: 0x04000994 RID: 2452
			private bool resultIsReadOnly;

			// Token: 0x04000995 RID: 2453
			private objectTakeDamage result;
		}
	}
}
