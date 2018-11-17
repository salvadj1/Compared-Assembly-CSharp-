using System;
using System.Collections;
using System.Collections.Generic;
using EasyRoads3D;
using UnityEngine;

// Token: 0x0200074C RID: 1868
public class RoadObjectScript : MonoBehaviour
{
	// Token: 0x06004430 RID: 17456 RVA: 0x001085B0 File Offset: 0x001067B0
	public void OCOQDDODDQ(ArrayList arr, string[] DOODQOQO, string[] OODDQOQO)
	{
		this.ODOCOQCCOC(base.transform, arr, DOODQOQO, OODDQOQO);
	}

	// Token: 0x06004431 RID: 17457 RVA: 0x001085C4 File Offset: 0x001067C4
	public void OCCOCQQQDO(MarkerScript markerScript)
	{
		this.OCQOCOCQQO = markerScript.transform;
		List<GameObject> list = new List<GameObject>();
		for (int i = 0; i < this.OCQOCOCQQOs.Length; i++)
		{
			if (this.OCQOCOCQQOs[i] != markerScript.gameObject)
			{
				list.Add(this.OCQOCOCQQOs[i]);
			}
		}
		list.Add(markerScript.gameObject);
		this.OCQOCOCQQOs = list.ToArray();
		this.OCQOCOCQQO = markerScript.transform;
		this.OOQQCODOCD.OODCDQDOCO(this.OCQOCOCQQO, this.OCQOCOCQQOs, markerScript.OCCCCODCOD, markerScript.OQCQOQQDCQ, this.OODDDCQCOC, ref markerScript.OCQOCOCQQOs, ref markerScript.trperc, this.OCQOCOCQQOs);
		this.ODOCDOOOQQ = -1;
	}

	// Token: 0x06004432 RID: 17458 RVA: 0x00108688 File Offset: 0x00106888
	public void OCOQDCQOCD(MarkerScript markerScript)
	{
		if (markerScript.OQCQOQQDCQ != markerScript.ODOOQQOO || markerScript.OQCQOQQDCQ != markerScript.ODOOQQOO)
		{
			this.OOQQCODOCD.OODCDQDOCO(this.OCQOCOCQQO, this.OCQOCOCQQOs, markerScript.OCCCCODCOD, markerScript.OQCQOQQDCQ, this.OODDDCQCOC, ref markerScript.OCQOCOCQQOs, ref markerScript.trperc, this.OCQOCOCQQOs);
			markerScript.ODQDOQOO = markerScript.OCCCCODCOD;
			markerScript.ODOOQQOO = markerScript.OQCQOQQDCQ;
		}
		if (this.OODCCOODCC.autoUpdate)
		{
			this.OCOOCODDOC(this.OODCCOODCC.geoResolution, false, false);
		}
	}

	// Token: 0x06004433 RID: 17459 RVA: 0x00108730 File Offset: 0x00106930
	public void ResetMaterials(MarkerScript markerScript)
	{
		if (this.OOQQCODOCD != null)
		{
			this.OOQQCODOCD.OODCDQDOCO(this.OCQOCOCQQO, this.OCQOCOCQQOs, markerScript.OCCCCODCOD, markerScript.OQCQOQQDCQ, this.OODDDCQCOC, ref markerScript.OCQOCOCQQOs, ref markerScript.trperc, this.OCQOCOCQQOs);
		}
	}

	// Token: 0x06004434 RID: 17460 RVA: 0x00108784 File Offset: 0x00106984
	public void OOOOQCDODD(MarkerScript markerScript)
	{
		if (markerScript.OQCQOQQDCQ != markerScript.ODOOQQOO)
		{
			this.OOQQCODOCD.OODCDQDOCO(this.OCQOCOCQQO, this.OCQOCOCQQOs, markerScript.OCCCCODCOD, markerScript.OQCQOQQDCQ, this.OODDDCQCOC, ref markerScript.OCQOCOCQQOs, ref markerScript.trperc, this.OCQOCOCQQOs);
			markerScript.ODOOQQOO = markerScript.OQCQOQQDCQ;
		}
		this.OCOOCODDOC(this.OODCCOODCC.geoResolution, false, false);
	}

	// Token: 0x06004435 RID: 17461 RVA: 0x001087FC File Offset: 0x001069FC
	private void OQDODCODOQ(string ctrl, MarkerScript markerScript)
	{
		int num = 0;
		foreach (Transform transform in markerScript.OCQOCOCQQOs)
		{
			MarkerScript component = transform.GetComponent<MarkerScript>();
			if (ctrl == "rs")
			{
				component.LeftSurrounding(markerScript.rs - markerScript.ODOQQOOO, markerScript.trperc[num]);
			}
			else if (ctrl == "ls")
			{
				component.RightSurrounding(markerScript.ls - markerScript.DODOQQOO, markerScript.trperc[num]);
			}
			else if (ctrl == "ri")
			{
				component.LeftIndent(markerScript.ri - markerScript.OOQOQQOO, markerScript.trperc[num]);
			}
			else if (ctrl == "li")
			{
				component.RightIndent(markerScript.li - markerScript.ODODQQOO, markerScript.trperc[num]);
			}
			else if (ctrl == "rt")
			{
				component.LeftTilting(markerScript.rt - markerScript.ODDQODOO, markerScript.trperc[num]);
			}
			else if (ctrl == "lt")
			{
				component.RightTilting(markerScript.lt - markerScript.ODDOQOQQ, markerScript.trperc[num]);
			}
			else if (ctrl == "floorDepth")
			{
				component.FloorDepth(markerScript.floorDepth - markerScript.oldFloorDepth, markerScript.trperc[num]);
			}
			num++;
		}
	}

