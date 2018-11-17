using System;
using UnityEngine;

// Token: 0x02000499 RID: 1177
public class ModelPrefabSkin : ScriptableObject
{
	// Token: 0x040015A9 RID: 5545
	public string prefab;

	// Token: 0x040015AA RID: 5546
	public ModelPrefabSkin.Part[] parts;

	// Token: 0x040015AB RID: 5547
	public bool once;

	// Token: 0x040015AC RID: 5548
	[NonSerialized]
	public object editorData;

	// Token: 0x0200049A RID: 1178
	[Serializable]
	public class Part
	{
		// Token: 0x060029C1 RID: 10689 RVA: 0x000A3894 File Offset: 0x000A1A94
		public Part()
		{
			this.path = string.Empty;
			this.mesh = string.Empty;
		}

		// Token: 0x040015AD RID: 5549
		public string path;

		// Token: 0x040015AE RID: 5550
		public string mesh;

		// Token: 0x040015AF RID: 5551
		public string[] materials;
	}
}
