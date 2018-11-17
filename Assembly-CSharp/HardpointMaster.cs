using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000641 RID: 1601
public class HardpointMaster : IDLocal
{
	// Token: 0x060037F4 RID: 14324 RVA: 0x000CD308 File Offset: 0x000CB508
	public void Awake()
	{
		this._points = new List<Hardpoint>();
	}

	// Token: 0x060037F5 RID: 14325 RVA: 0x000CD318 File Offset: 0x000CB518
	public void AddHardpoint(Hardpoint point)
	{
		this._points.Add(point);
	}

	// Token: 0x060037F6 RID: 14326 RVA: 0x000CD328 File Offset: 0x000CB528
	public Hardpoint GetHardpointNear(Vector3 worldPos, float maxRange, Hardpoint.hardpoint_type type)
	{
		foreach (Hardpoint hardpoint in this._points)
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

	// Token: 0x060037F7 RID: 14327 RVA: 0x000CD3CC File Offset: 0x000CB5CC
	public Hardpoint GetHardpointNear(Vector3 worldPos, Hardpoint.hardpoint_type type)
	{
		return this.GetHardpointNear(worldPos, 3f, type);
	}

	// Token: 0x060037F8 RID: 14328 RVA: 0x000CD3DC File Offset: 0x000CB5DC
	public TransCarrier GetTransCarrier()
	{
		return this.idMain.GetLocal<TransCarrier>();
	}

	// Token: 0x04001C19 RID: 7193
	public List<Hardpoint> _points;
}
