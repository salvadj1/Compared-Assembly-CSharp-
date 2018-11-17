using System;
using UnityEngine;

// Token: 0x02000665 RID: 1637
public class ItemModRepresentation : MonoBehaviour
{
	// Token: 0x0600384A RID: 14410 RVA: 0x000C7C9C File Offset: 0x000C5E9C
	public ItemModRepresentation()
	{
		if (base.GetType() != typeof(global::ItemModRepresentation))
		{
			this.caps = (global::ItemModRepresentation.Caps.Initialize | global::ItemModRepresentation.Caps.BindStateFlags | global::ItemModRepresentation.Caps.Shutdown);
		}
		else
		{
			this.caps = (global::ItemModRepresentation.Caps)0;
		}
	}

	// Token: 0x0600384B RID: 14411 RVA: 0x000C7CD8 File Offset: 0x000C5ED8
	protected ItemModRepresentation(global::ItemModRepresentation.Caps caps)
	{
		this.caps = caps;
	}

	// Token: 0x17000ACA RID: 2762
	// (get) Token: 0x0600384C RID: 14412 RVA: 0x000C7CF0 File Offset: 0x000C5EF0
	public global::ItemRepresentation itemRep
	{
		get
		{
			return this._itemRep;
		}
	}

	// Token: 0x17000ACB RID: 2763
	// (get) Token: 0x0600384D RID: 14413 RVA: 0x000C7CF8 File Offset: 0x000C5EF8
	public global::HeldItemDataBlock itemDatablock
	{
		get
		{
			return (!this._itemRep) ? null : this._itemRep.datablock;
		}
	}

	// Token: 0x17000ACC RID: 2764
	// (get) Token: 0x0600384E RID: 14414 RVA: 0x000C7D1C File Offset: 0x000C5F1C
	public global::ItemModDataBlock modDataBlock
	{
		get
		{
			return this._itemRep._itemMods.ItemModDataBlock(this._modSlot);
		}
	}

	// Token: 0x17000ACD RID: 2765
	// (get) Token: 0x0600384F RID: 14415 RVA: 0x000C7D34 File Offset: 0x000C5F34
	public int modSlot
	{
		get
		{
			return this._modSlot;
		}
	}

	// Token: 0x17000ACE RID: 2766
	// (get) Token: 0x06003850 RID: 14416 RVA: 0x000C7D3C File Offset: 0x000C5F3C
	public bool initialized
	{
		get
		{
			return this._modSlot != -1;
		}
	}

	// Token: 0x17000ACF RID: 2767
	// (get) Token: 0x06003851 RID: 14417 RVA: 0x000C7D4C File Offset: 0x000C5F4C
	public bool destroyed
	{
		get
		{
			return this._modSlot == -2;
		}
	}

	// Token: 0x06003852 RID: 14418 RVA: 0x000C7D58 File Offset: 0x000C5F58
	internal void Initialize(global::ItemRepresentation itemRep, int modSlot, global::CharacterStateFlags flags)
	{
		if (this._modSlot == -1)
		{
			if (!itemRep)
			{
				throw new ArgumentOutOfRangeException("itemRep", itemRep, "!itemRep");
			}
			if (modSlot < 0 || modSlot >= 5)
			{
				throw new ArgumentOutOfRangeException("modSlot", modSlot, "modSlot<0||modSlot>=MAX_SUPPORTED_ITEM_MODS");
			}
			this._itemRep = itemRep;
			this._modSlot = modSlot;
			if ((byte)(this.caps & global::ItemModRepresentation.Caps.Initialize) == 1)
			{
				try
				{
					this.Initialize();
				}
				catch (Exception)
				{
					this._itemRep = null;
					this._modSlot = -1;
					throw;
				}
			}
			this.HandleChangedStateFlags(flags, false);
		}
		else
		{
			if (this._modSlot == -2)
			{
				throw new InvalidOperationException("This ItemModRepresentation has been destroyed");
			}
			if (itemRep != this._itemRep || (modSlot < 0 && modSlot < 5 && modSlot != this._modSlot))
			{
				throw new InvalidOperationException(string.Format("The ItemModRepresentation was already initialized with {{\"item\":\"{0}\",\"slot\":{1}}} and cannot be re-initialized to use {{\"item\":\"{2|\",\"slot\":{3}}}", new object[]
				{
					this._itemRep,
					this._modSlot,
					itemRep,
					modSlot
				}));
			}
		}
	}

