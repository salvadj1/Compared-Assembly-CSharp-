using System;
using Facepunch.Build;
using Facepunch.Hash;
using uLink;
using UnityEngine;

// Token: 0x020005A0 RID: 1440
[UniqueBundleScriptableObject]
public class Datablock : ScriptableObject, IComparable<Datablock>
{
	// Token: 0x17000A46 RID: 2630
	// (get) Token: 0x06003424 RID: 13348 RVA: 0x000BEAD4 File Offset: 0x000BCCD4
	public int uniqueID
	{
		get
		{
			return this._uniqueID;
		}
	}

	// Token: 0x06003425 RID: 13349 RVA: 0x000BEADC File Offset: 0x000BCCDC
	public override int GetHashCode()
	{
		return this._uniqueID;
	}

	// Token: 0x06003426 RID: 13350 RVA: 0x000BEAE4 File Offset: 0x000BCCE4
	public int CompareTo(Datablock other)
	{
		if (object.ReferenceEquals(other, this))
		{
			return 0;
		}
		if (!other)
		{
			return -1;
		}
		int num = this._uniqueID.CompareTo(other._uniqueID);
		if (num == 0)
		{
			return base.name.CompareTo(other.name);
		}
		return num;
	}

	// Token: 0x06003427 RID: 13351 RVA: 0x000BEB38 File Offset: 0x000BCD38
	protected virtual void SecureWriteMemberValues(BitStream stream)
	{
		stream.WriteInt32(this._uniqueID);
	}

	// Token: 0x06003428 RID: 13352 RVA: 0x000BEB48 File Offset: 0x000BCD48
	public uint SecureHash()
	{
		return this.SecureHash(0u);
	}

	// Token: 0x06003429 RID: 13353 RVA: 0x000BEB54 File Offset: 0x000BCD54
	public uint SecureHash(uint seed)
	{
		BitStream stream = new BitStream(true);
		try
		{
			this.SecureWriteMemberValues(stream);
		}
		catch (Exception ex)
		{
			Debug.LogException(ex);
		}
		return MurmurHash2.UINT(stream.GetDataByteArray(), seed);
	}

	// Token: 0x04001A0D RID: 6669
	[SerializeField]
	[HideInInspector]
	private int _uniqueID;

	// Token: 0x020005A1 RID: 1441
	public struct Ident : IEquatable<Datablock.Ident>, IEquatable<Datablock>
	{
		// Token: 0x0600342A RID: 13354 RVA: 0x000BEBA8 File Offset: 0x000BCDA8
		private Ident(object refValue, int uniqueID, byte type_f)
		{
			this.refValue = refValue;
			this.uid = uniqueID;
			this.type_f = type_f;
		}

		// Token: 0x0600342B RID: 13355 RVA: 0x000BEBC0 File Offset: 0x000BCDC0
		private Ident(object referenceValue, bool isNull, byte type)
		{
			if (isNull)
			{
				this = default(Datablock.Ident);
			}
			else
			{
				this.refValue = referenceValue;
				this.uid = 0;
				this.type_f = type;
			}
		}

		// Token: 0x0600342C RID: 13356 RVA: 0x000BEBFC File Offset: 0x000BCDFC
		private Ident(object referenceValue, byte type)
		{
			this = new Datablock.Ident(referenceValue, !object.ReferenceEquals(referenceValue, null), type);
		}

		// Token: 0x0600342D RID: 13357 RVA: 0x000BEC10 File Offset: 0x000BCE10
		private Ident(Datablock db)
		{
			this = new Datablock.Ident(db, db, 129);
		}

		// Token: 0x0600342E RID: 13358 RVA: 0x000BEC24 File Offset: 0x000BCE24
		private Ident(InventoryItem item)
		{
			this = new Datablock.Ident(item, 130);
		}

		// Token: 0x0600342F RID: 13359 RVA: 0x000BEC34 File Offset: 0x000BCE34
		private Ident(string name)
		{
			this = new Datablock.Ident(name, string.IsNullOrEmpty(name), 131);
		}

