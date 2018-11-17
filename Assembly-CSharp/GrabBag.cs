using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200016E RID: 366
public sealed class GrabBag<T> : IEnumerable, IList<T>, ICollection<T>, IEnumerable<T>
{
	// Token: 0x06000B1F RID: 2847 RVA: 0x0002BB68 File Offset: 0x00029D68
	public GrabBag(int capacity)
	{
		this._array = new T[capacity];
		this._length = 0;
	}

	// Token: 0x06000B20 RID: 2848 RVA: 0x0002BB84 File Offset: 0x00029D84
	public GrabBag()
	{
		this._array = EmptyArray<T>.array;
		this._length = 0;
	}

	// Token: 0x06000B21 RID: 2849 RVA: 0x0002BBA0 File Offset: 0x00029DA0
	public GrabBag(T[] copy)
	{
		if (copy == null || (this._length = copy.Length) == 0)
		{
			this._length = 0;
			this._array = EmptyArray<T>.array;
		}
		else
		{
			this._length = copy.Length;
			this._array = new T[this._length];
			Array.Copy(copy, this._array, this._length);
		}
	}

	// Token: 0x06000B22 RID: 2850 RVA: 0x0002BC10 File Offset: 0x00029E10
	public GrabBag(GrabBag<T> copy)
	{
		if (copy == null || copy._length == 0)
		{
			this._length = 0;
			this._array = EmptyArray<T>.array;
		}
		else
		{
			this._length = copy._length;
			this._array = new T[this._length];
			Array.Copy(copy._array, this._array, this._length);
		}
	}

	// Token: 0x06000B23 RID: 2851 RVA: 0x0002BC80 File Offset: 0x00029E80
	public GrabBag(ICollection<T> collection)
	{
		this._array = collection.ToArray<T>();
		this._length = this._array.Length;
	}

	// Token: 0x06000B24 RID: 2852 RVA: 0x0002BCB0 File Offset: 0x00029EB0
	public GrabBag(IEnumerable<T> collection)
	{
		this._array = collection.ToArray<T>();
		this._length = this._array.Length;
	}

	// Token: 0x06000B25 RID: 2853 RVA: 0x0002BCE0 File Offset: 0x00029EE0
	void ICollection<T>.Add(T item)
	{
		int num = this.Grow(1);
		this._array[num] = item;
	}

	// Token: 0x06000B26 RID: 2854 RVA: 0x0002BD04 File Offset: 0x00029F04
	IEnumerator<T> IEnumerable<T>.GetEnumerator()
	{
		IEnumerator<T> result;
		if (this._length == 0)
		{
			IEnumerator<T> emptyEnumerator = EmptyArray<T>.emptyEnumerator;
			result = emptyEnumerator;
		}
		else
		{
			result = new GrabBag<T>.KlassEnumerator(this);
		}
		return result;
	}

	// Token: 0x06000B27 RID: 2855 RVA: 0x0002BD30 File Offset: 0x00029F30
	IEnumerator IEnumerable.GetEnumerator()
	{
		IEnumerator result;
		if (this._length == 0)
		{
			IEnumerator<T> emptyEnumerator = EmptyArray<T>.emptyEnumerator;
			result = emptyEnumerator;
		}
		else
		{
			result = new GrabBag<T>.KlassEnumerator(this);
		}
		return result;
	}

	// Token: 0x1700030C RID: 780
	// (get) Token: 0x06000B28 RID: 2856 RVA: 0x0002BD5C File Offset: 0x00029F5C
	public int Count
	{
		get
		{
			return this._length;
		}
	}

	// Token: 0x1700030D RID: 781
	// (get) Token: 0x06000B29 RID: 2857 RVA: 0x0002BD64 File Offset: 0x00029F64
	public int Capacity
	{
		get
		{
			return this._array.Length;
		}
	}

	// Token: 0x1700030E RID: 782
	// (get) Token: 0x06000B2A RID: 2858 RVA: 0x0002BD70 File Offset: 0x00029F70
	public T[] Buffer
	{
		get
		{
			return this._array;
		}
	}

