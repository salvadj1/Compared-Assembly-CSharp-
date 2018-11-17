using System;
using UnityEngine;

// Token: 0x020008B0 RID: 2224
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Anchor (Bordered)")]
public class UIBorderedAnchor : global::UIAnchor
{
	// Token: 0x06004C13 RID: 19475 RVA: 0x00129A70 File Offset: 0x00127C70
	protected new void Update()
	{
		if (this.uiCamera)
		{
			Vector3 vector = global::UIAnchor.WorldOrigin(this.uiCamera, this.side, this.screenPixelBorder, this.depthOffset, this.relativeOffset.x, this.relativeOffset.y, this.halfPixelOffset);
			base.SetPosition(ref vector);
		}
	}

	// Token: 0x040029C6 RID: 10694
	public RectOffset screenPixelBorder;
}