		// Token: 0x06003430 RID: 13360 RVA: 0x000BEC48 File Offset: 0x000BCE48
		private Ident(int uniqueID)
		{
			this.refValue = null;
			this.type_f = 132;
			this.uid = uniqueID;
		}

		// Token: 0x06003431 RID: 13361 RVA: 0x000BEC64 File Offset: 0x000BCE64
		private void Confirm()
		{
			Datablock datablock;
			switch (this.type_f & 127)
			{
			case 1:
				datablock = (Datablock)this.refValue;
				break;
			case 2:
				datablock = ((InventoryItem)this.refValue).datablock;
				break;
			case 3:
				datablock = DatablockDictionary.GetByName((string)this.refValue);
				break;
			case 4:
				datablock = DatablockDictionary.GetByUniqueID(this.uid);
				break;
			default:
				this = default(Datablock.Ident);
				return;
			}
			if (datablock)
			{
				this = new Datablock.Ident(datablock, datablock.uniqueID, 1);
			}
			else
			{
				this = default(Datablock.Ident);
			}
		}

		// Token: 0x06003432 RID: 13362 RVA: 0x000BED24 File Offset: 0x000BCF24
		public override int GetHashCode()
		{
			return this.uid;
		}

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x06003433 RID: 13363 RVA: 0x000BED2C File Offset: 0x000BCF2C
		public Datablock datablock
		{
			get
			{
				if ((this.type_f & 128) == 128)
				{
					this.Confirm();
				}
				return (Datablock)this.refValue;
			}
		}

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x06003434 RID: 13364 RVA: 0x000BED58 File Offset: 0x000BCF58
		public int uniqueID
		{
			get
			{
				if ((this.type_f & 128) == 128)
				{
					this.Confirm();
				}
				return this.uid;
			}
		}

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x06003435 RID: 13365 RVA: 0x000BED88 File Offset: 0x000BCF88
		public int? uniqueIDIfExists
		{
			get
			{
				if ((this.type_f & 128) == 128)
				{
					this.Confirm();
				}
				if (this.type_f != 0)
				{
					return new int?(this.uid);
				}
				return null;
			}
		}

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x06003436 RID: 13366 RVA: 0x000BEDD4 File Offset: 0x000BCFD4
		public bool exists
		{
			get
			{
				if ((this.type_f & 128) == 128)
				{
					this.Confirm();
				}
				return this.type_f != 0 && (Datablock)this.refValue;
			}
		}

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x06003437 RID: 13367 RVA: 0x000BEE1C File Offset: 0x000BD01C
		public string name
		{
			get
			{
				if ((this.type_f & 128) == 128)
				{
					this.Confirm();
				}
				if (this.type_f != 1)
				{
					return string.Empty;
				}
				Datablock datablock = (Datablock)this.refValue;
				if (datablock)
				{
					return datablock.name;
				}
				return string.Empty;
			}
		}

		// Token: 0x06003438 RID: 13368 RVA: 0x000BEE7C File Offset: 0x000BD07C
		public bool Equals(Datablock.Ident other)
		{
			if ((this.type_f & 128) == 128)
			{
				this.Confirm();
			}
			if ((other.type_f & 128) == 128)
			{
				other.Confirm();
			}
			return object.Equals(this.refValue, other.refValue);
		}

		// Token: 0x06003439 RID: 13369 RVA: 0x000BEED8 File Offset: 0x000BD0D8
		public bool Equals(Datablock datablock)
		{
			if ((this.type_f & 128) == 128)
			{
				this.Confirm();
			}
			return object.Equals(this.refValue, datablock);
		}

		// Token: 0x0600343A RID: 13370 RVA: 0x000BEF10 File Offset: 0x000BD110
		public override bool Equals(object obj)
		{
			if (obj is Datablock.Ident)
			{
				return this.Equals((Datablock.Ident)obj);
			}
			return obj is Datablock && this.Equals((Datablock)obj);
		}

