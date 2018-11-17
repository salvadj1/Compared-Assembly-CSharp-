using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020006DC RID: 1756
public class dfList<T> : IDisposable, IEnumerable, ICollection<T>, IList<T>, IEnumerable<T>
{
	// Token: 0x06003E46 RID: 15942 RVA: 0x000EBFE0 File Offset: 0x000EA1E0
	internal dfList()
	{
	}

	// Token: 0x06003E47 RID: 15943 RVA: 0x000EBFF8 File Offset: 0x000EA1F8
	internal dfList(IList<T> listToClone)
	{
		this.AddRange(listToClone);
	}

	// Token: 0x06003E48 RID: 15944 RVA: 0x000EC018 File Offset: 0x000EA218
	internal dfList(int capacity)
	{
		this.EnsureCapacity(capacity);
	}

	// Token: 0x06003E4A RID: 15946 RVA: 0x000EC044 File Offset: 0x000EA244
	IEnumerator IEnumerable.GetEnumerator()
	{
		return dfList<T>.PooledEnumerator.Obtain(this, null);
	}

	// Token: 0x06003E4B RID: 15947 RVA: 0x000EC050 File Offset: 0x000EA250
	public static dfList<T> Obtain()
	{
		return (dfList<T>.pool.Count <= 0) ? new dfList<T>() : ((dfList<T>)dfList<T>.pool.Dequeue());
	}

	// Token: 0x06003E4C RID: 15948 RVA: 0x000EC07C File Offset: 0x000EA27C
	internal static dfList<T> Obtain(int capacity)
	{
		dfList<T> dfList = dfList<T>.Obtain();
		dfList.EnsureCapacity(capacity);
		return dfList;
	}

	// Token: 0x17000C36 RID: 3126
	// (get) Token: 0x06003E4D RID: 15949 RVA: 0x000EC098 File Offset: 0x000EA298
	public int Count
	{
		get
		{
			return this.count;
		}
	}

	// Token: 0x17000C37 RID: 3127
	// (get) Token: 0x06003E4E RID: 15950 RVA: 0x000EC0A0 File Offset: 0x000EA2A0
	internal int Capacity
	{
		get
		{
			return this.items.Length;
		}
	}

	// Token: 0x17000C38 RID: 3128
	// (get) Token: 0x06003E4F RID: 15951 RVA: 0x000EC0AC File Offset: 0x000EA2AC
	public bool IsReadOnly
	{
		get
		{
			return false;
		}
	}

	// Token: 0x17000C39 RID: 3129
	public T this[int index]
	{
		get
		{
			if (index < 0 || index > this.count - 1)
			{
				throw new IndexOutOfRangeException();
			}
			return this.items[index];
		}
		set
		{
			if (index < 0 || index > this.count - 1)
			{
				throw new IndexOutOfRangeException();
			}
			this.items[index] = value;
		}
	}

	// Token: 0x17000C3A RID: 3130
	// (get) Token: 0x06003E52 RID: 15954 RVA: 0x000EC114 File Offset: 0x000EA314
	internal T[] Items
	{
		get
		{
			return this.items;
		}
	}

	// Token: 0x06003E53 RID: 15955 RVA: 0x000EC11C File Offset: 0x000EA31C
	public void Enqueue(T item)
	{
		this.Add(item);
	}

	// Token: 0x06003E54 RID: 15956 RVA: 0x000EC128 File Offset: 0x000EA328
	public T Dequeue()
	{
		if (this.count == 0)
		{
			throw new IndexOutOfRangeException();
		}
		T result = this.items[0];
		this.RemoveAt(0);
		return result;
	}

	// Token: 0x06003E55 RID: 15957 RVA: 0x000EC15C File Offset: 0x000EA35C
	public dfList<T> Clone()
	{
		dfList<T> dfList = dfList<T>.Obtain(this.count);
		Array.Copy(this.items, dfList.items, this.count);
		dfList.count = this.count;
		return dfList;
	}

