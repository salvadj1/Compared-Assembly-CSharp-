using System;

// Token: 0x020002A9 RID: 681
public struct DeployedObjectInfo
{
	// Token: 0x17000712 RID: 1810
	// (get) Token: 0x06001872 RID: 6258 RVA: 0x000608BC File Offset: 0x0005EABC
	public PlayerClient playerClient
	{
		get
		{
			if (!this.valid)
			{
				return null;
			}
			PlayerClient result;
			PlayerClient.FindByUserID(this.userID, out result);
			return result;
		}
	}

	// Token: 0x17000713 RID: 1811
	// (get) Token: 0x06001873 RID: 6259 RVA: 0x000608E8 File Offset: 0x0005EAE8
	public Controllable playerControllable
	{
		get
		{
			if (!this.valid)
			{
				return null;
			}
			PlayerClient playerClient = this.playerClient;
			if (playerClient)
			{
				return playerClient.controllable;
			}
			return null;
		}
	}

	// Token: 0x17000714 RID: 1812
	// (get) Token: 0x06001874 RID: 6260 RVA: 0x0006091C File Offset: 0x0005EB1C
	public Character playerCharacter
	{
		get
		{
			if (!this.valid)
			{
				return null;
			}
			Controllable playerControllable = this.playerControllable;
			if (playerControllable)
			{
				return playerControllable.idMain;
			}
			return null;
		}
	}

	// Token: 0x04000D05 RID: 3333
	public bool valid;

	// Token: 0x04000D06 RID: 3334
	public ulong userID;
}
