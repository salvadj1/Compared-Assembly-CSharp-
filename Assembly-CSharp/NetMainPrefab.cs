using System;
using System.Collections.Generic;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x0200030E RID: 782
public class NetMainPrefab : ScriptableObject
{
	// Token: 0x06001E21 RID: 7713 RVA: 0x00076304 File Offset: 0x00074504
	public NetMainPrefab() : this(typeof(IDMain), false)
	{
	}

	// Token: 0x06001E22 RID: 7714 RVA: 0x00076318 File Offset: 0x00074518
	protected NetMainPrefab(Type minimumType) : this(minimumType, true)
	{
	}

	// Token: 0x06001E23 RID: 7715 RVA: 0x00076324 File Offset: 0x00074524
	private NetMainPrefab(Type minimumType, bool typeCheck)
	{
		if (typeCheck && !typeof(IDMain).IsAssignableFrom(minimumType))
		{
			throw new ArgumentOutOfRangeException("minimumType", "must be assignable to IDMain");
		}
		this.MinimumTypeAllowed = minimumType;
		this.CollectCallbacks(out this.creator, out this.destroyer);
	}

	// Token: 0x1700078D RID: 1933
	// (get) Token: 0x06001E24 RID: 7716 RVA: 0x0007637C File Offset: 0x0007457C
	private IDRemote localAppend
	{
		get
		{
			return this._localAppend;
		}
	}

	// Token: 0x1700078E RID: 1934
	// (get) Token: 0x06001E25 RID: 7717 RVA: 0x00076384 File Offset: 0x00074584
	public IDMain prefab
	{
		get
		{
			return this.proxyPrefab;
		}
	}

	// Token: 0x1700078F RID: 1935
	// (get) Token: 0x06001E26 RID: 7718 RVA: 0x0007638C File Offset: 0x0007458C
	public IDMain proxyPrefab
	{
		get
		{
			return (!this._proxyPrefab) ? this._serverPrefab : this._proxyPrefab;
		}
	}

	// Token: 0x17000790 RID: 1936
	// (get) Token: 0x06001E27 RID: 7719 RVA: 0x000763B0 File Offset: 0x000745B0
	public IDMain localPrefab
	{
		get
		{
			return (!this._localPrefab) ? this.proxyPrefab : this._localPrefab;
		}
	}

	// Token: 0x17000791 RID: 1937
	// (get) Token: 0x06001E28 RID: 7720 RVA: 0x000763D4 File Offset: 0x000745D4
	public IDMain serverPrefab
	{
		get
		{
			return (!this._serverPrefab) ? this.proxyPrefab : this._serverPrefab;
		}
	}

	// Token: 0x06001E29 RID: 7721 RVA: 0x000763F8 File Offset: 0x000745F8
	public static Transform GetLocalAppendTransform(IDMain instanceOrPrefab, string _pathToLocalAppend)
	{
		if (!instanceOrPrefab)
		{
			return null;
		}
		if (string.IsNullOrEmpty(_pathToLocalAppend))
		{
			return instanceOrPrefab.transform;
		}
		Transform transform = instanceOrPrefab.transform.FindChild(_pathToLocalAppend);
		if (!transform)
		{
			Debug.LogError("The transform path:\"" + _pathToLocalAppend + "\" is no longer valid for given transform. returning the transform of the main", instanceOrPrefab);
			transform = instanceOrPrefab.transform;
		}
		return transform;
	}

	// Token: 0x06001E2A RID: 7722 RVA: 0x0007645C File Offset: 0x0007465C
	public Transform GetLocalAppendTransform(IDMain instanceOrPrefab)
	{
		return NetMainPrefab.GetLocalAppendTransform(instanceOrPrefab, this._pathToLocalAppend);
	}

	// Token: 0x17000792 RID: 1938
	// (get) Token: 0x06001E2B RID: 7723 RVA: 0x0007646C File Offset: 0x0007466C
	public Transform localAppendTransformInPrefab
	{
		get
		{
			return this.GetLocalAppendTransform(this.proxyPrefab);
		}
	}

