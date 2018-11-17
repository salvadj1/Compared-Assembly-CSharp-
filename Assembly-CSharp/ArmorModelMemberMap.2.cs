using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020005C3 RID: 1475
[Serializable]
public struct ArmorModelMemberMap<T> : IEnumerable, IEnumerable<T>, IEnumerable<KeyValuePair<global::ArmorModelSlot, T>>
{
	// Token: 0x06002F53 RID: 12115 RVA: 0x000B6604 File Offset: 0x000B4804
	public ArmorModelMemberMap(T defaultValue)
	{
		this = default(global::ArmorModelMemberMap<T>);
		for (global::ArmorModelSlot armorModelSlot = global::ArmorModelSlot.Feet; armorModelSlot < (global::ArmorModelSlot)4; armorModelSlot += 1)
		{
			this[armorModelSlot] = defaultValue;
		}
	}

	// Token: 0x06002F54 RID: 12116 RVA: 0x000B663C File Offset: 0x000B483C
	IEnumerator<T> IEnumerable<T>.GetEnumerator()
	{
		return new global::ArmorModelMemberMap<T>.Enumerator(this);
	}

	// Token: 0x06002F55 RID: 12117 RVA: 0x000B6650 File Offset: 0x000B4850
	IEnumerator<KeyValuePair<global::ArmorModelSlot, T>> IEnumerable<KeyValuePair<global::ArmorModelSlot, T>>.GetEnumerator()
	{
		return new global::ArmorModelMemberMap<T>.Enumerator(this);
	}

	// Token: 0x06002F56 RID: 12118 RVA: 0x000B6664 File Offset: 0x000B4864
	IEnumerator IEnumerable.GetEnumerator()
	{
		return new global::ArmorModelMemberMap<T>.Enumerator(this);
	}

	// Token: 0x06002F57 RID: 12119 RVA: 0x000B6678 File Offset: 0x000B4878
	public void Clear(T value)
	{
		for (global::ArmorModelSlot armorModelSlot = global::ArmorModelSlot.Feet; armorModelSlot < (global::ArmorModelSlot)4; armorModelSlot += 1)
		{
			this[armorModelSlot] = value;
		}
	}

	// Token: 0x06002F58 RID: 12120 RVA: 0x000B66A0 File Offset: 0x000B48A0
	public void Clear()
	{
		for (global::ArmorModelSlot armorModelSlot = global::ArmorModelSlot.Feet; armorModelSlot < (global::ArmorModelSlot)4; armorModelSlot += 1)
		{
			this[armorModelSlot] = default(T);
		}
	}

	// Token: 0x06002F59 RID: 12121 RVA: 0x000B66D0 File Offset: 0x000B48D0
	public global::ArmorModelMemberMap<T>.Enumerator GetEnumerator()
	{
		return new global::ArmorModelMemberMap<T>.Enumerator(this);
	}

	// Token: 0x06002F5A RID: 12122 RVA: 0x000B66E0 File Offset: 0x000B48E0
	public int CopyTo(T[] array, int offset, int maxCount)
	{
		int num = (maxCount >= 4) ? 4 : maxCount;
		for (int i = 0; i < 4; i++)
		{
			array[offset++] = this[(global::ArmorModelSlot)i];
		}
		return offset;
	}

	// Token: 0x06002F5B RID: 12123 RVA: 0x000B6724 File Offset: 0x000B4924
	public void CopyFrom(T[] array, int offset)
	{
		for (int i = 0; i < 4; i++)
		{
			this[(global::ArmorModelSlot)i] = array[offset++];
		}
	}

	// Token: 0x17000A15 RID: 2581
	public T this[global::ArmorModelSlot slot]
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
				return default(T);
			}
		}
		set
		{
			switch (slot)
			{
			case global::ArmorModelSlot.Feet:
				this.feet = value;
				break;
			case global::ArmorModelSlot.Legs:
				this.legs = value;
				break;
			case global::ArmorModelSlot.Torso:
				this.torso = value;
				break;
			case global::ArmorModelSlot.Head:
				this.head = value;
				break;
			}
		}
	}

	// Token: 0x040019A5 RID: 6565
	public T feet;

	// Token: 0x040019A6 RID: 6566
	public T legs;

	// Token: 0x040019A7 RID: 6567
	public T torso;

	// Token: 0x040019A8 RID: 6568
	public T head;

	// Token: 0x020005C4 RID: 1476
	public struct Enumerator : IDisposable, IEnumerator, IEnumerator<T>, IEnumerator<KeyValuePair<global::ArmorModelSlot, T>>
	{
		// Token: 0x06002F5E RID: 12126 RVA: 0x000B6808 File Offset: 0x000B4A08
		internal Enumerator(global::ArmorModelMemberMap<T> collection)
		{
			this.collection = collection;
			this.index = -1;
		}

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x06002F5F RID: 12127 RVA: 0x000B6818 File Offset: 0x000B4A18
		KeyValuePair<global::ArmorModelSlot, T> IEnumerator<KeyValuePair<global::ArmorModelSlot, T>>.Current
		{
			get
			{
				if (this.index > 0 && this.index < 4)
				{
					return new KeyValuePair<global::ArmorModelSlot, T>((global::ArmorModelSlot)this.index, this.collection[(global::ArmorModelSlot)this.index]);
				}
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x06002F60 RID: 12128 RVA: 0x000B6864 File Offset: 0x000B4A64
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x06002F61 RID: 12129 RVA: 0x000B6874 File Offset: 0x000B4A74
		public T Current
		{
			get
			{
				return (this.index <= 0 || this.index >= 4) ? default(T) : this.collection[(global::ArmorModelSlot)this.index];
			}
		}

		// Token: 0x06002F62 RID: 12130 RVA: 0x000B68BC File Offset: 0x000B4ABC
		public bool MoveNext()
		{
			return ++this.index < 4;
		}

		// Token: 0x06002F63 RID: 12131 RVA: 0x000B68E0 File Offset: 0x000B4AE0
		public void Reset()
		{
			this.index = -1;
		}

		// Token: 0x06002F64 RID: 12132 RVA: 0x000B68EC File Offset: 0x000B4AEC
		public void Dispose()
		{
			this = default(global::ArmorModelMemberMap<T>.Enumerator);
		}

		// Token: 0x040019A9 RID: 6569
		private global::ArmorModelMemberMap<T> collection;

		// Token: 0x040019AA RID: 6570
		private int index;
	}
}
