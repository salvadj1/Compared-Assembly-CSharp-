using System;
using UnityEngine;

// Token: 0x0200048E RID: 1166
public class RockDecorAreaHelper : MonoBehaviour
{
	// Token: 0x06002842 RID: 10306 RVA: 0x00092B3C File Offset: 0x00090D3C
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.grey;
		this.DrawBounds();
	}

	// Token: 0x06002843 RID: 10307 RVA: 0x00092B50 File Offset: 0x00090D50
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.white;
		this.DrawBounds();
	}

	// Token: 0x06002844 RID: 10308 RVA: 0x00092B64 File Offset: 0x00090D64
	private void DrawBounds()
	{
		Color color = Gizmos.color;
		color.a = 0.25f;
		Gizmos.color = color;
		Gizmos.DrawCube(base.transform.position, base.transform.localScale);
		Gizmos.color = Color.white;
		Gizmos.DrawWireCube(base.transform.position, base.transform.localScale);
	}
}
