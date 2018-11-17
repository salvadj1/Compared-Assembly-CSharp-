using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Collections;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;
using RustProto.Helpers;

namespace RustProto
{
	// Token: 0x0200023A RID: 570
	[DebuggerNonUserCode]
	public sealed class SavedObject : GeneratedMessage<SavedObject, SavedObject.Builder>
	{
		// Token: 0x06001109 RID: 4361 RVA: 0x0003EB54 File Offset: 0x0003CD54
		private SavedObject()
		{
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x0003EB70 File Offset: 0x0003CD70
		static SavedObject()
		{
			object.ReferenceEquals(Worldsave.Descriptor, null);
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x0003EC3C File Offset: 0x0003CE3C
		public static RustProto.Helpers.Recycler<SavedObject, SavedObject.Builder> Recycler()
		{
			return RustProto.Helpers.Recycler<SavedObject, SavedObject.Builder>.Manufacture();
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x0600110C RID: 4364 RVA: 0x0003EC44 File Offset: 0x0003CE44
		public static SavedObject DefaultInstance
		{
			get
			{
				return SavedObject.defaultInstance;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x0600110D RID: 4365 RVA: 0x0003EC4C File Offset: 0x0003CE4C
		public override SavedObject DefaultInstanceForType
		{
			get
			{
				return SavedObject.DefaultInstance;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x0600110E RID: 4366 RVA: 0x0003EC54 File Offset: 0x0003CE54
		protected override SavedObject ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x0600110F RID: 4367 RVA: 0x0003EC58 File Offset: 0x0003CE58
		public static MessageDescriptor Descriptor
		{
			get
			{
				return Worldsave.internal__static_RustProto_SavedObject__Descriptor;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06001110 RID: 4368 RVA: 0x0003EC60 File Offset: 0x0003CE60
		protected override FieldAccessorTable<SavedObject, SavedObject.Builder> InternalFieldAccessors
		{
			get
			{
				return Worldsave.internal__static_RustProto_SavedObject__FieldAccessorTable;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06001111 RID: 4369 RVA: 0x0003EC68 File Offset: 0x0003CE68
		public bool HasId
		{
			get
			{
				return this.hasId;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06001112 RID: 4370 RVA: 0x0003EC70 File Offset: 0x0003CE70
		public int Id
		{
			get
			{
				return this.id_;
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06001113 RID: 4371 RVA: 0x0003EC78 File Offset: 0x0003CE78
		public bool HasDoor
		{
			get
			{
				return this.hasDoor;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06001114 RID: 4372 RVA: 0x0003EC80 File Offset: 0x0003CE80
		public objectDoor Door
		{
			get
			{
				return this.door_ ?? objectDoor.DefaultInstance;
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06001115 RID: 4373 RVA: 0x0003EC94 File Offset: 0x0003CE94
		public IList<Item> InventoryList
		{
			get
			{
				return this.inventory_;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06001116 RID: 4374 RVA: 0x0003EC9C File Offset: 0x0003CE9C
		public int InventoryCount
		{
			get
			{
				return this.inventory_.Count;
			}
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x0003ECAC File Offset: 0x0003CEAC
		public Item GetInventory(int index)
		{
			return this.inventory_[index];
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06001118 RID: 4376 RVA: 0x0003ECBC File Offset: 0x0003CEBC
		public bool HasDeployable
		{
			get
			{
				return this.hasDeployable;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06001119 RID: 4377 RVA: 0x0003ECC4 File Offset: 0x0003CEC4
		public objectDeployable Deployable
		{
			get
			{
				return this.deployable_ ?? objectDeployable.DefaultInstance;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x0600111A RID: 4378 RVA: 0x0003ECD8 File Offset: 0x0003CED8
		public bool HasStructMaster
		{
			get
			{
				return this.hasStructMaster;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x0600111B RID: 4379 RVA: 0x0003ECE0 File Offset: 0x0003CEE0
		public objectStructMaster StructMaster
		{
			get
			{
				return this.structMaster_ ?? objectStructMaster.DefaultInstance;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x0600111C RID: 4380 RVA: 0x0003ECF4 File Offset: 0x0003CEF4
		public bool HasStructComponent
		{
			get
			{
				return this.hasStructComponent;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x0600111D RID: 4381 RVA: 0x0003ECFC File Offset: 0x0003CEFC
		public objectStructComponent StructComponent
		{
			get
			{
				return this.structComponent_ ?? objectStructComponent.DefaultInstance;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x0600111E RID: 4382 RVA: 0x0003ED10 File Offset: 0x0003CF10
		public bool HasFireBarrel
		{
			get
			{
				return this.hasFireBarrel;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x0600111F RID: 4383 RVA: 0x0003ED18 File Offset: 0x0003CF18
		public objectFireBarrel FireBarrel
		{
			get
			{
				return this.fireBarrel_ ?? objectFireBarrel.DefaultInstance;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06001120 RID: 4384 RVA: 0x0003ED2C File Offset: 0x0003CF2C
		public bool HasNetInstance
		{
			get
			{
				return this.hasNetInstance;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06001121 RID: 4385 RVA: 0x0003ED34 File Offset: 0x0003CF34
		public objectNetInstance NetInstance
		{
			get
			{
				return this.netInstance_ ?? objectNetInstance.DefaultInstance;
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06001122 RID: 4386 RVA: 0x0003ED48 File Offset: 0x0003CF48
		public bool HasCoords
		{
			get
			{
				return this.hasCoords;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06001123 RID: 4387 RVA: 0x0003ED50 File Offset: 0x0003CF50
		public objectCoords Coords
		{
			get
			{
				return this.coords_ ?? objectCoords.DefaultInstance;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06001124 RID: 4388 RVA: 0x0003ED64 File Offset: 0x0003CF64
		public bool HasNgcInstance
		{
			get
			{
				return this.hasNgcInstance;
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06001125 RID: 4389 RVA: 0x0003ED6C File Offset: 0x0003CF6C
		public objectNGCInstance NgcInstance
		{
			get
			{
				return this.ngcInstance_ ?? objectNGCInstance.DefaultInstance;
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06001126 RID: 4390 RVA: 0x0003ED80 File Offset: 0x0003CF80
		public bool HasCarriableTrans
		{
			get
			{
				return this.hasCarriableTrans;
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001127 RID: 4391 RVA: 0x0003ED88 File Offset: 0x0003CF88
		public objectICarriableTrans CarriableTrans
		{
			get
			{
				return this.carriableTrans_ ?? objectICarriableTrans.DefaultInstance;
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06001128 RID: 4392 RVA: 0x0003ED9C File Offset: 0x0003CF9C
		public bool HasTakeDamage
		{
			get
			{
				return this.hasTakeDamage;
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06001129 RID: 4393 RVA: 0x0003EDA4 File Offset: 0x0003CFA4
		public objectTakeDamage TakeDamage
		{
			get
			{
				return this.takeDamage_ ?? objectTakeDamage.DefaultInstance;
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x0600112A RID: 4394 RVA: 0x0003EDB8 File Offset: 0x0003CFB8
		public bool HasSortOrder
		{
			get
			{
				return this.hasSortOrder;
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x0600112B RID: 4395 RVA: 0x0003EDC0 File Offset: 0x0003CFC0
		public int SortOrder
		{
			get
			{
				return this.sortOrder_;
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x0600112C RID: 4396 RVA: 0x0003EDC8 File Offset: 0x0003CFC8
		public bool HasSleepingAvatar
		{
			get
			{
				return this.hasSleepingAvatar;
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x0600112D RID: 4397 RVA: 0x0003EDD0 File Offset: 0x0003CFD0
		public objectSleepingAvatar SleepingAvatar
		{
			get
			{
				return this.sleepingAvatar_ ?? objectSleepingAvatar.DefaultInstance;
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x0600112E RID: 4398 RVA: 0x0003EDE4 File Offset: 0x0003CFE4
		public bool HasLockable
		{
			get
			{
				return this.hasLockable;
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x0600112F RID: 4399 RVA: 0x0003EDEC File Offset: 0x0003CFEC
		public objectLockable Lockable
		{
			get
			{
				return this.lockable_ ?? objectLockable.DefaultInstance;
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001130 RID: 4400 RVA: 0x0003EE00 File Offset: 0x0003D000
		public override bool IsInitialized
		{
			get
			{
				foreach (Item item in this.InventoryList)
				{
					if (!item.IsInitialized)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x0003EE74 File Offset: 0x0003D074
		public override void WriteTo(ICodedOutputStream output)
		{
			int serializedSize = this.SerializedSize;
			string[] savedObjectFieldNames = SavedObject._savedObjectFieldNames;
			if (this.hasId)
			{
				output.WriteInt32(1, savedObjectFieldNames[5], this.Id);
			}
			if (this.hasDoor)
			{
				output.WriteMessage(2, savedObjectFieldNames[3], this.Door);
			}
			if (this.inventory_.Count > 0)
			{
				output.WriteMessageArray<Item>(3, savedObjectFieldNames[6], this.inventory_);
			}
			if (this.hasDeployable)
			{
				output.WriteMessage(4, savedObjectFieldNames[2], this.Deployable);
			}
			if (this.hasStructMaster)
			{
				output.WriteMessage(5, savedObjectFieldNames[13], this.StructMaster);
			}
			if (this.hasStructComponent)
			{
				output.WriteMessage(6, savedObjectFieldNames[12], this.StructComponent);
			}
			if (this.hasFireBarrel)
			{
				output.WriteMessage(7, savedObjectFieldNames[4], this.FireBarrel);
			}
			if (this.hasNetInstance)
			{
				output.WriteMessage(8, savedObjectFieldNames[8], this.NetInstance);
			}
			if (this.hasCoords)
			{
				output.WriteMessage(9, savedObjectFieldNames[1], this.Coords);
			}
			if (this.hasNgcInstance)
			{
				output.WriteMessage(10, savedObjectFieldNames[9], this.NgcInstance);
			}
			if (this.hasCarriableTrans)
			{
				output.WriteMessage(11, savedObjectFieldNames[0], this.CarriableTrans);
			}
			if (this.hasTakeDamage)
			{
				output.WriteMessage(12, savedObjectFieldNames[14], this.TakeDamage);
			}
			if (this.hasSortOrder)
			{
				output.WriteInt32(13, savedObjectFieldNames[11], this.SortOrder);
			}
			if (this.hasSleepingAvatar)
			{
				output.WriteMessage(14, savedObjectFieldNames[10], this.SleepingAvatar);
			}
			if (this.hasLockable)
			{
				output.WriteMessage(15, savedObjectFieldNames[7], this.Lockable);
			}
			this.UnknownFields.WriteTo(output);
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06001132 RID: 4402 RVA: 0x0003F044 File Offset: 0x0003D244
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
				if (this.hasId)
				{
					num += CodedOutputStream.ComputeInt32Size(1, this.Id);
				}
				if (this.hasDoor)
				{
					num += CodedOutputStream.ComputeMessageSize(2, this.Door);
				}
				foreach (Item item in this.InventoryList)
				{
					num += CodedOutputStream.ComputeMessageSize(3, item);
				}
				if (this.hasDeployable)
				{
					num += CodedOutputStream.ComputeMessageSize(4, this.Deployable);
				}
				if (this.hasStructMaster)
				{
					num += CodedOutputStream.ComputeMessageSize(5, this.StructMaster);
				}
				if (this.hasStructComponent)
				{
					num += CodedOutputStream.ComputeMessageSize(6, this.StructComponent);
				}
				if (this.hasFireBarrel)
				{
					num += CodedOutputStream.ComputeMessageSize(7, this.FireBarrel);
				}
				if (this.hasNetInstance)
				{
					num += CodedOutputStream.ComputeMessageSize(8, this.NetInstance);
				}
				if (this.hasCoords)
				{
					num += CodedOutputStream.ComputeMessageSize(9, this.Coords);
				}
				if (this.hasNgcInstance)
				{
					num += CodedOutputStream.ComputeMessageSize(10, this.NgcInstance);
				}
				if (this.hasCarriableTrans)
				{
					num += CodedOutputStream.ComputeMessageSize(11, this.CarriableTrans);
				}
				if (this.hasTakeDamage)
				{
					num += CodedOutputStream.ComputeMessageSize(12, this.TakeDamage);
				}
				if (this.hasSortOrder)
				{
					num += CodedOutputStream.ComputeInt32Size(13, this.SortOrder);
				}
				if (this.hasSleepingAvatar)
				{
					num += CodedOutputStream.ComputeMessageSize(14, this.SleepingAvatar);
				}
				if (this.hasLockable)
				{
					num += CodedOutputStream.ComputeMessageSize(15, this.Lockable);
				}
				num += this.UnknownFields.SerializedSize;
				this.memoizedSerializedSize = num;
				return num;
			}
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x0003F248 File Offset: 0x0003D448
		public static SavedObject ParseFrom(ByteString data)
		{
			return SavedObject.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x0003F25C File Offset: 0x0003D45C
		public static SavedObject ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return SavedObject.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x0003F270 File Offset: 0x0003D470
		public static SavedObject ParseFrom(byte[] data)
		{
			return SavedObject.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x0003F284 File Offset: 0x0003D484
		public static SavedObject ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return SavedObject.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x0003F298 File Offset: 0x0003D498
		public static SavedObject ParseFrom(Stream input)
		{
			return SavedObject.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x0003F2AC File Offset: 0x0003D4AC
		public static SavedObject ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return SavedObject.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x0003F2C0 File Offset: 0x0003D4C0
		public static SavedObject ParseDelimitedFrom(Stream input)
		{
			return SavedObject.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x0003F2D4 File Offset: 0x0003D4D4
		public static SavedObject ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return SavedObject.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x0003F2E8 File Offset: 0x0003D4E8
		public static SavedObject ParseFrom(ICodedInputStream input)
		{
			return SavedObject.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x0003F2FC File Offset: 0x0003D4FC
		public static SavedObject ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return SavedObject.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x0003F310 File Offset: 0x0003D510
		private SavedObject MakeReadOnly()
		{
			this.inventory_.MakeReadOnly();
			return this;
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x0003F320 File Offset: 0x0003D520
		public static SavedObject.Builder CreateBuilder()
		{
			return new SavedObject.Builder();
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x0003F328 File Offset: 0x0003D528
		public override SavedObject.Builder ToBuilder()
		{
			return SavedObject.CreateBuilder(this);
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x0003F330 File Offset: 0x0003D530
		public override SavedObject.Builder CreateBuilderForType()
		{
			return new SavedObject.Builder();
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x0003F338 File Offset: 0x0003D538
		public static SavedObject.Builder CreateBuilder(SavedObject prototype)
		{
			return new SavedObject.Builder(prototype);
		}

		// Token: 0x04000A1E RID: 2590
		public const int IdFieldNumber = 1;

		// Token: 0x04000A1F RID: 2591
		public const int DoorFieldNumber = 2;

		// Token: 0x04000A20 RID: 2592
		public const int InventoryFieldNumber = 3;

		// Token: 0x04000A21 RID: 2593
		public const int DeployableFieldNumber = 4;

		// Token: 0x04000A22 RID: 2594
		public const int StructMasterFieldNumber = 5;

		// Token: 0x04000A23 RID: 2595
		public const int StructComponentFieldNumber = 6;

		// Token: 0x04000A24 RID: 2596
		public const int FireBarrelFieldNumber = 7;

		// Token: 0x04000A25 RID: 2597
		public const int NetInstanceFieldNumber = 8;

		// Token: 0x04000A26 RID: 2598
		public const int CoordsFieldNumber = 9;

		// Token: 0x04000A27 RID: 2599
		public const int NgcInstanceFieldNumber = 10;

		// Token: 0x04000A28 RID: 2600
		public const int CarriableTransFieldNumber = 11;

		// Token: 0x04000A29 RID: 2601
		public const int TakeDamageFieldNumber = 12;

		// Token: 0x04000A2A RID: 2602
		public const int SortOrderFieldNumber = 13;

		// Token: 0x04000A2B RID: 2603
		public const int SleepingAvatarFieldNumber = 14;

		// Token: 0x04000A2C RID: 2604
		public const int LockableFieldNumber = 15;

		// Token: 0x04000A2D RID: 2605
		private static readonly SavedObject defaultInstance = new SavedObject().MakeReadOnly();

		// Token: 0x04000A2E RID: 2606
		private static readonly string[] _savedObjectFieldNames = new string[]
		{
			"carriableTrans",
			"coords",
			"deployable",
			"door",
			"fireBarrel",
			"id",
			"inventory",
			"lockable",
			"netInstance",
			"ngcInstance",
			"sleepingAvatar",
			"sortOrder",
			"structComponent",
			"structMaster",
			"takeDamage"
		};

		// Token: 0x04000A2F RID: 2607
		private static readonly uint[] _savedObjectFieldTags = new uint[]
		{
			90u,
			74u,
			34u,
			18u,
			58u,
			8u,
			26u,
			122u,
			66u,
			82u,
			114u,
			104u,
			50u,
			42u,
			98u
		};

		// Token: 0x04000A30 RID: 2608
		private bool hasId;

		// Token: 0x04000A31 RID: 2609
		private int id_;

		// Token: 0x04000A32 RID: 2610
		private bool hasDoor;

		// Token: 0x04000A33 RID: 2611
		private objectDoor door_;

		// Token: 0x04000A34 RID: 2612
		private PopsicleList<Item> inventory_ = new PopsicleList<Item>();

		// Token: 0x04000A35 RID: 2613
		private bool hasDeployable;

		// Token: 0x04000A36 RID: 2614
		private objectDeployable deployable_;

		// Token: 0x04000A37 RID: 2615
		private bool hasStructMaster;

		// Token: 0x04000A38 RID: 2616
		private objectStructMaster structMaster_;

		// Token: 0x04000A39 RID: 2617
		private bool hasStructComponent;

		// Token: 0x04000A3A RID: 2618
		private objectStructComponent structComponent_;

		// Token: 0x04000A3B RID: 2619
		private bool hasFireBarrel;

		// Token: 0x04000A3C RID: 2620
		private objectFireBarrel fireBarrel_;

		// Token: 0x04000A3D RID: 2621
		private bool hasNetInstance;

		// Token: 0x04000A3E RID: 2622
		private objectNetInstance netInstance_;

		// Token: 0x04000A3F RID: 2623
		private bool hasCoords;

		// Token: 0x04000A40 RID: 2624
		private objectCoords coords_;

		// Token: 0x04000A41 RID: 2625
		private bool hasNgcInstance;

		// Token: 0x04000A42 RID: 2626
		private objectNGCInstance ngcInstance_;

		// Token: 0x04000A43 RID: 2627
		private bool hasCarriableTrans;

		// Token: 0x04000A44 RID: 2628
		private objectICarriableTrans carriableTrans_;

		// Token: 0x04000A45 RID: 2629
		private bool hasTakeDamage;

		// Token: 0x04000A46 RID: 2630
		private objectTakeDamage takeDamage_;

		// Token: 0x04000A47 RID: 2631
		private bool hasSortOrder;

		// Token: 0x04000A48 RID: 2632
		private int sortOrder_;

		// Token: 0x04000A49 RID: 2633
		private bool hasSleepingAvatar;

		// Token: 0x04000A4A RID: 2634
		private objectSleepingAvatar sleepingAvatar_;

		// Token: 0x04000A4B RID: 2635
		private bool hasLockable;

		// Token: 0x04000A4C RID: 2636
		private objectLockable lockable_;

		// Token: 0x04000A4D RID: 2637
		private int memoizedSerializedSize = -1;

		// Token: 0x0200023B RID: 571
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<SavedObject, SavedObject.Builder>
		{
			// Token: 0x06001142 RID: 4418 RVA: 0x0003F340 File Offset: 0x0003D540
			public Builder()
			{
				this.result = SavedObject.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x06001143 RID: 4419 RVA: 0x0003F35C File Offset: 0x0003D55C
			internal Builder(SavedObject cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x1700047F RID: 1151
			// (get) Token: 0x06001144 RID: 4420 RVA: 0x0003F374 File Offset: 0x0003D574
			protected override SavedObject.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x06001145 RID: 4421 RVA: 0x0003F378 File Offset: 0x0003D578
			private SavedObject PrepareBuilder()
			{
				if (this.resultIsReadOnly)
				{
					SavedObject other = this.result;
					this.result = new SavedObject();
					this.resultIsReadOnly = false;
					this.MergeFrom(other);
				}
				return this.result;
			}

			// Token: 0x17000480 RID: 1152
			// (get) Token: 0x06001146 RID: 4422 RVA: 0x0003F3B8 File Offset: 0x0003D5B8
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x17000481 RID: 1153
			// (get) Token: 0x06001147 RID: 4423 RVA: 0x0003F3C8 File Offset: 0x0003D5C8
			protected override SavedObject MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x06001148 RID: 4424 RVA: 0x0003F3D0 File Offset: 0x0003D5D0
			public override SavedObject.Builder Clear()
			{
				this.result = SavedObject.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x06001149 RID: 4425 RVA: 0x0003F3E8 File Offset: 0x0003D5E8
			public override SavedObject.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new SavedObject.Builder(this.result);
				}
				return new SavedObject.Builder().MergeFrom(this.result);
			}

			// Token: 0x17000482 RID: 1154
			// (get) Token: 0x0600114A RID: 4426 RVA: 0x0003F414 File Offset: 0x0003D614
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return SavedObject.Descriptor;
				}
			}

			// Token: 0x17000483 RID: 1155
			// (get) Token: 0x0600114B RID: 4427 RVA: 0x0003F41C File Offset: 0x0003D61C
			public override SavedObject DefaultInstanceForType
			{
				get
				{
					return SavedObject.DefaultInstance;
				}
			}

			// Token: 0x0600114C RID: 4428 RVA: 0x0003F424 File Offset: 0x0003D624
			public override SavedObject BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x0600114D RID: 4429 RVA: 0x0003F458 File Offset: 0x0003D658
			public override SavedObject.Builder MergeFrom(IMessage other)
			{
				if (other is SavedObject)
				{
					return this.MergeFrom((SavedObject)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x0600114E RID: 4430 RVA: 0x0003F47C File Offset: 0x0003D67C
			public override SavedObject.Builder MergeFrom(SavedObject other)
			{
				if (other == SavedObject.DefaultInstance)
				{
					return this;
				}
				this.PrepareBuilder();
				if (other.HasId)
				{
					this.Id = other.Id;
				}
				if (other.HasDoor)
				{
					this.MergeDoor(other.Door);
				}
				if (other.inventory_.Count != 0)
				{
					this.result.inventory_.Add(other.inventory_);
				}
				if (other.HasDeployable)
				{
					this.MergeDeployable(other.Deployable);
				}
				if (other.HasStructMaster)
				{
					this.MergeStructMaster(other.StructMaster);
				}
				if (other.HasStructComponent)
				{
					this.MergeStructComponent(other.StructComponent);
				}
				if (other.HasFireBarrel)
				{
					this.MergeFireBarrel(other.FireBarrel);
				}
				if (other.HasNetInstance)
				{
					this.MergeNetInstance(other.NetInstance);
				}
				if (other.HasCoords)
				{
					this.MergeCoords(other.Coords);
				}
				if (other.HasNgcInstance)
				{
					this.MergeNgcInstance(other.NgcInstance);
				}
				if (other.HasCarriableTrans)
				{
					this.MergeCarriableTrans(other.CarriableTrans);
				}
				if (other.HasTakeDamage)
				{
					this.MergeTakeDamage(other.TakeDamage);
				}
				if (other.HasSortOrder)
				{
					this.SortOrder = other.SortOrder;
				}
				if (other.HasSleepingAvatar)
				{
					this.MergeSleepingAvatar(other.SleepingAvatar);
				}
				if (other.HasLockable)
				{
					this.MergeLockable(other.Lockable);
				}
				this.MergeUnknownFields(other.UnknownFields);
				return this;
			}

			// Token: 0x0600114F RID: 4431 RVA: 0x0003F620 File Offset: 0x0003D820
			public override SavedObject.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x06001150 RID: 4432 RVA: 0x0003F630 File Offset: 0x0003D830
			public override SavedObject.Builder MergeFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
			{
				this.PrepareBuilder();
				UnknownFieldSet.Builder builder = null;
				uint num;
				string text;
				while (input.ReadTag(ref num, ref text))
				{
					if (num == 0u && text != null)
					{
						int num2 = Array.BinarySearch<string>(SavedObject._savedObjectFieldNames, text, StringComparer.Ordinal);
						if (num2 < 0)
						{
							if (builder == null)
							{
								builder = UnknownFieldSet.CreateBuilder(this.UnknownFields);
							}
							this.ParseUnknownField(input, builder, extensionRegistry, num, text);
							continue;
						}
						num = SavedObject._savedObjectFieldTags[num2];
					}
					uint num3 = num;
					if (num3 == 0u)
					{
						throw InvalidProtocolBufferException.InvalidTag();
					}
					if (num3 != 8u)
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
													if (num3 != 74u)
													{
														if (num3 != 82u)
														{
															if (num3 != 90u)
															{
																if (num3 != 98u)
																{
																	if (num3 != 104u)
																	{
																		if (num3 != 114u)
																		{
																			if (num3 != 122u)
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
																				objectLockable.Builder builder2 = objectLockable.CreateBuilder();
																				if (this.result.hasLockable)
																				{
																					builder2.MergeFrom(this.Lockable);
																				}
																				input.ReadMessage(builder2, extensionRegistry);
																				this.Lockable = builder2.BuildPartial();
																			}
																		}
																		else
																		{
																			objectSleepingAvatar.Builder builder3 = objectSleepingAvatar.CreateBuilder();
																			if (this.result.hasSleepingAvatar)
																			{
																				builder3.MergeFrom(this.SleepingAvatar);
																			}
																			input.ReadMessage(builder3, extensionRegistry);
																			this.SleepingAvatar = builder3.BuildPartial();
																		}
																	}
																	else
																	{
																		this.result.hasSortOrder = input.ReadInt32(ref this.result.sortOrder_);
																	}
																}
																else
																{
																	objectTakeDamage.Builder builder4 = objectTakeDamage.CreateBuilder();
																	if (this.result.hasTakeDamage)
																	{
																		builder4.MergeFrom(this.TakeDamage);
																	}
																	input.ReadMessage(builder4, extensionRegistry);
																	this.TakeDamage = builder4.BuildPartial();
																}
															}
															else
															{
																objectICarriableTrans.Builder builder5 = objectICarriableTrans.CreateBuilder();
																if (this.result.hasCarriableTrans)
																{
																	builder5.MergeFrom(this.CarriableTrans);
																}
																input.ReadMessage(builder5, extensionRegistry);
																this.CarriableTrans = builder5.BuildPartial();
															}
														}
														else
														{
															objectNGCInstance.Builder builder6 = objectNGCInstance.CreateBuilder();
															if (this.result.hasNgcInstance)
															{
																builder6.MergeFrom(this.NgcInstance);
															}
															input.ReadMessage(builder6, extensionRegistry);
															this.NgcInstance = builder6.BuildPartial();
														}
													}
													else
													{
														objectCoords.Builder builder7 = objectCoords.CreateBuilder();
														if (this.result.hasCoords)
														{
															builder7.MergeFrom(this.Coords);
														}
														input.ReadMessage(builder7, extensionRegistry);
														this.Coords = builder7.BuildPartial();
													}
												}
												else
												{
													objectNetInstance.Builder builder8 = objectNetInstance.CreateBuilder();
													if (this.result.hasNetInstance)
													{
														builder8.MergeFrom(this.NetInstance);
													}
													input.ReadMessage(builder8, extensionRegistry);
													this.NetInstance = builder8.BuildPartial();
												}
											}
											else
											{
												objectFireBarrel.Builder builder9 = objectFireBarrel.CreateBuilder();
												if (this.result.hasFireBarrel)
												{
													builder9.MergeFrom(this.FireBarrel);
												}
												input.ReadMessage(builder9, extensionRegistry);
												this.FireBarrel = builder9.BuildPartial();
											}
										}
										else
										{
											objectStructComponent.Builder builder10 = objectStructComponent.CreateBuilder();
											if (this.result.hasStructComponent)
											{
												builder10.MergeFrom(this.StructComponent);
											}
											input.ReadMessage(builder10, extensionRegistry);
											this.StructComponent = builder10.BuildPartial();
										}
									}
									else
									{
										objectStructMaster.Builder builder11 = objectStructMaster.CreateBuilder();
										if (this.result.hasStructMaster)
										{
											builder11.MergeFrom(this.StructMaster);
										}
										input.ReadMessage(builder11, extensionRegistry);
										this.StructMaster = builder11.BuildPartial();
									}
								}
								else
								{
									objectDeployable.Builder builder12 = objectDeployable.CreateBuilder();
									if (this.result.hasDeployable)
									{
										builder12.MergeFrom(this.Deployable);
									}
									input.ReadMessage(builder12, extensionRegistry);
									this.Deployable = builder12.BuildPartial();
								}
							}
							else
							{
								input.ReadMessageArray<Item>(num, text, this.result.inventory_, Item.DefaultInstance, extensionRegistry);
							}
						}
						else
						{
							objectDoor.Builder builder13 = objectDoor.CreateBuilder();
							if (this.result.hasDoor)
							{
								builder13.MergeFrom(this.Door);
							}
							input.ReadMessage(builder13, extensionRegistry);
							this.Door = builder13.BuildPartial();
						}
					}
					else
					{
						this.result.hasId = input.ReadInt32(ref this.result.id_);
					}
				}
				if (builder != null)
				{
					this.UnknownFields = builder.Build();
				}
				return this;
			}

			// Token: 0x17000484 RID: 1156
			// (get) Token: 0x06001151 RID: 4433 RVA: 0x0003FB00 File Offset: 0x0003DD00
			public bool HasId
			{
				get
				{
					return this.result.hasId;
				}
			}

			// Token: 0x17000485 RID: 1157
			// (get) Token: 0x06001152 RID: 4434 RVA: 0x0003FB10 File Offset: 0x0003DD10
			// (set) Token: 0x06001153 RID: 4435 RVA: 0x0003FB20 File Offset: 0x0003DD20
			public int Id
			{
				get
				{
					return this.result.Id;
				}
				set
				{
					this.SetId(value);
				}
			}

			// Token: 0x06001154 RID: 4436 RVA: 0x0003FB2C File Offset: 0x0003DD2C
			public SavedObject.Builder SetId(int value)
			{
				this.PrepareBuilder();
				this.result.hasId = true;
				this.result.id_ = value;
				return this;
			}

			// Token: 0x06001155 RID: 4437 RVA: 0x0003FB5C File Offset: 0x0003DD5C
			public SavedObject.Builder ClearId()
			{
				this.PrepareBuilder();
				this.result.hasId = false;
				this.result.id_ = 0;
				return this;
			}

			// Token: 0x17000486 RID: 1158
			// (get) Token: 0x06001156 RID: 4438 RVA: 0x0003FB8C File Offset: 0x0003DD8C
			public bool HasDoor
			{
				get
				{
					return this.result.hasDoor;
				}
			}

			// Token: 0x17000487 RID: 1159
			// (get) Token: 0x06001157 RID: 4439 RVA: 0x0003FB9C File Offset: 0x0003DD9C
			// (set) Token: 0x06001158 RID: 4440 RVA: 0x0003FBAC File Offset: 0x0003DDAC
			public objectDoor Door
			{
				get
				{
					return this.result.Door;
				}
				set
				{
					this.SetDoor(value);
				}
			}

			// Token: 0x06001159 RID: 4441 RVA: 0x0003FBB8 File Offset: 0x0003DDB8
			public SavedObject.Builder SetDoor(objectDoor value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasDoor = true;
				this.result.door_ = value;
				return this;
			}

			// Token: 0x0600115A RID: 4442 RVA: 0x0003FBE8 File Offset: 0x0003DDE8
			public SavedObject.Builder SetDoor(objectDoor.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.hasDoor = true;
				this.result.door_ = builderForValue.Build();
				return this;
			}

			// Token: 0x0600115B RID: 4443 RVA: 0x0003FC28 File Offset: 0x0003DE28
			public SavedObject.Builder MergeDoor(objectDoor value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				if (this.result.hasDoor && this.result.door_ != objectDoor.DefaultInstance)
				{
					this.result.door_ = objectDoor.CreateBuilder(this.result.door_).MergeFrom(value).BuildPartial();
				}
				else
				{
					this.result.door_ = value;
				}
				this.result.hasDoor = true;
				return this;
			}

			// Token: 0x0600115C RID: 4444 RVA: 0x0003FCB0 File Offset: 0x0003DEB0
			public SavedObject.Builder ClearDoor()
			{
				this.PrepareBuilder();
				this.result.hasDoor = false;
				this.result.door_ = null;
				return this;
			}

			// Token: 0x17000488 RID: 1160
			// (get) Token: 0x0600115D RID: 4445 RVA: 0x0003FCE0 File Offset: 0x0003DEE0
			public IPopsicleList<Item> InventoryList
			{
				get
				{
					return this.PrepareBuilder().inventory_;
				}
			}

			// Token: 0x17000489 RID: 1161
			// (get) Token: 0x0600115E RID: 4446 RVA: 0x0003FCF0 File Offset: 0x0003DEF0
			public int InventoryCount
			{
				get
				{
					return this.result.InventoryCount;
				}
			}

			// Token: 0x0600115F RID: 4447 RVA: 0x0003FD00 File Offset: 0x0003DF00
			public Item GetInventory(int index)
			{
				return this.result.GetInventory(index);
			}

			// Token: 0x06001160 RID: 4448 RVA: 0x0003FD10 File Offset: 0x0003DF10
			public SavedObject.Builder SetInventory(int index, Item value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.inventory_[index] = value;
				return this;
			}

			// Token: 0x06001161 RID: 4449 RVA: 0x0003FD38 File Offset: 0x0003DF38
			public SavedObject.Builder SetInventory(int index, Item.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.inventory_[index] = builderForValue.Build();
				return this;
			}

			// Token: 0x06001162 RID: 4450 RVA: 0x0003FD70 File Offset: 0x0003DF70
			public SavedObject.Builder AddInventory(Item value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.inventory_.Add(value);
				return this;
			}

			// Token: 0x06001163 RID: 4451 RVA: 0x0003FDA4 File Offset: 0x0003DFA4
			public SavedObject.Builder AddInventory(Item.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.inventory_.Add(builderForValue.Build());
				return this;
			}

			// Token: 0x06001164 RID: 4452 RVA: 0x0003FDD0 File Offset: 0x0003DFD0
			public SavedObject.Builder AddRangeInventory(IEnumerable<Item> values)
			{
				this.PrepareBuilder();
				this.result.inventory_.Add(values);
				return this;
			}

			// Token: 0x06001165 RID: 4453 RVA: 0x0003FDEC File Offset: 0x0003DFEC
			public SavedObject.Builder ClearInventory()
			{
				this.PrepareBuilder();
				this.result.inventory_.Clear();
				return this;
			}

			// Token: 0x1700048A RID: 1162
			// (get) Token: 0x06001166 RID: 4454 RVA: 0x0003FE08 File Offset: 0x0003E008
			public bool HasDeployable
			{
				get
				{
					return this.result.hasDeployable;
				}
			}

			// Token: 0x1700048B RID: 1163
			// (get) Token: 0x06001167 RID: 4455 RVA: 0x0003FE18 File Offset: 0x0003E018
			// (set) Token: 0x06001168 RID: 4456 RVA: 0x0003FE28 File Offset: 0x0003E028
			public objectDeployable Deployable
			{
				get
				{
					return this.result.Deployable;
				}
				set
				{
					this.SetDeployable(value);
				}
			}

			// Token: 0x06001169 RID: 4457 RVA: 0x0003FE34 File Offset: 0x0003E034
			public SavedObject.Builder SetDeployable(objectDeployable value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasDeployable = true;
				this.result.deployable_ = value;
				return this;
			}

			// Token: 0x0600116A RID: 4458 RVA: 0x0003FE64 File Offset: 0x0003E064
			public SavedObject.Builder SetDeployable(objectDeployable.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.hasDeployable = true;
				this.result.deployable_ = builderForValue.Build();
				return this;
			}

			// Token: 0x0600116B RID: 4459 RVA: 0x0003FEA4 File Offset: 0x0003E0A4
			public SavedObject.Builder MergeDeployable(objectDeployable value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				if (this.result.hasDeployable && this.result.deployable_ != objectDeployable.DefaultInstance)
				{
					this.result.deployable_ = objectDeployable.CreateBuilder(this.result.deployable_).MergeFrom(value).BuildPartial();
				}
				else
				{
					this.result.deployable_ = value;
				}
				this.result.hasDeployable = true;
				return this;
			}

			// Token: 0x0600116C RID: 4460 RVA: 0x0003FF2C File Offset: 0x0003E12C
			public SavedObject.Builder ClearDeployable()
			{
				this.PrepareBuilder();
				this.result.hasDeployable = false;
				this.result.deployable_ = null;
				return this;
			}

			// Token: 0x1700048C RID: 1164
			// (get) Token: 0x0600116D RID: 4461 RVA: 0x0003FF5C File Offset: 0x0003E15C
			public bool HasStructMaster
			{
				get
				{
					return this.result.hasStructMaster;
				}
			}

			// Token: 0x1700048D RID: 1165
			// (get) Token: 0x0600116E RID: 4462 RVA: 0x0003FF6C File Offset: 0x0003E16C
			// (set) Token: 0x0600116F RID: 4463 RVA: 0x0003FF7C File Offset: 0x0003E17C
			public objectStructMaster StructMaster
			{
				get
				{
					return this.result.StructMaster;
				}
				set
				{
					this.SetStructMaster(value);
				}
			}

			// Token: 0x06001170 RID: 4464 RVA: 0x0003FF88 File Offset: 0x0003E188
			public SavedObject.Builder SetStructMaster(objectStructMaster value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasStructMaster = true;
				this.result.structMaster_ = value;
				return this;
			}

			// Token: 0x06001171 RID: 4465 RVA: 0x0003FFB8 File Offset: 0x0003E1B8
			public SavedObject.Builder SetStructMaster(objectStructMaster.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.hasStructMaster = true;
				this.result.structMaster_ = builderForValue.Build();
				return this;
			}

			// Token: 0x06001172 RID: 4466 RVA: 0x0003FFF8 File Offset: 0x0003E1F8
			public SavedObject.Builder MergeStructMaster(objectStructMaster value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				if (this.result.hasStructMaster && this.result.structMaster_ != objectStructMaster.DefaultInstance)
				{
					this.result.structMaster_ = objectStructMaster.CreateBuilder(this.result.structMaster_).MergeFrom(value).BuildPartial();
				}
				else
				{
					this.result.structMaster_ = value;
				}
				this.result.hasStructMaster = true;
				return this;
			}

			// Token: 0x06001173 RID: 4467 RVA: 0x00040080 File Offset: 0x0003E280
			public SavedObject.Builder ClearStructMaster()
			{
				this.PrepareBuilder();
				this.result.hasStructMaster = false;
				this.result.structMaster_ = null;
				return this;
			}

			// Token: 0x1700048E RID: 1166
			// (get) Token: 0x06001174 RID: 4468 RVA: 0x000400B0 File Offset: 0x0003E2B0
			public bool HasStructComponent
			{
				get
				{
					return this.result.hasStructComponent;
				}
			}

			// Token: 0x1700048F RID: 1167
			// (get) Token: 0x06001175 RID: 4469 RVA: 0x000400C0 File Offset: 0x0003E2C0
			// (set) Token: 0x06001176 RID: 4470 RVA: 0x000400D0 File Offset: 0x0003E2D0
			public objectStructComponent StructComponent
			{
				get
				{
					return this.result.StructComponent;
				}
				set
				{
					this.SetStructComponent(value);
				}
			}

			// Token: 0x06001177 RID: 4471 RVA: 0x000400DC File Offset: 0x0003E2DC
			public SavedObject.Builder SetStructComponent(objectStructComponent value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasStructComponent = true;
				this.result.structComponent_ = value;
				return this;
			}

			// Token: 0x06001178 RID: 4472 RVA: 0x0004010C File Offset: 0x0003E30C
			public SavedObject.Builder SetStructComponent(objectStructComponent.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.hasStructComponent = true;
				this.result.structComponent_ = builderForValue.Build();
				return this;
			}

			// Token: 0x06001179 RID: 4473 RVA: 0x0004014C File Offset: 0x0003E34C
			public SavedObject.Builder MergeStructComponent(objectStructComponent value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				if (this.result.hasStructComponent && this.result.structComponent_ != objectStructComponent.DefaultInstance)
				{
					this.result.structComponent_ = objectStructComponent.CreateBuilder(this.result.structComponent_).MergeFrom(value).BuildPartial();
				}
				else
				{
					this.result.structComponent_ = value;
				}
				this.result.hasStructComponent = true;
				return this;
			}

			// Token: 0x0600117A RID: 4474 RVA: 0x000401D4 File Offset: 0x0003E3D4
			public SavedObject.Builder ClearStructComponent()
			{
				this.PrepareBuilder();
				this.result.hasStructComponent = false;
				this.result.structComponent_ = null;
				return this;
			}

			// Token: 0x17000490 RID: 1168
			// (get) Token: 0x0600117B RID: 4475 RVA: 0x00040204 File Offset: 0x0003E404
			public bool HasFireBarrel
			{
				get
				{
					return this.result.hasFireBarrel;
				}
			}

			// Token: 0x17000491 RID: 1169
			// (get) Token: 0x0600117C RID: 4476 RVA: 0x00040214 File Offset: 0x0003E414
			// (set) Token: 0x0600117D RID: 4477 RVA: 0x00040224 File Offset: 0x0003E424
			public objectFireBarrel FireBarrel
			{
				get
				{
					return this.result.FireBarrel;
				}
				set
				{
					this.SetFireBarrel(value);
				}
			}

			// Token: 0x0600117E RID: 4478 RVA: 0x00040230 File Offset: 0x0003E430
			public SavedObject.Builder SetFireBarrel(objectFireBarrel value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasFireBarrel = true;
				this.result.fireBarrel_ = value;
				return this;
			}

			// Token: 0x0600117F RID: 4479 RVA: 0x00040260 File Offset: 0x0003E460
			public SavedObject.Builder SetFireBarrel(objectFireBarrel.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.hasFireBarrel = true;
				this.result.fireBarrel_ = builderForValue.Build();
				return this;
			}

			// Token: 0x06001180 RID: 4480 RVA: 0x000402A0 File Offset: 0x0003E4A0
			public SavedObject.Builder MergeFireBarrel(objectFireBarrel value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				if (this.result.hasFireBarrel && this.result.fireBarrel_ != objectFireBarrel.DefaultInstance)
				{
					this.result.fireBarrel_ = objectFireBarrel.CreateBuilder(this.result.fireBarrel_).MergeFrom(value).BuildPartial();
				}
				else
				{
					this.result.fireBarrel_ = value;
				}
				this.result.hasFireBarrel = true;
				return this;
			}

			// Token: 0x06001181 RID: 4481 RVA: 0x00040328 File Offset: 0x0003E528
			public SavedObject.Builder ClearFireBarrel()
			{
				this.PrepareBuilder();
				this.result.hasFireBarrel = false;
				this.result.fireBarrel_ = null;
				return this;
			}

			// Token: 0x17000492 RID: 1170
			// (get) Token: 0x06001182 RID: 4482 RVA: 0x00040358 File Offset: 0x0003E558
			public bool HasNetInstance
			{
				get
				{
					return this.result.hasNetInstance;
				}
			}

			// Token: 0x17000493 RID: 1171
			// (get) Token: 0x06001183 RID: 4483 RVA: 0x00040368 File Offset: 0x0003E568
			// (set) Token: 0x06001184 RID: 4484 RVA: 0x00040378 File Offset: 0x0003E578
			public objectNetInstance NetInstance
			{
				get
				{
					return this.result.NetInstance;
				}
				set
				{
					this.SetNetInstance(value);
				}
			}

			// Token: 0x06001185 RID: 4485 RVA: 0x00040384 File Offset: 0x0003E584
			public SavedObject.Builder SetNetInstance(objectNetInstance value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasNetInstance = true;
				this.result.netInstance_ = value;
				return this;
			}

			// Token: 0x06001186 RID: 4486 RVA: 0x000403B4 File Offset: 0x0003E5B4
			public SavedObject.Builder SetNetInstance(objectNetInstance.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.hasNetInstance = true;
				this.result.netInstance_ = builderForValue.Build();
				return this;
			}

			// Token: 0x06001187 RID: 4487 RVA: 0x000403F4 File Offset: 0x0003E5F4
			public SavedObject.Builder MergeNetInstance(objectNetInstance value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				if (this.result.hasNetInstance && this.result.netInstance_ != objectNetInstance.DefaultInstance)
				{
					this.result.netInstance_ = objectNetInstance.CreateBuilder(this.result.netInstance_).MergeFrom(value).BuildPartial();
				}
				else
				{
					this.result.netInstance_ = value;
				}
				this.result.hasNetInstance = true;
				return this;
			}

			// Token: 0x06001188 RID: 4488 RVA: 0x0004047C File Offset: 0x0003E67C
			public SavedObject.Builder ClearNetInstance()
			{
				this.PrepareBuilder();
				this.result.hasNetInstance = false;
				this.result.netInstance_ = null;
				return this;
			}

			// Token: 0x17000494 RID: 1172
			// (get) Token: 0x06001189 RID: 4489 RVA: 0x000404AC File Offset: 0x0003E6AC
			public bool HasCoords
			{
				get
				{
					return this.result.hasCoords;
				}
			}

			// Token: 0x17000495 RID: 1173
			// (get) Token: 0x0600118A RID: 4490 RVA: 0x000404BC File Offset: 0x0003E6BC
			// (set) Token: 0x0600118B RID: 4491 RVA: 0x000404CC File Offset: 0x0003E6CC
			public objectCoords Coords
			{
				get
				{
					return this.result.Coords;
				}
				set
				{
					this.SetCoords(value);
				}
			}

			// Token: 0x0600118C RID: 4492 RVA: 0x000404D8 File Offset: 0x0003E6D8
			public SavedObject.Builder SetCoords(objectCoords value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasCoords = true;
				this.result.coords_ = value;
				return this;
			}

			// Token: 0x0600118D RID: 4493 RVA: 0x00040508 File Offset: 0x0003E708
			public SavedObject.Builder SetCoords(objectCoords.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.hasCoords = true;
				this.result.coords_ = builderForValue.Build();
				return this;
			}

			// Token: 0x0600118E RID: 4494 RVA: 0x00040548 File Offset: 0x0003E748
			public SavedObject.Builder MergeCoords(objectCoords value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				if (this.result.hasCoords && this.result.coords_ != objectCoords.DefaultInstance)
				{
					this.result.coords_ = objectCoords.CreateBuilder(this.result.coords_).MergeFrom(value).BuildPartial();
				}
				else
				{
					this.result.coords_ = value;
				}
				this.result.hasCoords = true;
				return this;
			}

			// Token: 0x0600118F RID: 4495 RVA: 0x000405D0 File Offset: 0x0003E7D0
			public SavedObject.Builder ClearCoords()
			{
				this.PrepareBuilder();
				this.result.hasCoords = false;
				this.result.coords_ = null;
				return this;
			}

			// Token: 0x17000496 RID: 1174
			// (get) Token: 0x06001190 RID: 4496 RVA: 0x00040600 File Offset: 0x0003E800
			public bool HasNgcInstance
			{
				get
				{
					return this.result.hasNgcInstance;
				}
			}

			// Token: 0x17000497 RID: 1175
			// (get) Token: 0x06001191 RID: 4497 RVA: 0x00040610 File Offset: 0x0003E810
			// (set) Token: 0x06001192 RID: 4498 RVA: 0x00040620 File Offset: 0x0003E820
			public objectNGCInstance NgcInstance
			{
				get
				{
					return this.result.NgcInstance;
				}
				set
				{
					this.SetNgcInstance(value);
				}
			}

			// Token: 0x06001193 RID: 4499 RVA: 0x0004062C File Offset: 0x0003E82C
			public SavedObject.Builder SetNgcInstance(objectNGCInstance value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasNgcInstance = true;
				this.result.ngcInstance_ = value;
				return this;
			}

			// Token: 0x06001194 RID: 4500 RVA: 0x0004065C File Offset: 0x0003E85C
			public SavedObject.Builder SetNgcInstance(objectNGCInstance.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.hasNgcInstance = true;
				this.result.ngcInstance_ = builderForValue.Build();
				return this;
			}

			// Token: 0x06001195 RID: 4501 RVA: 0x0004069C File Offset: 0x0003E89C
			public SavedObject.Builder MergeNgcInstance(objectNGCInstance value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				if (this.result.hasNgcInstance && this.result.ngcInstance_ != objectNGCInstance.DefaultInstance)
				{
					this.result.ngcInstance_ = objectNGCInstance.CreateBuilder(this.result.ngcInstance_).MergeFrom(value).BuildPartial();
				}
				else
				{
					this.result.ngcInstance_ = value;
				}
				this.result.hasNgcInstance = true;
				return this;
			}

			// Token: 0x06001196 RID: 4502 RVA: 0x00040724 File Offset: 0x0003E924
			public SavedObject.Builder ClearNgcInstance()
			{
				this.PrepareBuilder();
				this.result.hasNgcInstance = false;
				this.result.ngcInstance_ = null;
				return this;
			}

			// Token: 0x17000498 RID: 1176
			// (get) Token: 0x06001197 RID: 4503 RVA: 0x00040754 File Offset: 0x0003E954
			public bool HasCarriableTrans
			{
				get
				{
					return this.result.hasCarriableTrans;
				}
			}

			// Token: 0x17000499 RID: 1177
			// (get) Token: 0x06001198 RID: 4504 RVA: 0x00040764 File Offset: 0x0003E964
			// (set) Token: 0x06001199 RID: 4505 RVA: 0x00040774 File Offset: 0x0003E974
			public objectICarriableTrans CarriableTrans
			{
				get
				{
					return this.result.CarriableTrans;
				}
				set
				{
					this.SetCarriableTrans(value);
				}
			}

			// Token: 0x0600119A RID: 4506 RVA: 0x00040780 File Offset: 0x0003E980
			public SavedObject.Builder SetCarriableTrans(objectICarriableTrans value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasCarriableTrans = true;
				this.result.carriableTrans_ = value;
				return this;
			}

			// Token: 0x0600119B RID: 4507 RVA: 0x000407B0 File Offset: 0x0003E9B0
			public SavedObject.Builder SetCarriableTrans(objectICarriableTrans.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.hasCarriableTrans = true;
				this.result.carriableTrans_ = builderForValue.Build();
				return this;
			}

			// Token: 0x0600119C RID: 4508 RVA: 0x000407F0 File Offset: 0x0003E9F0
			public SavedObject.Builder MergeCarriableTrans(objectICarriableTrans value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				if (this.result.hasCarriableTrans && this.result.carriableTrans_ != objectICarriableTrans.DefaultInstance)
				{
					this.result.carriableTrans_ = objectICarriableTrans.CreateBuilder(this.result.carriableTrans_).MergeFrom(value).BuildPartial();
				}
				else
				{
					this.result.carriableTrans_ = value;
				}
				this.result.hasCarriableTrans = true;
				return this;
			}

			// Token: 0x0600119D RID: 4509 RVA: 0x00040878 File Offset: 0x0003EA78
			public SavedObject.Builder ClearCarriableTrans()
			{
				this.PrepareBuilder();
				this.result.hasCarriableTrans = false;
				this.result.carriableTrans_ = null;
				return this;
			}

			// Token: 0x1700049A RID: 1178
			// (get) Token: 0x0600119E RID: 4510 RVA: 0x000408A8 File Offset: 0x0003EAA8
			public bool HasTakeDamage
			{
				get
				{
					return this.result.hasTakeDamage;
				}
			}

			// Token: 0x1700049B RID: 1179
			// (get) Token: 0x0600119F RID: 4511 RVA: 0x000408B8 File Offset: 0x0003EAB8
			// (set) Token: 0x060011A0 RID: 4512 RVA: 0x000408C8 File Offset: 0x0003EAC8
			public objectTakeDamage TakeDamage
			{
				get
				{
					return this.result.TakeDamage;
				}
				set
				{
					this.SetTakeDamage(value);
				}
			}

			// Token: 0x060011A1 RID: 4513 RVA: 0x000408D4 File Offset: 0x0003EAD4
			public SavedObject.Builder SetTakeDamage(objectTakeDamage value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasTakeDamage = true;
				this.result.takeDamage_ = value;
				return this;
			}

			// Token: 0x060011A2 RID: 4514 RVA: 0x00040904 File Offset: 0x0003EB04
			public SavedObject.Builder SetTakeDamage(objectTakeDamage.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.hasTakeDamage = true;
				this.result.takeDamage_ = builderForValue.Build();
				return this;
			}

			// Token: 0x060011A3 RID: 4515 RVA: 0x00040944 File Offset: 0x0003EB44
			public SavedObject.Builder MergeTakeDamage(objectTakeDamage value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				if (this.result.hasTakeDamage && this.result.takeDamage_ != objectTakeDamage.DefaultInstance)
				{
					this.result.takeDamage_ = objectTakeDamage.CreateBuilder(this.result.takeDamage_).MergeFrom(value).BuildPartial();
				}
				else
				{
					this.result.takeDamage_ = value;
				}
				this.result.hasTakeDamage = true;
				return this;
			}

			// Token: 0x060011A4 RID: 4516 RVA: 0x000409CC File Offset: 0x0003EBCC
			public SavedObject.Builder ClearTakeDamage()
			{
				this.PrepareBuilder();
				this.result.hasTakeDamage = false;
				this.result.takeDamage_ = null;
				return this;
			}

			// Token: 0x1700049C RID: 1180
			// (get) Token: 0x060011A5 RID: 4517 RVA: 0x000409FC File Offset: 0x0003EBFC
			public bool HasSortOrder
			{
				get
				{
					return this.result.hasSortOrder;
				}
			}

			// Token: 0x1700049D RID: 1181
			// (get) Token: 0x060011A6 RID: 4518 RVA: 0x00040A0C File Offset: 0x0003EC0C
			// (set) Token: 0x060011A7 RID: 4519 RVA: 0x00040A1C File Offset: 0x0003EC1C
			public int SortOrder
			{
				get
				{
					return this.result.SortOrder;
				}
				set
				{
					this.SetSortOrder(value);
				}
			}

			// Token: 0x060011A8 RID: 4520 RVA: 0x00040A28 File Offset: 0x0003EC28
			public SavedObject.Builder SetSortOrder(int value)
			{
				this.PrepareBuilder();
				this.result.hasSortOrder = true;
				this.result.sortOrder_ = value;
				return this;
			}

			// Token: 0x060011A9 RID: 4521 RVA: 0x00040A58 File Offset: 0x0003EC58
			public SavedObject.Builder ClearSortOrder()
			{
				this.PrepareBuilder();
				this.result.hasSortOrder = false;
				this.result.sortOrder_ = 0;
				return this;
			}

			// Token: 0x1700049E RID: 1182
			// (get) Token: 0x060011AA RID: 4522 RVA: 0x00040A88 File Offset: 0x0003EC88
			public bool HasSleepingAvatar
			{
				get
				{
					return this.result.hasSleepingAvatar;
				}
			}

			// Token: 0x1700049F RID: 1183
			// (get) Token: 0x060011AB RID: 4523 RVA: 0x00040A98 File Offset: 0x0003EC98
			// (set) Token: 0x060011AC RID: 4524 RVA: 0x00040AA8 File Offset: 0x0003ECA8
			public objectSleepingAvatar SleepingAvatar
			{
				get
				{
					return this.result.SleepingAvatar;
				}
				set
				{
					this.SetSleepingAvatar(value);
				}
			}

			// Token: 0x060011AD RID: 4525 RVA: 0x00040AB4 File Offset: 0x0003ECB4
			public SavedObject.Builder SetSleepingAvatar(objectSleepingAvatar value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasSleepingAvatar = true;
				this.result.sleepingAvatar_ = value;
				return this;
			}

			// Token: 0x060011AE RID: 4526 RVA: 0x00040AE4 File Offset: 0x0003ECE4
			public SavedObject.Builder SetSleepingAvatar(objectSleepingAvatar.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.hasSleepingAvatar = true;
				this.result.sleepingAvatar_ = builderForValue.Build();
				return this;
			}

			// Token: 0x060011AF RID: 4527 RVA: 0x00040B24 File Offset: 0x0003ED24
			public SavedObject.Builder MergeSleepingAvatar(objectSleepingAvatar value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				if (this.result.hasSleepingAvatar && this.result.sleepingAvatar_ != objectSleepingAvatar.DefaultInstance)
				{
					this.result.sleepingAvatar_ = objectSleepingAvatar.CreateBuilder(this.result.sleepingAvatar_).MergeFrom(value).BuildPartial();
				}
				else
				{
					this.result.sleepingAvatar_ = value;
				}
				this.result.hasSleepingAvatar = true;
				return this;
			}

			// Token: 0x060011B0 RID: 4528 RVA: 0x00040BAC File Offset: 0x0003EDAC
			public SavedObject.Builder ClearSleepingAvatar()
			{
				this.PrepareBuilder();
				this.result.hasSleepingAvatar = false;
				this.result.sleepingAvatar_ = null;
				return this;
			}

			// Token: 0x170004A0 RID: 1184
			// (get) Token: 0x060011B1 RID: 4529 RVA: 0x00040BDC File Offset: 0x0003EDDC
			public bool HasLockable
			{
				get
				{
					return this.result.hasLockable;
				}
			}

			// Token: 0x170004A1 RID: 1185
			// (get) Token: 0x060011B2 RID: 4530 RVA: 0x00040BEC File Offset: 0x0003EDEC
			// (set) Token: 0x060011B3 RID: 4531 RVA: 0x00040BFC File Offset: 0x0003EDFC
			public objectLockable Lockable
			{
				get
				{
					return this.result.Lockable;
				}
				set
				{
					this.SetLockable(value);
				}
			}

			// Token: 0x060011B4 RID: 4532 RVA: 0x00040C08 File Offset: 0x0003EE08
			public SavedObject.Builder SetLockable(objectLockable value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasLockable = true;
				this.result.lockable_ = value;
				return this;
			}

			// Token: 0x060011B5 RID: 4533 RVA: 0x00040C38 File Offset: 0x0003EE38
			public SavedObject.Builder SetLockable(objectLockable.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.hasLockable = true;
				this.result.lockable_ = builderForValue.Build();
				return this;
			}

			// Token: 0x060011B6 RID: 4534 RVA: 0x00040C78 File Offset: 0x0003EE78
			public SavedObject.Builder MergeLockable(objectLockable value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				if (this.result.hasLockable && this.result.lockable_ != objectLockable.DefaultInstance)
				{
					this.result.lockable_ = objectLockable.CreateBuilder(this.result.lockable_).MergeFrom(value).BuildPartial();
				}
				else
				{
					this.result.lockable_ = value;
				}
				this.result.hasLockable = true;
				return this;
			}

			// Token: 0x060011B7 RID: 4535 RVA: 0x00040D00 File Offset: 0x0003EF00
			public SavedObject.Builder ClearLockable()
			{
				this.PrepareBuilder();
				this.result.hasLockable = false;
				this.result.lockable_ = null;
				return this;
			}

			// Token: 0x04000A4E RID: 2638
			private bool resultIsReadOnly;

			// Token: 0x04000A4F RID: 2639
			private SavedObject result;
		}
	}
}
