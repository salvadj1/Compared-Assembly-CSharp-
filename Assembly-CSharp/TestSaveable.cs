using System;
using RustProto;
using UnityEngine;

// Token: 0x020000C9 RID: 201
public class TestSaveable : MonoBehaviour, IServerSaveable
{
	// Token: 0x06000426 RID: 1062 RVA: 0x0001564C File Offset: 0x0001384C
	public void WriteObjectSave(ref SavedObject.Builder saveobj)
	{
	}

	// Token: 0x06000427 RID: 1063 RVA: 0x00015650 File Offset: 0x00013850
	public void ReadObjectSave(ref SavedObject saveobj)
	{
	}
}
