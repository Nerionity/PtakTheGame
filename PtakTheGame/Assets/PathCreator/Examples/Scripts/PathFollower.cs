using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour
    {
        public playerMOve player;
        public Animator playerAnim;
        public PathCreator pathCreator;
        public PathCreator pathCreator2;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 5;
        float distanceTravelled;
        Quaternion savedRotation;

        private Coroutine backToFlyCoroutine;


        IEnumerator ZmienNaFlyPoCzasie(float time)
        {
            yield return new WaitForSeconds(time);
        
           GlobalVariables.Instance.playerState = "Fly";
        }


        void Start() {
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
            }

        }


        
        void Update()
        {
            if (pathCreator != null && GlobalVariables.Instance.playerState == "FirstWind"){
                if(backToFlyCoroutine != null){
                    StopCoroutine(backToFlyCoroutine);
                    backToFlyCoroutine = null;
                }
                distanceTravelled=pathCreator.path.GetClosestDistanceAlongPath(gameObject.transform.position);
                GlobalVariables.Instance.playerState = "Wind";
            }   
            if (pathCreator != null && GlobalVariables.Instance.playerState == "Wind" )
            {
                playerAnim.Play("Base Layer.Armature|wir_wejście", -1,float.NegativeInfinity);
                distanceTravelled += speed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                if(pathCreator.path.GetClosestTimeOnPath(pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction)) == 1){
                    //player.FirstFly(pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction));
                        GlobalVariables.Instance.playerState = "FirstFly";
                        distanceTravelled = 0;d
                        savedRotation = transform.rotation;
                        transform.DORotate(new Vector3(savedRotation.x, savedRotation.y, 0), 1);
                }
            }
            if (pathCreator2 != null &&  GlobalVariables.Instance.playerState == "FirstFly")
            {
                playerAnim.Play("Base Layer.Armature|wir_wyjście", -1,float.NegativeInfinity);
                distanceTravelled += speed * Time.deltaTime;
                transform.position = pathCreator2.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
               // transform.rotation = Quaternion.Lerp( Quaternion.Euler(new Vector3(savedRotation.x,savedRotation.y,savedRotation.z)), Quaternion.Euler(new Vector3(savedRotation.x,savedRotation.y,savedRotation.z)), distanceTravelled);
               
                
                transform.rotation = savedRotation;
                if(pathCreator2.path.GetClosestTimeOnPath(pathCreator2.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction)) == 1){
                    //player.FirstFly(pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction));
                         GlobalVariables.Instance.playerState = "Fly";
                         playerAnim.Play("Base Layer.Amature|lot", -1,float.NegativeInfinity);
                         player.move.x = transform.rotation.x;
                         player.move.y = transform.rotation.y;

                        //backToFlyCoroutine = StartCoroutine(ZmienNaFlyPoCzasie(2));
                }
            }
            
        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged() {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }
    }
}