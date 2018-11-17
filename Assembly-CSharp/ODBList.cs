using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000375 RID: 885
public class ODBList<T> : IEnumerable, IEnumerable<T>, ICollection<T>, ODBEnumerable<T, ODBForwardEnumerator<T>> where T : Object
{
	// Token: 0x06002184 RID: 8580 RVA: 0x000829D8 File Offset: 0x00080BD8
	protected ODBList()
	{
		this.hashSet = new HSet<T>();
	}

	// Token: 0x06002185 RID: 8581 RVA: 0x000829EC File Offset: 0x00080BEC
	protected ODBList(IEnumerable<T> collection) : this()
	{
		foreach (T item in collection)
		{
			this.DoAdd(item);
		}
	}

	// Token: 0x06002186 RID: 8582 RVA: 0x00082A54 File Offset: 0x00080C54
	protected ODBList(bool isReadOnly) : this()
	{
		this.isReadOnly = isReadOnly;
	}

	// Token: 0x06002187 RID: 8583 RVA: 0x00082A64 File Offset: 0x00080C64
	protected ODBList(bool isReadOnly, IEnumerable<T> collection) : this(collection)
	{
		this.isReadOnly = isReadOnly;
	}

	// Token: 0x06002188 RID: 8584 RVA: 0x00082A74 File Offset: 0x00080C74
	IEnumerator<T> IEnumerable<T>.GetEnumerator()
	{
		ODBForwardEnumerator<T> enumerator = this.GetEnumerator();
		return ODBCachedEnumerator<T, ODBForwardEnumerator<T>>.Cache(ref enumerator);
	}

	// Token: 0x06002189 RID: 8585 RVA: 0x00082A90 File Offset: 0x00080C90
	IEnumerator IEnumerable.GetEnumerator()
	{
		return this.GetEnumerator();
	}

	// Token: 0x0600218A RID: 8586 RVA: 0x00082AA0 File Offset: 0x00080CA0
	void ICollection<T>.CopyTo(T[] array, int arrayIndex)
	{
		this.CopyTo(array, arrayIndex);
	}

	// Token: 0x17000835 RID: 2101
	// (get) Token: 0x0600218B RID: 8587 RVA: 0x00082AAC File Offset: 0x00080CAC
	int ICollection<T>.Count
	{
		get
		{
			return this.count;
		}
	}

	// Token: 0x17000836 RID: 2102
	// (get) Token: 0x0600218C RID: 8588 RVA: 0x00082AB4 File Offset: 0x00080CB4
	bool ICollection<T>.IsReadOnly
	{
		get
		{
			return this.isReadOnly;
		}
	}

	// Token: 0x0600218D RID: 8589 RVA: 0x00082ABC File Offset: 0x00080CBC
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

	// Token: 0x0600218E RID: 8590 RVA: 0x00082AFC File Offset: 0x00080CFC
	bool ICollection<T>.Remove(T item)
	{
		if (this.isReadOnly)
		{
			throw new NotSupportedException("Read Only");
		}
		return this.DoRemove(item);
	}

	// Token: 0x0600218F RID: 8591 RVA: 0x00082B1C File Offset: 0x00080D1C
	void ICollection<T>.Clear()
	{
		if (this.isReadOnly)
		{
			throw new NotSupportedException("Read Only");
		}
		this.DoClear();
	}

	// Token: 0x17000837 RID: 2103
	// (get) Token: 0x06002190 RID: 8592 RVA: 0x00082B3C File Offset: 0x00080D3C
	public ODBForwardEnumerable<T> forward
	{
		get
		{
			return new ODBForwardEnumerable<T>(this);
		}
	}

	// Token: 0x17000838 RID: 2104
	// (get) Token: 0x06002191 RID: 8593 RVA: 0x00082B44 File Offset: 0x00080D44
	public ODBReverseEnumerable<T> reverse
	{
		get
		{
			return new ODBReverseEnumerable<T>(this);
		}
	}

