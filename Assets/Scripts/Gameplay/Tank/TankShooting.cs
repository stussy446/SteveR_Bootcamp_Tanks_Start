using Photon.Pun;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Tanks
{
    public class TankShooting : MonoBehaviour
    {
        private const string FIRE_BUTTON = "Fire1";
        private const string AIRSTRIKE_BUTTON = "Fire3";

        [Header("airstrike configs")]
        public GameObject airStrikePrefab;
        public LayerMask boundariesLayerMask;

        public Rigidbody shell;
        public Transform fireTransform;
        public Slider aimSlider;
        public AudioSource shootingAudio;
        public AudioClip chargingClip;
        public AudioClip fireClip;
        public float minLaunchForce = 15f;
        public float maxLaunchForce = 30f;
        public float maxChargeTime = 0.75f;

        private PhotonView photonView;

        private float currentLaunchForce;
        private float chargeSpeed;
        private bool fired;

        private void OnEnable()
        {
            currentLaunchForce = minLaunchForce;
            aimSlider.value = minLaunchForce;
        }

        private void Start()
        {
            photonView = GetComponent<PhotonView>();

            chargeSpeed = (maxLaunchForce - minLaunchForce) / maxChargeTime;
        }

        private void Update()
        {
            // Only allow owner of this tank to shoot
            if (!photonView.IsMine)
            {
                return;
            }

            aimSlider.value = minLaunchForce;

            HandleBasicFire();
            HandleAirStrikeFire();
        }


        private void HandleBasicFire()
        {
            if (currentLaunchForce >= maxLaunchForce && !fired)
            {
                currentLaunchForce = maxLaunchForce;
                Fire();
            }
            else if (Input.GetButtonDown(FIRE_BUTTON))
            {
                StartChargingShot();
            }
            else if (Input.GetButton(FIRE_BUTTON) && !fired)
            {
                currentLaunchForce += chargeSpeed * Time.deltaTime;

            }
            else if (Input.GetButtonUp(FIRE_BUTTON) && !fired)
            {
                Fire();
            }

            Aim();
        }

        private void StartChargingShot()
        {
            fired = false;
            currentLaunchForce = minLaunchForce;
            shootingAudio.clip = chargingClip;
            shootingAudio.Play();
        }

        private void Fire()
        {
            fired = true;

            // Instantiate the projectile on all clients
            photonView.RPC
                (
                "Fire",
                RpcTarget.All,
                fireTransform.position,
                fireTransform.rotation,
                currentLaunchForce * fireTransform.forward
                );

            currentLaunchForce = minLaunchForce;
        }

        [PunRPC]
        private void Fire(Vector3 position, Quaternion rotation, Vector3 velocity)
        {
            Rigidbody shellInstance = Instantiate(shell, position, rotation);
            shellInstance.velocity = velocity;

            shootingAudio.clip = fireClip;
            shootingAudio.Play();

        }

        private void Aim()
        {
            // shows tank's aim slider to all clients
            photonView.RPC
                (
                "Aim",
                RpcTarget.All,
                currentLaunchForce
                );
        }

        [PunRPC]
        private void Aim(float launchForce)
        {
            aimSlider.value = launchForce;
        }

        private void HandleAirStrikeFire()
        {
            if (Input.GetButtonDown(AIRSTRIKE_BUTTON))
            {
                RaycastHit hit; 
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    // compare hit layer to boundaries layer using ints, if they match do nothing, otherwise fire an airstrike
                    int hitLayerValue = hit.collider.gameObject.layer;
                    int boundaryLayerValue = (int)Mathf.Log(boundariesLayerMask.value, 2);

                    if (hitLayerValue == boundaryLayerValue)
                    {
                        return;
                    }
                    else
                    {
                        photonView.RPC
                            (
                            "HandleAirStrike",
                            RpcTarget.All,
                            hit.point
                            );
                    }
                }
            }
        }

        [PunRPC]
        private void HandleAirStrike(Vector3 target)
        {
            GameObject airStrikeInstance = Instantiate(airStrikePrefab, target, Quaternion.identity);
            Destroy(airStrikeInstance, 1.5f);
        }

    }
}