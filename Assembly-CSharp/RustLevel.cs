using System;
using System.Collections;
using System.Collections.Generic;
using Facepunch.Progress;
using Rust;
using UnityEngine;

// Token: 0x0200048F RID: 1167
public static class RustLevel
{
	// Token: 0x06002845 RID: 10309 RVA: 0x00092BCC File Offset: 0x00090DCC
	public static Coroutine Load(string levelName, out GameObject loader)
	{
		Rust.Globals.currentLevel = levelName;
		loader = new GameObject("Loading Level:" + levelName, new Type[]
		{
			typeof(MonoBehaviour)
		});
		Object.DontDestroyOnLoad(loader);
		MonoBehaviour component = loader.GetComponent<MonoBehaviour>();
		return component.StartCoroutine(global::RustLevel.LoadRoutine(component, levelName));
	}

	// Token: 0x06002846 RID: 10310 RVA: 0x00092C20 File Offset: 0x00090E20
	public static Coroutine Load(string levelName)
	{
		GameObject gameObject;
		return global::RustLevel.Load(levelName, out gameObject);
	}

	// Token: 0x06002847 RID: 10311 RVA: 0x00092C38 File Offset: 0x00090E38
	private static IEnumerator LoadRoutine(MonoBehaviour script, string levelName)
	{
		global::RustLevel.LevelLoadLog(1);
		Rust.Globals.isLoading = true;
		Application.backgroundLoadingPriority = 0;
		global::LoadingScreen.Update("loading " + levelName);
		global::NetCull.isMessageQueueRunning = false;
		global::HudEnabled.Disable();
		AsyncOperation async = Application.LoadLevelAsync(levelName);
		async.allowSceneActivation = false;
		global::LoadingScreen.Operations.Add(async);
		while (async.progress < 0.9f)
		{
			yield return new WaitForSeconds(0.2f);
		}
		global::LoadingScreen.Operations.Clear();
		global::LoadingScreen.Update("activating " + levelName);
		yield return new WaitForSeconds(0.2f);
		async.allowSceneActivation = true;
		yield return async;
		global::RustLevel.LevelLoadLog(2);
		if (Application.CanStreamedLevelBeLoaded(levelName + "-TREES"))
		{
			global::LoadingScreen.Update("loading trees");
			AsyncOperation async2 = Application.LoadLevelAdditiveAsync(levelName + "-TREES");
			global::LoadingScreen.Operations.Add(async2);
			yield return async2;
			global::LoadingScreen.Operations.Clear();
		}
		else
		{
			Debug.Log("No tree level found.");
		}
		yield return new WaitForEndOfFrame();
		global::RustLevel.LevelLoadLog(3);
		global::LoadingScreen.Update("loading gui");
		AsyncOperation async3 = Application.LoadLevelAdditiveAsync("RPOS");
		global::LoadingScreen.Operations.Add(async3);
		yield return async3;
		global::LoadingScreen.Operations.Clear();
		global::LoadingScreen.Update("loading shared");
		AsyncOperation async4 = Application.LoadLevelAdditiveAsync("LevelShared");
		global::LoadingScreen.Operations.Add(async4);
		yield return async4;
		global::LoadingScreen.Operations.Clear();
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		global::RustLevel.LevelLoadLog(4);
		global::NetCull.isMessageQueueRunning = true;
		yield return new WaitForEndOfFrame();
		global::LoadingScreen.Update("growing trees");
		yield return new WaitForEndOfFrame();
		global::LoadingScreen.Operations.AddMultiple<Facepunch.Progress.IProgress>(global::ThrottledTask.AllWorkingTasksProgress);
		while (global::ThrottledTask.Operational)
		{
			yield return new WaitForSeconds(0.1f);
		}
		global::LoadingScreen.Operations.Clear();
		global::RustLevel.LevelLoadLog(7);
		global::LoadingScreen.Update("waiting for server");
		yield return new WaitForSeconds(0.1f);
		yield return global::RustLevel.WaitForCondition(script, () => global::ServerManagement.Get() != null, "ServerManagement.Get()");
		global::RustLevel.LevelLoadLog(8);
		global::LoadingScreen.Update("becoming ready");
		global::ServerManagement.Get().LocalClientPoliteReady();
		global::LoadingScreen.Update("waiting for character");
		yield return global::RustLevel.WaitForCondition(script, () => global::Controllable.localPlayerControllableExists, "Controllable.localPlayerControllableExists == true");
		global::RustLevel.LevelLoadLog(9);
		global::ConsoleSystem.Run("gameui.hide", false);
		global::LoadingScreen.Update("finished");
		global::HudEnabled.Enable();
		global::RustLevel.LevelLoadLog(10);
		Rust.Globals.isLoading = false;
		Object.Destroy(script.gameObject);
		yield break;
	}

	// Token: 0x06002848 RID: 10312 RVA: 0x00092C68 File Offset: 0x00090E68
	public static void LevelLoadLog(byte iStage)
	{
	}

	// Token: 0x06002849 RID: 10313 RVA: 0x00092C6C File Offset: 0x00090E6C
	private static Coroutine WaitForCondition(MonoBehaviour script, System.Func<bool> condition, string requestLabel)
	{
		return script.StartCoroutine(global::RustLevel.WaitForCondition(condition, requestLabel));
	}

	// Token: 0x0600284A RID: 10314 RVA: 0x00092C7C File Offset: 0x00090E7C
	private static IEnumerator WaitForCondition(System.Func<bool> condition, string requestLabel)
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

	// Token: 0x0600284B RID: 10315 RVA: 0x00092CAC File Offset: 0x00090EAC
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

	// Token: 0x0600284C RID: 10316 RVA: 0x00092D2C File Offset: 0x00090F2C
	private static void BroadcastGlobalMessage(string messageName)
	{
		foreach (GameObject gameObject in global::RustLevel.CollectRootGameObjects())
		{
			if (gameObject)
			{
				gameObject.BroadcastMessage(messageName, 1);
			}
		}
	}
}
