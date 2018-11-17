using System;
using UnityEngine;

// Token: 0x02000621 RID: 1569
public class NearFarAdjustment : MonoBehaviour
{
	// Token: 0x060037A7 RID: 14247 RVA: 0x000CC5E4 File Offset: 0x000CA7E4
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
