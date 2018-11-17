using System;
using Facepunch;
using RustProto;
using UnityEngine;

// Token: 0x0200054A RID: 1354
public class Metabolism : global::IDLocalCharacter
{
	// Token: 0x06002D37 RID: 11575 RVA: 0x000AADB4 File Offset: 0x000A8FB4
	public void SetTargetActivityLevel(float level)
	{
		this._targetActivityLevel = level;
	}

	// Token: 0x06002D38 RID: 11576 RVA: 0x000AADC0 File Offset: 0x000A8FC0
	public float GetActivityLevel()
	{
		return this._activityLevel;
	}

	// Token: 0x06002D39 RID: 11577 RVA: 0x000AADC8 File Offset: 0x000A8FC8
	public float GetNextConsumeTime()
	{
		return this._lastConsumeTime + 3f;
	}

	// Token: 0x06002D3A RID: 11578 RVA: 0x000AADD8 File Offset: 0x000A8FD8
	public void MarkConsumptionTime()
	{
		this._lastConsumeTime = Time.time;
	}

	// Token: 0x06002D3B RID: 11579 RVA: 0x000AADE8 File Offset: 0x000A8FE8
	public bool CanConsumeYet()
	{
		return this.GetNextConsumeTime() < Time.time;
	}

	// Token: 0x06002D3C RID: 11580 RVA: 0x000AADF8 File Offset: 0x000A8FF8
	public float GetCalorieLevel()
	{
		return this.caloricLevel;
	}

	// Token: 0x06002D3D RID: 11581 RVA: 0x000AAE00 File Offset: 0x000A9000
	public float GetRemainingCaloricSpace()
	{
		return this.maxCaloricLevel - this.caloricLevel;
	}

	// Token: 0x06002D3E RID: 11582 RVA: 0x000AAE10 File Offset: 0x000A9010
	public float GetRadLevel()
	{
		return this.radiationLevel;
	}

	// Token: 0x06002D3F RID: 11583 RVA: 0x000AAE18 File Offset: 0x000A9018
	public bool IsCold()
	{
		return this.coreTemperature < 0f;
	}

	// Token: 0x06002D40 RID: 11584 RVA: 0x000AAE28 File Offset: 0x000A9028
	public bool HasRadiationPoisoning()
	{
		return this.radiationLevel > 500f;
	}

	// Token: 0x06002D41 RID: 11585 RVA: 0x000AAE38 File Offset: 0x000A9038
	public bool IsPoisoned()
	{
		return this.poisonLevel > 1f;
	}

	// Token: 0x06002D42 RID: 11586 RVA: 0x000AAE48 File Offset: 0x000A9048
	private void Awake()
	{
		global::CharacterMetabolismTrait trait = base.GetTrait<global::CharacterMetabolismTrait>();
		if (trait)
		{
			this.hungerDamagePerMin = trait.hungerDamagePerMin;
			this.selfTick = trait.selfTick;
			this.tickRate = trait.tickRate;
		}
		this._lastTickTime = Time.time;
	}

	// Token: 0x06002D43 RID: 11587 RVA: 0x000AAE98 File Offset: 0x000A9098
	public void MakeDirty()
	{
		this._dirty = true;
	}

	// Token: 0x06002D44 RID: 11588 RVA: 0x000AAEA4 File Offset: 0x000A90A4
	public void MakeClean()
	{
		this._dirty = false;
	}

	// Token: 0x06002D45 RID: 11589 RVA: 0x000AAEB0 File Offset: 0x000A90B0
	public bool IsDirty()
	{
		return this._dirty;
	}

	// Token: 0x06002D46 RID: 11590 RVA: 0x000AAEB8 File Offset: 0x000A90B8
	public void SubtractCalories(float numCalories)
	{
		this.caloricLevel -= numCalories;
		if (this.caloricLevel < 0f)
		{
			this.caloricLevel = 0f;
		}
		this.MakeDirty();
	}

	// Token: 0x06002D47 RID: 11591 RVA: 0x000AAEEC File Offset: 0x000A90EC
	public void AddCalories(float numCalories)
	{
		this.caloricLevel += numCalories;
		if (this.caloricLevel > this.maxCaloricLevel)
		{
			this.caloricLevel = this.maxCaloricLevel;
		}
		this.MakeDirty();
	}

