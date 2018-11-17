using System;
using UnityEngine;

// Token: 0x0200049F RID: 1183
public class EffectSoundPlayer : MonoBehaviour
{
	// Token: 0x060029D4 RID: 10708 RVA: 0x000A3BB8 File Offset: 0x000A1DB8
	private void Start()
	{
		AudioClip clip = this.sounds[Random.Range(0, this.sounds.Length)];
		clip.Play(base.transform.position, 1f, 1f, 10f);
	}

	// Token: 0x040015C2 RID: 5570
	public AudioClipArray sounds;
}
