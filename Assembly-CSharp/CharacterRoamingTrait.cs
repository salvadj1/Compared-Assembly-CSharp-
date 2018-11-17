using System;
using UnityEngine;

// Token: 0x0200012F RID: 303
public class CharacterRoamingTrait : global::CharacterTrait
{
	// Token: 0x170001BB RID: 443
	// (get) Token: 0x060007D0 RID: 2000 RVA: 0x00022400 File Offset: 0x00020600
	public float maxRoamDistance
	{
		get
		{
			return (!this._allowed) ? 0f : this._maxRoamDistance;
		}
	}

	// Token: 0x170001BC RID: 444
	// (get) Token: 0x060007D1 RID: 2001 RVA: 0x00022420 File Offset: 0x00020620
	public float minRoamDistance
	{
		get
		{
			return (!this._allowed) ? 0f : this._minRoamDistance;
		}
	}

	// Token: 0x170001BD RID: 445
	// (get) Token: 0x060007D2 RID: 2002 RVA: 0x00022440 File Offset: 0x00020640
	public float randomRoamDistance
	{
		get
		{
			return (!this._allowed) ? 0f : ((this._minRoamDistance != this._maxRoamDistance) ? (this._minRoamDistance + (this._maxRoamDistance - this._minRoamDistance) * Random.value) : this._minRoamDistance);
		}
	}

	// Token: 0x170001BE RID: 446
	// (get) Token: 0x060007D3 RID: 2003 RVA: 0x00022498 File Offset: 0x00020698
	public float maxFleeDistance
	{
		get
		{
			return (!this._allowed) ? 0f : this._maxFleeDistance;
		}
	}

	// Token: 0x170001BF RID: 447
	// (get) Token: 0x060007D4 RID: 2004 RVA: 0x000224B8 File Offset: 0x000206B8
	public float minFleeDistance
	{
		get
		{
			return (!this._allowed) ? 0f : this._minFleeDistance;
		}
	}

	// Token: 0x170001C0 RID: 448
	// (get) Token: 0x060007D5 RID: 2005 RVA: 0x000224D8 File Offset: 0x000206D8
	public float randomFleeDistance
	{
		get
		{
			return (!this._allowed) ? 0f : ((this._minFleeDistance != this._maxFleeDistance) ? (this._minFleeDistance + (this._maxFleeDistance - this._minFleeDistance) * Random.value) : this._minFleeDistance);
		}
	}

	// Token: 0x170001C1 RID: 449
	// (get) Token: 0x060007D6 RID: 2006 RVA: 0x00022530 File Offset: 0x00020730
	public float minRoamAngle
	{
		get
		{
			return (!this._allowed) ? 0f : this._minRoamAngle;
		}
	}

	// Token: 0x170001C2 RID: 450
	// (get) Token: 0x060007D7 RID: 2007 RVA: 0x00022550 File Offset: 0x00020750
	public float maxRoamAngle
	{
		get
		{
			return (!this._allowed) ? 0f : this._maxRoamAngle;
		}
	}

	// Token: 0x170001C3 RID: 451
	// (get) Token: 0x060007D8 RID: 2008 RVA: 0x00022570 File Offset: 0x00020770
	public float randomRoamAngle
	{
		get
		{
			return (!this._allowed) ? 0f : ((this._maxRoamAngle != this._minRoamAngle) ? (this._minRoamAngle + (this._maxRoamAngle - this._minRoamAngle) * Random.value) : this._minRoamAngle);
		}
	}

	// Token: 0x170001C4 RID: 452
	// (get) Token: 0x060007D9 RID: 2009 RVA: 0x000225C8 File Offset: 0x000207C8
	public int minIdleMilliseconds
	{
		get
		{
			return this._minIdleMilliseconds;
		}
	}

	// Token: 0x170001C5 RID: 453
	// (get) Token: 0x060007DA RID: 2010 RVA: 0x000225D0 File Offset: 0x000207D0
	public int maxIdleMilliseconds
	{
		get
		{
			return this._maxIdleMilliseconds;
		}
	}

	// Token: 0x170001C6 RID: 454
	// (get) Token: 0x060007DB RID: 2011 RVA: 0x000225D8 File Offset: 0x000207D8
	public int randomIdleMilliseconds
	{
		get
		{
			return (this._minIdleMilliseconds != this._maxIdleMilliseconds) ? ((this._minIdleMilliseconds >= this._maxIdleMilliseconds) ? Random.Range(this._maxIdleMilliseconds, this._minIdleMilliseconds + 1) : Random.Range(this._minIdleMilliseconds, this._maxIdleMilliseconds + 1)) : this._minIdleMilliseconds;
		}
	}

	// Token: 0x170001C7 RID: 455
	// (get) Token: 0x060007DC RID: 2012 RVA: 0x00022640 File Offset: 0x00020840
	public int retryFromFailureMilliseconds
	{
		get
		{
			return this._retryFromFailureMilliseconds;
		}
	}

	// Token: 0x170001C8 RID: 456
	// (get) Token: 0x060007DD RID: 2013 RVA: 0x00022648 File Offset: 0x00020848
	public float minIdleSeconds
	{
		get
		{
			return (float)((double)this._minIdleMilliseconds / 1000.0);
		}
	}

	// Token: 0x170001C9 RID: 457
	// (get) Token: 0x060007DE RID: 2014 RVA: 0x0002265C File Offset: 0x0002085C
	public float maxIdleSeconds
	{
		get
		{
			return (float)((double)this._maxIdleMilliseconds / 1000.0);
		}
	}