		// Token: 0x0600343B RID: 13371 RVA: 0x000BEF44 File Offset: 0x000BD144
		public override string ToString()
		{
			if ((this.type_f & 128) == 128)
			{
				this.Confirm();
			}
			Datablock datablock;
			return (this.type_f != 0 && (datablock = (Datablock)this.refValue)) ? datablock.name : "null";
		}

		// Token: 0x0600343C RID: 13372 RVA: 0x000BEFA0 File Offset: 0x000BD1A0
		public bool GetDatablock(out Datablock datablock)
		{
			if ((this.type_f & 128) == 128)
			{
				this.Confirm();
			}
			if (this.type_f == 0)
			{
				datablock = null;
				return false;
			}
			datablock = (Datablock)this.refValue;
			return datablock;
		}

		// Token: 0x0600343D RID: 13373 RVA: 0x000BEFF0 File Offset: 0x000BD1F0
		public bool GetDatablock<TDatablock>(out TDatablock datablock) where TDatablock : Datablock
		{
			if ((this.type_f & 128) == 128)
			{
				this.Confirm();
			}
			if (this.type_f == 0)
			{
				datablock = (TDatablock)((object)null);
				return false;
			}
			datablock = (((Datablock)this.refValue) as TDatablock);
			return datablock;
		}

		// Token: 0x0600343E RID: 13374 RVA: 0x000BF060 File Offset: 0x000BD260
		public Datablock GetDatablock()
		{
			if ((this.type_f & 128) == 128)
			{
				this.Confirm();
			}
			if (this.type_f == 0)
			{
				return null;
			}
			return (Datablock)this.refValue;
		}

		// Token: 0x0600343F RID: 13375 RVA: 0x000BF0A4 File Offset: 0x000BD2A4
		public Datablock GetDatablock<TDatablock>() where TDatablock : Datablock
		{
			if ((this.type_f & 128) == 128)
			{
				this.Confirm();
			}
			if (this.type_f == 0)
			{
				throw new MissingReferenceException("this identifier is not valid");
			}
			return (TDatablock)((object)this.refValue);
		}

		// Token: 0x06003440 RID: 13376 RVA: 0x000BF0F4 File Offset: 0x000BD2F4
		public static implicit operator Datablock.Ident(string dbName)
		{
			return new Datablock.Ident(dbName);
		}

		// Token: 0x06003441 RID: 13377 RVA: 0x000BF0FC File Offset: 0x000BD2FC
		public static implicit operator Datablock.Ident(int dbHash)
		{
			return new Datablock.Ident(dbHash);
		}

		// Token: 0x06003442 RID: 13378 RVA: 0x000BF104 File Offset: 0x000BD304
		public static implicit operator Datablock.Ident(uint dbHash)
		{
			return new Datablock.Ident((int)dbHash);
		}

		// Token: 0x06003443 RID: 13379 RVA: 0x000BF10C File Offset: 0x000BD30C
		[Obsolete("Make sure your wanting to get a dbhash from a ushort here.")]
		public static implicit operator Datablock.Ident(ushort dbHash)
		{
			return new Datablock.Ident((int)dbHash);
		}

		// Token: 0x06003444 RID: 13380 RVA: 0x000BF114 File Offset: 0x000BD314
		[Obsolete("Make sure your wanting to get a dbhash from a short here.")]
		public static implicit operator Datablock.Ident(short dbHash)
		{
			return new Datablock.Ident((int)dbHash);
		}

		// Token: 0x06003445 RID: 13381 RVA: 0x000BF11C File Offset: 0x000BD31C
		[Obsolete("Make sure your wanting to get a dbhash from a byte here.")]
		public static implicit operator Datablock.Ident(byte dbHash)
		{
			return new Datablock.Ident((int)dbHash);
		}

		// Token: 0x06003446 RID: 13382 RVA: 0x000BF124 File Offset: 0x000BD324
		[Obsolete("Make sure your wanting to get a dbhash from a sbyte here.")]
		public static implicit operator Datablock.Ident(sbyte dbHash)
		{
			return new Datablock.Ident((int)dbHash);
		}

