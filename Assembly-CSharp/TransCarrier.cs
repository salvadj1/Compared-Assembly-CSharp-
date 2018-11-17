using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000643 RID: 1603
public class TransCarrier : IDLocal, ICarriableTrans
{
	// Token: 0x060037FC RID: 14332 RVA: 0x000CD3F4 File Offset: 0x000CB5F4
	public void TryInit()
	{
		if (this._objs == null)
		{
			this._objs = new HashSet<ICarriableTrans>();
		}
	}

	// Token: 0x060037FD RID: 14333 RVA: 0x000CD40C File Offset: 0x000CB60C
	public virtual void AddObject(ICarriableTrans obj)
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

	// Token: 0x060037FE RID: 14334 RVA: 0x000CD45C File Offset: 0x000CB65C
	public virtual void RemoveObject(ICarriableTrans obj)
	{
		if (this._objs != null)
		{
			this._objs.Remove(obj);
		}
	}

	// Token: 0x060037FF RID: 14335 RVA: 0x000CD478 File Offset: 0x000CB678
	public virtual void DropObjects()
	{
		if (this._objs == null)
		{
			return;
		}
		HashSet<ICarriableTrans> objs = this._objs;
		this._objs = null;
		foreach (ICarriableTrans carriableTrans in objs)
		{
			if (!(carriableTrans is Object) || (Object)carriableTrans)
			{
				carriableTrans.OnDroppedFromCarrier(this);
			}
		}
	}

	// Token: 0x06003800 RID: 14336 RVA: 0x000CD510 File Offset: 0x000CB710
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

	// Token: 0x06003801 RID: 14337 RVA: 0x000CD554 File Offset: 0x000CB754
	public virtual void OnAddedToCarrier(TransCarrier carrier)
	{
	}

	// Token: 0x06003802 RID: 14338 RVA: 0x000CD558 File Offset: 0x000CB758
	public virtual void OnDroppedFromCarrier(TransCarrier carrier)
	{
		this.DropObjects();
	}

	// Token: 0x04001C1A RID: 7194
	public HashSet<ICarriableTrans> _objs;

	// Token: 0x04001C1B RID: 7195
	private bool destroying;
}
