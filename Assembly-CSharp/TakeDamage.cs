using System;
using RustProto;
using UnityEngine;

// Token: 0x02000156 RID: 342
[AddComponentMenu("ID/Local/Take Damage")]
public class TakeDamage : IDLocal, IServerSaveable
{
	// Token: 0x06000A49 RID: 2633 RVA: 0x000293D4 File Offset: 0x000275D4
	public virtual void SetGodMode(bool on)
	{
		this.takenodamage = on;
	}

	// Token: 0x06000A4A RID: 2634 RVA: 0x000293E0 File Offset: 0x000275E0
	private static float HealthAliveValueClamp(float newHealth)
	{
		return (newHealth >= 0.001f) ? newHealth : 0.001f;
	}

	// Token: 0x06000A4B RID: 2635 RVA: 0x000293F8 File Offset: 0x000275F8
	public bool ShouldPlayHitNotification()
	{
		return this.playsHitNotification && this.alive;
	}

	// Token: 0x06000A4C RID: 2636 RVA: 0x00029410 File Offset: 0x00027610
	public void MarkDamageTime()
	{
		this._lastDamageTime = Time.time;
	}

	// Token: 0x06000A4D RID: 2637 RVA: 0x00029420 File Offset: 0x00027620
	public float TimeSinceHurt()
	{
		return Time.time - this._lastDamageTime;
	}

	// Token: 0x06000A4E RID: 2638 RVA: 0x00029430 File Offset: 0x00027630
	public static string DamageIndexToString(int index)
	{
		return TakeDamage.DamageIndexToString((DamageTypeIndex)index);
	}

	// Token: 0x06000A4F RID: 2639 RVA: 0x00029438 File Offset: 0x00027638
	public static string DamageIndexToString(DamageTypeIndex index)
	{
		string result;
		switch (index)
		{
		case DamageTypeIndex.damage_bullet:
			result = "Bullet";
			break;
		case DamageTypeIndex.damage_melee:
			result = "Melee";
			break;
		case DamageTypeIndex.damage_explosion:
			result = "Explosion";
			break;
		case DamageTypeIndex.damage_radiation:
			result = "Radiation";
			break;
		case DamageTypeIndex.damage_cold:
			result = "Cold";
			break;
		default:
			result = "Generic";
			break;
		}
		return result;
	}

	// Token: 0x170002E5 RID: 741
	// (get) Token: 0x06000A50 RID: 2640 RVA: 0x000294AC File Offset: 0x000276AC
	// (set) Token: 0x06000A51 RID: 2641 RVA: 0x000294B4 File Offset: 0x000276B4
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

	// Token: 0x170002E6 RID: 742
	// (get) Token: 0x06000A52 RID: 2642 RVA: 0x000294C0 File Offset: 0x000276C0
	public float healthFraction
	{
		get
		{
			return this._health / this._maxHealth;
		}
	}

	// Token: 0x170002E7 RID: 743
	// (get) Token: 0x06000A53 RID: 2643 RVA: 0x000294D0 File Offset: 0x000276D0
	public float healthLoss
	{
		get
		{
			return this._maxHealth - this.health;
		}
	}

	// Token: 0x170002E8 RID: 744
	// (get) Token: 0x06000A54 RID: 2644 RVA: 0x000294E0 File Offset: 0x000276E0
	public float healthLossFraction
	{
		get
		{
			return 1f - this._health / this._maxHealth;
		}
	}

	// Token: 0x170002E9 RID: 745
	// (get) Token: 0x06000A55 RID: 2645 RVA: 0x000294F8 File Offset: 0x000276F8
	// (set) Token: 0x06000A56 RID: 2646 RVA: 0x00029500 File Offset: 0x00027700
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

	// Token: 0x170002EA RID: 746
	// (get) Token: 0x06000A57 RID: 2647 RVA: 0x0002950C File Offset: 0x0002770C
	public bool alive
	{
		get
		{
			return this.health > 0f;
		}
	}

	// Token: 0x170002EB RID: 747
	// (get) Token: 0x06000A58 RID: 2648 RVA: 0x0002951C File Offset: 0x0002771C
	public bool dead
	{
		get
		{
			return this.health <= 0f;
		}
	}

	// Token: 0x06000A59 RID: 2649 RVA: 0x00029530 File Offset: 0x00027730
	protected void Awake()
	{
		this._maxHealth = (this._health = TakeDamage.HealthAliveValueClamp(this._maxHealth));
	}

	// Token: 0x06000A5A RID: 2650 RVA: 0x00029558 File Offset: 0x00027758
	public RepairEvent Heal(IDBase healer, float amount)
	{
		RepairEvent result;
		this.Heal(healer, amount, out result);
		return result;
	}

