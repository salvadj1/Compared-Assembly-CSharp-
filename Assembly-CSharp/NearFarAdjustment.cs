using System;
using UnityEngine;

// Token: 0x020006E2 RID: 1762
public class NearFarAdjustment : MonoBehaviour
{
	// Token: 0x06003B87 RID: 15239 RVA: 0x000D4CBC File Offset: 0x000D2EBC
	private void Update()
	{
		bool flag = Physics.Raycast(new Ray(base.transform.position, base.transform.forward), 1.2f);
		if (flag)
		{
			base.camera.nearClipPlane = 0.21f;
		}
		else
		{
			base.camera.nearClipPlane = 0.8f;
		}
	}
}
