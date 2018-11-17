using System;
using Facepunch;
using RustProto;
using UnityEngine;

// Token: 0x0200048F RID: 1167
public class Metabolism : IDLocalCharacter
{
	// Token: 0x06002985 RID: 10629 RVA: 0x000A301C File Offset: 0x000A121C
	public void SetTargetActivityLevel(float level)
	{
		this._targetActivityLevel = level;
	}

	// Token: 0x06002986 RID: 10630 RVA: 0x000A3028 File Offset: 0x000A1228
	public float GetActivityLevel()
	{
		return this._activityLevel;
	}

	// Token: 0x06002987 RID: 10631 RVA: 0x000A3030 File Offset: 0x000A1230
	public float GetNextConsumeTime()
	{
		return this._lastConsumeTime + 3f;
	}

	// Token: 0x06002988 RID: 10632 RVA: 0x000A3040 File Offset: 0x000A1240
	public void MarkConsumptionTime()
	{
		this._lastConsumeTime = Time.time;
	}

	// Token: 0x06002989 RID: 10633 RVA: 0x000A3050 File Offset: 0x000A1250
	public bool CanConsumeYet()
	{
		return this.GetNextConsumeTime() < Time.time;
	}

	// Token: 0x0600298A RID: 10634 RVA: 0x000A3060 File Offset: 0x000A1260
	public float GetCalorieLevel()
	{
		return this.caloricLevel;
	}

	// Token: 0x0600298B RID: 10635 RVA: 0x000A3068 File Offset: 0x000A1268
	public float GetRemainingCaloricSpace()
	{
		return this.maxCaloricLevel - this.caloricLevel;
	}

	// Token: 0x0600298C RID: 10636 RVA: 0x000A3078 File Offset: 0x000A1278
	public float GetRadLevel()
	{
		return this.radiationLevel;
	}

	// Token: 0x0600298D RID: 10637 RVA: 0x000A3080 File Offset: 0x000A1280
	public bool IsCold()
	{
		return this.coreTemperature < 0f;
	}

	// Token: 0x0600298E RID: 10638 RVA: 0x000A3090 File Offset: 0x000A1290
	public bool HasRadiationPoisoning()
	{
		return this.radiationLevel > 500f;
	}

	// Token: 0x0600298F RID: 10639 RVA: 0x000A30A0 File Offset: 0x000A12A0
	public bool IsPoisoned()
	{
		return this.poisonLevel > 1f;
	}

	// Token: 0x06002990 RID: 10640 RVA: 0x000A30B0 File Offset: 0x000A12B0
	private void Awake()
	{
		CharacterMetabolismTrait trait = base.GetTrait<CharacterMetabolismTrait>();
		if (trait)
		{
			this.hungerDamagePerMin = trait.hungerDamagePerMin;
			this.selfTick = trait.selfTick;
			this.tickRate = trait.tickRate;
		}
		this._lastTickTime = Time.time;
	}

	// Token: 0x06002991 RID: 10641 RVA: 0x000A3100 File Offset: 0x000A1300
	public void MakeDirty()
	{
		this._dirty = true;
	}

	// Token: 0x06002992 RID: 10642 RVA: 0x000A310C File Offset: 0x000A130C
	public void MakeClean()
	{
		this._dirty = false;
	}

	// Token: 0x06002993 RID: 10643 RVA: 0x000A3118 File Offset: 0x000A1318
	public bool IsDirty()
	{
		return this._dirty;
	}

	// Token: 0x06002994 RID: 10644 RVA: 0x000A3120 File Offset: 0x000A1320
	public void SubtractCalories(float numCalories)
	{
		this.caloricLevel -= numCalories;
		if (this.caloricLevel < 0f)
		{
			this.caloricLevel = 0f;
		}
		this.MakeDirty();
	}

