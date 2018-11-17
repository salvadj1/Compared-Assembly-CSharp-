using System;
using UnityEngine;

// Token: 0x02000459 RID: 1113
public class CharacterFootstepTrait : CharacterTrait
{
	// Token: 0x17000931 RID: 2353
	// (get) Token: 0x0600289A RID: 10394 RVA: 0x0009FA74 File Offset: 0x0009DC74
	public AudioClipArray defaultFootsteps
	{
		get
		{
			return this._defaultFootsteps;
		}
	}

	// Token: 0x17000932 RID: 2354
	// (get) Token: 0x0600289B RID: 10395 RVA: 0x0009FA7C File Offset: 0x0009DC7C
	public float strideDist
	{
		get
		{
			return this._strideDist;
		}
	}

	// Token: 0x17000933 RID: 2355
	// (get) Token: 0x0600289C RID: 10396 RVA: 0x0009FA84 File Offset: 0x0009DC84
	public float sqrStrideDist
	{
		get
		{
			return this._strideDist * this._strideDist;
		}
	}

	// Token: 0x17000934 RID: 2356
	// (get) Token: 0x0600289D RID: 10397 RVA: 0x0009FA94 File Offset: 0x0009DC94
	public float maxPerSecond
	{
		get
		{
			return this._maxPerSecond;
		}
	}

	// Token: 0x17000935 RID: 2357
	// (get) Token: 0x0600289E RID: 10398 RVA: 0x0009FA9C File Offset: 0x0009DC9C
	public float minInterval
	{
		get
		{
			return (!this.timeLimited) ? 0f : (1f / this._maxPerSecond);
		}
	}

	// Token: 0x17000936 RID: 2358
	// (get) Token: 0x0600289F RID: 10399 RVA: 0x0009FAC0 File Offset: 0x0009DCC0
	public bool timeLimited
	{
		get
		{
			return this._maxPerSecond > 0f && !float.IsInfinity(this._maxPerSecond);
		}
	}

	// Token: 0x17000937 RID: 2359
	// (get) Token: 0x060028A0 RID: 10400 RVA: 0x0009FAE4 File Offset: 0x0009DCE4
	public float minAudioDist
	{
		get
		{
			return this._minAudioDist;
		}
	}

	// Token: 0x17000938 RID: 2360
	// (get) Token: 0x060028A1 RID: 10401 RVA: 0x0009FAEC File Offset: 0x0009DCEC
	public float maxAudioDist
	{
		get
		{
			return this._maxAudioDist;
		}
	}

	// Token: 0x17000939 RID: 2361
	// (get) Token: 0x060028A2 RID: 10402 RVA: 0x0009FAF4 File Offset: 0x0009DCF4
	public bool animal
	{
		get
		{
			return this._animal;
		}
	}

	// Token: 0x04001496 RID: 5270
	[SerializeField]
	private AudioClipArray _defaultFootsteps;

	// Token: 0x04001497 RID: 5271
	[SerializeField]
	private float _strideDist = 2.5f;

	// Token: 0x04001498 RID: 5272
	[SerializeField]
	private float _minAudioDist = 3f;

	// Token: 0x04001499 RID: 5273
	[SerializeField]
	private float _maxAudioDist = 30f;

	// Token: 0x0400149A RID: 5274
	[SerializeField]
	private bool _animal;

	// Token: 0x0400149B RID: 5275
	[SerializeField]
	private float _maxPerSecond = 6f;
}
