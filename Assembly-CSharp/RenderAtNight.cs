using System;
using UnityEngine;

// Token: 0x02000847 RID: 2119
[RequireComponent(typeof(Renderer))]
public class RenderAtNight : MonoBehaviour
{
	// Token: 0x06004AD3 RID: 19155 RVA: 0x00146BA8 File Offset: 0x00144DA8
	protected void OnEnable()
	{
		if (!this.sky)
		{
			Debug.LogError("Sky instance reference not set. Disabling script.");
			base.enabled = false;
		}
		this.rendererComponent = base.renderer;
	}

	// Token: 0x06004AD4 RID: 19156 RVA: 0x00146BD8 File Offset: 0x00144DD8
	protected void Update()
	{
		this.rendererComponent.enabled = this.sky.IsNight;
	}

	// Token: 0x04002BED RID: 11245
	public TOD_Sky sky;

	// Token: 0x04002BEE RID: 11246
	private Renderer rendererComponent;
}
