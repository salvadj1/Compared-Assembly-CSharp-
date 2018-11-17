using System;
using System.Collections;
using System.Collections.Generic;
using EasyRoads3D;
using UnityEngine;

// Token: 0x0200082E RID: 2094
public class RoadObjectScript : MonoBehaviour
{
	// Token: 0x06004891 RID: 18577 RVA: 0x00111F30 File Offset: 0x00110130
	public void OCOQDDODDQ(ArrayList arr, string[] DOODQOQO, string[] OODDQOQO)
	{
		this.ODOCOQCCOC(base.transform, arr, DOODQOQO, OODDQOQO);
	}

	// Token: 0x06004892 RID: 18578 RVA: 0x00111F44 File Offset: 0x00110144
	public void OCCOCQQQDO(global::MarkerScript markerScript)
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

	// Token: 0x06004893 RID: 18579 RVA: 0x00112008 File Offset: 0x00110208
	public void OCOQDCQOCD(global::MarkerScript markerScript)
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

	// Token: 0x06004894 RID: 18580 RVA: 0x001120B0 File Offset: 0x001102B0
	public void ResetMaterials(global::MarkerScript markerScript)
	{
		if (this.OOQQCODOCD != null)
		{
			this.OOQQCODOCD.OODCDQDOCO(this.OCQOCOCQQO, this.OCQOCOCQQOs, markerScript.OCCCCODCOD, markerScript.OQCQOQQDCQ, this.OODDDCQCOC, ref markerScript.OCQOCOCQQOs, ref markerScript.trperc, this.OCQOCOCQQOs);
		}
	}

	// Token: 0x06004895 RID: 18581 RVA: 0x00112104 File Offset: 0x00110304
	public void OOOOQCDODD(global::MarkerScript markerScript)
	{
		if (markerScript.OQCQOQQDCQ != markerScript.ODOOQQOO)
		{
			this.OOQQCODOCD.OODCDQDOCO(this.OCQOCOCQQO, this.OCQOCOCQQOs, markerScript.OCCCCODCOD, markerScript.OQCQOQQDCQ, this.OODDDCQCOC, ref markerScript.OCQOCOCQQOs, ref markerScript.trperc, this.OCQOCOCQQOs);
			markerScript.ODOOQQOO = markerScript.OQCQOQQDCQ;
		}
		this.OCOOCODDOC(this.OODCCOODCC.geoResolution, false, false);
	}

