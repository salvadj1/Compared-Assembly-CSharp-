using System;
using uLink;
using UnityEngine;

// Token: 0x020001F6 RID: 502
public struct QueuedShotDeathInfo
{
	// Token: 0x1700037B RID: 891
	// (get) Token: 0x06000E18 RID: 3608 RVA: 0x00036038 File Offset: 0x00034238
	public bool exists
	{
		get
		{
			return this.queued && this.transform;
		}
	}

	// Token: 0x06000E19 RID: 3609 RVA: 0x00036054 File Offset: 0x00034254
	public void Set(global::Character character, ref Vector3 localPoint, ref global::Angle2 localNormal, byte bodyPart, ref uLink.NetworkMessageInfo info)
	{
		this.Set(character.hitBoxSystem, ref localPoint, ref localNormal, bodyPart, ref info);
	}

	// Token: 0x06000E1A RID: 3610 RVA: 0x00036074 File Offset: 0x00034274
	public void Set(IDMain idMain, ref Vector3 localPoint, ref global::Angle2 localNormal, byte bodyPart, ref uLink.NetworkMessageInfo info)
	{
		if (idMain is global::Character)
		{
			this.Set((global::Character)idMain, ref localPoint, ref localNormal, bodyPart, ref info);
		}
		else
		{
			this.Set(idMain.GetRemote<global::HitBoxSystem>(), ref localPoint, ref localNormal, bodyPart, ref info);
		}
	}

	// Token: 0x06000E1B RID: 3611 RVA: 0x000360B8 File Offset: 0x000342B8
	public void LinkRagdoll(Transform thisRoot, GameObject ragdoll)
	{
		if (this.exists)
		{
			Transform transform;
			if (global::RagdollHelper.RecursiveLinkTransformsByName(ragdoll.transform, thisRoot, this.transform, out transform))
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
			global::RagdollHelper.RecursiveLinkTransformsByName(ragdoll.transform, thisRoot);
		}
	}

	// Token: 0x06000E1C RID: 3612 RVA: 0x0003613C File Offset: 0x0003433C
	public void Set(global::HitBoxSystem hitBoxSystem, ref Vector3 localPoint, ref global::Angle2 localNormal, byte bodyPart, ref uLink.NetworkMessageInfo info)
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

	// Token: 0x040008B0 RID: 2224
	public bool queued;

	// Token: 0x040008B1 RID: 2225
	public Vector3 localPoint;

	// Token: 0x040008B2 RID: 2226
	public Vector3 localNormal;

	// Token: 0x040008B3 RID: 2227
	public BodyPart bodyPart;

	// Token: 0x040008B4 RID: 2228
	public Transform transform;
}
