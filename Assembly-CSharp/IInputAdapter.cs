using System;
using UnityEngine;

// Token: 0x020006D7 RID: 1751
public interface IInputAdapter
{
	// Token: 0x06003DDD RID: 15837
	bool GetKeyDown(KeyCode key);

	// Token: 0x06003DDE RID: 15838
	bool GetKeyUp(KeyCode key);

	// Token: 0x06003DDF RID: 15839
	float GetAxis(string axisName);

	// Token: 0x06003DE0 RID: 15840
	Vector2 GetMousePosition();

	// Token: 0x06003DE1 RID: 15841
	bool GetMouseButton(int button);

	// Token: 0x06003DE2 RID: 15842
	bool GetMouseButtonDown(int button);

	// Token: 0x06003DE3 RID: 15843
	bool GetMouseButtonUp(int button);
}
