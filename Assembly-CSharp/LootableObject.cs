using System;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x02000727 RID: 1831
[RequireComponent(typeof(global::Inventory))]
[global::NGCAutoAddScript]
public class LootableObject : IDLocal, global::IUseable, global::IContextRequestable, global::IContextRequestableQuick, global::IContextRequestableText, global::IContextRequestablePointText, global::IComponentInterface<global::IUseable, MonoBehaviour, global::Useable>, global::IComponentInterface<global::IUseable, MonoBehaviour>, global::IComponentInterface<global::IUseable>, global::IComponentInterface<global::IContextRequestable, MonoBehaviour, global::Contextual>, global::IComponentInterface<global::IContextRequestable, MonoBehaviour>, global::IComponentInterface<global::IContextRequestable>
{
	// Token: 0x17000BA3 RID: 2979
	// (get) Token: 0x06003CD5 RID: 15573 RVA: 0x000D9B4C File Offset: 0x000D7D4C
	// (set) Token: 0x06003CD6 RID: 15574 RVA: 0x000D9B5C File Offset: 0x000D7D5C
	public global::LootSpawnList _spawnList
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

	// Token: 0x06003CD7 RID: 15575 RVA: 0x000D9B6C File Offset: 0x000D7D6C
	public void OnUseEnter(global::Useable use)
	{
	}

	// Token: 0x06003CD8 RID: 15576 RVA: 0x000D9B70 File Offset: 0x000D7D70
	public void OnUseExit(global::Useable use, global::UseExitReason reason)
	{
	}

	// Token: 0x06003CD9 RID: 15577 RVA: 0x000D9B74 File Offset: 0x000D7D74
	[RPC]
	protected void SetLooter(uLink.NetworkPlayer ply)
	{
		this.occupierText = null;
		if (ply == uLink.NetworkPlayer.unassigned)
		{
			this.ClearLooter();
		}
		else
		{
			if (ply == global::NetCull.player)
			{
				if (!this.thisClientIsInWindow)
				{
					try
					{
						this._currentlyUsingPlayer = ply;
						global::RPOS.OpenLootWindow(this);
						this.thisClientIsInWindow = true;
					}
					catch (Exception ex)
					{
						Debug.LogError(ex, this);
						global::NetCull.RPC(this, "StopLooting", 0);
						this.thisClientIsInWindow = false;
						ply = uLink.NetworkPlayer.unassigned;
					}
				}
			}
			else if (this._currentlyUsingPlayer == global::NetCull.player && global::NetCull.player != uLink.NetworkPlayer.unassigned)
			{
				this.ClearLooter();
			}
			this._currentlyUsingPlayer = ply;
		}
	}

