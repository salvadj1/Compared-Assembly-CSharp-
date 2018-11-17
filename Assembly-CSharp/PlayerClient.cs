using System;
using System.Collections.Generic;
using Rust;
using uLink;
using UnityEngine;

// Token: 0x020003BE RID: 958
[RequireComponent(typeof(uLinkNetworkView))]
public class PlayerClient : IDMain
{
	// Token: 0x06002195 RID: 8597 RVA: 0x0007BB00 File Offset: 0x00079D00
	public PlayerClient() : base(0)
	{
	}

	// Token: 0x06002196 RID: 8598 RVA: 0x0007BB14 File Offset: 0x00079D14
	public static global::PlayerClient GetLocalPlayer()
	{
		return global::PlayerClient.localPlayerClient;
	}

	// Token: 0x170007EF RID: 2031
	// (get) Token: 0x06002197 RID: 8599 RVA: 0x0007BB1C File Offset: 0x00079D1C
	public global::Controllable controllable
	{
		get
		{
			return this._controllable;
		}
	}

	// Token: 0x170007F0 RID: 2032
	// (get) Token: 0x06002198 RID: 8600 RVA: 0x0007BB24 File Offset: 0x00079D24
	public double instantiationTimeStamp
	{
		get
		{
			return this.instantiationinfo.timestamp;
		}
	}

	// Token: 0x170007F1 RID: 2033
	// (get) Token: 0x06002199 RID: 8601 RVA: 0x0007BB34 File Offset: 0x00079D34
	public global::Controllable rootControllable
	{
		get
		{
			return this._controllable;
		}
	}

	// Token: 0x170007F2 RID: 2034
	// (get) Token: 0x0600219A RID: 8602 RVA: 0x0007BB3C File Offset: 0x00079D3C
	public global::Controllable topControllable
	{
		get
		{
			return this._controllable;
		}
	}

	// Token: 0x170007F3 RID: 2035
	// (get) Token: 0x0600219B RID: 8603 RVA: 0x0007BB44 File Offset: 0x00079D44
	public bool local
	{
		get
		{
			return global::PlayerClient.localPlayerClient && global::PlayerClient.localPlayerClient == this;
		}
	}

	// Token: 0x0600219C RID: 8604 RVA: 0x0007BB64 File Offset: 0x00079D64
	private void Awake()
	{
		uLink.NetworkPlayer unassigned = uLink.NetworkPlayer.unassigned;
		this._playerID = unassigned.id;
	}

	// Token: 0x0600219D RID: 8605 RVA: 0x0007BB84 File Offset: 0x00079D84
	private void uLink_OnNetworkInstantiate(uLink.NetworkMessageInfo info)
	{
		this.netPlayer = info.networkView.owner;
		BitStream initialData = info.networkView.initialData;
		this.userID = initialData.ReadUInt64();
		this.userName = initialData.ReadString();
		base.name = string.Concat(new string[]
		{
			"Player ",
			this.userName,
			" (",
			this.userID.ToString(),
			")"
		});
		this.instantiationinfo = info;
		this._playerID = this.netPlayer.id;
		global::PlayerClient.g.playerIDDict[this._playerID] = this;
		global::PlayerClient.g.userIDDict[this.userID] = this;
		this.boundUserID = true;
		if (!global::PlayerClient.localPlayerClient && base.networkView.isMine)
		{
			global::PlayerClient.localPlayerClient = this;
			base.enabled = true;
			this.nextAutoReclockTime = global::NetCull.localTimeInMillis + 8000UL;
		}
		else
		{
			base.enabled = false;
		}
	}

	// Token: 0x0600219E RID: 8606 RVA: 0x0007BC94 File Offset: 0x00079E94
	private void OnDisable()
	{
		if (this.local && !base.destroying && !global::NetInstance.IsCurrentlyDestroying(this))
		{
			Debug.LogWarning("The local player got disabled", this);
		}
	}

	// Token: 0x0600219F RID: 8607 RVA: 0x0007BCD0 File Offset: 0x00079ED0
	private void OnEnable()
	{
		if (!this.local)
		{
			Debug.LogWarning("Something tried to enable a non local player.. setting enabled to false", this);
			base.enabled = false;
		}
	}

