using System;

// Token: 0x020005D7 RID: 1495
public struct CraftingSession
{
	// Token: 0x17000A26 RID: 2598
	// (get) Token: 0x06002FB8 RID: 12216 RVA: 0x000B7B80 File Offset: 0x000B5D80
	// (set) Token: 0x06002FB9 RID: 12217 RVA: 0x000B7B88 File Offset: 0x000B5D88
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

	// Token: 0x17000A27 RID: 2599
	// (get) Token: 0x06002FBA RID: 12218 RVA: 0x000B7B94 File Offset: 0x000B5D94
	public float remainingSeconds
	{
		get
		{
			return (this.duration - this.progressSeconds) / this.progressPerSec;
		}
	}

	// Token: 0x17000A28 RID: 2600
	// (get) Token: 0x06002FBB RID: 12219 RVA: 0x000B7BAC File Offset: 0x000B5DAC
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

	// Token: 0x06002FBC RID: 12220 RVA: 0x000B7BD4 File Offset: 0x000B5DD4
	public bool Restart(global::Inventory inventory, int amount, global::BlueprintDataBlock blueprint, ulong startTimeMillis)
	{
		if (!blueprint || !blueprint.CanWork(amount, inventory))
		{
			this = default(global::CraftingSession);
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

	// Token: 0x040019D6 RID: 6614
	[NonSerialized]
	public global::BlueprintDataBlock blueprint;

	// Token: 0x040019D7 RID: 6615
	[NonSerialized]
	public float startTime;

	// Token: 0x040019D8 RID: 6616
	[NonSerialized]
	public float duration;

	// Token: 0x040019D9 RID: 6617
	[NonSerialized]
	public float progressSeconds;

	// Token: 0x040019DA RID: 6618
	[NonSerialized]
	public float _progressPerSec;

	// Token: 0x040019DB RID: 6619
	[NonSerialized]
	public ulong startTimeMillis;

	// Token: 0x040019DC RID: 6620
	[NonSerialized]
	public ulong durationMillis;

	// Token: 0x040019DD RID: 6621
	[NonSerialized]
	public ulong secondsCraftingFor;

	// Token: 0x040019DE RID: 6622
	[NonSerialized]
	public int amount;

	// Token: 0x040019DF RID: 6623
	[NonSerialized]
	public bool inProgress;
}
