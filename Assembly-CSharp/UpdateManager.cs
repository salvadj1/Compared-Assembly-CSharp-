using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200089A RID: 2202
[AddComponentMenu("NGUI/Internal/Update Manager")]
[ExecuteInEditMode]
public class UpdateManager : MonoBehaviour
{
	// Token: 0x06004BA2 RID: 19362 RVA: 0x00127828 File Offset: 0x00125A28
	private static int Compare(global::UpdateManager.UpdateEntry a, global::UpdateManager.UpdateEntry b)
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

	// Token: 0x06004BA3 RID: 19363 RVA: 0x00127854 File Offset: 0x00125A54
	private static void CreateInstance()
	{
		if (global::UpdateManager.mInst == null)
		{
			global::UpdateManager.mInst = (Object.FindObjectOfType(typeof(global::UpdateManager)) as global::UpdateManager);
			if (global::UpdateManager.mInst == null && Application.isPlaying)
			{
				GameObject gameObject = new GameObject("_UpdateManager");
				Object.DontDestroyOnLoad(gameObject);
				global::UpdateManager.mInst = gameObject.AddComponent<global::UpdateManager>();
			}
		}
	}

	// Token: 0x06004BA4 RID: 19364 RVA: 0x001278C0 File Offset: 0x00125AC0
	private void UpdateList(List<global::UpdateManager.UpdateEntry> list, float delta)
	{
		int i = list.Count;
		while (i > 0)
		{
			global::UpdateManager.UpdateEntry updateEntry = list[--i];
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

	// Token: 0x06004BA5 RID: 19365 RVA: 0x0012794C File Offset: 0x00125B4C
	private void Start()
	{
		if (Application.isPlaying)
		{
			this.mTime = Time.realtimeSinceStartup;
			base.StartCoroutine(this.CoroutineFunction());
		}
	}

	// Token: 0x06004BA6 RID: 19366 RVA: 0x0012797C File Offset: 0x00125B7C
	private void OnApplicationQuit()
	{
		Object.DestroyImmediate(base.gameObject);
	}

	// Token: 0x06004BA7 RID: 19367 RVA: 0x0012798C File Offset: 0x00125B8C
	private void Update()
	{
		if (global::UpdateManager.mInst != this)
		{
			global::NGUITools.Destroy(base.gameObject);
		}
		else
		{
			this.UpdateList(this.mOnUpdate, Time.deltaTime);
		}
	}

	// Token: 0x06004BA8 RID: 19368 RVA: 0x001279CC File Offset: 0x00125BCC
	private void LateUpdate()
	{
		this.UpdateList(this.mOnLate, Time.deltaTime);
		if (!Application.isPlaying)
		{
			this.CoroutineUpdate();
		}
	}

	// Token: 0x06004BA9 RID: 19369 RVA: 0x001279FC File Offset: 0x00125BFC
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
			global::UpdateManager.DestroyEntry destroyEntry = this.mDest[--i];
			if (!isPlaying || destroyEntry.time < this.mTime)
			{
				if (destroyEntry.obj != null)
				{
					global::NGUITools.Destroy(destroyEntry.obj);
					destroyEntry.obj = null;
				}
				this.mDest.RemoveAt(i);
			}
		}
		if (this.mOnUpdate.Count == 0 && this.mOnLate.Count == 0 && this.mOnCoro.Count == 0 && this.mDest.Count == 0)
		{
			global::NGUITools.Destroy(base.gameObject);
			return false;
		}
		return true;
	}

	// Token: 0x06004BAA RID: 19370 RVA: 0x00127B04 File Offset: 0x00125D04
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

