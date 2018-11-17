using System;
using UnityEngine;

// Token: 0x0200045B RID: 1115
public class CharacterItemAttachmentTrait : CharacterTrait
{
	// Token: 0x1700093A RID: 2362
	// (get) Token: 0x060028A5 RID: 10405 RVA: 0x0009FB0C File Offset: 0x0009DD0C
	public Socket.ConfigBodyPart socket
	{
		get
		{
			return this._socket;
		}
	}

	// Token: 0x0400149C RID: 5276
	[SerializeField]
	private Socket.ConfigBodyPart _socket;
}
