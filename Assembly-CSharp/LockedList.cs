using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

// Token: 0x020001C3 RID: 451
[DebuggerDisplay("Count = {Count}")]
public sealed class LockedList<T> : IEnumerable, IList, ICollection, ICollection<T>, IList<T>, IEnumerable<T>, IEquatable<List<T>>
{
	// Token: 0x06000C7E RID: 3198 RVA: 0x00031990 File Offset: 0x0002FB90
	private LockedList()
	{
		this.list = new List<T>(0);
	}

	// Token: 0x06000C7F RID: 3199 RVA: 0x000319A4 File Offset: 0x0002FBA4
	public LockedList(List<T> list)
	{
		if (object.ReferenceEquals(list, null))
		{
			throw new ArgumentNullException("list");
		}
		this.list = list;
	}

	// Token: 0x06000C80 RID: 3200 RVA: 0x000319D8 File Offset: 0x0002FBD8
	int IList<T>.IndexOf(T item)
	{
		return this.list.IndexOf(item);
	}

	// Token: 0x06000C81 RID: 3201 RVA: 0x000319E8 File Offset: 0x0002FBE8
	void IList<T>.Insert(int index, T item)
	{
		throw new NotSupportedException();
	}

	// Token: 0x06000C82 RID: 3202 RVA: 0x000319F0 File Offset: 0x0002FBF0
	void IList<T>.RemoveAt(int index)
	{
		throw new NotSupportedException();
	}

