using System;
using UnityEngine;

// Token: 0x020000DC RID: 220
[AddComponentMenu("")]
public class ServerSaveManager : MonoBehaviour
{
	// Token: 0x0400041E RID: 1054
	[SerializeField]
	[HideInInspector]
	private int nextID = 1;

	// Token: 0x0400041F RID: 1055
	[HideInInspector]
	[SerializeField]
	private int[] keys;

	// Token: 0x04000420 RID: 1056
	[SerializeField]
	[HideInInspector]
	private global::ServerSave[] values;
}
