using System;
using UnityEngine;

// Token: 0x02000048 RID: 72
public class ClientStreamLevelLoader : MonoBehaviour
{
	// Token: 0x0600027D RID: 637 RVA: 0x0000DF48 File Offset: 0x0000C148
	private void Start()
	{
		RustLoader rustLoader = (RustLoader)Object.Instantiate(this.loaderPrefab);
		base.enabled = false;
	}

	// Token: 0x040001A2 RID: 418
	[SerializeField]
	private RustLoader loaderPrefab;
}
