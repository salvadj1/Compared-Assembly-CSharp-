using System;
using Facepunch;
using UnityEngine;

// Token: 0x020000B9 RID: 185
public sealed class LocalRadiationEffect : global::IDLocalCharacterAddon
{
	// Token: 0x06000411 RID: 1041 RVA: 0x00013EB8 File Offset: 0x000120B8
	public LocalRadiationEffect() : base(global::IDLocalCharacterAddon.AddonFlags.PrerequisitCheck)
	{
	}

	// Token: 0x06000413 RID: 1043 RVA: 0x00013EC8 File Offset: 0x000120C8
	protected override bool CheckPrerequesits()
	{
		this.radiation = base.GetComponent<global::Radiation>();
		return this.radiation;
	}

	// Token: 0x06000414 RID: 1044 RVA: 0x00013EE4 File Offset: 0x000120E4
	private void OnDestroy()
	{
		if (global::LocalRadiationEffect.geigerSoundPlayer)
		{
			Object.Destroy(global::LocalRadiationEffect.geigerSoundPlayer);
		}
	}

	// Token: 0x06000415 RID: 1045 RVA: 0x00013F00 File Offset: 0x00012100
	private void Update()
	{
		if (base.dead)
		{
			return;
		}
		float num;
		float num2;
		float num3;
		if (this.radiation)
		{
			float exposure = this.radiation.CalculateExposure(true);
			num = this.radiation.CalculateExposure(false);
			num2 = this.radiation.GetRadExposureScalar(exposure);
			num3 = this.radiation.GetRadExposureScalar(num);
		}
		else
		{
			num2 = 0f;
			num3 = 0f;
			num = 0f;
		}
		global::ImageEffectManager.GetInstance<NoiseAndGrain>().intensityMultiplier = 10f * num2;
		if (global::LocalRadiationEffect.geiger0 == null)
		{
			Facepunch.Bundling.Load<AudioClip>("content/item/sfx/geiger_low", out global::LocalRadiationEffect.geiger0);
			Facepunch.Bundling.Load<AudioClip>("content/item/sfx/geiger_medium", out global::LocalRadiationEffect.geiger1);
			Facepunch.Bundling.Load<AudioClip>("content/item/sfx/geiger_high", out global::LocalRadiationEffect.geiger2);
			Facepunch.Bundling.Load<AudioClip>("content/item/sfx/geiger_ultra", out global::LocalRadiationEffect.geiger3);
		}
		if (num >= 0.02f)
		{
			if (!global::LocalRadiationEffect.geigerSoundPlayer)
			{
				global::LocalRadiationEffect.geigerSoundPlayer = new GameObject("GEIGER SOUNDS", new Type[]
				{
					typeof(AudioSource)
				});
				global::LocalRadiationEffect.geigerSoundPlayer.transform.position = base.transform.position;
				global::LocalRadiationEffect.geigerSoundPlayer.transform.parent = base.transform;
				global::LocalRadiationEffect.geigerSoundPlayer.audio.loop = true;
			}
			AudioClip audioClip;
			if (num3 <= 0.25f)
			{
				audioClip = global::LocalRadiationEffect.geiger0;
			}
			else if (num3 <= 0.5f)
			{
				audioClip = global::LocalRadiationEffect.geiger1;
			}
			else if (num3 <= 0.75f)
			{
				audioClip = global::LocalRadiationEffect.geiger2;
			}
			else
			{
				audioClip = global::LocalRadiationEffect.geiger3;
			}
			if (audioClip != global::LocalRadiationEffect.geigerSoundPlayer.audio.clip)
			{
				global::LocalRadiationEffect.geigerSoundPlayer.audio.Stop();
				global::LocalRadiationEffect.geigerSoundPlayer.audio.clip = audioClip;
				global::LocalRadiationEffect.geigerSoundPlayer.audio.Play();
			}
		}
		else if (global::LocalRadiationEffect.geigerSoundPlayer)
		{
			global::LocalRadiationEffect.geigerSoundPlayer.audio.Stop();
			Object.Destroy(global::LocalRadiationEffect.geigerSoundPlayer);
			global::LocalRadiationEffect.geigerSoundPlayer = null;
		}
	}

	// Token: 0x0400037A RID: 890
	[NonSerialized]
	private global::Radiation radiation;

	// Token: 0x0400037B RID: 891
	private static AudioClip geiger0;

	// Token: 0x0400037C RID: 892
	private static AudioClip geiger1;

	// Token: 0x0400037D RID: 893
	private static AudioClip geiger2;

	// Token: 0x0400037E RID: 894
	private static AudioClip geiger3;

	// Token: 0x0400037F RID: 895
	private static GameObject geigerSoundPlayer;
}
