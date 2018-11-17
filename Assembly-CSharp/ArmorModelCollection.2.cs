using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000502 RID: 1282
[Serializable]
public class ArmorModelCollection<T> : IEnumerable, IEnumerable<T>, IEnumerable<KeyValuePair<ArmorModelSlot, T>>
{
	// Token: 0x06002B68 RID: 11112 RVA: 0x000ADE78 File Offset: 0x000AC078
	public ArmorModelCollection()
	{
	}

	// Token: 0x06002B69 RID: 11113 RVA: 0x000ADE80 File Offset: 0x000AC080
	public ArmorModelCollection(T defaultValue)
	{
		for (ArmorModelSlot armorModelSlot = ArmorModelSlot.Feet; armorModelSlot < (ArmorModelSlot)4; armorModelSlot += 1)
		{
			this[armorModelSlot] = defaultValue;
		}
	}

	// Token: 0x06002B6A RID: 11114 RVA: 0x000ADEB0 File Offset: 0x000AC0B0
	public ArmorModelCollection(ArmorModelMemberMap<T> map) : this()
	{
		for (ArmorModelSlot armorModelSlot = ArmorModelSlot.Feet; armorModelSlot < (ArmorModelSlot)4; armorModelSlot += 1)
		{
			this[armorModelSlot] = map[armorModelSlot];
		}
	}

	// Token: 0x06002B6B RID: 11115 RVA: 0x000ADEE4 File Offset: 0x000AC0E4
	IEnumerator<T> IEnumerable<T>.GetEnumerator()
	{
		return new ArmorModelCollection<T>.Enumerator(this);
	}

	// Token: 0x06002B6C RID: 11116 RVA: 0x000ADEF4 File Offset: 0x000AC0F4
	IEnumerator<KeyValuePair<ArmorModelSlot, T>> IEnumerable<KeyValuePair<ArmorModelSlot, T>>.GetEnumerator()
	{
		return new ArmorModelCollection<T>.Enumerator(this);
	}

	// Token: 0x06002B6D RID: 11117 RVA: 0x000ADF04 File Offset: 0x000AC104
	IEnumerator IEnumerable.GetEnumerator()
	{
		return new ArmorModelCollection<T>.Enumerator(this);
	}

	// Token: 0x06002B6E RID: 11118 RVA: 0x000ADF14 File Offset: 0x000AC114
	public void Clear(T value)
	{
		for (ArmorModelSlot armorModelSlot = ArmorModelSlot.Feet; armorModelSlot < (ArmorModelSlot)4; armorModelSlot += 1)
		{
			this[armorModelSlot] = value;
		}
	}

	// Token: 0x06002B6F RID: 11119 RVA: 0x000ADF3C File Offset: 0x000AC13C
	public void Clear()
	{
		for (ArmorModelSlot armorModelSlot = ArmorModelSlot.Feet; armorModelSlot < (ArmorModelSlot)4; armorModelSlot += 1)
		{
			this[armorModelSlot] = default(T);
		}
	}

	// Token: 0x06002B70 RID: 11120 RVA: 0x000ADF6C File Offset: 0x000AC16C
	public ArmorModelCollection<T>.Enumerator GetEnumerator()
	{
		return new ArmorModelCollection<T>.Enumerator(this);
	}

	// Token: 0x06002B71 RID: 11121 RVA: 0x000ADF74 File Offset: 0x000AC174
	public int CopyTo(T[] array, int offset, int maxCount)
	{
		int num = (maxCount >= 4) ? 4 : maxCount;
		for (int i = 0; i < 4; i++)
		{
			array[offset++] = this[(ArmorModelSlot)i];
		}
		return offset;
	}

	// Token: 0x06002B72 RID: 11122 RVA: 0x000ADFB8 File Offset: 0x000AC1B8
	public void CopyFrom(T[] array, int offset)
	{
		for (int i = 0; i < 4; i++)
		{
			this[(ArmorModelSlot)i] = array[offset++];
		}
	}

	// Token: 0x06002B73 RID: 11123 RVA: 0x000ADFEC File Offset: 0x000AC1EC
	public ArmorModelMemberMap<T> ToMemberMap()
	{
		ArmorModelMemberMap<T> result = default(ArmorModelMemberMap<T>);
		for (ArmorModelSlot armorModelSlot = ArmorModelSlot.Feet; armorModelSlot < (ArmorModelSlot)4; armorModelSlot += 1)
		{
			result[armorModelSlot] = this[armorModelSlot];
		}
		return result;
	}

	// Token: 0x17000999 RID: 2457
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

	// Token: 0x040017CD RID: 6093
	public T feet;

	// Token: 0x040017CE RID: 6094
	public T legs;

	// Token: 0x040017CF RID: 6095
	public T torso;

	// Token: 0x040017D0 RID: 6096
	public T head;

	// Token: 0x02000503 RID: 1283
	public struct Enumerator : IDisposable, IEnumerator, IEnumerator<T>, IEnumerator<KeyValuePair<ArmorModelSlot, T>>
	{
		// Token: 0x06002B76 RID: 11126 RVA: 0x000AE0D8 File Offset: 0x000AC2D8
		internal Enumerator(ArmorModelCollection<T> collection)
		{
			this.collection = collection;
			this.index = -1;
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x06002B77 RID: 11127 RVA: 0x000AE0E8 File Offset: 0x000AC2E8
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

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x06002B78 RID: 11128 RVA: 0x000AE134 File Offset: 0x000AC334
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x06002B79 RID: 11129 RVA: 0x000AE144 File Offset: 0x000AC344
		public T Current
		{
			get
			{
				return (this.index <= 0 || this.index >= 4) ? default(T) : this.collection[(ArmorModelSlot)this.index];
			}
		}

		// Token: 0x06002B7A RID: 11130 RVA: 0x000AE18C File Offset: 0x000AC38C
		public bool MoveNext()
		{
			return ++this.index < 4;
		}

		// Token: 0x06002B7B RID: 11131 RVA: 0x000AE1B0 File Offset: 0x000AC3B0
		public void Reset()
		{
			this.index = -1;
		}

		// Token: 0x06002B7C RID: 11132 RVA: 0x000AE1BC File Offset: 0x000AC3BC
		public void Dispose()
		{
			this = default(ArmorModelCollection<T>.Enumerator);
		}

		// Token: 0x040017D1 RID: 6097
		private ArmorModelCollection<T> collection;

		// Token: 0x040017D2 RID: 6098
		private int index;
	}
}
