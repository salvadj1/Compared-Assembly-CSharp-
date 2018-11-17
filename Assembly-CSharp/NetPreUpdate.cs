using System;
using UnityEngine;

// Token: 0x02000313 RID: 787
internal class NetPreUpdate : MonoBehaviour
{
	// Token: 0x06001E4F RID: 7759 RVA: 0x00076F90 File Offset: 0x00075190
	private void Awake()
	{
		NetCull.Callbacks.BindUpdater(this);
	}

	// Token: 0x06001E50 RID: 7760 RVA: 0x00076F98 File Offset: 0x00075198
	private void OnDestroy()
	{
		NetCull.Callbacks.ResignUpdater(this);
	}

	// Token: 0x06001E51 RID: 7761 RVA: 0x00076FA0 File Offset: 0x000751A0
	private void LateUpdate()
	{
		if (global.fpslog >= 0f)
		{
			if (this.lastfpslog != global.fpslog)
			{
				this.lastfpslog = global.fpslog;
				this.lastfpslogtime = Time.time - this.lastfpslog;
			}
			float time = Time.time;
			if (this.lastfpslog == 0f || time - this.lastfpslogtime >= this.lastfpslog)
			{
				this.lastfpslogtime = time;
				MonoBehaviour.print(string.Concat(new object[]
				{
					DateTime.Now,
					": frame #",
					Time.frameCount,
					", fps ",
					1f / Time.smoothDeltaTime
				}));
			}
		}
		if (Application.isPlaying)
		{
			NetCull.Callbacks.FirePreUpdate(this);
		}
	}

	// Token: 0x04000E97 RID: 3735
	[NonSerialized]
	private float lastfpslog = -1f;

	// Token: 0x04000E98 RID: 3736
	[NonSerialized]
	private float lastfpslogtime;
}
