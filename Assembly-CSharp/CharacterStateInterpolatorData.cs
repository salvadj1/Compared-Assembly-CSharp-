using System;
using UnityEngine;

// Token: 0x02000133 RID: 307
public struct CharacterStateInterpolatorData
{
	// Token: 0x06000865 RID: 2149 RVA: 0x00023394 File Offset: 0x00021594
	public static void Lerp(ref global::CharacterStateInterpolatorData a, ref global::CharacterStateInterpolatorData b, float t, out global::CharacterStateInterpolatorData result)
	{
		if (t == 0f)
		{
			result = a;
		}
		else if (t == 1f)
		{
			result = b;
		}
		else
		{
			float num = 1f - t;
			result.origin.x = a.origin.x * num + b.origin.x * t;
			result.origin.y = a.origin.y * num + b.origin.y * t;
			result.origin.z = a.origin.z * num + b.origin.z * t;
			result.eyesAngles = default(global::Angle2);
			result.eyesAngles.yaw = a.eyesAngles.yaw + Mathf.DeltaAngle(a.eyesAngles.yaw, b.eyesAngles.yaw) * t;
			result.eyesAngles.pitch = Mathf.DeltaAngle(0f, a.eyesAngles.pitch + Mathf.DeltaAngle(a.eyesAngles.pitch, b.eyesAngles.pitch) * t);
			if (t > 1f)
			{
				result.state = b.state;
			}
			else if (t < 0f)
			{
				result.state = a.state;
			}
			else
			{
				result.state = a.state;
				result.state.flags = (result.state.flags | (ushort)((byte)(b.state.flags & 67)));
				if (result.state.grounded != b.state.grounded)
				{
					result.state.grounded = false;
				}
			}
		}
	}

	// Token: 0x0400062C RID: 1580
	public Vector3 origin;

	// Token: 0x0400062D RID: 1581
	public global::Angle2 eyesAngles;

	// Token: 0x0400062E RID: 1582
	public global::CharacterStateFlags state;
}
