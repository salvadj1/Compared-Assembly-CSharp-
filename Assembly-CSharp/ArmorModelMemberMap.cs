using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020005C1 RID: 1473
[Serializable]
public struct ArmorModelMemberMap : IEnumerable, IEnumerable<global::ArmorModel>, IEnumerable<KeyValuePair<global::ArmorModelSlot, global::ArmorModel>>
{
	// Token: 0x06002F3D RID: 12093 RVA: 0x000B6274 File Offset: 0x000B4474
	public ArmorModelMemberMap(global::ArmorModelMemberMap<global::ArmorModel> map)
	{
		this = default(global::ArmorModelMemberMap);
		for (global::ArmorModelSlot armorModelSlot = global::ArmorModelSlot.Feet; armorModelSlot < (global::ArmorModelSlot)4; armorModelSlot += 1)
		{
			this[armorModelSlot] = map[armorModelSlot];
		}
	}

	// Token: 0x06002F3E RID: 12094 RVA: 0x000B62B4 File Offset: 0x000B44B4
	IEnumerator<global::ArmorModel> IEnumerable<global::ArmorModel>.GetEnumerator()
	{
		return new global::ArmorModelMemberMap.Enumerator(this);
	}

	// Token: 0x06002F3F RID: 12095 RVA: 0x000B62C8 File Offset: 0x000B44C8
	IEnumerator<KeyValuePair<global::ArmorModelSlot, global::ArmorModel>> IEnumerable<KeyValuePair<global::ArmorModelSlot, global::ArmorModel>>.GetEnumerator()
	{
		return new global::ArmorModelMemberMap.Enumerator(this);
	}

	// Token: 0x06002F40 RID: 12096 RVA: 0x000B62DC File Offset: 0x000B44DC
	IEnumerator IEnumerable.GetEnumerator()
	{
		return new global::ArmorModelMemberMap.Enumerator(this);
	}

	// Token: 0x06002F41 RID: 12097 RVA: 0x000B62F0 File Offset: 0x000B44F0
	public T GetArmorModel<T>() where T : global::ArmorModel, new()
	{
		return (T)((object)this[global::ArmorModelSlotUtility.GetArmorModelSlotForClass<T>()]);
	}

	// Token: 0x06002F42 RID: 12098 RVA: 0x000B6304 File Offset: 0x000B4504
	public void SetArmorModel<T>(T armorModel) where T : global::ArmorModel, new()
	{
		this[global::ArmorModelSlotUtility.GetArmorModelSlotForClass<T>()] = armorModel;
	}

	// Token: 0x06002F43 RID: 12099 RVA: 0x000B6318 File Offset: 0x000B4518
	public global::ArmorModel GetArmorModel(global::ArmorModelSlot slot)
	{
		return this[slot];
	}

	// Token: 0x06002F44 RID: 12100 RVA: 0x000B6324 File Offset: 0x000B4524
	public global::ArmorModelMemberMap.Enumerator GetEnumerator()
	{
		return new global::ArmorModelMemberMap.Enumerator(this);
	}

	// Token: 0x06002F45 RID: 12101 RVA: 0x000B6334 File Offset: 0x000B4534
	public global::ArmorModelMemberMap<global::ArmorModel> ToGenericArmorModelMap()
	{
		global::ArmorModelMemberMap<global::ArmorModel> result = default(global::ArmorModelMemberMap<global::ArmorModel>);
		for (global::ArmorModelSlot armorModelSlot = global::ArmorModelSlot.Feet; armorModelSlot < (global::ArmorModelSlot)4; armorModelSlot += 1)
		{
			result[armorModelSlot] = this[armorModelSlot];
		}
		return result;
	}

	// Token: 0x06002F46 RID: 12102 RVA: 0x000B6370 File Offset: 0x000B4570
	public int CopyTo(global::ArmorModel[] array, int offset, int maxCount)
	{
		int num = (maxCount >= 4) ? 4 : maxCount;
		for (int i = 0; i < 4; i++)
		{
			array[offset++] = this[(global::ArmorModelSlot)i];
		}
		return offset;
	}

