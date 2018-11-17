using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200044F RID: 1103
[RequireComponent(typeof(Camera))]
public sealed class CameraMount : MonoBehaviour
{
	// Token: 0x17000926 RID: 2342
	// (get) Token: 0x0600285A RID: 10330 RVA: 0x0009EB84 File Offset: 0x0009CD84
	public static CameraMount current
	{
		get
		{
			return CameraMount.top;
		}
	}

	// Token: 0x17000927 RID: 2343
	// (get) Token: 0x0600285B RID: 10331 RVA: 0x0009EB8C File Offset: 0x0009CD8C
	public bool isActiveMount
	{
		get
		{
			return CameraMount.top == this;
		}
	}

	// Token: 0x0600285C RID: 10332 RVA: 0x0009EB9C File Offset: 0x0009CD9C
	private void OnSetActiveMount()
	{
	}

	// Token: 0x0600285D RID: 10333 RVA: 0x0009EBA0 File Offset: 0x0009CDA0
	private void OnNotActiveMount()
	{
	}

	// Token: 0x0600285E RID: 10334 RVA: 0x0009EBA4 File Offset: 0x0009CDA4
	public void OnPreMount(MountedCamera camera)
	{
	}

	// Token: 0x0600285F RID: 10335 RVA: 0x0009EBA8 File Offset: 0x0009CDA8
	public void OnPostMount(MountedCamera camera)
	{
	}

	// Token: 0x17000928 RID: 2344
	// (get) Token: 0x06002860 RID: 10336 RVA: 0x0009EBAC File Offset: 0x0009CDAC
	private static Stack<CameraMount> queue
	{
		get
		{
			return CameraMount.QUEUE_LATE.queue;
		}
	}

	// Token: 0x06002861 RID: 10337 RVA: 0x0009EBB4 File Offset: 0x0009CDB4
	private void Awake()
	{
		this.awoke = true;
		if (!this.camera)
		{
			this.camera = base.camera;
		}
		this.camera.enabled = false;
		if (CameraMount.creatingTemporaryCameraMount)
		{
			CameraMount.temporaryCameraMount = this;
			if (CameraMount.temporaryCameraMountHasParent)
			{
				Transform transform = base.transform;
				transform.parent = CameraMount.temporaryCameraMountParent;
				Transform transform2 = CameraMount.temporaryCameraMountSource.transform;
				transform.localPosition = transform2.localPosition;
				transform.localRotation = transform2.localRotation;
				transform.localScale = transform2.localScale;
			}
			this.camera.CopyFrom(CameraMount.temporaryCameraMountSource.camera);
			this.cameraFX = CameraMount.temporaryCameraMountSource.cameraFX;
			if (CameraMount.temporaryCameraMountSource.open)
			{
				this.Bind();
			}
		}
		else if (this.autoBind)
		{
			this.Bind();
		}
	}

	// Token: 0x06002862 RID: 10338 RVA: 0x0009EC9C File Offset: 0x0009CE9C
	private static void SORT_QUEUE(CameraMount addExtra)
	{
		CameraMount.WORK_LATE.list.Add(addExtra);
		CameraMount.SORT_QUEUE();
	}

	// Token: 0x06002863 RID: 10339 RVA: 0x0009ECB0 File Offset: 0x0009CEB0
	private static void SORT_QUEUE()
	{
		CameraMount.WORK_LATE.list.AddRange(CameraMount.queue);
		try
		{
			CameraMount.WORK_LATE.list.Sort((CameraMount a, CameraMount b) => a.importance.CompareTo(b.importance));
			CameraMount.queue.Clear();
			foreach (CameraMount t in CameraMount.WORK_LATE.list)
			{
				CameraMount.queue.Push(t);
			}
		}
		finally
		{
			CameraMount.WORK_LATE.list.Clear();
		}
	}

	// Token: 0x06002864 RID: 10340 RVA: 0x0009ED84 File Offset: 0x0009CF84
	private static void REMOVE_FROM_QUEUE(CameraMount rem)
	{
		try
		{
			int count = CameraMount.queue.Count;
			for (int i = 0; i < count; i++)
			{
				CameraMount cameraMount = CameraMount.queue.Pop();
				if (cameraMount != rem)
				{
					CameraMount.WORK_LATE.list.Add(cameraMount);
				}
			}
			CameraMount.WORK_LATE.list.Reverse();
			foreach (CameraMount t in CameraMount.WORK_LATE.list)
			{
				CameraMount.queue.Push(t);
			}
		}
		finally
		{
			CameraMount.WORK_LATE.list.Clear();
		}
	}

