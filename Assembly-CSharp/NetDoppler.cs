using System;
using UnityEngine;

// Token: 0x02000091 RID: 145
public class NetDoppler : MonoBehaviour
{
	// Token: 0x06000317 RID: 791 RVA: 0x0000FA7C File Offset: 0x0000DC7C
	public void Update()
	{
		MountedCamera main = MountedCamera.main;
		if (main)
		{
			float num = Vector3.Distance(base.transform.position, main.transform.position);
			float num2 = 1f - Mathf.Clamp01(num / base.audio.maxDistance);
			float pitch = 1f + this.minPitch * num2;
			base.audio.pitch = pitch;
			if (this.lastVolume != null)
			{
				base.audio.volume = this.lastVolume.Value;
				this.lastVolume = null;
			}
		}
		else
		{
			this.lastVolume = new float?(base.audio.volume);
		}
	}

	// Token: 0x04000294 RID: 660
	public float minPitch = 0.5f;

	// Token: 0x04000295 RID: 661
	private float? lastVolume;
}
