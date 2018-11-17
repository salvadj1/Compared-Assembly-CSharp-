using System;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x020005D4 RID: 1492
public class BasicTorchItemRep : global::ItemRepresentation
{
	// Token: 0x06002F9D RID: 12189 RVA: 0x000B76C8 File Offset: 0x000B58C8
	public void RepIgnite()
	{
		if (!this.lit)
		{
			this.lit = true;
			this._myLight = this.muzzle.InstantiateAsChild(this._myLightPrefab, false);
		}
	}

	// Token: 0x06002F9E RID: 12190 RVA: 0x000B7700 File Offset: 0x000B5900
	public void RepExtinguish()
	{
		if (this.lit)
		{
			this.lit = false;
			this.KillLight();
		}
	}

	// Token: 0x06002F9F RID: 12191 RVA: 0x000B771C File Offset: 0x000B591C
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

	// Token: 0x06002FA0 RID: 12192 RVA: 0x000B7754 File Offset: 0x000B5954
	private void ServerRPC_Status(bool lit)
	{
		Facepunch.NetworkView networkView = base.networkView;
		uLink.RPCMode rpcmode;
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

	// Token: 0x06002FA1 RID: 12193 RVA: 0x000B7790 File Offset: 0x000B5990
	private void KillLight()
	{
		if (this._myLight)
		{
			Object.Destroy(this._myLight);
			this._myLight = null;
		}
	}

	// Token: 0x040019CA RID: 6602
	private const bool defaultLit = false;

	// Token: 0x040019CB RID: 6603
	public GameObject _myLight;

	// Token: 0x040019CC RID: 6604
	public GameObject _myLightPrefab;

	// Token: 0x040019CD RID: 6605
	private bool lit;
}
