using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using UnitySampleAssets.CrossPlatformInput;

//　, IPointerUpHandler , IPointerDownHandler
public class Joystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler/*, ICanvasRaycastFilter*/ {

    public int MovementRange = 100;

    public enum AxisOption
    {                                                    // Options for which axes to use                                                     
        Both,                                                                   // Use both
        OnlyHorizontal,                                                         // Only horizontal
        OnlyVertical                                                            // Only vertical
    }

    public AxisOption axesToUse = AxisOption.Both;   // The options for the axes that the still will use
    public string horizontalAxisName = "Horizontal";// The name given to the horizontal axis for the cross platform input
    public string verticalAxisName = "Vertical";    // The name given to the vertical axis for the cross platform input 

    private Vector3 startPos;
    private bool useX;                                                          // Toggle for using the x axis
    private bool useY;                                                          // Toggle for using the Y axis
    private CrossPlatformInputManager.VirtualAxis horizontalVirtualAxis;               // Reference to the joystick in the cross platform input
    private CrossPlatformInputManager.VirtualAxis verticalVirtualAxis;                 // Reference to the joystick in the cross platform input
    
    // ドラッグフラグ
    bool isDragging = false;
    
    CanvasGroup canvasGroup;
    
    void Start () {
        startPos = transform.position;
        CreateVirtualAxes ();
        //
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void UpdateVirtualAxes (Vector3 value) {

        var delta = startPos - value;
        delta.y = -delta.y;
        delta /= MovementRange;
        
        if(useX) horizontalVirtualAxis.Update (-delta.x);
        if(useY) verticalVirtualAxis.Update (delta.y);

    }

    private void CreateVirtualAxes()
    {
        // set axes to use
        useX = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyHorizontal);
        useY = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyVertical);

        // create new axes based on axes to use
        if (useX) horizontalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(horizontalAxisName);
        if (useY) verticalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(verticalAxisName);
    }
    
    public void OnBeginDrag( PointerEventData data ) {
        isDragging = true;
    }


    public  void OnDrag(PointerEventData data) {
        
        if ( Mathf.Abs( data.position.x - startPos.x ) > MovementRange || Mathf.Abs( data.position.y - startPos.y ) > MovementRange ) return;
		
        Vector3 newPos = Vector3.zero;

        if (useX) {
            int delta = (int) (data.position.x - startPos.x);
            delta = Mathf.Clamp(delta,  - MovementRange,  MovementRange);
            newPos.x = delta;
        }

        if (useY) {
            int delta = (int)(data.position.y - startPos.y);
            delta = Mathf.Clamp(delta, -MovementRange,  MovementRange);
            newPos.y = delta;
        }
        transform.position = new Vector3(startPos.x + newPos.x , startPos.y + newPos.y , startPos.z + newPos.z);
        UpdateVirtualAxes (transform.position);
        
        //
        canvasGroup.blocksRaycasts = false;
    }


    public  void OnEndDrag (PointerEventData data)
    {
        transform.position = startPos;
        UpdateVirtualAxes (startPos);
        //
        isDragging = false;
        
        //
        canvasGroup.blocksRaycasts = true;
        
    }
    
    ///<summary>
    ///
    ///</summary>
    //  public bool IsRaycastLocationValid( Vector2 sp, Camera eventCamera ) {
    //      //  return Vector2.Distance(sp, transform.position) < 20.0f;
    //      Debug.Log( "Raycast" );
    //      return true;
    //  }


    //  public  void OnPointerDown (PointerEventData data) {}

    void OnDisable () {
        // remove the joysticks from the cross platform input
        if (useX) {
            horizontalVirtualAxis.Remove();
        }
        if (useY)
        {
            verticalVirtualAxis.Remove();
        }
    }
}
