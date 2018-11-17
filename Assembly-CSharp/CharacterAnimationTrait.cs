using System;
using UnityEngine;

// Token: 0x0200011D RID: 285
public class CharacterAnimationTrait : global::CharacterTrait
{
	// Token: 0x17000177 RID: 375
	// (get) Token: 0x06000759 RID: 1881 RVA: 0x00021180 File Offset: 0x0001F380
	public MovementAnimationSetup movementAnimationSetup
	{
		get
		{
			return this._movementAnimationSetup;
		}
	}

	// Token: 0x17000178 RID: 376
	// (get) Token: 0x0600075A RID: 1882 RVA: 0x00021188 File Offset: 0x0001F388
	public string defaultGroupName
	{
		get
		{
			return this._defaultGroupName;
		}
	}

	// Token: 0x040005B3 RID: 1459
	[SerializeField]
	private MovementAnimationSetup _movementAnimationSetup;

	// Token: 0x040005B4 RID: 1460
	[SerializeField]
	private string _defaultGroupName = "noitem";
}
