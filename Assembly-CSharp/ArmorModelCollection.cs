using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000500 RID: 1280
[Serializable]
public class ArmorModelCollection : IEnumerable, IEnumerable<ArmorModel>, IEnumerable<KeyValuePair<ArmorModelSlot, ArmorModel>>
{
	// Token: 0x06002B52 RID: 11090 RVA: 0x000ADB44 File Offset: 0x000ABD44
	public ArmorModelCollection()
	{
	}

	// Token: 0x06002B53 RID: 11091 RVA: 0x000ADB4C File Offset: 0x000ABD4C
	public ArmorModelCollection(ArmorModelMemberMap map) : this()
	{
		for (ArmorModelSlot armorModelSlot = ArmorModelSlot.Feet; armorModelSlot < (ArmorModelSlot)4; armorModelSlot += 1)
		{
			this[armorModelSlot] = map[armorModelSlot];
		}
	}

	// Token: 0x06002B54 RID: 11092 RVA: 0x000ADB80 File Offset: 0x000ABD80
	public ArmorModelCollection(ArmorModelMemberMap<ArmorModel> map)
	{
		for (ArmorModelSlot armorModelSlot = ArmorModelSlot.Feet; armorModelSlot < (ArmorModelSlot)4; armorModelSlot += 1)
		{
			this[armorModelSlot] = map[armorModelSlot];
		}
	}

	// Token: 0x06002B55 RID: 11093 RVA: 0x000ADBB4 File Offset: 0x000ABDB4
	IEnumerator<ArmorModel> IEnumerable<ArmorModel>.GetEnumerator()
	{
		return new ArmorModelCollection.Enumerator(this);
	}

	// Token: 0x06002B56 RID: 11094 RVA: 0x000ADBC4 File Offset: 0x000ABDC4
	IEnumerator<KeyValuePair<ArmorModelSlot, ArmorModel>> IEnumerable<KeyValuePair<ArmorModelSlot, ArmorModel>>.GetEnumerator()
	{
		return new ArmorModelCollection.Enumerator(this);
	}

	// Token: 0x06002B57 RID: 11095 RVA: 0x000ADBD4 File Offset: 0x000ABDD4
	IEnumerator IEnumerable.GetEnumerator()
	{
		return new ArmorModelCollection.Enumerator(this);
	}

	// Token: 0x06002B58 RID: 11096 RVA: 0x000ADBE4 File Offset: 0x000ABDE4
	public T GetArmorModel<T>() where T : ArmorModel, new()
	{
		return (T)((object)this[ArmorModelSlotUtility.GetArmorModelSlotForClass<T>()]);
	}

	// Token: 0x06002B59 RID: 11097 RVA: 0x000ADBF8 File Offset: 0x000ABDF8
	public void SetArmorModel<T>(T armorModel) where T : ArmorModel, new()
	{
		this[ArmorModelSlotUtility.GetArmorModelSlotForClass<T>()] = armorModel;
	}

	// Token: 0x06002B5A RID: 11098 RVA: 0x000ADC0C File Offset: 0x000ABE0C
	public ArmorModel GetArmorModel(ArmorModelSlot slot)
	{
		return this[slot];
	}

	// Token: 0x06002B5B RID: 11099 RVA: 0x000ADC18 File Offset: 0x000ABE18
	public ArmorModelCollection.Enumerator GetEnumerator()
	{
		return new ArmorModelCollection.Enumerator(this);
	}

	// Token: 0x06002B5C RID: 11100 RVA: 0x000ADC20 File Offset: 0x000ABE20
	public int CopyTo(ArmorModel[] array, int offset, int maxCount)
	{
		int num = (maxCount >= 4) ? 4 : maxCount;
		for (int i = 0; i < 4; i++)
		{
			array[offset++] = this[(ArmorModelSlot)i];
		}
		return offset;
	}

	// Token: 0x06002B5D RID: 11101 RVA: 0x000ADC60 File Offset: 0x000ABE60
	public void CopyFrom(ArmorModel[] array, int offset)
	{
		for (int i = 0; i < 4; i++)
		{
			this[(ArmorModelSlot)i] = array[offset++];
		}
	}

