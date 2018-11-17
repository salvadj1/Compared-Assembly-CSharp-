using System;
using Facepunch;
using UnityEngine;

// Token: 0x02000097 RID: 151
public class SupplyParachute : MonoBehaviour
{
	// Token: 0x0600032D RID: 813 RVA: 0x0000FDF0 File Offset: 0x0000DFF0
	private void Start()
	{
		this.targetScale = base.transform.localScale;
		base.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
	}

	// Token: 0x0600032E RID: 814 RVA: 0x0000FE30 File Offset: 0x0000E030
	private void Update()
	{
		base.transform.localScale = Vector3.MoveTowards(base.transform.localScale, this.targetScale, Time.deltaTime * 2f);
		if (base.transform.localScale == this.targetScale)
		{
			base.enabled = false;
		}
	}

	// Token: 0x0600032F RID: 815 RVA: 0x0000FE8C File Offset: 0x0000E08C
	public void Landed()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x040002B2 RID: 690
	[NonSerialized]
	private Vector3 targetScale;
}
