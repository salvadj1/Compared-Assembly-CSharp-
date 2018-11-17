using System;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x02000517 RID: 1303
public class BasicTorchItemRep : ItemRepresentation
{
	// Token: 0x06002BDD RID: 11229 RVA: 0x000AF62C File Offset: 0x000AD82C
	public void RepIgnite()
	{
		if (!this.lit)
		{
			this.lit = true;
			this._myLight = this.muzzle.InstantiateAsChild(this._myLightPrefab, false);
		}
	}

	// Token: 0x06002BDE RID: 11230 RVA: 0x000AF664 File Offset: 0x000AD864
	public void RepExtinguish()
	{
		if (this.lit)
		{
			this.lit = false;
			this.KillLight();
		}
	}

	// Token: 0x06002BDF RID: 11231 RVA: 0x000AF680 File Offset: 0x000AD880
	[RPC]
	protected void OnStatus(bool on)
	{
		if (on != this.lit)
		{
			if (on)
			{
				this.RepIgnite();
			}
			else
			{
				this.RepExtinguish();
			}
			this.lit = on;
		}
	}

	// Token: 0x06002BE0 RID: 11232 RVA: 0x000AF6B8 File Offset: 0x000AD8B8
	private void ServerRPC_Status(bool lit)
	{
		NetworkView networkView = base.networkView;
		RPCMode rpcmode;
		if (!lit)
		{
			rpcmode = 9;
		}
		else
		{
			rpcmode = 13;
		}
		networkView.RPC<bool>("OnStatus", rpcmode, lit);
		this.lit = lit;
	}

	// Token: 0x06002BE1 RID: 11233 RVA: 0x000AF6F4 File Offset: 0x000AD8F4
	private void KillLight()
	{
		if (this._myLight)
		{
			Object.Destroy(this._myLight);
			this._myLight = null;
		}
	}

	// Token: 0x040017FE RID: 6142
	private const bool defaultLit = false;

	// Token: 0x040017FF RID: 6143
	public GameObject _myLight;

	// Token: 0x04001800 RID: 6144
	public GameObject _myLightPrefab;

	// Token: 0x04001801 RID: 6145
	private bool lit;
}
