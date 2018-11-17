using System;
using UnityEngine;

// Token: 0x020006C1 RID: 1729
public class dfMouseEventArgs : dfControlEventArgs
{
	// Token: 0x06003C8B RID: 15499 RVA: 0x000E4024 File Offset: 0x000E2224
	public dfMouseEventArgs(dfControl Source, dfMouseButtons button, int clicks, Ray ray, Vector2 location, float wheel) : base(Source)
	{
		this.Buttons = button;
		this.Clicks = clicks;
		this.Position = location;
		this.WheelDelta = wheel;
		this.Ray = ray;
	}

	// Token: 0x06003C8C RID: 15500 RVA: 0x000E4060 File Offset: 0x000E2260
	public dfMouseEventArgs(dfControl Source) : base(Source)
	{
		this.Buttons = dfMouseButtons.None;
		this.Clicks = 0;
		this.Position = Vector2.zero;
		this.WheelDelta = 0f;
	}

	// Token: 0x17000BBC RID: 3004
	// (get) Token: 0x06003C8D RID: 15501 RVA: 0x000E4098 File Offset: 0x000E2298
	// (set) Token: 0x06003C8E RID: 15502 RVA: 0x000E40A0 File Offset: 0x000E22A0
	public dfMouseButtons Buttons { get; private set; }

	// Token: 0x17000BBD RID: 3005
	// (get) Token: 0x06003C8F RID: 15503 RVA: 0x000E40AC File Offset: 0x000E22AC
	// (set) Token: 0x06003C90 RID: 15504 RVA: 0x000E40B4 File Offset: 0x000E22B4
	public int Clicks { get; private set; }

	// Token: 0x17000BBE RID: 3006
	// (get) Token: 0x06003C91 RID: 15505 RVA: 0x000E40C0 File Offset: 0x000E22C0
	// (set) Token: 0x06003C92 RID: 15506 RVA: 0x000E40C8 File Offset: 0x000E22C8
	public float WheelDelta { get; private set; }

	// Token: 0x17000BBF RID: 3007
	// (get) Token: 0x06003C93 RID: 15507 RVA: 0x000E40D4 File Offset: 0x000E22D4
	// (set) Token: 0x06003C94 RID: 15508 RVA: 0x000E40DC File Offset: 0x000E22DC
	public Vector2 MoveDelta { get; set; }

	// Token: 0x17000BC0 RID: 3008
	// (get) Token: 0x06003C95 RID: 15509 RVA: 0x000E40E8 File Offset: 0x000E22E8
	// (set) Token: 0x06003C96 RID: 15510 RVA: 0x000E40F0 File Offset: 0x000E22F0
	public Vector2 Position { get; set; }

	// Token: 0x17000BC1 RID: 3009
	// (get) Token: 0x06003C97 RID: 15511 RVA: 0x000E40FC File Offset: 0x000E22FC
	// (set) Token: 0x06003C98 RID: 15512 RVA: 0x000E4104 File Offset: 0x000E2304
	public Ray Ray { get; set; }
}
