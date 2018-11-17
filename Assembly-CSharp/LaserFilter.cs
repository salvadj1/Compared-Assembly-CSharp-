using System;
using UnityEngine;

// Token: 0x020006C5 RID: 1733
public sealed class LaserFilter : MonoBehaviour
{
	// Token: 0x17000B6B RID: 2923
	// (get) Token: 0x06003ACD RID: 15053 RVA: 0x000CE228 File Offset: 0x000CC428
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

	// Token: 0x06003ACE RID: 15054 RVA: 0x000CE25C File Offset: 0x000CC45C
	private void OnPreCull()
	{
		if (base.enabled)
		{
			global::LaserGraphics.RenderLasersOnCamera(this.camera);
		}
	}

	// Token: 0x04001D11 RID: 7441
	[NonSerialized]
	private bool _gotCam;

	// Token: 0x04001D12 RID: 7442
	[NonSerialized]
	private Camera _camera;
}
