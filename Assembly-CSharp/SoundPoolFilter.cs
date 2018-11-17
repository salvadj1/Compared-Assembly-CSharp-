using System;
using UnityEngine;

// Token: 0x020000FD RID: 253
[RequireComponent(typeof(Camera))]
public sealed class SoundPoolFilter : MonoBehaviour
{
	// Token: 0x060005DD RID: 1501 RVA: 0x0001C6CC File Offset: 0x0001A8CC
	private void Awake()
	{
		if (global::SoundPoolFilter.instance && global::SoundPoolFilter.instance != this)
		{
			Debug.LogError("ONLY HAVE ONE PLEASE", this);
		}
		else
		{
			global::SoundPoolFilter.instance = this;
			this.awake = true;
			global::SoundPool.enabled = base.enabled;
		}
	}

	// Token: 0x060005DE RID: 1502 RVA: 0x0001C720 File Offset: 0x0001A920
	private void OnApplicationQuit()
	{
		global::SoundPool.quitting = true;
		this.quitting = true;
	}

	// Token: 0x060005DF RID: 1503 RVA: 0x0001C730 File Offset: 0x0001A930
	private void OnEnable()
	{
		if (this.awake)
		{
			global::SoundPool.enabled = true;
		}
	}

	// Token: 0x060005E0 RID: 1504 RVA: 0x0001C744 File Offset: 0x0001A944
	private void OnDisable()
	{
		if (this.awake)
		{
			global::SoundPool.enabled = false;
		}
	}

	// Token: 0x060005E1 RID: 1505 RVA: 0x0001C758 File Offset: 0x0001A958
	private void OnPreCull()
	{
		global::SoundPool.Pump();
	}

	// Token: 0x060005E2 RID: 1506 RVA: 0x0001C760 File Offset: 0x0001A960
	private void OnDestroy()
	{
		if (global::SoundPoolFilter.instance == this)
		{
			this.awake = false;
			global::SoundPoolFilter.instance = null;
			global::SoundPool.enabled = false;
			if (this.quitting)
			{
				global::SoundPool.Drain();
			}
		}
	}

	// Token: 0x040004FB RID: 1275
	private static global::SoundPoolFilter instance;

	// Token: 0x040004FC RID: 1276
	private bool awake;

	// Token: 0x040004FD RID: 1277
	private bool quitting;
}
