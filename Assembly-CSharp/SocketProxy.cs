using System;
using UnityEngine;

// Token: 0x020003EA RID: 1002
[AddComponentMenu("")]
public sealed class SocketProxy : global::Socket.Proxy
{
	// Token: 0x060022F8 RID: 8952 RVA: 0x00081A20 File Offset: 0x0007FC20
	protected override void UninitializeProxy()
	{
		base.transform.DetachChildren();
	}
}
