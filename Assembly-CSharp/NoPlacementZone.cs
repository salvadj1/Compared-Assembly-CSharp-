using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000066 RID: 102
public class NoPlacementZone : MonoBehaviour
{
	// Token: 0x0600033B RID: 827 RVA: 0x0000FF38 File Offset: 0x0000E138
	public static void AddZone(global::NoPlacementZone zone)
	{
		if (global::NoPlacementZone._zones == null)
		{
			global::NoPlacementZone._zones = new List<global::NoPlacementZone>();
		}
		if (global::NoPlacementZone._zones.Contains(zone))
		{
			return;
		}
		global::NoPlacementZone._zones.Add(zone);
	}

	// Token: 0x0600033C RID: 828 RVA: 0x0000FF78 File Offset: 0x0000E178
	public static void RemoveZone(global::NoPlacementZone zone)
	{
		if (global::NoPlacementZone._zones.Contains(zone))
		{
			global::NoPlacementZone._zones.Remove(zone);
		}
	}

	// Token: 0x0600033D RID: 829 RVA: 0x0000FF98 File Offset: 0x0000E198
	public void Awake()
	{
		global::NoPlacementZone.AddZone(this);
	}

	// Token: 0x0600033E RID: 830 RVA: 0x0000FFA0 File Offset: 0x0000E1A0
	public void OnDestroy()
	{
		global::NoPlacementZone.RemoveZone(this);
	}

	// Token: 0x0600033F RID: 831 RVA: 0x0000FFA8 File Offset: 0x0000E1A8
	public void OnDrawGizmos()
	{
		Gizmos.color = new Color(1f, 0.5f, 0.3f, 0.1f);
		Gizmos.DrawSphere(base.transform.position, this.GetRadius());
		Gizmos.color = Color.green;
		Gizmos.DrawCube(base.transform.position, Vector3.one * 0.5f);
	}

	// Token: 0x06000340 RID: 832 RVA: 0x00010014 File Offset: 0x0000E214
	public void OnDrawGizmosSelected()
	{
		Gizmos.color = new Color(1f, 0.5f, 0.3f, 0.8f);
		Gizmos.DrawWireSphere(base.transform.position, this.GetRadius());
		Gizmos.color = Color.green;
		Gizmos.DrawCube(base.transform.position, Vector3.one * 0.5f);
	}

	// Token: 0x06000341 RID: 833 RVA: 0x00010080 File Offset: 0x0000E280
	public float GetRadius()
	{
		return base.transform.localScale.x;
	}

	// Token: 0x06000342 RID: 834 RVA: 0x000100A0 File Offset: 0x0000E2A0
	public static bool ValidPos(Vector3 pos)
	{
		foreach (global::NoPlacementZone noPlacementZone in global::NoPlacementZone._zones)
		{
			float num = Vector3.Distance(pos, noPlacementZone.transform.position);
			if (num <= noPlacementZone.GetRadius())
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x04000217 RID: 535
	public static List<global::NoPlacementZone> _zones;
}
