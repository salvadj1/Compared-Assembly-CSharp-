using System;
using System.Collections.Generic;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x020003B7 RID: 951
public class NetMainPrefab : ScriptableObject
{
	// Token: 0x06002163 RID: 8547 RVA: 0x0007AD84 File Offset: 0x00078F84
	public NetMainPrefab() : this(typeof(IDMain), false)
	{
	}

	// Token: 0x06002164 RID: 8548 RVA: 0x0007AD98 File Offset: 0x00078F98
	protected NetMainPrefab(Type minimumType) : this(minimumType, true)
	{
	}

	// Token: 0x06002165 RID: 8549 RVA: 0x0007ADA4 File Offset: 0x00078FA4
	private NetMainPrefab(Type minimumType, bool typeCheck)
	{
		if (typeCheck && !typeof(IDMain).IsAssignableFrom(minimumType))
		{
			throw new ArgumentOutOfRangeException("minimumType", "must be assignable to IDMain");
		}
		this.MinimumTypeAllowed = minimumType;
		this.CollectCallbacks(out this.creator, out this.destroyer);
	}

	// Token: 0x170007E3 RID: 2019
	// (get) Token: 0x06002166 RID: 8550 RVA: 0x0007ADFC File Offset: 0x00078FFC
	private IDRemote localAppend
	{
		get
		{
			return this._localAppend;
		}
	}

	// Token: 0x170007E4 RID: 2020
	// (get) Token: 0x06002167 RID: 8551 RVA: 0x0007AE04 File Offset: 0x00079004
	public IDMain prefab
	{
		get
		{
			return this.proxyPrefab;
		}
	}

	// Token: 0x170007E5 RID: 2021
	// (get) Token: 0x06002168 RID: 8552 RVA: 0x0007AE0C File Offset: 0x0007900C
	public IDMain proxyPrefab
	{
		get
		{
			return (!this._proxyPrefab) ? this._serverPrefab : this._proxyPrefab;
		}
	}

	// Token: 0x170007E6 RID: 2022
	// (get) Token: 0x06002169 RID: 8553 RVA: 0x0007AE30 File Offset: 0x00079030
	public IDMain localPrefab
	{
		get
		{
			return (!this._localPrefab) ? this.proxyPrefab : this._localPrefab;
		}
	}

	// Token: 0x170007E7 RID: 2023
	// (get) Token: 0x0600216A RID: 8554 RVA: 0x0007AE54 File Offset: 0x00079054
	public IDMain serverPrefab
	{
		get
		{
			return (!this._serverPrefab) ? this.proxyPrefab : this._serverPrefab;
		}
	}

	// Token: 0x0600216B RID: 8555 RVA: 0x0007AE78 File Offset: 0x00079078
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

	// Token: 0x0600216C RID: 8556 RVA: 0x0007AEDC File Offset: 0x000790DC
	public Transform GetLocalAppendTransform(IDMain instanceOrPrefab)
	{
		return global::NetMainPrefab.GetLocalAppendTransform(instanceOrPrefab, this._pathToLocalAppend);
	}

	// Token: 0x170007E8 RID: 2024
	// (get) Token: 0x0600216D RID: 8557 RVA: 0x0007AEEC File Offset: 0x000790EC
	public Transform localAppendTransformInPrefab
	{
		get
		{
			return this.GetLocalAppendTransform(this.proxyPrefab);
		}
	}

	// Token: 0x0600216E RID: 8558 RVA: 0x0007AEFC File Offset: 0x000790FC
	private Facepunch.NetworkView Create(ref global::CustomInstantiationArgs args, out IDMain instance)
	{
		if (float.IsNaN(args.position.x) || float.IsNaN(args.position.y) || float.IsNaN(args.position.z))
		{
			Debug.LogWarning("NetMainPrefab -> Create -  args.position = " + args.position);
			Debug.LogWarning("This means you're creating an object with a bad position!");
		}
		global::NetInstance currentNetInstance = global::NetMainPrefab._currentNetInstance;
		Facepunch.NetworkView result;
		try
		{
			global::NetMainPrefab._currentNetInstance = null;
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
				Facepunch.NetworkView networkView;
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
			Facepunch.NetworkView networkView2 = (Facepunch.NetworkView)NetworkInstantiatorUtility.Instantiate(args.prefabNetworkView, args.args);
			instance = networkView2.GetComponent<IDMain>();
			result = networkView2;
		}
		finally
		{
			global::NetMainPrefab._currentNetInstance = currentNetInstance;
		}
		return result;
	}

