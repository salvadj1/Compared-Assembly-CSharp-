using System;
using UnityEngine;

// Token: 0x02000435 RID: 1077
public class NavMeshMovement : BaseAIMovement
{
	// Token: 0x060027CF RID: 10191 RVA: 0x0009B638 File Offset: 0x00099838
	public void Awake()
	{
	}

	// Token: 0x060027D0 RID: 10192 RVA: 0x0009B63C File Offset: 0x0009983C
	public bool RemoveIfNotOnNavmesh()
	{
		if (this._agent == null || !this._agent.enabled)
		{
			TakeDamage.KillSelf(base.GetComponent<IDBase>(), null);
			return true;
		}
		return false;
	}

	// Token: 0x060027D1 RID: 10193 RVA: 0x0009B670 File Offset: 0x00099870
	public override void SetMoveDirection(Vector3 worldDir, float speed)
	{
		this.SetAgentAiming(true);
		this._agent.SetDestination(this.movementTransform.position + worldDir * 30f);
		this._agent.speed = speed;
	}

	// Token: 0x060027D2 RID: 10194 RVA: 0x0009B6B8 File Offset: 0x000998B8
	public override void Stop()
	{
		if (this.RemoveIfNotOnNavmesh())
		{
			return;
		}
		this._agent.Stop();
		this.SetAgentAiming(false);
		this.desiredSpeed = 0f;
	}

	// Token: 0x060027D3 RID: 10195 RVA: 0x0009B6E4 File Offset: 0x000998E4
	public override bool IsStuck()
	{
		Vector3 vector = base.transform.InverseTransformDirection(this._agent.velocity);
		return this._agent.hasPath && this._agent.speed > 0.5f && vector.z < this._agent.speed * 0.25f;
	}

	// Token: 0x060027D4 RID: 10196 RVA: 0x0009B74C File Offset: 0x0009994C
	public override void SetLookDirection(Vector3 worldDir)
	{
		this._agent.SetDestination(base.transform.position);
		this._agent.Stop();
		this.SetAgentAiming(false);
		if (worldDir == Vector3.zero)
		{
			return;
		}
		this.movementTransform.rotation = Quaternion.LookRotation(worldDir);
	}

	// Token: 0x060027D5 RID: 10197 RVA: 0x0009B7A4 File Offset: 0x000999A4
	public override void SetMovePosition(Vector3 worldPos, float speed)
	{
		this.SetAgentAiming(true);
		this._agent.SetDestination(worldPos);
		this._agent.speed = speed;
	}

	// Token: 0x060027D6 RID: 10198 RVA: 0x0009B7D4 File Offset: 0x000999D4
	public override void SetMoveTarget(GameObject target, float speed)
	{
		this.SetAgentAiming(true);
		Vector3 vector = target.transform.position - base.transform.position;
		this._agent.SetDestination(target.transform.position + vector.normalized * 0.5f);
		this._agent.speed = speed;
	}

	// Token: 0x060027D7 RID: 10199 RVA: 0x0009B840 File Offset: 0x00099A40
	public override void ProcessNetworkUpdate(ref Vector3 origin, ref Quaternion rotation)
	{
		Vector3 vector;
		Vector3 vector2;
		TransformHelpers.GetGroundInfo(origin + new Vector3(0f, 0.25f, 0f), 10f, out vector, out vector2);
		Vector3 vector3 = rotation * Vector3.up;
		float num = Vector3.Angle(vector3, vector2);
		if (num > 20f)
		{
			vector2 = Vector3.Slerp(vector3, vector2, 20f / num);
		}
		origin = vector;
		rotation = TransformHelpers.LookRotationForcedUp(rotation, vector2);
	}

	// Token: 0x060027D8 RID: 10200 RVA: 0x0009B8C8 File Offset: 0x00099AC8
	public virtual void SetAgentAiming(bool enabled)
	{
		this._agent.updateRotation = enabled;
	}

	// Token: 0x040013C1 RID: 5057
	public NavMeshAgent _agent;

	// Token: 0x040013C2 RID: 5058
	public Transform movementTransform;

	// Token: 0x040013C3 RID: 5059
	public float targetLookRotation;

	// Token: 0x040013C4 RID: 5060
	private Vector3 lastStuckPos = Vector3.zero;
}
