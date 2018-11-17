using System;
using UnityEngine;

// Token: 0x02000846 RID: 2118
[RequireComponent(typeof(Renderer))]
public class RenderAtDay : MonoBehaviour
{
	// Token: 0x06004AD0 RID: 19152 RVA: 0x00146B58 File Offset: 0x00144D58
	protected void OnEnable()
	{
		if (!this.sky)
		{
			Debug.LogError("Sky instance reference not set. Disabling script.");
			base.enabled = false;
		}
		this.rendererComponent = base.renderer;
	}

	// Token: 0x06004AD1 RID: 19153 RVA: 0x00146B88 File Offset: 0x00144D88
	protected void Update()
	{
		this.rendererComponent.enabled = this.sky.IsDay;
	}

	// Token: 0x04002BEB RID: 11243
	public TOD_Sky sky;

	// Token: 0x04002BEC RID: 11244
	private Renderer rendererComponent;
}
