using System;
using UnityEngine;

// Token: 0x02000828 RID: 2088
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Time of Day/Camera Main Script")]
[ExecuteInEditMode]
public class TOD_Camera : MonoBehaviour
{
	// Token: 0x06004A5D RID: 19037 RVA: 0x001425B0 File Offset: 0x001407B0
	protected void OnPreCull()
	{
		if (!this.sky)
		{
			return;
		}
		if (this.DomeScaleToFarClip)
		{
			float num = this.DomeScaleFactor * base.camera.farClipPlane;
			Vector3 localScale;
			localScale..ctor(num, num, num);
			this.sky.transform.localScale = localScale;
		}
		if (this.DomePosToCamera)
		{
			Vector3 position = base.transform.position;
			this.sky.transform.position = position;
		}
	}

	// Token: 0x04002AEF RID: 10991
	public TOD_Sky sky;

	// Token: 0x04002AF0 RID: 10992
	public bool DomePosToCamera = true;

	// Token: 0x04002AF1 RID: 10993
	public bool DomeScaleToFarClip;

	// Token: 0x04002AF2 RID: 10994
	public float DomeScaleFactor = 0.95f;
}
