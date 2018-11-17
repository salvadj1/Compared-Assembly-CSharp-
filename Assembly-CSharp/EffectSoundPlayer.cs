using System;
using UnityEngine;

// Token: 0x0200055A RID: 1370
public class EffectSoundPlayer : MonoBehaviour
{
	// Token: 0x06002D86 RID: 11654 RVA: 0x000AB950 File Offset: 0x000A9B50
	private void Start()
	{
		AudioClip clip = this.sounds[Random.Range(0, this.sounds.Length)];
		clip.Play(base.transform.position, 1f, 1f, 10f);
	}

	// Token: 0x0400177F RID: 6015
	public global::AudioClipArray sounds;
}
