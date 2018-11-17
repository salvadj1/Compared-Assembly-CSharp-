using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004F7 RID: 1271
public class WoodBlockerTemp : MonoBehaviour
{
	// Token: 0x06002B22 RID: 11042 RVA: 0x000ACB88 File Offset: 0x000AAD88
	private static void TryInitBlockers()
	{
		if (WoodBlockerTemp._blockers == null)
		{
			WoodBlockerTemp._blockers = new List<WoodBlockerTemp>();
		}
	}

	// Token: 0x06002B23 RID: 11043 RVA: 0x000ACBA0 File Offset: 0x000AADA0
	private void Awake()
	{
		WoodBlockerTemp.TryInitBlockers();
		this.numWood = (float)Random.Range(10, 15);
		WoodBlockerTemp._blockers.Add(this);
		Object.Destroy(base.gameObject, 300f);
	}

	// Token: 0x06002B24 RID: 11044 RVA: 0x000ACBE0 File Offset: 0x000AADE0
	public static WoodBlockerTemp GetBlockerForPoint(Vector3 point)
	{
		WoodBlockerTemp.TryInitBlockers();
		foreach (WoodBlockerTemp woodBlockerTemp in WoodBlockerTemp._blockers)
		{
			float num = Vector3.Distance(woodBlockerTemp.transform.position, point);
			if (num < 4f)
			{
				return woodBlockerTemp;
			}
		}
		GameObject gameObject = GameObject.CreatePrimitive(0);
		WoodBlockerTemp woodBlockerTemp2 = (WoodBlockerTemp)gameObject.AddComponent("WoodBlockerTemp");
		woodBlockerTemp2.renderer.enabled = false;
		woodBlockerTemp2.collider.enabled = false;
		woodBlockerTemp2.transform.position = point;
		woodBlockerTemp2.name = "WBT";
		return woodBlockerTemp2;
	}

	// Token: 0x06002B25 RID: 11045 RVA: 0x000ACCBC File Offset: 0x000AAEBC
	public void OnDestroy()
	{
		WoodBlockerTemp._blockers.Remove(this);
	}

	// Token: 0x06002B26 RID: 11046 RVA: 0x000ACCCC File Offset: 0x000AAECC
	public bool HasWood()
	{
		return this.numWood >= 1f;
	}

	// Token: 0x06002B27 RID: 11047 RVA: 0x000ACCE0 File Offset: 0x000AAEE0
	public float GetWoodLeft()
	{
		return this.numWood;
	}

	// Token: 0x06002B28 RID: 11048 RVA: 0x000ACCE8 File Offset: 0x000AAEE8
	public void ConsumeWood(float consume)
	{
		this.numWood -= consume;
		if (this.numWood < 0f)
		{
			this.numWood = 0f;
		}
	}

	// Token: 0x040017A5 RID: 6053
	public static List<WoodBlockerTemp> _blockers;

	// Token: 0x040017A6 RID: 6054
	public float numWood;
}
