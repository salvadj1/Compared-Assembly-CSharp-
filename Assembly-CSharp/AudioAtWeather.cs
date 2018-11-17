using System;
using UnityEngine;

// Token: 0x02000936 RID: 2358
[RequireComponent(typeof(AudioSource))]
public class AudioAtWeather : MonoBehaviour
{
	// Token: 0x06004F7D RID: 20349 RVA: 0x001506D8 File Offset: 0x0014E8D8
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

	// Token: 0x06004F7E RID: 20350 RVA: 0x00150724 File Offset: 0x0014E924
	protected void Update()
	{
		int num = (this.sky.Components.Weather.Weather != this.type) ? -1 : 1;
		this.lerpTime = Mathf.Clamp01(this.lerpTime + (float)num * Time.deltaTime / this.fadeTime);
		this.audioComponent.volume = Mathf.Lerp(0f, this.audioVolume, this.lerpTime);
	}

	// Token: 0x04002E22 RID: 11810
	public global::TOD_Sky sky;

	// Token: 0x04002E23 RID: 11811
	public global::TOD_Weather.WeatherType type;

	// Token: 0x04002E24 RID: 11812
	public float fadeTime = 1f;

	// Token: 0x04002E25 RID: 11813
	private float lerpTime;

	// Token: 0x04002E26 RID: 11814
	private AudioSource audioComponent;

	// Token: 0x04002E27 RID: 11815
	private float audioVolume;
}
