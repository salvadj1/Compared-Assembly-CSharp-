using System;
using RustProto;
using UnityEngine;

// Token: 0x02000180 RID: 384
[AddComponentMenu("ID/Local/Take Damage")]
public class TakeDamage : IDLocal, global::IServerSaveable
{
	// Token: 0x06000B6F RID: 2927 RVA: 0x0002D150 File Offset: 0x0002B350
	public virtual void SetGodMode(bool on)
	{
		this.takenodamage = on;
	}

	// Token: 0x06000B70 RID: 2928 RVA: 0x0002D15C File Offset: 0x0002B35C
	private static float HealthAliveValueClamp(float newHealth)
	{
		return (newHealth >= 0.001f) ? newHealth : 0.001f;
	}

	// Token: 0x06000B71 RID: 2929 RVA: 0x0002D174 File Offset: 0x0002B374
	public bool ShouldPlayHitNotification()
	{
		return this.playsHitNotification && this.alive;
	}

	// Token: 0x06000B72 RID: 2930 RVA: 0x0002D18C File Offset: 0x0002B38C
	public void MarkDamageTime()
	{
		this._lastDamageTime = Time.time;
	}

	// Token: 0x06000B73 RID: 2931 RVA: 0x0002D19C File Offset: 0x0002B39C
	public float TimeSinceHurt()
	{
		return Time.time - this._lastDamageTime;
	}

	// Token: 0x06000B74 RID: 2932 RVA: 0x0002D1AC File Offset: 0x0002B3AC
	public static string DamageIndexToString(int index)
	{
		return global::TakeDamage.DamageIndexToString((global::DamageTypeIndex)index);
	}

	// Token: 0x06000B75 RID: 2933 RVA: 0x0002D1B4 File Offset: 0x0002B3B4
	public static string DamageIndexToString(global::DamageTypeIndex index)
	{
		string result;
		switch (index)
		{
		case global::DamageTypeIndex.damage_bullet:
			result = "Bullet";
			break;
		case global::DamageTypeIndex.damage_melee:
			result = "Melee";
			break;
		case global::DamageTypeIndex.damage_explosion:
			result = "Explosion";
			break;
		case global::DamageTypeIndex.damage_radiation:
			result = "Radiation";
			break;
		case global::DamageTypeIndex.damage_cold:
			result = "Cold";
			break;
		default:
			result = "Generic";
			break;
		}
		return result;
	}

	// Token: 0x17000327 RID: 807
	// (get) Token: 0x06000B76 RID: 2934 RVA: 0x0002D228 File Offset: 0x0002B428
	// (set) Token: 0x06000B77 RID: 2935 RVA: 0x0002D230 File Offset: 0x0002B430
	public float health
	{
		get
		{
			return this._health;
		}
		set
		{
			this._health = value;
		}
	}

	// Token: 0x17000328 RID: 808
	// (get) Token: 0x06000B78 RID: 2936 RVA: 0x0002D23C File Offset: 0x0002B43C
	public float healthFraction
	{
		get
		{
			return this._health / this._maxHealth;
		}
	}

	// Token: 0x17000329 RID: 809
	// (get) Token: 0x06000B79 RID: 2937 RVA: 0x0002D24C File Offset: 0x0002B44C
	public float healthLoss
	{
		get
		{
			return this._maxHealth - this.health;
		}
	}

	// Token: 0x1700032A RID: 810
	// (get) Token: 0x06000B7A RID: 2938 RVA: 0x0002D25C File Offset: 0x0002B45C
	public float healthLossFraction
	{
		get
		{
			return 1f - this._health / this._maxHealth;
		}
	}

	// Token: 0x1700032B RID: 811
	// (get) Token: 0x06000B7B RID: 2939 RVA: 0x0002D274 File Offset: 0x0002B474
	// (set) Token: 0x06000B7C RID: 2940 RVA: 0x0002D27C File Offset: 0x0002B47C
	public float maxHealth
	{
		get
		{
			return this._maxHealth;
		}
		set
		{
			this._maxHealth = value;
		}
	}

