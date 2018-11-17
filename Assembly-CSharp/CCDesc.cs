using System;
using UnityEngine;

// Token: 0x02000273 RID: 627
[RequireComponent(typeof(CharacterController))]
public sealed class CCDesc : MonoBehaviour
{
	// Token: 0x060016BA RID: 5818 RVA: 0x000567B4 File Offset: 0x000549B4
	public CCDesc()
	{
		Vector3 center = default(Vector3);
		center.y = 1f;
		this.m_Center = center;
		base..ctor();
	}

	// Token: 0x17000681 RID: 1665
	// (get) Token: 0x060016BB RID: 5819 RVA: 0x0005681C File Offset: 0x00054A1C
	public float height
	{
		get
		{
			return this.m_Height;
		}
	}

	// Token: 0x17000682 RID: 1666
	// (get) Token: 0x060016BC RID: 5820 RVA: 0x00056824 File Offset: 0x00054A24
	public float radius
	{
		get
		{
			return this.m_Radius;
		}
	}

	// Token: 0x17000683 RID: 1667
	// (get) Token: 0x060016BD RID: 5821 RVA: 0x0005682C File Offset: 0x00054A2C
	public Vector3 center
	{
		get
		{
			return this.m_Center;
		}
	}

	// Token: 0x17000684 RID: 1668
	// (get) Token: 0x060016BE RID: 5822 RVA: 0x00056834 File Offset: 0x00054A34
	public float slopeLimit
	{
		get
		{
			return this.m_SlopeLimit;
		}
	}

	// Token: 0x17000685 RID: 1669
	// (get) Token: 0x060016BF RID: 5823 RVA: 0x0005683C File Offset: 0x00054A3C
	public float stepOffset
	{
		get
		{
			return this.m_StepOffset;
		}
	}

	// Token: 0x17000686 RID: 1670
	// (get) Token: 0x060016C0 RID: 5824 RVA: 0x00056844 File Offset: 0x00054A44
	// (set) Token: 0x060016C1 RID: 5825 RVA: 0x00056854 File Offset: 0x00054A54
	public bool detectCollisions
	{
		get
		{
			return this.m_Collider.detectCollisions;
		}
		set
		{
			this.m_Collider.detectCollisions = value;
		}
	}

	// Token: 0x17000687 RID: 1671
	// (get) Token: 0x060016C2 RID: 5826 RVA: 0x00056864 File Offset: 0x00054A64
	public CollisionFlags collisionFlags
	{
		get
		{
			return this.m_Collider.collisionFlags;
		}
	}

	// Token: 0x17000688 RID: 1672
	// (get) Token: 0x060016C3 RID: 5827 RVA: 0x00056874 File Offset: 0x00054A74
	public bool isGrounded
	{
		get
		{
			return this.m_Collider.isGrounded;
		}
	}

	// Token: 0x17000689 RID: 1673
	// (get) Token: 0x060016C4 RID: 5828 RVA: 0x00056884 File Offset: 0x00054A84
	public Vector3 velocity
	{
		get
		{
			return this.m_Collider.velocity;
		}
	}

	// Token: 0x1700068A RID: 1674
	// (get) Token: 0x060016C5 RID: 5829 RVA: 0x00056894 File Offset: 0x00054A94
	public float skinWidth
	{
		get
		{
			return this.m_SkinWidth;
		}
	}

	// Token: 0x1700068B RID: 1675
	// (get) Token: 0x060016C6 RID: 5830 RVA: 0x0005689C File Offset: 0x00054A9C
	public float minMoveDistance
	{
		get
		{
			return this.m_MinMoveDistance;
		}
	}

	// Token: 0x1700068C RID: 1676
	// (get) Token: 0x060016C7 RID: 5831 RVA: 0x000568A4 File Offset: 0x00054AA4
	public float diameter
	{
		get
		{
			return this.m_Radius + this.m_Radius;
		}
	}

	// Token: 0x1700068D RID: 1677
	// (get) Token: 0x060016C8 RID: 5832 RVA: 0x000568B4 File Offset: 0x00054AB4
	public float skinnedRadius
	{
		get
		{
			return this.m_Radius + this.m_SkinWidth;
		}
	}

