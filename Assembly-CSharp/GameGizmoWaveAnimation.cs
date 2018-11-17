using System;
using UnityEngine;

// Token: 0x0200056B RID: 1387
public class GameGizmoWaveAnimation : global::GameGizmo
{
	// Token: 0x06002DDC RID: 11740 RVA: 0x000AD040 File Offset: 0x000AB240
	protected override global::GameGizmo.Instance ConstructInstance()
	{
		return new global::GameGizmoWaveAnimation.Instance(this);
	}

	// Token: 0x040017D3 RID: 6099
	[SerializeField]
	protected float frequency = 1.2566371f;

	// Token: 0x040017D4 RID: 6100
	[SerializeField]
	protected float amplitudePositive = 0.15f;

	// Token: 0x040017D5 RID: 6101
	[SerializeField]
	protected float amplitudeNegative = -0.1f;

	// Token: 0x040017D6 RID: 6102
	[SerializeField]
	protected float phase;

	// Token: 0x040017D7 RID: 6103
	[SerializeField]
	protected Vector3 axis = Vector3.up;

	// Token: 0x0200056C RID: 1388
	public new class Instance : global::GameGizmo.Instance
	{
		// Token: 0x06002DDD RID: 11741 RVA: 0x000AD048 File Offset: 0x000AB248
		protected internal Instance(global::GameGizmoWaveAnimation gameGizmo) : base(gameGizmo)
		{
			this.Frequency = (double)gameGizmo.frequency;
			this.Phase = (double)gameGizmo.phase;
			this.AmplitudePositive = (double)gameGizmo.amplitudePositive;
			this.AmplitudeNegative = (double)gameGizmo.amplitudeNegative;
			this.Axis = gameGizmo.axis;
		}

		// Token: 0x06002DDE RID: 11742 RVA: 0x000AD09C File Offset: 0x000AB29C
		protected override void Render(bool useCamera, Camera camera)
		{
			ulong localTimeInMillis = global::NetCull.localTimeInMillis;
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

		// Token: 0x040017D8 RID: 6104
		public double Phase;

		// Token: 0x040017D9 RID: 6105
		public double Frequency;

		// Token: 0x040017DA RID: 6106
		public double AmplitudePositive;

		// Token: 0x040017DB RID: 6107
		public double AmplitudeNegative;

		// Token: 0x040017DC RID: 6108
		public double value;

		// Token: 0x040017DD RID: 6109
		public double arcSine;

		// Token: 0x040017DE RID: 6110
		public double sine;

		// Token: 0x040017DF RID: 6111
		public Vector3 Axis;

		// Token: 0x040017E0 RID: 6112
		public bool Started;

		// Token: 0x040017E1 RID: 6113
		private ulong lastRenderTime;
	}
}
