using System;
using UnityEngine;

// Token: 0x0200055D RID: 1373
public class MuzzleFlash : MonoBehaviour
{
	// Token: 0x06002D90 RID: 11664 RVA: 0x000ABB9C File Offset: 0x000A9D9C
	private void Start()
	{
		this.startTime = Time.time;
		this.initialIntensity = this.myLight.intensity;
	}

	// Token: 0x06002D91 RID: 11665 RVA: 0x000ABBBC File Offset: 0x000A9DBC
	private void Update()
	{
		float num = Mathf.Clamp(1f - (Time.time - this.startTime) / 0.1f, 0f, 1f);
		this.myLight.intensity = this.initialIntensity * num;
	}

	// Token: 0x04001786 RID: 6022
	public Light myLight;

	// Token: 0x04001787 RID: 6023
	private float initialIntensity;

	// Token: 0x04001788 RID: 6024
	private float startTime;
}
