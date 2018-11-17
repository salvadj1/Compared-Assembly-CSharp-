using System;
using System.Collections;
using EasyRoads3D;
using UnityEngine;

// Token: 0x0200074B RID: 1867
[ExecuteInEditMode]
public class MarkerScript : MonoBehaviour
{
	// Token: 0x06004423 RID: 17443 RVA: 0x00107FBC File Offset: 0x001061BC
	private void Start()
	{
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			this.surface = transform;
		}
	}

	// Token: 0x06004424 RID: 17444 RVA: 0x0010802C File Offset: 0x0010622C
	private void OnDrawGizmos()
	{
		if (this.objectScript != null)
		{
			if (!this.objectScript.ODODCOCCDQ)
			{
				Vector3 vector = base.transform.position - this.oldPos;
				if (this.OCCCCODCOD && this.oldPos != Vector3.zero && vector != Vector3.zero)
				{
					int num = 0;
					foreach (Transform transform in this.OCQOCOCQQOs)
					{
						transform.position += vector * this.trperc[num];
						num++;
					}
				}
				if (this.oldPos != Vector3.zero && vector != Vector3.zero)
				{
					this.changed = true;
					if (this.objectScript.ODODCOCCDQ)
					{
						this.objectScript.OOQQCODOCD.specialRoadMaterial = true;
					}
				}
				this.oldPos = base.transform.position;
			}
			else if (this.objectScript.ODODDDOO)
			{
				base.transform.position = this.oldPos;
			}
		}
	}

	// Token: 0x06004425 RID: 17445 RVA: 0x00108170 File Offset: 0x00106370
	private void SetObjectScript()
	{
		this.objectScript = base.transform.parent.parent.GetComponent<RoadObjectScript>();
		if (this.objectScript.OOQQCODOCD == null)
		{
			ArrayList arrayList = ODODDCCOQO.OCDCQOOODO(false);
			this.objectScript.OCOQDDODDQ(arrayList, ODODDCCOQO.OOQOOQODQQ(arrayList), ODODDCCOQO.OQQDOODOOQ(arrayList));
		}
	}

	// Token: 0x06004426 RID: 17446 RVA: 0x001081C8 File Offset: 0x001063C8
	public void LeftIndent(float change, float perc)
	{
		this.ri += change * perc;
		if (this.ri < this.objectScript.indent)
		{
			this.ri = this.objectScript.indent;
		}
		this.OOQOQQOO = this.ri;
	}

	// Token: 0x06004427 RID: 17447 RVA: 0x00108218 File Offset: 0x00106418
	public void RightIndent(float change, float perc)
	{
		this.li += change * perc;
		if (this.li < this.objectScript.indent)
		{
			this.li = this.objectScript.indent;
		}
		this.ODODQQOO = this.li;
	}

	// Token: 0x06004428 RID: 17448 RVA: 0x00108268 File Offset: 0x00106468
	public void LeftSurrounding(float change, float perc)
	{
		this.rs += change * perc;
		if (this.rs < this.objectScript.indent)
		{
			this.rs = this.objectScript.indent;
		}
		this.ODOQQOOO = this.rs;
	}

	// Token: 0x06004429 RID: 17449 RVA: 0x001082B8 File Offset: 0x001064B8
	public void RightSurrounding(float change, float perc)
	{
		this.ls += change * perc;
		if (this.ls < this.objectScript.indent)
		{
			this.ls = this.objectScript.indent;
		}
		this.DODOQQOO = this.ls;
	}

	// Token: 0x0600442A RID: 17450 RVA: 0x00108308 File Offset: 0x00106508
	public void LeftTilting(float change, float perc)
	{
		this.rt += change * perc;
		if (this.rt < 0f)
		{
			this.rt = 0f;
		}
		this.ODDQODOO = this.rt;
	}

	// Token: 0x0600442B RID: 17451 RVA: 0x00108344 File Offset: 0x00106544
	public void RightTilting(float change, float perc)
	{
		this.lt += change * perc;
		if (this.lt < 0f)
		{
			this.lt = 0f;
		}
		this.ODDOQOQQ = this.lt;
	}

	// Token: 0x0600442C RID: 17452 RVA: 0x00108380 File Offset: 0x00106580
	public void FloorDepth(float change, float perc)
	{
		this.floorDepth += change * perc;
		if (this.floorDepth > 0f)
		{
			this.floorDepth = 0f;
		}
		this.oldFloorDepth = this.floorDepth;
	}

	// Token: 0x0600442D RID: 17453 RVA: 0x001083BC File Offset: 0x001065BC
	public bool InSelected()
	{
		for (int i = 0; i < this.objectScript.OCQOCOCQQOs.Length; i++)
		{
			if (this.objectScript.OCQOCOCQQOs[i] == base.gameObject)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x040023CD RID: 9165
	public float tension = 0.5f;

	// Token: 0x040023CE RID: 9166
	public float ri;

	// Token: 0x040023CF RID: 9167
	public float OOQOQQOO;

	// Token: 0x040023D0 RID: 9168
	public float li;

	// Token: 0x040023D1 RID: 9169
	public float ODODQQOO;

	// Token: 0x040023D2 RID: 9170
	public float rs;

	// Token: 0x040023D3 RID: 9171
	public float ODOQQOOO;

	// Token: 0x040023D4 RID: 9172
	public float ls;

	// Token: 0x040023D5 RID: 9173
	public float DODOQQOO;

	// Token: 0x040023D6 RID: 9174
	public float rt;

	// Token: 0x040023D7 RID: 9175
	public float ODDQODOO;

	// Token: 0x040023D8 RID: 9176
	public float lt;

	// Token: 0x040023D9 RID: 9177
	public float ODDOQOQQ;

	// Token: 0x040023DA RID: 9178
	public bool OCCCCODCOD;

	// Token: 0x040023DB RID: 9179
	public bool ODQDOQOO;

	// Token: 0x040023DC RID: 9180
	public float OQCQOQQDCQ;

	// Token: 0x040023DD RID: 9181
	public float ODOOQQOO;

	// Token: 0x040023DE RID: 9182
	public Transform[] OCQOCOCQQOs;

	// Token: 0x040023DF RID: 9183
	public float[] trperc;

	// Token: 0x040023E0 RID: 9184
	public Vector3 oldPos = Vector3.zero;

	// Token: 0x040023E1 RID: 9185
	public bool autoUpdate;

	// Token: 0x040023E2 RID: 9186
	public bool changed;

	// Token: 0x040023E3 RID: 9187
	public Transform surface;

	// Token: 0x040023E4 RID: 9188
	public bool OOCCDCOQCQ;

	// Token: 0x040023E5 RID: 9189
	private Vector3 position;

	// Token: 0x040023E6 RID: 9190
	private bool updated;

	// Token: 0x040023E7 RID: 9191
	private int frameCount;

	// Token: 0x040023E8 RID: 9192
	private float currentstamp;

	// Token: 0x040023E9 RID: 9193
	private float newstamp;

	// Token: 0x040023EA RID: 9194
	private bool mousedown;

	// Token: 0x040023EB RID: 9195
	private Vector3 lookAtPoint;

	// Token: 0x040023EC RID: 9196
	public bool bridgeObject;

	// Token: 0x040023ED RID: 9197
	public bool distHeights;

	// Token: 0x040023EE RID: 9198
	public RoadObjectScript objectScript;

	// Token: 0x040023EF RID: 9199
	public ArrayList OQODQQDO = new ArrayList();

	// Token: 0x040023F0 RID: 9200
	public ArrayList ODOQQQDO = new ArrayList();

	// Token: 0x040023F1 RID: 9201
	public ArrayList OQQODQQOO = new ArrayList();

	// Token: 0x040023F2 RID: 9202
	public ArrayList ODDOQQOO = new ArrayList();

	// Token: 0x040023F3 RID: 9203
	public ArrayList ODDDDQOO = new ArrayList();

	// Token: 0x040023F4 RID: 9204
	public ArrayList DQQOQQOO = new ArrayList();

	// Token: 0x040023F5 RID: 9205
	public string[] ODDOOQDO;

	// Token: 0x040023F6 RID: 9206
	public bool[] ODDGDOOO;

	// Token: 0x040023F7 RID: 9207
	public bool[] ODDQOOO;

	// Token: 0x040023F8 RID: 9208
	public float[] ODDQOODO;

	// Token: 0x040023F9 RID: 9209
	public float[] ODOQODOO;

	// Token: 0x040023FA RID: 9210
	public float[] ODDOQDO;

	// Token: 0x040023FB RID: 9211
	public int markerNum;

	// Token: 0x040023FC RID: 9212
	public string distance = "0";

	// Token: 0x040023FD RID: 9213
	public string OQOQODQCQC = "0";

	// Token: 0x040023FE RID: 9214
	public string OODDQCQQDD = "0";

	// Token: 0x040023FF RID: 9215
	public bool newSegment;

	// Token: 0x04002400 RID: 9216
	public float floorDepth = 2f;

	// Token: 0x04002401 RID: 9217
	public float oldFloorDepth = 2f;

	// Token: 0x04002402 RID: 9218
	public float waterLevel = 0.5f;

	// Token: 0x04002403 RID: 9219
	public bool lockWaterLevel = true;

	// Token: 0x04002404 RID: 9220
	public bool sharpCorner;
}
