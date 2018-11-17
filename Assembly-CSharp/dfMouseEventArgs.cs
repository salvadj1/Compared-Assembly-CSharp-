using System;
using UnityEngine;

// Token: 0x0200078C RID: 1932
public class dfMouseEventArgs : global::dfControlEventArgs
{
	// Token: 0x06004095 RID: 16533 RVA: 0x000ECB68 File Offset: 0x000EAD68
	public dfMouseEventArgs(global::dfControl Source, global::dfMouseButtons button, int clicks, Ray ray, Vector2 location, float wheel) : base(Source)
	{
		this.Buttons = button;
		this.Clicks = clicks;
		this.Position = location;
		this.WheelDelta = wheel;
		this.Ray = ray;
	}

	// Token: 0x06004096 RID: 16534 RVA: 0x000ECBA4 File Offset: 0x000EADA4
	public dfMouseEventArgs(global::dfControl Source) : base(Source)
	{
		this.Buttons = global::dfMouseButtons.None;
		this.Clicks = 0;
		this.Position = Vector2.zero;
		this.WheelDelta = 0f;
	}

	// Token: 0x17000C40 RID: 3136
	// (get) Token: 0x06004097 RID: 16535 RVA: 0x000ECBDC File Offset: 0x000EADDC
	// (set) Token: 0x06004098 RID: 16536 RVA: 0x000ECBE4 File Offset: 0x000EADE4
	public global::dfMouseButtons Buttons { get; private set; }

	// Token: 0x17000C41 RID: 3137
	// (get) Token: 0x06004099 RID: 16537 RVA: 0x000ECBF0 File Offset: 0x000EADF0
	// (set) Token: 0x0600409A RID: 16538 RVA: 0x000ECBF8 File Offset: 0x000EADF8
	public int Clicks { get; private set; }

	// Token: 0x17000C42 RID: 3138
	// (get) Token: 0x0600409B RID: 16539 RVA: 0x000ECC04 File Offset: 0x000EAE04
	// (set) Token: 0x0600409C RID: 16540 RVA: 0x000ECC0C File Offset: 0x000EAE0C
	public float WheelDelta { get; private set; }

	// Token: 0x17000C43 RID: 3139
	// (get) Token: 0x0600409D RID: 16541 RVA: 0x000ECC18 File Offset: 0x000EAE18
	// (set) Token: 0x0600409E RID: 16542 RVA: 0x000ECC20 File Offset: 0x000EAE20
	public Vector2 MoveDelta { get; set; }

	// Token: 0x17000C44 RID: 3140
	// (get) Token: 0x0600409F RID: 16543 RVA: 0x000ECC2C File Offset: 0x000EAE2C
	// (set) Token: 0x060040A0 RID: 16544 RVA: 0x000ECC34 File Offset: 0x000EAE34
	public Vector2 Position { get; set; }

	// Token: 0x17000C45 RID: 3141
	// (get) Token: 0x060040A1 RID: 16545 RVA: 0x000ECC40 File Offset: 0x000EAE40
	// (set) Token: 0x060040A2 RID: 16546 RVA: 0x000ECC48 File Offset: 0x000EAE48
	public Ray Ray { get; set; }
}
