using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// Token: 0x0200044F RID: 1103
[AddComponentMenu("Vis/Node")]
public class VisNode : IDLocal
{
	// Token: 0x170008FB RID: 2299
	// (set) Token: 0x0600269E RID: 9886 RVA: 0x0008CCC0 File Offset: 0x0008AEC0
	internal global::VisReactor __reactor
	{
		set
		{
			this.reactor = value;
		}
	}

	// Token: 0x170008FC RID: 2300
	// (get) Token: 0x0600269F RID: 9887 RVA: 0x0008CCCC File Offset: 0x0008AECC
	// (set) Token: 0x060026A0 RID: 9888 RVA: 0x0008CCD4 File Offset: 0x0008AED4
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

	// Token: 0x170008FD RID: 2301
	// (get) Token: 0x060026A1 RID: 9889 RVA: 0x0008CCE4 File Offset: 0x0008AEE4
	// (set) Token: 0x060026A2 RID: 9890 RVA: 0x0008CCEC File Offset: 0x0008AEEC
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

	// Token: 0x170008FE RID: 2302
	// (get) Token: 0x060026A3 RID: 9891 RVA: 0x0008CCF8 File Offset: 0x0008AEF8
	// (set) Token: 0x060026A4 RID: 9892 RVA: 0x0008CD20 File Offset: 0x0008AF20
	public global::Vis.Mask viewMask
	{
		get
		{
			return new global::Vis.Mask
			{
				data = this._sightMask
			};
		}
		set
		{
			this._sightMask = value.data;
		}
	}

	// Token: 0x170008FF RID: 2303
	// (get) Token: 0x060026A5 RID: 9893 RVA: 0x0008CD30 File Offset: 0x0008AF30
	// (set) Token: 0x060026A6 RID: 9894 RVA: 0x0008CD58 File Offset: 0x0008AF58
	public global::Vis.Mask spectMask
	{
		get
		{
			return new global::Vis.Mask
			{
				data = this._spectMask
			};
		}
		set
		{
			this._spectMask = value.data;
		}
	}

	// Token: 0x17000900 RID: 2304
	// (get) Token: 0x060026A7 RID: 9895 RVA: 0x0008CD68 File Offset: 0x0008AF68
	// (set) Token: 0x060026A8 RID: 9896 RVA: 0x0008CD90 File Offset: 0x0008AF90
	public global::Vis.Mask traitMask
	{
		get
		{
			return new global::Vis.Mask
			{
				data = this._traitMask
			};
		}
		set
		{
			this._traitMask = value.data;
		}
	}

	// Token: 0x17000901 RID: 2305
	// (get) Token: 0x060026A9 RID: 9897 RVA: 0x0008CDA0 File Offset: 0x0008AFA0
	public global::Vis.Mask seenMask
	{
		get
		{
			return new global::Vis.Mask
			{
				data = this._seeMask
			};
		}
	}

	// Token: 0x17000902 RID: 2306
	// (get) Token: 0x060026AA RID: 9898 RVA: 0x0008CDC8 File Offset: 0x0008AFC8
	public global::Vis.Stamp stamp
	{
		get
		{
			return this._stamp;
		}
	}

	// Token: 0x17000903 RID: 2307
	// (get) Token: 0x060026AB RID: 9899 RVA: 0x0008CDD0 File Offset: 0x0008AFD0
	public Vector3 position
	{
		get
		{
			return this._stamp.position;
		}
	}

	// Token: 0x17000904 RID: 2308
	// (get) Token: 0x060026AC RID: 9900 RVA: 0x0008CDE0 File Offset: 0x0008AFE0
	public Vector3 forward
	{
		get
		{
			return this._stamp.forward;
		}
	}

	// Token: 0x17000905 RID: 2309
	// (get) Token: 0x060026AD RID: 9901 RVA: 0x0008CDF0 File Offset: 0x0008AFF0
	public Quaternion rotation
	{
		get
		{
			return this._stamp.rotation;
		}
	}

	// Token: 0x17000906 RID: 2310
	// (get) Token: 0x060026AE RID: 9902 RVA: 0x0008CE00 File Offset: 0x0008B000
	public Plane plane
	{
		get
		{
			Vector4 vector = this._stamp.forward;
			return new Plane(new Vector3(vector.x, vector.y, vector.z), vector.w);
		}
	}

	// Token: 0x17000907 RID: 2311
	// (get) Token: 0x060026AF RID: 9903 RVA: 0x0008CE44 File Offset: 0x0008B044
	public int numSight
	{
		get
		{
			return this.sight.count;
		}
	}

	// Token: 0x17000908 RID: 2312
	// (get) Token: 0x060026B0 RID: 9904 RVA: 0x0008CE54 File Offset: 0x0008B054
	public bool anySight
	{
		get
		{
			return this.sight.any;
		}
	}

	// Token: 0x17000909 RID: 2313
	// (get) Token: 0x060026B1 RID: 9905 RVA: 0x0008CE64 File Offset: 0x0008B064
	public bool anySightNew
	{
		get
		{
			return this.sight.add;
		}
	}

	// Token: 0x1700090A RID: 2314
	// (get) Token: 0x060026B2 RID: 9906 RVA: 0x0008CE74 File Offset: 0x0008B074
	public bool anySightLost
	{
		get
		{
			return this.sight.rem;
		}
	}

	// Token: 0x1700090B RID: 2315
	// (get) Token: 0x060026B3 RID: 9907 RVA: 0x0008CE84 File Offset: 0x0008B084
	public bool anySightHad
	{
		get
		{
			return this.sight.had;
		}
	}

	// Token: 0x1700090C RID: 2316
	// (get) Token: 0x060026B4 RID: 9908 RVA: 0x0008CE94 File Offset: 0x0008B094
	public int numSpectators
	{
		get
		{
			return this.spect.count;
		}
	}

	// Token: 0x1700090D RID: 2317
	// (get) Token: 0x060026B5 RID: 9909 RVA: 0x0008CEA4 File Offset: 0x0008B0A4
	public bool anySpectators
	{
		get
		{
			return this.spect.any;
		}
	}

	// Token: 0x1700090E RID: 2318
	// (get) Token: 0x060026B6 RID: 9910 RVA: 0x0008CEB4 File Offset: 0x0008B0B4
	public bool anySpectatorsNew
	{
		get
		{
			return this.spect.add;
		}
	}

	// Token: 0x1700090F RID: 2319
	// (get) Token: 0x060026B7 RID: 9911 RVA: 0x0008CEC4 File Offset: 0x0008B0C4
	public bool anySpectatorsLost
	{
		get
		{
			return this.spect.rem;
		}
	}

	// Token: 0x17000910 RID: 2320
	// (get) Token: 0x060026B8 RID: 9912 RVA: 0x0008CED4 File Offset: 0x0008B0D4
	public bool anySpectatorsHad
	{
		get
		{
			return this.spect.had;
		}
	}

	// Token: 0x060026B9 RID: 9913 RVA: 0x0008CEE4 File Offset: 0x0008B0E4
	public bool CanSeeAny(global::Vis.Life life)
	{
		return (this._seeMask & (int)life) != 0;
	}

	// Token: 0x060026BA RID: 9914 RVA: 0x0008CEF4 File Offset: 0x0008B0F4
	public bool CanSeeAny(global::Vis.Status status)
	{
		return (this._seeMask & (int)((int)status << 8)) != 0;
	}

	// Token: 0x060026BB RID: 9915 RVA: 0x0008CF08 File Offset: 0x0008B108
	public bool CanSeeAny(global::Vis.Role role)
	{
		return (this._seeMask & (int)((int)role << 24)) != 0;
	}

	// Token: 0x060026BC RID: 9916 RVA: 0x0008CF1C File Offset: 0x0008B11C
	public bool CanSeeAny(global::Vis.Mask mask)
	{
		return (this._seeMask & mask.data) != 0;
	}

	// Token: 0x060026BD RID: 9917 RVA: 0x0008CF34 File Offset: 0x0008B134
	public bool CanSee(global::Vis.Trait trait)
	{
		return (this._seeMask & 1 << (int)trait) != 0;
	}

	// Token: 0x060026BE RID: 9918 RVA: 0x0008CF4C File Offset: 0x0008B14C
	public bool CanSee(global::Vis.Life life)
	{
		return (this._seeMask & (int)life) == (int)life;
	}

	// Token: 0x060026BF RID: 9919 RVA: 0x0008CF5C File Offset: 0x0008B15C
	public bool CanSee(global::Vis.Status status)
	{
		return (this._seeMask >> 8 & (int)status) == (int)status;
	}

	// Token: 0x060026C0 RID: 9920 RVA: 0x0008CF6C File Offset: 0x0008B16C
	public bool CanSee(global::Vis.Role role)
	{
		return (this._seeMask >> 24 & (int)role) == (int)role;
	}

	// Token: 0x060026C1 RID: 9921 RVA: 0x0008CF7C File Offset: 0x0008B17C
	public bool CanSee(global::Vis.Mask mask)
	{
		return (this._seeMask & mask.data) == mask.data;
	}

	// Token: 0x060026C2 RID: 9922 RVA: 0x0008CF98 File Offset: 0x0008B198
	public bool CanSeeOnly(global::Vis.Life life)
	{
		return (this._seeMask & 7) == (int)life;
	}

	// Token: 0x060026C3 RID: 9923 RVA: 0x0008CFA8 File Offset: 0x0008B1A8
	public bool CanSeeOnly(global::Vis.Status status)
	{
		return (this._seeMask & 32512) == (int)((int)status << 8);
	}

	// Token: 0x060026C4 RID: 9924 RVA: 0x0008CFBC File Offset: 0x0008B1BC
	public bool CanSeeOnly(global::Vis.Role role)
	{
		return (this._seeMask & -16777216) == (int)((int)role << 24);
	}

	// Token: 0x060026C5 RID: 9925 RVA: 0x0008CFD0 File Offset: 0x0008B1D0
	public bool CanSeeOnly(global::Vis.Mask mask)
	{
		return this._seeMask == mask.data;
	}

	// Token: 0x060026C6 RID: 9926 RVA: 0x0008CFE4 File Offset: 0x0008B1E4
	public bool CanSeeOnly(global::Vis.Trait trait)
	{
		return this._seeMask == 1 << (int)trait;
	}

	// Token: 0x17000911 RID: 2321
	// (get) Token: 0x060026C7 RID: 9927 RVA: 0x0008CFF4 File Offset: 0x0008B1F4
	// (set) Token: 0x060026C8 RID: 9928 RVA: 0x0008CFFC File Offset: 0x0008B1FC
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

	// Token: 0x060026C9 RID: 9929 RVA: 0x0008D024 File Offset: 0x0008B224
	protected void Reset()
	{
		base.Reset();
		global::VisReactor component = base.GetComponent<global::VisReactor>();
		if (component)
		{
			this.reactor = component;
			this.reactor.__visNode = this;
		}
	}

	// Token: 0x060026CA RID: 9930 RVA: 0x0008D05C File Offset: 0x0008B25C
	private void Register()
	{
		if (!this.awake || this.active)
		{
			return;
		}
		if (global::VisManager.guardedUpdate)
		{
			throw new InvalidOperationException("DO NOT INSTANTIATE WHILE VisibilityManager.isUpdatingVisibility!!");
		}
		if (!global::VisNode.manager)
		{
			global::VisNode.manager = new GameObject("__Vis", new Type[]
			{
				typeof(global::VisManager)
			}).GetComponent<global::VisManager>();
		}
		if (!this.dataConstructed)
		{
			this.sight.list = new global::ODBSet<global::VisNode>();
			this.sight.last = new global::ODBSet<global::VisNode>();
			this.spect.list = new global::ODBSet<global::VisNode>();
			this.spect.last = new global::ODBSet<global::VisNode>();
			this.enter = new global::ODBSet<global::VisNode>();
			this.exit = new global::ODBSet<global::VisNode>();
			this.cleanList = new List<global::VisNode>();
			this.dataConstructed = true;
		}
		else if (!global::VisNode.recentlyDisabled.Remove(this))
		{
			global::VisNode.disabledLastStep.Remove(this);
		}
		this.item = global::VisNode.db.Register(this);
		this.active = (this.item == this);
	}

	// Token: 0x060026CB RID: 9931 RVA: 0x0008D184 File Offset: 0x0008B384
	private void Unregister()
	{
		if (this.active)
		{
			if (global::VisManager.guardedUpdate)
			{
				throw new InvalidOperationException("DO NOT OR DISABLE DESTROY WHILE VisibilityManager.isUpdatingVisibility!!");
			}
			global::VisNode.db.Unregister(ref this.item);
			this.active = (this.item == this);
		}
	}

