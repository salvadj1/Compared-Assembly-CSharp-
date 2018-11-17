using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007AF RID: 1967
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Internal/Update Manager")]
public class UpdateManager : MonoBehaviour
{
	// Token: 0x0600471D RID: 18205 RVA: 0x0011DEA8 File Offset: 0x0011C0A8
	private static int Compare(UpdateManager.UpdateEntry a, UpdateManager.UpdateEntry b)
	{
		if (a.index < b.index)
		{
			return 1;
		}
		if (a.index > b.index)
		{
			return 1;
		}
		return 0;
	}

	// Token: 0x0600471E RID: 18206 RVA: 0x0011DED4 File Offset: 0x0011C0D4
	private static void CreateInstance()
	{
		if (UpdateManager.mInst == null)
		{
			UpdateManager.mInst = (Object.FindObjectOfType(typeof(UpdateManager)) as UpdateManager);
			if (UpdateManager.mInst == null && Application.isPlaying)
			{
				GameObject gameObject = new GameObject("_UpdateManager");
				Object.DontDestroyOnLoad(gameObject);
				UpdateManager.mInst = gameObject.AddComponent<UpdateManager>();
			}
		}
	}

	// Token: 0x0600471F RID: 18207 RVA: 0x0011DF40 File Offset: 0x0011C140
	private void UpdateList(List<UpdateManager.UpdateEntry> list, float delta)
	{
		int i = list.Count;
		while (i > 0)
		{
			UpdateManager.UpdateEntry updateEntry = list[--i];
			if (updateEntry.isMonoBehaviour)
			{
				if (updateEntry.mb == null)
				{
					list.RemoveAt(i);
					continue;
				}
				if (!updateEntry.mb.enabled || !updateEntry.mb.gameObject.activeInHierarchy)
				{
					continue;
				}
			}
			updateEntry.func(delta);
		}
	}

	// Token: 0x06004720 RID: 18208 RVA: 0x0011DFCC File Offset: 0x0011C1CC
	private void Start()
	{
		if (Application.isPlaying)
		{
			this.mTime = Time.realtimeSinceStartup;
			base.StartCoroutine(this.CoroutineFunction());
		}
	}

	// Token: 0x06004721 RID: 18209 RVA: 0x0011DFFC File Offset: 0x0011C1FC
	private void OnApplicationQuit()
	{
		Object.DestroyImmediate(base.gameObject);
	}

	// Token: 0x06004722 RID: 18210 RVA: 0x0011E00C File Offset: 0x0011C20C
	private void Update()
	{
		if (UpdateManager.mInst != this)
		{
			NGUITools.Destroy(base.gameObject);
		}
		else
		{
			this.UpdateList(this.mOnUpdate, Time.deltaTime);
		}
	}

	// Token: 0x06004723 RID: 18211 RVA: 0x0011E04C File Offset: 0x0011C24C
	private void LateUpdate()
	{
		this.UpdateList(this.mOnLate, Time.deltaTime);
		if (!Application.isPlaying)
		{
			this.CoroutineUpdate();
		}
	}