	// Token: 0x06001E2C RID: 7724 RVA: 0x0007647C File Offset: 0x0007467C
	private NetworkView Create(ref CustomInstantiationArgs args, out IDMain instance)
	{
		if (float.IsNaN(args.position.x) || float.IsNaN(args.position.y) || float.IsNaN(args.position.z))
		{
			Debug.LogWarning("NetMainPrefab -> Create -  args.position = " + args.position);
			Debug.LogWarning("This means you're creating an object with a bad position!");
		}
		NetInstance currentNetInstance = NetMainPrefab._currentNetInstance;
		NetworkView result;
		try
		{
			NetMainPrefab._currentNetInstance = null;
			if (args.hasCustomInstantiator)
			{
				instance = null;
				try
				{
					instance = args.customInstantiate.CustomInstantiatePrefab(ref args);
				}
				catch (Exception arg)
				{
					Debug.LogError(string.Format("Thrown Exception during custom instantiate via '{0}' with instantiation '{2}'\r\ndefault instantiation will now occur --  exception follows..\r\n{1}", args.customInstantiate, arg, this), this);
					if (instance)
					{
						Object.Destroy(instance);
					}
					instance = null;
				}
				NetworkView networkView;
				try
				{
					networkView = instance.networkView;
					if (networkView == null)
					{
						Debug.LogWarning(string.Format("The custom instantiator '{0}' with instantiation '{1}' did not return a idmain with a network view. so its being added", args.customInstantiate, this), this);
						networkView = instance.gameObject.AddComponent<uLinkNetworkView>();
					}
				}
				catch (Exception arg2)
				{
					networkView = null;
					Debug.LogError(string.Format("The custom instantiator '{0}' did not instantiate a IDMain with a networkview or something else with instantiation '{2}'.. \r\n {1}", args.customInstantiate, arg2, this), this);
				}
				if (networkView)
				{
					return networkView;
				}
			}
			NetworkView networkView2 = (NetworkView)NetworkInstantiatorUtility.Instantiate(args.prefabNetworkView, args.args);
			instance = networkView2.GetComponent<IDMain>();
			result = networkView2;
		}
		finally
		{
			NetMainPrefab._currentNetInstance = currentNetInstance;
		}
		return result;
	}

	// Token: 0x06001E2D RID: 7725 RVA: 0x00076650 File Offset: 0x00074850
	private NetInstance Summon(IDMain prefab, bool isServer, ref NetworkInstantiateArgs niargs)
	{
		CustomInstantiationArgs args = new CustomInstantiationArgs(this, this._customInstantiator, prefab, ref niargs, isServer);
		IDMain idMain;
		NetworkView networkView = this.Create(ref args, out idMain);
		NetInstance netInstance = networkView.gameObject.AddComponent<NetInstance>();
		netInstance.args = args;
		netInstance.idMain = idMain;
		netInstance.prepared = false;
		netInstance.networkView = networkView;
		return netInstance;
	}

	// Token: 0x06001E2E RID: 7726 RVA: 0x000766A4 File Offset: 0x000748A4
	private bool ShouldDoStandardInitialization(NetInstance instance)
	{
		Object customInstantiator = this._customInstantiator;
		bool result;
		try
		{
			this._customInstantiator = instance;
			if (instance.args.hasCustomInstantiator)
			{
				try
				{
					return instance.args.customInstantiate.InitializePrefabInstance(instance);
				}
				catch (Exception ex)
				{
					Debug.LogError(string.Format("A exception was thrown during InitializePrefabInstance with '{0}' as custom instantiate, prefab '{1}' instance '{2}'.\r\ndoing standard initialization..\r\n{3}", new object[]
					{
						instance.args.customInstantiate,
						this,
						instance.args.prefab,
						ex
					}), instance);
				}
			}
			result = true;
		}
		finally
		{
			this._customInstantiator = customInstantiator;
		}
		return result;
	}

