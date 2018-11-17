using System;
using UnityEngine;

// Token: 0x020001BB RID: 443
public class TestFlagsScript : MonoBehaviour
{
	// Token: 0x0400086D RID: 2157
	public global::TestFlagsScript.E1 flags;

	// Token: 0x020001BC RID: 444
	[Flags]
	public enum E1
	{
		// Token: 0x0400086F RID: 2159
		bit1 = 1,
		// Token: 0x04000870 RID: 2160
		bit3 = 4,
		// Token: 0x04000871 RID: 2161
		bit5 = 16
	}
}