	// Token: 0x0600216F RID: 8559 RVA: 0x0007B0D0 File Offset: 0x000792D0
	private global::NetInstance Summon(IDMain prefab, bool isServer, ref NetworkInstantiateArgs niargs)
	{
		global::CustomInstantiationArgs args = new global::CustomInstantiationArgs(this, this._customInstantiator, prefab, ref niargs, isServer);
		IDMain idMain;
		Facepunch.NetworkView networkView = this.Create(ref args, out idMain);
		global::NetInstance netInstance = networkView.gameObject.AddComponent<global::NetInstance>();
		netInstance.args = args;
		netInstance.idMain = idMain;
		netInstance.prepared = false;
		netInstance.networkView = networkView;
		return netInstance;
	}

	// Token: 0x06002170 RID: 8560 RVA: 0x0007B124 File Offset: 0x00079324
	private bool ShouldDoStandardInitialization(global::NetInstance instance)
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

	// Token: 0x170007E9 RID: 2025
	// (get) Token: 0x06002171 RID: 8561 RVA: 0x0007B1F4 File Offset: 0x000793F4
	internal static global::NetInstance zzz__currentNetInstance
	{
		get
		{
			return global::NetMainPrefab._currentNetInstance;
		}
	}

	// Token: 0x06002172 RID: 8562 RVA: 0x0007B1FC File Offset: 0x000793FC
	public static void IssueLocallyAppended(IDRemote appended, IDMain instance)
	{
		appended.BroadcastMessage("OnLocallyAppended", instance, 1);
	}

	// Token: 0x06002173 RID: 8563 RVA: 0x0007B20C File Offset: 0x0007940C
	protected virtual void StandardInitialization(bool didAppend, IDRemote appended, global::NetInstance instance, Facepunch.NetworkView view, ref uLink.NetworkMessageInfo info)
	{
		if (didAppend)
		{
			global::NetMainPrefab.IssueLocallyAppended(appended, instance.idMain);
		}
		if (this.ShouldDoStandardInitialization(instance))
		{
			NetworkInstantiatorUtility.BroadcastOnNetworkInstantiate(view, "uLink_OnNetworkInstantiate", info);
		}
	}

	// Token: 0x06002174 RID: 8564 RVA: 0x0007B23C File Offset: 0x0007943C
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

	// Token: 0x06002175 RID: 8565 RVA: 0x0007B318 File Offset: 0x00079518
	protected uLink.NetworkView _Creator(string prefabName, NetworkInstantiateArgs args, uLink.NetworkMessageInfo info)
	{
		global::NetInstance netInstance = this.Summon(this.proxyPrefab, false, ref args);
		if (!netInstance)
		{
			return null;
		}
		Facepunch.NetworkView networkView = netInstance.networkView;
		if (!networkView)
		{
			return null;
		}
		info = new uLink.NetworkMessageInfo(info, networkView);
		global::NetInstance currentNetInstance = global::NetMainPrefab._currentNetInstance;
		try
		{
			global::NetMainPrefab._currentNetInstance = netInstance;
			netInstance.info = info;
			netInstance.prepared = true;
			global::NetInstance netInstance2 = netInstance;
			uLink.NetworkViewID viewID = args.viewID;
			netInstance2.local = viewID.isMine;
			bool didAppend = false;
			IDRemote appended = null;
			if (netInstance.local)
			{
				IDRemote localAppend = this.localAppend;
				if (localAppend)
				{
					appended = global::NetMainPrefab.DoLocalAppend(localAppend, netInstance.idMain, this.GetLocalAppendTransform(netInstance.idMain));
					didAppend = true;
				}
			}
			netInstance.zzz___onprecreate();
			this.StandardInitialization(didAppend, appended, netInstance, networkView, ref info);
			netInstance.zzz___onpostcreate();
		}
		finally
		{
			global::NetMainPrefab._currentNetInstance = currentNetInstance;
		}
		return networkView;
	}

	// Token: 0x06002176 RID: 8566 RVA: 0x0007B414 File Offset: 0x00079614
	protected void _Destroyer(uLink.NetworkView networkView)
	{
		global::NetInstance currentNetInstance = global::NetMainPrefab._currentNetInstance;
		try
		{
			global::NetInstance component = networkView.GetComponent<global::NetInstance>();
			global::NetMainPrefab._currentNetInstance = component;
			if (component)
			{
				component.zzz___onpredestroy();
			}
			Object.Destroy(networkView.gameObject);
		}
		finally
		{
			global::NetMainPrefab._currentNetInstance = currentNetInstance;
		}
	}

	// Token: 0x06002177 RID: 8567 RVA: 0x0007B478 File Offset: 0x00079678
	protected virtual void CollectCallbacks(out NetworkInstantiator.Creator creator, out NetworkInstantiator.Destroyer destroyer)
	{
		creator = new NetworkInstantiator.Creator(this._Creator);
		destroyer = new NetworkInstantiator.Destroyer(this._Destroyer);
	}

	// Token: 0x06002178 RID: 8568 RVA: 0x0007B498 File Offset: 0x00079698
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

