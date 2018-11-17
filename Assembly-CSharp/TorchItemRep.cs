using System;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x020005FE RID: 1534
public class TorchItemRep : ItemRepresentation
{
	// Token: 0x060036D9 RID: 14041 RVA: 0x000C5960 File Offset: 0x000C3B60
	private void KillLight()
	{
		if (this._myLight)
		{
			Object.Destroy(this._myLight);
			this._myLight = null;
		}
	}

	// Token: 0x060036DA RID: 14042 RVA: 0x000C5990 File Offset: 0x000C3B90
	protected new void OnDestroy()
	{
		this.KillLight();
		base.OnDestroy();
	}

	// Token: 0x060036DB RID: 14043 RVA: 0x000C59A0 File Offset: 0x000C3BA0
	protected new void uLink_OnNetworkInstantiate(NetworkMessageInfo info)
	{
		base.uLink_OnNetworkInstantiate(info);
		this.OnStatus(false);
	}

	// Token: 0x060036DC RID: 14044 RVA: 0x000C59B0 File Offset: 0x000C3BB0
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

	// Token: 0x060036DD RID: 14045 RVA: 0x000C59E8 File Offset: 0x000C3BE8
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

	// Token: 0x060036DE RID: 14046 RVA: 0x000C5A24 File Offset: 0x000C3C24
	public void RepIgnite()
	{
		if (!this.lit)
		{
			this.lit = true;
			this.StrikeSound.Play(base.transform.position, 1f, 2f, 8f);
			this._myLight = this.muzzle.InstantiateAsChild(this._myLightPrefab, false);
		}
	}

	// Token: 0x060036DF RID: 14047 RVA: 0x000C5A80 File Offset: 0x000C3C80
	public void RepExtinguish()
	{
		if (this.lit)
		{
			this.lit = false;
			this.KillLight();
		}
	}

	// Token: 0x04001AF5 RID: 6901
	private const bool defaultLit = false;

	// Token: 0x04001AF6 RID: 6902
	public GameObject _myLight;

	// Token: 0x04001AF7 RID: 6903
	public GameObject _myLightPrefab;

	// Token: 0x04001AF8 RID: 6904
	public AudioClip StrikeSound;

	// Token: 0x04001AF9 RID: 6905
	private bool lit;
}
