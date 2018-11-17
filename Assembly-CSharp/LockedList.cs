using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

// Token: 0x020001F3 RID: 499
[DebuggerDisplay("Count = {Count}")]
public sealed class LockedList<T> : IEnumerable, IList, ICollection, ICollection<T>, IList<T>, IEnumerable<T>, IEquatable<List<T>>
{
	// Token: 0x06000DBE RID: 3518 RVA: 0x0003587C File Offset: 0x00033A7C
	private LockedList()
	{
		this.list = new List<T>(0);
	}

	// Token: 0x06000DBF RID: 3519 RVA: 0x00035890 File Offset: 0x00033A90
	public LockedList(List<T> list)
	{
		if (object.ReferenceEquals(list, null))
		{
			throw new ArgumentNullException("list");
		}
		this.list = list;
	}

	// Token: 0x06000DC0 RID: 3520 RVA: 0x000358C4 File Offset: 0x00033AC4
	int IList<T>.IndexOf(T item)
	{
		return this.list.IndexOf(item);
	}

	// Token: 0x06000DC1 RID: 3521 RVA: 0x000358D4 File Offset: 0x00033AD4
	void IList<T>.Insert(int index, T item)
	{
		throw new NotSupportedException();
	}

	// Token: 0x06000DC2 RID: 3522 RVA: 0x000358DC File Offset: 0x00033ADC
	void IList<T>.RemoveAt(int index)
	{
		throw new NotSupportedException();
	}

	// Token: 0x17000369 RID: 873
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

	// Token: 0x06000DC5 RID: 3525 RVA: 0x000358FC File Offset: 0x00033AFC
	void ICollection<T>.Add(T item)
	{
		throw new NotSupportedException();
	}

	// Token: 0x06000DC6 RID: 3526 RVA: 0x00035904 File Offset: 0x00033B04
	void ICollection<T>.Clear()
	{
		throw new NotSupportedException();
	}

	// Token: 0x06000DC7 RID: 3527 RVA: 0x0003590C File Offset: 0x00033B0C
	bool ICollection<T>.Contains(T item)
	{
		return this.ilist.Contains(item);
	}

	// Token: 0x06000DC8 RID: 3528 RVA: 0x0003591C File Offset: 0x00033B1C
	void ICollection<T>.CopyTo(T[] array, int arrayIndex)
	{
		this.ilist.CopyTo(array, arrayIndex);
	}

	// Token: 0x1700036A RID: 874
	// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x0003592C File Offset: 0x00033B2C
	int ICollection<T>.Count
	{
		get
		{
			return this.ilist.Count;
		}
	}

	// Token: 0x1700036B RID: 875
	// (get) Token: 0x06000DCA RID: 3530 RVA: 0x0003593C File Offset: 0x00033B3C
	bool ICollection<T>.IsReadOnly
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06000DCB RID: 3531 RVA: 0x00035940 File Offset: 0x00033B40
	bool ICollection<T>.Remove(T item)
	{
		throw new NotSupportedException();
	}

	// Token: 0x06000DCC RID: 3532 RVA: 0x00035948 File Offset: 0x00033B48
	IEnumerator<T> IEnumerable<T>.GetEnumerator()
	{
		return this.ilist.GetEnumerator();
	}

	// Token: 0x06000DCD RID: 3533 RVA: 0x00035958 File Offset: 0x00033B58
	IEnumerator IEnumerable.GetEnumerator()
	{
		return this.olist.GetEnumerator();
	}

	// Token: 0x06000DCE RID: 3534 RVA: 0x00035968 File Offset: 0x00033B68
	int IList.Add(object value)
	{
		throw new NotSupportedException();
	}

	// Token: 0x06000DCF RID: 3535 RVA: 0x00035970 File Offset: 0x00033B70
	void IList.Clear()
	{
		throw new NotSupportedException();
	}

	// Token: 0x06000DD0 RID: 3536 RVA: 0x00035978 File Offset: 0x00033B78
	bool IList.Contains(object value)
	{
		return this.olist.Contains(value);
	}

	// Token: 0x06000DD1 RID: 3537 RVA: 0x00035988 File Offset: 0x00033B88
	int IList.IndexOf(object value)
	{
		return this.olist.IndexOf(value);
	}

	// Token: 0x06000DD2 RID: 3538 RVA: 0x00035998 File Offset: 0x00033B98
	void IList.Insert(int index, object value)
	{
		throw new NotSupportedException();
	}

	// Token: 0x1700036C RID: 876
	// (get) Token: 0x06000DD3 RID: 3539 RVA: 0x000359A0 File Offset: 0x00033BA0
	bool IList.IsFixedSize
	{
		get
		{
			return false;
		}
	}

