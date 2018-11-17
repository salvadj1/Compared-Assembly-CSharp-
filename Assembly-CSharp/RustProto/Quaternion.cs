using System;
using System.Diagnostics;
using System.IO;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;
using RustProto.Helpers;
using UnityEngine;

namespace RustProto
{
	// Token: 0x02000223 RID: 547
	[DebuggerNonUserCode]
	public sealed class Quaternion : GeneratedMessage<Quaternion, Quaternion.Builder>
	{
		// Token: 0x06001384 RID: 4996 RVA: 0x00042E50 File Offset: 0x00041050
		private Quaternion()
		{
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x00042E60 File Offset: 0x00041060
		static Quaternion()
		{
			object.ReferenceEquals(Common.Descriptor, null);
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x00042ECC File Offset: 0x000410CC
		public static Recycler<Quaternion, Quaternion.Builder> Recycler()
		{
			return Recycler<Quaternion, Quaternion.Builder>.Manufacture();
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06001387 RID: 4999 RVA: 0x00042ED4 File Offset: 0x000410D4
		public static Quaternion DefaultInstance
		{
			get
			{
				return Quaternion.defaultInstance;
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06001388 RID: 5000 RVA: 0x00042EDC File Offset: 0x000410DC
		public override Quaternion DefaultInstanceForType
		{
			get
			{
				return Quaternion.DefaultInstance;
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06001389 RID: 5001 RVA: 0x00042EE4 File Offset: 0x000410E4
		protected override Quaternion ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x0600138A RID: 5002 RVA: 0x00042EE8 File Offset: 0x000410E8
		public static MessageDescriptor Descriptor
		{
			get
			{
				return Common.internal__static_RustProto_Quaternion__Descriptor;
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x0600138B RID: 5003 RVA: 0x00042EF0 File Offset: 0x000410F0
		protected override FieldAccessorTable<Quaternion, Quaternion.Builder> InternalFieldAccessors
		{
			get
			{
				return Common.internal__static_RustProto_Quaternion__FieldAccessorTable;
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x0600138C RID: 5004 RVA: 0x00042EF8 File Offset: 0x000410F8
		public bool HasX
		{
			get
			{
				return this.hasX;
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x0600138D RID: 5005 RVA: 0x00042F00 File Offset: 0x00041100
		public float X
		{
			get
			{
				return this.x_;
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x0600138E RID: 5006 RVA: 0x00042F08 File Offset: 0x00041108
		public bool HasY
		{
			get
			{
				return this.hasY;
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x0600138F RID: 5007 RVA: 0x00042F10 File Offset: 0x00041110
		public float Y
		{
			get
			{
				return this.y_;
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001390 RID: 5008 RVA: 0x00042F18 File Offset: 0x00041118
		public bool HasZ
		{
			get
			{
				return this.hasZ;
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001391 RID: 5009 RVA: 0x00042F20 File Offset: 0x00041120
		public float Z
		{
			get
			{
				return this.z_;
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06001392 RID: 5010 RVA: 0x00042F28 File Offset: 0x00041128
		public bool HasW
		{
			get
			{
				return this.hasW;
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001393 RID: 5011 RVA: 0x00042F30 File Offset: 0x00041130
		public float W
		{
			get
			{
				return this.w_;
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06001394 RID: 5012 RVA: 0x00042F38 File Offset: 0x00041138
		public override bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x00042F3C File Offset: 0x0004113C
		public override void WriteTo(ICodedOutputStream output)
		{
			int serializedSize = this.SerializedSize;
			string[] quaternionFieldNames = Quaternion._quaternionFieldNames;
			if (this.hasX)
			{
				output.WriteFloat(1, quaternionFieldNames[1], this.X);
			}
			if (this.hasY)
			{
				output.WriteFloat(2, quaternionFieldNames[2], this.Y);
			}
			if (this.hasZ)
			{
				output.WriteFloat(3, quaternionFieldNames[3], this.Z);
			}
			if (this.hasW)
			{
				output.WriteFloat(4, quaternionFieldNames[0], this.W);
			}
			this.UnknownFields.WriteTo(output);
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001396 RID: 5014 RVA: 0x00042FD0 File Offset: 0x000411D0
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
				if (this.hasX)
				{
					num += CodedOutputStream.ComputeFloatSize(1, this.X);
				}
				if (this.hasY)
				{
					num += CodedOutputStream.ComputeFloatSize(2, this.Y);
				}
				if (this.hasZ)
				{
					num += CodedOutputStream.ComputeFloatSize(3, this.Z);
				}
				if (this.hasW)
				{
					num += CodedOutputStream.ComputeFloatSize(4, this.W);
				}
				num += this.UnknownFields.SerializedSize;
				this.memoizedSerializedSize = num;
				return num;
			}
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x00043070 File Offset: 0x00041270
		public static Quaternion ParseFrom(ByteString data)
		{
			return Quaternion.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x00043084 File Offset: 0x00041284
		public static Quaternion ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return Quaternion.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x00043098 File Offset: 0x00041298
		public static Quaternion ParseFrom(byte[] data)
		{
			return Quaternion.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x000430AC File Offset: 0x000412AC
		public static Quaternion ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return Quaternion.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x000430C0 File Offset: 0x000412C0
		public static Quaternion ParseFrom(Stream input)
		{
			return Quaternion.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x000430D4 File Offset: 0x000412D4
		public static Quaternion ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return Quaternion.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x000430E8 File Offset: 0x000412E8
		public static Quaternion ParseDelimitedFrom(Stream input)
		{
			return Quaternion.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x000430FC File Offset: 0x000412FC
		public static Quaternion ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return Quaternion.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x00043110 File Offset: 0x00041310
		public static Quaternion ParseFrom(ICodedInputStream input)
		{
			return Quaternion.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x00043124 File Offset: 0x00041324
		public static Quaternion ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return Quaternion.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x00043138 File Offset: 0x00041338
		private Quaternion MakeReadOnly()
		{
			return this;
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x0004313C File Offset: 0x0004133C
		public static Quaternion.Builder CreateBuilder()
		{
			return new Quaternion.Builder();
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x00043144 File Offset: 0x00041344
		public override Quaternion.Builder ToBuilder()
		{
			return Quaternion.CreateBuilder(this);
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x0004314C File Offset: 0x0004134C
		public override Quaternion.Builder CreateBuilderForType()
		{
			return new Quaternion.Builder();
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x00043154 File Offset: 0x00041354
		public static Quaternion.Builder CreateBuilder(Quaternion prototype)
		{
			return new Quaternion.Builder(prototype);
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x0004315C File Offset: 0x0004135C
		public static implicit operator Quaternion(Quaternion v)
		{
			Quaternion result;
			using (Recycler<Quaternion, Quaternion.Builder> recycler = Quaternion.Recycler())
			{
				Quaternion.Builder builder = recycler.OpenBuilder();
				builder.SetX(v.x);
				builder.SetY(v.y);
				builder.SetZ(v.z);
				builder.SetW(v.w);
				result = builder.Build();
			}
			return result;
		}

		// Token: 0x040009E6 RID: 2534
		public const int XFieldNumber = 1;

		// Token: 0x040009E7 RID: 2535
		public const int YFieldNumber = 2;

		// Token: 0x040009E8 RID: 2536
		public const int ZFieldNumber = 3;

		// Token: 0x040009E9 RID: 2537
		public const int WFieldNumber = 4;

		// Token: 0x040009EA RID: 2538
		private static readonly Quaternion defaultInstance = new Quaternion().MakeReadOnly();

		// Token: 0x040009EB RID: 2539
		private static readonly string[] _quaternionFieldNames = new string[]
		{
			"w",
			"x",
			"y",
			"z"
		};

		// Token: 0x040009EC RID: 2540
		private static readonly uint[] _quaternionFieldTags = new uint[]
		{
			37u,
			13u,
			21u,
			29u
		};

		// Token: 0x040009ED RID: 2541
		private bool hasX;

		// Token: 0x040009EE RID: 2542
		private float x_;

		// Token: 0x040009EF RID: 2543
		private bool hasY;

		// Token: 0x040009F0 RID: 2544
		private float y_;

		// Token: 0x040009F1 RID: 2545
		private bool hasZ;

		// Token: 0x040009F2 RID: 2546
		private float z_;

		// Token: 0x040009F3 RID: 2547
		private bool hasW;

		// Token: 0x040009F4 RID: 2548
		private float w_;

		// Token: 0x040009F5 RID: 2549
		private int memoizedSerializedSize = -1;

		// Token: 0x02000224 RID: 548
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<Quaternion, Quaternion.Builder>
		{
			// Token: 0x060013A7 RID: 5031 RVA: 0x000431EC File Offset: 0x000413EC
			public Builder()
			{
				this.result = Quaternion.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x060013A8 RID: 5032 RVA: 0x00043208 File Offset: 0x00041408
			internal Builder(Quaternion cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x060013A9 RID: 5033 RVA: 0x00043220 File Offset: 0x00041420
			public void Set(Quaternion value)
			{
				this.SetX(value.x);
				this.SetY(value.y);
				this.SetZ(value.z);
				this.SetW(value.w);
			}

			// Token: 0x17000595 RID: 1429
			// (get) Token: 0x060013AA RID: 5034 RVA: 0x00043268 File Offset: 0x00041468
			protected override Quaternion.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x060013AB RID: 5035 RVA: 0x0004326C File Offset: 0x0004146C
			private Quaternion PrepareBuilder()
			{
				if (this.resultIsReadOnly)
				{
					Quaternion other = this.result;
					this.result = new Quaternion();
					this.resultIsReadOnly = false;
					this.MergeFrom(other);
				}
				return this.result;
			}

			// Token: 0x17000596 RID: 1430
			// (get) Token: 0x060013AC RID: 5036 RVA: 0x000432AC File Offset: 0x000414AC
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x17000597 RID: 1431
			// (get) Token: 0x060013AD RID: 5037 RVA: 0x000432BC File Offset: 0x000414BC
			protected override Quaternion MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x060013AE RID: 5038 RVA: 0x000432C4 File Offset: 0x000414C4
			public override Quaternion.Builder Clear()
			{
				this.result = Quaternion.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x060013AF RID: 5039 RVA: 0x000432DC File Offset: 0x000414DC
			public override Quaternion.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new Quaternion.Builder(this.result);
				}
				return new Quaternion.Builder().MergeFrom(this.result);
			}

			// Token: 0x17000598 RID: 1432
			// (get) Token: 0x060013B0 RID: 5040 RVA: 0x00043308 File Offset: 0x00041508
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return Quaternion.Descriptor;
				}
			}

			// Token: 0x17000599 RID: 1433
			// (get) Token: 0x060013B1 RID: 5041 RVA: 0x00043310 File Offset: 0x00041510
			public override Quaternion DefaultInstanceForType
			{
				get
				{
					return Quaternion.DefaultInstance;
				}
			}

			// Token: 0x060013B2 RID: 5042 RVA: 0x00043318 File Offset: 0x00041518
			public override Quaternion BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x060013B3 RID: 5043 RVA: 0x0004334C File Offset: 0x0004154C
			public override Quaternion.Builder MergeFrom(IMessage other)
			{
				if (other is Quaternion)
				{
					return this.MergeFrom((Quaternion)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x060013B4 RID: 5044 RVA: 0x00043370 File Offset: 0x00041570
			public override Quaternion.Builder MergeFrom(Quaternion other)
			{
				if (other == Quaternion.DefaultInstance)
				{
					return this;
				}
				this.PrepareBuilder();
				if (other.HasX)
				{
					this.X = other.X;
				}
				if (other.HasY)
				{
					this.Y = other.Y;
				}
				if (other.HasZ)
				{
					this.Z = other.Z;
				}
				if (other.HasW)
				{
					this.W = other.W;
				}
				this.MergeUnknownFields(other.UnknownFields);
				return this;
			}

			// Token: 0x060013B5 RID: 5045 RVA: 0x000433FC File Offset: 0x000415FC
			public override Quaternion.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x060013B6 RID: 5046 RVA: 0x0004340C File Offset: 0x0004160C
			public override Quaternion.Builder MergeFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
			{
				this.PrepareBuilder();
				UnknownFieldSet.Builder builder = null;
				uint num;
				string text;
				while (input.ReadTag(ref num, ref text))
				{
					if (num == 0u && text != null)
					{
						int num2 = Array.BinarySearch<string>(Quaternion._quaternionFieldNames, text, StringComparer.Ordinal);
						if (num2 < 0)
						{
							if (builder == null)
							{
								builder = UnknownFieldSet.CreateBuilder(this.UnknownFields);
							}
							this.ParseUnknownField(input, builder, extensionRegistry, num, text);
							continue;
						}
						num = Quaternion._quaternionFieldTags[num2];
					}
					uint num3 = num;
					if (num3 == 0u)
					{
						throw InvalidProtocolBufferException.InvalidTag();
					}
					if (num3 != 13u)
					{
						if (num3 != 21u)
						{
							if (num3 != 29u)
							{
								if (num3 != 37u)
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
									this.result.hasW = input.ReadFloat(ref this.result.w_);
								}
							}
							else
							{
								this.result.hasZ = input.ReadFloat(ref this.result.z_);
							}
						}
						else
						{
							this.result.hasY = input.ReadFloat(ref this.result.y_);
						}
					}
					else
					{
						this.result.hasX = input.ReadFloat(ref this.result.x_);
					}
				}
				if (builder != null)
				{
					this.UnknownFields = builder.Build();
				}
				return this;
			}

			// Token: 0x1700059A RID: 1434
			// (get) Token: 0x060013B7 RID: 5047 RVA: 0x000435A0 File Offset: 0x000417A0
			public bool HasX
			{
				get
				{
					return this.result.hasX;
				}
			}

			// Token: 0x1700059B RID: 1435
			// (get) Token: 0x060013B8 RID: 5048 RVA: 0x000435B0 File Offset: 0x000417B0
			// (set) Token: 0x060013B9 RID: 5049 RVA: 0x000435C0 File Offset: 0x000417C0
			public float X
			{
				get
				{
					return this.result.X;
				}
				set
				{
					this.SetX(value);
				}
			}

			// Token: 0x060013BA RID: 5050 RVA: 0x000435CC File Offset: 0x000417CC
			public Quaternion.Builder SetX(float value)
			{
				this.PrepareBuilder();
				this.result.hasX = true;
				this.result.x_ = value;
				return this;
			}

			// Token: 0x060013BB RID: 5051 RVA: 0x000435FC File Offset: 0x000417FC
			public Quaternion.Builder ClearX()
			{
				this.PrepareBuilder();
				this.result.hasX = false;
				this.result.x_ = 0f;
				return this;
			}

			// Token: 0x1700059C RID: 1436
			// (get) Token: 0x060013BC RID: 5052 RVA: 0x00043630 File Offset: 0x00041830
			public bool HasY
			{
				get
				{
					return this.result.hasY;
				}
			}

			// Token: 0x1700059D RID: 1437
			// (get) Token: 0x060013BD RID: 5053 RVA: 0x00043640 File Offset: 0x00041840
			// (set) Token: 0x060013BE RID: 5054 RVA: 0x00043650 File Offset: 0x00041850
			public float Y
			{
				get
				{
					return this.result.Y;
				}
				set
				{
					this.SetY(value);
				}
			}

			// Token: 0x060013BF RID: 5055 RVA: 0x0004365C File Offset: 0x0004185C
			public Quaternion.Builder SetY(float value)
			{
				this.PrepareBuilder();
				this.result.hasY = true;
				this.result.y_ = value;
				return this;
			}

			// Token: 0x060013C0 RID: 5056 RVA: 0x0004368C File Offset: 0x0004188C
			public Quaternion.Builder ClearY()
			{
				this.PrepareBuilder();
				this.result.hasY = false;
				this.result.y_ = 0f;
				return this;
			}

			// Token: 0x1700059E RID: 1438
			// (get) Token: 0x060013C1 RID: 5057 RVA: 0x000436C0 File Offset: 0x000418C0
			public bool HasZ
			{
				get
				{
					return this.result.hasZ;
				}
			}

			// Token: 0x1700059F RID: 1439
			// (get) Token: 0x060013C2 RID: 5058 RVA: 0x000436D0 File Offset: 0x000418D0
			// (set) Token: 0x060013C3 RID: 5059 RVA: 0x000436E0 File Offset: 0x000418E0
			public float Z
			{
				get
				{
					return this.result.Z;
				}
				set
				{
					this.SetZ(value);
				}
			}

			// Token: 0x060013C4 RID: 5060 RVA: 0x000436EC File Offset: 0x000418EC
			public Quaternion.Builder SetZ(float value)
			{
				this.PrepareBuilder();
				this.result.hasZ = true;
				this.result.z_ = value;
				return this;
			}

			// Token: 0x060013C5 RID: 5061 RVA: 0x0004371C File Offset: 0x0004191C
			public Quaternion.Builder ClearZ()
			{
				this.PrepareBuilder();
				this.result.hasZ = false;
				this.result.z_ = 0f;
				return this;
			}

			// Token: 0x170005A0 RID: 1440
			// (get) Token: 0x060013C6 RID: 5062 RVA: 0x00043750 File Offset: 0x00041950
			public bool HasW
			{
				get
				{
					return this.result.hasW;
				}
			}

			// Token: 0x170005A1 RID: 1441
			// (get) Token: 0x060013C7 RID: 5063 RVA: 0x00043760 File Offset: 0x00041960
			// (set) Token: 0x060013C8 RID: 5064 RVA: 0x00043770 File Offset: 0x00041970
			public float W
			{
				get
				{
					return this.result.W;
				}
				set
				{
					this.SetW(value);
				}
			}

			// Token: 0x060013C9 RID: 5065 RVA: 0x0004377C File Offset: 0x0004197C
			public Quaternion.Builder SetW(float value)
			{
				this.PrepareBuilder();
				this.result.hasW = true;
				this.result.w_ = value;
				return this;
			}

			// Token: 0x060013CA RID: 5066 RVA: 0x000437AC File Offset: 0x000419AC
			public Quaternion.Builder ClearW()
			{
				this.PrepareBuilder();
				this.result.hasW = false;
				this.result.w_ = 0f;
				return this;
			}

			// Token: 0x040009F6 RID: 2550
			private bool resultIsReadOnly;

			// Token: 0x040009F7 RID: 2551
			private Quaternion result;
		}
	}
}
