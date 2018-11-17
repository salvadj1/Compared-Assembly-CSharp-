using System;
using UnityEngine;

// Token: 0x0200042A RID: 1066
public class WaterLine : MonoBehaviour
{
	// Token: 0x06002794 RID: 10132 RVA: 0x0009AA24 File Offset: 0x00098C24
	public void Start()
	{
	}

	// Token: 0x06002795 RID: 10133 RVA: 0x0009AA28 File Offset: 0x00098C28
	public void OnDestroy()
	{
		WaterLine.Height = 0f;
	}

	// Token: 0x06002796 RID: 10134 RVA: 0x0009AA34 File Offset: 0x00098C34
	public void Update()
	{
		WaterLine.Height = base.transform.position.y;
	}

	// Token: 0x0400137D RID: 4989
	public static float Height;
}
