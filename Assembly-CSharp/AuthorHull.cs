using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Facepunch.Actor;
using UnityEngine;

// Token: 0x0200002A RID: 42
[AuthorSuiteCreation(Title = "Author Hull", Description = "Create a new character. Allows you to define hitboxes and fine tune ragdoll and joints.", Scripter = "Pat", OutputType = typeof(Character), Ready = true)]
public class AuthorHull : AuthorCreation
{
	// Token: 0x060001A9 RID: 425 RVA: 0x000085E8 File Offset: 0x000067E8
	public AuthorHull() : this(typeof(Character))
	{
	}

	// Token: 0x060001AA RID: 426 RVA: 0x000085FC File Offset: 0x000067FC
	protected AuthorHull(Type type) : base(type)
	{
	}

	// Token: 0x060001AC RID: 428 RVA: 0x00008698 File Offset: 0x00006898
	public HitBox CreateHitBox(GameObject target)
	{
		HitBox hitBox = AuthorShared.AddComponent<HitBox>(target, this.hitBoxType);
		AuthorShared.SetSerializedProperty(hitBox, "_hitBoxSystem", this.creatingSystem);
		hitBox.idMain = hitBox.hitBoxSystem.idMain;
		return hitBox;
	}

	// Token: 0x060001AD RID: 429 RVA: 0x000086D8 File Offset: 0x000068D8
	public HitBoxSystem CreateHitBoxSystem(GameObject target)
	{
		return AuthorShared.AddComponent<HitBoxSystem>(target, this.hitBoxSystemType);
	}

	// Token: 0x060001AE RID: 430 RVA: 0x000086E8 File Offset: 0x000068E8
	private Transform GetHitColliderParent(GameObject root)
	{
		SkinnedMeshRenderer skinnedMeshRenderer;
		Transform rootBone = AuthorShared.GetRootBone(root, out skinnedMeshRenderer);
		return (!skinnedMeshRenderer || !skinnedMeshRenderer.transform.parent) ? rootBone : skinnedMeshRenderer.transform.parent;
	}

	// Token: 0x060001AF RID: 431 RVA: 0x00008730 File Offset: 0x00006930
	public override IEnumerable<AuthorPeice> DoSceneView()
	{
		if (this.drawBones && this.modelPrefabInstance != null)
		{
			Transform rootBone = AuthorShared.GetRootBone(this.modelPrefabInstance);
			if (rootBone)
			{
				Color color = AuthorShared.Scene.color;
				Color color2 = color * new Color(0.9f, 0.8f, 0.3f, 0.1f);
				List<Transform> list = rootBone.ListDecendantsByDepth();
				AuthorShared.Scene.color = color2;
				foreach (Transform transform in list)
				{
					Vector3 position = transform.parent.position;
					Vector3 position2 = transform.position;
					Vector3 vector = position2 - position;
					float magnitude = vector.magnitude;
					if (magnitude != 0f)
					{
						Vector3 up = transform.up;
						Quaternion rot = Quaternion.LookRotation(vector, up);
						AuthorShared.Scene.DrawBone(position, rot, magnitude, Mathf.Min(magnitude / 2f, 0.025f), Vector3.one * Mathf.Min(magnitude, 0.05f));
					}
				}
				AuthorShared.Scene.color = color;
			}
		}
		return base.DoSceneView();
	}

	// Token: 0x060001B0 RID: 432 RVA: 0x00008884 File Offset: 0x00006A84
	private void ApplyMaterials(GameObject instance)
	{
		SkinnedMeshRenderer skinnedMeshRenderer = (!(instance == null)) ? instance.GetComponentInChildren<SkinnedMeshRenderer>() : null;
		if (skinnedMeshRenderer)
		{
			skinnedMeshRenderer.sharedMaterials = this.materials;
		}
	}

	// Token: 0x060001B1 RID: 433 RVA: 0x000088C4 File Offset: 0x00006AC4
	private void DestroyRepresentations(ref GameObject stored, string suffix)
	{
		if (stored)
		{
			Object.DestroyImmediate(stored);
		}
		foreach (Object @object in Object.FindObjectsOfType(typeof(GameObject)))
		{
			if (@object && ((GameObject)@object).transform.parent == null && @object.name.EndsWith(suffix))
			{
				Object.DestroyImmediate(@object);
			}
		}
	}

