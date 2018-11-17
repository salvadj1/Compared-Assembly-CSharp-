using System;
using System.Runtime.InteropServices;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x020003B0 RID: 944
[StructLayout(LayoutKind.Explicit, Size = 4)]
public struct NetEntityID : IEquatable<uLink.NetworkViewID>, IEquatable<global::NetEntityID>, IComparable<uLink.NetworkViewID>, IComparable<global::NetEntityID>
{
	// Token: 0x060020EE RID: 8430 RVA: 0x00079A1C File Offset: 0x00077C1C
	public NetEntityID(global::NGCView view)
	{
		this = default(global::NetEntityID);
		if (view)
		{
			this.v = view.id;
		}
	}

	// Token: 0x060020EF RID: 8431 RVA: 0x00079A50 File Offset: 0x00077C50
	public NetEntityID(uLink.NetworkView view)
	{
		this = default(global::NetEntityID);
		if (view)
		{
			this._viewID = view.viewID;
		}
	}

	// Token: 0x060020F0 RID: 8432 RVA: 0x00079A84 File Offset: 0x00077C84
	public NetEntityID(uLink.NetworkViewID viewID)
	{
		this = default(global::NetEntityID);
		this._viewID = viewID;
	}

	// Token: 0x060020F1 RID: 8433 RVA: 0x00079AA8 File Offset: 0x00077CA8
	static NetEntityID()
	{
		BitStreamCodec.AddAndMakeArray<global::NetEntityID>(global::NetEntityID.deserializer, global::NetEntityID.serializer);
	}

	// Token: 0x170007C7 RID: 1991
	// (get) Token: 0x060020F2 RID: 8434 RVA: 0x00079ADC File Offset: 0x00077CDC
	public bool isNet
	{
		get
		{
			return this.p1 == 0 && this._viewID != uLink.NetworkViewID.unassigned;
		}
	}

	// Token: 0x170007C8 RID: 1992
	// (get) Token: 0x060020F3 RID: 8435 RVA: 0x00079AFC File Offset: 0x00077CFC
	public bool isNGC
	{
		get
		{
			return this.p1 != 0;
		}
	}

	// Token: 0x170007C9 RID: 1993
	// (get) Token: 0x060020F4 RID: 8436 RVA: 0x00079B0C File Offset: 0x00077D0C
	public bool isUnassigned
	{
		get
		{
			return this.v == 0;
		}
	}

	// Token: 0x170007CA RID: 1994
	// (get) Token: 0x060020F5 RID: 8437 RVA: 0x00079B18 File Offset: 0x00077D18
	public bool isMine
	{
		get
		{
			return this.p1 == 0 && this._viewID.isMine;
		}
	}

	// Token: 0x170007CB RID: 1995
	// (get) Token: 0x060020F6 RID: 8438 RVA: 0x00079B34 File Offset: 0x00077D34
	public bool isAllocated
	{
		get
		{
			return this.p1 != 0 || this._viewID.isAllocated;
		}
	}

	// Token: 0x170007CC RID: 1996
	// (get) Token: 0x060020F7 RID: 8439 RVA: 0x00079B50 File Offset: 0x00077D50
	public bool isManual
	{
		get
		{
			return this.p1 == 0 && this._viewID.isManual;
		}
	}

	// Token: 0x170007CD RID: 1997
	// (get) Token: 0x060020F8 RID: 8440 RVA: 0x00079B6C File Offset: 0x00077D6C
	public int id
	{
		get
		{
			return this.v;
		}
	}

	// Token: 0x170007CE RID: 1998
	// (get) Token: 0x060020F9 RID: 8441 RVA: 0x00079B74 File Offset: 0x00077D74
	public uLink.NetworkPlayer owner
	{
		get
		{
			if (this.p1 == 0)
			{
				return this._viewID.owner;
			}
			return uLink.NetworkPlayer.server;
		}
	}

