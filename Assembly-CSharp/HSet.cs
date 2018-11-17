using System;
using System.Collections.Generic;

// Token: 0x02000412 RID: 1042
public class HSet<T> : HashSet<T>
{
	// Token: 0x06002496 RID: 9366 RVA: 0x000872A4 File Offset: 0x000854A4
	public HSet()
	{
	}

	// Token: 0x06002497 RID: 9367 RVA: 0x000872AC File Offset: 0x000854AC
	public HSet(IEnumerable<T> collection) : base(collection)
	{
	}

	// Token: 0x06002498 RID: 9368 RVA: 0x000872B8 File Offset: 0x000854B8
	public HSet(IEqualityComparer<T> comparer) : base(comparer)
	{
	}

	// Token: 0x06002499 RID: 9369 RVA: 0x000872C4 File Offset: 0x000854C4
	public HSet(IEnumerable<T> collection, IEqualityComparer<T> comparer) : base(collection, comparer)
	{
	}

	// Token: 0x0600249B RID: 9371 RVA: 0x000872DC File Offset: 0x000854DC
	public new global::HSetIter<T> GetEnumerator()
	{
		return new global::HSetIter<T>(base.GetEnumerator());
	}

	// Token: 0x0600249C RID: 9372 RVA: 0x000872EC File Offset: 0x000854EC
	private global::RecycleList<T> ToList()
	{
		global::HSetIter<T> enumerator = this.GetEnumerator();
		return global::RecycleList<T>.MakeFromValuedEnumerator<global::HSetIter<T>>(ref enumerator);
	}

	// Token: 0x0600249D RID: 9373 RVA: 0x00087308 File Offset: 0x00085508
	public global::RecycleList<T> UnionList(IEnumerable<T> unionWith)
	{
		global::RecycleList<T> result = null;
		try
		{
			global::HSet<T>.temp.UnionWith(this);
			global::HSet<T>.temp.UnionWith(unionWith);
			result = global::HSet<T>.temp.ToList();
		}
		finally
		{
			global::HSet<T>.temp.Clear();
		}
		return result;
	}

	// Token: 0x0600249E RID: 9374 RVA: 0x00087368 File Offset: 0x00085568
	public global::RecycleList<T> IntersectList(IEnumerable<T> intersectWith)
	{
		global::RecycleList<T> result = null;
		try
		{
			global::HSet<T>.temp.UnionWith(this);
			global::HSet<T>.temp.IntersectWith(intersectWith);
			result = global::HSet<T>.temp.ToList();
		}
		finally
		{
			global::HSet<T>.temp.Clear();
		}
		return result;
	}

	// Token: 0x0600249F RID: 9375 RVA: 0x000873C8 File Offset: 0x000855C8
	public global::RecycleList<T> ExceptList(IEnumerable<T> exceptWith)
	{
		global::RecycleList<T> result = null;
		try
		{
			global::HSet<T>.temp.UnionWith(this);
			global::HSet<T>.temp.ExceptWith(exceptWith);
			result = global::HSet<T>.temp.ToList();
		}
		finally
		{
			global::HSet<T>.temp.Clear();
		}
		return result;
	}

	// Token: 0x060024A0 RID: 9376 RVA: 0x00087428 File Offset: 0x00085628
	public global::RecycleList<T> SymmetricExceptList(IEnumerable<T> exceptWith)
	{
		global::RecycleList<T> result = null;
		try
		{
			global::HSet<T>.temp.UnionWith(this);
			global::HSet<T>.temp.SymmetricExceptWith(exceptWith);
			result = global::HSet<T>.temp.ToList();
		}
		finally
		{
			global::HSet<T>.temp.Clear();
		}
		return result;
	}

	// Token: 0x060024A1 RID: 9377 RVA: 0x00087488 File Offset: 0x00085688
	public global::RecycleList<T> OperList(global::HSetOper oper, IEnumerable<T> collection)
	{
		switch (oper)
		{
		case global::HSetOper.Union:
			return this.UnionList(collection);
		case global::HSetOper.Intersect:
			return this.IntersectList(collection);
		case global::HSetOper.Except:
			return this.ExceptList(collection);
		case global::HSetOper.SymmetricExcept:
			return this.SymmetricExceptList(collection);
		default:
			throw new ArgumentException("Don't know what to do with " + oper, "oper");
		}
	}

	// Token: 0x04001100 RID: 4352
	private static global::HSet<T> temp = new global::HSet<T>();
}
