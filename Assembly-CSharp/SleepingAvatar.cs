using System;
using Facepunch.Actor;
using uLink;
using UnityEngine;

// Token: 0x0200066A RID: 1642
[NGCAutoAddScript]
public class SleepingAvatar : DeployableObject, IServerSaveable
{
	// Token: 0x17000B22 RID: 2850
	// (get) Token: 0x06003908 RID: 14600 RVA: 0x000D1ABC File Offset: 0x000CFCBC
	public MeshRenderer footRenderer
	{
		get
		{
			return (!this.footMeshFilter) ? null : (this.footMeshFilter.renderer as MeshRenderer);
		}
	}

	// Token: 0x17000B23 RID: 2851
	// (get) Token: 0x06003909 RID: 14601 RVA: 0x000D1AF0 File Offset: 0x000CFCF0
	public MeshRenderer legRenderer
	{
		get
		{
			return (!this.legMeshFilter) ? null : (this.legMeshFilter.renderer as MeshRenderer);
		}
	}

	// Token: 0x17000B24 RID: 2852
	// (get) Token: 0x0600390A RID: 14602 RVA: 0x000D1B24 File Offset: 0x000CFD24
	public MeshRenderer torsoRenderer
	{
		get
		{
			return (!this.torsoMeshFilter) ? null : (this.torsoMeshFilter.renderer as MeshRenderer);
		}
	}

	// Token: 0x17000B25 RID: 2853
	// (get) Token: 0x0600390B RID: 14603 RVA: 0x000D1B58 File Offset: 0x000CFD58
	public MeshRenderer headRenderer
	{
		get
		{
			return (!this.headMeshFilter) ? null : (this.headMeshFilter.renderer as MeshRenderer);
		}
	}

	// Token: 0x0600390C RID: 14604 RVA: 0x000D1B8C File Offset: 0x000CFD8C
	[NGCRPC]
	protected void SAAM(int footArmorUID, int legArmorUID, int torsoArmorUID, int headArmorUID)
	{
		if (footArmorUID == 0)
		{
			this.footArmor = null;
		}
		else
		{
			this.footArmor = (ArmorDataBlock)DatablockDictionary.GetByUniqueID(footArmorUID);
		}
		if (legArmorUID == 0)
		{
			this.legArmor = null;
		}
		else
		{
			this.legArmor = (ArmorDataBlock)DatablockDictionary.GetByUniqueID(legArmorUID);
		}
		if (torsoArmorUID == 0)
		{
			this.torsoArmor = null;
		}
		else
		{
			this.torsoArmor = (ArmorDataBlock)DatablockDictionary.GetByUniqueID(torsoArmorUID);
		}
		if (headArmorUID == 0)
		{
			this.headArmor = null;
		}
		else
		{
			this.headArmor = (ArmorDataBlock)DatablockDictionary.GetByUniqueID(headArmorUID);
		}
		this.RebindRenderers();
	}

	// Token: 0x0600390D RID: 14605 RVA: 0x000D1C30 File Offset: 0x000CFE30
	[NGCRPC]
	protected void SACH(NetEntityID makingForCharacterIDNow, NetworkMessageInfo info)
	{
		AudioSource audio = base.audio;
		if (audio)
		{
			audio.Play();
		}
	}

	// Token: 0x0600390E RID: 14606 RVA: 0x000D1C58 File Offset: 0x000CFE58
	[NGCRPC]
	protected void SAKL(NetworkMessageInfo info)
	{
		if (this.CreateRagdoll())
		{
			if (this.footMeshFilter)
			{
				this.footMeshFilter.renderer.enabled = false;
			}
			if (this.legMeshFilter)
			{
				this.legMeshFilter.renderer.enabled = false;
			}
			if (this.torsoMeshFilter)
			{
				this.torsoMeshFilter.renderer.enabled = false;
			}
			if (this.headMeshFilter)
			{
				this.headMeshFilter.renderer.enabled = false;
			}
		}
	}

	// Token: 0x0600390F RID: 14607 RVA: 0x000D1CF4 File Offset: 0x000CFEF4
	private static void BindRenderer<TArmorModel>(ArmorModelRenderer prefabRenderer, ArmorDataBlock armor, MeshFilter filter, MeshRenderer renderer) where TArmorModel : ArmorModel<TArmorModel>, new()
	{
		TArmorModel tarmorModel;
		if (armor)
		{
			tarmorModel = armor.GetArmorModel<TArmorModel>();
			if (!tarmorModel && prefabRenderer)
			{
				tarmorModel = prefabRenderer.GetArmorModel<TArmorModel>();
			}
		}
		else
		{
			if (!prefabRenderer)
			{
				return;
			}
			tarmorModel = prefabRenderer.GetArmorModel<TArmorModel>();
		}
		if (!tarmorModel)
		{
			return;
		}
		if (ArmorModelRenderer.Censored && tarmorModel.censoredModel)
		{
			tarmorModel = tarmorModel.censoredModel;
		}
		Mesh sharedMesh;
		if (tarmorModel && tarmorModel.actorMeshInfo.FindPose("sleep", ref sharedMesh))
		{
			filter.sharedMesh = sharedMesh;
			renderer.sharedMaterials = tarmorModel.sharedMaterials;
		}
	}

