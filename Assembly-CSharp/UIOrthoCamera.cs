using System;
using UnityEngine;

// Token: 0x020008E9 RID: 2281
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Orthographic Camera")]
[RequireComponent(typeof(Camera))]
public class UIOrthoCamera : MonoBehaviour
{
	// Token: 0x06004DCE RID: 19918 RVA: 0x00135ED4 File Offset: 0x001340D4
	private void Start()
	{
		this.mCam = base.camera;
		this.mTrans = base.transform;
		this.mCam.orthographic = true;
	}

	// Token: 0x06004DCF RID: 19919 RVA: 0x00135F08 File Offset: 0x00134108
	private void Update()
	{
		float num = this.mCam.rect.yMin * (float)Screen.height;
		float num2 = this.mCam.rect.yMax * (float)Screen.height;
		float num3 = (num2 - num) * 0.5f * this.mTrans.lossyScale.y;
		if (!Mathf.Approximately(this.mCam.orthographicSize, num3))
		{
			this.mCam.orthographicSize = num3;
		}
	}

	// Token: 0x04002B9C RID: 11164
	private Camera mCam;

	// Token: 0x04002B9D RID: 11165
	private Transform mTrans;
}
