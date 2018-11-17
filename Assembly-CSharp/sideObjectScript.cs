using System;
using UnityEngine;

// Token: 0x0200074E RID: 1870
public class sideObjectScript : MonoBehaviour
{
	// Token: 0x0400247E RID: 9342
	public RoadObjectScript OODCCOODCC;

	// Token: 0x0400247F RID: 9343
	public int soIndex;

	// Token: 0x04002480 RID: 9344
	public string soName;

	// Token: 0x04002481 RID: 9345
	public int soAlign;

	// Token: 0x04002482 RID: 9346
	public float soUVx = 0.1f;

	// Token: 0x04002483 RID: 9347
	public float soUVy = 1f;

	// Token: 0x04002484 RID: 9348
	public float m_distance = 10f;

	// Token: 0x04002485 RID: 9349
	public int objectType;

	// Token: 0x04002486 RID: 9350
	public int position;

	// Token: 0x04002487 RID: 9351
	public Material mat;

	// Token: 0x04002488 RID: 9352
	public bool weld = true;

	// Token: 0x04002489 RID: 9353
	public bool combine = true;

	// Token: 0x0400248A RID: 9354
	public bool OQCCQQDDOC = true;

	// Token: 0x0400248B RID: 9355
	public string m_go = string.Empty;

	// Token: 0x0400248C RID: 9356
	public string ODDDOCCOQO = string.Empty;

	// Token: 0x0400248D RID: 9357
	public string ODOQDQQCCQ = string.Empty;

	// Token: 0x0400248E RID: 9358
	public GameObject goStart;

	// Token: 0x0400248F RID: 9359
	public GameObject goEnd;

	// Token: 0x04002490 RID: 9360
	public GameObject goInstantiated;

	// Token: 0x04002491 RID: 9361
	public int selectedRotation;

	// Token: 0x04002492 RID: 9362
	public static string[] rotationOptions;

	// Token: 0x04002493 RID: 9363
	public static string[] uvStrings;

	// Token: 0x04002494 RID: 9364
	public int uvInt;

	// Token: 0x04002495 RID: 9365
	public bool randomObjects;

	// Token: 0x04002496 RID: 9366
	public int childOrder;

	// Token: 0x04002497 RID: 9367
	public string[] childOrderStrings;

	// Token: 0x04002498 RID: 9368
	public float density = 1f;

	// Token: 0x04002499 RID: 9369
	public float sidewaysOffset;

	// Token: 0x0400249A RID: 9370
	public int terrainTree;

	// Token: 0x0400249B RID: 9371
	public string[] rotationStrings;

	// Token: 0x0400249C RID: 9372
	public int selectedYRotation;

	// Token: 0x0400249D RID: 9373
	public int childCount;

	// Token: 0x0400249E RID: 9374
	public float xPosition;

	// Token: 0x0400249F RID: 9375
	public float yPosition;

	// Token: 0x040024A0 RID: 9376
	public float uvYRound;

	// Token: 0x040024A1 RID: 9377
	public bool m_collider;
}