	// Token: 0x060001B2 RID: 434 RVA: 0x0000894C File Offset: 0x00006B4C
	protected override bool OnGUICreationSettings()
	{
		bool flag = base.OnGUICreationSettings();
		bool flag2 = this.modelPrefab;
		GameObject gameObject = (GameObject)AuthorShared.ObjectField("Model Prefab", this.modelPrefab, typeof(GameObject), true, new GUILayoutOption[0]);
		if (gameObject != this.modelPrefab)
		{
			if (!gameObject)
			{
				gameObject = this.modelPrefab;
			}
			else if (AuthorShared.GetObjectKind(gameObject) != AuthorShared.ObjectKind.Model)
			{
				gameObject = this.modelPrefab;
			}
			else
			{
				gameObject = AuthorShared.FindPrefabRoot(gameObject);
			}
		}
		if (gameObject != this.modelPrefab)
		{
			this.modelPrefab = gameObject;
			this.ChangedModelPrefab();
			this.ChangedEditingOptions();
			flag |= true;
		}
		bool enabled = GUI.enabled;
		if (!flag2)
		{
			GUI.enabled = false;
		}
		bool flag3 = this.modelPrefabForHitBox;
		GameObject gameObject2 = (GameObject)AuthorShared.ObjectField("Override Model Prefab [HitBox]", (!flag3) ? this.modelPrefab : this.modelPrefabForHitBox, typeof(GameObject), true, new GUILayoutOption[0]);
		GUI.enabled = enabled;
		if (!gameObject2 || gameObject2 == this.modelPrefab)
		{
			if (flag2)
			{
				GUILayout.Label(AuthorHull.guis.notOverridingContent, AuthorShared.Styles.miniLabel, new GUILayoutOption[0]);
			}
			gameObject2 = null;
		}
		else
		{
			GUILayout.Label(AuthorHull.guis.overridingContent, AuthorShared.Styles.miniLabel, new GUILayoutOption[0]);
			bool flag4 = AuthorShared.Toggle("Use Meshes from Override in Ragdoll output", this.useMeshesFromHitBoxOnRagdoll, new GUILayoutOption[0]);
			if (flag4 != this.useMeshesFromHitBoxOnRagdoll)
			{
				this.useMeshesFromHitBoxOnRagdoll = flag4;
				flag = true;
			}
		}
		if (gameObject2 != this.modelPrefabForHitBox)
		{
			if (!gameObject2)
			{
				gameObject2 = this.modelPrefabForHitBox;
			}
			else if (AuthorShared.GetObjectKind(gameObject2) != AuthorShared.ObjectKind.Model)
			{
				gameObject2 = this.modelPrefabForHitBox;
			}
			else
			{
				gameObject2 = AuthorShared.FindPrefabRoot(gameObject2);
			}
		}
		if (gameObject2 != this.modelPrefabForHitBox)
		{
			this.modelPrefabForHitBox = gameObject2;
			flag |= true;
		}
		ActorRig actorRig = (ActorRig)AuthorShared.ObjectField("Actor Rig", this.actorRig, typeof(ActorRig), AuthorShared.ObjectFieldFlags.Asset, new GUILayoutOption[0]);
		if (actorRig != this.actorRig && !actorRig)
		{
			actorRig = this.actorRig;
		}
		if (actorRig != this.actorRig)
		{
			this.actorRig = actorRig;
			flag |= true;
		}
		Character character = (Character)AuthorShared.ObjectField("Prototype Prefab", this.prototype, typeof(IDMain), AuthorShared.ObjectFieldFlags.Prefab, new GUILayoutOption[0]);
		if (character != this.prototype && character && AuthorShared.GetObjectKind(character.gameObject) != AuthorShared.ObjectKind.Prefab)
		{
			character = this.prototype;
		}
		if (character != this.prototype)
		{
			this.prototype = character;
			flag |= true;
		}
		Ragdoll ragdoll = (Ragdoll)AuthorShared.ObjectField("Prototype Ragdoll", this.ragdollPrototype, typeof(IDMain), AuthorShared.ObjectFieldFlags.Prefab, new GUILayoutOption[0]);
		if (ragdoll != this.ragdollPrototype && ragdoll && AuthorShared.GetObjectKind(ragdoll.gameObject) != AuthorShared.ObjectKind.Prefab)
		{
			ragdoll = this.ragdollPrototype;
		}
		if (ragdoll != this.ragdollPrototype)
		{
			this.ragdollPrototype = ragdoll;
			flag |= true;
		}
		if (this.modelPrefabInstance)
		{
			bool activeSelf = this.modelPrefabInstance.activeSelf;
			AuthorShared.BeginHorizontal(new GUILayoutOption[0]);
			if (AuthorShared.Toggle("Show Model Prefab", ref activeSelf, AuthorShared.Styles.miniButton, new GUILayoutOption[0]))
			{
				this.modelPrefabInstance.SetActive(activeSelf);
			}
			flag |= AuthorShared.Toggle("Render Bones", ref this.drawBones, AuthorShared.Styles.miniButton, new GUILayoutOption[0]);
			AuthorShared.EndHorizontal();
		}
		AuthorShared.BeginSubSection("Rendering", new GUILayoutOption[0]);
		if (AuthorShared.ArrayField<Material>("Materials", ref this.materials, delegate(ref Material material)
		{
			return AuthorShared.ObjectField<Material>(default(AuthorShared.Content), ref material, typeof(Material), (AuthorShared.ObjectFieldFlags)0, new GUILayoutOption[0]);
		}))
		{
			flag = true;
			this.ApplyMaterials(this.modelPrefabInstance);
		}
		AuthorShared.EndSubSection();
		AuthorShared.BeginSubSection("Types", "AddComponent strings", new GUILayoutOption[0]);
		string a = AuthorShared.StringField("HitBox Type", this.hitBoxType, new GUILayoutOption[0]);
		string a2 = AuthorShared.StringField("HitBoxSystem Type", this.hitBoxSystemType, new GUILayoutOption[0]);
		AuthorShared.EndSubSection();
		AuthorShared.BeginSubSection("Hit Capsule", "Should be large enough to fit all boxes at any time", new GUILayoutOption[0]);
		Vector3 vector = AuthorShared.Vector3Field("Center", this.hitCapsuleCenter, new GUILayoutOption[0]);
		float num = AuthorShared.FloatField("Radius", this.hitCapsuleRadius, new GUILayoutOption[0]);
		float num2 = AuthorShared.FloatField("Height", this.hitCapsuleHeight, new GUILayoutOption[0]);
		int num3 = AuthorShared.IntField("Axis", this.hitCapsuleDirection, new GUILayoutOption[0]);
		float num4 = AuthorShared.FloatField("Eye Height", this.eyeHeight, new GUILayoutOption[0]);
		AuthorShared.EndSubSection();
		AuthorShared.BeginSubSection("Rigidbody", new GUILayoutOption[0]);
		flag |= AuthorShared.IntField("Ignore n. parent col.", ref this.ignoreCollisionUpSteps, new GUILayoutOption[0]);
		flag |= AuthorShared.IntField("Ignore n. child col.", ref this.ignoreCollisionDownSteps, new GUILayoutOption[0]);
		AuthorShared.EndSubSection();
		AuthorShared.BeginSubSection("Body Parts", new GUILayoutOption[0]);
		string a3 = AuthorShared.StringField("Default Hit Box Layer", this.defaultBodyPartLayer ?? string.Empty, new GUILayoutOption[0]);
		if (string.IsNullOrEmpty(this.defaultBodyPartLayer))
		{
			AuthorShared.Label("[the layer in the models will be used]", new GUILayoutOption[0]);
		}
		if (a3 != (this.defaultBodyPartLayer ?? string.Empty))
		{
			this.defaultBodyPartLayer = a3;
			flag = true;
		}
		bool flag5 = this.bodyParts.Count == 0 || AuthorShared.Toggle("Show Unassigned Parts", this.showAllBones, new GUILayoutOption[0]);
		for (BodyPart bodyPart = 0; bodyPart < 120; bodyPart++)
		{
			Transform transform;
			if ((this.bodyParts.TryGetValue(bodyPart, ref transform) || this.showAllBones) && AuthorShared.ObjectField<Transform>(bodyPart.ToString(), ref transform, (AuthorShared.ObjectFieldFlags)17, new GUILayoutOption[0]))
			{
				if (transform)
				{
					BodyPart? bodyPart2 = this.bodyParts.BodyPartOf(transform);
					if (bodyPart2 != null)
					{
						bool? flag6 = AuthorShared.Ask(string.Concat(new object[]
						{
							"That transform was assigned do something else.\r\nChange it from ",
							bodyPart2.Value,
							" to ",
							bodyPart,
							"?"
						}));
						bool? flag7 = (flag6 == null) ? null : new bool?(!flag6.Value);
						if (flag7 != null && flag7.Value)
						{
							goto IL_7C3;
						}
						this.bodyParts.Remove(bodyPart2.Value);
					}
					this.bodyParts[bodyPart] = transform;
				}
				else
				{
					this.bodyParts.Remove(bodyPart);
				}
				flag = true;
			}
			IL_7C3:;
		}
		this.showAllBones = flag5;
		AuthorShared.BeginSubSection("Destroy Children", new GUILayoutOption[0]);
		AuthorShared.BeginSubSection(AuthorHull.guis.destroyDrop, "Remove these from generation", AuthorShared.Styles.miniLabel, new GUILayoutOption[0]);
		Transform transform2 = (Transform)AuthorShared.ObjectField(null, typeof(Transform), (AuthorShared.ObjectFieldFlags)25, new GUILayoutOption[0]);
		AuthorShared.EndSubSection();
		if (transform2 && (!this.modelPrefabInstance || !transform2.IsChildOf(this.modelPrefabInstance.transform)))
		{
			Debug.Log("Thats not a valid selection", transform2);
			transform2 = null;
		}
		bool flag8 = false;
		if (this.removeThese != null && this.removeThese.Length > 0)
		{
			AuthorShared.BeginSubSection("These will be removed with generation", new GUILayoutOption[0]);
			for (int i = 0; i < this.removeThese.Length; i++)
			{
				AuthorShared.BeginHorizontal(AuthorShared.Styles.gradientOutline, new GUILayoutOption[0]);
				if (AuthorShared.Button(AuthorShared.ObjectContent<Transform>(this.removeThese[i], typeof(Transform)), AuthorShared.Styles.peiceButtonLeft, new GUILayoutOption[0]) && this.removeThese[i])
				{
					AuthorShared.PingObject(this.removeThese[i]);
				}
				if (AuthorShared.Button(AuthorShared.Icon.delete, AuthorShared.Styles.peiceButtonRight, new GUILayoutOption[0]))
				{
					this.removeThese[i] = null;
					flag8 = true;
				}
				AuthorShared.EndHorizontal();
			}
			AuthorShared.EndSubSection();
		}
		AuthorShared.EndSubSection();
		AuthorShared.EndSubSection();
		AuthorShared.BeginSubSection("Output", "this is where stuff will be saved", new GUILayoutOption[0]);
		Object @object = AuthorShared.ObjectField("OUTPUT HITBOX", this.hitBoxOutputPrefab, typeof(GameObject), (AuthorShared.ObjectFieldFlags)196, new GUILayoutOption[0]);
		Object object2 = AuthorShared.ObjectField("OUTPUT RAGDOLL", this.ragdollOutputPrefab, typeof(GameObject), (AuthorShared.ObjectFieldFlags)196, new GUILayoutOption[0]);
		AuthorShared.EndSubSection();
		AuthorShared.BeginSubSection("Authoring Helpers", "These do not output to the mesh, just are here to help you author", new GUILayoutOption[0]);
		Vector3 vector2 = AuthorShared.Vector3Field("Angles Offset", this.editingAngles, new GUILayoutOption[0]);
		bool flag9 = AuthorShared.Toggle("Origin To Root", this.editingCenterToRoot, new GUILayoutOption[0]);
		AuthorShared.EndSubSection();
		AuthorShared.BeginHorizontal(AuthorShared.Styles.box, new GUILayoutOption[0]);
		bool enabled2 = GUI.enabled;
		if (!gameObject)
		{
			GUI.enabled = false;
		}
		if (AuthorShared.Button("Generate", AuthorShared.Styles.miniButtonLeft, new GUILayoutOption[0]))
		{
			this.GeneratePrefabInstances();
			this.savedGenerated = false;
			AuthorShared.SetDirty(this);
			flag = true;
		}
		GUI.enabled = (!this.savedGenerated && this.generatedRigid && this.generatedHitBox && this.hitBoxOutputPrefab && this.ragdollOutputPrefab && this.ragdollOutputPrefab != this.hitBoxOutputPrefab);
		if (AuthorShared.Button("Update Prefabs", AuthorShared.Styles.miniButtonRight, new GUILayoutOption[0]) && AuthorShared.Ask("This will overwrite any changes made to the output prefab that may have been done externally\r\nStill go ahead?") == true)
		{
			this.UpdatePrefabs();
			this.savedGenerated = true;
			flag = true;
		}
		GUI.enabled = enabled2;
		AuthorShared.EndHorizontal();
		if (AuthorShared.Button("Save To JSON", new GUILayoutOption[0]))
		{
			base.SaveSettings();
		}
		if (this.prototype && AuthorShared.Button("Write JSON Serialized Properties from Prototype", new GUILayoutOption[0]))
		{
			this.PreviewPrototype();
		}
		if (a != this.hitBoxType || a2 != this.hitBoxSystemType)
		{
			this.hitBoxType = a;
			this.hitBoxSystemType = a2;
			flag = true;
		}
		else if (vector != this.hitCapsuleCenter || num != this.hitCapsuleRadius || num2 != this.hitCapsuleHeight || num3 != this.hitCapsuleDirection || num4 != this.eyeHeight)
		{
			this.hitCapsuleCenter = vector;
			this.hitCapsuleRadius = num;
			this.hitCapsuleHeight = num2;
			this.hitCapsuleDirection = num3;
			this.eyeHeight = num4;
			flag = true;
		}
		else if (vector2 != this.editingAngles || this.editingCenterToRoot != flag9)
		{
			this.editingAngles = vector2;
			this.editingCenterToRoot = flag9;
			flag = true;
			this.ChangedEditingOptions();
		}
		else if (@object != this.hitBoxOutputPrefab)
		{
			if (this.EnsureItsAPrefab(ref @object) && @object != this.hitBoxOutputPrefab)
			{
				this.hitBoxOutputPrefab = (GameObject)@object;
				flag = true;
			}
		}
		else if (object2 != this.ragdollOutputPrefab)
		{
			if (this.EnsureItsAPrefab(ref object2) && object2 != this.ragdollOutputPrefab)
			{
				this.ragdollOutputPrefab = (GameObject)object2;
				flag = true;
			}
		}
		else if (transform2)
		{
			Array.Resize<Transform>(ref this.removeThese, (this.removeThese != null) ? (this.removeThese.Length + 1) : 1);
			this.removeThese[this.removeThese.Length - 1] = transform2;
			flag8 = true;
		}
		if (flag8)
		{
			int newSize = 0;
			for (int j = 0; j < this.removeThese.Length; j++)
			{
				if (this.removeThese[j])
				{
					this.removeThese[newSize++] = this.removeThese[j];
				}
			}
			Array.Resize<Transform>(ref this.removeThese, newSize);
			flag = true;
		}
		return flag;
	}

