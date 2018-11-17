using System;
using UnityEngine;

// Token: 0x020004A0 RID: 1184
public class ExplosionEffect : MonoBehaviour
{
	// Token: 0x060029D6 RID: 10710 RVA: 0x000A3C18 File Offset: 0x000A1E18
	public virtual void Start()
	{
		this.startTime = Time.time;
		Object.Destroy(base.gameObject, 3f);
		base.audio.pitch = Random.Range(0.9f, 1f);
		base.audio.Play();
	}

	// Token: 0x060029D7 RID: 10711 RVA: 0x000A3C68 File Offset: 0x000A1E68
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

	// Token: 0x040015C3 RID: 5571
	public Light myLight;

	// Token: 0x040015C4 RID: 5572
	public float initialLightIntensity = 2f;

	// Token: 0x040015C5 RID: 5573
	public float startTime;
}
