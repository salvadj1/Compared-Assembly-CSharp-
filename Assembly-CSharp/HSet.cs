using System;
using System.Collections.Generic;

// Token: 0x02000365 RID: 869
public class HSet<T> : HashSet<T>
{
	// Token: 0x06002134 RID: 8500 RVA: 0x00081EA8 File Offset: 0x000800A8
	public HSet()
	{
	}

	// Token: 0x06002135 RID: 8501 RVA: 0x00081EB0 File Offset: 0x000800B0
	public HSet(IEnumerable<T> collection) : base(collection)
	{
	}

	// Token: 0x06002136 RID: 8502 RVA: 0x00081EBC File Offset: 0x000800BC
	public HSet(IEqualityComparer<T> comparer) : base(comparer)
	{
	}

	// Token: 0x06002137 RID: 8503 RVA: 0x00081EC8 File Offset: 0x000800C8
	public HSet(IEnumerable<T> collection, IEqualityComparer<T> comparer) : base(collection, comparer)
	{
	}

	// Token: 0x06002139 RID: 8505 RVA: 0x00081EE0 File Offset: 0x000800E0
	public new HSetIter<T> GetEnumerator()
	{
		return new HSetIter<T>(base.GetEnumerator());
	}

	// Token: 0x0600213A RID: 8506 RVA: 0x00081EF0 File Offset: 0x000800F0
	private RecycleList<T> ToList()
	{
		HSetIter<T> enumerator = this.GetEnumerator();
		return RecycleList<T>.MakeFromValuedEnumerator<HSetIter<T>>(ref enumerator);
	}

	// Token: 0x0600213B RID: 8507 RVA: 0x00081F0C File Offset: 0x0008010C
	public RecycleList<T> UnionList(IEnumerable<T> unionWith)
	{
		RecycleList<T> result = null;
		try
		{
			HSet<T>.temp.UnionWith(this);
			HSet<T>.temp.UnionWith(unionWith);
			result = HSet<T>.temp.ToList();
		}
		finally
		{
			HSet<T>.temp.Clear();
		}
		return result;
	}

	// Token: 0x0600213C RID: 8508 RVA: 0x00081F6C File Offset: 0x0008016C
	public RecycleList<T> IntersectList(IEnumerable<T> intersectWith)
	{
		RecycleList<T> result = null;
		try
		{
			HSet<T>.temp.UnionWith(this);
			HSet<T>.temp.IntersectWith(intersectWith);
			result = HSet<T>.temp.ToList();
		}
		finally
		{
			HSet<T>.temp.Clear();
		}
		return result;
	}

	// Token: 0x0600213D RID: 8509 RVA: 0x00081FCC File Offset: 0x000801CC
	public RecycleList<T> ExceptList(IEnumerable<T> exceptWith)
	{
		RecycleList<T> result = null;
		try
		{
			HSet<T>.temp.UnionWith(this);
			HSet<T>.temp.ExceptWith(exceptWith);
			result = HSet<T>.temp.ToList();
		}
		finally
		{
			HSet<T>.temp.Clear();
		}
		return result;
	}

	// Token: 0x0600213E RID: 8510 RVA: 0x0008202C File Offset: 0x0008022C
	public RecycleList<T> SymmetricExceptList(IEnumerable<T> exceptWith)
	{
		RecycleList<T> result = null;
		try
		{
			HSet<T>.temp.UnionWith(this);
			HSet<T>.temp.SymmetricExceptWith(exceptWith);
			result = HSet<T>.temp.ToList();
		}
		finally
		{
			HSet<T>.temp.Clear();
		}
		return result;
	}

	// Token: 0x0600213F RID: 8511 RVA: 0x0008208C File Offset: 0x0008028C
	public RecycleList<T> OperList(HSetOper oper, IEnumerable<T> collection)
	{
		switch (oper)
		{
		case HSetOper.Union:
			return this.UnionList(collection);
		case HSetOper.Intersect:
			return this.IntersectList(collection);
		case HSetOper.Except:
			return this.ExceptList(collection);
		case HSetOper.SymmetricExcept:
			return this.SymmetricExceptList(collection);
		default:
			throw new ArgumentException("Don't know what to do with " + oper, "oper");
		}
	}

	// Token: 0x04000F9A RID: 3994
	private static HSet<T> temp = new HSet<T>();
}
