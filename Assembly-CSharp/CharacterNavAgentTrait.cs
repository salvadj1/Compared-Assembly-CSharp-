using System;
using UnityEngine;

// Token: 0x0200012B RID: 299
public class CharacterNavAgentTrait : global::CharacterTrait
{
	// Token: 0x170001A8 RID: 424
	// (get) Token: 0x060007B4 RID: 1972 RVA: 0x000220E4 File Offset: 0x000202E4
	public float radius
	{
		get
		{
			return this._radius;
		}
	}

	// Token: 0x170001A9 RID: 425
	// (get) Token: 0x060007B5 RID: 1973 RVA: 0x000220EC File Offset: 0x000202EC
	public float speed
	{
		get
		{
			return this._speed;
		}
	}

	// Token: 0x170001AA RID: 426
	// (get) Token: 0x060007B6 RID: 1974 RVA: 0x000220F4 File Offset: 0x000202F4
	public float acceleration
	{
		get
		{
			return this._acceleration;
		}
	}

	// Token: 0x170001AB RID: 427
	// (get) Token: 0x060007B7 RID: 1975 RVA: 0x000220FC File Offset: 0x000202FC
	public float angularSpeed
	{
		get
		{
			return this._angularSpeed;
		}
	}

	// Token: 0x170001AC RID: 428
	// (get) Token: 0x060007B8 RID: 1976 RVA: 0x00022104 File Offset: 0x00020304
	public float stoppingDistance
	{
		get
		{
			return this._stoppingDistance;
		}
	}

	// Token: 0x170001AD RID: 429
	// (get) Token: 0x060007B9 RID: 1977 RVA: 0x0002210C File Offset: 0x0002030C
	public bool autoTraverseOffMeshLink
	{
		get
		{
			return this._autoTraverseOffMeshLink;
		}
	}

	// Token: 0x170001AE RID: 430
	// (get) Token: 0x060007BA RID: 1978 RVA: 0x00022114 File Offset: 0x00020314
	public bool autoBraking
	{
		get
		{
			return this._autoBraking;
		}
	}

	// Token: 0x170001AF RID: 431
	// (get) Token: 0x060007BB RID: 1979 RVA: 0x0002211C File Offset: 0x0002031C
	public bool autoRepath
	{
		get
		{
			return this._autoRepath;
		}
	}

	// Token: 0x170001B0 RID: 432
	// (get) Token: 0x060007BC RID: 1980 RVA: 0x00022124 File Offset: 0x00020324
	public float height
	{
		get
		{
			return this._height;
		}
	}

	// Token: 0x170001B1 RID: 433
	// (get) Token: 0x060007BD RID: 1981 RVA: 0x0002212C File Offset: 0x0002032C
	public float baseOffset
	{
		get
		{
			return this._baseOffset;
		}
	}

	// Token: 0x170001B2 RID: 434
	// (get) Token: 0x060007BE RID: 1982 RVA: 0x00022134 File Offset: 0x00020334
	public ObstacleAvoidanceType obstacleAvoidanceType
	{
		get
		{
			return this._obstacleAvoidanceType;
		}
	}

	// Token: 0x170001B3 RID: 435
	// (get) Token: 0x060007BF RID: 1983 RVA: 0x0002213C File Offset: 0x0002033C
	public int avoidancePriority
	{
		get
		{
			return this._avoidancePriority;
		}
	}

	// Token: 0x170001B4 RID: 436
	// (get) Token: 0x060007C0 RID: 1984 RVA: 0x00022144 File Offset: 0x00020344
	public int walkableMaks
	{
		get
		{
			return this._walkableMask;
		}
	}

	// Token: 0x060007C1 RID: 1985 RVA: 0x0002214C File Offset: 0x0002034C
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

	// Token: 0x040005E7 RID: 1511
	[SerializeField]
	private float _radius = 0.5f;

	// Token: 0x040005E8 RID: 1512
	[SerializeField]
	private float _speed = 3f;

	// Token: 0x040005E9 RID: 1513
	[SerializeField]
	private float _acceleration = 8f;

	// Token: 0x040005EA RID: 1514
	[SerializeField]
	private float _angularSpeed = 120f;

	// Token: 0x040005EB RID: 1515
	[SerializeField]
	private float _stoppingDistance = 2f;

	// Token: 0x040005EC RID: 1516
	[SerializeField]
	private bool _autoTraverseOffMeshLink = true;

	// Token: 0x040005ED RID: 1517
	[SerializeField]
	private bool _autoBraking = true;

	// Token: 0x040005EE RID: 1518
	[SerializeField]
	private bool _autoRepath = true;

	// Token: 0x040005EF RID: 1519
	[SerializeField]
	private float _height = 2f;

	// Token: 0x040005F0 RID: 1520
	[SerializeField]
	private float _baseOffset;

	// Token: 0x040005F1 RID: 1521
	[SerializeField]
	private ObstacleAvoidanceType _obstacleAvoidanceType = 1;

	// Token: 0x040005F2 RID: 1522
	[SerializeField]
	private int _avoidancePriority = 50;

	// Token: 0x040005F3 RID: 1523
	[SerializeField]
	private int _walkableMask = -1;
}