	// Token: 0x1700068E RID: 1678
	// (get) Token: 0x060016C9 RID: 5833 RVA: 0x000568C4 File Offset: 0x00054AC4
	public float skinnedDiameter
	{
		get
		{
			return this.m_Radius + this.m_Radius + this.m_SkinWidth + this.m_SkinWidth;
		}
	}

	// Token: 0x1700068F RID: 1679
	// (get) Token: 0x060016CA RID: 5834 RVA: 0x000568E4 File Offset: 0x00054AE4
	public float effectiveHeight
	{
		get
		{
			float num = this.m_Radius + this.m_Radius;
			return (num <= this.m_Height) ? this.m_Height : num;
		}
	}

	// Token: 0x17000690 RID: 1680
	// (get) Token: 0x060016CB RID: 5835 RVA: 0x00056918 File Offset: 0x00054B18
	public float effectiveSkinnedHeight
	{
		get
		{
			float num = this.m_Radius + this.m_Radius;
			return ((num <= this.m_Height) ? this.m_Height : num) + (this.m_SkinWidth + this.m_SkinWidth);
		}
	}

	// Token: 0x17000691 RID: 1681
	// (get) Token: 0x060016CC RID: 5836 RVA: 0x0005695C File Offset: 0x00054B5C
	public float skinnedHeight
	{
		get
		{
			return this.m_Height + this.m_SkinWidth + this.m_SkinWidth;
		}
	}

	// Token: 0x17000692 RID: 1682
	// (get) Token: 0x060016CD RID: 5837 RVA: 0x00056974 File Offset: 0x00054B74
	public Vector3 top
	{
		get
		{
			Vector3 result;
			result.x = this.m_Center.x;
			result.z = this.m_Center.z;
			float num = this.m_Height * 0.5f;
			if (this.m_Radius > num)
			{
				num = this.m_Radius;
			}
			result.y = this.m_Center.y + num;
			return result;
		}
	}

	// Token: 0x17000693 RID: 1683
	// (get) Token: 0x060016CE RID: 5838 RVA: 0x000569DC File Offset: 0x00054BDC
	public Vector3 skinnedTop
	{
		get
		{
			Vector3 result;
			result.x = this.m_Center.x;
			result.z = this.m_Center.z;
			float num = this.m_Height * 0.5f;
			if (this.m_Radius > num)
			{
				num = this.m_Radius;
			}
			result.y = this.m_Center.y + num + this.m_SkinWidth;
			return result;
		}
	}

	// Token: 0x17000694 RID: 1684
	// (get) Token: 0x060016CF RID: 5839 RVA: 0x00056A4C File Offset: 0x00054C4C
	public Vector3 bottom
	{
		get
		{
			Vector3 result;
			result.x = this.m_Center.x;
			result.z = this.m_Center.z;
			float num = this.m_Height * 0.5f;
			if (this.m_Radius > num)
			{
				num = this.m_Radius;
			}
			result.y = this.m_Center.y - num;
			return result;
		}
	}

	// Token: 0x17000695 RID: 1685
	// (get) Token: 0x060016D0 RID: 5840 RVA: 0x00056AB4 File Offset: 0x00054CB4
	public Vector3 skinnedBottom
	{
		get
		{
			Vector3 result;
			result.x = this.m_Center.x;
			result.z = this.m_Center.z;
			float num = this.m_Height * 0.5f;
			if (this.m_Radius > num)
			{
				num = this.m_Radius;
			}
			result.y = this.m_Center.y - (num + this.m_SkinWidth);
			return result;
		}
	}

	// Token: 0x17000696 RID: 1686
	// (get) Token: 0x060016D1 RID: 5841 RVA: 0x00056B24 File Offset: 0x00054D24
	public Vector3 worldTop
	{
		get
		{
			return this.OffsetToWorld(this.top);
		}
	}

	// Token: 0x17000697 RID: 1687
	// (get) Token: 0x060016D2 RID: 5842 RVA: 0x00056B34 File Offset: 0x00054D34
	public Vector3 worldSkinnedTop
	{
		get
		{
			return this.OffsetToWorld(this.skinnedTop);
		}
	}

