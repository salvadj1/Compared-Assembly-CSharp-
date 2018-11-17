using System;
using UnityEngine;

// Token: 0x0200061F RID: 1567
public class MusicPlayer : MonoBehaviour
{
	// Token: 0x060037A1 RID: 14241 RVA: 0x000CC3A4 File Offset: 0x000CA5A4
	private void Start()
	{
		this.wasMuted = MusicPlayer.settings.mute;
		this.nextMusicTime = Time.time + 3f + this.startDelay;
		base.audio.volume = 0f;
		if (this.tracks == null || this.tracks.Length == 0)
		{
			base.enabled = false;
		}
	}

	// Token: 0x060037A2 RID: 14242 RVA: 0x000CC404 File Offset: 0x000CA604
	private void Update()
	{
		int frameCount = Time.frameCount;
		if (frameCount != MusicPlayer.savedFrameCount)
		{
			MusicPlayer.savedFrameCount = frameCount;
			if (Input.GetKeyDown(280))
			{
				MusicPlayer.settings.mute = !MusicPlayer.settings.mute;
			}
		}
		if (this.wasMuted != MusicPlayer.settings.mute)
		{
			if (this.wasMuted && Time.time > this.nextMusicTime)
			{
				this.nextMusicTime = Time.time;
			}
			else
			{
				base.audio.Stop();
				this.nextMusicTime = Time.time;
			}
			this.wasMuted = !this.wasMuted;
		}
		if (!this.wasMuted)
		{
			if (Time.time > this.nextMusicTime)
			{
				if (this.tracks.Length == 0)
				{
					return;
				}
				AudioClip audioClip = this.tracks[Random.Range(0, this.tracks.Length)];
				base.audio.Stop();
				base.audio.clip = audioClip;
				this.nextMusicTime = Time.time + audioClip.length + this.timeBetweenTracks * Random.RandomRange(0.75f, 1.25f);
				base.audio.Play();
			}
			float num = this.targetVolume * sound.music;
			if (base.audio.volume < num)
			{
				base.audio.volume += Time.deltaTime / 3f * num;
			}
			if (base.audio.volume > num)
			{
				base.audio.volume = num;
			}
		}
	}

	// Token: 0x04001BC9 RID: 7113
	public float timeBetweenTracks = 600f;

	// Token: 0x04001BCA RID: 7114
	public float targetVolume = 0.2f;

	// Token: 0x04001BCB RID: 7115
	public float startDelay;

	// Token: 0x04001BCC RID: 7116
	public AudioClip[] tracks;

	// Token: 0x04001BCD RID: 7117
	private float nextMusicTime = 5f;

	// Token: 0x04001BCE RID: 7118
	private bool wasMuted = true;

	// Token: 0x04001BCF RID: 7119
	private static int savedFrameCount = -1;

	// Token: 0x02000620 RID: 1568
	private static class settings
	{
		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x060037A4 RID: 14244 RVA: 0x000CC5A0 File Offset: 0x000CA7A0
		// (set) Token: 0x060037A5 RID: 14245 RVA: 0x000CC5A8 File Offset: 0x000CA7A8
		public static bool mute
		{
			get
			{
				return MusicPlayer.settings._mute;
			}
			set
			{
				if (value != MusicPlayer.settings._mute)
				{
					if (value)
					{
						PlayerPrefs.SetInt("MUSIC_MUTE", 1);
					}
					else
					{
						PlayerPrefs.DeleteKey("MUSIC_MUTE");
					}
					MusicPlayer.settings._mute = value;
				}
			}
		}

		// Token: 0x04001BD0 RID: 7120
		private static bool _mute = PlayerPrefs.GetInt("MUSIC_MUTE", 0) != 0;
	}
}
