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
	// Token: 0x02000207 RID: 519
	[DebuggerNonUserCode]
	public sealed class SavedObject : GeneratedMessage<SavedObject, SavedObject.Builder>
	{
		// Token: 0x06000FB5 RID: 4021 RVA: 0x0003A7AC File Offset: 0x000389AC
		private SavedObject()
		{
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x0003A7C8 File Offset: 0x000389C8
		static SavedObject()
		{
			object.ReferenceEquals(Worldsave.Descriptor, null);
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x0003A894 File Offset: 0x00038A94
		public static Recycler<SavedObject, SavedObject.Builder> Recycler()
		{
			return Recycler<SavedObject, SavedObject.Builder>.Manufacture();
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000FB8 RID: 4024 RVA: 0x0003A89C File Offset: 0x00038A9C
		public static SavedObject DefaultInstance
		{
			get
			{
				return SavedObject.defaultInstance;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000FB9 RID: 4025 RVA: 0x0003A8A4 File Offset: 0x00038AA4
		public override SavedObject DefaultInstanceForType
		{
			get
			{
				return SavedObject.DefaultInstance;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000FBA RID: 4026 RVA: 0x0003A8AC File Offset: 0x00038AAC
		protected override SavedObject ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000FBB RID: 4027 RVA: 0x0003A8B0 File Offset: 0x00038AB0
		public static MessageDescriptor Descriptor
		{
			get
			{
				return Worldsave.internal__static_RustProto_SavedObject__Descriptor;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000FBC RID: 4028 RVA: 0x0003A8B8 File Offset: 0x00038AB8
		protected override FieldAccessorTable<SavedObject, SavedObject.Builder> InternalFieldAccessors
		{
			get
			{
				return Worldsave.internal__static_RustProto_SavedObject__FieldAccessorTable;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000FBD RID: 4029 RVA: 0x0003A8C0 File Offset: 0x00038AC0
		public bool HasId
		{
			get
			{
				return this.hasId;
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000FBE RID: 4030 RVA: 0x0003A8C8 File Offset: 0x00038AC8
		public int Id
		{
			get
			{
				return this.id_;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000FBF RID: 4031 RVA: 0x0003A8D0 File Offset: 0x00038AD0
		public bool HasDoor
		{
			get
			{
				return this.hasDoor;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000FC0 RID: 4032 RVA: 0x0003A8D8 File Offset: 0x00038AD8
		public objectDoor Door
		{
			get
			{
				return this.door_ ?? objectDoor.DefaultInstance;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000FC1 RID: 4033 RVA: 0x0003A8EC File Offset: 0x00038AEC
		public IList<Item> InventoryList
		{
			get
			{
				return this.inventory_;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000FC2 RID: 4034 RVA: 0x0003A8F4 File Offset: 0x00038AF4
		public int InventoryCount
		{
			get
			{
				return this.inventory_.Count;
			}
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x0003A904 File Offset: 0x00038B04
		public Item GetInventory(int index)
		{
			return this.inventory_[index];
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000FC4 RID: 4036 RVA: 0x0003A914 File Offset: 0x00038B14
		public bool HasDeployable
		{
			get
			{
				return this.hasDeployable;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x0003A91C File Offset: 0x00038B1C
		public objectDeployable Deployable
		{
			get
			{
				return this.deployable_ ?? objectDeployable.DefaultInstance;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000FC6 RID: 4038 RVA: 0x0003A930 File Offset: 0x00038B30
		public bool HasStructMaster
		{
			get
			{
				return this.hasStructMaster;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x0003A938 File Offset: 0x00038B38
		public objectStructMaster StructMaster
		{
			get
			{
				return this.structMaster_ ?? objectStructMaster.DefaultInstance;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000FC8 RID: 4040 RVA: 0x0003A94C File Offset: 0x00038B4C
		public bool HasStructComponent
		{
			get
			{
				return this.hasStructComponent;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000FC9 RID: 4041 RVA: 0x0003A954 File Offset: 0x00038B54
		public objectStructComponent StructComponent
		{
			get
			{
				return this.structComponent_ ?? objectStructComponent.DefaultInstance;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000FCA RID: 4042 RVA: 0x0003A968 File Offset: 0x00038B68
		public bool HasFireBarrel
		{
			get
			{
				return this.hasFireBarrel;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000FCB RID: 4043 RVA: 0x0003A970 File Offset: 0x00038B70
		public objectFireBarrel FireBarrel
		{
			get
			{
				return this.fireBarrel_ ?? objectFireBarrel.DefaultInstance;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000FCC RID: 4044 RVA: 0x0003A984 File Offset: 0x00038B84
		public bool HasNetInstance
		{
			get
			{
				return this.hasNetInstance;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000FCD RID: 4045 RVA: 0x0003A98C File Offset: 0x00038B8C
		public objectNetInstance NetInstance
		{
			get
			{
				return this.netInstance_ ?? objectNetInstance.DefaultInstance;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000FCE RID: 4046 RVA: 0x0003A9A0 File Offset: 0x00038BA0
		public bool HasCoords
		{
			get
			{
				return this.hasCoords;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000FCF RID: 4047 RVA: 0x0003A9A8 File Offset: 0x00038BA8
		public objectCoords Coords
		{
			get
			{
				return this.coords_ ?? objectCoords.DefaultInstance;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000FD0 RID: 4048 RVA: 0x0003A9BC File Offset: 0x00038BBC
		public bool HasNgcInstance
		{
			get
			{
				return this.hasNgcInstance;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000FD1 RID: 4049 RVA: 0x0003A9C4 File Offset: 0x00038BC4
		public objectNGCInstance NgcInstance
		{
			get
			{
				return this.ngcInstance_ ?? objectNGCInstance.DefaultInstance;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000FD2 RID: 4050 RVA: 0x0003A9D8 File Offset: 0x00038BD8
		public bool HasCarriableTrans
		{
			get
			{
				return this.hasCarriableTrans;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000FD3 RID: 4051 RVA: 0x0003A9E0 File Offset: 0x00038BE0
		public objectICarriableTrans CarriableTrans
		{
			get
			{
				return this.carriableTrans_ ?? objectICarriableTrans.DefaultInstance;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x0003A9F4 File Offset: 0x00038BF4
		public bool HasTakeDamage
		{
			get
			{
				return this.hasTakeDamage;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x0003A9FC File Offset: 0x00038BFC
		public objectTakeDamage TakeDamage
		{
			get
			{
				return this.takeDamage_ ?? objectTakeDamage.DefaultInstance;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x0003AA10 File Offset: 0x00038C10
		public bool HasSortOrder
		{
			get
			{
				return this.hasSortOrder;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x0003AA18 File Offset: 0x00038C18
		public int SortOrder
		{
			get
			{
				return this.sortOrder_;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x0003AA20 File Offset: 0x00038C20
		public bool HasSleepingAvatar
		{
			get
			{
				return this.hasSleepingAvatar;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000FD9 RID: 4057 RVA: 0x0003AA28 File Offset: 0x00038C28
		public objectSleepingAvatar SleepingAvatar
		{
			get
			{
				return this.sleepingAvatar_ ?? objectSleepingAvatar.DefaultInstance;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x0003AA3C File Offset: 0x00038C3C
		public bool HasLockable
		{
			get
			{
				return this.hasLockable;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000FDB RID: 4059 RVA: 0x0003AA44 File Offset: 0x00038C44
		public objectLockable Lockable
		{
			get
			{
				return this.lockable_ ?? objectLockable.DefaultInstance;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x0003AA58 File Offset: 0x00038C58
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

		// Token: 0x06000FDD RID: 4061 RVA: 0x0003AACC File Offset: 0x00038CCC
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

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000FDE RID: 4062 RVA: 0x0003AC9C File Offset: 0x00038E9C
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

		// Token: 0x06000FDF RID: 4063 RVA: 0x0003AEA0 File Offset: 0x000390A0
		public static SavedObject ParseFrom(ByteString data)
		{
			return SavedObject.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x0003AEB4 File Offset: 0x000390B4
		public static SavedObject ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return SavedObject.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x0003AEC8 File Offset: 0x000390C8
		public static SavedObject ParseFrom(byte[] data)
		{
			return SavedObject.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x0003AEDC File Offset: 0x000390DC
		public static SavedObject ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return SavedObject.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x0003AEF0 File Offset: 0x000390F0
		public static SavedObject ParseFrom(Stream input)
		{
			return SavedObject.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x0003AF04 File Offset: 0x00039104
		public static SavedObject ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return SavedObject.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x0003AF18 File Offset: 0x00039118
		public static SavedObject ParseDelimitedFrom(Stream input)
		{
			return SavedObject.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x0003AF2C File Offset: 0x0003912C
		public static SavedObject ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return SavedObject.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x0003AF40 File Offset: 0x00039140
		public static SavedObject ParseFrom(ICodedInputStream input)
		{
			return SavedObject.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x0003AF54 File Offset: 0x00039154
		public static SavedObject ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return SavedObject.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x0003AF68 File Offset: 0x00039168
		private SavedObject MakeReadOnly()
		{
			this.inventory_.MakeReadOnly();
			return this;
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x0003AF78 File Offset: 0x00039178
		public static SavedObject.Builder CreateBuilder()
		{
			return new SavedObject.Builder();
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x0003AF80 File Offset: 0x00039180
		public override SavedObject.Builder ToBuilder()
		{
			return SavedObject.CreateBuilder(this);
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x0003AF88 File Offset: 0x00039188
		public override SavedObject.Builder CreateBuilderForType()
		{
			return new SavedObject.Builder();
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x0003AF90 File Offset: 0x00039190
		public static SavedObject.Builder CreateBuilder(SavedObject prototype)
		{
			return new SavedObject.Builder(prototype);
		}

		// Token: 0x040008FB RID: 2299
		public const int IdFieldNumber = 1;

		// Token: 0x040008FC RID: 2300
		public const int DoorFieldNumber = 2;

		// Token: 0x040008FD RID: 2301
		public const int InventoryFieldNumber = 3;

		// Token: 0x040008FE RID: 2302
		public const int DeployableFieldNumber = 4;

		// Token: 0x040008FF RID: 2303
		public const int StructMasterFieldNumber = 5;

		// Token: 0x04000900 RID: 2304
		public const int StructComponentFieldNumber = 6;

		// Token: 0x04000901 RID: 2305
		public const int FireBarrelFieldNumber = 7;

		// Token: 0x04000902 RID: 2306
		public const int NetInstanceFieldNumber = 8;

		// Token: 0x04000903 RID: 2307
		public const int CoordsFieldNumber = 9;

		// Token: 0x04000904 RID: 2308
		public const int NgcInstanceFieldNumber = 10;

		// Token: 0x04000905 RID: 2309
		public const int CarriableTransFieldNumber = 11;

		// Token: 0x04000906 RID: 2310
		public const int TakeDamageFieldNumber = 12;

		// Token: 0x04000907 RID: 2311
		public const int SortOrderFieldNumber = 13;

		// Token: 0x04000908 RID: 2312
		public const int SleepingAvatarFieldNumber = 14;

		// Token: 0x04000909 RID: 2313
		public const int LockableFieldNumber = 15;

		// Token: 0x0400090A RID: 2314
		private static readonly SavedObject defaultInstance = new SavedObject().MakeReadOnly();

		// Token: 0x0400090B RID: 2315
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

		// Token: 0x0400090C RID: 2316
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

		// Token: 0x0400090D RID: 2317
		private bool hasId;

		// Token: 0x0400090E RID: 2318
		private int id_;

		// Token: 0x0400090F RID: 2319
		private bool hasDoor;

		// Token: 0x04000910 RID: 2320
		private objectDoor door_;

		// Token: 0x04000911 RID: 2321
		private PopsicleList<Item> inventory_ = new PopsicleList<Item>();

		// Token: 0x04000912 RID: 2322
		private bool hasDeployable;

		// Token: 0x04000913 RID: 2323
		private objectDeployable deployable_;

		// Token: 0x04000914 RID: 2324
		private bool hasStructMaster;

		// Token: 0x04000915 RID: 2325
		private objectStructMaster structMaster_;

		// Token: 0x04000916 RID: 2326
		private bool hasStructComponent;

		// Token: 0x04000917 RID: 2327
		private objectStructComponent structComponent_;

		// Token: 0x04000918 RID: 2328
		private bool hasFireBarrel;

		// Token: 0x04000919 RID: 2329
		private objectFireBarrel fireBarrel_;

		// Token: 0x0400091A RID: 2330
		private bool hasNetInstance;

		// Token: 0x0400091B RID: 2331
		private objectNetInstance netInstance_;

		// Token: 0x0400091C RID: 2332
		private bool hasCoords;

		// Token: 0x0400091D RID: 2333
		private objectCoords coords_;

		// Token: 0x0400091E RID: 2334
		private bool hasNgcInstance;

		// Token: 0x0400091F RID: 2335
		private objectNGCInstance ngcInstance_;

		// Token: 0x04000920 RID: 2336
		private bool hasCarriableTrans;

		// Token: 0x04000921 RID: 2337
		private objectICarriableTrans carriableTrans_;

		// Token: 0x04000922 RID: 2338
		private bool hasTakeDamage;

		// Token: 0x04000923 RID: 2339
		private objectTakeDamage takeDamage_;

		// Token: 0x04000924 RID: 2340
		private bool hasSortOrder;

		// Token: 0x04000925 RID: 2341
		private int sortOrder_;

		// Token: 0x04000926 RID: 2342
		private bool hasSleepingAvatar;

		// Token: 0x04000927 RID: 2343
		private objectSleepingAvatar sleepingAvatar_;

		// Token: 0x04000928 RID: 2344
		private bool hasLockable;

		// Token: 0x04000929 RID: 2345
		private objectLockable lockable_;

		// Token: 0x0400092A RID: 2346
		private int memoizedSerializedSize = -1;

		// Token: 0x02000208 RID: 520
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<SavedObject, SavedObject.Builder>
		{
			// Token: 0x06000FEE RID: 4078 RVA: 0x0003AF98 File Offset: 0x00039198
			public Builder()
			{
				this.result = SavedObject.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x06000FEF RID: 4079 RVA: 0x0003AFB4 File Offset: 0x000391B4
			internal Builder(SavedObject cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x17000437 RID: 1079
			// (get) Token: 0x06000FF0 RID: 4080 RVA: 0x0003AFCC File Offset: 0x000391CC
			protected override SavedObject.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x06000FF1 RID: 4081 RVA: 0x0003AFD0 File Offset: 0x000391D0
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

			// Token: 0x17000438 RID: 1080
			// (get) Token: 0x06000FF2 RID: 4082 RVA: 0x0003B010 File Offset: 0x00039210
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x17000439 RID: 1081
			// (get) Token: 0x06000FF3 RID: 4083 RVA: 0x0003B020 File Offset: 0x00039220
			protected override SavedObject MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x06000FF4 RID: 4084 RVA: 0x0003B028 File Offset: 0x00039228
			public override SavedObject.Builder Clear()
			{
				this.result = SavedObject.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x06000FF5 RID: 4085 RVA: 0x0003B040 File Offset: 0x00039240
			public override SavedObject.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new SavedObject.Builder(this.result);
				}
				return new SavedObject.Builder().MergeFrom(this.result);
			}

			// Token: 0x1700043A RID: 1082
			// (get) Token: 0x06000FF6 RID: 4086 RVA: 0x0003B06C File Offset: 0x0003926C
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return SavedObject.Descriptor;
				}
			}

			// Token: 0x1700043B RID: 1083
			// (get) Token: 0x06000FF7 RID: 4087 RVA: 0x0003B074 File Offset: 0x00039274
			public override SavedObject DefaultInstanceForType
			{
				get
				{
					return SavedObject.DefaultInstance;
				}
			}

			// Token: 0x06000FF8 RID: 4088 RVA: 0x0003B07C File Offset: 0x0003927C
			public override SavedObject BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x06000FF9 RID: 4089 RVA: 0x0003B0B0 File Offset: 0x000392B0
			public override SavedObject.Builder MergeFrom(IMessage other)
			{
				if (other is SavedObject)
				{
					return this.MergeFrom((SavedObject)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x06000FFA RID: 4090 RVA: 0x0003B0D4 File Offset: 0x000392D4
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

			// Token: 0x06000FFB RID: 4091 RVA: 0x0003B278 File Offset: 0x00039478
			public override SavedObject.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x06000FFC RID: 4092 RVA: 0x0003B288 File Offset: 0x00039488
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

			// Token: 0x1700043C RID: 1084
			// (get) Token: 0x06000FFD RID: 4093 RVA: 0x0003B758 File Offset: 0x00039958
			public bool HasId
			{
				get
				{
					return this.result.hasId;
				}
			}

			// Token: 0x1700043D RID: 1085
			// (get) Token: 0x06000FFE RID: 4094 RVA: 0x0003B768 File Offset: 0x00039968
			// (set) Token: 0x06000FFF RID: 4095 RVA: 0x0003B778 File Offset: 0x00039978
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

			// Token: 0x06001000 RID: 4096 RVA: 0x0003B784 File Offset: 0x00039984
			public SavedObject.Builder SetId(int value)
			{
				this.PrepareBuilder();
				this.result.hasId = true;
				this.result.id_ = value;
				return this;
			}

			// Token: 0x06001001 RID: 4097 RVA: 0x0003B7B4 File Offset: 0x000399B4
			public SavedObject.Builder ClearId()
			{
				this.PrepareBuilder();
				this.result.hasId = false;
				this.result.id_ = 0;
				return this;
			}

			// Token: 0x1700043E RID: 1086
			// (get) Token: 0x06001002 RID: 4098 RVA: 0x0003B7E4 File Offset: 0x000399E4
			public bool HasDoor
			{
				get
				{
					return this.result.hasDoor;
				}
			}

			// Token: 0x1700043F RID: 1087
			// (get) Token: 0x06001003 RID: 4099 RVA: 0x0003B7F4 File Offset: 0x000399F4
			// (set) Token: 0x06001004 RID: 4100 RVA: 0x0003B804 File Offset: 0x00039A04
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

			// Token: 0x06001005 RID: 4101 RVA: 0x0003B810 File Offset: 0x00039A10
			public SavedObject.Builder SetDoor(objectDoor value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasDoor = true;
				this.result.door_ = value;
				return this;
			}

			// Token: 0x06001006 RID: 4102 RVA: 0x0003B840 File Offset: 0x00039A40
			public SavedObject.Builder SetDoor(objectDoor.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.hasDoor = true;
				this.result.door_ = builderForValue.Build();
				return this;
			}

			// Token: 0x06001007 RID: 4103 RVA: 0x0003B880 File Offset: 0x00039A80
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

			// Token: 0x06001008 RID: 4104 RVA: 0x0003B908 File Offset: 0x00039B08
			public SavedObject.Builder ClearDoor()
			{
				this.PrepareBuilder();
				this.result.hasDoor = false;
				this.result.door_ = null;
				return this;
			}

			// Token: 0x17000440 RID: 1088
			// (get) Token: 0x06001009 RID: 4105 RVA: 0x0003B938 File Offset: 0x00039B38
			public IPopsicleList<Item> InventoryList
			{
				get
				{
					return this.PrepareBuilder().inventory_;
				}
			}

			// Token: 0x17000441 RID: 1089
			// (get) Token: 0x0600100A RID: 4106 RVA: 0x0003B948 File Offset: 0x00039B48
			public int InventoryCount
			{
				get
				{
					return this.result.InventoryCount;
				}
			}

			// Token: 0x0600100B RID: 4107 RVA: 0x0003B958 File Offset: 0x00039B58
			public Item GetInventory(int index)
			{
				return this.result.GetInventory(index);
			}

			// Token: 0x0600100C RID: 4108 RVA: 0x0003B968 File Offset: 0x00039B68
			public SavedObject.Builder SetInventory(int index, Item value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.inventory_[index] = value;
				return this;
			}

			// Token: 0x0600100D RID: 4109 RVA: 0x0003B990 File Offset: 0x00039B90
			public SavedObject.Builder SetInventory(int index, Item.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.inventory_[index] = builderForValue.Build();
				return this;
			}

			// Token: 0x0600100E RID: 4110 RVA: 0x0003B9C8 File Offset: 0x00039BC8
			public SavedObject.Builder AddInventory(Item value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.inventory_.Add(value);
				return this;
			}

			// Token: 0x0600100F RID: 4111 RVA: 0x0003B9FC File Offset: 0x00039BFC
			public SavedObject.Builder AddInventory(Item.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.inventory_.Add(builderForValue.Build());
				return this;
			}

			// Token: 0x06001010 RID: 4112 RVA: 0x0003BA28 File Offset: 0x00039C28
			public SavedObject.Builder AddRangeInventory(IEnumerable<Item> values)
			{
				this.PrepareBuilder();
				this.result.inventory_.Add(values);
				return this;
			}

			// Token: 0x06001011 RID: 4113 RVA: 0x0003BA44 File Offset: 0x00039C44
			public SavedObject.Builder ClearInventory()
			{
				this.PrepareBuilder();
				this.result.inventory_.Clear();
				return this;
			}

			// Token: 0x17000442 RID: 1090
			// (get) Token: 0x06001012 RID: 4114 RVA: 0x0003BA60 File Offset: 0x00039C60
			public bool HasDeployable
			{
				get
				{
					return this.result.hasDeployable;
				}
			}

			// Token: 0x17000443 RID: 1091
			// (get) Token: 0x06001013 RID: 4115 RVA: 0x0003BA70 File Offset: 0x00039C70
			// (set) Token: 0x06001014 RID: 4116 RVA: 0x0003BA80 File Offset: 0x00039C80
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

			// Token: 0x06001015 RID: 4117 RVA: 0x0003BA8C File Offset: 0x00039C8C
			public SavedObject.Builder SetDeployable(objectDeployable value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasDeployable = true;
				this.result.deployable_ = value;
				return this;
			}

			// Token: 0x06001016 RID: 4118 RVA: 0x0003BABC File Offset: 0x00039CBC
			public SavedObject.Builder SetDeployable(objectDeployable.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.hasDeployable = true;
				this.result.deployable_ = builderForValue.Build();
				return this;
			}

			// Token: 0x06001017 RID: 4119 RVA: 0x0003BAFC File Offset: 0x00039CFC
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

			// Token: 0x06001018 RID: 4120 RVA: 0x0003BB84 File Offset: 0x00039D84
			public SavedObject.Builder ClearDeployable()
			{
				this.PrepareBuilder();
				this.result.hasDeployable = false;
				this.result.deployable_ = null;
				return this;
			}

			// Token: 0x17000444 RID: 1092
			// (get) Token: 0x06001019 RID: 4121 RVA: 0x0003BBB4 File Offset: 0x00039DB4
			public bool HasStructMaster
			{
				get
				{
					return this.result.hasStructMaster;
				}
			}

			// Token: 0x17000445 RID: 1093
			// (get) Token: 0x0600101A RID: 4122 RVA: 0x0003BBC4 File Offset: 0x00039DC4
			// (set) Token: 0x0600101B RID: 4123 RVA: 0x0003BBD4 File Offset: 0x00039DD4
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

			// Token: 0x0600101C RID: 4124 RVA: 0x0003BBE0 File Offset: 0x00039DE0
			public SavedObject.Builder SetStructMaster(objectStructMaster value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasStructMaster = true;
				this.result.structMaster_ = value;
				return this;
			}

			// Token: 0x0600101D RID: 4125 RVA: 0x0003BC10 File Offset: 0x00039E10
			public SavedObject.Builder SetStructMaster(objectStructMaster.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.hasStructMaster = true;
				this.result.structMaster_ = builderForValue.Build();
				return this;
			}

			// Token: 0x0600101E RID: 4126 RVA: 0x0003BC50 File Offset: 0x00039E50
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

			// Token: 0x0600101F RID: 4127 RVA: 0x0003BCD8 File Offset: 0x00039ED8
			public SavedObject.Builder ClearStructMaster()
			{
				this.PrepareBuilder();
				this.result.hasStructMaster = false;
				this.result.structMaster_ = null;
				return this;
			}

			// Token: 0x17000446 RID: 1094
			// (get) Token: 0x06001020 RID: 4128 RVA: 0x0003BD08 File Offset: 0x00039F08
			public bool HasStructComponent
			{
				get
				{
					return this.result.hasStructComponent;
				}
			}

			// Token: 0x17000447 RID: 1095
			// (get) Token: 0x06001021 RID: 4129 RVA: 0x0003BD18 File Offset: 0x00039F18
			// (set) Token: 0x06001022 RID: 4130 RVA: 0x0003BD28 File Offset: 0x00039F28
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

			// Token: 0x06001023 RID: 4131 RVA: 0x0003BD34 File Offset: 0x00039F34
			public SavedObject.Builder SetStructComponent(objectStructComponent value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasStructComponent = true;
				this.result.structComponent_ = value;
				return this;
			}

			// Token: 0x06001024 RID: 4132 RVA: 0x0003BD64 File Offset: 0x00039F64
			public SavedObject.Builder SetStructComponent(objectStructComponent.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.hasStructComponent = true;
				this.result.structComponent_ = builderForValue.Build();
				return this;
			}

			// Token: 0x06001025 RID: 4133 RVA: 0x0003BDA4 File Offset: 0x00039FA4
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

			// Token: 0x06001026 RID: 4134 RVA: 0x0003BE2C File Offset: 0x0003A02C
			public SavedObject.Builder ClearStructComponent()
			{
				this.PrepareBuilder();
				this.result.hasStructComponent = false;
				this.result.structComponent_ = null;
				return this;
			}

			// Token: 0x17000448 RID: 1096
			// (get) Token: 0x06001027 RID: 4135 RVA: 0x0003BE5C File Offset: 0x0003A05C
			public bool HasFireBarrel
			{
				get
				{
					return this.result.hasFireBarrel;
				}
			}

			// Token: 0x17000449 RID: 1097
			// (get) Token: 0x06001028 RID: 4136 RVA: 0x0003BE6C File Offset: 0x0003A06C
			// (set) Token: 0x06001029 RID: 4137 RVA: 0x0003BE7C File Offset: 0x0003A07C
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

			// Token: 0x0600102A RID: 4138 RVA: 0x0003BE88 File Offset: 0x0003A088
			public SavedObject.Builder SetFireBarrel(objectFireBarrel value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasFireBarrel = true;
				this.result.fireBarrel_ = value;
				return this;
			}

			// Token: 0x0600102B RID: 4139 RVA: 0x0003BEB8 File Offset: 0x0003A0B8
			public SavedObject.Builder SetFireBarrel(objectFireBarrel.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.hasFireBarrel = true;
				this.result.fireBarrel_ = builderForValue.Build();
				return this;
			}

			// Token: 0x0600102C RID: 4140 RVA: 0x0003BEF8 File Offset: 0x0003A0F8
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

			// Token: 0x0600102D RID: 4141 RVA: 0x0003BF80 File Offset: 0x0003A180
			public SavedObject.Builder ClearFireBarrel()
			{
				this.PrepareBuilder();
				this.result.hasFireBarrel = false;
				this.result.fireBarrel_ = null;
				return this;
			}

			// Token: 0x1700044A RID: 1098
			// (get) Token: 0x0600102E RID: 4142 RVA: 0x0003BFB0 File Offset: 0x0003A1B0
			public bool HasNetInstance
			{
				get
				{
					return this.result.hasNetInstance;
				}
			}

			// Token: 0x1700044B RID: 1099
			// (get) Token: 0x0600102F RID: 4143 RVA: 0x0003BFC0 File Offset: 0x0003A1C0
			// (set) Token: 0x06001030 RID: 4144 RVA: 0x0003BFD0 File Offset: 0x0003A1D0
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

			// Token: 0x06001031 RID: 4145 RVA: 0x0003BFDC File Offset: 0x0003A1DC
			public SavedObject.Builder SetNetInstance(objectNetInstance value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasNetInstance = true;
				this.result.netInstance_ = value;
				return this;
			}

			// Token: 0x06001032 RID: 4146 RVA: 0x0003C00C File Offset: 0x0003A20C
			public SavedObject.Builder SetNetInstance(objectNetInstance.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.hasNetInstance = true;
				this.result.netInstance_ = builderForValue.Build();
				return this;
			}

			// Token: 0x06001033 RID: 4147 RVA: 0x0003C04C File Offset: 0x0003A24C
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

			// Token: 0x06001034 RID: 4148 RVA: 0x0003C0D4 File Offset: 0x0003A2D4
			public SavedObject.Builder ClearNetInstance()
			{
				this.PrepareBuilder();
				this.result.hasNetInstance = false;
				this.result.netInstance_ = null;
				return this;
			}

			// Token: 0x1700044C RID: 1100
			// (get) Token: 0x06001035 RID: 4149 RVA: 0x0003C104 File Offset: 0x0003A304
			public bool HasCoords
			{
				get
				{
					return this.result.hasCoords;
				}
			}

			// Token: 0x1700044D RID: 1101
			// (get) Token: 0x06001036 RID: 4150 RVA: 0x0003C114 File Offset: 0x0003A314
			// (set) Token: 0x06001037 RID: 4151 RVA: 0x0003C124 File Offset: 0x0003A324
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

			// Token: 0x06001038 RID: 4152 RVA: 0x0003C130 File Offset: 0x0003A330
			public SavedObject.Builder SetCoords(objectCoords value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasCoords = true;
				this.result.coords_ = value;
				return this;
			}

			// Token: 0x06001039 RID: 4153 RVA: 0x0003C160 File Offset: 0x0003A360
			public SavedObject.Builder SetCoords(objectCoords.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.hasCoords = true;
				this.result.coords_ = builderForValue.Build();
				return this;
			}

			// Token: 0x0600103A RID: 4154 RVA: 0x0003C1A0 File Offset: 0x0003A3A0
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

			// Token: 0x0600103B RID: 4155 RVA: 0x0003C228 File Offset: 0x0003A428
			public SavedObject.Builder ClearCoords()
			{
				this.PrepareBuilder();
				this.result.hasCoords = false;
				this.result.coords_ = null;
				return this;
			}

			// Token: 0x1700044E RID: 1102
			// (get) Token: 0x0600103C RID: 4156 RVA: 0x0003C258 File Offset: 0x0003A458
			public bool HasNgcInstance
			{
				get
				{
					return this.result.hasNgcInstance;
				}
			}

			// Token: 0x1700044F RID: 1103
			// (get) Token: 0x0600103D RID: 4157 RVA: 0x0003C268 File Offset: 0x0003A468
			// (set) Token: 0x0600103E RID: 4158 RVA: 0x0003C278 File Offset: 0x0003A478
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

			// Token: 0x0600103F RID: 4159 RVA: 0x0003C284 File Offset: 0x0003A484
			public SavedObject.Builder SetNgcInstance(objectNGCInstance value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasNgcInstance = true;
				this.result.ngcInstance_ = value;
				return this;
			}

			// Token: 0x06001040 RID: 4160 RVA: 0x0003C2B4 File Offset: 0x0003A4B4
			public SavedObject.Builder SetNgcInstance(objectNGCInstance.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.hasNgcInstance = true;
				this.result.ngcInstance_ = builderForValue.Build();
				return this;
			}

			// Token: 0x06001041 RID: 4161 RVA: 0x0003C2F4 File Offset: 0x0003A4F4
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

			// Token: 0x06001042 RID: 4162 RVA: 0x0003C37C File Offset: 0x0003A57C
			public SavedObject.Builder ClearNgcInstance()
			{
				this.PrepareBuilder();
				this.result.hasNgcInstance = false;
				this.result.ngcInstance_ = null;
				return this;
			}

			// Token: 0x17000450 RID: 1104
			// (get) Token: 0x06001043 RID: 4163 RVA: 0x0003C3AC File Offset: 0x0003A5AC
			public bool HasCarriableTrans
			{
				get
				{
					return this.result.hasCarriableTrans;
				}
			}

			// Token: 0x17000451 RID: 1105
			// (get) Token: 0x06001044 RID: 4164 RVA: 0x0003C3BC File Offset: 0x0003A5BC
			// (set) Token: 0x06001045 RID: 4165 RVA: 0x0003C3CC File Offset: 0x0003A5CC
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

			// Token: 0x06001046 RID: 4166 RVA: 0x0003C3D8 File Offset: 0x0003A5D8
			public SavedObject.Builder SetCarriableTrans(objectICarriableTrans value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasCarriableTrans = true;
				this.result.carriableTrans_ = value;
				return this;
			}

			// Token: 0x06001047 RID: 4167 RVA: 0x0003C408 File Offset: 0x0003A608
			public SavedObject.Builder SetCarriableTrans(objectICarriableTrans.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.hasCarriableTrans = true;
				this.result.carriableTrans_ = builderForValue.Build();
				return this;
			}

			// Token: 0x06001048 RID: 4168 RVA: 0x0003C448 File Offset: 0x0003A648
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

			// Token: 0x06001049 RID: 4169 RVA: 0x0003C4D0 File Offset: 0x0003A6D0
			public SavedObject.Builder ClearCarriableTrans()
			{
				this.PrepareBuilder();
				this.result.hasCarriableTrans = false;
				this.result.carriableTrans_ = null;
				return this;
			}

			// Token: 0x17000452 RID: 1106
			// (get) Token: 0x0600104A RID: 4170 RVA: 0x0003C500 File Offset: 0x0003A700
			public bool HasTakeDamage
			{
				get
				{
					return this.result.hasTakeDamage;
				}
			}

			// Token: 0x17000453 RID: 1107
			// (get) Token: 0x0600104B RID: 4171 RVA: 0x0003C510 File Offset: 0x0003A710
			// (set) Token: 0x0600104C RID: 4172 RVA: 0x0003C520 File Offset: 0x0003A720
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

			// Token: 0x0600104D RID: 4173 RVA: 0x0003C52C File Offset: 0x0003A72C
			public SavedObject.Builder SetTakeDamage(objectTakeDamage value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasTakeDamage = true;
				this.result.takeDamage_ = value;
				return this;
			}

			// Token: 0x0600104E RID: 4174 RVA: 0x0003C55C File Offset: 0x0003A75C
			public SavedObject.Builder SetTakeDamage(objectTakeDamage.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.hasTakeDamage = true;
				this.result.takeDamage_ = builderForValue.Build();
				return this;
			}

			// Token: 0x0600104F RID: 4175 RVA: 0x0003C59C File Offset: 0x0003A79C
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

			// Token: 0x06001050 RID: 4176 RVA: 0x0003C624 File Offset: 0x0003A824
			public SavedObject.Builder ClearTakeDamage()
			{
				this.PrepareBuilder();
				this.result.hasTakeDamage = false;
				this.result.takeDamage_ = null;
				return this;
			}

			// Token: 0x17000454 RID: 1108
			// (get) Token: 0x06001051 RID: 4177 RVA: 0x0003C654 File Offset: 0x0003A854
			public bool HasSortOrder
			{
				get
				{
					return this.result.hasSortOrder;
				}
			}

			// Token: 0x17000455 RID: 1109
			// (get) Token: 0x06001052 RID: 4178 RVA: 0x0003C664 File Offset: 0x0003A864
			// (set) Token: 0x06001053 RID: 4179 RVA: 0x0003C674 File Offset: 0x0003A874
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

			// Token: 0x06001054 RID: 4180 RVA: 0x0003C680 File Offset: 0x0003A880
			public SavedObject.Builder SetSortOrder(int value)
			{
				this.PrepareBuilder();
				this.result.hasSortOrder = true;
				this.result.sortOrder_ = value;
				return this;
			}

			// Token: 0x06001055 RID: 4181 RVA: 0x0003C6B0 File Offset: 0x0003A8B0
			public SavedObject.Builder ClearSortOrder()
			{
				this.PrepareBuilder();
				this.result.hasSortOrder = false;
				this.result.sortOrder_ = 0;
				return this;
			}

			// Token: 0x17000456 RID: 1110
			// (get) Token: 0x06001056 RID: 4182 RVA: 0x0003C6E0 File Offset: 0x0003A8E0
			public bool HasSleepingAvatar
			{
				get
				{
					return this.result.hasSleepingAvatar;
				}
			}

			// Token: 0x17000457 RID: 1111
			// (get) Token: 0x06001057 RID: 4183 RVA: 0x0003C6F0 File Offset: 0x0003A8F0
			// (set) Token: 0x06001058 RID: 4184 RVA: 0x0003C700 File Offset: 0x0003A900
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

			// Token: 0x06001059 RID: 4185 RVA: 0x0003C70C File Offset: 0x0003A90C
			public SavedObject.Builder SetSleepingAvatar(objectSleepingAvatar value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasSleepingAvatar = true;
				this.result.sleepingAvatar_ = value;
				return this;
			}

			// Token: 0x0600105A RID: 4186 RVA: 0x0003C73C File Offset: 0x0003A93C
			public SavedObject.Builder SetSleepingAvatar(objectSleepingAvatar.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.hasSleepingAvatar = true;
				this.result.sleepingAvatar_ = builderForValue.Build();
				return this;
			}

			// Token: 0x0600105B RID: 4187 RVA: 0x0003C77C File Offset: 0x0003A97C
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

			// Token: 0x0600105C RID: 4188 RVA: 0x0003C804 File Offset: 0x0003AA04
			public SavedObject.Builder ClearSleepingAvatar()
			{
				this.PrepareBuilder();
				this.result.hasSleepingAvatar = false;
				this.result.sleepingAvatar_ = null;
				return this;
			}

			// Token: 0x17000458 RID: 1112
			// (get) Token: 0x0600105D RID: 4189 RVA: 0x0003C834 File Offset: 0x0003AA34
			public bool HasLockable
			{
				get
				{
					return this.result.hasLockable;
				}
			}

			// Token: 0x17000459 RID: 1113
			// (get) Token: 0x0600105E RID: 4190 RVA: 0x0003C844 File Offset: 0x0003AA44
			// (set) Token: 0x0600105F RID: 4191 RVA: 0x0003C854 File Offset: 0x0003AA54
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

			// Token: 0x06001060 RID: 4192 RVA: 0x0003C860 File Offset: 0x0003AA60
			public SavedObject.Builder SetLockable(objectLockable value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasLockable = true;
				this.result.lockable_ = value;
				return this;
			}

			// Token: 0x06001061 RID: 4193 RVA: 0x0003C890 File Offset: 0x0003AA90
			public SavedObject.Builder SetLockable(objectLockable.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.hasLockable = true;
				this.result.lockable_ = builderForValue.Build();
				return this;
			}

			// Token: 0x06001062 RID: 4194 RVA: 0x0003C8D0 File Offset: 0x0003AAD0
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

			// Token: 0x06001063 RID: 4195 RVA: 0x0003C958 File Offset: 0x0003AB58
			public SavedObject.Builder ClearLockable()
			{
				this.PrepareBuilder();
				this.result.hasLockable = false;
				this.result.lockable_ = null;
				return this;
			}

			// Token: 0x0400092B RID: 2347
			private bool resultIsReadOnly;

			// Token: 0x0400092C RID: 2348
			private SavedObject result;
		}
	}
}