	// Token: 0x17000698 RID: 1688
	// (get) Token: 0x060016D3 RID: 5843 RVA: 0x00056B44 File Offset: 0x00054D44
	public Vector3 worldCenter
	{
		get
		{
			return this.OffsetToWorld(this.m_Center);
		}
	}

	// Token: 0x17000699 RID: 1689
	// (get) Token: 0x060016D4 RID: 5844 RVA: 0x00056B54 File Offset: 0x00054D54
	public Vector3 worldBottom
	{
		get
		{
			return this.OffsetToWorld(this.bottom);
		}
	}

	// Token: 0x1700069A RID: 1690
	// (get) Token: 0x060016D5 RID: 5845 RVA: 0x00056B64 File Offset: 0x00054D64
	public Vector3 worldSkinnedBottom
	{
		get
		{
			return this.OffsetToWorld(this.skinnedBottom);
		}
	}

	// Token: 0x1700069B RID: 1691
	// (get) Token: 0x060016D6 RID: 5846 RVA: 0x00056B74 File Offset: 0x00054D74
	public Vector3 centroidTop
	{
		get
		{
			Vector3 result;
			result.x = this.m_Center.x;
			result.z = this.m_Center.z;
			float num = this.m_Height * 0.5f;
			if (this.m_Radius > num)
			{
				num = 0f;
			}
			else
			{
				num -= this.m_Radius;
			}
			result.y = this.m_Center.y + num;
			return result;
		}
	}

	// Token: 0x1700069C RID: 1692
	// (get) Token: 0x060016D7 RID: 5847 RVA: 0x00056BE8 File Offset: 0x00054DE8
	public Vector3 centroidBottom
	{
		get
		{
			Vector3 result;
			result.x = this.m_Center.x;
			result.z = this.m_Center.z;
			float num = this.m_Height * 0.5f;
			if (this.m_Radius > num)
			{
				num = 0f;
			}
			else
			{
				num -= this.m_Radius;
			}
			result.y = this.m_Center.y - num;
			return result;
		}
	}

	// Token: 0x1700069D RID: 1693
	// (get) Token: 0x060016D8 RID: 5848 RVA: 0x00056C5C File Offset: 0x00054E5C
	public Vector3 worldCentroidTop
	{
		get
		{
			return this.OffsetToWorld(this.centroidTop);
		}
	}

	// Token: 0x1700069E RID: 1694
	// (get) Token: 0x060016D9 RID: 5849 RVA: 0x00056C6C File Offset: 0x00054E6C
	public Vector3 worldCentroidBottom
	{
		get
		{
			return this.OffsetToWorld(this.centroidBottom);
		}
	}

	// Token: 0x1700069F RID: 1695
	// (get) Token: 0x060016DA RID: 5850 RVA: 0x00056C7C File Offset: 0x00054E7C
	public Quaternion flatRotation
	{
		get
		{
			Vector3 forward = base.transform.forward;
			forward.y = forward.x * forward.x + forward.z * forward.z;
			if (Mathf.Approximately(forward.y, 0f))
			{
				Vector3 right = base.transform.right;
				forward.z = right.x;
				forward.x = -right.z;
				forward.y = right.x * right.x + right.z * right.z;
			}
			if (forward.y != 1f)
			{
				forward.y = 1f / Mathf.Sqrt(forward.y);
			}
			forward.x *= forward.y;
			forward.z *= forward.z;
			forward.y = 0f;
			return Quaternion.LookRotation(forward, Vector3.up);
		}
	}

	// Token: 0x170006A0 RID: 1696
	// (get) Token: 0x060016DB RID: 5851 RVA: 0x00056D8C File Offset: 0x00054F8C
	public static CCDesc Moving
	{
		get
		{
			return CCDesc.s_CurrentMovingCCDesc;
		}
	}

	// Token: 0x170006A1 RID: 1697
	// (get) Token: 0x060016DC RID: 5852 RVA: 0x00056D94 File Offset: 0x00054F94
	public Rigidbody attachedRigidbody
	{
		get
		{
			return this.m_Collider.attachedRigidbody;
		}
	}

	// Token: 0x170006A2 RID: 1698
	// (get) Token: 0x060016DD RID: 5853 RVA: 0x00056DA4 File Offset: 0x00054FA4
	public Bounds bounds
	{
		get
		{
			return this.m_Collider.bounds;
		}
	}

