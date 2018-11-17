using System;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x02000663 RID: 1635
[RequireComponent(typeof(Inventory))]
[NGCAutoAddScript]
public class LootableObject : IDLocal, IUseable, IContextRequestable, IContextRequestableQuick, IContextRequestableText, IContextRequestablePointText, IComponentInterface<IUseable, MonoBehaviour, Useable>, IComponentInterface<IUseable, MonoBehaviour>, IComponentInterface<IUseable>, IComponentInterface<IContextRequestable, MonoBehaviour, Contextual>, IComponentInterface<IContextRequestable, MonoBehaviour>, IComponentInterface<IContextRequestable>
{
	// Token: 0x17000B21 RID: 2849
	// (get) Token: 0x060038E1 RID: 14561 RVA: 0x000D116C File Offset: 0x000CF36C
	// (set) Token: 0x060038E2 RID: 14562 RVA: 0x000D117C File Offset: 0x000CF37C
	public LootSpawnList _spawnList
	{
		get
		{
			return this._lootSpawnListName.list;
		}
		set
		{
			this._lootSpawnListName.list = value;
		}
	}

	// Token: 0x060038E3 RID: 14563 RVA: 0x000D118C File Offset: 0x000CF38C
	public void OnUseEnter(Useable use)
	{
	}

	// Token: 0x060038E4 RID: 14564 RVA: 0x000D1190 File Offset: 0x000CF390
	public void OnUseExit(Useable use, UseExitReason reason)
	{
	}

	// Token: 0x060038E5 RID: 14565 RVA: 0x000D1194 File Offset: 0x000CF394
	[RPC]
	protected void SetLooter(NetworkPlayer ply)
	{
		this.occupierText = null;
		if (ply == NetworkPlayer.unassigned)
		{
			this.ClearLooter();
		}
		else
		{
			if (ply == NetCull.player)
			{
				if (!this.thisClientIsInWindow)
				{
					try
					{
						this._currentlyUsingPlayer = ply;
						RPOS.OpenLootWindow(this);
						this.thisClientIsInWindow = true;
					}
					catch (Exception ex)
					{
						Debug.LogError(ex, this);
						NetCull.RPC(this, "StopLooting", 0);
						this.thisClientIsInWindow = false;
						ply = NetworkPlayer.unassigned;
					}
				}
			}
			else if (this._currentlyUsingPlayer == NetCull.player && NetCull.player != NetworkPlayer.unassigned)
			{
				this.ClearLooter();
			}
			this._currentlyUsingPlayer = ply;
		}
	}

	// Token: 0x060038E6 RID: 14566 RVA: 0x000D1278 File Offset: 0x000CF478
	[RPC]
	protected void ClearLooter()
	{
		this.occupierText = null;
		this._currentlyUsingPlayer = NetworkPlayer.unassigned;
		if (this.thisClientIsInWindow)
		{
			try
			{
				RPOS.CloseLootWindow();
			}
			catch (Exception ex)
			{
				Debug.LogError(ex);
			}
			finally
			{
				this.thisClientIsInWindow = false;
			}
		}
	}

	// Token: 0x060038E7 RID: 14567 RVA: 0x000D12F4 File Offset: 0x000CF4F4
	[RPC]
	protected void TakeAll()
	{
	}

	// Token: 0x060038E8 RID: 14568 RVA: 0x000D12F8 File Offset: 0x000CF4F8
	[RPC]
	protected void StopLooting(NetworkMessageInfo info)
	{
		if (this._currentlyUsingPlayer == info.sender)
		{
			this._useable.Eject();
		}
	}

	// Token: 0x060038E9 RID: 14569 RVA: 0x000D1328 File Offset: 0x000CF528
	public void ClientClosedLootWindow()
	{
		try
		{
			if (this.IsLocalLooting())
			{
				NetCull.RPC(this, "StopLooting", 0);
			}
		}
		finally
		{
			if (this.thisClientIsInWindow)
			{
				this.thisClientIsInWindow = false;
			}
		}
	}

	// Token: 0x060038EA RID: 14570 RVA: 0x000D1384 File Offset: 0x000CF584
	protected virtual ContextResponse ContextRespond_OpenLoot(Controllable controllable, ulong timestamp)
	{
		return ContextRequestable.UseableForwardFromContextRespond(this, controllable, this._useable);
	}

