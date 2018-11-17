using System;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x0200045F RID: 1119
public sealed class Context : MonoBehaviour
{
	// Token: 0x14000020 RID: 32
	// (add) Token: 0x060028B7 RID: 10423 RVA: 0x0009FD28 File Offset: 0x0009DF28
	// (remove) Token: 0x060028B8 RID: 10424 RVA: 0x0009FD5C File Offset: 0x0009DF5C
	public static event ContextClientWorkingCallback OnClientWorking
	{
		add
		{
			ContextUI.clientWorkingCallbacks = (ContextClientWorkingCallback)Delegate.Combine(ContextUI.clientWorkingCallbacks, value);
			if (Context.Working)
			{
				value(true);
			}
		}
		remove
		{
			ContextUI.clientWorkingCallbacks = (ContextClientWorkingCallback)Delegate.Remove(ContextUI.clientWorkingCallbacks, value);
			if (Context.Working)
			{
				value(false);
			}
		}
	}

	// Token: 0x060028B9 RID: 10425 RVA: 0x0009FD90 File Offset: 0x0009DF90
	private void Awake()
	{
		if (Context.self && Context.self != this)
		{
			Debug.LogError("More than one", this);
			return;
		}
		Context.self = this;
		Context.network = base.GetComponent<uLinkNetworkView>();
		Context.ui = base.GetComponent<ContextUI>();
	}

	// Token: 0x060028BA RID: 10426 RVA: 0x0009FDE4 File Offset: 0x0009DFE4
	private void OnDestroy()
	{
		if (Context.self == this)
		{
			Context.self = null;
			Context.network = null;
			Context.swallowInputCount = 0;
			Context.ui = null;
		}
	}

	// Token: 0x17000946 RID: 2374
	// (get) Token: 0x060028BB RID: 10427 RVA: 0x0009FE1C File Offset: 0x0009E01C
	public static bool Working
	{
		get
		{
			return Context.self && Context.ui._clientState != ContextClientState.Off;
		}
	}

	// Token: 0x17000947 RID: 2375
	// (get) Token: 0x060028BC RID: 10428 RVA: 0x0009FE4C File Offset: 0x0009E04C
	public static bool WorkingInMenu
	{
		get
		{
			return Context.self && Context.ui._clientState > ContextClientState.Off && Context.ui._clientState < ContextClientState.Validating;
		}
	}

	// Token: 0x17000948 RID: 2376
	// (get) Token: 0x060028BD RID: 10429 RVA: 0x0009FE80 File Offset: 0x0009E080
	public static bool ButtonDown
	{
		get
		{
			if (Input.GetButtonDown("WorldUse"))
			{
				if (Context.swallowInputCount == 0)
				{
					return !ChatUI.IsVisible();
				}
				Context.swallowInputCount--;
			}
			return false;
		}
	}

	// Token: 0x17000949 RID: 2377
	// (get) Token: 0x060028BE RID: 10430 RVA: 0x0009FEB4 File Offset: 0x0009E0B4
	public static bool ButtonUp
	{
		get
		{
			return Input.GetButtonUp("WorldUse");
		}
	}

	// Token: 0x060028BF RID: 10431 RVA: 0x0009FEC0 File Offset: 0x0009E0C0
	public static bool BeginQuery(Contextual contextual)
	{
		if (!Context.self)
		{
			Debug.LogWarning("Theres no instance", Context.self);
		}
		else if (Context.ui._clientState != ContextClientState.Off)
		{
			Debug.LogWarning("Client is already in a context menu. Wait", contextual);
		}
		else if (!contextual)
		{
			Debug.LogWarning("null", Context.self);
		}
		else if (!contextual.exists)
		{
			Debug.LogWarning("requestable destroyed or did not implement monobehaviour", Context.self);
		}
		else
		{
			MonoBehaviour implementor = contextual.implementor;
			NetEntityID entID;
			if ((int)NetEntityID.Of(contextual, out entID) != 0)
			{
				Context.ui.OnServerQuerySent(implementor, entID);
				return true;
			}
			Debug.LogWarning("requestable has no network view", implementor);
		}
		return false;
	}

	// Token: 0x060028C0 RID: 10432 RVA: 0x0009FF84 File Offset: 0x0009E184
	public static void EndQuery()
	{
		if (Context.self && Context.ui._clientState > ContextClientState.Off && Context.ui._clientState < ContextClientState.Validating)
		{
			if (NetCull.localTimeInMillis - Context.ui.clientQueryTime <= 300UL)
			{
				Context.ui.OnServerQuickTapSent();
			}
			else
			{
				Context.ui.OnServerCancelSent();
			}
		}
	}

	// Token: 0x060028C1 RID: 10433 RVA: 0x0009FFF4 File Offset: 0x0009E1F4
	[RPC]
	private void A(NetEntityID hit, NetworkMessageInfo info)
	{
	}

	// Token: 0x060028C2 RID: 10434 RVA: 0x0009FFF8 File Offset: 0x0009E1F8
	[RPC]
	private void B(NetworkMessageInfo info)
	{
	}

	// Token: 0x060028C3 RID: 10435 RVA: 0x0009FFFC File Offset: 0x0009E1FC
	[RPC]
	private void C(int name, NetworkMessageInfo info)
	{
	}

	// Token: 0x060028C4 RID: 10436 RVA: 0x000A0000 File Offset: 0x0009E200
	[RPC]
	private void D(NetworkMessageInfo info)
	{
	}

