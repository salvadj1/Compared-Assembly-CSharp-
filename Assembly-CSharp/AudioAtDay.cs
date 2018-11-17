using System;
using UnityEngine;

// Token: 0x0200083F RID: 2111
[RequireComponent(typeof(AudioSource))]
public class AudioAtDay : MonoBehaviour
{
	// Token: 0x06004ABC RID: 19132 RVA: 0x001465E4 File Offset: 0x001447E4
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

	// Token: 0x06004ABD RID: 19133 RVA: 0x00146630 File Offset: 0x00144830
	protected void Update()
	{
		int num = (!this.sky.IsDay) ? -1 : 1;
		this.lerpTime = Mathf.Clamp01(this.lerpTime + (float)num * Time.deltaTime / this.fadeTime);
		this.audioComponent.volume = Mathf.Lerp(0f, this.audioVolume, this.lerpTime);
	}

	// Token: 0x04002BCA RID: 11210
	public TOD_Sky sky;

	// Token: 0x04002BCB RID: 11211
	public float fadeTime = 1f;

	// Token: 0x04002BCC RID: 11212
	private float lerpTime;

	// Token: 0x04002BCD RID: 11213
	private AudioSource audioComponent;

	// Token: 0x04002BCE RID: 11214
	private float audioVolume;
}
