using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020007AE RID: 1966
public class dfList<T> : IDisposable, IEnumerable, ICollection<T>, IList<T>, IEnumerable<T>
{
	// Token: 0x06004262 RID: 16994 RVA: 0x000F4BE4 File Offset: 0x000F2DE4
	internal dfList()
	{
	}

	// Token: 0x06004263 RID: 16995 RVA: 0x000F4BFC File Offset: 0x000F2DFC
	internal dfList(IList<T> listToClone)
	{
		this.AddRange(listToClone);
	}

	// Token: 0x06004264 RID: 16996 RVA: 0x000F4C1C File Offset: 0x000F2E1C
	internal dfList(int capacity)
	{
		this.EnsureCapacity(capacity);
	}

	// Token: 0x06004266 RID: 16998 RVA: 0x000F4C48 File Offset: 0x000F2E48
	IEnumerator IEnumerable.GetEnumerator()
	{
		return global::dfList<T>.PooledEnumerator.Obtain(this, null);
	}

	// Token: 0x06004267 RID: 16999 RVA: 0x000F4C54 File Offset: 0x000F2E54
	public static global::dfList<T> Obtain()
	{
		return (global::dfList<T>.pool.Count <= 0) ? new global::dfList<T>() : ((global::dfList<T>)global::dfList<T>.pool.Dequeue());
	}

	// Token: 0x06004268 RID: 17000 RVA: 0x000F4C80 File Offset: 0x000F2E80
	internal static global::dfList<T> Obtain(int capacity)
	{
		global::dfList<T> dfList = global::dfList<T>.Obtain();
		dfList.EnsureCapacity(capacity);
		return dfList;
	}

	// Token: 0x17000CBA RID: 3258
	// (get) Token: 0x06004269 RID: 17001 RVA: 0x000F4C9C File Offset: 0x000F2E9C
	public int Count
	{
		get
		{
			return this.count;
		}
	}

	// Token: 0x17000CBB RID: 3259
	// (get) Token: 0x0600426A RID: 17002 RVA: 0x000F4CA4 File Offset: 0x000F2EA4
	internal int Capacity
	{
		get
		{
			return this.items.Length;
		}
	}

	// Token: 0x17000CBC RID: 3260
	// (get) Token: 0x0600426B RID: 17003 RVA: 0x000F4CB0 File Offset: 0x000F2EB0
	public bool IsReadOnly
	{
		get
		{
			return false;
		}
	}

	// Token: 0x17000CBD RID: 3261
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

	// Token: 0x17000CBE RID: 3262
	// (get) Token: 0x0600426E RID: 17006 RVA: 0x000F4D18 File Offset: 0x000F2F18
	internal T[] Items
	{
		get
		{
			return this.items;
		}
	}

	// Token: 0x0600426F RID: 17007 RVA: 0x000F4D20 File Offset: 0x000F2F20
	public void Enqueue(T item)
	{
		this.Add(item);
	}

	// Token: 0x06004270 RID: 17008 RVA: 0x000F4D2C File Offset: 0x000F2F2C
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

	// Token: 0x06004271 RID: 17009 RVA: 0x000F4D60 File Offset: 0x000F2F60
	public global::dfList<T> Clone()
	{
		global::dfList<T> dfList = global::dfList<T>.Obtain(this.count);
		Array.Copy(this.items, dfList.items, this.count);
		dfList.count = this.count;
		return dfList;
	}

	// Token: 0x06004272 RID: 17010 RVA: 0x000F4DA0 File Offset: 0x000F2FA0
	public void Release()
	{
		this.Clear();
		global::dfList<T>.pool.Enqueue(this);
	}

	// Token: 0x06004273 RID: 17011 RVA: 0x000F4DB4 File Offset: 0x000F2FB4
	public void Reverse()
	{
		Array.Reverse(this.items, 0, this.count);
	}

	// Token: 0x06004274 RID: 17012 RVA: 0x000F4DC8 File Offset: 0x000F2FC8
	public void Sort()
	{
		Array.Sort<T>(this.items, 0, this.count, null);
	}

	// Token: 0x06004275 RID: 17013 RVA: 0x000F4DE0 File Offset: 0x000F2FE0
	public void Sort(IComparer<T> comparer)
	{
		Array.Sort<T>(this.items, 0, this.count, comparer);
	}

	// Token: 0x06004276 RID: 17014 RVA: 0x000F4DF8 File Offset: 0x000F2FF8
	public void Sort(Comparison<T> comparison)
	{
		if (comparison == null)
		{
			throw new ArgumentNullException("comparison");
		}
		if (this.count > 0)
		{
			using (global::dfList<T>.FunctorComparer functorComparer = global::dfList<T>.FunctorComparer.Obtain(comparison))
			{
				Array.Sort<T>(this.items, 0, this.count, functorComparer);
			}
		}
	}

