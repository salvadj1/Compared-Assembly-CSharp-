using System;
using uLink;
using UnityEngine;

// Token: 0x020001C6 RID: 454
public struct QueuedShotDeathInfo
{
	// Token: 0x17000337 RID: 823
	// (get) Token: 0x06000CD8 RID: 3288 RVA: 0x0003214C File Offset: 0x0003034C
	public bool exists
	{
		get
		{
			return this.queued && this.transform;
		}
	}

	// Token: 0x06000CD9 RID: 3289 RVA: 0x00032168 File Offset: 0x00030368
	public void Set(Character character, ref Vector3 localPoint, ref Angle2 localNormal, byte bodyPart, ref NetworkMessageInfo info)
	{
		this.Set(character.hitBoxSystem, ref localPoint, ref localNormal, bodyPart, ref info);
	}

	// Token: 0x06000CDA RID: 3290 RVA: 0x00032188 File Offset: 0x00030388
	public void Set(IDMain idMain, ref Vector3 localPoint, ref Angle2 localNormal, byte bodyPart, ref NetworkMessageInfo info)
	{
		if (idMain is Character)
		{
			this.Set((Character)idMain, ref localPoint, ref localNormal, bodyPart, ref info);
		}
		else
		{
			this.Set(idMain.GetRemote<HitBoxSystem>(), ref localPoint, ref localNormal, bodyPart, ref info);
		}
	}

	// Token: 0x06000CDB RID: 3291 RVA: 0x000321CC File Offset: 0x000303CC
	public void LinkRagdoll(Transform thisRoot, GameObject ragdoll)
	{
		if (this.exists)
		{
			Transform transform;
			if (RagdollHelper.RecursiveLinkTransformsByName(ragdoll.transform, thisRoot, this.transform, out transform))
			{
				Transform transform2 = transform;
				Rigidbody rigidbody = transform2.rigidbody;
				if (rigidbody)
				{
					Vector3 vector = transform2.TransformPoint(this.localPoint);
					Vector3 vector2 = transform2.TransformDirection(this.localNormal);
					rigidbody.AddForceAtPosition(vector2 * 1000f, vector);
				}
			}
		}
		else
		{
			RagdollHelper.RecursiveLinkTransformsByName(ragdoll.transform, thisRoot);
		}
	}

	// Token: 0x06000CDC RID: 3292 RVA: 0x00032250 File Offset: 0x00030450
	public void Set(HitBoxSystem hitBoxSystem, ref Vector3 localPoint, ref Angle2 localNormal, byte bodyPart, ref NetworkMessageInfo info)
	{
		this.queued = true;
		this.localPoint = localPoint;
		this.localNormal = localNormal.forward;
		this.bodyPart = bodyPart;
		if (this.bodyPart != null)
		{
			IDRemoteBodyPart idremoteBodyPart;
			if (hitBoxSystem.bodyParts.TryGetValue(this.bodyPart, ref idremoteBodyPart))
			{
				this.transform = idremoteBodyPart.transform;
			}
			else
			{
				this.transform = null;
			}
		}
		else
		{
			this.transform = null;
		}
	}

	// Token: 0x0400079C RID: 1948
	public bool queued;

	// Token: 0x0400079D RID: 1949
	public Vector3 localPoint;

	// Token: 0x0400079E RID: 1950
	public Vector3 localNormal;

	// Token: 0x0400079F RID: 1951
	public BodyPart bodyPart;

	// Token: 0x040007A0 RID: 1952
	public Transform transform;
}
