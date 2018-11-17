using System;
using Facepunch;
using UnityEngine;

// Token: 0x0200072F RID: 1839
[global::NGCAutoAddScript]
public class SleepingBag : global::DeployedRespawn, global::IContextRequestable, global::IContextRequestableQuick, global::IContextRequestableStatus, global::IContextRequestableText, global::IContextRequestablePointText, global::IComponentInterface<global::IContextRequestable, MonoBehaviour, global::Contextual>, global::IComponentInterface<global::IContextRequestable, MonoBehaviour>, global::IComponentInterface<global::IContextRequestable>
{
	// Token: 0x06003D09 RID: 15625 RVA: 0x000DAA20 File Offset: 0x000D8C20
	bool global::IContextRequestablePointText.ContextTextPoint(out Vector3 worldPoint)
	{
		global::ContextRequestable.PointUtil.SpriteOrOrigin(this, out worldPoint);
		return true;
	}

	// Token: 0x06003D0A RID: 15626 RVA: 0x000DAA2C File Offset: 0x000D8C2C
	public global::ContextExecution ContextQuery(global::Controllable controllable, ulong timestamp)
	{
		return global::ContextExecution.Quick;
	}

	// Token: 0x06003D0B RID: 15627 RVA: 0x000DAA30 File Offset: 0x000D8C30
	public global::ContextResponse ContextRespondQuick(global::Controllable controllable, ulong timestamp)
	{
		this.PlayerUse(controllable);
		return global::ContextResponse.DoneBreak;
	}

	// Token: 0x06003D0C RID: 15628 RVA: 0x000DAA3C File Offset: 0x000D8C3C
	public string ContextText(global::Controllable localControllable)
	{
		global::PlayerClient playerClient = localControllable.playerClient;
		if (playerClient && playerClient.userID == this.creatorID)
		{
			return "Pick Up";
		}
		return string.Empty;
	}

	// Token: 0x06003D0D RID: 15629 RVA: 0x000DAA78 File Offset: 0x000D8C78
	public global::ContextStatusFlags ContextStatusPoll()
	{
		global::PlayerClient localPlayerClient = global::PlayerClient.localPlayerClient;
		if (localPlayerClient && localPlayerClient.userID == this.creatorID)
		{
			return (global::ContextStatusFlags)0;
		}
		return global::ContextStatusFlags.SpriteFlag1;
	}

	// Token: 0x06003D0E RID: 15630 RVA: 0x000DAAB0 File Offset: 0x000D8CB0
	public void PlayerUse(global::Controllable controllable)
	{
		if (base.BelongsTo(controllable))
		{
			if (!this.IsValidToSpawn())
			{
				return;
			}
			global::Inventory component = controllable.GetComponent<global::Inventory>();
			if (component.AddItemAmount(global::DatablockDictionary.GetByName(this.giveItemName), 1) == 1)
			{
				return;
			}
		}
	}

	// Token: 0x04001F34 RID: 7988
	public string giveItemName;
}
