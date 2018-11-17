using System;
using UnityEngine;

// Token: 0x020006BF RID: 1727
public abstract class WeaponModRep : global::ItemModRepresentation
{
	// Token: 0x06003AB8 RID: 15032 RVA: 0x000CDFCC File Offset: 0x000CC1CC
	protected WeaponModRep(global::ItemModRepresentation.Caps caps, bool defaultsOn) : base(caps)
	{
		this.defaultsOn = defaultsOn;
		this._on = defaultsOn;
	}

	// Token: 0x06003AB9 RID: 15033 RVA: 0x000CDFE4 File Offset: 0x000CC1E4
	protected WeaponModRep(global::ItemModRepresentation.Caps caps) : this(caps, false)
	{
	}

	// Token: 0x17000B69 RID: 2921
	// (get) Token: 0x06003ABA RID: 15034 RVA: 0x000CDFF0 File Offset: 0x000CC1F0
	// (set) Token: 0x06003ABB RID: 15035 RVA: 0x000CDFF8 File Offset: 0x000CC1F8
	public GameObject attached
	{
		get
		{
			return this._attached;
		}
		protected set
		{
			if (value != this._attached)
			{
				if (value)
				{
					if (!this.VerifyCompatible(value))
					{
						throw new ArgumentOutOfRangeException("value", "incompatible");
					}
					if (this._attached)
					{
						this.OnRemoveAttached();
					}
					this._attached = value;
					this.OnAddAttached();
					if (this._on)
					{
						this.EnableMod(global::ItemModRepresentation.Reason.Implicit);
					}
					else
					{
						this.DisableMod(global::ItemModRepresentation.Reason.Implicit);
					}
				}
				else
				{
					if (this._attached)
					{
						this.OnRemoveAttached();
					}
					this._attached = null;
				}
			}
			this._attached = value;
		}
	}

	// Token: 0x06003ABC RID: 15036 RVA: 0x000CE0A8 File Offset: 0x000CC2A8
	public virtual void SetAttached(GameObject attached, bool vm)
	{
		this.attached = attached;
	}

	// Token: 0x17000B6A RID: 2922
	// (get) Token: 0x06003ABD RID: 15037 RVA: 0x000CE0B4 File Offset: 0x000CC2B4
	// (set) Token: 0x06003ABE RID: 15038 RVA: 0x000CE0BC File Offset: 0x000CC2BC
	public bool on
	{
		get
		{
			return this._on;
		}
		protected set
		{
			this.SetOn(value, global::ItemModRepresentation.Reason.Explicit);
		}
	}

	// Token: 0x06003ABF RID: 15039 RVA: 0x000CE0C8 File Offset: 0x000CC2C8
	protected void SetOn(bool on, global::ItemModRepresentation.Reason reason)
	{
		if (this._on != on)
		{
			this._on = on;
			if (this._attached)
			{
				if (on)
				{
					this.EnableMod(reason);
				}
				else
				{
					this.DisableMod(reason);
				}
			}
		}
	}

	// Token: 0x06003AC0 RID: 15040 RVA: 0x000CE114 File Offset: 0x000CC314
	protected virtual bool VerifyCompatible(GameObject attachment)
	{
		return true;
	}

	// Token: 0x06003AC1 RID: 15041 RVA: 0x000CE118 File Offset: 0x000CC318
	protected virtual void OnAddAttached()
	{
	}

	// Token: 0x06003AC2 RID: 15042 RVA: 0x000CE11C File Offset: 0x000CC31C
	protected virtual void OnRemoveAttached()
	{
	}

	// Token: 0x06003AC3 RID: 15043
	protected abstract void DisableMod(global::ItemModRepresentation.Reason reason);

	// Token: 0x06003AC4 RID: 15044
	protected abstract void EnableMod(global::ItemModRepresentation.Reason reason);

	// Token: 0x04001CE0 RID: 7392
	private GameObject _attached;

	// Token: 0x04001CE1 RID: 7393
	protected readonly bool defaultsOn;

	// Token: 0x04001CE2 RID: 7394
	private bool _on;
}