	// Token: 0x060001B3 RID: 435 RVA: 0x00009730 File Offset: 0x00007930
	internal void FigureOutDefaultBodyPart(ref Transform bone, ref BodyPart bodyPart, ref Transform mirrored, ref BodyPart mirroredBodyPart)
	{
		BodyPart bodyPart2 = bodyPart;
		for (BodyPart bodyPart3 = 0; bodyPart3 < 120; bodyPart3++)
		{
			Transform transform;
			if (this.bodyParts.TryGetValue(bodyPart3, ref transform) && transform == bone)
			{
				bodyPart2 = bodyPart3;
			}
		}
		if (bodyPart2 != bodyPart)
		{
			bodyPart = bodyPart2;
			if (!mirrored && BodyParts.IsSided(bodyPart))
			{
				bodyPart2 = BodyParts.SwapSide(bodyPart2);
				if (this.bodyParts.TryGetValue(bodyPart2, ref mirrored))
				{
					mirroredBodyPart = bodyPart2;
				}
			}
		}
	}

	// Token: 0x060001B4 RID: 436 RVA: 0x000097B8 File Offset: 0x000079B8
	private void ChangedEditingOptions()
	{
		if (this.modelPrefabInstance)
		{
			this.modelPrefabInstance.transform.localEulerAngles = this.editingAngles;
			this.modelPrefabInstance.transform.localPosition = Vector3.zero;
			if (this.editingCenterToRoot)
			{
				Transform rootBone = AuthorShared.GetRootBone(this.modelPrefabInstance.GetComponentInChildren<SkinnedMeshRenderer>());
				if (rootBone)
				{
					this.modelPrefabInstance.transform.position = -rootBone.position;
				}
			}
		}
	}

