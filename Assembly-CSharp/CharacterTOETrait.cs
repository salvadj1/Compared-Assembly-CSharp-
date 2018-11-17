using System;
using UnityEngine;

// Token: 0x02000116 RID: 278
public class CharacterTOETrait : CharacterTrait
{
	// Token: 0x170001BC RID: 444
	// (get) Token: 0x060007A3 RID: 1955 RVA: 0x00021844 File Offset: 0x0001FA44
	public float attackMinimumDistance
	{
		get
		{
			return this._attackMinimumDistance;
		}
	}

	// Token: 0x170001BD RID: 445
	// (get) Token: 0x060007A4 RID: 1956 RVA: 0x0002184C File Offset: 0x0001FA4C
	public float attackMaximumDistance
	{
		get
		{
			return this._attackMaximumDistance;
		}
	}

	// Token: 0x170001BE RID: 446
	// (get) Token: 0x060007A5 RID: 1957 RVA: 0x00021854 File Offset: 0x0001FA54
	public float seekMaximumDistance
	{
		get
		{
			return this._seekMaximumDistance;
		}
	}

	// Token: 0x170001BF RID: 447
	// (get) Token: 0x060007A6 RID: 1958 RVA: 0x0002185C File Offset: 0x0001FA5C
	public float persuitMaximumDistance
	{
		get
		{
			return this._persuitMaximumDistance;
		}
	}

	// Token: 0x170001C0 RID: 448
	// (get) Token: 0x060007A7 RID: 1959 RVA: 0x00021864 File Offset: 0x0001FA64
	public float attackDurationInSeconds
	{
		get
		{
			return this._attackDuration;
		}
	}

	// Token: 0x04000565 RID: 1381
	[SerializeField]
	private float _attackMinimumDistance = 1.5f;

	// Token: 0x04000566 RID: 1382
	[SerializeField]
	private float _attackMaximumDistance = 3f;

	// Token: 0x04000567 RID: 1383
	[SerializeField]
	private float _seekMaximumDistance = 30f;

	// Token: 0x04000568 RID: 1384
	[SerializeField]
	private float _persuitMaximumDistance = 40f;

	// Token: 0x04000569 RID: 1385
	[SerializeField]
	private float _attackDuration = 1.5f;
}
