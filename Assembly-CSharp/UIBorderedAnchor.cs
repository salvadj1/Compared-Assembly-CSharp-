using System;
using UnityEngine;

// Token: 0x020007C3 RID: 1987
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Anchor (Bordered)")]
public class UIBorderedAnchor : UIAnchor
{
	// Token: 0x06004784 RID: 18308 RVA: 0x0012004C File Offset: 0x0011E24C
	protected new void Update()
	{
		if (this.uiCamera)
		{
			Vector3 vector = UIAnchor.WorldOrigin(this.uiCamera, this.side, this.screenPixelBorder, this.depthOffset, this.relativeOffset.x, this.relativeOffset.y, this.halfPixelOffset);
			base.SetPosition(ref vector);
		}
	}

	// Token: 0x0400278C RID: 10124
	public RectOffset screenPixelBorder;
}