	// Token: 0x060001B5 RID: 437 RVA: 0x00009844 File Offset: 0x00007A44
	private static KeyValuePair<Collider, Collider> MakeKV(Collider a, Collider b)
	{
		if (string.Compare(a.name, b.name) < 0)
		{
			return new KeyValuePair<Collider, Collider>(b, a);
		}
		return new KeyValuePair<Collider, Collider>(a, b);
	}

	// Token: 0x060001B6 RID: 438 RVA: 0x00009878 File Offset: 0x00007A78
	private static IEnumerable<Collider> GetCollidersOnRigidbody(Rigidbody rb)
	{
		foreach (Collider collider in rb.GetComponentsInChildren<Collider>())
		{
			if (collider.attachedRigidbody == rb)
			{
				yield return collider;
			}
		}
		yield break;
	}

	// Token: 0x060001B7 RID: 439 RVA: 0x000098A4 File Offset: 0x00007AA4
	private GameObject InstantiatePrefabWithRemovedBones(GameObject prefab)
	{
		GameObject gameObject = AuthorShared.InstantiatePrefab(prefab);
		if (this.modelPrefabInstance)
		{
			if (this.removeThese != null)
			{
				for (int i = 0; i < this.removeThese.Length; i++)
				{
					if (this.removeThese[i])
					{
						Transform transform = gameObject.transform.FindChild(AuthorShared.CalculatePath(this.removeThese[i], this.modelPrefabInstance.transform));
						if (transform)
						{
							Object.DestroyImmediate(transform);
						}
					}
				}
			}
			if (!this.allowBonesOutsideOfModelPrefab && prefab != this.modelPrefab)
			{
				foreach (Transform transform2 in gameObject.GetComponentsInChildren<Transform>(true))
				{
					if (transform2)
					{
						string text = AuthorShared.CalculatePath(transform2, gameObject.transform);
						if (!string.IsNullOrEmpty(text))
						{
							if (!this.modelPrefabInstance.transform.Find(text))
							{
								Debug.LogWarning("Deleted bone because it was not in the model prefab instance:" + text, gameObject);
								Object.DestroyImmediate(transform2.gameObject);
							}
						}
					}
				}
			}
		}
		return gameObject;
	}

	// Token: 0x060001B8 RID: 440 RVA: 0x000099E0 File Offset: 0x00007BE0
	private GameObject MakeColliderPrefab()
	{
		GameObject gameObject = this.InstantiatePrefabWithRemovedBones(this.modelPrefab);
		if (this.removeThese != null)
		{
			GameObject gameObject2 = gameObject;
			gameObject2.name += "::RAGDOLL_OUTPUT::";
		}
		foreach (Animation animation in gameObject.GetComponentsInChildren<Animation>())
		{
			if (animation)
			{
				Object.DestroyImmediate(animation, true);
			}
		}
		if (this.useMeshesFromHitBoxOnRagdoll && this.modelPrefabForHitBox && this.modelPrefabForHitBox != this.modelPrefab)
		{
			foreach (Renderer renderer in gameObject.GetComponentsInChildren<Renderer>())
			{
				if (renderer)
				{
					if (renderer is MeshRenderer)
					{
						MeshFilter component = renderer.GetComponent<MeshFilter>();
						string text = AuthorShared.CalculatePath(renderer.transform, gameObject.transform);
						component.sharedMesh = this.modelPrefabForHitBox.transform.FindChild(text).GetComponent<MeshFilter>().sharedMesh;
						AuthorShared.SetDirty(component);
					}
					else if (renderer is SkinnedMeshRenderer)
					{
						((SkinnedMeshRenderer)renderer).sharedMesh = this.modelPrefabForHitBox.transform.FindChild(AuthorShared.CalculatePath(renderer.transform, gameObject.transform)).GetComponent<SkinnedMeshRenderer>().sharedMesh;
						AuthorShared.SetDirty(renderer);
					}
				}
			}
		}
		this.ApplyMaterials(gameObject);
		int? layerIndex;
		if (string.IsNullOrEmpty(this.defaultBodyPartLayer))
		{
			layerIndex = null;
		}
		else
		{
			layerIndex = new int?(LayerMask.NameToLayer(this.defaultBodyPartLayer));
		}
		foreach (AuthorPeice authorPeice in base.EnumeratePeices())
		{
			if (authorPeice && authorPeice is AuthorChHit)
			{
				((AuthorChHit)authorPeice).CreateColliderOn(gameObject.transform, this.modelPrefabInstance.transform, true, layerIndex);
			}
		}
		Transform rootBone = AuthorShared.GetRootBone(gameObject);
		Dictionary<Rigidbody, List<Collider>> dictionary = new Dictionary<Rigidbody, List<Collider>>();
		foreach (Collider collider in rootBone.GetComponentsInChildren<Collider>())
		{
			Rigidbody attachedRigidbody = collider.attachedRigidbody;
			if (attachedRigidbody)
			{
				List<Collider> list;
				if (!dictionary.TryGetValue(attachedRigidbody, out list))
				{
					list = new List<Collider>();
					dictionary[attachedRigidbody] = list;
				}
				list.Add(collider);
			}
		}
		HashSet<KeyValuePair<Collider, Collider>> hashSet = new HashSet<KeyValuePair<Collider, Collider>>();
		foreach (KeyValuePair<Rigidbody, List<Collider>> keyValuePair in dictionary)
		{
			Transform transform = keyValuePair.Key.transform;
			Transform parent = transform.parent;
			int num = 0;
			while (num++ < this.ignoreCollisionUpSteps && parent && parent.IsChildOf(rootBone))
			{
				Rigidbody rigidbody;
				do
				{
					rigidbody = parent.rigidbody;
				}
				while (!rigidbody && (parent = parent.parent) && parent.IsChildOf(rootBone));
				if (rigidbody)
				{
					foreach (Collider a in keyValuePair.Value)
					{
						foreach (Collider b in dictionary[rigidbody])
						{
							hashSet.Add(AuthorHull.MakeKV(a, b));
						}
					}
				}
			}
			if (this.ignoreCollisionDownSteps > 0)
			{
				foreach (Transform transform2 in transform.ListDecendantsByDepth())
				{
					Rigidbody rigidbody2 = transform2.rigidbody;
					if (rigidbody2)
					{
						parent = transform2.parent;
						num = 0;
						while (parent != transform)
						{
							if (parent.rigidbody && ++num > this.ignoreCollisionDownSteps)
							{
								break;
							}
							parent = parent.parent;
						}
						if (num < this.ignoreCollisionDownSteps)
						{
							foreach (Collider a2 in keyValuePair.Value)
							{
								foreach (Collider b2 in dictionary[transform2.rigidbody])
								{
									hashSet.Add(AuthorHull.MakeKV(a2, b2));
								}
							}
						}
					}
				}
			}
		}
		int count = hashSet.Count;
		if (count > 0)
		{
			Collider[] array = new Collider[count];
			Collider[] array2 = new Collider[count];
			int num2 = 0;
			foreach (KeyValuePair<Collider, Collider> keyValuePair2 in hashSet)
			{
				array[num2] = keyValuePair2.Key;
				array2[num2] = keyValuePair2.Value;
				num2++;
			}
			IgnoreColliders ignoreColliders = gameObject.AddComponent<IgnoreColliders>();
			ignoreColliders.a = array;
			ignoreColliders.b = array2;
		}
		this.CreateEyes(gameObject);
		if (this.ragdollPrototype)
		{
			this.ApplyPrototype(gameObject, this.ragdollPrototype);
		}
		this.ApplyRig(gameObject);
		return gameObject;
	}

