using System;
using Facepunch.Procedural;

// Token: 0x020004E6 RID: 1254
public class BasicWildLifeMovement : global::BaseAIMovement
{
	// Token: 0x04001521 RID: 5409
	[NonSerialized]
	protected Facepunch.Procedural.Direction look;

	// Token: 0x04001522 RID: 5410
	protected float actualMoveSpeedPerSec;

	// Token: 0x04001523 RID: 5411
	public float simRate = 5f;

	// Token: 0x04001524 RID: 5412
	public float moveCastOffset = 0.25f;

	// Token: 0x04001525 RID: 5413
	private float hullLength = 0.1f;
}