	// Token: 0x06002995 RID: 10645 RVA: 0x000A3154 File Offset: 0x000A1354
	public void AddCalories(float numCalories)
	{
		this.caloricLevel += numCalories;
		if (this.caloricLevel > this.maxCaloricLevel)
		{
			this.caloricLevel = this.maxCaloricLevel;
		}
		this.MakeDirty();
	}

	// Token: 0x06002996 RID: 10646 RVA: 0x000A3188 File Offset: 0x000A1388
	public void AddWater(float litres)
	{
		this.waterLevelLitre += litres;
		if (this.waterLevelLitre > this.maxWaterLevelLitre)
		{
			this.waterLevelLitre = this.maxWaterLevelLitre;
		}
		this.MakeDirty();
	}

	// Token: 0x06002997 RID: 10647 RVA: 0x000A31BC File Offset: 0x000A13BC
	public void AddAntiRad(float addAntiRad)
	{
		this.antiRads += addAntiRad;
		this.MakeDirty();
	}

	// Token: 0x06002998 RID: 10648 RVA: 0x000A31D4 File Offset: 0x000A13D4
	public void AddRads(float rads)
	{
		this.radiationLevel += rads;
		this.MakeDirty();
	}

	// Token: 0x06002999 RID: 10649 RVA: 0x000A31EC File Offset: 0x000A13EC
	public void AddPoison(float amount)
	{
		this.poisonLevel += amount;
		if (Time.time > this.nextVomitTime)
		{
			this.nextVomitTime = Time.time + 5f;
		}
		this.MakeDirty();
	}

	// Token: 0x0600299A RID: 10650 RVA: 0x000A3224 File Offset: 0x000A1424
	public void SubtractPosion(float amount)
	{
		this.poisonLevel -= amount;
		if (this.poisonLevel <= 0f)
		{
			this.nextVomitTime = 0f;
		}
		this.MakeDirty();
	}

	// Token: 0x0600299B RID: 10651 RVA: 0x000A3258 File Offset: 0x000A1458
	public void MarkWarm()
	{
		this._lastWarmTime = Time.time;
	}

	// Token: 0x0600299C RID: 10652 RVA: 0x000A3268 File Offset: 0x000A1468
	public bool IsWarm()
	{
		return Time.time - this._lastWarmTime <= 1f;
	}

	// Token: 0x0600299D RID: 10653 RVA: 0x000A3280 File Offset: 0x000A1480
	private void MetabolicFrame()
	{
		this.MetabolicUpdateFrame();
	}

	// Token: 0x0600299E RID: 10654 RVA: 0x000A328C File Offset: 0x000A148C
	public LifeStatus MetabolicUpdateFrame()
	{
		return (!base.alive) ? LifeStatus.IsDead : LifeStatus.IsAlive;
	}

	// Token: 0x0600299F RID: 10655 RVA: 0x000A32B0 File Offset: 0x000A14B0
	[RPC]
	public void Vomit()
	{
		if (Metabolism.vomitSound == null)
		{
			Bundling.Load<AudioClip>("content/shared/sfx/vomit", out Metabolism.vomitSound);
		}
		Metabolism.vomitSound.Play(1f);
	}

	// Token: 0x060029A0 RID: 10656 RVA: 0x000A32E4 File Offset: 0x000A14E4
	public void MarkDamageTime()
	{
		this._lastDamageTime = Time.time;
	}

	// Token: 0x060029A1 RID: 10657 RVA: 0x000A32F4 File Offset: 0x000A14F4
	public float TimeSinceHurt()
	{
		return Time.time - this._lastDamageTime;
	}

	// Token: 0x060029A2 RID: 10658 RVA: 0x000A3304 File Offset: 0x000A1504
	public void OnHurt(DamageEvent damage)
	{
		this.MarkDamageTime();
	}

	// Token: 0x060029A3 RID: 10659 RVA: 0x000A330C File Offset: 0x000A150C
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

