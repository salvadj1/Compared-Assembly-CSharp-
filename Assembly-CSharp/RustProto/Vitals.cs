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
	// Token: 0x02000232 RID: 562
	[DebuggerNonUserCode]
	public sealed class Vitals : GeneratedMessage<RustProto.Vitals, RustProto.Vitals.Builder>
	{
		// Token: 0x06000FCD RID: 4045 RVA: 0x0003C124 File Offset: 0x0003A324
		private Vitals()
		{
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x0003C160 File Offset: 0x0003A360
		static Vitals()
		{
			object.ReferenceEquals(RustProto.Proto.Vitals.Descriptor, null);
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x0003C1FC File Offset: 0x0003A3FC
		public static RustProto.Helpers.Recycler<RustProto.Vitals, RustProto.Vitals.Builder> Recycler()
		{
			return RustProto.Helpers.Recycler<RustProto.Vitals, RustProto.Vitals.Builder>.Manufacture();
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000FD0 RID: 4048 RVA: 0x0003C204 File Offset: 0x0003A404
		public static RustProto.Vitals DefaultInstance
		{
			get
			{
				return RustProto.Vitals.defaultInstance;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000FD1 RID: 4049 RVA: 0x0003C20C File Offset: 0x0003A40C
		public override RustProto.Vitals DefaultInstanceForType
		{
			get
			{
				return RustProto.Vitals.DefaultInstance;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000FD2 RID: 4050 RVA: 0x0003C214 File Offset: 0x0003A414
		protected override RustProto.Vitals ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000FD3 RID: 4051 RVA: 0x0003C218 File Offset: 0x0003A418
		public static MessageDescriptor Descriptor
		{
			get
			{
				return RustProto.Proto.Vitals.internal__static_RustProto_Vitals__Descriptor;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x0003C220 File Offset: 0x0003A420
		protected override FieldAccessorTable<RustProto.Vitals, RustProto.Vitals.Builder> InternalFieldAccessors
		{
			get
			{
				return RustProto.Proto.Vitals.internal__static_RustProto_Vitals__FieldAccessorTable;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x0003C228 File Offset: 0x0003A428
		public bool HasHealth
		{
			get
			{
				return this.hasHealth;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x0003C230 File Offset: 0x0003A430
		public float Health
		{
			get
			{
				return this.health_;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x0003C238 File Offset: 0x0003A438
		public bool HasHydration
		{
			get
			{
				return this.hasHydration;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x0003C240 File Offset: 0x0003A440
		public float Hydration
		{
			get
			{
				return this.hydration_;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000FD9 RID: 4057 RVA: 0x0003C248 File Offset: 0x0003A448
		public bool HasCalories
		{
			get
			{
				return this.hasCalories;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x0003C250 File Offset: 0x0003A450
		public float Calories
		{
			get
			{
				return this.calories_;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000FDB RID: 4059 RVA: 0x0003C258 File Offset: 0x0003A458
		public bool HasRadiation
		{
			get
			{
				return this.hasRadiation;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x0003C260 File Offset: 0x0003A460
		public float Radiation
		{
			get
			{
				return this.radiation_;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000FDD RID: 4061 RVA: 0x0003C268 File Offset: 0x0003A468
		public bool HasRadiationAnti
		{
			get
			{
				return this.hasRadiationAnti;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000FDE RID: 4062 RVA: 0x0003C270 File Offset: 0x0003A470
		public float RadiationAnti
		{
			get
			{
				return this.radiationAnti_;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000FDF RID: 4063 RVA: 0x0003C278 File Offset: 0x0003A478
		public bool HasBleedSpeed
		{
			get
			{
				return this.hasBleedSpeed;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x0003C280 File Offset: 0x0003A480
		public float BleedSpeed
		{
			get
			{
				return this.bleedSpeed_;
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x0003C288 File Offset: 0x0003A488
		public bool HasBleedMax
		{
			get
			{
				return this.hasBleedMax;
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000FE2 RID: 4066 RVA: 0x0003C290 File Offset: 0x0003A490
		public float BleedMax
		{
			get
			{
				return this.bleedMax_;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000FE3 RID: 4067 RVA: 0x0003C298 File Offset: 0x0003A498
		public bool HasHealSpeed
		{
			get
			{
				return this.hasHealSpeed;
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000FE4 RID: 4068 RVA: 0x0003C2A0 File Offset: 0x0003A4A0
		public float HealSpeed
		{
			get
			{
				return this.healSpeed_;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000FE5 RID: 4069 RVA: 0x0003C2A8 File Offset: 0x0003A4A8
		public bool HasHealMax
		{
			get
			{
				return this.hasHealMax;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000FE6 RID: 4070 RVA: 0x0003C2B0 File Offset: 0x0003A4B0
		public float HealMax
		{
			get
			{
				return this.healMax_;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000FE7 RID: 4071 RVA: 0x0003C2B8 File Offset: 0x0003A4B8
		public bool HasTemperature
		{
			get
			{
				return this.hasTemperature;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000FE8 RID: 4072 RVA: 0x0003C2C0 File Offset: 0x0003A4C0
		public float Temperature
		{
			get
			{
				return this.temperature_;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000FE9 RID: 4073 RVA: 0x0003C2C8 File Offset: 0x0003A4C8
		public override bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x0003C2CC File Offset: 0x0003A4CC
		public override void WriteTo(ICodedOutputStream output)
		{
			int serializedSize = this.SerializedSize;
			string[] vitalsFieldNames = RustProto.Vitals._vitalsFieldNames;
			if (this.hasHealth)
			{
				output.WriteFloat(1, vitalsFieldNames[5], this.Health);
			}
			if (this.hasHydration)
			{
				output.WriteFloat(2, vitalsFieldNames[6], this.Hydration);
			}
			if (this.hasCalories)
			{
				output.WriteFloat(3, vitalsFieldNames[2], this.Calories);
			}
			if (this.hasRadiation)
			{
				output.WriteFloat(4, vitalsFieldNames[7], this.Radiation);
			}
			if (this.hasRadiationAnti)
			{
				output.WriteFloat(5, vitalsFieldNames[8], this.RadiationAnti);
			}
			if (this.hasBleedSpeed)
			{
				output.WriteFloat(6, vitalsFieldNames[1], this.BleedSpeed);
			}
			if (this.hasBleedMax)
			{
				output.WriteFloat(7, vitalsFieldNames[0], this.BleedMax);
			}
			if (this.hasHealSpeed)
			{
				output.WriteFloat(8, vitalsFieldNames[4], this.HealSpeed);
			}
			if (this.hasHealMax)
			{
				output.WriteFloat(9, vitalsFieldNames[3], this.HealMax);
			}
			if (this.hasTemperature)
			{
				output.WriteFloat(10, vitalsFieldNames[9], this.Temperature);
			}
			this.UnknownFields.WriteTo(output);
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000FEB RID: 4075 RVA: 0x0003C404 File Offset: 0x0003A604
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
				if (this.hasHydration)
				{
					num += CodedOutputStream.ComputeFloatSize(2, this.Hydration);
				}
				if (this.hasCalories)
				{
					num += CodedOutputStream.ComputeFloatSize(3, this.Calories);
				}
				if (this.hasRadiation)
				{
					num += CodedOutputStream.ComputeFloatSize(4, this.Radiation);
				}
				if (this.hasRadiationAnti)
				{
					num += CodedOutputStream.ComputeFloatSize(5, this.RadiationAnti);
				}
				if (this.hasBleedSpeed)
				{
					num += CodedOutputStream.ComputeFloatSize(6, this.BleedSpeed);
				}
				if (this.hasBleedMax)
				{
					num += CodedOutputStream.ComputeFloatSize(7, this.BleedMax);
				}
				if (this.hasHealSpeed)
				{
					num += CodedOutputStream.ComputeFloatSize(8, this.HealSpeed);
				}
				if (this.hasHealMax)
				{
					num += CodedOutputStream.ComputeFloatSize(9, this.HealMax);
				}
				if (this.hasTemperature)
				{
					num += CodedOutputStream.ComputeFloatSize(10, this.Temperature);
				}
				num += this.UnknownFields.SerializedSize;
				this.memoizedSerializedSize = num;
				return num;
			}
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x0003C540 File Offset: 0x0003A740
		public static RustProto.Vitals ParseFrom(ByteString data)
		{
			return RustProto.Vitals.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x0003C554 File Offset: 0x0003A754
		public static RustProto.Vitals ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return RustProto.Vitals.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x0003C568 File Offset: 0x0003A768
		public static RustProto.Vitals ParseFrom(byte[] data)
		{
			return RustProto.Vitals.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x0003C57C File Offset: 0x0003A77C
		public static RustProto.Vitals ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return RustProto.Vitals.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x0003C590 File Offset: 0x0003A790
		public static RustProto.Vitals ParseFrom(Stream input)
		{
			return RustProto.Vitals.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x0003C5A4 File Offset: 0x0003A7A4
		public static RustProto.Vitals ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return RustProto.Vitals.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x0003C5B8 File Offset: 0x0003A7B8
		public static RustProto.Vitals ParseDelimitedFrom(Stream input)
		{
			return RustProto.Vitals.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x0003C5CC File Offset: 0x0003A7CC
		public static RustProto.Vitals ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return RustProto.Vitals.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x0003C5E0 File Offset: 0x0003A7E0
		public static RustProto.Vitals ParseFrom(ICodedInputStream input)
		{
			return RustProto.Vitals.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x0003C5F4 File Offset: 0x0003A7F4
		public static RustProto.Vitals ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return RustProto.Vitals.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x0003C608 File Offset: 0x0003A808
		private RustProto.Vitals MakeReadOnly()
		{
			return this;
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x0003C60C File Offset: 0x0003A80C
		public static RustProto.Vitals.Builder CreateBuilder()
		{
			return new RustProto.Vitals.Builder();
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x0003C614 File Offset: 0x0003A814
		public override RustProto.Vitals.Builder ToBuilder()
		{
			return RustProto.Vitals.CreateBuilder(this);
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x0003C61C File Offset: 0x0003A81C
		public override RustProto.Vitals.Builder CreateBuilderForType()
		{
			return new RustProto.Vitals.Builder();
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x0003C624 File Offset: 0x0003A824
		public static RustProto.Vitals.Builder CreateBuilder(RustProto.Vitals prototype)
		{
			return new RustProto.Vitals.Builder(prototype);
		}

		// Token: 0x040009C8 RID: 2504
		public const int HealthFieldNumber = 1;

		// Token: 0x040009C9 RID: 2505
		public const int HydrationFieldNumber = 2;

		// Token: 0x040009CA RID: 2506
		public const int CaloriesFieldNumber = 3;

		// Token: 0x040009CB RID: 2507
		public const int RadiationFieldNumber = 4;

		// Token: 0x040009CC RID: 2508
		public const int RadiationAntiFieldNumber = 5;

		// Token: 0x040009CD RID: 2509
		public const int BleedSpeedFieldNumber = 6;

		// Token: 0x040009CE RID: 2510
		public const int BleedMaxFieldNumber = 7;

		// Token: 0x040009CF RID: 2511
		public const int HealSpeedFieldNumber = 8;

		// Token: 0x040009D0 RID: 2512
		public const int HealMaxFieldNumber = 9;

		// Token: 0x040009D1 RID: 2513
		public const int TemperatureFieldNumber = 10;

		// Token: 0x040009D2 RID: 2514
		private static readonly RustProto.Vitals defaultInstance = new RustProto.Vitals().MakeReadOnly();

		// Token: 0x040009D3 RID: 2515
		private static readonly string[] _vitalsFieldNames = new string[]
		{
			"bleed_max",
			"bleed_speed",
			"calories",
			"heal_max",
			"heal_speed",
			"health",
			"hydration",
			"radiation",
			"radiation_anti",
			"temperature"
		};

		// Token: 0x040009D4 RID: 2516
		private static readonly uint[] _vitalsFieldTags = new uint[]
		{
			61u,
			53u,
			29u,
			77u,
			69u,
			13u,
			21u,
			37u,
			45u,
			85u
		};

		// Token: 0x040009D5 RID: 2517
		private bool hasHealth;

		// Token: 0x040009D6 RID: 2518
		private float health_ = 100f;

		// Token: 0x040009D7 RID: 2519
		private bool hasHydration;

		// Token: 0x040009D8 RID: 2520
		private float hydration_ = 30f;

		// Token: 0x040009D9 RID: 2521
		private bool hasCalories;

		// Token: 0x040009DA RID: 2522
		private float calories_ = 1000f;

		// Token: 0x040009DB RID: 2523
		private bool hasRadiation;

		// Token: 0x040009DC RID: 2524
		private float radiation_;

		// Token: 0x040009DD RID: 2525
		private bool hasRadiationAnti;

		// Token: 0x040009DE RID: 2526
		private float radiationAnti_;

		// Token: 0x040009DF RID: 2527
		private bool hasBleedSpeed;

		// Token: 0x040009E0 RID: 2528
		private float bleedSpeed_;

		// Token: 0x040009E1 RID: 2529
		private bool hasBleedMax;

		// Token: 0x040009E2 RID: 2530
		private float bleedMax_;

		// Token: 0x040009E3 RID: 2531
		private bool hasHealSpeed;

		// Token: 0x040009E4 RID: 2532
		private float healSpeed_;

		// Token: 0x040009E5 RID: 2533
		private bool hasHealMax;

		// Token: 0x040009E6 RID: 2534
		private float healMax_;

		// Token: 0x040009E7 RID: 2535
		private bool hasTemperature;

		// Token: 0x040009E8 RID: 2536
		private float temperature_;

		// Token: 0x040009E9 RID: 2537
		private int memoizedSerializedSize = -1;

		// Token: 0x02000233 RID: 563
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<RustProto.Vitals, RustProto.Vitals.Builder>
		{
			// Token: 0x06000FFB RID: 4091 RVA: 0x0003C62C File Offset: 0x0003A82C
			public Builder()
			{
				this.result = RustProto.Vitals.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x06000FFC RID: 4092 RVA: 0x0003C648 File Offset: 0x0003A848
			internal Builder(RustProto.Vitals cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x170003F1 RID: 1009
			// (get) Token: 0x06000FFD RID: 4093 RVA: 0x0003C660 File Offset: 0x0003A860
			protected override RustProto.Vitals.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x06000FFE RID: 4094 RVA: 0x0003C664 File Offset: 0x0003A864
			private RustProto.Vitals PrepareBuilder()
			{
				if (this.resultIsReadOnly)
				{
					RustProto.Vitals other = this.result;
					this.result = new RustProto.Vitals();
					this.resultIsReadOnly = false;
					this.MergeFrom(other);
				}
				return this.result;
			}

			// Token: 0x170003F2 RID: 1010
			// (get) Token: 0x06000FFF RID: 4095 RVA: 0x0003C6A4 File Offset: 0x0003A8A4
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x170003F3 RID: 1011
			// (get) Token: 0x06001000 RID: 4096 RVA: 0x0003C6B4 File Offset: 0x0003A8B4
			protected override RustProto.Vitals MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x06001001 RID: 4097 RVA: 0x0003C6BC File Offset: 0x0003A8BC
			public override RustProto.Vitals.Builder Clear()
			{
				this.result = RustProto.Vitals.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x06001002 RID: 4098 RVA: 0x0003C6D4 File Offset: 0x0003A8D4
			public override RustProto.Vitals.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new RustProto.Vitals.Builder(this.result);
				}
				return new RustProto.Vitals.Builder().MergeFrom(this.result);
			}

			// Token: 0x170003F4 RID: 1012
			// (get) Token: 0x06001003 RID: 4099 RVA: 0x0003C700 File Offset: 0x0003A900
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return RustProto.Vitals.Descriptor;
				}
			}

			// Token: 0x170003F5 RID: 1013
			// (get) Token: 0x06001004 RID: 4100 RVA: 0x0003C708 File Offset: 0x0003A908
			public override RustProto.Vitals DefaultInstanceForType
			{
				get
				{
					return RustProto.Vitals.DefaultInstance;
				}
			}

			// Token: 0x06001005 RID: 4101 RVA: 0x0003C710 File Offset: 0x0003A910
			public override RustProto.Vitals BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x06001006 RID: 4102 RVA: 0x0003C744 File Offset: 0x0003A944
			public override RustProto.Vitals.Builder MergeFrom(IMessage other)
			{
				if (other is RustProto.Vitals)
				{
					return this.MergeFrom((RustProto.Vitals)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x06001007 RID: 4103 RVA: 0x0003C768 File Offset: 0x0003A968
			public override RustProto.Vitals.Builder MergeFrom(RustProto.Vitals other)
			{
				if (other == RustProto.Vitals.DefaultInstance)
				{
					return this;
				}
				this.PrepareBuilder();
				if (other.HasHealth)
				{
					this.Health = other.Health;
				}
				if (other.HasHydration)
				{
					this.Hydration = other.Hydration;
				}
				if (other.HasCalories)
				{
					this.Calories = other.Calories;
				}
				if (other.HasRadiation)
				{
					this.Radiation = other.Radiation;
				}
				if (other.HasRadiationAnti)
				{
					this.RadiationAnti = other.RadiationAnti;
				}
				if (other.HasBleedSpeed)
				{
					this.BleedSpeed = other.BleedSpeed;
				}
				if (other.HasBleedMax)
				{
					this.BleedMax = other.BleedMax;
				}
				if (other.HasHealSpeed)
				{
					this.HealSpeed = other.HealSpeed;
				}
				if (other.HasHealMax)
				{
					this.HealMax = other.HealMax;
				}
				if (other.HasTemperature)
				{
					this.Temperature = other.Temperature;
				}
				this.MergeUnknownFields(other.UnknownFields);
				return this;
			}

			// Token: 0x06001008 RID: 4104 RVA: 0x0003C880 File Offset: 0x0003AA80
			public override RustProto.Vitals.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x06001009 RID: 4105 RVA: 0x0003C890 File Offset: 0x0003AA90
			public override RustProto.Vitals.Builder MergeFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
			{
				this.PrepareBuilder();
				UnknownFieldSet.Builder builder = null;
				uint num;
				string text;
				while (input.ReadTag(ref num, ref text))
				{
					if (num == 0u && text != null)
					{
						int num2 = Array.BinarySearch<string>(RustProto.Vitals._vitalsFieldNames, text, StringComparer.Ordinal);
						if (num2 < 0)
						{
							if (builder == null)
							{
								builder = UnknownFieldSet.CreateBuilder(this.UnknownFields);
							}
							this.ParseUnknownField(input, builder, extensionRegistry, num, text);
							continue;
						}
						num = RustProto.Vitals._vitalsFieldTags[num2];
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
									if (num3 != 45u)
									{
										if (num3 != 53u)
										{
											if (num3 != 61u)
											{
												if (num3 != 69u)
												{
													if (num3 != 77u)
													{
														if (num3 != 85u)
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
															this.result.hasTemperature = input.ReadFloat(ref this.result.temperature_);
														}
													}
													else
													{
														this.result.hasHealMax = input.ReadFloat(ref this.result.healMax_);
													}
												}
												else
												{
													this.result.hasHealSpeed = input.ReadFloat(ref this.result.healSpeed_);
												}
											}
											else
											{
												this.result.hasBleedMax = input.ReadFloat(ref this.result.bleedMax_);
											}
										}
										else
										{
											this.result.hasBleedSpeed = input.ReadFloat(ref this.result.bleedSpeed_);
										}
									}
									else
									{
										this.result.hasRadiationAnti = input.ReadFloat(ref this.result.radiationAnti_);
									}
								}
								else
								{
									this.result.hasRadiation = input.ReadFloat(ref this.result.radiation_);
								}
							}
							else
							{
								this.result.hasCalories = input.ReadFloat(ref this.result.calories_);
							}
						}
						else
						{
							this.result.hasHydration = input.ReadFloat(ref this.result.hydration_);
						}
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

			// Token: 0x170003F6 RID: 1014
			// (get) Token: 0x0600100A RID: 4106 RVA: 0x0003CB20 File Offset: 0x0003AD20
			public bool HasHealth
			{
				get
				{
					return this.result.hasHealth;
				}
			}

			// Token: 0x170003F7 RID: 1015
			// (get) Token: 0x0600100B RID: 4107 RVA: 0x0003CB30 File Offset: 0x0003AD30
			// (set) Token: 0x0600100C RID: 4108 RVA: 0x0003CB40 File Offset: 0x0003AD40
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

			// Token: 0x0600100D RID: 4109 RVA: 0x0003CB4C File Offset: 0x0003AD4C
			public RustProto.Vitals.Builder SetHealth(float value)
			{
				this.PrepareBuilder();
				this.result.hasHealth = true;
				this.result.health_ = value;
				return this;
			}

			// Token: 0x0600100E RID: 4110 RVA: 0x0003CB7C File Offset: 0x0003AD7C
			public RustProto.Vitals.Builder ClearHealth()
			{
				this.PrepareBuilder();
				this.result.hasHealth = false;
				this.result.health_ = 100f;
				return this;
			}

			// Token: 0x170003F8 RID: 1016
			// (get) Token: 0x0600100F RID: 4111 RVA: 0x0003CBB0 File Offset: 0x0003ADB0
			public bool HasHydration
			{
				get
				{
					return this.result.hasHydration;
				}
			}

			// Token: 0x170003F9 RID: 1017
			// (get) Token: 0x06001010 RID: 4112 RVA: 0x0003CBC0 File Offset: 0x0003ADC0
			// (set) Token: 0x06001011 RID: 4113 RVA: 0x0003CBD0 File Offset: 0x0003ADD0
			public float Hydration
			{
				get
				{
					return this.result.Hydration;
				}
				set
				{
					this.SetHydration(value);
				}
			}

			// Token: 0x06001012 RID: 4114 RVA: 0x0003CBDC File Offset: 0x0003ADDC
			public RustProto.Vitals.Builder SetHydration(float value)
			{
				this.PrepareBuilder();
				this.result.hasHydration = true;
				this.result.hydration_ = value;
				return this;
			}

			// Token: 0x06001013 RID: 4115 RVA: 0x0003CC0C File Offset: 0x0003AE0C
			public RustProto.Vitals.Builder ClearHydration()
			{
				this.PrepareBuilder();
				this.result.hasHydration = false;
				this.result.hydration_ = 30f;
				return this;
			}

			// Token: 0x170003FA RID: 1018
			// (get) Token: 0x06001014 RID: 4116 RVA: 0x0003CC40 File Offset: 0x0003AE40
			public bool HasCalories
			{
				get
				{
					return this.result.hasCalories;
				}
			}

			// Token: 0x170003FB RID: 1019
			// (get) Token: 0x06001015 RID: 4117 RVA: 0x0003CC50 File Offset: 0x0003AE50
			// (set) Token: 0x06001016 RID: 4118 RVA: 0x0003CC60 File Offset: 0x0003AE60
			public float Calories
			{
				get
				{
					return this.result.Calories;
				}
				set
				{
					this.SetCalories(value);
				}
			}

			// Token: 0x06001017 RID: 4119 RVA: 0x0003CC6C File Offset: 0x0003AE6C
			public RustProto.Vitals.Builder SetCalories(float value)
			{
				this.PrepareBuilder();
				this.result.hasCalories = true;
				this.result.calories_ = value;
				return this;
			}

			// Token: 0x06001018 RID: 4120 RVA: 0x0003CC9C File Offset: 0x0003AE9C
			public RustProto.Vitals.Builder ClearCalories()
			{
				this.PrepareBuilder();
				this.result.hasCalories = false;
				this.result.calories_ = 1000f;
				return this;
			}

			// Token: 0x170003FC RID: 1020
			// (get) Token: 0x06001019 RID: 4121 RVA: 0x0003CCD0 File Offset: 0x0003AED0
			public bool HasRadiation
			{
				get
				{
					return this.result.hasRadiation;
				}
			}

			// Token: 0x170003FD RID: 1021
			// (get) Token: 0x0600101A RID: 4122 RVA: 0x0003CCE0 File Offset: 0x0003AEE0
			// (set) Token: 0x0600101B RID: 4123 RVA: 0x0003CCF0 File Offset: 0x0003AEF0
			public float Radiation
			{
				get
				{
					return this.result.Radiation;
				}
				set
				{
					this.SetRadiation(value);
				}
			}

			// Token: 0x0600101C RID: 4124 RVA: 0x0003CCFC File Offset: 0x0003AEFC
			public RustProto.Vitals.Builder SetRadiation(float value)
			{
				this.PrepareBuilder();
				this.result.hasRadiation = true;
				this.result.radiation_ = value;
				return this;
			}

			// Token: 0x0600101D RID: 4125 RVA: 0x0003CD2C File Offset: 0x0003AF2C
			public RustProto.Vitals.Builder ClearRadiation()
			{
				this.PrepareBuilder();
				this.result.hasRadiation = false;
				this.result.radiation_ = 0f;
				return this;
			}

			// Token: 0x170003FE RID: 1022
			// (get) Token: 0x0600101E RID: 4126 RVA: 0x0003CD60 File Offset: 0x0003AF60
			public bool HasRadiationAnti
			{
				get
				{
					return this.result.hasRadiationAnti;
				}
			}

			// Token: 0x170003FF RID: 1023
			// (get) Token: 0x0600101F RID: 4127 RVA: 0x0003CD70 File Offset: 0x0003AF70
			// (set) Token: 0x06001020 RID: 4128 RVA: 0x0003CD80 File Offset: 0x0003AF80
			public float RadiationAnti
			{
				get
				{
					return this.result.RadiationAnti;
				}
				set
				{
					this.SetRadiationAnti(value);
				}
			}

			// Token: 0x06001021 RID: 4129 RVA: 0x0003CD8C File Offset: 0x0003AF8C
			public RustProto.Vitals.Builder SetRadiationAnti(float value)
			{
				this.PrepareBuilder();
				this.result.hasRadiationAnti = true;
				this.result.radiationAnti_ = value;
				return this;
			}

			// Token: 0x06001022 RID: 4130 RVA: 0x0003CDBC File Offset: 0x0003AFBC
			public RustProto.Vitals.Builder ClearRadiationAnti()
			{
				this.PrepareBuilder();
				this.result.hasRadiationAnti = false;
				this.result.radiationAnti_ = 0f;
				return this;
			}

			// Token: 0x17000400 RID: 1024
			// (get) Token: 0x06001023 RID: 4131 RVA: 0x0003CDF0 File Offset: 0x0003AFF0
			public bool HasBleedSpeed
			{
				get
				{
					return this.result.hasBleedSpeed;
				}
			}

			// Token: 0x17000401 RID: 1025
			// (get) Token: 0x06001024 RID: 4132 RVA: 0x0003CE00 File Offset: 0x0003B000
			// (set) Token: 0x06001025 RID: 4133 RVA: 0x0003CE10 File Offset: 0x0003B010
			public float BleedSpeed
			{
				get
				{
					return this.result.BleedSpeed;
				}
				set
				{
					this.SetBleedSpeed(value);
				}
			}

			// Token: 0x06001026 RID: 4134 RVA: 0x0003CE1C File Offset: 0x0003B01C
			public RustProto.Vitals.Builder SetBleedSpeed(float value)
			{
				this.PrepareBuilder();
				this.result.hasBleedSpeed = true;
				this.result.bleedSpeed_ = value;
				return this;
			}

			// Token: 0x06001027 RID: 4135 RVA: 0x0003CE4C File Offset: 0x0003B04C
			public RustProto.Vitals.Builder ClearBleedSpeed()
			{
				this.PrepareBuilder();
				this.result.hasBleedSpeed = false;
				this.result.bleedSpeed_ = 0f;
				return this;
			}

			// Token: 0x17000402 RID: 1026
			// (get) Token: 0x06001028 RID: 4136 RVA: 0x0003CE80 File Offset: 0x0003B080
			public bool HasBleedMax
			{
				get
				{
					return this.result.hasBleedMax;
				}
			}

			// Token: 0x17000403 RID: 1027
			// (get) Token: 0x06001029 RID: 4137 RVA: 0x0003CE90 File Offset: 0x0003B090
			// (set) Token: 0x0600102A RID: 4138 RVA: 0x0003CEA0 File Offset: 0x0003B0A0
			public float BleedMax
			{
				get
				{
					return this.result.BleedMax;
				}
				set
				{
					this.SetBleedMax(value);
				}
			}

			// Token: 0x0600102B RID: 4139 RVA: 0x0003CEAC File Offset: 0x0003B0AC
			public RustProto.Vitals.Builder SetBleedMax(float value)
			{
				this.PrepareBuilder();
				this.result.hasBleedMax = true;
				this.result.bleedMax_ = value;
				return this;
			}

			// Token: 0x0600102C RID: 4140 RVA: 0x0003CEDC File Offset: 0x0003B0DC
			public RustProto.Vitals.Builder ClearBleedMax()
			{
				this.PrepareBuilder();
				this.result.hasBleedMax = false;
				this.result.bleedMax_ = 0f;
				return this;
			}

			// Token: 0x17000404 RID: 1028
			// (get) Token: 0x0600102D RID: 4141 RVA: 0x0003CF10 File Offset: 0x0003B110
			public bool HasHealSpeed
			{
				get
				{
					return this.result.hasHealSpeed;
				}
			}

			// Token: 0x17000405 RID: 1029
			// (get) Token: 0x0600102E RID: 4142 RVA: 0x0003CF20 File Offset: 0x0003B120
			// (set) Token: 0x0600102F RID: 4143 RVA: 0x0003CF30 File Offset: 0x0003B130
			public float HealSpeed
			{
				get
				{
					return this.result.HealSpeed;
				}
				set
				{
					this.SetHealSpeed(value);
				}
			}

			// Token: 0x06001030 RID: 4144 RVA: 0x0003CF3C File Offset: 0x0003B13C
			public RustProto.Vitals.Builder SetHealSpeed(float value)
			{
				this.PrepareBuilder();
				this.result.hasHealSpeed = true;
				this.result.healSpeed_ = value;
				return this;
			}

			// Token: 0x06001031 RID: 4145 RVA: 0x0003CF6C File Offset: 0x0003B16C
			public RustProto.Vitals.Builder ClearHealSpeed()
			{
				this.PrepareBuilder();
				this.result.hasHealSpeed = false;
				this.result.healSpeed_ = 0f;
				return this;
			}

			// Token: 0x17000406 RID: 1030
			// (get) Token: 0x06001032 RID: 4146 RVA: 0x0003CFA0 File Offset: 0x0003B1A0
			public bool HasHealMax
			{
				get
				{
					return this.result.hasHealMax;
				}
			}

			// Token: 0x17000407 RID: 1031
			// (get) Token: 0x06001033 RID: 4147 RVA: 0x0003CFB0 File Offset: 0x0003B1B0
			// (set) Token: 0x06001034 RID: 4148 RVA: 0x0003CFC0 File Offset: 0x0003B1C0
			public float HealMax
			{
				get
				{
					return this.result.HealMax;
				}
				set
				{
					this.SetHealMax(value);
				}
			}

			// Token: 0x06001035 RID: 4149 RVA: 0x0003CFCC File Offset: 0x0003B1CC
			public RustProto.Vitals.Builder SetHealMax(float value)
			{
				this.PrepareBuilder();
				this.result.hasHealMax = true;
				this.result.healMax_ = value;
				return this;
			}

			// Token: 0x06001036 RID: 4150 RVA: 0x0003CFFC File Offset: 0x0003B1FC
			public RustProto.Vitals.Builder ClearHealMax()
			{
				this.PrepareBuilder();
				this.result.hasHealMax = false;
				this.result.healMax_ = 0f;
				return this;
			}

			// Token: 0x17000408 RID: 1032
			// (get) Token: 0x06001037 RID: 4151 RVA: 0x0003D030 File Offset: 0x0003B230
			public bool HasTemperature
			{
				get
				{
					return this.result.hasTemperature;
				}
			}

			// Token: 0x17000409 RID: 1033
			// (get) Token: 0x06001038 RID: 4152 RVA: 0x0003D040 File Offset: 0x0003B240
			// (set) Token: 0x06001039 RID: 4153 RVA: 0x0003D050 File Offset: 0x0003B250
			public float Temperature
			{
				get
				{
					return this.result.Temperature;
				}
				set
				{
					this.SetTemperature(value);
				}
			}

			// Token: 0x0600103A RID: 4154 RVA: 0x0003D05C File Offset: 0x0003B25C
			public RustProto.Vitals.Builder SetTemperature(float value)
			{
				this.PrepareBuilder();
				this.result.hasTemperature = true;
				this.result.temperature_ = value;
				return this;
			}

			// Token: 0x0600103B RID: 4155 RVA: 0x0003D08C File Offset: 0x0003B28C
			public RustProto.Vitals.Builder ClearTemperature()
			{
				this.PrepareBuilder();
				this.result.hasTemperature = false;
				this.result.temperature_ = 0f;
				return this;
			}

			// Token: 0x040009EA RID: 2538
			private bool resultIsReadOnly;

			// Token: 0x040009EB RID: 2539
			private RustProto.Vitals result;
		}
	}
}
