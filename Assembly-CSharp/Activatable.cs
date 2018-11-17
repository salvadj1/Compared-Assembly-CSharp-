using System;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x0200043A RID: 1082
[InterfaceDriverComponent(typeof(IActivatable), "_implementation", "implementation", SearchRoute = InterfaceSearchRoute.GameObject, UnityType = typeof(MonoBehaviour), AlwaysSaveDisabled = true)]
public sealed class Activatable : MonoBehaviour, IComponentInterfaceDriver<IActivatable, MonoBehaviour, Activatable>
{
	// Token: 0x17000918 RID: 2328
	// (get) Token: 0x060027E3 RID: 10211 RVA: 0x0009BB6C File Offset: 0x00099D6C
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

	// Token: 0x17000919 RID: 2329
	// (get) Token: 0x060027E4 RID: 10212 RVA: 0x0009BBBC File Offset: 0x00099DBC
	public IActivatable @interface
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

	// Token: 0x1700091A RID: 2330
	// (get) Token: 0x060027E5 RID: 10213 RVA: 0x0009BC0C File Offset: 0x00099E0C
	public bool exists
	{
		get
		{
			return this._implemented && (this._implemented = this.implementation);
		}
	}

	// Token: 0x1700091B RID: 2331
	// (get) Token: 0x060027E6 RID: 10214 RVA: 0x0009BC3C File Offset: 0x00099E3C
	public Activatable driver
	{
		get
		{
			return this;
		}
	}

	// Token: 0x1700091C RID: 2332
	// (get) Token: 0x060027E7 RID: 10215 RVA: 0x0009BC40 File Offset: 0x00099E40
	public ActivationToggleState toggleState
	{
		get
		{
			return (!this.canToggle || !this.implementation) ? ActivationToggleState.Unspecified : this.actToggle.ActGetToggleState();
		}
	}

	// Token: 0x1700091D RID: 2333
	// (get) Token: 0x060027E8 RID: 10216 RVA: 0x0009BC7C File Offset: 0x00099E7C
	public bool isToggle
	{
		get
		{
			return this.canToggle;
		}
	}

