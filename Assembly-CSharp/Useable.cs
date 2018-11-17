using System;
using Facepunch;
using UnityEngine;

// Token: 0x0200021D RID: 541
[global::InterfaceDriverComponent(typeof(global::IUseable), "_implementation", "implementation", SearchRoute = global::InterfaceSearchRoute.GameObject, UnityType = typeof(MonoBehaviour), AlwaysSaveDisabled = true)]
public sealed class Useable : MonoBehaviour, global::IComponentInterfaceDriver<global::IUseable, MonoBehaviour, global::Useable>
{
	// Token: 0x14000007 RID: 7
	// (add) Token: 0x06000ED1 RID: 3793 RVA: 0x00039110 File Offset: 0x00037310
	// (remove) Token: 0x06000ED2 RID: 3794 RVA: 0x0003912C File Offset: 0x0003732C
	public event global::Useable.UseExitCallback onUseExited;

	// Token: 0x1700038F RID: 911
	// (get) Token: 0x06000ED3 RID: 3795 RVA: 0x00039148 File Offset: 0x00037348
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

	// Token: 0x17000390 RID: 912
	// (get) Token: 0x06000ED4 RID: 3796 RVA: 0x00039198 File Offset: 0x00037398
	public global::IUseable @interface
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

	// Token: 0x17000391 RID: 913
	// (get) Token: 0x06000ED5 RID: 3797 RVA: 0x000391E8 File Offset: 0x000373E8
	public bool exists
	{
		get
		{
			return this._implemented && (this._implemented = this.implementation);
		}
	}

	// Token: 0x17000392 RID: 914
	// (get) Token: 0x06000ED6 RID: 3798 RVA: 0x00039218 File Offset: 0x00037418
	public global::Useable driver
	{
		get
		{
			return this;
		}
	}

	// Token: 0x17000393 RID: 915
	// (get) Token: 0x06000ED7 RID: 3799 RVA: 0x0003921C File Offset: 0x0003741C
	public global::Character user
	{
		get
		{
			return this._user;
		}
	}

	// Token: 0x17000394 RID: 916
	// (get) Token: 0x06000ED8 RID: 3800 RVA: 0x00039224 File Offset: 0x00037424
	public bool occupied
	{
		get
		{
			return this._user;
		}
	}

	// Token: 0x06000ED9 RID: 3801 RVA: 0x00039234 File Offset: 0x00037434
	private void OnEnable()
	{
		Debug.LogError("Something is trying to enable useable on client.", this);
		base.enabled = false;
	}

