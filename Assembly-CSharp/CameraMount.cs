using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000505 RID: 1285
[RequireComponent(typeof(Camera))]
public sealed class CameraMount : MonoBehaviour
{
	// Token: 0x1700098E RID: 2446
	// (get) Token: 0x06002BEA RID: 11242 RVA: 0x000A4B04 File Offset: 0x000A2D04
	public static global::CameraMount current
	{
		get
		{
			return global::CameraMount.top;
		}
	}

	// Token: 0x1700098F RID: 2447
	// (get) Token: 0x06002BEB RID: 11243 RVA: 0x000A4B0C File Offset: 0x000A2D0C
	public bool isActiveMount
	{
		get
		{
			return global::CameraMount.top == this;
		}
	}

	// Token: 0x06002BEC RID: 11244 RVA: 0x000A4B1C File Offset: 0x000A2D1C
	private void OnSetActiveMount()
	{
	}

	// Token: 0x06002BED RID: 11245 RVA: 0x000A4B20 File Offset: 0x000A2D20
	private void OnNotActiveMount()
	{
	}

	// Token: 0x06002BEE RID: 11246 RVA: 0x000A4B24 File Offset: 0x000A2D24
	public void OnPreMount(global::MountedCamera camera)
	{
	}

	// Token: 0x06002BEF RID: 11247 RVA: 0x000A4B28 File Offset: 0x000A2D28
	public void OnPostMount(global::MountedCamera camera)
	{
	}

	// Token: 0x17000990 RID: 2448
	// (get) Token: 0x06002BF0 RID: 11248 RVA: 0x000A4B2C File Offset: 0x000A2D2C
	private static Stack<global::CameraMount> queue
	{
		get
		{
			return global::CameraMount.QUEUE_LATE.queue;
		}
	}

	// Token: 0x06002BF1 RID: 11249 RVA: 0x000A4B34 File Offset: 0x000A2D34
	private void Awake()
	{
		this.awoke = true;
		if (!this.camera)
		{
			this.camera = base.camera;
		}
		this.camera.enabled = false;
		if (global::CameraMount.creatingTemporaryCameraMount)
		{
			global::CameraMount.temporaryCameraMount = this;
			if (global::CameraMount.temporaryCameraMountHasParent)
			{
				Transform transform = base.transform;
				transform.parent = global::CameraMount.temporaryCameraMountParent;
				Transform transform2 = global::CameraMount.temporaryCameraMountSource.transform;
				transform.localPosition = transform2.localPosition;
				transform.localRotation = transform2.localRotation;
				transform.localScale = transform2.localScale;
			}
			this.camera.CopyFrom(global::CameraMount.temporaryCameraMountSource.camera);
			this.cameraFX = global::CameraMount.temporaryCameraMountSource.cameraFX;
			if (global::CameraMount.temporaryCameraMountSource.open)
			{
				this.Bind();
			}
		}
		else if (this.autoBind)
		{
			this.Bind();
		}
	}

	// Token: 0x06002BF2 RID: 11250 RVA: 0x000A4C1C File Offset: 0x000A2E1C
	private static void SORT_QUEUE(global::CameraMount addExtra)
	{
		global::CameraMount.WORK_LATE.list.Add(addExtra);
		global::CameraMount.SORT_QUEUE();
	}

	// Token: 0x06002BF3 RID: 11251 RVA: 0x000A4C30 File Offset: 0x000A2E30
	private static void SORT_QUEUE()
	{
		global::CameraMount.WORK_LATE.list.AddRange(global::CameraMount.queue);
		try
		{
			global::CameraMount.WORK_LATE.list.Sort((global::CameraMount a, global::CameraMount b) => a.importance.CompareTo(b.importance));
			global::CameraMount.queue.Clear();
			foreach (global::CameraMount t in global::CameraMount.WORK_LATE.list)
			{
				global::CameraMount.queue.Push(t);
			}
		}
		finally
		{
			global::CameraMount.WORK_LATE.list.Clear();
		}
	}

	// Token: 0x06002BF4 RID: 11252 RVA: 0x000A4D04 File Offset: 0x000A2F04
	private static void REMOVE_FROM_QUEUE(global::CameraMount rem)
	{
		try
		{
			int count = global::CameraMount.queue.Count;
			for (int i = 0; i < count; i++)
			{
				global::CameraMount cameraMount = global::CameraMount.queue.Pop();
				if (cameraMount != rem)
				{
					global::CameraMount.WORK_LATE.list.Add(cameraMount);
				}
			}
			global::CameraMount.WORK_LATE.list.Reverse();
			foreach (global::CameraMount t in global::CameraMount.WORK_LATE.list)
			{
				global::CameraMount.queue.Push(t);
			}
		}
		finally
		{
			global::CameraMount.WORK_LATE.list.Clear();
		}
	}