	// Token: 0x1700032C RID: 812
	// (get) Token: 0x06000B7D RID: 2941 RVA: 0x0002D288 File Offset: 0x0002B488
	public bool alive
	{
		get
		{
			return this.health > 0f;
		}
	}

	// Token: 0x1700032D RID: 813
	// (get) Token: 0x06000B7E RID: 2942 RVA: 0x0002D298 File Offset: 0x0002B498
	public bool dead
	{
		get
		{
			return this.health <= 0f;
		}
	}

	// Token: 0x06000B7F RID: 2943 RVA: 0x0002D2AC File Offset: 0x0002B4AC
	protected void Awake()
	{
		this._maxHealth = (this._health = global::TakeDamage.HealthAliveValueClamp(this._maxHealth));
	}

	// Token: 0x06000B80 RID: 2944 RVA: 0x0002D2D4 File Offset: 0x0002B4D4
	public global::RepairEvent Heal(IDBase healer, float amount)
	{
		global::RepairEvent result;
		this.Heal(healer, amount, out result);
		return result;
	}

	// Token: 0x06000B81 RID: 2945 RVA: 0x0002D2F0 File Offset: 0x0002B4F0
	public global::RepairStatus Heal(IDBase healer, float amount, out global::RepairEvent repair)
	{
		repair.doner = healer;
		repair.receiver = this;
		repair.givenAmount = amount;
		if (amount <= 0f)
		{
			repair.status = global::RepairStatus.Failed;
			repair.usedAmount = 0f;
			return global::RepairStatus.Failed;
		}
		if (this.dead)
		{
			repair.status = global::RepairStatus.FailedUnreparable;
			repair.usedAmount = 0f;
		}
		else if (this._health == this._maxHealth)
		{
			repair.status = global::RepairStatus.FailedFull;
			repair.usedAmount = 0f;
		}
		else if (this._health > this._maxHealth - amount)
		{
			this._health = this._maxHealth;
			repair.usedAmount = this._maxHealth - this._health;
			repair.status = global::RepairStatus.AppliedPartial;
		}
		else
		{
			this._health += amount;
			repair.usedAmount = repair.givenAmount;
			if (this._health == this._maxHealth)
			{
				repair.status = global::RepairStatus.AppliedFull;
			}
			else
			{
				repair.status = global::RepairStatus.Applied;
			}
		}
		if (this.sendMessageRepair)
		{
			base.SendMessage("OnRepair", repair, 1);
		}
		return repair.status;
	}

	// Token: 0x06000B82 RID: 2946 RVA: 0x0002D420 File Offset: 0x0002B620
	public static global::LifeStatus Hurt(IDBase attacker, IDBase victim, global::TakeDamage.Quantity damageQuantity, out global::DamageEvent damage, object extraData = null)
	{
		return global::TakeDamage.HurtShared(attacker, victim, damageQuantity, out damage, extraData);
	}

	// Token: 0x06000B83 RID: 2947 RVA: 0x0002D430 File Offset: 0x0002B630
	public static global::LifeStatus Hurt(IDBase attacker, IDBase victim, global::TakeDamage.Quantity damageQuantity, object extraData = null)
	{
		return global::TakeDamage.HurtShared(attacker, victim, damageQuantity, extraData);
	}

	// Token: 0x06000B84 RID: 2948 RVA: 0x0002D43C File Offset: 0x0002B63C
	public static global::LifeStatus Kill(IDBase attacker, IDBase victim, out global::DamageEvent damage, object extraData = null)
	{
		return global::TakeDamage.HurtShared(attacker, victim, global::TakeDamage.Quantity.AllHealth, out damage, extraData);
	}

	// Token: 0x06000B85 RID: 2949 RVA: 0x0002D44C File Offset: 0x0002B64C
	public static global::LifeStatus Kill(IDBase attacker, IDBase victim, object extraData = null)
	{
		return global::TakeDamage.HurtShared(attacker, victim, global::TakeDamage.Quantity.AllHealth, extraData);
	}

	// Token: 0x06000B86 RID: 2950 RVA: 0x0002D45C File Offset: 0x0002B65C
	public static global::LifeStatus HurtSelf(IDBase victim, global::TakeDamage.Quantity damageQuantity, out global::DamageEvent damage, object extraData = null)
	{
		return global::TakeDamage.HurtShared(victim, victim, damageQuantity, out damage, extraData);
	}