	// Token: 0x170006A3 RID: 1699
	// (get) Token: 0x060016DE RID: 5854 RVA: 0x00056DB4 File Offset: 0x00054FB4
	// (set) Token: 0x060016DF RID: 5855 RVA: 0x00056DC4 File Offset: 0x00054FC4
	public bool enabled
	{
		get
		{
			return this.m_Collider.enabled;
		}
		set
		{
			this.m_Collider.enabled = value;
		}
	}

	// Token: 0x170006A4 RID: 1700
	// (get) Token: 0x060016E0 RID: 5856 RVA: 0x00056DD4 File Offset: 0x00054FD4
	// (set) Token: 0x060016E1 RID: 5857 RVA: 0x00056DE4 File Offset: 0x00054FE4
	public bool isTrigger
	{
		get
		{
			return this.m_Collider.isTrigger;
		}
		set
		{
			this.m_Collider.isTrigger = value;
		}
	}

	// Token: 0x170006A5 RID: 1701
	// (get) Token: 0x060016E2 RID: 5858 RVA: 0x00056DF4 File Offset: 0x00054FF4
	// (set) Token: 0x060016E3 RID: 5859 RVA: 0x00056E04 File Offset: 0x00055004
	public PhysicMaterial material
	{
		get
		{
			return this.m_Collider.material;
		}
		set
		{
			this.m_Collider.material = value;
		}
	}

	// Token: 0x170006A6 RID: 1702
	// (get) Token: 0x060016E4 RID: 5860 RVA: 0x00056E14 File Offset: 0x00055014
	// (set) Token: 0x060016E5 RID: 5861 RVA: 0x00056E24 File Offset: 0x00055024
	public PhysicMaterial sharedMaterial
	{
		get
		{
			return this.m_Collider.sharedMaterial;
		}
		set
		{
			this.m_Collider.sharedMaterial = value;
		}
	}

	// Token: 0x170006A7 RID: 1703
	// (get) Token: 0x060016E6 RID: 5862 RVA: 0x00056E34 File Offset: 0x00055034
	public CharacterController collider
	{
		get
		{
			return this.m_Collider;
		}
	}

	// Token: 0x060016E7 RID: 5863 RVA: 0x00056E3C File Offset: 0x0005503C
	public CollisionFlags Move(Vector3 motion)
	{
		CCDesc ccdesc = CCDesc.s_CurrentMovingCCDesc;
		CollisionFlags result;
		try
		{
			CCDesc.s_CurrentMovingCCDesc = this;
			if (!object.ReferenceEquals(this.AssignedHitManager, null))
			{
				this.AssignedHitManager.Clear();
			}
			result = this.m_Collider.Move(motion);
		}
		finally
		{
			CCDesc.s_CurrentMovingCCDesc = ((!ccdesc) ? null : ccdesc);
		}
		return result;
	}

	// Token: 0x060016E8 RID: 5864 RVA: 0x00056EC0 File Offset: 0x000550C0
	public bool SimpleMove(Vector3 speed)
	{
		CCDesc ccdesc = CCDesc.s_CurrentMovingCCDesc;
		bool result;
		try
		{
			CCDesc.s_CurrentMovingCCDesc = this;
			result = this.m_Collider.SimpleMove(speed);
		}
		finally
		{
			CCDesc.s_CurrentMovingCCDesc = ((!ccdesc) ? null : ccdesc);
		}
		return result;
	}

	// Token: 0x060016E9 RID: 5865 RVA: 0x00056F28 File Offset: 0x00055128
	public Vector3 ClosestPointOnBounds(Vector3 position)
	{
		return this.m_Collider.ClosestPointOnBounds(position);
	}

	// Token: 0x060016EA RID: 5866 RVA: 0x00056F38 File Offset: 0x00055138
	public bool Raycast(Ray ray, out RaycastHit hitInfo, float distance)
	{
		return this.m_Collider.Raycast(ray, ref hitInfo, distance);
	}

