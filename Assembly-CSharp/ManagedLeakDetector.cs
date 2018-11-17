using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x0200018A RID: 394
[ExecuteInEditMode]
public class ManagedLeakDetector : MonoBehaviour
{
	// Token: 0x06000BD8 RID: 3032 RVA: 0x0002EA80 File Offset: 0x0002CC80
	private static bool CheckRelation(Type a, Type b)
	{
		return a.IsAssignableFrom(b) || b.IsAssignableFrom(a);
	}

	// Token: 0x06000BD9 RID: 3033 RVA: 0x0002EA98 File Offset: 0x0002CC98
	public static string Poll()
	{
		return ManagedLeakDetector.Poll(typeof(Object));
	}

	// Token: 0x06000BDA RID: 3034 RVA: 0x0002EAAC File Offset: 0x0002CCAC
	public static string Poll(Type searchType)
	{
		return ManagedLeakDetector.Poll(searchType, typeof(Object));
	}

	// Token: 0x06000BDB RID: 3035 RVA: 0x0002EAC0 File Offset: 0x0002CCC0
	public static string Poll(Type searchType, Type minType)
	{
		return new ManagedLeakDetector.ReadResult(searchType, minType).ToString();
	}

	// Token: 0x06000BDC RID: 3036 RVA: 0x0002EAD0 File Offset: 0x0002CCD0
	private void OnGUI()
	{
		if (Event.current.type == 7)
		{
			if (!Camera.main)
			{
				GUI.Box(new Rect(-5f, -5f, (float)(Screen.width + 10), (float)(Screen.height + 10)), GUIContent.none);
			}
			ManagedLeakDetector.ReadResult readResult = new ManagedLeakDetector.ReadResult();
			readResult.Read();
			ManagedLeakDetector.Counter[] counters = readResult.counters;
			float num = (float)(Screen.width - 10);
			this.scroll = GUI.BeginScrollView(new Rect(5f, 5f, num, (float)(Screen.height - 10)), this.scroll, new Rect(0f, 0f, num, (float)(counters.Length * 20)));
			int num2 = 0;
			foreach (ManagedLeakDetector.Counter counter in counters)
			{
				GUI.Label(new Rect(0f, (float)num2, num, 20f), string.Format("{0:000} [{1:0000}] {2}", counter.actualInstanceCount, counter.derivedInstanceCount, counter.type));
				num2 += 20;
			}
		}
	}

	// Token: 0x0400073B RID: 1851
	private Vector2 scroll;

	// Token: 0x0200018B RID: 395
	private class Counter
	{
		// Token: 0x0400073C RID: 1852
		public int actualInstanceCount;

		// Token: 0x0400073D RID: 1853
		public int derivedInstanceCount;

		// Token: 0x0400073E RID: 1854
		public int enabledCount;

		// Token: 0x0400073F RID: 1855
		public Type type;
	}

