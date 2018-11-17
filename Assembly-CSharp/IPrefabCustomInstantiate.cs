using System;

// Token: 0x02000311 RID: 785
public interface IPrefabCustomInstantiate
{
	// Token: 0x06001E48 RID: 7752
	IDMain CustomInstantiatePrefab(ref CustomInstantiationArgs args);

	// Token: 0x06001E49 RID: 7753
	bool InitializePrefabInstance(NetInstance net);
}
