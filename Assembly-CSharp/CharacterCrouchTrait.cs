using System;
using UnityEngine;

// Token: 0x02000101 RID: 257
public class CharacterCrouchTrait : CharacterTrait
{
	// Token: 0x17000156 RID: 342
	// (get) Token: 0x06000699 RID: 1689 RVA: 0x0001E6DC File Offset: 0x0001C8DC
	public AnimationCurve crouchCurve
	{
		get
		{
			return this._crouchCurve;
		}
	}

	// Token: 0x17000157 RID: 343
	// (get) Token: 0x0600069A RID: 1690 RVA: 0x0001E6E4 File Offset: 0x0001C8E4
	public float crouchToSpeedFraction
	{
		get
		{
			return this._crouchToSpeedFraction;
		}
	}

	// Token: 0x17000158 RID: 344
	// (get) Token: 0x0600069B RID: 1691 RVA: 0x0001E6EC File Offset: 0x0001C8EC
	private float crouchSpeedBase
	{
		get
		{
			Keyframe keyframe = this._crouchCurve[0];
			Keyframe keyframe2 = this._crouchCurve[this._crouchCurve.length - 1];
			float num = keyframe2.value - keyframe.value;
			float num2 = keyframe2.time - keyframe.time;
			return num / num2;
		}
	}

	// Token: 0x17000159 RID: 345
	// (get) Token: 0x0600069C RID: 1692 RVA: 0x0001E744 File Offset: 0x0001C944
	public float crouchOutSpeed
	{
		get
		{
			return Mathf.Abs(this.crouchSpeedBase);
		}
	}

	// Token: 0x1700015A RID: 346
	// (get) Token: 0x0600069D RID: 1693 RVA: 0x0001E754 File Offset: 0x0001C954
	public float crouchInSpeed
	{
		get
		{
			return -Mathf.Abs(this.crouchSpeedBase * this._crouchToSpeedFraction);
		}
	}

	// Token: 0x0600069E RID: 1694 RVA: 0x0001E76C File Offset: 0x0001C96C
	public bool IsCrouching(float minHeight, float maxHeight, float currentHeight)
	{
		return Mathf.InverseLerp(minHeight, maxHeight, currentHeight) <= this._maxCrouchFraction;
	}

	// Token: 0x040004F5 RID: 1269
	[SerializeField]
	private AnimationCurve _crouchCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f, 0f, 0f),
		new Keyframe(0.55f, -0.55f, 0f, 0f)
	});

	// Token: 0x040004F6 RID: 1270
	[SerializeField]
	private float _crouchToSpeedFraction = 1.3f;

	// Token: 0x040004F7 RID: 1271
	[SerializeField]
	private float _maxCrouchFraction = 0.9f;
}
