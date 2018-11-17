using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x020001B6 RID: 438
[ExecuteInEditMode]
public class ManagedLeakDetector : MonoBehaviour
{
	// Token: 0x06000D08 RID: 3336 RVA: 0x0003296C File Offset: 0x00030B6C
	private static bool CheckRelation(Type a, Type b)
	{
		return a.IsAssignableFrom(b) || b.IsAssignableFrom(a);
	}

	// Token: 0x06000D09 RID: 3337 RVA: 0x00032984 File Offset: 0x00030B84
	public static string Poll()
	{
		return global::ManagedLeakDetector.Poll(typeof(Object));
	}

	// Token: 0x06000D0A RID: 3338 RVA: 0x00032998 File Offset: 0x00030B98
	public static string Poll(Type searchType)
	{
		return global::ManagedLeakDetector.Poll(searchType, typeof(Object));
	}

	// Token: 0x06000D0B RID: 3339 RVA: 0x000329AC File Offset: 0x00030BAC
	public static string Poll(Type searchType, Type minType)
	{
		return new global::ManagedLeakDetector.ReadResult(searchType, minType).ToString();
	}

	// Token: 0x06000D0C RID: 3340 RVA: 0x000329BC File Offset: 0x00030BBC
	private void OnGUI()
	{
		if (Event.current.type == 7)
		{
			if (!Camera.main)
			{
				GUI.Box(new Rect(-5f, -5f, (float)(Screen.width + 10), (float)(Screen.height + 10)), GUIContent.none);
			}
			global::ManagedLeakDetector.ReadResult readResult = new global::ManagedLeakDetector.ReadResult();
			readResult.Read();
			global::ManagedLeakDetector.Counter[] counters = readResult.counters;
			float num = (float)(Screen.width - 10);
			this.scroll = GUI.BeginScrollView(new Rect(5f, 5f, num, (float)(Screen.height - 10)), this.scroll, new Rect(0f, 0f, num, (float)(counters.Length * 20)));
			int num2 = 0;
			foreach (global::ManagedLeakDetector.Counter counter in counters)
			{
				GUI.Label(new Rect(0f, (float)num2, num, 20f), string.Format("{0:000} [{1:0000}] {2}", counter.actualInstanceCount, counter.derivedInstanceCount, counter.type));
				num2 += 20;
			}
		}
	}

	// Token: 0x0400084F RID: 2127
	private Vector2 scroll;

	// Token: 0x020001B7 RID: 439
	private class Counter
	{
		// Token: 0x04000850 RID: 2128
		public int actualInstanceCount;

		// Token: 0x04000851 RID: 2129
		public int derivedInstanceCount;

		// Token: 0x04000852 RID: 2130
		public int enabledCount;

		// Token: 0x04000853 RID: 2131
		public Type type;
	}

