using System;
using System.Collections.Generic;
using Rust;
using uLink;
using UnityEngine;

// Token: 0x02000315 RID: 789
[RequireComponent(typeof(uLinkNetworkView))]
public class PlayerClient : IDMain
{
	// Token: 0x06001E53 RID: 7763 RVA: 0x00077080 File Offset: 0x00075280
	public PlayerClient() : base(0)
	{
	}

	// Token: 0x06001E54 RID: 7764 RVA: 0x00077094 File Offset: 0x00075294
	public static PlayerClient GetLocalPlayer()
	{
		return PlayerClient.localPlayerClient;
	}

	// Token: 0x17000799 RID: 1945
	// (get) Token: 0x06001E55 RID: 7765 RVA: 0x0007709C File Offset: 0x0007529C
	public Controllable controllable
	{
		get
		{
			return this._controllable;
		}
	}

	// Token: 0x1700079A RID: 1946
	// (get) Token: 0x06001E56 RID: 7766 RVA: 0x000770A4 File Offset: 0x000752A4
	public double instantiationTimeStamp
	{
		get
		{
			return this.instantiationinfo.timestamp;
		}
	}

	// Token: 0x1700079B RID: 1947
	// (get) Token: 0x06001E57 RID: 7767 RVA: 0x000770B4 File Offset: 0x000752B4
	public Controllable rootControllable
	{
		get
		{
			return this._controllable;
		}
	}

	// Token: 0x1700079C RID: 1948
	// (get) Token: 0x06001E58 RID: 7768 RVA: 0x000770BC File Offset: 0x000752BC
	public Controllable topControllable
	{
		get
		{
			return this._controllable;
		}
	}

	// Token: 0x1700079D RID: 1949
	// (get) Token: 0x06001E59 RID: 7769 RVA: 0x000770C4 File Offset: 0x000752C4
	public bool local
	{
		get
		{
			return PlayerClient.localPlayerClient && PlayerClient.localPlayerClient == this;
		}
	}

	// Token: 0x06001E5A RID: 7770 RVA: 0x000770E4 File Offset: 0x000752E4
	private void Awake()
	{
		NetworkPlayer unassigned = NetworkPlayer.unassigned;
		this._playerID = unassigned.id;
	}

