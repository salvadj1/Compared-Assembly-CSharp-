using System;
using UnityEngine;

// Token: 0x0200073D RID: 1853
[AddComponentMenu("Daikon Forge/Tweens/Color32")]
public class dfTweenColor32 : dfTweenComponent<Color32>
{
	// Token: 0x0600439C RID: 17308 RVA: 0x00106AD8 File Offset: 0x00104CD8
	public override Color32 offset(Color32 lhs, Color32 rhs)
	{
		return lhs + rhs;
	}

	// Token: 0x0600439D RID: 17309 RVA: 0x00106AF0 File Offset: 0x00104CF0
	public override Color32 evaluate(Color32 startValue, Color32 endValue, float time)
	{
		Vector4 vector = startValue;
		Vector4 vector2 = endValue;
		Vector4 vector3;
		vector3..ctor(dfTweenComponent<Color32>.Lerp(vector.x, vector2.x, time), dfTweenComponent<Color32>.Lerp(vector.y, vector2.y, time), dfTweenComponent<Color32>.Lerp(vector.z, vector2.z, time), dfTweenComponent<Color32>.Lerp(vector.w, vector2.w, time));
		return vector3;
	}
}
