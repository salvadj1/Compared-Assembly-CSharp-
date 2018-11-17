using System;
using UnityEngine;

// Token: 0x0200005A RID: 90
public class ClientStreamLevelLoader : MonoBehaviour
{
	// Token: 0x060002EF RID: 751 RVA: 0x0000F4F0 File Offset: 0x0000D6F0
	private void Start()
	{
		global::RustLoader rustLoader = (global::RustLoader)Object.Instantiate(this.loaderPrefab);
		base.enabled = false;
	}

	// Token: 0x04000204 RID: 516
	[SerializeField]
	private global::RustLoader loaderPrefab;
}
