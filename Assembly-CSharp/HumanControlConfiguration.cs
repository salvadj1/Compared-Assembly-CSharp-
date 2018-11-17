using System;
using UnityEngine;

// Token: 0x02000170 RID: 368
public class HumanControlConfiguration : global::ControlConfiguration
{
	// Token: 0x170002B0 RID: 688
	// (get) Token: 0x06000A72 RID: 2674 RVA: 0x0002B554 File Offset: 0x00029754
	public AnimationCurve curveSprintAddSpeedByTime
	{
		get
		{
			return this.sprintAddSpeedByTime;
		}
	}

	// Token: 0x170002B1 RID: 689
	// (get) Token: 0x06000A73 RID: 2675 RVA: 0x0002B55C File Offset: 0x0002975C
	public AnimationCurve curveCrouchMulSpeedByTime
	{
		get
		{
			return this.crouchMulSpeedByTime;
		}
	}

	// Token: 0x170002B2 RID: 690
	// (get) Token: 0x06000A74 RID: 2676 RVA: 0x0002B564 File Offset: 0x00029764
	public AnimationCurve curveLandingSpeedPenalty
	{
		get
		{
			return this.landingSpeedPenalty;
		}
	}

	// Token: 0x170002B3 RID: 691
	// (get) Token: 0x06000A75 RID: 2677 RVA: 0x0002B56C File Offset: 0x0002976C
	public float sprintScaleX
	{
		get
		{
			return this.sprintScalars.x;
		}
	}

	// Token: 0x170002B4 RID: 692
	// (get) Token: 0x06000A76 RID: 2678 RVA: 0x0002B57C File Offset: 0x0002977C
	public float sprintScaleY
	{
		get
		{
			return this.sprintScalars.y;
		}
	}

	// Token: 0x170002B5 RID: 693
	// (get) Token: 0x06000A77 RID: 2679 RVA: 0x0002B58C File Offset: 0x0002978C
	public Vector2 sprintScale
	{
		get
		{
			return this.sprintScalars;
		}
	}

	// Token: 0x04000789 RID: 1929
	[SerializeField]
	private AnimationCurve sprintAddSpeedByTime = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f, 0f, 0f),
		new Keyframe(0.4f, 1f, 0f, 0f)
	});

	// Token: 0x0400078A RID: 1930
	[SerializeField]
	private AnimationCurve crouchMulSpeedByTime = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 1f, 0f, 0f),
		new Keyframe(0.4f, 0.55f, 0f, 0f)
	});

	// Token: 0x0400078B RID: 1931
	[SerializeField]
	private AnimationCurve landingSpeedPenalty = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 1f, 0f, 0f),
		new Keyframe(0.25f, 0.5f, -2f, -2f),
		new Keyframe(0.75f, 1f, 0f, 0f)
	});

	// Token: 0x0400078C RID: 1932
	[SerializeField]
	private Vector2 sprintScalars = new Vector2(0.2f, 1f);
}
