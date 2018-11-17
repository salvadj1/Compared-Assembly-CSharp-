using System;
using UnityEngine;

// Token: 0x02000731 RID: 1841
public class TimedExplosive : IDLocal
{
	// Token: 0x06003D11 RID: 15633 RVA: 0x000DAB50 File Offset: 0x000D8D50
	private void Awake()
	{
		this.testView = base.GetComponent<global::NGCView>();
		if (this.tickSound != null)
		{
			base.InvokeRepeating("TickSound", 0f, 1f);
		}
	}

	// Token: 0x06003D12 RID: 15634 RVA: 0x000DAB90 File Offset: 0x000D8D90
	[RPC]
	public void ClientExplode()
	{
		Object.Instantiate(this.explosionEffect, base.transform.position, base.transform.rotation);
		base.CancelInvoke();
	}

	// Token: 0x06003D13 RID: 15635 RVA: 0x000DABC8 File Offset: 0x000D8DC8
	public void TickSound()
	{
		this.tickSound.Play(base.transform.position, 1f, 3f, 20f);
	}

	// Token: 0x06003D14 RID: 15636 RVA: 0x000DABFC File Offset: 0x000D8DFC
	public void OnDestroy()
	{
		base.CancelInvoke();
	}

	// Token: 0x04001F3A RID: 7994
	public float fuseLength = 5f;

	// Token: 0x04001F3B RID: 7995
	public float explosionRadius = 4f;

	// Token: 0x04001F3C RID: 7996
	public float damage = 100f;

	// Token: 0x04001F3D RID: 7997
	public GameObject explosionEffect;

	// Token: 0x04001F3E RID: 7998
	public AudioClip tickSound;

	// Token: 0x04001F3F RID: 7999
	private global::NGCView testView;
}
