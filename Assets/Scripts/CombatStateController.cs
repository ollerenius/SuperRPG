using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStateController : MonoBehaviour {

    public enum CombatState {
        NEUTRAL, UNDER_ATTACK, FIGHTING
    }

    public CombatState combatState;

	void Start () {
        combatState = CombatState.NEUTRAL;
	}
	
	void Update () {
		
	}
}
