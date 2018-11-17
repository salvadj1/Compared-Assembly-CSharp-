using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x0200078D RID: 1933
public class dfTouchEventArgs : global::dfMouseEventArgs
{
	// Token: 0x060040A3 RID: 16547 RVA: 0x000ECC54 File Offset: 0x000EAE54
	public dfTouchEventArgs(global::dfControl Source, Touch touch, Ray ray) : base(Source, global::dfMouseButtons.Left, touch.tapCount, ray, touch.position, 0f)
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

	// Token: 0x060040A4 RID: 16548 RVA: 0x000ECCC8 File Offset: 0x000EAEC8
	public dfTouchEventArgs(global::dfControl source, List<Touch> touches, Ray ray) : this(source, touches.First<Touch>(), ray)
	{
		this.Touches = touches;
	}

	// Token: 0x060040A5 RID: 16549 RVA: 0x000ECCE0 File Offset: 0x000EAEE0
	public dfTouchEventArgs(global::dfControl Source) : base(Source)
	{
		base.Position = Vector2.zero;
	}

	// Token: 0x17000C46 RID: 3142
	// (get) Token: 0x060040A6 RID: 16550 RVA: 0x000ECCF4 File Offset: 0x000EAEF4
	// (set) Token: 0x060040A7 RID: 16551 RVA: 0x000ECCFC File Offset: 0x000EAEFC
	public Touch Touch { get; private set; }

	// Token: 0x17000C47 RID: 3143
	// (get) Token: 0x060040A8 RID: 16552 RVA: 0x000ECD08 File Offset: 0x000EAF08
	// (set) Token: 0x060040A9 RID: 16553 RVA: 0x000ECD10 File Offset: 0x000EAF10
	public List<Touch> Touches { get; private set; }

	// Token: 0x17000C48 RID: 3144
	// (get) Token: 0x060040AA RID: 16554 RVA: 0x000ECD1C File Offset: 0x000EAF1C
	public bool IsMultiTouch
	{
		get
		{
			return this.Touches.Count > 1;
		}
	}
}
