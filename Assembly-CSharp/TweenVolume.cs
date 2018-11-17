using System;
using UnityEngine;

// Token: 0x020007B8 RID: 1976
[AddComponentMenu("NGUI/Tween/Volume")]
public class TweenVolume : UITweener
{
	// Token: 0x17000DC2 RID: 3522
	// (get) Token: 0x0600474D RID: 18253 RVA: 0x0011EAC4 File Offset: 0x0011CCC4
	public AudioSource audioSource
	{
		get
		{
			if (this.mSource == null)
			{
				this.mSource = base.audio;
				if (this.mSource == null)
				{
					this.mSource = base.GetComponentInChildren<AudioSource>();
					if (this.mSource == null)
					{
						Debug.LogError("TweenVolume needs an AudioSource to work with", this);
						base.enabled = false;
					}
				}
			}
			return this.mSource;
		}
	}

	// Token: 0x17000DC3 RID: 3523
	// (get) Token: 0x0600474E RID: 18254 RVA: 0x0011EB34 File Offset: 0x0011CD34
	// (set) Token: 0x0600474F RID: 18255 RVA: 0x0011EB44 File Offset: 0x0011CD44
	public float volume
	{
		get
		{
			return this.audioSource.volume;
		}
		set
		{
			this.audioSource.volume = value;
		}
	}

	// Token: 0x06004750 RID: 18256 RVA: 0x0011EB54 File Offset: 0x0011CD54
	protected override void OnUpdate(float factor)
	{
		this.volume = this.from * (1f - factor) + this.to * factor;
		this.mSource.enabled = (this.mSource.volume > 0.01f);
	}

	// Token: 0x06004751 RID: 18257 RVA: 0x0011EB9C File Offset: 0x0011CD9C
	public static TweenVolume Begin(GameObject go, float duration, float targetVolume)
	{
		TweenVolume tweenVolume = UITweener.Begin<TweenVolume>(go, duration);
		tweenVolume.from = tweenVolume.volume;
		tweenVolume.to = targetVolume;
		return tweenVolume;
	}

	// Token: 0x0400274D RID: 10061
	public float from;

	// Token: 0x0400274E RID: 10062
	public float to = 1f;

	// Token: 0x0400274F RID: 10063
	private AudioSource mSource;
}
