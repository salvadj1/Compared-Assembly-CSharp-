using System;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x020003B4 RID: 948
[AddComponentMenu("")]
public sealed class NetInstance : IDLocal
{
	// Token: 0x06002140 RID: 8512 RVA: 0x0007AAEC File Offset: 0x00078CEC
	public NetInstance()
	{
		this.preDestroy = new global::DisposeCallbackList<global::NetInstance, global::NetInstance.CallbackFunction>(this, global::NetInstance.callbackFire);
		this.postCreate = new global::DisposeCallbackList<global::NetInstance, global::NetInstance.CallbackFunction>(this, global::NetInstance.callbackFire);
		this.preCreate = new global::DisposeCallbackList<global::NetInstance, global::NetInstance.CallbackFunction>(this, global::NetInstance.callbackFire);
	}

	// Token: 0x14000014 RID: 20
	// (add) Token: 0x06002142 RID: 8514 RVA: 0x0007AB3C File Offset: 0x00078D3C
	// (remove) Token: 0x06002143 RID: 8515 RVA: 0x0007AB4C File Offset: 0x00078D4C
	public event global::NetInstance.CallbackFunction onPreDestroy
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
	// (add) Token: 0x06002144 RID: 8516 RVA: 0x0007AB5C File Offset: 0x00078D5C
	// (remove) Token: 0x06002145 RID: 8517 RVA: 0x0007AB6C File Offset: 0x00078D6C
	public event global::NetInstance.CallbackFunction onPreCreate
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
	// (add) Token: 0x06002146 RID: 8518 RVA: 0x0007AB7C File Offset: 0x00078D7C
	// (remove) Token: 0x06002147 RID: 8519 RVA: 0x0007AB8C File Offset: 0x00078D8C
	public event global::NetInstance.CallbackFunction onPostCreate
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

	// Token: 0x170007DA RID: 2010
	// (get) Token: 0x06002148 RID: 8520 RVA: 0x0007AB9C File Offset: 0x00078D9C
	public bool serverSide
	{
		get
		{
			return this.args.server;
		}
	}

	// Token: 0x170007DB RID: 2011
	// (get) Token: 0x06002149 RID: 8521 RVA: 0x0007ABAC File Offset: 0x00078DAC
	public bool clientSide
	{
		get
		{
			return this.args.client;
		}
	}

	// Token: 0x170007DC RID: 2012
	// (get) Token: 0x0600214A RID: 8522 RVA: 0x0007ABBC File Offset: 0x00078DBC
	public bool isProxy
	{
		get
		{
			return this.prepared && this.local && !this.args.server;
		}
	}

	// Token: 0x170007DD RID: 2013
	// (get) Token: 0x0600214B RID: 8523 RVA: 0x0007ABE8 File Offset: 0x00078DE8
	public IDMain prefab
	{
		get
		{
			return this.args.prefab;
		}
	}

	// Token: 0x170007DE RID: 2014
	// (get) Token: 0x0600214C RID: 8524 RVA: 0x0007ABF8 File Offset: 0x00078DF8
	public uLink.NetworkView prefabNetworkView
	{
		get
		{
			return this.args.prefabNetworkView;
		}
	}

	// Token: 0x170007DF RID: 2015
	// (get) Token: 0x0600214D RID: 8525 RVA: 0x0007AC08 File Offset: 0x00078E08
	public global::NetMainPrefab netMain
	{
		get
		{
			return this.args.netMain;
		}
	}

	// Token: 0x170007E0 RID: 2016
	// (get) Token: 0x0600214E RID: 8526 RVA: 0x0007AC18 File Offset: 0x00078E18
	public bool wasCreatedByCustomInstantiate
	{
		get
		{
			return this.args.hasCustomInstantiator;
		}
	}

	// Token: 0x170007E1 RID: 2017
	// (get) Token: 0x0600214F RID: 8527 RVA: 0x0007AC28 File Offset: 0x00078E28
	public global::IPrefabCustomInstantiate customeInstantiateCreator
	{
		get
		{
			return this.args.customInstantiate;
		}
	}

