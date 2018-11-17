using System;
using UnityEngine;

// Token: 0x02000557 RID: 1367
public class BounceArrow : MonoBehaviour
{
	// Token: 0x06002D7E RID: 11646 RVA: 0x000AB764 File Offset: 0x000A9964
	private void Update()
	{
		float num = 0f + Mathf.Abs(Mathf.Sin(Time.time * 5f)) * 0.15f;
		base.transform.localPosition = new Vector3(0f, num, 0f);
	}
}