	// Token: 0x060027E9 RID: 10217 RVA: 0x0009BC84 File Offset: 0x00099E84
	private void Refresh()
	{
		this.implementation = this._implementation;
		this._implementation = null;
		this.act = (this.implementation as IActivatable);
		this.canAct = (this.act != null);
		if (this.canAct)
		{
			this.actToggle = (this.implementation as IActivatableToggle);
			this.canToggle = (this.actToggle != null);
			IActivatableFill activatableFill = this.implementation as IActivatableFill;
			if (activatableFill != null)
			{
				activatableFill.ActivatableChanged(this, true);
			}
			IActivatableInfo activatableInfo = this.implementation as IActivatableInfo;
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

	// Token: 0x060027EA RID: 10218 RVA: 0x0009BD40 File Offset: 0x00099F40
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

	// Token: 0x060027EB RID: 10219 RVA: 0x0009BD88 File Offset: 0x00099F88
	private void OnDestroy()
	{
		if (this.implementation)
		{
			IActivatableFill activatableFill = this.implementation as IActivatableFill;
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
		this.info = default(ActivatableInfo);
	}

	// Token: 0x060027EC RID: 10220 RVA: 0x0009BDF4 File Offset: 0x00099FF4
	public ActivationResult Activate(ulong timestamp)
	{
		return this.Activate(null, timestamp);
	}

	// Token: 0x060027ED RID: 10221 RVA: 0x0009BE00 File Offset: 0x0009A000
	public ActivationResult Activate(Character instigator, ulong timestamp)
	{
		throw new NotSupportedException("Server only");
	}

	// Token: 0x060027EE RID: 10222 RVA: 0x0009BE0C File Offset: 0x0009A00C
	public ActivationResult Activate()
	{
		return this.Activate(null, NetCull.timeInMillis);
	}

	// Token: 0x060027EF RID: 10223 RVA: 0x0009BE1C File Offset: 0x0009A01C
	private ActivationResult Act(Character instigator, ulong timestamp)
	{
		return (!this.canAct) ? ActivationResult.Error_Implementation : ((!this.implementation) ? ActivationResult.Error_Destroyed : this.act.ActTrigger(instigator, timestamp));
	}

	// Token: 0x060027F0 RID: 10224 RVA: 0x0009BE60 File Offset: 0x0009A060
	private ActivationResult Act(Character instigator, ActivationToggleState state, ulong timestamp)
	{
		return (!this.canToggle) ? ActivationResult.Error_Implementation : ((!this.implementation) ? ActivationResult.Error_Destroyed : this.actToggle.ActTrigger(instigator, state, timestamp));
	}

	// Token: 0x060027F1 RID: 10225 RVA: 0x0009BE98 File Offset: 0x0009A098
	public ActivationResult Activate(bool on, Character instigator, ulong timestamp)
	{
		throw new NotSupportedException("Server only");
	}

	// Token: 0x060027F2 RID: 10226 RVA: 0x0009BEA4 File Offset: 0x0009A0A4
	public ActivationResult Activate(bool on, Character instigator)
	{
		return this.Activate(on, instigator, NetCull.timeInMillis);
	}

	// Token: 0x060027F3 RID: 10227 RVA: 0x0009BEB4 File Offset: 0x0009A0B4
	public ActivationResult Activate(bool on, ulong timestamp)
	{
		return this.Activate(on, null, timestamp);
	}

	// Token: 0x060027F4 RID: 10228 RVA: 0x0009BEC0 File Offset: 0x0009A0C0
	public ActivationResult Activate(bool on)
	{
		return this.Activate(on, null, NetCull.timeInMillis);
	}

	// Token: 0x060027F5 RID: 10229 RVA: 0x0009BED0 File Offset: 0x0009A0D0
	private ActivationResult ActRoute(bool? on, Character character, ulong timestamp)
	{
		if (on != null)
		{
			return this.Activate(on.Value, character, timestamp);
		}
		return this.Activate(character, timestamp);
	}

	// Token: 0x060027F6 RID: 10230 RVA: 0x0009BF04 File Offset: 0x0009A104
	private ActivationResult ActRoute(bool? on, Controllable controllable, ulong timestamp)
	{
		return this.ActRoute(on, (!controllable) ? null : controllable.GetComponent<Character>(), timestamp);
	}

	// Token: 0x060027F7 RID: 10231 RVA: 0x0009BF30 File Offset: 0x0009A130
	private ActivationResult ActRoute(bool? on, PlayerClient sender, ulong timestamp)
	{
		return this.ActRoute(on, (!sender || !sender.controllable) ? null : sender.controllable, timestamp);
	}

	// Token: 0x060027F8 RID: 10232 RVA: 0x0009BF6C File Offset: 0x0009A16C
	private ActivationResult ActRoute(bool? on, NetworkPlayer sender, ulong timestamp)
	{
		ServerManagement serverManagement = ServerManagement.Get();
		PlayerClient sender2;
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

	// Token: 0x060027F9 RID: 10233 RVA: 0x0009BFA4 File Offset: 0x0009A1A4
	public ActivationResult Activate(ref NetworkMessageInfo info)
	{
		return this.ActRoute(null, info.sender, info.timestampInMillis);
	}

	// Token: 0x060027FA RID: 10234 RVA: 0x0009BFD0 File Offset: 0x0009A1D0
	public ActivationResult Activate(bool on, ref NetworkMessageInfo info)
	{
		return this.ActRoute(new bool?(on), info.sender, info.timestampInMillis);
	}

	// Token: 0x060027FB RID: 10235 RVA: 0x0009BFEC File Offset: 0x0009A1EC
	private void Reset()
	{
		if (!this.canAct)
		{
			foreach (MonoBehaviour monoBehaviour in base.GetComponents<MonoBehaviour>())
			{
				if (monoBehaviour != this && monoBehaviour is IActivatable)
				{
					this._implementation = monoBehaviour;
					break;
				}
			}
		}
	}

	// Token: 0x040013C9 RID: 5065
	[SerializeField]
	private MonoBehaviour _implementation;

	// Token: 0x040013CA RID: 5066
	private MonoBehaviour implementation;

	// Token: 0x040013CB RID: 5067
	private IActivatable act;

	// Token: 0x040013CC RID: 5068
	private bool canAct;

	// Token: 0x040013CD RID: 5069
	private IActivatableToggle actToggle;

	// Token: 0x040013CE RID: 5070
	private bool canToggle;

	// Token: 0x040013CF RID: 5071
	private ActivatableInfo info;

	// Token: 0x040013D0 RID: 5072
	[NonSerialized]
	private bool _implemented;

	// Token: 0x040013D1 RID: 5073
	[NonSerialized]
	private bool _awoke;
}
