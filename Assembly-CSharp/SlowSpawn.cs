using System;
using System.Collections;
using System.Collections.Generic;
using Facepunch.Progress;
using UnityEngine;

// Token: 0x020005A9 RID: 1449
public class SlowSpawn : global::ThrottledTask, Facepunch.Progress.IProgress
{
	// Token: 0x170009EF RID: 2543
	public global::SlowSpawn.InstanceParameters this[int i]
	{
		get
		{
			return new global::SlowSpawn.InstanceParameters(this, i);
		}
	}

	// Token: 0x170009F0 RID: 2544
	// (get) Token: 0x06002EA8 RID: 11944 RVA: 0x000B3A30 File Offset: 0x000B1C30
	public int Count
	{
		get
		{
			return (this.ps != null) ? this.ps.Length : 0;
		}
	}

	// Token: 0x170009F1 RID: 2545
	// (get) Token: 0x06002EA9 RID: 11945 RVA: 0x000B3A4C File Offset: 0x000B1C4C
	public int CountSpawned
	{
		get
		{
			return (!base.Working) ? ((this.iter != -1) ? this.Count : 0) : this.iter;
		}
	}

	// Token: 0x170009F2 RID: 2546
	// (get) Token: 0x06002EAA RID: 11946 RVA: 0x000B3A88 File Offset: 0x000B1C88
	public float progress
	{
		get
		{
			return (!base.Working) ? ((this.iter != -1 || !base.enabled) ? 1f : 0f) : ((float)((double)this.iter / (double)this.Count));
		}
	}

	// Token: 0x06002EAB RID: 11947 RVA: 0x000B3ADC File Offset: 0x000B1CDC
	private IEnumerator Start()
	{
		if (!base.Working && ++this.iter < (this.iter_end = this.Count))
		{
			base.Working = true;
			for (;;)
			{
				global::ThrottledTask.Timer timer = base.Begin;
				do
				{
					try
					{
						this[this.iter].Spawn(this.runtimeLoad, 9);
					}
					catch (Exception ex)
					{
						Exception e = ex;
						Debug.LogException(e, this);
					}
					if (++this.iter >= this.iter_end)
					{
						goto IL_FC;
					}
				}
				while (timer.Continue);
				yield return null;
			}
			IL_FC:
			base.Working = false;
			yield break;
		}
		yield break;
	}

	// Token: 0x06002EAC RID: 11948 RVA: 0x000B3AF8 File Offset: 0x000B1CF8
	public IEnumerable<GameObject> SpawnAll(global::SlowSpawn.SpawnFlags SpawnFlags = global::SlowSpawn.SpawnFlags.All, HideFlags HideFlags = 9)
	{
		int i = 0;
		while (i < this.Count)
		{
			GameObject newSpawn;
			try
			{
				newSpawn = this[i].Spawn(SpawnFlags, HideFlags);
			}
			catch (Exception ex)
			{
				Exception e = ex;
				Debug.LogException(e);
				goto IL_92;
			}
			goto IL_7A;
			IL_92:
			i++;
			continue;
			IL_7A:
			yield return newSpawn;
			goto IL_92;
		}
		yield break;
	}

	// Token: 0x0400193E RID: 6462
	[SerializeField]
	private string findSequence = "_decor_";

	// Token: 0x0400193F RID: 6463
	[SerializeField]
	private Mesh[] meshes;

	// Token: 0x04001940 RID: 6464
	[SerializeField]
	private Material sharedMaterial;

	// Token: 0x04001941 RID: 6465
	[SerializeField]
	private global::SlowSpawn.SpawnFlags runtimeLoad = global::SlowSpawn.SpawnFlags.Collider;

	// Token: 0x04001942 RID: 6466
	[HideInInspector]
	[SerializeField]
	private int[] meshIndex;

	// Token: 0x04001943 RID: 6467
	[HideInInspector]
	[SerializeField]
	private Vector4[] ps;

	// Token: 0x04001944 RID: 6468
	[SerializeField]
	[HideInInspector]
	private Quaternion[] r;

	// Token: 0x04001945 RID: 6469
	[NonSerialized]
	private int iter = -1;

	// Token: 0x04001946 RID: 6470
	[NonSerialized]
	private int iter_end;

	// Token: 0x020005AA RID: 1450
	[Flags]
	public enum SpawnFlags
	{
		// Token: 0x04001948 RID: 6472
		Collider = 1,
		// Token: 0x04001949 RID: 6473
		Renderer = 2,
		// Token: 0x0400194A RID: 6474
		MeshFilter = 4,
		// Token: 0x0400194B RID: 6475
		All = 7,
		// Token: 0x0400194C RID: 6476
		Graphics = 6
	}

