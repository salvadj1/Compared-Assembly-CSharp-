using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Facepunch;
using Facepunch.Load;
using Facepunch.Load.Downloaders;
using Facepunch.Traits;
using UnityEngine;

// Token: 0x0200002E RID: 46
public class RustLoader : MonoBehaviour, IRustLoaderTasks
{
	// Token: 0x17000052 RID: 82
	// (get) Token: 0x060001DD RID: 477 RVA: 0x0000B194 File Offset: 0x00009394
	bool IRustLoaderTasks.Active
	{
		get
		{
			return this.loader != null;
		}
	}

	// Token: 0x17000053 RID: 83
	// (get) Token: 0x060001DE RID: 478 RVA: 0x0000B1A4 File Offset: 0x000093A4
	IDownloadTask IRustLoaderTasks.Overall
	{
		get
		{
			return this.loader;
		}
	}

	// Token: 0x17000054 RID: 84
	// (get) Token: 0x060001DF RID: 479 RVA: 0x0000B1AC File Offset: 0x000093AC
	IEnumerable<IDownloadTask> IRustLoaderTasks.Groups
	{
		get
		{
			if (this.loader == null)
			{
				yield break;
			}
			foreach (Group group in this.loader.Groups)
			{
				yield return group;
			}
			yield break;
		}
	}

	// Token: 0x17000055 RID: 85
	// (get) Token: 0x060001E0 RID: 480 RVA: 0x0000B1D0 File Offset: 0x000093D0
	IDownloadTask IRustLoaderTasks.ActiveGroup
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

	// Token: 0x17000056 RID: 86
	// (get) Token: 0x060001E1 RID: 481 RVA: 0x0000B1EC File Offset: 0x000093EC
	IEnumerable<IDownloadTask> IRustLoaderTasks.ActiveJobs
	{
		get
		{
			if (this.loader == null)
			{
				yield break;
			}
			Group currentGroup = this.loader.CurrentGroup;
			if (currentGroup == null)
			{
				yield break;
			}
			foreach (Job task in currentGroup.Jobs)
			{
				yield return task;
			}
			yield break;
		}
	}

	// Token: 0x17000057 RID: 87
	// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000B210 File Offset: 0x00009410
	IEnumerable<IDownloadTask> IRustLoaderTasks.Jobs
	{
		get
		{
			if (this.loader == null)
			{
				yield break;
			}
			foreach (Job task in this.loader.Jobs)
			{
				yield return task;
			}
			yield break;
		}
	}

	// Token: 0x060001E3 RID: 483 RVA: 0x0000B234 File Offset: 0x00009434
	public void ServerInit()
	{
		Object.Destroy(base.GetComponent<RustLoaderInstantiateOnComplete>());
	}

	// Token: 0x17000058 RID: 88
	// (get) Token: 0x060001E4 RID: 484 RVA: 0x0000B244 File Offset: 0x00009444
	public IRustLoaderTasks Tasks
	{
		get
		{
			return this;
		}
	}

	// Token: 0x060001E5 RID: 485 RVA: 0x0000B248 File Offset: 0x00009448
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

	// Token: 0x060001E6 RID: 486 RVA: 0x0000B294 File Offset: 0x00009494
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

	// Token: 0x060001E7 RID: 487 RVA: 0x0000B308 File Offset: 0x00009508
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

	// Token: 0x060001E8 RID: 488 RVA: 0x0000B354 File Offset: 0x00009554
	private void Callback_OnBundleLoaded(AssetBundle bundle, Item item)
	{
		this.DispatchLoadMessage("OnRustBundleLoaded", this);
	}

	// Token: 0x060001E9 RID: 489 RVA: 0x0000B364 File Offset: 0x00009564
	private void Callback_OnBundleGroupLoaded(AssetBundle[] bundles, Item[] items)
	{
		this.DispatchLoadMessage("OnRustBundleGroupLoaded", this);
	}

	// Token: 0x060001EA RID: 490 RVA: 0x0000B374 File Offset: 0x00009574
	private void Callback_OnBundleAllLoaded(AssetBundle[] bundles, Item[] items)
	{
		this.DispatchLoadMessage("OnRustBundleCompleteLoaded", this);
	}

	// Token: 0x060001EB RID: 491 RVA: 0x0000B384 File Offset: 0x00009584
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
		this.loader = Loader.CreateFromText(loaderText, bundleDirectory, new FileDispatch());
		Bundling.BindToLoader(this.loader);
		Bundling.OnceLoaded += RustLoader.OnResourcesLoaded;
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
		yield return Resources.UnloadUnusedAssets();
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

	// Token: 0x060001EC RID: 492 RVA: 0x0000B3A0 File Offset: 0x000095A0
	private static void OnResourcesLoaded()
	{
		foreach (BaseTraitMap baseTraitMap in Bundling.LoadAll<BaseTraitMap>())
		{
			if (baseTraitMap)
			{
				try
				{
					Binder.BindMap(baseTraitMap);
				}
				catch (Exception ex)
				{
					Debug.LogError(ex, baseTraitMap);
				}
			}
		}
		DatablockDictionary.Initialize();
		foreach (NetMainPrefab netMainPrefab in Bundling.LoadAll<NetMainPrefab>())
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
		foreach (uLinkNetworkView uLinkNetworkView in Bundling.LoadAll<uLinkNetworkView>())
		{
			try
			{
				NetCull.RegisterNetAutoPrefab(uLinkNetworkView);
			}
			catch (Exception ex3)
			{
				Debug.LogException(ex3, uLinkNetworkView);
			}
		}
		NGC.Register(NGCConfiguration.Load());
	}

	// Token: 0x04000126 RID: 294
	[SerializeField]
	public string releaseBundleLoaderFilePath = "bundles/manifest.txt";

	// Token: 0x04000127 RID: 295
	[SerializeField]
	public GameObject[] messageReceivers;

	// Token: 0x04000128 RID: 296
	[NonSerialized]
	private Loader loader;

	// Token: 0x04000129 RID: 297
	public bool destroyGameObjectOnceLoaded;

	// Token: 0x0400012A RID: 298
	[NonSerialized]
	private string preloadedJsonLoaderText;

	// Token: 0x0400012B RID: 299
	[NonSerialized]
	private string preloadedJsonLoaderRoot;

	// Token: 0x0400012C RID: 300
	[NonSerialized]
	private bool preloadedJsonLoader;

	// Token: 0x0400012D RID: 301
	[NonSerialized]
	private string preloadedJsonLoaderError;
}