		// Token: 0x06003447 RID: 13383 RVA: 0x000BF130 File Offset: 0x000BD330
		public static explicit operator Datablock.Ident(ulong dbHash)
		{
			uint uniqueID = (uint)dbHash;
			return new Datablock.Ident((int)uniqueID);
		}

		// Token: 0x06003448 RID: 13384 RVA: 0x000BF148 File Offset: 0x000BD348
		public static explicit operator Datablock.Ident(long dbHash)
		{
			int uniqueID = (int)dbHash;
			return new Datablock.Ident(uniqueID);
		}

		// Token: 0x06003449 RID: 13385 RVA: 0x000BF160 File Offset: 0x000BD360
		public static explicit operator Datablock.Ident(InventoryItem item)
		{
			return new Datablock.Ident(item);
		}

		// Token: 0x0600344A RID: 13386 RVA: 0x000BF168 File Offset: 0x000BD368
		public static explicit operator Datablock.Ident(Datablock db)
		{
			if (db)
			{
				return new Datablock.Ident(db, db.uniqueID, 1);
			}
			return default(Datablock.Ident);
		}

		// Token: 0x0600344B RID: 13387 RVA: 0x000BF198 File Offset: 0x000BD398
		public static Datablock.Ident operator +(Datablock.Ident ident)
		{
			if ((ident.type_f & 128) == 128)
			{
				ident.Confirm();
			}
			return ident;
		}

		// Token: 0x0600344C RID: 13388 RVA: 0x000BF1BC File Offset: 0x000BD3BC
		public static bool operator ==(Datablock.Ident ident, Datablock.Ident other)
		{
			return ident.Equals(other);
		}

		// Token: 0x0600344D RID: 13389 RVA: 0x000BF1C8 File Offset: 0x000BD3C8
		public static bool operator !=(Datablock.Ident ident, Datablock.Ident other)
		{
			return !ident.Equals(other);
		}

		// Token: 0x0600344E RID: 13390 RVA: 0x000BF1D8 File Offset: 0x000BD3D8
		public static bool operator ==(Datablock.Ident ident, Datablock other)
		{
			return ident.Equals(other);
		}

		// Token: 0x0600344F RID: 13391 RVA: 0x000BF1E4 File Offset: 0x000BD3E4
		public static bool operator !=(Datablock.Ident ident, Datablock other)
		{
			return !ident.Equals(other);
		}

		// Token: 0x06003450 RID: 13392 RVA: 0x000BF1F4 File Offset: 0x000BD3F4
		public static bool operator ==(Datablock.Ident ident, string other)
		{
			if (string.IsNullOrEmpty(other))
			{
				return !ident.exists;
			}
			return ident.name == other;
		}

		// Token: 0x06003451 RID: 13393 RVA: 0x000BF224 File Offset: 0x000BD424
		public static bool operator !=(Datablock.Ident ident, string other)
		{
			if (string.IsNullOrEmpty(other))
			{
				return ident.exists;
			}
			return ident.name != other;
		}

		// Token: 0x06003452 RID: 13394 RVA: 0x000BF254 File Offset: 0x000BD454
		public static bool operator ==(Datablock.Ident ident, int hash)
		{
			return ident.uniqueIDIfExists == hash;
		}

		// Token: 0x06003453 RID: 13395 RVA: 0x000BF280 File Offset: 0x000BD480
		public static bool operator !=(Datablock.Ident ident, int hash)
		{
			return ident.uniqueIDIfExists != hash;
		}

		// Token: 0x06003454 RID: 13396 RVA: 0x000BF2AC File Offset: 0x000BD4AC
		public static bool operator ==(Datablock.Ident ident, uint hash)
		{
			return ident.uniqueID == (int)hash;
		}

		// Token: 0x06003455 RID: 13397 RVA: 0x000BF2B8 File Offset: 0x000BD4B8
		public static bool operator !=(Datablock.Ident ident, uint hash)
		{
			return ident.uniqueID != (int)hash;
		}

