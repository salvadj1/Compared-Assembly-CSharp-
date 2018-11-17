using System;
using UnityEngine;

// Token: 0x02000903 RID: 2307
[AddComponentMenu("NGUI/UI/Viewport Camera")]
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class UIViewport : MonoBehaviour
{
	// Token: 0x06004EC0 RID: 20160 RVA: 0x001469C8 File Offset: 0x00144BC8
	private void Start()
	{
		this.mCam = base.camera;
		if (this.sourceCamera == null)
		{
			this.sourceCamera = Camera.main;
		}
	}

	// Token: 0x06004EC1 RID: 20161 RVA: 0x00146A00 File Offset: 0x00144C00
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

	// Token: 0x04002C74 RID: 11380
	public Camera sourceCamera;

	// Token: 0x04002C75 RID: 11381
	public Transform topLeft;

	// Token: 0x04002C76 RID: 11382
	public Transform bottomRight;

	// Token: 0x04002C77 RID: 11383
	public float fullSize = 1f;

	// Token: 0x04002C78 RID: 11384
	private Camera mCam;
}
