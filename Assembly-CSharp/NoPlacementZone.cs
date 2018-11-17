using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000054 RID: 84
public class NoPlacementZone : MonoBehaviour
{
	// Token: 0x060002C9 RID: 713 RVA: 0x0000E990 File Offset: 0x0000CB90
	public static void AddZone(NoPlacementZone zone)
	{
		if (NoPlacementZone._zones == null)
		{
			NoPlacementZone._zones = new List<NoPlacementZone>();
		}
		if (NoPlacementZone._zones.Contains(zone))
		{
			return;
		}
		NoPlacementZone._zones.Add(zone);
	}

	// Token: 0x060002CA RID: 714 RVA: 0x0000E9D0 File Offset: 0x0000CBD0
	public static void RemoveZone(NoPlacementZone zone)
	{
		if (NoPlacementZone._zones.Contains(zone))
		{
			NoPlacementZone._zones.Remove(zone);
		}
	}

	// Token: 0x060002CB RID: 715 RVA: 0x0000E9F0 File Offset: 0x0000CBF0
	public void Awake()
	{
		NoPlacementZone.AddZone(this);
	}

	// Token: 0x060002CC RID: 716 RVA: 0x0000E9F8 File Offset: 0x0000CBF8
	public void OnDestroy()
	{
		NoPlacementZone.RemoveZone(this);
	}

	// Token: 0x060002CD RID: 717 RVA: 0x0000EA00 File Offset: 0x0000CC00
	public void OnDrawGizmos()
	{
		Gizmos.color = new Color(1f, 0.5f, 0.3f, 0.1f);
		Gizmos.DrawSphere(base.transform.position, this.GetRadius());
		Gizmos.color = Color.green;
		Gizmos.DrawCube(base.transform.position, Vector3.one * 0.5f);
	}

	// Token: 0x060002CE RID: 718 RVA: 0x0000EA6C File Offset: 0x0000CC6C
	public void OnDrawGizmosSelected()
	{
		Gizmos.color = new Color(1f, 0.5f, 0.3f, 0.8f);
		Gizmos.DrawWireSphere(base.transform.position, this.GetRadius());
		Gizmos.color = Color.green;
		Gizmos.DrawCube(base.transform.position, Vector3.one * 0.5f);
	}

	// Token: 0x060002CF RID: 719 RVA: 0x0000EAD8 File Offset: 0x0000CCD8
	public float GetRadius()
	{
		return base.transform.localScale.x;
	}

	// Token: 0x060002D0 RID: 720 RVA: 0x0000EAF8 File Offset: 0x0000CCF8
	public static bool ValidPos(Vector3 pos)
	{
		foreach (NoPlacementZone noPlacementZone in NoPlacementZone._zones)
		{
			float num = Vector3.Distance(pos, noPlacementZone.transform.position);
			if (num <= noPlacementZone.GetRadius())
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x040001B5 RID: 437
	public static List<NoPlacementZone> _zones;
}
