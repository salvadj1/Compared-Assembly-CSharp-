using System;
using Facepunch;
using uLink;

// Token: 0x02000179 RID: 377
public struct DamageBeing
{
	// Token: 0x17000318 RID: 792
	// (get) Token: 0x06000B4F RID: 2895 RVA: 0x0002C790 File Offset: 0x0002A990
	public IDMain idMain
	{
		get
		{
			return (!this.id) ? null : this.id.idMain;
		}
	}

	// Token: 0x17000319 RID: 793
	// (get) Token: 0x06000B50 RID: 2896 RVA: 0x0002C7B4 File Offset: 0x0002A9B4
	public global::Character character
	{
		get
		{
			return this.idOwnerMain as global::Character;
		}
	}

	// Token: 0x1700031A RID: 794
	// (get) Token: 0x06000B51 RID: 2897 RVA: 0x0002C7C4 File Offset: 0x0002A9C4
	public IDMain idOwnerMain
	{
		get
		{
			IDMain idmain = (!this.id) ? null : this.id.idMain;
			if (idmain)
			{
				if (idmain is global::RigidObj)
				{
					Facepunch.NetworkView ownerView = ((global::RigidObj)idmain).ownerView;
					if (ownerView)
					{
						idmain = ownerView.GetComponent<IDMain>();
					}
					else
					{
						idmain = null;
					}
				}
				else if (idmain is global::IDeployedObjectMain)
				{
					global::DeployedObjectInfo deployedObjectInfo = ((global::IDeployedObjectMain)idmain).DeployedObjectInfo;
					if (deployedObjectInfo.valid)
					{
						return deployedObjectInfo.playerCharacter;
					}
				}
			}
			return idmain;
		}
	}

	// Token: 0x1700031B RID: 795
	// (get) Token: 0x06000B52 RID: 2898 RVA: 0x0002C860 File Offset: 0x0002AA60
	public global::Controllable controllable
	{
		get
		{
			if (!this.id)
			{
				return null;
			}
			IDMain idOwnerMain = this.idOwnerMain;
			if (!idOwnerMain)
			{
				return null;
			}
			if (idOwnerMain is global::Character)
			{
				return ((global::Character)idOwnerMain).controllable;
			}
			if (idOwnerMain is global::IDeployedObjectMain)
			{
				global::DeployedObjectInfo deployedObjectInfo = ((global::IDeployedObjectMain)idOwnerMain).DeployedObjectInfo;
				if (deployedObjectInfo.valid)
				{
					return deployedObjectInfo.playerControllable;
				}
			}
			return null;
		}
	}

	// Token: 0x1700031C RID: 796
	// (get) Token: 0x06000B53 RID: 2899 RVA: 0x0002C8D8 File Offset: 0x0002AAD8
	public global::PlayerClient client
	{
		get
		{
			if (!this.id)
			{
				return null;
			}
			IDMain idOwnerMain = this.idOwnerMain;
			if (!idOwnerMain)
			{
				return null;
			}
			if (idOwnerMain is global::Character)
			{
				return ((global::Character)idOwnerMain).playerClient;
			}
			if (idOwnerMain is global::IDeployedObjectMain)
			{
				global::DeployedObjectInfo deployedObjectInfo = ((global::IDeployedObjectMain)idOwnerMain).DeployedObjectInfo;
				if (deployedObjectInfo.valid)
				{
					return deployedObjectInfo.playerClient;
				}
			}
			global::Controllable component = idOwnerMain.GetComponent<global::Controllable>();
			if (component)
			{
				global::PlayerClient playerClient = component.playerClient;
				if (!playerClient)
				{
					Facepunch.NetworkView networkView = component.networkView;
					if (networkView)
					{
						global::PlayerClient.Find(networkView.owner, out playerClient);
					}
				}
				return playerClient;
			}
			return null;
		}
	}

	// Token: 0x1700031D RID: 797
	// (get) Token: 0x06000B54 RID: 2900 RVA: 0x0002C998 File Offset: 0x0002AB98
	public Facepunch.NetworkView networkView
	{
		get
		{
			if (!this.id)
			{
				return null;
			}
			IDMain idMain = this.id.idMain;
			if (idMain)
			{
				return idMain.networkView;
			}
			return this.id.networkView;
		}
	}

	// Token: 0x1700031E RID: 798
	// (get) Token: 0x06000B55 RID: 2901 RVA: 0x0002C9E0 File Offset: 0x0002ABE0
	public Facepunch.NetworkView ownerView
	{
		get
		{
			IDMain idmain = (!this.id) ? null : this.id.idMain;
			if (idmain is global::RigidObj)
			{
				return ((global::RigidObj)idmain).ownerView;
			}
			return this.networkView;
		}
	}

