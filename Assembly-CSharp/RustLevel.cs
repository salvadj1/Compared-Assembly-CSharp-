using System;
using System.Collections;
using System.Collections.Generic;
using Facepunch.Progress;
using Rust;
using UnityEngine;

// Token: 0x020003E1 RID: 993
public static class RustLevel
{
	// Token: 0x060024E1 RID: 9441 RVA: 0x0008D7E0 File Offset: 0x0008B9E0
	public static Coroutine Load(string levelName, out GameObject loader)
	{
		Globals.currentLevel = levelName;
		loader = new GameObject("Loading Level:" + levelName, new Type[]
		{
			typeof(MonoBehaviour)
		});
		Object.DontDestroyOnLoad(loader);
		MonoBehaviour component = loader.GetComponent<MonoBehaviour>();
		return component.StartCoroutine(RustLevel.LoadRoutine(component, levelName));
	}

	// Token: 0x060024E2 RID: 9442 RVA: 0x0008D834 File Offset: 0x0008BA34
	public static Coroutine Load(string levelName)
	{
		GameObject gameObject;
		return RustLevel.Load(levelName, out gameObject);
	}

	// Token: 0x060024E3 RID: 9443 RVA: 0x0008D84C File Offset: 0x0008BA4C
	private static IEnumerator LoadRoutine(MonoBehaviour script, string levelName)
	{
		RustLevel.LevelLoadLog(1);
		Globals.isLoading = true;
		Application.backgroundLoadingPriority = 0;
		LoadingScreen.Update("loading " + levelName);
		NetCull.isMessageQueueRunning = false;
		HudEnabled.Disable();
		AsyncOperation async = Application.LoadLevelAsync(levelName);
		async.allowSceneActivation = false;
		LoadingScreen.Operations.Add(async);
		while (async.progress < 0.9f)
		{
			yield return new WaitForSeconds(0.2f);
		}
		LoadingScreen.Operations.Clear();
		LoadingScreen.Update("activating " + levelName);
		yield return new WaitForSeconds(0.2f);
		async.allowSceneActivation = true;
		yield return async;
		RustLevel.LevelLoadLog(2);
		if (Application.CanStreamedLevelBeLoaded(levelName + "-TREES"))
		{
			LoadingScreen.Update("loading trees");
			AsyncOperation async2 = Application.LoadLevelAdditiveAsync(levelName + "-TREES");
			LoadingScreen.Operations.Add(async2);
			yield return async2;
			LoadingScreen.Operations.Clear();
		}
		else
		{
			Debug.Log("No tree level found.");
		}
		yield return new WaitForEndOfFrame();
		RustLevel.LevelLoadLog(3);
		LoadingScreen.Update("loading gui");
		AsyncOperation async3 = Application.LoadLevelAdditiveAsync("RPOS");
		LoadingScreen.Operations.Add(async3);
		yield return async3;
		LoadingScreen.Operations.Clear();
		LoadingScreen.Update("loading shared");
		AsyncOperation async4 = Application.LoadLevelAdditiveAsync("LevelShared");
		LoadingScreen.Operations.Add(async4);
		yield return async4;
		LoadingScreen.Operations.Clear();
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		RustLevel.LevelLoadLog(4);
		NetCull.isMessageQueueRunning = true;
		yield return new WaitForEndOfFrame();
		LoadingScreen.Update("growing trees");
		yield return new WaitForEndOfFrame();
		LoadingScreen.Operations.AddMultiple<IProgress>(ThrottledTask.AllWorkingTasksProgress);
		while (ThrottledTask.Operational)
		{
			yield return new WaitForSeconds(0.1f);
		}
		LoadingScreen.Operations.Clear();
		RustLevel.LevelLoadLog(7);
		LoadingScreen.Update("waiting for server");
		yield return new WaitForSeconds(0.1f);
		yield return RustLevel.WaitForCondition(script, () => ServerManagement.Get() != null, "ServerManagement.Get()");
		RustLevel.LevelLoadLog(8);
		LoadingScreen.Update("becoming ready");
		ServerManagement.Get().LocalClientPoliteReady();
		LoadingScreen.Update("waiting for character");
		yield return RustLevel.WaitForCondition(script, () => Controllable.localPlayerControllableExists, "Controllable.localPlayerControllableExists == true");
		RustLevel.LevelLoadLog(9);
		ConsoleSystem.Run("gameui.hide", false);
		LoadingScreen.Update("finished");
		HudEnabled.Enable();
		RustLevel.LevelLoadLog(10);
		Globals.isLoading = false;
		Object.Destroy(script.gameObject);
		yield break;
	}

	// Token: 0x060024E4 RID: 9444 RVA: 0x0008D87C File Offset: 0x0008BA7C
	public static void LevelLoadLog(byte iStage)
	{
	}

	// Token: 0x060024E5 RID: 9445 RVA: 0x0008D880 File Offset: 0x0008BA80
	private static Coroutine WaitForCondition(MonoBehaviour script, Func<bool> condition, string requestLabel)
	{
		return script.StartCoroutine(RustLevel.WaitForCondition(condition, requestLabel));
	}

	// Token: 0x060024E6 RID: 9446 RVA: 0x0008D890 File Offset: 0x0008BA90
	private static IEnumerator WaitForCondition(Func<bool> condition, string requestLabel)
	{
		if (!condition())
		{
			ulong counter = 0UL;
			do
			{
				if ((counter += 1UL) % 50UL == 0UL)
				{
					Debug.LogWarning(string.Concat(new object[]
					{
						"condition still not met:",
						requestLabel,
						" ( ",
						counter,
						" frames later )"
					}));
				}
				yield return new WaitForEndOfFrame();
			}
			while (!condition());
			if ((counter += 1UL) > 50UL)
			{
				Debug.LogWarning(string.Concat(new object[]
				{
					"Took ",
					counter,
					" additional frame(s) for condition ",
					requestLabel
				}));
			}
		}
		yield break;
	}

	// Token: 0x060024E7 RID: 9447 RVA: 0x0008D8C0 File Offset: 0x0008BAC0
	private static List<GameObject> CollectRootGameObjects()
	{
		HashSet<Transform> hashSet = new HashSet<Transform>();
		List<GameObject> list = new List<GameObject>();
		foreach (Object @object in Object.FindObjectsOfType(typeof(Transform)))
		{
			if (@object)
			{
				Transform root = ((Transform)@object).root;
				if (hashSet.Add(root))
				{
					list.Add(root.gameObject);
				}
			}
		}
		return list;
	}

	// Token: 0x060024E8 RID: 9448 RVA: 0x0008D940 File Offset: 0x0008BB40
	private static void BroadcastGlobalMessage(string messageName)
	{
		foreach (GameObject gameObject in RustLevel.CollectRootGameObjects())
		{
			if (gameObject)
			{
				gameObject.BroadcastMessage(messageName, 1);
			}
		}
	}
}
