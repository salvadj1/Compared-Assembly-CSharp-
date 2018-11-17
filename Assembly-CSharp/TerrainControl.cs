using System;
using Facepunch;
using UnityEngine;

// Token: 0x0200068D RID: 1677
[ExecuteInEditMode]
public sealed class TerrainControl : MonoBehaviour
{
	// Token: 0x06003A05 RID: 14853 RVA: 0x000D7E6C File Offset: 0x000D606C
	[ContextMenu("Get settings from terrain")]
	private void CopyTerrainSettings()
	{
		this.settings.CopyFrom(this.terrain);
	}

	// Token: 0x06003A06 RID: 14854 RVA: 0x000D7E80 File Offset: 0x000D6080
	[ContextMenu("Set settings to terrain")]
	private void RestoreTerrainSettings()
	{
		this.settings.CopyTo(this.terrain);
	}

	// Token: 0x17000B3C RID: 2876
	// (get) Token: 0x06003A07 RID: 14855 RVA: 0x000D7E94 File Offset: 0x000D6094
	// (set) Token: 0x06003A08 RID: 14856 RVA: 0x000D7E9C File Offset: 0x000D609C
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

	// Token: 0x17000B3D RID: 2877
	// (get) Token: 0x06003A09 RID: 14857 RVA: 0x000D7EAC File Offset: 0x000D60AC
	public Terrain terrain
	{
		get
		{
			return this._terrain;
		}
	}

	// Token: 0x06003A0A RID: 14858 RVA: 0x000D7EB4 File Offset: 0x000D60B4
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

	// Token: 0x06003A0B RID: 14859 RVA: 0x000D7F0C File Offset: 0x000D610C
	private void OnApplicationQuit()
	{
		this.quitting = true;
	}

