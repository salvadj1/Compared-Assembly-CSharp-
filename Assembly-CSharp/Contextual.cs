using System;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x02000542 RID: 1346
[global::InterfaceDriverComponent(typeof(global::IContextRequestable), "_implementation", "implementation", SearchRoute = global::InterfaceSearchRoute.GameObject, UnityType = typeof(MonoBehaviour), AlwaysSaveDisabled = true)]
public sealed class Contextual : MonoBehaviour, global::IComponentInterfaceDriver<global::IContextRequestable, MonoBehaviour, global::Contextual>
{
	// Token: 0x170009C0 RID: 2496
	// (get) Token: 0x06002CF4 RID: 11508 RVA: 0x000A830C File Offset: 0x000A650C
	public bool isSoleAccess
	{
		get
		{
			bool? isSoleAccess = this._isSoleAccess;
			bool value;
			if (isSoleAccess != null)
			{
				value = isSoleAccess.Value;
			}
			else
			{
				bool? flag = this._isSoleAccess = new bool?(this.@interface is global::IContextRequestableSoleAccess);
				value = flag.Value;
			}
			return value;
		}
	}

	// Token: 0x170009C1 RID: 2497
	// (get) Token: 0x06002CF5 RID: 11509 RVA: 0x000A835C File Offset: 0x000A655C
	public bool isMenu
	{
		get
		{
			bool? isMenu = this._isMenu;
			bool value;
			if (isMenu != null)
			{
				value = isMenu.Value;
			}
			else
			{
				bool? flag = this._isMenu = new bool?(this.@interface is global::IContextRequestableMenu);
				value = flag.Value;
			}
			return value;
		}
	}

	// Token: 0x170009C2 RID: 2498
	// (get) Token: 0x06002CF6 RID: 11510 RVA: 0x000A83AC File Offset: 0x000A65AC
	public bool isQuick
	{
		get
		{
			bool? isQuick = this._isQuick;
			bool value;
			if (isQuick != null)
			{
				value = isQuick.Value;
			}
			else
			{
				bool? flag = this._isQuick = new bool?(this.@interface is global::IContextRequestableQuick);
				value = flag.Value;
			}
			return value;
		}
	}

	// Token: 0x06002CF7 RID: 11511 RVA: 0x000A83FC File Offset: 0x000A65FC
	public bool AsMenu(out global::IContextRequestableMenu menu)
	{
		if (this.isMenu)
		{
			menu = (this.@interface as global::IContextRequestableMenu);
			return this.implementor;
		}
		menu = null;
		return false;
	}

	// Token: 0x06002CF8 RID: 11512 RVA: 0x000A8434 File Offset: 0x000A6634
	public bool AsMenu<IContextRequestableMenuType>(out IContextRequestableMenuType menu) where IContextRequestableMenuType : class, global::IContextRequestableMenu
	{
		global::IContextRequestableMenu contextRequestableMenu;
		if (this.AsMenu(out contextRequestableMenu))
		{
			return !object.ReferenceEquals(menu = (contextRequestableMenu as IContextRequestableMenuType), null);
		}
		menu = (IContextRequestableMenuType)((object)null);
		return false;
	}

	// Token: 0x06002CF9 RID: 11513 RVA: 0x000A8480 File Offset: 0x000A6680
	public bool AsQuick(out global::IContextRequestableQuick quick)
	{
		if (this.isQuick)
		{
			quick = (this.@interface as global::IContextRequestableQuick);
			return this.implementor;
		}
		quick = null;
		return false;
	}

	// Token: 0x06002CFA RID: 11514 RVA: 0x000A84B8 File Offset: 0x000A66B8
	public bool AsQuick<IContextRequestableQuickType>(out IContextRequestableQuickType quick) where IContextRequestableQuickType : class, global::IContextRequestableQuick
	{
		global::IContextRequestableQuick contextRequestableQuick;
		if (this.AsQuick(out contextRequestableQuick))
		{
			return !object.ReferenceEquals(quick = (contextRequestableQuick as IContextRequestableQuickType), null);
		}
		quick = (IContextRequestableQuickType)((object)null);
		return false;
	}

	// Token: 0x170009C3 RID: 2499
	// (get) Token: 0x06002CFB RID: 11515 RVA: 0x000A8504 File Offset: 0x000A6704
	public MonoBehaviour implementor
	{
		get
		{
			if (!this._awoke)
			{
				try
				{
					this.Refresh();
				}
				finally
				{
					this._awoke = true;
				}
			}
			return this.implementation;
		}
	}

	// Token: 0x170009C4 RID: 2500
	// (get) Token: 0x06002CFC RID: 11516 RVA: 0x000A8554 File Offset: 0x000A6754
	public global::IContextRequestable @interface
	{
		get
		{
			if (!this._awoke)
			{
				try
				{
					this.Refresh();
				}
				finally
				{
					this._awoke = true;
				}
			}
			return this._requestable;
		}
	}

