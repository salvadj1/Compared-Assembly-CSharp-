using System;
using uLink;

// Token: 0x020004E4 RID: 1252
public class NetCheck
{
	// Token: 0x06002ADF RID: 10975 RVA: 0x000AB370 File Offset: 0x000A9570
	public static bool PlayerValid(NetworkPlayer ply)
	{
		return ply.isConnected;
	}
}
