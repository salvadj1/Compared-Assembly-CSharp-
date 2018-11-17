using System;
using UnityEngine;

// Token: 0x020007F7 RID: 2039
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("NGUI/UI/Orthographic Camera")]
public class UIOrthoCamera : MonoBehaviour
{
	// Token: 0x0600491F RID: 18719 RVA: 0x0012BF70 File Offset: 0x0012A170
	private void Start()
	{
		this.mCam = base.camera;
		this.mTrans = base.transform;
		this.mCam.orthographic = true;
	}

	// Token: 0x06004920 RID: 18720 RVA: 0x0012BFA4 File Offset: 0x0012A1A4
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

	// Token: 0x0400294E RID: 10574
	private Camera mCam;

	// Token: 0x0400294F RID: 10575
	private Transform mTrans;
}