	// Token: 0x1700030F RID: 783
	// (get) Token: 0x06000B2B RID: 2859 RVA: 0x0002BD78 File Offset: 0x00029F78
	public ArraySegment<T> ArraySegment
	{
		get
		{
			return new ArraySegment<T>(this._array, 0, this._length);
		}
	}

	// Token: 0x06000B2C RID: 2860 RVA: 0x0002BD8C File Offset: 0x00029F8C
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

	// Token: 0x06000B2D RID: 2861 RVA: 0x0002BDE4 File Offset: 0x00029FE4
	public void Shrink()
	{
		if (this._length < this._array.Length)
		{
			Array.Resize<T>(ref this._array, this._length);
		}
	}

	// Token: 0x06000B2E RID: 2862 RVA: 0x0002BE18 File Offset: 0x0002A018
	public int Add(T item)
	{
		int num = this.Grow(1);
		this._array[num] = item;
		return num;
	}

	// Token: 0x06000B2F RID: 2863 RVA: 0x0002BE3C File Offset: 0x0002A03C
	public void Insert(int index, T item)
	{
		int num = this.Grow(1);
		this._array[num] = this._array[index];
		this._array[index] = item;
	}

	// Token: 0x06000B30 RID: 2864 RVA: 0x0002BE78 File Offset: 0x0002A078
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

	// Token: 0x06000B31 RID: 2865 RVA: 0x0002BEE8 File Offset: 0x0002A0E8
	public int RemoveAll(T item)
	{
		int num = 0;
		while (this.Remove(item))
		{
			num++;
		}
		return num;
	}

	// Token: 0x06000B32 RID: 2866 RVA: 0x0002BF10 File Offset: 0x0002A110
	public void RemoveAt(int index)
	{
		this._array[index] = this._array[--this._length];
		this._array[this._length] = default(T);
	}

	// Token: 0x06000B33 RID: 2867 RVA: 0x0002BF60 File Offset: 0x0002A160
	public int IndexOf(T item)
	{
		return (this._length != 0) ? Array.IndexOf<T>(this._array, item, 0, this._length) : -1;
	}

	// Token: 0x06000B34 RID: 2868 RVA: 0x0002BF94 File Offset: 0x0002A194
	public int LastIndexOf(T item)
	{
		return (this._length != 0) ? Array.LastIndexOf<T>(this._array, item, 0, this._length) : -1;
	}

	// Token: 0x06000B35 RID: 2869 RVA: 0x0002BFC8 File Offset: 0x0002A1C8
	public int IndexOf(T item, int start)
	{
		return (this._length != 0) ? Array.IndexOf<T>(this._array, item, start, this._length - start) : -1;
	}

	// Token: 0x06000B36 RID: 2870 RVA: 0x0002BFFC File Offset: 0x0002A1FC
	public int LastIndexOf(T item, int start)
	{
		return (this._length != 0) ? Array.LastIndexOf<T>(this._array, item, start, this._length - start) : -1;
	}

	// Token: 0x06000B37 RID: 2871 RVA: 0x0002C030 File Offset: 0x0002A230
	public int IndexOf(T item, int start, int count)
	{
		return (this._length != 0) ? Array.IndexOf<T>(this._array, item, start, count) : -1;
	}

	// Token: 0x06000B38 RID: 2872 RVA: 0x0002C054 File Offset: 0x0002A254
	public int LastIndexOf(T item, int start, int count)
	{
		return (this._length != 0) ? Array.LastIndexOf<T>(this._array, item, start, count) : -1;
	}

	// Token: 0x06000B39 RID: 2873 RVA: 0x0002C078 File Offset: 0x0002A278
	public bool Contains(T item)
	{
		return Array.IndexOf<T>(this._array, item) != -1;
	}

	// Token: 0x06000B3A RID: 2874 RVA: 0x0002C08C File Offset: 0x0002A28C
	public void Reverse()
	{
		if (this._length > 0)
		{
			Array.Reverse(this._array, 0, this._length);
		}
	}