	// Token: 0x06002D48 RID: 11592 RVA: 0x000AAF20 File Offset: 0x000A9120
	public void AddWater(float litres)
	{
		this.waterLevelLitre += litres;
		if (this.waterLevelLitre > this.maxWaterLevelLitre)
		{
			this.waterLevelLitre = this.maxWaterLevelLitre;
		}
		this.MakeDirty();
	}

	// Token: 0x06002D49 RID: 11593 RVA: 0x000AAF54 File Offset: 0x000A9154
	public void AddAntiRad(float addAntiRad)
	{
		this.antiRads += addAntiRad;
		this.MakeDirty();
	}

	// Token: 0x06002D4A RID: 11594 RVA: 0x000AAF6C File Offset: 0x000A916C
	public void AddRads(float rads)
	{
		this.radiationLevel += rads;
		this.MakeDirty();
	}

	// Token: 0x06002D4B RID: 11595 RVA: 0x000AAF84 File Offset: 0x000A9184
	public void AddPoison(float amount)
	{
		this.poisonLevel += amount;
		if (Time.time > this.nextVomitTime)
		{
			this.nextVomitTime = Time.time + 5f;
		}
		this.MakeDirty();
	}

	// Token: 0x06002D4C RID: 11596 RVA: 0x000AAFBC File Offset: 0x000A91BC
	public void SubtractPosion(float amount)
	{
		this.poisonLevel -= amount;
		if (this.poisonLevel <= 0f)
		{
			this.nextVomitTime = 0f;
		}
		this.MakeDirty();
	}

	// Token: 0x06002D4D RID: 11597 RVA: 0x000AAFF0 File Offset: 0x000A91F0
	public void MarkWarm()
	{
		this._lastWarmTime = Time.time;
	}

	// Token: 0x06002D4E RID: 11598 RVA: 0x000AB000 File Offset: 0x000A9200
	public bool IsWarm()
	{
		return Time.time - this._lastWarmTime <= 1f;
	}

	// Token: 0x06002D4F RID: 11599 RVA: 0x000AB018 File Offset: 0x000A9218
	private void MetabolicFrame()
	{
		this.MetabolicUpdateFrame();
	}

	// Token: 0x06002D50 RID: 11600 RVA: 0x000AB024 File Offset: 0x000A9224
	public global::LifeStatus MetabolicUpdateFrame()
	{
		return (!base.alive) ? global::LifeStatus.IsDead : global::LifeStatus.IsAlive;
	}

	// Token: 0x06002D51 RID: 11601 RVA: 0x000AB048 File Offset: 0x000A9248
	[RPC]
	public void Vomit()
	{
		if (global::Metabolism.vomitSound == null)
		{
			Facepunch.Bundling.Load<AudioClip>("content/shared/sfx/vomit", out global::Metabolism.vomitSound);
		}
		global::Metabolism.vomitSound.Play(1f);
	}

	// Token: 0x06002D52 RID: 11602 RVA: 0x000AB07C File Offset: 0x000A927C
	public void MarkDamageTime()
	{
		this._lastDamageTime = Time.time;
	}

	// Token: 0x06002D53 RID: 11603 RVA: 0x000AB08C File Offset: 0x000A928C
	public float TimeSinceHurt()
	{
		return Time.time - this._lastDamageTime;
	}

	// Token: 0x06002D54 RID: 11604 RVA: 0x000AB09C File Offset: 0x000A929C
	public void OnHurt(global::DamageEvent damage)
	{
		this.MarkDamageTime();
	}

	// Token: 0x06002D55 RID: 11605 RVA: 0x000AB0A4 File Offset: 0x000A92A4
	public void DoNetworkUpdate()
	{
		if (this.IsDirty())
		{
			base.networkView.RPC("RecieveNetwork", base.networkView.owner, new object[]
			{
				this.caloricLevel,
				this.waterLevelLitre,
				this.radiationLevel,
				this.antiRads,
				this.coreTemperature,
				this.poisonLevel
			});
		}
		this.MakeClean();
	}