	// Token: 0x060021A0 RID: 8608 RVA: 0x0007BCF0 File Offset: 0x00079EF0
	protected void OnDestroy()
	{
		try
		{
			uLink.NetworkPlayer unassigned = uLink.NetworkPlayer.unassigned;
			int id = unassigned.id;
			if (this._playerID != id)
			{
				try
				{
					global::PlayerClient objA = global::PlayerClient.g.playerIDDict[this._playerID];
					if (object.ReferenceEquals(objA, this))
					{
						global::PlayerClient.g.playerIDDict.Remove(this._playerID);
					}
				}
				catch (Exception ex)
				{
					Debug.LogException(ex, this);
				}
				finally
				{
					this._playerID = id;
				}
			}
			if (this.boundUserID)
			{
				try
				{
					global::PlayerClient objA2 = global::PlayerClient.g.userIDDict[this.userID];
					if (object.ReferenceEquals(objA2, this))
					{
						global::PlayerClient.g.userIDDict.Remove(this.userID);
					}
				}
				catch (Exception ex2)
				{
					Debug.LogException(ex2, this);
				}
				finally
				{
					this.boundUserID = false;
				}
			}
			if (global::PlayerClient.localPlayerClient == this)
			{
				global::PlayerClient.localPlayerClient = null;
			}
		}
		finally
		{
			base.OnDestroy();
		}
	}

	// Token: 0x060021A1 RID: 8609 RVA: 0x0007BE54 File Offset: 0x0007A054
	public static void InputFunction(GameObject req)
	{
		if (req && global::PlayerClient.localPlayerClient && global::PlayerClient.localPlayerClient._controllable && global::PlayerClient.localPlayerClient._controllable.gameObject == req && global::PlayerClient.localPlayerClient.lastInputFrame != Time.frameCount)
		{
			global::PlayerClient.localPlayerClient.lastInputFrame = Time.frameCount;
			global::PlayerClient.localPlayerClient.ClientInput();
		}
	}

	// Token: 0x060021A2 RID: 8610 RVA: 0x0007BED8 File Offset: 0x0007A0D8
	protected virtual void ClientInput()
	{
	}

	// Token: 0x060021A3 RID: 8611 RVA: 0x0007BEDC File Offset: 0x0007A0DC
	private void Update()
	{
		if (this.lastInputFrame != Time.frameCount && (!this._controllable || !this._controllable.masterControllable.forwardsPlayerClientInput))
		{
			this.lastInputFrame = Time.frameCount;
			this.ClientInput();
		}
		if (global::NetCull.isClientRunning && !Rust.Globals.isLoading)
		{
			ulong num = global::NetCull.localTimeInMillis;
			if (num >= this.nextAutoReclockTime)
			{
				try
				{
					ulong num2 = Math.Min(num - this.nextAutoReclockTime, 500UL);
					global::NetCull.ResynchronizeClock((3000UL + num2) / 1000.0);
					num += num2;
				}
				finally
				{
					this.nextAutoReclockTime = num + 420000UL;
				}
			}
		}
	}

	// Token: 0x060021A4 RID: 8612 RVA: 0x0007BFB8 File Offset: 0x0007A1B8
	internal void OnRootControllableEntered(global::Controllable controllable)
	{
		if (this._controllable)
		{
			Debug.LogWarning("There was a controllable for player client already", this);
		}
		this._controllable = controllable;
	}

	// Token: 0x060021A5 RID: 8613 RVA: 0x0007BFE8 File Offset: 0x0007A1E8
	internal void OnRootControllableExited(global::Controllable controllable)
	{
		if (this._controllable != controllable)
		{
			Debug.LogWarning("The controllable exited did not match that of the existing value", this);
		}
		else
		{
			this._controllable = null;
		}
	}

	// Token: 0x060021A6 RID: 8614 RVA: 0x0007C020 File Offset: 0x0007A220
	public static bool Find(uLink.NetworkPlayer player, out global::PlayerClient pc)
	{
		int id = player.id;
		int num = id;
		uLink.NetworkPlayer unassigned = uLink.NetworkPlayer.unassigned;
		if (num == unassigned.id || player == uLink.NetworkPlayer.server)
		{
			pc = null;
			return false;
		}
		return global::PlayerClient.g.playerIDDict.TryGetValue(id, out pc);
	}

	// Token: 0x060021A7 RID: 8615 RVA: 0x0007C06C File Offset: 0x0007A26C
	public static bool Find(uLink.NetworkPlayer player, out global::PlayerClient pc, bool throwIfNotFound)
	{
		if (!throwIfNotFound)
		{
			return global::PlayerClient.Find(player, out pc);
		}
		if (!global::PlayerClient.Find(player, out pc))
		{
			throw new ArgumentException("There was no PlayerClient for that player", "player");
		}
		return true;
	}

