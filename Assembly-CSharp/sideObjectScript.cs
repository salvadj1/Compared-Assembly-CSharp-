using System;
using UnityEngine;

// Token: 0x02000830 RID: 2096
public class sideObjectScript : MonoBehaviour
{
	// Token: 0x040026B5 RID: 9909
	public global::RoadObjectScript OODCCOODCC;

	// Token: 0x040026B6 RID: 9910
	public int soIndex;

	// Token: 0x040026B7 RID: 9911
	public string soName;

	// Token: 0x040026B8 RID: 9912
	public int soAlign;

	// Token: 0x040026B9 RID: 9913
	public float soUVx = 0.1f;

	// Token: 0x040026BA RID: 9914
	public float soUVy = 1f;

	// Token: 0x040026BB RID: 9915
	public float m_distance = 10f;

	// Token: 0x040026BC RID: 9916
	public int objectType;

	// Token: 0x040026BD RID: 9917
	public int position;

	// Token: 0x040026BE RID: 9918
	public Material mat;

	// Token: 0x040026BF RID: 9919
	public bool weld = true;

	// Token: 0x040026C0 RID: 9920
	public bool combine = true;

	// Token: 0x040026C1 RID: 9921
	public bool OQCCQQDDOC = true;

	// Token: 0x040026C2 RID: 9922
	public string m_go = string.Empty;

	// Token: 0x040026C3 RID: 9923
	public string ODDDOCCOQO = string.Empty;

	// Token: 0x040026C4 RID: 9924
	public string ODOQDQQCCQ = string.Empty;

	// Token: 0x040026C5 RID: 9925
	public GameObject goStart;

	// Token: 0x040026C6 RID: 9926
	public GameObject goEnd;

	// Token: 0x040026C7 RID: 9927
	public GameObject goInstantiated;

	// Token: 0x040026C8 RID: 9928
	public int selectedRotation;

	// Token: 0x040026C9 RID: 9929
	public static string[] rotationOptions;

	// Token: 0x040026CA RID: 9930
	public static string[] uvStrings;

	// Token: 0x040026CB RID: 9931
	public int uvInt;

	// Token: 0x040026CC RID: 9932
	public bool randomObjects;

	// Token: 0x040026CD RID: 9933
	public int childOrder;

	// Token: 0x040026CE RID: 9934
	public string[] childOrderStrings;

	// Token: 0x040026CF RID: 9935
	public float density = 1f;

	// Token: 0x040026D0 RID: 9936
	public float sidewaysOffset;

	// Token: 0x040026D1 RID: 9937
	public int terrainTree;

	// Token: 0x040026D2 RID: 9938
	public string[] rotationStrings;

	// Token: 0x040026D3 RID: 9939
	public int selectedYRotation;

	// Token: 0x040026D4 RID: 9940
	public int childCount;

	// Token: 0x040026D5 RID: 9941
	public float xPosition;

	// Token: 0x040026D6 RID: 9942
	public float yPosition;

	// Token: 0x040026D7 RID: 9943
	public float uvYRound;

	// Token: 0x040026D8 RID: 9944
	public bool m_collider;
}
