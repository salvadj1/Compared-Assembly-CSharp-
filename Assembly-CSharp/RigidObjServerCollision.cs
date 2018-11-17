using System;
using UnityEngine;

// Token: 0x020003C2 RID: 962
[AddComponentMenu("")]
public sealed class RigidObjServerCollision : MonoBehaviour
{
	// Token: 0x060021C6 RID: 8646 RVA: 0x0007C688 File Offset: 0x0007A888
	private void OnCollisionEnter(Collision collision)
	{
		if (this.rigidObj)
		{
			this.rigidObj.OnServerCollision(0, collision);
		}
	}

	// Token: 0x060021C7 RID: 8647 RVA: 0x0007C6A8 File Offset: 0x0007A8A8
	private void OnCollisionExit(Collision collision)
	{
		if (this.rigidObj)
		{
			this.rigidObj.OnServerCollision(1, collision);
		}
	}

	// Token: 0x060021C8 RID: 8648 RVA: 0x0007C6C8 File Offset: 0x0007A8C8
	private void OnCollisionStay(Collision collision)
	{
		if (this.rigidObj)
		{
			this.rigidObj.OnServerCollision(2, collision);
		}
	}

	// Token: 0x04001000 RID: 4096
	public const byte Enter = 0;

	// Token: 0x04001001 RID: 4097
	public const byte Exit = 1;

	// Token: 0x04001002 RID: 4098
	public const byte Stay = 2;

	// Token: 0x04001003 RID: 4099
	[NonSerialized]
	public global::RigidObj rigidObj;
}
