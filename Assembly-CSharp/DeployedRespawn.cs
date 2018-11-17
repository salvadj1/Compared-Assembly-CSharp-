using System;
using UnityEngine;

// Token: 0x0200064D RID: 1613
[NGCAutoAddScript]
public class DeployedRespawn : DeployableObject
{
	// Token: 0x06003835 RID: 14389 RVA: 0x000CE564 File Offset: 0x000CC764
	protected DeployedRespawn() : base(2)
	{
		this.lastSpawnTime = double.NegativeInfinity;
	}

	// Token: 0x06003836 RID: 14390 RVA: 0x000CE58C File Offset: 0x000CC78C
	public virtual bool IsValidToSpawn()
	{
		return NetCull.time > this.lastSpawnTime + this.spawnDelay;
	}

	// Token: 0x06003837 RID: 14391 RVA: 0x000CE5A4 File Offset: 0x000CC7A4
	public virtual void NearbyRespawn()
	{
		this.lastSpawnTime = NetCull.time;
	}

	// Token: 0x06003838 RID: 14392 RVA: 0x000CE5B4 File Offset: 0x000CC7B4
	public virtual void MarkSpawnedOn()
	{
		this.lastSpawnTime = NetCull.time;
	}

	// Token: 0x06003839 RID: 14393 RVA: 0x000CE5C4 File Offset: 0x000CC7C4
	public double CooldownTimeLeft()
	{
		return (double)Mathf.Clamp((float)(this.lastSpawnTime + this.spawnDelay - NetCull.time), 0f, (float)this.spawnDelay);
	}

	// Token: 0x0600383A RID: 14394 RVA: 0x000CE5F8 File Offset: 0x000CC7F8
	public virtual Quaternion GetSpawnRot()
	{
		return base.transform.rotation;
	}

	// Token: 0x0600383B RID: 14395 RVA: 0x000CE608 File Offset: 0x000CC808
	public virtual Vector3 GetSpawnPos()
	{
		return base.transform.position;
	}

	// Token: 0x04001C4B RID: 7243
	public double lastSpawnTime;

	// Token: 0x04001C4C RID: 7244
	public double spawnDelay = 240.0;
}
