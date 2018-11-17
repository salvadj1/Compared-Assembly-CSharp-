using System;
using RustProto;
using UnityEngine;

// Token: 0x020000DD RID: 221
public class TestSaveable : MonoBehaviour, global::IServerSaveable
{
	// Token: 0x060004A4 RID: 1188 RVA: 0x00017014 File Offset: 0x00015214
	public void WriteObjectSave(ref RustProto.SavedObject.Builder saveobj)
	{
	}

	// Token: 0x060004A5 RID: 1189 RVA: 0x00017018 File Offset: 0x00015218
	public void ReadObjectSave(ref RustProto.SavedObject saveobj)
	{
	}
}