	// Token: 0x060029A4 RID: 10660 RVA: 0x000A33A0 File Offset: 0x000A15A0
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
		RPOS.MetabolismUpdate();
	}

	// Token: 0x060029A5 RID: 10661 RVA: 0x000A3414 File Offset: 0x000A1614
	public void SaveVitals(ref Vitals.Builder vitals)
	{
		vitals.SetCalories(this.caloricLevel);
		vitals.SetHydration(this.waterLevelLitre);
		vitals.SetRadiation(this.radiationLevel);
		vitals.SetRadiationAnti(this.antiRads);
		vitals.SetTemperature(this.coreTemperature);
	}

	// Token: 0x060029A6 RID: 10662 RVA: 0x000A3468 File Offset: 0x000A1668
	public void LoadVitals(Vitals vitals)
	{
		this.caloricLevel = vitals.Calories;
		this.waterLevelLitre = vitals.Hydration;
		this.radiationLevel = vitals.Radiation;
		this.antiRads = vitals.RadiationAnti;
		this.coreTemperature = vitals.Temperature;
	}

	// Token: 0x0400156D RID: 5485
	private bool _dirty;

	// Token: 0x0400156E RID: 5486
	private float _lastTickTime;

	// Token: 0x0400156F RID: 5487
	[NonSerialized]
	public float tickRate = 3f;

	// Token: 0x04001570 RID: 5488
	[NonSerialized]
	public bool selfTick;

	// Token: 0x04001571 RID: 5489
	[NonSerialized]
	public float hungerDamagePerMin = 5f;

	// Token: 0x04001572 RID: 5490
	private float caloricLevel = 1250f;

	// Token: 0x04001573 RID: 5491
	private float maxCaloricLevel = 3000f;

	// Token: 0x04001574 RID: 5492
	private float caloriesPerHP = 5f;

	// Token: 0x04001575 RID: 5493
	private float starvingDamagePerMin = 10f;

	// Token: 0x04001576 RID: 5494
	private float waterLevelLitre = 30f;

	// Token: 0x04001577 RID: 5495
	private float maxWaterLevelLitre = 30f;

	// Token: 0x04001578 RID: 5496
	private float caloricMetabolicRate = 300f;

	// Token: 0x04001579 RID: 5497
	private float caloricMetabolicRateMax = 3000f;

	// Token: 0x0400157A RID: 5498
	private float hydrationMetablicRate = 0.125f;

	// Token: 0x0400157B RID: 5499
	private float sweatWaterLossMax = 1.5f;

	// Token: 0x0400157C RID: 5500
	private float radMetabolizationRate = 800f;

	// Token: 0x0400157D RID: 5501
	private float damagePerRad = 0.06f;

	// Token: 0x0400157E RID: 5502
	private float radiationLevel;

	// Token: 0x0400157F RID: 5503
	private float maxRadiationLevel = 3000f;

	// Token: 0x04001580 RID: 5504
	private float antiRads;

	// Token: 0x04001581 RID: 5505
	private float antiRadUsePerMin = 3000f;

	// Token: 0x04001582 RID: 5506
	private float _activityLevel;

	// Token: 0x04001583 RID: 5507
	private float _targetActivityLevel;

	// Token: 0x04001584 RID: 5508
	private float _lastConsumeTime;

	// Token: 0x04001585 RID: 5509
	private float coreTemperature;

	// Token: 0x04001586 RID: 5510
	private float _lastWarmTime = -1000f;

	// Token: 0x04001587 RID: 5511
	private float lastVomitTime;

	// Token: 0x04001588 RID: 5512
	private float nextVomitTime;

	// Token: 0x04001589 RID: 5513
	private float poisonLevel;

	// Token: 0x0400158A RID: 5514
	private float _lastDamageTime;

	// Token: 0x0400158B RID: 5515
	private static bool dmg_metabolism = true;

	// Token: 0x0400158C RID: 5516
	private static AudioClip vomitSound;
}
