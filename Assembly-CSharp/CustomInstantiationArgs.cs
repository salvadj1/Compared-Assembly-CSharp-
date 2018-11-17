using System;
using uLink;
using UnityEngine;

// Token: 0x02000310 RID: 784
public struct CustomInstantiationArgs
{
	// Token: 0x06001E3E RID: 7742 RVA: 0x00076DDC File Offset: 0x00074FDC
	public CustomInstantiationArgs(NetMainPrefab netMain, IDMain prefab, ref NetworkInstantiateArgs args, bool server)
	{
		this = new CustomInstantiationArgs(netMain, null, prefab, ref args, server, false);
	}

	// Token: 0x06001E3F RID: 7743 RVA: 0x00076DEC File Offset: 0x00074FEC
	public CustomInstantiationArgs(NetMainPrefab netMain, Object customInstantiator, IDMain prefab, ref NetworkInstantiateArgs args, bool server)
	{
		this = new CustomInstantiationArgs(netMain, customInstantiator, prefab, ref args, server, true);
	}

	// Token: 0x06001E40 RID: 7744 RVA: 0x00076DFC File Offset: 0x00074FFC
	private CustomInstantiationArgs(NetMainPrefab netMain, Object customInstantiator, IDMain prefab, ref NetworkInstantiateArgs args, bool server, bool checkCustomInstantitorArgument)
	{
		this.netMain = netMain;
		this.prefab = prefab;
		this.prefabNetworkView = prefab.networkView;
		this.args = args;
		this.server = server;
		if (checkCustomInstantitorArgument && customInstantiator)
		{
			this.customInstantiate = (customInstantiator as IPrefabCustomInstantiate);
			if (this.customInstantiate == null)
			{
				this.hasCustomInstantiator = CustomInstantiationArgs.CheckNetworkViewCustomInstantiator(this.prefabNetworkView, this.prefab, out this.customInstantiate);
			}
			else
			{
				this.hasCustomInstantiator = true;
			}
		}
		else
		{
			this.hasCustomInstantiator = CustomInstantiationArgs.CheckNetworkViewCustomInstantiator(this.prefabNetworkView, this.prefab, out this.customInstantiate);
		}
	}

	// Token: 0x17000795 RID: 1941
	// (get) Token: 0x06001E41 RID: 7745 RVA: 0x00076EAC File Offset: 0x000750AC
	public BitStream initialData
	{
		get
		{
			return this.args.initialData;
		}
	}

	// Token: 0x17000796 RID: 1942
	// (get) Token: 0x06001E42 RID: 7746 RVA: 0x00076EC8 File Offset: 0x000750C8
	public Vector3 position
	{
		get
		{
			return this.args.position;
		}
	}

	// Token: 0x17000797 RID: 1943
	// (get) Token: 0x06001E43 RID: 7747 RVA: 0x00076EE4 File Offset: 0x000750E4
	public Quaternion rotation
	{
		get
		{
			return this.args.rotation;
		}
	}

	// Token: 0x17000798 RID: 1944
	// (get) Token: 0x06001E44 RID: 7748 RVA: 0x00076F00 File Offset: 0x00075100
	public bool client
	{
		get
		{
			return !this.server;
		}
	}

	// Token: 0x06001E45 RID: 7749 RVA: 0x00076F0C File Offset: 0x0007510C
	private static bool CheckNetworkViewCustomInstantiator(NetworkView view, out IPrefabCustomInstantiate custom)
	{
		custom = (view.observed as IPrefabCustomInstantiate);
		return custom != null;
	}

	// Token: 0x06001E46 RID: 7750 RVA: 0x00076F24 File Offset: 0x00075124
	private static bool CheckNetworkViewCustomInstantiator(IDMain character, out IPrefabCustomInstantiate custom)
	{
		custom = (character as IPrefabCustomInstantiate);
		return custom != null;
	}

	// Token: 0x06001E47 RID: 7751 RVA: 0x00076F38 File Offset: 0x00075138
	private static bool CheckNetworkViewCustomInstantiator(NetworkView view, IDMain character, out IPrefabCustomInstantiate custom)
	{
		return CustomInstantiationArgs.CheckNetworkViewCustomInstantiator(view, out custom) || CustomInstantiationArgs.CheckNetworkViewCustomInstantiator(character, out custom);
	}

	// Token: 0x04000E90 RID: 3728
	public readonly NetMainPrefab netMain;

	// Token: 0x04000E91 RID: 3729
	public readonly IDMain prefab;

	// Token: 0x04000E92 RID: 3730
	public readonly NetworkView prefabNetworkView;

	// Token: 0x04000E93 RID: 3731
	public readonly NetworkInstantiateArgs args;

	// Token: 0x04000E94 RID: 3732
	public readonly IPrefabCustomInstantiate customInstantiate;

	// Token: 0x04000E95 RID: 3733
	public readonly bool server;

	// Token: 0x04000E96 RID: 3734
	public readonly bool hasCustomInstantiator;
}
