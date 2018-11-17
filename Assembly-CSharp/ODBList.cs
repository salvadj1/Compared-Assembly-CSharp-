using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000422 RID: 1058
public class ODBList<T> : IEnumerable, IEnumerable<T>, ICollection<T>, global::ODBEnumerable<T, global::ODBForwardEnumerator<T>> where T : Object
{
	// Token: 0x060024E6 RID: 9446 RVA: 0x00087DD4 File Offset: 0x00085FD4
	protected ODBList()
	{
		this.hashSet = new global::HSet<T>();
	}

	// Token: 0x060024E7 RID: 9447 RVA: 0x00087DE8 File Offset: 0x00085FE8
	protected ODBList(IEnumerable<T> collection) : this()
	{
		foreach (T item in collection)
		{
			this.DoAdd(item);
		}
	}

	// Token: 0x060024E8 RID: 9448 RVA: 0x00087E50 File Offset: 0x00086050
	protected ODBList(bool isReadOnly) : this()
	{
		this.isReadOnly = isReadOnly;
	}

	// Token: 0x060024E9 RID: 9449 RVA: 0x00087E60 File Offset: 0x00086060
	protected ODBList(bool isReadOnly, IEnumerable<T> collection) : this(collection)
	{
		this.isReadOnly = isReadOnly;
	}

	// Token: 0x060024EA RID: 9450 RVA: 0x00087E70 File Offset: 0x00086070
	IEnumerator<T> IEnumerable<T>.GetEnumerator()
	{
		global::ODBForwardEnumerator<T> enumerator = this.GetEnumerator();
		return global::ODBCachedEnumerator<T, global::ODBForwardEnumerator<T>>.Cache(ref enumerator);
	}

	// Token: 0x060024EB RID: 9451 RVA: 0x00087E8C File Offset: 0x0008608C
	IEnumerator IEnumerable.GetEnumerator()
	{
		return this.GetEnumerator();
	}

	// Token: 0x060024EC RID: 9452 RVA: 0x00087E9C File Offset: 0x0008609C
	void ICollection<T>.CopyTo(T[] array, int arrayIndex)
	{
		this.CopyTo(array, arrayIndex);
	}

	// Token: 0x17000893 RID: 2195
	// (get) Token: 0x060024ED RID: 9453 RVA: 0x00087EA8 File Offset: 0x000860A8
	int ICollection<T>.Count
	{
		get
		{
			return this.count;
		}
	}

	// Token: 0x17000894 RID: 2196
	// (get) Token: 0x060024EE RID: 9454 RVA: 0x00087EB0 File Offset: 0x000860B0
	bool ICollection<T>.IsReadOnly
	{
		get
		{
			return this.isReadOnly;
		}
	}

	// Token: 0x060024EF RID: 9455 RVA: 0x00087EB8 File Offset: 0x000860B8
	void ICollection<T>.Add(T item)
	{
		if (this.isReadOnly)
		{
			throw new NotSupportedException("Read Only");
		}
		if (!this.DoAdd(item))
		{
			throw new ArgumentException("The list already contains the given item " + item, "item");
		}
	}

	// Token: 0x060024F0 RID: 9456 RVA: 0x00087EF8 File Offset: 0x000860F8
	bool ICollection<T>.Remove(T item)
	{
		if (this.isReadOnly)
		{
			throw new NotSupportedException("Read Only");
		}
		return this.DoRemove(item);
	}

	// Token: 0x060024F1 RID: 9457 RVA: 0x00087F18 File Offset: 0x00086118
	void ICollection<T>.Clear()
	{
		if (this.isReadOnly)
		{
			throw new NotSupportedException("Read Only");
		}
		this.DoClear();
	}

	// Token: 0x17000895 RID: 2197
	// (get) Token: 0x060024F2 RID: 9458 RVA: 0x00087F38 File Offset: 0x00086138
	public global::ODBForwardEnumerable<T> forward
	{
		get
		{
			return new global::ODBForwardEnumerable<T>(this);
		}
	}

	// Token: 0x17000896 RID: 2198
	// (get) Token: 0x060024F3 RID: 9459 RVA: 0x00087F40 File Offset: 0x00086140
	public global::ODBReverseEnumerable<T> reverse
	{
		get
		{
			return new global::ODBReverseEnumerable<T>(this);
		}
	}

	// Token: 0x060024F4 RID: 9460 RVA: 0x00087F48 File Offset: 0x00086148
	public bool Contains(T item)
	{
		return this.any && this.hashSet.Contains(item);
	}

	// Token: 0x060024F5 RID: 9461 RVA: 0x00087F64 File Offset: 0x00086164
	public bool Contains(global::ODBNode<T> item)
	{
		return this.any && item.list == this;
	}

	// Token: 0x060024F6 RID: 9462 RVA: 0x00087F80 File Offset: 0x00086180
	public int CopyTo(T[] array)
	{
		return this.CopyTo(array, 0, this.count);
	}

