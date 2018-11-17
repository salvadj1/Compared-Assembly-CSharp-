using System;
using Facepunch;
using uLink;

// Token: 0x0200014F RID: 335
public struct DamageBeing
{
	// Token: 0x170002D6 RID: 726
	// (get) Token: 0x06000A29 RID: 2601 RVA: 0x00028A14 File Offset: 0x00026C14
	public IDMain idMain
	{
		get
		{
			return (!this.id) ? null : this.id.idMain;
		}
	}

	// Token: 0x170002D7 RID: 727
	// (get) Token: 0x06000A2A RID: 2602 RVA: 0x00028A38 File Offset: 0x00026C38
	public Character character
	{
		get
		{
			return this.idOwnerMain as Character;
		}
	}

	// Token: 0x170002D8 RID: 728
	// (get) Token: 0x06000A2B RID: 2603 RVA: 0x00028A48 File Offset: 0x00026C48
	public IDMain idOwnerMain
	{
		get
		{
			IDMain idmain = (!this.id) ? null : this.id.idMain;
			if (idmain)
			{
				if (idmain is RigidObj)
				{
					NetworkView ownerView = ((RigidObj)idmain).ownerView;
					if (ownerView)
					{
						idmain = ownerView.GetComponent<IDMain>();
					}
					else
					{
						idmain = null;
					}
				}
				else if (idmain is IDeployedObjectMain)
				{
					DeployedObjectInfo deployedObjectInfo = ((IDeployedObjectMain)idmain).DeployedObjectInfo;
					if (deployedObjectInfo.valid)
					{
						return deployedObjectInfo.playerCharacter;
					}
				}
			}
			return idmain;
		}
	}

