using System;
using UnityEngine;

// Token: 0x02000589 RID: 1417
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioSource))]
public class AmbientAudioPlayer : MonoBehaviour
{
	// Token: 0x06002E4C RID: 11852 RVA: 0x000B183C File Offset: 0x000AFA3C
	private void Awake()
	{
		base.InvokeRepeating("CheckTimeChange", 0f, 5f);
		this.daySource.clip = this.daySound;
		this.nightSource.clip = this.nightSound;
		this.daySource.volume = 0f;
		this.nightSource.volume = 0f;
		this.daySource.Stop();
		this.nightSource.Stop();
		this.daySource.enabled = false;
		this.nightSource.enabled = false;
	}

	// Token: 0x06002E4D RID: 11853 RVA: 0x000B18D0 File Offset: 0x000AFAD0
	public bool NeedsAudioUpdate()
	{
		return !global::EnvironmentControlCenter.Singleton || (global::EnvironmentControlCenter.Singleton && !global::EnvironmentControlCenter.Singleton.IsNight() && (this.daySource.volume < 1f || this.nightSource.volume > 0f)) || (global::EnvironmentControlCenter.Singleton && global::EnvironmentControlCenter.Singleton.IsNight() && (this.nightSource.volume < 1f || this.daySource.volume > 0f));
	}

	// Token: 0x06002E4E RID: 11854 RVA: 0x000B1984 File Offset: 0x000AFB84
	private void CheckTimeChange()
	{
		if (this.NeedsAudioUpdate())
		{
			base.Invoke("UpdateVolume", 0f);
		}
	}

	// Token: 0x06002E4F RID: 11855 RVA: 0x000B19A4 File Offset: 0x000AFBA4
	private void UpdateVolume()
	{
		if (this.NeedsAudioUpdate())
		{
			base.Invoke("UpdateVolume", Time.deltaTime);
			bool flag = !global::EnvironmentControlCenter.Singleton || !global::EnvironmentControlCenter.Singleton.IsNight();
			AudioSource audioSource = (!flag) ? this.nightSource : this.daySource;
			AudioSource audioSource2 = (!flag) ? this.daySource : this.nightSource;
			if (!audioSource.isPlaying)
			{
				audioSource.enabled = true;
				audioSource.Play();
			}
			audioSource.volume += 0.2f * Time.deltaTime;
			audioSource2.volume -= 0.2f * Time.deltaTime;
			if (audioSource.volume > 1f)
			{
				audioSource.volume = 1f;
			}
			if (audioSource2.volume < 0f)
			{
				audioSource2.volume = 0f;
				audioSource2.Stop();
				audioSource2.enabled = false;
			}
			return;
		}
	}

	// Token: 0x040018DF RID: 6367
	public AudioClip daySound;

	// Token: 0x040018E0 RID: 6368
	public AudioClip nightSound;

	// Token: 0x040018E1 RID: 6369
	public AudioSource daySource;

	// Token: 0x040018E2 RID: 6370
	public AudioSource nightSource;
}
