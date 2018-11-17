using System;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x020004F0 RID: 1264
[global::InterfaceDriverComponent(typeof(global::IActivatable), "_implementation", "implementation", SearchRoute = global::InterfaceSearchRoute.GameObject, UnityType = typeof(MonoBehaviour), AlwaysSaveDisabled = true)]
public sealed class Activatable : MonoBehaviour, global::IComponentInterfaceDriver<global::IActivatable, MonoBehaviour, global::Activatable>
{
	// Token: 0x17000980 RID: 2432
	// (get) Token: 0x06002B73 RID: 11123 RVA: 0x000A1AEC File Offset: 0x0009FCEC
	public MonoBehaviour implementor
	{
		get
		{
			if (!this._awoke)
			{
				try
				{
					this.Refresh();
				}
				finally
				{
					this._awoke = true;
				}
			}
			return this.implementation;
		}
	}

	// Token: 0x17000981 RID: 2433
	// (get) Token: 0x06002B74 RID: 11124 RVA: 0x000A1B3C File Offset: 0x0009FD3C
	public global::IActivatable @interface
	{
		get
		{
			if (!this._awoke)
			{
				try
				{
					this.Refresh();
				}
				finally
				{
					this._awoke = true;
				}
			}
			return this.act;
		}
	}

	// Token: 0x17000982 RID: 2434
	// (get) Token: 0x06002B75 RID: 11125 RVA: 0x000A1B8C File Offset: 0x0009FD8C
	public bool exists
	{
		get
		{
			return this._implemented && (this._implemented = this.implementation);
		}
	}

	// Token: 0x17000983 RID: 2435
	// (get) Token: 0x06002B76 RID: 11126 RVA: 0x000A1BBC File Offset: 0x0009FDBC
	public global::Activatable driver
	{
		get
		{
			return this;
		}
	}

	// Token: 0x17000984 RID: 2436
	// (get) Token: 0x06002B77 RID: 11127 RVA: 0x000A1BC0 File Offset: 0x0009FDC0
	public global::ActivationToggleState toggleState
	{
		get
		{
			return (!this.canToggle || !this.implementation) ? global::ActivationToggleState.Unspecified : this.actToggle.ActGetToggleState();
		}
	}

	// Token: 0x17000985 RID: 2437
	// (get) Token: 0x06002B78 RID: 11128 RVA: 0x000A1BFC File Offset: 0x0009FDFC
	public bool isToggle
	{
		get
		{
			return this.canToggle;
		}
	}

	// Token: 0x06002B79 RID: 11129 RVA: 0x000A1C04 File Offset: 0x0009FE04
	private void Refresh()
	{
		this.implementation = this._implementation;
		this._implementation = null;
		this.act = (this.implementation as global::IActivatable);
		this.canAct = (this.act != null);
		if (this.canAct)
		{
			this.actToggle = (this.implementation as global::IActivatableToggle);
			this.canToggle = (this.actToggle != null);
			global::IActivatableFill activatableFill = this.implementation as global::IActivatableFill;
			if (activatableFill != null)
			{
				activatableFill.ActivatableChanged(this, true);
			}
			global::IActivatableInfo activatableInfo = this.implementation as global::IActivatableInfo;
			if (activatableInfo != null)
			{
				activatableInfo.ActInfo(out this.info);
			}
		}
		else
		{
			Debug.LogWarning("implementation is null or does not implement IActivatable", this);
		}
	}

	// Token: 0x06002B7A RID: 11130 RVA: 0x000A1CC0 File Offset: 0x0009FEC0
	private void Awake()
	{
		if (!this._awoke)
		{
			try
			{
				this.Refresh();
			}
			finally
			{
				this._awoke = true;
			}
		}
	}

	// Token: 0x06002B7B RID: 11131 RVA: 0x000A1D08 File Offset: 0x0009FF08
	private void OnDestroy()
	{
		if (this.implementation)
		{
			global::IActivatableFill activatableFill = this.implementation as global::IActivatableFill;
			if (activatableFill != null)
			{
				activatableFill.ActivatableChanged(this, false);
			}
		}
		this.implementation = null;
		this.canAct = false;
		this.canToggle = false;
		this.act = null;
		this.actToggle = null;
		this.info = default(global::ActivatableInfo);
	}

	// Token: 0x06002B7C RID: 11132 RVA: 0x000A1D74 File Offset: 0x0009FF74
	public global::ActivationResult Activate(ulong timestamp)
	{
		return this.Activate(null, timestamp);
	}

	// Token: 0x06002B7D RID: 11133 RVA: 0x000A1D80 File Offset: 0x0009FF80
	public global::ActivationResult Activate(global::Character instigator, ulong timestamp)
	{
		throw new NotSupportedException("Server only");
	}

	// Token: 0x06002B7E RID: 11134 RVA: 0x000A1D8C File Offset: 0x0009FF8C
	public global::ActivationResult Activate()
	{
		return this.Activate(null, global::NetCull.timeInMillis);
	}

