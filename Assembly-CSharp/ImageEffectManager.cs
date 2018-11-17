using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200050B RID: 1291
public class ImageEffectManager : MonoBehaviour
{
	// Token: 0x06002C14 RID: 11284 RVA: 0x000A535C File Offset: 0x000A355C
	protected void OnEnable()
	{
		global::ImageEffectManager.singleton = this;
	}

	// Token: 0x06002C15 RID: 11285 RVA: 0x000A5364 File Offset: 0x000A3564
	protected void OnDisable()
	{
		if (global::ImageEffectManager.singleton == this)
		{
			global::ImageEffectManager.singleton = null;
		}
	}

	// Token: 0x06002C16 RID: 11286 RVA: 0x000A537C File Offset: 0x000A357C
	protected void Start()
	{
		foreach (MonoBehaviour monoBehaviour in base.GetComponents<MonoBehaviour>())
		{
			if (!monoBehaviour.enabled)
			{
				return;
			}
			Type type = monoBehaviour.GetType();
			if (global::ImageEffectManager.states.ContainsKey(type))
			{
				monoBehaviour.enabled = global::ImageEffectManager.states[type];
			}
		}
	}

	// Token: 0x06002C17 RID: 11287 RVA: 0x000A53DC File Offset: 0x000A35DC
	public static T GetInstance<T>() where T : MonoBehaviour
	{
		return (!(global::ImageEffectManager.singleton != null)) ? ((T)((object)null)) : global::ImageEffectManager.singleton.GetComponent<T>();
	}

	// Token: 0x06002C18 RID: 11288 RVA: 0x000A5404 File Offset: 0x000A3604
	public static bool GetEnabled<T>() where T : MonoBehaviour
	{
		return !global::ImageEffectManager.states.ContainsKey(typeof(T)) || global::ImageEffectManager.states[typeof(T)];
	}

	// Token: 0x06002C19 RID: 11289 RVA: 0x000A543C File Offset: 0x000A363C
	public static void SetEnabled<T>(bool value) where T : MonoBehaviour
	{
		if (global::ImageEffectManager.GetInstance<T>() != null)
		{
			T instance = global::ImageEffectManager.GetInstance<T>();
			instance.enabled = value;
		}
		if (!global::ImageEffectManager.states.ContainsKey(typeof(T)))
		{
			global::ImageEffectManager.states.Add(typeof(T), value);
		}
		else
		{
			global::ImageEffectManager.states[typeof(T)] = value;
		}
	}

	// Token: 0x04001608 RID: 5640
	private static global::ImageEffectManager singleton;

	// Token: 0x04001609 RID: 5641
	private static Dictionary<Type, bool> states = new Dictionary<Type, bool>();
}
