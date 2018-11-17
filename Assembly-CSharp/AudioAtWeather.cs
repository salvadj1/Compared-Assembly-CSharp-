using System;
using UnityEngine;

// Token: 0x02000841 RID: 2113
[RequireComponent(typeof(AudioSource))]
public class AudioAtWeather : MonoBehaviour
{
	// Token: 0x06004AC2 RID: 19138 RVA: 0x00146774 File Offset: 0x00144974
	protected void OnEnable()
	{
		if (!this.sky)
		{
			Debug.LogError("Sky instance reference not set. Disabling script.");
			base.enabled = false;
		}
		this.audioComponent = base.audio;
		this.audioVolume = this.audioComponent.volume;
	}

	// Token: 0x06004AC3 RID: 19139 RVA: 0x001467C0 File Offset: 0x001449C0
	protected void Update()
	{
		int num = (this.sky.Components.Weather.Weather != this.type) ? -1 : 1;
		this.lerpTime = Mathf.Clamp01(this.lerpTime + (float)num * Time.deltaTime / this.fadeTime);
		this.audioComponent.volume = Mathf.Lerp(0f, this.audioVolume, this.lerpTime);
	}

	// Token: 0x04002BD4 RID: 11220
	public TOD_Sky sky;

	// Token: 0x04002BD5 RID: 11221
	public TOD_Weather.WeatherType type;

	// Token: 0x04002BD6 RID: 11222
	public float fadeTime = 1f;

	// Token: 0x04002BD7 RID: 11223
	private float lerpTime;

	// Token: 0x04002BD8 RID: 11224
	private AudioSource audioComponent;

	// Token: 0x04002BD9 RID: 11225
	private float audioVolume;
}
