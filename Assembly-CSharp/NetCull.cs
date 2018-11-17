using System;
using System.Collections.Generic;
using System.Diagnostics;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x020002F3 RID: 755
public static class NetCull
{
	// Token: 0x06001B24 RID: 6948 RVA: 0x0006ADF0 File Offset: 0x00068FF0
	public static void RPC(NetworkView view, NetworkFlags flags, string messageName, RPCMode rpcMode)
	{
		view.RPC(flags, messageName, rpcMode, new object[0]);
	}

	// Token: 0x06001B25 RID: 6949 RVA: 0x0006AE04 File Offset: 0x00069004
	public static void RPC(NetworkView view, NetworkFlags flags, string messageName, NetworkPlayer target)
	{
		view.RPC(flags, messageName, target, new object[0]);
	}

	// Token: 0x06001B26 RID: 6950 RVA: 0x0006AE18 File Offset: 0x00069018
	public static void RPC(NetworkView view, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets)
	{
		view.RPC(flags, messageName, targets, new object[0]);
	}

	// Token: 0x06001B27 RID: 6951 RVA: 0x0006AE2C File Offset: 0x0006902C
	public static void RPC(NetworkView view, string messageName, RPCMode rpcMode)
	{
		view.RPC(messageName, rpcMode, new object[0]);
	}

	// Token: 0x06001B28 RID: 6952 RVA: 0x0006AE3C File Offset: 0x0006903C
	public static void RPC(NetworkView view, string messageName, NetworkPlayer target)
	{
		view.RPC(messageName, target, new object[0]);
	}

	// Token: 0x06001B29 RID: 6953 RVA: 0x0006AE4C File Offset: 0x0006904C
	public static void RPC(NetworkView view, string messageName, IEnumerable<NetworkPlayer> targets)
	{
		view.RPC(messageName, targets, new object[0]);
	}

	// Token: 0x06001B2A RID: 6954 RVA: 0x0006AE5C File Offset: 0x0006905C
	public static void RPC<P0>(NetworkView view, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0)
	{
		view.RPC<P0>(flags, messageName, rpcMode, p0);
	}

	// Token: 0x06001B2B RID: 6955 RVA: 0x0006AE6C File Offset: 0x0006906C
	public static void RPC<P0>(NetworkView view, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0)
	{
		view.RPC<P0>(flags, messageName, target, p0);
	}

	// Token: 0x06001B2C RID: 6956 RVA: 0x0006AE7C File Offset: 0x0006907C
	public static void RPC<P0>(NetworkView view, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0)
	{
		view.RPC<P0>(flags, messageName, targets, p0);
	}

	// Token: 0x06001B2D RID: 6957 RVA: 0x0006AE8C File Offset: 0x0006908C
	public static void RPC<P0>(NetworkView view, string messageName, RPCMode rpcMode, P0 p0)
	{
		view.RPC<P0>(messageName, rpcMode, p0);
	}

	// Token: 0x06001B2E RID: 6958 RVA: 0x0006AE98 File Offset: 0x00069098
	public static void RPC<P0>(NetworkView view, string messageName, NetworkPlayer target, P0 p0)
	{
		view.RPC<P0>(messageName, target, p0);
	}

	// Token: 0x06001B2F RID: 6959 RVA: 0x0006AEA4 File Offset: 0x000690A4
	public static void RPC<P0>(NetworkView view, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0)
	{
		view.RPC<P0>(messageName, targets, p0);
	}

	// Token: 0x06001B30 RID: 6960 RVA: 0x0006AEB0 File Offset: 0x000690B0
	public static void RPC<P0, P1>(NetworkView view, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1)
	{
		view.RPC(flags, messageName, rpcMode, new object[]
		{
			p0,
			p1
		});
	}

	// Token: 0x06001B31 RID: 6961 RVA: 0x0006AEE0 File Offset: 0x000690E0
	public static void RPC<P0, P1>(NetworkView view, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1)
	{
		view.RPC(flags, messageName, target, new object[]
		{
			p0,
			p1
		});
	}

	// Token: 0x06001B32 RID: 6962 RVA: 0x0006AF10 File Offset: 0x00069110
	public static void RPC<P0, P1>(NetworkView view, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1)
	{
		view.RPC(flags, messageName, targets, new object[]
		{
			p0,
			p1
		});
	}

	// Token: 0x06001B33 RID: 6963 RVA: 0x0006AF40 File Offset: 0x00069140
	public static void RPC<P0, P1>(NetworkView view, string messageName, RPCMode rpcMode, P0 p0, P1 p1)
	{
		view.RPC(messageName, rpcMode, new object[]
		{
			p0,
			p1
		});
	}

	// Token: 0x06001B34 RID: 6964 RVA: 0x0006AF64 File Offset: 0x00069164
	public static void RPC<P0, P1>(NetworkView view, string messageName, NetworkPlayer target, P0 p0, P1 p1)
	{
		view.RPC(messageName, target, new object[]
		{
			p0,
			p1
		});
	}

	// Token: 0x06001B35 RID: 6965 RVA: 0x0006AF88 File Offset: 0x00069188
	public static void RPC<P0, P1>(NetworkView view, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1)
	{
		view.RPC(messageName, targets, new object[]
		{
			p0,
			p1
		});
	}

	// Token: 0x06001B36 RID: 6966 RVA: 0x0006AFAC File Offset: 0x000691AC
	public static void RPC<P0, P1, P2>(NetworkView view, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2)
	{
		view.RPC(flags, messageName, rpcMode, new object[]
		{
			p0,
			p1,
			p2
		});
	}

	// Token: 0x06001B37 RID: 6967 RVA: 0x0006AFE8 File Offset: 0x000691E8
	public static void RPC<P0, P1, P2>(NetworkView view, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2)
	{
		view.RPC(flags, messageName, target, new object[]
		{
			p0,
			p1,
			p2
		});
	}

	// Token: 0x06001B38 RID: 6968 RVA: 0x0006B024 File Offset: 0x00069224
	public static void RPC<P0, P1, P2>(NetworkView view, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2)
	{
		view.RPC(flags, messageName, targets, new object[]
		{
			p0,
			p1,
			p2
		});
	}

	// Token: 0x06001B39 RID: 6969 RVA: 0x0006B060 File Offset: 0x00069260
	public static void RPC<P0, P1, P2>(NetworkView view, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2)
	{
		view.RPC(messageName, rpcMode, new object[]
		{
			p0,
			p1,
			p2
		});
	}

	// Token: 0x06001B3A RID: 6970 RVA: 0x0006B090 File Offset: 0x00069290
	public static void RPC<P0, P1, P2>(NetworkView view, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2)
	{
		view.RPC(messageName, target, new object[]
		{
			p0,
			p1,
			p2
		});
	}

	// Token: 0x06001B3B RID: 6971 RVA: 0x0006B0C0 File Offset: 0x000692C0
	public static void RPC<P0, P1, P2>(NetworkView view, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2)
	{
		view.RPC(messageName, targets, new object[]
		{
			p0,
			p1,
			p2
		});
	}

	// Token: 0x06001B3C RID: 6972 RVA: 0x0006B0F0 File Offset: 0x000692F0
	public static void RPC<P0, P1, P2, P3>(NetworkView view, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		view.RPC(flags, messageName, rpcMode, new object[]
		{
			p0,
			p1,
			p2,
			p3
		});
	}

	// Token: 0x06001B3D RID: 6973 RVA: 0x0006B134 File Offset: 0x00069334
	public static void RPC<P0, P1, P2, P3>(NetworkView view, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		view.RPC(flags, messageName, target, new object[]
		{
			p0,
			p1,
			p2,
			p3
		});
	}

	// Token: 0x06001B3E RID: 6974 RVA: 0x0006B178 File Offset: 0x00069378
	public static void RPC<P0, P1, P2, P3>(NetworkView view, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		view.RPC(flags, messageName, targets, new object[]
		{
			p0,
			p1,
			p2,
			p3
		});
	}

	// Token: 0x06001B3F RID: 6975 RVA: 0x0006B1BC File Offset: 0x000693BC
	public static void RPC<P0, P1, P2, P3>(NetworkView view, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		view.RPC(messageName, rpcMode, new object[]
		{
			p0,
			p1,
			p2,
			p3
		});
	}

	// Token: 0x06001B40 RID: 6976 RVA: 0x0006B1F4 File Offset: 0x000693F4
	public static void RPC<P0, P1, P2, P3>(NetworkView view, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		view.RPC(messageName, target, new object[]
		{
			p0,
			p1,
			p2,
			p3
		});
	}

	// Token: 0x06001B41 RID: 6977 RVA: 0x0006B22C File Offset: 0x0006942C
	public static void RPC<P0, P1, P2, P3>(NetworkView view, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		view.RPC(messageName, targets, new object[]
		{
			p0,
			p1,
			p2,
			p3
		});
	}