	// Token: 0x06000A5B RID: 2651 RVA: 0x00029574 File Offset: 0x00027774
	public RepairStatus Heal(IDBase healer, float amount, out RepairEvent repair)
	{
		repair.doner = healer;
		repair.receiver = this;
		repair.givenAmount = amount;
		if (amount <= 0f)
		{
			repair.status = RepairStatus.Failed;
			repair.usedAmount = 0f;
			return RepairStatus.Failed;
		}
		if (this.dead)
		{
			repair.status = RepairStatus.FailedUnreparable;
			repair.usedAmount = 0f;
		}
		else if (this._health == this._maxHealth)
		{
			repair.status = RepairStatus.FailedFull;
			repair.usedAmount = 0f;
		}
		else if (this._health > this._maxHealth - amount)
		{
			this._health = this._maxHealth;
			repair.usedAmount = this._maxHealth - this._health;
			repair.status = RepairStatus.AppliedPartial;
		}
		else
		{
			this._health += amount;
			repair.usedAmount = repair.givenAmount;
			if (this._health == this._maxHealth)
			{
				repair.status = RepairStatus.AppliedFull;
			}
			else
			{
				repair.status = RepairStatus.Applied;
			}
		}
		if (this.sendMessageRepair)
		{
			base.SendMessage("OnRepair", repair, 1);
		}
		return repair.status;
	}

	// Token: 0x06000A5C RID: 2652 RVA: 0x000296A4 File Offset: 0x000278A4
	public static LifeStatus Hurt(IDBase attacker, IDBase victim, TakeDamage.Quantity damageQuantity, out DamageEvent damage, object extraData = null)
	{
		return TakeDamage.HurtShared(attacker, victim, damageQuantity, out damage, extraData);
	}

	// Token: 0x06000A5D RID: 2653 RVA: 0x000296B4 File Offset: 0x000278B4
	public static LifeStatus Hurt(IDBase attacker, IDBase victim, TakeDamage.Quantity damageQuantity, object extraData = null)
	{
		return TakeDamage.HurtShared(attacker, victim, damageQuantity, extraData);
	}

	// Token: 0x06000A5E RID: 2654 RVA: 0x000296C0 File Offset: 0x000278C0
	public static LifeStatus Kill(IDBase attacker, IDBase victim, out DamageEvent damage, object extraData = null)
	{
		return TakeDamage.HurtShared(attacker, victim, TakeDamage.Quantity.AllHealth, out damage, extraData);
	}

	// Token: 0x06000A5F RID: 2655 RVA: 0x000296D0 File Offset: 0x000278D0
	public static LifeStatus Kill(IDBase attacker, IDBase victim, object extraData = null)
	{
		return TakeDamage.HurtShared(attacker, victim, TakeDamage.Quantity.AllHealth, extraData);
	}

	// Token: 0x06000A60 RID: 2656 RVA: 0x000296E0 File Offset: 0x000278E0
	public static LifeStatus HurtSelf(IDBase victim, TakeDamage.Quantity damageQuantity, out DamageEvent damage, object extraData = null)
	{
		return TakeDamage.HurtShared(victim, victim, damageQuantity, out damage, extraData);
	}

	// Token: 0x06000A61 RID: 2657 RVA: 0x000296EC File Offset: 0x000278EC
	public static LifeStatus HurtSelf(IDBase victim, TakeDamage.Quantity damageQuantity, object extraData = null)
	{
		return TakeDamage.HurtShared(victim, victim, damageQuantity, extraData);
	}

	// Token: 0x06000A62 RID: 2658 RVA: 0x000296F8 File Offset: 0x000278F8
	public static LifeStatus KillSelf(IDBase victim, object extraData = null)
	{
		return TakeDamage.HurtShared(victim, victim, TakeDamage.Quantity.AllHealth, extraData);
	}

	// Token: 0x06000A63 RID: 2659 RVA: 0x00029708 File Offset: 0x00027908
	public static LifeStatus KillSelf(IDBase victim, out DamageEvent damage, object extraData = null)
	{
		return TakeDamage.HurtShared(victim, victim, TakeDamage.Quantity.AllHealth, out damage, extraData);
	}