	// Token: 0x060038EB RID: 14571 RVA: 0x000D1394 File Offset: 0x000CF594
	public virtual string ContextText(Controllable localControllable)
	{
		if (this._currentlyUsingPlayer == NetworkPlayer.unassigned)
		{
			return "Search";
		}
		if (this.occupierText == null)
		{
			PlayerClient playerClient;
			if (!PlayerClient.Find(this._currentlyUsingPlayer, out playerClient))
			{
				this.occupierText = "Occupied";
			}
			else
			{
				this.occupierText = string.Format("Occupied by {0}", playerClient.userName);
			}
		}
		return this.occupierText;
	}

	// Token: 0x060038EC RID: 14572 RVA: 0x000D1408 File Offset: 0x000CF608
	protected void OnDestroy()
	{
		UseableUtility.OnDestroy(this, this._useable);
	}

	// Token: 0x060038ED RID: 14573 RVA: 0x000D1418 File Offset: 0x000CF618
	public bool IsLocalLooting()
	{
		return this.thisClientIsInWindow || (this._currentlyUsingPlayer == NetCull.player && this._currentlyUsingPlayer != NetworkPlayer.unassigned);
	}

	// Token: 0x060038EE RID: 14574 RVA: 0x000D145C File Offset: 0x000CF65C
	public void CancelInvokes()
	{
		if (this.LootCycle > 0f)
		{
			base.CancelInvoke("TryAddLoot");
		}
		if (this.lifeTime > 0f)
		{
			base.CancelInvoke("DelayedDestroy");
		}
	}

	// Token: 0x060038EF RID: 14575 RVA: 0x000D14A0 File Offset: 0x000CF6A0
	public void RadialCheck()
	{
		if (this._useable.user && Vector3.Distance(this._useable.user.transform.position, base.transform.position) > 5f)
		{
			this._useable.Eject();
			base.CancelInvoke("RadialCheck");
		}
	}

	// Token: 0x060038F0 RID: 14576 RVA: 0x000D1508 File Offset: 0x000CF708
	public virtual bool ContextTextPoint(out Vector3 worldPoint)
	{
		if (ContextRequestable.PointUtil.SpriteOrOrigin(base.transform, out worldPoint))
		{
			worldPoint.y += 0.15f;
			return true;
		}
		return true;
	}

	// Token: 0x04001D08 RID: 7432
	private const string kAnimation_OpenIdle = "opened idle";

	// Token: 0x04001D09 RID: 7433
	private const string kAnimation_Open = "open";

	// Token: 0x04001D0A RID: 7434
	private const string kAnimation_CloseIdle = "closed idle";

	// Token: 0x04001D0B RID: 7435
	private const string kAnimation_Close = "close";

	// Token: 0x04001D0C RID: 7436
	[SerializeField]
	private LootSpawnListReference _lootSpawnListName;

	// Token: 0x04001D0D RID: 7437
	public float LootCycle;

	// Token: 0x04001D0E RID: 7438
	public float lifeTime;

	// Token: 0x04001D0F RID: 7439
	[PrefetchComponent]
	public Inventory _inventory;

	// Token: 0x04001D10 RID: 7440
	private Useable _useable;

	// Token: 0x04001D11 RID: 7441
	protected NetworkPlayer _currentlyUsingPlayer;

	// Token: 0x04001D12 RID: 7442
	public bool destroyOnEmpty;

	// Token: 0x04001D13 RID: 7443
	public int NumberOfSlots = 12;

	// Token: 0x04001D14 RID: 7444
	public bool lateSized;

	// Token: 0x04001D15 RID: 7445
	[NonSerialized]
	public bool accessLocked;

	// Token: 0x04001D16 RID: 7446
	public RPOSLootWindow lootWindowOverride;

	// Token: 0x04001D17 RID: 7447
	private bool thisClientIsInWindow;

	// Token: 0x04001D18 RID: 7448
	protected string occupierText;

	// Token: 0x04001D19 RID: 7449
	private bool sentSetLooter;

	// Token: 0x04001D1A RID: 7450
	private NetworkPlayer sentLooter;
}
