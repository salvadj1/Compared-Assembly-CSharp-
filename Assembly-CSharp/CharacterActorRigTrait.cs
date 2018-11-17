using System;
using Facepunch.Actor;
using UnityEngine;

// Token: 0x020000FD RID: 253
public class CharacterActorRigTrait : CharacterTrait
{
	// Token: 0x17000148 RID: 328
	// (get) Token: 0x06000685 RID: 1669 RVA: 0x0001E590 File Offset: 0x0001C790
	public ActorRig actorRig
	{
		get
		{
			return this._actorRig;
		}
	}

	// Token: 0x040004E7 RID: 1255
	[SerializeField]
	private ActorRig _actorRig;
}
