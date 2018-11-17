using System;
using UnityEngine;

// Token: 0x020006D5 RID: 1749
public class LightStyleDefault : global::LightStyle
{
	// Token: 0x06003B44 RID: 15172 RVA: 0x000D1AA8 File Offset: 0x000CFCA8
	private void OnEnable()
	{
		global::LightStyleDefault.singleton = this;
	}

	// Token: 0x06003B45 RID: 15173 RVA: 0x000D1AB0 File Offset: 0x000CFCB0
	private void OnDisable()
	{
		if (global::LightStyleDefault.singleton == this)
		{
			global::LightStyleDefault.singleton = null;
		}
	}

	// Token: 0x06003B46 RID: 15174 RVA: 0x000D1AC8 File Offset: 0x000CFCC8
	protected override global::LightStyle.Simulation ConstructSimulation(global::LightStylist stylist)
	{
		global::LightStyleDefault.DefaultSimulation result;
		if ((result = this.singletonSimulation) == null)
		{
			result = (this.singletonSimulation = new global::LightStyleDefault.DefaultSimulation(this));
		}
		return result;
	}

	// Token: 0x06003B47 RID: 15175 RVA: 0x000D1AF4 File Offset: 0x000CFCF4
	protected override bool DeconstructSimulation(global::LightStyle.Simulation simulation)
	{
		return false;
	}

	// Token: 0x17000B74 RID: 2932
	// (get) Token: 0x06003B48 RID: 15176 RVA: 0x000D1AF8 File Offset: 0x000CFCF8
	public static global::LightStyleDefault Singleton
	{
		get
		{
			if (global::LightStyleDefault.singleton)
			{
				return global::LightStyleDefault.singleton;
			}
			return ScriptableObject.CreateInstance<global::LightStyleDefault>();
		}
	}

	// Token: 0x04001D7B RID: 7547
	private static global::LightStyleDefault singleton;

	// Token: 0x04001D7C RID: 7548
	private global::LightStyleDefault.DefaultSimulation singletonSimulation;

	// Token: 0x020006D6 RID: 1750
	private class DefaultSimulation : global::LightStyle.Simulation
	{
		// Token: 0x06003B49 RID: 15177 RVA: 0x000D1B14 File Offset: 0x000CFD14
		public DefaultSimulation(global::LightStyleDefault def) : base(def)
		{
			float? value = new float?(1f);
			for (global::LightStyle.Mod.Element element = global::LightStyle.Mod.Element.Red; element < (global::LightStyle.Mod.Element)7; element++)
			{
				this.mod[element] = value;
			}
		}

		// Token: 0x06003B4A RID: 15178 RVA: 0x000D1B54 File Offset: 0x000CFD54
		protected override void Simulate(double currentTime)
		{
		}
	}
}
