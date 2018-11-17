using System;
using UnityEngine;

// Token: 0x02000362 RID: 866
public struct ODBItem<TItem> : IEquatable<TItem> where TItem : Object
{
	// Token: 0x06002122 RID: 8482 RVA: 0x00081C80 File Offset: 0x0007FE80
	internal ODBItem(ODBNode<TItem> node)
	{
		this.node = node;
	}

	// Token: 0x06002123 RID: 8483 RVA: 0x00081C8C File Offset: 0x0007FE8C
	public override int GetHashCode()
	{
		return this.node.GetHashCode();
	}

	// Token: 0x06002124 RID: 8484 RVA: 0x00081C9C File Offset: 0x0007FE9C
	public override string ToString()
	{
		return this.node.ToString();
	}

	// Token: 0x06002125 RID: 8485 RVA: 0x00081CAC File Offset: 0x0007FEAC
	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return this.node == null || !this.node.self;
		}
		if (obj is ODBItem<TItem>)
		{
			return ((ODBItem<TItem>)obj).node == this.node;
		}
		if (obj is Object)
		{
			return this.node != null && this.node.self && this.node.self == (Object)obj;
		}
		return obj.Equals(this.node);
	}

	// Token: 0x06002126 RID: 8486 RVA: 0x00081D64 File Offset: 0x0007FF64
	public bool Equals(TItem obj)
	{
		if (obj)
		{
			return obj.Equals(this);
		}
		return this.node == null || !this.node.self;
	}

	// Token: 0x06002127 RID: 8487 RVA: 0x00081DC8 File Offset: 0x0007FFC8
	public static bool operator ==(ODBItem<TItem> L, ODBItem<TItem> R)
	{
		return L.node == R.node;
	}

	// Token: 0x06002128 RID: 8488 RVA: 0x00081DDC File Offset: 0x0007FFDC
	public static bool operator !=(ODBItem<TItem> L, ODBItem<TItem> R)
	{
		return L.node != R.node;
	}

	// Token: 0x06002129 RID: 8489 RVA: 0x00081DF4 File Offset: 0x0007FFF4
	public static bool operator ==(ODBItem<TItem> L, TItem R)
	{
		return L.Equals(R);
	}

	// Token: 0x0600212A RID: 8490 RVA: 0x00081E00 File Offset: 0x00080000
	public static bool operator !=(ODBItem<TItem> L, TItem R)
	{
		return !L.Equals(R);
	}

	// Token: 0x0600212B RID: 8491 RVA: 0x00081E10 File Offset: 0x00080010
	public static bool operator ==(TItem L, ODBItem<TItem> R)
	{
		return R.Equals(L);
	}

	// Token: 0x0600212C RID: 8492 RVA: 0x00081E1C File Offset: 0x0008001C
	public static bool operator !=(TItem L, ODBItem<TItem> R)
	{
		return !R.Equals(L);
	}

	// Token: 0x0600212D RID: 8493 RVA: 0x00081E2C File Offset: 0x0008002C
	public static implicit operator TItem(ODBItem<TItem> item)
	{
		if (item.node != null)
		{
			return item.node.self;
		}
		return (TItem)((object)null);
	}

	// Token: 0x04000F93 RID: 3987
	internal ODBNode<TItem> node;
}
