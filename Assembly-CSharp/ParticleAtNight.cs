using System;
using UnityEngine;

// Token: 0x02000844 RID: 2116
[RequireComponent(typeof(ParticleSystem))]
public class ParticleAtNight : MonoBehaviour
{
	// Token: 0x06004ACA RID: 19146 RVA: 0x001469C4 File Offset: 0x00144BC4
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

	// Token: 0x06004ACB RID: 19147 RVA: 0x00146A10 File Offset: 0x00144C10
	protected void Update()
	{
		int num = (!this.sky.IsNight) ? -1 : 1;
		this.lerpTime = Mathf.Clamp01(this.lerpTime + (float)num * Time.deltaTime / this.fadeTime);
		this.particleComponent.emissionRate = Mathf.Lerp(0f, this.particleEmission, this.lerpTime);
	}

	// Token: 0x04002BE0 RID: 11232
	public TOD_Sky sky;

	// Token: 0x04002BE1 RID: 11233
	public float fadeTime = 1f;

	// Token: 0x04002BE2 RID: 11234
	private float lerpTime;

	// Token: 0x04002BE3 RID: 11235
	private ParticleSystem particleComponent;

	// Token: 0x04002BE4 RID: 11236
	private float particleEmission;
}
