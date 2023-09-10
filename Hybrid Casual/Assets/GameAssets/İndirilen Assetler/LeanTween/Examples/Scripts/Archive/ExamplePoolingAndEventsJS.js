#pragma strict

public enum ShellEvent{
	MoveLights, ChangeLightColor, Length
}

class ExamplePoolingAndEventsJS extends MonoBehaviour{
	public var usePooling:boolean = true;
	public var prefaRoadPiece:GameObject;

	private var lastRoadPieceZ:float;
	private var roadPool:LTPool;
	private var changeZ:float;
	private var speed:float;
	private var speedIter:int;
	private var i:int;

	function Awake(){
		LeanTween.EVENTS_MAX = ShellEvent.Length;
		LeanTween.LISTENERS_MAX = 60;

		roadPool = new LTPool( prefaRoadPiece, 30 );
	}

	function Start () {
		for(i=0; i < 8; i++){ // setup initial roads
			addMoreRoad();
		}

		shiftSpeed();

	}

	function shiftSpeed(){
		speed = (speedIter%7+1)*30;

		if(speedIter%7==0){
			LeanTween.dispatchEvent(ShellEvent.MoveLights, Color.white); // any listener to event will respond
		}else if(speedIter%7==4){
			var newColor:Color = new Color(Random.Range(0.0,1.0),Random.Range(0.0,1.0),Random.Range(0.0,1.0));
			LeanTween.dispatchEvent(ShellEvent.ChangeLightColor, newColor); 
		}

		speedIter++;
		LeanTween.delayedCall( gameObject, 2, shiftSpeed);
	}

	function Update(){
		var diff:float = Time.deltaTime * speed;
		transform.position.z += diff;

		changeZ += diff;

		if(changeZ>40){
			changeZ = changeZ - 40;
			addMoreRoad();
		}

		if(Input.touchCount>0 && Input.touches[0].phase == TouchPhase.Began){
			usePooling = !usePooling;
		}
	}

	function OnGUI(){
		GUI.Label(new Rect(Screen.width*0.7,0,Screen.width*0.3,Screen.height*0.3),"usePooling:"+usePooling);
	}

	function addMoreRoad(){
		var go:GameObject = usePooling ? roadPool.retrieve( new Vector3(0,0,lastRoadPieceZ), Quaternion.identity) : GameObject.Instantiate( prefaRoadPiece, new Vector3(0,0,lastRoadPieceZ), prefaRoadPiece.transform.rotation);
		lastRoadPieceZ += 40;

		LeanTween.delayedCall( gameObject, 6, destroyRoad, {"onCompleteParam":{"go":go}});
	}

	function destroyRoad( hash:Hashtable ){
		var go:GameObject = hash["go"] as GameObject;
		if(usePooling){
			roadPool.destroy( go );
		} else{
			DestroyImmediate(go);
		}
	}

}

