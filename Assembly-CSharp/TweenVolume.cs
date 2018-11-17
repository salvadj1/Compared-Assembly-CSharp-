using System;
using UnityEngine;

// Token: 0x020008A5 RID: 2213
[AddComponentMenu("NGUI/Tween/Volume")]
public class TweenVolume : global::UITweener
{
	// Token: 0x17000E54 RID: 3668
	// (get) Token: 0x06004BDC RID: 19420 RVA: 0x001284E8 File Offset: 0x001266E8
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

	// Token: 0x17000E55 RID: 3669
	// (get) Token: 0x06004BDD RID: 19421 RVA: 0x00128558 File Offset: 0x00126758
	// (set) Token: 0x06004BDE RID: 19422 RVA: 0x00128568 File Offset: 0x00126768
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

	// Token: 0x06004BDF RID: 19423 RVA: 0x00128578 File Offset: 0x00126778
	protected override void OnUpdate(float factor)
	{
		this.volume = this.from * (1f - factor) + this.to * factor;
		this.mSource.enabled = (this.mSource.volume > 0.01f);
	}

	// Token: 0x06004BE0 RID: 19424 RVA: 0x001285C0 File Offset: 0x001267C0
	public static global::TweenVolume Begin(GameObject go, float duration, float targetVolume)
	{
		global::TweenVolume tweenVolume = global::UITweener.Begin<global::TweenVolume>(go, duration);
		tweenVolume.from = tweenVolume.volume;
		tweenVolume.to = targetVolume;
		return tweenVolume;
	}

	// Token: 0x04002987 RID: 10631
	public float from;

	// Token: 0x04002988 RID: 10632
	public float to = 1f;

	// Token: 0x04002989 RID: 10633
	private AudioSource mSource;
}