	// Token: 0x06001B42 RID: 6978 RVA: 0x0006B264 File Offset: 0x00069464
	public static void RPC<P0, P1, P2, P3, P4>(NetworkView view, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		view.RPC(flags, messageName, rpcMode, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4
		});
	}

	// Token: 0x06001B43 RID: 6979 RVA: 0x0006B2B4 File Offset: 0x000694B4
	public static void RPC<P0, P1, P2, P3, P4>(NetworkView view, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		view.RPC(flags, messageName, target, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4
		});
	}

	// Token: 0x06001B44 RID: 6980 RVA: 0x0006B304 File Offset: 0x00069504
	public static void RPC<P0, P1, P2, P3, P4>(NetworkView view, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		view.RPC(flags, messageName, targets, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4
		});
	}

	// Token: 0x06001B45 RID: 6981 RVA: 0x0006B354 File Offset: 0x00069554
	public static void RPC<P0, P1, P2, P3, P4>(NetworkView view, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		view.RPC(messageName, rpcMode, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4
		});
	}

	// Token: 0x06001B46 RID: 6982 RVA: 0x0006B3A0 File Offset: 0x000695A0
	public static void RPC<P0, P1, P2, P3, P4>(NetworkView view, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		view.RPC(messageName, target, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4
		});
	}

	// Token: 0x06001B47 RID: 6983 RVA: 0x0006B3EC File Offset: 0x000695EC
	public static void RPC<P0, P1, P2, P3, P4>(NetworkView view, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		view.RPC(messageName, targets, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4
		});
	}

	// Token: 0x06001B48 RID: 6984 RVA: 0x0006B438 File Offset: 0x00069638
	public static void RPC<P0, P1, P2, P3, P4, P5>(NetworkView view, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		view.RPC(flags, messageName, rpcMode, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4,
			p5
		});
	}

	// Token: 0x06001B49 RID: 6985 RVA: 0x0006B490 File Offset: 0x00069690
	public static void RPC<P0, P1, P2, P3, P4, P5>(NetworkView view, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		view.RPC(flags, messageName, target, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4,
			p5
		});
	}

	// Token: 0x06001B4A RID: 6986 RVA: 0x0006B4E8 File Offset: 0x000696E8
	public static void RPC<P0, P1, P2, P3, P4, P5>(NetworkView view, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		view.RPC(flags, messageName, targets, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4,
			p5
		});
	}

	// Token: 0x06001B4B RID: 6987 RVA: 0x0006B540 File Offset: 0x00069740
	public static void RPC<P0, P1, P2, P3, P4, P5>(NetworkView view, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		view.RPC(messageName, rpcMode, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4,
			p5
		});
	}

	// Token: 0x06001B4C RID: 6988 RVA: 0x0006B598 File Offset: 0x00069798
	public static void RPC<P0, P1, P2, P3, P4, P5>(NetworkView view, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		view.RPC(messageName, target, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4,
			p5
		});
	}

	// Token: 0x06001B4D RID: 6989 RVA: 0x0006B5F0 File Offset: 0x000697F0
	public static void RPC<P0, P1, P2, P3, P4, P5>(NetworkView view, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		view.RPC(messageName, targets, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4,
			p5
		});
	}

	// Token: 0x06001B4E RID: 6990 RVA: 0x0006B648 File Offset: 0x00069848
	public static void RPC<P0, P1, P2, P3, P4, P5, P6>(NetworkView view, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		view.RPC(flags, messageName, rpcMode, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4,
			p5,
			p6
		});
	}

	// Token: 0x06001B4F RID: 6991 RVA: 0x0006B6AC File Offset: 0x000698AC
	public static void RPC<P0, P1, P2, P3, P4, P5, P6>(NetworkView view, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		view.RPC(flags, messageName, target, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4,
			p5,
			p6
		});
	}

	// Token: 0x06001B50 RID: 6992 RVA: 0x0006B710 File Offset: 0x00069910
	public static void RPC<P0, P1, P2, P3, P4, P5, P6>(NetworkView view, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		view.RPC(flags, messageName, targets, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4,
			p5,
			p6
		});
	}

	// Token: 0x06001B51 RID: 6993 RVA: 0x0006B774 File Offset: 0x00069974
	public static void RPC<P0, P1, P2, P3, P4, P5, P6>(NetworkView view, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		view.RPC(messageName, rpcMode, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4,
			p5,
			p6
		});
	}

	// Token: 0x06001B52 RID: 6994 RVA: 0x0006B7D4 File Offset: 0x000699D4
	public static void RPC<P0, P1, P2, P3, P4, P5, P6>(NetworkView view, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		view.RPC(messageName, target, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4,
			p5,
			p6
		});
	}

	// Token: 0x06001B53 RID: 6995 RVA: 0x0006B834 File Offset: 0x00069A34
	public static void RPC<P0, P1, P2, P3, P4, P5, P6>(NetworkView view, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		view.RPC(messageName, targets, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4,
			p5,
			p6
		});
	}

	// Token: 0x06001B54 RID: 6996 RVA: 0x0006B894 File Offset: 0x00069A94
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(NetworkView view, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		view.RPC(flags, messageName, rpcMode, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4,
			p5,
			p6,
			p7
		});
	}

	// Token: 0x06001B55 RID: 6997 RVA: 0x0006B900 File Offset: 0x00069B00
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(NetworkView view, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		view.RPC(flags, messageName, target, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4,
			p5,
			p6,
			p7
		});
	}

	// Token: 0x06001B56 RID: 6998 RVA: 0x0006B96C File Offset: 0x00069B6C
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(NetworkView view, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		view.RPC(flags, messageName, targets, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4,
			p5,
			p6,
			p7
		});
	}

	// Token: 0x06001B57 RID: 6999 RVA: 0x0006B9D8 File Offset: 0x00069BD8
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(NetworkView view, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		view.RPC(messageName, rpcMode, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4,
			p5,
			p6,
			p7
		});
	}

	// Token: 0x06001B58 RID: 7000 RVA: 0x0006BA44 File Offset: 0x00069C44
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(NetworkView view, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		view.RPC(messageName, target, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4,
			p5,
			p6,
			p7
		});
	}

	// Token: 0x06001B59 RID: 7001 RVA: 0x0006BAB0 File Offset: 0x00069CB0
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(NetworkView view, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		view.RPC(messageName, targets, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4,
			p5,
			p6,
			p7
		});
	}

	// Token: 0x06001B5A RID: 7002 RVA: 0x0006BB1C File Offset: 0x00069D1C
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(NetworkView view, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		view.RPC(flags, messageName, rpcMode, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4,
			p5,
			p6,
			p7,
			p8
		});
	}

	// Token: 0x06001B5B RID: 7003 RVA: 0x0006BB94 File Offset: 0x00069D94
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(NetworkView view, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		view.RPC(flags, messageName, target, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4,
			p5,
			p6,
			p7,
			p8
		});
	}

	// Token: 0x06001B5C RID: 7004 RVA: 0x0006BC0C File Offset: 0x00069E0C
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(NetworkView view, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		view.RPC(flags, messageName, targets, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4,
			p5,
			p6,
			p7,
			p8
		});
	}

	// Token: 0x06001B5D RID: 7005 RVA: 0x0006BC84 File Offset: 0x00069E84
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(NetworkView view, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		view.RPC(messageName, rpcMode, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4,
			p5,
			p6,
			p7,
			p8
		});
	}

	// Token: 0x06001B5E RID: 7006 RVA: 0x0006BCFC File Offset: 0x00069EFC
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(NetworkView view, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		view.RPC(messageName, target, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4,
			p5,
			p6,
			p7,
			p8
		});
	}

	// Token: 0x06001B5F RID: 7007 RVA: 0x0006BD74 File Offset: 0x00069F74
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(NetworkView view, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		view.RPC(messageName, targets, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4,
			p5,
			p6,
			p7,
			p8
		});
	}

	// Token: 0x06001B60 RID: 7008 RVA: 0x0006BDEC File Offset: 0x00069FEC
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(NetworkView view, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		view.RPC(flags, messageName, rpcMode, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4,
			p5,
			p6,
			p7,
			p8,
			p9
		});
	}

	// Token: 0x06001B61 RID: 7009 RVA: 0x0006BE70 File Offset: 0x0006A070
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(NetworkView view, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		view.RPC(flags, messageName, target, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4,
			p5,
			p6,
			p7,
			p8,
			p9
		});
	}

	// Token: 0x06001B62 RID: 7010 RVA: 0x0006BEF4 File Offset: 0x0006A0F4
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(NetworkView view, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		view.RPC(flags, messageName, targets, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4,
			p5,
			p6,
			p7,
			p8,
			p9
		});
	}

	// Token: 0x06001B63 RID: 7011 RVA: 0x0006BF78 File Offset: 0x0006A178
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(NetworkView view, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		view.RPC(messageName, rpcMode, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4,
			p5,
			p6,
			p7,
			p8,
			p9
		});
	}

	// Token: 0x06001B64 RID: 7012 RVA: 0x0006BFF8 File Offset: 0x0006A1F8
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(NetworkView view, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		view.RPC(messageName, target, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4,
			p5,
			p6,
			p7,
			p8,
			p9
		});
	}

	// Token: 0x06001B65 RID: 7013 RVA: 0x0006C078 File Offset: 0x0006A278
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(NetworkView view, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		view.RPC(messageName, targets, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4,
			p5,
			p6,
			p7,
			p8,
			p9
		});
	}

	// Token: 0x06001B66 RID: 7014 RVA: 0x0006C0F8 File Offset: 0x0006A2F8
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(NetworkView view, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		view.RPC(flags, messageName, rpcMode, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4,
			p5,
			p6,
			p7,
			p8,
			p9,
			p10
		});
	}

	// Token: 0x06001B67 RID: 7015 RVA: 0x0006C188 File Offset: 0x0006A388
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(NetworkView view, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		view.RPC(flags, messageName, target, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4,
			p5,
			p6,
			p7,
			p8,
			p9,
			p10
		});
	}

	// Token: 0x06001B68 RID: 7016 RVA: 0x0006C218 File Offset: 0x0006A418
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(NetworkView view, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		view.RPC(flags, messageName, targets, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4,
			p5,
			p6,
			p7,
			p8,
			p9,
			p10
		});
	}

	// Token: 0x06001B69 RID: 7017 RVA: 0x0006C2A8 File Offset: 0x0006A4A8
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(NetworkView view, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		view.RPC(messageName, rpcMode, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4,
			p5,
			p6,
			p7,
			p8,
			p9,
			p10
		});
	}

	// Token: 0x06001B6A RID: 7018 RVA: 0x0006C334 File Offset: 0x0006A534
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(NetworkView view, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		view.RPC(messageName, target, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4,
			p5,
			p6,
			p7,
			p8,
			p9,
			p10
		});
	}

	// Token: 0x06001B6B RID: 7019 RVA: 0x0006C3C0 File Offset: 0x0006A5C0
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(NetworkView view, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		view.RPC(messageName, targets, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4,
			p5,
			p6,
			p7,
			p8,
			p9,
			p10
		});
	}

	// Token: 0x06001B6C RID: 7020 RVA: 0x0006C44C File Offset: 0x0006A64C
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(NetworkView view, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		view.RPC(flags, messageName, rpcMode, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4,
			p5,
			p6,
			p7,
			p8,
			p9,
			p10,
			p11
		});
	}

	// Token: 0x06001B6D RID: 7021 RVA: 0x0006C4E4 File Offset: 0x0006A6E4
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(NetworkView view, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		view.RPC(flags, messageName, target, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4,
			p5,
			p6,
			p7,
			p8,
			p9,
			p10,
			p11
		});
	}

	// Token: 0x06001B6E RID: 7022 RVA: 0x0006C57C File Offset: 0x0006A77C
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(NetworkView view, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		view.RPC(flags, messageName, targets, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4,
			p5,
			p6,
			p7,
			p8,
			p9,
			p10,
			p11
		});
	}

	// Token: 0x06001B6F RID: 7023 RVA: 0x0006C614 File Offset: 0x0006A814
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(NetworkView view, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		view.RPC(messageName, rpcMode, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4,
			p5,
			p6,
			p7,
			p8,
			p9,
			p10,
			p11
		});
	}

	// Token: 0x06001B70 RID: 7024 RVA: 0x0006C6AC File Offset: 0x0006A8AC
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(NetworkView view, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		view.RPC(messageName, target, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4,
			p5,
			p6,
			p7,
			p8,
			p9,
			p10,
			p11
		});
	}

	// Token: 0x06001B71 RID: 7025 RVA: 0x0006C744 File Offset: 0x0006A944
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(NetworkView view, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		view.RPC(messageName, targets, new object[]
		{
			p0,
			p1,
			p2,
			p3,
			p4,
			p5,
			p6,
			p7,
			p8,
			p9,
			p10,
			p11
		});
	}

	// Token: 0x06001B72 RID: 7026 RVA: 0x0006C7DC File Offset: 0x0006A9DC
	public static void RPC(NGCView view, NetworkFlags flags, string messageName, RPCMode rpcMode)
	{
		view.RPC(flags, messageName, rpcMode);
	}

	// Token: 0x06001B73 RID: 7027 RVA: 0x0006C7E8 File Offset: 0x0006A9E8
	public static void RPC(NGCView view, NetworkFlags flags, string messageName, NetworkPlayer target)
	{
		view.RPC(flags, messageName, target);
	}

	// Token: 0x06001B74 RID: 7028 RVA: 0x0006C7F4 File Offset: 0x0006A9F4
	public static void RPC(NGCView view, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets)
	{
		view.RPC(flags, messageName, targets);
	}

	// Token: 0x06001B75 RID: 7029 RVA: 0x0006C800 File Offset: 0x0006AA00
	public static void RPC(NGCView view, string messageName, RPCMode rpcMode)
	{
		view.RPC(messageName, rpcMode);
	}

	// Token: 0x06001B76 RID: 7030 RVA: 0x0006C80C File Offset: 0x0006AA0C
	public static void RPC(NGCView view, string messageName, NetworkPlayer target)
	{
		view.RPC(messageName, target);
	}

	// Token: 0x06001B77 RID: 7031 RVA: 0x0006C818 File Offset: 0x0006AA18
	public static void RPC(NGCView view, string messageName, IEnumerable<NetworkPlayer> targets)
	{
		view.RPC(messageName, targets);
	}

	// Token: 0x06001B78 RID: 7032 RVA: 0x0006C824 File Offset: 0x0006AA24
	public static void RPC<P0>(NGCView view, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0)
	{
		view.RPC<P0>(flags, messageName, rpcMode, p0);
	}

	// Token: 0x06001B79 RID: 7033 RVA: 0x0006C834 File Offset: 0x0006AA34
	public static void RPC<P0>(NGCView view, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0)
	{
		view.RPC<P0>(flags, messageName, target, p0);
	}

	// Token: 0x06001B7A RID: 7034 RVA: 0x0006C844 File Offset: 0x0006AA44
	public static void RPC<P0>(NGCView view, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0)
	{
		view.RPC<P0>(flags, messageName, targets, p0);
	}

	// Token: 0x06001B7B RID: 7035 RVA: 0x0006C854 File Offset: 0x0006AA54
	public static void RPC<P0>(NGCView view, string messageName, RPCMode rpcMode, P0 p0)
	{
		view.RPC<P0>(messageName, rpcMode, p0);
	}

	// Token: 0x06001B7C RID: 7036 RVA: 0x0006C860 File Offset: 0x0006AA60
	public static void RPC<P0>(NGCView view, string messageName, NetworkPlayer target, P0 p0)
	{
		view.RPC<P0>(messageName, target, p0);
	}

	// Token: 0x06001B7D RID: 7037 RVA: 0x0006C86C File Offset: 0x0006AA6C
	public static void RPC<P0>(NGCView view, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0)
	{
		view.RPC<P0>(messageName, targets, p0);
	}

	// Token: 0x06001B7E RID: 7038 RVA: 0x0006C878 File Offset: 0x0006AA78
	public static void RPC<P0, P1>(NGCView view, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1)
	{
		view.RPC<P0, P1>(flags, messageName, rpcMode, p0, p1);
	}

	// Token: 0x06001B7F RID: 7039 RVA: 0x0006C888 File Offset: 0x0006AA88
	public static void RPC<P0, P1>(NGCView view, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1)
	{
		view.RPC<P0, P1>(flags, messageName, target, p0, p1);
	}

	// Token: 0x06001B80 RID: 7040 RVA: 0x0006C898 File Offset: 0x0006AA98
	public static void RPC<P0, P1>(NGCView view, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1)
	{
		view.RPC<P0, P1>(flags, messageName, targets, p0, p1);
	}

	// Token: 0x06001B81 RID: 7041 RVA: 0x0006C8A8 File Offset: 0x0006AAA8
	public static void RPC<P0, P1>(NGCView view, string messageName, RPCMode rpcMode, P0 p0, P1 p1)
	{
		view.RPC<P0, P1>(messageName, rpcMode, p0, p1);
	}

	// Token: 0x06001B82 RID: 7042 RVA: 0x0006C8B8 File Offset: 0x0006AAB8
	public static void RPC<P0, P1>(NGCView view, string messageName, NetworkPlayer target, P0 p0, P1 p1)
	{
		view.RPC<P0, P1>(messageName, target, p0, p1);
	}

	// Token: 0x06001B83 RID: 7043 RVA: 0x0006C8C8 File Offset: 0x0006AAC8
	public static void RPC<P0, P1>(NGCView view, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1)
	{
		view.RPC<P0, P1>(messageName, targets, p0, p1);
	}

	// Token: 0x06001B84 RID: 7044 RVA: 0x0006C8D8 File Offset: 0x0006AAD8
	public static void RPC<P0, P1, P2>(NGCView view, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2)
	{
		view.RPC<P0, P1, P2>(flags, messageName, rpcMode, p0, p1, p2);
	}

	// Token: 0x06001B85 RID: 7045 RVA: 0x0006C8EC File Offset: 0x0006AAEC
	public static void RPC<P0, P1, P2>(NGCView view, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2)
	{
		view.RPC<P0, P1, P2>(flags, messageName, target, p0, p1, p2);
	}

	// Token: 0x06001B86 RID: 7046 RVA: 0x0006C900 File Offset: 0x0006AB00
	public static void RPC<P0, P1, P2>(NGCView view, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2)
	{
		view.RPC<P0, P1, P2>(flags, messageName, targets, p0, p1, p2);
	}

	// Token: 0x06001B87 RID: 7047 RVA: 0x0006C914 File Offset: 0x0006AB14
	public static void RPC<P0, P1, P2>(NGCView view, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2)
	{
		view.RPC<P0, P1, P2>(messageName, rpcMode, p0, p1, p2);
	}

	// Token: 0x06001B88 RID: 7048 RVA: 0x0006C924 File Offset: 0x0006AB24
	public static void RPC<P0, P1, P2>(NGCView view, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2)
	{
		view.RPC<P0, P1, P2>(messageName, target, p0, p1, p2);
	}

	// Token: 0x06001B89 RID: 7049 RVA: 0x0006C934 File Offset: 0x0006AB34
	public static void RPC<P0, P1, P2>(NGCView view, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2)
	{
		view.RPC<P0, P1, P2>(messageName, targets, p0, p1, p2);
	}

	// Token: 0x06001B8A RID: 7050 RVA: 0x0006C944 File Offset: 0x0006AB44
	public static void RPC<P0, P1, P2, P3>(NGCView view, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		view.RPC<P0, P1, P2, P3>(flags, messageName, rpcMode, p0, p1, p2, p3);
	}

	// Token: 0x06001B8B RID: 7051 RVA: 0x0006C964 File Offset: 0x0006AB64
	public static void RPC<P0, P1, P2, P3>(NGCView view, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		view.RPC<P0, P1, P2, P3>(flags, messageName, target, p0, p1, p2, p3);
	}

	// Token: 0x06001B8C RID: 7052 RVA: 0x0006C984 File Offset: 0x0006AB84
	public static void RPC<P0, P1, P2, P3>(NGCView view, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		view.RPC<P0, P1, P2, P3>(flags, messageName, targets, p0, p1, p2, p3);
	}

	// Token: 0x06001B8D RID: 7053 RVA: 0x0006C9A4 File Offset: 0x0006ABA4
	public static void RPC<P0, P1, P2, P3>(NGCView view, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		view.RPC<P0, P1, P2, P3>(messageName, rpcMode, p0, p1, p2, p3);
	}

	// Token: 0x06001B8E RID: 7054 RVA: 0x0006C9B8 File Offset: 0x0006ABB8
	public static void RPC<P0, P1, P2, P3>(NGCView view, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		view.RPC<P0, P1, P2, P3>(messageName, target, p0, p1, p2, p3);
	}

	// Token: 0x06001B8F RID: 7055 RVA: 0x0006C9CC File Offset: 0x0006ABCC
	public static void RPC<P0, P1, P2, P3>(NGCView view, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		view.RPC<P0, P1, P2, P3>(messageName, targets, p0, p1, p2, p3);
	}

	// Token: 0x06001B90 RID: 7056 RVA: 0x0006C9E0 File Offset: 0x0006ABE0
	public static void RPC<P0, P1, P2, P3, P4>(NGCView view, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		view.RPC<P0, P1, P2, P3, P4>(flags, messageName, rpcMode, p0, p1, p2, p3, p4);
	}

	// Token: 0x06001B91 RID: 7057 RVA: 0x0006CA00 File Offset: 0x0006AC00
	public static void RPC<P0, P1, P2, P3, P4>(NGCView view, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		view.RPC<P0, P1, P2, P3, P4>(flags, messageName, target, p0, p1, p2, p3, p4);
	}

	// Token: 0x06001B92 RID: 7058 RVA: 0x0006CA20 File Offset: 0x0006AC20
	public static void RPC<P0, P1, P2, P3, P4>(NGCView view, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		view.RPC<P0, P1, P2, P3, P4>(flags, messageName, targets, p0, p1, p2, p3, p4);
	}

	// Token: 0x06001B93 RID: 7059 RVA: 0x0006CA40 File Offset: 0x0006AC40
	public static void RPC<P0, P1, P2, P3, P4>(NGCView view, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		view.RPC<P0, P1, P2, P3, P4>(messageName, rpcMode, p0, p1, p2, p3, p4);
	}

	// Token: 0x06001B94 RID: 7060 RVA: 0x0006CA60 File Offset: 0x0006AC60
	public static void RPC<P0, P1, P2, P3, P4>(NGCView view, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		view.RPC<P0, P1, P2, P3, P4>(messageName, target, p0, p1, p2, p3, p4);
	}

	// Token: 0x06001B95 RID: 7061 RVA: 0x0006CA80 File Offset: 0x0006AC80
	public static void RPC<P0, P1, P2, P3, P4>(NGCView view, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		view.RPC<P0, P1, P2, P3, P4>(messageName, targets, p0, p1, p2, p3, p4);
	}

	// Token: 0x06001B96 RID: 7062 RVA: 0x0006CAA0 File Offset: 0x0006ACA0
	public static void RPC<P0, P1, P2, P3, P4, P5>(NGCView view, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		view.RPC<P0, P1, P2, P3, P4, P5>(flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x06001B97 RID: 7063 RVA: 0x0006CAC4 File Offset: 0x0006ACC4
	public static void RPC<P0, P1, P2, P3, P4, P5>(NGCView view, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		view.RPC<P0, P1, P2, P3, P4, P5>(flags, messageName, target, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x06001B98 RID: 7064 RVA: 0x0006CAE8 File Offset: 0x0006ACE8
	public static void RPC<P0, P1, P2, P3, P4, P5>(NGCView view, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		view.RPC<P0, P1, P2, P3, P4, P5>(flags, messageName, targets, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x06001B99 RID: 7065 RVA: 0x0006CB0C File Offset: 0x0006AD0C
	public static void RPC<P0, P1, P2, P3, P4, P5>(NGCView view, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		view.RPC<P0, P1, P2, P3, P4, P5>(messageName, rpcMode, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x06001B9A RID: 7066 RVA: 0x0006CB2C File Offset: 0x0006AD2C
	public static void RPC<P0, P1, P2, P3, P4, P5>(NGCView view, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		view.RPC<P0, P1, P2, P3, P4, P5>(messageName, target, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x06001B9B RID: 7067 RVA: 0x0006CB4C File Offset: 0x0006AD4C
	public static void RPC<P0, P1, P2, P3, P4, P5>(NGCView view, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		view.RPC<P0, P1, P2, P3, P4, P5>(messageName, targets, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x06001B9C RID: 7068 RVA: 0x0006CB6C File Offset: 0x0006AD6C
	public static void RPC<P0, P1, P2, P3, P4, P5, P6>(NGCView view, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6>(flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x06001B9D RID: 7069 RVA: 0x0006CB90 File Offset: 0x0006AD90
	public static void RPC<P0, P1, P2, P3, P4, P5, P6>(NGCView view, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6>(flags, messageName, target, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x06001B9E RID: 7070 RVA: 0x0006CBB4 File Offset: 0x0006ADB4
	public static void RPC<P0, P1, P2, P3, P4, P5, P6>(NGCView view, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6>(flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x06001B9F RID: 7071 RVA: 0x0006CBD8 File Offset: 0x0006ADD8
	public static void RPC<P0, P1, P2, P3, P4, P5, P6>(NGCView view, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6>(messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x06001BA0 RID: 7072 RVA: 0x0006CBFC File Offset: 0x0006ADFC
	public static void RPC<P0, P1, P2, P3, P4, P5, P6>(NGCView view, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6>(messageName, target, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x06001BA1 RID: 7073 RVA: 0x0006CC20 File Offset: 0x0006AE20
	public static void RPC<P0, P1, P2, P3, P4, P5, P6>(NGCView view, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6>(messageName, targets, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x06001BA2 RID: 7074 RVA: 0x0006CC44 File Offset: 0x0006AE44
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(NGCView view, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x06001BA3 RID: 7075 RVA: 0x0006CC6C File Offset: 0x0006AE6C
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(NGCView view, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x06001BA4 RID: 7076 RVA: 0x0006CC94 File Offset: 0x0006AE94
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(NGCView view, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x06001BA5 RID: 7077 RVA: 0x0006CCBC File Offset: 0x0006AEBC
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(NGCView view, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x06001BA6 RID: 7078 RVA: 0x0006CCE0 File Offset: 0x0006AEE0
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(NGCView view, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(messageName, target, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x06001BA7 RID: 7079 RVA: 0x0006CD04 File Offset: 0x0006AF04
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(NGCView view, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x06001BA8 RID: 7080 RVA: 0x0006CD28 File Offset: 0x0006AF28
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(NGCView view, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x06001BA9 RID: 7081 RVA: 0x0006CD50 File Offset: 0x0006AF50
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(NGCView view, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x06001BAA RID: 7082 RVA: 0x0006CD78 File Offset: 0x0006AF78
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(NGCView view, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x06001BAB RID: 7083 RVA: 0x0006CDA0 File Offset: 0x0006AFA0
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(NGCView view, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x06001BAC RID: 7084 RVA: 0x0006CDC8 File Offset: 0x0006AFC8
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(NGCView view, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x06001BAD RID: 7085 RVA: 0x0006CDF0 File Offset: 0x0006AFF0
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(NGCView view, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x06001BAE RID: 7086 RVA: 0x0006CE18 File Offset: 0x0006B018
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(NGCView view, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x06001BAF RID: 7087 RVA: 0x0006CE44 File Offset: 0x0006B044
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(NGCView view, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x06001BB0 RID: 7088 RVA: 0x0006CE70 File Offset: 0x0006B070
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(NGCView view, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x06001BB1 RID: 7089 RVA: 0x0006CE9C File Offset: 0x0006B09C
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(NGCView view, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x06001BB2 RID: 7090 RVA: 0x0006CEC4 File Offset: 0x0006B0C4
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(NGCView view, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x06001BB3 RID: 7091 RVA: 0x0006CEEC File Offset: 0x0006B0EC
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(NGCView view, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x06001BB4 RID: 7092 RVA: 0x0006CF14 File Offset: 0x0006B114
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(NGCView view, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x06001BB5 RID: 7093 RVA: 0x0006CF40 File Offset: 0x0006B140
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(NGCView view, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x06001BB6 RID: 7094 RVA: 0x0006CF6C File Offset: 0x0006B16C
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(NGCView view, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x06001BB7 RID: 7095 RVA: 0x0006CF98 File Offset: 0x0006B198
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(NGCView view, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x06001BB8 RID: 7096 RVA: 0x0006CFC4 File Offset: 0x0006B1C4
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(NGCView view, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x06001BB9 RID: 7097 RVA: 0x0006CFF0 File Offset: 0x0006B1F0
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(NGCView view, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x06001BBA RID: 7098 RVA: 0x0006D01C File Offset: 0x0006B21C
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(NGCView view, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x06001BBB RID: 7099 RVA: 0x0006D04C File Offset: 0x0006B24C
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(NGCView view, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x06001BBC RID: 7100 RVA: 0x0006D07C File Offset: 0x0006B27C
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(NGCView view, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x06001BBD RID: 7101 RVA: 0x0006D0AC File Offset: 0x0006B2AC
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(NGCView view, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x06001BBE RID: 7102 RVA: 0x0006D0D8 File Offset: 0x0006B2D8
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(NGCView view, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x06001BBF RID: 7103 RVA: 0x0006D104 File Offset: 0x0006B304
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(NGCView view, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x06001BC0 RID: 7104 RVA: 0x0006D130 File Offset: 0x0006B330
	public static bool RPC(NetworkViewID viewID, NetworkFlags flags, string messageName, RPCMode rpcMode)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC(networkView, flags, messageName, rpcMode);
		return true;
	}

	// Token: 0x06001BC1 RID: 7105 RVA: 0x0006D174 File Offset: 0x0006B374
	public static bool RPC(NetworkViewID viewID, NetworkFlags flags, string messageName, NetworkPlayer target)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC(networkView, flags, messageName, target);
		return true;
	}

	// Token: 0x06001BC2 RID: 7106 RVA: 0x0006D1B8 File Offset: 0x0006B3B8
	public static bool RPC(NetworkViewID viewID, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC(networkView, flags, messageName, targets);
		return true;
	}

	// Token: 0x06001BC3 RID: 7107 RVA: 0x0006D1FC File Offset: 0x0006B3FC
	public static bool RPC(NetworkViewID viewID, string messageName, RPCMode rpcMode)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC(networkView, messageName, rpcMode);
		return true;
	}

	// Token: 0x06001BC4 RID: 7108 RVA: 0x0006D23C File Offset: 0x0006B43C
	public static bool RPC(NetworkViewID viewID, string messageName, NetworkPlayer target)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC(networkView, messageName, target);
		return true;
	}

	// Token: 0x06001BC5 RID: 7109 RVA: 0x0006D27C File Offset: 0x0006B47C
	public static bool RPC(NetworkViewID viewID, string messageName, IEnumerable<NetworkPlayer> targets)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC(networkView, messageName, targets);
		return true;
	}

	// Token: 0x06001BC6 RID: 7110 RVA: 0x0006D2BC File Offset: 0x0006B4BC
	public static bool RPC<P0>(NetworkViewID viewID, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0>(networkView, flags, messageName, rpcMode, p0);
		return true;
	}

	// Token: 0x06001BC7 RID: 7111 RVA: 0x0006D300 File Offset: 0x0006B500
	public static bool RPC<P0>(NetworkViewID viewID, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0>(networkView, flags, messageName, target, p0);
		return true;
	}

	// Token: 0x06001BC8 RID: 7112 RVA: 0x0006D344 File Offset: 0x0006B544
	public static bool RPC<P0>(NetworkViewID viewID, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0>(networkView, flags, messageName, targets, p0);
		return true;
	}

	// Token: 0x06001BC9 RID: 7113 RVA: 0x0006D388 File Offset: 0x0006B588
	public static bool RPC<P0>(NetworkViewID viewID, string messageName, RPCMode rpcMode, P0 p0)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0>(networkView, messageName, rpcMode, p0);
		return true;
	}

	// Token: 0x06001BCA RID: 7114 RVA: 0x0006D3CC File Offset: 0x0006B5CC
	public static bool RPC<P0>(NetworkViewID viewID, string messageName, NetworkPlayer target, P0 p0)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0>(networkView, messageName, target, p0);
		return true;
	}

	// Token: 0x06001BCB RID: 7115 RVA: 0x0006D410 File Offset: 0x0006B610
	public static bool RPC<P0>(NetworkViewID viewID, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0>(networkView, messageName, targets, p0);
		return true;
	}

	// Token: 0x06001BCC RID: 7116 RVA: 0x0006D454 File Offset: 0x0006B654
	public static bool RPC<P0, P1>(NetworkViewID viewID, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1>(networkView, flags, messageName, rpcMode, p0, p1);
		return true;
	}

	// Token: 0x06001BCD RID: 7117 RVA: 0x0006D49C File Offset: 0x0006B69C
	public static bool RPC<P0, P1>(NetworkViewID viewID, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1>(networkView, flags, messageName, target, p0, p1);
		return true;
	}

	// Token: 0x06001BCE RID: 7118 RVA: 0x0006D4E4 File Offset: 0x0006B6E4
	public static bool RPC<P0, P1>(NetworkViewID viewID, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1>(networkView, flags, messageName, targets, p0, p1);
		return true;
	}

	// Token: 0x06001BCF RID: 7119 RVA: 0x0006D52C File Offset: 0x0006B72C
	public static bool RPC<P0, P1>(NetworkViewID viewID, string messageName, RPCMode rpcMode, P0 p0, P1 p1)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1>(networkView, messageName, rpcMode, p0, p1);
		return true;
	}

	// Token: 0x06001BD0 RID: 7120 RVA: 0x0006D570 File Offset: 0x0006B770
	public static bool RPC<P0, P1>(NetworkViewID viewID, string messageName, NetworkPlayer target, P0 p0, P1 p1)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1>(networkView, messageName, target, p0, p1);
		return true;
	}

	// Token: 0x06001BD1 RID: 7121 RVA: 0x0006D5B4 File Offset: 0x0006B7B4
	public static bool RPC<P0, P1>(NetworkViewID viewID, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1>(networkView, messageName, targets, p0, p1);
		return true;
	}

	// Token: 0x06001BD2 RID: 7122 RVA: 0x0006D5F8 File Offset: 0x0006B7F8
	public static bool RPC<P0, P1, P2>(NetworkViewID viewID, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2>(networkView, flags, messageName, rpcMode, p0, p1, p2);
		return true;
	}

	// Token: 0x06001BD3 RID: 7123 RVA: 0x0006D640 File Offset: 0x0006B840
	public static bool RPC<P0, P1, P2>(NetworkViewID viewID, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2>(networkView, flags, messageName, target, p0, p1, p2);
		return true;
	}

	// Token: 0x06001BD4 RID: 7124 RVA: 0x0006D688 File Offset: 0x0006B888
	public static bool RPC<P0, P1, P2>(NetworkViewID viewID, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2>(networkView, flags, messageName, targets, p0, p1, p2);
		return true;
	}

	// Token: 0x06001BD5 RID: 7125 RVA: 0x0006D6D0 File Offset: 0x0006B8D0
	public static bool RPC<P0, P1, P2>(NetworkViewID viewID, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2>(networkView, messageName, rpcMode, p0, p1, p2);
		return true;
	}

	// Token: 0x06001BD6 RID: 7126 RVA: 0x0006D718 File Offset: 0x0006B918
	public static bool RPC<P0, P1, P2>(NetworkViewID viewID, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2>(networkView, messageName, target, p0, p1, p2);
		return true;
	}

	// Token: 0x06001BD7 RID: 7127 RVA: 0x0006D760 File Offset: 0x0006B960
	public static bool RPC<P0, P1, P2>(NetworkViewID viewID, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2>(networkView, messageName, targets, p0, p1, p2);
		return true;
	}

	// Token: 0x06001BD8 RID: 7128 RVA: 0x0006D7A8 File Offset: 0x0006B9A8
	public static bool RPC<P0, P1, P2, P3>(NetworkViewID viewID, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3>(networkView, flags, messageName, rpcMode, p0, p1, p2, p3);
		return true;
	}

	// Token: 0x06001BD9 RID: 7129 RVA: 0x0006D7F4 File Offset: 0x0006B9F4
	public static bool RPC<P0, P1, P2, P3>(NetworkViewID viewID, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3>(networkView, flags, messageName, target, p0, p1, p2, p3);
		return true;
	}

	// Token: 0x06001BDA RID: 7130 RVA: 0x0006D840 File Offset: 0x0006BA40
	public static bool RPC<P0, P1, P2, P3>(NetworkViewID viewID, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3>(networkView, flags, messageName, targets, p0, p1, p2, p3);
		return true;
	}

	// Token: 0x06001BDB RID: 7131 RVA: 0x0006D88C File Offset: 0x0006BA8C
	public static bool RPC<P0, P1, P2, P3>(NetworkViewID viewID, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3>(networkView, messageName, rpcMode, p0, p1, p2, p3);
		return true;
	}

	// Token: 0x06001BDC RID: 7132 RVA: 0x0006D8D4 File Offset: 0x0006BAD4
	public static bool RPC<P0, P1, P2, P3>(NetworkViewID viewID, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3>(networkView, messageName, target, p0, p1, p2, p3);
		return true;
	}

	// Token: 0x06001BDD RID: 7133 RVA: 0x0006D91C File Offset: 0x0006BB1C
	public static bool RPC<P0, P1, P2, P3>(NetworkViewID viewID, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3>(networkView, messageName, targets, p0, p1, p2, p3);
		return true;
	}

	// Token: 0x06001BDE RID: 7134 RVA: 0x0006D964 File Offset: 0x0006BB64
	public static bool RPC<P0, P1, P2, P3, P4>(NetworkViewID viewID, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4>(networkView, flags, messageName, rpcMode, p0, p1, p2, p3, p4);
		return true;
	}

	// Token: 0x06001BDF RID: 7135 RVA: 0x0006D9B0 File Offset: 0x0006BBB0
	public static bool RPC<P0, P1, P2, P3, P4>(NetworkViewID viewID, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4>(networkView, flags, messageName, target, p0, p1, p2, p3, p4);
		return true;
	}

	// Token: 0x06001BE0 RID: 7136 RVA: 0x0006D9FC File Offset: 0x0006BBFC
	public static bool RPC<P0, P1, P2, P3, P4>(NetworkViewID viewID, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4>(networkView, flags, messageName, targets, p0, p1, p2, p3, p4);
		return true;
	}

	// Token: 0x06001BE1 RID: 7137 RVA: 0x0006DA48 File Offset: 0x0006BC48
	public static bool RPC<P0, P1, P2, P3, P4>(NetworkViewID viewID, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4>(networkView, messageName, rpcMode, p0, p1, p2, p3, p4);
		return true;
	}

	// Token: 0x06001BE2 RID: 7138 RVA: 0x0006DA94 File Offset: 0x0006BC94
	public static bool RPC<P0, P1, P2, P3, P4>(NetworkViewID viewID, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4>(networkView, messageName, target, p0, p1, p2, p3, p4);
		return true;
	}

	// Token: 0x06001BE3 RID: 7139 RVA: 0x0006DAE0 File Offset: 0x0006BCE0
	public static bool RPC<P0, P1, P2, P3, P4>(NetworkViewID viewID, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4>(networkView, messageName, targets, p0, p1, p2, p3, p4);
		return true;
	}

	// Token: 0x06001BE4 RID: 7140 RVA: 0x0006DB2C File Offset: 0x0006BD2C
	public static bool RPC<P0, P1, P2, P3, P4, P5>(NetworkViewID viewID, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5>(networkView, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5);
		return true;
	}

	// Token: 0x06001BE5 RID: 7141 RVA: 0x0006DB7C File Offset: 0x0006BD7C
	public static bool RPC<P0, P1, P2, P3, P4, P5>(NetworkViewID viewID, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5>(networkView, flags, messageName, target, p0, p1, p2, p3, p4, p5);
		return true;
	}

	// Token: 0x06001BE6 RID: 7142 RVA: 0x0006DBCC File Offset: 0x0006BDCC
	public static bool RPC<P0, P1, P2, P3, P4, P5>(NetworkViewID viewID, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5>(networkView, flags, messageName, targets, p0, p1, p2, p3, p4, p5);
		return true;
	}

	// Token: 0x06001BE7 RID: 7143 RVA: 0x0006DC1C File Offset: 0x0006BE1C
	public static bool RPC<P0, P1, P2, P3, P4, P5>(NetworkViewID viewID, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5>(networkView, messageName, rpcMode, p0, p1, p2, p3, p4, p5);
		return true;
	}

	// Token: 0x06001BE8 RID: 7144 RVA: 0x0006DC68 File Offset: 0x0006BE68
	public static bool RPC<P0, P1, P2, P3, P4, P5>(NetworkViewID viewID, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5>(networkView, messageName, target, p0, p1, p2, p3, p4, p5);
		return true;
	}

	// Token: 0x06001BE9 RID: 7145 RVA: 0x0006DCB4 File Offset: 0x0006BEB4
	public static bool RPC<P0, P1, P2, P3, P4, P5>(NetworkViewID viewID, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5>(networkView, messageName, targets, p0, p1, p2, p3, p4, p5);
		return true;
	}

	// Token: 0x06001BEA RID: 7146 RVA: 0x0006DD00 File Offset: 0x0006BF00
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(NetworkViewID viewID, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(networkView, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6);
		return true;
	}

	// Token: 0x06001BEB RID: 7147 RVA: 0x0006DD50 File Offset: 0x0006BF50
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(NetworkViewID viewID, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(networkView, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6);
		return true;
	}

	// Token: 0x06001BEC RID: 7148 RVA: 0x0006DDA0 File Offset: 0x0006BFA0
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(NetworkViewID viewID, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(networkView, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6);
		return true;
	}

	// Token: 0x06001BED RID: 7149 RVA: 0x0006DDF0 File Offset: 0x0006BFF0
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(NetworkViewID viewID, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(networkView, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6);
		return true;
	}

	// Token: 0x06001BEE RID: 7150 RVA: 0x0006DE40 File Offset: 0x0006C040
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(NetworkViewID viewID, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(networkView, messageName, target, p0, p1, p2, p3, p4, p5, p6);
		return true;
	}

	// Token: 0x06001BEF RID: 7151 RVA: 0x0006DE90 File Offset: 0x0006C090
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(NetworkViewID viewID, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(networkView, messageName, targets, p0, p1, p2, p3, p4, p5, p6);
		return true;
	}

	// Token: 0x06001BF0 RID: 7152 RVA: 0x0006DEE0 File Offset: 0x0006C0E0
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(NetworkViewID viewID, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(networkView, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7);
		return true;
	}

	// Token: 0x06001BF1 RID: 7153 RVA: 0x0006DF34 File Offset: 0x0006C134
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(NetworkViewID viewID, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(networkView, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7);
		return true;
	}

	// Token: 0x06001BF2 RID: 7154 RVA: 0x0006DF88 File Offset: 0x0006C188
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(NetworkViewID viewID, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(networkView, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7);
		return true;
	}

	// Token: 0x06001BF3 RID: 7155 RVA: 0x0006DFDC File Offset: 0x0006C1DC
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(NetworkViewID viewID, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(networkView, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7);
		return true;
	}

	// Token: 0x06001BF4 RID: 7156 RVA: 0x0006E02C File Offset: 0x0006C22C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(NetworkViewID viewID, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(networkView, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7);
		return true;
	}

	// Token: 0x06001BF5 RID: 7157 RVA: 0x0006E07C File Offset: 0x0006C27C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(NetworkViewID viewID, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(networkView, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7);
		return true;
	}

	// Token: 0x06001BF6 RID: 7158 RVA: 0x0006E0CC File Offset: 0x0006C2CC
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(NetworkViewID viewID, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(networkView, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8);
		return true;
	}

	// Token: 0x06001BF7 RID: 7159 RVA: 0x0006E120 File Offset: 0x0006C320
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(NetworkViewID viewID, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(networkView, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8);
		return true;
	}

	// Token: 0x06001BF8 RID: 7160 RVA: 0x0006E174 File Offset: 0x0006C374
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(NetworkViewID viewID, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(networkView, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8);
		return true;
	}

	// Token: 0x06001BF9 RID: 7161 RVA: 0x0006E1C8 File Offset: 0x0006C3C8
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(NetworkViewID viewID, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(networkView, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8);
		return true;
	}

	// Token: 0x06001BFA RID: 7162 RVA: 0x0006E21C File Offset: 0x0006C41C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(NetworkViewID viewID, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(networkView, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8);
		return true;
	}

	// Token: 0x06001BFB RID: 7163 RVA: 0x0006E270 File Offset: 0x0006C470
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(NetworkViewID viewID, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(networkView, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8);
		return true;
	}

	// Token: 0x06001BFC RID: 7164 RVA: 0x0006E2C4 File Offset: 0x0006C4C4
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(NetworkViewID viewID, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(networkView, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
		return true;
	}

	// Token: 0x06001BFD RID: 7165 RVA: 0x0006E31C File Offset: 0x0006C51C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(NetworkViewID viewID, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(networkView, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
		return true;
	}

	// Token: 0x06001BFE RID: 7166 RVA: 0x0006E374 File Offset: 0x0006C574
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(NetworkViewID viewID, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(networkView, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
		return true;
	}

	// Token: 0x06001BFF RID: 7167 RVA: 0x0006E3CC File Offset: 0x0006C5CC
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(NetworkViewID viewID, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(networkView, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
		return true;
	}

	// Token: 0x06001C00 RID: 7168 RVA: 0x0006E420 File Offset: 0x0006C620
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(NetworkViewID viewID, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(networkView, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
		return true;
	}

	// Token: 0x06001C01 RID: 7169 RVA: 0x0006E474 File Offset: 0x0006C674
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(NetworkViewID viewID, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(networkView, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
		return true;
	}

	// Token: 0x06001C02 RID: 7170 RVA: 0x0006E4C8 File Offset: 0x0006C6C8
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(NetworkViewID viewID, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(networkView, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
		return true;
	}

	// Token: 0x06001C03 RID: 7171 RVA: 0x0006E520 File Offset: 0x0006C720
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(NetworkViewID viewID, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(networkView, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
		return true;
	}

	// Token: 0x06001C04 RID: 7172 RVA: 0x0006E578 File Offset: 0x0006C778
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(NetworkViewID viewID, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(networkView, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
		return true;
	}

	// Token: 0x06001C05 RID: 7173 RVA: 0x0006E5D0 File Offset: 0x0006C7D0
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(NetworkViewID viewID, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(networkView, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
		return true;
	}

	// Token: 0x06001C06 RID: 7174 RVA: 0x0006E628 File Offset: 0x0006C828
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(NetworkViewID viewID, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(networkView, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
		return true;
	}

	// Token: 0x06001C07 RID: 7175 RVA: 0x0006E680 File Offset: 0x0006C880
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(NetworkViewID viewID, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(networkView, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
		return true;
	}

	// Token: 0x06001C08 RID: 7176 RVA: 0x0006E6D8 File Offset: 0x0006C8D8
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(NetworkViewID viewID, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(networkView, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
		return true;
	}

	// Token: 0x06001C09 RID: 7177 RVA: 0x0006E734 File Offset: 0x0006C934
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(NetworkViewID viewID, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(networkView, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
		return true;
	}

	// Token: 0x06001C0A RID: 7178 RVA: 0x0006E790 File Offset: 0x0006C990
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(NetworkViewID viewID, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(networkView, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
		return true;
	}

	// Token: 0x06001C0B RID: 7179 RVA: 0x0006E7EC File Offset: 0x0006C9EC
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(NetworkViewID viewID, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(networkView, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
		return true;
	}

	// Token: 0x06001C0C RID: 7180 RVA: 0x0006E844 File Offset: 0x0006CA44
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(NetworkViewID viewID, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(networkView, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
		return true;
	}

	// Token: 0x06001C0D RID: 7181 RVA: 0x0006E89C File Offset: 0x0006CA9C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(NetworkViewID viewID, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		NetworkView networkView = NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(networkView, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
		return true;
	}

	// Token: 0x06001C0E RID: 7182 RVA: 0x0006E8F4 File Offset: 0x0006CAF4
	public static bool RPC(NetEntityID entID, NetworkFlags flags, string messageName, RPCMode rpcMode)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC((NetworkViewID)entID, flags, messageName, rpcMode);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC(ngcView, flags, messageName, rpcMode);
		return true;
	}

	// Token: 0x06001C0F RID: 7183 RVA: 0x0006E954 File Offset: 0x0006CB54
	public static bool RPC(NetEntityID entID, NetworkFlags flags, string messageName, NetworkPlayer target)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC((NetworkViewID)entID, flags, messageName, target);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC(ngcView, flags, messageName, target);
		return true;
	}

	// Token: 0x06001C10 RID: 7184 RVA: 0x0006E9B4 File Offset: 0x0006CBB4
	public static bool RPC(NetEntityID entID, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC((NetworkViewID)entID, flags, messageName, targets);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC(ngcView, flags, messageName, targets);
		return true;
	}

	// Token: 0x06001C11 RID: 7185 RVA: 0x0006EA14 File Offset: 0x0006CC14
	public static bool RPC(NetEntityID entID, string messageName, RPCMode rpcMode)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC((NetworkViewID)entID, messageName, rpcMode);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC(ngcView, messageName, rpcMode);
		return true;
	}

	// Token: 0x06001C12 RID: 7186 RVA: 0x0006EA70 File Offset: 0x0006CC70
	public static bool RPC(NetEntityID entID, string messageName, NetworkPlayer target)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC((NetworkViewID)entID, messageName, target);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC(ngcView, messageName, target);
		return true;
	}

	// Token: 0x06001C13 RID: 7187 RVA: 0x0006EACC File Offset: 0x0006CCCC
	public static bool RPC(NetEntityID entID, string messageName, IEnumerable<NetworkPlayer> targets)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC((NetworkViewID)entID, messageName, targets);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC(ngcView, messageName, targets);
		return true;
	}

	// Token: 0x06001C14 RID: 7188 RVA: 0x0006EB28 File Offset: 0x0006CD28
	public static bool RPC<P0>(NetEntityID entID, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0>((NetworkViewID)entID, flags, messageName, rpcMode, p0);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0>(ngcView, flags, messageName, rpcMode, p0);
		return true;
	}

	// Token: 0x06001C15 RID: 7189 RVA: 0x0006EB8C File Offset: 0x0006CD8C
	public static bool RPC<P0>(NetEntityID entID, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0>((NetworkViewID)entID, flags, messageName, target, p0);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0>(ngcView, flags, messageName, target, p0);
		return true;
	}

	// Token: 0x06001C16 RID: 7190 RVA: 0x0006EBF0 File Offset: 0x0006CDF0
	public static bool RPC<P0>(NetEntityID entID, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0>((NetworkViewID)entID, flags, messageName, targets, p0);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0>(ngcView, flags, messageName, targets, p0);
		return true;
	}

	// Token: 0x06001C17 RID: 7191 RVA: 0x0006EC54 File Offset: 0x0006CE54
	public static bool RPC<P0>(NetEntityID entID, string messageName, RPCMode rpcMode, P0 p0)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0>((NetworkViewID)entID, messageName, rpcMode, p0);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0>(ngcView, messageName, rpcMode, p0);
		return true;
	}

	// Token: 0x06001C18 RID: 7192 RVA: 0x0006ECB4 File Offset: 0x0006CEB4
	public static bool RPC<P0>(NetEntityID entID, string messageName, NetworkPlayer target, P0 p0)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0>((NetworkViewID)entID, messageName, target, p0);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0>(ngcView, messageName, target, p0);
		return true;
	}

	// Token: 0x06001C19 RID: 7193 RVA: 0x0006ED14 File Offset: 0x0006CF14
	public static bool RPC<P0>(NetEntityID entID, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0>((NetworkViewID)entID, messageName, targets, p0);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0>(ngcView, messageName, targets, p0);
		return true;
	}

	// Token: 0x06001C1A RID: 7194 RVA: 0x0006ED74 File Offset: 0x0006CF74
	public static bool RPC<P0, P1>(NetEntityID entID, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1>((NetworkViewID)entID, flags, messageName, rpcMode, p0, p1);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1>(ngcView, flags, messageName, rpcMode, p0, p1);
		return true;
	}

	// Token: 0x06001C1B RID: 7195 RVA: 0x0006EDDC File Offset: 0x0006CFDC
	public static bool RPC<P0, P1>(NetEntityID entID, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1>((NetworkViewID)entID, flags, messageName, target, p0, p1);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1>(ngcView, flags, messageName, target, p0, p1);
		return true;
	}

	// Token: 0x06001C1C RID: 7196 RVA: 0x0006EE44 File Offset: 0x0006D044
	public static bool RPC<P0, P1>(NetEntityID entID, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1>((NetworkViewID)entID, flags, messageName, targets, p0, p1);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1>(ngcView, flags, messageName, targets, p0, p1);
		return true;
	}

	// Token: 0x06001C1D RID: 7197 RVA: 0x0006EEAC File Offset: 0x0006D0AC
	public static bool RPC<P0, P1>(NetEntityID entID, string messageName, RPCMode rpcMode, P0 p0, P1 p1)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1>((NetworkViewID)entID, messageName, rpcMode, p0, p1);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1>(ngcView, messageName, rpcMode, p0, p1);
		return true;
	}

	// Token: 0x06001C1E RID: 7198 RVA: 0x0006EF10 File Offset: 0x0006D110
	public static bool RPC<P0, P1>(NetEntityID entID, string messageName, NetworkPlayer target, P0 p0, P1 p1)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1>((NetworkViewID)entID, messageName, target, p0, p1);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1>(ngcView, messageName, target, p0, p1);
		return true;
	}

	// Token: 0x06001C1F RID: 7199 RVA: 0x0006EF74 File Offset: 0x0006D174
	public static bool RPC<P0, P1>(NetEntityID entID, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1>((NetworkViewID)entID, messageName, targets, p0, p1);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1>(ngcView, messageName, targets, p0, p1);
		return true;
	}

	// Token: 0x06001C20 RID: 7200 RVA: 0x0006EFD8 File Offset: 0x0006D1D8
	public static bool RPC<P0, P1, P2>(NetEntityID entID, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2>((NetworkViewID)entID, flags, messageName, rpcMode, p0, p1, p2);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2>(ngcView, flags, messageName, rpcMode, p0, p1, p2);
		return true;
	}

	// Token: 0x06001C21 RID: 7201 RVA: 0x0006F044 File Offset: 0x0006D244
	public static bool RPC<P0, P1, P2>(NetEntityID entID, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2>((NetworkViewID)entID, flags, messageName, target, p0, p1, p2);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2>(ngcView, flags, messageName, target, p0, p1, p2);
		return true;
	}

	// Token: 0x06001C22 RID: 7202 RVA: 0x0006F0B0 File Offset: 0x0006D2B0
	public static bool RPC<P0, P1, P2>(NetEntityID entID, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2>((NetworkViewID)entID, flags, messageName, targets, p0, p1, p2);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2>(ngcView, flags, messageName, targets, p0, p1, p2);
		return true;
	}

	// Token: 0x06001C23 RID: 7203 RVA: 0x0006F11C File Offset: 0x0006D31C
	public static bool RPC<P0, P1, P2>(NetEntityID entID, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2>((NetworkViewID)entID, messageName, rpcMode, p0, p1, p2);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2>(ngcView, messageName, rpcMode, p0, p1, p2);
		return true;
	}

	// Token: 0x06001C24 RID: 7204 RVA: 0x0006F184 File Offset: 0x0006D384
	public static bool RPC<P0, P1, P2>(NetEntityID entID, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2>((NetworkViewID)entID, messageName, target, p0, p1, p2);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2>(ngcView, messageName, target, p0, p1, p2);
		return true;
	}

	// Token: 0x06001C25 RID: 7205 RVA: 0x0006F1EC File Offset: 0x0006D3EC
	public static bool RPC<P0, P1, P2>(NetEntityID entID, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2>((NetworkViewID)entID, messageName, targets, p0, p1, p2);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2>(ngcView, messageName, targets, p0, p1, p2);
		return true;
	}

	// Token: 0x06001C26 RID: 7206 RVA: 0x0006F254 File Offset: 0x0006D454
	public static bool RPC<P0, P1, P2, P3>(NetEntityID entID, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3>((NetworkViewID)entID, flags, messageName, rpcMode, p0, p1, p2, p3);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3>(ngcView, flags, messageName, rpcMode, p0, p1, p2, p3);
		return true;
	}

	// Token: 0x06001C27 RID: 7207 RVA: 0x0006F2C4 File Offset: 0x0006D4C4
	public static bool RPC<P0, P1, P2, P3>(NetEntityID entID, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3>((NetworkViewID)entID, flags, messageName, target, p0, p1, p2, p3);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3>(ngcView, flags, messageName, target, p0, p1, p2, p3);
		return true;
	}

	// Token: 0x06001C28 RID: 7208 RVA: 0x0006F334 File Offset: 0x0006D534
	public static bool RPC<P0, P1, P2, P3>(NetEntityID entID, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3>((NetworkViewID)entID, flags, messageName, targets, p0, p1, p2, p3);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3>(ngcView, flags, messageName, targets, p0, p1, p2, p3);
		return true;
	}

	// Token: 0x06001C29 RID: 7209 RVA: 0x0006F3A4 File Offset: 0x0006D5A4
	public static bool RPC<P0, P1, P2, P3>(NetEntityID entID, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3>((NetworkViewID)entID, messageName, rpcMode, p0, p1, p2, p3);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3>(ngcView, messageName, rpcMode, p0, p1, p2, p3);
		return true;
	}

	// Token: 0x06001C2A RID: 7210 RVA: 0x0006F410 File Offset: 0x0006D610
	public static bool RPC<P0, P1, P2, P3>(NetEntityID entID, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3>((NetworkViewID)entID, messageName, target, p0, p1, p2, p3);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3>(ngcView, messageName, target, p0, p1, p2, p3);
		return true;
	}

	// Token: 0x06001C2B RID: 7211 RVA: 0x0006F47C File Offset: 0x0006D67C
	public static bool RPC<P0, P1, P2, P3>(NetEntityID entID, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3>((NetworkViewID)entID, messageName, targets, p0, p1, p2, p3);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3>(ngcView, messageName, targets, p0, p1, p2, p3);
		return true;
	}

	// Token: 0x06001C2C RID: 7212 RVA: 0x0006F4E8 File Offset: 0x0006D6E8
	public static bool RPC<P0, P1, P2, P3, P4>(NetEntityID entID, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4>((NetworkViewID)entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4>(ngcView, flags, messageName, rpcMode, p0, p1, p2, p3, p4);
		return true;
	}

	// Token: 0x06001C2D RID: 7213 RVA: 0x0006F55C File Offset: 0x0006D75C
	public static bool RPC<P0, P1, P2, P3, P4>(NetEntityID entID, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4>((NetworkViewID)entID, flags, messageName, target, p0, p1, p2, p3, p4);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4>(ngcView, flags, messageName, target, p0, p1, p2, p3, p4);
		return true;
	}

	// Token: 0x06001C2E RID: 7214 RVA: 0x0006F5D0 File Offset: 0x0006D7D0
	public static bool RPC<P0, P1, P2, P3, P4>(NetEntityID entID, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4>((NetworkViewID)entID, flags, messageName, targets, p0, p1, p2, p3, p4);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4>(ngcView, flags, messageName, targets, p0, p1, p2, p3, p4);
		return true;
	}

	// Token: 0x06001C2F RID: 7215 RVA: 0x0006F644 File Offset: 0x0006D844
	public static bool RPC<P0, P1, P2, P3, P4>(NetEntityID entID, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4>((NetworkViewID)entID, messageName, rpcMode, p0, p1, p2, p3, p4);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4>(ngcView, messageName, rpcMode, p0, p1, p2, p3, p4);
		return true;
	}

	// Token: 0x06001C30 RID: 7216 RVA: 0x0006F6B4 File Offset: 0x0006D8B4
	public static bool RPC<P0, P1, P2, P3, P4>(NetEntityID entID, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4>((NetworkViewID)entID, messageName, target, p0, p1, p2, p3, p4);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4>(ngcView, messageName, target, p0, p1, p2, p3, p4);
		return true;
	}

	// Token: 0x06001C31 RID: 7217 RVA: 0x0006F724 File Offset: 0x0006D924
	public static bool RPC<P0, P1, P2, P3, P4>(NetEntityID entID, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4>((NetworkViewID)entID, messageName, targets, p0, p1, p2, p3, p4);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4>(ngcView, messageName, targets, p0, p1, p2, p3, p4);
		return true;
	}

	// Token: 0x06001C32 RID: 7218 RVA: 0x0006F794 File Offset: 0x0006D994
	public static bool RPC<P0, P1, P2, P3, P4, P5>(NetEntityID entID, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4, P5>((NetworkViewID)entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5>(ngcView, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5);
		return true;
	}

	// Token: 0x06001C33 RID: 7219 RVA: 0x0006F80C File Offset: 0x0006DA0C
	public static bool RPC<P0, P1, P2, P3, P4, P5>(NetEntityID entID, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4, P5>((NetworkViewID)entID, flags, messageName, target, p0, p1, p2, p3, p4, p5);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5>(ngcView, flags, messageName, target, p0, p1, p2, p3, p4, p5);
		return true;
	}

	// Token: 0x06001C34 RID: 7220 RVA: 0x0006F884 File Offset: 0x0006DA84
	public static bool RPC<P0, P1, P2, P3, P4, P5>(NetEntityID entID, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4, P5>((NetworkViewID)entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5>(ngcView, flags, messageName, targets, p0, p1, p2, p3, p4, p5);
		return true;
	}

	// Token: 0x06001C35 RID: 7221 RVA: 0x0006F8FC File Offset: 0x0006DAFC
	public static bool RPC<P0, P1, P2, P3, P4, P5>(NetEntityID entID, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4, P5>((NetworkViewID)entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5>(ngcView, messageName, rpcMode, p0, p1, p2, p3, p4, p5);
		return true;
	}

	// Token: 0x06001C36 RID: 7222 RVA: 0x0006F970 File Offset: 0x0006DB70
	public static bool RPC<P0, P1, P2, P3, P4, P5>(NetEntityID entID, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4, P5>((NetworkViewID)entID, messageName, target, p0, p1, p2, p3, p4, p5);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5>(ngcView, messageName, target, p0, p1, p2, p3, p4, p5);
		return true;
	}

	// Token: 0x06001C37 RID: 7223 RVA: 0x0006F9E4 File Offset: 0x0006DBE4
	public static bool RPC<P0, P1, P2, P3, P4, P5>(NetEntityID entID, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4, P5>((NetworkViewID)entID, messageName, targets, p0, p1, p2, p3, p4, p5);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5>(ngcView, messageName, targets, p0, p1, p2, p3, p4, p5);
		return true;
	}

	// Token: 0x06001C38 RID: 7224 RVA: 0x0006FA58 File Offset: 0x0006DC58
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(NetEntityID entID, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>((NetworkViewID)entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(ngcView, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6);
		return true;
	}

	// Token: 0x06001C39 RID: 7225 RVA: 0x0006FAD4 File Offset: 0x0006DCD4
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(NetEntityID entID, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>((NetworkViewID)entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(ngcView, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6);
		return true;
	}

	// Token: 0x06001C3A RID: 7226 RVA: 0x0006FB50 File Offset: 0x0006DD50
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(NetEntityID entID, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>((NetworkViewID)entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(ngcView, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6);
		return true;
	}

	// Token: 0x06001C3B RID: 7227 RVA: 0x0006FBCC File Offset: 0x0006DDCC
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(NetEntityID entID, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>((NetworkViewID)entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(ngcView, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6);
		return true;
	}

	// Token: 0x06001C3C RID: 7228 RVA: 0x0006FC44 File Offset: 0x0006DE44
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(NetEntityID entID, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>((NetworkViewID)entID, messageName, target, p0, p1, p2, p3, p4, p5, p6);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(ngcView, messageName, target, p0, p1, p2, p3, p4, p5, p6);
		return true;
	}

	// Token: 0x06001C3D RID: 7229 RVA: 0x0006FCBC File Offset: 0x0006DEBC
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(NetEntityID entID, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>((NetworkViewID)entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(ngcView, messageName, targets, p0, p1, p2, p3, p4, p5, p6);
		return true;
	}

	// Token: 0x06001C3E RID: 7230 RVA: 0x0006FD34 File Offset: 0x0006DF34
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(NetEntityID entID, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>((NetworkViewID)entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(ngcView, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7);
		return true;
	}

	// Token: 0x06001C3F RID: 7231 RVA: 0x0006FDB4 File Offset: 0x0006DFB4
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(NetEntityID entID, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>((NetworkViewID)entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(ngcView, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7);
		return true;
	}

	// Token: 0x06001C40 RID: 7232 RVA: 0x0006FE34 File Offset: 0x0006E034
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(NetEntityID entID, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>((NetworkViewID)entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(ngcView, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7);
		return true;
	}

	// Token: 0x06001C41 RID: 7233 RVA: 0x0006FEB4 File Offset: 0x0006E0B4
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(NetEntityID entID, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>((NetworkViewID)entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(ngcView, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7);
		return true;
	}

	// Token: 0x06001C42 RID: 7234 RVA: 0x0006FF30 File Offset: 0x0006E130
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(NetEntityID entID, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>((NetworkViewID)entID, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(ngcView, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7);
		return true;
	}

	// Token: 0x06001C43 RID: 7235 RVA: 0x0006FFAC File Offset: 0x0006E1AC
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(NetEntityID entID, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>((NetworkViewID)entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(ngcView, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7);
		return true;
	}

	// Token: 0x06001C44 RID: 7236 RVA: 0x00070028 File Offset: 0x0006E228
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(NetEntityID entID, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>((NetworkViewID)entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(ngcView, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8);
		return true;
	}

	// Token: 0x06001C45 RID: 7237 RVA: 0x000700AC File Offset: 0x0006E2AC
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(NetEntityID entID, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>((NetworkViewID)entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(ngcView, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8);
		return true;
	}

	// Token: 0x06001C46 RID: 7238 RVA: 0x00070130 File Offset: 0x0006E330
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(NetEntityID entID, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>((NetworkViewID)entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(ngcView, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8);
		return true;
	}

	// Token: 0x06001C47 RID: 7239 RVA: 0x000701B4 File Offset: 0x0006E3B4
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(NetEntityID entID, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>((NetworkViewID)entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(ngcView, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8);
		return true;
	}

	// Token: 0x06001C48 RID: 7240 RVA: 0x00070234 File Offset: 0x0006E434
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(NetEntityID entID, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>((NetworkViewID)entID, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(ngcView, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8);
		return true;
	}

	// Token: 0x06001C49 RID: 7241 RVA: 0x000702B4 File Offset: 0x0006E4B4
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(NetEntityID entID, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>((NetworkViewID)entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(ngcView, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8);
		return true;
	}

	// Token: 0x06001C4A RID: 7242 RVA: 0x00070334 File Offset: 0x0006E534
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(NetEntityID entID, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>((NetworkViewID)entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(ngcView, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
		return true;
	}

	// Token: 0x06001C4B RID: 7243 RVA: 0x000703BC File Offset: 0x0006E5BC
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(NetEntityID entID, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>((NetworkViewID)entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(ngcView, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
		return true;
	}

	// Token: 0x06001C4C RID: 7244 RVA: 0x00070444 File Offset: 0x0006E644
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(NetEntityID entID, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>((NetworkViewID)entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(ngcView, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
		return true;
	}

	// Token: 0x06001C4D RID: 7245 RVA: 0x000704CC File Offset: 0x0006E6CC
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(NetEntityID entID, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>((NetworkViewID)entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(ngcView, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
		return true;
	}

	// Token: 0x06001C4E RID: 7246 RVA: 0x00070550 File Offset: 0x0006E750
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(NetEntityID entID, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>((NetworkViewID)entID, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(ngcView, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
		return true;
	}

	// Token: 0x06001C4F RID: 7247 RVA: 0x000705D4 File Offset: 0x0006E7D4
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(NetEntityID entID, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>((NetworkViewID)entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(ngcView, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
		return true;
	}

	// Token: 0x06001C50 RID: 7248 RVA: 0x00070658 File Offset: 0x0006E858
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(NetEntityID entID, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>((NetworkViewID)entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(ngcView, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
		return true;
	}

	// Token: 0x06001C51 RID: 7249 RVA: 0x000706E4 File Offset: 0x0006E8E4
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(NetEntityID entID, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>((NetworkViewID)entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(ngcView, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
		return true;
	}

	// Token: 0x06001C52 RID: 7250 RVA: 0x00070770 File Offset: 0x0006E970
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(NetEntityID entID, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>((NetworkViewID)entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(ngcView, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
		return true;
	}

	// Token: 0x06001C53 RID: 7251 RVA: 0x000707FC File Offset: 0x0006E9FC
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(NetEntityID entID, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>((NetworkViewID)entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(ngcView, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
		return true;
	}

	// Token: 0x06001C54 RID: 7252 RVA: 0x00070884 File Offset: 0x0006EA84
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(NetEntityID entID, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>((NetworkViewID)entID, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(ngcView, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
		return true;
	}

	// Token: 0x06001C55 RID: 7253 RVA: 0x0007090C File Offset: 0x0006EB0C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(NetEntityID entID, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>((NetworkViewID)entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(ngcView, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
		return true;
	}

	// Token: 0x06001C56 RID: 7254 RVA: 0x00070994 File Offset: 0x0006EB94
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(NetEntityID entID, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>((NetworkViewID)entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(ngcView, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
		return true;
	}

	// Token: 0x06001C57 RID: 7255 RVA: 0x00070A24 File Offset: 0x0006EC24
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(NetEntityID entID, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>((NetworkViewID)entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(ngcView, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
		return true;
	}

	// Token: 0x06001C58 RID: 7256 RVA: 0x00070AB4 File Offset: 0x0006ECB4
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(NetEntityID entID, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>((NetworkViewID)entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(ngcView, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
		return true;
	}

	// Token: 0x06001C59 RID: 7257 RVA: 0x00070B44 File Offset: 0x0006ED44
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(NetEntityID entID, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>((NetworkViewID)entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(ngcView, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
		return true;
	}

	// Token: 0x06001C5A RID: 7258 RVA: 0x00070BD0 File Offset: 0x0006EDD0
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(NetEntityID entID, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>((NetworkViewID)entID, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(ngcView, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
		return true;
	}

	// Token: 0x06001C5B RID: 7259 RVA: 0x00070C5C File Offset: 0x0006EE5C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(NetEntityID entID, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		if (!entID.isNGC)
		{
			return NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>((NetworkViewID)entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
		}
		NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(ngcView, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
		return true;
	}

	// Token: 0x06001C5C RID: 7260 RVA: 0x00070CE8 File Offset: 0x0006EEE8
	public static bool RPC(GameObject entity, NetworkFlags flags, string messageName, RPCMode rpcMode)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC(entID, flags, messageName, rpcMode);
	}

	// Token: 0x06001C5D RID: 7261 RVA: 0x00070D10 File Offset: 0x0006EF10
	public static bool RPC(GameObject entity, NetworkFlags flags, string messageName, NetworkPlayer target)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC(entID, flags, messageName, target);
	}

	// Token: 0x06001C5E RID: 7262 RVA: 0x00070D38 File Offset: 0x0006EF38
	public static bool RPC(GameObject entity, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC(entID, flags, messageName, targets);
	}

	// Token: 0x06001C5F RID: 7263 RVA: 0x00070D60 File Offset: 0x0006EF60
	public static bool RPC(GameObject entity, string messageName, RPCMode rpcMode)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC(entID, messageName, rpcMode);
	}

	// Token: 0x06001C60 RID: 7264 RVA: 0x00070D88 File Offset: 0x0006EF88
	public static bool RPC(GameObject entity, string messageName, NetworkPlayer target)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC(entID, messageName, target);
	}

	// Token: 0x06001C61 RID: 7265 RVA: 0x00070DB0 File Offset: 0x0006EFB0
	public static bool RPC(GameObject entity, string messageName, IEnumerable<NetworkPlayer> targets)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC(entID, messageName, targets);
	}

	// Token: 0x06001C62 RID: 7266 RVA: 0x00070DD8 File Offset: 0x0006EFD8
	public static bool RPC(MonoBehaviour entityScript, NetworkFlags flags, string messageName, RPCMode rpcMode)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC(entID, flags, messageName, rpcMode);
	}

	// Token: 0x06001C63 RID: 7267 RVA: 0x00070E00 File Offset: 0x0006F000
	public static bool RPC(MonoBehaviour entityScript, NetworkFlags flags, string messageName, NetworkPlayer target)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC(entID, flags, messageName, target);
	}

	// Token: 0x06001C64 RID: 7268 RVA: 0x00070E28 File Offset: 0x0006F028
	public static bool RPC(MonoBehaviour entityScript, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC(entID, flags, messageName, targets);
	}

	// Token: 0x06001C65 RID: 7269 RVA: 0x00070E50 File Offset: 0x0006F050
	public static bool RPC(MonoBehaviour entityScript, string messageName, RPCMode rpcMode)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC(entID, messageName, rpcMode);
	}

	// Token: 0x06001C66 RID: 7270 RVA: 0x00070E78 File Offset: 0x0006F078
	public static bool RPC(MonoBehaviour entityScript, string messageName, NetworkPlayer target)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC(entID, messageName, target);
	}

	// Token: 0x06001C67 RID: 7271 RVA: 0x00070EA0 File Offset: 0x0006F0A0
	public static bool RPC(MonoBehaviour entityScript, string messageName, IEnumerable<NetworkPlayer> targets)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC(entID, messageName, targets);
	}

	// Token: 0x06001C68 RID: 7272 RVA: 0x00070EC8 File Offset: 0x0006F0C8
	public static bool RPC(Component entityComponent, NetworkFlags flags, string messageName, RPCMode rpcMode)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC(entID, flags, messageName, rpcMode);
	}

	// Token: 0x06001C69 RID: 7273 RVA: 0x00070EF0 File Offset: 0x0006F0F0
	public static bool RPC(Component entityComponent, NetworkFlags flags, string messageName, NetworkPlayer target)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC(entID, flags, messageName, target);
	}

	// Token: 0x06001C6A RID: 7274 RVA: 0x00070F18 File Offset: 0x0006F118
	public static bool RPC(Component entityComponent, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC(entID, flags, messageName, targets);
	}

	// Token: 0x06001C6B RID: 7275 RVA: 0x00070F40 File Offset: 0x0006F140
	public static bool RPC(Component entityComponent, string messageName, RPCMode rpcMode)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC(entID, messageName, rpcMode);
	}

	// Token: 0x06001C6C RID: 7276 RVA: 0x00070F68 File Offset: 0x0006F168
	public static bool RPC(Component entityComponent, string messageName, NetworkPlayer target)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC(entID, messageName, target);
	}

	// Token: 0x06001C6D RID: 7277 RVA: 0x00070F90 File Offset: 0x0006F190
	public static bool RPC(Component entityComponent, string messageName, IEnumerable<NetworkPlayer> targets)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC(entID, messageName, targets);
	}

	// Token: 0x06001C6E RID: 7278 RVA: 0x00070FB8 File Offset: 0x0006F1B8
	public static bool RPC<P0>(GameObject entity, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0>(entID, flags, messageName, rpcMode, p0);
	}

	// Token: 0x06001C6F RID: 7279 RVA: 0x00070FE0 File Offset: 0x0006F1E0
	public static bool RPC<P0>(GameObject entity, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0>(entID, flags, messageName, target, p0);
	}

	// Token: 0x06001C70 RID: 7280 RVA: 0x00071008 File Offset: 0x0006F208
	public static bool RPC<P0>(GameObject entity, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0>(entID, flags, messageName, targets, p0);
	}

	// Token: 0x06001C71 RID: 7281 RVA: 0x00071030 File Offset: 0x0006F230
	public static bool RPC<P0>(GameObject entity, string messageName, RPCMode rpcMode, P0 p0)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0>(entID, messageName, rpcMode, p0);
	}

	// Token: 0x06001C72 RID: 7282 RVA: 0x00071058 File Offset: 0x0006F258
	public static bool RPC<P0>(GameObject entity, string messageName, NetworkPlayer target, P0 p0)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0>(entID, messageName, target, p0);
	}

	// Token: 0x06001C73 RID: 7283 RVA: 0x00071080 File Offset: 0x0006F280
	public static bool RPC<P0>(GameObject entity, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0>(entID, messageName, targets, p0);
	}

	// Token: 0x06001C74 RID: 7284 RVA: 0x000710A8 File Offset: 0x0006F2A8
	public static bool RPC<P0>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0>(entID, flags, messageName, rpcMode, p0);
	}

	// Token: 0x06001C75 RID: 7285 RVA: 0x000710D0 File Offset: 0x0006F2D0
	public static bool RPC<P0>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0>(entID, flags, messageName, target, p0);
	}

	// Token: 0x06001C76 RID: 7286 RVA: 0x000710F8 File Offset: 0x0006F2F8
	public static bool RPC<P0>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0>(entID, flags, messageName, targets, p0);
	}

	// Token: 0x06001C77 RID: 7287 RVA: 0x00071120 File Offset: 0x0006F320
	public static bool RPC<P0>(MonoBehaviour entityScript, string messageName, RPCMode rpcMode, P0 p0)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0>(entID, messageName, rpcMode, p0);
	}

	// Token: 0x06001C78 RID: 7288 RVA: 0x00071148 File Offset: 0x0006F348
	public static bool RPC<P0>(MonoBehaviour entityScript, string messageName, NetworkPlayer target, P0 p0)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0>(entID, messageName, target, p0);
	}

	// Token: 0x06001C79 RID: 7289 RVA: 0x00071170 File Offset: 0x0006F370
	public static bool RPC<P0>(MonoBehaviour entityScript, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0>(entID, messageName, targets, p0);
	}

	// Token: 0x06001C7A RID: 7290 RVA: 0x00071198 File Offset: 0x0006F398
	public static bool RPC<P0>(Component entityComponent, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0>(entID, flags, messageName, rpcMode, p0);
	}

	// Token: 0x06001C7B RID: 7291 RVA: 0x000711C0 File Offset: 0x0006F3C0
	public static bool RPC<P0>(Component entityComponent, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0>(entID, flags, messageName, target, p0);
	}

	// Token: 0x06001C7C RID: 7292 RVA: 0x000711E8 File Offset: 0x0006F3E8
	public static bool RPC<P0>(Component entityComponent, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0>(entID, flags, messageName, targets, p0);
	}

	// Token: 0x06001C7D RID: 7293 RVA: 0x00071210 File Offset: 0x0006F410
	public static bool RPC<P0>(Component entityComponent, string messageName, RPCMode rpcMode, P0 p0)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0>(entID, messageName, rpcMode, p0);
	}

	// Token: 0x06001C7E RID: 7294 RVA: 0x00071238 File Offset: 0x0006F438
	public static bool RPC<P0>(Component entityComponent, string messageName, NetworkPlayer target, P0 p0)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0>(entID, messageName, target, p0);
	}

	// Token: 0x06001C7F RID: 7295 RVA: 0x00071260 File Offset: 0x0006F460
	public static bool RPC<P0>(Component entityComponent, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0>(entID, messageName, targets, p0);
	}

	// Token: 0x06001C80 RID: 7296 RVA: 0x00071288 File Offset: 0x0006F488
	public static bool RPC<P0, P1>(GameObject entity, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1>(entID, flags, messageName, rpcMode, p0, p1);
	}

	// Token: 0x06001C81 RID: 7297 RVA: 0x000712B4 File Offset: 0x0006F4B4
	public static bool RPC<P0, P1>(GameObject entity, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1>(entID, flags, messageName, target, p0, p1);
	}

	// Token: 0x06001C82 RID: 7298 RVA: 0x000712E0 File Offset: 0x0006F4E0
	public static bool RPC<P0, P1>(GameObject entity, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1>(entID, flags, messageName, targets, p0, p1);
	}

	// Token: 0x06001C83 RID: 7299 RVA: 0x0007130C File Offset: 0x0006F50C
	public static bool RPC<P0, P1>(GameObject entity, string messageName, RPCMode rpcMode, P0 p0, P1 p1)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1>(entID, messageName, rpcMode, p0, p1);
	}

	// Token: 0x06001C84 RID: 7300 RVA: 0x00071334 File Offset: 0x0006F534
	public static bool RPC<P0, P1>(GameObject entity, string messageName, NetworkPlayer target, P0 p0, P1 p1)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1>(entID, messageName, target, p0, p1);
	}

	// Token: 0x06001C85 RID: 7301 RVA: 0x0007135C File Offset: 0x0006F55C
	public static bool RPC<P0, P1>(GameObject entity, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1>(entID, messageName, targets, p0, p1);
	}

	// Token: 0x06001C86 RID: 7302 RVA: 0x00071384 File Offset: 0x0006F584
	public static bool RPC<P0, P1>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1>(entID, flags, messageName, rpcMode, p0, p1);
	}

	// Token: 0x06001C87 RID: 7303 RVA: 0x000713B0 File Offset: 0x0006F5B0
	public static bool RPC<P0, P1>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1>(entID, flags, messageName, target, p0, p1);
	}

	// Token: 0x06001C88 RID: 7304 RVA: 0x000713DC File Offset: 0x0006F5DC
	public static bool RPC<P0, P1>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1>(entID, flags, messageName, targets, p0, p1);
	}

	// Token: 0x06001C89 RID: 7305 RVA: 0x00071408 File Offset: 0x0006F608
	public static bool RPC<P0, P1>(MonoBehaviour entityScript, string messageName, RPCMode rpcMode, P0 p0, P1 p1)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1>(entID, messageName, rpcMode, p0, p1);
	}

	// Token: 0x06001C8A RID: 7306 RVA: 0x00071430 File Offset: 0x0006F630
	public static bool RPC<P0, P1>(MonoBehaviour entityScript, string messageName, NetworkPlayer target, P0 p0, P1 p1)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1>(entID, messageName, target, p0, p1);
	}

	// Token: 0x06001C8B RID: 7307 RVA: 0x00071458 File Offset: 0x0006F658
	public static bool RPC<P0, P1>(MonoBehaviour entityScript, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1>(entID, messageName, targets, p0, p1);
	}

	// Token: 0x06001C8C RID: 7308 RVA: 0x00071480 File Offset: 0x0006F680
	public static bool RPC<P0, P1>(Component entityComponent, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1>(entID, flags, messageName, rpcMode, p0, p1);
	}

	// Token: 0x06001C8D RID: 7309 RVA: 0x000714AC File Offset: 0x0006F6AC
	public static bool RPC<P0, P1>(Component entityComponent, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1>(entID, flags, messageName, target, p0, p1);
	}

	// Token: 0x06001C8E RID: 7310 RVA: 0x000714D8 File Offset: 0x0006F6D8
	public static bool RPC<P0, P1>(Component entityComponent, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1>(entID, flags, messageName, targets, p0, p1);
	}

	// Token: 0x06001C8F RID: 7311 RVA: 0x00071504 File Offset: 0x0006F704
	public static bool RPC<P0, P1>(Component entityComponent, string messageName, RPCMode rpcMode, P0 p0, P1 p1)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1>(entID, messageName, rpcMode, p0, p1);
	}

	// Token: 0x06001C90 RID: 7312 RVA: 0x0007152C File Offset: 0x0006F72C
	public static bool RPC<P0, P1>(Component entityComponent, string messageName, NetworkPlayer target, P0 p0, P1 p1)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1>(entID, messageName, target, p0, p1);
	}

	// Token: 0x06001C91 RID: 7313 RVA: 0x00071554 File Offset: 0x0006F754
	public static bool RPC<P0, P1>(Component entityComponent, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1>(entID, messageName, targets, p0, p1);
	}

	// Token: 0x06001C92 RID: 7314 RVA: 0x0007157C File Offset: 0x0006F77C
	public static bool RPC<P0, P1, P2>(GameObject entity, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2>(entID, flags, messageName, rpcMode, p0, p1, p2);
	}

	// Token: 0x06001C93 RID: 7315 RVA: 0x000715A8 File Offset: 0x0006F7A8
	public static bool RPC<P0, P1, P2>(GameObject entity, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2>(entID, flags, messageName, target, p0, p1, p2);
	}

	// Token: 0x06001C94 RID: 7316 RVA: 0x000715D4 File Offset: 0x0006F7D4
	public static bool RPC<P0, P1, P2>(GameObject entity, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2>(entID, flags, messageName, targets, p0, p1, p2);
	}

	// Token: 0x06001C95 RID: 7317 RVA: 0x00071600 File Offset: 0x0006F800
	public static bool RPC<P0, P1, P2>(GameObject entity, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2>(entID, messageName, rpcMode, p0, p1, p2);
	}

	// Token: 0x06001C96 RID: 7318 RVA: 0x0007162C File Offset: 0x0006F82C
	public static bool RPC<P0, P1, P2>(GameObject entity, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2>(entID, messageName, target, p0, p1, p2);
	}

	// Token: 0x06001C97 RID: 7319 RVA: 0x00071658 File Offset: 0x0006F858
	public static bool RPC<P0, P1, P2>(GameObject entity, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2>(entID, messageName, targets, p0, p1, p2);
	}

	// Token: 0x06001C98 RID: 7320 RVA: 0x00071684 File Offset: 0x0006F884
	public static bool RPC<P0, P1, P2>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2>(entID, flags, messageName, rpcMode, p0, p1, p2);
	}

	// Token: 0x06001C99 RID: 7321 RVA: 0x000716B0 File Offset: 0x0006F8B0
	public static bool RPC<P0, P1, P2>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2>(entID, flags, messageName, target, p0, p1, p2);
	}

	// Token: 0x06001C9A RID: 7322 RVA: 0x000716DC File Offset: 0x0006F8DC
	public static bool RPC<P0, P1, P2>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2>(entID, flags, messageName, targets, p0, p1, p2);
	}

	// Token: 0x06001C9B RID: 7323 RVA: 0x00071708 File Offset: 0x0006F908
	public static bool RPC<P0, P1, P2>(MonoBehaviour entityScript, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2>(entID, messageName, rpcMode, p0, p1, p2);
	}

	// Token: 0x06001C9C RID: 7324 RVA: 0x00071734 File Offset: 0x0006F934
	public static bool RPC<P0, P1, P2>(MonoBehaviour entityScript, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2>(entID, messageName, target, p0, p1, p2);
	}

	// Token: 0x06001C9D RID: 7325 RVA: 0x00071760 File Offset: 0x0006F960
	public static bool RPC<P0, P1, P2>(MonoBehaviour entityScript, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2>(entID, messageName, targets, p0, p1, p2);
	}

	// Token: 0x06001C9E RID: 7326 RVA: 0x0007178C File Offset: 0x0006F98C
	public static bool RPC<P0, P1, P2>(Component entityComponent, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2>(entID, flags, messageName, rpcMode, p0, p1, p2);
	}

	// Token: 0x06001C9F RID: 7327 RVA: 0x000717B8 File Offset: 0x0006F9B8
	public static bool RPC<P0, P1, P2>(Component entityComponent, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2>(entID, flags, messageName, target, p0, p1, p2);
	}

	// Token: 0x06001CA0 RID: 7328 RVA: 0x000717E4 File Offset: 0x0006F9E4
	public static bool RPC<P0, P1, P2>(Component entityComponent, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2>(entID, flags, messageName, targets, p0, p1, p2);
	}

	// Token: 0x06001CA1 RID: 7329 RVA: 0x00071810 File Offset: 0x0006FA10
	public static bool RPC<P0, P1, P2>(Component entityComponent, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2>(entID, messageName, rpcMode, p0, p1, p2);
	}

	// Token: 0x06001CA2 RID: 7330 RVA: 0x0007183C File Offset: 0x0006FA3C
	public static bool RPC<P0, P1, P2>(Component entityComponent, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2>(entID, messageName, target, p0, p1, p2);
	}

	// Token: 0x06001CA3 RID: 7331 RVA: 0x00071868 File Offset: 0x0006FA68
	public static bool RPC<P0, P1, P2>(Component entityComponent, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2>(entID, messageName, targets, p0, p1, p2);
	}

	// Token: 0x06001CA4 RID: 7332 RVA: 0x00071894 File Offset: 0x0006FA94
	public static bool RPC<P0, P1, P2, P3>(GameObject entity, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3>(entID, flags, messageName, rpcMode, p0, p1, p2, p3);
	}

	// Token: 0x06001CA5 RID: 7333 RVA: 0x000718C4 File Offset: 0x0006FAC4
	public static bool RPC<P0, P1, P2, P3>(GameObject entity, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3>(entID, flags, messageName, target, p0, p1, p2, p3);
	}

	// Token: 0x06001CA6 RID: 7334 RVA: 0x000718F4 File Offset: 0x0006FAF4
	public static bool RPC<P0, P1, P2, P3>(GameObject entity, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3>(entID, flags, messageName, targets, p0, p1, p2, p3);
	}

	// Token: 0x06001CA7 RID: 7335 RVA: 0x00071924 File Offset: 0x0006FB24
	public static bool RPC<P0, P1, P2, P3>(GameObject entity, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3>(entID, messageName, rpcMode, p0, p1, p2, p3);
	}

	// Token: 0x06001CA8 RID: 7336 RVA: 0x00071950 File Offset: 0x0006FB50
	public static bool RPC<P0, P1, P2, P3>(GameObject entity, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3>(entID, messageName, target, p0, p1, p2, p3);
	}

	// Token: 0x06001CA9 RID: 7337 RVA: 0x0007197C File Offset: 0x0006FB7C
	public static bool RPC<P0, P1, P2, P3>(GameObject entity, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3>(entID, messageName, targets, p0, p1, p2, p3);
	}

	// Token: 0x06001CAA RID: 7338 RVA: 0x000719A8 File Offset: 0x0006FBA8
	public static bool RPC<P0, P1, P2, P3>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3>(entID, flags, messageName, rpcMode, p0, p1, p2, p3);
	}

	// Token: 0x06001CAB RID: 7339 RVA: 0x000719D8 File Offset: 0x0006FBD8
	public static bool RPC<P0, P1, P2, P3>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3>(entID, flags, messageName, target, p0, p1, p2, p3);
	}

	// Token: 0x06001CAC RID: 7340 RVA: 0x00071A08 File Offset: 0x0006FC08
	public static bool RPC<P0, P1, P2, P3>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3>(entID, flags, messageName, targets, p0, p1, p2, p3);
	}

	// Token: 0x06001CAD RID: 7341 RVA: 0x00071A38 File Offset: 0x0006FC38
	public static bool RPC<P0, P1, P2, P3>(MonoBehaviour entityScript, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3>(entID, messageName, rpcMode, p0, p1, p2, p3);
	}

	// Token: 0x06001CAE RID: 7342 RVA: 0x00071A64 File Offset: 0x0006FC64
	public static bool RPC<P0, P1, P2, P3>(MonoBehaviour entityScript, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3>(entID, messageName, target, p0, p1, p2, p3);
	}

	// Token: 0x06001CAF RID: 7343 RVA: 0x00071A90 File Offset: 0x0006FC90
	public static bool RPC<P0, P1, P2, P3>(MonoBehaviour entityScript, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3>(entID, messageName, targets, p0, p1, p2, p3);
	}

	// Token: 0x06001CB0 RID: 7344 RVA: 0x00071ABC File Offset: 0x0006FCBC
	public static bool RPC<P0, P1, P2, P3>(Component entityComponent, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3>(entID, flags, messageName, rpcMode, p0, p1, p2, p3);
	}

	// Token: 0x06001CB1 RID: 7345 RVA: 0x00071AEC File Offset: 0x0006FCEC
	public static bool RPC<P0, P1, P2, P3>(Component entityComponent, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3>(entID, flags, messageName, target, p0, p1, p2, p3);
	}

	// Token: 0x06001CB2 RID: 7346 RVA: 0x00071B1C File Offset: 0x0006FD1C
	public static bool RPC<P0, P1, P2, P3>(Component entityComponent, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3>(entID, flags, messageName, targets, p0, p1, p2, p3);
	}

	// Token: 0x06001CB3 RID: 7347 RVA: 0x00071B4C File Offset: 0x0006FD4C
	public static bool RPC<P0, P1, P2, P3>(Component entityComponent, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3>(entID, messageName, rpcMode, p0, p1, p2, p3);
	}

	// Token: 0x06001CB4 RID: 7348 RVA: 0x00071B78 File Offset: 0x0006FD78
	public static bool RPC<P0, P1, P2, P3>(Component entityComponent, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3>(entID, messageName, target, p0, p1, p2, p3);
	}

	// Token: 0x06001CB5 RID: 7349 RVA: 0x00071BA4 File Offset: 0x0006FDA4
	public static bool RPC<P0, P1, P2, P3>(Component entityComponent, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3>(entID, messageName, targets, p0, p1, p2, p3);
	}

	// Token: 0x06001CB6 RID: 7350 RVA: 0x00071BD0 File Offset: 0x0006FDD0
	public static bool RPC<P0, P1, P2, P3, P4>(GameObject entity, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4);
	}

	// Token: 0x06001CB7 RID: 7351 RVA: 0x00071C00 File Offset: 0x0006FE00
	public static bool RPC<P0, P1, P2, P3, P4>(GameObject entity, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4>(entID, flags, messageName, target, p0, p1, p2, p3, p4);
	}

	// Token: 0x06001CB8 RID: 7352 RVA: 0x00071C30 File Offset: 0x0006FE30
	public static bool RPC<P0, P1, P2, P3, P4>(GameObject entity, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4>(entID, flags, messageName, targets, p0, p1, p2, p3, p4);
	}

	// Token: 0x06001CB9 RID: 7353 RVA: 0x00071C60 File Offset: 0x0006FE60
	public static bool RPC<P0, P1, P2, P3, P4>(GameObject entity, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4>(entID, messageName, rpcMode, p0, p1, p2, p3, p4);
	}

	// Token: 0x06001CBA RID: 7354 RVA: 0x00071C90 File Offset: 0x0006FE90
	public static bool RPC<P0, P1, P2, P3, P4>(GameObject entity, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4>(entID, messageName, target, p0, p1, p2, p3, p4);
	}

	// Token: 0x06001CBB RID: 7355 RVA: 0x00071CC0 File Offset: 0x0006FEC0
	public static bool RPC<P0, P1, P2, P3, P4>(GameObject entity, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4>(entID, messageName, targets, p0, p1, p2, p3, p4);
	}

	// Token: 0x06001CBC RID: 7356 RVA: 0x00071CF0 File Offset: 0x0006FEF0
	public static bool RPC<P0, P1, P2, P3, P4>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4);
	}

	// Token: 0x06001CBD RID: 7357 RVA: 0x00071D20 File Offset: 0x0006FF20
	public static bool RPC<P0, P1, P2, P3, P4>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4>(entID, flags, messageName, target, p0, p1, p2, p3, p4);
	}

	// Token: 0x06001CBE RID: 7358 RVA: 0x00071D50 File Offset: 0x0006FF50
	public static bool RPC<P0, P1, P2, P3, P4>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4>(entID, flags, messageName, targets, p0, p1, p2, p3, p4);
	}

	// Token: 0x06001CBF RID: 7359 RVA: 0x00071D80 File Offset: 0x0006FF80
	public static bool RPC<P0, P1, P2, P3, P4>(MonoBehaviour entityScript, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4>(entID, messageName, rpcMode, p0, p1, p2, p3, p4);
	}

	// Token: 0x06001CC0 RID: 7360 RVA: 0x00071DB0 File Offset: 0x0006FFB0
	public static bool RPC<P0, P1, P2, P3, P4>(MonoBehaviour entityScript, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4>(entID, messageName, target, p0, p1, p2, p3, p4);
	}

	// Token: 0x06001CC1 RID: 7361 RVA: 0x00071DE0 File Offset: 0x0006FFE0
	public static bool RPC<P0, P1, P2, P3, P4>(MonoBehaviour entityScript, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4>(entID, messageName, targets, p0, p1, p2, p3, p4);
	}

	// Token: 0x06001CC2 RID: 7362 RVA: 0x00071E10 File Offset: 0x00070010
	public static bool RPC<P0, P1, P2, P3, P4>(Component entityComponent, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4);
	}

	// Token: 0x06001CC3 RID: 7363 RVA: 0x00071E40 File Offset: 0x00070040
	public static bool RPC<P0, P1, P2, P3, P4>(Component entityComponent, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4>(entID, flags, messageName, target, p0, p1, p2, p3, p4);
	}

	// Token: 0x06001CC4 RID: 7364 RVA: 0x00071E70 File Offset: 0x00070070
	public static bool RPC<P0, P1, P2, P3, P4>(Component entityComponent, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4>(entID, flags, messageName, targets, p0, p1, p2, p3, p4);
	}

	// Token: 0x06001CC5 RID: 7365 RVA: 0x00071EA0 File Offset: 0x000700A0
	public static bool RPC<P0, P1, P2, P3, P4>(Component entityComponent, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4>(entID, messageName, rpcMode, p0, p1, p2, p3, p4);
	}

	// Token: 0x06001CC6 RID: 7366 RVA: 0x00071ED0 File Offset: 0x000700D0
	public static bool RPC<P0, P1, P2, P3, P4>(Component entityComponent, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4>(entID, messageName, target, p0, p1, p2, p3, p4);
	}

	// Token: 0x06001CC7 RID: 7367 RVA: 0x00071F00 File Offset: 0x00070100
	public static bool RPC<P0, P1, P2, P3, P4>(Component entityComponent, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4>(entID, messageName, targets, p0, p1, p2, p3, p4);
	}

	// Token: 0x06001CC8 RID: 7368 RVA: 0x00071F30 File Offset: 0x00070130
	public static bool RPC<P0, P1, P2, P3, P4, P5>(GameObject entity, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x06001CC9 RID: 7369 RVA: 0x00071F64 File Offset: 0x00070164
	public static bool RPC<P0, P1, P2, P3, P4, P5>(GameObject entity, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5>(entID, flags, messageName, target, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x06001CCA RID: 7370 RVA: 0x00071F98 File Offset: 0x00070198
	public static bool RPC<P0, P1, P2, P3, P4, P5>(GameObject entity, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5>(entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x06001CCB RID: 7371 RVA: 0x00071FCC File Offset: 0x000701CC
	public static bool RPC<P0, P1, P2, P3, P4, P5>(GameObject entity, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5>(entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x06001CCC RID: 7372 RVA: 0x00071FFC File Offset: 0x000701FC
	public static bool RPC<P0, P1, P2, P3, P4, P5>(GameObject entity, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5>(entID, messageName, target, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x06001CCD RID: 7373 RVA: 0x0007202C File Offset: 0x0007022C
	public static bool RPC<P0, P1, P2, P3, P4, P5>(GameObject entity, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5>(entID, messageName, targets, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x06001CCE RID: 7374 RVA: 0x0007205C File Offset: 0x0007025C
	public static bool RPC<P0, P1, P2, P3, P4, P5>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x06001CCF RID: 7375 RVA: 0x00072090 File Offset: 0x00070290
	public static bool RPC<P0, P1, P2, P3, P4, P5>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5>(entID, flags, messageName, target, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x06001CD0 RID: 7376 RVA: 0x000720C4 File Offset: 0x000702C4
	public static bool RPC<P0, P1, P2, P3, P4, P5>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5>(entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x06001CD1 RID: 7377 RVA: 0x000720F8 File Offset: 0x000702F8
	public static bool RPC<P0, P1, P2, P3, P4, P5>(MonoBehaviour entityScript, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5>(entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x06001CD2 RID: 7378 RVA: 0x00072128 File Offset: 0x00070328
	public static bool RPC<P0, P1, P2, P3, P4, P5>(MonoBehaviour entityScript, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5>(entID, messageName, target, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x06001CD3 RID: 7379 RVA: 0x00072158 File Offset: 0x00070358
	public static bool RPC<P0, P1, P2, P3, P4, P5>(MonoBehaviour entityScript, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5>(entID, messageName, targets, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x06001CD4 RID: 7380 RVA: 0x00072188 File Offset: 0x00070388
	public static bool RPC<P0, P1, P2, P3, P4, P5>(Component entityComponent, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x06001CD5 RID: 7381 RVA: 0x000721BC File Offset: 0x000703BC
	public static bool RPC<P0, P1, P2, P3, P4, P5>(Component entityComponent, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5>(entID, flags, messageName, target, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x06001CD6 RID: 7382 RVA: 0x000721F0 File Offset: 0x000703F0
	public static bool RPC<P0, P1, P2, P3, P4, P5>(Component entityComponent, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5>(entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x06001CD7 RID: 7383 RVA: 0x00072224 File Offset: 0x00070424
	public static bool RPC<P0, P1, P2, P3, P4, P5>(Component entityComponent, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5>(entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x06001CD8 RID: 7384 RVA: 0x00072254 File Offset: 0x00070454
	public static bool RPC<P0, P1, P2, P3, P4, P5>(Component entityComponent, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5>(entID, messageName, target, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x06001CD9 RID: 7385 RVA: 0x00072284 File Offset: 0x00070484
	public static bool RPC<P0, P1, P2, P3, P4, P5>(Component entityComponent, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5>(entID, messageName, targets, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x06001CDA RID: 7386 RVA: 0x000722B4 File Offset: 0x000704B4
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(GameObject entity, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x06001CDB RID: 7387 RVA: 0x000722E8 File Offset: 0x000704E8
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(GameObject entity, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x06001CDC RID: 7388 RVA: 0x0007231C File Offset: 0x0007051C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(GameObject entity, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x06001CDD RID: 7389 RVA: 0x00072350 File Offset: 0x00070550
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(GameObject entity, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x06001CDE RID: 7390 RVA: 0x00072384 File Offset: 0x00070584
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(GameObject entity, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(entID, messageName, target, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x06001CDF RID: 7391 RVA: 0x000723B8 File Offset: 0x000705B8
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(GameObject entity, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x06001CE0 RID: 7392 RVA: 0x000723EC File Offset: 0x000705EC
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x06001CE1 RID: 7393 RVA: 0x00072420 File Offset: 0x00070620
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x06001CE2 RID: 7394 RVA: 0x00072454 File Offset: 0x00070654
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x06001CE3 RID: 7395 RVA: 0x00072488 File Offset: 0x00070688
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(MonoBehaviour entityScript, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x06001CE4 RID: 7396 RVA: 0x000724BC File Offset: 0x000706BC
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(MonoBehaviour entityScript, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(entID, messageName, target, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x06001CE5 RID: 7397 RVA: 0x000724F0 File Offset: 0x000706F0
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(MonoBehaviour entityScript, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x06001CE6 RID: 7398 RVA: 0x00072524 File Offset: 0x00070724
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(Component entityComponent, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x06001CE7 RID: 7399 RVA: 0x00072558 File Offset: 0x00070758
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(Component entityComponent, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x06001CE8 RID: 7400 RVA: 0x0007258C File Offset: 0x0007078C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(Component entityComponent, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x06001CE9 RID: 7401 RVA: 0x000725C0 File Offset: 0x000707C0
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(Component entityComponent, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x06001CEA RID: 7402 RVA: 0x000725F4 File Offset: 0x000707F4
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(Component entityComponent, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(entID, messageName, target, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x06001CEB RID: 7403 RVA: 0x00072628 File Offset: 0x00070828
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(Component entityComponent, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x06001CEC RID: 7404 RVA: 0x0007265C File Offset: 0x0007085C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(GameObject entity, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x06001CED RID: 7405 RVA: 0x00072694 File Offset: 0x00070894
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(GameObject entity, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x06001CEE RID: 7406 RVA: 0x000726CC File Offset: 0x000708CC
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(GameObject entity, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x06001CEF RID: 7407 RVA: 0x00072704 File Offset: 0x00070904
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(GameObject entity, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x06001CF0 RID: 7408 RVA: 0x00072738 File Offset: 0x00070938
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(GameObject entity, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(entID, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x06001CF1 RID: 7409 RVA: 0x0007276C File Offset: 0x0007096C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(GameObject entity, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x06001CF2 RID: 7410 RVA: 0x000727A0 File Offset: 0x000709A0
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x06001CF3 RID: 7411 RVA: 0x000727D8 File Offset: 0x000709D8
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x06001CF4 RID: 7412 RVA: 0x00072810 File Offset: 0x00070A10
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x06001CF5 RID: 7413 RVA: 0x00072848 File Offset: 0x00070A48
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(MonoBehaviour entityScript, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x06001CF6 RID: 7414 RVA: 0x0007287C File Offset: 0x00070A7C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(MonoBehaviour entityScript, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(entID, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x06001CF7 RID: 7415 RVA: 0x000728B0 File Offset: 0x00070AB0
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(MonoBehaviour entityScript, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x06001CF8 RID: 7416 RVA: 0x000728E4 File Offset: 0x00070AE4
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(Component entityComponent, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x06001CF9 RID: 7417 RVA: 0x0007291C File Offset: 0x00070B1C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(Component entityComponent, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x06001CFA RID: 7418 RVA: 0x00072954 File Offset: 0x00070B54
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(Component entityComponent, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x06001CFB RID: 7419 RVA: 0x0007298C File Offset: 0x00070B8C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(Component entityComponent, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x06001CFC RID: 7420 RVA: 0x000729C0 File Offset: 0x00070BC0
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(Component entityComponent, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(entID, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x06001CFD RID: 7421 RVA: 0x000729F4 File Offset: 0x00070BF4
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(Component entityComponent, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x06001CFE RID: 7422 RVA: 0x00072A28 File Offset: 0x00070C28
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(GameObject entity, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x06001CFF RID: 7423 RVA: 0x00072A60 File Offset: 0x00070C60
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(GameObject entity, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x06001D00 RID: 7424 RVA: 0x00072A98 File Offset: 0x00070C98
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(GameObject entity, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x06001D01 RID: 7425 RVA: 0x00072AD0 File Offset: 0x00070CD0
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(GameObject entity, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x06001D02 RID: 7426 RVA: 0x00072B08 File Offset: 0x00070D08
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(GameObject entity, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(entID, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x06001D03 RID: 7427 RVA: 0x00072B40 File Offset: 0x00070D40
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(GameObject entity, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x06001D04 RID: 7428 RVA: 0x00072B78 File Offset: 0x00070D78
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x06001D05 RID: 7429 RVA: 0x00072BB0 File Offset: 0x00070DB0
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x06001D06 RID: 7430 RVA: 0x00072BE8 File Offset: 0x00070DE8
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x06001D07 RID: 7431 RVA: 0x00072C20 File Offset: 0x00070E20
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(MonoBehaviour entityScript, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x06001D08 RID: 7432 RVA: 0x00072C58 File Offset: 0x00070E58
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(MonoBehaviour entityScript, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(entID, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x06001D09 RID: 7433 RVA: 0x00072C90 File Offset: 0x00070E90
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(MonoBehaviour entityScript, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x06001D0A RID: 7434 RVA: 0x00072CC8 File Offset: 0x00070EC8
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(Component entityComponent, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x06001D0B RID: 7435 RVA: 0x00072D00 File Offset: 0x00070F00
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(Component entityComponent, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x06001D0C RID: 7436 RVA: 0x00072D38 File Offset: 0x00070F38
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(Component entityComponent, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x06001D0D RID: 7437 RVA: 0x00072D70 File Offset: 0x00070F70
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(Component entityComponent, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x06001D0E RID: 7438 RVA: 0x00072DA8 File Offset: 0x00070FA8
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(Component entityComponent, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(entID, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x06001D0F RID: 7439 RVA: 0x00072DE0 File Offset: 0x00070FE0
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(Component entityComponent, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x06001D10 RID: 7440 RVA: 0x00072E18 File Offset: 0x00071018
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(GameObject entity, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x06001D11 RID: 7441 RVA: 0x00072E54 File Offset: 0x00071054
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(GameObject entity, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x06001D12 RID: 7442 RVA: 0x00072E90 File Offset: 0x00071090
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(GameObject entity, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x06001D13 RID: 7443 RVA: 0x00072ECC File Offset: 0x000710CC
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(GameObject entity, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x06001D14 RID: 7444 RVA: 0x00072F04 File Offset: 0x00071104
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(GameObject entity, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(entID, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x06001D15 RID: 7445 RVA: 0x00072F3C File Offset: 0x0007113C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(GameObject entity, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x06001D16 RID: 7446 RVA: 0x00072F74 File Offset: 0x00071174
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x06001D17 RID: 7447 RVA: 0x00072FB0 File Offset: 0x000711B0
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x06001D18 RID: 7448 RVA: 0x00072FEC File Offset: 0x000711EC
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x06001D19 RID: 7449 RVA: 0x00073028 File Offset: 0x00071228
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(MonoBehaviour entityScript, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x06001D1A RID: 7450 RVA: 0x00073060 File Offset: 0x00071260
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(MonoBehaviour entityScript, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(entID, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x06001D1B RID: 7451 RVA: 0x00073098 File Offset: 0x00071298
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(MonoBehaviour entityScript, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x06001D1C RID: 7452 RVA: 0x000730D0 File Offset: 0x000712D0
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(Component entityComponent, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x06001D1D RID: 7453 RVA: 0x0007310C File Offset: 0x0007130C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(Component entityComponent, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x06001D1E RID: 7454 RVA: 0x00073148 File Offset: 0x00071348
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(Component entityComponent, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x06001D1F RID: 7455 RVA: 0x00073184 File Offset: 0x00071384
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(Component entityComponent, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x06001D20 RID: 7456 RVA: 0x000731BC File Offset: 0x000713BC
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(Component entityComponent, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(entID, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x06001D21 RID: 7457 RVA: 0x000731F4 File Offset: 0x000713F4
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(Component entityComponent, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x06001D22 RID: 7458 RVA: 0x0007322C File Offset: 0x0007142C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(GameObject entity, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x06001D23 RID: 7459 RVA: 0x00073268 File Offset: 0x00071468
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(GameObject entity, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x06001D24 RID: 7460 RVA: 0x000732A4 File Offset: 0x000714A4
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(GameObject entity, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x06001D25 RID: 7461 RVA: 0x000732E0 File Offset: 0x000714E0
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(GameObject entity, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x06001D26 RID: 7462 RVA: 0x0007331C File Offset: 0x0007151C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(GameObject entity, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(entID, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x06001D27 RID: 7463 RVA: 0x00073358 File Offset: 0x00071558
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(GameObject entity, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x06001D28 RID: 7464 RVA: 0x00073394 File Offset: 0x00071594
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x06001D29 RID: 7465 RVA: 0x000733D0 File Offset: 0x000715D0
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x06001D2A RID: 7466 RVA: 0x0007340C File Offset: 0x0007160C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x06001D2B RID: 7467 RVA: 0x00073448 File Offset: 0x00071648
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(MonoBehaviour entityScript, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x06001D2C RID: 7468 RVA: 0x00073484 File Offset: 0x00071684
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(MonoBehaviour entityScript, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(entID, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x06001D2D RID: 7469 RVA: 0x000734C0 File Offset: 0x000716C0
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(MonoBehaviour entityScript, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x06001D2E RID: 7470 RVA: 0x000734FC File Offset: 0x000716FC
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(Component entityComponent, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x06001D2F RID: 7471 RVA: 0x00073538 File Offset: 0x00071738
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(Component entityComponent, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x06001D30 RID: 7472 RVA: 0x00073574 File Offset: 0x00071774
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(Component entityComponent, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x06001D31 RID: 7473 RVA: 0x000735B0 File Offset: 0x000717B0
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(Component entityComponent, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x06001D32 RID: 7474 RVA: 0x000735EC File Offset: 0x000717EC
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(Component entityComponent, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(entID, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x06001D33 RID: 7475 RVA: 0x00073628 File Offset: 0x00071828
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(Component entityComponent, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x06001D34 RID: 7476 RVA: 0x00073664 File Offset: 0x00071864
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(GameObject entity, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x06001D35 RID: 7477 RVA: 0x000736A4 File Offset: 0x000718A4
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(GameObject entity, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x06001D36 RID: 7478 RVA: 0x000736E4 File Offset: 0x000718E4
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(GameObject entity, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x06001D37 RID: 7479 RVA: 0x00073724 File Offset: 0x00071924
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(GameObject entity, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x06001D38 RID: 7480 RVA: 0x00073760 File Offset: 0x00071960
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(GameObject entity, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(entID, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x06001D39 RID: 7481 RVA: 0x0007379C File Offset: 0x0007199C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(GameObject entity, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entity, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x06001D3A RID: 7482 RVA: 0x000737D8 File Offset: 0x000719D8
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x06001D3B RID: 7483 RVA: 0x00073818 File Offset: 0x00071A18
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x06001D3C RID: 7484 RVA: 0x00073858 File Offset: 0x00071A58
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x06001D3D RID: 7485 RVA: 0x00073898 File Offset: 0x00071A98
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(MonoBehaviour entityScript, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x06001D3E RID: 7486 RVA: 0x000738D4 File Offset: 0x00071AD4
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(MonoBehaviour entityScript, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(entID, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x06001D3F RID: 7487 RVA: 0x00073910 File Offset: 0x00071B10
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(MonoBehaviour entityScript, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityScript, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x06001D40 RID: 7488 RVA: 0x0007394C File Offset: 0x00071B4C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(Component entityComponent, NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x06001D41 RID: 7489 RVA: 0x0007398C File Offset: 0x00071B8C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(Component entityComponent, NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x06001D42 RID: 7490 RVA: 0x000739CC File Offset: 0x00071BCC
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(Component entityComponent, NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x06001D43 RID: 7491 RVA: 0x00073A0C File Offset: 0x00071C0C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(Component entityComponent, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x06001D44 RID: 7492 RVA: 0x00073A48 File Offset: 0x00071C48
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(Component entityComponent, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(entID, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x06001D45 RID: 7493 RVA: 0x00073A84 File Offset: 0x00071C84
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(Component entityComponent, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		NetEntityID entID;
		return (int)NetEntityID.Of(entityComponent, out entID) != 0 && NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x06001D46 RID: 7494 RVA: 0x00073AC0 File Offset: 0x00071CC0
	[Conditional("SERVER")]
	public static void VerifyRPC(ref NetworkMessageInfo info, bool skipOwnerCheck = false)
	{
	}

	// Token: 0x06001D47 RID: 7495 RVA: 0x00073AC4 File Offset: 0x00071CC4
	private static void FreeViewIDOnly_Destroyer(NetworkView instance)
	{
	}

	// Token: 0x06001D48 RID: 7496 RVA: 0x00073AC8 File Offset: 0x00071CC8
	public static void DontDestroyWithNetwork(NetworkView view)
	{
		if (view)
		{
			view.instantiator.destroyer = NetCull.destroyerFreeViewIDOnly;
		}
	}

	// Token: 0x06001D49 RID: 7497 RVA: 0x00073AE8 File Offset: 0x00071CE8
	public static void DontDestroyWithNetwork(GameObject go)
	{
		if (go)
		{
			NetCull.DontDestroyWithNetwork(go.GetComponent<uLinkNetworkView>());
		}
	}

	// Token: 0x06001D4A RID: 7498 RVA: 0x00073B00 File Offset: 0x00071D00
	public static void DontDestroyWithNetwork(MonoBehaviour behaviour)
	{
		if (behaviour)
		{
			NetCull.DontDestroyWithNetwork(behaviour.networkView);
		}
	}

	// Token: 0x06001D4B RID: 7499 RVA: 0x00073B18 File Offset: 0x00071D18
	public static void DontDestroyWithNetwork(Component component)
	{
		if (component)
		{
			NetCull.DontDestroyWithNetwork(component.GetComponent<uLinkNetworkView>());
		}
	}

	// Token: 0x17000756 RID: 1878
	// (get) Token: 0x06001D4C RID: 7500 RVA: 0x00073B30 File Offset: 0x00071D30
	public static bool isClientRunning
	{
		get
		{
			return Network.isClient;
		}
	}

	// Token: 0x17000757 RID: 1879
	// (get) Token: 0x06001D4D RID: 7501 RVA: 0x00073B38 File Offset: 0x00071D38
	public static bool isServerRunning
	{
		get
		{
			return Network.isServer;
		}
	}

	// Token: 0x17000758 RID: 1880
	// (get) Token: 0x06001D4E RID: 7502 RVA: 0x00073B40 File Offset: 0x00071D40
	public static bool isNotRunning
	{
		get
		{
			return !Network.isClient && !Network.isServer;
		}
	}

	// Token: 0x17000759 RID: 1881
	// (get) Token: 0x06001D4F RID: 7503 RVA: 0x00073B58 File Offset: 0x00071D58
	public static bool isRunning
	{
		get
		{
			return Network.isClient || Network.isServer;
		}
	}

	// Token: 0x1700075A RID: 1882
	// (get) Token: 0x06001D50 RID: 7504 RVA: 0x00073B6C File Offset: 0x00071D6C
	[Obsolete("Use #if CLIENT (unless your trying to check if the client is connected.. then use NetCull.isClientRunning")]
	public static bool isClient
	{
		get
		{
			return NetCull.isClientRunning;
		}
	}

	// Token: 0x1700075B RID: 1883
	// (get) Token: 0x06001D51 RID: 7505 RVA: 0x00073B74 File Offset: 0x00071D74
	[Obsolete("Use #if SERVER (unless your trying to check if the server is running.. then use NetCull.isServerRunning")]
	public static bool isServer
	{
		get
		{
			return NetCull.isServerRunning;
		}
	}

	// Token: 0x1700075C RID: 1884
	// (get) Token: 0x06001D52 RID: 7506 RVA: 0x00073B7C File Offset: 0x00071D7C
	public static NetworkPlayer player
	{
		get
		{
			return Network.player;
		}
	}

	// Token: 0x1700075D RID: 1885
	// (get) Token: 0x06001D53 RID: 7507 RVA: 0x00073B84 File Offset: 0x00071D84
	public static double time
	{
		get
		{
			return Network.time;
		}
	}

	// Token: 0x1700075E RID: 1886
	// (get) Token: 0x06001D54 RID: 7508 RVA: 0x00073B8C File Offset: 0x00071D8C
	// (set) Token: 0x06001D55 RID: 7509 RVA: 0x00073B94 File Offset: 0x00071D94
	public static float sendRate
	{
		get
		{
			return Network.sendRate;
		}
		set
		{
			Network.sendRate = value;
			NetCull.Send.Rate = Network.sendRate;
			NetCull.Send.Interval = 1.0 / (double)NetCull.Send.Rate;
			NetCull.Send.IntervalF = (float)NetCull.Send.Interval;
			Interpolation.sendRate = NetCull.Send.Rate;
		}
	}

	// Token: 0x1700075F RID: 1887
	// (get) Token: 0x06001D56 RID: 7510 RVA: 0x00073BDC File Offset: 0x00071DDC
	public static double sendInterval
	{
		get
		{
			return NetCull.Send.Interval;
		}
	}

	// Token: 0x17000760 RID: 1888
	// (get) Token: 0x06001D57 RID: 7511 RVA: 0x00073BE4 File Offset: 0x00071DE4
	public static float sendIntervalF
	{
		get
		{
			return NetCull.Send.IntervalF;
		}
	}

	// Token: 0x17000761 RID: 1889
	// (get) Token: 0x06001D58 RID: 7512 RVA: 0x00073BEC File Offset: 0x00071DEC
	public static ulong timeInMillis
	{
		get
		{
			return Network.timeInMillis;
		}
	}

	// Token: 0x17000762 RID: 1890
	// (get) Token: 0x06001D59 RID: 7513 RVA: 0x00073BF4 File Offset: 0x00071DF4
	public static NetworkConfig config
	{
		get
		{
			return Network.config;
		}
	}

	// Token: 0x17000763 RID: 1891
	// (get) Token: 0x06001D5A RID: 7514 RVA: 0x00073BFC File Offset: 0x00071DFC
	public static NetworkPlayer[] connections
	{
		get
		{
			return Network.connections;
		}
	}

	// Token: 0x17000764 RID: 1892
	// (get) Token: 0x06001D5B RID: 7515 RVA: 0x00073C04 File Offset: 0x00071E04
	public static NetworkStatus status
	{
		get
		{
			return Network.status;
		}
	}

	// Token: 0x17000765 RID: 1893
	// (get) Token: 0x06001D5C RID: 7516 RVA: 0x00073C0C File Offset: 0x00071E0C
	public static double localTime
	{
		get
		{
			return Network.localTime;
		}
	}

	// Token: 0x17000766 RID: 1894
	// (get) Token: 0x06001D5D RID: 7517 RVA: 0x00073C14 File Offset: 0x00071E14
	public static ulong localTimeInMillis
	{
		get
		{
			return Network.localTimeInMillis;
		}
	}

	// Token: 0x17000767 RID: 1895
	// (get) Token: 0x06001D5E RID: 7518 RVA: 0x00073C1C File Offset: 0x00071E1C
	public static int listenPort
	{
		get
		{
			return Network.listenPort;
		}
	}

	// Token: 0x17000768 RID: 1896
	// (get) Token: 0x06001D5F RID: 7519 RVA: 0x00073C24 File Offset: 0x00071E24
	public static BitStream approvalData
	{
		get
		{
			return Network.approvalData;
		}
	}

	// Token: 0x17000769 RID: 1897
	// (get) Token: 0x06001D60 RID: 7520 RVA: 0x00073C2C File Offset: 0x00071E2C
	// (set) Token: 0x06001D61 RID: 7521 RVA: 0x00073C34 File Offset: 0x00071E34
	public static bool isMessageQueueRunning
	{
		get
		{
			return Network.isMessageQueueRunning;
		}
		set
		{
			Network.isMessageQueueRunning = value;
		}
	}

	// Token: 0x1700076A RID: 1898
	// (get) Token: 0x06001D62 RID: 7522 RVA: 0x00073C3C File Offset: 0x00071E3C
	// (set) Token: 0x06001D63 RID: 7523 RVA: 0x00073C48 File Offset: 0x00071E48
	public static NetError lastError
	{
		get
		{
			return Network.lastError.ToNetError();
		}
		set
		{
			Network.lastError = value._uLink();
		}
	}

	// Token: 0x06001D64 RID: 7524 RVA: 0x00073C58 File Offset: 0x00071E58
	public static void CloseConnection(NetworkPlayer target, bool sendDisconnectionNotification)
	{
		Network.CloseConnection(target, sendDisconnectionNotification, 3);
	}

	// Token: 0x06001D65 RID: 7525 RVA: 0x00073C64 File Offset: 0x00071E64
	public static void ResynchronizeClock(double durationInSeconds)
	{
		Network.ResynchronizeClock(durationInSeconds);
	}

	// Token: 0x06001D66 RID: 7526 RVA: 0x00073C6C File Offset: 0x00071E6C
	[Obsolete("void NetCull.ResynchronizeClock(ulong) is deprecated, Bla bla bla don't use this", true)]
	public static void ResynchronizeClock(ulong intervalMillis)
	{
		Network.ResynchronizeClock(intervalMillis);
	}

	// Token: 0x06001D67 RID: 7527 RVA: 0x00073C74 File Offset: 0x00071E74
	public static NetError Connect(string host, int remotePort, string password, params object[] loginData)
	{
		return Network.Connect(host, remotePort, password, loginData).ToNetError();
	}

	// Token: 0x06001D68 RID: 7528 RVA: 0x00073C84 File Offset: 0x00071E84
	public static void Disconnect(int timeout)
	{
		Network.Disconnect(timeout);
	}

	// Token: 0x06001D69 RID: 7529 RVA: 0x00073C8C File Offset: 0x00071E8C
	public static void Disconnect()
	{
		Network.Disconnect();
	}

	// Token: 0x06001D6A RID: 7530 RVA: 0x00073C94 File Offset: 0x00071E94
	public static void DisconnectImmediate()
	{
		Network.DisconnectImmediate();
	}

	// Token: 0x06001D6B RID: 7531 RVA: 0x00073C9C File Offset: 0x00071E9C
	public static void RegisterNetAutoPrefab(uLinkNetworkView viewPrefab)
	{
		if (viewPrefab)
		{
			string name = viewPrefab.name;
			try
			{
				NetCull.AutoPrefabs.all[name] = viewPrefab;
			}
			catch
			{
				Debug.LogError("skipped duplicate prefab named " + name, viewPrefab);
				return;
			}
			NetworkInstantiator.AddPrefab(viewPrefab.gameObject);
		}
	}

	// Token: 0x06001D6C RID: 7532 RVA: 0x00073D10 File Offset: 0x00071F10
	public static bool Found(this NetCull.PrefabSearch search)
	{
		return (int)search != 0;
	}

	// Token: 0x06001D6D RID: 7533 RVA: 0x00073D1C File Offset: 0x00071F1C
	public static bool Missing(this NetCull.PrefabSearch search)
	{
		return (int)search == 0;
	}

	// Token: 0x06001D6E RID: 7534 RVA: 0x00073D24 File Offset: 0x00071F24
	public static bool IsNGC(this NetCull.PrefabSearch search)
	{
		return (int)search == 1;
	}

	// Token: 0x06001D6F RID: 7535 RVA: 0x00073D2C File Offset: 0x00071F2C
	public static bool IsNet(this NetCull.PrefabSearch search)
	{
		return (int)search > 1;
	}

	// Token: 0x06001D70 RID: 7536 RVA: 0x00073D34 File Offset: 0x00071F34
	public static bool IsNetMainPrefab(this NetCull.PrefabSearch search)
	{
		return (int)search == 2;
	}

	// Token: 0x06001D71 RID: 7537 RVA: 0x00073D3C File Offset: 0x00071F3C
	public static bool IsNetAutoPrefab(this NetCull.PrefabSearch search)
	{
		return (int)search == 3;
	}

	// Token: 0x06001D72 RID: 7538 RVA: 0x00073D44 File Offset: 0x00071F44
	public static NetCull.PrefabSearch LoadPrefab(string prefabName, out GameObject prefab)
	{
		if (string.IsNullOrEmpty(prefabName))
		{
			prefab = null;
			return NetCull.PrefabSearch.Missing;
		}
		if (prefabName.StartsWith(":"))
		{
			try
			{
				prefab = NetMainPrefab.Lookup<GameObject>(prefabName);
				return (!prefab) ? NetCull.PrefabSearch.Missing : NetCull.PrefabSearch.NetMain;
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
				prefab = null;
				return NetCull.PrefabSearch.Missing;
			}
		}
		if (prefabName.StartsWith(";"))
		{
			try
			{
				NGC.Prefab prefab2;
				if (!NGC.Prefab.Register.Find(prefabName, out prefab2))
				{
					prefab = null;
					return NetCull.PrefabSearch.Missing;
				}
				NGCView prefab3 = prefab2.prefab;
				if (prefab3)
				{
					prefab = prefab3.gameObject;
					return (!prefab) ? NetCull.PrefabSearch.Missing : NetCull.PrefabSearch.NGC;
				}
				prefab = null;
				return NetCull.PrefabSearch.Missing;
			}
			catch (Exception ex2)
			{
				Debug.LogException(ex2);
				prefab = null;
				return NetCull.PrefabSearch.Missing;
			}
		}
		NetCull.PrefabSearch result;
		try
		{
			uLinkNetworkView uLinkNetworkView;
			if (NetCull.AutoPrefabs.all.TryGetValue(prefabName, out uLinkNetworkView) && uLinkNetworkView)
			{
				GameObject gameObject;
				prefab = (gameObject = uLinkNetworkView.gameObject);
				if (gameObject)
				{
					return NetCull.PrefabSearch.NetAuto;
				}
			}
			prefab = null;
			result = NetCull.PrefabSearch.Missing;
		}
		catch (Exception ex3)
		{
			Debug.LogException(ex3);
			prefab = null;
			result = NetCull.PrefabSearch.Missing;
		}
		return result;
	}

	// Token: 0x06001D73 RID: 7539 RVA: 0x00073EE8 File Offset: 0x000720E8
	public static GameObject LoadPrefab(string prefabName)
	{
		GameObject result;
		if ((int)NetCull.LoadPrefab(prefabName, out result) == 0)
		{
			throw new MissingReferenceException(prefabName);
		}
		return result;
	}

	// Token: 0x06001D74 RID: 7540 RVA: 0x00073F0C File Offset: 0x0007210C
	public static NetCull.PrefabSearch LoadPrefabScript<T>(string prefabName, out T script) where T : MonoBehaviour
	{
		MonoBehaviour monoBehaviour;
		NetCull.PrefabSearch prefabSearch = NetCull.LoadPrefabView(prefabName, out monoBehaviour);
		if ((int)prefabSearch == 0)
		{
			script = (T)((object)null);
		}
		else if (monoBehaviour is T)
		{
			script = (T)((object)monoBehaviour);
		}
		else
		{
			script = monoBehaviour.GetComponent<T>();
			if (!script)
			{
				prefabSearch = NetCull.PrefabSearch.Missing;
			}
		}
		return prefabSearch;
	}

	// Token: 0x06001D75 RID: 7541 RVA: 0x00073F7C File Offset: 0x0007217C
	public static T LoadPrefabScript<T>(string prefabName) where T : MonoBehaviour
	{
		T result;
		if ((int)NetCull.LoadPrefabScript<T>(prefabName, out result) == 0)
		{
			throw new MissingReferenceException(prefabName);
		}
		return result;
	}

	// Token: 0x06001D76 RID: 7542 RVA: 0x00073FA0 File Offset: 0x000721A0
	public static NetCull.PrefabSearch LoadPrefabComponent<T>(string prefabName, out T component) where T : Component
	{
		MonoBehaviour monoBehaviour;
		NetCull.PrefabSearch prefabSearch = NetCull.LoadPrefabView(prefabName, out monoBehaviour);
		if ((int)prefabSearch == 0)
		{
			component = (T)((object)null);
		}
		else if (typeof(MonoBehaviour).IsAssignableFrom(typeof(T)) && monoBehaviour is T)
		{
			component = (T)((object)monoBehaviour);
		}
		else
		{
			component = monoBehaviour.GetComponent<T>();
			if (!component)
			{
				prefabSearch = NetCull.PrefabSearch.Missing;
			}
		}
		return prefabSearch;
	}

	// Token: 0x06001D77 RID: 7543 RVA: 0x0007402C File Offset: 0x0007222C
	public static T LoadPrefabComponent<T>(string prefabName) where T : Component
	{
		T result;
		if ((int)NetCull.LoadPrefabComponent<T>(prefabName, out result) == 0)
		{
			throw new MissingReferenceException(prefabName);
		}
		return result;
	}

	// Token: 0x06001D78 RID: 7544 RVA: 0x00074050 File Offset: 0x00072250
	public static NetCull.PrefabSearch LoadPrefabView(string prefabName, out MonoBehaviour prefabView)
	{
		if (string.IsNullOrEmpty(prefabName))
		{
			prefabView = null;
			return NetCull.PrefabSearch.Missing;
		}
		if (prefabName.StartsWith(":"))
		{
			try
			{
				prefabView = NetMainPrefab.Lookup<uLinkNetworkView>(prefabName);
				return (!prefabView) ? NetCull.PrefabSearch.Missing : NetCull.PrefabSearch.NetMain;
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
				prefabView = null;
				return NetCull.PrefabSearch.Missing;
			}
		}
		if (prefabName.StartsWith(";"))
		{
			try
			{
				NGC.Prefab prefab;
				if (NGC.Prefab.Register.Find(prefabName, out prefab))
				{
					MonoBehaviour prefab2;
					prefabView = (prefab2 = prefab.prefab);
					if (prefab2)
					{
						return NetCull.PrefabSearch.NGC;
					}
				}
				prefabView = null;
				return NetCull.PrefabSearch.Missing;
			}
			catch (Exception ex2)
			{
				Debug.LogException(ex2);
				prefabView = null;
				return NetCull.PrefabSearch.Missing;
			}
		}
		NetCull.PrefabSearch result;
		try
		{
			uLinkNetworkView uLinkNetworkView;
			if (!NetCull.AutoPrefabs.all.TryGetValue(prefabName, out uLinkNetworkView) || !uLinkNetworkView)
			{
				prefabView = uLinkNetworkView;
				result = NetCull.PrefabSearch.Missing;
			}
			else
			{
				prefabView = null;
				result = NetCull.PrefabSearch.NetAuto;
			}
		}
		catch (Exception ex3)
		{
			Debug.LogException(ex3);
			prefabView = null;
			result = NetCull.PrefabSearch.Missing;
		}
		return result;
	}

	// Token: 0x06001D79 RID: 7545 RVA: 0x000741C0 File Offset: 0x000723C0
	public static MonoBehaviour LoadPrefabView(string prefabName)
	{
		MonoBehaviour result;
		if ((int)NetCull.LoadPrefabView(prefabName, out result) == 0)
		{
			throw new MissingReferenceException(prefabName);
		}
		return result;
	}

	// Token: 0x06001D7A RID: 7546 RVA: 0x000741E4 File Offset: 0x000723E4
	private static void OnPreUpdatePreCallbacks()
	{
	}

	// Token: 0x06001D7B RID: 7547 RVA: 0x000741E8 File Offset: 0x000723E8
	private static void OnPreUpdatePostCallbacks()
	{
	}

	// Token: 0x06001D7C RID: 7548 RVA: 0x000741EC File Offset: 0x000723EC
	private static void OnPostUpdatePreCallbacks()
	{
	}

	// Token: 0x06001D7D RID: 7549 RVA: 0x000741F0 File Offset: 0x000723F0
	private static void OnPostUpdatePostCallbacks()
	{
		Interpolator.SyncronizeAll();
		CharacterInterpolatorBase.SyncronizeAll();
	}

	// Token: 0x04000DF1 RID: 3569
	public const bool canDestroy = false;

	// Token: 0x04000DF2 RID: 3570
	public const bool canRemoveRPCs = false;

	// Token: 0x04000DF3 RID: 3571
	private const bool ensureCanDestroy = false;

	// Token: 0x04000DF4 RID: 3572
	private const bool ensureCanRemoveRPCS = false;

	// Token: 0x04000DF5 RID: 3573
	public const bool kServer = false;

	// Token: 0x04000DF6 RID: 3574
	public const bool kClient = true;

	// Token: 0x04000DF7 RID: 3575
	private static readonly NetworkInstantiator.Destroyer destroyerFreeViewIDOnly = new NetworkInstantiator.Destroyer(NetCull.FreeViewIDOnly_Destroyer);

	// Token: 0x020002F4 RID: 756
	public static class Callbacks
	{
		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06001D7E RID: 7550 RVA: 0x000741FC File Offset: 0x000723FC
		// (remove) Token: 0x06001D7F RID: 7551 RVA: 0x0007420C File Offset: 0x0007240C
		public static event NetCull.UpdateFunctor beforeEveryUpdate
		{
			add
			{
				NetCull.Callbacks.PRE.DELEGATE.Add(value, false);
			}
			remove
			{
				if (NetCull.Callbacks.MADE_PRE)
				{
					NetCull.Callbacks.PRE.DELEGATE.Remove(value);
				}
			}
		}

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06001D80 RID: 7552 RVA: 0x00074224 File Offset: 0x00072424
		// (remove) Token: 0x06001D81 RID: 7553 RVA: 0x00074234 File Offset: 0x00072434
		public static event NetCull.UpdateFunctor beforeNextUpdate
		{
			add
			{
				NetCull.Callbacks.PRE.DELEGATE.Add(value, true);
			}
			remove
			{
				if (NetCull.Callbacks.MADE_PRE)
				{
					NetCull.Callbacks.PRE.DELEGATE.Remove(value);
				}
			}
		}

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06001D82 RID: 7554 RVA: 0x0007424C File Offset: 0x0007244C
		// (remove) Token: 0x06001D83 RID: 7555 RVA: 0x0007425C File Offset: 0x0007245C
		public static event NetCull.UpdateFunctor afterEveryUpdate
		{
			add
			{
				NetCull.Callbacks.POST.DELEGATE.Add(value, false);
			}
			remove
			{
				if (NetCull.Callbacks.MADE_POST)
				{
					NetCull.Callbacks.POST.DELEGATE.Remove(value);
				}
			}
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06001D84 RID: 7556 RVA: 0x00074274 File Offset: 0x00072474
		// (remove) Token: 0x06001D85 RID: 7557 RVA: 0x00074284 File Offset: 0x00072484
		public static event NetCull.UpdateFunctor afterNextUpdate
		{
			add
			{
				NetCull.Callbacks.POST.DELEGATE.Add(value, true);
			}
			remove
			{
				if (NetCull.Callbacks.MADE_POST)
				{
					NetCull.Callbacks.POST.DELEGATE.Remove(value);
				}
			}
		}

		// Token: 0x06001D86 RID: 7558 RVA: 0x0007429C File Offset: 0x0007249C
		internal static void FirePreUpdate(NetPreUpdate preUpdate)
		{
			if (preUpdate != NetCull.Callbacks.netPreUpdate || !NetCull.Callbacks.Updating())
			{
				return;
			}
			NetCull.OnPreUpdatePreCallbacks();
			if (NetCull.Callbacks.MADE_PRE)
			{
				try
				{
					NetCull.Callbacks.PRE.DELEGATE.Invoke();
				}
				catch (Exception ex)
				{
					Debug.LogException(ex, preUpdate);
				}
			}
			NetCull.OnPreUpdatePostCallbacks();
		}

		// Token: 0x06001D87 RID: 7559 RVA: 0x00074310 File Offset: 0x00072510
		internal static void FirePostUpdate(NetPostUpdate postUpdate)
		{
			if (postUpdate != NetCull.Callbacks.netPostUpdate || !NetCull.Callbacks.Updating())
			{
				return;
			}
			NetCull.OnPostUpdatePreCallbacks();
			if (NetCull.Callbacks.MADE_POST)
			{
				try
				{
					NetCull.Callbacks.POST.DELEGATE.Invoke();
				}
				catch (Exception ex)
				{
					Debug.LogException(ex, postUpdate);
				}
			}
			NetCull.OnPostUpdatePostCallbacks();
		}

		// Token: 0x06001D88 RID: 7560 RVA: 0x00074384 File Offset: 0x00072584
		private static bool Updating()
		{
			if (!NetCull.Callbacks.internalHelper)
			{
				GameObject gameObject = GameObject.Find("uLinkInternalHelper");
				if (!gameObject)
				{
					return false;
				}
				NetCull.Callbacks.internalHelper = gameObject.GetComponent<InternalHelper>();
				if (!NetCull.Callbacks.internalHelper)
				{
					return false;
				}
			}
			return NetCull.Callbacks.internalHelper.enabled;
		}

		// Token: 0x06001D89 RID: 7561 RVA: 0x000743E0 File Offset: 0x000725E0
		private static void Replace<T>(ref T current, T replacement) where T : MonoBehaviour
		{
			if (current == replacement)
			{
				return;
			}
			if (current)
			{
				Debug.LogWarning(((!replacement) ? "Destroying " : "Replacing ") + typeof(T), current.gameObject);
				T t = current;
				NetCull.Callbacks.Resign<T>(ref current, current);
				if (t)
				{
					Object.Destroy(t);
				}
				if (replacement)
				{
					Debug.LogWarning("With " + typeof(T), replacement);
				}
			}
			current = replacement;
		}

		// Token: 0x06001D8A RID: 7562 RVA: 0x000744C4 File Offset: 0x000726C4
		private static void Resign<T>(ref T current, T resigning) where T : MonoBehaviour
		{
			if (current == resigning)
			{
				current = (T)((object)null);
			}
		}

		// Token: 0x06001D8B RID: 7563 RVA: 0x000744F0 File Offset: 0x000726F0
		internal static void BindUpdater(NetPreUpdate netUpdate)
		{
			NetCull.Callbacks.Replace<NetPreUpdate>(ref NetCull.Callbacks.netPreUpdate, netUpdate);
		}

		// Token: 0x06001D8C RID: 7564 RVA: 0x00074500 File Offset: 0x00072700
		internal static void BindUpdater(NetPostUpdate netUpdate)
		{
			NetCull.Callbacks.Replace<NetPostUpdate>(ref NetCull.Callbacks.netPostUpdate, netUpdate);
		}

		// Token: 0x06001D8D RID: 7565 RVA: 0x00074510 File Offset: 0x00072710
		internal static void ResignUpdater(NetPreUpdate netUpdate)
		{
			NetCull.Callbacks.Resign<NetPreUpdate>(ref NetCull.Callbacks.netPreUpdate, netUpdate);
		}

		// Token: 0x06001D8E RID: 7566 RVA: 0x00074520 File Offset: 0x00072720
		internal static void ResignUpdater(NetPostUpdate netUpdate)
		{
			NetCull.Callbacks.Resign<NetPostUpdate>(ref NetCull.Callbacks.netPostUpdate, netUpdate);
		}

		// Token: 0x04000DF8 RID: 3576
		private static bool MADE_PRE;

		// Token: 0x04000DF9 RID: 3577
		private static NetPreUpdate netPreUpdate;

		// Token: 0x04000DFA RID: 3578
		private static bool MADE_POST;

		// Token: 0x04000DFB RID: 3579
		private static NetPostUpdate netPostUpdate;

		// Token: 0x04000DFC RID: 3580
		private static InternalHelper internalHelper;

		// Token: 0x020002F5 RID: 757
		private class UpdateDelegate
		{
			// Token: 0x06001D90 RID: 7568 RVA: 0x00074588 File Offset: 0x00072788
			public void Invoke()
			{
				if (this.guarded || (this.count = this.list.Count) == 0)
				{
					return;
				}
				this.iterPosition = -1;
				try
				{
					this.guarded = true;
					this.iterPosition = -1;
					this.invokation.AddRange(this.list);
					HashSet<NetCull.UpdateFunctor> hashSet = (!this.onceSwap) ? this.once1 : this.once2;
					HashSet<NetCull.UpdateFunctor> hashSet2 = (!this.onceSwap) ? this.once2 : this.once1;
					hashSet2.Clear();
					hashSet2.UnionWith(hashSet);
					this.onceSwap = !this.onceSwap;
					foreach (NetCull.UpdateFunctor item in hashSet)
					{
						if (this.hashSet.Remove(item))
						{
							this.list.Remove(item);
						}
					}
					hashSet.Clear();
					while (++this.iterPosition < this.count)
					{
						if (!this.skip.Remove(this.iterPosition))
						{
							NetCull.UpdateFunctor updateFunctor = this.invokation[this.iterPosition];
							try
							{
								updateFunctor();
							}
							catch (Exception ex)
							{
								Object @object;
								try
								{
									@object = (updateFunctor.Target as Object);
								}
								catch
								{
									@object = null;
								}
								Debug.LogException(ex, @object);
							}
						}
					}
				}
				finally
				{
					try
					{
						this.invokation.Clear();
					}
					finally
					{
						this.guarded = false;
					}
				}
			}

			// Token: 0x06001D91 RID: 7569 RVA: 0x000747B0 File Offset: 0x000729B0
			private bool HandleRemoval(NetCull.UpdateFunctor functor)
			{
				if (this.guarded)
				{
					int num = this.invokation.IndexOf(functor);
					if (num != -1)
					{
						this.invokation[num] = null;
						if (this.iterPosition < num)
						{
							this.skip.Add(num);
							return true;
						}
					}
				}
				return false;
			}

			// Token: 0x06001D92 RID: 7570 RVA: 0x00074808 File Offset: 0x00072A08
			public bool Remove(NetCull.UpdateFunctor functor)
			{
				if (this.hashSet.Remove(functor))
				{
					this.list.Remove(functor);
					((!this.onceSwap) ? this.once1 : this.once2).Remove(functor);
					this.HandleRemoval(functor);
					return true;
				}
				return ((!this.onceSwap) ? this.once1 : this.once2).Remove(functor) && this.HandleRemoval(functor);
			}

			// Token: 0x06001D93 RID: 7571 RVA: 0x00074890 File Offset: 0x00072A90
			public bool Add(NetCull.UpdateFunctor functor, bool oneTimeOnly)
			{
				if (this.hashSet.Add(functor))
				{
					this.list.Add(functor);
					if (oneTimeOnly)
					{
						((!this.onceSwap) ? this.once1 : this.once2).Add(functor);
					}
					return true;
				}
				return false;
			}

			// Token: 0x04000DFD RID: 3581
			private readonly HashSet<NetCull.UpdateFunctor> hashSet = new HashSet<NetCull.UpdateFunctor>();

			// Token: 0x04000DFE RID: 3582
			private readonly List<NetCull.UpdateFunctor> list = new List<NetCull.UpdateFunctor>();

			// Token: 0x04000DFF RID: 3583
			private readonly List<NetCull.UpdateFunctor> invokation = new List<NetCull.UpdateFunctor>();

			// Token: 0x04000E00 RID: 3584
			private readonly HashSet<NetCull.UpdateFunctor> once1 = new HashSet<NetCull.UpdateFunctor>();

			// Token: 0x04000E01 RID: 3585
			private readonly HashSet<NetCull.UpdateFunctor> once2 = new HashSet<NetCull.UpdateFunctor>();

			// Token: 0x04000E02 RID: 3586
			private readonly HashSet<int> skip = new HashSet<int>();

			// Token: 0x04000E03 RID: 3587
			private int count;

			// Token: 0x04000E04 RID: 3588
			private int iterPosition;

			// Token: 0x04000E05 RID: 3589
			private bool guarded;

			// Token: 0x04000E06 RID: 3590
			private bool onceSwap;
		}

		// Token: 0x020002F6 RID: 758
		private static class PRE
		{
			// Token: 0x06001D94 RID: 7572 RVA: 0x000748E8 File Offset: 0x00072AE8
			static PRE()
			{
				NetCull.Callbacks.MADE_PRE = true;
			}

			// Token: 0x04000E07 RID: 3591
			public static readonly NetCull.Callbacks.UpdateDelegate DELEGATE = new NetCull.Callbacks.UpdateDelegate();
		}

		// Token: 0x020002F7 RID: 759
		private static class POST
		{
			// Token: 0x06001D95 RID: 7573 RVA: 0x000748FC File Offset: 0x00072AFC
			static POST()
			{
				NetCull.Callbacks.MADE_POST = true;
			}

			// Token: 0x04000E08 RID: 3592
			public static readonly NetCull.Callbacks.UpdateDelegate DELEGATE = new NetCull.Callbacks.UpdateDelegate();
		}
	}

	// Token: 0x020002F8 RID: 760
	[Serializable]
	public abstract class RPCVerificationException : Exception
	{
		// Token: 0x06001D96 RID: 7574 RVA: 0x00074910 File Offset: 0x00072B10
		internal RPCVerificationException()
		{
		}
	}

	// Token: 0x020002F9 RID: 761
	[Serializable]
	public class RPCVerificationDropException : NetCull.RPCVerificationException
	{
		// Token: 0x06001D97 RID: 7575 RVA: 0x00074918 File Offset: 0x00072B18
		internal RPCVerificationDropException()
		{
		}
	}

	// Token: 0x020002FA RID: 762
	[Serializable]
	public class RPCVerificationLateException : NetCull.RPCVerificationDropException
	{
		// Token: 0x06001D98 RID: 7576 RVA: 0x00074920 File Offset: 0x00072B20
		internal RPCVerificationLateException()
		{
		}
	}

	// Token: 0x020002FB RID: 763
	[Serializable]
	public class RPCVerificationSenderException : NetCull.RPCVerificationException
	{
		// Token: 0x06001D99 RID: 7577 RVA: 0x00074928 File Offset: 0x00072B28
		internal RPCVerificationSenderException(NetworkPlayer Sender)
		{
			this.Sender = Sender;
		}

		// Token: 0x04000E09 RID: 3593
		public readonly NetworkPlayer Sender;
	}

	// Token: 0x020002FC RID: 764
	[Serializable]
	public class RPCVerificationWrongSenderException : NetCull.RPCVerificationSenderException
	{
		// Token: 0x06001D9A RID: 7578 RVA: 0x00074938 File Offset: 0x00072B38
		internal RPCVerificationWrongSenderException(NetworkPlayer Sender, NetworkPlayer Owner) : base(Sender)
		{
			this.Owner = Owner;
		}

		// Token: 0x04000E0A RID: 3594
		public readonly NetworkPlayer Owner;
	}

	// Token: 0x020002FD RID: 765
	private static class Send
	{
		// Token: 0x04000E0B RID: 3595
		public static float Rate = Network.sendRate;

		// Token: 0x04000E0C RID: 3596
		public static double Interval = 1.0 / (double)NetCull.sendRate;

		// Token: 0x04000E0D RID: 3597
		public static float IntervalF = (float)NetCull.Send.Interval;
	}

	// Token: 0x020002FE RID: 766
	private static class AutoPrefabs
	{
		// Token: 0x04000E0E RID: 3598
		public static Dictionary<string, uLinkNetworkView> all = new Dictionary<string, uLinkNetworkView>();
	}

	// Token: 0x020002FF RID: 767
	public enum PrefabSearch : sbyte
	{
		// Token: 0x04000E10 RID: 3600
		Missing,
		// Token: 0x04000E11 RID: 3601
		NGC,
		// Token: 0x04000E12 RID: 3602
		NetMain,
		// Token: 0x04000E13 RID: 3603
		NetAuto
	}

	// Token: 0x020008D5 RID: 2261
	// (Invoke) Token: 0x06004D2C RID: 19756
	public delegate void UpdateFunctor();
}
