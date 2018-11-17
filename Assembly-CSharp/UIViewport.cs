using System;
using UnityEngine;

// Token: 0x02000811 RID: 2065
[AddComponentMenu("NGUI/UI/Viewport Camera")]
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class UIViewport : MonoBehaviour
{
	// Token: 0x06004A11 RID: 18961 RVA: 0x0013CA64 File Offset: 0x0013AC64
	private void Start()
	{
		this.mCam = base.camera;
		if (this.sourceCamera == null)
		{
			this.sourceCamera = Camera.main;
		}
	}

	// Token: 0x06004A12 RID: 18962 RVA: 0x0013CA9C File Offset: 0x0013AC9C
	private void LateUpdate()
	{
		if (this.topLeft != null && this.bottomRight != null)
		{
			Vector3 vector = this.sourceCamera.WorldToScreenPoint(this.topLeft.position);
			Vector3 vector2 = this.sourceCamera.WorldToScreenPoint(this.bottomRight.position);
			Rect rect;
			rect..ctor(vector.x / (float)Screen.width, vector2.y / (float)Screen.height, (vector2.x - vector.x) / (float)Screen.width, (vector.y - vector2.y) / (float)Screen.height);
			float num = this.fullSize * rect.height;
			if (rect != this.mCam.rect)
			{
				this.mCam.rect = rect;
			}
			if (this.mCam.orthographicSize != num)
			{
				this.mCam.orthographicSize = num;
			}
		}
	}

	// Token: 0x04002A26 RID: 10790
	public Camera sourceCamera;

	// Token: 0x04002A27 RID: 10791
	public Transform topLeft;

	// Token: 0x04002A28 RID: 10792
	public Transform bottomRight;

	// Token: 0x04002A29 RID: 10793
	public float fullSize = 1f;

	// Token: 0x04002A2A RID: 10794
	private Camera mCam;
}
