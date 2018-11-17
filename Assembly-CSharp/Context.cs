using System;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x02000515 RID: 1301
public sealed class Context : MonoBehaviour
{
	// Token: 0x14000020 RID: 32
	// (add) Token: 0x06002C47 RID: 11335 RVA: 0x000A5CA8 File Offset: 0x000A3EA8
	// (remove) Token: 0x06002C48 RID: 11336 RVA: 0x000A5CDC File Offset: 0x000A3EDC
	public static event global::ContextClientWorkingCallback OnClientWorking
	{
		add
		{
			ContextUI.clientWorkingCallbacks = (global::ContextClientWorkingCallback)Delegate.Combine(ContextUI.clientWorkingCallbacks, value);
			if (global::Context.Working)
			{
				value(true);
			}
		}
		remove
		{
			ContextUI.clientWorkingCallbacks = (global::ContextClientWorkingCallback)Delegate.Remove(ContextUI.clientWorkingCallbacks, value);
			if (global::Context.Working)
			{
				value(false);
			}
		}
	}

	// Token: 0x06002C49 RID: 11337 RVA: 0x000A5D10 File Offset: 0x000A3F10
	private void Awake()
	{
		if (global::Context.self && global::Context.self != this)
		{
			Debug.LogError("More than one", this);
			return;
		}
		global::Context.self = this;
		global::Context.network = base.GetComponent<uLinkNetworkView>();
		global::Context.ui = base.GetComponent<ContextUI>();
	}

	// Token: 0x06002C4A RID: 11338 RVA: 0x000A5D64 File Offset: 0x000A3F64
	private void OnDestroy()
	{
		if (global::Context.self == this)
		{
			global::Context.self = null;
			global::Context.network = null;
			global::Context.swallowInputCount = 0;
			global::Context.ui = null;
		}
	}

	// Token: 0x170009AE RID: 2478
	// (get) Token: 0x06002C4B RID: 11339 RVA: 0x000A5D9C File Offset: 0x000A3F9C
	public static bool Working
	{
		get
		{
			return global::Context.self && global::Context.ui._clientState != ContextClientState.Off;
		}
	}

	// Token: 0x170009AF RID: 2479
	// (get) Token: 0x06002C4C RID: 11340 RVA: 0x000A5DCC File Offset: 0x000A3FCC
	public static bool WorkingInMenu
	{
		get
		{
			return global::Context.self && global::Context.ui._clientState > ContextClientState.Off && global::Context.ui._clientState < ContextClientState.Validating;
		}
	}

	// Token: 0x170009B0 RID: 2480
	// (get) Token: 0x06002C4D RID: 11341 RVA: 0x000A5E00 File Offset: 0x000A4000
	public static bool ButtonDown
	{
		get
		{
			if (Input.GetButtonDown("WorldUse"))
			{
				if (global::Context.swallowInputCount == 0)
				{
					return !global::ChatUI.IsVisible();
				}
				global::Context.swallowInputCount--;
			}
			return false;
		}
	}

	// Token: 0x170009B1 RID: 2481
	// (get) Token: 0x06002C4E RID: 11342 RVA: 0x000A5E34 File Offset: 0x000A4034
	public static bool ButtonUp
	{
		get
		{
			return Input.GetButtonUp("WorldUse");
		}
	}

	// Token: 0x06002C4F RID: 11343 RVA: 0x000A5E40 File Offset: 0x000A4040
	public static bool BeginQuery(global::Contextual contextual)
	{
		if (!global::Context.self)
		{
			Debug.LogWarning("Theres no instance", global::Context.self);
		}
		else if (global::Context.ui._clientState != ContextClientState.Off)
		{
			Debug.LogWarning("Client is already in a context menu. Wait", contextual);
		}
		else if (!contextual)
		{
			Debug.LogWarning("null", global::Context.self);
		}
		else if (!contextual.exists)
		{
			Debug.LogWarning("requestable destroyed or did not implement monobehaviour", global::Context.self);
		}
		else
		{
			MonoBehaviour implementor = contextual.implementor;
			global::NetEntityID entID;
			if ((int)global::NetEntityID.Of(contextual, out entID) != 0)
			{
				global::Context.ui.OnServerQuerySent(implementor, entID);
				return true;
			}
			Debug.LogWarning("requestable has no network view", implementor);
		}
		return false;
	}

