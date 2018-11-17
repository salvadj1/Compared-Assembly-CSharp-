using System;
using UnityEngine;

// Token: 0x02000146 RID: 326
public class HumanControlConfiguration : ControlConfiguration
{
	// Token: 0x1700026E RID: 622
	// (get) Token: 0x0600094C RID: 2380 RVA: 0x000277D8 File Offset: 0x000259D8
	public AnimationCurve curveSprintAddSpeedByTime
	{
		get
		{
			return this.sprintAddSpeedByTime;
		}
	}

	// Token: 0x1700026F RID: 623
	// (get) Token: 0x0600094D RID: 2381 RVA: 0x000277E0 File Offset: 0x000259E0
	public AnimationCurve curveCrouchMulSpeedByTime
	{
		get
		{
			return this.crouchMulSpeedByTime;
		}
	}

	// Token: 0x17000270 RID: 624
	// (get) Token: 0x0600094E RID: 2382 RVA: 0x000277E8 File Offset: 0x000259E8
	public AnimationCurve curveLandingSpeedPenalty
	{
		get
		{
			return this.landingSpeedPenalty;
		}
	}

	// Token: 0x17000271 RID: 625
	// (get) Token: 0x0600094F RID: 2383 RVA: 0x000277F0 File Offset: 0x000259F0
	public float sprintScaleX
	{
		get
		{
			return this.sprintScalars.x;
		}
	}

	// Token: 0x17000272 RID: 626
	// (get) Token: 0x06000950 RID: 2384 RVA: 0x00027800 File Offset: 0x00025A00
	public float sprintScaleY
	{
		get
		{
			return this.sprintScalars.y;
		}
	}

	// Token: 0x17000273 RID: 627
	// (get) Token: 0x06000951 RID: 2385 RVA: 0x00027810 File Offset: 0x00025A10
	public Vector2 sprintScale
	{
		get
		{
			return this.sprintScalars;
		}
	}

	// Token: 0x0400067A RID: 1658
	[SerializeField]
	private AnimationCurve sprintAddSpeedByTime = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f, 0f, 0f),
		new Keyframe(0.4f, 1f, 0f, 0f)
	});

	// Token: 0x0400067B RID: 1659
	[SerializeField]
	private AnimationCurve crouchMulSpeedByTime = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 1f, 0f, 0f),
		new Keyframe(0.4f, 0.55f, 0f, 0f)
	});

	// Token: 0x0400067C RID: 1660
	[SerializeField]
	private AnimationCurve landingSpeedPenalty = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 1f, 0f, 0f),
		new Keyframe(0.25f, 0.5f, -2f, -2f),
		new Keyframe(0.75f, 1f, 0f, 0f)
	});

	// Token: 0x0400067D RID: 1661
	[SerializeField]
	private Vector2 sprintScalars = new Vector2(0.2f, 1f);
}
