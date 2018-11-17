using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// Token: 0x020003A2 RID: 930
[AddComponentMenu("Vis/Node")]
public class VisNode : IDLocal
{
	// Token: 0x1700089D RID: 2205
	// (set) Token: 0x0600233C RID: 9020 RVA: 0x000878C4 File Offset: 0x00085AC4
	internal VisReactor __reactor
	{
		set
		{
			this.reactor = value;
		}
	}

	// Token: 0x1700089E RID: 2206
	// (get) Token: 0x0600233D RID: 9021 RVA: 0x000878D0 File Offset: 0x00085AD0
	// (set) Token: 0x0600233E RID: 9022 RVA: 0x000878D8 File Offset: 0x00085AD8
	public float arc
	{
		get
		{
			return this.dotArc;
		}
		set
		{
			this.dotArc = Mathf.Clamp01(value);
		}
	}

	// Token: 0x1700089F RID: 2207
	// (get) Token: 0x0600233F RID: 9023 RVA: 0x000878E8 File Offset: 0x00085AE8
	// (set) Token: 0x06002340 RID: 9024 RVA: 0x000878F0 File Offset: 0x00085AF0
	public float radius
	{
		get
		{
			return this.distance;
		}
		set
		{
			this.distance = value;
		}
	}

	// Token: 0x170008A0 RID: 2208
	// (get) Token: 0x06002341 RID: 9025 RVA: 0x000878FC File Offset: 0x00085AFC
	// (set) Token: 0x06002342 RID: 9026 RVA: 0x00087924 File Offset: 0x00085B24
	public Vis.Mask viewMask
	{
		get
		{
			return new Vis.Mask
			{
				data = this._sightMask
			};
		}
		set
		{
			this._sightMask = value.data;
		}
	}

	// Token: 0x170008A1 RID: 2209
	// (get) Token: 0x06002343 RID: 9027 RVA: 0x00087934 File Offset: 0x00085B34
	// (set) Token: 0x06002344 RID: 9028 RVA: 0x0008795C File Offset: 0x00085B5C
	public Vis.Mask spectMask
	{
		get
		{
			return new Vis.Mask
			{
				data = this._spectMask
			};
		}
		set
		{
			this._spectMask = value.data;
		}
	}

	// Token: 0x170008A2 RID: 2210
	// (get) Token: 0x06002345 RID: 9029 RVA: 0x0008796C File Offset: 0x00085B6C
	// (set) Token: 0x06002346 RID: 9030 RVA: 0x00087994 File Offset: 0x00085B94
	public Vis.Mask traitMask
	{
		get
		{
			return new Vis.Mask
			{
				data = this._traitMask
			};
		}
		set
		{
			this._traitMask = value.data;
		}
	}

	// Token: 0x170008A3 RID: 2211
	// (get) Token: 0x06002347 RID: 9031 RVA: 0x000879A4 File Offset: 0x00085BA4
	public Vis.Mask seenMask
	{
		get
		{
			return new Vis.Mask
			{
				data = this._seeMask
			};
		}
	}

	// Token: 0x170008A4 RID: 2212
	// (get) Token: 0x06002348 RID: 9032 RVA: 0x000879CC File Offset: 0x00085BCC
	public Vis.Stamp stamp
	{
		get
		{
			return this._stamp;
		}
	}

	// Token: 0x170008A5 RID: 2213
	// (get) Token: 0x06002349 RID: 9033 RVA: 0x000879D4 File Offset: 0x00085BD4
	public Vector3 position
	{
		get
		{
			return this._stamp.position;
		}
	}

	// Token: 0x170008A6 RID: 2214
	// (get) Token: 0x0600234A RID: 9034 RVA: 0x000879E4 File Offset: 0x00085BE4
	public Vector3 forward
	{
		get
		{
			return this._stamp.forward;
		}
	}

	// Token: 0x170008A7 RID: 2215
	// (get) Token: 0x0600234B RID: 9035 RVA: 0x000879F4 File Offset: 0x00085BF4
	public Quaternion rotation
	{
		get
		{
			return this._stamp.rotation;
		}
	}

	// Token: 0x170008A8 RID: 2216
	// (get) Token: 0x0600234C RID: 9036 RVA: 0x00087A04 File Offset: 0x00085C04
	public Plane plane
	{
		get
		{
			Vector4 vector = this._stamp.forward;
			return new Plane(new Vector3(vector.x, vector.y, vector.z), vector.w);
		}
	}

	// Token: 0x170008A9 RID: 2217
	// (get) Token: 0x0600234D RID: 9037 RVA: 0x00087A48 File Offset: 0x00085C48
	public int numSight
	{
		get
		{
			return this.sight.count;
		}
	}

	// Token: 0x170008AA RID: 2218
	// (get) Token: 0x0600234E RID: 9038 RVA: 0x00087A58 File Offset: 0x00085C58
	public bool anySight
	{
		get
		{
			return this.sight.any;
		}
	}

	// Token: 0x170008AB RID: 2219
	// (get) Token: 0x0600234F RID: 9039 RVA: 0x00087A68 File Offset: 0x00085C68
	public bool anySightNew
	{
		get
		{
			return this.sight.add;
		}
	}

	// Token: 0x170008AC RID: 2220
	// (get) Token: 0x06002350 RID: 9040 RVA: 0x00087A78 File Offset: 0x00085C78
	public bool anySightLost
	{
		get
		{
			return this.sight.rem;
		}
	}

	// Token: 0x170008AD RID: 2221
	// (get) Token: 0x06002351 RID: 9041 RVA: 0x00087A88 File Offset: 0x00085C88
	public bool anySightHad
	{
		get
		{
			return this.sight.had;
		}
	}

	// Token: 0x170008AE RID: 2222
	// (get) Token: 0x06002352 RID: 9042 RVA: 0x00087A98 File Offset: 0x00085C98
	public int numSpectators
	{
		get
		{
			return this.spect.count;
		}
	}

	// Token: 0x170008AF RID: 2223
	// (get) Token: 0x06002353 RID: 9043 RVA: 0x00087AA8 File Offset: 0x00085CA8
	public bool anySpectators
	{
		get
		{
			return this.spect.any;
		}
	}

	// Token: 0x170008B0 RID: 2224
	// (get) Token: 0x06002354 RID: 9044 RVA: 0x00087AB8 File Offset: 0x00085CB8
	public bool anySpectatorsNew
	{
		get
		{
			return this.spect.add;
		}
	}

	// Token: 0x170008B1 RID: 2225
	// (get) Token: 0x06002355 RID: 9045 RVA: 0x00087AC8 File Offset: 0x00085CC8
	public bool anySpectatorsLost
	{
		get
		{
			return this.spect.rem;
		}
	}

	// Token: 0x170008B2 RID: 2226
	// (get) Token: 0x06002356 RID: 9046 RVA: 0x00087AD8 File Offset: 0x00085CD8
	public bool anySpectatorsHad
	{
		get
		{
			return this.spect.had;
		}
	}

	// Token: 0x06002357 RID: 9047 RVA: 0x00087AE8 File Offset: 0x00085CE8
	public bool CanSeeAny(Vis.Life life)
	{
		return (this._seeMask & (int)life) != 0;
	}

	// Token: 0x06002358 RID: 9048 RVA: 0x00087AF8 File Offset: 0x00085CF8
	public bool CanSeeAny(Vis.Status status)
	{
		return (this._seeMask & (int)((int)status << 8)) != 0;
	}

	// Token: 0x06002359 RID: 9049 RVA: 0x00087B0C File Offset: 0x00085D0C
	public bool CanSeeAny(Vis.Role role)
	{
		return (this._seeMask & (int)((int)role << 24)) != 0;
	}

	// Token: 0x0600235A RID: 9050 RVA: 0x00087B20 File Offset: 0x00085D20
	public bool CanSeeAny(Vis.Mask mask)
	{
		return (this._seeMask & mask.data) != 0;
	}

	// Token: 0x0600235B RID: 9051 RVA: 0x00087B38 File Offset: 0x00085D38
	public bool CanSee(Vis.Trait trait)
	{
		return (this._seeMask & 1 << (int)trait) != 0;
	}

	// Token: 0x0600235C RID: 9052 RVA: 0x00087B50 File Offset: 0x00085D50
	public bool CanSee(Vis.Life life)
	{
		return (this._seeMask & (int)life) == (int)life;
	}

	// Token: 0x0600235D RID: 9053 RVA: 0x00087B60 File Offset: 0x00085D60
	public bool CanSee(Vis.Status status)
	{
		return (this._seeMask >> 8 & (int)status) == (int)status;
	}

	// Token: 0x0600235E RID: 9054 RVA: 0x00087B70 File Offset: 0x00085D70
	public bool CanSee(Vis.Role role)
	{
		return (this._seeMask >> 24 & (int)role) == (int)role;
	}

	// Token: 0x0600235F RID: 9055 RVA: 0x00087B80 File Offset: 0x00085D80
	public bool CanSee(Vis.Mask mask)
	{
		return (this._seeMask & mask.data) == mask.data;
	}

	// Token: 0x06002360 RID: 9056 RVA: 0x00087B9C File Offset: 0x00085D9C
	public bool CanSeeOnly(Vis.Life life)
	{
		return (this._seeMask & 7) == (int)life;
	}

	// Token: 0x06002361 RID: 9057 RVA: 0x00087BAC File Offset: 0x00085DAC
	public bool CanSeeOnly(Vis.Status status)
	{
		return (this._seeMask & 32512) == (int)((int)status << 8);
	}

	// Token: 0x06002362 RID: 9058 RVA: 0x00087BC0 File Offset: 0x00085DC0
	public bool CanSeeOnly(Vis.Role role)
	{
		return (this._seeMask & -16777216) == (int)((int)role << 24);
	}

	// Token: 0x06002363 RID: 9059 RVA: 0x00087BD4 File Offset: 0x00085DD4
	public bool CanSeeOnly(Vis.Mask mask)
	{
		return this._seeMask == mask.data;
	}

	// Token: 0x06002364 RID: 9060 RVA: 0x00087BE8 File Offset: 0x00085DE8
	public bool CanSeeOnly(Vis.Trait trait)
	{
		return this._seeMask == 1 << (int)trait;
	}

	// Token: 0x170008B3 RID: 2227
	// (get) Token: 0x06002365 RID: 9061 RVA: 0x00087BF8 File Offset: 0x00085DF8
	// (set) Token: 0x06002366 RID: 9062 RVA: 0x00087C00 File Offset: 0x00085E00
	public Transform head
	{
		get
		{
			return this._transform;
		}
		set
		{
			if (value)
			{
				this._transform = value;
			}
			else
			{
				this._transform = base.transform;
			}
		}
	}

	// Token: 0x06002367 RID: 9063 RVA: 0x00087C28 File Offset: 0x00085E28
	protected void Reset()
	{
		base.Reset();
		VisReactor component = base.GetComponent<VisReactor>();
		if (component)
		{
			this.reactor = component;
			this.reactor.__visNode = this;
		}
	}

	// Token: 0x06002368 RID: 9064 RVA: 0x00087C60 File Offset: 0x00085E60
	private void Register()
	{
		if (!this.awake || this.active)
		{
			return;
		}
		if (VisManager.guardedUpdate)
		{
			throw new InvalidOperationException("DO NOT INSTANTIATE WHILE VisibilityManager.isUpdatingVisibility!!");
		}
		if (!VisNode.manager)
		{
			VisNode.manager = new GameObject("__Vis", new Type[]
			{
				typeof(VisManager)
			}).GetComponent<VisManager>();
		}
		if (!this.dataConstructed)
		{
			this.sight.list = new ODBSet<VisNode>();
			this.sight.last = new ODBSet<VisNode>();
			this.spect.list = new ODBSet<VisNode>();
			this.spect.last = new ODBSet<VisNode>();
			this.enter = new ODBSet<VisNode>();
			this.exit = new ODBSet<VisNode>();
			this.cleanList = new List<VisNode>();
			this.dataConstructed = true;
		}
		else if (!VisNode.recentlyDisabled.Remove(this))
		{
			VisNode.disabledLastStep.Remove(this);
		}
		this.item = VisNode.db.Register(this);
		this.active = (this.item == this);
	}

	// Token: 0x06002369 RID: 9065 RVA: 0x00087D88 File Offset: 0x00085F88
	private void Unregister()
	{
		if (this.active)
		{
			if (VisManager.guardedUpdate)
			{
				throw new InvalidOperationException("DO NOT OR DISABLE DESTROY WHILE VisibilityManager.isUpdatingVisibility!!");
			}
			VisNode.db.Unregister(ref this.item);
			this.active = (this.item == this);
		}
	}

	// Token: 0x0600236A RID: 9066 RVA: 0x00087DD8 File Offset: 0x00085FD8
	private void Awake()
	{
		this.awake = true;
		if (!this._transform)
		{
			this._transform = base.transform;
		}
		if (base.enabled)
		{
			Debug.LogWarning("VisNode was enabled prior to awake. VisNode's enabled button should always be off when the game is not running");
			this.Register();
		}
		this.histSight.last = 0;
		this.histSpect.last = this._spectMask;
		this.histTrait.last = this._traitMask;
		this.statusHandler = (this.idMain as IVisHandler);
		this.hasStatusHandler = (this.statusHandler != null);
		if (this._class)
		{
			this._handle = this._class.handle;
		}
	}

	// Token: 0x0600236B RID: 9067 RVA: 0x00087E98 File Offset: 0x00086098
	private void OnDestroy()
	{
		if (VisManager.guardedUpdate)
		{
			Debug.LogError("DESTROYING IN GUARDED UPDATE! " + base.name, this);
		}
		this.Unregister();
		VisNode.RemoveNow(this);
	}

	// Token: 0x0600236C RID: 9068 RVA: 0x00087ED4 File Offset: 0x000860D4
	private void OnEnable()
	{
		if (this.awake)
		{
			this.Register();
		}
	}

	// Token: 0x0600236D RID: 9069 RVA: 0x00087EE8 File Offset: 0x000860E8
	private void OnDisable()
	{
		if (this.awake)
		{
			bool flag = this.active;
			this.Unregister();
			if (flag && !this.active)
			{
				VisNode.recentlyDisabled.Add(this);
			}
		}
	}

	// Token: 0x0600236E RID: 9070 RVA: 0x00087F2C File Offset: 0x0008612C
	private static void ResolveSee()
	{
		if (VisNode.operandA.sight.list.Add(VisNode.operandB))
		{
			VisNode visNode = VisNode.operandB;
			visNode.spect.add = (visNode.spect.add | VisNode.operandB.spect.list.Add(VisNode.operandA));
			VisNode.operandA.sight.add = true;
			VisNode.operandA.enter.Add(VisNode.operandB);
		}
	}

	// Token: 0x0600236F RID: 9071 RVA: 0x00087FAC File Offset: 0x000861AC
	private static void ResolveHide()
	{
		if (VisNode.operandA.sight.list.Remove(VisNode.operandB))
		{
			VisNode visNode = VisNode.operandB;
			visNode.spect.rem = (visNode.spect.rem | VisNode.operandB.spect.list.Remove(VisNode.operandA));
			VisNode.operandA.exit.Add(VisNode.operandB);
			VisNode.operandB.cleanList.Add(VisNode.operandA);
		}
	}

