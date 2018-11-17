using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020005BD RID: 1469
[Serializable]
public class ArmorModelCollection : IEnumerable, IEnumerable<global::ArmorModel>, IEnumerable<KeyValuePair<global::ArmorModelSlot, global::ArmorModel>>
{
	// Token: 0x06002F12 RID: 12050 RVA: 0x000B5BE0 File Offset: 0x000B3DE0
	public ArmorModelCollection()
	{
	}

	// Token: 0x06002F13 RID: 12051 RVA: 0x000B5BE8 File Offset: 0x000B3DE8
	public ArmorModelCollection(global::ArmorModelMemberMap map) : this()
	{
		for (global::ArmorModelSlot armorModelSlot = global::ArmorModelSlot.Feet; armorModelSlot < (global::ArmorModelSlot)4; armorModelSlot += 1)
		{
			this[armorModelSlot] = map[armorModelSlot];
		}
	}

	// Token: 0x06002F14 RID: 12052 RVA: 0x000B5C1C File Offset: 0x000B3E1C
	public ArmorModelCollection(global::ArmorModelMemberMap<global::ArmorModel> map)
	{
		for (global::ArmorModelSlot armorModelSlot = global::ArmorModelSlot.Feet; armorModelSlot < (global::ArmorModelSlot)4; armorModelSlot += 1)
		{
			this[armorModelSlot] = map[armorModelSlot];
		}
	}

	// Token: 0x06002F15 RID: 12053 RVA: 0x000B5C50 File Offset: 0x000B3E50
	IEnumerator<global::ArmorModel> IEnumerable<global::ArmorModel>.GetEnumerator()
	{
		return new global::ArmorModelCollection.Enumerator(this);
	}

	// Token: 0x06002F16 RID: 12054 RVA: 0x000B5C60 File Offset: 0x000B3E60
	IEnumerator<KeyValuePair<global::ArmorModelSlot, global::ArmorModel>> IEnumerable<KeyValuePair<global::ArmorModelSlot, global::ArmorModel>>.GetEnumerator()
	{
		return new global::ArmorModelCollection.Enumerator(this);
	}

	// Token: 0x06002F17 RID: 12055 RVA: 0x000B5C70 File Offset: 0x000B3E70
	IEnumerator IEnumerable.GetEnumerator()
	{
		return new global::ArmorModelCollection.Enumerator(this);
	}

	// Token: 0x06002F18 RID: 12056 RVA: 0x000B5C80 File Offset: 0x000B3E80
	public T GetArmorModel<T>() where T : global::ArmorModel, new()
	{
		return (T)((object)this[global::ArmorModelSlotUtility.GetArmorModelSlotForClass<T>()]);
	}

	// Token: 0x06002F19 RID: 12057 RVA: 0x000B5C94 File Offset: 0x000B3E94
	public void SetArmorModel<T>(T armorModel) where T : global::ArmorModel, new()
	{
		this[global::ArmorModelSlotUtility.GetArmorModelSlotForClass<T>()] = armorModel;
	}

	// Token: 0x06002F1A RID: 12058 RVA: 0x000B5CA8 File Offset: 0x000B3EA8
	public global::ArmorModel GetArmorModel(global::ArmorModelSlot slot)
	{
		return this[slot];
	}

	// Token: 0x06002F1B RID: 12059 RVA: 0x000B5CB4 File Offset: 0x000B3EB4
	public global::ArmorModelCollection.Enumerator GetEnumerator()
	{
		return new global::ArmorModelCollection.Enumerator(this);
	}

	// Token: 0x06002F1C RID: 12060 RVA: 0x000B5CBC File Offset: 0x000B3EBC
	public int CopyTo(global::ArmorModel[] array, int offset, int maxCount)
	{
		int num = (maxCount >= 4) ? 4 : maxCount;
		for (int i = 0; i < 4; i++)
		{
			array[offset++] = this[(global::ArmorModelSlot)i];
		}
		return offset;
	}

	// Token: 0x06002F1D RID: 12061 RVA: 0x000B5CFC File Offset: 0x000B3EFC
	public void CopyFrom(global::ArmorModel[] array, int offset)
	{
		for (int i = 0; i < 4; i++)
		{
			this[(global::ArmorModelSlot)i] = array[offset++];
		}
	}

