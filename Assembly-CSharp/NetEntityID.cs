using System;
using System.Runtime.InteropServices;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x02000308 RID: 776
[StructLayout(LayoutKind.Explicit, Size = 4)]
public struct NetEntityID : IEquatable<NetworkViewID>, IEquatable<NetEntityID>, IComparable<NetworkViewID>, IComparable<NetEntityID>
{
	// Token: 0x06001DB0 RID: 7600 RVA: 0x00074F9C File Offset: 0x0007319C
	public NetEntityID(NGCView view)
	{
		this = default(NetEntityID);
		if (view)
		{
			this.v = view.id;
		}
	}

	// Token: 0x06001DB1 RID: 7601 RVA: 0x00074FD0 File Offset: 0x000731D0
	public NetEntityID(NetworkView view)
	{
		this = default(NetEntityID);
		if (view)
		{
			this._viewID = view.viewID;
		}
	}

	// Token: 0x06001DB2 RID: 7602 RVA: 0x00075004 File Offset: 0x00073204
	public NetEntityID(NetworkViewID viewID)
	{
		this = default(NetEntityID);
		this._viewID = viewID;
	}

	// Token: 0x06001DB3 RID: 7603 RVA: 0x00075028 File Offset: 0x00073228
	static NetEntityID()
	{
		BitStreamCodec.AddAndMakeArray<NetEntityID>(NetEntityID.deserializer, NetEntityID.serializer);
	}

	// Token: 0x17000771 RID: 1905
	// (get) Token: 0x06001DB4 RID: 7604 RVA: 0x0007505C File Offset: 0x0007325C
	public bool isNet
	{
		get
		{
			return this.p1 == 0 && this._viewID != NetworkViewID.unassigned;
		}
	}

	// Token: 0x17000772 RID: 1906
	// (get) Token: 0x06001DB5 RID: 7605 RVA: 0x0007507C File Offset: 0x0007327C
	public bool isNGC
	{
		get
		{
			return this.p1 != 0;
		}
	}

	// Token: 0x17000773 RID: 1907
	// (get) Token: 0x06001DB6 RID: 7606 RVA: 0x0007508C File Offset: 0x0007328C
	public bool isUnassigned
	{
		get
		{
			return this.v == 0;
		}
	}

	// Token: 0x17000774 RID: 1908
	// (get) Token: 0x06001DB7 RID: 7607 RVA: 0x00075098 File Offset: 0x00073298
	public bool isMine
	{
		get
		{
			return this.p1 == 0 && this._viewID.isMine;
		}
	}

	// Token: 0x17000775 RID: 1909
	// (get) Token: 0x06001DB8 RID: 7608 RVA: 0x000750B4 File Offset: 0x000732B4
	public bool isAllocated
	{
		get
		{
			return this.p1 != 0 || this._viewID.isAllocated;
		}
	}

	// Token: 0x17000776 RID: 1910
	// (get) Token: 0x06001DB9 RID: 7609 RVA: 0x000750D0 File Offset: 0x000732D0
	public bool isManual
	{
		get
		{
			return this.p1 == 0 && this._viewID.isManual;
		}
	}

	// Token: 0x17000777 RID: 1911
	// (get) Token: 0x06001DBA RID: 7610 RVA: 0x000750EC File Offset: 0x000732EC
	public int id
	{
		get
		{
			return this.v;
		}
	}

	// Token: 0x17000778 RID: 1912
	// (get) Token: 0x06001DBB RID: 7611 RVA: 0x000750F4 File Offset: 0x000732F4
	public NetworkPlayer owner
	{
		get
		{
			if (this.p1 == 0)
			{
				return this._viewID.owner;
			}
			return NetworkPlayer.server;
		}
	}

	// Token: 0x06001DBC RID: 7612 RVA: 0x00075114 File Offset: 0x00073314
	public override bool Equals(object obj)
	{
		return (!(obj is NetEntityID)) ? (this.isNet && obj is NetworkViewID && this.Equals((NetworkViewID)obj)) : this.Equals((NetEntityID)obj);
	}

	// Token: 0x06001DBD RID: 7613 RVA: 0x00075164 File Offset: 0x00073364
	public bool Equals(NetEntityID obj)
	{
		return this.v == obj.v;
	}