	// Token: 0x06002865 RID: 10341 RVA: 0x0009EE60 File Offset: 0x0009D060
	private static void SetMountActive()
	{
		try
		{
			CameraMount.top.OnSetActiveMount();
		}
		catch (Exception ex)
		{
			Debug.LogError(ex, CameraMount.top);
		}
	}

	// Token: 0x06002866 RID: 10342 RVA: 0x0009EEAC File Offset: 0x0009D0AC
	private static void SetMountInactive()
	{
		try
		{
			CameraMount.top.OnNotActiveMount();
		}
		catch (Exception ex)
		{
			Debug.LogError(ex, CameraMount.top);
		}
	}

	// Token: 0x06002867 RID: 10343 RVA: 0x0009EEF8 File Offset: 0x0009D0F8
	private void Bind()
	{
		if (!CameraMount.top)
		{
			CameraMount.top = this;
			CameraMount.SetMountActive();
		}
		else if (CameraMount.top.importance < this.importance)
		{
			CameraMount.SetMountInactive();
			CameraMount.queue.Push(CameraMount.top);
			CameraMount.top = this;
			CameraMount.SetMountActive();
		}
		else if (CameraMount.queue.Count == 0 || CameraMount.queue.Peek().importance <= this.importance)
		{
			CameraMount.queue.Push(this);
		}
		else
		{
			CameraMount.SORT_QUEUE(this);
		}
		this.bound = true;
	}

	// Token: 0x06002868 RID: 10344 RVA: 0x0009EFA4 File Offset: 0x0009D1A4
	private void UnBind()
	{
		if (CameraMount.top == this)
		{
			CameraMount.SetMountInactive();
			if (CameraMount.queue.Count > 0)
			{
				CameraMount.top = CameraMount.queue.Pop();
				CameraMount.SetMountActive();
			}
			else
			{
				CameraMount.top = null;
			}
		}
		else if (CameraMount.queue.Count > 1)
		{
			if (CameraMount.queue.Peek() == this)
			{
				CameraMount.queue.Pop();
			}
			else
			{
				CameraMount.REMOVE_FROM_QUEUE(this);
			}
		}
		else
		{
			CameraMount.queue.Pop();
		}
		this.bound = false;
	}

	// Token: 0x06002869 RID: 10345 RVA: 0x0009F04C File Offset: 0x0009D24C
	public void EnableTransition(float duration, TransitionFunction function)
	{
		if (!this.open)
		{
			this.open = true;
			if (this.isActiveMount)
			{
				CameraFX.TransitionNow(duration, function);
			}
		}
	}

	// Token: 0x0600286A RID: 10346 RVA: 0x0009F080 File Offset: 0x0009D280
	public void EnableTransitionSpeed(float metersPerSecond, TransitionFunction function)
	{
		if (!this.open)
		{
			this.open = true;
			Vector3 vector;
			if (this.isActiveMount && MountedCamera.GetPoint(out vector))
			{
				float num = Vector3.Distance(this.camera.worldToCameraMatrix.MultiplyPoint(Vector3.zero), vector);
				if (num != 0f)
				{
					CameraFX.TransitionNow(num / metersPerSecond, function);
				}
			}
		}
	}

	// Token: 0x17000929 RID: 2345
	// (get) Token: 0x0600286B RID: 10347 RVA: 0x0009F0EC File Offset: 0x0009D2EC
	// (set) Token: 0x0600286C RID: 10348 RVA: 0x0009F10C File Offset: 0x0009D30C
	public bool open
	{
		get
		{
			return (!this.awoke) ? this.autoBind : this.bound;
		}
		set
		{
			if (!this.destroyed)
			{
				if (this.awoke)
				{
					if (this.bound != value)
					{
						if (this.bound)
						{
							this.UnBind();
						}
						else
						{
							this.Bind();
						}
					}
				}
				else
				{
					this.autoBind = value;
				}
			}
		}
	}

	// Token: 0x1700092A RID: 2346
	// (get) Token: 0x0600286D RID: 10349 RVA: 0x0009F164 File Offset: 0x0009D364
	// (set) Token: 0x0600286E RID: 10350 RVA: 0x0009F16C File Offset: 0x0009D36C
	[Obsolete("use the open property instead!", true)]
	public bool enabled
	{
		get
		{
			return this.open;
		}
		set
		{
			this.open = value;
		}
	}

	// Token: 0x0600286F RID: 10351 RVA: 0x0009F178 File Offset: 0x0009D378
	private void OnDestroy()
	{
		this.destroyed = true;
		if (this.bound)
		{
			this.UnBind();
		}
	}

