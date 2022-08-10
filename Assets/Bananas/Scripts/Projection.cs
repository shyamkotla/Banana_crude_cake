using UnityEngine;
using UnityEngine.SceneManagement;


    public class Projection : MonoBehaviour
    {

        private Scene projectionScene;
        private PhysicsScene2D physicsScene;
        #region UnityMethods
        [SerializeField] Transform obstaclesParent;
        [SerializeField] Transform playerPhyPrefab;
        public LineRenderer lr;
        [SerializeField] int maxFrameIterations = 100;
        
        
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
        }

        public void SimulateTrajectory(Vector2 pos,Vector2 direction,float force)
        {
            var ghostPlayer = Instantiate(playerPhyPrefab, pos, Quaternion.identity);
            ghostPlayer.GetComponent<SpriteRenderer>().enabled = false;
            
            SceneManager.MoveGameObjectToScene(ghostPlayer.gameObject, projectionScene);
            
            ghostPlayer.GetComponent<Rigidbody2D>().AddForce(direction * force,ForceMode2D.Impulse);
            
            //Physics2D.simulationMode = SimulationMode2D.Script;
            lr.positionCount = maxFrameIterations;
            for(int i=0;i<maxFrameIterations;i++)
            {
                physicsScene.Simulate(Time.fixedDeltaTime);
                lr.SetPosition(i, ghostPlayer.transform.position);
            }

            Destroy(ghostPlayer.gameObject);
        }
        void Update()
        {

        }
        #endregion

       
    }