	// Token: 0x06003E56 RID: 15958 RVA: 0x000EC19C File Offset: 0x000EA39C
	public void Release()
	{
		this.Clear();
		dfList<T>.pool.Enqueue(this);
	}

	// Token: 0x06003E57 RID: 15959 RVA: 0x000EC1B0 File Offset: 0x000EA3B0
	public void Reverse()
	{
		Array.Reverse(this.items, 0, this.count);
	}

	// Token: 0x06003E58 RID: 15960 RVA: 0x000EC1C4 File Offset: 0x000EA3C4
	public void Sort()
	{
		Array.Sort<T>(this.items, 0, this.count, null);
	}

	// Token: 0x06003E59 RID: 15961 RVA: 0x000EC1DC File Offset: 0x000EA3DC
	public void Sort(IComparer<T> comparer)
	{
		Array.Sort<T>(this.items, 0, this.count, comparer);
	}

	// Token: 0x06003E5A RID: 15962 RVA: 0x000EC1F4 File Offset: 0x000EA3F4
	public void Sort(Comparison<T> comparison)
	{
		if (comparison == null)
		{
			throw new ArgumentNullException("comparison");
		}
		if (this.count > 0)
		{
			using (dfList<T>.FunctorComparer functorComparer = dfList<T>.FunctorComparer.Obtain(comparison))
			{
				Array.Sort<T>(this.items, 0, this.count, functorComparer);
			}
		}
	}

	// Token: 0x06003E5B RID: 15963 RVA: 0x000EC268 File Offset: 0x000EA468
	public void EnsureCapacity(int Size)
	{
		if (this.items.Length < Size)
		{
			int newSize = Size / 128 * 128 + 128;
			Array.Resize<T>(ref this.items, newSize);
		}
	}

	// Token: 0x06003E5C RID: 15964 RVA: 0x000EC2A4 File Offset: 0x000EA4A4
	public void AddRange(dfList<T> list)
	{
		this.EnsureCapacity(this.count + list.Count);
		Array.Copy(list.items, 0, this.items, this.count, list.Count);
		this.count += list.Count;
	}

	// Token: 0x06003E5D RID: 15965 RVA: 0x000EC2F8 File Offset: 0x000EA4F8
	public void AddRange(IList<T> list)
	{
		this.EnsureCapacity(this.count + list.Count);
		for (int i = 0; i < list.Count; i++)
		{
			this.items[this.count++] = list[i];
		}
	}

	// Token: 0x06003E5E RID: 15966 RVA: 0x000EC354 File Offset: 0x000EA554
	public void AddRange(T[] list)
	{
		this.EnsureCapacity(this.count + list.Length);
		Array.Copy(list, 0, this.items, this.count, list.Length);
		this.count += list.Length;
	}

	// Token: 0x06003E5F RID: 15967 RVA: 0x000EC398 File Offset: 0x000EA598
	public int IndexOf(T item)
	{
		return Array.IndexOf<T>(this.items, item, 0, this.count);
	}

	// Token: 0x06003E60 RID: 15968 RVA: 0x000EC3B0 File Offset: 0x000EA5B0
	public void Insert(int index, T item)
	{
		this.EnsureCapacity(this.count + 1);
		if (index < this.count)
		{
			Array.Copy(this.items, index, this.items, index + 1, this.count - index);
		}
		this.items[index] = item;
		this.count++;
	}

	// Token: 0x06003E61 RID: 15969 RVA: 0x000EC410 File Offset: 0x000EA610
	public void InsertRange(int index, T[] array)
	{
		if (array == null)
		{
			throw new ArgumentNullException("items");
		}
		if (index < 0 || index > this.count)
		{
			throw new ArgumentOutOfRangeException("index");
		}
		this.EnsureCapacity(this.count + array.Length);
		if (index < this.count)
		{
			Array.Copy(this.items, index, this.items, index + array.Length, this.count - index);
		}
		array.CopyTo(this.items, index);
		this.count += array.Length;
	}

