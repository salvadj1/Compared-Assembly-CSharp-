using System;
using UnityEngine;

// Token: 0x0200055B RID: 1371
public class ExplosionEffect : MonoBehaviour
{
	// Token: 0x06002D88 RID: 11656 RVA: 0x000AB9B0 File Offset: 0x000A9BB0
	public virtual void Start()
	{
		this.startTime = Time.time;
		Object.Destroy(base.gameObject, 3f);
		base.audio.pitch = Random.Range(0.9f, 1f);
		base.audio.Play();
	}

	// Token: 0x06002D89 RID: 11657 RVA: 0x000ABA00 File Offset: 0x000A9C00
	public virtual void Update()
	{
		float num = Time.time - this.startTime;
		if (this.myLight)
		{
			this.myLight.intensity = Mathf.Clamp(this.initialLightIntensity * (1f - num / 0.25f), 0f, this.initialLightIntensity);
			if (this.myLight.intensity <= 0f)
			{
				Object.Destroy(this.myLight.gameObject);
				this.myLight = null;
			}
		}
	}

	// Token: 0x04001780 RID: 6016
	public Light myLight;

	// Token: 0x04001781 RID: 6017
	public float initialLightIntensity = 2f;

	// Token: 0x04001782 RID: 6018
	public float startTime;
}