	// Token: 0x06000B87 RID: 2951 RVA: 0x0002D468 File Offset: 0x0002B668
	public static global::LifeStatus HurtSelf(IDBase victim, global::TakeDamage.Quantity damageQuantity, object extraData = null)
	{
		return global::TakeDamage.HurtShared(victim, victim, damageQuantity, extraData);
	}

	// Token: 0x06000B88 RID: 2952 RVA: 0x0002D474 File Offset: 0x0002B674
	public static global::LifeStatus KillSelf(IDBase victim, object extraData = null)
	{
		return global::TakeDamage.HurtShared(victim, victim, global::TakeDamage.Quantity.AllHealth, extraData);
	}

	// Token: 0x06000B89 RID: 2953 RVA: 0x0002D484 File Offset: 0x0002B684
	public static global::LifeStatus KillSelf(IDBase victim, out global::DamageEvent damage, object extraData = null)
	{
		return global::TakeDamage.HurtShared(victim, victim, global::TakeDamage.Quantity.AllHealth, out damage, extraData);
	}

	// Token: 0x06000B8A RID: 2954 RVA: 0x0002D494 File Offset: 0x0002B694
	private static global::LifeStatus HurtShared(IDBase attacker, IDBase victim, global::TakeDamage.Quantity damageQuantity, out global::DamageEvent damage, object extraData = null)
	{
		if (victim)
		{
			IDMain idMain = victim.idMain;
			if (idMain)
			{
				global::TakeDamage takeDamage;
				if (idMain is global::Character)
				{
					takeDamage = ((global::Character)idMain).takeDamage;
				}
				else
				{
					takeDamage = idMain.GetLocal<global::TakeDamage>();
				}
				if (takeDamage && !takeDamage.takenodamage)
				{
					takeDamage.MarkDamageTime();
					damage.victim.id = victim;
					damage.attacker.id = attacker;
					damage.amount = damageQuantity.value;
					damage.sender = takeDamage;
					damage.status = ((!takeDamage.dead) ? global::LifeStatus.IsAlive : global::LifeStatus.IsDead);
					damage.damageTypes = (global::DamageTypeFlags)0;
					damage.extraData = extraData;
					if ((int)damageQuantity.Unit == -1)
					{
						takeDamage.ApplyDamageTypeList(ref damage, damageQuantity.list);
					}
					takeDamage.Hurt(ref damage);
					return damage.status;
				}
			}
		}
		damage.victim.id = null;
		damage.attacker.id = null;
		damage.amount = 0f;
		damage.sender = null;
		damage.damageTypes = (global::DamageTypeFlags)0;
		damage.status = global::LifeStatus.Failed;
		damage.extraData = extraData;
		return global::LifeStatus.Failed;
	}

	// Token: 0x06000B8B RID: 2955 RVA: 0x0002D5BC File Offset: 0x0002B7BC
	private static global::LifeStatus HurtShared(IDBase attacker, IDBase victim, global::TakeDamage.Quantity damageQuantity, object extraData = null)
	{
		global::DamageEvent damageEvent;
		return global::TakeDamage.HurtShared(attacker, victim, damageQuantity, out damageEvent, extraData);
	}

	// Token: 0x06000B8C RID: 2956 RVA: 0x0002D5D4 File Offset: 0x0002B7D4
	protected virtual void ApplyDamageTypeList(ref global::DamageEvent damage, global::DamageTypeList damageTypes)
	{
		for (int i = 0; i < 6; i++)
		{
			if (!Mathf.Approximately(damageTypes[i], 0f))
			{
				damage.damageTypes |= (global::DamageTypeFlags)(1 << i);
				damage.amount += damageTypes[i];
			}
		}
	}

