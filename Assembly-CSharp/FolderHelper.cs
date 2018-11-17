using System;
using UnityEngine;

// Token: 0x020004DA RID: 1242
public class FolderHelper : MonoBehaviour
{
	// Token: 0x06002AC1 RID: 10945 RVA: 0x000AAC40 File Offset: 0x000A8E40
	private void Awake()
	{
		base.transform.DetachChildren();
		Object.Destroy(base.gameObject);
	}
}
