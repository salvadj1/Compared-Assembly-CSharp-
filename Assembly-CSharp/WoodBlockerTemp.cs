using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005B4 RID: 1460
public class WoodBlockerTemp : MonoBehaviour
{
	// Token: 0x06002EE2 RID: 12002 RVA: 0x000B4C24 File Offset: 0x000B2E24
	private static void TryInitBlockers()
	{
		if (global::WoodBlockerTemp._blockers == null)
		{
			global::WoodBlockerTemp._blockers = new List<global::WoodBlockerTemp>();
		}
	}

	// Token: 0x06002EE3 RID: 12003 RVA: 0x000B4C3C File Offset: 0x000B2E3C
	private void Awake()
	{
		global::WoodBlockerTemp.TryInitBlockers();
		this.numWood = (float)Random.Range(10, 15);
		global::WoodBlockerTemp._blockers.Add(this);
		Object.Destroy(base.gameObject, 300f);
	}

	// Token: 0x06002EE4 RID: 12004 RVA: 0x000B4C7C File Offset: 0x000B2E7C
	public static global::WoodBlockerTemp GetBlockerForPoint(Vector3 point)
	{
		global::WoodBlockerTemp.TryInitBlockers();
		foreach (global::WoodBlockerTemp woodBlockerTemp in global::WoodBlockerTemp._blockers)
		{
			float num = Vector3.Distance(woodBlockerTemp.transform.position, point);
			if (num < 4f)
			{
				return woodBlockerTemp;
			}
		}
		GameObject gameObject = GameObject.CreatePrimitive(0);
		global::WoodBlockerTemp woodBlockerTemp2 = (global::WoodBlockerTemp)gameObject.AddComponent("WoodBlockerTemp");
		woodBlockerTemp2.renderer.enabled = false;
		woodBlockerTemp2.collider.enabled = false;
		woodBlockerTemp2.transform.position = point;
		woodBlockerTemp2.name = "WBT";
		return woodBlockerTemp2;
	}

	// Token: 0x06002EE5 RID: 12005 RVA: 0x000B4D58 File Offset: 0x000B2F58
	public void OnDestroy()
	{
		global::WoodBlockerTemp._blockers.Remove(this);
	}

	// Token: 0x06002EE6 RID: 12006 RVA: 0x000B4D68 File Offset: 0x000B2F68
	public bool HasWood()
	{
		return this.numWood >= 1f;
	}

	// Token: 0x06002EE7 RID: 12007 RVA: 0x000B4D7C File Offset: 0x000B2F7C
	public float GetWoodLeft()
	{
		return this.numWood;
	}

	// Token: 0x06002EE8 RID: 12008 RVA: 0x000B4D84 File Offset: 0x000B2F84
	public void ConsumeWood(float consume)
	{
		this.numWood -= consume;
		if (this.numWood < 0f)
		{
			this.numWood = 0f;
		}
	}

	// Token: 0x04001971 RID: 6513
	public static List<global::WoodBlockerTemp> _blockers;

	// Token: 0x04001972 RID: 6514
	public float numWood;
}