	// Token: 0x170001CA RID: 458
	// (get) Token: 0x060007DF RID: 2015 RVA: 0x00022670 File Offset: 0x00020870
	public float randomIdleSeconds
	{
		get
		{
			return (float)((double)this.randomIdleMilliseconds / 1000.0);
		}
	}

	// Token: 0x170001CB RID: 459
	// (get) Token: 0x060007E0 RID: 2016 RVA: 0x00022684 File Offset: 0x00020884
	public float retryFromFailureSeconds
	{
		get
		{
			return (float)((double)this._retryFromFailureMilliseconds / 1000.0);
		}
	}

	// Token: 0x170001CC RID: 460
	// (get) Token: 0x060007E1 RID: 2017 RVA: 0x00022698 File Offset: 0x00020898
	public float roamRadius
	{
		get
		{
			return this._roamRadius;
		}
	}

	// Token: 0x170001CD RID: 461
	// (get) Token: 0x060007E2 RID: 2018 RVA: 0x000226A0 File Offset: 0x000208A0
	public bool allowed
	{
		get
		{
			return this._allowed;
		}
	}

	// Token: 0x170001CE RID: 462
	// (get) Token: 0x060007E3 RID: 2019 RVA: 0x000226A8 File Offset: 0x000208A8
	public Vector3 randomRoamVector
	{
		get
		{
			Vector3 result;
			result.y = 0f;
			if (this._allowed)
			{
				float randomRoamDistance = this.randomRoamDistance;
				float num = this.randomRoamAngle * 0.0174532924f;
				result.x = Mathf.Sin(num) * randomRoamDistance;
				result.z = Mathf.Cos(num) * randomRoamDistance;
			}
			else
			{
				result.x = 0f;
				result.z = 0f;
			}
			return result;
		}
	}

	// Token: 0x170001CF RID: 463
	// (get) Token: 0x060007E4 RID: 2020 RVA: 0x0002271C File Offset: 0x0002091C
	public Vector3 randomFleeVector
	{
		get
		{
			Vector3 result;
			result.y = 0f;
			if (this._allowed)
			{
				float randomFleeDistance = this.randomFleeDistance;
				float num = this.randomRoamAngle * 0.0174532924f;
				result.x = Mathf.Sin(num) * randomFleeDistance;
				result.z = Mathf.Cos(num) * randomFleeDistance;
			}
			else
			{
				result.x = 0f;
				result.z = 0f;
			}
			return result;
		}
	}

	// Token: 0x170001D0 RID: 464
	// (get) Token: 0x060007E5 RID: 2021 RVA: 0x00022790 File Offset: 0x00020990
	public Vector3 randomRoamNormal
	{
		get
		{
			Vector3 result;
			result.y = 0f;
			if (this._allowed)
			{
				float num = this.randomRoamAngle * 0.0174532924f;
				result.x = Mathf.Sin(num);
				result.z = Mathf.Cos(num);
			}
			else
			{
				result.x = 0f;
				result.z = 0f;
			}
			return result;
		}
	}

	// Token: 0x170001D1 RID: 465
	// (get) Token: 0x060007E6 RID: 2022 RVA: 0x000227FC File Offset: 0x000209FC
	public float fleeSpeed
	{
		get
		{
			return this._fleeSpeed;
		}
	}

	// Token: 0x170001D2 RID: 466
	// (get) Token: 0x060007E7 RID: 2023 RVA: 0x00022804 File Offset: 0x00020A04
	public float runSpeed
	{
		get
		{
			return this._runSpeed;
		}
	}

	// Token: 0x170001D3 RID: 467
	// (get) Token: 0x060007E8 RID: 2024 RVA: 0x0002280C File Offset: 0x00020A0C
	public float walkSpeed
	{
		get
		{
			return this._walkSpeed;
		}
	}

	// Token: 0x040005FA RID: 1530
	[SerializeField]
	private float _maxRoamDistance = 20f;

	// Token: 0x040005FB RID: 1531
	[SerializeField]
	private float _minRoamDistance = 10f;

	// Token: 0x040005FC RID: 1532
	[SerializeField]
	private float _minRoamAngle = -180f;

	// Token: 0x040005FD RID: 1533
	[SerializeField]
	private float _maxRoamAngle = 180f;

	// Token: 0x040005FE RID: 1534
	[SerializeField]
	private float _maxFleeDistance = 40f;

	// Token: 0x040005FF RID: 1535
	[SerializeField]
	private float _minFleeDistance = 21f;

	// Token: 0x04000600 RID: 1536
	[SerializeField]
	private float _roamRadius = 80f;

	// Token: 0x04000601 RID: 1537
	[SerializeField]
	private bool _allowed = true;

	// Token: 0x04000602 RID: 1538
	[SerializeField]
	private int _minIdleMilliseconds = 2000;

	// Token: 0x04000603 RID: 1539
	[SerializeField]
	private int _maxIdleMilliseconds = 8000;

	// Token: 0x04000604 RID: 1540
	[SerializeField]
	private int _retryFromFailureMilliseconds = 800;

	// Token: 0x04000605 RID: 1541
	[SerializeField]
	private float _fleeSpeed = 9f;

	// Token: 0x04000606 RID: 1542
	[SerializeField]
	private float _runSpeed = 6f;

	// Token: 0x04000607 RID: 1543
	[SerializeField]
	private float _walkSpeed = 1.8f;
}
