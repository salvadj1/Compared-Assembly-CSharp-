using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000506 RID: 1286
[Serializable]
public struct ArmorModelMemberMap<T> : IEnumerable, IEnumerable<T>, IEnumerable<KeyValuePair<ArmorModelSlot, T>>
{
	// Token: 0x06002B93 RID: 11155 RVA: 0x000AE568 File Offset: 0x000AC768
	public ArmorModelMemberMap(T defaultValue)
	{
		this = default(ArmorModelMemberMap<T>);
		for (ArmorModelSlot armorModelSlot = ArmorModelSlot.Feet; armorModelSlot < (ArmorModelSlot)4; armorModelSlot += 1)
		{
			this[armorModelSlot] = defaultValue;
		}
	}

	// Token: 0x06002B94 RID: 11156 RVA: 0x000AE5A0 File Offset: 0x000AC7A0
	IEnumerator<T> IEnumerable<T>.GetEnumerator()
	{
		return new ArmorModelMemberMap<T>.Enumerator(this);
	}

	// Token: 0x06002B95 RID: 11157 RVA: 0x000AE5B4 File Offset: 0x000AC7B4
	IEnumerator<KeyValuePair<ArmorModelSlot, T>> IEnumerable<KeyValuePair<ArmorModelSlot, T>>.GetEnumerator()
	{
		return new ArmorModelMemberMap<T>.Enumerator(this);
	}

	// Token: 0x06002B96 RID: 11158 RVA: 0x000AE5C8 File Offset: 0x000AC7C8
	IEnumerator IEnumerable.GetEnumerator()
	{
		return new ArmorModelMemberMap<T>.Enumerator(this);
	}

	// Token: 0x06002B97 RID: 11159 RVA: 0x000AE5DC File Offset: 0x000AC7DC
	public void Clear(T value)
	{
		for (ArmorModelSlot armorModelSlot = ArmorModelSlot.Feet; armorModelSlot < (ArmorModelSlot)4; armorModelSlot += 1)
		{
			this[armorModelSlot] = value;
		}
	}

	// Token: 0x06002B98 RID: 11160 RVA: 0x000AE604 File Offset: 0x000AC804
	public void Clear()
	{
		for (ArmorModelSlot armorModelSlot = ArmorModelSlot.Feet; armorModelSlot < (ArmorModelSlot)4; armorModelSlot += 1)
		{
			this[armorModelSlot] = default(T);
		}
	}

	// Token: 0x06002B99 RID: 11161 RVA: 0x000AE634 File Offset: 0x000AC834
	public ArmorModelMemberMap<T>.Enumerator GetEnumerator()
	{
		return new ArmorModelMemberMap<T>.Enumerator(this);
	}

	// Token: 0x06002B9A RID: 11162 RVA: 0x000AE644 File Offset: 0x000AC844
	public int CopyTo(T[] array, int offset, int maxCount)
	{
		int num = (maxCount >= 4) ? 4 : maxCount;
		for (int i = 0; i < 4; i++)
		{
			array[offset++] = this[(ArmorModelSlot)i];
		}
		return offset;
	}

	// Token: 0x06002B9B RID: 11163 RVA: 0x000AE688 File Offset: 0x000AC888
	public void CopyFrom(T[] array, int offset)
	{
		for (int i = 0; i < 4; i++)
		{
			this[(ArmorModelSlot)i] = array[offset++];
		}
	}

	// Token: 0x170009A1 RID: 2465
	public T this[ArmorModelSlot slot]
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
				return default(T);
			}
		}
		set
		{
			switch (slot)
			{
			case ArmorModelSlot.Feet:
				this.feet = value;
				break;
			case ArmorModelSlot.Legs:
				this.legs = value;
				break;
			case ArmorModelSlot.Torso:
				this.torso = value;
				break;
			case ArmorModelSlot.Head:
				this.head = value;
				break;
			}
		}
	}

	// Token: 0x040017D9 RID: 6105
	public T feet;

	// Token: 0x040017DA RID: 6106
	public T legs;

	// Token: 0x040017DB RID: 6107
	public T torso;

	// Token: 0x040017DC RID: 6108
	public T head;

	// Token: 0x02000507 RID: 1287
	public struct Enumerator : IDisposable, IEnumerator, IEnumerator<T>, IEnumerator<KeyValuePair<ArmorModelSlot, T>>
	{
		// Token: 0x06002B9E RID: 11166 RVA: 0x000AE76C File Offset: 0x000AC96C
		internal Enumerator(ArmorModelMemberMap<T> collection)
		{
			this.collection = collection;
			this.index = -1;
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06002B9F RID: 11167 RVA: 0x000AE77C File Offset: 0x000AC97C
		KeyValuePair<ArmorModelSlot, T> IEnumerator<KeyValuePair<ArmorModelSlot, T>>.Current
		{
			get
			{
				if (this.index > 0 && this.index < 4)
				{
					return new KeyValuePair<ArmorModelSlot, T>((ArmorModelSlot)this.index, this.collection[(ArmorModelSlot)this.index]);
				}
				throw new InvalidOperationException();
			}
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x06002BA0 RID: 11168 RVA: 0x000AE7C8 File Offset: 0x000AC9C8
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x06002BA1 RID: 11169 RVA: 0x000AE7D8 File Offset: 0x000AC9D8
		public T Current
		{
			get
			{
				return (this.index <= 0 || this.index >= 4) ? default(T) : this.collection[(ArmorModelSlot)this.index];
			}
		}

		// Token: 0x06002BA2 RID: 11170 RVA: 0x000AE820 File Offset: 0x000ACA20
		public bool MoveNext()
		{
			return ++this.index < 4;
		}

		// Token: 0x06002BA3 RID: 11171 RVA: 0x000AE844 File Offset: 0x000ACA44
		public void Reset()
		{
			this.index = -1;
		}

		// Token: 0x06002BA4 RID: 11172 RVA: 0x000AE850 File Offset: 0x000ACA50
		public void Dispose()
		{
			this = default(ArmorModelMemberMap<T>.Enumerator);
		}

		// Token: 0x040017DD RID: 6109
		private ArmorModelMemberMap<T> collection;

		// Token: 0x040017DE RID: 6110
		private int index;
	}
}
