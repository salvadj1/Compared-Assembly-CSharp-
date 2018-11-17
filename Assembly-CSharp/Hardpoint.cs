using System;
using Facepunch.MeshBatch;
using UnityEngine;

// Token: 0x0200063F RID: 1599
public class Hardpoint : IDRemote
{
	// Token: 0x060037EB RID: 14315 RVA: 0x000CD1B0 File Offset: 0x000CB3B0
	public void Awake()
	{
		HardpointMaster component = base.idMain.GetComponent<HardpointMaster>();
		if (component)
		{
			this.SetMaster(component);
		}
		base.Awake();
	}

	// Token: 0x060037EC RID: 14316 RVA: 0x000CD1E4 File Offset: 0x000CB3E4
	public void OnDestroy()
	{
		base.OnDestroy();
	}

	// Token: 0x060037ED RID: 14317 RVA: 0x000CD1EC File Offset: 0x000CB3EC
	public void SetMaster(HardpointMaster master)
	{
		this._master = master;
		master.AddHardpoint(this);
	}

	// Token: 0x060037EE RID: 14318 RVA: 0x000CD1FC File Offset: 0x000CB3FC
	public HardpointMaster GetMaster()
	{
		return this._master;
	}

	// Token: 0x060037EF RID: 14319 RVA: 0x000CD204 File Offset: 0x000CB404
	public bool IsFree()
	{
		return this.holding == null;
	}

	// Token: 0x060037F0 RID: 14320 RVA: 0x000CD214 File Offset: 0x000CB414
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawCube(base.transform.position, new Vector3(0.2f, 0.2f, 0.2f));
	}

	// Token: 0x060037F1 RID: 14321 RVA: 0x000CD250 File Offset: 0x000CB450
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawCube(base.transform.position, new Vector3(0.2f, 0.2f, 0.2f));
	}

	// Token: 0x060037F2 RID: 14322 RVA: 0x000CD28C File Offset: 0x000CB48C
	public static Hardpoint GetHardpointFromRay(Ray ray, Hardpoint.hardpoint_type type)
	{
		RaycastHit raycastHit;
		bool flag;
		MeshBatchInstance meshBatchInstance;
		if (MeshBatchPhysics.Raycast(ray, ref raycastHit, 10f, ref flag, ref meshBatchInstance))
		{
			IDMain idmain = (!flag) ? IDBase.GetMain(raycastHit.collider) : meshBatchInstance.idMain;
			if (idmain)
			{
				HardpointMaster component = idmain.GetComponent<HardpointMaster>();
				if (component)
				{
					return component.GetHardpointNear(raycastHit.point, type);
				}
			}
		}
		return null;
	}

	// Token: 0x04001C0F RID: 7183
	public Hardpoint.hardpoint_type type = Hardpoint.hardpoint_type.Generic;

	// Token: 0x04001C10 RID: 7184
	private DeployableObject holding;

	// Token: 0x04001C11 RID: 7185
	private HardpointMaster _master;

	// Token: 0x02000640 RID: 1600
	public enum hardpoint_type
	{
		// Token: 0x04001C13 RID: 7187
		None,
		// Token: 0x04001C14 RID: 7188
		Generic,
		// Token: 0x04001C15 RID: 7189
		Door,
		// Token: 0x04001C16 RID: 7190
		Turret,
		// Token: 0x04001C17 RID: 7191
		Gate,
		// Token: 0x04001C18 RID: 7192
		Window
	}
}
