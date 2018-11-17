using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000DA RID: 218
public class ServerSave : MonoBehaviour
{
	// Token: 0x170000B0 RID: 176
	// (get) Token: 0x060004A1 RID: 1185 RVA: 0x00016FF4 File Offset: 0x000151F4
	internal global::ServerSave.Reged REGED
	{
		get
		{
			return this.registered;
		}
	}

	// Token: 0x04000417 RID: 1047
	private static Dictionary<int, string> StructureDictionary;

	// Token: 0x04000418 RID: 1048
	[SerializeField]
	private bool autoNetSerialize = true;

	// Token: 0x04000419 RID: 1049
	[NonSerialized]
	private global::ServerSave.Reged registered;

	// Token: 0x020000DB RID: 219
	internal enum Reged : sbyte
	{
		// Token: 0x0400041B RID: 1051
		None,
		// Token: 0x0400041C RID: 1052
		ToNet,
		// Token: 0x0400041D RID: 1053
		ToNGC
	}
}
