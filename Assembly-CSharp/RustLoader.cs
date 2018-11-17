using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Facepunch;
using Facepunch.Load;
using Facepunch.Load.Downloaders;
using Facepunch.Traits;
using UnityEngine;

// Token: 0x0200003C RID: 60
public class RustLoader : MonoBehaviour, global::IRustLoaderTasks
{
	// Token: 0x17000060 RID: 96
	// (get) Token: 0x06000231 RID: 561 RVA: 0x0000C01C File Offset: 0x0000A21C
	bool global::IRustLoaderTasks.Active
	{
		get
		{
			return this.loader != null;
		}
	}

	// Token: 0x17000061 RID: 97
	// (get) Token: 0x06000232 RID: 562 RVA: 0x0000C02C File Offset: 0x0000A22C
	Facepunch.Load.IDownloadTask global::IRustLoaderTasks.Overall
	{
		get
		{
			return this.loader;
		}
	}

	// Token: 0x17000062 RID: 98
	// (get) Token: 0x06000233 RID: 563 RVA: 0x0000C034 File Offset: 0x0000A234
	IEnumerable<Facepunch.Load.IDownloadTask> global::IRustLoaderTasks.Groups
	{
		get
		{
			if (this.loader == null)
			{
				yield break;
			}
			foreach (Facepunch.Load.Group group in this.loader.Groups)
			{
				yield return group;
			}
			yield break;
		}
	}

	// Token: 0x17000063 RID: 99
	// (get) Token: 0x06000234 RID: 564 RVA: 0x0000C058 File Offset: 0x0000A258
	Facepunch.Load.IDownloadTask global::IRustLoaderTasks.ActiveGroup
	{
		get
		{
			if (this.loader == null)
			{
				return null;
			}
			return this.loader.CurrentGroup;
		}
	}

	// Token: 0x17000064 RID: 100
	// (get) Token: 0x06000235 RID: 565 RVA: 0x0000C074 File Offset: 0x0000A274
	IEnumerable<Facepunch.Load.IDownloadTask> global::IRustLoaderTasks.ActiveJobs
	{
		get
		{
			if (this.loader == null)
			{
				yield break;
			}
			Facepunch.Load.Group currentGroup = this.loader.CurrentGroup;
			if (currentGroup == null)
			{
				yield break;
			}
			foreach (Facepunch.Load.Job task in currentGroup.Jobs)
			{
				yield return task;
			}
			yield break;
		}
	}

	// Token: 0x17000065 RID: 101
	// (get) Token: 0x06000236 RID: 566 RVA: 0x0000C098 File Offset: 0x0000A298
	IEnumerable<Facepunch.Load.IDownloadTask> global::IRustLoaderTasks.Jobs
	{
		get
		{
			if (this.loader == null)
			{
				yield break;
			}
			foreach (Facepunch.Load.Job task in this.loader.Jobs)
			{
				yield return task;
			}
			yield break;
		}
	}

	// Token: 0x06000237 RID: 567 RVA: 0x0000C0BC File Offset: 0x0000A2BC
	public void ServerInit()
	{
		Object.Destroy(base.GetComponent<global::RustLoaderInstantiateOnComplete>());
	}

	// Token: 0x17000066 RID: 102
	// (get) Token: 0x06000238 RID: 568 RVA: 0x0000C0CC File Offset: 0x0000A2CC
	public global::IRustLoaderTasks Tasks
	{
		get
		{
			return this;
		}
	}

	// Token: 0x06000239 RID: 569 RVA: 0x0000C0D0 File Offset: 0x0000A2D0
	public void SetPreloadedManifest(string text, string path, string error)
	{
		if (this.loader != null)
		{
			throw new InvalidOperationException("The loader has already begun. Its too late!");
		}
		this.preloadedJsonLoaderText = text;
		this.preloadedJsonLoaderRoot = (path ?? string.Empty);
		this.preloadedJsonLoader = true;
		this.preloadedJsonLoaderError = error;
	}

	// Token: 0x0600023A RID: 570 RVA: 0x0000C11C File Offset: 0x0000A31C
	public void AddMessageReceiver(GameObject newReceiver)
	{
		if (!newReceiver)
		{
			return;
		}
		if (this.messageReceivers == null)
		{
			this.messageReceivers = new GameObject[]
			{
				newReceiver
			};
		}
		else if (Array.IndexOf<GameObject>(this.messageReceivers, newReceiver) == -1)
		{
			Array.Resize<GameObject>(ref this.messageReceivers, this.messageReceivers.Length + 1);
			this.messageReceivers[this.messageReceivers.Length - 1] = newReceiver;
		}
	}

	// Token: 0x0600023B RID: 571 RVA: 0x0000C190 File Offset: 0x0000A390
	private void DispatchLoadMessage(string message, object value)
	{
		if (this.messageReceivers != null)
		{
			foreach (GameObject gameObject in this.messageReceivers)
			{
				if (gameObject)
				{
					gameObject.SendMessage(message, value, 1);
				}
			}
		}
	}

	// Token: 0x0600023C RID: 572 RVA: 0x0000C1DC File Offset: 0x0000A3DC
	private void Callback_OnBundleLoaded(AssetBundle bundle, Facepunch.Load.Item item)
	{
		this.DispatchLoadMessage("OnRustBundleLoaded", this);
	}

	// Token: 0x0600023D RID: 573 RVA: 0x0000C1EC File Offset: 0x0000A3EC
	private void Callback_OnBundleGroupLoaded(AssetBundle[] bundles, Facepunch.Load.Item[] items)
	{
		this.DispatchLoadMessage("OnRustBundleGroupLoaded", this);
	}