	// Token: 0x06004724 RID: 18212 RVA: 0x0011E07C File Offset: 0x0011C27C
	private bool CoroutineUpdate()
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		float num = realtimeSinceStartup - this.mTime;
		if (num < 0.001f)
		{
			return true;
		}
		this.mTime = realtimeSinceStartup;
		this.UpdateList(this.mOnCoro, num);
		bool isPlaying = Application.isPlaying;
		int i = this.mDest.Count;
		while (i > 0)
		{
			UpdateManager.DestroyEntry destroyEntry = this.mDest[--i];
			if (!isPlaying || destroyEntry.time < this.mTime)
			{
				if (destroyEntry.obj != null)
				{
					NGUITools.Destroy(destroyEntry.obj);
					destroyEntry.obj = null;
				}
				this.mDest.RemoveAt(i);
			}
		}
		if (this.mOnUpdate.Count == 0 && this.mOnLate.Count == 0 && this.mOnCoro.Count == 0 && this.mDest.Count == 0)
		{
			NGUITools.Destroy(base.gameObject);
			return false;
		}
		return true;
	}

	// Token: 0x06004725 RID: 18213 RVA: 0x0011E184 File Offset: 0x0011C384
	private IEnumerator CoroutineFunction()
	{
		while (Application.isPlaying)
		{
			if (!this.CoroutineUpdate())
			{
				break;
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x06004726 RID: 18214 RVA: 0x0011E1A0 File Offset: 0x0011C3A0
	private void Add(MonoBehaviour mb, int updateOrder, UpdateManager.OnUpdate func, List<UpdateManager.UpdateEntry> list)
	{
		int i = 0;
		int count = list.Count;
		while (i < count)
		{
			UpdateManager.UpdateEntry updateEntry = list[i];
			if (updateEntry.func == func)
			{
				return;
			}
			i++;
		}
		list.Add(new UpdateManager.UpdateEntry
		{
			index = updateOrder,
			func = func,
			mb = mb,
			isMonoBehaviour = (mb != null)
		});
		if (updateOrder != 0)
		{
			list.Sort(new Comparison<UpdateManager.UpdateEntry>(UpdateManager.Compare));
		}
	}

	// Token: 0x06004727 RID: 18215 RVA: 0x0011E22C File Offset: 0x0011C42C
	public static void AddUpdate(MonoBehaviour mb, int updateOrder, UpdateManager.OnUpdate func)
	{
		UpdateManager.CreateInstance();
		UpdateManager.mInst.Add(mb, updateOrder, func, UpdateManager.mInst.mOnUpdate);
	}

	// Token: 0x06004728 RID: 18216 RVA: 0x0011E24C File Offset: 0x0011C44C
	public static void AddLateUpdate(MonoBehaviour mb, int updateOrder, UpdateManager.OnUpdate func)
	{
		UpdateManager.CreateInstance();
		UpdateManager.mInst.Add(mb, updateOrder, func, UpdateManager.mInst.mOnLate);
	}

	// Token: 0x06004729 RID: 18217 RVA: 0x0011E26C File Offset: 0x0011C46C
	public static void AddCoroutine(MonoBehaviour mb, int updateOrder, UpdateManager.OnUpdate func)
	{
		UpdateManager.CreateInstance();
		UpdateManager.mInst.Add(mb, updateOrder, func, UpdateManager.mInst.mOnCoro);
	}

	// Token: 0x0600472A RID: 18218 RVA: 0x0011E28C File Offset: 0x0011C48C
	public static void AddDestroy(Object obj, float delay)
	{
		if (obj == null)
		{
			return;
		}
		if (Application.isPlaying)
		{
			if (delay > 0f)
			{
				UpdateManager.CreateInstance();
				UpdateManager.DestroyEntry destroyEntry = new UpdateManager.DestroyEntry();
				destroyEntry.obj = obj;
				destroyEntry.time = Time.realtimeSinceStartup + delay;
				UpdateManager.mInst.mDest.Add(destroyEntry);
			}
			else
			{
				Object.Destroy(obj);
			}
		}
		else
		{
			Object.DestroyImmediate(obj);
		}
	}

	// Token: 0x04002726 RID: 10022
	private static UpdateManager mInst;

	// Token: 0x04002727 RID: 10023
	private List<UpdateManager.UpdateEntry> mOnUpdate = new List<UpdateManager.UpdateEntry>();

	// Token: 0x04002728 RID: 10024
	private List<UpdateManager.UpdateEntry> mOnLate = new List<UpdateManager.UpdateEntry>();

	// Token: 0x04002729 RID: 10025
	private List<UpdateManager.UpdateEntry> mOnCoro = new List<UpdateManager.UpdateEntry>();

	// Token: 0x0400272A RID: 10026
	private List<UpdateManager.DestroyEntry> mDest = new List<UpdateManager.DestroyEntry>();

	// Token: 0x0400272B RID: 10027
	private float mTime;

	// Token: 0x020007B0 RID: 1968
	public class UpdateEntry
	{
		// Token: 0x0400272C RID: 10028
		public int index;

		// Token: 0x0400272D RID: 10029
		public UpdateManager.OnUpdate func;

		// Token: 0x0400272E RID: 10030
		public MonoBehaviour mb;

		// Token: 0x0400272F RID: 10031
		public bool isMonoBehaviour;
	}

	// Token: 0x020007B1 RID: 1969
	public class DestroyEntry
	{
		// Token: 0x04002730 RID: 10032
		public Object obj;

		// Token: 0x04002731 RID: 10033
		public float time;
	}

	// Token: 0x020008ED RID: 2285
	// (Invoke) Token: 0x06004D8C RID: 19852
	public delegate void OnUpdate(float delta);
}