	// Token: 0x170002D9 RID: 729
	// (get) Token: 0x06000A2C RID: 2604 RVA: 0x00028AE4 File Offset: 0x00026CE4
	public Controllable controllable
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
			if (idOwnerMain is Character)
			{
				return ((Character)idOwnerMain).controllable;
			}
			if (idOwnerMain is IDeployedObjectMain)
			{
				DeployedObjectInfo deployedObjectInfo = ((IDeployedObjectMain)idOwnerMain).DeployedObjectInfo;
				if (deployedObjectInfo.valid)
				{
					return deployedObjectInfo.playerControllable;
				}
			}
			return null;
		}
	}

	// Token: 0x170002DA RID: 730
	// (get) Token: 0x06000A2D RID: 2605 RVA: 0x00028B5C File Offset: 0x00026D5C
	public PlayerClient client
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
			if (idOwnerMain is Character)
			{
				return ((Character)idOwnerMain).playerClient;
			}
			if (idOwnerMain is IDeployedObjectMain)
			{
				DeployedObjectInfo deployedObjectInfo = ((IDeployedObjectMain)idOwnerMain).DeployedObjectInfo;
				if (deployedObjectInfo.valid)
				{
					return deployedObjectInfo.playerClient;
				}
			}
			Controllable component = idOwnerMain.GetComponent<Controllable>();
			if (component)
			{
				PlayerClient playerClient = component.playerClient;
				if (!playerClient)
				{
					NetworkView networkView = component.networkView;
					if (networkView)
					{
						PlayerClient.Find(networkView.owner, out playerClient);
					}
				}
				return playerClient;
			}
			return null;
		}
	}

	// Token: 0x170002DB RID: 731
	// (get) Token: 0x06000A2E RID: 2606 RVA: 0x00028C1C File Offset: 0x00026E1C
	public NetworkView networkView
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

	// Token: 0x170002DC RID: 732
	// (get) Token: 0x06000A2F RID: 2607 RVA: 0x00028C64 File Offset: 0x00026E64
	public NetworkView ownerView
	{
		get
		{
			IDMain idmain = (!this.id) ? null : this.id.idMain;
			if (idmain is RigidObj)
			{
				return ((RigidObj)idmain).ownerView;
			}
			return this.networkView;
		}
	}

	// Token: 0x170002DD RID: 733
	// (get) Token: 0x06000A30 RID: 2608 RVA: 0x00028CB0 File Offset: 0x00026EB0
	public NetworkViewID networkViewID
	{
		get
		{
			NetworkView networkView = this.networkView;
			if (networkView)
			{
				return networkView.viewID;
			}
			return NetworkViewID.unassigned;
		}
	}

	// Token: 0x170002DE RID: 734
	// (get) Token: 0x06000A31 RID: 2609 RVA: 0x00028CDC File Offset: 0x00026EDC
	public NetworkViewID ownerViewID
	{
		get
		{
			NetworkView ownerView = this.ownerView;
			if (ownerView)
			{
				return ownerView.viewID;
			}
			return NetworkViewID.unassigned;
		}
	}

	// Token: 0x170002DF RID: 735
	// (get) Token: 0x06000A32 RID: 2610 RVA: 0x00028D08 File Offset: 0x00026F08
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

	// Token: 0x06000A33 RID: 2611 RVA: 0x00028D48 File Offset: 0x00026F48
	public bool Equals(DamageBeing other)
	{
		return this.id == other.id;
	}

	// Token: 0x06000A34 RID: 2612 RVA: 0x00028D5C File Offset: 0x00026F5C
	public override bool Equals(object obj)
	{
		return object.Equals(this.id, obj);
	}

	// Token: 0x06000A35 RID: 2613 RVA: 0x00028D6C File Offset: 0x00026F6C
	public override int GetHashCode()
	{
		return (!this.id) ? 0 : this.id.GetHashCode();
	}

	// Token: 0x06000A36 RID: 2614 RVA: 0x00028D90 File Offset: 0x00026F90
	public override string ToString()
	{
		if (this.id)
		{
			return string.Format("{{id=({0}),idMain=({1})}}", this.id, this.id.idMain);
		}
		return "{{null}}";
	}

	// Token: 0x06000A37 RID: 2615 RVA: 0x00028DC4 File Offset: 0x00026FC4
	public bool IsDifferentPlayer(PlayerClient exclude)
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
		if (idmain is Character)
		{
			PlayerClient playerClient = ((Character)idmain).playerClient;
			return playerClient && playerClient != exclude;
		}
		if (idmain is IDeployedObjectMain)
		{
			DeployedObjectInfo deployedObjectInfo = ((IDeployedObjectMain)idmain).DeployedObjectInfo;
			if (deployedObjectInfo.valid)
			{
				PlayerClient playerClient2 = deployedObjectInfo.playerClient;
				return playerClient2 && playerClient2 != exclude;
			}
		}
		Controllable component = idmain.GetComponent<Controllable>();
		if (component)
		{
			PlayerClient playerClient3 = component.playerClient;
			return playerClient3 && playerClient3 != exclude;
		}
		return false;
	}

	// Token: 0x170002E0 RID: 736
	// (get) Token: 0x06000A38 RID: 2616 RVA: 0x00028EB4 File Offset: 0x000270B4
	public ulong userID
	{
		get
		{
			PlayerClient client = this.client;
			if (client)
			{
				return client.userID;
			}
			return 0UL;
		}
	}

	// Token: 0x06000A39 RID: 2617 RVA: 0x00028EDC File Offset: 0x000270DC
	public static implicit operator IDBase(DamageBeing being)
	{
		return being.id;
	}

	// Token: 0x06000A3A RID: 2618 RVA: 0x00028EE8 File Offset: 0x000270E8
	public static bool operator true(DamageBeing being)
	{
		return being.id;
	}

	// Token: 0x06000A3B RID: 2619 RVA: 0x00028EFC File Offset: 0x000270FC
	public static bool operator false(DamageBeing being)
	{
		return !being.id;
	}

	// Token: 0x04000694 RID: 1684
	public IDBase id;
}
