using System;
using UnityEngine;

// Token: 0x0200042D RID: 1069
public class BaseAIMovement : MonoBehaviour
{
	// Token: 0x0600279E RID: 10142 RVA: 0x0009ABAC File Offset: 0x00098DAC
	public virtual bool IsStuck()
	{
		return false;
	}

	// Token: 0x0600279F RID: 10143 RVA: 0x0009ABB0 File Offset: 0x00098DB0
	public virtual void SetMoveDirection(Vector3 worldDir, float speed)
	{
	}

	// Token: 0x060027A0 RID: 10144 RVA: 0x0009ABB4 File Offset: 0x00098DB4
	public virtual void SetLookDirection(Vector3 worldDir)
	{
	}

	// Token: 0x060027A1 RID: 10145 RVA: 0x0009ABB8 File Offset: 0x00098DB8
	public virtual void SetMovePosition(Vector3 worldPos, float speed)
	{
	}

	// Token: 0x060027A2 RID: 10146 RVA: 0x0009ABBC File Offset: 0x00098DBC
	public virtual void SetMoveTarget(GameObject target, float speed)
	{
	}

	// Token: 0x060027A3 RID: 10147 RVA: 0x0009ABC0 File Offset: 0x00098DC0
	public virtual void Stop()
	{
	}

	// Token: 0x060027A4 RID: 10148 RVA: 0x0009ABC4 File Offset: 0x00098DC4
	public virtual void ProcessNetworkUpdate(ref Vector3 origin, ref Quaternion rotation)
	{
		origin = origin;
		rotation = rotation;
	}

	// Token: 0x060027A5 RID: 10149 RVA: 0x0009ABE0 File Offset: 0x00098DE0
	public virtual void DoMove(BasicWildLifeAI ai, ulong simMillis)
	{
	}

	// Token: 0x060027A6 RID: 10150 RVA: 0x0009ABE4 File Offset: 0x00098DE4
	public virtual void InitializeMovement(BasicWildLifeAI ai)
	{
	}

	// Token: 0x060027A7 RID: 10151 RVA: 0x0009ABE8 File Offset: 0x00098DE8
	public virtual float GetActualMovementSpeed()
	{
		return 0f;
	}

	// Token: 0x04001380 RID: 4992
	protected float desiredSpeed;

	// Token: 0x04001381 RID: 4993
	protected float collisionRadius = 0.3f;

	// Token: 0x04001382 RID: 4994
	public float lookDegreeSpeed = 80f;

	// Token: 0x04001383 RID: 4995
	public float maxSlope = 45f;
}
