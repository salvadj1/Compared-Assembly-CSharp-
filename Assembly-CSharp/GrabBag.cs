using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200019A RID: 410
public sealed class GrabBag<T> : IEnumerable, IList<T>, ICollection<T>, IEnumerable<T>
{
	// Token: 0x06000C4F RID: 3151 RVA: 0x0002FA54 File Offset: 0x0002DC54
	public GrabBag(int capacity)
	{
		this._array = new T[capacity];
		this._length = 0;
	}

	// Token: 0x06000C50 RID: 3152 RVA: 0x0002FA70 File Offset: 0x0002DC70
	public GrabBag()
	{
		this._array = global::EmptyArray<T>.array;
		this._length = 0;
	}

	// Token: 0x06000C51 RID: 3153 RVA: 0x0002FA8C File Offset: 0x0002DC8C
	public GrabBag(T[] copy)
	{
		if (copy == null || (this._length = copy.Length) == 0)
		{
			this._length = 0;
			this._array = global::EmptyArray<T>.array;
		}
		else
		{
			this._length = copy.Length;
			this._array = new T[this._length];
			Array.Copy(copy, this._array, this._length);
		}
	}

	// Token: 0x06000C52 RID: 3154 RVA: 0x0002FAFC File Offset: 0x0002DCFC
	public GrabBag(global::GrabBag<T> copy)
	{
		if (copy == null || copy._length == 0)
		{
			this._length = 0;
			this._array = global::EmptyArray<T>.array;
		}
		else
		{
			this._length = copy._length;
			this._array = new T[this._length];
			Array.Copy(copy._array, this._array, this._length);
		}
	}

	// Token: 0x06000C53 RID: 3155 RVA: 0x0002FB6C File Offset: 0x0002DD6C
	public GrabBag(ICollection<T> collection)
	{
		this._array = collection.ToArray<T>();
		this._length = this._array.Length;
	}

	// Token: 0x06000C54 RID: 3156 RVA: 0x0002FB9C File Offset: 0x0002DD9C
	public GrabBag(IEnumerable<T> collection)
	{
		this._array = collection.ToArray<T>();
		this._length = this._array.Length;
	}

	// Token: 0x06000C55 RID: 3157 RVA: 0x0002FBCC File Offset: 0x0002DDCC
	void ICollection<T>.Add(T item)
	{
		int num = this.Grow(1);
		this._array[num] = item;
	}

	// Token: 0x06000C56 RID: 3158 RVA: 0x0002FBF0 File Offset: 0x0002DDF0
	IEnumerator<T> IEnumerable<T>.GetEnumerator()
	{
		IEnumerator<T> result;
		if (this._length == 0)
		{
			IEnumerator<T> emptyEnumerator = global::EmptyArray<T>.emptyEnumerator;
			result = emptyEnumerator;
		}
		else
		{
			result = new global::GrabBag<T>.KlassEnumerator(this);
		}
		return result;
	}

	// Token: 0x06000C57 RID: 3159 RVA: 0x0002FC1C File Offset: 0x0002DE1C
	IEnumerator IEnumerable.GetEnumerator()
	{
		IEnumerator result;
		if (this._length == 0)
		{
			IEnumerator<T> emptyEnumerator = global::EmptyArray<T>.emptyEnumerator;
			result = emptyEnumerator;
		}
		else
		{
			result = new global::GrabBag<T>.KlassEnumerator(this);
		}
		return result;
	}

	// Token: 0x17000350 RID: 848
	// (get) Token: 0x06000C58 RID: 3160 RVA: 0x0002FC48 File Offset: 0x0002DE48
	public int Count
	{
		get
		{
			return this._length;
		}
	}

	// Token: 0x17000351 RID: 849
	// (get) Token: 0x06000C59 RID: 3161 RVA: 0x0002FC50 File Offset: 0x0002DE50
	public int Capacity
	{
		get
		{
			return this._array.Length;
		}
	}

	// Token: 0x17000352 RID: 850
	// (get) Token: 0x06000C5A RID: 3162 RVA: 0x0002FC5C File Offset: 0x0002DE5C
	public T[] Buffer
	{
		get
		{
			return this._array;
		}
	}