	// Token: 0x06002BF5 RID: 11253 RVA: 0x000A4DE0 File Offset: 0x000A2FE0
	private static void SetMountActive()
	{
		try
		{
			global::CameraMount.top.OnSetActiveMount();
		}
		catch (Exception ex)
		{
			Debug.LogError(ex, global::CameraMount.top);
		}
	}

	// Token: 0x06002BF6 RID: 11254 RVA: 0x000A4E2C File Offset: 0x000A302C
	private static void SetMountInactive()
	{
		try
		{
			global::CameraMount.top.OnNotActiveMount();
		}
		catch (Exception ex)
		{
			Debug.LogError(ex, global::CameraMount.top);
		}
	}

	// Token: 0x06002BF7 RID: 11255 RVA: 0x000A4E78 File Offset: 0x000A3078
	private void Bind()
	{
		if (!global::CameraMount.top)
		{
			global::CameraMount.top = this;
			global::CameraMount.SetMountActive();
		}
		else if (global::CameraMount.top.importance < this.importance)
		{
			global::CameraMount.SetMountInactive();
			global::CameraMount.queue.Push(global::CameraMount.top);
			global::CameraMount.top = this;
			global::CameraMount.SetMountActive();
		}
		else if (global::CameraMount.queue.Count == 0 || global::CameraMount.queue.Peek().importance <= this.importance)
		{
			global::CameraMount.queue.Push(this);
		}
		else
		{
			global::CameraMount.SORT_QUEUE(this);
		}
		this.bound = true;
	}

	// Token: 0x06002BF8 RID: 11256 RVA: 0x000A4F24 File Offset: 0x000A3124
	private void UnBind()
	{
		if (global::CameraMount.top == this)
		{
			global::CameraMount.SetMountInactive();
			if (global::CameraMount.queue.Count > 0)
			{
				global::CameraMount.top = global::CameraMount.queue.Pop();
				global::CameraMount.SetMountActive();
			}
			else
			{
				global::CameraMount.top = null;
			}
		}
		else if (global::CameraMount.queue.Count > 1)
		{
			if (global::CameraMount.queue.Peek() == this)
			{
				global::CameraMount.queue.Pop();
			}
			else
			{
				global::CameraMount.REMOVE_FROM_QUEUE(this);
			}
		}
		else
		{
			global::CameraMount.queue.Pop();
		}
		this.bound = false;
	}

	// Token: 0x06002BF9 RID: 11257 RVA: 0x000A4FCC File Offset: 0x000A31CC
	public void EnableTransition(float duration, global::TransitionFunction function)
	{
		if (!this.open)
		{
			this.open = true;
			if (this.isActiveMount)
			{
				global::CameraFX.TransitionNow(duration, function);
			}
		}
	}

	// Token: 0x06002BFA RID: 11258 RVA: 0x000A5000 File Offset: 0x000A3200
	public void EnableTransitionSpeed(float metersPerSecond, global::TransitionFunction function)
	{
		if (!this.open)
		{
			this.open = true;
			Vector3 vector;
			if (this.isActiveMount && global::MountedCamera.GetPoint(out vector))
			{
				float num = Vector3.Distance(this.camera.worldToCameraMatrix.MultiplyPoint(Vector3.zero), vector);
				if (num != 0f)
				{
					global::CameraFX.TransitionNow(num / metersPerSecond, function);
				}
			}
		}
	}

	// Token: 0x17000991 RID: 2449
	// (get) Token: 0x06002BFB RID: 11259 RVA: 0x000A506C File Offset: 0x000A326C
	// (set) Token: 0x06002BFC RID: 11260 RVA: 0x000A508C File Offset: 0x000A328C
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

	// Token: 0x17000992 RID: 2450
	// (get) Token: 0x06002BFD RID: 11261 RVA: 0x000A50E4 File Offset: 0x000A32E4
	// (set) Token: 0x06002BFE RID: 11262 RVA: 0x000A50EC File Offset: 0x000A32EC
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

	// Token: 0x06002BFF RID: 11263 RVA: 0x000A50F8 File Offset: 0x000A32F8
	private void OnDestroy()
	{
		this.destroyed = true;
		if (this.bound)
		{
			this.UnBind();
		}
	}

