using System;
using UnityEngine;

// Token: 0x020004E0 RID: 1248
public class WaterLine : MonoBehaviour
{
	// Token: 0x06002B24 RID: 11044 RVA: 0x000A09A4 File Offset: 0x0009EBA4
	public void Start()
	{
	}

	// Token: 0x06002B25 RID: 11045 RVA: 0x000A09A8 File Offset: 0x0009EBA8
	public void OnDestroy()
	{
		global::WaterLine.Height = 0f;
	}

	// Token: 0x06002B26 RID: 11046 RVA: 0x000A09B4 File Offset: 0x0009EBB4
	public void Update()
	{
		global::WaterLine.Height = base.transform.position.y;
	}

	// Token: 0x04001500 RID: 5376
	public static float Height;
}
