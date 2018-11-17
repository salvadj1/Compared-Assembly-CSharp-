using System;
using UnityEngine;

// Token: 0x02000319 RID: 793
[AddComponentMenu("")]
public sealed class RigidObjServerCollision : MonoBehaviour
{
	// Token: 0x06001E84 RID: 7812 RVA: 0x00077C08 File Offset: 0x00075E08
	private void OnCollisionEnter(Collision collision)
	{
		if (this.rigidObj)
		{
			this.rigidObj.OnServerCollision(0, collision);
		}
	}

	// Token: 0x06001E85 RID: 7813 RVA: 0x00077C28 File Offset: 0x00075E28
	private void OnCollisionExit(Collision collision)
	{
		if (this.rigidObj)
		{
			this.rigidObj.OnServerCollision(1, collision);
		}
	}

	// Token: 0x06001E86 RID: 7814 RVA: 0x00077C48 File Offset: 0x00075E48
	private void OnCollisionStay(Collision collision)
	{
		if (this.rigidObj)
		{
			this.rigidObj.OnServerCollision(2, collision);
		}
	}

	// Token: 0x04000EC0 RID: 3776
	public const byte Enter = 0;

	// Token: 0x04000EC1 RID: 3777
	public const byte Exit = 1;

	// Token: 0x04000EC2 RID: 3778
	public const byte Stay = 2;

	// Token: 0x04000EC3 RID: 3779
	[NonSerialized]
	public RigidObj rigidObj;
}
