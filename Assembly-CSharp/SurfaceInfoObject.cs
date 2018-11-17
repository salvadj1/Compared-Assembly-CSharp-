using System;
using Facepunch;
using UnityEngine;

// Token: 0x02000560 RID: 1376
public class SurfaceInfoObject : ScriptableObject
{
	// Token: 0x06002DA1 RID: 11681 RVA: 0x000ABE20 File Offset: 0x000AA020
	public GameObject GetImpactEffect(global::SurfaceInfoObject.ImpactType type)
	{
		if (type == global::SurfaceInfoObject.ImpactType.Bullet)
		{
			return this.bulletEffects[Random.Range(0, this.bulletEffects.Length)];
		}
		if (type == global::SurfaceInfoObject.ImpactType.Melee)
		{
			return this.meleeEffects[Random.Range(0, this.meleeEffects.Length)];
		}
		return null;
	}

	// Token: 0x06002DA2 RID: 11682 RVA: 0x000ABE68 File Offset: 0x000AA068
	public AudioClip GetFootstepBiped(AudioClip last)
	{
		int num = Random.Range(0, this.bipedFootsteps.Length);
		AudioClip audioClip = this.bipedFootsteps[num];
		if (last && audioClip == last)
		{
			if (num < this.bipedFootsteps.Length - 1)
			{
				num++;
			}
			else if (num >= 1)
			{
				num--;
			}
			audioClip = this.bipedFootsteps[num];
		}
		return this.bipedFootsteps[num];
	}

	// Token: 0x06002DA3 RID: 11683 RVA: 0x000ABEEC File Offset: 0x000AA0EC
	public AudioClip GetFootstepBiped()
	{
		return this.bipedFootsteps[Random.Range(0, this.bipedFootsteps.Length)];
	}

	// Token: 0x06002DA4 RID: 11684 RVA: 0x000ABF0C File Offset: 0x000AA10C
	public AudioClip GetFootstepAnimal()
	{
		return this.animalFootsteps[Random.Range(0, this.animalFootsteps.Length)];
	}

	// Token: 0x06002DA5 RID: 11685 RVA: 0x000ABF2C File Offset: 0x000AA12C
	public static global::SurfaceInfoObject GetDefault()
	{
		if (global::SurfaceInfoObject._default == null)
		{
			Facepunch.Bundling.Load<global::SurfaceInfoObject>("rust/effects/impact/default", out global::SurfaceInfoObject._default);
			if (global::SurfaceInfoObject._default == null)
			{
				Debug.Log("COULD NOT GET DEFAULT!");
			}
		}
		return global::SurfaceInfoObject._default;
	}

	// Token: 0x0400178F RID: 6031
	public static global::SurfaceInfoObject _default;

	// Token: 0x04001790 RID: 6032
	public GameObject[] bulletEffects;

	// Token: 0x04001791 RID: 6033
	public GameObject[] meleeEffects;

	// Token: 0x04001792 RID: 6034
	public global::AudioClipArray bipedFootsteps;

	// Token: 0x04001793 RID: 6035
	public global::AudioClipArray animalFootsteps;

	// Token: 0x02000561 RID: 1377
	public enum ImpactType
	{
		// Token: 0x04001795 RID: 6037
		Melee,
		// Token: 0x04001796 RID: 6038
		Bullet
	}
}
