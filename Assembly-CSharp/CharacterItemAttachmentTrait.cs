using System;
using UnityEngine;

// Token: 0x02000511 RID: 1297
public class CharacterItemAttachmentTrait : global::CharacterTrait
{
	// Token: 0x170009A2 RID: 2466
	// (get) Token: 0x06002C35 RID: 11317 RVA: 0x000A5A8C File Offset: 0x000A3C8C
	public global::Socket.ConfigBodyPart socket
	{
		get
		{
			return this._socket;
		}
	}

	// Token: 0x0400161F RID: 5663
	[SerializeField]
	private global::Socket.ConfigBodyPart _socket;
}
