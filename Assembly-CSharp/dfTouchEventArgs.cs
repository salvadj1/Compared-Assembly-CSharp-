using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020006C2 RID: 1730
public class dfTouchEventArgs : dfMouseEventArgs
{
	// Token: 0x06003C99 RID: 15513 RVA: 0x000E4110 File Offset: 0x000E2310
	public dfTouchEventArgs(dfControl Source, Touch touch, Ray ray) : base(Source, dfMouseButtons.Left, touch.tapCount, ray, touch.position, 0f)
	{
		this.Touch = touch;
		this.Touches = new List<Touch>
		{
			touch
		};
		if (touch.deltaTime > 1.401298E-45f)
		{
			base.MoveDelta = touch.deltaPosition * (Time.deltaTime / touch.deltaTime);
		}
	}

	// Token: 0x06003C9A RID: 15514 RVA: 0x000E4184 File Offset: 0x000E2384
	public dfTouchEventArgs(dfControl source, List<Touch> touches, Ray ray) : this(source, touches.First<Touch>(), ray)
	{
		this.Touches = touches;
	}

	// Token: 0x06003C9B RID: 15515 RVA: 0x000E419C File Offset: 0x000E239C
	public dfTouchEventArgs(dfControl Source) : base(Source)
	{
		base.Position = Vector2.zero;
	}

	// Token: 0x17000BC2 RID: 3010
	// (get) Token: 0x06003C9C RID: 15516 RVA: 0x000E41B0 File Offset: 0x000E23B0
	// (set) Token: 0x06003C9D RID: 15517 RVA: 0x000E41B8 File Offset: 0x000E23B8
	public Touch Touch { get; private set; }

	// Token: 0x17000BC3 RID: 3011
	// (get) Token: 0x06003C9E RID: 15518 RVA: 0x000E41C4 File Offset: 0x000E23C4
	// (set) Token: 0x06003C9F RID: 15519 RVA: 0x000E41CC File Offset: 0x000E23CC
	public List<Touch> Touches { get; private set; }

	// Token: 0x17000BC4 RID: 3012
	// (get) Token: 0x06003CA0 RID: 15520 RVA: 0x000E41D8 File Offset: 0x000E23D8
	public bool IsMultiTouch
	{
		get
		{
			return this.Touches.Count > 1;
		}
	}
}
