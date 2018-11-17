using System;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x02000487 RID: 1159
[InterfaceDriverComponent(typeof(IContextRequestable), "_implementation", "implementation", SearchRoute = InterfaceSearchRoute.GameObject, UnityType = typeof(MonoBehaviour), AlwaysSaveDisabled = true)]
public sealed class Contextual : MonoBehaviour, IComponentInterfaceDriver<IContextRequestable, MonoBehaviour, Contextual>
{
	// Token: 0x17000950 RID: 2384
	// (get) Token: 0x06002942 RID: 10562 RVA: 0x000A1F10 File Offset: 0x000A0110
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
				bool? flag = this._isSoleAccess = new bool?(this.@interface is IContextRequestableSoleAccess);
				value = flag.Value;
			}
			return value;
		}
	}

	// Token: 0x17000951 RID: 2385
	// (get) Token: 0x06002943 RID: 10563 RVA: 0x000A1F60 File Offset: 0x000A0160
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
				bool? flag = this._isMenu = new bool?(this.@interface is IContextRequestableMenu);
				value = flag.Value;
			}
			return value;
		}
	}

	// Token: 0x17000952 RID: 2386
	// (get) Token: 0x06002944 RID: 10564 RVA: 0x000A1FB0 File Offset: 0x000A01B0
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
				bool? flag = this._isQuick = new bool?(this.@interface is IContextRequestableQuick);
				value = flag.Value;
			}
			return value;
		}
	}

	// Token: 0x06002945 RID: 10565 RVA: 0x000A2000 File Offset: 0x000A0200
	public bool AsMenu(out IContextRequestableMenu menu)
	{
		if (this.isMenu)
		{
			menu = (this.@interface as IContextRequestableMenu);
			return this.implementor;
		}
		menu = null;
		return false;
	}

	// Token: 0x06002946 RID: 10566 RVA: 0x000A2038 File Offset: 0x000A0238
	public bool AsMenu<IContextRequestableMenuType>(out IContextRequestableMenuType menu) where IContextRequestableMenuType : class, IContextRequestableMenu
	{
		IContextRequestableMenu contextRequestableMenu;
		if (this.AsMenu(out contextRequestableMenu))
		{
			return !object.ReferenceEquals(menu = (contextRequestableMenu as IContextRequestableMenuType), null);
		}
		menu = (IContextRequestableMenuType)((object)null);
		return false;
	}

	// Token: 0x06002947 RID: 10567 RVA: 0x000A2084 File Offset: 0x000A0284
	public bool AsQuick(out IContextRequestableQuick quick)
	{
		if (this.isQuick)
		{
			quick = (this.@interface as IContextRequestableQuick);
			return this.implementor;
		}
		quick = null;
		return false;
	}

	// Token: 0x06002948 RID: 10568 RVA: 0x000A20BC File Offset: 0x000A02BC
	public bool AsQuick<IContextRequestableQuickType>(out IContextRequestableQuickType quick) where IContextRequestableQuickType : class, IContextRequestableQuick
	{
		IContextRequestableQuick contextRequestableQuick;
		if (this.AsQuick(out contextRequestableQuick))
		{
			return !object.ReferenceEquals(quick = (contextRequestableQuick as IContextRequestableQuickType), null);
		}
		quick = (IContextRequestableQuickType)((object)null);
		return false;
	}

	// Token: 0x17000953 RID: 2387
	// (get) Token: 0x06002949 RID: 10569 RVA: 0x000A2108 File Offset: 0x000A0308
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

	// Token: 0x17000954 RID: 2388
	// (get) Token: 0x0600294A RID: 10570 RVA: 0x000A2158 File Offset: 0x000A0358
	public IContextRequestable @interface
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

	// Token: 0x17000955 RID: 2389
	// (get) Token: 0x0600294B RID: 10571 RVA: 0x000A21A8 File Offset: 0x000A03A8
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

	// Token: 0x17000956 RID: 2390
	// (get) Token: 0x0600294C RID: 10572 RVA: 0x000A2214 File Offset: 0x000A0414
	public Contextual driver
	{
		get
		{
			return this;
		}
	}

	// Token: 0x0600294D RID: 10573 RVA: 0x000A2218 File Offset: 0x000A0418
	private void Refresh()
	{
		this.implementation = this._implementation;
		this._implementation = null;
		this._requestable = (this.implementation as IContextRequestable);
		this._implemented = (this._requestable != null);
		if (!this._implemented)
		{
			Debug.LogWarning("implementation is null or does not implement IContextRequestable", this);
		}
	}

	// Token: 0x0600294E RID: 10574 RVA: 0x000A2274 File Offset: 0x000A0474
	public static bool FindUp(Transform transform, out Contextual contextual)
	{
		while (transform)
		{
			Contextual component;
			contextual = (component = transform.GetComponent<Contextual>());
			if (component)
			{
				return true;
			}
			transform = transform.parent;
		}
		contextual = null;
		return false;
	}

	// Token: 0x0600294F RID: 10575 RVA: 0x000A22B4 File Offset: 0x000A04B4
	private static bool GetMB(MonoBehaviour networkView, out Contextual contextual)
	{
		if (networkView)
		{
			Contextual component;
			contextual = (component = networkView.GetComponent<Contextual>());
			if (component)
			{
				return contextual.exists;
			}
		}
		contextual = null;
		return false;
	}

	// Token: 0x06002950 RID: 10576 RVA: 0x000A22F0 File Offset: 0x000A04F0
	public static bool ContextOf(NetworkView networkView, out Contextual contextual)
	{
		return Contextual.GetMB(networkView, out contextual);
	}

	// Token: 0x06002951 RID: 10577 RVA: 0x000A22FC File Offset: 0x000A04FC
	public static bool ContextOf(NGCView networkView, out Contextual contextual)
	{
		return Contextual.GetMB(networkView, out contextual);
	}

	// Token: 0x06002952 RID: 10578 RVA: 0x000A2308 File Offset: 0x000A0508
	public static bool ContextOf(NetworkViewID networkViewID, out Contextual contextual)
	{
		return Contextual.GetMB(NetworkView.Find(networkViewID), out contextual);
	}

	// Token: 0x06002953 RID: 10579 RVA: 0x000A2318 File Offset: 0x000A0518
	public static bool ContextOf(NetEntityID entityID, out Contextual contextual)
	{
		return Contextual.GetMB(entityID.view, out contextual);
	}

	// Token: 0x06002954 RID: 10580 RVA: 0x000A2328 File Offset: 0x000A0528
	public static bool ContextOf(GameObject gameObject, out Contextual contextual)
	{
		MonoBehaviour networkView;
		if ((int)NetEntityID.Of(gameObject, out networkView) == 0)
		{
			contextual = null;
			return false;
		}
		return Contextual.GetMB(networkView, out contextual);
	}

	// Token: 0x06002955 RID: 10581 RVA: 0x000A2350 File Offset: 0x000A0550
	public static bool ContextOf(Component component, out Contextual contextual)
	{
		MonoBehaviour networkView;
		if ((int)NetEntityID.Of(component, out networkView) == 0)
		{
			contextual = null;
			return false;
		}
		return Contextual.GetMB(networkView, out contextual);
	}

	// Token: 0x04001538 RID: 5432
	[SerializeField]
	private MonoBehaviour _implementation;

	// Token: 0x04001539 RID: 5433
	[NonSerialized]
	private MonoBehaviour implementation;

	// Token: 0x0400153A RID: 5434
	[NonSerialized]
	private IContextRequestable _requestable;

	// Token: 0x0400153B RID: 5435
	[NonSerialized]
	private bool _implemented;

	// Token: 0x0400153C RID: 5436
	[NonSerialized]
	private bool _awoke;

	// Token: 0x0400153D RID: 5437
	[NonSerialized]
	private bool? _isSoleAccess;

	// Token: 0x0400153E RID: 5438
	[NonSerialized]
	private bool? _isMenu;

	// Token: 0x0400153F RID: 5439
	[NonSerialized]
	private bool? _isQuick;
}
