using System;
using UnityEngine;

// Token: 0x020005FF RID: 1535
public abstract class WeaponModRep : ItemModRepresentation
{
	// Token: 0x060036E0 RID: 14048 RVA: 0x000C5A9C File Offset: 0x000C3C9C
	protected WeaponModRep(ItemModRepresentation.Caps caps, bool defaultsOn) : base(caps)
	{
		this.defaultsOn = defaultsOn;
		this._on = defaultsOn;
	}

	// Token: 0x060036E1 RID: 14049 RVA: 0x000C5AB4 File Offset: 0x000C3CB4
	protected WeaponModRep(ItemModRepresentation.Caps caps) : this(caps, false)
	{
	}

	// Token: 0x17000AEF RID: 2799
	// (get) Token: 0x060036E2 RID: 14050 RVA: 0x000C5AC0 File Offset: 0x000C3CC0
	// (set) Token: 0x060036E3 RID: 14051 RVA: 0x000C5AC8 File Offset: 0x000C3CC8
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
						this.EnableMod(ItemModRepresentation.Reason.Implicit);
					}
					else
					{
						this.DisableMod(ItemModRepresentation.Reason.Implicit);
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

	// Token: 0x060036E4 RID: 14052 RVA: 0x000C5B78 File Offset: 0x000C3D78
	public virtual void SetAttached(GameObject attached, bool vm)
	{
		this.attached = attached;
	}

	// Token: 0x17000AF0 RID: 2800
	// (get) Token: 0x060036E5 RID: 14053 RVA: 0x000C5B84 File Offset: 0x000C3D84
	// (set) Token: 0x060036E6 RID: 14054 RVA: 0x000C5B8C File Offset: 0x000C3D8C
	public bool on
	{
		get
		{
			return this._on;
		}
		protected set
		{
			this.SetOn(value, ItemModRepresentation.Reason.Explicit);
		}
	}

	// Token: 0x060036E7 RID: 14055 RVA: 0x000C5B98 File Offset: 0x000C3D98
	protected void SetOn(bool on, ItemModRepresentation.Reason reason)
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

	// Token: 0x060036E8 RID: 14056 RVA: 0x000C5BE4 File Offset: 0x000C3DE4
	protected virtual bool VerifyCompatible(GameObject attachment)
	{
		return true;
	}

	// Token: 0x060036E9 RID: 14057 RVA: 0x000C5BE8 File Offset: 0x000C3DE8
	protected virtual void OnAddAttached()
	{
	}

	// Token: 0x060036EA RID: 14058 RVA: 0x000C5BEC File Offset: 0x000C3DEC
	protected virtual void OnRemoveAttached()
	{
	}

	// Token: 0x060036EB RID: 14059
	protected abstract void DisableMod(ItemModRepresentation.Reason reason);

	// Token: 0x060036EC RID: 14060
	protected abstract void EnableMod(ItemModRepresentation.Reason reason);

	// Token: 0x04001AFA RID: 6906
	private GameObject _attached;

	// Token: 0x04001AFB RID: 6907
	protected readonly bool defaultsOn;

	// Token: 0x04001AFC RID: 6908
	private bool _on;
}
