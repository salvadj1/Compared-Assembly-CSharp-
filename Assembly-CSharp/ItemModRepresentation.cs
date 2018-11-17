using System;
using UnityEngine;

// Token: 0x020005A7 RID: 1447
public class ItemModRepresentation : MonoBehaviour
{
	// Token: 0x06003482 RID: 13442 RVA: 0x000BFA40 File Offset: 0x000BDC40
	public ItemModRepresentation()
	{
		if (base.GetType() != typeof(ItemModRepresentation))
		{
			this.caps = (ItemModRepresentation.Caps.Initialize | ItemModRepresentation.Caps.BindStateFlags | ItemModRepresentation.Caps.Shutdown);
		}
		else
		{
			this.caps = (ItemModRepresentation.Caps)0;
		}
	}

	// Token: 0x06003483 RID: 13443 RVA: 0x000BFA7C File Offset: 0x000BDC7C
	protected ItemModRepresentation(ItemModRepresentation.Caps caps)
	{
		this.caps = caps;
	}

	// Token: 0x17000A54 RID: 2644
	// (get) Token: 0x06003484 RID: 13444 RVA: 0x000BFA94 File Offset: 0x000BDC94
	public ItemRepresentation itemRep
	{
		get
		{
			return this._itemRep;
		}
	}

	// Token: 0x17000A55 RID: 2645
	// (get) Token: 0x06003485 RID: 13445 RVA: 0x000BFA9C File Offset: 0x000BDC9C
	public HeldItemDataBlock itemDatablock
	{
		get
		{
			return (!this._itemRep) ? null : this._itemRep.datablock;
		}
	}

	// Token: 0x17000A56 RID: 2646
	// (get) Token: 0x06003486 RID: 13446 RVA: 0x000BFAC0 File Offset: 0x000BDCC0
	public ItemModDataBlock modDataBlock
	{
		get
		{
			return this._itemRep._itemMods.ItemModDataBlock(this._modSlot);
		}
	}

	// Token: 0x17000A57 RID: 2647
	// (get) Token: 0x06003487 RID: 13447 RVA: 0x000BFAD8 File Offset: 0x000BDCD8
	public int modSlot
	{
		get
		{
			return this._modSlot;
		}
	}

	// Token: 0x17000A58 RID: 2648
	// (get) Token: 0x06003488 RID: 13448 RVA: 0x000BFAE0 File Offset: 0x000BDCE0
	public bool initialized
	{
		get
		{
			return this._modSlot != -1;
		}
	}

	// Token: 0x17000A59 RID: 2649
	// (get) Token: 0x06003489 RID: 13449 RVA: 0x000BFAF0 File Offset: 0x000BDCF0
	public bool destroyed
	{
		get
		{
			return this._modSlot == -2;
		}
	}

	// Token: 0x0600348A RID: 13450 RVA: 0x000BFAFC File Offset: 0x000BDCFC
	internal void Initialize(ItemRepresentation itemRep, int modSlot, CharacterStateFlags flags)
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
			if ((byte)(this.caps & ItemModRepresentation.Caps.Initialize) == 1)
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

	// Token: 0x0600348B RID: 13451 RVA: 0x000BFC3C File Offset: 0x000BDE3C
	internal void HandleChangedStateFlags(CharacterStateFlags flags, bool notFromLoading)
	{
		if ((byte)(this.caps & ItemModRepresentation.Caps.BindStateFlags) == 2 && (this._lastFlags == null || !this._lastFlags.Value.Equals(flags)))
		{
			this.BindStateFlags(flags, (!notFromLoading) ? ItemModRepresentation.Reason.Initialization : ItemModRepresentation.Reason.Explicit);
			this._lastFlags = new CharacterStateFlags?(flags);
		}
	}

	// Token: 0x0600348C RID: 13452 RVA: 0x000BFCA4 File Offset: 0x000BDEA4
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
							if ((byte)(this.caps & ItemModRepresentation.Caps.Shutdown) == 128)
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

	// Token: 0x0600348D RID: 13453 RVA: 0x000BFDB4 File Offset: 0x000BDFB4
	protected virtual void Initialize()
	{
	}

	// Token: 0x0600348E RID: 13454 RVA: 0x000BFDB8 File Offset: 0x000BDFB8
	protected virtual void BindStateFlags(CharacterStateFlags flags, ItemModRepresentation.Reason reason)
	{
	}

	// Token: 0x0600348F RID: 13455 RVA: 0x000BFDBC File Offset: 0x000BDFBC
	protected virtual void Shutdown()
	{
	}

	// Token: 0x04001A2C RID: 6700
	protected const ItemModRepresentation.Caps kAllCaps = ItemModRepresentation.Caps.Initialize | ItemModRepresentation.Caps.BindStateFlags | ItemModRepresentation.Caps.Shutdown;

	// Token: 0x04001A2D RID: 6701
	protected const ItemModRepresentation.Caps kNoCaps = (ItemModRepresentation.Caps)0;

	// Token: 0x04001A2E RID: 6702
	private ItemRepresentation _itemRep;

	// Token: 0x04001A2F RID: 6703
	private int _modSlot = -1;

	// Token: 0x04001A30 RID: 6704
	[NonSerialized]
	public GameObject instantiatedThing;

	// Token: 0x04001A31 RID: 6705
	[NonSerialized]
	protected readonly ItemModRepresentation.Caps caps;

	// Token: 0x04001A32 RID: 6706
	private CharacterStateFlags? _lastFlags;

	// Token: 0x020005A8 RID: 1448
	[Flags]
	protected enum Caps : byte
	{
		// Token: 0x04001A34 RID: 6708
		Initialize = 1,
		// Token: 0x04001A35 RID: 6709
		BindStateFlags = 2,
		// Token: 0x04001A36 RID: 6710
		Shutdown = 128
	}

	// Token: 0x020005A9 RID: 1449
	protected enum Reason
	{
		// Token: 0x04001A38 RID: 6712
		Initialization,
		// Token: 0x04001A39 RID: 6713
		Implicit,
		// Token: 0x04001A3A RID: 6714
		Explicit
	}
}
