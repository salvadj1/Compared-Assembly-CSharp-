using System;
using UnityEngine;

// Token: 0x020000A4 RID: 164
public class NetDoppler : MonoBehaviour
{
	// Token: 0x0600038F RID: 911 RVA: 0x0001126C File Offset: 0x0000F46C
	public void Update()
	{
		global::MountedCamera main = global::MountedCamera.main;
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

	// Token: 0x040002FF RID: 767
	public float minPitch = 0.5f;

	// Token: 0x04000300 RID: 768
	private float? lastVolume;
}
