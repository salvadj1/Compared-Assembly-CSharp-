using System;
using UnityEngine;

// Token: 0x02000567 RID: 1383
public class FootstepEmitter : global::IDLocalCharacter
{
	// Token: 0x06002DBB RID: 11707 RVA: 0x000AC6DC File Offset: 0x000AA8DC
	private void Start()
	{
		this.lastFootstepPos = base.origin;
		this.trait = base.GetTrait<global::CharacterFootstepTrait>();
		if (!this.trait || !this.trait.defaultFootsteps || this.trait.defaultFootsteps.Length == 0)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06002DBC RID: 11708 RVA: 0x000AC744 File Offset: 0x000AA944
	private Collider GetBelowObj()
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(new Ray(base.transform.position + new Vector3(0f, 0.25f, 0f), Vector3.down), ref raycastHit, 1f))
		{
			return raycastHit.collider;
		}
		return null;
	}

	// Token: 0x06002DBD RID: 11709 RVA: 0x000AC79C File Offset: 0x000AA99C
	private void Update()
	{
		if (this.terraincheck)
		{
			int textureIndex = global::TerrainTextureHelper.GetTextureIndex(base.origin);
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
			if (global::footsteps.quality >= 2 || (global::footsteps.quality == 1 && base.character.localControlled))
			{
				Collider belowObj = this.GetBelowObj();
				if (belowObj)
				{
					global::SurfaceInfoObject surfaceInfoFor = global::SurfaceInfo.GetSurfaceInfoFor(belowObj, origin);
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

	// Token: 0x040017B6 RID: 6070
	[NonSerialized]
	private global::CharacterFootstepTrait trait;

	// Token: 0x040017B7 RID: 6071
	[NonSerialized]
	private Vector3 lastFootstepPos;

	// Token: 0x040017B8 RID: 6072
	[NonSerialized]
	private float nextAllowTime;

	// Token: 0x040017B9 RID: 6073
	[NonSerialized]
	private float movedAmount;

	// Token: 0x040017BA RID: 6074
	public bool terraincheck;

	// Token: 0x040017BB RID: 6075
	private AudioClip lastPlayed;
}
