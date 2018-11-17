using System;
using UnityEngine;

// Token: 0x0200066D RID: 1645
public class TimedExplosive : IDLocal
{
	// Token: 0x0600391D RID: 14621 RVA: 0x000D2170 File Offset: 0x000D0370
	private void Awake()
	{
		this.testView = base.GetComponent<NGCView>();
		if (this.tickSound != null)
		{
			base.InvokeRepeating("TickSound", 0f, 1f);
		}
	}

	// Token: 0x0600391E RID: 14622 RVA: 0x000D21B0 File Offset: 0x000D03B0
	[RPC]
	public void ClientExplode()
	{
		Object.Instantiate(this.explosionEffect, base.transform.position, base.transform.rotation);
		base.CancelInvoke();
	}

	// Token: 0x0600391F RID: 14623 RVA: 0x000D21E8 File Offset: 0x000D03E8
	public void TickSound()
	{
		this.tickSound.Play(base.transform.position, 1f, 3f, 20f);
	}

	// Token: 0x06003920 RID: 14624 RVA: 0x000D221C File Offset: 0x000D041C
	public void OnDestroy()
	{
		base.CancelInvoke();
	}

	// Token: 0x04001D42 RID: 7490
	public float fuseLength = 5f;

	// Token: 0x04001D43 RID: 7491
	public float explosionRadius = 4f;

	// Token: 0x04001D44 RID: 7492
	public float damage = 100f;

	// Token: 0x04001D45 RID: 7493
	public GameObject explosionEffect;

	// Token: 0x04001D46 RID: 7494
	public AudioClip tickSound;

	// Token: 0x04001D47 RID: 7495
	private NGCView testView;
}
