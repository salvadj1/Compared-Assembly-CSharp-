using System;
using uLink;
using UnityEngine;

// Token: 0x020003B9 RID: 953
public struct CustomInstantiationArgs
{
	// Token: 0x06002180 RID: 8576 RVA: 0x0007B85C File Offset: 0x00079A5C
	public CustomInstantiationArgs(global::NetMainPrefab netMain, IDMain prefab, ref NetworkInstantiateArgs args, bool server)
	{
		this = new global::CustomInstantiationArgs(netMain, null, prefab, ref args, server, false);
	}

	// Token: 0x06002181 RID: 8577 RVA: 0x0007B86C File Offset: 0x00079A6C
	public CustomInstantiationArgs(global::NetMainPrefab netMain, Object customInstantiator, IDMain prefab, ref NetworkInstantiateArgs args, bool server)
	{
		this = new global::CustomInstantiationArgs(netMain, customInstantiator, prefab, ref args, server, true);
	}

	// Token: 0x06002182 RID: 8578 RVA: 0x0007B87C File Offset: 0x00079A7C
	private CustomInstantiationArgs(global::NetMainPrefab netMain, Object customInstantiator, IDMain prefab, ref NetworkInstantiateArgs args, bool server, bool checkCustomInstantitorArgument)
	{
		this.netMain = netMain;
		this.prefab = prefab;
		this.prefabNetworkView = prefab.networkView;
		this.args = args;
		this.server = server;
		if (checkCustomInstantitorArgument && customInstantiator)
		{
			this.customInstantiate = (customInstantiator as global::IPrefabCustomInstantiate);
			if (this.customInstantiate == null)
			{
				this.hasCustomInstantiator = global::CustomInstantiationArgs.CheckNetworkViewCustomInstantiator(this.prefabNetworkView, this.prefab, out this.customInstantiate);
			}
			else
			{
				this.hasCustomInstantiator = true;
			}
		}
		else
		{
			this.hasCustomInstantiator = global::CustomInstantiationArgs.CheckNetworkViewCustomInstantiator(this.prefabNetworkView, this.prefab, out this.customInstantiate);
		}
	}

	// Token: 0x170007EB RID: 2027
	// (get) Token: 0x06002183 RID: 8579 RVA: 0x0007B92C File Offset: 0x00079B2C
	public BitStream initialData
	{
		get
		{
			return this.args.initialData;
		}
	}

	// Token: 0x170007EC RID: 2028
	// (get) Token: 0x06002184 RID: 8580 RVA: 0x0007B948 File Offset: 0x00079B48
	public Vector3 position
	{
		get
		{
			return this.args.position;
		}
	}

	// Token: 0x170007ED RID: 2029
	// (get) Token: 0x06002185 RID: 8581 RVA: 0x0007B964 File Offset: 0x00079B64
	public Quaternion rotation
	{
		get
		{
			return this.args.rotation;
		}
	}

	// Token: 0x170007EE RID: 2030
	// (get) Token: 0x06002186 RID: 8582 RVA: 0x0007B980 File Offset: 0x00079B80
	public bool client
	{
		get
		{
			return !this.server;
		}
	}

	// Token: 0x06002187 RID: 8583 RVA: 0x0007B98C File Offset: 0x00079B8C
	private static bool CheckNetworkViewCustomInstantiator(uLink.NetworkView view, out global::IPrefabCustomInstantiate custom)
	{
		custom = (view.observed as global::IPrefabCustomInstantiate);
		return custom != null;
	}

	// Token: 0x06002188 RID: 8584 RVA: 0x0007B9A4 File Offset: 0x00079BA4
	private static bool CheckNetworkViewCustomInstantiator(IDMain character, out global::IPrefabCustomInstantiate custom)
	{
		custom = (character as global::IPrefabCustomInstantiate);
		return custom != null;
	}

	// Token: 0x06002189 RID: 8585 RVA: 0x0007B9B8 File Offset: 0x00079BB8
	private static bool CheckNetworkViewCustomInstantiator(uLink.NetworkView view, IDMain character, out global::IPrefabCustomInstantiate custom)
	{
		return global::CustomInstantiationArgs.CheckNetworkViewCustomInstantiator(view, out custom) || global::CustomInstantiationArgs.CheckNetworkViewCustomInstantiator(character, out custom);
	}

	// Token: 0x04000FD0 RID: 4048
	public readonly global::NetMainPrefab netMain;

	// Token: 0x04000FD1 RID: 4049
	public readonly IDMain prefab;

	// Token: 0x04000FD2 RID: 4050
	public readonly uLink.NetworkView prefabNetworkView;

	// Token: 0x04000FD3 RID: 4051
	public readonly NetworkInstantiateArgs args;

	// Token: 0x04000FD4 RID: 4052
	public readonly global::IPrefabCustomInstantiate customInstantiate;

	// Token: 0x04000FD5 RID: 4053
	public readonly bool server;

	// Token: 0x04000FD6 RID: 4054
	public readonly bool hasCustomInstantiator;
}
