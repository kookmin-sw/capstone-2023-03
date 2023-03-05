using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditorInternal.ReorderableList;

public class TiltWindow : MonoBehaviour
{
	public Vector2 range = new Vector2(5f, 3f);
    
	DefaultInputActions inputActions;	
    Transform mTrans;
	Quaternion mStart;
	Vector2 mRot = Vector2.zero;

	void Start ()
	{
		inputActions = new DefaultInputActions();
		mTrans = transform;
		mStart = mTrans.localRotation;
	}

	void Update ()
	{
		Vector2 pos = inputActions.UI.Point.ReadValue<Vector2>();
		Debug.Log(pos);

		float halfWidth = Screen.width * 0.5f;
		float halfHeight = Screen.height * 0.5f;
		float x = Mathf.Clamp((pos.x - halfWidth) / halfWidth, -1f, 1f);
		float y = Mathf.Clamp((pos.y - halfHeight) / halfHeight, -1f, 1f);
		mRot = Vector2.Lerp(mRot, new Vector2(x, y), Time.deltaTime * 5f);

		mTrans.localRotation = mStart * Quaternion.Euler(-mRot.y * range.y, mRot.x * range.x, 0f);
	}
}
