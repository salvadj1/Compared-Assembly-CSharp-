using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000706 RID: 1798
public class TransCarrier : IDLocal, global::ICarriableTrans
{
	// Token: 0x06003BE8 RID: 15336 RVA: 0x000D5CA4 File Offset: 0x000D3EA4
	public void TryInit()
	{
		if (this._objs == null)
		{
			this._objs = new HashSet<global::ICarriableTrans>();
		}
	}

	// Token: 0x06003BE9 RID: 15337 RVA: 0x000D5CBC File Offset: 0x000D3EBC
	public virtual void AddObject(global::ICarriableTrans obj)
	{
		if (object.ReferenceEquals(obj, this))
		{
			return;
		}
		if (this.destroying)
		{
			Debug.LogWarning("Did not add object because the this carrier is destroying", this);
		}
		else
		{
			this.TryInit();
			this._objs.Add(obj);
			obj.OnAddedToCarrier(this);
		}
	}

	// Token: 0x06003BEA RID: 15338 RVA: 0x000D5D0C File Offset: 0x000D3F0C
	public virtual void RemoveObject(global::ICarriableTrans obj)
	{
		if (this._objs != null)
		{
			this._objs.Remove(obj);
		}
	}

	// Token: 0x06003BEB RID: 15339 RVA: 0x000D5D28 File Offset: 0x000D3F28
	public virtual void DropObjects()
	{
		if (this._objs == null)
		{
			return;
		}
		HashSet<global::ICarriableTrans> objs = this._objs;
		this._objs = null;
		foreach (global::ICarriableTrans carriableTrans in objs)
		{
			if (!(carriableTrans is Object) || (Object)carriableTrans)
			{
				carriableTrans.OnDroppedFromCarrier(this);
			}
		}
	}

	// Token: 0x06003BEC RID: 15340 RVA: 0x000D5DC0 File Offset: 0x000D3FC0
	public void DropObjects(bool andDisableAddingAfter)
	{
		try
		{
			this.DropObjects();
		}
		finally
		{
			if (andDisableAddingAfter)
			{
				this.destroying = true;
			}
		}
	}

	// Token: 0x06003BED RID: 15341 RVA: 0x000D5E04 File Offset: 0x000D4004
	public virtual void OnAddedToCarrier(global::TransCarrier carrier)
	{
	}

	// Token: 0x06003BEE RID: 15342 RVA: 0x000D5E08 File Offset: 0x000D4008
	public virtual void OnDroppedFromCarrier(global::TransCarrier carrier)
	{
		this.DropObjects();
	}

	// Token: 0x04001E0F RID: 7695
	public HashSet<global::ICarriableTrans> _objs;

	// Token: 0x04001E10 RID: 7696
	private bool destroying;
}