	// Token: 0x020001B8 RID: 440
	private class ReadResult
	{
		// Token: 0x06000D0E RID: 3342 RVA: 0x00032AE4 File Offset: 0x00030CE4
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
			this.sumComponent.check = global::ManagedLeakDetector.CheckRelation(searchType, typeof(Component));
			this.sumBehaviour.check = (this.sumComponent.check && global::ManagedLeakDetector.CheckRelation(typeof(Behaviour), searchType));
			this.sumRenderer.check = (this.sumComponent.check && global::ManagedLeakDetector.CheckRelation(typeof(Renderer), searchType));
			this.sumCollider.check = (this.sumComponent.check && global::ManagedLeakDetector.CheckRelation(typeof(Collider), searchType));
			this.sumCloth.check = (this.sumComponent.check && global::ManagedLeakDetector.CheckRelation(typeof(Cloth), searchType));
			this.sumParticleSystem.check = (this.sumComponent.check && global::ManagedLeakDetector.CheckRelation(typeof(ParticleSystem), searchType));
			this.sumAnimation.check = (this.sumBehaviour.check && global::ManagedLeakDetector.CheckRelation(typeof(Animation), searchType));
			this.sumParticleEmitter.check = (this.sumComponent.check && global::ManagedLeakDetector.CheckRelation(typeof(ParticleEmitter), searchType));
			this.sumGameObject.check = global::ManagedLeakDetector.CheckRelation(typeof(GameObject), searchType);
			this.sumScriptableObject.check = global::ManagedLeakDetector.CheckRelation(typeof(ScriptableObject), searchType);
			this.sumMaterial.check = global::ManagedLeakDetector.CheckRelation(typeof(Material), searchType);
			this.sumTexture.check = global::ManagedLeakDetector.CheckRelation(typeof(Texture), searchType);
			this.sumMesh.check = global::ManagedLeakDetector.CheckRelation(typeof(Mesh), searchType);
			this.sumAudioClip.check = global::ManagedLeakDetector.CheckRelation(typeof(AudioClip), searchType);
			this.sumAnimationClip.check = global::ManagedLeakDetector.CheckRelation(typeof(AnimationClip), searchType);
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x00032E34 File Offset: 0x00031034
		public ReadResult(Type searchType) : this(searchType, typeof(Object))
		{
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x00032E48 File Offset: 0x00031048
		public ReadResult() : this(typeof(Object))
		{
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x00032E5C File Offset: 0x0003105C
		public void Read()
		{
			this.Read(false);
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x00032E68 File Offset: 0x00031068
		public void Read(bool forceUpdate)
		{
			if (this.complete && !forceUpdate)
			{
				return;
			}
			Dictionary<Type, global::ManagedLeakDetector.Counter> dictionary = new Dictionary<Type, global::ManagedLeakDetector.Counter>();
			global::ManagedLeakDetector.Counter counter = new global::ManagedLeakDetector.Counter();
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
			this.sumComponent.check = global::ManagedLeakDetector.CheckRelation(this.searchType, typeof(Component));
			this.sumBehaviour.check = (this.sumComponent.check && global::ManagedLeakDetector.CheckRelation(typeof(Behaviour), this.searchType));
			this.sumRenderer.check = (this.sumComponent.check && global::ManagedLeakDetector.CheckRelation(typeof(Renderer), this.searchType));
			this.sumCollider.check = (this.sumComponent.check && global::ManagedLeakDetector.CheckRelation(typeof(Collider), this.searchType));
			this.sumCloth.check = (this.sumComponent.check && global::ManagedLeakDetector.CheckRelation(typeof(Cloth), this.searchType));
			this.sumParticleSystem.check = (this.sumComponent.check && global::ManagedLeakDetector.CheckRelation(typeof(ParticleSystem), this.searchType));
			this.sumAnimation.check = (this.sumBehaviour.check && global::ManagedLeakDetector.CheckRelation(typeof(Animation), this.searchType));
			this.sumParticleEmitter.check = (this.sumComponent.check && global::ManagedLeakDetector.CheckRelation(typeof(ParticleEmitter), this.searchType));
			this.sumGameObject.check = global::ManagedLeakDetector.CheckRelation(typeof(GameObject), this.searchType);
			this.sumScriptableObject.check = global::ManagedLeakDetector.CheckRelation(typeof(ScriptableObject), this.searchType);
			this.sumMaterial.check = global::ManagedLeakDetector.CheckRelation(typeof(Material), this.searchType);
			this.sumTexture.check = global::ManagedLeakDetector.CheckRelation(typeof(Texture), this.searchType);
			this.sumMesh.check = global::ManagedLeakDetector.CheckRelation(typeof(Mesh), this.searchType);
			this.sumAudioClip.check = global::ManagedLeakDetector.CheckRelation(typeof(AudioClip), this.searchType);
			this.sumAnimationClip.check = global::ManagedLeakDetector.CheckRelation(typeof(AnimationClip), this.searchType);
			foreach (Object @object in Object.FindObjectsOfType(this.searchType))
			{
				Type type = @object.GetType();
				global::ManagedLeakDetector.Counter counter2;
				if (dictionary.TryGetValue(type, out counter2))
				{
					counter2.actualInstanceCount++;
				}
				else
				{
					dictionary.Add(type, counter2 = new global::ManagedLeakDetector.Counter
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
							dictionary.Add(type, new global::ManagedLeakDetector.Counter
							{
								type = type,
								derivedInstanceCount = 1
							});
						}
					}
					counter.derivedInstanceCount++;
				}
			}
			List<global::ManagedLeakDetector.Counter> list = new List<global::ManagedLeakDetector.Counter>(dictionary.Values);
			list.Sort(delegate(global::ManagedLeakDetector.Counter firstPair, global::ManagedLeakDetector.Counter nextPair)
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

		// Token: 0x06000D13 RID: 3347 RVA: 0x000338B0 File Offset: 0x00031AB0
		private static void Print(StringBuilder sb, ref global::ManagedLeakDetector.SumEnable en)
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

		// Token: 0x06000D14 RID: 3348 RVA: 0x00033928 File Offset: 0x00031B28
		public override string ToString()
		{
			this.Read();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("Instances, Deriving Instances, Type, (# Enabled [if not shown 0] )");
			foreach (global::ManagedLeakDetector.Counter counter in this.counters)
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
			global::ManagedLeakDetector.ReadResult.Print(stringBuilder, ref this.sumComponent);
			global::ManagedLeakDetector.ReadResult.Print(stringBuilder, ref this.sumBehaviour);
			global::ManagedLeakDetector.ReadResult.Print(stringBuilder, ref this.sumRenderer);
			global::ManagedLeakDetector.ReadResult.Print(stringBuilder, ref this.sumCollider);
			global::ManagedLeakDetector.ReadResult.Print(stringBuilder, ref this.sumCloth);
			global::ManagedLeakDetector.ReadResult.Print(stringBuilder, ref this.sumGameObject);
			global::ManagedLeakDetector.ReadResult.Print(stringBuilder, ref this.sumScriptableObject);
			global::ManagedLeakDetector.ReadResult.Print(stringBuilder, ref this.sumMaterial);
			global::ManagedLeakDetector.ReadResult.Print(stringBuilder, ref this.sumTexture);
			global::ManagedLeakDetector.ReadResult.Print(stringBuilder, ref this.sumAnimation);
			global::ManagedLeakDetector.ReadResult.Print(stringBuilder, ref this.sumMesh);
			global::ManagedLeakDetector.ReadResult.Print(stringBuilder, ref this.sumAudioClip);
			global::ManagedLeakDetector.ReadResult.Print(stringBuilder, ref this.sumAnimationClip);
			global::ManagedLeakDetector.ReadResult.Print(stringBuilder, ref this.sumParticleSystem);
			global::ManagedLeakDetector.ReadResult.Print(stringBuilder, ref this.sumParticleEmitter);
			stringBuilder.AppendFormat("Count done for search {0} (min:{1})", this.searchType, this.minType);
			return stringBuilder.ToString();
		}

		// Token: 0x04000854 RID: 2132
		public global::ManagedLeakDetector.SumEnable sumComponent;

		// Token: 0x04000855 RID: 2133
		public global::ManagedLeakDetector.SumEnable sumBehaviour;

		// Token: 0x04000856 RID: 2134
		public global::ManagedLeakDetector.SumEnable sumRenderer;

		// Token: 0x04000857 RID: 2135
		public global::ManagedLeakDetector.SumEnable sumCollider;

		// Token: 0x04000858 RID: 2136
		public global::ManagedLeakDetector.SumEnable sumCloth;

		// Token: 0x04000859 RID: 2137
		public global::ManagedLeakDetector.SumEnable sumGameObject;

		// Token: 0x0400085A RID: 2138
		public global::ManagedLeakDetector.SumEnable sumScriptableObject;

		// Token: 0x0400085B RID: 2139
		public global::ManagedLeakDetector.SumEnable sumMaterial;

		// Token: 0x0400085C RID: 2140
		public global::ManagedLeakDetector.SumEnable sumTexture;

		// Token: 0x0400085D RID: 2141
		public global::ManagedLeakDetector.SumEnable sumAnimation;

		// Token: 0x0400085E RID: 2142
		public global::ManagedLeakDetector.SumEnable sumMesh;

		// Token: 0x0400085F RID: 2143
		public global::ManagedLeakDetector.SumEnable sumAudioClip;

		// Token: 0x04000860 RID: 2144
		public global::ManagedLeakDetector.SumEnable sumAnimationClip;

		// Token: 0x04000861 RID: 2145
		public global::ManagedLeakDetector.SumEnable sumParticleEmitter;

		// Token: 0x04000862 RID: 2146
		public global::ManagedLeakDetector.SumEnable sumParticleSystem;

		// Token: 0x04000863 RID: 2147
		public bool complete;

		// Token: 0x04000864 RID: 2148
		public global::ManagedLeakDetector.Counter[] counters;

		// Token: 0x04000865 RID: 2149
		public readonly Type searchType;

		// Token: 0x04000866 RID: 2150
		public readonly Type minType;
	}

	// Token: 0x020001B9 RID: 441
	private struct SumEnable
	{
		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000D16 RID: 3350 RVA: 0x00033B00 File Offset: 0x00031D00
		public int disabled
		{
			get
			{
				return this.total - this.enabled;
			}
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x00033B10 File Offset: 0x00031D10
		public void Reset()
		{
			this.total = 0;
			this.enabled = 0;
		}

		// Token: 0x04000868 RID: 2152
		public bool check;

		// Token: 0x04000869 RID: 2153
		public int total;

		// Token: 0x0400086A RID: 2154
		public int enabled;

		// Token: 0x0400086B RID: 2155
		public string name;

		// Token: 0x0400086C RID: 2156
		public Type type;
	}
}
