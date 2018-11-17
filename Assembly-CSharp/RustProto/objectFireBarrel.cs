using System;
using System.Diagnostics;
using System.IO;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;
using RustProto.Helpers;

namespace RustProto
{
	// Token: 0x0200024A RID: 586
	[DebuggerNonUserCode]
	public sealed class objectFireBarrel : GeneratedMessage<objectFireBarrel, objectFireBarrel.Builder>
	{
		// Token: 0x06001374 RID: 4980 RVA: 0x000446A8 File Offset: 0x000428A8
		private objectFireBarrel()
		{
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x000446B8 File Offset: 0x000428B8
		static objectFireBarrel()
		{
			object.ReferenceEquals(Worldsave.Descriptor, null);
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x000446F8 File Offset: 0x000428F8
		public static RustProto.Helpers.Recycler<objectFireBarrel, objectFireBarrel.Builder> Recycler()
		{
			return RustProto.Helpers.Recycler<objectFireBarrel, objectFireBarrel.Builder>.Manufacture();
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001377 RID: 4983 RVA: 0x00044700 File Offset: 0x00042900
		public static objectFireBarrel DefaultInstance
		{
			get
			{
				return objectFireBarrel.defaultInstance;
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001378 RID: 4984 RVA: 0x00044708 File Offset: 0x00042908
		public override objectFireBarrel DefaultInstanceForType
		{
			get
			{
				return objectFireBarrel.DefaultInstance;
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001379 RID: 4985 RVA: 0x00044710 File Offset: 0x00042910
		protected override objectFireBarrel ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x0600137A RID: 4986 RVA: 0x00044714 File Offset: 0x00042914
		public static MessageDescriptor Descriptor
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectFireBarrel__Descriptor;
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x0600137B RID: 4987 RVA: 0x0004471C File Offset: 0x0004291C
		protected override FieldAccessorTable<objectFireBarrel, objectFireBarrel.Builder> InternalFieldAccessors
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectFireBarrel__FieldAccessorTable;
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x0600137C RID: 4988 RVA: 0x00044724 File Offset: 0x00042924
		public bool HasOnFire
		{
			get
			{
				return this.hasOnFire;
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x0600137D RID: 4989 RVA: 0x0004472C File Offset: 0x0004292C
		public bool OnFire
		{
			get
			{
				return this.onFire_;
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x0600137E RID: 4990 RVA: 0x00044734 File Offset: 0x00042934
		public override bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x00044738 File Offset: 0x00042938
		public override void WriteTo(ICodedOutputStream output)
		{
			int serializedSize = this.SerializedSize;
			string[] objectFireBarrelFieldNames = objectFireBarrel._objectFireBarrelFieldNames;
			if (this.hasOnFire)
			{
				output.WriteBool(1, objectFireBarrelFieldNames[0], this.OnFire);
			}
			this.UnknownFields.WriteTo(output);
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001380 RID: 4992 RVA: 0x0004477C File Offset: 0x0004297C
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
				if (this.hasOnFire)
				{
					num += CodedOutputStream.ComputeBoolSize(1, this.OnFire);
				}
				num += this.UnknownFields.SerializedSize;
				this.memoizedSerializedSize = num;
				return num;
			}
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x000447CC File Offset: 0x000429CC
		public static objectFireBarrel ParseFrom(ByteString data)
		{
			return objectFireBarrel.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x000447E0 File Offset: 0x000429E0
		public static objectFireBarrel ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return objectFireBarrel.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x000447F4 File Offset: 0x000429F4
		public static objectFireBarrel ParseFrom(byte[] data)
		{
			return objectFireBarrel.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x00044808 File Offset: 0x00042A08
		public static objectFireBarrel ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return objectFireBarrel.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x0004481C File Offset: 0x00042A1C
		public static objectFireBarrel ParseFrom(Stream input)
		{
			return objectFireBarrel.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x00044830 File Offset: 0x00042A30
		public static objectFireBarrel ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectFireBarrel.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x00044844 File Offset: 0x00042A44
		public static objectFireBarrel ParseDelimitedFrom(Stream input)
		{
			return objectFireBarrel.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x00044858 File Offset: 0x00042A58
		public static objectFireBarrel ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectFireBarrel.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x0004486C File Offset: 0x00042A6C
		public static objectFireBarrel ParseFrom(ICodedInputStream input)
		{
			return objectFireBarrel.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x00044880 File Offset: 0x00042A80
		public static objectFireBarrel ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return objectFireBarrel.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x00044894 File Offset: 0x00042A94
		private objectFireBarrel MakeReadOnly()
		{
			return this;
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x00044898 File Offset: 0x00042A98
		public static objectFireBarrel.Builder CreateBuilder()
		{
			return new objectFireBarrel.Builder();
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x000448A0 File Offset: 0x00042AA0
		public override objectFireBarrel.Builder ToBuilder()
		{
			return objectFireBarrel.CreateBuilder(this);
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x000448A8 File Offset: 0x00042AA8
		public override objectFireBarrel.Builder CreateBuilderForType()
		{
			return new objectFireBarrel.Builder();
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x000448B0 File Offset: 0x00042AB0
		public static objectFireBarrel.Builder CreateBuilder(objectFireBarrel prototype)
		{
			return new objectFireBarrel.Builder(prototype);
		}

		// Token: 0x04000AB9 RID: 2745
		public const int OnFireFieldNumber = 1;

		// Token: 0x04000ABA RID: 2746
		private static readonly objectFireBarrel defaultInstance = new objectFireBarrel().MakeReadOnly();

		// Token: 0x04000ABB RID: 2747
		private static readonly string[] _objectFireBarrelFieldNames = new string[]
		{
			"OnFire"
		};

		// Token: 0x04000ABC RID: 2748
		private static readonly uint[] _objectFireBarrelFieldTags = new uint[]
		{
			8u
		};

		// Token: 0x04000ABD RID: 2749
		private bool hasOnFire;

		// Token: 0x04000ABE RID: 2750
		private bool onFire_;

		// Token: 0x04000ABF RID: 2751
		private int memoizedSerializedSize = -1;

		// Token: 0x0200024B RID: 587
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<objectFireBarrel, objectFireBarrel.Builder>
		{
			// Token: 0x06001390 RID: 5008 RVA: 0x000448B8 File Offset: 0x00042AB8
			public Builder()
			{
				this.result = objectFireBarrel.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x06001391 RID: 5009 RVA: 0x000448D4 File Offset: 0x00042AD4
			internal Builder(objectFireBarrel cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x17000553 RID: 1363
			// (get) Token: 0x06001392 RID: 5010 RVA: 0x000448EC File Offset: 0x00042AEC
			protected override objectFireBarrel.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x06001393 RID: 5011 RVA: 0x000448F0 File Offset: 0x00042AF0
			private objectFireBarrel PrepareBuilder()
			{
				if (this.resultIsReadOnly)
				{
					objectFireBarrel other = this.result;
					this.result = new objectFireBarrel();
					this.resultIsReadOnly = false;
					this.MergeFrom(other);
				}
				return this.result;
			}

			// Token: 0x17000554 RID: 1364
			// (get) Token: 0x06001394 RID: 5012 RVA: 0x00044930 File Offset: 0x00042B30
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x17000555 RID: 1365
			// (get) Token: 0x06001395 RID: 5013 RVA: 0x00044940 File Offset: 0x00042B40
			protected override objectFireBarrel MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x06001396 RID: 5014 RVA: 0x00044948 File Offset: 0x00042B48
			public override objectFireBarrel.Builder Clear()
			{
				this.result = objectFireBarrel.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x06001397 RID: 5015 RVA: 0x00044960 File Offset: 0x00042B60
			public override objectFireBarrel.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new objectFireBarrel.Builder(this.result);
				}
				return new objectFireBarrel.Builder().MergeFrom(this.result);
			}

			// Token: 0x17000556 RID: 1366
			// (get) Token: 0x06001398 RID: 5016 RVA: 0x0004498C File Offset: 0x00042B8C
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return objectFireBarrel.Descriptor;
				}
			}

			// Token: 0x17000557 RID: 1367
			// (get) Token: 0x06001399 RID: 5017 RVA: 0x00044994 File Offset: 0x00042B94
			public override objectFireBarrel DefaultInstanceForType
			{
				get
				{
					return objectFireBarrel.DefaultInstance;
				}
			}

			// Token: 0x0600139A RID: 5018 RVA: 0x0004499C File Offset: 0x00042B9C
			public override objectFireBarrel BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x0600139B RID: 5019 RVA: 0x000449D0 File Offset: 0x00042BD0
			public override objectFireBarrel.Builder MergeFrom(IMessage other)
			{
				if (other is objectFireBarrel)
				{
					return this.MergeFrom((objectFireBarrel)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x0600139C RID: 5020 RVA: 0x000449F4 File Offset: 0x00042BF4
			public override objectFireBarrel.Builder MergeFrom(objectFireBarrel other)
			{
				if (other == objectFireBarrel.DefaultInstance)
				{
					return this;
				}
				this.PrepareBuilder();
				if (other.HasOnFire)
				{
					this.OnFire = other.OnFire;
				}
				this.MergeUnknownFields(other.UnknownFields);
				return this;
			}

			// Token: 0x0600139D RID: 5021 RVA: 0x00044A3C File Offset: 0x00042C3C
			public override objectFireBarrel.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x0600139E RID: 5022 RVA: 0x00044A4C File Offset: 0x00042C4C
			public override objectFireBarrel.Builder MergeFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
			{
				this.PrepareBuilder();
				UnknownFieldSet.Builder builder = null;
				uint num;
				string text;
				while (input.ReadTag(ref num, ref text))
				{
					if (num == 0u && text != null)
					{
						int num2 = Array.BinarySearch<string>(objectFireBarrel._objectFireBarrelFieldNames, text, StringComparer.Ordinal);
						if (num2 < 0)
						{
							if (builder == null)
							{
								builder = UnknownFieldSet.CreateBuilder(this.UnknownFields);
							}
							this.ParseUnknownField(input, builder, extensionRegistry, num, text);
							continue;
						}
						num = objectFireBarrel._objectFireBarrelFieldTags[num2];
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
						this.result.hasOnFire = input.ReadBool(ref this.result.onFire_);
					}
				}
				if (builder != null)
				{
					this.UnknownFields = builder.Build();
				}
				return this;
			}

			// Token: 0x17000558 RID: 1368
			// (get) Token: 0x0600139F RID: 5023 RVA: 0x00044B60 File Offset: 0x00042D60
			public bool HasOnFire
			{
				get
				{
					return this.result.hasOnFire;
				}
			}

			// Token: 0x17000559 RID: 1369
			// (get) Token: 0x060013A0 RID: 5024 RVA: 0x00044B70 File Offset: 0x00042D70
			// (set) Token: 0x060013A1 RID: 5025 RVA: 0x00044B80 File Offset: 0x00042D80
			public bool OnFire
			{
				get
				{
					return this.result.OnFire;
				}
				set
				{
					this.SetOnFire(value);
				}
			}

			// Token: 0x060013A2 RID: 5026 RVA: 0x00044B8C File Offset: 0x00042D8C
			public objectFireBarrel.Builder SetOnFire(bool value)
			{
				this.PrepareBuilder();
				this.result.hasOnFire = true;
				this.result.onFire_ = value;
				return this;
			}

			// Token: 0x060013A3 RID: 5027 RVA: 0x00044BBC File Offset: 0x00042DBC
			public objectFireBarrel.Builder ClearOnFire()
			{
				this.PrepareBuilder();
				this.result.hasOnFire = false;
				this.result.onFire_ = false;
				return this;
			}

			// Token: 0x04000AC0 RID: 2752
			private bool resultIsReadOnly;

			// Token: 0x04000AC1 RID: 2753
			private objectFireBarrel result;
		}
	}
}
