using System;
using uLink;
using UnityEngine;

// Token: 0x02000519 RID: 1305
public class CraftingInventory : Inventory
{
	// Token: 0x06002BE7 RID: 11239 RVA: 0x000AF7F0 File Offset: 0x000AD9F0
	public void CraftThink()
	{
		if (this.crafting.inProgress)
		{
			double time = NetCull.time;
			float num = (float)(time - this._lastThinkTime);
			this.crafting.progressSeconds = Mathf.Clamp(this.crafting.progressSeconds + this.crafting.progressPerSec * num, 0f, this.crafting.duration);
			this._lastThinkTime = time;
		}
	}

	// Token: 0x06002BE8 RID: 11240 RVA: 0x000AF860 File Offset: 0x000ADA60
	public bool ValidateCraftRequirements(BlueprintDataBlock bp)
	{
		return !bp.RequireWorkbench || this.AtWorkBench();
	}

	// Token: 0x06002BE9 RID: 11241 RVA: 0x000AF880 File Offset: 0x000ADA80
	public bool AtWorkBench()
	{
		return this._lastWorkBenchTime < 0f;
	}

	// Token: 0x06002BEA RID: 11242 RVA: 0x000AF890 File Offset: 0x000ADA90
	[RPC]
	public void wbi(bool at, NetworkMessageInfo info)
	{
		if (at)
		{
			this._lastWorkBenchTime = float.NegativeInfinity;
		}
		else
		{
			this._lastWorkBenchTime = float.PositiveInfinity;
		}
	}

	// Token: 0x170009AD RID: 2477
	// (get) Token: 0x06002BEB RID: 11243 RVA: 0x000AF8B4 File Offset: 0x000ADAB4
	public new bool isCraftingInventory
	{
		get
		{
			return true;
		}
	}

	// Token: 0x170009AE RID: 2478
	// (get) Token: 0x06002BEC RID: 11244 RVA: 0x000AF8B8 File Offset: 0x000ADAB8
	public new bool isCrafting
	{
		get
		{
			return this.crafting.inProgress;
		}
	}

	// Token: 0x170009AF RID: 2479
	// (get) Token: 0x06002BED RID: 11245 RVA: 0x000AF8C8 File Offset: 0x000ADAC8
	public new float? craftingCompletePercent
	{
		get
		{
			if (this.crafting.inProgress)
			{
				return new float?((float)this.crafting.percentComplete);
			}
			return null;
		}
	}

	// Token: 0x170009B0 RID: 2480
	// (get) Token: 0x06002BEE RID: 11246 RVA: 0x000AF900 File Offset: 0x000ADB00
	public new float? craftingSecondsRemaining
	{
		get
		{
			if (this.crafting.inProgress)
			{
				return new float?(this.crafting.remainingSeconds);
			}
			return null;
		}
	}

	// Token: 0x170009B1 RID: 2481
	// (get) Token: 0x06002BEF RID: 11247 RVA: 0x000AF938 File Offset: 0x000ADB38
	public float craftingSpeedPerSec
	{
		get
		{
			return this.crafting.progressPerSec;
		}
	}

	// Token: 0x06002BF0 RID: 11248 RVA: 0x000AF948 File Offset: 0x000ADB48
	private static BlueprintDataBlock FindBlueprint(int uniqueID)
	{
		if (uniqueID == 0)
		{
			return null;
		}
		return (BlueprintDataBlock)DatablockDictionary.GetByUniqueID(uniqueID);
	}

	// Token: 0x06002BF1 RID: 11249 RVA: 0x000AF960 File Offset: 0x000ADB60
	public bool CancelCrafting()
	{
		if (this.crafting.inProgress)
		{
			base.networkView.RPC("CRFX", 0, new object[0]);
			this.crafting.inProgress = false;
			return true;
		}
		return false;
	}

	// Token: 0x06002BF2 RID: 11250 RVA: 0x000AF9A4 File Offset: 0x000ADBA4
	public bool StartCrafting(BlueprintDataBlock blueprint, int amount)
	{
		if (blueprint.CanWork(amount, this))
		{
			base.networkView.RPC("CRFS", 0, new object[]
			{
				amount,
				blueprint.uniqueID
			});
			return true;
		}
		return false;
	}

	// Token: 0x06002BF3 RID: 11251 RVA: 0x000AF9F0 File Offset: 0x000ADBF0
	protected void UpdateCrafting(BlueprintDataBlock blueprint, int amount, float start, float dur, float progress, float progresspersec)
	{
		Debug.Log(string.Format("Craft network update :{0}:", (!blueprint) ? "NONE" : blueprint.name), this);
		this._lastThinkTime = NetCull.time;
		this.crafting.blueprint = blueprint;
		this.crafting.inProgress = blueprint;
		this.crafting.startTime = start;
		this.crafting.duration = dur;
		this.crafting.progressSeconds = progress;
		this.crafting.progressPerSec = progresspersec;
		this.crafting.amount = amount;
		this.Refresh();
	}

	// Token: 0x06002BF4 RID: 11252 RVA: 0x000AFA98 File Offset: 0x000ADC98
	[RPC]
	[NGCRPCSkip]
	protected void CRFX()
	{
	}

	// Token: 0x06002BF5 RID: 11253 RVA: 0x000AFA9C File Offset: 0x000ADC9C
	[NGCRPCSkip]
	[RPC]
	protected void CRFU(float start, float dur, float progresspersec, float progress, int blueprintUniqueID, int amount)
	{
		this.UpdateCrafting(CraftingInventory.FindBlueprint(blueprintUniqueID), amount, start, dur, progress, progresspersec);
	}

	// Token: 0x06002BF6 RID: 11254 RVA: 0x000AFAC0 File Offset: 0x000ADCC0
	[RPC]
	[NGCRPCSkip]
	protected void CRFC()
	{
		this.UpdateCrafting(null, 0, 0f, 0f, 0f, 0f);
	}

	// Token: 0x06002BF7 RID: 11255 RVA: 0x000AFAE0 File Offset: 0x000ADCE0
	[NGCRPCSkip]
	[RPC]
	protected void CRFS(int amount, int blueprintUID, NetworkMessageInfo info)
	{
	}

	// Token: 0x04001802 RID: 6146
	private const string CancelCraftingRPC = "CRFX";

	// Token: 0x04001803 RID: 6147
	private const string CraftNetworkUpdateRPC = "CRFU";

	// Token: 0x04001804 RID: 6148
	private const string CraftNetworkClearRPC = "CRFC";

	// Token: 0x04001805 RID: 6149
	private const string StartCraftingRPC = "CRFS";

	// Token: 0x04001806 RID: 6150
	public float _lastWorkBenchTime;

	// Token: 0x04001807 RID: 6151
	protected bool _wasAtWorkbench;

	// Token: 0x04001808 RID: 6152
	private double _lastThinkTime;

	// Token: 0x04001809 RID: 6153
	private CraftingSession crafting;
}