	// Token: 0x06002192 RID: 8594 RVA: 0x00082B4C File Offset: 0x00080D4C
	public bool Contains(T item)
	{
		return this.any && this.hashSet.Contains(item);
	}

	// Token: 0x06002193 RID: 8595 RVA: 0x00082B68 File Offset: 0x00080D68
	public bool Contains(ODBNode<T> item)
	{
		return this.any && item.list == this;
	}

	// Token: 0x06002194 RID: 8596 RVA: 0x00082B84 File Offset: 0x00080D84
	public int CopyTo(T[] array)
	{
		return this.CopyTo(array, 0, this.count);
	}

	// Token: 0x06002195 RID: 8597 RVA: 0x00082B94 File Offset: 0x00080D94
	public int CopyTo(T[] array, int arrayIndex)
	{
		return this.CopyTo(array, arrayIndex, this.count);
	}

	// Token: 0x06002196 RID: 8598 RVA: 0x00082BA4 File Offset: 0x00080DA4
	public int CopyTo(T[] array, int arrayIndex, int count)
	{
		if (!this.any)
		{
			return 0;
		}
		ODBNode<T> item = this.first.item;
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

	// Token: 0x06002197 RID: 8599 RVA: 0x00082C10 File Offset: 0x00080E10
	public T[] ToArray()
	{
		T[] array = new T[this.count];
		this.CopyTo(array, 0, this.count);
		return array;
	}

	// Token: 0x06002198 RID: 8600 RVA: 0x00082C3C File Offset: 0x00080E3C
	public ODBForwardEnumerator<T> GetEnumerator()
	{
		return new ODBForwardEnumerator<T>(this);
	}

	// Token: 0x06002199 RID: 8601 RVA: 0x00082C44 File Offset: 0x00080E44
	public IEnumerable<T> ToGeneric()
	{
		return this;
	}

	// Token: 0x0600219A RID: 8602 RVA: 0x00082C48 File Offset: 0x00080E48
	protected bool DoAdd(T item)
	{
		if (!item)
		{
			throw new MissingReferenceException("You cannot pass a missing or null item into the list");
		}
		if (this.hashSet.Add(item))
		{
			ODBNode<T>.New(this, item);
			return true;
		}
		return false;
	}

	// Token: 0x0600219B RID: 8603 RVA: 0x00082C84 File Offset: 0x00080E84
	protected bool DoAdd(T item, out ODBNode<T> node)
	{
		if (!item)
		{
			throw new MissingReferenceException("You cannot pass a missing or null item into the list");
		}
		if (this.hashSet.Add(item))
		{
			node = ODBNode<T>.New(this, item);
			return true;
		}
		node = null;
		return false;
	}

	// Token: 0x0600219C RID: 8604 RVA: 0x00082CC4 File Offset: 0x00080EC4
	protected bool DoRemove(ref ODBNode<T> node)
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

	// Token: 0x0600219D RID: 8605 RVA: 0x00082D0C File Offset: 0x00080F0C
	protected bool DoRemove(T item)
	{
		if (this.any && this.hashSet.Remove(item))
		{
			this.KnownFind(item).Dispose();
			return true;
		}
		return false;
	}

	// Token: 0x0600219E RID: 8606 RVA: 0x00082D3C File Offset: 0x00080F3C
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

	// Token: 0x0600219F RID: 8607 RVA: 0x00082D70 File Offset: 0x00080F70
	protected void DoUnionWith(ODBList<T> list)
	{
		if (!list.any || list == this)
		{
			return;
		}
		ODBSibling<T> n = list.first;
		do
		{
			T self = n.item.self;
			n = n.item.n;
			if (this.hashSet.Add(self))
			{
				ODBNode<T>.New(this, self);
			}
		}
		while (n.has);
	}

	// Token: 0x060021A0 RID: 8608 RVA: 0x00082DD8 File Offset: 0x00080FD8
	protected void DoExceptWith(ODBList<T> list)
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
			ODBSibling<T> n = list.first;
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

	// Token: 0x060021A1 RID: 8609 RVA: 0x00082E58 File Offset: 0x00081058
	protected void DoSymmetricExceptWith(ODBList<T> list)
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
					ODBSibling<T> n = list.first;
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
							ODBNode<T>.New(this, self);
						}
					}
					while (n.has);
				}
			}
		}
		else if (list.any)
		{
			ODBSibling<T> n2 = list.first;
			do
			{
				T self2 = n2.item.self;
				n2 = n2.item.n;
				this.hashSet.Add(self2);
				ODBNode<T>.New(this, self2);
			}
			while (n2.has);
		}
	}

	// Token: 0x060021A2 RID: 8610 RVA: 0x00082F44 File Offset: 0x00081144
	protected void DoIntersectWith(ODBList<T> list)
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
						ODBSibling<T> n = this.first;
						do
						{
							ODBNode<T> item = n.item;
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

	// Token: 0x060021A3 RID: 8611 RVA: 0x00083018 File Offset: 0x00081218
	protected ODBNode<T> KnownFind(T item)
	{
		ODBSibling<T> n = this.first;
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

	// Token: 0x060021A4 RID: 8612 RVA: 0x00083080 File Offset: 0x00081280
	public RecycleList<T> UnionList(ODBList<T> list)
	{
		return this.hashSet.UnionList(list.hashSet);
	}

	// Token: 0x060021A5 RID: 8613 RVA: 0x00083094 File Offset: 0x00081294
	public RecycleList<T> UnionList(IEnumerable<T> e)
	{
		return this.hashSet.UnionList(e);
	}

	// Token: 0x060021A6 RID: 8614 RVA: 0x000830A4 File Offset: 0x000812A4
	public RecycleList<T> IntersectList(ODBList<T> list)
	{
		return this.hashSet.IntersectList(list.hashSet);
	}

	// Token: 0x060021A7 RID: 8615 RVA: 0x000830B8 File Offset: 0x000812B8
	public RecycleList<T> IntersectList(IEnumerable<T> e)
	{
		return this.hashSet.IntersectList(e);
	}

	// Token: 0x060021A8 RID: 8616 RVA: 0x000830C8 File Offset: 0x000812C8
	public RecycleList<T> ExceptList(ODBList<T> list)
	{
		return this.hashSet.ExceptList(list.hashSet);
	}

	// Token: 0x060021A9 RID: 8617 RVA: 0x000830DC File Offset: 0x000812DC
	public RecycleList<T> ExceptList(IEnumerable<T> e)
	{
		return this.hashSet.ExceptList(e);
	}

	// Token: 0x060021AA RID: 8618 RVA: 0x000830EC File Offset: 0x000812EC
	public RecycleList<T> SymmetricExceptList(ODBList<T> list)
	{
		return this.hashSet.SymmetricExceptList(list.hashSet);
	}

	// Token: 0x060021AB RID: 8619 RVA: 0x00083100 File Offset: 0x00081300
	public RecycleList<T> SymmetricExceptList(IEnumerable<T> e)
	{
		return this.hashSet.SymmetricExceptList(e);
	}

	// Token: 0x060021AC RID: 8620 RVA: 0x00083110 File Offset: 0x00081310
	public RecycleList<T> OperList(HSetOper oper, ODBList<T> list)
	{
		return this.hashSet.OperList(oper, list.hashSet);
	}

	// Token: 0x060021AD RID: 8621 RVA: 0x00083124 File Offset: 0x00081324
	public RecycleList<T> OperList(HSetOper oper, IEnumerable<T> collection)
	{
		return this.hashSet.OperList(oper, collection);
	}

	// Token: 0x04000FB4 RID: 4020
	protected readonly HSet<T> hashSet;

	// Token: 0x04000FB5 RID: 4021
	public ODBSibling<T> first;

	// Token: 0x04000FB6 RID: 4022
	public ODBSibling<T> last;

	// Token: 0x04000FB7 RID: 4023
	public int count;

	// Token: 0x04000FB8 RID: 4024
	public bool any;

	// Token: 0x04000FB9 RID: 4025
	private readonly bool isReadOnly;
}
