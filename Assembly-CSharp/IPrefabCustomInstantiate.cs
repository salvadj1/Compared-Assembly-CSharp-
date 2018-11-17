using System;

// Token: 0x020003BA RID: 954
public interface IPrefabCustomInstantiate
{
	// Token: 0x0600218A RID: 8586
	IDMain CustomInstantiatePrefab(ref global::CustomInstantiationArgs args);

	// Token: 0x0600218B RID: 8587
	bool InitializePrefabInstance(global::NetInstance net);
}