	// Token: 0x06000B8D RID: 2957 RVA: 0x0002D630 File Offset: 0x0002B830
	protected virtual global::LifeStatus Hurt(ref global::DamageEvent damage)
	{
		if (this.dead)
		{
			damage.status = global::LifeStatus.IsDead;
		}
		else if (this.health > damage.amount)
		{
			damage.status = global::LifeStatus.IsAlive;
		}
		else
		{
			damage.status = global::LifeStatus.WasKilled;
		}
		this.ProcessDamageEvent(ref damage);
		if (this.ShouldRelayDamageEvent(ref damage))
		{
			base.SendMessage("OnHurt", damage, 1);
		}
		if (damage.status == global::LifeStatus.WasKilled)
		{
			base.SendMessage("OnKilled", damage, 1);
		}
		return damage.status;
	}

	// Token: 0x06000B8E RID: 2958 RVA: 0x0002D6CC File Offset: 0x0002B8CC
	protected void ProcessDamageEvent(ref global::DamageEvent damage)
	{
		if (this.takenodamage)
		{
			return;
		}
		global::LifeStatus status = damage.status;
		if (status != global::LifeStatus.IsAlive)
		{
			if (status == global::LifeStatus.WasKilled)
			{
				this._health = 0f;
			}
		}
		else
		{
			this._health -= damage.amount;
		}
	}

	// Token: 0x06000B8F RID: 2959 RVA: 0x0002D72C File Offset: 0x0002B92C
	protected bool ShouldRelayDamageEvent(ref global::DamageEvent damage)
	{
		switch (damage.status)
		{
		case global::LifeStatus.IsAlive:
			return this.sendMessageWhenAlive;
		case global::LifeStatus.WasKilled:
			return this.sendMessageWhenKilled;
		case global::LifeStatus.IsDead:
			return this.sendMessageWhenDead;
		default:
			Debug.LogWarning("Unhandled LifeStatus " + damage.status, this);
			return false;
		}
	}

	// Token: 0x06000B90 RID: 2960 RVA: 0x0002D788 File Offset: 0x0002B988
	public override string ToString()
	{
		return string.Format("[{0}: health={1}]", base.ToString(), this.health);
	}

	// Token: 0x06000B91 RID: 2961 RVA: 0x0002D7A8 File Offset: 0x0002B9A8
	public virtual void ServerFrame()
	{
	}

	// Token: 0x06000B92 RID: 2962 RVA: 0x0002D7AC File Offset: 0x0002B9AC
	public virtual void SaveVitals(ref RustProto.Vitals.Builder vitals)
	{
		vitals.SetHealth(this.health);
	}

	// Token: 0x06000B93 RID: 2963 RVA: 0x0002D7BC File Offset: 0x0002B9BC
	public virtual void LoadVitals(RustProto.Vitals vitals)
	{
		this.health = vitals.Health;
		if (this.health <= 0f)
		{
			Debug.Log("LOAD VITALS - HEALTH WAS " + this.health);
			this.health = 1f;
		}
	}

	// Token: 0x040007C8 RID: 1992
	public const string DamageMessage = "OnHurt";

	// Token: 0x040007C9 RID: 1993
	public const string KillMessage = "OnKilled";

	// Token: 0x040007CA RID: 1994
	public const string RepairMessage = "OnRepair";

	// Token: 0x040007CB RID: 1995
	public const SendMessageOptions DamageMessageOptions = 1;

	// Token: 0x040007CC RID: 1996
	public const SendMessageOptions RepairMessageOptions = 1;

	// Token: 0x040007CD RID: 1997
	public const float kMinimumSetHealthValueWhenAlive = 0.001f;

	// Token: 0x040007CE RID: 1998
	protected float _lastDamageTime;

	// Token: 0x040007CF RID: 1999
	private bool takenodamage;

	// Token: 0x040007D0 RID: 2000
	[SerializeField]
	private float _maxHealth = 100f;

	// Token: 0x040007D1 RID: 2001
	private float _health;

	// Token: 0x040007D2 RID: 2002
	public bool playsHitNotification;

	// Token: 0x040007D3 RID: 2003
	public bool sendMessageWhenAlive = true;

	// Token: 0x040007D4 RID: 2004
	public bool sendMessageWhenKilled = true;

	// Token: 0x040007D5 RID: 2005
	public bool sendMessageWhenDead = true;

	// Token: 0x040007D6 RID: 2006
	public bool sendMessageRepair = true;