	// Token: 0x06004277 RID: 17015 RVA: 0x000F4E6C File Offset: 0x000F306C
	public void EnsureCapacity(int Size)
	{
		if (this.items.Length < Size)
		{
			int newSize = Size / 128 * 128 + 128;
			Array.Resize<T>(ref this.items, newSize);
		}
	}

	// Token: 0x06004278 RID: 17016 RVA: 0x000F4EA8 File Offset: 0x000F30A8
	public void AddRange(global::dfList<T> list)
	{
		this.EnsureCapacity(this.count + list.Count);
		Array.Copy(list.items, 0, this.items, this.count, list.Count);
		this.count += list.Count;
	}

	// Token: 0x06004279 RID: 17017 RVA: 0x000F4EFC File Offset: 0x000F30FC
	public void AddRange(IList<T> list)
	{
		this.EnsureCapacity(this.count + list.Count);
		for (int i = 0; i < list.Count; i++)
		{
			this.items[this.count++] = list[i];
		}
	}

	// Token: 0x0600427A RID: 17018 RVA: 0x000F4F58 File Offset: 0x000F3158
	public void AddRange(T[] list)
	{
		this.EnsureCapacity(this.count + list.Length);
		Array.Copy(list, 0, this.items, this.count, list.Length);
		this.count += list.Length;
	}

	// Token: 0x0600427B RID: 17019 RVA: 0x000F4F9C File Offset: 0x000F319C
	public int IndexOf(T item)
	{
		return Array.IndexOf<T>(this.items, item, 0, this.count);
	}

	// Token: 0x0600427C RID: 17020 RVA: 0x000F4FB4 File Offset: 0x000F31B4
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

	// Token: 0x0600427D RID: 17021 RVA: 0x000F5014 File Offset: 0x000F3214
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

	// Token: 0x0600427E RID: 17022 RVA: 0x000F50A8 File Offset: 0x000F32A8
	public void InsertRange(int index, global::dfList<T> list)
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

	// Token: 0x0600427F RID: 17023 RVA: 0x000F5154 File Offset: 0x000F3354
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

	// Token: 0x06004280 RID: 17024 RVA: 0x000F519C File Offset: 0x000F339C
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

	// Token: 0x06004281 RID: 17025 RVA: 0x000F520C File Offset: 0x000F340C
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

	// Token: 0x06004282 RID: 17026 RVA: 0x000F5290 File Offset: 0x000F3490
	public void Add(T item)
	{
		this.EnsureCapacity(this.count + 1);
		this.items[this.count++] = item;
	}

	// Token: 0x06004283 RID: 17027 RVA: 0x000F52C8 File Offset: 0x000F34C8
	public void Clear()
	{
		Array.Clear(this.items, 0, this.items.Length);
		this.count = 0;
	}

	// Token: 0x06004284 RID: 17028 RVA: 0x000F52E8 File Offset: 0x000F34E8
	public void TrimExcess()
	{
		Array.Resize<T>(ref this.items, this.count);
	}

	// Token: 0x06004285 RID: 17029 RVA: 0x000F52FC File Offset: 0x000F34FC
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

	// Token: 0x06004286 RID: 17030 RVA: 0x000F5380 File Offset: 0x000F3580
	public void CopyTo(T[] array)
	{
		this.CopyTo(array, 0);
	}

	// Token: 0x06004287 RID: 17031 RVA: 0x000F538C File Offset: 0x000F358C
	public void CopyTo(T[] array, int arrayIndex)
	{
		Array.Copy(this.items, 0, array, arrayIndex, this.count);
	}

	// Token: 0x06004288 RID: 17032 RVA: 0x000F53A4 File Offset: 0x000F35A4
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

	// Token: 0x06004289 RID: 17033 RVA: 0x000F5404 File Offset: 0x000F3604
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

	// Token: 0x0600428A RID: 17034 RVA: 0x000F542C File Offset: 0x000F362C
	public T[] ToArray()
	{
		T[] array = new T[this.count];
		Array.Copy(this.items, array, this.count);
		return array;
	}

	// Token: 0x0600428B RID: 17035 RVA: 0x000F5458 File Offset: 0x000F3658
	public T[] ToArray(int index, int length)
	{
		T[] array = new T[this.count];
		if (this.count > 0)
		{
			this.CopyTo(index, array, 0, length);
		}
		return array;
	}

