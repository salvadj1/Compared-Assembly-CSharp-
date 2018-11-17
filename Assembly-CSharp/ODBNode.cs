using System;
using UnityEngine;

// Token: 0x02000420 RID: 1056
public class ODBNode<T> : IDisposable where T : Object
{
	// Token: 0x060024DC RID: 9436 RVA: 0x00087A38 File Offset: 0x00085C38
	private ODBNode()
	{
	}

	// Token: 0x1700088F RID: 2191
	// (get) Token: 0x060024DD RID: 9437 RVA: 0x00087A40 File Offset: 0x00085C40
	public global::ODBReverseEnumerable<T> beforeInclusive
	{
		get
		{
			return new global::ODBReverseEnumerable<T>(this);
		}
	}

	// Token: 0x17000890 RID: 2192
	// (get) Token: 0x060024DE RID: 9438 RVA: 0x00087A48 File Offset: 0x00085C48
	public global::ODBReverseEnumerable<T> beforeExclusive
	{
		get
		{
			return new global::ODBReverseEnumerable<T>(this.p);
		}
	}

	// Token: 0x17000891 RID: 2193
	// (get) Token: 0x060024DF RID: 9439 RVA: 0x00087A58 File Offset: 0x00085C58
	public global::ODBForwardEnumerable<T> afterInclusive
	{
		get
		{
			return new global::ODBForwardEnumerable<T>(this);
		}
	}

	// Token: 0x17000892 RID: 2194
	// (get) Token: 0x060024E0 RID: 9440 RVA: 0x00087A60 File Offset: 0x00085C60
	public global::ODBForwardEnumerable<T> afterExclusive
	{
		get
		{
			return new global::ODBForwardEnumerable<T>(this.n);
		}
	}

	// Token: 0x060024E1 RID: 9441 RVA: 0x00087A70 File Offset: 0x00085C70
	private void Setup(global::ODBList<T> list, T self)
	{
		this.self = self;
		this.list = list;
		this.hasList = true;
		this.n = default(global::ODBSibling<T>);
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
			global::ODBSibling<T> odbsibling;
			odbsibling.has = true;
			odbsibling.item = this;
			list.first = odbsibling;
			list.last = odbsibling;
		}
	}

	// Token: 0x060024E2 RID: 9442 RVA: 0x00087B30 File Offset: 0x00085D30
	public static global::ODBNode<T> New(global::ODBList<T> list, T self)
	{
		global::ODBNode<T> odbnode;
		if (!global::ODBNode<T>.recycle.Pop(out odbnode))
		{
			odbnode = new global::ODBNode<T>();
		}
		odbnode.Setup(list, self);
		return odbnode;
	}

	// Token: 0x060024E3 RID: 9443 RVA: 0x00087B60 File Offset: 0x00085D60
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
					this.p = default(global::ODBSibling<T>);
					this.list.count--;
				}
				else
				{
					this.n.item.p = default(global::ODBSibling<T>);
					this.list.first = this.n;
					this.list.count--;
				}
			}
			else if (this.p.has)
			{
				this.p.item.n = default(global::ODBSibling<T>);
				this.list.last = this.p;
				this.p = default(global::ODBSibling<T>);
				this.list.count--;
			}
			else
			{
				this.list.count = 0;
				this.list.any = false;
				this.list.first = default(global::ODBSibling<T>);
				this.list.last = default(global::ODBSibling<T>);
			}
			this.hasList = false;
			this.list = null;
			global::ODBNode<T>.recycle.Push(this);
		}
	}

	// Token: 0x04001111 RID: 4369
	public T self;

	// Token: 0x04001112 RID: 4370
	public global::ODBSibling<T> n;

	// Token: 0x04001113 RID: 4371
	public global::ODBSibling<T> p;

	// Token: 0x04001114 RID: 4372
	public global::ODBList<T> list;

	// Token: 0x04001115 RID: 4373
	private bool hasList;

	// Token: 0x04001116 RID: 4374
	private static global::ODBNode<T>.Recycler recycle;

	// Token: 0x02000421 RID: 1057
	private struct Recycler
	{
		// Token: 0x060024E4 RID: 9444 RVA: 0x00087CEC File Offset: 0x00085EEC
		public bool Pop(out global::ODBNode<T> o)
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

		// Token: 0x060024E5 RID: 9445 RVA: 0x00087D4C File Offset: 0x00085F4C
		public void Push(global::ODBNode<T> item)
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
				item.n = default(global::ODBSibling<T>);
				this.items = item;
				this.count = 1;
				this.any = true;
			}
		}

		// Token: 0x04001117 RID: 4375
		public global::ODBNode<T> items;

		// Token: 0x04001118 RID: 4376
		public int count;

		// Token: 0x04001119 RID: 4377
		public bool any;
	}
}
