using System;
using UnityEngine;

// Token: 0x020007B4 RID: 1972
[AddComponentMenu("NGUI/Tween/Position")]
public class TweenPosition : UITweener
{
	// Token: 0x17000DBC RID: 3516
	// (get) Token: 0x06004738 RID: 18232 RVA: 0x0011E710 File Offset: 0x0011C910
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

	// Token: 0x17000DBD RID: 3517
	// (get) Token: 0x06004739 RID: 18233 RVA: 0x0011E738 File Offset: 0x0011C938
	// (set) Token: 0x0600473A RID: 18234 RVA: 0x0011E748 File Offset: 0x0011C948
	public Vector3 position
	{
		get
		{
			return this.cachedTransform.localPosition;
		}
		set
		{
			this.cachedTransform.localPosition = value;
		}
	}

	// Token: 0x0600473B RID: 18235 RVA: 0x0011E758 File Offset: 0x0011C958
	protected override void OnUpdate(float factor)
	{
		this.cachedTransform.localPosition = this.from * (1f - factor) + this.to * factor;
	}

	// Token: 0x0600473C RID: 18236 RVA: 0x0011E794 File Offset: 0x0011C994
	public static TweenPosition Begin(GameObject go, float duration, Vector3 pos)
	{
		TweenPosition tweenPosition = UITweener.Begin<TweenPosition>(go, duration);
		tweenPosition.from = tweenPosition.position;
		tweenPosition.to = pos;
		return tweenPosition;
	}

	// Token: 0x0400273F RID: 10047
	public Vector3 from;

	// Token: 0x04002740 RID: 10048
	public Vector3 to;

	// Token: 0x04002741 RID: 10049
	private Transform mTrans;
}