	// Token: 0x06002D56 RID: 11606 RVA: 0x000AB138 File Offset: 0x000A9338
	[RPC]
	public void RecieveNetwork(float calories, float water, float rad, float anti, float temp, float poison)
	{
		this.caloricLevel = calories;
		this.waterLevelLitre = water;
		this.radiationLevel = rad;
		this.antiRads = anti;
		this.coreTemperature = temp;
		this.poisonLevel = poison;
		if (temp >= 1f)
		{
			this._lastWarmTime = Time.time;
		}
		else if (temp < 0f)
		{
			this._lastWarmTime = -1000f;
		}
		global::RPOS.MetabolismUpdate();
	}

	// Token: 0x06002D57 RID: 11607 RVA: 0x000AB1AC File Offset: 0x000A93AC
	public void SaveVitals(ref RustProto.Vitals.Builder vitals)
	{
		vitals.SetCalories(this.caloricLevel);
		vitals.SetHydration(this.waterLevelLitre);
		vitals.SetRadiation(this.radiationLevel);
		vitals.SetRadiationAnti(this.antiRads);
		vitals.SetTemperature(this.coreTemperature);
	}

	// Token: 0x06002D58 RID: 11608 RVA: 0x000AB200 File Offset: 0x000A9400
	public void LoadVitals(RustProto.Vitals vitals)
	{
		this.caloricLevel = vitals.Calories;
		this.waterLevelLitre = vitals.Hydration;
		this.radiationLevel = vitals.Radiation;
		this.antiRads = vitals.RadiationAnti;
		this.coreTemperature = vitals.Temperature;
	}

	// Token: 0x0400172A RID: 5930
	private bool _dirty;

	// Token: 0x0400172B RID: 5931
	private float _lastTickTime;

	// Token: 0x0400172C RID: 5932
	[NonSerialized]
	public float tickRate = 3f;

	// Token: 0x0400172D RID: 5933
	[NonSerialized]
	public bool selfTick;

	// Token: 0x0400172E RID: 5934
	[NonSerialized]
	public float hungerDamagePerMin = 5f;

	// Token: 0x0400172F RID: 5935
	private float caloricLevel = 1250f;

	// Token: 0x04001730 RID: 5936
	private float maxCaloricLevel = 3000f;

	// Token: 0x04001731 RID: 5937
	private float caloriesPerHP = 5f;

	// Token: 0x04001732 RID: 5938
	private float starvingDamagePerMin = 10f;

	// Token: 0x04001733 RID: 5939
	private float waterLevelLitre = 30f;

	// Token: 0x04001734 RID: 5940
	private float maxWaterLevelLitre = 30f;

	// Token: 0x04001735 RID: 5941
	private float caloricMetabolicRate = 300f;

	// Token: 0x04001736 RID: 5942
	private float caloricMetabolicRateMax = 3000f;

	// Token: 0x04001737 RID: 5943
	private float hydrationMetablicRate = 0.125f;

	// Token: 0x04001738 RID: 5944
	private float sweatWaterLossMax = 1.5f;

	// Token: 0x04001739 RID: 5945
	private float radMetabolizationRate = 800f;

	// Token: 0x0400173A RID: 5946
	private float damagePerRad = 0.06f;

	// Token: 0x0400173B RID: 5947
	private float radiationLevel;

	// Token: 0x0400173C RID: 5948
	private float maxRadiationLevel = 3000f;

	// Token: 0x0400173D RID: 5949
	private float antiRads;

	// Token: 0x0400173E RID: 5950
	private float antiRadUsePerMin = 3000f;

	// Token: 0x0400173F RID: 5951
	private float _activityLevel;

	// Token: 0x04001740 RID: 5952
	private float _targetActivityLevel;

	// Token: 0x04001741 RID: 5953
	private float _lastConsumeTime;

	// Token: 0x04001742 RID: 5954
	private float coreTemperature;

	// Token: 0x04001743 RID: 5955
	private float _lastWarmTime = -1000f;

	// Token: 0x04001744 RID: 5956
	private float lastVomitTime;

	// Token: 0x04001745 RID: 5957
	private float nextVomitTime;

	// Token: 0x04001746 RID: 5958
	private float poisonLevel;

	// Token: 0x04001747 RID: 5959
	private float _lastDamageTime;

	// Token: 0x04001748 RID: 5960
	private static bool dmg_metabolism = true;

	// Token: 0x04001749 RID: 5961
	private static AudioClip vomitSound;
}
