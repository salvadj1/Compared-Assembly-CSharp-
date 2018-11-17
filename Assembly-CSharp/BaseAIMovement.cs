using System;
using UnityEngine;

// Token: 0x020004E3 RID: 1251
public class BaseAIMovement : MonoBehaviour
{
	// Token: 0x06002B2E RID: 11054 RVA: 0x000A0B2C File Offset: 0x0009ED2C
	public virtual bool IsStuck()
	{
		return false;
	}

	// Token: 0x06002B2F RID: 11055 RVA: 0x000A0B30 File Offset: 0x0009ED30
	public virtual void SetMoveDirection(Vector3 worldDir, float speed)
	{
	}

	// Token: 0x06002B30 RID: 11056 RVA: 0x000A0B34 File Offset: 0x0009ED34
	public virtual void SetLookDirection(Vector3 worldDir)
	{
	}

	// Token: 0x06002B31 RID: 11057 RVA: 0x000A0B38 File Offset: 0x0009ED38
	public virtual void SetMovePosition(Vector3 worldPos, float speed)
	{
	}

	// Token: 0x06002B32 RID: 11058 RVA: 0x000A0B3C File Offset: 0x0009ED3C
	public virtual void SetMoveTarget(GameObject target, float speed)
	{
	}

	// Token: 0x06002B33 RID: 11059 RVA: 0x000A0B40 File Offset: 0x0009ED40
	public virtual void Stop()
	{
	}

	// Token: 0x06002B34 RID: 11060 RVA: 0x000A0B44 File Offset: 0x0009ED44
	public virtual void ProcessNetworkUpdate(ref Vector3 origin, ref Quaternion rotation)
	{
		origin = origin;
		rotation = rotation;
	}

	// Token: 0x06002B35 RID: 11061 RVA: 0x000A0B60 File Offset: 0x0009ED60
	public virtual void DoMove(global::BasicWildLifeAI ai, ulong simMillis)
	{
	}

	// Token: 0x06002B36 RID: 11062 RVA: 0x000A0B64 File Offset: 0x0009ED64
	public virtual void InitializeMovement(global::BasicWildLifeAI ai)
	{
	}

	// Token: 0x06002B37 RID: 11063 RVA: 0x000A0B68 File Offset: 0x0009ED68
	public virtual float GetActualMovementSpeed()
	{
		return 0f;
	}

	// Token: 0x04001503 RID: 5379
	protected float desiredSpeed;

	// Token: 0x04001504 RID: 5380
	protected float collisionRadius = 0.3f;

	// Token: 0x04001505 RID: 5381
	public float lookDegreeSpeed = 80f;

	// Token: 0x04001506 RID: 5382
	public float maxSlope = 45f;
}