	// Token: 0x06002870 RID: 10352 RVA: 0x0009F194 File Offset: 0x0009D394
	public static void ClearTemporaryCameraMount()
	{
		if (CameraMount.createdTemporaryCameraMount && CameraMount.temporaryCameraMount)
		{
			Object.Destroy(CameraMount.temporaryCameraMount);
			Object.Destroy(CameraMount.temporaryCameraMountGameObject);
			CameraMount.createdTemporaryCameraMount = false;
		}
	}

	// Token: 0x06002871 RID: 10353 RVA: 0x0009F1CC File Offset: 0x0009D3CC
	public static CameraMount CreateTemporaryCameraMount(CameraMount copyFrom)
	{
		return CameraMount.CreateTemporaryCameraMount(copyFrom, null, false);
	}

	// Token: 0x06002872 RID: 10354 RVA: 0x0009F1D8 File Offset: 0x0009D3D8
	public static CameraMount CreateTemporaryCameraMount(CameraMount copyFrom, Transform parent)
	{
		return CameraMount.CreateTemporaryCameraMount(copyFrom, parent, parent);
	}

	// Token: 0x06002873 RID: 10355 RVA: 0x0009F1E8 File Offset: 0x0009D3E8
	private static CameraMount CreateTemporaryCameraMount(CameraMount copyFrom, Transform parent, bool hasParent)
	{
		if (CameraMount.creatingTemporaryCameraMount)
		{
			throw new InvalidOperationException("Invalid/unexpected call stack recursion");
		}
		CameraMount.ClearTemporaryCameraMount();
		try
		{
			CameraMount.creatingTemporaryCameraMount = true;
			CameraMount.temporaryCameraMountSource = copyFrom;
			CameraMount.temporaryCameraMountHasParent = hasParent;
			CameraMount.temporaryCameraMountParent = parent;
			CameraMount.temporaryCameraMountGameObject = new GameObject("__-temp mount-__", new Type[]
			{
				typeof(CameraMount)
			})
			{
				hideFlags = 4
			};
		}
		finally
		{
			CameraMount.creatingTemporaryCameraMount = false;
			CameraMount.temporaryCameraMountSource = null;
			CameraMount.temporaryCameraMountHasParent = false;
			CameraMount.temporaryCameraMountParent = null;
			CameraMount.createdTemporaryCameraMount = CameraMount.temporaryCameraMount;
		}
		return CameraMount.temporaryCameraMount;
	}

	// Token: 0x0400146C RID: 5228
	private const string kTempMountGOName = "__-temp mount-__";

	// Token: 0x0400146D RID: 5229
	[PrefetchComponent]
	public CameraFX cameraFX;

	// Token: 0x0400146E RID: 5230
	[PrefetchComponent]
	public Camera camera;

	// Token: 0x0400146F RID: 5231
	public KindOfCamera kindOfCamera;

	// Token: 0x04001470 RID: 5232
	public SharedCameraMode cameraMode;

	// Token: 0x04001471 RID: 5233
	private static CameraMount top;

	// Token: 0x04001472 RID: 5234
	[SerializeField]
	private int importance;

	// Token: 0x04001473 RID: 5235
	[SerializeField]
	private bool autoBind;

	// Token: 0x04001474 RID: 5236
	[NonSerialized]
	private bool awoke;

	// Token: 0x04001475 RID: 5237
	[NonSerialized]
	private bool bound;

	// Token: 0x04001476 RID: 5238
	[NonSerialized]
	private bool destroyed;

	// Token: 0x04001477 RID: 5239
	private static CameraMount temporaryCameraMount;

	// Token: 0x04001478 RID: 5240
	private static CameraMount temporaryCameraMountSource;

	// Token: 0x04001479 RID: 5241
	private static GameObject temporaryCameraMountGameObject;

	// Token: 0x0400147A RID: 5242
	private static Transform temporaryCameraMountParent;

	// Token: 0x0400147B RID: 5243
	private static bool temporaryCameraMountHasParent;

	// Token: 0x0400147C RID: 5244
	private static bool creatingTemporaryCameraMount;

	// Token: 0x0400147D RID: 5245
	private static bool createdTemporaryCameraMount;

	// Token: 0x02000450 RID: 1104
	private static class QUEUE_LATE
	{
		// Token: 0x0400147F RID: 5247
		public static readonly Stack<CameraMount> queue = new Stack<CameraMount>();
	}

	// Token: 0x02000451 RID: 1105
	private static class WORK_LATE
	{
		// Token: 0x04001480 RID: 5248
		public static readonly List<CameraMount> list = new List<CameraMount>();
	}
}
