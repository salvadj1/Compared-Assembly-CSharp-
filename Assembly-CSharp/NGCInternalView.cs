using System;
using System.Collections.Generic;
using uLink;
using UnityEngine;

// Token: 0x020003A8 RID: 936
[AddComponentMenu("")]
internal sealed class NGCInternalView : uLinkNetworkView
{
	// Token: 0x060020DC RID: 8412 RVA: 0x00079414 File Offset: 0x00077614
	internal global::NGC GetNGC()
	{
		return this.ngc;
	}

	// Token: 0x060020DD RID: 8413 RVA: 0x0007941C File Offset: 0x0007761C
	private void Awake()
	{
		this.ngc = base.GetComponent<global::NGC>();
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

	// Token: 0x060020DE RID: 8414 RVA: 0x000794BC File Offset: 0x000776BC
	protected override bool OnRPC(string rpcName, BitStream stream, uLink.NetworkMessageInfo info)
	{
		char c = rpcName[0];
		string text;
		if (!NGCInternalView.Hack.actionToRPCName.TryGetValue(c, out text))
		{
			text = (NGCInternalView.Hack.actionToRPCName[c] = "NGC:" + c);
		}
		return base.OnRPC(text, stream, info);
	}

	// Token: 0x04000F54 RID: 3924
	[NonSerialized]
	private global::NGC ngc;

	// Token: 0x020003A9 RID: 937
	private static class Hack
	{
		// Token: 0x04000F55 RID: 3925
		public static Dictionary<char, string> actionToRPCName = new Dictionary<char, string>();
	}
}