	// Token: 0x06002370 RID: 9072 RVA: 0x00088030 File Offset: 0x00086230
	private static void RemoveLinkNow(VisNode node, VisNode didSee)
	{
		if (node.sight.list.Remove(node))
		{
			node.sight.rem = true;
			didSee.spect.rem = (didSee.spect.rem | didSee.spect.list.Remove(node));
		}
		if (!node.sight.last.Remove(didSee))
		{
			node.enter.Remove(didSee);
		}
		else
		{
			didSee.spect.last.Remove(node);
		}
		if ((node.sight.count = node.sight.count - 1) == 0)
		{
			node.sight.any = false;
		}
		if ((didSee.spect.count = didSee.spect.count - 1) == 0)
		{
			didSee.spect.any = false;
		}
	}

	// Token: 0x06002371 RID: 9073 RVA: 0x0008810C File Offset: 0x0008630C
	internal static void RemoveNow(VisNode node)
	{
		if (!node.dataConstructed)
		{
			return;
		}
		if (!VisNode.recentlyDisabled.Remove(node))
		{
			VisNode.disabledLastStep.Remove(node);
		}
		for (int i = 0; i < node.cleanList.Count; i++)
		{
			node.cleanList[i].exit.Remove(node);
		}
		ODBForwardEnumerator<VisNode> enumerator = node.exit.GetEnumerator();
		while (enumerator.MoveNext())
		{
			VisNode current = enumerator.Current;
			current.cleanList.Remove(node);
		}
		enumerator.Dispose();
		node.cleanList.Clear();
		node.cleanList.AddRange(node.sight.list);
		for (int i = 0; i < node.cleanList.Count; i++)
		{
			VisNode.RemoveLinkNow(node, node.cleanList[i]);
		}
		node.cleanList.Clear();
		node.cleanList.AddRange(node.spect.list);
		for (int i = 0; i < node.cleanList.Count; i++)
		{
			VisNode.RemoveLinkNow(node.cleanList[i], node);
		}
		node.cleanList.Clear();
	}

	// Token: 0x06002372 RID: 9074 RVA: 0x00088258 File Offset: 0x00086458
	private static void Copy(ODBSet<VisNode> src, ODBSet<VisNode> dst)
	{
		dst.Clear();
		dst.UnionWith(src);
	}

	// Token: 0x06002373 RID: 9075 RVA: 0x00088268 File Offset: 0x00086468
	private static void Transfer(ODBSet<VisNode> src, ODBSet<VisNode> dst, bool addAny, bool remAny)
	{
		if (addAny)
		{
			if (remAny)
			{
				VisNode.Copy(src, dst);
			}
			else
			{
				dst.UnionWith(src);
			}
		}
		else if (remAny)
		{
			dst.ExceptWith(src);
		}
	}

	// Token: 0x06002374 RID: 9076 RVA: 0x0008829C File Offset: 0x0008649C
	private void Stamp()
	{
		this._stamp.Collect(this._transform);
		VisNode.Transfer(this.sight.list, this.sight.last, this.sight.add, this.sight.rem);
		VisNode.Transfer(this.spect.list, this.spect.last, this.spect.add, this.spect.rem);
		if (this.sight.add)
		{
			this.enter.Clear();
			this.sight.add = false;
		}
		if (this.sight.rem)
		{
			this.exit.Clear();
			this.sight.rem = false;
		}
		this.spect.add = false;
		if (this.spect.rem)
		{
			this.spect.rem = false;
			this.cleanList.Clear();
		}
		if (this.hasStatusHandler)
		{
			this._traitMask = this.statusHandler.VisPoll(this.traitMask).data;
		}
		this.histTrait.Upd(this._traitMask);
		this._sightCurrentMask = 0;
		this.histSight.Upd(this._sightCurrentMask);
		this.histSpect.Upd(this._spectMask);
		this._seeMask = 0;
		this.anySeenTraitChanges = false;
	}

	// Token: 0x06002375 RID: 9077 RVA: 0x00088414 File Offset: 0x00086614
	internal static void Stage1(VisNode self)
	{
		self.Stamp();
	}

	// Token: 0x06002376 RID: 9078 RVA: 0x0008841C File Offset: 0x0008661C
	private static bool LogicSight()
	{
		if (!VisNode.operandB.active)
		{
			return false;
		}
		VisNode.bX = VisNode.operandB._stamp.position.x;
		VisNode.bY = VisNode.operandB._stamp.position.y;
		VisNode.bZ = VisNode.operandB._stamp.position.z;
		VisNode.planeDot = VisNode.bX * VisNode.fX + VisNode.bY * VisNode.fY + VisNode.bZ * VisNode.fZ;
		if (VisNode.planeDot < VisNode.fW || VisNode.planeDot > VisNode.PLANEDOTSIGHT)
		{
			return false;
		}
		VisNode.dX = VisNode.bX - VisNode.pX;
		VisNode.dY = VisNode.bY - VisNode.pY;
		VisNode.dZ = VisNode.bZ - VisNode.pZ;
		VisNode.dV2 = VisNode.dX * VisNode.dX + VisNode.dY * VisNode.dY + VisNode.dZ * VisNode.dZ;
		if (VisNode.dV2 > VisNode.SIGHT2)
		{
			return false;
		}
		if (VisNode.dV2 < 4.203895E-45f)
		{
			return VisNode.FALLBACK_TOO_CLOSE;
		}
		VisNode.dV = Mathf.Sqrt(VisNode.dV2);
		VisNode.nX = VisNode.dX / VisNode.dV;
		VisNode.nY = VisNode.dY / VisNode.dV;
		VisNode.nZ = VisNode.dZ / VisNode.dV;
		VisNode.dot = VisNode.fX * VisNode.nX + VisNode.fY * VisNode.nY + VisNode.fZ * VisNode.nZ;
		return VisNode.DOT < VisNode.dot;
	}

	// Token: 0x06002377 RID: 9079 RVA: 0x000885C0 File Offset: 0x000867C0
	private static void UpdateVis(ODBSibling<VisNode> first_sib)
	{
		VisNode.FALLBACK_TOO_CLOSE = false;
		ODBSibling<VisNode> odbsibling = first_sib;
		do
		{
			VisNode.operandA = odbsibling.item.self;
			odbsibling = odbsibling.item.n;
			if (VisNode.operandA._sightCurrentMask == 0)
			{
				if (VisNode.operandA.sight.any)
				{
					ODBSibling<VisNode> odbsibling2 = VisNode.operandA.sight.last.first;
					do
					{
						VisNode.operandB = odbsibling2.item.self;
						odbsibling2 = odbsibling2.item.n;
						VisNode.ResolveHide();
					}
					while (odbsibling2.has);
					VisNode.operandB = null;
				}
			}
			else
			{
				VisNode.pX = VisNode.operandA._stamp.position.x;
				VisNode.pY = VisNode.operandA._stamp.position.y;
				VisNode.pZ = VisNode.operandA._stamp.position.z;
				VisNode.fX = VisNode.operandA._stamp.plane.x;
				VisNode.fY = VisNode.operandA._stamp.plane.y;
				VisNode.fZ = VisNode.operandA._stamp.plane.z;
				VisNode.fW = VisNode.operandA._stamp.plane.w;
				VisNode.DOT = VisNode.operandA.dotArc;
				VisNode.SIGHT = VisNode.operandA.distance;
				VisNode.SIGHT2 = VisNode.SIGHT * VisNode.SIGHT;
				VisNode.PLANEDOTSIGHT = VisNode.fW + VisNode.SIGHT;
				if (VisNode.operandA.sight.any)
				{
					VisNode.FALLBACK_TOO_CLOSE = true;
					ODBSibling<VisNode> odbsibling3 = VisNode.operandA.sight.last.first;
					if (VisNode.operandA.histSight.changed)
					{
						do
						{
							VisNode.operandB = odbsibling3.item.self;
							odbsibling3 = odbsibling3.item.n;
							if (!VisNode.operandB.active)
							{
								VisNode.ResolveHide();
							}
							else
							{
								VisNode.operandB.__skipOnce_ = true;
								VisNode.temp_bTraits = VisNode.operandB._traitMask;
								if ((VisNode.temp_bTraits & VisNode.operandA._sightCurrentMask) == 0 || !VisNode.LogicSight())
								{
									VisNode.ResolveHide();
								}
								else
								{
									VisNode.operandA._seeMask |= VisNode.temp_bTraits;
								}
							}
						}
						while (odbsibling3.has);
					}
					else
					{
						VisNode.operandB = odbsibling3.item.self;
						do
						{
							VisNode.operandB = odbsibling3.item.self;
							odbsibling3 = odbsibling3.item.n;
							if (!VisNode.operandB.active)
							{
								VisNode.ResolveHide();
							}
							else
							{
								VisNode.operandB.__skipOnce_ = true;
								VisNode.temp_bTraits = VisNode.operandB._traitMask;
								if (VisNode.operandB.histTrait.changed)
								{
									if ((VisNode.temp_bTraits & VisNode.operandA._sightCurrentMask) == 0 || !VisNode.LogicSight())
									{
										VisNode.ResolveHide();
										goto IL_342;
									}
									VisNode.operandA.anySeenTraitChanges = true;
								}
								else if (!VisNode.LogicSight())
								{
									VisNode.ResolveHide();
									goto IL_342;
								}
								VisNode.operandA._seeMask |= VisNode.temp_bTraits;
							}
							IL_342:;
						}
						while (odbsibling3.has);
					}
					VisNode.FALLBACK_TOO_CLOSE = false;
				}
				VisNode.operandA.__skipOnce_ = true;
				ODBSibling<VisNode> odbsibling4 = first_sib;
				do
				{
					VisNode.operandB = odbsibling4.item.self;
					odbsibling4 = odbsibling4.item.n;
					if (VisNode.operandB.__skipOnce_)
					{
						VisNode.operandB.__skipOnce_ = false;
					}
					else
					{
						VisNode.temp_bTraits = VisNode.operandB._traitMask;
						if ((VisNode.temp_bTraits & VisNode.operandA._sightCurrentMask) != 0 && VisNode.LogicSight())
						{
							VisNode.ResolveSee();
							VisNode.operandA._seeMask |= VisNode.temp_bTraits;
						}
					}
				}
				while (odbsibling4.has);
				VisNode.operandB = null;
			}
		}
		while (odbsibling.has);
		VisNode.operandA = null;
	}

	// Token: 0x06002378 RID: 9080 RVA: 0x000889D8 File Offset: 0x00086BD8
	private static void ClearVis(ODBSibling<VisNode> iter)
	{
		do
		{
			VisNode.operandA = iter.item.self;
			iter = iter.item.n;
			if (VisNode.operandA.sight.any)
			{
				ODBSibling<VisNode> odbsibling = VisNode.operandA.sight.last.first;
				do
				{
					VisNode.operandB = odbsibling.item.self;
					odbsibling = odbsibling.item.n;
					VisNode.ResolveHide();
				}
				while (odbsibling.has);
				VisNode.operandB = null;
			}
		}
		while (iter.has);
		VisNode.operandA = null;
	}

	// Token: 0x06002379 RID: 9081 RVA: 0x00088A74 File Offset: 0x00086C74
	private static void RunStamp(ODBSibling<VisNode> sib)
	{
		do
		{
			VisNode.operandA = sib.item.self;
			sib = sib.item.n;
			VisNode.operandA.Stamp();
		}
		while (sib.has);
		VisNode.operandA = null;
	}

	// Token: 0x0600237A RID: 9082 RVA: 0x00088AB4 File Offset: 0x00086CB4
	private static void RunStat(ODBSibling<VisNode> sib)
	{
		do
		{
			VisNode.operandA = sib.item.self;
			sib = sib.item.n;
			VisNode.operandA.StatUpdate();
		}
		while (sib.has);
		VisNode.operandA = null;
	}

	// Token: 0x0600237B RID: 9083 RVA: 0x00088AF4 File Offset: 0x00086CF4
	private static void RunHiddenCalls(ODBSibling<VisNode> sib)
	{
		do
		{
			VisNode.operandA = sib.item.self;
			sib = sib.item.n;
			if (VisNode.operandA.sight.rem)
			{
				ODBSibling<VisNode> odbsibling = VisNode.operandA.exit.first;
				do
				{
					VisNode.operandB = odbsibling.item.self;
					odbsibling = odbsibling.item.n;
					VisNode.operandB._CB_OnHiddenFrom_(VisNode.operandA);
				}
				while (odbsibling.has);
				VisNode.operandB = null;
			}
		}
		while (sib.has);
		VisNode.operandA = null;
	}

	// Token: 0x0600237C RID: 9084 RVA: 0x00088B98 File Offset: 0x00086D98
	private static void RunVoidSeenHiddenCalls(ODBSibling<VisNode> sib)
	{
		do
		{
			VisNode.operandA = sib.item.self;
			sib = sib.item.p;
			if (VisNode.operandA.spect.had)
			{
				if (!VisNode.operandA.spect.any)
				{
					VisNode.operandA._CB_OnHidden_();
					VisNode.operandA.spect.had = false;
				}
			}
			else if (VisNode.operandA.spect.any)
			{
				VisNode.operandA._CB_OnSeen_();
				VisNode.operandA.spect.had = true;
			}
			VisNode.operandA.sight.had = VisNode.operandA.sight.any;
		}
		while (sib.has);
		VisNode.operandA = null;
	}

	// Token: 0x0600237D RID: 9085 RVA: 0x00088C6C File Offset: 0x00086E6C
	private static void RunSeenCalls(ODBSibling<VisNode> sib)
	{
		do
		{
			VisNode.operandA = sib.item.self;
			sib = sib.item.n;
			if (VisNode.operandA.sight.add)
			{
				ODBSibling<VisNode> odbsibling = VisNode.operandA.enter.last;
				do
				{
					VisNode.operandB = odbsibling.item.self;
					odbsibling = odbsibling.item.p;
					VisNode.operandB._CB_OnSeenBy_(VisNode.operandA);
				}
				while (odbsibling.has);
				VisNode.operandB = null;
			}
		}
		while (sib.has);
		VisNode.operandA = null;
	}

	// Token: 0x0600237E RID: 9086 RVA: 0x00088D10 File Offset: 0x00086F10
	private static void RunQueries(ODBSibling<VisNode> sib)
	{
		do
		{
			VisNode.operandA = sib.item.self;
			sib = sib.item.p;
			if (VisNode.operandA.reactor)
			{
				VisNode.operandA.CheckReactions();
			}
			VisNode.operandA.CheckQueries();
		}
		while (sib.has);
		VisNode.operandA = null;
	}