	// Token: 0x060024F7 RID: 9463 RVA: 0x00087F90 File Offset: 0x00086190
	public int CopyTo(T[] array, int arrayIndex)
	{
		return this.CopyTo(array, arrayIndex, this.count);
	}

	// Token: 0x060024F8 RID: 9464 RVA: 0x00087FA0 File Offset: 0x000861A0
	public int CopyTo(T[] array, int arrayIndex, int count)
	{
		if (!this.any)
		{
			return 0;
		}
		global::ODBNode<T> item = this.first.item;
		int num = -1;
		if (count > this.count)
		{
			count = this.count;
		}
		while (++num < count)
		{
			array[arrayIndex++] = item.self;
			item = item.n.item;
		}
		return num;
	}

	// Token: 0x060024F9 RID: 9465 RVA: 0x0008800C File Offset: 0x0008620C
	public T[] ToArray()
	{
		T[] array = new T[this.count];
		this.CopyTo(array, 0, this.count);
		return array;
	}

	// Token: 0x060024FA RID: 9466 RVA: 0x00088038 File Offset: 0x00086238
	public global::ODBForwardEnumerator<T> GetEnumerator()
	{
		return new global::ODBForwardEnumerator<T>(this);
	}

	// Token: 0x060024FB RID: 9467 RVA: 0x00088040 File Offset: 0x00086240
	public IEnumerable<T> ToGeneric()
	{
		return this;
	}

	// Token: 0x060024FC RID: 9468 RVA: 0x00088044 File Offset: 0x00086244
	protected bool DoAdd(T item)
	{
		if (!item)
		{
			throw new MissingReferenceException("You cannot pass a missing or null item into the list");
		}
		if (this.hashSet.Add(item))
		{
			global::ODBNode<T>.New(this, item);
			return true;
		}
		return false;
	}

	// Token: 0x060024FD RID: 9469 RVA: 0x00088080 File Offset: 0x00086280
	protected bool DoAdd(T item, out global::ODBNode<T> node)
	{
		if (!item)
		{
			throw new MissingReferenceException("You cannot pass a missing or null item into the list");
		}
		if (this.hashSet.Add(item))
		{
			node = global::ODBNode<T>.New(this, item);
			return true;
		}
		node = null;
		return false;
	}

	// Token: 0x060024FE RID: 9470 RVA: 0x000880C0 File Offset: 0x000862C0
	protected bool DoRemove(ref global::ODBNode<T> node)
	{
		if (this.any && node.list == this)
		{
			this.hashSet.Remove(node.self);
			node.Dispose();
			node = null;
			return true;
		}
		return false;
	}

	// Token: 0x060024FF RID: 9471 RVA: 0x00088108 File Offset: 0x00086308
	protected bool DoRemove(T item)
	{
		if (this.any && this.hashSet.Remove(item))
		{
			this.KnownFind(item).Dispose();
			return true;
		}
		return false;
	}

	// Token: 0x06002500 RID: 9472 RVA: 0x00088138 File Offset: 0x00086338
	protected void DoClear()
	{
		if (this.any)
		{
			this.hashSet.Clear();
			do
			{
				this.first.item.Dispose();
			}
			while (this.any);
		}
	}

	// Token: 0x06002501 RID: 9473 RVA: 0x0008816C File Offset: 0x0008636C
	protected void DoUnionWith(global::ODBList<T> list)
	{
		if (!list.any || list == this)
		{
			return;
		}
		global::ODBSibling<T> n = list.first;
		do
		{
			T self = n.item.self;
			n = n.item.n;
			if (this.hashSet.Add(self))
			{
				global::ODBNode<T>.New(this, self);
			}
		}
		while (n.has);
	}

	// Token: 0x06002502 RID: 9474 RVA: 0x000881D4 File Offset: 0x000863D4
	protected void DoExceptWith(global::ODBList<T> list)
	{
		if (!this.any || !list.any)
		{
			return;
		}
		if (list == this)
		{
			this.DoClear();
		}
		else
		{
			global::ODBSibling<T> n = list.first;
			do
			{
				T self = n.item.self;
				n = n.item.n;
				if (this.hashSet.Remove(self))
				{
					this.KnownFind(self).Dispose();
				}
			}
			while (n.has);
		}
	}

	// Token: 0x06002503 RID: 9475 RVA: 0x00088254 File Offset: 0x00086454
	protected void DoSymmetricExceptWith(global::ODBList<T> list)
	{
		if (this.any)
		{
			if (list.any)
			{
				if (list == this)
				{
					this.DoClear();
				}
				else
				{
					global::ODBSibling<T> n = list.first;
					do
					{
						T self = n.item.self;
						n = n.item.n;
						if (this.hashSet.Remove(self))
						{
							this.KnownFind(self).Dispose();
						}
						else
						{
							this.hashSet.Add(self);
							global::ODBNode<T>.New(this, self);
						}
					}
					while (n.has);
				}
			}
		}
		else if (list.any)
		{
			global::ODBSibling<T> n2 = list.first;
			do
			{
				T self2 = n2.item.self;
				n2 = n2.item.n;
				this.hashSet.Add(self2);
				global::ODBNode<T>.New(this, self2);
			}
			while (n2.has);
		}
	}