	// Token: 0x06003CDA RID: 15578 RVA: 0x000D9C58 File Offset: 0x000D7E58
	[RPC]
	protected void ClearLooter()
	{
		this.occupierText = null;
		this._currentlyUsingPlayer = uLink.NetworkPlayer.unassigned;
		if (this.thisClientIsInWindow)
		{
			try
			{
				global::RPOS.CloseLootWindow();
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

	// Token: 0x06003CDB RID: 15579 RVA: 0x000D9CD4 File Offset: 0x000D7ED4
	[RPC]
	protected void TakeAll()
	{
	}

	// Token: 0x06003CDC RID: 15580 RVA: 0x000D9CD8 File Offset: 0x000D7ED8
	[RPC]
	protected void StopLooting(uLink.NetworkMessageInfo info)
	{
		if (this._currentlyUsingPlayer == info.sender)
		{
			this._useable.Eject();
		}
	}

	// Token: 0x06003CDD RID: 15581 RVA: 0x000D9D08 File Offset: 0x000D7F08
	public void ClientClosedLootWindow()
	{
		try
		{
			if (this.IsLocalLooting())
			{
				global::NetCull.RPC(this, "StopLooting", 0);
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

	// Token: 0x06003CDE RID: 15582 RVA: 0x000D9D64 File Offset: 0x000D7F64
	protected virtual global::ContextResponse ContextRespond_OpenLoot(global::Controllable controllable, ulong timestamp)
	{
		return global::ContextRequestable.UseableForwardFromContextRespond(this, controllable, this._useable);
	}

	// Token: 0x06003CDF RID: 15583 RVA: 0x000D9D74 File Offset: 0x000D7F74
	public virtual string ContextText(global::Controllable localControllable)
	{
		if (this._currentlyUsingPlayer == uLink.NetworkPlayer.unassigned)
		{
			return "Search";
		}
		if (this.occupierText == null)
		{
			global::PlayerClient playerClient;
			if (!global::PlayerClient.Find(this._currentlyUsingPlayer, out playerClient))
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

	// Token: 0x06003CE0 RID: 15584 RVA: 0x000D9DE8 File Offset: 0x000D7FE8
	protected void OnDestroy()
	{
		global::UseableUtility.OnDestroy(this, this._useable);
	}

	// Token: 0x06003CE1 RID: 15585 RVA: 0x000D9DF8 File Offset: 0x000D7FF8
	public bool IsLocalLooting()
	{
		return this.thisClientIsInWindow || (this._currentlyUsingPlayer == global::NetCull.player && this._currentlyUsingPlayer != uLink.NetworkPlayer.unassigned);
	}

	// Token: 0x06003CE2 RID: 15586 RVA: 0x000D9E3C File Offset: 0x000D803C
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

	// Token: 0x06003CE3 RID: 15587 RVA: 0x000D9E80 File Offset: 0x000D8080
	public void RadialCheck()
	{
		if (this._useable.user && Vector3.Distance(this._useable.user.transform.position, base.transform.position) > 5f)
		{
			this._useable.Eject();
			base.CancelInvoke("RadialCheck");
		}
	}

	// Token: 0x06003CE4 RID: 15588 RVA: 0x000D9EE8 File Offset: 0x000D80E8
	public virtual bool ContextTextPoint(out Vector3 worldPoint)
	{
		if (global::ContextRequestable.PointUtil.SpriteOrOrigin(base.transform, out worldPoint))
		{
			worldPoint.y += 0.15f;
			return true;
		}
		return true;
	}

	// Token: 0x04001F00 RID: 7936
	private const string kAnimation_OpenIdle = "opened idle";

	// Token: 0x04001F01 RID: 7937
	private const string kAnimation_Open = "open";

	// Token: 0x04001F02 RID: 7938
	private const string kAnimation_CloseIdle = "closed idle";

	// Token: 0x04001F03 RID: 7939
	private const string kAnimation_Close = "close";

	// Token: 0x04001F04 RID: 7940
	[SerializeField]
	private global::LootSpawnListReference _lootSpawnListName;

	// Token: 0x04001F05 RID: 7941
	public float LootCycle;

	// Token: 0x04001F06 RID: 7942
	public float lifeTime;

	// Token: 0x04001F07 RID: 7943
	[PrefetchComponent]
	public global::Inventory _inventory;

	// Token: 0x04001F08 RID: 7944
	private global::Useable _useable;

	// Token: 0x04001F09 RID: 7945
	protected uLink.NetworkPlayer _currentlyUsingPlayer;

	// Token: 0x04001F0A RID: 7946
	public bool destroyOnEmpty;

	// Token: 0x04001F0B RID: 7947
	public int NumberOfSlots = 12;

	// Token: 0x04001F0C RID: 7948
	public bool lateSized;

	// Token: 0x04001F0D RID: 7949
	[NonSerialized]
	public bool accessLocked;

	// Token: 0x04001F0E RID: 7950
	public global::RPOSLootWindow lootWindowOverride;

	// Token: 0x04001F0F RID: 7951
	private bool thisClientIsInWindow;

	// Token: 0x04001F10 RID: 7952
	protected string occupierText;

	// Token: 0x04001F11 RID: 7953
	private bool sentSetLooter;

	// Token: 0x04001F12 RID: 7954
	private uLink.NetworkPlayer sentLooter;
}
