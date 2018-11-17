using System;
using UnityEngine;

// Token: 0x0200058C RID: 1420
public class DummyHelper : MonoBehaviour
{
	// Token: 0x06002E54 RID: 11860 RVA: 0x000B1B94 File Offset: 0x000AFD94
	public void OnDrawGizmos()
	{
		Gizmos.color = new Color(1f, 1f, 0f, 0.5f);
		Gizmos.DrawCube(base.transform.position, Vector3.one * 0.5f);
	}

	// Token: 0x06002E55 RID: 11861 RVA: 0x000B1BE0 File Offset: 0x000AFDE0
	public void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawCube(base.transform.position, Vector3.one * 0.5f);
	}
}
