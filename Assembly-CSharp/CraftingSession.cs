using System;

// Token: 0x0200051A RID: 1306
public struct CraftingSession
{
	// Token: 0x170009B2 RID: 2482
	// (get) Token: 0x06002BF8 RID: 11256 RVA: 0x000AFAE4 File Offset: 0x000ADCE4
	// (set) Token: 0x06002BF9 RID: 11257 RVA: 0x000AFAEC File Offset: 0x000ADCEC
	public float progressPerSec
	{
		get
		{
			return this._progressPerSec;
		}
		set
		{
			this._progressPerSec = value;
		}
	}

	// Token: 0x170009B3 RID: 2483
	// (get) Token: 0x06002BFA RID: 11258 RVA: 0x000AFAF8 File Offset: 0x000ADCF8
	public float remainingSeconds
	{
		get
		{
			return (this.duration - this.progressSeconds) / this.progressPerSec;
		}
	}

	// Token: 0x170009B4 RID: 2484
	// (get) Token: 0x06002BFB RID: 11259 RVA: 0x000AFB10 File Offset: 0x000ADD10
	public double percentComplete
	{
		get
		{
			if (this.inProgress)
			{
				return (double)(this.progressSeconds / this.duration);
			}
			return 0.0;
		}
	}

	// Token: 0x06002BFC RID: 11260 RVA: 0x000AFB38 File Offset: 0x000ADD38
	public bool Restart(Inventory inventory, int amount, BlueprintDataBlock blueprint, ulong startTimeMillis)
	{
		if (!blueprint || !blueprint.CanWork(amount, inventory))
		{
			this = default(CraftingSession);
			return false;
		}
		this.blueprint = blueprint;
		this.startTime = (float)(startTimeMillis / 1000.0);
		this.duration = blueprint.craftingDuration * (float)amount;
		this.progressPerSec = 1f;
		this.progressSeconds = 0f;
		this.amount = amount;
		this.inProgress = true;
		return true;
	}

	// Token: 0x0400180A RID: 6154
	[NonSerialized]
	public BlueprintDataBlock blueprint;

	// Token: 0x0400180B RID: 6155
	[NonSerialized]
	public float startTime;

	// Token: 0x0400180C RID: 6156
	[NonSerialized]
	public float duration;

	// Token: 0x0400180D RID: 6157
	[NonSerialized]
	public float progressSeconds;

	// Token: 0x0400180E RID: 6158
	[NonSerialized]
	public float _progressPerSec;

	// Token: 0x0400180F RID: 6159
	[NonSerialized]
	public ulong startTimeMillis;

	// Token: 0x04001810 RID: 6160
	[NonSerialized]
	public ulong durationMillis;

	// Token: 0x04001811 RID: 6161
	[NonSerialized]
	public ulong secondsCraftingFor;

	// Token: 0x04001812 RID: 6162
	[NonSerialized]
	public int amount;

	// Token: 0x04001813 RID: 6163
	[NonSerialized]
	public bool inProgress;
}