	// Token: 0x06004436 RID: 17462 RVA: 0x00108988 File Offset: 0x00106B88
	public void OQOCODCDOO()
	{
		if (this.markers > 1)
		{
			this.OCOOCODDOC(this.OODCCOODCC.geoResolution, false, false);
		}
	}

	// Token: 0x06004437 RID: 17463 RVA: 0x001089AC File Offset: 0x00106BAC
	public void ODOCOQCCOC(Transform tr, ArrayList arr, string[] DOODQOQO, string[] OODDQOQO)
	{
		RoadObjectScript.version = "2.4.6";
		RoadObjectScript.OODCDOQDCC = (GUISkin)Resources.Load("ER3DSkin", typeof(GUISkin));
		RoadObjectScript.OQOOODODQD = (Texture2D)Resources.Load("ER3DLogo", typeof(Texture2D));
		if (RoadObjectScript.objectStrings == null)
		{
			RoadObjectScript.objectStrings = new string[3];
			RoadObjectScript.objectStrings[0] = "Road Object";
			RoadObjectScript.objectStrings[1] = "River Object";
			RoadObjectScript.objectStrings[2] = "Procedural Mesh Object";
		}
		this.obj = tr;
		this.OOQQCODOCD = new OQCDQQDQCC();
		this.OODCCOODCC = this.obj.GetComponent<RoadObjectScript>();
		foreach (object obj in this.obj)
		{
			Transform transform = (Transform)obj;
			if (transform.name == "Markers")
			{
				this.OODDDCQCOC = transform;
			}
		}
		OQCDQQDQCC.terrainList.Clear();
		Terrain[] array = (Terrain[])Object.FindObjectsOfType(typeof(Terrain));
		foreach (Terrain terrain in array)
		{
			Terrains terrains = new Terrains();
			terrains.terrain = terrain;
			if (!terrain.gameObject.GetComponent<EasyRoads3DTerrainID>())
			{
				EasyRoads3DTerrainID easyRoads3DTerrainID = (EasyRoads3DTerrainID)terrain.gameObject.AddComponent("EasyRoads3DTerrainID");
				string text = Random.Range(100000000, 999999999).ToString();
				easyRoads3DTerrainID.terrainid = text;
				terrains.id = text;
			}
			else
			{
				terrains.id = terrain.gameObject.GetComponent<EasyRoads3DTerrainID>().terrainid;
			}
			this.OOQQCODOCD.OCDQQCDOQO(terrains);
		}
		ODCDDDDQQD.OCDQQCDOQO();
		if (this.roadMaterialEdit == null)
		{
			this.roadMaterialEdit = (Material)Resources.Load("materials/roadMaterialEdit", typeof(Material));
		}
		if (this.objectType == 0 && GameObject.Find(base.gameObject.name + "/road") == null)
		{
			GameObject gameObject = new GameObject("road");
			gameObject.transform.parent = base.transform;
		}
		this.OOQQCODOCD.OODQOQCDCQ(this.obj, RoadObjectScript.OCQCDDDOCC, this.OODCCOODCC.roadWidth, this.surfaceOpacity, ref this.OOCCDCOQCQ, ref this.indent, this.applyAnimation, this.waveSize, this.waveHeight);
		this.OOQQCODOCD.ODDQCCDCDC = this.ODDQCCDCDC;
		this.OOQQCODOCD.OOCQDOOCQD = this.OOCQDOOCQD;
		this.OOQQCODOCD.OdQODQOD = this.OdQODQOD + 1;
		this.OOQQCODOCD.OOQQQDOD = this.OOQQQDOD;
		this.OOQQCODOCD.OOQQQDODOffset = this.OOQQQDODOffset;
		this.OOQQCODOCD.OOQQQDODLength = this.OOQQQDODLength;
		this.OOQQCODOCD.objectType = this.objectType;
		this.OOQQCODOCD.snapY = this.snapY;
		this.OOQQCODOCD.terrainRendered = this.ODODCOCCDQ;
		this.OOQQCODOCD.handleVegetation = this.handleVegetation;
		this.OOQQCODOCD.raise = this.raise;
		this.OOQQCODOCD.roadResolution = this.roadResolution;
		this.OOQQCODOCD.multipleTerrains = this.multipleTerrains;
		this.OOQQCODOCD.editRestore = this.editRestore;
		this.OOQQCODOCD.roadMaterialEdit = this.roadMaterialEdit;
		if (RoadObjectScript.backupLocation == 0)
		{
			OOCDQCOODC.backupFolder = "/EasyRoads3D";
		}
		else
		{
			OOCDQCOODC.backupFolder = "/Assets/EasyRoads3D/backups";
		}
		this.ODODQOQO = this.OOQQCODOCD.OCDODCOCOC();
		this.ODODQOQOInt = this.OOQQCODOCD.OCCQOQCQDO();
		if (this.ODODCOCCDQ)
		{
			this.doRestore = true;
		}
		this.OOQODQOCOC();
		if (arr != null || RoadObjectScript.ODODQOOQ == null)
		{
			this.OOOOOOODCD(arr, DOODQOQO, OODDQOQO);
		}
		if (this.doRestore)
		{
			return;
		}
	}

	// Token: 0x06004438 RID: 17464 RVA: 0x00108DF4 File Offset: 0x00106FF4
	public void UpdateBackupFolder()
	{
	}

	// Token: 0x06004439 RID: 17465 RVA: 0x00108DF8 File Offset: 0x00106FF8
	public void OCCOOQDCQO()
	{
		if ((!this.ODODDDOO || this.objectType == 2) && this.OQCCDQCDDD != null)
		{
			for (int i = 0; i < this.OQCCDQCDDD.Length; i++)
			{
				this.OQCCDQCDDD[i] = false;
				this.ODODODCODD[i] = false;
			}
		}
	}

