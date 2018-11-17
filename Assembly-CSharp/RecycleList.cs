using System;
using System.Collections.Generic;

// Token: 0x02000379 RID: 889
public class RecycleList<T> : List<T>, IDisposable
{
	// Token: 0x060021C5 RID: 8645 RVA: 0x000832A8 File Offset: 0x000814A8
	internal RecycleList()
	{
	}

	// Token: 0x060021C7 RID: 8647 RVA: 0x000832C4 File Offset: 0x000814C4
	public static RecycleList<T> Make()
	{
		RecycleList<T> recycleList;
		if (RecycleList<T>.binCount > 0)
		{
			recycleList = RecycleList<T>.bin.First.Value;
			RecycleList<T>.bin.RemoveFirst();
			RecycleList<T>.binCount--;
		}
		else
		{
			recycleList = new RecycleList<T>();
		}
		recycleList.bound = true;
		return recycleList;
	}

	// Token: 0x060021C8 RID: 8648 RVA: 0x00083318 File Offset: 0x00081518
	public static void Bin(ref RecycleList<T> list)
	{
		if (list != null)
		{
			if (list.bound)
			{
				RecycleList<T>.bin.AddLast(list);
				list.bound = false;
			}
			list = null;
		}
	}

	// Token: 0x060021C9 RID: 8649 RVA: 0x00083348 File Offset: 0x00081548
	public static RecycleList<T> MakeFromValuedEnumerator<TEnumerator>(ref TEnumerator enumerator) where TEnumerator : struct, IEnumerator<T>
	{
		RecycleList<T> recycleList = RecycleList<T>.Make();
		while (enumerator.MoveNext())
		{
			recycleList.Add((T)((object)enumerator.Current));
		}
		enumerator.Dispose();
		return recycleList;
	}

	// Token: 0x060021CA RID: 8650 RVA: 0x00083398 File Offset: 0x00081598
	public static RecycleList<T> Make<TClassEnumerable>(TClassEnumerable enumerable) where TClassEnumerable : class, IEnumerable<T>
	{
		RecycleList<T> recycleList = RecycleList<T>.Make();
		recycleList.AddRange(enumerable);
		return recycleList;
	}

	// Token: 0x060021CB RID: 8651 RVA: 0x000833B8 File Offset: 0x000815B8
	public static RecycleList<T> MakeValueEnumerable<TStructEnumerable>(ref TStructEnumerable enumerable) where TStructEnumerable : struct, IEnumerable<T>
	{
		RecycleList<T> recycleList = RecycleList<T>.Make();
		recycleList.AddRange(enumerable);
		return recycleList;
	}

	// Token: 0x060021CC RID: 8652 RVA: 0x000833E0 File Offset: 0x000815E0
	public RecycleList<T> Clone()
	{
		return RecycleList<T>.Make<RecycleList<T>>(this);
	}

	// Token: 0x060021CD RID: 8653 RVA: 0x000833E8 File Offset: 0x000815E8
	public void Dispose()
	{
		RecycleList<T> recycleList = this;
		RecycleList<T>.Bin(ref recycleList);
	}

	// Token: 0x060021CE RID: 8654 RVA: 0x00083400 File Offset: 0x00081600
	public RecycleListIter<T> MakeIter()
	{
		return new RecycleListIter<T>(base.GetEnumerator());
	}

	// Token: 0x04000FBB RID: 4027
	private bool bound;

	// Token: 0x04000FBC RID: 4028
	private static int binCount = 0;

	// Token: 0x04000FBD RID: 4029
	private static LinkedList<RecycleList<T>> bin = new LinkedList<RecycleList<T>>();
}
