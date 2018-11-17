using System;
using UnityEngine;

// Token: 0x02000827 RID: 2087
public abstract class dfTweenPlayableBase : MonoBehaviour
{
	// Token: 0x17000DDC RID: 3548
	// (get) Token: 0x0600486B RID: 18539
	// (set) Token: 0x0600486C RID: 18540
	public abstract string TweenName { get; set; }

	// Token: 0x17000DDD RID: 3549
	// (get) Token: 0x0600486D RID: 18541
	public abstract bool IsPlaying { get; }

	// Token: 0x0600486E RID: 18542
	public abstract void Play();

	// Token: 0x0600486F RID: 18543
	public abstract void Stop();

	// Token: 0x06004870 RID: 18544
	public abstract void Reset();

	// Token: 0x06004871 RID: 18545 RVA: 0x0011166C File Offset: 0x0010F86C
	public void Enable()
	{
		base.enabled = true;
	}

	// Token: 0x06004872 RID: 18546 RVA: 0x00111678 File Offset: 0x0010F878
	public void Disable()
	{
		base.enabled = false;
	}

	// Token: 0x06004873 RID: 18547 RVA: 0x00111684 File Offset: 0x0010F884
	public override string ToString()
	{
		return this.TweenName + " - " + base.ToString();
	}
}
