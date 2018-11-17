using System;
using UnityEngine;

// Token: 0x02000120 RID: 288
public class CharacterCrouchTrait : global::CharacterTrait
{
	// Token: 0x17000184 RID: 388
	// (get) Token: 0x0600076B RID: 1899 RVA: 0x000212B0 File Offset: 0x0001F4B0
	public AnimationCurve crouchCurve
	{
		get
		{
			return this._crouchCurve;
		}
	}

	// Token: 0x17000185 RID: 389
	// (get) Token: 0x0600076C RID: 1900 RVA: 0x000212B8 File Offset: 0x0001F4B8
	public float crouchToSpeedFraction
	{
		get
		{
			return this._crouchToSpeedFraction;
		}
	}

	// Token: 0x17000186 RID: 390
	// (get) Token: 0x0600076D RID: 1901 RVA: 0x000212C0 File Offset: 0x0001F4C0
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

	// Token: 0x17000187 RID: 391
	// (get) Token: 0x0600076E RID: 1902 RVA: 0x00021318 File Offset: 0x0001F518
	public float crouchOutSpeed
	{
		get
		{
			return Mathf.Abs(this.crouchSpeedBase);
		}
	}

	// Token: 0x17000188 RID: 392
	// (get) Token: 0x0600076F RID: 1903 RVA: 0x00021328 File Offset: 0x0001F528
	public float crouchInSpeed
	{
		get
		{
			return -Mathf.Abs(this.crouchSpeedBase * this._crouchToSpeedFraction);
		}
	}

	// Token: 0x06000770 RID: 1904 RVA: 0x00021340 File Offset: 0x0001F540
	public bool IsCrouching(float minHeight, float maxHeight, float currentHeight)
	{
		return Mathf.InverseLerp(minHeight, maxHeight, currentHeight) <= this._maxCrouchFraction;
	}

	// Token: 0x040005C0 RID: 1472
	[SerializeField]
	private AnimationCurve _crouchCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f, 0f, 0f),
		new Keyframe(0.55f, -0.55f, 0f, 0f)
	});

	// Token: 0x040005C1 RID: 1473
	[SerializeField]
	private float _crouchToSpeedFraction = 1.3f;

	// Token: 0x040005C2 RID: 1474
	[SerializeField]
	private float _maxCrouchFraction = 0.9f;
}