	// Token: 0x17000793 RID: 1939
	// (get) Token: 0x06001E2F RID: 7727 RVA: 0x00076774 File Offset: 0x00074974
	internal static NetInstance zzz__currentNetInstance
	{
		get
		{
			return NetMainPrefab._currentNetInstance;
		}
	}

	// Token: 0x06001E30 RID: 7728 RVA: 0x0007677C File Offset: 0x0007497C
	public static void IssueLocallyAppended(IDRemote appended, IDMain instance)
	{
		appended.BroadcastMessage("OnLocallyAppended", instance, 1);
	}

	// Token: 0x06001E31 RID: 7729 RVA: 0x0007678C File Offset: 0x0007498C
	protected virtual void StandardInitialization(bool didAppend, IDRemote appended, NetInstance instance, NetworkView view, ref NetworkMessageInfo info)
	{
		if (didAppend)
		{
			NetMainPrefab.IssueLocallyAppended(appended, instance.idMain);
		}
		if (this.ShouldDoStandardInitialization(instance))
		{
			NetworkInstantiatorUtility.BroadcastOnNetworkInstantiate(view, "uLink_OnNetworkInstantiate", info);
		}
	}

	// Token: 0x06001E32 RID: 7730 RVA: 0x000767BC File Offset: 0x000749BC
	public static IDRemote DoLocalAppend(IDRemote localAppend, IDMain instance, Transform appendPoint)
	{
		Transform transform = localAppend.transform;
		if (localAppend.transform != localAppend.transform.root)
		{
			Debug.LogWarning("The localAppend transform was not a root");
		}
		IDRemote idremote = (IDRemote)Object.Instantiate(localAppend, appendPoint.TransformPoint(transform.localPosition), appendPoint.rotation * transform.localRotation);
		Transform transform2 = idremote.transform;
		transform2.parent = appendPoint;
		transform2.localPosition = transform.localPosition;
		transform2.localRotation = transform.localRotation;
		transform2.localScale = transform.localScale;
		idremote.idMain = instance;
		foreach (IDRemote idremote2 in instance.GetComponentsInChildren<IDRemote>())
		{
			if (!idremote2.idMain)
			{
				idremote2.idMain = instance;
			}
		}
		return idremote;
	}

	// Token: 0x06001E33 RID: 7731 RVA: 0x00076898 File Offset: 0x00074A98
	protected NetworkView _Creator(string prefabName, NetworkInstantiateArgs args, NetworkMessageInfo info)
	{
		NetInstance netInstance = this.Summon(this.proxyPrefab, false, ref args);
		if (!netInstance)
		{
			return null;
		}
		NetworkView networkView = netInstance.networkView;
		if (!networkView)
		{
			return null;
		}
		info = new NetworkMessageInfo(info, networkView);
		NetInstance currentNetInstance = NetMainPrefab._currentNetInstance;
		try
		{
			NetMainPrefab._currentNetInstance = netInstance;
			netInstance.info = info;
			netInstance.prepared = true;
			NetInstance netInstance2 = netInstance;
			NetworkViewID viewID = args.viewID;
			netInstance2.local = viewID.isMine;
			bool didAppend = false;
			IDRemote appended = null;
			if (netInstance.local)
			{
				IDRemote localAppend = this.localAppend;
				if (localAppend)
				{
					appended = NetMainPrefab.DoLocalAppend(localAppend, netInstance.idMain, this.GetLocalAppendTransform(netInstance.idMain));
					didAppend = true;
				}
			}
			netInstance.zzz___onprecreate();
			this.StandardInitialization(didAppend, appended, netInstance, networkView, ref info);
			netInstance.zzz___onpostcreate();
		}
		finally
		{
			NetMainPrefab._currentNetInstance = currentNetInstance;
		}
		return networkView;
	}

