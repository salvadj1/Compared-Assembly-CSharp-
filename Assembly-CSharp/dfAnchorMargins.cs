using System;
using UnityEngine;

// Token: 0x020006B6 RID: 1718
[Serializable]
public class dfAnchorMargins
{
	// Token: 0x06003C5C RID: 15452 RVA: 0x000E3A20 File Offset: 0x000E1C20
	public override string ToString()
	{
		return string.Format("[L:{0},T:{1},R:{2},B:{3}]", new object[]
		{
			this.left,
			this.top,
			this.right,
			this.bottom
		});
	}

	// Token: 0x04001FD2 RID: 8146
	[SerializeField]
	public float left;

	// Token: 0x04001FD3 RID: 8147
	[SerializeField]
	public float top;

	// Token: 0x04001FD4 RID: 8148
	[SerializeField]
	public float right;

	// Token: 0x04001FD5 RID: 8149
	[SerializeField]
	public float bottom;
}
