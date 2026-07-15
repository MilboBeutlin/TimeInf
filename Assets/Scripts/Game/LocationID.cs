using UnityEngine;

//the location of all Rooms, Corridors and Gänge in the game. 
// The first character of the location is the location type, the second character is the index of the location.
public enum LocationID
{
    None,
    //rooms:
    R0, R1, R2, R3, R4, R5, R6, R7, R8, R9, R10, R11,
    //vertical hallway:
    K0, K1, K2,
    //horizontal hallway:
    G0, G1
}