	// Token: 0x06002C50 RID: 11344 RVA: 0x000A5F04 File Offset: 0x000A4104
	public static void EndQuery()
	{
		if (global::Context.self && global::Context.ui._clientState > ContextClientState.Off && global::Context.ui._clientState < ContextClientState.Validating)
		{
			if (global::NetCull.localTimeInMillis - global::Context.ui.clientQueryTime <= 300UL)
			{
				global::Context.ui.OnServerQuickTapSent();
			}
			else
			{
				global::Context.ui.OnServerCancelSent();
			}
		}
	}

	// Token: 0x06002C51 RID: 11345 RVA: 0x000A5F74 File Offset: 0x000A4174
	[RPC]
	private void A(global::NetEntityID hit, uLink.NetworkMessageInfo info)
	{
	}

	// Token: 0x06002C52 RID: 11346 RVA: 0x000A5F78 File Offset: 0x000A4178
	[RPC]
	private void B(uLink.NetworkMessageInfo info)
	{
	}

	// Token: 0x06002C53 RID: 11347 RVA: 0x000A5F7C File Offset: 0x000A417C
	[RPC]
	private void C(int name, uLink.NetworkMessageInfo info)
	{
	}

	// Token: 0x06002C54 RID: 11348 RVA: 0x000A5F80 File Offset: 0x000A4180
	[RPC]
	private void D(uLink.NetworkMessageInfo info)
	{
	}

	// Token: 0x06002C55 RID: 11349 RVA: 0x000A5F84 File Offset: 0x000A4184
	[RPC]
	private void E(ContextMenuData options, uLink.NetworkMessageInfo info)
	{
		global::Context.ui.OnServerMenu(options);
	}

	// Token: 0x06002C56 RID: 11350 RVA: 0x000A5F94 File Offset: 0x000A4194
	[RPC]
	private void F(uLink.NetworkMessageInfo info)
	{
		global::Context.ui.OnServerNoOp();
	}

	// Token: 0x06002C57 RID: 11351 RVA: 0x000A5FA0 File Offset: 0x000A41A0
	[RPC]
	private void G(uLink.NetworkMessageInfo info)
	{
		global::Context.ui.OnServerCancel();
	}

	// Token: 0x06002C58 RID: 11352 RVA: 0x000A5FAC File Offset: 0x000A41AC
	[RPC]
	private void H(uLink.NetworkMessageInfo info)
	{
		global::Context.ui.OnServerImmediate(false);
	}

	// Token: 0x06002C59 RID: 11353 RVA: 0x000A5FBC File Offset: 0x000A41BC
	[RPC]
	private void I(uLink.NetworkMessageInfo info)
	{
		global::Context.ui.OnServerImmediate(true);
	}

	// Token: 0x06002C5A RID: 11354 RVA: 0x000A5FCC File Offset: 0x000A41CC
	[RPC]
	private void J(uLink.NetworkMessageInfo info)
	{
		global::Context.ui.OnServerSelection(false);
	}

	// Token: 0x06002C5B RID: 11355 RVA: 0x000A5FDC File Offset: 0x000A41DC
	[RPC]
	private void K(uLink.NetworkMessageInfo info)
	{
		global::Context.ui.OnServerSelection(true);
	}

	// Token: 0x06002C5C RID: 11356 RVA: 0x000A5FEC File Offset: 0x000A41EC
	[RPC]
	private void L(uLink.NetworkMessageInfo info)
	{
		global::Context.ui.OnServerSelectionStale();
	}