	// Token: 0x060020FA RID: 8442 RVA: 0x00079B94 File Offset: 0x00077D94
	public override bool Equals(object obj)
	{
		return (!(obj is global::NetEntityID)) ? (this.isNet && obj is uLink.NetworkViewID && this.Equals((uLink.NetworkViewID)obj)) : this.Equals((global::NetEntityID)obj);
	}

	// Token: 0x060020FB RID: 8443 RVA: 0x00079BE4 File Offset: 0x00077DE4
	public bool Equals(global::NetEntityID obj)
	{
		return this.v == obj.v;
	}

	// Token: 0x060020FC RID: 8444 RVA: 0x00079BF8 File Offset: 0x00077DF8
	public bool Equals(uLink.NetworkViewID obj)
	{
		return this.p1 == 0 && this._viewID == obj;
	}

	// Token: 0x060020FD RID: 8445 RVA: 0x00079C14 File Offset: 0x00077E14
	public override string ToString()
	{
		if (this.v == 0)
		{
			return "Unassigned";
		}
		if (this.p1 == 0)
		{
			return this._viewID.ToString();
		}
		return string.Format("NGC ViewID {0} ({1}:{2})", this.v, this.p1, (int)(this.p2 + 1));
	}

	// Token: 0x170007CF RID: 1999
	// (get) Token: 0x060020FE RID: 8446 RVA: 0x00079C78 File Offset: 0x00077E78
	public static global::NetEntityID unassigned
	{
		get
		{
			return default(global::NetEntityID);
		}
	}

	// Token: 0x170007D0 RID: 2000
	// (get) Token: 0x060020FF RID: 8447 RVA: 0x00079C90 File Offset: 0x00077E90
	public IDMain main
	{
		get
		{
			if (this.p1 == 0)
			{
				if (this.p2 == 0)
				{
					return null;
				}
				Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(this._viewID);
				if (!networkView)
				{
					return null;
				}
				IDBase idbase = IDBase.Get(networkView);
				if (idbase)
				{
					return idbase.idMain;
				}
				return null;
			}
			else
			{
				global::NGCView ngcview = global::NGC.Find(this.v);
				if (ngcview)
				{
					return IDBase.GetMain(ngcview.gameObject);
				}
				return null;
			}
		}
	}

	// Token: 0x170007D1 RID: 2001
	// (get) Token: 0x06002100 RID: 8448 RVA: 0x00079D10 File Offset: 0x00077F10
	public IDBase idBase
	{
		get
		{
			if (this.p1 == 0)
			{
				if (this.p2 == 0)
				{
					return null;
				}
				Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(this._viewID);
				if (networkView)
				{
					return IDBase.Get(networkView);
				}
				return null;
			}
			else
			{
				global::NGCView ngcview = global::NGC.Find(this.v);
				if (ngcview)
				{
					return IDBase.Get(ngcview.gameObject);
				}
				return null;
			}
		}
	}

	// Token: 0x170007D2 RID: 2002
	// (get) Token: 0x06002101 RID: 8449 RVA: 0x00079D7C File Offset: 0x00077F7C
	public Facepunch.NetworkView networkView
	{
		get
		{
			if (this.p1 == 0)
			{
				return Facepunch.NetworkView.Find(this._viewID);
			}
			return null;
		}
	}

	// Token: 0x170007D3 RID: 2003
	// (get) Token: 0x06002102 RID: 8450 RVA: 0x00079D98 File Offset: 0x00077F98
	public global::NGCView ngcView
	{
		get
		{
			if (this.p1 == 0)
			{
				return null;
			}
			return global::NGC.Find(this.v);
		}
	}

	// Token: 0x170007D4 RID: 2004
	// (get) Token: 0x06002103 RID: 8451 RVA: 0x00079DB4 File Offset: 0x00077FB4
	public GameObject gameObject
	{
		get
		{
			MonoBehaviour view = this.view;
			return (!view) ? null : view.gameObject;
		}
	}

