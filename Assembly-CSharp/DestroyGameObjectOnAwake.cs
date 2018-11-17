using System;
using UnityEngine;

// Token: 0x02000550 RID: 1360
public class DestroyGameObjectOnAwake : MonoBehaviour
{
	// Token: 0x06002D6D RID: 11629 RVA: 0x000AB518 File Offset: 0x000A9718
	private void Awake()
	{
		Object.Destroy(base.gameObject);
	}
}
