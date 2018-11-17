using System;
using Facepunch.Actor;
using uLink;
using UnityEngine;

// Token: 0x0200072E RID: 1838
[global::NGCAutoAddScript]
public class SleepingAvatar : global::DeployableObject, global::IServerSaveable
{
	// Token: 0x17000BA4 RID: 2980
	// (get) Token: 0x06003CFC RID: 15612 RVA: 0x000DA49C File Offset: 0x000D869C
	public MeshRenderer footRenderer
	{
		get
		{
			return (!this.footMeshFilter) ? null : (this.footMeshFilter.renderer as MeshRenderer);
		}
	}

	// Token: 0x17000BA5 RID: 2981
	// (get) Token: 0x06003CFD RID: 15613 RVA: 0x000DA4D0 File Offset: 0x000D86D0
	public MeshRenderer legRenderer
	{
		get
		{
			return (!this.legMeshFilter) ? null : (this.legMeshFilter.renderer as MeshRenderer);
		}
	}

	// Token: 0x17000BA6 RID: 2982
	// (get) Token: 0x06003CFE RID: 15614 RVA: 0x000DA504 File Offset: 0x000D8704
	public MeshRenderer torsoRenderer
	{
		get
		{
			return (!this.torsoMeshFilter) ? null : (this.torsoMeshFilter.renderer as MeshRenderer);
		}
	}

	// Token: 0x17000BA7 RID: 2983
	// (get) Token: 0x06003CFF RID: 15615 RVA: 0x000DA538 File Offset: 0x000D8738
	public MeshRenderer headRenderer
	{
		get
		{
			return (!this.headMeshFilter) ? null : (this.headMeshFilter.renderer as MeshRenderer);
		}
	}

	// Token: 0x06003D00 RID: 15616 RVA: 0x000DA56C File Offset: 0x000D876C
	[global::NGCRPC]
	protected void SAAM(int footArmorUID, int legArmorUID, int torsoArmorUID, int headArmorUID)
	{
		if (footArmorUID == 0)
		{
			this.footArmor = null;
		}
		else
		{
			this.footArmor = (global::ArmorDataBlock)global::DatablockDictionary.GetByUniqueID(footArmorUID);
		}
		if (legArmorUID == 0)
		{
			this.legArmor = null;
		}
		else
		{
			this.legArmor = (global::ArmorDataBlock)global::DatablockDictionary.GetByUniqueID(legArmorUID);
		}
		if (torsoArmorUID == 0)
		{
			this.torsoArmor = null;
		}
		else
		{
			this.torsoArmor = (global::ArmorDataBlock)global::DatablockDictionary.GetByUniqueID(torsoArmorUID);
		}
		if (headArmorUID == 0)
		{
			this.headArmor = null;
		}
		else
		{
			this.headArmor = (global::ArmorDataBlock)global::DatablockDictionary.GetByUniqueID(headArmorUID);
		}
		this.RebindRenderers();
	}

	// Token: 0x06003D01 RID: 15617 RVA: 0x000DA610 File Offset: 0x000D8810
	[global::NGCRPC]
	protected void SACH(global::NetEntityID makingForCharacterIDNow, uLink.NetworkMessageInfo info)
	{
		AudioSource audio = base.audio;
		if (audio)
		{
			audio.Play();
		}
	}

	// Token: 0x06003D02 RID: 15618 RVA: 0x000DA638 File Offset: 0x000D8838
	[global::NGCRPC]
	protected void SAKL(uLink.NetworkMessageInfo info)
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

	// Token: 0x06003D03 RID: 15619 RVA: 0x000DA6D4 File Offset: 0x000D88D4
	private static void BindRenderer<TArmorModel>(global::ArmorModelRenderer prefabRenderer, global::ArmorDataBlock armor, MeshFilter filter, MeshRenderer renderer) where TArmorModel : global::ArmorModel<TArmorModel>, new()
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
		if (global::ArmorModelRenderer.Censored && tarmorModel.censoredModel)
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

