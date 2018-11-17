using System;
using UnityEngine;

// Token: 0x0200093C RID: 2364
[RequireComponent(typeof(Renderer))]
public class RenderAtNight : MonoBehaviour
{
	// Token: 0x06004F8E RID: 20366 RVA: 0x00150B0C File Offset: 0x0014ED0C
	protected void OnEnable()
	{
		if (!this.sky)
		{
			Debug.LogError("Sky instance reference not set. Disabling script.");
			base.enabled = false;
		}
		this.rendererComponent = base.renderer;
	}

	// Token: 0x06004F8F RID: 20367 RVA: 0x00150B3C File Offset: 0x0014ED3C
	protected void Update()
	{
		this.rendererComponent.enabled = this.sky.IsNight;
	}

	// Token: 0x04002E3B RID: 11835
	public global::TOD_Sky sky;

	// Token: 0x04002E3C RID: 11836
	private Renderer rendererComponent;
}
