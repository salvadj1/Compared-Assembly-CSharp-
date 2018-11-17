using System;
using UnityEngine;

// Token: 0x020004EB RID: 1259
public class NavMeshMovement : global::BaseAIMovement
{
	// Token: 0x06002B5F RID: 11103 RVA: 0x000A15B8 File Offset: 0x0009F7B8
	public void Awake()
	{
	}

	// Token: 0x06002B60 RID: 11104 RVA: 0x000A15BC File Offset: 0x0009F7BC
	public bool RemoveIfNotOnNavmesh()
	{
		if (this._agent == null || !this._agent.enabled)
		{
			global::TakeDamage.KillSelf(base.GetComponent<IDBase>(), null);
			return true;
		}
		return false;
	}

	// Token: 0x06002B61 RID: 11105 RVA: 0x000A15F0 File Offset: 0x0009F7F0
	public override void SetMoveDirection(Vector3 worldDir, float speed)
	{
		this.SetAgentAiming(true);
		this._agent.SetDestination(this.movementTransform.position + worldDir * 30f);
		this._agent.speed = speed;
	}

	// Token: 0x06002B62 RID: 11106 RVA: 0x000A1638 File Offset: 0x0009F838
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

	// Token: 0x06002B63 RID: 11107 RVA: 0x000A1664 File Offset: 0x0009F864
	public override bool IsStuck()
	{
		Vector3 vector = base.transform.InverseTransformDirection(this._agent.velocity);
		return this._agent.hasPath && this._agent.speed > 0.5f && vector.z < this._agent.speed * 0.25f;
	}

	// Token: 0x06002B64 RID: 11108 RVA: 0x000A16CC File Offset: 0x0009F8CC
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

	// Token: 0x06002B65 RID: 11109 RVA: 0x000A1724 File Offset: 0x0009F924
	public override void SetMovePosition(Vector3 worldPos, float speed)
	{
		this.SetAgentAiming(true);
		this._agent.SetDestination(worldPos);
		this._agent.speed = speed;
	}

	// Token: 0x06002B66 RID: 11110 RVA: 0x000A1754 File Offset: 0x0009F954
	public override void SetMoveTarget(GameObject target, float speed)
	{
		this.SetAgentAiming(true);
		Vector3 vector = target.transform.position - base.transform.position;
		this._agent.SetDestination(target.transform.position + vector.normalized * 0.5f);
		this._agent.speed = speed;
	}

	// Token: 0x06002B67 RID: 11111 RVA: 0x000A17C0 File Offset: 0x0009F9C0
	public override void ProcessNetworkUpdate(ref Vector3 origin, ref Quaternion rotation)
	{
		Vector3 vector;
		Vector3 vector2;
		global::TransformHelpers.GetGroundInfo(origin + new Vector3(0f, 0.25f, 0f), 10f, out vector, out vector2);
		Vector3 vector3 = rotation * Vector3.up;
		float num = Vector3.Angle(vector3, vector2);
		if (num > 20f)
		{
			vector2 = Vector3.Slerp(vector3, vector2, 20f / num);
		}
		origin = vector;
		rotation = global::TransformHelpers.LookRotationForcedUp(rotation, vector2);
	}

	// Token: 0x06002B68 RID: 11112 RVA: 0x000A1848 File Offset: 0x0009FA48
	public virtual void SetAgentAiming(bool enabled)
	{
		this._agent.updateRotation = enabled;
	}

	// Token: 0x04001544 RID: 5444
	public NavMeshAgent _agent;

	// Token: 0x04001545 RID: 5445
	public Transform movementTransform;

	// Token: 0x04001546 RID: 5446
	public float targetLookRotation;

	// Token: 0x04001547 RID: 5447
	private Vector3 lastStuckPos = Vector3.zero;
}
