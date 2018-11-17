using System;
using Facepunch;
using UnityEngine;

// Token: 0x020000A6 RID: 166
public sealed class LocalRadiationEffect : IDLocalCharacterAddon
{
	// Token: 0x06000399 RID: 921 RVA: 0x000126C8 File Offset: 0x000108C8
	public LocalRadiationEffect() : base(IDLocalCharacterAddon.AddonFlags.PrerequisitCheck)
	{
	}

	// Token: 0x0600039B RID: 923 RVA: 0x000126D8 File Offset: 0x000108D8
	protected override bool CheckPrerequesits()
	{
		this.radiation = base.GetComponent<Radiation>();
		return this.radiation;
	}

	// Token: 0x0600039C RID: 924 RVA: 0x000126F4 File Offset: 0x000108F4
	private void OnDestroy()
	{
		if (LocalRadiationEffect.geigerSoundPlayer)
		{
			Object.Destroy(LocalRadiationEffect.geigerSoundPlayer);
		}
	}

	// Token: 0x0600039D RID: 925 RVA: 0x00012710 File Offset: 0x00010910
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
		ImageEffectManager.GetInstance<NoiseAndGrain>().intensityMultiplier = 10f * num2;
		if (LocalRadiationEffect.geiger0 == null)
		{
			Bundling.Load<AudioClip>("content/item/sfx/geiger_low", out LocalRadiationEffect.geiger0);
			Bundling.Load<AudioClip>("content/item/sfx/geiger_medium", out LocalRadiationEffect.geiger1);
			Bundling.Load<AudioClip>("content/item/sfx/geiger_high", out LocalRadiationEffect.geiger2);
			Bundling.Load<AudioClip>("content/item/sfx/geiger_ultra", out LocalRadiationEffect.geiger3);
		}
		if (num >= 0.02f)
		{
			if (!LocalRadiationEffect.geigerSoundPlayer)
			{
				LocalRadiationEffect.geigerSoundPlayer = new GameObject("GEIGER SOUNDS", new Type[]
				{
					typeof(AudioSource)
				});
				LocalRadiationEffect.geigerSoundPlayer.transform.position = base.transform.position;
				LocalRadiationEffect.geigerSoundPlayer.transform.parent = base.transform;
				LocalRadiationEffect.geigerSoundPlayer.audio.loop = true;
			}
			AudioClip audioClip;
			if (num3 <= 0.25f)
			{
				audioClip = LocalRadiationEffect.geiger0;
			}
			else if (num3 <= 0.5f)
			{
				audioClip = LocalRadiationEffect.geiger1;
			}
			else if (num3 <= 0.75f)
			{
				audioClip = LocalRadiationEffect.geiger2;
			}
			else
			{
				audioClip = LocalRadiationEffect.geiger3;
			}
			if (audioClip != LocalRadiationEffect.geigerSoundPlayer.audio.clip)
			{
				LocalRadiationEffect.geigerSoundPlayer.audio.Stop();
				LocalRadiationEffect.geigerSoundPlayer.audio.clip = audioClip;
				LocalRadiationEffect.geigerSoundPlayer.audio.Play();
			}
		}
		else if (LocalRadiationEffect.geigerSoundPlayer)
		{
			LocalRadiationEffect.geigerSoundPlayer.audio.Stop();
			Object.Destroy(LocalRadiationEffect.geigerSoundPlayer);
			LocalRadiationEffect.geigerSoundPlayer = null;
		}
	}

	// Token: 0x0400030F RID: 783
	[NonSerialized]
	private Radiation radiation;

	// Token: 0x04000310 RID: 784
	private static AudioClip geiger0;

	// Token: 0x04000311 RID: 785
	private static AudioClip geiger1;

	// Token: 0x04000312 RID: 786
	private static AudioClip geiger2;

	// Token: 0x04000313 RID: 787
	private static AudioClip geiger3;

	// Token: 0x04000314 RID: 788
	private static GameObject geigerSoundPlayer;
}