		// Token: 0x06003456 RID: 13398 RVA: 0x000BF2C8 File Offset: 0x000BD4C8
		public static bool operator ==(Datablock.Ident ident, ushort hash)
		{
			return ident.uniqueIDIfExists == (int)hash;
		}

		// Token: 0x06003457 RID: 13399 RVA: 0x000BF2F4 File Offset: 0x000BD4F4
		public static bool operator !=(Datablock.Ident ident, ushort hash)
		{
			return ident.uniqueIDIfExists != (int)hash;
		}

		// Token: 0x06003458 RID: 13400 RVA: 0x000BF320 File Offset: 0x000BD520
		public static bool operator ==(Datablock.Ident ident, short hash)
		{
			return ident.uniqueID == (int)hash;
		}

		// Token: 0x06003459 RID: 13401 RVA: 0x000BF32C File Offset: 0x000BD52C
		public static bool operator !=(Datablock.Ident ident, short hash)
		{
			return ident.uniqueID != (int)hash;
		}

		// Token: 0x0600345A RID: 13402 RVA: 0x000BF33C File Offset: 0x000BD53C
		public static bool operator ==(Datablock.Ident ident, byte hash)
		{
			return ident.uniqueIDIfExists == (int)hash;
		}

		// Token: 0x0600345B RID: 13403 RVA: 0x000BF368 File Offset: 0x000BD568
		public static bool operator !=(Datablock.Ident ident, byte hash)
		{
			return ident.uniqueIDIfExists != (int)hash;
		}

		// Token: 0x0600345C RID: 13404 RVA: 0x000BF394 File Offset: 0x000BD594
		public static bool operator ==(Datablock.Ident ident, sbyte hash)
		{
			return ident.uniqueID == (int)hash;
		}

		// Token: 0x0600345D RID: 13405 RVA: 0x000BF3A4 File Offset: 0x000BD5A4
		public static bool operator !=(Datablock.Ident ident, sbyte hash)
		{
			return ident.uniqueID != (int)hash;
		}

		// Token: 0x0600345E RID: 13406 RVA: 0x000BF3B4 File Offset: 0x000BD5B4
		public static bool operator true(Datablock.Ident ident)
		{
			return ident.exists;
		}

		// Token: 0x0600345F RID: 13407 RVA: 0x000BF3C0 File Offset: 0x000BD5C0
		public static bool operator false(Datablock.Ident ident)
		{
			return !ident.exists;
		}

		// Token: 0x04001A0E RID: 6670
		private const byte TYPE_NULL = 0;

		// Token: 0x04001A0F RID: 6671
		private const byte TYPE_DATABLOCK = 1;

		// Token: 0x04001A10 RID: 6672
		private const byte TYPE_INVENTORY_ITEM = 2;

		// Token: 0x04001A11 RID: 6673
		private const byte TYPE_STRING = 3;

		// Token: 0x04001A12 RID: 6674
		private const byte TYPE_HASH = 4;

		// Token: 0x04001A13 RID: 6675
		private const int FLAG_UNCONFIRMED = 128;

		// Token: 0x04001A14 RID: 6676
		private const int MASK_TYPE = 127;

		// Token: 0x04001A15 RID: 6677
		private const byte TYPE_STRING_UNCONFIRMED = 131;

		// Token: 0x04001A16 RID: 6678
		private const byte TYPE_HASH_UNCONFIRMED = 132;

		// Token: 0x04001A17 RID: 6679
		private const byte TYPE_INVENTORY_ITEM_UNCONFIRMED = 130;

		// Token: 0x04001A18 RID: 6680
		private const byte TYPE_DATABLOCK_UNCONFIRMED = 129;

		// Token: 0x04001A19 RID: 6681
		private readonly object refValue;

		// Token: 0x04001A1A RID: 6682
		private readonly int uid;

		// Token: 0x04001A1B RID: 6683
		private readonly byte type_f;
	}
}