	// Token: 0x060016EB RID: 5867 RVA: 0x00056F48 File Offset: 0x00055148
	public Vector3 OffsetToWorld(Vector3 offset)
	{
		if (offset.x != 0f || offset.z != 0f)
		{
			offset = this.flatRotation * offset;
		}
		Vector3 lossyScale = base.transform.lossyScale;
		offset.x *= lossyScale.x;
		offset.y *= lossyScale.y;
		offset.z *= lossyScale.z;
		Vector3 position = base.transform.position;
		offset.x += position.x;
		offset.y += position.y;
		offset.z += position.z;
		return offset;
	}

	// Token: 0x060016EC RID: 5868 RVA: 0x0005701C File Offset: 0x0005521C
	public CCDesc.HeightModification ModifyHeight(float newEffectiveSkinnedHeight, bool preview = false)
	{
		float num = this.m_Radius + this.m_Radius;
		float num2 = this.m_SkinWidth + this.m_SkinWidth + num;
		float num3 = (num <= this.m_Height) ? (this.m_Height + this.m_SkinWidth + this.m_SkinWidth) : num2;
		CCDesc.HeightModification result;
		result.original.effectiveSkinnedHeight = num3;
		result.original.center = this.m_Center;
		if (newEffectiveSkinnedHeight < num2)
		{
			result.modified.effectiveSkinnedHeight = num2;
		}
		else
		{
			result.modified.effectiveSkinnedHeight = newEffectiveSkinnedHeight;
		}
		if (result.differed = (result.original.effectiveSkinnedHeight != result.modified.effectiveSkinnedHeight))
		{
			float num4 = num3 * 0.5f;
			float num5 = result.original.center.y - num4;
			float num6 = result.original.center.y + num4;
			result.delta.effectiveSkinnedHeight = result.modified.effectiveSkinnedHeight - result.original.effectiveSkinnedHeight;
			result.scale = result.modified.effectiveSkinnedHeight / result.original.effectiveSkinnedHeight;
			float num7 = num5 * result.scale;
			float num8 = num6 * result.scale;
			result.modified.center.x = result.original.center.x;
			result.modified.center.z = result.original.center.z;
			result.modified.center.y = num7 + (num8 - num7) * 0.5f;
			result.delta.center.x = 0f;
			result.delta.center.z = 0f;
			result.delta.center.y = result.modified.center.y - result.original.center.y;
			if (result.applied = !preview)
			{
				this.m_Height = result.modified.effectiveSkinnedHeight - (this.m_SkinWidth + this.m_SkinWidth);
				this.m_Center = result.modified.center;
				if (result.scale < 1f)
				{
					this.m_Collider.center = this.m_Center;
					this.m_Collider.height = this.m_Height;
				}
				else
				{
					this.m_Collider.height = this.m_Height;
					this.m_Collider.center = this.m_Center;
				}
			}
		}
		else
		{
			result.modified = result.original;
			result.delta = default(CCDesc.HeightModification.State);
			result.applied = false;
			result.scale = 1f;
		}
		return result;
	}

	// Token: 0x04000B9D RID: 2973
	[SerializeField]
	private float m_Height = 2f;

	// Token: 0x04000B9E RID: 2974
	[SerializeField]
	private float m_Radius = 0.4f;

	// Token: 0x04000B9F RID: 2975
	[SerializeField]
	private float m_SlopeLimit = 90f;

	// Token: 0x04000BA0 RID: 2976
	[SerializeField]
	private float m_StepOffset = 0.5f;

	// Token: 0x04000BA1 RID: 2977
	[SerializeField]
	private float m_SkinWidth = 0.05f;

	// Token: 0x04000BA2 RID: 2978
	[SerializeField]
	private float m_MinMoveDistance;

	// Token: 0x04000BA3 RID: 2979
	[SerializeField]
	private Vector3 m_Center;

	// Token: 0x04000BA4 RID: 2980
	[PrefetchComponent]
	[HideInInspector]
	[SerializeField]
	private CharacterController m_Collider;

	// Token: 0x04000BA5 RID: 2981
	[NonSerialized]
	public object Tag;

	// Token: 0x04000BA6 RID: 2982
	private static CCDesc s_CurrentMovingCCDesc;

	// Token: 0x04000BA7 RID: 2983
	[NonSerialized]
	internal CCDesc.HitManager AssignedHitManager;

