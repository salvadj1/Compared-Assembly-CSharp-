using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200047C RID: 1148
[InterfaceDriverComponent(typeof(IContextRequestable), "_contextRequestable", "contextRequestable", AlwaysSaveDisabled = true, SearchRoute = (InterfaceSearchRoute.GameObject | InterfaceSearchRoute.Parents), AdditionalProperties = "renderer;meshFilter")]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class ContextSprite : MonoBehaviour
{
	// Token: 0x1700094B RID: 2379
	// (get) Token: 0x060028F3 RID: 10483 RVA: 0x000A091C File Offset: 0x0009EB1C
	public static int layer
	{
		get
		{
			return ContextSprite.layerinfo.index;
		}
	}

	// Token: 0x1700094C RID: 2380
	// (get) Token: 0x060028F4 RID: 10484 RVA: 0x000A0924 File Offset: 0x0009EB24
	public static int layerMask
	{
		get
		{
			return ContextSprite.layerinfo.mask;
		}
	}

	// Token: 0x060028F5 RID: 10485 RVA: 0x000A092C File Offset: 0x0009EB2C
	private static bool CalculateFadeOut(ref double fade, float elapsed)
	{
		if ((double)elapsed <= 0.0)
		{
			return false;
		}
		if (fade < 0.0)
		{
			fade = 0.0;
			return true;
		}
		if (fade == 0.0)
		{
			return false;
		}
		double num = (double)elapsed / 0.15;
		if (num >= fade)
		{
			fade = 0.0;
		}
		else
		{
			fade -= num;
		}
		return true;
	}

	// Token: 0x060028F6 RID: 10486 RVA: 0x000A09A8 File Offset: 0x0009EBA8
	private static bool CalculateFadeDim(ref double fade, float elapsed)
	{
		if (fade < 0.15)
		{
			if (ContextSprite.CalculateFadeIn(ref fade, elapsed))
			{
				if (fade > 0.15)
				{
					fade = 0.15;
				}
				return true;
			}
		}
		else if (fade > 0.15 && ContextSprite.CalculateFadeOut(ref fade, elapsed))
		{
			if (fade < 0.15)
			{
				fade = 0.15;
			}
			return true;
		}
		return false;
	}

	// Token: 0x060028F7 RID: 10487 RVA: 0x000A0A30 File Offset: 0x0009EC30
	private static bool CalculateFadeIn(ref double fade, float elapsed)
	{
		if ((double)elapsed <= 0.0)
		{
			return false;
		}
		if (fade > 1.2)
		{
			fade = 1.2;
			return true;
		}
		if (fade == 1.2)
		{
			return false;
		}
		double num = (double)elapsed / 0.15;
		if (1.2 - fade <= num)
		{
			fade = 1.2;
		}
		else
		{
			fade += num;
		}
		return true;
	}

	// Token: 0x060028F8 RID: 10488 RVA: 0x000A0AB4 File Offset: 0x0009ECB4
	public static void UpdateSpriteFading(Camera camera)
	{
		if (ContextSprite.gInit && camera)
		{
			ContextSprite.g.Step(camera);
		}
	}

	// Token: 0x060028F9 RID: 10489 RVA: 0x000A0AD4 File Offset: 0x0009ECD4
	private void Awake()
	{
		this.contextRequestable = this._contextRequestable;
		if (!this.contextRequestable)
		{
			if (!this.SearchForContextRequestable(out this.contextRequestable))
			{
				Debug.LogError("Could not locate a IContextRequestable! -- destroying self.(component)", base.gameObject);
				Object.Destroy(this);
				return;
			}
			Debug.LogWarning("Please set the interface in inspector! had to search for it!", this.contextRequestable);
		}
		else
		{
			this._contextRequestable = null;
		}
		if ((this.requestable = (this.contextRequestable as IContextRequestable)) == null)
		{
			Debug.LogError("Context Requestable is not a IContextRequestable", base.gameObject);
			Object.Destroy(this);
			return;
		}
		if (!base.transform.IsChildOf(this.contextRequestable.transform))
		{
			Debug.LogWarning(string.Format("Sprite for {0} is not a child of {0}.", this.contextRequestable), this);
		}
		this.requestableVisibility = (this.contextRequestable as IContextRequestableVisibility);
		this.requestableIsVisibility = (this.requestableVisibility != null);
		this.requestableStatus = (this.contextRequestable as IContextRequestableStatus);
		this.requestableHasStatus = (this.requestableStatus != null);
		this.renderer.SetPropertyBlock(this.materialProperties = new MaterialPropertyBlock());
	}

	// Token: 0x060028FA RID: 10490 RVA: 0x000A0C08 File Offset: 0x0009EE08
	private void UpdateMaterialProperties()
	{
		float num = Mathf.Clamp01((float)this.fade);
		if (num != this.lastBoundFade)
		{
			this.materialProperties.Clear();
			this.materialProperties.AddFloat(ContextSprite.matHelper.fadeProp, num);
			this.lastBoundFade = num;
			this.renderer.SetPropertyBlock(this.materialProperties);
		}
	}

	// Token: 0x060028FB RID: 10491 RVA: 0x000A0C64 File Offset: 0x0009EE64
	private bool SearchForContextRequestable(out MonoBehaviour impl)
	{
		Contextual contextual;
		if (Contextual.FindUp(base.transform, out contextual))
		{
			MonoBehaviour implementor;
			impl = (implementor = contextual.implementor);
			if (implementor)
			{
				return true;
			}
		}
		impl = null;
		return false;
	}

	// Token: 0x060028FC RID: 10492 RVA: 0x000A0CA0 File Offset: 0x0009EEA0
	private void Reset()
	{
		if (!this.renderer)
		{
			this.renderer = (base.renderer as MeshRenderer);
		}
		if (!this.meshFilter)
		{
			this.meshFilter = base.GetComponent<MeshFilter>();
		}
		if (!this._contextRequestable && !this.SearchForContextRequestable(out this._contextRequestable))
		{
			Debug.LogWarning("Please add a script implementing IContextRequestable on this or a parent game object", this);
		}
	}

	// Token: 0x060028FD RID: 10493 RVA: 0x000A0D18 File Offset: 0x0009EF18
	private static bool CheckRelation(Collider collider, Rigidbody rigidbody, Transform self)
	{
		return collider.transform.IsChildOf(self) || self.IsChildOf(collider.transform) || (rigidbody && collider.transform != rigidbody.transform && (rigidbody.transform.IsChildOf(self) || self.IsChildOf(rigidbody.transform)));
	}

	// Token: 0x060028FE RID: 10494 RVA: 0x000A0D94 File Offset: 0x0009EF94
	private bool IsSeeThrough(ref RaycastHit hit)
	{
		Transform transform = base.transform;
		Transform transform3;
		if (this.contextRequestable)
		{
			Transform transform2 = this.contextRequestable.transform;
			if (transform != transform2)
			{
				if (transform.IsChildOf(transform2))
				{
					transform = transform2;
				}
				else if (!transform2.IsChildOf(transform))
				{
					transform3 = hit.collider.transform;
					return transform3 == transform2 || transform3 == transform || transform3.IsChildOf(transform) || transform3.IsChildOf(transform2);
				}
			}
		}
		transform3 = hit.collider.transform;
		return transform3 == transform || transform3.IsChildOf(transform);
	}

	// Token: 0x060028FF RID: 10495 RVA: 0x000A0E4C File Offset: 0x0009F04C
	private void OnBecameVisible()
	{
		if (!this.selfVisible)
		{
			ContextSprite.g.Add(this);
			this.selfVisible = true;
			if (this.requestableIsVisibility && this.contextRequestable)
			{
				this.requestableVisibility.OnContextVisibilityChanged(this, true);
			}
		}
	}

	// Token: 0x06002900 RID: 10496 RVA: 0x000A0E9C File Offset: 0x0009F09C
	private IEnumerator Retry()
	{
		this.renderer.enabled = false;
		yield return ContextSprite.r.wait;
		this.renderer.enabled = true;
		yield break;
	}

	// Token: 0x06002901 RID: 10497 RVA: 0x000A0EB8 File Offset: 0x0009F0B8
	private void OnBecameInvisible()
	{
		if (this.selfVisible)
		{
			this.selfVisible = false;
			ContextSprite.g.Remove(this);
			if (this.requestableIsVisibility && this.contextRequestable)
			{
				this.requestableVisibility.OnContextVisibilityChanged(this, false);
			}
		}
		else if (this.denied)
		{
			this.denied = false;
		}
	}

	// Token: 0x06002902 RID: 10498 RVA: 0x000A0F1C File Offset: 0x0009F11C
	private void OnDestroy()
	{
		try
		{
			this.OnBecameInvisible();
		}
		finally
		{
			this.contextRequestable = null;
			this.requestable = null;
			this.requestableVisibility = null;
			this.requestableIsVisibility = false;
			this.requestableStatus = null;
			this.requestableHasStatus = false;
		}
	}

	// Token: 0x06002903 RID: 10499 RVA: 0x000A0F7C File Offset: 0x0009F17C
	public static bool Raycast(Ray ray, out ContextSprite sprite)
	{
		bool result = false;
		sprite = null;
		float num = float.PositiveInfinity;
		foreach (ContextSprite contextSprite in ContextSprite.g.visible)
		{
			if (contextSprite.contextRequestable)
			{
				Collider collider = contextSprite.contextRequestable.collider;
				if (!collider)
				{
					collider = contextSprite.collider;
				}
				if (collider)
				{
					if (!collider.enabled)
					{
						continue;
					}
					RaycastHit raycastHit;
					if (contextSprite.collider.Raycast(ray, ref raycastHit, 5f))
					{
						float num2 = raycastHit.distance;
						num2 *= num2;
						if (num2 < num)
						{
							result = true;
							num = num2;
							sprite = contextSprite;
						}
					}
				}
				float num3;
				if (contextSprite.renderer.bounds.IntersectRay(ray, ref num3))
				{
					num3 *= num3;
					if (num3 < num)
					{
						result = true;
						num = num3;
						sprite = contextSprite;
					}
				}
			}
		}
		return result;
	}

	// Token: 0x1700094D RID: 2381
	// (get) Token: 0x06002904 RID: 10500 RVA: 0x000A10AC File Offset: 0x0009F2AC
	public static ContextSprite.VisibleList AllVisible
	{
		get
		{
			return ContextSprite.visibleList;
		}
	}

	// Token: 0x06002905 RID: 10501 RVA: 0x000A10B4 File Offset: 0x0009F2B4
	public static IEnumerable<ContextSprite> AllVisibleForRequestable(IContextRequestableVisibility requestable)
	{
		MonoBehaviour monoBehaviour;
		if (ContextSprite.g.visible.Count == 0 || !(monoBehaviour = (requestable as MonoBehaviour)))
		{
			return ContextSprite.empty;
		}
		return ContextSprite.AllVisibleForRequestable(monoBehaviour);
	}

	// Token: 0x06002906 RID: 10502 RVA: 0x000A10F0 File Offset: 0x0009F2F0
	private static IEnumerable<ContextSprite> AllVisibleForRequestable(MonoBehaviour requestable)
	{
		foreach (ContextSprite sprite in ContextSprite.g.visible)
		{
			if (sprite.contextRequestable == requestable)
			{
				yield return sprite;
			}
		}
		yield break;
	}

	// Token: 0x06002907 RID: 10503 RVA: 0x000A111C File Offset: 0x0009F31C
	public static bool FindSprite(Component component, out ContextSprite sprite)
	{
		if (component is ContextSprite)
		{
			sprite = (ContextSprite)component;
			return true;
		}
		if (component is IContextRequestable)
		{
			sprite = component.GetComponentInChildren<ContextSprite>();
			return sprite && ((!sprite.contextRequestable) ? sprite._contextRequestable : sprite.contextRequestable) == component;
		}
		sprite = component.GetComponentInChildren<ContextSprite>();
		return sprite;
	}

	// Token: 0x04001508 RID: 5384
	private const double kFadeInRate = 8.0;

	// Token: 0x04001509 RID: 5385
	private const double kFadeOutRate = 8.0;

	// Token: 0x0400150A RID: 5386
	private const double kMinFade = 0.0;

	// Token: 0x0400150B RID: 5387
	private const double kMaxFade = 1.2;

	// Token: 0x0400150C RID: 5388
	private const double kGhostFade = 0.15;

	// Token: 0x0400150D RID: 5389
	private const double kFadeDurationInFull = 0.15;

	// Token: 0x0400150E RID: 5390
	private const double kFadeDurationOutFull = 0.15;

	// Token: 0x0400150F RID: 5391
	private const float kRayDistance = 5f;

	// Token: 0x04001510 RID: 5392
	private static bool gInit;

	// Token: 0x04001511 RID: 5393
	private float timeRemoved;

	// Token: 0x04001512 RID: 5394
	[SerializeField]
	[HideInInspector]
	private MonoBehaviour _contextRequestable;

	// Token: 0x04001513 RID: 5395
	private MonoBehaviour contextRequestable;

	// Token: 0x04001514 RID: 5396
	[PrefetchComponent]
	public MeshFilter meshFilter;

	// Token: 0x04001515 RID: 5397
	[PrefetchComponent]
	public MeshRenderer renderer;

	// Token: 0x04001516 RID: 5398
	private IContextRequestable requestable;

	// Token: 0x04001517 RID: 5399
	private IContextRequestableVisibility requestableVisibility;

	// Token: 0x04001518 RID: 5400
	private IContextRequestableStatus requestableStatus;

	// Token: 0x04001519 RID: 5401
	private bool requestableIsVisibility;

	// Token: 0x0400151A RID: 5402
	private bool requestableHasStatus;

	// Token: 0x0400151B RID: 5403
	private bool selfVisible;

	// Token: 0x0400151C RID: 5404
	private bool denied;

	// Token: 0x0400151D RID: 5405
	private double fade;

	// Token: 0x0400151E RID: 5406
	private MaterialPropertyBlock materialProperties;

	// Token: 0x0400151F RID: 5407
	private float lastBoundFade = float.NegativeInfinity;

	// Token: 0x04001520 RID: 5408
	private static readonly ContextSprite.VisibleList visibleList = new ContextSprite.VisibleList();

	// Token: 0x04001521 RID: 5409
	private static ContextSprite[] empty = new ContextSprite[0];

	// Token: 0x0200047D RID: 1149
	private static class layerinfo
	{
		// Token: 0x04001522 RID: 5410
		public static readonly int index = LayerMask.NameToLayer("Sprite");

		// Token: 0x04001523 RID: 5411
		public static readonly int mask = 1 << ContextSprite.layerinfo.index;
	}

	// Token: 0x0200047E RID: 1150
	private static class g
	{
		// Token: 0x06002909 RID: 10505 RVA: 0x000A11C0 File Offset: 0x0009F3C0
		static g()
		{
			ContextSprite.gInit = true;
		}

		// Token: 0x0600290A RID: 10506 RVA: 0x000A11F4 File Offset: 0x0009F3F4
		public static void Add(ContextSprite sprite)
		{
			ContextSprite.g.visible.Add(sprite);
			ContextSprite.g.count++;
			HashSet<ContextSprite> hashSet;
			if (!ContextSprite.g.requestableVisibleSprites.TryGetValue(sprite.contextRequestable, out hashSet))
			{
				hashSet = ((ContextSprite.g.hashRecycle.Count <= 0) ? new HashSet<ContextSprite>() : ContextSprite.g.hashRecycle.Dequeue());
				ContextSprite.g.requestableVisibleSprites[sprite.contextRequestable] = hashSet;
			}
			hashSet.Add(sprite);
			if (ContextSprite.CalculateFadeOut(ref sprite.fade, Time.time - sprite.timeRemoved))
			{
				sprite.UpdateMaterialProperties();
			}
		}

		// Token: 0x0600290B RID: 10507 RVA: 0x000A1290 File Offset: 0x0009F490
		public static void Step(Camera camera)
		{
			if (ContextSprite.g.count > 0)
			{
				float deltaTime = Time.deltaTime;
				if (deltaTime <= 0f)
				{
					return;
				}
				int layerMask = 525313;
				if (RPOS.hideSprites)
				{
					foreach (ContextSprite contextSprite in ContextSprite.g.visible)
					{
						if (ContextSprite.CalculateFadeOut(ref contextSprite.fade, deltaTime))
						{
							contextSprite.UpdateMaterialProperties();
						}
					}
				}
				else
				{
					foreach (ContextSprite contextSprite2 in ContextSprite.g.visible)
					{
						bool flag;
						if (contextSprite2.requestableHasStatus)
						{
							ContextStatusFlags contextStatusFlags = contextSprite2.requestableStatus.ContextStatusPoll() & (ContextStatusFlags.SpriteFlag0 | ContextStatusFlags.SpriteFlag1);
							ContextStatusFlags contextStatusFlags2 = contextStatusFlags;
							if (contextStatusFlags2 == (ContextStatusFlags)0)
							{
								goto IL_F1;
							}
							if (contextStatusFlags2 != ContextStatusFlags.SpriteFlag0)
							{
								if (contextStatusFlags2 == ContextStatusFlags.SpriteFlag1)
								{
									if (ContextSprite.CalculateFadeOut(ref contextSprite2.fade, deltaTime))
									{
										contextSprite2.UpdateMaterialProperties();
									}
									continue;
								}
								if (contextStatusFlags2 != (ContextStatusFlags.SpriteFlag0 | ContextStatusFlags.SpriteFlag1))
								{
									goto IL_F1;
								}
								if (ContextSprite.CalculateFadeIn(ref contextSprite2.fade, deltaTime))
								{
									contextSprite2.UpdateMaterialProperties();
								}
								continue;
							}
							else
							{
								flag = true;
							}
							goto IL_145;
							IL_F1:
							flag = false;
						}
						else
						{
							flag = false;
						}
						IL_145:
						Vector3 position = contextSprite2.transform.position;
						Vector3 vector = camera.WorldToScreenPoint(position);
						Ray ray = camera.ScreenPointToRay(vector);
						Vector3 direction = ray.direction;
						Vector3 origin = ray.origin;
						float num = position.x * direction.x + position.y * direction.y + position.z * direction.z - (origin.x * direction.x + origin.y * direction.y + origin.z * direction.z);
						RaycastHit raycastHit;
						if ((num > 0f && (!ContextSprite.g.PhysRaycast(ref ray, out raycastHit, num, layerMask) || contextSprite2.IsSeeThrough(ref raycastHit))) ? ((!flag) ? ContextSprite.CalculateFadeIn(ref contextSprite2.fade, deltaTime) : ContextSprite.CalculateFadeDim(ref contextSprite2.fade, deltaTime)) : ContextSprite.CalculateFadeOut(ref contextSprite2.fade, deltaTime))
						{
							contextSprite2.UpdateMaterialProperties();
						}
					}
				}
			}
		}

		// Token: 0x0600290C RID: 10508 RVA: 0x000A1534 File Offset: 0x0009F734
		private static bool PhysRaycast(ref Ray ray, out RaycastHit hit, float distanceTo, int layerMask)
		{
			if (Physics.Raycast(ray, ref hit, distanceTo, layerMask))
			{
				Debug.DrawLine(ray.origin, ray.GetPoint(hit.distance), Color.green);
				Debug.DrawLine(ray.GetPoint(hit.distance), ray.GetPoint(distanceTo), Color.red);
				return true;
			}
			return false;
		}

		// Token: 0x0600290D RID: 10509 RVA: 0x000A1590 File Offset: 0x0009F790
		public static void Remove(ContextSprite sprite)
		{
			ContextSprite.g.visible.Remove(sprite);
			ContextSprite.g.count--;
			HashSet<ContextSprite> hashSet;
			if (ContextSprite.g.requestableVisibleSprites.TryGetValue(sprite.contextRequestable, out hashSet))
			{
				if (hashSet.Count == 1)
				{
					hashSet.Clear();
					if (ContextSprite.g.hashRecycle.Count < 5)
					{
						ContextSprite.g.hashRecycle.Enqueue(hashSet);
					}
					ContextSprite.g.requestableVisibleSprites.Remove(sprite.contextRequestable);
				}
				else
				{
					hashSet.Remove(sprite);
				}
			}
			sprite.timeRemoved = Time.time;
		}

		// Token: 0x04001524 RID: 5412
		private const int kMaxRecycleCount = 5;

		// Token: 0x04001525 RID: 5413
		public static HashSet<ContextSprite> visible = new HashSet<ContextSprite>();

		// Token: 0x04001526 RID: 5414
		public static Queue<HashSet<ContextSprite>> hashRecycle = new Queue<HashSet<ContextSprite>>();

		// Token: 0x04001527 RID: 5415
		public static Dictionary<MonoBehaviour, HashSet<ContextSprite>> requestableVisibleSprites = new Dictionary<MonoBehaviour, HashSet<ContextSprite>>();

		// Token: 0x04001528 RID: 5416
		private static int count;
	}

	// Token: 0x0200047F RID: 1151
	private static class matHelper
	{
		// Token: 0x04001529 RID: 5417
		public static int fadeProp = Shader.PropertyToID("_Fade");
	}

	// Token: 0x02000480 RID: 1152
	private static class r
	{
		// Token: 0x0400152A RID: 5418
		public static WaitForEndOfFrame wait = new WaitForEndOfFrame();
	}

	// Token: 0x02000481 RID: 1153
	public sealed class VisibleList : IEnumerable, IEnumerable<ContextSprite>
	{
		// Token: 0x06002910 RID: 10512 RVA: 0x000A1644 File Offset: 0x0009F844
		internal VisibleList()
		{
		}

		// Token: 0x06002911 RID: 10513 RVA: 0x000A164C File Offset: 0x0009F84C
		IEnumerator<ContextSprite> IEnumerable<ContextSprite>.GetEnumerator()
		{
			return ((IEnumerable<ContextSprite>)ContextSprite.g.visible).GetEnumerator();
		}

		// Token: 0x06002912 RID: 10514 RVA: 0x000A1658 File Offset: 0x0009F858
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)ContextSprite.g.visible).GetEnumerator();
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x06002913 RID: 10515 RVA: 0x000A1664 File Offset: 0x0009F864
		public int Count
		{
			get
			{
				return ContextSprite.g.visible.Count;
			}
		}

		// Token: 0x06002914 RID: 10516 RVA: 0x000A1670 File Offset: 0x0009F870
		public bool Contains(ContextSprite sprite)
		{
			return sprite && sprite.selfVisible && ContextSprite.g.visible.Contains(sprite);
		}

		// Token: 0x06002915 RID: 10517 RVA: 0x000A16A4 File Offset: 0x0009F8A4
		public HashSet<ContextSprite>.Enumerator GetEnumerator()
		{
			return ContextSprite.g.visible.GetEnumerator();
		}
	}
}