	// Token: 0x17000353 RID: 851
	// (get) Token: 0x06000C5B RID: 3163 RVA: 0x0002FC64 File Offset: 0x0002DE64
	public ArraySegment<T> ArraySegment
	{
		get
		{
			return new ArraySegment<T>(this._array, 0, this._length);
		}
	}

	// Token: 0x06000C5C RID: 3164 RVA: 0x0002FC78 File Offset: 0x0002DE78
	public int Grow(int count)
	{
		int length = this._length;
		int num = this._length + count - this._array.Length;
		if (num > 0)
		{
			Array.Resize<T>(ref this._array, num / 2 * 4 + 1 + this._length);
		}
		this._length += count;
		return length;
	}

	// Token: 0x06000C5D RID: 3165 RVA: 0x0002FCD0 File Offset: 0x0002DED0
	public void Shrink()
	{
		if (this._length < this._array.Length)
		{
			Array.Resize<T>(ref this._array, this._length);
		}
	}

	// Token: 0x06000C5E RID: 3166 RVA: 0x0002FD04 File Offset: 0x0002DF04
	public int Add(T item)
	{
		int num = this.Grow(1);
		this._array[num] = item;
		return num;
	}

	// Token: 0x06000C5F RID: 3167 RVA: 0x0002FD28 File Offset: 0x0002DF28
	public void Insert(int index, T item)
	{
		int num = this.Grow(1);
		this._array[num] = this._array[index];
		this._array[index] = item;
	}

	// Token: 0x06000C60 RID: 3168 RVA: 0x0002FD64 File Offset: 0x0002DF64
	public bool Remove(T item)
	{
		int num = Array.IndexOf<T>(this._array, item, 0, this._length);
		if (num != -1)
		{
			this._array[num] = this._array[--this._length];
			this._array[this._length] = default(T);
			return true;
		}
		return false;
	}

	// Token: 0x06000C61 RID: 3169 RVA: 0x0002FDD4 File Offset: 0x0002DFD4
	public int RemoveAll(T item)
	{
		int num = 0;
		while (this.Remove(item))
		{
			num++;
		}
		return num;
	}

	// Token: 0x06000C62 RID: 3170 RVA: 0x0002FDFC File Offset: 0x0002DFFC
	public void RemoveAt(int index)
	{
		this._array[index] = this._array[--this._length];
		this._array[this._length] = default(T);
	}

	// Token: 0x06000C63 RID: 3171 RVA: 0x0002FE4C File Offset: 0x0002E04C
	public int IndexOf(T item)
	{
		return (this._length != 0) ? Array.IndexOf<T>(this._array, item, 0, this._length) : -1;
	}

	// Token: 0x06000C64 RID: 3172 RVA: 0x0002FE80 File Offset: 0x0002E080
	public int LastIndexOf(T item)
	{
		return (this._length != 0) ? Array.LastIndexOf<T>(this._array, item, 0, this._length) : -1;
	}

	// Token: 0x06000C65 RID: 3173 RVA: 0x0002FEB4 File Offset: 0x0002E0B4
	public int IndexOf(T item, int start)
	{
		return (this._length != 0) ? Array.IndexOf<T>(this._array, item, start, this._length - start) : -1;
	}

	// Token: 0x06000C66 RID: 3174 RVA: 0x0002FEE8 File Offset: 0x0002E0E8
	public int LastIndexOf(T item, int start)
	{
		return (this._length != 0) ? Array.LastIndexOf<T>(this._array, item, start, this._length - start) : -1;
	}

	// Token: 0x06000C67 RID: 3175 RVA: 0x0002FF1C File Offset: 0x0002E11C
	public int IndexOf(T item, int start, int count)
	{
		return (this._length != 0) ? Array.IndexOf<T>(this._array, item, start, count) : -1;
	}

	// Token: 0x06000C68 RID: 3176 RVA: 0x0002FF40 File Offset: 0x0002E140
	public int LastIndexOf(T item, int start, int count)
	{
		return (this._length != 0) ? Array.LastIndexOf<T>(this._array, item, start, count) : -1;
	}

	// Token: 0x06000C69 RID: 3177 RVA: 0x0002FF64 File Offset: 0x0002E164
	public bool Contains(T item)
	{
		return Array.IndexOf<T>(this._array, item) != -1;
	}

