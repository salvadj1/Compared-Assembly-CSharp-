using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020005BF RID: 1471
[Serializable]
public class ArmorModelCollection<T> : IEnumerable, IEnumerable<T>, IEnumerable<KeyValuePair<global::ArmorModelSlot, T>>
{
	// Token: 0x06002F28 RID: 12072 RVA: 0x000B5F14 File Offset: 0x000B4114
	public ArmorModelCollection()
	{
	}

	// Token: 0x06002F29 RID: 12073 RVA: 0x000B5F1C File Offset: 0x000B411C
	public ArmorModelCollection(T defaultValue)
	{
		for (global::ArmorModelSlot armorModelSlot = global::ArmorModelSlot.Feet; armorModelSlot < (global::ArmorModelSlot)4; armorModelSlot += 1)
		{
			this[armorModelSlot] = defaultValue;
		}
	}

	// Token: 0x06002F2A RID: 12074 RVA: 0x000B5F4C File Offset: 0x000B414C
	public ArmorModelCollection(global::ArmorModelMemberMap<T> map) : this()
	{
		for (global::ArmorModelSlot armorModelSlot = global::ArmorModelSlot.Feet; armorModelSlot < (global::ArmorModelSlot)4; armorModelSlot += 1)
		{
			this[armorModelSlot] = map[armorModelSlot];
		}
	}

	// Token: 0x06002F2B RID: 12075 RVA: 0x000B5F80 File Offset: 0x000B4180
	IEnumerator<T> IEnumerable<T>.GetEnumerator()
	{
		return new global::ArmorModelCollection<T>.Enumerator(this);
	}

	// Token: 0x06002F2C RID: 12076 RVA: 0x000B5F90 File Offset: 0x000B4190
	IEnumerator<KeyValuePair<global::ArmorModelSlot, T>> IEnumerable<KeyValuePair<global::ArmorModelSlot, T>>.GetEnumerator()
	{
		return new global::ArmorModelCollection<T>.Enumerator(this);
	}

	// Token: 0x06002F2D RID: 12077 RVA: 0x000B5FA0 File Offset: 0x000B41A0
	IEnumerator IEnumerable.GetEnumerator()
	{
		return new global::ArmorModelCollection<T>.Enumerator(this);
	}

	// Token: 0x06002F2E RID: 12078 RVA: 0x000B5FB0 File Offset: 0x000B41B0
	public void Clear(T value)
	{
		for (global::ArmorModelSlot armorModelSlot = global::ArmorModelSlot.Feet; armorModelSlot < (global::ArmorModelSlot)4; armorModelSlot += 1)
		{
			this[armorModelSlot] = value;
		}
	}

	// Token: 0x06002F2F RID: 12079 RVA: 0x000B5FD8 File Offset: 0x000B41D8
	public void Clear()
	{
		for (global::ArmorModelSlot armorModelSlot = global::ArmorModelSlot.Feet; armorModelSlot < (global::ArmorModelSlot)4; armorModelSlot += 1)
		{
			this[armorModelSlot] = default(T);
		}
	}

	// Token: 0x06002F30 RID: 12080 RVA: 0x000B6008 File Offset: 0x000B4208
	public global::ArmorModelCollection<T>.Enumerator GetEnumerator()
	{
		return new global::ArmorModelCollection<T>.Enumerator(this);
	}

	// Token: 0x06002F31 RID: 12081 RVA: 0x000B6010 File Offset: 0x000B4210
	public int CopyTo(T[] array, int offset, int maxCount)
	{
		int num = (maxCount >= 4) ? 4 : maxCount;
		for (int i = 0; i < 4; i++)
		{
			array[offset++] = this[(global::ArmorModelSlot)i];
		}
		return offset;
	}

	// Token: 0x06002F32 RID: 12082 RVA: 0x000B6054 File Offset: 0x000B4254
	public void CopyFrom(T[] array, int offset)
	{
		for (int i = 0; i < 4; i++)
		{
			this[(global::ArmorModelSlot)i] = array[offset++];
		}
	}

	// Token: 0x06002F33 RID: 12083 RVA: 0x000B6088 File Offset: 0x000B4288
	public global::ArmorModelMemberMap<T> ToMemberMap()
	{
		global::ArmorModelMemberMap<T> result = default(global::ArmorModelMemberMap<T>);
		for (global::ArmorModelSlot armorModelSlot = global::ArmorModelSlot.Feet; armorModelSlot < (global::ArmorModelSlot)4; armorModelSlot += 1)
		{
			result[armorModelSlot] = this[armorModelSlot];
		}
		return result;
	}

	// Token: 0x17000A0D RID: 2573
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

	// Token: 0x04001999 RID: 6553
	public T feet;

	// Token: 0x0400199A RID: 6554
	public T legs;

	// Token: 0x0400199B RID: 6555
	public T torso;

	// Token: 0x0400199C RID: 6556
	public T head;

	// Token: 0x020005C0 RID: 1472
	public struct Enumerator : IDisposable, IEnumerator, IEnumerator<T>, IEnumerator<KeyValuePair<global::ArmorModelSlot, T>>
	{
		// Token: 0x06002F36 RID: 12086 RVA: 0x000B6174 File Offset: 0x000B4374
		internal Enumerator(global::ArmorModelCollection<T> collection)
		{
			this.collection = collection;
			this.index = -1;
		}

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x06002F37 RID: 12087 RVA: 0x000B6184 File Offset: 0x000B4384
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

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x06002F38 RID: 12088 RVA: 0x000B61D0 File Offset: 0x000B43D0
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x06002F39 RID: 12089 RVA: 0x000B61E0 File Offset: 0x000B43E0
		public T Current
		{
			get
			{
				return (this.index <= 0 || this.index >= 4) ? default(T) : this.collection[(global::ArmorModelSlot)this.index];
			}
		}

		// Token: 0x06002F3A RID: 12090 RVA: 0x000B6228 File Offset: 0x000B4428
		public bool MoveNext()
		{
			return ++this.index < 4;
		}

		// Token: 0x06002F3B RID: 12091 RVA: 0x000B624C File Offset: 0x000B444C
		public void Reset()
		{
			this.index = -1;
		}

		// Token: 0x06002F3C RID: 12092 RVA: 0x000B6258 File Offset: 0x000B4458
		public void Dispose()
		{
			this = default(global::ArmorModelCollection<T>.Enumerator);
		}

		// Token: 0x0400199D RID: 6557
		private global::ArmorModelCollection<T> collection;

		// Token: 0x0400199E RID: 6558
		private int index;
	}
}
