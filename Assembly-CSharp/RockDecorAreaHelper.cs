using System;
using UnityEngine;

// Token: 0x020003E0 RID: 992
public class RockDecorAreaHelper : MonoBehaviour
{
	// Token: 0x060024DE RID: 9438 RVA: 0x0008D750 File Offset: 0x0008B950
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.grey;
		this.DrawBounds();
	}

	// Token: 0x060024DF RID: 9439 RVA: 0x0008D764 File Offset: 0x0008B964
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.white;
		this.DrawBounds();
	}

	// Token: 0x060024E0 RID: 9440 RVA: 0x0008D778 File Offset: 0x0008B978
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