	// Token: 0x1700036D RID: 877
	// (get) Token: 0x06000DD4 RID: 3540 RVA: 0x000359A4 File Offset: 0x00033BA4
	bool IList.IsReadOnly
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06000DD5 RID: 3541 RVA: 0x000359A8 File Offset: 0x00033BA8
	void IList.Remove(object value)
	{
		throw new NotSupportedException();
	}

	// Token: 0x06000DD6 RID: 3542 RVA: 0x000359B0 File Offset: 0x00033BB0
	void IList.RemoveAt(int index)
	{
		throw new NotSupportedException();
	}

	// Token: 0x1700036E RID: 878
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

	// Token: 0x06000DD9 RID: 3545 RVA: 0x000359D0 File Offset: 0x00033BD0
	void ICollection.CopyTo(Array array, int index)
	{
		this.olist.CopyTo(array, index);
	}

	// Token: 0x1700036F RID: 879
	// (get) Token: 0x06000DDA RID: 3546 RVA: 0x000359E0 File Offset: 0x00033BE0
	int ICollection.Count
	{
		get
		{
			return this.olist.Count;
		}
	}

	// Token: 0x17000370 RID: 880
	// (get) Token: 0x06000DDB RID: 3547 RVA: 0x000359F0 File Offset: 0x00033BF0
	bool ICollection.IsSynchronized
	{
		get
		{
			return this.olist.IsSynchronized;
		}
	}

	// Token: 0x17000371 RID: 881
	// (get) Token: 0x06000DDC RID: 3548 RVA: 0x00035A00 File Offset: 0x00033C00
	object ICollection.SyncRoot
	{
		get
		{
			return this.olist.SyncRoot;
		}
	}

	// Token: 0x17000372 RID: 882
	// (get) Token: 0x06000DDD RID: 3549 RVA: 0x00035A10 File Offset: 0x00033C10
	public static global::LockedList<T> Empty
	{
		get
		{
			return global::LockedList<T>.EmptyInstance.List;
		}
	}

	// Token: 0x17000373 RID: 883
	// (get) Token: 0x06000DDE RID: 3550 RVA: 0x00035A18 File Offset: 0x00033C18
	private IList<T> ilist
	{
		get
		{
			return this.list;
		}
	}

	// Token: 0x17000374 RID: 884
	// (get) Token: 0x06000DDF RID: 3551 RVA: 0x00035A20 File Offset: 0x00033C20
	private IList olist
	{
		get
		{
			return this.list;
		}
	}

	// Token: 0x17000375 RID: 885
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

	// Token: 0x17000376 RID: 886
	// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x00035A40 File Offset: 0x00033C40
	public int Count
	{
		get
		{
			return this.list.Count;
		}
	}

	// Token: 0x06000DE3 RID: 3555 RVA: 0x00035A50 File Offset: 0x00033C50
	public List<T>.Enumerator GetEnumerator()
	{
		return this.list.GetEnumerator();
	}

	// Token: 0x06000DE4 RID: 3556 RVA: 0x00035A60 File Offset: 0x00033C60
	public bool Equals(List<T> list)
	{
		return this.list.Equals(list);
	}

	// Token: 0x06000DE5 RID: 3557 RVA: 0x00035A70 File Offset: 0x00033C70
	public override bool Equals(object obj)
	{
		return (!(obj is global::LockedList<T>)) ? (obj is List<T> && this.list.Equals(obj)) : this.list.Equals(((global::LockedList<T>)obj).list);
	}

	// Token: 0x06000DE6 RID: 3558 RVA: 0x00035AC0 File Offset: 0x00033CC0
	public override int GetHashCode()
	{
		return this.list.GetHashCode();
	}

	// Token: 0x06000DE7 RID: 3559 RVA: 0x00035AD0 File Offset: 0x00033CD0
	public override string ToString()
	{
		return this.list.ToString();
	}

	// Token: 0x06000DE8 RID: 3560 RVA: 0x00035AE0 File Offset: 0x00033CE0
	public T[] ToArray()
	{
		return this.list.ToArray();
	}

	// Token: 0x06000DE9 RID: 3561 RVA: 0x00035AF0 File Offset: 0x00033CF0
	public List<T> ToList()
	{
		return this.list.GetRange(0, this.list.Count);
	}

	// Token: 0x06000DEA RID: 3562 RVA: 0x00035B0C File Offset: 0x00033D0C
	public void CopyTo(T[] array)
	{
		this.list.CopyTo(array);
	}

	// Token: 0x06000DEB RID: 3563 RVA: 0x00035B1C File Offset: 0x00033D1C
	public void CopyTo(T[] array, int arrayIndex)
	{
		this.list.CopyTo(array, arrayIndex);
	}

	// Token: 0x06000DEC RID: 3564 RVA: 0x00035B2C File Offset: 0x00033D2C
	public void CopyTo(int index, T[] array, int arrayIndex, int count)
	{
		this.list.CopyTo(index, array, arrayIndex, count);
	}

