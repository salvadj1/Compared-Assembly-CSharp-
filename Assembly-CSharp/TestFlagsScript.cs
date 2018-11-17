using System;
using UnityEngine;

// Token: 0x0200018F RID: 399
public class TestFlagsScript : MonoBehaviour
{
	// Token: 0x04000759 RID: 1881
	public TestFlagsScript.E1 flags;

	// Token: 0x02000190 RID: 400
	[Flags]
	public enum E1
	{
		// Token: 0x0400075B RID: 1883
		bit1 = 1,
		// Token: 0x0400075C RID: 1884
		bit3 = 4,
		// Token: 0x0400075D RID: 1885
		bit5 = 16
	}
}