	// Token: 0x060028C5 RID: 10437 RVA: 0x000A0004 File Offset: 0x0009E204
	[RPC]
	private void E(ContextMenuData options, NetworkMessageInfo info)
	{
		Context.ui.OnServerMenu(options);
	}

	// Token: 0x060028C6 RID: 10438 RVA: 0x000A0014 File Offset: 0x0009E214
	[RPC]
	private void F(NetworkMessageInfo info)
	{
		Context.ui.OnServerNoOp();
	}

	// Token: 0x060028C7 RID: 10439 RVA: 0x000A0020 File Offset: 0x0009E220
	[RPC]
	private void G(NetworkMessageInfo info)
	{
		Context.ui.OnServerCancel();
	}

	// Token: 0x060028C8 RID: 10440 RVA: 0x000A002C File Offset: 0x0009E22C
	[RPC]
	private void H(NetworkMessageInfo info)
	{
		Context.ui.OnServerImmediate(false);
	}

	// Token: 0x060028C9 RID: 10441 RVA: 0x000A003C File Offset: 0x0009E23C
	[RPC]
	private void I(NetworkMessageInfo info)
	{
		Context.ui.OnServerImmediate(true);
	}

	// Token: 0x060028CA RID: 10442 RVA: 0x000A004C File Offset: 0x0009E24C
	[RPC]
	private void J(NetworkMessageInfo info)
	{
		Context.ui.OnServerSelection(false);
	}

	// Token: 0x060028CB RID: 10443 RVA: 0x000A005C File Offset: 0x0009E25C
	[RPC]
	private void K(NetworkMessageInfo info)
	{
		Context.ui.OnServerSelection(true);
	}

	// Token: 0x060028CC RID: 10444 RVA: 0x000A006C File Offset: 0x0009E26C
	[RPC]
	private void L(NetworkMessageInfo info)
	{
		Context.ui.OnServerSelectionStale();
	}

	// Token: 0x060028CD RID: 10445 RVA: 0x000A0078 File Offset: 0x0009E278
	[RPC]
	private void M(NetworkMessageInfo info)
	{
		Context.ui.OnServerRestartPolling();
	}

	// Token: 0x040014AA RID: 5290
	private const string kButtonName = "WorldUse";

	// Token: 0x040014AB RID: 5291
	public const ulong kQuickTapMillisecondLimit = 300UL;

	// Token: 0x040014AC RID: 5292
	private const string kRPCPrefix = "Context:";

	// Token: 0x040014AD RID: 5293
	private const string kRPC_RequestFromClient = "Context:A";

	// Token: 0x040014AE RID: 5294
	private const string kRPC_QuickTapFromClient = "Context:B";

	// Token: 0x040014AF RID: 5295
	private const string kRPC_SelectedOptionFromClient = "Context:C";

	// Token: 0x040014B0 RID: 5296
	private const string kRPC_NoSelectionFromClient = "Context:D";

	// Token: 0x040014B1 RID: 5297
	private const string kRPC_ReadOptionsFromServer = "Context:E";

	// Token: 0x040014B2 RID: 5298
	private const string kRPC_NoOpFromServer = "Context:F";

	// Token: 0x040014B3 RID: 5299
	private const string kRPC_CancelFromServer = "Context:G";

	// Token: 0x040014B4 RID: 5300
	private const string kRPC_FailedImmediateFromServer = "Context:H";

	// Token: 0x040014B5 RID: 5301
	private const string kRPC_SuccessImmediateFromServer = "Context:I";

	// Token: 0x040014B6 RID: 5302
	private const string kRPC_FailedSelectionFromServer = "Context:J";

	// Token: 0x040014B7 RID: 5303
	private const string kRPC_SuccessSelectionFromServer = "Context:K";

	// Token: 0x040014B8 RID: 5304
	private const string kRPC_StaleSelectionFromServer = "Context:L";

	// Token: 0x040014B9 RID: 5305
	private const string kRPC_RetryFromServer = "Context:M";

	// Token: 0x040014BA RID: 5306
	private static Context self;

	// Token: 0x040014BB RID: 5307
	private static uLinkNetworkView network;

	// Token: 0x040014BC RID: 5308
	private static ContextUI ui;

	// Token: 0x040014BD RID: 5309
	private static int swallowInputCount;

	// Token: 0x02000460 RID: 1120
	internal static class UICommands
	{
		// Token: 0x060028CE RID: 10446 RVA: 0x000A0084 File Offset: 0x0009E284
		internal static void Issue_Request(NetEntityID clientQueryEntID)
		{
			Context.network.RPC<NetEntityID>("Context:A", 0, clientQueryEntID);
		}

		// Token: 0x060028CF RID: 10447 RVA: 0x000A0098 File Offset: 0x0009E298
		internal static void Issue_QuickTap()
		{
			Context.network.RPC("Context:B", 0, new object[0]);
		}

		// Token: 0x060028D0 RID: 10448 RVA: 0x000A00B0 File Offset: 0x0009E2B0
		internal static void Issue_Cancel()
		{
			Context.network.RPC("Context:D", 0, new object[0]);
		}

		// Token: 0x060028D1 RID: 10449 RVA: 0x000A00C8 File Offset: 0x0009E2C8
		internal static void Issue_Selection(int name)
		{
			Context.network.RPC<int>("Context:C", 0, name);
		}

		// Token: 0x060028D2 RID: 10450 RVA: 0x000A00DC File Offset: 0x0009E2DC
		internal static bool IsButtonHeld(bool swallow)
		{
			if (Input.GetButton("WorldUse"))
			{
				if (swallow)
				{
					Context.swallowInputCount = 1;
				}
				return true;
			}
			return false;
		}
	}
}
