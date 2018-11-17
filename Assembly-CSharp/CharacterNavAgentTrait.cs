using System;
using UnityEngine;

// Token: 0x0200010C RID: 268
public class CharacterNavAgentTrait : CharacterTrait
{
	// Token: 0x1700017A RID: 378
	// (get) Token: 0x060006E2 RID: 1762 RVA: 0x0001F510 File Offset: 0x0001D710
	public float radius
	{
		get
		{
			return this._radius;
		}
	}

	// Token: 0x1700017B RID: 379
	// (get) Token: 0x060006E3 RID: 1763 RVA: 0x0001F518 File Offset: 0x0001D718
	public float speed
	{
		get
		{
			return this._speed;
		}
	}

	// Token: 0x1700017C RID: 380
	// (get) Token: 0x060006E4 RID: 1764 RVA: 0x0001F520 File Offset: 0x0001D720
	public float acceleration
	{
		get
		{
			return this._acceleration;
		}
	}

	// Token: 0x1700017D RID: 381
	// (get) Token: 0x060006E5 RID: 1765 RVA: 0x0001F528 File Offset: 0x0001D728
	public float angularSpeed
	{
		get
		{
			return this._angularSpeed;
		}
	}

	// Token: 0x1700017E RID: 382
	// (get) Token: 0x060006E6 RID: 1766 RVA: 0x0001F530 File Offset: 0x0001D730
	public float stoppingDistance
	{
		get
		{
			return this._stoppingDistance;
		}
	}

	// Token: 0x1700017F RID: 383
	// (get) Token: 0x060006E7 RID: 1767 RVA: 0x0001F538 File Offset: 0x0001D738
	public bool autoTraverseOffMeshLink
	{
		get
		{
			return this._autoTraverseOffMeshLink;
		}
	}

	// Token: 0x17000180 RID: 384
	// (get) Token: 0x060006E8 RID: 1768 RVA: 0x0001F540 File Offset: 0x0001D740
	public bool autoBraking
	{
		get
		{
			return this._autoBraking;
		}
	}

	// Token: 0x17000181 RID: 385
	// (get) Token: 0x060006E9 RID: 1769 RVA: 0x0001F548 File Offset: 0x0001D748
	public bool autoRepath
	{
		get
		{
			return this._autoRepath;
		}
	}

	// Token: 0x17000182 RID: 386
	// (get) Token: 0x060006EA RID: 1770 RVA: 0x0001F550 File Offset: 0x0001D750
	public float height
	{
		get
		{
			return this._height;
		}
	}

	// Token: 0x17000183 RID: 387
	// (get) Token: 0x060006EB RID: 1771 RVA: 0x0001F558 File Offset: 0x0001D758
	public float baseOffset
	{
		get
		{
			return this._baseOffset;
		}
	}

	// Token: 0x17000184 RID: 388
	// (get) Token: 0x060006EC RID: 1772 RVA: 0x0001F560 File Offset: 0x0001D760
	public ObstacleAvoidanceType obstacleAvoidanceType
	{
		get
		{
			return this._obstacleAvoidanceType;
		}
	}

	// Token: 0x17000185 RID: 389
	// (get) Token: 0x060006ED RID: 1773 RVA: 0x0001F568 File Offset: 0x0001D768
	public int avoidancePriority
	{
		get
		{
			return this._avoidancePriority;
		}
	}

	// Token: 0x17000186 RID: 390
	// (get) Token: 0x060006EE RID: 1774 RVA: 0x0001F570 File Offset: 0x0001D770
	public int walkableMaks
	{
		get
		{
			return this._walkableMask;
		}
	}

	// Token: 0x060006EF RID: 1775 RVA: 0x0001F578 File Offset: 0x0001D778
	public void CopyTo(NavMeshAgent agent)
	{
		agent.radius = this._radius;
		agent.speed = this._speed;
		agent.acceleration = this._acceleration;
		agent.angularSpeed = this._angularSpeed;
		agent.stoppingDistance = this._stoppingDistance;
		agent.autoTraverseOffMeshLink = this._autoTraverseOffMeshLink;
		agent.autoBraking = this._autoBraking;
		agent.autoRepath = this._autoRepath;
		agent.height = this._height;
		agent.baseOffset = this._baseOffset;
		agent.obstacleAvoidanceType = this._obstacleAvoidanceType;
		agent.avoidancePriority = this._avoidancePriority;
		agent.walkableMask = this._walkableMask;
	}

	// Token: 0x0400051C RID: 1308
	[SerializeField]
	private float _radius = 0.5f;

	// Token: 0x0400051D RID: 1309
	[SerializeField]
	private float _speed = 3f;

	// Token: 0x0400051E RID: 1310
	[SerializeField]
	private float _acceleration = 8f;

	// Token: 0x0400051F RID: 1311
	[SerializeField]
	private float _angularSpeed = 120f;

	// Token: 0x04000520 RID: 1312
	[SerializeField]
	private float _stoppingDistance = 2f;

	// Token: 0x04000521 RID: 1313
	[SerializeField]
	private bool _autoTraverseOffMeshLink = true;

	// Token: 0x04000522 RID: 1314
	[SerializeField]
	private bool _autoBraking = true;

	// Token: 0x04000523 RID: 1315
	[SerializeField]
	private bool _autoRepath = true;

	// Token: 0x04000524 RID: 1316
	[SerializeField]
	private float _height = 2f;

	// Token: 0x04000525 RID: 1317
	[SerializeField]
	private float _baseOffset;

	// Token: 0x04000526 RID: 1318
	[SerializeField]
	private ObstacleAvoidanceType _obstacleAvoidanceType = 1;

	// Token: 0x04000527 RID: 1319
	[SerializeField]
	private int _avoidancePriority = 50;

	// Token: 0x04000528 RID: 1320
	[SerializeField]
	private int _walkableMask = -1;
}
