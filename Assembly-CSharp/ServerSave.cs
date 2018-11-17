using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000C6 RID: 198
public class ServerSave : MonoBehaviour
{
	// Token: 0x17000096 RID: 150
	// (get) Token: 0x06000423 RID: 1059 RVA: 0x0001562C File Offset: 0x0001382C
	internal ServerSave.Reged REGED
	{
		get
		{
			return this.registered;
		}
	}

	// Token: 0x040003A8 RID: 936
	private static Dictionary<int, string> StructureDictionary;

	// Token: 0x040003A9 RID: 937
	[SerializeField]
	private bool autoNetSerialize = true;

	// Token: 0x040003AA RID: 938
	[NonSerialized]
	private ServerSave.Reged registered;

	// Token: 0x020000C7 RID: 199
	internal enum Reged : sbyte
	{
		// Token: 0x040003AC RID: 940
		None,
		// Token: 0x040003AD RID: 941
		ToNet,
		// Token: 0x040003AE RID: 942
		ToNGC
	}
}
