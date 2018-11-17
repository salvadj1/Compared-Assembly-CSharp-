using System;
using UnityEngine;

// Token: 0x0200033D RID: 829
[AddComponentMenu("")]
public sealed class SocketProxy : Socket.Proxy
{
	// Token: 0x06001F96 RID: 8086 RVA: 0x0007C624 File Offset: 0x0007A824
	protected override void UninitializeProxy()
	{
		base.transform.DetachChildren();
	}
}