	// Token: 0x060026CC RID: 9932 RVA: 0x0008D1D4 File Offset: 0x0008B3D4
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
		this.statusHandler = (this.idMain as global::IVisHandler);
		this.hasStatusHandler = (this.statusHandler != null);
		if (this._class)
		{
			this._handle = this._class.handle;
		}
	}

	// Token: 0x060026CD RID: 9933 RVA: 0x0008D294 File Offset: 0x0008B494
	private void OnDestroy()
	{
		if (global::VisManager.guardedUpdate)
		{
			Debug.LogError("DESTROYING IN GUARDED UPDATE! " + base.name, this);
		}
		this.Unregister();
		global::VisNode.RemoveNow(this);
	}

	// Token: 0x060026CE RID: 9934 RVA: 0x0008D2D0 File Offset: 0x0008B4D0
	private void OnEnable()
	{
		if (this.awake)
		{
			this.Register();
		}
	}

	// Token: 0x060026CF RID: 9935 RVA: 0x0008D2E4 File Offset: 0x0008B4E4
	private void OnDisable()
	{
		if (this.awake)
		{
			bool flag = this.active;
			this.Unregister();
			if (flag && !this.active)
			{
				global::VisNode.recentlyDisabled.Add(this);
			}
		}
	}

	// Token: 0x060026D0 RID: 9936 RVA: 0x0008D328 File Offset: 0x0008B528
	private static void ResolveSee()
	{
		if (global::VisNode.operandA.sight.list.Add(global::VisNode.operandB))
		{
			global::VisNode visNode = global::VisNode.operandB;
			visNode.spect.add = (visNode.spect.add | global::VisNode.operandB.spect.list.Add(global::VisNode.operandA));
			global::VisNode.operandA.sight.add = true;
			global::VisNode.operandA.enter.Add(global::VisNode.operandB);
		}
	}

	// Token: 0x060026D1 RID: 9937 RVA: 0x0008D3A8 File Offset: 0x0008B5A8
	private static void ResolveHide()
	{
		if (global::VisNode.operandA.sight.list.Remove(global::VisNode.operandB))
		{
			global::VisNode visNode = global::VisNode.operandB;
			visNode.spect.rem = (visNode.spect.rem | global::VisNode.operandB.spect.list.Remove(global::VisNode.operandA));
			global::VisNode.operandA.exit.Add(global::VisNode.operandB);
			global::VisNode.operandB.cleanList.Add(global::VisNode.operandA);
		}
	}

	// Token: 0x060026D2 RID: 9938 RVA: 0x0008D42C File Offset: 0x0008B62C
	private static void RemoveLinkNow(global::VisNode node, global::VisNode didSee)
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

	// Token: 0x060026D3 RID: 9939 RVA: 0x0008D508 File Offset: 0x0008B708
	internal static void RemoveNow(global::VisNode node)
	{
		if (!node.dataConstructed)
		{
			return;
		}
		if (!global::VisNode.recentlyDisabled.Remove(node))
		{
			global::VisNode.disabledLastStep.Remove(node);
		}
		for (int i = 0; i < node.cleanList.Count; i++)
		{
			node.cleanList[i].exit.Remove(node);
		}
		global::ODBForwardEnumerator<global::VisNode> enumerator = node.exit.GetEnumerator();
		while (enumerator.MoveNext())
		{
			global::VisNode current = enumerator.Current;
			current.cleanList.Remove(node);
		}
		enumerator.Dispose();
		node.cleanList.Clear();
		node.cleanList.AddRange(node.sight.list);
		for (int i = 0; i < node.cleanList.Count; i++)
		{
			global::VisNode.RemoveLinkNow(node, node.cleanList[i]);
		}
		node.cleanList.Clear();
		node.cleanList.AddRange(node.spect.list);
		for (int i = 0; i < node.cleanList.Count; i++)
		{
			global::VisNode.RemoveLinkNow(node.cleanList[i], node);
		}
		node.cleanList.Clear();
	}

	// Token: 0x060026D4 RID: 9940 RVA: 0x0008D654 File Offset: 0x0008B854
	private static void Copy(global::ODBSet<global::VisNode> src, global::ODBSet<global::VisNode> dst)
	{
		dst.Clear();
		dst.UnionWith(src);
	}

	// Token: 0x060026D5 RID: 9941 RVA: 0x0008D664 File Offset: 0x0008B864
	private static void Transfer(global::ODBSet<global::VisNode> src, global::ODBSet<global::VisNode> dst, bool addAny, bool remAny)
	{
		if (addAny)
		{
			if (remAny)
			{
				global::VisNode.Copy(src, dst);
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

	// Token: 0x060026D6 RID: 9942 RVA: 0x0008D698 File Offset: 0x0008B898
	private void Stamp()
	{
		this._stamp.Collect(this._transform);
		global::VisNode.Transfer(this.sight.list, this.sight.last, this.sight.add, this.sight.rem);
		global::VisNode.Transfer(this.spect.list, this.spect.last, this.spect.add, this.spect.rem);
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

	// Token: 0x060026D7 RID: 9943 RVA: 0x0008D810 File Offset: 0x0008BA10
	internal static void Stage1(global::VisNode self)
	{
		self.Stamp();
	}

	// Token: 0x060026D8 RID: 9944 RVA: 0x0008D818 File Offset: 0x0008BA18
	private static bool LogicSight()
	{
		if (!global::VisNode.operandB.active)
		{
			return false;
		}
		global::VisNode.bX = global::VisNode.operandB._stamp.position.x;
		global::VisNode.bY = global::VisNode.operandB._stamp.position.y;
		global::VisNode.bZ = global::VisNode.operandB._stamp.position.z;
		global::VisNode.planeDot = global::VisNode.bX * global::VisNode.fX + global::VisNode.bY * global::VisNode.fY + global::VisNode.bZ * global::VisNode.fZ;
		if (global::VisNode.planeDot < global::VisNode.fW || global::VisNode.planeDot > global::VisNode.PLANEDOTSIGHT)
		{
			return false;
		}
		global::VisNode.dX = global::VisNode.bX - global::VisNode.pX;
		global::VisNode.dY = global::VisNode.bY - global::VisNode.pY;
		global::VisNode.dZ = global::VisNode.bZ - global::VisNode.pZ;
		global::VisNode.dV2 = global::VisNode.dX * global::VisNode.dX + global::VisNode.dY * global::VisNode.dY + global::VisNode.dZ * global::VisNode.dZ;
		if (global::VisNode.dV2 > global::VisNode.SIGHT2)
		{
			return false;
		}
		if (global::VisNode.dV2 < 4.203895E-45f)
		{
			return global::VisNode.FALLBACK_TOO_CLOSE;
		}
		global::VisNode.dV = Mathf.Sqrt(global::VisNode.dV2);
		global::VisNode.nX = global::VisNode.dX / global::VisNode.dV;
		global::VisNode.nY = global::VisNode.dY / global::VisNode.dV;
		global::VisNode.nZ = global::VisNode.dZ / global::VisNode.dV;
		global::VisNode.dot = global::VisNode.fX * global::VisNode.nX + global::VisNode.fY * global::VisNode.nY + global::VisNode.fZ * global::VisNode.nZ;
		return global::VisNode.DOT < global::VisNode.dot;
	}

	// Token: 0x060026D9 RID: 9945 RVA: 0x0008D9BC File Offset: 0x0008BBBC
	private static void UpdateVis(global::ODBSibling<global::VisNode> first_sib)
	{
		global::VisNode.FALLBACK_TOO_CLOSE = false;
		global::ODBSibling<global::VisNode> odbsibling = first_sib;
		do
		{
			global::VisNode.operandA = odbsibling.item.self;
			odbsibling = odbsibling.item.n;
			if (global::VisNode.operandA._sightCurrentMask == 0)
			{
				if (global::VisNode.operandA.sight.any)
				{
					global::ODBSibling<global::VisNode> odbsibling2 = global::VisNode.operandA.sight.last.first;
					do
					{
						global::VisNode.operandB = odbsibling2.item.self;
						odbsibling2 = odbsibling2.item.n;
						global::VisNode.ResolveHide();
					}
					while (odbsibling2.has);
					global::VisNode.operandB = null;
				}
			}
			else
			{
				global::VisNode.pX = global::VisNode.operandA._stamp.position.x;
				global::VisNode.pY = global::VisNode.operandA._stamp.position.y;
				global::VisNode.pZ = global::VisNode.operandA._stamp.position.z;
				global::VisNode.fX = global::VisNode.operandA._stamp.plane.x;
				global::VisNode.fY = global::VisNode.operandA._stamp.plane.y;
				global::VisNode.fZ = global::VisNode.operandA._stamp.plane.z;
				global::VisNode.fW = global::VisNode.operandA._stamp.plane.w;
				global::VisNode.DOT = global::VisNode.operandA.dotArc;
				global::VisNode.SIGHT = global::VisNode.operandA.distance;
				global::VisNode.SIGHT2 = global::VisNode.SIGHT * global::VisNode.SIGHT;
				global::VisNode.PLANEDOTSIGHT = global::VisNode.fW + global::VisNode.SIGHT;
				if (global::VisNode.operandA.sight.any)
				{
					global::VisNode.FALLBACK_TOO_CLOSE = true;
					global::ODBSibling<global::VisNode> odbsibling3 = global::VisNode.operandA.sight.last.first;
					if (global::VisNode.operandA.histSight.changed)
					{
						do
						{
							global::VisNode.operandB = odbsibling3.item.self;
							odbsibling3 = odbsibling3.item.n;
							if (!global::VisNode.operandB.active)
							{
								global::VisNode.ResolveHide();
							}
							else
							{
								global::VisNode.operandB.__skipOnce_ = true;
								global::VisNode.temp_bTraits = global::VisNode.operandB._traitMask;
								if ((global::VisNode.temp_bTraits & global::VisNode.operandA._sightCurrentMask) == 0 || !global::VisNode.LogicSight())
								{
									global::VisNode.ResolveHide();
								}
								else
								{
									global::VisNode.operandA._seeMask |= global::VisNode.temp_bTraits;
								}
							}
						}
						while (odbsibling3.has);
					}
					else
					{
						global::VisNode.operandB = odbsibling3.item.self;
						do
						{
							global::VisNode.operandB = odbsibling3.item.self;
							odbsibling3 = odbsibling3.item.n;
							if (!global::VisNode.operandB.active)
							{
								global::VisNode.ResolveHide();
							}
							else
							{
								global::VisNode.operandB.__skipOnce_ = true;
								global::VisNode.temp_bTraits = global::VisNode.operandB._traitMask;
								if (global::VisNode.operandB.histTrait.changed)
								{
									if ((global::VisNode.temp_bTraits & global::VisNode.operandA._sightCurrentMask) == 0 || !global::VisNode.LogicSight())
									{
										global::VisNode.ResolveHide();
										goto IL_342;
									}
									global::VisNode.operandA.anySeenTraitChanges = true;
								}
								else if (!global::VisNode.LogicSight())
								{
									global::VisNode.ResolveHide();
									goto IL_342;
								}
								global::VisNode.operandA._seeMask |= global::VisNode.temp_bTraits;
							}
							IL_342:;
						}
						while (odbsibling3.has);
					}
					global::VisNode.FALLBACK_TOO_CLOSE = false;
				}
				global::VisNode.operandA.__skipOnce_ = true;
				global::ODBSibling<global::VisNode> odbsibling4 = first_sib;
				do
				{
					global::VisNode.operandB = odbsibling4.item.self;
					odbsibling4 = odbsibling4.item.n;
					if (global::VisNode.operandB.__skipOnce_)
					{
						global::VisNode.operandB.__skipOnce_ = false;
					}
					else
					{
						global::VisNode.temp_bTraits = global::VisNode.operandB._traitMask;
						if ((global::VisNode.temp_bTraits & global::VisNode.operandA._sightCurrentMask) != 0 && global::VisNode.LogicSight())
						{
							global::VisNode.ResolveSee();
							global::VisNode.operandA._seeMask |= global::VisNode.temp_bTraits;
						}
					}
				}
				while (odbsibling4.has);
				global::VisNode.operandB = null;
			}
		}
		while (odbsibling.has);
		global::VisNode.operandA = null;
	}

	// Token: 0x060026DA RID: 9946 RVA: 0x0008DDD4 File Offset: 0x0008BFD4
	private static void ClearVis(global::ODBSibling<global::VisNode> iter)
	{
		do
		{
			global::VisNode.operandA = iter.item.self;
			iter = iter.item.n;
			if (global::VisNode.operandA.sight.any)
			{
				global::ODBSibling<global::VisNode> odbsibling = global::VisNode.operandA.sight.last.first;
				do
				{
					global::VisNode.operandB = odbsibling.item.self;
					odbsibling = odbsibling.item.n;
					global::VisNode.ResolveHide();
				}
				while (odbsibling.has);
				global::VisNode.operandB = null;
			}
		}
		while (iter.has);
		global::VisNode.operandA = null;
	}

	// Token: 0x060026DB RID: 9947 RVA: 0x0008DE70 File Offset: 0x0008C070
	private static void RunStamp(global::ODBSibling<global::VisNode> sib)
	{
		do
		{
			global::VisNode.operandA = sib.item.self;
			sib = sib.item.n;
			global::VisNode.operandA.Stamp();
		}
		while (sib.has);
		global::VisNode.operandA = null;
	}

	// Token: 0x060026DC RID: 9948 RVA: 0x0008DEB0 File Offset: 0x0008C0B0
	private static void RunStat(global::ODBSibling<global::VisNode> sib)
	{
		do
		{
			global::VisNode.operandA = sib.item.self;
			sib = sib.item.n;
			global::VisNode.operandA.StatUpdate();
		}
		while (sib.has);
		global::VisNode.operandA = null;
	}

	// Token: 0x060026DD RID: 9949 RVA: 0x0008DEF0 File Offset: 0x0008C0F0
	private static void RunHiddenCalls(global::ODBSibling<global::VisNode> sib)
	{
		do
		{
			global::VisNode.operandA = sib.item.self;
			sib = sib.item.n;
			if (global::VisNode.operandA.sight.rem)
			{
				global::ODBSibling<global::VisNode> odbsibling = global::VisNode.operandA.exit.first;
				do
				{
					global::VisNode.operandB = odbsibling.item.self;
					odbsibling = odbsibling.item.n;
					global::VisNode.operandB._CB_OnHiddenFrom_(global::VisNode.operandA);
				}
				while (odbsibling.has);
				global::VisNode.operandB = null;
			}
		}
		while (sib.has);
		global::VisNode.operandA = null;
	}

	// Token: 0x060026DE RID: 9950 RVA: 0x0008DF94 File Offset: 0x0008C194
	private static void RunVoidSeenHiddenCalls(global::ODBSibling<global::VisNode> sib)
	{
		do
		{
			global::VisNode.operandA = sib.item.self;
			sib = sib.item.p;
			if (global::VisNode.operandA.spect.had)
			{
				if (!global::VisNode.operandA.spect.any)
				{
					global::VisNode.operandA._CB_OnHidden_();
					global::VisNode.operandA.spect.had = false;
				}
			}
			else if (global::VisNode.operandA.spect.any)
			{
				global::VisNode.operandA._CB_OnSeen_();
				global::VisNode.operandA.spect.had = true;
			}
			global::VisNode.operandA.sight.had = global::VisNode.operandA.sight.any;
		}
		while (sib.has);
		global::VisNode.operandA = null;
	}

	// Token: 0x060026DF RID: 9951 RVA: 0x0008E068 File Offset: 0x0008C268
	private static void RunSeenCalls(global::ODBSibling<global::VisNode> sib)
	{
		do
		{
			global::VisNode.operandA = sib.item.self;
			sib = sib.item.n;
			if (global::VisNode.operandA.sight.add)
			{
				global::ODBSibling<global::VisNode> odbsibling = global::VisNode.operandA.enter.last;
				do
				{
					global::VisNode.operandB = odbsibling.item.self;
					odbsibling = odbsibling.item.p;
					global::VisNode.operandB._CB_OnSeenBy_(global::VisNode.operandA);
				}
				while (odbsibling.has);
				global::VisNode.operandB = null;
			}
		}
		while (sib.has);
		global::VisNode.operandA = null;
	}

	// Token: 0x060026E0 RID: 9952 RVA: 0x0008E10C File Offset: 0x0008C30C
	private static void RunQueries(global::ODBSibling<global::VisNode> sib)
	{
		do
		{
			global::VisNode.operandA = sib.item.self;
			sib = sib.item.p;
			if (global::VisNode.operandA.reactor)
			{
				global::VisNode.operandA.CheckReactions();
			}
			global::VisNode.operandA.CheckQueries();
		}
		while (sib.has);
		global::VisNode.operandA = null;
	}

	// Token: 0x060026E1 RID: 9953 RVA: 0x0008E174 File Offset: 0x0008C374
	public static void Process()
	{
		if (global::VisNode.db.any)
		{
			if (global::VisNode.recentlyDisabled.any)
			{
				global::VisNode.RunStamp(global::VisNode.db.first);
				global::VisNode.RunStamp(global::VisNode.recentlyDisabled.first);
				global::VisNode.ClearVis(global::VisNode.recentlyDisabled.first);
				global::VisNode.UpdateVis(global::VisNode.db.first);
				global::VisNode.RunStat(global::VisNode.recentlyDisabled.first);
				global::VisNode.RunStat(global::VisNode.db.first);
				global::VisNode.RunHiddenCalls(global::VisNode.recentlyDisabled.first);
				global::VisNode.RunHiddenCalls(global::VisNode.db.first);
				global::VisNode.RunVoidSeenHiddenCalls(global::VisNode.recentlyDisabled.last);
				global::VisNode.RunVoidSeenHiddenCalls(global::VisNode.db.last);
				global::VisNode.RunSeenCalls(global::VisNode.recentlyDisabled.first);
				global::VisNode.RunSeenCalls(global::VisNode.db.first);
				global::VisNode.RunQueries(global::VisNode.recentlyDisabled.last);
				global::VisNode.RunQueries(global::VisNode.db.last);
				global::VisNode.Finally();
				global::VisNode.SwapDisabled();
			}
			else
			{
				global::VisNode.RunStamp(global::VisNode.db.first);
				global::VisNode.UpdateVis(global::VisNode.db.first);
				global::VisNode.RunStat(global::VisNode.db.first);
				global::VisNode.RunHiddenCalls(global::VisNode.db.first);
				global::VisNode.RunVoidSeenHiddenCalls(global::VisNode.db.last);
				global::VisNode.RunSeenCalls(global::VisNode.db.first);
				global::VisNode.RunQueries(global::VisNode.db.last);
				global::VisNode.Finally();
			}
		}
		else if (global::VisNode.recentlyDisabled.any)
		{
			global::VisNode.RunStamp(global::VisNode.recentlyDisabled.first);
			global::VisNode.ClearVis(global::VisNode.recentlyDisabled.first);
			global::VisNode.RunStat(global::VisNode.recentlyDisabled.first);
			global::VisNode.RunHiddenCalls(global::VisNode.recentlyDisabled.first);
			global::VisNode.RunVoidSeenHiddenCalls(global::VisNode.recentlyDisabled.last);
			global::VisNode.RunSeenCalls(global::VisNode.recentlyDisabled.first);
			global::VisNode.RunQueries(global::VisNode.recentlyDisabled.last);
			global::VisNode.Finally();
			global::VisNode.SwapDisabled();
		}
	}

	// Token: 0x060026E2 RID: 9954 RVA: 0x0008E378 File Offset: 0x0008C578
	private void StatUpdate()
	{
		this.sight.count = this.sight.list.count;
		this.sight.any = (this.sight.count > 0);
		this.spect.count = this.spect.list.count;
		this.spect.any = (this.spect.count > 0);
	}

	// Token: 0x060026E3 RID: 9955 RVA: 0x0008E3F0 File Offset: 0x0008C5F0
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

	// Token: 0x060026E4 RID: 9956 RVA: 0x0008E468 File Offset: 0x0008C668
	private void DoQueryRecurse(int i, global::VisNode other)
	{
		if (i >= this._handle.Length)
		{
			return;
		}
		global::VisQuery.Instance instance = this._handle[i];
		switch (instance.TryAdd(this, other))
		{
		case global::VisQuery.TryResult.Enter:
			this.DoQueryRecurse(i + 1, other);
			instance.ExecuteEnter(this, other);
			return;
		case global::VisQuery.TryResult.Exit:
			instance.ExecuteExit(this, other);
			this.DoQueryRecurse(i + 1, other);
			return;
		}
		this.DoQueryRecurse(i + 1, other);
	}

	// Token: 0x060026E5 RID: 9957 RVA: 0x0008E4F4 File Offset: 0x0008C6F4
	private void DoQueryRemAdd(global::ODBSibling<global::VisNode> sib)
	{
		if (this._handle.valid && this._handle.Length > 0)
		{
			while (sib.has)
			{
				global::VisNode self = sib.item.self;
				sib = sib.item.n;
				this.DoQueryRecurse(0, self);
			}
		}
	}

	// Token: 0x060026E6 RID: 9958 RVA: 0x0008E558 File Offset: 0x0008C758
	private void DoQueryRem(global::ODBSibling<global::VisNode> sib)
	{
		int length;
		if (this._handle.valid && (length = this._handle.Length) > 0)
		{
			while (sib.has)
			{
				global::VisNode self = sib.item.self;
				sib = sib.item.n;
				for (int i = 0; i < length; i++)
				{
					global::VisQuery.Instance instance = this._handle[i];
					if (instance.TryRemove(this, self) == global::VisQuery.TryResult.Exit)
					{
						instance.ExecuteExit(this, self);
					}
				}
			}
		}
	}

	// Token: 0x060026E7 RID: 9959 RVA: 0x0008E5EC File Offset: 0x0008C7EC
	private void _REACTOR_SEE_REMOVE(global::ODBSibling<global::VisNode> sib)
	{
		while (sib.has)
		{
			global::VisNode self = sib.item.self;
			sib = sib.item.n;
			this.reactor.SEE_REMOVE(self);
		}
	}

	// Token: 0x060026E8 RID: 9960 RVA: 0x0008E634 File Offset: 0x0008C834
	private void _REACTOR_SEE_ADD(global::ODBSibling<global::VisNode> sib)
	{
		while (sib.has)
		{
			global::VisNode self = sib.item.self;
			sib = sib.item.n;
			this.reactor.SEE_ADD(self);
		}
	}

	// Token: 0x060026E9 RID: 9961 RVA: 0x0008E67C File Offset: 0x0008C87C
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

	// Token: 0x060026EA RID: 9962 RVA: 0x0008E714 File Offset: 0x0008C914
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

	// Token: 0x060026EB RID: 9963 RVA: 0x0008E7BC File Offset: 0x0008C9BC
	private static void Finally()
	{
		if (global::VisNode.disabledLastStep.any)
		{
			global::VisNode.RunStamp(global::VisNode.disabledLastStep.first);
			global::VisNode.disabledLastStep.Clear();
		}
	}

	// Token: 0x060026EC RID: 9964 RVA: 0x0008E7F4 File Offset: 0x0008C9F4
	private static void SwapDisabled()
	{
		global::ODBSet<global::VisNode> odbset = global::VisNode.disabledLastStep;
		global::VisNode.disabledLastStep = global::VisNode.recentlyDisabled;
		global::VisNode.recentlyDisabled = odbset;
	}

	// Token: 0x060026ED RID: 9965 RVA: 0x0008E818 File Offset: 0x0008CA18
	protected void _CB_OnSeen_()
	{
		if (this.reactor)
		{
			this.reactor.SPECTATED_ENTER();
		}
	}

	// Token: 0x060026EE RID: 9966 RVA: 0x0008E838 File Offset: 0x0008CA38
	protected void _CB_OnHidden_()
	{
		if (this.reactor)
		{
			this.reactor.SPECTATED_EXIT();
		}
	}

	// Token: 0x060026EF RID: 9967 RVA: 0x0008E858 File Offset: 0x0008CA58
	protected void _CB_OnSeenBy_(global::VisNode spectator)
	{
		if (this.reactor)
		{
			this.reactor.SPECTATOR_ADD(spectator);
		}
	}

	// Token: 0x060026F0 RID: 9968 RVA: 0x0008E878 File Offset: 0x0008CA78
	protected void _CB_OnHiddenFrom_(global::VisNode spectator)
	{
		if (this.reactor)
		{
			this.reactor.SPECTATOR_REMOVE(spectator);
		}
	}

	// Token: 0x060026F1 RID: 9969 RVA: 0x0008E898 File Offset: 0x0008CA98
	public bool CanSee(global::VisNode other)
	{
		return global::VisNode.CanSee(this, other);
	}

	// Token: 0x060026F2 RID: 9970 RVA: 0x0008E8A4 File Offset: 0x0008CAA4
	public bool IsSeenBy(global::VisNode other)
	{
		return global::VisNode.IsSeenBy(this, other);
	}

	// Token: 0x060026F3 RID: 9971 RVA: 0x0008E8B0 File Offset: 0x0008CAB0
	public bool CanSeeUnobstructed(global::VisNode other)
	{
		return this.CanSee(other) && this.Unobstructed(other);
	}

	// Token: 0x060026F4 RID: 9972 RVA: 0x0008E8C8 File Offset: 0x0008CAC8
	public bool Unobstructed(global::VisNode other)
	{
		return Physics.Linecast(this._stamp.position, other._stamp.position, 1);
	}

	// Token: 0x060026F5 RID: 9973 RVA: 0x0008E8E8 File Offset: 0x0008CAE8
	public static bool CanSee(global::VisNode instigator, global::VisNode target)
	{
		return instigator == target || instigator._CanSee(target);
	}

	// Token: 0x060026F6 RID: 9974 RVA: 0x0008E900 File Offset: 0x0008CB00
	public static bool IsSeenBy(global::VisNode instigator, global::VisNode target)
	{
		return instigator == target || instigator._IsSeenBy(target);
	}

	// Token: 0x060026F7 RID: 9975 RVA: 0x0008E918 File Offset: 0x0008CB18
	public static bool AreAware(global::VisNode instigator, global::VisNode target)
	{
		return global::VisNode.CanSee(instigator, target) && instigator._IsSeenBy(target);
	}

	// Token: 0x060026F8 RID: 9976 RVA: 0x0008E930 File Offset: 0x0008CB30
	public static bool IsStealthly(global::VisNode instigator, global::VisNode target)
	{
		return global::VisNode.CanSee(instigator, target) && !instigator._IsSeenBy(target);
	}

	// Token: 0x060026F9 RID: 9977 RVA: 0x0008E94C File Offset: 0x0008CB4C
	public static bool AreOblivious(global::VisNode instigator, global::VisNode target)
	{
		return !global::VisNode.CanSee(instigator, target) && !instigator._IsSeenBy(target);
	}

	// Token: 0x060026FA RID: 9978 RVA: 0x0008E968 File Offset: 0x0008CB68
	public static global::Vis.Comparison Compare(global::VisNode self, global::VisNode target)
	{
		if (self == target)
		{
			return global::Vis.Comparison.IsSelf;
		}
		if (self._CanSee(target))
		{
			if (self._IsSeenBy(target))
			{
				return global::Vis.Comparison.Contact;
			}
			return global::Vis.Comparison.Stealthy;
		}
		else
		{
			if (self._IsSeenBy(target))
			{
				return global::Vis.Comparison.Prey;
			}
			return global::Vis.Comparison.Oblivious;
		}
	}

	// Token: 0x060026FB RID: 9979 RVA: 0x0008E9B4 File Offset: 0x0008CBB4
	private bool _CanSee(global::VisNode other)
	{
		return (other.spect.count >= this.sight.count) ? this.sight.list.Contains(other) : other.spect.list.Contains(this);
	}

	// Token: 0x060026FC RID: 9980 RVA: 0x0008EA04 File Offset: 0x0008CC04
	private bool _IsSeenBy(global::VisNode other)
	{
		return (other.sight.count >= this.spect.count) ? this.spect.list.Contains(other) : other.sight.list.Contains(this);
	}

	// Token: 0x060026FD RID: 9981 RVA: 0x0008EA54 File Offset: 0x0008CC54
	[Conditional("UNITY_EDITOR")]
	private static void _VALIDATE(global::VisNode vis)
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

	// Token: 0x060026FE RID: 9982 RVA: 0x0008EBA8 File Offset: 0x0008CDA8
	private static void RouteMessageHSet(global::ODBSet<global::VisNode> list, string msg, object arg)
	{
		if (list.any)
		{
			global::ODBSibling<global::VisNode> odbsibling = list.first;
			do
			{
				global::VisNode self = odbsibling.item.self;
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

	// Token: 0x060026FF RID: 9983 RVA: 0x0008EC24 File Offset: 0x0008CE24
	private static void RouteMessageList(global::RecycleList<global::VisNode> list, string msg)
	{
		global::VisNode.RouteMessageList(list, msg, null);
	}

	// Token: 0x06002700 RID: 9984 RVA: 0x0008EC30 File Offset: 0x0008CE30
	private static void RouteMessageList(global::RecycleList<global::VisNode> list, string msg, object arg)
	{
		using (global::RecycleListIter<global::VisNode> recycleListIter = list.MakeIter())
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

	// Token: 0x06002701 RID: 9985 RVA: 0x0008ECC0 File Offset: 0x0008CEC0
	private static void RouteMessageOp(global::HSetOper op, global::ODBSet<global::VisNode> a, IEnumerable<global::VisNode> b, string msg, object arg)
	{
		global::RecycleList<global::VisNode> recycleList = a.OperList(op, b);
		global::VisNode.RouteMessageList(recycleList, msg, arg);
		recycleList.Dispose();
	}

	// Token: 0x06002702 RID: 9986 RVA: 0x0008ECE8 File Offset: 0x0008CEE8
	private static void RouteMessageOpUnionFirst(global::HSetOper op, global::ODBSet<global::VisNode> a, global::ODBSet<global::VisNode> aa, IEnumerable<global::VisNode> b, string msg, object arg)
	{
		global::ODBSet<global::VisNode> odbset = new global::ODBSet<global::VisNode>(a);
		odbset.UnionWith(aa);
		global::VisNode.RouteMessageOp(op, odbset, b, msg, arg);
	}

	// Token: 0x06002703 RID: 9987 RVA: 0x0008ED10 File Offset: 0x0008CF10
	private static void RouteMessageOpUnionFirst(global::HSetOper op, global::ODBSet<global::VisNode> a, global::ODBSet<global::VisNode> aa, IEnumerable<global::VisNode> b, string msg)
	{
		global::VisNode.RouteMessageOpUnionFirst(op, a, aa, b, msg, null);
	}

	// Token: 0x06002704 RID: 9988 RVA: 0x0008ED20 File Offset: 0x0008CF20
	private static void RouteMessageOp(global::HSetOper op, global::ODBSet<global::VisNode> a, IEnumerable<global::VisNode> b, string msg)
	{
		global::VisNode.RouteMessageOp(op, a, b, msg, null);
	}

	// Token: 0x06002705 RID: 9989 RVA: 0x0008ED2C File Offset: 0x0008CF2C
	private static void DoGestureMessage(global::VisNode instigator, string message, object arg)
	{
		global::VisNode.RouteMessageHSet(instigator.spect.list, message, arg);
	}

	// Token: 0x06002706 RID: 9990 RVA: 0x0008ED40 File Offset: 0x0008CF40
	public static bool GestureMessage(global::VisNode instigator, string message, object arg)
	{
		if (!instigator || !instigator.enabled)
		{
			return false;
		}
		global::VisNode.DoGestureMessage(instigator, message, arg);
		return true;
	}

	// Token: 0x06002707 RID: 9991 RVA: 0x0008ED64 File Offset: 0x0008CF64
	private static void DoAttentionMessage(global::VisNode instigator, string message, object arg)
	{
		global::VisNode.RouteMessageHSet(instigator.sight.list, message, arg);
	}

	// Token: 0x06002708 RID: 9992 RVA: 0x0008ED78 File Offset: 0x0008CF78
	public static bool AttentionMessage(global::VisNode instigator, string message, object arg)
	{
		return false;
	}

	// Token: 0x06002709 RID: 9993 RVA: 0x0008ED7C File Offset: 0x0008CF7C
	private static void DoStealthMessage(global::VisNode instigator, string message, object arg)
	{
		global::VisNode.RouteMessageOp(global::HSetOper.Except, instigator.sight.list, instigator.spect.list, message, arg);
	}

	// Token: 0x0600270A RID: 9994 RVA: 0x0008ED9C File Offset: 0x0008CF9C
	public static bool StealthMessage(global::VisNode instigator, string message, object arg)
	{
		return false;
	}

	// Token: 0x0600270B RID: 9995 RVA: 0x0008EDA0 File Offset: 0x0008CFA0
	private static void DoPreyMessage(global::VisNode instigator, string message, object arg)
	{
		global::VisNode.RouteMessageOp(global::HSetOper.Except, instigator.spect.list, instigator.sight.list, message, arg);
	}

	// Token: 0x0600270C RID: 9996 RVA: 0x0008EDC0 File Offset: 0x0008CFC0
	public static bool PreyMessage(global::VisNode instigator, string message, object arg)
	{
		return false;
	}

	// Token: 0x0600270D RID: 9997 RVA: 0x0008EDC4 File Offset: 0x0008CFC4
	private static void DoContactMessage(global::VisNode instigator, string message, object arg)
	{
		if (instigator.spect.count < instigator.sight.count)
		{
			global::VisNode.RouteMessageOp(global::HSetOper.Intersect, instigator.spect.list, instigator.sight.list, message, arg);
		}
		else
		{
			global::VisNode.RouteMessageOp(global::HSetOper.Intersect, instigator.sight.list, instigator.spect.list, message, arg);
		}
	}

	// Token: 0x0600270E RID: 9998 RVA: 0x0008EE30 File Offset: 0x0008D030
	public static bool ContactMessage(global::VisNode instigator, string message, object arg)
	{
		return false;
	}

	// Token: 0x0600270F RID: 9999 RVA: 0x0008EE34 File Offset: 0x0008D034
	private static void DoObliviousMessage(global::VisNode instigator, string message, object arg)
	{
		if (instigator.spect.count < instigator.sight.count)
		{
			global::VisNode.RouteMessageOpUnionFirst(global::HSetOper.SymmetricExcept, instigator.spect.list, instigator.sight.list, global::VisNode.db, message, arg);
		}
		else
		{
			global::VisNode.RouteMessageOpUnionFirst(global::HSetOper.SymmetricExcept, instigator.sight.list, instigator.spect.list, global::VisNode.db, message, arg);
		}
	}

	// Token: 0x06002710 RID: 10000 RVA: 0x0008EEA8 File Offset: 0x0008D0A8
	public static bool ObliviousMessage(global::VisNode instigator, string message, object arg)
	{
		if (!instigator || !instigator.enabled)
		{
			return false;
		}
		global::VisNode.DoObliviousMessage(instigator, message, arg);
		return true;
	}

	// Token: 0x06002711 RID: 10001 RVA: 0x0008EECC File Offset: 0x0008D0CC
	public static void GlobalMessage(string message, object arg)
	{
		using (global::ODBForwardEnumerator<global::VisNode> enumerator = global::VisNode.db.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				enumerator.Current.SendMessage(message, arg, 1);
			}
		}
	}

	// Token: 0x06002712 RID: 10002 RVA: 0x0008EF30 File Offset: 0x0008D130
	public static bool ComparisonMessage(global::VisNode instigator, global::Vis.Comparison comparison, string message, object arg)
	{
		switch (comparison)
		{
		case global::Vis.Comparison.Prey:
			return global::VisNode.PreyMessage(instigator, message, arg);
		default:
			if (comparison == global::Vis.Comparison.Oblivious)
			{
				return global::VisNode.ObliviousMessage(instigator, message, arg);
			}
			if (comparison != global::Vis.Comparison.Stealthy)
			{
				throw new ArgumentException(" do not know what to do with " + comparison, "comparison");
			}
			return global::VisNode.StealthMessage(instigator, message, arg);
		case global::Vis.Comparison.IsSelf:
			if (!instigator || !instigator.enabled)
			{
				return false;
			}
			instigator.SendMessage(message, arg, 1);
			return true;
		case global::Vis.Comparison.Contact:
			return global::VisNode.ContactMessage(instigator, message, arg);
		}
	}

	// Token: 0x06002713 RID: 10003 RVA: 0x0008EFD0 File Offset: 0x0008D1D0
	private static void DoAudibleMessage(global::VisNode instigator, Vector3 position, float radius, string message, object arg)
	{
		global::VisNode.Search.Radial.Enumerator nodesInRadius = global::Vis.GetNodesInRadius(position, radius);
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

	// Token: 0x06002714 RID: 10004 RVA: 0x0008F050 File Offset: 0x0008D250
	public static bool AudibleMessage(global::VisNode instigator, Vector3 position, float radius, string message, object arg)
	{
		if (!instigator || instigator.mute || radius <= 0f || !instigator.enabled)
		{
			return false;
		}
		global::VisNode.DoAudibleMessage(instigator, position, radius, message, arg);
		return true;
	}

	// Token: 0x06002715 RID: 10005 RVA: 0x0008F098 File Offset: 0x0008D298
	public static bool AudibleMessage(global::VisNode instigator, float radius, string message, object arg)
	{
		if (!instigator || instigator.mute || radius <= 0f || !instigator.enabled)
		{
			return false;
		}
		global::VisNode.DoAudibleMessage(instigator, instigator._stamp.position, radius, message, arg);
		return true;
	}

	// Token: 0x06002716 RID: 10006 RVA: 0x0008F0E8 File Offset: 0x0008D2E8
	public static bool AudibleMessage(global::VisNode instigator, Vector3 position, float radius, string message)
	{
		if (!instigator || instigator.mute || radius <= 0f || !instigator.enabled)
		{
			return false;
		}
		global::VisNode.DoAudibleMessage(instigator, position, radius, message, null);
		return true;
	}

	// Token: 0x06002717 RID: 10007 RVA: 0x0008F130 File Offset: 0x0008D330
	public static bool AudibleMessage(global::VisNode instigator, float radius, string message)
	{
		if (!instigator || instigator.mute || radius <= 0f || !instigator.enabled)
		{
			return false;
		}
		global::VisNode.DoAudibleMessage(instigator, instigator._stamp.position, radius, message, null);
		return true;
	}

	// Token: 0x06002718 RID: 10008 RVA: 0x0008F180 File Offset: 0x0008D380
	public static bool GestureMessage(global::VisNode instigator, string message)
	{
		return global::VisNode.GestureMessage(instigator, message, null);
	}

	// Token: 0x06002719 RID: 10009 RVA: 0x0008F18C File Offset: 0x0008D38C
	public static bool AttentionMessage(global::VisNode instigator, string message)
	{
		return global::VisNode.AttentionMessage(instigator, message, null);
	}

	// Token: 0x0600271A RID: 10010 RVA: 0x0008F198 File Offset: 0x0008D398
	public static bool StealthMessage(global::VisNode instigator, string message)
	{
		return global::VisNode.StealthMessage(instigator, message, null);
	}

	// Token: 0x0600271B RID: 10011 RVA: 0x0008F1A4 File Offset: 0x0008D3A4
	public static bool PreyMessage(global::VisNode instigator, string message)
	{
		return global::VisNode.GestureMessage(instigator, message, null);
	}

	// Token: 0x0600271C RID: 10012 RVA: 0x0008F1B0 File Offset: 0x0008D3B0
	public static bool ContactMessage(global::VisNode instigator, string message)
	{
		return global::VisNode.AttentionMessage(instigator, message, null);
	}

	// Token: 0x0600271D RID: 10013 RVA: 0x0008F1BC File Offset: 0x0008D3BC
	public static bool ObliviousMessage(global::VisNode instigator, string message)
	{
		return global::VisNode.StealthMessage(instigator, message, null);
	}

	// Token: 0x0600271E RID: 10014 RVA: 0x0008F1C8 File Offset: 0x0008D3C8
	public static bool ComparisonMessage(global::VisNode instigator, global::Vis.Comparison comparison, string message)
	{
		return global::VisNode.ComparisonMessage(instigator, comparison, message, null);
	}

	// Token: 0x0600271F RID: 10015 RVA: 0x0008F1D4 File Offset: 0x0008D3D4
	public bool GestureMessage(string message, object arg)
	{
		if (!base.enabled)
		{
			return false;
		}
		global::VisNode.DoGestureMessage(this, message, arg);
		return true;
	}

	// Token: 0x06002720 RID: 10016 RVA: 0x0008F1EC File Offset: 0x0008D3EC
	public bool GestureMessage(string message)
	{
		if (!base.enabled)
		{
			return false;
		}
		global::VisNode.DoGestureMessage(this, message, null);
		return true;
	}

	// Token: 0x06002721 RID: 10017 RVA: 0x0008F204 File Offset: 0x0008D404
	public bool AttentionMessage(string message, object arg)
	{
		return false;
	}

	// Token: 0x06002722 RID: 10018 RVA: 0x0008F208 File Offset: 0x0008D408
	public bool AttentionMessage(string message)
	{
		return false;
	}

	// Token: 0x06002723 RID: 10019 RVA: 0x0008F20C File Offset: 0x0008D40C
	public bool StealthMessage(string message, object arg)
	{
		return false;
	}

	// Token: 0x06002724 RID: 10020 RVA: 0x0008F210 File Offset: 0x0008D410
	public bool PreyMessage(string message)
	{
		return false;
	}

	// Token: 0x06002725 RID: 10021 RVA: 0x0008F214 File Offset: 0x0008D414
	public bool ContactMessage(string message, object arg)
	{
		return false;
	}

	// Token: 0x06002726 RID: 10022 RVA: 0x0008F218 File Offset: 0x0008D418
	public bool ContactMessage(string message)
	{
		return false;
	}

	// Token: 0x06002727 RID: 10023 RVA: 0x0008F21C File Offset: 0x0008D41C
	public bool ObliviousMessage(string message, object arg)
	{
		if (!base.enabled)
		{
			return false;
		}
		global::VisNode.ContactMessage(this, message, arg);
		return true;
	}

	// Token: 0x06002728 RID: 10024 RVA: 0x0008F238 File Offset: 0x0008D438
	public bool ObliviousMessage(string message)
	{
		if (!base.enabled)
		{
			return false;
		}
		global::VisNode.ContactMessage(this, message, null);
		return true;
	}

	// Token: 0x06002729 RID: 10025 RVA: 0x0008F254 File Offset: 0x0008D454
	public bool ComparisonMessage(global::Vis.Comparison comparison, string message, object arg)
	{
		return global::VisNode.ComparisonMessage(this, comparison, message, arg);
	}

	// Token: 0x0600272A RID: 10026 RVA: 0x0008F260 File Offset: 0x0008D460
	public bool ComparisonMessage(global::Vis.Comparison comparison, string message)
	{
		return global::VisNode.ComparisonMessage(this, comparison, message, null);
	}

	// Token: 0x0600272B RID: 10027 RVA: 0x0008F26C File Offset: 0x0008D46C
	public bool AudibleMessage(float radius, string message, object arg)
	{
		if (this.mute || !base.enabled || radius <= 0f)
		{
			return false;
		}
		global::VisNode.DoAudibleMessage(this, this._stamp.position, radius, message, arg);
		return true;
	}

	// Token: 0x0600272C RID: 10028 RVA: 0x0008F2B4 File Offset: 0x0008D4B4
	public bool AudibleMessage(float radius, string message)
	{
		if (this.mute || !base.enabled || radius <= 0f)
		{
			return false;
		}
		global::VisNode.DoAudibleMessage(this, this._stamp.position, radius, message, null);
		return true;
	}

	// Token: 0x0600272D RID: 10029 RVA: 0x0008F2FC File Offset: 0x0008D4FC
	public bool AudibleMessage(Vector3 point, float radius, string message, object arg)
	{
		if (this.mute || !base.enabled || radius <= 0f)
		{
			return false;
		}
		global::VisNode.DoAudibleMessage(this, point, radius, message, arg);
		return true;
	}

	// Token: 0x0600272E RID: 10030 RVA: 0x0008F338 File Offset: 0x0008D538
	public bool AudibleMessage(Vector3 point, float radius, string message)
	{
		if (this.mute || !base.enabled || radius <= 0f)
		{
			return false;
		}
		global::VisNode.DoAudibleMessage(this, point, radius, message, null);
		return true;
	}

	// Token: 0x0600272F RID: 10031 RVA: 0x0008F374 File Offset: 0x0008D574
	private void DrawConnections(global::ODBSet<global::VisNode> list)
	{
		if (list != null)
		{
			global::ODBForwardEnumerator<global::VisNode> enumerator = list.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Vector3 position = enumerator.Current._stamp.position;
				Gizmos.DrawLine(this._stamp.position, position);
				Gizmos.DrawWireSphere(position, 0.5f);
			}
			enumerator.Dispose();
		}
	}

	// Token: 0x06002730 RID: 10032 RVA: 0x0008F3D4 File Offset: 0x0008D5D4
	private void OnDrawGizmosSelected()
	{
		global::VisGizmosUtility.ResetMatrixStack();
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
		global::VisGizmosUtility.DrawDotArc(position, transform, this.distance, this.dotArc, this.dotArcBegin);
	}

	// Token: 0x04001229 RID: 4649
	private const int defaultUnobstructedLayers = 1;

	// Token: 0x0400122A RID: 4650
	[SerializeField]
	[PrefetchComponent]
	private global::VisReactor reactor;

	// Token: 0x0400122B RID: 4651
	[SerializeField]
	private float dotArc = 0.75f;

	// Token: 0x0400122C RID: 4652
	[SerializeField]
	private float distance = 10f;

	// Token: 0x0400122D RID: 4653
	[SerializeField]
	private float dotArcBegin;

	// Token: 0x0400122E RID: 4654
	[SerializeField]
	[HideInInspector]
	private int _sightMask = -1;

	// Token: 0x0400122F RID: 4655
	[HideInInspector]
	[SerializeField]
	private int _spectMask = -1;

	// Token: 0x04001230 RID: 4656
	[HideInInspector]
	[SerializeField]
	private int _traitMask = 16777217;

	// Token: 0x04001231 RID: 4657
	[NonSerialized]
	private int _sightCurrentMask;

	// Token: 0x04001232 RID: 4658
	[NonSerialized]
	private int _seeMask;

	// Token: 0x04001233 RID: 4659
	[NonSerialized]
	private bool anySeenTraitChanges;

	// Token: 0x04001234 RID: 4660
	[NonSerialized]
	private bool hasStatusHandler;

	// Token: 0x04001235 RID: 4661
	[NonSerialized]
	private bool __skipOnce_;

	// Token: 0x04001236 RID: 4662
	[NonSerialized]
	private bool awake;

	// Token: 0x04001237 RID: 4663
	[NonSerialized]
	private bool active;

	// Token: 0x04001238 RID: 4664
	[NonSerialized]
	private bool dataConstructed;

	// Token: 0x04001239 RID: 4665
	public bool blind;

	// Token: 0x0400123A RID: 4666
	public bool deaf;

	// Token: 0x0400123B RID: 4667
	public bool mute;

	// Token: 0x0400123C RID: 4668
	[SerializeField]
	private global::VisClass _class;

	// Token: 0x0400123D RID: 4669
	[NonSerialized]
	private global::VisClass.Handle _handle;

	// Token: 0x0400123E RID: 4670
	private long queriesBitMask;

	// Token: 0x0400123F RID: 4671
	private global::IVisHandler statusHandler;

	// Token: 0x04001240 RID: 4672
	[NonSerialized]
	private global::VisNode.TraitHistory histSight;

	// Token: 0x04001241 RID: 4673
	[NonSerialized]
	private global::VisNode.TraitHistory histSpect;

	// Token: 0x04001242 RID: 4674
	[NonSerialized]
	private global::VisNode.TraitHistory histTrait;

	// Token: 0x04001243 RID: 4675
	[NonSerialized]
	private global::VisNode.TraitHistory histSeen;

	// Token: 0x04001244 RID: 4676
	private global::VisNode.VisMem spect;

	// Token: 0x04001245 RID: 4677
	private global::VisNode.VisMem sight;

	// Token: 0x04001246 RID: 4678
	private global::ODBSet<global::VisNode> enter;

	// Token: 0x04001247 RID: 4679
	private global::ODBSet<global::VisNode> exit;

	// Token: 0x04001248 RID: 4680
	internal global::ODBItem<global::VisNode> item;

	// Token: 0x04001249 RID: 4681
	private List<global::VisNode> cleanList;

	// Token: 0x0400124A RID: 4682
	[HideInInspector]
	[NonSerialized]
	private Transform _transform;

	// Token: 0x0400124B RID: 4683
	[NonSerialized]
	private global::Vis.Stamp _stamp;

	// Token: 0x0400124C RID: 4684
	private static global::ObjectDB<global::VisNode> db = new global::ObjectDB<global::VisNode>();

	// Token: 0x0400124D RID: 4685
	private static global::VisManager manager;

	// Token: 0x0400124E RID: 4686
	private static global::ODBSet<global::VisNode> recentlyDisabled = new global::ODBSet<global::VisNode>();

	// Token: 0x0400124F RID: 4687
	private static global::ODBSet<global::VisNode> disabledLastStep = new global::ODBSet<global::VisNode>();

	// Token: 0x04001250 RID: 4688
	private static global::VisNode operandA;

	// Token: 0x04001251 RID: 4689
	private static global::VisNode operandB;

	// Token: 0x04001252 RID: 4690
	private static float pX;

	// Token: 0x04001253 RID: 4691
	private static float pY;

	// Token: 0x04001254 RID: 4692
	private static float pZ;

	// Token: 0x04001255 RID: 4693
	private static float bX;

	// Token: 0x04001256 RID: 4694
	private static float bY;

	// Token: 0x04001257 RID: 4695
	private static float bZ;

	// Token: 0x04001258 RID: 4696
	private static float fX;

	// Token: 0x04001259 RID: 4697
	private static float fY;

	// Token: 0x0400125A RID: 4698
	private static float fZ;

	// Token: 0x0400125B RID: 4699
	private static float fW;

	// Token: 0x0400125C RID: 4700
	private static float dX;

	// Token: 0x0400125D RID: 4701
	private static float dY;

	// Token: 0x0400125E RID: 4702
	private static float dZ;

	// Token: 0x0400125F RID: 4703
	private static float nX;

	// Token: 0x04001260 RID: 4704
	private static float nY;

	// Token: 0x04001261 RID: 4705
	private static float nZ;

	// Token: 0x04001262 RID: 4706
	private static float dV;

	// Token: 0x04001263 RID: 4707
	private static float dV2;

	// Token: 0x04001264 RID: 4708
	private static float dot;

	// Token: 0x04001265 RID: 4709
	private static float planeDot;

	// Token: 0x04001266 RID: 4710
	private static float SIGHT;

	// Token: 0x04001267 RID: 4711
	private static float PLANEDOTSIGHT;

	// Token: 0x04001268 RID: 4712
	private static float SIGHT2;

	// Token: 0x04001269 RID: 4713
	private static float DOT;

	// Token: 0x0400126A RID: 4714
	private static bool FALLBACK_TOO_CLOSE = false;

	// Token: 0x0400126B RID: 4715
	private static int temp_bTraits;

	// Token: 0x02000450 RID: 1104
	private struct TraitHistory
	{
		// Token: 0x06002731 RID: 10033 RVA: 0x0008F4D0 File Offset: 0x0008D6D0
		public int Upd(int newTraits)
		{
			int num = newTraits ^ this.last;
			this.changed = (num != 0);
			this.last = newTraits;
			return num;
		}

		// Token: 0x0400126C RID: 4716
		public int last;

		// Token: 0x0400126D RID: 4717
		public bool changed;
	}

	// Token: 0x02000451 RID: 1105
	private struct VisMem
	{
		// Token: 0x0400126E RID: 4718
		public global::ODBSet<global::VisNode> list;

		// Token: 0x0400126F RID: 4719
		public global::ODBSet<global::VisNode> last;

		// Token: 0x04001270 RID: 4720
		public int count;

		// Token: 0x04001271 RID: 4721
		public bool add;

		// Token: 0x04001272 RID: 4722
		public bool rem;

		// Token: 0x04001273 RID: 4723
		public bool any;

		// Token: 0x04001274 RID: 4724
		public bool had;
	}

	// Token: 0x02000452 RID: 1106
	public static class Search
	{
		// Token: 0x02000453 RID: 1107
		public interface ISearch : IEnumerable, IEnumerable<global::VisNode>
		{
		}

		// Token: 0x02000454 RID: 1108
		public interface ISearch<TEnumerator> : IEnumerable, global::VisNode.Search.ISearch, IEnumerable<global::VisNode> where TEnumerator : struct, IEnumerator<global::VisNode>
		{
			// Token: 0x06002732 RID: 10034
			TEnumerator GetEnumerator();
		}

		// Token: 0x02000455 RID: 1109
		public struct PointRadiusData
		{
			// Token: 0x06002733 RID: 10035 RVA: 0x0008F4FC File Offset: 0x0008D6FC
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

			// Token: 0x06002734 RID: 10036 RVA: 0x0008F568 File Offset: 0x0008D768
			public bool Pass(global::VisNode current)
			{
				this.dX = this.x - current._stamp.position.x;
				this.dY = this.y - current._stamp.position.y;
				this.dZ = this.z - current._stamp.position.z;
				this.d2 = this.dX * this.dX + this.dY * this.dY + this.dZ * this.dZ;
				return this.d2 <= this.radiusSquare;
			}

			// Token: 0x04001275 RID: 4725
			public float radiusSquare;

			// Token: 0x04001276 RID: 4726
			public float x;

			// Token: 0x04001277 RID: 4727
			public float y;

			// Token: 0x04001278 RID: 4728
			public float z;

			// Token: 0x04001279 RID: 4729
			public float dX;

			// Token: 0x0400127A RID: 4730
			public float dY;

			// Token: 0x0400127B RID: 4731
			public float dZ;

			// Token: 0x0400127C RID: 4732
			public float d2;
		}

		// Token: 0x02000456 RID: 1110
		public struct PointVisibilityData
		{
			// Token: 0x06002735 RID: 10037 RVA: 0x0008F610 File Offset: 0x0008D810
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

			// Token: 0x06002736 RID: 10038 RVA: 0x0008F6B4 File Offset: 0x0008D8B4
			public bool Pass(global::VisNode Current)
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
				global::VisNode.dot = Current._stamp.plane.x * this.nX + Current._stamp.plane.y * this.nY + Current._stamp.plane.z * this.nZ;
				return global::VisNode.dot >= Current.dotArc;
			}

			// Token: 0x0400127D RID: 4733
			public float x;

			// Token: 0x0400127E RID: 4734
			public float y;

			// Token: 0x0400127F RID: 4735
			public float z;

			// Token: 0x04001280 RID: 4736
			public float dX;

			// Token: 0x04001281 RID: 4737
			public float dY;

			// Token: 0x04001282 RID: 4738
			public float dZ;

			// Token: 0x04001283 RID: 4739
			public float d2;

			// Token: 0x04001284 RID: 4740
			public float d;

			// Token: 0x04001285 RID: 4741
			public float nX;

			// Token: 0x04001286 RID: 4742
			public float nY;

			// Token: 0x04001287 RID: 4743
			public float nZ;

			// Token: 0x04001288 RID: 4744
			public float radius;

			// Token: 0x04001289 RID: 4745
			public float radiusSquare;
		}

		// Token: 0x02000457 RID: 1111
		public struct MaskCompareData
		{
			// Token: 0x06002737 RID: 10039 RVA: 0x0008F824 File Offset: 0x0008DA24
			public MaskCompareData(global::Vis.Op op, global::Vis.Mask mask)
			{
				this.op = op;
				this.mask = mask.data;
			}

			// Token: 0x06002738 RID: 10040 RVA: 0x0008F83C File Offset: 0x0008DA3C
			public bool Pass(int mask)
			{
				return global::Vis.Evaluate(this.op, this.mask, mask);
			}

			// Token: 0x0400128A RID: 4746
			public global::Vis.Op op;

			// Token: 0x0400128B RID: 4747
			public int mask;
		}

		// Token: 0x02000458 RID: 1112
		public struct PointRadiusMaskData
		{
			// Token: 0x06002739 RID: 10041 RVA: 0x0008F850 File Offset: 0x0008DA50
			public PointRadiusMaskData(Vector3 pos, float radius, global::Vis.Op op, global::Vis.Mask mask)
			{
				this = new global::VisNode.Search.PointRadiusMaskData(new global::VisNode.Search.PointRadiusData(pos, radius), new global::VisNode.Search.MaskCompareData(op, mask));
			}

			// Token: 0x0600273A RID: 10042 RVA: 0x0008F868 File Offset: 0x0008DA68
			public PointRadiusMaskData(global::VisNode.Search.PointRadiusData pr, global::VisNode.Search.MaskCompareData mc)
			{
				this.pr = pr;
				this.mc = mc;
			}

			// Token: 0x0600273B RID: 10043 RVA: 0x0008F878 File Offset: 0x0008DA78
			public bool Pass(global::VisNode current, int mask)
			{
				return this.mc.Pass(mask) && this.pr.Pass(current);
			}

			// Token: 0x0400128C RID: 4748
			public global::VisNode.Search.PointRadiusData pr;

			// Token: 0x0400128D RID: 4749
			public global::VisNode.Search.MaskCompareData mc;
		}

		// Token: 0x02000459 RID: 1113
		public struct Radial : IEnumerable, global::VisNode.Search.ISearch, IEnumerable<global::VisNode>, global::VisNode.Search.ISearch<global::VisNode.Search.Radial.Enumerator>
		{
			// Token: 0x0600273C RID: 10044 RVA: 0x0008F8A8 File Offset: 0x0008DAA8
			public Radial(Vector3 point, float radius)
			{
				this.point = point;
				this.radius = radius;
			}

			// Token: 0x0600273D RID: 10045 RVA: 0x0008F8B8 File Offset: 0x0008DAB8
			IEnumerator<global::VisNode> IEnumerable<global::VisNode>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0600273E RID: 10046 RVA: 0x0008F8C8 File Offset: 0x0008DAC8
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0600273F RID: 10047 RVA: 0x0008F8D8 File Offset: 0x0008DAD8
			public global::VisNode.Search.Radial.Enumerator GetEnumerator()
			{
				return new global::VisNode.Search.Radial.Enumerator(new global::VisNode.Search.PointRadiusData(this.point, this.radius));
			}

			// Token: 0x0400128E RID: 4750
			public Vector3 point;

			// Token: 0x0400128F RID: 4751
			public float radius;

			// Token: 0x0200045A RID: 1114
			public struct Enumerator : IDisposable, IEnumerator, IEnumerator<global::VisNode>
			{
				// Token: 0x06002740 RID: 10048 RVA: 0x0008F8F0 File Offset: 0x0008DAF0
				public Enumerator(global::VisNode.Search.PointRadiusData pr)
				{
					this.Current = null;
					this.d = false;
					this.e = global::VisNode.db.GetEnumerator();
					this.data = pr;
				}

				// Token: 0x17000912 RID: 2322
				// (get) Token: 0x06002741 RID: 10049 RVA: 0x0008F918 File Offset: 0x0008DB18
				global::VisNode IEnumerator<global::VisNode>.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x17000913 RID: 2323
				// (get) Token: 0x06002742 RID: 10050 RVA: 0x0008F920 File Offset: 0x0008DB20
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x06002743 RID: 10051 RVA: 0x0008F928 File Offset: 0x0008DB28
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

				// Token: 0x06002744 RID: 10052 RVA: 0x0008F960 File Offset: 0x0008DB60
				public void Dispose()
				{
					if (!this.d)
					{
						this.e.Dispose();
						this.d = true;
					}
				}

				// Token: 0x06002745 RID: 10053 RVA: 0x0008F980 File Offset: 0x0008DB80
				public void Reset()
				{
					this.Dispose();
					this.d = false;
					this.e = global::VisNode.db.GetEnumerator();
				}

				// Token: 0x06002746 RID: 10054 RVA: 0x0008F9A0 File Offset: 0x0008DBA0
				private bool Pass(global::VisNode cur)
				{
					if (this.data.Pass(cur))
					{
						this.Current = cur;
						return true;
					}
					return false;
				}

				// Token: 0x04001290 RID: 4752
				public global::ODBForwardEnumerator<global::VisNode> e;

				// Token: 0x04001291 RID: 4753
				public global::VisNode Current;

				// Token: 0x04001292 RID: 4754
				private bool d;

				// Token: 0x04001293 RID: 4755
				public global::VisNode.Search.PointRadiusData data;
			}

			// Token: 0x0200045B RID: 1115
			public struct TraitMasked : IEnumerable, global::VisNode.Search.ISearch, IEnumerable<global::VisNode>, global::VisNode.Search.ISearch<global::VisNode.Search.Radial.TraitMasked.Enumerator>
			{
				// Token: 0x06002747 RID: 10055 RVA: 0x0008F9C0 File Offset: 0x0008DBC0
				public TraitMasked(Vector3 point, float radius, global::Vis.Mask mask, global::Vis.Op op)
				{
					this.point = point;
					this.radius = radius;
					this.maskComp = new global::VisNode.Search.MaskCompareData(op, mask);
				}

				// Token: 0x06002748 RID: 10056 RVA: 0x0008F9E0 File Offset: 0x0008DBE0
				IEnumerator<global::VisNode> IEnumerable<global::VisNode>.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x06002749 RID: 10057 RVA: 0x0008F9F0 File Offset: 0x0008DBF0
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x0600274A RID: 10058 RVA: 0x0008FA00 File Offset: 0x0008DC00
				public global::VisNode.Search.Radial.TraitMasked.Enumerator GetEnumerator()
				{
					return new global::VisNode.Search.Radial.TraitMasked.Enumerator(new global::VisNode.Search.PointRadiusData(this.point, this.radius), this.maskComp);
				}

				// Token: 0x04001294 RID: 4756
				public Vector3 point;

				// Token: 0x04001295 RID: 4757
				public float radius;

				// Token: 0x04001296 RID: 4758
				public global::VisNode.Search.MaskCompareData maskComp;

				// Token: 0x0200045C RID: 1116
				public struct Enumerator : IDisposable, IEnumerator, IEnumerator<global::VisNode>
				{
					// Token: 0x0600274B RID: 10059 RVA: 0x0008FA20 File Offset: 0x0008DC20
					public Enumerator(global::VisNode.Search.PointRadiusData pr, global::VisNode.Search.MaskCompareData mc)
					{
						this.Current = null;
						this.d = false;
						this.e = global::VisNode.db.GetEnumerator();
						this.data = pr;
						this.traitComp = mc;
					}

					// Token: 0x17000914 RID: 2324
					// (get) Token: 0x0600274C RID: 10060 RVA: 0x0008FA5C File Offset: 0x0008DC5C
					global::VisNode IEnumerator<global::VisNode>.Current
					{
						get
						{
							return this.Current;
						}
					}

					// Token: 0x17000915 RID: 2325
					// (get) Token: 0x0600274D RID: 10061 RVA: 0x0008FA64 File Offset: 0x0008DC64
					object IEnumerator.Current
					{
						get
						{
							return this.Current;
						}
					}

					// Token: 0x0600274E RID: 10062 RVA: 0x0008FA6C File Offset: 0x0008DC6C
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

					// Token: 0x0600274F RID: 10063 RVA: 0x0008FAA4 File Offset: 0x0008DCA4
					public void Dispose()
					{
						if (!this.d)
						{
							this.e.Dispose();
							this.d = true;
						}
					}

					// Token: 0x06002750 RID: 10064 RVA: 0x0008FAC4 File Offset: 0x0008DCC4
					public void Reset()
					{
						this.Dispose();
						this.d = false;
						this.e = global::VisNode.db.GetEnumerator();
					}

					// Token: 0x06002751 RID: 10065 RVA: 0x0008FAE4 File Offset: 0x0008DCE4
					private bool Pass(global::VisNode cur)
					{
						if (this.traitComp.Pass(cur._traitMask) && this.data.Pass(cur))
						{
							this.Current = cur;
							return true;
						}
						return false;
					}

					// Token: 0x04001297 RID: 4759
					public global::ODBForwardEnumerator<global::VisNode> e;

					// Token: 0x04001298 RID: 4760
					public global::VisNode Current;

					// Token: 0x04001299 RID: 4761
					private bool d;

					// Token: 0x0400129A RID: 4762
					public global::VisNode.Search.PointRadiusData data;

					// Token: 0x0400129B RID: 4763
					public global::VisNode.Search.MaskCompareData traitComp;
				}
			}

			// Token: 0x0200045D RID: 1117
			public struct SightMasked : IEnumerable, global::VisNode.Search.ISearch, IEnumerable<global::VisNode>, global::VisNode.Search.ISearch<global::VisNode.Search.Radial.SightMasked.Enumerator>
			{
				// Token: 0x06002752 RID: 10066 RVA: 0x0008FB18 File Offset: 0x0008DD18
				public SightMasked(Vector3 point, float radius, global::Vis.Mask mask, global::Vis.Op op)
				{
					this.point = point;
					this.radius = radius;
					this.maskComp = new global::VisNode.Search.MaskCompareData(op, mask);
				}

				// Token: 0x06002753 RID: 10067 RVA: 0x0008FB38 File Offset: 0x0008DD38
				IEnumerator<global::VisNode> IEnumerable<global::VisNode>.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x06002754 RID: 10068 RVA: 0x0008FB48 File Offset: 0x0008DD48
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x06002755 RID: 10069 RVA: 0x0008FB58 File Offset: 0x0008DD58
				public global::VisNode.Search.Radial.SightMasked.Enumerator GetEnumerator()
				{
					return new global::VisNode.Search.Radial.SightMasked.Enumerator(new global::VisNode.Search.PointRadiusData(this.point, this.radius), this.maskComp);
				}

				// Token: 0x0400129C RID: 4764
				public Vector3 point;

				// Token: 0x0400129D RID: 4765
				public float radius;

				// Token: 0x0400129E RID: 4766
				public global::VisNode.Search.MaskCompareData maskComp;

				// Token: 0x0200045E RID: 1118
				public struct Enumerator : IDisposable, IEnumerator, IEnumerator<global::VisNode>
				{
					// Token: 0x06002756 RID: 10070 RVA: 0x0008FB78 File Offset: 0x0008DD78
					public Enumerator(global::VisNode.Search.PointRadiusData pr, global::VisNode.Search.MaskCompareData mc)
					{
						this.Current = null;
						this.d = false;
						this.e = global::VisNode.db.GetEnumerator();
						this.data = pr;
						this.viewComp = mc;
					}

					// Token: 0x17000916 RID: 2326
					// (get) Token: 0x06002757 RID: 10071 RVA: 0x0008FBB4 File Offset: 0x0008DDB4
					global::VisNode IEnumerator<global::VisNode>.Current
					{
						get
						{
							return this.Current;
						}
					}

					// Token: 0x17000917 RID: 2327
					// (get) Token: 0x06002758 RID: 10072 RVA: 0x0008FBBC File Offset: 0x0008DDBC
					object IEnumerator.Current
					{
						get
						{
							return this.Current;
						}
					}

					// Token: 0x06002759 RID: 10073 RVA: 0x0008FBC4 File Offset: 0x0008DDC4
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

					// Token: 0x0600275A RID: 10074 RVA: 0x0008FBFC File Offset: 0x0008DDFC
					public void Dispose()
					{
						if (!this.d)
						{
							this.e.Dispose();
							this.d = true;
						}
					}

					// Token: 0x0600275B RID: 10075 RVA: 0x0008FC1C File Offset: 0x0008DE1C
					public void Reset()
					{
						this.Dispose();
						this.d = false;
						this.e = global::VisNode.db.GetEnumerator();
					}

					// Token: 0x0600275C RID: 10076 RVA: 0x0008FC3C File Offset: 0x0008DE3C
					private bool Pass(global::VisNode cur)
					{
						if (this.viewComp.Pass(cur._sightMask) && this.data.Pass(cur))
						{
							this.Current = cur;
							return true;
						}
						return false;
					}

					// Token: 0x0400129F RID: 4767
					public global::ODBForwardEnumerator<global::VisNode> e;

					// Token: 0x040012A0 RID: 4768
					public global::VisNode Current;

					// Token: 0x040012A1 RID: 4769
					private bool d;

					// Token: 0x040012A2 RID: 4770
					public global::VisNode.Search.PointRadiusData data;

					// Token: 0x040012A3 RID: 4771
					public global::VisNode.Search.MaskCompareData viewComp;
				}
			}

			// Token: 0x0200045F RID: 1119
			public struct Audible : IEnumerable, global::VisNode.Search.ISearch, IEnumerable<global::VisNode>, global::VisNode.Search.ISearch<global::VisNode.Search.Radial.Audible.Enumerator>
			{
				// Token: 0x0600275D RID: 10077 RVA: 0x0008FC70 File Offset: 0x0008DE70
				public Audible(Vector3 point, float radius)
				{
					this.point = point;
					this.radius = radius;
				}

				// Token: 0x0600275E RID: 10078 RVA: 0x0008FC80 File Offset: 0x0008DE80
				IEnumerator<global::VisNode> IEnumerable<global::VisNode>.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x0600275F RID: 10079 RVA: 0x0008FC90 File Offset: 0x0008DE90
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x06002760 RID: 10080 RVA: 0x0008FCA0 File Offset: 0x0008DEA0
				public global::VisNode.Search.Radial.Audible.Enumerator GetEnumerator()
				{
					return new global::VisNode.Search.Radial.Audible.Enumerator(new global::VisNode.Search.PointRadiusData(this.point, this.radius));
				}

				// Token: 0x040012A4 RID: 4772
				public Vector3 point;

				// Token: 0x040012A5 RID: 4773
				public float radius;

				// Token: 0x02000460 RID: 1120
				public struct Enumerator : IDisposable, IEnumerator, IEnumerator<global::VisNode>
				{
					// Token: 0x06002761 RID: 10081 RVA: 0x0008FCB8 File Offset: 0x0008DEB8
					public Enumerator(global::VisNode.Search.PointRadiusData pr)
					{
						this.Current = null;
						this.d = false;
						this.e = global::VisNode.db.GetEnumerator();
						this.data = pr;
					}

					// Token: 0x17000918 RID: 2328
					// (get) Token: 0x06002762 RID: 10082 RVA: 0x0008FCE0 File Offset: 0x0008DEE0
					global::VisNode IEnumerator<global::VisNode>.Current
					{
						get
						{
							return this.Current;
						}
					}

					// Token: 0x17000919 RID: 2329
					// (get) Token: 0x06002763 RID: 10083 RVA: 0x0008FCE8 File Offset: 0x0008DEE8
					object IEnumerator.Current
					{
						get
						{
							return this.Current;
						}
					}

					// Token: 0x06002764 RID: 10084 RVA: 0x0008FCF0 File Offset: 0x0008DEF0
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

					// Token: 0x06002765 RID: 10085 RVA: 0x0008FD28 File Offset: 0x0008DF28
					public void Dispose()
					{
						if (!this.d)
						{
							this.e.Dispose();
							this.d = true;
						}
					}

					// Token: 0x06002766 RID: 10086 RVA: 0x0008FD48 File Offset: 0x0008DF48
					public void Reset()
					{
						this.Dispose();
						this.d = false;
						this.e = global::VisNode.db.GetEnumerator();
					}

					// Token: 0x06002767 RID: 10087 RVA: 0x0008FD68 File Offset: 0x0008DF68
					private bool Pass(global::VisNode cur)
					{
						if (!cur.deaf && this.data.Pass(cur))
						{
							this.Current = cur;
							return true;
						}
						return false;
					}

					// Token: 0x040012A6 RID: 4774
					public global::ODBForwardEnumerator<global::VisNode> e;

					// Token: 0x040012A7 RID: 4775
					public global::VisNode Current;

					// Token: 0x040012A8 RID: 4776
					private bool d;

					// Token: 0x040012A9 RID: 4777
					public global::VisNode.Search.PointRadiusData data;
				}

				// Token: 0x02000461 RID: 1121
				public struct TraitMasked : IEnumerable, global::VisNode.Search.ISearch, IEnumerable<global::VisNode>, global::VisNode.Search.ISearch<global::VisNode.Search.Radial.Audible.TraitMasked.Enumerator>
				{
					// Token: 0x06002768 RID: 10088 RVA: 0x0008FD9C File Offset: 0x0008DF9C
					public TraitMasked(Vector3 point, float radius, global::Vis.Mask mask, global::Vis.Op op)
					{
						this.point = point;
						this.radius = radius;
						this.maskComp = new global::VisNode.Search.MaskCompareData(op, mask);
					}

					// Token: 0x06002769 RID: 10089 RVA: 0x0008FDBC File Offset: 0x0008DFBC
					IEnumerator<global::VisNode> IEnumerable<global::VisNode>.GetEnumerator()
					{
						return this.GetEnumerator();
					}

					// Token: 0x0600276A RID: 10090 RVA: 0x0008FDCC File Offset: 0x0008DFCC
					IEnumerator IEnumerable.GetEnumerator()
					{
						return this.GetEnumerator();
					}

					// Token: 0x0600276B RID: 10091 RVA: 0x0008FDDC File Offset: 0x0008DFDC
					public global::VisNode.Search.Radial.Audible.TraitMasked.Enumerator GetEnumerator()
					{
						return new global::VisNode.Search.Radial.Audible.TraitMasked.Enumerator(new global::VisNode.Search.PointRadiusData(this.point, this.radius), this.maskComp);
					}

					// Token: 0x040012AA RID: 4778
					public Vector3 point;

					// Token: 0x040012AB RID: 4779
					public float radius;

					// Token: 0x040012AC RID: 4780
					public global::VisNode.Search.MaskCompareData maskComp;

					// Token: 0x02000462 RID: 1122
					public struct Enumerator : IDisposable, IEnumerator, IEnumerator<global::VisNode>
					{
						// Token: 0x0600276C RID: 10092 RVA: 0x0008FDFC File Offset: 0x0008DFFC
						public Enumerator(global::VisNode.Search.PointRadiusData pr, global::VisNode.Search.MaskCompareData mc)
						{
							this.Current = null;
							this.d = false;
							this.e = global::VisNode.db.GetEnumerator();
							this.data = pr;
							this.traitComp = mc;
						}

						// Token: 0x1700091A RID: 2330
						// (get) Token: 0x0600276D RID: 10093 RVA: 0x0008FE38 File Offset: 0x0008E038
						global::VisNode IEnumerator<global::VisNode>.Current
						{
							get
							{
								return this.Current;
							}
						}

						// Token: 0x1700091B RID: 2331
						// (get) Token: 0x0600276E RID: 10094 RVA: 0x0008FE40 File Offset: 0x0008E040
						object IEnumerator.Current
						{
							get
							{
								return this.Current;
							}
						}

						// Token: 0x0600276F RID: 10095 RVA: 0x0008FE48 File Offset: 0x0008E048
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

						// Token: 0x06002770 RID: 10096 RVA: 0x0008FE80 File Offset: 0x0008E080
						public void Dispose()
						{
							if (!this.d)
							{
								this.e.Dispose();
								this.d = true;
							}
						}

						// Token: 0x06002771 RID: 10097 RVA: 0x0008FEA0 File Offset: 0x0008E0A0
						public void Reset()
						{
							this.Dispose();
							this.d = false;
							this.e = global::VisNode.db.GetEnumerator();
						}

						// Token: 0x06002772 RID: 10098 RVA: 0x0008FEC0 File Offset: 0x0008E0C0
						private bool Pass(global::VisNode cur)
						{
							if (!cur.deaf && this.traitComp.Pass(cur._traitMask) && this.data.Pass(cur))
							{
								this.Current = cur;
								return true;
							}
							return false;
						}

						// Token: 0x040012AD RID: 4781
						public global::ODBForwardEnumerator<global::VisNode> e;

						// Token: 0x040012AE RID: 4782
						public global::VisNode Current;

						// Token: 0x040012AF RID: 4783
						private bool d;

						// Token: 0x040012B0 RID: 4784
						public global::VisNode.Search.PointRadiusData data;

						// Token: 0x040012B1 RID: 4785
						public global::VisNode.Search.MaskCompareData traitComp;
					}
				}

				// Token: 0x02000463 RID: 1123
				public struct SightMasked : IEnumerable, global::VisNode.Search.ISearch, IEnumerable<global::VisNode>, global::VisNode.Search.ISearch<global::VisNode.Search.Radial.Audible.SightMasked.Enumerator>
				{
					// Token: 0x06002773 RID: 10099 RVA: 0x0008FF0C File Offset: 0x0008E10C
					public SightMasked(Vector3 point, float radius, global::Vis.Mask mask, global::Vis.Op op)
					{
						this.point = point;
						this.radius = radius;
						this.maskComp = new global::VisNode.Search.MaskCompareData(op, mask);
					}

					// Token: 0x06002774 RID: 10100 RVA: 0x0008FF2C File Offset: 0x0008E12C
					IEnumerator<global::VisNode> IEnumerable<global::VisNode>.GetEnumerator()
					{
						return this.GetEnumerator();
					}

					// Token: 0x06002775 RID: 10101 RVA: 0x0008FF3C File Offset: 0x0008E13C
					IEnumerator IEnumerable.GetEnumerator()
					{
						return this.GetEnumerator();
					}

					// Token: 0x06002776 RID: 10102 RVA: 0x0008FF4C File Offset: 0x0008E14C
					public global::VisNode.Search.Radial.Audible.SightMasked.Enumerator GetEnumerator()
					{
						return new global::VisNode.Search.Radial.Audible.SightMasked.Enumerator(new global::VisNode.Search.PointRadiusData(this.point, this.radius), this.maskComp);
					}

					// Token: 0x040012B2 RID: 4786
					public Vector3 point;

					// Token: 0x040012B3 RID: 4787
					public float radius;

					// Token: 0x040012B4 RID: 4788
					public global::VisNode.Search.MaskCompareData maskComp;

					// Token: 0x02000464 RID: 1124
					public struct Enumerator : IDisposable, IEnumerator, IEnumerator<global::VisNode>
					{
						// Token: 0x06002777 RID: 10103 RVA: 0x0008FF6C File Offset: 0x0008E16C
						public Enumerator(global::VisNode.Search.PointRadiusData pr, global::VisNode.Search.MaskCompareData mc)
						{
							this.Current = null;
							this.d = false;
							this.e = global::VisNode.db.GetEnumerator();
							this.data = pr;
							this.viewComp = mc;
						}

						// Token: 0x1700091C RID: 2332
						// (get) Token: 0x06002778 RID: 10104 RVA: 0x0008FFA8 File Offset: 0x0008E1A8
						global::VisNode IEnumerator<global::VisNode>.Current
						{
							get
							{
								return this.Current;
							}
						}

						// Token: 0x1700091D RID: 2333
						// (get) Token: 0x06002779 RID: 10105 RVA: 0x0008FFB0 File Offset: 0x0008E1B0
						object IEnumerator.Current
						{
							get
							{
								return this.Current;
							}
						}

						// Token: 0x0600277A RID: 10106 RVA: 0x0008FFB8 File Offset: 0x0008E1B8
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

						// Token: 0x0600277B RID: 10107 RVA: 0x0008FFF0 File Offset: 0x0008E1F0
						public void Dispose()
						{
							if (!this.d)
							{
								this.e.Dispose();
								this.d = true;
							}
						}

						// Token: 0x0600277C RID: 10108 RVA: 0x00090010 File Offset: 0x0008E210
						public void Reset()
						{
							this.Dispose();
							this.d = false;
							this.e = global::VisNode.db.GetEnumerator();
						}

						// Token: 0x0600277D RID: 10109 RVA: 0x00090030 File Offset: 0x0008E230
						private bool Pass(global::VisNode cur)
						{
							if (!cur.deaf && this.viewComp.Pass(cur._sightMask) && this.data.Pass(cur))
							{
								this.Current = cur;
								return true;
							}
							return false;
						}

						// Token: 0x040012B5 RID: 4789
						public global::ODBForwardEnumerator<global::VisNode> e;

						// Token: 0x040012B6 RID: 4790
						public global::VisNode Current;

						// Token: 0x040012B7 RID: 4791
						private bool d;

						// Token: 0x040012B8 RID: 4792
						public global::VisNode.Search.PointRadiusData data;

						// Token: 0x040012B9 RID: 4793
						public global::VisNode.Search.MaskCompareData viewComp;
					}
				}
			}
		}

		// Token: 0x02000465 RID: 1125
		public struct Point : IEnumerable, global::VisNode.Search.ISearch, IEnumerable<global::VisNode>, global::VisNode.Search.ISearch<global::VisNode.Search.Point.Enumerator>
		{
			// Token: 0x0600277E RID: 10110 RVA: 0x0009007C File Offset: 0x0008E27C
			public Point(Vector3 point)
			{
				this.point = point;
			}

			// Token: 0x0600277F RID: 10111 RVA: 0x00090088 File Offset: 0x0008E288
			IEnumerator<global::VisNode> IEnumerable<global::VisNode>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06002780 RID: 10112 RVA: 0x00090098 File Offset: 0x0008E298
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06002781 RID: 10113 RVA: 0x000900A8 File Offset: 0x0008E2A8
			public global::VisNode.Search.Point.Enumerator GetEnumerator()
			{
				return new global::VisNode.Search.Point.Enumerator(new global::VisNode.Search.PointVisibilityData(this.point));
			}

			// Token: 0x040012BA RID: 4794
			public Vector3 point;

			// Token: 0x02000466 RID: 1126
			public struct Enumerator : IDisposable, IEnumerator, IEnumerator<global::VisNode>
			{
				// Token: 0x06002782 RID: 10114 RVA: 0x000900BC File Offset: 0x0008E2BC
				public Enumerator(global::VisNode.Search.PointVisibilityData pv)
				{
					this.Current = null;
					this.d = false;
					this.e = global::VisNode.db.GetEnumerator();
					this.data = pv;
				}

				// Token: 0x1700091E RID: 2334
				// (get) Token: 0x06002783 RID: 10115 RVA: 0x000900E4 File Offset: 0x0008E2E4
				global::VisNode IEnumerator<global::VisNode>.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x1700091F RID: 2335
				// (get) Token: 0x06002784 RID: 10116 RVA: 0x000900EC File Offset: 0x0008E2EC
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x06002785 RID: 10117 RVA: 0x000900F4 File Offset: 0x0008E2F4
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

				// Token: 0x06002786 RID: 10118 RVA: 0x0009012C File Offset: 0x0008E32C
				public void Dispose()
				{
					if (!this.d)
					{
						this.e.Dispose();
						this.d = true;
					}
				}

				// Token: 0x06002787 RID: 10119 RVA: 0x0009014C File Offset: 0x0008E34C
				public void Reset()
				{
					this.Dispose();
					this.d = false;
					this.e = global::VisNode.db.GetEnumerator();
				}

				// Token: 0x06002788 RID: 10120 RVA: 0x0009016C File Offset: 0x0008E36C
				private bool Pass(global::VisNode cur)
				{
					if (this.data.Pass(cur))
					{
						this.Current = cur;
						return true;
					}
					return false;
				}

				// Token: 0x040012BB RID: 4795
				public global::ODBForwardEnumerator<global::VisNode> e;

				// Token: 0x040012BC RID: 4796
				public global::VisNode Current;

				// Token: 0x040012BD RID: 4797
				private bool d;

				// Token: 0x040012BE RID: 4798
				public global::VisNode.Search.PointVisibilityData data;
			}

			// Token: 0x02000467 RID: 1127
			public struct TraitMasked : IEnumerable, global::VisNode.Search.ISearch, IEnumerable<global::VisNode>, global::VisNode.Search.ISearch<global::VisNode.Search.Point.TraitMasked.Enumerator>
			{
				// Token: 0x06002789 RID: 10121 RVA: 0x0009018C File Offset: 0x0008E38C
				public TraitMasked(Vector3 point, global::Vis.Mask mask, global::Vis.Op op)
				{
					this.point = point;
					this.maskComp = new global::VisNode.Search.MaskCompareData(op, mask);
				}

				// Token: 0x0600278A RID: 10122 RVA: 0x000901A4 File Offset: 0x0008E3A4
				IEnumerator<global::VisNode> IEnumerable<global::VisNode>.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x0600278B RID: 10123 RVA: 0x000901B4 File Offset: 0x0008E3B4
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x0600278C RID: 10124 RVA: 0x000901C4 File Offset: 0x0008E3C4
				public global::VisNode.Search.Point.TraitMasked.Enumerator GetEnumerator()
				{
					return new global::VisNode.Search.Point.TraitMasked.Enumerator(new global::VisNode.Search.PointVisibilityData(this.point), this.maskComp);
				}

				// Token: 0x040012BF RID: 4799
				public Vector3 point;

				// Token: 0x040012C0 RID: 4800
				public global::VisNode.Search.MaskCompareData maskComp;

				// Token: 0x02000468 RID: 1128
				public struct Enumerator : IDisposable, IEnumerator, IEnumerator<global::VisNode>
				{
					// Token: 0x0600278D RID: 10125 RVA: 0x000901DC File Offset: 0x0008E3DC
					public Enumerator(global::VisNode.Search.PointVisibilityData pv, global::VisNode.Search.MaskCompareData mc)
					{
						this.Current = null;
						this.d = false;
						this.e = global::VisNode.db.GetEnumerator();
						this.data = pv;
						this.traitComp = mc;
					}

					// Token: 0x17000920 RID: 2336
					// (get) Token: 0x0600278E RID: 10126 RVA: 0x00090218 File Offset: 0x0008E418
					global::VisNode IEnumerator<global::VisNode>.Current
					{
						get
						{
							return this.Current;
						}
					}

					// Token: 0x17000921 RID: 2337
					// (get) Token: 0x0600278F RID: 10127 RVA: 0x00090220 File Offset: 0x0008E420
					object IEnumerator.Current
					{
						get
						{
							return this.Current;
						}
					}

					// Token: 0x06002790 RID: 10128 RVA: 0x00090228 File Offset: 0x0008E428
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

					// Token: 0x06002791 RID: 10129 RVA: 0x00090260 File Offset: 0x0008E460
					public void Dispose()
					{
						if (!this.d)
						{
							this.e.Dispose();
							this.d = true;
						}
					}

					// Token: 0x06002792 RID: 10130 RVA: 0x00090280 File Offset: 0x0008E480
					public void Reset()
					{
						this.Dispose();
						this.d = false;
						this.e = global::VisNode.db.GetEnumerator();
					}

					// Token: 0x06002793 RID: 10131 RVA: 0x000902A0 File Offset: 0x0008E4A0
					private bool Pass(global::VisNode cur)
					{
						if (this.traitComp.Pass(cur._traitMask) && this.data.Pass(cur))
						{
							this.Current = cur;
							return true;
						}
						return false;
					}

					// Token: 0x040012C1 RID: 4801
					public global::ODBForwardEnumerator<global::VisNode> e;

					// Token: 0x040012C2 RID: 4802
					public global::VisNode Current;

					// Token: 0x040012C3 RID: 4803
					private bool d;

					// Token: 0x040012C4 RID: 4804
					public global::VisNode.Search.PointVisibilityData data;

					// Token: 0x040012C5 RID: 4805
					public global::VisNode.Search.MaskCompareData traitComp;
				}
			}

			// Token: 0x02000469 RID: 1129
			public struct SightMasked : IEnumerable, global::VisNode.Search.ISearch, IEnumerable<global::VisNode>, global::VisNode.Search.ISearch<global::VisNode.Search.Point.SightMasked.Enumerator>
			{
				// Token: 0x06002794 RID: 10132 RVA: 0x000902D4 File Offset: 0x0008E4D4
				public SightMasked(Vector3 point, global::Vis.Mask mask, global::Vis.Op op)
				{
					this.point = point;
					this.maskComp = new global::VisNode.Search.MaskCompareData(op, mask);
				}

				// Token: 0x06002795 RID: 10133 RVA: 0x000902EC File Offset: 0x0008E4EC
				IEnumerator<global::VisNode> IEnumerable<global::VisNode>.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x06002796 RID: 10134 RVA: 0x000902FC File Offset: 0x0008E4FC
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x06002797 RID: 10135 RVA: 0x0009030C File Offset: 0x0008E50C
				public global::VisNode.Search.Point.SightMasked.Enumerator GetEnumerator()
				{
					return new global::VisNode.Search.Point.SightMasked.Enumerator(new global::VisNode.Search.PointVisibilityData(this.point), this.maskComp);
				}

				// Token: 0x040012C6 RID: 4806
				public Vector3 point;

				// Token: 0x040012C7 RID: 4807
				public global::VisNode.Search.MaskCompareData maskComp;

				// Token: 0x0200046A RID: 1130
				public struct Enumerator : IDisposable, IEnumerator, IEnumerator<global::VisNode>
				{
					// Token: 0x06002798 RID: 10136 RVA: 0x00090324 File Offset: 0x0008E524
					public Enumerator(global::VisNode.Search.PointVisibilityData pv, global::VisNode.Search.MaskCompareData mc)
					{
						this.Current = null;
						this.d = false;
						this.e = global::VisNode.db.GetEnumerator();
						this.data = pv;
						this.viewComp = mc;
					}

					// Token: 0x17000922 RID: 2338
					// (get) Token: 0x06002799 RID: 10137 RVA: 0x00090360 File Offset: 0x0008E560
					global::VisNode IEnumerator<global::VisNode>.Current
					{
						get
						{
							return this.Current;
						}
					}

					// Token: 0x17000923 RID: 2339
					// (get) Token: 0x0600279A RID: 10138 RVA: 0x00090368 File Offset: 0x0008E568
					object IEnumerator.Current
					{
						get
						{
							return this.Current;
						}
					}

					// Token: 0x0600279B RID: 10139 RVA: 0x00090370 File Offset: 0x0008E570
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

					// Token: 0x0600279C RID: 10140 RVA: 0x000903A8 File Offset: 0x0008E5A8
					public void Dispose()
					{
						if (!this.d)
						{
							this.e.Dispose();
							this.d = true;
						}
					}

					// Token: 0x0600279D RID: 10141 RVA: 0x000903C8 File Offset: 0x0008E5C8
					public void Reset()
					{
						this.Dispose();
						this.d = false;
						this.e = global::VisNode.db.GetEnumerator();
					}

					// Token: 0x0600279E RID: 10142 RVA: 0x000903E8 File Offset: 0x0008E5E8
					private bool Pass(global::VisNode cur)
					{
						if (this.viewComp.Pass(cur._sightMask) && this.data.Pass(cur))
						{
							this.Current = cur;
							return true;
						}
						return false;
					}

					// Token: 0x040012C8 RID: 4808
					public global::ODBForwardEnumerator<global::VisNode> e;

					// Token: 0x040012C9 RID: 4809
					public global::VisNode Current;

					// Token: 0x040012CA RID: 4810
					private bool d;

					// Token: 0x040012CB RID: 4811
					public global::VisNode.Search.PointVisibilityData data;

					// Token: 0x040012CC RID: 4812
					public global::VisNode.Search.MaskCompareData viewComp;
				}
			}

			// Token: 0x0200046B RID: 1131
			public struct Visual : IEnumerable, global::VisNode.Search.ISearch, IEnumerable<global::VisNode>, global::VisNode.Search.ISearch<global::VisNode.Search.Point.Visual.Enumerator>
			{
				// Token: 0x0600279F RID: 10143 RVA: 0x0009041C File Offset: 0x0008E61C
				public Visual(Vector3 point)
				{
					this.point = point;
				}

				// Token: 0x060027A0 RID: 10144 RVA: 0x00090428 File Offset: 0x0008E628
				IEnumerator<global::VisNode> IEnumerable<global::VisNode>.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x060027A1 RID: 10145 RVA: 0x00090438 File Offset: 0x0008E638
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x060027A2 RID: 10146 RVA: 0x00090448 File Offset: 0x0008E648
				public global::VisNode.Search.Point.Visual.Enumerator GetEnumerator()
				{
					return new global::VisNode.Search.Point.Visual.Enumerator(new global::VisNode.Search.PointVisibilityData(this.point));
				}

				// Token: 0x040012CD RID: 4813
				public Vector3 point;

				// Token: 0x0200046C RID: 1132
				public struct Enumerator : IDisposable, IEnumerator, IEnumerator<global::VisNode>
				{
					// Token: 0x060027A3 RID: 10147 RVA: 0x0009045C File Offset: 0x0008E65C
					public Enumerator(global::VisNode.Search.PointVisibilityData pv)
					{
						this.Current = null;
						this.d = false;
						this.e = global::VisNode.db.GetEnumerator();
						this.data = pv;
					}

					// Token: 0x17000924 RID: 2340
					// (get) Token: 0x060027A4 RID: 10148 RVA: 0x00090484 File Offset: 0x0008E684
					global::VisNode IEnumerator<global::VisNode>.Current
					{
						get
						{
							return this.Current;
						}
					}

					// Token: 0x17000925 RID: 2341
					// (get) Token: 0x060027A5 RID: 10149 RVA: 0x0009048C File Offset: 0x0008E68C
					object IEnumerator.Current
					{
						get
						{
							return this.Current;
						}
					}

					// Token: 0x060027A6 RID: 10150 RVA: 0x00090494 File Offset: 0x0008E694
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

					// Token: 0x060027A7 RID: 10151 RVA: 0x000904CC File Offset: 0x0008E6CC
					public void Dispose()
					{
						if (!this.d)
						{
							this.e.Dispose();
							this.d = true;
						}
					}

					// Token: 0x060027A8 RID: 10152 RVA: 0x000904EC File Offset: 0x0008E6EC
					public void Reset()
					{
						this.Dispose();
						this.d = false;
						this.e = global::VisNode.db.GetEnumerator();
					}

					// Token: 0x060027A9 RID: 10153 RVA: 0x0009050C File Offset: 0x0008E70C
					private bool Pass(global::VisNode cur)
					{
						return false;
					}

					// Token: 0x040012CE RID: 4814
					public global::ODBForwardEnumerator<global::VisNode> e;

					// Token: 0x040012CF RID: 4815
					public global::VisNode Current;

					// Token: 0x040012D0 RID: 4816
					private bool d;

					// Token: 0x040012D1 RID: 4817
					public global::VisNode.Search.PointVisibilityData data;
				}

				// Token: 0x0200046D RID: 1133
				public struct TraitMasked : IEnumerable, global::VisNode.Search.ISearch, IEnumerable<global::VisNode>, global::VisNode.Search.ISearch<global::VisNode.Search.Point.Visual.TraitMasked.Enumerator>
				{
					// Token: 0x060027AA RID: 10154 RVA: 0x00090510 File Offset: 0x0008E710
					public TraitMasked(Vector3 point, global::Vis.Mask mask, global::Vis.Op op)
					{
						this.point = point;
						this.maskComp = new global::VisNode.Search.MaskCompareData(op, mask);
					}

					// Token: 0x060027AB RID: 10155 RVA: 0x00090528 File Offset: 0x0008E728
					IEnumerator<global::VisNode> IEnumerable<global::VisNode>.GetEnumerator()
					{
						return this.GetEnumerator();
					}

					// Token: 0x060027AC RID: 10156 RVA: 0x00090538 File Offset: 0x0008E738
					IEnumerator IEnumerable.GetEnumerator()
					{
						return this.GetEnumerator();
					}

					// Token: 0x060027AD RID: 10157 RVA: 0x00090548 File Offset: 0x0008E748
					public global::VisNode.Search.Point.Visual.TraitMasked.Enumerator GetEnumerator()
					{
						return new global::VisNode.Search.Point.Visual.TraitMasked.Enumerator(new global::VisNode.Search.PointVisibilityData(this.point), this.maskComp);
					}

					// Token: 0x040012D2 RID: 4818
					public Vector3 point;

					// Token: 0x040012D3 RID: 4819
					public global::VisNode.Search.MaskCompareData maskComp;

					// Token: 0x0200046E RID: 1134
					public struct Enumerator : IDisposable, IEnumerator, IEnumerator<global::VisNode>
					{
						// Token: 0x060027AE RID: 10158 RVA: 0x00090560 File Offset: 0x0008E760
						public Enumerator(global::VisNode.Search.PointVisibilityData pv, global::VisNode.Search.MaskCompareData mc)
						{
							this.Current = null;
							this.d = false;
							this.e = global::VisNode.db.GetEnumerator();
							this.data = pv;
							this.traitComp = mc;
						}

						// Token: 0x17000926 RID: 2342
						// (get) Token: 0x060027AF RID: 10159 RVA: 0x0009059C File Offset: 0x0008E79C
						global::VisNode IEnumerator<global::VisNode>.Current
						{
							get
							{
								return this.Current;
							}
						}

						// Token: 0x17000927 RID: 2343
						// (get) Token: 0x060027B0 RID: 10160 RVA: 0x000905A4 File Offset: 0x0008E7A4
						object IEnumerator.Current
						{
							get
							{
								return this.Current;
							}
						}

						// Token: 0x060027B1 RID: 10161 RVA: 0x000905AC File Offset: 0x0008E7AC
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

						// Token: 0x060027B2 RID: 10162 RVA: 0x000905E4 File Offset: 0x0008E7E4
						public void Dispose()
						{
							if (!this.d)
							{
								this.e.Dispose();
								this.d = true;
							}
						}

						// Token: 0x060027B3 RID: 10163 RVA: 0x00090604 File Offset: 0x0008E804
						public void Reset()
						{
							this.Dispose();
							this.d = false;
							this.e = global::VisNode.db.GetEnumerator();
						}

						// Token: 0x060027B4 RID: 10164 RVA: 0x00090624 File Offset: 0x0008E824
						private bool Pass(global::VisNode cur)
						{
							return false;
						}

						// Token: 0x040012D4 RID: 4820
						public global::ODBForwardEnumerator<global::VisNode> e;

						// Token: 0x040012D5 RID: 4821
						public global::VisNode Current;

						// Token: 0x040012D6 RID: 4822
						private bool d;

						// Token: 0x040012D7 RID: 4823
						public global::VisNode.Search.PointVisibilityData data;

						// Token: 0x040012D8 RID: 4824
						public global::VisNode.Search.MaskCompareData traitComp;
					}
				}

				// Token: 0x0200046F RID: 1135
				public struct SightMasked : IEnumerable, global::VisNode.Search.ISearch, IEnumerable<global::VisNode>, global::VisNode.Search.ISearch<global::VisNode.Search.Point.Visual.SightMasked.Enumerator>
				{
					// Token: 0x060027B5 RID: 10165 RVA: 0x00090628 File Offset: 0x0008E828
					public SightMasked(Vector3 point, global::Vis.Mask mask, global::Vis.Op op)
					{
						this.point = point;
						this.maskComp = new global::VisNode.Search.MaskCompareData(op, mask);
					}

					// Token: 0x060027B6 RID: 10166 RVA: 0x00090640 File Offset: 0x0008E840
					IEnumerator<global::VisNode> IEnumerable<global::VisNode>.GetEnumerator()
					{
						return this.GetEnumerator();
					}

					// Token: 0x060027B7 RID: 10167 RVA: 0x00090650 File Offset: 0x0008E850
					IEnumerator IEnumerable.GetEnumerator()
					{
						return this.GetEnumerator();
					}

					// Token: 0x060027B8 RID: 10168 RVA: 0x00090660 File Offset: 0x0008E860
					public global::VisNode.Search.Point.Visual.SightMasked.Enumerator GetEnumerator()
					{
						return new global::VisNode.Search.Point.Visual.SightMasked.Enumerator(new global::VisNode.Search.PointVisibilityData(this.point), this.maskComp);
					}

					// Token: 0x040012D9 RID: 4825
					public Vector3 point;

					// Token: 0x040012DA RID: 4826
					public global::VisNode.Search.MaskCompareData maskComp;

					// Token: 0x02000470 RID: 1136
					public struct Enumerator : IDisposable, IEnumerator, IEnumerator<global::VisNode>
					{
						// Token: 0x060027B9 RID: 10169 RVA: 0x00090678 File Offset: 0x0008E878
						public Enumerator(global::VisNode.Search.PointVisibilityData pv, global::VisNode.Search.MaskCompareData mc)
						{
							this.Current = null;
							this.d = false;
							this.e = global::VisNode.db.GetEnumerator();
							this.data = pv;
							this.viewComp = mc;
						}

						// Token: 0x17000928 RID: 2344
						// (get) Token: 0x060027BA RID: 10170 RVA: 0x000906B4 File Offset: 0x0008E8B4
						global::VisNode IEnumerator<global::VisNode>.Current
						{
							get
							{
								return this.Current;
							}
						}

						// Token: 0x17000929 RID: 2345
						// (get) Token: 0x060027BB RID: 10171 RVA: 0x000906BC File Offset: 0x0008E8BC
						object IEnumerator.Current
						{
							get
							{
								return this.Current;
							}
						}

						// Token: 0x060027BC RID: 10172 RVA: 0x000906C4 File Offset: 0x0008E8C4
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

						// Token: 0x060027BD RID: 10173 RVA: 0x000906FC File Offset: 0x0008E8FC
						public void Dispose()
						{
							if (!this.d)
							{
								this.e.Dispose();
								this.d = true;
							}
						}

						// Token: 0x060027BE RID: 10174 RVA: 0x0009071C File Offset: 0x0008E91C
						public void Reset()
						{
							this.Dispose();
							this.d = false;
							this.e = global::VisNode.db.GetEnumerator();
						}

						// Token: 0x060027BF RID: 10175 RVA: 0x0009073C File Offset: 0x0008E93C
						private bool Pass(global::VisNode cur)
						{
							return false;
						}

						// Token: 0x040012DB RID: 4827
						public global::ODBForwardEnumerator<global::VisNode> e;

						// Token: 0x040012DC RID: 4828
						public global::VisNode Current;

						// Token: 0x040012DD RID: 4829
						private bool d;

						// Token: 0x040012DE RID: 4830
						public global::VisNode.Search.PointVisibilityData data;

						// Token: 0x040012DF RID: 4831
						public global::VisNode.Search.MaskCompareData viewComp;
					}
				}
			}
		}
	}
}
