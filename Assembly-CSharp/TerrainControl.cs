using System;
using Facepunch;
using UnityEngine;

// Token: 0x02000752 RID: 1874
[ExecuteInEditMode]
public sealed class TerrainControl : MonoBehaviour
{
	// Token: 0x06003DFD RID: 15869 RVA: 0x000E084C File Offset: 0x000DEA4C
	[ContextMenu("Get settings from terrain")]
	private void CopyTerrainSettings()
	{
		this.settings.CopyFrom(this.terrain);
	}

	// Token: 0x06003DFE RID: 15870 RVA: 0x000E0860 File Offset: 0x000DEA60
	[ContextMenu("Set settings to terrain")]
	private void RestoreTerrainSettings()
	{
		this.settings.CopyTo(this.terrain);
	}

	// Token: 0x17000BBE RID: 3006
	// (get) Token: 0x06003DFF RID: 15871 RVA: 0x000E0874 File Offset: 0x000DEA74
	// (set) Token: 0x06003E00 RID: 15872 RVA: 0x000E087C File Offset: 0x000DEA7C
	public float customBasemapDistance
	{
		get
		{
			return this._customBasemapDistance;
		}
		set
		{
			this._customBasemapDistance = value;
			this.BindTerrainSettings();
		}
	}

	// Token: 0x17000BBF RID: 3007
	// (get) Token: 0x06003E01 RID: 15873 RVA: 0x000E088C File Offset: 0x000DEA8C
	public Terrain terrain
	{
		get
		{
			return this._terrain;
		}
	}