	// Token: 0x170007D5 RID: 2005
	// (get) Token: 0x06002104 RID: 8452 RVA: 0x00079DE0 File Offset: 0x00077FE0
	public MonoBehaviour view
	{
		get
		{
			if (this.p1 != 0)
			{
				return global::NGC.Find(this.v);
			}
			if (this.p2 == 0)
			{
				return null;
			}
			return Facepunch.NetworkView.Find(this._viewID);
		}
	}

	// Token: 0x06002105 RID: 8453 RVA: 0x00079E14 File Offset: 0x00078014
	public override int GetHashCode()
	{
		return (this.p1 != 0) ? (this.v ^ -65536) : this.p2.GetHashCode();
	}

	// Token: 0x06002106 RID: 8454 RVA: 0x00079E40 File Offset: 0x00078040
	public int CompareTo(global::NetEntityID other)
	{
		return this.v.CompareTo(other.v);
	}

	// Token: 0x06002107 RID: 8455 RVA: 0x00079E54 File Offset: 0x00078054
	public int CompareTo(uLink.NetworkViewID other)
	{
		return this.v.CompareTo(other.id);
	}

	// Token: 0x06002108 RID: 8456 RVA: 0x00079E68 File Offset: 0x00078068
	private static void Serializer(BitStream bs, object value, params object[] codecOptions)
	{
		global::NetEntityID netEntityID = (global::NetEntityID)value;
		bs.Write<ushort>(netEntityID.p1, codecOptions);
		if (netEntityID.p1 == 0)
		{
			bs.Write<uLink.NetworkViewID>(netEntityID._viewID, codecOptions);
		}
		else
		{
			bs.Write<ushort>(netEntityID.p2, new object[0]);
		}
	}

	// Token: 0x06002109 RID: 8457 RVA: 0x00079EBC File Offset: 0x000780BC
	private static object Deserializer(BitStream bs, params object[] codecOptions)
	{
		global::NetEntityID netEntityID = default(global::NetEntityID);
		netEntityID.p1 = bs.Read<ushort>(codecOptions);
		if (netEntityID.p1 == 0)
		{
			netEntityID._viewID = bs.Read<uLink.NetworkViewID>(codecOptions);
		}
		else
		{
			netEntityID.p2 = bs.Read<ushort>(codecOptions);
		}
		return netEntityID;
	}

	// Token: 0x0600210A RID: 8458 RVA: 0x00079F14 File Offset: 0x00078114
	public static global::NetEntityID.Kind Of(Component component, out global::NetEntityID entID, out MonoBehaviour view)
	{
		if (component is MonoBehaviour)
		{
			return global::NetEntityID.Of((MonoBehaviour)component, out entID, out view);
		}
		if (component)
		{
			return global::NetEntityID.Of(component.gameObject, out entID, out view);
		}
		entID = global::NetEntityID.unassigned;
		view = null;
		return global::NetEntityID.Kind.Missing;
	}

	// Token: 0x0600210B RID: 8459 RVA: 0x00079F64 File Offset: 0x00078164
	public static global::NetEntityID.Kind Of(Component component, out global::NetEntityID entID)
	{
		MonoBehaviour monoBehaviour;
		return global::NetEntityID.Of(component, out entID, out monoBehaviour);
	}

	// Token: 0x0600210C RID: 8460 RVA: 0x00079F7C File Offset: 0x0007817C
	public static global::NetEntityID.Kind Of(Component component, out MonoBehaviour view)
	{
		global::NetEntityID netEntityID;
		return global::NetEntityID.Of(component, out netEntityID, out view);
	}

	// Token: 0x0600210D RID: 8461 RVA: 0x00079F94 File Offset: 0x00078194
	public static global::NetEntityID.Kind Of(Component component)
	{
		global::NetEntityID netEntityID;
		MonoBehaviour monoBehaviour;
		return global::NetEntityID.Of(component, out netEntityID, out monoBehaviour);
	}

