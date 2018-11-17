using System;
using UnityEngine;

// Token: 0x02000615 RID: 1557
public class LightStyleDefault : LightStyle
{
	// Token: 0x0600376C RID: 14188 RVA: 0x000C9578 File Offset: 0x000C7778
	private void OnEnable()
	{
		LightStyleDefault.singleton = this;
	}

	// Token: 0x0600376D RID: 14189 RVA: 0x000C9580 File Offset: 0x000C7780
	private void OnDisable()
	{
		if (LightStyleDefault.singleton == this)
		{
			LightStyleDefault.singleton = null;
		}
	}

	// Token: 0x0600376E RID: 14190 RVA: 0x000C9598 File Offset: 0x000C7798
	protected override LightStyle.Simulation ConstructSimulation(LightStylist stylist)
	{
		LightStyleDefault.DefaultSimulation result;
		if ((result = this.singletonSimulation) == null)
		{
			result = (this.singletonSimulation = new LightStyleDefault.DefaultSimulation(this));
		}
		return result;
	}

	// Token: 0x0600376F RID: 14191 RVA: 0x000C95C4 File Offset: 0x000C77C4
	protected override bool DeconstructSimulation(LightStyle.Simulation simulation)
	{
		return false;
	}

	// Token: 0x17000AFA RID: 2810
	// (get) Token: 0x06003770 RID: 14192 RVA: 0x000C95C8 File Offset: 0x000C77C8
	public static LightStyleDefault Singleton
	{
		get
		{
			if (LightStyleDefault.singleton)
			{
				return LightStyleDefault.singleton;
			}
			return ScriptableObject.CreateInstance<LightStyleDefault>();
		}
	}

	// Token: 0x04001B95 RID: 7061
	private static LightStyleDefault singleton;

	// Token: 0x04001B96 RID: 7062
	private LightStyleDefault.DefaultSimulation singletonSimulation;

	// Token: 0x02000616 RID: 1558
	private class DefaultSimulation : LightStyle.Simulation
	{
		// Token: 0x06003771 RID: 14193 RVA: 0x000C95E4 File Offset: 0x000C77E4
		public DefaultSimulation(LightStyleDefault def) : base(def)
		{
			float? value = new float?(1f);
			for (LightStyle.Mod.Element element = LightStyle.Mod.Element.Red; element < (LightStyle.Mod.Element)7; element++)
			{
				this.mod[element] = value;
			}
		}

		// Token: 0x06003772 RID: 14194 RVA: 0x000C9624 File Offset: 0x000C7824
		protected override void Simulate(double currentTime)
		{
		}
	}
}
