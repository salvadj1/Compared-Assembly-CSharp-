using System;
using UnityEngine;

// Token: 0x020004CE RID: 1230
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioSource))]
public class AmbientAudioPlayer : MonoBehaviour
{
	// Token: 0x06002A9A RID: 10906 RVA: 0x000A9AA4 File Offset: 0x000A7CA4
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

	// Token: 0x06002A9B RID: 10907 RVA: 0x000A9B38 File Offset: 0x000A7D38
	public bool NeedsAudioUpdate()
	{
		return !EnvironmentControlCenter.Singleton || (EnvironmentControlCenter.Singleton && !EnvironmentControlCenter.Singleton.IsNight() && (this.daySource.volume < 1f || this.nightSource.volume > 0f)) || (EnvironmentControlCenter.Singleton && EnvironmentControlCenter.Singleton.IsNight() && (this.nightSource.volume < 1f || this.daySource.volume > 0f));
	}

	// Token: 0x06002A9C RID: 10908 RVA: 0x000A9BEC File Offset: 0x000A7DEC
	private void CheckTimeChange()
	{
		if (this.NeedsAudioUpdate())
		{
			base.Invoke("UpdateVolume", 0f);
		}
	}

	// Token: 0x06002A9D RID: 10909 RVA: 0x000A9C0C File Offset: 0x000A7E0C
	private void UpdateVolume()
	{
		if (this.NeedsAudioUpdate())
		{
			base.Invoke("UpdateVolume", Time.deltaTime);
			bool flag = !EnvironmentControlCenter.Singleton || !EnvironmentControlCenter.Singleton.IsNight();
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

	// Token: 0x04001722 RID: 5922
	public AudioClip daySound;

	// Token: 0x04001723 RID: 5923
	public AudioClip nightSound;

	// Token: 0x04001724 RID: 5924
	public AudioSource daySource;

	// Token: 0x04001725 RID: 5925
	public AudioSource nightSource;
}
