using System;
using UnityEngine;

// Token: 0x02000484 RID: 1156
public class IgnoreColliders : MonoBehaviour
{
	// Token: 0x06002811 RID: 10257 RVA: 0x000916C0 File Offset: 0x0008F8C0
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

	// Token: 0x0400130F RID: 4879
	public Collider[] a;

	// Token: 0x04001310 RID: 4880
	public Collider[] b;
}
