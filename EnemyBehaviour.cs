using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class NewBehaviourScript : MonoBehaviour
{
   private enum State {
       Roaming,
       ChaseTarget,
       ShootingTarget,
       GoingBackHome
   } 

   private IAimShootAnims aimShootAnims;
   private Vector3 startingPosition;
   private Vector3 roamPosition;
   private EnemyPathfindingMovement pathfindingMovement;
   private float nextShootTime;
   private State state;

   private void Awake(){
       pathfindingMovement = getComponent<EnemyPathfindingMovement>();
       aimShootAnims = getComponent<IAimShootAnims>();
       state = State.Roaming;
   }

   private void Start() {
       startingPosition = transform.position;
       roamPosition = GetRoamingPosition();
   }

   private void Update(){
      switch (state){
      default:
      case State.Roaming:
         pathfindingMovement.MoveTo(roamPosition);
      
        float reachedPositionDistance = 1f;
        if (Vector3.Distance(transfom.position, roamPosition) < reachedPositionDistance){
          roamPosition = GetRandomPosition();
        } 
        FindTarget();
        break;
      case State.ChaseTarget:
        pathfindingMovement.MoveToTimer(Player.Instance.GetPosition());
        
        aimShootAnims.SetAimTarget(Player.Instance.GetPosition());
        float attackRange = 10f;
        if (Vector3.Distance(transfom.position, Player.Instance.GetPosition()) < attackRange){
            if (Time.time > nextShootTime){
                pathfindingMovement.StopMoving();
                state = State.ShootingTarget;
                aimShootAnims.ShootTarget(Player.Instance.GetPosition(), () => {
                    state = State.ChaseTarget;
                });
                float fireRate = .03f;
                nextShootTime = Time.time + fireRate;
            }
            
        }

        float stopChaseingDistance = 80f;
        if (Vector3.Distance(transfom.position, Player.Instance.GetPosition()) > stopChaseingDistance){
            state = State.GoingBackHome;
        }
        break;
    case State.ShootingTarget:
        break;
    case State.GoingBackHome:
        pathfindingMovement.MoveToTimer(startingPosition);

        reachedPositionDistance = 10f;
        if (Vector3.Distance(transfom.position, startingPosition) < reachedPositionDistance){
            state = State.Roaming;
        }
        break; 
    }
}   
   private void FindTarget(){
       float targetRange = 50f;
       if (Vector3.Distance(transfom.position, Player.Instance.GetPosition()) < targetRange){
           state = State.ChaseTarget;
       }
   }

   private Vector3 GetRoamingPosition() {
       return startingPosition + UtilsClass.GetRandomDir() * Random.Range(10f, 70f);


   } 
}
