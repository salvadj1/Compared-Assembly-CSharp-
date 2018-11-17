using System;
using UnityEngine;

// Token: 0x02000938 RID: 2360
[RequireComponent(typeof(ParticleSystem))]
public class ParticleAtDay : MonoBehaviour
{
	// Token: 0x06004F82 RID: 20354 RVA: 0x00150860 File Offset: 0x0014EA60
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

	// Token: 0x06004F83 RID: 20355 RVA: 0x001508AC File Offset: 0x0014EAAC
	protected void Update()
	{
		int num = (!this.sky.IsDay) ? -1 : 1;
		this.lerpTime = Mathf.Clamp01(this.lerpTime + (float)num * Time.deltaTime / this.fadeTime);
		this.particleComponent.emissionRate = Mathf.Lerp(0f, this.particleEmission, this.lerpTime);
	}

	// Token: 0x04002E29 RID: 11817
	public global::TOD_Sky sky;

	// Token: 0x04002E2A RID: 11818
	public float fadeTime = 1f;

	// Token: 0x04002E2B RID: 11819
	private float lerpTime;

	// Token: 0x04002E2C RID: 11820
	private ParticleSystem particleComponent;

	// Token: 0x04002E2D RID: 11821
	private float particleEmission;
}
