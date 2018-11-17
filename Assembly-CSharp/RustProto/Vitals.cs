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
	// Token: 0x020001FF RID: 511
	[DebuggerNonUserCode]
	public sealed class Vitals : GeneratedMessage<Vitals, Vitals.Builder>
	{
		// Token: 0x06000E79 RID: 3705 RVA: 0x00037D7C File Offset: 0x00035F7C
		private Vitals()
		{
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x00037DB8 File Offset: 0x00035FB8
		static Vitals()
		{
			object.ReferenceEquals(Vitals.Descriptor, null);
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x00037E54 File Offset: 0x00036054
		public static Recycler<Vitals, Vitals.Builder> Recycler()
		{
			return Recycler<Vitals, Vitals.Builder>.Manufacture();
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000E7C RID: 3708 RVA: 0x00037E5C File Offset: 0x0003605C
		public static Vitals DefaultInstance
		{
			get
			{
				return Vitals.defaultInstance;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000E7D RID: 3709 RVA: 0x00037E64 File Offset: 0x00036064
		public override Vitals DefaultInstanceForType
		{
			get
			{
				return Vitals.DefaultInstance;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000E7E RID: 3710 RVA: 0x00037E6C File Offset: 0x0003606C
		protected override Vitals ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000E7F RID: 3711 RVA: 0x00037E70 File Offset: 0x00036070
		public static MessageDescriptor Descriptor
		{
			get
			{
				return Vitals.internal__static_RustProto_Vitals__Descriptor;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000E80 RID: 3712 RVA: 0x00037E78 File Offset: 0x00036078
		protected override FieldAccessorTable<Vitals, Vitals.Builder> InternalFieldAccessors
		{
			get
			{
				return Vitals.internal__static_RustProto_Vitals__FieldAccessorTable;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000E81 RID: 3713 RVA: 0x00037E80 File Offset: 0x00036080
		public bool HasHealth
		{
			get
			{
				return this.hasHealth;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000E82 RID: 3714 RVA: 0x00037E88 File Offset: 0x00036088
		public float Health
		{
			get
			{
				return this.health_;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000E83 RID: 3715 RVA: 0x00037E90 File Offset: 0x00036090
		public bool HasHydration
		{
			get
			{
				return this.hasHydration;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000E84 RID: 3716 RVA: 0x00037E98 File Offset: 0x00036098
		public float Hydration
		{
			get
			{
				return this.hydration_;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000E85 RID: 3717 RVA: 0x00037EA0 File Offset: 0x000360A0
		public bool HasCalories
		{
			get
			{
				return this.hasCalories;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000E86 RID: 3718 RVA: 0x00037EA8 File Offset: 0x000360A8
		public float Calories
		{
			get
			{
				return this.calories_;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000E87 RID: 3719 RVA: 0x00037EB0 File Offset: 0x000360B0
		public bool HasRadiation
		{
			get
			{
				return this.hasRadiation;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000E88 RID: 3720 RVA: 0x00037EB8 File Offset: 0x000360B8
		public float Radiation
		{
			get
			{
				return this.radiation_;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000E89 RID: 3721 RVA: 0x00037EC0 File Offset: 0x000360C0
		public bool HasRadiationAnti
		{
			get
			{
				return this.hasRadiationAnti;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000E8A RID: 3722 RVA: 0x00037EC8 File Offset: 0x000360C8
		public float RadiationAnti
		{
			get
			{
				return this.radiationAnti_;
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000E8B RID: 3723 RVA: 0x00037ED0 File Offset: 0x000360D0
		public bool HasBleedSpeed
		{
			get
			{
				return this.hasBleedSpeed;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000E8C RID: 3724 RVA: 0x00037ED8 File Offset: 0x000360D8
		public float BleedSpeed
		{
			get
			{
				return this.bleedSpeed_;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000E8D RID: 3725 RVA: 0x00037EE0 File Offset: 0x000360E0
		public bool HasBleedMax
		{
			get
			{
				return this.hasBleedMax;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000E8E RID: 3726 RVA: 0x00037EE8 File Offset: 0x000360E8
		public float BleedMax
		{
			get
			{
				return this.bleedMax_;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000E8F RID: 3727 RVA: 0x00037EF0 File Offset: 0x000360F0
		public bool HasHealSpeed
		{
			get
			{
				return this.hasHealSpeed;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000E90 RID: 3728 RVA: 0x00037EF8 File Offset: 0x000360F8
		public float HealSpeed
		{
			get
			{
				return this.healSpeed_;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000E91 RID: 3729 RVA: 0x00037F00 File Offset: 0x00036100
		public bool HasHealMax
		{
			get
			{
				return this.hasHealMax;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000E92 RID: 3730 RVA: 0x00037F08 File Offset: 0x00036108
		public float HealMax
		{
			get
			{
				return this.healMax_;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000E93 RID: 3731 RVA: 0x00037F10 File Offset: 0x00036110
		public bool HasTemperature
		{
			get
			{
				return this.hasTemperature;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000E94 RID: 3732 RVA: 0x00037F18 File Offset: 0x00036118
		public float Temperature
		{
			get
			{
				return this.temperature_;
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000E95 RID: 3733 RVA: 0x00037F20 File Offset: 0x00036120
		public override bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x00037F24 File Offset: 0x00036124
		public override void WriteTo(ICodedOutputStream output)
		{
			int serializedSize = this.SerializedSize;
			string[] vitalsFieldNames = Vitals._vitalsFieldNames;
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

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000E97 RID: 3735 RVA: 0x0003805C File Offset: 0x0003625C
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

		// Token: 0x06000E98 RID: 3736 RVA: 0x00038198 File Offset: 0x00036398
		public static Vitals ParseFrom(ByteString data)
		{
			return Vitals.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x000381AC File Offset: 0x000363AC
		public static Vitals ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return Vitals.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x000381C0 File Offset: 0x000363C0
		public static Vitals ParseFrom(byte[] data)
		{
			return Vitals.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x000381D4 File Offset: 0x000363D4
		public static Vitals ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return Vitals.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x000381E8 File Offset: 0x000363E8
		public static Vitals ParseFrom(Stream input)
		{
			return Vitals.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x000381FC File Offset: 0x000363FC
		public static Vitals ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return Vitals.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x00038210 File Offset: 0x00036410
		public static Vitals ParseDelimitedFrom(Stream input)
		{
			return Vitals.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x00038224 File Offset: 0x00036424
		public static Vitals ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return Vitals.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x00038238 File Offset: 0x00036438
		public static Vitals ParseFrom(ICodedInputStream input)
		{
			return Vitals.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x0003824C File Offset: 0x0003644C
		public static Vitals ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return Vitals.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x00038260 File Offset: 0x00036460
		private Vitals MakeReadOnly()
		{
			return this;
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x00038264 File Offset: 0x00036464
		public static Vitals.Builder CreateBuilder()
		{
			return new Vitals.Builder();
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x0003826C File Offset: 0x0003646C
		public override Vitals.Builder ToBuilder()
		{
			return Vitals.CreateBuilder(this);
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x00038274 File Offset: 0x00036474
		public override Vitals.Builder CreateBuilderForType()
		{
			return new Vitals.Builder();
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x0003827C File Offset: 0x0003647C
		public static Vitals.Builder CreateBuilder(Vitals prototype)
		{
			return new Vitals.Builder(prototype);
		}

		// Token: 0x040008A5 RID: 2213
		public const int HealthFieldNumber = 1;

		// Token: 0x040008A6 RID: 2214
		public const int HydrationFieldNumber = 2;

		// Token: 0x040008A7 RID: 2215
		public const int CaloriesFieldNumber = 3;

		// Token: 0x040008A8 RID: 2216
		public const int RadiationFieldNumber = 4;

		// Token: 0x040008A9 RID: 2217
		public const int RadiationAntiFieldNumber = 5;

		// Token: 0x040008AA RID: 2218
		public const int BleedSpeedFieldNumber = 6;

		// Token: 0x040008AB RID: 2219
		public const int BleedMaxFieldNumber = 7;

		// Token: 0x040008AC RID: 2220
		public const int HealSpeedFieldNumber = 8;

		// Token: 0x040008AD RID: 2221
		public const int HealMaxFieldNumber = 9;

		// Token: 0x040008AE RID: 2222
		public const int TemperatureFieldNumber = 10;

		// Token: 0x040008AF RID: 2223
		private static readonly Vitals defaultInstance = new Vitals().MakeReadOnly();

		// Token: 0x040008B0 RID: 2224
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

		// Token: 0x040008B1 RID: 2225
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

		// Token: 0x040008B2 RID: 2226
		private bool hasHealth;

		// Token: 0x040008B3 RID: 2227
		private float health_ = 100f;

		// Token: 0x040008B4 RID: 2228
		private bool hasHydration;

		// Token: 0x040008B5 RID: 2229
		private float hydration_ = 30f;

		// Token: 0x040008B6 RID: 2230
		private bool hasCalories;

		// Token: 0x040008B7 RID: 2231
		private float calories_ = 1000f;

		// Token: 0x040008B8 RID: 2232
		private bool hasRadiation;

		// Token: 0x040008B9 RID: 2233
		private float radiation_;

		// Token: 0x040008BA RID: 2234
		private bool hasRadiationAnti;

		// Token: 0x040008BB RID: 2235
		private float radiationAnti_;

		// Token: 0x040008BC RID: 2236
		private bool hasBleedSpeed;

		// Token: 0x040008BD RID: 2237
		private float bleedSpeed_;

		// Token: 0x040008BE RID: 2238
		private bool hasBleedMax;

		// Token: 0x040008BF RID: 2239
		private float bleedMax_;

		// Token: 0x040008C0 RID: 2240
		private bool hasHealSpeed;

		// Token: 0x040008C1 RID: 2241
		private float healSpeed_;

		// Token: 0x040008C2 RID: 2242
		private bool hasHealMax;

		// Token: 0x040008C3 RID: 2243
		private float healMax_;

		// Token: 0x040008C4 RID: 2244
		private bool hasTemperature;

		// Token: 0x040008C5 RID: 2245
		private float temperature_;

		// Token: 0x040008C6 RID: 2246
		private int memoizedSerializedSize = -1;

		// Token: 0x02000200 RID: 512
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<Vitals, Vitals.Builder>
		{
			// Token: 0x06000EA7 RID: 3751 RVA: 0x00038284 File Offset: 0x00036484
			public Builder()
			{
				this.result = Vitals.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x06000EA8 RID: 3752 RVA: 0x000382A0 File Offset: 0x000364A0
			internal Builder(Vitals cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x170003A9 RID: 937
			// (get) Token: 0x06000EA9 RID: 3753 RVA: 0x000382B8 File Offset: 0x000364B8
			protected override Vitals.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x06000EAA RID: 3754 RVA: 0x000382BC File Offset: 0x000364BC
			private Vitals PrepareBuilder()
			{
				if (this.resultIsReadOnly)
				{
					Vitals other = this.result;
					this.result = new Vitals();
					this.resultIsReadOnly = false;
					this.MergeFrom(other);
				}
				return this.result;
			}

			// Token: 0x170003AA RID: 938
			// (get) Token: 0x06000EAB RID: 3755 RVA: 0x000382FC File Offset: 0x000364FC
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x170003AB RID: 939
			// (get) Token: 0x06000EAC RID: 3756 RVA: 0x0003830C File Offset: 0x0003650C
			protected override Vitals MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x06000EAD RID: 3757 RVA: 0x00038314 File Offset: 0x00036514
			public override Vitals.Builder Clear()
			{
				this.result = Vitals.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x06000EAE RID: 3758 RVA: 0x0003832C File Offset: 0x0003652C
			public override Vitals.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new Vitals.Builder(this.result);
				}
				return new Vitals.Builder().MergeFrom(this.result);
			}

			// Token: 0x170003AC RID: 940
			// (get) Token: 0x06000EAF RID: 3759 RVA: 0x00038358 File Offset: 0x00036558
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return Vitals.Descriptor;
				}
			}

			// Token: 0x170003AD RID: 941
			// (get) Token: 0x06000EB0 RID: 3760 RVA: 0x00038360 File Offset: 0x00036560
			public override Vitals DefaultInstanceForType
			{
				get
				{
					return Vitals.DefaultInstance;
				}
			}

			// Token: 0x06000EB1 RID: 3761 RVA: 0x00038368 File Offset: 0x00036568
			public override Vitals BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x06000EB2 RID: 3762 RVA: 0x0003839C File Offset: 0x0003659C
			public override Vitals.Builder MergeFrom(IMessage other)
			{
				if (other is Vitals)
				{
					return this.MergeFrom((Vitals)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x06000EB3 RID: 3763 RVA: 0x000383C0 File Offset: 0x000365C0
			public override Vitals.Builder MergeFrom(Vitals other)
			{
				if (other == Vitals.DefaultInstance)
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

			// Token: 0x06000EB4 RID: 3764 RVA: 0x000384D8 File Offset: 0x000366D8
			public override Vitals.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x06000EB5 RID: 3765 RVA: 0x000384E8 File Offset: 0x000366E8
			public override Vitals.Builder MergeFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
			{
				this.PrepareBuilder();
				UnknownFieldSet.Builder builder = null;
				uint num;
				string text;
				while (input.ReadTag(ref num, ref text))
				{
					if (num == 0u && text != null)
					{
						int num2 = Array.BinarySearch<string>(Vitals._vitalsFieldNames, text, StringComparer.Ordinal);
						if (num2 < 0)
						{
							if (builder == null)
							{
								builder = UnknownFieldSet.CreateBuilder(this.UnknownFields);
							}
							this.ParseUnknownField(input, builder, extensionRegistry, num, text);
							continue;
						}
						num = Vitals._vitalsFieldTags[num2];
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

			// Token: 0x170003AE RID: 942
			// (get) Token: 0x06000EB6 RID: 3766 RVA: 0x00038778 File Offset: 0x00036978
			public bool HasHealth
			{
				get
				{
					return this.result.hasHealth;
				}
			}

			// Token: 0x170003AF RID: 943
			// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x00038788 File Offset: 0x00036988
			// (set) Token: 0x06000EB8 RID: 3768 RVA: 0x00038798 File Offset: 0x00036998
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

			// Token: 0x06000EB9 RID: 3769 RVA: 0x000387A4 File Offset: 0x000369A4
			public Vitals.Builder SetHealth(float value)
			{
				this.PrepareBuilder();
				this.result.hasHealth = true;
				this.result.health_ = value;
				return this;
			}

			// Token: 0x06000EBA RID: 3770 RVA: 0x000387D4 File Offset: 0x000369D4
			public Vitals.Builder ClearHealth()
			{
				this.PrepareBuilder();
				this.result.hasHealth = false;
				this.result.health_ = 100f;
				return this;
			}

			// Token: 0x170003B0 RID: 944
			// (get) Token: 0x06000EBB RID: 3771 RVA: 0x00038808 File Offset: 0x00036A08
			public bool HasHydration
			{
				get
				{
					return this.result.hasHydration;
				}
			}

			// Token: 0x170003B1 RID: 945
			// (get) Token: 0x06000EBC RID: 3772 RVA: 0x00038818 File Offset: 0x00036A18
			// (set) Token: 0x06000EBD RID: 3773 RVA: 0x00038828 File Offset: 0x00036A28
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

			// Token: 0x06000EBE RID: 3774 RVA: 0x00038834 File Offset: 0x00036A34
			public Vitals.Builder SetHydration(float value)
			{
				this.PrepareBuilder();
				this.result.hasHydration = true;
				this.result.hydration_ = value;
				return this;
			}

			// Token: 0x06000EBF RID: 3775 RVA: 0x00038864 File Offset: 0x00036A64
			public Vitals.Builder ClearHydration()
			{
				this.PrepareBuilder();
				this.result.hasHydration = false;
				this.result.hydration_ = 30f;
				return this;
			}

			// Token: 0x170003B2 RID: 946
			// (get) Token: 0x06000EC0 RID: 3776 RVA: 0x00038898 File Offset: 0x00036A98
			public bool HasCalories
			{
				get
				{
					return this.result.hasCalories;
				}
			}

			// Token: 0x170003B3 RID: 947
			// (get) Token: 0x06000EC1 RID: 3777 RVA: 0x000388A8 File Offset: 0x00036AA8
			// (set) Token: 0x06000EC2 RID: 3778 RVA: 0x000388B8 File Offset: 0x00036AB8
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

			// Token: 0x06000EC3 RID: 3779 RVA: 0x000388C4 File Offset: 0x00036AC4
			public Vitals.Builder SetCalories(float value)
			{
				this.PrepareBuilder();
				this.result.hasCalories = true;
				this.result.calories_ = value;
				return this;
			}

			// Token: 0x06000EC4 RID: 3780 RVA: 0x000388F4 File Offset: 0x00036AF4
			public Vitals.Builder ClearCalories()
			{
				this.PrepareBuilder();
				this.result.hasCalories = false;
				this.result.calories_ = 1000f;
				return this;
			}

			// Token: 0x170003B4 RID: 948
			// (get) Token: 0x06000EC5 RID: 3781 RVA: 0x00038928 File Offset: 0x00036B28
			public bool HasRadiation
			{
				get
				{
					return this.result.hasRadiation;
				}
			}

			// Token: 0x170003B5 RID: 949
			// (get) Token: 0x06000EC6 RID: 3782 RVA: 0x00038938 File Offset: 0x00036B38
			// (set) Token: 0x06000EC7 RID: 3783 RVA: 0x00038948 File Offset: 0x00036B48
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

			// Token: 0x06000EC8 RID: 3784 RVA: 0x00038954 File Offset: 0x00036B54
			public Vitals.Builder SetRadiation(float value)
			{
				this.PrepareBuilder();
				this.result.hasRadiation = true;
				this.result.radiation_ = value;
				return this;
			}

			// Token: 0x06000EC9 RID: 3785 RVA: 0x00038984 File Offset: 0x00036B84
			public Vitals.Builder ClearRadiation()
			{
				this.PrepareBuilder();
				this.result.hasRadiation = false;
				this.result.radiation_ = 0f;
				return this;
			}

			// Token: 0x170003B6 RID: 950
			// (get) Token: 0x06000ECA RID: 3786 RVA: 0x000389B8 File Offset: 0x00036BB8
			public bool HasRadiationAnti
			{
				get
				{
					return this.result.hasRadiationAnti;
				}
			}

			// Token: 0x170003B7 RID: 951
			// (get) Token: 0x06000ECB RID: 3787 RVA: 0x000389C8 File Offset: 0x00036BC8
			// (set) Token: 0x06000ECC RID: 3788 RVA: 0x000389D8 File Offset: 0x00036BD8
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

			// Token: 0x06000ECD RID: 3789 RVA: 0x000389E4 File Offset: 0x00036BE4
			public Vitals.Builder SetRadiationAnti(float value)
			{
				this.PrepareBuilder();
				this.result.hasRadiationAnti = true;
				this.result.radiationAnti_ = value;
				return this;
			}

			// Token: 0x06000ECE RID: 3790 RVA: 0x00038A14 File Offset: 0x00036C14
			public Vitals.Builder ClearRadiationAnti()
			{
				this.PrepareBuilder();
				this.result.hasRadiationAnti = false;
				this.result.radiationAnti_ = 0f;
				return this;
			}

			// Token: 0x170003B8 RID: 952
			// (get) Token: 0x06000ECF RID: 3791 RVA: 0x00038A48 File Offset: 0x00036C48
			public bool HasBleedSpeed
			{
				get
				{
					return this.result.hasBleedSpeed;
				}
			}

			// Token: 0x170003B9 RID: 953
			// (get) Token: 0x06000ED0 RID: 3792 RVA: 0x00038A58 File Offset: 0x00036C58
			// (set) Token: 0x06000ED1 RID: 3793 RVA: 0x00038A68 File Offset: 0x00036C68
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

			// Token: 0x06000ED2 RID: 3794 RVA: 0x00038A74 File Offset: 0x00036C74
			public Vitals.Builder SetBleedSpeed(float value)
			{
				this.PrepareBuilder();
				this.result.hasBleedSpeed = true;
				this.result.bleedSpeed_ = value;
				return this;
			}

			// Token: 0x06000ED3 RID: 3795 RVA: 0x00038AA4 File Offset: 0x00036CA4
			public Vitals.Builder ClearBleedSpeed()
			{
				this.PrepareBuilder();
				this.result.hasBleedSpeed = false;
				this.result.bleedSpeed_ = 0f;
				return this;
			}

			// Token: 0x170003BA RID: 954
			// (get) Token: 0x06000ED4 RID: 3796 RVA: 0x00038AD8 File Offset: 0x00036CD8
			public bool HasBleedMax
			{
				get
				{
					return this.result.hasBleedMax;
				}
			}

			// Token: 0x170003BB RID: 955
			// (get) Token: 0x06000ED5 RID: 3797 RVA: 0x00038AE8 File Offset: 0x00036CE8
			// (set) Token: 0x06000ED6 RID: 3798 RVA: 0x00038AF8 File Offset: 0x00036CF8
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

			// Token: 0x06000ED7 RID: 3799 RVA: 0x00038B04 File Offset: 0x00036D04
			public Vitals.Builder SetBleedMax(float value)
			{
				this.PrepareBuilder();
				this.result.hasBleedMax = true;
				this.result.bleedMax_ = value;
				return this;
			}

			// Token: 0x06000ED8 RID: 3800 RVA: 0x00038B34 File Offset: 0x00036D34
			public Vitals.Builder ClearBleedMax()
			{
				this.PrepareBuilder();
				this.result.hasBleedMax = false;
				this.result.bleedMax_ = 0f;
				return this;
			}

			// Token: 0x170003BC RID: 956
			// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x00038B68 File Offset: 0x00036D68
			public bool HasHealSpeed
			{
				get
				{
					return this.result.hasHealSpeed;
				}
			}

			// Token: 0x170003BD RID: 957
			// (get) Token: 0x06000EDA RID: 3802 RVA: 0x00038B78 File Offset: 0x00036D78
			// (set) Token: 0x06000EDB RID: 3803 RVA: 0x00038B88 File Offset: 0x00036D88
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

			// Token: 0x06000EDC RID: 3804 RVA: 0x00038B94 File Offset: 0x00036D94
			public Vitals.Builder SetHealSpeed(float value)
			{
				this.PrepareBuilder();
				this.result.hasHealSpeed = true;
				this.result.healSpeed_ = value;
				return this;
			}

			// Token: 0x06000EDD RID: 3805 RVA: 0x00038BC4 File Offset: 0x00036DC4
			public Vitals.Builder ClearHealSpeed()
			{
				this.PrepareBuilder();
				this.result.hasHealSpeed = false;
				this.result.healSpeed_ = 0f;
				return this;
			}

			// Token: 0x170003BE RID: 958
			// (get) Token: 0x06000EDE RID: 3806 RVA: 0x00038BF8 File Offset: 0x00036DF8
			public bool HasHealMax
			{
				get
				{
					return this.result.hasHealMax;
				}
			}

			// Token: 0x170003BF RID: 959
			// (get) Token: 0x06000EDF RID: 3807 RVA: 0x00038C08 File Offset: 0x00036E08
			// (set) Token: 0x06000EE0 RID: 3808 RVA: 0x00038C18 File Offset: 0x00036E18
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

			// Token: 0x06000EE1 RID: 3809 RVA: 0x00038C24 File Offset: 0x00036E24
			public Vitals.Builder SetHealMax(float value)
			{
				this.PrepareBuilder();
				this.result.hasHealMax = true;
				this.result.healMax_ = value;
				return this;
			}

			// Token: 0x06000EE2 RID: 3810 RVA: 0x00038C54 File Offset: 0x00036E54
			public Vitals.Builder ClearHealMax()
			{
				this.PrepareBuilder();
				this.result.hasHealMax = false;
				this.result.healMax_ = 0f;
				return this;
			}

			// Token: 0x170003C0 RID: 960
			// (get) Token: 0x06000EE3 RID: 3811 RVA: 0x00038C88 File Offset: 0x00036E88
			public bool HasTemperature
			{
				get
				{
					return this.result.hasTemperature;
				}
			}

			// Token: 0x170003C1 RID: 961
			// (get) Token: 0x06000EE4 RID: 3812 RVA: 0x00038C98 File Offset: 0x00036E98
			// (set) Token: 0x06000EE5 RID: 3813 RVA: 0x00038CA8 File Offset: 0x00036EA8
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

			// Token: 0x06000EE6 RID: 3814 RVA: 0x00038CB4 File Offset: 0x00036EB4
			public Vitals.Builder SetTemperature(float value)
			{
				this.PrepareBuilder();
				this.result.hasTemperature = true;
				this.result.temperature_ = value;
				return this;
			}

			// Token: 0x06000EE7 RID: 3815 RVA: 0x00038CE4 File Offset: 0x00036EE4
			public Vitals.Builder ClearTemperature()
			{
				this.PrepareBuilder();
				this.result.hasTemperature = false;
				this.result.temperature_ = 0f;
				return this;
			}

			// Token: 0x040008C7 RID: 2247
			private bool resultIsReadOnly;

			// Token: 0x040008C8 RID: 2248
			private Vitals result;
		}
	}
}
