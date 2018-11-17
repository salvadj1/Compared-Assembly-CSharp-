using System;
using UnityEngine;

// Token: 0x0200080F RID: 2063
public class dfAnimatedInt : global::dfAnimatedValue<int>
{
	// Token: 0x0600479E RID: 18334 RVA: 0x0010F19C File Offset: 0x0010D39C
	public dfAnimatedInt(int StartValue, int EndValue, float Time) : base(StartValue, EndValue, Time)
	{
	}

	// Token: 0x0600479F RID: 18335 RVA: 0x0010F1A8 File Offset: 0x0010D3A8
	protected override int Lerp(int startValue, int endValue, float time)
	{
		return Mathf.RoundToInt(Mathf.Lerp((float)startValue, (float)endValue, time));
	}

	// Token: 0x060047A0 RID: 18336 RVA: 0x0010F1BC File Offset: 0x0010D3BC
	public static implicit operator global::dfAnimatedInt(int value)
	{
		return new global::dfAnimatedInt(value, value, 0f);
	}
}
