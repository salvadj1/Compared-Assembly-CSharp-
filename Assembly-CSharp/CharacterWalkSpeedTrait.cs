using System;
using UnityEngine;

// Token: 0x0200011B RID: 283
public class CharacterWalkSpeedTrait : CharacterTrait
{
	// Token: 0x170001C1 RID: 449
	// (get) Token: 0x060007B8 RID: 1976 RVA: 0x000224AC File Offset: 0x000206AC
	public float jog
	{
		get
		{
			return this._jog;
		}
	}

	// Token: 0x170001C2 RID: 450
	// (get) Token: 0x060007B9 RID: 1977 RVA: 0x000224B4 File Offset: 0x000206B4
	public float run
	{
		get
		{
			return this._run;
		}
	}

	// Token: 0x170001C3 RID: 451
	// (get) Token: 0x060007BA RID: 1978 RVA: 0x000224BC File Offset: 0x000206BC
	public float walk
	{
		get
		{
			return this._walk;
		}
	}

	// Token: 0x060007BB RID: 1979 RVA: 0x000224C4 File Offset: 0x000206C4
	public bool IsRunningAtSpeed(float metersPerSecond)
	{
		return (this._jog >= this._run) ? (this._run > metersPerSecond) : (this._run <= metersPerSecond);
	}

	// Token: 0x060007BC RID: 1980 RVA: 0x000224F4 File Offset: 0x000206F4
	public bool IsJoggingOrRunningAtSpeed(float metersPerSecond)
	{
		return (this._jog >= this._run) ? (this._run <= metersPerSecond) : (this._jog <= metersPerSecond);
	}

	// Token: 0x0400056D RID: 1389
	[SerializeField]
	private float _jog = 3f;

	// Token: 0x0400056E RID: 1390
	[SerializeField]
	private float _run = 6f;

	// Token: 0x0400056F RID: 1391
	[SerializeField]
	private float _walk = 1.8f;
}
