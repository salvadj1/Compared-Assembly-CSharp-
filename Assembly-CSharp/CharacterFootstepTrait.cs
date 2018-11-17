using System;
using UnityEngine;

// Token: 0x0200050F RID: 1295
public class CharacterFootstepTrait : global::CharacterTrait
{
	// Token: 0x17000999 RID: 2457
	// (get) Token: 0x06002C2A RID: 11306 RVA: 0x000A59F4 File Offset: 0x000A3BF4
	public global::AudioClipArray defaultFootsteps
	{
		get
		{
			return this._defaultFootsteps;
		}
	}

	// Token: 0x1700099A RID: 2458
	// (get) Token: 0x06002C2B RID: 11307 RVA: 0x000A59FC File Offset: 0x000A3BFC
	public float strideDist
	{
		get
		{
			return this._strideDist;
		}
	}

	// Token: 0x1700099B RID: 2459
	// (get) Token: 0x06002C2C RID: 11308 RVA: 0x000A5A04 File Offset: 0x000A3C04
	public float sqrStrideDist
	{
		get
		{
			return this._strideDist * this._strideDist;
		}
	}

	// Token: 0x1700099C RID: 2460
	// (get) Token: 0x06002C2D RID: 11309 RVA: 0x000A5A14 File Offset: 0x000A3C14
	public float maxPerSecond
	{
		get
		{
			return this._maxPerSecond;
		}
	}

	// Token: 0x1700099D RID: 2461
	// (get) Token: 0x06002C2E RID: 11310 RVA: 0x000A5A1C File Offset: 0x000A3C1C
	public float minInterval
	{
		get
		{
			return (!this.timeLimited) ? 0f : (1f / this._maxPerSecond);
		}
	}

	// Token: 0x1700099E RID: 2462
	// (get) Token: 0x06002C2F RID: 11311 RVA: 0x000A5A40 File Offset: 0x000A3C40
	public bool timeLimited
	{
		get
		{
			return this._maxPerSecond > 0f && !float.IsInfinity(this._maxPerSecond);
		}
	}

	// Token: 0x1700099F RID: 2463
	// (get) Token: 0x06002C30 RID: 11312 RVA: 0x000A5A64 File Offset: 0x000A3C64
	public float minAudioDist
	{
		get
		{
			return this._minAudioDist;
		}
	}

	// Token: 0x170009A0 RID: 2464
	// (get) Token: 0x06002C31 RID: 11313 RVA: 0x000A5A6C File Offset: 0x000A3C6C
	public float maxAudioDist
	{
		get
		{
			return this._maxAudioDist;
		}
	}

	// Token: 0x170009A1 RID: 2465
	// (get) Token: 0x06002C32 RID: 11314 RVA: 0x000A5A74 File Offset: 0x000A3C74
	public bool animal
	{
		get
		{
			return this._animal;
		}
	}

	// Token: 0x04001619 RID: 5657
	[SerializeField]
	private global::AudioClipArray _defaultFootsteps;

	// Token: 0x0400161A RID: 5658
	[SerializeField]
	private float _strideDist = 2.5f;

	// Token: 0x0400161B RID: 5659
	[SerializeField]
	private float _minAudioDist = 3f;

	// Token: 0x0400161C RID: 5660
	[SerializeField]
	private float _maxAudioDist = 30f;

	// Token: 0x0400161D RID: 5661
	[SerializeField]
	private bool _animal;

	// Token: 0x0400161E RID: 5662
	[SerializeField]
	private float _maxPerSecond = 6f;
}