	// Token: 0x0600210E RID: 8462 RVA: 0x00079FAC File Offset: 0x000781AC
	public static global::NetEntityID.Kind Of(MonoBehaviour script, out global::NetEntityID entID, out MonoBehaviour view)
	{
		if (!script)
		{
			entID = global::NetEntityID.unassigned;
			view = null;
			return global::NetEntityID.Kind.Missing;
		}
		if (script is uLink.NetworkView)
		{
			view = script;
			entID = ((uLink.NetworkView)script).viewID;
			return global::NetEntityID.Kind.Net;
		}
		if (script is global::NGCView)
		{
			view = script;
			entID = new global::NetEntityID((global::NGCView)script);
			return global::NetEntityID.Kind.NGC;
		}
		return global::NetEntityID.Of(script.gameObject, out entID, out view);
	}

	// Token: 0x0600210F RID: 8463 RVA: 0x0007A024 File Offset: 0x00078224
	public static global::NetEntityID.Kind Of(MonoBehaviour script, out global::NetEntityID entID)
	{
		MonoBehaviour monoBehaviour;
		return global::NetEntityID.Of(script, out entID, out monoBehaviour);
	}

	// Token: 0x06002110 RID: 8464 RVA: 0x0007A03C File Offset: 0x0007823C
	public static global::NetEntityID.Kind Of(MonoBehaviour script, out MonoBehaviour view)
	{
		global::NetEntityID netEntityID;
		return global::NetEntityID.Of(script, out netEntityID, out view);
	}

	// Token: 0x06002111 RID: 8465 RVA: 0x0007A054 File Offset: 0x00078254
	public static global::NetEntityID.Kind Of(MonoBehaviour script)
	{
		global::NetEntityID netEntityID;
		MonoBehaviour monoBehaviour;
		return global::NetEntityID.Of(script, out netEntityID, out monoBehaviour);
	}

	// Token: 0x06002112 RID: 8466 RVA: 0x0007A06C File Offset: 0x0007826C
	public static global::NetEntityID.Kind Of(GameObject entity)
	{
		global::NetEntityID netEntityID;
		MonoBehaviour monoBehaviour;
		return global::NetEntityID.Of(entity, out netEntityID, out monoBehaviour);
	}

	// Token: 0x06002113 RID: 8467 RVA: 0x0007A084 File Offset: 0x00078284
	public static global::NetEntityID.Kind Of(GameObject entity, out MonoBehaviour view)
	{
		global::NetEntityID netEntityID;
		return global::NetEntityID.Of(entity, out netEntityID, out view);
	}

	// Token: 0x06002114 RID: 8468 RVA: 0x0007A09C File Offset: 0x0007829C
	public static global::NetEntityID.Kind Of(GameObject entity, out global::NetEntityID entID)
	{
		MonoBehaviour monoBehaviour;
		return global::NetEntityID.Of(entity, out entID, out monoBehaviour);
	}

	// Token: 0x06002115 RID: 8469 RVA: 0x0007A0B4 File Offset: 0x000782B4
	public static global::NetEntityID.Kind Of(GameObject entity, out global::NetEntityID entID, out MonoBehaviour view)
	{
		if (!entity)
		{
			entID = global::NetEntityID.unassigned;
			view = null;
			return global::NetEntityID.Kind.Missing;
		}
		uLink.NetworkView component = entity.GetComponent<uLink.NetworkView>();
		if (component)
		{
			entID = new global::NetEntityID(component.viewID);
			view = component;
			return global::NetEntityID.Kind.Net;
		}
		global::NGCView component2 = entity.GetComponent<global::NGCView>();
		if (component2)
		{
			entID = new global::NetEntityID(component2);
			view = component2;
			return global::NetEntityID.Kind.NGC;
		}
		entID = global::NetEntityID.unassigned;
		view = null;
		return global::NetEntityID.Kind.Missing;
	}

