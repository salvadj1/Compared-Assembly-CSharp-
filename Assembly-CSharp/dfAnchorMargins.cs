using System;
using UnityEngine;

// Token: 0x02000781 RID: 1921
[Serializable]
public class dfAnchorMargins
{
	// Token: 0x06004066 RID: 16486 RVA: 0x000EC564 File Offset: 0x000EA764
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

	// Token: 0x040021D3 RID: 8659
	[SerializeField]
	public float left;

	// Token: 0x040021D4 RID: 8660
	[SerializeField]
	public float top;

	// Token: 0x040021D5 RID: 8661
	[SerializeField]
	public float right;

	// Token: 0x040021D6 RID: 8662
	[SerializeField]
	public float bottom;
}
