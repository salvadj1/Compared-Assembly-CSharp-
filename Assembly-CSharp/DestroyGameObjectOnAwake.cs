using System;
using UnityEngine;

// Token: 0x02000495 RID: 1173
public class DestroyGameObjectOnAwake : MonoBehaviour
{
	// Token: 0x060029BB RID: 10683 RVA: 0x000A3780 File Offset: 0x000A1980
	private void Awake()
	{
		Object.Destroy(base.gameObject);
	}
}
