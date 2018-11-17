using System;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x0200030C RID: 780
[AddComponentMenu("")]
public sealed class NetInstance : IDLocal
{
	// Token: 0x06001E02 RID: 7682 RVA: 0x0007606C File Offset: 0x0007426C
	public NetInstance()
	{
		this.preDestroy = new DisposeCallbackList<NetInstance, NetInstance.CallbackFunction>(this, NetInstance.callbackFire);
		this.postCreate = new DisposeCallbackList<NetInstance, NetInstance.CallbackFunction>(this, NetInstance.callbackFire);
		this.preCreate = new DisposeCallbackList<NetInstance, NetInstance.CallbackFunction>(this, NetInstance.callbackFire);
	}

	// Token: 0x14000014 RID: 20
	// (add) Token: 0x06001E04 RID: 7684 RVA: 0x000760BC File Offset: 0x000742BC
	// (remove) Token: 0x06001E05 RID: 7685 RVA: 0x000760CC File Offset: 0x000742CC
	public event NetInstance.CallbackFunction onPreDestroy
	{
		add
		{
			this.preDestroy.Add(value);
		}
		remove
		{
			this.preDestroy.Remove(value);
		}
	}

	// Token: 0x14000015 RID: 21
	// (add) Token: 0x06001E06 RID: 7686 RVA: 0x000760DC File Offset: 0x000742DC
	// (remove) Token: 0x06001E07 RID: 7687 RVA: 0x000760EC File Offset: 0x000742EC
	public event NetInstance.CallbackFunction onPreCreate
	{
		add
		{
			this.preCreate.Add(value);
		}
		remove
		{
			this.preCreate.Remove(value);
		}
	}

	// Token: 0x14000016 RID: 22
	// (add) Token: 0x06001E08 RID: 7688 RVA: 0x000760FC File Offset: 0x000742FC
	// (remove) Token: 0x06001E09 RID: 7689 RVA: 0x0007610C File Offset: 0x0007430C
	public event NetInstance.CallbackFunction onPostCreate
	{
		add
		{
			this.postCreate.Add(value);
		}
		remove
		{
			this.postCreate.Remove(value);
		}
	}

	// Token: 0x17000784 RID: 1924
	// (get) Token: 0x06001E0A RID: 7690 RVA: 0x0007611C File Offset: 0x0007431C
	public bool serverSide
	{
		get
		{
			return this.args.server;
		}
	}

	// Token: 0x17000785 RID: 1925
	// (get) Token: 0x06001E0B RID: 7691 RVA: 0x0007612C File Offset: 0x0007432C
	public bool clientSide
	{
		get
		{
			return this.args.client;
		}
	}

	// Token: 0x17000786 RID: 1926
	// (get) Token: 0x06001E0C RID: 7692 RVA: 0x0007613C File Offset: 0x0007433C
	public bool isProxy
	{
		get
		{
			return this.prepared && this.local && !this.args.server;
		}
	}

	// Token: 0x17000787 RID: 1927
	// (get) Token: 0x06001E0D RID: 7693 RVA: 0x00076168 File Offset: 0x00074368
	public IDMain prefab
	{
		get
		{
			return this.args.prefab;
		}
	}

	// Token: 0x17000788 RID: 1928
	// (get) Token: 0x06001E0E RID: 7694 RVA: 0x00076178 File Offset: 0x00074378
	public NetworkView prefabNetworkView
	{
		get
		{
			return this.args.prefabNetworkView;
		}
	}

	// Token: 0x17000789 RID: 1929
	// (get) Token: 0x06001E0F RID: 7695 RVA: 0x00076188 File Offset: 0x00074388
	public NetMainPrefab netMain
	{
		get
		{
			return this.args.netMain;
		}
	}

	// Token: 0x1700078A RID: 1930
	// (get) Token: 0x06001E10 RID: 7696 RVA: 0x00076198 File Offset: 0x00074398
	public bool wasCreatedByCustomInstantiate
	{
		get
		{
			return this.args.hasCustomInstantiator;
		}
	}