	// Token: 0x06001E34 RID: 7732 RVA: 0x00076994 File Offset: 0x00074B94
	protected void _Destroyer(NetworkView networkView)
	{
		NetInstance currentNetInstance = NetMainPrefab._currentNetInstance;
		try
		{
			NetInstance component = networkView.GetComponent<NetInstance>();
			NetMainPrefab._currentNetInstance = component;
			if (component)
			{
				component.zzz___onpredestroy();
			}
			Object.Destroy(networkView.gameObject);
		}
		finally
		{
			NetMainPrefab._currentNetInstance = currentNetInstance;
		}
	}

	// Token: 0x06001E35 RID: 7733 RVA: 0x000769F8 File Offset: 0x00074BF8
	protected virtual void CollectCallbacks(out NetworkInstantiator.Creator creator, out NetworkInstantiator.Destroyer destroyer)
	{
		creator = new NetworkInstantiator.Creator(this._Creator);
		destroyer = new NetworkInstantiator.Destroyer(this._Destroyer);
	}

	// Token: 0x06001E36 RID: 7734 RVA: 0x00076A18 File Offset: 0x00074C18
	public static string DressName(string name)
	{
		if (string.IsNullOrEmpty(name))
		{
			throw new ArgumentException("name cannot be null or empty", "name");
		}
		if (name[0] != ':')
		{
			int length = name.Length;
			for (int i = length - 1; i >= 0; i--)
			{
				if (char.IsUpper(name, i))
				{
					Debug.LogWarning(string.Format("the name \":{0}\" contains upper case characters. it should not.", name));
					return ":" + name.ToLower();
				}
			}
			return ":" + name;
		}
		int length2 = name.Length;
		if (length2 == 1)
		{
			throw new ArgumentException("if name includes the prefix char it must be followed by at least one more char.", "name");
		}
		for (int j = length2 - 1; j > 0; j--)
		{
			if (char.IsUpper(name, j))
			{
				Debug.LogWarning(string.Format("the name \"{0}\" contains upper case characters. it should not.", name));
				return name.ToLower();
			}
		}
		string text = name.ToLower();
		if (text != name)
		{
			Debug.LogWarning(string.Format("the name \"{0}\" contains upper case characters. it should not.", name));
		}
		if (text[0] == ':')
		{
			return text;
		}
		return ":" + text;
	}

	// Token: 0x17000794 RID: 1940
	// (get) Token: 0x06001E37 RID: 7735 RVA: 0x00076B3C File Offset: 0x00074D3C
	public string name
	{
		get
		{
			string name = base.name;
			if (name != this._originalName)
			{
				if (Application.isPlaying && !string.IsNullOrEmpty(this._originalName))
				{
					Debug.LogWarning("You can't rename proxy instantiations at runtime!", this);
				}
				else
				{
					this._originalName = name;
					this._name = NetMainPrefab.DressName(name);
				}
			}
			return this._name;
		}
	}

	// Token: 0x06001E38 RID: 7736 RVA: 0x00076BA4 File Offset: 0x00074DA4
	public static T Lookup<T>(string key) where T : Object
	{
		if (!NetMainPrefab.ginit)
		{
			return (T)((object)null);
		}
		NetMainPrefab netMainPrefab;
		if (!NetMainPrefab.g.dict.TryGetValue(key, out netMainPrefab))
		{
			Debug.LogWarning("There was no registered proxy with key " + key);
			return (T)((object)null);
		}
		if (typeof(NetMainPrefab).IsAssignableFrom(typeof(T)))
		{
			return (T)((object)netMainPrefab);
		}
		if (typeof(GameObject).IsAssignableFrom(typeof(T)))
		{
			return (T)((object)netMainPrefab.prefab.gameObject);
		}
		if (!typeof(Component).IsAssignableFrom(typeof(T)))
		{
			return (T)((object)null);
		}
		if (typeof(IDMain).IsAssignableFrom(typeof(T)))
		{
			return (T)((object)netMainPrefab.prefab);
		}
		return (T)((object)netMainPrefab.prefab.GetComponent(typeof(T)));
	}

