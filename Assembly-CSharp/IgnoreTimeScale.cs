using System;
using UnityEngine;

// Token: 0x02000872 RID: 2162
[AddComponentMenu("NGUI/Internal/Ignore TimeScale Behaviour")]
public class IgnoreTimeScale : MonoBehaviour
{
	// Token: 0x17000E21 RID: 3617
	// (get) Token: 0x06004A3A RID: 19002 RVA: 0x0011E2B4 File Offset: 0x0011C4B4
	public float realTimeDelta
	{
		get
		{
			return this.mTimeDelta;
		}
	}

	// Token: 0x06004A3B RID: 19003 RVA: 0x0011E2BC File Offset: 0x0011C4BC
	private void OnEnable()
	{
		this.mTimeStarted = true;
		this.mTimeDelta = 0f;
		this.mTimeStart = Time.realtimeSinceStartup;
	}

	// Token: 0x06004A3C RID: 19004 RVA: 0x0011E2DC File Offset: 0x0011C4DC
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

	// Token: 0x04002883 RID: 10371
	private float mTimeStart;

	// Token: 0x04002884 RID: 10372
	private float mTimeDelta;

	// Token: 0x04002885 RID: 10373
	private float mActual;

	// Token: 0x04002886 RID: 10374
	private bool mTimeStarted;
}
