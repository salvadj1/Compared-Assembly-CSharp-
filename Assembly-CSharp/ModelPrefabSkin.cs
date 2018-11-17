using System;
using UnityEngine;

// Token: 0x02000554 RID: 1364
public class ModelPrefabSkin : ScriptableObject
{
	// Token: 0x04001766 RID: 5990
	public string prefab;

	// Token: 0x04001767 RID: 5991
	public global::ModelPrefabSkin.Part[] parts;

	// Token: 0x04001768 RID: 5992
	public bool once;

	// Token: 0x04001769 RID: 5993
	[NonSerialized]
	public object editorData;

	// Token: 0x02000555 RID: 1365
	[Serializable]
	public class Part
	{
		// Token: 0x06002D73 RID: 11635 RVA: 0x000AB62C File Offset: 0x000A982C
		public Part()
		{
			this.path = string.Empty;
			this.mesh = string.Empty;
		}

		// Token: 0x0400176A RID: 5994
		public string path;

		// Token: 0x0400176B RID: 5995
		public string mesh;

		// Token: 0x0400176C RID: 5996
		public string[] materials;
	}
}