	// Token: 0x060001B9 RID: 441 RVA: 0x0000A098 File Offset: 0x00008298
	private static AuthorShared.AttributeKeyValueList GenKVL(GameObject hitBox, GameObject rigid)
	{
		return new AuthorShared.AttributeKeyValueList(new object[]
		{
			AuthTarg.HitBox,
			hitBox,
			AuthTarg.Ragdoll,
			rigid
		});
	}

	// Token: 0x060001BA RID: 442 RVA: 0x0000A0C0 File Offset: 0x000082C0
	private void GeneratePrefabInstances()
	{
		this.DestroyRepresentations(ref this.generatedRigid, "::RAGDOLL_OUTPUT::");
		this.generatedRigid = this.MakeColliderPrefab();
		this.DestroyRepresentations(ref this.generatedHitBox, "::HITBOX_OUTPUT::");
		this.generatedHitBox = this.MakeHitBoxPrefab();
		if (this.generatedHitBox && this.generatedRigid)
		{
			AuthorShared.AttributeKeyValueList attributeKeyValueList = AuthorHull.GenKVL(this.generatedHitBox, this.generatedRigid);
			attributeKeyValueList.Run(this.generatedHitBox);
			attributeKeyValueList.Run(this.generatedRigid);
			List<KeyValuePair<MethodInfo, MonoBehaviour>> list = this.CaptureFinalizeMethods(this.generatedHitBox, "OnAuthoredAsHitBoxPrefab");
			List<KeyValuePair<MethodInfo, MonoBehaviour>> list2 = this.CaptureFinalizeMethods(this.generatedRigid, "OnAuthoredAsRagdollPrefab");
			object[] parameters = new object[]
			{
				this.generatedRigid
			};
			foreach (KeyValuePair<MethodInfo, MonoBehaviour> keyValuePair in list)
			{
				if (keyValuePair.Value)
				{
					try
					{
						keyValuePair.Key.Invoke(keyValuePair.Value, parameters);
					}
					catch (Exception ex)
					{
						Debug.LogException(ex, keyValuePair.Value);
					}
				}
			}
			object[] parameters2 = new object[]
			{
				this.generatedHitBox
			};
			foreach (KeyValuePair<MethodInfo, MonoBehaviour> keyValuePair2 in list2)
			{
				if (keyValuePair2.Value)
				{
					try
					{
						keyValuePair2.Key.Invoke(keyValuePair2.Value, parameters2);
					}
					catch (Exception ex2)
					{
						Debug.LogException(ex2, keyValuePair2.Value);
					}
				}
			}
		}
		AuthorShared.SetDirty(this.generatedRigid);
		AuthorShared.SetDirty(this.generatedHitBox);
	}

	// Token: 0x060001BB RID: 443 RVA: 0x0000A2FC File Offset: 0x000084FC
	private GameObject CreateEyes(GameObject output)
	{
		return new GameObject("Eyes")
		{
			transform = 
			{
				parent = output.transform,
				localPosition = new Vector3(0f, this.eyeHeight, 0f)
			}
		};
	}

	// Token: 0x060001BC RID: 444 RVA: 0x0000A348 File Offset: 0x00008548
	private GameObject MakeHitBoxPrefab()
	{
		GameObject result;
		try
		{
			GameObject gameObject = this.InstantiatePrefabWithRemovedBones((!this.modelPrefabForHitBox) ? this.modelPrefab : this.modelPrefabForHitBox);
			GameObject gameObject2 = gameObject;
			gameObject2.name += "::HITBOX_OUTPUT::";
			this.ApplyMaterials(gameObject);
			SkinnedMeshRenderer skinnedMeshRenderer;
			AuthorShared.GetRootBone(gameObject, out skinnedMeshRenderer);
			GameObject gameObject3 = new GameObject("HB Hit", new Type[]
			{
				typeof(CapsuleCollider),
				typeof(Rigidbody)
			})
			{
				layer = LayerMask.NameToLayer(this.hitBoxLayerName)
			};
			gameObject3.transform.parent = ((!skinnedMeshRenderer.transform.parent) ? gameObject.transform : skinnedMeshRenderer.transform.parent);
			CapsuleCollider capsuleCollider = gameObject3.collider as CapsuleCollider;
			capsuleCollider.center = this.hitCapsuleCenter;
			capsuleCollider.height = this.hitCapsuleHeight;
			capsuleCollider.radius = this.hitCapsuleRadius;
			capsuleCollider.direction = this.hitCapsuleDirection;
			capsuleCollider.isTrigger = false;
			capsuleCollider.rigidbody.isKinematic = true;
			gameObject3.layer = LayerMask.NameToLayer("Hitbox");
			HitBoxSystem hitBoxSystem = this.creatingSystem = this.CreateHitBoxSystem(gameObject3);
			if (hitBoxSystem.bodyParts == null)
			{
				hitBoxSystem.bodyParts = new IDRemoteBodyPartCollection();
			}
			List<HitShape> list = new List<HitShape>();
			int? layerIndex;
			if (string.IsNullOrEmpty(this.defaultBodyPartLayer))
			{
				layerIndex = null;
			}
			else
			{
				layerIndex = new int?(LayerMask.NameToLayer(this.defaultBodyPartLayer));
			}
			foreach (AuthorPeice authorPeice in base.EnumeratePeices())
			{
				if (authorPeice && authorPeice is AuthorChHit)
				{
					((AuthorChHit)authorPeice).CreateHitBoxOn(list, gameObject.transform, this.modelPrefabInstance.transform, layerIndex);
				}
			}
			int i = 0;
			int num = list.Count;
			while (i < num)
			{
				if (list[i] == null)
				{
					list.RemoveAt(i--);
					num--;
				}
				i++;
			}
			list.Sort(HitShape.prioritySorter);
			hitBoxSystem.shapes = list.ToArray();
			foreach (HitBox hitBox in gameObject.GetComponentsInChildren<HitBox>())
			{
				try
				{
					IDRemoteBodyPart idremoteBodyPart;
					bool flag = hitBoxSystem.bodyParts.TryGetValue(hitBox.bodyPart, ref idremoteBodyPart);
					hitBoxSystem.bodyParts[hitBox.bodyPart] = hitBox;
					foreach (Collider collider in hitBox.GetComponents<Collider>())
					{
						Object.DestroyImmediate(collider);
					}
					if (flag)
					{
						Debug.LogWarning(string.Concat(new object[]
						{
							"Overwrite ",
							hitBox.bodyPart,
							". Was ",
							idremoteBodyPart,
							", now ",
							hitBox
						}), hitBox);
					}
				}
				catch (Exception arg)
				{
					Debug.LogError(string.Format("{0}:{2}:{1}", hitBox, arg, hitBox.bodyPart));
					throw;
				}
			}
			AuthorShared.SetDirty(hitBoxSystem);
			this.CreateEyes(gameObject);
			IDMain idmain = this.ApplyPrototype(gameObject, this.prototype);
			this.ApplyRig(gameObject);
			AuthorShared.SetDirty(gameObject);
			result = gameObject;
		}
		finally
		{
			this.creatingSystem = null;
		}
		return result;
	}