	// Token: 0x0600428C RID: 17036 RVA: 0x000F5488 File Offset: 0x000F3688
	public global::dfList<T> GetRange(int index, int length)
	{
		global::dfList<T> dfList = global::dfList<T>.Obtain(length);
		this.CopyTo(0, dfList.items, index, length);
		return dfList;
	}

	// Token: 0x0600428D RID: 17037 RVA: 0x000F54AC File Offset: 0x000F36AC
	public bool Any(System.Func<T, bool> predicate)
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

	// Token: 0x0600428E RID: 17038 RVA: 0x000F54EC File Offset: 0x000F36EC
	public T First()
	{
		if (this.count == 0)
		{
			throw new IndexOutOfRangeException();
		}
		return this.items[0];
	}

	// Token: 0x0600428F RID: 17039 RVA: 0x000F550C File Offset: 0x000F370C
	public T FirstOrDefault()
	{
		if (this.count > 0)
		{
			return this.items[0];
		}
		return default(T);
	}

	// Token: 0x06004290 RID: 17040 RVA: 0x000F553C File Offset: 0x000F373C
	public T FirstOrDefault(System.Func<T, bool> predicate)
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

	// Token: 0x06004291 RID: 17041 RVA: 0x000F5590 File Offset: 0x000F3790
	public T Last()
	{
		if (this.count == 0)
		{
			throw new IndexOutOfRangeException();
		}
		return this.items[this.count - 1];
	}

	// Token: 0x06004292 RID: 17042 RVA: 0x000F55C4 File Offset: 0x000F37C4
	public T LastOrDefault()
	{
		if (this.count == 0)
		{
			return default(T);
		}
		return this.items[this.count - 1];
	}

	// Token: 0x06004293 RID: 17043 RVA: 0x000F55FC File Offset: 0x000F37FC
	public T LastOrDefault(System.Func<T, bool> predicate)
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

	// Token: 0x06004294 RID: 17044 RVA: 0x000F5650 File Offset: 0x000F3850
	public global::dfList<T> Where(System.Func<T, bool> predicate)
	{
		global::dfList<T> dfList = global::dfList<T>.Obtain(this.count);
		for (int i = 0; i < this.count; i++)
		{
			if (predicate(this.items[i]))
			{
				dfList.Add(this.items[i]);
			}
		}
		return dfList;
	}

	// Token: 0x06004295 RID: 17045 RVA: 0x000F56AC File Offset: 0x000F38AC
	public int Matching(System.Func<T, bool> predicate)
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

	// Token: 0x06004296 RID: 17046 RVA: 0x000F56F0 File Offset: 0x000F38F0
	public global::dfList<TResult> Select<TResult>(System.Func<T, TResult> selector)
	{
		global::dfList<TResult> dfList = global::dfList<TResult>.Obtain(this.count);
		for (int i = 0; i < this.count; i++)
		{
			dfList.Add(selector(this.items[i]));
		}
		return dfList;
	}

	// Token: 0x06004297 RID: 17047 RVA: 0x000F573C File Offset: 0x000F393C
	public global::dfList<T> Concat(global::dfList<T> list)
	{
		global::dfList<T> dfList = global::dfList<T>.Obtain(this.count + list.count);
		dfList.AddRange(this);
		dfList.AddRange(list);
		return dfList;
	}

	// Token: 0x06004298 RID: 17048 RVA: 0x000F576C File Offset: 0x000F396C
	public global::dfList<TResult> Convert<TResult>()
	{
		global::dfList<TResult> dfList = global::dfList<TResult>.Obtain(this.count);
		for (int i = 0; i < this.count; i++)
		{
			dfList.Add((TResult)((object)System.Convert.ChangeType(this.items[i], typeof(TResult))));
		}
		return dfList;
	}

	// Token: 0x06004299 RID: 17049 RVA: 0x000F57C8 File Offset: 0x000F39C8
	public void ForEach(Action<T> action)
	{
		int i = 0;
		while (i < this.Count)
		{
			action(this.items[i++]);
		}
	}

	// Token: 0x0600429A RID: 17050 RVA: 0x000F5800 File Offset: 0x000F3A00
	public IEnumerator<T> GetEnumerator()
	{
		return global::dfList<T>.PooledEnumerator.Obtain(this, null);
	}

	// Token: 0x0600429B RID: 17051 RVA: 0x000F580C File Offset: 0x000F3A0C
	public void Dispose()
	{
		this.Release();
	}

	// Token: 0x0400237D RID: 9085
	private const int DEFAULT_CAPACITY = 128;

	// Token: 0x0400237E RID: 9086
	private static Queue<object> pool = new Queue<object>();

	// Token: 0x0400237F RID: 9087
	private T[] items = new T[128];

