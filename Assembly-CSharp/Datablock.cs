using System;
using Facepunch.Build;
using Facepunch.Hash;
using uLink;
using UnityEngine;

// Token: 0x0200065E RID: 1630
[UniqueBundleScriptableObject]
public class Datablock : ScriptableObject, IComparable<global::Datablock>
{
	// Token: 0x17000ABC RID: 2748
	// (get) Token: 0x060037EC RID: 14316 RVA: 0x000C6D30 File Offset: 0x000C4F30
	public int uniqueID
	{
		get
		{
			return this._uniqueID;
		}
	}

	// Token: 0x060037ED RID: 14317 RVA: 0x000C6D38 File Offset: 0x000C4F38
	public override int GetHashCode()
	{
		return this._uniqueID;
	}

	// Token: 0x060037EE RID: 14318 RVA: 0x000C6D40 File Offset: 0x000C4F40
	public int CompareTo(global::Datablock other)
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

	// Token: 0x060037EF RID: 14319 RVA: 0x000C6D94 File Offset: 0x000C4F94
	protected virtual void SecureWriteMemberValues(BitStream stream)
	{
		stream.WriteInt32(this._uniqueID);
	}

	// Token: 0x060037F0 RID: 14320 RVA: 0x000C6DA4 File Offset: 0x000C4FA4
	public uint SecureHash()
	{
		return this.SecureHash(0u);
	}

