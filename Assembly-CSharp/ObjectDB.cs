using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000377 RID: 887
public sealed class ObjectDB<Object> : ODBList<Object>, IEnumerable, ICollection<Object>, IEnumerable<Object>, ODBEnumerable<Object, ODBForwardEnumerator<Object>> where Object : UnityEngine.Object
{
	// Token: 0x060021B9 RID: 8633 RVA: 0x000831B0 File Offset: 0x000813B0
	public ObjectDB() : base(true)
	{
	}

	// Token: 0x17000839 RID: 2105
	// (get) Token: 0x060021BA RID: 8634 RVA: 0x000831BC File Offset: 0x000813BC
	bool ICollection<Object>.IsReadOnly
	{
		get
		{
			return true;
		}
	}

	// Token: 0x060021BB RID: 8635 RVA: 0x000831C0 File Offset: 0x000813C0
	void ICollection<Object>.Add(Object value)
	{
		throw new NotSupportedException("Use register and you must keep track of the return value");
	}

	// Token: 0x060021BC RID: 8636 RVA: 0x000831CC File Offset: 0x000813CC
	bool ICollection<Object>.Remove(Object value)
	{
		throw new NotSupportedException("You must call unregister using the return value from Register");
	}

	// Token: 0x060021BD RID: 8637 RVA: 0x000831D8 File Offset: 0x000813D8
	void ICollection<Object>.Clear()
	{
		throw new NotSupportedException("Clear would be catastrophic to design. you must manually unregister everything");
	}

	// Token: 0x060021BE RID: 8638 RVA: 0x000831E4 File Offset: 0x000813E4
	public ODBItem<Object> Register(Object value)
	{
		ODBNode<Object> node;
		if (base.DoAdd(value, out node))
		{
			return new ODBItem<Object>(node);
		}
		throw new ArgumentException(value.ToString() + " was already registered", "value");
	}

	// Token: 0x060021BF RID: 8639 RVA: 0x00083228 File Offset: 0x00081428
	public void Unregister(ref ODBItem<Object> value)
	{
		if (!base.DoRemove(ref value.node))
		{
			throw new ArgumentException(value.node.ToString() + " does not belong to this list", "value");
		}
	}

	// Token: 0x060021C0 RID: 8640 RVA: 0x0008325C File Offset: 0x0008145C
	public bool Contains(ref ODBItem<Object> value)
	{
		return base.Contains(value.node);
	}
}
