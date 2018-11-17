using System;
using UnityEngine;

// Token: 0x02000446 RID: 1094
public sealed class CameraEventMaskClear : MonoBehaviour
{
	// Token: 0x0600280C RID: 10252 RVA: 0x0009C438 File Offset: 0x0009A638
	private void Awake()
	{
		base.camera.eventMask = 0;
	}
}