	// Token: 0x06002C00 RID: 11264 RVA: 0x000A5114 File Offset: 0x000A3314
	public static void ClearTemporaryCameraMount()
	{
		if (global::CameraMount.createdTemporaryCameraMount && global::CameraMount.temporaryCameraMount)
		{
			Object.Destroy(global::CameraMount.temporaryCameraMount);
			Object.Destroy(global::CameraMount.temporaryCameraMountGameObject);
			global::CameraMount.createdTemporaryCameraMount = false;
		}
	}

	// Token: 0x06002C01 RID: 11265 RVA: 0x000A514C File Offset: 0x000A334C
	public static global::CameraMount CreateTemporaryCameraMount(global::CameraMount copyFrom)
	{
		return global::CameraMount.CreateTemporaryCameraMount(copyFrom, null, false);
	}

	// Token: 0x06002C02 RID: 11266 RVA: 0x000A5158 File Offset: 0x000A3358
	public static global::CameraMount CreateTemporaryCameraMount(global::CameraMount copyFrom, Transform parent)
	{
		return global::CameraMount.CreateTemporaryCameraMount(copyFrom, parent, parent);
	}

	// Token: 0x06002C03 RID: 11267 RVA: 0x000A5168 File Offset: 0x000A3368
	private static global::CameraMount CreateTemporaryCameraMount(global::CameraMount copyFrom, Transform parent, bool hasParent)
	{
		if (global::CameraMount.creatingTemporaryCameraMount)
		{
			throw new InvalidOperationException("Invalid/unexpected call stack recursion");
		}
		global::CameraMount.ClearTemporaryCameraMount();
		try
		{
			global::CameraMount.creatingTemporaryCameraMount = true;
			global::CameraMount.temporaryCameraMountSource = copyFrom;
			global::CameraMount.temporaryCameraMountHasParent = hasParent;
			global::CameraMount.temporaryCameraMountParent = parent;
			global::CameraMount.temporaryCameraMountGameObject = new GameObject("__-temp mount-__", new Type[]
			{
				typeof(global::CameraMount)
			})
			{
				hideFlags = 4
			};
		}
		finally
		{
			global::CameraMount.creatingTemporaryCameraMount = false;
			global::CameraMount.temporaryCameraMountSource = null;
			global::CameraMount.temporaryCameraMountHasParent = false;
			global::CameraMount.temporaryCameraMountParent = null;
			global::CameraMount.createdTemporaryCameraMount = global::CameraMount.temporaryCameraMount;
		}
		return global::CameraMount.temporaryCameraMount;
	}

	// Token: 0x040015EF RID: 5615
	private const string kTempMountGOName = "__-temp mount-__";

	// Token: 0x040015F0 RID: 5616
	[PrefetchComponent]
	public global::CameraFX cameraFX;

	// Token: 0x040015F1 RID: 5617
	[PrefetchComponent]
	public Camera camera;

	// Token: 0x040015F2 RID: 5618
	public global::KindOfCamera kindOfCamera;

	// Token: 0x040015F3 RID: 5619
	public global::SharedCameraMode cameraMode;

	// Token: 0x040015F4 RID: 5620
	private static global::CameraMount top;

	// Token: 0x040015F5 RID: 5621
	[SerializeField]
	private int importance;

	// Token: 0x040015F6 RID: 5622
	[SerializeField]
	private bool autoBind;

	// Token: 0x040015F7 RID: 5623
	[NonSerialized]
	private bool awoke;

	// Token: 0x040015F8 RID: 5624
	[NonSerialized]
	private bool bound;

	// Token: 0x040015F9 RID: 5625
	[NonSerialized]
	private bool destroyed;

	// Token: 0x040015FA RID: 5626
	private static global::CameraMount temporaryCameraMount;

	// Token: 0x040015FB RID: 5627
	private static global::CameraMount temporaryCameraMountSource;

	// Token: 0x040015FC RID: 5628
	private static GameObject temporaryCameraMountGameObject;

	// Token: 0x040015FD RID: 5629
	private static Transform temporaryCameraMountParent;

	// Token: 0x040015FE RID: 5630
	private static bool temporaryCameraMountHasParent;

	// Token: 0x040015FF RID: 5631
	private static bool creatingTemporaryCameraMount;

	// Token: 0x04001600 RID: 5632
	private static bool createdTemporaryCameraMount;

	// Token: 0x02000506 RID: 1286
	private static class QUEUE_LATE
	{
		// Token: 0x04001602 RID: 5634
		public static readonly Stack<global::CameraMount> queue = new Stack<global::CameraMount>();
	}

	// Token: 0x02000507 RID: 1287
	private static class WORK_LATE
	{
		// Token: 0x04001603 RID: 5635
		public static readonly List<global::CameraMount> list = new List<global::CameraMount>();
	}
}
