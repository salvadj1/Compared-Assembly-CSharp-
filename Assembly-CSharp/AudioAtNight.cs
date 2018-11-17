using System;
using UnityEngine;

// Token: 0x02000840 RID: 2112
[RequireComponent(typeof(AudioSource))]
public class AudioAtNight : MonoBehaviour
{
	// Token: 0x06004ABF RID: 19135 RVA: 0x001466AC File Offset: 0x001448AC
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

	// Token: 0x06004AC0 RID: 19136 RVA: 0x001466F8 File Offset: 0x001448F8
	protected void Update()
	{
		int num = (!this.sky.IsNight) ? -1 : 1;
		this.lerpTime = Mathf.Clamp01(this.lerpTime + (float)num * Time.deltaTime / this.fadeTime);
		this.audioComponent.volume = Mathf.Lerp(0f, this.audioVolume, this.lerpTime);
	}

	// Token: 0x04002BCF RID: 11215
	public TOD_Sky sky;

	// Token: 0x04002BD0 RID: 11216
	public float fadeTime = 1f;

	// Token: 0x04002BD1 RID: 11217
	private float lerpTime;

	// Token: 0x04002BD2 RID: 11218
	private AudioSource audioComponent;

	// Token: 0x04002BD3 RID: 11219
	private float audioVolume;
}