	// Token: 0x060001BD RID: 445 RVA: 0x0000A740 File Offset: 0x00008940
	private IDMain ApplyPrototype(GameObject output, IDMain prototype)
	{
		IDMain result = null;
		if (prototype)
		{
			Component[] components = prototype.GetComponents<Component>();
			Component[] array = new Component[components.Length];
			Component[] array2 = new Component[components.Length];
			int num = components.Length;
			int num2 = -1;
			int num3 = 500;
			int num4;
			do
			{
				num4 = 0;
				for (int i = 0; i < num; i++)
				{
					Component component = components[i];
					if (!component)
					{
						components[i] = null;
					}
					else if (component is Transform)
					{
						components[i] = null;
						array[i] = component;
						array2[num2 = i] = output.transform;
					}
					else
					{
						Component component2 = output.AddComponent(component.GetType());
						if (component2)
						{
							array2[i] = component2;
							components[i] = null;
							array[i] = component;
							if (component2 is IDMain)
							{
								result = (IDMain)component2;
							}
						}
						else
						{
							num4++;
						}
					}
				}
			}
			while (num4 != 0 || num3-- <= 0);
			if (num3 < 0)
			{
				Debug.LogError("Couldnt remake all components");
			}
			for (int j = 0; j < 2; j++)
			{
				for (int k = 0; k < num; k++)
				{
					if (k != num2)
					{
						Component component3 = array[k];
						Component component4 = array2[k];
						if (component3)
						{
							if (component4)
							{
								this.TransferComponentSettings(prototype.gameObject, output, array, array2, component3, component4);
								AuthorShared.SetDirty(component4);
							}
							else
							{
								Debug.LogWarning("no dest for source " + component3, component3);
							}
						}
						else if (component4)
						{
							Debug.LogWarning("no source for dest " + component4, component4);
						}
						else
						{
							Debug.LogWarning("no source or dest", output);
						}
					}
				}
			}
			output.layer = prototype.gameObject.layer;
			output.tag = prototype.gameObject.tag;
		}
		return result;
	}

	// Token: 0x060001BE RID: 446 RVA: 0x0000A93C File Offset: 0x00008B3C
	private bool ApplyRig(GameObject output)
	{
		bool result = false;
		if (this.actorRig)
		{
			BoneStructure.EditorOnly_AddOrUpdateBoneStructure(output, this.actorRig);
			result = true;
		}
		else
		{
			BoneStructure component = output.GetComponent<BoneStructure>();
			if (component)
			{
				Object.DestroyImmediate(component, true);
			}
		}
		return result;
	}