	// Token: 0x06003A0C RID: 14860 RVA: 0x000D7F18 File Offset: 0x000D6118
	private void OnEnable()
	{
		TerrainControl.activeTerrainControl = this;
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

	// Token: 0x06003A0D RID: 14861 RVA: 0x000D7F6C File Offset: 0x000D616C
	internal static void ter_reassign()
	{
		if (TerrainControl.activeTerrainControl)
		{
			TerrainControl.activeTerrainControl.DoReassignmentOfTerrainData(true, false, false, false);
		}
	}

	// Token: 0x06003A0E RID: 14862 RVA: 0x000D7F8C File Offset: 0x000D618C
	internal static void ter_reassign_nocopy()
	{
		if (TerrainControl.activeTerrainControl)
		{
			TerrainControl.activeTerrainControl.DoReassignmentOfTerrainData(true, false, false, true);
		}
	}

	// Token: 0x06003A0F RID: 14863 RVA: 0x000D7FAC File Offset: 0x000D61AC
	internal static void ter_flush()
	{
		if (TerrainControl.activeTerrainControl)
		{
			TerrainControl.activeTerrainControl.DoReassignmentOfTerrainData(false, true, false, false);
		}
	}

	// Token: 0x06003A10 RID: 14864 RVA: 0x000D7FCC File Offset: 0x000D61CC
	internal static void ter_mat()
	{
		if (TerrainControl.activeTerrainControl)
		{
			TerrainControl.activeTerrainControl.DoReassignmentOfTerrainData(false, false, true, false);
		}
	}

	// Token: 0x06003A11 RID: 14865 RVA: 0x000D7FEC File Offset: 0x000D61EC
	internal static void ter_flushtrees()
	{
		if (TerrainControl.activeTerrainControl && TerrainControl.activeTerrainControl._terrain)
		{
			TerrainHack.RefreshTreeTextures(TerrainControl.activeTerrainControl._terrain);
		}
	}

	// Token: 0x06003A12 RID: 14866 RVA: 0x000D802C File Offset: 0x000D622C
	private bool DoReassignmentOfTerrainData(bool td, bool andFlush, bool mats, bool doNotCopySettings)
	{
		if (!this.terrainDataFromBundle && !Bundling.Load<TerrainData>(this.bundlePathToTerrainData, out this.terrainDataFromBundle))
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

	// Token: 0x06003A13 RID: 14867 RVA: 0x000D8100 File Offset: 0x000D6300
	private void ReassignTerrainData()
	{
		if (Application.isPlaying && !global::terrain.manual)
		{
			if (!Bundling.Load<TerrainData>(this.bundlePathToTerrainData, out this.terrainDataFromBundle))
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

	// Token: 0x06003A14 RID: 14868 RVA: 0x000D81A4 File Offset: 0x000D63A4
	private void OnDisable()
	{
		if (!this.quitting && this.running)
		{
			this.running = false;
		}
	}

	// Token: 0x06003A15 RID: 14869 RVA: 0x000D81C4 File Offset: 0x000D63C4
	private void BindTerrainSettings()
	{
		if (this.forceCustomBasemapDistance && this.terrain)
		{
			this.terrain.basemapDistance = this.customBasemapDistance;
		}
	}

	// Token: 0x06003A16 RID: 14870 RVA: 0x000D8200 File Offset: 0x000D6400
	private void Update()
	{
		float idleinterval = global::terrain.idleinterval;
		MountedCamera main;
		bool flag;
		if (idleinterval <= 0f || !(main = MountedCamera.main))
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
				TerrainHack.RefreshTreeTextures(this._terrain);
			}
		}
	}

	// Token: 0x04001E22 RID: 7714
	[SerializeField]
	private Terrain _terrain;

	// Token: 0x04001E23 RID: 7715
	[SerializeField]
	private float _customBasemapDistance = 10000f;

	// Token: 0x04001E24 RID: 7716
	[NonSerialized]
	private bool running;

	// Token: 0x04001E25 RID: 7717
	[NonSerialized]
	private bool quitting;

	// Token: 0x04001E26 RID: 7718
	[SerializeField]
	private Material _terrainMaterialTemplate;

	// Token: 0x04001E27 RID: 7719
	private static TerrainControl activeTerrainControl;

	// Token: 0x04001E28 RID: 7720
	[SerializeField]
	private TerrainControl.TerrainSettingsHack settings;

	// Token: 0x04001E29 RID: 7721
	public bool forceCustomBasemapDistance = true;

	// Token: 0x04001E2A RID: 7722
	public string bundlePathToTerrainData = "Env/ter/rust_island_2013-2";

	// Token: 0x04001E2B RID: 7723
	public float reassignTerrainDataInterval;

	// Token: 0x04001E2C RID: 7724
	private TerrainData terrainDataFromBundle;

	// Token: 0x04001E2D RID: 7725
	[NonSerialized]
	private float timeNoticedCameraChange;

	// Token: 0x04001E2E RID: 7726
	[NonSerialized]
	private Vector3 lastCameraPosition;

	// Token: 0x04001E2F RID: 7727
	[NonSerialized]
	private Vector3 lastCameraForward;

	// Token: 0x0200068E RID: 1678
	[Serializable]
	private class TerrainSettingsHack
	{
		// Token: 0x06003A18 RID: 14872 RVA: 0x000D837C File Offset: 0x000D657C
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

		// Token: 0x06003A19 RID: 14873 RVA: 0x000D8410 File Offset: 0x000D6610
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

		// Token: 0x04001E30 RID: 7728
		public float basemapDistance;

		// Token: 0x04001E31 RID: 7729
		public bool castShadows;

		// Token: 0x04001E32 RID: 7730
		public float detailObjectDensity;

		// Token: 0x04001E33 RID: 7731
		public float detailObjectDistance;

		// Token: 0x04001E34 RID: 7732
		public int heightmapMaximumLOD;

		// Token: 0x04001E35 RID: 7733
		public float heightmapPixelError;

		// Token: 0x04001E36 RID: 7734
		public Material materialTemplate;

		// Token: 0x04001E37 RID: 7735
		public float treeBillboardDistance;

		// Token: 0x04001E38 RID: 7736
		public float treeCrossFadeLength;

		// Token: 0x04001E39 RID: 7737
		public float treeDistance;

		// Token: 0x04001E3A RID: 7738
		public int treeMaximumFullLODCount;
	}
}
