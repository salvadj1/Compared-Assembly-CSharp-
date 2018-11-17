using System;
using UnityEngine;

// Token: 0x02000373 RID: 883
public class ODBNode<T> : IDisposable where T : Object
{
	// Token: 0x0600217A RID: 8570 RVA: 0x0008263C File Offset: 0x0008083C
	private ODBNode()
	{
	}

	// Token: 0x17000831 RID: 2097
	// (get) Token: 0x0600217B RID: 8571 RVA: 0x00082644 File Offset: 0x00080844
	public ODBReverseEnumerable<T> beforeInclusive
	{
		get
		{
			return new ODBReverseEnumerable<T>(this);
		}
	}

	// Token: 0x17000832 RID: 2098
	// (get) Token: 0x0600217C RID: 8572 RVA: 0x0008264C File Offset: 0x0008084C
	public ODBReverseEnumerable<T> beforeExclusive
	{
		get
		{
			return new ODBReverseEnumerable<T>(this.p);
		}
	}

	// Token: 0x17000833 RID: 2099
	// (get) Token: 0x0600217D RID: 8573 RVA: 0x0008265C File Offset: 0x0008085C
	public ODBForwardEnumerable<T> afterInclusive
	{
		get
		{
			return new ODBForwardEnumerable<T>(this);
		}
	}

	// Token: 0x17000834 RID: 2100
	// (get) Token: 0x0600217E RID: 8574 RVA: 0x00082664 File Offset: 0x00080864
	public ODBForwardEnumerable<T> afterExclusive
	{
		get
		{
			return new ODBForwardEnumerable<T>(this.n);
		}
	}

	// Token: 0x0600217F RID: 8575 RVA: 0x00082674 File Offset: 0x00080874
	private void Setup(ODBList<T> list, T self)
	{
		this.self = self;
		this.list = list;
		this.hasList = true;
		this.n = default(ODBSibling<T>);
		if (list.any)
		{
			this.p = list.last;
			this.p.item.n.item = this;
			this.p.item.n.has = true;
			list.last.item = this;
			list.count++;
		}
		else
		{
			list.count = 1;
			list.any = true;
			ODBSibling<T> odbsibling;
			odbsibling.has = true;
			odbsibling.item = this;
			list.first = odbsibling;
			list.last = odbsibling;
		}
	}

	// Token: 0x06002180 RID: 8576 RVA: 0x00082734 File Offset: 0x00080934
	public static ODBNode<T> New(ODBList<T> list, T self)
	{
		ODBNode<T> odbnode;
		if (!ODBNode<T>.recycle.Pop(out odbnode))
		{
			odbnode = new ODBNode<T>();
		}
		odbnode.Setup(list, self);
		return odbnode;
	}

	// Token: 0x06002181 RID: 8577 RVA: 0x00082764 File Offset: 0x00080964
	public void Dispose()
	{
		if (this.hasList)
		{
			if (this.n.has)
			{
				if (this.p.has)
				{
					this.p.item.n = this.n;
					this.n.item.p = this.p;
					this.p = default(ODBSibling<T>);
					this.list.count--;
				}
				else
				{
					this.n.item.p = default(ODBSibling<T>);
					this.list.first = this.n;
					this.list.count--;
				}
			}
			else if (this.p.has)
			{
				this.p.item.n = default(ODBSibling<T>);
				this.list.last = this.p;
				this.p = default(ODBSibling<T>);
				this.list.count--;
			}
			else
			{
				this.list.count = 0;
				this.list.any = false;
				this.list.first = default(ODBSibling<T>);
				this.list.last = default(ODBSibling<T>);
			}
			this.hasList = false;
			this.list = null;
			ODBNode<T>.recycle.Push(this);
		}
	}

	// Token: 0x04000FAB RID: 4011
	public T self;

	// Token: 0x04000FAC RID: 4012
	public ODBSibling<T> n;

	// Token: 0x04000FAD RID: 4013
	public ODBSibling<T> p;

	// Token: 0x04000FAE RID: 4014
	public ODBList<T> list;

	// Token: 0x04000FAF RID: 4015
	private bool hasList;

	// Token: 0x04000FB0 RID: 4016
	private static ODBNode<T>.Recycler recycle;

	// Token: 0x02000374 RID: 884
	private struct Recycler
	{
		// Token: 0x06002182 RID: 8578 RVA: 0x000828F0 File Offset: 0x00080AF0
		public bool Pop(out ODBNode<T> o)
		{
			o = this.items;
			if (this.any)
			{
				if (--this.count == 0)
				{
					this.any = false;
					this.items = null;
				}
				else
				{
					this.items = o.n.item;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06002183 RID: 8579 RVA: 0x00082950 File Offset: 0x00080B50
		public void Push(ODBNode<T> item)
		{
			item.list = null;
			item.self = (T)((object)null);
			if (this.any)
			{
				item.n.item = this.items;
				item.n.has = true;
				this.items = item;
				this.count++;
			}
			else
			{
				item.n = default(ODBSibling<T>);
				this.items = item;
				this.count = 1;
				this.any = true;
			}
		}

		// Token: 0x04000FB1 RID: 4017
		public ODBNode<T> items;

		// Token: 0x04000FB2 RID: 4018
		public int count;

		// Token: 0x04000FB3 RID: 4019
		public bool any;
	}
}
