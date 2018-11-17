using System;
using UnityEngine;

// Token: 0x02000613 RID: 1555
public class LightStyleCurve : LightStyle
{
	// Token: 0x06003766 RID: 14182 RVA: 0x000C94BC File Offset: 0x000C76BC
	private float GetCurveValue(double relativeStartTime)
	{
		return this.curve.Evaluate((float)relativeStartTime);
	}

	// Token: 0x06003767 RID: 14183 RVA: 0x000C94CC File Offset: 0x000C76CC
	protected override LightStyle.Simulation ConstructSimulation(LightStylist stylist)
	{
		return new LightStyleCurve.Simulation(this);
	}

	// Token: 0x06003768 RID: 14184 RVA: 0x000C94D4 File Offset: 0x000C76D4
	protected override bool DeconstructSimulation(LightStyle.Simulation simulation)
	{
		return true;
	}

	// Token: 0x04001B93 RID: 7059
	[SerializeField]
	private AnimationCurve curve;

	// Token: 0x02000614 RID: 1556
	protected new class Simulation : LightStyle.Simulation<LightStyleCurve>
	{
		// Token: 0x06003769 RID: 14185 RVA: 0x000C94D8 File Offset: 0x000C76D8
		public Simulation(LightStyleCurve creator) : base(creator)
		{
			this.lastValue = null;
		}

		// Token: 0x0600376A RID: 14186 RVA: 0x000C94FC File Offset: 0x000C76FC
		protected override void Simulate(double currentTime)
		{
			float curveValue = base.creator.GetCurveValue(currentTime - this.startTime);
			if (this.lastValue == null || this.lastValue.Value != curveValue)
			{
				this.lastValue = new float?(curveValue);
				for (LightStyle.Mod.Element element = LightStyle.Mod.Element.Red; element < (LightStyle.Mod.Element)7; element++)
				{
					this.mod[element] = this.lastValue;
				}
			}
		}

		// Token: 0x04001B94 RID: 7060
		private float? lastValue;
	}
}
