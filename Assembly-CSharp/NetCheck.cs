using System;
using uLink;

// Token: 0x0200059F RID: 1439
public class NetCheck
{
	// Token: 0x06002E91 RID: 11921 RVA: 0x000B3108 File Offset: 0x000B1308
	public static bool PlayerValid(uLink.NetworkPlayer ply)
	{
		return ply.isConnected;
	}
}
