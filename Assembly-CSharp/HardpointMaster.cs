using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000704 RID: 1796
public class HardpointMaster : IDLocal
{
	// Token: 0x06003BE0 RID: 15328 RVA: 0x000D5BB8 File Offset: 0x000D3DB8
	public void Awake()
	{
		this._points = new List<global::Hardpoint>();
	}

	// Token: 0x06003BE1 RID: 15329 RVA: 0x000D5BC8 File Offset: 0x000D3DC8
	public void AddHardpoint(global::Hardpoint point)
	{
		this._points.Add(point);
	}

	// Token: 0x06003BE2 RID: 15330 RVA: 0x000D5BD8 File Offset: 0x000D3DD8
	public global::Hardpoint GetHardpointNear(Vector3 worldPos, float maxRange, global::Hardpoint.hardpoint_type type)
	{
		foreach (global::Hardpoint hardpoint in this._points)
		{
			if (hardpoint.type == type)
			{
				if (hardpoint.IsFree())
				{
					if (Vector3.Distance(hardpoint.transform.position, worldPos) <= maxRange)
					{
						return hardpoint;
					}
				}
			}
		}
		return null;
	}

	// Token: 0x06003BE3 RID: 15331 RVA: 0x000D5C7C File Offset: 0x000D3E7C
	public global::Hardpoint GetHardpointNear(Vector3 worldPos, global::Hardpoint.hardpoint_type type)
	{
		return this.GetHardpointNear(worldPos, 3f, type);
	}

	// Token: 0x06003BE4 RID: 15332 RVA: 0x000D5C8C File Offset: 0x000D3E8C
	public global::TransCarrier GetTransCarrier()
	{
		return this.idMain.GetLocal<global::TransCarrier>();
	}

	// Token: 0x04001E0E RID: 7694
	public List<global::Hardpoint> _points;
}