	// Token: 0x06002B7F RID: 11135 RVA: 0x000A1D9C File Offset: 0x0009FF9C
	private global::ActivationResult Act(global::Character instigator, ulong timestamp)
	{
		return (!this.canAct) ? global::ActivationResult.Error_Implementation : ((!this.implementation) ? global::ActivationResult.Error_Destroyed : this.act.ActTrigger(instigator, timestamp));
	}

	// Token: 0x06002B80 RID: 11136 RVA: 0x000A1DE0 File Offset: 0x0009FFE0
	private global::ActivationResult Act(global::Character instigator, global::ActivationToggleState state, ulong timestamp)
	{
		return (!this.canToggle) ? global::ActivationResult.Error_Implementation : ((!this.implementation) ? global::ActivationResult.Error_Destroyed : this.actToggle.ActTrigger(instigator, state, timestamp));
	}

	// Token: 0x06002B81 RID: 11137 RVA: 0x000A1E18 File Offset: 0x000A0018
	public global::ActivationResult Activate(bool on, global::Character instigator, ulong timestamp)
	{
		throw new NotSupportedException("Server only");
	}

	// Token: 0x06002B82 RID: 11138 RVA: 0x000A1E24 File Offset: 0x000A0024
	public global::ActivationResult Activate(bool on, global::Character instigator)
	{
		return this.Activate(on, instigator, global::NetCull.timeInMillis);
	}

	// Token: 0x06002B83 RID: 11139 RVA: 0x000A1E34 File Offset: 0x000A0034
	public global::ActivationResult Activate(bool on, ulong timestamp)
	{
		return this.Activate(on, null, timestamp);
	}

	// Token: 0x06002B84 RID: 11140 RVA: 0x000A1E40 File Offset: 0x000A0040
	public global::ActivationResult Activate(bool on)
	{
		return this.Activate(on, null, global::NetCull.timeInMillis);
	}

	// Token: 0x06002B85 RID: 11141 RVA: 0x000A1E50 File Offset: 0x000A0050
	private global::ActivationResult ActRoute(bool? on, global::Character character, ulong timestamp)
	{
		if (on != null)
		{
			return this.Activate(on.Value, character, timestamp);
		}
		return this.Activate(character, timestamp);
	}

	// Token: 0x06002B86 RID: 11142 RVA: 0x000A1E84 File Offset: 0x000A0084
	private global::ActivationResult ActRoute(bool? on, global::Controllable controllable, ulong timestamp)
	{
		return this.ActRoute(on, (!controllable) ? null : controllable.GetComponent<global::Character>(), timestamp);
	}

	// Token: 0x06002B87 RID: 11143 RVA: 0x000A1EB0 File Offset: 0x000A00B0
	private global::ActivationResult ActRoute(bool? on, global::PlayerClient sender, ulong timestamp)
	{
		return this.ActRoute(on, (!sender || !sender.controllable) ? null : sender.controllable, timestamp);
	}

	// Token: 0x06002B88 RID: 11144 RVA: 0x000A1EEC File Offset: 0x000A00EC
	private global::ActivationResult ActRoute(bool? on, uLink.NetworkPlayer sender, ulong timestamp)
	{
		global::ServerManagement serverManagement = global::ServerManagement.Get();
		global::PlayerClient sender2;
		if (serverManagement)
		{
			serverManagement.GetPlayerClient(sender, out sender2);
		}
		else
		{
			sender2 = null;
		}
		return this.ActRoute(on, sender2, timestamp);
	}

	// Token: 0x06002B89 RID: 11145 RVA: 0x000A1F24 File Offset: 0x000A0124
	public global::ActivationResult Activate(ref uLink.NetworkMessageInfo info)
	{
		return this.ActRoute(null, info.sender, info.timestampInMillis);
	}

	// Token: 0x06002B8A RID: 11146 RVA: 0x000A1F50 File Offset: 0x000A0150
	public global::ActivationResult Activate(bool on, ref uLink.NetworkMessageInfo info)
	{
		return this.ActRoute(new bool?(on), info.sender, info.timestampInMillis);
	}

	// Token: 0x06002B8B RID: 11147 RVA: 0x000A1F6C File Offset: 0x000A016C
	private void Reset()
	{
		if (!this.canAct)
		{
			foreach (MonoBehaviour monoBehaviour in base.GetComponents<MonoBehaviour>())
			{
				if (monoBehaviour != this && monoBehaviour is global::IActivatable)
				{
					this._implementation = monoBehaviour;
					break;
				}
			}
		}
	}

	// Token: 0x0400154C RID: 5452
	[SerializeField]
	private MonoBehaviour _implementation;

	// Token: 0x0400154D RID: 5453
	private MonoBehaviour implementation;

	// Token: 0x0400154E RID: 5454
	private global::IActivatable act;

	// Token: 0x0400154F RID: 5455
	private bool canAct;

	// Token: 0x04001550 RID: 5456
	private global::IActivatableToggle actToggle;

	// Token: 0x04001551 RID: 5457
	private bool canToggle;

	// Token: 0x04001552 RID: 5458
	private global::ActivatableInfo info;

	// Token: 0x04001553 RID: 5459
	[NonSerialized]
	private bool _implemented;

	// Token: 0x04001554 RID: 5460
	[NonSerialized]
	private bool _awoke;
}