	// Token: 0x06001E5B RID: 7771 RVA: 0x00077104 File Offset: 0x00075304
	private void uLink_OnNetworkInstantiate(NetworkMessageInfo info)
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
		PlayerClient.g.playerIDDict[this._playerID] = this;
		PlayerClient.g.userIDDict[this.userID] = this;
		this.boundUserID = true;
		if (!PlayerClient.localPlayerClient && base.networkView.isMine)
		{
			PlayerClient.localPlayerClient = this;
			base.enabled = true;
			this.nextAutoReclockTime = NetCull.localTimeInMillis + 8000UL;
		}
		else
		{
			base.enabled = false;
		}
	}

	// Token: 0x06001E5C RID: 7772 RVA: 0x00077214 File Offset: 0x00075414
	private void OnDisable()
	{
		if (this.local && !base.destroying && !NetInstance.IsCurrentlyDestroying(this))
		{
			Debug.LogWarning("The local player got disabled", this);
		}
	}

	// Token: 0x06001E5D RID: 7773 RVA: 0x00077250 File Offset: 0x00075450
	private void OnEnable()
	{
		if (!this.local)
		{
			Debug.LogWarning("Something tried to enable a non local player.. setting enabled to false", this);
			base.enabled = false;
		}
	}

	// Token: 0x06001E5E RID: 7774 RVA: 0x00077270 File Offset: 0x00075470
	protected void OnDestroy()
	{
		try
		{
			NetworkPlayer unassigned = NetworkPlayer.unassigned;
			int id = unassigned.id;
			if (this._playerID != id)
			{
				try
				{
					PlayerClient objA = PlayerClient.g.playerIDDict[this._playerID];
					if (object.ReferenceEquals(objA, this))
					{
						PlayerClient.g.playerIDDict.Remove(this._playerID);
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
					PlayerClient objA2 = PlayerClient.g.userIDDict[this.userID];
					if (object.ReferenceEquals(objA2, this))
					{
						PlayerClient.g.userIDDict.Remove(this.userID);
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
			if (PlayerClient.localPlayerClient == this)
			{
				PlayerClient.localPlayerClient = null;
			}
		}
		finally
		{
			base.OnDestroy();
		}
	}

	// Token: 0x06001E5F RID: 7775 RVA: 0x000773D4 File Offset: 0x000755D4
	public static void InputFunction(GameObject req)
	{
		if (req && PlayerClient.localPlayerClient && PlayerClient.localPlayerClient._controllable && PlayerClient.localPlayerClient._controllable.gameObject == req && PlayerClient.localPlayerClient.lastInputFrame != Time.frameCount)
		{
			PlayerClient.localPlayerClient.lastInputFrame = Time.frameCount;
			PlayerClient.localPlayerClient.ClientInput();
		}
	}

	// Token: 0x06001E60 RID: 7776 RVA: 0x00077458 File Offset: 0x00075658
	protected virtual void ClientInput()
	{
	}

	// Token: 0x06001E61 RID: 7777 RVA: 0x0007745C File Offset: 0x0007565C
	private void Update()
	{
		if (this.lastInputFrame != Time.frameCount && (!this._controllable || !this._controllable.masterControllable.forwardsPlayerClientInput))
		{
			this.lastInputFrame = Time.frameCount;
			this.ClientInput();
		}
		if (NetCull.isClientRunning && !Globals.isLoading)
		{
			ulong num = NetCull.localTimeInMillis;
			if (num >= this.nextAutoReclockTime)
			{
				try
				{
					ulong num2 = Math.Min(num - this.nextAutoReclockTime, 500UL);
					NetCull.ResynchronizeClock((3000UL + num2) / 1000.0);
					num += num2;
				}
				finally
				{
					this.nextAutoReclockTime = num + 420000UL;
				}
			}
		}
	}

	// Token: 0x06001E62 RID: 7778 RVA: 0x00077538 File Offset: 0x00075738
	internal void OnRootControllableEntered(Controllable controllable)
	{
		if (this._controllable)
		{
			Debug.LogWarning("There was a controllable for player client already", this);
		}
		this._controllable = controllable;
	}

	// Token: 0x06001E63 RID: 7779 RVA: 0x00077568 File Offset: 0x00075768
	internal void OnRootControllableExited(Controllable controllable)
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

	// Token: 0x06001E64 RID: 7780 RVA: 0x000775A0 File Offset: 0x000757A0
	public static bool Find(NetworkPlayer player, out PlayerClient pc)
	{
		int id = player.id;
		int num = id;
		NetworkPlayer unassigned = NetworkPlayer.unassigned;
		if (num == unassigned.id || player == NetworkPlayer.server)
		{
			pc = null;
			return false;
		}
		return PlayerClient.g.playerIDDict.TryGetValue(id, out pc);
	}

	// Token: 0x06001E65 RID: 7781 RVA: 0x000775EC File Offset: 0x000757EC
	public static bool Find(NetworkPlayer player, out PlayerClient pc, bool throwIfNotFound)
	{
		if (!throwIfNotFound)
		{
			return PlayerClient.Find(player, out pc);
		}
		if (!PlayerClient.Find(player, out pc))
		{
			throw new ArgumentException("There was no PlayerClient for that player", "player");
		}
		return true;
	}

	// Token: 0x06001E66 RID: 7782 RVA: 0x0007761C File Offset: 0x0007581C
	public static IEnumerable<PlayerClient> FindAllWithName(string name, StringComparison comparison)
	{
		ServerManagement serverManagement;
		if (!string.IsNullOrEmpty(name) && (serverManagement = ServerManagement.Get()))
		{
			return serverManagement.FindPlayerClientsByName(name, comparison);
		}
		return EmptyArray<PlayerClient>.emptyEnumerable;
	}

	// Token: 0x06001E67 RID: 7783 RVA: 0x00077654 File Offset: 0x00075854
	public static IEnumerable<PlayerClient> FindAllWithName(string name)
	{
		return PlayerClient.FindAllWithName(name, StringComparison.InvariantCultureIgnoreCase);
	}

	// Token: 0x06001E68 RID: 7784 RVA: 0x00077660 File Offset: 0x00075860
	public static IEnumerable<PlayerClient> FindAllWithString(string partialNameOrIDInt)
	{
		ServerManagement serverManagement = ServerManagement.Get();
		if (serverManagement == null)
		{
			return EmptyArray<PlayerClient>.emptyEnumerable;
		}
		if (!string.IsNullOrEmpty(partialNameOrIDInt))
		{
			return serverManagement.FindPlayerClientsByString(partialNameOrIDInt);
		}
		return EmptyArray<PlayerClient>.emptyEnumerable;
	}

	// Token: 0x06001E69 RID: 7785 RVA: 0x000776A0 File Offset: 0x000758A0
	public static bool FindByUserID(ulong userID, out PlayerClient client)
	{
		if (userID == 0UL)
		{
			client = null;
			return false;
		}
		return PlayerClient.g.userIDDict.TryGetValue(userID, out client);
	}

	// Token: 0x1700079E RID: 1950
	// (get) Token: 0x06001E6A RID: 7786 RVA: 0x000776BC File Offset: 0x000758BC
	public static LockedList<PlayerClient> All
	{
		get
		{
			ServerManagement serverManagement = ServerManagement.Get();
			if (serverManagement)
			{
				return serverManagement.lockedPlayerClientList;
			}
			return LockedList<PlayerClient>.Empty;
		}
	}

	// Token: 0x06001E6B RID: 7787 RVA: 0x000776E8 File Offset: 0x000758E8
	public void ProcessLocalPlayerPreRender()
	{
		if (this._controllable)
		{
			this._controllable.masterControllable.ProcessLocalPlayerPreRender();
		}
	}

	// Token: 0x04000E99 RID: 3737
	private const ulong kAutoReclockInitialDelay = 8000UL;

	// Token: 0x04000E9A RID: 3738
	private const ulong kAutoReclockInterval = 420000UL;

	// Token: 0x04000E9B RID: 3739
	private const ulong kAutoReclockMS_Base = 3000UL;

	// Token: 0x04000E9C RID: 3740
	private const ulong kAutoReclockMS_AddMax = 500UL;

	// Token: 0x04000E9D RID: 3741
	public static PlayerClient localPlayerClient;

	// Token: 0x04000E9E RID: 3742
	private Controllable _controllable;

	// Token: 0x04000E9F RID: 3743
	public NetworkPlayer netPlayer;

	// Token: 0x04000EA0 RID: 3744
	private NetworkMessageInfo instantiationinfo;

	// Token: 0x04000EA1 RID: 3745
	private int _playerID;

	// Token: 0x04000EA2 RID: 3746
	[NonSerialized]
	private bool boundUserID;

	// Token: 0x04000EA3 RID: 3747
	public ulong userID;

	// Token: 0x04000EA4 RID: 3748
	public string userName;

	// Token: 0x04000EA5 RID: 3749
	[NonSerialized]
	public bool firstReady;

	// Token: 0x04000EA6 RID: 3750
	private int lastInputFrame = int.MinValue;

	// Token: 0x04000EA7 RID: 3751
	private ulong nextAutoReclockTime;

	// Token: 0x02000316 RID: 790
	private static class g
	{
		// Token: 0x04000EA8 RID: 3752
		public static Dictionary<int, PlayerClient> playerIDDict = new Dictionary<int, PlayerClient>();

		// Token: 0x04000EA9 RID: 3753
		public static Dictionary<ulong, PlayerClient> userIDDict = new Dictionary<ulong, PlayerClient>();
	}
}