	// Token: 0x0600443A RID: 17466 RVA: 0x00108E54 File Offset: 0x00107054
	public void OODDQODDCC(Vector3 pos)
	{
		if (!this.displayRoad)
		{
			this.displayRoad = true;
			this.OOQQCODOCD.OODDDCQCCQ(this.displayRoad, this.OODDDCQCOC);
		}
		pos.y += this.OODCCOODCC.raiseMarkers;
		if (this.forceY && this.ODOQDQOO != null)
		{
			float num = Vector3.Distance(pos, this.ODOQDQOO.transform.position);
			pos.y = this.ODOQDQOO.transform.position.y + this.yChange * (num / 100f);
		}
		else if (this.forceY && this.markers == 0)
		{
			this.lastY = pos.y;
		}
		GameObject gameObject;
		if (this.ODOQDQOO != null)
		{
			gameObject = (GameObject)Object.Instantiate(this.ODOQDQOO);
		}
		else
		{
			gameObject = (GameObject)Object.Instantiate(Resources.Load("marker", typeof(GameObject)));
		}
		Transform transform = gameObject.transform;
		transform.position = pos;
		transform.parent = this.OODDDCQCOC;
		this.markers++;
		string name;
		if (this.markers < 10)
		{
			name = "Marker000" + this.markers.ToString();
		}
		else if (this.markers < 100)
		{
			name = "Marker00" + this.markers.ToString();
		}
		else
		{
			name = "Marker0" + this.markers.ToString();
		}
		transform.gameObject.name = name;
		MarkerScript component = transform.GetComponent<MarkerScript>();
		component.OOCCDCOQCQ = false;
		component.objectScript = this.obj.GetComponent<RoadObjectScript>();
		if (this.ODOQDQOO == null)
		{
			component.waterLevel = this.OODCCOODCC.waterLevel;
			component.floorDepth = this.OODCCOODCC.floorDepth;
			component.ri = this.OODCCOODCC.indent;
			component.li = this.OODCCOODCC.indent;
			component.rs = this.OODCCOODCC.surrounding;
			component.ls = this.OODCCOODCC.surrounding;
			component.tension = 0.5f;
			if (this.objectType == 1)
			{
				pos.y -= this.waterLevel;
				transform.position = pos;
			}
		}
		if (this.objectType == 2 && component.surface != null)
		{
			component.surface.gameObject.SetActive(false);
		}
		this.ODOQDQOO = transform.gameObject;
		if (this.markers > 1)
		{
			this.OCOOCODDOC(this.OODCCOODCC.geoResolution, false, false);
			if (this.materialType == 0)
			{
				this.OOQQCODOCD.OOQOOCDQOD(this.materialType);
			}
		}
	}

	// Token: 0x0600443B RID: 17467 RVA: 0x0010915C File Offset: 0x0010735C
	public void OCOOCODDOC(float geo, bool renderMode, bool camMode)
	{
		this.OOQQCODOCD.OOODOQDODQ.Clear();
		int num = 0;
		foreach (object obj in this.obj)
		{
			Transform transform = (Transform)obj;
			if (transform.name == "Markers")
			{
				foreach (object obj2 in transform)
				{
					Transform transform2 = (Transform)obj2;
					MarkerScript component = transform2.GetComponent<MarkerScript>();
					component.objectScript = this.obj.GetComponent<RoadObjectScript>();
					if (!component.OOCCDCOQCQ)
					{
						component.OOCCDCOQCQ = this.OOQQCODOCD.OOOCQDOCDC(transform2);
					}
					OQDQOQDOQO oqdqoqdoqo = new OQDQOQDOQO();
					oqdqoqdoqo.position = transform2.position;
					oqdqoqdoqo.num = this.OOQQCODOCD.OOODOQDODQ.Count;
					oqdqoqdoqo.object1 = transform2;
					oqdqoqdoqo.object2 = component.surface;
					oqdqoqdoqo.tension = component.tension;
					oqdqoqdoqo.ri = component.ri;
					if (oqdqoqdoqo.ri < 1f)
					{
						oqdqoqdoqo.ri = 1f;
					}
					oqdqoqdoqo.li = component.li;
					if (oqdqoqdoqo.li < 1f)
					{
						oqdqoqdoqo.li = 1f;
					}
					oqdqoqdoqo.rt = component.rt;
					oqdqoqdoqo.lt = component.lt;
					oqdqoqdoqo.rs = component.rs;
					if (oqdqoqdoqo.rs < 1f)
					{
						oqdqoqdoqo.rs = 1f;
					}
					oqdqoqdoqo.OQDOOODDQD = component.rs;
					oqdqoqdoqo.ls = component.ls;
					if (oqdqoqdoqo.ls < 1f)
					{
						oqdqoqdoqo.ls = 1f;
					}
					oqdqoqdoqo.OOOCDQODDO = component.ls;
					oqdqoqdoqo.renderFlag = component.bridgeObject;
					oqdqoqdoqo.OCCOQCQDOD = component.distHeights;
					oqdqoqdoqo.newSegment = component.newSegment;
					oqdqoqdoqo.floorDepth = component.floorDepth;
					oqdqoqdoqo.waterLevel = this.waterLevel;
					oqdqoqdoqo.lockWaterLevel = component.lockWaterLevel;
					oqdqoqdoqo.sharpCorner = component.sharpCorner;
					oqdqoqdoqo.OQCDCODODQ = this.OOQQCODOCD;
					component.markerNum = num;
					component.distance = "-1";
					component.OODDQCQQDD = "-1";
					this.OOQQCODOCD.OOODOQDODQ.Add(oqdqoqdoqo);
					num++;
				}
			}
		}
		this.distance = "-1";
		this.OOQQCODOCD.ODQQQCQCOO = this.OODCCOODCC.roadWidth;
		this.OOQQCODOCD.ODOCODQOOC(geo, this.obj, this.OODCCOODCC.OOQDOOQQ, renderMode, camMode, this.objectType);
		if (this.OOQQCODOCD.leftVecs.Count > 0)
		{
			this.leftVecs = this.OOQQCODOCD.leftVecs.ToArray();
			this.rightVecs = this.OOQQCODOCD.rightVecs.ToArray();
		}
	}

