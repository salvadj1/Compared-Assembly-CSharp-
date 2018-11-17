using System;
using Facepunch.MeshBatch;
using UnityEngine;

// Token: 0x02000702 RID: 1794
public class Hardpoint : IDRemote
{
	// Token: 0x06003BD7 RID: 15319 RVA: 0x000D5A60 File Offset: 0x000D3C60
	public void Awake()
	{
		global::HardpointMaster component = base.idMain.GetComponent<global::HardpointMaster>();
		if (component)
		{
			this.SetMaster(component);
		}
		base.Awake();
	}

	// Token: 0x06003BD8 RID: 15320 RVA: 0x000D5A94 File Offset: 0x000D3C94
	public void OnDestroy()
	{
		base.OnDestroy();
	}

	// Token: 0x06003BD9 RID: 15321 RVA: 0x000D5A9C File Offset: 0x000D3C9C
	public void SetMaster(global::HardpointMaster master)
	{
		this._master = master;
		master.AddHardpoint(this);
	}

	// Token: 0x06003BDA RID: 15322 RVA: 0x000D5AAC File Offset: 0x000D3CAC
	public global::HardpointMaster GetMaster()
	{
		return this._master;
	}

	// Token: 0x06003BDB RID: 15323 RVA: 0x000D5AB4 File Offset: 0x000D3CB4
	public bool IsFree()
	{
		return this.holding == null;
	}

	// Token: 0x06003BDC RID: 15324 RVA: 0x000D5AC4 File Offset: 0x000D3CC4
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawCube(base.transform.position, new Vector3(0.2f, 0.2f, 0.2f));
	}

	// Token: 0x06003BDD RID: 15325 RVA: 0x000D5B00 File Offset: 0x000D3D00
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawCube(base.transform.position, new Vector3(0.2f, 0.2f, 0.2f));
	}

	// Token: 0x06003BDE RID: 15326 RVA: 0x000D5B3C File Offset: 0x000D3D3C
	public static global::Hardpoint GetHardpointFromRay(Ray ray, global::Hardpoint.hardpoint_type type)
	{
		RaycastHit raycastHit;
		bool flag;
		MeshBatchInstance meshBatchInstance;
		if (Facepunch.MeshBatch.MeshBatchPhysics.Raycast(ray, ref raycastHit, 10f, ref flag, ref meshBatchInstance))
		{
			IDMain idmain = (!flag) ? IDBase.GetMain(raycastHit.collider) : meshBatchInstance.idMain;
			if (idmain)
			{
				global::HardpointMaster component = idmain.GetComponent<global::HardpointMaster>();
				if (component)
				{
					return component.GetHardpointNear(raycastHit.point, type);
				}
			}
		}
		return null;
	}

	// Token: 0x04001E04 RID: 7684
	public global::Hardpoint.hardpoint_type type = global::Hardpoint.hardpoint_type.Generic;

	// Token: 0x04001E05 RID: 7685
	private global::DeployableObject holding;

	// Token: 0x04001E06 RID: 7686
	private global::HardpointMaster _master;

	// Token: 0x02000703 RID: 1795
	public enum hardpoint_type
	{
		// Token: 0x04001E08 RID: 7688
		None,
		// Token: 0x04001E09 RID: 7689
		Generic,
		// Token: 0x04001E0A RID: 7690
		Door,
		// Token: 0x04001E0B RID: 7691
		Turret,
		// Token: 0x04001E0C RID: 7692
		Gate,
		// Token: 0x04001E0D RID: 7693
		Window
	}
}
