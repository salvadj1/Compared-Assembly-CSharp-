using System;
using UnityEngine;

// Token: 0x02000114 RID: 276
public struct CharacterStateInterpolatorData
{
	// Token: 0x06000793 RID: 1939 RVA: 0x000207C0 File Offset: 0x0001E9C0
	public static void Lerp(ref CharacterStateInterpolatorData a, ref CharacterStateInterpolatorData b, float t, out CharacterStateInterpolatorData result)
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
			result.eyesAngles = default(Angle2);
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

	// Token: 0x04000561 RID: 1377
	public Vector3 origin;

	// Token: 0x04000562 RID: 1378
	public Angle2 eyesAngles;

	// Token: 0x04000563 RID: 1379
	public CharacterStateFlags state;
}
