using System;
using UnityEngine;

// Token: 0x02000934 RID: 2356
[RequireComponent(typeof(AudioSource))]
public class AudioAtDay : MonoBehaviour
{
	// Token: 0x06004F77 RID: 20343 RVA: 0x00150548 File Offset: 0x0014E748
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

	// Token: 0x06004F78 RID: 20344 RVA: 0x00150594 File Offset: 0x0014E794
	protected void Update()
	{
		int num = (!this.sky.IsDay) ? -1 : 1;
		this.lerpTime = Mathf.Clamp01(this.lerpTime + (float)num * Time.deltaTime / this.fadeTime);
		this.audioComponent.volume = Mathf.Lerp(0f, this.audioVolume, this.lerpTime);
	}

	// Token: 0x04002E18 RID: 11800
	public global::TOD_Sky sky;

	// Token: 0x04002E19 RID: 11801
	public float fadeTime = 1f;

	// Token: 0x04002E1A RID: 11802
	private float lerpTime;

	// Token: 0x04002E1B RID: 11803
	private AudioSource audioComponent;

	// Token: 0x04002E1C RID: 11804
	private float audioVolume;
}
