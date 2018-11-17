using System;
using System.Collections;
using EasyRoads3D;
using UnityEngine;

// Token: 0x0200082D RID: 2093
[ExecuteInEditMode]
public class MarkerScript : MonoBehaviour
{
	// Token: 0x06004884 RID: 18564 RVA: 0x0011193C File Offset: 0x0010FB3C
	private void Start()
	{
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			this.surface = transform;
		}
	}

	// Token: 0x06004885 RID: 18565 RVA: 0x001119AC File Offset: 0x0010FBAC
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

	// Token: 0x06004886 RID: 18566 RVA: 0x00111AF0 File Offset: 0x0010FCF0
	private void SetObjectScript()
	{
		this.objectScript = base.transform.parent.parent.GetComponent<global::RoadObjectScript>();
		if (this.objectScript.OOQQCODOCD == null)
		{
			ArrayList arrayList = ODODDCCOQO.OCDCQOOODO(false);
			this.objectScript.OCOQDDODDQ(arrayList, ODODDCCOQO.OOQOOQODQQ(arrayList), ODODDCCOQO.OQQDOODOOQ(arrayList));
		}
	}

	// Token: 0x06004887 RID: 18567 RVA: 0x00111B48 File Offset: 0x0010FD48
	public void LeftIndent(float change, float perc)
	{
		this.ri += change * perc;
		if (this.ri < this.objectScript.indent)
		{
			this.ri = this.objectScript.indent;
		}
		this.OOQOQQOO = this.ri;
	}

	// Token: 0x06004888 RID: 18568 RVA: 0x00111B98 File Offset: 0x0010FD98
	public void RightIndent(float change, float perc)
	{
		this.li += change * perc;
		if (this.li < this.objectScript.indent)
		{
			this.li = this.objectScript.indent;
		}
		this.ODODQQOO = this.li;
	}

	// Token: 0x06004889 RID: 18569 RVA: 0x00111BE8 File Offset: 0x0010FDE8
	public void LeftSurrounding(float change, float perc)
	{
		this.rs += change * perc;
		if (this.rs < this.objectScript.indent)
		{
			this.rs = this.objectScript.indent;
		}
		this.ODOQQOOO = this.rs;
	}

	// Token: 0x0600488A RID: 18570 RVA: 0x00111C38 File Offset: 0x0010FE38
	public void RightSurrounding(float change, float perc)
	{
		this.ls += change * perc;
		if (this.ls < this.objectScript.indent)
		{
			this.ls = this.objectScript.indent;
		}
		this.DODOQQOO = this.ls;
	}

	// Token: 0x0600488B RID: 18571 RVA: 0x00111C88 File Offset: 0x0010FE88
	public void LeftTilting(float change, float perc)
	{
		this.rt += change * perc;
		if (this.rt < 0f)
		{
			this.rt = 0f;
		}
		this.ODDQODOO = this.rt;
	}

	// Token: 0x0600488C RID: 18572 RVA: 0x00111CC4 File Offset: 0x0010FEC4
	public void RightTilting(float change, float perc)
	{
		this.lt += change * perc;
		if (this.lt < 0f)
		{
			this.lt = 0f;
		}
		this.ODDOQOQQ = this.lt;
	}

	// Token: 0x0600488D RID: 18573 RVA: 0x00111D00 File Offset: 0x0010FF00
	public void FloorDepth(float change, float perc)
	{
		this.floorDepth += change * perc;
		if (this.floorDepth > 0f)
		{
			this.floorDepth = 0f;
		}
		this.oldFloorDepth = this.floorDepth;
	}

	// Token: 0x0600488E RID: 18574 RVA: 0x00111D3C File Offset: 0x0010FF3C
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

	// Token: 0x04002604 RID: 9732
	public float tension = 0.5f;

	// Token: 0x04002605 RID: 9733
	public float ri;

	// Token: 0x04002606 RID: 9734
	public float OOQOQQOO;

	// Token: 0x04002607 RID: 9735
	public float li;

	// Token: 0x04002608 RID: 9736
	public float ODODQQOO;

	// Token: 0x04002609 RID: 9737
	public float rs;

	// Token: 0x0400260A RID: 9738
	public float ODOQQOOO;

	// Token: 0x0400260B RID: 9739
	public float ls;

	// Token: 0x0400260C RID: 9740
	public float DODOQQOO;

	// Token: 0x0400260D RID: 9741
	public float rt;

	// Token: 0x0400260E RID: 9742
	public float ODDQODOO;

	// Token: 0x0400260F RID: 9743
	public float lt;

	// Token: 0x04002610 RID: 9744
	public float ODDOQOQQ;

	// Token: 0x04002611 RID: 9745
	public bool OCCCCODCOD;

	// Token: 0x04002612 RID: 9746
	public bool ODQDOQOO;

	// Token: 0x04002613 RID: 9747
	public float OQCQOQQDCQ;

	// Token: 0x04002614 RID: 9748
	public float ODOOQQOO;

	// Token: 0x04002615 RID: 9749
	public Transform[] OCQOCOCQQOs;

	// Token: 0x04002616 RID: 9750
	public float[] trperc;

	// Token: 0x04002617 RID: 9751
	public Vector3 oldPos = Vector3.zero;

	// Token: 0x04002618 RID: 9752
	public bool autoUpdate;

	// Token: 0x04002619 RID: 9753
	public bool changed;

	// Token: 0x0400261A RID: 9754
	public Transform surface;

	// Token: 0x0400261B RID: 9755
	public bool OOCCDCOQCQ;

	// Token: 0x0400261C RID: 9756
	private Vector3 position;

	// Token: 0x0400261D RID: 9757
	private bool updated;

	// Token: 0x0400261E RID: 9758
	private int frameCount;

	// Token: 0x0400261F RID: 9759
	private float currentstamp;

	// Token: 0x04002620 RID: 9760
	private float newstamp;

	// Token: 0x04002621 RID: 9761
	private bool mousedown;

	// Token: 0x04002622 RID: 9762
	private Vector3 lookAtPoint;

	// Token: 0x04002623 RID: 9763
	public bool bridgeObject;

	// Token: 0x04002624 RID: 9764
	public bool distHeights;

	// Token: 0x04002625 RID: 9765
	public global::RoadObjectScript objectScript;

	// Token: 0x04002626 RID: 9766
	public ArrayList OQODQQDO = new ArrayList();

	// Token: 0x04002627 RID: 9767
	public ArrayList ODOQQQDO = new ArrayList();

	// Token: 0x04002628 RID: 9768
	public ArrayList OQQODQQOO = new ArrayList();

	// Token: 0x04002629 RID: 9769
	public ArrayList ODDOQQOO = new ArrayList();

	// Token: 0x0400262A RID: 9770
	public ArrayList ODDDDQOO = new ArrayList();

	// Token: 0x0400262B RID: 9771
	public ArrayList DQQOQQOO = new ArrayList();

	// Token: 0x0400262C RID: 9772
	public string[] ODDOOQDO;

	// Token: 0x0400262D RID: 9773
	public bool[] ODDGDOOO;

	// Token: 0x0400262E RID: 9774
	public bool[] ODDQOOO;

	// Token: 0x0400262F RID: 9775
	public float[] ODDQOODO;

	// Token: 0x04002630 RID: 9776
	public float[] ODOQODOO;

	// Token: 0x04002631 RID: 9777
	public float[] ODDOQDO;

	// Token: 0x04002632 RID: 9778
	public int markerNum;

	// Token: 0x04002633 RID: 9779
	public string distance = "0";

	// Token: 0x04002634 RID: 9780
	public string OQOQODQCQC = "0";

	// Token: 0x04002635 RID: 9781
	public string OODDQCQQDD = "0";

	// Token: 0x04002636 RID: 9782
	public bool newSegment;

	// Token: 0x04002637 RID: 9783
	public float floorDepth = 2f;

	// Token: 0x04002638 RID: 9784
	public float oldFloorDepth = 2f;

	// Token: 0x04002639 RID: 9785
	public float waterLevel = 0.5f;

	// Token: 0x0400263A RID: 9786
	public bool lockWaterLevel = true;

	// Token: 0x0400263B RID: 9787
	public bool sharpCorner;
}