	// Token: 0x1700078B RID: 1931
	// (get) Token: 0x06001E11 RID: 7697 RVA: 0x000761A8 File Offset: 0x000743A8
	public IPrefabCustomInstantiate customeInstantiateCreator
	{
		get
		{
			return this.args.customInstantiate;
		}
	}

	// Token: 0x06001E12 RID: 7698 RVA: 0x000761B8 File Offset: 0x000743B8
	private static void CallbackFire(NetInstance instance, NetInstance.CallbackFunction func)
	{
		func(instance);
	}

	// Token: 0x06001E13 RID: 7699 RVA: 0x000761C4 File Offset: 0x000743C4
	internal void zzz___onpredestroy()
	{
		this.preDestroy.Dispose();
	}

	// Token: 0x06001E14 RID: 7700 RVA: 0x000761D4 File Offset: 0x000743D4
	internal void zzz___onprecreate()
	{
		this.preCreate.Dispose();
	}

	// Token: 0x06001E15 RID: 7701 RVA: 0x000761E4 File Offset: 0x000743E4
	internal void zzz___onpostcreate()
	{
		this.postCreate.Dispose();
	}

	// Token: 0x06001E16 RID: 7702 RVA: 0x000761F4 File Offset: 0x000743F4
	private void OnDestroy()
	{
		this.postCreate = (this.preCreate = (this.preDestroy = DisposeCallbackList<NetInstance, NetInstance.CallbackFunction>.invalid));
	}

	// Token: 0x1700078C RID: 1932
	// (get) Token: 0x06001E17 RID: 7703 RVA: 0x00076220 File Offset: 0x00074420
	public static NetInstance current
	{
		get
		{
			return NetMainPrefab.zzz__currentNetInstance;
		}
	}

	// Token: 0x06001E18 RID: 7704 RVA: 0x00076228 File Offset: 0x00074428
	public static bool IsCurrentlyDestroying(IDMain main)
	{
		NetInstance current = NetInstance.current;
		return current && current.idMain == main;
	}

	// Token: 0x06001E19 RID: 7705 RVA: 0x00076258 File Offset: 0x00074458
	public static bool IsCurrentlyDestroying(IDLocal local)
	{
		NetInstance current = NetInstance.current;
		return current && current.idMain == local.idMain;
	}

	// Token: 0x06001E1A RID: 7706 RVA: 0x0007628C File Offset: 0x0007448C
	public static bool IsCurrentlyDestroying(IDRemote remote)
	{
		NetInstance current = NetInstance.current;
		return current && current.idMain == remote.idMain;
	}

	// Token: 0x04000E74 RID: 3700
	[NonSerialized]
	public CustomInstantiationArgs args;

	// Token: 0x04000E75 RID: 3701
	[NonSerialized]
	public bool prepared;

	// Token: 0x04000E76 RID: 3702
	[NonSerialized]
	public bool local;

	// Token: 0x04000E77 RID: 3703
	[NonSerialized]
	internal bool destroying;

	// Token: 0x04000E78 RID: 3704
	[NonSerialized]
	public NetworkMessageInfo info;

	// Token: 0x04000E79 RID: 3705
	[NonSerialized]
	public NetworkView networkView;

	// Token: 0x04000E7A RID: 3706
	[NonSerialized]
	public IDRemote localAppendage;

	// Token: 0x04000E7B RID: 3707
	[NonSerialized]
	public bool madeLocalAppendage;

	// Token: 0x04000E7C RID: 3708
	private static readonly DisposeCallbackList<NetInstance, NetInstance.CallbackFunction>.Function callbackFire = new DisposeCallbackList<NetInstance, NetInstance.CallbackFunction>.Function(NetInstance.CallbackFire);

	// Token: 0x04000E7D RID: 3709
	private DisposeCallbackList<NetInstance, NetInstance.CallbackFunction> preDestroy;

	// Token: 0x04000E7E RID: 3710
	private DisposeCallbackList<NetInstance, NetInstance.CallbackFunction> preCreate;

	// Token: 0x04000E7F RID: 3711
	private DisposeCallbackList<NetInstance, NetInstance.CallbackFunction> postCreate;

	// Token: 0x020008D6 RID: 2262
	// (Invoke) Token: 0x06004D30 RID: 19760
	public delegate void CallbackFunction(NetInstance instance);
}
