using System;
using uLink;
using UnityEngine;

// Token: 0x020005D6 RID: 1494
public class CraftingInventory : global::Inventory
{
	// Token: 0x06002FA7 RID: 12199 RVA: 0x000B788C File Offset: 0x000B5A8C
	public void CraftThink()
	{
		if (this.crafting.inProgress)
		{
			double time = global::NetCull.time;
			float num = (float)(time - this._lastThinkTime);
			this.crafting.progressSeconds = Mathf.Clamp(this.crafting.progressSeconds + this.crafting.progressPerSec * num, 0f, this.crafting.duration);
			this._lastThinkTime = time;
		}
	}

	// Token: 0x06002FA8 RID: 12200 RVA: 0x000B78FC File Offset: 0x000B5AFC
	public bool ValidateCraftRequirements(global::BlueprintDataBlock bp)
	{
		return !bp.RequireWorkbench || this.AtWorkBench();
	}

	// Token: 0x06002FA9 RID: 12201 RVA: 0x000B791C File Offset: 0x000B5B1C
	public bool AtWorkBench()
	{
		return this._lastWorkBenchTime < 0f;
	}

	// Token: 0x06002FAA RID: 12202 RVA: 0x000B792C File Offset: 0x000B5B2C
	[RPC]
	public void wbi(bool at, uLink.NetworkMessageInfo info)
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

	// Token: 0x17000A21 RID: 2593
	// (get) Token: 0x06002FAB RID: 12203 RVA: 0x000B7950 File Offset: 0x000B5B50
	public new bool isCraftingInventory
	{
		get
		{
			return true;
		}
	}

	// Token: 0x17000A22 RID: 2594
	// (get) Token: 0x06002FAC RID: 12204 RVA: 0x000B7954 File Offset: 0x000B5B54
	public new bool isCrafting
	{
		get
		{
			return this.crafting.inProgress;
		}
	}

	// Token: 0x17000A23 RID: 2595
	// (get) Token: 0x06002FAD RID: 12205 RVA: 0x000B7964 File Offset: 0x000B5B64
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

	// Token: 0x17000A24 RID: 2596
	// (get) Token: 0x06002FAE RID: 12206 RVA: 0x000B799C File Offset: 0x000B5B9C
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

	// Token: 0x17000A25 RID: 2597
	// (get) Token: 0x06002FAF RID: 12207 RVA: 0x000B79D4 File Offset: 0x000B5BD4
	public float craftingSpeedPerSec
	{
		get
		{
			return this.crafting.progressPerSec;
		}
	}

	// Token: 0x06002FB0 RID: 12208 RVA: 0x000B79E4 File Offset: 0x000B5BE4
	private static global::BlueprintDataBlock FindBlueprint(int uniqueID)
	{
		if (uniqueID == 0)
		{
			return null;
		}
		return (global::BlueprintDataBlock)global::DatablockDictionary.GetByUniqueID(uniqueID);
	}

	// Token: 0x06002FB1 RID: 12209 RVA: 0x000B79FC File Offset: 0x000B5BFC
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

	// Token: 0x06002FB2 RID: 12210 RVA: 0x000B7A40 File Offset: 0x000B5C40
	public bool StartCrafting(global::BlueprintDataBlock blueprint, int amount)
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

	// Token: 0x06002FB3 RID: 12211 RVA: 0x000B7A8C File Offset: 0x000B5C8C
	protected void UpdateCrafting(global::BlueprintDataBlock blueprint, int amount, float start, float dur, float progress, float progresspersec)
	{
		Debug.Log(string.Format("Craft network update :{0}:", (!blueprint) ? "NONE" : blueprint.name), this);
		this._lastThinkTime = global::NetCull.time;
		this.crafting.blueprint = blueprint;
		this.crafting.inProgress = blueprint;
		this.crafting.startTime = start;
		this.crafting.duration = dur;
		this.crafting.progressSeconds = progress;
		this.crafting.progressPerSec = progresspersec;
		this.crafting.amount = amount;
		this.Refresh();
	}

	// Token: 0x06002FB4 RID: 12212 RVA: 0x000B7B34 File Offset: 0x000B5D34
	[global::NGCRPCSkip]
	[RPC]
	protected void CRFX()
	{
	}

	// Token: 0x06002FB5 RID: 12213 RVA: 0x000B7B38 File Offset: 0x000B5D38
	[global::NGCRPCSkip]
	[RPC]
	protected void CRFU(float start, float dur, float progresspersec, float progress, int blueprintUniqueID, int amount)
	{
		this.UpdateCrafting(global::CraftingInventory.FindBlueprint(blueprintUniqueID), amount, start, dur, progress, progresspersec);
	}

	// Token: 0x06002FB6 RID: 12214 RVA: 0x000B7B5C File Offset: 0x000B5D5C
	[global::NGCRPCSkip]
	[RPC]
	protected void CRFC()
	{
		this.UpdateCrafting(null, 0, 0f, 0f, 0f, 0f);
	}

	// Token: 0x06002FB7 RID: 12215 RVA: 0x000B7B7C File Offset: 0x000B5D7C
	[RPC]
	[global::NGCRPCSkip]
	protected void CRFS(int amount, int blueprintUID, uLink.NetworkMessageInfo info)
	{
	}

	// Token: 0x040019CE RID: 6606
	private const string CancelCraftingRPC = "CRFX";

	// Token: 0x040019CF RID: 6607
	private const string CraftNetworkUpdateRPC = "CRFU";

	// Token: 0x040019D0 RID: 6608
	private const string CraftNetworkClearRPC = "CRFC";

	// Token: 0x040019D1 RID: 6609
	private const string StartCraftingRPC = "CRFS";

	// Token: 0x040019D2 RID: 6610
	public float _lastWorkBenchTime;

	// Token: 0x040019D3 RID: 6611
	protected bool _wasAtWorkbench;

	// Token: 0x040019D4 RID: 6612
	private double _lastThinkTime;

	// Token: 0x040019D5 RID: 6613
	private global::CraftingSession crafting;
}
