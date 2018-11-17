using System;
using System.Collections.Generic;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x0200031C RID: 796
[RequireComponent(typeof(uLinkNetworkView))]
public class ServerManagement : MonoBehaviour
{
	// Token: 0x06001E8F RID: 7823 RVA: 0x000780E4 File Offset: 0x000762E4
	public ServerManagement() : this(new List<PlayerClient>())
	{
	}

	// Token: 0x06001E90 RID: 7824 RVA: 0x000780F4 File Offset: 0x000762F4
	private ServerManagement(List<PlayerClient> pcList)
	{
		this.lockedPlayerClientList = new LockedList<PlayerClient>(pcList);
		this._playerClientList = pcList;
	}

	// Token: 0x06001E92 RID: 7826 RVA: 0x0007812C File Offset: 0x0007632C
	public static ServerManagement Get()
	{
		return ServerManagement._serverMan;
	}

	// Token: 0x06001E93 RID: 7827 RVA: 0x00078134 File Offset: 0x00076334
	public virtual void AddPlayerSpawn(GameObject spawn)
	{
	}

	// Token: 0x06001E94 RID: 7828 RVA: 0x00078138 File Offset: 0x00076338
	public virtual void RemovePlayerSpawn(GameObject spawn)
	{
	}

	// Token: 0x06001E95 RID: 7829 RVA: 0x0007813C File Offset: 0x0007633C
	protected void Awake()
	{
		ServerManagement._serverMan = this;
		Object.DontDestroyOnLoad(base.gameObject);
		DestroysOnDisconnect.ApplyToGameObject(base.gameObject);
	}

	// Token: 0x06001E96 RID: 7830 RVA: 0x0007815C File Offset: 0x0007635C
	public static NetError GetLastKickReason(bool clear)
	{
		NetError? netError = ServerManagement.kickedNetError;
		NetError result = (netError == null) ? NetCull.lastError : netError.Value;
		if (clear)
		{
			ServerManagement.kickedNetError = null;
		}
		return result;
	}

	// Token: 0x06001E97 RID: 7831 RVA: 0x000781A4 File Offset: 0x000763A4
	public void LocalClientPoliteReady()
	{
		base.networkView.RPC("ClientFirstReady", 0, new object[0]);
	}

	// Token: 0x06001E98 RID: 7832 RVA: 0x000781C0 File Offset: 0x000763C0
	[RPC]
	protected void RequestRespawn(bool campRequest, NetworkMessageInfo info)
	{
	}

	// Token: 0x06001E99 RID: 7833 RVA: 0x000781C4 File Offset: 0x000763C4
	[RPC]
	protected void ClientFirstReady(NetworkMessageInfo info)
	{
	}

	// Token: 0x06001E9A RID: 7834 RVA: 0x000781C8 File Offset: 0x000763C8
	private void AddPlayerClientToList(PlayerClient pc)
	{
		this._playerClientList.Add(pc);
	}

	// Token: 0x06001E9B RID: 7835 RVA: 0x000781D8 File Offset: 0x000763D8
	private void RemovePlayerClientFromList(PlayerClient pc)
	{
		this._playerClientList.Remove(pc);
	}

	// Token: 0x06001E9C RID: 7836 RVA: 0x000781E8 File Offset: 0x000763E8
	private void RemovePlayerClientFromListByNetworkPlayer(NetworkPlayer np)
	{
		PlayerClient pc;
		if (this.GetPlayerClient(np, out pc))
		{
			this.RemovePlayerClientFromList(pc);
		}
		else
		{
			Debug.Log("Error, could not find PC for NP");
		}
	}

