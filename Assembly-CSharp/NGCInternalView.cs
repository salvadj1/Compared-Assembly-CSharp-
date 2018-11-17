using System;
using System.Collections.Generic;
using uLink;
using UnityEngine;

// Token: 0x02000300 RID: 768
[AddComponentMenu("")]
internal sealed class NGCInternalView : uLinkNetworkView
{
	// Token: 0x06001D9E RID: 7582 RVA: 0x00074994 File Offset: 0x00072B94
	internal NGC GetNGC()
	{
		return this.ngc;
	}

	// Token: 0x06001D9F RID: 7583 RVA: 0x0007499C File Offset: 0x00072B9C
	private void Awake()
	{
		this.ngc = base.GetComponent<NGC>();
		this.ngc.networkView = this;
		try
		{
			this.observed = this.ngc;
			this.rpcReceiver = 1;
			this.stateSynchronization = 0;
			this.securable = 0;
		}
		finally
		{
			try
			{
				base.Awake();
			}
			finally
			{
				this.ngc.networkViewID = base.viewID;
			}
		}
	}

	// Token: 0x06001DA0 RID: 7584 RVA: 0x00074A3C File Offset: 0x00072C3C
	protected override bool OnRPC(string rpcName, BitStream stream, NetworkMessageInfo info)
	{
		char c = rpcName[0];
		string text;
		if (!NGCInternalView.Hack.actionToRPCName.TryGetValue(c, out text))
		{
			text = (NGCInternalView.Hack.actionToRPCName[c] = "NGC:" + c);
		}
		return base.OnRPC(text, stream, info);
	}

	// Token: 0x04000E14 RID: 3604
	[NonSerialized]
	private NGC ngc;

	// Token: 0x02000301 RID: 769
	private static class Hack
	{
		// Token: 0x04000E15 RID: 3605
		public static Dictionary<char, string> actionToRPCName = new Dictionary<char, string>();
	}
}