	// Token: 0x060037F1 RID: 14321 RVA: 0x000C6DB0 File Offset: 0x000C4FB0
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
		return Facepunch.Hash.MurmurHash2.UINT(stream.GetDataByteArray(), seed);
	}

	// Token: 0x04001BDE RID: 7134
	[HideInInspector]
	[SerializeField]
	private int _uniqueID;

	// Token: 0x0200065F RID: 1631
	public struct Ident : IEquatable<global::Datablock.Ident>, IEquatable<global::Datablock>
	{
		// Token: 0x060037F2 RID: 14322 RVA: 0x000C6E04 File Offset: 0x000C5004
		private Ident(object refValue, int uniqueID, byte type_f)
		{
			this.refValue = refValue;
			this.uid = uniqueID;
			this.type_f = type_f;
		}

		// Token: 0x060037F3 RID: 14323 RVA: 0x000C6E1C File Offset: 0x000C501C
		private Ident(object referenceValue, bool isNull, byte type)
		{
			if (isNull)
			{
				this = default(global::Datablock.Ident);
			}
			else
			{
				this.refValue = referenceValue;
				this.uid = 0;
				this.type_f = type;
			}
		}

		// Token: 0x060037F4 RID: 14324 RVA: 0x000C6E58 File Offset: 0x000C5058
		private Ident(object referenceValue, byte type)
		{
			this = new global::Datablock.Ident(referenceValue, !object.ReferenceEquals(referenceValue, null), type);
		}

		// Token: 0x060037F5 RID: 14325 RVA: 0x000C6E6C File Offset: 0x000C506C
		private Ident(global::Datablock db)
		{
			this = new global::Datablock.Ident(db, db, 129);
		}

		// Token: 0x060037F6 RID: 14326 RVA: 0x000C6E80 File Offset: 0x000C5080
		private Ident(global::InventoryItem item)
		{
			this = new global::Datablock.Ident(item, 130);
		}

		// Token: 0x060037F7 RID: 14327 RVA: 0x000C6E90 File Offset: 0x000C5090
		private Ident(string name)
		{
			this = new global::Datablock.Ident(name, string.IsNullOrEmpty(name), 131);
		}

		// Token: 0x060037F8 RID: 14328 RVA: 0x000C6EA4 File Offset: 0x000C50A4
		private Ident(int uniqueID)
		{
			this.refValue = null;
			this.type_f = 132;
			this.uid = uniqueID;
		}

		// Token: 0x060037F9 RID: 14329 RVA: 0x000C6EC0 File Offset: 0x000C50C0
		private void Confirm()
		{
			global::Datablock datablock;
			switch (this.type_f & 127)
			{
			case 1:
				datablock = (global::Datablock)this.refValue;
				break;
			case 2:
				datablock = ((global::InventoryItem)this.refValue).datablock;
				break;
			case 3:
				datablock = global::DatablockDictionary.GetByName((string)this.refValue);
				break;
			case 4:
				datablock = global::DatablockDictionary.GetByUniqueID(this.uid);
				break;
			default:
				this = default(global::Datablock.Ident);
				return;
			}
			if (datablock)
			{
				this = new global::Datablock.Ident(datablock, datablock.uniqueID, 1);
			}
			else
			{
				this = default(global::Datablock.Ident);
			}
		}

		// Token: 0x060037FA RID: 14330 RVA: 0x000C6F80 File Offset: 0x000C5180
		public override int GetHashCode()
		{
			return this.uid;
		}

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x060037FB RID: 14331 RVA: 0x000C6F88 File Offset: 0x000C5188
		public global::Datablock datablock
		{
			get
			{
				if ((this.type_f & 128) == 128)
				{
					this.Confirm();
				}
				return (global::Datablock)this.refValue;
			}
		}

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x060037FC RID: 14332 RVA: 0x000C6FB4 File Offset: 0x000C51B4
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

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x060037FD RID: 14333 RVA: 0x000C6FE4 File Offset: 0x000C51E4
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

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x060037FE RID: 14334 RVA: 0x000C7030 File Offset: 0x000C5230
		public bool exists
		{
			get
			{
				if ((this.type_f & 128) == 128)
				{
					this.Confirm();
				}
				return this.type_f != 0 && (global::Datablock)this.refValue;
			}
		}

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x060037FF RID: 14335 RVA: 0x000C7078 File Offset: 0x000C5278
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
				global::Datablock datablock = (global::Datablock)this.refValue;
				if (datablock)
				{
					return datablock.name;
				}
				return string.Empty;
			}
		}

		// Token: 0x06003800 RID: 14336 RVA: 0x000C70D8 File Offset: 0x000C52D8
		public bool Equals(global::Datablock.Ident other)
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

		// Token: 0x06003801 RID: 14337 RVA: 0x000C7134 File Offset: 0x000C5334
		public bool Equals(global::Datablock datablock)
		{
			if ((this.type_f & 128) == 128)
			{
				this.Confirm();
			}
			return object.Equals(this.refValue, datablock);
		}

		// Token: 0x06003802 RID: 14338 RVA: 0x000C716C File Offset: 0x000C536C
		public override bool Equals(object obj)
		{
			if (obj is global::Datablock.Ident)
			{
				return this.Equals((global::Datablock.Ident)obj);
			}
			return obj is global::Datablock && this.Equals((global::Datablock)obj);
		}

		// Token: 0x06003803 RID: 14339 RVA: 0x000C71A0 File Offset: 0x000C53A0
		public override string ToString()
		{
			if ((this.type_f & 128) == 128)
			{
				this.Confirm();
			}
			global::Datablock datablock;
			return (this.type_f != 0 && (datablock = (global::Datablock)this.refValue)) ? datablock.name : "null";
		}

		// Token: 0x06003804 RID: 14340 RVA: 0x000C71FC File Offset: 0x000C53FC
		public bool GetDatablock(out global::Datablock datablock)
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
			datablock = (global::Datablock)this.refValue;
			return datablock;
		}

		// Token: 0x06003805 RID: 14341 RVA: 0x000C724C File Offset: 0x000C544C
		public bool GetDatablock<TDatablock>(out TDatablock datablock) where TDatablock : global::Datablock
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
			datablock = (((global::Datablock)this.refValue) as TDatablock);
			return datablock;
		}

		// Token: 0x06003806 RID: 14342 RVA: 0x000C72BC File Offset: 0x000C54BC
		public global::Datablock GetDatablock()
		{
			if ((this.type_f & 128) == 128)
			{
				this.Confirm();
			}
			if (this.type_f == 0)
			{
				return null;
			}
			return (global::Datablock)this.refValue;
		}

		// Token: 0x06003807 RID: 14343 RVA: 0x000C7300 File Offset: 0x000C5500
		public global::Datablock GetDatablock<TDatablock>() where TDatablock : global::Datablock
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

		// Token: 0x06003808 RID: 14344 RVA: 0x000C7350 File Offset: 0x000C5550
		public static implicit operator global::Datablock.Ident(string dbName)
		{
			return new global::Datablock.Ident(dbName);
		}

		// Token: 0x06003809 RID: 14345 RVA: 0x000C7358 File Offset: 0x000C5558
		public static implicit operator global::Datablock.Ident(int dbHash)
		{
			return new global::Datablock.Ident(dbHash);
		}

		// Token: 0x0600380A RID: 14346 RVA: 0x000C7360 File Offset: 0x000C5560
		public static implicit operator global::Datablock.Ident(uint dbHash)
		{
			return new global::Datablock.Ident((int)dbHash);
		}

		// Token: 0x0600380B RID: 14347 RVA: 0x000C7368 File Offset: 0x000C5568
		[Obsolete("Make sure your wanting to get a dbhash from a ushort here.")]
		public static implicit operator global::Datablock.Ident(ushort dbHash)
		{
			return new global::Datablock.Ident((int)dbHash);
		}

		// Token: 0x0600380C RID: 14348 RVA: 0x000C7370 File Offset: 0x000C5570
		[Obsolete("Make sure your wanting to get a dbhash from a short here.")]
		public static implicit operator global::Datablock.Ident(short dbHash)
		{
			return new global::Datablock.Ident((int)dbHash);
		}

		// Token: 0x0600380D RID: 14349 RVA: 0x000C7378 File Offset: 0x000C5578
		[Obsolete("Make sure your wanting to get a dbhash from a byte here.")]
		public static implicit operator global::Datablock.Ident(byte dbHash)
		{
			return new global::Datablock.Ident((int)dbHash);
		}

		// Token: 0x0600380E RID: 14350 RVA: 0x000C7380 File Offset: 0x000C5580
		[Obsolete("Make sure your wanting to get a dbhash from a sbyte here.")]
		public static implicit operator global::Datablock.Ident(sbyte dbHash)
		{
			return new global::Datablock.Ident((int)dbHash);
		}

		// Token: 0x0600380F RID: 14351 RVA: 0x000C738C File Offset: 0x000C558C
		public static explicit operator global::Datablock.Ident(ulong dbHash)
		{
			uint uniqueID = (uint)dbHash;
			return new global::Datablock.Ident((int)uniqueID);
		}

		// Token: 0x06003810 RID: 14352 RVA: 0x000C73A4 File Offset: 0x000C55A4
		public static explicit operator global::Datablock.Ident(long dbHash)
		{
			int uniqueID = (int)dbHash;
			return new global::Datablock.Ident(uniqueID);
		}

		// Token: 0x06003811 RID: 14353 RVA: 0x000C73BC File Offset: 0x000C55BC
		public static explicit operator global::Datablock.Ident(global::InventoryItem item)
		{
			return new global::Datablock.Ident(item);
		}

		// Token: 0x06003812 RID: 14354 RVA: 0x000C73C4 File Offset: 0x000C55C4
		public static explicit operator global::Datablock.Ident(global::Datablock db)
		{
			if (db)
			{
				return new global::Datablock.Ident(db, db.uniqueID, 1);
			}
			return default(global::Datablock.Ident);
		}

		// Token: 0x06003813 RID: 14355 RVA: 0x000C73F4 File Offset: 0x000C55F4
		public static global::Datablock.Ident operator +(global::Datablock.Ident ident)
		{
			if ((ident.type_f & 128) == 128)
			{
				ident.Confirm();
			}
			return ident;
		}

		// Token: 0x06003814 RID: 14356 RVA: 0x000C7418 File Offset: 0x000C5618
		public static bool operator ==(global::Datablock.Ident ident, global::Datablock.Ident other)
		{
			return ident.Equals(other);
		}

		// Token: 0x06003815 RID: 14357 RVA: 0x000C7424 File Offset: 0x000C5624
		public static bool operator !=(global::Datablock.Ident ident, global::Datablock.Ident other)
		{
			return !ident.Equals(other);
		}

		// Token: 0x06003816 RID: 14358 RVA: 0x000C7434 File Offset: 0x000C5634
		public static bool operator ==(global::Datablock.Ident ident, global::Datablock other)
		{
			return ident.Equals(other);
		}

		// Token: 0x06003817 RID: 14359 RVA: 0x000C7440 File Offset: 0x000C5640
		public static bool operator !=(global::Datablock.Ident ident, global::Datablock other)
		{
			return !ident.Equals(other);
		}

		// Token: 0x06003818 RID: 14360 RVA: 0x000C7450 File Offset: 0x000C5650
		public static bool operator ==(global::Datablock.Ident ident, string other)
		{
			if (string.IsNullOrEmpty(other))
			{
				return !ident.exists;
			}
			return ident.name == other;
		}

		// Token: 0x06003819 RID: 14361 RVA: 0x000C7480 File Offset: 0x000C5680
		public static bool operator !=(global::Datablock.Ident ident, string other)
		{
			if (string.IsNullOrEmpty(other))
			{
				return ident.exists;
			}
			return ident.name != other;
		}

		// Token: 0x0600381A RID: 14362 RVA: 0x000C74B0 File Offset: 0x000C56B0
		public static bool operator ==(global::Datablock.Ident ident, int hash)
		{
			return ident.uniqueIDIfExists == hash;
		}

		// Token: 0x0600381B RID: 14363 RVA: 0x000C74DC File Offset: 0x000C56DC
		public static bool operator !=(global::Datablock.Ident ident, int hash)
		{
			return ident.uniqueIDIfExists != hash;
		}

		// Token: 0x0600381C RID: 14364 RVA: 0x000C7508 File Offset: 0x000C5708
		public static bool operator ==(global::Datablock.Ident ident, uint hash)
		{
			return ident.uniqueID == (int)hash;
		}

		// Token: 0x0600381D RID: 14365 RVA: 0x000C7514 File Offset: 0x000C5714
		public static bool operator !=(global::Datablock.Ident ident, uint hash)
		{
			return ident.uniqueID != (int)hash;
		}

		// Token: 0x0600381E RID: 14366 RVA: 0x000C7524 File Offset: 0x000C5724
		public static bool operator ==(global::Datablock.Ident ident, ushort hash)
		{
			return ident.uniqueIDIfExists == (int)hash;
		}

		// Token: 0x0600381F RID: 14367 RVA: 0x000C7550 File Offset: 0x000C5750
		public static bool operator !=(global::Datablock.Ident ident, ushort hash)
		{
			return ident.uniqueIDIfExists != (int)hash;
		}

		// Token: 0x06003820 RID: 14368 RVA: 0x000C757C File Offset: 0x000C577C
		public static bool operator ==(global::Datablock.Ident ident, short hash)
		{
			return ident.uniqueID == (int)hash;
		}

		// Token: 0x06003821 RID: 14369 RVA: 0x000C7588 File Offset: 0x000C5788
		public static bool operator !=(global::Datablock.Ident ident, short hash)
		{
			return ident.uniqueID != (int)hash;
		}

		// Token: 0x06003822 RID: 14370 RVA: 0x000C7598 File Offset: 0x000C5798
		public static bool operator ==(global::Datablock.Ident ident, byte hash)
		{
			return ident.uniqueIDIfExists == (int)hash;
		}

		// Token: 0x06003823 RID: 14371 RVA: 0x000C75C4 File Offset: 0x000C57C4
		public static bool operator !=(global::Datablock.Ident ident, byte hash)
		{
			return ident.uniqueIDIfExists != (int)hash;
		}

		// Token: 0x06003824 RID: 14372 RVA: 0x000C75F0 File Offset: 0x000C57F0
		public static bool operator ==(global::Datablock.Ident ident, sbyte hash)
		{
			return ident.uniqueID == (int)hash;
		}

		// Token: 0x06003825 RID: 14373 RVA: 0x000C7600 File Offset: 0x000C5800
		public static bool operator !=(global::Datablock.Ident ident, sbyte hash)
		{
			return ident.uniqueID != (int)hash;
		}

		// Token: 0x06003826 RID: 14374 RVA: 0x000C7610 File Offset: 0x000C5810
		public static bool operator true(global::Datablock.Ident ident)
		{
			return ident.exists;
		}

		// Token: 0x06003827 RID: 14375 RVA: 0x000C761C File Offset: 0x000C581C
		public static bool operator false(global::Datablock.Ident ident)
		{
			return !ident.exists;
		}

		// Token: 0x04001BDF RID: 7135
		private const byte TYPE_NULL = 0;

		// Token: 0x04001BE0 RID: 7136
		private const byte TYPE_DATABLOCK = 1;

		// Token: 0x04001BE1 RID: 7137
		private const byte TYPE_INVENTORY_ITEM = 2;

		// Token: 0x04001BE2 RID: 7138
		private const byte TYPE_STRING = 3;

		// Token: 0x04001BE3 RID: 7139
		private const byte TYPE_HASH = 4;

		// Token: 0x04001BE4 RID: 7140
		private const int FLAG_UNCONFIRMED = 128;

		// Token: 0x04001BE5 RID: 7141
		private const int MASK_TYPE = 127;

		// Token: 0x04001BE6 RID: 7142
		private const byte TYPE_STRING_UNCONFIRMED = 131;

		// Token: 0x04001BE7 RID: 7143
		private const byte TYPE_HASH_UNCONFIRMED = 132;

		// Token: 0x04001BE8 RID: 7144
		private const byte TYPE_INVENTORY_ITEM_UNCONFIRMED = 130;

		// Token: 0x04001BE9 RID: 7145
		private const byte TYPE_DATABLOCK_UNCONFIRMED = 129;

		// Token: 0x04001BEA RID: 7146
		private readonly object refValue;

		// Token: 0x04001BEB RID: 7147
		private readonly int uid;

		// Token: 0x04001BEC RID: 7148
		private readonly byte type_f;
	}
}
