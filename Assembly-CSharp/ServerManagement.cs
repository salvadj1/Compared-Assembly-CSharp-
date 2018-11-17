using System;
using System.Collections.Generic;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x020003C5 RID: 965
[RequireComponent(typeof(uLinkNetworkView))]
public class ServerManagement : MonoBehaviour
{
	// Token: 0x060021D1 RID: 8657 RVA: 0x0007CB64 File Offset: 0x0007AD64
	public ServerManagement() : this(new List<global::PlayerClient>())
	{
	}

	// Token: 0x060021D2 RID: 8658 RVA: 0x0007CB74 File Offset: 0x0007AD74
	private ServerManagement(List<global::PlayerClient> pcList)
	{
		this.lockedPlayerClientList = new global::LockedList<global::PlayerClient>(pcList);
		this._playerClientList = pcList;
	}

	// Token: 0x060021D4 RID: 8660 RVA: 0x0007CBAC File Offset: 0x0007ADAC
	public static global::ServerManagement Get()
	{
		return global::ServerManagement._serverMan;
	}

	// Token: 0x060021D5 RID: 8661 RVA: 0x0007CBB4 File Offset: 0x0007ADB4
	public virtual void AddPlayerSpawn(GameObject spawn)
	{
	}

	// Token: 0x060021D6 RID: 8662 RVA: 0x0007CBB8 File Offset: 0x0007ADB8
	public virtual void RemovePlayerSpawn(GameObject spawn)
	{
	}

	// Token: 0x060021D7 RID: 8663 RVA: 0x0007CBBC File Offset: 0x0007ADBC
	protected void Awake()
	{
		global::ServerManagement._serverMan = this;
		Object.DontDestroyOnLoad(base.gameObject);
		global::DestroysOnDisconnect.ApplyToGameObject(base.gameObject);
	}

	// Token: 0x060021D8 RID: 8664 RVA: 0x0007CBDC File Offset: 0x0007ADDC
	public static global::NetError GetLastKickReason(bool clear)
	{
		global::NetError? netError = global::ServerManagement.kickedNetError;
		global::NetError result = (netError == null) ? global::NetCull.lastError : netError.Value;
		if (clear)
		{
			global::ServerManagement.kickedNetError = null;
		}
		return result;
	}

	// Token: 0x060021D9 RID: 8665 RVA: 0x0007CC24 File Offset: 0x0007AE24
	public void LocalClientPoliteReady()
	{
		base.networkView.RPC("ClientFirstReady", 0, new object[0]);
	}

	// Token: 0x060021DA RID: 8666 RVA: 0x0007CC40 File Offset: 0x0007AE40
	[RPC]
	protected void RequestRespawn(bool campRequest, uLink.NetworkMessageInfo info)
	{
	}

	// Token: 0x060021DB RID: 8667 RVA: 0x0007CC44 File Offset: 0x0007AE44
	[RPC]
	protected void ClientFirstReady(uLink.NetworkMessageInfo info)
	{
	}

	// Token: 0x060021DC RID: 8668 RVA: 0x0007CC48 File Offset: 0x0007AE48
	private void AddPlayerClientToList(global::PlayerClient pc)
	{
		this._playerClientList.Add(pc);
	}

	// Token: 0x060021DD RID: 8669 RVA: 0x0007CC58 File Offset: 0x0007AE58
	private void RemovePlayerClientFromList(global::PlayerClient pc)
	{
		this._playerClientList.Remove(pc);
	}

	// Token: 0x060021DE RID: 8670 RVA: 0x0007CC68 File Offset: 0x0007AE68
	private void RemovePlayerClientFromListByNetworkPlayer(uLink.NetworkPlayer np)
	{
		global::PlayerClient pc;
		if (this.GetPlayerClient(np, out pc))
		{
			this.RemovePlayerClientFromList(pc);
		}
		else
		{
			Debug.Log("Error, could not find PC for NP");
		}
	}

	// Token: 0x060021DF RID: 8671 RVA: 0x0007CC9C File Offset: 0x0007AE9C
	public bool GetPlayerClient(GameObject go, out global::PlayerClient playerClient)
	{
		foreach (global::PlayerClient playerClient2 in this._playerClientList)
		{
			if (playerClient2.controllable && playerClient2.controllable.gameObject == go)
			{
				playerClient = playerClient2;
				return true;
			}
		}
		playerClient = null;
		return false;
	}

