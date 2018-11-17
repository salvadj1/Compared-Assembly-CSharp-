using System;
using System.Diagnostics;
using System.IO;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;
using RustProto.Helpers;

namespace RustProto
{
	// Token: 0x02000248 RID: 584
	[DebuggerNonUserCode]
	public sealed class objectTakeDamage : GeneratedMessage<objectTakeDamage, objectTakeDamage.Builder>
	{
		// Token: 0x06001344 RID: 4932 RVA: 0x00044154 File Offset: 0x00042354
		private objectTakeDamage()
		{
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x00044164 File Offset: 0x00042364
		static objectTakeDamage()
		{
			object.ReferenceEquals(Worldsave.Descriptor, null);
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x000441B0 File Offset: 0x000423B0
		public static RustProto.Helpers.Recycler<objectTakeDamage, objectTakeDamage.Builder> Recycler()
		{
			return RustProto.Helpers.Recycler<objectTakeDamage, objectTakeDamage.Builder>.Manufacture();
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06001347 RID: 4935 RVA: 0x000441B8 File Offset: 0x000423B8
		public static objectTakeDamage DefaultInstance
		{
			get
			{
				return objectTakeDamage.defaultInstance;
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06001348 RID: 4936 RVA: 0x000441C0 File Offset: 0x000423C0
		public override objectTakeDamage DefaultInstanceForType
		{
			get
			{
				return objectTakeDamage.DefaultInstance;
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06001349 RID: 4937 RVA: 0x000441C8 File Offset: 0x000423C8
		protected override objectTakeDamage ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x0600134A RID: 4938 RVA: 0x000441CC File Offset: 0x000423CC
		public static MessageDescriptor Descriptor
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectTakeDamage__Descriptor;
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x0600134B RID: 4939 RVA: 0x000441D4 File Offset: 0x000423D4
		protected override FieldAccessorTable<objectTakeDamage, objectTakeDamage.Builder> InternalFieldAccessors
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectTakeDamage__FieldAccessorTable;
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x0600134C RID: 4940 RVA: 0x000441DC File Offset: 0x000423DC
		public bool HasHealth
		{
			get
			{
				return this.hasHealth;
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x0600134D RID: 4941 RVA: 0x000441E4 File Offset: 0x000423E4
		public float Health
		{
			get
			{
				return this.health_;
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x0600134E RID: 4942 RVA: 0x000441EC File Offset: 0x000423EC
		public override bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x000441F0 File Offset: 0x000423F0
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

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001350 RID: 4944 RVA: 0x00044234 File Offset: 0x00042434
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

		// Token: 0x06001351 RID: 4945 RVA: 0x00044284 File Offset: 0x00042484
		public static objectTakeDamage ParseFrom(ByteString data)
		{
			return objectTakeDamage.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x00044298 File Offset: 0x00042498
		public static objectTakeDamage ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return objectTakeDamage.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x000442AC File Offset: 0x000424AC
		public static objectTakeDamage ParseFrom(byte[] data)
		{
			return objectTakeDamage.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x000442C0 File Offset: 0x000424C0
		public static objectTakeDamage ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return objectTakeDamage.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x000442D4 File Offset: 0x000424D4
		public static objectTakeDamage ParseFrom(Stream input)
		{
			return objectTakeDamage.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x000442E8 File Offset: 0x000424E8
		public static objectTakeDamage ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectTakeDamage.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x000442FC File Offset: 0x000424FC
		public static objectTakeDamage ParseDelimitedFrom(Stream input)
		{
			return objectTakeDamage.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x00044310 File Offset: 0x00042510
		public static objectTakeDamage ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectTakeDamage.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x00044324 File Offset: 0x00042524
		public static objectTakeDamage ParseFrom(ICodedInputStream input)
		{
			return objectTakeDamage.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x00044338 File Offset: 0x00042538
		public static objectTakeDamage ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return objectTakeDamage.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x0004434C File Offset: 0x0004254C
		private objectTakeDamage MakeReadOnly()
		{
			return this;
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x00044350 File Offset: 0x00042550
		public static objectTakeDamage.Builder CreateBuilder()
		{
			return new objectTakeDamage.Builder();
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x00044358 File Offset: 0x00042558
		public override objectTakeDamage.Builder ToBuilder()
		{
			return objectTakeDamage.CreateBuilder(this);
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x00044360 File Offset: 0x00042560
		public override objectTakeDamage.Builder CreateBuilderForType()
		{
			return new objectTakeDamage.Builder();
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x00044368 File Offset: 0x00042568
		public static objectTakeDamage.Builder CreateBuilder(objectTakeDamage prototype)
		{
			return new objectTakeDamage.Builder(prototype);
		}

		// Token: 0x04000AB0 RID: 2736
		public const int HealthFieldNumber = 1;

		// Token: 0x04000AB1 RID: 2737
		private static readonly objectTakeDamage defaultInstance = new objectTakeDamage().MakeReadOnly();

		// Token: 0x04000AB2 RID: 2738
		private static readonly string[] _objectTakeDamageFieldNames = new string[]
		{
			"health"
		};

		// Token: 0x04000AB3 RID: 2739
		private static readonly uint[] _objectTakeDamageFieldTags = new uint[]
		{
			13u
		};

		// Token: 0x04000AB4 RID: 2740
		private bool hasHealth;

		// Token: 0x04000AB5 RID: 2741
		private float health_;

		// Token: 0x04000AB6 RID: 2742
		private int memoizedSerializedSize = -1;

		// Token: 0x02000249 RID: 585
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<objectTakeDamage, objectTakeDamage.Builder>
		{
			// Token: 0x06001360 RID: 4960 RVA: 0x00044370 File Offset: 0x00042570
			public Builder()
			{
				this.result = objectTakeDamage.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x06001361 RID: 4961 RVA: 0x0004438C File Offset: 0x0004258C
			internal Builder(objectTakeDamage cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x17000543 RID: 1347
			// (get) Token: 0x06001362 RID: 4962 RVA: 0x000443A4 File Offset: 0x000425A4
			protected override objectTakeDamage.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x06001363 RID: 4963 RVA: 0x000443A8 File Offset: 0x000425A8
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

			// Token: 0x17000544 RID: 1348
			// (get) Token: 0x06001364 RID: 4964 RVA: 0x000443E8 File Offset: 0x000425E8
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x17000545 RID: 1349
			// (get) Token: 0x06001365 RID: 4965 RVA: 0x000443F8 File Offset: 0x000425F8
			protected override objectTakeDamage MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x06001366 RID: 4966 RVA: 0x00044400 File Offset: 0x00042600
			public override objectTakeDamage.Builder Clear()
			{
				this.result = objectTakeDamage.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x06001367 RID: 4967 RVA: 0x00044418 File Offset: 0x00042618
			public override objectTakeDamage.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new objectTakeDamage.Builder(this.result);
				}
				return new objectTakeDamage.Builder().MergeFrom(this.result);
			}

			// Token: 0x17000546 RID: 1350
			// (get) Token: 0x06001368 RID: 4968 RVA: 0x00044444 File Offset: 0x00042644
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return objectTakeDamage.Descriptor;
				}
			}

			// Token: 0x17000547 RID: 1351
			// (get) Token: 0x06001369 RID: 4969 RVA: 0x0004444C File Offset: 0x0004264C
			public override objectTakeDamage DefaultInstanceForType
			{
				get
				{
					return objectTakeDamage.DefaultInstance;
				}
			}

			// Token: 0x0600136A RID: 4970 RVA: 0x00044454 File Offset: 0x00042654
			public override objectTakeDamage BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x0600136B RID: 4971 RVA: 0x00044488 File Offset: 0x00042688
			public override objectTakeDamage.Builder MergeFrom(IMessage other)
			{
				if (other is objectTakeDamage)
				{
					return this.MergeFrom((objectTakeDamage)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x0600136C RID: 4972 RVA: 0x000444AC File Offset: 0x000426AC
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

			// Token: 0x0600136D RID: 4973 RVA: 0x000444F4 File Offset: 0x000426F4
			public override objectTakeDamage.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x0600136E RID: 4974 RVA: 0x00044504 File Offset: 0x00042704
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

			// Token: 0x17000548 RID: 1352
			// (get) Token: 0x0600136F RID: 4975 RVA: 0x00044618 File Offset: 0x00042818
			public bool HasHealth
			{
				get
				{
					return this.result.hasHealth;
				}
			}

			// Token: 0x17000549 RID: 1353
			// (get) Token: 0x06001370 RID: 4976 RVA: 0x00044628 File Offset: 0x00042828
			// (set) Token: 0x06001371 RID: 4977 RVA: 0x00044638 File Offset: 0x00042838
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

			// Token: 0x06001372 RID: 4978 RVA: 0x00044644 File Offset: 0x00042844
			public objectTakeDamage.Builder SetHealth(float value)
			{
				this.PrepareBuilder();
				this.result.hasHealth = true;
				this.result.health_ = value;
				return this;
			}

			// Token: 0x06001373 RID: 4979 RVA: 0x00044674 File Offset: 0x00042874
			public objectTakeDamage.Builder ClearHealth()
			{
				this.PrepareBuilder();
				this.result.hasHealth = false;
				this.result.health_ = 0f;
				return this;
			}

			// Token: 0x04000AB7 RID: 2743
			private bool resultIsReadOnly;

			// Token: 0x04000AB8 RID: 2744
			private objectTakeDamage result;
		}
	}
}
