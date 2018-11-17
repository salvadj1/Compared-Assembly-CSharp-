using System;
using UnityEngine;

// Token: 0x020007B5 RID: 1973
[AddComponentMenu("NGUI/Tween/Rotation")]
public class TweenRotation : UITweener
{
	// Token: 0x17000DBE RID: 3518
	// (get) Token: 0x0600473E RID: 18238 RVA: 0x0011E7C8 File Offset: 0x0011C9C8
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

	// Token: 0x17000DBF RID: 3519
	// (get) Token: 0x0600473F RID: 18239 RVA: 0x0011E7F0 File Offset: 0x0011C9F0
	// (set) Token: 0x06004740 RID: 18240 RVA: 0x0011E800 File Offset: 0x0011CA00
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

	// Token: 0x06004741 RID: 18241 RVA: 0x0011E810 File Offset: 0x0011CA10
	protected override void OnUpdate(float factor)
	{
		this.cachedTransform.localRotation = Quaternion.Slerp(Quaternion.Euler(this.from), Quaternion.Euler(this.to), factor);
	}

	// Token: 0x06004742 RID: 18242 RVA: 0x0011E844 File Offset: 0x0011CA44
	public static TweenRotation Begin(GameObject go, float duration, Quaternion rot)
	{
		TweenRotation tweenRotation = UITweener.Begin<TweenRotation>(go, duration);
		tweenRotation.from = tweenRotation.rotation.eulerAngles;
		tweenRotation.to = rot.eulerAngles;
		return tweenRotation;
	}

	// Token: 0x04002742 RID: 10050
	public Vector3 from;

	// Token: 0x04002743 RID: 10051
	public Vector3 to;

	// Token: 0x04002744 RID: 10052
	private Transform mTrans;
}