	// Token: 0x06001E9D RID: 7837 RVA: 0x0007821C File Offset: 0x0007641C
	public bool GetPlayerClient(GameObject go, out PlayerClient playerClient)
	{
		foreach (PlayerClient playerClient2 in this._playerClientList)
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

	// Token: 0x06001E9E RID: 7838 RVA: 0x000782B4 File Offset: 0x000764B4
	public bool GetPlayerClient(NetworkPlayer player, out PlayerClient playerClient)
	{
		foreach (PlayerClient playerClient2 in this._playerClientList)
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

	// Token: 0x06001E9F RID: 7839 RVA: 0x00078334 File Offset: 0x00076534
	[Obsolete("You should be using PlayerClient.FindAllWithString")]
	internal IEnumerable<PlayerClient> FindPlayerClientsByString(string name)
	{
		int iFound = 0;
		ulong iUserID = 0UL;
		if (ulong.TryParse(name, out iUserID))
		{
			foreach (PlayerClient client in this._playerClientList)
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
		foreach (PlayerClient client2 in this._playerClientList)
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
		foreach (PlayerClient client3 in this._playerClientList)
		{
			if (client3.userName.StartsWith(name, StringComparison.InvariantCultureIgnoreCase))
			{
				yield return client3;
			}
		}
		yield break;
	}

	// Token: 0x06001EA0 RID: 7840 RVA: 0x00078368 File Offset: 0x00076568
	[Obsolete("You should be using PlayerClient.FindAllWithName")]
	internal IEnumerable<PlayerClient> FindPlayerClientsByName(string name, StringComparison comparison)
	{
		foreach (PlayerClient client in this._playerClientList)
		{
			if (string.Equals(client.userName, name, comparison))
			{
				yield return client;
			}
		}
		yield break;
	}

	// Token: 0x06001EA1 RID: 7841 RVA: 0x000783A8 File Offset: 0x000765A8
	public static IEnumerable<NetworkPlayer> GetNetworkPlayersByName(string name)
	{
		return ServerManagement.GetNetworkPlayersByName(name, StringComparison.InvariantCultureIgnoreCase);
	}

	// Token: 0x06001EA2 RID: 7842 RVA: 0x000783B4 File Offset: 0x000765B4
	public static IEnumerable<NetworkPlayer> GetNetworkPlayersByName(string name, StringComparison comparison)
	{
		ServerManagement svm = ServerManagement.Get();
		if (svm)
		{
			foreach (PlayerClient pc in svm.FindPlayerClientsByName(name, comparison))
			{
				yield return pc.netPlayer;
			}
		}
		yield break;
	}

	// Token: 0x06001EA3 RID: 7843 RVA: 0x000783EC File Offset: 0x000765EC
	public static IEnumerable<NetworkPlayer> GetNetworkPlayersByString(string partialNameOrIntID)
	{
		ServerManagement svm = ServerManagement.Get();
		if (svm)
		{
			foreach (PlayerClient pc in svm.FindPlayerClientsByString(partialNameOrIntID))
			{
				yield return pc.netPlayer;
			}
		}
		yield break;
	}

	// Token: 0x06001EA4 RID: 7844 RVA: 0x00078418 File Offset: 0x00076618
	public RPCMode GetNetworkPlayersInSameZone(PlayerClient client)
	{
		return 1;
	}

	// Token: 0x06001EA5 RID: 7845 RVA: 0x0007841C File Offset: 0x0007661C
	public RPCMode GetNetworkPlayersInGroup(string group)
	{
		return 1;
	}

	// Token: 0x06001EA6 RID: 7846 RVA: 0x00078420 File Offset: 0x00076620
	protected static bool GetOrigin(NetworkPlayer player, bool eyes, out Vector3 origin)
	{
		ServerManagement serverManagement = ServerManagement.Get();
		PlayerClient playerClient;
		if (serverManagement && serverManagement.GetPlayerClient(player, out playerClient))
		{
			Controllable controllable = playerClient.controllable;
			if (controllable)
			{
				Character component = controllable.GetComponent<Character>();
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

	// Token: 0x06001EA7 RID: 7847 RVA: 0x000784C8 File Offset: 0x000766C8
	private void UnstickInvoke()
	{
		if (this.hasUnstickPosition)
		{
			try
			{
				if (this.unstickTransform)
				{
					this.unstickTransform.position = this.nextUnstickPosition;
					Character component = this.unstickTransform.GetComponent<Character>();
					if (component)
					{
						CCMotor ccmotor = component.ccmotor;
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

	// Token: 0x06001EA8 RID: 7848 RVA: 0x00078564 File Offset: 0x00076764
	[RPC]
	protected void UnstickMove(Vector3 point)
	{
		PlayerClient localPlayerClient = PlayerClient.localPlayerClient;
		if (localPlayerClient)
		{
			Controllable controllable = localPlayerClient.controllable;
			if (controllable)
			{
				Character component = controllable.GetComponent<Character>();
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

	// Token: 0x06001EA9 RID: 7849 RVA: 0x00078600 File Offset: 0x00076800
	[RPC]
	protected void KP(int err)
	{
		ServerManagement.kickedNetError = new NetError?((NetError)err);
	}

	// Token: 0x06001EAA RID: 7850 RVA: 0x00078610 File Offset: 0x00076810
	[RPC]
	protected void RS(float duration)
	{
		NetCull.ResynchronizeClock((double)duration);
	}

	// Token: 0x06001EAB RID: 7851 RVA: 0x0007861C File Offset: 0x0007681C
	protected void OnDestroy()
	{
		if (ServerManagement._serverMan == this)
		{
			ServerManagement._serverMan = null;
		}
	}

	// Token: 0x06001EAC RID: 7852 RVA: 0x00078634 File Offset: 0x00076834
	public virtual void TeleportPlayer(NetworkPlayer move, Vector3 worldPoint)
	{
	}

	// Token: 0x04000EC7 RID: 3783
	[SerializeField]
	protected string defaultPlayerControllableKey = ":player_soldier";

	// Token: 0x04000EC8 RID: 3784
	private static ServerManagement _serverMan;

	// Token: 0x04000EC9 RID: 3785
	[NonSerialized]
	protected readonly List<PlayerClient> _playerClientList;

	// Token: 0x04000ECA RID: 3786
	[Obsolete("Use PlayerClient.All")]
	[NonSerialized]
	internal readonly LockedList<PlayerClient> lockedPlayerClientList;

	// Token: 0x04000ECB RID: 3787
	private static NetError? kickedNetError;

	// Token: 0x04000ECC RID: 3788
	private bool hasUnstickPosition;

	// Token: 0x04000ECD RID: 3789
	private Transform unstickTransform;

	// Token: 0x04000ECE RID: 3790
	private Vector3 nextUnstickPosition;

	// Token: 0x04000ECF RID: 3791
	protected bool blockFutureConnections;
}
