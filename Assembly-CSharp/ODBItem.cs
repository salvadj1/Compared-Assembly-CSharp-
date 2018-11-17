using System;
using UnityEngine;

// Token: 0x0200040F RID: 1039
public struct ODBItem<TItem> : IEquatable<TItem> where TItem : Object
{
	// Token: 0x06002484 RID: 9348 RVA: 0x0008707C File Offset: 0x0008527C
	internal ODBItem(global::ODBNode<TItem> node)
	{
		this.node = node;
	}

	// Token: 0x06002485 RID: 9349 RVA: 0x00087088 File Offset: 0x00085288
	public override int GetHashCode()
	{
		return this.node.GetHashCode();
	}

	// Token: 0x06002486 RID: 9350 RVA: 0x00087098 File Offset: 0x00085298
	public override string ToString()
	{
		return this.node.ToString();
	}

	// Token: 0x06002487 RID: 9351 RVA: 0x000870A8 File Offset: 0x000852A8
	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return this.node == null || !this.node.self;
		}
		if (obj is global::ODBItem<TItem>)
		{
			return ((global::ODBItem<TItem>)obj).node == this.node;
		}
		if (obj is Object)
		{
			return this.node != null && this.node.self && this.node.self == (Object)obj;
		}
		return obj.Equals(this.node);
	}

	// Token: 0x06002488 RID: 9352 RVA: 0x00087160 File Offset: 0x00085360
	public bool Equals(TItem obj)
	{
		if (obj)
		{
			return obj.Equals(this);
		}
		return this.node == null || !this.node.self;
	}

	// Token: 0x06002489 RID: 9353 RVA: 0x000871C4 File Offset: 0x000853C4
	public static bool operator ==(global::ODBItem<TItem> L, global::ODBItem<TItem> R)
	{
		return L.node == R.node;
	}

	// Token: 0x0600248A RID: 9354 RVA: 0x000871D8 File Offset: 0x000853D8
	public static bool operator !=(global::ODBItem<TItem> L, global::ODBItem<TItem> R)
	{
		return L.node != R.node;
	}

	// Token: 0x0600248B RID: 9355 RVA: 0x000871F0 File Offset: 0x000853F0
	public static bool operator ==(global::ODBItem<TItem> L, TItem R)
	{
		return L.Equals(R);
	}

	// Token: 0x0600248C RID: 9356 RVA: 0x000871FC File Offset: 0x000853FC
	public static bool operator !=(global::ODBItem<TItem> L, TItem R)
	{
		return !L.Equals(R);
	}

	// Token: 0x0600248D RID: 9357 RVA: 0x0008720C File Offset: 0x0008540C
	public static bool operator ==(TItem L, global::ODBItem<TItem> R)
	{
		return R.Equals(L);
	}

	// Token: 0x0600248E RID: 9358 RVA: 0x00087218 File Offset: 0x00085418
	public static bool operator !=(TItem L, global::ODBItem<TItem> R)
	{
		return !R.Equals(L);
	}

	// Token: 0x0600248F RID: 9359 RVA: 0x00087228 File Offset: 0x00085428
	public static implicit operator TItem(global::ODBItem<TItem> item)
	{
		if (item.node != null)
		{
			return item.node.self;
		}
		return (TItem)((object)null);
	}

	// Token: 0x040010F9 RID: 4345
	internal global::ODBNode<TItem> node;
}