	// Token: 0x06002C5D RID: 11357 RVA: 0x000A5FF8 File Offset: 0x000A41F8
	[RPC]
	private void M(uLink.NetworkMessageInfo info)
	{
		global::Context.ui.OnServerRestartPolling();
	}

	// Token: 0x0400162D RID: 5677
	private const string kButtonName = "WorldUse";

	// Token: 0x0400162E RID: 5678
	public const ulong kQuickTapMillisecondLimit = 300UL;

	// Token: 0x0400162F RID: 5679
	private const string kRPCPrefix = "Context:";

	// Token: 0x04001630 RID: 5680
	private const string kRPC_RequestFromClient = "Context:A";

	// Token: 0x04001631 RID: 5681
	private const string kRPC_QuickTapFromClient = "Context:B";

	// Token: 0x04001632 RID: 5682
	private const string kRPC_SelectedOptionFromClient = "Context:C";

	// Token: 0x04001633 RID: 5683
	private const string kRPC_NoSelectionFromClient = "Context:D";

	// Token: 0x04001634 RID: 5684
	private const string kRPC_ReadOptionsFromServer = "Context:E";

	// Token: 0x04001635 RID: 5685
	private const string kRPC_NoOpFromServer = "Context:F";

	// Token: 0x04001636 RID: 5686
	private const string kRPC_CancelFromServer = "Context:G";

	// Token: 0x04001637 RID: 5687
	private const string kRPC_FailedImmediateFromServer = "Context:H";

	// Token: 0x04001638 RID: 5688
	private const string kRPC_SuccessImmediateFromServer = "Context:I";

	// Token: 0x04001639 RID: 5689
	private const string kRPC_FailedSelectionFromServer = "Context:J";

	// Token: 0x0400163A RID: 5690
	private const string kRPC_SuccessSelectionFromServer = "Context:K";

	// Token: 0x0400163B RID: 5691
	private const string kRPC_StaleSelectionFromServer = "Context:L";

	// Token: 0x0400163C RID: 5692
	private const string kRPC_RetryFromServer = "Context:M";

	// Token: 0x0400163D RID: 5693
	private static global::Context self;

	// Token: 0x0400163E RID: 5694
	private static uLinkNetworkView network;

	// Token: 0x0400163F RID: 5695
	private static ContextUI ui;

	// Token: 0x04001640 RID: 5696
	private static int swallowInputCount;

	// Token: 0x02000516 RID: 1302
	internal static class UICommands
	{
		// Token: 0x06002C5E RID: 11358 RVA: 0x000A6004 File Offset: 0x000A4204
		internal static void Issue_Request(global::NetEntityID clientQueryEntID)
		{
			global::Context.network.RPC<global::NetEntityID>("Context:A", 0, clientQueryEntID);
		}

		// Token: 0x06002C5F RID: 11359 RVA: 0x000A6018 File Offset: 0x000A4218
		internal static void Issue_QuickTap()
		{
			global::Context.network.RPC("Context:B", 0, new object[0]);
		}

		// Token: 0x06002C60 RID: 11360 RVA: 0x000A6030 File Offset: 0x000A4230
		internal static void Issue_Cancel()
		{
			global::Context.network.RPC("Context:D", 0, new object[0]);
		}

		// Token: 0x06002C61 RID: 11361 RVA: 0x000A6048 File Offset: 0x000A4248
		internal static void Issue_Selection(int name)
		{
			global::Context.network.RPC<int>("Context:C", 0, name);
		}

		// Token: 0x06002C62 RID: 11362 RVA: 0x000A605C File Offset: 0x000A425C
		internal static bool IsButtonHeld(bool swallow)
		{
			if (Input.GetButton("WorldUse"))
			{
				if (swallow)
				{
					global::Context.swallowInputCount = 1;
				}
				return true;
			}
			return false;
		}
	}
}