	// Token: 0x06003D04 RID: 15620 RVA: 0x000DA7C0 File Offset: 0x000D89C0
	private void RebindRenderers()
	{
		global::ArmorModelRenderer prefabRenderer = (!this.ragdollPrefab) ? null : this.ragdollPrefab.GetLocal<global::ArmorModelRenderer>();
		global::SleepingAvatar.BindRenderer<global::ArmorModelFeet>(prefabRenderer, this.footArmor, this.footMeshFilter, this.footRenderer);
		global::SleepingAvatar.BindRenderer<global::ArmorModelLegs>(prefabRenderer, this.legArmor, this.legMeshFilter, this.legRenderer);
		global::SleepingAvatar.BindRenderer<global::ArmorModelTorso>(prefabRenderer, this.torsoArmor, this.torsoMeshFilter, this.torsoRenderer);
		global::SleepingAvatar.BindRenderer<global::ArmorModelHead>(prefabRenderer, this.headArmor, this.headMeshFilter, this.headRenderer);
	}

	// Token: 0x06003D05 RID: 15621 RVA: 0x000DA850 File Offset: 0x000D8A50
	public static void RebindAllRenderers()
	{
		foreach (Object @object in Object.FindObjectsOfType(typeof(global::SleepingAvatar)))
		{
			global::SleepingAvatar sleepingAvatar = (global::SleepingAvatar)@object;
			if (sleepingAvatar)
			{
				sleepingAvatar.RebindRenderers();
			}
		}
	}

	// Token: 0x06003D06 RID: 15622 RVA: 0x000DA8A0 File Offset: 0x000D8AA0
	private static bool BindArmorMap<TArmorModel>(global::ArmorDataBlock armor, ref global::ArmorModelMemberMap map) where TArmorModel : global::ArmorModel, new()
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

	// Token: 0x06003D07 RID: 15623 RVA: 0x000DA8DC File Offset: 0x000D8ADC
	private bool CreateRagdoll()
	{
		if (this.ragdollPrefab)
		{
			global::ArmorModelRenderer local = this.ragdollPrefab.GetLocal<global::ArmorModelRenderer>();
			if (local)
			{
				ActorRig actorRig = local.actorRig;
				if (actorRig)
				{
					AnimationClip animationClip;
					float num;
					if (actorRig.FindPoseClip("sleep", ref animationClip, ref num))
					{
						this.ragdollInstance = (Object.Instantiate(this.ragdollPrefab, base.transform.position, base.transform.rotation) as global::Ragdoll);
						this.ragdollInstance.sourceMain = this;
						GameObject gameObject = this.ragdollInstance.gameObject;
						Object.Destroy(gameObject, 80f);
						gameObject.SampleAnimation(animationClip, num);
						local = this.ragdollInstance.GetLocal<global::ArmorModelRenderer>();
						global::ArmorModelMemberMap map = default(global::ArmorModelMemberMap);
						bool flag = false;
						flag |= global::SleepingAvatar.BindArmorMap<global::ArmorModelFeet>(this.footArmor, ref map);
						flag |= global::SleepingAvatar.BindArmorMap<global::ArmorModelLegs>(this.legArmor, ref map);
						flag |= global::SleepingAvatar.BindArmorMap<global::ArmorModelTorso>(this.torsoArmor, ref map);
						flag |= global::SleepingAvatar.BindArmorMap<global::ArmorModelHead>(this.headArmor, ref map);
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

	// Token: 0x04001F26 RID: 7974
	private const string kPoseName = "sleep";

	// Token: 0x04001F27 RID: 7975
	protected const string ArmorConfigRPC = "SAAM";

	// Token: 0x04001F28 RID: 7976
	protected const string SettingLiveCharacterNowRPC = "SACH";

	// Token: 0x04001F29 RID: 7977
	protected const string HasDiedNowRPC = "SAKL";

	// Token: 0x04001F2A RID: 7978
	[NonSerialized]
	public global::ArmorDataBlock footArmor;

	// Token: 0x04001F2B RID: 7979
	[NonSerialized]
	public global::ArmorDataBlock legArmor;

	// Token: 0x04001F2C RID: 7980
	[NonSerialized]
	public global::ArmorDataBlock torsoArmor;

	// Token: 0x04001F2D RID: 7981
	[NonSerialized]
	public global::ArmorDataBlock headArmor;

	// Token: 0x04001F2E RID: 7982
	public MeshFilter footMeshFilter;

	// Token: 0x04001F2F RID: 7983
	public MeshFilter legMeshFilter;

	// Token: 0x04001F30 RID: 7984
	public MeshFilter torsoMeshFilter;

	// Token: 0x04001F31 RID: 7985
	public MeshFilter headMeshFilter;

	// Token: 0x04001F32 RID: 7986
	public global::Ragdoll ragdollPrefab;

	// Token: 0x04001F33 RID: 7987
	[NonSerialized]
	private global::Ragdoll ragdollInstance;
}
