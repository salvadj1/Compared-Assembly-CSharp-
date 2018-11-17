using System;
using Facepunch.Procedural;

// Token: 0x02000430 RID: 1072
public class BasicWildLifeMovement : BaseAIMovement
{
	// Token: 0x0400139E RID: 5022
	[NonSerialized]
	protected Direction look;

	// Token: 0x0400139F RID: 5023
	protected float actualMoveSpeedPerSec;

	// Token: 0x040013A0 RID: 5024
	public float simRate = 5f;

	// Token: 0x040013A1 RID: 5025
	public float moveCastOffset = 0.25f;

	// Token: 0x040013A2 RID: 5026
	private float hullLength = 0.1f;
}
