using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000532 RID: 1330
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[global::InterfaceDriverComponent(typeof(global::IContextRequestable), "_contextRequestable", "contextRequestable", AlwaysSaveDisabled = true, SearchRoute = (global::InterfaceSearchRoute.GameObject | global::InterfaceSearchRoute.Parents), AdditionalProperties = "renderer;meshFilter")]
public class ContextSprite : MonoBehaviour
{
	// Token: 0x170009B3 RID: 2483
	// (get) Token: 0x06002C83 RID: 11395 RVA: 0x000A689C File Offset: 0x000A4A9C
	public static int layer
	{
		get
		{
			return global::ContextSprite.layerinfo.index;
		}
	}

	// Token: 0x170009B4 RID: 2484
	// (get) Token: 0x06002C84 RID: 11396 RVA: 0x000A68A4 File Offset: 0x000A4AA4
	public static int layerMask
	{
		get
		{
			return global::ContextSprite.layerinfo.mask;
		}
	}

	// Token: 0x06002C85 RID: 11397 RVA: 0x000A68AC File Offset: 0x000A4AAC
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

	// Token: 0x06002C86 RID: 11398 RVA: 0x000A6928 File Offset: 0x000A4B28
	private static bool CalculateFadeDim(ref double fade, float elapsed)
	{
		if (fade < 0.15)
		{
			if (global::ContextSprite.CalculateFadeIn(ref fade, elapsed))
			{
				if (fade > 0.15)
				{
					fade = 0.15;
				}
				return true;
			}
		}
		else if (fade > 0.15 && global::ContextSprite.CalculateFadeOut(ref fade, elapsed))
		{
			if (fade < 0.15)
			{
				fade = 0.15;
			}
			return true;
		}
		return false;
	}

	// Token: 0x06002C87 RID: 11399 RVA: 0x000A69B0 File Offset: 0x000A4BB0
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

	// Token: 0x06002C88 RID: 11400 RVA: 0x000A6A34 File Offset: 0x000A4C34
	public static void UpdateSpriteFading(Camera camera)
	{
		if (global::ContextSprite.gInit && camera)
		{
			global::ContextSprite.g.Step(camera);
		}
	}