	// Token: 0x0600237F RID: 9087 RVA: 0x00088D78 File Offset: 0x00086F78
	public static void Process()
	{
		if (VisNode.db.any)
		{
			if (VisNode.recentlyDisabled.any)
			{
				VisNode.RunStamp(VisNode.db.first);
				VisNode.RunStamp(VisNode.recentlyDisabled.first);
				VisNode.ClearVis(VisNode.recentlyDisabled.first);
				VisNode.UpdateVis(VisNode.db.first);
				VisNode.RunStat(VisNode.recentlyDisabled.first);
				VisNode.RunStat(VisNode.db.first);
				VisNode.RunHiddenCalls(VisNode.recentlyDisabled.first);
				VisNode.RunHiddenCalls(VisNode.db.first);
				VisNode.RunVoidSeenHiddenCalls(VisNode.recentlyDisabled.last);
				VisNode.RunVoidSeenHiddenCalls(VisNode.db.last);
				VisNode.RunSeenCalls(VisNode.recentlyDisabled.first);
				VisNode.RunSeenCalls(VisNode.db.first);
				VisNode.RunQueries(VisNode.recentlyDisabled.last);
				VisNode.RunQueries(VisNode.db.last);
				VisNode.Finally();
				VisNode.SwapDisabled();
			}
			else
			{
				VisNode.RunStamp(VisNode.db.first);
				VisNode.UpdateVis(VisNode.db.first);
				VisNode.RunStat(VisNode.db.first);
				VisNode.RunHiddenCalls(VisNode.db.first);
				VisNode.RunVoidSeenHiddenCalls(VisNode.db.last);
				VisNode.RunSeenCalls(VisNode.db.first);
				VisNode.RunQueries(VisNode.db.last);
				VisNode.Finally();
			}
		}
		else if (VisNode.recentlyDisabled.any)
		{
			VisNode.RunStamp(VisNode.recentlyDisabled.first);
			VisNode.ClearVis(VisNode.recentlyDisabled.first);
			VisNode.RunStat(VisNode.recentlyDisabled.first);
			VisNode.RunHiddenCalls(VisNode.recentlyDisabled.first);
			VisNode.RunVoidSeenHiddenCalls(VisNode.recentlyDisabled.last);
			VisNode.RunSeenCalls(VisNode.recentlyDisabled.first);
			VisNode.RunQueries(VisNode.recentlyDisabled.last);
			VisNode.Finally();
			VisNode.SwapDisabled();
		}
	}

	// Token: 0x06002380 RID: 9088 RVA: 0x00088F7C File Offset: 0x0008717C
	private void StatUpdate()
	{
		this.sight.count = this.sight.list.count;
		this.sight.any = (this.sight.count > 0);
		this.spect.count = this.spect.list.count;
		this.spect.any = (this.spect.count > 0);
	}

	// Token: 0x06002381 RID: 9089 RVA: 0x00088FF4 File Offset: 0x000871F4
	private void SeenHideFire()
	{
		if (this.spect.had != this.spect.any)
		{
			if (this.spect.any)
			{
				this._CB_OnSeen_();
			}
			else
			{
				this._CB_OnHidden_();
			}
			this.spect.had = this.spect.any;
		}
		this.sight.had = this.sight.any;
	}

	// Token: 0x06002382 RID: 9090 RVA: 0x0008906C File Offset: 0x0008726C
	private void DoQueryRecurse(int i, VisNode other)
	{
		if (i >= this._handle.Length)
		{
			return;
		}
		VisQuery.Instance instance = this._handle[i];
		switch (instance.TryAdd(this, other))
		{
		case VisQuery.TryResult.Enter:
			this.DoQueryRecurse(i + 1, other);
			instance.ExecuteEnter(this, other);
			return;
		case VisQuery.TryResult.Exit:
			instance.ExecuteExit(this, other);
			this.DoQueryRecurse(i + 1, other);
			return;
		}
		this.DoQueryRecurse(i + 1, other);
	}

	// Token: 0x06002383 RID: 9091 RVA: 0x000890F8 File Offset: 0x000872F8
	private void DoQueryRemAdd(ODBSibling<VisNode> sib)
	{
		if (this._handle.valid && this._handle.Length > 0)
		{
			while (sib.has)
			{
				VisNode self = sib.item.self;
				sib = sib.item.n;
				this.DoQueryRecurse(0, self);
			}
		}
	}

	// Token: 0x06002384 RID: 9092 RVA: 0x0008915C File Offset: 0x0008735C
	private void DoQueryRem(ODBSibling<VisNode> sib)
	{
		int length;
		if (this._handle.valid && (length = this._handle.Length) > 0)
		{
			while (sib.has)
			{
				VisNode self = sib.item.self;
				sib = sib.item.n;
				for (int i = 0; i < length; i++)
				{
					VisQuery.Instance instance = this._handle[i];
					if (instance.TryRemove(this, self) == VisQuery.TryResult.Exit)
					{
						instance.ExecuteExit(this, self);
					}
				}
			}
		}
	}

	// Token: 0x06002385 RID: 9093 RVA: 0x000891F0 File Offset: 0x000873F0
	private void _REACTOR_SEE_REMOVE(ODBSibling<VisNode> sib)
	{
		while (sib.has)
		{
			VisNode self = sib.item.self;
			sib = sib.item.n;
			this.reactor.SEE_REMOVE(self);
		}
	}

	// Token: 0x06002386 RID: 9094 RVA: 0x00089238 File Offset: 0x00087438
	private void _REACTOR_SEE_ADD(ODBSibling<VisNode> sib)
	{
		while (sib.has)
		{
			VisNode self = sib.item.self;
			sib = sib.item.n;
			this.reactor.SEE_ADD(self);
		}
	}

	// Token: 0x06002387 RID: 9095 RVA: 0x00089280 File Offset: 0x00087480
	private void CheckReactions()
	{
		if (this.sight.rem)
		{
			this._REACTOR_SEE_REMOVE(this.exit.first);
			if (!this.sight.add && !this.sight.any)
			{
				this.reactor.AWARE_EXIT();
			}
		}
		if (this.sight.add)
		{
			if (!this.sight.had)
			{
				this.reactor.AWARE_ENTER();
			}
			this._REACTOR_SEE_ADD(this.enter.first);
		}
	}

	// Token: 0x06002388 RID: 9096 RVA: 0x00089318 File Offset: 0x00087518
	private void CheckQueries()
	{
		this.histSeen.Upd(this._seeMask);
		if (this._handle.valid)
		{
			if (this.sight.rem)
			{
				this.DoQueryRem(this.exit.first);
			}
			if (this.anySeenTraitChanges || this.histTrait.changed)
			{
				this.DoQueryRemAdd(this.sight.list.first);
			}
			else if (this.sight.add)
			{
				this.DoQueryRemAdd(this.enter.first);
			}
		}
	}

	// Token: 0x06002389 RID: 9097 RVA: 0x000893C0 File Offset: 0x000875C0
	private static void Finally()
	{
		if (VisNode.disabledLastStep.any)
		{
			VisNode.RunStamp(VisNode.disabledLastStep.first);
			VisNode.disabledLastStep.Clear();
		}
	}

	// Token: 0x0600238A RID: 9098 RVA: 0x000893F8 File Offset: 0x000875F8
	private static void SwapDisabled()
	{
		ODBSet<VisNode> odbset = VisNode.disabledLastStep;
		VisNode.disabledLastStep = VisNode.recentlyDisabled;
		VisNode.recentlyDisabled = odbset;
	}

	// Token: 0x0600238B RID: 9099 RVA: 0x0008941C File Offset: 0x0008761C
	protected void _CB_OnSeen_()
	{
		if (this.reactor)
		{
			this.reactor.SPECTATED_ENTER();
		}
	}

	// Token: 0x0600238C RID: 9100 RVA: 0x0008943C File Offset: 0x0008763C
	protected void _CB_OnHidden_()
	{
		if (this.reactor)
		{
			this.reactor.SPECTATED_EXIT();
		}
	}

	// Token: 0x0600238D RID: 9101 RVA: 0x0008945C File Offset: 0x0008765C
	protected void _CB_OnSeenBy_(VisNode spectator)
	{
		if (this.reactor)
		{
			this.reactor.SPECTATOR_ADD(spectator);
		}
	}

	// Token: 0x0600238E RID: 9102 RVA: 0x0008947C File Offset: 0x0008767C
	protected void _CB_OnHiddenFrom_(VisNode spectator)
	{
		if (this.reactor)
		{
			this.reactor.SPECTATOR_REMOVE(spectator);
		}
	}

	// Token: 0x0600238F RID: 9103 RVA: 0x0008949C File Offset: 0x0008769C
	public bool CanSee(VisNode other)
	{
		return VisNode.CanSee(this, other);
	}

	// Token: 0x06002390 RID: 9104 RVA: 0x000894A8 File Offset: 0x000876A8
	public bool IsSeenBy(VisNode other)
	{
		return VisNode.IsSeenBy(this, other);
	}

	// Token: 0x06002391 RID: 9105 RVA: 0x000894B4 File Offset: 0x000876B4
	public bool CanSeeUnobstructed(VisNode other)
	{
		return this.CanSee(other) && this.Unobstructed(other);
	}

	// Token: 0x06002392 RID: 9106 RVA: 0x000894CC File Offset: 0x000876CC
	public bool Unobstructed(VisNode other)
	{
		return Physics.Linecast(this._stamp.position, other._stamp.position, 1);
	}

	// Token: 0x06002393 RID: 9107 RVA: 0x000894EC File Offset: 0x000876EC
	public static bool CanSee(VisNode instigator, VisNode target)
	{
		return instigator == target || instigator._CanSee(target);
	}

	// Token: 0x06002394 RID: 9108 RVA: 0x00089504 File Offset: 0x00087704
	public static bool IsSeenBy(VisNode instigator, VisNode target)
	{
		return instigator == target || instigator._IsSeenBy(target);
	}

	// Token: 0x06002395 RID: 9109 RVA: 0x0008951C File Offset: 0x0008771C
	public static bool AreAware(VisNode instigator, VisNode target)
	{
		return VisNode.CanSee(instigator, target) && instigator._IsSeenBy(target);
	}

	// Token: 0x06002396 RID: 9110 RVA: 0x00089534 File Offset: 0x00087734
	public static bool IsStealthly(VisNode instigator, VisNode target)
	{
		return VisNode.CanSee(instigator, target) && !instigator._IsSeenBy(target);
	}

	// Token: 0x06002397 RID: 9111 RVA: 0x00089550 File Offset: 0x00087750
	public static bool AreOblivious(VisNode instigator, VisNode target)
	{
		return !VisNode.CanSee(instigator, target) && !instigator._IsSeenBy(target);
	}

	// Token: 0x06002398 RID: 9112 RVA: 0x0008956C File Offset: 0x0008776C
	public static Vis.Comparison Compare(VisNode self, VisNode target)
	{
		if (self == target)
		{
			return Vis.Comparison.IsSelf;
		}
		if (self._CanSee(target))
		{
			if (self._IsSeenBy(target))
			{
				return Vis.Comparison.Contact;
			}
			return Vis.Comparison.Stealthy;
		}
		else
		{
			if (self._IsSeenBy(target))
			{
				return Vis.Comparison.Prey;
			}
			return Vis.Comparison.Oblivious;
		}
	}

	// Token: 0x06002399 RID: 9113 RVA: 0x000895B8 File Offset: 0x000877B8
	private bool _CanSee(VisNode other)
	{
		return (other.spect.count >= this.sight.count) ? this.sight.list.Contains(other) : other.spect.list.Contains(this);
	}

	// Token: 0x0600239A RID: 9114 RVA: 0x00089608 File Offset: 0x00087808
	private bool _IsSeenBy(VisNode other)
	{
		return (other.sight.count >= this.spect.count) ? this.spect.list.Contains(other) : other.sight.list.Contains(this);
	}

	// Token: 0x0600239B RID: 9115 RVA: 0x00089658 File Offset: 0x00087858
	[Conditional("UNITY_EDITOR")]
	private static void _VALIDATE(VisNode vis)
	{
		if (vis.sight.count > 0 != vis.sight.any)
		{
			Debug.LogError(string.Format("buzz {0} {1}", vis.sight.count, vis.sight.any), vis);
		}
		if (vis.sight.list.count != vis.sight.count)
		{
			Debug.LogError(string.Format("buzz {0} {1}", vis.sight.list.count, vis.sight.count), vis);
		}
		if (vis.spect.count > 0 != vis.spect.any)
		{
			Debug.LogError(string.Format("buzz {0} {1}", vis.spect.count, vis.spect.any), vis);
		}
		if (vis.spect.list.count != vis.spect.count)
		{
			Debug.LogError(string.Format("buzz {0} {1}", vis.spect.list.count, vis.spect.count), vis);
		}
	}

	// Token: 0x0600239C RID: 9116 RVA: 0x000897AC File Offset: 0x000879AC
	private static void RouteMessageHSet(ODBSet<VisNode> list, string msg, object arg)
	{
		if (list.any)
		{
			ODBSibling<VisNode> odbsibling = list.first;
			do
			{
				VisNode self = odbsibling.item.self;
				odbsibling = odbsibling.item.n;
				try
				{
					self.SendMessage(msg, arg, 1);
				}
				catch (Exception ex)
				{
					Debug.LogError(ex, self);
				}
			}
			while (odbsibling.has);
		}
	}

	// Token: 0x0600239D RID: 9117 RVA: 0x00089828 File Offset: 0x00087A28
	private static void RouteMessageList(RecycleList<VisNode> list, string msg)
	{
		VisNode.RouteMessageList(list, msg, null);
	}

	// Token: 0x0600239E RID: 9118 RVA: 0x00089834 File Offset: 0x00087A34
	private static void RouteMessageList(RecycleList<VisNode> list, string msg, object arg)
	{
		using (RecycleListIter<VisNode> recycleListIter = list.MakeIter())
		{
			while (recycleListIter.MoveNext())
			{
				try
				{
					recycleListIter.Current.SendMessage(msg, arg, 1);
				}
				catch (Exception ex)
				{
					Debug.LogError(ex, recycleListIter.Current);
				}
			}
		}
	}

	// Token: 0x0600239F RID: 9119 RVA: 0x000898C4 File Offset: 0x00087AC4
	private static void RouteMessageOp(HSetOper op, ODBSet<VisNode> a, IEnumerable<VisNode> b, string msg, object arg)
	{
		RecycleList<VisNode> recycleList = a.OperList(op, b);
		VisNode.RouteMessageList(recycleList, msg, arg);
		recycleList.Dispose();
	}

	// Token: 0x060023A0 RID: 9120 RVA: 0x000898EC File Offset: 0x00087AEC
	private static void RouteMessageOpUnionFirst(HSetOper op, ODBSet<VisNode> a, ODBSet<VisNode> aa, IEnumerable<VisNode> b, string msg, object arg)
	{
		ODBSet<VisNode> odbset = new ODBSet<VisNode>(a);
		odbset.UnionWith(aa);
		VisNode.RouteMessageOp(op, odbset, b, msg, arg);
	}

