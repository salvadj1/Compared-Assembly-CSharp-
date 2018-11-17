using System;
using RustProto;
using UnityEngine;

// Token: 0x020000AC RID: 172
public class AvatarSaveRestore : MonoBehaviour
{
	// Token: 0x060003AA RID: 938 RVA: 0x0001169C File Offset: 0x0000F89C
	public static void CopyPersistantMessages(ref RustProto.Avatar.Builder builder, ref RustProto.Avatar avatar)
	{
		builder.ClearBlueprints();
		for (int i = 0; i < avatar.BlueprintsCount; i++)
		{
			builder.AddBlueprints(avatar.GetBlueprints(i));
		}
	}
}