	// Token: 0x06002C89 RID: 11401 RVA: 0x000A6A54 File Offset: 0x000A4C54
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
		if ((this.requestable = (this.contextRequestable as global::IContextRequestable)) == null)
		{
			Debug.LogError("Context Requestable is not a IContextRequestable", base.gameObject);
			Object.Destroy(this);
			return;
		}
		if (!base.transform.IsChildOf(this.contextRequestable.transform))
		{
			Debug.LogWarning(string.Format("Sprite for {0} is not a child of {0}.", this.contextRequestable), this);
		}
		this.requestableVisibility = (this.contextRequestable as global::IContextRequestableVisibility);
		this.requestableIsVisibility = (this.requestableVisibility != null);
		this.requestableStatus = (this.contextRequestable as global::IContextRequestableStatus);
		this.requestableHasStatus = (this.requestableStatus != null);
		this.renderer.SetPropertyBlock(this.materialProperties = new MaterialPropertyBlock());
	}

	// Token: 0x06002C8A RID: 11402 RVA: 0x000A6B88 File Offset: 0x000A4D88
	private void UpdateMaterialProperties()
	{
		float num = Mathf.Clamp01((float)this.fade);
		if (num != this.lastBoundFade)
		{
			this.materialProperties.Clear();
			this.materialProperties.AddFloat(global::ContextSprite.matHelper.fadeProp, num);
			this.lastBoundFade = num;
			this.renderer.SetPropertyBlock(this.materialProperties);
		}
	}

	// Token: 0x06002C8B RID: 11403 RVA: 0x000A6BE4 File Offset: 0x000A4DE4
	private bool SearchForContextRequestable(out MonoBehaviour impl)
	{
		global::Contextual contextual;
		if (global::Contextual.FindUp(base.transform, out contextual))
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

	// Token: 0x06002C8C RID: 11404 RVA: 0x000A6C20 File Offset: 0x000A4E20
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

	// Token: 0x06002C8D RID: 11405 RVA: 0x000A6C98 File Offset: 0x000A4E98
	private static bool CheckRelation(Collider collider, Rigidbody rigidbody, Transform self)
	{
		return collider.transform.IsChildOf(self) || self.IsChildOf(collider.transform) || (rigidbody && collider.transform != rigidbody.transform && (rigidbody.transform.IsChildOf(self) || self.IsChildOf(rigidbody.transform)));
	}

	// Token: 0x06002C8E RID: 11406 RVA: 0x000A6D14 File Offset: 0x000A4F14
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

	// Token: 0x06002C8F RID: 11407 RVA: 0x000A6DCC File Offset: 0x000A4FCC
	private void OnBecameVisible()
	{
		if (!this.selfVisible)
		{
			global::ContextSprite.g.Add(this);
			this.selfVisible = true;
			if (this.requestableIsVisibility && this.contextRequestable)
			{
				this.requestableVisibility.OnContextVisibilityChanged(this, true);
			}
		}
	}

	// Token: 0x06002C90 RID: 11408 RVA: 0x000A6E1C File Offset: 0x000A501C
	private IEnumerator Retry()
	{
		this.renderer.enabled = false;
		yield return global::ContextSprite.r.wait;
		this.renderer.enabled = true;
		yield break;
	}

	// Token: 0x06002C91 RID: 11409 RVA: 0x000A6E38 File Offset: 0x000A5038
	private void OnBecameInvisible()
	{
		if (this.selfVisible)
		{
			this.selfVisible = false;
			global::ContextSprite.g.Remove(this);
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

	// Token: 0x06002C92 RID: 11410 RVA: 0x000A6E9C File Offset: 0x000A509C
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

	// Token: 0x06002C93 RID: 11411 RVA: 0x000A6EFC File Offset: 0x000A50FC
	public static bool Raycast(Ray ray, out global::ContextSprite sprite)
	{
		bool result = false;
		sprite = null;
		float num = float.PositiveInfinity;
		foreach (global::ContextSprite contextSprite in global::ContextSprite.g.visible)
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

	// Token: 0x170009B5 RID: 2485
	// (get) Token: 0x06002C94 RID: 11412 RVA: 0x000A702C File Offset: 0x000A522C
	public static global::ContextSprite.VisibleList AllVisible
	{
		get
		{
			return global::ContextSprite.visibleList;
		}
	}

	// Token: 0x06002C95 RID: 11413 RVA: 0x000A7034 File Offset: 0x000A5234
	public static IEnumerable<global::ContextSprite> AllVisibleForRequestable(global::IContextRequestableVisibility requestable)
	{
		MonoBehaviour monoBehaviour;
		if (global::ContextSprite.g.visible.Count == 0 || !(monoBehaviour = (requestable as MonoBehaviour)))
		{
			return global::ContextSprite.empty;
		}
		return global::ContextSprite.AllVisibleForRequestable(monoBehaviour);
	}

	// Token: 0x06002C96 RID: 11414 RVA: 0x000A7070 File Offset: 0x000A5270
	private static IEnumerable<global::ContextSprite> AllVisibleForRequestable(MonoBehaviour requestable)
	{
		foreach (global::ContextSprite sprite in global::ContextSprite.g.visible)
		{
			if (sprite.contextRequestable == requestable)
			{
				yield return sprite;
			}
		}
		yield break;
	}

	// Token: 0x06002C97 RID: 11415 RVA: 0x000A709C File Offset: 0x000A529C
	public static bool FindSprite(Component component, out global::ContextSprite sprite)
	{
		if (component is global::ContextSprite)
		{
			sprite = (global::ContextSprite)component;
			return true;
		}
		if (component is global::IContextRequestable)
		{
			sprite = component.GetComponentInChildren<global::ContextSprite>();
			return sprite && ((!sprite.contextRequestable) ? sprite._contextRequestable : sprite.contextRequestable) == component;
		}
		sprite = component.GetComponentInChildren<global::ContextSprite>();
		return sprite;
	}

	// Token: 0x0400168B RID: 5771
	private const double kFadeInRate = 8.0;

	// Token: 0x0400168C RID: 5772
	private const double kFadeOutRate = 8.0;

	// Token: 0x0400168D RID: 5773
	private const double kMinFade = 0.0;

	// Token: 0x0400168E RID: 5774
	private const double kMaxFade = 1.2;

	// Token: 0x0400168F RID: 5775
	private const double kGhostFade = 0.15;

	// Token: 0x04001690 RID: 5776
	private const double kFadeDurationInFull = 0.15;

	// Token: 0x04001691 RID: 5777
	private const double kFadeDurationOutFull = 0.15;

	// Token: 0x04001692 RID: 5778
	private const float kRayDistance = 5f;

	// Token: 0x04001693 RID: 5779
	private static bool gInit;

	// Token: 0x04001694 RID: 5780
	private float timeRemoved;

	// Token: 0x04001695 RID: 5781
	[HideInInspector]
	[SerializeField]
	private MonoBehaviour _contextRequestable;

	// Token: 0x04001696 RID: 5782
	private MonoBehaviour contextRequestable;

	// Token: 0x04001697 RID: 5783
	[PrefetchComponent]
	public MeshFilter meshFilter;

	// Token: 0x04001698 RID: 5784
	[PrefetchComponent]
	public MeshRenderer renderer;

	// Token: 0x04001699 RID: 5785
	private global::IContextRequestable requestable;

	// Token: 0x0400169A RID: 5786
	private global::IContextRequestableVisibility requestableVisibility;

	// Token: 0x0400169B RID: 5787
	private global::IContextRequestableStatus requestableStatus;

	// Token: 0x0400169C RID: 5788
	private bool requestableIsVisibility;

	// Token: 0x0400169D RID: 5789
	private bool requestableHasStatus;

	// Token: 0x0400169E RID: 5790
	private bool selfVisible;

	// Token: 0x0400169F RID: 5791
	private bool denied;

	// Token: 0x040016A0 RID: 5792
	private double fade;

	// Token: 0x040016A1 RID: 5793
	private MaterialPropertyBlock materialProperties;

	// Token: 0x040016A2 RID: 5794
	private float lastBoundFade = float.NegativeInfinity;

	// Token: 0x040016A3 RID: 5795
	private static readonly global::ContextSprite.VisibleList visibleList = new global::ContextSprite.VisibleList();

	// Token: 0x040016A4 RID: 5796
	private static global::ContextSprite[] empty = new global::ContextSprite[0];

	// Token: 0x02000533 RID: 1331
	private static class layerinfo
	{
		// Token: 0x040016A5 RID: 5797
		public static readonly int index = LayerMask.NameToLayer("Sprite");

		// Token: 0x040016A6 RID: 5798
		public static readonly int mask = 1 << global::ContextSprite.layerinfo.index;
	}

	// Token: 0x02000534 RID: 1332
	private static class g
	{
		// Token: 0x06002C99 RID: 11417 RVA: 0x000A7140 File Offset: 0x000A5340
		static g()
		{
			global::ContextSprite.gInit = true;
		}

		// Token: 0x06002C9A RID: 11418 RVA: 0x000A7174 File Offset: 0x000A5374
		public static void Add(global::ContextSprite sprite)
		{
			global::ContextSprite.g.visible.Add(sprite);
			global::ContextSprite.g.count++;
			HashSet<global::ContextSprite> hashSet;
			if (!global::ContextSprite.g.requestableVisibleSprites.TryGetValue(sprite.contextRequestable, out hashSet))
			{
				hashSet = ((global::ContextSprite.g.hashRecycle.Count <= 0) ? new HashSet<global::ContextSprite>() : global::ContextSprite.g.hashRecycle.Dequeue());
				global::ContextSprite.g.requestableVisibleSprites[sprite.contextRequestable] = hashSet;
			}
			hashSet.Add(sprite);
			if (global::ContextSprite.CalculateFadeOut(ref sprite.fade, Time.time - sprite.timeRemoved))
			{
				sprite.UpdateMaterialProperties();
			}
		}

		// Token: 0x06002C9B RID: 11419 RVA: 0x000A7210 File Offset: 0x000A5410
		public static void Step(Camera camera)
		{
			if (global::ContextSprite.g.count > 0)
			{
				float deltaTime = Time.deltaTime;
				if (deltaTime <= 0f)
				{
					return;
				}
				int layerMask = 525313;
				if (global::RPOS.hideSprites)
				{
					foreach (global::ContextSprite contextSprite in global::ContextSprite.g.visible)
					{
						if (global::ContextSprite.CalculateFadeOut(ref contextSprite.fade, deltaTime))
						{
							contextSprite.UpdateMaterialProperties();
						}
					}
				}
				else
				{
					foreach (global::ContextSprite contextSprite2 in global::ContextSprite.g.visible)
					{
						bool flag;
						if (contextSprite2.requestableHasStatus)
						{
							global::ContextStatusFlags contextStatusFlags = contextSprite2.requestableStatus.ContextStatusPoll() & (global::ContextStatusFlags.SpriteFlag0 | global::ContextStatusFlags.SpriteFlag1);
							global::ContextStatusFlags contextStatusFlags2 = contextStatusFlags;
							if (contextStatusFlags2 == (global::ContextStatusFlags)0)
							{
								goto IL_F1;
							}
							if (contextStatusFlags2 != global::ContextStatusFlags.SpriteFlag0)
							{
								if (contextStatusFlags2 == global::ContextStatusFlags.SpriteFlag1)
								{
									if (global::ContextSprite.CalculateFadeOut(ref contextSprite2.fade, deltaTime))
									{
										contextSprite2.UpdateMaterialProperties();
									}
									continue;
								}
								if (contextStatusFlags2 != (global::ContextStatusFlags.SpriteFlag0 | global::ContextStatusFlags.SpriteFlag1))
								{
									goto IL_F1;
								}
								if (global::ContextSprite.CalculateFadeIn(ref contextSprite2.fade, deltaTime))
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
						if ((num > 0f && (!global::ContextSprite.g.PhysRaycast(ref ray, out raycastHit, num, layerMask) || contextSprite2.IsSeeThrough(ref raycastHit))) ? ((!flag) ? global::ContextSprite.CalculateFadeIn(ref contextSprite2.fade, deltaTime) : global::ContextSprite.CalculateFadeDim(ref contextSprite2.fade, deltaTime)) : global::ContextSprite.CalculateFadeOut(ref contextSprite2.fade, deltaTime))
						{
							contextSprite2.UpdateMaterialProperties();
						}
					}
				}
			}
		}

		// Token: 0x06002C9C RID: 11420 RVA: 0x000A74B4 File Offset: 0x000A56B4
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

		// Token: 0x06002C9D RID: 11421 RVA: 0x000A7510 File Offset: 0x000A5710
		public static void Remove(global::ContextSprite sprite)
		{
			global::ContextSprite.g.visible.Remove(sprite);
			global::ContextSprite.g.count--;
			HashSet<global::ContextSprite> hashSet;
			if (global::ContextSprite.g.requestableVisibleSprites.TryGetValue(sprite.contextRequestable, out hashSet))
			{
				if (hashSet.Count == 1)
				{
					hashSet.Clear();
					if (global::ContextSprite.g.hashRecycle.Count < 5)
					{
						global::ContextSprite.g.hashRecycle.Enqueue(hashSet);
					}
					global::ContextSprite.g.requestableVisibleSprites.Remove(sprite.contextRequestable);
				}
				else
				{
					hashSet.Remove(sprite);
				}
			}
			sprite.timeRemoved = Time.time;
		}

		// Token: 0x040016A7 RID: 5799
		private const int kMaxRecycleCount = 5;

		// Token: 0x040016A8 RID: 5800
		public static HashSet<global::ContextSprite> visible = new HashSet<global::ContextSprite>();

		// Token: 0x040016A9 RID: 5801
		public static Queue<HashSet<global::ContextSprite>> hashRecycle = new Queue<HashSet<global::ContextSprite>>();

		// Token: 0x040016AA RID: 5802
		public static Dictionary<MonoBehaviour, HashSet<global::ContextSprite>> requestableVisibleSprites = new Dictionary<MonoBehaviour, HashSet<global::ContextSprite>>();

		// Token: 0x040016AB RID: 5803
		private static int count;
	}

	// Token: 0x02000535 RID: 1333
	private static class matHelper
	{
		// Token: 0x040016AC RID: 5804
		public static int fadeProp = Shader.PropertyToID("_Fade");
	}

	// Token: 0x02000536 RID: 1334
	private static class r
	{
		// Token: 0x040016AD RID: 5805
		public static WaitForEndOfFrame wait = new WaitForEndOfFrame();
	}

	// Token: 0x02000537 RID: 1335
	public sealed class VisibleList : IEnumerable, IEnumerable<global::ContextSprite>
	{
		// Token: 0x06002CA0 RID: 11424 RVA: 0x000A75C4 File Offset: 0x000A57C4
		internal VisibleList()
		{
		}

		// Token: 0x06002CA1 RID: 11425 RVA: 0x000A75CC File Offset: 0x000A57CC
		IEnumerator<global::ContextSprite> IEnumerable<global::ContextSprite>.GetEnumerator()
		{
			return ((IEnumerable<global::ContextSprite>)global::ContextSprite.g.visible).GetEnumerator();
		}

		// Token: 0x06002CA2 RID: 11426 RVA: 0x000A75D8 File Offset: 0x000A57D8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)global::ContextSprite.g.visible).GetEnumerator();
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06002CA3 RID: 11427 RVA: 0x000A75E4 File Offset: 0x000A57E4
		public int Count
		{
			get
			{
				return global::ContextSprite.g.visible.Count;
			}
		}

		// Token: 0x06002CA4 RID: 11428 RVA: 0x000A75F0 File Offset: 0x000A57F0
		public bool Contains(global::ContextSprite sprite)
		{
			return sprite && sprite.selfVisible && global::ContextSprite.g.visible.Contains(sprite);
		}

		// Token: 0x06002CA5 RID: 11429 RVA: 0x000A7624 File Offset: 0x000A5824
		public HashSet<global::ContextSprite>.Enumerator GetEnumerator()
		{
			return global::ContextSprite.g.visible.GetEnumerator();
		}
	}
}
