using System;
using UnityEngine;

// Token: 0x020006D3 RID: 1747
public class LightStyleCurve : global::LightStyle
{
	// Token: 0x06003B3E RID: 15166 RVA: 0x000D19EC File Offset: 0x000CFBEC
	private float GetCurveValue(double relativeStartTime)
	{
		return this.curve.Evaluate((float)relativeStartTime);
	}

	// Token: 0x06003B3F RID: 15167 RVA: 0x000D19FC File Offset: 0x000CFBFC
	protected override global::LightStyle.Simulation ConstructSimulation(global::LightStylist stylist)
	{
		return new global::LightStyleCurve.Simulation(this);
	}

	// Token: 0x06003B40 RID: 15168 RVA: 0x000D1A04 File Offset: 0x000CFC04
	protected override bool DeconstructSimulation(global::LightStyle.Simulation simulation)
	{
		return true;
	}

	// Token: 0x04001D79 RID: 7545
	[SerializeField]
	private AnimationCurve curve;

	// Token: 0x020006D4 RID: 1748
	protected new class Simulation : global::LightStyle.Simulation<global::LightStyleCurve>
	{
		// Token: 0x06003B41 RID: 15169 RVA: 0x000D1A08 File Offset: 0x000CFC08
		public Simulation(global::LightStyleCurve creator) : base(creator)
		{
			this.lastValue = null;
		}

		// Token: 0x06003B42 RID: 15170 RVA: 0x000D1A2C File Offset: 0x000CFC2C
		protected override void Simulate(double currentTime)
		{
			float curveValue = base.creator.GetCurveValue(currentTime - this.startTime);
			if (this.lastValue == null || this.lastValue.Value != curveValue)
			{
				this.lastValue = new float?(curveValue);
				for (global::LightStyle.Mod.Element element = global::LightStyle.Mod.Element.Red; element < (global::LightStyle.Mod.Element)7; element++)
				{
					this.mod[element] = this.lastValue;
				}
			}
		}

		// Token: 0x04001D7A RID: 7546
		private float? lastValue;
	}
}
