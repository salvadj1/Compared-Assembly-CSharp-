using System;
using UnityEngine;

// Token: 0x02000135 RID: 309
public class CharacterTOETrait : global::CharacterTrait
{
	// Token: 0x170001EA RID: 490
	// (get) Token: 0x06000875 RID: 2165 RVA: 0x00024418 File Offset: 0x00022618
	public float attackMinimumDistance
	{
		get
		{
			return this._attackMinimumDistance;
		}
	}

	// Token: 0x170001EB RID: 491
	// (get) Token: 0x06000876 RID: 2166 RVA: 0x00024420 File Offset: 0x00022620
	public float attackMaximumDistance
	{
		get
		{
			return this._attackMaximumDistance;
		}
	}

	// Token: 0x170001EC RID: 492
	// (get) Token: 0x06000877 RID: 2167 RVA: 0x00024428 File Offset: 0x00022628
	public float seekMaximumDistance
	{
		get
		{
			return this._seekMaximumDistance;
		}
	}

	// Token: 0x170001ED RID: 493
	// (get) Token: 0x06000878 RID: 2168 RVA: 0x00024430 File Offset: 0x00022630
	public float persuitMaximumDistance
	{
		get
		{
			return this._persuitMaximumDistance;
		}
	}

	// Token: 0x170001EE RID: 494
	// (get) Token: 0x06000879 RID: 2169 RVA: 0x00024438 File Offset: 0x00022638
	public float attackDurationInSeconds
	{
		get
		{
			return this._attackDuration;
		}
	}

	// Token: 0x04000630 RID: 1584
	[SerializeField]
	private float _attackMinimumDistance = 1.5f;

	// Token: 0x04000631 RID: 1585
	[SerializeField]
	private float _attackMaximumDistance = 3f;

	// Token: 0x04000632 RID: 1586
	[SerializeField]
	private float _seekMaximumDistance = 30f;

	// Token: 0x04000633 RID: 1587
	[SerializeField]
	private float _persuitMaximumDistance = 40f;

	// Token: 0x04000634 RID: 1588
	[SerializeField]
	private float _attackDuration = 1.5f;
}