	// Token: 0x06002116 RID: 8470 RVA: 0x0007A12C File Offset: 0x0007832C
	public TComponent GetComponent<TComponent>() where TComponent : Component
	{
		MonoBehaviour view = this.view;
		return (!view) ? ((TComponent)((object)null)) : view.GetComponent<TComponent>();
	}

	// Token: 0x06002117 RID: 8471 RVA: 0x0007A15C File Offset: 0x0007835C
	public bool GetComponent<TComponent>(out TComponent component) where TComponent : Component
	{
		MonoBehaviour view = this.view;
		if (!view)
		{
			component = (TComponent)((object)null);
			return false;
		}
		if (view is TComponent)
		{
			component = (TComponent)((object)view);
			return true;
		}
		return component = view.GetComponent<TComponent>();
	}

	// Token: 0x170007D6 RID: 2006
	// (get) Token: 0x06002118 RID: 8472 RVA: 0x0007A1BC File Offset: 0x000783BC
	public Collider collider
	{
		get
		{
			MonoBehaviour view = this.view;
			return (!view) ? null : view.collider;
		}
	}

	// Token: 0x170007D7 RID: 2007
	// (get) Token: 0x06002119 RID: 8473 RVA: 0x0007A1E8 File Offset: 0x000783E8
	public Renderer renderer
	{
		get
		{
			MonoBehaviour view = this.view;
			return (!view) ? null : view.renderer;
		}
	}

	// Token: 0x170007D8 RID: 2008
	// (get) Token: 0x0600211A RID: 8474 RVA: 0x0007A214 File Offset: 0x00078414
	public Transform transform
	{
		get
		{
			MonoBehaviour view = this.view;
			return (!view) ? null : view.transform;
		}
	}

	// Token: 0x170007D9 RID: 2009
	// (get) Token: 0x0600211B RID: 8475 RVA: 0x0007A240 File Offset: 0x00078440
	public Rigidbody rigidbody
	{
		get
		{
			MonoBehaviour view = this.view;
			return (!view) ? null : view.rigidbody;
		}
	}

	// Token: 0x0600211C RID: 8476 RVA: 0x0007A26C File Offset: 0x0007846C
	public static global::NetEntityID Get(GameObject entity)
	{
		return global::NetEntityID.Get(entity, false);
	}

	// Token: 0x0600211D RID: 8477 RVA: 0x0007A278 File Offset: 0x00078478
	public static global::NetEntityID Get(GameObject entity, bool throwIfNotFound)
	{
		global::NetEntityID result;
		if ((int)global::NetEntityID.Of(entity, out result) != 0)
		{
			return result;
		}
		if (throwIfNotFound)
		{
			throw new InvalidOperationException("no recognizable net entity id");
		}
		return global::NetEntityID.unassigned;
	}

	// Token: 0x0600211E RID: 8478 RVA: 0x0007A2AC File Offset: 0x000784AC
	public static global::NetEntityID Get(Component entityComponent)
	{
		return global::NetEntityID.Get(entityComponent, false);
	}

	// Token: 0x0600211F RID: 8479 RVA: 0x0007A2B8 File Offset: 0x000784B8
	public static global::NetEntityID Get(Component entityComponent, bool throwIfNotFound)
	{
		global::NetEntityID result;
		if ((int)global::NetEntityID.Of(entityComponent, out result) != 0)
		{
			return result;
		}
		if (throwIfNotFound)
		{
			throw new InvalidOperationException("no recognizable net entity id");
		}
		return global::NetEntityID.unassigned;
	}

	// Token: 0x06002120 RID: 8480 RVA: 0x0007A2EC File Offset: 0x000784EC
	public static global::NetEntityID Get(MonoBehaviour entityScript)
	{
		return global::NetEntityID.Get(entityScript, false);
	}

