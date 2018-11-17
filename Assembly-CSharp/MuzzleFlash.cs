using System;
using UnityEngine;

// Token: 0x020004A2 RID: 1186
public class MuzzleFlash : MonoBehaviour
{
	// Token: 0x060029DE RID: 10718 RVA: 0x000A3E04 File Offset: 0x000A2004
	private void Start()
	{
		this.startTime = Time.time;
		this.initialIntensity = this.myLight.intensity;
	}

	// Token: 0x060029DF RID: 10719 RVA: 0x000A3E24 File Offset: 0x000A2024
	private void Update()
	{
		float num = Mathf.Clamp(1f - (Time.time - this.startTime) / 0.1f, 0f, 1f);
		this.myLight.intensity = this.initialIntensity * num;
	}

	// Token: 0x040015C9 RID: 5577
	public Light myLight;

	// Token: 0x040015CA RID: 5578
	private float initialIntensity;

	// Token: 0x040015CB RID: 5579
	private float startTime;
}
