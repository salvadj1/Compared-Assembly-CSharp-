using System;
using UnityEngine;

// Token: 0x020003D7 RID: 983
public class IgnoreColliders : MonoBehaviour
{
	// Token: 0x060024AF RID: 9391 RVA: 0x0008C2C4 File Offset: 0x0008A4C4
	private void Awake()
	{
		if (this.a != null && this.b != null)
		{
			int num = Mathf.Min(this.a.Length, this.b.Length);
			for (int i = 0; i < num; i++)
			{
				if (this.a[i] && this.b[i] && this.b[i] != this.a[i])
				{
					Physics.IgnoreCollision(this.a[i], this.b[i]);
				}
			}
			this.a = null;
			this.b = null;
		}
	}

	// Token: 0x040011A9 RID: 4521
	public Collider[] a;

	// Token: 0x040011AA RID: 4522
	public Collider[] b;
}