	// Token: 0x1700031F RID: 799
	// (get) Token: 0x06000B56 RID: 2902 RVA: 0x0002CA2C File Offset: 0x0002AC2C
	public uLink.NetworkViewID networkViewID
	{
		get
		{
			Facepunch.NetworkView networkView = this.networkView;
			if (networkView)
			{
				return networkView.viewID;
			}
			return uLink.NetworkViewID.unassigned;
		}
	}

	// Token: 0x17000320 RID: 800
	// (get) Token: 0x06000B57 RID: 2903 RVA: 0x0002CA58 File Offset: 0x0002AC58
	public uLink.NetworkViewID ownerViewID
	{
		get
		{
			Facepunch.NetworkView ownerView = this.ownerView;
			if (ownerView)
			{
				return ownerView.viewID;
			}
			return uLink.NetworkViewID.unassigned;
		}
	}

	// Token: 0x17000321 RID: 801
	// (get) Token: 0x06000B58 RID: 2904 RVA: 0x0002CA84 File Offset: 0x0002AC84
	public BodyPart bodyPart
	{
		get
		{
			if (this.id is IDRemoteBodyPart && this.id)
			{
				return ((IDRemoteBodyPart)this.id).bodyPart;
			}
			return 0;
		}
	}

	// Token: 0x06000B59 RID: 2905 RVA: 0x0002CAC4 File Offset: 0x0002ACC4
	public bool Equals(global::DamageBeing other)
	{
		return this.id == other.id;
	}

	// Token: 0x06000B5A RID: 2906 RVA: 0x0002CAD8 File Offset: 0x0002ACD8
	public override bool Equals(object obj)
	{
		return object.Equals(this.id, obj);
	}

	// Token: 0x06000B5B RID: 2907 RVA: 0x0002CAE8 File Offset: 0x0002ACE8
	public override int GetHashCode()
	{
		return (!this.id) ? 0 : this.id.GetHashCode();
	}

	// Token: 0x06000B5C RID: 2908 RVA: 0x0002CB0C File Offset: 0x0002AD0C
	public override string ToString()
	{
		if (this.id)
		{
			return string.Format("{{id=({0}),idMain=({1})}}", this.id, this.id.idMain);
		}
		return "{{null}}";
	}

	// Token: 0x06000B5D RID: 2909 RVA: 0x0002CB40 File Offset: 0x0002AD40
	public bool IsDifferentPlayer(global::PlayerClient exclude)
	{
		if (!this.id)
		{
			return false;
		}
		IDMain idmain = this.idOwnerMain;
		if (!idmain)
		{
			idmain = this.id.idMain;
			if (!idmain)
			{
				return false;
			}
		}
		if (idmain is global::Character)
		{
			global::PlayerClient playerClient = ((global::Character)idmain).playerClient;
			return playerClient && playerClient != exclude;
		}
		if (idmain is global::IDeployedObjectMain)
		{
			global::DeployedObjectInfo deployedObjectInfo = ((global::IDeployedObjectMain)idmain).DeployedObjectInfo;
			if (deployedObjectInfo.valid)
			{
				global::PlayerClient playerClient2 = deployedObjectInfo.playerClient;
				return playerClient2 && playerClient2 != exclude;
			}
		}
		global::Controllable component = idmain.GetComponent<global::Controllable>();
		if (component)
		{
			global::PlayerClient playerClient3 = component.playerClient;
			return playerClient3 && playerClient3 != exclude;
		}
		return false;
	}

	// Token: 0x17000322 RID: 802
	// (get) Token: 0x06000B5E RID: 2910 RVA: 0x0002CC30 File Offset: 0x0002AE30
	public ulong userID
	{
		get
		{
			global::PlayerClient client = this.client;
			if (client)
			{
				return client.userID;
			}
			return 0UL;
		}
	}

	// Token: 0x06000B5F RID: 2911 RVA: 0x0002CC58 File Offset: 0x0002AE58
	public static implicit operator IDBase(global::DamageBeing being)
	{
		return being.id;
	}

	// Token: 0x06000B60 RID: 2912 RVA: 0x0002CC64 File Offset: 0x0002AE64
	public static bool operator true(global::DamageBeing being)
	{
		return being.id;
	}

	// Token: 0x06000B61 RID: 2913 RVA: 0x0002CC78 File Offset: 0x0002AE78
	public static bool operator false(global::DamageBeing being)
	{
		return !being.id;
	}

	// Token: 0x040007A3 RID: 1955
	public IDBase id;
}