	// Token: 0x06004896 RID: 18582 RVA: 0x0011217C File Offset: 0x0011037C
	private void OQDODCODOQ(string ctrl, global::MarkerScript markerScript)
	{
		int num = 0;
		foreach (Transform transform in markerScript.OCQOCOCQQOs)
		{
			global::MarkerScript component = transform.GetComponent<global::MarkerScript>();
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

	// Token: 0x06004897 RID: 18583 RVA: 0x00112308 File Offset: 0x00110508
	public void OQOCODCDOO()
	{
		if (this.markers > 1)
		{
			this.OCOOCODDOC(this.OODCCOODCC.geoResolution, false, false);
		}
	}

	// Token: 0x06004898 RID: 18584 RVA: 0x0011232C File Offset: 0x0011052C
	public void ODOCOQCCOC(Transform tr, ArrayList arr, string[] DOODQOQO, string[] OODDQOQO)
	{
		global::RoadObjectScript.version = "2.4.6";
		global::RoadObjectScript.OODCDOQDCC = (GUISkin)UnityEngine.Resources.Load("ER3DSkin", typeof(GUISkin));
		global::RoadObjectScript.OQOOODODQD = (Texture2D)UnityEngine.Resources.Load("ER3DLogo", typeof(Texture2D));
		if (global::RoadObjectScript.objectStrings == null)
		{
			global::RoadObjectScript.objectStrings = new string[3];
			global::RoadObjectScript.objectStrings[0] = "Road Object";
			global::RoadObjectScript.objectStrings[1] = "River Object";
			global::RoadObjectScript.objectStrings[2] = "Procedural Mesh Object";
		}
		this.obj = tr;
		this.OOQQCODOCD = new OQCDQQDQCC();
		this.OODCCOODCC = this.obj.GetComponent<global::RoadObjectScript>();
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
			if (!terrain.gameObject.GetComponent<global::EasyRoads3DTerrainID>())
			{
				global::EasyRoads3DTerrainID easyRoads3DTerrainID = (global::EasyRoads3DTerrainID)terrain.gameObject.AddComponent("EasyRoads3DTerrainID");
				string text = Random.Range(100000000, 999999999).ToString();
				easyRoads3DTerrainID.terrainid = text;
				terrains.id = text;
			}
			else
			{
				terrains.id = terrain.gameObject.GetComponent<global::EasyRoads3DTerrainID>().terrainid;
			}
			this.OOQQCODOCD.OCDQQCDOQO(terrains);
		}
		ODCDDDDQQD.OCDQQCDOQO();
		if (this.roadMaterialEdit == null)
		{
			this.roadMaterialEdit = (Material)UnityEngine.Resources.Load("materials/roadMaterialEdit", typeof(Material));
		}
		if (this.objectType == 0 && GameObject.Find(base.gameObject.name + "/road") == null)
		{
			GameObject gameObject = new GameObject("road");
			gameObject.transform.parent = base.transform;
		}
		this.OOQQCODOCD.OODQOQCDCQ(this.obj, global::RoadObjectScript.OCQCDDDOCC, this.OODCCOODCC.roadWidth, this.surfaceOpacity, ref this.OOCCDCOQCQ, ref this.indent, this.applyAnimation, this.waveSize, this.waveHeight);
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
		if (global::RoadObjectScript.backupLocation == 0)
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
		if (arr != null || global::RoadObjectScript.ODODQOOQ == null)
		{
			this.OOOOOOODCD(arr, DOODQOQO, OODDQOQO);
		}
		if (this.doRestore)
		{
			return;
		}
	}

	// Token: 0x06004899 RID: 18585 RVA: 0x00112774 File Offset: 0x00110974
	public void UpdateBackupFolder()
	{
	}

	// Token: 0x0600489A RID: 18586 RVA: 0x00112778 File Offset: 0x00110978
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

	// Token: 0x0600489B RID: 18587 RVA: 0x001127D4 File Offset: 0x001109D4
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
			gameObject = (GameObject)Object.Instantiate(UnityEngine.Resources.Load("marker", typeof(GameObject)));
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
		global::MarkerScript component = transform.GetComponent<global::MarkerScript>();
		component.OOCCDCOQCQ = false;
		component.objectScript = this.obj.GetComponent<global::RoadObjectScript>();
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

	// Token: 0x0600489C RID: 18588 RVA: 0x00112ADC File Offset: 0x00110CDC
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
					global::MarkerScript component = transform2.GetComponent<global::MarkerScript>();
					component.objectScript = this.obj.GetComponent<global::RoadObjectScript>();
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

	// Token: 0x0600489D RID: 18589 RVA: 0x00112E48 File Offset: 0x00111048
	public void StartCam()
	{
		this.OCOOCODDOC(0.5f, false, true);
	}

	// Token: 0x0600489E RID: 18590 RVA: 0x00112E58 File Offset: 0x00111058
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

	// Token: 0x0600489F RID: 18591 RVA: 0x00112FBC File Offset: 0x001111BC
	public void ODDOOODDCQ()
	{
		global::RoadObjectScript[] array = (global::RoadObjectScript[])Object.FindObjectsOfType(typeof(global::RoadObjectScript));
		ArrayList arrayList = new ArrayList();
		foreach (global::RoadObjectScript roadObjectScript in array)
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

	// Token: 0x060048A0 RID: 18592 RVA: 0x0011309C File Offset: 0x0011129C
	public ArrayList RebuildObjs()
	{
		global::RoadObjectScript[] array = (global::RoadObjectScript[])Object.FindObjectsOfType(typeof(global::RoadObjectScript));
		ArrayList arrayList = new ArrayList();
		foreach (global::RoadObjectScript roadObjectScript in array)
		{
			if (roadObjectScript.transform != base.transform)
			{
				arrayList.Add(roadObjectScript.transform);
			}
		}
		return arrayList;
	}

	// Token: 0x060048A1 RID: 18593 RVA: 0x00113108 File Offset: 0x00111308
	public void ODQDOOOCOC()
	{
		this.OCOOCODDOC(this.OODCCOODCC.geoResolution, false, false);
		if (this.OOQQCODOCD != null)
		{
			this.OOQQCODOCD.ODQDOOOCOC();
		}
		this.ODODDDOO = false;
	}

	// Token: 0x060048A2 RID: 18594 RVA: 0x00113148 File Offset: 0x00111348
	public void OCQDCQDDCO()
	{
		this.OOQQCODOCD.OCQDCQDDCO(this.OODCCOODCC.applySplatmap, this.OODCCOODCC.splatmapSmoothLevel, this.OODCCOODCC.renderRoad, this.OODCCOODCC.tuw, this.OODCCOODCC.roadResolution, this.OODCCOODCC.raise, this.OODCCOODCC.opacity, this.OODCCOODCC.expand, this.OODCCOODCC.offsetX, this.OODCCOODCC.offsetY, this.OODCCOODCC.beveledRoad, this.OODCCOODCC.splatmapLayer, this.OODCCOODCC.OdQODQOD, this.OOQQQDOD, this.OOQQQDODOffset, this.OOQQQDODLength);
	}

	// Token: 0x060048A3 RID: 18595 RVA: 0x00113204 File Offset: 0x00111404
	public void OQQDQCQQOC()
	{
		this.OOQQCODOCD.OQQDQCQQOC(this.OODCCOODCC.renderRoad, this.OODCCOODCC.tuw, this.OODCCOODCC.roadResolution, this.OODCCOODCC.raise, this.OODCCOODCC.beveledRoad, this.OODCCOODCC.OdQODQOD, this.OOQQQDOD, this.OOQQQDODOffset, this.OOQQQDODLength);
	}

	// Token: 0x060048A4 RID: 18596 RVA: 0x00113274 File Offset: 0x00111474
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
				global::MarkerScript component = transform.GetComponent<global::MarkerScript>();
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

	// Token: 0x060048A5 RID: 18597 RVA: 0x001135D0 File Offset: 0x001117D0
	public void ODCQOCDQOC()
	{
		Object.DestroyImmediate(this.OODCCOODCC.OCQOCOCQQO.gameObject);
		this.OCQOCOCQQO = null;
		this.OOQODQOCOC();
	}

	// Token: 0x060048A6 RID: 18598 RVA: 0x00113600 File Offset: 0x00111800
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
			Material material = (Material)UnityEngine.Resources.Load("roadMaterial", typeof(Material));
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

	// Token: 0x060048A7 RID: 18599 RVA: 0x001137D8 File Offset: 0x001119D8
	public void OQQOOCCQCO()
	{
		this.OOQQCODOCD.OOQDODCQOQ(12);
	}

	// Token: 0x060048A8 RID: 18600 RVA: 0x001137E8 File Offset: 0x001119E8
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
					global::MarkerScript component = transform2.GetComponent<global::MarkerScript>();
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

	// Token: 0x060048A9 RID: 18601 RVA: 0x00113910 File Offset: 0x00111B10
	public void OQCOCQDQDD()
	{
		ArrayList arrayList = new ArrayList();
		ArrayList arrayList2 = new ArrayList();
		ArrayList arrayList3 = new ArrayList();
		for (int i = 0; i < global::RoadObjectScript.ODODOQQO.Length; i++)
		{
			if (this.ODODQQOD[i])
			{
				arrayList.Add(global::RoadObjectScript.ODODQOOQ[i]);
				arrayList3.Add(global::RoadObjectScript.ODODOQQO[i]);
				arrayList2.Add(i);
			}
		}
		this.ODODDQOO = (string[])arrayList.ToArray(typeof(string));
		this.OOQQQOQO = (int[])arrayList2.ToArray(typeof(int));
	}

	// Token: 0x060048AA RID: 18602 RVA: 0x001139B4 File Offset: 0x00111BB4
	public void OOOOOOODCD(ArrayList arr, string[] DOODQOQO, string[] OODDQOQO)
	{
		bool flag = false;
		global::RoadObjectScript.ODODOQQO = DOODQOQO;
		global::RoadObjectScript.ODODQOOQ = OODDQOQO;
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
					global::MarkerScript component = transform2.GetComponent<global::MarkerScript>();
					component.OQODQQDO.Clear();
					component.ODOQQQDO.Clear();
					component.OQQODQQOO.Clear();
					component.ODDOQQOO.Clear();
					arrayList.Add(component);
				}
			}
		}
		this.mSc = (global::MarkerScript[])arrayList.ToArray(typeof(global::MarkerScript));
		ArrayList arrayList2 = new ArrayList();
		int num = 0;
		int num2 = 0;
		if (this.ODQQQQQO != null)
		{
			if (arr.Count == 0)
			{
				return;
			}
			for (int i = 0; i < global::RoadObjectScript.ODODOQQO.Length; i++)
			{
				ODODDQQO ododdqqo = (ODODDQQO)arr[i];
				for (int j = 0; j < this.ODQQQQQO.Length; j++)
				{
					if (global::RoadObjectScript.ODODOQQO[i] == this.ODQQQQQO[j])
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
						foreach (global::MarkerScript markerScript in this.mSc)
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
		for (int m = 0; m < global::RoadObjectScript.ODODOQQO.Length; m++)
		{
			ODODDQQO ododdqqo2 = (ODODDQQO)arr[m];
			bool flag2 = false;
			for (int n = 0; n < this.ODQQQQQO.Length; n++)
			{
				if (global::RoadObjectScript.ODODOQQO[m] == this.ODQQQQQO[n])
				{
					flag2 = true;
				}
			}
			if (!flag2)
			{
				num2++;
				arrayList2.Add(false);
				foreach (global::MarkerScript markerScript2 in this.mSc)
				{
					markerScript2.OQODQQDO.Add(ododdqqo2.id);
					markerScript2.ODOQQQDO.Add(ododdqqo2.markerActive);
					markerScript2.OQQODQQOO.Add(true);
					markerScript2.ODDOQQOO.Add(ododdqqo2.splinePosition);
				}
			}
		}
		this.ODODQQOD = (bool[])arrayList2.ToArray(typeof(bool));
		this.ODQQQQQO = new string[global::RoadObjectScript.ODODOQQO.Length];
		global::RoadObjectScript.ODODOQQO.CopyTo(this.ODQQQQQO, 0);
		ArrayList arrayList3 = new ArrayList();
		for (int num5 = 0; num5 < this.ODODQQOD.Length; num5++)
		{
			if (this.ODODQQOD[num5])
			{
				arrayList3.Add(num5);
			}
		}
		this.OOQQQOQO = (int[])arrayList3.ToArray(typeof(int));
		foreach (global::MarkerScript markerScript3 in this.mSc)
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

	// Token: 0x060048AB RID: 18603 RVA: 0x00113FD8 File Offset: 0x001121D8
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

	// Token: 0x0400263C RID: 9788
	public static string version = string.Empty;

	// Token: 0x0400263D RID: 9789
	public int objectType;

	// Token: 0x0400263E RID: 9790
	public bool displayRoad = true;

	// Token: 0x0400263F RID: 9791
	public float roadWidth = 5f;

	// Token: 0x04002640 RID: 9792
	public float indent = 3f;

	// Token: 0x04002641 RID: 9793
	public float surrounding = 5f;

	// Token: 0x04002642 RID: 9794
	public float raise = 1f;

	// Token: 0x04002643 RID: 9795
	public float raiseMarkers = 0.5f;

	// Token: 0x04002644 RID: 9796
	public bool OOQDOOQQ;

	// Token: 0x04002645 RID: 9797
	public bool renderRoad = true;

	// Token: 0x04002646 RID: 9798
	public bool beveledRoad;

	// Token: 0x04002647 RID: 9799
	public bool applySplatmap;

	// Token: 0x04002648 RID: 9800
	public int splatmapLayer = 4;

	// Token: 0x04002649 RID: 9801
	public bool autoUpdate = true;

	// Token: 0x0400264A RID: 9802
	public float geoResolution = 5f;

	// Token: 0x0400264B RID: 9803
	public int roadResolution = 1;

	// Token: 0x0400264C RID: 9804
	public float tuw = 15f;

	// Token: 0x0400264D RID: 9805
	public int splatmapSmoothLevel;

	// Token: 0x0400264E RID: 9806
	public float opacity = 1f;

	// Token: 0x0400264F RID: 9807
	public int expand;

	// Token: 0x04002650 RID: 9808
	public int offsetX;

	// Token: 0x04002651 RID: 9809
	public int offsetY;

	// Token: 0x04002652 RID: 9810
	private Material surfaceMaterial;

	// Token: 0x04002653 RID: 9811
	public float surfaceOpacity = 1f;

	// Token: 0x04002654 RID: 9812
	public float smoothDistance = 1f;

	// Token: 0x04002655 RID: 9813
	public float smoothSurDistance = 3f;

	// Token: 0x04002656 RID: 9814
	private bool handleInsertFlag;

	// Token: 0x04002657 RID: 9815
	public bool handleVegetation = true;

	// Token: 0x04002658 RID: 9816
	public float OOCQDOOCQD = 2f;

	// Token: 0x04002659 RID: 9817
	public float ODDQCCDCDC = 1f;

	// Token: 0x0400265A RID: 9818
	public int materialType;

	// Token: 0x0400265B RID: 9819
	private string[] materialStrings;

	// Token: 0x0400265C RID: 9820
	private global::MarkerScript[] mSc;

	// Token: 0x0400265D RID: 9821
	private bool ODQDOQOCCD;

	// Token: 0x0400265E RID: 9822
	private bool[] OQCCDQCDDD;

	// Token: 0x0400265F RID: 9823
	private bool[] ODODODCODD;

	// Token: 0x04002660 RID: 9824
	public string[] OODQCQODQQ;

	// Token: 0x04002661 RID: 9825
	public string[] ODODQOQO;

	// Token: 0x04002662 RID: 9826
	public int[] ODODQOQOInt;

	// Token: 0x04002663 RID: 9827
	public int OQDCQDCDDD = -1;

	// Token: 0x04002664 RID: 9828
	public int ODOCDOOOQQ = -1;

	// Token: 0x04002665 RID: 9829
	public static GUISkin OODCDOQDCC;

	// Token: 0x04002666 RID: 9830
	public static GUISkin OODQQDDCDD;

	// Token: 0x04002667 RID: 9831
	public bool OQOCDODDQC;

	// Token: 0x04002668 RID: 9832
	private Vector3 cPos;

	// Token: 0x04002669 RID: 9833
	private Vector3 ePos;

	// Token: 0x0400266A RID: 9834
	public bool OOCCDCOQCQ;

	// Token: 0x0400266B RID: 9835
	public static Texture2D OQOOODODQD;

	// Token: 0x0400266C RID: 9836
	public int markers = 1;

	// Token: 0x0400266D RID: 9837
	public OQCDQQDQCC OOQQCODOCD;

	// Token: 0x0400266E RID: 9838
	private GameObject ODOQDQOO;

	// Token: 0x0400266F RID: 9839
	public bool ODODCOCCDQ;

	// Token: 0x04002670 RID: 9840
	public bool doTerrain;

	// Token: 0x04002671 RID: 9841
	private Transform OCQOCOCQQO;

	// Token: 0x04002672 RID: 9842
	public GameObject[] OCQOCOCQQOs;

	// Token: 0x04002673 RID: 9843
	private static string OCQCDDDOCC;

	// Token: 0x04002674 RID: 9844
	public Transform obj;

	// Token: 0x04002675 RID: 9845
	private string OOQCQCDDOQ;

	// Token: 0x04002676 RID: 9846
	public static string erInit = string.Empty;

	// Token: 0x04002677 RID: 9847
	public static Transform OCQQQOQOQC;

	// Token: 0x04002678 RID: 9848
	private global::RoadObjectScript OODCCOODCC;

	// Token: 0x04002679 RID: 9849
	public bool flyby;

	// Token: 0x0400267A RID: 9850
	private Vector3 pos;

	// Token: 0x0400267B RID: 9851
	private float fl;

	// Token: 0x0400267C RID: 9852
	private float oldfl;

	// Token: 0x0400267D RID: 9853
	private bool ODDCQQQQOO;

	// Token: 0x0400267E RID: 9854
	private bool ODOQCDDCOO;

	// Token: 0x0400267F RID: 9855
	private bool ODQQOQCQCO;

	// Token: 0x04002680 RID: 9856
	public Transform OODDDCQCOC;

	// Token: 0x04002681 RID: 9857
	public int OdQODQOD = 1;

	// Token: 0x04002682 RID: 9858
	public float OOQQQDOD;

	// Token: 0x04002683 RID: 9859
	public float OOQQQDODOffset;

	// Token: 0x04002684 RID: 9860
	public float OOQQQDODLength;

	// Token: 0x04002685 RID: 9861
	public bool ODODDDOO;

	// Token: 0x04002686 RID: 9862
	public static string[] ODOQDOQO;

	// Token: 0x04002687 RID: 9863
	public static string[] ODODOQQO;

	// Token: 0x04002688 RID: 9864
	public static string[] ODODQOOQ;

	// Token: 0x04002689 RID: 9865
	public int ODQDOOQO;

	// Token: 0x0400268A RID: 9866
	public string[] ODQQQQQO;

	// Token: 0x0400268B RID: 9867
	public string[] ODODDQOO;

	// Token: 0x0400268C RID: 9868
	public bool[] ODODQQOD;

	// Token: 0x0400268D RID: 9869
	public int[] OOQQQOQO;

	// Token: 0x0400268E RID: 9870
	public int ODOQOOQO;

	// Token: 0x0400268F RID: 9871
	public bool forceY;

	// Token: 0x04002690 RID: 9872
	public float yChange;

	// Token: 0x04002691 RID: 9873
	public float floorDepth = 2f;

	// Token: 0x04002692 RID: 9874
	public float waterLevel = 1.5f;

	// Token: 0x04002693 RID: 9875
	public bool lockWaterLevel = true;

	// Token: 0x04002694 RID: 9876
	public float lastY;

	// Token: 0x04002695 RID: 9877
	public string distance = "0";

	// Token: 0x04002696 RID: 9878
	public string markerDisplayStr = "Hide Markers";

	// Token: 0x04002697 RID: 9879
	public static string[] objectStrings;

	// Token: 0x04002698 RID: 9880
	public string objectText = "Road";

	// Token: 0x04002699 RID: 9881
	public bool applyAnimation;

	// Token: 0x0400269A RID: 9882
	public float waveSize = 1.5f;

	// Token: 0x0400269B RID: 9883
	public float waveHeight = 0.15f;

	// Token: 0x0400269C RID: 9884
	public bool snapY = true;

	// Token: 0x0400269D RID: 9885
	private TextAnchor origAnchor;

	// Token: 0x0400269E RID: 9886
	public bool autoODODDQQO;

	// Token: 0x0400269F RID: 9887
	public Texture2D roadTexture;

	// Token: 0x040026A0 RID: 9888
	public Texture2D roadMaterial;

	// Token: 0x040026A1 RID: 9889
	public string[] ODQOOCCQQO;

	// Token: 0x040026A2 RID: 9890
	public string[] OOOOCOCCDC;

	// Token: 0x040026A3 RID: 9891
	public int selectedWaterMaterial;

	// Token: 0x040026A4 RID: 9892
	public int selectedWaterScript;

	// Token: 0x040026A5 RID: 9893
	private bool doRestore;

	// Token: 0x040026A6 RID: 9894
	public bool doFlyOver;

	// Token: 0x040026A7 RID: 9895
	public static GameObject tracer;

	// Token: 0x040026A8 RID: 9896
	public Camera goCam;

	// Token: 0x040026A9 RID: 9897
	public float speed = 1f;

	// Token: 0x040026AA RID: 9898
	public float offset;

	// Token: 0x040026AB RID: 9899
	public bool camInit;

	// Token: 0x040026AC RID: 9900
	public GameObject customMesh;

	// Token: 0x040026AD RID: 9901
	public static bool disableFreeAlerts = true;

	// Token: 0x040026AE RID: 9902
	public bool multipleTerrains;

	// Token: 0x040026AF RID: 9903
	public bool editRestore = true;

	// Token: 0x040026B0 RID: 9904
	public Material roadMaterialEdit;

	// Token: 0x040026B1 RID: 9905
	public static int backupLocation;

	// Token: 0x040026B2 RID: 9906
	public string[] backupStrings = new string[]
	{
		"Outside Assets folder path",
		"Inside Assets folder path"
	};

	// Token: 0x040026B3 RID: 9907
	public Vector3[] leftVecs = new Vector3[0];

	// Token: 0x040026B4 RID: 9908
	public Vector3[] rightVecs = new Vector3[0];
}
