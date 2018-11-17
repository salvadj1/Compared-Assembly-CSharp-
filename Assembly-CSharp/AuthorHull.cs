using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Facepunch.Actor;
using UnityEngine;

// Token: 0x02000037 RID: 55
[global::AuthorSuiteCreation(Title = "Author Hull", Description = "Create a new character. Allows you to define hitboxes and fine tune ragdoll and joints.", Scripter = "Pat", OutputType = typeof(global::Character), Ready = true)]
public class AuthorHull : global::AuthorCreation
{
	// Token: 0x060001F5 RID: 501 RVA: 0x00009348 File Offset: 0x00007548
	public AuthorHull() : this(typeof(global::Character))
	{
	}

	// Token: 0x060001F6 RID: 502 RVA: 0x0000935C File Offset: 0x0000755C
	protected AuthorHull(Type type) : base(type)
	{
	}

	// Token: 0x060001F8 RID: 504 RVA: 0x000093F8 File Offset: 0x000075F8
	public global::HitBox CreateHitBox(GameObject target)
	{
		global::HitBox hitBox = global::AuthorShared.AddComponent<global::HitBox>(target, this.hitBoxType);
		global::AuthorShared.SetSerializedProperty(hitBox, "_hitBoxSystem", this.creatingSystem);
		hitBox.idMain = hitBox.hitBoxSystem.idMain;
		return hitBox;
	}

	// Token: 0x060001F9 RID: 505 RVA: 0x00009438 File Offset: 0x00007638
	public global::HitBoxSystem CreateHitBoxSystem(GameObject target)
	{
		return global::AuthorShared.AddComponent<global::HitBoxSystem>(target, this.hitBoxSystemType);
	}

	// Token: 0x060001FA RID: 506 RVA: 0x00009448 File Offset: 0x00007648
	private Transform GetHitColliderParent(GameObject root)
	{
		SkinnedMeshRenderer skinnedMeshRenderer;
		Transform rootBone = global::AuthorShared.GetRootBone(root, out skinnedMeshRenderer);
		return (!skinnedMeshRenderer || !skinnedMeshRenderer.transform.parent) ? rootBone : skinnedMeshRenderer.transform.parent;
	}

