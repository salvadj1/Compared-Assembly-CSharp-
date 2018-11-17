using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000455 RID: 1109
public class ImageEffectManager : MonoBehaviour
{
	// Token: 0x06002884 RID: 10372 RVA: 0x0009F3DC File Offset: 0x0009D5DC
	protected void OnEnable()
	{
		ImageEffectManager.singleton = this;
	}

	// Token: 0x06002885 RID: 10373 RVA: 0x0009F3E4 File Offset: 0x0009D5E4
	protected void OnDisable()
	{
		if (ImageEffectManager.singleton == this)
		{
			ImageEffectManager.singleton = null;
		}
	}

	// Token: 0x06002886 RID: 10374 RVA: 0x0009F3FC File Offset: 0x0009D5FC
	protected void Start()
	{
		foreach (MonoBehaviour monoBehaviour in base.GetComponents<MonoBehaviour>())
		{
			if (!monoBehaviour.enabled)
			{
				return;
			}
			Type type = monoBehaviour.GetType();
			if (ImageEffectManager.states.ContainsKey(type))
			{
				monoBehaviour.enabled = ImageEffectManager.states[type];
			}
		}
	}

	// Token: 0x06002887 RID: 10375 RVA: 0x0009F45C File Offset: 0x0009D65C
	public static T GetInstance<T>() where T : MonoBehaviour
	{
		return (!(ImageEffectManager.singleton != null)) ? ((T)((object)null)) : ImageEffectManager.singleton.GetComponent<T>();
	}

	// Token: 0x06002888 RID: 10376 RVA: 0x0009F484 File Offset: 0x0009D684
	public static bool GetEnabled<T>() where T : MonoBehaviour
	{
		return !ImageEffectManager.states.ContainsKey(typeof(T)) || ImageEffectManager.states[typeof(T)];
	}

	// Token: 0x06002889 RID: 10377 RVA: 0x0009F4BC File Offset: 0x0009D6BC
	public static void SetEnabled<T>(bool value) where T : MonoBehaviour
	{
		if (ImageEffectManager.GetInstance<T>() != null)
		{
			T instance = ImageEffectManager.GetInstance<T>();
			instance.enabled = value;
		}
		if (!ImageEffectManager.states.ContainsKey(typeof(T)))
		{
			ImageEffectManager.states.Add(typeof(T), value);
		}
		else
		{
			ImageEffectManager.states[typeof(T)] = value;
		}
	}

	// Token: 0x04001485 RID: 5253
	private static ImageEffectManager singleton;

	// Token: 0x04001486 RID: 5254
	private static Dictionary<Type, bool> states = new Dictionary<Type, bool>();
}
