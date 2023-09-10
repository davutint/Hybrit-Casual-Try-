#pragma strict

public var myLight:GameObject;
public var lightComponent:Light;

private static var nameIter:int;
private var plane:GameObject;

function Awake () {
	gameObject.name = "road"+nameIter;
	// Debug.Log("road start"+gameObject.name);
	nameIter++;

	if(plane==null){
		// lightComponent = myLight.GetComponent(Light);
		// lightComponent.color = Color.white;

		LeanTween.addListener(gameObject, ShellEvent.MoveLights, moveLights);
		LeanTween.addListener(gameObject, ShellEvent.ChangeLightColor, changeLightColor);

		Debug.Log("creating plane");
		var dimensions:Vector2 = Vector2(10, 10);
		plane = createPlane(dimensions, Vector2(480,40));
		plane.transform.eulerAngles.x = 90;
		plane.transform.parent = transform;
		plane.transform.localPosition = Vector3(-0.5,0,-0.5);
		sineCurveMesh(plane, dimensions);
	}
}

function sineCurveMesh( go:GameObject, dimensions:Vector2 ){
	var mesh : Mesh = go.GetComponent(MeshFilter).mesh;
	var vertices : Vector3[] = mesh.vertices;
	
	var h:float = 4;
	var spaceX:float = Mathf.PI / 1.25;
	var spaceY:float = Mathf.PI / 5.0;
	var j:int = 0;
	for(var i:int=0; i < vertices.Length; i+=4){
		var x:int = j%dimensions.y;
		var y:int = j/dimensions.y;
		// Debug.Log("j:"+j+" sx:"+sx+" sy:"+sy+" x:"+x+" y:"+y);
		vertices[i+0].z = Mathf.Sin( y*spaceY ) * h + Mathf.Sin( x*spaceX ) * h;
		vertices[i+1].z = Mathf.Sin( y*spaceY ) * h + Mathf.Sin( (x+1)*spaceX ) * h;
		vertices[i+2].z = Mathf.Sin( (y+1)*spaceY ) * h + Mathf.Sin( x*spaceX ) * h;
		vertices[i+3].z = Mathf.Sin( (y+1)*spaceY ) * h + Mathf.Sin( (x+1)*spaceX ) * h;

		j++;
	}
	mesh.vertices = vertices;
	mesh.RecalculateNormals();
	mesh.RecalculateBounds();
	mesh.Optimize();
	var meshCollider:MeshCollider = go.GetComponent(MeshCollider);
	meshCollider.sharedMesh = mesh;
}

function createPlane(dimensions:Vector2, size:Vector2):GameObject{
	var go:GameObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
	var mf: MeshFilter = go.GetComponent(MeshFilter);
	var mesh = new Mesh();
	mf.mesh = mesh;
	mf.hideFlags = HideFlags.HideAndDontSave;
	
	var vertices: Vector3[] = new Vector3[ dimensions.x*dimensions.y*4 ];
	var normals: Vector3[] = new Vector3[ vertices.Length ];
	var uv: Vector2[] = new Vector2[ vertices.Length ];
	var tri: int[] = new int[dimensions.x*dimensions.y*6];

	var k:int = 0;
	var j:int = 0;
	var x:int = 0;
	var y:int = 0;
	for(var i:int=0; i < vertices.Length; i+=4){
		var sx:float = size.x / dimensions.x;
		var sy:float = size.y / dimensions.y;
		x = j%dimensions.y;
		y = j/dimensions.y;
		// Debug.Log("j:"+j+" sx:"+sx+" sy:"+sy+" x:"+x+" y:"+y);
		vertices[i+0] = new Vector3(sx*x, sy*y, 0);
		vertices[i+1] = new Vector3(sx*x+sx, sy*y, 0);
		vertices[i+2] = new Vector3(sx*x, sy*y+sy, 0);
		vertices[i+3] = new Vector3(sx*x+sx, sy*y+sy, 0);
		uv[i+0] = new Vector2(sx*x, sy*y);
		uv[i+1] = new Vector2(sx*x+sx, sy*y);
		uv[i+2] = new Vector2(sx*x, sy*y+sy);
		uv[i+3] = new Vector2(sx*x+sx, sy*y+sy);
		tri[k+0] = i+0;
		tri[k+1] = i+2;
		tri[k+2] = i+1;

		tri[k+3] = i+2;
		tri[k+4] = i+3;
		tri[k+5] = i+1;

		k+=6;
		j++;
	}
	mesh.vertices = vertices;
	mesh.normals = normals;
	mesh.uv = uv;
	mesh.triangles = tri;

	return go;
}

function OnDestroy(){
	LeanTween.removeListener(gameObject, ShellEvent.MoveLights, moveLights);
	LeanTween.removeListener(gameObject, ShellEvent.ChangeLightColor, changeLightColor);
}

function moveLights( e:LTEvent ){
	// LeanTween.rotateAround( myLight, Vector3.left, 360, 2.0, {"ease":LeanTweenType.easeOutBounce});
}

function changeLightColor( e:LTEvent ){
	// var newColor:Color = e.data;
	// lightComponent.color = newColor;
}