	// Token: 0x06000DED RID: 3565 RVA: 0x00035B40 File Offset: 0x00033D40
	public List<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
	{
		return this.list.ConvertAll<TOutput>(converter);
	}

	// Token: 0x06000DEE RID: 3566 RVA: 0x00035B50 File Offset: 0x00033D50
	public int BinarySearch(T item)
	{
		return this.list.BinarySearch(item);
	}

	// Token: 0x06000DEF RID: 3567 RVA: 0x00035B60 File Offset: 0x00033D60
	public int BinarySearch(int index, int count, T item, IComparer<T> comparer)
	{
		return this.list.BinarySearch(index, count, item, comparer);
	}

	// Token: 0x06000DF0 RID: 3568 RVA: 0x00035B74 File Offset: 0x00033D74
	public int BinarySearch(T item, IComparer<T> comparer)
	{
		return this.list.BinarySearch(item, comparer);
	}

	// Token: 0x17000377 RID: 887
	// (get) Token: 0x06000DF1 RID: 3569 RVA: 0x00035B84 File Offset: 0x00033D84
	public int Capacity
	{
		get
		{
			return this.list.Capacity;
		}
	}

	// Token: 0x06000DF2 RID: 3570 RVA: 0x00035B94 File Offset: 0x00033D94
	public bool TrueForAll(Predicate<T> match)
	{
		return this.list.TrueForAll(match);
	}

	// Token: 0x06000DF3 RID: 3571 RVA: 0x00035BA4 File Offset: 0x00033DA4
	public List<T> FindAll(Predicate<T> match)
	{
		return this.list.FindAll(match);
	}

	// Token: 0x06000DF4 RID: 3572 RVA: 0x00035BB4 File Offset: 0x00033DB4
	public int FindIndex(Predicate<T> match)
	{
		return this.list.FindIndex(match);
	}

	// Token: 0x06000DF5 RID: 3573 RVA: 0x00035BC4 File Offset: 0x00033DC4
	public T Find(Predicate<T> match)
	{
		return this.list.Find(match);
	}

	// Token: 0x06000DF6 RID: 3574 RVA: 0x00035BD4 File Offset: 0x00033DD4
	public int FindLastIndex(Predicate<T> match)
	{
		return this.list.FindLastIndex(match);
	}

	// Token: 0x06000DF7 RID: 3575 RVA: 0x00035BE4 File Offset: 0x00033DE4
	public T FindLast(Predicate<T> match)
	{
		return this.list.FindLast(match);
	}

	// Token: 0x06000DF8 RID: 3576 RVA: 0x00035BF4 File Offset: 0x00033DF4
	public void ForEach(Action<T> action)
	{
		this.list.ForEach(action);
	}

	// Token: 0x06000DF9 RID: 3577 RVA: 0x00035C04 File Offset: 0x00033E04
	public List<T> GetRange(int index, int count)
	{
		return this.list.GetRange(index, count);
	}

	// Token: 0x06000DFA RID: 3578 RVA: 0x00035C14 File Offset: 0x00033E14
	public int LastIndexOf(T item)
	{
		return this.list.LastIndexOf(item);
	}

	// Token: 0x06000DFB RID: 3579 RVA: 0x00035C24 File Offset: 0x00033E24
	public int LastIndexOf(T item, int index)
	{
		return this.list.LastIndexOf(item, index);
	}

	// Token: 0x06000DFC RID: 3580 RVA: 0x00035C34 File Offset: 0x00033E34
	public int LastIndexOf(T item, int index, int count)
	{
		return this.list.LastIndexOf(item, index, count);
	}

	// Token: 0x06000DFD RID: 3581 RVA: 0x00035C44 File Offset: 0x00033E44
	public int IndexOf(T item)
	{
		return this.list.IndexOf(item);
	}

	// Token: 0x06000DFE RID: 3582 RVA: 0x00035C54 File Offset: 0x00033E54
	public int IndexOf(T item, int index)
	{
		return this.list.IndexOf(item, index);
	}

	// Token: 0x06000DFF RID: 3583 RVA: 0x00035C64 File Offset: 0x00033E64
	public int IndexOf(T item, int index, int count)
	{
		return this.list.IndexOf(item, index, count);
	}

	// Token: 0x06000E00 RID: 3584 RVA: 0x00035C74 File Offset: 0x00033E74
	public bool Contains(T item)
	{
		return this.list.Contains(item);
	}

	// Token: 0x040008AD RID: 2221
	private readonly List<T> list;

	// Token: 0x020001F4 RID: 500
	private static class EmptyInstance
	{
		// Token: 0x040008AE RID: 2222
		public static readonly global::LockedList<T> List = new global::LockedList<T>();
	}
}