	// Token: 0x06000C6A RID: 3178 RVA: 0x0002FF78 File Offset: 0x0002E178
	public void Reverse()
	{
		if (this._length > 0)
		{
			Array.Reverse(this._array, 0, this._length);
		}
	}

	// Token: 0x06000C6B RID: 3179 RVA: 0x0002FF98 File Offset: 0x0002E198
	public void Reverse(int start, int count)
	{
		if (this._length > 0)
		{
			Array.Reverse(this._array, start, count);
		}
	}

	// Token: 0x06000C6C RID: 3180 RVA: 0x0002FFB4 File Offset: 0x0002E1B4
	public void Sort()
	{
		if (this._length != 0)
		{
			Array.Sort<T>(this._array, 0, this._length);
		}
	}

	// Token: 0x06000C6D RID: 3181 RVA: 0x0002FFD4 File Offset: 0x0002E1D4
	public void Sort(int start, int count)
	{
		if (this._length != 0)
		{
			Array.Sort<T>(this._array, start, count);
		}
	}

	// Token: 0x06000C6E RID: 3182 RVA: 0x0002FFF0 File Offset: 0x0002E1F0
	public void Sort(IComparer<T> comparer)
	{
		if (this._length != 0)
		{
			Array.Sort<T>(this._array, 0, this._length, comparer);
		}
	}

	// Token: 0x06000C6F RID: 3183 RVA: 0x00030010 File Offset: 0x0002E210
	public void Sort(IComparer<T> comparer, int start, int count)
	{
		if (this._length != 0)
		{
			Array.Sort<T>(this._array, start, count, comparer);
		}
	}

	// Token: 0x06000C70 RID: 3184 RVA: 0x0003002C File Offset: 0x0002E22C
	public void SortAsValue<K>(K[] keys)
	{
		Array.Sort<K, T>(keys, this._array, 0, this._length);
	}

	// Token: 0x06000C71 RID: 3185 RVA: 0x00030044 File Offset: 0x0002E244
	public void SortAsValue<K>(K[] keys, IComparer<K> comparer)
	{
		Array.Sort<K, T>(keys, this._array, 0, this._length, comparer);
	}

	// Token: 0x06000C72 RID: 3186 RVA: 0x0003005C File Offset: 0x0002E25C
	public void SortAsValue<K>(K[] keys, int start, int count)
	{
		Array.Sort<K, T>(keys, this._array, start, count);
	}

	// Token: 0x06000C73 RID: 3187 RVA: 0x0003006C File Offset: 0x0002E26C
	public void SortAsValue<K>(K[] keys, int start, int count, IComparer<K> comparer)
	{
		Array.Sort<K, T>(keys, this._array, start, count, comparer);
	}

	// Token: 0x06000C74 RID: 3188 RVA: 0x00030080 File Offset: 0x0002E280
	public void SortAsKey<V>(V[] values)
	{
		Array.Sort<T, V>(this._array, values, 0, this._length);
	}

	// Token: 0x06000C75 RID: 3189 RVA: 0x00030098 File Offset: 0x0002E298
	public void SortAsKey<V>(V[] values, IComparer<T> comparer)
	{
		Array.Sort<T, V>(this._array, values, 0, this._length, comparer);
	}

	// Token: 0x06000C76 RID: 3190 RVA: 0x000300B0 File Offset: 0x0002E2B0
	public void SortAsKey<V>(V[] values, int start, int count)
	{
		Array.Sort<T, V>(this._array, values, start, count);
	}

	// Token: 0x06000C77 RID: 3191 RVA: 0x000300C0 File Offset: 0x0002E2C0
	public void SortAsKey<V>(V[] values, int start, int count, IComparer<T> comparer)
	{
		Array.Sort<T, V>(this._array, values, start, count, comparer);
	}

	// Token: 0x06000C78 RID: 3192 RVA: 0x000300D4 File Offset: 0x0002E2D4
	public void Clear()
	{
		while (this._length > 0)
		{
			this._array[--this._length] = default(T);
		}
	}

	// Token: 0x17000354 RID: 852
	public T this[int i]
	{
		get
		{
			return this._array[i];
		}
		set
		{
			this._array[i] = value;
		}
	}