	// Token: 0x060023A1 RID: 9121 RVA: 0x00089914 File Offset: 0x00087B14
	private static void RouteMessageOpUnionFirst(HSetOper op, ODBSet<VisNode> a, ODBSet<VisNode> aa, IEnumerable<VisNode> b, string msg)
	{
		VisNode.RouteMessageOpUnionFirst(op, a, aa, b, msg, null);
	}

	// Token: 0x060023A2 RID: 9122 RVA: 0x00089924 File Offset: 0x00087B24
	private static void RouteMessageOp(HSetOper op, ODBSet<VisNode> a, IEnumerable<VisNode> b, string msg)
	{
		VisNode.RouteMessageOp(op, a, b, msg, null);
	}

	// Token: 0x060023A3 RID: 9123 RVA: 0x00089930 File Offset: 0x00087B30
	private static void DoGestureMessage(VisNode instigator, string message, object arg)
	{
		VisNode.RouteMessageHSet(instigator.spect.list, message, arg);
	}

	// Token: 0x060023A4 RID: 9124 RVA: 0x00089944 File Offset: 0x00087B44
	public static bool GestureMessage(VisNode instigator, string message, object arg)
	{
		if (!instigator || !instigator.enabled)
		{
			return false;
		}
		VisNode.DoGestureMessage(instigator, message, arg);
		return true;
	}

	// Token: 0x060023A5 RID: 9125 RVA: 0x00089968 File Offset: 0x00087B68
	private static void DoAttentionMessage(VisNode instigator, string message, object arg)
	{
		VisNode.RouteMessageHSet(instigator.sight.list, message, arg);
	}

	// Token: 0x060023A6 RID: 9126 RVA: 0x0008997C File Offset: 0x00087B7C
	public static bool AttentionMessage(VisNode instigator, string message, object arg)
	{
		return false;
	}

	// Token: 0x060023A7 RID: 9127 RVA: 0x00089980 File Offset: 0x00087B80
	private static void DoStealthMessage(VisNode instigator, string message, object arg)
	{
		VisNode.RouteMessageOp(HSetOper.Except, instigator.sight.list, instigator.spect.list, message, arg);
	}

	// Token: 0x060023A8 RID: 9128 RVA: 0x000899A0 File Offset: 0x00087BA0
	public static bool StealthMessage(VisNode instigator, string message, object arg)
	{
		return false;
	}

	// Token: 0x060023A9 RID: 9129 RVA: 0x000899A4 File Offset: 0x00087BA4
	private static void DoPreyMessage(VisNode instigator, string message, object arg)
	{
		VisNode.RouteMessageOp(HSetOper.Except, instigator.spect.list, instigator.sight.list, message, arg);
	}

	// Token: 0x060023AA RID: 9130 RVA: 0x000899C4 File Offset: 0x00087BC4
	public static bool PreyMessage(VisNode instigator, string message, object arg)
	{
		return false;
	}

	// Token: 0x060023AB RID: 9131 RVA: 0x000899C8 File Offset: 0x00087BC8
	private static void DoContactMessage(VisNode instigator, string message, object arg)
	{
		if (instigator.spect.count < instigator.sight.count)
		{
			VisNode.RouteMessageOp(HSetOper.Intersect, instigator.spect.list, instigator.sight.list, message, arg);
		}
		else
		{
			VisNode.RouteMessageOp(HSetOper.Intersect, instigator.sight.list, instigator.spect.list, message, arg);
		}
	}

	// Token: 0x060023AC RID: 9132 RVA: 0x00089A34 File Offset: 0x00087C34
	public static bool ContactMessage(VisNode instigator, string message, object arg)
	{
		return false;
	}

	// Token: 0x060023AD RID: 9133 RVA: 0x00089A38 File Offset: 0x00087C38
	private static void DoObliviousMessage(VisNode instigator, string message, object arg)
	{
		if (instigator.spect.count < instigator.sight.count)
		{
			VisNode.RouteMessageOpUnionFirst(HSetOper.SymmetricExcept, instigator.spect.list, instigator.sight.list, VisNode.db, message, arg);
		}
		else
		{
			VisNode.RouteMessageOpUnionFirst(HSetOper.SymmetricExcept, instigator.sight.list, instigator.spect.list, VisNode.db, message, arg);
		}
	}

	// Token: 0x060023AE RID: 9134 RVA: 0x00089AAC File Offset: 0x00087CAC
	public static bool ObliviousMessage(VisNode instigator, string message, object arg)
	{
		if (!instigator || !instigator.enabled)
		{
			return false;
		}
		VisNode.DoObliviousMessage(instigator, message, arg);
		return true;
	}