	// Token: 0x06002F47 RID: 12103 RVA: 0x000B63B0 File Offset: 0x000B45B0
	public void CopyFrom(global::ArmorModel[] array, int offset)
	{
		for (int i = 0; i < 4; i++)
		{
			this[(global::ArmorModelSlot)i] = array[offset++];
		}
	}

	// Token: 0x17000A11 RID: 2577
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

	// Token: 0x06002F4A RID: 12106 RVA: 0x000B649C File Offset: 0x000B469C
	public static explicit operator global::ArmorModelMemberMap(global::ArmorModelMemberMap<global::ArmorModel> generic)
	{
		global::ArmorModelMemberMap result = default(global::ArmorModelMemberMap);
		for (global::ArmorModelSlot armorModelSlot = global::ArmorModelSlot.Feet; armorModelSlot < (global::ArmorModelSlot)4; armorModelSlot += 1)
		{
			result[armorModelSlot] = generic[armorModelSlot];
		}
		return result;
	}

	// Token: 0x06002F4B RID: 12107 RVA: 0x000B64D8 File Offset: 0x000B46D8
	public static implicit operator global::ArmorModelMemberMap<global::ArmorModel>(global::ArmorModelMemberMap self)
	{
		global::ArmorModelMemberMap<global::ArmorModel> result = default(global::ArmorModelMemberMap<global::ArmorModel>);
		for (global::ArmorModelSlot armorModelSlot = global::ArmorModelSlot.Feet; armorModelSlot < (global::ArmorModelSlot)4; armorModelSlot += 1)
		{
			result[armorModelSlot] = self[armorModelSlot];
		}
		return result;
	}

	// Token: 0x0400199F RID: 6559
	public global::ArmorModelFeet feet;

	// Token: 0x040019A0 RID: 6560
	public global::ArmorModelLegs legs;

	// Token: 0x040019A1 RID: 6561
	public global::ArmorModelTorso torso;

	// Token: 0x040019A2 RID: 6562
	public global::ArmorModelHead head;

	// Token: 0x020005C2 RID: 1474
	public struct Enumerator : IDisposable, IEnumerator, IEnumerator<global::ArmorModel>, IEnumerator<KeyValuePair<global::ArmorModelSlot, global::ArmorModel>>
	{
		// Token: 0x06002F4C RID: 12108 RVA: 0x000B6514 File Offset: 0x000B4714
		internal Enumerator(global::ArmorModelMemberMap collection)
		{
			this.collection = collection;
			this.index = -1;
		}

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x06002F4D RID: 12109 RVA: 0x000B6524 File Offset: 0x000B4724
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

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x06002F4E RID: 12110 RVA: 0x000B6570 File Offset: 0x000B4770
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06002F4F RID: 12111 RVA: 0x000B6578 File Offset: 0x000B4778
		public global::ArmorModel Current
		{
			get
			{
				return (this.index <= 0 || this.index >= 4) ? null : this.collection[(global::ArmorModelSlot)this.index];
			}
		}

		// Token: 0x06002F50 RID: 12112 RVA: 0x000B65B8 File Offset: 0x000B47B8
		public bool MoveNext()
		{
			return ++this.index < 4;
		}

		// Token: 0x06002F51 RID: 12113 RVA: 0x000B65DC File Offset: 0x000B47DC
		public void Reset()
		{
			this.index = -1;
		}

		// Token: 0x06002F52 RID: 12114 RVA: 0x000B65E8 File Offset: 0x000B47E8
		public void Dispose()
		{
			this = default(global::ArmorModelMemberMap.Enumerator);
		}

		// Token: 0x040019A3 RID: 6563
		private global::ArmorModelMemberMap collection;

		// Token: 0x040019A4 RID: 6564
		private int index;
	}
}