	// Token: 0x17000325 RID: 805
	T IList<T>.this[int index]
	{
		get
		{
			return this.ilist[index];
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	// Token: 0x06000C85 RID: 3205 RVA: 0x00031A10 File Offset: 0x0002FC10
	void ICollection<T>.Add(T item)
	{
		throw new NotSupportedException();
	}

	// Token: 0x06000C86 RID: 3206 RVA: 0x00031A18 File Offset: 0x0002FC18
	void ICollection<T>.Clear()
	{
		throw new NotSupportedException();
	}

	// Token: 0x06000C87 RID: 3207 RVA: 0x00031A20 File Offset: 0x0002FC20
	bool ICollection<T>.Contains(T item)
	{
		return this.ilist.Contains(item);
	}

	// Token: 0x06000C88 RID: 3208 RVA: 0x00031A30 File Offset: 0x0002FC30
	void ICollection<T>.CopyTo(T[] array, int arrayIndex)
	{
		this.ilist.CopyTo(array, arrayIndex);
	}

	// Token: 0x17000326 RID: 806
	// (get) Token: 0x06000C89 RID: 3209 RVA: 0x00031A40 File Offset: 0x0002FC40
	int ICollection<T>.Count
	{
		get
		{
			return this.ilist.Count;
		}
	}

	// Token: 0x17000327 RID: 807
	// (get) Token: 0x06000C8A RID: 3210 RVA: 0x00031A50 File Offset: 0x0002FC50
	bool ICollection<T>.IsReadOnly
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06000C8B RID: 3211 RVA: 0x00031A54 File Offset: 0x0002FC54
	bool ICollection<T>.Remove(T item)
	{
		throw new NotSupportedException();
	}

	// Token: 0x06000C8C RID: 3212 RVA: 0x00031A5C File Offset: 0x0002FC5C
	IEnumerator<T> IEnumerable<T>.GetEnumerator()
	{
		return this.ilist.GetEnumerator();
	}

	// Token: 0x06000C8D RID: 3213 RVA: 0x00031A6C File Offset: 0x0002FC6C
	IEnumerator IEnumerable.GetEnumerator()
	{
		return this.olist.GetEnumerator();
	}

	// Token: 0x06000C8E RID: 3214 RVA: 0x00031A7C File Offset: 0x0002FC7C
	int IList.Add(object value)
	{
		throw new NotSupportedException();
	}

	// Token: 0x06000C8F RID: 3215 RVA: 0x00031A84 File Offset: 0x0002FC84
	void IList.Clear()
	{
		throw new NotSupportedException();
	}

	// Token: 0x06000C90 RID: 3216 RVA: 0x00031A8C File Offset: 0x0002FC8C
	bool IList.Contains(object value)
	{
		return this.olist.Contains(value);
	}

	// Token: 0x06000C91 RID: 3217 RVA: 0x00031A9C File Offset: 0x0002FC9C
	int IList.IndexOf(object value)
	{
		return this.olist.IndexOf(value);
	}

	// Token: 0x06000C92 RID: 3218 RVA: 0x00031AAC File Offset: 0x0002FCAC
	void IList.Insert(int index, object value)
	{
		throw new NotSupportedException();
	}

	// Token: 0x17000328 RID: 808
	// (get) Token: 0x06000C93 RID: 3219 RVA: 0x00031AB4 File Offset: 0x0002FCB4
	bool IList.IsFixedSize
	{
		get
		{
			return false;
		}
	}

	// Token: 0x17000329 RID: 809
	// (get) Token: 0x06000C94 RID: 3220 RVA: 0x00031AB8 File Offset: 0x0002FCB8
	bool IList.IsReadOnly
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06000C95 RID: 3221 RVA: 0x00031ABC File Offset: 0x0002FCBC
	void IList.Remove(object value)
	{
		throw new NotSupportedException();
	}

	// Token: 0x06000C96 RID: 3222 RVA: 0x00031AC4 File Offset: 0x0002FCC4
	void IList.RemoveAt(int index)
	{
		throw new NotSupportedException();
	}

	// Token: 0x1700032A RID: 810
	object IList.this[int index]
	{
		get
		{
			return this.olist[index];
		}
		set
		{
			throw new NotSupportedException();
		}
	}

	// Token: 0x06000C99 RID: 3225 RVA: 0x00031AE4 File Offset: 0x0002FCE4
	void ICollection.CopyTo(Array array, int index)
	{
		this.olist.CopyTo(array, index);
	}

	// Token: 0x1700032B RID: 811
	// (get) Token: 0x06000C9A RID: 3226 RVA: 0x00031AF4 File Offset: 0x0002FCF4
	int ICollection.Count
	{
		get
		{
			return this.olist.Count;
		}
	}

	// Token: 0x1700032C RID: 812
	// (get) Token: 0x06000C9B RID: 3227 RVA: 0x00031B04 File Offset: 0x0002FD04
	bool ICollection.IsSynchronized
	{
		get
		{
			return this.olist.IsSynchronized;
		}
	}

	// Token: 0x1700032D RID: 813
	// (get) Token: 0x06000C9C RID: 3228 RVA: 0x00031B14 File Offset: 0x0002FD14
	object ICollection.SyncRoot
	{
		get
		{
			return this.olist.SyncRoot;
		}
	}

	// Token: 0x1700032E RID: 814
	// (get) Token: 0x06000C9D RID: 3229 RVA: 0x00031B24 File Offset: 0x0002FD24
	public static LockedList<T> Empty
	{
		get
		{
			return LockedList<T>.EmptyInstance.List;
		}
	}

	// Token: 0x1700032F RID: 815
	// (get) Token: 0x06000C9E RID: 3230 RVA: 0x00031B2C File Offset: 0x0002FD2C
	private IList<T> ilist
	{
		get
		{
			return this.list;
		}
	}

	// Token: 0x17000330 RID: 816
	// (get) Token: 0x06000C9F RID: 3231 RVA: 0x00031B34 File Offset: 0x0002FD34
	private IList olist
	{
		get
		{
			return this.list;
		}
	}

	// Token: 0x17000331 RID: 817
	public T this[int index]
	{
		get
		{
			return this.list[index];
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	// Token: 0x17000332 RID: 818
	// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x00031B54 File Offset: 0x0002FD54
	public int Count
	{
		get
		{
			return this.list.Count;
		}
	}

	// Token: 0x06000CA3 RID: 3235 RVA: 0x00031B64 File Offset: 0x0002FD64
	public List<T>.Enumerator GetEnumerator()
	{
		return this.list.GetEnumerator();
	}

	// Token: 0x06000CA4 RID: 3236 RVA: 0x00031B74 File Offset: 0x0002FD74
	public bool Equals(List<T> list)
	{
		return this.list.Equals(list);
	}

	// Token: 0x06000CA5 RID: 3237 RVA: 0x00031B84 File Offset: 0x0002FD84
	public override bool Equals(object obj)
	{
		return (!(obj is LockedList<T>)) ? (obj is List<T> && this.list.Equals(obj)) : this.list.Equals(((LockedList<T>)obj).list);
	}

	// Token: 0x06000CA6 RID: 3238 RVA: 0x00031BD4 File Offset: 0x0002FDD4
	public override int GetHashCode()
	{
		return this.list.GetHashCode();
	}

	// Token: 0x06000CA7 RID: 3239 RVA: 0x00031BE4 File Offset: 0x0002FDE4
	public override string ToString()
	{
		return this.list.ToString();
	}

	// Token: 0x06000CA8 RID: 3240 RVA: 0x00031BF4 File Offset: 0x0002FDF4
	public T[] ToArray()
	{
		return this.list.ToArray();
	}

	// Token: 0x06000CA9 RID: 3241 RVA: 0x00031C04 File Offset: 0x0002FE04
	public List<T> ToList()
	{
		return this.list.GetRange(0, this.list.Count);
	}

	// Token: 0x06000CAA RID: 3242 RVA: 0x00031C20 File Offset: 0x0002FE20
	public void CopyTo(T[] array)
	{
		this.list.CopyTo(array);
	}

	// Token: 0x06000CAB RID: 3243 RVA: 0x00031C30 File Offset: 0x0002FE30
	public void CopyTo(T[] array, int arrayIndex)
	{
		this.list.CopyTo(array, arrayIndex);
	}

	// Token: 0x06000CAC RID: 3244 RVA: 0x00031C40 File Offset: 0x0002FE40
	public void CopyTo(int index, T[] array, int arrayIndex, int count)
	{
		this.list.CopyTo(index, array, arrayIndex, count);
	}

	// Token: 0x06000CAD RID: 3245 RVA: 0x00031C54 File Offset: 0x0002FE54
	public List<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
	{
		return this.list.ConvertAll<TOutput>(converter);
	}

	// Token: 0x06000CAE RID: 3246 RVA: 0x00031C64 File Offset: 0x0002FE64
	public int BinarySearch(T item)
	{
		return this.list.BinarySearch(item);
	}

	// Token: 0x06000CAF RID: 3247 RVA: 0x00031C74 File Offset: 0x0002FE74
	public int BinarySearch(int index, int count, T item, IComparer<T> comparer)
	{
		return this.list.BinarySearch(index, count, item, comparer);
	}

	// Token: 0x06000CB0 RID: 3248 RVA: 0x00031C88 File Offset: 0x0002FE88
	public int BinarySearch(T item, IComparer<T> comparer)
	{
		return this.list.BinarySearch(item, comparer);
	}

	// Token: 0x17000333 RID: 819
	// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x00031C98 File Offset: 0x0002FE98
	public int Capacity
	{
		get
		{
			return this.list.Capacity;
		}
	}

	// Token: 0x06000CB2 RID: 3250 RVA: 0x00031CA8 File Offset: 0x0002FEA8
	public bool TrueForAll(Predicate<T> match)
	{
		return this.list.TrueForAll(match);
	}

	// Token: 0x06000CB3 RID: 3251 RVA: 0x00031CB8 File Offset: 0x0002FEB8
	public List<T> FindAll(Predicate<T> match)
	{
		return this.list.FindAll(match);
	}

	// Token: 0x06000CB4 RID: 3252 RVA: 0x00031CC8 File Offset: 0x0002FEC8
	public int FindIndex(Predicate<T> match)
	{
		return this.list.FindIndex(match);
	}

	// Token: 0x06000CB5 RID: 3253 RVA: 0x00031CD8 File Offset: 0x0002FED8
	public T Find(Predicate<T> match)
	{
		return this.list.Find(match);
	}

	// Token: 0x06000CB6 RID: 3254 RVA: 0x00031CE8 File Offset: 0x0002FEE8
	public int FindLastIndex(Predicate<T> match)
	{
		return this.list.FindLastIndex(match);
	}

	// Token: 0x06000CB7 RID: 3255 RVA: 0x00031CF8 File Offset: 0x0002FEF8
	public T FindLast(Predicate<T> match)
	{
		return this.list.FindLast(match);
	}

	// Token: 0x06000CB8 RID: 3256 RVA: 0x00031D08 File Offset: 0x0002FF08
	public void ForEach(Action<T> action)
	{
		this.list.ForEach(action);
	}

	// Token: 0x06000CB9 RID: 3257 RVA: 0x00031D18 File Offset: 0x0002FF18
	public List<T> GetRange(int index, int count)
	{
		return this.list.GetRange(index, count);
	}

	// Token: 0x06000CBA RID: 3258 RVA: 0x00031D28 File Offset: 0x0002FF28
	public int LastIndexOf(T item)
	{
		return this.list.LastIndexOf(item);
	}

	// Token: 0x06000CBB RID: 3259 RVA: 0x00031D38 File Offset: 0x0002FF38
	public int LastIndexOf(T item, int index)
	{
		return this.list.LastIndexOf(item, index);
	}

	// Token: 0x06000CBC RID: 3260 RVA: 0x00031D48 File Offset: 0x0002FF48
	public int LastIndexOf(T item, int index, int count)
	{
		return this.list.LastIndexOf(item, index, count);
	}

	// Token: 0x06000CBD RID: 3261 RVA: 0x00031D58 File Offset: 0x0002FF58
	public int IndexOf(T item)
	{
		return this.list.IndexOf(item);
	}

	// Token: 0x06000CBE RID: 3262 RVA: 0x00031D68 File Offset: 0x0002FF68
	public int IndexOf(T item, int index)
	{
		return this.list.IndexOf(item, index);
	}

	// Token: 0x06000CBF RID: 3263 RVA: 0x00031D78 File Offset: 0x0002FF78
	public int IndexOf(T item, int index, int count)
	{
		return this.list.IndexOf(item, index, count);
	}

	// Token: 0x06000CC0 RID: 3264 RVA: 0x00031D88 File Offset: 0x0002FF88
	public bool Contains(T item)
	{
		return this.list.Contains(item);
	}

	// Token: 0x04000799 RID: 1945
	private readonly List<T> list;

	// Token: 0x020001C4 RID: 452
	private static class EmptyInstance
	{
		// Token: 0x0400079A RID: 1946
		public static readonly LockedList<T> List = new LockedList<T>();
	}
}