	// Token: 0x06000B3B RID: 2875 RVA: 0x0002C0AC File Offset: 0x0002A2AC
	public void Reverse(int start, int count)
	{
		if (this._length > 0)
		{
			Array.Reverse(this._array, start, count);
		}
	}

	// Token: 0x06000B3C RID: 2876 RVA: 0x0002C0C8 File Offset: 0x0002A2C8
	public void Sort()
	{
		if (this._length != 0)
		{
			Array.Sort<T>(this._array, 0, this._length);
		}
	}

	// Token: 0x06000B3D RID: 2877 RVA: 0x0002C0E8 File Offset: 0x0002A2E8
	public void Sort(int start, int count)
	{
		if (this._length != 0)
		{
			Array.Sort<T>(this._array, start, count);
		}
	}

	// Token: 0x06000B3E RID: 2878 RVA: 0x0002C104 File Offset: 0x0002A304
	public void Sort(IComparer<T> comparer)
	{
		if (this._length != 0)
		{
			Array.Sort<T>(this._array, 0, this._length, comparer);
		}
	}

	// Token: 0x06000B3F RID: 2879 RVA: 0x0002C124 File Offset: 0x0002A324
	public void Sort(IComparer<T> comparer, int start, int count)
	{
		if (this._length != 0)
		{
			Array.Sort<T>(this._array, start, count, comparer);
		}
	}

	// Token: 0x06000B40 RID: 2880 RVA: 0x0002C140 File Offset: 0x0002A340
	public void SortAsValue<K>(K[] keys)
	{
		Array.Sort<K, T>(keys, this._array, 0, this._length);
	}

	// Token: 0x06000B41 RID: 2881 RVA: 0x0002C158 File Offset: 0x0002A358
	public void SortAsValue<K>(K[] keys, IComparer<K> comparer)
	{
		Array.Sort<K, T>(keys, this._array, 0, this._length, comparer);
	}

	// Token: 0x06000B42 RID: 2882 RVA: 0x0002C170 File Offset: 0x0002A370
	public void SortAsValue<K>(K[] keys, int start, int count)
	{
		Array.Sort<K, T>(keys, this._array, start, count);
	}

	// Token: 0x06000B43 RID: 2883 RVA: 0x0002C180 File Offset: 0x0002A380
	public void SortAsValue<K>(K[] keys, int start, int count, IComparer<K> comparer)
	{
		Array.Sort<K, T>(keys, this._array, start, count, comparer);
	}

	// Token: 0x06000B44 RID: 2884 RVA: 0x0002C194 File Offset: 0x0002A394
	public void SortAsKey<V>(V[] values)
	{
		Array.Sort<T, V>(this._array, values, 0, this._length);
	}

	// Token: 0x06000B45 RID: 2885 RVA: 0x0002C1AC File Offset: 0x0002A3AC
	public void SortAsKey<V>(V[] values, IComparer<T> comparer)
	{
		Array.Sort<T, V>(this._array, values, 0, this._length, comparer);
	}

	// Token: 0x06000B46 RID: 2886 RVA: 0x0002C1C4 File Offset: 0x0002A3C4
	public void SortAsKey<V>(V[] values, int start, int count)
	{
		Array.Sort<T, V>(this._array, values, start, count);
	}

	// Token: 0x06000B47 RID: 2887 RVA: 0x0002C1D4 File Offset: 0x0002A3D4
	public void SortAsKey<V>(V[] values, int start, int count, IComparer<T> comparer)
	{
		Array.Sort<T, V>(this._array, values, start, count, comparer);
	}

	// Token: 0x06000B48 RID: 2888 RVA: 0x0002C1E8 File Offset: 0x0002A3E8
	public void Clear()
	{
		while (this._length > 0)
		{
			this._array[--this._length] = default(T);
		}
	}

	// Token: 0x17000310 RID: 784
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

