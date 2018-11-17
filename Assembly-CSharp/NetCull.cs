using System;
using System.Collections.Generic;
using System.Diagnostics;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x0200039A RID: 922
public static class NetCull
{
	// Token: 0x06001E5E RID: 7774 RVA: 0x0006F870 File Offset: 0x0006DA70
	public static void RPC(Facepunch.NetworkView view, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode)
	{
		view.RPC(flags, messageName, rpcMode, new object[0]);
	}

	// Token: 0x06001E5F RID: 7775 RVA: 0x0006F884 File Offset: 0x0006DA84
	public static void RPC(Facepunch.NetworkView view, NetworkFlags flags, string messageName, uLink.NetworkPlayer target)
	{
		view.RPC(flags, messageName, target, new object[0]);
	}

	// Token: 0x06001E60 RID: 7776 RVA: 0x0006F898 File Offset: 0x0006DA98
	public static void RPC(Facepunch.NetworkView view, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets)
	{
		view.RPC(flags, messageName, targets, new object[0]);
	}

	// Token: 0x06001E61 RID: 7777 RVA: 0x0006F8AC File Offset: 0x0006DAAC
	public static void RPC(Facepunch.NetworkView view, string messageName, uLink.RPCMode rpcMode)
	{
		view.RPC(messageName, rpcMode, new object[0]);
	}

	// Token: 0x06001E62 RID: 7778 RVA: 0x0006F8BC File Offset: 0x0006DABC
	public static void RPC(Facepunch.NetworkView view, string messageName, uLink.NetworkPlayer target)
	{
		view.RPC(messageName, target, new object[0]);
	}

	// Token: 0x06001E63 RID: 7779 RVA: 0x0006F8CC File Offset: 0x0006DACC
	public static void RPC(Facepunch.NetworkView view, string messageName, IEnumerable<uLink.NetworkPlayer> targets)
	{
		view.RPC(messageName, targets, new object[0]);
	}

	// Token: 0x06001E64 RID: 7780 RVA: 0x0006F8DC File Offset: 0x0006DADC
	public static void RPC<P0>(Facepunch.NetworkView view, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0)
	{
		view.RPC<P0>(flags, messageName, rpcMode, p0);
	}

	// Token: 0x06001E65 RID: 7781 RVA: 0x0006F8EC File Offset: 0x0006DAEC
	public static void RPC<P0>(Facepunch.NetworkView view, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0)
	{
		view.RPC<P0>(flags, messageName, target, p0);
	}

	// Token: 0x06001E66 RID: 7782 RVA: 0x0006F8FC File Offset: 0x0006DAFC
	public static void RPC<P0>(Facepunch.NetworkView view, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0)
	{
		view.RPC<P0>(flags, messageName, targets, p0);
	}

	// Token: 0x06001E67 RID: 7783 RVA: 0x0006F90C File Offset: 0x0006DB0C
	public static void RPC<P0>(Facepunch.NetworkView view, string messageName, uLink.RPCMode rpcMode, P0 p0)
	{
		view.RPC<P0>(messageName, rpcMode, p0);
	}

	// Token: 0x06001E68 RID: 7784 RVA: 0x0006F918 File Offset: 0x0006DB18
	public static void RPC<P0>(Facepunch.NetworkView view, string messageName, uLink.NetworkPlayer target, P0 p0)
	{
		view.RPC<P0>(messageName, target, p0);
	}

	// Token: 0x06001E69 RID: 7785 RVA: 0x0006F924 File Offset: 0x0006DB24
	public static void RPC<P0>(Facepunch.NetworkView view, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0)
	{
		view.RPC<P0>(messageName, targets, p0);
	}

	// Token: 0x06001E6A RID: 7786 RVA: 0x0006F930 File Offset: 0x0006DB30
	public static void RPC<P0, P1>(Facepunch.NetworkView view, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1)
	{
		view.RPC(flags, messageName, rpcMode, new object[]
		{
			p0,
			p1
		});
	}

	// Token: 0x06001E6B RID: 7787 RVA: 0x0006F960 File Offset: 0x0006DB60
	public static void RPC<P0, P1>(Facepunch.NetworkView view, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1)
	{
		view.RPC(flags, messageName, target, new object[]
		{
			p0,
			p1
		});
	}

	// Token: 0x06001E6C RID: 7788 RVA: 0x0006F990 File Offset: 0x0006DB90
	public static void RPC<P0, P1>(Facepunch.NetworkView view, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1)
	{
		view.RPC(flags, messageName, targets, new object[]
		{
			p0,
			p1
		});
	}

	// Token: 0x06001E6D RID: 7789 RVA: 0x0006F9C0 File Offset: 0x0006DBC0
	public static void RPC<P0, P1>(Facepunch.NetworkView view, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1)
	{
		view.RPC(messageName, rpcMode, new object[]
		{
			p0,
			p1
		});
	}

	// Token: 0x06001E6E RID: 7790 RVA: 0x0006F9E4 File Offset: 0x0006DBE4
	public static void RPC<P0, P1>(Facepunch.NetworkView view, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1)
	{
		view.RPC(messageName, target, new object[]
		{
			p0,
			p1
		});
	}

	// Token: 0x06001E6F RID: 7791 RVA: 0x0006FA08 File Offset: 0x0006DC08
	public static void RPC<P0, P1>(Facepunch.NetworkView view, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1)
	{
		view.RPC(messageName, targets, new object[]
		{
			p0,
			p1
		});
	}

	// Token: 0x06001E70 RID: 7792 RVA: 0x0006FA2C File Offset: 0x0006DC2C
	public static void RPC<P0, P1, P2>(Facepunch.NetworkView view, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2)
	{
		view.RPC(flags, messageName, rpcMode, new object[]
		{
			p0,
			p1,
			p2
		});
	}

	// Token: 0x06001E71 RID: 7793 RVA: 0x0006FA68 File Offset: 0x0006DC68
	public static void RPC<P0, P1, P2>(Facepunch.NetworkView view, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2)
	{
		view.RPC(flags, messageName, target, new object[]
		{
			p0,
			p1,
			p2
		});
	}

	// Token: 0x06001E72 RID: 7794 RVA: 0x0006FAA4 File Offset: 0x0006DCA4
	public static void RPC<P0, P1, P2>(Facepunch.NetworkView view, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2)
	{
		view.RPC(flags, messageName, targets, new object[]
		{
			p0,
			p1,
			p2
		});
	}

	// Token: 0x06001E73 RID: 7795 RVA: 0x0006FAE0 File Offset: 0x0006DCE0
	public static void RPC<P0, P1, P2>(Facepunch.NetworkView view, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2)
	{
		view.RPC(messageName, rpcMode, new object[]
		{
			p0,
			p1,
			p2
		});
	}

	// Token: 0x06001E74 RID: 7796 RVA: 0x0006FB10 File Offset: 0x0006DD10
	public static void RPC<P0, P1, P2>(Facepunch.NetworkView view, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2)
	{
		view.RPC(messageName, target, new object[]
		{
			p0,
			p1,
			p2
		});
	}

	// Token: 0x06001E75 RID: 7797 RVA: 0x0006FB40 File Offset: 0x0006DD40
	public static void RPC<P0, P1, P2>(Facepunch.NetworkView view, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2)
	{
		view.RPC(messageName, targets, new object[]
		{
			p0,
			p1,
			p2
		});
	}

	// Token: 0x06001E76 RID: 7798 RVA: 0x0006FB70 File Offset: 0x0006DD70
	public static void RPC<P0, P1, P2, P3>(Facepunch.NetworkView view, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		view.RPC(flags, messageName, rpcMode, new object[]
		{
			p0,
			p1,
			p2,
			p3
		});
	}

	// Token: 0x06001E77 RID: 7799 RVA: 0x0006FBB4 File Offset: 0x0006DDB4
	public static void RPC<P0, P1, P2, P3>(Facepunch.NetworkView view, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		view.RPC(flags, messageName, target, new object[]
		{
			p0,
			p1,
			p2,
			p3
		});
	}

	// Token: 0x06001E78 RID: 7800 RVA: 0x0006FBF8 File Offset: 0x0006DDF8
	public static void RPC<P0, P1, P2, P3>(Facepunch.NetworkView view, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		view.RPC(flags, messageName, targets, new object[]
		{
			p0,
			p1,
			p2,
			p3
		});
	}

	// Token: 0x06001E79 RID: 7801 RVA: 0x0006FC3C File Offset: 0x0006DE3C
	public static void RPC<P0, P1, P2, P3>(Facepunch.NetworkView view, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		view.RPC(messageName, rpcMode, new object[]
		{
			p0,
			p1,
			p2,
			p3
		});
	}

	// Token: 0x06001E7A RID: 7802 RVA: 0x0006FC74 File Offset: 0x0006DE74
	public static void RPC<P0, P1, P2, P3>(Facepunch.NetworkView view, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		view.RPC(messageName, target, new object[]
		{
			p0,
			p1,
			p2,
			p3
		});
	}

	// Token: 0x06001E7B RID: 7803 RVA: 0x0006FCAC File Offset: 0x0006DEAC
	public static void RPC<P0, P1, P2, P3>(Facepunch.NetworkView view, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		view.RPC(messageName, targets, new object[]
		{
			p0,
			p1,
			p2,
			p3
		});
	}

