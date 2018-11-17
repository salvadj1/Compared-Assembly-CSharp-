using System;
using System.Runtime.Serialization;
using UnityEngine;

// Token: 0x02000148 RID: 328
public abstract class IDLocalCharacterAddon : IDLocalCharacter
{
	// Token: 0x06000A0E RID: 2574 RVA: 0x00028408 File Offset: 0x00026608
	protected IDLocalCharacterAddon(IDLocalCharacterAddon.AddonFlags addonFlags)
	{
		this.addonFlags = addonFlags;
	}

	// Token: 0x06000A0F RID: 2575 RVA: 0x00028418 File Offset: 0x00026618
	protected virtual bool CheckPrerequesits()
	{
		throw new IDLocalCharacterAddon.BaseNoImplementationCalled("You should not call base.CheckPrerequesits. or define AddonFlags you do not use.");
	}

	// Token: 0x06000A10 RID: 2576 RVA: 0x00028424 File Offset: 0x00026624
	protected virtual void OnAddonAwake()
	{
		throw new IDLocalCharacterAddon.BaseNoImplementationCalled("You should not call base.OnAddonAwake. or define AddonFlags you do not use.");
	}

	// Token: 0x06000A11 RID: 2577 RVA: 0x00028430 File Offset: 0x00026630
	protected virtual void OnAddonPostAwake()
	{
		throw new IDLocalCharacterAddon.BaseNoImplementationCalled("You should not call base.OnAddonPostAwake. or define AddonFlags you do not use.");
	}

	// Token: 0x06000A12 RID: 2578 RVA: 0x0002843C File Offset: 0x0002663C
	protected virtual void OnWillRemoveAddon()
	{
		throw new IDLocalCharacterAddon.BaseNoImplementationCalled("You should not call base.OnWillRemoveAddon. or define AddonFlags you do not use.");
	}

	// Token: 0x06000A13 RID: 2579 RVA: 0x00028448 File Offset: 0x00026648
	internal byte InitializeAddon(Character idMain)
	{
		if (this.addonWasAdded)
		{
			return 0;
		}
		this.idMain = idMain;
		this.addonWasAdded = true;
		byte b = 0;
		if ((byte)(this.addonFlags & IDLocalCharacterAddon.AddonFlags.PrerequisitCheck) == 4)
		{
			try
			{
				if (!this.CheckPrerequesits())
				{
					b |= 8;
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex, this);
				if (!(ex is IDLocalCharacterAddon.BaseNoImplementationCalled))
				{
					b |= 8;
				}
			}
		}
		if ((b & 8) == 8)
		{
			Object.Destroy(this);
			return b;
		}
		if ((byte)(this.addonFlags & IDLocalCharacterAddon.AddonFlags.FireOnAddonPostAwake) == 2)
		{
			b |= 2;
		}
		if ((byte)(this.addonFlags & IDLocalCharacterAddon.AddonFlags.FireOnAddonAwake) == 1)
		{
			try
			{
				this.OnAddonAwake();
			}
			catch (Exception ex2)
			{
				Debug.Log(ex2, this);
			}
		}
		return b;
	}

	// Token: 0x06000A14 RID: 2580 RVA: 0x00028534 File Offset: 0x00026734
	internal void PostInitializeAddon()
	{
		try
		{
			this.OnAddonPostAwake();
		}
		catch (Exception ex)
		{
			Debug.Log(ex, this);
		}
	}

	// Token: 0x06000A15 RID: 2581 RVA: 0x00028578 File Offset: 0x00026778
	internal void RemoveAddon()
	{
		if (!this.removingThisAddon)
		{
			this.removingThisAddon = true;
			if ((byte)(this.addonFlags & IDLocalCharacterAddon.AddonFlags.FireOnWillRemoveAddon) == 8)
			{
				try
				{
					this.OnWillRemoveAddon();
				}
				catch (Exception ex)
				{
					Debug.LogError(ex, this);
				}
			}
			Object.Destroy(this);
		}
	}

	// Token: 0x0400067E RID: 1662
	internal const byte kInitializeAddonFlag_PostAwake = 2;

	// Token: 0x0400067F RID: 1663
	internal const byte kInitializeAddonFlag_Failed = 8;

	// Token: 0x04000680 RID: 1664
	[NonSerialized]
	private bool addonWasAdded;

	// Token: 0x04000681 RID: 1665
	[NonSerialized]
	private bool removingThisAddon;

	// Token: 0x04000682 RID: 1666
	private readonly IDLocalCharacterAddon.AddonFlags addonFlags;

	// Token: 0x02000149 RID: 329
	[Flags]
	protected internal enum AddonFlags : byte
	{
		// Token: 0x04000684 RID: 1668
		FireOnAddonAwake = 1,
		// Token: 0x04000685 RID: 1669
		FireOnAddonPostAwake = 2,
		// Token: 0x04000686 RID: 1670
		PrerequisitCheck = 4,
		// Token: 0x04000687 RID: 1671
		FireOnWillRemoveAddon = 8
	}

	// Token: 0x0200014A RID: 330
	[Serializable]
	private class BaseNoImplementationCalled : NotSupportedException
	{
		// Token: 0x06000A16 RID: 2582 RVA: 0x000285E0 File Offset: 0x000267E0
		public BaseNoImplementationCalled()
		{
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x000285E8 File Offset: 0x000267E8
		public BaseNoImplementationCalled(string message) : base(message)
		{
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x000285F4 File Offset: 0x000267F4
		public BaseNoImplementationCalled(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x00028600 File Offset: 0x00026800
		protected BaseNoImplementationCalled(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
