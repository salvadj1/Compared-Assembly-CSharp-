using System;
using UnityEngine;

// Token: 0x02000935 RID: 2357
[RequireComponent(typeof(AudioSource))]
public class AudioAtNight : MonoBehaviour
{
	// Token: 0x06004F7A RID: 20346 RVA: 0x00150610 File Offset: 0x0014E810
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

	// Token: 0x06004F7B RID: 20347 RVA: 0x0015065C File Offset: 0x0014E85C
	protected void Update()
	{
		int num = (!this.sky.IsNight) ? -1 : 1;
		this.lerpTime = Mathf.Clamp01(this.lerpTime + (float)num * Time.deltaTime / this.fadeTime);
		this.audioComponent.volume = Mathf.Lerp(0f, this.audioVolume, this.lerpTime);
	}

	// Token: 0x04002E1D RID: 11805
	public global::TOD_Sky sky;

	// Token: 0x04002E1E RID: 11806
	public float fadeTime = 1f;

	// Token: 0x04002E1F RID: 11807
	private float lerpTime;

	// Token: 0x04002E20 RID: 11808
	private AudioSource audioComponent;

	// Token: 0x04002E21 RID: 11809
	private float audioVolume;
}
