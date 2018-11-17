using System;
using Facepunch;
using UnityEngine;

// Token: 0x020001EB RID: 491
[InterfaceDriverComponent(typeof(IUseable), "_implementation", "implementation", SearchRoute = InterfaceSearchRoute.GameObject, UnityType = typeof(MonoBehaviour), AlwaysSaveDisabled = true)]
public sealed class Useable : MonoBehaviour, IComponentInterfaceDriver<IUseable, MonoBehaviour, Useable>
{
	// Token: 0x14000007 RID: 7
	// (add) Token: 0x06000D81 RID: 3457 RVA: 0x00034D68 File Offset: 0x00032F68
	// (remove) Token: 0x06000D82 RID: 3458 RVA: 0x00034D84 File Offset: 0x00032F84
	public event Useable.UseExitCallback onUseExited;

	// Token: 0x17000347 RID: 839
	// (get) Token: 0x06000D83 RID: 3459 RVA: 0x00034DA0 File Offset: 0x00032FA0
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

	// Token: 0x17000348 RID: 840
	// (get) Token: 0x06000D84 RID: 3460 RVA: 0x00034DF0 File Offset: 0x00032FF0
	public IUseable @interface
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
			return this.use;
		}
	}

	// Token: 0x17000349 RID: 841
	// (get) Token: 0x06000D85 RID: 3461 RVA: 0x00034E40 File Offset: 0x00033040
	public bool exists
	{
		get
		{
			return this._implemented && (this._implemented = this.implementation);
		}
	}

	// Token: 0x1700034A RID: 842
	// (get) Token: 0x06000D86 RID: 3462 RVA: 0x00034E70 File Offset: 0x00033070
	public Useable driver
	{
		get
		{
			return this;
		}
	}

	// Token: 0x1700034B RID: 843
	// (get) Token: 0x06000D87 RID: 3463 RVA: 0x00034E74 File Offset: 0x00033074
	public Character user
	{
		get
		{
			return this._user;
		}
	}

	// Token: 0x1700034C RID: 844
	// (get) Token: 0x06000D88 RID: 3464 RVA: 0x00034E7C File Offset: 0x0003307C
	public bool occupied
	{
		get
		{
			return this._user;
		}
	}

	// Token: 0x06000D89 RID: 3465 RVA: 0x00034E8C File Offset: 0x0003308C
	private void OnEnable()
	{
		Debug.LogError("Something is trying to enable useable on client.", this);
		base.enabled = false;
	}

	// Token: 0x06000D8A RID: 3466 RVA: 0x00034EA0 File Offset: 0x000330A0
	private void Refresh()
	{
		this.implementation = this._implementation;
		this._implementation = null;
		this.use = (this.implementation as IUseable);
		this.canUse = (this.use != null);
		if (this.canUse)
		{
			base.enabled = false;
			this.useDecline = null;
			this.useCheck = null;
			this.updateFlags = UseUpdateFlags.None;
			IUseableAwake useableAwake = this.implementation as IUseableAwake;
			if (useableAwake != null)
			{
				useableAwake.OnUseableAwake(this);
			}
		}
		else
		{
			Debug.LogWarning("implementation is null or does not implement IUseable", this);
		}
	}

	// Token: 0x06000D8B RID: 3467 RVA: 0x00034F34 File Offset: 0x00033134
	private void Awake()
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
	}

	// Token: 0x06000D8C RID: 3468 RVA: 0x00034F7C File Offset: 0x0003317C
	private void RunUpdate()
	{
		Useable.FunctionCallState functionCallState = this.callState;
		try
		{
			this.callState = Useable.FunctionCallState.OnUseUpdate;
			this.useUpdate.OnUseUpdate(this);
		}
		catch (Exception arg)
		{
			Debug.LogError("Inside OnUseUpdate\r\n" + arg, this.implementation);
		}
		finally
		{
			this.callState = functionCallState;
		}
	}

	// Token: 0x06000D8D RID: 3469 RVA: 0x00035000 File Offset: 0x00033200
	private void Update()
	{
		if (!this._user)
		{
			if ((this.updateFlags & UseUpdateFlags.UpdateWithoutUser) == UseUpdateFlags.UpdateWithoutUser)
			{
				if (this.implementation)
				{
					this.RunUpdate();
				}
				else
				{
					base.enabled = false;
				}
			}
			else
			{
				Debug.LogWarning("Most likely the user was destroyed without being set up properly!", this);
				base.enabled = false;
			}
		}
		else if (this.implementation)
		{
			this.RunUpdate();
		}
		else
		{
			base.enabled = false;
		}
	}

	// Token: 0x06000D8E RID: 3470 RVA: 0x00035090 File Offset: 0x00033290
	private static void EnsureServer()
	{
		throw new InvalidOperationException("A function ( Enter, Exit or Eject ) in Useable was called client side. Should have only been called server side.");
	}

	// Token: 0x06000D8F RID: 3471 RVA: 0x0003509C File Offset: 0x0003329C
	public UseResponse EnterFromElsewhere(Character attempt)
	{
		return this.Enter(attempt, UseEnterRequest.Elsewhere);
	}

	// Token: 0x06000D90 RID: 3472 RVA: 0x000350A8 File Offset: 0x000332A8
	public UseResponse EnterFromContext(Character attempt)
	{
		return this.Enter(attempt, UseEnterRequest.Context);
	}

	// Token: 0x06000D91 RID: 3473 RVA: 0x000350B4 File Offset: 0x000332B4
	private UseResponse Enter(Character attempt, UseEnterRequest request)
	{
		if (!this.canUse)
		{
			return UseResponse.Fail_NotIUseable;
		}
		Useable.EnsureServer();
		if ((int)this.callState != 0)
		{
			Debug.LogWarning("Some how Enter got called from a call stack originating with " + this.callState + " fix your script to not do this.", this);
			return UseResponse.Fail_InvalidOperation;
		}
		if (Useable.hasException)
		{
			Useable.ClearException(false);
		}
		if (!attempt)
		{
			return UseResponse.Fail_NullOrMissingUser;
		}
		if (attempt.signaledDeath)
		{
			return UseResponse.Fail_UserDead;
		}
		if (!this._user)
		{
			if (this.implementation)
			{
				try
				{
					this.callState = Useable.FunctionCallState.Enter;
					UseResponse useResponse;
					if (this.canCheck)
					{
						try
						{
							useResponse = (UseResponse)this.useCheck.CanUse(attempt, request);
						}
						catch (Exception ex)
						{
							Useable.lastException = ex;
							return UseResponse.Fail_CheckException;
						}
						if ((int)useResponse != 1)
						{
							if (useResponse.Succeeded())
							{
								Debug.LogError("A IUseableChecked return a invalid value that should have cause success [" + useResponse + "], but it was not UseCheck.Success! fix your script.", this.implementation);
								return UseResponse.Fail_Checked_BadResult;
							}
							if (this.wantDeclines)
							{
								try
								{
									this.useDecline.OnUseDeclined(attempt, useResponse, request);
								}
								catch (Exception ex2)
								{
									Debug.LogError(string.Concat(new object[]
									{
										"Caught exception in OnUseDeclined \r\n (response was ",
										useResponse,
										")",
										ex2
									}), this.implementation);
								}
							}
							return useResponse;
						}
					}
					else
					{
						useResponse = UseResponse.Pass_Unchecked;
					}
					try
					{
						this._user = attempt;
						this.use.OnUseEnter(this);
					}
					catch (Exception arg)
					{
						this._user = null;
						Debug.LogError("Exception thrown during Useable.Enter. Object not set as used!\r\n" + arg, attempt);
						Useable.lastException = arg;
						return UseResponse.Fail_EnterException;
					}
					if (useResponse.Succeeded())
					{
						this.LatchUse();
					}
					return useResponse;
				}
				finally
				{
					this.callState = Useable.FunctionCallState.None;
				}
				return UseResponse.Fail_Destroyed;
			}
			return UseResponse.Fail_Destroyed;
		}
		if (this._user == attempt)
		{
			if (this.wantDeclines && this.implementation)
			{
				try
				{
					this.useDecline.OnUseDeclined(attempt, UseResponse.Fail_Redundant, request);
				}
				catch (Exception arg2)
				{
					Debug.LogError("Caught exception in OnUseDeclined \r\n (response was Fail_Redundant)" + arg2, this.implementation);
				}
			}
			return UseResponse.Fail_Redundant;
		}
		if (this.wantDeclines && this.implementation)
		{
			try
			{
				this.useDecline.OnUseDeclined(attempt, UseResponse.Fail_Vacancy, request);
			}
			catch (Exception arg3)
			{
				Debug.LogError("Caught exception in OnUseDeclined \r\n (response was Fail_Vacancy)" + arg3, this.implementation);
			}
		}
		return UseResponse.Fail_Vacancy;
	}

	// Token: 0x06000D92 RID: 3474 RVA: 0x000353E0 File Offset: 0x000335E0
	public bool Exit(Character attempt)
	{
		Useable.EnsureServer();
		if ((int)this.callState != 0)
		{
			Debug.LogWarning("Some how Exit got called from a call stack originating with " + this.callState + " fix your script to not do this.", this);
			return false;
		}
		if (attempt == this._user && attempt)
		{
			try
			{
				if (this.implementation)
				{
					try
					{
						this.callState = Useable.FunctionCallState.Exit;
						this.use.OnUseExit(this, UseExitReason.Manual);
					}
					finally
					{
						this.InvokeUseExitCallback();
						this.callState = Useable.FunctionCallState.None;
					}
				}
				return true;
			}
			finally
			{
				this._user = null;
				this.UnlatchUse();
			}
			return false;
		}
		return false;
	}

	// Token: 0x06000D93 RID: 3475 RVA: 0x000354C4 File Offset: 0x000336C4
	private void InvokeUseExitCallback()
	{
		if (this.onUseExited != null)
		{
			this.onUseExited(this, (int)this.callState == 3);
		}
	}

	// Token: 0x06000D94 RID: 3476 RVA: 0x000354E8 File Offset: 0x000336E8
	public bool Eject()
	{
		Useable.EnsureServer();
		UseExitReason reason;
		if ((int)this.callState != 0)
		{
			if ((int)this.callState != 4)
			{
				Debug.LogWarning("Some how Eject got called from a call stack originating with " + this.callState + " fix your script to not do this.", this);
				return false;
			}
			reason = UseExitReason.Manual;
		}
		else
		{
			reason = ((!this.inDestroy) ? ((!this.inKillCallback) ? UseExitReason.Forced : UseExitReason.Killed) : UseExitReason.Destroy);
		}
		if (this._user)
		{
			try
			{
				if (this.implementation)
				{
					try
					{
						this.callState = Useable.FunctionCallState.Eject;
						this.use.OnUseExit(this, reason);
					}
					finally
					{
						try
						{
							this.InvokeUseExitCallback();
						}
						finally
						{
							this.callState = Useable.FunctionCallState.None;
						}
					}
				}
				else
				{
					Debug.LogError("The IUseable has been destroyed with a user on it. IUseable should ALWAYS call UseableUtility.OnDestroy within the script's OnDestroy message first thing! " + base.gameObject, this);
				}
				return true;
			}
			finally
			{
				this.UnlatchUse();
				this._user = null;
			}
			return false;
		}
		return false;
	}

	// Token: 0x06000D95 RID: 3477 RVA: 0x00035638 File Offset: 0x00033838
	private void KilledCallback(Character user, CharacterDeathSignalReason reason)
	{
		if (!user)
		{
			Debug.LogError("Somehow KilledCallback got a null", this);
		}
		if (user != this._user)
		{
			Debug.LogError("Some callback invoked kill callback on the Useable but it was not being used by it", user);
		}
		else
		{
			try
			{
				this.inKillCallback = true;
				if (!this.Eject())
				{
					Debug.LogWarning("Failure to eject??", this);
				}
			}
			catch (Exception arg)
			{
				Debug.LogError("Exception in Eject while inside a killed callback\r\n" + arg, user);
			}
			finally
			{
				this.inKillCallback = false;
			}
		}
	}

	// Token: 0x06000D96 RID: 3478 RVA: 0x000356F4 File Offset: 0x000338F4
	private void OnDestroy()
	{
		this.inDestroy = true;
		if (this._user)
		{
			this.Eject();
		}
		this.canCheck = false;
		this.canUpdate = false;
		this.canUse = false;
		this.wantDeclines = false;
		this.use = null;
		this.useUpdate = null;
		this.useCheck = null;
		this.useDecline = null;
	}

	// Token: 0x06000D97 RID: 3479 RVA: 0x00035758 File Offset: 0x00033958
	private void LatchUse()
	{
		this._user.signal_death += this.onDeathCallback;
		base.enabled = ((this.updateFlags & UseUpdateFlags.UpdateWithUser) == UseUpdateFlags.UpdateWithUser);
	}

	// Token: 0x06000D98 RID: 3480 RVA: 0x00035788 File Offset: 0x00033988
	private void UnlatchUse()
	{
		try
		{
			if (this._user)
			{
				this._user.signal_death -= this.onDeathCallback;
			}
		}
		catch (Exception arg)
		{
			Debug.LogError("Exception caught during unlatch\r\n" + arg, this);
		}
		finally
		{
			this._user = null;
		}
	}

	// Token: 0x06000D99 RID: 3481 RVA: 0x00035810 File Offset: 0x00033A10
	public static bool GetLastException<E>(out E exception, bool doNotClear) where E : Exception
	{
		if (Useable.hasException && Useable.lastException is E)
		{
			exception = (E)((object)Useable.lastException);
			if (!doNotClear)
			{
				Useable.ClearException(true);
			}
			return true;
		}
		exception = (E)((object)null);
		return false;
	}

	// Token: 0x06000D9A RID: 3482 RVA: 0x00035864 File Offset: 0x00033A64
	public static bool GetLastException<E>(out E exception) where E : Exception
	{
		return Useable.GetLastException<E>(out exception, false);
	}

	// Token: 0x06000D9B RID: 3483 RVA: 0x00035870 File Offset: 0x00033A70
	public static bool GetLastException(out Exception exception, bool doNotClear)
	{
		if (Useable.hasException)
		{
			exception = Useable.lastException;
			if (!doNotClear)
			{
				Useable.ClearException(true);
			}
			return true;
		}
		exception = null;
		return true;
	}

	// Token: 0x06000D9C RID: 3484 RVA: 0x00035898 File Offset: 0x00033A98
	public static bool GetLastException(out Exception exception)
	{
		return Useable.GetLastException(out exception, false);
	}

	// Token: 0x06000D9D RID: 3485 RVA: 0x000358A4 File Offset: 0x00033AA4
	private static void ClearException(bool got)
	{
		if (!got)
		{
			Debug.LogWarning("Nothing got previous now clearing exception \r\n" + Useable.lastException);
		}
		Useable.lastException = null;
		Useable.hasException = false;
	}

	// Token: 0x06000D9E RID: 3486 RVA: 0x000358D8 File Offset: 0x00033AD8
	private void Reset()
	{
		foreach (MonoBehaviour monoBehaviour in base.GetComponents<MonoBehaviour>())
		{
			if (monoBehaviour is IUseable)
			{
				this._implementation = monoBehaviour;
			}
		}
	}

	// Token: 0x04000835 RID: 2101
	[SerializeField]
	private MonoBehaviour _implementation;

	// Token: 0x04000836 RID: 2102
	[NonSerialized]
	private MonoBehaviour implementation;

	// Token: 0x04000837 RID: 2103
	[NonSerialized]
	private IUseable use;

	// Token: 0x04000838 RID: 2104
	[NonSerialized]
	private IUseableChecked useCheck;

	// Token: 0x04000839 RID: 2105
	[NonSerialized]
	private IUseableNotifyDecline useDecline;

	// Token: 0x0400083A RID: 2106
	[NonSerialized]
	private IUseableUpdated useUpdate;

	// Token: 0x0400083B RID: 2107
	[NonSerialized]
	private bool canUse;

	// Token: 0x0400083C RID: 2108
	[NonSerialized]
	private bool canCheck;

	// Token: 0x0400083D RID: 2109
	[NonSerialized]
	private bool wantDeclines;

	// Token: 0x0400083E RID: 2110
	[NonSerialized]
	private bool canUpdate;

	// Token: 0x0400083F RID: 2111
	[NonSerialized]
	private bool inKillCallback;

	// Token: 0x04000840 RID: 2112
	[NonSerialized]
	private bool inDestroy;

	// Token: 0x04000841 RID: 2113
	[NonSerialized]
	private bool _implemented;

	// Token: 0x04000842 RID: 2114
	[NonSerialized]
	private bool _awoke;

	// Token: 0x04000843 RID: 2115
	[NonSerialized]
	private UseUpdateFlags updateFlags;

	// Token: 0x04000844 RID: 2116
	[NonSerialized]
	private Character _user;

	// Token: 0x04000845 RID: 2117
	[NonSerialized]
	private CharacterDeathSignal onDeathCallback;

	// Token: 0x04000846 RID: 2118
	[NonSerialized]
	private Useable.FunctionCallState callState;

	// Token: 0x04000847 RID: 2119
	private static bool hasException;

	// Token: 0x04000848 RID: 2120
	private static Exception lastException;

	// Token: 0x020001EC RID: 492
	private enum FunctionCallState : sbyte
	{
		// Token: 0x0400084B RID: 2123
		None,
		// Token: 0x0400084C RID: 2124
		Enter,
		// Token: 0x0400084D RID: 2125
		Exit,
		// Token: 0x0400084E RID: 2126
		Eject,
		// Token: 0x0400084F RID: 2127
		OnUseUpdate
	}

	// Token: 0x02000860 RID: 2144
	// (Invoke) Token: 0x06004B58 RID: 19288
	public delegate void UseExitCallback(Useable useable, bool wasEjected);
}