	// Token: 0x0600443C RID: 17468 RVA: 0x001094C8 File Offset: 0x001076C8
	public void StartCam()
	{
		this.OCOOCODDOC(0.5f, false, true);
	}

	// Token: 0x0600443D RID: 17469 RVA: 0x001094D8 File Offset: 0x001076D8
	public void OOQODQOCOC()
	{
		int num = 0;
		foreach (object obj in this.obj)
		{
			Transform transform = (Transform)obj;
			if (transform.name == "Markers")
			{
				num = 1;
				foreach (object obj2 in transform)
				{
					Transform transform2 = (Transform)obj2;
					string name;
					if (num < 10)
					{
						name = "Marker000" + num.ToString();
					}
					else if (num < 100)
					{
						name = "Marker00" + num.ToString();
					}
					else
					{
						name = "Marker0" + num.ToString();
					}
					transform2.name = name;
					this.ODOQDQOO = transform2.gameObject;
					num++;
				}
			}
		}
		this.markers = num - 1;
		this.OCOOCODDOC(this.OODCCOODCC.geoResolution, false, false);
	}

	// Token: 0x0600443E RID: 17470 RVA: 0x0010963C File Offset: 0x0010783C
	public void ODDOOODDCQ()
	{
		RoadObjectScript[] array = (RoadObjectScript[])Object.FindObjectsOfType(typeof(RoadObjectScript));
		ArrayList arrayList = new ArrayList();
		foreach (RoadObjectScript roadObjectScript in array)
		{
			if (roadObjectScript.transform != base.transform)
			{
				arrayList.Add(roadObjectScript.transform);
			}
		}
		if (this.ODODQOQO == null)
		{
			this.ODODQOQO = this.OOQQCODOCD.OCDODCOCOC();
			this.ODODQOQOInt = this.OOQQCODOCD.OCCQOQCQDO();
		}
		this.OCOOCODDOC(0.5f, true, false);
		this.OOQQCODOCD.OCOOOOCOQO(Vector3.zero, this.OODCCOODCC.raise, this.obj, this.OODCCOODCC.OOQDOOQQ, arrayList, this.handleVegetation);
		this.OCQDCQDDCO();
	}

	// Token: 0x0600443F RID: 17471 RVA: 0x0010971C File Offset: 0x0010791C
	public ArrayList RebuildObjs()
	{
		RoadObjectScript[] array = (RoadObjectScript[])Object.FindObjectsOfType(typeof(RoadObjectScript));
		ArrayList arrayList = new ArrayList();
		foreach (RoadObjectScript roadObjectScript in array)
		{
			if (roadObjectScript.transform != base.transform)
			{
				arrayList.Add(roadObjectScript.transform);
			}
		}
		return arrayList;
	}

	// Token: 0x06004440 RID: 17472 RVA: 0x00109788 File Offset: 0x00107988
	public void ODQDOOOCOC()
	{
		this.OCOOCODDOC(this.OODCCOODCC.geoResolution, false, false);
		if (this.OOQQCODOCD != null)
		{
			this.OOQQCODOCD.ODQDOOOCOC();
		}
		this.ODODDDOO = false;
	}

	// Token: 0x06004441 RID: 17473 RVA: 0x001097C8 File Offset: 0x001079C8
	public void OCQDCQDDCO()
	{
		this.OOQQCODOCD.OCQDCQDDCO(this.OODCCOODCC.applySplatmap, this.OODCCOODCC.splatmapSmoothLevel, this.OODCCOODCC.renderRoad, this.OODCCOODCC.tuw, this.OODCCOODCC.roadResolution, this.OODCCOODCC.raise, this.OODCCOODCC.opacity, this.OODCCOODCC.expand, this.OODCCOODCC.offsetX, this.OODCCOODCC.offsetY, this.OODCCOODCC.beveledRoad, this.OODCCOODCC.splatmapLayer, this.OODCCOODCC.OdQODQOD, this.OOQQQDOD, this.OOQQQDODOffset, this.OOQQQDODLength);
	}

	// Token: 0x06004442 RID: 17474 RVA: 0x00109884 File Offset: 0x00107A84
	public void OQQDQCQQOC()
	{
		this.OOQQCODOCD.OQQDQCQQOC(this.OODCCOODCC.renderRoad, this.OODCCOODCC.tuw, this.OODCCOODCC.roadResolution, this.OODCCOODCC.raise, this.OODCCOODCC.beveledRoad, this.OODCCOODCC.OdQODQOD, this.OOQQQDOD, this.OOQQQDODOffset, this.OOQQQDODLength);
	}

