using System;
using System.Collections;
using System.Collections.Generic;
using Facepunch.Progress;
using UnityEngine;

// Token: 0x020004EE RID: 1262
public class SlowSpawn : ThrottledTask, IProgress
{
	// Token: 0x1700097F RID: 2431
	public SlowSpawn.InstanceParameters this[int i]
	{
		get
		{
			return new SlowSpawn.InstanceParameters(this, i);
		}
	}

	// Token: 0x17000980 RID: 2432
	// (get) Token: 0x06002AF6 RID: 10998 RVA: 0x000ABC98 File Offset: 0x000A9E98
	public int Count
	{
		get
		{
			return (this.ps != null) ? this.ps.Length : 0;
		}
	}

	// Token: 0x17000981 RID: 2433
	// (get) Token: 0x06002AF7 RID: 10999 RVA: 0x000ABCB4 File Offset: 0x000A9EB4
	public int CountSpawned
	{
		get
		{
			return (!base.Working) ? ((this.iter != -1) ? this.Count : 0) : this.iter;
		}
	}

	// Token: 0x17000982 RID: 2434
	// (get) Token: 0x06002AF8 RID: 11000 RVA: 0x000ABCF0 File Offset: 0x000A9EF0
	public float progress
	{
		get
		{
			return (!base.Working) ? ((this.iter != -1 || !base.enabled) ? 1f : 0f) : ((float)((double)this.iter / (double)this.Count));
		}
	}