	// Token: 0x060001BF RID: 447 RVA: 0x0000A988 File Offset: 0x00008B88
	private List<KeyValuePair<MethodInfo, MonoBehaviour>> CaptureFinalizeMethods(GameObject output, string methodName)
	{
		List<KeyValuePair<MethodInfo, MonoBehaviour>> list = new List<KeyValuePair<MethodInfo, MonoBehaviour>>();
		foreach (MonoBehaviour monoBehaviour in output.GetComponentsInChildren<MonoBehaviour>(true))
		{
			if (monoBehaviour)
			{
				foreach (MethodInfo methodInfo in monoBehaviour.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
				{
					if (methodInfo.Name == methodName)
					{
						list.Add(new KeyValuePair<MethodInfo, MonoBehaviour>(methodInfo, monoBehaviour));
					}
				}
			}
		}
		return list;
	}

	// Token: 0x060001C0 RID: 448 RVA: 0x0000AA18 File Offset: 0x00008C18
	private static bool ActorRigSearch(MemberInfo m, object filterCriteria)
	{
		return ((FieldInfo)m).FieldType == typeof(ActorRig);
	}

	// Token: 0x060001C1 RID: 449 RVA: 0x0000AA34 File Offset: 0x00008C34
	private void TransferComponentSettings(GameObject srcGO, GameObject dstGO, Component[] srcComponents, Component[] dstComponents, Component src, Component dst)
	{
		if (!(src is MonoBehaviour) && src is SkinnedMeshRenderer)
		{
			Debug.LogWarning("Cannot copy skinned mesh renderers");
			return;
		}
	}

	// Token: 0x060001C2 RID: 450 RVA: 0x0000AA5C File Offset: 0x00008C5C
	private void TransferComponentSettings(NavMeshAgent src, NavMeshAgent dst)
	{
		dst.radius = src.radius;
		dst.speed = src.speed;
		dst.acceleration = src.acceleration;
		dst.angularSpeed = src.angularSpeed;
		dst.stoppingDistance = src.stoppingDistance;
		dst.autoTraverseOffMeshLink = src.autoTraverseOffMeshLink;
		dst.autoRepath = src.autoRepath;
		dst.height = src.height;
		dst.baseOffset = src.baseOffset;
		dst.obstacleAvoidanceType = src.obstacleAvoidanceType;
		dst.walkableMask = src.walkableMask;
		dst.enabled = src.enabled;
	}

	// Token: 0x060001C3 RID: 451 RVA: 0x0000AAFC File Offset: 0x00008CFC
	private void ChangedModelPrefab()
	{
		if (this.modelPrefabInstance)
		{
			Object.DestroyImmediate(this.modelPrefabInstance);
		}
		this.modelPrefabInstance = AuthorShared.InstantiatePrefab(this.modelPrefab);
		this.modelPrefabInstance.transform.localPosition = Vector3.zero;
		this.modelPrefabInstance.transform.localRotation = Quaternion.identity;
		this.modelPrefabInstance.transform.localScale = Vector3.one;
	}

	// Token: 0x060001C4 RID: 452 RVA: 0x0000AB74 File Offset: 0x00008D74
	protected override IEnumerable<AuthorPalletObject> EnumeratePalletObjects()
	{
		AuthorPalletObject[] pallet = AuthorHull.HitBoxItems.pallet;
		if (!AuthorHull.once)
		{
			pallet[0].guiContent.image = AuthorShared.ObjectContent(null, typeof(BoxCollider)).image;
			pallet[1].guiContent.image = AuthorShared.ObjectContent(null, typeof(SphereCollider)).image;
			pallet[2].guiContent.image = AuthorShared.ObjectContent(null, typeof(CapsuleCollider)).image;
			AuthorHull.once = true;
		}
		return pallet;
	}

	// Token: 0x060001C5 RID: 453 RVA: 0x0000AC08 File Offset: 0x00008E08
	private void OnDrawGizmosSelected()
	{
		if (this.modelPrefabInstance)
		{
			Gizmos.matrix = this.modelPrefabInstance.transform.localToWorldMatrix;
			Transform hitColliderParent = this.GetHitColliderParent(this.modelPrefabInstance);
			if (hitColliderParent)
			{
				Gizmos.matrix = hitColliderParent.localToWorldMatrix;
				Gizmos2.DrawWireCapsule(this.hitCapsuleCenter, this.hitCapsuleRadius, this.hitCapsuleHeight, this.hitCapsuleDirection);
			}
		}
	}

	// Token: 0x060001C6 RID: 454 RVA: 0x0000AC7C File Offset: 0x00008E7C
	private static void WriteJSONGUID(JSONStream stream, Object obj)
	{
		string text = AuthorShared.GetAssetPath(obj);
		string text2 = null;
		if (text == string.Empty)
		{
			text = null;
		}
		else
		{
			text2 = AuthorShared.PathToGUID(text);
			if (string.IsNullOrEmpty(text2))
			{
				text2 = null;
			}
		}
		string text3;
		if (obj)
		{
			text3 = obj.GetType().AssemblyQualifiedName;
		}
		else
		{
			text3 = null;
		}
		stream.WriteObjectStart();
		stream.WriteText("path", text);
		stream.WriteText("guid", text2);
		stream.WriteText("type", text3);
		stream.WriteObjectEnd();
	}

	// Token: 0x060001C7 RID: 455 RVA: 0x0000AD0C File Offset: 0x00008F0C
	private static void WriteJSONGUID(JSONStream stream, string property, Object obj)
	{
		stream.WriteProperty(property);
		AuthorHull.WriteJSONGUID(stream, obj);
	}

	// Token: 0x060001C8 RID: 456 RVA: 0x0000AD1C File Offset: 0x00008F1C
	protected override void SaveSettings(JSONStream stream)
	{
		stream.WriteObjectStart();
		stream.WriteObjectStart("types");
		stream.WriteText("hitboxsystem", this.hitBoxSystemType);
		stream.WriteText("hitbox", this.hitBoxType);
		stream.WriteObjectEnd();
		stream.WriteObjectStart("assets");
		AuthorHull.WriteJSONGUID(stream, "model", this.modelPrefabInstance);
		stream.WriteArrayStart("materials");
		if (this.materials != null)
		{
			for (int i = 0; i < this.materials.Length; i++)
			{
				AuthorHull.WriteJSONGUID(stream, this.materials[i]);
			}
		}
		stream.WriteArrayEnd();
		stream.WriteObjectStart("bodyparts");
		foreach (BodyPartPair<Transform> bodyPartPair in this.bodyParts)
		{
			stream.WriteText(bodyPartPair.key.ToString(), AuthorShared.CalculatePath(bodyPartPair.value, this.modelPrefabInstance.transform));
		}
		stream.WriteObjectEnd();
		stream.WriteArrayStart("peices");
		foreach (AuthorPeice authorPeice in base.EnumeratePeices())
		{
			stream.WriteObjectStart();
			stream.WriteText("type", authorPeice.GetType().AssemblyQualifiedName);
			stream.WriteText("id", authorPeice.peiceID);
			stream.WriteObjectStart("instance");
			authorPeice.SaveJsonProperties(stream);
			stream.WriteObjectEnd();
			stream.WriteObjectEnd();
		}
		stream.WriteArrayEnd();
		stream.WriteObjectEnd();
	}

	// Token: 0x060001C9 RID: 457 RVA: 0x0000AF04 File Offset: 0x00009104
	protected override void LoadSettings(JSONStream stream)
	{
		stream.ReadSkip();
	}

	// Token: 0x060001CA RID: 458 RVA: 0x0000AF10 File Offset: 0x00009110
	public override string RootBonePath(AuthorPeice callingPeice, Transform bone)
	{
		return AuthorShared.CalculatePath(bone, this.modelPrefabInstance.transform);
	}

	// Token: 0x060001CB RID: 459 RVA: 0x0000AF24 File Offset: 0x00009124
	[Conditional("EXPECT_CRASH")]
	private static void PreCrashLog(string text)
	{
		Debug.Log(text);
	}

	// Token: 0x060001CC RID: 460 RVA: 0x0000AF2C File Offset: 0x0000912C
	[Conditional("LOG_GENERATE")]
	private static void GenerateLog(object text)
	{
		Debug.Log(text);
	}

	// Token: 0x060001CD RID: 461 RVA: 0x0000AF34 File Offset: 0x00009134
	[Conditional("LOG_GENERATE")]
	private static void GenerateLog(object text, Object obj)
	{
		Debug.Log(text, obj);
	}

	// Token: 0x060001CE RID: 462 RVA: 0x0000AF40 File Offset: 0x00009140
	protected void PreviewPrototype()
	{
		AuthorCreationProject authorCreationProject;
		using (Stream stream = base.GetStream(true, "protoprev", out authorCreationProject))
		{
			if (stream != null)
			{
				using (JSONStream jsonstream = JSONStream.CreateWriter(stream))
				{
					if (jsonstream == null)
					{
					}
				}
			}
		}
	}

	// Token: 0x060001CF RID: 463 RVA: 0x0000AFCC File Offset: 0x000091CC
	private void UpdatePrefabs()
	{
	}

	// Token: 0x060001D0 RID: 464 RVA: 0x0000AFD0 File Offset: 0x000091D0
	private bool EnsureItsAPrefab(ref Object obj)
	{
		return !obj;
	}

	// Token: 0x040000F7 RID: 247
	private const string suffix_rigid = "::RAGDOLL_OUTPUT::";

	// Token: 0x040000F8 RID: 248
	private const string suffix_hitbox = "::HITBOX_OUTPUT::";

	// Token: 0x040000F9 RID: 249
	[SerializeField]
	private GameObject modelPrefab;

	// Token: 0x040000FA RID: 250
	[SerializeField]
	private GameObject modelPrefabForHitBox;

	// Token: 0x040000FB RID: 251
	[SerializeField]
	private GameObject modelPrefabInstance;

	// Token: 0x040000FC RID: 252
	[SerializeField]
	private GameObject hitBoxOutputPrefab;

	// Token: 0x040000FD RID: 253
	[SerializeField]
	private GameObject ragdollOutputPrefab;

	// Token: 0x040000FE RID: 254
	[SerializeField]
	private Vector3 hitCapsuleCenter;

	// Token: 0x040000FF RID: 255
	[SerializeField]
	private float hitCapsuleRadius = 1f;

	// Token: 0x04000100 RID: 256
	[SerializeField]
	private float hitCapsuleHeight = 2.5f;

	// Token: 0x04000101 RID: 257
	[SerializeField]
	private int hitCapsuleDirection;

	// Token: 0x04000102 RID: 258
	[SerializeField]
	private bool drawBones;

	// Token: 0x04000103 RID: 259
	[SerializeField]
	private bool allowBonesOutsideOfModelPrefab;

	// Token: 0x04000104 RID: 260
	[SerializeField]
	private int ignoreCollisionDownSteps = 2;

	// Token: 0x04000105 RID: 261
	[SerializeField]
	private int ignoreCollisionUpSteps = 1;

	// Token: 0x04000106 RID: 262
	[SerializeField]
	private string hitBoxType = "HitBox";

	// Token: 0x04000107 RID: 263
	[SerializeField]
	private string hitBoxSystemType = "HitBoxSystem";

	// Token: 0x04000108 RID: 264
	[SerializeField]
	private string defaultBodyPartLayer = string.Empty;

	// Token: 0x04000109 RID: 265
	[SerializeField]
	private ActorRig actorRig;

	// Token: 0x0400010A RID: 266
	[SerializeField]
	private Character prototype;

	// Token: 0x0400010B RID: 267
	[SerializeField]
	private Ragdoll ragdollPrototype;

	// Token: 0x0400010C RID: 268
	[SerializeField]
	private Transform hitCapsuleTransform;

	// Token: 0x0400010D RID: 269
	[SerializeField]
	private Transform[] removeThese;

	// Token: 0x0400010E RID: 270
	[SerializeField]
	private float eyeHeight = 1f;

	// Token: 0x0400010F RID: 271
	[SerializeField]
	private GameObject generatedRigid;

	// Token: 0x04000110 RID: 272
	[SerializeField]
	private GameObject generatedHitBox;

	// Token: 0x04000111 RID: 273
	[SerializeField]
	private bool savedGenerated;

	// Token: 0x04000112 RID: 274
	[SerializeField]
	private Material[] materials;

	// Token: 0x04000113 RID: 275
	[SerializeField]
	private Vector3 editingAngles = Vector3.zero;

	// Token: 0x04000114 RID: 276
	[SerializeField]
	private bool editingCenterToRoot;

	// Token: 0x04000115 RID: 277
	[SerializeField]
	private BodyPartTransformMap bodyParts = new BodyPartTransformMap();

	// Token: 0x04000116 RID: 278
	[SerializeField]
	private string saveJSONGUID;

	// Token: 0x04000117 RID: 279
	[SerializeField]
	private string previewPrototypeGUID;

	// Token: 0x04000118 RID: 280
	[SerializeField]
	private bool removeAnimationFromRagdoll;

	// Token: 0x04000119 RID: 281
	[SerializeField]
	private string hitBoxLayerName = "Hitbox";

	// Token: 0x0400011A RID: 282
	[SerializeField]
	private bool useMeshesFromHitBoxOnRagdoll;

	// Token: 0x0400011B RID: 283
	private static bool once;

	// Token: 0x0400011C RID: 284
	private HitBoxSystem creatingSystem;

	// Token: 0x0400011D RID: 285
	private bool showAllBones;

	// Token: 0x0400011E RID: 286
	private static readonly MemberFilter actorRigSearch = new MemberFilter(AuthorHull.ActorRigSearch);

	// Token: 0x0200002B RID: 43
	private static class guis
	{
		// Token: 0x04000120 RID: 288
		public static readonly GUIContent overridingContent = new GUIContent("[overriding the hitbox output prefab]", "The HitBox prefab output will use the overriding mesh prefab provided, You must make sure that the heirarchy matches between the two");

		// Token: 0x04000121 RID: 289
		public static readonly GUIContent notOverridingContent = new GUIContent("[both outputs will use the same base]", "The HitBox prefab output will use the same mesh prefab as the one for the rigidbody");

		// Token: 0x04000122 RID: 290
		public static readonly GUIContent destroyDrop = new GUIContent("Destroy bone", "Drag a transform off the model instance that contains no ::'s");
	}

	// Token: 0x0200002C RID: 44
	private static class HitBoxItems
	{
		// Token: 0x060001D4 RID: 468 RVA: 0x0000B124 File Offset: 0x00009324
		private static bool ValidateByID(AuthorCreation creation, AuthorPalletObject palletObject)
		{
			return true;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000B128 File Offset: 0x00009328
		private static AuthorPeice CreateByID<TPeice>(AuthorCreation creation, AuthorPalletObject palletObject) where TPeice : AuthorPeice
		{
			GameObject gameObject = new GameObject(palletObject.guiContent.text, new Type[]
			{
				typeof(TPeice)
			});
			TPeice component = gameObject.GetComponent<TPeice>();
			component.peiceID = palletObject.guiContent.text;
			return component;
		}

		// Token: 0x04000123 RID: 291
		public static readonly AuthorPalletObject.Validator validateByID = new AuthorPalletObject.Validator(AuthorHull.HitBoxItems.ValidateByID);

		// Token: 0x04000124 RID: 292
		public static readonly AuthorPalletObject.Creator createSocketByID = new AuthorPalletObject.Creator(AuthorHull.HitBoxItems.CreateByID<AuthorChHit>);

		// Token: 0x04000125 RID: 293
		public static readonly AuthorPalletObject[] pallet = new AuthorPalletObject[]
		{
			new AuthorPalletObject
			{
				guiContent = new GUIContent("Box"),
				validator = AuthorHull.HitBoxItems.validateByID,
				creator = AuthorHull.HitBoxItems.createSocketByID
			},
			new AuthorPalletObject
			{
				guiContent = new GUIContent("Sphere"),
				validator = AuthorHull.HitBoxItems.validateByID,
				creator = AuthorHull.HitBoxItems.createSocketByID
			},
			new AuthorPalletObject
			{
				guiContent = new GUIContent("Capsule"),
				validator = AuthorHull.HitBoxItems.validateByID,
				creator = AuthorHull.HitBoxItems.createSocketByID
			}
		};
	}
}
