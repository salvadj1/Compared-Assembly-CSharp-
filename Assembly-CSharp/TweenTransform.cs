using System;
using UnityEngine;

// Token: 0x020007B7 RID: 1975
[AddComponentMenu("NGUI/Tween/Transform")]
public class TweenTransform : UITweener
{
	// Token: 0x0600474A RID: 18250 RVA: 0x0011E9A8 File Offset: 0x0011CBA8
	protected override void OnUpdate(float factor)
	{
		if (this.from != null && this.to != null)
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
			}
			this.mTrans.position = this.from.position * (1f - factor) + this.to.position * factor;
			this.mTrans.localScale = this.from.localScale * (1f - factor) + this.to.localScale * factor;
			this.mTrans.rotation = Quaternion.Slerp(this.from.rotation, this.to.rotation, factor);
		}
	}

	// Token: 0x0600474B RID: 18251 RVA: 0x0011EA8C File Offset: 0x0011CC8C
	public static TweenTransform Begin(GameObject go, float duration, Transform from, Transform to)
	{
		TweenTransform tweenTransform = UITweener.Begin<TweenTransform>(go, duration);
		tweenTransform.from = from;
		tweenTransform.to = to;
		return tweenTransform;
	}

	// Token: 0x0400274A RID: 10058
	public Transform from;

	// Token: 0x0400274B RID: 10059
	public Transform to;

	// Token: 0x0400274C RID: 10060
	private Transform mTrans;
}
