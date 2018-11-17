using System;
using UnityEngine;

// Token: 0x020004AC RID: 1196
public class FootstepEmitter : IDLocalCharacter
{
	// Token: 0x06002A09 RID: 10761 RVA: 0x000A4944 File Offset: 0x000A2B44
	private void Start()
	{
		this.lastFootstepPos = base.origin;
		this.trait = base.GetTrait<CharacterFootstepTrait>();
		if (!this.trait || !this.trait.defaultFootsteps || this.trait.defaultFootsteps.Length == 0)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06002A0A RID: 10762 RVA: 0x000A49AC File Offset: 0x000A2BAC
	private Collider GetBelowObj()
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(new Ray(base.transform.position + new Vector3(0f, 0.25f, 0f), Vector3.down), ref raycastHit, 1f))
		{
			return raycastHit.collider;
		}
		return null;
	}

	// Token: 0x06002A0B RID: 10763 RVA: 0x000A4A04 File Offset: 0x000A2C04
	private void Update()
	{
		if (this.terraincheck)
		{
			int textureIndex = TerrainTextureHelper.GetTextureIndex(base.origin);
		}
		bool timeLimited;
		if (!base.stateFlags.grounded || ((timeLimited = this.trait.timeLimited) && this.nextAllowTime > Time.time) || (base.masterControllable && base.masterControllable.idMain != base.idMain))
		{
			return;
		}
		bool crouch = base.stateFlags.crouch;
		Vector3 origin = base.origin;
		this.movedAmount += Vector3.Distance(this.lastFootstepPos, origin);
		this.lastFootstepPos = origin;
		if (this.movedAmount >= this.trait.sqrStrideDist)
		{
			this.movedAmount = 0f;
			AudioClip audioClip = null;
			if (footsteps.quality >= 2 || (footsteps.quality == 1 && base.character.localControlled))
			{
				Collider belowObj = this.GetBelowObj();
				if (belowObj)
				{
					SurfaceInfoObject surfaceInfoFor = SurfaceInfo.GetSurfaceInfoFor(belowObj, origin);
					if (surfaceInfoFor)
					{
						audioClip = ((!this.trait.animal) ? surfaceInfoFor.GetFootstepBiped(this.lastPlayed) : surfaceInfoFor.GetFootstepAnimal());
						this.lastPlayed = audioClip;
					}
				}
			}
			if (!audioClip)
			{
				audioClip = this.trait.defaultFootsteps[Random.Range(0, this.trait.defaultFootsteps.Length)];
				if (!audioClip)
				{
					return;
				}
			}
			float minAudioDist = this.trait.minAudioDist;
			float maxAudioDist = this.trait.maxAudioDist;
			if (crouch)
			{
				audioClip.Play(origin, 0.2f, Random.Range(0.95f, 1.05f), minAudioDist * 0.333f, maxAudioDist * 0.333f, 30);
			}
			else
			{
				audioClip.Play(origin, 0.65f, Random.Range(0.95f, 1.05f), minAudioDist, maxAudioDist, 30);
			}
			if (timeLimited)
			{
				this.nextAllowTime = Time.time + this.trait.minInterval;
			}
		}
	}

	// Token: 0x040015F9 RID: 5625
	[NonSerialized]
	private CharacterFootstepTrait trait;

	// Token: 0x040015FA RID: 5626
	[NonSerialized]
	private Vector3 lastFootstepPos;

	// Token: 0x040015FB RID: 5627
	[NonSerialized]
	private float nextAllowTime;

	// Token: 0x040015FC RID: 5628
	[NonSerialized]
	private float movedAmount;

	// Token: 0x040015FD RID: 5629
	public bool terraincheck;

	// Token: 0x040015FE RID: 5630
	private AudioClip lastPlayed;
}
