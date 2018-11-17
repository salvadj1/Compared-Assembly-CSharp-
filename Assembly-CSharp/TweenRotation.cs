using System;
using UnityEngine;

// Token: 0x020008A2 RID: 2210
[AddComponentMenu("NGUI/Tween/Rotation")]
public class TweenRotation : global::UITweener
{
	// Token: 0x17000E50 RID: 3664
	// (get) Token: 0x06004BCD RID: 19405 RVA: 0x001281EC File Offset: 0x001263EC
	public Transform cachedTransform
	{
		get
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
			}
			return this.mTrans;
		}
	}

	// Token: 0x17000E51 RID: 3665
	// (get) Token: 0x06004BCE RID: 19406 RVA: 0x00128214 File Offset: 0x00126414
	// (set) Token: 0x06004BCF RID: 19407 RVA: 0x00128224 File Offset: 0x00126424
	public Quaternion rotation
	{
		get
		{
			return this.cachedTransform.localRotation;
		}
		set
		{
			this.cachedTransform.localRotation = value;
		}
	}

	// Token: 0x06004BD0 RID: 19408 RVA: 0x00128234 File Offset: 0x00126434
	protected override void OnUpdate(float factor)
	{
		this.cachedTransform.localRotation = Quaternion.Slerp(Quaternion.Euler(this.from), Quaternion.Euler(this.to), factor);
	}

	// Token: 0x06004BD1 RID: 19409 RVA: 0x00128268 File Offset: 0x00126468
	public static global::TweenRotation Begin(GameObject go, float duration, Quaternion rot)
	{
		global::TweenRotation tweenRotation = global::UITweener.Begin<global::TweenRotation>(go, duration);
		tweenRotation.from = tweenRotation.rotation.eulerAngles;
		tweenRotation.to = rot.eulerAngles;
		return tweenRotation;
	}

	// Token: 0x0400297C RID: 10620
	public Vector3 from;

	// Token: 0x0400297D RID: 10621
	public Vector3 to;

	// Token: 0x0400297E RID: 10622
	private Transform mTrans;
}
