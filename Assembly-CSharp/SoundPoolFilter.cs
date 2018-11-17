using System;
using UnityEngine;

// Token: 0x020000E9 RID: 233
[RequireComponent(typeof(Camera))]
public sealed class SoundPoolFilter : MonoBehaviour
{
	// Token: 0x0600055F RID: 1375 RVA: 0x0001AD04 File Offset: 0x00018F04
	private void Awake()
	{
		if (SoundPoolFilter.instance && SoundPoolFilter.instance != this)
		{
			Debug.LogError("ONLY HAVE ONE PLEASE", this);
		}
		else
		{
			SoundPoolFilter.instance = this;
			this.awake = true;
			SoundPool.enabled = base.enabled;
		}
	}

	// Token: 0x06000560 RID: 1376 RVA: 0x0001AD58 File Offset: 0x00018F58
	private void OnApplicationQuit()
	{
		SoundPool.quitting = true;
		this.quitting = true;
	}

	// Token: 0x06000561 RID: 1377 RVA: 0x0001AD68 File Offset: 0x00018F68
	private void OnEnable()
	{
		if (this.awake)
		{
			SoundPool.enabled = true;
		}
	}

	// Token: 0x06000562 RID: 1378 RVA: 0x0001AD7C File Offset: 0x00018F7C
	private void OnDisable()
	{
		if (this.awake)
		{
			SoundPool.enabled = false;
		}
	}

	// Token: 0x06000563 RID: 1379 RVA: 0x0001AD90 File Offset: 0x00018F90
	private void OnPreCull()
	{
		SoundPool.Pump();
	}

	// Token: 0x06000564 RID: 1380 RVA: 0x0001AD98 File Offset: 0x00018F98
	private void OnDestroy()
	{
		if (SoundPoolFilter.instance == this)
		{
			this.awake = false;
			SoundPoolFilter.instance = null;
			SoundPool.enabled = false;
			if (this.quitting)
			{
				SoundPool.Drain();
			}
		}
	}

	// Token: 0x0400048C RID: 1164
	private static SoundPoolFilter instance;

	// Token: 0x0400048D RID: 1165
	private bool awake;

	// Token: 0x0400048E RID: 1166
	private bool quitting;
}
