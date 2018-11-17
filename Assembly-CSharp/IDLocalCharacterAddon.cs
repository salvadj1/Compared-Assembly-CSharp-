using System;
using System.Runtime.Serialization;
using UnityEngine;

// Token: 0x02000172 RID: 370
public abstract class IDLocalCharacterAddon : global::IDLocalCharacter
{
	// Token: 0x06000B34 RID: 2868 RVA: 0x0002C184 File Offset: 0x0002A384
	protected IDLocalCharacterAddon(global::IDLocalCharacterAddon.AddonFlags addonFlags)
	{
		this.addonFlags = addonFlags;
	}

	// Token: 0x06000B35 RID: 2869 RVA: 0x0002C194 File Offset: 0x0002A394
	protected virtual bool CheckPrerequesits()
	{
		throw new global::IDLocalCharacterAddon.BaseNoImplementationCalled("You should not call base.CheckPrerequesits. or define AddonFlags you do not use.");
	}

	// Token: 0x06000B36 RID: 2870 RVA: 0x0002C1A0 File Offset: 0x0002A3A0
	protected virtual void OnAddonAwake()
	{
		throw new global::IDLocalCharacterAddon.BaseNoImplementationCalled("You should not call base.OnAddonAwake. or define AddonFlags you do not use.");
	}

	// Token: 0x06000B37 RID: 2871 RVA: 0x0002C1AC File Offset: 0x0002A3AC
	protected virtual void OnAddonPostAwake()
	{
		throw new global::IDLocalCharacterAddon.BaseNoImplementationCalled("You should not call base.OnAddonPostAwake. or define AddonFlags you do not use.");
	}

	// Token: 0x06000B38 RID: 2872 RVA: 0x0002C1B8 File Offset: 0x0002A3B8
	protected virtual void OnWillRemoveAddon()
	{
		throw new global::IDLocalCharacterAddon.BaseNoImplementationCalled("You should not call base.OnWillRemoveAddon. or define AddonFlags you do not use.");
	}

	// Token: 0x06000B39 RID: 2873 RVA: 0x0002C1C4 File Offset: 0x0002A3C4
	internal byte InitializeAddon(global::Character idMain)
	{
		if (this.addonWasAdded)
		{
			return 0;
		}
		this.idMain = idMain;
		this.addonWasAdded = true;
		byte b = 0;
		if ((byte)(this.addonFlags & global::IDLocalCharacterAddon.AddonFlags.PrerequisitCheck) == 4)
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
				if (!(ex is global::IDLocalCharacterAddon.BaseNoImplementationCalled))
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
		if ((byte)(this.addonFlags & global::IDLocalCharacterAddon.AddonFlags.FireOnAddonPostAwake) == 2)
		{
			b |= 2;
		}
		if ((byte)(this.addonFlags & global::IDLocalCharacterAddon.AddonFlags.FireOnAddonAwake) == 1)
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

	// Token: 0x06000B3A RID: 2874 RVA: 0x0002C2B0 File Offset: 0x0002A4B0
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

	// Token: 0x06000B3B RID: 2875 RVA: 0x0002C2F4 File Offset: 0x0002A4F4
	internal void RemoveAddon()
	{
		if (!this.removingThisAddon)
		{
			this.removingThisAddon = true;
			if ((byte)(this.addonFlags & global::IDLocalCharacterAddon.AddonFlags.FireOnWillRemoveAddon) == 8)
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

	// Token: 0x0400078D RID: 1933
	internal const byte kInitializeAddonFlag_PostAwake = 2;

	// Token: 0x0400078E RID: 1934
	internal const byte kInitializeAddonFlag_Failed = 8;

	// Token: 0x0400078F RID: 1935
	[NonSerialized]
	private bool addonWasAdded;

	// Token: 0x04000790 RID: 1936
	[NonSerialized]
	private bool removingThisAddon;

	// Token: 0x04000791 RID: 1937
	private readonly global::IDLocalCharacterAddon.AddonFlags addonFlags;

	// Token: 0x02000173 RID: 371
	[Flags]
	protected internal enum AddonFlags : byte
	{
		// Token: 0x04000793 RID: 1939
		FireOnAddonAwake = 1,
		// Token: 0x04000794 RID: 1940
		FireOnAddonPostAwake = 2,
		// Token: 0x04000795 RID: 1941
		PrerequisitCheck = 4,
		// Token: 0x04000796 RID: 1942
		FireOnWillRemoveAddon = 8
	}

	// Token: 0x02000174 RID: 372
	[Serializable]
	private class BaseNoImplementationCalled : NotSupportedException
	{
		// Token: 0x06000B3C RID: 2876 RVA: 0x0002C35C File Offset: 0x0002A55C
		public BaseNoImplementationCalled()
		{
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x0002C364 File Offset: 0x0002A564
		public BaseNoImplementationCalled(string message) : base(message)
		{
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x0002C370 File Offset: 0x0002A570
		public BaseNoImplementationCalled(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0002C37C File Offset: 0x0002A57C
		protected BaseNoImplementationCalled(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
