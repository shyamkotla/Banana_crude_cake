using UnityEngine;
using UnityEngine.SceneManagement;
using static PlayerInput;


public class Projection : MonoBehaviour
{
    private Scene projectionScene;
    private PhysicsScene2D physicsScene;
    [SerializeField] Transform obstaclesParent;
    [SerializeField] Transform playerPhyPrefab;
    [SerializeField] PlayerInput playerInput;
    public LineRenderer lr;
    [SerializeField] int maxFrameIterations = 100;

    Transform ghostPlayer;
    Rigidbody2D ghostPlayerRB;
        
    void Start()
    {
        CreatPhysicsScene();
    }
    void CreatPhysicsScene()
    {
        projectionScene = SceneManager.CreateScene("Projection", new CreateSceneParameters(LocalPhysicsMode.Physics2D));
        physicsScene = projectionScene.GetPhysicsScene2D();
            

        foreach(Transform child in obstaclesParent)
        {
            var ghostObj = Instantiate(child.gameObject, child.position, child.rotation);
            ghostObj.GetComponent<SpriteRenderer>().enabled = false;
            SceneManager.MoveGameObjectToScene(ghostObj, projectionScene);
        }

        SetupPlayerinPhysicsScene();
    }
    private void SetupPlayerinPhysicsScene()
    {
        ghostPlayer = Instantiate(playerPhyPrefab, playerPhyPrefab.transform.position, Quaternion.identity);
        ghostPlayerRB = ghostPlayer.GetComponent<Rigidbody2D>();

        SceneManager.MoveGameObjectToScene(ghostPlayer.gameObject, projectionScene);
        ghostPlayer.gameObject.SetActive(false);
    }

    public void SimulateTrajectory(Vector2 pos,Vector2 direction, Vector2 rbVelocity, float force)
    {
        ghostPlayer.gameObject.SetActive(true);

        ghostPlayer.transform.position = pos;
            
        ghostPlayerRB.velocity = rbVelocity;
        ghostPlayerRB.AddForce(direction * force,ForceMode2D.Impulse);

        //if (ghostPlayerRB.velocity.y <= 0.1f )
        //{
        //    ghostPlayerRB.velocity += Vector2.up * Physics2D.gravity.y * (2.5f - 1f) * Time.fixedDeltaTime;
        //}

        //Physics2D.simulationMode = SimulationMode2D.Script;
        lr.positionCount = maxFrameIterations;
        for(int i=0;i<maxFrameIterations;i++)
        {
            physicsScene.Simulate(0.02f); //0.02f is the fixed Delta Time , which is affected during slomo effect , so hardcoded it 
            lr.SetPosition(i, ghostPlayer.transform.position);
        }

        ghostPlayer.gameObject.SetActive(false);
    }
    private void FixedUpdate()
    {
        //if (ghostPlayerRB.velocity.y <= 0.1f && playerInput.playerState == PlayerState.AIMING)
        //{
        //    ghostPlayerRB.velocity += Vector2.up * Physics2D.gravity.y * (2.5f - 1f) * Time.fixedDeltaTime;
        //}
    }
    public void ToggelLineRenderer(bool value)
    {
        lr.enabled = value;
    }
}
