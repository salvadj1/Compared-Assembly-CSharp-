using System;
using Facepunch;
using UnityEngine;

// Token: 0x020004A5 RID: 1189
public class SurfaceInfoObject : ScriptableObject
{
	// Token: 0x060029EF RID: 10735 RVA: 0x000A4088 File Offset: 0x000A2288
	public GameObject GetImpactEffect(SurfaceInfoObject.ImpactType type)
	{
		if (type == SurfaceInfoObject.ImpactType.Bullet)
		{
			return this.bulletEffects[Random.Range(0, this.bulletEffects.Length)];
		}
		if (type == SurfaceInfoObject.ImpactType.Melee)
		{
			return this.meleeEffects[Random.Range(0, this.meleeEffects.Length)];
		}
		return null;
	}

	// Token: 0x060029F0 RID: 10736 RVA: 0x000A40D0 File Offset: 0x000A22D0
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

	// Token: 0x060029F1 RID: 10737 RVA: 0x000A4154 File Offset: 0x000A2354
	public AudioClip GetFootstepBiped()
	{
		return this.bipedFootsteps[Random.Range(0, this.bipedFootsteps.Length)];
	}

	// Token: 0x060029F2 RID: 10738 RVA: 0x000A4174 File Offset: 0x000A2374
	public AudioClip GetFootstepAnimal()
	{
		return this.animalFootsteps[Random.Range(0, this.animalFootsteps.Length)];
	}

	// Token: 0x060029F3 RID: 10739 RVA: 0x000A4194 File Offset: 0x000A2394
	public static SurfaceInfoObject GetDefault()
	{
		if (SurfaceInfoObject._default == null)
		{
			Bundling.Load<SurfaceInfoObject>("rust/effects/impact/default", out SurfaceInfoObject._default);
			if (SurfaceInfoObject._default == null)
			{
				Debug.Log("COULD NOT GET DEFAULT!");
			}
		}
		return SurfaceInfoObject._default;
	}

	// Token: 0x040015D2 RID: 5586
	public static SurfaceInfoObject _default;

	// Token: 0x040015D3 RID: 5587
	public GameObject[] bulletEffects;

	// Token: 0x040015D4 RID: 5588
	public GameObject[] meleeEffects;

	// Token: 0x040015D5 RID: 5589
	public AudioClipArray bipedFootsteps;

	// Token: 0x040015D6 RID: 5590
	public AudioClipArray animalFootsteps;

	// Token: 0x020004A6 RID: 1190
	public enum ImpactType
	{
		// Token: 0x040015D8 RID: 5592
		Melee,
		// Token: 0x040015D9 RID: 5593
		Bullet
	}
}
