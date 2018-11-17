using System;
using UnityEngine;

// Token: 0x0200013A RID: 314
public class CharacterWalkSpeedTrait : global::CharacterTrait
{
	// Token: 0x170001EF RID: 495
	// (get) Token: 0x0600088A RID: 2186 RVA: 0x00025080 File Offset: 0x00023280
	public float jog
	{
		get
		{
			return this._jog;
		}
	}

	// Token: 0x170001F0 RID: 496
	// (get) Token: 0x0600088B RID: 2187 RVA: 0x00025088 File Offset: 0x00023288
	public float run
	{
		get
		{
			return this._run;
		}
	}

	// Token: 0x170001F1 RID: 497
	// (get) Token: 0x0600088C RID: 2188 RVA: 0x00025090 File Offset: 0x00023290
	public float walk
	{
		get
		{
			return this._walk;
		}
	}

	// Token: 0x0600088D RID: 2189 RVA: 0x00025098 File Offset: 0x00023298
	public bool IsRunningAtSpeed(float metersPerSecond)
	{
		return (this._jog >= this._run) ? (this._run > metersPerSecond) : (this._run <= metersPerSecond);
	}

	// Token: 0x0600088E RID: 2190 RVA: 0x000250C8 File Offset: 0x000232C8
	public bool IsJoggingOrRunningAtSpeed(float metersPerSecond)
	{
		return (this._jog >= this._run) ? (this._run <= metersPerSecond) : (this._jog <= metersPerSecond);
	}

	// Token: 0x04000638 RID: 1592
	[SerializeField]
	private float _jog = 3f;

	// Token: 0x04000639 RID: 1593
	[SerializeField]
	private float _run = 6f;

	// Token: 0x0400063A RID: 1594
	[SerializeField]
	private float _walk = 1.8f;
}
