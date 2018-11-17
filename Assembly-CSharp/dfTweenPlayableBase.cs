using System;
using UnityEngine;

// Token: 0x02000745 RID: 1861
public abstract class dfTweenPlayableBase : MonoBehaviour
{
	// Token: 0x17000D4C RID: 3404
	// (get) Token: 0x0600440A RID: 17418
	// (set) Token: 0x0600440B RID: 17419
	public abstract string TweenName { get; set; }

	// Token: 0x17000D4D RID: 3405
	// (get) Token: 0x0600440C RID: 17420
	public abstract bool IsPlaying { get; }

	// Token: 0x0600440D RID: 17421
	public abstract void Play();

	// Token: 0x0600440E RID: 17422
	public abstract void Stop();

	// Token: 0x0600440F RID: 17423
	public abstract void Reset();

	// Token: 0x06004410 RID: 17424 RVA: 0x00107CEC File Offset: 0x00105EEC
	public void Enable()
	{
		base.enabled = true;
	}

	// Token: 0x06004411 RID: 17425 RVA: 0x00107CF8 File Offset: 0x00105EF8
	public void Disable()
	{
		base.enabled = false;
	}

	// Token: 0x06004412 RID: 17426 RVA: 0x00107D04 File Offset: 0x00105F04
	public override string ToString()
	{
		return this.TweenName + " - " + base.ToString();
	}
}
