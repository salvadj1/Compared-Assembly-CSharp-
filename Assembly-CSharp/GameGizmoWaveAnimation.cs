using System;
using UnityEngine;

// Token: 0x020004B0 RID: 1200
public class GameGizmoWaveAnimation : GameGizmo
{
	// Token: 0x06002A2A RID: 10794 RVA: 0x000A52A8 File Offset: 0x000A34A8
	protected override GameGizmo.Instance ConstructInstance()
	{
		return new GameGizmoWaveAnimation.Instance(this);
	}

	// Token: 0x04001616 RID: 5654
	[SerializeField]
	protected float frequency = 1.2566371f;

	// Token: 0x04001617 RID: 5655
	[SerializeField]
	protected float amplitudePositive = 0.15f;

	// Token: 0x04001618 RID: 5656
	[SerializeField]
	protected float amplitudeNegative = -0.1f;

	// Token: 0x04001619 RID: 5657
	[SerializeField]
	protected float phase;

	// Token: 0x0400161A RID: 5658
	[SerializeField]
	protected Vector3 axis = Vector3.up;

	// Token: 0x020004B1 RID: 1201
	public new class Instance : GameGizmo.Instance
	{
		// Token: 0x06002A2B RID: 10795 RVA: 0x000A52B0 File Offset: 0x000A34B0
		protected internal Instance(GameGizmoWaveAnimation gameGizmo) : base(gameGizmo)
		{
			this.Frequency = (double)gameGizmo.frequency;
			this.Phase = (double)gameGizmo.phase;
			this.AmplitudePositive = (double)gameGizmo.amplitudePositive;
			this.AmplitudeNegative = (double)gameGizmo.amplitudeNegative;
			this.Axis = gameGizmo.axis;
		}

		// Token: 0x06002A2C RID: 10796 RVA: 0x000A5304 File Offset: 0x000A3504
		protected override void Render(bool useCamera, Camera camera)
		{
			ulong localTimeInMillis = NetCull.localTimeInMillis;
			if (!this.Started || this.lastRenderTime >= localTimeInMillis)
			{
				this.Started = true;
			}
			else
			{
				ulong num = localTimeInMillis - this.lastRenderTime;
				double num2 = num * 0.001;
				double num3 = 1000.0 / num;
				this.Phase += this.Frequency * num2;
			}
			this.lastRenderTime = localTimeInMillis;
			if (this.Phase > 1.0)
			{
				if (double.IsPositiveInfinity(this.Phase))
				{
					this.Phase = 0.0;
				}
				else
				{
					this.Phase %= 1.0;
				}
			}
			else if (this.Phase == 1.0)
			{
				this.Phase = 0.0;
			}
			else if (this.Phase < 0.0)
			{
				if (double.IsNegativeInfinity(this.Phase))
				{
					this.Phase = 0.0;
				}
				else
				{
					this.Phase = -this.Phase % 1.0;
					if (this.Phase > 0.0)
					{
						this.Phase = 1.0 - this.Phase;
					}
				}
			}
			double num4;
			if (this.Phase < 0.5)
			{
				this.arcSine = this.Phase * 6.2831853071795862;
				num4 = this.AmplitudePositive;
			}
			else
			{
				this.arcSine = (this.Phase * 2.0 - 2.0) * 3.1415926535897931;
				num4 = this.AmplitudeNegative;
			}
			this.sine = Math.Sin(this.arcSine);
			this.value = this.sine * num4;
			Vector3 vector;
			vector.x = (float)((double)this.Axis.x * this.value);
			vector.y = (float)((double)this.Axis.y * this.value);
			vector.z = (float)((double)this.Axis.z * this.value);
			Matrix4x4? ultimateMatrix = this.ultimateMatrix;
			Matrix4x4 matrix4x;
			if (ultimateMatrix != null)
			{
				matrix4x = ultimateMatrix.Value;
			}
			else
			{
				Matrix4x4? overrideMatrix = this.overrideMatrix;
				matrix4x = ((overrideMatrix == null) ? base.DefaultMatrix() : overrideMatrix.Value);
			}
			this.ultimateMatrix = new Matrix4x4?(matrix4x * Matrix4x4.TRS(vector, Quaternion.identity, Vector3.one));
			base.Render(useCamera, camera);
			this.ultimateMatrix = ultimateMatrix;
		}

		// Token: 0x0400161B RID: 5659
		public double Phase;

		// Token: 0x0400161C RID: 5660
		public double Frequency;

		// Token: 0x0400161D RID: 5661
		public double AmplitudePositive;

		// Token: 0x0400161E RID: 5662
		public double AmplitudeNegative;

		// Token: 0x0400161F RID: 5663
		public double value;

		// Token: 0x04001620 RID: 5664
		public double arcSine;

		// Token: 0x04001621 RID: 5665
		public double sine;

		// Token: 0x04001622 RID: 5666
		public Vector3 Axis;

		// Token: 0x04001623 RID: 5667
		public bool Started;

		// Token: 0x04001624 RID: 5668
		private ulong lastRenderTime;
	}
}
