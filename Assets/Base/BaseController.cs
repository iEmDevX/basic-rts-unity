using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    [SerializeField] GameObject characterGameObj;
    private bool isPlayer;

    public GameObject SetCharacter(GameObject model)
    {
        if (model != null)
        {
            model.transform.parent = transform;

            var position = Vector3.zero;
            position.z -= 0.15f;
            model.transform.localPosition = position;
        }

        characterGameObj = model;
        return characterGameObj;
    }
}