	// Token: 0x06004443 RID: 17475 RVA: 0x001098F4 File Offset: 0x00107AF4
	public void ODQDCQQDDO(Vector3 pos, bool doInsert)
	{
		if (!this.displayRoad)
		{
			this.displayRoad = true;
			this.OOQQCODOCD.OODDDCQCCQ(this.displayRoad, this.OODDDCQCOC);
		}
		int num = -1;
		int num2 = -1;
		float num3 = 10000f;
		float num4 = 10000f;
		Vector3 vector = pos;
		OQDQOQDOQO oqdqoqdoqo = (OQDQOQDOQO)this.OOQQCODOCD.OOODOQDODQ[0];
		OQDQOQDOQO oqdqoqdoqo2 = (OQDQOQDOQO)this.OOQQCODOCD.OOODOQDODQ[1];
		this.OOQQCODOCD.ODDDDCCDCO(pos, ref num, ref num2, ref num3, ref num4, ref oqdqoqdoqo, ref oqdqoqdoqo2, ref vector);
		pos = vector;
		if (doInsert && num >= 0 && num2 >= 0)
		{
			if (this.OODCCOODCC.OOQDOOQQ && num2 == this.OOQQCODOCD.OOODOQDODQ.Count - 1)
			{
				this.OODDQODDCC(pos);
			}
			else
			{
				OQDQOQDOQO oqdqoqdoqo3 = (OQDQOQDOQO)this.OOQQCODOCD.OOODOQDODQ[num2];
				string name = oqdqoqdoqo3.object1.name;
				int num5 = num2 + 2;
				for (int i = num2; i < this.OOQQCODOCD.OOODOQDODQ.Count - 1; i++)
				{
					oqdqoqdoqo3 = (OQDQOQDOQO)this.OOQQCODOCD.OOODOQDODQ[i];
					string name2;
					if (num5 < 10)
					{
						name2 = "Marker000" + num5.ToString();
					}
					else if (num5 < 100)
					{
						name2 = "Marker00" + num5.ToString();
					}
					else
					{
						name2 = "Marker0" + num5.ToString();
					}
					oqdqoqdoqo3.object1.name = name2;
					num5++;
				}
				oqdqoqdoqo3 = (OQDQOQDOQO)this.OOQQCODOCD.OOODOQDODQ[num];
				Transform transform = (Transform)Object.Instantiate(oqdqoqdoqo3.object1.transform, pos, oqdqoqdoqo3.object1.rotation);
				transform.gameObject.name = name;
				transform.parent = this.OODDDCQCOC;
				MarkerScript component = transform.GetComponent<MarkerScript>();
				component.OOCCDCOQCQ = false;
				float num6 = num3 + num4;
				float num7 = num3 / num6;
				float num8 = oqdqoqdoqo.ri - oqdqoqdoqo2.ri;
				component.ri = oqdqoqdoqo.ri - num8 * num7;
				num8 = oqdqoqdoqo.li - oqdqoqdoqo2.li;
				component.li = oqdqoqdoqo.li - num8 * num7;
				num8 = oqdqoqdoqo.rt - oqdqoqdoqo2.rt;
				component.rt = oqdqoqdoqo.rt - num8 * num7;
				num8 = oqdqoqdoqo.lt - oqdqoqdoqo2.lt;
				component.lt = oqdqoqdoqo.lt - num8 * num7;
				num8 = oqdqoqdoqo.rs - oqdqoqdoqo2.rs;
				component.rs = oqdqoqdoqo.rs - num8 * num7;
				num8 = oqdqoqdoqo.ls - oqdqoqdoqo2.ls;
				component.ls = oqdqoqdoqo.ls - num8 * num7;
				this.OCOOCODDOC(this.OODCCOODCC.geoResolution, false, false);
				if (this.materialType == 0)
				{
					this.OOQQCODOCD.OOQOOCDQOD(this.materialType);
				}
				if (this.objectType == 2)
				{
					component.surface.gameObject.SetActive(false);
				}
			}
		}
		this.OOQODQOCOC();
	}

	// Token: 0x06004444 RID: 17476 RVA: 0x00109C50 File Offset: 0x00107E50
	public void ODCQOCDQOC()
	{
		Object.DestroyImmediate(this.OODCCOODCC.OCQOCOCQQO.gameObject);
		this.OCQOCOCQQO = null;
		this.OOQODQOCOC();
	}

	// Token: 0x06004445 RID: 17477 RVA: 0x00109C80 File Offset: 0x00107E80
	public void OQCQQDODDC()
	{
		if (this.OOQQCODOCD == null)
		{
			this.ODOCOQCCOC(base.transform, null, null, null);
		}
		OQCDQQDQCC.ODOQCCODQC = true;
		if (!this.ODODCOCCDQ)
		{
			this.geoResolution = 0.5f;
			this.ODODCOCCDQ = true;
			this.doTerrain = false;
			this.OOQODQOCOC();
			if (this.objectType < 2)
			{
				this.ODDOOODDCQ();
			}
			this.OOQQCODOCD.terrainRendered = true;
			this.OCQDCQDDCO();
		}
		if (this.displayRoad && this.objectType < 2)
		{
			Material material = (Material)Resources.Load("roadMaterial", typeof(Material));
			if (this.OOQQCODOCD.road.renderer != null)
			{
				this.OOQQCODOCD.road.renderer.material = material;
			}
			foreach (object obj in this.OOQQCODOCD.road.transform)
			{
				Transform transform = (Transform)obj;
				if (transform.gameObject.renderer != null)
				{
					transform.gameObject.renderer.material = material;
				}
			}
			this.OOQQCODOCD.road.transform.parent = null;
			this.OOQQCODOCD.road.layer = 0;
			this.OOQQCODOCD.road.name = base.gameObject.name;
		}
		else if (this.OOQQCODOCD.road != null)
		{
			Object.DestroyImmediate(this.OOQQCODOCD.road);
		}
	}

	// Token: 0x06004446 RID: 17478 RVA: 0x00109E58 File Offset: 0x00108058
	public void OQQOOCCQCO()
	{
		this.OOQQCODOCD.OOQDODCQOQ(12);
	}

	// Token: 0x06004447 RID: 17479 RVA: 0x00109E68 File Offset: 0x00108068
	public ArrayList ODCOQCODCC()
	{
		ArrayList arrayList = new ArrayList();
		foreach (object obj in this.obj)
		{
			Transform transform = (Transform)obj;
			if (transform.name == "Markers")
			{
				foreach (object obj2 in transform)
				{
					Transform transform2 = (Transform)obj2;
					MarkerScript component = transform2.GetComponent<MarkerScript>();
					arrayList.Add(component.ODDGDOOO);
					arrayList.Add(component.ODDQOODO);
					if (transform2.name == "Marker0003")
					{
					}
					arrayList.Add(component.ODDQOOO);
				}
			}
		}
		return arrayList;
	}

