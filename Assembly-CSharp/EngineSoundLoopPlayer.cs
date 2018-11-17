using System;
using UnityEngine;

// Token: 0x020000D2 RID: 210
internal sealed class EngineSoundLoopPlayer : MonoBehaviour
{
	// Token: 0x06000477 RID: 1143 RVA: 0x00017058 File Offset: 0x00015258
	private void OnDestroy()
	{
		if (this.instance != null)
		{
			EngineSoundLoop.Instance instance = this.instance;
			this.instance = null;
			instance.Dispose(true);
		}
	}

	// Token: 0x04000415 RID: 1045
	[NonSerialized]
	internal EngineSoundLoop.Instance instance;
}
