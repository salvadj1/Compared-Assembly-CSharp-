using System;
using RustProto;
using UnityEngine;

// Token: 0x02000099 RID: 153
public class AvatarSaveRestore : MonoBehaviour
{
	// Token: 0x06000332 RID: 818 RVA: 0x0000FEAC File Offset: 0x0000E0AC
	public static void CopyPersistantMessages(ref Avatar.Builder builder, ref Avatar avatar)
	{
		builder.ClearBlueprints();
		for (int i = 0; i < avatar.BlueprintsCount; i++)
		{
			builder.AddBlueprints(avatar.GetBlueprints(i));
		}
	}
}