	// Token: 0x020005AB RID: 1451
	public struct InstanceParameters
	{
		// Token: 0x06002EAD RID: 11949 RVA: 0x000B3B38 File Offset: 0x000B1D38
		public InstanceParameters(global::SlowSpawn SlowSpawn, int Index)
		{
			this.Index = Index;
			this.Layer = SlowSpawn.gameObject.layer;
			Vector4 vector = SlowSpawn.ps[Index];
			this.Position.x = vector.x;
			this.Position.y = vector.y;
			this.Position.z = vector.z;
			this.Scale.x = (this.Scale.y = (this.Scale.z = vector.w));
			this.Rotation = SlowSpawn.r[Index];
			this.Mesh = SlowSpawn.meshes[SlowSpawn.meshIndex[Index]];
			this.SharedMaterial = SlowSpawn.sharedMaterial;
		}

		// Token: 0x06002EAE RID: 11950 RVA: 0x000B3C0C File Offset: 0x000B1E0C
		public MeshCollider AddCollider(GameObject go)
		{
			MeshCollider meshCollider = go.AddComponent<MeshCollider>();
			meshCollider.sharedMesh = this.Mesh;
			return meshCollider;
		}

		// Token: 0x06002EAF RID: 11951 RVA: 0x000B3C30 File Offset: 0x000B1E30
		public MeshRenderer AddRenderer(GameObject go)
		{
			MeshRenderer meshRenderer = go.AddComponent<MeshRenderer>();
			meshRenderer.sharedMaterial = this.SharedMaterial;
			return meshRenderer;
		}

		// Token: 0x06002EB0 RID: 11952 RVA: 0x000B3C54 File Offset: 0x000B1E54
		public MeshFilter AddMeshFilter(GameObject go)
		{
			MeshFilter meshFilter = go.AddComponent<MeshFilter>();
			meshFilter.sharedMesh = this.Mesh;
			return meshFilter;
		}

		// Token: 0x06002EB1 RID: 11953 RVA: 0x000B3C78 File Offset: 0x000B1E78
		private global::SlowSpawn.SpawnFlags AddTo(GameObject go, global::SlowSpawn.SpawnFlags spawnFlags, bool safe)
		{
			global::SlowSpawn.SpawnFlags spawnFlags2 = (global::SlowSpawn.SpawnFlags)0;
			if ((spawnFlags & global::SlowSpawn.SpawnFlags.MeshFilter) == global::SlowSpawn.SpawnFlags.MeshFilter && (safe || !go.GetComponent<MeshFilter>()))
			{
				spawnFlags2 |= global::SlowSpawn.SpawnFlags.MeshFilter;
				this.AddMeshFilter(go);
			}
			if ((spawnFlags & global::SlowSpawn.SpawnFlags.Renderer) == global::SlowSpawn.SpawnFlags.Renderer && (safe || !go.renderer))
			{
				spawnFlags2 |= global::SlowSpawn.SpawnFlags.Renderer;
				this.AddRenderer(go);
			}
			if ((spawnFlags & global::SlowSpawn.SpawnFlags.Collider) == global::SlowSpawn.SpawnFlags.Collider && (safe || !go.collider))
			{
				spawnFlags2 |= global::SlowSpawn.SpawnFlags.Collider;
				this.AddCollider(go);
			}
			return spawnFlags2;
		}

		// Token: 0x06002EB2 RID: 11954 RVA: 0x000B3D0C File Offset: 0x000B1F0C
		public global::SlowSpawn.SpawnFlags AddTo(GameObject go, global::SlowSpawn.SpawnFlags spawnFlags = global::SlowSpawn.SpawnFlags.All)
		{
			return this.AddTo(go, spawnFlags, false);
		}

		// Token: 0x06002EB3 RID: 11955 RVA: 0x000B3D18 File Offset: 0x000B1F18
		public GameObject Spawn(global::SlowSpawn.SpawnFlags spawnFlags = global::SlowSpawn.SpawnFlags.All, HideFlags HideFlags = 9)
		{
			GameObject gameObject = new GameObject(string.Empty)
			{
				hideFlags = HideFlags,
				layer = this.Layer,
				transform = 
				{
					position = this.Position,
					rotation = this.Rotation
				}
			};
			this.AddTo(gameObject, spawnFlags, true);
			return gameObject;
		}

		// Token: 0x0400194D RID: 6477
		public const global::SlowSpawn.SpawnFlags DefaultSpawnFlags = global::SlowSpawn.SpawnFlags.All;

		// Token: 0x0400194E RID: 6478
		public const HideFlags DefaultHideFlags = 9;

		// Token: 0x0400194F RID: 6479
		public readonly Vector3 Position;

		// Token: 0x04001950 RID: 6480
		public readonly Vector3 Scale;

		// Token: 0x04001951 RID: 6481
		public readonly Quaternion Rotation;

		// Token: 0x04001952 RID: 6482
		public readonly Mesh Mesh;

		// Token: 0x04001953 RID: 6483
		public readonly Material SharedMaterial;

		// Token: 0x04001954 RID: 6484
		public readonly int Layer;

		// Token: 0x04001955 RID: 6485
		public readonly int Index;
	}
}
