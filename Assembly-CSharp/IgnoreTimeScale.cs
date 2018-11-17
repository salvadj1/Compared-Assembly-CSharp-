using System;
using UnityEngine;

// Token: 0x0200078D RID: 1933
[AddComponentMenu("NGUI/Internal/Ignore TimeScale Behaviour")]
public class IgnoreTimeScale : MonoBehaviour
{
	// Token: 0x17000D91 RID: 3473
	// (get) Token: 0x060045CD RID: 17869 RVA: 0x00114934 File Offset: 0x00112B34
	public float realTimeDelta
	{
		get
		{
			return this.mTimeDelta;
		}
	}

	// Token: 0x060045CE RID: 17870 RVA: 0x0011493C File Offset: 0x00112B3C
	private void OnEnable()
	{
		this.mTimeStarted = true;
		this.mTimeDelta = 0f;
		this.mTimeStart = Time.realtimeSinceStartup;
	}

	// Token: 0x060045CF RID: 17871 RVA: 0x0011495C File Offset: 0x00112B5C
	protected float UpdateRealTimeDelta()
	{
		if (this.mTimeStarted)
		{
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			float num = realtimeSinceStartup - this.mTimeStart;
			this.mActual += Mathf.Max(0f, num);
			this.mTimeDelta = 0.001f * Mathf.Round(this.mActual * 1000f);
			this.mActual -= this.mTimeDelta;
			this.mTimeStart = realtimeSinceStartup;
		}
		else
		{
			this.mTimeStarted = true;
			this.mTimeStart = Time.realtimeSinceStartup;
			this.mTimeDelta = 0f;
		}
		return this.mTimeDelta;
	}

	// Token: 0x0400264C RID: 9804
	private float mTimeStart;

	// Token: 0x0400264D RID: 9805
	private float mTimeDelta;

	// Token: 0x0400264E RID: 9806
	private float mActual;

	// Token: 0x0400264F RID: 9807
	private bool mTimeStarted;
}