	// Token: 0x06003E62 RID: 15970 RVA: 0x000EC4A4 File Offset: 0x000EA6A4
	public void InsertRange(int index, dfList<T> list)
	{
		if (list == null)
		{
			throw new ArgumentNullException("items");
		}
		if (index < 0 || index > this.count)
		{
			throw new ArgumentOutOfRangeException("index");
		}
		this.EnsureCapacity(this.count + list.count);
		if (index < this.count)
		{
			Array.Copy(this.items, index, this.items, index + list.count, this.count - index);
		}
		Array.Copy(list.items, 0, this.items, index, list.count);
		this.count += list.count;
	}

	// Token: 0x06003E63 RID: 15971 RVA: 0x000EC550 File Offset: 0x000EA750
	public void RemoveAll(Predicate<T> predicate)
	{
		int i = 0;
		while (i < this.count)
		{
			if (predicate(this.items[i]))
			{
				this.RemoveAt(i);
			}
			else
			{
				i++;
			}
		}
	}

	// Token: 0x06003E64 RID: 15972 RVA: 0x000EC598 File Offset: 0x000EA798
	public void RemoveAt(int index)
	{
		if (index >= this.count)
		{
			throw new ArgumentOutOfRangeException();
		}
		this.count--;
		if (index < this.count)
		{
			Array.Copy(this.items, index + 1, this.items, index, this.count - index);
		}
		this.items[this.count] = default(T);
	}

	// Token: 0x06003E65 RID: 15973 RVA: 0x000EC608 File Offset: 0x000EA808
	public void RemoveRange(int index, int length)
	{
		if (index < 0 || length < 0 || this.count - index < length)
		{
			throw new ArgumentOutOfRangeException();
		}
		if (this.count > 0)
		{
			this.count -= length;
			if (index < this.count)
			{
				Array.Copy(this.items, index + length, this.items, index, this.count - index);
			}
			Array.Clear(this.items, this.count, length);
		}
	}

	// Token: 0x06003E66 RID: 15974 RVA: 0x000EC68C File Offset: 0x000EA88C
	public void Add(T item)
	{
		this.EnsureCapacity(this.count + 1);
		this.items[this.count++] = item;
	}

	// Token: 0x06003E67 RID: 15975 RVA: 0x000EC6C4 File Offset: 0x000EA8C4
	public void Clear()
	{
		Array.Clear(this.items, 0, this.items.Length);
		this.count = 0;
	}

	// Token: 0x06003E68 RID: 15976 RVA: 0x000EC6E4 File Offset: 0x000EA8E4
	public void TrimExcess()
	{
		Array.Resize<T>(ref this.items, this.count);
	}

