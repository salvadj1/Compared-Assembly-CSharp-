using System;
using UnityEngine;

// Token: 0x020000C8 RID: 200
[AddComponentMenu("")]
public class ServerSaveManager : MonoBehaviour
{
	// Token: 0x040003AF RID: 943
	[SerializeField]
	[HideInInspector]
	private int nextID = 1;

	// Token: 0x040003B0 RID: 944
	[HideInInspector]
	[SerializeField]
	private int[] keys;

	// Token: 0x040003B1 RID: 945
	[SerializeField]
	[HideInInspector]
	private ServerSave[] values;
}
