using System;
using UnityEngine;

// Token: 0x020000BF RID: 191
public class OfflineMode : MonoBehaviour
{
	// Token: 0x0600042E RID: 1070 RVA: 0x00014F60 File Offset: 0x00013160
	private void Start()
	{
	}

	// Token: 0x040003AF RID: 943
	[SerializeField]
	private global::CharacterPrefab characterPrefab;

	// Token: 0x040003B0 RID: 944
	[SerializeField]
	private global::OfflinePlayer offlinePlayer;

	// Token: 0x040003B1 RID: 945
	[SerializeField]
	private global::MountedCamera sceneCameraPrefab;

	// Token: 0x040003B2 RID: 946
	[SerializeField]
	private bool useSceneViewWhenAvailable;

	// Token: 0x040003B3 RID: 947
	[SerializeField]
	private bool paused;

	// Token: 0x040003B4 RID: 948
	[SerializeField]
	private bool respawn;

	// Token: 0x040003B5 RID: 949
	[SerializeField]
	private bool teleport;

	// Token: 0x040003B6 RID: 950
	[SerializeField]
	private float timeScale = 1f;
}