	// Token: 0x0600023E RID: 574 RVA: 0x0000C1FC File Offset: 0x0000A3FC
	private void Callback_OnBundleAllLoaded(AssetBundle[] bundles, Facepunch.Load.Item[] items)
	{
		this.DispatchLoadMessage("OnRustBundleCompleteLoaded", this);
	}

	// Token: 0x0600023F RID: 575 RVA: 0x0000C20C File Offset: 0x0000A40C
	private IEnumerator Start()
	{
		this.DispatchLoadMessage("OnRustBundleFetching", this);
		string loaderText;
		string bundleDirectory;
		string loaderError;
		if (this.preloadedJsonLoader)
		{
			loaderText = this.preloadedJsonLoaderText;
			bundleDirectory = (this.preloadedJsonLoaderRoot ?? string.Empty);
			loaderError = this.preloadedJsonLoaderError;
			this.preloadedJsonLoaderText = null;
			this.preloadedJsonLoaderRoot = null;
			this.preloadedJsonLoaderError = null;
		}
		else
		{
			bundleDirectory = this.releaseBundleLoaderFilePath.Remove(this.releaseBundleLoaderFilePath.LastIndexOfAny(new char[]
			{
				'\\',
				'/'
			}) + 1);
			try
			{
				loaderText = File.ReadAllText(this.releaseBundleLoaderFilePath);
				loaderError = string.Empty;
			}
			catch (Exception ex)
			{
				Exception e = ex;
				loaderText = string.Empty;
				loaderError = e.ToString();
				if (string.IsNullOrEmpty(loaderError))
				{
					loaderError = "Failed";
				}
			}
		}
		if (!string.IsNullOrEmpty(loaderError))
		{
			Debug.LogError(loaderError);
			yield break;
		}
		this.loader = Facepunch.Load.Loader.CreateFromText(loaderText, bundleDirectory, new Facepunch.Load.Downloaders.FileDispatch());
		Facepunch.Bundling.BindToLoader(this.loader);
		Facepunch.Bundling.OnceLoaded += global::RustLoader.OnResourcesLoaded;
		this.DispatchLoadMessage("OnRustBundlePreLoad", this);
		this.loader.StartLoading();
		this.DispatchLoadMessage("OnRustBundleLoadStart", this);
		yield return base.StartCoroutine(this.loader.WaitEnumerator);
		this.DispatchLoadMessage("OnRustBundleLoadComplete", this);
		try
		{
			this.loader.Dispose();
		}
		catch (Exception ex2)
		{
			Exception e2 = ex2;
			Debug.LogException(e2);
		}
		finally
		{
			this.loader = null;
		}
		yield return UnityEngine.Resources.UnloadUnusedAssets();
		base.BroadcastMessage("OnRustLoadedFirst", 1);
		this.DispatchLoadMessage("OnRustLoaded", this);
		this.DispatchLoadMessage("OnRustReady", this);
		if (this.destroyGameObjectOnceLoaded)
		{
			Object.Destroy(base.gameObject);
		}
		else
		{
			Object.Destroy(this);
		}
		yield break;
	}

	// Token: 0x06000240 RID: 576 RVA: 0x0000C228 File Offset: 0x0000A428
	private static void OnResourcesLoaded()
	{
		foreach (global::BaseTraitMap baseTraitMap in Facepunch.Bundling.LoadAll<global::BaseTraitMap>())
		{
			if (baseTraitMap)
			{
				try
				{
					Facepunch.Traits.Binder.BindMap(baseTraitMap);
				}
				catch (Exception ex)
				{
					Debug.LogError(ex, baseTraitMap);
				}
			}
		}
		global::DatablockDictionary.Initialize();
		foreach (global::NetMainPrefab netMainPrefab in Facepunch.Bundling.LoadAll<global::NetMainPrefab>())
		{
			try
			{
				netMainPrefab.Register(true);
			}
			catch (Exception ex2)
			{
				Debug.LogException(ex2, netMainPrefab);
			}
		}
		foreach (uLinkNetworkView uLinkNetworkView in Facepunch.Bundling.LoadAll<uLinkNetworkView>())
		{
			try
			{
				global::NetCull.RegisterNetAutoPrefab(uLinkNetworkView);
			}
			catch (Exception ex3)
			{
				Debug.LogException(ex3, uLinkNetworkView);
			}
		}
		global::NGC.Register(global::NGCConfiguration.Load());
	}

	// Token: 0x0400016D RID: 365
	[SerializeField]
	public string releaseBundleLoaderFilePath = "bundles/manifest.txt";

	// Token: 0x0400016E RID: 366
	[SerializeField]
	public GameObject[] messageReceivers;

	// Token: 0x0400016F RID: 367
	[NonSerialized]
	private Facepunch.Load.Loader loader;

	// Token: 0x04000170 RID: 368
	public bool destroyGameObjectOnceLoaded;

	// Token: 0x04000171 RID: 369
	[NonSerialized]
	private string preloadedJsonLoaderText;

	// Token: 0x04000172 RID: 370
	[NonSerialized]
	private string preloadedJsonLoaderRoot;

	// Token: 0x04000173 RID: 371
	[NonSerialized]
	private bool preloadedJsonLoader;

	// Token: 0x04000174 RID: 372
	[NonSerialized]
	private string preloadedJsonLoaderError;
}