	// Token: 0x06004448 RID: 17480 RVA: 0x00109F90 File Offset: 0x00108190
	public void OQCOCQDQDD()
	{
		ArrayList arrayList = new ArrayList();
		ArrayList arrayList2 = new ArrayList();
		ArrayList arrayList3 = new ArrayList();
		for (int i = 0; i < RoadObjectScript.ODODOQQO.Length; i++)
		{
			if (this.ODODQQOD[i])
			{
				arrayList.Add(RoadObjectScript.ODODQOOQ[i]);
				arrayList3.Add(RoadObjectScript.ODODOQQO[i]);
				arrayList2.Add(i);
			}
		}
		this.ODODDQOO = (string[])arrayList.ToArray(typeof(string));
		this.OOQQQOQO = (int[])arrayList2.ToArray(typeof(int));
	}

	// Token: 0x06004449 RID: 17481 RVA: 0x0010A034 File Offset: 0x00108234
	public void OOOOOOODCD(ArrayList arr, string[] DOODQOQO, string[] OODDQOQO)
	{
		bool flag = false;
		RoadObjectScript.ODODOQQO = DOODQOQO;
		RoadObjectScript.ODODQOOQ = OODDQOQO;
		ArrayList arrayList = new ArrayList();
		if (this.obj == null)
		{
			this.ODOCOQCCOC(base.transform, null, null, null);
		}
		foreach (object obj in this.obj)
		{
			Transform transform = (Transform)obj;
			if (transform.name == "Markers")
			{
				foreach (object obj2 in transform)
				{
					Transform transform2 = (Transform)obj2;
					MarkerScript component = transform2.GetComponent<MarkerScript>();
					component.OQODQQDO.Clear();
					component.ODOQQQDO.Clear();
					component.OQQODQQOO.Clear();
					component.ODDOQQOO.Clear();
					arrayList.Add(component);
				}
			}
		}
		this.mSc = (MarkerScript[])arrayList.ToArray(typeof(MarkerScript));
		ArrayList arrayList2 = new ArrayList();
		int num = 0;
		int num2 = 0;
		if (this.ODQQQQQO != null)
		{
			if (arr.Count == 0)
			{
				return;
			}
			for (int i = 0; i < RoadObjectScript.ODODOQQO.Length; i++)
			{
				ODODDQQO ododdqqo = (ODODDQQO)arr[i];
				for (int j = 0; j < this.ODQQQQQO.Length; j++)
				{
					if (RoadObjectScript.ODODOQQO[i] == this.ODQQQQQO[j])
					{
						num++;
						if (this.ODODQQOD.Length > j)
						{
							arrayList2.Add(this.ODODQQOD[j]);
						}
						else
						{
							arrayList2.Add(false);
						}
						foreach (MarkerScript markerScript in this.mSc)
						{
							int num3 = -1;
							for (int l = 0; l < markerScript.ODDOOQDO.Length; l++)
							{
								if (ododdqqo.id == markerScript.ODDOOQDO[l])
								{
									num3 = l;
									break;
								}
							}
							if (num3 >= 0)
							{
								markerScript.OQODQQDO.Add(markerScript.ODDOOQDO[num3]);
								markerScript.ODOQQQDO.Add(markerScript.ODDGDOOO[num3]);
								markerScript.OQQODQQOO.Add(markerScript.ODDQOOO[num3]);
								if (ododdqqo.sidewaysDistanceUpdate == 0 || (ododdqqo.sidewaysDistanceUpdate == 2 && markerScript.ODDQOODO[num3] != ododdqqo.oldSidwaysDistance))
								{
									markerScript.ODDOQQOO.Add(markerScript.ODDQOODO[num3]);
								}
								else
								{
									markerScript.ODDOQQOO.Add(ododdqqo.splinePosition);
								}
							}
							else
							{
								markerScript.OQODQQDO.Add(ododdqqo.id);
								markerScript.ODOQQQDO.Add(ododdqqo.markerActive);
								markerScript.OQQODQQOO.Add(true);
								markerScript.ODDOQQOO.Add(ododdqqo.splinePosition);
							}
						}
					}
				}
				if (ododdqqo.sidewaysDistanceUpdate != 0)
				{
				}
				flag = false;
			}
		}
		for (int m = 0; m < RoadObjectScript.ODODOQQO.Length; m++)
		{
			ODODDQQO ododdqqo2 = (ODODDQQO)arr[m];
			bool flag2 = false;
			for (int n = 0; n < this.ODQQQQQO.Length; n++)
			{
				if (RoadObjectScript.ODODOQQO[m] == this.ODQQQQQO[n])
				{
					flag2 = true;
				}
			}
			if (!flag2)
			{
				num2++;
				arrayList2.Add(false);
				foreach (MarkerScript markerScript2 in this.mSc)
				{
					markerScript2.OQODQQDO.Add(ododdqqo2.id);
					markerScript2.ODOQQQDO.Add(ododdqqo2.markerActive);
					markerScript2.OQQODQQOO.Add(true);
					markerScript2.ODDOQQOO.Add(ododdqqo2.splinePosition);
				}
			}
		}
		this.ODODQQOD = (bool[])arrayList2.ToArray(typeof(bool));
		this.ODQQQQQO = new string[RoadObjectScript.ODODOQQO.Length];
		RoadObjectScript.ODODOQQO.CopyTo(this.ODQQQQQO, 0);
		ArrayList arrayList3 = new ArrayList();
		for (int num5 = 0; num5 < this.ODODQQOD.Length; num5++)
		{
			if (this.ODODQQOD[num5])
			{
				arrayList3.Add(num5);
			}
		}
		this.OOQQQOQO = (int[])arrayList3.ToArray(typeof(int));
		foreach (MarkerScript markerScript3 in this.mSc)
		{
			markerScript3.ODDOOQDO = (string[])markerScript3.OQODQQDO.ToArray(typeof(string));
			markerScript3.ODDGDOOO = (bool[])markerScript3.ODOQQQDO.ToArray(typeof(bool));
			markerScript3.ODDQOOO = (bool[])markerScript3.OQQODQQOO.ToArray(typeof(bool));
			markerScript3.ODDQOODO = (float[])markerScript3.ODDOQQOO.ToArray(typeof(float));
		}
		if (flag)
		{
		}
	}