	// Token: 0x02000274 RID: 628
	public struct HeightModification
	{
		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x060016ED RID: 5869 RVA: 0x00057310 File Offset: 0x00055510
		public float bottomDeltaHeight
		{
			get
			{
				return this.modified.skinnedBottomY - this.original.skinnedBottomY;
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x060016EE RID: 5870 RVA: 0x0005732C File Offset: 0x0005552C
		public float topDeltaHeight
		{
			get
			{
				return this.modified.skinnedTopY - this.original.skinnedTopY;
			}
		}

		// Token: 0x04000BA8 RID: 2984
		public CCDesc.HeightModification.State original;

		// Token: 0x04000BA9 RID: 2985
		public CCDesc.HeightModification.State modified;

		// Token: 0x04000BAA RID: 2986
		public CCDesc.HeightModification.State delta;

		// Token: 0x04000BAB RID: 2987
		public float scale;

		// Token: 0x04000BAC RID: 2988
		public bool differed;

		// Token: 0x04000BAD RID: 2989
		public bool applied;

		// Token: 0x02000275 RID: 629
		public struct State
		{
			// Token: 0x170006AA RID: 1706
			// (get) Token: 0x060016EF RID: 5871 RVA: 0x00057348 File Offset: 0x00055548
			public float skinnedBottomY
			{
				get
				{
					return this.center.y - this.effectiveSkinnedHeight * 0.5f;
				}
			}

			// Token: 0x170006AB RID: 1707
			// (get) Token: 0x060016F0 RID: 5872 RVA: 0x00057364 File Offset: 0x00055564
			public float skinnedTopY
			{
				get
				{
					return this.center.y + this.effectiveSkinnedHeight * 0.5f;
				}
			}

			// Token: 0x04000BAE RID: 2990
			public float effectiveSkinnedHeight;

			// Token: 0x04000BAF RID: 2991
			public Vector3 center;
		}
	}

	// Token: 0x02000276 RID: 630
	public struct Hit
	{
		// Token: 0x060016F1 RID: 5873 RVA: 0x00057380 File Offset: 0x00055580
		public Hit(ControllerColliderHit ControllerColliderHit)
		{
			this.CharacterController = ControllerColliderHit.controller;
			CCDesc s_CurrentMovingCCDesc = CCDesc.s_CurrentMovingCCDesc;
			if (!s_CurrentMovingCCDesc || s_CurrentMovingCCDesc.collider != this.CharacterController)
			{
				this.CCDesc = this.CharacterController.GetComponent<CCDesc>();
			}
			else
			{
				this.CCDesc = s_CurrentMovingCCDesc;
			}
			this.Collider = ControllerColliderHit.collider;
			this.Point = ControllerColliderHit.point;
			this.Normal = ControllerColliderHit.normal;
			this.MoveDirection = ControllerColliderHit.moveDirection;
			this.MoveLength = ControllerColliderHit.moveLength;
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x060016F2 RID: 5874 RVA: 0x0005741C File Offset: 0x0005561C
		public GameObject GameObject
		{
			get
			{
				return (!this.Collider) ? null : this.Collider.transform.gameObject;
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x060016F3 RID: 5875 RVA: 0x00057450 File Offset: 0x00055650
		public Transform Transform
		{
			get
			{
				return (!this.Collider) ? null : this.Collider.transform;
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x060016F4 RID: 5876 RVA: 0x00057474 File Offset: 0x00055674
		public Rigidbody Rigidbody
		{
			get
			{
				return (!this.Collider) ? null : this.Collider.attachedRigidbody;
			}
		}

		// Token: 0x04000BB0 RID: 2992
		public readonly CharacterController CharacterController;

		// Token: 0x04000BB1 RID: 2993
		public readonly CCDesc CCDesc;

		// Token: 0x04000BB2 RID: 2994
		public readonly Collider Collider;

		// Token: 0x04000BB3 RID: 2995
		public readonly Vector3 Point;

		// Token: 0x04000BB4 RID: 2996
		public readonly Vector3 Normal;

		// Token: 0x04000BB5 RID: 2997
		public readonly Vector3 MoveDirection;

		// Token: 0x04000BB6 RID: 2998
		public readonly float MoveLength;
	}

	// Token: 0x02000277 RID: 631
	public class HitManager : IDisposable
	{
		// Token: 0x060016F5 RID: 5877 RVA: 0x00057498 File Offset: 0x00055698
		public HitManager(int bufferSize)
		{
			this.bufferSize = bufferSize;
			this.buffer = new CCDesc.Hit[bufferSize];
			this.filledCount = 0;
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x000574C8 File Offset: 0x000556C8
		public HitManager() : this(8)
		{
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060016F7 RID: 5879 RVA: 0x000574D4 File Offset: 0x000556D4
		// (remove) Token: 0x060016F8 RID: 5880 RVA: 0x000574F0 File Offset: 0x000556F0
		public event CCDesc.HitFilter OnHit;

		// Token: 0x060016F9 RID: 5881 RVA: 0x0005750C File Offset: 0x0005570C
		public bool Push(ControllerColliderHit cchit)
		{
			if (this.issuingEvent)
			{
				Debug.LogError("Push during event call back");
				return false;
			}
			if (!object.ReferenceEquals(cchit, null))
			{
				CCDesc.Hit hit = new CCDesc.Hit(cchit);
				return this.Push(ref hit);
			}
			return false;
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x00057550 File Offset: 0x00055750
		public bool Push(ref CCDesc.Hit evnt)
		{
			if (this.issuingEvent)
			{
				Debug.LogError("Push during event call back");
				return false;
			}
			CCDesc.HitFilter onHit = this.OnHit;
			if (onHit != null)
			{
				bool flag = false;
				try
				{
					this.issuingEvent = true;
					flag = !onHit(this, ref evnt);
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
				}
				finally
				{
					this.issuingEvent = false;
				}
				if (flag)
				{
					return false;
				}
			}
			int num = this.filledCount++;
			if (this.filledCount > this.bufferSize)
			{
				do
				{
					this.bufferSize += 8;
				}
				while (this.filledCount > this.bufferSize);
				if (this.filledCount > 1)
				{
					CCDesc.Hit[] sourceArray = this.buffer;
					this.buffer = new CCDesc.Hit[this.bufferSize];
					Array.Copy(sourceArray, this.buffer, this.filledCount - 1);
				}
				else
				{
					this.buffer = new CCDesc.Hit[this.bufferSize];
				}
			}
			this.buffer[num] = evnt;
			return true;
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x00057698 File Offset: 0x00055898
		public void Clear()
		{
			while (this.filledCount > 0)
			{
				this.buffer[--this.filledCount] = default(CCDesc.Hit);
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x060016FC RID: 5884 RVA: 0x000576E0 File Offset: 0x000558E0
		public int Count
		{
			get
			{
				return this.filledCount;
			}
		}

		// Token: 0x170006B0 RID: 1712
		public CCDesc.Hit this[int i]
		{
			get
			{
				if (i < 0 || i >= this.filledCount)
				{
					throw new ArgumentOutOfRangeException("i");
				}
				return this.buffer[i];
			}
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x0005771C File Offset: 0x0005591C
		public void Dispose()
		{
			this.buffer = null;
			this.OnHit = null;
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x0005772C File Offset: 0x0005592C
		public void CopyTo(CCDesc.Hit[] array, int startIndex = 0)
		{
			for (int i = 0; i < this.filledCount; i++)
			{
				array[startIndex++] = this.buffer[i];
			}
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x00057774 File Offset: 0x00055974
		public CCDesc.Hit[] ToArray()
		{
			CCDesc.Hit[] array = new CCDesc.Hit[this.filledCount];
			this.CopyTo(array, 0);
			return array;
		}

		// Token: 0x04000BB7 RID: 2999
		private CCDesc.Hit[] buffer;

		// Token: 0x04000BB8 RID: 3000
		private int bufferSize;

		// Token: 0x04000BB9 RID: 3001
		private int filledCount;

		// Token: 0x04000BBA RID: 3002
		private bool issuingEvent;

		// Token: 0x04000BBB RID: 3003
		public object Tag;
	}

	// Token: 0x02000869 RID: 2153
	// (Invoke) Token: 0x06004B7C RID: 19324
	public delegate bool HitFilter(CCDesc.HitManager hitManager, ref CCDesc.Hit hit);
}