	// Token: 0x06000B4B RID: 2891 RVA: 0x0002C24C File Offset: 0x0002A44C
	public void CopyTo(T[] array, int arrayIndex)
	{
		for (int i = 0; i < this._length; i++)
		{
			array[arrayIndex++] = this._array[i];
		}
	}

	// Token: 0x17000311 RID: 785
	// (get) Token: 0x06000B4C RID: 2892 RVA: 0x0002C288 File Offset: 0x0002A488
	public bool IsReadOnly
	{
		get
		{
			return false;
		}
	}

	// Token: 0x06000B4D RID: 2893 RVA: 0x0002C28C File Offset: 0x0002A48C
	public GrabBag<T>.Enumerator GetEnumerator()
	{
		GrabBag<T>.Enumerator result;
		result.array = this;
		result.nonNull = true;
		result.index = -1;
		return result;
	}

	// Token: 0x06000B4E RID: 2894 RVA: 0x0002C2B4 File Offset: 0x0002A4B4
	public T[] ToArray()
	{
		if (this._length == 0)
		{
			return EmptyArray<T>.array;
		}
		T[] array = new T[this._length];
		Array.Copy(this._array, array, this._length);
		return array;
	}

	// Token: 0x06000B4F RID: 2895 RVA: 0x0002C2F4 File Offset: 0x0002A4F4
	public override string ToString()
	{
		return string.Format(GrabBag<T>.StringGetter.Format, this.Count, this.Capacity);
	}

	// Token: 0x04000701 RID: 1793
	private T[] _array;

	// Token: 0x04000702 RID: 1794
	private int _length;

	// Token: 0x0200016F RID: 367
	public struct Enumerator : IDisposable, IEnumerator, IEnumerator<T>
	{
		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x0002C324 File Offset: 0x0002A524
		object IEnumerator.Current
		{
			get
			{
				return this.array._array[this.index];
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x0002C344 File Offset: 0x0002A544
		public T Current
		{
			get
			{
				return this.array._array[this.index];
			}
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x0002C35C File Offset: 0x0002A55C
		public bool MoveNext()
		{
			return this.nonNull && ++this.index < this.array._length;
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x0002C398 File Offset: 0x0002A598
		public void Reset()
		{
			this.index = -1;
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x0002C3A4 File Offset: 0x0002A5A4
		public void Dispose()
		{
			this = default(GrabBag<T>.Enumerator);
		}

		// Token: 0x04000703 RID: 1795
		public GrabBag<T> array;

		// Token: 0x04000704 RID: 1796
		public int index;

		// Token: 0x04000705 RID: 1797
		public bool nonNull;
	}

	// Token: 0x02000170 RID: 368
	private class KlassEnumerator : IDisposable, IEnumerator, IEnumerator<T>
	{
		// Token: 0x06000B55 RID: 2901 RVA: 0x0002C3C0 File Offset: 0x0002A5C0
		public KlassEnumerator(GrabBag<T> array)
		{
			this.array = array;
			this.index = -1;
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000B56 RID: 2902 RVA: 0x0002C3D8 File Offset: 0x0002A5D8
		object IEnumerator.Current
		{
			get
			{
				return this.array._array[this.index];
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x0002C3F8 File Offset: 0x0002A5F8
		public T Current
		{
			get
			{
				return this.array._array[this.index];
			}
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x0002C410 File Offset: 0x0002A610
		public bool MoveNext()
		{
			return ++this.index < this.array._length;
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x0002C43C File Offset: 0x0002A63C
		public void Reset()
		{
			this.index = -1;
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x0002C448 File Offset: 0x0002A648
		public void Dispose()
		{
			this.array = null;
		}

		// Token: 0x04000706 RID: 1798
		public GrabBag<T> array;

		// Token: 0x04000707 RID: 1799
		public int index;
	}

	// Token: 0x02000171 RID: 369
	private static class StringGetter
	{
		// Token: 0x04000708 RID: 1800
		public static readonly string Format = "[DynArray<" + typeof(T).Name + ">: Count={0}, Capacity={1}]";
	}
}