	// Token: 0x02000181 RID: 385
	public enum Unit : sbyte
	{
		// Token: 0x040007D8 RID: 2008
		Unspecified,
		// Token: 0x040007D9 RID: 2009
		HealthPoints,
		// Token: 0x040007DA RID: 2010
		AllHealth,
		// Token: 0x040007DB RID: 2011
		List = -1
	}

	// Token: 0x02000182 RID: 386
	public struct Quantity
	{
		// Token: 0x06000B94 RID: 2964 RVA: 0x0002D80C File Offset: 0x0002BA0C
		private Quantity(global::TakeDamage.Unit Measurement, global::DamageTypeList DamageTypeList, float Value)
		{
			this.Unit = Measurement;
			this.list = DamageTypeList;
			this.value = Value;
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000B95 RID: 2965 RVA: 0x0002D824 File Offset: 0x0002BA24
		public static global::TakeDamage.Quantity AllHealth
		{
			get
			{
				return new global::TakeDamage.Quantity(global::TakeDamage.Unit.AllHealth, null, float.PositiveInfinity);
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000B96 RID: 2966 RVA: 0x0002D834 File Offset: 0x0002BA34
		public global::DamageTypeList DamageTypeList
		{
			get
			{
				if ((int)this.Unit > 0)
				{
					throw new InvalidOperationException("Quantity is of HealthPoints");
				}
				return this.list;
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000B97 RID: 2967 RVA: 0x0002D854 File Offset: 0x0002BA54
		public bool IsDamageTypeList
		{
			get
			{
				return (int)this.Unit == -1;
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x0002D860 File Offset: 0x0002BA60
		public float HealthPoints
		{
			get
			{
				if ((int)this.Unit < -1)
				{
					throw new InvalidOperationException("Quantity is of DamageTypeList");
				}
				return this.value;
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000B99 RID: 2969 RVA: 0x0002D880 File Offset: 0x0002BA80
		public bool IsHealthPoints
		{
			get
			{
				return (int)this.Unit > 0;
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x0002D88C File Offset: 0x0002BA8C
		public bool IsAllHealthPoints
		{
			get
			{
				return (int)this.Unit == 2;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000B9B RID: 2971 RVA: 0x0002D898 File Offset: 0x0002BA98
		public bool Specified
		{
			get
			{
				return (int)this.Unit != 0;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000B9C RID: 2972 RVA: 0x0002D8A8 File Offset: 0x0002BAA8
		public object BoxedValue
		{
			get
			{
				return ((int)this.Unit > 0) ? this.value : this.list;
			}
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x0002D8D0 File Offset: 0x0002BAD0
		public override string ToString()
		{
			return string.Format("[{0}:{1}]", this.Unit, this.BoxedValue);
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x0002D8F0 File Offset: 0x0002BAF0
		public static implicit operator global::TakeDamage.Quantity(int HealthPoints)
		{
			return new global::TakeDamage.Quantity((HealthPoints != 0) ? global::TakeDamage.Unit.HealthPoints : global::TakeDamage.Unit.Unspecified, null, (float)((HealthPoints >= 0) ? HealthPoints : (-(float)HealthPoints)));
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x0002D918 File Offset: 0x0002BB18
		public static implicit operator global::TakeDamage.Quantity(float HealthPoints)
		{
			return new global::TakeDamage.Quantity((HealthPoints != 0f) ? ((!float.IsInfinity(HealthPoints)) ? global::TakeDamage.Unit.HealthPoints : global::TakeDamage.Unit.AllHealth) : global::TakeDamage.Unit.Unspecified, null, HealthPoints);
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0002D950 File Offset: 0x0002BB50
		public static implicit operator global::TakeDamage.Quantity(global::DamageTypeList DamageTypeList)
		{
			return new global::TakeDamage.Quantity((!object.ReferenceEquals(DamageTypeList, null)) ? global::TakeDamage.Unit.List : global::TakeDamage.Unit.Unspecified, DamageTypeList, 0f);
		}

		// Token: 0x040007DC RID: 2012
		public readonly global::TakeDamage.Unit Unit;

		// Token: 0x040007DD RID: 2013
		internal readonly float value;

		// Token: 0x040007DE RID: 2014
		internal readonly global::DamageTypeList list;
	}
}
