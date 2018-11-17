using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Collections;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;
using RustProto.Helpers;
using RustProto.Proto;

namespace RustProto
{
	// Token: 0x02000230 RID: 560
	[DebuggerNonUserCode]
	public sealed class Avatar : GeneratedMessage<RustProto.Avatar, RustProto.Avatar.Builder>
	{
		// Token: 0x06000F50 RID: 3920 RVA: 0x0003AA14 File Offset: 0x00038C14
		private Avatar()
		{
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x0003AA50 File Offset: 0x00038C50
		static Avatar()
		{
			object.ReferenceEquals(RustProto.Proto.Avatar.Descriptor, null);
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x0003AADC File Offset: 0x00038CDC
		public static RustProto.Helpers.Recycler<RustProto.Avatar, RustProto.Avatar.Builder> Recycler()
		{
			return RustProto.Helpers.Recycler<RustProto.Avatar, RustProto.Avatar.Builder>.Manufacture();
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000F53 RID: 3923 RVA: 0x0003AAE4 File Offset: 0x00038CE4
		public static RustProto.Avatar DefaultInstance
		{
			get
			{
				return RustProto.Avatar.defaultInstance;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000F54 RID: 3924 RVA: 0x0003AAEC File Offset: 0x00038CEC
		public override RustProto.Avatar DefaultInstanceForType
		{
			get
			{
				return RustProto.Avatar.DefaultInstance;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000F55 RID: 3925 RVA: 0x0003AAF4 File Offset: 0x00038CF4
		protected override RustProto.Avatar ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000F56 RID: 3926 RVA: 0x0003AAF8 File Offset: 0x00038CF8
		public static MessageDescriptor Descriptor
		{
			get
			{
				return RustProto.Proto.Avatar.internal__static_RustProto_Avatar__Descriptor;
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000F57 RID: 3927 RVA: 0x0003AB00 File Offset: 0x00038D00
		protected override FieldAccessorTable<RustProto.Avatar, RustProto.Avatar.Builder> InternalFieldAccessors
		{
			get
			{
				return RustProto.Proto.Avatar.internal__static_RustProto_Avatar__FieldAccessorTable;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000F58 RID: 3928 RVA: 0x0003AB08 File Offset: 0x00038D08
		public bool HasPos
		{
			get
			{
				return this.hasPos;
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000F59 RID: 3929 RVA: 0x0003AB10 File Offset: 0x00038D10
		public Vector Pos
		{
			get
			{
				return this.pos_ ?? Vector.DefaultInstance;
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000F5A RID: 3930 RVA: 0x0003AB24 File Offset: 0x00038D24
		public bool HasAng
		{
			get
			{
				return this.hasAng;
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000F5B RID: 3931 RVA: 0x0003AB2C File Offset: 0x00038D2C
		public Quaternion Ang
		{
			get
			{
				return this.ang_ ?? Quaternion.DefaultInstance;
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000F5C RID: 3932 RVA: 0x0003AB40 File Offset: 0x00038D40
		public bool HasVitals
		{
			get
			{
				return this.hasVitals;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000F5D RID: 3933 RVA: 0x0003AB48 File Offset: 0x00038D48
		public RustProto.Vitals Vitals
		{
			get
			{
				return this.vitals_ ?? RustProto.Vitals.DefaultInstance;
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000F5E RID: 3934 RVA: 0x0003AB5C File Offset: 0x00038D5C
		public IList<RustProto.Blueprint> BlueprintsList
		{
			get
			{
				return this.blueprints_;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000F5F RID: 3935 RVA: 0x0003AB64 File Offset: 0x00038D64
		public int BlueprintsCount
		{
			get
			{
				return this.blueprints_.Count;
			}
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x0003AB74 File Offset: 0x00038D74
		public RustProto.Blueprint GetBlueprints(int index)
		{
			return this.blueprints_[index];
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000F61 RID: 3937 RVA: 0x0003AB84 File Offset: 0x00038D84
		public IList<RustProto.Item> InventoryList
		{
			get
			{
				return this.inventory_;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000F62 RID: 3938 RVA: 0x0003AB8C File Offset: 0x00038D8C
		public int InventoryCount
		{
			get
			{
				return this.inventory_.Count;
			}
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x0003AB9C File Offset: 0x00038D9C
		public RustProto.Item GetInventory(int index)
		{
			return this.inventory_[index];
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000F64 RID: 3940 RVA: 0x0003ABAC File Offset: 0x00038DAC
		public IList<RustProto.Item> WearableList
		{
			get
			{
				return this.wearable_;
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000F65 RID: 3941 RVA: 0x0003ABB4 File Offset: 0x00038DB4
		public int WearableCount
		{
			get
			{
				return this.wearable_.Count;
			}
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x0003ABC4 File Offset: 0x00038DC4
		public RustProto.Item GetWearable(int index)
		{
			return this.wearable_[index];
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000F67 RID: 3943 RVA: 0x0003ABD4 File Offset: 0x00038DD4
		public IList<RustProto.Item> BeltList
		{
			get
			{
				return this.belt_;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000F68 RID: 3944 RVA: 0x0003ABDC File Offset: 0x00038DDC
		public int BeltCount
		{
			get
			{
				return this.belt_.Count;
			}
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x0003ABEC File Offset: 0x00038DEC
		public RustProto.Item GetBelt(int index)
		{
			return this.belt_[index];
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000F6A RID: 3946 RVA: 0x0003ABFC File Offset: 0x00038DFC
		public bool HasAwayEvent
		{
			get
			{
				return this.hasAwayEvent;
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000F6B RID: 3947 RVA: 0x0003AC04 File Offset: 0x00038E04
		public AwayEvent AwayEvent
		{
			get
			{
				return this.awayEvent_ ?? AwayEvent.DefaultInstance;
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000F6C RID: 3948 RVA: 0x0003AC18 File Offset: 0x00038E18
		public override bool IsInitialized
		{
			get
			{
				foreach (RustProto.Blueprint blueprint in this.BlueprintsList)
				{
					if (!blueprint.IsInitialized)
					{
						return false;
					}
				}
				foreach (RustProto.Item item in this.InventoryList)
				{
					if (!item.IsInitialized)
					{
						return false;
					}
				}
				foreach (RustProto.Item item2 in this.WearableList)
				{
					if (!item2.IsInitialized)
					{
						return false;
					}
				}
				foreach (RustProto.Item item3 in this.BeltList)
				{
					if (!item3.IsInitialized)
					{
						return false;
					}
				}
				return !this.HasAwayEvent || this.AwayEvent.IsInitialized;
			}
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x0003ADD0 File Offset: 0x00038FD0
		public override void WriteTo(ICodedOutputStream output)
		{
			int serializedSize = this.SerializedSize;
			string[] avatarFieldNames = RustProto.Avatar._avatarFieldNames;
			if (this.hasPos)
			{
				output.WriteMessage(1, avatarFieldNames[5], this.Pos);
			}
			if (this.hasAng)
			{
				output.WriteMessage(2, avatarFieldNames[0], this.Ang);
			}
			if (this.hasVitals)
			{
				output.WriteMessage(3, avatarFieldNames[6], this.Vitals);
			}
			if (this.blueprints_.Count > 0)
			{
				output.WriteMessageArray<RustProto.Blueprint>(4, avatarFieldNames[3], this.blueprints_);
			}
			if (this.inventory_.Count > 0)
			{
				output.WriteMessageArray<RustProto.Item>(5, avatarFieldNames[4], this.inventory_);
			}
			if (this.wearable_.Count > 0)
			{
				output.WriteMessageArray<RustProto.Item>(6, avatarFieldNames[7], this.wearable_);
			}
			if (this.belt_.Count > 0)
			{
				output.WriteMessageArray<RustProto.Item>(7, avatarFieldNames[2], this.belt_);
			}
			if (this.hasAwayEvent)
			{
				output.WriteMessage(8, avatarFieldNames[1], this.AwayEvent);
			}
			this.UnknownFields.WriteTo(output);
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000F6E RID: 3950 RVA: 0x0003AEE8 File Offset: 0x000390E8
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
				if (this.hasPos)
				{
					num += CodedOutputStream.ComputeMessageSize(1, this.Pos);
				}
				if (this.hasAng)
				{
					num += CodedOutputStream.ComputeMessageSize(2, this.Ang);
				}
				if (this.hasVitals)
				{
					num += CodedOutputStream.ComputeMessageSize(3, this.Vitals);
				}
				foreach (RustProto.Blueprint blueprint in this.BlueprintsList)
				{
					num += CodedOutputStream.ComputeMessageSize(4, blueprint);
				}
				foreach (RustProto.Item item in this.InventoryList)
				{
					num += CodedOutputStream.ComputeMessageSize(5, item);
				}
				foreach (RustProto.Item item2 in this.WearableList)
				{
					num += CodedOutputStream.ComputeMessageSize(6, item2);
				}
				foreach (RustProto.Item item3 in this.BeltList)
				{
					num += CodedOutputStream.ComputeMessageSize(7, item3);
				}
				if (this.hasAwayEvent)
				{
					num += CodedOutputStream.ComputeMessageSize(8, this.AwayEvent);
				}
				num += this.UnknownFields.SerializedSize;
				this.memoizedSerializedSize = num;
				return num;
			}
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x0003B0F0 File Offset: 0x000392F0
		public static RustProto.Avatar ParseFrom(ByteString data)
		{
			return RustProto.Avatar.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x0003B104 File Offset: 0x00039304
		public static RustProto.Avatar ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return RustProto.Avatar.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x0003B118 File Offset: 0x00039318
		public static RustProto.Avatar ParseFrom(byte[] data)
		{
			return RustProto.Avatar.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x0003B12C File Offset: 0x0003932C
		public static RustProto.Avatar ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return RustProto.Avatar.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x0003B140 File Offset: 0x00039340
		public static RustProto.Avatar ParseFrom(Stream input)
		{
			return RustProto.Avatar.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x0003B154 File Offset: 0x00039354
		public static RustProto.Avatar ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return RustProto.Avatar.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x0003B168 File Offset: 0x00039368
		public static RustProto.Avatar ParseDelimitedFrom(Stream input)
		{
			return RustProto.Avatar.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x0003B17C File Offset: 0x0003937C
		public static RustProto.Avatar ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return RustProto.Avatar.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x0003B190 File Offset: 0x00039390
		public static RustProto.Avatar ParseFrom(ICodedInputStream input)
		{
			return RustProto.Avatar.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x0003B1A4 File Offset: 0x000393A4
		public static RustProto.Avatar ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return RustProto.Avatar.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x0003B1B8 File Offset: 0x000393B8
		private RustProto.Avatar MakeReadOnly()
		{
			this.blueprints_.MakeReadOnly();
			this.inventory_.MakeReadOnly();
			this.wearable_.MakeReadOnly();
			this.belt_.MakeReadOnly();
			return this;
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x0003B1F4 File Offset: 0x000393F4
		public static RustProto.Avatar.Builder CreateBuilder()
		{
			return new RustProto.Avatar.Builder();
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x0003B1FC File Offset: 0x000393FC
		public override RustProto.Avatar.Builder ToBuilder()
		{
			return RustProto.Avatar.CreateBuilder(this);
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x0003B204 File Offset: 0x00039404
		public override RustProto.Avatar.Builder CreateBuilderForType()
		{
			return new RustProto.Avatar.Builder();
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x0003B20C File Offset: 0x0003940C
		public static RustProto.Avatar.Builder CreateBuilder(RustProto.Avatar prototype)
		{
			return new RustProto.Avatar.Builder(prototype);
		}

		// Token: 0x040009AE RID: 2478
		public const int PosFieldNumber = 1;

		// Token: 0x040009AF RID: 2479
		public const int AngFieldNumber = 2;

		// Token: 0x040009B0 RID: 2480
		public const int VitalsFieldNumber = 3;

		// Token: 0x040009B1 RID: 2481
		public const int BlueprintsFieldNumber = 4;

		// Token: 0x040009B2 RID: 2482
		public const int InventoryFieldNumber = 5;

		// Token: 0x040009B3 RID: 2483
		public const int WearableFieldNumber = 6;

		// Token: 0x040009B4 RID: 2484
		public const int BeltFieldNumber = 7;

		// Token: 0x040009B5 RID: 2485
		public const int AwayEventFieldNumber = 8;

		// Token: 0x040009B6 RID: 2486
		private static readonly RustProto.Avatar defaultInstance = new RustProto.Avatar().MakeReadOnly();

		// Token: 0x040009B7 RID: 2487
		private static readonly string[] _avatarFieldNames = new string[]
		{
			"ang",
			"awayEvent",
			"belt",
			"blueprints",
			"inventory",
			"pos",
			"vitals",
			"wearable"
		};

		// Token: 0x040009B8 RID: 2488
		private static readonly uint[] _avatarFieldTags = new uint[]
		{
			18u,
			66u,
			58u,
			34u,
			42u,
			10u,
			26u,
			50u
		};

		// Token: 0x040009B9 RID: 2489
		private bool hasPos;

		// Token: 0x040009BA RID: 2490
		private Vector pos_;

		// Token: 0x040009BB RID: 2491
		private bool hasAng;

		// Token: 0x040009BC RID: 2492
		private Quaternion ang_;

		// Token: 0x040009BD RID: 2493
		private bool hasVitals;

		// Token: 0x040009BE RID: 2494
		private RustProto.Vitals vitals_;

		// Token: 0x040009BF RID: 2495
		private PopsicleList<RustProto.Blueprint> blueprints_ = new PopsicleList<RustProto.Blueprint>();

		// Token: 0x040009C0 RID: 2496
		private PopsicleList<RustProto.Item> inventory_ = new PopsicleList<RustProto.Item>();

		// Token: 0x040009C1 RID: 2497
		private PopsicleList<RustProto.Item> wearable_ = new PopsicleList<RustProto.Item>();

		// Token: 0x040009C2 RID: 2498
		private PopsicleList<RustProto.Item> belt_ = new PopsicleList<RustProto.Item>();

		// Token: 0x040009C3 RID: 2499
		private bool hasAwayEvent;

		// Token: 0x040009C4 RID: 2500
		private AwayEvent awayEvent_;

		// Token: 0x040009C5 RID: 2501
		private int memoizedSerializedSize = -1;

		// Token: 0x02000231 RID: 561
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<RustProto.Avatar, RustProto.Avatar.Builder>
		{
			// Token: 0x06000F7E RID: 3966 RVA: 0x0003B214 File Offset: 0x00039414
			public Builder()
			{
				this.result = RustProto.Avatar.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x06000F7F RID: 3967 RVA: 0x0003B230 File Offset: 0x00039430
			internal Builder(RustProto.Avatar cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x170003C1 RID: 961
			// (get) Token: 0x06000F80 RID: 3968 RVA: 0x0003B248 File Offset: 0x00039448
			protected override RustProto.Avatar.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x06000F81 RID: 3969 RVA: 0x0003B24C File Offset: 0x0003944C
			private RustProto.Avatar PrepareBuilder()
			{
				if (this.resultIsReadOnly)
				{
					RustProto.Avatar other = this.result;
					this.result = new RustProto.Avatar();
					this.resultIsReadOnly = false;
					this.MergeFrom(other);
				}
				return this.result;
			}

			// Token: 0x170003C2 RID: 962
			// (get) Token: 0x06000F82 RID: 3970 RVA: 0x0003B28C File Offset: 0x0003948C
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x170003C3 RID: 963
			// (get) Token: 0x06000F83 RID: 3971 RVA: 0x0003B29C File Offset: 0x0003949C
			protected override RustProto.Avatar MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x06000F84 RID: 3972 RVA: 0x0003B2A4 File Offset: 0x000394A4
			public override RustProto.Avatar.Builder Clear()
			{
				this.result = RustProto.Avatar.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x06000F85 RID: 3973 RVA: 0x0003B2BC File Offset: 0x000394BC
			public override RustProto.Avatar.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new RustProto.Avatar.Builder(this.result);
				}
				return new RustProto.Avatar.Builder().MergeFrom(this.result);
			}

			// Token: 0x170003C4 RID: 964
			// (get) Token: 0x06000F86 RID: 3974 RVA: 0x0003B2E8 File Offset: 0x000394E8
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return RustProto.Avatar.Descriptor;
				}
			}

			// Token: 0x170003C5 RID: 965
			// (get) Token: 0x06000F87 RID: 3975 RVA: 0x0003B2F0 File Offset: 0x000394F0
			public override RustProto.Avatar DefaultInstanceForType
			{
				get
				{
					return RustProto.Avatar.DefaultInstance;
				}
			}

			// Token: 0x06000F88 RID: 3976 RVA: 0x0003B2F8 File Offset: 0x000394F8
			public override RustProto.Avatar BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x06000F89 RID: 3977 RVA: 0x0003B32C File Offset: 0x0003952C
			public override RustProto.Avatar.Builder MergeFrom(IMessage other)
			{
				if (other is RustProto.Avatar)
				{
					return this.MergeFrom((RustProto.Avatar)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x06000F8A RID: 3978 RVA: 0x0003B350 File Offset: 0x00039550
			public override RustProto.Avatar.Builder MergeFrom(RustProto.Avatar other)
			{
				if (other == RustProto.Avatar.DefaultInstance)
				{
					return this;
				}
				this.PrepareBuilder();
				if (other.HasPos)
				{
					this.MergePos(other.Pos);
				}
				if (other.HasAng)
				{
					this.MergeAng(other.Ang);
				}
				if (other.HasVitals)
				{
					this.MergeVitals(other.Vitals);
				}
				if (other.blueprints_.Count != 0)
				{
					this.result.blueprints_.Add(other.blueprints_);
				}
				if (other.inventory_.Count != 0)
				{
					this.result.inventory_.Add(other.inventory_);
				}
				if (other.wearable_.Count != 0)
				{
					this.result.wearable_.Add(other.wearable_);
				}
				if (other.belt_.Count != 0)
				{
					this.result.belt_.Add(other.belt_);
				}
				if (other.HasAwayEvent)
				{
					this.MergeAwayEvent(other.AwayEvent);
				}
				this.MergeUnknownFields(other.UnknownFields);
				return this;
			}

			// Token: 0x06000F8B RID: 3979 RVA: 0x0003B478 File Offset: 0x00039678
			public override RustProto.Avatar.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x06000F8C RID: 3980 RVA: 0x0003B488 File Offset: 0x00039688
			public override RustProto.Avatar.Builder MergeFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
			{
				this.PrepareBuilder();
				UnknownFieldSet.Builder builder = null;
				uint num;
				string text;
				while (input.ReadTag(ref num, ref text))
				{
					if (num == 0u && text != null)
					{
						int num2 = Array.BinarySearch<string>(RustProto.Avatar._avatarFieldNames, text, StringComparer.Ordinal);
						if (num2 < 0)
						{
							if (builder == null)
							{
								builder = UnknownFieldSet.CreateBuilder(this.UnknownFields);
							}
							this.ParseUnknownField(input, builder, extensionRegistry, num, text);
							continue;
						}
						num = RustProto.Avatar._avatarFieldTags[num2];
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
							if (num3 != 26u)
							{
								if (num3 != 34u)
								{
									if (num3 != 42u)
									{
										if (num3 != 50u)
										{
											if (num3 != 58u)
											{
												if (num3 != 66u)
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
													AwayEvent.Builder builder2 = AwayEvent.CreateBuilder();
													if (this.result.hasAwayEvent)
													{
														builder2.MergeFrom(this.AwayEvent);
													}
													input.ReadMessage(builder2, extensionRegistry);
													this.AwayEvent = builder2.BuildPartial();
												}
											}
											else
											{
												input.ReadMessageArray<RustProto.Item>(num, text, this.result.belt_, RustProto.Item.DefaultInstance, extensionRegistry);
											}
										}
										else
										{
											input.ReadMessageArray<RustProto.Item>(num, text, this.result.wearable_, RustProto.Item.DefaultInstance, extensionRegistry);
										}
									}
									else
									{
										input.ReadMessageArray<RustProto.Item>(num, text, this.result.inventory_, RustProto.Item.DefaultInstance, extensionRegistry);
									}
								}
								else
								{
									input.ReadMessageArray<RustProto.Blueprint>(num, text, this.result.blueprints_, RustProto.Blueprint.DefaultInstance, extensionRegistry);
								}
							}
							else
							{
								RustProto.Vitals.Builder builder3 = RustProto.Vitals.CreateBuilder();
								if (this.result.hasVitals)
								{
									builder3.MergeFrom(this.Vitals);
								}
								input.ReadMessage(builder3, extensionRegistry);
								this.Vitals = builder3.BuildPartial();
							}
						}
						else
						{
							Quaternion.Builder builder4 = Quaternion.CreateBuilder();
							if (this.result.hasAng)
							{
								builder4.MergeFrom(this.Ang);
							}
							input.ReadMessage(builder4, extensionRegistry);
							this.Ang = builder4.BuildPartial();
						}
					}
					else
					{
						Vector.Builder builder5 = Vector.CreateBuilder();
						if (this.result.hasPos)
						{
							builder5.MergeFrom(this.Pos);
						}
						input.ReadMessage(builder5, extensionRegistry);
						this.Pos = builder5.BuildPartial();
					}
				}
				if (builder != null)
				{
					this.UnknownFields = builder.Build();
				}
				return this;
			}

			// Token: 0x170003C6 RID: 966
			// (get) Token: 0x06000F8D RID: 3981 RVA: 0x0003B734 File Offset: 0x00039934
			public bool HasPos
			{
				get
				{
					return this.result.hasPos;
				}
			}

			// Token: 0x170003C7 RID: 967
			// (get) Token: 0x06000F8E RID: 3982 RVA: 0x0003B744 File Offset: 0x00039944
			// (set) Token: 0x06000F8F RID: 3983 RVA: 0x0003B754 File Offset: 0x00039954
			public Vector Pos
			{
				get
				{
					return this.result.Pos;
				}
				set
				{
					this.SetPos(value);
				}
			}

			// Token: 0x06000F90 RID: 3984 RVA: 0x0003B760 File Offset: 0x00039960
			public RustProto.Avatar.Builder SetPos(Vector value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasPos = true;
				this.result.pos_ = value;
				return this;
			}

			// Token: 0x06000F91 RID: 3985 RVA: 0x0003B790 File Offset: 0x00039990
			public RustProto.Avatar.Builder SetPos(Vector.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.hasPos = true;
				this.result.pos_ = builderForValue.Build();
				return this;
			}

			// Token: 0x06000F92 RID: 3986 RVA: 0x0003B7D0 File Offset: 0x000399D0
			public RustProto.Avatar.Builder MergePos(Vector value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				if (this.result.hasPos && this.result.pos_ != Vector.DefaultInstance)
				{
					this.result.pos_ = Vector.CreateBuilder(this.result.pos_).MergeFrom(value).BuildPartial();
				}
				else
				{
					this.result.pos_ = value;
				}
				this.result.hasPos = true;
				return this;
			}

			// Token: 0x06000F93 RID: 3987 RVA: 0x0003B858 File Offset: 0x00039A58
			public RustProto.Avatar.Builder ClearPos()
			{
				this.PrepareBuilder();
				this.result.hasPos = false;
				this.result.pos_ = null;
				return this;
			}

			// Token: 0x170003C8 RID: 968
			// (get) Token: 0x06000F94 RID: 3988 RVA: 0x0003B888 File Offset: 0x00039A88
			public bool HasAng
			{
				get
				{
					return this.result.hasAng;
				}
			}

			// Token: 0x170003C9 RID: 969
			// (get) Token: 0x06000F95 RID: 3989 RVA: 0x0003B898 File Offset: 0x00039A98
			// (set) Token: 0x06000F96 RID: 3990 RVA: 0x0003B8A8 File Offset: 0x00039AA8
			public Quaternion Ang
			{
				get
				{
					return this.result.Ang;
				}
				set
				{
					this.SetAng(value);
				}
			}

			// Token: 0x06000F97 RID: 3991 RVA: 0x0003B8B4 File Offset: 0x00039AB4
			public RustProto.Avatar.Builder SetAng(Quaternion value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasAng = true;
				this.result.ang_ = value;
				return this;
			}

			// Token: 0x06000F98 RID: 3992 RVA: 0x0003B8E4 File Offset: 0x00039AE4
			public RustProto.Avatar.Builder SetAng(Quaternion.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.hasAng = true;
				this.result.ang_ = builderForValue.Build();
				return this;
			}

			// Token: 0x06000F99 RID: 3993 RVA: 0x0003B924 File Offset: 0x00039B24
			public RustProto.Avatar.Builder MergeAng(Quaternion value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				if (this.result.hasAng && this.result.ang_ != Quaternion.DefaultInstance)
				{
					this.result.ang_ = Quaternion.CreateBuilder(this.result.ang_).MergeFrom(value).BuildPartial();
				}
				else
				{
					this.result.ang_ = value;
				}
				this.result.hasAng = true;
				return this;
			}

			// Token: 0x06000F9A RID: 3994 RVA: 0x0003B9AC File Offset: 0x00039BAC
			public RustProto.Avatar.Builder ClearAng()
			{
				this.PrepareBuilder();
				this.result.hasAng = false;
				this.result.ang_ = null;
				return this;
			}

			// Token: 0x170003CA RID: 970
			// (get) Token: 0x06000F9B RID: 3995 RVA: 0x0003B9DC File Offset: 0x00039BDC
			public bool HasVitals
			{
				get
				{
					return this.result.hasVitals;
				}
			}

			// Token: 0x170003CB RID: 971
			// (get) Token: 0x06000F9C RID: 3996 RVA: 0x0003B9EC File Offset: 0x00039BEC
			// (set) Token: 0x06000F9D RID: 3997 RVA: 0x0003B9FC File Offset: 0x00039BFC
			public RustProto.Vitals Vitals
			{
				get
				{
					return this.result.Vitals;
				}
				set
				{
					this.SetVitals(value);
				}
			}

			// Token: 0x06000F9E RID: 3998 RVA: 0x0003BA08 File Offset: 0x00039C08
			public RustProto.Avatar.Builder SetVitals(RustProto.Vitals value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasVitals = true;
				this.result.vitals_ = value;
				return this;
			}

			// Token: 0x06000F9F RID: 3999 RVA: 0x0003BA38 File Offset: 0x00039C38
			public RustProto.Avatar.Builder SetVitals(RustProto.Vitals.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.hasVitals = true;
				this.result.vitals_ = builderForValue.Build();
				return this;
			}

			// Token: 0x06000FA0 RID: 4000 RVA: 0x0003BA78 File Offset: 0x00039C78
			public RustProto.Avatar.Builder MergeVitals(RustProto.Vitals value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				if (this.result.hasVitals && this.result.vitals_ != RustProto.Vitals.DefaultInstance)
				{
					this.result.vitals_ = RustProto.Vitals.CreateBuilder(this.result.vitals_).MergeFrom(value).BuildPartial();
				}
				else
				{
					this.result.vitals_ = value;
				}
				this.result.hasVitals = true;
				return this;
			}

			// Token: 0x06000FA1 RID: 4001 RVA: 0x0003BB00 File Offset: 0x00039D00
			public RustProto.Avatar.Builder ClearVitals()
			{
				this.PrepareBuilder();
				this.result.hasVitals = false;
				this.result.vitals_ = null;
				return this;
			}

			// Token: 0x170003CC RID: 972
			// (get) Token: 0x06000FA2 RID: 4002 RVA: 0x0003BB30 File Offset: 0x00039D30
			public IPopsicleList<RustProto.Blueprint> BlueprintsList
			{
				get
				{
					return this.PrepareBuilder().blueprints_;
				}
			}

			// Token: 0x170003CD RID: 973
			// (get) Token: 0x06000FA3 RID: 4003 RVA: 0x0003BB40 File Offset: 0x00039D40
			public int BlueprintsCount
			{
				get
				{
					return this.result.BlueprintsCount;
				}
			}

			// Token: 0x06000FA4 RID: 4004 RVA: 0x0003BB50 File Offset: 0x00039D50
			public RustProto.Blueprint GetBlueprints(int index)
			{
				return this.result.GetBlueprints(index);
			}

			// Token: 0x06000FA5 RID: 4005 RVA: 0x0003BB60 File Offset: 0x00039D60
			public RustProto.Avatar.Builder SetBlueprints(int index, RustProto.Blueprint value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.blueprints_[index] = value;
				return this;
			}

			// Token: 0x06000FA6 RID: 4006 RVA: 0x0003BB88 File Offset: 0x00039D88
			public RustProto.Avatar.Builder SetBlueprints(int index, RustProto.Blueprint.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.blueprints_[index] = builderForValue.Build();
				return this;
			}

			// Token: 0x06000FA7 RID: 4007 RVA: 0x0003BBC0 File Offset: 0x00039DC0
			public RustProto.Avatar.Builder AddBlueprints(RustProto.Blueprint value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.blueprints_.Add(value);
				return this;
			}

			// Token: 0x06000FA8 RID: 4008 RVA: 0x0003BBF4 File Offset: 0x00039DF4
			public RustProto.Avatar.Builder AddBlueprints(RustProto.Blueprint.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.blueprints_.Add(builderForValue.Build());
				return this;
			}

			// Token: 0x06000FA9 RID: 4009 RVA: 0x0003BC20 File Offset: 0x00039E20
			public RustProto.Avatar.Builder AddRangeBlueprints(IEnumerable<RustProto.Blueprint> values)
			{
				this.PrepareBuilder();
				this.result.blueprints_.Add(values);
				return this;
			}

			// Token: 0x06000FAA RID: 4010 RVA: 0x0003BC3C File Offset: 0x00039E3C
			public RustProto.Avatar.Builder ClearBlueprints()
			{
				this.PrepareBuilder();
				this.result.blueprints_.Clear();
				return this;
			}

			// Token: 0x170003CE RID: 974
			// (get) Token: 0x06000FAB RID: 4011 RVA: 0x0003BC58 File Offset: 0x00039E58
			public IPopsicleList<RustProto.Item> InventoryList
			{
				get
				{
					return this.PrepareBuilder().inventory_;
				}
			}

			// Token: 0x170003CF RID: 975
			// (get) Token: 0x06000FAC RID: 4012 RVA: 0x0003BC68 File Offset: 0x00039E68
			public int InventoryCount
			{
				get
				{
					return this.result.InventoryCount;
				}
			}

			// Token: 0x06000FAD RID: 4013 RVA: 0x0003BC78 File Offset: 0x00039E78
			public RustProto.Item GetInventory(int index)
			{
				return this.result.GetInventory(index);
			}

			// Token: 0x06000FAE RID: 4014 RVA: 0x0003BC88 File Offset: 0x00039E88
			public RustProto.Avatar.Builder SetInventory(int index, RustProto.Item value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.inventory_[index] = value;
				return this;
			}

			// Token: 0x06000FAF RID: 4015 RVA: 0x0003BCB0 File Offset: 0x00039EB0
			public RustProto.Avatar.Builder SetInventory(int index, RustProto.Item.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.inventory_[index] = builderForValue.Build();
				return this;
			}

			// Token: 0x06000FB0 RID: 4016 RVA: 0x0003BCE8 File Offset: 0x00039EE8
			public RustProto.Avatar.Builder AddInventory(RustProto.Item value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.inventory_.Add(value);
				return this;
			}

			// Token: 0x06000FB1 RID: 4017 RVA: 0x0003BD1C File Offset: 0x00039F1C
			public RustProto.Avatar.Builder AddInventory(RustProto.Item.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.inventory_.Add(builderForValue.Build());
				return this;
			}

			// Token: 0x06000FB2 RID: 4018 RVA: 0x0003BD48 File Offset: 0x00039F48
			public RustProto.Avatar.Builder AddRangeInventory(IEnumerable<RustProto.Item> values)
			{
				this.PrepareBuilder();
				this.result.inventory_.Add(values);
				return this;
			}

			// Token: 0x06000FB3 RID: 4019 RVA: 0x0003BD64 File Offset: 0x00039F64
			public RustProto.Avatar.Builder ClearInventory()
			{
				this.PrepareBuilder();
				this.result.inventory_.Clear();
				return this;
			}

			// Token: 0x170003D0 RID: 976
			// (get) Token: 0x06000FB4 RID: 4020 RVA: 0x0003BD80 File Offset: 0x00039F80
			public IPopsicleList<RustProto.Item> WearableList
			{
				get
				{
					return this.PrepareBuilder().wearable_;
				}
			}

			// Token: 0x170003D1 RID: 977
			// (get) Token: 0x06000FB5 RID: 4021 RVA: 0x0003BD90 File Offset: 0x00039F90
			public int WearableCount
			{
				get
				{
					return this.result.WearableCount;
				}
			}

			// Token: 0x06000FB6 RID: 4022 RVA: 0x0003BDA0 File Offset: 0x00039FA0
			public RustProto.Item GetWearable(int index)
			{
				return this.result.GetWearable(index);
			}

			// Token: 0x06000FB7 RID: 4023 RVA: 0x0003BDB0 File Offset: 0x00039FB0
			public RustProto.Avatar.Builder SetWearable(int index, RustProto.Item value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.wearable_[index] = value;
				return this;
			}

			// Token: 0x06000FB8 RID: 4024 RVA: 0x0003BDD8 File Offset: 0x00039FD8
			public RustProto.Avatar.Builder SetWearable(int index, RustProto.Item.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.wearable_[index] = builderForValue.Build();
				return this;
			}

			// Token: 0x06000FB9 RID: 4025 RVA: 0x0003BE10 File Offset: 0x0003A010
			public RustProto.Avatar.Builder AddWearable(RustProto.Item value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.wearable_.Add(value);
				return this;
			}

			// Token: 0x06000FBA RID: 4026 RVA: 0x0003BE44 File Offset: 0x0003A044
			public RustProto.Avatar.Builder AddWearable(RustProto.Item.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.wearable_.Add(builderForValue.Build());
				return this;
			}

			// Token: 0x06000FBB RID: 4027 RVA: 0x0003BE70 File Offset: 0x0003A070
			public RustProto.Avatar.Builder AddRangeWearable(IEnumerable<RustProto.Item> values)
			{
				this.PrepareBuilder();
				this.result.wearable_.Add(values);
				return this;
			}

			// Token: 0x06000FBC RID: 4028 RVA: 0x0003BE8C File Offset: 0x0003A08C
			public RustProto.Avatar.Builder ClearWearable()
			{
				this.PrepareBuilder();
				this.result.wearable_.Clear();
				return this;
			}

			// Token: 0x170003D2 RID: 978
			// (get) Token: 0x06000FBD RID: 4029 RVA: 0x0003BEA8 File Offset: 0x0003A0A8
			public IPopsicleList<RustProto.Item> BeltList
			{
				get
				{
					return this.PrepareBuilder().belt_;
				}
			}

			// Token: 0x170003D3 RID: 979
			// (get) Token: 0x06000FBE RID: 4030 RVA: 0x0003BEB8 File Offset: 0x0003A0B8
			public int BeltCount
			{
				get
				{
					return this.result.BeltCount;
				}
			}

			// Token: 0x06000FBF RID: 4031 RVA: 0x0003BEC8 File Offset: 0x0003A0C8
			public RustProto.Item GetBelt(int index)
			{
				return this.result.GetBelt(index);
			}

			// Token: 0x06000FC0 RID: 4032 RVA: 0x0003BED8 File Offset: 0x0003A0D8
			public RustProto.Avatar.Builder SetBelt(int index, RustProto.Item value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.belt_[index] = value;
				return this;
			}

			// Token: 0x06000FC1 RID: 4033 RVA: 0x0003BF00 File Offset: 0x0003A100
			public RustProto.Avatar.Builder SetBelt(int index, RustProto.Item.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.belt_[index] = builderForValue.Build();
				return this;
			}

			// Token: 0x06000FC2 RID: 4034 RVA: 0x0003BF38 File Offset: 0x0003A138
			public RustProto.Avatar.Builder AddBelt(RustProto.Item value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.belt_.Add(value);
				return this;
			}

			// Token: 0x06000FC3 RID: 4035 RVA: 0x0003BF6C File Offset: 0x0003A16C
			public RustProto.Avatar.Builder AddBelt(RustProto.Item.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.belt_.Add(builderForValue.Build());
				return this;
			}

			// Token: 0x06000FC4 RID: 4036 RVA: 0x0003BF98 File Offset: 0x0003A198
			public RustProto.Avatar.Builder AddRangeBelt(IEnumerable<RustProto.Item> values)
			{
				this.PrepareBuilder();
				this.result.belt_.Add(values);
				return this;
			}

			// Token: 0x06000FC5 RID: 4037 RVA: 0x0003BFB4 File Offset: 0x0003A1B4
			public RustProto.Avatar.Builder ClearBelt()
			{
				this.PrepareBuilder();
				this.result.belt_.Clear();
				return this;
			}

			// Token: 0x170003D4 RID: 980
			// (get) Token: 0x06000FC6 RID: 4038 RVA: 0x0003BFD0 File Offset: 0x0003A1D0
			public bool HasAwayEvent
			{
				get
				{
					return this.result.hasAwayEvent;
				}
			}

			// Token: 0x170003D5 RID: 981
			// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x0003BFE0 File Offset: 0x0003A1E0
			// (set) Token: 0x06000FC8 RID: 4040 RVA: 0x0003BFF0 File Offset: 0x0003A1F0
			public AwayEvent AwayEvent
			{
				get
				{
					return this.result.AwayEvent;
				}
				set
				{
					this.SetAwayEvent(value);
				}
			}

			// Token: 0x06000FC9 RID: 4041 RVA: 0x0003BFFC File Offset: 0x0003A1FC
			public RustProto.Avatar.Builder SetAwayEvent(AwayEvent value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasAwayEvent = true;
				this.result.awayEvent_ = value;
				return this;
			}

			// Token: 0x06000FCA RID: 4042 RVA: 0x0003C02C File Offset: 0x0003A22C
			public RustProto.Avatar.Builder SetAwayEvent(AwayEvent.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.hasAwayEvent = true;
				this.result.awayEvent_ = builderForValue.Build();
				return this;
			}

			// Token: 0x06000FCB RID: 4043 RVA: 0x0003C06C File Offset: 0x0003A26C
			public RustProto.Avatar.Builder MergeAwayEvent(AwayEvent value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				if (this.result.hasAwayEvent && this.result.awayEvent_ != AwayEvent.DefaultInstance)
				{
					this.result.awayEvent_ = AwayEvent.CreateBuilder(this.result.awayEvent_).MergeFrom(value).BuildPartial();
				}
				else
				{
					this.result.awayEvent_ = value;
				}
				this.result.hasAwayEvent = true;
				return this;
			}

			// Token: 0x06000FCC RID: 4044 RVA: 0x0003C0F4 File Offset: 0x0003A2F4
			public RustProto.Avatar.Builder ClearAwayEvent()
			{
				this.PrepareBuilder();
				this.result.hasAwayEvent = false;
				this.result.awayEvent_ = null;
				return this;
			}

			// Token: 0x040009C6 RID: 2502
			private bool resultIsReadOnly;

			// Token: 0x040009C7 RID: 2503
			private RustProto.Avatar result;
		}
	}
}