	// Token: 0x170009C5 RID: 2501
	// (get) Token: 0x06002CFD RID: 11517 RVA: 0x000A85A4 File Offset: 0x000A67A4
	public bool exists
	{
		get
		{
			if (!this._awoke)
			{
				try
				{
					this.Refresh();
				}
				finally
				{
					this._awoke = true;
				}
			}
			return this._implemented && (this._implemented = this.implementation);
		}
	}

	// Token: 0x170009C6 RID: 2502
	// (get) Token: 0x06002CFE RID: 11518 RVA: 0x000A8610 File Offset: 0x000A6810
	public global::Contextual driver
	{
		get
		{
			return this;
		}
	}

	// Token: 0x06002CFF RID: 11519 RVA: 0x000A8614 File Offset: 0x000A6814
	private void Refresh()
	{
		this.implementation = this._implementation;
		this._implementation = null;
		this._requestable = (this.implementation as global::IContextRequestable);
		this._implemented = (this._requestable != null);
		if (!this._implemented)
		{
			Debug.LogWarning("implementation is null or does not implement IContextRequestable", this);
		}
	}

	// Token: 0x06002D00 RID: 11520 RVA: 0x000A8670 File Offset: 0x000A6870
	public static bool FindUp(Transform transform, out global::Contextual contextual)
	{
		while (transform)
		{
			global::Contextual component;
			contextual = (component = transform.GetComponent<global::Contextual>());
			if (component)
			{
				return true;
			}
			transform = transform.parent;
		}
		contextual = null;
		return false;
	}

	// Token: 0x06002D01 RID: 11521 RVA: 0x000A86B0 File Offset: 0x000A68B0
	private static bool GetMB(MonoBehaviour networkView, out global::Contextual contextual)
	{
		if (networkView)
		{
			global::Contextual component;
			contextual = (component = networkView.GetComponent<global::Contextual>());
			if (component)
			{
				return contextual.exists;
			}
		}
		contextual = null;
		return false;
	}

	// Token: 0x06002D02 RID: 11522 RVA: 0x000A86EC File Offset: 0x000A68EC
	public static bool ContextOf(Facepunch.NetworkView networkView, out global::Contextual contextual)
	{
		return global::Contextual.GetMB(networkView, out contextual);
	}

	// Token: 0x06002D03 RID: 11523 RVA: 0x000A86F8 File Offset: 0x000A68F8
	public static bool ContextOf(global::NGCView networkView, out global::Contextual contextual)
	{
		return global::Contextual.GetMB(networkView, out contextual);
	}

	// Token: 0x06002D04 RID: 11524 RVA: 0x000A8704 File Offset: 0x000A6904
	public static bool ContextOf(uLink.NetworkViewID networkViewID, out global::Contextual contextual)
	{
		return global::Contextual.GetMB(Facepunch.NetworkView.Find(networkViewID), out contextual);
	}

	// Token: 0x06002D05 RID: 11525 RVA: 0x000A8714 File Offset: 0x000A6914
	public static bool ContextOf(global::NetEntityID entityID, out global::Contextual contextual)
	{
		return global::Contextual.GetMB(entityID.view, out contextual);
	}

	// Token: 0x06002D06 RID: 11526 RVA: 0x000A8724 File Offset: 0x000A6924
	public static bool ContextOf(GameObject gameObject, out global::Contextual contextual)
	{
		MonoBehaviour networkView;
		if ((int)global::NetEntityID.Of(gameObject, out networkView) == 0)
		{
			contextual = null;
			return false;
		}
		return global::Contextual.GetMB(networkView, out contextual);
	}

	// Token: 0x06002D07 RID: 11527 RVA: 0x000A874C File Offset: 0x000A694C
	public static bool ContextOf(Component component, out global::Contextual contextual)
	{
		MonoBehaviour networkView;
		if ((int)global::NetEntityID.Of(component, out networkView) == 0)
		{
			contextual = null;
			return false;
		}
		return global::Contextual.GetMB(networkView, out contextual);
	}

	// Token: 0x040016CE RID: 5838
	[SerializeField]
	private MonoBehaviour _implementation;

	// Token: 0x040016CF RID: 5839
	[NonSerialized]
	private MonoBehaviour implementation;

	// Token: 0x040016D0 RID: 5840
	[NonSerialized]
	private global::IContextRequestable _requestable;

	// Token: 0x040016D1 RID: 5841
	[NonSerialized]
	private bool _implemented;

	// Token: 0x040016D2 RID: 5842
	[NonSerialized]
	private bool _awoke;

	// Token: 0x040016D3 RID: 5843
	[NonSerialized]
	private bool? _isSoleAccess;

	// Token: 0x040016D4 RID: 5844
	[NonSerialized]
	private bool? _isMenu;

	// Token: 0x040016D5 RID: 5845
	[NonSerialized]
	private bool? _isQuick;
}
