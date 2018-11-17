using System;
using UnityEngine;

// Token: 0x02000710 RID: 1808
[global::NGCAutoAddScript]
public class DeployedRespawn : global::DeployableObject
{
	// Token: 0x06003C21 RID: 15393 RVA: 0x000D6E14 File Offset: 0x000D5014
	protected DeployedRespawn() : base(2)
	{
		this.lastSpawnTime = double.NegativeInfinity;
	}

	// Token: 0x06003C22 RID: 15394 RVA: 0x000D6E3C File Offset: 0x000D503C
	public virtual bool IsValidToSpawn()
	{
		return global::NetCull.time > this.lastSpawnTime + this.spawnDelay;
	}

	// Token: 0x06003C23 RID: 15395 RVA: 0x000D6E54 File Offset: 0x000D5054
	public virtual void NearbyRespawn()
	{
		this.lastSpawnTime = global::NetCull.time;
	}

	// Token: 0x06003C24 RID: 15396 RVA: 0x000D6E64 File Offset: 0x000D5064
	public virtual void MarkSpawnedOn()
	{
		this.lastSpawnTime = global::NetCull.time;
	}

	// Token: 0x06003C25 RID: 15397 RVA: 0x000D6E74 File Offset: 0x000D5074
	public double CooldownTimeLeft()
	{
		return (double)Mathf.Clamp((float)(this.lastSpawnTime + this.spawnDelay - global::NetCull.time), 0f, (float)this.spawnDelay);
	}

	// Token: 0x06003C26 RID: 15398 RVA: 0x000D6EA8 File Offset: 0x000D50A8
	public virtual Quaternion GetSpawnRot()
	{
		return base.transform.rotation;
	}

	// Token: 0x06003C27 RID: 15399 RVA: 0x000D6EB8 File Offset: 0x000D50B8
	public virtual Vector3 GetSpawnPos()
	{
		return base.transform.position;
	}

	// Token: 0x04001E40 RID: 7744
	public double lastSpawnTime;

	// Token: 0x04001E41 RID: 7745
	public double spawnDelay = 240.0;
}