	// Token: 0x06002504 RID: 9476 RVA: 0x00088340 File Offset: 0x00086540
	protected void DoIntersectWith(global::ODBList<T> list)
	{
		if (this.any)
		{
			if (list.any)
			{
				if (list != this)
				{
					this.hashSet.IntersectWith(list.hashSet);
					int num = this.hashSet.Count;
					if (num == 0)
					{
						while (this.any)
						{
							this.first.item.Dispose();
						}
					}
					else
					{
						global::ODBSibling<T> n = this.first;
						do
						{
							global::ODBNode<T> item = n.item;
							n = n.item.n;
							if (!this.hashSet.Contains(item.self))
							{
								item.Dispose();
								if (this.count == num)
								{
									break;
								}
							}
						}
						while (n.has);
					}
				}
			}
			else
			{
				this.DoClear();
			}
		}
	}

	// Token: 0x06002505 RID: 9477 RVA: 0x00088414 File Offset: 0x00086614
	protected global::ODBNode<T> KnownFind(T item)
	{
		global::ODBSibling<T> n = this.first;
		for (;;)
		{
			T self = n.item.self;
			if (self == item)
			{
				break;
			}
			n = n.item.n;
			if (!n.has)
			{
				goto Block_2;
			}
		}
		return n.item;
		Block_2:
		throw new ArgumentException("item was not found", "item");
	}

	// Token: 0x06002506 RID: 9478 RVA: 0x0008847C File Offset: 0x0008667C
	public global::RecycleList<T> UnionList(global::ODBList<T> list)
	{
		return this.hashSet.UnionList(list.hashSet);
	}

	// Token: 0x06002507 RID: 9479 RVA: 0x00088490 File Offset: 0x00086690
	public global::RecycleList<T> UnionList(IEnumerable<T> e)
	{
		return this.hashSet.UnionList(e);
	}

	// Token: 0x06002508 RID: 9480 RVA: 0x000884A0 File Offset: 0x000866A0
	public global::RecycleList<T> IntersectList(global::ODBList<T> list)
	{
		return this.hashSet.IntersectList(list.hashSet);
	}

	// Token: 0x06002509 RID: 9481 RVA: 0x000884B4 File Offset: 0x000866B4
	public global::RecycleList<T> IntersectList(IEnumerable<T> e)
	{
		return this.hashSet.IntersectList(e);
	}

	// Token: 0x0600250A RID: 9482 RVA: 0x000884C4 File Offset: 0x000866C4
	public global::RecycleList<T> ExceptList(global::ODBList<T> list)
	{
		return this.hashSet.ExceptList(list.hashSet);
	}

	// Token: 0x0600250B RID: 9483 RVA: 0x000884D8 File Offset: 0x000866D8
	public global::RecycleList<T> ExceptList(IEnumerable<T> e)
	{
		return this.hashSet.ExceptList(e);
	}

	// Token: 0x0600250C RID: 9484 RVA: 0x000884E8 File Offset: 0x000866E8
	public global::RecycleList<T> SymmetricExceptList(global::ODBList<T> list)
	{
		return this.hashSet.SymmetricExceptList(list.hashSet);
	}

	// Token: 0x0600250D RID: 9485 RVA: 0x000884FC File Offset: 0x000866FC
	public global::RecycleList<T> SymmetricExceptList(IEnumerable<T> e)
	{
		return this.hashSet.SymmetricExceptList(e);
	}

	// Token: 0x0600250E RID: 9486 RVA: 0x0008850C File Offset: 0x0008670C
	public global::RecycleList<T> OperList(global::HSetOper oper, global::ODBList<T> list)
	{
		return this.hashSet.OperList(oper, list.hashSet);
	}

	// Token: 0x0600250F RID: 9487 RVA: 0x00088520 File Offset: 0x00086720
	public global::RecycleList<T> OperList(global::HSetOper oper, IEnumerable<T> collection)
	{
		return this.hashSet.OperList(oper, collection);
	}

	// Token: 0x0400111A RID: 4378
	protected readonly global::HSet<T> hashSet;

	// Token: 0x0400111B RID: 4379
	public global::ODBSibling<T> first;

	// Token: 0x0400111C RID: 4380
	public global::ODBSibling<T> last;

	// Token: 0x0400111D RID: 4381
	public int count;

	// Token: 0x0400111E RID: 4382
	public bool any;

	// Token: 0x0400111F RID: 4383
	private readonly bool isReadOnly;
}