	// Token: 0x060021E0 RID: 8672 RVA: 0x0007CD34 File Offset: 0x0007AF34
	public bool GetPlayerClient(uLink.NetworkPlayer player, out global::PlayerClient playerClient)
	{
		foreach (global::PlayerClient playerClient2 in this._playerClientList)
		{
			if (playerClient2.netPlayer == player)
			{
				playerClient = playerClient2;
				return true;
			}
		}
		playerClient = null;
		return false;
	}

	// Token: 0x060021E1 RID: 8673 RVA: 0x0007CDB4 File Offset: 0x0007AFB4
	[Obsolete("You should be using PlayerClient.FindAllWithString")]
	internal IEnumerable<global::PlayerClient> FindPlayerClientsByString(string name)
	{
		int iFound = 0;
		ulong iUserID = 0UL;
		if (ulong.TryParse(name, out iUserID))
		{
			foreach (global::PlayerClient client in this._playerClientList)
			{
				if (client.userID == iUserID)
				{
					yield return client;
					iFound++;
					break;
				}
			}
			if (iFound > 0)
			{
				yield break;
			}
		}
		foreach (global::PlayerClient client2 in this._playerClientList)
		{
			if (string.Equals(client2.userName, name, StringComparison.InvariantCultureIgnoreCase))
			{
				yield return client2;
				iFound++;
			}
		}
		if (iFound > 0)
		{
			yield break;
		}
		foreach (global::PlayerClient client3 in this._playerClientList)
		{
			if (client3.userName.StartsWith(name, StringComparison.InvariantCultureIgnoreCase))
			{
				yield return client3;
			}
		}
		yield break;
	}

	// Token: 0x060021E2 RID: 8674 RVA: 0x0007CDE8 File Offset: 0x0007AFE8
	[Obsolete("You should be using PlayerClient.FindAllWithName")]
	internal IEnumerable<global::PlayerClient> FindPlayerClientsByName(string name, StringComparison comparison)
	{
		foreach (global::PlayerClient client in this._playerClientList)
		{
			if (string.Equals(client.userName, name, comparison))
			{
				yield return client;
			}
		}
		yield break;
	}

	// Token: 0x060021E3 RID: 8675 RVA: 0x0007CE28 File Offset: 0x0007B028
	public static IEnumerable<uLink.NetworkPlayer> GetNetworkPlayersByName(string name)
	{
		return global::ServerManagement.GetNetworkPlayersByName(name, StringComparison.InvariantCultureIgnoreCase);
	}

	// Token: 0x060021E4 RID: 8676 RVA: 0x0007CE34 File Offset: 0x0007B034
	public static IEnumerable<uLink.NetworkPlayer> GetNetworkPlayersByName(string name, StringComparison comparison)
	{
		global::ServerManagement svm = global::ServerManagement.Get();
		if (svm)
		{
			foreach (global::PlayerClient pc in svm.FindPlayerClientsByName(name, comparison))
			{
				yield return pc.netPlayer;
			}
		}
		yield break;
	}

	// Token: 0x060021E5 RID: 8677 RVA: 0x0007CE6C File Offset: 0x0007B06C
	public static IEnumerable<uLink.NetworkPlayer> GetNetworkPlayersByString(string partialNameOrIntID)
	{
		global::ServerManagement svm = global::ServerManagement.Get();
		if (svm)
		{
			foreach (global::PlayerClient pc in svm.FindPlayerClientsByString(partialNameOrIntID))
			{
				yield return pc.netPlayer;
			}
		}
		yield break;
	}

	// Token: 0x060021E6 RID: 8678 RVA: 0x0007CE98 File Offset: 0x0007B098
	public uLink.RPCMode GetNetworkPlayersInSameZone(global::PlayerClient client)
	{
		return 1;
	}

	// Token: 0x060021E7 RID: 8679 RVA: 0x0007CE9C File Offset: 0x0007B09C
	public uLink.RPCMode GetNetworkPlayersInGroup(string group)
	{
		return 1;
	}

