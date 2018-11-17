using System;
using UnityEngine;

// Token: 0x0200050A RID: 1290
[ExecuteInEditMode]
public class ClearFirstFrame : MonoBehaviour
{
	// Token: 0x06002C0F RID: 11279 RVA: 0x000A530C File Offset: 0x000A350C
	protected void Update()
	{
		if (base.camera.clearFlags != 3)
		{
			this.Disable();
		}
	}

	// Token: 0x06002C10 RID: 11280 RVA: 0x000A5328 File Offset: 0x000A3528
	protected void OnPreRender()
	{
		GL.Clear(true, true, Color.black);
		this.Disable();
	}

	// Token: 0x06002C11 RID: 11281 RVA: 0x000A533C File Offset: 0x000A353C
	private void Disable()
	{
		base.enabled = false;
	}
}