	// Token: 0x170007EA RID: 2026
	// (get) Token: 0x06002179 RID: 8569 RVA: 0x0007B5BC File Offset: 0x000797BC
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
					this._name = global::NetMainPrefab.DressName(name);
				}
			}
			return this._name;
		}
	}

	// Token: 0x0600217A RID: 8570 RVA: 0x0007B624 File Offset: 0x00079824
	public static T Lookup<T>(string key) where T : Object
	{
		if (!global::NetMainPrefab.ginit)
		{
			return (T)((object)null);
		}
		global::NetMainPrefab netMainPrefab;
		if (!global::NetMainPrefab.g.dict.TryGetValue(key, out netMainPrefab))
		{
			Debug.LogWarning("There was no registered proxy with key " + key);
			return (T)((object)null);
		}
		if (typeof(global::NetMainPrefab).IsAssignableFrom(typeof(T)))
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

	// Token: 0x0600217B RID: 8571 RVA: 0x0007B728 File Offset: 0x00079928
	public static T LookupInChildren<T>(string key) where T : Component
	{
		if (!global::NetMainPrefab.ginit)
		{
			return (T)((object)null);
		}
		global::NetMainPrefab netMainPrefab;
		if (!global::NetMainPrefab.g.dict.TryGetValue(key, out netMainPrefab))
		{
			Debug.LogWarning("There was no registered proxy with key " + key);
			return (T)((object)null);
		}
		return netMainPrefab.prefab.GetComponentInChildren<T>();
	}

	// Token: 0x0600217C RID: 8572 RVA: 0x0007B77C File Offset: 0x0007997C
	public void Register(bool forceReplace)
	{
		NetworkInstantiator.Add(this.name, this.creator, this.destroyer, forceReplace);
		global::NetMainPrefab.g.dict[this.name] = this;
	}

	// Token: 0x0600217D RID: 8573 RVA: 0x0007B7B4 File Offset: 0x000799B4
	public static void EnsurePrefabName(string name)
	{
		global::NetMainPrefabNameException ex;
		if (!global::NetMainPrefab.ValidatePrefabNameOrMakeException(name, out ex))
		{
			throw ex;
		}
	}

	// Token: 0x0600217E RID: 8574 RVA: 0x0007B7D0 File Offset: 0x000799D0
	public static bool ValidatePrefabNameOrMakeException(string name, out global::NetMainPrefabNameException e)
	{
		if (name == null)
		{
			e = new global::NetMainPrefabNameException("name", name, "null");
		}
		else if (name.Length < 2)
		{
			e = new global::NetMainPrefabNameException("name", name, "name must include the prefix character and at least one other after");
		}
		else
		{
			if (name[0] == ':')
			{
				e = null;
				return true;
			}
			e = new global::NetMainPrefabNameException("name", name, "name did not begin with the prefix character");
		}
		return false;
	}

	// Token: 0x04000FC0 RID: 4032
	public const char prefixChar = ':';

	// Token: 0x04000FC1 RID: 4033
	public const string prefixCharString = ":";

	// Token: 0x04000FC2 RID: 4034
	[SerializeField]
	private IDMain _proxyPrefab;

	// Token: 0x04000FC3 RID: 4035
	[SerializeField]
	private IDMain _serverPrefab;

	// Token: 0x04000FC4 RID: 4036
	[SerializeField]
	private IDMain _localPrefab;

	// Token: 0x04000FC5 RID: 4037
	[SerializeField]
	private IDRemote _localAppend;

	// Token: 0x04000FC6 RID: 4038
	[SerializeField]
	private string _pathToLocalAppend;

	// Token: 0x04000FC7 RID: 4039
	[SerializeField]
	private Object _customInstantiator;

	// Token: 0x04000FC8 RID: 4040
	[NonSerialized]
	public readonly Type MinimumTypeAllowed;

	// Token: 0x04000FC9 RID: 4041
	private readonly NetworkInstantiator.Creator creator;

	// Token: 0x04000FCA RID: 4042
	private readonly NetworkInstantiator.Destroyer destroyer;

	// Token: 0x04000FCB RID: 4043
	private static global::NetInstance _currentNetInstance;

	// Token: 0x04000FCC RID: 4044
	private string _name;

	// Token: 0x04000FCD RID: 4045
	private string _originalName;

	// Token: 0x04000FCE RID: 4046
	private static bool ginit;

	// Token: 0x020003B8 RID: 952
	private static class g
	{
		// Token: 0x0600217F RID: 8575 RVA: 0x0007B848 File Offset: 0x00079A48
		static g()
		{
			global::NetMainPrefab.ginit = true;
		}

		// Token: 0x04000FCF RID: 4047
		public static Dictionary<string, global::NetMainPrefab> dict = new Dictionary<string, global::NetMainPrefab>();
	}
}