	// Token: 0x06002B5E RID: 11102 RVA: 0x000ADC90 File Offset: 0x000ABE90
	public ArmorModelMemberMap ToMemberMap()
	{
		ArmorModelMemberMap result = default(ArmorModelMemberMap);
		for (ArmorModelSlot armorModelSlot = ArmorModelSlot.Feet; armorModelSlot < (ArmorModelSlot)4; armorModelSlot += 1)
		{
			result[armorModelSlot] = this[armorModelSlot];
		}
		return result;
	}

	// Token: 0x17000995 RID: 2453
	public ArmorModel this[ArmorModelSlot slot]
	{
		get
		{
			switch (slot)
			{
			case ArmorModelSlot.Feet:
				return this.feet;
			case ArmorModelSlot.Legs:
				return this.legs;
			case ArmorModelSlot.Torso:
				return this.torso;
			case ArmorModelSlot.Head:
				return this.head;
			default:
				return null;
			}
		}
		set
		{
			switch (slot)
			{
			case ArmorModelSlot.Feet:
				this.feet = (ArmorModelFeet)value;
				break;
			case ArmorModelSlot.Legs:
				this.legs = (ArmorModelLegs)value;
				break;
			case ArmorModelSlot.Torso:
				this.torso = (ArmorModelTorso)value;
				break;
			case ArmorModelSlot.Head:
				this.head = (ArmorModelHead)value;
				break;
			}
		}
	}

	// Token: 0x040017C7 RID: 6087
	public ArmorModelFeet feet;

	// Token: 0x040017C8 RID: 6088
	public ArmorModelLegs legs;

	// Token: 0x040017C9 RID: 6089
	public ArmorModelTorso torso;

	// Token: 0x040017CA RID: 6090
	public ArmorModelHead head;

	// Token: 0x02000501 RID: 1281
	public struct Enumerator : IDisposable, IEnumerator, IEnumerator<ArmorModel>, IEnumerator<KeyValuePair<ArmorModelSlot, ArmorModel>>
	{
		// Token: 0x06002B61 RID: 11105 RVA: 0x000ADD88 File Offset: 0x000ABF88
		internal Enumerator(ArmorModelCollection collection)
		{
			this.collection = collection;
			this.index = -1;
		}

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x06002B62 RID: 11106 RVA: 0x000ADD98 File Offset: 0x000ABF98
		KeyValuePair<ArmorModelSlot, ArmorModel> IEnumerator<KeyValuePair<ArmorModelSlot, ArmorModel>>.Current
		{
			get
			{
				if (this.index > 0 && this.index < 4)
				{
					return new KeyValuePair<ArmorModelSlot, ArmorModel>((ArmorModelSlot)this.index, this.collection[(ArmorModelSlot)this.index]);
				}
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x06002B63 RID: 11107 RVA: 0x000ADDE4 File Offset: 0x000ABFE4
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x06002B64 RID: 11108 RVA: 0x000ADDEC File Offset: 0x000ABFEC
		public ArmorModel Current
		{
			get
			{
				return (this.index <= 0 || this.index >= 4) ? null : this.collection[(ArmorModelSlot)this.index];
			}
		}

		// Token: 0x06002B65 RID: 11109 RVA: 0x000ADE2C File Offset: 0x000AC02C
		public bool MoveNext()
		{
			return ++this.index < 4;
		}

		// Token: 0x06002B66 RID: 11110 RVA: 0x000ADE50 File Offset: 0x000AC050
		public void Reset()
		{
			this.index = -1;
		}

		// Token: 0x06002B67 RID: 11111 RVA: 0x000ADE5C File Offset: 0x000AC05C
		public void Dispose()
		{
			this = default(ArmorModelCollection.Enumerator);
		}

		// Token: 0x040017CB RID: 6091
		private ArmorModelCollection collection;

		// Token: 0x040017CC RID: 6092
		private int index;
	}
}