	// Token: 0x06000A64 RID: 2660 RVA: 0x00029718 File Offset: 0x00027918
	private static LifeStatus HurtShared(IDBase attacker, IDBase victim, TakeDamage.Quantity damageQuantity, out DamageEvent damage, object extraData = null)
	{
		if (victim)
		{
			IDMain idMain = victim.idMain;
			if (idMain)
			{
				TakeDamage takeDamage;
				if (idMain is Character)
				{
					takeDamage = ((Character)idMain).takeDamage;
				}
				else
				{
					takeDamage = idMain.GetLocal<TakeDamage>();
				}
				if (takeDamage && !takeDamage.takenodamage)
				{
					takeDamage.MarkDamageTime();
					damage.victim.id = victim;
					damage.attacker.id = attacker;
					damage.amount = damageQuantity.value;
					damage.sender = takeDamage;
					damage.status = ((!takeDamage.dead) ? LifeStatus.IsAlive : LifeStatus.IsDead);
					damage.damageTypes = (DamageTypeFlags)0;
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
		damage.damageTypes = (DamageTypeFlags)0;
		damage.status = LifeStatus.Failed;
		damage.extraData = extraData;
		return LifeStatus.Failed;
	}

	// Token: 0x06000A65 RID: 2661 RVA: 0x00029840 File Offset: 0x00027A40
	private static LifeStatus HurtShared(IDBase attacker, IDBase victim, TakeDamage.Quantity damageQuantity, object extraData = null)
	{
		DamageEvent damageEvent;
		return TakeDamage.HurtShared(attacker, victim, damageQuantity, out damageEvent, extraData);
	}

	// Token: 0x06000A66 RID: 2662 RVA: 0x00029858 File Offset: 0x00027A58
	protected virtual void ApplyDamageTypeList(ref DamageEvent damage, DamageTypeList damageTypes)
	{
		for (int i = 0; i < 6; i++)
		{
			if (!Mathf.Approximately(damageTypes[i], 0f))
			{
				damage.damageTypes |= (DamageTypeFlags)(1 << i);
				damage.amount += damageTypes[i];
			}
		}
	}

	// Token: 0x06000A67 RID: 2663 RVA: 0x000298B4 File Offset: 0x00027AB4
	protected virtual LifeStatus Hurt(ref DamageEvent damage)
	{
		if (this.dead)
		{
			damage.status = LifeStatus.IsDead;
		}
		else if (this.health > damage.amount)
		{
			damage.status = LifeStatus.IsAlive;
		}
		else
		{
			damage.status = LifeStatus.WasKilled;
		}
		this.ProcessDamageEvent(ref damage);
		if (this.ShouldRelayDamageEvent(ref damage))
		{
			base.SendMessage("OnHurt", damage, 1);
		}
		if (damage.status == LifeStatus.WasKilled)
		{
			base.SendMessage("OnKilled", damage, 1);
		}
		return damage.status;
	}

	// Token: 0x06000A68 RID: 2664 RVA: 0x00029950 File Offset: 0x00027B50
	protected void ProcessDamageEvent(ref DamageEvent damage)
	{
		if (this.takenodamage)
		{
			return;
		}
		LifeStatus status = damage.status;
		if (status != LifeStatus.IsAlive)
		{
			if (status == LifeStatus.WasKilled)
			{
				this._health = 0f;
			}
		}
		else
		{
			this._health -= damage.amount;
		}
	}

	// Token: 0x06000A69 RID: 2665 RVA: 0x000299B0 File Offset: 0x00027BB0
	protected bool ShouldRelayDamageEvent(ref DamageEvent damage)
	{
		switch (damage.status)
		{
		case LifeStatus.IsAlive:
			return this.sendMessageWhenAlive;
		case LifeStatus.WasKilled:
			return this.sendMessageWhenKilled;
		case LifeStatus.IsDead:
			return this.sendMessageWhenDead;
		default:
			Debug.LogWarning("Unhandled LifeStatus " + damage.status, this);
			return false;
		}
	}

	// Token: 0x06000A6A RID: 2666 RVA: 0x00029A0C File Offset: 0x00027C0C
	public override string ToString()
	{
		return string.Format("[{0}: health={1}]", base.ToString(), this.health);
	}

	// Token: 0x06000A6B RID: 2667 RVA: 0x00029A2C File Offset: 0x00027C2C
	public virtual void ServerFrame()
	{
	}

	// Token: 0x06000A6C RID: 2668 RVA: 0x00029A30 File Offset: 0x00027C30
	public virtual void SaveVitals(ref Vitals.Builder vitals)
	{
		vitals.SetHealth(this.health);
	}

	// Token: 0x06000A6D RID: 2669 RVA: 0x00029A40 File Offset: 0x00027C40
	public virtual void LoadVitals(Vitals vitals)
	{
		this.health = vitals.Health;
		if (this.health <= 0f)
		{
			Debug.Log("LOAD VITALS - HEALTH WAS " + this.health);
			this.health = 1f;
		}
	}

	// Token: 0x040006B9 RID: 1721
	public const string DamageMessage = "OnHurt";

	// Token: 0x040006BA RID: 1722
	public const string KillMessage = "OnKilled";

	// Token: 0x040006BB RID: 1723
	public const string RepairMessage = "OnRepair";

	// Token: 0x040006BC RID: 1724
	public const SendMessageOptions DamageMessageOptions = 1;

	// Token: 0x040006BD RID: 1725
	public const SendMessageOptions RepairMessageOptions = 1;

	// Token: 0x040006BE RID: 1726
	public const float kMinimumSetHealthValueWhenAlive = 0.001f;

	// Token: 0x040006BF RID: 1727
	protected float _lastDamageTime;

	// Token: 0x040006C0 RID: 1728
	private bool takenodamage;

	// Token: 0x040006C1 RID: 1729
	[SerializeField]
	private float _maxHealth = 100f;

	// Token: 0x040006C2 RID: 1730
	private float _health;

	// Token: 0x040006C3 RID: 1731
	public bool playsHitNotification;

	// Token: 0x040006C4 RID: 1732
	public bool sendMessageWhenAlive = true;

	// Token: 0x040006C5 RID: 1733
	public bool sendMessageWhenKilled = true;

	// Token: 0x040006C6 RID: 1734
	public bool sendMessageWhenDead = true;

	// Token: 0x040006C7 RID: 1735
	public bool sendMessageRepair = true;

	// Token: 0x02000157 RID: 343
	public enum Unit : sbyte
	{
		// Token: 0x040006C9 RID: 1737
		Unspecified,
		// Token: 0x040006CA RID: 1738
		HealthPoints,
		// Token: 0x040006CB RID: 1739
		AllHealth,
		// Token: 0x040006CC RID: 1740
		List = -1
	}

	// Token: 0x02000158 RID: 344
	public struct Quantity
	{
		// Token: 0x06000A6E RID: 2670 RVA: 0x00029A90 File Offset: 0x00027C90
		private Quantity(TakeDamage.Unit Measurement, DamageTypeList DamageTypeList, float Value)
		{
			this.Unit = Measurement;
			this.list = DamageTypeList;
			this.value = Value;
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000A6F RID: 2671 RVA: 0x00029AA8 File Offset: 0x00027CA8
		public static TakeDamage.Quantity AllHealth
		{
			get
			{
				return new TakeDamage.Quantity(TakeDamage.Unit.AllHealth, null, float.PositiveInfinity);
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000A70 RID: 2672 RVA: 0x00029AB8 File Offset: 0x00027CB8
		public DamageTypeList DamageTypeList
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

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000A71 RID: 2673 RVA: 0x00029AD8 File Offset: 0x00027CD8
		public bool IsDamageTypeList
		{
			get
			{
				return (int)this.Unit == -1;
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000A72 RID: 2674 RVA: 0x00029AE4 File Offset: 0x00027CE4
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

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x00029B04 File Offset: 0x00027D04
		public bool IsHealthPoints
		{
			get
			{
				return (int)this.Unit > 0;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x00029B10 File Offset: 0x00027D10
		public bool IsAllHealthPoints
		{
			get
			{
				return (int)this.Unit == 2;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000A75 RID: 2677 RVA: 0x00029B1C File Offset: 0x00027D1C
		public bool Specified
		{
			get
			{
				return (int)this.Unit != 0;
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000A76 RID: 2678 RVA: 0x00029B2C File Offset: 0x00027D2C
		public object BoxedValue
		{
			get
			{
				return ((int)this.Unit > 0) ? this.value : this.list;
			}
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x00029B54 File Offset: 0x00027D54
		public override string ToString()
		{
			return string.Format("[{0}:{1}]", this.Unit, this.BoxedValue);
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x00029B74 File Offset: 0x00027D74
		public static implicit operator TakeDamage.Quantity(int HealthPoints)
		{
			return new TakeDamage.Quantity((HealthPoints != 0) ? TakeDamage.Unit.HealthPoints : TakeDamage.Unit.Unspecified, null, (float)((HealthPoints >= 0) ? HealthPoints : (-(float)HealthPoints)));
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x00029B9C File Offset: 0x00027D9C
		public static implicit operator TakeDamage.Quantity(float HealthPoints)
		{
			return new TakeDamage.Quantity((HealthPoints != 0f) ? ((!float.IsInfinity(HealthPoints)) ? TakeDamage.Unit.HealthPoints : TakeDamage.Unit.AllHealth) : TakeDamage.Unit.Unspecified, null, HealthPoints);
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x00029BD4 File Offset: 0x00027DD4
		public static implicit operator TakeDamage.Quantity(DamageTypeList DamageTypeList)
		{
			return new TakeDamage.Quantity((!object.ReferenceEquals(DamageTypeList, null)) ? TakeDamage.Unit.List : TakeDamage.Unit.Unspecified, DamageTypeList, 0f);
		}

		// Token: 0x040006CD RID: 1741
		public readonly TakeDamage.Unit Unit;

		// Token: 0x040006CE RID: 1742
		internal readonly float value;

		// Token: 0x040006CF RID: 1743
		internal readonly DamageTypeList list;
	}
}