	// Token: 0x06003853 RID: 14419 RVA: 0x000C7E98 File Offset: 0x000C6098
	internal void HandleChangedStateFlags(global::CharacterStateFlags flags, bool notFromLoading)
	{
		if ((byte)(this.caps & global::ItemModRepresentation.Caps.BindStateFlags) == 2 && (this._lastFlags == null || !this._lastFlags.Value.Equals(flags)))
		{
			this.BindStateFlags(flags, (!notFromLoading) ? global::ItemModRepresentation.Reason.Initialization : global::ItemModRepresentation.Reason.Explicit);
			this._lastFlags = new global::CharacterStateFlags?(flags);
		}
	}

	// Token: 0x06003854 RID: 14420 RVA: 0x000C7F00 File Offset: 0x000C6100
	[Obsolete("Do not use OnDestroy in implementing classes. Instead override Shutdown() and specify Caps.Shutdown in the constructor!")]
	private void OnDestroy()
	{
		if (this._modSlot != -2)
		{
			try
			{
				if (this._modSlot != -1)
				{
					if (this._itemRep)
					{
						try
						{
							if ((byte)(this.caps & global::ItemModRepresentation.Caps.Shutdown) == 128)
							{
								try
								{
									this.Shutdown();
								}
								catch (Exception ex)
								{
									Debug.LogError(ex, this);
								}
							}
							try
							{
								this._itemRep.ItemModRepresentationDestroyed(this);
							}
							catch (Exception ex2)
							{
								Debug.LogError(ex2, this);
							}
						}
						finally
						{
							this._itemRep = null;
						}
					}
					else
					{
						this._itemRep = null;
					}
				}
			}
			finally
			{
				this._modSlot = -2;
			}
		}
	}

	// Token: 0x06003855 RID: 14421 RVA: 0x000C8010 File Offset: 0x000C6210
	protected virtual void Initialize()
	{
	}

	// Token: 0x06003856 RID: 14422 RVA: 0x000C8014 File Offset: 0x000C6214
	protected virtual void BindStateFlags(global::CharacterStateFlags flags, global::ItemModRepresentation.Reason reason)
	{
	}

	// Token: 0x06003857 RID: 14423 RVA: 0x000C8018 File Offset: 0x000C6218
	protected virtual void Shutdown()
	{
	}

	// Token: 0x04001BFD RID: 7165
	protected const global::ItemModRepresentation.Caps kAllCaps = global::ItemModRepresentation.Caps.Initialize | global::ItemModRepresentation.Caps.BindStateFlags | global::ItemModRepresentation.Caps.Shutdown;

	// Token: 0x04001BFE RID: 7166
	protected const global::ItemModRepresentation.Caps kNoCaps = (global::ItemModRepresentation.Caps)0;

	// Token: 0x04001BFF RID: 7167
	private global::ItemRepresentation _itemRep;

	// Token: 0x04001C00 RID: 7168
	private int _modSlot = -1;

	// Token: 0x04001C01 RID: 7169
	[NonSerialized]
	public GameObject instantiatedThing;

	// Token: 0x04001C02 RID: 7170
	[NonSerialized]
	protected readonly global::ItemModRepresentation.Caps caps;

	// Token: 0x04001C03 RID: 7171
	private global::CharacterStateFlags? _lastFlags;

	// Token: 0x02000666 RID: 1638
	[Flags]
	protected enum Caps : byte
	{
		// Token: 0x04001C05 RID: 7173
		Initialize = 1,
		// Token: 0x04001C06 RID: 7174
		BindStateFlags = 2,
		// Token: 0x04001C07 RID: 7175
		Shutdown = 128
	}

	// Token: 0x02000667 RID: 1639
	protected enum Reason
	{
		// Token: 0x04001C09 RID: 7177
		Initialization,
		// Token: 0x04001C0A RID: 7178
		Implicit,
		// Token: 0x04001C0B RID: 7179
		Explicit
	}
}