	// Token: 0x06002121 RID: 8481 RVA: 0x0007A2F8 File Offset: 0x000784F8
	public static global::NetEntityID Get(MonoBehaviour entityScript, bool throwIfNotFound)
	{
		global::NetEntityID result;
		if ((int)global::NetEntityID.Of(entityScript, out result) != 0)
		{
			return result;
		}
		if (throwIfNotFound)
		{
			throw new InvalidOperationException("no recognizable net entity id");
		}
		return global::NetEntityID.unassigned;
	}

	// Token: 0x06002122 RID: 8482 RVA: 0x0007A32C File Offset: 0x0007852C
	public static global::NetEntityID Get(uLink.NetworkViewID id)
	{
		return new global::NetEntityID(id);
	}

	// Token: 0x06002123 RID: 8483 RVA: 0x0007A334 File Offset: 0x00078534
	public static bool operator ==(global::NetEntityID lhs, global::NetEntityID rhs)
	{
		return lhs.v == rhs.v;
	}

	// Token: 0x06002124 RID: 8484 RVA: 0x0007A348 File Offset: 0x00078548
	public static bool operator !=(global::NetEntityID lhs, global::NetEntityID rhs)
	{
		return lhs.v != rhs.v;
	}

	// Token: 0x06002125 RID: 8485 RVA: 0x0007A360 File Offset: 0x00078560
	public static bool operator ==(global::NetEntityID lhs, uLink.NetworkViewID rhs)
	{
		return lhs.p1 == 0 && (int)lhs.p2 == rhs.id;
	}

	// Token: 0x06002126 RID: 8486 RVA: 0x0007A384 File Offset: 0x00078584
	public static bool operator !=(global::NetEntityID lhs, uLink.NetworkViewID rhs)
	{
		return lhs.p1 != 0 || (int)lhs.p2 != rhs.id;
	}

	// Token: 0x06002127 RID: 8487 RVA: 0x0007A3B4 File Offset: 0x000785B4
	public static bool operator ==(uLink.NetworkViewID lhs, global::NetEntityID rhs)
	{
		return rhs.p1 == 0 && (int)rhs.p2 == lhs.id;
	}

	// Token: 0x06002128 RID: 8488 RVA: 0x0007A3D8 File Offset: 0x000785D8
	public static bool operator !=(uLink.NetworkViewID lhs, global::NetEntityID rhs)
	{
		return rhs.p1 != 0 || (int)rhs.p2 != lhs.id;
	}

	// Token: 0x06002129 RID: 8489 RVA: 0x0007A408 File Offset: 0x00078608
	public static bool operator >=(global::NetEntityID lhs, global::NetEntityID rhs)
	{
		return lhs.v >= rhs.v;
	}

	// Token: 0x0600212A RID: 8490 RVA: 0x0007A420 File Offset: 0x00078620
	public static bool operator >=(global::NetEntityID lhs, uLink.NetworkViewID rhs)
	{
		return lhs.v >= rhs.id;
	}

	// Token: 0x0600212B RID: 8491 RVA: 0x0007A438 File Offset: 0x00078638
	public static bool operator >=(uLink.NetworkViewID lhs, global::NetEntityID rhs)
	{
		return lhs.id >= rhs.v;
	}

	// Token: 0x0600212C RID: 8492 RVA: 0x0007A450 File Offset: 0x00078650
	public static bool operator <=(global::NetEntityID lhs, global::NetEntityID rhs)
	{
		return lhs.v <= rhs.v;
	}

	// Token: 0x0600212D RID: 8493 RVA: 0x0007A468 File Offset: 0x00078668
	public static bool operator <=(global::NetEntityID lhs, uLink.NetworkViewID rhs)
	{
		return lhs.v <= rhs.id;
	}

