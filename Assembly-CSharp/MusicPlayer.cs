using System;
using UnityEngine;

// Token: 0x020006E0 RID: 1760
public class MusicPlayer : MonoBehaviour
{
	// Token: 0x06003B81 RID: 15233 RVA: 0x000D4A7C File Offset: 0x000D2C7C
	private void Start()
	{
		this.wasMuted = global::MusicPlayer.settings.mute;
		this.nextMusicTime = Time.time + 3f + this.startDelay;
		base.audio.volume = 0f;
		if (this.tracks == null || this.tracks.Length == 0)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06003B82 RID: 15234 RVA: 0x000D4ADC File Offset: 0x000D2CDC
	private void Update()
	{
		int frameCount = Time.frameCount;
		if (frameCount != global::MusicPlayer.savedFrameCount)
		{
			global::MusicPlayer.savedFrameCount = frameCount;
			if (Input.GetKeyDown(280))
			{
				global::MusicPlayer.settings.mute = !global::MusicPlayer.settings.mute;
			}
		}
		if (this.wasMuted != global::MusicPlayer.settings.mute)
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
			float num = this.targetVolume * global::sound.music;
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

	// Token: 0x04001DB4 RID: 7604
	public float timeBetweenTracks = 600f;

	// Token: 0x04001DB5 RID: 7605
	public float targetVolume = 0.2f;

	// Token: 0x04001DB6 RID: 7606
	public float startDelay;

	// Token: 0x04001DB7 RID: 7607
	public AudioClip[] tracks;

	// Token: 0x04001DB8 RID: 7608
	private float nextMusicTime = 5f;

	// Token: 0x04001DB9 RID: 7609
	private bool wasMuted = true;

	// Token: 0x04001DBA RID: 7610
	private static int savedFrameCount = -1;

	// Token: 0x020006E1 RID: 1761
	private static class settings
	{
		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x06003B84 RID: 15236 RVA: 0x000D4C78 File Offset: 0x000D2E78
		// (set) Token: 0x06003B85 RID: 15237 RVA: 0x000D4C80 File Offset: 0x000D2E80
		public static bool mute
		{
			get
			{
				return global::MusicPlayer.settings._mute;
			}
			set
			{
				if (value != global::MusicPlayer.settings._mute)
				{
					if (value)
					{
						PlayerPrefs.SetInt("MUSIC_MUTE", 1);
					}
					else
					{
						PlayerPrefs.DeleteKey("MUSIC_MUTE");
					}
					global::MusicPlayer.settings._mute = value;
				}
			}
		}

		// Token: 0x04001DBB RID: 7611
		private static bool _mute = PlayerPrefs.GetInt("MUSIC_MUTE", 0) != 0;
	}
}
