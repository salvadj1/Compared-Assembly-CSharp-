using System;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x020006BE RID: 1726
public class TorchItemRep : global::ItemRepresentation
{
	// Token: 0x06003AB1 RID: 15025 RVA: 0x000CDE90 File Offset: 0x000CC090
	private void KillLight()
	{
		if (this._myLight)
		{
			Object.Destroy(this._myLight);
			this._myLight = null;
		}
	}

	// Token: 0x06003AB2 RID: 15026 RVA: 0x000CDEC0 File Offset: 0x000CC0C0
	protected new void OnDestroy()
	{
		this.KillLight();
		base.OnDestroy();
	}

	// Token: 0x06003AB3 RID: 15027 RVA: 0x000CDED0 File Offset: 0x000CC0D0
	protected new void uLink_OnNetworkInstantiate(uLink.NetworkMessageInfo info)
	{
		base.uLink_OnNetworkInstantiate(info);
		this.OnStatus(false);
	}

	// Token: 0x06003AB4 RID: 15028 RVA: 0x000CDEE0 File Offset: 0x000CC0E0
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

	// Token: 0x06003AB5 RID: 15029 RVA: 0x000CDF18 File Offset: 0x000CC118
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

	// Token: 0x06003AB6 RID: 15030 RVA: 0x000CDF54 File Offset: 0x000CC154
	public void RepIgnite()
	{
		if (!this.lit)
		{
			this.lit = true;
			this.StrikeSound.Play(base.transform.position, 1f, 2f, 8f);
			this._myLight = this.muzzle.InstantiateAsChild(this._myLightPrefab, false);
		}
	}

	// Token: 0x06003AB7 RID: 15031 RVA: 0x000CDFB0 File Offset: 0x000CC1B0
	public void RepExtinguish()
	{
		if (this.lit)
		{
			this.lit = false;
			this.KillLight();
		}
	}

	// Token: 0x04001CDB RID: 7387
	private const bool defaultLit = false;

	// Token: 0x04001CDC RID: 7388
	public GameObject _myLight;

	// Token: 0x04001CDD RID: 7389
	public GameObject _myLightPrefab;

	// Token: 0x04001CDE RID: 7390
	public AudioClip StrikeSound;

	// Token: 0x04001CDF RID: 7391
	private bool lit;
}