	// Token: 0x06001DBE RID: 7614 RVA: 0x00075178 File Offset: 0x00073378
	public bool Equals(NetworkViewID obj)
	{
		return this.p1 == 0 && this._viewID == obj;
	}

	// Token: 0x06001DBF RID: 7615 RVA: 0x00075194 File Offset: 0x00073394
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

	// Token: 0x17000779 RID: 1913
	// (get) Token: 0x06001DC0 RID: 7616 RVA: 0x000751F8 File Offset: 0x000733F8
	public static NetEntityID unassigned
	{
		get
		{
			return default(NetEntityID);
		}
	}

	// Token: 0x1700077A RID: 1914
	// (get) Token: 0x06001DC1 RID: 7617 RVA: 0x00075210 File Offset: 0x00073410
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
				NetworkView networkView = NetworkView.Find(this._viewID);
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
				NGCView ngcview = NGC.Find(this.v);
				if (ngcview)
				{
					return IDBase.GetMain(ngcview.gameObject);
				}
				return null;
			}
		}
	}

	// Token: 0x1700077B RID: 1915
	// (get) Token: 0x06001DC2 RID: 7618 RVA: 0x00075290 File Offset: 0x00073490
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
				NetworkView networkView = NetworkView.Find(this._viewID);
				if (networkView)
				{
					return IDBase.Get(networkView);
				}
				return null;
			}
			else
			{
				NGCView ngcview = NGC.Find(this.v);
				if (ngcview)
				{
					return IDBase.Get(ngcview.gameObject);
				}
				return null;
			}
		}
	}

	// Token: 0x1700077C RID: 1916
	// (get) Token: 0x06001DC3 RID: 7619 RVA: 0x000752FC File Offset: 0x000734FC
	public NetworkView networkView
	{
		get
		{
			if (this.p1 == 0)
			{
				return NetworkView.Find(this._viewID);
			}
			return null;
		}
	}

	// Token: 0x1700077D RID: 1917
	// (get) Token: 0x06001DC4 RID: 7620 RVA: 0x00075318 File Offset: 0x00073518
	public NGCView ngcView
	{
		get
		{
			if (this.p1 == 0)
			{
				return null;
			}
			return NGC.Find(this.v);
		}
	}

	// Token: 0x1700077E RID: 1918
	// (get) Token: 0x06001DC5 RID: 7621 RVA: 0x00075334 File Offset: 0x00073534
	public GameObject gameObject
	{
		get
		{
			MonoBehaviour view = this.view;
			return (!view) ? null : view.gameObject;
		}
	}

	// Token: 0x1700077F RID: 1919
	// (get) Token: 0x06001DC6 RID: 7622 RVA: 0x00075360 File Offset: 0x00073560
	public MonoBehaviour view
	{
		get
		{
			if (this.p1 != 0)
			{
				return NGC.Find(this.v);
			}
			if (this.p2 == 0)
			{
				return null;
			}
			return NetworkView.Find(this._viewID);
		}
	}

	// Token: 0x06001DC7 RID: 7623 RVA: 0x00075394 File Offset: 0x00073594
	public override int GetHashCode()
	{
		return (this.p1 != 0) ? (this.v ^ -65536) : this.p2.GetHashCode();
	}

	// Token: 0x06001DC8 RID: 7624 RVA: 0x000753C0 File Offset: 0x000735C0
	public int CompareTo(NetEntityID other)
	{
		return this.v.CompareTo(other.v);
	}

	// Token: 0x06001DC9 RID: 7625 RVA: 0x000753D4 File Offset: 0x000735D4
	public int CompareTo(NetworkViewID other)
	{
		return this.v.CompareTo(other.id);
	}

	// Token: 0x06001DCA RID: 7626 RVA: 0x000753E8 File Offset: 0x000735E8
	private static void Serializer(BitStream bs, object value, params object[] codecOptions)
	{
		NetEntityID netEntityID = (NetEntityID)value;
		bs.Write<ushort>(netEntityID.p1, codecOptions);
		if (netEntityID.p1 == 0)
		{
			bs.Write<NetworkViewID>(netEntityID._viewID, codecOptions);
		}
		else
		{
			bs.Write<ushort>(netEntityID.p2, new object[0]);
		}
	}

	// Token: 0x06001DCB RID: 7627 RVA: 0x0007543C File Offset: 0x0007363C
	private static object Deserializer(BitStream bs, params object[] codecOptions)
	{
		NetEntityID netEntityID = default(NetEntityID);
		netEntityID.p1 = bs.Read<ushort>(codecOptions);
		if (netEntityID.p1 == 0)
		{
			netEntityID._viewID = bs.Read<NetworkViewID>(codecOptions);
		}
		else
		{
			netEntityID.p2 = bs.Read<ushort>(codecOptions);
		}
		return netEntityID;
	}

	// Token: 0x06001DCC RID: 7628 RVA: 0x00075494 File Offset: 0x00073694
	public static NetEntityID.Kind Of(Component component, out NetEntityID entID, out MonoBehaviour view)
	{
		if (component is MonoBehaviour)
		{
			return NetEntityID.Of((MonoBehaviour)component, out entID, out view);
		}
		if (component)
		{
			return NetEntityID.Of(component.gameObject, out entID, out view);
		}
		entID = NetEntityID.unassigned;
		view = null;
		return NetEntityID.Kind.Missing;
	}

	// Token: 0x06001DCD RID: 7629 RVA: 0x000754E4 File Offset: 0x000736E4
	public static NetEntityID.Kind Of(Component component, out NetEntityID entID)
	{
		MonoBehaviour monoBehaviour;
		return NetEntityID.Of(component, out entID, out monoBehaviour);
	}

	// Token: 0x06001DCE RID: 7630 RVA: 0x000754FC File Offset: 0x000736FC
	public static NetEntityID.Kind Of(Component component, out MonoBehaviour view)
	{
		NetEntityID netEntityID;
		return NetEntityID.Of(component, out netEntityID, out view);
	}

	// Token: 0x06001DCF RID: 7631 RVA: 0x00075514 File Offset: 0x00073714
	public static NetEntityID.Kind Of(Component component)
	{
		NetEntityID netEntityID;
		MonoBehaviour monoBehaviour;
		return NetEntityID.Of(component, out netEntityID, out monoBehaviour);
	}

	// Token: 0x06001DD0 RID: 7632 RVA: 0x0007552C File Offset: 0x0007372C
	public static NetEntityID.Kind Of(MonoBehaviour script, out NetEntityID entID, out MonoBehaviour view)
	{
		if (!script)
		{
			entID = NetEntityID.unassigned;
			view = null;
			return NetEntityID.Kind.Missing;
		}
		if (script is NetworkView)
		{
			view = script;
			entID = ((NetworkView)script).viewID;
			return NetEntityID.Kind.Net;
		}
		if (script is NGCView)
		{
			view = script;
			entID = new NetEntityID((NGCView)script);
			return NetEntityID.Kind.NGC;
		}
		return NetEntityID.Of(script.gameObject, out entID, out view);
	}

	// Token: 0x06001DD1 RID: 7633 RVA: 0x000755A4 File Offset: 0x000737A4
	public static NetEntityID.Kind Of(MonoBehaviour script, out NetEntityID entID)
	{
		MonoBehaviour monoBehaviour;
		return NetEntityID.Of(script, out entID, out monoBehaviour);
	}

	// Token: 0x06001DD2 RID: 7634 RVA: 0x000755BC File Offset: 0x000737BC
	public static NetEntityID.Kind Of(MonoBehaviour script, out MonoBehaviour view)
	{
		NetEntityID netEntityID;
		return NetEntityID.Of(script, out netEntityID, out view);
	}

	// Token: 0x06001DD3 RID: 7635 RVA: 0x000755D4 File Offset: 0x000737D4
	public static NetEntityID.Kind Of(MonoBehaviour script)
	{
		NetEntityID netEntityID;
		MonoBehaviour monoBehaviour;
		return NetEntityID.Of(script, out netEntityID, out monoBehaviour);
	}

	// Token: 0x06001DD4 RID: 7636 RVA: 0x000755EC File Offset: 0x000737EC
	public static NetEntityID.Kind Of(GameObject entity)
	{
		NetEntityID netEntityID;
		MonoBehaviour monoBehaviour;
		return NetEntityID.Of(entity, out netEntityID, out monoBehaviour);
	}

	// Token: 0x06001DD5 RID: 7637 RVA: 0x00075604 File Offset: 0x00073804
	public static NetEntityID.Kind Of(GameObject entity, out MonoBehaviour view)
	{
		NetEntityID netEntityID;
		return NetEntityID.Of(entity, out netEntityID, out view);
	}

	// Token: 0x06001DD6 RID: 7638 RVA: 0x0007561C File Offset: 0x0007381C
	public static NetEntityID.Kind Of(GameObject entity, out NetEntityID entID)
	{
		MonoBehaviour monoBehaviour;
		return NetEntityID.Of(entity, out entID, out monoBehaviour);
	}

	// Token: 0x06001DD7 RID: 7639 RVA: 0x00075634 File Offset: 0x00073834
	public static NetEntityID.Kind Of(GameObject entity, out NetEntityID entID, out MonoBehaviour view)
	{
		if (!entity)
		{
			entID = NetEntityID.unassigned;
			view = null;
			return NetEntityID.Kind.Missing;
		}
		NetworkView component = entity.GetComponent<NetworkView>();
		if (component)
		{
			entID = new NetEntityID(component.viewID);
			view = component;
			return NetEntityID.Kind.Net;
		}
		NGCView component2 = entity.GetComponent<NGCView>();
		if (component2)
		{
			entID = new NetEntityID(component2);
			view = component2;
			return NetEntityID.Kind.NGC;
		}
		entID = NetEntityID.unassigned;
		view = null;
		return NetEntityID.Kind.Missing;
	}

	// Token: 0x06001DD8 RID: 7640 RVA: 0x000756AC File Offset: 0x000738AC
	public TComponent GetComponent<TComponent>() where TComponent : Component
	{
		MonoBehaviour view = this.view;
		return (!view) ? ((TComponent)((object)null)) : view.GetComponent<TComponent>();
	}

	// Token: 0x06001DD9 RID: 7641 RVA: 0x000756DC File Offset: 0x000738DC
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

	// Token: 0x17000780 RID: 1920
	// (get) Token: 0x06001DDA RID: 7642 RVA: 0x0007573C File Offset: 0x0007393C
	public Collider collider
	{
		get
		{
			MonoBehaviour view = this.view;
			return (!view) ? null : view.collider;
		}
	}

	// Token: 0x17000781 RID: 1921
	// (get) Token: 0x06001DDB RID: 7643 RVA: 0x00075768 File Offset: 0x00073968
	public Renderer renderer
	{
		get
		{
			MonoBehaviour view = this.view;
			return (!view) ? null : view.renderer;
		}
	}

	// Token: 0x17000782 RID: 1922
	// (get) Token: 0x06001DDC RID: 7644 RVA: 0x00075794 File Offset: 0x00073994
	public Transform transform
	{
		get
		{
			MonoBehaviour view = this.view;
			return (!view) ? null : view.transform;
		}
	}

	// Token: 0x17000783 RID: 1923
	// (get) Token: 0x06001DDD RID: 7645 RVA: 0x000757C0 File Offset: 0x000739C0
	public Rigidbody rigidbody
	{
		get
		{
			MonoBehaviour view = this.view;
			return (!view) ? null : view.rigidbody;
		}
	}

	// Token: 0x06001DDE RID: 7646 RVA: 0x000757EC File Offset: 0x000739EC
	public static NetEntityID Get(GameObject entity)
	{
		return NetEntityID.Get(entity, false);
	}

	// Token: 0x06001DDF RID: 7647 RVA: 0x000757F8 File Offset: 0x000739F8
	public static NetEntityID Get(GameObject entity, bool throwIfNotFound)
	{
		NetEntityID result;
		if ((int)NetEntityID.Of(entity, out result) != 0)
		{
			return result;
		}
		if (throwIfNotFound)
		{
			throw new InvalidOperationException("no recognizable net entity id");
		}
		return NetEntityID.unassigned;
	}

	// Token: 0x06001DE0 RID: 7648 RVA: 0x0007582C File Offset: 0x00073A2C
	public static NetEntityID Get(Component entityComponent)
	{
		return NetEntityID.Get(entityComponent, false);
	}

	// Token: 0x06001DE1 RID: 7649 RVA: 0x00075838 File Offset: 0x00073A38
	public static NetEntityID Get(Component entityComponent, bool throwIfNotFound)
	{
		NetEntityID result;
		if ((int)NetEntityID.Of(entityComponent, out result) != 0)
		{
			return result;
		}
		if (throwIfNotFound)
		{
			throw new InvalidOperationException("no recognizable net entity id");
		}
		return NetEntityID.unassigned;
	}

	// Token: 0x06001DE2 RID: 7650 RVA: 0x0007586C File Offset: 0x00073A6C
	public static NetEntityID Get(MonoBehaviour entityScript)
	{
		return NetEntityID.Get(entityScript, false);
	}

	// Token: 0x06001DE3 RID: 7651 RVA: 0x00075878 File Offset: 0x00073A78
	public static NetEntityID Get(MonoBehaviour entityScript, bool throwIfNotFound)
	{
		NetEntityID result;
		if ((int)NetEntityID.Of(entityScript, out result) != 0)
		{
			return result;
		}
		if (throwIfNotFound)
		{
			throw new InvalidOperationException("no recognizable net entity id");
		}
		return NetEntityID.unassigned;
	}

	// Token: 0x06001DE4 RID: 7652 RVA: 0x000758AC File Offset: 0x00073AAC
	public static NetEntityID Get(NetworkViewID id)
	{
		return new NetEntityID(id);
	}

	// Token: 0x06001DE5 RID: 7653 RVA: 0x000758B4 File Offset: 0x00073AB4
	public static bool operator ==(NetEntityID lhs, NetEntityID rhs)
	{
		return lhs.v == rhs.v;
	}

	// Token: 0x06001DE6 RID: 7654 RVA: 0x000758C8 File Offset: 0x00073AC8
	public static bool operator !=(NetEntityID lhs, NetEntityID rhs)
	{
		return lhs.v != rhs.v;
	}

	// Token: 0x06001DE7 RID: 7655 RVA: 0x000758E0 File Offset: 0x00073AE0
	public static bool operator ==(NetEntityID lhs, NetworkViewID rhs)
	{
		return lhs.p1 == 0 && (int)lhs.p2 == rhs.id;
	}

	// Token: 0x06001DE8 RID: 7656 RVA: 0x00075904 File Offset: 0x00073B04
	public static bool operator !=(NetEntityID lhs, NetworkViewID rhs)
	{
		return lhs.p1 != 0 || (int)lhs.p2 != rhs.id;
	}

	// Token: 0x06001DE9 RID: 7657 RVA: 0x00075934 File Offset: 0x00073B34
	public static bool operator ==(NetworkViewID lhs, NetEntityID rhs)
	{
		return rhs.p1 == 0 && (int)rhs.p2 == lhs.id;
	}

	// Token: 0x06001DEA RID: 7658 RVA: 0x00075958 File Offset: 0x00073B58
	public static bool operator !=(NetworkViewID lhs, NetEntityID rhs)
	{
		return rhs.p1 != 0 || (int)rhs.p2 != lhs.id;
	}

	// Token: 0x06001DEB RID: 7659 RVA: 0x00075988 File Offset: 0x00073B88
	public static bool operator >=(NetEntityID lhs, NetEntityID rhs)
	{
		return lhs.v >= rhs.v;
	}

	// Token: 0x06001DEC RID: 7660 RVA: 0x000759A0 File Offset: 0x00073BA0
	public static bool operator >=(NetEntityID lhs, NetworkViewID rhs)
	{
		return lhs.v >= rhs.id;
	}

	// Token: 0x06001DED RID: 7661 RVA: 0x000759B8 File Offset: 0x00073BB8
	public static bool operator >=(NetworkViewID lhs, NetEntityID rhs)
	{
		return lhs.id >= rhs.v;
	}

	// Token: 0x06001DEE RID: 7662 RVA: 0x000759D0 File Offset: 0x00073BD0
	public static bool operator <=(NetEntityID lhs, NetEntityID rhs)
	{
		return lhs.v <= rhs.v;
	}

	// Token: 0x06001DEF RID: 7663 RVA: 0x000759E8 File Offset: 0x00073BE8
	public static bool operator <=(NetEntityID lhs, NetworkViewID rhs)
	{
		return lhs.v <= rhs.id;
	}

	// Token: 0x06001DF0 RID: 7664 RVA: 0x00075A00 File Offset: 0x00073C00
	public static bool operator <=(NetworkViewID lhs, NetEntityID rhs)
	{
		return lhs.id <= rhs.v;
	}

	// Token: 0x06001DF1 RID: 7665 RVA: 0x00075A18 File Offset: 0x00073C18
	public static bool operator >(NetEntityID lhs, NetEntityID rhs)
	{
		return lhs.v > rhs.v;
	}

	// Token: 0x06001DF2 RID: 7666 RVA: 0x00075A2C File Offset: 0x00073C2C
	public static bool operator >(NetEntityID lhs, NetworkViewID rhs)
	{
		return lhs.v > rhs.id;
	}

	// Token: 0x06001DF3 RID: 7667 RVA: 0x00075A40 File Offset: 0x00073C40
	public static bool operator >(NetworkViewID lhs, NetEntityID rhs)
	{
		return lhs.id > rhs.v;
	}

	// Token: 0x06001DF4 RID: 7668 RVA: 0x00075A54 File Offset: 0x00073C54
	public static bool operator <(NetEntityID lhs, NetEntityID rhs)
	{
		return lhs.v < rhs.v;
	}

	// Token: 0x06001DF5 RID: 7669 RVA: 0x00075A68 File Offset: 0x00073C68
	public static bool operator <(NetEntityID lhs, NetworkViewID rhs)
	{
		return lhs.v < rhs.id;
	}

	// Token: 0x06001DF6 RID: 7670 RVA: 0x00075A7C File Offset: 0x00073C7C
	public static bool operator <(NetworkViewID lhs, NetEntityID rhs)
	{
		return lhs.id < rhs.v;
	}

	// Token: 0x06001DF7 RID: 7671 RVA: 0x00075A90 File Offset: 0x00073C90
	public static implicit operator NetEntityID(NetworkViewID viewID)
	{
		return new NetEntityID
		{
			_viewID = viewID
		};
	}

	// Token: 0x06001DF8 RID: 7672 RVA: 0x00075AB0 File Offset: 0x00073CB0
	public static explicit operator NetworkViewID(NetEntityID viewID)
	{
		if (viewID.p1 != 0)
		{
			throw new InvalidCastException("The NetEntityID did not represet a NetworkViewID");
		}
		return viewID._viewID;
	}

	// Token: 0x06001DF9 RID: 7673 RVA: 0x00075AD0 File Offset: 0x00073CD0
	public static bool operator true(NetEntityID id)
	{
		return id.v != 0;
	}

	// Token: 0x06001DFA RID: 7674 RVA: 0x00075AE0 File Offset: 0x00073CE0
	public static bool operator false(NetEntityID id)
	{
		return id.v == 0;
	}

	// Token: 0x04000E27 RID: 3623
	[FieldOffset(0)]
	private NetworkViewID _viewID;

	// Token: 0x04000E28 RID: 3624
	[FieldOffset(0)]
	private ushort p2;

	// Token: 0x04000E29 RID: 3625
	[FieldOffset(2)]
	private ushort p1;

	// Token: 0x04000E2A RID: 3626
	[FieldOffset(0)]
	private int v;

	// Token: 0x04000E2B RID: 3627
	private static readonly BitStreamCodec.Serializer serializer = new BitStreamCodec.Serializer(NetEntityID.Serializer);

	// Token: 0x04000E2C RID: 3628
	private static readonly BitStreamCodec.Deserializer deserializer = new BitStreamCodec.Deserializer(NetEntityID.Deserializer);

	// Token: 0x02000309 RID: 777
	public enum Kind : sbyte
	{
		// Token: 0x04000E2E RID: 3630
		NGC = -1,
		// Token: 0x04000E2F RID: 3631
		Missing,
		// Token: 0x04000E30 RID: 3632
		Net
	}
}
