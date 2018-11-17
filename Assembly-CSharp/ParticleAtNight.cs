using System;
using UnityEngine;

// Token: 0x02000939 RID: 2361
[RequireComponent(typeof(ParticleSystem))]
public class ParticleAtNight : MonoBehaviour
{
	// Token: 0x06004F85 RID: 20357 RVA: 0x00150928 File Offset: 0x0014EB28
	protected void OnEnable()
	{
		if (!this.sky)
		{
			Debug.LogError("Sky instance reference not set. Disabling script.");
			base.enabled = false;
		}
		this.particleComponent = base.particleSystem;
		this.particleEmission = this.particleComponent.emissionRate;
	}

	// Token: 0x06004F86 RID: 20358 RVA: 0x00150974 File Offset: 0x0014EB74
	protected void Update()
	{
		int num = (!this.sky.IsNight) ? -1 : 1;
		this.lerpTime = Mathf.Clamp01(this.lerpTime + (float)num * Time.deltaTime / this.fadeTime);
		this.particleComponent.emissionRate = Mathf.Lerp(0f, this.particleEmission, this.lerpTime);
	}

	// Token: 0x04002E2E RID: 11822
	public global::TOD_Sky sky;

	// Token: 0x04002E2F RID: 11823
	public float fadeTime = 1f;

	// Token: 0x04002E30 RID: 11824
	private float lerpTime;

	// Token: 0x04002E31 RID: 11825
	private ParticleSystem particleComponent;

	// Token: 0x04002E32 RID: 11826
	private float particleEmission;
}
