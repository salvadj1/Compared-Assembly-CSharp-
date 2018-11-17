using System;
using Facepunch.Actor;
using UnityEngine;

// Token: 0x0200011C RID: 284
public class CharacterActorRigTrait : global::CharacterTrait
{
	// Token: 0x17000176 RID: 374
	// (get) Token: 0x06000757 RID: 1879 RVA: 0x00021164 File Offset: 0x0001F364
	public ActorRig actorRig
	{
		get
		{
			return this._actorRig;
		}
	}

	// Token: 0x040005B2 RID: 1458
	[SerializeField]
	private ActorRig _actorRig;
}