	// Token: 0x06001E7C RID: 7804 RVA: 0x0006FCE4 File Offset: 0x0006DEE4
	public static void RPC<P0, P1, P2, P3, P4>(Facepunch.NetworkView view, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
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

	// Token: 0x06001E7D RID: 7805 RVA: 0x0006FD34 File Offset: 0x0006DF34
	public static void RPC<P0, P1, P2, P3, P4>(Facepunch.NetworkView view, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
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

	// Token: 0x06001E7E RID: 7806 RVA: 0x0006FD84 File Offset: 0x0006DF84
	public static void RPC<P0, P1, P2, P3, P4>(Facepunch.NetworkView view, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
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

	// Token: 0x06001E7F RID: 7807 RVA: 0x0006FDD4 File Offset: 0x0006DFD4
	public static void RPC<P0, P1, P2, P3, P4>(Facepunch.NetworkView view, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
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

	// Token: 0x06001E80 RID: 7808 RVA: 0x0006FE20 File Offset: 0x0006E020
	public static void RPC<P0, P1, P2, P3, P4>(Facepunch.NetworkView view, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
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

	// Token: 0x06001E81 RID: 7809 RVA: 0x0006FE6C File Offset: 0x0006E06C
	public static void RPC<P0, P1, P2, P3, P4>(Facepunch.NetworkView view, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
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

	// Token: 0x06001E82 RID: 7810 RVA: 0x0006FEB8 File Offset: 0x0006E0B8
	public static void RPC<P0, P1, P2, P3, P4, P5>(Facepunch.NetworkView view, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
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

	// Token: 0x06001E83 RID: 7811 RVA: 0x0006FF10 File Offset: 0x0006E110
	public static void RPC<P0, P1, P2, P3, P4, P5>(Facepunch.NetworkView view, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
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

	// Token: 0x06001E84 RID: 7812 RVA: 0x0006FF68 File Offset: 0x0006E168
	public static void RPC<P0, P1, P2, P3, P4, P5>(Facepunch.NetworkView view, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
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

	// Token: 0x06001E85 RID: 7813 RVA: 0x0006FFC0 File Offset: 0x0006E1C0
	public static void RPC<P0, P1, P2, P3, P4, P5>(Facepunch.NetworkView view, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
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

	// Token: 0x06001E86 RID: 7814 RVA: 0x00070018 File Offset: 0x0006E218
	public static void RPC<P0, P1, P2, P3, P4, P5>(Facepunch.NetworkView view, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
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

	// Token: 0x06001E87 RID: 7815 RVA: 0x00070070 File Offset: 0x0006E270
	public static void RPC<P0, P1, P2, P3, P4, P5>(Facepunch.NetworkView view, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
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

	// Token: 0x06001E88 RID: 7816 RVA: 0x000700C8 File Offset: 0x0006E2C8
	public static void RPC<P0, P1, P2, P3, P4, P5, P6>(Facepunch.NetworkView view, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
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

	// Token: 0x06001E89 RID: 7817 RVA: 0x0007012C File Offset: 0x0006E32C
	public static void RPC<P0, P1, P2, P3, P4, P5, P6>(Facepunch.NetworkView view, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
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

	// Token: 0x06001E8A RID: 7818 RVA: 0x00070190 File Offset: 0x0006E390
	public static void RPC<P0, P1, P2, P3, P4, P5, P6>(Facepunch.NetworkView view, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
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

	// Token: 0x06001E8B RID: 7819 RVA: 0x000701F4 File Offset: 0x0006E3F4
	public static void RPC<P0, P1, P2, P3, P4, P5, P6>(Facepunch.NetworkView view, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
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

	// Token: 0x06001E8C RID: 7820 RVA: 0x00070254 File Offset: 0x0006E454
	public static void RPC<P0, P1, P2, P3, P4, P5, P6>(Facepunch.NetworkView view, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
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

	// Token: 0x06001E8D RID: 7821 RVA: 0x000702B4 File Offset: 0x0006E4B4
	public static void RPC<P0, P1, P2, P3, P4, P5, P6>(Facepunch.NetworkView view, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
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

	// Token: 0x06001E8E RID: 7822 RVA: 0x00070314 File Offset: 0x0006E514
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(Facepunch.NetworkView view, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
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

	// Token: 0x06001E8F RID: 7823 RVA: 0x00070380 File Offset: 0x0006E580
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(Facepunch.NetworkView view, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
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

	// Token: 0x06001E90 RID: 7824 RVA: 0x000703EC File Offset: 0x0006E5EC
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(Facepunch.NetworkView view, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
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

	// Token: 0x06001E91 RID: 7825 RVA: 0x00070458 File Offset: 0x0006E658
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(Facepunch.NetworkView view, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
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

	// Token: 0x06001E92 RID: 7826 RVA: 0x000704C4 File Offset: 0x0006E6C4
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(Facepunch.NetworkView view, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
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

	// Token: 0x06001E93 RID: 7827 RVA: 0x00070530 File Offset: 0x0006E730
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(Facepunch.NetworkView view, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
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

	// Token: 0x06001E94 RID: 7828 RVA: 0x0007059C File Offset: 0x0006E79C
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(Facepunch.NetworkView view, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
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

	// Token: 0x06001E95 RID: 7829 RVA: 0x00070614 File Offset: 0x0006E814
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(Facepunch.NetworkView view, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
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

	// Token: 0x06001E96 RID: 7830 RVA: 0x0007068C File Offset: 0x0006E88C
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(Facepunch.NetworkView view, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
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

	// Token: 0x06001E97 RID: 7831 RVA: 0x00070704 File Offset: 0x0006E904
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(Facepunch.NetworkView view, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
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

	// Token: 0x06001E98 RID: 7832 RVA: 0x0007077C File Offset: 0x0006E97C
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(Facepunch.NetworkView view, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
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

	// Token: 0x06001E99 RID: 7833 RVA: 0x000707F4 File Offset: 0x0006E9F4
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(Facepunch.NetworkView view, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
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

	// Token: 0x06001E9A RID: 7834 RVA: 0x0007086C File Offset: 0x0006EA6C
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(Facepunch.NetworkView view, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
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

	// Token: 0x06001E9B RID: 7835 RVA: 0x000708F0 File Offset: 0x0006EAF0
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(Facepunch.NetworkView view, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
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

	// Token: 0x06001E9C RID: 7836 RVA: 0x00070974 File Offset: 0x0006EB74
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(Facepunch.NetworkView view, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
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

	// Token: 0x06001E9D RID: 7837 RVA: 0x000709F8 File Offset: 0x0006EBF8
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(Facepunch.NetworkView view, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
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

	// Token: 0x06001E9E RID: 7838 RVA: 0x00070A78 File Offset: 0x0006EC78
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(Facepunch.NetworkView view, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
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

	// Token: 0x06001E9F RID: 7839 RVA: 0x00070AF8 File Offset: 0x0006ECF8
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(Facepunch.NetworkView view, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
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

	// Token: 0x06001EA0 RID: 7840 RVA: 0x00070B78 File Offset: 0x0006ED78
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(Facepunch.NetworkView view, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
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

	// Token: 0x06001EA1 RID: 7841 RVA: 0x00070C08 File Offset: 0x0006EE08
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(Facepunch.NetworkView view, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
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

	// Token: 0x06001EA2 RID: 7842 RVA: 0x00070C98 File Offset: 0x0006EE98
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(Facepunch.NetworkView view, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
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

	// Token: 0x06001EA3 RID: 7843 RVA: 0x00070D28 File Offset: 0x0006EF28
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(Facepunch.NetworkView view, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
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

	// Token: 0x06001EA4 RID: 7844 RVA: 0x00070DB4 File Offset: 0x0006EFB4
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(Facepunch.NetworkView view, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
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

	// Token: 0x06001EA5 RID: 7845 RVA: 0x00070E40 File Offset: 0x0006F040
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(Facepunch.NetworkView view, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
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

	// Token: 0x06001EA6 RID: 7846 RVA: 0x00070ECC File Offset: 0x0006F0CC
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(Facepunch.NetworkView view, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
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

	// Token: 0x06001EA7 RID: 7847 RVA: 0x00070F64 File Offset: 0x0006F164
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(Facepunch.NetworkView view, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
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

	// Token: 0x06001EA8 RID: 7848 RVA: 0x00070FFC File Offset: 0x0006F1FC
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(Facepunch.NetworkView view, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
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

	// Token: 0x06001EA9 RID: 7849 RVA: 0x00071094 File Offset: 0x0006F294
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(Facepunch.NetworkView view, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
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

	// Token: 0x06001EAA RID: 7850 RVA: 0x0007112C File Offset: 0x0006F32C
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(Facepunch.NetworkView view, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
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

	// Token: 0x06001EAB RID: 7851 RVA: 0x000711C4 File Offset: 0x0006F3C4
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(Facepunch.NetworkView view, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
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

	// Token: 0x06001EAC RID: 7852 RVA: 0x0007125C File Offset: 0x0006F45C
	public static void RPC(global::NGCView view, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode)
	{
		view.RPC(flags, messageName, rpcMode);
	}

	// Token: 0x06001EAD RID: 7853 RVA: 0x00071268 File Offset: 0x0006F468
	public static void RPC(global::NGCView view, NetworkFlags flags, string messageName, uLink.NetworkPlayer target)
	{
		view.RPC(flags, messageName, target);
	}

	// Token: 0x06001EAE RID: 7854 RVA: 0x00071274 File Offset: 0x0006F474
	public static void RPC(global::NGCView view, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets)
	{
		view.RPC(flags, messageName, targets);
	}

	// Token: 0x06001EAF RID: 7855 RVA: 0x00071280 File Offset: 0x0006F480
	public static void RPC(global::NGCView view, string messageName, uLink.RPCMode rpcMode)
	{
		view.RPC(messageName, rpcMode);
	}

	// Token: 0x06001EB0 RID: 7856 RVA: 0x0007128C File Offset: 0x0006F48C
	public static void RPC(global::NGCView view, string messageName, uLink.NetworkPlayer target)
	{
		view.RPC(messageName, target);
	}

	// Token: 0x06001EB1 RID: 7857 RVA: 0x00071298 File Offset: 0x0006F498
	public static void RPC(global::NGCView view, string messageName, IEnumerable<uLink.NetworkPlayer> targets)
	{
		view.RPC(messageName, targets);
	}

	// Token: 0x06001EB2 RID: 7858 RVA: 0x000712A4 File Offset: 0x0006F4A4
	public static void RPC<P0>(global::NGCView view, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0)
	{
		view.RPC<P0>(flags, messageName, rpcMode, p0);
	}

	// Token: 0x06001EB3 RID: 7859 RVA: 0x000712B4 File Offset: 0x0006F4B4
	public static void RPC<P0>(global::NGCView view, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0)
	{
		view.RPC<P0>(flags, messageName, target, p0);
	}

	// Token: 0x06001EB4 RID: 7860 RVA: 0x000712C4 File Offset: 0x0006F4C4
	public static void RPC<P0>(global::NGCView view, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0)
	{
		view.RPC<P0>(flags, messageName, targets, p0);
	}

	// Token: 0x06001EB5 RID: 7861 RVA: 0x000712D4 File Offset: 0x0006F4D4
	public static void RPC<P0>(global::NGCView view, string messageName, uLink.RPCMode rpcMode, P0 p0)
	{
		view.RPC<P0>(messageName, rpcMode, p0);
	}

	// Token: 0x06001EB6 RID: 7862 RVA: 0x000712E0 File Offset: 0x0006F4E0
	public static void RPC<P0>(global::NGCView view, string messageName, uLink.NetworkPlayer target, P0 p0)
	{
		view.RPC<P0>(messageName, target, p0);
	}

	// Token: 0x06001EB7 RID: 7863 RVA: 0x000712EC File Offset: 0x0006F4EC
	public static void RPC<P0>(global::NGCView view, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0)
	{
		view.RPC<P0>(messageName, targets, p0);
	}

	// Token: 0x06001EB8 RID: 7864 RVA: 0x000712F8 File Offset: 0x0006F4F8
	public static void RPC<P0, P1>(global::NGCView view, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1)
	{
		view.RPC<P0, P1>(flags, messageName, rpcMode, p0, p1);
	}

	// Token: 0x06001EB9 RID: 7865 RVA: 0x00071308 File Offset: 0x0006F508
	public static void RPC<P0, P1>(global::NGCView view, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1)
	{
		view.RPC<P0, P1>(flags, messageName, target, p0, p1);
	}

	// Token: 0x06001EBA RID: 7866 RVA: 0x00071318 File Offset: 0x0006F518
	public static void RPC<P0, P1>(global::NGCView view, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1)
	{
		view.RPC<P0, P1>(flags, messageName, targets, p0, p1);
	}

	// Token: 0x06001EBB RID: 7867 RVA: 0x00071328 File Offset: 0x0006F528
	public static void RPC<P0, P1>(global::NGCView view, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1)
	{
		view.RPC<P0, P1>(messageName, rpcMode, p0, p1);
	}

	// Token: 0x06001EBC RID: 7868 RVA: 0x00071338 File Offset: 0x0006F538
	public static void RPC<P0, P1>(global::NGCView view, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1)
	{
		view.RPC<P0, P1>(messageName, target, p0, p1);
	}

	// Token: 0x06001EBD RID: 7869 RVA: 0x00071348 File Offset: 0x0006F548
	public static void RPC<P0, P1>(global::NGCView view, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1)
	{
		view.RPC<P0, P1>(messageName, targets, p0, p1);
	}

	// Token: 0x06001EBE RID: 7870 RVA: 0x00071358 File Offset: 0x0006F558
	public static void RPC<P0, P1, P2>(global::NGCView view, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2)
	{
		view.RPC<P0, P1, P2>(flags, messageName, rpcMode, p0, p1, p2);
	}

	// Token: 0x06001EBF RID: 7871 RVA: 0x0007136C File Offset: 0x0006F56C
	public static void RPC<P0, P1, P2>(global::NGCView view, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2)
	{
		view.RPC<P0, P1, P2>(flags, messageName, target, p0, p1, p2);
	}

	// Token: 0x06001EC0 RID: 7872 RVA: 0x00071380 File Offset: 0x0006F580
	public static void RPC<P0, P1, P2>(global::NGCView view, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2)
	{
		view.RPC<P0, P1, P2>(flags, messageName, targets, p0, p1, p2);
	}

	// Token: 0x06001EC1 RID: 7873 RVA: 0x00071394 File Offset: 0x0006F594
	public static void RPC<P0, P1, P2>(global::NGCView view, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2)
	{
		view.RPC<P0, P1, P2>(messageName, rpcMode, p0, p1, p2);
	}

	// Token: 0x06001EC2 RID: 7874 RVA: 0x000713A4 File Offset: 0x0006F5A4
	public static void RPC<P0, P1, P2>(global::NGCView view, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2)
	{
		view.RPC<P0, P1, P2>(messageName, target, p0, p1, p2);
	}

	// Token: 0x06001EC3 RID: 7875 RVA: 0x000713B4 File Offset: 0x0006F5B4
	public static void RPC<P0, P1, P2>(global::NGCView view, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2)
	{
		view.RPC<P0, P1, P2>(messageName, targets, p0, p1, p2);
	}

	// Token: 0x06001EC4 RID: 7876 RVA: 0x000713C4 File Offset: 0x0006F5C4
	public static void RPC<P0, P1, P2, P3>(global::NGCView view, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		view.RPC<P0, P1, P2, P3>(flags, messageName, rpcMode, p0, p1, p2, p3);
	}

	// Token: 0x06001EC5 RID: 7877 RVA: 0x000713E4 File Offset: 0x0006F5E4
	public static void RPC<P0, P1, P2, P3>(global::NGCView view, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		view.RPC<P0, P1, P2, P3>(flags, messageName, target, p0, p1, p2, p3);
	}

	// Token: 0x06001EC6 RID: 7878 RVA: 0x00071404 File Offset: 0x0006F604
	public static void RPC<P0, P1, P2, P3>(global::NGCView view, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		view.RPC<P0, P1, P2, P3>(flags, messageName, targets, p0, p1, p2, p3);
	}

	// Token: 0x06001EC7 RID: 7879 RVA: 0x00071424 File Offset: 0x0006F624
	public static void RPC<P0, P1, P2, P3>(global::NGCView view, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		view.RPC<P0, P1, P2, P3>(messageName, rpcMode, p0, p1, p2, p3);
	}

	// Token: 0x06001EC8 RID: 7880 RVA: 0x00071438 File Offset: 0x0006F638
	public static void RPC<P0, P1, P2, P3>(global::NGCView view, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		view.RPC<P0, P1, P2, P3>(messageName, target, p0, p1, p2, p3);
	}

	// Token: 0x06001EC9 RID: 7881 RVA: 0x0007144C File Offset: 0x0006F64C
	public static void RPC<P0, P1, P2, P3>(global::NGCView view, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		view.RPC<P0, P1, P2, P3>(messageName, targets, p0, p1, p2, p3);
	}

	// Token: 0x06001ECA RID: 7882 RVA: 0x00071460 File Offset: 0x0006F660
	public static void RPC<P0, P1, P2, P3, P4>(global::NGCView view, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		view.RPC<P0, P1, P2, P3, P4>(flags, messageName, rpcMode, p0, p1, p2, p3, p4);
	}

	// Token: 0x06001ECB RID: 7883 RVA: 0x00071480 File Offset: 0x0006F680
	public static void RPC<P0, P1, P2, P3, P4>(global::NGCView view, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		view.RPC<P0, P1, P2, P3, P4>(flags, messageName, target, p0, p1, p2, p3, p4);
	}

	// Token: 0x06001ECC RID: 7884 RVA: 0x000714A0 File Offset: 0x0006F6A0
	public static void RPC<P0, P1, P2, P3, P4>(global::NGCView view, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		view.RPC<P0, P1, P2, P3, P4>(flags, messageName, targets, p0, p1, p2, p3, p4);
	}

	// Token: 0x06001ECD RID: 7885 RVA: 0x000714C0 File Offset: 0x0006F6C0
	public static void RPC<P0, P1, P2, P3, P4>(global::NGCView view, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		view.RPC<P0, P1, P2, P3, P4>(messageName, rpcMode, p0, p1, p2, p3, p4);
	}

	// Token: 0x06001ECE RID: 7886 RVA: 0x000714E0 File Offset: 0x0006F6E0
	public static void RPC<P0, P1, P2, P3, P4>(global::NGCView view, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		view.RPC<P0, P1, P2, P3, P4>(messageName, target, p0, p1, p2, p3, p4);
	}

	// Token: 0x06001ECF RID: 7887 RVA: 0x00071500 File Offset: 0x0006F700
	public static void RPC<P0, P1, P2, P3, P4>(global::NGCView view, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		view.RPC<P0, P1, P2, P3, P4>(messageName, targets, p0, p1, p2, p3, p4);
	}

	// Token: 0x06001ED0 RID: 7888 RVA: 0x00071520 File Offset: 0x0006F720
	public static void RPC<P0, P1, P2, P3, P4, P5>(global::NGCView view, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		view.RPC<P0, P1, P2, P3, P4, P5>(flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x06001ED1 RID: 7889 RVA: 0x00071544 File Offset: 0x0006F744
	public static void RPC<P0, P1, P2, P3, P4, P5>(global::NGCView view, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		view.RPC<P0, P1, P2, P3, P4, P5>(flags, messageName, target, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x06001ED2 RID: 7890 RVA: 0x00071568 File Offset: 0x0006F768
	public static void RPC<P0, P1, P2, P3, P4, P5>(global::NGCView view, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		view.RPC<P0, P1, P2, P3, P4, P5>(flags, messageName, targets, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x06001ED3 RID: 7891 RVA: 0x0007158C File Offset: 0x0006F78C
	public static void RPC<P0, P1, P2, P3, P4, P5>(global::NGCView view, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		view.RPC<P0, P1, P2, P3, P4, P5>(messageName, rpcMode, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x06001ED4 RID: 7892 RVA: 0x000715AC File Offset: 0x0006F7AC
	public static void RPC<P0, P1, P2, P3, P4, P5>(global::NGCView view, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		view.RPC<P0, P1, P2, P3, P4, P5>(messageName, target, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x06001ED5 RID: 7893 RVA: 0x000715CC File Offset: 0x0006F7CC
	public static void RPC<P0, P1, P2, P3, P4, P5>(global::NGCView view, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		view.RPC<P0, P1, P2, P3, P4, P5>(messageName, targets, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x06001ED6 RID: 7894 RVA: 0x000715EC File Offset: 0x0006F7EC
	public static void RPC<P0, P1, P2, P3, P4, P5, P6>(global::NGCView view, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6>(flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x06001ED7 RID: 7895 RVA: 0x00071610 File Offset: 0x0006F810
	public static void RPC<P0, P1, P2, P3, P4, P5, P6>(global::NGCView view, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6>(flags, messageName, target, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x06001ED8 RID: 7896 RVA: 0x00071634 File Offset: 0x0006F834
	public static void RPC<P0, P1, P2, P3, P4, P5, P6>(global::NGCView view, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6>(flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x06001ED9 RID: 7897 RVA: 0x00071658 File Offset: 0x0006F858
	public static void RPC<P0, P1, P2, P3, P4, P5, P6>(global::NGCView view, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6>(messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x06001EDA RID: 7898 RVA: 0x0007167C File Offset: 0x0006F87C
	public static void RPC<P0, P1, P2, P3, P4, P5, P6>(global::NGCView view, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6>(messageName, target, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x06001EDB RID: 7899 RVA: 0x000716A0 File Offset: 0x0006F8A0
	public static void RPC<P0, P1, P2, P3, P4, P5, P6>(global::NGCView view, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6>(messageName, targets, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x06001EDC RID: 7900 RVA: 0x000716C4 File Offset: 0x0006F8C4
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(global::NGCView view, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x06001EDD RID: 7901 RVA: 0x000716EC File Offset: 0x0006F8EC
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(global::NGCView view, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x06001EDE RID: 7902 RVA: 0x00071714 File Offset: 0x0006F914
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(global::NGCView view, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x06001EDF RID: 7903 RVA: 0x0007173C File Offset: 0x0006F93C
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(global::NGCView view, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x06001EE0 RID: 7904 RVA: 0x00071760 File Offset: 0x0006F960
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(global::NGCView view, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(messageName, target, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x06001EE1 RID: 7905 RVA: 0x00071784 File Offset: 0x0006F984
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(global::NGCView view, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x06001EE2 RID: 7906 RVA: 0x000717A8 File Offset: 0x0006F9A8
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(global::NGCView view, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x06001EE3 RID: 7907 RVA: 0x000717D0 File Offset: 0x0006F9D0
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(global::NGCView view, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x06001EE4 RID: 7908 RVA: 0x000717F8 File Offset: 0x0006F9F8
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(global::NGCView view, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x06001EE5 RID: 7909 RVA: 0x00071820 File Offset: 0x0006FA20
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(global::NGCView view, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x06001EE6 RID: 7910 RVA: 0x00071848 File Offset: 0x0006FA48
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(global::NGCView view, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x06001EE7 RID: 7911 RVA: 0x00071870 File Offset: 0x0006FA70
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(global::NGCView view, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x06001EE8 RID: 7912 RVA: 0x00071898 File Offset: 0x0006FA98
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(global::NGCView view, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x06001EE9 RID: 7913 RVA: 0x000718C4 File Offset: 0x0006FAC4
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(global::NGCView view, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x06001EEA RID: 7914 RVA: 0x000718F0 File Offset: 0x0006FAF0
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(global::NGCView view, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x06001EEB RID: 7915 RVA: 0x0007191C File Offset: 0x0006FB1C
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(global::NGCView view, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x06001EEC RID: 7916 RVA: 0x00071944 File Offset: 0x0006FB44
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(global::NGCView view, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x06001EED RID: 7917 RVA: 0x0007196C File Offset: 0x0006FB6C
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(global::NGCView view, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x06001EEE RID: 7918 RVA: 0x00071994 File Offset: 0x0006FB94
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(global::NGCView view, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x06001EEF RID: 7919 RVA: 0x000719C0 File Offset: 0x0006FBC0
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(global::NGCView view, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x06001EF0 RID: 7920 RVA: 0x000719EC File Offset: 0x0006FBEC
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(global::NGCView view, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x06001EF1 RID: 7921 RVA: 0x00071A18 File Offset: 0x0006FC18
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(global::NGCView view, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x06001EF2 RID: 7922 RVA: 0x00071A44 File Offset: 0x0006FC44
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(global::NGCView view, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x06001EF3 RID: 7923 RVA: 0x00071A70 File Offset: 0x0006FC70
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(global::NGCView view, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x06001EF4 RID: 7924 RVA: 0x00071A9C File Offset: 0x0006FC9C
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(global::NGCView view, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x06001EF5 RID: 7925 RVA: 0x00071ACC File Offset: 0x0006FCCC
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(global::NGCView view, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x06001EF6 RID: 7926 RVA: 0x00071AFC File Offset: 0x0006FCFC
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(global::NGCView view, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x06001EF7 RID: 7927 RVA: 0x00071B2C File Offset: 0x0006FD2C
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(global::NGCView view, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x06001EF8 RID: 7928 RVA: 0x00071B58 File Offset: 0x0006FD58
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(global::NGCView view, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x06001EF9 RID: 7929 RVA: 0x00071B84 File Offset: 0x0006FD84
	public static void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(global::NGCView view, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		view.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x06001EFA RID: 7930 RVA: 0x00071BB0 File Offset: 0x0006FDB0
	public static bool RPC(uLink.NetworkViewID viewID, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC(networkView, flags, messageName, rpcMode);
		return true;
	}

	// Token: 0x06001EFB RID: 7931 RVA: 0x00071BF4 File Offset: 0x0006FDF4
	public static bool RPC(uLink.NetworkViewID viewID, NetworkFlags flags, string messageName, uLink.NetworkPlayer target)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC(networkView, flags, messageName, target);
		return true;
	}

	// Token: 0x06001EFC RID: 7932 RVA: 0x00071C38 File Offset: 0x0006FE38
	public static bool RPC(uLink.NetworkViewID viewID, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC(networkView, flags, messageName, targets);
		return true;
	}

	// Token: 0x06001EFD RID: 7933 RVA: 0x00071C7C File Offset: 0x0006FE7C
	public static bool RPC(uLink.NetworkViewID viewID, string messageName, uLink.RPCMode rpcMode)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC(networkView, messageName, rpcMode);
		return true;
	}

	// Token: 0x06001EFE RID: 7934 RVA: 0x00071CBC File Offset: 0x0006FEBC
	public static bool RPC(uLink.NetworkViewID viewID, string messageName, uLink.NetworkPlayer target)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC(networkView, messageName, target);
		return true;
	}

	// Token: 0x06001EFF RID: 7935 RVA: 0x00071CFC File Offset: 0x0006FEFC
	public static bool RPC(uLink.NetworkViewID viewID, string messageName, IEnumerable<uLink.NetworkPlayer> targets)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC(networkView, messageName, targets);
		return true;
	}

	// Token: 0x06001F00 RID: 7936 RVA: 0x00071D3C File Offset: 0x0006FF3C
	public static bool RPC<P0>(uLink.NetworkViewID viewID, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0>(networkView, flags, messageName, rpcMode, p0);
		return true;
	}

	// Token: 0x06001F01 RID: 7937 RVA: 0x00071D80 File Offset: 0x0006FF80
	public static bool RPC<P0>(uLink.NetworkViewID viewID, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0>(networkView, flags, messageName, target, p0);
		return true;
	}

	// Token: 0x06001F02 RID: 7938 RVA: 0x00071DC4 File Offset: 0x0006FFC4
	public static bool RPC<P0>(uLink.NetworkViewID viewID, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0>(networkView, flags, messageName, targets, p0);
		return true;
	}

	// Token: 0x06001F03 RID: 7939 RVA: 0x00071E08 File Offset: 0x00070008
	public static bool RPC<P0>(uLink.NetworkViewID viewID, string messageName, uLink.RPCMode rpcMode, P0 p0)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0>(networkView, messageName, rpcMode, p0);
		return true;
	}

	// Token: 0x06001F04 RID: 7940 RVA: 0x00071E4C File Offset: 0x0007004C
	public static bool RPC<P0>(uLink.NetworkViewID viewID, string messageName, uLink.NetworkPlayer target, P0 p0)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0>(networkView, messageName, target, p0);
		return true;
	}

	// Token: 0x06001F05 RID: 7941 RVA: 0x00071E90 File Offset: 0x00070090
	public static bool RPC<P0>(uLink.NetworkViewID viewID, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0>(networkView, messageName, targets, p0);
		return true;
	}

	// Token: 0x06001F06 RID: 7942 RVA: 0x00071ED4 File Offset: 0x000700D4
	public static bool RPC<P0, P1>(uLink.NetworkViewID viewID, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1>(networkView, flags, messageName, rpcMode, p0, p1);
		return true;
	}

	// Token: 0x06001F07 RID: 7943 RVA: 0x00071F1C File Offset: 0x0007011C
	public static bool RPC<P0, P1>(uLink.NetworkViewID viewID, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1>(networkView, flags, messageName, target, p0, p1);
		return true;
	}

	// Token: 0x06001F08 RID: 7944 RVA: 0x00071F64 File Offset: 0x00070164
	public static bool RPC<P0, P1>(uLink.NetworkViewID viewID, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1>(networkView, flags, messageName, targets, p0, p1);
		return true;
	}

	// Token: 0x06001F09 RID: 7945 RVA: 0x00071FAC File Offset: 0x000701AC
	public static bool RPC<P0, P1>(uLink.NetworkViewID viewID, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1>(networkView, messageName, rpcMode, p0, p1);
		return true;
	}

	// Token: 0x06001F0A RID: 7946 RVA: 0x00071FF0 File Offset: 0x000701F0
	public static bool RPC<P0, P1>(uLink.NetworkViewID viewID, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1>(networkView, messageName, target, p0, p1);
		return true;
	}

	// Token: 0x06001F0B RID: 7947 RVA: 0x00072034 File Offset: 0x00070234
	public static bool RPC<P0, P1>(uLink.NetworkViewID viewID, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1>(networkView, messageName, targets, p0, p1);
		return true;
	}

	// Token: 0x06001F0C RID: 7948 RVA: 0x00072078 File Offset: 0x00070278
	public static bool RPC<P0, P1, P2>(uLink.NetworkViewID viewID, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2>(networkView, flags, messageName, rpcMode, p0, p1, p2);
		return true;
	}

	// Token: 0x06001F0D RID: 7949 RVA: 0x000720C0 File Offset: 0x000702C0
	public static bool RPC<P0, P1, P2>(uLink.NetworkViewID viewID, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2>(networkView, flags, messageName, target, p0, p1, p2);
		return true;
	}

	// Token: 0x06001F0E RID: 7950 RVA: 0x00072108 File Offset: 0x00070308
	public static bool RPC<P0, P1, P2>(uLink.NetworkViewID viewID, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2>(networkView, flags, messageName, targets, p0, p1, p2);
		return true;
	}

	// Token: 0x06001F0F RID: 7951 RVA: 0x00072150 File Offset: 0x00070350
	public static bool RPC<P0, P1, P2>(uLink.NetworkViewID viewID, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2>(networkView, messageName, rpcMode, p0, p1, p2);
		return true;
	}

	// Token: 0x06001F10 RID: 7952 RVA: 0x00072198 File Offset: 0x00070398
	public static bool RPC<P0, P1, P2>(uLink.NetworkViewID viewID, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2>(networkView, messageName, target, p0, p1, p2);
		return true;
	}

	// Token: 0x06001F11 RID: 7953 RVA: 0x000721E0 File Offset: 0x000703E0
	public static bool RPC<P0, P1, P2>(uLink.NetworkViewID viewID, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2>(networkView, messageName, targets, p0, p1, p2);
		return true;
	}

	// Token: 0x06001F12 RID: 7954 RVA: 0x00072228 File Offset: 0x00070428
	public static bool RPC<P0, P1, P2, P3>(uLink.NetworkViewID viewID, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3>(networkView, flags, messageName, rpcMode, p0, p1, p2, p3);
		return true;
	}

	// Token: 0x06001F13 RID: 7955 RVA: 0x00072274 File Offset: 0x00070474
	public static bool RPC<P0, P1, P2, P3>(uLink.NetworkViewID viewID, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3>(networkView, flags, messageName, target, p0, p1, p2, p3);
		return true;
	}

	// Token: 0x06001F14 RID: 7956 RVA: 0x000722C0 File Offset: 0x000704C0
	public static bool RPC<P0, P1, P2, P3>(uLink.NetworkViewID viewID, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3>(networkView, flags, messageName, targets, p0, p1, p2, p3);
		return true;
	}

	// Token: 0x06001F15 RID: 7957 RVA: 0x0007230C File Offset: 0x0007050C
	public static bool RPC<P0, P1, P2, P3>(uLink.NetworkViewID viewID, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3>(networkView, messageName, rpcMode, p0, p1, p2, p3);
		return true;
	}

	// Token: 0x06001F16 RID: 7958 RVA: 0x00072354 File Offset: 0x00070554
	public static bool RPC<P0, P1, P2, P3>(uLink.NetworkViewID viewID, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3>(networkView, messageName, target, p0, p1, p2, p3);
		return true;
	}

	// Token: 0x06001F17 RID: 7959 RVA: 0x0007239C File Offset: 0x0007059C
	public static bool RPC<P0, P1, P2, P3>(uLink.NetworkViewID viewID, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3>(networkView, messageName, targets, p0, p1, p2, p3);
		return true;
	}

	// Token: 0x06001F18 RID: 7960 RVA: 0x000723E4 File Offset: 0x000705E4
	public static bool RPC<P0, P1, P2, P3, P4>(uLink.NetworkViewID viewID, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4>(networkView, flags, messageName, rpcMode, p0, p1, p2, p3, p4);
		return true;
	}

	// Token: 0x06001F19 RID: 7961 RVA: 0x00072430 File Offset: 0x00070630
	public static bool RPC<P0, P1, P2, P3, P4>(uLink.NetworkViewID viewID, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4>(networkView, flags, messageName, target, p0, p1, p2, p3, p4);
		return true;
	}

	// Token: 0x06001F1A RID: 7962 RVA: 0x0007247C File Offset: 0x0007067C
	public static bool RPC<P0, P1, P2, P3, P4>(uLink.NetworkViewID viewID, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4>(networkView, flags, messageName, targets, p0, p1, p2, p3, p4);
		return true;
	}

	// Token: 0x06001F1B RID: 7963 RVA: 0x000724C8 File Offset: 0x000706C8
	public static bool RPC<P0, P1, P2, P3, P4>(uLink.NetworkViewID viewID, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4>(networkView, messageName, rpcMode, p0, p1, p2, p3, p4);
		return true;
	}

	// Token: 0x06001F1C RID: 7964 RVA: 0x00072514 File Offset: 0x00070714
	public static bool RPC<P0, P1, P2, P3, P4>(uLink.NetworkViewID viewID, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4>(networkView, messageName, target, p0, p1, p2, p3, p4);
		return true;
	}

	// Token: 0x06001F1D RID: 7965 RVA: 0x00072560 File Offset: 0x00070760
	public static bool RPC<P0, P1, P2, P3, P4>(uLink.NetworkViewID viewID, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4>(networkView, messageName, targets, p0, p1, p2, p3, p4);
		return true;
	}

	// Token: 0x06001F1E RID: 7966 RVA: 0x000725AC File Offset: 0x000707AC
	public static bool RPC<P0, P1, P2, P3, P4, P5>(uLink.NetworkViewID viewID, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5>(networkView, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5);
		return true;
	}

	// Token: 0x06001F1F RID: 7967 RVA: 0x000725FC File Offset: 0x000707FC
	public static bool RPC<P0, P1, P2, P3, P4, P5>(uLink.NetworkViewID viewID, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5>(networkView, flags, messageName, target, p0, p1, p2, p3, p4, p5);
		return true;
	}

	// Token: 0x06001F20 RID: 7968 RVA: 0x0007264C File Offset: 0x0007084C
	public static bool RPC<P0, P1, P2, P3, P4, P5>(uLink.NetworkViewID viewID, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5>(networkView, flags, messageName, targets, p0, p1, p2, p3, p4, p5);
		return true;
	}

	// Token: 0x06001F21 RID: 7969 RVA: 0x0007269C File Offset: 0x0007089C
	public static bool RPC<P0, P1, P2, P3, P4, P5>(uLink.NetworkViewID viewID, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5>(networkView, messageName, rpcMode, p0, p1, p2, p3, p4, p5);
		return true;
	}

	// Token: 0x06001F22 RID: 7970 RVA: 0x000726E8 File Offset: 0x000708E8
	public static bool RPC<P0, P1, P2, P3, P4, P5>(uLink.NetworkViewID viewID, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5>(networkView, messageName, target, p0, p1, p2, p3, p4, p5);
		return true;
	}

	// Token: 0x06001F23 RID: 7971 RVA: 0x00072734 File Offset: 0x00070934
	public static bool RPC<P0, P1, P2, P3, P4, P5>(uLink.NetworkViewID viewID, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5>(networkView, messageName, targets, p0, p1, p2, p3, p4, p5);
		return true;
	}

	// Token: 0x06001F24 RID: 7972 RVA: 0x00072780 File Offset: 0x00070980
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(uLink.NetworkViewID viewID, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(networkView, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6);
		return true;
	}

	// Token: 0x06001F25 RID: 7973 RVA: 0x000727D0 File Offset: 0x000709D0
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(uLink.NetworkViewID viewID, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(networkView, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6);
		return true;
	}

	// Token: 0x06001F26 RID: 7974 RVA: 0x00072820 File Offset: 0x00070A20
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(uLink.NetworkViewID viewID, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(networkView, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6);
		return true;
	}

	// Token: 0x06001F27 RID: 7975 RVA: 0x00072870 File Offset: 0x00070A70
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(uLink.NetworkViewID viewID, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(networkView, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6);
		return true;
	}

	// Token: 0x06001F28 RID: 7976 RVA: 0x000728C0 File Offset: 0x00070AC0
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(uLink.NetworkViewID viewID, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(networkView, messageName, target, p0, p1, p2, p3, p4, p5, p6);
		return true;
	}

	// Token: 0x06001F29 RID: 7977 RVA: 0x00072910 File Offset: 0x00070B10
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(uLink.NetworkViewID viewID, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(networkView, messageName, targets, p0, p1, p2, p3, p4, p5, p6);
		return true;
	}

	// Token: 0x06001F2A RID: 7978 RVA: 0x00072960 File Offset: 0x00070B60
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(uLink.NetworkViewID viewID, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(networkView, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7);
		return true;
	}

	// Token: 0x06001F2B RID: 7979 RVA: 0x000729B4 File Offset: 0x00070BB4
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(uLink.NetworkViewID viewID, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(networkView, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7);
		return true;
	}

	// Token: 0x06001F2C RID: 7980 RVA: 0x00072A08 File Offset: 0x00070C08
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(uLink.NetworkViewID viewID, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(networkView, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7);
		return true;
	}

	// Token: 0x06001F2D RID: 7981 RVA: 0x00072A5C File Offset: 0x00070C5C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(uLink.NetworkViewID viewID, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(networkView, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7);
		return true;
	}

	// Token: 0x06001F2E RID: 7982 RVA: 0x00072AAC File Offset: 0x00070CAC
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(uLink.NetworkViewID viewID, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(networkView, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7);
		return true;
	}

	// Token: 0x06001F2F RID: 7983 RVA: 0x00072AFC File Offset: 0x00070CFC
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(uLink.NetworkViewID viewID, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(networkView, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7);
		return true;
	}

	// Token: 0x06001F30 RID: 7984 RVA: 0x00072B4C File Offset: 0x00070D4C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(uLink.NetworkViewID viewID, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(networkView, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8);
		return true;
	}

	// Token: 0x06001F31 RID: 7985 RVA: 0x00072BA0 File Offset: 0x00070DA0
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(uLink.NetworkViewID viewID, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(networkView, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8);
		return true;
	}

	// Token: 0x06001F32 RID: 7986 RVA: 0x00072BF4 File Offset: 0x00070DF4
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(uLink.NetworkViewID viewID, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(networkView, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8);
		return true;
	}

	// Token: 0x06001F33 RID: 7987 RVA: 0x00072C48 File Offset: 0x00070E48
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(uLink.NetworkViewID viewID, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(networkView, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8);
		return true;
	}

	// Token: 0x06001F34 RID: 7988 RVA: 0x00072C9C File Offset: 0x00070E9C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(uLink.NetworkViewID viewID, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(networkView, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8);
		return true;
	}

	// Token: 0x06001F35 RID: 7989 RVA: 0x00072CF0 File Offset: 0x00070EF0
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(uLink.NetworkViewID viewID, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(networkView, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8);
		return true;
	}

	// Token: 0x06001F36 RID: 7990 RVA: 0x00072D44 File Offset: 0x00070F44
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(uLink.NetworkViewID viewID, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(networkView, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
		return true;
	}

	// Token: 0x06001F37 RID: 7991 RVA: 0x00072D9C File Offset: 0x00070F9C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(uLink.NetworkViewID viewID, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(networkView, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
		return true;
	}

	// Token: 0x06001F38 RID: 7992 RVA: 0x00072DF4 File Offset: 0x00070FF4
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(uLink.NetworkViewID viewID, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(networkView, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
		return true;
	}

	// Token: 0x06001F39 RID: 7993 RVA: 0x00072E4C File Offset: 0x0007104C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(uLink.NetworkViewID viewID, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(networkView, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
		return true;
	}

	// Token: 0x06001F3A RID: 7994 RVA: 0x00072EA0 File Offset: 0x000710A0
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(uLink.NetworkViewID viewID, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(networkView, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
		return true;
	}

	// Token: 0x06001F3B RID: 7995 RVA: 0x00072EF4 File Offset: 0x000710F4
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(uLink.NetworkViewID viewID, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(networkView, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
		return true;
	}

	// Token: 0x06001F3C RID: 7996 RVA: 0x00072F48 File Offset: 0x00071148
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(uLink.NetworkViewID viewID, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(networkView, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
		return true;
	}

	// Token: 0x06001F3D RID: 7997 RVA: 0x00072FA0 File Offset: 0x000711A0
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(uLink.NetworkViewID viewID, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(networkView, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
		return true;
	}

	// Token: 0x06001F3E RID: 7998 RVA: 0x00072FF8 File Offset: 0x000711F8
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(uLink.NetworkViewID viewID, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(networkView, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
		return true;
	}

	// Token: 0x06001F3F RID: 7999 RVA: 0x00073050 File Offset: 0x00071250
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(uLink.NetworkViewID viewID, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(networkView, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
		return true;
	}

	// Token: 0x06001F40 RID: 8000 RVA: 0x000730A8 File Offset: 0x000712A8
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(uLink.NetworkViewID viewID, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(networkView, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
		return true;
	}

	// Token: 0x06001F41 RID: 8001 RVA: 0x00073100 File Offset: 0x00071300
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(uLink.NetworkViewID viewID, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(networkView, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
		return true;
	}

	// Token: 0x06001F42 RID: 8002 RVA: 0x00073158 File Offset: 0x00071358
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(uLink.NetworkViewID viewID, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(networkView, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
		return true;
	}

	// Token: 0x06001F43 RID: 8003 RVA: 0x000731B4 File Offset: 0x000713B4
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(uLink.NetworkViewID viewID, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(networkView, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
		return true;
	}

	// Token: 0x06001F44 RID: 8004 RVA: 0x00073210 File Offset: 0x00071410
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(uLink.NetworkViewID viewID, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(networkView, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
		return true;
	}

	// Token: 0x06001F45 RID: 8005 RVA: 0x0007326C File Offset: 0x0007146C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(uLink.NetworkViewID viewID, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(networkView, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
		return true;
	}

	// Token: 0x06001F46 RID: 8006 RVA: 0x000732C4 File Offset: 0x000714C4
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(uLink.NetworkViewID viewID, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(networkView, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
		return true;
	}

	// Token: 0x06001F47 RID: 8007 RVA: 0x0007331C File Offset: 0x0007151C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(uLink.NetworkViewID viewID, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(viewID);
		if (!networkView)
		{
			Debug.LogError(string.Format("No Network View with id {0} to send RPC \"{1}\"", viewID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(networkView, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
		return true;
	}

	// Token: 0x06001F48 RID: 8008 RVA: 0x00073374 File Offset: 0x00071574
	public static bool RPC(global::NetEntityID entID, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC((uLink.NetworkViewID)entID, flags, messageName, rpcMode);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC(ngcView, flags, messageName, rpcMode);
		return true;
	}

	// Token: 0x06001F49 RID: 8009 RVA: 0x000733D4 File Offset: 0x000715D4
	public static bool RPC(global::NetEntityID entID, NetworkFlags flags, string messageName, uLink.NetworkPlayer target)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC((uLink.NetworkViewID)entID, flags, messageName, target);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC(ngcView, flags, messageName, target);
		return true;
	}

	// Token: 0x06001F4A RID: 8010 RVA: 0x00073434 File Offset: 0x00071634
	public static bool RPC(global::NetEntityID entID, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC((uLink.NetworkViewID)entID, flags, messageName, targets);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC(ngcView, flags, messageName, targets);
		return true;
	}

	// Token: 0x06001F4B RID: 8011 RVA: 0x00073494 File Offset: 0x00071694
	public static bool RPC(global::NetEntityID entID, string messageName, uLink.RPCMode rpcMode)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC((uLink.NetworkViewID)entID, messageName, rpcMode);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC(ngcView, messageName, rpcMode);
		return true;
	}

	// Token: 0x06001F4C RID: 8012 RVA: 0x000734F0 File Offset: 0x000716F0
	public static bool RPC(global::NetEntityID entID, string messageName, uLink.NetworkPlayer target)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC((uLink.NetworkViewID)entID, messageName, target);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC(ngcView, messageName, target);
		return true;
	}

	// Token: 0x06001F4D RID: 8013 RVA: 0x0007354C File Offset: 0x0007174C
	public static bool RPC(global::NetEntityID entID, string messageName, IEnumerable<uLink.NetworkPlayer> targets)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC((uLink.NetworkViewID)entID, messageName, targets);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC(ngcView, messageName, targets);
		return true;
	}

	// Token: 0x06001F4E RID: 8014 RVA: 0x000735A8 File Offset: 0x000717A8
	public static bool RPC<P0>(global::NetEntityID entID, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0>((uLink.NetworkViewID)entID, flags, messageName, rpcMode, p0);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0>(ngcView, flags, messageName, rpcMode, p0);
		return true;
	}

	// Token: 0x06001F4F RID: 8015 RVA: 0x0007360C File Offset: 0x0007180C
	public static bool RPC<P0>(global::NetEntityID entID, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0>((uLink.NetworkViewID)entID, flags, messageName, target, p0);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0>(ngcView, flags, messageName, target, p0);
		return true;
	}

	// Token: 0x06001F50 RID: 8016 RVA: 0x00073670 File Offset: 0x00071870
	public static bool RPC<P0>(global::NetEntityID entID, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0>((uLink.NetworkViewID)entID, flags, messageName, targets, p0);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0>(ngcView, flags, messageName, targets, p0);
		return true;
	}

	// Token: 0x06001F51 RID: 8017 RVA: 0x000736D4 File Offset: 0x000718D4
	public static bool RPC<P0>(global::NetEntityID entID, string messageName, uLink.RPCMode rpcMode, P0 p0)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0>((uLink.NetworkViewID)entID, messageName, rpcMode, p0);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0>(ngcView, messageName, rpcMode, p0);
		return true;
	}

	// Token: 0x06001F52 RID: 8018 RVA: 0x00073734 File Offset: 0x00071934
	public static bool RPC<P0>(global::NetEntityID entID, string messageName, uLink.NetworkPlayer target, P0 p0)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0>((uLink.NetworkViewID)entID, messageName, target, p0);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0>(ngcView, messageName, target, p0);
		return true;
	}

	// Token: 0x06001F53 RID: 8019 RVA: 0x00073794 File Offset: 0x00071994
	public static bool RPC<P0>(global::NetEntityID entID, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0>((uLink.NetworkViewID)entID, messageName, targets, p0);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0>(ngcView, messageName, targets, p0);
		return true;
	}

	// Token: 0x06001F54 RID: 8020 RVA: 0x000737F4 File Offset: 0x000719F4
	public static bool RPC<P0, P1>(global::NetEntityID entID, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1>((uLink.NetworkViewID)entID, flags, messageName, rpcMode, p0, p1);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1>(ngcView, flags, messageName, rpcMode, p0, p1);
		return true;
	}

	// Token: 0x06001F55 RID: 8021 RVA: 0x0007385C File Offset: 0x00071A5C
	public static bool RPC<P0, P1>(global::NetEntityID entID, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1>((uLink.NetworkViewID)entID, flags, messageName, target, p0, p1);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1>(ngcView, flags, messageName, target, p0, p1);
		return true;
	}

	// Token: 0x06001F56 RID: 8022 RVA: 0x000738C4 File Offset: 0x00071AC4
	public static bool RPC<P0, P1>(global::NetEntityID entID, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1>((uLink.NetworkViewID)entID, flags, messageName, targets, p0, p1);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1>(ngcView, flags, messageName, targets, p0, p1);
		return true;
	}

	// Token: 0x06001F57 RID: 8023 RVA: 0x0007392C File Offset: 0x00071B2C
	public static bool RPC<P0, P1>(global::NetEntityID entID, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1>((uLink.NetworkViewID)entID, messageName, rpcMode, p0, p1);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1>(ngcView, messageName, rpcMode, p0, p1);
		return true;
	}

	// Token: 0x06001F58 RID: 8024 RVA: 0x00073990 File Offset: 0x00071B90
	public static bool RPC<P0, P1>(global::NetEntityID entID, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1>((uLink.NetworkViewID)entID, messageName, target, p0, p1);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1>(ngcView, messageName, target, p0, p1);
		return true;
	}

	// Token: 0x06001F59 RID: 8025 RVA: 0x000739F4 File Offset: 0x00071BF4
	public static bool RPC<P0, P1>(global::NetEntityID entID, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1>((uLink.NetworkViewID)entID, messageName, targets, p0, p1);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1>(ngcView, messageName, targets, p0, p1);
		return true;
	}

	// Token: 0x06001F5A RID: 8026 RVA: 0x00073A58 File Offset: 0x00071C58
	public static bool RPC<P0, P1, P2>(global::NetEntityID entID, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2>((uLink.NetworkViewID)entID, flags, messageName, rpcMode, p0, p1, p2);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2>(ngcView, flags, messageName, rpcMode, p0, p1, p2);
		return true;
	}

	// Token: 0x06001F5B RID: 8027 RVA: 0x00073AC4 File Offset: 0x00071CC4
	public static bool RPC<P0, P1, P2>(global::NetEntityID entID, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2>((uLink.NetworkViewID)entID, flags, messageName, target, p0, p1, p2);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2>(ngcView, flags, messageName, target, p0, p1, p2);
		return true;
	}

	// Token: 0x06001F5C RID: 8028 RVA: 0x00073B30 File Offset: 0x00071D30
	public static bool RPC<P0, P1, P2>(global::NetEntityID entID, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2>((uLink.NetworkViewID)entID, flags, messageName, targets, p0, p1, p2);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2>(ngcView, flags, messageName, targets, p0, p1, p2);
		return true;
	}

	// Token: 0x06001F5D RID: 8029 RVA: 0x00073B9C File Offset: 0x00071D9C
	public static bool RPC<P0, P1, P2>(global::NetEntityID entID, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2>((uLink.NetworkViewID)entID, messageName, rpcMode, p0, p1, p2);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2>(ngcView, messageName, rpcMode, p0, p1, p2);
		return true;
	}

	// Token: 0x06001F5E RID: 8030 RVA: 0x00073C04 File Offset: 0x00071E04
	public static bool RPC<P0, P1, P2>(global::NetEntityID entID, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2>((uLink.NetworkViewID)entID, messageName, target, p0, p1, p2);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2>(ngcView, messageName, target, p0, p1, p2);
		return true;
	}

	// Token: 0x06001F5F RID: 8031 RVA: 0x00073C6C File Offset: 0x00071E6C
	public static bool RPC<P0, P1, P2>(global::NetEntityID entID, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2>((uLink.NetworkViewID)entID, messageName, targets, p0, p1, p2);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2>(ngcView, messageName, targets, p0, p1, p2);
		return true;
	}

	// Token: 0x06001F60 RID: 8032 RVA: 0x00073CD4 File Offset: 0x00071ED4
	public static bool RPC<P0, P1, P2, P3>(global::NetEntityID entID, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3>((uLink.NetworkViewID)entID, flags, messageName, rpcMode, p0, p1, p2, p3);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3>(ngcView, flags, messageName, rpcMode, p0, p1, p2, p3);
		return true;
	}

	// Token: 0x06001F61 RID: 8033 RVA: 0x00073D44 File Offset: 0x00071F44
	public static bool RPC<P0, P1, P2, P3>(global::NetEntityID entID, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3>((uLink.NetworkViewID)entID, flags, messageName, target, p0, p1, p2, p3);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3>(ngcView, flags, messageName, target, p0, p1, p2, p3);
		return true;
	}

	// Token: 0x06001F62 RID: 8034 RVA: 0x00073DB4 File Offset: 0x00071FB4
	public static bool RPC<P0, P1, P2, P3>(global::NetEntityID entID, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3>((uLink.NetworkViewID)entID, flags, messageName, targets, p0, p1, p2, p3);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3>(ngcView, flags, messageName, targets, p0, p1, p2, p3);
		return true;
	}

	// Token: 0x06001F63 RID: 8035 RVA: 0x00073E24 File Offset: 0x00072024
	public static bool RPC<P0, P1, P2, P3>(global::NetEntityID entID, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3>((uLink.NetworkViewID)entID, messageName, rpcMode, p0, p1, p2, p3);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3>(ngcView, messageName, rpcMode, p0, p1, p2, p3);
		return true;
	}

	// Token: 0x06001F64 RID: 8036 RVA: 0x00073E90 File Offset: 0x00072090
	public static bool RPC<P0, P1, P2, P3>(global::NetEntityID entID, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3>((uLink.NetworkViewID)entID, messageName, target, p0, p1, p2, p3);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3>(ngcView, messageName, target, p0, p1, p2, p3);
		return true;
	}

	// Token: 0x06001F65 RID: 8037 RVA: 0x00073EFC File Offset: 0x000720FC
	public static bool RPC<P0, P1, P2, P3>(global::NetEntityID entID, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3>((uLink.NetworkViewID)entID, messageName, targets, p0, p1, p2, p3);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3>(ngcView, messageName, targets, p0, p1, p2, p3);
		return true;
	}

	// Token: 0x06001F66 RID: 8038 RVA: 0x00073F68 File Offset: 0x00072168
	public static bool RPC<P0, P1, P2, P3, P4>(global::NetEntityID entID, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4>((uLink.NetworkViewID)entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4>(ngcView, flags, messageName, rpcMode, p0, p1, p2, p3, p4);
		return true;
	}

	// Token: 0x06001F67 RID: 8039 RVA: 0x00073FDC File Offset: 0x000721DC
	public static bool RPC<P0, P1, P2, P3, P4>(global::NetEntityID entID, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4>((uLink.NetworkViewID)entID, flags, messageName, target, p0, p1, p2, p3, p4);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4>(ngcView, flags, messageName, target, p0, p1, p2, p3, p4);
		return true;
	}

	// Token: 0x06001F68 RID: 8040 RVA: 0x00074050 File Offset: 0x00072250
	public static bool RPC<P0, P1, P2, P3, P4>(global::NetEntityID entID, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4>((uLink.NetworkViewID)entID, flags, messageName, targets, p0, p1, p2, p3, p4);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4>(ngcView, flags, messageName, targets, p0, p1, p2, p3, p4);
		return true;
	}

	// Token: 0x06001F69 RID: 8041 RVA: 0x000740C4 File Offset: 0x000722C4
	public static bool RPC<P0, P1, P2, P3, P4>(global::NetEntityID entID, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4>((uLink.NetworkViewID)entID, messageName, rpcMode, p0, p1, p2, p3, p4);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4>(ngcView, messageName, rpcMode, p0, p1, p2, p3, p4);
		return true;
	}

	// Token: 0x06001F6A RID: 8042 RVA: 0x00074134 File Offset: 0x00072334
	public static bool RPC<P0, P1, P2, P3, P4>(global::NetEntityID entID, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4>((uLink.NetworkViewID)entID, messageName, target, p0, p1, p2, p3, p4);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4>(ngcView, messageName, target, p0, p1, p2, p3, p4);
		return true;
	}

	// Token: 0x06001F6B RID: 8043 RVA: 0x000741A4 File Offset: 0x000723A4
	public static bool RPC<P0, P1, P2, P3, P4>(global::NetEntityID entID, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4>((uLink.NetworkViewID)entID, messageName, targets, p0, p1, p2, p3, p4);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4>(ngcView, messageName, targets, p0, p1, p2, p3, p4);
		return true;
	}

	// Token: 0x06001F6C RID: 8044 RVA: 0x00074214 File Offset: 0x00072414
	public static bool RPC<P0, P1, P2, P3, P4, P5>(global::NetEntityID entID, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4, P5>((uLink.NetworkViewID)entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5>(ngcView, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5);
		return true;
	}

	// Token: 0x06001F6D RID: 8045 RVA: 0x0007428C File Offset: 0x0007248C
	public static bool RPC<P0, P1, P2, P3, P4, P5>(global::NetEntityID entID, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4, P5>((uLink.NetworkViewID)entID, flags, messageName, target, p0, p1, p2, p3, p4, p5);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5>(ngcView, flags, messageName, target, p0, p1, p2, p3, p4, p5);
		return true;
	}

	// Token: 0x06001F6E RID: 8046 RVA: 0x00074304 File Offset: 0x00072504
	public static bool RPC<P0, P1, P2, P3, P4, P5>(global::NetEntityID entID, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4, P5>((uLink.NetworkViewID)entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5>(ngcView, flags, messageName, targets, p0, p1, p2, p3, p4, p5);
		return true;
	}

	// Token: 0x06001F6F RID: 8047 RVA: 0x0007437C File Offset: 0x0007257C
	public static bool RPC<P0, P1, P2, P3, P4, P5>(global::NetEntityID entID, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4, P5>((uLink.NetworkViewID)entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5>(ngcView, messageName, rpcMode, p0, p1, p2, p3, p4, p5);
		return true;
	}

	// Token: 0x06001F70 RID: 8048 RVA: 0x000743F0 File Offset: 0x000725F0
	public static bool RPC<P0, P1, P2, P3, P4, P5>(global::NetEntityID entID, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4, P5>((uLink.NetworkViewID)entID, messageName, target, p0, p1, p2, p3, p4, p5);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5>(ngcView, messageName, target, p0, p1, p2, p3, p4, p5);
		return true;
	}

	// Token: 0x06001F71 RID: 8049 RVA: 0x00074464 File Offset: 0x00072664
	public static bool RPC<P0, P1, P2, P3, P4, P5>(global::NetEntityID entID, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4, P5>((uLink.NetworkViewID)entID, messageName, targets, p0, p1, p2, p3, p4, p5);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5>(ngcView, messageName, targets, p0, p1, p2, p3, p4, p5);
		return true;
	}

	// Token: 0x06001F72 RID: 8050 RVA: 0x000744D8 File Offset: 0x000726D8
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(global::NetEntityID entID, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>((uLink.NetworkViewID)entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(ngcView, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6);
		return true;
	}

	// Token: 0x06001F73 RID: 8051 RVA: 0x00074554 File Offset: 0x00072754
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(global::NetEntityID entID, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>((uLink.NetworkViewID)entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(ngcView, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6);
		return true;
	}

	// Token: 0x06001F74 RID: 8052 RVA: 0x000745D0 File Offset: 0x000727D0
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(global::NetEntityID entID, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>((uLink.NetworkViewID)entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(ngcView, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6);
		return true;
	}

	// Token: 0x06001F75 RID: 8053 RVA: 0x0007464C File Offset: 0x0007284C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(global::NetEntityID entID, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>((uLink.NetworkViewID)entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(ngcView, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6);
		return true;
	}

	// Token: 0x06001F76 RID: 8054 RVA: 0x000746C4 File Offset: 0x000728C4
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(global::NetEntityID entID, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>((uLink.NetworkViewID)entID, messageName, target, p0, p1, p2, p3, p4, p5, p6);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(ngcView, messageName, target, p0, p1, p2, p3, p4, p5, p6);
		return true;
	}

	// Token: 0x06001F77 RID: 8055 RVA: 0x0007473C File Offset: 0x0007293C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(global::NetEntityID entID, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>((uLink.NetworkViewID)entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(ngcView, messageName, targets, p0, p1, p2, p3, p4, p5, p6);
		return true;
	}

	// Token: 0x06001F78 RID: 8056 RVA: 0x000747B4 File Offset: 0x000729B4
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(global::NetEntityID entID, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>((uLink.NetworkViewID)entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(ngcView, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7);
		return true;
	}

	// Token: 0x06001F79 RID: 8057 RVA: 0x00074834 File Offset: 0x00072A34
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(global::NetEntityID entID, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>((uLink.NetworkViewID)entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(ngcView, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7);
		return true;
	}

	// Token: 0x06001F7A RID: 8058 RVA: 0x000748B4 File Offset: 0x00072AB4
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(global::NetEntityID entID, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>((uLink.NetworkViewID)entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(ngcView, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7);
		return true;
	}

	// Token: 0x06001F7B RID: 8059 RVA: 0x00074934 File Offset: 0x00072B34
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(global::NetEntityID entID, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>((uLink.NetworkViewID)entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(ngcView, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7);
		return true;
	}

	// Token: 0x06001F7C RID: 8060 RVA: 0x000749B0 File Offset: 0x00072BB0
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(global::NetEntityID entID, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>((uLink.NetworkViewID)entID, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(ngcView, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7);
		return true;
	}

	// Token: 0x06001F7D RID: 8061 RVA: 0x00074A2C File Offset: 0x00072C2C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(global::NetEntityID entID, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>((uLink.NetworkViewID)entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(ngcView, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7);
		return true;
	}

	// Token: 0x06001F7E RID: 8062 RVA: 0x00074AA8 File Offset: 0x00072CA8
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(global::NetEntityID entID, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>((uLink.NetworkViewID)entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(ngcView, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8);
		return true;
	}

	// Token: 0x06001F7F RID: 8063 RVA: 0x00074B2C File Offset: 0x00072D2C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(global::NetEntityID entID, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>((uLink.NetworkViewID)entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(ngcView, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8);
		return true;
	}

	// Token: 0x06001F80 RID: 8064 RVA: 0x00074BB0 File Offset: 0x00072DB0
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(global::NetEntityID entID, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>((uLink.NetworkViewID)entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(ngcView, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8);
		return true;
	}

	// Token: 0x06001F81 RID: 8065 RVA: 0x00074C34 File Offset: 0x00072E34
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(global::NetEntityID entID, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>((uLink.NetworkViewID)entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(ngcView, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8);
		return true;
	}

	// Token: 0x06001F82 RID: 8066 RVA: 0x00074CB4 File Offset: 0x00072EB4
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(global::NetEntityID entID, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>((uLink.NetworkViewID)entID, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(ngcView, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8);
		return true;
	}

	// Token: 0x06001F83 RID: 8067 RVA: 0x00074D34 File Offset: 0x00072F34
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(global::NetEntityID entID, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>((uLink.NetworkViewID)entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(ngcView, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8);
		return true;
	}

	// Token: 0x06001F84 RID: 8068 RVA: 0x00074DB4 File Offset: 0x00072FB4
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(global::NetEntityID entID, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>((uLink.NetworkViewID)entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(ngcView, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
		return true;
	}

	// Token: 0x06001F85 RID: 8069 RVA: 0x00074E3C File Offset: 0x0007303C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(global::NetEntityID entID, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>((uLink.NetworkViewID)entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(ngcView, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
		return true;
	}

	// Token: 0x06001F86 RID: 8070 RVA: 0x00074EC4 File Offset: 0x000730C4
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(global::NetEntityID entID, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>((uLink.NetworkViewID)entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(ngcView, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
		return true;
	}

	// Token: 0x06001F87 RID: 8071 RVA: 0x00074F4C File Offset: 0x0007314C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(global::NetEntityID entID, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>((uLink.NetworkViewID)entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(ngcView, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
		return true;
	}

	// Token: 0x06001F88 RID: 8072 RVA: 0x00074FD0 File Offset: 0x000731D0
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(global::NetEntityID entID, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>((uLink.NetworkViewID)entID, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(ngcView, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
		return true;
	}

	// Token: 0x06001F89 RID: 8073 RVA: 0x00075054 File Offset: 0x00073254
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(global::NetEntityID entID, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>((uLink.NetworkViewID)entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(ngcView, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
		return true;
	}

	// Token: 0x06001F8A RID: 8074 RVA: 0x000750D8 File Offset: 0x000732D8
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(global::NetEntityID entID, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>((uLink.NetworkViewID)entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(ngcView, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
		return true;
	}

	// Token: 0x06001F8B RID: 8075 RVA: 0x00075164 File Offset: 0x00073364
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(global::NetEntityID entID, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>((uLink.NetworkViewID)entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(ngcView, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
		return true;
	}

	// Token: 0x06001F8C RID: 8076 RVA: 0x000751F0 File Offset: 0x000733F0
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(global::NetEntityID entID, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>((uLink.NetworkViewID)entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(ngcView, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
		return true;
	}

	// Token: 0x06001F8D RID: 8077 RVA: 0x0007527C File Offset: 0x0007347C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(global::NetEntityID entID, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>((uLink.NetworkViewID)entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(ngcView, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
		return true;
	}

	// Token: 0x06001F8E RID: 8078 RVA: 0x00075304 File Offset: 0x00073504
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(global::NetEntityID entID, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>((uLink.NetworkViewID)entID, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(ngcView, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
		return true;
	}

	// Token: 0x06001F8F RID: 8079 RVA: 0x0007538C File Offset: 0x0007358C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(global::NetEntityID entID, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>((uLink.NetworkViewID)entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(ngcView, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
		return true;
	}

	// Token: 0x06001F90 RID: 8080 RVA: 0x00075414 File Offset: 0x00073614
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(global::NetEntityID entID, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>((uLink.NetworkViewID)entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(ngcView, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
		return true;
	}

	// Token: 0x06001F91 RID: 8081 RVA: 0x000754A4 File Offset: 0x000736A4
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(global::NetEntityID entID, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>((uLink.NetworkViewID)entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(ngcView, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
		return true;
	}

	// Token: 0x06001F92 RID: 8082 RVA: 0x00075534 File Offset: 0x00073734
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(global::NetEntityID entID, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>((uLink.NetworkViewID)entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(ngcView, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
		return true;
	}

	// Token: 0x06001F93 RID: 8083 RVA: 0x000755C4 File Offset: 0x000737C4
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(global::NetEntityID entID, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>((uLink.NetworkViewID)entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(ngcView, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
		return true;
	}

	// Token: 0x06001F94 RID: 8084 RVA: 0x00075650 File Offset: 0x00073850
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(global::NetEntityID entID, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>((uLink.NetworkViewID)entID, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(ngcView, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
		return true;
	}

	// Token: 0x06001F95 RID: 8085 RVA: 0x000756DC File Offset: 0x000738DC
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(global::NetEntityID entID, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		if (!entID.isNGC)
		{
			return global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>((uLink.NetworkViewID)entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
		}
		global::NGCView ngcView = entID.ngcView;
		if (!ngcView)
		{
			Debug.LogError(string.Format("No NGC View with id {0} to send RPC \"{1}\"", entID, messageName));
			return false;
		}
		global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(ngcView, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
		return true;
	}

	// Token: 0x06001F96 RID: 8086 RVA: 0x00075768 File Offset: 0x00073968
	public static bool RPC(GameObject entity, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC(entID, flags, messageName, rpcMode);
	}

	// Token: 0x06001F97 RID: 8087 RVA: 0x00075790 File Offset: 0x00073990
	public static bool RPC(GameObject entity, NetworkFlags flags, string messageName, uLink.NetworkPlayer target)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC(entID, flags, messageName, target);
	}

	// Token: 0x06001F98 RID: 8088 RVA: 0x000757B8 File Offset: 0x000739B8
	public static bool RPC(GameObject entity, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC(entID, flags, messageName, targets);
	}

	// Token: 0x06001F99 RID: 8089 RVA: 0x000757E0 File Offset: 0x000739E0
	public static bool RPC(GameObject entity, string messageName, uLink.RPCMode rpcMode)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC(entID, messageName, rpcMode);
	}

	// Token: 0x06001F9A RID: 8090 RVA: 0x00075808 File Offset: 0x00073A08
	public static bool RPC(GameObject entity, string messageName, uLink.NetworkPlayer target)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC(entID, messageName, target);
	}

	// Token: 0x06001F9B RID: 8091 RVA: 0x00075830 File Offset: 0x00073A30
	public static bool RPC(GameObject entity, string messageName, IEnumerable<uLink.NetworkPlayer> targets)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC(entID, messageName, targets);
	}

	// Token: 0x06001F9C RID: 8092 RVA: 0x00075858 File Offset: 0x00073A58
	public static bool RPC(MonoBehaviour entityScript, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC(entID, flags, messageName, rpcMode);
	}

	// Token: 0x06001F9D RID: 8093 RVA: 0x00075880 File Offset: 0x00073A80
	public static bool RPC(MonoBehaviour entityScript, NetworkFlags flags, string messageName, uLink.NetworkPlayer target)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC(entID, flags, messageName, target);
	}

	// Token: 0x06001F9E RID: 8094 RVA: 0x000758A8 File Offset: 0x00073AA8
	public static bool RPC(MonoBehaviour entityScript, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC(entID, flags, messageName, targets);
	}

	// Token: 0x06001F9F RID: 8095 RVA: 0x000758D0 File Offset: 0x00073AD0
	public static bool RPC(MonoBehaviour entityScript, string messageName, uLink.RPCMode rpcMode)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC(entID, messageName, rpcMode);
	}

	// Token: 0x06001FA0 RID: 8096 RVA: 0x000758F8 File Offset: 0x00073AF8
	public static bool RPC(MonoBehaviour entityScript, string messageName, uLink.NetworkPlayer target)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC(entID, messageName, target);
	}

	// Token: 0x06001FA1 RID: 8097 RVA: 0x00075920 File Offset: 0x00073B20
	public static bool RPC(MonoBehaviour entityScript, string messageName, IEnumerable<uLink.NetworkPlayer> targets)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC(entID, messageName, targets);
	}

	// Token: 0x06001FA2 RID: 8098 RVA: 0x00075948 File Offset: 0x00073B48
	public static bool RPC(Component entityComponent, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC(entID, flags, messageName, rpcMode);
	}

	// Token: 0x06001FA3 RID: 8099 RVA: 0x00075970 File Offset: 0x00073B70
	public static bool RPC(Component entityComponent, NetworkFlags flags, string messageName, uLink.NetworkPlayer target)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC(entID, flags, messageName, target);
	}

	// Token: 0x06001FA4 RID: 8100 RVA: 0x00075998 File Offset: 0x00073B98
	public static bool RPC(Component entityComponent, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC(entID, flags, messageName, targets);
	}

	// Token: 0x06001FA5 RID: 8101 RVA: 0x000759C0 File Offset: 0x00073BC0
	public static bool RPC(Component entityComponent, string messageName, uLink.RPCMode rpcMode)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC(entID, messageName, rpcMode);
	}

	// Token: 0x06001FA6 RID: 8102 RVA: 0x000759E8 File Offset: 0x00073BE8
	public static bool RPC(Component entityComponent, string messageName, uLink.NetworkPlayer target)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC(entID, messageName, target);
	}

	// Token: 0x06001FA7 RID: 8103 RVA: 0x00075A10 File Offset: 0x00073C10
	public static bool RPC(Component entityComponent, string messageName, IEnumerable<uLink.NetworkPlayer> targets)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC(entID, messageName, targets);
	}

	// Token: 0x06001FA8 RID: 8104 RVA: 0x00075A38 File Offset: 0x00073C38
	public static bool RPC<P0>(GameObject entity, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0>(entID, flags, messageName, rpcMode, p0);
	}

	// Token: 0x06001FA9 RID: 8105 RVA: 0x00075A60 File Offset: 0x00073C60
	public static bool RPC<P0>(GameObject entity, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0>(entID, flags, messageName, target, p0);
	}

	// Token: 0x06001FAA RID: 8106 RVA: 0x00075A88 File Offset: 0x00073C88
	public static bool RPC<P0>(GameObject entity, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0>(entID, flags, messageName, targets, p0);
	}

	// Token: 0x06001FAB RID: 8107 RVA: 0x00075AB0 File Offset: 0x00073CB0
	public static bool RPC<P0>(GameObject entity, string messageName, uLink.RPCMode rpcMode, P0 p0)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0>(entID, messageName, rpcMode, p0);
	}

	// Token: 0x06001FAC RID: 8108 RVA: 0x00075AD8 File Offset: 0x00073CD8
	public static bool RPC<P0>(GameObject entity, string messageName, uLink.NetworkPlayer target, P0 p0)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0>(entID, messageName, target, p0);
	}

	// Token: 0x06001FAD RID: 8109 RVA: 0x00075B00 File Offset: 0x00073D00
	public static bool RPC<P0>(GameObject entity, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0>(entID, messageName, targets, p0);
	}

	// Token: 0x06001FAE RID: 8110 RVA: 0x00075B28 File Offset: 0x00073D28
	public static bool RPC<P0>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0>(entID, flags, messageName, rpcMode, p0);
	}

	// Token: 0x06001FAF RID: 8111 RVA: 0x00075B50 File Offset: 0x00073D50
	public static bool RPC<P0>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0>(entID, flags, messageName, target, p0);
	}

	// Token: 0x06001FB0 RID: 8112 RVA: 0x00075B78 File Offset: 0x00073D78
	public static bool RPC<P0>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0>(entID, flags, messageName, targets, p0);
	}

	// Token: 0x06001FB1 RID: 8113 RVA: 0x00075BA0 File Offset: 0x00073DA0
	public static bool RPC<P0>(MonoBehaviour entityScript, string messageName, uLink.RPCMode rpcMode, P0 p0)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0>(entID, messageName, rpcMode, p0);
	}

	// Token: 0x06001FB2 RID: 8114 RVA: 0x00075BC8 File Offset: 0x00073DC8
	public static bool RPC<P0>(MonoBehaviour entityScript, string messageName, uLink.NetworkPlayer target, P0 p0)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0>(entID, messageName, target, p0);
	}

	// Token: 0x06001FB3 RID: 8115 RVA: 0x00075BF0 File Offset: 0x00073DF0
	public static bool RPC<P0>(MonoBehaviour entityScript, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0>(entID, messageName, targets, p0);
	}

	// Token: 0x06001FB4 RID: 8116 RVA: 0x00075C18 File Offset: 0x00073E18
	public static bool RPC<P0>(Component entityComponent, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0>(entID, flags, messageName, rpcMode, p0);
	}

	// Token: 0x06001FB5 RID: 8117 RVA: 0x00075C40 File Offset: 0x00073E40
	public static bool RPC<P0>(Component entityComponent, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0>(entID, flags, messageName, target, p0);
	}

	// Token: 0x06001FB6 RID: 8118 RVA: 0x00075C68 File Offset: 0x00073E68
	public static bool RPC<P0>(Component entityComponent, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0>(entID, flags, messageName, targets, p0);
	}

	// Token: 0x06001FB7 RID: 8119 RVA: 0x00075C90 File Offset: 0x00073E90
	public static bool RPC<P0>(Component entityComponent, string messageName, uLink.RPCMode rpcMode, P0 p0)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0>(entID, messageName, rpcMode, p0);
	}

	// Token: 0x06001FB8 RID: 8120 RVA: 0x00075CB8 File Offset: 0x00073EB8
	public static bool RPC<P0>(Component entityComponent, string messageName, uLink.NetworkPlayer target, P0 p0)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0>(entID, messageName, target, p0);
	}

	// Token: 0x06001FB9 RID: 8121 RVA: 0x00075CE0 File Offset: 0x00073EE0
	public static bool RPC<P0>(Component entityComponent, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0>(entID, messageName, targets, p0);
	}

	// Token: 0x06001FBA RID: 8122 RVA: 0x00075D08 File Offset: 0x00073F08
	public static bool RPC<P0, P1>(GameObject entity, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1>(entID, flags, messageName, rpcMode, p0, p1);
	}

	// Token: 0x06001FBB RID: 8123 RVA: 0x00075D34 File Offset: 0x00073F34
	public static bool RPC<P0, P1>(GameObject entity, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1>(entID, flags, messageName, target, p0, p1);
	}

	// Token: 0x06001FBC RID: 8124 RVA: 0x00075D60 File Offset: 0x00073F60
	public static bool RPC<P0, P1>(GameObject entity, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1>(entID, flags, messageName, targets, p0, p1);
	}

	// Token: 0x06001FBD RID: 8125 RVA: 0x00075D8C File Offset: 0x00073F8C
	public static bool RPC<P0, P1>(GameObject entity, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1>(entID, messageName, rpcMode, p0, p1);
	}

	// Token: 0x06001FBE RID: 8126 RVA: 0x00075DB4 File Offset: 0x00073FB4
	public static bool RPC<P0, P1>(GameObject entity, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1>(entID, messageName, target, p0, p1);
	}

	// Token: 0x06001FBF RID: 8127 RVA: 0x00075DDC File Offset: 0x00073FDC
	public static bool RPC<P0, P1>(GameObject entity, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1>(entID, messageName, targets, p0, p1);
	}

	// Token: 0x06001FC0 RID: 8128 RVA: 0x00075E04 File Offset: 0x00074004
	public static bool RPC<P0, P1>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1>(entID, flags, messageName, rpcMode, p0, p1);
	}

	// Token: 0x06001FC1 RID: 8129 RVA: 0x00075E30 File Offset: 0x00074030
	public static bool RPC<P0, P1>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1>(entID, flags, messageName, target, p0, p1);
	}

	// Token: 0x06001FC2 RID: 8130 RVA: 0x00075E5C File Offset: 0x0007405C
	public static bool RPC<P0, P1>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1>(entID, flags, messageName, targets, p0, p1);
	}

	// Token: 0x06001FC3 RID: 8131 RVA: 0x00075E88 File Offset: 0x00074088
	public static bool RPC<P0, P1>(MonoBehaviour entityScript, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1>(entID, messageName, rpcMode, p0, p1);
	}

	// Token: 0x06001FC4 RID: 8132 RVA: 0x00075EB0 File Offset: 0x000740B0
	public static bool RPC<P0, P1>(MonoBehaviour entityScript, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1>(entID, messageName, target, p0, p1);
	}

	// Token: 0x06001FC5 RID: 8133 RVA: 0x00075ED8 File Offset: 0x000740D8
	public static bool RPC<P0, P1>(MonoBehaviour entityScript, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1>(entID, messageName, targets, p0, p1);
	}

	// Token: 0x06001FC6 RID: 8134 RVA: 0x00075F00 File Offset: 0x00074100
	public static bool RPC<P0, P1>(Component entityComponent, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1>(entID, flags, messageName, rpcMode, p0, p1);
	}

	// Token: 0x06001FC7 RID: 8135 RVA: 0x00075F2C File Offset: 0x0007412C
	public static bool RPC<P0, P1>(Component entityComponent, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1>(entID, flags, messageName, target, p0, p1);
	}

	// Token: 0x06001FC8 RID: 8136 RVA: 0x00075F58 File Offset: 0x00074158
	public static bool RPC<P0, P1>(Component entityComponent, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1>(entID, flags, messageName, targets, p0, p1);
	}

	// Token: 0x06001FC9 RID: 8137 RVA: 0x00075F84 File Offset: 0x00074184
	public static bool RPC<P0, P1>(Component entityComponent, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1>(entID, messageName, rpcMode, p0, p1);
	}

	// Token: 0x06001FCA RID: 8138 RVA: 0x00075FAC File Offset: 0x000741AC
	public static bool RPC<P0, P1>(Component entityComponent, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1>(entID, messageName, target, p0, p1);
	}

	// Token: 0x06001FCB RID: 8139 RVA: 0x00075FD4 File Offset: 0x000741D4
	public static bool RPC<P0, P1>(Component entityComponent, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1>(entID, messageName, targets, p0, p1);
	}

	// Token: 0x06001FCC RID: 8140 RVA: 0x00075FFC File Offset: 0x000741FC
	public static bool RPC<P0, P1, P2>(GameObject entity, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2>(entID, flags, messageName, rpcMode, p0, p1, p2);
	}

	// Token: 0x06001FCD RID: 8141 RVA: 0x00076028 File Offset: 0x00074228
	public static bool RPC<P0, P1, P2>(GameObject entity, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2>(entID, flags, messageName, target, p0, p1, p2);
	}

	// Token: 0x06001FCE RID: 8142 RVA: 0x00076054 File Offset: 0x00074254
	public static bool RPC<P0, P1, P2>(GameObject entity, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2>(entID, flags, messageName, targets, p0, p1, p2);
	}

	// Token: 0x06001FCF RID: 8143 RVA: 0x00076080 File Offset: 0x00074280
	public static bool RPC<P0, P1, P2>(GameObject entity, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2>(entID, messageName, rpcMode, p0, p1, p2);
	}

	// Token: 0x06001FD0 RID: 8144 RVA: 0x000760AC File Offset: 0x000742AC
	public static bool RPC<P0, P1, P2>(GameObject entity, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2>(entID, messageName, target, p0, p1, p2);
	}

	// Token: 0x06001FD1 RID: 8145 RVA: 0x000760D8 File Offset: 0x000742D8
	public static bool RPC<P0, P1, P2>(GameObject entity, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2>(entID, messageName, targets, p0, p1, p2);
	}

	// Token: 0x06001FD2 RID: 8146 RVA: 0x00076104 File Offset: 0x00074304
	public static bool RPC<P0, P1, P2>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2>(entID, flags, messageName, rpcMode, p0, p1, p2);
	}

	// Token: 0x06001FD3 RID: 8147 RVA: 0x00076130 File Offset: 0x00074330
	public static bool RPC<P0, P1, P2>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2>(entID, flags, messageName, target, p0, p1, p2);
	}

	// Token: 0x06001FD4 RID: 8148 RVA: 0x0007615C File Offset: 0x0007435C
	public static bool RPC<P0, P1, P2>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2>(entID, flags, messageName, targets, p0, p1, p2);
	}

	// Token: 0x06001FD5 RID: 8149 RVA: 0x00076188 File Offset: 0x00074388
	public static bool RPC<P0, P1, P2>(MonoBehaviour entityScript, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2>(entID, messageName, rpcMode, p0, p1, p2);
	}

	// Token: 0x06001FD6 RID: 8150 RVA: 0x000761B4 File Offset: 0x000743B4
	public static bool RPC<P0, P1, P2>(MonoBehaviour entityScript, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2>(entID, messageName, target, p0, p1, p2);
	}

	// Token: 0x06001FD7 RID: 8151 RVA: 0x000761E0 File Offset: 0x000743E0
	public static bool RPC<P0, P1, P2>(MonoBehaviour entityScript, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2>(entID, messageName, targets, p0, p1, p2);
	}

	// Token: 0x06001FD8 RID: 8152 RVA: 0x0007620C File Offset: 0x0007440C
	public static bool RPC<P0, P1, P2>(Component entityComponent, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2>(entID, flags, messageName, rpcMode, p0, p1, p2);
	}

	// Token: 0x06001FD9 RID: 8153 RVA: 0x00076238 File Offset: 0x00074438
	public static bool RPC<P0, P1, P2>(Component entityComponent, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2>(entID, flags, messageName, target, p0, p1, p2);
	}

	// Token: 0x06001FDA RID: 8154 RVA: 0x00076264 File Offset: 0x00074464
	public static bool RPC<P0, P1, P2>(Component entityComponent, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2>(entID, flags, messageName, targets, p0, p1, p2);
	}

	// Token: 0x06001FDB RID: 8155 RVA: 0x00076290 File Offset: 0x00074490
	public static bool RPC<P0, P1, P2>(Component entityComponent, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2>(entID, messageName, rpcMode, p0, p1, p2);
	}

	// Token: 0x06001FDC RID: 8156 RVA: 0x000762BC File Offset: 0x000744BC
	public static bool RPC<P0, P1, P2>(Component entityComponent, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2>(entID, messageName, target, p0, p1, p2);
	}

	// Token: 0x06001FDD RID: 8157 RVA: 0x000762E8 File Offset: 0x000744E8
	public static bool RPC<P0, P1, P2>(Component entityComponent, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2>(entID, messageName, targets, p0, p1, p2);
	}

	// Token: 0x06001FDE RID: 8158 RVA: 0x00076314 File Offset: 0x00074514
	public static bool RPC<P0, P1, P2, P3>(GameObject entity, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3>(entID, flags, messageName, rpcMode, p0, p1, p2, p3);
	}

	// Token: 0x06001FDF RID: 8159 RVA: 0x00076344 File Offset: 0x00074544
	public static bool RPC<P0, P1, P2, P3>(GameObject entity, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3>(entID, flags, messageName, target, p0, p1, p2, p3);
	}

	// Token: 0x06001FE0 RID: 8160 RVA: 0x00076374 File Offset: 0x00074574
	public static bool RPC<P0, P1, P2, P3>(GameObject entity, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3>(entID, flags, messageName, targets, p0, p1, p2, p3);
	}

	// Token: 0x06001FE1 RID: 8161 RVA: 0x000763A4 File Offset: 0x000745A4
	public static bool RPC<P0, P1, P2, P3>(GameObject entity, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3>(entID, messageName, rpcMode, p0, p1, p2, p3);
	}

	// Token: 0x06001FE2 RID: 8162 RVA: 0x000763D0 File Offset: 0x000745D0
	public static bool RPC<P0, P1, P2, P3>(GameObject entity, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3>(entID, messageName, target, p0, p1, p2, p3);
	}

	// Token: 0x06001FE3 RID: 8163 RVA: 0x000763FC File Offset: 0x000745FC
	public static bool RPC<P0, P1, P2, P3>(GameObject entity, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3>(entID, messageName, targets, p0, p1, p2, p3);
	}

	// Token: 0x06001FE4 RID: 8164 RVA: 0x00076428 File Offset: 0x00074628
	public static bool RPC<P0, P1, P2, P3>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3>(entID, flags, messageName, rpcMode, p0, p1, p2, p3);
	}

	// Token: 0x06001FE5 RID: 8165 RVA: 0x00076458 File Offset: 0x00074658
	public static bool RPC<P0, P1, P2, P3>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3>(entID, flags, messageName, target, p0, p1, p2, p3);
	}

	// Token: 0x06001FE6 RID: 8166 RVA: 0x00076488 File Offset: 0x00074688
	public static bool RPC<P0, P1, P2, P3>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3>(entID, flags, messageName, targets, p0, p1, p2, p3);
	}

	// Token: 0x06001FE7 RID: 8167 RVA: 0x000764B8 File Offset: 0x000746B8
	public static bool RPC<P0, P1, P2, P3>(MonoBehaviour entityScript, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3>(entID, messageName, rpcMode, p0, p1, p2, p3);
	}

	// Token: 0x06001FE8 RID: 8168 RVA: 0x000764E4 File Offset: 0x000746E4
	public static bool RPC<P0, P1, P2, P3>(MonoBehaviour entityScript, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3>(entID, messageName, target, p0, p1, p2, p3);
	}

	// Token: 0x06001FE9 RID: 8169 RVA: 0x00076510 File Offset: 0x00074710
	public static bool RPC<P0, P1, P2, P3>(MonoBehaviour entityScript, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3>(entID, messageName, targets, p0, p1, p2, p3);
	}

	// Token: 0x06001FEA RID: 8170 RVA: 0x0007653C File Offset: 0x0007473C
	public static bool RPC<P0, P1, P2, P3>(Component entityComponent, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3>(entID, flags, messageName, rpcMode, p0, p1, p2, p3);
	}

	// Token: 0x06001FEB RID: 8171 RVA: 0x0007656C File Offset: 0x0007476C
	public static bool RPC<P0, P1, P2, P3>(Component entityComponent, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3>(entID, flags, messageName, target, p0, p1, p2, p3);
	}

	// Token: 0x06001FEC RID: 8172 RVA: 0x0007659C File Offset: 0x0007479C
	public static bool RPC<P0, P1, P2, P3>(Component entityComponent, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3>(entID, flags, messageName, targets, p0, p1, p2, p3);
	}

	// Token: 0x06001FED RID: 8173 RVA: 0x000765CC File Offset: 0x000747CC
	public static bool RPC<P0, P1, P2, P3>(Component entityComponent, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3>(entID, messageName, rpcMode, p0, p1, p2, p3);
	}

	// Token: 0x06001FEE RID: 8174 RVA: 0x000765F8 File Offset: 0x000747F8
	public static bool RPC<P0, P1, P2, P3>(Component entityComponent, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3>(entID, messageName, target, p0, p1, p2, p3);
	}

	// Token: 0x06001FEF RID: 8175 RVA: 0x00076624 File Offset: 0x00074824
	public static bool RPC<P0, P1, P2, P3>(Component entityComponent, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3>(entID, messageName, targets, p0, p1, p2, p3);
	}

	// Token: 0x06001FF0 RID: 8176 RVA: 0x00076650 File Offset: 0x00074850
	public static bool RPC<P0, P1, P2, P3, P4>(GameObject entity, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4);
	}

	// Token: 0x06001FF1 RID: 8177 RVA: 0x00076680 File Offset: 0x00074880
	public static bool RPC<P0, P1, P2, P3, P4>(GameObject entity, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4>(entID, flags, messageName, target, p0, p1, p2, p3, p4);
	}

	// Token: 0x06001FF2 RID: 8178 RVA: 0x000766B0 File Offset: 0x000748B0
	public static bool RPC<P0, P1, P2, P3, P4>(GameObject entity, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4>(entID, flags, messageName, targets, p0, p1, p2, p3, p4);
	}

	// Token: 0x06001FF3 RID: 8179 RVA: 0x000766E0 File Offset: 0x000748E0
	public static bool RPC<P0, P1, P2, P3, P4>(GameObject entity, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4>(entID, messageName, rpcMode, p0, p1, p2, p3, p4);
	}

	// Token: 0x06001FF4 RID: 8180 RVA: 0x00076710 File Offset: 0x00074910
	public static bool RPC<P0, P1, P2, P3, P4>(GameObject entity, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4>(entID, messageName, target, p0, p1, p2, p3, p4);
	}

	// Token: 0x06001FF5 RID: 8181 RVA: 0x00076740 File Offset: 0x00074940
	public static bool RPC<P0, P1, P2, P3, P4>(GameObject entity, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4>(entID, messageName, targets, p0, p1, p2, p3, p4);
	}

	// Token: 0x06001FF6 RID: 8182 RVA: 0x00076770 File Offset: 0x00074970
	public static bool RPC<P0, P1, P2, P3, P4>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4);
	}

	// Token: 0x06001FF7 RID: 8183 RVA: 0x000767A0 File Offset: 0x000749A0
	public static bool RPC<P0, P1, P2, P3, P4>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4>(entID, flags, messageName, target, p0, p1, p2, p3, p4);
	}

	// Token: 0x06001FF8 RID: 8184 RVA: 0x000767D0 File Offset: 0x000749D0
	public static bool RPC<P0, P1, P2, P3, P4>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4>(entID, flags, messageName, targets, p0, p1, p2, p3, p4);
	}

	// Token: 0x06001FF9 RID: 8185 RVA: 0x00076800 File Offset: 0x00074A00
	public static bool RPC<P0, P1, P2, P3, P4>(MonoBehaviour entityScript, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4>(entID, messageName, rpcMode, p0, p1, p2, p3, p4);
	}

	// Token: 0x06001FFA RID: 8186 RVA: 0x00076830 File Offset: 0x00074A30
	public static bool RPC<P0, P1, P2, P3, P4>(MonoBehaviour entityScript, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4>(entID, messageName, target, p0, p1, p2, p3, p4);
	}

	// Token: 0x06001FFB RID: 8187 RVA: 0x00076860 File Offset: 0x00074A60
	public static bool RPC<P0, P1, P2, P3, P4>(MonoBehaviour entityScript, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4>(entID, messageName, targets, p0, p1, p2, p3, p4);
	}

	// Token: 0x06001FFC RID: 8188 RVA: 0x00076890 File Offset: 0x00074A90
	public static bool RPC<P0, P1, P2, P3, P4>(Component entityComponent, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4);
	}

	// Token: 0x06001FFD RID: 8189 RVA: 0x000768C0 File Offset: 0x00074AC0
	public static bool RPC<P0, P1, P2, P3, P4>(Component entityComponent, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4>(entID, flags, messageName, target, p0, p1, p2, p3, p4);
	}

	// Token: 0x06001FFE RID: 8190 RVA: 0x000768F0 File Offset: 0x00074AF0
	public static bool RPC<P0, P1, P2, P3, P4>(Component entityComponent, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4>(entID, flags, messageName, targets, p0, p1, p2, p3, p4);
	}

	// Token: 0x06001FFF RID: 8191 RVA: 0x00076920 File Offset: 0x00074B20
	public static bool RPC<P0, P1, P2, P3, P4>(Component entityComponent, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4>(entID, messageName, rpcMode, p0, p1, p2, p3, p4);
	}

	// Token: 0x06002000 RID: 8192 RVA: 0x00076950 File Offset: 0x00074B50
	public static bool RPC<P0, P1, P2, P3, P4>(Component entityComponent, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4>(entID, messageName, target, p0, p1, p2, p3, p4);
	}

	// Token: 0x06002001 RID: 8193 RVA: 0x00076980 File Offset: 0x00074B80
	public static bool RPC<P0, P1, P2, P3, P4>(Component entityComponent, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4>(entID, messageName, targets, p0, p1, p2, p3, p4);
	}

	// Token: 0x06002002 RID: 8194 RVA: 0x000769B0 File Offset: 0x00074BB0
	public static bool RPC<P0, P1, P2, P3, P4, P5>(GameObject entity, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x06002003 RID: 8195 RVA: 0x000769E4 File Offset: 0x00074BE4
	public static bool RPC<P0, P1, P2, P3, P4, P5>(GameObject entity, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5>(entID, flags, messageName, target, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x06002004 RID: 8196 RVA: 0x00076A18 File Offset: 0x00074C18
	public static bool RPC<P0, P1, P2, P3, P4, P5>(GameObject entity, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5>(entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x06002005 RID: 8197 RVA: 0x00076A4C File Offset: 0x00074C4C
	public static bool RPC<P0, P1, P2, P3, P4, P5>(GameObject entity, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5>(entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x06002006 RID: 8198 RVA: 0x00076A7C File Offset: 0x00074C7C
	public static bool RPC<P0, P1, P2, P3, P4, P5>(GameObject entity, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5>(entID, messageName, target, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x06002007 RID: 8199 RVA: 0x00076AAC File Offset: 0x00074CAC
	public static bool RPC<P0, P1, P2, P3, P4, P5>(GameObject entity, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5>(entID, messageName, targets, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x06002008 RID: 8200 RVA: 0x00076ADC File Offset: 0x00074CDC
	public static bool RPC<P0, P1, P2, P3, P4, P5>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x06002009 RID: 8201 RVA: 0x00076B10 File Offset: 0x00074D10
	public static bool RPC<P0, P1, P2, P3, P4, P5>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5>(entID, flags, messageName, target, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x0600200A RID: 8202 RVA: 0x00076B44 File Offset: 0x00074D44
	public static bool RPC<P0, P1, P2, P3, P4, P5>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5>(entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x0600200B RID: 8203 RVA: 0x00076B78 File Offset: 0x00074D78
	public static bool RPC<P0, P1, P2, P3, P4, P5>(MonoBehaviour entityScript, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5>(entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x0600200C RID: 8204 RVA: 0x00076BA8 File Offset: 0x00074DA8
	public static bool RPC<P0, P1, P2, P3, P4, P5>(MonoBehaviour entityScript, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5>(entID, messageName, target, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x0600200D RID: 8205 RVA: 0x00076BD8 File Offset: 0x00074DD8
	public static bool RPC<P0, P1, P2, P3, P4, P5>(MonoBehaviour entityScript, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5>(entID, messageName, targets, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x0600200E RID: 8206 RVA: 0x00076C08 File Offset: 0x00074E08
	public static bool RPC<P0, P1, P2, P3, P4, P5>(Component entityComponent, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x0600200F RID: 8207 RVA: 0x00076C3C File Offset: 0x00074E3C
	public static bool RPC<P0, P1, P2, P3, P4, P5>(Component entityComponent, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5>(entID, flags, messageName, target, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x06002010 RID: 8208 RVA: 0x00076C70 File Offset: 0x00074E70
	public static bool RPC<P0, P1, P2, P3, P4, P5>(Component entityComponent, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5>(entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x06002011 RID: 8209 RVA: 0x00076CA4 File Offset: 0x00074EA4
	public static bool RPC<P0, P1, P2, P3, P4, P5>(Component entityComponent, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5>(entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x06002012 RID: 8210 RVA: 0x00076CD4 File Offset: 0x00074ED4
	public static bool RPC<P0, P1, P2, P3, P4, P5>(Component entityComponent, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5>(entID, messageName, target, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x06002013 RID: 8211 RVA: 0x00076D04 File Offset: 0x00074F04
	public static bool RPC<P0, P1, P2, P3, P4, P5>(Component entityComponent, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5>(entID, messageName, targets, p0, p1, p2, p3, p4, p5);
	}

	// Token: 0x06002014 RID: 8212 RVA: 0x00076D34 File Offset: 0x00074F34
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(GameObject entity, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x06002015 RID: 8213 RVA: 0x00076D68 File Offset: 0x00074F68
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(GameObject entity, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x06002016 RID: 8214 RVA: 0x00076D9C File Offset: 0x00074F9C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(GameObject entity, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x06002017 RID: 8215 RVA: 0x00076DD0 File Offset: 0x00074FD0
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(GameObject entity, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x06002018 RID: 8216 RVA: 0x00076E04 File Offset: 0x00075004
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(GameObject entity, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(entID, messageName, target, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x06002019 RID: 8217 RVA: 0x00076E38 File Offset: 0x00075038
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(GameObject entity, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x0600201A RID: 8218 RVA: 0x00076E6C File Offset: 0x0007506C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x0600201B RID: 8219 RVA: 0x00076EA0 File Offset: 0x000750A0
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x0600201C RID: 8220 RVA: 0x00076ED4 File Offset: 0x000750D4
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x0600201D RID: 8221 RVA: 0x00076F08 File Offset: 0x00075108
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(MonoBehaviour entityScript, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x0600201E RID: 8222 RVA: 0x00076F3C File Offset: 0x0007513C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(MonoBehaviour entityScript, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(entID, messageName, target, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x0600201F RID: 8223 RVA: 0x00076F70 File Offset: 0x00075170
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(MonoBehaviour entityScript, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x06002020 RID: 8224 RVA: 0x00076FA4 File Offset: 0x000751A4
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(Component entityComponent, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x06002021 RID: 8225 RVA: 0x00076FD8 File Offset: 0x000751D8
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(Component entityComponent, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x06002022 RID: 8226 RVA: 0x0007700C File Offset: 0x0007520C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(Component entityComponent, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x06002023 RID: 8227 RVA: 0x00077040 File Offset: 0x00075240
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(Component entityComponent, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x06002024 RID: 8228 RVA: 0x00077074 File Offset: 0x00075274
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(Component entityComponent, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(entID, messageName, target, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x06002025 RID: 8229 RVA: 0x000770A8 File Offset: 0x000752A8
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6>(Component entityComponent, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6>(entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6);
	}

	// Token: 0x06002026 RID: 8230 RVA: 0x000770DC File Offset: 0x000752DC
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(GameObject entity, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x06002027 RID: 8231 RVA: 0x00077114 File Offset: 0x00075314
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(GameObject entity, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x06002028 RID: 8232 RVA: 0x0007714C File Offset: 0x0007534C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(GameObject entity, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x06002029 RID: 8233 RVA: 0x00077184 File Offset: 0x00075384
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(GameObject entity, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x0600202A RID: 8234 RVA: 0x000771B8 File Offset: 0x000753B8
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(GameObject entity, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(entID, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x0600202B RID: 8235 RVA: 0x000771EC File Offset: 0x000753EC
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(GameObject entity, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x0600202C RID: 8236 RVA: 0x00077220 File Offset: 0x00075420
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x0600202D RID: 8237 RVA: 0x00077258 File Offset: 0x00075458
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x0600202E RID: 8238 RVA: 0x00077290 File Offset: 0x00075490
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x0600202F RID: 8239 RVA: 0x000772C8 File Offset: 0x000754C8
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(MonoBehaviour entityScript, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x06002030 RID: 8240 RVA: 0x000772FC File Offset: 0x000754FC
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(MonoBehaviour entityScript, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(entID, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x06002031 RID: 8241 RVA: 0x00077330 File Offset: 0x00075530
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(MonoBehaviour entityScript, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x06002032 RID: 8242 RVA: 0x00077364 File Offset: 0x00075564
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(Component entityComponent, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x06002033 RID: 8243 RVA: 0x0007739C File Offset: 0x0007559C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(Component entityComponent, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x06002034 RID: 8244 RVA: 0x000773D4 File Offset: 0x000755D4
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(Component entityComponent, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x06002035 RID: 8245 RVA: 0x0007740C File Offset: 0x0007560C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(Component entityComponent, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x06002036 RID: 8246 RVA: 0x00077440 File Offset: 0x00075640
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(Component entityComponent, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(entID, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x06002037 RID: 8247 RVA: 0x00077474 File Offset: 0x00075674
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7>(Component entityComponent, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7);
	}

	// Token: 0x06002038 RID: 8248 RVA: 0x000774A8 File Offset: 0x000756A8
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(GameObject entity, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x06002039 RID: 8249 RVA: 0x000774E0 File Offset: 0x000756E0
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(GameObject entity, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x0600203A RID: 8250 RVA: 0x00077518 File Offset: 0x00075718
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(GameObject entity, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x0600203B RID: 8251 RVA: 0x00077550 File Offset: 0x00075750
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(GameObject entity, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x0600203C RID: 8252 RVA: 0x00077588 File Offset: 0x00075788
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(GameObject entity, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(entID, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x0600203D RID: 8253 RVA: 0x000775C0 File Offset: 0x000757C0
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(GameObject entity, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x0600203E RID: 8254 RVA: 0x000775F8 File Offset: 0x000757F8
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x0600203F RID: 8255 RVA: 0x00077630 File Offset: 0x00075830
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x06002040 RID: 8256 RVA: 0x00077668 File Offset: 0x00075868
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x06002041 RID: 8257 RVA: 0x000776A0 File Offset: 0x000758A0
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(MonoBehaviour entityScript, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x06002042 RID: 8258 RVA: 0x000776D8 File Offset: 0x000758D8
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(MonoBehaviour entityScript, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(entID, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x06002043 RID: 8259 RVA: 0x00077710 File Offset: 0x00075910
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(MonoBehaviour entityScript, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x06002044 RID: 8260 RVA: 0x00077748 File Offset: 0x00075948
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(Component entityComponent, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x06002045 RID: 8261 RVA: 0x00077780 File Offset: 0x00075980
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(Component entityComponent, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x06002046 RID: 8262 RVA: 0x000777B8 File Offset: 0x000759B8
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(Component entityComponent, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x06002047 RID: 8263 RVA: 0x000777F0 File Offset: 0x000759F0
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(Component entityComponent, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x06002048 RID: 8264 RVA: 0x00077828 File Offset: 0x00075A28
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(Component entityComponent, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(entID, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x06002049 RID: 8265 RVA: 0x00077860 File Offset: 0x00075A60
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(Component entityComponent, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	// Token: 0x0600204A RID: 8266 RVA: 0x00077898 File Offset: 0x00075A98
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(GameObject entity, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x0600204B RID: 8267 RVA: 0x000778D4 File Offset: 0x00075AD4
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(GameObject entity, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x0600204C RID: 8268 RVA: 0x00077910 File Offset: 0x00075B10
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(GameObject entity, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x0600204D RID: 8269 RVA: 0x0007794C File Offset: 0x00075B4C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(GameObject entity, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x0600204E RID: 8270 RVA: 0x00077984 File Offset: 0x00075B84
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(GameObject entity, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(entID, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x0600204F RID: 8271 RVA: 0x000779BC File Offset: 0x00075BBC
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(GameObject entity, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x06002050 RID: 8272 RVA: 0x000779F4 File Offset: 0x00075BF4
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x06002051 RID: 8273 RVA: 0x00077A30 File Offset: 0x00075C30
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x06002052 RID: 8274 RVA: 0x00077A6C File Offset: 0x00075C6C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x06002053 RID: 8275 RVA: 0x00077AA8 File Offset: 0x00075CA8
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(MonoBehaviour entityScript, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x06002054 RID: 8276 RVA: 0x00077AE0 File Offset: 0x00075CE0
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(MonoBehaviour entityScript, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(entID, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x06002055 RID: 8277 RVA: 0x00077B18 File Offset: 0x00075D18
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(MonoBehaviour entityScript, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x06002056 RID: 8278 RVA: 0x00077B50 File Offset: 0x00075D50
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(Component entityComponent, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x06002057 RID: 8279 RVA: 0x00077B8C File Offset: 0x00075D8C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(Component entityComponent, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x06002058 RID: 8280 RVA: 0x00077BC8 File Offset: 0x00075DC8
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(Component entityComponent, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x06002059 RID: 8281 RVA: 0x00077C04 File Offset: 0x00075E04
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(Component entityComponent, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x0600205A RID: 8282 RVA: 0x00077C3C File Offset: 0x00075E3C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(Component entityComponent, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(entID, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x0600205B RID: 8283 RVA: 0x00077C74 File Offset: 0x00075E74
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(Component entityComponent, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
	}

	// Token: 0x0600205C RID: 8284 RVA: 0x00077CAC File Offset: 0x00075EAC
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(GameObject entity, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x0600205D RID: 8285 RVA: 0x00077CE8 File Offset: 0x00075EE8
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(GameObject entity, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x0600205E RID: 8286 RVA: 0x00077D24 File Offset: 0x00075F24
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(GameObject entity, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x0600205F RID: 8287 RVA: 0x00077D60 File Offset: 0x00075F60
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(GameObject entity, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x06002060 RID: 8288 RVA: 0x00077D9C File Offset: 0x00075F9C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(GameObject entity, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(entID, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x06002061 RID: 8289 RVA: 0x00077DD8 File Offset: 0x00075FD8
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(GameObject entity, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x06002062 RID: 8290 RVA: 0x00077E14 File Offset: 0x00076014
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x06002063 RID: 8291 RVA: 0x00077E50 File Offset: 0x00076050
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x06002064 RID: 8292 RVA: 0x00077E8C File Offset: 0x0007608C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x06002065 RID: 8293 RVA: 0x00077EC8 File Offset: 0x000760C8
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(MonoBehaviour entityScript, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x06002066 RID: 8294 RVA: 0x00077F04 File Offset: 0x00076104
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(MonoBehaviour entityScript, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(entID, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x06002067 RID: 8295 RVA: 0x00077F40 File Offset: 0x00076140
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(MonoBehaviour entityScript, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x06002068 RID: 8296 RVA: 0x00077F7C File Offset: 0x0007617C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(Component entityComponent, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x06002069 RID: 8297 RVA: 0x00077FB8 File Offset: 0x000761B8
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(Component entityComponent, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x0600206A RID: 8298 RVA: 0x00077FF4 File Offset: 0x000761F4
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(Component entityComponent, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x0600206B RID: 8299 RVA: 0x00078030 File Offset: 0x00076230
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(Component entityComponent, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x0600206C RID: 8300 RVA: 0x0007806C File Offset: 0x0007626C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(Component entityComponent, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(entID, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x0600206D RID: 8301 RVA: 0x000780A8 File Offset: 0x000762A8
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(Component entityComponent, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
	}

	// Token: 0x0600206E RID: 8302 RVA: 0x000780E4 File Offset: 0x000762E4
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(GameObject entity, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x0600206F RID: 8303 RVA: 0x00078124 File Offset: 0x00076324
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(GameObject entity, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x06002070 RID: 8304 RVA: 0x00078164 File Offset: 0x00076364
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(GameObject entity, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x06002071 RID: 8305 RVA: 0x000781A4 File Offset: 0x000763A4
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(GameObject entity, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x06002072 RID: 8306 RVA: 0x000781E0 File Offset: 0x000763E0
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(GameObject entity, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(entID, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x06002073 RID: 8307 RVA: 0x0007821C File Offset: 0x0007641C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(GameObject entity, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entity, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x06002074 RID: 8308 RVA: 0x00078258 File Offset: 0x00076458
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x06002075 RID: 8309 RVA: 0x00078298 File Offset: 0x00076498
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x06002076 RID: 8310 RVA: 0x000782D8 File Offset: 0x000764D8
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(MonoBehaviour entityScript, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x06002077 RID: 8311 RVA: 0x00078318 File Offset: 0x00076518
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(MonoBehaviour entityScript, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x06002078 RID: 8312 RVA: 0x00078354 File Offset: 0x00076554
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(MonoBehaviour entityScript, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(entID, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x06002079 RID: 8313 RVA: 0x00078390 File Offset: 0x00076590
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(MonoBehaviour entityScript, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityScript, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x0600207A RID: 8314 RVA: 0x000783CC File Offset: 0x000765CC
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(Component entityComponent, NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(entID, flags, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x0600207B RID: 8315 RVA: 0x0007840C File Offset: 0x0007660C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(Component entityComponent, NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(entID, flags, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x0600207C RID: 8316 RVA: 0x0007844C File Offset: 0x0007664C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(Component entityComponent, NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(entID, flags, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x0600207D RID: 8317 RVA: 0x0007848C File Offset: 0x0007668C
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(Component entityComponent, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(entID, messageName, rpcMode, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x0600207E RID: 8318 RVA: 0x000784C8 File Offset: 0x000766C8
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(Component entityComponent, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(entID, messageName, target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x0600207F RID: 8319 RVA: 0x00078504 File Offset: 0x00076704
	public static bool RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(Component entityComponent, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		global::NetEntityID entID;
		return (int)global::NetEntityID.Of(entityComponent, out entID) != 0 && global::NetCull.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(entID, messageName, targets, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
	}

	// Token: 0x06002080 RID: 8320 RVA: 0x00078540 File Offset: 0x00076740
	[Conditional("SERVER")]
	public static void VerifyRPC(ref uLink.NetworkMessageInfo info, bool skipOwnerCheck = false)
	{
	}

	// Token: 0x06002081 RID: 8321 RVA: 0x00078544 File Offset: 0x00076744
	private static void FreeViewIDOnly_Destroyer(uLink.NetworkView instance)
	{
	}

	// Token: 0x06002082 RID: 8322 RVA: 0x00078548 File Offset: 0x00076748
	public static void DontDestroyWithNetwork(uLink.NetworkView view)
	{
		if (view)
		{
			view.instantiator.destroyer = global::NetCull.destroyerFreeViewIDOnly;
		}
	}

	// Token: 0x06002083 RID: 8323 RVA: 0x00078568 File Offset: 0x00076768
	public static void DontDestroyWithNetwork(GameObject go)
	{
		if (go)
		{
			global::NetCull.DontDestroyWithNetwork(go.GetComponent<uLinkNetworkView>());
		}
	}

	// Token: 0x06002084 RID: 8324 RVA: 0x00078580 File Offset: 0x00076780
	public static void DontDestroyWithNetwork(MonoBehaviour behaviour)
	{
		if (behaviour)
		{
			global::NetCull.DontDestroyWithNetwork(behaviour.networkView);
		}
	}

	// Token: 0x06002085 RID: 8325 RVA: 0x00078598 File Offset: 0x00076798
	public static void DontDestroyWithNetwork(Component component)
	{
		if (component)
		{
			global::NetCull.DontDestroyWithNetwork(component.GetComponent<uLinkNetworkView>());
		}
	}

	// Token: 0x170007AC RID: 1964
	// (get) Token: 0x06002086 RID: 8326 RVA: 0x000785B0 File Offset: 0x000767B0
	public static bool isClientRunning
	{
		get
		{
			return uLink.Network.isClient;
		}
	}

	// Token: 0x170007AD RID: 1965
	// (get) Token: 0x06002087 RID: 8327 RVA: 0x000785B8 File Offset: 0x000767B8
	public static bool isServerRunning
	{
		get
		{
			return uLink.Network.isServer;
		}
	}

	// Token: 0x170007AE RID: 1966
	// (get) Token: 0x06002088 RID: 8328 RVA: 0x000785C0 File Offset: 0x000767C0
	public static bool isNotRunning
	{
		get
		{
			return !uLink.Network.isClient && !uLink.Network.isServer;
		}
	}

	// Token: 0x170007AF RID: 1967
	// (get) Token: 0x06002089 RID: 8329 RVA: 0x000785D8 File Offset: 0x000767D8
	public static bool isRunning
	{
		get
		{
			return uLink.Network.isClient || uLink.Network.isServer;
		}
	}

	// Token: 0x170007B0 RID: 1968
	// (get) Token: 0x0600208A RID: 8330 RVA: 0x000785EC File Offset: 0x000767EC
	[Obsolete("Use #if CLIENT (unless your trying to check if the client is connected.. then use NetCull.isClientRunning")]
	public static bool isClient
	{
		get
		{
			return global::NetCull.isClientRunning;
		}
	}

	// Token: 0x170007B1 RID: 1969
	// (get) Token: 0x0600208B RID: 8331 RVA: 0x000785F4 File Offset: 0x000767F4
	[Obsolete("Use #if SERVER (unless your trying to check if the server is running.. then use NetCull.isServerRunning")]
	public static bool isServer
	{
		get
		{
			return global::NetCull.isServerRunning;
		}
	}

	// Token: 0x170007B2 RID: 1970
	// (get) Token: 0x0600208C RID: 8332 RVA: 0x000785FC File Offset: 0x000767FC
	public static uLink.NetworkPlayer player
	{
		get
		{
			return uLink.Network.player;
		}
	}

	// Token: 0x170007B3 RID: 1971
	// (get) Token: 0x0600208D RID: 8333 RVA: 0x00078604 File Offset: 0x00076804
	public static double time
	{
		get
		{
			return uLink.Network.time;
		}
	}

	// Token: 0x170007B4 RID: 1972
	// (get) Token: 0x0600208E RID: 8334 RVA: 0x0007860C File Offset: 0x0007680C
	// (set) Token: 0x0600208F RID: 8335 RVA: 0x00078614 File Offset: 0x00076814
	public static float sendRate
	{
		get
		{
			return uLink.Network.sendRate;
		}
		set
		{
			uLink.Network.sendRate = value;
			global::NetCull.Send.Rate = uLink.Network.sendRate;
			global::NetCull.Send.Interval = 1.0 / (double)global::NetCull.Send.Rate;
			global::NetCull.Send.IntervalF = (float)global::NetCull.Send.Interval;
			global::Interpolation.sendRate = global::NetCull.Send.Rate;
		}
	}

	// Token: 0x170007B5 RID: 1973
	// (get) Token: 0x06002090 RID: 8336 RVA: 0x0007865C File Offset: 0x0007685C
	public static double sendInterval
	{
		get
		{
			return global::NetCull.Send.Interval;
		}
	}

	// Token: 0x170007B6 RID: 1974
	// (get) Token: 0x06002091 RID: 8337 RVA: 0x00078664 File Offset: 0x00076864
	public static float sendIntervalF
	{
		get
		{
			return global::NetCull.Send.IntervalF;
		}
	}

	// Token: 0x170007B7 RID: 1975
	// (get) Token: 0x06002092 RID: 8338 RVA: 0x0007866C File Offset: 0x0007686C
	public static ulong timeInMillis
	{
		get
		{
			return uLink.Network.timeInMillis;
		}
	}

	// Token: 0x170007B8 RID: 1976
	// (get) Token: 0x06002093 RID: 8339 RVA: 0x00078674 File Offset: 0x00076874
	public static NetworkConfig config
	{
		get
		{
			return uLink.Network.config;
		}
	}

	// Token: 0x170007B9 RID: 1977
	// (get) Token: 0x06002094 RID: 8340 RVA: 0x0007867C File Offset: 0x0007687C
	public static uLink.NetworkPlayer[] connections
	{
		get
		{
			return uLink.Network.connections;
		}
	}

	// Token: 0x170007BA RID: 1978
	// (get) Token: 0x06002095 RID: 8341 RVA: 0x00078684 File Offset: 0x00076884
	public static NetworkStatus status
	{
		get
		{
			return uLink.Network.status;
		}
	}

	// Token: 0x170007BB RID: 1979
	// (get) Token: 0x06002096 RID: 8342 RVA: 0x0007868C File Offset: 0x0007688C
	public static double localTime
	{
		get
		{
			return uLink.Network.localTime;
		}
	}

	// Token: 0x170007BC RID: 1980
	// (get) Token: 0x06002097 RID: 8343 RVA: 0x00078694 File Offset: 0x00076894
	public static ulong localTimeInMillis
	{
		get
		{
			return uLink.Network.localTimeInMillis;
		}
	}

	// Token: 0x170007BD RID: 1981
	// (get) Token: 0x06002098 RID: 8344 RVA: 0x0007869C File Offset: 0x0007689C
	public static int listenPort
	{
		get
		{
			return uLink.Network.listenPort;
		}
	}

	// Token: 0x170007BE RID: 1982
	// (get) Token: 0x06002099 RID: 8345 RVA: 0x000786A4 File Offset: 0x000768A4
	public static BitStream approvalData
	{
		get
		{
			return uLink.Network.approvalData;
		}
	}

	// Token: 0x170007BF RID: 1983
	// (get) Token: 0x0600209A RID: 8346 RVA: 0x000786AC File Offset: 0x000768AC
	// (set) Token: 0x0600209B RID: 8347 RVA: 0x000786B4 File Offset: 0x000768B4
	public static bool isMessageQueueRunning
	{
		get
		{
			return uLink.Network.isMessageQueueRunning;
		}
		set
		{
			uLink.Network.isMessageQueueRunning = value;
		}
	}

	// Token: 0x170007C0 RID: 1984
	// (get) Token: 0x0600209C RID: 8348 RVA: 0x000786BC File Offset: 0x000768BC
	// (set) Token: 0x0600209D RID: 8349 RVA: 0x000786C8 File Offset: 0x000768C8
	public static global::NetError lastError
	{
		get
		{
			return uLink.Network.lastError.ToNetError();
		}
		set
		{
			uLink.Network.lastError = value._uLink();
		}
	}

	// Token: 0x0600209E RID: 8350 RVA: 0x000786D8 File Offset: 0x000768D8
	public static void CloseConnection(uLink.NetworkPlayer target, bool sendDisconnectionNotification)
	{
		uLink.Network.CloseConnection(target, sendDisconnectionNotification, 3);
	}

	// Token: 0x0600209F RID: 8351 RVA: 0x000786E4 File Offset: 0x000768E4
	public static void ResynchronizeClock(double durationInSeconds)
	{
		uLink.Network.ResynchronizeClock(durationInSeconds);
	}

	// Token: 0x060020A0 RID: 8352 RVA: 0x000786EC File Offset: 0x000768EC
	[Obsolete("void NetCull.ResynchronizeClock(ulong) is deprecated, Bla bla bla don't use this", true)]
	public static void ResynchronizeClock(ulong intervalMillis)
	{
		uLink.Network.ResynchronizeClock(intervalMillis);
	}

	// Token: 0x060020A1 RID: 8353 RVA: 0x000786F4 File Offset: 0x000768F4
	public static global::NetError Connect(string host, int remotePort, string password, params object[] loginData)
	{
		return uLink.Network.Connect(host, remotePort, password, loginData).ToNetError();
	}

	// Token: 0x060020A2 RID: 8354 RVA: 0x00078704 File Offset: 0x00076904
	public static void Disconnect(int timeout)
	{
		uLink.Network.Disconnect(timeout);
	}

	// Token: 0x060020A3 RID: 8355 RVA: 0x0007870C File Offset: 0x0007690C
	public static void Disconnect()
	{
		uLink.Network.Disconnect();
	}

	// Token: 0x060020A4 RID: 8356 RVA: 0x00078714 File Offset: 0x00076914
	public static void DisconnectImmediate()
	{
		uLink.Network.DisconnectImmediate();
	}

	// Token: 0x060020A5 RID: 8357 RVA: 0x0007871C File Offset: 0x0007691C
	public static void RegisterNetAutoPrefab(uLinkNetworkView viewPrefab)
	{
		if (viewPrefab)
		{
			string name = viewPrefab.name;
			try
			{
				global::NetCull.AutoPrefabs.all[name] = viewPrefab;
			}
			catch
			{
				Debug.LogError("skipped duplicate prefab named " + name, viewPrefab);
				return;
			}
			NetworkInstantiator.AddPrefab(viewPrefab.gameObject);
		}
	}

	// Token: 0x060020A6 RID: 8358 RVA: 0x00078790 File Offset: 0x00076990
	public static bool Found(this global::NetCull.PrefabSearch search)
	{
		return (int)search != 0;
	}

	// Token: 0x060020A7 RID: 8359 RVA: 0x0007879C File Offset: 0x0007699C
	public static bool Missing(this global::NetCull.PrefabSearch search)
	{
		return (int)search == 0;
	}

	// Token: 0x060020A8 RID: 8360 RVA: 0x000787A4 File Offset: 0x000769A4
	public static bool IsNGC(this global::NetCull.PrefabSearch search)
	{
		return (int)search == 1;
	}

	// Token: 0x060020A9 RID: 8361 RVA: 0x000787AC File Offset: 0x000769AC
	public static bool IsNet(this global::NetCull.PrefabSearch search)
	{
		return (int)search > 1;
	}

	// Token: 0x060020AA RID: 8362 RVA: 0x000787B4 File Offset: 0x000769B4
	public static bool IsNetMainPrefab(this global::NetCull.PrefabSearch search)
	{
		return (int)search == 2;
	}

	// Token: 0x060020AB RID: 8363 RVA: 0x000787BC File Offset: 0x000769BC
	public static bool IsNetAutoPrefab(this global::NetCull.PrefabSearch search)
	{
		return (int)search == 3;
	}

	// Token: 0x060020AC RID: 8364 RVA: 0x000787C4 File Offset: 0x000769C4
	public static global::NetCull.PrefabSearch LoadPrefab(string prefabName, out GameObject prefab)
	{
		if (string.IsNullOrEmpty(prefabName))
		{
			prefab = null;
			return global::NetCull.PrefabSearch.Missing;
		}
		if (prefabName.StartsWith(":"))
		{
			try
			{
				prefab = global::NetMainPrefab.Lookup<GameObject>(prefabName);
				return (!prefab) ? global::NetCull.PrefabSearch.Missing : global::NetCull.PrefabSearch.NetMain;
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
				prefab = null;
				return global::NetCull.PrefabSearch.Missing;
			}
		}
		if (prefabName.StartsWith(";"))
		{
			try
			{
				global::NGC.Prefab prefab2;
				if (!global::NGC.Prefab.Register.Find(prefabName, out prefab2))
				{
					prefab = null;
					return global::NetCull.PrefabSearch.Missing;
				}
				global::NGCView prefab3 = prefab2.prefab;
				if (prefab3)
				{
					prefab = prefab3.gameObject;
					return (!prefab) ? global::NetCull.PrefabSearch.Missing : global::NetCull.PrefabSearch.NGC;
				}
				prefab = null;
				return global::NetCull.PrefabSearch.Missing;
			}
			catch (Exception ex2)
			{
				Debug.LogException(ex2);
				prefab = null;
				return global::NetCull.PrefabSearch.Missing;
			}
		}
		global::NetCull.PrefabSearch result;
		try
		{
			uLinkNetworkView uLinkNetworkView;
			if (global::NetCull.AutoPrefabs.all.TryGetValue(prefabName, out uLinkNetworkView) && uLinkNetworkView)
			{
				GameObject gameObject;
				prefab = (gameObject = uLinkNetworkView.gameObject);
				if (gameObject)
				{
					return global::NetCull.PrefabSearch.NetAuto;
				}
			}
			prefab = null;
			result = global::NetCull.PrefabSearch.Missing;
		}
		catch (Exception ex3)
		{
			Debug.LogException(ex3);
			prefab = null;
			result = global::NetCull.PrefabSearch.Missing;
		}
		return result;
	}

	// Token: 0x060020AD RID: 8365 RVA: 0x00078968 File Offset: 0x00076B68
	public static GameObject LoadPrefab(string prefabName)
	{
		GameObject result;
		if ((int)global::NetCull.LoadPrefab(prefabName, out result) == 0)
		{
			throw new MissingReferenceException(prefabName);
		}
		return result;
	}

	// Token: 0x060020AE RID: 8366 RVA: 0x0007898C File Offset: 0x00076B8C
	public static global::NetCull.PrefabSearch LoadPrefabScript<T>(string prefabName, out T script) where T : MonoBehaviour
	{
		MonoBehaviour monoBehaviour;
		global::NetCull.PrefabSearch prefabSearch = global::NetCull.LoadPrefabView(prefabName, out monoBehaviour);
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
				prefabSearch = global::NetCull.PrefabSearch.Missing;
			}
		}
		return prefabSearch;
	}

	// Token: 0x060020AF RID: 8367 RVA: 0x000789FC File Offset: 0x00076BFC
	public static T LoadPrefabScript<T>(string prefabName) where T : MonoBehaviour
	{
		T result;
		if ((int)global::NetCull.LoadPrefabScript<T>(prefabName, out result) == 0)
		{
			throw new MissingReferenceException(prefabName);
		}
		return result;
	}

	// Token: 0x060020B0 RID: 8368 RVA: 0x00078A20 File Offset: 0x00076C20
	public static global::NetCull.PrefabSearch LoadPrefabComponent<T>(string prefabName, out T component) where T : Component
	{
		MonoBehaviour monoBehaviour;
		global::NetCull.PrefabSearch prefabSearch = global::NetCull.LoadPrefabView(prefabName, out monoBehaviour);
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
				prefabSearch = global::NetCull.PrefabSearch.Missing;
			}
		}
		return prefabSearch;
	}

	// Token: 0x060020B1 RID: 8369 RVA: 0x00078AAC File Offset: 0x00076CAC
	public static T LoadPrefabComponent<T>(string prefabName) where T : Component
	{
		T result;
		if ((int)global::NetCull.LoadPrefabComponent<T>(prefabName, out result) == 0)
		{
			throw new MissingReferenceException(prefabName);
		}
		return result;
	}

	// Token: 0x060020B2 RID: 8370 RVA: 0x00078AD0 File Offset: 0x00076CD0
	public static global::NetCull.PrefabSearch LoadPrefabView(string prefabName, out MonoBehaviour prefabView)
	{
		if (string.IsNullOrEmpty(prefabName))
		{
			prefabView = null;
			return global::NetCull.PrefabSearch.Missing;
		}
		if (prefabName.StartsWith(":"))
		{
			try
			{
				prefabView = global::NetMainPrefab.Lookup<uLinkNetworkView>(prefabName);
				return (!prefabView) ? global::NetCull.PrefabSearch.Missing : global::NetCull.PrefabSearch.NetMain;
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
				prefabView = null;
				return global::NetCull.PrefabSearch.Missing;
			}
		}
		if (prefabName.StartsWith(";"))
		{
			try
			{
				global::NGC.Prefab prefab;
				if (global::NGC.Prefab.Register.Find(prefabName, out prefab))
				{
					MonoBehaviour prefab2;
					prefabView = (prefab2 = prefab.prefab);
					if (prefab2)
					{
						return global::NetCull.PrefabSearch.NGC;
					}
				}
				prefabView = null;
				return global::NetCull.PrefabSearch.Missing;
			}
			catch (Exception ex2)
			{
				Debug.LogException(ex2);
				prefabView = null;
				return global::NetCull.PrefabSearch.Missing;
			}
		}
		global::NetCull.PrefabSearch result;
		try
		{
			uLinkNetworkView uLinkNetworkView;
			if (!global::NetCull.AutoPrefabs.all.TryGetValue(prefabName, out uLinkNetworkView) || !uLinkNetworkView)
			{
				prefabView = uLinkNetworkView;
				result = global::NetCull.PrefabSearch.Missing;
			}
			else
			{
				prefabView = null;
				result = global::NetCull.PrefabSearch.NetAuto;
			}
		}
		catch (Exception ex3)
		{
			Debug.LogException(ex3);
			prefabView = null;
			result = global::NetCull.PrefabSearch.Missing;
		}
		return result;
	}

	// Token: 0x060020B3 RID: 8371 RVA: 0x00078C40 File Offset: 0x00076E40
	public static MonoBehaviour LoadPrefabView(string prefabName)
	{
		MonoBehaviour result;
		if ((int)global::NetCull.LoadPrefabView(prefabName, out result) == 0)
		{
			throw new MissingReferenceException(prefabName);
		}
		return result;
	}

	// Token: 0x060020B4 RID: 8372 RVA: 0x00078C64 File Offset: 0x00076E64
	private static void OnPreUpdatePreCallbacks()
	{
	}

	// Token: 0x060020B5 RID: 8373 RVA: 0x00078C68 File Offset: 0x00076E68
	private static void OnPreUpdatePostCallbacks()
	{
	}

	// Token: 0x060020B6 RID: 8374 RVA: 0x00078C6C File Offset: 0x00076E6C
	private static void OnPostUpdatePreCallbacks()
	{
	}

	// Token: 0x060020B7 RID: 8375 RVA: 0x00078C70 File Offset: 0x00076E70
	private static void OnPostUpdatePostCallbacks()
	{
		global::Interpolator.SyncronizeAll();
		global::CharacterInterpolatorBase.SyncronizeAll();
	}

	// Token: 0x04000F31 RID: 3889
	public const bool canDestroy = false;

	// Token: 0x04000F32 RID: 3890
	public const bool canRemoveRPCs = false;

	// Token: 0x04000F33 RID: 3891
	private const bool ensureCanDestroy = false;

	// Token: 0x04000F34 RID: 3892
	private const bool ensureCanRemoveRPCS = false;

	// Token: 0x04000F35 RID: 3893
	public const bool kServer = false;

	// Token: 0x04000F36 RID: 3894
	public const bool kClient = true;

	// Token: 0x04000F37 RID: 3895
	private static readonly NetworkInstantiator.Destroyer destroyerFreeViewIDOnly = new NetworkInstantiator.Destroyer(global::NetCull.FreeViewIDOnly_Destroyer);

	// Token: 0x0200039B RID: 923
	public static class Callbacks
	{
		// Token: 0x14000010 RID: 16
		// (add) Token: 0x060020B8 RID: 8376 RVA: 0x00078C7C File Offset: 0x00076E7C
		// (remove) Token: 0x060020B9 RID: 8377 RVA: 0x00078C8C File Offset: 0x00076E8C
		public static event global::NetCull.UpdateFunctor beforeEveryUpdate
		{
			add
			{
				global::NetCull.Callbacks.PRE.DELEGATE.Add(value, false);
			}
			remove
			{
				if (global::NetCull.Callbacks.MADE_PRE)
				{
					global::NetCull.Callbacks.PRE.DELEGATE.Remove(value);
				}
			}
		}

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060020BA RID: 8378 RVA: 0x00078CA4 File Offset: 0x00076EA4
		// (remove) Token: 0x060020BB RID: 8379 RVA: 0x00078CB4 File Offset: 0x00076EB4
		public static event global::NetCull.UpdateFunctor beforeNextUpdate
		{
			add
			{
				global::NetCull.Callbacks.PRE.DELEGATE.Add(value, true);
			}
			remove
			{
				if (global::NetCull.Callbacks.MADE_PRE)
				{
					global::NetCull.Callbacks.PRE.DELEGATE.Remove(value);
				}
			}
		}

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060020BC RID: 8380 RVA: 0x00078CCC File Offset: 0x00076ECC
		// (remove) Token: 0x060020BD RID: 8381 RVA: 0x00078CDC File Offset: 0x00076EDC
		public static event global::NetCull.UpdateFunctor afterEveryUpdate
		{
			add
			{
				global::NetCull.Callbacks.POST.DELEGATE.Add(value, false);
			}
			remove
			{
				if (global::NetCull.Callbacks.MADE_POST)
				{
					global::NetCull.Callbacks.POST.DELEGATE.Remove(value);
				}
			}
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x060020BE RID: 8382 RVA: 0x00078CF4 File Offset: 0x00076EF4
		// (remove) Token: 0x060020BF RID: 8383 RVA: 0x00078D04 File Offset: 0x00076F04
		public static event global::NetCull.UpdateFunctor afterNextUpdate
		{
			add
			{
				global::NetCull.Callbacks.POST.DELEGATE.Add(value, true);
			}
			remove
			{
				if (global::NetCull.Callbacks.MADE_POST)
				{
					global::NetCull.Callbacks.POST.DELEGATE.Remove(value);
				}
			}
		}

		// Token: 0x060020C0 RID: 8384 RVA: 0x00078D1C File Offset: 0x00076F1C
		internal static void FirePreUpdate(NetPreUpdate preUpdate)
		{
			if (preUpdate != global::NetCull.Callbacks.netPreUpdate || !global::NetCull.Callbacks.Updating())
			{
				return;
			}
			global::NetCull.OnPreUpdatePreCallbacks();
			if (global::NetCull.Callbacks.MADE_PRE)
			{
				try
				{
					global::NetCull.Callbacks.PRE.DELEGATE.Invoke();
				}
				catch (Exception ex)
				{
					Debug.LogException(ex, preUpdate);
				}
			}
			global::NetCull.OnPreUpdatePostCallbacks();
		}

		// Token: 0x060020C1 RID: 8385 RVA: 0x00078D90 File Offset: 0x00076F90
		internal static void FirePostUpdate(NetPostUpdate postUpdate)
		{
			if (postUpdate != global::NetCull.Callbacks.netPostUpdate || !global::NetCull.Callbacks.Updating())
			{
				return;
			}
			global::NetCull.OnPostUpdatePreCallbacks();
			if (global::NetCull.Callbacks.MADE_POST)
			{
				try
				{
					global::NetCull.Callbacks.POST.DELEGATE.Invoke();
				}
				catch (Exception ex)
				{
					Debug.LogException(ex, postUpdate);
				}
			}
			global::NetCull.OnPostUpdatePostCallbacks();
		}

		// Token: 0x060020C2 RID: 8386 RVA: 0x00078E04 File Offset: 0x00077004
		private static bool Updating()
		{
			if (!global::NetCull.Callbacks.internalHelper)
			{
				GameObject gameObject = GameObject.Find("uLinkInternalHelper");
				if (!gameObject)
				{
					return false;
				}
				global::NetCull.Callbacks.internalHelper = gameObject.GetComponent<InternalHelper>();
				if (!global::NetCull.Callbacks.internalHelper)
				{
					return false;
				}
			}
			return global::NetCull.Callbacks.internalHelper.enabled;
		}

		// Token: 0x060020C3 RID: 8387 RVA: 0x00078E60 File Offset: 0x00077060
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
				global::NetCull.Callbacks.Resign<T>(ref current, current);
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

		// Token: 0x060020C4 RID: 8388 RVA: 0x00078F44 File Offset: 0x00077144
		private static void Resign<T>(ref T current, T resigning) where T : MonoBehaviour
		{
			if (current == resigning)
			{
				current = (T)((object)null);
			}
		}

		// Token: 0x060020C5 RID: 8389 RVA: 0x00078F70 File Offset: 0x00077170
		internal static void BindUpdater(NetPreUpdate netUpdate)
		{
			global::NetCull.Callbacks.Replace<NetPreUpdate>(ref global::NetCull.Callbacks.netPreUpdate, netUpdate);
		}

		// Token: 0x060020C6 RID: 8390 RVA: 0x00078F80 File Offset: 0x00077180
		internal static void BindUpdater(NetPostUpdate netUpdate)
		{
			global::NetCull.Callbacks.Replace<NetPostUpdate>(ref global::NetCull.Callbacks.netPostUpdate, netUpdate);
		}

		// Token: 0x060020C7 RID: 8391 RVA: 0x00078F90 File Offset: 0x00077190
		internal static void ResignUpdater(NetPreUpdate netUpdate)
		{
			global::NetCull.Callbacks.Resign<NetPreUpdate>(ref global::NetCull.Callbacks.netPreUpdate, netUpdate);
		}

		// Token: 0x060020C8 RID: 8392 RVA: 0x00078FA0 File Offset: 0x000771A0
		internal static void ResignUpdater(NetPostUpdate netUpdate)
		{
			global::NetCull.Callbacks.Resign<NetPostUpdate>(ref global::NetCull.Callbacks.netPostUpdate, netUpdate);
		}

		// Token: 0x04000F38 RID: 3896
		private static bool MADE_PRE;

		// Token: 0x04000F39 RID: 3897
		private static NetPreUpdate netPreUpdate;

		// Token: 0x04000F3A RID: 3898
		private static bool MADE_POST;

		// Token: 0x04000F3B RID: 3899
		private static NetPostUpdate netPostUpdate;

		// Token: 0x04000F3C RID: 3900
		private static InternalHelper internalHelper;

		// Token: 0x0200039C RID: 924
		private class UpdateDelegate
		{
			// Token: 0x060020CA RID: 8394 RVA: 0x00079008 File Offset: 0x00077208
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
					HashSet<global::NetCull.UpdateFunctor> hashSet = (!this.onceSwap) ? this.once1 : this.once2;
					HashSet<global::NetCull.UpdateFunctor> hashSet2 = (!this.onceSwap) ? this.once2 : this.once1;
					hashSet2.Clear();
					hashSet2.UnionWith(hashSet);
					this.onceSwap = !this.onceSwap;
					foreach (global::NetCull.UpdateFunctor item in hashSet)
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
							global::NetCull.UpdateFunctor updateFunctor = this.invokation[this.iterPosition];
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

			// Token: 0x060020CB RID: 8395 RVA: 0x00079230 File Offset: 0x00077430
			private bool HandleRemoval(global::NetCull.UpdateFunctor functor)
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

			// Token: 0x060020CC RID: 8396 RVA: 0x00079288 File Offset: 0x00077488
			public bool Remove(global::NetCull.UpdateFunctor functor)
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

			// Token: 0x060020CD RID: 8397 RVA: 0x00079310 File Offset: 0x00077510
			public bool Add(global::NetCull.UpdateFunctor functor, bool oneTimeOnly)
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

			// Token: 0x04000F3D RID: 3901
			private readonly HashSet<global::NetCull.UpdateFunctor> hashSet = new HashSet<global::NetCull.UpdateFunctor>();

			// Token: 0x04000F3E RID: 3902
			private readonly List<global::NetCull.UpdateFunctor> list = new List<global::NetCull.UpdateFunctor>();

			// Token: 0x04000F3F RID: 3903
			private readonly List<global::NetCull.UpdateFunctor> invokation = new List<global::NetCull.UpdateFunctor>();

			// Token: 0x04000F40 RID: 3904
			private readonly HashSet<global::NetCull.UpdateFunctor> once1 = new HashSet<global::NetCull.UpdateFunctor>();

			// Token: 0x04000F41 RID: 3905
			private readonly HashSet<global::NetCull.UpdateFunctor> once2 = new HashSet<global::NetCull.UpdateFunctor>();

			// Token: 0x04000F42 RID: 3906
			private readonly HashSet<int> skip = new HashSet<int>();

			// Token: 0x04000F43 RID: 3907
			private int count;

			// Token: 0x04000F44 RID: 3908
			private int iterPosition;

			// Token: 0x04000F45 RID: 3909
			private bool guarded;

			// Token: 0x04000F46 RID: 3910
			private bool onceSwap;
		}

		// Token: 0x0200039D RID: 925
		private static class PRE
		{
			// Token: 0x060020CE RID: 8398 RVA: 0x00079368 File Offset: 0x00077568
			static PRE()
			{
				global::NetCull.Callbacks.MADE_PRE = true;
			}

			// Token: 0x04000F47 RID: 3911
			public static readonly global::NetCull.Callbacks.UpdateDelegate DELEGATE = new global::NetCull.Callbacks.UpdateDelegate();
		}

		// Token: 0x0200039E RID: 926
		private static class POST
		{
			// Token: 0x060020CF RID: 8399 RVA: 0x0007937C File Offset: 0x0007757C
			static POST()
			{
				global::NetCull.Callbacks.MADE_POST = true;
			}

			// Token: 0x04000F48 RID: 3912
			public static readonly global::NetCull.Callbacks.UpdateDelegate DELEGATE = new global::NetCull.Callbacks.UpdateDelegate();
		}
	}

	// Token: 0x0200039F RID: 927
	[Serializable]
	public abstract class RPCVerificationException : Exception
	{
		// Token: 0x060020D0 RID: 8400 RVA: 0x00079390 File Offset: 0x00077590
		internal RPCVerificationException()
		{
		}
	}

	// Token: 0x020003A0 RID: 928
	[Serializable]
	public class RPCVerificationDropException : global::NetCull.RPCVerificationException
	{
		// Token: 0x060020D1 RID: 8401 RVA: 0x00079398 File Offset: 0x00077598
		internal RPCVerificationDropException()
		{
		}
	}

	// Token: 0x020003A1 RID: 929
	[Serializable]
	public class RPCVerificationLateException : global::NetCull.RPCVerificationDropException
	{
		// Token: 0x060020D2 RID: 8402 RVA: 0x000793A0 File Offset: 0x000775A0
		internal RPCVerificationLateException()
		{
		}
	}

	// Token: 0x020003A2 RID: 930
	[Serializable]
	public class RPCVerificationSenderException : global::NetCull.RPCVerificationException
	{
		// Token: 0x060020D3 RID: 8403 RVA: 0x000793A8 File Offset: 0x000775A8
		internal RPCVerificationSenderException(uLink.NetworkPlayer Sender)
		{
			this.Sender = Sender;
		}

		// Token: 0x04000F49 RID: 3913
		public readonly uLink.NetworkPlayer Sender;
	}

	// Token: 0x020003A3 RID: 931
	[Serializable]
	public class RPCVerificationWrongSenderException : global::NetCull.RPCVerificationSenderException
	{
		// Token: 0x060020D4 RID: 8404 RVA: 0x000793B8 File Offset: 0x000775B8
		internal RPCVerificationWrongSenderException(uLink.NetworkPlayer Sender, uLink.NetworkPlayer Owner) : base(Sender)
		{
			this.Owner = Owner;
		}

		// Token: 0x04000F4A RID: 3914
		public readonly uLink.NetworkPlayer Owner;
	}

	// Token: 0x020003A4 RID: 932
	private static class Send
	{
		// Token: 0x04000F4B RID: 3915
		public static float Rate = uLink.Network.sendRate;

		// Token: 0x04000F4C RID: 3916
		public static double Interval = 1.0 / (double)global::NetCull.sendRate;

		// Token: 0x04000F4D RID: 3917
		public static float IntervalF = (float)global::NetCull.Send.Interval;
	}

	// Token: 0x020003A5 RID: 933
	private static class AutoPrefabs
	{
		// Token: 0x04000F4E RID: 3918
		public static Dictionary<string, uLinkNetworkView> all = new Dictionary<string, uLinkNetworkView>();
	}

	// Token: 0x020003A6 RID: 934
	public enum PrefabSearch : sbyte
	{
		// Token: 0x04000F50 RID: 3920
		Missing,
		// Token: 0x04000F51 RID: 3921
		NGC,
		// Token: 0x04000F52 RID: 3922
		NetMain,
		// Token: 0x04000F53 RID: 3923
		NetAuto
	}

	// Token: 0x020003A7 RID: 935
	// (Invoke) Token: 0x060020D8 RID: 8408
	public delegate void UpdateFunctor();
}