	// Token: 0x06000EDA RID: 3802 RVA: 0x00039248 File Offset: 0x00037448
	private void Refresh()
	{
		this.implementation = this._implementation;
		this._implementation = null;
		this.use = (this.implementation as global::IUseable);
		this.canUse = (this.use != null);
		if (this.canUse)
		{
			base.enabled = false;
			this.useDecline = null;
			this.useCheck = null;
			this.updateFlags = global::UseUpdateFlags.None;
			global::IUseableAwake useableAwake = this.implementation as global::IUseableAwake;
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

	// Token: 0x06000EDB RID: 3803 RVA: 0x000392DC File Offset: 0x000374DC
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

	// Token: 0x06000EDC RID: 3804 RVA: 0x00039324 File Offset: 0x00037524
	private void RunUpdate()
	{
		global::Useable.FunctionCallState functionCallState = this.callState;
		try
		{
			this.callState = global::Useable.FunctionCallState.OnUseUpdate;
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

	// Token: 0x06000EDD RID: 3805 RVA: 0x000393A8 File Offset: 0x000375A8
	private void Update()
	{
		if (!this._user)
		{
			if ((this.updateFlags & global::UseUpdateFlags.UpdateWithoutUser) == global::UseUpdateFlags.UpdateWithoutUser)
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

	// Token: 0x06000EDE RID: 3806 RVA: 0x00039438 File Offset: 0x00037638
	private static void EnsureServer()
	{
		throw new InvalidOperationException("A function ( Enter, Exit or Eject ) in Useable was called client side. Should have only been called server side.");
	}

	// Token: 0x06000EDF RID: 3807 RVA: 0x00039444 File Offset: 0x00037644
	public global::UseResponse EnterFromElsewhere(global::Character attempt)
	{
		return this.Enter(attempt, global::UseEnterRequest.Elsewhere);
	}

	// Token: 0x06000EE0 RID: 3808 RVA: 0x00039450 File Offset: 0x00037650
	public global::UseResponse EnterFromContext(global::Character attempt)
	{
		return this.Enter(attempt, global::UseEnterRequest.Context);
	}

	// Token: 0x06000EE1 RID: 3809 RVA: 0x0003945C File Offset: 0x0003765C
	private global::UseResponse Enter(global::Character attempt, global::UseEnterRequest request)
	{
		if (!this.canUse)
		{
			return global::UseResponse.Fail_NotIUseable;
		}
		global::Useable.EnsureServer();
		if ((int)this.callState != 0)
		{
			Debug.LogWarning("Some how Enter got called from a call stack originating with " + this.callState + " fix your script to not do this.", this);
			return global::UseResponse.Fail_InvalidOperation;
		}
		if (global::Useable.hasException)
		{
			global::Useable.ClearException(false);
		}
		if (!attempt)
		{
			return global::UseResponse.Fail_NullOrMissingUser;
		}
		if (attempt.signaledDeath)
		{
			return global::UseResponse.Fail_UserDead;
		}
		if (!this._user)
		{
			if (this.implementation)
			{
				try
				{
					this.callState = global::Useable.FunctionCallState.Enter;
					global::UseResponse useResponse;
					if (this.canCheck)
					{
						try
						{
							useResponse = (global::UseResponse)this.useCheck.CanUse(attempt, request);
						}
						catch (Exception ex)
						{
							global::Useable.lastException = ex;
							return global::UseResponse.Fail_CheckException;
						}
						if ((int)useResponse != 1)
						{
							if (useResponse.Succeeded())
							{
								Debug.LogError("A IUseableChecked return a invalid value that should have cause success [" + useResponse + "], but it was not UseCheck.Success! fix your script.", this.implementation);
								return global::UseResponse.Fail_Checked_BadResult;
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
						useResponse = global::UseResponse.Pass_Unchecked;
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
						global::Useable.lastException = arg;
						return global::UseResponse.Fail_EnterException;
					}
					if (useResponse.Succeeded())
					{
						this.LatchUse();
					}
					return useResponse;
				}
				finally
				{
					this.callState = global::Useable.FunctionCallState.None;
				}
				return global::UseResponse.Fail_Destroyed;
			}
			return global::UseResponse.Fail_Destroyed;
		}
		if (this._user == attempt)
		{
			if (this.wantDeclines && this.implementation)
			{
				try
				{
					this.useDecline.OnUseDeclined(attempt, global::UseResponse.Fail_Redundant, request);
				}
				catch (Exception arg2)
				{
					Debug.LogError("Caught exception in OnUseDeclined \r\n (response was Fail_Redundant)" + arg2, this.implementation);
				}
			}
			return global::UseResponse.Fail_Redundant;
		}
		if (this.wantDeclines && this.implementation)
		{
			try
			{
				this.useDecline.OnUseDeclined(attempt, global::UseResponse.Fail_Vacancy, request);
			}
			catch (Exception arg3)
			{
				Debug.LogError("Caught exception in OnUseDeclined \r\n (response was Fail_Vacancy)" + arg3, this.implementation);
			}
		}
		return global::UseResponse.Fail_Vacancy;
	}

	// Token: 0x06000EE2 RID: 3810 RVA: 0x00039788 File Offset: 0x00037988
	public bool Exit(global::Character attempt)
	{
		global::Useable.EnsureServer();
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
						this.callState = global::Useable.FunctionCallState.Exit;
						this.use.OnUseExit(this, global::UseExitReason.Manual);
					}
					finally
					{
						this.InvokeUseExitCallback();
						this.callState = global::Useable.FunctionCallState.None;
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

	// Token: 0x06000EE3 RID: 3811 RVA: 0x0003986C File Offset: 0x00037A6C
	private void InvokeUseExitCallback()
	{
		if (this.onUseExited != null)
		{
			this.onUseExited(this, (int)this.callState == 3);
		}
	}

	// Token: 0x06000EE4 RID: 3812 RVA: 0x00039890 File Offset: 0x00037A90
	public bool Eject()
	{
		global::Useable.EnsureServer();
		global::UseExitReason reason;
		if ((int)this.callState != 0)
		{
			if ((int)this.callState != 4)
			{
				Debug.LogWarning("Some how Eject got called from a call stack originating with " + this.callState + " fix your script to not do this.", this);
				return false;
			}
			reason = global::UseExitReason.Manual;
		}
		else
		{
			reason = ((!this.inDestroy) ? ((!this.inKillCallback) ? global::UseExitReason.Forced : global::UseExitReason.Killed) : global::UseExitReason.Destroy);
		}
		if (this._user)
		{
			try
			{
				if (this.implementation)
				{
					try
					{
						this.callState = global::Useable.FunctionCallState.Eject;
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
							this.callState = global::Useable.FunctionCallState.None;
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

	// Token: 0x06000EE5 RID: 3813 RVA: 0x000399E0 File Offset: 0x00037BE0
	private void KilledCallback(global::Character user, global::CharacterDeathSignalReason reason)
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

	// Token: 0x06000EE6 RID: 3814 RVA: 0x00039A9C File Offset: 0x00037C9C
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

	// Token: 0x06000EE7 RID: 3815 RVA: 0x00039B00 File Offset: 0x00037D00
	private void LatchUse()
	{
		this._user.signal_death += this.onDeathCallback;
		base.enabled = ((this.updateFlags & global::UseUpdateFlags.UpdateWithUser) == global::UseUpdateFlags.UpdateWithUser);
	}

	// Token: 0x06000EE8 RID: 3816 RVA: 0x00039B30 File Offset: 0x00037D30
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

	// Token: 0x06000EE9 RID: 3817 RVA: 0x00039BB8 File Offset: 0x00037DB8
	public static bool GetLastException<E>(out E exception, bool doNotClear) where E : Exception
	{
		if (global::Useable.hasException && global::Useable.lastException is E)
		{
			exception = (E)((object)global::Useable.lastException);
			if (!doNotClear)
			{
				global::Useable.ClearException(true);
			}
			return true;
		}
		exception = (E)((object)null);
		return false;
	}

	// Token: 0x06000EEA RID: 3818 RVA: 0x00039C0C File Offset: 0x00037E0C
	public static bool GetLastException<E>(out E exception) where E : Exception
	{
		return global::Useable.GetLastException<E>(out exception, false);
	}

	// Token: 0x06000EEB RID: 3819 RVA: 0x00039C18 File Offset: 0x00037E18
	public static bool GetLastException(out Exception exception, bool doNotClear)
	{
		if (global::Useable.hasException)
		{
			exception = global::Useable.lastException;
			if (!doNotClear)
			{
				global::Useable.ClearException(true);
			}
			return true;
		}
		exception = null;
		return true;
	}

	// Token: 0x06000EEC RID: 3820 RVA: 0x00039C40 File Offset: 0x00037E40
	public static bool GetLastException(out Exception exception)
	{
		return global::Useable.GetLastException(out exception, false);
	}

	// Token: 0x06000EED RID: 3821 RVA: 0x00039C4C File Offset: 0x00037E4C
	private static void ClearException(bool got)
	{
		if (!got)
		{
			Debug.LogWarning("Nothing got previous now clearing exception \r\n" + global::Useable.lastException);
		}
		global::Useable.lastException = null;
		global::Useable.hasException = false;
	}

	// Token: 0x06000EEE RID: 3822 RVA: 0x00039C80 File Offset: 0x00037E80
	private void Reset()
	{
		foreach (MonoBehaviour monoBehaviour in base.GetComponents<MonoBehaviour>())
		{
			if (monoBehaviour is global::IUseable)
			{
				this._implementation = monoBehaviour;
			}
		}
	}

	// Token: 0x04000958 RID: 2392
	[SerializeField]
	private MonoBehaviour _implementation;

	// Token: 0x04000959 RID: 2393
	[NonSerialized]
	private MonoBehaviour implementation;

	// Token: 0x0400095A RID: 2394
	[NonSerialized]
	private global::IUseable use;

	// Token: 0x0400095B RID: 2395
	[NonSerialized]
	private global::IUseableChecked useCheck;

	// Token: 0x0400095C RID: 2396
	[NonSerialized]
	private global::IUseableNotifyDecline useDecline;

	// Token: 0x0400095D RID: 2397
	[NonSerialized]
	private global::IUseableUpdated useUpdate;

	// Token: 0x0400095E RID: 2398
	[NonSerialized]
	private bool canUse;

	// Token: 0x0400095F RID: 2399
	[NonSerialized]
	private bool canCheck;

	// Token: 0x04000960 RID: 2400
	[NonSerialized]
	private bool wantDeclines;

	// Token: 0x04000961 RID: 2401
	[NonSerialized]
	private bool canUpdate;

	// Token: 0x04000962 RID: 2402
	[NonSerialized]
	private bool inKillCallback;

	// Token: 0x04000963 RID: 2403
	[NonSerialized]
	private bool inDestroy;

	// Token: 0x04000964 RID: 2404
	[NonSerialized]
	private bool _implemented;

	// Token: 0x04000965 RID: 2405
	[NonSerialized]
	private bool _awoke;

	// Token: 0x04000966 RID: 2406
	[NonSerialized]
	private global::UseUpdateFlags updateFlags;

	// Token: 0x04000967 RID: 2407
	[NonSerialized]
	private global::Character _user;

	// Token: 0x04000968 RID: 2408
	[NonSerialized]
	private global::CharacterDeathSignal onDeathCallback;

	// Token: 0x04000969 RID: 2409
	[NonSerialized]
	private global::Useable.FunctionCallState callState;

	// Token: 0x0400096A RID: 2410
	private static bool hasException;

	// Token: 0x0400096B RID: 2411
	private static Exception lastException;

	// Token: 0x0200021E RID: 542
	private enum FunctionCallState : sbyte
	{
		// Token: 0x0400096E RID: 2414
		None,
		// Token: 0x0400096F RID: 2415
		Enter,
		// Token: 0x04000970 RID: 2416
		Exit,
		// Token: 0x04000971 RID: 2417
		Eject,
		// Token: 0x04000972 RID: 2418
		OnUseUpdate
	}

	// Token: 0x0200021F RID: 543
	// (Invoke) Token: 0x06000EF0 RID: 3824
	public delegate void UseExitCallback(global::Useable useable, bool wasEjected);
}