	// Token: 0x06003910 RID: 14608 RVA: 0x000D1DE0 File Offset: 0x000CFFE0
	private void RebindRenderers()
	{
		ArmorModelRenderer prefabRenderer = (!this.ragdollPrefab) ? null : this.ragdollPrefab.GetLocal<ArmorModelRenderer>();
		SleepingAvatar.BindRenderer<ArmorModelFeet>(prefabRenderer, this.footArmor, this.footMeshFilter, this.footRenderer);
		SleepingAvatar.BindRenderer<ArmorModelLegs>(prefabRenderer, this.legArmor, this.legMeshFilter, this.legRenderer);
		SleepingAvatar.BindRenderer<ArmorModelTorso>(prefabRenderer, this.torsoArmor, this.torsoMeshFilter, this.torsoRenderer);
		SleepingAvatar.BindRenderer<ArmorModelHead>(prefabRenderer, this.headArmor, this.headMeshFilter, this.headRenderer);
	}

	// Token: 0x06003911 RID: 14609 RVA: 0x000D1E70 File Offset: 0x000D0070
	public static void RebindAllRenderers()
	{
		foreach (Object @object in Object.FindObjectsOfType(typeof(SleepingAvatar)))
		{
			SleepingAvatar sleepingAvatar = (SleepingAvatar)@object;
			if (sleepingAvatar)
			{
				sleepingAvatar.RebindRenderers();
			}
		}
	}

	// Token: 0x06003912 RID: 14610 RVA: 0x000D1EC0 File Offset: 0x000D00C0
	private static bool BindArmorMap<TArmorModel>(ArmorDataBlock armor, ref ArmorModelMemberMap map) where TArmorModel : ArmorModel, new()
	{
		if (armor)
		{
			TArmorModel armorModel = armor.GetArmorModel<TArmorModel>();
			if (armorModel)
			{
				map.SetArmorModel<TArmorModel>(armorModel);
				return true;
			}
		}
		return false;
	}

	// Token: 0x06003913 RID: 14611 RVA: 0x000D1EFC File Offset: 0x000D00FC
	private bool CreateRagdoll()
	{
		if (this.ragdollPrefab)
		{
			ArmorModelRenderer local = this.ragdollPrefab.GetLocal<ArmorModelRenderer>();
			if (local)
			{
				ActorRig actorRig = local.actorRig;
				if (actorRig)
				{
					AnimationClip animationClip;
					float num;
					if (actorRig.FindPoseClip("sleep", ref animationClip, ref num))
					{
						this.ragdollInstance = (Object.Instantiate(this.ragdollPrefab, base.transform.position, base.transform.rotation) as Ragdoll);
						this.ragdollInstance.sourceMain = this;
						GameObject gameObject = this.ragdollInstance.gameObject;
						Object.Destroy(gameObject, 80f);
						gameObject.SampleAnimation(animationClip, num);
						local = this.ragdollInstance.GetLocal<ArmorModelRenderer>();
						ArmorModelMemberMap map = default(ArmorModelMemberMap);
						bool flag = false;
						flag |= SleepingAvatar.BindArmorMap<ArmorModelFeet>(this.footArmor, ref map);
						flag |= SleepingAvatar.BindArmorMap<ArmorModelLegs>(this.legArmor, ref map);
						flag |= SleepingAvatar.BindArmorMap<ArmorModelTorso>(this.torsoArmor, ref map);
						flag |= SleepingAvatar.BindArmorMap<ArmorModelHead>(this.headArmor, ref map);
						if (flag)
						{
							local.BindArmorModels(map);
						}
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x04001D2E RID: 7470
	private const string kPoseName = "sleep";

	// Token: 0x04001D2F RID: 7471
	protected const string ArmorConfigRPC = "SAAM";

	// Token: 0x04001D30 RID: 7472
	protected const string SettingLiveCharacterNowRPC = "SACH";

	// Token: 0x04001D31 RID: 7473
	protected const string HasDiedNowRPC = "SAKL";

	// Token: 0x04001D32 RID: 7474
	[NonSerialized]
	public ArmorDataBlock footArmor;

	// Token: 0x04001D33 RID: 7475
	[NonSerialized]
	public ArmorDataBlock legArmor;

	// Token: 0x04001D34 RID: 7476
	[NonSerialized]
	public ArmorDataBlock torsoArmor;

	// Token: 0x04001D35 RID: 7477
	[NonSerialized]
	public ArmorDataBlock headArmor;

	// Token: 0x04001D36 RID: 7478
	public MeshFilter footMeshFilter;

	// Token: 0x04001D37 RID: 7479
	public MeshFilter legMeshFilter;

	// Token: 0x04001D38 RID: 7480
	public MeshFilter torsoMeshFilter;

	// Token: 0x04001D39 RID: 7481
	public MeshFilter headMeshFilter;

	// Token: 0x04001D3A RID: 7482
	public Ragdoll ragdollPrefab;

	// Token: 0x04001D3B RID: 7483
	[NonSerialized]
	private Ragdoll ragdollInstance;
}
