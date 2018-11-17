using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000056 RID: 86
public class RadiationZone : MonoBehaviour
{
	// Token: 0x060002D8 RID: 728 RVA: 0x0000EDA4 File Offset: 0x0000CFA4
	private void Start()
	{
		this.UpdateCollider();
	}

	// Token: 0x060002D9 RID: 729 RVA: 0x0000EDAC File Offset: 0x0000CFAC
	public Character GetFromCollider(Collider other)
	{
		IDBase idbase = IDBase.Get(other);
		if (!idbase)
		{
			return null;
		}
		return idbase.idMain as Character;
	}

	// Token: 0x060002DA RID: 730 RVA: 0x0000EDD8 File Offset: 0x0000CFD8
	private void OnTriggerEnter(Collider other)
	{
		Character fromCollider = this.GetFromCollider(other);
		if (!fromCollider)
		{
			return;
		}
		Radiation local = fromCollider.GetLocal<Radiation>();
		if (local)
		{
			local.AddRadiationZone(this);
		}
	}

	// Token: 0x060002DB RID: 731 RVA: 0x0000EE14 File Offset: 0x0000D014
	public float GetExposureForPos(Vector3 pos)
	{
		if (this.strongerAtCenter)
		{
			return this.exposurePerMin * (1f - Mathf.Clamp01(Vector3.Distance(pos, base.transform.position) / this.radius));
		}
		return this.exposurePerMin;
	}

	// Token: 0x060002DC RID: 732 RVA: 0x0000EE60 File Offset: 0x0000D060
	private void OnTriggerExit(Collider other)
	{
		Character fromCollider = this.GetFromCollider(other);
		if (!fromCollider)
		{
			return;
		}
		Radiation local = fromCollider.GetLocal<Radiation>();
		if (!local)
		{
			return;
		}
		local.RemoveRadiationZone(this);
	}

	// Token: 0x060002DD RID: 733 RVA: 0x0000EE9C File Offset: 0x0000D09C
	internal bool CanAddToRadiation(Radiation radiation)
	{
		bool result;
		if (!this.shuttingDown)
		{
			HashSet<Radiation> hashSet;
			if ((hashSet = this.radiating) == null)
			{
				hashSet = (this.radiating = new HashSet<Radiation>());
			}
			result = hashSet.Add(radiation);
		}
		else
		{
			result = false;
		}
		return result;
	}

	// Token: 0x060002DE RID: 734 RVA: 0x0000EED8 File Offset: 0x0000D0D8
	internal bool RemoveFromRadiation(Radiation radiation)
	{
		return this.shuttingDown || (this.radiating != null && this.radiating.Remove(radiation));
	}

	// Token: 0x060002DF RID: 735 RVA: 0x0000EF10 File Offset: 0x0000D110
	[ContextMenu("Update Collider")]
	public void UpdateCollider()
	{
		base.GetComponent<SphereCollider>().radius = this.radius;
		base.collider.isTrigger = true;
	}

	// Token: 0x060002E0 RID: 736 RVA: 0x0000EF3C File Offset: 0x0000D13C
	private void OnDestroy()
	{
		this.shuttingDown = true;
		if (this.radiating != null)
		{
			foreach (Radiation radiation in this.radiating)
			{
				if (radiation)
				{
					radiation.RemoveRadiationZone(this);
				}
			}
			this.radiating = null;
		}
	}

	// Token: 0x060002E1 RID: 737 RVA: 0x0000EFC8 File Offset: 0x0000D1C8
	public void OnDrawGizmos()
	{
		Gizmos.color = new Color(0.3f, 0.5f, 0.3f, 0.25f);
		Gizmos.DrawSphere(base.transform.position, this.radius);
		Gizmos.color = Color.green;
		Gizmos.DrawCube(base.transform.position, Vector3.one * 0.5f);
	}

	// Token: 0x060002E2 RID: 738 RVA: 0x0000F034 File Offset: 0x0000D234
	public void OnDrawGizmosSelected()
	{
		Gizmos.color = new Color(0.3f, 0.5f, 0.3f, 0.4f);
		Gizmos.DrawWireSphere(base.transform.position, this.radius);
		Gizmos.color = Color.green;
		Gizmos.DrawCube(base.transform.position, Vector3.one * 0.5f);
	}

	// Token: 0x040001B7 RID: 439
	public float radius = 10f;

	// Token: 0x040001B8 RID: 440
	public float exposurePerMin = 50f;

	// Token: 0x040001B9 RID: 441
	public bool strongerAtCenter = true;

	// Token: 0x040001BA RID: 442
	[NonSerialized]
	private HashSet<Radiation> radiating;

	// Token: 0x040001BB RID: 443
	[NonSerialized]
	private bool shuttingDown;
}