	// Token: 0x060001FB RID: 507 RVA: 0x00009490 File Offset: 0x00007690
	public override IEnumerable<global::AuthorPeice> DoSceneView()
	{
		if (this.drawBones && this.modelPrefabInstance != null)
		{
			Transform rootBone = global::AuthorShared.GetRootBone(this.modelPrefabInstance);
			if (rootBone)
			{
				Color color = global::AuthorShared.Scene.color;
				Color color2 = color * new Color(0.9f, 0.8f, 0.3f, 0.1f);
				List<Transform> list = rootBone.ListDecendantsByDepth();
				global::AuthorShared.Scene.color = color2;
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
						global::AuthorShared.Scene.DrawBone(position, rot, magnitude, Mathf.Min(magnitude / 2f, 0.025f), Vector3.one * Mathf.Min(magnitude, 0.05f));
					}
				}
				global::AuthorShared.Scene.color = color;
			}
		}
		return base.DoSceneView();
	}

	// Token: 0x060001FC RID: 508 RVA: 0x000095E4 File Offset: 0x000077E4
	private void ApplyMaterials(GameObject instance)
	{
		SkinnedMeshRenderer skinnedMeshRenderer = (!(instance == null)) ? instance.GetComponentInChildren<SkinnedMeshRenderer>() : null;
		if (skinnedMeshRenderer)
		{
			skinnedMeshRenderer.sharedMaterials = this.materials;
		}
	}

	// Token: 0x060001FD RID: 509 RVA: 0x00009624 File Offset: 0x00007824
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

	// Token: 0x060001FE RID: 510 RVA: 0x000096AC File Offset: 0x000078AC
	protected override bool OnGUICreationSettings()
	{
		bool flag = base.OnGUICreationSettings();
		bool flag2 = this.modelPrefab;
		GameObject gameObject = (GameObject)global::AuthorShared.ObjectField("Model Prefab", this.modelPrefab, typeof(GameObject), true, new GUILayoutOption[0]);
		if (gameObject != this.modelPrefab)
		{
			if (!gameObject)
			{
				gameObject = this.modelPrefab;
			}
			else if (global::AuthorShared.GetObjectKind(gameObject) != global::AuthorShared.ObjectKind.Model)
			{
				gameObject = this.modelPrefab;
			}
			else
			{
				gameObject = global::AuthorShared.FindPrefabRoot(gameObject);
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
		GameObject gameObject2 = (GameObject)global::AuthorShared.ObjectField("Override Model Prefab [HitBox]", (!flag3) ? this.modelPrefab : this.modelPrefabForHitBox, typeof(GameObject), true, new GUILayoutOption[0]);
		GUI.enabled = enabled;
		if (!gameObject2 || gameObject2 == this.modelPrefab)
		{
			if (flag2)
			{
				GUILayout.Label(global::AuthorHull.guis.notOverridingContent, global::AuthorShared.Styles.miniLabel, new GUILayoutOption[0]);
			}
			gameObject2 = null;
		}
		else
		{
			GUILayout.Label(global::AuthorHull.guis.overridingContent, global::AuthorShared.Styles.miniLabel, new GUILayoutOption[0]);
			bool flag4 = global::AuthorShared.Toggle("Use Meshes from Override in Ragdoll output", this.useMeshesFromHitBoxOnRagdoll, new GUILayoutOption[0]);
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
			else if (global::AuthorShared.GetObjectKind(gameObject2) != global::AuthorShared.ObjectKind.Model)
			{
				gameObject2 = this.modelPrefabForHitBox;
			}
			else
			{
				gameObject2 = global::AuthorShared.FindPrefabRoot(gameObject2);
			}
		}
		if (gameObject2 != this.modelPrefabForHitBox)
		{
			this.modelPrefabForHitBox = gameObject2;
			flag |= true;
		}
		ActorRig actorRig = (ActorRig)global::AuthorShared.ObjectField("Actor Rig", this.actorRig, typeof(ActorRig), global::AuthorShared.ObjectFieldFlags.Asset, new GUILayoutOption[0]);
		if (actorRig != this.actorRig && !actorRig)
		{
			actorRig = this.actorRig;
		}
		if (actorRig != this.actorRig)
		{
			this.actorRig = actorRig;
			flag |= true;
		}
		global::Character character = (global::Character)global::AuthorShared.ObjectField("Prototype Prefab", this.prototype, typeof(IDMain), global::AuthorShared.ObjectFieldFlags.Prefab, new GUILayoutOption[0]);
		if (character != this.prototype && character && global::AuthorShared.GetObjectKind(character.gameObject) != global::AuthorShared.ObjectKind.Prefab)
		{
			character = this.prototype;
		}
		if (character != this.prototype)
		{
			this.prototype = character;
			flag |= true;
		}
		global::Ragdoll ragdoll = (global::Ragdoll)global::AuthorShared.ObjectField("Prototype Ragdoll", this.ragdollPrototype, typeof(IDMain), global::AuthorShared.ObjectFieldFlags.Prefab, new GUILayoutOption[0]);
		if (ragdoll != this.ragdollPrototype && ragdoll && global::AuthorShared.GetObjectKind(ragdoll.gameObject) != global::AuthorShared.ObjectKind.Prefab)
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
			global::AuthorShared.BeginHorizontal(new GUILayoutOption[0]);
			if (global::AuthorShared.Toggle("Show Model Prefab", ref activeSelf, global::AuthorShared.Styles.miniButton, new GUILayoutOption[0]))
			{
				this.modelPrefabInstance.SetActive(activeSelf);
			}
			flag |= global::AuthorShared.Toggle("Render Bones", ref this.drawBones, global::AuthorShared.Styles.miniButton, new GUILayoutOption[0]);
			global::AuthorShared.EndHorizontal();
		}
		global::AuthorShared.BeginSubSection("Rendering", new GUILayoutOption[0]);
		if (global::AuthorShared.ArrayField<Material>("Materials", ref this.materials, delegate(ref Material material)
		{
			return global::AuthorShared.ObjectField<Material>(default(global::AuthorShared.Content), ref material, typeof(Material), (global::AuthorShared.ObjectFieldFlags)0, new GUILayoutOption[0]);
		}))
		{
			flag = true;
			this.ApplyMaterials(this.modelPrefabInstance);
		}
		global::AuthorShared.EndSubSection();
		global::AuthorShared.BeginSubSection("Types", "AddComponent strings", new GUILayoutOption[0]);
		string a = global::AuthorShared.StringField("HitBox Type", this.hitBoxType, new GUILayoutOption[0]);
		string a2 = global::AuthorShared.StringField("HitBoxSystem Type", this.hitBoxSystemType, new GUILayoutOption[0]);
		global::AuthorShared.EndSubSection();
		global::AuthorShared.BeginSubSection("Hit Capsule", "Should be large enough to fit all boxes at any time", new GUILayoutOption[0]);
		Vector3 vector = global::AuthorShared.Vector3Field("Center", this.hitCapsuleCenter, new GUILayoutOption[0]);
		float num = global::AuthorShared.FloatField("Radius", this.hitCapsuleRadius, new GUILayoutOption[0]);
		float num2 = global::AuthorShared.FloatField("Height", this.hitCapsuleHeight, new GUILayoutOption[0]);
		int num3 = global::AuthorShared.IntField("Axis", this.hitCapsuleDirection, new GUILayoutOption[0]);
		float num4 = global::AuthorShared.FloatField("Eye Height", this.eyeHeight, new GUILayoutOption[0]);
		global::AuthorShared.EndSubSection();
		global::AuthorShared.BeginSubSection("Rigidbody", new GUILayoutOption[0]);
		flag |= global::AuthorShared.IntField("Ignore n. parent col.", ref this.ignoreCollisionUpSteps, new GUILayoutOption[0]);
		flag |= global::AuthorShared.IntField("Ignore n. child col.", ref this.ignoreCollisionDownSteps, new GUILayoutOption[0]);
		global::AuthorShared.EndSubSection();
		global::AuthorShared.BeginSubSection("Body Parts", new GUILayoutOption[0]);
		string a3 = global::AuthorShared.StringField("Default Hit Box Layer", this.defaultBodyPartLayer ?? string.Empty, new GUILayoutOption[0]);
		if (string.IsNullOrEmpty(this.defaultBodyPartLayer))
		{
			global::AuthorShared.Label("[the layer in the models will be used]", new GUILayoutOption[0]);
		}
		if (a3 != (this.defaultBodyPartLayer ?? string.Empty))
		{
			this.defaultBodyPartLayer = a3;
			flag = true;
		}
		bool flag5 = this.bodyParts.Count == 0 || global::AuthorShared.Toggle("Show Unassigned Parts", this.showAllBones, new GUILayoutOption[0]);
		for (BodyPart bodyPart = 0; bodyPart < 120; bodyPart++)
		{
			Transform transform;
			if ((this.bodyParts.TryGetValue(bodyPart, ref transform) || this.showAllBones) && global::AuthorShared.ObjectField<Transform>(bodyPart.ToString(), ref transform, (global::AuthorShared.ObjectFieldFlags)17, new GUILayoutOption[0]))
			{
				if (transform)
				{
					BodyPart? bodyPart2 = this.bodyParts.BodyPartOf(transform);
					if (bodyPart2 != null)
					{
						bool? flag6 = global::AuthorShared.Ask(string.Concat(new object[]
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
		global::AuthorShared.BeginSubSection("Destroy Children", new GUILayoutOption[0]);
		global::AuthorShared.BeginSubSection(global::AuthorHull.guis.destroyDrop, "Remove these from generation", global::AuthorShared.Styles.miniLabel, new GUILayoutOption[0]);
		Transform transform2 = (Transform)global::AuthorShared.ObjectField(null, typeof(Transform), (global::AuthorShared.ObjectFieldFlags)25, new GUILayoutOption[0]);
		global::AuthorShared.EndSubSection();
		if (transform2 && (!this.modelPrefabInstance || !transform2.IsChildOf(this.modelPrefabInstance.transform)))
		{
			Debug.Log("Thats not a valid selection", transform2);
			transform2 = null;
		}
		bool flag8 = false;
		if (this.removeThese != null && this.removeThese.Length > 0)
		{
			global::AuthorShared.BeginSubSection("These will be removed with generation", new GUILayoutOption[0]);
			for (int i = 0; i < this.removeThese.Length; i++)
			{
				global::AuthorShared.BeginHorizontal(global::AuthorShared.Styles.gradientOutline, new GUILayoutOption[0]);
				if (global::AuthorShared.Button(global::AuthorShared.ObjectContent<Transform>(this.removeThese[i], typeof(Transform)), global::AuthorShared.Styles.peiceButtonLeft, new GUILayoutOption[0]) && this.removeThese[i])
				{
					global::AuthorShared.PingObject(this.removeThese[i]);
				}
				if (global::AuthorShared.Button(global::AuthorShared.Icon.delete, global::AuthorShared.Styles.peiceButtonRight, new GUILayoutOption[0]))
				{
					this.removeThese[i] = null;
					flag8 = true;
				}
				global::AuthorShared.EndHorizontal();
			}
			global::AuthorShared.EndSubSection();
		}
		global::AuthorShared.EndSubSection();
		global::AuthorShared.EndSubSection();
		global::AuthorShared.BeginSubSection("Output", "this is where stuff will be saved", new GUILayoutOption[0]);
		Object @object = global::AuthorShared.ObjectField("OUTPUT HITBOX", this.hitBoxOutputPrefab, typeof(GameObject), (global::AuthorShared.ObjectFieldFlags)196, new GUILayoutOption[0]);
		Object object2 = global::AuthorShared.ObjectField("OUTPUT RAGDOLL", this.ragdollOutputPrefab, typeof(GameObject), (global::AuthorShared.ObjectFieldFlags)196, new GUILayoutOption[0]);
		global::AuthorShared.EndSubSection();
		global::AuthorShared.BeginSubSection("Authoring Helpers", "These do not output to the mesh, just are here to help you author", new GUILayoutOption[0]);
		Vector3 vector2 = global::AuthorShared.Vector3Field("Angles Offset", this.editingAngles, new GUILayoutOption[0]);
		bool flag9 = global::AuthorShared.Toggle("Origin To Root", this.editingCenterToRoot, new GUILayoutOption[0]);
		global::AuthorShared.EndSubSection();
		global::AuthorShared.BeginHorizontal(global::AuthorShared.Styles.box, new GUILayoutOption[0]);
		bool enabled2 = GUI.enabled;
		if (!gameObject)
		{
			GUI.enabled = false;
		}
		if (global::AuthorShared.Button("Generate", global::AuthorShared.Styles.miniButtonLeft, new GUILayoutOption[0]))
		{
			this.GeneratePrefabInstances();
			this.savedGenerated = false;
			global::AuthorShared.SetDirty(this);
			flag = true;
		}
		GUI.enabled = (!this.savedGenerated && this.generatedRigid && this.generatedHitBox && this.hitBoxOutputPrefab && this.ragdollOutputPrefab && this.ragdollOutputPrefab != this.hitBoxOutputPrefab);
		if (global::AuthorShared.Button("Update Prefabs", global::AuthorShared.Styles.miniButtonRight, new GUILayoutOption[0]) && global::AuthorShared.Ask("This will overwrite any changes made to the output prefab that may have been done externally\r\nStill go ahead?") == true)
		{
			this.UpdatePrefabs();
			this.savedGenerated = true;
			flag = true;
		}
		GUI.enabled = enabled2;
		global::AuthorShared.EndHorizontal();
		if (global::AuthorShared.Button("Save To JSON", new GUILayoutOption[0]))
		{
			base.SaveSettings();
		}
		if (this.prototype && global::AuthorShared.Button("Write JSON Serialized Properties from Prototype", new GUILayoutOption[0]))
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

	// Token: 0x060001FF RID: 511 RVA: 0x0000A490 File Offset: 0x00008690
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

	// Token: 0x06000200 RID: 512 RVA: 0x0000A518 File Offset: 0x00008718
	private void ChangedEditingOptions()
	{
		if (this.modelPrefabInstance)
		{
			this.modelPrefabInstance.transform.localEulerAngles = this.editingAngles;
			this.modelPrefabInstance.transform.localPosition = Vector3.zero;
			if (this.editingCenterToRoot)
			{
				Transform rootBone = global::AuthorShared.GetRootBone(this.modelPrefabInstance.GetComponentInChildren<SkinnedMeshRenderer>());
				if (rootBone)
				{
					this.modelPrefabInstance.transform.position = -rootBone.position;
				}
			}
		}
	}

	// Token: 0x06000201 RID: 513 RVA: 0x0000A5A4 File Offset: 0x000087A4
	private static KeyValuePair<Collider, Collider> MakeKV(Collider a, Collider b)
	{
		if (string.Compare(a.name, b.name) < 0)
		{
			return new KeyValuePair<Collider, Collider>(b, a);
		}
		return new KeyValuePair<Collider, Collider>(a, b);
	}

	// Token: 0x06000202 RID: 514 RVA: 0x0000A5D8 File Offset: 0x000087D8
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

	// Token: 0x06000203 RID: 515 RVA: 0x0000A604 File Offset: 0x00008804
	private GameObject InstantiatePrefabWithRemovedBones(GameObject prefab)
	{
		GameObject gameObject = global::AuthorShared.InstantiatePrefab(prefab);
		if (this.modelPrefabInstance)
		{
			if (this.removeThese != null)
			{
				for (int i = 0; i < this.removeThese.Length; i++)
				{
					if (this.removeThese[i])
					{
						Transform transform = gameObject.transform.FindChild(global::AuthorShared.CalculatePath(this.removeThese[i], this.modelPrefabInstance.transform));
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
						string text = global::AuthorShared.CalculatePath(transform2, gameObject.transform);
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

	// Token: 0x06000204 RID: 516 RVA: 0x0000A740 File Offset: 0x00008940
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
						string text = global::AuthorShared.CalculatePath(renderer.transform, gameObject.transform);
						component.sharedMesh = this.modelPrefabForHitBox.transform.FindChild(text).GetComponent<MeshFilter>().sharedMesh;
						global::AuthorShared.SetDirty(component);
					}
					else if (renderer is SkinnedMeshRenderer)
					{
						((SkinnedMeshRenderer)renderer).sharedMesh = this.modelPrefabForHitBox.transform.FindChild(global::AuthorShared.CalculatePath(renderer.transform, gameObject.transform)).GetComponent<SkinnedMeshRenderer>().sharedMesh;
						global::AuthorShared.SetDirty(renderer);
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
		foreach (global::AuthorPeice authorPeice in base.EnumeratePeices())
		{
			if (authorPeice && authorPeice is global::AuthorChHit)
			{
				((global::AuthorChHit)authorPeice).CreateColliderOn(gameObject.transform, this.modelPrefabInstance.transform, true, layerIndex);
			}
		}
		Transform rootBone = global::AuthorShared.GetRootBone(gameObject);
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
							hashSet.Add(global::AuthorHull.MakeKV(a, b));
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
									hashSet.Add(global::AuthorHull.MakeKV(a2, b2));
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
			global::IgnoreColliders ignoreColliders = gameObject.AddComponent<global::IgnoreColliders>();
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

	// Token: 0x06000205 RID: 517 RVA: 0x0000ADF8 File Offset: 0x00008FF8
	private static global::AuthorShared.AttributeKeyValueList GenKVL(GameObject hitBox, GameObject rigid)
	{
		return new global::AuthorShared.AttributeKeyValueList(new object[]
		{
			global::AuthTarg.HitBox,
			hitBox,
			global::AuthTarg.Ragdoll,
			rigid
		});
	}

	// Token: 0x06000206 RID: 518 RVA: 0x0000AE20 File Offset: 0x00009020
	private void GeneratePrefabInstances()
	{
		this.DestroyRepresentations(ref this.generatedRigid, "::RAGDOLL_OUTPUT::");
		this.generatedRigid = this.MakeColliderPrefab();
		this.DestroyRepresentations(ref this.generatedHitBox, "::HITBOX_OUTPUT::");
		this.generatedHitBox = this.MakeHitBoxPrefab();
		if (this.generatedHitBox && this.generatedRigid)
		{
			global::AuthorShared.AttributeKeyValueList attributeKeyValueList = global::AuthorHull.GenKVL(this.generatedHitBox, this.generatedRigid);
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
		global::AuthorShared.SetDirty(this.generatedRigid);
		global::AuthorShared.SetDirty(this.generatedHitBox);
	}

	// Token: 0x06000207 RID: 519 RVA: 0x0000B05C File Offset: 0x0000925C
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

	// Token: 0x06000208 RID: 520 RVA: 0x0000B0A8 File Offset: 0x000092A8
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
			global::AuthorShared.GetRootBone(gameObject, out skinnedMeshRenderer);
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
			global::HitBoxSystem hitBoxSystem = this.creatingSystem = this.CreateHitBoxSystem(gameObject3);
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
			foreach (global::AuthorPeice authorPeice in base.EnumeratePeices())
			{
				if (authorPeice && authorPeice is global::AuthorChHit)
				{
					((global::AuthorChHit)authorPeice).CreateHitBoxOn(list, gameObject.transform, this.modelPrefabInstance.transform, layerIndex);
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
			foreach (global::HitBox hitBox in gameObject.GetComponentsInChildren<global::HitBox>())
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
			global::AuthorShared.SetDirty(hitBoxSystem);
			this.CreateEyes(gameObject);
			IDMain idmain = this.ApplyPrototype(gameObject, this.prototype);
			this.ApplyRig(gameObject);
			global::AuthorShared.SetDirty(gameObject);
			result = gameObject;
		}
		finally
		{
			this.creatingSystem = null;
		}
		return result;
	}

	// Token: 0x06000209 RID: 521 RVA: 0x0000B4A0 File Offset: 0x000096A0
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
								global::AuthorShared.SetDirty(component4);
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

	// Token: 0x0600020A RID: 522 RVA: 0x0000B69C File Offset: 0x0000989C
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

	// Token: 0x0600020B RID: 523 RVA: 0x0000B6E8 File Offset: 0x000098E8
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

	// Token: 0x0600020C RID: 524 RVA: 0x0000B778 File Offset: 0x00009978
	private static bool ActorRigSearch(MemberInfo m, object filterCriteria)
	{
		return ((FieldInfo)m).FieldType == typeof(ActorRig);
	}

	// Token: 0x0600020D RID: 525 RVA: 0x0000B794 File Offset: 0x00009994
	private void TransferComponentSettings(GameObject srcGO, GameObject dstGO, Component[] srcComponents, Component[] dstComponents, Component src, Component dst)
	{
		if (!(src is MonoBehaviour) && src is SkinnedMeshRenderer)
		{
			Debug.LogWarning("Cannot copy skinned mesh renderers");
			return;
		}
	}

	// Token: 0x0600020E RID: 526 RVA: 0x0000B7BC File Offset: 0x000099BC
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

	// Token: 0x0600020F RID: 527 RVA: 0x0000B85C File Offset: 0x00009A5C
	private void ChangedModelPrefab()
	{
		if (this.modelPrefabInstance)
		{
			Object.DestroyImmediate(this.modelPrefabInstance);
		}
		this.modelPrefabInstance = global::AuthorShared.InstantiatePrefab(this.modelPrefab);
		this.modelPrefabInstance.transform.localPosition = Vector3.zero;
		this.modelPrefabInstance.transform.localRotation = Quaternion.identity;
		this.modelPrefabInstance.transform.localScale = Vector3.one;
	}

	// Token: 0x06000210 RID: 528 RVA: 0x0000B8D4 File Offset: 0x00009AD4
	protected override IEnumerable<global::AuthorPalletObject> EnumeratePalletObjects()
	{
		global::AuthorPalletObject[] pallet = global::AuthorHull.HitBoxItems.pallet;
		if (!global::AuthorHull.once)
		{
			pallet[0].guiContent.image = global::AuthorShared.ObjectContent(null, typeof(BoxCollider)).image;
			pallet[1].guiContent.image = global::AuthorShared.ObjectContent(null, typeof(SphereCollider)).image;
			pallet[2].guiContent.image = global::AuthorShared.ObjectContent(null, typeof(CapsuleCollider)).image;
			global::AuthorHull.once = true;
		}
		return pallet;
	}

	// Token: 0x06000211 RID: 529 RVA: 0x0000B968 File Offset: 0x00009B68
	private void OnDrawGizmosSelected()
	{
		if (this.modelPrefabInstance)
		{
			Gizmos.matrix = this.modelPrefabInstance.transform.localToWorldMatrix;
			Transform hitColliderParent = this.GetHitColliderParent(this.modelPrefabInstance);
			if (hitColliderParent)
			{
				Gizmos.matrix = hitColliderParent.localToWorldMatrix;
				global::Gizmos2.DrawWireCapsule(this.hitCapsuleCenter, this.hitCapsuleRadius, this.hitCapsuleHeight, this.hitCapsuleDirection);
			}
		}
	}

	// Token: 0x06000212 RID: 530 RVA: 0x0000B9DC File Offset: 0x00009BDC
	private static void WriteJSONGUID(JSONStream stream, Object obj)
	{
		string text = global::AuthorShared.GetAssetPath(obj);
		string text2 = null;
		if (text == string.Empty)
		{
			text = null;
		}
		else
		{
			text2 = global::AuthorShared.PathToGUID(text);
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

	// Token: 0x06000213 RID: 531 RVA: 0x0000BA6C File Offset: 0x00009C6C
	private static void WriteJSONGUID(JSONStream stream, string property, Object obj)
	{
		stream.WriteProperty(property);
		global::AuthorHull.WriteJSONGUID(stream, obj);
	}

	// Token: 0x06000214 RID: 532 RVA: 0x0000BA7C File Offset: 0x00009C7C
	protected override void SaveSettings(JSONStream stream)
	{
		stream.WriteObjectStart();
		stream.WriteObjectStart("types");
		stream.WriteText("hitboxsystem", this.hitBoxSystemType);
		stream.WriteText("hitbox", this.hitBoxType);
		stream.WriteObjectEnd();
		stream.WriteObjectStart("assets");
		global::AuthorHull.WriteJSONGUID(stream, "model", this.modelPrefabInstance);
		stream.WriteArrayStart("materials");
		if (this.materials != null)
		{
			for (int i = 0; i < this.materials.Length; i++)
			{
				global::AuthorHull.WriteJSONGUID(stream, this.materials[i]);
			}
		}
		stream.WriteArrayEnd();
		stream.WriteObjectStart("bodyparts");
		foreach (BodyPartPair<Transform> bodyPartPair in this.bodyParts)
		{
			stream.WriteText(bodyPartPair.key.ToString(), global::AuthorShared.CalculatePath(bodyPartPair.value, this.modelPrefabInstance.transform));
		}
		stream.WriteObjectEnd();
		stream.WriteArrayStart("peices");
		foreach (global::AuthorPeice authorPeice in base.EnumeratePeices())
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

	// Token: 0x06000215 RID: 533 RVA: 0x0000BC64 File Offset: 0x00009E64
	protected override void LoadSettings(JSONStream stream)
	{
		stream.ReadSkip();
	}

	// Token: 0x06000216 RID: 534 RVA: 0x0000BC70 File Offset: 0x00009E70
	public override string RootBonePath(global::AuthorPeice callingPeice, Transform bone)
	{
		return global::AuthorShared.CalculatePath(bone, this.modelPrefabInstance.transform);
	}

	// Token: 0x06000217 RID: 535 RVA: 0x0000BC84 File Offset: 0x00009E84
	[Conditional("EXPECT_CRASH")]
	private static void PreCrashLog(string text)
	{
		Debug.Log(text);
	}

	// Token: 0x06000218 RID: 536 RVA: 0x0000BC8C File Offset: 0x00009E8C
	[Conditional("LOG_GENERATE")]
	private static void GenerateLog(object text)
	{
		Debug.Log(text);
	}

	// Token: 0x06000219 RID: 537 RVA: 0x0000BC94 File Offset: 0x00009E94
	[Conditional("LOG_GENERATE")]
	private static void GenerateLog(object text, Object obj)
	{
		Debug.Log(text, obj);
	}

	// Token: 0x0600021A RID: 538 RVA: 0x0000BCA0 File Offset: 0x00009EA0
	protected void PreviewPrototype()
	{
		global::AuthorCreationProject authorCreationProject;
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

	// Token: 0x0600021B RID: 539 RVA: 0x0000BD2C File Offset: 0x00009F2C
	private void UpdatePrefabs()
	{
	}

	// Token: 0x0600021C RID: 540 RVA: 0x0000BD30 File Offset: 0x00009F30
	private bool EnsureItsAPrefab(ref Object obj)
	{
		return !obj;
	}

	// Token: 0x04000137 RID: 311
	private const string suffix_rigid = "::RAGDOLL_OUTPUT::";

	// Token: 0x04000138 RID: 312
	private const string suffix_hitbox = "::HITBOX_OUTPUT::";

	// Token: 0x04000139 RID: 313
	[SerializeField]
	private GameObject modelPrefab;

	// Token: 0x0400013A RID: 314
	[SerializeField]
	private GameObject modelPrefabForHitBox;

	// Token: 0x0400013B RID: 315
	[SerializeField]
	private GameObject modelPrefabInstance;

	// Token: 0x0400013C RID: 316
	[SerializeField]
	private GameObject hitBoxOutputPrefab;

	// Token: 0x0400013D RID: 317
	[SerializeField]
	private GameObject ragdollOutputPrefab;

	// Token: 0x0400013E RID: 318
	[SerializeField]
	private Vector3 hitCapsuleCenter;

	// Token: 0x0400013F RID: 319
	[SerializeField]
	private float hitCapsuleRadius = 1f;

	// Token: 0x04000140 RID: 320
	[SerializeField]
	private float hitCapsuleHeight = 2.5f;

	// Token: 0x04000141 RID: 321
	[SerializeField]
	private int hitCapsuleDirection;

	// Token: 0x04000142 RID: 322
	[SerializeField]
	private bool drawBones;

	// Token: 0x04000143 RID: 323
	[SerializeField]
	private bool allowBonesOutsideOfModelPrefab;

	// Token: 0x04000144 RID: 324
	[SerializeField]
	private int ignoreCollisionDownSteps = 2;

	// Token: 0x04000145 RID: 325
	[SerializeField]
	private int ignoreCollisionUpSteps = 1;

	// Token: 0x04000146 RID: 326
	[SerializeField]
	private string hitBoxType = "HitBox";

	// Token: 0x04000147 RID: 327
	[SerializeField]
	private string hitBoxSystemType = "HitBoxSystem";

	// Token: 0x04000148 RID: 328
	[SerializeField]
	private string defaultBodyPartLayer = string.Empty;

	// Token: 0x04000149 RID: 329
	[SerializeField]
	private ActorRig actorRig;

	// Token: 0x0400014A RID: 330
	[SerializeField]
	private global::Character prototype;

	// Token: 0x0400014B RID: 331
	[SerializeField]
	private global::Ragdoll ragdollPrototype;

	// Token: 0x0400014C RID: 332
	[SerializeField]
	private Transform hitCapsuleTransform;

	// Token: 0x0400014D RID: 333
	[SerializeField]
	private Transform[] removeThese;

	// Token: 0x0400014E RID: 334
	[SerializeField]
	private float eyeHeight = 1f;

	// Token: 0x0400014F RID: 335
	[SerializeField]
	private GameObject generatedRigid;

	// Token: 0x04000150 RID: 336
	[SerializeField]
	private GameObject generatedHitBox;

	// Token: 0x04000151 RID: 337
	[SerializeField]
	private bool savedGenerated;

	// Token: 0x04000152 RID: 338
	[SerializeField]
	private Material[] materials;

	// Token: 0x04000153 RID: 339
	[SerializeField]
	private Vector3 editingAngles = Vector3.zero;

	// Token: 0x04000154 RID: 340
	[SerializeField]
	private bool editingCenterToRoot;

	// Token: 0x04000155 RID: 341
	[SerializeField]
	private BodyPartTransformMap bodyParts = new BodyPartTransformMap();

	// Token: 0x04000156 RID: 342
	[SerializeField]
	private string saveJSONGUID;

	// Token: 0x04000157 RID: 343
	[SerializeField]
	private string previewPrototypeGUID;

	// Token: 0x04000158 RID: 344
	[SerializeField]
	private bool removeAnimationFromRagdoll;

	// Token: 0x04000159 RID: 345
	[SerializeField]
	private string hitBoxLayerName = "Hitbox";

	// Token: 0x0400015A RID: 346
	[SerializeField]
	private bool useMeshesFromHitBoxOnRagdoll;

	// Token: 0x0400015B RID: 347
	private static bool once;

	// Token: 0x0400015C RID: 348
	private global::HitBoxSystem creatingSystem;

	// Token: 0x0400015D RID: 349
	private bool showAllBones;

	// Token: 0x0400015E RID: 350
	private static readonly MemberFilter actorRigSearch = new MemberFilter(global::AuthorHull.ActorRigSearch);

	// Token: 0x02000038 RID: 56
	private static class guis
	{
		// Token: 0x04000160 RID: 352
		public static readonly GUIContent overridingContent = new GUIContent("[overriding the hitbox output prefab]", "The HitBox prefab output will use the overriding mesh prefab provided, You must make sure that the heirarchy matches between the two");

		// Token: 0x04000161 RID: 353
		public static readonly GUIContent notOverridingContent = new GUIContent("[both outputs will use the same base]", "The HitBox prefab output will use the same mesh prefab as the one for the rigidbody");

		// Token: 0x04000162 RID: 354
		public static readonly GUIContent destroyDrop = new GUIContent("Destroy bone", "Drag a transform off the model instance that contains no ::'s");
	}

	// Token: 0x02000039 RID: 57
	private static class HitBoxItems
	{
		// Token: 0x06000220 RID: 544 RVA: 0x0000BE84 File Offset: 0x0000A084
		private static bool ValidateByID(global::AuthorCreation creation, global::AuthorPalletObject palletObject)
		{
			return true;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000BE88 File Offset: 0x0000A088
		private static global::AuthorPeice CreateByID<TPeice>(global::AuthorCreation creation, global::AuthorPalletObject palletObject) where TPeice : global::AuthorPeice
		{
			GameObject gameObject = new GameObject(palletObject.guiContent.text, new Type[]
			{
				typeof(TPeice)
			});
			TPeice component = gameObject.GetComponent<TPeice>();
			component.peiceID = palletObject.guiContent.text;
			return component;
		}

		// Token: 0x04000163 RID: 355
		public static readonly global::AuthorPalletObject.Validator validateByID = new global::AuthorPalletObject.Validator(global::AuthorHull.HitBoxItems.ValidateByID);

		// Token: 0x04000164 RID: 356
		public static readonly global::AuthorPalletObject.Creator createSocketByID = new global::AuthorPalletObject.Creator(global::AuthorHull.HitBoxItems.CreateByID<global::AuthorChHit>);

		// Token: 0x04000165 RID: 357
		public static readonly global::AuthorPalletObject[] pallet = new global::AuthorPalletObject[]
		{
			new global::AuthorPalletObject
			{
				guiContent = new GUIContent("Box"),
				validator = global::AuthorHull.HitBoxItems.validateByID,
				creator = global::AuthorHull.HitBoxItems.createSocketByID
			},
			new global::AuthorPalletObject
			{
				guiContent = new GUIContent("Sphere"),
				validator = global::AuthorHull.HitBoxItems.validateByID,
				creator = global::AuthorHull.HitBoxItems.createSocketByID
			},
			new global::AuthorPalletObject
			{
				guiContent = new GUIContent("Capsule"),
				validator = global::AuthorHull.HitBoxItems.validateByID,
				creator = global::AuthorHull.HitBoxItems.createSocketByID
			}
		};
	}
}
