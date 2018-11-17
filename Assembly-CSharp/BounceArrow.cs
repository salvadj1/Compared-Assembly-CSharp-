using System;
using UnityEngine;

// Token: 0x0200049C RID: 1180
public class BounceArrow : MonoBehaviour
{
	// Token: 0x060029CC RID: 10700 RVA: 0x000A39CC File Offset: 0x000A1BCC
	private void Update()
	{
		float num = 0f + Mathf.Abs(Mathf.Sin(Time.time * 5f)) * 0.15f;
		base.transform.localPosition = new Vector3(0f, num, 0f);
	}
}