	// Token: 0x06004BAB RID: 19371 RVA: 0x00127B20 File Offset: 0x00125D20
	private void Add(MonoBehaviour mb, int updateOrder, global::UpdateManager.OnUpdate func, List<global::UpdateManager.UpdateEntry> list)
	{
		int i = 0;
		int count = list.Count;
		while (i < count)
		{
			global::UpdateManager.UpdateEntry updateEntry = list[i];
			if (updateEntry.func == func)
			{
				return;
			}
			i++;
		}
		list.Add(new global::UpdateManager.UpdateEntry
		{
			index = updateOrder,
			func = func,
			mb = mb,
			isMonoBehaviour = (mb != null)
		});
		if (updateOrder != 0)
		{
			list.Sort(new Comparison<global::UpdateManager.UpdateEntry>(global::UpdateManager.Compare));
		}
	}

	// Token: 0x06004BAC RID: 19372 RVA: 0x00127BAC File Offset: 0x00125DAC
	public static void AddUpdate(MonoBehaviour mb, int updateOrder, global::UpdateManager.OnUpdate func)
	{
		global::UpdateManager.CreateInstance();
		global::UpdateManager.mInst.Add(mb, updateOrder, func, global::UpdateManager.mInst.mOnUpdate);
	}

	// Token: 0x06004BAD RID: 19373 RVA: 0x00127BCC File Offset: 0x00125DCC
	public static void AddLateUpdate(MonoBehaviour mb, int updateOrder, global::UpdateManager.OnUpdate func)
	{
		global::UpdateManager.CreateInstance();
		global::UpdateManager.mInst.Add(mb, updateOrder, func, global::UpdateManager.mInst.mOnLate);
	}

	// Token: 0x06004BAE RID: 19374 RVA: 0x00127BEC File Offset: 0x00125DEC
	public static void AddCoroutine(MonoBehaviour mb, int updateOrder, global::UpdateManager.OnUpdate func)
	{
		global::UpdateManager.CreateInstance();
		global::UpdateManager.mInst.Add(mb, updateOrder, func, global::UpdateManager.mInst.mOnCoro);
	}

	// Token: 0x06004BAF RID: 19375 RVA: 0x00127C0C File Offset: 0x00125E0C
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
				global::UpdateManager.CreateInstance();
				global::UpdateManager.DestroyEntry destroyEntry = new global::UpdateManager.DestroyEntry();
				destroyEntry.obj = obj;
				destroyEntry.time = Time.realtimeSinceStartup + delay;
				global::UpdateManager.mInst.mDest.Add(destroyEntry);
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

	// Token: 0x0400295D RID: 10589
	private static global::UpdateManager mInst;

	// Token: 0x0400295E RID: 10590
	private List<global::UpdateManager.UpdateEntry> mOnUpdate = new List<global::UpdateManager.UpdateEntry>();

	// Token: 0x0400295F RID: 10591
	private List<global::UpdateManager.UpdateEntry> mOnLate = new List<global::UpdateManager.UpdateEntry>();

	// Token: 0x04002960 RID: 10592
	private List<global::UpdateManager.UpdateEntry> mOnCoro = new List<global::UpdateManager.UpdateEntry>();

	// Token: 0x04002961 RID: 10593
	private List<global::UpdateManager.DestroyEntry> mDest = new List<global::UpdateManager.DestroyEntry>();

	// Token: 0x04002962 RID: 10594
	private float mTime;

	// Token: 0x0200089B RID: 2203
	public class UpdateEntry
	{
		// Token: 0x04002963 RID: 10595
		public int index;

		// Token: 0x04002964 RID: 10596
		public global::UpdateManager.OnUpdate func;

		// Token: 0x04002965 RID: 10597
		public MonoBehaviour mb;

		// Token: 0x04002966 RID: 10598
		public bool isMonoBehaviour;
	}

	// Token: 0x0200089C RID: 2204
	public class DestroyEntry
	{
		// Token: 0x04002967 RID: 10599
		public Object obj;

		// Token: 0x04002968 RID: 10600
		public float time;
	}

	// Token: 0x0200089D RID: 2205
	// (Invoke) Token: 0x06004BB3 RID: 19379
	public delegate void OnUpdate(float delta);
}
