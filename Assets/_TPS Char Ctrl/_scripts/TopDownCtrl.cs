using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class TopDownCtrl : MonoBehaviour
{
    Camera _cam;
    Animator anim;

    [System.Serializable]
    public class InputSettings
    {
        public string HORZ = "Horizontal";
        public string VERT = "Vertical";
        public string SPRINT = "Sprint";

        public string RELOAD = "Reload";
        public string FIRE = "Fire1";
        public string DROP = "DropWeapon";
        public string SWITCHWEAPON = "SwitchWeapon";
    }
    [SerializeField] public InputSettings INPUT;

    void Start()
    {
        _cam = Camera.main;
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        MovementLogic();
        FacingMouse();
    }

    private void MovementLogic()
    {
        float H = CrossPlatformInputManager.GetAxis(INPUT.HORZ);
        float V = CrossPlatformInputManager.GetAxis(INPUT.VERT);
        bool sprint = CrossPlatformInputManager.GetButton(INPUT.SPRINT);

        //Vector3 camForward = Vector3.Scale(_cam.transform.forward, new Vector3(1, 0, 1)).normalized;

        Vector3 moveDir = V * _cam.transform.forward + H * _cam.transform.right;
        //if (!sprint) moveDir *= 0.5f;
        GetComponent<TPS_Ctrl>().Move(moveDir, false, false, false);

        //anim.SetFloat("forward", H, 0.1f, Time.deltaTime);
        //anim.SetFloat("turn", V, 0.1f, Time.deltaTime);
    }

    void FacingMouse()
    {
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        Plane xPlane = new Plane(Vector3.up, Vector3.zero);
        float enter;

        if(xPlane.Raycast(ray, out enter))
        {
            Vector3 point = ray.GetPoint(enter);

            transform.LookAt(point);
        }
    }
}