	// Token: 0x060023AF RID: 9135 RVA: 0x00089AD0 File Offset: 0x00087CD0
	public static void GlobalMessage(string message, object arg)
	{
		using (ODBForwardEnumerator<VisNode> enumerator = VisNode.db.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				enumerator.Current.SendMessage(message, arg, 1);
			}
		}
	}

	// Token: 0x060023B0 RID: 9136 RVA: 0x00089B34 File Offset: 0x00087D34
	public static bool ComparisonMessage(VisNode instigator, Vis.Comparison comparison, string message, object arg)
	{
		switch (comparison)
		{
		case Vis.Comparison.Prey:
			return VisNode.PreyMessage(instigator, message, arg);
		default:
			if (comparison == Vis.Comparison.Oblivious)
			{
				return VisNode.ObliviousMessage(instigator, message, arg);
			}
			if (comparison != Vis.Comparison.Stealthy)
			{
				throw new ArgumentException(" do not know what to do with " + comparison, "comparison");
			}
			return VisNode.StealthMessage(instigator, message, arg);
		case Vis.Comparison.IsSelf:
			if (!instigator || !instigator.enabled)
			{
				return false;
			}
			instigator.SendMessage(message, arg, 1);
			return true;
		case Vis.Comparison.Contact:
			return VisNode.ContactMessage(instigator, message, arg);
		}
	}

	// Token: 0x060023B1 RID: 9137 RVA: 0x00089BD4 File Offset: 0x00087DD4
	private static void DoAudibleMessage(VisNode instigator, Vector3 position, float radius, string message, object arg)
	{
		VisNode.Search.Radial.Enumerator nodesInRadius = Vis.GetNodesInRadius(position, radius);
		if (!instigator.deaf)
		{
			while (nodesInRadius.MoveNext())
			{
				if (object.ReferenceEquals(nodesInRadius.Current, instigator))
				{
					break;
				}
				nodesInRadius.Current.SendMessage(message, arg, 1);
			}
		}
		while (nodesInRadius.MoveNext())
		{
			nodesInRadius.Current.SendMessage(message, arg, 1);
		}
		nodesInRadius.Dispose();
	}

	// Token: 0x060023B2 RID: 9138 RVA: 0x00089C54 File Offset: 0x00087E54
	public static bool AudibleMessage(VisNode instigator, Vector3 position, float radius, string message, object arg)
	{
		if (!instigator || instigator.mute || radius <= 0f || !instigator.enabled)
		{
			return false;
		}
		VisNode.DoAudibleMessage(instigator, position, radius, message, arg);
		return true;
	}

	// Token: 0x060023B3 RID: 9139 RVA: 0x00089C9C File Offset: 0x00087E9C
	public static bool AudibleMessage(VisNode instigator, float radius, string message, object arg)
	{
		if (!instigator || instigator.mute || radius <= 0f || !instigator.enabled)
		{
			return false;
		}
		VisNode.DoAudibleMessage(instigator, instigator._stamp.position, radius, message, arg);
		return true;
	}

	// Token: 0x060023B4 RID: 9140 RVA: 0x00089CEC File Offset: 0x00087EEC
	public static bool AudibleMessage(VisNode instigator, Vector3 position, float radius, string message)
	{
		if (!instigator || instigator.mute || radius <= 0f || !instigator.enabled)
		{
			return false;
		}
		VisNode.DoAudibleMessage(instigator, position, radius, message, null);
		return true;
	}

	// Token: 0x060023B5 RID: 9141 RVA: 0x00089D34 File Offset: 0x00087F34
	public static bool AudibleMessage(VisNode instigator, float radius, string message)
	{
		if (!instigator || instigator.mute || radius <= 0f || !instigator.enabled)
		{
			return false;
		}
		VisNode.DoAudibleMessage(instigator, instigator._stamp.position, radius, message, null);
		return true;
	}

	// Token: 0x060023B6 RID: 9142 RVA: 0x00089D84 File Offset: 0x00087F84
	public static bool GestureMessage(VisNode instigator, string message)
	{
		return VisNode.GestureMessage(instigator, message, null);
	}

	// Token: 0x060023B7 RID: 9143 RVA: 0x00089D90 File Offset: 0x00087F90
	public static bool AttentionMessage(VisNode instigator, string message)
	{
		return VisNode.AttentionMessage(instigator, message, null);
	}

	// Token: 0x060023B8 RID: 9144 RVA: 0x00089D9C File Offset: 0x00087F9C
	public static bool StealthMessage(VisNode instigator, string message)
	{
		return VisNode.StealthMessage(instigator, message, null);
	}

	// Token: 0x060023B9 RID: 9145 RVA: 0x00089DA8 File Offset: 0x00087FA8
	public static bool PreyMessage(VisNode instigator, string message)
	{
		return VisNode.GestureMessage(instigator, message, null);
	}

	// Token: 0x060023BA RID: 9146 RVA: 0x00089DB4 File Offset: 0x00087FB4
	public static bool ContactMessage(VisNode instigator, string message)
	{
		return VisNode.AttentionMessage(instigator, message, null);
	}

	// Token: 0x060023BB RID: 9147 RVA: 0x00089DC0 File Offset: 0x00087FC0
	public static bool ObliviousMessage(VisNode instigator, string message)
	{
		return VisNode.StealthMessage(instigator, message, null);
	}

	// Token: 0x060023BC RID: 9148 RVA: 0x00089DCC File Offset: 0x00087FCC
	public static bool ComparisonMessage(VisNode instigator, Vis.Comparison comparison, string message)
	{
		return VisNode.ComparisonMessage(instigator, comparison, message, null);
	}

	// Token: 0x060023BD RID: 9149 RVA: 0x00089DD8 File Offset: 0x00087FD8
	public bool GestureMessage(string message, object arg)
	{
		if (!base.enabled)
		{
			return false;
		}
		VisNode.DoGestureMessage(this, message, arg);
		return true;
	}

	// Token: 0x060023BE RID: 9150 RVA: 0x00089DF0 File Offset: 0x00087FF0
	public bool GestureMessage(string message)
	{
		if (!base.enabled)
		{
			return false;
		}
		VisNode.DoGestureMessage(this, message, null);
		return true;
	}

	// Token: 0x060023BF RID: 9151 RVA: 0x00089E08 File Offset: 0x00088008
	public bool AttentionMessage(string message, object arg)
	{
		return false;
	}

	// Token: 0x060023C0 RID: 9152 RVA: 0x00089E0C File Offset: 0x0008800C
	public bool AttentionMessage(string message)
	{
		return false;
	}

	// Token: 0x060023C1 RID: 9153 RVA: 0x00089E10 File Offset: 0x00088010
	public bool StealthMessage(string message, object arg)
	{
		return false;
	}

	// Token: 0x060023C2 RID: 9154 RVA: 0x00089E14 File Offset: 0x00088014
	public bool PreyMessage(string message)
	{
		return false;
	}

	// Token: 0x060023C3 RID: 9155 RVA: 0x00089E18 File Offset: 0x00088018
	public bool ContactMessage(string message, object arg)
	{
		return false;
	}

	// Token: 0x060023C4 RID: 9156 RVA: 0x00089E1C File Offset: 0x0008801C
	public bool ContactMessage(string message)
	{
		return false;
	}

	// Token: 0x060023C5 RID: 9157 RVA: 0x00089E20 File Offset: 0x00088020
	public bool ObliviousMessage(string message, object arg)
	{
		if (!base.enabled)
		{
			return false;
		}
		VisNode.ContactMessage(this, message, arg);
		return true;
	}

	// Token: 0x060023C6 RID: 9158 RVA: 0x00089E3C File Offset: 0x0008803C
	public bool ObliviousMessage(string message)
	{
		if (!base.enabled)
		{
			return false;
		}
		VisNode.ContactMessage(this, message, null);
		return true;
	}

	// Token: 0x060023C7 RID: 9159 RVA: 0x00089E58 File Offset: 0x00088058
	public bool ComparisonMessage(Vis.Comparison comparison, string message, object arg)
	{
		return VisNode.ComparisonMessage(this, comparison, message, arg);
	}

	// Token: 0x060023C8 RID: 9160 RVA: 0x00089E64 File Offset: 0x00088064
	public bool ComparisonMessage(Vis.Comparison comparison, string message)
	{
		return VisNode.ComparisonMessage(this, comparison, message, null);
	}

	// Token: 0x060023C9 RID: 9161 RVA: 0x00089E70 File Offset: 0x00088070
	public bool AudibleMessage(float radius, string message, object arg)
	{
		if (this.mute || !base.enabled || radius <= 0f)
		{
			return false;
		}
		VisNode.DoAudibleMessage(this, this._stamp.position, radius, message, arg);
		return true;
	}

	// Token: 0x060023CA RID: 9162 RVA: 0x00089EB8 File Offset: 0x000880B8
	public bool AudibleMessage(float radius, string message)
	{
		if (this.mute || !base.enabled || radius <= 0f)
		{
			return false;
		}
		VisNode.DoAudibleMessage(this, this._stamp.position, radius, message, null);
		return true;
	}

	// Token: 0x060023CB RID: 9163 RVA: 0x00089F00 File Offset: 0x00088100
	public bool AudibleMessage(Vector3 point, float radius, string message, object arg)
	{
		if (this.mute || !base.enabled || radius <= 0f)
		{
			return false;
		}
		VisNode.DoAudibleMessage(this, point, radius, message, arg);
		return true;
	}

	// Token: 0x060023CC RID: 9164 RVA: 0x00089F3C File Offset: 0x0008813C
	public bool AudibleMessage(Vector3 point, float radius, string message)
	{
		if (this.mute || !base.enabled || radius <= 0f)
		{
			return false;
		}
		VisNode.DoAudibleMessage(this, point, radius, message, null);
		return true;
	}

	// Token: 0x060023CD RID: 9165 RVA: 0x00089F78 File Offset: 0x00088178
	private void DrawConnections(ODBSet<VisNode> list)
	{
		if (list != null)
		{
			ODBForwardEnumerator<VisNode> enumerator = list.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Vector3 position = enumerator.Current._stamp.position;
				Gizmos.DrawLine(this._stamp.position, position);
				Gizmos.DrawWireSphere(position, 0.5f);
			}
			enumerator.Dispose();
		}
	}

	// Token: 0x060023CE RID: 9166 RVA: 0x00089FD8 File Offset: 0x000881D8
	private void OnDrawGizmosSelected()
	{
		VisGizmosUtility.ResetMatrixStack();
		Gizmos.color = new Color(0f, 1f, 0f, 0.5f);
		this.DrawConnections(this.sight.list);
		Gizmos.color = new Color(0f, 0f, 1f, 0.5f);
		this.DrawConnections(this.spect.list);
		Transform transform = (!this._transform) ? base.transform : this._transform;
		Gizmos.color = new Color(1f, 1f, 1f, 0.9f);
		Vector3 normalized = transform.forward.normalized;
		Vector3 position = transform.position;
		Vector3 vector = position + normalized * this.distance;
		Gizmos.DrawLine(position, vector);
		VisGizmosUtility.DrawDotArc(position, transform, this.distance, this.dotArc, this.dotArcBegin);
	}

	// Token: 0x040010C3 RID: 4291
	private const int defaultUnobstructedLayers = 1;

	// Token: 0x040010C4 RID: 4292
	[SerializeField]
	[PrefetchComponent]
	private VisReactor reactor;

	// Token: 0x040010C5 RID: 4293
	[SerializeField]
	private float dotArc = 0.75f;

	// Token: 0x040010C6 RID: 4294
	[SerializeField]
	private float distance = 10f;

	// Token: 0x040010C7 RID: 4295
	[SerializeField]
	private float dotArcBegin;

	// Token: 0x040010C8 RID: 4296
	[HideInInspector]
	[SerializeField]
	private int _sightMask = -1;

	// Token: 0x040010C9 RID: 4297
	[HideInInspector]
	[SerializeField]
	private int _spectMask = -1;

	// Token: 0x040010CA RID: 4298
	[HideInInspector]
	[SerializeField]
	private int _traitMask = 16777217;

	// Token: 0x040010CB RID: 4299
	[NonSerialized]
	private int _sightCurrentMask;

	// Token: 0x040010CC RID: 4300
	[NonSerialized]
	private int _seeMask;

	// Token: 0x040010CD RID: 4301
	[NonSerialized]
	private bool anySeenTraitChanges;

	// Token: 0x040010CE RID: 4302
	[NonSerialized]
	private bool hasStatusHandler;

	// Token: 0x040010CF RID: 4303
	[NonSerialized]
	private bool __skipOnce_;

	// Token: 0x040010D0 RID: 4304
	[NonSerialized]
	private bool awake;

	// Token: 0x040010D1 RID: 4305
	[NonSerialized]
	private bool active;

	// Token: 0x040010D2 RID: 4306
	[NonSerialized]
	private bool dataConstructed;

	// Token: 0x040010D3 RID: 4307
	public bool blind;

	// Token: 0x040010D4 RID: 4308
	public bool deaf;

	// Token: 0x040010D5 RID: 4309
	public bool mute;

	// Token: 0x040010D6 RID: 4310
	[SerializeField]
	private VisClass _class;

	// Token: 0x040010D7 RID: 4311
	[NonSerialized]
	private VisClass.Handle _handle;

	// Token: 0x040010D8 RID: 4312
	private long queriesBitMask;

	// Token: 0x040010D9 RID: 4313
	private IVisHandler statusHandler;

	// Token: 0x040010DA RID: 4314
	[NonSerialized]
	private VisNode.TraitHistory histSight;

	// Token: 0x040010DB RID: 4315
	[NonSerialized]
	private VisNode.TraitHistory histSpect;

	// Token: 0x040010DC RID: 4316
	[NonSerialized]
	private VisNode.TraitHistory histTrait;

	// Token: 0x040010DD RID: 4317
	[NonSerialized]
	private VisNode.TraitHistory histSeen;

	// Token: 0x040010DE RID: 4318
	private VisNode.VisMem spect;

	// Token: 0x040010DF RID: 4319
	private VisNode.VisMem sight;

	// Token: 0x040010E0 RID: 4320
	private ODBSet<VisNode> enter;

	// Token: 0x040010E1 RID: 4321
	private ODBSet<VisNode> exit;

	// Token: 0x040010E2 RID: 4322
	internal ODBItem<VisNode> item;

	// Token: 0x040010E3 RID: 4323
	private List<VisNode> cleanList;

	// Token: 0x040010E4 RID: 4324
	[HideInInspector]
	[NonSerialized]
	private Transform _transform;

	// Token: 0x040010E5 RID: 4325
	[NonSerialized]
	private Vis.Stamp _stamp;

	// Token: 0x040010E6 RID: 4326
	private static ObjectDB<VisNode> db = new ObjectDB<VisNode>();

	// Token: 0x040010E7 RID: 4327
	private static VisManager manager;

	// Token: 0x040010E8 RID: 4328
	private static ODBSet<VisNode> recentlyDisabled = new ODBSet<VisNode>();

	// Token: 0x040010E9 RID: 4329
	private static ODBSet<VisNode> disabledLastStep = new ODBSet<VisNode>();

	// Token: 0x040010EA RID: 4330
	private static VisNode operandA;

	// Token: 0x040010EB RID: 4331
	private static VisNode operandB;

	// Token: 0x040010EC RID: 4332
	private static float pX;

	// Token: 0x040010ED RID: 4333
	private static float pY;

	// Token: 0x040010EE RID: 4334
	private static float pZ;

	// Token: 0x040010EF RID: 4335
	private static float bX;

	// Token: 0x040010F0 RID: 4336
	private static float bY;

	// Token: 0x040010F1 RID: 4337
	private static float bZ;

	// Token: 0x040010F2 RID: 4338
	private static float fX;

	// Token: 0x040010F3 RID: 4339
	private static float fY;

	// Token: 0x040010F4 RID: 4340
	private static float fZ;

	// Token: 0x040010F5 RID: 4341
	private static float fW;

	// Token: 0x040010F6 RID: 4342
	private static float dX;

	// Token: 0x040010F7 RID: 4343
	private static float dY;

	// Token: 0x040010F8 RID: 4344
	private static float dZ;

	// Token: 0x040010F9 RID: 4345
	private static float nX;

	// Token: 0x040010FA RID: 4346
	private static float nY;

	// Token: 0x040010FB RID: 4347
	private static float nZ;

	// Token: 0x040010FC RID: 4348
	private static float dV;

	// Token: 0x040010FD RID: 4349
	private static float dV2;

	// Token: 0x040010FE RID: 4350
	private static float dot;

	// Token: 0x040010FF RID: 4351
	private static float planeDot;

	// Token: 0x04001100 RID: 4352
	private static float SIGHT;

	// Token: 0x04001101 RID: 4353
	private static float PLANEDOTSIGHT;

	// Token: 0x04001102 RID: 4354
	private static float SIGHT2;

	// Token: 0x04001103 RID: 4355
	private static float DOT;

	// Token: 0x04001104 RID: 4356
	private static bool FALLBACK_TOO_CLOSE = false;

	// Token: 0x04001105 RID: 4357
	private static int temp_bTraits;

	// Token: 0x020003A3 RID: 931
	private struct TraitHistory
	{
		// Token: 0x060023CF RID: 9167 RVA: 0x0008A0D4 File Offset: 0x000882D4
		public int Upd(int newTraits)
		{
			int num = newTraits ^ this.last;
			this.changed = (num != 0);
			this.last = newTraits;
			return num;
		}

		// Token: 0x04001106 RID: 4358
		public int last;

		// Token: 0x04001107 RID: 4359
		public bool changed;
	}

	// Token: 0x020003A4 RID: 932
	private struct VisMem
	{
		// Token: 0x04001108 RID: 4360
		public ODBSet<VisNode> list;

		// Token: 0x04001109 RID: 4361
		public ODBSet<VisNode> last;

		// Token: 0x0400110A RID: 4362
		public int count;

		// Token: 0x0400110B RID: 4363
		public bool add;

		// Token: 0x0400110C RID: 4364
		public bool rem;

		// Token: 0x0400110D RID: 4365
		public bool any;

		// Token: 0x0400110E RID: 4366
		public bool had;
	}

	// Token: 0x020003A5 RID: 933
	public static class Search
	{
		// Token: 0x020003A6 RID: 934
		public interface ISearch : IEnumerable, IEnumerable<VisNode>
		{
		}

		// Token: 0x020003A7 RID: 935
		public interface ISearch<TEnumerator> : IEnumerable, VisNode.Search.ISearch, IEnumerable<VisNode> where TEnumerator : struct, IEnumerator<VisNode>
		{
			// Token: 0x060023D0 RID: 9168
			TEnumerator GetEnumerator();
		}

		// Token: 0x020003A8 RID: 936
		public struct PointRadiusData
		{
			// Token: 0x060023D1 RID: 9169 RVA: 0x0008A100 File Offset: 0x00088300
			public PointRadiusData(Vector3 pos, float radius)
			{
				this.x = pos.x;
				this.y = pos.y;
				this.z = pos.z;
				this.radiusSquare = radius * radius;
				this.dX = 0f;
				this.dY = 0f;
				this.dZ = 0f;
				this.d2 = 0f;
			}

			// Token: 0x060023D2 RID: 9170 RVA: 0x0008A16C File Offset: 0x0008836C
			public bool Pass(VisNode current)
			{
				this.dX = this.x - current._stamp.position.x;
				this.dY = this.y - current._stamp.position.y;
				this.dZ = this.z - current._stamp.position.z;
				this.d2 = this.dX * this.dX + this.dY * this.dY + this.dZ * this.dZ;
				return this.d2 <= this.radiusSquare;
			}

			// Token: 0x0400110F RID: 4367
			public float radiusSquare;

			// Token: 0x04001110 RID: 4368
			public float x;

			// Token: 0x04001111 RID: 4369
			public float y;

			// Token: 0x04001112 RID: 4370
			public float z;

			// Token: 0x04001113 RID: 4371
			public float dX;

			// Token: 0x04001114 RID: 4372
			public float dY;

			// Token: 0x04001115 RID: 4373
			public float dZ;

			// Token: 0x04001116 RID: 4374
			public float d2;
		}

		// Token: 0x020003A9 RID: 937
		public struct PointVisibilityData
		{
			// Token: 0x060023D3 RID: 9171 RVA: 0x0008A214 File Offset: 0x00088414
			public PointVisibilityData(Vector3 point)
			{
				this.x = point.x;
				this.y = point.y;
				this.z = point.z;
				this.dX = 0f;
				this.dY = 0f;
				this.dZ = 0f;
				this.d2 = 0f;
				this.d = 0f;
				this.nX = 0f;
				this.nY = 0f;
				this.nZ = 0f;
				this.radius = 0f;
				this.radiusSquare = 0f;
			}

			// Token: 0x060023D4 RID: 9172 RVA: 0x0008A2B8 File Offset: 0x000884B8
			public bool Pass(VisNode Current)
			{
				this.radius = Current.distance;
				this.radiusSquare *= this.radiusSquare;
				this.dX = this.x - Current._stamp.position.x;
				this.dY = this.y - Current._stamp.position.y;
				this.dZ = this.z - Current._stamp.position.z;
				this.d2 = this.dX * this.dX + this.dY * this.dY + this.dZ * this.dZ;
				if (this.d2 < 4.203895E-45f)
				{
					return true;
				}
				this.d = Mathf.Sqrt(this.d2);
				this.nX = this.dX / this.d;
				this.nY = this.dY / this.d;
				this.nZ = this.dZ / this.d;
				VisNode.dot = Current._stamp.plane.x * this.nX + Current._stamp.plane.y * this.nY + Current._stamp.plane.z * this.nZ;
				return VisNode.dot >= Current.dotArc;
			}

			// Token: 0x04001117 RID: 4375
			public float x;

			// Token: 0x04001118 RID: 4376
			public float y;

			// Token: 0x04001119 RID: 4377
			public float z;

			// Token: 0x0400111A RID: 4378
			public float dX;

			// Token: 0x0400111B RID: 4379
			public float dY;

			// Token: 0x0400111C RID: 4380
			public float dZ;

			// Token: 0x0400111D RID: 4381
			public float d2;

			// Token: 0x0400111E RID: 4382
			public float d;

			// Token: 0x0400111F RID: 4383
			public float nX;

			// Token: 0x04001120 RID: 4384
			public float nY;

			// Token: 0x04001121 RID: 4385
			public float nZ;

			// Token: 0x04001122 RID: 4386
			public float radius;

			// Token: 0x04001123 RID: 4387
			public float radiusSquare;
		}

		// Token: 0x020003AA RID: 938
		public struct MaskCompareData
		{
			// Token: 0x060023D5 RID: 9173 RVA: 0x0008A428 File Offset: 0x00088628
			public MaskCompareData(Vis.Op op, Vis.Mask mask)
			{
				this.op = op;
				this.mask = mask.data;
			}

			// Token: 0x060023D6 RID: 9174 RVA: 0x0008A440 File Offset: 0x00088640
			public bool Pass(int mask)
			{
				return Vis.Evaluate(this.op, this.mask, mask);
			}

			// Token: 0x04001124 RID: 4388
			public Vis.Op op;

			// Token: 0x04001125 RID: 4389
			public int mask;
		}

		// Token: 0x020003AB RID: 939
		public struct PointRadiusMaskData
		{
			// Token: 0x060023D7 RID: 9175 RVA: 0x0008A454 File Offset: 0x00088654
			public PointRadiusMaskData(Vector3 pos, float radius, Vis.Op op, Vis.Mask mask)
			{
				this = new VisNode.Search.PointRadiusMaskData(new VisNode.Search.PointRadiusData(pos, radius), new VisNode.Search.MaskCompareData(op, mask));
			}

			// Token: 0x060023D8 RID: 9176 RVA: 0x0008A46C File Offset: 0x0008866C
			public PointRadiusMaskData(VisNode.Search.PointRadiusData pr, VisNode.Search.MaskCompareData mc)
			{
				this.pr = pr;
				this.mc = mc;
			}

			// Token: 0x060023D9 RID: 9177 RVA: 0x0008A47C File Offset: 0x0008867C
			public bool Pass(VisNode current, int mask)
			{
				return this.mc.Pass(mask) && this.pr.Pass(current);
			}

			// Token: 0x04001126 RID: 4390
			public VisNode.Search.PointRadiusData pr;

			// Token: 0x04001127 RID: 4391
			public VisNode.Search.MaskCompareData mc;
		}

		// Token: 0x020003AC RID: 940
		public struct Radial : IEnumerable, VisNode.Search.ISearch, IEnumerable<VisNode>, VisNode.Search.ISearch<VisNode.Search.Radial.Enumerator>
		{
			// Token: 0x060023DA RID: 9178 RVA: 0x0008A4AC File Offset: 0x000886AC
			public Radial(Vector3 point, float radius)
			{
				this.point = point;
				this.radius = radius;
			}

			// Token: 0x060023DB RID: 9179 RVA: 0x0008A4BC File Offset: 0x000886BC
			IEnumerator<VisNode> IEnumerable<VisNode>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060023DC RID: 9180 RVA: 0x0008A4CC File Offset: 0x000886CC
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060023DD RID: 9181 RVA: 0x0008A4DC File Offset: 0x000886DC
			public VisNode.Search.Radial.Enumerator GetEnumerator()
			{
				return new VisNode.Search.Radial.Enumerator(new VisNode.Search.PointRadiusData(this.point, this.radius));
			}

			// Token: 0x04001128 RID: 4392
			public Vector3 point;

			// Token: 0x04001129 RID: 4393
			public float radius;

			// Token: 0x020003AD RID: 941
			public struct Enumerator : IDisposable, IEnumerator, IEnumerator<VisNode>
			{
				// Token: 0x060023DE RID: 9182 RVA: 0x0008A4F4 File Offset: 0x000886F4
				public Enumerator(VisNode.Search.PointRadiusData pr)
				{
					this.Current = null;
					this.d = false;
					this.e = VisNode.db.GetEnumerator();
					this.data = pr;
				}

				// Token: 0x170008B4 RID: 2228
				// (get) Token: 0x060023DF RID: 9183 RVA: 0x0008A51C File Offset: 0x0008871C
				VisNode IEnumerator<VisNode>.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x170008B5 RID: 2229
				// (get) Token: 0x060023E0 RID: 9184 RVA: 0x0008A524 File Offset: 0x00088724
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x060023E1 RID: 9185 RVA: 0x0008A52C File Offset: 0x0008872C
				public bool MoveNext()
				{
					while (this.e.MoveNext())
					{
						if (this.Pass(this.e.Current))
						{
							return true;
						}
					}
					this.Current = null;
					return false;
				}

				// Token: 0x060023E2 RID: 9186 RVA: 0x0008A564 File Offset: 0x00088764
				public void Dispose()
				{
					if (!this.d)
					{
						this.e.Dispose();
						this.d = true;
					}
				}

				// Token: 0x060023E3 RID: 9187 RVA: 0x0008A584 File Offset: 0x00088784
				public void Reset()
				{
					this.Dispose();
					this.d = false;
					this.e = VisNode.db.GetEnumerator();
				}

				// Token: 0x060023E4 RID: 9188 RVA: 0x0008A5A4 File Offset: 0x000887A4
				private bool Pass(VisNode cur)
				{
					if (this.data.Pass(cur))
					{
						this.Current = cur;
						return true;
					}
					return false;
				}

				// Token: 0x0400112A RID: 4394
				public ODBForwardEnumerator<VisNode> e;

				// Token: 0x0400112B RID: 4395
				public VisNode Current;

				// Token: 0x0400112C RID: 4396
				private bool d;

				// Token: 0x0400112D RID: 4397
				public VisNode.Search.PointRadiusData data;
			}

			// Token: 0x020003AE RID: 942
			public struct TraitMasked : IEnumerable, VisNode.Search.ISearch, IEnumerable<VisNode>, VisNode.Search.ISearch<VisNode.Search.Radial.TraitMasked.Enumerator>
			{
				// Token: 0x060023E5 RID: 9189 RVA: 0x0008A5C4 File Offset: 0x000887C4
				public TraitMasked(Vector3 point, float radius, Vis.Mask mask, Vis.Op op)
				{
					this.point = point;
					this.radius = radius;
					this.maskComp = new VisNode.Search.MaskCompareData(op, mask);
				}

				// Token: 0x060023E6 RID: 9190 RVA: 0x0008A5E4 File Offset: 0x000887E4
				IEnumerator<VisNode> IEnumerable<VisNode>.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x060023E7 RID: 9191 RVA: 0x0008A5F4 File Offset: 0x000887F4
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x060023E8 RID: 9192 RVA: 0x0008A604 File Offset: 0x00088804
				public VisNode.Search.Radial.TraitMasked.Enumerator GetEnumerator()
				{
					return new VisNode.Search.Radial.TraitMasked.Enumerator(new VisNode.Search.PointRadiusData(this.point, this.radius), this.maskComp);
				}

				// Token: 0x0400112E RID: 4398
				public Vector3 point;

				// Token: 0x0400112F RID: 4399
				public float radius;

				// Token: 0x04001130 RID: 4400
				public VisNode.Search.MaskCompareData maskComp;

				// Token: 0x020003AF RID: 943
				public struct Enumerator : IDisposable, IEnumerator, IEnumerator<VisNode>
				{
					// Token: 0x060023E9 RID: 9193 RVA: 0x0008A624 File Offset: 0x00088824
					public Enumerator(VisNode.Search.PointRadiusData pr, VisNode.Search.MaskCompareData mc)
					{
						this.Current = null;
						this.d = false;
						this.e = VisNode.db.GetEnumerator();
						this.data = pr;
						this.traitComp = mc;
					}

					// Token: 0x170008B6 RID: 2230
					// (get) Token: 0x060023EA RID: 9194 RVA: 0x0008A660 File Offset: 0x00088860
					VisNode IEnumerator<VisNode>.Current
					{
						get
						{
							return this.Current;
						}
					}

					// Token: 0x170008B7 RID: 2231
					// (get) Token: 0x060023EB RID: 9195 RVA: 0x0008A668 File Offset: 0x00088868
					object IEnumerator.Current
					{
						get
						{
							return this.Current;
						}
					}

					// Token: 0x060023EC RID: 9196 RVA: 0x0008A670 File Offset: 0x00088870
					public bool MoveNext()
					{
						while (this.e.MoveNext())
						{
							if (this.Pass(this.e.Current))
							{
								return true;
							}
						}
						this.Current = null;
						return false;
					}

					// Token: 0x060023ED RID: 9197 RVA: 0x0008A6A8 File Offset: 0x000888A8
					public void Dispose()
					{
						if (!this.d)
						{
							this.e.Dispose();
							this.d = true;
						}
					}

					// Token: 0x060023EE RID: 9198 RVA: 0x0008A6C8 File Offset: 0x000888C8
					public void Reset()
					{
						this.Dispose();
						this.d = false;
						this.e = VisNode.db.GetEnumerator();
					}

					// Token: 0x060023EF RID: 9199 RVA: 0x0008A6E8 File Offset: 0x000888E8
					private bool Pass(VisNode cur)
					{
						if (this.traitComp.Pass(cur._traitMask) && this.data.Pass(cur))
						{
							this.Current = cur;
							return true;
						}
						return false;
					}

					// Token: 0x04001131 RID: 4401
					public ODBForwardEnumerator<VisNode> e;

					// Token: 0x04001132 RID: 4402
					public VisNode Current;

					// Token: 0x04001133 RID: 4403
					private bool d;

					// Token: 0x04001134 RID: 4404
					public VisNode.Search.PointRadiusData data;

					// Token: 0x04001135 RID: 4405
					public VisNode.Search.MaskCompareData traitComp;
				}
			}

			// Token: 0x020003B0 RID: 944
			public struct SightMasked : IEnumerable, VisNode.Search.ISearch, IEnumerable<VisNode>, VisNode.Search.ISearch<VisNode.Search.Radial.SightMasked.Enumerator>
			{
				// Token: 0x060023F0 RID: 9200 RVA: 0x0008A71C File Offset: 0x0008891C
				public SightMasked(Vector3 point, float radius, Vis.Mask mask, Vis.Op op)
				{
					this.point = point;
					this.radius = radius;
					this.maskComp = new VisNode.Search.MaskCompareData(op, mask);
				}

				// Token: 0x060023F1 RID: 9201 RVA: 0x0008A73C File Offset: 0x0008893C
				IEnumerator<VisNode> IEnumerable<VisNode>.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x060023F2 RID: 9202 RVA: 0x0008A74C File Offset: 0x0008894C
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x060023F3 RID: 9203 RVA: 0x0008A75C File Offset: 0x0008895C
				public VisNode.Search.Radial.SightMasked.Enumerator GetEnumerator()
				{
					return new VisNode.Search.Radial.SightMasked.Enumerator(new VisNode.Search.PointRadiusData(this.point, this.radius), this.maskComp);
				}

				// Token: 0x04001136 RID: 4406
				public Vector3 point;

				// Token: 0x04001137 RID: 4407
				public float radius;

				// Token: 0x04001138 RID: 4408
				public VisNode.Search.MaskCompareData maskComp;

				// Token: 0x020003B1 RID: 945
				public struct Enumerator : IDisposable, IEnumerator, IEnumerator<VisNode>
				{
					// Token: 0x060023F4 RID: 9204 RVA: 0x0008A77C File Offset: 0x0008897C
					public Enumerator(VisNode.Search.PointRadiusData pr, VisNode.Search.MaskCompareData mc)
					{
						this.Current = null;
						this.d = false;
						this.e = VisNode.db.GetEnumerator();
						this.data = pr;
						this.viewComp = mc;
					}

					// Token: 0x170008B8 RID: 2232
					// (get) Token: 0x060023F5 RID: 9205 RVA: 0x0008A7B8 File Offset: 0x000889B8
					VisNode IEnumerator<VisNode>.Current
					{
						get
						{
							return this.Current;
						}
					}

					// Token: 0x170008B9 RID: 2233
					// (get) Token: 0x060023F6 RID: 9206 RVA: 0x0008A7C0 File Offset: 0x000889C0
					object IEnumerator.Current
					{
						get
						{
							return this.Current;
						}
					}

					// Token: 0x060023F7 RID: 9207 RVA: 0x0008A7C8 File Offset: 0x000889C8
					public bool MoveNext()
					{
						while (this.e.MoveNext())
						{
							if (this.Pass(this.e.Current))
							{
								return true;
							}
						}
						this.Current = null;
						return false;
					}

					// Token: 0x060023F8 RID: 9208 RVA: 0x0008A800 File Offset: 0x00088A00
					public void Dispose()
					{
						if (!this.d)
						{
							this.e.Dispose();
							this.d = true;
						}
					}

					// Token: 0x060023F9 RID: 9209 RVA: 0x0008A820 File Offset: 0x00088A20
					public void Reset()
					{
						this.Dispose();
						this.d = false;
						this.e = VisNode.db.GetEnumerator();
					}

					// Token: 0x060023FA RID: 9210 RVA: 0x0008A840 File Offset: 0x00088A40
					private bool Pass(VisNode cur)
					{
						if (this.viewComp.Pass(cur._sightMask) && this.data.Pass(cur))
						{
							this.Current = cur;
							return true;
						}
						return false;
					}

					// Token: 0x04001139 RID: 4409
					public ODBForwardEnumerator<VisNode> e;

					// Token: 0x0400113A RID: 4410
					public VisNode Current;

					// Token: 0x0400113B RID: 4411
					private bool d;

					// Token: 0x0400113C RID: 4412
					public VisNode.Search.PointRadiusData data;

					// Token: 0x0400113D RID: 4413
					public VisNode.Search.MaskCompareData viewComp;
				}
			}

			// Token: 0x020003B2 RID: 946
			public struct Audible : IEnumerable, VisNode.Search.ISearch, IEnumerable<VisNode>, VisNode.Search.ISearch<VisNode.Search.Radial.Audible.Enumerator>
			{
				// Token: 0x060023FB RID: 9211 RVA: 0x0008A874 File Offset: 0x00088A74
				public Audible(Vector3 point, float radius)
				{
					this.point = point;
					this.radius = radius;
				}

				// Token: 0x060023FC RID: 9212 RVA: 0x0008A884 File Offset: 0x00088A84
				IEnumerator<VisNode> IEnumerable<VisNode>.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x060023FD RID: 9213 RVA: 0x0008A894 File Offset: 0x00088A94
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x060023FE RID: 9214 RVA: 0x0008A8A4 File Offset: 0x00088AA4
				public VisNode.Search.Radial.Audible.Enumerator GetEnumerator()
				{
					return new VisNode.Search.Radial.Audible.Enumerator(new VisNode.Search.PointRadiusData(this.point, this.radius));
				}

				// Token: 0x0400113E RID: 4414
				public Vector3 point;

				// Token: 0x0400113F RID: 4415
				public float radius;

				// Token: 0x020003B3 RID: 947
				public struct Enumerator : IDisposable, IEnumerator, IEnumerator<VisNode>
				{
					// Token: 0x060023FF RID: 9215 RVA: 0x0008A8BC File Offset: 0x00088ABC
					public Enumerator(VisNode.Search.PointRadiusData pr)
					{
						this.Current = null;
						this.d = false;
						this.e = VisNode.db.GetEnumerator();
						this.data = pr;
					}

					// Token: 0x170008BA RID: 2234
					// (get) Token: 0x06002400 RID: 9216 RVA: 0x0008A8E4 File Offset: 0x00088AE4
					VisNode IEnumerator<VisNode>.Current
					{
						get
						{
							return this.Current;
						}
					}

					// Token: 0x170008BB RID: 2235
					// (get) Token: 0x06002401 RID: 9217 RVA: 0x0008A8EC File Offset: 0x00088AEC
					object IEnumerator.Current
					{
						get
						{
							return this.Current;
						}
					}

					// Token: 0x06002402 RID: 9218 RVA: 0x0008A8F4 File Offset: 0x00088AF4
					public bool MoveNext()
					{
						while (this.e.MoveNext())
						{
							if (this.Pass(this.e.Current))
							{
								return true;
							}
						}
						this.Current = null;
						return false;
					}

					// Token: 0x06002403 RID: 9219 RVA: 0x0008A92C File Offset: 0x00088B2C
					public void Dispose()
					{
						if (!this.d)
						{
							this.e.Dispose();
							this.d = true;
						}
					}

					// Token: 0x06002404 RID: 9220 RVA: 0x0008A94C File Offset: 0x00088B4C
					public void Reset()
					{
						this.Dispose();
						this.d = false;
						this.e = VisNode.db.GetEnumerator();
					}

					// Token: 0x06002405 RID: 9221 RVA: 0x0008A96C File Offset: 0x00088B6C
					private bool Pass(VisNode cur)
					{
						if (!cur.deaf && this.data.Pass(cur))
						{
							this.Current = cur;
							return true;
						}
						return false;
					}

					// Token: 0x04001140 RID: 4416
					public ODBForwardEnumerator<VisNode> e;

					// Token: 0x04001141 RID: 4417
					public VisNode Current;

					// Token: 0x04001142 RID: 4418
					private bool d;

					// Token: 0x04001143 RID: 4419
					public VisNode.Search.PointRadiusData data;
				}

				// Token: 0x020003B4 RID: 948
				public struct TraitMasked : IEnumerable, VisNode.Search.ISearch, IEnumerable<VisNode>, VisNode.Search.ISearch<VisNode.Search.Radial.Audible.TraitMasked.Enumerator>
				{
					// Token: 0x06002406 RID: 9222 RVA: 0x0008A9A0 File Offset: 0x00088BA0
					public TraitMasked(Vector3 point, float radius, Vis.Mask mask, Vis.Op op)
					{
						this.point = point;
						this.radius = radius;
						this.maskComp = new VisNode.Search.MaskCompareData(op, mask);
					}

					// Token: 0x06002407 RID: 9223 RVA: 0x0008A9C0 File Offset: 0x00088BC0
					IEnumerator<VisNode> IEnumerable<VisNode>.GetEnumerator()
					{
						return this.GetEnumerator();
					}

					// Token: 0x06002408 RID: 9224 RVA: 0x0008A9D0 File Offset: 0x00088BD0
					IEnumerator IEnumerable.GetEnumerator()
					{
						return this.GetEnumerator();
					}

					// Token: 0x06002409 RID: 9225 RVA: 0x0008A9E0 File Offset: 0x00088BE0
					public VisNode.Search.Radial.Audible.TraitMasked.Enumerator GetEnumerator()
					{
						return new VisNode.Search.Radial.Audible.TraitMasked.Enumerator(new VisNode.Search.PointRadiusData(this.point, this.radius), this.maskComp);
					}

					// Token: 0x04001144 RID: 4420
					public Vector3 point;

					// Token: 0x04001145 RID: 4421
					public float radius;

					// Token: 0x04001146 RID: 4422
					public VisNode.Search.MaskCompareData maskComp;

					// Token: 0x020003B5 RID: 949
					public struct Enumerator : IDisposable, IEnumerator, IEnumerator<VisNode>
					{
						// Token: 0x0600240A RID: 9226 RVA: 0x0008AA00 File Offset: 0x00088C00
						public Enumerator(VisNode.Search.PointRadiusData pr, VisNode.Search.MaskCompareData mc)
						{
							this.Current = null;
							this.d = false;
							this.e = VisNode.db.GetEnumerator();
							this.data = pr;
							this.traitComp = mc;
						}

						// Token: 0x170008BC RID: 2236
						// (get) Token: 0x0600240B RID: 9227 RVA: 0x0008AA3C File Offset: 0x00088C3C
						VisNode IEnumerator<VisNode>.Current
						{
							get
							{
								return this.Current;
							}
						}

						// Token: 0x170008BD RID: 2237
						// (get) Token: 0x0600240C RID: 9228 RVA: 0x0008AA44 File Offset: 0x00088C44
						object IEnumerator.Current
						{
							get
							{
								return this.Current;
							}
						}

						// Token: 0x0600240D RID: 9229 RVA: 0x0008AA4C File Offset: 0x00088C4C
						public bool MoveNext()
						{
							while (this.e.MoveNext())
							{
								if (this.Pass(this.e.Current))
								{
									return true;
								}
							}
							this.Current = null;
							return false;
						}

						// Token: 0x0600240E RID: 9230 RVA: 0x0008AA84 File Offset: 0x00088C84
						public void Dispose()
						{
							if (!this.d)
							{
								this.e.Dispose();
								this.d = true;
							}
						}

						// Token: 0x0600240F RID: 9231 RVA: 0x0008AAA4 File Offset: 0x00088CA4
						public void Reset()
						{
							this.Dispose();
							this.d = false;
							this.e = VisNode.db.GetEnumerator();
						}

						// Token: 0x06002410 RID: 9232 RVA: 0x0008AAC4 File Offset: 0x00088CC4
						private bool Pass(VisNode cur)
						{
							if (!cur.deaf && this.traitComp.Pass(cur._traitMask) && this.data.Pass(cur))
							{
								this.Current = cur;
								return true;
							}
							return false;
						}

						// Token: 0x04001147 RID: 4423
						public ODBForwardEnumerator<VisNode> e;

						// Token: 0x04001148 RID: 4424
						public VisNode Current;

						// Token: 0x04001149 RID: 4425
						private bool d;

						// Token: 0x0400114A RID: 4426
						public VisNode.Search.PointRadiusData data;

						// Token: 0x0400114B RID: 4427
						public VisNode.Search.MaskCompareData traitComp;
					}
				}

				// Token: 0x020003B6 RID: 950
				public struct SightMasked : IEnumerable, VisNode.Search.ISearch, IEnumerable<VisNode>, VisNode.Search.ISearch<VisNode.Search.Radial.Audible.SightMasked.Enumerator>
				{
					// Token: 0x06002411 RID: 9233 RVA: 0x0008AB10 File Offset: 0x00088D10
					public SightMasked(Vector3 point, float radius, Vis.Mask mask, Vis.Op op)
					{
						this.point = point;
						this.radius = radius;
						this.maskComp = new VisNode.Search.MaskCompareData(op, mask);
					}

					// Token: 0x06002412 RID: 9234 RVA: 0x0008AB30 File Offset: 0x00088D30
					IEnumerator<VisNode> IEnumerable<VisNode>.GetEnumerator()
					{
						return this.GetEnumerator();
					}

					// Token: 0x06002413 RID: 9235 RVA: 0x0008AB40 File Offset: 0x00088D40
					IEnumerator IEnumerable.GetEnumerator()
					{
						return this.GetEnumerator();
					}

					// Token: 0x06002414 RID: 9236 RVA: 0x0008AB50 File Offset: 0x00088D50
					public VisNode.Search.Radial.Audible.SightMasked.Enumerator GetEnumerator()
					{
						return new VisNode.Search.Radial.Audible.SightMasked.Enumerator(new VisNode.Search.PointRadiusData(this.point, this.radius), this.maskComp);
					}

					// Token: 0x0400114C RID: 4428
					public Vector3 point;

					// Token: 0x0400114D RID: 4429
					public float radius;

					// Token: 0x0400114E RID: 4430
					public VisNode.Search.MaskCompareData maskComp;

					// Token: 0x020003B7 RID: 951
					public struct Enumerator : IDisposable, IEnumerator, IEnumerator<VisNode>
					{
						// Token: 0x06002415 RID: 9237 RVA: 0x0008AB70 File Offset: 0x00088D70
						public Enumerator(VisNode.Search.PointRadiusData pr, VisNode.Search.MaskCompareData mc)
						{
							this.Current = null;
							this.d = false;
							this.e = VisNode.db.GetEnumerator();
							this.data = pr;
							this.viewComp = mc;
						}

						// Token: 0x170008BE RID: 2238
						// (get) Token: 0x06002416 RID: 9238 RVA: 0x0008ABAC File Offset: 0x00088DAC
						VisNode IEnumerator<VisNode>.Current
						{
							get
							{
								return this.Current;
							}
						}

						// Token: 0x170008BF RID: 2239
						// (get) Token: 0x06002417 RID: 9239 RVA: 0x0008ABB4 File Offset: 0x00088DB4
						object IEnumerator.Current
						{
							get
							{
								return this.Current;
							}
						}

						// Token: 0x06002418 RID: 9240 RVA: 0x0008ABBC File Offset: 0x00088DBC
						public bool MoveNext()
						{
							while (this.e.MoveNext())
							{
								if (this.Pass(this.e.Current))
								{
									return true;
								}
							}
							this.Current = null;
							return false;
						}

						// Token: 0x06002419 RID: 9241 RVA: 0x0008ABF4 File Offset: 0x00088DF4
						public void Dispose()
						{
							if (!this.d)
							{
								this.e.Dispose();
								this.d = true;
							}
						}

						// Token: 0x0600241A RID: 9242 RVA: 0x0008AC14 File Offset: 0x00088E14
						public void Reset()
						{
							this.Dispose();
							this.d = false;
							this.e = VisNode.db.GetEnumerator();
						}

						// Token: 0x0600241B RID: 9243 RVA: 0x0008AC34 File Offset: 0x00088E34
						private bool Pass(VisNode cur)
						{
							if (!cur.deaf && this.viewComp.Pass(cur._sightMask) && this.data.Pass(cur))
							{
								this.Current = cur;
								return true;
							}
							return false;
						}

						// Token: 0x0400114F RID: 4431
						public ODBForwardEnumerator<VisNode> e;

						// Token: 0x04001150 RID: 4432
						public VisNode Current;

						// Token: 0x04001151 RID: 4433
						private bool d;

						// Token: 0x04001152 RID: 4434
						public VisNode.Search.PointRadiusData data;

						// Token: 0x04001153 RID: 4435
						public VisNode.Search.MaskCompareData viewComp;
					}
				}
			}
		}

		// Token: 0x020003B8 RID: 952
		public struct Point : IEnumerable, VisNode.Search.ISearch, IEnumerable<VisNode>, VisNode.Search.ISearch<VisNode.Search.Point.Enumerator>
		{
			// Token: 0x0600241C RID: 9244 RVA: 0x0008AC80 File Offset: 0x00088E80
			public Point(Vector3 point)
			{
				this.point = point;
			}

			// Token: 0x0600241D RID: 9245 RVA: 0x0008AC8C File Offset: 0x00088E8C
			IEnumerator<VisNode> IEnumerable<VisNode>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0600241E RID: 9246 RVA: 0x0008AC9C File Offset: 0x00088E9C
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0600241F RID: 9247 RVA: 0x0008ACAC File Offset: 0x00088EAC
			public VisNode.Search.Point.Enumerator GetEnumerator()
			{
				return new VisNode.Search.Point.Enumerator(new VisNode.Search.PointVisibilityData(this.point));
			}

			// Token: 0x04001154 RID: 4436
			public Vector3 point;

			// Token: 0x020003B9 RID: 953
			public struct Enumerator : IDisposable, IEnumerator, IEnumerator<VisNode>
			{
				// Token: 0x06002420 RID: 9248 RVA: 0x0008ACC0 File Offset: 0x00088EC0
				public Enumerator(VisNode.Search.PointVisibilityData pv)
				{
					this.Current = null;
					this.d = false;
					this.e = VisNode.db.GetEnumerator();
					this.data = pv;
				}

				// Token: 0x170008C0 RID: 2240
				// (get) Token: 0x06002421 RID: 9249 RVA: 0x0008ACE8 File Offset: 0x00088EE8
				VisNode IEnumerator<VisNode>.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x170008C1 RID: 2241
				// (get) Token: 0x06002422 RID: 9250 RVA: 0x0008ACF0 File Offset: 0x00088EF0
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x06002423 RID: 9251 RVA: 0x0008ACF8 File Offset: 0x00088EF8
				public bool MoveNext()
				{
					while (this.e.MoveNext())
					{
						if (this.Pass(this.e.Current))
						{
							return true;
						}
					}
					this.Current = null;
					return false;
				}

				// Token: 0x06002424 RID: 9252 RVA: 0x0008AD30 File Offset: 0x00088F30
				public void Dispose()
				{
					if (!this.d)
					{
						this.e.Dispose();
						this.d = true;
					}
				}

				// Token: 0x06002425 RID: 9253 RVA: 0x0008AD50 File Offset: 0x00088F50
				public void Reset()
				{
					this.Dispose();
					this.d = false;
					this.e = VisNode.db.GetEnumerator();
				}

				// Token: 0x06002426 RID: 9254 RVA: 0x0008AD70 File Offset: 0x00088F70
				private bool Pass(VisNode cur)
				{
					if (this.data.Pass(cur))
					{
						this.Current = cur;
						return true;
					}
					return false;
				}

				// Token: 0x04001155 RID: 4437
				public ODBForwardEnumerator<VisNode> e;

				// Token: 0x04001156 RID: 4438
				public VisNode Current;

				// Token: 0x04001157 RID: 4439
				private bool d;

				// Token: 0x04001158 RID: 4440
				public VisNode.Search.PointVisibilityData data;
			}

			// Token: 0x020003BA RID: 954
			public struct TraitMasked : IEnumerable, VisNode.Search.ISearch, IEnumerable<VisNode>, VisNode.Search.ISearch<VisNode.Search.Point.TraitMasked.Enumerator>
			{
				// Token: 0x06002427 RID: 9255 RVA: 0x0008AD90 File Offset: 0x00088F90
				public TraitMasked(Vector3 point, Vis.Mask mask, Vis.Op op)
				{
					this.point = point;
					this.maskComp = new VisNode.Search.MaskCompareData(op, mask);
				}

				// Token: 0x06002428 RID: 9256 RVA: 0x0008ADA8 File Offset: 0x00088FA8
				IEnumerator<VisNode> IEnumerable<VisNode>.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x06002429 RID: 9257 RVA: 0x0008ADB8 File Offset: 0x00088FB8
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x0600242A RID: 9258 RVA: 0x0008ADC8 File Offset: 0x00088FC8
				public VisNode.Search.Point.TraitMasked.Enumerator GetEnumerator()
				{
					return new VisNode.Search.Point.TraitMasked.Enumerator(new VisNode.Search.PointVisibilityData(this.point), this.maskComp);
				}

				// Token: 0x04001159 RID: 4441
				public Vector3 point;

				// Token: 0x0400115A RID: 4442
				public VisNode.Search.MaskCompareData maskComp;

				// Token: 0x020003BB RID: 955
				public struct Enumerator : IDisposable, IEnumerator, IEnumerator<VisNode>
				{
					// Token: 0x0600242B RID: 9259 RVA: 0x0008ADE0 File Offset: 0x00088FE0
					public Enumerator(VisNode.Search.PointVisibilityData pv, VisNode.Search.MaskCompareData mc)
					{
						this.Current = null;
						this.d = false;
						this.e = VisNode.db.GetEnumerator();
						this.data = pv;
						this.traitComp = mc;
					}

					// Token: 0x170008C2 RID: 2242
					// (get) Token: 0x0600242C RID: 9260 RVA: 0x0008AE1C File Offset: 0x0008901C
					VisNode IEnumerator<VisNode>.Current
					{
						get
						{
							return this.Current;
						}
					}

					// Token: 0x170008C3 RID: 2243
					// (get) Token: 0x0600242D RID: 9261 RVA: 0x0008AE24 File Offset: 0x00089024
					object IEnumerator.Current
					{
						get
						{
							return this.Current;
						}
					}

					// Token: 0x0600242E RID: 9262 RVA: 0x0008AE2C File Offset: 0x0008902C
					public bool MoveNext()
					{
						while (this.e.MoveNext())
						{
							if (this.Pass(this.e.Current))
							{
								return true;
							}
						}
						this.Current = null;
						return false;
					}

					// Token: 0x0600242F RID: 9263 RVA: 0x0008AE64 File Offset: 0x00089064
					public void Dispose()
					{
						if (!this.d)
						{
							this.e.Dispose();
							this.d = true;
						}
					}

					// Token: 0x06002430 RID: 9264 RVA: 0x0008AE84 File Offset: 0x00089084
					public void Reset()
					{
						this.Dispose();
						this.d = false;
						this.e = VisNode.db.GetEnumerator();
					}

					// Token: 0x06002431 RID: 9265 RVA: 0x0008AEA4 File Offset: 0x000890A4
					private bool Pass(VisNode cur)
					{
						if (this.traitComp.Pass(cur._traitMask) && this.data.Pass(cur))
						{
							this.Current = cur;
							return true;
						}
						return false;
					}

					// Token: 0x0400115B RID: 4443
					public ODBForwardEnumerator<VisNode> e;

					// Token: 0x0400115C RID: 4444
					public VisNode Current;

					// Token: 0x0400115D RID: 4445
					private bool d;

					// Token: 0x0400115E RID: 4446
					public VisNode.Search.PointVisibilityData data;

					// Token: 0x0400115F RID: 4447
					public VisNode.Search.MaskCompareData traitComp;
				}
			}

			// Token: 0x020003BC RID: 956
			public struct SightMasked : IEnumerable, VisNode.Search.ISearch, IEnumerable<VisNode>, VisNode.Search.ISearch<VisNode.Search.Point.SightMasked.Enumerator>
			{
				// Token: 0x06002432 RID: 9266 RVA: 0x0008AED8 File Offset: 0x000890D8
				public SightMasked(Vector3 point, Vis.Mask mask, Vis.Op op)
				{
					this.point = point;
					this.maskComp = new VisNode.Search.MaskCompareData(op, mask);
				}

				// Token: 0x06002433 RID: 9267 RVA: 0x0008AEF0 File Offset: 0x000890F0
				IEnumerator<VisNode> IEnumerable<VisNode>.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x06002434 RID: 9268 RVA: 0x0008AF00 File Offset: 0x00089100
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x06002435 RID: 9269 RVA: 0x0008AF10 File Offset: 0x00089110
				public VisNode.Search.Point.SightMasked.Enumerator GetEnumerator()
				{
					return new VisNode.Search.Point.SightMasked.Enumerator(new VisNode.Search.PointVisibilityData(this.point), this.maskComp);
				}

				// Token: 0x04001160 RID: 4448
				public Vector3 point;

				// Token: 0x04001161 RID: 4449
				public VisNode.Search.MaskCompareData maskComp;

				// Token: 0x020003BD RID: 957
				public struct Enumerator : IDisposable, IEnumerator, IEnumerator<VisNode>
				{
					// Token: 0x06002436 RID: 9270 RVA: 0x0008AF28 File Offset: 0x00089128
					public Enumerator(VisNode.Search.PointVisibilityData pv, VisNode.Search.MaskCompareData mc)
					{
						this.Current = null;
						this.d = false;
						this.e = VisNode.db.GetEnumerator();
						this.data = pv;
						this.viewComp = mc;
					}

					// Token: 0x170008C4 RID: 2244
					// (get) Token: 0x06002437 RID: 9271 RVA: 0x0008AF64 File Offset: 0x00089164
					VisNode IEnumerator<VisNode>.Current
					{
						get
						{
							return this.Current;
						}
					}

					// Token: 0x170008C5 RID: 2245
					// (get) Token: 0x06002438 RID: 9272 RVA: 0x0008AF6C File Offset: 0x0008916C
					object IEnumerator.Current
					{
						get
						{
							return this.Current;
						}
					}

					// Token: 0x06002439 RID: 9273 RVA: 0x0008AF74 File Offset: 0x00089174
					public bool MoveNext()
					{
						while (this.e.MoveNext())
						{
							if (this.Pass(this.e.Current))
							{
								return true;
							}
						}
						this.Current = null;
						return false;
					}

					// Token: 0x0600243A RID: 9274 RVA: 0x0008AFAC File Offset: 0x000891AC
					public void Dispose()
					{
						if (!this.d)
						{
							this.e.Dispose();
							this.d = true;
						}
					}

					// Token: 0x0600243B RID: 9275 RVA: 0x0008AFCC File Offset: 0x000891CC
					public void Reset()
					{
						this.Dispose();
						this.d = false;
						this.e = VisNode.db.GetEnumerator();
					}

					// Token: 0x0600243C RID: 9276 RVA: 0x0008AFEC File Offset: 0x000891EC
					private bool Pass(VisNode cur)
					{
						if (this.viewComp.Pass(cur._sightMask) && this.data.Pass(cur))
						{
							this.Current = cur;
							return true;
						}
						return false;
					}

					// Token: 0x04001162 RID: 4450
					public ODBForwardEnumerator<VisNode> e;

					// Token: 0x04001163 RID: 4451
					public VisNode Current;

					// Token: 0x04001164 RID: 4452
					private bool d;

					// Token: 0x04001165 RID: 4453
					public VisNode.Search.PointVisibilityData data;

					// Token: 0x04001166 RID: 4454
					public VisNode.Search.MaskCompareData viewComp;
				}
			}

			// Token: 0x020003BE RID: 958
			public struct Visual : IEnumerable, VisNode.Search.ISearch, IEnumerable<VisNode>, VisNode.Search.ISearch<VisNode.Search.Point.Visual.Enumerator>
			{
				// Token: 0x0600243D RID: 9277 RVA: 0x0008B020 File Offset: 0x00089220
				public Visual(Vector3 point)
				{
					this.point = point;
				}

				// Token: 0x0600243E RID: 9278 RVA: 0x0008B02C File Offset: 0x0008922C
				IEnumerator<VisNode> IEnumerable<VisNode>.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x0600243F RID: 9279 RVA: 0x0008B03C File Offset: 0x0008923C
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x06002440 RID: 9280 RVA: 0x0008B04C File Offset: 0x0008924C
				public VisNode.Search.Point.Visual.Enumerator GetEnumerator()
				{
					return new VisNode.Search.Point.Visual.Enumerator(new VisNode.Search.PointVisibilityData(this.point));
				}

				// Token: 0x04001167 RID: 4455
				public Vector3 point;

				// Token: 0x020003BF RID: 959
				public struct Enumerator : IDisposable, IEnumerator, IEnumerator<VisNode>
				{
					// Token: 0x06002441 RID: 9281 RVA: 0x0008B060 File Offset: 0x00089260
					public Enumerator(VisNode.Search.PointVisibilityData pv)
					{
						this.Current = null;
						this.d = false;
						this.e = VisNode.db.GetEnumerator();
						this.data = pv;
					}

					// Token: 0x170008C6 RID: 2246
					// (get) Token: 0x06002442 RID: 9282 RVA: 0x0008B088 File Offset: 0x00089288
					VisNode IEnumerator<VisNode>.Current
					{
						get
						{
							return this.Current;
						}
					}

					// Token: 0x170008C7 RID: 2247
					// (get) Token: 0x06002443 RID: 9283 RVA: 0x0008B090 File Offset: 0x00089290
					object IEnumerator.Current
					{
						get
						{
							return this.Current;
						}
					}

					// Token: 0x06002444 RID: 9284 RVA: 0x0008B098 File Offset: 0x00089298
					public bool MoveNext()
					{
						while (this.e.MoveNext())
						{
							if (this.Pass(this.e.Current))
							{
								return true;
							}
						}
						this.Current = null;
						return false;
					}

					// Token: 0x06002445 RID: 9285 RVA: 0x0008B0D0 File Offset: 0x000892D0
					public void Dispose()
					{
						if (!this.d)
						{
							this.e.Dispose();
							this.d = true;
						}
					}

					// Token: 0x06002446 RID: 9286 RVA: 0x0008B0F0 File Offset: 0x000892F0
					public void Reset()
					{
						this.Dispose();
						this.d = false;
						this.e = VisNode.db.GetEnumerator();
					}

					// Token: 0x06002447 RID: 9287 RVA: 0x0008B110 File Offset: 0x00089310
					private bool Pass(VisNode cur)
					{
						return false;
					}

					// Token: 0x04001168 RID: 4456
					public ODBForwardEnumerator<VisNode> e;

					// Token: 0x04001169 RID: 4457
					public VisNode Current;

					// Token: 0x0400116A RID: 4458
					private bool d;

					// Token: 0x0400116B RID: 4459
					public VisNode.Search.PointVisibilityData data;
				}

				// Token: 0x020003C0 RID: 960
				public struct TraitMasked : IEnumerable, VisNode.Search.ISearch, IEnumerable<VisNode>, VisNode.Search.ISearch<VisNode.Search.Point.Visual.TraitMasked.Enumerator>
				{
					// Token: 0x06002448 RID: 9288 RVA: 0x0008B114 File Offset: 0x00089314
					public TraitMasked(Vector3 point, Vis.Mask mask, Vis.Op op)
					{
						this.point = point;
						this.maskComp = new VisNode.Search.MaskCompareData(op, mask);
					}

					// Token: 0x06002449 RID: 9289 RVA: 0x0008B12C File Offset: 0x0008932C
					IEnumerator<VisNode> IEnumerable<VisNode>.GetEnumerator()
					{
						return this.GetEnumerator();
					}

					// Token: 0x0600244A RID: 9290 RVA: 0x0008B13C File Offset: 0x0008933C
					IEnumerator IEnumerable.GetEnumerator()
					{
						return this.GetEnumerator();
					}

					// Token: 0x0600244B RID: 9291 RVA: 0x0008B14C File Offset: 0x0008934C
					public VisNode.Search.Point.Visual.TraitMasked.Enumerator GetEnumerator()
					{
						return new VisNode.Search.Point.Visual.TraitMasked.Enumerator(new VisNode.Search.PointVisibilityData(this.point), this.maskComp);
					}

					// Token: 0x0400116C RID: 4460
					public Vector3 point;

					// Token: 0x0400116D RID: 4461
					public VisNode.Search.MaskCompareData maskComp;

					// Token: 0x020003C1 RID: 961
					public struct Enumerator : IDisposable, IEnumerator, IEnumerator<VisNode>
					{
						// Token: 0x0600244C RID: 9292 RVA: 0x0008B164 File Offset: 0x00089364
						public Enumerator(VisNode.Search.PointVisibilityData pv, VisNode.Search.MaskCompareData mc)
						{
							this.Current = null;
							this.d = false;
							this.e = VisNode.db.GetEnumerator();
							this.data = pv;
							this.traitComp = mc;
						}

						// Token: 0x170008C8 RID: 2248
						// (get) Token: 0x0600244D RID: 9293 RVA: 0x0008B1A0 File Offset: 0x000893A0
						VisNode IEnumerator<VisNode>.Current
						{
							get
							{
								return this.Current;
							}
						}

						// Token: 0x170008C9 RID: 2249
						// (get) Token: 0x0600244E RID: 9294 RVA: 0x0008B1A8 File Offset: 0x000893A8
						object IEnumerator.Current
						{
							get
							{
								return this.Current;
							}
						}

						// Token: 0x0600244F RID: 9295 RVA: 0x0008B1B0 File Offset: 0x000893B0
						public bool MoveNext()
						{
							while (this.e.MoveNext())
							{
								if (this.Pass(this.e.Current))
								{
									return true;
								}
							}
							this.Current = null;
							return false;
						}

						// Token: 0x06002450 RID: 9296 RVA: 0x0008B1E8 File Offset: 0x000893E8
						public void Dispose()
						{
							if (!this.d)
							{
								this.e.Dispose();
								this.d = true;
							}
						}

						// Token: 0x06002451 RID: 9297 RVA: 0x0008B208 File Offset: 0x00089408
						public void Reset()
						{
							this.Dispose();
							this.d = false;
							this.e = VisNode.db.GetEnumerator();
						}

						// Token: 0x06002452 RID: 9298 RVA: 0x0008B228 File Offset: 0x00089428
						private bool Pass(VisNode cur)
						{
							return false;
						}

						// Token: 0x0400116E RID: 4462
						public ODBForwardEnumerator<VisNode> e;

						// Token: 0x0400116F RID: 4463
						public VisNode Current;

						// Token: 0x04001170 RID: 4464
						private bool d;

						// Token: 0x04001171 RID: 4465
						public VisNode.Search.PointVisibilityData data;

						// Token: 0x04001172 RID: 4466
						public VisNode.Search.MaskCompareData traitComp;
					}
				}

				// Token: 0x020003C2 RID: 962
				public struct SightMasked : IEnumerable, VisNode.Search.ISearch, IEnumerable<VisNode>, VisNode.Search.ISearch<VisNode.Search.Point.Visual.SightMasked.Enumerator>
				{
					// Token: 0x06002453 RID: 9299 RVA: 0x0008B22C File Offset: 0x0008942C
					public SightMasked(Vector3 point, Vis.Mask mask, Vis.Op op)
					{
						this.point = point;
						this.maskComp = new VisNode.Search.MaskCompareData(op, mask);
					}

					// Token: 0x06002454 RID: 9300 RVA: 0x0008B244 File Offset: 0x00089444
					IEnumerator<VisNode> IEnumerable<VisNode>.GetEnumerator()
					{
						return this.GetEnumerator();
					}

					// Token: 0x06002455 RID: 9301 RVA: 0x0008B254 File Offset: 0x00089454
					IEnumerator IEnumerable.GetEnumerator()
					{
						return this.GetEnumerator();
					}

					// Token: 0x06002456 RID: 9302 RVA: 0x0008B264 File Offset: 0x00089464
					public VisNode.Search.Point.Visual.SightMasked.Enumerator GetEnumerator()
					{
						return new VisNode.Search.Point.Visual.SightMasked.Enumerator(new VisNode.Search.PointVisibilityData(this.point), this.maskComp);
					}

					// Token: 0x04001173 RID: 4467
					public Vector3 point;

					// Token: 0x04001174 RID: 4468
					public VisNode.Search.MaskCompareData maskComp;

					// Token: 0x020003C3 RID: 963
					public struct Enumerator : IDisposable, IEnumerator, IEnumerator<VisNode>
					{
						// Token: 0x06002457 RID: 9303 RVA: 0x0008B27C File Offset: 0x0008947C
						public Enumerator(VisNode.Search.PointVisibilityData pv, VisNode.Search.MaskCompareData mc)
						{
							this.Current = null;
							this.d = false;
							this.e = VisNode.db.GetEnumerator();
							this.data = pv;
							this.viewComp = mc;
						}

						// Token: 0x170008CA RID: 2250
						// (get) Token: 0x06002458 RID: 9304 RVA: 0x0008B2B8 File Offset: 0x000894B8
						VisNode IEnumerator<VisNode>.Current
						{
							get
							{
								return this.Current;
							}
						}

						// Token: 0x170008CB RID: 2251
						// (get) Token: 0x06002459 RID: 9305 RVA: 0x0008B2C0 File Offset: 0x000894C0
						object IEnumerator.Current
						{
							get
							{
								return this.Current;
							}
						}

						// Token: 0x0600245A RID: 9306 RVA: 0x0008B2C8 File Offset: 0x000894C8
						public bool MoveNext()
						{
							while (this.e.MoveNext())
							{
								if (this.Pass(this.e.Current))
								{
									return true;
								}
							}
							this.Current = null;
							return false;
						}

						// Token: 0x0600245B RID: 9307 RVA: 0x0008B300 File Offset: 0x00089500
						public void Dispose()
						{
							if (!this.d)
							{
								this.e.Dispose();
								this.d = true;
							}
						}

						// Token: 0x0600245C RID: 9308 RVA: 0x0008B320 File Offset: 0x00089520
						public void Reset()
						{
							this.Dispose();
							this.d = false;
							this.e = VisNode.db.GetEnumerator();
						}

						// Token: 0x0600245D RID: 9309 RVA: 0x0008B340 File Offset: 0x00089540
						private bool Pass(VisNode cur)
						{
							return false;
						}

						// Token: 0x04001175 RID: 4469
						public ODBForwardEnumerator<VisNode> e;

						// Token: 0x04001176 RID: 4470
						public VisNode Current;

						// Token: 0x04001177 RID: 4471
						private bool d;

						// Token: 0x04001178 RID: 4472
						public VisNode.Search.PointVisibilityData data;

						// Token: 0x04001179 RID: 4473
						public VisNode.Search.MaskCompareData viewComp;
					}
				}
			}
		}
	}
}