	// Token: 0x04002380 RID: 9088
	private int count;

	// Token: 0x020007AF RID: 1967
	private class PooledEnumerator : IDisposable, IEnumerator, IEnumerable, IEnumerable<T>, IEnumerator<T>
	{
		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x0600429E RID: 17054 RVA: 0x000F5828 File Offset: 0x000F3A28
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x0600429F RID: 17055 RVA: 0x000F5838 File Offset: 0x000F3A38
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this;
		}

		// Token: 0x060042A0 RID: 17056 RVA: 0x000F583C File Offset: 0x000F3A3C
		public static global::dfList<T>.PooledEnumerator Obtain(global::dfList<T> list, System.Func<T, bool> predicate = null)
		{
			global::dfList<T>.PooledEnumerator pooledEnumerator = (global::dfList<T>.PooledEnumerator.pool.Count <= 0) ? new global::dfList<T>.PooledEnumerator() : global::dfList<T>.PooledEnumerator.pool.Dequeue();
			pooledEnumerator.ResetInternal(list, predicate);
			return pooledEnumerator;
		}

		// Token: 0x060042A1 RID: 17057 RVA: 0x000F5878 File Offset: 0x000F3A78
		public void Release()
		{
			if (this.isValid)
			{
				this.isValid = false;
				global::dfList<T>.PooledEnumerator.pool.Enqueue(this);
			}
		}

		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x060042A2 RID: 17058 RVA: 0x000F5898 File Offset: 0x000F3A98
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

		// Token: 0x060042A3 RID: 17059 RVA: 0x000F58B8 File Offset: 0x000F3AB8
		private void ResetInternal(global::dfList<T> list, System.Func<T, bool> predicate = null)
		{
			this.isValid = true;
			this.list = list;
			this.predicate = predicate;
			this.currentIndex = 0;
			this.currentValue = default(T);
		}

		// Token: 0x060042A4 RID: 17060 RVA: 0x000F58F0 File Offset: 0x000F3AF0
		public void Dispose()
		{
			this.Release();
		}

		// Token: 0x060042A5 RID: 17061 RVA: 0x000F58F8 File Offset: 0x000F3AF8
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

		// Token: 0x060042A6 RID: 17062 RVA: 0x000F5994 File Offset: 0x000F3B94
		public void Reset()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060042A7 RID: 17063 RVA: 0x000F599C File Offset: 0x000F3B9C
		public IEnumerator<T> GetEnumerator()
		{
			return this;
		}

		// Token: 0x04002381 RID: 9089
		private static Queue<global::dfList<T>.PooledEnumerator> pool = new Queue<global::dfList<T>.PooledEnumerator>();

		// Token: 0x04002382 RID: 9090
		private global::dfList<T> list;

		// Token: 0x04002383 RID: 9091
		private System.Func<T, bool> predicate;

		// Token: 0x04002384 RID: 9092
		private int currentIndex;

		// Token: 0x04002385 RID: 9093
		private T currentValue;

		// Token: 0x04002386 RID: 9094
		private bool isValid;
	}

	// Token: 0x020007B0 RID: 1968
	private class FunctorComparer : IDisposable, IComparer<T>
	{
		// Token: 0x060042AA RID: 17066 RVA: 0x000F59B4 File Offset: 0x000F3BB4
		public static global::dfList<T>.FunctorComparer Obtain(Comparison<T> comparison)
		{
			global::dfList<T>.FunctorComparer functorComparer = (global::dfList<T>.FunctorComparer.pool.Count <= 0) ? new global::dfList<T>.FunctorComparer() : global::dfList<T>.FunctorComparer.pool.Dequeue();
			functorComparer.comparison = comparison;
			return functorComparer;
		}

		// Token: 0x060042AB RID: 17067 RVA: 0x000F59F0 File Offset: 0x000F3BF0
		public void Release()
		{
			this.comparison = null;
			if (!global::dfList<T>.FunctorComparer.pool.Contains(this))
			{
				global::dfList<T>.FunctorComparer.pool.Enqueue(this);
			}
		}

		// Token: 0x060042AC RID: 17068 RVA: 0x000F5A20 File Offset: 0x000F3C20
		public int Compare(T x, T y)
		{
			return this.comparison(x, y);
		}

		// Token: 0x060042AD RID: 17069 RVA: 0x000F5A30 File Offset: 0x000F3C30
		public void Dispose()
		{
			this.Release();
		}

		// Token: 0x04002387 RID: 9095
		private static Queue<global::dfList<T>.FunctorComparer> pool = new Queue<global::dfList<T>.FunctorComparer>();

		// Token: 0x04002388 RID: 9096
		private Comparison<T> comparison;
	}
}