	// Token: 0x06002F1E RID: 12062 RVA: 0x000B5D2C File Offset: 0x000B3F2C
	public global::ArmorModelMemberMap ToMemberMap()
	{
		global::ArmorModelMemberMap result = default(global::ArmorModelMemberMap);
		for (global::ArmorModelSlot armorModelSlot = global::ArmorModelSlot.Feet; armorModelSlot < (global::ArmorModelSlot)4; armorModelSlot += 1)
		{
			result[armorModelSlot] = this[armorModelSlot];
		}
		return result;
	}

	// Token: 0x17000A09 RID: 2569
	public global::ArmorModel this[global::ArmorModelSlot slot]
	{
		get
		{
			switch (slot)
			{
			case global::ArmorModelSlot.Feet:
				return this.feet;
			case global::ArmorModelSlot.Legs:
				return this.legs;
			case global::ArmorModelSlot.Torso:
				return this.torso;
			case global::ArmorModelSlot.Head:
				return this.head;
			default:
				return null;
			}
		}
		set
		{
			switch (slot)
			{
			case global::ArmorModelSlot.Feet:
				this.feet = (global::ArmorModelFeet)value;
				break;
			case global::ArmorModelSlot.Legs:
				this.legs = (global::ArmorModelLegs)value;
				break;
			case global::ArmorModelSlot.Torso:
				this.torso = (global::ArmorModelTorso)value;
				break;
			case global::ArmorModelSlot.Head:
				this.head = (global::ArmorModelHead)value;
				break;
			}
		}
	}

	// Token: 0x04001993 RID: 6547
	public global::ArmorModelFeet feet;

	// Token: 0x04001994 RID: 6548
	public global::ArmorModelLegs legs;

	// Token: 0x04001995 RID: 6549
	public global::ArmorModelTorso torso;

	// Token: 0x04001996 RID: 6550
	public global::ArmorModelHead head;

	// Token: 0x020005BE RID: 1470
	public struct Enumerator : IDisposable, IEnumerator, IEnumerator<global::ArmorModel>, IEnumerator<KeyValuePair<global::ArmorModelSlot, global::ArmorModel>>
	{
		// Token: 0x06002F21 RID: 12065 RVA: 0x000B5E24 File Offset: 0x000B4024
		internal Enumerator(global::ArmorModelCollection collection)
		{
			this.collection = collection;
			this.index = -1;
		}

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06002F22 RID: 12066 RVA: 0x000B5E34 File Offset: 0x000B4034
		KeyValuePair<global::ArmorModelSlot, global::ArmorModel> IEnumerator<KeyValuePair<global::ArmorModelSlot, global::ArmorModel>>.Current
		{
			get
			{
				if (this.index > 0 && this.index < 4)
				{
					return new KeyValuePair<global::ArmorModelSlot, global::ArmorModel>((global::ArmorModelSlot)this.index, this.collection[(global::ArmorModelSlot)this.index]);
				}
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06002F23 RID: 12067 RVA: 0x000B5E80 File Offset: 0x000B4080
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06002F24 RID: 12068 RVA: 0x000B5E88 File Offset: 0x000B4088
		public global::ArmorModel Current
		{
			get
			{
				return (this.index <= 0 || this.index >= 4) ? null : this.collection[(global::ArmorModelSlot)this.index];
			}
		}

		// Token: 0x06002F25 RID: 12069 RVA: 0x000B5EC8 File Offset: 0x000B40C8
		public bool MoveNext()
		{
			return ++this.index < 4;
		}

		// Token: 0x06002F26 RID: 12070 RVA: 0x000B5EEC File Offset: 0x000B40EC
		public void Reset()
		{
			this.index = -1;
		}

		// Token: 0x06002F27 RID: 12071 RVA: 0x000B5EF8 File Offset: 0x000B40F8
		public void Dispose()
		{
			this = default(global::ArmorModelCollection.Enumerator);
		}

		// Token: 0x04001997 RID: 6551
		private global::ArmorModelCollection collection;

		// Token: 0x04001998 RID: 6552
		private int index;
	}
}
