using System;
using UnityEngine;

// Token: 0x020000AC RID: 172
public class OfflineMode : MonoBehaviour
{
	// Token: 0x060003B6 RID: 950 RVA: 0x00013770 File Offset: 0x00011970
	private void Start()
	{
	}

	// Token: 0x04000344 RID: 836
	[SerializeField]
	private CharacterPrefab characterPrefab;

	// Token: 0x04000345 RID: 837
	[SerializeField]
	private OfflinePlayer offlinePlayer;

	// Token: 0x04000346 RID: 838
	[SerializeField]
	private MountedCamera sceneCameraPrefab;

	// Token: 0x04000347 RID: 839
	[SerializeField]
	private bool useSceneViewWhenAvailable;

	// Token: 0x04000348 RID: 840
	[SerializeField]
	private bool paused;

	// Token: 0x04000349 RID: 841
	[SerializeField]
	private bool respawn;

	// Token: 0x0400034A RID: 842
	[SerializeField]
	private bool teleport;

	// Token: 0x0400034B RID: 843
	[SerializeField]
	private float timeScale = 1f;
}
