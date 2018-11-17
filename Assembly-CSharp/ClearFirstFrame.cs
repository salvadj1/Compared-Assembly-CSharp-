using System;
using UnityEngine;

// Token: 0x02000454 RID: 1108
[ExecuteInEditMode]
public class ClearFirstFrame : MonoBehaviour
{
	// Token: 0x0600287F RID: 10367 RVA: 0x0009F38C File Offset: 0x0009D58C
	protected void Update()
	{
		if (base.camera.clearFlags != 3)
		{
			this.Disable();
		}
	}

	// Token: 0x06002880 RID: 10368 RVA: 0x0009F3A8 File Offset: 0x0009D5A8
	protected void OnPreRender()
	{
		GL.Clear(true, true, Color.black);
		this.Disable();
	}

	// Token: 0x06002881 RID: 10369 RVA: 0x0009F3BC File Offset: 0x0009D5BC
	private void Disable()
	{
		base.enabled = false;
	}
}
