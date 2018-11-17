using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000424 RID: 1060
public sealed class ObjectDB<Object> : global::ODBList<Object>, IEnumerable, ICollection<Object>, IEnumerable<Object>, global::ODBEnumerable<Object, global::ODBForwardEnumerator<Object>> where Object : UnityEngine.Object
{
	// Token: 0x0600251B RID: 9499 RVA: 0x000885AC File Offset: 0x000867AC
	public ObjectDB() : base(true)
	{
	}

	// Token: 0x17000897 RID: 2199
	// (get) Token: 0x0600251C RID: 9500 RVA: 0x000885B8 File Offset: 0x000867B8
	bool ICollection<Object>.IsReadOnly
	{
		get
		{
			return true;
		}
	}

	// Token: 0x0600251D RID: 9501 RVA: 0x000885BC File Offset: 0x000867BC
	void ICollection<Object>.Add(Object value)
	{
		throw new NotSupportedException("Use register and you must keep track of the return value");
	}

	// Token: 0x0600251E RID: 9502 RVA: 0x000885C8 File Offset: 0x000867C8
	bool ICollection<Object>.Remove(Object value)
	{
		throw new NotSupportedException("You must call unregister using the return value from Register");
	}

	// Token: 0x0600251F RID: 9503 RVA: 0x000885D4 File Offset: 0x000867D4
	void ICollection<Object>.Clear()
	{
		throw new NotSupportedException("Clear would be catastrophic to design. you must manually unregister everything");
	}

	// Token: 0x06002520 RID: 9504 RVA: 0x000885E0 File Offset: 0x000867E0
	public global::ODBItem<Object> Register(Object value)
	{
		global::ODBNode<Object> node;
		if (base.DoAdd(value, out node))
		{
			return new global::ODBItem<Object>(node);
		}
		throw new ArgumentException(value.ToString() + " was already registered", "value");
	}

	// Token: 0x06002521 RID: 9505 RVA: 0x00088624 File Offset: 0x00086824
	public void Unregister(ref global::ODBItem<Object> value)
	{
		if (!base.DoRemove(ref value.node))
		{
			throw new ArgumentException(value.node.ToString() + " does not belong to this list", "value");
		}
	}

	// Token: 0x06002522 RID: 9506 RVA: 0x00088658 File Offset: 0x00086858
	public bool Contains(ref global::ODBItem<Object> value)
	{
		return base.Contains(value.node);
	}
}