	// Token: 0x06000C7B RID: 3195 RVA: 0x00030138 File Offset: 0x0002E338
	public void CopyTo(T[] array, int arrayIndex)
	{
		for (int i = 0; i < this._length; i++)
		{
			array[arrayIndex++] = this._array[i];
		}
	}

	// Token: 0x17000355 RID: 853
	// (get) Token: 0x06000C7C RID: 3196 RVA: 0x00030174 File Offset: 0x0002E374
	public bool IsReadOnly
	{
		get
		{
			return false;
		}
	}

	// Token: 0x06000C7D RID: 3197 RVA: 0x00030178 File Offset: 0x0002E378
	public global::GrabBag<T>.Enumerator GetEnumerator()
	{
		global::GrabBag<T>.Enumerator result;
		result.array = this;
		result.nonNull = true;
		result.index = -1;
		return result;
	}

	// Token: 0x06000C7E RID: 3198 RVA: 0x000301A0 File Offset: 0x0002E3A0
	public T[] ToArray()
	{
		if (this._length == 0)
		{
			return global::EmptyArray<T>.array;
		}
		T[] array = new T[this._length];
		Array.Copy(this._array, array, this._length);
		return array;
	}

	// Token: 0x06000C7F RID: 3199 RVA: 0x000301E0 File Offset: 0x0002E3E0
	public override string ToString()
	{
		return string.Format(global::GrabBag<T>.StringGetter.Format, this.Count, this.Capacity);
	}

	// Token: 0x04000815 RID: 2069
	private T[] _array;

	// Token: 0x04000816 RID: 2070
	private int _length;

	// Token: 0x0200019B RID: 411
	public struct Enumerator : IDisposable, IEnumerator, IEnumerator<T>
	{
		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000C80 RID: 3200 RVA: 0x00030210 File Offset: 0x0002E410
		object IEnumerator.Current
		{
			get
			{
				return this.array._array[this.index];
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000C81 RID: 3201 RVA: 0x00030230 File Offset: 0x0002E430
		public T Current
		{
			get
			{
				return this.array._array[this.index];
			}
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x00030248 File Offset: 0x0002E448
		public bool MoveNext()
		{
			return this.nonNull && ++this.index < this.array._length;
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x00030284 File Offset: 0x0002E484
		public void Reset()
		{
			this.index = -1;
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x00030290 File Offset: 0x0002E490
		public void Dispose()
		{
			this = default(global::GrabBag<T>.Enumerator);
		}

		// Token: 0x04000817 RID: 2071
		public global::GrabBag<T> array;

		// Token: 0x04000818 RID: 2072
		public int index;

		// Token: 0x04000819 RID: 2073
		public bool nonNull;
	}

	// Token: 0x0200019C RID: 412
	private class KlassEnumerator : IDisposable, IEnumerator, IEnumerator<T>
	{
		// Token: 0x06000C85 RID: 3205 RVA: 0x000302AC File Offset: 0x0002E4AC
		public KlassEnumerator(global::GrabBag<T> array)
		{
			this.array = array;
			this.index = -1;
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x000302C4 File Offset: 0x0002E4C4
		object IEnumerator.Current
		{
			get
			{
				return this.array._array[this.index];
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000C87 RID: 3207 RVA: 0x000302E4 File Offset: 0x0002E4E4
		public T Current
		{
			get
			{
				return this.array._array[this.index];
			}
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x000302FC File Offset: 0x0002E4FC
		public bool MoveNext()
		{
			return ++this.index < this.array._length;
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x00030328 File Offset: 0x0002E528
		public void Reset()
		{
			this.index = -1;
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x00030334 File Offset: 0x0002E534
		public void Dispose()
		{
			this.array = null;
		}

		// Token: 0x0400081A RID: 2074
		public global::GrabBag<T> array;

		// Token: 0x0400081B RID: 2075
		public int index;
	}

	// Token: 0x0200019D RID: 413
	private static class StringGetter
	{
		// Token: 0x0400081C RID: 2076
		public static readonly string Format = "[DynArray<" + typeof(T).Name + ">: Count={0}, Capacity={1}]";
	}
}
