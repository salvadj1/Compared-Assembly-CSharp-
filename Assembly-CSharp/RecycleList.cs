using System;
using System.Collections.Generic;

// Token: 0x02000426 RID: 1062
public class RecycleList<T> : List<T>, IDisposable
{
	// Token: 0x06002527 RID: 9511 RVA: 0x000886A4 File Offset: 0x000868A4
	internal RecycleList()
	{
	}

	// Token: 0x06002529 RID: 9513 RVA: 0x000886C0 File Offset: 0x000868C0
	public static global::RecycleList<T> Make()
	{
		global::RecycleList<T> recycleList;
		if (global::RecycleList<T>.binCount > 0)
		{
			recycleList = global::RecycleList<T>.bin.First.Value;
			global::RecycleList<T>.bin.RemoveFirst();
			global::RecycleList<T>.binCount--;
		}
		else
		{
			recycleList = new global::RecycleList<T>();
		}
		recycleList.bound = true;
		return recycleList;
	}

	// Token: 0x0600252A RID: 9514 RVA: 0x00088714 File Offset: 0x00086914
	public static void Bin(ref global::RecycleList<T> list)
	{
		if (list != null)
		{
			if (list.bound)
			{
				global::RecycleList<T>.bin.AddLast(list);
				list.bound = false;
			}
			list = null;
		}
	}

	// Token: 0x0600252B RID: 9515 RVA: 0x00088744 File Offset: 0x00086944
	public static global::RecycleList<T> MakeFromValuedEnumerator<TEnumerator>(ref TEnumerator enumerator) where TEnumerator : struct, IEnumerator<T>
	{
		global::RecycleList<T> recycleList = global::RecycleList<T>.Make();
		while (enumerator.MoveNext())
		{
			recycleList.Add((T)((object)enumerator.Current));
		}
		enumerator.Dispose();
		return recycleList;
	}

	// Token: 0x0600252C RID: 9516 RVA: 0x00088794 File Offset: 0x00086994
	public static global::RecycleList<T> Make<TClassEnumerable>(TClassEnumerable enumerable) where TClassEnumerable : class, IEnumerable<T>
	{
		global::RecycleList<T> recycleList = global::RecycleList<T>.Make();
		recycleList.AddRange(enumerable);
		return recycleList;
	}

	// Token: 0x0600252D RID: 9517 RVA: 0x000887B4 File Offset: 0x000869B4
	public static global::RecycleList<T> MakeValueEnumerable<TStructEnumerable>(ref TStructEnumerable enumerable) where TStructEnumerable : struct, IEnumerable<T>
	{
		global::RecycleList<T> recycleList = global::RecycleList<T>.Make();
		recycleList.AddRange(enumerable);
		return recycleList;
	}

	// Token: 0x0600252E RID: 9518 RVA: 0x000887DC File Offset: 0x000869DC
	public global::RecycleList<T> Clone()
	{
		return global::RecycleList<T>.Make<global::RecycleList<T>>(this);
	}

	// Token: 0x0600252F RID: 9519 RVA: 0x000887E4 File Offset: 0x000869E4
	public void Dispose()
	{
		global::RecycleList<T> recycleList = this;
		global::RecycleList<T>.Bin(ref recycleList);
	}

	// Token: 0x06002530 RID: 9520 RVA: 0x000887FC File Offset: 0x000869FC
	public global::RecycleListIter<T> MakeIter()
	{
		return new global::RecycleListIter<T>(base.GetEnumerator());
	}

	// Token: 0x04001121 RID: 4385
	private bool bound;

	// Token: 0x04001122 RID: 4386
	private static int binCount = 0;

	// Token: 0x04001123 RID: 4387
	private static LinkedList<global::RecycleList<T>> bin = new LinkedList<global::RecycleList<T>>();
}