	// Token: 0x06002AF9 RID: 11001 RVA: 0x000ABD44 File Offset: 0x000A9F44
	private IEnumerator Start()
	{
		if (!base.Working && ++this.iter < (this.iter_end = this.Count))
		{
			base.Working = true;
			for (;;)
			{
				ThrottledTask.Timer timer = base.Begin;
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

	// Token: 0x06002AFA RID: 11002 RVA: 0x000ABD60 File Offset: 0x000A9F60
	public IEnumerable<GameObject> SpawnAll(SlowSpawn.SpawnFlags SpawnFlags = SlowSpawn.SpawnFlags.All, HideFlags HideFlags = 9)
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

	// Token: 0x04001781 RID: 6017
	[SerializeField]
	private string findSequence = "_decor_";

	// Token: 0x04001782 RID: 6018
	[SerializeField]
	private Mesh[] meshes;

	// Token: 0x04001783 RID: 6019
	[SerializeField]
	private Material sharedMaterial;

	// Token: 0x04001784 RID: 6020
	[SerializeField]
	private SlowSpawn.SpawnFlags runtimeLoad = SlowSpawn.SpawnFlags.Collider;

	// Token: 0x04001785 RID: 6021
	[HideInInspector]
	[SerializeField]
	private int[] meshIndex;

	// Token: 0x04001786 RID: 6022
	[HideInInspector]
	[SerializeField]
	private Vector4[] ps;

	// Token: 0x04001787 RID: 6023
	[SerializeField]
	[HideInInspector]
	private Quaternion[] r;

	// Token: 0x04001788 RID: 6024
	[NonSerialized]
	private int iter = -1;

	// Token: 0x04001789 RID: 6025
	[NonSerialized]
	private int iter_end;

	// Token: 0x020004EF RID: 1263
	[Flags]
	public enum SpawnFlags
	{
		// Token: 0x0400178B RID: 6027
		Collider = 1,
		// Token: 0x0400178C RID: 6028
		Renderer = 2,
		// Token: 0x0400178D RID: 6029
		MeshFilter = 4,
		// Token: 0x0400178E RID: 6030
		All = 7,
		// Token: 0x0400178F RID: 6031
		Graphics = 6
	}

	// Token: 0x020004F0 RID: 1264
	public struct InstanceParameters
	{
		// Token: 0x06002AFB RID: 11003 RVA: 0x000ABDA0 File Offset: 0x000A9FA0
		public InstanceParameters(SlowSpawn SlowSpawn, int Index)
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

		// Token: 0x06002AFC RID: 11004 RVA: 0x000ABE74 File Offset: 0x000AA074
		public MeshCollider AddCollider(GameObject go)
		{
			MeshCollider meshCollider = go.AddComponent<MeshCollider>();
			meshCollider.sharedMesh = this.Mesh;
			return meshCollider;
		}

		// Token: 0x06002AFD RID: 11005 RVA: 0x000ABE98 File Offset: 0x000AA098
		public MeshRenderer AddRenderer(GameObject go)
		{
			MeshRenderer meshRenderer = go.AddComponent<MeshRenderer>();
			meshRenderer.sharedMaterial = this.SharedMaterial;
			return meshRenderer;
		}

		// Token: 0x06002AFE RID: 11006 RVA: 0x000ABEBC File Offset: 0x000AA0BC
		public MeshFilter AddMeshFilter(GameObject go)
		{
			MeshFilter meshFilter = go.AddComponent<MeshFilter>();
			meshFilter.sharedMesh = this.Mesh;
			return meshFilter;
		}

		// Token: 0x06002AFF RID: 11007 RVA: 0x000ABEE0 File Offset: 0x000AA0E0
		private SlowSpawn.SpawnFlags AddTo(GameObject go, SlowSpawn.SpawnFlags spawnFlags, bool safe)
		{
			SlowSpawn.SpawnFlags spawnFlags2 = (SlowSpawn.SpawnFlags)0;
			if ((spawnFlags & SlowSpawn.SpawnFlags.MeshFilter) == SlowSpawn.SpawnFlags.MeshFilter && (safe || !go.GetComponent<MeshFilter>()))
			{
				spawnFlags2 |= SlowSpawn.SpawnFlags.MeshFilter;
				this.AddMeshFilter(go);
			}
			if ((spawnFlags & SlowSpawn.SpawnFlags.Renderer) == SlowSpawn.SpawnFlags.Renderer && (safe || !go.renderer))
			{
				spawnFlags2 |= SlowSpawn.SpawnFlags.Renderer;
				this.AddRenderer(go);
			}
			if ((spawnFlags & SlowSpawn.SpawnFlags.Collider) == SlowSpawn.SpawnFlags.Collider && (safe || !go.collider))
			{
				spawnFlags2 |= SlowSpawn.SpawnFlags.Collider;
				this.AddCollider(go);
			}
			return spawnFlags2;
		}

		// Token: 0x06002B00 RID: 11008 RVA: 0x000ABF74 File Offset: 0x000AA174
		public SlowSpawn.SpawnFlags AddTo(GameObject go, SlowSpawn.SpawnFlags spawnFlags = SlowSpawn.SpawnFlags.All)
		{
			return this.AddTo(go, spawnFlags, false);
		}

		// Token: 0x06002B01 RID: 11009 RVA: 0x000ABF80 File Offset: 0x000AA180
		public GameObject Spawn(SlowSpawn.SpawnFlags spawnFlags = SlowSpawn.SpawnFlags.All, HideFlags HideFlags = 9)
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

		// Token: 0x04001790 RID: 6032
		public const SlowSpawn.SpawnFlags DefaultSpawnFlags = SlowSpawn.SpawnFlags.All;

		// Token: 0x04001791 RID: 6033
		public const HideFlags DefaultHideFlags = 9;

		// Token: 0x04001792 RID: 6034
		public readonly Vector3 Position;

		// Token: 0x04001793 RID: 6035
		public readonly Vector3 Scale;

		// Token: 0x04001794 RID: 6036
		public readonly Quaternion Rotation;

		// Token: 0x04001795 RID: 6037
		public readonly Mesh Mesh;

		// Token: 0x04001796 RID: 6038
		public readonly Material SharedMaterial;

		// Token: 0x04001797 RID: 6039
		public readonly int Layer;

		// Token: 0x04001798 RID: 6040
		public readonly int Index;
	}
}