	// Token: 0x06001E39 RID: 7737 RVA: 0x00076CA8 File Offset: 0x00074EA8
	public static T LookupInChildren<T>(string key) where T : Component
	{
		if (!NetMainPrefab.ginit)
		{
			return (T)((object)null);
		}
		NetMainPrefab netMainPrefab;
		if (!NetMainPrefab.g.dict.TryGetValue(key, out netMainPrefab))
		{
			Debug.LogWarning("There was no registered proxy with key " + key);
			return (T)((object)null);
		}
		return netMainPrefab.prefab.GetComponentInChildren<T>();
	}

	// Token: 0x06001E3A RID: 7738 RVA: 0x00076CFC File Offset: 0x00074EFC
	public void Register(bool forceReplace)
	{
		NetworkInstantiator.Add(this.name, this.creator, this.destroyer, forceReplace);
		NetMainPrefab.g.dict[this.name] = this;
	}

	// Token: 0x06001E3B RID: 7739 RVA: 0x00076D34 File Offset: 0x00074F34
	public static void EnsurePrefabName(string name)
	{
		NetMainPrefabNameException ex;
		if (!NetMainPrefab.ValidatePrefabNameOrMakeException(name, out ex))
		{
			throw ex;
		}
	}

	// Token: 0x06001E3C RID: 7740 RVA: 0x00076D50 File Offset: 0x00074F50
	public static bool ValidatePrefabNameOrMakeException(string name, out NetMainPrefabNameException e)
	{
		if (name == null)
		{
			e = new NetMainPrefabNameException("name", name, "null");
		}
		else if (name.Length < 2)
		{
			e = new NetMainPrefabNameException("name", name, "name must include the prefix character and at least one other after");
		}
		else
		{
			if (name[0] == ':')
			{
				e = null;
				return true;
			}
			e = new NetMainPrefabNameException("name", name, "name did not begin with the prefix character");
		}
		return false;
	}

	// Token: 0x04000E80 RID: 3712
	public const char prefixChar = ':';

	// Token: 0x04000E81 RID: 3713
	public const string prefixCharString = ":";

	// Token: 0x04000E82 RID: 3714
	[SerializeField]
	private IDMain _proxyPrefab;

	// Token: 0x04000E83 RID: 3715
	[SerializeField]
	private IDMain _serverPrefab;

	// Token: 0x04000E84 RID: 3716
	[SerializeField]
	private IDMain _localPrefab;

	// Token: 0x04000E85 RID: 3717
	[SerializeField]
	private IDRemote _localAppend;

	// Token: 0x04000E86 RID: 3718
	[SerializeField]
	private string _pathToLocalAppend;

	// Token: 0x04000E87 RID: 3719
	[SerializeField]
	private Object _customInstantiator;

	// Token: 0x04000E88 RID: 3720
	[NonSerialized]
	public readonly Type MinimumTypeAllowed;

	// Token: 0x04000E89 RID: 3721
	private readonly NetworkInstantiator.Creator creator;

	// Token: 0x04000E8A RID: 3722
	private readonly NetworkInstantiator.Destroyer destroyer;

	// Token: 0x04000E8B RID: 3723
	private static NetInstance _currentNetInstance;

	// Token: 0x04000E8C RID: 3724
	private string _name;

	// Token: 0x04000E8D RID: 3725
	private string _originalName;

	// Token: 0x04000E8E RID: 3726
	private static bool ginit;

	// Token: 0x0200030F RID: 783
	private static class g
	{
		// Token: 0x06001E3D RID: 7741 RVA: 0x00076DC8 File Offset: 0x00074FC8
		static g()
		{
			NetMainPrefab.ginit = true;
		}

		// Token: 0x04000E8F RID: 3727
		public static Dictionary<string, NetMainPrefab> dict = new Dictionary<string, NetMainPrefab>();
	}
}