	// Token: 0x0600444A RID: 17482 RVA: 0x0010A658 File Offset: 0x00108858
	public bool CheckWaterHeights()
	{
		bool result = true;
		float y = Terrain.activeTerrain.transform.position.y;
		foreach (object obj in this.obj)
		{
			Transform transform = (Transform)obj;
			if (transform.name == "Markers")
			{
				foreach (object obj2 in transform)
				{
					Transform transform2 = (Transform)obj2;
					if (transform2.position.y - y <= 0.1f)
					{
						result = false;
					}
				}
			}
		}
		return result;
	}

	// Token: 0x04002405 RID: 9221
	public static string version = string.Empty;

	// Token: 0x04002406 RID: 9222
	public int objectType;

	// Token: 0x04002407 RID: 9223
	public bool displayRoad = true;

	// Token: 0x04002408 RID: 9224
	public float roadWidth = 5f;

	// Token: 0x04002409 RID: 9225
	public float indent = 3f;

	// Token: 0x0400240A RID: 9226
	public float surrounding = 5f;

	// Token: 0x0400240B RID: 9227
	public float raise = 1f;

	// Token: 0x0400240C RID: 9228
	public float raiseMarkers = 0.5f;

	// Token: 0x0400240D RID: 9229
	public bool OOQDOOQQ;

	// Token: 0x0400240E RID: 9230
	public bool renderRoad = true;

	// Token: 0x0400240F RID: 9231
	public bool beveledRoad;

	// Token: 0x04002410 RID: 9232
	public bool applySplatmap;

	// Token: 0x04002411 RID: 9233
	public int splatmapLayer = 4;

	// Token: 0x04002412 RID: 9234
	public bool autoUpdate = true;

	// Token: 0x04002413 RID: 9235
	public float geoResolution = 5f;

	// Token: 0x04002414 RID: 9236
	public int roadResolution = 1;

	// Token: 0x04002415 RID: 9237
	public float tuw = 15f;

	// Token: 0x04002416 RID: 9238
	public int splatmapSmoothLevel;

	// Token: 0x04002417 RID: 9239
	public float opacity = 1f;

	// Token: 0x04002418 RID: 9240
	public int expand;

	// Token: 0x04002419 RID: 9241
	public int offsetX;

	// Token: 0x0400241A RID: 9242
	public int offsetY;

	// Token: 0x0400241B RID: 9243
	private Material surfaceMaterial;

	// Token: 0x0400241C RID: 9244
	public float surfaceOpacity = 1f;

	// Token: 0x0400241D RID: 9245
	public float smoothDistance = 1f;

	// Token: 0x0400241E RID: 9246
	public float smoothSurDistance = 3f;

	// Token: 0x0400241F RID: 9247
	private bool handleInsertFlag;

	// Token: 0x04002420 RID: 9248
	public bool handleVegetation = true;

	// Token: 0x04002421 RID: 9249
	public float OOCQDOOCQD = 2f;

	// Token: 0x04002422 RID: 9250
	public float ODDQCCDCDC = 1f;

	// Token: 0x04002423 RID: 9251
	public int materialType;

	// Token: 0x04002424 RID: 9252
	private string[] materialStrings;

	// Token: 0x04002425 RID: 9253
	private MarkerScript[] mSc;

	// Token: 0x04002426 RID: 9254
	private bool ODQDOQOCCD;

	// Token: 0x04002427 RID: 9255
	private bool[] OQCCDQCDDD;

	// Token: 0x04002428 RID: 9256
	private bool[] ODODODCODD;

	// Token: 0x04002429 RID: 9257
	public string[] OODQCQODQQ;

	// Token: 0x0400242A RID: 9258
	public string[] ODODQOQO;

	// Token: 0x0400242B RID: 9259
	public int[] ODODQOQOInt;

	// Token: 0x0400242C RID: 9260
	public int OQDCQDCDDD = -1;

	// Token: 0x0400242D RID: 9261
	public int ODOCDOOOQQ = -1;

	// Token: 0x0400242E RID: 9262
	public static GUISkin OODCDOQDCC;

	// Token: 0x0400242F RID: 9263
	public static GUISkin OODQQDDCDD;

	// Token: 0x04002430 RID: 9264
	public bool OQOCDODDQC;

	// Token: 0x04002431 RID: 9265
	private Vector3 cPos;

	// Token: 0x04002432 RID: 9266
	private Vector3 ePos;

	// Token: 0x04002433 RID: 9267
	public bool OOCCDCOQCQ;

	// Token: 0x04002434 RID: 9268
	public static Texture2D OQOOODODQD;

	// Token: 0x04002435 RID: 9269
	public int markers = 1;

	// Token: 0x04002436 RID: 9270
	public OQCDQQDQCC OOQQCODOCD;

	// Token: 0x04002437 RID: 9271
	private GameObject ODOQDQOO;

	// Token: 0x04002438 RID: 9272
	public bool ODODCOCCDQ;