	// Token: 0x060021A8 RID: 8616 RVA: 0x0007C09C File Offset: 0x0007A29C
	public static IEnumerable<global::PlayerClient> FindAllWithName(string name, StringComparison comparison)
	{
		global::ServerManagement serverManagement;
		if (!string.IsNullOrEmpty(name) && (serverManagement = global::ServerManagement.Get()))
		{
			return serverManagement.FindPlayerClientsByName(name, comparison);
		}
		return global::EmptyArray<global::PlayerClient>.emptyEnumerable;
	}

	// Token: 0x060021A9 RID: 8617 RVA: 0x0007C0D4 File Offset: 0x0007A2D4
	public static IEnumerable<global::PlayerClient> FindAllWithName(string name)
	{
		return global::PlayerClient.FindAllWithName(name, StringComparison.InvariantCultureIgnoreCase);
	}

	// Token: 0x060021AA RID: 8618 RVA: 0x0007C0E0 File Offset: 0x0007A2E0
	public static IEnumerable<global::PlayerClient> FindAllWithString(string partialNameOrIDInt)
	{
		global::ServerManagement serverManagement = global::ServerManagement.Get();
		if (serverManagement == null)
		{
			return global::EmptyArray<global::PlayerClient>.emptyEnumerable;
		}
		if (!string.IsNullOrEmpty(partialNameOrIDInt))
		{
			return serverManagement.FindPlayerClientsByString(partialNameOrIDInt);
		}
		return global::EmptyArray<global::PlayerClient>.emptyEnumerable;
	}

	// Token: 0x060021AB RID: 8619 RVA: 0x0007C120 File Offset: 0x0007A320
	public static bool FindByUserID(ulong userID, out global::PlayerClient client)
	{
		if (userID == 0UL)
		{
			client = null;
			return false;
		}
		return global::PlayerClient.g.userIDDict.TryGetValue(userID, out client);
	}

	// Token: 0x170007F4 RID: 2036
	// (get) Token: 0x060021AC RID: 8620 RVA: 0x0007C13C File Offset: 0x0007A33C
	public static global::LockedList<global::PlayerClient> All
	{
		get
		{
			global::ServerManagement serverManagement = global::ServerManagement.Get();
			if (serverManagement)
			{
				return serverManagement.lockedPlayerClientList;
			}
			return global::LockedList<global::PlayerClient>.Empty;
		}
	}

	// Token: 0x060021AD RID: 8621 RVA: 0x0007C168 File Offset: 0x0007A368
	public void ProcessLocalPlayerPreRender()
	{
		if (this._controllable)
		{
			this._controllable.masterControllable.ProcessLocalPlayerPreRender();
		}
	}

	// Token: 0x04000FD9 RID: 4057
	private const ulong kAutoReclockInitialDelay = 8000UL;

	// Token: 0x04000FDA RID: 4058
	private const ulong kAutoReclockInterval = 420000UL;

	// Token: 0x04000FDB RID: 4059
	private const ulong kAutoReclockMS_Base = 3000UL;

	// Token: 0x04000FDC RID: 4060
	private const ulong kAutoReclockMS_AddMax = 500UL;

	// Token: 0x04000FDD RID: 4061
	public static global::PlayerClient localPlayerClient;

	// Token: 0x04000FDE RID: 4062
	private global::Controllable _controllable;

	// Token: 0x04000FDF RID: 4063
	public uLink.NetworkPlayer netPlayer;

	// Token: 0x04000FE0 RID: 4064
	private uLink.NetworkMessageInfo instantiationinfo;

	// Token: 0x04000FE1 RID: 4065
	private int _playerID;

	// Token: 0x04000FE2 RID: 4066
	[NonSerialized]
	private bool boundUserID;

	// Token: 0x04000FE3 RID: 4067
	public ulong userID;

	// Token: 0x04000FE4 RID: 4068
	public string userName;

	// Token: 0x04000FE5 RID: 4069
	[NonSerialized]
	public bool firstReady;

	// Token: 0x04000FE6 RID: 4070
	private int lastInputFrame = int.MinValue;

	// Token: 0x04000FE7 RID: 4071
	private ulong nextAutoReclockTime;

	// Token: 0x020003BF RID: 959
	private static class g
	{
		// Token: 0x04000FE8 RID: 4072
		public static Dictionary<int, global::PlayerClient> playerIDDict = new Dictionary<int, global::PlayerClient>();

		// Token: 0x04000FE9 RID: 4073
		public static Dictionary<ulong, global::PlayerClient> userIDDict = new Dictionary<ulong, global::PlayerClient>();
	}
}
