using System;

// Token: 0x02000491 RID: 1169
public class RepairReceiver : IDLocal
{
	// Token: 0x060029AF RID: 10671 RVA: 0x000A35D0 File Offset: 0x000A17D0
	public ItemDataBlock GetRepairAmmo()
	{
		return this.repairAmmo;
	}

	// Token: 0x04001591 RID: 5521
	public ItemDataBlock repairAmmo;

	// Token: 0x04001592 RID: 5522
	public int ResForMaxHealth = 10;
}
