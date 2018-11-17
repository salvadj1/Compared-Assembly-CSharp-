using System;
using UnityEngine;

// Token: 0x0200093B RID: 2363
[RequireComponent(typeof(Renderer))]
public class RenderAtDay : MonoBehaviour
{
	// Token: 0x06004F8B RID: 20363 RVA: 0x00150ABC File Offset: 0x0014ECBC
	protected void OnEnable()
	{
		if (!this.sky)
		{
			Debug.LogError("Sky instance reference not set. Disabling script.");
			base.enabled = false;
		}
		this.rendererComponent = base.renderer;
	}

	// Token: 0x06004F8C RID: 20364 RVA: 0x00150AEC File Offset: 0x0014ECEC
	protected void Update()
	{
		this.rendererComponent.enabled = this.sky.IsDay;
	}

	// Token: 0x04002E39 RID: 11833
	public global::TOD_Sky sky;

	// Token: 0x04002E3A RID: 11834
	private Renderer rendererComponent;
}
