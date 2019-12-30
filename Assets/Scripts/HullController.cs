using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System;


namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(GameManager))]
    public class HullController : MonoBehaviour
    {
        //variables of the WeRun Team

        int lives;
        string goal;

        Identity blue;
        Identity two;
        Identity there;

        Identity currentIdentity;
        //public GameManager.IdentityState startIdentity;
        KeyCode[] currentInput = { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.Space, KeyCode.E };

        int numberOfJumps = 0;
        int maxNumberOfJumps = 1;

        bool manualSwitchEnabled = false;
        bool cancelJump = false;

        GUIStyle guiStyle = new GUIStyle();
        float[] speeds = new float[10];
        int speedsindex = 0;
       
       

        [Serializable]
        public class MovementSettings
        {

            //WeRun Variables
            [HideInInspector] public bool walkingEnabled = true;

            public float ForwardSpeed = 8.0f;   // Speed when walking forward
            public float BackwardSpeed = 4.0f;  // Speed when walking backwards
            public float StrafeSpeed = 4.0f;    // Speed when walking sideways
            public float RunMultiplier = 2;   // Speed when walking
            public KeyCode RunKey = KeyCode.LeftShift;
            public float JumpForce = 30f;
            public AnimationCurve SlopeCurveModifier = new AnimationCurve(new Keyframe(-90.0f, 1.0f), new Keyframe(0.0f, 1.0f), new Keyframe(90.0f, 0.0f));
            [HideInInspector] public float CurrentTargetSpeed = 8f;



#if !MOBILE_INPUT
            private bool m_Running;
#endif

            public void UpdateDesiredTargetSpeed(Vector2 input)
            {
                if (input == Vector2.zero) return;
                if (input.x > 0 || input.x < 0)
                {
                    //strafe
                    CurrentTargetSpeed = StrafeSpeed;
                }
                if (input.y < 0)
                {
                    //backwards
                    CurrentTargetSpeed = BackwardSpeed;
                }
                if (input.y > 0)
                {
                    //forwards
                    //handled last as if strafing and moving forward at the same time forwards speed should take precedence
                    CurrentTargetSpeed = ForwardSpeed;
                }
#if !MOBILE_INPUT
                if (Input.GetKey(RunKey))
                {
                    CurrentTargetSpeed *= RunMultiplier;
                    m_Running = true;
                }
                else
                {
                    m_Running = false;
                }
#endif
            }

#if !MOBILE_INPUT
            public bool Running
            {
                get { return m_Running; }
            }
#endif
        }


        [Serializable]
        public class AdvancedSettings
        {
            public float groundCheckDistance = 0.01f; // distance for checking if the controller is grounded ( 0.01f seems to work best for this )
            public float stickToGroundHelperDistance = 0.5f; // stops the character
            [HideInInspector]
            public float slowDownRate = 20f; // rate at which the controller comes to a stop when there is no input
            [HideInInspector]
            public float slowDownRateAir = 5f;
            public bool airControl; // can the user control the direction that is being moved in the air
            [Tooltip("set it to 0.1 or more if you get stuck in wall")]
            public float shellOffset; //reduce the radius by that ratio to avoid getting stuck in wall (a value of 0.1f is nice)
        }


        public Camera cam;
        public MovementSettings movementSettings = new MovementSettings();
        public MouseLook mouseLook = new MouseLook();
        public AdvancedSettings advancedSettings = new AdvancedSettings();


        private Rigidbody m_RigidBody;
        private CapsuleCollider m_Capsule;
        private float m_YRotation;
        private Vector3 m_GroundContactNormal;
        private bool m_Jump, m_PreviouslyGrounded, m_Jumping, m_IsGrounded;


        public Vector3 Velocity
        {
            get { return m_RigidBody.velocity; }
        }

        public bool Grounded
        {
            get { return m_IsGrounded; }
        }

        public bool Jumping
        {
            get { return m_Jumping; }
        }

        public bool Running
        {
            get
            {
#if !MOBILE_INPUT
                return movementSettings.Running;
#else
	            return false;
#endif
            }
        }

        private void Awake()
        {
            GameManager.OnTransitionEnter += TransitionEnter; // events are registered to event Manager
            GameManager.OnTransitionExit += TransitionExit;



        }


        private void Start()
        {
            guiStyle.fontSize = 30;

            m_RigidBody = GetComponent<Rigidbody>();
            m_Capsule = GetComponent<CapsuleCollider>();
            mouseLook.Init(transform, cam.transform);
        }


        private void Update()
        {
            RotateView();

            if (Input.GetKeyDown(currentInput[4]) && !m_Jump)
            {
                m_Jump = true;
                cancelJump = false;
            }

            if (Input.GetKeyUp(currentInput[4]) && m_Jumping)
            {
                cancelJump = true;
            }


            if (manualSwitchEnabled)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    GetComponent<GameManager>().SendMessage("TriggerIdentity", 0, SendMessageOptions.RequireReceiver);
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    GetComponent<GameManager>().SendMessage("TriggerIdentity", 1, SendMessageOptions.RequireReceiver);
                }
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    GetComponent<GameManager>().SendMessage("TriggerIdentity", 2, SendMessageOptions.RequireReceiver);
                }
            }

        }


        private void FixedUpdate()
        {
            GroundCheck();
            Vector2 input = GetInput();

            if ((Mathf.Abs(input.x) > float.Epsilon || Mathf.Abs(input.y) > float.Epsilon) && (advancedSettings.airControl || m_IsGrounded))
            {
                // always move along the camera forward as it is the direction that it being aimed at
                Vector3 desiredMove = cam.transform.forward * input.y + cam.transform.right * input.x;
                desiredMove = Vector3.ProjectOnPlane(desiredMove, m_GroundContactNormal).normalized;

                desiredMove.x = desiredMove.x * movementSettings.CurrentTargetSpeed ;
                desiredMove.z = desiredMove.z * movementSettings.CurrentTargetSpeed ;
                desiredMove.y = desiredMove.y * movementSettings.CurrentTargetSpeed ;
                if (m_RigidBody.velocity.sqrMagnitude <
                    (movementSettings.CurrentTargetSpeed  * movementSettings.CurrentTargetSpeed))
                {
                    m_RigidBody.AddForce(desiredMove * SlopeMultiplier(), ForceMode.Impulse);
                    m_RigidBody.drag = 0;
                }
                if (m_RigidBody.velocity.sqrMagnitude > movementSettings.CurrentTargetSpeed * movementSettings.CurrentTargetSpeed * 0.81f && !m_IsGrounded)
                {
                    m_RigidBody.drag = 0.8f;
                    
                    
                }
            }

            if (m_IsGrounded || (numberOfJumps < maxNumberOfJumps && numberOfJumps != 0)) //is the character able to jump
            {
                //this line is actually what slows the character down, good to know
                if(numberOfJumps == 0) m_RigidBody.drag = advancedSettings.slowDownRate;

                if (m_Jump)
                {
                   
                    m_RigidBody.velocity = new Vector3(m_RigidBody.velocity.x, 0f, m_RigidBody.velocity.z);
                    m_RigidBody.AddForce(new Vector3(0f, movementSettings.JumpForce, 0f), ForceMode.Impulse);
                    m_RigidBody.drag = advancedSettings.slowDownRateAir;
                    m_Jumping = true;
                    //doubleJump
                    numberOfJumps++;

                }

                if (!m_Jumping && Mathf.Abs(input.x) < float.Epsilon && Mathf.Abs(input.y) < float.Epsilon && m_RigidBody.velocity.magnitude < 1f)
                {
                    m_RigidBody.Sleep();
                }
            }
            else
            {
                //m_RigidBody.drag = advancedSettings.slowDownRateAir;
                if (m_PreviouslyGrounded && !m_Jumping)
                {
                    StickToGroundHelper();
                }

            }
            if (m_Jumping && cancelJump && m_RigidBody.velocity.y > float.Epsilon)
            {
                m_RigidBody.velocity = new Vector3(m_RigidBody.velocity.x, 0f, m_RigidBody.velocity.z);
                cancelJump = false;
            }

            m_Jump = false;

            
            speeds[speedsindex] = Mathf.Sqrt(Mathf.Pow(m_RigidBody.velocity.x, 2) + Mathf.Pow(m_RigidBody.velocity.z, 2));
            speeds[speedsindex] = Mathf.Round(speeds[speedsindex] * 100);
            //Debug.Log(speeds[speedsindex].ToString() + " / " + Velocity.y);
            if (speedsindex < 9) speedsindex++;
            else speedsindex = 0;


        }


        private float SlopeMultiplier()
        {
            float angle = Vector3.Angle(m_GroundContactNormal, Vector3.up);
            return movementSettings.SlopeCurveModifier.Evaluate(angle);
        }


        private void StickToGroundHelper()
        {
            RaycastHit hitInfo;
            if (Physics.SphereCast(transform.position, m_Capsule.radius * (1.0f - advancedSettings.shellOffset), Vector3.down, out hitInfo,
                                   ((m_Capsule.height / 2f) - m_Capsule.radius) +
                                   advancedSettings.stickToGroundHelperDistance, Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                if (Mathf.Abs(Vector3.Angle(hitInfo.normal, Vector3.up)) < 85f)
                {
                    m_RigidBody.velocity = Vector3.ProjectOnPlane(m_RigidBody.velocity, hitInfo.normal);
                }
            }
        }


        private Vector2 GetInput()
        {
            float tempX;
            float tempY;

            if (Input.GetKey(currentInput[0]))
            {
                tempY = 1;
            }
            else
            if (Input.GetKey(currentInput[2]))
            {
                tempY = -1;
            }
            else { tempY = 0; }
            if (Input.GetKey(currentInput[1]))
            {
                tempX = -1;
            }
            else
            if (Input.GetKey(currentInput[3]))
            {
                tempX = 1;
            }
            else { tempX = 0; }

            Vector2 input = new Vector2
            {
                x = tempX,
                y = tempY
                //x = CrossPlatformInputManager.GetAxis("Horizontal"),
                //y = CrossPlatformInputManager.GetAxis("Vertical")
            };
            movementSettings.UpdateDesiredTargetSpeed(input);
            return input;
        }


        private void RotateView()
        {
            //avoids the mouse looking if the game is effectively paused
            if (Mathf.Abs(Time.timeScale) < float.Epsilon) return;

            // get the rotation before it's changed
            float oldYRotation = transform.eulerAngles.y;

            mouseLook.LookRotation(transform, cam.transform);

            if (m_IsGrounded || advancedSettings.airControl)
            {
                // Rotate the rigidbody velocity to match the new direction that the character is looking
                Quaternion velRotation = Quaternion.AngleAxis(transform.eulerAngles.y - oldYRotation, Vector3.up);
                m_RigidBody.velocity = velRotation * m_RigidBody.velocity;
            }
        }

        /// sphere cast down just beyond the bottom of the capsule to see if the capsule is colliding round the bottom
        private void GroundCheck()
        {
            m_PreviouslyGrounded = m_IsGrounded;
            RaycastHit hitInfo;
            if (Physics.SphereCast(transform.position, m_Capsule.radius * (1.0f - advancedSettings.shellOffset), Vector3.down, out hitInfo,
                                   ((m_Capsule.height / 2f) - m_Capsule.radius) + advancedSettings.groundCheckDistance, Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                m_IsGrounded = true;
                m_GroundContactNormal = hitInfo.normal;
            }
            else
            {
                m_IsGrounded = false;
                m_GroundContactNormal = Vector3.up;
            }
            if (!m_PreviouslyGrounded && m_IsGrounded && m_Jumping)
            {
                m_Jumping = false;
                
                //resetting double jump variable
                numberOfJumps = 0;
            }
        }

        void TransitionEnter(GameManager.IdentityState state)
        {
            maxNumberOfJumps = 1;
            manualSwitchEnabled = false;
        }

        void TransitionExit(GameManager.IdentityState state)
        {
           
            switch (state)
            {
                case GameManager.IdentityState.Two:
                    currentIdentity = two;
                    maxNumberOfJumps = 2;
                    break;
                case GameManager.IdentityState.Blue:
                    currentIdentity = blue;
                    manualSwitchEnabled = true;
                    break;
                case GameManager.IdentityState.There:
                    currentIdentity = there;
                    //make invisible platforms visible
                    break;

            
           
            }

        }

        private void OnGUI()
        {
            float sum = 0;

            for(int i = 0; i < speeds.Length; i++)
            {
                sum += speeds[i];
            }
            sum /= 10;
            
            GUI.Label(new Rect(20, 70, 700, 80), "X-speed: " + sum * 0.01f, guiStyle);
            GUI.Label(new Rect(20, 110, 700, 80), "Y-speed: " + Mathf.Abs(m_RigidBody.velocity.y), guiStyle);
            GUI.Label(new Rect(20, 150, 700, 80), "Grounded: " + m_IsGrounded, guiStyle);



        }



    }
}