	// Token: 0x04002439 RID: 9273
	public bool doTerrain;

	// Token: 0x0400243A RID: 9274
	private Transform OCQOCOCQQO;

	// Token: 0x0400243B RID: 9275
	public GameObject[] OCQOCOCQQOs;

	// Token: 0x0400243C RID: 9276
	private static string OCQCDDDOCC;

	// Token: 0x0400243D RID: 9277
	public Transform obj;

	// Token: 0x0400243E RID: 9278
	private string OOQCQCDDOQ;

	// Token: 0x0400243F RID: 9279
	public static string erInit = string.Empty;

	// Token: 0x04002440 RID: 9280
	public static Transform OCQQQOQOQC;

	// Token: 0x04002441 RID: 9281
	private RoadObjectScript OODCCOODCC;

	// Token: 0x04002442 RID: 9282
	public bool flyby;

	// Token: 0x04002443 RID: 9283
	private Vector3 pos;

	// Token: 0x04002444 RID: 9284
	private float fl;

	// Token: 0x04002445 RID: 9285
	private float oldfl;

	// Token: 0x04002446 RID: 9286
	private bool ODDCQQQQOO;

	// Token: 0x04002447 RID: 9287
	private bool ODOQCDDCOO;

	// Token: 0x04002448 RID: 9288
	private bool ODQQOQCQCO;

	// Token: 0x04002449 RID: 9289
	public Transform OODDDCQCOC;

	// Token: 0x0400244A RID: 9290
	public int OdQODQOD = 1;

	// Token: 0x0400244B RID: 9291
	public float OOQQQDOD;

	// Token: 0x0400244C RID: 9292
	public float OOQQQDODOffset;

	// Token: 0x0400244D RID: 9293
	public float OOQQQDODLength;

	// Token: 0x0400244E RID: 9294
	public bool ODODDDOO;

	// Token: 0x0400244F RID: 9295
	public static string[] ODOQDOQO;

	// Token: 0x04002450 RID: 9296
	public static string[] ODODOQQO;

	// Token: 0x04002451 RID: 9297
	public static string[] ODODQOOQ;

	// Token: 0x04002452 RID: 9298
	public int ODQDOOQO;

	// Token: 0x04002453 RID: 9299
	public string[] ODQQQQQO;

	// Token: 0x04002454 RID: 9300
	public string[] ODODDQOO;

	// Token: 0x04002455 RID: 9301
	public bool[] ODODQQOD;

	// Token: 0x04002456 RID: 9302
	public int[] OOQQQOQO;

	// Token: 0x04002457 RID: 9303
	public int ODOQOOQO;

	// Token: 0x04002458 RID: 9304
	public bool forceY;

	// Token: 0x04002459 RID: 9305
	public float yChange;

	// Token: 0x0400245A RID: 9306
	public float floorDepth = 2f;

	// Token: 0x0400245B RID: 9307
	public float waterLevel = 1.5f;

	// Token: 0x0400245C RID: 9308
	public bool lockWaterLevel = true;

	// Token: 0x0400245D RID: 9309
	public float lastY;

	// Token: 0x0400245E RID: 9310
	public string distance = "0";

	// Token: 0x0400245F RID: 9311
	public string markerDisplayStr = "Hide Markers";

	// Token: 0x04002460 RID: 9312
	public static string[] objectStrings;

	// Token: 0x04002461 RID: 9313
	public string objectText = "Road";

	// Token: 0x04002462 RID: 9314
	public bool applyAnimation;

	// Token: 0x04002463 RID: 9315
	public float waveSize = 1.5f;

	// Token: 0x04002464 RID: 9316
	public float waveHeight = 0.15f;

	// Token: 0x04002465 RID: 9317
	public bool snapY = true;

	// Token: 0x04002466 RID: 9318
	private TextAnchor origAnchor;

	// Token: 0x04002467 RID: 9319
	public bool autoODODDQQO;

	// Token: 0x04002468 RID: 9320
	public Texture2D roadTexture;

	// Token: 0x04002469 RID: 9321
	public Texture2D roadMaterial;

	// Token: 0x0400246A RID: 9322
	public string[] ODQOOCCQQO;

	// Token: 0x0400246B RID: 9323
	public string[] OOOOCOCCDC;

	// Token: 0x0400246C RID: 9324
	public int selectedWaterMaterial;

	// Token: 0x0400246D RID: 9325
	public int selectedWaterScript;

	// Token: 0x0400246E RID: 9326
	private bool doRestore;

	// Token: 0x0400246F RID: 9327
	public bool doFlyOver;

	// Token: 0x04002470 RID: 9328
	public static GameObject tracer;

	// Token: 0x04002471 RID: 9329
	public Camera goCam;

	// Token: 0x04002472 RID: 9330
	public float speed = 1f;

	// Token: 0x04002473 RID: 9331
	public float offset;

	// Token: 0x04002474 RID: 9332
	public bool camInit;

	// Token: 0x04002475 RID: 9333
	public GameObject customMesh;

	// Token: 0x04002476 RID: 9334
	public static bool disableFreeAlerts = true;

	// Token: 0x04002477 RID: 9335
	public bool multipleTerrains;

	// Token: 0x04002478 RID: 9336
	public bool editRestore = true;

	// Token: 0x04002479 RID: 9337
	public Material roadMaterialEdit;

	// Token: 0x0400247A RID: 9338
	public static int backupLocation;

	// Token: 0x0400247B RID: 9339
	public string[] backupStrings = new string[]
	{
		"Outside Assets folder path",
		"Inside Assets folder path"
	};

	// Token: 0x0400247C RID: 9340
	public Vector3[] leftVecs = new Vector3[0];

	// Token: 0x0400247D RID: 9341
	public Vector3[] rightVecs = new Vector3[0];
}
