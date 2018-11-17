using System;
using UnityEngine;

// Token: 0x020003BC RID: 956
internal class NetPreUpdate : MonoBehaviour
{
	// Token: 0x06002191 RID: 8593 RVA: 0x0007BA10 File Offset: 0x00079C10
	private void Awake()
	{
		global::NetCull.Callbacks.BindUpdater(this);
	}

	// Token: 0x06002192 RID: 8594 RVA: 0x0007BA18 File Offset: 0x00079C18
	private void OnDestroy()
	{
		global::NetCull.Callbacks.ResignUpdater(this);
	}

	// Token: 0x06002193 RID: 8595 RVA: 0x0007BA20 File Offset: 0x00079C20
	private void LateUpdate()
	{
		if (global::global.fpslog >= 0f)
		{
			if (this.lastfpslog != global::global.fpslog)
			{
				this.lastfpslog = global::global.fpslog;
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
			global::NetCull.Callbacks.FirePreUpdate(this);
		}
	}

	// Token: 0x04000FD7 RID: 4055
	[NonSerialized]
	private float lastfpslog = -1f;

	// Token: 0x04000FD8 RID: 4056
	[NonSerialized]
	private float lastfpslogtime;
}