	// Token: 0x0200018C RID: 396
	private class ReadResult
	{
		// Token: 0x06000BDE RID: 3038 RVA: 0x0002EBF8 File Offset: 0x0002CDF8
		public ReadResult(Type searchType, Type minType)
		{
			this.minType = (minType ?? typeof(Object));
			this.searchType = (searchType ?? typeof(Object));
			this.sumComponent.name = "Components";
			this.sumBehaviour.name = "Behaviours";
			this.sumRenderer.name = "Renderers";
			this.sumCollider.name = "Colliders";
			this.sumCloth.name = "Cloths";
			this.sumGameObject.name = "Game Objects";
			this.sumScriptableObject.name = "Scriptable Objects";
			this.sumMaterial.name = "Materials";
			this.sumTexture.name = "Textures";
			this.sumAnimation.name = "Animations";
			this.sumMesh.name = "Meshes";
			this.sumAudioClip.name = "Audio Clips";
			this.sumAnimationClip.name = "Animation Clips";
			this.sumParticleEmitter.name = "Particle Emitters (Legacy)";
			this.sumParticleSystem.name = "Particle Systems";
			this.sumComponent.check = ManagedLeakDetector.CheckRelation(searchType, typeof(Component));
			this.sumBehaviour.check = (this.sumComponent.check && ManagedLeakDetector.CheckRelation(typeof(Behaviour), searchType));
			this.sumRenderer.check = (this.sumComponent.check && ManagedLeakDetector.CheckRelation(typeof(Renderer), searchType));
			this.sumCollider.check = (this.sumComponent.check && ManagedLeakDetector.CheckRelation(typeof(Collider), searchType));
			this.sumCloth.check = (this.sumComponent.check && ManagedLeakDetector.CheckRelation(typeof(Cloth), searchType));
			this.sumParticleSystem.check = (this.sumComponent.check && ManagedLeakDetector.CheckRelation(typeof(ParticleSystem), searchType));
			this.sumAnimation.check = (this.sumBehaviour.check && ManagedLeakDetector.CheckRelation(typeof(Animation), searchType));
			this.sumParticleEmitter.check = (this.sumComponent.check && ManagedLeakDetector.CheckRelation(typeof(ParticleEmitter), searchType));
			this.sumGameObject.check = ManagedLeakDetector.CheckRelation(typeof(GameObject), searchType);
			this.sumScriptableObject.check = ManagedLeakDetector.CheckRelation(typeof(ScriptableObject), searchType);
			this.sumMaterial.check = ManagedLeakDetector.CheckRelation(typeof(Material), searchType);
			this.sumTexture.check = ManagedLeakDetector.CheckRelation(typeof(Texture), searchType);
			this.sumMesh.check = ManagedLeakDetector.CheckRelation(typeof(Mesh), searchType);
			this.sumAudioClip.check = ManagedLeakDetector.CheckRelation(typeof(AudioClip), searchType);
			this.sumAnimationClip.check = ManagedLeakDetector.CheckRelation(typeof(AnimationClip), searchType);
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x0002EF48 File Offset: 0x0002D148
		public ReadResult(Type searchType) : this(searchType, typeof(Object))
		{
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0002EF5C File Offset: 0x0002D15C
		public ReadResult() : this(typeof(Object))
		{
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x0002EF70 File Offset: 0x0002D170
		public void Read()
		{
			this.Read(false);
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x0002EF7C File Offset: 0x0002D17C
		public void Read(bool forceUpdate)
		{
			if (this.complete && !forceUpdate)
			{
				return;
			}
			Dictionary<Type, ManagedLeakDetector.Counter> dictionary = new Dictionary<Type, ManagedLeakDetector.Counter>();
			ManagedLeakDetector.Counter counter = new ManagedLeakDetector.Counter();
			counter.type = this.minType;
			dictionary.Add(this.minType, counter);
			this.sumComponent.Reset();
			this.sumBehaviour.Reset();
			this.sumRenderer.Reset();
			this.sumCollider.Reset();
			this.sumCloth.Reset();
			this.sumGameObject.Reset();
			this.sumScriptableObject.Reset();
			this.sumMaterial.Reset();
			this.sumTexture.Reset();
			this.sumAnimation.Reset();
			this.sumMesh.Reset();
			this.sumAudioClip.Reset();
			this.sumAnimationClip.Reset();
			this.sumParticleSystem.Reset();
			this.sumParticleEmitter.Reset();
			this.sumComponent.check = ManagedLeakDetector.CheckRelation(this.searchType, typeof(Component));
			this.sumBehaviour.check = (this.sumComponent.check && ManagedLeakDetector.CheckRelation(typeof(Behaviour), this.searchType));
			this.sumRenderer.check = (this.sumComponent.check && ManagedLeakDetector.CheckRelation(typeof(Renderer), this.searchType));
			this.sumCollider.check = (this.sumComponent.check && ManagedLeakDetector.CheckRelation(typeof(Collider), this.searchType));
			this.sumCloth.check = (this.sumComponent.check && ManagedLeakDetector.CheckRelation(typeof(Cloth), this.searchType));
			this.sumParticleSystem.check = (this.sumComponent.check && ManagedLeakDetector.CheckRelation(typeof(ParticleSystem), this.searchType));
			this.sumAnimation.check = (this.sumBehaviour.check && ManagedLeakDetector.CheckRelation(typeof(Animation), this.searchType));
			this.sumParticleEmitter.check = (this.sumComponent.check && ManagedLeakDetector.CheckRelation(typeof(ParticleEmitter), this.searchType));
			this.sumGameObject.check = ManagedLeakDetector.CheckRelation(typeof(GameObject), this.searchType);
			this.sumScriptableObject.check = ManagedLeakDetector.CheckRelation(typeof(ScriptableObject), this.searchType);
			this.sumMaterial.check = ManagedLeakDetector.CheckRelation(typeof(Material), this.searchType);
			this.sumTexture.check = ManagedLeakDetector.CheckRelation(typeof(Texture), this.searchType);
			this.sumMesh.check = ManagedLeakDetector.CheckRelation(typeof(Mesh), this.searchType);
			this.sumAudioClip.check = ManagedLeakDetector.CheckRelation(typeof(AudioClip), this.searchType);
			this.sumAnimationClip.check = ManagedLeakDetector.CheckRelation(typeof(AnimationClip), this.searchType);
			foreach (Object @object in Object.FindObjectsOfType(this.searchType))
			{
				Type type = @object.GetType();
				ManagedLeakDetector.Counter counter2;
				if (dictionary.TryGetValue(type, out counter2))
				{
					counter2.actualInstanceCount++;
				}
				else
				{
					dictionary.Add(type, counter2 = new ManagedLeakDetector.Counter
					{
						type = type,
						actualInstanceCount = 1
					});
				}
				if (this.sumComponent.check && typeof(Component).IsAssignableFrom(type))
				{
					this.sumComponent.total = this.sumComponent.total + 1;
					if (this.sumBehaviour.check && typeof(Behaviour).IsAssignableFrom(type))
					{
						if (((Behaviour)@object).enabled)
						{
							this.sumComponent.enabled = this.sumComponent.enabled + 1;
							counter2.enabledCount++;
							this.sumBehaviour.enabled = this.sumBehaviour.enabled + 1;
							this.sumBehaviour.total = this.sumBehaviour.total + 1;
							if (this.sumAnimation.check && typeof(Animation).IsAssignableFrom(type))
							{
								this.sumAnimation.enabled = this.sumAnimation.enabled + 1;
								this.sumAnimation.total = this.sumAnimation.total + 1;
							}
						}
						else if (this.sumAnimation.check && typeof(Animation).IsAssignableFrom(type))
						{
							this.sumAnimation.total = this.sumAnimation.total + 1;
						}
					}
					else if (this.sumRenderer.check && typeof(Renderer).IsAssignableFrom(type))
					{
						this.sumRenderer.total = this.sumRenderer.total + 1;
						if (((Renderer)@object).enabled)
						{
							this.sumComponent.enabled = this.sumComponent.enabled + 1;
							this.sumRenderer.enabled = this.sumRenderer.enabled + 1;
							counter2.enabledCount++;
						}
					}
					else if (this.sumCollider.check && typeof(Collider).IsAssignableFrom(type))
					{
						this.sumCollider.total = this.sumCollider.total + 1;
						if (((Collider)@object).enabled)
						{
							this.sumComponent.enabled = this.sumComponent.enabled + 1;
							this.sumCollider.enabled = this.sumCollider.enabled + 1;
							counter2.enabledCount++;
						}
					}
					else if (this.sumParticleSystem.check && typeof(ParticleSystem).IsAssignableFrom(type))
					{
						this.sumParticleSystem.total = this.sumParticleSystem.total + 1;
						if (((ParticleSystem)@object).IsAlive())
						{
							counter2.enabledCount++;
							this.sumParticleSystem.enabled = this.sumParticleSystem.enabled + 1;
							this.sumComponent.enabled = this.sumComponent.enabled + 1;
						}
					}
					else if (this.sumCloth.check && typeof(Cloth).IsAssignableFrom(type))
					{
						this.sumCloth.total = this.sumCloth.total + 1;
						if (((Cloth)@object).enabled)
						{
							counter2.enabledCount++;
							this.sumComponent.enabled = this.sumComponent.enabled + 1;
							this.sumCloth.enabled = this.sumCloth.enabled + 1;
						}
					}
					else if (this.sumParticleEmitter.check && typeof(ParticleEmitter).IsAssignableFrom(type))
					{
						this.sumParticleEmitter.total = this.sumParticleEmitter.total + 1;
						if (((ParticleEmitter)@object).enabled)
						{
							counter2.enabledCount++;
							this.sumParticleEmitter.enabled = this.sumParticleEmitter.enabled + 1;
							this.sumComponent.enabled = this.sumComponent.enabled + 1;
						}
					}
				}
				else if (this.sumGameObject.check && typeof(GameObject).IsAssignableFrom(type))
				{
					this.sumGameObject.total = this.sumGameObject.total + 1;
					if (((GameObject)@object).activeInHierarchy)
					{
						this.sumGameObject.enabled = this.sumGameObject.enabled + 1;
						counter2.enabledCount++;
					}
				}
				else if (this.sumMaterial.check && typeof(Material).IsAssignableFrom(type))
				{
					this.sumMaterial.total = this.sumMaterial.total + 1;
				}
				else if (this.sumTexture.check && typeof(Texture).IsAssignableFrom(type))
				{
					this.sumTexture.total = this.sumTexture.total + 1;
				}
				else if (this.sumAudioClip.check && typeof(AudioClip).IsAssignableFrom(type))
				{
					this.sumAudioClip.total = this.sumAudioClip.total + 1;
				}
				else if (this.sumAnimationClip.check && typeof(AnimationClip).IsAssignableFrom(type))
				{
					this.sumAnimationClip.total = this.sumAnimationClip.total + 1;
				}
				else if (this.sumMesh.check && typeof(Mesh).IsAssignableFrom(type))
				{
					this.sumMesh.total = this.sumMesh.total + 1;
				}
				else if (this.sumScriptableObject.check && typeof(ScriptableObject).IsAssignableFrom(type))
				{
					this.sumScriptableObject.total = this.sumScriptableObject.total + 1;
				}
				if (type != this.minType)
				{
					for (type = type.BaseType; type != typeof(Object); type = type.BaseType)
					{
						if (dictionary.TryGetValue(type, out counter2))
						{
							counter2.derivedInstanceCount++;
						}
						else
						{
							dictionary.Add(type, new ManagedLeakDetector.Counter
							{
								type = type,
								derivedInstanceCount = 1
							});
						}
					}
					counter.derivedInstanceCount++;
				}
			}
			List<ManagedLeakDetector.Counter> list = new List<ManagedLeakDetector.Counter>(dictionary.Values);
			list.Sort(delegate(ManagedLeakDetector.Counter firstPair, ManagedLeakDetector.Counter nextPair)
			{
				int num = nextPair.actualInstanceCount.CompareTo(firstPair.actualInstanceCount);
				if (num == 0)
				{
					return nextPair.derivedInstanceCount.CompareTo(firstPair.derivedInstanceCount);
				}
				return num;
			});
			this.counters = list.ToArray();
			this.complete = true;
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x0002F9C4 File Offset: 0x0002DBC4
		private static void Print(StringBuilder sb, ref ManagedLeakDetector.SumEnable en)
		{
			if (en.check)
			{
				if (en.enabled != 0)
				{
					sb.AppendFormat("{0} {1} ({2})\r\n", en.name, en.total, en.enabled);
				}
				else if (en.total != 0)
				{
					sb.AppendFormat("{0} {1}\r\n", en.name, en.total);
				}
			}
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x0002FA3C File Offset: 0x0002DC3C
		public override string ToString()
		{
			this.Read();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("Instances, Deriving Instances, Type, (# Enabled [if not shown 0] )");
			foreach (ManagedLeakDetector.Counter counter in this.counters)
			{
				if (counter.enabledCount != 0)
				{
					stringBuilder.AppendFormat("{0,8} [{1,8}] {2} ({3} enabled)\r\n", new object[]
					{
						counter.actualInstanceCount,
						counter.derivedInstanceCount,
						counter.type,
						counter.enabledCount
					});
				}
				else
				{
					stringBuilder.AppendFormat("{0,8} [{1,8}] {2}\r\n", counter.actualInstanceCount, counter.derivedInstanceCount, counter.type);
				}
			}
			stringBuilder.AppendLine("basic counters: if not there, there is none.");
			ManagedLeakDetector.ReadResult.Print(stringBuilder, ref this.sumComponent);
			ManagedLeakDetector.ReadResult.Print(stringBuilder, ref this.sumBehaviour);
			ManagedLeakDetector.ReadResult.Print(stringBuilder, ref this.sumRenderer);
			ManagedLeakDetector.ReadResult.Print(stringBuilder, ref this.sumCollider);
			ManagedLeakDetector.ReadResult.Print(stringBuilder, ref this.sumCloth);
			ManagedLeakDetector.ReadResult.Print(stringBuilder, ref this.sumGameObject);
			ManagedLeakDetector.ReadResult.Print(stringBuilder, ref this.sumScriptableObject);
			ManagedLeakDetector.ReadResult.Print(stringBuilder, ref this.sumMaterial);
			ManagedLeakDetector.ReadResult.Print(stringBuilder, ref this.sumTexture);
			ManagedLeakDetector.ReadResult.Print(stringBuilder, ref this.sumAnimation);
			ManagedLeakDetector.ReadResult.Print(stringBuilder, ref this.sumMesh);
			ManagedLeakDetector.ReadResult.Print(stringBuilder, ref this.sumAudioClip);
			ManagedLeakDetector.ReadResult.Print(stringBuilder, ref this.sumAnimationClip);
			ManagedLeakDetector.ReadResult.Print(stringBuilder, ref this.sumParticleSystem);
			ManagedLeakDetector.ReadResult.Print(stringBuilder, ref this.sumParticleEmitter);
			stringBuilder.AppendFormat("Count done for search {0} (min:{1})", this.searchType, this.minType);
			return stringBuilder.ToString();
		}

		// Token: 0x04000740 RID: 1856
		public ManagedLeakDetector.SumEnable sumComponent;

		// Token: 0x04000741 RID: 1857
		public ManagedLeakDetector.SumEnable sumBehaviour;

		// Token: 0x04000742 RID: 1858
		public ManagedLeakDetector.SumEnable sumRenderer;

		// Token: 0x04000743 RID: 1859
		public ManagedLeakDetector.SumEnable sumCollider;

		// Token: 0x04000744 RID: 1860
		public ManagedLeakDetector.SumEnable sumCloth;

		// Token: 0x04000745 RID: 1861
		public ManagedLeakDetector.SumEnable sumGameObject;

		// Token: 0x04000746 RID: 1862
		public ManagedLeakDetector.SumEnable sumScriptableObject;

		// Token: 0x04000747 RID: 1863
		public ManagedLeakDetector.SumEnable sumMaterial;

		// Token: 0x04000748 RID: 1864
		public ManagedLeakDetector.SumEnable sumTexture;

		// Token: 0x04000749 RID: 1865
		public ManagedLeakDetector.SumEnable sumAnimation;

		// Token: 0x0400074A RID: 1866
		public ManagedLeakDetector.SumEnable sumMesh;

		// Token: 0x0400074B RID: 1867
		public ManagedLeakDetector.SumEnable sumAudioClip;

		// Token: 0x0400074C RID: 1868
		public ManagedLeakDetector.SumEnable sumAnimationClip;

		// Token: 0x0400074D RID: 1869
		public ManagedLeakDetector.SumEnable sumParticleEmitter;

		// Token: 0x0400074E RID: 1870
		public ManagedLeakDetector.SumEnable sumParticleSystem;

		// Token: 0x0400074F RID: 1871
		public bool complete;

		// Token: 0x04000750 RID: 1872
		public ManagedLeakDetector.Counter[] counters;

		// Token: 0x04000751 RID: 1873
		public readonly Type searchType;

		// Token: 0x04000752 RID: 1874
		public readonly Type minType;
	}

	// Token: 0x0200018D RID: 397
	private struct SumEnable
	{
		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x0002FC14 File Offset: 0x0002DE14
		public int disabled
		{
			get
			{
				return this.total - this.enabled;
			}
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x0002FC24 File Offset: 0x0002DE24
		public void Reset()
		{
			this.total = 0;
			this.enabled = 0;
		}

		// Token: 0x04000754 RID: 1876
		public bool check;

		// Token: 0x04000755 RID: 1877
		public int total;

		// Token: 0x04000756 RID: 1878
		public int enabled;

		// Token: 0x04000757 RID: 1879
		public string name;

		// Token: 0x04000758 RID: 1880
		public Type type;
	}
}
