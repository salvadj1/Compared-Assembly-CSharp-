using System;
using UnityEngine;

// Token: 0x020004FC RID: 1276
public sealed class CameraEventMaskClear : MonoBehaviour
{
	// Token: 0x06002B9C RID: 11164 RVA: 0x000A23B8 File Offset: 0x000A05B8
	private void Awake()
	{
		base.camera.eventMask = 0;
	}
}
