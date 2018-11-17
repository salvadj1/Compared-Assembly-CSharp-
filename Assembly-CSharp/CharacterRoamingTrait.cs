using System;
using UnityEngine;

// Token: 0x02000110 RID: 272
public class CharacterRoamingTrait : CharacterTrait
{
	// Token: 0x1700018D RID: 397
	// (get) Token: 0x060006FE RID: 1790 RVA: 0x0001F82C File Offset: 0x0001DA2C
	public float maxRoamDistance
	{
		get
		{
			return (!this._allowed) ? 0f : this._maxRoamDistance;
		}
	}

	// Token: 0x1700018E RID: 398
	// (get) Token: 0x060006FF RID: 1791 RVA: 0x0001F84C File Offset: 0x0001DA4C
	public float minRoamDistance
	{
		get
		{
			return (!this._allowed) ? 0f : this._minRoamDistance;
		}
	}

	// Token: 0x1700018F RID: 399
	// (get) Token: 0x06000700 RID: 1792 RVA: 0x0001F86C File Offset: 0x0001DA6C
	public float randomRoamDistance
	{
		get
		{
			return (!this._allowed) ? 0f : ((this._minRoamDistance != this._maxRoamDistance) ? (this._minRoamDistance + (this._maxRoamDistance - this._minRoamDistance) * Random.value) : this._minRoamDistance);
		}
	}

	// Token: 0x17000190 RID: 400
	// (get) Token: 0x06000701 RID: 1793 RVA: 0x0001F8C4 File Offset: 0x0001DAC4
	public float maxFleeDistance
	{
		get
		{
			return (!this._allowed) ? 0f : this._maxFleeDistance;
		}
	}

	// Token: 0x17000191 RID: 401
	// (get) Token: 0x06000702 RID: 1794 RVA: 0x0001F8E4 File Offset: 0x0001DAE4
	public float minFleeDistance
	{
		get
		{
			return (!this._allowed) ? 0f : this._minFleeDistance;
		}
	}

	// Token: 0x17000192 RID: 402
	// (get) Token: 0x06000703 RID: 1795 RVA: 0x0001F904 File Offset: 0x0001DB04
	public float randomFleeDistance
	{
		get
		{
			return (!this._allowed) ? 0f : ((this._minFleeDistance != this._maxFleeDistance) ? (this._minFleeDistance + (this._maxFleeDistance - this._minFleeDistance) * Random.value) : this._minFleeDistance);
		}
	}

	// Token: 0x17000193 RID: 403
	// (get) Token: 0x06000704 RID: 1796 RVA: 0x0001F95C File Offset: 0x0001DB5C
	public float minRoamAngle
	{
		get
		{
			return (!this._allowed) ? 0f : this._minRoamAngle;
		}
	}

	// Token: 0x17000194 RID: 404
	// (get) Token: 0x06000705 RID: 1797 RVA: 0x0001F97C File Offset: 0x0001DB7C
	public float maxRoamAngle
	{
		get
		{
			return (!this._allowed) ? 0f : this._maxRoamAngle;
		}
	}

	// Token: 0x17000195 RID: 405
	// (get) Token: 0x06000706 RID: 1798 RVA: 0x0001F99C File Offset: 0x0001DB9C
	public float randomRoamAngle
	{
		get
		{
			return (!this._allowed) ? 0f : ((this._maxRoamAngle != this._minRoamAngle) ? (this._minRoamAngle + (this._maxRoamAngle - this._minRoamAngle) * Random.value) : this._minRoamAngle);
		}
	}

	// Token: 0x17000196 RID: 406
	// (get) Token: 0x06000707 RID: 1799 RVA: 0x0001F9F4 File Offset: 0x0001DBF4
	public int minIdleMilliseconds
	{
		get
		{
			return this._minIdleMilliseconds;
		}
	}

	// Token: 0x17000197 RID: 407
	// (get) Token: 0x06000708 RID: 1800 RVA: 0x0001F9FC File Offset: 0x0001DBFC
	public int maxIdleMilliseconds
	{
		get
		{
			return this._maxIdleMilliseconds;
		}
	}

	// Token: 0x17000198 RID: 408
	// (get) Token: 0x06000709 RID: 1801 RVA: 0x0001FA04 File Offset: 0x0001DC04
	public int randomIdleMilliseconds
	{
		get
		{
			return (this._minIdleMilliseconds != this._maxIdleMilliseconds) ? ((this._minIdleMilliseconds >= this._maxIdleMilliseconds) ? Random.Range(this._maxIdleMilliseconds, this._minIdleMilliseconds + 1) : Random.Range(this._minIdleMilliseconds, this._maxIdleMilliseconds + 1)) : this._minIdleMilliseconds;
		}
	}

