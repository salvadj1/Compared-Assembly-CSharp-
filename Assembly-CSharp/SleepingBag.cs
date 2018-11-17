using System;
using Facepunch;
using UnityEngine;

// Token: 0x0200066B RID: 1643
[NGCAutoAddScript]
public class SleepingBag : DeployedRespawn, IContextRequestable, IContextRequestableQuick, IContextRequestableStatus, IContextRequestableText, IContextRequestablePointText, IComponentInterface<IContextRequestable, MonoBehaviour, Contextual>, IComponentInterface<IContextRequestable, MonoBehaviour>, IComponentInterface<IContextRequestable>
{
	// Token: 0x06003915 RID: 14613 RVA: 0x000D2040 File Offset: 0x000D0240
	bool IContextRequestablePointText.ContextTextPoint(out Vector3 worldPoint)
	{
		ContextRequestable.PointUtil.SpriteOrOrigin(this, out worldPoint);
		return true;
	}

	// Token: 0x06003916 RID: 14614 RVA: 0x000D204C File Offset: 0x000D024C
	public ContextExecution ContextQuery(Controllable controllable, ulong timestamp)
	{
		return ContextExecution.Quick;
	}

	// Token: 0x06003917 RID: 14615 RVA: 0x000D2050 File Offset: 0x000D0250
	public ContextResponse ContextRespondQuick(Controllable controllable, ulong timestamp)
	{
		this.PlayerUse(controllable);
		return ContextResponse.DoneBreak;
	}

	// Token: 0x06003918 RID: 14616 RVA: 0x000D205C File Offset: 0x000D025C
	public string ContextText(Controllable localControllable)
	{
		PlayerClient playerClient = localControllable.playerClient;
		if (playerClient && playerClient.userID == this.creatorID)
		{
			return "Pick Up";
		}
		return string.Empty;
	}

	// Token: 0x06003919 RID: 14617 RVA: 0x000D2098 File Offset: 0x000D0298
	public ContextStatusFlags ContextStatusPoll()
	{
		PlayerClient localPlayerClient = PlayerClient.localPlayerClient;
		if (localPlayerClient && localPlayerClient.userID == this.creatorID)
		{
			return (ContextStatusFlags)0;
		}
		return ContextStatusFlags.SpriteFlag1;
	}

	// Token: 0x0600391A RID: 14618 RVA: 0x000D20D0 File Offset: 0x000D02D0
	public void PlayerUse(Controllable controllable)
	{
		if (base.BelongsTo(controllable))
		{
			if (!this.IsValidToSpawn())
			{
				return;
			}
			Inventory component = controllable.GetComponent<Inventory>();
			if (component.AddItemAmount(DatablockDictionary.GetByName(this.giveItemName), 1) == 1)
			{
				return;
			}
		}
	}

	// Token: 0x04001D3C RID: 7484
	public string giveItemName;
}
