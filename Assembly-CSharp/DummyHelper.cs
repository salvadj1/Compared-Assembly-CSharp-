using System;
using UnityEngine;

// Token: 0x020004D1 RID: 1233
public class DummyHelper : MonoBehaviour
{
	// Token: 0x06002AA2 RID: 10914 RVA: 0x000A9DFC File Offset: 0x000A7FFC
	public void OnDrawGizmos()
	{
		Gizmos.color = new Color(1f, 1f, 0f, 0.5f);
		Gizmos.DrawCube(base.transform.position, Vector3.one * 0.5f);
	}

	// Token: 0x06002AA3 RID: 10915 RVA: 0x000A9E48 File Offset: 0x000A8048
	public void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawCube(base.transform.position, Vector3.one * 0.5f);
	}
}
