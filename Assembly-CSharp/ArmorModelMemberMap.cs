using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000504 RID: 1284
[Serializable]
public struct ArmorModelMemberMap : IEnumerable, IEnumerable<ArmorModel>, IEnumerable<KeyValuePair<ArmorModelSlot, ArmorModel>>
{
	// Token: 0x06002B7D RID: 11133 RVA: 0x000AE1D8 File Offset: 0x000AC3D8
	public ArmorModelMemberMap(ArmorModelMemberMap<ArmorModel> map)
	{
		this = default(ArmorModelMemberMap);
		for (ArmorModelSlot armorModelSlot = ArmorModelSlot.Feet; armorModelSlot < (ArmorModelSlot)4; armorModelSlot += 1)
		{
			this[armorModelSlot] = map[armorModelSlot];
		}
	}

	// Token: 0x06002B7E RID: 11134 RVA: 0x000AE218 File Offset: 0x000AC418
	IEnumerator<ArmorModel> IEnumerable<ArmorModel>.GetEnumerator()
	{
		return new ArmorModelMemberMap.Enumerator(this);
	}

	// Token: 0x06002B7F RID: 11135 RVA: 0x000AE22C File Offset: 0x000AC42C
	IEnumerator<KeyValuePair<ArmorModelSlot, ArmorModel>> IEnumerable<KeyValuePair<ArmorModelSlot, ArmorModel>>.GetEnumerator()
	{
		return new ArmorModelMemberMap.Enumerator(this);
	}

	// Token: 0x06002B80 RID: 11136 RVA: 0x000AE240 File Offset: 0x000AC440
	IEnumerator IEnumerable.GetEnumerator()
	{
		return new ArmorModelMemberMap.Enumerator(this);
	}

	// Token: 0x06002B81 RID: 11137 RVA: 0x000AE254 File Offset: 0x000AC454
	public T GetArmorModel<T>() where T : ArmorModel, new()
	{
		return (T)((object)this[ArmorModelSlotUtility.GetArmorModelSlotForClass<T>()]);
	}

	// Token: 0x06002B82 RID: 11138 RVA: 0x000AE268 File Offset: 0x000AC468
	public void SetArmorModel<T>(T armorModel) where T : ArmorModel, new()
	{
		this[ArmorModelSlotUtility.GetArmorModelSlotForClass<T>()] = armorModel;
	}

	// Token: 0x06002B83 RID: 11139 RVA: 0x000AE27C File Offset: 0x000AC47C
	public ArmorModel GetArmorModel(ArmorModelSlot slot)
	{
		return this[slot];
	}

	// Token: 0x06002B84 RID: 11140 RVA: 0x000AE288 File Offset: 0x000AC488
	public ArmorModelMemberMap.Enumerator GetEnumerator()
	{
		return new ArmorModelMemberMap.Enumerator(this);
	}

	// Token: 0x06002B85 RID: 11141 RVA: 0x000AE298 File Offset: 0x000AC498
	public ArmorModelMemberMap<ArmorModel> ToGenericArmorModelMap()
	{
		ArmorModelMemberMap<ArmorModel> result = default(ArmorModelMemberMap<ArmorModel>);
		for (ArmorModelSlot armorModelSlot = ArmorModelSlot.Feet; armorModelSlot < (ArmorModelSlot)4; armorModelSlot += 1)
		{
			result[armorModelSlot] = this[armorModelSlot];
		}
		return result;
	}

	// Token: 0x06002B86 RID: 11142 RVA: 0x000AE2D4 File Offset: 0x000AC4D4
	public int CopyTo(ArmorModel[] array, int offset, int maxCount)
	{
		int num = (maxCount >= 4) ? 4 : maxCount;
		for (int i = 0; i < 4; i++)
		{
			array[offset++] = this[(ArmorModelSlot)i];
		}
		return offset;
	}

	// Token: 0x06002B87 RID: 11143 RVA: 0x000AE314 File Offset: 0x000AC514
	public void CopyFrom(ArmorModel[] array, int offset)
	{
		for (int i = 0; i < 4; i++)
		{
			this[(ArmorModelSlot)i] = array[offset++];
		}
	}

	// Token: 0x1700099D RID: 2461
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

	// Token: 0x06002B8A RID: 11146 RVA: 0x000AE400 File Offset: 0x000AC600
	public static explicit operator ArmorModelMemberMap(ArmorModelMemberMap<ArmorModel> generic)
	{
		ArmorModelMemberMap result = default(ArmorModelMemberMap);
		for (ArmorModelSlot armorModelSlot = ArmorModelSlot.Feet; armorModelSlot < (ArmorModelSlot)4; armorModelSlot += 1)
		{
			result[armorModelSlot] = generic[armorModelSlot];
		}
		return result;
	}

	// Token: 0x06002B8B RID: 11147 RVA: 0x000AE43C File Offset: 0x000AC63C
	public static implicit operator ArmorModelMemberMap<ArmorModel>(ArmorModelMemberMap self)
	{
		ArmorModelMemberMap<ArmorModel> result = default(ArmorModelMemberMap<ArmorModel>);
		for (ArmorModelSlot armorModelSlot = ArmorModelSlot.Feet; armorModelSlot < (ArmorModelSlot)4; armorModelSlot += 1)
		{
			result[armorModelSlot] = self[armorModelSlot];
		}
		return result;
	}

	// Token: 0x040017D3 RID: 6099
	public ArmorModelFeet feet;

	// Token: 0x040017D4 RID: 6100
	public ArmorModelLegs legs;

	// Token: 0x040017D5 RID: 6101
	public ArmorModelTorso torso;

	// Token: 0x040017D6 RID: 6102
	public ArmorModelHead head;

	// Token: 0x02000505 RID: 1285
	public struct Enumerator : IDisposable, IEnumerator, IEnumerator<ArmorModel>, IEnumerator<KeyValuePair<ArmorModelSlot, ArmorModel>>
	{
		// Token: 0x06002B8C RID: 11148 RVA: 0x000AE478 File Offset: 0x000AC678
		internal Enumerator(ArmorModelMemberMap collection)
		{
			this.collection = collection;
			this.index = -1;
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x06002B8D RID: 11149 RVA: 0x000AE488 File Offset: 0x000AC688
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

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x06002B8E RID: 11150 RVA: 0x000AE4D4 File Offset: 0x000AC6D4
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x06002B8F RID: 11151 RVA: 0x000AE4DC File Offset: 0x000AC6DC
		public ArmorModel Current
		{
			get
			{
				return (this.index <= 0 || this.index >= 4) ? null : this.collection[(ArmorModelSlot)this.index];
			}
		}

		// Token: 0x06002B90 RID: 11152 RVA: 0x000AE51C File Offset: 0x000AC71C
		public bool MoveNext()
		{
			return ++this.index < 4;
		}

		// Token: 0x06002B91 RID: 11153 RVA: 0x000AE540 File Offset: 0x000AC740
		public void Reset()
		{
			this.index = -1;
		}

		// Token: 0x06002B92 RID: 11154 RVA: 0x000AE54C File Offset: 0x000AC74C
		public void Dispose()
		{
			this = default(ArmorModelMemberMap.Enumerator);
		}

		// Token: 0x040017D7 RID: 6103
		private ArmorModelMemberMap collection;

		// Token: 0x040017D8 RID: 6104
		private int index;
	}
}