	// Token: 0x06003E02 RID: 15874 RVA: 0x000E0894 File Offset: 0x000DEA94
	private void Reset()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Main Terrain");
		if (array.Length > 0)
		{
			for (int i = 0; i < array.Length; i++)
			{
				this._terrain = array[i].GetComponent<Terrain>();
				if (this._terrain)
				{
					break;
				}
			}
		}
	}

	// Token: 0x06003E03 RID: 15875 RVA: 0x000E08EC File Offset: 0x000DEAEC
	private void OnApplicationQuit()
	{
		this.quitting = true;
	}

	// Token: 0x06003E04 RID: 15876 RVA: 0x000E08F8 File Offset: 0x000DEAF8
	private void OnEnable()
	{
		global::TerrainControl.activeTerrainControl = this;
		this.quitting = false;
		if (!this.running)
		{
			this.running = true;
			this.BindTerrainSettings();
		}
		if (this.reassignTerrainDataInterval > 0f)
		{
			base.Invoke("ReassignTerrainData", this.reassignTerrainDataInterval);
		}
	}

	// Token: 0x06003E05 RID: 15877 RVA: 0x000E094C File Offset: 0x000DEB4C
	internal static void ter_reassign()
	{
		if (global::TerrainControl.activeTerrainControl)
		{
			global::TerrainControl.activeTerrainControl.DoReassignmentOfTerrainData(true, false, false, false);
		}
	}

	// Token: 0x06003E06 RID: 15878 RVA: 0x000E096C File Offset: 0x000DEB6C
	internal static void ter_reassign_nocopy()
	{
		if (global::TerrainControl.activeTerrainControl)
		{
			global::TerrainControl.activeTerrainControl.DoReassignmentOfTerrainData(true, false, false, true);
		}
	}

	// Token: 0x06003E07 RID: 15879 RVA: 0x000E098C File Offset: 0x000DEB8C
	internal static void ter_flush()
	{
		if (global::TerrainControl.activeTerrainControl)
		{
			global::TerrainControl.activeTerrainControl.DoReassignmentOfTerrainData(false, true, false, false);
		}
	}

	// Token: 0x06003E08 RID: 15880 RVA: 0x000E09AC File Offset: 0x000DEBAC
	internal static void ter_mat()
	{
		if (global::TerrainControl.activeTerrainControl)
		{
			global::TerrainControl.activeTerrainControl.DoReassignmentOfTerrainData(false, false, true, false);
		}
	}

	// Token: 0x06003E09 RID: 15881 RVA: 0x000E09CC File Offset: 0x000DEBCC
	internal static void ter_flushtrees()
	{
		if (global::TerrainControl.activeTerrainControl && global::TerrainControl.activeTerrainControl._terrain)
		{
			global::TerrainHack.RefreshTreeTextures(global::TerrainControl.activeTerrainControl._terrain);
		}
	}

	// Token: 0x06003E0A RID: 15882 RVA: 0x000E0A0C File Offset: 0x000DEC0C
	private bool DoReassignmentOfTerrainData(bool td, bool andFlush, bool mats, bool doNotCopySettings)
	{
		if (!this.terrainDataFromBundle && !Facepunch.Bundling.Load<TerrainData>(this.bundlePathToTerrainData, out this.terrainDataFromBundle))
		{
			Debug.LogError("Bad terrain data path " + this.bundlePathToTerrainData);
			return true;
		}
		if (td)
		{
			if (doNotCopySettings)
			{
				this.terrain.terrainData = this.terrainDataFromBundle;
			}
			else
			{
				this.terrain.terrainData = this.terrainDataFromBundle;
				this.RestoreTerrainSettings();
			}
		}
		if (mats)
		{
			this.terrain.materialTemplate = this._terrainMaterialTemplate;
		}
		if (andFlush)
		{
			this.terrain.Flush();
			if (mats)
			{
				this.terrain.materialTemplate = this._terrainMaterialTemplate;
			}
		}
		return !this.terrainDataFromBundle;
	}

	// Token: 0x06003E0B RID: 15883 RVA: 0x000E0AE0 File Offset: 0x000DECE0
	private void ReassignTerrainData()
	{
		if (Application.isPlaying && !global::terrain.manual)
		{
			if (!Facepunch.Bundling.Load<TerrainData>(this.bundlePathToTerrainData, out this.terrainDataFromBundle))
			{
				Debug.LogError("Bad terrain data path " + this.bundlePathToTerrainData);
			}
			try
			{
				this.terrain.terrainData = this.terrainDataFromBundle;
				this.RestoreTerrainSettings();
			}
			catch (Exception ex)
			{
				Debug.Log(ex, this);
				base.Invoke("ReassignTerrainData", this.reassignTerrainDataInterval);
			}
		}
	}

	// Token: 0x06003E0C RID: 15884 RVA: 0x000E0B84 File Offset: 0x000DED84
	private void OnDisable()
	{
		if (!this.quitting && this.running)
		{
			this.running = false;
		}
	}

	// Token: 0x06003E0D RID: 15885 RVA: 0x000E0BA4 File Offset: 0x000DEDA4
	private void BindTerrainSettings()
	{
		if (this.forceCustomBasemapDistance && this.terrain)
		{
			this.terrain.basemapDistance = this.customBasemapDistance;
		}
	}

	// Token: 0x06003E0E RID: 15886 RVA: 0x000E0BE0 File Offset: 0x000DEDE0
	private void Update()
	{
		float idleinterval = global::terrain.idleinterval;
		global::MountedCamera main;
		bool flag;
		if (idleinterval <= 0f || !(main = global::MountedCamera.main))
		{
			flag = true;
		}
		else
		{
			Vector3 position = main.transform.position;
			Vector3 forward = main.transform.forward;
			forward.Normalize();
			Vector3 vector;
			vector.x = position.x - this.lastCameraPosition.x;
			vector.y = position.y - this.lastCameraPosition.y;
			vector.z = position.z - this.lastCameraPosition.z;
			float num = vector.x * vector.x + vector.y * vector.y + vector.z * vector.z;
			bool flag2 = num > 5.625E-05f || Vector3.Angle(forward, this.lastCameraForward) > 0.5f;
			if (flag2)
			{
				this.lastCameraPosition = position;
				this.lastCameraForward = forward;
				flag = true;
			}
			else
			{
				flag = false;
			}
		}
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		if (flag)
		{
			this.timeNoticedCameraChange = realtimeSinceStartup;
		}
		else
		{
			float num2 = Time.realtimeSinceStartup - this.timeNoticedCameraChange;
			if (num2 > idleinterval)
			{
				this.timeNoticedCameraChange = ((idleinterval <= 0f) ? realtimeSinceStartup : (realtimeSinceStartup - num2 % idleinterval));
				global::TerrainHack.RefreshTreeTextures(this._terrain);
			}
		}
	}

	// Token: 0x0400201A RID: 8218
	[SerializeField]
	private Terrain _terrain;

	// Token: 0x0400201B RID: 8219
	[SerializeField]
	private float _customBasemapDistance = 10000f;

	// Token: 0x0400201C RID: 8220
	[NonSerialized]
	private bool running;

	// Token: 0x0400201D RID: 8221
	[NonSerialized]
	private bool quitting;

	// Token: 0x0400201E RID: 8222
	[SerializeField]
	private Material _terrainMaterialTemplate;

	// Token: 0x0400201F RID: 8223
	private static global::TerrainControl activeTerrainControl;

	// Token: 0x04002020 RID: 8224
	[SerializeField]
	private global::TerrainControl.TerrainSettingsHack settings;

	// Token: 0x04002021 RID: 8225
	public bool forceCustomBasemapDistance = true;

	// Token: 0x04002022 RID: 8226
	public string bundlePathToTerrainData = "Env/ter/rust_island_2013-2";

	// Token: 0x04002023 RID: 8227
	public float reassignTerrainDataInterval;

	// Token: 0x04002024 RID: 8228
	private TerrainData terrainDataFromBundle;

	// Token: 0x04002025 RID: 8229
	[NonSerialized]
	private float timeNoticedCameraChange;

	// Token: 0x04002026 RID: 8230
	[NonSerialized]
	private Vector3 lastCameraPosition;

	// Token: 0x04002027 RID: 8231
	[NonSerialized]
	private Vector3 lastCameraForward;

	// Token: 0x02000753 RID: 1875
	[Serializable]
	private class TerrainSettingsHack
	{
		// Token: 0x06003E10 RID: 15888 RVA: 0x000E0D5C File Offset: 0x000DEF5C
		public void CopyFrom(Terrain terrain)
		{
			this.basemapDistance = terrain.basemapDistance;
			this.castShadows = terrain.castShadows;
			this.detailObjectDensity = terrain.detailObjectDensity;
			this.detailObjectDistance = terrain.detailObjectDistance;
			this.heightmapMaximumLOD = terrain.heightmapMaximumLOD;
			this.heightmapPixelError = terrain.heightmapPixelError;
			this.materialTemplate = terrain.materialTemplate;
			this.treeBillboardDistance = terrain.treeBillboardDistance;
			this.treeCrossFadeLength = terrain.treeCrossFadeLength;
			this.treeDistance = terrain.treeDistance;
			this.treeMaximumFullLODCount = terrain.treeMaximumFullLODCount;
		}

		// Token: 0x06003E11 RID: 15889 RVA: 0x000E0DF0 File Offset: 0x000DEFF0
		public void CopyTo(Terrain terrain)
		{
			terrain.basemapDistance = this.basemapDistance;
			terrain.castShadows = this.castShadows;
			terrain.detailObjectDensity = this.detailObjectDensity;
			terrain.detailObjectDistance = this.detailObjectDistance;
			terrain.heightmapMaximumLOD = this.heightmapMaximumLOD;
			terrain.heightmapPixelError = this.heightmapPixelError;
			terrain.materialTemplate = this.materialTemplate;
			terrain.treeBillboardDistance = this.treeBillboardDistance;
			terrain.treeCrossFadeLength = this.treeCrossFadeLength;
			terrain.treeDistance = this.treeDistance;
			terrain.treeMaximumFullLODCount = this.treeMaximumFullLODCount;
		}

		// Token: 0x04002028 RID: 8232
		public float basemapDistance;

		// Token: 0x04002029 RID: 8233
		public bool castShadows;

		// Token: 0x0400202A RID: 8234
		public float detailObjectDensity;

		// Token: 0x0400202B RID: 8235
		public float detailObjectDistance;

		// Token: 0x0400202C RID: 8236
		public int heightmapMaximumLOD;

		// Token: 0x0400202D RID: 8237
		public float heightmapPixelError;

		// Token: 0x0400202E RID: 8238
		public Material materialTemplate;

		// Token: 0x0400202F RID: 8239
		public float treeBillboardDistance;

		// Token: 0x04002030 RID: 8240
		public float treeCrossFadeLength;

		// Token: 0x04002031 RID: 8241
		public float treeDistance;

		// Token: 0x04002032 RID: 8242
		public int treeMaximumFullLODCount;
	}
}