	// Token: 0x0600212E RID: 8494 RVA: 0x0007A480 File Offset: 0x00078680
	public static bool operator <=(uLink.NetworkViewID lhs, global::NetEntityID rhs)
	{
		return lhs.id <= rhs.v;
	}

	// Token: 0x0600212F RID: 8495 RVA: 0x0007A498 File Offset: 0x00078698
	public static bool operator >(global::NetEntityID lhs, global::NetEntityID rhs)
	{
		return lhs.v > rhs.v;
	}

	// Token: 0x06002130 RID: 8496 RVA: 0x0007A4AC File Offset: 0x000786AC
	public static bool operator >(global::NetEntityID lhs, uLink.NetworkViewID rhs)
	{
		return lhs.v > rhs.id;
	}

	// Token: 0x06002131 RID: 8497 RVA: 0x0007A4C0 File Offset: 0x000786C0
	public static bool operator >(uLink.NetworkViewID lhs, global::NetEntityID rhs)
	{
		return lhs.id > rhs.v;
	}

	// Token: 0x06002132 RID: 8498 RVA: 0x0007A4D4 File Offset: 0x000786D4
	public static bool operator <(global::NetEntityID lhs, global::NetEntityID rhs)
	{
		return lhs.v < rhs.v;
	}

	// Token: 0x06002133 RID: 8499 RVA: 0x0007A4E8 File Offset: 0x000786E8
	public static bool operator <(global::NetEntityID lhs, uLink.NetworkViewID rhs)
	{
		return lhs.v < rhs.id;
	}

	// Token: 0x06002134 RID: 8500 RVA: 0x0007A4FC File Offset: 0x000786FC
	public static bool operator <(uLink.NetworkViewID lhs, global::NetEntityID rhs)
	{
		return lhs.id < rhs.v;
	}

	// Token: 0x06002135 RID: 8501 RVA: 0x0007A510 File Offset: 0x00078710
	public static implicit operator global::NetEntityID(uLink.NetworkViewID viewID)
	{
		return new global::NetEntityID
		{
			_viewID = viewID
		};
	}

	// Token: 0x06002136 RID: 8502 RVA: 0x0007A530 File Offset: 0x00078730
	public static explicit operator uLink.NetworkViewID(global::NetEntityID viewID)
	{
		if (viewID.p1 != 0)
		{
			throw new InvalidCastException("The NetEntityID did not represet a NetworkViewID");
		}
		return viewID._viewID;
	}

	// Token: 0x06002137 RID: 8503 RVA: 0x0007A550 File Offset: 0x00078750
	public static bool operator true(global::NetEntityID id)
	{
		return id.v != 0;
	}

	// Token: 0x06002138 RID: 8504 RVA: 0x0007A560 File Offset: 0x00078760
	public static bool operator false(global::NetEntityID id)
	{
		return id.v == 0;
	}

	// Token: 0x04000F67 RID: 3943
	[FieldOffset(0)]
	private uLink.NetworkViewID _viewID;

	// Token: 0x04000F68 RID: 3944
	[FieldOffset(0)]
	private ushort p2;

	// Token: 0x04000F69 RID: 3945
	[FieldOffset(2)]
	private ushort p1;

	// Token: 0x04000F6A RID: 3946
	[FieldOffset(0)]
	private int v;

	// Token: 0x04000F6B RID: 3947
	private static readonly BitStreamCodec.Serializer serializer = new BitStreamCodec.Serializer(global::NetEntityID.Serializer);

	// Token: 0x04000F6C RID: 3948
	private static readonly BitStreamCodec.Deserializer deserializer = new BitStreamCodec.Deserializer(global::NetEntityID.Deserializer);

	// Token: 0x020003B1 RID: 945
	public enum Kind : sbyte
	{
		// Token: 0x04000F6E RID: 3950
		NGC = -1,
		// Token: 0x04000F6F RID: 3951
		Missing,
		// Token: 0x04000F70 RID: 3952
		Net
	}
}
