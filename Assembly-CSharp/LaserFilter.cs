using System;
using UnityEngine;

// Token: 0x02000605 RID: 1541
public sealed class LaserFilter : MonoBehaviour
{
	// Token: 0x17000AF1 RID: 2801
	// (get) Token: 0x060036F5 RID: 14069 RVA: 0x000C5CF8 File Offset: 0x000C3EF8
	public Camera camera
	{
		get
		{
			if (!this._gotCam)
			{
				this._gotCam = true;
				this._camera = base.camera;
			}
			return this._camera;
		}
	}

	// Token: 0x060036F6 RID: 14070 RVA: 0x000C5D2C File Offset: 0x000C3F2C
	private void OnPreCull()
	{
		if (base.enabled)
		{
			LaserGraphics.RenderLasersOnCamera(this.camera);
		}
	}

	// Token: 0x04001B2B RID: 6955
	[NonSerialized]
	private bool _gotCam;

	// Token: 0x04001B2C RID: 6956
	[NonSerialized]
	private Camera _camera;
}
