using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000068 RID: 104
public class RadiationZone : MonoBehaviour
{
	// Token: 0x0600034A RID: 842 RVA: 0x0001034C File Offset: 0x0000E54C
	private void Start()
	{
		this.UpdateCollider();
	}

	// Token: 0x0600034B RID: 843 RVA: 0x00010354 File Offset: 0x0000E554
	public global::Character GetFromCollider(Collider other)
	{
		IDBase idbase = IDBase.Get(other);
		if (!idbase)
		{
			return null;
		}
		return idbase.idMain as global::Character;
	}

	// Token: 0x0600034C RID: 844 RVA: 0x00010380 File Offset: 0x0000E580
	private void OnTriggerEnter(Collider other)
	{
		global::Character fromCollider = this.GetFromCollider(other);
		if (!fromCollider)
		{
			return;
		}
		global::Radiation local = fromCollider.GetLocal<global::Radiation>();
		if (local)
		{
			local.AddRadiationZone(this);
		}
	}

	// Token: 0x0600034D RID: 845 RVA: 0x000103BC File Offset: 0x0000E5BC
	public float GetExposureForPos(Vector3 pos)
	{
		if (this.strongerAtCenter)
		{
			return this.exposurePerMin * (1f - Mathf.Clamp01(Vector3.Distance(pos, base.transform.position) / this.radius));
		}
		return this.exposurePerMin;
	}

	// Token: 0x0600034E RID: 846 RVA: 0x00010408 File Offset: 0x0000E608
	private void OnTriggerExit(Collider other)
	{
		global::Character fromCollider = this.GetFromCollider(other);
		if (!fromCollider)
		{
			return;
		}
		global::Radiation local = fromCollider.GetLocal<global::Radiation>();
		if (!local)
		{
			return;
		}
		local.RemoveRadiationZone(this);
	}

	// Token: 0x0600034F RID: 847 RVA: 0x00010444 File Offset: 0x0000E644
	internal bool CanAddToRadiation(global::Radiation radiation)
	{
		bool result;
		if (!this.shuttingDown)
		{
			HashSet<global::Radiation> hashSet;
			if ((hashSet = this.radiating) == null)
			{
				hashSet = (this.radiating = new HashSet<global::Radiation>());
			}
			result = hashSet.Add(radiation);
		}
		else
		{
			result = false;
		}
		return result;
	}

	// Token: 0x06000350 RID: 848 RVA: 0x00010480 File Offset: 0x0000E680
	internal bool RemoveFromRadiation(global::Radiation radiation)
	{
		return this.shuttingDown || (this.radiating != null && this.radiating.Remove(radiation));
	}

	// Token: 0x06000351 RID: 849 RVA: 0x000104B8 File Offset: 0x0000E6B8
	[ContextMenu("Update Collider")]
	public void UpdateCollider()
	{
		base.GetComponent<SphereCollider>().radius = this.radius;
		base.collider.isTrigger = true;
	}

	// Token: 0x06000352 RID: 850 RVA: 0x000104E4 File Offset: 0x0000E6E4
	private void OnDestroy()
	{
		this.shuttingDown = true;
		if (this.radiating != null)
		{
			foreach (global::Radiation radiation in this.radiating)
			{
				if (radiation)
				{
					radiation.RemoveRadiationZone(this);
				}
			}
			this.radiating = null;
		}
	}

	// Token: 0x06000353 RID: 851 RVA: 0x00010570 File Offset: 0x0000E770
	public void OnDrawGizmos()
	{
		Gizmos.color = new Color(0.3f, 0.5f, 0.3f, 0.25f);
		Gizmos.DrawSphere(base.transform.position, this.radius);
		Gizmos.color = Color.green;
		Gizmos.DrawCube(base.transform.position, Vector3.one * 0.5f);
	}

	// Token: 0x06000354 RID: 852 RVA: 0x000105DC File Offset: 0x0000E7DC
	public void OnDrawGizmosSelected()
	{
		Gizmos.color = new Color(0.3f, 0.5f, 0.3f, 0.4f);
		Gizmos.DrawWireSphere(base.transform.position, this.radius);
		Gizmos.color = Color.green;
		Gizmos.DrawCube(base.transform.position, Vector3.one * 0.5f);
	}

	// Token: 0x04000219 RID: 537
	public float radius = 10f;

	// Token: 0x0400021A RID: 538
	public float exposurePerMin = 50f;

	// Token: 0x0400021B RID: 539
	public bool strongerAtCenter = true;

	// Token: 0x0400021C RID: 540
	[NonSerialized]
	private HashSet<global::Radiation> radiating;

	// Token: 0x0400021D RID: 541
	[NonSerialized]
	private bool shuttingDown;
}