	// Token: 0x060021E8 RID: 8680 RVA: 0x0007CEA0 File Offset: 0x0007B0A0
	protected static bool GetOrigin(uLink.NetworkPlayer player, bool eyes, out Vector3 origin)
	{
		global::ServerManagement serverManagement = global::ServerManagement.Get();
		global::PlayerClient playerClient;
		if (serverManagement && serverManagement.GetPlayerClient(player, out playerClient))
		{
			global::Controllable controllable = playerClient.controllable;
			if (controllable)
			{
				global::Character component = controllable.GetComponent<global::Character>();
				Transform transform;
				if (component)
				{
					transform = ((!eyes || !component.eyesTransformReadOnly) ? component.transform : component.eyesTransformReadOnly);
				}
				else
				{
					transform = controllable.transform;
				}
				origin = transform.position;
				return true;
			}
		}
		origin = default(Vector3);
		return false;
	}

	// Token: 0x060021E9 RID: 8681 RVA: 0x0007CF48 File Offset: 0x0007B148
	private void UnstickInvoke()
	{
		if (this.hasUnstickPosition)
		{
			try
			{
				if (this.unstickTransform)
				{
					this.unstickTransform.position = this.nextUnstickPosition;
					global::Character component = this.unstickTransform.GetComponent<global::Character>();
					if (component)
					{
						global::CCMotor ccmotor = component.ccmotor;
						if (ccmotor)
						{
							ccmotor.Teleport(this.nextUnstickPosition);
						}
					}
				}
			}
			finally
			{
				this.hasUnstickPosition = false;
			}
		}
	}

	// Token: 0x060021EA RID: 8682 RVA: 0x0007CFE4 File Offset: 0x0007B1E4
	[RPC]
	protected void UnstickMove(Vector3 point)
	{
		global::PlayerClient localPlayerClient = global::PlayerClient.localPlayerClient;
		if (localPlayerClient)
		{
			global::Controllable controllable = localPlayerClient.controllable;
			if (controllable)
			{
				global::Character component = controllable.GetComponent<global::Character>();
				Transform transform;
				if (component)
				{
					transform = component.transform;
				}
				else
				{
					transform = controllable.transform;
				}
				if (transform)
				{
					this.hasUnstickPosition = true;
					this.nextUnstickPosition = point;
					this.unstickTransform = transform;
					this.UnstickInvoke();
					base.Invoke("UnstickInvoke", 0.25f);
				}
			}
		}
	}

	// Token: 0x060021EB RID: 8683 RVA: 0x0007D080 File Offset: 0x0007B280
	[RPC]
	protected void KP(int err)
	{
		global::ServerManagement.kickedNetError = new global::NetError?((global::NetError)err);
	}

	// Token: 0x060021EC RID: 8684 RVA: 0x0007D090 File Offset: 0x0007B290
	[RPC]
	protected void RS(float duration)
	{
		global::NetCull.ResynchronizeClock((double)duration);
	}

	// Token: 0x060021ED RID: 8685 RVA: 0x0007D09C File Offset: 0x0007B29C
	protected void OnDestroy()
	{
		if (global::ServerManagement._serverMan == this)
		{
			global::ServerManagement._serverMan = null;
		}
	}

	// Token: 0x060021EE RID: 8686 RVA: 0x0007D0B4 File Offset: 0x0007B2B4
	public virtual void TeleportPlayer(uLink.NetworkPlayer move, Vector3 worldPoint)
	{
	}

	// Token: 0x04001007 RID: 4103
	[SerializeField]
	protected string defaultPlayerControllableKey = ":player_soldier";

	// Token: 0x04001008 RID: 4104
	private static global::ServerManagement _serverMan;

	// Token: 0x04001009 RID: 4105
	[NonSerialized]
	protected readonly List<global::PlayerClient> _playerClientList;

	// Token: 0x0400100A RID: 4106
	[Obsolete("Use PlayerClient.All")]
	[NonSerialized]
	internal readonly global::LockedList<global::PlayerClient> lockedPlayerClientList;

	// Token: 0x0400100B RID: 4107
	private static global::NetError? kickedNetError;

	// Token: 0x0400100C RID: 4108
	private bool hasUnstickPosition;

	// Token: 0x0400100D RID: 4109
	private Transform unstickTransform;

	// Token: 0x0400100E RID: 4110
	private Vector3 nextUnstickPosition;

	// Token: 0x0400100F RID: 4111
	protected bool blockFutureConnections;
}