	// Token: 0x06002150 RID: 8528 RVA: 0x0007AC38 File Offset: 0x00078E38
	private static void CallbackFire(global::NetInstance instance, global::NetInstance.CallbackFunction func)
	{
		func(instance);
	}

	// Token: 0x06002151 RID: 8529 RVA: 0x0007AC44 File Offset: 0x00078E44
	internal void zzz___onpredestroy()
	{
		this.preDestroy.Dispose();
	}

	// Token: 0x06002152 RID: 8530 RVA: 0x0007AC54 File Offset: 0x00078E54
	internal void zzz___onprecreate()
	{
		this.preCreate.Dispose();
	}

	// Token: 0x06002153 RID: 8531 RVA: 0x0007AC64 File Offset: 0x00078E64
	internal void zzz___onpostcreate()
	{
		this.postCreate.Dispose();
	}

	// Token: 0x06002154 RID: 8532 RVA: 0x0007AC74 File Offset: 0x00078E74
	private void OnDestroy()
	{
		this.postCreate = (this.preCreate = (this.preDestroy = global::DisposeCallbackList<global::NetInstance, global::NetInstance.CallbackFunction>.invalid));
	}

	// Token: 0x170007E2 RID: 2018
	// (get) Token: 0x06002155 RID: 8533 RVA: 0x0007ACA0 File Offset: 0x00078EA0
	public static global::NetInstance current
	{
		get
		{
			return global::NetMainPrefab.zzz__currentNetInstance;
		}
	}

	// Token: 0x06002156 RID: 8534 RVA: 0x0007ACA8 File Offset: 0x00078EA8
	public static bool IsCurrentlyDestroying(IDMain main)
	{
		global::NetInstance current = global::NetInstance.current;
		return current && current.idMain == main;
	}

	// Token: 0x06002157 RID: 8535 RVA: 0x0007ACD8 File Offset: 0x00078ED8
	public static bool IsCurrentlyDestroying(IDLocal local)
	{
		global::NetInstance current = global::NetInstance.current;
		return current && current.idMain == local.idMain;
	}

	// Token: 0x06002158 RID: 8536 RVA: 0x0007AD0C File Offset: 0x00078F0C
	public static bool IsCurrentlyDestroying(IDRemote remote)
	{
		global::NetInstance current = global::NetInstance.current;
		return current && current.idMain == remote.idMain;
	}

	// Token: 0x04000FB4 RID: 4020
	[NonSerialized]
	public global::CustomInstantiationArgs args;

	// Token: 0x04000FB5 RID: 4021
	[NonSerialized]
	public bool prepared;

	// Token: 0x04000FB6 RID: 4022
	[NonSerialized]
	public bool local;

	// Token: 0x04000FB7 RID: 4023
	[NonSerialized]
	internal bool destroying;

	// Token: 0x04000FB8 RID: 4024
	[NonSerialized]
	public uLink.NetworkMessageInfo info;

	// Token: 0x04000FB9 RID: 4025
	[NonSerialized]
	public Facepunch.NetworkView networkView;

	// Token: 0x04000FBA RID: 4026
	[NonSerialized]
	public IDRemote localAppendage;

	// Token: 0x04000FBB RID: 4027
	[NonSerialized]
	public bool madeLocalAppendage;

	// Token: 0x04000FBC RID: 4028
	private static readonly global::DisposeCallbackList<global::NetInstance, global::NetInstance.CallbackFunction>.Function callbackFire = new global::DisposeCallbackList<global::NetInstance, global::NetInstance.CallbackFunction>.Function(global::NetInstance.CallbackFire);

	// Token: 0x04000FBD RID: 4029
	private global::DisposeCallbackList<global::NetInstance, global::NetInstance.CallbackFunction> preDestroy;

	// Token: 0x04000FBE RID: 4030
	private global::DisposeCallbackList<global::NetInstance, global::NetInstance.CallbackFunction> preCreate;

	// Token: 0x04000FBF RID: 4031
	private global::DisposeCallbackList<global::NetInstance, global::NetInstance.CallbackFunction> postCreate;

	// Token: 0x020003B5 RID: 949
	// (Invoke) Token: 0x0600215A RID: 8538
	public delegate void CallbackFunction(global::NetInstance instance);
}
