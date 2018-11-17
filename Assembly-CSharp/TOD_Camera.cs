using System;
using UnityEngine;

// Token: 0x0200091D RID: 2333
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Time of Day/Camera Main Script")]
[ExecuteInEditMode]
public class TOD_Camera : MonoBehaviour
{
	// Token: 0x06004F18 RID: 20248 RVA: 0x0014C514 File Offset: 0x0014A714
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

	// Token: 0x04002D3D RID: 11581
	public global::TOD_Sky sky;

	// Token: 0x04002D3E RID: 11582
	public bool DomePosToCamera = true;

	// Token: 0x04002D3F RID: 11583
	public bool DomeScaleToFarClip;

	// Token: 0x04002D40 RID: 11584
	public float DomeScaleFactor = 0.95f;
}
