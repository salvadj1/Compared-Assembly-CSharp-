using System;

// Token: 0x020002E6 RID: 742
public struct DeployedObjectInfo
{
	// Token: 0x17000766 RID: 1894
	// (get) Token: 0x06001A02 RID: 6658 RVA: 0x00065230 File Offset: 0x00063430
	public global::PlayerClient playerClient
	{
		get
		{
			if (!this.valid)
			{
				return null;
			}
			global::PlayerClient result;
			global::PlayerClient.FindByUserID(this.userID, out result);
			return result;
		}
	}

	// Token: 0x17000767 RID: 1895
	// (get) Token: 0x06001A03 RID: 6659 RVA: 0x0006525C File Offset: 0x0006345C
	public global::Controllable playerControllable
	{
		get
		{
			if (!this.valid)
			{
				return null;
			}
			global::PlayerClient playerClient = this.playerClient;
			if (playerClient)
			{
				return playerClient.controllable;
			}
			return null;
		}
	}

	// Token: 0x17000768 RID: 1896
	// (get) Token: 0x06001A04 RID: 6660 RVA: 0x00065290 File Offset: 0x00063490
	public global::Character playerCharacter
	{
		get
		{
			if (!this.valid)
			{
				return null;
			}
			global::Controllable playerControllable = this.playerControllable;
			if (playerControllable)
			{
				return playerControllable.idMain;
			}
			return null;
		}
	}

	// Token: 0x04000E40 RID: 3648
	public bool valid;

	// Token: 0x04000E41 RID: 3649
	public ulong userID;
}
