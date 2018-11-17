using System;
using UnityEngine;

// Token: 0x020000FE RID: 254
public class CharacterAnimationTrait : CharacterTrait
{
	// Token: 0x17000149 RID: 329
	// (get) Token: 0x06000687 RID: 1671 RVA: 0x0001E5AC File Offset: 0x0001C7AC
	public MovementAnimationSetup movementAnimationSetup
	{
		get
		{
			return this._movementAnimationSetup;
		}
	}

	// Token: 0x1700014A RID: 330
	// (get) Token: 0x06000688 RID: 1672 RVA: 0x0001E5B4 File Offset: 0x0001C7B4
	public string defaultGroupName
	{
		get
		{
			return this._defaultGroupName;
		}
	}

	// Token: 0x040004E8 RID: 1256
	[SerializeField]
	private MovementAnimationSetup _movementAnimationSetup;

	// Token: 0x040004E9 RID: 1257
	[SerializeField]
	private string _defaultGroupName = "noitem";
}
