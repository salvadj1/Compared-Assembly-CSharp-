using System;
using UnityEngine;

// Token: 0x020000E6 RID: 230
internal sealed class EngineSoundLoopPlayer : MonoBehaviour
{
	// Token: 0x060004F5 RID: 1269 RVA: 0x00018A20 File Offset: 0x00016C20
	private void OnDestroy()
	{
		if (this.instance != null)
		{
			global::EngineSoundLoop.Instance instance = this.instance;
			this.instance = null;
			instance.Dispose(true);
		}
	}

	// Token: 0x04000484 RID: 1156
	[NonSerialized]
	internal global::EngineSoundLoop.Instance instance;
}
