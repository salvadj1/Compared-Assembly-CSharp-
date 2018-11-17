using System;

// Token: 0x0200054C RID: 1356
public class RepairReceiver : IDLocal
{
	// Token: 0x06002D61 RID: 11617 RVA: 0x000AB368 File Offset: 0x000A9568
	public global::ItemDataBlock GetRepairAmmo()
	{
		return this.repairAmmo;
	}

	// Token: 0x0400174E RID: 5966
	public global::ItemDataBlock repairAmmo;

	// Token: 0x0400174F RID: 5967
	public int ResForMaxHealth = 10;
}