	// Token: 0x17000199 RID: 409
	// (get) Token: 0x0600070A RID: 1802 RVA: 0x0001FA6C File Offset: 0x0001DC6C
	public int retryFromFailureMilliseconds
	{
		get
		{
			return this._retryFromFailureMilliseconds;
		}
	}

	// Token: 0x1700019A RID: 410
	// (get) Token: 0x0600070B RID: 1803 RVA: 0x0001FA74 File Offset: 0x0001DC74
	public float minIdleSeconds
	{
		get
		{
			return (float)((double)this._minIdleMilliseconds / 1000.0);
		}
	}

	// Token: 0x1700019B RID: 411
	// (get) Token: 0x0600070C RID: 1804 RVA: 0x0001FA88 File Offset: 0x0001DC88
	public float maxIdleSeconds
	{
		get
		{
			return (float)((double)this._maxIdleMilliseconds / 1000.0);
		}
	}

	// Token: 0x1700019C RID: 412
	// (get) Token: 0x0600070D RID: 1805 RVA: 0x0001FA9C File Offset: 0x0001DC9C
	public float randomIdleSeconds
	{
		get
		{
			return (float)((double)this.randomIdleMilliseconds / 1000.0);
		}
	}

	// Token: 0x1700019D RID: 413
	// (get) Token: 0x0600070E RID: 1806 RVA: 0x0001FAB0 File Offset: 0x0001DCB0
	public float retryFromFailureSeconds
	{
		get
		{
			return (float)((double)this._retryFromFailureMilliseconds / 1000.0);
		}
	}

	// Token: 0x1700019E RID: 414
	// (get) Token: 0x0600070F RID: 1807 RVA: 0x0001FAC4 File Offset: 0x0001DCC4
	public float roamRadius
	{
		get
		{
			return this._roamRadius;
		}
	}

	// Token: 0x1700019F RID: 415
	// (get) Token: 0x06000710 RID: 1808 RVA: 0x0001FACC File Offset: 0x0001DCCC
	public bool allowed
	{
		get
		{
			return this._allowed;
		}
	}

	// Token: 0x170001A0 RID: 416
	// (get) Token: 0x06000711 RID: 1809 RVA: 0x0001FAD4 File Offset: 0x0001DCD4
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

	// Token: 0x170001A1 RID: 417
	// (get) Token: 0x06000712 RID: 1810 RVA: 0x0001FB48 File Offset: 0x0001DD48
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

	// Token: 0x170001A2 RID: 418
	// (get) Token: 0x06000713 RID: 1811 RVA: 0x0001FBBC File Offset: 0x0001DDBC
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

	// Token: 0x170001A3 RID: 419
	// (get) Token: 0x06000714 RID: 1812 RVA: 0x0001FC28 File Offset: 0x0001DE28
	public float fleeSpeed
	{
		get
		{
			return this._fleeSpeed;
		}
	}

	// Token: 0x170001A4 RID: 420
	// (get) Token: 0x06000715 RID: 1813 RVA: 0x0001FC30 File Offset: 0x0001DE30
	public float runSpeed
	{
		get
		{
			return this._runSpeed;
		}
	}

	// Token: 0x170001A5 RID: 421
	// (get) Token: 0x06000716 RID: 1814 RVA: 0x0001FC38 File Offset: 0x0001DE38
	public float walkSpeed
	{
		get
		{
			return this._walkSpeed;
		}
	}

	// Token: 0x0400052F RID: 1327
	[SerializeField]
	private float _maxRoamDistance = 20f;

	// Token: 0x04000530 RID: 1328
	[SerializeField]
	private float _minRoamDistance = 10f;

	// Token: 0x04000531 RID: 1329
	[SerializeField]
	private float _minRoamAngle = -180f;

	// Token: 0x04000532 RID: 1330
	[SerializeField]
	private float _maxRoamAngle = 180f;

	// Token: 0x04000533 RID: 1331
	[SerializeField]
	private float _maxFleeDistance = 40f;

	// Token: 0x04000534 RID: 1332
	[SerializeField]
	private float _minFleeDistance = 21f;

	// Token: 0x04000535 RID: 1333
	[SerializeField]
	private float _roamRadius = 80f;

	// Token: 0x04000536 RID: 1334
	[SerializeField]
	private bool _allowed = true;

	// Token: 0x04000537 RID: 1335
	[SerializeField]
	private int _minIdleMilliseconds = 2000;

	// Token: 0x04000538 RID: 1336
	[SerializeField]
	private int _maxIdleMilliseconds = 8000;

	// Token: 0x04000539 RID: 1337
	[SerializeField]
	private int _retryFromFailureMilliseconds = 800;

	// Token: 0x0400053A RID: 1338
	[SerializeField]
	private float _fleeSpeed = 9f;

	// Token: 0x0400053B RID: 1339
	[SerializeField]
	private float _runSpeed = 6f;

	// Token: 0x0400053C RID: 1340
	[SerializeField]
	private float _walkSpeed = 1.8f;
}