	// Token: 0x06003E69 RID: 15977 RVA: 0x000EC6F8 File Offset: 0x000EA8F8
	public bool Contains(T item)
	{
		if (item == null)
		{
			for (int i = 0; i < this.count; i++)
			{
				if (this.items[i] == null)
				{
					return true;
				}
			}
			return false;
		}
		EqualityComparer<T> @default = EqualityComparer<T>.Default;
		for (int j = 0; j < this.count; j++)
		{
			if (@default.Equals(this.items[j], item))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06003E6A RID: 15978 RVA: 0x000EC77C File Offset: 0x000EA97C
	public void CopyTo(T[] array)
	{
		this.CopyTo(array, 0);
	}

	// Token: 0x06003E6B RID: 15979 RVA: 0x000EC788 File Offset: 0x000EA988
	public void CopyTo(T[] array, int arrayIndex)
	{
		Array.Copy(this.items, 0, array, arrayIndex, this.count);
	}

	// Token: 0x06003E6C RID: 15980 RVA: 0x000EC7A0 File Offset: 0x000EA9A0
	public void CopyTo(int sourceIndex, T[] dest, int destIndex, int length)
	{
		if (sourceIndex + length > this.count)
		{
			throw new IndexOutOfRangeException("sourceIndex");
		}
		if (dest == null)
		{
			throw new ArgumentNullException("dest");
		}
		if (destIndex + length > dest.Length)
		{
			throw new IndexOutOfRangeException("destIndex");
		}
		Array.Copy(this.items, sourceIndex, dest, destIndex, length);
	}

	// Token: 0x06003E6D RID: 15981 RVA: 0x000EC800 File Offset: 0x000EAA00
	public bool Remove(T item)
	{
		int num = this.IndexOf(item);
		if (num == -1)
		{
			return false;
		}
		this.RemoveAt(num);
		return true;
	}

	// Token: 0x06003E6E RID: 15982 RVA: 0x000EC828 File Offset: 0x000EAA28
	public T[] ToArray()
	{
		T[] array = new T[this.count];
		Array.Copy(this.items, array, this.count);
		return array;
	}

	// Token: 0x06003E6F RID: 15983 RVA: 0x000EC854 File Offset: 0x000EAA54
	public T[] ToArray(int index, int length)
	{
		T[] array = new T[this.count];
		if (this.count > 0)
		{
			this.CopyTo(index, array, 0, length);
		}
		return array;
	}

	// Token: 0x06003E70 RID: 15984 RVA: 0x000EC884 File Offset: 0x000EAA84
	public dfList<T> GetRange(int index, int length)
	{
		dfList<T> dfList = dfList<T>.Obtain(length);
		this.CopyTo(0, dfList.items, index, length);
		return dfList;
	}

	// Token: 0x06003E71 RID: 15985 RVA: 0x000EC8A8 File Offset: 0x000EAAA8
	public bool Any(Func<T, bool> predicate)
	{
		for (int i = 0; i < this.count; i++)
		{
			if (predicate(this.items[i]))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06003E72 RID: 15986 RVA: 0x000EC8E8 File Offset: 0x000EAAE8
	public T First()
	{
		if (this.count == 0)
		{
			throw new IndexOutOfRangeException();
		}
		return this.items[0];
	}

	// Token: 0x06003E73 RID: 15987 RVA: 0x000EC908 File Offset: 0x000EAB08
	public T FirstOrDefault()
	{
		if (this.count > 0)
		{
			return this.items[0];
		}
		return default(T);
	}

	// Token: 0x06003E74 RID: 15988 RVA: 0x000EC938 File Offset: 0x000EAB38
	public T FirstOrDefault(Func<T, bool> predicate)
	{
		for (int i = 0; i < this.count; i++)
		{
			if (predicate(this.items[i]))
			{
				return this.items[i];
			}
		}
		return default(T);
	}

	// Token: 0x06003E75 RID: 15989 RVA: 0x000EC98C File Offset: 0x000EAB8C
	public T Last()
	{
		if (this.count == 0)
		{
			throw new IndexOutOfRangeException();
		}
		return this.items[this.count - 1];
	}

	// Token: 0x06003E76 RID: 15990 RVA: 0x000EC9C0 File Offset: 0x000EABC0
	public T LastOrDefault()
	{
		if (this.count == 0)
		{
			return default(T);
		}
		return this.items[this.count - 1];
	}

	// Token: 0x06003E77 RID: 15991 RVA: 0x000EC9F8 File Offset: 0x000EABF8
	public T LastOrDefault(Func<T, bool> predicate)
	{
		T result = default(T);
		for (int i = 0; i < this.count; i++)
		{
			if (predicate(this.items[i]))
			{
				result = this.items[i];
			}
		}
		return result;
	}

	// Token: 0x06003E78 RID: 15992 RVA: 0x000ECA4C File Offset: 0x000EAC4C
	public dfList<T> Where(Func<T, bool> predicate)
	{
		dfList<T> dfList = dfList<T>.Obtain(this.count);
		for (int i = 0; i < this.count; i++)
		{
			if (predicate(this.items[i]))
			{
				dfList.Add(this.items[i]);
			}
		}
		return dfList;
	}

	// Token: 0x06003E79 RID: 15993 RVA: 0x000ECAA8 File Offset: 0x000EACA8
	public int Matching(Func<T, bool> predicate)
	{
		int num = 0;
		for (int i = 0; i < this.count; i++)
		{
			if (predicate(this.items[i]))
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x06003E7A RID: 15994 RVA: 0x000ECAEC File Offset: 0x000EACEC
	public dfList<TResult> Select<TResult>(Func<T, TResult> selector)
	{
		dfList<TResult> dfList = dfList<TResult>.Obtain(this.count);
		for (int i = 0; i < this.count; i++)
		{
			dfList.Add(selector(this.items[i]));
		}
		return dfList;
	}

	// Token: 0x06003E7B RID: 15995 RVA: 0x000ECB38 File Offset: 0x000EAD38
	public dfList<T> Concat(dfList<T> list)
	{
		dfList<T> dfList = dfList<T>.Obtain(this.count + list.count);
		dfList.AddRange(this);
		dfList.AddRange(list);
		return dfList;
	}

	// Token: 0x06003E7C RID: 15996 RVA: 0x000ECB68 File Offset: 0x000EAD68
	public dfList<TResult> Convert<TResult>()
	{
		dfList<TResult> dfList = dfList<TResult>.Obtain(this.count);
		for (int i = 0; i < this.count; i++)
		{
			dfList.Add((TResult)((object)System.Convert.ChangeType(this.items[i], typeof(TResult))));
		}
		return dfList;
	}

	// Token: 0x06003E7D RID: 15997 RVA: 0x000ECBC4 File Offset: 0x000EADC4
	public void ForEach(Action<T> action)
	{
		int i = 0;
		while (i < this.Count)
		{
			action(this.items[i++]);
		}
	}

	// Token: 0x06003E7E RID: 15998 RVA: 0x000ECBFC File Offset: 0x000EADFC
	public IEnumerator<T> GetEnumerator()
	{
		return dfList<T>.PooledEnumerator.Obtain(this, null);
	}

	// Token: 0x06003E7F RID: 15999 RVA: 0x000ECC08 File Offset: 0x000EAE08
	public void Dispose()
	{
		this.Release();
	}

	// Token: 0x04002174 RID: 8564
	private const int DEFAULT_CAPACITY = 128;

	// Token: 0x04002175 RID: 8565
	private static Queue<object> pool = new Queue<object>();

	// Token: 0x04002176 RID: 8566
	private T[] items = new T[128];

	// Token: 0x04002177 RID: 8567
	private int count;

	// Token: 0x020006DD RID: 1757
	private class PooledEnumerator : IDisposable, IEnumerator, IEnumerable, IEnumerable<T>, IEnumerator<T>
	{
		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x06003E82 RID: 16002 RVA: 0x000ECC24 File Offset: 0x000EAE24
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x06003E83 RID: 16003 RVA: 0x000ECC34 File Offset: 0x000EAE34
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this;
		}

		// Token: 0x06003E84 RID: 16004 RVA: 0x000ECC38 File Offset: 0x000EAE38
		public static dfList<T>.PooledEnumerator Obtain(dfList<T> list, Func<T, bool> predicate = null)
		{
			dfList<T>.PooledEnumerator pooledEnumerator = (dfList<T>.PooledEnumerator.pool.Count <= 0) ? new dfList<T>.PooledEnumerator() : dfList<T>.PooledEnumerator.pool.Dequeue();
			pooledEnumerator.ResetInternal(list, predicate);
			return pooledEnumerator;
		}

		// Token: 0x06003E85 RID: 16005 RVA: 0x000ECC74 File Offset: 0x000EAE74
		public void Release()
		{
			if (this.isValid)
			{
				this.isValid = false;
				dfList<T>.PooledEnumerator.pool.Enqueue(this);
			}
		}

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x06003E86 RID: 16006 RVA: 0x000ECC94 File Offset: 0x000EAE94
		public T Current
		{
			get
			{
				if (!this.isValid)
				{
					throw new InvalidOperationException("The enumerator is no longer valid");
				}
				return this.currentValue;
			}
		}

		// Token: 0x06003E87 RID: 16007 RVA: 0x000ECCB4 File Offset: 0x000EAEB4
		private void ResetInternal(dfList<T> list, Func<T, bool> predicate = null)
		{
			this.isValid = true;
			this.list = list;
			this.predicate = predicate;
			this.currentIndex = 0;
			this.currentValue = default(T);
		}

		// Token: 0x06003E88 RID: 16008 RVA: 0x000ECCEC File Offset: 0x000EAEEC
		public void Dispose()
		{
			this.Release();
		}

		// Token: 0x06003E89 RID: 16009 RVA: 0x000ECCF4 File Offset: 0x000EAEF4
		public bool MoveNext()
		{
			if (!this.isValid)
			{
				throw new InvalidOperationException("The enumerator is no longer valid");
			}
			while (this.currentIndex < this.list.Count)
			{
				T arg = this.list[this.currentIndex++];
				if (this.predicate == null || this.predicate(arg))
				{
					this.currentValue = arg;
					return true;
				}
			}
			this.Release();
			this.currentValue = default(T);
			return false;
		}

		// Token: 0x06003E8A RID: 16010 RVA: 0x000ECD90 File Offset: 0x000EAF90
		public void Reset()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003E8B RID: 16011 RVA: 0x000ECD98 File Offset: 0x000EAF98
		public IEnumerator<T> GetEnumerator()
		{
			return this;
		}

		// Token: 0x04002178 RID: 8568
		private static Queue<dfList<T>.PooledEnumerator> pool = new Queue<dfList<T>.PooledEnumerator>();

		// Token: 0x04002179 RID: 8569
		private dfList<T> list;

		// Token: 0x0400217A RID: 8570
		private Func<T, bool> predicate;

		// Token: 0x0400217B RID: 8571
		private int currentIndex;

		// Token: 0x0400217C RID: 8572
		private T currentValue;

		// Token: 0x0400217D RID: 8573
		private bool isValid;
	}

	// Token: 0x020006DE RID: 1758
	private class FunctorComparer : IDisposable, IComparer<T>
	{
		// Token: 0x06003E8E RID: 16014 RVA: 0x000ECDB0 File Offset: 0x000EAFB0
		public static dfList<T>.FunctorComparer Obtain(Comparison<T> comparison)
		{
			dfList<T>.FunctorComparer functorComparer = (dfList<T>.FunctorComparer.pool.Count <= 0) ? new dfList<T>.FunctorComparer() : dfList<T>.FunctorComparer.pool.Dequeue();
			functorComparer.comparison = comparison;
			return functorComparer;
		}

		// Token: 0x06003E8F RID: 16015 RVA: 0x000ECDEC File Offset: 0x000EAFEC
		public void Release()
		{
			this.comparison = null;
			if (!dfList<T>.FunctorComparer.pool.Contains(this))
			{
				dfList<T>.FunctorComparer.pool.Enqueue(this);
			}
		}

		// Token: 0x06003E90 RID: 16016 RVA: 0x000ECE1C File Offset: 0x000EB01C
		public int Compare(T x, T y)
		{
			return this.comparison(x, y);
		}

		// Token: 0x06003E91 RID: 16017 RVA: 0x000ECE2C File Offset: 0x000EB02C
		public void Dispose()
		{
			this.Release();
		}

		// Token: 0x0400217E RID: 8574
		private static Queue<dfList<T>.FunctorComparer> pool = new Queue<dfList<T>.FunctorComparer>();

		// Token: 0x0400217F RID: 8575
		private Comparison<T> comparison;
	}
}
